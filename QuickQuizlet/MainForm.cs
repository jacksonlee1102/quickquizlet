using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using QuickQuizlet.Entity;

namespace QuickQuizlet
{
    public partial class MainForm : Form
    {
        Term currentTerm = new Term();
        List<Term> listTerm = new List<Term>();
        Boolean isDisplayTerm = false;
        Random rnd = new Random();

        public MainForm()
        {
            InitializeComponent();
            
            string textContent = System.IO.File.ReadAllText(System.IO.Directory.GetCurrentDirectory() + "\\data\\158284560.txt");
            listTerm = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Term>>(textContent);
            
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            

            MainForm.ActiveForm.TopMost = true;
            timer1.Interval = 3000;
            timer1.Start();

            currentTerm = listTerm[rnd.Next(0, listTerm.Count - 1)];
            textBox1.Text = currentTerm.term;
            isDisplayTerm = true;

            setLandingPosition();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (isDisplayTerm)
            {
                textBox1.Text = currentTerm.definition;
                isDisplayTerm = false;
            }
            else
            {
                currentTerm = listTerm[rnd.Next(0, listTerm.Count - 1)];
                textBox1.Text = currentTerm.term;
                isDisplayTerm = true;
            }
        }

        private void setLandingPosition()
        {
            //Rectangle resolution = Screen.PrimaryScreen.Bounds;
            Rectangle resolution = Screen.PrimaryScreen.WorkingArea;
            this.Location = new System.Drawing.Point(resolution.Right - 250, resolution.Bottom - 250);
        }

        private void btnSetting_Click(object sender, EventArgs e)
        {
            SettingForm setting = new SettingForm();
            setting.mainForm = this;
            setting.Show();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
