using System;
using System.Collections.Generic;
using System.Linq;
using MySql.Data.MySqlClient;
using System.Text;
using System.Windows.Forms;

namespace TTC_Payroll_System.Classes
{
    class Sss
    {
        public int id { get; set; }
        public string salaryRange { get; set; }
        public decimal monthlySalary { get; set; }
        public decimal ssEr { get; set; }
        public decimal ssEe { get; set; }
        public decimal ssTotal { get; set; }
        public decimal ecEr { get; set; }
        public decimal tcEr { get; set; }
        public decimal tcEe { get; set; }
        public decimal tcTotal { get; set; }
        public decimal totalContribution { get; set; }

        public static MySqlDataAdapter GetDataAdapter()
        {
            MySqlDataAdapter adapter = new MySqlDataAdapter();
            try
            {
                MySqlConnection con = new MySqlConnection(Connection.Connect());
                con.Open();
                adapter = new MySqlDataAdapter("SELECT salary_range AS 'Salary Range', monthly_salary_credit 'Monthly Salary Credit', ss_er AS 'Social Security ER', ss_ee AS 'Social Security EE', ss_total AS 'Social Security Total', ec_er AS 'EC ER', tc_er AS 'Total Contribution ER', tc_ee AS 'Total Contribution EE', tc_total AS 'Total Contribution Total', total_contribution AS 'Total Contribution' FROM sss", con);
                MySqlCommandBuilder cmdBuilder = new MySqlCommandBuilder(adapter);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return adapter;
        }

        public static List<Sss> getAll()
        {

            List<Sss> sss = new List<Sss>();

            try
            {
                using (MySqlConnection con = new MySqlConnection(Connection.Connect()))
                {
                    MySqlCommand cmd = new MySqlCommand();
                    cmd.Connection = con;
                    cmd.CommandText = "SELECT * FROM sss";
                    con.Open();
                    using (MySqlDataReader rdr = cmd.ExecuteReader())
                    {
                        while (rdr.Read())
                        {
                            Sss ss = new Sss();
                            ss.id = rdr.GetInt32(0);
                            ss.salaryRange = rdr.GetString(1);
                            ss.monthlySalary = rdr.GetDecimal(2);
                            ss.ssEr = rdr.GetDecimal(3);
                            ss.ssEe = rdr.GetDecimal(4);
                            ss.ssTotal = rdr.GetDecimal(5);
                            ss.ecEr = rdr.GetDecimal(6);
                            ss.tcEr = rdr.GetDecimal(7);
                            ss.tcEe = rdr.GetDecimal(8);
                            ss.tcTotal = rdr.GetDecimal(9);
                            ss.totalContribution = rdr.GetDecimal(10);
                            sss.Add(ss);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            return sss;
        }

        public static List<Sss> getAllSortByCredit()
        {

            List<Sss> sss = new List<Sss>();

            try
            {
                using (MySqlConnection con = new MySqlConnection(Connection.Connect()))
                {
                    MySqlCommand cmd = new MySqlCommand();
                    cmd.Connection = con;
                    cmd.CommandText = "SELECT * FROM sss ORDER BY monthly_salary_credit DESC";
                    con.Open();
                    using (MySqlDataReader rdr = cmd.ExecuteReader())
                    {
                        while (rdr.Read())
                        {
                            Sss ss = new Sss();
                            ss.id = rdr.GetInt32(0);
                            ss.salaryRange = rdr.GetString(1);
                            ss.monthlySalary = rdr.GetDecimal(2);
                            ss.ssEr = rdr.GetDecimal(3);
                            ss.ssEe = rdr.GetDecimal(4);
                            ss.ssTotal = rdr.GetDecimal(5);
                            ss.ecEr = rdr.GetDecimal(6);
                            ss.tcEr = rdr.GetDecimal(7);
                            ss.tcEe = rdr.GetDecimal(8);
                            ss.tcTotal = rdr.GetDecimal(9);
                            ss.totalContribution = rdr.GetDecimal(10);
                            sss.Add(ss);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            return sss;
        }
    }
}
