using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TTC_Payroll_System.Forms
{
    public partial class Departments : Form
    {
        public Departments()
        {
            InitializeComponent();
        }

        List<Classes.Department> departments = new List<Classes.Department>();
        Classes.Department department = new Classes.Department();
        //start methods
        private void showData()
        {
            departments = Classes.Department.getAll();
            dgvDepartments.Rows.Clear();
            foreach(Classes.Department department in departments)
            {
                dgvDepartments.Rows.Add(department.id, department.name);
            }
            dgvDepartments.ClearSelection();
        }

        private void Department_Load(object sender, EventArgs e)
        {

            showData();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
            Program.main.Show();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            department.name = txtDepartment.Text;
            if (department.Check() != null)
            {
                MessageBox.Show(department.Check(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            department.Save();
            showData();
        }

        private void dgvDepartments_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvDepartments.SelectedRows.Count > 0)
            {
                int id = Convert.ToInt32(dgvDepartments.SelectedRows[0].Cells[0].Value);
                department = Classes.Department.getById(id);
            }
            else
            {
                department = new Classes.Department();
            }
            txtDepartment.Text = department.name;

        }

        Bitmap bmp;

        private void btnPrint_Click(object sender, EventArgs e)
        {
            dgvDepartments.ClearSelection();
            bmp = new Bitmap(dgvDepartments.Width, dgvDepartments.Height);
            dgvDepartments.DrawToBitmap(bmp, new Rectangle(0, 0, dgvDepartments.Width, dgvDepartments.Height));
            printpreviewDialog.ShowDialog();
        }

        private void pdDepartment_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            e.Graphics.InterpolationMode = InterpolationMode.Bicubic;
            e.Graphics.SmoothingMode = SmoothingMode.HighQuality;
            e.Graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;
            e.Graphics.CompositingQuality = CompositingQuality.HighQuality;
            e.Graphics.DrawImage(bmp, 30, 50);
        }
    }
}
