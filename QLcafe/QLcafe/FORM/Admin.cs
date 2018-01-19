using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using QLcafe.DATA;
using QLcafe.Model;
using QLcafe.BUS;
namespace QLcafe.FORM
{
    public partial class Admin : Form
    {
        public Admin()
        {
            InitializeComponent();
        }

        ///----------------------------QUẢN LÍ TÀI KHOẢN NHÂN VIÊN----------------
        ///

        List<NguoiDung> dsnguoidung;
        Database data = new Database();
        BUS_QLTKNV bus = new BUS_QLTKNV();
        private void Themvaogridview()
        {
            GridviewNV.Rows.Clear();

            for (int i = 0; i < dsnguoidung.Count; i++)
                if (dsnguoidung.ElementAt(i).QuyenHan == -2 || dsnguoidung.ElementAt(i).QuyenHan == 0) dsnguoidung.RemoveAt(i);

            try
            {
                foreach (var i in dsnguoidung)
                {
                    DataGridViewRow row = new DataGridViewRow();
                    row.CreateCells(GridviewNV);
                    row.Cells[0].Value = i.MaNguoiDung;
                    row.Cells[1].Value = i.TaiKhoan;
                    row.Cells[2].Value = '*';
                    row.Cells[3].Value = i.TenNguoiDung;
                    row.Cells[4].Value = i.NgaySinh;
                    GridviewNV.Rows.Add(row);
                }

            }
            catch { }
        }
        void loadthongtintatcanguoidung()
        {
            dsnguoidung = (List<NguoiDung>)data.Laytatcathongtin("NguoiDungs");
            Themvaogridview();

        }
        private void QLTKNV_Enter(object sender, EventArgs e)
        {
            loadthongtintatcanguoidung();
            comboBoxTim.SelectedIndex = 0;
        }           
        NguoiDung laydulieunhapNV()
        {

            var ngdung = new NguoiDung();
            if (txtTK.Text != "" && txtMK.Text != "" && txtHoTen.Text != "")
            {
                ngdung.MaNguoiDung = lbMaND.Text;
                ngdung.TaiKhoan = txtTK.Text;
                ngdung.MatKhau = txtMK.Text;
                ngdung.TenNguoiDung = txtHoTen.Text;
                ngdung.NgaySinh = Convert.ToDateTime(dateTimePickerNS.Text);
                if (cbQuyenHan.SelectedIndex == 0) ngdung.QuyenHan = 2;
                else if (cbQuyenHan.SelectedIndex == 1) ngdung.QuyenHan = 1;
                else ngdung.QuyenHan = -1;
                return ngdung;
            }
            else { MessageBox.Show("Hãy nhập đầy đủ thông tin"); return null; }

        }
        private void GridviewNV_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int row = GridviewNV.CurrentRow.Index;
            lbMaND.Text = dsnguoidung[row].MaNguoiDung;
            txtTK.Text = dsnguoidung[row].TaiKhoan;
            txtMK.Text = dsnguoidung[row].MatKhau;           
            txtHoTen.Text = dsnguoidung[row].TenNguoiDung;
            dateTimePickerNS.Value = dsnguoidung[row].NgaySinh;
            if (dsnguoidung[row].QuyenHan == 2) cbQuyenHan.SelectedIndex = 0;
            else if (dsnguoidung[row].QuyenHan == 1) cbQuyenHan.SelectedIndex = 1;
            else cbQuyenHan.SelectedIndex = 2;
        }
        private void ThemNV_Click(object sender, EventArgs e)
        {
            var nd = laydulieunhapNV();
            if (nd != null)
            {
                int kq = bus.Them(nd);
                loadthongtintatcanguoidung();
                if (kq == 1) MessageBox.Show("Thêm nhân viên thành công");
                else if (kq == 2) MessageBox.Show("Tài khoản bị trùng hoặc chứa kí tự đặc biệt");
                else
                    MessageBox.Show("Đã xảy ra lỗi không thể Thêm nhân viên");
            }
        }
        private void SuaNV_Click(object sender, EventArgs e)
        {
            var nd = laydulieunhapNV();
            if (nd != null)
            {
                int kq = bus.Sua(nd);
                loadthongtintatcanguoidung();
                if (kq == 1) MessageBox.Show("Sua nhân viên thành công");
                else if (kq == 2) MessageBox.Show("Tài khoản bị trùng hoặc chứa kí tự đặc biệt");
                else
                    MessageBox.Show("Đã xảy ra lỗi không thể Thêm nhân viên");
            }
        }
        private void textTim_TextChanged(object sender, EventArgs e)
        {
            try
            {
                string thuoctinhcantim;
                if (comboBoxTim.SelectedIndex == 0) thuoctinhcantim = "TaiKhoan";
                else thuoctinhcantim = "TenNguoiDung";
                dsnguoidung = (List<NguoiDung>)data.Laytatcathongtin("NguoiDungs", thuoctinhcantim, "Like", "%" + textTim.Text + "%");
                Themvaogridview();
                if (textTim.Text == "") loadthongtintatcanguoidung();
            }
            catch { }
        }

        //-----------------------------CAU HINH------------------------------------
        //tabCauHinh

        private void Cauhinh_Enter(object sender, EventArgs e)
        {
            loadthongtintatcaban();
        }
        //CAU HINH BAN
        List<Ban> dsban;     
        
        int timphucvu(string maphucvu)
        {
            for (int i = 0; i < dsnguoidung.Count; i++)
                if (dsnguoidung[i].MaNguoiDung.Equals(maphucvu)) return i;
            return -1;
        }// ham ho tro k co tren seq
        void loadthongtintatcaban()
        {
            GridViewBan.Rows.Clear(); comboTK.Items.Clear();
            dsban = (List<Ban>)data.Laytatcathongtin("Bans");
            dsnguoidung = (List<NguoiDung>)data.Laytatcathongtin("NguoiDungs", "QuyenHan","=", "2");
            try
            {
                foreach (var i in dsban)
                {
                    DataGridViewRow row = new DataGridViewRow();
                    row.CreateCells(GridViewBan);
                    row.Cells[0].Value = i.TenBan;
                    row.Cells[1].Value = i.SucChua;
                    row.Cells[2].Value = dsnguoidung[timphucvu(i.MaPhucVu)].TaiKhoan;
                    row.Cells[3].Value = dsnguoidung[timphucvu(i.MaPhucVu)].TenNguoiDung;
                    GridViewBan.Rows.Add(row);
                }
                foreach (var i in dsnguoidung)
                comboTK.Items.Add(i.TaiKhoan);

            }
            catch { }
        }
        private void CauhinhBan_Enter(object sender, EventArgs e)
        {
            loadthongtintatcaban();
        }      
        private void comboTK_SelectedIndexChanged(object sender, EventArgs e)
        {

            txtTenPV.Text = dsnguoidung[comboTK.SelectedIndex].TenNguoiDung;
        }
        Ban laydulieunhapBan()
        {

            var ban = new Ban();
            try
            {
                if (txtTenBan.Text != "" && txtSucchua.Text != "" && comboTK.SelectedIndex >= 0)
                {
                    ban.MaBan = dsban[GridViewBan.CurrentRow.Index].MaBan;
                    ban.TenBan = txtTenBan.Text;
                    ban.SucChua = Convert.ToInt32(txtSucchua.Text);
                    ban.MaPhucVu = dsnguoidung[comboTK.SelectedIndex].MaNguoiDung;
                    return ban;
                }
                return null;
            }
            catch { MessageBox.Show("Hãy nhập đầy đủ thông tin"); return null; }
        }
        private void GridViewBan_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            txtTenBan.Text = this.GridViewBan.CurrentRow.Cells[0].Value.ToString();
            txtSucchua.Text = this.GridViewBan.CurrentRow.Cells[1].Value.ToString();
            int row = GridViewBan.CurrentRow.Index;
            comboTK.SelectedIndex = timphucvu(dsban[row].MaPhucVu);
            txtTenPV.Text = dsnguoidung[timphucvu(dsban[row].MaPhucVu)].TenNguoiDung;
        }
        private void btnThemBan_Click(object sender, EventArgs e)
        {

            var ban = laydulieunhapNV();
            if (ban != null)
            {

                if (new BUS_CauHinh().Them(ban) > 0) MessageBox.Show("Thêm Bàn thành công");
                else MessageBox.Show("Xảy ra lỗi không thể thêm");
                loadthongtintatcaban();
            }

        }
        private void btnSuaBan_Click(object sender, EventArgs e)
        {
            var ban = laydulieunhapNV();
            if (ban != null)
            {

                if (new BUS_CauHinh().Sua(ban) > 0) MessageBox.Show("Sửa bàn thành công");
                else MessageBox.Show("Xảy ra lỗi không thể sửa");
                loadthongtintatcaban();
            }
        }

        //CAU HINH LOAI SP

        List<LoaiSP> dsloaisp;
        void loadthongtintatcaLoaiSP()
        {
            GridViewLoaiSP.Rows.Clear();
            Database data = new Database();
            dsloaisp = (List<LoaiSP>)data.Laytatcathongtin("LoaiSPs");


            try
            {
                foreach (var i in dsloaisp)
                {
                    DataGridViewRow row = new DataGridViewRow();
                    row.CreateCells(GridViewLoaiSP);
                    row.Cells[0].Value = i.MaLoaiSP;
                    row.Cells[1].Value = i.TenLoaiSP;
                    GridViewLoaiSP.Rows.Add(row);
                }


            }
            catch { }
        }
        private void CauHinhLoaiSP_Enter(object sender, EventArgs e)
        {
            loadthongtintatcaLoaiSP();
        }
        LoaiSP laydulieunhapLoaiSP()
        {

            var loaisp = new LoaiSP();
            try
            {
                if (txtTenLoai.Text != "" && lbMaLoaiSP.Text != "")
                {
                    loaisp.MaLoaiSP = lbMaLoaiSP.Text;
                    loaisp.TenLoaiSP = txtTenLoai.Text;
                    return loaisp;
                }
                return null;
            }
            catch { MessageBox.Show("Hãy nhập đầy đủ thông tin"); return null; }
        }
        private void GridViewLoaiSP_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int row = GridViewLoaiSP.CurrentRow.Index;
            lbMaLoaiSP.Text = dsloaisp[row].MaLoaiSP;
            txtTenLoai.Text = dsloaisp[row].TenLoaiSP;
        }
        private void btnThemLoaiSP_Click(object sender, EventArgs e)
        {
            var loaisp = laydulieunhapLoaiSP();
            if (loaisp != null)
            {

                if (new BUS_CauHinh().Them(loaisp) > 0) MessageBox.Show("Thêm Loại sản phẩm thành công");
                else MessageBox.Show("Xảy ra lỗi không thể thêm");
                loadthongtintatcaLoaiSP();

            }
        }
        private void btnSuaLoaiSP_Click(object sender, EventArgs e)
        {
            var loaiSP = laydulieunhapLoaiSP();
            if (loaiSP != null)
            {

                if (new BUS_CauHinh().Sua(loaiSP) > 0) MessageBox.Show("Sửa Loại sản phẩm thành công");
                else MessageBox.Show("Xảy ra lỗi không thể sửa");
                loadthongtintatcaLoaiSP();
            }
        }

        //CAU HINH SAN PHAM
        List<SanPham> dssp;       
        int timloaisp(string maloai)
        {
            for (int i = 0; i < dsloaisp.Count; i++)
                if (dsloaisp[i].MaLoaiSP.Equals(maloai)) return i;
            return -1;
        }//ham tu them vao de ho tro k co tren seq
        void loadthongtintatcasp()
        {
            GridViewSP.Rows.Clear(); cbLoaiSP.Items.Clear();
            dssp = (List<SanPham>)data.Laytatcathongtin("SanPhams");
            dsloaisp = (List<LoaiSP>)data.Laytatcathongtin("LoaiSPs");
            try
            {
                foreach (var i in dssp)
                {
                    DataGridViewRow row = new DataGridViewRow();
                    row.CreateCells(GridViewSP);
                    row.Cells[0].Value = i.TenSanPham;
                    row.Cells[1].Value = i.DonGiaBan;
                    row.Cells[2].Value = i.DonVi;
                    row.Cells[3].Value = dsloaisp[timloaisp(i.MaLoaiSP)].TenLoaiSP;
                    if (i.TrangThai) row.Cells[4].Value = "Đang bán";
                    else row.Cells[4].Value = "Ngưng bán";
                    GridViewSP.Rows.Add(row);
                }
                foreach (var i in dsloaisp)
                    cbLoaiSP.Items.Add(i.TenLoaiSP);
            }
            catch { }
        }
        private void CauhinhSanPham_Enter(object sender, EventArgs e)
        {
            loadthongtintatcasp();
        }
        SanPham laydulieunhapSP()
        {
            var sp = new SanPham();
            try
            {
                if (txtTenSP.Text != "" && txtGia.Text != "" && lbDonvi.Text != "" && cbLoaiSP.SelectedIndex >= 0 && cbTrangThai.SelectedIndex >= 0)
                {
                    sp.MaSanPham = dssp[GridViewSP.CurrentRow.Index].MaSanPham;
                    sp.TenSanPham = txtTenSP.Text;
                    sp.DonGiaBan = Convert.ToInt32(txtGia.Text);
                    sp.DonVi = txtDonViSP.Text;
                    sp.MaLoaiSP = dsloaisp[cbLoaiSP.SelectedIndex].MaLoaiSP;
                    if (cbTrangThai.SelectedIndex == 0) sp.TrangThai = true;
                    else sp.TrangThai = false;
                    return sp;
                }
                return null;
            }
            catch { MessageBox.Show("Hãy nhập đầy đủ thông tin"); return null; }
        }
        private void GridViewSP_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int row = GridViewSP.CurrentRow.Index;
            txtTenSP.Text = dssp[row].TenSanPham;
            txtGia.Text = dssp[row].DonGiaBan.ToString();
            txtDonViSP.Text = dssp[row].DonVi;
            cbLoaiSP.SelectedIndex = timloaisp(dssp[row].MaLoaiSP);
            if (dssp[row].TrangThai) cbTrangThai.SelectedIndex = 0;
            else cbTrangThai.SelectedIndex = 1;

        }
        private void btnThemSP_Click(object sender, EventArgs e)
        {
            var sp = laydulieunhapSP();
            if (sp != null)
            {

                if (new BUS_CauHinh().Them(sp) > 0) MessageBox.Show("Thêm sản phẩm thành công");
                else MessageBox.Show("Xảy ra lỗi không thể thêm");
                loadthongtintatcasp();
                
            }
        }
        private void btnSuaSP_Click(object sender, EventArgs e)
        {
            var sp = laydulieunhapSP();
            if (sp != null)
            {

                if (new BUS_CauHinh().Sua(sp) > 0) MessageBox.Show("Sửa sản phẩm thành công");
                else MessageBox.Show("Xảy ra lỗi không thể thêm");
                int row = GridViewSP.CurrentRow.Index;
                loadthongtintatcasp();
            }
        }
  
        //CAU HINH NGUYEN LIEU

        List<NguyenLieu> dsngl;
        void loadthongtintatcaNguyenLieu()
        {
            GridViewNGL.Rows.Clear();
            dsngl = (List<NguyenLieu>)data.Laytatcathongtin("NguyenLieus");
            try
            {
                foreach (var i in dsngl)
                {
                    DataGridViewRow row = new DataGridViewRow();
                    row.CreateCells(GridViewNGL);

                    row.Cells[0].Value = i.TenNguyenLieu;
                    row.Cells[1].Value = i.DonGiaNhap;
                    row.Cells[2].Value = i.DonVi;
                    row.Cells[3].Value = i.NhaCungCap;
                    GridViewNGL.Rows.Add(row);
                }
            }
            catch { }
        }
        private void CauHinhNL_Enter(object sender, EventArgs e)
        {
            loadthongtintatcaNguyenLieu();
        }
        NguyenLieu laydulieunhapNL()
        {

            var ngl = new NguyenLieu();
            try
            {
                if (txtTenNL.Text != "" && txtDonGia.Text != "" && txtNCC.Text != "")
                {
                    ngl.MaNguyenLieu = dsngl[GridViewNGL.CurrentRow.Index].MaNguyenLieu;
                    ngl.TenNguyenLieu = txtTenNL.Text;
                    ngl.DonVi = txtDonViNL.Text;
                    ngl.DonGiaNhap = Convert.ToInt32(txtDonGia.Text);
                    ngl.NhaCungCap = txtNCC.Text;
                    ngl.SLConThieu = dsngl[GridViewNGL.CurrentRow.Index].SLConThieu;
                    ngl.TienConNo = dsngl[GridViewNGL.CurrentRow.Index].TienConNo;
                    return ngl;
                }
                return null;
            }
            catch { MessageBox.Show("Hãy nhập đầy đủ thông tin"); return null; }
        }
        private void GridViewNGL_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int row = GridViewNGL.CurrentRow.Index;
            txtTenNL.Text = dsngl[row].TenNguyenLieu;
            txtDonGia.Text = dsngl[row].DonGiaNhap.ToString();
            txtDonViNL.Text = dsngl[row].DonVi;
            txtNCC.Text = dsngl[row].NhaCungCap;
        }
        private void btnThemNL_Click(object sender, EventArgs e)
        {
            var ngl = laydulieunhapNL();
            if (ngl != null)
            {
                ngl.SLConThieu = 0; ngl.TienConNo = 0;
                if (new BUS_CauHinh().Them(ngl) > 0) MessageBox.Show("Thêm Nguyên Liệu thành công");
                else MessageBox.Show("Xảy ra lỗi không thể thêm");
                loadthongtintatcaNguyenLieu();

            }

        }
        private void SuaNL_Click(object sender, EventArgs e)
        {
            var ngl = laydulieunhapNL();
            if (ngl != null)
            {

                if (new BUS_CauHinh().Sua(ngl) > 0) MessageBox.Show("Sửa NL thành công");
                else MessageBox.Show("Xảy ra lỗi không thể sửa");
                loadthongtintatcaNguyenLieu();
            }
        }


        ///----------------------------LƯU THÔNG TIN ĐẶT HÀNG---------------------
        ///
        private void LuuTTDH_Enter(object sender, EventArgs e)
        {
            dsngl = (List<NguyenLieu>)data.Laytatcathongtin("NguyenLieus");
            cbTenNL.Items.Clear();
            try
            {
                foreach (var i in dsngl)
                {
                    cbTenNL.Items.Add(i.TenNguyenLieu);
                }
            }
            catch { }
        }

        private void cbTenNL_SelectedIndexChanged(object sender, EventArgs e)
        {
            int row = cbTenNL.SelectedIndex;
            lbNCC.Text = dsngl[row].NhaCungCap;
            txtSL.Focus();
        }

        private void btnThemDH_Click(object sender, EventArgs e)
        {
            try
            {
                int vt = cbTenNL.SelectedIndex;
                bool flag = true;
                for (int i = 0; i < GridViewDH.Rows.Count; i++)
                {
                    if (dsngl[vt].TenNguyenLieu.Equals(GridViewDH[0, i].Value))
                    {
                        int solgcu = Convert.ToInt32(GridViewDH[2, i].Value);
                        GridViewDH[2, i].Value = (solgcu + Convert.ToInt32(txtSL.Text));
                        flag = false;
                    }
                }
                if (flag)
                {
                    DataGridViewRow row = new DataGridViewRow();
                    row.CreateCells(GridViewDH);
                    row.Cells[0].Value = dsngl[vt].TenNguyenLieu;
                    row.Cells[1].Value = dsngl[vt].DonGiaNhap;
                    row.Cells[2].Value = Convert.ToInt32(txtSL.Text);
                    row.Cells[3].Value = dsngl[vt].DonVi;
                    row.Cells[4].Value = dsngl[vt].NhaCungCap;
                    int gia = Convert.ToInt32(txtSL.Text) * Convert.ToInt32(dsngl[vt].DonGiaNhap);
                    row.Cells[5].Value = String.Format("{0:0,0}", gia);
         
                    GridViewDH.Rows.Add(row);
                }             
            }
            catch { MessageBox.Show("So luong ko hop le"); }
        }

        private void GridViewDH_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int row = GridViewDH.CurrentRow.Index;int vt=0;
            for (int i = 0; i < dsngl.Count; i++)
                if (dsngl[i].TenNguyenLieu.Equals(GridViewDH[0, row].Value)) vt = i;
            cbTenNL.SelectedIndex = vt;
            txtNCC.Text = GridViewDH[3, row].Value.ToString();
            txtSL.Text=GridViewDH[2, row].Value.ToString();
        }

        private void btnSuaDH_Click(object sender, EventArgs e)
        {
            try
            {
                int row = GridViewDH.CurrentRow.Index;
                int vt = cbTenNL.SelectedIndex;
                GridViewDH[0, row].Value = dsngl[vt].TenNguyenLieu;
                GridViewDH[1, row].Value = dsngl[vt].DonGiaNhap;
                GridViewDH[2, row].Value = Convert.ToInt32(txtSL.Text);
                GridViewDH[3, row].Value = dsngl[vt].NhaCungCap;
            }
            catch { MessageBox.Show("So luong ko hop le"); }
        }

        private void btnXoaDH_Click(object sender, EventArgs e)
        {
            if (GridViewDH.Rows.Count > 0)
            {
                int rowSelected = GridViewDH.CurrentRow.Index;
                GridViewDH.Rows.RemoveAt(rowSelected);
            }
        }

        private void btnXacNhan_Click(object sender, EventArgs e)
        {
            if (GridViewDH.Rows.Count > 0)
            {    List<DSDatHang> a=new List<DSDatHang>();

                for (int row = 0; row < GridViewDH.Rows.Count; row++)
                {
                    var b = new DSDatHang();
                    b.MaNguyenLieu=dsngl[cbTenNL.Items.IndexOf(GridViewDH[0,row].Value)].MaNguyenLieu;
                    b.NgayDat = DateTime.Now;
                    b.SoLuong = Convert.ToInt32(GridViewDH[2,row].Value);
                    a.Add(b);
                   
                }
                if (new BUS_Hang().LuudatHang(a) > 0) 
                {
                    MessageBox.Show("Lưu thành công");
                    GridViewDH.Rows.Clear(); 
                }
                else MessageBox.Show("Xảy ra lỗi không thể lưu");
            }
        }

        

        ///---------------------------NHẬP HÀNG-------------------------
        ///
 
        void loadthongtinNguyenLieuCanNhap()
        {
            GridViewNH.Rows.Clear();          
            try
            {
                foreach (var i in dsngl)
                {
                    DataGridViewRow row = new DataGridViewRow();
                    row.CreateCells(GridViewNH);
                    row.Cells[0].Value = i.TenNguyenLieu;
                    row.Cells[1].Value = i.NhaCungCap;
                    row.Cells[2].Value = i.DonGiaNhap;
                    row.Cells[3].Value = i.SLConThieu;
                    row.Cells[4].Value = i.TienConNo;
                    GridViewNH.Rows.Add(row);
                }
            }
            catch { }
        }

        private void NhapHang_Enter(object sender, EventArgs e)
        {
            dsngl = (List<NguyenLieu>)data.Laytatcathongtin("NguyenLieus");
            for (int i = 0; i < dsngl.Count; i++)
                if (dsngl[i].SLConThieu == 0 && dsngl[i].TienConNo == 0) { dsngl.RemoveAt(i); i--; }
            loadthongtinNguyenLieuCanNhap();
        }

        private void GridViewNH_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (GridViewNH.Rows.Count > 0)
            {
                int row = GridViewNH.CurrentRow.Index;
                lbTenNL.Text = dsngl[row].TenNguyenLieu;
                lbSLconthieu.Text = dsngl[row].SLConThieu.ToString();
                lbTienNo.Text = String.Format("{0:0,0}", dsngl[row].TienConNo);
            }
           
        }

        private void txtTienTra_TextChanged(object sender, EventArgs e)
        {
            try
            {
               int tien= Convert.ToInt32(txtTienTra.Text);
               int tienno=dsngl[GridViewNH.CurrentRow.Index].TienConNo - tien;
               if (tienno <= 0) lbTienNo.Text = "0";
               else lbTienNo.Text = String.Format("{0:0,0}", tienno);
               
            }
            catch 
            {
                if (txtTienTra.Text == "") lbTienNo.Text = String.Format("{0:0,0}",dsngl[GridViewNH.CurrentRow.Index].TienConNo);
                else lbTienNo.Text = "-1"; 
            }
        }

        private void btnNhap_Click(object sender, EventArgs e)
        {
            try
            {
                if (int.Parse(txtSLGiao.Text) >= 0 || txtSLGiao.Text=="")
                {
                    var pn = new PhieuNhap();
                    var ngl = new NguyenLieu();
                    pn.Ngay = DateTime.Now;
                    pn.SoLuongNhap = int.Parse(txtSLGiao.Text);
                    if (txtTienTra.Text == "") pn.SoTienTra = 0;
                    else pn.SoTienTra = int.Parse(txtTienTra.Text);
                    int row=GridViewNH.CurrentRow.Index;
                    pn.MaNguyenLieu = dsngl[row].MaNguyenLieu;
                    //ngl
                    ngl.MaNguyenLieu=pn.MaNguyenLieu;
                    ngl.TenNguyenLieu = dsngl[row].TenNguyenLieu;
                    ngl.NhaCungCap = dsngl[row].NhaCungCap;
                    ngl.SLConThieu = dsngl[row].SLConThieu - int.Parse(txtSLGiao.Text);
                    ngl.TienConNo = dsngl[row].TienConNo - pn.SoTienTra;
                    ngl.DonGiaNhap = dsngl[row].DonGiaNhap;
                    ngl.DonVi = dsngl[row].DonVi;
                    if(new BUS_Hang().NhapHang(pn, ngl)>0) MessageBox.Show("Nhập hàng thành công");
                    else MessageBox.Show("Xảy ra lỗi không thể nhập hàng");
                }
            }
            catch { MessageBox.Show("Số lượng giao hoặc số tiền giao không hợp lệ"); }
            
        }

        void Loc()
        {
            GridViewNH.Rows.Clear();
            dsngl = (List<NguyenLieu>)data.Laytatcathongtin("NguyenLieus");
            if(checkSL.Checked) 
                for (int i = 0; i < dsngl.Count; i++)
                    if (dsngl[i].SLConThieu == 0) { dsngl.RemoveAt(i); i--; }
            if(checkTien.Checked)
                for (int i = 0; i < dsngl.Count; i++)
                    if (dsngl[i].TienConNo == 0) { dsngl.RemoveAt(i); i--; }
            if(!checkTien.Checked && !checkSL.Checked)
                for (int i = 0; i < dsngl.Count; i++)
                    if (dsngl[i].SLConThieu == 0 && dsngl[i].TienConNo == 0) { dsngl.RemoveAt(i); i--; }
            loadthongtinNguyenLieuCanNhap();
        }

        private void checkSL_CheckedChanged(object sender, EventArgs e)
        {
            Loc();
        }

        private void checkTien_CheckedChanged(object sender, EventArgs e)
        {
            Loc();
        }

        

        ///--------------------------THỐNG KÊ------------------------
        ///

        bool KTraNgay()
        {
            return true;
        }

        private void btnXemHD_Click(object sender, EventArgs e)
        {
            
        }

        private void btnThongKe_Click(object sender, EventArgs e)
        {

        }




       


        

        

       

       
        
        
     




    }
}
