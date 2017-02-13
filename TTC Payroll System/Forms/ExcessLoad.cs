using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Windows.Forms;
using Excel = Microsoft.Office.Interop.Excel;

namespace TTC_Payroll_System.Forms
{
    public partial class ExcessLoad : Form
    {
        public ExcessLoad()
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
            dgvLoads.Rows.Add("", "Total", 0.00, "", 0.00, 0.00, 0.00);
            dgvLoads.Columns[2].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgvLoads.Columns[3].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgvLoads.Columns[4].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgvLoads.Columns[5].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgvLoads.Columns[6].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgvLoads.ClearSelection();
        }

        private void SocialObligation_Load(object sender, EventArgs e)
        {
            List<Classes.Employee> employees = Classes.Employee.getAll();

            Classes.Employee employee = new Classes.Employee();

            employee = employees.Find(em => em.position_id == 1);
            lblAccountingAssistant.Text = employee.GetFullName();

            employee = employees.Find(em => em.position_id == 7);
            lblCenterAdministrator.Text = employee.GetFullName();
            
            showData();
        }

        Bitmap image;
        private void btnPrint_Click_1(object sender, EventArgs e)
        {
            btnClose.Hide();
            btnPrint.Hide();
            btnExportToExcel.Hide();
            pnlPrint2.Show();

            dgvLoads.ClearSelection();
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

        private void btnExportToExcel_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "Excel Documents (*.xls)|*.xls";
            sfd.FileName = "Export-PayrollSummary.xls";
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                dgvLoads.SelectAll();
                DataObject dataObj = dgvLoads.GetClipboardContent();
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
            dgvLoads.ClearSelection();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            ExcessLoadInput eli = new ExcessLoadInput();

            if (eli.ShowDialog() == DialogResult.OK)
            {
                dgvLoads.Rows.Remove(dgvLoads.Rows[dgvLoads.RowCount - 1]);
                dgvLoads.Rows.Add(eli.employee.GetFullName(), eli.load, eli.hours.ToString("N"), eli.rate.ToString("N"));
                compute();
            }
        }
        private void compute()
        {
            int lastrow = dgvLoads.RowCount - 1;
            decimal hours = Convert.ToDecimal(dgvLoads.Rows[lastrow].Cells[dgcHours.Name].Value);
            decimal rate = Convert.ToDecimal(dgvLoads.Rows[lastrow].Cells[dgcRate.Name].Value);
            decimal total = hours * rate;
            dgvLoads.Rows[lastrow].Cells[dgcTotal.Name].Value = total.ToString("N");
            decimal tax = total * 0.10m;
            dgvLoads.Rows[lastrow].Cells[dgcTax.Name].Value = tax.ToString("N");
            decimal net = total - tax;
            dgvLoads.Rows[lastrow].Cells[dgcNet.Name].Value = net.ToString("N");

            decimal total_hours = 0m;
            decimal total_total = 0m;
            decimal total_tax = 0m;
            decimal total_net = 0m;
            foreach (DataGridViewRow row in dgvLoads.Rows)
            {
                total_hours += Convert.ToDecimal(row.Cells[dgcHours.Name].Value);
                total_total += Convert.ToDecimal(row.Cells[dgcTotal.Name].Value);
                total_tax += Convert.ToDecimal(row.Cells[dgcTax.Name].Value);
                total_net += Convert.ToDecimal(row.Cells[dgcNet.Name].Value);
            }
            dgvLoads.Rows.Add("", "Total", total_hours.ToString("N"), "", total_total.ToString("N"), total_tax.ToString("N"), total_net.ToString("N"));
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            dgvLoads.Rows.Clear();
            dgvLoads.Rows.Add("", "Total", 0.00, "", 0.00, 0.00, 0.00);
        }
    }
}
