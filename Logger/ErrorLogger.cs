using System.Data.SqlClient;
using System.Reflection;
using System.Text;

namespace SprinCTTest_Basvaraj.Logger
{
    public class ErrorLogger
    {
        public static async Task LogMessage(IConfiguration configuration, Exception ex, string message = "")
        {
            IConfiguration _configuration = configuration;
            try
            {
                string conStr = _configuration.GetConnectionString("ConnectionString");
                SqlConnection con = new SqlConnection(conStr);
                SqlCommand cmd = new SqlCommand("INSERT INTO ErrorLogger (Message, StackTrace, DATETIME) Values (@Message,@StackTrace, @DATETIME);", con);

                var tmpstr = $"{(message ?? "")} : {ex.Message ?? ""}";
                cmd.Parameters.Add(new SqlParameter("@Message", tmpstr ?? ""));
                cmd.Parameters.Add(new SqlParameter("@StackTrace", ex.StackTrace ?? ""));
                cmd.Parameters.Add(new SqlParameter("@DATETIME", DateTime.Now));

                cmd.CommandType = System.Data.CommandType.Text;
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }
            catch (Exception e)
            {
                await LogMessageIntoFile(e, message);
            }
        }

        public static async Task LogMessageIntoFile(Exception ex, string message = "")
        {
            FileStream fs = null;
            try
            {
                string logFolder = $@"{Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName}\Log\";
                string logFile = $@"{logFolder}{DateTime.Today.ToString("dd/MM/yyyy")}.txt";

                if (!Directory.Exists(logFolder))
                    Directory.CreateDirectory(logFolder);

                if (!File.Exists(logFile))
                    fs = File.Create(logFile);

                if (fs == null)
                {
                    fs = new FileStream(logFile, FileMode.Append);
                }
                string textToLog = string.Empty;
                if (ex is not null)
                {
                    textToLog = $"Error occured at: {DateTime.Now} {Environment.NewLine}"
                        + $"Exception Message: {ex.Message ?? ""} {Environment.NewLine}"
                        + $"Stack Trace: {ex.StackTrace ?? ""} {Environment.NewLine}";
                    if (ex.InnerException != null)
                    {
                        textToLog += $"=============Inner Exception=========="
                        + $"Error occured at: {DateTime.Now} {Environment.NewLine}"
                        + $"Exception Message: {ex.InnerException.Message ?? ""} {Environment.NewLine}"
                        + $"Stack Trace: {ex.InnerException.StackTrace ?? ""} {Environment.NewLine}";
                    }
                }
                else
                    textToLog = $@"Custom Message: {message ?? ""}";

                textToLog += $"============================================================{Environment.NewLine}";

                byte[] data = new UTF8Encoding(true).GetBytes(textToLog);
                await fs.WriteAsync(data, 0, data.Length);
            }
            catch (Exception e)
            {
            }
            finally
            {
                if (fs != null)
                    fs.Close();
            }
        }
    }
}
