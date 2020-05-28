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

namespace QLBanThuoc.frmTimKiem
{
    public partial class ThapCao : DevExpress.XtraEditors.XtraUserControl
    {
        frmConnection TimKiem = new frmConnection();
        DataTable mainTable = new DataTable();
        DataTable searchTable = new DataTable();

        public ThapCao()
        {
            InitializeComponent();
        }

        void loadData()
        {
            MessageBox.Show("Vui lòng nhập chính xác đơn giá của thuốc cần tìm kiếm.", "Thông báo.");
            //Lấy danh sách thông tin thuốc vào bảng
            SqlDataAdapter search = new SqlDataAdapter("execute dbo.proc_TimKiem", frmConnection.connection);
            search.Fill(mainTable);
            dgvKetQua.DataSource = mainTable;
        }

        private void ThapCao_Load(object sender, EventArgs e)
        {
            //load dữ liệu vào bảng
            loadData();
        }

        void search()
        {
            string giaTien = txbTimKiem.Text.ToString();
            string check1 = "execute dbo.proc_TimKiemThuoc '" + giaTien + "'";
            string get = "execute dbo.proc_TimKiemTheoGia '" + giaTien + "'";
            //đơn giá nhập
            mainTable.Clear();
            TimKiem.readDatathroughAdapter(check1, mainTable);
            if (mainTable.Rows.Count != 0)
            {
                TimKiem.readDatathroughAdapter(get, searchTable);
                dgvKetQua.DataSource = searchTable;
                MessageBox.Show("Có thuốc với giá nhập cần tìm.", "Thông báo.");                
            }
            else
            {
                txbTimKiem.Clear();
                searchTable.Clear();
                MessageBox.Show("Không có thuốc với giá nhập cần tìm....", "Thông báo.");
            }
        }

        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            //tìm kiếm tất cả các thông tin có liên quan đến chuỗi tìm kiếm
            if (txbTimKiem.Text == "")
            {
                MessageBox.Show("Vui lòng kiểm tra lại thông tin tìm kiếm.", "Thông báo.");
            }
            else
            {
                search();
            }
        }
    }
}
