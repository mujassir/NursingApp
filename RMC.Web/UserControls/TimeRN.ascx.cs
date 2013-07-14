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
using LogExceptions;
using RMC.BussinessService;
using RMC.BusinessEntities;
using System.Web.UI.DataVisualization.Charting;

namespace RMC.Web.UserControls
{
    public partial class TimeRN : System.Web.UI.UserControl
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

        string unitType = string.Empty, pharmacyType = string.Empty, hospitalType = string.Empty;
        string valueAddedCategory = string.Empty, activitiesID = string.Empty, othersCategory = string.Empty, locationCategory = string.Empty;

        #endregion

        #region Events
        
        /// <summary>
        /// fills all dropdowns and listboxes when page loads first time
        /// </summary>
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (Request.QueryString["Report"] == "Dashboard")
                {
                    LabelHeading.Text = "LEAN Dashboard";
                }
                else if (Request.QueryString["Report"] == "PieCharts")
                {
                    LabelHeading.Text = "Monthly Data - Pie Charts";
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
                    GetAllAvailableProfiles(userID);
                    //GetYearMonth();


                    
                    //--For Default Settings of a page
                    if (Request.QueryString["filter"] == null)
                    {
                        ListItem listItemTCABvalue = ListBoxAvailableProfiles.Items.FindByText("TCAB Value Add");
                        if (listItemTCABvalue != null)
                        {
                            ListBoxAvailableProfiles.Items.Remove(listItemTCABvalue);
                            ListBoxSelectedProfiles.Items.Add(listItemTCABvalue);
                        }
                        ListItem listItemTCABgeneral = ListBoxAvailableProfiles.Items.FindByText("TCAB General Categories");
                        if (listItemTCABgeneral != null)
                        {
                            ListBoxAvailableProfiles.Items.Remove(listItemTCABgeneral);
                            ListBoxSelectedProfiles.Items.Add(listItemTCABgeneral);
                        }
                        ListItem listItemTCABlocation = ListBoxAvailableProfiles.Items.FindByText("TCAB Location Categories");
                        if (listItemTCABlocation != null)
                        {
                            ListBoxAvailableProfiles.Items.Remove(listItemTCABlocation);
                            ListBoxSelectedProfiles.Items.Add(listItemTCABlocation);
                        }
                    }
                    //--------------------------------
                }

                if (Page.IsPostBack != true)
                {
                    if (Request.QueryString["filter"] != null)
                    {
                        string filter = Request.QueryString["filter"];
                        string beginingDate = Request.QueryString["beginingDate"];
                        string endingDate = Request.QueryString["endingDate"];
                        string value = Request.QueryString["value"];
                        string others = Request.QueryString["others"];
                        string location = Request.QueryString["location"];
                        string activities = Request.QueryString["activities"];
                        string hospitalName  = "#" + Request.QueryString["hospitalName"];
                        string hospitalUnitName = Request.QueryString["hospitalUnitName"];
                        
                        string[] strArrvalue = null, strArrothers = null, strArrlocation = null, strArractivities;
                        strArrvalue = value.Split(new char[] { ',' });
                        strArrothers = others.Split(new char[] { ',' });
                        strArrlocation = location.Split(new char[] { ',' });
                        strArractivities = activities.Split(new char[] { ',' });
                        if (value != "")
                        {
                            if (value.Contains(","))
                            {
                                for (int i = 0; i < strArrvalue.Length; i++)
                                {
                                    string valueText = ListBoxAvailableProfiles.Items.FindByText(strArrvalue[i]).Value;
                                    ListBoxAvailableProfiles.Items.Remove(new ListItem(strArrvalue[i], valueText));
                                    ListBoxSelectedProfiles.Items.Add(new ListItem(strArrvalue[i], valueText));
                                    valueText = "";
                                }
                            }
                            else
                            {
                                string valueText = ListBoxAvailableProfiles.Items.FindByText(value).Value;
                                ListBoxAvailableProfiles.Items.Remove(new ListItem(value, valueText));
                                ListBoxSelectedProfiles.Items.Add(new ListItem(value, valueText));
                            }
                        }

                        if (others != "")
                        {
                            if (others.Contains(","))
                            {
                                for (int i = 0; i < strArrothers.Length; i++)
                                {
                                    string otherText = ListBoxAvailableProfiles.Items.FindByText(strArrothers[i]).Value;
                                    ListBoxAvailableProfiles.Items.Remove(new ListItem(strArrothers[i], otherText));
                                    ListBoxSelectedProfiles.Items.Add(new ListItem(strArrothers[i], otherText));
                                    otherText = "";
                                }
                            }
                            else
                            {
                                string othersText = ListBoxAvailableProfiles.Items.FindByText(others).Value;
                                ListBoxAvailableProfiles.Items.Remove(new ListItem(others, othersText));
                                ListBoxSelectedProfiles.Items.Add(new ListItem(others, othersText));
                            }
                        }

                        if (location != "")
                        {
                            if (location.Contains(","))
                            {
                                for (int i = 0; i < strArrlocation.Length; i++)
                                {
                                    string locationText = ListBoxAvailableProfiles.Items.FindByText(strArrlocation[i]).Value;
                                    ListBoxAvailableProfiles.Items.Remove(new ListItem(strArrlocation[i], locationText));
                                    ListBoxSelectedProfiles.Items.Add(new ListItem(strArrlocation[i], locationText));
                                    locationText = "";
                                }
                            }
                            else
                            {
                                string locationText = ListBoxAvailableProfiles.Items.FindByText(location).Value;
                                ListBoxAvailableProfiles.Items.Remove(new ListItem(location, locationText));
                                ListBoxSelectedProfiles.Items.Add(new ListItem(location, locationText));
                            }
                        }
                        if (activities != "")
                        {
                            if (activities.Contains(","))
                            {
                                for (int i = 0; i < strArractivities.Length; i++)
                                {
                                    string activitiesText = ListBoxAvailableProfiles.Items.FindByText(strArractivities[i]).Value;
                                    ListBoxAvailableProfiles.Items.Remove(new ListItem(strArractivities[i], activitiesText));
                                    ListBoxSelectedProfiles.Items.Add(new ListItem(strArractivities[i], activitiesText));
                                    activitiesText = "";
                                }
                            }
                            else
                            {
                                string activitiesText = ListBoxAvailableProfiles.Items.FindByText(activities).Value;
                                ListBoxAvailableProfiles.Items.Remove(new ListItem(activities, activitiesText));
                                ListBoxSelectedProfiles.Items.Add(new ListItem(activities, activitiesText));
                            }
                        }

                        if (filter != null)
                        {
                            DropDownListBenchmarkingFilter.DataBind();
                            DropDownListBenchmarkingFilter.SelectedItem.Selected = false;
                            DropDownListBenchmarkingFilter.Items.FindByText(filter).Selected = true;
                        }
                        if (beginingDate != null)
                        {
                            DropDownListYearMonthFrom.Items.FindByText(beginingDate).Selected = true;
                        }
                        if (endingDate != null)
                        {
                            DropDownListYearMonthTo.Items.FindByText(endingDate).Selected = true;
                        }
                        if (hospitalName != null)
                        {
                            DropDownListHospitalName.Items.FindByText(hospitalName).Selected = true;
                        }
                        DropDownListHospitalUnit.DataBind();
                        if (hospitalUnitName != null)
                        {
                            DropDownListHospitalUnit.Items.FindByText(hospitalUnitName).Selected = true;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ex.Data.Add("Events", "Page_Load");
                ex.Data.Add("Page", "DemographicDetail.aspx");
                LogManager._stringObject = "DemographicDetail.aspx ---- Page_Load";
                LogManager.SetExceptionDetails(ex, LogManager._stringObject, null);
                LogManager.LogException(ex, LogManager.LoggingCategory.General, LogManager.LoggingLevel.Error);
                CommonClass.Show(LogManager.ShowErrorDetail(ex));
            }
        }

        /// <summary>
        /// Hospital Unit Name embedded in dropdown of HospitalUnit when particular hospital name select
        /// from dropdown of hospital name
        /// </summary>
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

        /// <summary>
        /// Use for moving data from one listbox to another
        /// </summary>
        protected void ButtonAdd_Click(object sender, EventArgs e)
        {
            try
            {
                if (ListBoxAvailableProfiles.SelectedIndex != -1)
                {
                    ListBoxSelectedProfiles.Items.Add(ListBoxAvailableProfiles.SelectedItem);
                    ListBoxAvailableProfiles.Items.Remove(ListBoxAvailableProfiles.SelectedItem);
                    ListBoxSelectedProfiles.ClearSelection();
                }
            }
            catch (Exception ex)
            {
                LogManager._stringObject = "UserNotification.ascx ---- Page_Load";
                LogManager.SetExceptionDetails(ex, LogManager._stringObject, null);
                LogManager.LogException(ex, LogManager.LoggingCategory.General, LogManager.LoggingLevel.Error);
                CommonClass.Show(LogManager.ShowErrorDetail(ex));
            }
        }
        
        /// <summary>
        /// Use for moving data from one listbox to another
        /// </summary>
        protected void ButtonRemove_Click(object sender, EventArgs e)
        {
            try
            {
                if (ListBoxSelectedProfiles.SelectedIndex != -1)
                {
                    ListBoxAvailableProfiles.Items.Add(ListBoxSelectedProfiles.SelectedItem);
                    ListBoxSelectedProfiles.Items.Remove(ListBoxSelectedProfiles.SelectedItem);
                    ListBoxAvailableProfiles.ClearSelection();
                }
            }
            catch (Exception ex)
            {
                LogManager._stringObject = "UserNotification.ascx ---- Page_Load";
                LogManager.SetExceptionDetails(ex, LogManager._stringObject, null);
                LogManager.LogException(ex, LogManager.LoggingCategory.General, LogManager.LoggingLevel.Error);
                CommonClass.Show(LogManager.ShowErrorDetail(ex));
            }
        }

        /// <summary>
        /// Calculates data for reporting and redirects to report page
        /// </summary>
        protected void ButtonGenerateReport_Click(object sender, EventArgs e)
        {
            
            try
            {
                LinkButtonSaveImage.Visible = true;
                ChartTimeRN.Visible = true;
                LinkButtonExportReport.Visible = true;
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

                if (ListBoxSelectedProfiles.Items.Count == 0 || Convert.ToInt32(yearFrom) > Convert.ToInt32(yearTo) || (Convert.ToInt32(yearFrom) == Convert.ToInt32(yearTo) && Convert.ToInt32(monthFrom) > Convert.ToInt32(monthTo)))
                {
                    if (ListBoxSelectedProfiles.Items.Count == 0)
                    {
                        CommonClass.Show("Atleast One Profile Must Be Chosen");
                    }
                    if (Convert.ToInt32(yearFrom) > Convert.ToInt32(yearTo) || (Convert.ToInt32(yearFrom) == Convert.ToInt32(yearTo) && Convert.ToInt32(monthFrom) > Convert.ToInt32(monthTo)))
                    {
                        CommonClass.Show("PeriodTo Cannot Be Less Than PeriodFrom");
                    }
                }
                else
                {
                    RMC.DataService.BenchmarkFilter objectBenchmarkingFilter = new RMC.DataService.BenchmarkFilter();
                    RMC.BussinessService.BSReports objectBSReports = new RMC.BussinessService.BSReports();
                    try
                    {
                        if (ListBoxSelectedProfiles.Items.Count < 11)
                        {
                            string bedsInUnitFrom, optBedsInUnitFrom, bedsInUnitTo, optBedsInUnitTo, budgetedPatientFrom, optBudgetedPatientFrom, budgetedPatientTo, optBudgetedPatientTo, electronicDocumentationFrom, optElectronicDocumentationFrom, electronicDocumentationTo, optElectronicDocumentationTo, docByException, unitType, pharmacyType, hospitalType, hospitalSizeFrom, optHospitalSizeFrom, hospitalSizeTo, optHospitalSizeTo, countryID, stateID, dataPointsFrom, optDataPointsFrom, dataPointsTo, optDataPointsTo, configName, unitIds;
                            if (DropDownListBenchmarkingFilter.SelectedIndex == 1)
                            {
                                bedsInUnitFrom = null;
                                optBedsInUnitFrom = "0";
                                bedsInUnitTo = null;
                                optBedsInUnitTo = "0";
                                budgetedPatientFrom = null;
                                optBudgetedPatientFrom = "0";
                                budgetedPatientTo = null;
                                optBudgetedPatientTo = "0";
                                electronicDocumentationFrom = null;
                                optElectronicDocumentationFrom = "0";
                                electronicDocumentationTo = null;
                                optElectronicDocumentationTo = "0";
                                docByException = "0";
                                unitType = null;
                                pharmacyType = null;
                                hospitalType = null;
                                hospitalSizeFrom = null;
                                optHospitalSizeFrom = "0";
                                hospitalSizeTo = null;
                                optHospitalSizeTo = "0";
                                countryID = "0";
                                stateID = "0";

                                dataPointsFrom = null;
                                optDataPointsFrom = "0";
                                dataPointsTo = null;
                                optDataPointsTo = "0";
                                configName = null;
                                unitIds = null;
                            }
                            else
                            {
                                objectBenchmarkingFilter = objectBSReports.GetBenchmarkFilterRow(Convert.ToInt32(DropDownListBenchmarkingFilter.SelectedValue), DropDownListBenchmarkingFilter.SelectedItem.Text);
                                bedsInUnitFrom = objectBenchmarkingFilter.BedsInUnitFrom.ToString();
                                if (bedsInUnitFrom == "0")
                                {
                                    bedsInUnitFrom = null;
                                }
                                optBedsInUnitFrom = objectBenchmarkingFilter.optBedsInUnitFrom.ToString();
                                bedsInUnitTo = objectBenchmarkingFilter.BedsInUnitTo.ToString();
                                if (bedsInUnitTo == "0")
                                {
                                    bedsInUnitTo = null;
                                }
                                optBedsInUnitTo = objectBenchmarkingFilter.optBedsInUnitTo.ToString();

                                budgetedPatientFrom = objectBenchmarkingFilter.BudgetedPatientFrom.ToString();
                                if (budgetedPatientFrom == "0")
                                {
                                    budgetedPatientFrom = null;
                                }
                                optBudgetedPatientFrom = objectBenchmarkingFilter.optBudgetedPatientFrom.ToString();
                                budgetedPatientTo = objectBenchmarkingFilter.BudgetedPatientTo.ToString();
                                if (budgetedPatientTo == "0")
                                {
                                    budgetedPatientTo = null;
                                }
                                optBudgetedPatientTo = objectBenchmarkingFilter.optBudgetedPatientTo.ToString();

                                electronicDocumentationFrom = objectBenchmarkingFilter.ElectronicDocumentationFrom.ToString();
                                if (electronicDocumentationFrom == "0")
                                {
                                    electronicDocumentationFrom = null;
                                }
                                optElectronicDocumentationFrom = objectBenchmarkingFilter.optElectronicDocumentationFrom.ToString();
                                electronicDocumentationTo = objectBenchmarkingFilter.ElectronicDocumentationTo.ToString();
                                if (electronicDocumentationTo == "0")
                                {
                                    electronicDocumentationTo = null;
                                }
                                optElectronicDocumentationTo = objectBenchmarkingFilter.optElectronicDocumentationTo.ToString();

                                docByException = objectBenchmarkingFilter.DocByException.ToString();

                                unitType = objectBenchmarkingFilter.UnitType.ToString();
                                if (unitType == "0")
                                {
                                    unitType = null;
                                }

                                pharmacyType = objectBenchmarkingFilter.PharmacyType.ToString();
                                if (pharmacyType == "0")
                                {
                                    pharmacyType = null;
                                }

                                hospitalType = objectBenchmarkingFilter.HospitalType.ToString();
                                if (hospitalType == "0")
                                {
                                    hospitalType = null;
                                }

                                hospitalSizeFrom = objectBenchmarkingFilter.HospitalSizeFrom.ToString();
                                if (hospitalSizeFrom == "0")
                                {
                                    hospitalSizeFrom = null;
                                }
                                optHospitalSizeFrom = objectBenchmarkingFilter.optHospitalSizeFrom.ToString();
                                hospitalSizeTo = objectBenchmarkingFilter.HospitalSizeTo.ToString();
                                if (hospitalSizeTo == "0")
                                {
                                    hospitalSizeTo = null;
                                }
                                optHospitalSizeTo = objectBenchmarkingFilter.optHospitalSizeTo.ToString();

                                countryID = objectBenchmarkingFilter.CountryId.ToString();
                                stateID = objectBenchmarkingFilter.StateId.ToString();

                                dataPointsFrom = objectBenchmarkingFilter.DataPointsFrom.ToString();
                                if (dataPointsFrom == "0")
                                {
                                    dataPointsFrom = null;
                                }
                                optDataPointsFrom = objectBenchmarkingFilter.optDataPointsFrom.ToString();
                                dataPointsTo = objectBenchmarkingFilter.DataPointsTo.ToString();
                                if (dataPointsTo == "0")
                                {
                                    dataPointsTo = null;
                                }
                                optDataPointsTo = objectBenchmarkingFilter.optDataPointsTo.ToString();

                                configName = objectBenchmarkingFilter.ConfigurationName.ToString();
                                if (configName == "0")
                                {
                                    configName = null;
                                }

                                unitIds = objectBenchmarkingFilter.HospitalUnitIds.ToString();
                                if (unitIds == "0")
                                {
                                    unitIds = null;
                                }
                            }

                            string value = "", others = "", location = "", activities = "";
                            if (ListBoxSelectedProfiles.Items.Count > 0)
                            {
                                for (int Index = 0; Index < ListBoxSelectedProfiles.Items.Count; Index++)
                                {
                                    if (ListBoxSelectedProfiles.Items[Index].Value.Contains("Activities"))
                                    {
                                        activitiesID += ListBoxSelectedProfiles.Items[Index].Value.Replace(",Activities", "") + ",";
                                        activities += ListBoxSelectedProfiles.Items[Index].Text + ",";
                                        
                                    }
                                    if (ListBoxSelectedProfiles.Items[Index].Value.Contains("Value Added"))
                                    {
                                        valueAddedCategory += ListBoxSelectedProfiles.Items[Index].Value.Replace(",Value Added", "") + ",";
                                        value += ListBoxSelectedProfiles.Items[Index].Text + ",";
                                    }
                                    if (ListBoxSelectedProfiles.Items[Index].Value.Contains("Others"))
                                    {
                                        othersCategory += ListBoxSelectedProfiles.Items[Index].Value.Replace(",Others", "") + ",";
                                        others += ListBoxSelectedProfiles.Items[Index].Text + ",";
                                    }
                                    if (ListBoxSelectedProfiles.Items[Index].Value.Contains("Location"))
                                    {
                                        locationCategory += ListBoxSelectedProfiles.Items[Index].Value.Replace(",Location", "") + ",";
                                        location += ListBoxSelectedProfiles.Items[Index].Text + ",";
                                    }
                                    if (Index == (ListBoxSelectedProfiles.Items.Count - 1) && activitiesID != "")
                                    {
                                        activitiesID = activitiesID.Remove(activitiesID.Length - 1, 1);
                                        activities = activities.Remove(activities.Length - 1, 1);
                                       
                                    }

                                    if (Index == (ListBoxSelectedProfiles.Items.Count - 1) && valueAddedCategory != "")
                                    {
                                        valueAddedCategory = valueAddedCategory.Remove(valueAddedCategory.Length - 1, 1);
                                        value = value.Remove(value.Length - 1, 1);
                                    }
                                    if (Index == (ListBoxSelectedProfiles.Items.Count - 1) && othersCategory != "")
                                    {
                                        othersCategory = othersCategory.Remove(othersCategory.Length - 1, 1);
                                        others = others.Remove(others.Length - 1, 1);
                                    }
                                    if (Index == (ListBoxSelectedProfiles.Items.Count - 1) && locationCategory != "")
                                    {
                                        locationCategory = locationCategory.Remove(locationCategory.Length - 1, 1);
                                        location = location.Remove(location.Length - 1, 1);
                                    }
                                }
                            }
                            string hospitalUnitID = DropDownListHospitalUnit.SelectedValue;
                            //string st = "ReportHospitalBenchmark.aspx?valueAddedCategoryID=" + +"&OthersCategoryID=" + +"&LocationCategoryID=" + +"&firstYear=" + +"&lastYear=" + +"&firstMonth=" + +"&lastMonth=" + +"&bedInUnit=" + TextBoxBedsInUnit.Text + "&optBedInUnit=" + DropDownListBedsInUnitOperator.SelectedValue + "&budgetedPatient=" + TextBoxBudgetedPatient.Text + "&optBudgetedPatient=" + DropDownListBudgetedPatientOperator.SelectedValue + "&electronicDocument=" + TextBoxElectronicDocumentation.Text + "&optElectronicDocument=" + DropDownListElectronicDocumentationOperator.SelectedValue + "&docByException=" + DropDownListDocByException.SelectedValue + "&unitType=" + unitType + "&pharmacyType=" + pharmacyType + "&hospitalUnitID=" + DropDownListHospitalUnit.SelectedValue + "&startDate=" + TextBoxStartDate.Text + "&endDate=" + TextBoxEndDate.Text + "&optHospitalSize=" + DropDownListHospitalSizeOperator.SelectedValue + "&hospitalSize=" + TextBoxHospitalSize.Text;

                            int RecordCounter = objectBSReports.GetRecordCounterOfHospitalID(Convert.ToInt32(DropDownListHospitalName.SelectedValue));
                            string filter = DropDownListBenchmarkingFilter.SelectedItem.Text;
                            if (DropDownListBenchmarkingFilter.SelectedIndex == 0)
                            {
                                filter = "No Filter";
                            }
                            string beginingDate, endingDate;
                            if (DropDownListYearMonthFrom.SelectedIndex != 0)
                            {
                                beginingDate = DropDownListYearMonthFrom.SelectedItem.Text;
                                endingDate = DropDownListYearMonthTo.SelectedItem.Text;
                            }
                            else
                            {
                                beginingDate = DropDownListYearMonthFrom.Items[1].Text;
                                int count = DropDownListYearMonthTo.Items.Count;
                                endingDate = DropDownListYearMonthTo.Items[count - 1].Text;
                            }

                            //string url = string.Empty;
                            //if (Request.QueryString["Report"] == "Dashboard")
                            //{
                            //    //url = "ReportTimeRN.aspx?Report=Dashboard&valueAddedCategoryID=" + valueAddedCategory + "&OthersCategoryID=" + othersCategory + "&LocationCategoryID=" + locationCategory + "&yearFrom=" + yearFrom + "&yearTo=" + yearTo + "&monthFrom=" + monthFrom + "&monthTo=" + monthTo + "&BedsInUnitFrom=" + bedsInUnitFrom + "&optBedsInUnitFrom=" + optBedsInUnitFrom + "&bedsInUnitTo=" + bedsInUnitTo + "&optBedsInUnitTo=" + optBedsInUnitTo + "&budgetedPatientFrom=" + budgetedPatientFrom + "&optBudgetedPatientFrom=" + optBudgetedPatientFrom + "&budgetedPatientTo=" + budgetedPatientTo + "&optBudgetedPatientTo=" + optBudgetedPatientTo + "&electronicDocumentationFrom=" + electronicDocumentationFrom + "&optElectronicDocumentationFrom=" + optElectronicDocumentationFrom + "&electronicDocumentationTo=" + electronicDocumentationTo + "&optElectronicDocumentationTo=" + optElectronicDocumentationTo + "&docByException=" + docByException + "&unitType=" + unitType + "&pharmacyType=" + pharmacyType + "&hospitalType=" + hospitalType + "&hospitalSizeFrom=" + hospitalSizeFrom + "&optHospitalSizeFrom=" + optHospitalSizeFrom + "&hospitalSizeTo=" + hospitalSizeTo + "&optHospitalSizeTo=" + optHospitalSizeTo + "&countryID=" + countryID + "&stateID=" + stateID + "&value=" + value + "&others=" + others + "&location=" + location + "&hospitalUnitID=" + hospitalUnitID + "&hospitalID=" + RecordCounter + "&hospitalUnitName=" + DropDownListHospitalUnit.SelectedItem.Text + "&filter=" + filter + "&hospitalName=" + DropDownListHospitalName.SelectedItem.Text.Remove(0, 1) + "&beginingDate=" + beginingDate + "&endingDate=" + endingDate + "&DataPointsFrom=" + dataPointsFrom + "&optDataPointsFrom=" + optDataPointsFrom + "&DataPointsTo=" + dataPointsTo + "&optDataPointsTo=" + optDataPointsTo;
                            //    url = "ReportTimeRNGrid.aspx?Report=Dashboard&valueAddedCategoryID=" + valueAddedCategory + "&activitiesID=" + activitiesID + "&OthersCategoryID=" + othersCategory + "&LocationCategoryID=" + locationCategory + "&yearFrom=" + yearFrom + "&yearTo=" + yearTo + "&monthFrom=" + monthFrom + "&monthTo=" + monthTo + "&BedsInUnitFrom=" + bedsInUnitFrom + "&optBedsInUnitFrom=" + optBedsInUnitFrom + "&bedsInUnitTo=" + bedsInUnitTo + "&optBedsInUnitTo=" + optBedsInUnitTo + "&budgetedPatientFrom=" + budgetedPatientFrom + "&optBudgetedPatientFrom=" + optBudgetedPatientFrom + "&budgetedPatientTo=" + budgetedPatientTo + "&optBudgetedPatientTo=" + optBudgetedPatientTo + "&electronicDocumentationFrom=" + electronicDocumentationFrom + "&optElectronicDocumentationFrom=" + optElectronicDocumentationFrom + "&electronicDocumentationTo=" + electronicDocumentationTo + "&optElectronicDocumentationTo=" + optElectronicDocumentationTo + "&docByException=" + docByException + "&unitType=" + unitType + "&pharmacyType=" + pharmacyType + "&hospitalType=" + hospitalType + "&hospitalSizeFrom=" + hospitalSizeFrom + "&optHospitalSizeFrom=" + optHospitalSizeFrom + "&hospitalSizeTo=" + hospitalSizeTo + "&optHospitalSizeTo=" + optHospitalSizeTo + "&countryID=" + countryID + "&stateID=" + stateID + "&activities=" + activities + "&value=" + value + "&others=" + others + "&location=" + location + "&hospitalUnitID=" + hospitalUnitID + "&hospitalID=" + RecordCounter + "&hospitalUnitName=" + DropDownListHospitalUnit.SelectedItem.Text + "&filter=" + filter + "&hospitalName=" + DropDownListHospitalName.SelectedItem.Text.Remove(0, 1) + "&beginingDate=" + beginingDate + "&endingDate=" + endingDate + "&dataPointsFrom=" + dataPointsFrom + "&optDataPointsFrom=" + optDataPointsFrom + "&dataPointsTo=" + dataPointsTo + "&optDataPointsTo=" + optDataPointsTo + "&configName=" + configName;
                            //}
                            //else if (Request.QueryString["Report"] == "PieCharts")
                            //{
                            //    //st = "ReportTimeRNChartsPie.aspx?Report=PieCharts&valueAddedCategoryID=" + DropDownListProfileTypeValueAdded.SelectedValue + "&OthersCategoryID=" + DropDownListProfileTypeOthers.SelectedValue + "&LocationCategoryID=" + DropDownListProfileTypeLocation.SelectedValue + "&firstYear=" + DropDownListYear.SelectedValue + "&lastYear=" + DropDownListYearTo.SelectedValue + "&firstMonth=" + DropDownListMonthFrom.SelectedValue + "&lastMonth=" + DropDownListMonthTo.SelectedValue + "&bedInUnit=" + TextBoxBedsInUnit.Text + "&optBedInUnit=" + DropDownListBedsInUnitOperator.SelectedValue + "&budgetedPatient=" + TextBoxBudgetedPatient.Text + "&optBudgetedPatient=" + DropDownListBudgetedPatientOperator.SelectedValue + "&electronicDocument=" + TextBoxElectronicDocumentation.Text + "&optElectronicDocument=" + DropDownListElectronicDocumentationOperator.SelectedValue + "&docByException=" + DropDownListDocByException.SelectedValue + "&unitType=" + unitType + "&pharmacyType=" + pharmacyType + "&hospitalUnitID=" + DropDownListHospitalUnit.SelectedValue + "&startDate=" + TextBoxStartDate.Text + "&endDate=" + TextBoxEndDate.Text + "&optHospitalSize=" + DropDownListHospitalSizeOperator.SelectedValue + "&hospitalSize=" + TextBoxHospitalSize.Text;
                            //    //url = "ReportTimeRNChartsPie.aspx?Report=PieCharts";
                            //}
                            //else if (Request.QueryString["Report"] == "ControlCharts")
                            //{
                            //    url = "ReportTimeRNCharts.aspx?Report=ControlCharts&valueAddedCategoryID=" + valueAddedCategory + "&activitiesID=" + activitiesID + "&OthersCategoryID=" + othersCategory + "&LocationCategoryID=" + locationCategory + "&yearFrom=" + yearFrom + "&yearTo=" + yearTo + "&monthFrom=" + monthFrom + "&monthTo=" + monthTo + "&BedsInUnitFrom=" + bedsInUnitFrom + "&optBedsInUnitFrom=" + optBedsInUnitFrom + "&bedsInUnitTo=" + bedsInUnitTo + "&optBedsInUnitTo=" + optBedsInUnitTo + "&budgetedPatientFrom=" + budgetedPatientFrom + "&optBudgetedPatientFrom=" + optBudgetedPatientFrom + "&budgetedPatientTo=" + budgetedPatientTo + "&optBudgetedPatientTo=" + optBudgetedPatientTo + "&electronicDocumentationFrom=" + electronicDocumentationFrom + "&optElectronicDocumentationFrom=" + optElectronicDocumentationFrom + "&electronicDocumentationTo=" + electronicDocumentationTo + "&optElectronicDocumentationTo=" + optElectronicDocumentationTo + "&docByException=" + docByException + "&unitType=" + unitType + "&pharmacyType=" + pharmacyType + "&hospitalType=" + hospitalType + "&hospitalSizeFrom=" + hospitalSizeFrom + "&optHospitalSizeFrom=" + optHospitalSizeFrom + "&hospitalSizeTo=" + hospitalSizeTo + "&optHospitalSizeTo=" + optHospitalSizeTo + "&countryID=" + countryID + "&stateID=" + stateID + "&activities=" + activities + "&value=" + value + "&others=" + others + "&location=" + location + "&hospitalUnitID=" + hospitalUnitID;
                            //}
                            //Response.Redirect(url, false);

                            //----to open page in new window
                            //string fullUrl = "window.open('" + url + "','_blank','height=500,width=800,status=yes,toolbar=no,menubar=yes,location=no,scrollbars=no,resizable=yes,titlebar=no');";
                            ////ButtonGenerateReport.Attributes.Add("OnClick", fullUrl);
                            //ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", fullUrl, true);
                            //cm start
                            string startDate= null;
                            string endDate = null;
                            getChart(activitiesID, valueAddedCategory, othersCategory, locationCategory, hospitalUnitID, yearFrom, yearTo, monthFrom, monthTo, bedsInUnitFrom, optBedsInUnitFrom, bedsInUnitTo, optBedsInUnitTo, budgetedPatientFrom, optBudgetedPatientFrom, budgetedPatientTo, optBudgetedPatientTo, startDate, endDate, electronicDocumentationFrom, optElectronicDocumentationFrom, electronicDocumentationTo, optElectronicDocumentationTo, docByException, unitType, pharmacyType, hospitalType, hospitalSizeFrom, optHospitalSizeFrom, hospitalSizeTo, optHospitalSizeTo, countryID, stateID, activities, value, others, location, dataPointsFrom, optDataPointsFrom, dataPointsTo, optDataPointsTo, configName, unitIds);
                        }
                        else
                        {
                            CommonClass.Show("Profile Chosen Should Not Be More Than 10");
                        }
                    }
                    catch (Exception ex)
                    {
                        LogManager._stringObject = "DemographicDetail.aspx ---- Page_Load";
                        LogManager.SetExceptionDetails(ex, LogManager._stringObject, null);
                        LogManager.LogException(ex, LogManager.LoggingCategory.General, LogManager.LoggingLevel.Error);
                        CommonClass.Show(LogManager.ShowErrorDetail(ex));
                    }

                    finally
                    {
                        objectBenchmarkingFilter = null;
                        objectBSReports = null;
                    }
                }
            }
            catch (Exception ex)
            {

                LogManager._stringObject = "DemographicDetail.aspx ---- Page_Load";
                LogManager.SetExceptionDetails(ex, LogManager._stringObject, null);
                LogManager.LogException(ex, LogManager.LoggingCategory.General, LogManager.LoggingLevel.Error);
                CommonClass.Show(LogManager.ShowErrorDetail(ex));
            }  
        }

        private void getChart(string activitiesIDs, string valueAddedCategoryIDs, string OthersCategoryIDs, string LocationCategoryIDs, string hospitalUnitIDs, string yearFrom, string yearTo, string monthFrom, string monthTo, string BedsInUnitFrom, string optBedsInUnitFrom, string bedsInUnitTo, string optBedsInUnitTo, string budgetedPatientFrom1, string optBudgetedPatientFrom1, string budgetedPatientTo1, string optBudgetedPatientTo1, string startDate1, string endDate1, string electronicDocumentationFrom, string optElectronicDocumentationFrom, string electronicDocumentationTo, string optElectronicDocumentationTo, string docByException1, string unitType1, string pharmacyType1, string hospitalType1, string hospitalSizeFrom1, string optHospitalSizeFrom1, string hospitalSizeTo1, string optHospitalSizeTo1, string countryId1, string stateId1, string activities1, string value1, string others1, string location1, string dataPointsFrom1, string optDataPointsFrom1, string dataPointsTo1, string optDataPointsTo1, string configName1, string unitIds1)
        {
            RMC.BussinessService.BSReports objectBEReports = new BSReports();

            string activitiesID = activitiesIDs;
            if (activitiesID == string.Empty)
            {
                activitiesID = null;
            }
            string valueAddedCategoryID = valueAddedCategoryIDs;
            if (valueAddedCategoryID == string.Empty)
            {
                valueAddedCategoryID = null;
            }

            string OthersCategoryID = OthersCategoryIDs;
            if (OthersCategoryID == string.Empty)
            {
                OthersCategoryID = null;
            }

            string LocationCategoryID = LocationCategoryIDs;
            if (LocationCategoryID == string.Empty)
            {
                LocationCategoryID = null;
            }

            int? hospitalUnitId = null;
            if (hospitalUnitIDs != string.Empty)
            {
                hospitalUnitId = Convert.ToInt32(hospitalUnitIDs);
            }

            int? firstYear = null;
            if (yearFrom != string.Empty)
            {
                firstYear = Convert.ToInt32(yearFrom);
            }

            int? lastYear = null;
            if (yearTo != string.Empty)
            {
                lastYear = Convert.ToInt32(yearTo);
            }

            int? firstMonth = null;
            if (monthFrom != string.Empty)
            {
                firstMonth = Convert.ToInt32(monthFrom);
            }

            int? lastMonth = null;
            if (monthTo != string.Empty)
            {
                lastMonth = Convert.ToInt32(monthTo);
            }

            int? bedInUnitFrom = null;
            if (BedsInUnitFrom != null)
            {
                bedInUnitFrom = Convert.ToInt32(BedsInUnitFrom);
            }

            int optBedInUnitFrom = 0;
            if (optBedsInUnitFrom != "0")
            {
                optBedInUnitFrom = Convert.ToInt32(optBedsInUnitFrom);
            }

            int? bedInUnitTo = null;
            if (bedsInUnitTo != null)
            {
                bedInUnitTo = Convert.ToInt32(bedsInUnitTo);
            }

            int optBedInUnitTo = 0;
            if (optBedsInUnitTo != "0")
            {
                optBedInUnitTo = Convert.ToInt32(optBedsInUnitTo);
            }

            float? budgetedPatientFrom = null;
            if (budgetedPatientFrom1 != null)
            {
                budgetedPatientFrom = Convert.ToInt32(budgetedPatientFrom1);
            }

            int optBudgetedPatientFrom = 0;
            if (optBudgetedPatientFrom1 != "0")
            {
                optBudgetedPatientFrom = Convert.ToInt32(optBudgetedPatientFrom1);
            }

            float? budgetedPatientTo = null;
            if (budgetedPatientTo1 != null)
            {
                budgetedPatientTo = Convert.ToInt32(budgetedPatientTo1);
            }

            int optBudgetedPatientTo = 0;
            if (optBudgetedPatientTo1 != "0")
            {
                optBudgetedPatientTo = Convert.ToInt32(optBudgetedPatientTo1);
            }

            string startDate = null;
            string endDate = null;

            int? electronicDocumentFrom = null;
            if (electronicDocumentationFrom != null)
            {
                electronicDocumentFrom = Convert.ToInt32(electronicDocumentationFrom);
            }

            int optElectronicDocumentFrom = 0;
            if (optElectronicDocumentationFrom != "0")
            {
                optElectronicDocumentFrom = Convert.ToInt32(optElectronicDocumentationFrom);
            }

            int? electronicDocumentTo = null;
            if (electronicDocumentationTo != null)
            {
                electronicDocumentTo = Convert.ToInt32(electronicDocumentationTo);
            }

            int optElectronicDocumentTo = 0;
            if (optElectronicDocumentationTo != "0")
            {
                optElectronicDocumentTo = Convert.ToInt32(optElectronicDocumentationTo);
            }

            int docByException = 0;
            if (docByException1 != "0")
            {
                docByException = Convert.ToInt32(docByException1);
            }

            string unitType = null;
            if (unitType1 != null)
            {
                unitType = unitType1;
            }

            string pharmacyType = null;
            if (pharmacyType1 != null)
            {
                pharmacyType = pharmacyType1;
            }

            string hospitalType = null;
            if (hospitalType1 != null)
            {
                hospitalType = hospitalType1;
            }

            int? hospitalSizeFrom = null;
            if (hospitalSizeFrom1 != null)
            {
                hospitalSizeFrom = Convert.ToInt32(hospitalSizeFrom1);
            }

            int optHospitalSizeFrom = 0;
            if (optHospitalSizeFrom1 != "0")
            {
                optHospitalSizeFrom = Convert.ToInt32(optHospitalSizeFrom1);
            }

            int? hospitalSizeTo = null;
            if (hospitalSizeTo1 != null)
            {
                hospitalSizeTo = Convert.ToInt32(hospitalSizeTo1);
            }

            int optHospitalSizeTo = 0;
            if (optHospitalSizeTo1 != "0")
            {
                optHospitalSizeTo = Convert.ToInt32(optHospitalSizeTo1);
            }

            int? countryId = null;
            if (countryId1 != "0")
            {
                countryId = Convert.ToInt32(countryId1);
            }

            int? stateId = null;
            if (stateId1 != "0")
            {
                stateId = Convert.ToInt32(stateId1);
            }

            string activities = string.Empty;
            if (activities1 != string.Empty)
            {
                activities = activities1;
            }


            string value = string.Empty;
            if (value1 != string.Empty)
            {
                value = value1;
            }

            string others = string.Empty;
            if (others1 != string.Empty)
            {
                others = others1;
            }

            string location = string.Empty;
            if (location1 != string.Empty)
            {
                location = location1;
            }

            int? dataPointsFrom = null;
            if (dataPointsFrom1 != null)
            {
                dataPointsFrom = Convert.ToInt32(dataPointsFrom1);
            }
            int optDataPointsFrom = 0;
            if (optDataPointsFrom1 != "0")
            {
                optDataPointsFrom = Convert.ToInt32(optDataPointsFrom1);
            }
            int? dataPointsTo = null;
            if (dataPointsTo1 != null)
            {
                dataPointsTo = Convert.ToInt32(dataPointsTo1);
            }
            int optDataPointsTo = 0;
            if (optDataPointsTo1 != "0")
            {
                optDataPointsTo = Convert.ToInt32(optDataPointsTo1);
            }

            string configName = null;
            if (configName1 != null)
            {
                configName = configName1;
            }
            string UnitIds = null;
            if (unitIds1 != null)
            {
                UnitIds = unitIds1;
            }

            

            DataTable DTGetDataForTimeRNSummaryGrid = new DataTable();
            DTGetDataForTimeRNSummaryGrid = objectBEReports.GetDataForTimeRNSummaryGrid(activitiesID, valueAddedCategoryID, OthersCategoryID, LocationCategoryID, hospitalUnitId, firstYear, lastYear, firstMonth, lastMonth, null, 0, null, 0, null, 0, null, 0, startDate, endDate, null, 0, null, 0, 0, null, null, null, 0, null, 0, null, 0, 0, activities, value, others, location, null, 0, null, 0, null);
            GridViewReport.DataSource = DTGetDataForTimeRNSummaryGrid;
            GridViewReport.DataBind();

            DataTable DTCalculateFunctionValuesGrid = new DataTable();
            DTCalculateFunctionValuesGrid = objectBEReports.CalculateFunctionValuesGrid(activitiesID, valueAddedCategoryID, OthersCategoryID, LocationCategoryID, null, firstYear, lastYear, firstMonth, lastMonth, bedInUnitFrom, optBedInUnitFrom, bedInUnitTo, optBedInUnitTo, budgetedPatientFrom, optBudgetedPatientFrom, budgetedPatientTo, optBudgetedPatientTo, startDate, endDate, electronicDocumentFrom, optElectronicDocumentFrom, electronicDocumentTo, optElectronicDocumentTo, docByException, unitType, pharmacyType, hospitalType, optHospitalSizeFrom, hospitalSizeFrom, optHospitalSizeTo, hospitalSizeTo, countryId, stateId, activities, value, others, location, dataPointsFrom, optDataPointsFrom, dataPointsTo, optDataPointsTo, configName, UnitIds);
            GridViewFunctionReport.DataSource = DTCalculateFunctionValuesGrid;
            GridViewFunctionReport.DataBind();

            //List<RMC.BusinessEntities.BEFunctionNames> objectBEFunctionNames = objectBEReports.GetPerformanceTest(valueAddedCategoryID, OthersCategoryID, LocationCategoryID, hospitalUnitId, firstYear, lastYear, firstMonth, lastMonth, bedInUnitFrom, optBedInUnitFrom, bedInUnitTo, optBedInUnitTo, budgetedPatientFrom, optBudgetedPatientFrom, budgetedPatientTo, optBudgetedPatientTo, startDate, endDate, electronicDocumentFrom, optElectronicDocumentFrom, electronicDocumentTo, optElectronicDocumentTo, docByException, unitType, pharmacyType, hospitalType, optHospitalSizeFrom, hospitalSizeFrom, optHospitalSizeTo, hospitalSizeTo, countryId, stateId, value, others, location, null, 0, null, 0, configName, "MonthlySummaryDashboard");
            List<RMC.BusinessEntities.BEFunctionNames> objectBEFunctionNames = objectBEReports.GetPerformanceTest(activitiesID, valueAddedCategoryID, OthersCategoryID, LocationCategoryID, hospitalUnitId, firstYear, lastYear, firstMonth, lastMonth, bedInUnitFrom, optBedInUnitFrom, bedInUnitTo, optBedInUnitTo, budgetedPatientFrom, optBudgetedPatientFrom, budgetedPatientTo, optBudgetedPatientTo, startDate, endDate, electronicDocumentFrom, optElectronicDocumentFrom, electronicDocumentTo, optElectronicDocumentTo, docByException, unitType, pharmacyType, hospitalType, optHospitalSizeFrom, hospitalSizeFrom, optHospitalSizeTo, hospitalSizeTo, countryId, stateId, activities, value, others, location, dataPointsFrom, optDataPointsFrom, dataPointsTo, optDataPointsTo, configName, UnitIds, "MonthlySummaryDashboard");
            RMC.BussinessService.BSChartControl objectBSChartControl = new RMC.BussinessService.BSChartControl();
            Series objectSeries = objectBSChartControl.GetColumnChartSeries(null, objectBEFunctionNames);

            ChartTimeRN.Series.Add(objectSeries);
            //ChartTimeRN.Series["Series1"]["PointWidth"] = "0.3";
            int count = objectBEFunctionNames.Count();
            ChartTimeRN.Width = 450 + (count * 60);
            ChartTimeRN.ChartAreas["ChartArea1"].AxisY.Title = "Positive is Better than National Average";
            //save image of chart
            if (System.IO.File.Exists("~\\Uploads\\ChartImg" + CommonClass.UserInformation.UserID.ToString() + ".png"))
            {
                System.IO.File.Delete("~\\Uploads\\ChartImg" + CommonClass.UserInformation.UserID.ToString() + ".png");
            }
            ChartTimeRN.SaveImage(Server.MapPath("~\\Uploads\\ChartImg" + CommonClass.UserInformation.UserID.ToString() + ".png"));
        }

        /// <summary>
        /// Resets all controls to its default value
        /// </summary>
        protected void ButtonReset_Click(object sender, EventArgs e)
        {
            try
            {
                Reset();
            }
            catch (Exception ex)
            {
                ex.Data.Add("Page", "DemographicDetail.aspx");
                LogManager._stringObject = "DemographicDetail.aspx ---- Page_Load";
                LogManager.SetExceptionDetails(ex, LogManager._stringObject, null);
                LogManager.LogException(ex, LogManager.LoggingCategory.General, LogManager.LoggingLevel.Error);
                CommonClass.Show(LogManager.ShowErrorDetail(ex));
            }
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Use to fetch all hospital names.
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
                    CommonClass.Show("Hospital Names does not exist.");
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

        /// <summary>
        /// Gets All available Profiles and fill in AvailableProfile ListBox
        /// </summary>
        /// <param name="userID">Login User</param>
        private void GetAllAvailableProfiles(int userID)
        {
            RMC.BussinessService.BSProfileType objectBSProfileType = new RMC.BussinessService.BSProfileType();
            //Generic List Of Data Service objects.
            List<RMC.DataService.ProfileType> objectGenericProfileType = null;
            try
            {
                objectGenericProfileType = objectBSProfileType.GetProfileTypes(userID);
                objectGenericProfileType = objectGenericProfileType.OrderBy(o => o.ProfileName).ToList();
                if (objectGenericProfileType.Count > 0)
                {
                    if (ListBoxAvailableProfiles.Items.Count > 0)
                    {
                        ListBoxAvailableProfiles.Items.Clear();
                    }

                    foreach (RMC.DataService.ProfileType pt in objectGenericProfileType)
                    {
                        ListBoxAvailableProfiles.Items.Add(new ListItem(pt.ProfileName, pt.ProfileTypeID + "," + pt.Type));
                        //ListBoxAvailableProfiles.Items.Add(new ListItem(pt.ProfileTypeID + "," + pt.Type, pt.ProfileName));
                    }

                    //ListBoxAvailableProfiles.DataValueField = "Type";
                    //ListBoxAvailableProfiles.DataTextField = "ProfileName";
                    //ListBoxAvailableProfiles.DataSource = objectGenericProfileType.OrderBy(o => o.ProfileName).ToList();
                    //ListBoxAvailableProfiles.DataBind();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                objectBSProfileType = null;
                objectGenericProfileType = null;
            }
        }

        /// <summary>
        /// Gets year month whose data is available in database
        /// </summary>
        private void GetYearMonth()
        {
            RMC.BussinessService.BSReports objectBSReports = new RMC.BussinessService.BSReports();
            List<RMC.BusinessEntities.BEReportsYearMonth> objectGenericBEReportsYearMonth = new List<RMC.BusinessEntities.BEReportsYearMonth>();

            try
            {
                objectGenericBEReportsYearMonth = objectBSReports.GetYearMonthComboByUnitId(Convert.ToInt32(DropDownListHospitalUnit.SelectedValue));
                //objectGenericBEReportsYearMonth = objectBSReports.GetYearMonthCombo();
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
                    DropDownListYearMonthFrom.DataBind();
                    DropDownListYearMonthTo.DataBind();

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        /// <summary>
        /// Private method which resets all controls to its default value
        /// </summary>
        private void Reset()
        {
            DropDownListHospitalName.SelectedIndex = 0;
            DropDownListHospitalUnit.SelectedIndex = 0;
            DropDownListBenchmarkingFilter.SelectedIndex = 0;
            ListBoxSelectedProfiles.Items.Clear();
            int userID = CommonClass.UserInformation.UserID;
            GetAllAvailableProfiles(userID);
            DropDownListYearMonthFrom.SelectedIndex = 0;
            DropDownListYearMonthTo.SelectedIndex = 0;
        }

        /// <summary>
        /// Sets Null if Blank or non Selected Values
        /// Created By : 
        /// Creation Date : 
        /// Modified By : 
        /// Modified Date :
        /// </summary>
        /// <param name="objectGenericHospitalInfo"></param>
        private void SetNullIfBlank()
        {
            //if (DropDownListHospitalName.SelectedValue == "0")
            //{
            //    DropDownListHospitalName.SelectedValue = null;
            //}
            //if (DropDownListHospitalUnit.SelectedValue == "0")
            //{
            //    DropDownListHospitalUnit.SelectedValue = null;
            //}
            //if (DropDownListProfileTypeValueAdded.SelectedValue == "0")
            //{
            //    DropDownListProfileTypeValueAdded.SelectedValue = null;
            //}
            //if (DropDownListProfileTypeOthers.SelectedValue == "0")
            //{
            //    DropDownListProfileTypeOthers.SelectedValue = null;
            //}
            //if (DropDownListProfileTypeLocation.SelectedValue == "0")
            //{
            //    DropDownListProfileTypeLocation.SelectedValue = null;
            //}
            //if (DropDownListYear.SelectedValue == "0")
            //{
            //    DropDownListYear.SelectedValue = null;
            //}
            //if (DropDownListMonthFrom.SelectedValue == "0")
            //{
            //    DropDownListMonthFrom.SelectedValue = null;
            //}
            //if (DropDownListMonthTo.SelectedValue == "0")
            //{
            //    DropDownListMonthTo.SelectedValue = null;
            //}
            //if (TextBoxBedsInUnit.Text == string.Empty)
            //{
            //    //TextBoxBedsInUnit.Text = null;
            //    TextBoxBedsInUnit.Text = "0";
            //}
            //if (TextBoxBudgetedPatient.Text == string.Empty)
            //{
            //    //TextBoxBudgetedPatient.Text = null;
            //    TextBoxBudgetedPatient.Text = "0";
            //}
            //if (TextBoxStartDate.Text == string.Empty)
            //{
            //    TextBoxStartDate.Text = null;

            //}
            //if (TextBoxEndDate.Text == string.Empty)
            //{
            //    TextBoxEndDate.Text = null;

            //}
            //if (TextBoxElectronicDocumentation.Text == string.Empty)
            //{
            //    //TextBoxElectronicDocumentation.Text = null;
            //    TextBoxElectronicDocumentation.Text = "0";
            //}
            ////if (CheckBoxDocByException.Checked == false)
            ////{
            ////    CheckBoxDocByException.Text = "0";
            ////}
            //if (unitType == "")
            //{
            //    unitType = null;
            //}
            //if (pharmacyType == "")
            //{
            //    pharmacyType = null;
            //}
            //if (hospitalType == "")
            //{
            //    hospitalType = null;
            //}
            //if (TextBoxHospitalSize.Text == string.Empty)
            //{
            //    //TextBoxHospitalSize.Text = null;
            //    TextBoxHospitalSize.Text = "0";
            //}
            //if (DropDownListCountry.SelectedValue == "0")
            //{
            //    DropDownListCountry.SelectedValue = null;

            //}
            //if (DropDownListState.SelectedValue == "0")
            //{
            //    DropDownListState.SelectedValue = null;
            //}
        }
        //cm start
        protected void LinkButtonExportReport_Click(object sender, EventArgs e)
        {
            try
            {
                string filterName = DropDownListBenchmarkingFilter.SelectedItem.Text;
                string fileName = "Attachment; FileName = MontlySummaryReport(";
                Response.Clear();
                if (filterName.Contains(">"))
                {
                    filterName = filterName.Replace(">", "gt");
                }
                if (filterName.Contains("<"))
                {
                    filterName = filterName.Replace("<", "lt");
                }
                fileName = fileName + filterName + "," + DropDownListYearMonthFrom.SelectedItem.Text.Replace(" ", "") + "-" + DropDownListYearMonthTo.SelectedItem.Text.Replace(" ", "") + ")" + ".xls";

                //fileName = fileName + ".xls";

                Response.AddHeader("Content-Disposition", fileName);
                Response.Charset = "";
                Response.Cache.SetCacheability(HttpCacheability.NoCache);
                Response.ContentType = "Application/ms-excel";
                System.IO.StringWriter stringWrite = new System.IO.StringWriter();
                System.Web.UI.HtmlTextWriter htmlWrite = new HtmlTextWriter(stringWrite);

               

                //GridViewReport.RenderControl(htmlWrite);
                //GridViewFunctionReport.RenderControl(htmlWrite);                
                //Response.Write(stringWrite.ToString());
                //Response.End();
                HtmlForm frm = new HtmlForm();

                GridViewReport.Parent.Controls.Add(frm);
                GridViewFunctionReport.Parent.Controls.Add(frm);
                frm.Attributes["runat"] = "server";
                frm.Controls.Add(GridViewReport);
                frm.Controls.Add(GridViewFunctionReport);
                frm.RenderControl(htmlWrite);
                Response.Write(stringWrite.ToString());
                Response.End();
            }
            catch (Exception ex)
            {
                LogManager._stringObject = "ReportTimeRNGrid.ascx ----";
                LogManager.SetExceptionDetails(ex, LogManager._stringObject, null);
                LogManager.LogException(ex, LogManager.LoggingCategory.General, LogManager.LoggingLevel.Error);
                CommonClass.Show(LogManager.ShowErrorDetail(ex));
            }
        }

        protected void GridViewReport_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    //DataRowView drv = (DataRowView)e.Row.DataItem;

                    //for (int i = 1; i < drv.DataView.Table.Columns.Count; i++)
                    //{
                    //    e.Row.Cells[i].BackColor = System.Drawing.Color.Green;
                    //}
                    //e.Row.Cells[0].BackColor = System.Drawing.ColorTranslator.FromHtml("#80ffff");
                    e.Row.Cells[0].BackColor = System.Drawing.ColorTranslator.FromHtml("#06569d");
                    e.Row.Cells[0].ForeColor = System.Drawing.Color.White;
                    e.Row.Cells[0].Font.Bold = true;
                }
                if (e.Row.RowType == DataControlRowType.Header)
                {
                    //e.Row.Cells[0].Width = 100;
                    //e.Row.Cells[1].Width = 40;
                }
            }
            catch (Exception ex)
            {
                LogManager._stringObject = "ReportTimeRNGrid.ascx ---- Page_Load";
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
                    //DataRowView drv = (DataRowView)e.Row.DataItem;

                    //for (int i = 1; i < drv.DataView.Table.Columns.Count; i++)
                    //{
                    //    e.Row.Cells[i].BackColor = System.Drawing.Color.Green;
                    //}
                    //e.Row.Cells[0].BackColor = System.Drawing.ColorTranslator.FromHtml("#80ffff");
                    e.Row.Cells[0].BackColor = System.Drawing.ColorTranslator.FromHtml("#06569d");
                    e.Row.Cells[0].ForeColor = System.Drawing.Color.White;
                    e.Row.Cells[0].Font.Bold = true;
                    e.Row.Cells[0].ColumnSpan = 2;
                    //if (e.Row.Cells.Count >= 21)
                    //{
                    //    e.Row.Cells[0].Width = 444;
                    //}
                    //else if (e.Row.Cells.Count >= 13 && e.Row.Cells.Count < 21)
                    if (e.Row.Cells.Count >= 13)
                    {
                        e.Row.Cells[0].Width = 344;
                    }
                    else if (e.Row.Cells.Count > 5 && e.Row.Cells.Count < 13)
                    {
                        e.Row.Cells[0].Width = 264;
                    }
                    else
                    { e.Row.Cells[0].Width = 160; }
                }
                if (e.Row.RowType == DataControlRowType.Header)
                {
                    ////e.Row.Cells[1].Text = "";
                    e.Row.Cells[0].ColumnSpan = 2;

                    //if (e.Row.Cells.Count >= 21)
                    //{
                    //    e.Row.Cells[0].Width = 444;
                    //}
                    //else if (e.Row.Cells.Count >= 13 && e.Row.Cells.Count < 21)
                    if (e.Row.Cells.Count >= 13)
                    {
                        e.Row.Cells[0].Width = 344;
                    }
                    else if (e.Row.Cells.Count > 5 && e.Row.Cells.Count < 13)
                    {
                        e.Row.Cells[0].Width = 264;
                    }
                    else
                    { e.Row.Cells[0].Width = 160; }
                }
            }
            catch (Exception ex)
            {
                LogManager._stringObject = "ReportTimeRNGrid.ascx ---- Page_Load";
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
                LogManager._stringObject = "ReportTimeRNGrid.ascx ----";
                LogManager.SetExceptionDetails(ex, LogManager._stringObject, null);
                LogManager.LogException(ex, LogManager.LoggingCategory.General, LogManager.LoggingLevel.Error);
                CommonClass.Show(LogManager.ShowErrorDetail(ex));
            }
        }
        #endregion

        protected void DropDownListHospitalUnit_SelectedIndexChanged(object sender, EventArgs e)
        {
            GetYearMonth();
        }
    }
}