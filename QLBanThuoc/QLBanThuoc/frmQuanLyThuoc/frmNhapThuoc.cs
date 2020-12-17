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
            QL_SR.QLBanThuocServiceSoapClient client = new QL_SR.QLBanThuocServiceSoapClient();

            DataTable DSNV = client.LoadThongTinNV(XtraForm1.Ma_USER);

            lbMaNhanVien.Text = XtraForm1.Ma_USER;
            lbTenNhanVien.Text = DSNV.Rows[0].ItemArray[0].ToString();
            labelSDT.Text = DSNV.Rows[0].ItemArray[1].ToString();

            //loadData();
        }

        //void loadData()
        //{
        //    SqlCommand sql = new SqlCommand();
        //    sql.CommandText = "proc_searchNhanVien";
        //    sql.Parameters.AddWithValue("@maNV", XtraForm1.Ma_USER);
        //    DataTable dataTemp = new DataTable();
        //    frmconnection.readDataProc(sql, dataTemp);

        //    lbMaNhanVien.Text = XtraForm1.Ma_USER;
        //    lbTenNhanVien.Text = dataTemp.Rows[0].ItemArray[0].ToString();
        //    labelSDT.Text = dataTemp.Rows[0].ItemArray[1].ToString();
        //    //Load thông tin tất cả các nhà cung cấp > tạo DataTable TenNCC
        //    SqlCommand cmNhaCungCap = new SqlCommand("proc_searchTenNhaCungCap");
        //    DataTable dtNhaCungCap = new DataTable();
        //    frmconnection.readDataProc(cmNhaCungCap, dtNhaCungCap);
        //    for (int i = 0; i < dtNhaCungCap.Rows.Count; i++)
        //    {
        //        comboTenNCC.Items.Add(dtNhaCungCap.Rows[i].ItemArray[0].ToString());
        //    }
        //    dtNhaCungCap.Clear();
        //    //

        //    //Load thông tin tất cả các hãng sản xuất > tạo DataTable HSX
        //    SqlCommand cmHangSX = new SqlCommand("proc_searchHangSanXuat");
        //    DataTable dtHangSX = new DataTable();
        //    frmconnection.readDataProc(cmHangSX, dtHangSX);
        //    for (int i = 0; i < dtHangSX.Rows.Count; i++)
        //    {
        //        comboBoxHangSanXuat.Items.Add(dtHangSX.Rows[i].ItemArray[0].ToString());
        //    }
        //    dtHangSX.Clear();
        //    //

        //    //Load thông tin tất cả các loại thuốc > tạo DataTable LoaiThuoc
        //    SqlCommand cmLoaiThuoc = new SqlCommand("proc_searchLoaiThuoc");
        //    DataTable dtLoaiThuoc = new DataTable();
        //    frmconnection.readDataProc(cmLoaiThuoc, dtLoaiThuoc);
        //    for (int i = 0; i < dtLoaiThuoc.Rows.Count; i++)
        //    {
        //        comboBoxLoaiThuoc.Items.Add(dtLoaiThuoc.Rows[i].ItemArray[0].ToString());
        //    }
        //    dtLoaiThuoc.Clear();
        //    //
        //}

        private void btnNhaCungCapOK_Click(object sender, EventArgs e)
        {
            QL_SR.QLBanThuocServiceSoapClient client = new QL_SR.QLBanThuocServiceSoapClient();

            DataTable DSNCC = client.TimNCC(comboTenNCC.Text);

            labelMaNhaCungCap.Text = DSNCC.Rows[0].ItemArray[0].ToString();
            labelThongTinDaiDien.Text = DSNCC.Rows[0].ItemArray[1].ToString();
        }

        private void btnNhaCungCapHuy_Click(object sender, EventArgs e)
        {
            comboTenNCC.Text = "";
            labelMaNhaCungCap.Text = "________________________";
            labelThongTinDaiDien.Text = "________________________";
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

        private int checkThongTin()
        {
            if (tbSoLuong.Text == "" || tbDonGia.Text == "" || tbMaThuoc.Text == "" ||
                tbMaThuoc.Text == "" || tbTenThuoc.Text == "" || tbCongDung.Text == "" ||
                tbThanhPhan.Text == "" || tbDangThuoc.Text == "")
            {
                return 0;
            }
            else
                return 1;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            QL_SR.QLBanThuocServiceSoapClient client = new QL_SR.QLBanThuocServiceSoapClient();

            int i = client.NhapThuoc(DateTime.Now.ToString(), labelMaNhaCungCap.Text, XtraForm1.Ma_USER, tbSoLuong.Text, tbDonGia.Text, dtpHanSD.Value.ToString(), tbMaThuoc.Text, maHangSanXuat, dtpNgaySX.Value.ToString(), tbTenThuoc.Text, tbCongDung.Text, tbThanhPhan.Text, tbDangThuoc.Text, maLoaiThuoc);

            MessageBox.Show(i.ToString() + " dòng bị ảnh hưởng", "thông báo");
            int check = checkThongTin();
            if (check == 0)
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin.", "Thông báo.");
            }
            else
            {
                int k = client.NhapThuoc(DateTime.Now.ToString(), labelMaNhaCungCap.Text, XtraForm1.Ma_USER, tbSoLuong.Text, tbDonGia.Text, dtpHanSD.Value.ToString(), tbMaThuoc.Text, maHangSanXuat, dtpNgaySX.Value.ToString(), tbTenThuoc.Text, tbCongDung.Text, tbThanhPhan.Text, tbDangThuoc.Text, maLoaiThuoc);
                if (k != 0)
                {
                    MessageBox.Show("Nhập thuốc thành công", "Thông báo.", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Vui lòng thử lại.", "Thông báo.", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void comboBoxHangSanXuat_SelectedIndexChanged(object sender, EventArgs e)
        {
            QL_SR.QLBanThuocServiceSoapClient client = new QL_SR.QLBanThuocServiceSoapClient();

            DataTable DSHSX = client.LoadThongTinHSX(comboBoxHangSanXuat.SelectedItem.ToString());

            maHangSanXuat = DSHSX.Rows[0].ItemArray[0].ToString();
        }

        private void comboBoxLoaiThuoc_SelectedIndexChanged(object sender, EventArgs e)
        {
            QL_SR.QLBanThuocServiceSoapClient client = new QL_SR.QLBanThuocServiceSoapClient();

            DataTable DSLT = client.LoadThongTinLoaiThuoc(comboBoxLoaiThuoc.SelectedItem.ToString());

            maLoaiThuoc = DSLT.Rows[0].ItemArray[0].ToString();
        }

        private void comboTenNCC_SelectedIndexChanged(object sender, EventArgs e)
        {
            QL_SR.QLBanThuocServiceSoapClient client = new QL_SR.QLBanThuocServiceSoapClient();

            DataTable DSNCC = client.TimNCC(comboTenNCC.SelectedItem.ToString());

            labelMaNhaCungCap.Text = DSNCC.Rows[0].ItemArray[0].ToString();
            labelThongTinDaiDien.Text = DSNCC.Rows[0].ItemArray[1].ToString();
        }
    }
}
