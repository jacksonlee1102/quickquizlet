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
        Boolean isDisplayTerm = false;
        Random rnd = new Random();

        public MainForm()
        {
            InitializeComponent();
            
            //string textContent = System.IO.File.ReadAllText(System.IO.Directory.GetCurrentDirectory() + "\\data\\158284560.txt");
            //Stored.currentTerms = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Term>>(textContent);
            
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            this.reloadTerms();

            MainForm.ActiveForm.TopMost = true;
            setLandingPosition();
        }

        private void reloadTerms()
        {
            if (Utility.SettingMgr.fileExist())
            {
                Setting setting = Utility.SettingMgr.read();
                QuickQuizlet.Utility.QuizletAPI api = new QuickQuizlet.Utility.QuizletAPI();
                SetDetail sets = api.getSetDetail(setting.currentSetId, setting.clientId);
                Stored.currentTerms = sets.terms;

                timer1.Interval = setting.timeSetting * 1000;
                timer1.Start();

                int rndNum = rnd.Next(0, Stored.currentTerms.Count - 1);
                currentTerm = Stored.currentTerms[rndNum];
                this.updateStatus(rndNum);
                textBox1.Text = currentTerm.term;
                isDisplayTerm = true;
            }
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
                int rndNum = rnd.Next(0, Stored.currentTerms.Count - 1);
                currentTerm = Stored.currentTerms[rndNum];
                this.updateStatus(rndNum);
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
            setting.FormClosed += new FormClosedEventHandler(settingForm_Close);
            
        }

        public void settingForm_Close(object sender, FormClosedEventArgs e)
        {
            this.reloadTerms();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void updateStatus(int index)
        {
            lblStatus.Text = index.ToString() + "/" + Stored.currentTerms.Count.ToString();
        }

    }
}
