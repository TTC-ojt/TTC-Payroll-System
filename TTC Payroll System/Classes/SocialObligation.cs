using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace TTC_Payroll_System.Classes
{
    class SocialObligation
    {
        public int id { get; set; }
        public int employee_id { get; set; }
        public DateTime date { get; set; }
        public string position { get; set; }
        public decimal sss_employee { get; set; }
        public decimal sss_employer { get; set; }
        public decimal sss_ec { get; set; }
        public decimal ph_employee { get; set; }
        public decimal ph_employer { get; set; }
        public decimal pi_employee { get; set; }
        public decimal pi_employer { get; set; }

        public static List<SocialObligation> getByDate(DateTime date)
        {
            List<SocialObligation> sos = new List<SocialObligation>();

            try
            {
                using (MySqlConnection con = new MySqlConnection(Connection.Connect()))
                {
                    MySqlCommand cmd = new MySqlCommand();
                    cmd.Connection = con;
                    cmd.CommandText = "SELECT * FROM social_obligations WHERE date = @date";
                    cmd.Parameters.AddWithValue("date", date);
                    con.Open();
                    using (MySqlDataReader rdr = cmd.ExecuteReader())
                    {
                        while (rdr.Read())
                        {
                            SocialObligation so = new SocialObligation();
                            so.id = rdr.GetInt32(0);
                            so.employee_id = rdr.GetInt32(1);
                            so.date = rdr.GetDateTime(2);
                            so.position = rdr.GetString(3);
                            so.sss_employee = rdr.GetDecimal(4);
                            so.sss_employer = rdr.GetDecimal(5);
                            so.sss_ec = rdr.GetDecimal(6);
                            so.ph_employee = rdr.GetDecimal(7);
                            so.ph_employer = rdr.GetDecimal(8);
                            so.pi_employee = rdr.GetDecimal(9);
                            so.pi_employer = rdr.GetDecimal(10);
                            sos.Add(so);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return sos;
        }

        public void Save()
        {
            try
            {
                using (MySqlConnection con = new MySqlConnection(Connection.Connect()))
                {
                    MySqlCommand cmd = new MySqlCommand();
                    cmd.Connection = con;
                    if (id > 0)
                    {
                        cmd.CommandText = "UPDATE social_obligations SET sss_employee = @sss_employee, sss_employer = @sss_employer, sss_ec = @sss_ec, ph_employee = @ph_employee, ph_employer = @ph_employer, pi_employee = @pi_employee, pi_employer = @pi_employer";
                        cmd.Parameters.AddWithValue("id", id);
                    }
                    else
                    {
                        cmd.CommandText = "INSERT INTO social_obligations (employee_id, `date`, position, sss_employee, sss_employer, sss_ec, ph_employee, ph_employer, pi_employee, pi_employer) VALUES (@employee_id, @date, @position, @sss_employee, @sss_employer, @sss_ec, @ph_employee, @ph_employer, @pi_employee, @pi_employer)";
                        cmd.Parameters.AddWithValue("employee_id", employee_id);
                        cmd.Parameters.AddWithValue("date", date);
                        cmd.Parameters.AddWithValue("position", position);
                    }
                    cmd.Parameters.AddWithValue("sss_employee", sss_employee);
                    cmd.Parameters.AddWithValue("sss_employer", sss_employer);
                    cmd.Parameters.AddWithValue("sss_ec", sss_ec);
                    cmd.Parameters.AddWithValue("ph_employee", ph_employee);
                    cmd.Parameters.AddWithValue("ph_employer", ph_employer);
                    cmd.Parameters.AddWithValue("pi_employee", pi_employee);
                    cmd.Parameters.AddWithValue("pi_employer", pi_employer);
                    con.Open();
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void Delete()
        {
            try
            {
                using (MySqlConnection con = new MySqlConnection(Connection.Connect()))
                {
                    MySqlCommand cmd = new MySqlCommand();
                    cmd.Connection = con;
                    cmd.CommandText = "DELETE FROM social_obligations WHERE id = @id";
                    cmd.Parameters.AddWithValue("id", id);
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
