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

namespace QLBanThuoc.BCTK
{
    public partial class ThuocHetHan : DevExpress.XtraEditors.XtraUserControl
    {
        frmConnection Connect = new frmConnection();
        DataTable main = new DataTable();

        public ThuocHetHan()
        {
            InitializeComponent();
        }

        void loadData()
        {
            //Lấy danh sách thông tin thuốc vào bảng
            string check = "execute dbo.proc_ThongKeHetHan";
            Connect.readDatathroughAdapter(check, main);
            if(main.Rows.Count == 0)
            {
                MessageBox.Show("Hiện tại không có loại thuốc nào hết hạn.", "Thông báo.");
            }   
            else
            {
                SqlDataAdapter search = new SqlDataAdapter("execute dbo.proc_ThongKeHetHan", frmConnection.connection);
                search.Fill(main);
                dgvKetQua.DataSource = main;
            }
        }

        private void ThuocHetHan_Load(object sender, EventArgs e)
        {
            loadData();
        }

        private void btnExcel_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Vui lòng kết nốt với máy in trước khi thực hiện thao tác này...", "Thông báo.");
        }
    }
}
