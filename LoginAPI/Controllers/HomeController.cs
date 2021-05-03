using LoginAPI.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.UI.WebControls;
using Login = LoginAPI.Models.Login;

namespace LoginAPI.Controllers
{
    public class HomeController : ApiController
    {
       

        [HttpPost]
        public Student GetLogin(Login login)
        {

            Student student = new Student();
            string strcon = ConfigurationManager.ConnectionStrings["dbconnection"].ConnectionString;
            SqlConnection connect = new SqlConnection(strcon);
            connect.Open();
            SqlCommand cmd = new SqlCommand("LoginStudent", connect);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@UserName", login.UserName);
            cmd.Parameters.AddWithValue("@Password", login.Password);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            String dataSetUserName;
            String dataSetPassword;

            if (ds.Tables[0].Rows.Count > 0)
            {

                dataSetUserName = ds.Tables[0].Rows[0]["UserName"].ToString();
                dataSetPassword = ds.Tables[0].Rows[0]["Password"].ToString();

                if (dataSetUserName == login.UserName && dataSetPassword == login.Password)
                {
                   
                    string stcon = ConfigurationManager.ConnectionStrings["dbconnection"].ConnectionString;
                    SqlConnection conn = new SqlConnection(stcon);
                    conn.Open();
                    SqlCommand cmmd = new SqlCommand("StudentList", conn);
                    cmmd.CommandType = CommandType.StoredProcedure;
                    cmmd.Parameters.AddWithValue("@UserName", login.UserName);
                    cmmd.Parameters.AddWithValue("@Password", login.Password);
                    SqlDataReader reader = cmmd.ExecuteReader();
                    while (reader.Read())
                    {
                        student.Name = reader["Name"].ToString();
                        student.English = Convert.ToInt32(reader["English"]);
                        student.Hindi = Convert.ToInt32(reader["Hindi"]);
                        student.Malayalam = Convert.ToInt32(reader["Malayalam"]);
                    }
                    conn.Close();
                }
                else
                {
                    student.Status = "Invalid Login";
                }             
            }
            else
            {
                student.Message = "Invalid UserName and Password";
            }
           
            return student;
        }

       
    }


}
