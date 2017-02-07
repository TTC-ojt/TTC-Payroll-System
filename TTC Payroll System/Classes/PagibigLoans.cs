using System;
using System.Collections.Generic;
using System.Linq;
using MySql.Data.MySqlClient;
using System.Text;
using System.Windows.Forms;

namespace TTC_Payroll_System.Classes
{
    class PagibigLoans
    {
        public int id { get; set; }
        public int employee_id { get; set; }
        public decimal regular { get; set; }
        public decimal calamity { get; set; }
        public int months_to_pay { get; set;}
        public int months_paid { get; set; }
        public bool fortnightly { get; set; }

        public static List<PagibigLoans> getAll()
        {
            List<PagibigLoans> loans = new List<PagibigLoans>();

            try
            {
                using (MySqlConnection con = new MySqlConnection(Connection.Connect()))
                {
                    MySqlCommand cmd = new MySqlCommand();
                    cmd.Connection = con;
                    cmd.CommandText = "SELECT * FROM pagibig_loans";
                    con.Open();
                    using (MySqlDataReader rdr = cmd.ExecuteReader())
                    {
                        while (rdr.Read())
                        {
                            PagibigLoans loan = new PagibigLoans();
                            loan.id = rdr.GetInt32(0);
                            loan.employee_id = rdr.GetInt32(1);
                            loan.regular = rdr.GetDecimal(2);
                            loan.calamity = rdr.GetDecimal(3);
                            loan.months_to_pay = rdr.GetInt32(4);
                            loan.months_paid = rdr.GetInt32(5);
                            loan.fortnightly = rdr.GetBoolean(6);
                            loans.Add(loan);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return loans;
        }


        public static List<PagibigLoans> getAllEmployeeID(int id)
        {
            List<PagibigLoans> loans = new List<PagibigLoans>();

            try
            {
                using (MySqlConnection con = new MySqlConnection(Connection.Connect()))
                {
                    MySqlCommand cmd = new MySqlCommand();
                    cmd.Connection = con;
                    cmd.CommandText = "SELECT * FROM pagibig_loans WHERE employee_id = @employee_id";
                    cmd.Parameters.AddWithValue("employee_id", id);
                    con.Open();
                    using (MySqlDataReader rdr = cmd.ExecuteReader())
                    {
                        while (rdr.Read())
                        {
                            PagibigLoans loan = new PagibigLoans();
                            loan.id = rdr.GetInt32(0);
                            loan.employee_id = rdr.GetInt32(1);
                            loan.regular = rdr.GetDecimal(2);
                            loan.calamity = rdr.GetDecimal(3);
                            loan.months_to_pay = rdr.GetInt32(4);
                            loan.months_paid = rdr.GetInt32(5);
                            loan.fortnightly = rdr.GetBoolean(6);
                            loans.Add(loan);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return loans;
        }

        public static PagibigLoans getEmployeeID(int id)
        {
            PagibigLoans loan = new PagibigLoans();

            try
            {
                using (MySqlConnection con = new MySqlConnection(Connection.Connect()))
                {
                    MySqlCommand cmd = new MySqlCommand();
                    cmd.Connection = con;
                    cmd.CommandText = "SELECT * FROM pagibig_loans WHERE employee_id = @employee_id";
                    cmd.Parameters.AddWithValue("employee_id", id);
                    con.Open();
                    using (MySqlDataReader rdr = cmd.ExecuteReader())
                    {
                        while (rdr.Read())
                        {
                            loan.id = rdr.GetInt32(0);
                            loan.employee_id = rdr.GetInt32(1);
                            loan.regular = rdr.GetDecimal(2);
                            loan.calamity = rdr.GetDecimal(3);
                            loan.months_to_pay = rdr.GetInt32(4);
                            loan.months_paid = rdr.GetInt32(5);
                            loan.fortnightly = rdr.GetBoolean(6);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return loan;
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
                        cmd.CommandText = "UPDATE pagibig_loans SET regular = @regular, calamity = @calamity, months_to_pay = @months_to_pay, months_paid = @months_paid, fortnightly = @fortnightly";
                    }
                    else
                    {
                        cmd.CommandText = "INSERT INTO pagibig_loans (employee_id,regular, calamity, months_to_pay, months_paid, fortnightly) VALUES (@employee_id, @regular, @calamity, @months_to_pay, @months_paid, @fortnightly)";
                        cmd.Parameters.AddWithValue("employee_id", employee_id);
                    }
                    cmd.Parameters.AddWithValue("regular", regular);
                    cmd.Parameters.AddWithValue("calamity", calamity);
                    cmd.Parameters.AddWithValue("months_to_pay", months_to_pay);
                    cmd.Parameters.AddWithValue("months_paid", months_paid);
                    cmd.Parameters.AddWithValue("fortnightly", fortnightly);
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
                    cmd.CommandText = "DELETE FROM pagibig_loans WHERE id = @id";
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
