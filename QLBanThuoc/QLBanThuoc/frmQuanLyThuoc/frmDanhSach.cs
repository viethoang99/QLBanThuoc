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

namespace QLBanThuoc.frmQuanLyThuoc
{
    public partial class frmDanhSach : DevExpress.XtraEditors.XtraUserControl
    {
        public frmDanhSach()
        {
            InitializeComponent();
        }
        DataTable dt = new DataTable();
        frmConnection frmConnection = new frmConnection();
        private string maThuoc;
        private int index;
        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void dgvCellClick_Click(object sender, DataGridViewCellEventArgs e)
        {
            int i = e.RowIndex;
            index = e.RowIndex;
            maThuoc = dataGridView1.Rows[i].Cells[0].Value.ToString();
        }
        private void buttonLuu_Click(object sender, EventArgs e)
        {
            frmConnection.createConn();
            SqlCommand cmd = new SqlCommand("proc_UpdateThuoc", frmConnection.connection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@mathuoc", SqlDbType.Char).Value = maThuoc;
            cmd.Parameters.AddWithValue("@malo", SqlDbType.Char).Value = textBoxLoThuoc.Text;
            cmd.Parameters.AddWithValue("@ten", SqlDbType.NVarChar).Value = textBoxTenThuoc.Text;
            cmd.Parameters.AddWithValue("@tp", SqlDbType.NVarChar).Value = textBoxThanhPhanChinh.Text;
            cmd.Parameters.AddWithValue("@cd", SqlDbType.NVarChar).Value = textBoxCongDung.Text;
            cmd.Parameters.AddWithValue("@nsx", SqlDbType.Date).Value = textBoxNgaySX.Text;
            cmd.Parameters.AddWithValue("@hsd", SqlDbType.Date).Value = textBoxHanSD.Text;
            cmd.Parameters.AddWithValue("@slt", SqlDbType.Int).Value = Convert.ToInt32(textBoxSoLuongTon.Text);
            cmd.Parameters.AddWithValue("@dt", SqlDbType.NVarChar).Value = textBoxDonVi.Text;
            cmd.ExecuteNonQuery();
            frmConnection.connection.Close();

            MessageBox.Show("Cập nhật thành công ! ", "Thông báo");


        }

        private void buttonHuy_Click(object sender, EventArgs e)
        {
            textBoxCongDung.Text = "";
            textBoxDonVi.Text = "";
            textBoxGiaBan.Text = "";
            textBoxHangSX.Text = "";
            textBoxHanSD.Text = "";
            textBoxLoaiThuoc.Text = "";
            textBoxLoThuoc.Text = "";
            textBoxNgaySX.Text = "";
            textBoxSoLuongTon.Text = "";
            textBoxTenThuoc.Text = "";
            textBoxThanhPhanChinh.Text = "";   
        }

        private void buttonSua_Click(object sender, EventArgs e)
        {
            maThuoc = dataGridView1.CurrentRow.Cells[1].Value.ToString();
            textBoxTenThuoc.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
            textBoxLoThuoc.Text = dataGridView1.CurrentRow.Cells[3].Value.ToString();
            textBoxLoaiThuoc.Text = dataGridView1.CurrentRow.Cells[4].Value.ToString();
            textBoxHangSX.Text = dataGridView1.CurrentRow.Cells[5].Value.ToString();
            textBoxThanhPhanChinh.Text = dataGridView1.CurrentRow.Cells[6].Value.ToString();
            textBoxCongDung.Text = dataGridView1.CurrentRow.Cells[7].Value.ToString();
            textBoxNgaySX.Text = dataGridView1.CurrentRow.Cells[8].Value.ToString();
            textBoxHanSD.Text = dataGridView1.CurrentRow.Cells[9].Value.ToString();
            textBoxGiaBan.Text = dataGridView1.CurrentRow.Cells[9].Value.ToString();
            textBoxSoLuongTon.Text = dataGridView1.CurrentRow.Cells[10].Value.ToString();
            textBoxDonVi.Text = dataGridView1.CurrentRow.Cells[11].Value.ToString();




        }

        private void buttonXoa_Click(object sender, EventArgs e)
        {
            maThuoc = dataGridView1.CurrentRow.Cells[1].Value.ToString();
            frmConnection.createConn();
            SqlCommand cmd = new SqlCommand("proc_deleteThuoc", frmConnection.connection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@ma", SqlDbType.Char).Value = maThuoc;
            cmd.ExecuteNonQuery();

            MessageBox.Show("Đã xóa thành công", "Thông báo");
            dataGridView1.Rows.RemoveAt(index);
        }
        
        private void buttonTimKiem_Click(object sender, EventArgs e)
        {
            dt.Rows.Clear();
            if (textBoxTimKiem.Text != "")
            {
                SqlCommand cmd = new SqlCommand("proc_SearchThuoc", frmConnection.connection);
                SqlDataAdapter adt = new SqlDataAdapter(cmd);
                adt.SelectCommand.CommandType = CommandType.StoredProcedure;
                adt.SelectCommand.Parameters.Add(new SqlParameter("@s", textBoxTimKiem.Text));
                adt.Fill(dt);
            }
            else
            {
                MessageBox.Show("Nhập thông tin tìm kiếm", "Thông báo");
                return;
            }

            if (dt.Rows.Count != 0)
            {
                dataGridView1.DataSource = dt;
            }
            else
            {
                MessageBox.Show("Không tìm thấy kết quả", "Thông báo");
            }

        }

    }
}
