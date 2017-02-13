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
    public partial class SocialObligation : Form
    {
        public SocialObligation()
        {
            InitializeComponent();
        }

        DateTime date = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1);
        List<Classes.SocialObligation> sos = new List<Classes.SocialObligation>();
        List<Classes.Sss> sss = new List<Classes.Sss>();
        List<Classes.PhilHealth> philhealths = new List<Classes.PhilHealth>();
        decimal pagibig = Classes.FixedDeductions.getPagIbig();

        private void showData()
        {
            decimal total = 0m;
            decimal total_sss = 0m;
            decimal total_sss_ee = 0m;
            decimal total_sss_er = 0m;
            decimal total_sss_ec = 0m;
            decimal total_ph = 0m;
            decimal total_ph_ee = 0m;
            decimal total_ph_er = 0m;
            decimal total_pi = 0m;
            decimal total_pi_ee = 0m;
            decimal total_pi_er = 0m;
            sos = Classes.SocialObligation.getByDate(date);
            if (sos.Count == 0)
            {
                NewSet();
                showData();
                return;
            }
            dgvSocialObligation.Rows.Clear();
            foreach (Classes.SocialObligation so in sos)
            {
                Classes.Employee employee = Classes.Employee.getById(so.employee_id);
                if (employee.department_id == 5) continue;
                Classes.Position position = Classes.Position.getById(employee.position_id);
                foreach (Classes.Sss ss in sss)
                {
                    if (ss.monthlySalary <= position.salary)
                    {
                        so.sss_employee = ss.ssEe;
                        so.sss_employer = ss.ssEr;
                        so.sss_ec = ss.ecEr;
                        break;
                    }
                }
                foreach (Classes.PhilHealth philhealth in philhealths)
                {
                    if (philhealth.salary_base <= position.salary)
                    {
                        so.ph_employee = philhealth.employee_share;
                        so.ph_employer = philhealth.employer_share;
                        break;
                    }
                }
                so.pi_employee = pagibig;
                so.pi_employer = pagibig;

                total_sss_ee += so.sss_employee;
                total_sss_er += so.sss_employer;
                total_sss_ec += so.sss_ec;
                total_sss = so.sss_employee + so.sss_employer + so.sss_ec;

                total_ph_ee += so.ph_employee;
                total_ph_er += so.ph_employer;
                total_ph = so.ph_employee + so.ph_employer;

                total_pi_ee += so.pi_employee;
                total_pi_er += so.pi_employer;
                total_pi = so.pi_employee + so.pi_employer;

                total += total_sss + total_ph + total_pi;
                dgvSocialObligation.Rows.Add(so.id, employee.GetFullName(), position.name, so.sss_employee, so.sss_employer, so.sss_ec, total_sss, so.ph_employee, so.ph_employer, total_ph, so.pi_employee, so.pi_employer, total_pi);
            }
            dgvSocialObligation.Rows.Add(0, "", "", total_sss_ee.ToString("N"), total_sss_er.ToString("N"), total_sss_ec.ToString("N"), (total_sss_ee+total_sss_er+total_sss_ec).ToString("N"), total_ph_ee.ToString("N"), total_ph_er.ToString("N"), (total_ph_ee+total_ph_er).ToString("N"), total_pi_ee.ToString("N"), total_pi_er.ToString("N"), (total_pi_ee + total_pi_er).ToString("N"));
            dgvSocialObligation.Rows.Add(0, "Grand Total", "", "", "", "", "", "", "", "", "", "", total.ToString("N"));
            dgvSocialObligation.ClearSelection();
        }

        private void NewSet()
        {
            List<Classes.Employee> employees = Classes.Employee.getAll();
            foreach (Classes.Employee employee in employees)
            {
                Classes.SocialObligation so = new Classes.SocialObligation();
                so.employee_id = employee.id;
                so.date = date;
                so.position = Classes.Position.getById(employee.position_id).name;
                so.Save();
            }
        }

        private void SocialObligation_Load(object sender, EventArgs e)
        {
            lblSummaryDate.Text = "Social Obligations for " + date.ToString("MMMM") + ", " + date.ToString("yyyy");
            List<Classes.Employee> employees = Classes.Employee.getAll();

            Classes.Employee employee = new Classes.Employee();

            employee = employees.Find(em => em.position_id == 1);
            lblAccountingAssistant.Text = employee.GetFullName();

            employee = employees.Find(em => em.position_id == 7);
            lblCenterAdministrator.Text = employee.GetFullName();
            
            sss = Classes.Sss.getAllSortByCredit();
            philhealths = Classes.PhilHealth.getAllandSort();
            showData();
        }

        Bitmap image;
        private void btnPrint_Click_1(object sender, EventArgs e)
        {
            btnClose.Hide();
            btnPrint.Hide();
            btnExportToExcel.Hide();
            pnlPrint2.Show();

            dgvSocialObligation.ClearSelection();
            image = new Bitmap(pnlPrint.Width, pnlPrint.Height);
            pnlPrint.DrawToBitmap(image, new Rectangle(0, 0, pnlPrint.Width, pnlPrint.Height));
            printDocument.DefaultPageSettings.Landscape = true;

            btnClose.Show();
            btnPrint.Show();
            btnExportToExcel.Show();
            pnlPrint2.Hide();
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

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
            Program.main.Show();
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            dgvSocialObligation.ClearSelection();
        }

        private void btnExportToExcel_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "Excel Documents (*.xls)|*.xls";
            sfd.FileName = "Export-PayrollSummary.xls";
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                dgvSocialObligation.SelectAll();
                DataObject dataObj = dgvSocialObligation.GetClipboardContent();
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
            dgvSocialObligation.ClearSelection();
        }
    }
}
