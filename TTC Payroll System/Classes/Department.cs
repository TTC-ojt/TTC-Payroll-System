using System;
using System.Collections.Generic;
using System.Linq;
using MySql.Data.MySqlClient;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TTC_Payroll_System.Classes
{
    class Department
    {
        public int id { get; set; }
        public string name { get; set; }

        public static List<Department> getAll()
        {
            List<Department> departments = new List<Department>();

            try
            {
                using (MySqlConnection con = new MySqlConnection(Connection.Connect()))
                {
                    MySqlCommand cmd = new MySqlCommand();
                    cmd.Connection = con;
                    cmd.CommandText = "SELECT * FROM departments";
                    con.Open();
                    using (MySqlDataReader rdr = cmd.ExecuteReader())
                    {
                        while (rdr.Read())
                        {
                            Department d = new Department();
                            d.id = rdr.GetInt32(0);
                            d.name = rdr.GetString(1);
                            departments.Add(d);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            return departments;
        }

        public static Department getById(int id)
        {
            Department department = new Department();

            try
            {
                using (MySqlConnection con = new MySqlConnection(Connection.Connect()))
                {
                    MySqlCommand cmd = new MySqlCommand();
                    cmd.Connection = con;
                    cmd.CommandText = "SELECT * FROM departments where id = @id";
                    cmd.Parameters.AddWithValue("id", id);
                    con.Open();
                    using (MySqlDataReader rdr = cmd.ExecuteReader())
                    {
                        while (rdr.Read())
                        {
                            department.id = rdr.GetInt32(0);
                            department.name = rdr.GetString(1);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            return department;
        }

        public static Department getByName(string name)
        {
            Department department = new Department();

            try
            {
                using (MySqlConnection con = new MySqlConnection(Connection.Connect()))
                {
                    MySqlCommand cmd = new MySqlCommand();
                    cmd.Connection = con;
                    cmd.CommandText = "SELECT * FROM departments where name = @name";
                    cmd.Parameters.AddWithValue("name", name);
                    con.Open();
                    using (MySqlDataReader rdr = cmd.ExecuteReader())
                    {
                        while (rdr.Read())
                        {
                            department.id = rdr.GetInt32(0);
                            department.name = rdr.GetString(1);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            return department;
        }

        public string Check()
        {
            if (string.IsNullOrWhiteSpace(name)) return "Name can not be blank.";
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
                        cmd.CommandText = "UPDATE departments SET name = @name WHERE id = @id";
                        cmd.Parameters.AddWithValue("id", id);
                    }
                    else
                    {
                        cmd.CommandText = "INSERT INTO departments (name) VALUES (@name)";
                    }
                    cmd.Parameters.AddWithValue("name", name);
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
