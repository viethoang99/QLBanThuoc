using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using QLBanThuoc.Data;

namespace QLBanThuoc.frmNhaCungCap
{
    public partial class NhaCungCap : UserControl
    {
        public NhaCungCap()
        {
            InitializeComponent();
        }

        frmConnection connection = new frmConnection();

        private void SimpleButton1_Click(object sender, EventArgs e)
        {
            SqlCommand sqlCommand = new SqlCommand("proc_searchNCC");
            sqlCommand.Parameters.AddWithValue("@ten", textBox6.Text);
            //SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM NHACUNGCAP",frmConnection.connection);
            DataTable dt = new DataTable();
            connection.readDataProc(sqlCommand, dt);
            dataGridView1.DataSource = dt;
        }

        private void groupControl2_Paint(object sender, PaintEventArgs e)
        {

        }

        private int checkNullTextbox()
        {
            if (textBoxMaNhaCungCap.Text == "" || textBoxTenNhaCungCap.Text == "" ||
               textBoxNguoiDaiDien.Text == "" || textBoxSoDienThoai.Text == "" || textBoxDiaChi.Text == "")
            {
                return 0;
            }
            else
                return 1;
        }
        private void ButtonThemMoi_Click(object sender, EventArgs e)
        {
            int iCheck = checkNullTextbox();
            if (iCheck == 0)
            {
                MessageBox.Show("Hãy nhập đủ thông tin", "Thông báo");
            }
            else
            {
                SqlCommand sqlCommand = new SqlCommand();
                sqlCommand.CommandText = "proc_addNhaCungCap";
                sqlCommand.Parameters.AddWithValue("@MaNCC", textBoxMaNhaCungCap.Text);
                sqlCommand.Parameters.AddWithValue("@TenNCC", textBoxTenNhaCungCap.Text);
                sqlCommand.Parameters.AddWithValue("@NguoiDaiDien", textBoxNguoiDaiDien.Text);
                sqlCommand.Parameters.AddWithValue("@SoDienThoai", textBoxSoDienThoai.Text);
                sqlCommand.Parameters.AddWithValue("@DiaChi", textBoxDiaChi.Text);
                int i = connection.executeProc(sqlCommand);
                if (i!= 0)
                {
                    MessageBox.Show("Thêm thành công", "Thông báo");
                }
            }
        }

        private string MaNCC;
        private string TenNCC;
        private string NguoiDaiDien;
        private string SoDienThoai;
        private string DiaChi;
        private int iRowIndex;
        private void dgvCellClick_Click(object sender, DataGridViewCellEventArgs e)
        {
            int i = e.RowIndex;
            iRowIndex = e.RowIndex;
            MaNCC = dataGridView1.Rows[i].Cells[0].Value.ToString();
            TenNCC = dataGridView1.Rows[i].Cells[1].Value.ToString();
            NguoiDaiDien = dataGridView1.Rows[i].Cells[2].Value.ToString();
            DiaChi = dataGridView1.Rows[i].Cells[3].Value.ToString();
            SoDienThoai = dataGridView1.Rows[i].Cells[4].Value.ToString();
        }

        private void ButtonSua_Click(object sender, EventArgs e)
        {
            textBoxMaNhaCungCap.Text = MaNCC;
            textBoxDiaChi.Text = DiaChi;
            textBoxSoDienThoai.Text = SoDienThoai;
            textBoxNguoiDaiDien.Text = NguoiDaiDien;
            textBoxTenNhaCungCap.Text = TenNCC;
            textBoxMaNhaCungCap.ReadOnly = true;

        }

        private void ButtonLuu_Click(object sender, EventArgs e)
        {
            int iCheck = checkNullTextbox();
            if (iCheck == 0)
            {
                MessageBox.Show("Hãy nhập đủ thông tin", "Thông báo");
            }
            else
            {
                SqlCommand sqlCommand = new SqlCommand();
                sqlCommand.CommandText = "proc_updateNhaCungCap";
                sqlCommand.Parameters.AddWithValue("@MaNCC", textBoxMaNhaCungCap.Text);
                sqlCommand.Parameters.AddWithValue("@TenNCC", textBoxTenNhaCungCap.Text);
                sqlCommand.Parameters.AddWithValue("@NguoiDaiDien", textBoxNguoiDaiDien.Text);
                sqlCommand.Parameters.AddWithValue("@SoDienThoai", textBoxSoDienThoai.Text);
                sqlCommand.Parameters.AddWithValue("@DiaChi", textBoxDiaChi.Text);
                int i = connection.executeProc(sqlCommand);
                if (i != 0)
                {
                    MessageBox.Show("Update thành công", "Thông báo");
                }
                textBoxMaNhaCungCap.Text = "";
                textBoxTenNhaCungCap.Text = "";
                textBoxNguoiDaiDien.Text = "";
                textBoxDiaChi.Text = "";
                textBoxSoDienThoai.Text = "";
                textBoxMaNhaCungCap.ReadOnly = false;
            }

        }

        private void ButtonXoa_Click(object sender, EventArgs e)
        {   
            SqlCommand sqlCommand = new SqlCommand();
            sqlCommand.CommandText = "proc_deleteNhaCungCap";
            sqlCommand.Parameters.AddWithValue("@MaNCC", textBoxMaNhaCungCap.Text);
            int i = connection.executeProc(sqlCommand);
            if (i != 0)
            {
                MessageBox.Show("Xóa thành công", "Thông báo");
                dataGridView1.Rows.RemoveAt(iRowIndex);
            }
            else
            {
                MessageBox.Show("Xóa thất bại", "Thông báo");
            }
            
        }

        private void ButtonHuy_Click(object sender, EventArgs e)
        {
            textBoxMaNhaCungCap.Text = "";
            textBoxTenNhaCungCap.Text = "";
            textBoxNguoiDaiDien.Text = "";
            textBoxDiaChi.Text = "";
            textBoxSoDienThoai.Text = "";
            textBoxMaNhaCungCap.ReadOnly = false;
        }







    }
}
