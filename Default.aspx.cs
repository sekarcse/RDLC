using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.Reporting.WebForms;

public partial class _Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void lnk_Click(object sender, EventArgs e)
    {
        //Get DataSet
        DataSet ds = new DataSet();
        using (SqlConnection conn = new SqlConnection("Data Source=wasvdasin56;Initial Catalog=PROD_RMG;Persist Security Info=True;User ID=intrac;Password=intraware"))
        {
            SqlCommand sqlComm = new SqlCommand("[Test1]", conn);
            sqlComm.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter da = new SqlDataAdapter();
            da.SelectCommand = sqlComm;
            da.Fill(ds);
        }

        // Create Report DataSource 
        ReportDataSource reportDataSource1 = new ReportDataSource("DataSet1", ds.Tables[0]);
        ReportDataSource reportDataSource2 = new ReportDataSource("DataSet2", ds.Tables[1]);
        ReportDataSource reportDataSource3 = new ReportDataSource("DataSet3", ds.Tables[2]);

        LocalReport localReport = new LocalReport();
        localReport.ReportPath = Server.MapPath("Report.rdlc");

        localReport.DataSources.Add(reportDataSource1);
        localReport.DataSources.Add(reportDataSource2);
        localReport.DataSources.Add(reportDataSource3);

        string reportType = "Excel";
        string mimeType, encoding, fileNameExtension;
        Response.Write(localReport);
        Warning[] warnings;
        string[] streams;
        byte[] renderedBytes;
        renderedBytes = localReport.Render(reportType, null, out mimeType, out encoding, out fileNameExtension, out streams, out warnings);

        Response.Clear();
        Response.ContentType = mimeType;
        Response.AddHeader("content-disposition", "attachment; filename=" + "File" + "." + fileNameExtension);
        Response.BinaryWrite(renderedBytes);
        Response.End();
    }
}
