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

namespace QLBanThuoc.frmTimKiem
{
    public partial class TenThuoc : DevExpress.XtraEditors.XtraUserControl
    {
        frmConnection TimKiem = new frmConnection();
        DataTable mainTable = new DataTable();
        DataTable searchTable = new DataTable();

        public TenThuoc()
        {
            InitializeComponent();
        }

        void loadData()
        {
            MessageBox.Show("Vui lòng nhập chính xác tên thuốc cần tìm kiếm.", "Thông báo.");
            //Lấy danh sách thông tin thuốc vào bảng
            SqlDataAdapter search = new SqlDataAdapter("execute dbo.proc_TimKiem", frmConnection.connection);       
            search.Fill(mainTable);
            dgvKetQua.DataSource = mainTable;
        }

        private void TenThuoc_Load(object sender, EventArgs e)
        {
            //load dữ liệu vào bảng
            loadData();
        }

        void search()
        {
            string ten = txbTimKiem.Text;
            string check1 = "execute dbo.proc_TimKiemThuoc2 '" + ten + "'";
            string get = "execute dbo.proc_TimKiemTheoTen '" + ten + "'";
            //tên thuốc  
            mainTable.Clear();
            TimKiem.readDatathroughAdapter(check1, mainTable);
            if (mainTable.Rows.Count != 0)
            {
                TimKiem.readDatathroughAdapter(get, searchTable);
                dgvKetQua.DataSource = searchTable;
                MessageBox.Show("Có thuốc cần tìm.", "Thông báo.");                           
            }
            else
            {
                txbTimKiem.Clear();
                searchTable.Clear();
                MessageBox.Show("Không có thuốc cần tìm....", "Thông báo.");                
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

        void checkTime()
        {
            dateHSD.Value = dateNSX.Value;
            dateHSD.MinDate = dateNSX.Value;
        }

        private void dateNSX_ValueChanged(object sender, EventArgs e)
        {
            checkTime();
        }

        private void dateHSD_ValueChanged(object sender, EventArgs e)
        {
            dateHSD.MinDate = dateNSX.Value;
        }
    }
}
