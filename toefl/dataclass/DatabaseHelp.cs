using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;

namespace toefl
{
    class DatabaseHelp
    {
        private static SqlConnection connection;
        public static SqlConnection Connection
        {
            get
            {
                if (connection == null)
                {
                    string connectionString = ConfigurationManager.AppSettings["connectionString"];
                    connection = new SqlConnection(connectionString);
                    connection.Open();
                }
                else if(connection.State == System.Data.ConnectionState.Closed)
                {
                    connection.Open();
                }
                else if(connection.State == System.Data.ConnectionState.Broken)
                {
                    connection.Close();
                    connection.Open();
                }
                return connection;
            }
        }
        public static void ExecuteProc(string xmlstr, string proc)
        {
            
            SqlCommand testcmd = new SqlCommand();
            testcmd.Connection = connection;
            
            try
            {
                testcmd.CommandType = CommandType.StoredProcedure;
                testcmd.CommandText = proc;
                testcmd.Parameters.Add("@xml", SqlDbType.VarChar, -1).Value = xmlstr;
                testcmd.ExecuteNonQuery();
            }
            catch (Exception exception)
            {
                throw exception;
            }

        }

        //通过SQL语句和条件增删改一条数据
        public static int executeCommand(string sql)
        {
            SqlCommand cmd = new SqlCommand(sql, Connection);
            int result = cmd.ExecuteNonQuery();
            return result;
        }
        public static int ExecuteCommand(string safeSql, params SqlParameter[] values)
        {
            SqlCommand cmd = new SqlCommand(safeSql, Connection);
            cmd.Parameters.AddRange(values);
            return cmd.ExecuteNonQuery();
        }

        //通过SQL语句查询数据库信息
        public static SqlDataReader getReader(string sql)
        {
            SqlCommand cmd = new SqlCommand(sql, Connection);
            SqlDataReader reader = cmd.ExecuteReader();
            return reader;
        }
        public static SqlDataReader GetReader(string sql, params SqlParameter[] values)
        {
            SqlCommand cmd = new SqlCommand(sql, Connection);
            cmd.Parameters.AddRange(values);
            SqlDataReader reader = cmd.ExecuteReader();
            return reader;
        }
        //通过SQL语句查询一条数据库信息
        public static int GetScalar(string safeSql)
        {
            SqlCommand cmd = new SqlCommand(safeSql, Connection);
            int result;
            try
            {
                result = Convert.ToInt32(cmd.ExecuteScalar());
            }
            catch (Exception ex)
            {
                result = 0;
            }
            return result;
        }
        //返回查询影响的行数
        public static int SelectNum(string safesql)
        {
            SqlCommand cmd = new SqlCommand(safesql, Connection);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            int count = dt.Rows.Count;
            return count;
        }
        #region DBTypeToSystemType
        //数据库bit转换为bool
        public static bool convert(bool obj, object data)
        {
            return Convert.ToBoolean(data);
        }
        public static bool? convert(bool? obj, object data)
        {
            if (DBNull.Value.Equals(data))
            {
                return null;
            }
            return Convert.ToBoolean(data);
        }
        //数据库tinyint转换为byte
        public static byte convert(byte obj, object data)
        {
            return Convert.ToByte(data);
        }
        public static byte? convert(byte? obj, object data)
        {
            if (DBNull.Value.Equals(data))
            {
                return null;
            }
            return Convert.ToByte(data);
        }
        //数据库float转换为double
        public static double convert(double obj, object data)
        {
            return Convert.ToDouble(data);
        }
        public static double? convert(double? obj, object data)
        {
            if (DBNull.Value.Equals(data))
            {
                return null;
            }
            return Convert.ToDouble(data);
        }
        //数据库smallint转换为short
        public static short convert(short obj, object data)
        {
            return Convert.ToInt16(data);
        }
        public static short? convert(short? obj, object data)
        {
            if (DBNull.Value.Equals(data))
            {
                return null;
            }
            return Convert.ToInt16(data);
        }
        //数据库int转换为int
        public static int convert(int obj, object data)
        {
            return Convert.ToInt32(data);
        }
        public static int? convert(int? obj, object data)
        {
            if (DBNull.Value.Equals(data))
            {
                return null;
            }
            return Convert.ToInt32(data);
        }
        //数据库datetime转换为DateTime
        public static DateTime convert(DateTime obj, object data)
        {
            return Convert.ToDateTime(data);
        }
        public static DateTime? convert(DateTime? obj, object data)
        {
            if (DBNull.Value.Equals(data))
            {
                return null;
            }
            return Convert.ToDateTime(data);
        }
        //将数据库binary类型转换为byte[]
        public static byte[] convert(byte[] obj, object data)
        {
            if (DBNull.Value.Equals(data))
            {
                return null;
            }
            return (byte[])data;
        }
        //将数据库char类型转换为string
        public static string convert(string obj, object data)
        {
            if (DBNull.Value.Equals(data))
            {
                return null;
            }
            return Convert.ToString(data);
        }
        //将数据库binary类型转换为Unicode string
        public static string convertUnicode(object data)
        {
            if (DBNull.Value.Equals(data))
            {
                return null;
            }
            return Encoding.Unicode.GetString((byte[])data);
        }
        #endregion

        public static object convertToSqlData(object obj)
        {
            if (obj == null)
            {
                return DBNull.Value;
            }
            else
            {
                return obj;
            }
        }
    }
}
