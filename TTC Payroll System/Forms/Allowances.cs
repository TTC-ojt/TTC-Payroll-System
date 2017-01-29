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
    public partial class Allowances : Form
    {
        public Allowances()
        {
            InitializeComponent();
        }

        List<Classes.Employee> employees = new List<Classes.Employee>();
        Classes.Employee employee = new Classes.Employee();

        private void showData()
        {
            employees = Classes.Employee.getAll();
            dgvAllowances.Rows.Clear();
            foreach(Classes.Employee employee in employees)
            {
                dgvAllowances.Rows.Add(employee.id, employee.GetFullName(), Classes.Allowance.getByEmployeeId(employee.id).amount);
            }
            dgvAllowances.ClearSelection();
        }

        private void Allowance_Load(object sender, EventArgs e)
        {
            showData();
        }

        private void dgvAllowances_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvAllowances.SelectedRows.Count > 0)
            {
                 int id = Convert.ToInt32(dgvAllowances.SelectedRows[0].Cells[0].Value);
                this.employee = employees.Find(em => em.id == id);
            }
            else
            {
                this.employee = new Classes.Employee();
            }
            txtCode.Text = employee.code;
            txtEmployeeName.Text = employee.GetFullName();
            nudAmount.Value = Classes.Allowance.getByEmployeeId(employee.id).amount;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
            Program.main.Show();
        }

        Bitmap image;

        private void btnPrint_Click(object sender, EventArgs e)
        { 
            image = new Bitmap(dgvAllowances.Width, dgvAllowances.Height);
            dgvAllowances.DrawToBitmap(image, new Rectangle(0, 0, dgvAllowances.Width, dgvAllowances.Height));
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

        private void btnSave_Click(object sender, EventArgs e)
        {
            Classes.Allowance allowance = Classes.Allowance.getByEmployeeId(employee.id);
            allowance.employee_id = employee.id;
            allowance.amount = nudAmount.Value;
            allowance.Save();
            showData();
        }
    }
}
