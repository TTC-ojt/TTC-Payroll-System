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
    public partial class PayrollSummary1 : Form
    {
        public PayrollSummary1()
        {
            InitializeComponent();
        }

        DateTime date = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1);
        List<Classes.PayrollSummary1> payroll = new List<Classes.PayrollSummary1>();
        decimal pagibig = Classes.FixedDeductions.getPagIbig();

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
            decimal total_sssloan = 0m;
            decimal total_pagibigregular = 0m;
            decimal total_pagibigcalamity = 0m;
            decimal total_leave = 0m;
            decimal total_withtax = 0m;
            decimal total_netpay = 0m;
            decimal sub_monthlyrate = 0m;
            decimal sub_sssloan = 0m;
            decimal sub_pagibigregular = 0m;
            decimal sub_pagibigcalamity = 0m;
            decimal sub_leave = 0m;
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
                if (department.id == 4 || department.id == 5) continue;
                Classes.Position position = Classes.Position.getById(employee.position_id);
                decimal halfrate = position.salary / 2;
                decimal tax = Classes.WithholdingTax.getByEmployeeId(ps1.EmployeeID).tax15;
                decimal sss_loan = 0m;
                Classes.Sss_Loan sloan = Classes.Sss_Loan.getLoanByEmployeeId(ps1.EmployeeID);
                if (sloan.fortnightly) sss_loan = sloan.amount / 2;
                else sss_loan = sloan.amount;
                decimal pagibig_regular = 0m;
                decimal pagibig_calamity = 0m;
                Classes.PagibigLoans ploan = Classes.PagibigLoans.getEmployeeID(ps1.EmployeeID);
                if (ploan.fortnightly) { pagibig_regular = ploan.regular / 2; pagibig_calamity = ploan.calamity / 2; }
                else { pagibig_regular = ploan.regular; pagibig_calamity = ploan.calamity; }
                List<Classes.Leave> leaves = Classes.Leave.getLeavesByEmployeeIDAndDate(employee.id, date.Year + "-" + date.Month + "-01", date.Year + "-" + date.Month + "-15");
                decimal leave_count = 0;
                foreach (Classes.Leave leave in leaves)
                {
                    decimal type = 0;
                    if (leave.HalfDay)
                    {
                        type = 0.5m;
                    }
                    else
                    {
                        type = 1;
                    }
                    leave_count += type;
                }
                decimal leave_deduction = leave_count * Classes.FixedDeductions.getLeaveRate();
                decimal deductions = tax + sss_loan + pagibig_regular + pagibig_calamity + leave_deduction;
                decimal netpay = halfrate - (deductions);

                total_monthlyrate += position.salary;
                total_sssloan += sss_loan;
                total_pagibigregular += pagibig_regular;
                total_pagibigcalamity += pagibig_calamity;
                total_leave += leave_deduction;
                total_withtax += tax;
                total_netpay += netpay;
                if (deptID != department.id)
                {
                    if (deptID > 0)
                    {
                        dgvPayroll1.Rows.Add(0, "", "SUBTOTAL", sub_monthlyrate.ToString("N"), (sub_monthlyrate / 2).ToString("N"), sub_sssloan.ToString("N"), sub_pagibigregular.ToString("N"), sub_pagibigcalamity.ToString("N"), sub_leave.ToString("N"), sub_withtax.ToString("N"), sub_netpay.ToString("N"));
                        dgvPayroll1.Rows[dgvPayroll1.Rows.Count - 1].DefaultCellStyle.Font = new Font("Arial Rounded MT Bold", 8F);
                    }
                    sub_monthlyrate = 0m;
                    sub_sssloan = 0m;
                    sub_pagibigregular = 0m;
                    sub_pagibigcalamity = 0m;
                    sub_leave = 0m;
                    sub_withtax = 0m;
                    sub_netpay = 0m;
                    dgvPayroll1.Rows.Add(0, department.name);
                    dgvPayroll1[1, dgvPayroll1.Rows.Count - 1].Style.Font = new Font("Arial Rounded MT Bold", 8F);
                    deptID = department.id;
                }
                sub_monthlyrate += position.salary;
                sub_sssloan += sss_loan;
                sub_pagibigregular += pagibig_regular;
                sub_pagibigcalamity += pagibig_calamity;
                sub_leave += leave_deduction;
                sub_withtax += tax;
                sub_netpay += netpay;

                ps1.MonthlyRate = position.salary;
                ps1.SSS_Loan = sss_loan;
                ps1.Regular = pagibig_regular;
                ps1.Calamity = pagibig_calamity;
                ps1.Leave = leave_deduction;
                ps1.Tax = tax;
                dgvPayroll1.Rows.Add(ps1.ID, employee.GetFullName(), position.name, position.salary.ToString("N"), halfrate.ToString("N"), sss_loan.ToString("N"), pagibig_regular.ToString("N"), pagibig_calamity.ToString("N"), leave_deduction.ToString("N"), tax.ToString("N"), netpay.ToString("N"));
                dgvPayroll1.Columns[3].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgvPayroll1.Columns[4].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgvPayroll1.Columns[5].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgvPayroll1.Columns[6].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgvPayroll1.Columns[7].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgvPayroll1.Columns[8].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgvPayroll1.Columns[9].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgvPayroll1.Columns[10].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            }
            dgvPayroll1.Rows.Add(0, "", "SUBTOTAL", sub_monthlyrate.ToString("N"), (sub_monthlyrate / 2).ToString("N"), sub_sssloan.ToString("N"), sub_pagibigregular.ToString("N"), sub_pagibigcalamity.ToString("N"), sub_leave.ToString("N"), sub_withtax.ToString("N"), sub_netpay.ToString("N"));
            dgvPayroll1.Rows[dgvPayroll1.Rows.Count - 1].DefaultCellStyle.Font = new Font("Arial Rounded MT Bold", 8F);
            dgvPayroll1.Rows.Add(0, "GRAND TOTAL FOR HUMAN RESOURCES", "", total_monthlyrate.ToString("N"), (total_monthlyrate / 2).ToString("N"), total_sssloan.ToString("N"), total_pagibigregular.ToString("N"), total_pagibigcalamity.ToString("N"), total_leave.ToString("N"), total_withtax.ToString("N"), total_netpay.ToString("N"));
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
                    payslip.lblGrossTotal.Text = row.Cells[4].Value.ToString();
                    payslip.lblSSSLoan.Text = row.Cells[5].Value.ToString();
                    payslip.lblPagibigLoan.Text = (Convert.ToDecimal(row.Cells[6].Value) + Convert.ToDecimal(row.Cells[7].Value)).ToString("N");
                    payslip.lblLeave.Text = row.Cells[8].Value.ToString();
                    payslip.lblWithTax.Text = row.Cells[9].Value.ToString();
                    payslip.lblNetPay.Text = row.Cells[10].Value.ToString();

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

        private void dgvPayroll1_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            int id = Convert.ToInt32(dgvPayroll1.Rows[e.RowIndex].Cells[0].Value);

            if (id > 0)
            {
                Classes.PayrollSummary1 ps1 = payroll.Find(p => p.ID == id);

                string column = dgvPayroll1[e.ColumnIndex, e.RowIndex].OwningColumn.Name;

                decimal sss_loan = Convert.ToDecimal(dgvPayroll1.Rows[e.RowIndex].Cells["dgcSssLoan"].Value);
                decimal pagibig_regular = Convert.ToDecimal(dgvPayroll1.Rows[e.RowIndex].Cells["dgcPagibigRegular"].Value);
                decimal pagibig_calamity = Convert.ToDecimal(dgvPayroll1.Rows[e.RowIndex].Cells["dgcPagibigCalamity"].Value);
                decimal leaves = Convert.ToDecimal(dgvPayroll1.Rows[e.RowIndex].Cells["dgcLeaves"].Value);
                decimal withtax = Convert.ToDecimal(dgvPayroll1.Rows[e.RowIndex].Cells["dgcWithTax"].Value);

                if (column == dgcSssLoan.Name)
                {
                    sss_loan = Convert.ToDecimal(dgvPayroll1.Rows[e.RowIndex].Cells["dgcSssLoan"].Value);
                }
                if (column == dgcPagibigRegular.Name)
                {
                    pagibig_regular = Convert.ToDecimal(dgvPayroll1.Rows[e.RowIndex].Cells["dgcPagibigRegular"].Value);
                }
                if (column == dgcPagibigCalamity.Name)
                {
                    pagibig_calamity = Convert.ToDecimal(dgvPayroll1.Rows[e.RowIndex].Cells["dgcPagibigCalamity"].Value);
                }
                if (column == dgcLeaves.Name)
                {
                    leaves = Convert.ToDecimal(dgvPayroll1.Rows[e.RowIndex].Cells["dgcLeaves"].Value);
                }
                if (column == dgcWithTax.Name)
                {
                    withtax = Convert.ToDecimal(dgvPayroll1.Rows[e.RowIndex].Cells["dgcWithTax"].Value);
                }

                decimal deductions = sss_loan + pagibig_regular + pagibig_calamity + leaves + withtax;

                decimal netpay = ps1.MonthlyRate / 2 - deductions;
                dgvPayroll1.Rows[e.RowIndex].Cells["dgcNetPay"].Value = netpay;

                ps1.SSS_Loan = sss_loan;
                ps1.Regular = pagibig_regular;
                ps1.Calamity = pagibig_calamity;
                ps1.Leave = leaves;
                ps1.Tax = withtax;
            }
        }
    }
}
