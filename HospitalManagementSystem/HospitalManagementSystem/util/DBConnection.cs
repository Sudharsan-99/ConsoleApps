using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalManagementSystem.util
{
    public class DBConnection
    {
        public static SqlConnection connection;

        public static SqlConnection getConnection()
        {
            connection = new SqlConnection("data source=DESKTOP-R2484O8\\SQLEXPRESS;intial catalog = HospitalManagementSystem;integrated security = true;");
            connection.Open();
            return connection;
        }
    }
}
