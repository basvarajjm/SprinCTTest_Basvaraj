using System.Data.SqlClient;

namespace SprinCTTest_Basvaraj.DAL
{
    public class BaseDAL
    {
        private readonly IConfiguration _configuration;
        public BaseDAL(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        protected (SqlConnection, SqlCommand, SqlDataAdapter) GetDBRelatedObjects(string procName, System.Data.CommandType cmdType)
        {
            string conStr = _configuration.GetConnectionString("ConnectionString");
            SqlConnection con = new SqlConnection(conStr);
            SqlCommand cmd = new SqlCommand(procName, con);
            cmd.CommandType = cmdType;
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter();
            sqlDataAdapter.SelectCommand = cmd;
            return (con, cmd, sqlDataAdapter);
        }
    }
}
