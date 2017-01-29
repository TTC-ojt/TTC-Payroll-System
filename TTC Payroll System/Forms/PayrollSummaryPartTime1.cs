using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace TTC_Payroll_System.Forms
{
    public partial class PayrollSummaryPartTime1 : Form
    {
        public PayrollSummaryPartTime1()
        {
            InitializeComponent();
        }

        DateTime date = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1);
        List<Classes.PayrollSummary1> payroll = new List<Classes.PayrollSummary1>();

        public void NewSet()
        {
            List<Classes.Employee> employees = Classes.Employee.getAll();
            foreach (Classes.Employee employee in employees)
            {
                Classes.PayrollSummary1 ps1 = new Classes.PayrollSummary1();
                ps1.EmployeeID = employee.id;
                ps1.Date = date;
                ps1.Save();
            }
            showData();
        }

        public void showData()
        {
            decimal total_monthlyrate = 0m;
            decimal total_withtax = 0m;
            decimal total_netpay = 0m;
            decimal sub_monthlyrate = 0m;
            decimal sub_withtax = 0m;
            decimal sub_netpay = 0m;
            payroll = Classes.PayrollSummary1.getByDate(date);
            dgvPayroll1.Rows.Clear();
            Classes.Department department = new Classes.Department();
            int deptID = 0;
            foreach (Classes.PayrollSummary1 ps1 in payroll)
            {
                Classes.Employee employee = Classes.Employee.getById(ps1.EmployeeID);
                department = Classes.Department.getById(employee.department_id);
                if (department.id != 5) continue;
                Classes.Position position = Classes.Position.getById(employee.position_id);
                decimal halfrate = position.salary / 2;
                decimal tax = Classes.WithholdingTax.getByEmployeeId(ps1.EmployeeID).tax15;
                decimal deductions = tax;
                decimal netpay = halfrate - (deductions);
                total_monthlyrate += position.salary;
                total_withtax += tax;
                total_netpay += netpay;
                if (deptID != department.id)
                {
                    if (deptID > 0)
                    {
                        dgvPayroll1.Rows.Add(0, "", "SUBTOTAL", sub_monthlyrate.ToString("N"), (sub_monthlyrate / 2).ToString("N"), sub_withtax.ToString("N"), sub_netpay.ToString("N"));
                        dgvPayroll1.Rows[dgvPayroll1.Rows.Count - 1].DefaultCellStyle.Font = new Font("Arial Rounded MT Bold", 8F);
                    }
                    sub_monthlyrate = 0m;
                    sub_withtax = 0m;
                    sub_netpay = 0m;
                    dgvPayroll1.Rows.Add(0, department.name);
                    dgvPayroll1[1, dgvPayroll1.Rows.Count - 1].Style.Font = new Font("Arial Rounded MT Bold", 8F);
                    deptID = department.id;
                }
                sub_monthlyrate += position.salary;
                sub_withtax += tax;
                sub_netpay += netpay;

                ps1.MonthlyRate = position.salary;
                ps1.Tax = tax;
                ps1.Save();
                dgvPayroll1.Rows.Add(ps1.ID, employee.GetFullName(), position.name, position.salary.ToString("N"), halfrate.ToString("N"), tax.ToString("N"), netpay.ToString("N"));
                dgvPayroll1.Columns[3].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgvPayroll1.Columns[4].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgvPayroll1.Columns[5].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgvPayroll1.Columns[6].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            }
            dgvPayroll1.Rows.Add(0, "", "SUBTOTAL", sub_monthlyrate.ToString("N"), (sub_monthlyrate / 2).ToString("N"), sub_withtax.ToString("N"), sub_netpay.ToString("N"));
            dgvPayroll1.Rows[dgvPayroll1.Rows.Count - 1].DefaultCellStyle.Font = new Font("Arial Rounded MT Bold", 8F);
            dgvPayroll1.Rows.Add(0, "GRAND TOTAL FOR HUMAN RESOURCES", "", total_monthlyrate.ToString("N"), (total_monthlyrate / 2).ToString("N"), total_withtax.ToString("N"), total_netpay.ToString("N"));
            dgvPayroll1.Rows[dgvPayroll1.Rows.Count - 1].DefaultCellStyle.Font = new Font("Arial Rounded MT Bold", 8F);
            dgvPayroll1.ClearSelection();
        }

        private void PayrollSummary1_Load(object sender, EventArgs e)
        {
            lblSummaryDate.Text = "Payroll Summary for " + date.ToString("MMMM") + " 1-15, " + date.ToString("yyyy");
            List<Classes.Employee> employees = Classes.Employee.getAll();

            Classes.Employee employee = new Classes.Employee();

            employee = employees.Find(em => em.position_id == 1);
            lblAccountingAssistant.Text = employee.GetFullName();

            employee = employees.Find(em => em.position_id == 7);
            lblCenterAdministrator.Text = employee.GetFullName();

            payroll = Classes.PayrollSummary1.getByDate(date);
            foreach (Classes.PayrollSummary1 ps1 in payroll)
            {
                ps1.Delete();
            }
            NewSet();
        }

        private void btnPayslip_Click(object sender, EventArgs e)
        {
            images.Clear();
            foreach (DataGridViewRow row in dgvPayroll1.Rows)
            {
                if (Convert.ToInt32(row.Cells[0].Value) > 0)
                {
                    Print.Payslip1st payslip = new Print.Payslip1st();
                    int id = Convert.ToInt32(row.Cells[0].Value);
                    Classes.PayrollSummary1 ps1 = payroll.Find(ps => ps.ID == id);
                    payslip.lblEmployeeCode.Text = Classes.Employee.getById(ps1.EmployeeID).code;
                    payslip.lblPayrollDate.Text = date.ToString("M/15/yyyy");
                    payslip.lblEmployeeName.Text = row.Cells[1].Value.ToString();
                    payslip.lblEmployeePosition.Text = row.Cells[2].Value.ToString();
                    payslip.lblGrossPay.Text = row.Cells[4].Value.ToString();
                    payslip.lblWithTax.Text = row.Cells[5].Value.ToString();
                    payslip.lblNetPay.Text = row.Cells[6].Value.ToString();

                    payslip.Show();
                    image = new Bitmap(payslip.Width, payslip.Height);
                    payslip.DrawToBitmap(image, new Rectangle(0, 0, payslip.Width, payslip.Height));
                    payslip.Close();
                    images.Add(image);
                }
            }
            psPrint = true;
            printDocument.DefaultPageSettings.Landscape = true;
            printPreviewDialog.ShowDialog();
        }

        List<Bitmap> images = new List<Bitmap>();
        bool psPrint = false;
        int psIndex = 0;

        Bitmap image;

        private void printDocument_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            e.Graphics.InterpolationMode = InterpolationMode.Bicubic;
            e.Graphics.SmoothingMode = SmoothingMode.HighQuality;
            e.Graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;
            e.Graphics.CompositingQuality = CompositingQuality.HighQuality;
            if (psPrint)
            {
                if (psIndex < images.Count)
                {
                    Bitmap bitmap = new Bitmap(images[0].Width * 3, images[0].Height);

                    for (int i = 0; i < 3; i++)
                    {
                        if (psIndex == images.Count) break;
                        Graphics g = Graphics.FromImage(bitmap);
                        g.DrawImage(images[psIndex], i * images[0].Width, 0);
                        psIndex++;
                    }
                    e.Graphics.DrawImage(bitmap, 30, 50);
                    if (psIndex == images.Count)
                    {
                        e.HasMorePages = false;
                        psIndex = 0;
                    } else
                    {
                        e.HasMorePages = true;
                    }
                }
            }
            else
            {
                e.Graphics.DrawImage(image, 30, 50);
            }
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            psPrint = false;
            dgvPayroll1.ClearSelection();
            image = new Bitmap(pnlPrint.Width, pnlPrint.Height);
            pnlPrint2.Visible = true;
            pnlPrint.DrawToBitmap(image, new Rectangle(0, 0, pnlPrint.Width, pnlPrint.Height));
            pnlPrint2.Visible = false;
            printDocument.DefaultPageSettings.Landscape = true;
            printPreviewDialog.ShowDialog();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
            Program.main.Show();
        }

    }
}
