using MySql.Data.MySqlClient;
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
    public partial class PhilHealth : Form
    {
        public PhilHealth()
        {
            InitializeComponent();
        }

        DataTable table = new DataTable();
        BindingSource source = new BindingSource();
        MySqlDataAdapter adapter = new MySqlDataAdapter();

        private void ShowData()
        {
            table = new DataTable();
            dgvPhilHealth.DataSource = source;
            adapter = Classes.PhilHealth.GetDataAdapter();
            table.Locale = System.Globalization.CultureInfo.InvariantCulture;
            adapter.Fill(table);
            source.DataSource = table;
        }

        private void PhilHealth_Load(object sender, EventArgs e)
        {
            ShowData();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
            Program.main.Show();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            adapter.Update((DataTable)source.DataSource);
            ShowData();
        }

        Bitmap image;

        private void btnPrint_Click(object sender, EventArgs e)
        {
            int height = dgvPhilHealth.Height;
            dgvPhilHealth.Height = dgvPhilHealth.RowCount * dgvPhilHealth.RowTemplate.Height + dgvPhilHealth.Columns[0].HeaderCell.Size.Height;
            dgvPhilHealth.ScrollBars = ScrollBars.None;
            image = new Bitmap(dgvPhilHealth.Width, dgvPhilHealth.Height);
            dgvPhilHealth.DrawToBitmap(image, new Rectangle(0, 0, dgvPhilHealth.Width, dgvPhilHealth.Height));
            dgvPhilHealth.Height = height;
            dgvPhilHealth.ScrollBars = ScrollBars.Vertical;
            printPreviewDialog.ShowDialog();
        }

        private void printDocument_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            e.Graphics.InterpolationMode = InterpolationMode.Bicubic;
            e.Graphics.SmoothingMode = SmoothingMode.HighQuality;
            e.Graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;
            e.Graphics.CompositingQuality = CompositingQuality.HighQuality;
            e.Graphics.DrawImage(image, e.PageBounds);
        }
        //end methods
    }
}
