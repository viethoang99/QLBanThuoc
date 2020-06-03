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
            MessageBox.Show("Vui lòng nhập chính xác tên / thời hạn / tên và thời hạn của thuốc cần tìm kiếm.", "Thông báo.");
            //Lấy danh sách thông tin thuốc vào bảng
            SqlDataAdapter search = new SqlDataAdapter("execute dbo.proc_TimKiem", frmConnection.connection);       
            search.Fill(mainTable);
            dgvKetQua.DataSource = mainTable;
        }

        private void TenThuoc_Load(object sender, EventArgs e)
        {
            //load dữ liệu vào bảng
            loadData();
            checkTime();
            dateNSX.Value = dateHSD.Value = DateTime.Now;
        }

        void searchTheoTen()
        {
            string ten = txbTimKiem.Text;
            string get = "execute dbo.proc_TimKiemTheoTen '" + ten + "'";
            //tên thuốc  
            mainTable.Clear();
            TimKiem.readDatathroughAdapter(get, mainTable);
            if (mainTable.Rows.Count != 0)
            {
                searchTable.Clear();
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

        void searchTheoThoiHan()
        {
            string date1 = dateNSX.Value.ToString().Split(' ')[0].Replace("/", "-");
            string date2 = dateHSD.Value.ToString().Split(' ')[0].Replace("/", "-");
            string get = "execute dbo.proc_TimKiemTheoThoiHan '" + date1 + "', '" + date2 + "'";
            //thời hạn
            mainTable.Clear();
            TimKiem.readDatathroughAdapter(get, mainTable);
            if (mainTable.Rows.Count != 0)
            {
                searchTable.Clear();
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

        void searchTheoTenVaThoiHan()
        {
            string ten = txbTimKiem.Text;
            string date1 = dateNSX.Value.ToString().Split(' ')[0].Replace("/", "-");
            string date2 = dateHSD.Value.ToString().Split(' ')[0].Replace("/", "-");
            string get = "execute dbo.proc_TimKiemTheoTenVaThoiHan '" + ten + "', '" + date1 + "', '" + date2 + "'";
            //tên và thời hạn
            mainTable.Clear();
            TimKiem.readDatathroughAdapter(get, mainTable);
            if (mainTable.Rows.Count != 0)
            {
                searchTable.Clear();
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
            if (txbTimKiem.Text == "" && dateNSX.Value == DateTime.Now && dateHSD.Value == DateTime.Now)
            {
                MessageBox.Show("Vui lòng kiểm tra lại thông tin tìm kiếm.", "Thông báo.");
            }   
            else if (txbTimKiem.Text != "" && dateNSX.Value == DateTime.Now && dateHSD.Value == DateTime.Now)
            {
                searchTheoTen();
            }  
            else if (txbTimKiem.Text == "" && dateNSX.Value != DateTime.Now && dateHSD.Value != DateTime.Now)
            {
                searchTheoThoiHan();
            }
            else if (txbTimKiem.Text != "" && dateNSX.Value != DateTime.Now && dateHSD.Value != DateTime.Now)
            {
                searchTheoTenVaThoiHan();
            }
            else
            {
                MessageBox.Show("Vui lòng kiểm tra lại thông tin tìm kiếm.", "Thông báo.");
            }
        }

        void checkTime()
        {
            dateHSD.Value = dateNSX.Value;
            dateHSD.MinDate = dateNSX.Value;
        }

        private void dateNSX_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                checkTime();
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message.ToString(),"Thông báo");
            }
        }

        private void dateHSD_ValueChanged(object sender, EventArgs e)
        {
            dateHSD.MinDate = dateNSX.Value;
        }
    }
}
