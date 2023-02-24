using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using API.Models;

namespace API.Controllers
{
    public class GiaoVuController : ApiController
    {
        private ConnectSever con = new ConnectSever();

        // cập nhật trạng thái lịch học của lớp học phần theo ngày và nhóm
        // 0 là lịch tạm ngưng
        // 1 là lịch học 
        // 2 là lịch học bù
        // 3 là lịch thi
        [HttpPut]
        [Route("api/sualichhoc/{ma_lhp}/{trang_thai}/{ngay:datetime}/{nhom?}")]
        public int CapNhapLichHoc(string ma_lhp, DateTime ngay, int trang_thai, int nhom = 0)
        {
            con.OpenConnection();

            SqlCommand cm = new SqlCommand();

            cm.CommandText = @" update tuan_hoc 
                                set th_trang_thai = @trang_thai
                                where EXISTS (
                                                select  *
                                                from lop_hoc_phan lhp,lich_hoc
                                                where 
                                                lich_hoc.lhp_key_id = lhp.lhp_key_id 
                                                and tuan_hoc.lh_id = lich_hoc.lh_id
                                                and lhp.lhp_ma_lhp = @ma_lhp
                                                and lich_hoc.lh_nhom = @nhom
                                                and tuan_hoc.th_ngay = @ngay
								)
                                ";

            cm.Parameters.Add(new SqlParameter("ma_lhp", ma_lhp));
            cm.Parameters.Add(new SqlParameter("nhom", nhom));
            cm.Parameters.Add(new SqlParameter("ngay", ngay));
            cm.Parameters.Add(new SqlParameter("trang_thai", trang_thai));

            cm.Connection = con.sqlCon;
            var a = cm.ExecuteNonQuery();


            con.CloseConnection();

            return a;

        }

        // cập nhật trạng thái tất cả lịch học theo ngày truyền vào
        // 0 là lịch tạm ngưng
        // 1 là lịch học 
        // 2 là lịch học bù
        // 3 là lịch thi
        [HttpPut]
        [Route("api/sualichhoc/{trang_thai}/{ngay:datetime}")]
        public int CapNhapLichHoc(DateTime ngay, int trang_thai)
        {
            con.OpenConnection();

            SqlCommand cm = new SqlCommand();

            cm.CommandText = @" update tuan_hoc 
                                set th_trang_thai = @trang_thai
                                where EXISTS (
                                                select  *
                                                from lop_hoc_phan lhp,lich_hoc
                                                where 
                                                lich_hoc.lhp_key_id = lhp.lhp_key_id 
                                                and tuan_hoc.lh_id = lich_hoc.lh_id
                                                and tuan_hoc.th_ngay = @ngay
								)
                                ";

            cm.Parameters.Add(new SqlParameter("ngay", ngay));
            cm.Parameters.Add(new SqlParameter("trang_thai", trang_thai));

            cm.Connection = con.sqlCon;
            var a = cm.ExecuteNonQuery();


            con.CloseConnection();

            return a;

        }

        // xóa tất cả lịch học theo ngày truyền vào
        [HttpDelete]
        [Route("api/xoalichhoc/{ngay:datetime}")]
        public int XoaLichHoc(DateTime ngay)
        {
            con.OpenConnection();

            SqlCommand cm = new SqlCommand();

            cm.CommandText = @" delete from tuan_hoc
                                where exists(
			                                    select	lhp.lhp_lop_hoc, lhp.lhp_ten_mon_hoc, lh.lh_tiet, lh.lh_phong, lh.lh_giang_vien
			                                    from	lop_hoc_phan lhp, lich_hoc lh
			                                    where 
					                                    lh.lhp_key_id = lhp.lhp_key_id 
					                                    and tuan_hoc.lh_id = lh.lh_id
					                                    and tuan_hoc.th_ngay = @ngay
								               )
                               ";
            cm.Parameters.Add(new SqlParameter("ngay", ngay));

            cm.Connection = con.sqlCon;
            var a = cm.ExecuteNonQuery();


            con.CloseConnection();


            return 1;
        }
    }
}