

--------------------form nhap thuoc-----------------------------
create proc proc_searchNhanVien 
	@maNV char(15)
as begin
	select TenNhanVien, SoDienThoai from NHANVIEN where MaNhanVien like @maNV
end



create proc proc_searchNhaCungCap
	@tenNCC nvarchar(30)
as begin
	select MaNhaCungCap,ThongTinDaiDien from NHACUNGCAP where TenNhaCungCap like @tenNCC
end


create proc proc_searchHangSanXuat
as begin
	select TenHangSX from HANGSANXUAT
end


	create proc proc_searchLoaiThuoc
	as
	begin
		select TenLoaiThuoc from LOAITHUOC
	end

	




create proc proc_addThuoc
	@maT char(20),
	@mahang char(15),
	@nsx date,
	@hsd date,
	@ten nvarchar(max),
	@cd nvarchar(max),
	@tp nvarchar(max),
	@sl int,
	@dang nvarchar(20),
	@mLoai char(15),
	@date date,
	@mncc char(15),
	@mnv char(15),
	@gia int
as begin
	declare @maThuoc char(20)
	declare @maLo char(15)
	set @maLo = rtrim(@mahang) +'.'+ RIGHT('00' + RTRIM(CAST(MONTH(@nsx) AS varchar(2))),2)+Right(Year(@nsx),2)
	set @maThuoc= rtrim(@maLo)+'.'+rtrim(@mLoai)+'.'+rtrim(@maT)
	insert into dbo.LOTHUOC
				(MaLoThuoc,MaHangSX,NgaySanXuat,HanSuDung,SoLuongThuoc)
		values(@maLo,@mahang,@nsx,@hsd,@sl)
	insert into dbo.THUOC
			(MaThuoc,MaLoThuoc,TenThuoc,CongDung,ThanhPhan,SoLuongTon,DangThuoc,MaLoaiThuoc)
	values(@maThuoc,@maLo,@ten,@cd,@tp,0,@dang,@mLoai)
		declare @mpn char(20)
	set @mpn=replace(convert(varchar, getdate(),103),'/','') + rtrim(@mnv)

	insert into dbo.PHIEUNHAP
			(MaPhieuNhap,NgayNhap,MaNhaCungCap,MaNhanVien)
	values(@mpn,@date,@mncc,@mnv)
	insert into dbo.CHITIETPHIEUNHAP
			(MaPhieuNhap,MaThuoc,SoLuong,DonGia)
	values(@mpn,@maThuoc,@sl,@gia)
end



create proc proc_selectMaHangSX
	@tenHSX nvarchar(50)
as begin
	select MaHangSX from HANGSANXUAT where TenHangSX like @tenHSX
end

create proc proc_selectLoaiThuoc
	@tenLoaiThuoc nvarchar(30)
as begin
	select MaLoaiThuoc from LOAITHUOC where TenLoaiThuoc like @tenLoaiThuoc
end

create proc proc_searchTenNhaCungCap
as begin
	select TenNhaCungCap from NHACUNGCAP
end
--------end form nhap thuoc ------------------------------

--------form ban thuoc -----------------------------------
create proc proc_searchTenThuoc
as begin
	select TenThuoc from THUOC
end

create proc proc_searchThuoc
	@tenThuoc nvarchar(max)
as begin
	select MaThuoc,ThanhPhan,CongDung,SoLuongTon,DonGia from THUOC where TenThuoc like @tenThuoc 
end

create proc proc_searchKH
	@cmt nchar(12)
as begin
	select * from KHACHHANG where [CMND/TCCCD] like @cmt 
end

create proc proc_addKH
	@ten nvarchar(40),
	@sdt char(15),
	@cmnd char(12)
as begin
	declare @makh char(35)
	set @makh=stuff(rtrim(@sdt),6,0,@cmnd)
	insert into dbo.KHACHHANG
			(MaKhachHang,TenKhachHang,SoDienThoai,[CMND/TCCCD])
	values(@makh,@ten,@sdt,@cmnd)
end

--thêm phiếu xuất + tự sinh mã phiếu
create proc proc_addPX
	@date date,
	@mkh char(35),
	@mnv char(15),
	@tong int
as begin
	declare @mpx char(20)
	declare @sdt char(15)
	if not exists (select * from KHACHHANG where MaKhachHang=@mkh)
	begin
		print N'Mã khách hàng không hợp lệ'
		return
	end
	else if not exists (select * from NHANVIEN where MaNhanVien=@mnv)
	begin
		print N'Mã nhân viên không hợp lệ'
		return
	end
	else
	begin
		set @sdt=(select SoDienThoai from KHACHHANG where MaKhachHang=@mkh)
		set @mpx=replace(convert(varchar, getdate(),103),'/','') + @sdt
		insert into dbo.PHIEUXUAT
				(MaPhieuXuat,NgayXuat,MaKhachHang,MaNhanVien,Tong)
		values(@mpx,@date,@mkh,@mnv,@tong)
	end
end
--chạy
	
--thêm chi tiết phiếu xuất kết hợp vs trigger update số lượng tồn
create proc proc_addCTPX
	@date date,
	@mkh char(35),
	@maT char(20),
	@sl int,
	@gia int
as begin
	declare @mpx char(20)
	declare @sdt char(15)
	set @sdt=(select SoDienThoai from KHACHHANG where MaKhachHang=@mkh)
	set @mpx=replace(convert(varchar, getdate(),103),'/','') + @sdt
	insert into dbo.CHITIETPHIEUXUAT
			(MaPhieuXuat,MaThuoc,SoLuong,DonGia)
	values(@mpx,@maT,@sl,@gia)
end



--trigger update số lượng tồn
create trigger Update_CTPX_Thuoc on dbo.CHITIETPHIEUXUAT
INSTEAD OF INSERT
as begin
	declare @SLT int
	set @SLT = (select SoLuongTon from THUOC where MaThuoc in (select MaThuoc from inserted))
	declare @SLM int
	set @SLM = (select SoLuong from inserted)
	if(@SLT>=@SLM)
		begin
			insert into CHITIETPHIEUXUAT
					(MaPhieuXuat,MaThuoc,SoLuong,DonGia)
			select * from inserted
			update THUOC
			set SoLuongTon=SoLuongTon-@SLM
			where MaThuoc in (select MaThuoc from inserted)
		end
	else
		print N'Kho không đủ thuốc'
end


---------end form ban thuoc -------------------------------

---------form danh sach------------------------------------

create proc proc_SearchThuoc
	@s nvarchar(max)
as begin
	select MaThuoc,TenThuoc,THUOC.MaLoThuoc,MaLoaiThuoc,MaHangSX,ThanhPhan,CongDung,NgaySanXuat,HanSuDung,SoLuongTon,DangThuoc
	from THUOC,LOTHUOC
	where THUOC.TenThuoc like '%'+@s+'%' and THUOC.MaLoThuoc=LOTHUOC.MaLoThuoc 
end

exec proc_SearchThuoc 'ca'




--proc update thuốc

create proc proc_UpdateThuoc
	@mathuoc char(20),
	@malo char(15),
	@ten nvarchar(max),
	@tp nvarchar(max),
	@cd nvarchar(max),
	@nsx date,
	@hsd date,
	@slt int,
	@dt nvarchar(20)
as begin
	update THUOC
	set TenThuoc=@ten,ThanhPhan=@tp,CongDung=@cd,SoLuongTon=@slt,DangThuoc=@dt
	where MaThuoc=@mathuoc

	update LOTHUOC 
	set HanSuDung=@hsd,NgaySanXuat=@nsx
	where MaLoThuoc=@malo
end

exec proc_UpdateThuoc 'BG5T0519.TDY.CSR','BG5T0519',N'Casoran',N'Cao hoa hòe, Cao dừa cạn, Cao tâm sen',N'Thuốc hạ huyết áp','5/10/2019 12:00:00 AM','5/10/2022 12:00:00 AM',1500,N'Thuốc cốm'

--proc del thuốc         

create proc proc_deleteThuoc
	@ma char(20)
as begin
	delete from THUOC where MaThuoc=@ma
end



--------------end form danh sach----------------------------

-------------form nha cung cap-----------------------------

create proc proc_searchNCC
	@ten nvarchar(30)
as begin
	select * from NHACUNGCAP where TenNhaCungCap like '%'+@ten+'%'
end

create proc proc_addNCC
	@ma char(15),
	@ten nvarchar(30),
	@diachi nvarchar(50),
	@tt nvarchar(50),
	@sdt nchar(11)
as begin
	insert into dbo.NHACUNGCAP
			(MaNhaCungCap,TenNhaCungCap,DiaChi,ThongTinDaiDien,SoDienThoai)
	values(@ma,@ten,@diachi,@tt,@sdt)
end

create proc proc_updateNCC
	@ma char(15),
	@ten nvarchar(30),
	@diachi nvarchar(50),
	@tt nvarchar(50),
	@sdt nchar(11)
as begin
	update NHACUNGCAP set TenNhaCungCap = @ten, DiaChi = @diachi, ThongTinDaiDien = @tt, SoDienThoai = @sdt
	where MaNhaCungCap = @ma
end


create proc proc_deleteNCC
	@ma char(15)
as begin
	delete from CHITIETPHIEUNHAP where MaPhieuNhap in 
	(
		select MaPhieuNhap from PHIEUNHAP where MaNhaCungCap = @ma
	)
	delete from PHIEUNHAP where MaPhieuNhap in
	(
		select MaPhieuNhap from PHIEUNHAP where MaNhaCungCap = @ma
	)
	
	delete from NHACUNGCAP where MaNhaCungCap = @ma

end


-------------end form nha cung cap---------------------------

