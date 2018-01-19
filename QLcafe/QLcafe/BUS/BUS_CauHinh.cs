using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using QLcafe.DATA;
using QLcafe.Model;

namespace QLcafe.BUS
{
    class BUS_CauHinh
    {
        Database data = new Database();
        //Tao MaNL moi
        string MaNLMoi()
        {

            var ds = (List<NguyenLieu>)data.Laytatcathongtin("NguyenLieus");
            if (ds[ds.Count - 1] == null) return "NL01";
            else
            {
                string s = ds[ds.Count - 1].MaNguyenLieu;
                s = s.Substring(s.IndexOf('L') + 1);
                int i = int.Parse(s) + 1;
                if (i < 10) s = "NL0" + i;
                else s = "NL" + i;
                return s;
            }
        }
        string MaBanMoi()
        {
            var ds = (List<Ban>)data.Laytatcathongtin("Bans");
            if (ds[ds.Count - 1] == null) return "B01";
            else
            {
                string s = ds[ds.Count - 1].MaBan;
                s = s.Substring(s.IndexOf('B') + 1);
                int i = int.Parse(s) + 1;
                if (i < 10) s = "B0" + i;
                else s = "B" + i;
                return s;
            }
        }
        string MaSPMoi()
        {
            var ds = (List<SanPham>)data.Laytatcathongtin("SanPhams");
            if (ds[ds.Count - 1] == null) return "SP01";
            else
            {
                string s = ds[ds.Count - 1].MaSanPham;
                s = s.Substring(s.IndexOf('P') + 1);
                int i = int.Parse(s) + 1;
                if (i < 10) s = "SP0" + i;
                else s = "SP" + i;
                return s;
            }
        }
        string MaLoaiSPMoi()
        {
            var ds = (List<LoaiSP>)data.Laytatcathongtin("LoaiSPs");
            if (ds[ds.Count - 1] == null) return "L01";
            else
            {
                string s = ds[ds.Count - 1].MaLoaiSP;
                s = s.Substring(s.IndexOf('L') + 1);
                int i = int.Parse(s) + 1;
                if (i < 10) s = "L0" + i;
                else s = "L" + i;
                return s;
            }
        }
        public int Them(object a)
        {

            if (a.GetType().ToString().IndexOf("Ban") > 1) { var b = (Ban)a; b.MaBan = MaBanMoi(); a = b; }
            else
                if (a.GetType().ToString().IndexOf("SanPham") > 1) { var b = (SanPham)a; b.MaSanPham = MaSPMoi(); a = b; }
                else
                    if (a.GetType().ToString().IndexOf("Loai") > 1) { var b = (LoaiSP)a; b.MaLoaiSP = MaLoaiSPMoi(); a = b; }
                    else
                        if (a.GetType().ToString().IndexOf("NguyenLieu") > 1) { var b = (NguyenLieu)a; b.MaNguyenLieu = MaNLMoi(); a = b; }
            if (data.Them(a) > 0) return 1; else return 0;
        }
        public int Sua(object a)
        {
            if (data.Sua(a) > 0) return 1; else return 0;
        }
    }
}
