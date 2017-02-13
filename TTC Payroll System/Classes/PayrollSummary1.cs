using System;
using System.Collections.Generic;
using System.Linq;
using MySql.Data.MySqlClient;
using System.Text;
using System.Windows.Forms;

namespace TTC_Payroll_System.Classes
{
    class PayrollSummary1
    {
        public int ID { get; set; }
        public int EmployeeID { get; set; }
        public DateTime Date { get; set; }
        public decimal MonthlyRate { get; set; }
        public decimal SSS_Loan { get; set; }
        public decimal Regular { get; set; }
        public decimal Calamity { get; set; }
        public decimal UTD { get; set; }
        public decimal Leave { get; set; }
        public decimal Tax { get; set; }

        public static List<PayrollSummary1> getByDate(DateTime date)
        {
            List<PayrollSummary1> ps1s = new List<PayrollSummary1>();

            try
            {
                using (MySqlConnection con = new MySqlConnection(Connection.Connect()))
                {
                    MySqlCommand cmd = new MySqlCommand();
                    cmd.Connection = con;
                    cmd.CommandText = "SELECT * FROM payroll15 WHERE date = @date";
                    cmd.Parameters.AddWithValue("date", date);
                    con.Open();
                    using (MySqlDataReader rdr = cmd.ExecuteReader())
                    {
                        while (rdr.Read())
                        {
                            PayrollSummary1 ps1 = new PayrollSummary1();
                            ps1.ID = rdr.GetInt32(0);
                            ps1.EmployeeID = rdr.GetInt32(1);
                            ps1.Date = rdr.GetDateTime(2);
                            ps1.MonthlyRate = rdr.GetDecimal(3);
                            ps1.SSS_Loan = rdr.GetDecimal(4);
                            ps1.Regular = rdr.GetDecimal(5);
                            ps1.Calamity = rdr.GetDecimal(6);
                            ps1.UTD = rdr.GetDecimal(7);
                            ps1.Leave = rdr.GetDecimal(8);
                            ps1.Tax = rdr.GetDecimal(9);
                            ps1s.Add(ps1);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            return ps1s;
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
                        cmd.CommandText = "UPDATE payroll15 SET monthly_rate = @monthly_rate, sss_loan = @sss_loan, pagibig_regular = @pagibig_regular, pagibig_calamity = @pagibig_calamity, utd = @utd, `leave` = @leave, tax = @tax";
                        cmd.Parameters.AddWithValue("id", ID);
                    }
                    else
                    {
                        cmd.CommandText = "INSERT INTO payroll15 (employee_id, `date`, monthly_rate, sss_loan, pagibig_regular, pagibig_calamity, utd, `leave`, tax) VALUES (@employee_id, @date, @monthly_rate, @sss_loan, @pagibig_regular, @pagibig_calamity, @utd, @leave, @tax)";
                        cmd.Parameters.AddWithValue("employee_id", EmployeeID);
                        cmd.Parameters.AddWithValue("date", Date);
                    }
                    cmd.Parameters.AddWithValue("monthly_rate", MonthlyRate);
                    cmd.Parameters.AddWithValue("sss_loan", SSS_Loan);
                    cmd.Parameters.AddWithValue("pagibig_regular", Regular);
                    cmd.Parameters.AddWithValue("pagibig_calamity", Calamity);
                    cmd.Parameters.AddWithValue("utd", UTD);
                    cmd.Parameters.AddWithValue("leave", Leave);
                    cmd.Parameters.AddWithValue("tax", Tax);
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
