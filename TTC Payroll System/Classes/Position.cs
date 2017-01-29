using System;
using System.Collections.Generic;
using System.Linq;
using MySql.Data.MySqlClient;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TTC_Payroll_System.Classes
{
    class Position
    {
        public int id { get; set; }
        public string name { get; set; }
        public decimal salary { get; set; }

        public static List<Position> getAll()
        {
            List<Position> positions = new List<Position>();
            
            try
            {
                using (MySqlConnection con = new MySqlConnection(Connection.Connect()))
                {
                    MySqlCommand cmd = new MySqlCommand();
                    cmd.Connection = con;
                    cmd.CommandText = "SELECT * FROM positions ORDER BY salary DESC";
                    con.Open();
                    using (MySqlDataReader rdr = cmd.ExecuteReader())
                    {
                        while (rdr.Read())
                        {
                            Position p = new Position();
                            p.id = rdr.GetInt32(0);
                            p.name = rdr.GetString(1);
                            p.salary = rdr.GetDecimal(2);
                            positions.Add(p);
                        }
                    }
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            return positions;
        }


        public static Position getById(int id)
        {
            Position position = new Position();

            try
            {
                using (MySqlConnection con = new MySqlConnection(Connection.Connect()))
                {
                    MySqlCommand cmd = new MySqlCommand();
                    cmd.Connection = con;
                    cmd.CommandText = "SELECT * FROM positions WHERE id = @id";
                    cmd.Parameters.AddWithValue("id", id);
                    con.Open();
                    using (MySqlDataReader rdr = cmd.ExecuteReader())
                    {
                        while (rdr.Read())
                        {
                            position.id = rdr.GetInt32(0);
                            position.name = rdr.GetString(1);
                            position.salary = rdr.GetDecimal(2);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            return position;
        }

        public static Position getByName(string name)
        {
            Position position = new Position();

            try
            {
                using (MySqlConnection con = new MySqlConnection(Connection.Connect()))
                {
                    MySqlCommand cmd = new MySqlCommand();
                    cmd.Connection = con;
                    cmd.CommandText = "SELECT * FROM positions WHERE name = @name";
                    cmd.Parameters.AddWithValue("name", name);
                    con.Open();
                    using (MySqlDataReader rdr = cmd.ExecuteReader())
                    {
                        while (rdr.Read())
                        {
                            position.id = rdr.GetInt32(0);
                            position.name = rdr.GetString(1);
                            position.salary = rdr.GetDecimal(2);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            return position;
        }

        public string Check()
        {
            if (string.IsNullOrWhiteSpace(name)) return "Name can not be blank.";
            if (salary == 0m) return "Salary can be 0.00.";
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
                    if(id > 0)
                    {
                        cmd.CommandText = "UPDATE positions SET name = @name, salary = @salary WHERE id = @id";
                        cmd.Parameters.AddWithValue("id", id);
                    }
                    else
                    {
                        cmd.CommandText = "INSERT INTO positions (name, salary) VALUES (@name, @salary)";
                    }
                    cmd.Parameters.AddWithValue("name", name);
                    cmd.Parameters.AddWithValue("salary", salary);
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
