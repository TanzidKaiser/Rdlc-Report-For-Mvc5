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
using System.Web.Script.Serialization;

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
        public JsonResult Save(List<Student> id)
        {
            //db.Students.Add(student);
            //db.SaveChanges();
            //ExportTo(student);            
            return Json("Save", JsonRequestBehavior.AllowGet);
        }
        //Student student
        //List<Student> student
        public void ExportTo(string id)
        {
            List<Student> a = new List<Student>();

            byte[] b = Convert.FromBase64String(id);           
            System.Text.UTF8Encoding encoder = new System.Text.UTF8Encoding();
            System.Text.Decoder utf8Decode = encoder.GetDecoder();

            int charCount = utf8Decode.GetCharCount(b, 0, b.Length);

            char[] decodedChar = new char[charCount];
            utf8Decode.GetChars(b, 0, b.Length, decodedChar, 0);
            string result = new String(decodedChar);

            JavaScriptSerializer js = new JavaScriptSerializer();
            Student[] student = js.Deserialize<Student[]>(result);
            
            
            ReportDataSource reportDataSource = new ReportDataSource();
            reportDataSource.Name = "DataSet";
            reportDataSource.Value = student;

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
            //Response.Flush();
            Response.BinaryWrite(bytes);
            Response.Flush();
           
            //Response.Close();
            //return File(bytes, fileNameExtension);
        }
       
        
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
                //ExportTo(student);                
                return RedirectToAction("Create");
            }

            return View(student);
        }

       
    }
}
