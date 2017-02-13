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
    public partial class Archive15 : Form
    {
        public Archive15()
        {
            InitializeComponent();
        }

        DateTime date;
        private Bitmap image;

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
            Program.main.Show();
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            dgvPayroll1.ClearSelection();
            image = new Bitmap(pnlPrint.Width, pnlPrint.Height);
            pnlPrint.DrawToBitmap(image, new Rectangle(0, 0, pnlPrint.Width, pnlPrint.Height));
            printDocument.DefaultPageSettings.Landscape = true;
            printPreviewDialog.ShowDialog();
        }

        private void Archive15_Load(object sender, EventArgs e)
        {
            cbxMonth.SelectedIndex = 0;
            cbxYear.SelectedIndex = 0;
        }

        private void LoadData()
        {
            dgvPayroll1.Rows.Clear();
            List<Classes.PayrollSummary1> ps = Classes.PayrollSummary1.getByDate(date);
            foreach (Classes.PayrollSummary1 p in ps)
            {
                Classes.Employee employee = Classes.Employee.getById(p.EmployeeID);
                Classes.Department department = Classes.Department.getById(employee.department_id);
                Classes.Position position = Classes.Position.getById(employee.position_id);
                decimal halfrate = position.salary / 2;
                decimal deduction = p.SSS_Loan + p.Regular + p.Calamity + p.Leave + p.Tax;
                decimal netpay = halfrate - deduction;
                dgvPayroll1.Rows.Add(p.ID, employee.GetFullName(), position.name, position.salary.ToString("N"), halfrate.ToString("N"), p.SSS_Loan.ToString("N"), p.Regular.ToString("N"), p.Calamity.ToString("N"), p.Leave.ToString("N"), p.Tax.ToString("N"), netpay.ToString("N"));
            }
        }

        private void ChangeDate(object sender, EventArgs e)
        {
            if (cbxYear.SelectedIndex > -1)
            {
                int year = Convert.ToInt32(cbxYear.SelectedItem);
                int month = cbxMonth.SelectedIndex + 1;
                date = new DateTime(year, month, 1);
                LoadData();
            }
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
