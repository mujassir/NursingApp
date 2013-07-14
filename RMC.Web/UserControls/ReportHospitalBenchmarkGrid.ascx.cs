using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using LogExceptions;

namespace RMC.Web.UserControls
{
    public partial class ReportHospitalBenchmarkGrid : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                LabelFilter.Text = " Filter:- " + Request.QueryString["filter"] + ",";
                LabelBegigningDate.Text = " Period:- " + Request.QueryString["beginingDate"];
                LabelEndingDate.Text = " - " + Request.QueryString["endingDate"];
            }
            catch (Exception ex)
            {
                ex.Data.Add("Page", "CreateNewProfile.ascx");
                LogManager._stringObject = "CreateNewProfile.ascx ---- Page_Load";
                LogManager.SetExceptionDetails(ex, LogManager._stringObject, null);
                LogManager.LogException(ex, LogManager.LoggingCategory.General, LogManager.LoggingLevel.Error);
                CommonClass.Show(LogManager.ShowErrorDetail(ex));
            }
        }

        protected void ObjectDataSource1_Selecting(object sender, ObjectDataSourceSelectingEventArgs e)
        {
            try
            {
                string activitiesID = Request.QueryString["activitiesID"];
                if (activitiesID == string.Empty)
                {
                    activitiesID = null;
                }
                e.InputParameters["activitiesID"] = activitiesID;

                string valueAddedCategoryID = Request.QueryString["valueAddedCategoryID"];
                if (valueAddedCategoryID == string.Empty)
                {
                    valueAddedCategoryID = null;
                }
                e.InputParameters["valueAddedCategoryID"] = valueAddedCategoryID;

                string OthersCategoryID = Request.QueryString["OthersCategoryID"];
                if (OthersCategoryID == string.Empty)
                {
                    OthersCategoryID = null;
                }
                e.InputParameters["OthersCategoryID"] = OthersCategoryID;

                string LocationCategoryID = Request.QueryString["LocationCategoryID"];
                if (LocationCategoryID == string.Empty)
                {
                    LocationCategoryID = null;
                }
                e.InputParameters["LocationCategoryID"] = LocationCategoryID;

                e.InputParameters["hospitalUnitID"] = null;

                string firstYear = Request.QueryString["yearFrom"];
                if (firstYear == string.Empty)
                {
                    firstYear = null;
                }
                e.InputParameters["firstYear"] = firstYear;

                string lastYear = Request.QueryString["yearTo"];
                if (lastYear == string.Empty)
                {
                    lastYear = null;
                }
                e.InputParameters["lastYear"] = lastYear;

                string firstMonth = Request.QueryString["monthFrom"];
                if (firstMonth == string.Empty)
                {
                    firstMonth = null;
                }
                e.InputParameters["firstMonth"] = firstMonth;

                string lastMonth = Request.QueryString["monthTo"];
                if (lastMonth == string.Empty)
                {
                    lastMonth = null;
                }
                e.InputParameters["lastMonth"] = lastMonth;

                string bedInUnitFrom = Request.QueryString["BedsInUnitFrom"];
                if (bedInUnitFrom == string.Empty)
                {
                    bedInUnitFrom = null;
                }
                e.InputParameters["bedInUnitFrom"] = bedInUnitFrom;
                e.InputParameters["optBedInUnitFrom"] = Convert.ToInt32(Request.QueryString["optBedsInUnitFrom"]);
                string bedInUnitTo = Request.QueryString["bedsInUnitTo"];
                if (bedInUnitTo == string.Empty)
                {
                    bedInUnitTo = null;
                }
                e.InputParameters["bedInUnitTo"] = bedInUnitTo;
                e.InputParameters["optBedInUnitTo"] = Convert.ToInt32(Request.QueryString["optBedsInUnitTo"]);

                string budgetedPatientFrom = Request.QueryString["budgetedPatientFrom"];
                if (budgetedPatientFrom == string.Empty)
                {
                    budgetedPatientFrom = null;
                }
                e.InputParameters["budgetedPatientFrom"] = budgetedPatientFrom;
                e.InputParameters["optBudgetedPatientFrom"] = Convert.ToInt32(Request.QueryString["optBudgetedPatientFrom"]);
                string budgetedPatientTo = Request.QueryString["budgetedPatientTo"];
                if (budgetedPatientTo == string.Empty)
                {
                    budgetedPatientTo = null;
                }
                e.InputParameters["budgetedPatientTo"] = budgetedPatientTo;
                e.InputParameters["optBudgetedPatientTo"] = Convert.ToInt32(Request.QueryString["optBudgetedPatientTo"]);

                e.InputParameters["startDate"] = null;
                e.InputParameters["endDate"] = null;

                string electronicDocumentFrom = Request.QueryString["electronicDocumentationFrom"];
                if (electronicDocumentFrom == string.Empty)
                {
                    electronicDocumentFrom = null;
                }
                e.InputParameters["electronicDocumentFrom"] = electronicDocumentFrom;
                e.InputParameters["optElectronicDocumentFrom"] = Convert.ToInt32(Request.QueryString["optElectronicDocumentationFrom"]);
                string electronicDocumentTo = Request.QueryString["electronicDocumentationTo"];
                if (electronicDocumentTo == string.Empty)
                {
                    electronicDocumentTo = null;
                }
                e.InputParameters["electronicDocumentTo"] = electronicDocumentTo;
                e.InputParameters["optElectronicDocumentTo"] = Convert.ToInt32(Request.QueryString["optElectronicDocumentationTo"]);

                string docByException = Request.QueryString["docByException"];
                if (docByException == string.Empty)
                {
                    docByException = null;
                }
                e.InputParameters["docByException"] = docByException;

                string unitType = Request.QueryString["unitType"];
                if (unitType == string.Empty)
                {
                    unitType = null;
                }
                e.InputParameters["unitType"] = unitType;
                string pharmacyType = Request.QueryString["pharmacyType"];
                if (pharmacyType == string.Empty)
                {
                    pharmacyType = null;
                }
                e.InputParameters["pharmacyType"] = pharmacyType;
                string hospitalType = Request.QueryString["hospitalType"];
                if (hospitalType == string.Empty)
                {
                    hospitalType = null;
                }
                e.InputParameters["hospitalType"] = hospitalType;


                e.InputParameters["optHospitalSizeFrom"] = Convert.ToInt32(Request.QueryString["optHospitalSizeFrom"]);
                string hospitalSizeFrom = Request.QueryString["hospitalSizeFrom"];
                if (hospitalSizeFrom == string.Empty)
                {
                    hospitalSizeFrom = null;
                }
                e.InputParameters["hospitalSizeFrom"] = hospitalSizeFrom;
                e.InputParameters["optHospitalSizeTo"] = Convert.ToInt32(Request.QueryString["optHospitalSizeTo"]);
                string hospitalSizeTo = Request.QueryString["hospitalSizeTo"];
                if (hospitalSizeTo == string.Empty)
                {
                    hospitalSizeTo = null;
                }
                e.InputParameters["hospitalSizeTo"] = hospitalSizeTo;
                    
                string countryId = Request.QueryString["countryId"];
                if (countryId == "0")
                {
                    countryId = null;
                }
                e.InputParameters["countryId"] = countryId;
                string stateId = Request.QueryString["stateId"];
                if (stateId == "0")
                {
                    stateId = null;
                }
                e.InputParameters["stateId"] = stateId;
                //e.InputParameters["countryId"] = 0;
                //e.InputParameters["stateId"] = 0;
                e.InputParameters["activities"] = Request.QueryString["activities"];
                e.InputParameters["value"] = Request.QueryString["value"];
                e.InputParameters["others"] = Request.QueryString["others"];
                e.InputParameters["location"] = Request.QueryString["location"];

                string dataPointsFrom = Request.QueryString["dataPointsFrom"];
                if (dataPointsFrom == string.Empty)
                {
                    dataPointsFrom = null;
                }
                e.InputParameters["dataPointsFrom"] = dataPointsFrom;

                e.InputParameters["optDataPointsFrom"] = Convert.ToInt32(Request.QueryString["optDataPointsFrom"]);

                string dataPointsTo = Request.QueryString["dataPointsTo"];
                if (dataPointsTo == string.Empty)
                {
                    dataPointsTo = null;
                }
                e.InputParameters["dataPointsTo"] = dataPointsTo;
                e.InputParameters["optdataPointsTo"] = Convert.ToInt32(Request.QueryString["optdataPointsTo"]);

                string configName = Request.QueryString["configName"];
                if (configName == string.Empty)
                {
                    configName = null;
                }
                e.InputParameters["configName"] = configName;

            }
            catch (Exception ex)
            {
                ex.Data.Add("Page", "CreateNewProfile.ascx");
                LogManager._stringObject = "CreateNewProfile.ascx ---- Page_Load";
                LogManager.SetExceptionDetails(ex, LogManager._stringObject, null);
                LogManager.LogException(ex, LogManager.LoggingCategory.General, LogManager.LoggingLevel.Error);
                CommonClass.Show(LogManager.ShowErrorDetail(ex));
            }
        }

        protected void ImageButtonBack_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                string filter = Request.QueryString["filter"];
                string beginingDate = Request.QueryString["beginingDate"];
                string endingDate = Request.QueryString["endingDate"];
                string value = Request.QueryString["value"];
                string others = Request.QueryString["others"];
                string location = Request.QueryString["location"];
                string activities = Request.QueryString["activities"];
                //ImageButtonBack.PostBackUrl = "~/Administrator/HospitalBenchmark.aspx?" + "&filter=" + filter + "&beginingDate=" + beginingDate + "&endingDate=" + endingDate + "&value=" + value + "&others=" + others + "&location=" + location;
                if (HttpContext.Current.User.IsInRole("superadmin"))
                {
                    Response.Redirect("~/Administrator/HospitalBenchmark.aspx?" + "&filter=" + filter + "&beginingDate=" + beginingDate + "&endingDate=" + endingDate + "&value=" + value + "&others=" + others + "&location=" + location + "&activities=" + activities, false);
                }
                else
                {
                    Response.Redirect("~/Users/HospitalBenchmark.aspx?" + "&filter=" + filter + "&beginingDate=" + beginingDate + "&endingDate=" + endingDate + "&value=" + value + "&others=" + others + "&location=" + location + "&activities=" + activities, false);

                }
            }
            catch (Exception ex)
            {
                LogManager._stringObject = "ReportTimeRN.ascx ----";
                LogManager.SetExceptionDetails(ex, LogManager._stringObject, null);
                LogManager.LogException(ex, LogManager.LoggingCategory.General, LogManager.LoggingLevel.Error);
                CommonClass.Show(LogManager.ShowErrorDetail(ex));
            }
        }

        protected void LinkButtonExportReport_Click(object sender, EventArgs e)
        {
            try
            {
                string filterName = Request.QueryString["filter"].Replace(" ", "");
                string fileName = "Attachment; FileName = HospitalBenchmarkingSummary(";
                Response.Clear();
                if (filterName.Contains(">"))
                {
                    filterName = filterName.Replace(">", "gt");
                }
                if (filterName.Contains("<"))
                {
                    filterName = filterName.Replace("<", "lt");
                }
                fileName = fileName + filterName + "," + Request.QueryString["beginingDate"].Replace(" ", "") + "-" + Request.QueryString["endingDate"].Replace(" ", "") + ")" + ".xls";

                //fileName = fileName + ".xls";
                Response.AddHeader("Content-Disposition", fileName);
                Response.Charset = "";
                Response.Cache.SetCacheability(HttpCacheability.NoCache);
                //Response.ContentType = "Application/vnd.xls";
                Response.ContentType = "Application/ms-excel";
                System.IO.StringWriter stringWrite = new System.IO.StringWriter();
                System.Web.UI.HtmlTextWriter htmlWrite = new HtmlTextWriter(stringWrite);

                //if (!LabelFilter.Text.Contains("No Filter"))
                //{
                //    LabelFilter.RenderControl(htmlWrite);
                //}
                //LabelBegigningDate.RenderControl(htmlWrite);
                //LabelEndingDate.RenderControl(htmlWrite);


                GridViewFunctionReport.RenderControl(htmlWrite);
                GridViewReport.RenderControl(htmlWrite);
                Response.Write(stringWrite.ToString());
                Response.End();
            }
            catch (Exception ex)
            {
                LogManager._stringObject = "ReportTimeRN.ascx ----";
                LogManager.SetExceptionDetails(ex, LogManager._stringObject, null);
                LogManager.LogException(ex, LogManager.LoggingCategory.General, LogManager.LoggingLevel.Error);
                CommonClass.Show(LogManager.ShowErrorDetail(ex));
            }
        }

        protected void LinkButtonExportFunctionReport_Click(object sender, EventArgs e)
        {
            //Response.Clear();
            //Response.AddHeader("Content-Disposition", "Attachment; FileName = FunctionReport.xls");
            //Response.Charset = "";
            //Response.Cache.SetCacheability(HttpCacheability.NoCache);
            //Response.ContentType = "Application/vnd.xls";
            //System.IO.StringWriter stringWrite = new System.IO.StringWriter();
            //System.Web.UI.HtmlTextWriter htmlWrite = new HtmlTextWriter(stringWrite);
            //GridViewFunctionReport.RenderControl(htmlWrite);
            //Response.Write(stringWrite.ToString());
            //Response.End();
        }

        protected void GridViewReport_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                //if (e.Row.RowType == DataControlRowType.DataRow)
                //{
                //    e.Row.Cells[0].BackColor = System.Drawing.ColorTranslator.FromHtml("#06569d");
                //    e.Row.Cells[0].ForeColor = System.Drawing.Color.White;
                //    e.Row.Cells[0].Font.Bold = true;
                    
                //    //e.Row.Cells[0].Style.Add("padding", "2");
                //    //e.Row.Cells[1].Style.Add("padding", "2");
                //}
                //if (e.Row.RowType == DataControlRowType.Header)
                //{
                //    //e.Row.Cells[0].Visible = false;
                //}

                //for popup
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    e.Row.Cells[1].Visible = false;
                    e.Row.Cells[0].BackColor = System.Drawing.ColorTranslator.FromHtml("#06569d");
                    e.Row.Cells[2].BackColor = System.Drawing.ColorTranslator.FromHtml("#06569d");
                    e.Row.Cells[2].ForeColor = System.Drawing.Color.White;
                    e.Row.Cells[2].Font.Bold = true;

                    //HyperLink popupLink = new HyperLink();
                    //popupLink.NavigateUrl = "#";
                    //popupLink.Attributes.Add("onclick", String.Format("Javascript:window.open('dv.aspx?applicationid={0}', 'windowName', 'options');", e.Row.Cells[0].Text));
                    //e.Row.Cells[0].Controls.Add(popupLink);

                    LinkButton emailLink = new LinkButton();
                    emailLink = (LinkButton)(e.Row.Cells[1].FindControl("LinkButton1"));
                    emailLink.OnClientClick = "javascript:popup_window=window.open('SendMessage.aspx?id=" + e.Row.Cells[1].Text + "', 'popup_window', 'width=500,height=420,top=100,left=400,scrollbars=1');popup_window.focus()";
                    //emailLink.OnClientClick = "javascript:popup_window=window.open('SendMessage.aspx?id=" + "bharatg@smartdatainc.net" + "', 'popup_window', 'width=500,height=420,top=100,left=400,scrollbars=1');popup_window.focus()";
                    

                }
                if (e.Row.RowType == DataControlRowType.Header)
                {
                    e.Row.Cells[1].Visible = false;
                    //e.Row.Cells[0].Visible = false;
                }
            }
            catch (Exception ex)
            {
                LogManager._stringObject = "ReportHospitalBenchmarkGrid.ascx ---- Page_Load";
                LogManager.SetExceptionDetails(ex, LogManager._stringObject, null);
                LogManager.LogException(ex, LogManager.LoggingCategory.General, LogManager.LoggingLevel.Error);
                CommonClass.Show(LogManager.ShowErrorDetail(ex));
            }
        }

        protected void GridViewFunctionReport_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    e.Row.Cells[0].BackColor = System.Drawing.ColorTranslator.FromHtml("#06569d");
                    e.Row.Cells[1].BackColor = System.Drawing.ColorTranslator.FromHtml("#06569d");
                    e.Row.Cells[1].ForeColor = System.Drawing.Color.White;
                    e.Row.Cells[1].Font.Bold = true;
                    //e.Row.Cells[0].ColumnSpan = 4;
                    //e.Row.Cells[0].Width = 140;
                }
                if (e.Row.RowType == DataControlRowType.Header)
                {
                    //e.Row.Cells[1].Text = "";
                    //e.Row.Cells[0].ColumnSpan = 4;
                    //e.Row.Cells[0].Width = 140;
                }
            }
            catch (Exception ex)
            {
                LogManager._stringObject = "ReportHospitalBenchmarkGrid.ascx ---- Page_Load";
                LogManager.SetExceptionDetails(ex, LogManager._stringObject, null);
                LogManager.LogException(ex, LogManager.LoggingCategory.General, LogManager.LoggingLevel.Error);
                CommonClass.Show(LogManager.ShowErrorDetail(ex));
            }
        }

       

    }
}