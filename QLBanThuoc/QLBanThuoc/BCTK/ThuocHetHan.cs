﻿using System;
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
        DataTable mainTable = new DataTable();

        public ThuocHetHan()
        {
            InitializeComponent();
        }

        void loadData()
        {
            //Lấy danh sách thông tin thuốc vào bảng
            string check = "execute dbo.proc_ThongKeHetHan";
            Connect.readDatathroughAdapter(check, mainTable);
            if (mainTable.Rows.Count == 0)
            {
                MessageBox.Show("Hiện tại không có loại thuốc nào hết hạn.", "Thông báo.");
            }   
            else
            {
                SqlDataAdapter search = new SqlDataAdapter(check, frmConnection.connection);
                search.Fill(mainTable);
                dgvKetQua.DataSource = mainTable;
            }
        }

        private void ThuocHetHan_Load(object sender, EventArgs e)
        {
            loadData();
        }

        private void btnExcel_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Đang xuất file Excel...", "Thông báo.");
        }
    }
}
