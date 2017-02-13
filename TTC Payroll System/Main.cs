using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TTC_Payroll_System
{
    public partial class Main : Form
    {
        public Main()
        {
            InitializeComponent();
        }

        private void btnDepartments_Click(object sender, EventArgs e)
        {
            Forms.Departments department = new Forms.Departments();
            department.Show();
            this.Hide();
        }

        private void btnPositions_Click(object sender, EventArgs e)
        {
            Forms.Positions position = new Forms.Positions();
            position.Show();
            this.Hide();
        }

        private void btnPhilHealth_Click(object sender, EventArgs e)
        {
            Forms.PhilHealth philhealth = new Forms.PhilHealth();
            philhealth.Show();
            this.Hide();
        }

        private void btnEmployees_Click(object sender, EventArgs e)
        {
            Forms.Employees employees = new Forms.Employees();
            employees.Show();
            this.Hide();
        }

        private void btnSss_Click(object sender, EventArgs e)
        {
            Forms.Sss sss = new Forms.Sss();
            sss.Show();
            this.Hide();
        }

        private void btnPagibig_Click(object sender, EventArgs e)
        {
            Forms.Pagibig pagibig = new Forms.Pagibig();
            pagibig.Show();
            this.Hide();
        }

        private void btnPayrollSummary1_Click(object sender, EventArgs e)
        {
            Forms.PayrollSummary1 ps1 = new Forms.PayrollSummary1();
            ps1.Show();
            this.Hide();
        }

        private void btnTeachersPayroll1_Click(object sender, EventArgs e)
        {
            Forms.PayrollSummaryTeachers1 ps1 = new Forms.PayrollSummaryTeachers1();
            ps1.Show();
            this.Hide();
        }

        private void btnPayrollSummary2_Click(object sender, EventArgs e)
        {
            Forms.PayrollSummary2 ps2 = new Forms.PayrollSummary2();
            ps2.Show();
            this.Hide();
        }

        private void btnTeachersPayroll2_Click(object sender, EventArgs e)
        {
            Forms.PayrollSummaryTeachers2 ps1 = new Forms.PayrollSummaryTeachers2();
            ps1.Show();
            this.Hide();
        }

        private void btnTax_Click(object sender, EventArgs e)
        {
            Forms.Withholdingtax tax = new Forms.Withholdingtax();
            tax.Show();
            this.Hide();
        }

        private void btnLeaves_Click(object sender, EventArgs e)
        {
            Forms.Leaves leave = new Forms.Leaves();
            leave.Show();
            this.Hide();
        }

        private void btnSssLoans_Click(object sender, EventArgs e)
        {
            Forms.SssLoans loans = new Forms.SssLoans();
            loans.Show();
            this.Hide();
        }

        private void btnSocialObligations_Click(object sender, EventArgs e)
        {
            Forms.SocialObligation so = new Forms.SocialObligation();
            so.Show();
            this.Hide();
        }

        private void btnPagIbigLoan_Click(object sender, EventArgs e)
        {
            Forms.PagibigLoan pagibigloans = new Forms.PagibigLoan();
            pagibigloans.Show();
            this.Hide();
        }

        private void Main_Load(object sender, EventArgs e)
        {
            lblDateTime.Text = DateTime.Now.ToString("MMMM dd, yyyy hh:mm:ss tt");
            ActiveControl = lblDateTime;
        }

        private void tmrDateTime_Tick(object sender, EventArgs e)
        {
            lblDateTime.Text = DateTime.Now.ToString("MMMM dd, yyyy hh:mm:ss tt");
        }

        private void btnHide_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnAllowance_Click(object sender, EventArgs e)
        {
            Forms.Allowances allowance = new Forms.Allowances();
            allowance.Show();
            this.Hide();
        }

        private void btnPartTimePayroll2_Click(object sender, EventArgs e)
        {
            Forms.PayrollSummaryPartTime2 ps1 = new Forms.PayrollSummaryPartTime2();
            ps1.Show();
            this.Hide();
        }

        private void btnPartTimePayroll1_Click(object sender, EventArgs e)
        {
            Forms.PayrollSummaryPartTime1 ps1 = new Forms.PayrollSummaryPartTime1();
            ps1.Show();
            this.Hide();
        }

        private void btnConnectionSettings_Click(object sender, EventArgs e)
        {
            Forms.ConnectionSettings cs = new Forms.ConnectionSettings();
            cs.ShowDialog();
        }

        private void btnExcessLoad_Click(object sender, EventArgs e)
        {
            Forms.ExcessLoad el = new Forms.ExcessLoad();
            el.Show();
            Hide();
        }

        private void btnArchive1_Click(object sender, EventArgs e)
        {
            Forms.Archive15 a1 = new Forms.Archive15();
            a1.Show();
            Hide();
        }

        private void btnArchive2_Click(object sender, EventArgs e)
        {
            Forms.Archive30 a2 = new Forms.Archive30();
            a2.Show();
            Hide();
        }
    }
}
