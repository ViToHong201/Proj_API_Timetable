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
    
    public partial class lich_hoc
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public lich_hoc()
        {
            this.tuan_hoc = new HashSet<tuan_hoc>();
        }
    
        public string lh_id { get; set; }
        public string lh_tiet { get; set; }
        public byte lh_nhom { get; set; }
        public Nullable<System.DateTime> lh_ngay_bat_dau { get; set; }
        public string lh_phong { get; set; }
        public string lh_giang_vien { get; set; }
        public string lh_ghi_chu { get; set; }
        public string lhp_key_id { get; set; }
    
        public virtual lop_hoc_phan lop_hoc_phan { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tuan_hoc> tuan_hoc { get; set; }
    }
}
