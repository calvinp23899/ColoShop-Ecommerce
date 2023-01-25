using Microsoft.Reporting.WebForms;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ColoShopEcommerce.WebApp.Reports
{
    public partial class ReportViewer : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadReport();
            }
        }
        private void LoadReport()
        {
            var reportParam = (dynamic)HttpContext.Current.Session["ReportParam"];
            if (reportParam != null && !string.IsNullOrEmpty(reportParam.RptFileName))
            {
                Page.Title = "Report |" + reportParam.ReportTitle;
                var dt = new DataTable();
                dt = reportParam.DataSource;
                if (dt.Rows.Count > 0)
                {
                    GenerateReportDocument(reportParam, reportParam.ReportType, dt);
                }
                else
                {
                    ShowErrorMessage();
                }
            }
        }

        public void GenerateReportDocument(dynamic reportParam, string reportType, DataTable data)
        {
            ReportParameter[] rptParameters = new ReportParameter[0];
            if (reportParam.IsHasParams)
            {
                rptParameters = ReportParameters(reportParam, reportType, rptParameters);
            }
            string dsName = reportParam.DataSetName;
            ReportViewer1.LocalReport.DataSources.Clear();
            ReportViewer1.LocalReport.DataSources.Add(new Microsoft.Reporting.WebForms.ReportDataSource(dsName, data));
            ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/" + "Reports//Rdlc//" + reportParam.RptFileName);

            if (reportParam.IsHasParams)
            {
                ReportViewer1.LocalReport.SetParameters(rptParameters);
            }
            ReportViewer1.DataBind();
            ReportViewer1.LocalReport.Refresh();
        }
        private static ReportParameter[] ReportParameters(dynamic reportParam, string reportType, ReportParameter[] rptParameters)
        {
            if(reportType == "OrderReport")
            {
                rptParameters = new ReportParameter[3]; // Number of params u want to pass in report
                rptParameters[0] = new ReportParameter("Code", reportParam.Code);
                rptParameters[1] = new ReportParameter("CreatedDate", reportParam.CreatedDate);
                rptParameters[2] = new ReportParameter("TotalAmount", reportParam.TotalAmount);
            }
            return rptParameters;
        }
        public void ShowErrorMessage()
        {
            ReportViewer1.LocalReport.DataSources.Clear();
            ReportViewer1.LocalReport.DataSources.Add(new Microsoft.Reporting.WebForms.ReportDataSource("", new DataTable()));
            ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/" + "Reports//Rdlc//blank.rdlc");
            ReportViewer1.DataBind();
            ReportViewer1.LocalReport.Refresh();
        }
    }
}