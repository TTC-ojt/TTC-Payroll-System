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
    public partial class Archive30 : Form
    {
        public Archive30()
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
            dgvPayroll2.ClearSelection();
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
            dgvPayroll2.Rows.Clear();
            List<Classes.PayrollSummary2> ps = Classes.PayrollSummary2.getByDate(date);
            foreach (Classes.PayrollSummary2 p in ps)
            {
                Classes.Employee employee = Classes.Employee.getById(p.EmployeeID);
                Classes.Department department = Classes.Department.getById(employee.department_id);
                Classes.Position position = Classes.Position.getById(employee.position_id);
                decimal halfrate = position.salary / 2;
                decimal deduction = p.SSS + p.PhilHealth + p.PagIbig + p.SssLoan + p.PagibigRegular + p.PagibigCalamity + p.Leave + p.Tax;
                decimal netpay = halfrate - deduction;
                dgvPayroll2.Rows.Add(p.ID, employee.GetFullName(), position.name, position.salary.ToString("N"), halfrate.ToString("N"), p.SSS.ToString("N"), p.PhilHealth.ToString("N"), p.PagIbig.ToString("N"), p.SssLoan.ToString("N"), p.PagibigRegular.ToString("N"), p.PagibigCalamity.ToString("N"), p.Leave.ToString("N"), p.Tax.ToString("N"), netpay.ToString("N"));
            }
        }

        private void ChangeDate(object sender, EventArgs e)
        {
            if (cbxYear.SelectedIndex > -1)
            {
                int year = Convert.ToInt32(cbxYear.SelectedItem);
                int month = cbxMonth.SelectedIndex + 1;
                date = new DateTime(year, month, 16);
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
