using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace TTC_Payroll_System.Forms
{
    public partial class ExcessLoadInput : Form
    {
        public ExcessLoadInput()
        {
            InitializeComponent();
        }

        private List<Classes.Employee> employees = new List<Classes.Employee>();
        internal Classes.Employee employee = new Classes.Employee();
        internal string load = "";
        internal decimal hours = 0m;
        internal decimal rate = 0m;

        private void ExcessLoadInput_Load(object sender, EventArgs e)
        {
            employees = Classes.Employee.getAllOrderByCode();
            cbxEmployee.Items.Clear();
            foreach (Classes.Employee employee in employees)
            {
                cbxEmployee.Items.Add(employee.code + ": " + employee.GetFullName());
            }
            cbxEmployee.SelectedIndex = -1;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (cbxEmployee.SelectedIndex > -1) employee = employees[cbxEmployee.SelectedIndex];
            load = txtLoad.Text;
            hours = nudHours.Value;
            rate = nudRate.Value;
            Close();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
