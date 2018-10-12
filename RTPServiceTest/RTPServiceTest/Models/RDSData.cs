using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.IO;

namespace RTPServiceTest.Models
{
    public class RDSData
    {
        SqlConnection cn;

        public DataTable GetEmployee(string Emailid, string password)
        {
            string connStr = ConfigurationManager.ConnectionStrings["Connection"].ToString();
            SqlConnection cn = new SqlConnection();
            cn.ConnectionString = connStr;
            cn.Open();
            SqlCommand cmd = new SqlCommand("GetEmployee");
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(new SqlParameter("@Emailid", Emailid));
            cmd.Parameters.Add(new SqlParameter("@password", password));
            cmd.Connection = cn;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            cn.Close();
            return dt;
        }

        public int SP_SaveFileDetails(int EmployeeID, string FileName, string BucketName)
        {
            string connStr = ConfigurationManager.ConnectionStrings["Connection"].ToString();
            SqlConnection cn = new SqlConnection();
            cn.ConnectionString = connStr;
            cn.Open();
            SqlCommand cmd = new SqlCommand("SP_SaveFileDetails");
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(new SqlParameter("@EmployeeID", EmployeeID));
            cmd.Parameters.Add(new SqlParameter("@FileName", FileName));
            cmd.Parameters.Add(new SqlParameter("@BucketName", BucketName));
            cmd.Connection = cn;
            int val = cmd.ExecuteNonQuery();
            cn.Close();
            return val;

        }

        public DataTable SP_GetFileByEmployee(int EmpId)
        {
            string connStr = ConfigurationManager.ConnectionStrings["Connection"].ToString();
            SqlConnection cn = new SqlConnection();
            cn.ConnectionString = connStr;
            cn.Open();
            SqlCommand cmd = new SqlCommand("SP_GetFileByEmployee");
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(new SqlParameter("@EmpId", EmpId));
            cmd.Connection = cn;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            cn.Close();
            return dt;
        }

        public DataTable SP_GetAllFile()
        {
            string connStr = ConfigurationManager.ConnectionStrings["Connection"].ToString();
            SqlConnection cn = new SqlConnection();
            cn.ConnectionString = connStr;
            cn.Open();
            SqlCommand cmd = new SqlCommand("SP_GetAllFile");
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = cn;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            cn.Close();
            return dt;
        }

        public DataTable IsAdminRole(int EmpId)
        {
            string connStr = ConfigurationManager.ConnectionStrings["Connection"].ToString();
            SqlConnection cn = new SqlConnection();
            cn.ConnectionString = connStr;
            cn.Open();
            SqlCommand cmd = new SqlCommand("IsAdminRole");
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(new SqlParameter("@EmpId", EmpId));
            cmd.Connection = cn;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            cn.Close();
            return dt;
        }

        public DataTable GetDeptEmployees(int EmpId)
        {
            string connStr = ConfigurationManager.ConnectionStrings["Connection"].ToString();
            SqlConnection cn = new SqlConnection();
            cn.ConnectionString = connStr;
            cn.Open();
            SqlCommand cmd = new SqlCommand("GetDeptEmployees");
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(new SqlParameter("@EmpId", EmpId));
            cmd.Connection = cn;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            cn.Close();
            return dt;
        }
    }
}