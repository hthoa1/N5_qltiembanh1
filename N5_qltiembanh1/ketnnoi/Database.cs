using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace N5_qltiembanh1.ketnnoi
{
    public class Database
    {
        private static string connString = ConfigurationManager.ConnectionStrings["QltiembanhConnectionString"].ConnectionString;

        // 1. Hàm SELECT dữ liệu, trả về DataTable dùng cho GridView, DropDownList, v.v.
        public static DataTable GetData(string sql, SqlParameter[] parameters = null)
        {
            DataTable dt = new DataTable();
            using (SqlConnection conn = new SqlConnection(connString))
            {
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    if (parameters != null)
                    {
                        cmd.Parameters.AddRange(parameters);
                    }
                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        da.Fill(dt);
                    }
                }
            }
            return dt;
        }

        // 2. Hàm thực thi INSERT, UPDATE, DELETE, trả về số dòng bị ảnh hưởng
        public static int ExecuteNonQuery(string sql, SqlParameter[] parameters = null)
        {
            int rowsAffected = 0;
            using (SqlConnection conn = new SqlConnection(connString))
            {
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    if (parameters != null)
                    {
                        cmd.Parameters.AddRange(parameters);
                    }
                    conn.Open();
                    rowsAffected = cmd.ExecuteNonQuery();
                }
            }
            return rowsAffected;
        }
    }
}