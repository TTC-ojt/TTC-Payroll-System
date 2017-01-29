using System;
using System.Collections.Generic;
using System.Linq;
using MySql.Data.MySqlClient;
using System.Text;

namespace TTC_Payroll_System.Classes
{
    class Late
    {
        public int id { get; set; }
        public int employee_id { get; set; }
        public DateTime leave_date { get; set; }
        public int hours { get; set; }
        public decimal penalty { get; set; }

        public static List<Late> getAll()
        {
            List<Late> lates = new List<Late>();

            try
            {
                using (MySqlConnection con = new MySqlConnection(Connection.Connect()))
                {
                    MySqlCommand cmd = new MySqlCommand();
                    cmd.Connection = con;
                    cmd.CommandText = "SELECT * FROM lates ORDER BY date DESC";
                    con.Open();
                    using (MySqlDataReader rdr = cmd.ExecuteReader())
                    {
                        while (rdr.Read())
                        {
                            Late late = new Late();
                            late.id = rdr.GetInt32(0);
                            late.employee_id = rdr.GetInt32(1);
                            late.leave_date = rdr.GetDateTime(2);
                            late.hours = rdr.GetInt32(3);
                            late.penalty = rdr.GetDecimal(4);
                            lates.Add(late);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return lates;
        }

        public static List<Late> getLeavesByEmployeeID(int EmployeeID)
        {
            List<Late> lates = new List<Late>();

            try
            {
                using (MySqlConnection con = new MySqlConnection(Connection.Connect()))
                {
                    MySqlCommand cmd = new MySqlCommand();
                    cmd.Connection = con;
                    cmd.CommandText = "SELECT * FROM lates WHERE employee_id = @employee_id ORDER BY date DESC";
                    cmd.Parameters.AddWithValue("employee_id", EmployeeID);
                    con.Open();
                    using (MySqlDataReader rdr = cmd.ExecuteReader())
                    {
                        while (rdr.Read())
                        {
                            Late late = new Late();
                            late.id = rdr.GetInt32(0);
                            late.employee_id = rdr.GetInt32(1);
                            late.leave_date = rdr.GetDateTime(2);
                            late.hours = rdr.GetInt32(3);
                            late.penalty = rdr.GetDecimal(4);
                            lates.Add(late);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return lates;
        }

        public static List<Late> getLeavesByEmployeeIDAndDate(int EmployeeID, string startDate, string endDate)
        {
            List<Late> lates = new List<Late>();

            try
            {
                using (MySqlConnection con = new MySqlConnection(Connection.Connect()))
                {
                    MySqlCommand cmd = new MySqlCommand();
                    cmd.Connection = con;
                    cmd.CommandText = "SELECT * FROM lates WHERE employee_id = @employee_id AND date >= DATE(@startdate) AND date <= DATE(@enddate)";
                    cmd.Parameters.AddWithValue("employee_id", EmployeeID);
                    cmd.Parameters.AddWithValue("startdate", startDate);
                    cmd.Parameters.AddWithValue("enddate", endDate);
                    con.Open();
                    using (MySqlDataReader rdr = cmd.ExecuteReader())
                    {
                        while (rdr.Read())
                        {
                            Late late = new Late();
                            late.id = rdr.GetInt32(0);
                            late.employee_id = rdr.GetInt32(1);
                            late.leave_date = rdr.GetDateTime(2);
                            late.hours = rdr.GetInt32(3);
                            lates.Add(late);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return lates;
        }

        public void Save()
        {
            try
            {
                using (MySqlConnection con = new MySqlConnection(Connection.Connect()))
                {
                    MySqlCommand cmd = new MySqlCommand();
                    cmd.Connection = con;
                    cmd.CommandText = "INSERT INTO lates (employee_id,`date`, hours, penalty) VALUES (@employee_id, @date, @hours, @penalty)";
                    cmd.Parameters.AddWithValue("employee_id", employee_id);
                    cmd.Parameters.AddWithValue("date", leave_date);
                    cmd.Parameters.AddWithValue("hours", hours);
                    cmd.Parameters.AddWithValue("penalty", penalty);
                    con.Open();
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
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
                    cmd.CommandText = "DELETE FROM lates WHERE id = @id";
                    cmd.Parameters.AddWithValue("id", id);
                    con.Open();
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
