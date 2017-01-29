using System;
using System.Collections.Generic;
using System.Linq;
using MySql.Data.MySqlClient;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TTC_Payroll_System.Classes
{
    class FixedDeductions
    {
        public int id { get; set; }
        public string name { get; set; }
        public decimal rate { get; set; }

        public static decimal getLeaveRate()
        {
            decimal rate = 0;
            try
            {
                using (MySqlConnection con = new MySqlConnection(Connection.Connect()))
                {
                    MySqlCommand cmd = new MySqlCommand();
                    cmd.Connection = con;
                    cmd.CommandText = "SELECT value FROM fixed_deductions WHERE name = 'leave_rate'";
                    con.Open();
                    using (MySqlDataReader rdr = cmd.ExecuteReader())
                    {
                        while (rdr.Read())
                        {
                            rate = rdr.GetDecimal(0);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            return rate;
        }

        public static decimal getPagIbig()
        {
            decimal rate = 0;
            try
            {
                using (MySqlConnection con = new MySqlConnection(Connection.Connect()))
                {
                    MySqlCommand cmd = new MySqlCommand();
                    cmd.Connection = con;
                    cmd.CommandText = "SELECT value FROM fixed_deductions WHERE name = 'pagibig'";
                    con.Open();
                    using (MySqlDataReader rdr = cmd.ExecuteReader())
                    {
                        while (rdr.Read())
                        {
                            rate = rdr.GetDecimal(0);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            return rate;
        }

        public static void SaveLeaveRate(decimal rate)
        {
            try
            {
                using (MySqlConnection con = new MySqlConnection(Connection.Connect()))
                {
                    MySqlCommand cmd = new MySqlCommand();
                    cmd.Connection = con;
                    cmd.CommandText = "UPDATE fixed_deductions SET value = @value WHERE name = 'leave_rate'";
                    cmd.Parameters.AddWithValue("value", rate);
                    con.Open();
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        public static void SavePagiBig(decimal rate)
        {
            try
            {
                using (MySqlConnection con = new MySqlConnection(Connection.Connect()))
                {
                    MySqlCommand cmd = new MySqlCommand();
                    cmd.Connection = con;
                    cmd.CommandText = "UPDATE fixed_deductions SET value = @value WHERE name = 'pagibig'";
                    cmd.Parameters.AddWithValue("value", rate);
                    con.Open();
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
