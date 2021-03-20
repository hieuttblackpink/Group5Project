using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Group5Project
{
    class DBSQLServer
    {
        public static SqlConnection GetSqlConnection ()
        {
            string str = @"uid=sa;pwd=blackpink9999;Initial Catalog=QLSVien;Data Source=SE141080\SQLEXPRESS";

            SqlConnection con = new SqlConnection(str);

            return con;
        }
    }
}
