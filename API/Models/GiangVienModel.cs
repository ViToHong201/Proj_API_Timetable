using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace API.Models
{
    public class GiangVienModel
    {
        public string gv_id { get; set; }
        public string gv_ho_ten { get; set; }
        public byte gv_gioi_tinh { get; set; }
        public DateTime gv_ngay_sinh { get; set; }
        public string gv_ghi_chu { get; set; }
    }
}