using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace API.Models
{
    public class SinhVien_LopHocPhanModel
    {
        public string Ma_sv { get; set; }
        public string ho_dem { get; set; }
        public string ten { get; set; }
        public byte gioi_tinh { get; set; }
        public DateTime ngay_sinh { get; set; }
        public string ghi_chu { get; set; }
        public string svlhp_id { get; set; }
        public string lhp_key_id { get; set; }
        public byte svlhp_nhom { get; set; }
        public string lhp_ma_lhp { get; set; }
        public string lhp_lop_hoc { get; set; }
        public string lhp_ten_mon_hoc { get; set; }
        public string lhp_nam_hk { get; set; }

    }
}