using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TTC_Payroll_System.Classes
{
    class WithholdingTax
    {

        public int ID { get; set; }
        public int EmployeeID { get; set; }
        public decimal tax15 { get; set; }
        public decimal tax30 { get; set; }

        public static List<WithholdingTax> getAll()
        {
            List<WithholdingTax> withtaxes = new List<WithholdingTax>();

            try
            {
                using (MySqlConnection con = new MySqlConnection(Connection.Connect()))
                {
                    MySqlCommand cmd = new MySqlCommand();
                    cmd.Connection = con;
                    cmd.CommandText = "SELECT * FROM withholding_tax";
                    con.Open();
                    using (MySqlDataReader rdr = cmd.ExecuteReader())
                    {
                        while (rdr.Read())
                        {
                            WithholdingTax withtax = new WithholdingTax();
                            withtax.ID = rdr.GetInt32(0);
                            withtax.EmployeeID = rdr.GetInt32(1);
                            withtax.tax15 = rdr.GetDecimal(2);
                            withtax.tax30 = rdr.GetDecimal(3);
                            withtaxes.Add(withtax);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return withtaxes;
        }

        public static WithholdingTax getByEmployeeId(int employee_id)
        {
            WithholdingTax withtax = new WithholdingTax();

            try
            {
                using (MySqlConnection con = new MySqlConnection(Connection.Connect()))
                {
                    MySqlCommand cmd = new MySqlCommand();
                    cmd.Connection = con;
                    cmd.CommandText = "SELECT * FROM withholding_tax WHERE employee_id = @employee_id";
                    cmd.Parameters.AddWithValue("employee_id", employee_id);
                    con.Open();
                    using (MySqlDataReader rdr = cmd.ExecuteReader())
                    {
                        while (rdr.Read())
                        {
                            withtax.ID = rdr.GetInt32(0);
                            withtax.EmployeeID = rdr.GetInt32(1);
                            withtax.tax15 = rdr.GetDecimal(2);
                            withtax.tax30 = rdr.GetDecimal(3);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return withtax;
        }

        public void Save()
        {
            try
            {
                using (MySqlConnection con = new MySqlConnection(Connection.Connect()))
                {
                    MySqlCommand cmd = new MySqlCommand();
                    cmd.Connection = con;
                    if (ID > 0)
                    {
                        cmd.CommandText = "UPDATE withholding_tax SET tax15 = @tax15, tax30 = @tax30 WHERE id = @id";
                        cmd.Parameters.AddWithValue("id", ID);
                    }
                    else
                    {
                        cmd.CommandText = "INSERT INTO withholding_tax (employee_id,tax15, tax30) VALUES (@employee_id, @tax15, @tax30)";
                    }
                    cmd.Parameters.AddWithValue("employee_id", EmployeeID);
                    cmd.Parameters.AddWithValue("tax15", tax15);
                    cmd.Parameters.AddWithValue("tax30", tax30);
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
