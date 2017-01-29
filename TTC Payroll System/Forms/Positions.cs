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
    public partial class Positions : Form
    {
        public Positions()
        {
            InitializeComponent();
        }

        List<Classes.Position> positions = new List<Classes.Position>();
        Classes.Position position = new Classes.Position();

        //start methods
        public void showData()
        {
            positions = Classes.Position.getAll();
            dgvPositions.Rows.Clear();
            foreach (Classes.Position position in positions)
            {
                dgvPositions.Rows.Add(position.id, position.name, position.salary);
            }
            dgvPositions.ClearSelection();
        }

        private void Position_Load(object sender, EventArgs e)
        {
            showData();
        }

        private void dgvPositions_SelectionChanged(object sender, EventArgs e)
        {
            if(dgvPositions.SelectedRows.Count > 0)
            {
                int id = Convert.ToInt32(dgvPositions.SelectedRows[0].Cells[0].Value);
                position = Classes.Position.getById(id);
            }
            else
            {
                position = new Classes.Position();
            }

            txtPosition.Text = position.name;
            nudSalary.Value = position.salary;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
            Program.main.Show();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            position.name = txtPosition.Text;
            position.salary = nudSalary.Value;
            if (position.Check() != null)
            {
                MessageBox.Show(position.Check(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            position.Save();
            showData();
        }

        Bitmap image;
        private void btnPrint_Click(object sender, EventArgs e)
        {
            image = new Bitmap(dgvPositions.Width, dgvPositions.Height);
            dgvPositions.DrawToBitmap(image, new Rectangle(0, 0, dgvPositions.Width, dgvPositions.Height));
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
        //end methods
    }
}
