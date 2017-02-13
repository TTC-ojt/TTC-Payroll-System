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
    public partial class Leaves : Form
    {
        public Leaves()
        {
            InitializeComponent();
        }

        Classes.Leave leave = new Classes.Leave();
        List<Classes.Leave> leaves = new List<Classes.Leave>();
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
            txtEmployeeName.Text = employee.GetFullName();
            showData();
        }

        private void showData()
        {
            if (employee.id > 0)
                leaves = Classes.Leave.getLeavesByEmployeeID(employee.id);
            else
                leaves = Classes.Leave.getAll();
            dgvLeaves.Rows.Clear();
            foreach (Classes.Leave leave in leaves)
            {
                employee = Classes.Employee.getById(leave.employee_id);
                string leavetype = "";
                if (leave.HalfDay) leavetype = "Half Day"; else leavetype = "Whole Day";
                dgvLeaves.Rows.Add(leave.id, employee.GetFullName(), leave.leave_date.ToShortDateString(), leavetype, leave.Count);
            }
            dgvLeaves.ClearSelection();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            leave = new Classes.Leave();
            leave.employee_id = employee.id;
            leave.leave_date = dtpLeaveDate.Value;
            leave.HalfDay = chkHalfDay.Checked;
            leave.Count = nudCount.Value;
            leave.Save();
            showData();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            leave.Delete();
            dgvLeaves.Rows.Remove(dgvLeaves.SelectedRows[0]);
        }

        private void dgvLeaves_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvLeaves.SelectedRows.Count > 0)
            {
                int id = Convert.ToInt32(dgvLeaves.SelectedRows[0].Cells[0].Value);
                leave = leaves.Find(l => l.id == id);
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

        private void dtpLeaveDate_ValueChanged(object sender, EventArgs e)
        {
            if (employee.id >0)
            {
                decimal count = 0m;
                if (chkHalfDay.Checked) count = 0.5m; else count = 1m;
                nudCount.Value = Classes.Leave.GetCountByEmployeeID(employee.id) + count;
            }
        }

        private void chkHalfDay_CheckedChanged(object sender, EventArgs e)
        {
            if (employee.id > 0)
            {
                decimal count = 0m;
                if (chkHalfDay.Checked) count = 0.5m; else count = 1m;
                nudCount.Value = Classes.Leave.GetCountByEmployeeID(employee.id) + count;
            }
        }
    }
}
