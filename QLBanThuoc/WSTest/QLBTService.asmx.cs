using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Services;

namespace WSTest
{
    /// <summary>
    /// Summary description for QLBanThuocService
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    static class Connection
    {
        static string ConnectionString = @"Data Source=(local)\SQLEXPRESS;Initial Catalog=QLBanThuoc; Integrated Security=True;Connect Timeout=200";
        public static SqlConnection connection = new SqlConnection(ConnectionString);
        public static int executeProc(SqlCommand command)
        {
            try
            {
                if (connection.State == 0)
                    connection.Open();
                command.Connection = connection;
                command.CommandType = CommandType.StoredProcedure;
                return command.ExecuteNonQuery();
            }
            catch (SqlException err)
            {
                return 0;
            }
        }
        public static void readDataProc(SqlCommand sql, DataTable dt)
        {
            if (connection.State == ConnectionState.Closed)
                connection.Open();
            sql.Connection = connection;
            sql.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter();
            sqlDataAdapter.SelectCommand = sql;
            sqlDataAdapter.Fill(dt);
        }
    }
    public class QLBanThuocService : System.Web.Services.WebService
    {
        //form nhà cung cấp
        //1. Chức năng thêm mới NCC
        [WebMethod]
        public int ThemMoiNCC(string mancc, string tenncc,string nguoidd,string sdt,string diachi)
        {
            SqlCommand sqlCommand = new SqlCommand();
            sqlCommand.CommandText = "proc_addNCC";
            sqlCommand.Parameters.AddWithValue("@ma", mancc);
            sqlCommand.Parameters.AddWithValue("@ten", tenncc);
            sqlCommand.Parameters.AddWithValue("@tt", nguoidd);
            sqlCommand.Parameters.AddWithValue("@sdt", sdt);
            sqlCommand.Parameters.AddWithValue("@diachi", diachi);
            int i = Connection.executeProc(sqlCommand);
            return i;
        }
        //2. Chức năng cập nhật nhà cung cấp
        [WebMethod]
        public int SuaNhaCungCap(string mancc, string tenncc, string nguoidd, string sdt, string diachi)
        {
            SqlCommand sqlCommand = new SqlCommand();
            sqlCommand.CommandText = "proc_updateNCC";
            sqlCommand.Parameters.AddWithValue("@ma", mancc);
            sqlCommand.Parameters.AddWithValue("@ten", tenncc);
            sqlCommand.Parameters.AddWithValue("@tt", nguoidd);
            sqlCommand.Parameters.AddWithValue("@sdt", sdt);
            sqlCommand.Parameters.AddWithValue("@diachi", diachi);
            int i = Connection.executeProc(sqlCommand);
            return i;
        }
        //3. Chức năng xóa nhà cung cấp
        [WebMethod]
        public int XoaNhaCungCap(string mancc)
        {
            SqlCommand sqlCommand = new SqlCommand();
            sqlCommand.CommandText = "proc_deleteNCC";
            sqlCommand.Parameters.AddWithValue("@ma", mancc);
            int i = Connection.executeProc(sqlCommand);
            return i;
        }
        //4. Tìm kiếm nhà cung cấp
        [WebMethod]
        public DataTable TimKiemNCC(string thongtin)
        {
            DataTable result = new DataTable("DSTB");
            SqlCommand sqlCommand = new SqlCommand("select * from NHACUNGCAP where TenNhaCungCap like '%'+ @ten +'%'",Connection.connection);
            sqlCommand.Parameters.AddWithValue("@ten", thongtin);
            SqlDataAdapter Adapter = new SqlDataAdapter(sqlCommand);
            Adapter.Fill(result);
            Adapter.Dispose();
            //Kết thúc lấy dữ liệu
            return result;
        }
        [WebMethod]
        public string HelloWorld()
        {
            return "Hello World";
        }

        [WebMethod]
        public string HelloName(string name)
        {
            return "Hello " + name;
        }
    }
}
