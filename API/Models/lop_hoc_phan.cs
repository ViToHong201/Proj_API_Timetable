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
    
    public partial class lop_hoc_phan
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public lop_hoc_phan()
        {
            this.giang_vien_lop_hoc_phan = new HashSet<giang_vien_lop_hoc_phan>();
            this.lich_hoc = new HashSet<lich_hoc>();
            this.sinh_vien_lop_hoc_phan = new HashSet<sinh_vien_lop_hoc_phan>();
        }
    
        public string lhp_key_id { get; set; }
        public string lhp_ma_lhp { get; set; }
        public string lhp_lop_hoc { get; set; }
        public string lhp_ten_mon_hoc { get; set; }
        public string lhp_nam_hk { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<giang_vien_lop_hoc_phan> giang_vien_lop_hoc_phan { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<lich_hoc> lich_hoc { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<sinh_vien_lop_hoc_phan> sinh_vien_lop_hoc_phan { get; set; }
    }
}
