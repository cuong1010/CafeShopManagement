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
    public partial class NhanVien : Form
    {   
        public NhanVien()
        {
            InitializeComponent();
        }
        Database data = new Database();
        List<Ban> dsban;
        List<SanPham> dssp;
        HoaDon hd;

        private void NhanVien_Load(object sender, EventArgs e)
        {
            if (Login.Level == 1) tabPhucvu.Dispose();
            else tabThanhtoan.Dispose();
        }
        void Themvaodatagridview(DataGridView gridview, SanPham sp,int solg,bool gopchungsp)
        {
            bool flag = true;
            if(gopchungsp)
            for (int i = 0; i < gridview.Rows.Count; i++)
            {
                if (sp.TenSanPham.Equals(gridview[0, i].Value))
                {
                    int solgcu = Convert.ToInt32(gridview[2, i].Value);
                    gridview[2, i].Value = (solgcu + Convert.ToInt32(solg));
                    flag = false;
                }
            }

            if (flag)
            {
                DataGridViewRow row = new DataGridViewRow(); row.CreateCells(gridview);
                row.Cells[0].Value = sp.TenSanPham;
                row.Cells[1].Value = sp.DonGiaBan;
                row.Cells[2].Value = solg;
                row.Cells[3].Value = sp.DonVi;
                row.Cells[1].ReadOnly = true;
                row.Cells[3].ReadOnly = true;
                row.Cells[0].ReadOnly = true;
                gridview.Rows.Add(row);
            }
        }
        int Kiemtralaso(string s)
        {
            try
            {
                return Convert.ToInt32(s);

            }
            catch
            {

                return -1;
            }
        }

        //tab Thanhtoan

        void loadtatcathongtinban()
        {
            dsban = (List<Ban>)data.Laytatcathongtin("Bans");
            try
            {
                listboxDSBanthanhtoan.Items.Clear();
                foreach (var i in dsban)
                    listboxDSBanthanhtoan.Items.Add(i.TenBan);
                lbTenBanThanhtoan.Text = dsban[0].TenBan;
            }
            catch { }
        }
        private void tabThanhtoan_Enter(object sender, EventArgs e)
        {
            loadtatcathongtinban(); lbTongTien.Text = ""; lbTienThoi.Text = ""; lbTenBanThanhtoan.Text = "";
            GridViewSPHet.Hide(); btnLuu.Hide();
        }
        HoaDon laythongtinhoadontheoban()
        {
            var hd = new HoaDon();
            int i = listboxDSBanthanhtoan.SelectedIndex;
            var dshd = (List<HoaDon>)data.Laytatcathongtin("HoaDons", "MaBan","=", dsban[i].MaBan);
            try
            {
                if (dshd[dshd.Count - 1].TrangThai == false)
                {
                    hd.MaHoaDon = dshd[dshd.Count - 1].MaHoaDon;
                    hd.MaBan = dshd[dshd.Count - 1].MaBan;
                    hd.Ngay = dshd[dshd.Count - 1].Ngay;
                    hd.TongTien = dshd[dshd.Count - 1].TongTien;
                    hd.TrangThai = dshd[dshd.Count - 1].TrangThai;
                    hd.MaThuNgan = dshd[dshd.Count - 1].MaThuNgan;
                    return hd;
                }
                return null;
            }
            catch
            {
                return null;
            }
        }
        private void listboxDSBanthanhtoan_SelectedIndexChanged(object sender, EventArgs e)
        {
            var cthd = new List<CTHD>(); lbTongTien.Text = ""; lbTienThoi.Text = ""; txtTienKhach.Text = "";
            lbTenBanThanhtoan.Text = dsban[listboxDSBanthanhtoan.SelectedIndex].TenBan;
            datagridviewthanhtoan.Rows.Clear();
            hd = laythongtinhoadontheoban();
            if (hd != null)
            {
                cthd = (List<CTHD>)data.Laytatcathongtin("CTHDs", "MaHoaDon","=", hd.MaHoaDon);
                int row = 0;
                foreach (var i in cthd)
                {                  
                    var dssp = (List<SanPham>)data.Laytatcathongtin("SanPhams", "MaSanPham","=", i.MaSanPham);
                    Themvaodatagridview(datagridviewthanhtoan, dssp[dssp.Count - 1],i.SoLuong,false);
                    datagridviewthanhtoan[4, row].Value = i.ThoiGianGoi;
                    row++;
                }
                lbTongTien.Text = String.Format("{0:0,0}", hd.TongTien);
            }
        }
        private void btnIn_Click(object sender, EventArgs e)
        {

        }
        private void txtTienKhach_TextChanged(object sender, EventArgs e)
        {
            var s = txtTienKhach.Text;
            int tien = Kiemtralaso(s);
            lbTienThoi.Text = String.Format("{0:0,0}", tien - hd.TongTien);
            if (s=="") lbTienThoi.Text = "";
            
        }
        private void btnThanhToan_Click(object sender, EventArgs e)
        {

            if (Kiemtralaso(txtTienKhach.Text) - hd.TongTien < 0) MessageBox.Show("Số tiền khách trả không hợp lệ");
            else
            {
                if (new BUS_HoaDon().Thanhtoan(hd) > 0) { MessageBox.Show("Hóa đơn đã được thanh toán"); datagridviewthanhtoan.Rows.Clear(); }
                else MessageBox.Show("Đã xảy ra lỗi không thể thanh toán");
            } 
        }
        private void btnThongbaoHet_Click(object sender, EventArgs e)
        {
            dssp = (List<SanPham>)data.Laytatcathongtin("SanPhams","TrangThai","=","true");
            GridViewSPHet.Rows.Clear();
            foreach (var i in dssp)
            { 
                DataGridViewRow row=new DataGridViewRow();
                row.CreateCells(GridViewSPHet);
                row.Cells[0].Value = i.TenSanPham;
                GridViewSPHet.Rows.Add(row);
            }
            GridViewSPHet.Show();
            btnLuu.Show();
        }
        private void btnLuu_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < GridViewSPHet.Rows.Count; i++)
                if(GridViewSPHet[1, i].Value!=null)
                if ((bool)GridViewSPHet[1, i].Value == true)
                {
                    var sp = new SanPham();
                    sp.MaSanPham = dssp[i].MaSanPham;
                    sp.TenSanPham = dssp[i].TenSanPham;
                    sp.DonGiaBan = dssp[i].DonGiaBan;
                    sp.DonVi = dssp[i].DonVi;
                    sp.MaLoaiSP = dssp[i].MaLoaiSP;
                    sp.TrangThai = false;
                    data.Sua(sp);
                }
            GridViewSPHet.Hide();
            btnLuu.Hide();
        }
        //tab Phucvu        

        List<LoaiSP> dsloaisp;               
        void loadthongtinbancuaphucvu() 
        {
            listboxDSBan.Items.Clear();
            dsban = (List<Ban>)data.Laytatcathongtin("Bans","MaPhucVu","=",Login.MaNguoiDung);//lay ma phuc vu
            try
            {
                foreach (var i in dsban)
                    listboxDSBan.Items.Add(i.TenBan);
                listboxDSBan.SelectedIndex = 0;
                lbTenBan.Text = dsban[0].TenBan;
            }
            catch { }
        }
        void loadthongtintatcasp()
        {
            Menu.Items.Clear();
            dssp = (List<SanPham>)data.Laytatcathongtin("SanPhams","TrangThai","=","true");
            try
            {
                foreach (var i in dssp)
                    Menu.Items.Add(i.TenSanPham);
                Menu.SelectedIndex = 0;
            }
            catch { }
        }
        void loadthongtintatcaloaisp()  
        {
            comboLoaiSP.Items.Clear();
            dsloaisp = (List<LoaiSP>)data.Laytatcathongtin("LoaiSPs");
            try
            {
                comboLoaiSP.Items.Add("Tất cả");
                foreach (var i in dsloaisp)
                    comboLoaiSP.Items.Add(i.TenLoaiSP);
                comboLoaiSP.SelectedIndex = 0;
            }
            catch { }
        }
        private void tabPhucvu_Enter(object sender, EventArgs e)
        {
          //load thong tin ban
            loadthongtinbancuaphucvu();
           // load thong tin sp
            loadthongtintatcasp();
           //load thong tin loai sp
            loadthongtintatcaloaisp();
        }
        //click chon ban
        private void listboxDSBan_Click(object sender, EventArgs e)
        {
            var i=listboxDSBan.SelectedIndex;
            lbTenBan.Text = dsban[i].TenBan;                
        }
        //chon loai sp
        private void comboLoaiSP_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboLoaiSP.SelectedIndex == 0) loadthongtintatcasp();
            else
            {
                dssp = (List<SanPham>)data.Laytatcathongtin("SanPhams", "MaLoaiSP","=", dsloaisp[comboLoaiSP.SelectedIndex - 1].MaLoaiSP);
                try
                {
                    Menu.Items.Clear();
                    foreach (var i in dssp)
                        Menu.Items.Add(i.TenSanPham);
                    Menu.SelectedIndex = 0;
                }
                catch { }
            }
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            int solg = Kiemtralaso(txtSolg.Text);
            var sp = dssp[Menu.SelectedIndex];
            if (solg > 0) Themvaodatagridview(datagridviewgoimon, sp, solg,true);
            else MessageBox.Show("Số lượng không hợp lệ");
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {   
            if(datagridviewgoimon.Rows.Count >0)
            {
                int rowSelected = datagridviewgoimon.CurrentRow.Index;
                datagridviewgoimon.Rows.RemoveAt(rowSelected);
            }
        }

        private void btnXacNhan_Click(object sender, EventArgs e)
        {
            hd = new HoaDon(); var dongia = new List<int>();
            if (datagridviewgoimon.Rows.Count > 0)
            {
                for (int row = 0; row < datagridviewgoimon.Rows.Count; row++)
                {

                    var cthd = new CTHD();
                    var sp = (List<SanPham>)data.Laytatcathongtin("SanPhams");
                    foreach (var i in sp)
                        if (i.TenSanPham.Equals(datagridviewgoimon[0, row].Value.ToString()))
                        { cthd.MaSanPham = i.MaSanPham; break; }
                    dongia.Add((int)datagridviewgoimon[1, row].Value);
                    try
                    {
                        cthd.SoLuong = Convert.ToInt32(datagridviewgoimon[2, row].Value);
                    }
                    catch { break; }
                    cthd.ThoiGianGoi = DateTime.Now;
                    hd.CTHDs.Add(cthd);
                }
                hd.MaBan = dsban[listboxDSBan.SelectedIndex].MaBan;
                if (new BUS_HoaDon().GoiMon(hd, dongia) > 0) { MessageBox.Show("Món đã được gọi thành công"); datagridviewgoimon.Rows.Clear(); }
                else MessageBox.Show("Đã xảy ra lỗi không thể gọi món");
            }
        }    
       
    }
}
