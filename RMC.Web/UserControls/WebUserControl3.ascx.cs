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
using RMC.BussinessService;
using System.Web.Script.Serialization;
using System.Net;
using System.Net.Mail;

namespace RMC.Web.UserControls
{
    public partial class WebUserControl3 : System.Web.UI.UserControl
    {
        #region Variables

        //Bussiness Service Objects. 
        BSCommon _objectBSCommon = null;
        RMC.BussinessService.BSHospitalInfo _objectBSHospitalInfo = null;
        BSHospitalDemographicDetail _objectBSHospitalDemgraphicDetail = null;

        //Data Service Objects.
        RMC.DataService.HospitalDemographicInfo _objectHospitalDemographicInfo = null;
        RMC.DataService.MultiUserDemographic _objectMultiUserDemographic = null;

        //Generic Data Service Objects.
        List<RMC.DataService.HospitalInfo> _objectGenericHospitalInfo = null;
        List<RMC.BusinessEntities.BEHospitalList> _objectGenericBEHospitalList = null;

        #endregion

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
                    LabelHeading.Text = "Pie Charts";
                }
                else if (Request.QueryString["Report"] == "ControlCharts")
                {
                    LabelHeading.Text = "Control Charts";
                }

                if (!Page.IsPostBack)
                {
                    if (!(HttpContext.Current.User.IsInRole("superadmin")))
                    {
                        if (!CommonClass.UserInformation.IsActive)
                        {
                            Response.Redirect("~/Users/InActiveUser.aspx", false);
                        }
                    }
                    int userID = CommonClass.UserInformation.UserID;
                    GetAllHospitalNames(userID);
                }

                if (DropDownListProfileCategory.SelectedValue == "0")
                {
                    DropDownListProfileSubCategory.Items.Clear();
                    DropDownListProfileSubCategory.Items.Add("Select Explode Point");
                    DropDownListProfileSubCategory.Items[0].Value = 0.ToString();
                }
                if (DropDownListProfileCategory.SelectedItem.Text == "Database Values" || DropDownListProfileCategory.SelectedItem.Text == "Special Category")
                {
                    CheckBoxExplodedPoint.Enabled = false;
                    DropDownListProfileSubCategory.Enabled = false;
                }
                else
                {
                    CheckBoxExplodedPoint.Enabled = true;
                }
                DropDownListProfileSubCategory.Enabled = CheckBoxExplodedPoint.Checked;

                if (!Page.IsPostBack)
                {
                    DropDownListChartType.SelectedValue = "Pie";
                    DropDownListDrawingStyle.SelectedValue = "SoftEdge";
                    DropDownListLabelStyle.SelectedValue = "Outside";
                    CheckBoxExplodedPoint.Checked = false;
                    DropDownListProfileSubCategory.Enabled = false;
                    PanelProperties.Visible = false;
                }
            }
            catch (Exception ex)
            {
                LogManager._stringObject = "ReportTimeRNChartsPie.ascx ----";
                LogManager.SetExceptionDetails(ex, LogManager._stringObject, null);
                LogManager.LogException(ex, LogManager.LoggingCategory.General, LogManager.LoggingLevel.Error);
                CommonClass.Show(LogManager.ShowErrorDetail(ex));
            }
        }

        protected void RadioButtonListMonth_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                GetPieChartSeries();
            }
            catch (Exception ex)
            {
                LogManager._stringObject = "ReportTimeRNChartsPie.ascx ----";
                LogManager.SetExceptionDetails(ex, LogManager._stringObject, null);
                LogManager.LogException(ex, LogManager.LoggingCategory.General, LogManager.LoggingLevel.Error);
                CommonClass.Show(LogManager.ShowErrorDetail(ex));
            }
        }

        protected void DropDownListChartType_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                GetPieChartSeries();
            }
            catch (Exception ex)
            {
                LogManager._stringObject = "ReportTimeRNChartsPie.ascx ----";
                LogManager.SetExceptionDetails(ex, LogManager._stringObject, null);
                LogManager.LogException(ex, LogManager.LoggingCategory.General, LogManager.LoggingLevel.Error);
                CommonClass.Show(LogManager.ShowErrorDetail(ex));
            }
        }

        protected void DropDownListDrawingStyle_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                GetPieChartSeries();
            }
            catch (Exception ex)
            {
                LogManager._stringObject = "ReportTimeRNChartsPie.ascx ----";
                LogManager.SetExceptionDetails(ex, LogManager._stringObject, null);
                LogManager.LogException(ex, LogManager.LoggingCategory.General, LogManager.LoggingLevel.Error);
                CommonClass.Show(LogManager.ShowErrorDetail(ex));
            }
        }

        protected void DropDownListLabelStyle_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                GetPieChartSeries();
            }
            catch (Exception ex)
            {
                LogManager._stringObject = "ReportTimeRNChartsPie.ascx ----";
                LogManager.SetExceptionDetails(ex, LogManager._stringObject, null);
                LogManager.LogException(ex, LogManager.LoggingCategory.General, LogManager.LoggingLevel.Error);
                CommonClass.Show(LogManager.ShowErrorDetail(ex));
            }
        }

        protected void CheckBoxExplodedPoint_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (CheckBoxExplodedPoint.Checked == true)
                {
                    DropDownListProfileSubCategory.Enabled = true;
                    DropDownListProfileSubCategory.SelectedIndex = 1;
                }
                else
                {
                    DropDownListProfileSubCategory.Enabled = false;
                    DropDownListProfileSubCategory.SelectedIndex = 0;
                }
                GetPieChartSeries();
            }
            catch (Exception ex)
            {
                LogManager._stringObject = "ReportTimeRNChartsPie.ascx ----";
                LogManager.SetExceptionDetails(ex, LogManager._stringObject, null);
                LogManager.LogException(ex, LogManager.LoggingCategory.General, LogManager.LoggingLevel.Error);
                CommonClass.Show(LogManager.ShowErrorDetail(ex));
            }
        }

        protected void DropDownListProfileSubCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                GetPieChartSeries();
            }
            catch (Exception ex)
            {
                LogManager._stringObject = "ReportTimeRNChartsPie.ascx ----";
                LogManager.SetExceptionDetails(ex, LogManager._stringObject, null);
                LogManager.LogException(ex, LogManager.LoggingCategory.General, LogManager.LoggingLevel.Error);
                CommonClass.Show(LogManager.ShowErrorDetail(ex));
            }
        }

        protected void DropDownListProfileCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {

                DropDownListProfiles.Items.Clear();
                DropDownListProfiles.Items.Add("Select Profile");
                DropDownListProfiles.Items[0].Value = 0.ToString();


                //If "Database Values" item is selected from profile category dropdown
                if (DropDownListProfileCategory.SelectedValue == "Database Values")
                {
                    DropDownListProfiles.Items.Clear();
                    DropDownListProfiles.Items.Add("Select Profile");
                    DropDownListProfiles.Items[0].Value = 0.ToString();
                    DropDownListProfiles.Items.Add(new ListItem("Last Location", "Last Location"));
                    DropDownListProfiles.Items.Add(new ListItem("Current Location", "Current Location"));
                    DropDownListProfiles.Items.Add(new ListItem("Activity", "Activity"));
                    DropDownListProfiles.Items.Add(new ListItem("Sub-Activity", "Sub-Activity"));
                    DropDownListProfiles.Items.Add(new ListItem("Cognitive", "Cognitive"));
                    DropDownListProfiles.Items.Add(new ListItem("Staffing Model", "Staffing Model"));

                }
                //if "Special Category" item is selected from profile category dropdown
                if (DropDownListProfileCategory.SelectedValue == "Special Category")
                {
                    DropDownListProfiles.Items.Clear();
                    DropDownListProfiles.Items.Add("Select Profile");
                    DropDownListProfiles.Items[0].Value = 0.ToString();

                    RMC.BussinessService.BSReports objectBSReports = new RMC.BussinessService.BSReports();
                    List<RMC.BusinessEntities.BEValidationSpecialType> objectGenericBEValidationSpecialType = null;
                    objectGenericBEValidationSpecialType = objectBSReports.GetSpecialCategory();

                    foreach (RMC.BusinessEntities.BEValidationSpecialType st in objectGenericBEValidationSpecialType)
                    {
                        DropDownListProfiles.Items.Add(new ListItem(st.SpecialCategory, st.SpecialCategory));
                    }
                }

                //DropDownListProfileSubCategory.Items.Clear();
                //DropDownListProfileSubCategory.Items.Add("Select Explode Point");
                //DropDownListProfileSubCategory.Items[0].Value = 0.ToString();
                //CheckBoxExplodedPoint.Checked = false;
                //DropDownListProfileSubCategory.Enabled = false;
            }
            catch (Exception ex)
            {
                LogManager._stringObject = "ReportTimeRNChartsPie.ascx ----";
                LogManager.SetExceptionDetails(ex, LogManager._stringObject, null);
                LogManager.LogException(ex, LogManager.LoggingCategory.General, LogManager.LoggingLevel.Error);
                CommonClass.Show(LogManager.ShowErrorDetail(ex));
            }
        }

        protected void DropDownListHospitalName_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (DropDownListHospitalUnit.Items.Count > 1)
                {
                    DropDownListHospitalUnit.Items.Clear();
                    DropDownListHospitalUnit.Items.Add(new ListItem("Select Unit Name", "0"));
                    DropDownListHospitalUnit.Focus();
                }
                DropDownListHospitalUnit.DataBind();

                DropDownListYearMonthFrom.Items.Clear();
                DropDownListYearMonthFrom.Items.Add(new ListItem("Select Period From", "0"));

                DropDownListYearMonthTo.Items.Clear();
                DropDownListYearMonthTo.Items.Add(new ListItem("Select Period To", "0"));

                //if (DropDownListYear.Items.Count > 1)
                //{
                //    DropDownListYear.Items.Clear();
                //    DropDownListYear.Items.Add(new ListItem("Select Year", "0"));
                //    DropDownListYear.Focus();
                //}
                //DropDownListYear.DataBind();

                //if (DropDownListMonth.Items.Count > 1)
                //{
                //    DropDownListMonth.Items.Clear();
                //    DropDownListMonth.Items.Add(new ListItem("Select Month", "0"));
                //    DropDownListMonth.Focus();
                //}
                //DropDownListMonth.DataBind();
            }
            catch (Exception ex)
            {
                ex.Data.Add("Events", "DropDownListHospitalName_SelectedIndexChanged");
                ex.Data.Add("Page", "DemographicDetail.aspx");
                LogManager._stringObject = "DemographicDetail.aspx ---- DropDownListHospitalName_SelectedIndexChanged";
                LogManager.SetExceptionDetails(ex, LogManager._stringObject, null);
                LogManager.LogException(ex, LogManager.LoggingCategory.General, LogManager.LoggingLevel.Error);
                CommonClass.Show(LogManager.ShowErrorDetail(ex));
            }
        }

        protected void DropDownListHospitalUnit_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                //if (DropDownListYear.Items.Count > 1)
                //{
                //    DropDownListYear.Items.Clear();
                //    DropDownListYear.Items.Add(new ListItem("Select Year", "0"));
                //    DropDownListYear.Focus();
                //}
                //DropDownListYear.DataBind();

                //if (DropDownListMonth.Items.Count > 1)
                //{
                //    DropDownListMonth.Items.Clear();
                //    DropDownListMonth.Items.Add(new ListItem("Select Month", "0"));
                //    DropDownListMonth.Focus();
                //}
                //DropDownListMonth.DataBind();
                GetYearMonth();
            }
            catch (Exception ex)
            {
                LogManager._stringObject = "ReportTimeRNChartsPie.ascx ----";
                LogManager.SetExceptionDetails(ex, LogManager._stringObject, null);
                LogManager.LogException(ex, LogManager.LoggingCategory.General, LogManager.LoggingLevel.Error);
                CommonClass.Show(LogManager.ShowErrorDetail(ex));
            }
        }

        //protected void DropDownListYear_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        if (DropDownListMonth.Items.Count > 1)
        //        {
        //            DropDownListMonth.Items.Clear();
        //            DropDownListMonth.Items.Add(new ListItem("Select Month", "0"));
        //            DropDownListMonth.Focus();
        //        }
        //        DropDownListMonth.DataBind();
        //    }
        //    catch (Exception ex)
        //    {
        //        LogManager._stringObject = "ReportTimeRNChartsPie.ascx ----";
        //        LogManager.SetExceptionDetails(ex, LogManager._stringObject, null);
        //        LogManager.LogException(ex, LogManager.LoggingCategory.General, LogManager.LoggingLevel.Error);
        //        CommonClass.Show(LogManager.ShowErrorDetail(ex));
        //    }
        //}

        protected void ButtonShowChart_Click(object sender, EventArgs e)
        {
            try
            {
                CheckBoxExplodedPoint.Checked = false;
                DropDownListProfileSubCategory.Enabled = false;
                DropDownListProfileSubCategory.Items.Clear();
                DropDownListProfileSubCategory.Items.Add("Select Explode Point");
                DropDownListProfileSubCategory.Items[0].Value = 0.ToString();
                DropDownListProfileSubCategory.DataBind();
                GetPieChartSeries();
                MultiViewChartsPie.ActiveViewIndex = 0;
                PanelProperties.Visible = true;
                if (System.IO.File.Exists("~\\Uploads\\ChartImg" + CommonClass.UserInformation.UserID.ToString() + ".png"))
                {
                    System.IO.File.Delete("~\\Uploads\\ChartImg" + CommonClass.UserInformation.UserID.ToString() + ".png");
                }
                ChartJan.SaveImage(Server.MapPath("~\\Uploads\\ChartImg" + CommonClass.UserInformation.UserID.ToString() + ".png"));

                if (TextBoxPostback.Text.Length > 0)
                {
                    TextBoxPostback.Text = string.Empty;
                }
                else
                {
                    TextBoxPostback.Text = "postback";
                }
            }
            catch (Exception ex)
            {
                LogManager._stringObject = "ReportTimeRNChartsPie.ascx ----";
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
                LogManager._stringObject = "ReportTimeRNChartsPie.ascx ----";
                LogManager.SetExceptionDetails(ex, LogManager._stringObject, null);
                LogManager.LogException(ex, LogManager.LoggingCategory.General, LogManager.LoggingLevel.Error);
                CommonClass.Show(LogManager.ShowErrorDetail(ex));
            }
        }

        #endregion

        #region Private

        /// <summary>
        /// Gets tha data for Pie chart to display
        /// </summary>
        /// <returns></returns>
        private List<RMC.BusinessEntities.BEReports> GetDataForPieChart()
        {
            try
            {
                RMC.BussinessService.BSReports objectBSReports = new RMC.BussinessService.BSReports();
                string ProfileCategoryValue = string.Empty;
                ProfileCategoryValue = DropDownListProfileCategory.SelectedValue.ToString();
                string ProfilesValue = string.Empty;
                ProfilesValue = DropDownListProfiles.SelectedItem.Text;
                int? hospitalUnitID = Convert.ToInt32(DropDownListHospitalUnit.SelectedValue);
                if (hospitalUnitID == 0)
                {
                    hospitalUnitID = null;
                }
                //
                string yearFrom = null, yearTo = null, monthFrom = null, monthTo = null;
                if (DropDownListYearMonthFrom.SelectedValue != "0")
                {

                    string yearMonthFrom = DropDownListYearMonthFrom.SelectedValue;
                    string[] yearMonthFromArr = yearMonthFrom.Split(new char[] { ',' });
                    yearFrom = yearMonthFromArr[1].ToString();
                    monthFrom = yearMonthFromArr[0].ToString();
                }
                if (DropDownListYearMonthTo.SelectedValue != "0")
                {

                    string yearMonthTo = DropDownListYearMonthTo.SelectedValue;
                    string[] yearMonthToArr = yearMonthTo.Split(new char[] { ',' });
                    yearTo = yearMonthToArr[1].ToString();
                    monthTo = yearMonthToArr[0].ToString();
                }
                //
                int? firstYear = Convert.ToInt32(yearFrom);
                //int? firstYear = 0;
                if (firstYear == 0)
                {
                    firstYear = null;
                }
                int? lastYear = Convert.ToInt32(yearTo);
                //int? lastYear = 0;
                if (lastYear == 0)
                {
                    lastYear = null;
                }
                int? firstMonth = Convert.ToInt32(monthFrom);
                //int? firstMonth = 0;
                if (firstMonth == 0)
                {
                    firstMonth = null;
                }
                int? lastMonth = Convert.ToInt32(monthTo);
                //int? lastMonth = 0;
                if (lastMonth == 0)
                {
                    lastMonth = null;
                }
                int? bedInUnit = 0;
                if (bedInUnit == 0)
                {
                    bedInUnit = null;
                }
                int? budgetedPatient = 0;
                if (budgetedPatient == 0)
                {
                    budgetedPatient = null;
                }
                string startDate = "";
                if (startDate == "")
                {
                    startDate = null;
                }
                string endDate = "";
                if (endDate == "")
                {
                    endDate = null;
                }
                int? electronicDocument = 0;
                if (electronicDocument == 0)
                {
                    electronicDocument = null;
                }
                string unitType = "";
                if (unitType == "")
                {
                    unitType = null;
                }
                string pharmacyType = "";
                if (pharmacyType == "")
                {
                    pharmacyType = null;
                }
                int? hospitalSize = 0;
                if (hospitalSize == 0)
                {
                    hospitalSize = null;
                }


                int? valueAddedCategoryId = null;
                int? othersCategoryId = null;
                int? locationCategoryId = null;
                int? activitiesId = null;
                if (DropDownListProfileCategory.SelectedItem.Text == "Value Added")
                {
                    valueAddedCategoryId = Convert.ToInt32(DropDownListProfiles.SelectedValue);
                }
                if (DropDownListProfileCategory.SelectedItem.Text == "Others")
                {
                    othersCategoryId = Convert.ToInt32(DropDownListProfiles.SelectedValue);
                }
                if (DropDownListProfileCategory.SelectedItem.Text == "Location")
                {
                    locationCategoryId = Convert.ToInt32(DropDownListProfiles.SelectedValue);
                }
                if (DropDownListProfileCategory.SelectedItem.Text == "Activities")
                {
                    activitiesId = Convert.ToInt32(DropDownListProfiles.SelectedValue);
                }

                //List<RMC.BusinessEntities.BEReports> objectGenericBEReports = objectBSReports.GetDataForPieChart(ProfileCategoryValue, ProfileCategoryValue, Convert.ToInt32(Request.QueryString["valueAddedCategoryID"]), Convert.ToInt32(Request.QueryString["OthersCategoryID"]), Convert.ToInt32(Request.QueryString["LocationCategoryID"]), hospitalUnitID, firstYear, lastYear, firstMonth, lastMonth, bedInUnit, Convert.ToInt32(Request.QueryString["optBedInUnit"]), budgetedPatient, Convert.ToInt32(Request.QueryString["optBudgetedPatient"]), startDate, endDate, electronicDocument, Convert.ToInt32(Request.QueryString["optElectronicDocument"]), Convert.ToInt32(Request.QueryString["docByException"]), unitType, pharmacyType, Convert.ToInt32(Request.QueryString["optHospitalSize"]), hospitalSize);
                List<RMC.BusinessEntities.BEReports> objectGenericBEReports = objectBSReports.GetDataForPieChartModified(ProfileCategoryValue, ProfilesValue, valueAddedCategoryId, othersCategoryId, locationCategoryId, activitiesId, hospitalUnitID, firstYear, lastYear, firstMonth, lastMonth, bedInUnit, 0, budgetedPatient, 0, startDate, endDate, electronicDocument, 0, 0, unitType, pharmacyType, 0, hospitalSize);
                return objectGenericBEReports;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Calls GetPieChartSeries for Each Month and Display Pie Chart
        /// </summary>
        private void GetPieChartSeries()
        {
            try
            {
                string explodedPoint = string.Empty;
                if (CheckBoxExplodedPoint.Checked == true)
                {
                    explodedPoint = DropDownListProfileSubCategory.SelectedValue;
                }
                else
                {
                    explodedPoint = "None";
                }
                List<RMC.BusinessEntities.BEReports> objectGenericBEReports = GetDataForPieChart();
                RMC.BussinessService.BSChartControl objectBSChartControl = new RMC.BussinessService.BSChartControl();

                List<RMC.BusinessEntities.BEReports> objectGenericBEReportsJan = new List<RMC.BusinessEntities.BEReports>();

                if (objectGenericBEReports != null && objectGenericBEReports.Count != 0)
                {
                    if (DropDownListProfileCategory.SelectedValue == "Database Values")
                    {
                        //foreach (RMC.BusinessEntities.BEReports objectBEReports in objectGenericBEReports)
                        //{
                        //    if (objectBEReports.MonthName == DropDownListMonth.SelectedItem.Text + " " + DropDownListYear.SelectedItem.Text)
                        //    {
                        //        objectGenericBEReportsJan.Add(objectBEReports);
                        //    }
                        //}
                        ////objectGenericBEReports.Sort();
                        ////if (objectGenericBEReports.Count > 15)
                        ////{ 

                        ////}


                        objectGenericBEReportsJan = objectGenericBEReports;



                        if (objectGenericBEReportsJan.Count > 0)
                        {
                            Series objectSeriesJan = objectBSChartControl.GetPieChartSeries(DropDownListChartType.SelectedValue, DropDownListDrawingStyle.SelectedValue, DropDownListLabelStyle.SelectedValue, explodedPoint, objectGenericBEReportsJan);
                            ChartJan.Series.Add(objectSeriesJan);
                            //ChartJan.Titles["Title1"].Text = DropDownListProfiles.SelectedItem.Text + ",  " + "Data Points: " + objectGenericBEReportsJan[0].DataPoint;
                            ChartJan.Titles["Title1"].Text = DropDownListHospitalName.SelectedItem.Text + " / " + DropDownListHospitalUnit.SelectedItem.Text + " / " + DropDownListProfileCategory.SelectedValue + "(" + DropDownListYearMonthFrom.SelectedItem.Text + " To " + DropDownListYearMonthTo.SelectedItem.Text + ")" + " / " + "Data Points: " + objectGenericBEReportsJan[0].DataPoint;


                            string areaName = "";
                            string areaValue = "";
                            string title = DropDownListHospitalName.SelectedItem.Text + " <br/> " + DropDownListHospitalUnit.SelectedItem.Text + " <br/> " + DropDownListProfileCategory.SelectedValue + " (" + DropDownListYearMonthFrom.SelectedItem.Text + " To " + DropDownListYearMonthTo.SelectedItem.Text + ")" + " <br/> " + "Data Points: " + objectGenericBEReportsJan[0].DataPoint;
                            foreach (var item in objectGenericBEReports)
                            {
                                areaName = areaName + item.ColumnName + ",";
                                areaValue = areaValue + item.ValuesSum.ToString() + ",";
                            }

                            string nameList = (new JavaScriptSerializer()).Serialize(areaName);
                            string valueList = (new JavaScriptSerializer()).Serialize(areaValue);

                            Page.ClientScript.RegisterStartupScript(this.GetType(), "click", "DrawChart('" + nameList + "','" + valueList + "', '" + title + "');", true);

                            if (objectSeriesJan.Points.Count > 8)
                            {
                                //ChartJan.ChartAreas[0].Area3DStyle.Enable3D = true;
                                //PanelProperties.Enabled = false;
                                ChartJan.Width = 1000;
                                ChartJan.Height = 850;
                            }
                        }
                        else
                        {
                            CommonClass.Show("No Data To Display.");
                        }
                    }


                    else
                    {
                        //foreach (RMC.BusinessEntities.BEReports objectBEReports in objectGenericBEReports)
                        //{
                        //    if (objectBEReports.MonthName == DropDownListMonth.SelectedItem.Text + " " + DropDownListYear.SelectedItem.Text)
                        //    {
                        //        objectGenericBEReportsJan.Add(objectBEReports);
                        //    }
                        //}

                        objectGenericBEReportsJan = objectGenericBEReports;

                        if (objectGenericBEReportsJan.Count != 0)
                        {
                            Series objectSeriesJan = objectBSChartControl.GetPieChartSeries(DropDownListChartType.SelectedValue, DropDownListDrawingStyle.SelectedValue, DropDownListLabelStyle.SelectedValue, explodedPoint, objectGenericBEReportsJan);
                            ChartJan.Series.Add(objectSeriesJan);
                            ChartJan.Titles["Title1"].Text = DropDownListHospitalName.SelectedItem.Text + " / " + DropDownListHospitalUnit.SelectedItem.Text + " / " + DropDownListProfileCategory.SelectedValue + " (" + DropDownListYearMonthFrom.SelectedItem.Text + " To " + DropDownListYearMonthTo.SelectedItem.Text + ")" + " / " + "Data Points: " + objectGenericBEReportsJan[0].DataPoint;




                            int count = objectGenericBEReportsJan.Count;

                            string areaName = "";
                            string areaValue = "";
                            string title = DropDownListHospitalName.SelectedItem.Text + " <br/> " + DropDownListHospitalUnit.SelectedItem.Text + " <br/> " + DropDownListProfileCategory.SelectedValue + " (" + DropDownListYearMonthFrom.SelectedItem.Text + " To " + DropDownListYearMonthTo.SelectedItem.Text + ")" + " <br/> " + "Data Points: " + objectGenericBEReportsJan[0].DataPoint;
                            foreach (var item in objectGenericBEReports)
                            {
                                areaName = areaName + item.ColumnName + ",";
                                areaValue = areaValue + item.ValuesSum.ToString() + ",";
                            }

                            string nameList = (new JavaScriptSerializer()).Serialize(areaName);
                            string valueList = (new JavaScriptSerializer()).Serialize(areaValue);

                            Page.ClientScript.RegisterStartupScript(this.GetType(), "click", "DrawChart('" + nameList + "','" + valueList + "', '" + title + "');", true);


                            PanelProperties.Enabled = true;
                            if (objectSeriesJan.Points.Count > 8)
                            {
                                //ChartJan.ChartAreas[0].Area3DStyle.Enable3D = true;
                                //PanelProperties.Enabled = false;
                                ChartJan.Width = 1000;
                                ChartJan.Height = 850;
                            }
                        }
                        else
                        {
                            CommonClass.Show("No Data To Display.");
                        }
                    }
                }
                else
                {
                    CommonClass.Show("No Data To Display.");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Use to fetch all hospital names.
        /// Created By : 
        /// Creation Date : 
        /// Modified By : 
        /// Modified Date : 
        /// </summary>
        private void GetAllHospitalNames(int userID)
        {
            try
            {
                _objectBSHospitalInfo = new BSHospitalInfo();

                _objectGenericBEHospitalList = _objectBSHospitalInfo.GetHospitalNamesByUserID(userID);
                if (_objectGenericBEHospitalList.Count > 0)
                {
                    BindDropDownListHospitalNames(_objectGenericBEHospitalList);
                }
                else
                {
                    CommonClass.Show("Hospital Names Does Not Exist.");
                }
            }
            catch (Exception ex)
            {
                ex.Data.Add("Function", "GetAllHospitalNames");
                ex.Data.Add("Class", "DemographicDetail");
                throw ex;
            }
            finally
            {
                _objectBSHospitalInfo = null;
            }
        }

        /// <summary>
        /// Bind DropDown list with HospitalInfo.
        /// Created By : 
        /// Creation Date : 
        /// Modified By : 
        /// Modified Date :
        /// </summary>
        /// <param name="objectGenericHospitalInfo"></param>
        private void BindDropDownListHospitalNames(List<RMC.BusinessEntities.BEHospitalList> objectGenericHospitalInfo)
        {
            try
            {
                if (DropDownListHospitalName.Items.Count > 1)
                {
                    DropDownListHospitalName.Items.Clear();
                    DropDownListHospitalName.Items.Add("Select Hospital");
                    DropDownListHospitalName.Items[0].Value = 0.ToString();
                    DropDownListHospitalName.Items[0].Selected = true;
                }

                DropDownListHospitalName.DataTextField = "HospitalExtendedName";
                DropDownListHospitalName.DataValueField = "HospitalInfoID";
                DropDownListHospitalName.DataSource = objectGenericHospitalInfo;
                DropDownListHospitalName.DataBind();
            }
            catch (Exception ex)
            {
                ex.Data.Add("Function", "BindDropDownListHospitalNames");
                ex.Data.Add("Class", "DemographicDetail");
                throw ex;
            }
            finally
            {
                objectGenericHospitalInfo = null;
            }
        }

        protected void ImageButtonRefresh_Click(object sender, ImageClickEventArgs e)
        {
            string url = Request.Url.ToString();
            //Response.Redirect(url, false);
            Response.AppendHeader("Refresh", url);
        }

        //private List<RMC.BusinessEntities.BEReports> GetDataForPieChart()
        //{
        //    try
        //    {
        //        RMC.BussinessService.BSReports objectBSReports = new RMC.BussinessService.BSReports();
        //        string ProfileCategoryValue;
        //        ProfileCategoryValue = DropDownListProfileCategory.SelectedValue.ToString();
        //        int? hospitalUnitID = Convert.ToInt32(Request.QueryString["hospitalUnitID"]);
        //        if (hospitalUnitID == 0)
        //        {
        //            hospitalUnitID = null;
        //        }
        //        int? firstYear = Convert.ToInt32(Request.QueryString["firstYear"]);
        //        if (firstYear == 0)
        //        {
        //            firstYear = null;
        //        }
        //        int? lastYear = Convert.ToInt32(Request.QueryString["lastYear"]);
        //        if (lastYear == 0)
        //        {
        //            lastYear = null;
        //        }
        //        int? firstMonth = Convert.ToInt32(Request.QueryString["firstMonth"]);
        //        if (firstMonth == 0)
        //        {
        //            firstMonth = null;
        //        }
        //        int? lastMonth = Convert.ToInt32(Request.QueryString["lastMonth"]);
        //        if (lastMonth == 0)
        //        {
        //            lastMonth = null;
        //        }
        //        int? bedInUnit = Convert.ToInt32(Request.QueryString["bedInUnit"]);
        //        if (bedInUnit == 0)
        //        {
        //            bedInUnit = null;
        //        }
        //        int? budgetedPatient = Convert.ToInt32(Request.QueryString["budgetedPatient"]);
        //        if (budgetedPatient == 0)
        //        {
        //            budgetedPatient = null;
        //        }
        //        string startDate = Request.QueryString["startDate"];
        //        if (startDate == "")
        //        {
        //            startDate = null;
        //        }
        //        string endDate = Request.QueryString["endDate"];
        //        if (endDate == "")
        //        {
        //            endDate = null;
        //        }
        //        int? electronicDocument = Convert.ToInt32(Request.QueryString["electronicDocument"]);
        //        if (electronicDocument == 0)
        //        {
        //            electronicDocument = null;
        //        }
        //        string unitType = Request.QueryString["unitType"];
        //        if (unitType == "")
        //        {
        //            unitType = null;
        //        }
        //        string pharmacyType = Request.QueryString["pharmacyType"];
        //        if (pharmacyType == "")
        //        {
        //            pharmacyType = null;
        //        }
        //        int? hospitalSize = Convert.ToInt32(Request.QueryString["hospitalSize"]);
        //        if (hospitalSize == 0)
        //        {
        //            hospitalSize = null;
        //        }
        //        List<RMC.BusinessEntities.BEReports> objectGenericBEReports = objectBSReports.GetDataForPieChart(ProfileCategoryValue, ProfileCategoryValue, Convert.ToInt32(Request.QueryString["valueAddedCategoryID"]), Convert.ToInt32(Request.QueryString["OthersCategoryID"]), Convert.ToInt32(Request.QueryString["LocationCategoryID"]), hospitalUnitID, firstYear, lastYear, firstMonth, lastMonth, bedInUnit, Convert.ToInt32(Request.QueryString["optBedInUnit"]), budgetedPatient, Convert.ToInt32(Request.QueryString["optBudgetedPatient"]), startDate, endDate, electronicDocument, Convert.ToInt32(Request.QueryString["optElectronicDocument"]), Convert.ToInt32(Request.QueryString["docByException"]), unitType, pharmacyType, Convert.ToInt32(Request.QueryString["optHospitalSize"]), hospitalSize);
        //        return objectGenericBEReports;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}

        //private void GetPieChartSeries()
        //{
        //    try
        //    {
        //        string explodedPoint = string.Empty;
        //        if (CheckBoxExplodedPoint.Checked == true)
        //        {
        //            explodedPoint = DropDownListProfileSubCategory.SelectedValue;
        //        }
        //        else
        //        {
        //            explodedPoint = "None";
        //        }
        //        List<RMC.BusinessEntities.BEReports> objectGenericBEReports = GetDataForPieChart();
        //        RMC.BussinessService.BSChartControl objectBSChartControl = new RMC.BussinessService.BSChartControl();

        //        List<RMC.BusinessEntities.BEReports> objectGenericBEReportsJan = new List<RMC.BusinessEntities.BEReports>();
        //        List<RMC.BusinessEntities.BEReports> objectGenericBEReportsFeb = new List<RMC.BusinessEntities.BEReports>();
        //        List<RMC.BusinessEntities.BEReports> objectGenericBEReportsMar = new List<RMC.BusinessEntities.BEReports>();
        //        List<RMC.BusinessEntities.BEReports> objectGenericBEReportsApr = new List<RMC.BusinessEntities.BEReports>();
        //        List<RMC.BusinessEntities.BEReports> objectGenericBEReportsMay = new List<RMC.BusinessEntities.BEReports>();
        //        List<RMC.BusinessEntities.BEReports> objectGenericBEReportsJun = new List<RMC.BusinessEntities.BEReports>();
        //        List<RMC.BusinessEntities.BEReports> objectGenericBEReportsJul = new List<RMC.BusinessEntities.BEReports>();
        //        List<RMC.BusinessEntities.BEReports> objectGenericBEReportsAug = new List<RMC.BusinessEntities.BEReports>();
        //        List<RMC.BusinessEntities.BEReports> objectGenericBEReportsSep = new List<RMC.BusinessEntities.BEReports>();
        //        List<RMC.BusinessEntities.BEReports> objectGenericBEReportsOct = new List<RMC.BusinessEntities.BEReports>();
        //        List<RMC.BusinessEntities.BEReports> objectGenericBEReportsNov = new List<RMC.BusinessEntities.BEReports>();
        //        List<RMC.BusinessEntities.BEReports> objectGenericBEReportsDec = new List<RMC.BusinessEntities.BEReports>();

        //        foreach (RMC.BusinessEntities.BEReports objectBEReports in objectGenericBEReports)
        //        {
        //            if (objectBEReports.MonthName == "January")
        //            {
        //                objectGenericBEReportsJan.Add(objectBEReports);
        //            }
        //            if (objectBEReports.MonthName == "February")
        //            {
        //                objectGenericBEReportsFeb.Add(objectBEReports);
        //            }
        //            if (objectBEReports.MonthName == "March")
        //            {
        //                objectGenericBEReportsMar.Add(objectBEReports);
        //            }
        //            if (objectBEReports.MonthName == "April")
        //            {
        //                objectGenericBEReportsApr.Add(objectBEReports);
        //            }
        //            if (objectBEReports.MonthName == "May")
        //            {
        //                objectGenericBEReportsMay.Add(objectBEReports);
        //            }
        //            if (objectBEReports.MonthName == "June")
        //            {
        //                objectGenericBEReportsJun.Add(objectBEReports);
        //            }
        //            if (objectBEReports.MonthName == "July")
        //            {
        //                objectGenericBEReportsJul.Add(objectBEReports);
        //            }
        //            if (objectBEReports.MonthName == "August")
        //            {
        //                objectGenericBEReportsAug.Add(objectBEReports);
        //            }
        //            if (objectBEReports.MonthName == "September")
        //            {
        //                objectGenericBEReportsSep.Add(objectBEReports);
        //            }
        //            if (objectBEReports.MonthName == "October")
        //            {
        //                objectGenericBEReportsOct.Add(objectBEReports);
        //            }
        //            if (objectBEReports.MonthName == "November")
        //            {
        //                objectGenericBEReportsNov.Add(objectBEReports);
        //            }
        //            if (objectBEReports.MonthName == "December")
        //            {
        //                objectGenericBEReportsDec.Add(objectBEReports);
        //            }
        //        }
        //        Series objectSeriesJan = objectBSChartControl.GetPieChartSeries(DropDownListChartType.SelectedValue, DropDownListDrawingStyle.SelectedValue, DropDownListLabelStyle.SelectedValue, explodedPoint, objectGenericBEReportsJan);
        //        ChartJan.Series.Add(objectSeriesJan);
        //        ChartJan.Titles["Title1"].Text = DropDownListProfileCategory.SelectedValue + " (January)";
        //        Series objectSeriesFeb = objectBSChartControl.GetPieChartSeries(DropDownListChartType.SelectedValue, DropDownListDrawingStyle.SelectedValue, DropDownListLabelStyle.SelectedValue, explodedPoint, objectGenericBEReportsFeb);
        //        ChartFeb.Series.Add(objectSeriesFeb);
        //        ChartFeb.Titles["Title1"].Text = DropDownListProfileCategory.SelectedValue + " (February)";
        //        Series objectSeriesMar = objectBSChartControl.GetPieChartSeries(DropDownListChartType.SelectedValue, DropDownListDrawingStyle.SelectedValue, DropDownListLabelStyle.SelectedValue, explodedPoint, objectGenericBEReportsMar);
        //        ChartMar.Series.Add(objectSeriesMar);
        //        ChartMar.Titles["Title1"].Text = DropDownListProfileCategory.SelectedValue + " (March)";
        //        Series objectSeriesApr = objectBSChartControl.GetPieChartSeries(DropDownListChartType.SelectedValue, DropDownListDrawingStyle.SelectedValue, DropDownListLabelStyle.SelectedValue, explodedPoint, objectGenericBEReportsApr);
        //        ChartApr.Series.Add(objectSeriesApr);
        //        ChartApr.Titles["Title1"].Text = DropDownListProfileCategory.SelectedValue + " (April)";
        //        Series objectSeriesMay = objectBSChartControl.GetPieChartSeries(DropDownListChartType.SelectedValue, DropDownListDrawingStyle.SelectedValue, DropDownListLabelStyle.SelectedValue, explodedPoint, objectGenericBEReportsMay);
        //        ChartMay.Series.Add(objectSeriesMay);
        //        ChartMay.Titles["Title1"].Text = DropDownListProfileCategory.SelectedValue + " (May)";
        //        Series objectSeriesJun = objectBSChartControl.GetPieChartSeries(DropDownListChartType.SelectedValue, DropDownListDrawingStyle.SelectedValue, DropDownListLabelStyle.SelectedValue, explodedPoint, objectGenericBEReportsJun);
        //        ChartJun.Series.Add(objectSeriesJun);
        //        ChartJun.Titles["Title1"].Text = DropDownListProfileCategory.SelectedValue + " (June)";
        //        Series objectSeriesJul = objectBSChartControl.GetPieChartSeries(DropDownListChartType.SelectedValue, DropDownListDrawingStyle.SelectedValue, DropDownListLabelStyle.SelectedValue, explodedPoint, objectGenericBEReportsJul);
        //        ChartJul.Series.Add(objectSeriesJul);
        //        ChartJul.Titles["Title1"].Text = DropDownListProfileCategory.SelectedValue + " (July)";
        //        Series objectSeriesAug = objectBSChartControl.GetPieChartSeries(DropDownListChartType.SelectedValue, DropDownListDrawingStyle.SelectedValue, DropDownListLabelStyle.SelectedValue, explodedPoint, objectGenericBEReportsAug);
        //        ChartAug.Series.Add(objectSeriesAug);
        //        ChartAug.Titles["Title1"].Text = DropDownListProfileCategory.SelectedValue + " (August)";
        //        Series objectSeriesSep = objectBSChartControl.GetPieChartSeries(DropDownListChartType.SelectedValue, DropDownListDrawingStyle.SelectedValue, DropDownListLabelStyle.SelectedValue, explodedPoint, objectGenericBEReportsSep);
        //        ChartSep.Series.Add(objectSeriesSep);
        //        ChartSep.Titles["Title1"].Text = DropDownListProfileCategory.SelectedValue + " (September)";
        //        Series objectSeriesOct = objectBSChartControl.GetPieChartSeries(DropDownListChartType.SelectedValue, DropDownListDrawingStyle.SelectedValue, DropDownListLabelStyle.SelectedValue, explodedPoint, objectGenericBEReportsOct);
        //        ChartOct.Series.Add(objectSeriesOct);
        //        ChartOct.Titles["Title1"].Text = DropDownListProfileCategory.SelectedValue + " (October)";
        //        Series objectSeriesNov = objectBSChartControl.GetPieChartSeries(DropDownListChartType.SelectedValue, DropDownListDrawingStyle.SelectedValue, DropDownListLabelStyle.SelectedValue, explodedPoint, objectGenericBEReportsNov);
        //        ChartNov.Series.Add(objectSeriesNov);
        //        ChartNov.Titles["Title1"].Text = DropDownListProfileCategory.SelectedValue + " (November)";
        //        Series objectSeriesDec = objectBSChartControl.GetPieChartSeries(DropDownListChartType.SelectedValue, DropDownListDrawingStyle.SelectedValue, DropDownListLabelStyle.SelectedValue, explodedPoint, objectGenericBEReportsDec);
        //        ChartDec.Series.Add(objectSeriesDec);
        //        ChartDec.Titles["Title1"].Text = DropDownListProfileCategory.SelectedValue + " (December)";
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}
        private void GetYearMonth()
        {
            RMC.BussinessService.BSReports objectBSReports = new RMC.BussinessService.BSReports();
            List<RMC.BusinessEntities.BEReportsYearMonth> objectGenericBEReportsYearMonth = new List<RMC.BusinessEntities.BEReportsYearMonth>();

            try
            {
                objectGenericBEReportsYearMonth = objectBSReports.GetYearMonthComboByUnitId(Convert.ToInt32(DropDownListHospitalUnit.SelectedValue));
                if (DropDownListYearMonthFrom.Items.Count > 0)
                {
                    DropDownListYearMonthFrom.Items.Clear();
                    DropDownListYearMonthFrom.Items.Add(new ListItem("Select Period From", "0"));

                }
                if (DropDownListYearMonthTo.Items.Count > 0)
                {
                    DropDownListYearMonthTo.Items.Clear();
                    DropDownListYearMonthTo.Items.Add(new ListItem("Select Period To", "0"));
                }
                if (objectGenericBEReportsYearMonth.Count > 1)
                {

                    foreach (RMC.BusinessEntities.BEReportsYearMonth ym in objectGenericBEReportsYearMonth)
                    {
                        //RMC.BussinessService.BSCommon objectBSCommon = new RMC.BussinessService.BSCommon();

                        DropDownListYearMonthFrom.Items.Add(new ListItem(BSCommon.GetMonthName(ym.month) + ", " + ym.year, ym.month + "," + ym.year));
                        DropDownListYearMonthTo.Items.Add(new ListItem(BSCommon.GetMonthName(ym.month) + ", " + ym.year, ym.month + "," + ym.year));
                        //ListBoxAvailableProfiles.Items.Add(new ListItem(pt.ProfileTypeID + "," + pt.Type, pt.ProfileName));
                    }

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        RMC.BussinessService.BSEmail _objectBSEmail = null;
        bool _flag, _emailFlag;

        protected void Button1_Click(object sender, EventArgs e)
        {
            SendMail();

            _objectBSEmail = new RMC.BussinessService.BSEmail("members@rapidmodeling.com", "nida.nidamalik@gmail.com", "test", "test", true);
           _objectBSEmail.SendMail(true, out _emailFlag);


           //_objectBSEmail.EmailSendMail();
        }

        protected void SendMail()
        {
            string body = "body";
            string emailFrom = System.Configuration.ConfigurationManager.AppSettings["Contact"];
            string subject = "Confirmation Email";
            string emailTo = "safdaraltaf@yahoo.co.uk";
          //  bool r = classSendEmail.SendEMail(emailFrom, emailTo, subject, body, true);


            try
            {
                SmtpClient client = new SmtpClient();
                MailMessage objMailMessage = new MailMessage(emailFrom, emailTo);
                objMailMessage.Subject = subject;
                objMailMessage.Body = body;
                objMailMessage.IsBodyHtml = true;
                objMailMessage.Priority = MailPriority.High;

                client.Send(objMailMessage);
                               

            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }


    }
}