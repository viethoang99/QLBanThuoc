﻿namespace QLBanThuoc.frmQuanLyThuoc
{
    partial class frmDanhSach
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmDanhSach));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.clMaThuoc = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clMaLoThuoc = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.simpleButton4 = new DevExpress.XtraEditors.SimpleButton();
            this.simpleButton3 = new DevExpress.XtraEditors.SimpleButton();
            this.groupControl2 = new DevExpress.XtraEditors.GroupControl();
            this.btnTimKiem = new DevExpress.XtraEditors.SimpleButton();
            this.textBoxTimKiem = new System.Windows.Forms.TextBox();
            this.groupControl1 = new DevExpress.XtraEditors.GroupControl();
            this.simpleButton5 = new DevExpress.XtraEditors.SimpleButton();
            this.btnLuuLai = new DevExpress.XtraEditors.SimpleButton();
            this.textBoxHanSD = new System.Windows.Forms.TextBox();
            this.textBoxGiaBan = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.textBoxThanhPhanChinh = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.textBoxDonVi = new System.Windows.Forms.TextBox();
            this.textBoxSoLuongTon = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.textBoxCongDung = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.textBoxNgaySX = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.textBoxLoThuoc = new System.Windows.Forms.TextBox();
            this.textBoxHangSX = new System.Windows.Forms.TextBox();
            this.textBoxLoaiThuoc = new System.Windows.Forms.TextBox();
            this.textBoxTenThuoc = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.groupBox2);
            this.groupBox1.Controls.Add(this.groupControl2);
            this.groupBox1.Controls.Add(this.groupControl1);
            this.groupBox1.Location = new System.Drawing.Point(-3, -1);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.groupBox1.Size = new System.Drawing.Size(878, 512);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Cập nhật thuốc";
            this.groupBox1.Enter += new System.EventHandler(this.groupBox1_Enter);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.dataGridView1);
            this.groupBox2.Controls.Add(this.simpleButton4);
            this.groupBox2.Controls.Add(this.simpleButton3);
            this.groupBox2.Location = new System.Drawing.Point(464, 196);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.groupBox2.Size = new System.Drawing.Size(405, 292);
            this.groupBox2.TabIndex = 6;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Danh sách các thuốc";
            // 
            // dataGridView1
            // 
            this.dataGridView1.BackgroundColor = System.Drawing.SystemColors.ControlLightLight;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.clMaThuoc,
            this.clMaLoThuoc,
            this.Column1,
            this.Column2,
            this.Column3,
            this.Column5});
            this.dataGridView1.Location = new System.Drawing.Point(7, 25);
            this.dataGridView1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersWidth = 51;
            this.dataGridView1.Size = new System.Drawing.Size(391, 204);
            this.dataGridView1.TabIndex = 2;
            this.dataGridView1.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvCellClick_Click);
            // 
            // clMaThuoc
            // 
            this.clMaThuoc.DataPropertyName = "MaThuoc";
            this.clMaThuoc.HeaderText = "Mã thuốc";
            this.clMaThuoc.MinimumWidth = 6;
            this.clMaThuoc.Name = "clMaThuoc";
            this.clMaThuoc.ReadOnly = true;
            this.clMaThuoc.Width = 125;
            // 
            // clMaLoThuoc
            // 
            this.clMaLoThuoc.DataPropertyName = "MaLoThuoc";
            this.clMaLoThuoc.HeaderText = "Mã lô thuốc";
            this.clMaLoThuoc.MinimumWidth = 6;
            this.clMaLoThuoc.Name = "clMaLoThuoc";
            this.clMaLoThuoc.ReadOnly = true;
            this.clMaLoThuoc.Width = 125;
            // 
            // Column1
            // 
            this.Column1.DataPropertyName = "TenThuoc";
            this.Column1.HeaderText = "Tên thuốc";
            this.Column1.MinimumWidth = 6;
            this.Column1.Name = "Column1";
            this.Column1.Width = 125;
            // 
            // Column2
            // 
            this.Column2.DataPropertyName = "MaLoaiThuoc";
            this.Column2.HeaderText = "Loại thuốc";
            this.Column2.MinimumWidth = 6;
            this.Column2.Name = "Column2";
            this.Column2.Width = 125;
            // 
            // Column3
            // 
            this.Column3.DataPropertyName = "DonGia";
            this.Column3.HeaderText = "Giá bán";
            this.Column3.MinimumWidth = 6;
            this.Column3.Name = "Column3";
            this.Column3.Width = 125;
            // 
            // Column5
            // 
            this.Column5.DataPropertyName = "SoLuongTon";
            this.Column5.HeaderText = "Số lượng tồn";
            this.Column5.MinimumWidth = 6;
            this.Column5.Name = "Column5";
            this.Column5.Width = 125;
            // 
            // simpleButton4
            // 
            this.simpleButton4.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("simpleButton4.ImageOptions.SvgImage")));
            this.simpleButton4.Location = new System.Drawing.Point(215, 242);
            this.simpleButton4.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.simpleButton4.Name = "simpleButton4";
            this.simpleButton4.Size = new System.Drawing.Size(110, 33);
            this.simpleButton4.TabIndex = 5;
            this.simpleButton4.Text = "Xóa";
            this.simpleButton4.Click += new System.EventHandler(this.buttonXoa_Click);
            // 
            // simpleButton3
            // 
            this.simpleButton3.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("simpleButton3.ImageOptions.SvgImage")));
            this.simpleButton3.Location = new System.Drawing.Point(90, 242);
            this.simpleButton3.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.simpleButton3.Name = "simpleButton3";
            this.simpleButton3.Size = new System.Drawing.Size(97, 33);
            this.simpleButton3.TabIndex = 4;
            this.simpleButton3.Text = "Sửa";
            this.simpleButton3.Click += new System.EventHandler(this.buttonSua_Click);
            // 
            // groupControl2
            // 
            this.groupControl2.AppearanceCaption.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupControl2.AppearanceCaption.ForeColor = System.Drawing.Color.Red;
            this.groupControl2.AppearanceCaption.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Vertical;
            this.groupControl2.AppearanceCaption.Options.UseFont = true;
            this.groupControl2.AppearanceCaption.Options.UseForeColor = true;
            this.groupControl2.CaptionImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("groupControl2.CaptionImageOptions.Image")));
            this.groupControl2.Controls.Add(this.btnTimKiem);
            this.groupControl2.Controls.Add(this.textBoxTimKiem);
            this.groupControl2.Location = new System.Drawing.Point(464, 25);
            this.groupControl2.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.groupControl2.Name = "groupControl2";
            this.groupControl2.Size = new System.Drawing.Size(386, 155);
            this.groupControl2.TabIndex = 1;
            this.groupControl2.Text = "Tìm kiếm";
            // 
            // btnTimKiem
            // 
            this.btnTimKiem.Appearance.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnTimKiem.Appearance.Options.UseFont = true;
            this.btnTimKiem.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnTimKiem.ImageOptions.Image")));
            this.btnTimKiem.Location = new System.Drawing.Point(136, 101);
            this.btnTimKiem.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnTimKiem.Name = "simpleButton1";
            this.btnTimKiem.Size = new System.Drawing.Size(140, 39);
            this.btnTimKiem.TabIndex = 15;
            this.btnTimKiem.Text = "Tìm kiếm";
            this.btnTimKiem.Click += new System.EventHandler(this.buttonTimKiem_Click);
            // 
            // textBoxTimKiem
            // 
            this.textBoxTimKiem.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxTimKiem.Location = new System.Drawing.Point(73, 57);
            this.textBoxTimKiem.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.textBoxTimKiem.Name = "textBoxTimKiem";
            this.textBoxTimKiem.Size = new System.Drawing.Size(279, 24);
            this.textBoxTimKiem.TabIndex = 14;
            this.textBoxTimKiem.Text = "Thông tin tìm kiếm";
            this.textBoxTimKiem.Click += new System.EventHandler(this.textBoxTimKiem_Click);
            this.textBoxTimKiem.TextChanged += new System.EventHandler(this.textBoxTimKiem_TextChanged);
            // 
            // groupControl1
            // 
            this.groupControl1.Appearance.BackColor2 = System.Drawing.Color.White;
            this.groupControl1.Appearance.BorderColor = System.Drawing.Color.White;
            this.groupControl1.Appearance.Options.UseBorderColor = true;
            this.groupControl1.AppearanceCaption.BackColor2 = System.Drawing.Color.White;
            this.groupControl1.AppearanceCaption.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupControl1.AppearanceCaption.ForeColor = System.Drawing.Color.Red;
            this.groupControl1.AppearanceCaption.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Vertical;
            this.groupControl1.AppearanceCaption.Options.UseFont = true;
            this.groupControl1.AppearanceCaption.Options.UseForeColor = true;
            this.groupControl1.CaptionImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("groupControl1.CaptionImageOptions.Image")));
            this.groupControl1.CaptionLocation = DevExpress.Utils.Locations.Top;
            this.groupControl1.Controls.Add(this.simpleButton5);
            this.groupControl1.Controls.Add(this.btnLuuLai);
            this.groupControl1.Controls.Add(this.textBoxHanSD);
            this.groupControl1.Controls.Add(this.textBoxGiaBan);
            this.groupControl1.Controls.Add(this.label11);
            this.groupControl1.Controls.Add(this.label3);
            this.groupControl1.Controls.Add(this.textBoxThanhPhanChinh);
            this.groupControl1.Controls.Add(this.label10);
            this.groupControl1.Controls.Add(this.textBoxDonVi);
            this.groupControl1.Controls.Add(this.textBoxSoLuongTon);
            this.groupControl1.Controls.Add(this.label5);
            this.groupControl1.Controls.Add(this.label9);
            this.groupControl1.Controls.Add(this.textBoxCongDung);
            this.groupControl1.Controls.Add(this.label8);
            this.groupControl1.Controls.Add(this.textBoxNgaySX);
            this.groupControl1.Controls.Add(this.label1);
            this.groupControl1.Controls.Add(this.textBoxLoThuoc);
            this.groupControl1.Controls.Add(this.textBoxHangSX);
            this.groupControl1.Controls.Add(this.textBoxLoaiThuoc);
            this.groupControl1.Controls.Add(this.textBoxTenThuoc);
            this.groupControl1.Controls.Add(this.label7);
            this.groupControl1.Controls.Add(this.label6);
            this.groupControl1.Controls.Add(this.label4);
            this.groupControl1.Controls.Add(this.label2);
            this.groupControl1.GroupStyle = DevExpress.Utils.GroupStyle.Card;
            this.groupControl1.Location = new System.Drawing.Point(14, 25);
            this.groupControl1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.groupControl1.Name = "groupControl1";
            this.groupControl1.Size = new System.Drawing.Size(443, 487);
            this.groupControl1.TabIndex = 0;
            this.groupControl1.Text = "Thông tin";
            // 
            // simpleButton5
            // 
            this.simpleButton5.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("simpleButton5.ImageOptions.Image")));
            this.simpleButton5.Location = new System.Drawing.Point(346, 442);
            this.simpleButton5.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.simpleButton5.Name = "simpleButton5";
            this.simpleButton5.Size = new System.Drawing.Size(97, 33);
            this.simpleButton5.TabIndex = 6;
            this.simpleButton5.Text = "Hủy bỏ";
            this.simpleButton5.Click += new System.EventHandler(this.buttonHuy_Click);
            // 
            // btnLuuLai
            // 
            this.btnLuuLai.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnLuuLai.ImageOptions.Image")));
            this.btnLuuLai.Location = new System.Drawing.Point(346, 390);
            this.btnLuuLai.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnLuuLai.Name = "simpleButton6";
            this.btnLuuLai.Size = new System.Drawing.Size(97, 33);
            this.btnLuuLai.TabIndex = 7;
            this.btnLuuLai.Text = "Lưu lại";
            this.btnLuuLai.Click += new System.EventHandler(this.buttonLuu_Click);
            // 
            // textBoxHanSD
            // 
            this.textBoxHanSD.Location = new System.Drawing.Point(150, 356);
            this.textBoxHanSD.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.textBoxHanSD.Name = "textBoxHanSD";
            this.textBoxHanSD.Size = new System.Drawing.Size(173, 23);
            this.textBoxHanSD.TabIndex = 15;
            // 
            // textBoxGiaBan
            // 
            this.textBoxGiaBan.Location = new System.Drawing.Point(150, 389);
            this.textBoxGiaBan.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.textBoxGiaBan.Name = "textBoxGiaBan";
            this.textBoxGiaBan.Size = new System.Drawing.Size(173, 23);
            this.textBoxGiaBan.TabIndex = 23;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.Location = new System.Drawing.Point(6, 423);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(121, 21);
            this.label11.TabIndex = 22;
            this.label11.Text = "Số lượng tồn";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(7, 357);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(119, 21);
            this.label3.TabIndex = 14;
            this.label3.Text = "Hạn sử dụng";
            // 
            // textBoxThanhPhanChinh
            // 
            this.textBoxThanhPhanChinh.Location = new System.Drawing.Point(150, 208);
            this.textBoxThanhPhanChinh.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.textBoxThanhPhanChinh.Name = "textBoxThanhPhanChinh";
            this.textBoxThanhPhanChinh.Size = new System.Drawing.Size(279, 23);
            this.textBoxThanhPhanChinh.TabIndex = 21;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.Location = new System.Drawing.Point(7, 390);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(75, 21);
            this.label10.TabIndex = 20;
            this.label10.Text = "Giá bán";
            // 
            // textBoxDonVi
            // 
            this.textBoxDonVi.Location = new System.Drawing.Point(150, 454);
            this.textBoxDonVi.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.textBoxDonVi.Name = "textBoxDonVi";
            this.textBoxDonVi.Size = new System.Drawing.Size(173, 23);
            this.textBoxDonVi.TabIndex = 19;
            // 
            // textBoxSoLuongTon
            // 
            this.textBoxSoLuongTon.Location = new System.Drawing.Point(150, 422);
            this.textBoxSoLuongTon.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.textBoxSoLuongTon.Name = "textBoxSoLuongTon";
            this.textBoxSoLuongTon.Size = new System.Drawing.Size(173, 23);
            this.textBoxSoLuongTon.TabIndex = 10;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(7, 455);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(65, 21);
            this.label5.TabIndex = 4;
            this.label5.Text = "Đơn vị";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(6, 241);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(103, 21);
            this.label9.TabIndex = 18;
            this.label9.Text = "Công dụng";
            // 
            // textBoxCongDung
            // 
            this.textBoxCongDung.Location = new System.Drawing.Point(150, 241);
            this.textBoxCongDung.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.textBoxCongDung.Multiline = true;
            this.textBoxCongDung.Name = "textBoxCongDung";
            this.textBoxCongDung.Size = new System.Drawing.Size(279, 66);
            this.textBoxCongDung.TabIndex = 17;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(5, 208);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(163, 21);
            this.label8.TabIndex = 16;
            this.label8.Text = "Thành phần chính";
            // 
            // textBoxNgaySX
            // 
            this.textBoxNgaySX.Location = new System.Drawing.Point(150, 320);
            this.textBoxNgaySX.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.textBoxNgaySX.Name = "textBoxNgaySX";
            this.textBoxNgaySX.Size = new System.Drawing.Size(173, 23);
            this.textBoxNgaySX.TabIndex = 13;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(7, 322);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(132, 21);
            this.label1.TabIndex = 12;
            this.label1.Text = "Ngày sản xuất";
            // 
            // textBoxLoThuoc
            // 
            this.textBoxLoThuoc.Location = new System.Drawing.Point(153, 89);
            this.textBoxLoThuoc.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.textBoxLoThuoc.Name = "textBoxLoThuoc";
            this.textBoxLoThuoc.Size = new System.Drawing.Size(279, 23);
            this.textBoxLoThuoc.TabIndex = 11;
            // 
            // textBoxHangSX
            // 
            this.textBoxHangSX.Location = new System.Drawing.Point(150, 171);
            this.textBoxHangSX.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.textBoxHangSX.Name = "textBoxHangSX";
            this.textBoxHangSX.Size = new System.Drawing.Size(279, 23);
            this.textBoxHangSX.TabIndex = 9;
            // 
            // textBoxLoaiThuoc
            // 
            this.textBoxLoaiThuoc.Location = new System.Drawing.Point(152, 129);
            this.textBoxLoaiThuoc.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.textBoxLoaiThuoc.Name = "textBoxLoaiThuoc";
            this.textBoxLoaiThuoc.Size = new System.Drawing.Size(279, 23);
            this.textBoxLoaiThuoc.TabIndex = 8;
            // 
            // textBoxTenThuoc
            // 
            this.textBoxTenThuoc.Location = new System.Drawing.Point(153, 52);
            this.textBoxTenThuoc.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.textBoxTenThuoc.Name = "textBoxTenThuoc";
            this.textBoxTenThuoc.Size = new System.Drawing.Size(279, 23);
            this.textBoxTenThuoc.TabIndex = 7;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(7, 89);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(110, 21);
            this.label7.TabIndex = 6;
            this.label7.Text = "Mã lô thuốc";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(6, 129);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(100, 21);
            this.label6.TabIndex = 5;
            this.label6.Text = "Loại thuốc";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(6, 172);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(133, 21);
            this.label4.TabIndex = 3;
            this.label4.Text = "Hãng sản xuất";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(6, 52);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(95, 21);
            this.label2.TabIndex = 1;
            this.label2.Text = "Tên thuốc";
            // 
            // frmDanhSach
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupBox1);
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "frmDanhSach";
            this.Size = new System.Drawing.Size(870, 511);
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.DataGridView dataGridView1;
        private DevExpress.XtraEditors.SimpleButton simpleButton4;
        private DevExpress.XtraEditors.SimpleButton simpleButton3;
        private DevExpress.XtraEditors.GroupControl groupControl2;
        private DevExpress.XtraEditors.SimpleButton btnTimKiem;
        private System.Windows.Forms.TextBox textBoxTimKiem;
        private DevExpress.XtraEditors.GroupControl groupControl1;
        private DevExpress.XtraEditors.SimpleButton simpleButton5;
        private DevExpress.XtraEditors.SimpleButton btnLuuLai;
        private System.Windows.Forms.TextBox textBoxHanSD;
        private System.Windows.Forms.TextBox textBoxGiaBan;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textBoxThanhPhanChinh;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox textBoxDonVi;
        private System.Windows.Forms.TextBox textBoxSoLuongTon;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox textBoxCongDung;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox textBoxNgaySX;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBoxLoThuoc;
        private System.Windows.Forms.TextBox textBoxHangSX;
        private System.Windows.Forms.TextBox textBoxLoaiThuoc;
        private System.Windows.Forms.TextBox textBoxTenThuoc;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DataGridViewTextBoxColumn clMaThuoc;
        private System.Windows.Forms.DataGridViewTextBoxColumn clMaLoThuoc;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column3;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column5;
    }
}
