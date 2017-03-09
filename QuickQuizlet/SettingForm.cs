using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using QuickQuizlet.Entity;

namespace QuickQuizlet
{
    public partial class SettingForm : Form
    {

        public MainForm mainForm;
        private Setting setting = new Setting();
        private SetDetail currentSet;

        public SettingForm()
        {
            InitializeComponent();
        }

        private void SettingForm_Load(object sender, EventArgs e)
        {
            if (Utility.SettingMgr.fileExist())
            {
                this.setting = Utility.SettingMgr.read();
                txtClientId.Text = this.setting.clientId;
                txtSetId.Text = this.setting.currentSetId;
                txtUsername.Text = this.setting.userFollow;
            }
            else
            {
                txtClientId.Text = "BTQVCsVWDc";
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            setting.clientId = txtClientId.Text;
            setting.currentSetId = txtSetId.Text;
            setting.userFollow = txtUsername.Text;

            Utility.SettingMgr.write(setting);
            if (this.currentSet == null)
            {
                this.currentSet = this.getSetDetail();
            }
            Stored.currentTerms = this.currentSet.terms;
            this.Close();
        }

        private void btnCheck_Click(object sender, EventArgs e)
        {
            
            this.currentSet = this.getSetDetail();
            lblSetTitle.Text = this.currentSet.title;
        }

        private SetDetail getSetDetail()
        {
            string clientId = txtClientId.Text;
            string setId = txtSetId.Text;
            if (clientId != "" && setId != "")
            {
                QuickQuizlet.Utility.QuizletAPI api = new QuickQuizlet.Utility.QuizletAPI();
                return api.getSetDetail(setId, clientId);
            }
            return new SetDetail();
            
        }

        private void btnLoad_Click(object sender, EventArgs e)
        {
            if(txtUsername.Text != "")
            {
                QuickQuizlet.Utility.QuizletAPI api = new QuickQuizlet.Utility.QuizletAPI();
                List<SetDetail> listSets = api.getUserSets(txtUsername.Text, txtClientId.Text);
                slListSet.DataSource = listSets;
                slListSet.DisplayMember = "title";
                slListSet.ValueMember = "id";
            }
            
        }

        private void slListSet_SelectedIndexChanged(object sender, EventArgs e)
        {
            int n;
            if (int.TryParse(slListSet.SelectedValue.ToString(),out n))
            txtSetId.Text = slListSet.SelectedValue.ToString();
        }

    }
}
