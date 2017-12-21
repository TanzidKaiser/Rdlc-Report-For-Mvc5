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
        protected void Page_Load(object sender, EventArgs e)
        {
            DatabaseContext db = new DatabaseContext();
            if(!IsPostBack)
            {
                List<Student> students = null;
                students = db.Students.ToList();
                ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Report/ReportViewer.rdlc");
                ReportViewer1.LocalReport.DataSources.Clear();
                ReportDataSource rdc = new ReportDataSource("DataSet",students);
                ReportViewer1.LocalReport.DataSources.Add(rdc);
                ReportViewer1.LocalReport.Refresh();

            }
        }
    }
}