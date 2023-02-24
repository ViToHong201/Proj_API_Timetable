using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using API.Models;

namespace API.Controllers
{
    public class SinhVienController : ApiController 
    {

        private ConnectSever con = new ConnectSever();
        

        // Lấy danh sách sinh viên lớp học phần theo nhóm
        [HttpGet]
        [Route("api/dssinhvien/{ma_lhp}/{nhom?}")]
        public List<SinhVien_LopHocPhanModel> SinhVienLopHocPhanNhom(string ma_lhp, int nhom = 0)
        {
            con.OpenConnection();
            List<SinhVien_LopHocPhanModel> ls = new List<SinhVien_LopHocPhanModel>();

            SqlCommand cm = new SqlCommand();
            cm.CommandText = @"
                               select sv.sv_ma_sv, sv.sv_ho_dem, sv.sv_ten, sv.sv_gioi_tinh, sv.sv_ngay_sinh, lhp.lhp_ma_lhp, lhp.lhp_ten_mon_hoc, svlhp.svlhp_nhom, lhp.lhp_nam_hk, sv.sv_ghi_chu 
                                from	sinh_vien sv, lop_hoc_phan lhp, sinh_vien_lop_hoc_phan svlhp
                                where	sv.sv_ma_sv = svlhp.sv_ma_sv
		                                and lhp.lhp_key_id = svlhp.lhp_key_id
		                                and lhp.lhp_ma_lhp = @ma_lhp
                                        and svlhp.svlhp_nhom = @nhom 
                                ";
            cm.Parameters.Add(new SqlParameter("ma_lhp", ma_lhp));
            cm.Parameters.Add(new SqlParameter("nhom", nhom));
            cm.Connection = con.sqlCon;

            var a = cm.ExecuteReader();

            while (a.Read())
            {
                SinhVien_LopHocPhanModel sv = new SinhVien_LopHocPhanModel();
                sv.Ma_sv = a[0].ToString();
                sv.ho_dem = a[1].ToString();
                sv.ten = a[2].ToString();
                sv.gioi_tinh = Convert.ToByte(a[3]);
                sv.ngay_sinh = Convert.ToDateTime(a[4]);
                sv.lhp_ma_lhp = a[5].ToString();
                sv.lhp_ten_mon_hoc = a[6].ToString();
                sv.svlhp_nhom = Convert.ToByte(a[7]);
                sv.lhp_nam_hk = a[8].ToString();
                sv.ghi_chu = a[9].ToString();

                ls.Add(sv);

            }
            con.CloseConnection();

            return ls;
        }

    }
}