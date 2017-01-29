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
    public partial class UTD : Form
    {
        public UTD()
        {
            InitializeComponent();
        }

        Classes.Late late = new Classes.Late();
        List<Classes.Late> lates = new List<Classes.Late>();
        Classes.Employee employee = new Classes.Employee();

        private void Leaves_Load(object sender, EventArgs e)
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

        private void showData()
        {
            if (employee.id > 0)
                lates = Classes.Late.getLeavesByEmployeeID(employee.id);
            else
                lates = Classes.Late.getAll();
            dgvLeaves.Rows.Clear();
            foreach (Classes.Late late in lates)
            {
                employee = Classes.Employee.getById(late.employee_id);
                string name = employee.firstname + " " + employee.lastname + " " + employee.extname;
                dgvLeaves.Rows.Add(late.id, name, late.leave_date.ToShortDateString(), late.hours, late.penalty.ToString("N"), (late.hours*late.penalty).ToString("N"));
            }
            dgvLeaves.ClearSelection();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            late = new Classes.Late();
            late.employee_id = employee.id;
            late.leave_date = dtpLeaveDate.Value;
            late.hours = Convert.ToInt32(nudHours.Value);
            late.penalty = nudPenalty.Value;
            late.Save();
            showData();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            late.Delete();
            dgvLeaves.Rows.Remove(dgvLeaves.SelectedRows[0]);
        }

        private void dgvLeaves_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvLeaves.SelectedRows.Count > 0)
            {
                int id = Convert.ToInt32(dgvLeaves.SelectedRows[0].Cells[0].Value);
                late = lates.Find(l => l.id == id);
                btnDelete.Enabled = true;
            }
            else
            {
                btnDelete.Enabled = false;
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
            Program.main.Show();
        }

        Bitmap bmp;

        private void btnPrint_Click(object sender, EventArgs e)
        {
            bmp = new Bitmap(dgvLeaves.Width, dgvLeaves.Height);
            dgvLeaves.DrawToBitmap(bmp, new Rectangle(0, 0, dgvLeaves.Width, dgvLeaves.Height));
            printPreviewDialog.ShowDialog();
        }

        private void printDocument_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            e.Graphics.InterpolationMode = InterpolationMode.Bicubic;
            e.Graphics.SmoothingMode = SmoothingMode.HighQuality;
            e.Graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;
            e.Graphics.CompositingQuality = CompositingQuality.HighQuality;
            e.Graphics.DrawImage(bmp, 30, 50);
        }
    }
}
