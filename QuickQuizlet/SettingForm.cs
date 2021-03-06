﻿using System;
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
                txtTime.Text = this.setting.timeSetting.ToString();
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
            setting.timeSetting = int.Parse( txtTime.Text);

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
            btnCheck.Enabled = false;
            this.currentSet = this.getSetDetail();
            lblSetTitle.Text = this.currentSet.title;
            btnCheck.Enabled = true;
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
                lblLoading.Visible = true;
                btnLoad.Enabled = false;

                QuickQuizlet.Utility.QuizletAPI api = new QuickQuizlet.Utility.QuizletAPI();
                //List<SetDetail> listSets = api.getUserSets(txtUsername.Text, txtClientId.Text);
                UserDetail listSets = api.getUserSets(txtUsername.Text, txtClientId.Text);
                listSets.sets.Insert(0, new SetDetail());
                slListSet.DataSource = listSets.sets;
                slListSet.DisplayMember = "title";
                slListSet.ValueMember = "id";

                lblLoading.Visible = false;
                btnLoad.Enabled = true;
            }
            
        }

        private void slListSet_SelectedIndexChanged(object sender, EventArgs e)
        {
            int n;
            if (int.TryParse(slListSet.SelectedValue.ToString(), out n) && n > 0)
            txtSetId.Text = slListSet.SelectedValue.ToString();
        }

    }
}
