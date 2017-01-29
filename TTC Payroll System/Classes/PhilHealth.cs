using System;
using System.Collections.Generic;
using System.Linq;
using MySql.Data.MySqlClient;
using System.Text;
using System.Windows.Forms;
using System.Data;

namespace TTC_Payroll_System.Classes
{
    class PhilHealth
    {
        public int salary_bracket { get; set; }
        public string salary_range { get; set; }
        public decimal salary_base { get; set; }
        public decimal total_monthly_premium { get; set; }
        public decimal employee_share { get; set; }
        public decimal employer_share { get; set; }

        public static MySqlDataAdapter GetDataAdapter()
        {
            MySqlDataAdapter adapter = new MySqlDataAdapter();
            try
            {
                MySqlConnection con = new MySqlConnection(Connection.Connect());
                con.Open();
                adapter = new MySqlDataAdapter("SELECT salary_bracket AS 'Salary Bracket', salary_range AS 'Salary Range', salary_base AS 'Salary Base', total_monthly_premium AS 'Total Monthly Premium', employee_share AS 'Employee Share', employer_share AS 'Employer Share' FROM philhealth", con);
                MySqlCommandBuilder cmdBuilder = new MySqlCommandBuilder(adapter);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return adapter;
        }

        public static List<PhilHealth> getAll()
        {

            List<PhilHealth> philhealths = new List<PhilHealth>();

            try
            {
                using (MySqlConnection con = new MySqlConnection(Connection.Connect()))
                {
                    MySqlCommand cmd = new MySqlCommand();
                    cmd.Connection = con;
                    cmd.CommandText = "SELECT * FROM philhealth";
                    con.Open();
                    using (MySqlDataReader rdr = cmd.ExecuteReader())
                    {
                        while (rdr.Read())
                        {
                            PhilHealth philhealth = new PhilHealth();
                            philhealth.salary_bracket = rdr.GetInt32(0);
                            philhealth.salary_range = rdr.GetString(1);
                            philhealth.salary_base = rdr.GetDecimal(2);
                            philhealth.total_monthly_premium = rdr.GetDecimal(3);
                            philhealth.employee_share = rdr.GetDecimal(4);
                            philhealth.employer_share = rdr.GetDecimal(5);
                            philhealths.Add(philhealth);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            return philhealths;
        }

        public static List<PhilHealth> getAllandSort()
        {

            List<PhilHealth> philhealths = new List<PhilHealth>();

            try
            {
                using (MySqlConnection con = new MySqlConnection(Connection.Connect()))
                {
                    MySqlCommand cmd = new MySqlCommand();
                    cmd.Connection = con;
                    cmd.CommandText = "SELECT * FROM philhealth ORDER BY salary_base DESC";
                    con.Open();
                    using (MySqlDataReader rdr = cmd.ExecuteReader())
                    {
                        while (rdr.Read())
                        {
                            PhilHealth philhealth = new PhilHealth();
                            philhealth.salary_bracket = rdr.GetInt32(0);
                            philhealth.salary_range = rdr.GetString(1);
                            philhealth.salary_base = rdr.GetDecimal(2);
                            philhealth.total_monthly_premium = rdr.GetDecimal(3);
                            philhealth.employee_share = rdr.GetDecimal(4);
                            philhealth.employer_share = rdr.GetDecimal(5);
                            philhealths.Add(philhealth);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            return philhealths;
        }

    }
}

