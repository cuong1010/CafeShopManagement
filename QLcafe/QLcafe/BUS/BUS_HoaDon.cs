using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using QLcafe.DATA;
using QLcafe.Model;
using QLcafe.FORM;
using System.Windows.Forms;

namespace QLcafe.BUS
{
    class BUS_HoaDon
    {
        Database data = new Database();

        public int Tinhtongtien(HoaDon HD,List<int>dongia)
        {
            int tong = 0; int vt = 0;
            foreach (var i in HD.CTHDs)
            {
                tong += dongia[vt] * i.SoLuong;
                vt++;
            }
            return tong;
        }

        string MaHDMoi()
        {
            var dshd = (List<HoaDon>)data.Laytatcathongtin("HoaDons");
            if (dshd[dshd.Count - 1]==null) return "HD01";
                else
                {
                    string s = dshd[dshd.Count - 1].MaHoaDon;
                    s = s.Substring(s.IndexOf('D') + 1);
                    int i = int.Parse(s) + 1;
                    if (i < 10) s = "HD0" + i;
                    else s = "HD" + i;
                    return s;
                }
        }

        public int GoiMon(HoaDon hd, List<int> dongia)
        {
            var db = new Database();/////
            var dshd = (List<HoaDon>)data.Laytatcathongtin("HoaDons", "MaBan","=", hd.MaBan);
            //
            //hoa don chua thanh toan
            //
            if ( dshd[dshd.Count - 1]!=null && dshd.Count > 0 && dshd[dshd.Count - 1].TrangThai == false)
            {
                //thong tin hoa don cũ chua thanh toán
                var hoadoncu = new HoaDon();
                hoadoncu.MaHoaDon = dshd[dshd.Count - 1].MaHoaDon;
                hoadoncu.MaBan = dshd[dshd.Count - 1].MaBan;
                hoadoncu.TrangThai = dshd[dshd.Count - 1].TrangThai;
                hoadoncu.Ngay = dshd[dshd.Count - 1].Ngay;
                hoadoncu.MaThuNgan = dshd[dshd.Count - 1].MaThuNgan;
                hoadoncu.TongTien = dshd[dshd.Count - 1].TongTien;
                /* 
                 vi du lieu lay len da thong wa proxy cua entity nen kieu du lieu ko nhu cũ khi sửa se xay ra lỗi
                 nen phai add lai de lay dung kieu du lieu HoaDon thay vi là 
                (system.data.entity.dynamicproxies.HoaDon......)
                */
                var dsspdagoi = (List<CTHD>)data.Laytatcathongtin("CTHDs", "MaHoaDon","=", hoadoncu.MaHoaDon);
                try
                {
                    //cap nhat lai tong tien cua hoa don cũ
                    hoadoncu.TongTien += Tinhtongtien(hd, dongia);
                    db.Sua(hoadoncu);
                    foreach (var i in hd.CTHDs)
                    {
                        i.MaHoaDon = hoadoncu.MaHoaDon;
                        db.Them(i);
                    }
                    return 1;
                }
                catch { return 0; }

            }
            //
            //tao hoa don moi      
            //
            else
            {

                    hd.MaHoaDon = MaHDMoi();
                    hd.TrangThai = false;
                    hd.Ngay = DateTime.Now;
                    hd.MaThuNgan = "null";
                    hd.TongTien = Tinhtongtien(hd, dongia);
                    if (data.Them(hd) > 0) return 1;
                    else return 0;            
            }
        }

        public int Thanhtoan(HoaDon hd)
        {
            hd.MaThuNgan = Login.MaNguoiDung;
            hd.TrangThai = true;
            if (data.Sua(hd) > 0) return 1;
            else return 0;
            
        }
        
    }
}
