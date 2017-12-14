using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace RdlcReportForMvc5.Models
{
    public class DatabaseContext: DbContext
    {
        public DbSet<Student> Students { get; set; }
    }
}