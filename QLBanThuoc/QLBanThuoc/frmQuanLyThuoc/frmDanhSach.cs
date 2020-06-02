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
            MessageBox.Show("cell click "+ dataGridView1.Rows[i].Cells[1].Value.ToString());
            int i = e.RowIndex;
            index = e.RowIndex;
            if (i >= 0)
            {
                maThuoc = dataGridView1.Rows[i].Cells[1].Value.ToString();
                MessageBox.Show(maThuoc);
            }
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
            try
            {

                SqlCommand sqlCommand = new SqlCommand("proc_searchThuocSua");
                sqlCommand.Parameters.AddWithValue("@maT", maThuoc);
                DataTable dtTemp = new DataTable();
                frmConnection.readDataProc(sqlCommand, dtTemp);
                textBoxTenThuoc.Text = dtTemp.Rows[0].ItemArray[0].ToString();
                textBoxLoThuoc.Text = dtTemp.Rows[0].ItemArray[1].ToString();
                textBoxLoaiThuoc.Text = dtTemp.Rows[0].ItemArray[2].ToString();
                textBoxHangSX.Text = dtTemp.Rows[0].ItemArray[3].ToString();
                textBoxThanhPhanChinh.Text = dtTemp.Rows[0].ItemArray[4].ToString();
                textBoxCongDung.Text = dtTemp.Rows[0].ItemArray[5].ToString();
                textBoxNgaySX.Text = dtTemp.Rows[0].ItemArray[6].ToString();
                textBoxHanSD.Text = dtTemp.Rows[0].ItemArray[7].ToString();
                textBoxGiaBan.Text = dtTemp.Rows[0].ItemArray[8].ToString();
                textBoxSoLuongTon.Text = dtTemp.Rows[0].ItemArray[9].ToString();
                textBoxDonVi.Text = dtTemp.Rows[0].ItemArray[10].ToString();
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message.ToString());
            }



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
                SqlCommand cmd = new SqlCommand("proc_searchThuocDanhSachDGV");
                cmd.Parameters.AddWithValue("@ten", textBoxTimKiem.Text);
                frmConnection.readDataProc(cmd, dt);
                
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

        private void textBoxTimKiem_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBoxTimKiem_Click(object sender, EventArgs e)
        {
            if(textBoxTimKiem.Text == "Thông tin tìm kiếm")
            {
                textBoxTimKiem.Text = "";
            }

        }
    }
}
