//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace QLcafe.Model
{
    using System;
    using System.Collections.Generic;
    
    public partial class SanPham
    {
        public SanPham()
        {
            this.CTHDs = new HashSet<CTHD>();
        }
    
        public string MaSanPham { get; set; }
        public string TenSanPham { get; set; }
        public string MaLoaiSP { get; set; }
        public int DonGiaBan { get; set; }
        public string DonVi { get; set; }
        public bool TrangThai { get; set; }
    
        public virtual ICollection<CTHD> CTHDs { get; set; }
        public virtual LoaiSP LoaiSP { get; set; }
    }
}
