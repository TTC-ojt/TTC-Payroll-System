using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MySql.Data.MySqlClient;
using System.Windows.Forms;

namespace TTC_Payroll_System.Classes
{
    class Allowance
    {
        public int id { get; set; }
        public int employee_id { get; set; }
        public decimal amount { get; set; }

        public static List<Allowance> getAll()
        {
            List<Allowance> allowances = new List<Allowance>();

            try
            {
                using (MySqlConnection con = new MySqlConnection(Connection.Connect()))
                {
                    MySqlCommand cmd = new MySqlCommand();
                    cmd.Connection = con;
                    cmd.CommandText = "SELECT * FROM allowances";
                    con.Open();
                    using (MySqlDataReader rdr = cmd.ExecuteReader())
                    {
                        while (rdr.Read())
                        {
                            Allowance a = new Allowance();
                            a.id = rdr.GetInt32(0);
                            a.employee_id = rdr.GetInt32(1);
                            a.amount = rdr.GetDecimal(2);
                            allowances.Add(a);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            return allowances;
        }

        public static Allowance getByEmployeeId(int employee_id)
        {
          Allowance a = new Allowance();

            try
            {
                using (MySqlConnection con = new MySqlConnection(Connection.Connect()))
                {
                    MySqlCommand cmd = new MySqlCommand();
                    cmd.Connection = con;
                    cmd.CommandText = "SELECT * FROM allowances WHERE employee_id = @employee_id";
                    cmd.Parameters.AddWithValue("employee_id", employee_id);
                    con.Open();
                    using (MySqlDataReader rdr = cmd.ExecuteReader())
                    {
                        while (rdr.Read())
                        {
                            a.id = rdr.GetInt32(0);
                            a.employee_id = rdr.GetInt32(1);
                            a.amount = rdr.GetDecimal(2);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            return a;
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
                        cmd.CommandText = "UPDATE allowances SET amount = @amount WHERE id = @id";
                        cmd.Parameters.AddWithValue("id", id);
                    }
                    else
                    {
                        cmd.CommandText = "INSERT INTO allowances (employee_id, amount) VALUES (@employee_id, @amount)";
                        cmd.Parameters.AddWithValue("employee_id", employee_id);
                    }
                    cmd.Parameters.AddWithValue("amount", amount);
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
