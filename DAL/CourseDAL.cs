using SprinCTTest_Basvaraj.Models;
using System.Data.SqlClient;
using System.Data;
using SprinCTTest_Basvaraj.Logger;

namespace SprinCTTest_Basvaraj.DAL
{
    public class CourseDAL: BaseDAL
    {
        private readonly IConfiguration _configuration;

        public CourseDAL(IConfiguration configuration) : base(configuration)
        {
            _configuration = configuration;
        }

        public async Task<ResponseModel<CourseModel>> AddCourse(CourseModel model)
        {
            try
            {
                var (con, cmd, sqlDataAdapter) = GetDBRelatedObjects("Proc_AddCourse", CommandType.StoredProcedure);
                DataSet ds = new DataSet();

                cmd.Parameters.Add(new SqlParameter("@name", model.Name));
                cmd.Parameters.Add(new SqlParameter("@professorName", model.ProfessorName));
                cmd.Parameters.Add(new SqlParameter("@description", model.Description));

                con.Open();
                sqlDataAdapter.Fill(ds);
                con.Close();

                if (ds.Tables != null && ds.Tables[0].Rows != null && ds.Tables[0].Rows.Count > 0)
                {
                    model.Id = Convert.ToInt32(ds.Tables[0].Rows[0]["ID"]);
                    return new ResponseModel<CourseModel>()
                    {
                        Data = model,
                        Status = 200,
                        Message = "Data added successfully."
                    };
                }
            }
            catch (Exception ex)
            {
                await ErrorLogger.LogMessage(_configuration, ex);
                return new ResponseModel<CourseModel>()
                {
                    Data = new CourseModel(),
                    Status = 500,
                    Message = "Error occured while saving the data."
                };

            }
            return new ResponseModel<CourseModel>()
            {
                Data = model,
                Status = 500,
                Message = "Data cannot be added."
            };
        }

        public async Task<ResponseModel<CourseModel>> DeleteCourse(int id)
        {
            try
            {
                var (con, cmd, sqlDataAdapter) = GetDBRelatedObjects("Proc_DeleteCourse", CommandType.StoredProcedure);
                DataSet ds = new DataSet();

                cmd.Parameters.Add(new SqlParameter("@id", id));

                con.Open();
                sqlDataAdapter.Fill(ds);
                con.Close();

                if (ds.Tables != null && ds.Tables[0].Rows != null && ds.Tables[0].Rows.Count > 0)
                {
                    var model = new CourseModel();
                    model.Id = Convert.ToInt32(ds.Tables[0].Rows[0]["ID"]);
                    model.Name = Convert.ToString(ds.Tables[0].Rows[0]["Name"]);
                    model.ProfessorName = Convert.ToString(ds.Tables[0].Rows[0]["ProfessorName"]);
                    model.Description = Convert.ToString(ds.Tables[0].Rows[0]["Description"]);
                    return new ResponseModel<CourseModel>()
                    {
                        Data = model,
                        Status = 200,
                        Message = "Course removed successfully."
                    };
                }
            }
            catch (Exception ex)
            {
                await ErrorLogger.LogMessage(_configuration, ex);
                return new ResponseModel<CourseModel>()
                {
                    Data = new CourseModel(),
                    Status = 500,
                    Message = "Error occured while removing the course data."
                };

            }
            return new ResponseModel<CourseModel>()
            {
                Data = new CourseModel(),
                Status = 500,
                Message = "Inavlid course id."
            };
        }
    }
}
