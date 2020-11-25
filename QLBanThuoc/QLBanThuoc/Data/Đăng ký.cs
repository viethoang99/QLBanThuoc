using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QLBanThuoc.Data
{
    public partial class Đăng_ký : Form
    {
        public Đăng_ký()
        {
            InitializeComponent();
        }

        private void btnTrangChu_Click(object sender, EventArgs e)
        {
            XtraForm1 DangNhap = new XtraForm1();
            DangNhap.Show();
            this.Hide();
        }

        private void btnDangKy_Click(object sender, EventArgs e)
        {
            QL_SR.QLBanThuocServiceSoapClient qlClient = new QL_SR.QLBanThuocServiceSoapClient();

            string TenDangNhap = txbTenDangNhap.Text;
            string MatKhau = txbMatKhau.Text;
            qlClient.DangKy(TenDangNhap, MatKhau);

            //Đăng ký thành công
            MessageBox.Show("Đăng ký thành công.", "Thông báo.");
            XtraForm1 DangNhap = new XtraForm1();
            DangNhap.Show();
            this.Hide();
        }
    }
}
