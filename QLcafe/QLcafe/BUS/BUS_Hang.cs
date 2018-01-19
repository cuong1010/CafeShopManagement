using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using QLcafe.Model;
using QLcafe.DATA;

namespace QLcafe.BUS
{
    class BUS_Hang
    {
        Database data = new Database();
        public int NhapHang(PhieuNhap pn,NguyenLieu ngl)
        {
            try
            {
                if(data.Sua(ngl)>0 && data.Them(pn)>0  ) return 1;
                else return 0;
            }
            catch
            {
                return 0;
            }
        }
        public int LuudatHang(List<DSDatHang> a)
        {

            try
            {
                
                foreach (var i in a)
                {
                    data.Them(i);                   
                     //sua lai so lg con thieu cua nguyen lieu
                    var dsngl = (List<NguyenLieu>)data.Laytatcathongtin("NguyenLieus", "MaNguyenLieu", "=", i.MaNguyenLieu);
                    var nglieu = new NguyenLieu();
                    nglieu.MaNguyenLieu = dsngl[dsngl.Count - 1].MaNguyenLieu;
                    nglieu.TenNguyenLieu = dsngl[dsngl.Count - 1].TenNguyenLieu;
                    nglieu.DonGiaNhap = dsngl[dsngl.Count - 1].DonGiaNhap;
                    nglieu.NhaCungCap = dsngl[dsngl.Count - 1].NhaCungCap;
                    nglieu.DonVi = dsngl[dsngl.Count - 1].DonVi;
                    nglieu.SLConThieu = dsngl[dsngl.Count - 1].SLConThieu + i.SoLuong;
                    nglieu.TienConNo += dsngl[dsngl.Count - 1].DonGiaNhap * i.SoLuong;
                    data.Sua(nglieu);
                }
                return 1;
            }
            catch
            {
                return 0;
            }
        }
        
    }
}
