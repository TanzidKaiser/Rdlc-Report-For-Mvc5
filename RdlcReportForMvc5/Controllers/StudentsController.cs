using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using RdlcReportForMvc5.Models;
using Microsoft.Reporting.WebForms;
using System.Web.UI.WebControls;
using System.IO;
using System.Threading;

namespace RdlcReportForMvc5.Controllers
{
    public class StudentsController : Controller
    {
        private DatabaseContext db = new DatabaseContext();

        // GET: Students
        public ActionResult Index()
        {
            return View(db.Students.ToList());
        }
        public ActionResult Save(Student student)
        {
            db.Students.Add(student);
            db.SaveChanges();
           // ExportTo(student);
            return Json("Save",JsonRequestBehavior.AllowGet);
        }
        private ActionResult ExportTo(Student models)
        {
            var model = new[] {

                new { Name =models.Name,Department = models.Department},
                new { Name =models.Name, Department = models.Department},
                new { Name =models.Name, Department = models.Department},
            }.ToList();

            //model.ID = 1;
            ReportDataSource reportDataSource = new ReportDataSource();
            reportDataSource.Name = "DataSet";
            reportDataSource.Value = model;

            string mimeType = string.Empty;
            string encodeing = string.Empty;
            string fileNameExtension = "pdf";            
            Warning[] warnings;
            string[] streams;

            LocalReport localReport = new LocalReport();
            localReport.ReportPath = Server.MapPath("~/Report/Report.rdlc");
            localReport.DataSources.Add(reportDataSource);

            byte[] bytes = localReport.Render("PDF", null, out mimeType, out encodeing, out fileNameExtension, out streams, out warnings);

            Response.Buffer = true;
            Response.Clear();
            Response.ContentType = mimeType;
            Response.AddHeader("content-disposition", "attachment;filename=file." + fileNameExtension);
            //Response.BinaryWrite(bytes);
            //Response.Flush();
            return File(bytes, fileNameExtension);
        }
        //[HttpPost]
        //public FileResult ExportTo(Student model)
        //{
        //    var a = db.Students.ToList();

        //    LocalReport localReport = new LocalReport();
        //    localReport.ReportPath = Server.MapPath("~/Report/Report.rdlc");
        //    ReportDataSource reportDataSource = new ReportDataSource();
        //    reportDataSource.Name = "DataSet";
        //    reportDataSource.Value = a;
        //    localReport.DataSources.Add(reportDataSource);

        //    string reportType = "PDF";
        //    string mimeType;
        //    string encodeing;
        //    //string fileNameExtension = (ReportType == "Excel") ? "xlsx" : (ReportType == "Word") ?"doc":"pdf";
        //    string fileNameExtension = "pdf";
        //    Warning[] warnings;
        //    string[] streams;
        //    byte[] renderBytes;

        //    renderBytes = localReport.Render(reportType, "", out mimeType, out encodeing,
        //        out fileNameExtension, out streams, out warnings);
        //    Response.AddHeader("content-disposition", "attachment; filename=file." + fileNameExtension);

        //    return File(renderBytes, fileNameExtension);
        //}
        //public ActionResult ExportToReport(string ReportType)
        //{
        //    var student = db.Students.ToList();
        //    LocalReport localReport = new LocalReport();
        //    localReport.ReportPath = Server.MapPath("~/Report/Report.rdlc");
        //    ReportDataSource reportDataSource = new ReportDataSource();
        //    reportDataSource.Name = "DataSet";
        //    reportDataSource.Value = student;
        //    localReport.DataSources.Add(reportDataSource);

        //    string reportType = "PDF";
        //    string mimeType;
        //    string encodeing;
        //    //string fileNameExtension = (ReportType == "Excel") ? "xlsx" : (ReportType == "Word") ?"doc":"pdf";
        //    string fileNameExtension = "pdf";
        //    Warning[] warnings;
        //    string[] streams;
        //    byte[] renderBytes;

        //    renderBytes = localReport.Render(reportType, "", out mimeType, out encodeing,
        //        out fileNameExtension, out streams, out warnings);
        //    Response.AddHeader("content-disposition", "attachment; filename=file." + fileNameExtension);

        //    Response.ClearHeaders();
        //    Response.ClearContent();
        //    Response.Buffer = true;
        //    Response.Clear();          
        //    Response.Flush();
        //    Response.Close();
        //    Response.End();

        //    return File(renderBytes, fileNameExtension);
        //}
        // GET: Students/Create
        public ActionResult Create()
        {
            return View();
        }
                
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Name,Department")] Student student)
        {
            if (ModelState.IsValid)
            {
                db.Students.Add(student);
                db.SaveChanges();

                //int milliseconds = 5000;
                //Thread.Sleep(milliseconds);
                //string ReportType = "Word";
                //ExportTo(student);                
                return RedirectToAction("Create");
            }

            return View(student);
        }

       
    }
}
