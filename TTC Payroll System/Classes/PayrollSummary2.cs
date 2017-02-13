using System;
using System.Collections.Generic;
using System.Linq;
using MySql.Data.MySqlClient;
using System.Text;
using System.Windows.Forms;

namespace TTC_Payroll_System.Classes
{
    class PayrollSummary2
    {
        public int ID { get; set; }
        public int EmployeeID { get; set; }
        public DateTime Date { get; set; }
        public decimal MonthlyRate { get; set; }
        public decimal SSS { get; set; }
        public decimal PhilHealth { get; set; }
        public decimal PagIbig { get; set; }
        public decimal UTD { get; set; }
        public decimal Leave { get; set; }
        public decimal Tax { get; set; }
        public decimal SssLoan { get; set; }
        public decimal PagibigRegular { get; set; }
        public decimal PagibigCalamity { get; set; }

        public static List<PayrollSummary2> getByDate(DateTime date)
        {
            List<PayrollSummary2> ps2s = new List<PayrollSummary2>();

            try
            {
                using (MySqlConnection con = new MySqlConnection(Connection.Connect()))
                {
                    MySqlCommand cmd = new MySqlCommand();
                    cmd.Connection = con;
                    cmd.CommandText = "SELECT * FROM payroll30 WHERE date = @date";
                    cmd.Parameters.AddWithValue("date", date);
                    con.Open();
                    using (MySqlDataReader rdr = cmd.ExecuteReader())
                    {
                        while(rdr.Read())
                        {
                            PayrollSummary2 ps2 = new PayrollSummary2();
                            ps2.ID = rdr.GetInt32(0);
                            ps2.EmployeeID = rdr.GetInt32(1);
                            ps2.Date = rdr.GetDateTime(2);
                            ps2.MonthlyRate = rdr.GetDecimal(3);
                            ps2.SSS = rdr.GetDecimal(4);
                            ps2.PhilHealth = rdr.GetDecimal(5);
                            ps2.PagIbig = rdr.GetDecimal(6);
                            ps2.UTD = rdr.GetDecimal(7);
                            ps2.Leave = rdr.GetDecimal(8);
                            ps2.Tax = rdr.GetDecimal(9);
                            ps2.SssLoan = rdr.GetDecimal(10);
                            ps2.PagibigRegular = rdr.GetDecimal(11);
                            ps2.PagibigCalamity = rdr.GetDecimal(12);
                            ps2s.Add(ps2);
                        }
                    }
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            return ps2s;
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
                        cmd.CommandText = "UPDATE payroll30 SET monthly_rate = @monthly_rate, sss = @sss, philhealth = @philhealth, pagibig = @pagibig, utd = @utd, `leave` = @leave, tax = @tax";
                        cmd.Parameters.AddWithValue("id", ID);
                    }
                    else
                    {
                        cmd.CommandText = "INSERT INTO payroll30 (employee_id, `date`, monthly_rate, sss, philhealth, pagibig, utd, `leave`, tax, sss_loan, pagibig_regular, pagibig_calamity) VALUES (@employee_id, @date, @monthly_rate, @sss, @philhealth, @pagibig, @utd, @leave, @tax, @sss_loan, @pagibig_regular, @pagibig_calamity)";
                        cmd.Parameters.AddWithValue("employee_id", EmployeeID);
                        cmd.Parameters.AddWithValue("date", Date);
                    }
                    cmd.Parameters.AddWithValue("monthly_rate", MonthlyRate);
                    cmd.Parameters.AddWithValue("sss", SSS);
                    cmd.Parameters.AddWithValue("philhealth", PhilHealth);
                    cmd.Parameters.AddWithValue("pagibig", PagIbig);
                    cmd.Parameters.AddWithValue("utd", UTD);
                    cmd.Parameters.AddWithValue("leave", Leave);
                    cmd.Parameters.AddWithValue("tax", Tax);
                    cmd.Parameters.AddWithValue("sss_loan", SssLoan);
                    cmd.Parameters.AddWithValue("pagibig_regular", PagibigRegular);
                    cmd.Parameters.AddWithValue("pagibig_calamity", PagibigCalamity);
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
