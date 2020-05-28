using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using QLBanThuoc.Data;
using System.Data.SqlClient;

namespace QLBanThuoc.frmThongKe
{
    public partial class DoanhThu : UserControl
    {
        frmConnection Connect = new frmConnection();
        DataTable doanhthu = new DataTable();

        public DoanhThu()
        {
            InitializeComponent();
        }

        private void btnThongKe_Click(object sender, EventArgs e)
        {
            string month = cmbThang.Text;
            string year = cmbNam.Text;

            //thông kê theo các giá trị đã chọn
            //txbThang = Tháng => THống kê theo năm
            if (cmbThang.Text != "Tháng")
            {
                if (cmbNam.Text != "Năm")
                {
                    //đưa ra doanh thu tháng - năm
                    DataTable dsthangnam = new DataTable();
                    string thangnam = "select DISTINCT MONTH('" + year + "-" + month + "-01" + "') as N'Tháng',YEAR('" + year + "-" + month + "-01" + "') as N'Năm'," +
                        "dbo.DOANHTHUTHANG('" + year + "-" + month + "-01" + "') as N'Doanh Thu' from PHIEUXUAT";
                    SqlDataAdapter search = new SqlDataAdapter(thangnam, frmConnection.connection);
                    search.Fill(dsthangnam);
                    dgvDoanhThu.DataSource = dsthangnam;
                }   
                else if (cmbNam.Text == "Năm")
                {
                    MessageBox.Show("Vui lòng lựa chọn năm cần thống kê doanh thu.", "Thông báo.");
                }    
            }   
            else if (cmbThang.Text == "Tháng")
            {
                if (cmbNam.Text != "Năm")
                {
                    //đưa ra doanh thu năm
                    DataTable dsnam = new DataTable();
                    string nam = "select DISTINCT YEAR('" + year + "-01-01" + "') as N'Năm',dbo.DOANHTHUNAM('" + year + "-01-01" + "') as N'Doanh thu' from PHIEUXUAT ";
                    SqlDataAdapter search = new SqlDataAdapter(nam, frmConnection.connection);
                    search.Fill(dsnam);
                    dgvDoanhThu.DataSource = dsnam;
                }   
                else if (cmbNam.Text == "Năm")
                {
                    MessageBox.Show("Vui lòng lựa chọn tháng và năm cần thống kê doanh thu.", "Thông báo.");
                }    
            }                
        }

        private void btnExcel_Click(object sender, EventArgs e)
        {
            //đưa ra màn hình xác nhận in
            MessageBox.Show("Vui lòng kết nốt với máy in trước khi thực hiện thao tác này...", "Thông báo.");
        }

        private void DoanhThu_Load(object sender, EventArgs e)
        {
            //load thông tin vào bảng
            SqlDataAdapter search = new SqlDataAdapter("execute dbo.DTThang", frmConnection.connection);
            search.Fill(doanhthu);
            dgvDoanhThu.DataSource = doanhthu;
        }
    }
}
