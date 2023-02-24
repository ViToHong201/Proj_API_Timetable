using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace API.Models
{
    public class SinhVienModel
    {
        public string Ma_sv { get; set; }
        public string ho_dem { get; set; }
        public string ten { get; set; }
        public byte gioi_tinh { get; set; }
        public DateTime ngay_sinh { get; set; }
        public string ghi_chu { get; set; }
        public int nhom { get; set; }
    }
    

}

