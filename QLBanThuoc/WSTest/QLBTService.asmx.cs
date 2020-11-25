﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
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
        //5. Chức năng đăng nhập
        public static string Ma_USER, Ten_USER;

        [WebMethod]
        public string getID(string username, string pass)
        {
            //Tạo MD5 
            MD5 mh = MD5.Create();
            //Chuyển kiểu chuổi thành kiểu byte
            byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(pass);
            //mã hóa chuỗi đã chuyển
            byte[] hash = mh.ComputeHash(inputBytes);
            //tạo đối tượng StringBuilder (làm việc với kiểu dữ liệu lớn)
            StringBuilder sb = new StringBuilder();

            for (int i = 0; i < hash.Length; i++)
            {
                sb.Append(hash[i].ToString("X2"));
            }
            string id = "";
            try
            {
                SqlCommand cmd = new SqlCommand("SELECT * FROM NHANVIEN WHERE username ='" + username + "' and password='" + sb.ToString() + "'", Connection.connection);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                if (dt != null)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        Ten_USER = dr["TenNhanVien"].ToString();
                        Ma_USER = dr["MaNhanVien"].ToString();
                    }
                }
            }
            catch (Exception)
            {
                return "0";
            }
            return Ten_USER;
        }
        [WebMethod]
        public string ma_user()
        {
            return Ma_USER;
        }
        //6. Chức năng cập nhật danh sách thuốc
        [WebMethod]
        public DataTable TimKiemThuoc(string str)
        {
            DataTable result = new DataTable("DSTB");
            SqlCommand sqlCommand = new SqlCommand("select MaThuoc, MaLoThuoc, TenThuoc, MaLoaiThuoc, DonGia, SoLuongTon from THUOC where THUOC.TenThuoc like N'%' + @ten + '%'", Connection.connection);
            sqlCommand.Parameters.AddWithValue("@ten", str);
            SqlDataAdapter Adapter = new SqlDataAdapter(sqlCommand);
            Adapter.Fill(result);
            Adapter.Dispose();
            //Kết thúc lấy dữ liệu
            return result;
        }
        //6.1 Tìm kiếm thuốc để điền lên form
        [WebMethod]
        public DataTable TimKiemThuocSua(string str)
        {
            DataTable result = new DataTable("DSTB");
            SqlCommand sqlCommand = new SqlCommand("select TenThuoc,THUOC.MaLoThuoc,MaLoaiThuoc,MaHangSX,ThanhPhan,CongDung,NgaySanXuat,HanSuDung,DonGia,SoLuongTon,DangThuoc from THUOC, LOTHUOC  where THUOC.MaThuoc like @maT and THUOC.MaLoThuoc = LOTHUOC.MaLoThuoc ", Connection.connection);
            sqlCommand.Parameters.AddWithValue("@maT", str);
            SqlDataAdapter Adapter = new SqlDataAdapter(sqlCommand);
            Adapter.Fill(result);
            Adapter.Dispose();
            //Kết thúc lấy dữ liệu
            return result;         
        }
        //6.2. Lưu thuốc sau khi sửa thông tin
        [WebMethod]
        public int LuuThuoc(string mathuoc,string malo,string ten,string tp,string cd, string nsx,string hsd,string slt,string dt,string dongia )
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "proc_UpdateThuoc";            
            cmd.Parameters.AddWithValue("@mathuoc", SqlDbType.Char).Value = mathuoc;
            cmd.Parameters.AddWithValue("@malo", SqlDbType.Char).Value = malo;
            cmd.Parameters.AddWithValue("@ten", SqlDbType.NVarChar).Value = ten;
            cmd.Parameters.AddWithValue("@tp", SqlDbType.NVarChar).Value = tp;
            cmd.Parameters.AddWithValue("@cd", SqlDbType.NVarChar).Value =cd;
            cmd.Parameters.AddWithValue("@nsx", SqlDbType.Date).Value = nsx;
            cmd.Parameters.AddWithValue("@hsd", SqlDbType.Date).Value = hsd;
            cmd.Parameters.AddWithValue("@slt", SqlDbType.Int).Value = Convert.ToInt32(slt);
            cmd.Parameters.AddWithValue("@dt", SqlDbType.NVarChar).Value = dt;
            cmd.Parameters.AddWithValue("@dongia", SqlDbType.Int).Value = Convert.ToInt32(dongia);
            int i = Connection.executeProc(cmd);
            return i;
        }
        //6.3. Xóa thuốc đi
        [WebMethod]
        public int XoaThuoc(string maThuoc)
        {
            SqlCommand sqlCommand = new SqlCommand();
            sqlCommand.CommandText = "proc_deleteThuoc";
            sqlCommand.Parameters.AddWithValue("@maT", SqlDbType.Char).Value = maThuoc;
            int i = Connection.executeProc(sqlCommand);
            return i;
        }
        //Tùng - Tìm kiếm
        [WebMethod]
        public DataTable DanhSachThuocTheoTen(string TenThuoc)
        {
            DataTable result = new DataTable("DS");
            //Lấy dữ liệu
            SqlCommand command = new SqlCommand("execute dbo.proc_TimKiemTheoTen '" + TenThuoc + "'", Connection.connection);
            SqlDataAdapter adapter = new SqlDataAdapter(command);
            adapter.Fill(result);
            adapter.Dispose();
            //Kết thúc lấy dữ liệu
            return result;
        }
        //[WebMethod]
        //public DataTable DanhSachThuocTheoThoiHan(string date1, string date2)
        //{
        //    DataTable result = new DataTable("DS");
        //    //Lấy dữ liệu
        //    string get = @"execute dbo.proc_TimKiemTheoThoiHan '" + date1 + "', '" + date2 + "'";
        //    SqlCommand command = new SqlCommand(get, Connection.conn);
        //    SqlDataAdapter adapter = new SqlDataAdapter(command);
        //    adapter.Fill(result);
        //    adapter.Dispose();
        //    //Kết thúc lấy dữ liệu
        //    return result;
        //}
        //[WebMethod]
        //public DataTable DanhSachThuocTheoTenVaThoiHan(string TenThuoc, string date1, string date2)
        //{
        //    DataTable result = new DataTable("DS");
        //    //Lấy dữ liệu
        //    string get = @"execute dbo.proc_TimKiemTheoTenVaThoiHan '" + TenThuoc + "', '" + date1 + "', '" + date2 + "'";
        //    SqlCommand command = new SqlCommand(get, Connection.conn);
        //    SqlDataAdapter adapter = new SqlDataAdapter(command);
        //    adapter.Fill(result);
        //    adapter.Dispose();
        //    //Kết thúc lấy dữ liệu
        //    return result;
        //}

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
