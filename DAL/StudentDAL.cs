using SprinCTTest_Basvaraj.Models;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Reflection;

namespace SprinCTTest_Basvaraj.DAL
{
    public class StudentDAL: BaseDAL
    {
        private readonly IConfiguration _configuration;

        public StudentDAL(IConfiguration configuration): base(configuration)
        {
            _configuration = configuration;
        }

        public ResponseModel<StudentModel> AddStudent(StudentModel model)
        {
            try
            {
                var (con, cmd, sqlDataAdapter) = GetDBRelatedObjects("Proc_AddStudent", CommandType.StoredProcedure);
                DataSet ds = new DataSet();

                cmd.Parameters.Add(new SqlParameter("@name", model.Name));
                cmd.Parameters.Add(new SqlParameter("@email", model.Email));
                cmd.Parameters.Add(new SqlParameter("@phone", model.Phone));

                con.Open();
                sqlDataAdapter.Fill(ds);
                con.Close();

                if (ds.Tables != null && ds.Tables[0].Rows != null && ds.Tables[0].Rows.Count > 0)
                {
                    model.Id = Convert.ToInt32(ds.Tables[0].Rows[0]["ID"]);
                    return new ResponseModel<StudentModel>()
                    {
                        Data = model,
                        Status = 200,
                        Message = "Data added successfully."
                    };
                }
            }
            catch (Exception ex)
            {
                return new ResponseModel<StudentModel>()
                {
                    Data = new StudentModel(),
                    Status = 500,
                    Message = "Error occured while saving the data."
                };

            }
            return new ResponseModel<StudentModel>()
            {
                Data = model,
                Status = 500,
                Message = "Data cannot be added."
            };
        }

        public ResponseModel<object> AssignCoursesToStudent(AssignCoursesToStudentModel model)
        {
            try
            {
                var (con, cmd, sqlDataAdapter) = GetDBRelatedObjects("Proc_AddStudentCourses", CommandType.StoredProcedure);
                DataSet ds = new DataSet();

                cmd.Parameters.Add(new SqlParameter("@studentId", model.StudentId));
                cmd.Parameters.Add(new SqlParameter("@commaSeparatedCourseIds", model.CommaSeparatedCourseIds));

                con.Open();
                sqlDataAdapter.Fill(ds);
                con.Close();

                if (ds.Tables != null && ds.Tables[0].Rows != null && ds.Tables[0].Rows.Count > 0)
                {
                    if (Convert.ToInt32(ds.Tables[0].Rows[0]["StatusCode"]) > 0)
                        return new ResponseModel<object>()
                        {
                            Status = 200,
                            Message = "Data added successfully."
                        };
                }
                return new ResponseModel<object>()
                {
                    Status = 500,
                    Message = ds.Tables[0].Rows[0]["Message"].ToString()
                };
            }
            catch (Exception ex)
            {
                return new ResponseModel<object>()
                {
                    Status = 500,
                    Message = "Error occured while saving the data."
                };

            }
        }

        public ResponseModel<List<StudentCoursesModel>> GetStudentsAndCourseEnrolledList()
        {
            var list = new List<StudentCoursesModel>();
            try
            {
                var (con, cmd, sqlDataAdapter) = GetDBRelatedObjects("Proc_GetStudentAndCourseEnrolledList", CommandType.StoredProcedure);
                DataSet ds = new DataSet();

                con.Open();
                sqlDataAdapter.Fill(ds);
                con.Close();

                if (ds.Tables != null && ds.Tables[0].Rows != null && ds.Tables[0].Rows.Count > 0)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; ++i)
                    {
                        StudentCoursesModel data = new StudentCoursesModel();
                        data.Id = Convert.ToInt32(ds.Tables[0].Rows[i]["ID"]);
                        data.Name = Convert.ToString(ds.Tables[0].Rows[i]["Name"]);
                        data.Email = Convert.ToString(ds.Tables[0].Rows[i]["Email"]);
                        data.Phone = Convert.ToString(ds.Tables[0].Rows[i]["Phone"]);
                        data.CourseEnrolled = Convert.ToString(ds.Tables[0].Rows[i]["CourseEnrolled"]);
                        list.Add(data);
                    }
                }
            }
            catch (Exception ex)
            {
                return new ResponseModel<List<StudentCoursesModel>>()
                {
                    Data = new List<StudentCoursesModel>(),
                    Status = 500,
                    Message = "Error occured while fetching the data."
                };

            }
            return new ResponseModel<List<StudentCoursesModel>>()
            {
                Data = list,
                Status = 200,
                Message = "Data fetched successfully."
            };
        }

        public ResponseModel<List<GetStudentByCourseModel>> GetStudentsListByCourseName(string courseName)
        {
            var list = new List<GetStudentByCourseModel>();
            try
            {
                var (con, cmd, sqlDataAdapter) = GetDBRelatedObjects("Proc_GetStudentsListByCourseName", CommandType.StoredProcedure);
                DataSet ds = new DataSet();

                cmd.Parameters.Add(new SqlParameter("@courseName", courseName));

                con.Open();
                sqlDataAdapter.Fill(ds);
                con.Close();

                if (ds.Tables != null && ds.Tables[0].Rows != null && ds.Tables[0].Rows.Count > 0)
                {
                    var map = new Dictionary<string, List<StudentModel>>();
                    for (int i = 0; i < ds.Tables[0].Rows.Count; ++i)
                    {
                        string cn = Convert.ToString(ds.Tables[0].Rows[i]["CourseName"]);
                        if (map.ContainsKey(cn))
                        {
                            var data = new StudentModel();
                            data.Name = Convert.ToString(ds.Tables[0].Rows[i]["Name"]);
                            data.Email = Convert.ToString(ds.Tables[0].Rows[i]["Email"]);
                            data.Phone = Convert.ToString(ds.Tables[0].Rows[i]["Phone"]);
                            map[cn].Add(data);
                        }
                        else
                        {
                            map.Add(cn, new List<StudentModel>());
                            var data = new StudentModel();
                            data.Name = Convert.ToString(ds.Tables[0].Rows[i]["Name"]);
                            data.Email = Convert.ToString(ds.Tables[0].Rows[i]["Email"]);
                            data.Phone = Convert.ToString(ds.Tables[0].Rows[i]["Phone"]);
                            map[cn].Add(data);
                        }
                        //GetStudentByCourseModel data = new GetStudentByCourseModel();
                        //data.CourseName = Convert.ToString(ds.Tables[0].Rows[i]["CourseName"]);
                        //data.Students = new List<StudentModel>();

                        //data.Name = Convert.ToString(ds.Tables[0].Rows[i]["Name"]);
                        //data.Email = Convert.ToString(ds.Tables[0].Rows[i]["Email"]);
                        //data.Phone = Convert.ToString(ds.Tables[0].Rows[i]["Phone"]);
                        //data.CourseEnrolled = Convert.ToString(ds.Tables[0].Rows[i]["CourseEnrolled"]);
                        //list.Add(data);
                    }
                    foreach (var item in map)
                    {
                        var data = new GetStudentByCourseModel();
                        data.CourseName = item.Key;
                        data.Students = item.Value;
                        list.Add(data);
                    }
                }
            }
            catch (Exception ex)
            {
                return new ResponseModel<List<GetStudentByCourseModel>>()
                {
                    Data = new List<GetStudentByCourseModel>(),
                    Status = 500,
                    Message = "Error occured while fetching the data."
                };

            }
            return new ResponseModel<List<GetStudentByCourseModel>>()
            {
                Data = list,
                Status = 200,
                Message = "Data fetched successfully."
            };
        }
    }
}
