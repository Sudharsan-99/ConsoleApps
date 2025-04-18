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
        private static SqlConnection connection;

        public SqlConnection getConnection()
        {

            return connection;
        }
    }
}
