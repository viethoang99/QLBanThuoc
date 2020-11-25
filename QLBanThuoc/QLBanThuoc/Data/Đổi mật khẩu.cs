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
    public partial class Đổi_mật_khẩu : Form
    {
        public Đổi_mật_khẩu()
        {
            InitializeComponent();
        }

        private void btnTrangChu_Click(object sender, EventArgs e)
        {
            Form1 frmMain = new Form1();
            frmMain.Show();
            this.Hide();
        }

        private void btnDoiMK_Click(object sender, EventArgs e)
        {
            //cập nhật thông tin người dùng
            if (txbMatKhau0.Text != XtraForm1.matkhaucu)
            {
                MessageBox.Show("Vui lòng kiểm tra lại mật khẩu cũ.", "Thông báo.");
            }
            else
            {
                if (txbMatKhau2.Text != txbMatKhau1.Text)
                {
                    MessageBox.Show("Vui lòng kiểm tra lại mật khẩu mới.", "Thông báo.");
                }
                else
                {
                    if (txbMatKhau1.Text == txbMatKhau0.Text)
                    {
                        MessageBox.Show("Mật khẩu mới phải khác mật khẩu cũ.", "Thông báo.");
                    }
                    else
                    {
                        //Hàm thay đổi mật khẩu
                        MessageBox.Show("Thay đổi mật khẩu thành công.", "Thông báo.");
                        XtraForm1 DangNhap = new XtraForm1();
                        DangNhap.Show();
                        this.Hide();
                    }
                }
            }
        }

        private void Đổi_mật_khẩu_Load(object sender, EventArgs e)
        {
            labelTenDangNhap.Text = XtraForm1.tendangnhap;
        }
    }
}
