using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Windows.Forms;
using Excel = Microsoft.Office.Interop.Excel;

namespace TTC_Payroll_System.Forms
{
    public partial class PayrollSummary2 : Form
    {
        public PayrollSummary2()
        {
            InitializeComponent();
        }

        DateTime date = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 16);
        List<Classes.PayrollSummary2> ps2s = new List<Classes.PayrollSummary2>();
        List<Classes.Sss> sss = new List<Classes.Sss>();
        decimal pagibig = Classes.FixedDeductions.getPagIbig();
        List<Classes.PhilHealth> philhealths = new List<Classes.PhilHealth>();

        //methods
        public void showData()
        {
            decimal total_monthlyrate = 0m;
            decimal total_sss = 0m;
            decimal total_philhealth = 0m;
            decimal total_pagibig = 0m;
            decimal total_sssloan = 0m;
            decimal total_pagibigregular = 0m;
            decimal total_pagibigcalamity = 0m;
            decimal total_leave = 0m;
            decimal total_withtax = 0m;
            decimal total_netpay = 0m;
            decimal sub_monthlyrate = 0m;
            decimal sub_sss = 0m;
            decimal sub_philhealth = 0m;
            decimal sub_pagibig = 0m;
            decimal sub_sssloan = 0m;
            decimal sub_pagibigregular = 0m;
            decimal sub_pagibigcalamity = 0m;
            decimal sub_leave = 0m;
            decimal sub_withtax = 0m;
            decimal sub_netpay = 0m;
            ps2s = Classes.PayrollSummary2.getByDate(date);
            dgvPayroll2.Rows.Clear();
            Classes.Department department = new Classes.Department();
            int deptID = 0;
            foreach (Classes.PayrollSummary2 ps2 in ps2s)
            {
                Classes.Employee employee = Classes.Employee.getById(ps2.EmployeeID);
                department = Classes.Department.getById(employee.department_id);
                if (department.id == 4 || department.id == 5) continue;
                Classes.Position position = Classes.Position.getById(employee.position_id);
                decimal halfrate = position.salary / 2;
                decimal sssRate = 0;
                decimal philhealthRate = 0;
                foreach (Classes.Sss ss in sss)
                {
                    if (ss.monthlySalary <= position.salary)
                    {
                        sssRate = ss.ssEe;
                        break;
                    }
                }
                foreach (Classes.PhilHealth philhealth in philhealths)
                {
                    if (philhealth.salary_base <= position.salary)
                    {
                        philhealthRate = philhealth.employee_share;
                        break;
                    }
                }
                decimal sss_loan = 0m;
                Classes.Sss_Loan sloan = Classes.Sss_Loan.getLoanByEmployeeId(ps2.EmployeeID);
                if (sloan.fortnightly) sss_loan = sloan.amount / 2;
                decimal pagibig_regular = 0m;
                decimal pagibig_calamity = 0m;
                Classes.PagibigLoans ploan = Classes.PagibigLoans.getEmployeeID(ps2.EmployeeID);
                if (ploan.fortnightly) { pagibig_regular = ploan.regular / 2; pagibig_calamity = ploan.calamity / 2; }
                List<Classes.Leave> leaves = Classes.Leave.getLeavesByEmployeeIDAndDate(employee.id, date.Year + "-" + date.Month + "-16", date.Year + "-" + date.Month + "-" + DateTime.DaysInMonth(date.Year, date.Month));
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
                decimal tax = Classes.WithholdingTax.getByEmployeeId(ps2.EmployeeID).tax30;
                decimal deductions = sssRate + philhealthRate + pagibig + tax + leave_deduction;
                decimal netpay = halfrate - (deductions);

                total_monthlyrate += position.salary;
                total_sss += sssRate;
                total_philhealth += philhealthRate;
                total_pagibig += pagibig;
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
                        dgvPayroll2.Rows.Add(0, "", "SUBTOTAL", sub_monthlyrate.ToString("N"), (sub_monthlyrate / 2).ToString("N"), sub_sss.ToString("N"), sub_philhealth.ToString("N"), sub_pagibig.ToString("N"), sub_sssloan.ToString("N"), sub_pagibigregular.ToString("N"), sub_pagibigcalamity.ToString("N"), sub_leave.ToString("N"), sub_withtax.ToString("N"), sub_netpay.ToString("N"));
                        dgvPayroll2.Rows[dgvPayroll2.Rows.Count - 1].DefaultCellStyle.Font = new Font("Arial Rounded MT Bold", 8F);
                    }
                    sub_monthlyrate = 0m;
                    sub_sss = 0m;
                    sub_philhealth = 0m;
                    sub_pagibig = 0m;
                    sub_sssloan = 0m;
                    sub_pagibigregular = 0m;
                    sub_pagibigcalamity = 0m;
                    sub_leave = 0m;
                    sub_withtax = 0m;
                    sub_netpay = 0m;
                    dgvPayroll2.Rows.Add(0, department.name);
                    dgvPayroll2[1, dgvPayroll2.Rows.Count - 1].Style.Font = new Font("Arial Rounded MT Bold", 8F);
                    deptID = department.id;
                }
                sub_monthlyrate += position.salary;
                sub_sss += sssRate;
                sub_philhealth += philhealthRate;
                sub_pagibig += pagibig;
                sub_sssloan += sss_loan;
                sub_pagibigregular += pagibig_regular;
                sub_pagibigcalamity += pagibig_calamity;
                sub_leave += leave_deduction;
                sub_withtax += tax;
                sub_netpay += netpay;

                ps2.MonthlyRate = position.salary;
                ps2.SSS = sssRate;
                ps2.PhilHealth = philhealthRate;
                ps2.PagIbig = pagibig;
                ps2.Leave = leave_deduction;
                ps2.Tax = tax;
                ps2.Save();
                dgvPayroll2.Rows.Add(ps2.ID, employee.GetFullName(), position.name, position.salary.ToString("N"), halfrate.ToString("N"), sssRate.ToString("N"), philhealthRate.ToString("N"), pagibig.ToString("N"), sss_loan.ToString("N"), pagibig_regular.ToString("N"), pagibig_calamity.ToString("N"), leave_deduction.ToString("N"), tax.ToString("N"), netpay.ToString("N"));
                dgvPayroll2.Columns[3].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgvPayroll2.Columns[4].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgvPayroll2.Columns[5].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgvPayroll2.Columns[6].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgvPayroll2.Columns[7].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgvPayroll2.Columns[8].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgvPayroll2.Columns[9].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgvPayroll2.Columns[10].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgvPayroll2.Columns[11].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgvPayroll2.Columns[12].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgvPayroll2.Columns[13].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            }
            dgvPayroll2.Rows.Add(0, "", "SUBTOTAL", sub_monthlyrate.ToString("N"), (sub_monthlyrate / 2).ToString("N"), sub_sss.ToString("N"), sub_philhealth.ToString("N"), sub_pagibig.ToString("N"), sub_sssloan.ToString("N"), sub_pagibigregular.ToString("N"), sub_pagibigcalamity.ToString("N"), sub_leave.ToString("N"), sub_withtax.ToString("N"), sub_netpay.ToString("N"));
            dgvPayroll2.Rows[dgvPayroll2.Rows.Count - 1].DefaultCellStyle.Font = new Font("Arial Rounded MT Bold", 8F);
            dgvPayroll2.Rows.Add(0, "GRAND TOTAL", "", total_monthlyrate.ToString("N"), (total_monthlyrate / 2).ToString("N"), total_sss.ToString("N"), total_philhealth.ToString("N"), total_pagibig.ToString("N"), total_sssloan.ToString("N"), total_pagibigregular.ToString("N"), total_pagibigcalamity.ToString("N"), total_leave.ToString("N"), total_withtax.ToString("N"), total_netpay.ToString("N"));
            dgvPayroll2.Rows[dgvPayroll2.Rows.Count - 1].DefaultCellStyle.Font = new Font("Arial Rounded MT Bold", 8F);
            dgvPayroll2.ClearSelection();
        }

        public void NewSet()
        {
            List<Classes.Employee> employees = Classes.Employee.getAll();
            foreach (Classes.Employee employee in employees)
            {
                Classes.PayrollSummary2 ps2 = new Classes.PayrollSummary2();
                ps2.EmployeeID = employee.id;
                ps2.Date = date;
                ps2.Save();
            }
            showData();
        }
        private void PayrollSummary2_Load(object sender, EventArgs e)
        {
            sss = Classes.Sss.getAllSortByCredit();
            philhealths = Classes.PhilHealth.getAllandSort();

            lblSummaryDate.Text = "Payroll Summary for " + date.ToString("MMMM") + " 16-" + DateTime.DaysInMonth(date.Year, date.Month) + ", " + date.ToString("yyyy");
            List<Classes.Employee> employees = Classes.Employee.getAll();

            Classes.Employee employee = new Classes.Employee();

            employee = employees.Find(em => em.position_id == 1);
            lblAccountingAssistant.Text = employee.GetFullName();

            employee = employees.Find(em => em.position_id == 7);
            lblCenterAdministrator.Text = employee.GetFullName();

            ps2s = Classes.PayrollSummary2.getByDate(date);
            foreach (Classes.PayrollSummary2 ps2 in ps2s)
            {
                ps2.Delete();
            }
            NewSet();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
            Program.main.Show();
        }

        Bitmap image;

        private void btnPayslip_Click(object sender, EventArgs e)
        {
            images.Clear();
            foreach (DataGridViewRow row in dgvPayroll2.Rows)
            {
                if (Convert.ToInt32(row.Cells[0].Value) > 0)
                {
                    Print.Payslip2nd payslip = new Print.Payslip2nd();
                    int id = Convert.ToInt32(row.Cells[0].Value);
                    Classes.PayrollSummary2 ps2 = ps2s.Find(ps => ps.ID == id);
                    payslip.lblEmployeeCode.Text = Classes.Employee.getById(ps2.EmployeeID).code;
                    payslip.lblPayrollEnding.Text = date.ToString("M/") + DateTime.DaysInMonth(date.Year, date.Month) + date.ToString("/yyyy");
                    payslip.lblEmployeeName.Text = row.Cells[1].Value.ToString();
                    payslip.lblPosition.Text = row.Cells[2].Value.ToString();
                    decimal gross = ps2.MonthlyRate / 2;
                    decimal allowance = Classes.Allowance.getByEmployeeId(ps2.EmployeeID).amount;
                    decimal deductions = ps2.SSS + ps2.PhilHealth + ps2.PagIbig + ps2.Leave + ps2.Tax;
                    payslip.lblGrossPay.Text = row.Cells[4].Value.ToString();
                    payslip.lblAllowance.Text = allowance.ToString("N");
                    payslip.lblTotal.Text = (gross + allowance).ToString("N");
                    payslip.lblSSS.Text = row.Cells[5].Value.ToString();
                    payslip.lblPhilhealth.Text = row.Cells[6].Value.ToString();
                    payslip.lblPagibig.Text = row.Cells[7].Value.ToString();
                    payslip.lblLeave.Text = row.Cells[9].Value.ToString();
                    payslip.lblTax.Text = row.Cells[10].Value.ToString();
                    payslip.lblNetPay.Text = ((gross + allowance) - deductions).ToString("N");

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
                    }
                    else
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
            dgvPayroll2.ClearSelection();
            image = new Bitmap(pnlPrint.Width, pnlPrint.Height);
            pnlPrint2.Visible = true;
            pnlPrint.DrawToBitmap(image, new Rectangle(0, 0, pnlPrint.Width, pnlPrint.Height));
            pnlPrint2.Visible = false;
            printDocument.DefaultPageSettings.Landscape = true;
            printPreviewDialog.ShowDialog();
        }

        private void dgvPayroll2_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            int id = Convert.ToInt32(dgvPayroll2.Rows[e.RowIndex].Cells[0].Value);
            if (id > 0)
            {
                Classes.PayrollSummary2 ps2 = ps2s.Find(p => p.ID == id);

                string column = dgvPayroll2[e.ColumnIndex, e.RowIndex].OwningColumn.Name;

                decimal sss = Convert.ToDecimal(dgvPayroll2.Rows[e.RowIndex].Cells["dgcSSS"].Value);
                decimal philhealth = Convert.ToDecimal(dgvPayroll2.Rows[e.RowIndex].Cells["dgcPhilHealth"].Value);
                decimal pagibig = Convert.ToDecimal(dgvPayroll2.Rows[e.RowIndex].Cells["dgcPagibig"].Value);
                decimal leaves = Convert.ToDecimal(dgvPayroll2.Rows[e.RowIndex].Cells["dgcLeaves"].Value);
                decimal withtax = Convert.ToDecimal(dgvPayroll2.Rows[e.RowIndex].Cells["dgcWithTax"].Value);

                if (column == dgcSSS.Name)
                {
                    sss = Convert.ToDecimal(dgvPayroll2.Rows[e.RowIndex].Cells["dgcSSS"].Value);
                }
                if (column == dgcPhilHealth.Name)
                {
                    philhealth = Convert.ToDecimal(dgvPayroll2.Rows[e.RowIndex].Cells["dgcPhilHealth"].Value);
                }
                if (column == dgcPagibig.Name)
                {
                    pagibig = Convert.ToDecimal(dgvPayroll2.Rows[e.RowIndex].Cells["dgcPagibig"].Value);
                }
                if (column == dgcLeaves.Name)
                {
                    leaves = Convert.ToDecimal(dgvPayroll2.Rows[e.RowIndex].Cells["dgcLeaves"].Value);
                }
                if (column == dgcWithTax.Name)
                {
                    withtax = Convert.ToDecimal(dgvPayroll2.Rows[e.RowIndex].Cells["dgcWithTax"].Value);
                }

                decimal deductions = sss + philhealth + pagibig + leaves + withtax;

                decimal netpay = ps2.MonthlyRate / 2 - deductions;
                dgvPayroll2.Rows[e.RowIndex].Cells["dgcNetPay"].Value = netpay;

                ps2.SSS = sss;
                ps2.PhilHealth = philhealth;
                ps2.PagIbig = pagibig;
                ps2.Leave = leaves;
                ps2.Tax = withtax;
            }
        }

        private void btnExportToExcel_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "Excel Documents (*.xls)|*.xls";
            sfd.FileName = "Export-PayrollSummary.xls";
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                dgvPayroll2.SelectAll();
                DataObject dataObj = dgvPayroll2.GetClipboardContent();
                if (dataObj != null)
                    Clipboard.SetDataObject(dataObj);

                object misValue = System.Reflection.Missing.Value;
                Excel.Application xlexcel = new Excel.Application();

                xlexcel.DisplayAlerts = false; // Without this you will get two confirm overwrite prompts
                Excel.Workbook xlWorkBook = xlexcel.Workbooks.Add(misValue);
                Excel.Worksheet xlWorkSheet = (Excel.Worksheet)xlWorkBook.Worksheets.get_Item(1);

                // Paste clipboard results to worksheet range
                Excel.Range CR = (Excel.Range)xlWorkSheet.Cells[1, 1];
                CR.Select();
                xlWorkSheet.PasteSpecial(CR, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, true);

                // Save the excel file under the captured location from the SaveFileDialog
                xlWorkBook.SaveAs(sfd.FileName, Excel.XlFileFormat.xlWorkbookNormal, misValue, misValue, misValue, misValue, Excel.XlSaveAsAccessMode.xlExclusive, misValue, misValue, misValue, misValue, misValue);
                xlexcel.DisplayAlerts = true;
                xlWorkBook.Close(true, misValue, misValue);
                xlexcel.Quit();

                // Clear Clipboard and DataGridView selection
                Clipboard.Clear();

                // Open the newly saved excel file
                if (File.Exists(sfd.FileName))
                    System.Diagnostics.Process.Start(sfd.FileName);
            }
            dgvPayroll2.ClearSelection();
        }
    }
}
