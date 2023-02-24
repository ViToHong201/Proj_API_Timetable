using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using API.Models;
using System.Data;
using System.IO;
using System.Data.SqlClient;
using LinqToExcel;
using System.Data.Entity.Validation;
using System.Data.OleDb;
using DocumentFormat.OpenXml.Spreadsheet;
using OfficeOpenXml;

using System.Drawing;
using OfficeOpenXml.Style;
using OfficeOpenXml.Table;

namespace MVCImportExcel.Controllers
{
   
    public class TestController : Controller
    {

        private DataTable ReadFromExcelfile(string path, string sheetName)
        {
            // Khởi tạo data table
            DataTable dt = new DataTable();
            // Load file excel và các setting ban đầu
            using (ExcelPackage package = new ExcelPackage(new FileInfo(path)))
            {
                if (package.Workbook.Worksheets.Count < 1)
                {
                    // Log - Không có sheet nào tồn tại trong file excel của bạn
                    return null;
                }
                // Khởi Lấy Sheet đầu tiện trong file Excel để truy vấn, truyền vào name của Sheet để lấy ra sheet cần, nếu name = null thì lấy sheet đầu tiên
                ExcelWorksheet workSheet = package.Workbook.Worksheets.FirstOrDefault(x => x.Name == sheetName) ?? package.Workbook.Worksheets.FirstOrDefault();
                // Đọc tất cả các header
                foreach (var firstRowCell in workSheet.Cells[1, 1, 1, workSheet.Dimension.End.Column])
                {
                    dt.Columns.Add(firstRowCell.Text);
                }
                // Đọc tất cả data bắt đầu từ row thứ 2
                for (var rowNumber = 2; rowNumber <= workSheet.Dimension.End.Row; rowNumber++)
                {
                    // Lấy 1 row trong excel để truy vấn
                    var row = workSheet.Cells[rowNumber, 1, rowNumber, workSheet.Dimension.End.Column];
                    // tạo 1 row trong data table
                    var newRow = dt.NewRow();
                    foreach (var cell in row)
                    {
                        newRow[cell.Start.Column - 1] = cell.Text;

                    }
                    dt.Rows.Add(newRow);
                }
            }
            return dt;
        }
        private thoi_khoa_bieu_sinh_vienEntities db = new thoi_khoa_bieu_sinh_vienEntities();
        // GET: User
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult ReadFromExcel()
        {
            var data = ReadFromExcelfile(@"D:\ExcelDemo.xlsx", "First Sheet");
            return View(data);
        }

        /// <summary>
        /// This function is used to download excel format.
        /// </summary>
        /// <param name="Path"></param>
        /// <returns>file</returns>
        public FileResult DownloadExcel()
        {
            string path = "/Doc/Users.xlsx";
            return File(path, "application/vnd.ms-excel", "Users.xlsx");
        }

        [HttpPost]
        public JsonResult UploadExcel(sinh_vien users, HttpPostedFileBase FileUpload)
        {

            List<string> data = new List<string>();
            if (FileUpload != null)
            {
                // tdata.ExecuteCommand("truncate table OtherCompanyAssets");
                if (FileUpload.ContentType == "application/vnd.ms-excel" || FileUpload.ContentType == "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet")
                {
                    string filename = FileUpload.FileName;
                    string targetpath = Server.MapPath("~/Doc/");
                    FileUpload.SaveAs(targetpath + filename);
                    string pathToExcelFile = targetpath + filename;
                    var connectionString = "";
                    if (filename.EndsWith(".xls"))
                    {
                        connectionString = string.Format("Provider=Microsoft.Jet.OLEDB.4.0; data source={0}; Extended Properties=Excel 8.0;", pathToExcelFile);
                    }
                    else if (filename.EndsWith(".xlsx"))
                    {
                        connectionString = string.Format("Provider=Microsoft.ACE.OLEDB.12.0;Data Source={0};Extended Properties=\"Excel 12.0 Xml;HDR=YES;IMEX=1\";", pathToExcelFile);
                    }
                    return Json(data, JsonRequestBehavior.AllowGet);


                    //var adapter = new OleDbDataAdapter("SELECT * FROM [Sheet1$]", connectionString);
                    //var ds = new DataSet();
                    //adapter.Fill(ds, "ExcelTable");
                    //DataTable dtable = ds.Tables["ExcelTable"];
                    //string sheetName = "Sheet1";
                    //var excelFile = new ExcelQueryFactory(pathToExcelFile);
                    //var artistAlbums = from a in excelFile.Worksheet<sinh_vien>(sheetName) select a;
                    //foreach (var a in artistAlbums)
                    //{
                    //    try
                    //    {
                    //        if (a.sv_ma_sv != "" && a.sv_ten != "" )
                    //        {
                    //            sinh_vien TU = new sinh_vien();
                    //            TU.sv_ten = a.sv_ten;
                    //            TU.sv_ho_dem = a.sv_ho_dem;
                    //            db.sinh_vien.Add(TU);
                    //            db.SaveChanges();
                    //        }
                    //        else
                    //        {
                    //            data.Add("<ul>");
                    //            if (a.sv_ten == "" || a.sv_ten == null) data.Add("<li> name is required</li>");

                    //            data.Add("</ul>");
                    //            data.ToArray();
                    //            return Json(data, JsonRequestBehavior.AllowGet);
                    //        }
                    //    }
                    //    catch (DbEntityValidationException ex)
                    //    {
                    //        foreach (var entityValidationErrors in ex.EntityValidationErrors)
                    //        {
                    //            foreach (var validationError in entityValidationErrors.ValidationErrors)
                    //            {
                    //                Response.Write("Property: " + validationError.PropertyName + " Error: " + validationError.ErrorMessage);
                    //            }
                    //        }
                    //        break;
                    //    }
                    //}
                    ////deleting excel file from folder
                    //if ((System.IO.File.Exists(pathToExcelFile)))
                    //{
                    //    System.IO.File.Delete(pathToExcelFile);
                    //}
                    //return Json("success", JsonRequestBehavior.AllowGet);
                }
                else
                {
                    //alert message for invalid file format
                    data.Add("<ul>");
                    data.Add("<li>Only Excel file format is allowed</li>");
                    data.Add("</ul>");
                    data.ToArray();
                    return Json(data, JsonRequestBehavior.AllowGet);
                }
            }
            else
            {
                data.Add("<ul>");
                if (FileUpload == null) data.Add("<li>Please choose Excel file</li>");
                data.Add("</ul>");
                data.ToArray();
                return Json(data, JsonRequestBehavior.AllowGet);
            }
        }
    }
}