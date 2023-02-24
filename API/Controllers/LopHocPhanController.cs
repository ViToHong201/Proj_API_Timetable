using API.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace API.Controllers
{
    public class LopHocPhanController : ApiController
    {
        private ConnectSever con = new ConnectSever();


        // Lấy danh sách lớp học phần theo năm - học kỳ
        [HttpGet]
        [Route("api/dslophocphan/{nam_hky}")]
        public List<LopHocPhanModel> DsLopHocPhan(string nam_hky)
        {
            con.OpenConnection();

            List<LopHocPhanModel> ls = new List<LopHocPhanModel>();

            SqlCommand cm = new SqlCommand();
            cm.CommandText = @"
                                select  lhp.lhp_ma_lhp, lhp.lhp_lop_hoc, lhp.lhp_ten_mon_hoc, lhp.lhp_nam_hk
                                from    lop_hoc_phan lhp
                                where	lhp.lhp_nam_hk = @nam_hky
                                ";

            cm.Parameters.Add(new SqlParameter("nam_hky", nam_hky));
            cm.Connection = con.sqlCon;
            var a = cm.ExecuteReader();

            while (a.Read())
            {
                LopHocPhanModel lhp = new LopHocPhanModel();

                lhp.lhp_ma_lhp = a[0].ToString();
                lhp.lhp_lop_hoc = a[1].ToString();
                lhp.lhp_ten_mon_hoc = a[2].ToString();
                lhp.lhp_nam_hk = a[3].ToString();

                ls.Add(lhp);
            }
            con.CloseConnection();
            return ls;
        }


        // lấy danh sách lớp học phần theo mã sinh viên
        [HttpGet]
        [Route("api/lhp_sv/{ma_sv}")]
        public List<LopHocPhanModel> DSLopHocPhan_SinhVIen(string ma_sv)
        {
            List<LopHocPhanModel> ls = new List<LopHocPhanModel>();
            con.OpenConnection();
            SqlCommand cm = new SqlCommand();
            cm.CommandText = @"
                                select  lhp.lhp_ma_lhp, lhp.lhp_ten_mon_hoc, lhp.lhp_nam_hk, lhp.lhp_lop_hoc
                                from	sinh_vien sv, lop_hoc_phan lhp, sinh_vien_lop_hoc_phan svlhp
                                where	sv.sv_ma_sv = svlhp.sv_ma_sv
		                                and lhp.lhp_key_id = svlhp.lhp_key_id
		                                and sv.sv_ma_sv = @ma_sv
                                ";

            cm.Parameters.Add(new SqlParameter("ma_sv", ma_sv));
            cm.Connection = con.sqlCon;

            var a = cm.ExecuteReader();

            while (a.Read())
            {
                LopHocPhanModel lhp = new LopHocPhanModel();
                lhp.lhp_ma_lhp = a[0].ToString();
                lhp.lhp_ten_mon_hoc = a[1].ToString();
                lhp.lhp_nam_hk = a[2].ToString();
                lhp.lhp_lop_hoc = a[3].ToString();

                ls.Add(lhp);
            }

            con.CloseConnection();
            return ls;
        }
    }
}