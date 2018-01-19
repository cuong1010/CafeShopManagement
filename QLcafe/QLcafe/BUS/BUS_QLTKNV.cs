using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using QLcafe.DATA;
using QLcafe.Model;
using System.Windows.Forms;
namespace QLcafe.BUS
{
    class BUS_QLTKNV
    {
        Database data = new Database();
        //Tao MaND moi
        string MaNDMoi()
        {
            data = new Database();

            var dsnguoidung = (List<NguoiDung>)data.Laytatcathongtin("NguoiDungs");
            for (int i = 0; i < dsnguoidung.Count; i++)
                if (dsnguoidung.ElementAt(i).QuyenHan == -2) dsnguoidung.RemoveAt(i);
            if (dsnguoidung.Count == 0) return "ND01";
            else
            {
                string s = dsnguoidung[dsnguoidung.Count - 1].MaNguoiDung;
                //Console.WriteLine("s={0}", s);
                s = s.Substring(s.IndexOf('D') + 1);
                int i = int.Parse(s) + 1;
                if (i < 10) s = "ND0" + i;
                else s = "ND" + i;
                //Console.WriteLine("send={0}", s);
                return s;
            }
        }

        bool KiemTraTK(NguoiDung nd)
        {
            var s = nd.TaiKhoan;
            for (int i = 0; i < s.Length; i++)
                if (s[i] < (char)48 || (s[i] > (char)57 && s[i] < 65) || (s[i] > (char)90 && s[i] < 97) || s[i] > 122) return false;
            var dsnd = (List<NguoiDung>)data.Laytatcathongtin("NguoiDungs", "TaiKhoan", "=", s);
            if (dsnd[dsnd.Count - 1] == null) return true;
            else if (dsnd[dsnd.Count - 1].MaNguoiDung.Equals(nd.MaNguoiDung)) return true;
            else return false;
        }
        //Them()
        public int Them(NguoiDung nd)
        {

            try
            {
                nd.MaNguoiDung = MaNDMoi();
                if (KiemTraTK(nd)) { data.Them(nd); return 1; }
                else return 2;


            }
            catch { return 0; }
        }
        //sua        
        public int Sua(NguoiDung nd)
        {

            if (KiemTraTK(nd)) { if (data.Sua(nd) > 0) return 1; else return 0; }
            else return 2;


        }


    }

}
