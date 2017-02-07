using System;
using System.Collections.Generic;
using System.Linq;
using MySql.Data.MySqlClient;
using System.Text;
using System.Threading.Tasks;

namespace TTC_Payroll_System.Classes
{
    class Connection
    {
        public static string Connect()
        {
            MySqlConnectionStringBuilder csb = new MySqlConnectionStringBuilder();
            csb.Server = Properties.Settings.Default.Server;
            csb.Port = Properties.Settings.Default.Port;
            csb.UserID = Properties.Settings.Default.UserID;
            csb.Password = Properties.Settings.Default.Password;
            csb.Database = Properties.Settings.Default.Database;

            return csb.ConnectionString;
           
        }
    }
}
