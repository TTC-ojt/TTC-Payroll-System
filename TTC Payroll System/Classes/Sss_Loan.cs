using System;
using System.Collections.Generic;
using System.Linq;
using MySql.Data.MySqlClient;
using System.Text;
using System.Windows.Forms;

namespace TTC_Payroll_System.Classes
{
    class Sss_Loan
    {
        public int id { get; set; }
        public int employee_id { get; set; }
        public decimal amount { get; set; }
        public int months_to_pay { get; set; }
        public int months_paid { get; set; }
        public bool fortnightly { get; set; }

        public static List<Sss_Loan> getAll()
        {
            List<Sss_Loan> loans = new List<Sss_Loan>();

            try
            {
                using (MySqlConnection con = new MySqlConnection(Connection.Connect()))
                {
                    MySqlCommand cmd = new MySqlCommand();
                    cmd.Connection = con;
                    cmd.CommandText = "SELECT * FROM sss_loans";
                    con.Open();
                    using (MySqlDataReader rdr = cmd.ExecuteReader())
                    {
                        while(rdr.Read())
                        {
                            Sss_Loan loan = new Sss_Loan();
                            loan.id = rdr.GetInt32(0);
                            loan.employee_id = rdr.GetInt32(1);
                            loan.amount = rdr.GetDecimal(2);
                            loan.months_to_pay = rdr.GetInt32(3);
                            loan.months_paid = rdr.GetInt32(4);
                            loan.fortnightly = rdr.GetBoolean(5);
                            loans.Add(loan);
                        }
                    }
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return loans;
        }

        public static List<Sss_Loan> getLoansByEmployeeId(int id)
        {
            List<Sss_Loan> loans = new List<Sss_Loan>();

            try
            {
                using (MySqlConnection con = new MySqlConnection(Connection.Connect()))
                {
                    MySqlCommand cmd = new MySqlCommand();
                    cmd.Connection = con;
                    cmd.CommandText = "SELECT * FROM sss_loans WHERE employee_id = @employee_id";
                    cmd.Parameters.AddWithValue("employee_id", id);
                    con.Open();
                    using (MySqlDataReader rdr = cmd.ExecuteReader())
                    {
                        while (rdr.Read())
                        {
                            Sss_Loan loan = new Sss_Loan();
                            loan.id = rdr.GetInt32(0);
                            loan.employee_id = rdr.GetInt32(1);
                            loan.amount = rdr.GetDecimal(2);
                            loan.months_to_pay = rdr.GetInt32(3);
                            loan.months_paid = rdr.GetInt32(4);
                            loan.fortnightly = rdr.GetBoolean(5);
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

        public static Sss_Loan getLoanByEmployeeId(int id)
        {
            Sss_Loan loan = new Sss_Loan();

            try
            {
                using (MySqlConnection con = new MySqlConnection(Connection.Connect()))
                {
                    MySqlCommand cmd = new MySqlCommand();
                    cmd.Connection = con;
                    cmd.CommandText = "SELECT * FROM sss_loans WHERE employee_id = @employee_id";
                    cmd.Parameters.AddWithValue("employee_id", id);
                    con.Open();
                    using (MySqlDataReader rdr = cmd.ExecuteReader())
                    {
                        while (rdr.Read())
                        {
                            loan.id = rdr.GetInt32(0);
                            loan.employee_id = rdr.GetInt32(1);
                            loan.amount = rdr.GetDecimal(2);
                            loan.months_to_pay = rdr.GetInt32(3);
                            loan.months_paid = rdr.GetInt32(4);
                            loan.fortnightly = rdr.GetBoolean(5);
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
                    cmd.CommandText = "INSERT INTO sss_loans (employee_id,amount, months_to_pay, months_paid, fortnightly) VALUES (@employee_id, @amount, @months_to_pay, @months_paid, @fortnightly)";
                    cmd.Parameters.AddWithValue("employee_id", employee_id);
                    cmd.Parameters.AddWithValue("amount", amount);
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
                    cmd.CommandText = "DELETE FROM sss_loans WHERE id = @id";
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
