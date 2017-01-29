using System;
using System.Collections.Generic;
using System.Linq;
using MySql.Data.MySqlClient;
using System.Text;

namespace TTC_Payroll_System.Classes
{
    class Leave
    {
        public int id { get; set; }
        public int employee_id { get; set; }
        public DateTime leave_date { get; set; }
        public bool HalfDay { get; set; }

        public static List<Leave> getAll()
        {
            List<Leave> leaves = new List<Leave>();

            try
            {
                using (MySqlConnection con = new MySqlConnection(Connection.Connect()))
                {
                    MySqlCommand cmd = new MySqlCommand();
                    cmd.Connection = con;
                    cmd.CommandText = "SELECT * FROM leaves ORDER BY date DESC";
                    con.Open();
                    using (MySqlDataReader rdr = cmd.ExecuteReader())
                    {
                        while (rdr.Read())
                        {
                            Leave leave = new Leave();
                            leave.id = rdr.GetInt32(0);
                            leave.employee_id = rdr.GetInt32(1);
                            leave.leave_date = rdr.GetDateTime(2);
                            leave.HalfDay = rdr.GetBoolean(3);
                            leaves.Add(leave);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return leaves;
        }

        public static List<Leave> getLeavesByEmployeeID(int EmployeeID)
        {
            List<Leave> leaves = new List<Leave>();

            try
            {
                using (MySqlConnection con = new MySqlConnection(Connection.Connect()))
                {
                    MySqlCommand cmd = new MySqlCommand();
                    cmd.Connection = con;
                    cmd.CommandText = "SELECT * FROM leaves WHERE employee_id = @employee_id ORDER BY date DESC";
                    cmd.Parameters.AddWithValue("employee_id", EmployeeID);
                    con.Open();
                    using (MySqlDataReader rdr = cmd.ExecuteReader())
                    {
                        while (rdr.Read())
                        {
                            Leave leave = new Leave();
                            leave.id = rdr.GetInt32(0);
                            leave.employee_id = rdr.GetInt32(1);
                            leave.leave_date = rdr.GetDateTime(2);
                            leave.HalfDay = rdr.GetBoolean(3);
                            leaves.Add(leave);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return leaves;
        }

        public static List<Leave> getLeavesByEmployeeIDAndDate(int EmployeeID, string startDate, string endDate)
        {
            List<Leave> leaves = new List<Leave>();

            try
            {
                using (MySqlConnection con = new MySqlConnection(Connection.Connect()))
                {
                    MySqlCommand cmd = new MySqlCommand();
                    cmd.Connection = con;
                    cmd.CommandText = "SELECT * FROM leaves WHERE employee_id = @employee_id AND date >= DATE(@startdate) AND date <= DATE(@enddate)";
                    cmd.Parameters.AddWithValue("employee_id", EmployeeID);
                    cmd.Parameters.AddWithValue("startdate", startDate);
                    cmd.Parameters.AddWithValue("enddate", endDate);
                    con.Open();
                    using (MySqlDataReader rdr = cmd.ExecuteReader())
                    {
                        while (rdr.Read())
                        {
                            Leave leave = new Leave();
                            leave.id = rdr.GetInt32(0);
                            leave.employee_id = rdr.GetInt32(1);
                            leave.leave_date = rdr.GetDateTime(2);
                            leave.HalfDay = rdr.GetBoolean(3);
                            leaves.Add(leave);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return leaves;
        }

        public void Save()
        {
            try
            {
                using (MySqlConnection con = new MySqlConnection(Connection.Connect()))
                {
                    MySqlCommand cmd = new MySqlCommand();
                    cmd.Connection = con;
                    cmd.CommandText = "INSERT INTO leaves (employee_id,`date`, half_day) VALUES (@employee_id, @date, @half_day)";
                    cmd.Parameters.AddWithValue("employee_id", employee_id);
                    cmd.Parameters.AddWithValue("date", leave_date);
                    cmd.Parameters.AddWithValue("half_day", HalfDay);
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
                    cmd.CommandText = "DELETE FROM leaves WHERE id = @id";
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
