using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace API.Models
{
    public class LichHocModel
    {
        public string lh_id { get; set; }
        public string lh_tiet { get; set; }
        public byte lh_nhom { get; set; }
        public DateTime lh_ngay_bat_dau { get; set; }
        public string lh_phong { get; set; }
        public string lh_giang_vien { get; set; }
        public string lh_ghi_chu { get; set; }
        public string lhp_key_id { get; set; }
    }
}