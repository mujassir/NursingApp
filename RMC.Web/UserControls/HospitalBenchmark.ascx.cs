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

namespace RMC.Web.UserControls
{
    public partial class RMCHospitalBenchmark : System.Web.UI.UserControl
    {
      

        #region Variables

        //Bussiness Service Objects. 
        RMC.DataService.BenchmarkFilter objectBenchmarkFilter = null;
        RMC.BussinessService.BSReports objectBSReports = null;

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
        string valueAddedCategory = "", activitiesID = "", othersCategory = "", locationCategory = "";

        //Intial view of collaboration report
        CollaborationReportView reportSummaryType;

        #endregion

        #region Events

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!Page.IsPostBack)
                {
                    if (!(HttpContext.Current.User.IsInRole("superadmin")))
                    {

                        if (!CommonClass.UserInformation.IsActive)
                        {
                            Response.Redirect("~/Users/InActiveUser.aspx", false);
                        }
                    }

                    RadioButtonSummarizeAllData.Checked = true;
                    reportSummaryType = CollaborationReportView.SummarizeAllData;
                    Session["CollaborationReportView"] = reportSummaryType;

                    GetAllAvailableProfiles(CommonClass.UserInformation.UserID);
                    GetYearMonth();

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
                    else
                    {
                        string filter = Request.QueryString["filter"];
                        string beginingDate = Request.QueryString["beginingDate"];
                        string endingDate = Request.QueryString["endingDate"];
                        string value = Request.QueryString["value"];
                        string others = Request.QueryString["others"];
                        string location = Request.QueryString["location"];
                        string activities = Request.QueryString["activities"];

                        string[] strArrvalue = null, strArrothers = null, strArrlocation = null, strActivities = null;
                        strArrvalue = value.Split(new char[] { ',' });
                        strArrothers = others.Split(new char[] { ',' });
                        strArrlocation = location.Split(new char[] { ',' });
                        strActivities = activities.Split(new char[] { ',' });
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
                                for (int i = 0; i < strActivities.Length; i++)
                                {
                                    string activitiesText = ListBoxAvailableProfiles.Items.FindByText(strActivities[i]).Value;
                                    ListBoxAvailableProfiles.Items.Remove(new ListItem(strActivities[i], activitiesText));
                                    ListBoxSelectedProfiles.Items.Add(new ListItem(strActivities[i], activitiesText));
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
                    }
                }
                else // if the page has been posted back
                {

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

        /// <summary>
        /// Calculate data and generates report on the basis of that data
        /// </summary>
        //protected void ButtonGenerateReport_Click(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        LinkButtonExportReport.Visible = true;

        //        int? yearFrom = null, yearTo = null, monthFrom = null, monthTo = null;
        //        if (DropDownListYearMonthFrom.SelectedValue != "0")
        //        {
        //            string yearMonthFrom = DropDownListYearMonthFrom.SelectedValue;
        //            string[] yearMonthFromArr = yearMonthFrom.Split(new char[] { ',' });
        //            yearFrom = int.Parse(yearMonthFromArr[1].ToString());
        //            monthFrom = int.Parse(yearMonthFromArr[0].ToString());
        //        }
        //        if (DropDownListYearMonthTo.SelectedValue != "0")
        //        {
        //            string yearMonthTo = DropDownListYearMonthTo.SelectedValue;
        //            string[] yearMonthToArr = yearMonthTo.Split(new char[] { ',' });
        //            yearTo = int.Parse(yearMonthToArr[1].ToString());
        //            monthTo = int.Parse(yearMonthToArr[0].ToString());
        //        }

        //        if (ListBoxSelectedProfiles.Items.Count == 0 || Convert.ToInt32(yearFrom) > Convert.ToInt32(yearTo) || (Convert.ToInt32(yearFrom) == Convert.ToInt32(yearTo) && Convert.ToInt32(monthFrom) > Convert.ToInt32(monthTo)))
        //        {
        //            if (ListBoxSelectedProfiles.Items.Count == 0)
        //            {
        //                CommonClass.Show("Atleast One Profile Must Be Chosen");
        //            }
        //            if (Convert.ToInt32(yearFrom) > Convert.ToInt32(yearTo) || (Convert.ToInt32(yearFrom) == Convert.ToInt32(yearTo) && Convert.ToInt32(monthFrom) > Convert.ToInt32(monthTo)))
        //            {
        //                CommonClass.Show("PeriodTo Cannot Be Less Than PeriodFrom");
        //            }
        //        }
        //        else
        //        {
        //            RMC.DataService.BenchmarkFilter objectBenchmarkingFilter = new RMC.DataService.BenchmarkFilter();
        //            RMC.BussinessService.BSReports objectBSReports = new RMC.BussinessService.BSReports();
        //            try
        //            {
        //                if (ListBoxSelectedProfiles.Items.Count < 11)
        //                {
        //                    string unitType, pharmacyType, hospitalType, configName;
        //                    float? budgetedPatientFrom, budgetedPatientTo;
        //                    int? dataPointsFrom,bedsInUnitTo, bedsInUnitFrom, electronicDocumentationFrom, electronicDocumentationTo, hospitalSizeFrom, hospitalSizeTo, countryID, stateID,  dataPointsTo;
        //                    int optBedsInUnitFrom, optBedsInUnitTo, optBudgetedPatientFrom, optBudgetedPatientTo, optElectronicDocumentationFrom, optElectronicDocumentationTo, docByException, optHospitalSizeFrom, optHospitalSizeTo, optDataPointsTo, optDataPointsFrom;
        //                    if (DropDownListBenchmarkingFilter.SelectedIndex == 1)
        //                    {
        //                        bedsInUnitFrom = null;
        //                        optBedsInUnitFrom = 0;
        //                        bedsInUnitTo = null;
        //                        optBedsInUnitTo = 0;
        //                        budgetedPatientFrom = null;
        //                        optBudgetedPatientFrom = 0;
        //                        budgetedPatientTo = null;
        //                        optBudgetedPatientTo = 0;
        //                        electronicDocumentationFrom = null;
        //                        optElectronicDocumentationFrom = 0;
        //                        electronicDocumentationTo = null;
        //                        optElectronicDocumentationTo = 0;
        //                        docByException = 0;
        //                        unitType = null;
        //                        pharmacyType = null;
        //                        hospitalType = null;
        //                        configName = null;
        //                        hospitalSizeFrom = null;
        //                        optHospitalSizeFrom = 0;
        //                        hospitalSizeTo = null;
        //                        optHospitalSizeTo = 0;
        //                        countryID = null;
        //                        stateID = null;
        //                        dataPointsFrom = null;
        //                        optDataPointsFrom = 0;
        //                        dataPointsTo = null;
        //                        optDataPointsTo = 0;
        //                    }
        //                    else
        //                    {
        //                        objectBenchmarkingFilter = objectBSReports.GetBenchmarkFilterRow(Convert.ToInt32(DropDownListBenchmarkingFilter.SelectedValue), DropDownListBenchmarkingFilter.SelectedItem.Text);
        //                        bedsInUnitFrom = int.Parse(objectBenchmarkingFilter.BedsInUnitFrom.ToString());
        //                        if (bedsInUnitFrom == 0)
        //                        {
        //                            bedsInUnitFrom = null;
        //                        }
        //                        optBedsInUnitFrom = int.Parse( objectBenchmarkingFilter.optBedsInUnitFrom.ToString());
        //                        bedsInUnitTo = int.Parse(objectBenchmarkingFilter.BedsInUnitTo.ToString());
        //                        if (bedsInUnitTo == 0)
        //                        {
        //                            bedsInUnitTo = null;
        //                        }
        //                        optBedsInUnitTo = int.Parse(objectBenchmarkingFilter.optBedsInUnitTo.ToString());

        //                        budgetedPatientFrom = float.Parse(objectBenchmarkingFilter.BudgetedPatientFrom.ToString());
        //                        if (budgetedPatientFrom == 0)
        //                        {
        //                            budgetedPatientFrom = null;
        //                        }
        //                        optBudgetedPatientFrom = int.Parse(objectBenchmarkingFilter.optBudgetedPatientFrom.ToString());
        //                        budgetedPatientTo = float.Parse(objectBenchmarkingFilter.BudgetedPatientTo.ToString());
        //                        if (budgetedPatientTo == 0)
        //                        {
        //                            budgetedPatientTo = null;
        //                        }
        //                        optBudgetedPatientTo = int.Parse(objectBenchmarkingFilter.optBudgetedPatientTo.ToString());

        //                        electronicDocumentationFrom = int.Parse(objectBenchmarkingFilter.ElectronicDocumentationFrom.ToString());
        //                        if (electronicDocumentationFrom == 0)
        //                        {
        //                            electronicDocumentationFrom = null;
        //                        }
        //                        optElectronicDocumentationFrom = int.Parse(objectBenchmarkingFilter.optElectronicDocumentationFrom.ToString());
        //                        electronicDocumentationTo = int.Parse(objectBenchmarkingFilter.ElectronicDocumentationTo.ToString());
        //                        if (electronicDocumentationTo == 0)
        //                        {
        //                            electronicDocumentationTo = null;
        //                        }
        //                        optElectronicDocumentationTo = int.Parse(objectBenchmarkingFilter.optElectronicDocumentationTo.ToString());

        //                        docByException = int.Parse(objectBenchmarkingFilter.DocByException.ToString());

        //                        unitType = objectBenchmarkingFilter.UnitType.ToString();
        //                        if (unitType == "0")
        //                        {
        //                            unitType = null;
        //                        }

        //                        pharmacyType = objectBenchmarkingFilter.PharmacyType.ToString();
        //                        if (pharmacyType == "0")
        //                        {
        //                            pharmacyType = null;
        //                        }

        //                        hospitalType = objectBenchmarkingFilter.HospitalType.ToString();
        //                        if (hospitalType == "0")
        //                        {
        //                            hospitalType = null;
        //                        }

        //                        hospitalSizeFrom = int.Parse(objectBenchmarkingFilter.HospitalSizeFrom.ToString());
        //                        if (hospitalSizeFrom == 0)
        //                        {
        //                            hospitalSizeFrom = null;
        //                        }
        //                        optHospitalSizeFrom = int.Parse(objectBenchmarkingFilter.optHospitalSizeFrom.ToString());
        //                        hospitalSizeTo = int.Parse(objectBenchmarkingFilter.HospitalSizeTo.ToString());
        //                        if (hospitalSizeTo == 0)
        //                        {
        //                            hospitalSizeTo = null;
        //                        }
        //                        optHospitalSizeTo = int.Parse(objectBenchmarkingFilter.optHospitalSizeTo.ToString());

        //                        countryID = int.Parse(objectBenchmarkingFilter.CountryId.ToString());
        //                        stateID = int.Parse(objectBenchmarkingFilter.StateId.ToString());

        //                        dataPointsFrom = int.Parse(objectBenchmarkingFilter.DataPointsFrom.ToString());
        //                        if (dataPointsFrom == 0)
        //                        {
        //                            dataPointsFrom = null;
        //                        }
        //                        optDataPointsFrom = int.Parse(objectBenchmarkingFilter.optDataPointsFrom.ToString());
        //                        dataPointsTo = int.Parse(objectBenchmarkingFilter.DataPointsTo.ToString());
        //                        if (dataPointsTo == 0)
        //                        {
        //                            dataPointsTo = null;
        //                        }
        //                        optDataPointsTo = int.Parse(objectBenchmarkingFilter.optDataPointsTo.ToString());

        //                        configName = objectBenchmarkingFilter.ConfigurationName.ToString();
        //                        if (configName == "0")
        //                        {
        //                            configName = null;
        //                        }
        //                    }

        //                    string value = "", others = "", location = "", activities= "";
        //                    if (ListBoxSelectedProfiles.Items.Count > 0)
        //                    {
        //                        for (int Index = 0; Index < ListBoxSelectedProfiles.Items.Count; Index++)
        //                        {
        //                            if (ListBoxSelectedProfiles.Items[Index].Value.Contains("Activities"))
        //                            {
        //                                activitiesID += ListBoxSelectedProfiles.Items[Index].Value.Replace(",Activities", "") + ",";
        //                                activities += ListBoxSelectedProfiles.Items[Index].Text + ",";
        //                            }
        //                            if (ListBoxSelectedProfiles.Items[Index].Value.Contains("Value Added"))
        //                            {
        //                                valueAddedCategory += ListBoxSelectedProfiles.Items[Index].Value.Replace(",Value Added", "") + ",";
        //                                value += ListBoxSelectedProfiles.Items[Index].Text + ",";
        //                            }
        //                            if (ListBoxSelectedProfiles.Items[Index].Value.Contains("Others"))
        //                            {
        //                                othersCategory += ListBoxSelectedProfiles.Items[Index].Value.Replace(",Others", "") + ",";
        //                                others += ListBoxSelectedProfiles.Items[Index].Text + ",";
        //                            }
        //                            if (ListBoxSelectedProfiles.Items[Index].Value.Contains("Location"))
        //                            {
        //                                locationCategory += ListBoxSelectedProfiles.Items[Index].Value.Replace(",Location", "") + ",";
        //                                location += ListBoxSelectedProfiles.Items[Index].Text + ",";
        //                            }
        //                            if (Index == (ListBoxSelectedProfiles.Items.Count - 1) && activitiesID != null)
        //                            {
        //                                activitiesID = activitiesID.Remove(activitiesID.Length - 1, 1);
        //                                activities = activities.Remove(activities.Length - 1, 1);
        //                            }
        //                            if (Index == (ListBoxSelectedProfiles.Items.Count - 1) && valueAddedCategory != null)
        //                            {
        //                                valueAddedCategory = valueAddedCategory.Remove(valueAddedCategory.Length - 1, 1);
        //                                value = value.Remove(value.Length - 1, 1);
        //                            }
        //                            if (Index == (ListBoxSelectedProfiles.Items.Count - 1) && othersCategory != null)
        //                            {
        //                                othersCategory = othersCategory.Remove(othersCategory.Length - 1, 1);
        //                                others = others.Remove(others.Length - 1, 1);
        //                            }
        //                            if (Index == (ListBoxSelectedProfiles.Items.Count - 1) && locationCategory != null)
        //                            {
        //                                locationCategory = locationCategory.Remove(locationCategory.Length - 1, 1);
        //                                location = location.Remove(location.Length - 1, 1);
        //                            }
        //                        }
        //                    }
                           
        //                    string filter = DropDownListBenchmarkingFilter.SelectedItem.Text;
        //                    if (DropDownListBenchmarkingFilter.SelectedIndex == 0)
        //                    {
        //                        filter = "No Filter";
        //                    }

        //                    string beginingDate, endingDate;
        //                    if (DropDownListYearMonthFrom.SelectedIndex != 0)
        //                    {
        //                        beginingDate = DropDownListYearMonthFrom.SelectedItem.Text;
        //                        endingDate = DropDownListYearMonthTo.SelectedItem.Text;
        //                    }
        //                    else
        //                    {
        //                        beginingDate = DropDownListYearMonthFrom.Items[1].Text;
        //                        int count = DropDownListYearMonthTo.Items.Count;
        //                        endingDate = DropDownListYearMonthTo.Items[count - 1].Text;
        //                    }


        //                    //string url = "ReportHospitalBenchmark.aspx?valueAddedCategoryID=" + valueAddedCategory + "&OthersCategoryID=" + othersCategory + "&LocationCategoryID=" + locationCategory + "&yearFrom=" + yearFrom + "&yearTo=" + yearTo + "&monthFrom=" + monthFrom + "&monthTo=" + monthTo + "&BedsInUnitFrom=" + bedsInUnitFrom + "&optBedsInUnitFrom=" + optBedsInUnitFrom + "&bedsInUnitTo=" + bedsInUnitTo + "&optBedsInUnitTo=" + optBedsInUnitTo + "&budgetedPatientFrom=" + budgetedPatientFrom + "&optBudgetedPatientFrom=" + optBudgetedPatientFrom + "&budgetedPatientTo=" + budgetedPatientTo + "&optBudgetedPatientTo=" + optBudgetedPatientTo + "&electronicDocumentationFrom=" + electronicDocumentationFrom + "&optElectronicDocumentationFrom=" + optElectronicDocumentationFrom + "&electronicDocumentationTo=" + electronicDocumentationTo + "&optElectronicDocumentationTo=" + optElectronicDocumentationTo + "&docByException=" + docByException + "&unitType=" + unitType + "&pharmacyType=" + pharmacyType + "&hospitalType=" + hospitalType + "&hospitalSizeFrom=" + hospitalSizeFrom + "&optHospitalSizeFrom=" + optHospitalSizeFrom + "&hospitalSizeTo=" + hospitalSizeTo + "&optHospitalSizeTo=" + optHospitalSizeTo + "&countryID=" + countryID + "&stateID=" + stateID + "&value=" + value + "&others=" + others + "&location=" + location + "&filter=" + filter + "&beginingDate=" + beginingDate + "&endingDate=" + endingDate + "&DataPointsFrom=" + dataPointsFrom + "&optDataPointsFrom=" + optDataPointsFrom + "&DataPointsTo=" + dataPointsTo + "&optDataPointsTo=" + optDataPointsTo;
        //                    //string url = "ReportHospitalBenchmarkGrid.aspx?valueAddedCategoryID=" + valueAddedCategory + "&activitiesID=" + activitiesID + "&OthersCategoryID=" + othersCategory + "&LocationCategoryID=" + locationCategory + "&yearFrom=" + yearFrom + "&yearTo=" + yearTo + "&monthFrom=" + monthFrom + "&monthTo=" + monthTo + "&BedsInUnitFrom=" + bedsInUnitFrom + "&optBedsInUnitFrom=" + optBedsInUnitFrom + "&bedsInUnitTo=" + bedsInUnitTo + "&optBedsInUnitTo=" + optBedsInUnitTo + "&budgetedPatientFrom=" + budgetedPatientFrom + "&optBudgetedPatientFrom=" + optBudgetedPatientFrom + "&budgetedPatientTo=" + budgetedPatientTo + "&optBudgetedPatientTo=" + optBudgetedPatientTo + "&electronicDocumentationFrom=" + electronicDocumentationFrom + "&optElectronicDocumentationFrom=" + optElectronicDocumentationFrom + "&electronicDocumentationTo=" + electronicDocumentationTo + "&optElectronicDocumentationTo=" + optElectronicDocumentationTo + "&docByException=" + docByException + "&unitType=" + unitType + "&pharmacyType=" + pharmacyType + "&hospitalType=" + hospitalType + "&hospitalSizeFrom=" + hospitalSizeFrom + "&optHospitalSizeFrom=" + optHospitalSizeFrom + "&hospitalSizeTo=" + hospitalSizeTo + "&optHospitalSizeTo=" + optHospitalSizeTo + "&countryID=" + countryID + "&stateID=" + stateID + "&activities=" + activities + "&value=" + value + "&others=" + others + "&location=" + location + "&filter=" + filter + "&beginingDate=" + beginingDate + "&endingDate=" + endingDate + "&DataPointsFrom=" + dataPointsFrom + "&optDataPointsFrom=" + optDataPointsFrom + "&DataPointsTo=" + dataPointsTo + "&optDataPointsTo=" + optDataPointsTo + "&configName=" + configName;
        //                    //Response.Redirect(url);
        //                    // added by Raman 
        //                    // added on 10 Jan 2010
        //                    //Response.Redirect(url, false);
        //                    ////
        //                    DataTable DTCalculateFunctionValuesGrid = new DataTable();
        //                    BSReports objbsreport = new BSReports();
        //                    string startDate = null;
        //                    string endDate = null;
        //                    int? hospitalUnitID = null;
        //                    DTCalculateFunctionValuesGrid = objbsreport.CalculateFunctionValuesGrid(activitiesID, valueAddedCategory, othersCategory, locationCategory, hospitalUnitID, yearFrom, yearTo, monthFrom, monthTo, bedsInUnitFrom, optBedsInUnitFrom, bedsInUnitTo, optBedsInUnitTo, budgetedPatientFrom, optBudgetedPatientFrom, budgetedPatientTo, optBudgetedPatientTo, startDate, endDate, electronicDocumentationFrom, optElectronicDocumentationFrom, electronicDocumentationTo, optElectronicDocumentationTo, docByException, unitType, pharmacyType, hospitalType, optHospitalSizeFrom, hospitalSizeFrom, optHospitalSizeTo, hospitalSizeTo, countryID, stateID, activities, value, others, location, dataPointsFrom, optDataPointsFrom, dataPointsTo, optDataPointsTo, configName);
        //                    GridViewFunctionReport.DataSource = DTCalculateFunctionValuesGrid;
        //                    GridViewFunctionReport.DataBind();

        //                    DataTable DTGetDataForHospitalBenchmarkSummaryGrid = new DataTable();
        //                    DTGetDataForHospitalBenchmarkSummaryGrid = objbsreport.GetDataForHospitalBenchmarkSummaryGrid(activitiesID, valueAddedCategory, othersCategory, locationCategory, hospitalUnitID, yearFrom, yearTo, monthFrom, monthTo, bedsInUnitFrom, optBedsInUnitFrom, bedsInUnitTo, optBedsInUnitTo, budgetedPatientFrom, optBudgetedPatientFrom, budgetedPatientTo, optBudgetedPatientTo, startDate, endDate, electronicDocumentationFrom, optElectronicDocumentationFrom, electronicDocumentationTo, optElectronicDocumentationTo, docByException, unitType, pharmacyType, hospitalType, optHospitalSizeFrom, hospitalSizeFrom, optHospitalSizeTo, hospitalSizeTo, countryID, stateID, activities, value, others, location, dataPointsFrom, optDataPointsFrom, dataPointsTo, optDataPointsTo, configName);
        //                    GridViewReport.DataSource = DTGetDataForHospitalBenchmarkSummaryGrid;
        //                    GridViewReport.DataBind();
        //                    ////
        //                    //string fullUrl = "window.open('" + url + "','_blank','height=500,width=800,status=yes,toolbar=no,menubar=yes,location=no,scrollbars=yes,resizable=yes,titlebar=no');";
        //                    ////ButtonGenerateReport.Attributes.Add("OnClick", fullUrl);
        //                    //ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", fullUrl, true);


        //                }
        //                else
        //                {
        //                    CommonClass.Show("Profile Chosen Should Not Be More Than 10");
        //                }
        //            }

        //            catch (Exception ex)
        //            {
        //                LogManager._stringObject = "DemographicDetail.aspx ---- Page_Load";
        //                LogManager.SetExceptionDetails(ex, LogManager._stringObject, null);
        //                LogManager.LogException(ex, LogManager.LoggingCategory.General, LogManager.LoggingLevel.Error);
        //                CommonClass.Show(LogManager.ShowErrorDetail(ex));
        //            }

        //            finally
        //            {
        //                objectBenchmarkingFilter = null;
        //                objectBSReports = null;
        //            }
        //        }

        //    }
        //    catch (Exception ex)
        //    {

        //        LogManager._stringObject = "DemographicDetail.aspx ---- Page_Load";
        //        LogManager.SetExceptionDetails(ex, LogManager._stringObject, null);
        //        LogManager.LogException(ex, LogManager.LoggingCategory.General, LogManager.LoggingLevel.Error);
        //        CommonClass.Show(LogManager.ShowErrorDetail(ex));
        //    }
        //}

        protected void ButtonGenerateReport_Click(object sender, EventArgs e)
        {
            GenerateReport();
        }

        private void GenerateReport()
        {
            string bedsInUnitFrom, optBedsInUnitFrom, bedsInUnitTo, optBedsInUnitTo, budgetedPatientFrom, optBudgetedPatientFrom, budgetedPatientTo, optBudgetedPatientTo, electronicDocumentationFrom, optElectronicDocumentationFrom, electronicDocumentationTo, optElectronicDocumentationTo, docByException, unitType, pharmacyType, hospitalType, hospitalSizeFrom, optHospitalSizeFrom, hospitalSizeTo, optHospitalSizeTo, countryID, stateID, dataPointsFrom, optDataPointsFrom, dataPointsTo, optDataPointsTo, configName, UnitIds;
            string value = null, others = null, location = null, activities = null;
            string startDate = null,endDate = null,hospitalUnitID = null;
            string beginingDate, endingDate; string filter;
            string yearFrom = null, yearTo = null, monthFrom = null, monthTo = null;

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
            configName = null;
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
            UnitIds = null;

            try
            {
                LinkButtonExportReport.Visible = true;

                
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

                    #region if (ListBoxSelectedProfiles.Items.Count < 11)
                    if (ListBoxSelectedProfiles.Items.Count < 11)
                        {
                            
                            if (DropDownListBenchmarkingFilter.SelectedIndex != 1)
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
                                    UnitIds = objectBenchmarkingFilter.HospitalUnitIds.ToString();
                                    if (UnitIds == null)
                                    {
                                        UnitIds = null;
                                    }
                                }
                        }
                    #endregion if (ListBoxSelectedProfiles.Items.Count < 11)

                    #region if (ListBoxSelectedProfiles.Items.Count > 0)

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
                           
                            #region Commented

                            //string value = null;
                            //for (int Index = 0; Index < ListBoxValueAddedProfile.Items.Count; Index++)
                            //{
                            //    if (ListBoxValueAddedProfile.Items[Index].Selected)
                            //    {
                            //        valueAddedCategory += ListBoxValueAddedProfile.Items[Index].Value + ",";
                            //        value += ListBoxValueAddedProfile.Items[Index].Text + ",";
                            //    }
                            //    if (Index == (ListBoxValueAddedProfile.Items.Count - 1) && valueAddedCategory != "")
                            //    {
                            //        valueAddedCategory = valueAddedCategory.Remove(valueAddedCategory.Length - 1, 1);
                            //        value = value.Remove(value.Length - 1, 1);
                            //    }
                            //}
                            //string others = null;
                            //for (int Index = 0; Index < ListBoxOthersProfile.Items.Count; Index++)
                            //{
                            //    if (ListBoxOthersProfile.Items[Index].Selected)
                            //    {
                            //        othersCategory += ListBoxOthersProfile.Items[Index].Value + ",";
                            //        others += ListBoxOthersProfile.Items[Index].Text + ",";
                            //    }
                            //    if (Index == (ListBoxOthersProfile.Items.Count - 1) && othersCategory != "")
                            //    {
                            //        othersCategory = othersCategory.Remove(othersCategory.Length - 1, 1);
                            //        others = others.Remove(others.Length - 1, 1);
                            //    }
                            //}
                            //string location = null;
                            //for (int Index = 0; Index < ListBoxLocationProfile.Items.Count; Index++)
                            //{
                            //    if (ListBoxLocationProfile.Items[Index].Selected)
                            //    {
                            //        locationCategory += ListBoxLocationProfile.Items[Index].Value + ",";
                            //        location += ListBoxLocationProfile.Items[Index].Text + ",";
                            //    }
                            //    if (Index == (ListBoxLocationProfile.Items.Count - 1) && locationCategory != "")
                            //    {
                            //        locationCategory = locationCategory.Remove(locationCategory.Length - 1, 1);
                            //        location = location.Remove(location.Length - 1, 1);
                            //    }
                            //}

                            //string yearFrom = DropDownListYear.SelectedValue;
                            //if (yearFrom == "0")
                            //{
                            //    yearFrom = null;
                            //}
                            //string yearTo = DropDownListYearTo.SelectedValue;
                            //if (yearTo == "0")
                            //{
                            //    yearTo = null;
                            //}
                            //string monthFrom = DropDownListMonthFrom.SelectedValue;
                            //if (monthFrom == "0")
                            //{
                            //    monthFrom = null;
                            //}
                            //string monthTo = DropDownListMonthTo.SelectedValue;
                            //if (monthTo == "0")
                            //{
                            //    monthTo = null;
                            //}

                            //string yearFrom = null, yearTo = null, monthFrom = null, monthTo = null;
                            //if (DropDownListYearMonthFrom.SelectedValue != "0")
                            //{
                            //    string yearMonthFrom = DropDownListYearMonthFrom.SelectedValue;
                            //    string[] yearMonthFromArr = yearMonthFrom.Split(new char[] { ',' });
                            //    yearFrom = yearMonthFromArr[1].ToString();
                            //    monthFrom = yearMonthFromArr[0].ToString();

                            //    string yearMonthTo = DropDownListYearMonthTo.SelectedValue;
                            //    string[] yearMonthToArr = yearMonthTo.Split(new char[] { ',' });
                            //    yearTo = yearMonthToArr[1].ToString();
                            //    monthTo = yearMonthToArr[0].ToString();
                            //}

                            //string st = "ReportHospitalBenchmark.aspx?valueAddedCategoryID=" +  + "&OthersCategoryID=" +  + "&LocationCategoryID=" + + "&firstYear=" +  + "&lastYear=" +  + "&firstMonth=" +  + "&lastMonth=" +  + "&bedInUnit=" + TextBoxBedsInUnit.Text + "&optBedInUnit=" + DropDownListBedsInUnitOperator.SelectedValue + "&budgetedPatient=" + TextBoxBudgetedPatient.Text + "&optBudgetedPatient=" + DropDownListBudgetedPatientOperator.SelectedValue + "&electronicDocument=" + TextBoxElectronicDocumentation.Text + "&optElectronicDocument=" + DropDownListElectronicDocumentationOperator.SelectedValue + "&docByException=" + DropDownListDocByException.SelectedValue + "&unitType=" + unitType + "&pharmacyType=" + pharmacyType + "&hospitalUnitID=" + DropDownListHospitalUnit.SelectedValue + "&startDate=" + TextBoxStartDate.Text + "&endDate=" + TextBoxEndDate.Text + "&optHospitalSize=" + DropDownListHospitalSizeOperator.SelectedValue + "&hospitalSize=" + TextBoxHospitalSize.Text;
                            #endregion

                            filter = DropDownListBenchmarkingFilter.SelectedItem.Text;
                            if (DropDownListBenchmarkingFilter.SelectedIndex == 0)
                            {
                                filter = "No Filter";
                            }

                            
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

                            #region commented
                            //-- string url = "ReportHospitalBenchmarkGrid.aspx?valueAddedCategoryID=" + valueAddedCategory + "&activitiesID=" + activitiesID + "&OthersCategoryID=" + othersCategory + "&LocationCategoryID=" + locationCategory + "&yearFrom=" + yearFrom + "&yearTo=" + yearTo + "&monthFrom=" + monthFrom + "&monthTo=" + monthTo + "&BedsInUnitFrom=" + bedsInUnitFrom + "&optBedsInUnitFrom=" + optBedsInUnitFrom + "&bedsInUnitTo=" + bedsInUnitTo + "&optBedsInUnitTo=" + optBedsInUnitTo + "&budgetedPatientFrom=" + budgetedPatientFrom + "&optBudgetedPatientFrom=" + optBudgetedPatientFrom + "&budgetedPatientTo=" + budgetedPatientTo + "&optBudgetedPatientTo=" + optBudgetedPatientTo + "&electronicDocumentationFrom=" + electronicDocumentationFrom + "&optElectronicDocumentationFrom=" + optElectronicDocumentationFrom + "&electronicDocumentationTo=" + electronicDocumentationTo + "&optElectronicDocumentationTo=" + optElectronicDocumentationTo + "&docByException=" + docByException + "&unitType=" + unitType + "&pharmacyType=" + pharmacyType + "&hospitalType=" + hospitalType + "&hospitalSizeFrom=" + hospitalSizeFrom + "&optHospitalSizeFrom=" + optHospitalSizeFrom + "&hospitalSizeTo=" + hospitalSizeTo + "&optHospitalSizeTo=" + optHospitalSizeTo + "&countryID=" + countryID + "&stateID=" + stateID + "&activities=" + activities + "&value=" + value + "&others=" + others + "&location=" + location + "&filter=" + filter + "&beginingDate=" + beginingDate + "&endingDate=" + endingDate + "&DataPointsFrom=" + dataPointsFrom + "&optDataPointsFrom=" + optDataPointsFrom + "&DataPointsTo=" + dataPointsTo + "&optDataPointsTo=" + optDataPointsTo + "&configName=" + configName;
                            // added by Raman 
                            // added on 10 Jan 2010
                            //----- Response.Redirect(url, false);
                            //string fullUrl = "window.open('" + url + "','_blank','height=500,width=800,status=yes,toolbar=no,menubar=yes,location=no,scrollbars=yes,resizable=yes,titlebar=no');";
                            ////ButtonGenerateReport.Attributes.Add("OnClick", fullUrl);
                            //ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", fullUrl, true);
                            #endregion

                            getDataForGrid(activitiesID, valueAddedCategory, othersCategory, locationCategory, hospitalUnitID, yearFrom, yearTo, monthFrom, monthTo, bedsInUnitFrom, optBedsInUnitFrom, bedsInUnitTo, optBedsInUnitTo, budgetedPatientFrom, optBudgetedPatientFrom, budgetedPatientTo, optBudgetedPatientTo, startDate, endDate, electronicDocumentationFrom, optElectronicDocumentationFrom, electronicDocumentationTo, optElectronicDocumentationTo, docByException, unitType, pharmacyType, hospitalType, hospitalSizeFrom, optHospitalSizeFrom, hospitalSizeTo, optHospitalSizeTo, countryID, stateID, activities, value, others, location, dataPointsFrom, optDataPointsFrom, dataPointsTo, optDataPointsTo, configName, UnitIds);
                            }
                        else
                        {
                            CommonClass.Show("Profile Chosen Should Not Be More Than 10");
                        }

                        #endregion if (ListBoxSelectedProfiles.Items.Count > 0)

                    objectBenchmarkingFilter = null;
                    objectBSReports = null;
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
        protected void ButtonReset_Click(object sender, EventArgs e)
        {
            try
            {
                DropDownListBenchmarkingFilter.SelectedIndex = 0;
                ListBoxSelectedProfiles.Items.Clear();
                int userID = CommonClass.UserInformation.UserID;
                GetAllAvailableProfiles(userID);
                DropDownListYearMonthFrom.SelectedIndex = 0;
                DropDownListYearMonthTo.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                LogManager._stringObject = "DemographicDetail.aspx ---- Page_Load";
                LogManager.SetExceptionDetails(ex, LogManager._stringObject, null);
                LogManager.LogException(ex, LogManager.LoggingCategory.General, LogManager.LoggingLevel.Error);
                CommonClass.Show(LogManager.ShowErrorDetail(ex));
            }
        }

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

        #endregion

        #region Private Methods

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
            //try
            //{
            //    if (DropDownListHospitalName.Items.Count > 1)
            //    {
            //        DropDownListHospitalName.Items.Clear();
            //        DropDownListHospitalName.Items.Add("Select Hospital");
            //        DropDownListHospitalName.Items[0].Value = 0.ToString();
            //        DropDownListHospitalName.Items[0].Selected = true;
            //    }

            //    DropDownListHospitalName.DataTextField = "HospitalExtendedName";
            //    DropDownListHospitalName.DataValueField = "HospitalInfoID";
            //    DropDownListHospitalName.DataSource = objectGenericHospitalInfo;
            //    DropDownListHospitalName.DataBind();
            //}
            //catch (Exception ex)
            //{
            //    throw ex;
            //}
            //finally
            //{
            //    objectGenericHospitalInfo = null;
            //}
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
                objectGenericBEReportsYearMonth = objectBSReports.GetYearMonthCombo();
                if (objectGenericBEReportsYearMonth.Count > 1)
                {
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
            try
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
                //    TextBoxBedsInUnit.Text = null;
                //}
                //if (TextBoxBudgetedPatient.Text == string.Empty)
                //{
                //    TextBoxBudgetedPatient.Text = null;
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
                //    TextBoxElectronicDocumentation.Text = null;
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
                //    TextBoxHospitalSize.Text = null;
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
            catch (Exception ex)
            {
                throw ex;
            }

        }

        #endregion
//cm
        //protected void LinkButtonExportReport_Click(object sender, EventArgs e)
        //{
        //    Response.Clear();

        //    Response.Buffer = true;



        //    Response.AddHeader("content-disposition",

        //     "attachment;filename=GridViewExport.xls");

        //    Response.Charset = "";

        //    Response.ContentType = "application/vnd.ms-excel";

        //    System.IO.StringWriter  sw = new System.IO.StringWriter();

        //    HtmlTextWriter hw = new HtmlTextWriter(sw);



        //    //PrepareForExport(GridView1);

        //   // PrepareForExport(GridViewReport);
        //    GridViewReport.Columns[0].Visible = false;


        //    Table tb = new Table();

        //    TableRow tr1 = new TableRow();

        //    TableCell cell1 = new TableCell();

        //    cell1.Controls.Add(GridViewFunctionReport);

        //    tr1.Cells.Add(cell1);

        //    TableCell cell3 = new TableCell();

        //    cell3.Controls.Add(GridViewReport);

        //    TableCell cell2 = new TableCell();

        //    cell2.Text = "&nbsp;";

        //    //if (rbPreference.SelectedValue == "2")
        //    //{

        //    //    tr1.Cells.Add(cell2);

        //    //    tr1.Cells.Add(cell3);

        //    //    tb.Rows.Add(tr1);

        //   // }

        //   // else
        //   // {

        //        TableRow tr2 = new TableRow();

        //        tr2.Cells.Add(cell2);

        //        TableRow tr3 = new TableRow();

        //        tr3.Cells.Add(cell3);

        //        tb.Rows.Add(tr1);

        //        tb.Rows.Add(tr2);

        //        tb.Rows.Add(tr3);

        //    //}

        //    tb.RenderControl(hw);



        //    //style to format numbers to string

        //    string style = @"<style> .textmode { mso-number-format:\@; } </style>";

        //    Response.Write(style);

        //    Response.Output.Write(sw.ToString());

        //    Response.Flush();

        //    Response.End();
        //}


        protected void LinkButtonExportReport_Click(object sender, EventArgs e)
        {
            try
            {
                string filterName = DropDownListBenchmarkingFilter.SelectedItem.Text;
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
                fileName = fileName + filterName + "," + DropDownListYearMonthFrom.SelectedItem.Text.Replace(" ", "") + "-" + DropDownListYearMonthTo.SelectedItem.Text.Replace(" ", "") + ")" + ".xls";

                //fileName = fileName + ".xls";
                Response.AddHeader("Content-Disposition", fileName);
                Response.Charset = "";
                Response.Cache.SetCacheability(HttpCacheability.NoCache);
                //Response.ContentType = "Application/vnd.xls";
                Response.ContentType = "Application/ms-excel";
                System.IO.StringWriter stringWrite = new System.IO.StringWriter();
                System.Web.UI.HtmlTextWriter htmlWrite = new HtmlTextWriter(stringWrite);

                //GridViewFunctionReport.RenderControl(htmlWrite);
                //GridViewReport.RenderControl(htmlWrite);
                //Response.Write(stringWrite.ToString());
                //Response.End();

                HtmlForm frm = new HtmlForm();
                GridViewReport.Columns[0].Visible = false;
                GridViewFunctionReport.Parent.Controls.Add(frm);
                GridViewReport.Parent.Controls.Add(frm);


                frm.Attributes["runat"] = "server";
                frm.Controls.Add(GridViewFunctionReport);
                frm.Controls.Add(GridViewReport);

                frm.RenderControl(htmlWrite);
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

                    ////HyperLink popupLink = new HyperLink();
                    ////popupLink.NavigateUrl = "#";
                    ////popupLink.Attributes.Add("onclick", String.Format("Javascript:window.open('dv.aspx?applicationid={0}', 'windowName', 'options');", e.Row.Cells[0].Text));
                    ////e.Row.Cells[0].Controls.Add(popupLink);

                    //LinkButton emailLink = new LinkButton();
                    //emailLink = (LinkButton)(e.Row.Cells[1].FindControl("LinkButton1"));
                    //emailLink.OnClientClick = "javascript:popup_window=window.open('SendMessage.aspx?id=" + e.Row.Cells[1].Text + "', 'popup_window', 'width=500,height=420,top=100,left=400,scrollbars=1');popup_window.focus()";
                    ////emailLink.OnClientClick = "javascript:popup_window=window.open('SendMessage.aspx?id=" + "bharatg@smartdatainc.net" + "', 'popup_window', 'width=500,height=420,top=100,left=400,scrollbars=1');popup_window.focus()";


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
        private void getDataForGrid(string activitiesIDs, string valueAddedCategoryIDs, string OthersCategoryIDs, string LocationCategoryIDs, string hospitalUnitIDs, string yearFrom, string yearTo, string monthFrom, string monthTo, string BedsInUnitFrom, string optBedsInUnitFrom, string bedsInUnitTo, string optBedsInUnitTo, string budgetedPatientFrom1, string optBudgetedPatientFrom1, string budgetedPatientTo1, string optBudgetedPatientTo1, string startDate1, string endDate1, string electronicDocumentationFrom, string optElectronicDocumentationFrom, string electronicDocumentationTo, string optElectronicDocumentationTo, string docByException1, string unitType1, string pharmacyType1, string hospitalType1, string hospitalSizeFrom1, string optHospitalSizeFrom1, string hospitalSizeTo1, string optHospitalSizeTo1, string countryId1, string stateId1, string activities1, string value1, string others1, string location1, string dataPointsFrom1, string optDataPointsFrom1, string dataPointsTo1, string optDataPointsTo1, string configName1, string unitIds1)
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
            //if (hospitalUnitIDs != string.Empty)
            //{
            //    hospitalUnitId = Convert.ToInt32(hospitalUnitIDs);
            //}

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
            if (unitIds1 != "0")
            {
                UnitIds = unitIds1;
            }

            DataTable DTCalculateFunctionValuesGrid = new DataTable();
            DTCalculateFunctionValuesGrid = objectBEReports.CalculateFunctionValuesGrid(activitiesID, valueAddedCategoryID, OthersCategoryID, LocationCategoryID, hospitalUnitId, firstYear, lastYear, firstMonth, lastMonth, bedInUnitFrom, optBedInUnitFrom, bedInUnitTo, optBedInUnitTo, budgetedPatientFrom, optBudgetedPatientFrom, budgetedPatientTo, optBudgetedPatientTo, startDate, endDate, electronicDocumentFrom, optElectronicDocumentFrom, electronicDocumentTo, optElectronicDocumentTo, docByException, unitType, pharmacyType, hospitalType, optHospitalSizeFrom, hospitalSizeFrom, optHospitalSizeTo, hospitalSizeTo, countryId, stateId, activities, value, others, location, dataPointsFrom, optDataPointsFrom, dataPointsTo, optDataPointsTo, configName, UnitIds);
            GridViewFunctionReport.DataSource = DTCalculateFunctionValuesGrid;
            GridViewFunctionReport.DataBind();
            

            DataTable DTGetDataForHospitalBenchmarkSummaryGrid = new DataTable();
            DTGetDataForHospitalBenchmarkSummaryGrid = objectBEReports.GetDataForHospitalBenchmarkSummaryGrid(activitiesID, valueAddedCategoryID, OthersCategoryID, LocationCategoryID, null, firstYear, lastYear, firstMonth, lastMonth, bedInUnitFrom, optBedInUnitFrom, bedInUnitTo, optBedInUnitTo, budgetedPatientFrom, optBudgetedPatientFrom, budgetedPatientTo, optBudgetedPatientTo, startDate, endDate, electronicDocumentFrom, optElectronicDocumentFrom, electronicDocumentTo, optElectronicDocumentTo, docByException, unitType, pharmacyType, hospitalType, optHospitalSizeFrom, hospitalSizeFrom, optHospitalSizeTo, hospitalSizeTo, countryId, stateId, activities, value, others, location, dataPointsFrom, optDataPointsFrom, dataPointsTo, optDataPointsTo, configName, UnitIds);
            GridViewReport.DataSource = DTGetDataForHospitalBenchmarkSummaryGrid;
            GridViewReport.DataBind();

            if (DTGetDataForHospitalBenchmarkSummaryGrid.Rows.Count > 0)
            { 
                ButtonSendEmail.Visible = true;
                ButtonNewBenchmarkingFilter.Visible = true;
            }
            else 
            { 
                ButtonSendEmail.Visible = false;
                ButtonNewBenchmarkingFilter.Visible = false;
            }
        }

        protected void ButtonNewBenchmarkingFilter_Click(object sender, EventArgs e)
        {
           
            try
            {
                //
                string UnitIds = HiddenFieldUnitId.Value;
                string[] arrayId = UnitIds.Split(',');
                string Id = "";
                string UnitId = "";
                for (int i = 0; i < arrayId.Length; i++)
                {
                    Id = arrayId[i];                    
                        if (UnitId != "")
                        {
                            UnitId = UnitId + ",";
                        }
                        Id = Id.Trim();
                        if (Id.Contains("{"))
                        {
                            int a = Id.IndexOf("{");
                            int b = Id.IndexOf("}");
                            UnitId = UnitId + Id.Substring(a + 1, b - a - 1);
                        }                    
                }
                //

                if (UnitId != "")
                {
                    bool flag = false;

                    objectBenchmarkFilter = new RMC.DataService.BenchmarkFilter();
                    objectBSReports = new RMC.BussinessService.BSReports();

                    bool filternameisexist = false;
                    filternameisexist = objectBSReports.FilterNameIsExist(TextBoxNewFilterName.Text);
                    if (filternameisexist)
                    {
                        TextBoxNewFilterName.Text = "";
                        string mes = "New Filter Name Is Already Exist! Please Change It.";
                        Response.Write("<script language=\"javascript\"  type=\"text/javascript\">alert('" + mes + "');</script>");
                    }
                    else
                    {

                        //objectBenchmarkFilter.FilterName = CommonClass.UserInformation.FirstName + CommonClass.UserInformation.LastName + " Only Include " + UnitId + " Hospital Unit Ids "; 
                        objectBenchmarkFilter.FilterName = TextBoxNewFilterName.Text;
                        //objectBenchmarkFilter.CreatedBy = ((List<RMC.DataService.UserInfo>)HttpContext.Current.Session["UserInformation"])[0].ToString();
                        objectBenchmarkFilter.CreatedBy = CommonClass.UserInformation.FirstName + CommonClass.UserInformation.LastName;
                        objectBenchmarkFilter.Share = true;
                        objectBenchmarkFilter.Comment = "Only Include " + UnitId + " Hospital Unit Ids";
                        objectBenchmarkFilter.BedsInUnitFrom = 0;
                        objectBenchmarkFilter.optBedsInUnitFrom = 0;
                        objectBenchmarkFilter.optBedsInUnitTo = 0;
                        objectBenchmarkFilter.optBudgetedPatientFrom = 0;
                        objectBenchmarkFilter.BedsInUnitTo = 0;
                        objectBenchmarkFilter.BudgetedPatientFrom = 0;
                        objectBenchmarkFilter.BudgetedPatientTo = 0;
                        objectBenchmarkFilter.ElectronicDocumentationFrom = 0;
                        objectBenchmarkFilter.optBudgetedPatientTo = 0;
                        objectBenchmarkFilter.ElectronicDocumentationFrom = 0;
                        objectBenchmarkFilter.optElectronicDocumentationFrom = 0;
                        objectBenchmarkFilter.ElectronicDocumentationTo = 0;
                        objectBenchmarkFilter.optElectronicDocumentationTo = 0;
                        objectBenchmarkFilter.HospitalSizeFrom = 0;
                        objectBenchmarkFilter.optHospitalSizeFrom = 0;
                        objectBenchmarkFilter.HospitalSizeTo = 0;
                        objectBenchmarkFilter.optHospitalSizeTo = 0;
                        objectBenchmarkFilter.DataPointsFrom = 0;
                        objectBenchmarkFilter.optDataPointsFrom = 0;
                        objectBenchmarkFilter.DataPointsTo = 0;
                        objectBenchmarkFilter.optDataPointsTo = 0;
                        objectBenchmarkFilter.UnitType = "0";
                        objectBenchmarkFilter.PharmacyType = "0";
                        objectBenchmarkFilter.HospitalType = "0";
                        objectBenchmarkFilter.ConfigurationName = "0";
                        objectBenchmarkFilter.DocByException = 0;
                        objectBenchmarkFilter.CountryId = 0;
                        objectBenchmarkFilter.CreatedDate = DateTime.Now;
                        //objectBenchmarkFilter.ModifiedBy = TextBoxFilterName.Text;
                        //objectBenchmarkFilter.ModifiedDate = DateTime.Now;
                        objectBenchmarkFilter.StateId = 0;
                        objectBenchmarkFilter.HospitalUnitIds = UnitId;
                        flag = objectBSReports.InsertBenchmarkFilter(objectBenchmarkFilter);

                        if (flag)
                        {
                            //CommonClass.Show("Benchmark Filter Saved Successfully.");                        
                            //string mes = "Benchmark Filter Saved Successfully.";
                            //Response.Write("<script language=\"javascript\"  type=\"text/javascript\">alert('" + mes + "');</script>");

                            DropDownListBenchmarkingFilter.DataBind();
                            DropDownListBenchmarkingFilter.SelectedItem.Selected = false;
                            DropDownListBenchmarkingFilter.Items.FindByText(TextBoxNewFilterName.Text).Selected = true;
                            TextBoxNewFilterName.Text = "";
                            GenerateReport();
                        }
                        else
                        {
                            //CommonClass.Show("Failed To Save Benchmark Filter.");
                            string mes = "Failed To Save Benchmark Filter.";
                            Response.Write("<script language=\"javascript\"  type=\"text/javascript\">alert('" + mes + "');</script>");
                        }
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

        protected void RadioButtonSummarizeByYear_CheckedChanged(object sender, EventArgs e)
        {
            reportSummaryType = CollaborationReportView.SummarizeByYear;
            Session["CollaborationReportView"] = reportSummaryType;
            //GenerateReport();
        }

        protected void RadioButtonSummarizeAllData_CheckedChanged(object sender, EventArgs e)
        {
            reportSummaryType = CollaborationReportView.SummarizeAllData;
            Session["CollaborationReportView"] = reportSummaryType;
            //GenerateReport();
        }

        protected void ButtonSendEmail_Click(object sender, EventArgs e)
        {

        }   
    }
}