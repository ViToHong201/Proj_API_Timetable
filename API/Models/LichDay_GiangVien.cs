using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace API.Models
{
    public class LichDay_GiangVien
    {
        public string gv_ho_ten { get; set; }
        public string th_tuan { get; set; }
        public string lh_tiet { get; set; }
        public DateTime ngay_day { get; set; }
        public int lh_nhom { get; set; }
        public string lh_phong { get; set; }
        public string lhp_ten_mon_hoc { get; set; }
        public string lhp_nam_hk { get; set; }
        public string si_so { get; set; }
        public string lh_ghi_chu { get; set; }
        public string gv_ghi_chu { get; set; }

    }
}