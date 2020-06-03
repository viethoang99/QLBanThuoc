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

namespace QLBanThuoc.frmQuanLyNguoiDung
{
    public partial class frmNhapThuoc : DevExpress.XtraEditors.XtraUserControl
    {
        public frmNhapThuoc()
        {
            InitializeComponent();
        }

        frmConnection frmconnection = new frmConnection();

        private string maHangSanXuat;
        private string maLoaiThuoc;
  
        private void comboBoxTenNCC_ItemChange(object sender, EventArgs e)
        {
            
        }
        private void frmNhapThuoc_Load(object sender, EventArgs e)
        {
            SqlCommand sql = new SqlCommand();
            sql.CommandText = "proc_searchNhanVien";
            sql.Parameters.AddWithValue("@maNV", XtraForm1.Ma_USER);
            DataTable dataTemp = new DataTable();
            frmconnection.readDataProc(sql, dataTemp);
            lbMaNhanVien.Text = XtraForm1.Ma_USER;
            lbTenNhanVien.Text = dataTemp.Rows[0].ItemArray[0].ToString();
            labelSDT.Text = dataTemp.Rows[0].ItemArray[1].ToString();

            SqlCommand cmNhaCungCap = new SqlCommand("proc_searchTenNhaCungCap");
        
            DataTable dtNhaCungCap = new DataTable();
            frmconnection.readDataProc(cmNhaCungCap, dtNhaCungCap);
            for (int i = 0; i < dtNhaCungCap.Rows.Count; i++)
            {
                comboTenNCC.Items.Add(dtNhaCungCap.Rows[i].ItemArray[0].ToString());
            }
            dtNhaCungCap.Clear();
            SqlCommand cmHangSX = new SqlCommand("proc_searchHangSanXuat");
            DataTable dtHangSX = new DataTable();
            frmconnection.readDataProc(cmHangSX, dtHangSX);
            for (int i = 0; i < dtHangSX.Rows.Count; i++)
            {
                comboBoxHangSanXuat.Items.Add(dtHangSX.Rows[i].ItemArray[0].ToString());
            }
            dtHangSX.Clear();
            SqlCommand cmLoaiThuoc = new SqlCommand("proc_searchLoaiThuoc");
            DataTable dtLoaiThuoc = new DataTable();
            frmconnection.readDataProc(cmLoaiThuoc, dtLoaiThuoc);
            for (int i = 0; i < dtLoaiThuoc.Rows.Count; i++)
            {
                comboBoxLoaiThuoc.Items.Add(dtLoaiThuoc.Rows[i].ItemArray[0].ToString());
            }
            dtLoaiThuoc.Clear();
        }

        private void buttonThemVaoKho_Click(object sender, EventArgs e)
        {

        }

        private void btnThemVaoGio_Click(object sender, EventArgs e)
        {

        }

        private void groupBox3_Enter(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

 

        private void comboTenNCC_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void btnNhaCungCapOK_Click(object sender, EventArgs e)
        {
            SqlCommand sql = new SqlCommand();
            sql.CommandText = "proc_searchNhaCungCap";
            sql.Parameters.AddWithValue("@tenNCC", comboTenNCC.SelectedItem.ToString());
            DataTable dataTemp = new DataTable();
            frmconnection.readDataProc(sql, dataTemp);
            labelMaNhaCungCap.Text = dataTemp.Rows[0].ItemArray[0].ToString();
            labelThongTinDaiDien.Text = dataTemp.Rows[0].ItemArray[1].ToString();
        }

        private void btnNhaCungCapHuy_Click(object sender, EventArgs e)
        {
            comboTenNCC.Text = "";
            labelMaNhaCungCap.Text = "________________________";
            labelThongTinDaiDien.Text = "________________________";
        }

        private void button2_Click(object sender, EventArgs e)
        {
            SqlCommand sql = new SqlCommand();
            sql.CommandText = "proc_searchNhanVien";
            sql.Parameters.AddWithValue("@maNV", lbMaNhanVien.Text.ToString());
            DataTable dataTemp = new DataTable();
            frmconnection.readDataProc(sql, dataTemp);
            lbMaNhanVien.Text = dataTemp.Rows[0].ItemArray[0].ToString();
            labelSDT.Text = dataTemp.Rows[0].ItemArray[1].ToString();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            tbTenThuoc.Text = "";
            tbMaThuoc.Text = "";
            tbTenLo.Text = "";
            dtpHanSD.Value = DateTime.Now;
            dtpNgaySX.Value = DateTime.Now;
            comboBoxHangSanXuat.Text = "";
            tbThanhPhan.Text = "";
            tbCongDung.Text = "";
            tbDangThuoc.Text = "";
            tbSoLuong.Text = "";
            tbDonGia.Text = "";
            lbTongTien.Text = "__________________";
        }

        private void button4_Click(object sender, EventArgs e)
        {
            SqlCommand sqlThuoc = new SqlCommand("proc_addThuoc");
            sqlThuoc.Parameters.AddWithValue("@date", DateTime.Now.ToString());
            sqlThuoc.Parameters.AddWithValue("@mncc", labelMaNhaCungCap.Text);
            sqlThuoc.Parameters.AddWithValue("@mnv", XtraForm1.Ma_USER);
            sqlThuoc.Parameters.AddWithValue("@sl", tbSoLuong.Text);
            sqlThuoc.Parameters.AddWithValue("@gia", tbDonGia.Text);
            sqlThuoc.Parameters.AddWithValue("@hsd", dtpHanSD.Value.ToString());
            sqlThuoc.Parameters.AddWithValue("@maT", tbMaThuoc.Text);
            sqlThuoc.Parameters.AddWithValue("@mahang",maHangSanXuat);
            sqlThuoc.Parameters.AddWithValue("@nsx", dtpNgaySX.Value.ToString());
            sqlThuoc.Parameters.AddWithValue("@ten", tbTenThuoc.Text);
            sqlThuoc.Parameters.AddWithValue("@cd", tbCongDung.Text);
            sqlThuoc.Parameters.AddWithValue("@tp", tbThanhPhan.Text);
            sqlThuoc.Parameters.AddWithValue("@dang", tbDangThuoc.Text);
            sqlThuoc.Parameters.AddWithValue("@mLoai", maLoaiThuoc);

            int i = frmconnection.executeProc(sqlThuoc);
            MessageBox.Show(i.ToString() + " dòng bị ảnh hưởng", "thông báo");
        }

        private void comboBoxHangSanXuat_SelectedIndexChanged(object sender, EventArgs e)
        {
            SqlCommand sqlCommand = new SqlCommand("proc_selectMaHangSX");
            sqlCommand.Parameters.AddWithValue("@tenHSX", comboBoxHangSanXuat.SelectedItem.ToString());
            DataTable dt = new DataTable();
            frmconnection.readDataProc(sqlCommand, dt);
            maHangSanXuat = dt.Rows[0].ItemArray[0].ToString();
        }

        private void comboBoxLoaiThuoc_SelectedIndexChanged(object sender, EventArgs e)
        {
            SqlCommand sqlCommand = new SqlCommand("proc_selectLoaiThuoc");
            sqlCommand.Parameters.AddWithValue("@tenLoaiThuoc", comboBoxLoaiThuoc.SelectedItem.ToString());
            DataTable dt = new DataTable();
            frmconnection.readDataProc(sqlCommand, dt);
            maLoaiThuoc = dt.Rows[0].ItemArray[0].ToString();
        }
    }
}
