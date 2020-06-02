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

namespace QLBanThuoc.frmQuanLyThuoc
{
    public partial class frmBanThuoc : DevExpress.XtraEditors.XtraUserControl
    {
        public frmBanThuoc()
        {
            InitializeComponent();
        }
        frmConnection conn = new frmConnection();
     
        private string maKH;
       

        private void button2_Click(object sender, EventArgs e)
        {
            txbCMTND.Text = "";
            txbSDT.Text = "";
            txbTen.Text = "";
        }


   
        private void button3_Click(object sender, EventArgs e)
        {
            string ten = txbTen.Text;
            string cmtnd = txbCMTND.Text;
            string SDT = txbSDT.Text;

            SqlCommand sqlKH = new SqlCommand("proc_searchKH");
            sqlKH.Parameters.AddWithValue("@cmt", txbCMTND.Text);
            DataTable dt = new DataTable();
            conn.readDataProc(sqlKH, dt);
            if (dt.Rows.Count != 0)
            {
                MessageBox.Show("Khách hàng đã được đăng ký rồi", "Thông báo");
                maKH = dt.Rows[0].ItemArray[0].ToString();
            }
            else
            {
                string cmdtext1;
                cmdtext1  = "proc_addKH";
                SqlCommand sqlCommand = new SqlCommand(cmdtext1);
                sqlCommand.Parameters.AddWithValue("@ten", ten);
                sqlCommand.Parameters.AddWithValue("@sdt", SDT);
                sqlCommand.Parameters.AddWithValue("@cmnd", cmtnd);
                conn.executeProc(sqlCommand);
                MessageBox.Show("Đăng ký khách hàng thành công", "Thông báo");
                SqlCommand sqlKHTemp = new SqlCommand("proc_searchKH");
                sqlKHTemp.Parameters.AddWithValue("@cmt", txbCMTND.Text);
                DataTable dtTemp = new DataTable();
                conn.readDataProc(sqlKH, dtTemp);
                maKH = dtTemp.Rows[0].ItemArray[0].ToString();

            }

        }

        private void TinhTongTien()
        {
            if (dataGridViewGioHang.Rows.Count > 1)
            {
                Int32 tongTien = 0;
                for (int i = 0; i < dataGridViewGioHang.Rows.Count - 1; i++)
                {
                    tongTien += Int32.Parse(dataGridViewGioHang.Rows[i].Cells[4].Value.ToString());
                }
                labelTongTien.Text = tongTien.ToString();
            }
            else 
            {
                labelTongTien.Text = "_____________";
            }
        }
        private class Thuoc
        {

        }


        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            var senderGrid = (DataGridView)sender;
            if (senderGrid.Columns[e.ColumnIndex] is DataGridViewButtonColumn && e.RowIndex >= 0)
            {
                senderGrid.Rows.RemoveAt(e.RowIndex);
                for (int i = 0; i < senderGrid.Rows.Count - 1; i++)
                {
                    senderGrid.Rows[i].Cells[0].Value = i + 1;
                }
                MessageBox.Show(dataGridViewGioHang.Rows.Count.ToString());
            }
        }

        private void dataGridView2_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            var senderGrid = (DataGridView)sender;
            if (senderGrid == dataGridViewGioHang)
            {
                if (e.RowIndex >= 0)
                {
                    Int32 sl = Int32.Parse(senderGrid.Rows[e.RowIndex].Cells[2].Value.ToString());
                    Int32 dg = Int32.Parse(senderGrid.Rows[e.RowIndex].Cells[3].Value.ToString());
                    senderGrid.Rows[e.RowIndex].Cells[4].Value = (sl * dg).ToString();
                    TinhTongTien();
                }
            }
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void frmBanThuoc_Load(object sender, EventArgs e)
        {
            DateTime now = DateTime.Today;
            int day = now.Day;
            int month = now.Month;
            int year = now.Year;
            lbDate.Text = day.ToString() + "/" + month.ToString() + "/" + year.ToString();
            
            DataTable dtTemp = new DataTable();
            dtTemp.Clear();
            SqlCommand sqlThuoc = new SqlCommand("proc_searchTenThuoc");
            conn.readDataProc(sqlThuoc, dtTemp);
            for (int i = 0; i < dtTemp.Rows.Count; i++)
            {
                comboBoxTenThuoc.Items.Add(dtTemp.Rows[i].ItemArray[0]);
            }
            tbTenNhanVien.Text = XtraForm1.Ten_USER;
        }

        private void panel4_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            {
                //SqlCommand sqlCommandPX = new SqlCommand();
                //sqlCommandPX.CommandText = "proc_addPX";
                //SqlCommand commandT = new SqlCommand();
                //commandT.CommandText = "select MaKhachHang from KHACHHANG where CMND/TCCCD like " +
                //    "'" + txbCMTND.Text + "'";
                //string makh = conn.executeSelectQuery(commandT).ToString();
                //sqlCommandPX.Parameters.AddWithValue("@date", DateTime.Today);
                //sqlCommandPX.Parameters.AddWithValue("@makh", makh);
                //sqlCommandPX.Parameters.AddWithValue("@manv", comboBoxTenThuoc.SelectedValue.ToString());
                //conn.executeProc(sqlCommandPX);
                //SqlCommand commandMaPH = new SqlCommand();
                //commandMaPH.CommandText = "select MaPhieuXuat from PhieuXuat where NgayXuat like @Ngay" +
                //    " and " +
                //    "MaKhachHang like @makh and MaNhanVien like @manv";
                //commandMaPH.Parameters.AddWithValue("@Ngay", DateTime.Today);
                //commandMaPH.Parameters.AddWithValue("@makh", makh);
                //commandMaPH.Parameters.AddWithValue("@manv", comboBoxTenThuoc.SelectedValue.ToString());
                //string maph = conn.executeSelectQuery(commandMaPH).ToString();
                //for (int i = 0; i < dataGridViewGioHang.Rows.Count; i++)
                //{
                //    SqlCommand commandCTPX = new SqlCommand();
                //    commandCTPX.CommandText = "proc_addCTPX";
                //    commandCTPX.Parameters.AddWithValue("@mpx", maph);
                //    commandCTPX.Parameters.AddWithValue("@maT", dataGridViewGioHang.Rows[i].Cells[0].Value.ToString());
                //    commandCTPX.Parameters.AddWithValue("@sl", dataGridViewGioHang.Rows[i].Cells[2].Value.ToString());
                //    commandCTPX.Parameters.AddWithValue("@gia", dataGridViewGioHang.Rows[i].Cells[3].Value.ToString());
                //    conn.executeProc(commandCTPX);
                //}
            }
           
         
            
            SqlCommand sqlPhieuXuat = new SqlCommand("proc_addPX");
            sqlPhieuXuat.Parameters.AddWithValue("@date", DateTime.Now.ToString());
            sqlPhieuXuat.Parameters.AddWithValue("@mkh", maKH);
            sqlPhieuXuat.Parameters.AddWithValue("@mnv", XtraForm1.Ma_USER);
            sqlPhieuXuat.Parameters.AddWithValue("@tong", labelTongTien.Text);
            DataTable dt = new DataTable();
            conn.readDataProc(sqlPhieuXuat, dt);
            string mapx = dt.Rows[0].ItemArray[0].ToString();
            for (int i = 0; i < dataGridViewGioHang.Rows.Count - 1; i++)
            {

                SqlCommand sqlCTPX = new SqlCommand("proc_addCTPX");
                sqlCTPX.Parameters.AddWithValue("@mpx", mapx);
                sqlCTPX.Parameters.AddWithValue("@maT", dataGridViewGioHang.Rows[i].Cells[0].Value.ToString());
                sqlCTPX.Parameters.AddWithValue("@sl", dataGridViewGioHang.Rows[i].Cells[2].Value.ToString());
                sqlCTPX.Parameters.AddWithValue("@gia", dataGridViewGioHang.Rows[i].Cells[3].Value.ToString());
                conn.executeProc(sqlCTPX);
            }
            MessageBox.Show("Xuất thành công", "Thông báo");
            
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void comboBoxTenThuoc_SelectedIndexChanged(object sender, EventArgs e)
        {
            SqlCommand sqlSelectThuoc = new SqlCommand("proc_searchThuocBan");
            sqlSelectThuoc.Parameters.AddWithValue("@tenThuoc", comboBoxTenThuoc.SelectedItem.ToString());
            DataTable dtThuoc = new DataTable();
            conn.readDataProc(sqlSelectThuoc, dtThuoc);
            tbMaThuoc.Text = dtThuoc.Rows[0].ItemArray[0].ToString();
            tbThanhPhan.Text = dtThuoc.Rows[0].ItemArray[1].ToString();
            tbCongDung.Text = dtThuoc.Rows[0].ItemArray[2].ToString();
            tbSoLuongTon.Text = dtThuoc.Rows[0].ItemArray[3].ToString();
            tbDonGia.Text = dtThuoc.Rows[0].ItemArray[4].ToString();


        }

        private void buttonXoa_Click(object sender, EventArgs e)
        {
            tbMaThuoc.Text = "";
            tbThanhPhan.Text = "";
            tbCongDung.Text = "";
            tbSoLuongTon.Text = "";
            tbDonGia.Text = "";
            comboBoxTenThuoc.Text = "";
            tbSoLuongMua.Text = "";
        }

        private void tbSoLuongMua_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (tbSoLuongMua.Text != "")
                {
                    int slTon = Int16.Parse(tbSoLuongTon.Text);
                    int slMua = Int16.Parse(tbSoLuongMua.Text);
                    if (slMua > slTon)
                    {
                        MessageBox.Show("Không đủ thuốc", "Thông báo");
                        tbSoLuongMua.Text = "0";
                    }
                }
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message.ToString());
            }
        }

        private void buttonThemVaoGio_Click(object sender, EventArgs e)
        {
            int slMua = Int32.Parse(tbSoLuongMua.Text);
            int donGia = Int32.Parse(tbDonGia.Text);
            string tien = (slMua * donGia).ToString();
            dataGridViewGioHang.Rows.Add(tbMaThuoc.Text, comboBoxTenThuoc.SelectedItem.ToString(),
                tbSoLuongMua.Text, tbDonGia.Text, tien);
            TinhTongTien();
        }
    }
}
