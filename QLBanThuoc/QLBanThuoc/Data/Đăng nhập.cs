using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.Data.SqlClient;
using QLBanThuoc.Data;
using System.Security.Cryptography;

namespace QLBanThuoc.Data
{
    public partial class XtraForm1 : DevExpress.XtraEditors.XtraForm
    {
        QL_SR.QLBanThuocServiceSoapClient client = new QL_SR.QLBanThuocServiceSoapClient();
        public static string Ten_USER = "";
        public static string Ma_USER ="";
        public XtraForm1()
        {
            InitializeComponent();
        }

        private void Label1_Click(object sender, EventArgs e)
        {

        }        
        private void BtnDangNhap_Click(object sender, EventArgs e)
        {
            Ten_USER = client.getID(txbTenDangNhap.Text, txbMatKhau.Text);
            Ma_USER = client.ma_user();
            if (Ten_USER != "")
                //if (txbTenDangNhap.Text!="")
            {
                MessageBox.Show("Phiên đăng nhập nhân viên: " + Ten_USER);
                Form1 fmain = new Form1();
                fmain.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Tài khoản và mật khẩu không đúng !");
            }
        }

        private void XtraForm1_Load(object sender, EventArgs e)
        {

        }
    }
}