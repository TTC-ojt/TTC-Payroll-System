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
    public partial class PagibigLoan : Form
    {
        public PagibigLoan()
        {
            InitializeComponent();
        }

        List<Classes.PagibigLoans> loans = new List<Classes.PagibigLoans>();
        Classes.PagibigLoans loan = new Classes.PagibigLoans();
        Classes.Employee employee = new Classes.Employee();

        private void showData()
        {
            if (employee.id > 0)
            {
                loans = Classes.PagibigLoans.getAllEmployeeID(employee.id);
            }
            else
            {
                loans = Classes.PagibigLoans.getAll();
            }
            dgvPagibigLoans.Rows.Clear();
            foreach (Classes.PagibigLoans loan in loans)
            {
                employee = Classes.Employee.getById(loan.employee_id);
                dgvPagibigLoans.Rows.Add(loan.id, employee.GetFullName(), loan.regular, loan.calamity, loan.months_to_pay, loan.months_paid, loan.fortnightly);
            }
            dgvPagibigLoans.ClearSelection();
        }


        private void cbxEmployeeCode_SelectedIndexChanged(object sender, EventArgs e)
        {
            employee = Classes.Employee.getByCode(cbxEmployeeCode.SelectedItem.ToString());
            txtEmployeeName.Text = employee.GetFullName();
            showData();
        }

        private void dgvPagibigLoans_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            loan.employee_id = employee.id;
            loan.regular = nudRegular.Value;
            loan.calamity = nudCalamity.Value;
            loan.months_to_pay = Convert.ToInt32(nudMonthToPay.Value);
            loan.months_paid = Convert.ToInt32(nudMonthsPaid.Value);
            loan.fortnightly = chkFortNightly.Checked;
            loan.Save();
            showData();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
            Program.main.Show();
        }

        private void PagibigLoan_Load(object sender, EventArgs e)
        {
            List<Classes.Employee> employees = Classes.Employee.getAll();
            cbxEmployeeCode.Items.Add("All Employee");
            foreach (Classes.Employee employee in employees)
            {
                cbxEmployeeCode.Items.Add(employee.code);
            }
            cbxEmployeeCode.SelectedIndex = 0;
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            loan.Delete();
            dgvPagibigLoans.Rows.Remove(dgvPagibigLoans.SelectedRows[0]);
        }

        private void dgvPagibigLoans_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvPagibigLoans.SelectedRows.Count > 0)
            {
                int id = Convert.ToInt32(dgvPagibigLoans.SelectedRows[0].Cells[0].Value);
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
            image = new Bitmap(dgvPagibigLoans.Width, dgvPagibigLoans.Height);
            dgvPagibigLoans.DrawToBitmap(image, new Rectangle(0, 0, dgvPagibigLoans.Width, dgvPagibigLoans.Height));
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
