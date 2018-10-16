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

        public DataTable GetPlacesFromCategory(string CategoryName)
        {
            string connStr = ConfigurationManager.ConnectionStrings["Connection"].ToString();
            SqlConnection cn = new SqlConnection();
            cn.ConnectionString = connStr;
            cn.Open();
            SqlCommand cmd = new SqlCommand("GetPlacesFromCategory");
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(new SqlParameter("@CategoryName", CategoryName));
            cmd.Connection = cn;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            cn.Close();
            return dt;
        }

        public int SaveUserFiles(string UserProfileId, string FileName, string BucketName)
        {
            string connStr = ConfigurationManager.ConnectionStrings["Connection"].ToString();
            SqlConnection cn = new SqlConnection();
            cn.ConnectionString = connStr;
            cn.Open();
            SqlCommand cmd = new SqlCommand("SaveUserFiles");
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(new SqlParameter("@UserProfileId", UserProfileId));
            cmd.Parameters.Add(new SqlParameter("@FileName", FileName));
            cmd.Parameters.Add(new SqlParameter("@BucketName", BucketName));
            cmd.Connection = cn;
            int val = cmd.ExecuteNonQuery();
            cn.Close();
            return val;

        }

        public DataTable GetUserFiles(string UserProfileId)
        {
            string connStr = ConfigurationManager.ConnectionStrings["Connection"].ToString();
            SqlConnection cn = new SqlConnection();
            cn.ConnectionString = connStr;
            cn.Open();
            SqlCommand cmd = new SqlCommand("GetUserFiles");
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(new SqlParameter("@UserProfileId", UserProfileId));
            cmd.Connection = cn;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            cn.Close();
            return dt;
        }

        public DataTable GetAllUserFiles()
        {
            string connStr = ConfigurationManager.ConnectionStrings["Connection"].ToString();
            SqlConnection cn = new SqlConnection();
            cn.ConnectionString = connStr;
            cn.Open();
            SqlCommand cmd = new SqlCommand("GetAllUserFiles");
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = cn;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            cn.Close();
            return dt;
        }

    }
}