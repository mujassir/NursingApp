using System;
using System.Collections;
using System.Collections.Generic;
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
using System.Web.UI.DataVisualization.Charting;
using LogExceptions;

namespace RMC.Web.UserControls
{
    public partial class ReportTimeRNCharts : System.Web.UI.UserControl
    {
        #region Events

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (Request.QueryString["Report"] == "Dashboard")
                {
                    LabelHeading.Text = "Monthly Summary Dashboard";
                }
                else if (Request.QueryString["Report"] == "PieCharts")
                {
                    LabelHeading.Text = "Monthly Data - Pie Charts";
                }
                else if (Request.QueryString["Report"] == "ControlCharts")
                {
                    LabelHeading.Text = "Control Charts";
                }

                if (Request.QueryString["Report"] == "ControlCharts")
                {
                    MultiViewCharts.ActiveViewIndex = 1;
                }

                if (Page.IsPostBack != true)
                {
                    DropDownListMarkerStyle.DataBind();
                    DropDownListMarkerStyle.SelectedIndex = 3;
                    DropDownListMarkerColor.DataBind();
                    DropDownListMarkerColor.SelectedIndex = 36;
                    DropDownListColor.DataBind();
                    DropDownListColor.SelectedIndex = 36;
                    DropDownListMarkerSize.SelectedValue = "6";
                    DropDownListLineWidth.SelectedValue = "2";
                    GetLineChartSeries();
                    //GetAllSelectedProfiles();               //Gets the selected profiles from the previous page
                    //DropDownListMarkerSize.SelectedValue = "6";
                    //DropDownListLineWidth.SelectedValue = "2";
                    //DropDownListProfileCategory.SelectedIndex = 1;
                    //DropDownListProfileSubCategory.DataBind();
                    //DropDownListProfileSubCategory.Enabled = true;
                    //if (DropDownListProfileSubCategory.Items.Count > 1)
                    //{
                    //    DropDownListProfileSubCategory.SelectedIndex = 1;
                    //}
                    //DropDownListMarkerStyle.DataBind();
                    //DropDownListMarkerStyle.SelectedIndex = 3;
                    //DropDownListMarkerColor.DataBind();
                    //DropDownListMarkerColor.SelectedIndex = 36;
                    //DropDownListColor.DataBind();
                    //DropDownListColor.SelectedIndex = 36;

                    //List<RMC.BusinessEntities.BEReports> objectGenericBEReports = GetDataForLineChart();
                    //RMC.BussinessService.BSChartControl objectBSChartControl = new RMC.BussinessService.BSChartControl();
                    //List<Series> objectGenericSeries = objectBSChartControl.GetLineChartSeries(null, Convert.ToInt32(DropDownListMarkerSize.SelectedValue), null, null, Convert.ToInt32(DropDownListLineWidth.SelectedValue), null, null, null, objectGenericBEReports);
                    //foreach (Series seriesChart in objectGenericSeries)
                    //{
                    //    ChartControlCharts.Series.Add(seriesChart);
                    //}
                    //if (objectGenericSeries.Count == 0)
                    //{
                    //    ChartControlCharts.Titles["Title1"].Text = "No Data for Graph to Display";
                    //}
                    //else
                    //{
                    //    ChartControlCharts.Titles["Title1"].Text = DropDownListProfileSubCategory.SelectedItem.Text + " (" + DropDownListProfileCategory.SelectedItem.Text + ")";
                    //}
                }

                
            }
            catch (Exception ex)
            {
                LogManager._stringObject = "ReportTimeRNCharts.ascx ----";
                LogManager.SetExceptionDetails(ex, LogManager._stringObject, null);
                LogManager.LogException(ex, LogManager.LoggingCategory.General, LogManager.LoggingLevel.Error);
                CommonClass.Show(LogManager.ShowErrorDetail(ex));
            }

        }

        protected void DropDownListMarkerStyle_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                GetLineChartSeries();
            }
            catch (Exception ex)
            {
                LogManager._stringObject = "ReportTimeRNCharts.ascx ----";
                LogManager.SetExceptionDetails(ex, LogManager._stringObject, null);
                LogManager.LogException(ex, LogManager.LoggingCategory.General, LogManager.LoggingLevel.Error);
                CommonClass.Show(LogManager.ShowErrorDetail(ex));
            }
        }

        protected void DropDownListMarkerSize_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                GetLineChartSeries();
            }
            catch (Exception ex)
            {
                LogManager._stringObject = "ReportTimeRNCharts.ascx ----";
                LogManager.SetExceptionDetails(ex, LogManager._stringObject, null);
                LogManager.LogException(ex, LogManager.LoggingCategory.General, LogManager.LoggingLevel.Error);
                CommonClass.Show(LogManager.ShowErrorDetail(ex));
            }
        }

        protected void DropDownListMarkerColor_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                GetLineChartSeries();
            }
            catch (Exception ex)
            {
                LogManager._stringObject = "ReportTimeRNCharts.ascx ----";
                LogManager.SetExceptionDetails(ex, LogManager._stringObject, null);
                LogManager.LogException(ex, LogManager.LoggingCategory.General, LogManager.LoggingLevel.Error);
                CommonClass.Show(LogManager.ShowErrorDetail(ex));
            }
        }

        protected void DropDownListPointLabel_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                GetLineChartSeries();
            }
            catch (Exception ex)
            {
                LogManager._stringObject = "ReportTimeRNCharts.ascx ----";
                LogManager.SetExceptionDetails(ex, LogManager._stringObject, null);
                LogManager.LogException(ex, LogManager.LoggingCategory.General, LogManager.LoggingLevel.Error);
                CommonClass.Show(LogManager.ShowErrorDetail(ex));
            }
        }

        protected void DropDownListChartType_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                GetLineChartSeries();
            }
            catch (Exception ex)
            {
                LogManager._stringObject = "ReportTimeRNCharts.ascx ----";
                LogManager.SetExceptionDetails(ex, LogManager._stringObject, null);
                LogManager.LogException(ex, LogManager.LoggingCategory.General, LogManager.LoggingLevel.Error);
                CommonClass.Show(LogManager.ShowErrorDetail(ex));
            }
        }

        protected void DropDownListLineWidth_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                GetLineChartSeries();
            }
            catch (Exception ex)
            {
                LogManager._stringObject = "ReportTimeRNCharts.ascx ----";
                LogManager.SetExceptionDetails(ex, LogManager._stringObject, null);
                LogManager.LogException(ex, LogManager.LoggingCategory.General, LogManager.LoggingLevel.Error);
                CommonClass.Show(LogManager.ShowErrorDetail(ex));
            }
        }

        protected void DropDownListColor_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                GetLineChartSeries();
            }
            catch (Exception ex)
            {
                LogManager._stringObject = "ReportTimeRNCharts.ascx ----";
                LogManager.SetExceptionDetails(ex, LogManager._stringObject, null);
                LogManager.LogException(ex, LogManager.LoggingCategory.General, LogManager.LoggingLevel.Error);
                CommonClass.Show(LogManager.ShowErrorDetail(ex));
            }
        }

        protected void DropDownListShadowOffset_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                GetLineChartSeries();
            }
            catch (Exception ex)
            {
                LogManager._stringObject = "ReportTimeRNCharts.ascx ----";
                LogManager.SetExceptionDetails(ex, LogManager._stringObject, null);
                LogManager.LogException(ex, LogManager.LoggingCategory.General, LogManager.LoggingLevel.Error);
                CommonClass.Show(LogManager.ShowErrorDetail(ex));
            }
        }

        protected void DropDownListProfileSubCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            //try
            //{
            //    GetLineChartSeries();
            //}
            //catch (Exception ex)
            //{
            //    LogManager._stringObject = "ReportTimeRNCharts.ascx ----";
            //    LogManager.SetExceptionDetails(ex, LogManager._stringObject, null);
            //    LogManager.LogException(ex, LogManager.LoggingCategory.General, LogManager.LoggingLevel.Error);
            //    CommonClass.Show(LogManager.ShowErrorDetail(ex));
            //}
        }

        protected void DropDownListProfileCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {

                //DropDownListProfileSubCategory.Items.Clear();
                //DropDownListProfileSubCategory.Items.Add("Select Sub Profile");
                //DropDownListProfileSubCategory.Items[0].Value = 0.ToString();
                //DropDownListProfileSubCategory.DataBind();
                //if (DropDownListProfileSubCategory.Items.Count > 1)
                //{
                //    DropDownListProfileSubCategory.SelectedIndex = 1;
                //    GetLineChartSeries();
                //}
                //DropDownListProfileSubCategory.Focus();

            }
            catch (Exception ex)
            {
                LogManager._stringObject = "ReportTimeRNCharts.ascx ----";
                LogManager.SetExceptionDetails(ex, LogManager._stringObject, null);
                LogManager.LogException(ex, LogManager.LoggingCategory.General, LogManager.LoggingLevel.Error);
                CommonClass.Show(LogManager.ShowErrorDetail(ex));
            }
        }

        protected void LinkButtonShowChart_Click(object sender, EventArgs e)
        {
            try
            {
                GetLineChartSeries();
            }
            catch (Exception ex)
            {
                LogManager._stringObject = "ReportTimeRNCharts.ascx ----";
                LogManager.SetExceptionDetails(ex, LogManager._stringObject, null);
                LogManager.LogException(ex, LogManager.LoggingCategory.General, LogManager.LoggingLevel.Error);
                CommonClass.Show(LogManager.ShowErrorDetail(ex));
            }
        }

        protected void ObjectDataSourceProfileSubCategory_Selecting(object sender, ObjectDataSourceSelectingEventArgs e)
        {
            //e.InputParameters["valueAddedCategoryID"] = Request.QueryString["valueAddedCategoryID"];
            //e.InputParameters["OthersCategoryID"] = Request.QueryString["OthersCategoryID"];
            //e.InputParameters["LocationCategoryID"] = Request.QueryString["LocationCategoryID"];
            string valueAddedCategoryID = Request.QueryString["valueAddedCategoryID"];
            if (valueAddedCategoryID == "")
            {
                valueAddedCategoryID = null;
            }
            e.InputParameters["valueAddedCategoryID"] = valueAddedCategoryID;

            string OthersCategoryID = Request.QueryString["OthersCategoryID"];
            if (OthersCategoryID == "")
            {
                OthersCategoryID = null;
            }
            e.InputParameters["OthersCategoryID"] = OthersCategoryID;

            string LocationCategoryID = Request.QueryString["LocationCategoryID"];
            if (LocationCategoryID == "")
            {
                LocationCategoryID = null;
            }
            e.InputParameters["LocationCategoryID"] = LocationCategoryID;

            e.InputParameters["value"] = Request.QueryString["value"];
            e.InputParameters["others"] = Request.QueryString["others"];
            e.InputParameters["location"] = Request.QueryString["location"];
        }

        protected void ImageButtonBack_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                string filter = Request.QueryString["filter"];
                string beginingDate = Request.QueryString["beginingDate"];
                string endingDate = Request.QueryString["endingDate"];
                //string value = Request.QueryString["value"];
                //string others = Request.QueryString["others"];
                //string location = Request.QueryString["location"];
                string hospitalName = Request.QueryString["hospitalName"];
                string hospitalUnitName = Request.QueryString["hospitalUnitName"];
                string profileCategory = Request.QueryString["profileCategory"];
                string profileSubCategory = Request.QueryString["profileSubCategory"];
                //ImageButtonBack.PostBackUrl = "~/Administrator/HospitalBenchmark.aspx?" + "&filter=" + filter + "&beginingDate=" + beginingDate + "&endingDate=" + endingDate + "&value=" + value + "&others=" + others + "&location=" + location;

                if (HttpContext.Current.User.IsInRole("superadmin"))
                {
                    Response.Redirect("~/Administrator/ControlCharts.aspx?Report=ControlCharts" + "&filter=" + filter + "&beginingDate=" + beginingDate + "&endingDate=" + endingDate + "&hospitalName=" + hospitalName + "&hospitalUnitName=" + hospitalUnitName + "&profileCategory=" + profileCategory + "&profileSubCategory=" + profileSubCategory,  false);
                }
                else
                {
                    Response.Redirect("~/Users/ControlCharts.aspx?Report=ControlCharts" + "&filter=" + filter + "&beginingDate=" + beginingDate + "&endingDate=" + endingDate + "&hospitalName=" + hospitalName + "&hospitalUnitName=" + hospitalUnitName + "&profileCategory=" + profileCategory + "&profileSubCategory=" + profileSubCategory, false);
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

        protected void LinkButtonSaveImage_Click(object sender, EventArgs e)
        {
            try
            {
                Response.Redirect("SaveImageAtClientSide.aspx", false);
            }
            catch (Exception ex)
            {
                LogManager._stringObject = "ReportTimeRN.ascx ----";
                LogManager.SetExceptionDetails(ex, LogManager._stringObject, null);
                LogManager.LogException(ex, LogManager.LoggingCategory.General, LogManager.LoggingLevel.Error);
                CommonClass.Show(LogManager.ShowErrorDetail(ex));
            }
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Method to Get a list of Data For Line Chart
        /// </summary>
        private List<RMC.BusinessEntities.BEReports> GetDataForLineChart()
        {
            try
            {
                //Modified
                RMC.BussinessService.BSReports objectBSReports = new RMC.BussinessService.BSReports();
                string ProfileCategoryValue;
                string ProfileSubCategoryValue;

                ProfileCategoryValue = Request.QueryString["profileCategory"];
                ProfileSubCategoryValue = Request.QueryString["profileSubCategory"];

                //string valueAddedCategoryID = Request.QueryString["valueAddedCategoryID"];
                //string OthersCategoryID = Request.QueryString["OthersCategoryID"];
                //string LocationCategoryID = Request.QueryString["LocationCategoryID"];

                string valueAddedCategoryID = Request.QueryString["valueAddedCategoryID"];
                if (valueAddedCategoryID == "")
                {
                    valueAddedCategoryID = null;
                }
                //e.InputParameters["valueAddedCategoryID"] = valueAddedCategoryID;

                string OthersCategoryID = Request.QueryString["OthersCategoryID"];
                if (OthersCategoryID == "")
                {
                    OthersCategoryID = null;
                }
                //e.InputParameters["OthersCategoryID"] = OthersCategoryID;

                string LocationCategoryID = Request.QueryString["LocationCategoryID"];
                if (LocationCategoryID == "")
                {
                    LocationCategoryID = null;
                }
                //e.InputParameters["LocationCategoryID"] = LocationCategoryID;

                int? hospitalUnitID = Convert.ToInt32(Request.QueryString["hospitalUnitID"]);
                if (hospitalUnitID == 0)
                {
                    hospitalUnitID = null;
                }

                string yearFrom = Request.QueryString["yearFrom"];
                if (yearFrom == "")
                { yearFrom = "0"; }
                int? firstYear = Convert.ToInt32(yearFrom);
                if (firstYear == 0)
                {
                    firstYear = null;
                }

                string yearTo = Request.QueryString["yearTo"];
                if (yearTo == "")
                { yearTo = "0"; }
                int? lastYear = Convert.ToInt32(yearTo);
                if (lastYear == 0)
                {
                    lastYear = null;
                }

                string monthFrom = Request.QueryString["monthFrom"];
                if (monthFrom == "")
                { monthFrom = "0"; }
                int? firstMonth = Convert.ToInt32(monthFrom);
                if (firstMonth == 0)
                {
                    firstMonth = null;
                }

                string monthTo = Request.QueryString["monthTo"];
                if (monthTo == "")
                { monthTo = "0"; }
                int? lastMonth = Convert.ToInt32(monthTo);
                if (lastMonth == 0)
                {
                    lastMonth = null;
                }

                string BedsInUnitFrom = Request.QueryString["BedsInUnitFrom"];
                if (BedsInUnitFrom == "")
                { BedsInUnitFrom = "0"; }
                int? bedInUnitFrom = Convert.ToInt32(BedsInUnitFrom);
                if (bedInUnitFrom == 0)
                {
                    bedInUnitFrom = null;
                }
                int optBedsInUnitFrom = Convert.ToInt32(Request.QueryString["optBedsInUnitFrom"]);
                string bedsInUnitTo = Request.QueryString["bedsInUnitTo"];
                if (bedsInUnitTo == "")
                { bedsInUnitTo = "0"; }
                int? bedInUnitTo = Convert.ToInt32(bedsInUnitTo);
                if (bedInUnitTo == 0)
                {
                    bedInUnitTo = null;
                }
                int optBedsInUnitTo = Convert.ToInt32(Request.QueryString["optBedsInUnitTo"]);

                string strbudgetedPatientFrom = Request.QueryString["budgetedPatientFrom"];
                if (strbudgetedPatientFrom == "")
                { strbudgetedPatientFrom = "0"; }
                float? budgetedPatientFrom = Convert.ToInt32(strbudgetedPatientFrom);
                if (budgetedPatientFrom == 0)
                {
                    budgetedPatientFrom = null;
                }
                int optBudgetedPatientFrom = Convert.ToInt32(Request.QueryString["optBudgetedPatientFrom"]);
                string strbudgetedPatientTo = Request.QueryString["budgetedPatientTo"];
                if (strbudgetedPatientTo == "")
                { strbudgetedPatientTo = "0"; }
                float? budgetedPatientTo = Convert.ToInt32(strbudgetedPatientTo);
                if (budgetedPatientTo == 0)
                {
                    budgetedPatientTo = null;
                }
                int optBudgetedPatientTo = Convert.ToInt32(Request.QueryString["optBudgetedPatientTo"]);

                string startDate = null;
                string endDate = null;

                string strelectronicDocumentationFrom = Request.QueryString["electronicDocumentationFrom"];
                if (strelectronicDocumentationFrom == "")
                { strelectronicDocumentationFrom = "0"; }
                int? electronicDocumentFrom = Convert.ToInt32(strelectronicDocumentationFrom);
                if (electronicDocumentFrom == 0)
                {
                    electronicDocumentFrom = null;
                }
                int optElectronicDocumentFrom = Convert.ToInt32(Request.QueryString["optElectronicDocumentationFrom"]);
                string strelectronicDocumentationTo = Request.QueryString["electronicDocumentationTo"];
                if (strelectronicDocumentationTo == "")
                { strelectronicDocumentationTo = "0"; }
                int? electronicDocumentTo = Convert.ToInt32(strelectronicDocumentationTo);
                if (electronicDocumentTo == 0)
                {
                    electronicDocumentTo = null;
                }
                int optElectronicDocumentTo = Convert.ToInt32(Request.QueryString["optElectronicDocumentationTo"]);

                int docByException = Convert.ToInt32(Request.QueryString["docByException"]);

                string unitType = Request.QueryString["unitType"];
                if (unitType == "")
                {
                    unitType = null;
                }

                string pharmacyType = Request.QueryString["pharmacyType"];
                if (pharmacyType == "")
                {
                    pharmacyType = null;
                }

                string hospitalType = Request.QueryString["hospitalType"];
                if (hospitalType == "")
                {
                    hospitalType = null;
                }

                int optHospitalSizeFrom = Convert.ToInt32(Request.QueryString["optHospitalSizeFrom"]);
                string strhospitalSizeFrom = Request.QueryString["hospitalSizeFrom"];
                if (strhospitalSizeFrom == "")
                { strhospitalSizeFrom = "0"; }
                int? hospitalSizeFrom = Convert.ToInt32(strhospitalSizeFrom);
                if (hospitalSizeFrom == 0)
                {
                    hospitalSizeFrom = null;
                }
                int optHospitalSizeTo = Convert.ToInt32(Request.QueryString["optHospitalSizeTo"]);
                string strhospitalSizeTo = Request.QueryString["hospitalSizeTo"];
                if (strhospitalSizeTo == "")
                { strhospitalSizeTo = "0"; }
                int? hospitalSizeTo = Convert.ToInt32(strhospitalSizeTo);
                if (hospitalSizeTo == 0)
                {
                    hospitalSizeTo = null;
                }

                int? countryId = Convert.ToInt32(Request.QueryString["countryId"]);
                if (countryId == 0)
                {
                    countryId = null;
                }
                int? stateId = Convert.ToInt32(Request.QueryString["stateId"]);
                if (stateId == 0)
                {
                    stateId = null;
                }
                string activities = Request.QueryString["activities"];
                string value = Request.QueryString["value"];
                string others = Request.QueryString["others"];
                string location = Request.QueryString["location"];

                string strdataPointsFrom = Request.QueryString["dataPointsFrom"];
                if (strdataPointsFrom == "")
                { strdataPointsFrom = "0"; }
                int? dataPointsFrom = Convert.ToInt32(strdataPointsFrom);
                if (dataPointsFrom == 0)
                {
                    dataPointsFrom = null;
                }
                int optDataPointsFrom = Convert.ToInt32(Request.QueryString["optDataPointsFrom"]);
                string strdataPointsTo = Request.QueryString["dataPointsTo"];
                if (strdataPointsTo == "")
                { strdataPointsTo = "0"; }
                int? dataPointsTo = Convert.ToInt32(strdataPointsTo);
                if (dataPointsTo == 0)
                {
                    dataPointsTo = null;
                }
                int optdataPointsTo = Convert.ToInt32(Request.QueryString["optdataPointsTo"]);


                List<RMC.BusinessEntities.BEReports> objectGenericBEReports = objectBSReports.GetDataForLineChartModified(ProfileCategoryValue, ProfileSubCategoryValue, null, valueAddedCategoryID, OthersCategoryID, LocationCategoryID, hospitalUnitID, firstYear, lastYear, firstMonth, lastMonth, bedInUnitFrom, optBedsInUnitFrom, bedInUnitTo, optBedsInUnitTo, budgetedPatientFrom, optBudgetedPatientFrom, budgetedPatientTo, optBudgetedPatientTo, startDate, endDate, electronicDocumentFrom, optElectronicDocumentFrom, electronicDocumentTo, optElectronicDocumentTo, docByException, unitType, pharmacyType, hospitalType, optHospitalSizeFrom, hospitalSizeFrom, optHospitalSizeTo, hospitalSizeTo, countryId, stateId, activities, value, others, location, dataPointsFrom, optDataPointsFrom, dataPointsTo, optdataPointsTo, null, null, "");
                return objectGenericBEReports;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Method to Generate Line Chart
        /// </summary>
        private void GetLineChartSeries()
        {
            try
            {
                MultiViewCharts.ActiveViewIndex = 1;
                List<RMC.BusinessEntities.BEReports> objectGenericBEReports = GetDataForLineChart();
                RMC.BussinessService.BSChartControl objectBSChartControl = new RMC.BussinessService.BSChartControl();
                //List<Series> objectGenericSeries = objectBSChartControl.GetLineChartSeries(DropDownListMarkerStyle.SelectedItem.Text, Convert.ToInt32(DropDownListMarkerSize.SelectedValue), DropDownListMarkerColor.SelectedItem.Text, DropDownListChartType.SelectedValue, Convert.ToInt32(DropDownListLineWidth.SelectedValue), DropDownListColor.SelectedItem.Text, Convert.ToInt32(DropDownListShadowOffset.SelectedValue), DropDownListPointLabel.SelectedItem.Text, objectGenericBEReports);

                //foreach (Series seriesChart in objectGenericSeries)
                //{
                //    ChartControlCharts.Series.Add(seriesChart);
                //}
                //if (objectGenericSeries.Count == 0)
                //{
                //    ChartControlCharts.Titles["Title1"].Text = "No Data to Display";
                //}
                //else
                //{
                //    ChartControlCharts.Titles["Title1"].Text = "#" + Request.QueryString["hospitalName"] + "   /   " + Request.QueryString["hospitalUnitName"] + "   /   " + Request.QueryString["ProfileSubCategory"] + " (" + Request.QueryString["ProfileCategory"] + ")";
                //}


                //if (System.IO.File.Exists("~\\Uploads\\ChartImg" + CommonClass.UserInformation.UserID.ToString() + ".png"))
                //{
                //    System.IO.File.Delete("~\\Uploads\\ChartImg" + CommonClass.UserInformation.UserID.ToString() + ".png");
                //}
                //ChartControlCharts.SaveImage(Server.MapPath("~\\Uploads\\ChartImg" + CommonClass.UserInformation.UserID.ToString() + ".png"));                
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Gets all selected profile(s) from querystring and fills the 
        /// </summary>
        private void GetAllSelectedProfiles()
        {
            try
            {
                //RMC.BussinessService.BSReports objectBSReports = new RMC.BussinessService.BSReports();

                //string valueAddedCategoryID = Request.QueryString["valueAddedCategoryID"];
                //if (valueAddedCategoryID == "")
                //{
                //    valueAddedCategoryID = null;
                //}
                //string OthersCategoryID = Request.QueryString["OthersCategoryID"];
                //if (OthersCategoryID == "")
                //{
                //    OthersCategoryID = null;
                //}
                //string LocationCategoryID = Request.QueryString["LocationCategoryID"];
                //if (LocationCategoryID == "")
                //{
                //    LocationCategoryID = null;
                //}
                //string value = Request.QueryString["value"];
                //string others = Request.QueryString["others"];
                //string location = Request.QueryString["location"];


                //if (valueAddedCategoryID != null)
                //{
                //    if (valueAddedCategoryID.Contains(","))
                //    {
                //        string[] valueAddedCategoryIdArr = valueAddedCategoryID.Split(new char[] { ',' });
                //        string[] valueArr = value.Split(new char[] { ',' });
                //        for (int index = 0; index <= valueAddedCategoryIdArr.Count() - 1; index++)
                //        {
                //            DropDownListProfileCategory.Items.Add(new ListItem(valueArr[index], valueAddedCategoryIdArr[index]));
                //        }
                //    }
                //    else
                //    {
                //        DropDownListProfileCategory.Items.Add(new ListItem(value, valueAddedCategoryID));
                //    }
                //}
                //if (OthersCategoryID != null)
                //{
                //    if (OthersCategoryID.Contains(","))
                //    {
                //        string[] OthersCategoryIDArr = OthersCategoryID.Split(new char[] { ',' });
                //        string[] othersArr = others.Split(new char[] { ',' });
                //        for (int index = 0; index <= OthersCategoryIDArr.Count() - 1; index++)
                //        {
                //            DropDownListProfileCategory.Items.Add(new ListItem(othersArr[index], OthersCategoryIDArr[index]));
                //        }
                //    }
                //    else
                //    {
                //        DropDownListProfileCategory.Items.Add(new ListItem(others, OthersCategoryID));
                //    }
                //}
                //if (LocationCategoryID != null)
                //{
                //    if (LocationCategoryID.Contains(","))
                //    {
                //        string[] LocationCategoryIDArr = LocationCategoryID.Split(new char[] { ',' });
                //        string[] locationArr = location.Split(new char[] { ',' });
                //        for (int index = 0; index <= LocationCategoryIDArr.Count() - 1; index++)
                //        {
                //            DropDownListProfileCategory.Items.Add(new ListItem(locationArr[index], LocationCategoryIDArr[index]));
                //        }
                //    }
                //    else
                //    {
                //        DropDownListProfileCategory.Items.Add(new ListItem(location, LocationCategoryID));
                //    }
                //}
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        #endregion

    }
}