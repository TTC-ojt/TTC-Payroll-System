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
    public partial class Employees : Form
    {
        public Employees()
        {
            InitializeComponent();
        }

        List<Classes.Department> departments = new List<Classes.Department>();
        List<Classes.Position> positions = new List<Classes.Position>();
        List<Classes.Employee> employees = new List<Classes.Employee>();
        Classes.Employee employee = new Classes.Employee();
        Classes.Department department = new Classes.Department();
        Classes.Position position = new Classes.Position();

        //methods
        private void loadDepartment()
        {
            departments = Classes.Department.getAll();
            cbxDepartment.Items.Clear();
            foreach (Classes.Department department in departments)
            {
                cbxDepartment.Items.Add(department.name);
            }
            cbxDepartment.SelectedIndex = -1;
        }

        private void loadPosition()
        {
            positions = Classes.Position.getAll();
            cbxPosition.Items.Clear();
            foreach (Classes.Position position in positions)
            {
                cbxPosition.Items.Add(position.name);
            }
            cbxDepartment.SelectedIndex = -1;
        }

        private void showData()
        {
            employees = Classes.Employee.getAllOrderByCode();
            dgvEmployees.Rows.Clear();
            foreach (Classes.Employee employee in employees)
            {
                department = Classes.Department.getById(employee.department_id);
                position = Classes.Position.getById(employee.position_id);
                dgvEmployees.Rows.Add(employee.id, employee.code, employee.GetFullName(), department.name, position.name, employee.active);
            }
            dgvEmployees.ClearSelection();
        }

        private void Employees_Load(object sender, EventArgs e)
        {
            showData();
            loadDepartment();
            loadPosition();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
            Program.main.Show();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            employee.department_id = department.id;
            employee.position_id = position.id;
            employee.code = txtCode.Text;
            employee.lastname = txtLastname.Text;
            employee.firstname = txtFirstname.Text;
            employee.middlename = txtMiddlename.Text;
            employee.extname = txtExtname.Text;
            employee.active = chbActive.Checked;

            if (employee.Check() != null)
            {
                MessageBox.Show(employee.Check(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            employee.Save();
            Classes.WithholdingTax tax = new Classes.WithholdingTax();
            tax.EmployeeID = employee.id;
            tax.Save();
            showData();
        }

        private void dgvEmployees_SelectionChanged(object sender, EventArgs e)
        {
            if(dgvEmployees.SelectedRows.Count > 0)
            {
                int id = Convert.ToInt32(dgvEmployees.SelectedRows[0].Cells[0].Value);
                employee = Classes.Employee.getById(id);
            }
            else
            {
                employee = new Classes.Employee();
                cbxDepartment.SelectedIndex = -1;
                cbxPosition.SelectedIndex = -1;
            }
            department = Classes.Department.getById(employee.department_id);
            position = Classes.Position.getById(employee.position_id);
            cbxDepartment.SelectedItem = department.name;
            cbxPosition.SelectedItem = position.name;
            txtCode.Text = employee.code;
            txtLastname.Text = employee.lastname;
            txtFirstname.Text = employee.firstname;
            txtMiddlename.Text = employee.middlename;
            txtExtname.Text = employee.extname;
            chbActive.Checked = employee.active;
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            dgvEmployees.ClearSelection();
            chbActive.Checked = true;
        }

        Bitmap bmp;

        private void btnPrint_Click(object sender, EventArgs e)
        {
            bmp = new Bitmap(dgvEmployees.Width, dgvEmployees.Height);
            dgvEmployees.DrawToBitmap(bmp, new Rectangle(0, 0, dgvEmployees.Width, dgvEmployees.Height));
            printDocument.DefaultPageSettings.Landscape = true;
            printPreviewDialog.ShowDialog();
        }

        private void printDocument_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            e.Graphics.InterpolationMode = InterpolationMode.Bicubic;
            e.Graphics.SmoothingMode = SmoothingMode.HighQuality;
            e.Graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;
            e.Graphics.CompositingQuality = CompositingQuality.HighQuality;
            e.Graphics.DrawImage(bmp, 30, 50);
        }

        private void cbxDepartment_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbxDepartment.SelectedIndex > -1)
            department = Classes.Department.getByName(cbxDepartment.SelectedItem.ToString());
        }

        private void cbxPosition_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbxPosition.SelectedIndex > -1)
                position = Classes.Position.getByName(cbxPosition.SelectedItem.ToString());
        }
    }
}
