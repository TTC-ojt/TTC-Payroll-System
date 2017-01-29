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
            csb.Database = "ttc_payroll";
            csb.UserID = "root";
            csb.Password = "";
            csb.Port = 3306;
            csb.Server = "localhost";

            return csb.ConnectionString;
           
        }
    }
}
