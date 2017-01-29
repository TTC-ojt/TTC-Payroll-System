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
    public partial class Withholdingtax : Form
    {
        public Withholdingtax()
        {
            InitializeComponent();
        }

        List<Classes.WithholdingTax> taxes = new List<Classes.WithholdingTax>();
        Classes.WithholdingTax tax = new Classes.WithholdingTax();

        //methods
        public void showData()
        {
            taxes = Classes.WithholdingTax.getAll();
            dgvTaxes.Rows.Clear();
            foreach(Classes.WithholdingTax tax in taxes)
            {
                Classes.Employee employee = Classes.Employee.getById(tax.EmployeeID);
                string middle_initial = employee.middlename.Split(' ')[0].Substring(0, 1);
                string name = employee.firstname + " " + middle_initial + ". " + employee.lastname + " " + employee.extname;
                dgvTaxes.Rows.Add(tax.EmployeeID, name, tax.tax15, tax.tax30);
            }
            dgvTaxes.ClearSelection();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            tax.tax15 = nud15th.Value;
            tax.tax30 = nud30th.Value;
            tax.Save();
            showData();   
        }

        private void Withholdingtax_Load(object sender, EventArgs e)
        {
            showData();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
            Program.main.Show();
        }

        private void dgvTaxes_SelectionChanged(object sender, EventArgs e)
        {
            if(dgvTaxes.SelectedRows.Count > 0)
            {
                int id = Convert.ToInt32(dgvTaxes.SelectedRows[0].Cells[0].Value);
                tax = Classes.WithholdingTax.getByEmployeeId(id);
            }
            else
            {
                tax = new Classes.WithholdingTax();
            }
            Classes.Employee employee = Classes.Employee.getById(tax.EmployeeID);
            txtCode.Text = employee.code;
            //string middle_initial = employee.middlename.Split(' ')[0].Substring(0, 1);
            string name = employee.firstname + " " + employee.lastname + " " + employee.extname;
            txtEmployeeName.Text = name;
            nud15th.Value = tax.tax15;
            nud30th.Value = tax.tax30;
        }
        Bitmap image;
        private void btnPrint_Click(object sender, EventArgs e)
        {
            image = new Bitmap(dgvTaxes.Width, dgvTaxes.Height);
            dgvTaxes.DrawToBitmap(image, new Rectangle(0, 0, dgvTaxes.Width, dgvTaxes.Height));
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
        //methods
    }
}
