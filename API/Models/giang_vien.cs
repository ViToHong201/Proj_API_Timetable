//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace API.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class giang_vien
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public giang_vien()
        {
            this.giang_vien_lop_hoc_phan = new HashSet<giang_vien_lop_hoc_phan>();
        }
    
        public string gv_id { get; set; }
        public string gv_ho_ten { get; set; }
        public byte gv_gioi_tinh { get; set; }
        public System.DateTime gv_ngay_sinh { get; set; }
        public string gv_ghi_chu { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<giang_vien_lop_hoc_phan> giang_vien_lop_hoc_phan { get; set; }
    }
}
