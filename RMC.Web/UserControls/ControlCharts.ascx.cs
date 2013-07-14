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
using RMC.BussinessService;
using System.Web.UI.DataVisualization.Charting;
using LogExceptions;

namespace RMC.Web.UserControls
{
    public partial class ControlCharts : System.Web.UI.UserControl
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
        string valueAddedCategory = string.Empty, othersCategory = string.Empty, locationCategory = string.Empty, activitiesCategory = string.Empty;

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
                    int userID = CommonClass.UserInformation.UserID;
                    GetAllHospitalNames(userID);
                    GetAllAvailableProfiles(userID);
                    DropDownListProfileCategory.Items.Add(new ListItem("Database Values", "Database Values"));
                    //DropDownListProfileCategory.Items.Add(new ListItem("Special Category", "Special Category"));
                    //GetYearMonth();
                }

                //if (Page.IsPostBack != true)
                //{
                //    if (Request.QueryString["filter"] != null)
                //    {
                //        string filter = Request.QueryString["filter"];
                //        string beginingDate = Request.QueryString["beginingDate"];
                //        string endingDate = Request.QueryString["endingDate"];

                //        string hospitalName = "#" + Request.QueryString["hospitalName"];
                //        string hospitalUnitName = Request.QueryString["hospitalUnitName"];
                //        string profileCategory = Request.QueryString["profileCategory"];
                //        string profileSubCategory = Request.QueryString["profileSubCategory"];

                //        if (profileCategory != null)
                //        {
                //            DropDownListProfileCategory.Items.FindByText(profileCategory).Selected = true;
                //        }
                //        if (DropDownListProfileCategory.SelectedValue == "Database Values")
                //        {
                //            DropDownListProfileSubCategory.Items.Clear();
                //            DropDownListProfileSubCategory.Items.Add("Select Sub Profile");
                //            DropDownListProfileSubCategory.Items[0].Value = 0.ToString();
                //            DropDownListProfileSubCategory.Items.Add(new ListItem("Last Location", "Last Location"));
                //            DropDownListProfileSubCategory.Items.Add(new ListItem("Current Location", "Current Location"));
                //            DropDownListProfileSubCategory.Items.Add(new ListItem("Activity", "Activity"));
                //            DropDownListProfileSubCategory.Items.Add(new ListItem("Sub-Activity", "Sub-Activity"));
                //            DropDownListProfileSubCategory.Items.Add(new ListItem("Cognitive", "Cognitive"));
                //            DropDownListProfileSubCategory.Items.Add(new ListItem("Staffing Model", "Staffing Model"));
                //        }
                //        else
                //        {
                //            DropDownListProfileSubCategory.DataBind();
                //        }
                //        if (profileSubCategory != null)
                //        {
                //            DropDownListProfileSubCategory.Items.FindByText(profileSubCategory).Selected = true;
                //        }
                //        if (filter != null)
                //        {
                //            DropDownListBenchmarkingFilter.Items.FindByText(filter).Selected = true;
                //        }
                //        if (beginingDate != null)
                //        {
                //            DropDownListYearMonthFrom.Items.FindByText(beginingDate).Selected = true;
                //        }
                //        if (endingDate != null)
                //        {
                //            DropDownListYearMonthTo.Items.FindByText(endingDate).Selected = true;
                //        }
                //        if (hospitalName != null)
                //        {
                //            DropDownListHospitalName.Items.FindByText(hospitalName).Selected = true;
                //        }
                //        DropDownListHospitalUnit.DataBind();
                //        if (hospitalUnitName != null)
                //        {
                //            DropDownListHospitalUnit.Items.FindByText(hospitalUnitName).Selected = true;
                //        }
                //    }
                //}
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

        protected void ButtonGenerateReport_Click(object sender, EventArgs e)
        {
            try
            {
                DropDownListMarkerStyle.DataBind();
                DropDownListMarkerStyle.SelectedIndex = 3;
                DropDownListMarkerColor.DataBind();
                DropDownListMarkerColor.SelectedIndex = 36;
                DropDownListColor.DataBind();
                DropDownListColor.SelectedIndex = 36;
                DropDownListMarkerSize.SelectedValue = "6";
                DropDownListLineWidth.SelectedValue = "2";
                MultiViewCharts.Visible = true;
                GetLineChartSeries();

                ////-----To display in pop-up page----------
                //RMC.BussinessService.BSReports objectBSReports = new RMC.BussinessService.BSReports();

                //string yearFrom = null, yearTo = null, monthFrom = null, monthTo = null;
                //if (DropDownListYearMonthFrom.SelectedValue != "0")
                //{
                //    string yearMonthFrom = DropDownListYearMonthFrom.SelectedValue;
                //    string[] yearMonthFromArr = yearMonthFrom.Split(new char[] { ',' });
                //    yearFrom = yearMonthFromArr[1].ToString();
                //    monthFrom = yearMonthFromArr[0].ToString();
                //}
                //if (DropDownListYearMonthTo.SelectedValue != "0")
                //{
                //    string yearMonthTo = DropDownListYearMonthTo.SelectedValue;
                //    string[] yearMonthToArr = yearMonthTo.Split(new char[] { ',' });
                //    yearTo = yearMonthToArr[1].ToString();
                //    monthTo = yearMonthToArr[0].ToString();
                //}

                //if (Convert.ToInt32(yearFrom) > Convert.ToInt32(yearTo) || (Convert.ToInt32(yearFrom) == Convert.ToInt32(yearTo) && Convert.ToInt32(monthFrom) > Convert.ToInt32(monthTo)))
                //{
                //    CommonClass.Show("Period To cannot be less than Period From");
                //}
                //else
                //{
                //    string bedsInUnitFrom, optBedsInUnitFrom, bedsInUnitTo, optBedsInUnitTo, budgetedPatientFrom, optBudgetedPatientFrom, budgetedPatientTo, optBudgetedPatientTo, electronicDocumentationFrom, optElectronicDocumentationFrom, electronicDocumentationTo, optElectronicDocumentationTo, docByException, unitType, pharmacyType, hospitalType, hospitalSizeFrom, optHospitalSizeFrom, hospitalSizeTo, optHospitalSizeTo, countryID, stateID;
                //    RMC.DataService.BenchmarkFilter objectBenchmarkingFilter = new RMC.DataService.BenchmarkFilter();
                //    try
                //    {
                //        if (DropDownListBenchmarkingFilter.SelectedIndex == 1)
                //        {
                //            bedsInUnitFrom = null;
                //            optBedsInUnitFrom = "0";
                //            bedsInUnitTo = null;
                //            optBedsInUnitTo = "0";
                //            budgetedPatientFrom = null;
                //            optBudgetedPatientFrom = "0";
                //            budgetedPatientTo = null;
                //            optBudgetedPatientTo = "0";
                //            electronicDocumentationFrom = null;
                //            optElectronicDocumentationFrom = "0";
                //            electronicDocumentationTo = null;
                //            optElectronicDocumentationTo = "0";
                //            docByException = "0";
                //            unitType = null;
                //            pharmacyType = null;
                //            hospitalType = null;
                //            hospitalSizeFrom = null;
                //            optHospitalSizeFrom = "0";
                //            hospitalSizeTo = null;
                //            optHospitalSizeTo = "0";
                //            countryID = "0";
                //            stateID = "0";
                //        }
                //        else
                //        {

                //            objectBenchmarkingFilter = objectBSReports.GetBenchmarkFilterRow(Convert.ToInt32(DropDownListBenchmarkingFilter.SelectedValue), DropDownListBenchmarkingFilter.SelectedItem.Text);
                //            bedsInUnitFrom = objectBenchmarkingFilter.BedsInUnitFrom.ToString();
                //            if (bedsInUnitFrom == "0")
                //            {
                //                bedsInUnitFrom = null;
                //            }
                //            optBedsInUnitFrom = objectBenchmarkingFilter.optBedsInUnitFrom.ToString();
                //            bedsInUnitTo = objectBenchmarkingFilter.BedsInUnitTo.ToString();
                //            if (bedsInUnitTo == "0")
                //            {
                //                bedsInUnitTo = null;
                //            }
                //            optBedsInUnitTo = objectBenchmarkingFilter.optBedsInUnitTo.ToString();

                //            budgetedPatientFrom = objectBenchmarkingFilter.BudgetedPatientFrom.ToString();
                //            if (budgetedPatientFrom == "0")
                //            {
                //                budgetedPatientFrom = null;
                //            }
                //            optBudgetedPatientFrom = objectBenchmarkingFilter.optBudgetedPatientFrom.ToString();
                //            budgetedPatientTo = objectBenchmarkingFilter.BudgetedPatientTo.ToString();
                //            if (budgetedPatientTo == "0")
                //            {
                //                budgetedPatientTo = null;
                //            }
                //            optBudgetedPatientTo = objectBenchmarkingFilter.optBudgetedPatientTo.ToString();

                //            electronicDocumentationFrom = objectBenchmarkingFilter.ElectronicDocumentationFrom.ToString();
                //            if (electronicDocumentationFrom == "0")
                //            {
                //                electronicDocumentationFrom = null;
                //            }
                //            optElectronicDocumentationFrom = objectBenchmarkingFilter.optElectronicDocumentationFrom.ToString();
                //            electronicDocumentationTo = objectBenchmarkingFilter.ElectronicDocumentationTo.ToString();
                //            if (electronicDocumentationTo == "0")
                //            {
                //                electronicDocumentationTo = null;
                //            }
                //            optElectronicDocumentationTo = objectBenchmarkingFilter.optElectronicDocumentationTo.ToString();

                //            docByException = objectBenchmarkingFilter.DocByException.ToString();

                //            unitType = objectBenchmarkingFilter.UnitType.ToString();
                //            if (unitType == "0")
                //            {
                //                unitType = null;
                //            }

                //            pharmacyType = objectBenchmarkingFilter.PharmacyType.ToString();
                //            if (pharmacyType == "0")
                //            {
                //                pharmacyType = null;
                //            }

                //            hospitalType = objectBenchmarkingFilter.HospitalType.ToString();
                //            if (hospitalType == "0")
                //            {
                //                hospitalType = null;
                //            }

                //            hospitalSizeFrom = objectBenchmarkingFilter.HospitalSizeFrom.ToString();
                //            if (hospitalSizeFrom == "0")
                //            {
                //                hospitalSizeFrom = null;
                //            }
                //            optHospitalSizeFrom = objectBenchmarkingFilter.optHospitalSizeFrom.ToString();
                //            hospitalSizeTo = objectBenchmarkingFilter.HospitalSizeTo.ToString();
                //            if (hospitalSizeTo == "0")
                //            {
                //                hospitalSizeTo = null;
                //            }
                //            optHospitalSizeTo = objectBenchmarkingFilter.optHospitalSizeTo.ToString();

                //            countryID = objectBenchmarkingFilter.CountryId.ToString();
                //            stateID = objectBenchmarkingFilter.StateId.ToString();
                //        }
                //        string value = null, others = null, location = null;

                //        if (DropDownListProfileCategory.SelectedValue.Contains("Value Added"))
                //        {
                //            valueAddedCategory = DropDownListProfileCategory.SelectedValue.Replace(",Value Added", "");
                //            value = DropDownListProfileCategory.SelectedItem.Text;
                //        }
                //        if (DropDownListProfileCategory.SelectedValue.Contains("Others"))
                //        {
                //            othersCategory = DropDownListProfileCategory.SelectedValue.Replace(",Others", "");
                //            others = DropDownListProfileCategory.SelectedItem.Text;
                //        }
                //        if (DropDownListProfileCategory.SelectedValue.Contains("Location"))
                //        {
                //            locationCategory = DropDownListProfileCategory.SelectedValue.Replace(",Location", "");
                //            location = DropDownListProfileCategory.SelectedItem.Text;
                //            //location = DropDownListProfileCategory.SelectedValue.Replace(locationCategory + ",", "");
                //        }

                //        string hospitalUnitID = DropDownListHospitalUnit.SelectedValue;
                //        string profileCategory = DropDownListProfileCategory.SelectedItem.Text;
                //        string profileSubCategory = DropDownListProfileSubCategory.SelectedItem.Text;
                //        int RecordCounter = objectBSReports.GetRecordCounterOfHospitalID(Convert.ToInt32(DropDownListHospitalName.SelectedValue));
                //        string filter = DropDownListBenchmarkingFilter.SelectedItem.Text;
                //        if (DropDownListBenchmarkingFilter.SelectedIndex == 0)
                //        {
                //            filter = "No Filter";
                //        }
                //        string beginingDate, endingDate;
                //        if (DropDownListYearMonthFrom.SelectedIndex != 0)
                //        {
                //            beginingDate = DropDownListYearMonthFrom.SelectedItem.Text;
                //            endingDate = DropDownListYearMonthTo.SelectedItem.Text;
                //        }
                //        else
                //        {
                //            beginingDate = DropDownListYearMonthFrom.Items[1].Text;
                //            int count = DropDownListYearMonthTo.Items.Count;
                //            endingDate = DropDownListYearMonthTo.Items[count - 1].Text;
                //        }
                //        string url = string.Empty;
                //        url = "ReportTimeRNCharts.aspx?Report=ControlCharts&valueAddedCategoryID=" + valueAddedCategory + "&OthersCategoryID=" + othersCategory + "&LocationCategoryID=" + locationCategory + "&yearFrom=" + yearFrom + "&yearTo=" + yearTo + "&monthFrom=" + monthFrom + "&monthTo=" + monthTo + "&BedsInUnitFrom=" + bedsInUnitFrom + "&optBedsInUnitFrom=" + optBedsInUnitFrom + "&bedsInUnitTo=" + bedsInUnitTo + "&optBedsInUnitTo=" + optBedsInUnitTo + "&budgetedPatientFrom=" + budgetedPatientFrom + "&optBudgetedPatientFrom=" + optBudgetedPatientFrom + "&budgetedPatientTo=" + budgetedPatientTo + "&optBudgetedPatientTo=" + optBudgetedPatientTo + "&electronicDocumentationFrom=" + electronicDocumentationFrom + "&optElectronicDocumentationFrom=" + optElectronicDocumentationFrom + "&electronicDocumentationTo=" + electronicDocumentationTo + "&optElectronicDocumentationTo=" + optElectronicDocumentationTo + "&docByException=" + docByException + "&unitType=" + unitType + "&pharmacyType=" + pharmacyType + "&hospitalType=" + hospitalType + "&hospitalSizeFrom=" + hospitalSizeFrom + "&optHospitalSizeFrom=" + optHospitalSizeFrom + "&hospitalSizeTo=" + hospitalSizeTo + "&optHospitalSizeTo=" + optHospitalSizeTo + "&countryID=" + countryID + "&stateID=" + stateID + "&value=" + value + "&others=" + others + "&location=" + location + "&hospitalUnitID=" + hospitalUnitID + "&profileSubCategory=" + profileSubCategory + "&profileCategory=" + profileCategory + "&hospitalID=" + RecordCounter + "&hospitalUnitName=" + DropDownListHospitalUnit.SelectedItem.Text + "&filter=" + filter + "&hospitalName=" + DropDownListHospitalName.SelectedItem.Text.Remove(0, 1) + "&beginingDate=" + beginingDate + "&endingDate=" + endingDate;
                //        Response.Redirect(url);

                //    }
                //    catch (Exception ex)
                //    {
                //        LogManager._stringObject = "DemographicDetail.aspx ---- Page_Load";
                //        LogManager.SetExceptionDetails(ex, LogManager._stringObject, null);
                //        LogManager.LogException(ex, LogManager.LoggingCategory.General, LogManager.LoggingLevel.Error);
                //        CommonClass.Show(LogManager.ShowErrorDetail(ex));
                //    }
                //}


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

        protected void DropDownListProfileCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                DropDownListValues.Items.Clear();
                DropDownListValues.Items.Add("Select Value");
                DropDownListValues.Items[0].Value = 0.ToString();
                DropDownListValues.Visible = false;
                LabelValues.Visible = false;

                DropDownListProfileSubCategory.Items.Clear();
                DropDownListProfileSubCategory.Items.Add("Select Sub Profile");
                DropDownListProfileSubCategory.Items[0].Value = 0.ToString();
                DropDownListProfileSubCategory.DataBind();
                DropDownListProfileSubCategory.AutoPostBack = false;

                //If "Database Values" item is selected from profile category dropdown
                if (DropDownListProfileCategory.SelectedValue == "Database Values")
                {
                    DropDownListProfileSubCategory.Items.Clear();
                    DropDownListProfileSubCategory.Items.Add("Select Sub Profile");
                    DropDownListProfileSubCategory.Items[0].Value = 0.ToString();
                    DropDownListProfileSubCategory.Items.Add(new ListItem("Last Location", "Last Location"));
                    DropDownListProfileSubCategory.Items.Add(new ListItem("Current Location", "Current Location"));
                    DropDownListProfileSubCategory.Items.Add(new ListItem("Activity", "Activity"));
                    DropDownListProfileSubCategory.Items.Add(new ListItem("Sub-Activity", "Sub-Activity"));
                    DropDownListProfileSubCategory.Items.Add(new ListItem("Cognitive", "Cognitive"));
                    DropDownListProfileSubCategory.Items.Add(new ListItem("Staffing Model", "Staffing Model"));
                    DropDownListProfileSubCategory.AutoPostBack = true;
                }
                //If "Special Category" item is selected from profile category dropdown
                if (DropDownListProfileCategory.SelectedValue == "Special Category")
                {
                    DropDownListProfileSubCategory.Items.Clear();
                    DropDownListProfileSubCategory.Items.Add("Select Sub Profile");
                    DropDownListProfileSubCategory.Items[0].Value = 0.ToString();
                    
                    RMC.BussinessService.BSReports objectBSReports = new RMC.BussinessService.BSReports();
                    List<RMC.BusinessEntities.BEValidationSpecialType> objectGenericBEValidationSpecialType = null;
                    objectGenericBEValidationSpecialType = objectBSReports.GetSpecialCategory();

                    foreach (RMC.BusinessEntities.BEValidationSpecialType st in objectGenericBEValidationSpecialType)
                    {
                        DropDownListProfileSubCategory.Items.Add(new ListItem(st.SpecialCategory, st.SpecialCategory));
                    }
                    DropDownListProfileSubCategory.AutoPostBack = true;
                }
                DropDownListProfileSubCategory.SelectedIndex = 0;
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

        protected void DropDownListProfileSubCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (DropDownListProfileCategory.SelectedItem.Text == "Database Values" || DropDownListProfileCategory.SelectedItem.Text == "Special Category")
                {
                    LabelValues.Visible = true;
                    DropDownListValues.Visible = true;
                    DropDownListValues.Items.Clear();
                    DropDownListValues.Items.Add("Select Value");
                    DropDownListValues.Items[0].Value = 0.ToString();
                    DropDownListValues.DataBind();
                }
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
                    //if (DropDownListProfileCategory.Items.Count > 0)
                    //{
                    //    DropDownListProfileCategory.Items.Clear();
                    //}

                    foreach (RMC.DataService.ProfileType pt in objectGenericProfileType)
                    {
                        DropDownListProfileCategory.Items.Add(new ListItem(pt.ProfileName, pt.ProfileTypeID + "," + pt.Type));
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
            try
            {
                DropDownListHospitalName.SelectedIndex = 0;
                DropDownListHospitalUnit.SelectedIndex = 0;
                DropDownListBenchmarkingFilter.SelectedIndex = 1;
                DropDownListProfileCategory.SelectedIndex = 0;
                DropDownListProfileSubCategory.SelectedIndex = 0;
                DropDownListYearMonthFrom.SelectedIndex = 0;
                DropDownListYearMonthTo.SelectedIndex = 0;
                DropDownListValues.SelectedIndex = 0;
                DropDownListValues.Visible = false;
                MultiViewCharts.Visible = false;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Method to Get a list of Data For Line Chart
        /// </summary>
        private List<RMC.BusinessEntities.BEReports> GetDataForLineChart()
        {
            try
            {
                RMC.BussinessService.BSReports objectBSReports = new RMC.BussinessService.BSReports();
                List<RMC.BusinessEntities.BEReports> objectGenericBEReports = null;

                string stryearFrom = "0", stryearTo = "0", strmonthFrom = "0", strmonthTo = "0";
                if (DropDownListYearMonthFrom.SelectedValue != "0")
                {
                    string yearMonthFrom = DropDownListYearMonthFrom.SelectedValue;
                    string[] yearMonthFromArr = yearMonthFrom.Split(new char[] { ',' });
                    stryearFrom = yearMonthFromArr[1].ToString();
                    strmonthFrom = yearMonthFromArr[0].ToString();
                }
                if (DropDownListYearMonthTo.SelectedValue != "0")
                {
                    string yearMonthTo = DropDownListYearMonthTo.SelectedValue;
                    string[] yearMonthToArr = yearMonthTo.Split(new char[] { ',' });
                    stryearTo = yearMonthToArr[1].ToString();
                    strmonthTo = yearMonthToArr[0].ToString();
                }

                if (Convert.ToInt32(stryearFrom) > Convert.ToInt32(stryearTo) || (Convert.ToInt32(stryearFrom) == Convert.ToInt32(stryearTo) && Convert.ToInt32(strmonthFrom) > Convert.ToInt32(strmonthTo)))
                {
                    CommonClass.Show("PeriodTo Cannot Be Less Than PeriodFrom");
                }
                else
                {
                    string BedsInUnitFrom, bedsInUnitTo, strbudgetedPatientFrom, strbudgetedPatientTo, strelectronicDocumentationFrom, strelectronicDocumentationTo, unitType, pharmacyType, hospitalType, strhospitalSizeFrom, strhospitalSizeTo, configName, unitIds;
                    int optBedsInUnitFrom, optBedsInUnitTo, optBudgetedPatientFrom, optBudgetedPatientTo, optElectronicDocumentFrom, optElectronicDocumentTo, docByException, optHospitalSizeFrom, optHospitalSizeTo;
                    int? countryId = null, stateId = null;
                    int? bedInUnitFrom = null, bedInUnitTo = null, electronicDocumentFrom = null, electronicDocumentTo = null, hospitalSizeFrom = null, hospitalSizeTo = null;
                    float? budgetedPatientFrom = null, budgetedPatientTo = null;
                    string startDate = null, endDate = null;
                    RMC.DataService.BenchmarkFilter objectBenchmarkingFilter = new RMC.DataService.BenchmarkFilter();

                    if (DropDownListBenchmarkingFilter.SelectedItem.Text == "No Filter")
                    {
                        BedsInUnitFrom = null;
                        optBedsInUnitFrom = 0;
                        bedsInUnitTo = null;
                        optBedsInUnitTo = 0;
                        strbudgetedPatientFrom = null;
                        optBudgetedPatientFrom = 0;
                        strbudgetedPatientTo = null;
                        optBudgetedPatientTo = 0;
                        strelectronicDocumentationFrom = null;
                        optElectronicDocumentFrom = 0;
                        strelectronicDocumentationFrom = null;
                        optElectronicDocumentTo = 0;
                        docByException = 0;
                        unitType = null;
                        pharmacyType = null;
                        hospitalType = null;
                        strhospitalSizeFrom = null;
                        optHospitalSizeFrom = 0;
                        strhospitalSizeTo = null;
                        optHospitalSizeTo = 0;
                        //countryId = 0;
                        //stateId = 0;
                        configName = null;
                        unitIds = null;
                    }
                    else
                    {
                        objectBenchmarkingFilter = objectBSReports.GetBenchmarkFilterRow(Convert.ToInt32(DropDownListBenchmarkingFilter.SelectedValue), DropDownListBenchmarkingFilter.SelectedItem.Text);

                        BedsInUnitFrom = objectBenchmarkingFilter.BedsInUnitFrom.ToString();
                        if (BedsInUnitFrom == string.Empty)
                        { BedsInUnitFrom = "0"; }
                        bedInUnitFrom = Convert.ToInt32(BedsInUnitFrom);
                        if (bedInUnitFrom == 0)
                        {
                            bedInUnitFrom = null;
                        }
                        optBedsInUnitFrom = Convert.ToInt32(objectBenchmarkingFilter.optBedsInUnitFrom);
                        bedsInUnitTo = objectBenchmarkingFilter.BedsInUnitTo.ToString();
                        if (bedsInUnitTo == string.Empty)
                        { bedsInUnitTo = "0"; }
                        bedInUnitTo = Convert.ToInt32(bedsInUnitTo);
                        if (bedInUnitTo == 0)
                        {
                            bedInUnitTo = null;
                        }
                        optBedsInUnitTo = Convert.ToInt32(objectBenchmarkingFilter.optBedsInUnitTo);

                        strbudgetedPatientFrom = objectBenchmarkingFilter.BudgetedPatientFrom.ToString();
                        if (strbudgetedPatientFrom == string.Empty)
                        { strbudgetedPatientFrom = "0"; }
                        budgetedPatientFrom = Convert.ToInt32(strbudgetedPatientFrom);
                        if (budgetedPatientFrom == 0)
                        {
                            budgetedPatientFrom = null;
                        }
                        optBudgetedPatientFrom = Convert.ToInt32(objectBenchmarkingFilter.optBudgetedPatientFrom);
                        strbudgetedPatientTo = objectBenchmarkingFilter.BudgetedPatientTo.ToString();
                        if (strbudgetedPatientTo == string.Empty)
                        { strbudgetedPatientTo = "0"; }
                        budgetedPatientTo = Convert.ToInt32(strbudgetedPatientTo);
                        if (budgetedPatientTo == 0)
                        {
                            budgetedPatientTo = null;
                        }
                        optBudgetedPatientTo = Convert.ToInt32(objectBenchmarkingFilter.BudgetedPatientTo);

                        //string startDate = null;
                        //string endDate = null;

                        strelectronicDocumentationFrom = objectBenchmarkingFilter.ElectronicDocumentationFrom.ToString();
                        if (strelectronicDocumentationFrom == string.Empty)
                        { strelectronicDocumentationFrom = "0"; }
                        electronicDocumentFrom = Convert.ToInt32(strelectronicDocumentationFrom);
                        if (electronicDocumentFrom == 0)
                        {
                            electronicDocumentFrom = null;
                        }
                        optElectronicDocumentFrom = Convert.ToInt32(objectBenchmarkingFilter.optElectronicDocumentationFrom);
                        strelectronicDocumentationTo = objectBenchmarkingFilter.ElectronicDocumentationTo.ToString();
                        if (strelectronicDocumentationTo == string.Empty)
                        { strelectronicDocumentationTo = "0"; }
                        electronicDocumentTo = Convert.ToInt32(strelectronicDocumentationTo);
                        if (electronicDocumentTo == 0)
                        {
                            electronicDocumentTo = null;
                        }
                        optElectronicDocumentTo = Convert.ToInt32(objectBenchmarkingFilter.optElectronicDocumentationTo);

                        docByException = Convert.ToInt32(objectBenchmarkingFilter.DocByException);

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

                        optHospitalSizeFrom = Convert.ToInt32(objectBenchmarkingFilter.optHospitalSizeFrom);
                        strhospitalSizeFrom = objectBenchmarkingFilter.HospitalSizeFrom.ToString();
                        if (strhospitalSizeFrom == string.Empty)
                        { strhospitalSizeFrom = "0"; }
                        hospitalSizeFrom = Convert.ToInt32(strhospitalSizeFrom);
                        if (hospitalSizeFrom == 0)
                        {
                            hospitalSizeFrom = null;
                        }
                        optHospitalSizeTo = Convert.ToInt32(objectBenchmarkingFilter.optHospitalSizeTo);
                        strhospitalSizeTo = objectBenchmarkingFilter.HospitalSizeTo.ToString();
                        if (strhospitalSizeTo == string.Empty)
                        { strhospitalSizeTo = "0"; }
                        hospitalSizeTo = Convert.ToInt32(strhospitalSizeTo);
                        if (hospitalSizeTo == 0)
                        {
                            hospitalSizeTo = null;
                        }

                        countryId = Convert.ToInt32(objectBenchmarkingFilter.CountryId);
                        if (countryId == 0)
                        {
                            countryId = null;
                        }
                        stateId = Convert.ToInt32(objectBenchmarkingFilter.StateId);
                        if (stateId == 0)
                        {
                            stateId = null;
                        }

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

                    string strvalue = string.Empty, strothers = string.Empty, strlocation = string.Empty, stractivities=string.Empty;
                    if (DropDownListProfileCategory.SelectedValue.Contains("Activities"))
                    {
                        activitiesCategory = DropDownListProfileCategory.SelectedValue.Replace(",Activities", "");
                        stractivities = DropDownListProfileCategory.SelectedItem.Text;
                    }
                    if (DropDownListProfileCategory.SelectedValue.Contains("Value Added"))
                    {
                        valueAddedCategory = DropDownListProfileCategory.SelectedValue.Replace(",Value Added", "");
                        strvalue = DropDownListProfileCategory.SelectedItem.Text;
                    }
                    if (DropDownListProfileCategory.SelectedValue.Contains("Others"))
                    {
                        othersCategory = DropDownListProfileCategory.SelectedValue.Replace(",Others", "");
                        strothers = DropDownListProfileCategory.SelectedItem.Text;
                    }
                    if (DropDownListProfileCategory.SelectedValue.Contains("Location"))
                    {
                        locationCategory = DropDownListProfileCategory.SelectedValue.Replace(",Location", "");
                        strlocation = DropDownListProfileCategory.SelectedItem.Text;
                        //location = DropDownListProfileCategory.SelectedValue.Replace(locationCategory + ",", "");
                    }

                    string ProfileCategoryValue;
                    string ProfileSubCategoryValue;

                    ProfileCategoryValue = DropDownListProfileCategory.SelectedItem.Text;
                    ProfileSubCategoryValue = DropDownListProfileSubCategory.SelectedItem.Text;

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

                    //------------
                    string activitiesID = activitiesCategory;
                    if (activitiesID == string.Empty)
                    {
                        activitiesID = null;
                    }
                    string valueAddedCategoryID = valueAddedCategory;
                    if (valueAddedCategoryID == string.Empty)
                    {
                        valueAddedCategoryID = null;
                    }

                    string OthersCategoryID = othersCategory;
                    if (OthersCategoryID == string.Empty)
                    {
                        OthersCategoryID = null;
                    }

                    string LocationCategoryID = locationCategory;
                    if (LocationCategoryID == string.Empty)
                    {
                        LocationCategoryID = null;
                    }

                    int? hospitalUnitID = Convert.ToInt32(DropDownListHospitalUnit.SelectedValue);
                    if (hospitalUnitID == 0)
                    {
                        hospitalUnitID = null;
                    }

                    string yearFrom = stryearFrom;
                    if (yearFrom == string.Empty)
                    { yearFrom = "0"; }
                    int? firstYear = Convert.ToInt32(yearFrom);
                    if (firstYear == 0)
                    {
                        firstYear = null;
                    }

                    string yearTo = stryearTo;
                    if (yearTo == string.Empty)
                    { yearTo = "0"; }
                    int? lastYear = Convert.ToInt32(yearTo);
                    if (lastYear == 0)
                    {
                        lastYear = null;
                    }

                    string monthFrom = strmonthFrom;
                    if (monthFrom == string.Empty)
                    { monthFrom = "0"; }
                    int? firstMonth = Convert.ToInt32(monthFrom);
                    if (firstMonth == 0)
                    {
                        firstMonth = null;
                    }

                    string monthTo = strmonthTo;
                    if (monthTo == string.Empty)
                    { monthTo = "0"; }
                    int? lastMonth = Convert.ToInt32(monthTo);
                    if (lastMonth == 0)
                    {
                        lastMonth = null;
                    }
                    string activities = stractivities;
                    string value = strvalue;
                    string others = strothers;
                    string location = strlocation;

                    string strdataPointsFrom = objectBenchmarkingFilter.DataPointsFrom.ToString();
                    if (strdataPointsFrom == string.Empty)
                    { strdataPointsFrom = "0"; }
                    int? dataPointsFrom = Convert.ToInt32(strdataPointsFrom);
                    if (dataPointsFrom == 0)
                    {
                        dataPointsFrom = null;
                    }
                    int optDataPointsFrom = Convert.ToInt32(objectBenchmarkingFilter.optDataPointsFrom);
                    string strdataPointsTo = objectBenchmarkingFilter.DataPointsTo.ToString();
                    if (strdataPointsTo == string.Empty)
                    { strdataPointsTo = "0"; }
                    int? dataPointsTo = Convert.ToInt32(strdataPointsTo);
                    if (dataPointsTo == 0)
                    {
                        dataPointsTo = null;
                    }
                    int optdataPointsTo = Convert.ToInt32(objectBenchmarkingFilter.optDataPointsTo);

                    string dbValues = string.Empty;
                    if (DropDownListValues.Visible == true)
                    {
                        dbValues = DropDownListValues.SelectedItem.Text;
                    }

                    objectGenericBEReports = objectBSReports.GetDataForLineChartModified(ProfileCategoryValue, ProfileSubCategoryValue,activitiesID, valueAddedCategoryID, OthersCategoryID, LocationCategoryID, hospitalUnitID, firstYear, lastYear, firstMonth, lastMonth, bedInUnitFrom, optBedsInUnitFrom, bedInUnitTo, optBedsInUnitTo, budgetedPatientFrom, optBudgetedPatientFrom, budgetedPatientTo, optBudgetedPatientTo, startDate, endDate, electronicDocumentFrom, optElectronicDocumentFrom, electronicDocumentTo, optElectronicDocumentTo, docByException, unitType, pharmacyType, hospitalType, optHospitalSizeFrom, hospitalSizeFrom, optHospitalSizeTo, hospitalSizeTo, countryId, stateId, activities, value, others, location, dataPointsFrom, optDataPointsFrom, dataPointsTo, optdataPointsTo, configName,unitIds, dbValues);

                }
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
                List<Series> objectGenericSeries = objectBSChartControl.GetLineChartSeries(DropDownListMarkerStyle.SelectedItem.Text, Convert.ToInt32(DropDownListMarkerSize.SelectedValue), DropDownListMarkerColor.SelectedItem.Text, DropDownListChartType.SelectedValue, Convert.ToInt32(DropDownListLineWidth.SelectedValue), DropDownListColor.SelectedItem.Text, Convert.ToInt32(DropDownListShadowOffset.SelectedValue), DropDownListPointLabel.SelectedItem.Text, objectGenericBEReports, DropDownListValues.SelectedItem.Text);

                foreach (Series seriesChart in objectGenericSeries)
                {
                    ChartControlCharts.Series.Add(seriesChart);
                }
                if (objectGenericSeries.Count == 0)
                {
                    ChartControlCharts.Titles["Title1"].Text = "No Data to Display";
                }
                else
                {
                    //ChartControlCharts.Titles["Title1"].Text = "#" + Request.QueryString["hospitalName"] + "   /   " + Request.QueryString["hospitalUnitName"] + "   /   " + Request.QueryString["ProfileSubCategory"] + " (" + Request.QueryString["ProfileCategory"] + ")";
                    ChartControlCharts.Titles["Title1"].Text = DropDownListHospitalName.SelectedItem.Text + "   /   " + DropDownListHospitalUnit.SelectedItem.Text + "   /   " + DropDownListProfileSubCategory.SelectedItem.Text + " (" + DropDownListProfileCategory.SelectedItem.Text + ")";
                }


                if (System.IO.File.Exists("~\\Uploads\\ChartImg" + CommonClass.UserInformation.UserID.ToString() + ".png"))
                {
                    System.IO.File.Delete("~\\Uploads\\ChartImg" + CommonClass.UserInformation.UserID.ToString() + ".png");
                }
                ChartControlCharts.SaveImage(Server.MapPath("~\\Uploads\\ChartImg" + CommonClass.UserInformation.UserID.ToString() + ".png"));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        protected void DropDownListHospitalUnit_SelectedIndexChanged(object sender, EventArgs e)
        {
            GetYearMonth();
        }

    }
}