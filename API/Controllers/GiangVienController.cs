using API.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Data;




namespace API.Controllers
{
    public class GiangVienController : ApiController
    {
        private ConnectSever con = new ConnectSever();
        [HttpGet]
        [Route("api/dsgiangvien")]
        public List<GiangVienModel> DanhSachGiangVien()
        {
            con.OpenConnection();

            List<GiangVienModel> ls = new List<GiangVienModel>();

            SqlCommand cm = new SqlCommand();
            cm.CommandText = " Select * From dbo.giang_vien";
            cm.Connection = con.sqlCon;
            var a = cm.ExecuteReader();

            while (a.Read())
            {
                GiangVienModel gv = new GiangVienModel();
                gv.gv_id = a[0].ToString();
                gv.gv_ho_ten = a[1].ToString();
                gv.gv_gioi_tinh = Convert.ToByte(a[2]);
                gv.gv_ngay_sinh = Convert.ToDateTime(a[3]);
                gv.gv_ghi_chu = a[4].ToString();

                ls.Add(gv);

            }

            con.CloseConnection();
            return ls.ToList();
        }

    }
}