using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.Reporting.WebForms;
using RdlcReportForMvc5.Models;
namespace RdlcReportForMvc5.Views.Students
{
    public partial class WebForm : System.Web.UI.Page
    {
        DatabaseContext db = new DatabaseContext();

        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                List<Student> students = null;
                students = db.Students.ToList();
                ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Report/ReportViewer.rdlc");
                ReportViewer1.LocalReport.DataSources.Clear();
                ReportDataSource rdc = new ReportDataSource("DataSet", students);
                ReportViewer1.LocalReport.DataSources.Add(rdc);
                ReportParameter[] rptParam = new ReportParameter[]
           {
                new ReportParameter("Department","Total Students"),
           };
                ReportViewer1.LocalReport.SetParameters(rptParam);
                ReportViewer1.LocalReport.Refresh();

            }
        }

        protected void SrcBtn_Click(object sender, EventArgs e)
        {
            ReportViewer1.Reset();
            List<Student> students = null;
            var department = DeptTextBox.Text;
            students = db.Students.Where(p => p.Department == department).ToList();
            ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Report/ReportViewer.rdlc");
            ReportViewer1.LocalReport.DataSources.Clear();
            ReportDataSource rdc = new ReportDataSource("DataSet", students);
            ReportViewer1.LocalReport.DataSources.Add(rdc);
            ReportParameter[] rptParam = new ReportParameter[]
            {
                new ReportParameter("Department",DeptTextBox.Text),
            };
            ReportViewer1.LocalReport.SetParameters(rptParam);
            ReportViewer1.LocalReport.Refresh();
        }
       
    
    }
}