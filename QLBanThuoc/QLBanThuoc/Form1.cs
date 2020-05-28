using System;
using QLBanThuoc.BCTK;
using QLBanThuoc.frmQuanLyNguoiDung;
using QLBanThuoc.frmQuanLyThuoc;
using QLBanThuoc.frmTimKiem;
using QLBanThuoc.frmThongKe;
using QLBanThuoc.frmNhaCungCap;

namespace QLBanThuoc
{
    public partial class Form1 : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
        }

        private void BarButtonItem28_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            panel.Controls.Clear();
            frmDanhSach ds = new frmDanhSach();
            panel.Controls.Add(ds);
        }

        private void Nhập_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            panel.Controls.Clear();
            frmNhapThuoc nt = new frmNhapThuoc();
            panel.Controls.Add(nt);
        }

        private void BarButtonItem2_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            panel.Controls.Clear();
            frmBanThuoc bt = new frmBanThuoc();
            panel.Controls.Add(bt);
        }

        private void BarButtonItem13_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            panel.Controls.Clear();
            TenThuoc tt = new TenThuoc();
            panel.Controls.Add(tt);
        }

        private void BarButtonItem18_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            panel.Controls.Clear();
            ThapCao so = new ThapCao();
            panel.Controls.Add(so);
        }

        private void BarButtonItem23_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            panel.Controls.Clear();
            ThuocNhap thuocnha = new ThuocNhap();
            panel.Controls.Add(thuocnha);
        }

        private void BarButtonItem24_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            panel.Controls.Clear();
            ThuocBanRa thuocban = new ThuocBanRa();
            panel.Controls.Add(thuocban);
        }

        private void BarButtonItem25_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            panel.Controls.Clear();
            SoLuongTon ton = new SoLuongTon();
            panel.Controls.Add(ton);
        }

        private void BarButtonItem26_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            panel.Controls.Clear();
            ThuocHetHan thuochet = new ThuocHetHan();
            panel.Controls.Add(thuochet);
        }

        private void BarButtonItem20_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            panel.Controls.Clear();
            DoanhThu dthang = new DoanhThu();
            panel.Controls.Add(dthang);
        }

        private void BarButtonItem21_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            panel.Controls.Clear();
            //DTQuy dtquy = new DTQuy();
            //panel.Controls.Add(dtquy);
        }

        private void BarButtonItem22_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            //panel.Controls.Clear();
            //DTNam dtnam = new DTNam();
            //panel.Controls.Add(dtnam);
        }


        private void BarButtonItem3_ItemClick_1(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            panel.Controls.Clear();
            NhaCungCap ncc = new NhaCungCap();
            panel.Controls.Add(ncc);
        }
    }
}
