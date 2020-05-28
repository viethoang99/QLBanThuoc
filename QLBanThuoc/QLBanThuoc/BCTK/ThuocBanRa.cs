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
using QLBanThuoc.Data;
using System.Data.SqlClient;

namespace QLBanThuoc.BCTK
{
    public partial class ThuocBanRa : DevExpress.XtraEditors.XtraUserControl
    {
        frmConnection Connect = new frmConnection();
        DataTable xuat = new DataTable();

        public ThuocBanRa()
        {
            InitializeComponent();
        }

        private void btnExcel_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Vui lòng kết nốt với máy in trước khi thực hiện thao tác này...", "Thông báo.");
        }

        private void btnBaoCao_Click(object sender, EventArgs e)
        {
            string startDate = date1.Value.ToString().Split(' ')[0].Replace("/", "-");
            string endDate = date2.Value.ToString().Split(' ')[0].Replace("/", "-");

            //xuất ra thông tin cho bảng
            SqlDataAdapter search = new SqlDataAdapter("select MaLoThuoc,TenThuoc,DangThuoc,SoLuong,DonGia,SUM(DonGia*SoLuong) ThanhTien " +
                "from THUOC T, PHIEUXUAT P, CHITIETPHIEUXUAT C " +
                "where T.MaThuoc = C.MaThuoc and P.MaPhieuXuat = C.MaPhieuXuat and (NgayXuat between '" + startDate + "' and '" + endDate + "') " +
                "group by MaLoThuoc, TenThuoc, DangThuoc, SoLuong, DonGia", frmConnection.connection);
            search.Fill(xuat);
            dgvThuocBanRa.DataSource = xuat;
        }

        private void ThuocBanRa_Load(object sender, EventArgs e)
        {
            date2.MinDate = date1.Value;
            date2.Value = date1.Value;
        }

        private void date1_ValueChanged(object sender, EventArgs e)
        {
            date2.MinDate = date1.Value;
            date2.Value = date1.Value;
        }

        private void date2_ValueChanged(object sender, EventArgs e)
        {
            date2.MinDate = date1.Value;
        }
    }
}
