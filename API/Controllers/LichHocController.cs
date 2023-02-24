using API.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Globalization;

namespace API.Controllers
{
    public class LichHocController : ApiController
    {
        private ConnectSever con = new ConnectSever();

        // phần lịch học sinh viên
        #region
        
        // lấy tất cả lịch học của sinh viên theo năm - học kỳ
        [HttpGet]
        [Route("api/lichhoc/{ma_sv}/{nam_hk}/all")]
        public List<LichHocCuaSinhVien> All_LichHocSinhVien(string ma_sv, string nam_hk)
        {

            con.OpenConnection();

            List<LichHocCuaSinhVien> ls = new List<LichHocCuaSinhVien>();


            SqlCommand cm = new SqlCommand();
            cm.CommandText = @"
                                select  th.th_tuan, sv.sv_ma_sv, sv.sv_ho_dem, sv.sv_ten, lhp.lhp_lop_hoc, lhp.lhp_ten_mon_hoc, lh.lh_tiet, lh.lh_nhom, lh.lh_phong, th.th_ngay as ngay_hoc, lh.lh_giang_vien, th.th_trang_thai, lhp.lhp_nam_hk, lh.lh_ghi_chu
                                from sinh_vien sv, sinh_vien_lop_hoc_phan svlhp, lop_hoc_phan lhp, dbo.lich_hoc lh, tuan_hoc th
                                where		sv.sv_ma_sv = svlhp.sv_ma_sv 
			                                and svlhp.lhp_key_id = lhp.lhp_key_id 
			                                and lh.lhp_key_id = lhp.lhp_key_id 
			                                and lh.lh_nhom = svlhp.svlhp_nhom
			                                and th.lh_id = lh.lh_id
			                                and sv.sv_ma_sv = @Ma_Sv 
                                            and lhp.lhp_nam_hk = @nam_hk
                               ";
            cm.Parameters.Add(new SqlParameter("Ma_Sv", ma_sv));
            cm.Parameters.Add(new SqlParameter("nam_hk", nam_hk));
            cm.Connection = con.sqlCon;
            var a = cm.ExecuteReader();

            while (a.Read())
            {
                LichHocCuaSinhVien lh = new LichHocCuaSinhVien();


                lh.th_tuan = a[0].ToString();
                lh.Ma_sv = a[1].ToString();
                lh.ho_dem = a[2].ToString();
                lh.ten = a[3].ToString();
                lh.lhp_lop_hoc = a[4].ToString();
                lh.lhp_ten_mon_hoc = a[5].ToString();
                lh.lh_tiet = a[6].ToString();
                lh.svlhp_nhom = Convert.ToByte(a[7]);
                lh.lh_phong = a[8].ToString();
                lh.ngay_hoc = Convert.ToDateTime(a[9]);
                lh.lh_giang_vien = a[10].ToString();
                lh.th_trang_thai = Convert.ToInt32(a[11]);
                lh.lhp_nam_hk = a[12].ToString();
                lh.lh_ghi_chu = a[13].ToString();

                ls.Add(lh);
            }
            con.CloseConnection();



            return ls;
        }

        // lấy lịch học của sinh viên theo tuần hiên hành hoặc các tuần tiếp theo
        [HttpGet]
        [Route("api/lichhoc/{ma_sv}/{tuan?}")] 
        public List<LichHocCuaSinhVien> LichHocSinhVien(string ma_sv, int tuan = 0)
        {

            //int tuan = 0;
            con.OpenConnection();

            List<LichHocCuaSinhVien> ls = new List<LichHocCuaSinhVien>();
           

            SqlCommand cm = new SqlCommand();
            cm.CommandText = @"
                                select  th.th_tuan, sv.sv_ma_sv, sv.sv_ho_dem, sv.sv_ten, lhp.lhp_lop_hoc, lhp.lhp_ten_mon_hoc, lh.lh_tiet, lh.lh_nhom, lh.lh_phong, th.th_ngay as ngay_hoc, lh.lh_giang_vien, th.th_trang_thai, lhp.lhp_nam_hk, lh.lh_ghi_chu
                                from sinh_vien sv, sinh_vien_lop_hoc_phan svlhp, lop_hoc_phan lhp, dbo.lich_hoc lh, tuan_hoc th
                                where		sv.sv_ma_sv = svlhp.sv_ma_sv 
			                                and svlhp.lhp_key_id = lhp.lhp_key_id 
			                                and lh.lhp_key_id = lhp.lhp_key_id 
			                                and lh.lh_nhom = svlhp.svlhp_nhom
			                                and th.lh_id = lh.lh_id
			                                and sv.sv_ma_sv = @Ma_Sv 
                                            and th.th_ngay between @ngay_dau_tuan and @ngay_cuoi_tuan
                               ";
            cm.Parameters.Add(new SqlParameter("Ma_Sv",ma_sv));
            cm.Parameters.Add(new SqlParameter("ngay_dau_tuan", GetFirstDayOfWeek().AddDays(tuan*7)));
            cm.Parameters.Add(new SqlParameter("ngay_cuoi_tuan", GetFirstDayOfWeek().AddDays(tuan*7+6)));
            cm.Connection = con.sqlCon;
            var a = cm.ExecuteReader();

            while (a.Read())
            {
                LichHocCuaSinhVien lh = new LichHocCuaSinhVien();
              

                lh.th_tuan = a[0].ToString();
                lh.Ma_sv = a[1].ToString();
                lh.ho_dem = a[2].ToString();
                lh.ten = a[3].ToString();
                lh.lhp_lop_hoc = a[4].ToString();
                lh.lhp_ten_mon_hoc = a[5].ToString();
                lh.lh_tiet = a[6].ToString();
                lh.svlhp_nhom = Convert.ToByte(a[7]);
                lh.lh_phong = a[8].ToString();
                lh.ngay_hoc = Convert.ToDateTime(a[9]);
                lh.lh_giang_vien = a[10].ToString();
                lh.th_trang_thai = Convert.ToInt32(a[11]);
                lh.lhp_nam_hk = a[12].ToString();
                lh.lh_ghi_chu = a[13].ToString();

                ls.Add(lh);
            }
            con.CloseConnection();

           

            return ls;
        }

     
        // lấy lịch học trong tuần của sinh viên theo 1 ngày bất kỳ
        [HttpGet]
        [Route("api/lichhoc/{ma_sv}/ngay/{ngay:datetime}")]
        public List<LichHocCuaSinhVien> lichhocsinhvien(string ma_sv, DateTime ngay)
        {

            con.OpenConnection();
            
            List<LichHocCuaSinhVien> ls = new List<LichHocCuaSinhVien>();


            SqlCommand cm = new SqlCommand();
            cm.CommandText = @"
                                select  th.th_tuan, sv.sv_ma_sv, sv.sv_ho_dem, sv.sv_ten, lhp.lhp_lop_hoc, lhp.lhp_ten_mon_hoc, lh.lh_tiet, lh.lh_nhom, lh.lh_phong, th.th_ngay as ngay_hoc, lh.lh_giang_vien, th.th_trang_thai, lhp.lhp_nam_hk, lh.lh_ghi_chu
                                from sinh_vien sv, sinh_vien_lop_hoc_phan svlhp, lop_hoc_phan lhp, dbo.lich_hoc lh, tuan_hoc th
                                where		sv.sv_ma_sv = svlhp.sv_ma_sv 
                                   and svlhp.lhp_key_id = lhp.lhp_key_id 
                                   and lh.lhp_key_id = lhp.lhp_key_id 
                                   and lh.lh_nhom = svlhp.svlhp_nhom
                                   and th.lh_id = lh.lh_id
                                   and sv.sv_ma_sv = @ma_sv 
                                   and th.th_ngay between @ngay_dau_tuan and @ngay_cuoi_tuan
                               ";
            cm.Parameters.Add(new SqlParameter("ma_sv", ma_sv));
            cm.Parameters.Add(new SqlParameter("ngay_dau_tuan",GetFirstDayOfWeek(ngay)));
            cm.Parameters.Add(new SqlParameter("ngay_cuoi_tuan",GetFirstDayOfWeek(ngay).AddDays(6)));
            cm.Connection = con.sqlCon;
            var a = cm.ExecuteReader();

            while (a.Read())
            {
                LichHocCuaSinhVien lh = new LichHocCuaSinhVien();


                lh.th_tuan = a[0].ToString();
                lh.Ma_sv = a[1].ToString();
                lh.ho_dem = a[2].ToString();
                lh.ten = a[3].ToString();
                lh.lhp_lop_hoc = a[4].ToString();
                lh.lhp_ten_mon_hoc = a[5].ToString();
                lh.lh_tiet = a[6].ToString();
                lh.svlhp_nhom = Convert.ToByte(a[7]);
                lh.lh_phong = a[8].ToString();
                lh.ngay_hoc = Convert.ToDateTime(a[9]);
                lh.lh_giang_vien = a[10].ToString();
                lh.th_trang_thai = Convert.ToInt32(a[11]);
                lh.lhp_nam_hk = a[12].ToString();
                lh.lh_ghi_chu = a[13].ToString();

                ls.Add(lh);
            }
            con.CloseConnection();



            return ls;
        }

        #endregion


        // phần lịch dạy của giảng viên
        #region

        // lấy lịch dạy của giảng viên theo tuần hiện hành hoặc các tuần sau đó
        [HttpGet]
        [Route("api/lichday/{ma_gv}/{tuan?}")]
        public List<LichDay_GiangVien> DsLopHocPhan(string ma_gv, int tuan = 0)
        {
            con.OpenConnection();

            List<LichDay_GiangVien> ls = new List<LichDay_GiangVien>();


            SqlCommand cm = new SqlCommand();
            cm.CommandText = @"
                                select distinct gv.gv_ho_ten, th.th_tuan, lh.lh_tiet, th.th_ngay as ngay_day, lh.lh_nhom, lh.lh_phong, lh.lh_ngay_bat_dau, lhp.lhp_ten_mon_hoc, lhp.lhp_nam_hk, a.si_so, gv.gv_ghi_chu, lh.lh_ghi_chu
                                from	lich_hoc lh, giang_vien gv, giang_vien_lop_hoc_phan gvlhp, lop_hoc_phan lhp, tuan_hoc th, sinh_vien sv, sinh_vien_lop_hoc_phan svlhp, 
                                (
		                                select svl.lhp_key_id, svl.svlhp_nhom, count(svl.lhp_key_id) as si_so
		                                from sinh_vien_lop_hoc_phan svl
		                                group by svl.lhp_key_id, svl.svlhp_nhom
                                ) as a
                                where	lhp.lhp_key_id = gvlhp.lhp_key_id 
                                        and gv.gv_id = gvlhp.gv_id 
                                        and lhp.lhp_key_id = lh.lhp_key_id 
                                        and th.lh_id = lh.lh_id 
                                        and a.lhp_key_id = lhp.lhp_key_id 
                                        and a.svlhp_nhom = lh.lh_nhom 
                                        and gv.gv_id  = @Ma_gv
                                        and th.th_ngay between @ngay_dau_tuan and @ngay_cuoi_tuan
                            ";
            cm.Parameters.Add(new SqlParameter("Ma_gv", ma_gv));
            cm.Parameters.Add(new SqlParameter("ngay_dau_tuan", GetFirstDayOfWeek().AddDays(tuan * 7)));
            cm.Parameters.Add(new SqlParameter("ngay_cuoi_tuan", GetFirstDayOfWeek().AddDays(tuan * 7).AddDays(6)));
            cm.Connection = con.sqlCon;
            var a = cm.ExecuteReader();

            while (a.Read())
            {
                LichDay_GiangVien lh = new LichDay_GiangVien();

                lh.gv_ho_ten = a[0].ToString();
                lh.th_tuan = a[1].ToString();
                lh.lh_tiet = a[2].ToString();
                lh.ngay_day = Convert.ToDateTime(a[3]);
                lh.lh_nhom = Convert.ToByte(a[4]);
                lh.lh_phong = a[5].ToString();
                lh.lhp_ten_mon_hoc = a[7].ToString();
                lh.lhp_nam_hk = a[8].ToString();
                lh.si_so = a[9].ToString();
                lh.lh_ghi_chu = a[10].ToString();
                lh.gv_ghi_chu = a[11].ToString();

                ls.Add(lh);
            }
            con.CloseConnection();
            return ls;
        }



        // lấy lịch dạy trong tuần của giảng viên khi chọn 1 ngày bất kỳ
        [HttpGet]
        [Route("api/lichday/{ma_gv}/ngay/{ngay:datetime}")]
        public List<LichDay_GiangVien> DsLopHocPhan(string ma_gv, DateTime ngay)
        {
            con.OpenConnection();

            List<LichDay_GiangVien> ls = new List<LichDay_GiangVien>();


            SqlCommand cm = new SqlCommand();
            cm.CommandText = @"
                                select distinct gv.gv_ho_ten, th.th_tuan, lh.lh_tiet, th.th_ngay as ngay_day, lh.lh_nhom, lh.lh_phong, lh.lh_ngay_bat_dau, lhp.lhp_ten_mon_hoc, lhp.lhp_nam_hk, a.si_so, gv.gv_ghi_chu, lh.lh_ghi_chu
                                from	lich_hoc lh, giang_vien gv, giang_vien_lop_hoc_phan gvlhp, lop_hoc_phan lhp, tuan_hoc th, sinh_vien sv, sinh_vien_lop_hoc_phan svlhp, 
                                (
		                                select svl.lhp_key_id, svl.svlhp_nhom, count(svl.lhp_key_id) as si_so
		                                from sinh_vien_lop_hoc_phan svl
		                                group by svl.lhp_key_id, svl.svlhp_nhom
                                ) as a
                                where	lhp.lhp_key_id = gvlhp.lhp_key_id 
                                        and gv.gv_id = gvlhp.gv_id 
                                        and lhp.lhp_key_id = lh.lhp_key_id 
                                        and th.lh_id = lh.lh_id 
                                        and a.lhp_key_id = lhp.lhp_key_id 
                                        and a.svlhp_nhom = lh.lh_nhom 
                                        and gv.gv_id  = @Ma_gv
                                        and th.th_ngay between @ngay_dau_tuan and @ngay_cuoi_tuan
                            ";
            cm.Parameters.Add(new SqlParameter("Ma_gv", ma_gv));
            cm.Parameters.Add(new SqlParameter("ngay_dau_tuan", GetFirstDayOfWeek(ngay)));
            cm.Parameters.Add(new SqlParameter("ngay_cuoi_tuan", GetFirstDayOfWeek(ngay).AddDays(6)));
            cm.Connection = con.sqlCon;
            var a = cm.ExecuteReader();

            while (a.Read())
            {
                LichDay_GiangVien lh = new LichDay_GiangVien();

                lh.gv_ho_ten = a[0].ToString();
                lh.th_tuan = a[1].ToString();
                lh.lh_tiet = a[2].ToString();
                lh.ngay_day = Convert.ToDateTime(a[3]);
                lh.lh_nhom = Convert.ToByte(a[4]);
                lh.lh_phong = a[5].ToString();
                lh.lhp_ten_mon_hoc = a[7].ToString();
                lh.lhp_nam_hk = a[8].ToString();
                lh.si_so = a[9].ToString();
                lh.lh_ghi_chu = a[10].ToString();
                lh.gv_ghi_chu = a[11].ToString();

                ls.Add(lh);
            }
            con.CloseConnection();
            return ls;
        }


        // lấy lịch dạy của giảng viên theo năm - học kỳ
        [HttpGet]
        [Route("api/lichday/{ma_gv}/{nam_hk}")]
        public List<LichDay_GiangVien> DsLopHocPhan(string ma_gv, string nam_hk)
        {
            con.OpenConnection();

            List<LichDay_GiangVien> ls = new List<LichDay_GiangVien>();


            SqlCommand cm = new SqlCommand();
            cm.CommandText = @"
                                select distinct gv.gv_ho_ten, th.th_tuan, lh.lh_tiet, th.th_ngay as ngay_day, lh.lh_nhom, lh.lh_phong, lh.lh_ngay_bat_dau, lhp.lhp_ten_mon_hoc, lhp.lhp_nam_hk, a.si_so, gv.gv_ghi_chu, lh.lh_ghi_chu
                                from	lich_hoc lh, giang_vien gv, giang_vien_lop_hoc_phan gvlhp, lop_hoc_phan lhp, tuan_hoc th, sinh_vien sv, sinh_vien_lop_hoc_phan svlhp, 
                                (
		                                select svl.lhp_key_id, svl.svlhp_nhom, count(svl.lhp_key_id) as si_so
		                                from sinh_vien_lop_hoc_phan svl
		                                group by svl.lhp_key_id, svl.svlhp_nhom
                                ) as a
                                where	lhp.lhp_key_id = gvlhp.lhp_key_id 
                                        and gv.gv_id = gvlhp.gv_id 
                                        and lhp.lhp_key_id = lh.lhp_key_id 
                                        and th.lh_id = lh.lh_id 
                                        and a.lhp_key_id = lhp.lhp_key_id 
                                        and a.svlhp_nhom = lh.lh_nhom 
                                        and gv.gv_id  = @Ma_gv
                                        and lhp.lhp_nam_hk =@nam_hk
                            ";
            cm.Parameters.Add(new SqlParameter("Ma_gv", ma_gv));
            cm.Parameters.Add(new SqlParameter("nam_hk", nam_hk));
            cm.Connection = con.sqlCon;
            var a = cm.ExecuteReader();

            while (a.Read())
            {
                LichDay_GiangVien lh = new LichDay_GiangVien();

                lh.gv_ho_ten = a[0].ToString();
                lh.th_tuan = a[1].ToString();
                lh.lh_tiet = a[2].ToString();
                lh.ngay_day = Convert.ToDateTime(a[3]);
                lh.lh_nhom = Convert.ToByte(a[4]);
                lh.lh_phong = a[5].ToString();
                lh.lhp_ten_mon_hoc = a[7].ToString();
                lh.lhp_nam_hk = a[8].ToString();
                lh.si_so = a[9].ToString();
                lh.lh_ghi_chu = a[10].ToString();
                lh.gv_ghi_chu = a[11].ToString();

                ls.Add(lh);
            }
            con.CloseConnection();
            return ls;
        }

        #endregion


        // hàm xử lý ngày
        #region

        // hàm lấy ngày đầu tuần của ngày hiện hành
        private DateTime GetFirstDayOfWeek()
        {
            DayOfWeek firstDay = CultureInfo.CurrentCulture.DateTimeFormat.FirstDayOfWeek;
            DateTime firstDayInWeek = DateTime.Now;
            while (firstDayInWeek.DayOfWeek != firstDay)
            {
                firstDayInWeek = firstDayInWeek.AddDays(-1);
            }
            return firstDayInWeek;
        }

        // hàm lấy ngày đầu tuần của ngày bất kỳ
        private DateTime GetFirstDayOfWeek(DateTime dayInWeek)
        {
            CultureInfo defaultCultureInfo = CultureInfo.CurrentCulture;
            return GetFirstDayOfWeek(dayInWeek, defaultCultureInfo);
        }

        private DateTime GetFirstDayOfWeek(DateTime dayInWeek, CultureInfo cultureInfo)
        {
            DayOfWeek firstDay = cultureInfo.DateTimeFormat.FirstDayOfWeek;
            DateTime firstDayInWeek = dayInWeek.Date;
            while (firstDayInWeek.DayOfWeek != firstDay)
            {
                firstDayInWeek = firstDayInWeek.AddDays(-1);
            }
            return firstDayInWeek;
        }


        #endregion



    }
}