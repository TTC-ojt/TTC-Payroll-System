using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace TTC_Payroll_System.Forms
{
    public partial class SssLoans : Form
    {
        public SssLoans()
        {
            InitializeComponent();
        }

        List<Classes.Sss_Loan> loans = new List<Classes.Sss_Loan>();
        Classes.Sss_Loan loan = new Classes.Sss_Loan();
        Classes.Employee employee = new Classes.Employee();

        private void showData()
        {
            if(employee.id > 0)
            {
                loans = Classes.Sss_Loan.getLoansByEmployeeId(employee.id);
            }
            else
            {
                loans = Classes.Sss_Loan.getAll();
            }
            dgvSssLoan.Rows.Clear();
            foreach (Classes.Sss_Loan loan in loans)
            {
                employee = Classes.Employee.getById(loan.employee_id);
                string name = employee.firstname + " " + employee.lastname + " " + employee.extname;
                dgvSssLoan.Rows.Add(loan.id, name, loan.amount, loan.months_to_pay, loan.months_paid);
            }
            dgvSssLoan.ClearSelection();
        }

        private void SssLoans_Load(object sender, EventArgs e)
        {
            List<Classes.Employee> employees = Classes.Employee.getAll();
            cbxEmployeeCode.Items.Add("All Employee");
            foreach (Classes.Employee employee in employees)
            {
                cbxEmployeeCode.Items.Add(employee.code);
            }
            cbxEmployeeCode.SelectedIndex = 0;
        }

        private void cbxEmployeeCode_SelectedIndexChanged(object sender, EventArgs e)
        {
            employee = Classes.Employee.getByCode(cbxEmployeeCode.SelectedItem.ToString());
            string name = employee.firstname + " " + employee.lastname + " " + employee.extname;
            txtEmployeeName.Text = name;
            showData();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
            Program.main.Show();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            loan = new Classes.Sss_Loan();
            loan.employee_id = employee.id;
            loan.amount = nudAmount.Value;
            loan.months_to_pay = Convert.ToInt32(nudMonthToPay.Value);
            loan.months_paid = Convert.ToInt32(nudMonthsPaid.Value);
            loan.Save();
            showData();
            nudMonthToPay.Value = 0;
            nudAmount.Value = 0;
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            loan.Delete();
            dgvSssLoan.Rows.Remove(dgvSssLoan.SelectedRows[0]);
        }

        private void dgvSssLoan_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvSssLoan.SelectedRows.Count > 0)
            {
                int id = Convert.ToInt32(dgvSssLoan.SelectedRows[0].Cells[0].Value);
                loan = loans.Find(l => l.id == id);
                btnDelete.Enabled = true;
            }
            else
            {
                btnDelete.Enabled = false;
            }
        }

        Bitmap image;

        private void btnPrint_Click(object sender, EventArgs e)
        {
            image = new Bitmap(dgvSssLoan.Width, dgvSssLoan.Height);
            dgvSssLoan.DrawToBitmap(image, new Rectangle(0, 0, dgvSssLoan.Width, dgvSssLoan.Height));
            printPreviewDialog.ShowDialog();
        }

        private void printDocument_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            e.Graphics.InterpolationMode = InterpolationMode.Bicubic;
            e.Graphics.SmoothingMode = SmoothingMode.HighQuality;
            e.Graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;
            e.Graphics.CompositingQuality = CompositingQuality.HighQuality;
            e.Graphics.DrawImage(image, 30, 50);
        }
    }
}
