using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace API.Models
{
    public class LichHocCuaSinhVien
    {
        public string th_tuan { get; set; }
        public string Ma_sv { get; set; }
        public string ho_dem { get; set; }
        public string ten { get; set; }
        public string lhp_lop_hoc { get; set; }
        public string lhp_ten_mon_hoc { get; set; }
        public string lh_tiet { get; set; }
        public int svlhp_nhom { get; set; }
        public string lh_phong { get; set; }
        public DateTime ngay_hoc { get; set; }
        public string lh_giang_vien { get; set; }
        public int th_trang_thai { get; set; }
        public string lhp_nam_hk {get; set;}
        public string lh_ghi_chu { get; set; }
    }
}