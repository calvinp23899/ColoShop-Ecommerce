using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace ColoShopEcommerce.WebApp.Models.ReportModel
{
    public class ReportParams
    {
        public string RptFileName { get; set; }
        public string ReportTitle { get; set; }
        public string ReportType { get; set; }
        public DataTable DataSource { get; set; }
        public bool IsHasParams { get; set; }
        public string DataSetName { get; set; }
        public string Code { get; set; }
        public string TotalAmount { get; set; }
        public string CreatedDate { get; set; }
    }
}