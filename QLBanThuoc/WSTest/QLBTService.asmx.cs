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
        //static string ConnectionString = @"Data Source=WIN7PROX64;Initial Catalog=QLBanThuoc; Integrated Security=True;Connect Timeout=200";
        static string ConnectionString1 = @"Data Source=(local)\SQLEXPRESS;Initial Catalog=CNWeb_Pharmacy; Integrated Security=True;Connect Timeout=200";
        public static SqlConnection connection = new SqlConnection(ConnectionString);
        public static SqlConnection connection1 = new SqlConnection(ConnectionString1);
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
        public static void createConn()
        {
            try
            {
                if (connection.State != ConnectionState.Open)
                {
                    connection.ConnectionString = ConnectionString;
                    connection.Open();
                }
            }
            catch (Exception err)
            {
                throw err;
            }
        }

        public static int executeQuery(SqlCommand dbCommend)
        {
            try
            {
                if (connection.State == 0)
                    createConn();
                dbCommend.Connection = connection;
                dbCommend.CommandType = CommandType.Text;
                return dbCommend.ExecuteNonQuery();
            }
            catch (Exception err)
            {
                throw err;
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
        public int ThemMoiNCC(string mancc, string tenncc, string nguoidd, string sdt, string diachi)
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
            SqlCommand sqlCommand = new SqlCommand("select * from NHACUNGCAP where TenNhaCungCap like '%'+ @ten +'%'", Connection.connection);
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
            Ten_USER = "0";
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
            //SqlCommand sqlCommand1 = new SqlCommand("select MaThuoc, MaLoThuoc, TenThuoc, MaLoaiThuoc, DonGia, SoLuongTon,CongDung,ThanhPhan, from THUOC where THUOC.TenThuoc like N'%' + @ten + '%' OR TimKiem LIKE N'%' + @ten + '%'", Connection.connection1);
            sqlCommand.Parameters.AddWithValue("@ten", str);
            //sqlCommand1.Parameters.AddWithValue("@ten", str);
            SqlDataAdapter Adapter = new SqlDataAdapter(sqlCommand);
            //SqlDataAdapter Adapter1 = new SqlDataAdapter(sqlCommand1);
            Adapter.Fill(result);
            Adapter.Dispose();
            //Adapter1.Fill(result);
            //Adapter1.Dispose();
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
            SqlCommand sqlCommand1 = new SqlCommand("select TenThuoc,THUOC.MaLoThuoc,MaLoaiThuoc,MaHangSX,ThanhPhan,CongDung,DonGia,SoLuongTon,DangThuoc from THUOC where THUOC.MaThuoc like @maT ", Connection.connection1);
            sqlCommand1.Parameters.AddWithValue("@maT", str);
            SqlDataAdapter Adapter = new SqlDataAdapter(sqlCommand);
            SqlDataAdapter Adapter1 = new SqlDataAdapter(sqlCommand1);
            Adapter.Fill(result);
            Adapter.Dispose();
            Adapter1.Fill(result);
            Adapter1.Dispose();
            //Kết thúc lấy dữ liệu
            return result;
        }
        //6.2. Lưu thuốc sau khi sửa thông tin
        [WebMethod]
        public int LuuThuoc(string mathuoc, string malo, string ten, string tp, string cd, string nsx, string hsd, string slt, string dt, string dongia)
        {
            int i;
            if (nsx == "" && hsd == "")
            {
                SqlCommand cmd = new SqlCommand("update THUOC set TenThuoc = @ten, ThanhPhan = @tp, CongDung = @cd, SoLuongTon = @slt, DangThuoc = @dt, DonGia = @dongia where MaThuoc = @mathuoc", Connection.connection1);
                cmd.Parameters.AddWithValue("@mathuoc", SqlDbType.Char).Value = mathuoc;
                cmd.Parameters.AddWithValue("@malo", SqlDbType.Char).Value = malo;
                cmd.Parameters.AddWithValue("@ten", SqlDbType.NVarChar).Value = ten;
                cmd.Parameters.AddWithValue("@tp", SqlDbType.NVarChar).Value = tp;
                cmd.Parameters.AddWithValue("@cd", SqlDbType.NVarChar).Value = cd;
                cmd.Parameters.AddWithValue("@slt", SqlDbType.Int).Value = Convert.ToInt32(slt);
                cmd.Parameters.AddWithValue("@dt", SqlDbType.NVarChar).Value = dt;
                cmd.Parameters.AddWithValue("@dongia", SqlDbType.Int).Value = Convert.ToInt32(dongia);
                i = Connection.executeQuery(cmd);
            }
            else
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "proc_UpdateThuoc";
                cmd.Parameters.AddWithValue("@mathuoc", SqlDbType.Char).Value = mathuoc;
                cmd.Parameters.AddWithValue("@malo", SqlDbType.Char).Value = malo;
                cmd.Parameters.AddWithValue("@ten", SqlDbType.NVarChar).Value = ten;
                cmd.Parameters.AddWithValue("@tp", SqlDbType.NVarChar).Value = tp;
                cmd.Parameters.AddWithValue("@cd", SqlDbType.NVarChar).Value = cd;
                cmd.Parameters.AddWithValue("@nsx", SqlDbType.Date).Value = nsx;
                cmd.Parameters.AddWithValue("@hsd", SqlDbType.Date).Value = hsd;
                cmd.Parameters.AddWithValue("@slt", SqlDbType.Int).Value = Convert.ToInt32(slt);
                cmd.Parameters.AddWithValue("@dt", SqlDbType.NVarChar).Value = dt;
                cmd.Parameters.AddWithValue("@dongia", SqlDbType.Int).Value = Convert.ToInt32(dongia);
                i = Connection.executeProc(cmd);
            }
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
        public DataTable DanhSachThuoc()
        {
            DataTable result = new DataTable("DS");
            //Lấy dữ liệu
            SqlCommand command = new SqlCommand("select TenThuoc,CongDung,ThanhPhan,DangThuoc,NgaySanXuat,HanSuDung,C1.DonGia,C2.DonGia " +
                "from THUOC T inner join LOTHUOC L on L.MaLoThuoc = T.MaLoThuoc " +
                "inner join CHITIETPHIEUNHAP C1 on C1.MaThuoc = T.MaThuoc " +
                "inner join CHITIETPHIEUXUAT C2 on C2.MaThuoc = T.MaThuoc ", Connection.connection);
            SqlDataAdapter adapter = new SqlDataAdapter(command);
            adapter.Fill(result);
            adapter.Dispose();
            //Kết thúc lấy dữ liệu
            return result;
        }
        [WebMethod]
        public DataTable DanhSachThuocTheoTen(string TenThuoc)
        {
            DataTable result = new DataTable("DS");
            //Lấy dữ liệu
            SqlCommand command = new SqlCommand("select TenThuoc,CongDung,ThanhPhan,DangThuoc,NgaySanXuat,HanSuDung,C1.DonGia,C2.DonGia " +
                "from THUOC T inner join LOTHUOC L on L.MaLoThuoc = T.MaLoThuoc " +
                "inner join CHITIETPHIEUNHAP C1 on C1.MaThuoc = T.MaThuoc " +
                "inner join CHITIETPHIEUXUAT C2 on C2.MaThuoc = T.MaThuoc " +
                "where T.TenThuoc LIKE like '%'+ @ten +'%'", Connection.connection);
            command.Parameters.AddWithValue("@ten", TenThuoc);
            SqlDataAdapter adapter = new SqlDataAdapter(command);
            adapter.Fill(result);
            adapter.Dispose();
            //Kết thúc lấy dữ liệu
            return result;
        }
        [WebMethod]
        public DataTable DanhSachThuocTheoThoiHan(string date1, string date2)
        {
            DataTable result = new DataTable("DS");
            //Lấy dữ liệu
            SqlCommand command = new SqlCommand("select TenThuoc,CongDung,ThanhPhan,DangThuoc,NgaySanXuat,HanSuDung,C1.DonGia,C2.DonGia " +
                "from THUOC T inner join LOTHUOC L on L.MaLoThuoc = T.MaLoThuoc " +
                "inner join CHITIETPHIEUNHAP C1 on C1.MaThuoc = T.MaThuoc " +
                "inner join CHITIETPHIEUXUAT C2 on C2.MaThuoc = T.MaThuoc " +
                "where (NgaySanXuat <= @date1) and (HanSuDung >= @date2)", Connection.connection);
            command.Parameters.AddWithValue("@date1", date1);
            command.Parameters.AddWithValue("@date2", date2);
            SqlDataAdapter adapter = new SqlDataAdapter(command);
            adapter.Fill(result);
            adapter.Dispose();
            //Kết thúc lấy dữ liệu
            return result;
        }
        [WebMethod]
        public DataTable DanhSachThuocTheoTenVaThoiHan(string TenThuoc, string date1, string date2)
        {
            DataTable result = new DataTable("DS");
            //Lấy dữ liệu
            SqlCommand command = new SqlCommand("select TenThuoc,CongDung,ThanhPhan,DangThuoc,NgaySanXuat,HanSuDung,C1.DonGia,C2.DonGia " +
                "from THUOC T inner join LOTHUOC L on L.MaLoThuoc = T.MaLoThuoc " +
                "inner join CHITIETPHIEUNHAP C1 on C1.MaThuoc = T.MaThuoc " +
                "inner join CHITIETPHIEUXUAT C2 on C2.MaThuoc = T.MaThuoc " +
                "where (NgaySanXuat <= @date1) and (HanSuDung >= @date2) and (T.TenThuoc LIKE '%'+ @ten + '%')", Connection.connection);
            command.Parameters.AddWithValue("@ten", TenThuoc);
            command.Parameters.AddWithValue("@date1", date1);
            command.Parameters.AddWithValue("@date2", date2);
            SqlDataAdapter adapter = new SqlDataAdapter(command);
            adapter.Fill(result);
            adapter.Dispose();
            //Kết thúc lấy dữ liệu
            return result;
        }

        //Tùng - Đăng ký
        [WebMethod]
        public void DangKy(string tenDangNhap, string MatKhau)
        {
            //hàm chuyển password thành mã hash
            //Tạo MD5 
            MD5 mh = MD5.Create();
            //Chuyển kiểu chuổi thành kiểu byte
            byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(MatKhau);
            //mã hóa chuỗi đã chuyển
            byte[] hash = mh.ComputeHash(inputBytes);
            //tạo đối tượng StringBuilder (làm việc với kiểu dữ liệu lớn)
            StringBuilder sb = new StringBuilder();

            for (int i = 0; i < hash.Length; i++)
            {
                sb.Append(hash[i].ToString("X2"));
            }
            MatKhau = sb.ToString();

            Random r = new Random();

            //hàm đăng ký
            SqlCommand insert = new SqlCommand("insert into NHANVIEN(MaNhanVien,username,password,IdRole) values('" + r.Next(1, 999999999) + "','" + tenDangNhap + "','" + MatKhau + "','1')");
            Connection.executeQuery(insert);
        }
        //Tùng - Đổi mật khẩu
        [WebMethod]
        public void DoiMatKhau(string tenDangNhap, string MatKhauCu, string MatKhauMoi)
        {
            //hàm chuyển password thành mã hash
            //Tạo MD5 
            MD5 mh = MD5.Create();
            //Chuyển kiểu chuổi thành kiểu byte
            byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(MatKhauCu);
            byte[] inputBytes2 = System.Text.Encoding.ASCII.GetBytes(MatKhauMoi);
            //mã hóa chuỗi đã chuyển
            byte[] hash = mh.ComputeHash(inputBytes);
            byte[] hash2 = mh.ComputeHash(inputBytes2);
            //tạo đối tượng StringBuilder (làm việc với kiểu dữ liệu lớn)
            StringBuilder sb = new StringBuilder();
            StringBuilder sb2 = new StringBuilder();

            for (int i = 0; i < hash.Length; i++)
            {
                sb.Append(hash[i].ToString("X2"));
            }
            MatKhauCu = sb.ToString();

            for (int i = 0; i < hash2.Length; i++)
            {
                sb2.Append(hash2[i].ToString("X2"));
            }
            MatKhauMoi = sb2.ToString();

            //hàm đổi mật khẩu    
            SqlCommand change = new SqlCommand("update NHANVIEN set password = '" + MatKhauMoi + "' where username = '" + tenDangNhap + "' and password = '" + MatKhauCu + "'");
            Connection.executeQuery(change);
        }
        //Tùng - Nhập thuốc
        [WebMethod]
        public int NhapThuoc(string date, string maNCC, string maNV, string SL, string Gia, string HSD, string maThuoc, string maHSX, string NSX, string tenThuoc, string CD, string TP, string dangThuoc, string maloaiThuoc)
        {
            SqlCommand command = new SqlCommand("execute proc_addThuoc '@date' '@mncc' '@mnv' '@sl' '@gia' '@hsd' '@maT' '@mahang' '@nsx' '@ten' '@cd' '@tp' '@dang' '@mLoai'", Connection.connection);

            //button nhập thuốc OK
            command.Parameters.AddWithValue("@date", date);
            command.Parameters.AddWithValue("@mncc", maNCC);
            command.Parameters.AddWithValue("@mnv", maNV);
            command.Parameters.AddWithValue("@sl", SL);
            command.Parameters.AddWithValue("@gia", Gia);
            command.Parameters.AddWithValue("@hsd", HSD);
            command.Parameters.AddWithValue("@maT", maThuoc);
            command.Parameters.AddWithValue("@mahang", maHSX);
            command.Parameters.AddWithValue("@nsx", NSX);
            command.Parameters.AddWithValue("@ten", tenThuoc);
            command.Parameters.AddWithValue("@cd", CD);
            command.Parameters.AddWithValue("@tp", TP);
            command.Parameters.AddWithValue("@dang", dangThuoc);
            command.Parameters.AddWithValue("@mLoai", maloaiThuoc);

            int i = Connection.executeProc(command);
            return i;
        }
        [WebMethod]
        public DataTable TimNCC(string TenNCC)
        {
            DataTable result = new DataTable("DSNCC");
            SqlCommand command = new SqlCommand("execute proc_searchNhaCungCap '@tenNCC'", Connection.connection);

            command.Parameters.AddWithValue("@tenNCC", TenNCC);

            SqlDataAdapter adapter = new SqlDataAdapter(command);
            adapter.Fill(result);
            adapter.Dispose();
            //Kết thúc lấy dữ liệu
            return result;
        }

        [WebMethod]
        public DataTable LoadThongTinNV(string maNV)
        {
            DataTable result = new DataTable("DSNV");
            SqlCommand command = new SqlCommand("execute proc_searchNhanVien '@maNV'", Connection.connection);

            command.Parameters.AddWithValue("@maNV", maNV);

            SqlDataAdapter adapter = new SqlDataAdapter(command);
            adapter.Fill(result);
            adapter.Dispose();
            //Kết thúc lấy dữ liệu
            return result;
        }
        [WebMethod]
        public DataTable LoadThongTinHSX(string tenHSX)
        {
            DataTable result = new DataTable("DSHSX");
            SqlCommand command = new SqlCommand("execute proc_selectMaHangSX '@maNV'", Connection.connection);

            command.Parameters.AddWithValue("@maNV", tenHSX);

            SqlDataAdapter adapter = new SqlDataAdapter(command);
            adapter.Fill(result);
            adapter.Dispose();
            //Kết thúc lấy dữ liệu
            return result;
        }
        [WebMethod]
        public DataTable LoadThongTinLoaiThuoc(string tenLoaiThuoc)
        {
            DataTable result = new DataTable("DSLT");
            SqlCommand command = new SqlCommand("execute proc_selectLoaiThuoc '@tenLoaiThuoc'", Connection.connection);

            command.Parameters.AddWithValue("@tenLoaiThuoc", tenLoaiThuoc);

            SqlDataAdapter adapter = new SqlDataAdapter(command);
            adapter.Fill(result);
            adapter.Dispose();
            //Kết thúc lấy dữ liệu
            return result;
        }

        [WebMethod]
        public DataTable getRole(string username)
        {
            DataTable role = new DataTable("Role");
            SqlCommand command = new SqlCommand("select * from NHANVIEN where username = '" + username + "'", Connection.connection);

            SqlDataAdapter adapter = new SqlDataAdapter(command);
            adapter.Fill(role);
            adapter.Dispose();
            //Kết thúc lấy dữ liệu
            return role;
        }
        //Thống kê phần nhập thuốc
        [WebMethod]
        public DataTable Thongke_ThuocNhap(string str1, string str2)
        {
            DataTable result = new DataTable("DSLT");
            SqlCommand command = new SqlCommand("execute dbo.proc_ThuocNhap '" + str1 + "', '" + str2 + "'", Connection.connection);
            SqlDataAdapter adapter = new SqlDataAdapter(command);
            adapter.Fill(result);
            adapter.Dispose();
            return result;
        }
        //Thống kê thuốc bán ra
        [WebMethod]
        public DataTable Thongke_ThuocBan(string str1, string str2)
        {
            DataTable result = new DataTable("DSLT");
            SqlCommand command = new SqlCommand("execute dbo.proc_ThuocBan '" + str1 + "', '" + str2 + "'", Connection.connection);
            SqlDataAdapter adapter = new SqlDataAdapter(command);
            adapter.Fill(result);
            adapter.Dispose();
            return result;
        }
        //Thống kê số lượng tồn
        [WebMethod]
        public DataTable ThongkeSLT()
        {
            DataTable result = new DataTable("DSLT");
            SqlCommand command = new SqlCommand("execute dbo.proc_ThongKeTon", Connection.connection);
            SqlDataAdapter adapter = new SqlDataAdapter(command);
            adapter.Fill(result);
            adapter.Dispose();
            return result;
        }
        //Thống kê thuốc hết hạn
        [WebMethod]
        public DataTable ThongkeTHH()
        {
            DataTable result = new DataTable("DSLT");
            SqlCommand command = new SqlCommand("execute dbo.proc_ThongKeHetHan", Connection.connection);
            SqlDataAdapter adapter = new SqlDataAdapter(command);
            adapter.Fill(result);
            adapter.Dispose();
            return result;
        }
        //Thống kê doanh thu theo tháng
        [WebMethod]
        public DataTable TKDoanhThuMY(string date1)
        {
            DataTable result = new DataTable("DT");
            SqlCommand command = new SqlCommand("exec dbo.DTThang '" + date1 + "'", Connection.connection);
            SqlDataAdapter adapter = new SqlDataAdapter(command);
            adapter.Fill(result);
            adapter.Dispose();
            //Kết thúc lấy dữ liệu
            return result;
        }
        //Thống kê doanh thu theo năm
        [WebMethod]
        public DataTable TKDoanhThuY(string date2)
        {
            DataTable result = new DataTable("DT");
            SqlCommand command = new SqlCommand("exec dbo.DTNam '" + date2 + "'", Connection.connection);
            SqlDataAdapter adapter = new SqlDataAdapter(command);
            adapter.Fill(result);
            adapter.Dispose();
            //Kết thúc lấy dữ liệu
            return result;
        }
        //Thống kê doanh thu 
        [WebMethod]
        public DataTable TKDoanhThu()
        {
            DataTable result = new DataTable("DT");
            SqlCommand command = new SqlCommand("execute dbo.DTThang0", Connection.connection);
            SqlDataAdapter adapter = new SqlDataAdapter(command);
            adapter.Fill(result);
            adapter.Dispose();
            //Kết thúc lấy dữ liệu
            return result;
        }
        //Tìm kiếm khách hàng
        [WebMethod]
        public DataTable TimKiemKH(string cmnd)
        {
            DataTable result = new DataTable("DSKH");
            SqlCommand sqlCommand = new SqlCommand("select * from KHACHHANG where [CMND/TCCCD] like @cmt or SoDienThoai LIKE @cmt ", Connection.connection);
            sqlCommand.Parameters.AddWithValue("@cmt", cmnd);
            SqlDataAdapter Adapter = new SqlDataAdapter(sqlCommand);
            Adapter.Fill(result);
            Adapter.Dispose();
            //Kết thúc lấy dữ liệu
            return result;
        }
        //THêm khách hàng
        [WebMethod]
        public int ThemKH(string ten, string SDT, string cmtnd)
        {
            SqlCommand sqlCommand = new SqlCommand();
            sqlCommand.CommandText = "proc_addKH";
            sqlCommand.Parameters.AddWithValue("@ten", ten);
            sqlCommand.Parameters.AddWithValue("@sdt", SDT);
            sqlCommand.Parameters.AddWithValue("@cmnd", cmtnd);
            int i = Connection.executeProc(sqlCommand);
            return i;
        }
        //Tìm kiếm thuốc
        [WebMethod]
        public DataTable TimKiemThuoc_Ban(string cmnd)
        {
            DataTable result = new DataTable("DSLT");
            SqlCommand command = new SqlCommand("execute proc_searchTenThuoc", Connection.connection);
            SqlDataAdapter adapter = new SqlDataAdapter(command);
            adapter.Fill(result);
            adapter.Dispose();
            //Kết thúc lấy dữ liệu
            return result;
        }
        //Thêm vào Phiếu xuất
        [WebMethod]
        public DataTable ThemPhieuXuat(string date, string makh, string manv, string tong)
        {
            DataTable result = new DataTable("PX");
            SqlCommand sqlPhieuXuat = new SqlCommand("execute proc_addPX @date,@mkh,@mnv,@tong", Connection.connection);
            sqlPhieuXuat.Parameters.AddWithValue("@date", date);
            sqlPhieuXuat.Parameters.AddWithValue("@mkh", makh);
            sqlPhieuXuat.Parameters.AddWithValue("@mnv", manv);
            sqlPhieuXuat.Parameters.AddWithValue("@tong", tong);
            SqlDataAdapter adapter = new SqlDataAdapter(sqlPhieuXuat);
            adapter.Fill(result);
            adapter.Dispose();
            //Kết thúc lấy dữ liệu
            return result;
        }
        [WebMethod]
        public DataTable ThemPhieuXuat1(string date, string makh, string manv, string tong)
        {
            DataTable result = new DataTable("PX");
            SqlCommand sqlPhieuXuat = new SqlCommand("execute proc_addPX @date,@mkh,@mnv,@tong", Connection.connection);
            sqlPhieuXuat.Parameters.AddWithValue("@date", date);
            sqlPhieuXuat.Parameters.AddWithValue("@mkh", makh);
            sqlPhieuXuat.Parameters.AddWithValue("@mnv", manv);
            sqlPhieuXuat.Parameters.AddWithValue("@tong", tong);
            SqlDataAdapter adapter = new SqlDataAdapter(sqlPhieuXuat);
            adapter.Fill(result);
            adapter.Dispose();
            //Kết thúc lấy dữ liệu
            return result;
        }
        //Lấy mã phiếu xuất
        [WebMethod]
        public DataTable LayMaPX(string date, string makh, string manv, string tong)
        {
            DataTable result = new DataTable("PX");
            SqlCommand sqlPhieuXuat = new SqlCommand("SELECT * FROM PHIEUXUAT WHERE NgayXuat = @date and MaKhachHang = @mkh and MaNhanVien = @mnv and Tong = @tong", Connection.connection);
            sqlPhieuXuat.Parameters.AddWithValue("@date", date);
            sqlPhieuXuat.Parameters.AddWithValue("@mkh", makh);
            sqlPhieuXuat.Parameters.AddWithValue("@mnv", manv);
            sqlPhieuXuat.Parameters.AddWithValue("@tong", tong);
            SqlDataAdapter adapter = new SqlDataAdapter(sqlPhieuXuat);
            adapter.Fill(result);
            adapter.Dispose();
            //Kết thúc lấy dữ liệu
            return result;
        }
        //THêm thuốc vào chi tiết phiếu xuất
        [WebMethod]
        public int ThemDonThuoc(string maPX, string maT, string sl, string gia)
        {
            SqlCommand sqlCTPX = new SqlCommand("execute proc_addCTPX @mpx,@maT,@sl,gia", Connection.connection);
            sqlCTPX.Parameters.AddWithValue("@mpx", maPX);
            sqlCTPX.Parameters.AddWithValue("@maT", maT);
            sqlCTPX.Parameters.AddWithValue("@sl", sl);
            sqlCTPX.Parameters.AddWithValue("@gia", gia);
            int i = Connection.executeProc(sqlCTPX);
            return i;
        }
        [WebMethod]
        public DataTable LayThuocDatHang()
        {
            DataTable result = new DataTable("DS");
            SqlCommand sql = new SqlCommand("SELECT * FROM HOADON", Connection.connection1);
            SqlDataAdapter adapter = new SqlDataAdapter(sql);
            adapter.Fill(result);
            adapter.Dispose();
            //Kết thúc lấy dữ liệu
            return result;
        }
        [WebMethod]
        public DataTable LayThuocDaDatHang(string id)
        {
            DataTable result = new DataTable("DS");
            SqlCommand sql = new SqlCommand("SELECT * FROM HOADON,CHITIETHOADON WHERE HOADON.MaHoaDon = CHITIETHOADON.MaHoaDon and HOADON.MaHoaDon = @id", Connection.connection1);
            sql.Parameters.Add("@id", id);
            SqlDataAdapter adapter = new SqlDataAdapter(sql);
            adapter.Fill(result);
            adapter.Dispose();
            //Kết thúc lấy dữ liệu
            return result;
        }

        [WebMethod]
        public DataTable LayThuocDatHangChiTiet(string id)
        {
            DataTable result = new DataTable("DS");
            SqlCommand sql = new SqlCommand("SELECT * FROM CHITIETHOADON WHERE MaHoaDon = @id", Connection.connection1);
            sql.Parameters.AddWithValue("@id", id);
            SqlDataAdapter adapter = new SqlDataAdapter(sql);
            adapter.Fill(result);
            adapter.Dispose();
            //Kết thúc lấy dữ liệu
            return result;
        }

        [WebMethod]
        public void XoaDon(string id)
        {
            Connection.connection1.Open();
            SqlCommand sql = new SqlCommand("DELETE FROM HOADON WHERE HOADON.MaHoaDon = @id", Connection.connection1);
            SqlCommand sql1 = new SqlCommand("DELETE FROM CHITIETHOADON WHERE MaHoaDon = @id", Connection.connection1);
            sql.Parameters.AddWithValue("@id", id);
            sql1.Parameters.AddWithValue("@id", id);
            sql1.ExecuteNonQuery();
            sql.ExecuteNonQuery();
        }
        [WebMethod]
        public List<THUOC> LayDS(string str)
        {
            List<THUOC> dsThuoc = new List<THUOC>();
            if (str == "")
            {
                DataTable result = new DataTable("DS");
                SqlCommand cmd = new SqlCommand("SELECT * FROM THUOC", Connection.connection1);
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                adapter.Fill(result);
                adapter.Dispose();

                for (int i = 0; i < result.Rows.Count; i++)
                {
                    THUOC t = new THUOC();
                    t.MaThuoc = result.Rows[i]["MaThuoc"].ToString();
                    t.TenThuoc = result.Rows[i]["TenThuoc"].ToString();
                    t.CongDung = result.Rows[i]["CongDung"].ToString();
                    t.ThanhPhan = result.Rows[i]["ThanhPhan"].ToString();
                    t.SoLuongTon = Convert.ToInt32(result.Rows[i]["SoLuongTon"].ToString());
                    t.MaNhaCungCap = result.Rows[i]["MaNhaCungCap"].ToString();
                    t.Tien = result.Rows[i]["Tien"].ToString();
                    t.DangThuoc = result.Rows[i]["DangThuoc"].ToString();
                    t.DonGia = Convert.ToInt32(result.Rows[i]["DonGia"].ToString());
                    t.MaHangSX = result.Rows[i]["MaHangSX"].ToString();
                    t.MaLoaiThuoc = result.Rows[i]["MaLoaiThuoc"].ToString();
                    t.UrlImage = result.Rows[i]["UrlImage"].ToString();
                    t.TimKiem = result.Rows[i]["TimKiem"].ToString();
                    t.MaLoThuoc = result.Rows[i]["MaLoThuoc"].ToString();
                    dsThuoc.Add(t);
                }
            }
            else
            {
                DataTable result = new DataTable("DS");
                SqlCommand cmd = new SqlCommand("SELECT * FROM THUOC WHERE MaThuoc LIKE @mt", Connection.connection1);
                cmd.Parameters.AddWithValue("@mt", str);
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                adapter.Fill(result);
                adapter.Dispose();

                for (int i = 0; i < result.Rows.Count; i++)
                {
                    THUOC t = new THUOC();
                    t.MaThuoc = result.Rows[i]["MaThuoc"].ToString();
                    t.TenThuoc = result.Rows[i]["TenThuoc"].ToString();
                    t.CongDung = result.Rows[i]["CongDung"].ToString();
                    t.ThanhPhan = result.Rows[i]["ThanhPhan"].ToString();
                    t.SoLuongTon = Convert.ToInt32(result.Rows[i]["SoLuongTon"].ToString());
                    t.MaNhaCungCap = result.Rows[i]["MaNhaCungCap"].ToString();
                    t.Tien = result.Rows[i]["Tien"].ToString();
                    t.DangThuoc = result.Rows[i]["DangThuoc"].ToString();
                    t.DonGia = Convert.ToInt32(result.Rows[i]["DonGia"].ToString());
                    t.MaHangSX = result.Rows[i]["MaHangSX"].ToString();
                    t.MaLoaiThuoc = result.Rows[i]["MaLoaiThuoc"].ToString();
                    t.UrlImage = result.Rows[i]["UrlImage"].ToString();
                    t.TimKiem = result.Rows[i]["TimKiem"].ToString();
                    t.MaLoThuoc = result.Rows[i]["MaLoThuoc"].ToString();
                    dsThuoc.Add(t);
                }
            }
            return dsThuoc;
        }
        //Login
        [WebMethod]
        public List<KHACHHANG> Login(string email, string password)
        {
            DataTable result = new DataTable("DS");
            SqlCommand cmd = new SqlCommand("SELECT * FROM KHACHHANG WHERE Email = @email AND MatKhau = @password", Connection.connection1);
            cmd.Parameters.AddWithValue("@email", email);
            cmd.Parameters.AddWithValue("@password", password);
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            adapter.Fill(result);
            adapter.Dispose();
            List<KHACHHANG> dskhachang = new List<KHACHHANG>();
            for (int i = 0; i < result.Rows.Count; i++)
            {
                KHACHHANG t = new KHACHHANG();
                t.Email = result.Rows[i]["Email"].ToString();
                t.MaKhachHang = result.Rows[i]["MaKhachHang"].ToString();
                t.MatKhau = result.Rows[i]["MatKhau"].ToString();
                t.TenKhachHang = result.Rows[i]["TenKhachHang"].ToString();                
                dskhachang.Add(t);
            }
            return dskhachang;
        }

        //register
        [WebMethod]
        public int Register(string email,string password, string name, string makh)
        {
            Connection.connection1.Open();
            SqlCommand sqlCommand = new SqlCommand("INSERT INTO KHACHHANG" +
                " VALUES (@makh,@name,@email,@pass)",Connection.connection1);
            sqlCommand.Parameters.AddWithValue("@makh",makh);
            sqlCommand.Parameters.AddWithValue("@name", name);
            sqlCommand.Parameters.AddWithValue("@email", email);
            sqlCommand.Parameters.AddWithValue("@pass", password);
            int i = sqlCommand.ExecuteNonQuery();
            return i;
        }


    }
}
