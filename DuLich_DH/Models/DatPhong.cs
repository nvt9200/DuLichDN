//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DuLich_DH.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class DatPhong
    {
        public int MaDP { get; set; }
        public string TenKhachHang { get; set; }
        public string SDT { get; set; }
        public Nullable<int> MaKS { get; set; }
        public string GhiChu { get; set; }
        public Nullable<bool> TinhTrang { get; set; }
    
        public virtual KhachSan KhachSan { get; set; }
    }
}
