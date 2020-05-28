using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.Data.SqlClient;
using QLBanThuoc.Data;

namespace QLBanThuoc.frmTimKiem
{
    public partial class TenThuoc : DevExpress.XtraEditors.XtraUserControl
    {
        frmConnection TimKiem = new frmConnection();
        DataTable mainTable = new DataTable();

        public TenThuoc()
        {
            InitializeComponent();
        }

        void loadData()
        {
            //Lấy danh sách thông tin thuốc vào bảng
            SqlDataAdapter search = new SqlDataAdapter("execute dbo.proc_TimKiem", frmConnection.connection);       
            search.Fill(mainTable);
            dgvKetQua.DataSource = mainTable;
        }

        private void TenThuoc_Load(object sender, EventArgs e)
        {
            //load dữ liệu vào bảng
            loadData();
        }

        void search()
        {
            string ten = txbTimKiem.Text;
            string check1 = "select * from THUOC T " +
                "inner join CHITIETPHIEUNHAP C1 on T.MaThuoc = C1.MaThuoc " +
                "inner join CHITIETPHIEUXUAT C2 on T.MaThuoc = C2.MaThuoc " +
                "where T.TenThuoc like '%" + ten + "%'";
            string get = "select TenThuoc,CongDung,ThanhPhan,DangThuoc,NgaySanXuat,HanSuDung,C1.DonGia,C2.DonGia " +
                "from THUOC T inner join LOTHUOC L on T.MaLoThuoc = L.MaLoThuoc " +
                "inner join CHITIETPHIEUNHAP C1 on T.MaThuoc = C1.MaThuoc inner join CHITIETPHIEUXUAT C2 on T.MaThuoc = C2.MaThuoc " +
                "where T.TenThuoc like '%" + ten + "%'";
            //tên thuốc  
            TimKiem.readDatathroughAdapter(check1, mainTable);
            if (mainTable.Rows.Count != 0)
            {
                MessageBox.Show("Có thuốc cần tìm.", "Thông báo.");

                DataTable TimDuoc = new DataTable();
                TimKiem.readDatathroughAdapter(get, TimDuoc);
                dgvKetQua.DataSource = TimDuoc;            
            }
            else
            {
                MessageBox.Show("Không có thuốc cần tìm....", "Thông báo.");
                txbTimKiem.Clear();
                dgvKetQua.Columns.Clear();
            } 
        }

        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            //tìm kiếm tất cả các thông tin có liên quan đến chuỗi tìm kiếm
            if (txbTimKiem.Text == "")
            {
                MessageBox.Show("Vui lòng kiểm tra lại thông tin tìm kiếm.", "Thông báo.");
            }   
            else 
            {
                search();
            }  
        }

        private void dateNSX_ValueChanged(object sender, EventArgs e)
        {
            dateHSD.Value = dateNSX.Value;
            dateHSD.MinDate = dateNSX.Value;
        }

        private void dateHSD_ValueChanged(object sender, EventArgs e)
        {
            dateHSD.MinDate = dateNSX.Value;
            int check = DateTime.Compare(dateHSD.Value, dateNSX.Value);
            if (check < 0)
            {
                MessageBox.Show("Vui lòng kiểm tra lại thời gian tìm kiếm.", "Thông báo.");
            }    
        }
    }
}
