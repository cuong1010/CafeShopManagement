
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, and Azure
-- --------------------------------------------------
-- Date Created: 05/09/2017 10:55:06
-- Generated from EDMX file: C:\Users\PC\Desktop\HĐT\QLcafe\QLcafe\Model\Cafe.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [Cafe];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[FK_HoaDonBan]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[HoaDons] DROP CONSTRAINT [FK_HoaDonBan];
GO
IF OBJECT_ID(N'[dbo].[FK_CTHDSanPham]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[CTHDs] DROP CONSTRAINT [FK_CTHDSanPham];
GO
IF OBJECT_ID(N'[dbo].[FK_HoaDonCTHD]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[CTHDs] DROP CONSTRAINT [FK_HoaDonCTHD];
GO
IF OBJECT_ID(N'[dbo].[FK_HoaDonUser]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[HoaDons] DROP CONSTRAINT [FK_HoaDonUser];
GO
IF OBJECT_ID(N'[dbo].[FK_SanPhamLoaiSP]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[SanPhams] DROP CONSTRAINT [FK_SanPhamLoaiSP];
GO
IF OBJECT_ID(N'[dbo].[FK_BanNguoiDung]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Bans] DROP CONSTRAINT [FK_BanNguoiDung];
GO
IF OBJECT_ID(N'[dbo].[FK_DSDatHangNguyenLieu]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[DSDatHangs] DROP CONSTRAINT [FK_DSDatHangNguyenLieu];
GO
IF OBJECT_ID(N'[dbo].[FK_PhieuNhapNguyenLieu]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[PhieuNhaps] DROP CONSTRAINT [FK_PhieuNhapNguyenLieu];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[Bans]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Bans];
GO
IF OBJECT_ID(N'[dbo].[CTHDs]', 'U') IS NOT NULL
    DROP TABLE [dbo].[CTHDs];
GO
IF OBJECT_ID(N'[dbo].[HoaDons]', 'U') IS NOT NULL
    DROP TABLE [dbo].[HoaDons];
GO
IF OBJECT_ID(N'[dbo].[LoaiSPs]', 'U') IS NOT NULL
    DROP TABLE [dbo].[LoaiSPs];
GO
IF OBJECT_ID(N'[dbo].[NguoiDungs]', 'U') IS NOT NULL
    DROP TABLE [dbo].[NguoiDungs];
GO
IF OBJECT_ID(N'[dbo].[PhieuNhaps]', 'U') IS NOT NULL
    DROP TABLE [dbo].[PhieuNhaps];
GO
IF OBJECT_ID(N'[dbo].[SanPhams]', 'U') IS NOT NULL
    DROP TABLE [dbo].[SanPhams];
GO
IF OBJECT_ID(N'[dbo].[NguyenLieus]', 'U') IS NOT NULL
    DROP TABLE [dbo].[NguyenLieus];
GO
IF OBJECT_ID(N'[dbo].[DSDatHangs]', 'U') IS NOT NULL
    DROP TABLE [dbo].[DSDatHangs];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'Bans'
CREATE TABLE [dbo].[Bans] (
    [MaBan] nvarchar(10)  NOT NULL,
    [TenBan] nvarchar(max)  NOT NULL,
    [MaPhucVu] nvarchar(10)  NOT NULL,
    [SucChua] int  NOT NULL
);
GO

-- Creating table 'CTHDs'
CREATE TABLE [dbo].[CTHDs] (
    [MaHoaDon] nvarchar(10)  NOT NULL,
    [MaSanPham] nvarchar(10)  NOT NULL,
    [SoLuong] int  NOT NULL,
    [ThoiGianGoi] datetime  NOT NULL
);
GO

-- Creating table 'HoaDons'
CREATE TABLE [dbo].[HoaDons] (
    [MaHoaDon] nvarchar(10)  NOT NULL,
    [TrangThai] bit  NOT NULL,
    [MaBan] nvarchar(10)  NOT NULL,
    [TongTien] int  NOT NULL,
    [Ngay] datetime  NOT NULL,
    [MaThuNgan] nvarchar(10)  NOT NULL
);
GO

-- Creating table 'LoaiSPs'
CREATE TABLE [dbo].[LoaiSPs] (
    [MaLoaiSP] nvarchar(10)  NOT NULL,
    [TenLoaiSP] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'NguoiDungs'
CREATE TABLE [dbo].[NguoiDungs] (
    [MaNguoiDung] nvarchar(10)  NOT NULL,
    [TaiKhoan] nvarchar(10)  NOT NULL,
    [MatKhau] nvarchar(max)  NOT NULL,
    [NgaySinh] datetime  NOT NULL,
    [QuyenHan] int  NOT NULL,
    [TenNguoiDung] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'PhieuNhaps'
CREATE TABLE [dbo].[PhieuNhaps] (
    [Ngay] datetime  NOT NULL,
    [SoTienTra] int  NOT NULL,
    [SoLuongNhap] int  NOT NULL,
    [MaNguyenLieu] nvarchar(10)  NOT NULL
);
GO

-- Creating table 'SanPhams'
CREATE TABLE [dbo].[SanPhams] (
    [MaSanPham] nvarchar(10)  NOT NULL,
    [TenSanPham] nvarchar(max)  NOT NULL,
    [MaLoaiSP] nvarchar(10)  NOT NULL,
    [DonGiaBan] int  NOT NULL,
    [DonVi] nvarchar(max)  NOT NULL,
    [TrangThai] bit  NOT NULL
);
GO

-- Creating table 'NguyenLieus'
CREATE TABLE [dbo].[NguyenLieus] (
    [MaNguyenLieu] nvarchar(10)  NOT NULL,
    [TenNguyenLieu] nvarchar(max)  NOT NULL,
    [DonGiaNhap] int  NOT NULL,
    [NhaCungCap] nvarchar(max)  NOT NULL,
    [DonVi] nvarchar(max)  NOT NULL,
    [SLConThieu] int  NOT NULL,
    [TienConNo] int  NOT NULL
);
GO

-- Creating table 'DSDatHangs'
CREATE TABLE [dbo].[DSDatHangs] (
    [NgayDat] datetime  NOT NULL,
    [SoLuong] int  NOT NULL,
    [MaNguyenLieu] nvarchar(10)  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [MaBan] in table 'Bans'
ALTER TABLE [dbo].[Bans]
ADD CONSTRAINT [PK_Bans]
    PRIMARY KEY CLUSTERED ([MaBan] ASC);
GO

-- Creating primary key on [MaHoaDon], [MaSanPham], [ThoiGianGoi] in table 'CTHDs'
ALTER TABLE [dbo].[CTHDs]
ADD CONSTRAINT [PK_CTHDs]
    PRIMARY KEY CLUSTERED ([MaHoaDon], [MaSanPham], [ThoiGianGoi] ASC);
GO

-- Creating primary key on [MaHoaDon] in table 'HoaDons'
ALTER TABLE [dbo].[HoaDons]
ADD CONSTRAINT [PK_HoaDons]
    PRIMARY KEY CLUSTERED ([MaHoaDon] ASC);
GO

-- Creating primary key on [MaLoaiSP] in table 'LoaiSPs'
ALTER TABLE [dbo].[LoaiSPs]
ADD CONSTRAINT [PK_LoaiSPs]
    PRIMARY KEY CLUSTERED ([MaLoaiSP] ASC);
GO

-- Creating primary key on [MaNguoiDung] in table 'NguoiDungs'
ALTER TABLE [dbo].[NguoiDungs]
ADD CONSTRAINT [PK_NguoiDungs]
    PRIMARY KEY CLUSTERED ([MaNguoiDung] ASC);
GO

-- Creating primary key on [Ngay] in table 'PhieuNhaps'
ALTER TABLE [dbo].[PhieuNhaps]
ADD CONSTRAINT [PK_PhieuNhaps]
    PRIMARY KEY CLUSTERED ([Ngay] ASC);
GO

-- Creating primary key on [MaSanPham] in table 'SanPhams'
ALTER TABLE [dbo].[SanPhams]
ADD CONSTRAINT [PK_SanPhams]
    PRIMARY KEY CLUSTERED ([MaSanPham] ASC);
GO

-- Creating primary key on [MaNguyenLieu] in table 'NguyenLieus'
ALTER TABLE [dbo].[NguyenLieus]
ADD CONSTRAINT [PK_NguyenLieus]
    PRIMARY KEY CLUSTERED ([MaNguyenLieu] ASC);
GO

-- Creating primary key on [NgayDat], [MaNguyenLieu] in table 'DSDatHangs'
ALTER TABLE [dbo].[DSDatHangs]
ADD CONSTRAINT [PK_DSDatHangs]
    PRIMARY KEY CLUSTERED ([NgayDat], [MaNguyenLieu] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [MaBan] in table 'HoaDons'
ALTER TABLE [dbo].[HoaDons]
ADD CONSTRAINT [FK_HoaDonBan]
    FOREIGN KEY ([MaBan])
    REFERENCES [dbo].[Bans]
        ([MaBan])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_HoaDonBan'
CREATE INDEX [IX_FK_HoaDonBan]
ON [dbo].[HoaDons]
    ([MaBan]);
GO

-- Creating foreign key on [MaSanPham] in table 'CTHDs'
ALTER TABLE [dbo].[CTHDs]
ADD CONSTRAINT [FK_CTHDSanPham]
    FOREIGN KEY ([MaSanPham])
    REFERENCES [dbo].[SanPhams]
        ([MaSanPham])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_CTHDSanPham'
CREATE INDEX [IX_FK_CTHDSanPham]
ON [dbo].[CTHDs]
    ([MaSanPham]);
GO

-- Creating foreign key on [MaHoaDon] in table 'CTHDs'
ALTER TABLE [dbo].[CTHDs]
ADD CONSTRAINT [FK_HoaDonCTHD]
    FOREIGN KEY ([MaHoaDon])
    REFERENCES [dbo].[HoaDons]
        ([MaHoaDon])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating foreign key on [MaThuNgan] in table 'HoaDons'
ALTER TABLE [dbo].[HoaDons]
ADD CONSTRAINT [FK_HoaDonUser]
    FOREIGN KEY ([MaThuNgan])
    REFERENCES [dbo].[NguoiDungs]
        ([MaNguoiDung])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_HoaDonUser'
CREATE INDEX [IX_FK_HoaDonUser]
ON [dbo].[HoaDons]
    ([MaThuNgan]);
GO

-- Creating foreign key on [MaLoaiSP] in table 'SanPhams'
ALTER TABLE [dbo].[SanPhams]
ADD CONSTRAINT [FK_SanPhamLoaiSP]
    FOREIGN KEY ([MaLoaiSP])
    REFERENCES [dbo].[LoaiSPs]
        ([MaLoaiSP])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_SanPhamLoaiSP'
CREATE INDEX [IX_FK_SanPhamLoaiSP]
ON [dbo].[SanPhams]
    ([MaLoaiSP]);
GO

-- Creating foreign key on [MaPhucVu] in table 'Bans'
ALTER TABLE [dbo].[Bans]
ADD CONSTRAINT [FK_BanNguoiDung]
    FOREIGN KEY ([MaPhucVu])
    REFERENCES [dbo].[NguoiDungs]
        ([MaNguoiDung])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_BanNguoiDung'
CREATE INDEX [IX_FK_BanNguoiDung]
ON [dbo].[Bans]
    ([MaPhucVu]);
GO

-- Creating foreign key on [MaNguyenLieu] in table 'DSDatHangs'
ALTER TABLE [dbo].[DSDatHangs]
ADD CONSTRAINT [FK_DSDatHangNguyenLieu]
    FOREIGN KEY ([MaNguyenLieu])
    REFERENCES [dbo].[NguyenLieus]
        ([MaNguyenLieu])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_DSDatHangNguyenLieu'
CREATE INDEX [IX_FK_DSDatHangNguyenLieu]
ON [dbo].[DSDatHangs]
    ([MaNguyenLieu]);
GO

-- Creating foreign key on [MaNguyenLieu] in table 'PhieuNhaps'
ALTER TABLE [dbo].[PhieuNhaps]
ADD CONSTRAINT [FK_PhieuNhapNguyenLieu]
    FOREIGN KEY ([MaNguyenLieu])
    REFERENCES [dbo].[NguyenLieus]
        ([MaNguyenLieu])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_PhieuNhapNguyenLieu'
CREATE INDEX [IX_FK_PhieuNhapNguyenLieu]
ON [dbo].[PhieuNhaps]
    ([MaNguyenLieu]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------