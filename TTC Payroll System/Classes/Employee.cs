using System;
using System.Collections.Generic;
using System.Linq;
using MySql.Data.MySqlClient;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TTC_Payroll_System.Classes
{
    class Employee
    {
        public int id { get; set; }
        public int department_id { get; set; }
        public int position_id { get; set; }
        public string code { get; set; }
        public string lastname { get; set; }
        public string firstname { get; set; }
        public string middlename { get; set; }
        public string extname { get; set; }
        public bool active { get; set; }

        public static List<Employee> getAll()
        {
            List<Employee> employees = new List<Employee>();

            try
            {
                using (MySqlConnection con = new MySqlConnection(Connection.Connect()))
                {
                    MySqlCommand cmd = new MySqlCommand();
                    cmd.Connection = con;
                    cmd.CommandText = "SELECT * FROM employees WHERE active = 1 ORDER BY department_id ASC";
                    con.Open();
                    using (MySqlDataReader rdr = cmd.ExecuteReader())
                    {
                        while (rdr.Read())
                        {
                            Employee e = new Employee();
                            e.id = rdr.GetInt32(0);
                            e.department_id = rdr.GetInt32(1);
                            e.position_id = rdr.GetInt32(2);
                            e.code = rdr.GetString(3);
                            e.lastname = rdr.GetString(4);
                            e.firstname = rdr.GetString(5);
                            e.middlename = rdr.GetString(6);
                            e.extname = rdr.GetString(7);
                            e.active = rdr.GetBoolean(8);
                            employees.Add(e);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            return employees;
        }

        public static List<Employee> getAllOrderByCode()
        {
            List<Employee> employees = new List<Employee>();

            try
            {
                using (MySqlConnection con = new MySqlConnection(Connection.Connect()))
                {
                    MySqlCommand cmd = new MySqlCommand();
                    cmd.Connection = con;
                    cmd.CommandText = "SELECT * FROM employees ORDER BY code ASC";
                    con.Open();
                    using (MySqlDataReader rdr = cmd.ExecuteReader())
                    {
                        while (rdr.Read())
                        {
                            Employee e = new Employee();
                            e.id = rdr.GetInt32(0);
                            e.department_id = rdr.GetInt32(1);
                            e.position_id = rdr.GetInt32(2);
                            e.code = rdr.GetString(3);
                            e.lastname = rdr.GetString(4);
                            e.firstname = rdr.GetString(5);
                            e.middlename = rdr.GetString(6);
                            e.extname = rdr.GetString(7);
                            e.active = rdr.GetBoolean(8);
                            employees.Add(e);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            return employees;
        }

        public static Employee getById(int id)
        {
            Employee employee = new Employee();

            try
            {
                using (MySqlConnection con = new MySqlConnection(Connection.Connect()))
                {
                    MySqlCommand cmd = new MySqlCommand();
                    cmd.Connection = con;
                    cmd.CommandText = "SELECT * FROM employees WHERE id = @id";
                    cmd.Parameters.AddWithValue("id", id);
                    con.Open();
                    using (MySqlDataReader rdr = cmd.ExecuteReader())
                    {
                        while (rdr.Read())
                        {
                            employee.id = rdr.GetInt32(0);
                            employee.department_id = rdr.GetInt32(1);
                            employee.position_id = rdr.GetInt32(2);
                            employee.code = rdr.GetString(3);
                            employee.lastname = rdr.GetString(4);
                            employee.firstname = rdr.GetString(5);
                            employee.middlename = rdr.GetString(6);
                            employee.extname = rdr.GetString(7);
                            employee.active = rdr.GetBoolean(8);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            return employee;
        }

        public static Employee getByCode(string Code)
        {
            Employee employee = new Employee();

            try
            {
                using (MySqlConnection con = new MySqlConnection(Connection.Connect()))
                {
                    MySqlCommand cmd = new MySqlCommand();
                    cmd.Connection = con;
                    cmd.CommandText = "SELECT * FROM employees WHERE code = @code";
                    cmd.Parameters.AddWithValue("code", Code);
                    con.Open();
                    using (MySqlDataReader rdr = cmd.ExecuteReader())
                    {
                        while (rdr.Read())
                        {
                            employee.id = rdr.GetInt32(0);
                            employee.department_id = rdr.GetInt32(1);
                            employee.position_id = rdr.GetInt32(2);
                            employee.code = rdr.GetString(3);
                            employee.lastname = rdr.GetString(4);
                            employee.firstname = rdr.GetString(5);
                            employee.middlename = rdr.GetString(6);
                            employee.extname = rdr.GetString(7);
                            employee.active = rdr.GetBoolean(8);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            return employee;
        }

        public string GetFullName()
        {
            string fullname = firstname + " ";
            if (!string.IsNullOrWhiteSpace(middlename))
            {
                string[] mis = middlename.Split(' ');
                foreach (string mi in mis)
                {
                    fullname += mi.Substring(0, 1) + ". ";
                }
            }
            fullname += lastname;
            if (!string.IsNullOrWhiteSpace(extname)) fullname += " " + extname;
            return fullname;
        }

        public string Check()
        {
            if (department_id == 0) return "Please select department.";
            if (position_id == 0) return "Please select position.";
            if (string.IsNullOrWhiteSpace(code)) return "Code can not be blank.";
            if (string.IsNullOrWhiteSpace(lastname)) return "Last name can not be blank.";
            if (string.IsNullOrWhiteSpace(firstname)) return "First name can not be blank.";
            return null;
        }

        //save
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
                        cmd.CommandText = "UPDATE employees SET department_id = @department_id, position_id = @position_id, code = @code, lastname = @lastname, firstname = @firstname, middlename = @middlename, extname = @extname, active = @active WHERE id = @id";
                        cmd.Parameters.AddWithValue("id", id);
                    }
                    else
                    {
                        cmd.CommandText = "INSERT INTO employees (department_id, position_id, code, lastname, firstname, middlename, extname, active) VALUES (@department_id, @position_id, @code, @lastname, @firstname, @middlename, @extname, @active)";
                    }
                    cmd.Parameters.AddWithValue("department_id", department_id);
                    cmd.Parameters.AddWithValue("position_id", position_id);
                    cmd.Parameters.AddWithValue("code", code);
                    cmd.Parameters.AddWithValue("lastname", lastname);
                    cmd.Parameters.AddWithValue("firstname", firstname);
                    cmd.Parameters.AddWithValue("middlename", middlename);
                    cmd.Parameters.AddWithValue("extname", extname);
                    cmd.Parameters.AddWithValue("active", active);
                    con.Open();
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
