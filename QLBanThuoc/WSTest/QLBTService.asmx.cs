using System;
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
        //static string ConnectionString = @"Data Source=(local)\SQLEXPRESS;Initial Catalog=QLBanThuoc; Integrated Security=True;Connect Timeout=200";
        static string ConnectionString = @"Data Source=WIN7PROX64;Initial Catalog=QLBanThuoc; Integrated Security=True;Connect Timeout=200";

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
            SqlCommand insert = new SqlCommand("insert into NHANVIEN(MaNhanVien,username,password,IdRole) values('" + r.Next(1,999999999) + "','" + tenDangNhap + "','" + MatKhau + "','1')");
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
            SqlCommand command = new SqlCommand("select * from NHANVIEN where username = '" +  username + "'", Connection.connection);
            
            SqlDataAdapter adapter = new SqlDataAdapter(command);
            adapter.Fill(role);
            adapter.Dispose();
            //Kết thúc lấy dữ liệu
            return role;
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
