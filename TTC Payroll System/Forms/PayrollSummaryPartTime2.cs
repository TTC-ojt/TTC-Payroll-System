using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using Excel = Microsoft.Office.Interop.Excel;

namespace TTC_Payroll_System.Forms
{
    public partial class PayrollSummaryPartTime2 : Form
    {
        public PayrollSummaryPartTime2()
        {
            InitializeComponent();
        }

        DateTime date = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 16);
        List<Classes.PayrollSummary2> ps2s = new List<Classes.PayrollSummary2>();

        //methods
        public void showData()
        {
            decimal total_monthlyrate = 0m;
            decimal total_withtax = 0m;
            decimal total_netpay = 0m;
            decimal sub_monthlyrate = 0m;
            decimal sub_withtax = 0m;
            decimal sub_netpay = 0m;
            ps2s = Classes.PayrollSummary2.getByDate(date);
            dgvPayroll2.Rows.Clear();
            Classes.Department department = new Classes.Department();
            int deptID = 0;
            foreach (Classes.PayrollSummary2 ps1 in ps2s)
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
                        dgvPayroll2.Rows.Add(0, "", "SUBTOTAL", sub_monthlyrate.ToString("N"), (sub_monthlyrate / 2).ToString("N"), sub_withtax.ToString("N"), sub_netpay.ToString("N"));
                        dgvPayroll2.Rows[dgvPayroll2.Rows.Count - 1].DefaultCellStyle.Font = new Font("Arial Rounded MT Bold", 8F);
                    }
                    sub_monthlyrate = 0m;
                    sub_withtax = 0m;
                    sub_netpay = 0m;
                    dgvPayroll2.Rows.Add(0, department.name);
                    dgvPayroll2[1, dgvPayroll2.Rows.Count - 1].Style.Font = new Font("Arial Rounded MT Bold", 8F);
                    deptID = department.id;
                }
                sub_monthlyrate += position.salary;
                sub_withtax += tax;
                sub_netpay += netpay;

                ps1.MonthlyRate = position.salary;
                ps1.Tax = tax;
                ps1.Save();
                dgvPayroll2.Rows.Add(ps1.ID, employee.GetFullName(), position.name, position.salary.ToString("N"), halfrate.ToString("N"), tax.ToString("N"), netpay.ToString("N"));
                dgvPayroll2.Columns[3].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgvPayroll2.Columns[4].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgvPayroll2.Columns[5].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgvPayroll2.Columns[6].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            }
            dgvPayroll2.Rows.Add(0, "", "SUBTOTAL", sub_monthlyrate.ToString("N"), (sub_monthlyrate / 2).ToString("N"), sub_withtax.ToString("N"), sub_netpay.ToString("N"));
            dgvPayroll2.Rows[dgvPayroll2.Rows.Count - 1].DefaultCellStyle.Font = new Font("Arial Rounded MT Bold", 8F);
            dgvPayroll2.Rows.Add(0, "GRAND TOTAL FOR HUMAN RESOURCES", "", total_monthlyrate.ToString("N"), (total_monthlyrate / 2).ToString("N"), total_withtax.ToString("N"), total_netpay.ToString("N"));
            dgvPayroll2.Rows[dgvPayroll2.Rows.Count - 1].DefaultCellStyle.Font = new Font("Arial Rounded MT Bold", 8F);
            dgvPayroll2.ClearSelection();
        }

        public void NewSet()
        {
            List<Classes.Employee> employees = Classes.Employee.getAll();
            foreach (Classes.Employee employee in employees)
            {
                if (employee.department_id != 5) continue;
                Classes.PayrollSummary2 ps2 = new Classes.PayrollSummary2();
                ps2.EmployeeID = employee.id;
                ps2.Date = date;
                ps2.Save();
            }
            showData();
        }
        private void PayrollSummary2_Load(object sender, EventArgs e)
        {

            lblSummaryDate.Text = "Payroll Summary for " + date.ToString("MMMM") + " 16-" + DateTime.DaysInMonth(date.Year, date.Month) + ", " + date.ToString("yyyy");
            List<Classes.Employee> employees = Classes.Employee.getAll();

            Classes.Employee employee = new Classes.Employee();

            employee = employees.Find(em => em.position_id == 1);
            lblAccountingAssistant.Text = employee.GetFullName();

            employee = employees.Find(em => em.position_id == 7);
            lblCenterAdministrator.Text = employee.GetFullName();

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
                    payslip.lblTax.Text = row.Cells[5].Value.ToString();
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

                decimal withtax = Convert.ToDecimal(dgvPayroll2.Rows[e.RowIndex].Cells["dgcWithTax"].Value);
                
                if (column == dgcWithTax.Name)
                {
                    withtax = Convert.ToDecimal(dgvPayroll2.Rows[e.RowIndex].Cells["dgcWithTax"].Value);
                }

                decimal deductions = withtax;

                decimal netpay = ps2.MonthlyRate / 2 - deductions;
                dgvPayroll2.Rows[e.RowIndex].Cells["dgcNetPay"].Value = netpay;
                
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
