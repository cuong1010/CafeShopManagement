using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using QLcafe.Model;
using System.Data;
using System.Data.Entity.Validation;

namespace QLcafe.DATA
{
    class Database
    {
        CafeEntities db = new CafeEntities();
        public object Laytatcathongtin(string bang)
        {

            db = new CafeEntities();
            if (bang == "Bans") return db.Database.SqlQuery<Ban>("select * from Bans").DefaultIfEmpty().ToList();
            else if (bang == "SanPhams") return db.SanPhams.SqlQuery("select * from SanPhams").DefaultIfEmpty().ToList();
            else if (bang == "HoaDons") return (from c in db.HoaDons select c).DefaultIfEmpty().ToList();
            else if (bang == "NguoiDungs") return (from c in db.NguoiDungs select c).DefaultIfEmpty().ToList();
            else if (bang == "LoaiSPs") return (from c in db.LoaiSPs select c).DefaultIfEmpty().ToList();
            else if (bang == "CTHDs") return (from c in db.CTHDs select c).DefaultIfEmpty().ToList();
            else if (bang == "NguyenLieus") return (from c in db.NguyenLieus select c).DefaultIfEmpty().ToList();
            else if (bang == "DSDatHangs") return (from c in db.DSDatHangs select c).DefaultIfEmpty().ToList();
            else return (from c in db.PhieuNhaps select c).DefaultIfEmpty().ToList();
        }
        public object Laytatcathongtin(string bang, string cotdieukien, string dieukientimkiem, string giatri)
        {
            db = new CafeEntities();
            string s = "select * from " + bang + " where " + cotdieukien + " " + dieukientimkiem + " '" + giatri + "'";
            // Console.WriteLine(s);
            if (bang == "Bans") return db.Bans.SqlQuery(s).DefaultIfEmpty().ToList();
            else if (bang == "SanPhams") return db.SanPhams.SqlQuery(s).DefaultIfEmpty().ToList();
            else if (bang == "HoaDons") return db.HoaDons.SqlQuery(s).DefaultIfEmpty().ToList();
            else if (bang == "NguoiDungs") return db.NguoiDungs.SqlQuery(s).DefaultIfEmpty().ToList();
            else if (bang == "LoaiSPs") return db.LoaiSPs.SqlQuery(s).DefaultIfEmpty().ToList();
            else if (bang == "CTHDs") return db.CTHDs.SqlQuery(s).DefaultIfEmpty().ToList();
            else if (bang == "NguyenLieus") return db.NguyenLieus.SqlQuery(s).DefaultIfEmpty().ToList();
            else if (bang == "DSDatHangs") return db.DSDatHangs.SqlQuery(s).DefaultIfEmpty().ToList();
            else return db.PhieuNhaps.SqlQuery(s).DefaultIfEmpty().ToList();
        }
        public int Them(object a)
        {
            try
            {
                if (a.GetType().ToString().IndexOf("HoaDon") > 1) db.HoaDons.Add((HoaDon)a);
                else
                if (a.GetType().ToString().IndexOf("CTHD") > 1) db.CTHDs.Add((CTHD)a);
                else
                if (a.GetType().ToString().IndexOf("Ban") > 1) db.Bans.Add((Ban)a);
                else
                if (a.GetType().ToString().IndexOf("SanPham") > 1) db.SanPhams.Add((SanPham)a);
                else
                if (a.GetType().ToString().IndexOf("NguoiDung") > 1) db.NguoiDungs.Add((NguoiDung)a);
                else
                if (a.GetType().ToString().IndexOf("Loai") > 1) db.LoaiSPs.Add((LoaiSP)a);
                else
                if (a.GetType().ToString().IndexOf("NguyenLieu") > 1) db.NguyenLieus.Add((NguyenLieu)a);
                else
                if (a.GetType().ToString().IndexOf("PhieuNhap") > 1) db.PhieuNhaps.Add((PhieuNhap)a);
                else 
                if (a.GetType().ToString().IndexOf("DSDatHang") > 1) db.DSDatHangs.Add((DSDatHang)a);
                db.SaveChanges();
                return 1;
            }
            catch { return 0; }
        }
        public int Sua(object a)
        {
            try
            {
                db = new CafeEntities();
                db.Entry(a).State = System.Data.EntityState.Modified;
                db.SaveChanges();
                return 1;
            }
            catch { return 0; }

        }
    }
}
