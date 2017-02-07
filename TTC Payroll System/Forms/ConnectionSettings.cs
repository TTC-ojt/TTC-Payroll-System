using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace TTC_Payroll_System.Forms
{
    public partial class ConnectionSettings : Form
    {
        public ConnectionSettings()
        {
            InitializeComponent();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.Server = txtServer.Text;
            Properties.Settings.Default.Port = Convert.ToUInt32(txtPort.Text);
            Properties.Settings.Default.UserID = txtUserID.Text;
            Properties.Settings.Default.Password = txtPassword.Text;
            Properties.Settings.Default.Database = txtDatabase.Text;
            Properties.Settings.Default.Save();
            Close();
        }

        private void ConnectionSettings_Load(object sender, EventArgs e)
        {
            txtServer.Text = Properties.Settings.Default.Server;
            txtPort.Text = Properties.Settings.Default.Port.ToString();
            txtUserID.Text = Properties.Settings.Default.UserID;
            txtPassword.Text = Properties.Settings.Default.Password;
            txtDatabase.Text = Properties.Settings.Default.Database;
        }
    }
}
