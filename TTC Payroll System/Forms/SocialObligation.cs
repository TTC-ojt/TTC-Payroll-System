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
                decimal total_sss = so.sss_employee + so.sss_employer + so.sss_ec;
                decimal total_ph = so.ph_employee + so.ph_employer;
                decimal total_pi = so.pi_employee + so.pi_employer;
                dgvSocialObligation.Rows.Add(so.id, employee.GetFullName(), position.name, so.sss_employee, so.sss_employer, so.sss_ec, total_sss, so.ph_employee, so.ph_employer, total_ph, so.pi_employee, so.pi_employer, total_pi);
            }
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
            pnlPrint2.Show();

            dgvSocialObligation.ClearSelection();
            image = new Bitmap(pnlPrint.Width, pnlPrint.Height);
            pnlPrint.DrawToBitmap(image, new Rectangle(0, 0, pnlPrint.Width, pnlPrint.Height));
            printDocument.DefaultPageSettings.Landscape = true;

            btnClose.Show();
            btnPrint.Show();
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
    }
}
