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
    public partial class Pagibig : Form
    {
        public Pagibig()
        {
            InitializeComponent();
        }
        private void Pagibig_Load(object sender, EventArgs e)
        {
            ShowData();
        }

        private void ShowData()
        {
            nudPagIbig.Value = Classes.FixedDeductions.getPagIbig();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            Classes.FixedDeductions.SavePagiBig(nudPagIbig.Value);
            ShowData();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
            Program.main.Show();
        }
    }
}
