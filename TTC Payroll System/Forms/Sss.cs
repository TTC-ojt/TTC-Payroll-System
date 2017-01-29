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
    public partial class Sss : Form
    {
        public Sss()
        {
            InitializeComponent();
        }

        DataTable table = new DataTable();
        BindingSource source = new BindingSource();
        MySqlDataAdapter adapter = new MySqlDataAdapter();

        private void ShowData()
        {
            table = new DataTable();
            dgvSss.DataSource = source;
            adapter = Classes.Sss.GetDataAdapter();
            table.Locale = System.Globalization.CultureInfo.InvariantCulture;
            adapter.Fill(table);
            source.DataSource = table;
        }

        private void Sss_Load(object sender, EventArgs e)
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

        private void btnClear_Click(object sender, EventArgs e)
        {
            dgvSss.ClearSelection();
        }

        Bitmap image;

        private void btnPrint_Click(object sender, EventArgs e)
        {
            int height = dgvSss.Height;
            dgvSss.Height = dgvSss.RowCount * dgvSss.RowTemplate.Height + dgvSss.Columns[0].HeaderCell.Size.Height;
            dgvSss.ScrollBars = ScrollBars.None;
            image = new Bitmap(dgvSss.Width, dgvSss.Height);
            dgvSss.DrawToBitmap(image, new Rectangle(0, 0, dgvSss.Width, dgvSss.Height));
            dgvSss.Height = height;
            dgvSss.ScrollBars = ScrollBars.Vertical;
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
    }
}

