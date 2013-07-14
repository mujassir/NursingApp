using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using RMC.Web;
using LogExceptions;
using RMC.BussinessService;
using System.Data;
using System.Collections;
using System.IO;
using System.Web.UI.HtmlControls;

namespace RMC.Web.UserControls
{
    public partial class UnitAssessmentReport : System.Web.UI.UserControl
    {
        RMC.BussinessService.BSHospitalInfo _objectBSHospitalInfo = null;
        BSHospitalDemographicDetail _objectBSHospitalDemgraphicDetail = null;
        List<RMC.BusinessEntities.BEHospitalList> _objectGenericBEHospitalList = null;

      

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

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (Page.IsPostBack != true)
                {
                    if (Request.QueryString["filter"] != null)
                    {
                       
                    }
                }
                
                
                LabelHeading.Text = "Unit Assessment";
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
                    //GetYearMonth();
                    //
                    if (Request.QueryString["filter"] != null)
                    {
                        //RMC.BussinessService.BSYear objectBSYear = new RMC.BussinessService.BSYear();
                        //Int32 hospitalid = objectBSYear.getHospitalId(Convert.ToInt32(Request.QueryString["hospitalUnitId"]));

                        //string filter = Request.QueryString["filter"];
                        //string year = Request.QueryString["year"];
                        //string month = Request.QueryString["month"];
                        //string hospitalunitid = Request.QueryString["hospitalUnitID"];
                        //string ProfileCategoryValue = Request.QueryString["ProfileCategoryValue"];
                        //string ProfileSubCategoryValue = Request.QueryString["ProfileSubCategoryValue"];

                       
                       
                        //DropDownListBenchmarkingFilter.DataBind();
                        //DropDownListBenchmarkingFilter.SelectedItem.Selected = false;
                        //DropDownListBenchmarkingFilter.Items.FindByText(filter).Selected = true;


                        //DropDownListHospitalName.SelectedValue = Convert.ToString(hospitalid);
                        //DropDownListYear.SelectedValue = year;
                        //DropDownListHospitalUnit.SelectedValue = hospitalunitid;
                        //DropDownListMonth.Items.Clear();
                        //DropDownListMonth.Items.Add(new ListItem("Select Month", "0"));
                       
                        //DropDownListYear.SelectedValue = year;
                        //DropDownListMonth.SelectedValue = month;               
                        //DropDownListProfileCategory.SelectedValue = ProfileCategoryValue;                        
                        //DropDownListProfiles.SelectedItem.Text = ProfileSubCategoryValue;
                       
                        
                    }
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

        protected void DropDownListProfileCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                DropDownListValues.Items.Clear();
                DropDownListValues.Items.Add("Select Value");
                DropDownListValues.Items[0].Value = 0.ToString();
                DropDownListValues.Visible = false;
                LabelValues.Visible = false;

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
                GetYearMonth();
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

            LinkButton3.Visible = true;
            Chart1.Visible = true;
            LinkButton2.Visible = true;

            int? benchmarkFilterId; int? year = null; int? month = null; int? hospitalInfoId; int? hospitalDemographicId; //int? profileTypeId;
            string profileTypeId;
            string profileCategory;
            string filterText;
            string yearMonthFromL = null;
            string yearMonthToL = null;

            string yearFrom = null, yearTo = null, monthFrom = null, monthTo = null;
            if (DropDownListYearMonthFrom.SelectedValue != "0")
            {
                yearMonthFromL = DropDownListYearMonthFrom.SelectedItem.ToString();
                string yearMonthFrom = DropDownListYearMonthFrom.SelectedValue;
                string[] yearMonthFromArr = yearMonthFrom.Split(new char[] { ',' });
                yearFrom = yearMonthFromArr[1].ToString();
                monthFrom = yearMonthFromArr[0].ToString();
            }
            if (DropDownListYearMonthTo.SelectedValue != "0")
            {
                yearMonthToL = DropDownListYearMonthTo.SelectedItem.ToString();
                string yearMonthTo = DropDownListYearMonthTo.SelectedValue;
                string[] yearMonthToArr = yearMonthTo.Split(new char[] { ',' });
                yearTo = yearMonthToArr[1].ToString();
                monthTo = yearMonthToArr[0].ToString();
            }
            if (Convert.ToInt32(yearFrom) > Convert.ToInt32(yearTo) || (Convert.ToInt32(yearFrom) == Convert.ToInt32(yearTo) && Convert.ToInt32(monthFrom) > Convert.ToInt32(monthTo)))
            {
                
                if (Convert.ToInt32(yearFrom) > Convert.ToInt32(yearTo) || (Convert.ToInt32(yearFrom) == Convert.ToInt32(yearTo) && Convert.ToInt32(monthFrom) > Convert.ToInt32(monthTo)))
                {
                    CommonClass.Show("PeriodTo Cannot Be Less Than PeriodFrom");
                }
            }
            else
            {
                RMC.DataService.BenchmarkFilter objectBenchmarkingFilter = new RMC.DataService.BenchmarkFilter();
                RMC.BussinessService.BSReports objectBSReports = new RMC.BussinessService.BSReports();

                if (DropDownListBenchmarkingFilter.SelectedIndex > 0)
                {
                    benchmarkFilterId = Convert.ToInt32(DropDownListBenchmarkingFilter.SelectedValue);
                    filterText = Convert.ToString(DropDownListBenchmarkingFilter.SelectedValue);
                }
                else
                {
                    benchmarkFilterId = null;
                    filterText = null;
                }

                if (DropDownListHospitalName.SelectedIndex > 0)
                {
                    hospitalInfoId = Convert.ToInt32(DropDownListHospitalName.SelectedValue);
                }
                else
                {
                    hospitalInfoId = null;
                }
                if (DropDownListHospitalUnit.SelectedIndex > 0)
                {
                    hospitalDemographicId = Convert.ToInt32(DropDownListHospitalUnit.SelectedValue);
                }
                else
                {
                    hospitalDemographicId = null;
                }
                //if(DropDownListYear.SelectedIndex>0)
                //{
                //    year = Convert.ToInt32(DropDownListYear.SelectedValue);
                //}
                //else
                //{
                //    year = null;
                //}

                //if(DropDownListMonth.SelectedIndex > 0)
                //{
                //    month = Convert.ToInt32(DropDownListMonth.SelectedValue);
                //}
                //else
                //{
                //    month = null;
                //}

                if (DropDownListProfileCategory.SelectedIndex > 0)
                {
                    profileCategory = DropDownListProfileCategory.SelectedValue;
                }
                else
                {
                    profileCategory = null;
                }

                if (DropDownListProfiles.SelectedIndex > 0)
                {
                    // profileTypeId = Convert.ToInt32(DropDownListProfiles.SelectedValue);
                    profileTypeId = DropDownListProfiles.SelectedValue;
                }
                else
                {
                    profileTypeId = null;
                }
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
                   // hospitalType = DropDownListHospitalName.SelectedValue;
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

                string ProfileCategoryValue;
                string ProfileSubCategoryValue;

                ProfileCategoryValue = DropDownListProfileCategory.SelectedItem.Text;
                ProfileSubCategoryValue = DropDownListProfiles.SelectedItem.Text;
                string valueAddedCategory = null;
                string otherCategory = null;
                string locatinonCategory = null;
                string activitiesID = null;
                if (profileCategory.ToLower() == "others")
                {
                    otherCategory = profileTypeId.ToString();
                }
                if (profileCategory.ToLower() == "value added")
                {
                    valueAddedCategory = profileTypeId.ToString();
                }
                if (profileCategory.ToLower() == "location")
                {
                    locatinonCategory = profileTypeId.ToString();
                }
                if (profileCategory.ToLower() == "activities")
                {
                    locatinonCategory = profileTypeId.ToString();
                }
                string filter = DropDownListBenchmarkingFilter.SelectedItem.Text;
                if (DropDownListBenchmarkingFilter.SelectedIndex == 0)
                {
                    filter = "No Filter";
                }
                string dbValues = string.Empty;
                if (DropDownListValues.Visible == true)
                {
                    dbValues = DropDownListValues.SelectedItem.Text;
                }

                string monthname = yearMonthFromL;
                string hospitalUnitId = DropDownListHospitalUnit.SelectedValue;

                //string redirectUrl = "UserAssessment.aspx?dbValues=" + dbValues + "&yearMonthFrom=" + yearMonthFromL + "&yearMonthTo=" + yearMonthToL + "&ProfileCategoryValue=" + ProfileCategoryValue + "&ProfileSubCategoryValue=" + ProfileSubCategoryValue + "&valueAddedCategoryID=" + valueAddedCategory + "&OthersCategoryID=" + otherCategory + "&LocationCategoryID=" + locatinonCategory + "&hospitalUnitID=" + hospitalUnitId + "&yearFrom=" + yearFrom + "&yearTo=" + yearTo + "&monthFrom=" + monthFrom + "&monthTo=" + monthTo + "&BedsInUnitFrom=" + bedsInUnitFrom + "&optBedsInUnitFrom=" + optBedsInUnitFrom + "&bedsInUnitTo=" + bedsInUnitTo + "&optBedsInUnitTo=" + optBedsInUnitTo + "&budgetedPatientFrom=" + budgetedPatientFrom + "&optBudgetedPatientFrom=" + optBudgetedPatientFrom + "&budgetedPatientTo=" + budgetedPatientTo + "&optBudgetedPatientTo=" + optBudgetedPatientTo + "&electronicDocumentationFrom=" + electronicDocumentationFrom + "&optElectronicDocumentationFrom=" + optElectronicDocumentationFrom + "&electronicDocumentationTo=" + electronicDocumentationTo + "&optElectronicDocumentationTo=" + optElectronicDocumentationTo + "&docByException=" + docByException + "&unitType=" + unitType + "&pharmacyType=" + pharmacyType + "&hospitalType=" + hospitalType + "&hospitalSizeFrom=" + hospitalSizeFrom + "&optHospitalSizeFrom=" + optHospitalSizeFrom + "&hospitalSizeTo=" + hospitalSizeTo + "&optHospitalSizeTo=" + optHospitalSizeTo + "&countryID=" + countryID + "&stateID=" + stateID + "&activities=" + profileCategory + "&value=" + profileCategory + "&others=" + profileCategory + "&location=" + profileCategory + "&filter=" + filter + "&beginingDate=" + year + "&endingDate=" + year + "&DataPointsFrom=" + dataPointsFrom + "&optDataPointsFrom=" + optDataPointsFrom + "&DataPointsTo=" + dataPointsTo + "&optDataPointsTo=" + optDataPointsTo + "&configName=" + configName;
                //Response.Redirect(redirectUrl);
                string startDate = null;
                string endDate = null;
                getDataforGrid(dbValues, ProfileCategoryValue, ProfileSubCategoryValue, activitiesID, valueAddedCategory, otherCategory, locatinonCategory, hospitalUnitId, yearFrom, yearTo, monthFrom, monthTo, bedsInUnitFrom, optBedsInUnitFrom, bedsInUnitTo, optBedsInUnitTo, budgetedPatientFrom, optBudgetedPatientFrom, budgetedPatientTo, optBudgetedPatientTo, startDate, endDate, electronicDocumentationFrom, optElectronicDocumentationFrom, electronicDocumentationTo, optElectronicDocumentationTo, docByException, unitType, pharmacyType, hospitalType, hospitalSizeFrom, optHospitalSizeFrom, hospitalSizeTo, optHospitalSizeTo, countryID, stateID, profileCategory, profileCategory, profileCategory, profileCategory, dataPointsFrom, optDataPointsFrom, dataPointsTo, optDataPointsTo, configName, unitIds);
            }
        }

        protected void DropDownListProfiles_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (DropDownListProfileCategory.SelectedItem.Text == "Database Values" || DropDownListProfileCategory.SelectedItem.Text == "Special Category")
                {
                    //LabelValues.Visible = true;
                    //DropDownListValues.Visible = true;
                    //DropDownListValues.Items.Clear();
                    //DropDownListValues.Items.Add("Select Value");
                    //DropDownListValues.Items[0].Value = 0.ToString();
                    //DropDownListValues.DataBind();
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

        protected void ObjectDataSourceBenchmarkingFilterNames_Selected(object sender, ObjectDataSourceStatusEventArgs e)
        {
            //if (Request.QueryString["filter"] != null)
            //{
            //    string filter = Request.QueryString["filter"];
            //    for (int i = 0; i < DropDownListBenchmarkingFilter.Items.Count; i++)
            //    {
            //        if (DropDownListBenchmarkingFilter.Items[i].Text == filter)
            //        {
            //            DropDownListBenchmarkingFilter.SelectedIndex = i;
            //        }

            //    }
            //}
           
        }

        protected void DropDownListYear_DataBinding(object sender, EventArgs e)
        {
           // DropDownListYear.Items.Clear();
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
        //add by cm
        private void getDataforGrid(string dbValues1, string ProfileCategoryValues, string ProfileSubCategoryValues, string activitiesIDs, string valueAddedCategoryIDs, string OthersCategoryIDs, string LocationCategoryIDs, string hospitalUnitIDs, string yearFrom, string yearTo, string monthFrom, string monthTo, string BedsInUnitFrom, string optBedsInUnitFrom, string bedsInUnitTo, string optBedsInUnitTo, string budgetedPatientFrom1, string optBudgetedPatientFrom1, string budgetedPatientTo1, string optBudgetedPatientTo1, string startDate1, string endDate1, string electronicDocumentationFrom, string optElectronicDocumentationFrom, string electronicDocumentationTo, string optElectronicDocumentationTo, string docByException1, string unitType1, string pharmacyType1, string hospitalType1, string hospitalSizeFrom1, string optHospitalSizeFrom1, string hospitalSizeTo1, string optHospitalSizeTo1, string countryId1, string stateId1, string activities1, string value1, string others1, string location1, string dataPointsFrom1, string optDataPointsFrom1, string dataPointsTo1, string optDataPointsTo1, string configName1, string unitIds1)
        {
            RMC.BussinessService.BSReports objectBEReports = new BSReports();

            string dbValues = dbValues1;
            if (dbValues == string.Empty)
            {
                dbValues = null;
            }
           

            string ProfileSubCategoryValue = ProfileSubCategoryValues;
            if (ProfileSubCategoryValue == string.Empty)
            {
                ProfileSubCategoryValue = null;
            }
            

            string ProfileCategoryValue = ProfileCategoryValues;
            if (ProfileCategoryValue == string.Empty)
            {
                ProfileCategoryValue = null;
            }
            

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
            if (yearFrom != null)
            {
                firstYear = Convert.ToInt32(yearFrom);
            }

            int? lastYear = null;
            if (yearTo != null)
            {
                lastYear = Convert.ToInt32(yearTo);
            }

            int? firstMonth = null;
            if (monthFrom != null)
            {
                firstMonth = Convert.ToInt32(monthFrom);
            }

            int? lastMonth = null;
            if (monthTo != null)
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

            DataTable DTCalculateFunctionValuesGrid = new DataTable();
            DTCalculateFunctionValuesGrid = objectBEReports.CalculateFunctionValuesGridForUnitAssessment(dbValues, ProfileCategoryValue, ProfileSubCategoryValue, valueAddedCategoryID, OthersCategoryID, LocationCategoryID, null, firstYear, lastYear, firstMonth, lastMonth, bedInUnitFrom, optBedInUnitFrom, bedInUnitTo, optBedInUnitTo, budgetedPatientFrom, optBudgetedPatientFrom, budgetedPatientTo, optBudgetedPatientTo, startDate, endDate, electronicDocumentFrom, optElectronicDocumentFrom, electronicDocumentTo, optElectronicDocumentTo, docByException, unitType, pharmacyType, hospitalType, optHospitalSizeFrom, hospitalSizeFrom, optHospitalSizeTo, hospitalSizeTo, countryId, stateId, activities, value, others, location, dataPointsFrom, optDataPointsFrom, dataPointsTo, optDataPointsTo, configName ,UnitIds);
            GridViewFunctionReport.DataSource = DTCalculateFunctionValuesGrid;
            GridViewFunctionReport.DataBind();

            DataTable DTGetDataForTimeRNSummaryGrid = new DataTable();
            DTGetDataForTimeRNSummaryGrid = objectBEReports.GetDataForHospitalBenchmarkSummaryGridUnitID(dbValues, ProfileCategoryValue, ProfileSubCategoryValue, valueAddedCategoryID, OthersCategoryID, LocationCategoryID, hospitalUnitId, firstYear, lastYear, firstMonth, lastMonth, bedInUnitFrom, optBedInUnitFrom, bedInUnitTo, optBedInUnitTo, budgetedPatientFrom, optBudgetedPatientFrom, budgetedPatientTo, optBudgetedPatientTo, startDate, endDate, electronicDocumentFrom, optElectronicDocumentFrom, electronicDocumentTo, optElectronicDocumentTo, docByException, unitType, pharmacyType, hospitalType, optHospitalSizeFrom, hospitalSizeFrom, optHospitalSizeTo, hospitalSizeTo, countryId, stateId, activities, value, others, location, dataPointsFrom, optDataPointsFrom, dataPointsTo, optDataPointsTo, configName,UnitIds);
            GridViewReport.DataSource = DTGetDataForTimeRNSummaryGrid;
            GridViewReport.DataBind();

            DataTable DTCalculateData = new DataTable();
            DTCalculateData = objectBEReports.CalculateData();
            GridViewtest.DataSource = DTCalculateData;
            GridViewtest.DataBind();
            showchart();
            
        }
        private void showchart()
        {

            DataTable dt = new DataTable();
            DataTable dtTopQuritile = (DataTable)Session["dtTopQuritile"];
            DataTable dtresult = (DataTable)Session["dtresult"];
            ///
            DataRow dr0 = dtresult.NewRow();
            DataRow dr1 = dtresult.NewRow();
            int col = dtresult.Columns.Count;
            int col1 = dtTopQuritile.Columns.Count;

            int k = 0;
            //merge table dtTopQuritile and dtresult, rows are actual , topQ, median
            for (int i = 0; i < col; i++)
            {

                for (int j = 1; j < col1; j++)
                {
                    if (dtresult.Columns[i].ToString() == dtTopQuritile.Columns[j].ToString())
                    {

                        // dtresult.Columns.Add(dtTopQuritile.Columns[j].ColumnName.ToString());
                        dr0[k] = (Convert.ToDecimal(dtTopQuritile.Rows[0][j]));
                        dr1[k] = (Convert.ToDecimal(dtTopQuritile.Rows[1][j]));
                        k += 1;


                    }
                }
            }
            dtresult.Rows.Add(dr0);
            dtresult.Rows.Add(dr1);
            // remove nagitive value from Qtr(1)
            for (int row = 1; row < dtresult.Rows.Count; row++)
            {
                for (int colm = 0; colm < dtresult.Columns.Count; colm++)
                {
                    //if (Math.Round(Convert.ToDouble(dtresult.Rows[row][colm]), 2) <= 0.00)
                    if (Math.Round(Convert.ToDouble(dtresult.Rows[0][colm]) - Convert.ToDouble(dtresult.Rows[row][colm]), 2) < 0.00)
                    {
                        dtresult.Columns.Remove(dtresult.Columns[colm]);
                        colm = colm - 1;
                        //dtresult.Rows[row][colm] = 0;
                    }
                }
            }
            //sorting function by cm
            ArrayList arrlist = new ArrayList();
            ArrayList arrlist1 = new ArrayList();
            Hashtable hs = new Hashtable();

            for (int i = 0; i < dtresult.Columns.Count; i++)
            {
                // hs.Add(dtresult.Columns[i].ColumnName.ToString(), dtresult.Rows[1][i].ToString());
                arrlist.Insert(i, (Convert.ToDouble(dtresult.Rows[0][i]) - Convert.ToDouble(dtresult.Rows[1][i])));
                arrlist1.Insert(i, (Convert.ToDouble(dtresult.Rows[0][i]) - Convert.ToDouble(dtresult.Rows[1][i])));
            }
            // arrlist = new ArrayList(hs.Values);
            // arrlist.Add();
            arrlist.Sort();
            arrlist.Reverse();
            DataTable dtSorted = new DataTable();
            DataRow drs0 = dtSorted.NewRow();
            DataRow drs11 = dtSorted.NewRow();
            DataRow drs2 = dtSorted.NewRow();
            for (int i = 0; i < arrlist.Count; i++)
            {
                for (int i1 = 0; i1 < arrlist1.Count; i1++)
                {
                    if (arrlist1[i1].ToString() == arrlist[i].ToString())
                    {
                        string col22 = dtresult.Columns[i1].ColumnName.ToString();
                        dtSorted.Columns.Add(col22);
                        //arrlist1.Insert(i1, 0);
                        arrlist1[i1] = "-1";
                        break;
                    }
                }
            }

            //for (int y = 0; y < 3; y++)
            //{
            for (int i = 0; i < dtSorted.Columns.Count; i++)
            {
                for (int x = 0; x < dtresult.Columns.Count; x++)
                {
                    if (dtSorted.Columns[i].ColumnName.ToUpper() == dtresult.Columns[x].ColumnName.ToUpper())
                    {
                        drs0[i] = dtresult.Rows[0][x].ToString();
                        drs11[i] = dtresult.Rows[1][x].ToString();
                        drs2[i] = dtresult.Rows[2][x].ToString();
                    }
                }
            }
            dtSorted.Rows.Add(drs0);
            dtSorted.Rows.Add(drs11);
            dtSorted.Rows.Add(drs2);
            //end by cm



            for (int colm = 0; colm < dtSorted.Columns.Count; colm++)
            {
                dtSorted.Rows[0][colm] = Convert.ToDouble(dtSorted.Rows[0][colm]) - Convert.ToDouble(dtSorted.Rows[2][colm]);
                dtSorted.Rows[2][colm] = Convert.ToDouble(dtSorted.Rows[2][colm]) - Convert.ToDouble(dtSorted.Rows[1][colm]);
            }
            ////
            // dt = (DataTable)HttpContext.Current.Session["dtTopQuritile"];
            for (int colm = 0; colm < dtSorted.Columns.Count; colm++)
            {
                string colname = null;
                colname = dtSorted.Columns[colm].ColumnName.ToString();
                string[] strarr = colname.Split(new Char[] { '(' });
                if (colname.Contains("("))
                {
                    dtSorted.Columns[colm].ColumnName = strarr[0] + "(" + strarr[1];
                }
            }


            //}

            var a = dt; Random random = new Random();
            for (int i = 0; i < dtSorted.Columns.Count; i++)
            {
                Chart1.Series["Actual"].Points.Add(Convert.ToDouble(dtSorted.Rows[0][i].ToString()));
                Chart1.Series["Median"].Points.Add(Convert.ToDouble(dtSorted.Rows[2][i].ToString()));
                Chart1.Series["Top Quartile"].Points.Add(Convert.ToDouble(dtSorted.Rows[1][i].ToString()));

                Chart1.Series["Actual"].Points[i].AxisLabel = dtSorted.Columns[i].ToString();
                Chart1.Series["Median"].Points[i].AxisLabel = dtSorted.Columns[i].ToString();
                Chart1.Series["Top Quartile"].Points[i].AxisLabel = dtSorted.Columns[i].ToString();

            }
            //Chart1.Series["Actual"].IsValueShownAsLabel = true;
            //Chart1.Series["Median"].IsValueShownAsLabel = true;
            //Chart1.Series["Top Quartial"].IsValueShownAsLabel = true;
            //Chart1.Series["Actual"]["BarLabelStyle"] = "Outside";

            /////
            // Set Legend's visual attributes
            // Chart1.Legends["Default"].BackColor = Color.MediumSeaGreen;
            // Chart1.Legends["Default"].BackSecondaryColor = Color.Green;
            // Chart1.Legends["Default"].BackGradientStyle = GradientStyle.DiagonalLeft;

            // Chart1.Legends["Default"].BorderColor = Color.Black;
            //Chart1.Legends["Default"].BorderWidth = 2;
            // Chart1.Legends["Default"].BorderDashStyle = ChartDashStyle.Solid;

            // Chart1.Legends["Default"].ShadowOffset = 2;
            ////
            for (int colm = 0; colm < dtSorted.Columns.Count; colm++)
            {
                dtSorted.Rows[2][colm] = Convert.ToDouble(dtSorted.Rows[2][colm]) + Convert.ToDouble(dtSorted.Rows[1][colm]);
                dtSorted.Rows[0][colm] = Convert.ToDouble(dtSorted.Rows[0][colm]) + Convert.ToDouble(dtSorted.Rows[2][colm]);
            }

            DataTable dtGridData = new DataTable();
            dtGridData.Columns.Add(".");
            for (int colm = 0; colm < dtSorted.Columns.Count; colm++)
            {
                dtGridData.Columns.Add(dtSorted.Columns[colm].ColumnName.ToString());
            }
            dtGridData.Columns.Add("Total Number of Minutes");
            dtGridData.Columns.Add("% Productivity Improvement");
            int k1 = 1;
            DataRow drs = dtGridData.NewRow();
            DataRow drs1 = dtGridData.NewRow();
            drs[0] = "Top Quartile ";
            drs1[0] = "Median";
            for (int i = 1; i < dtGridData.Columns.Count; i++)
            {
                for (int j = 0; j < dtSorted.Columns.Count; j++)
                {
                    if (dtSorted.Columns[j].ToString() == dtGridData.Columns[i].ToString())
                    {
                        // drs[k1] = (Convert.ToDecimal(dtresult.Rows[1][j]));
                        // drs1[k1] = (Convert.ToDecimal(dtresult.Rows[2][j]));
                        drs[k1] = Math.Round(Convert.ToDecimal(dtSorted.Rows[0][j]) - Convert.ToDecimal(dtSorted.Rows[1][j]), 2);
                        drs1[k1] = Math.Round(Convert.ToDecimal(dtSorted.Rows[0][j]) - Convert.ToDecimal(dtSorted.Rows[2][j]), 2);
                        k1 += 1;


                    }
                }
            }
            dtGridData.Rows.Add(drs);
            dtGridData.Rows.Add(drs1);
            decimal val = 0;
            decimal val2 = 0;
            int c = 0;
            for (int i = 1; i < dtGridData.Columns.Count - 2; i++)
            {
                val = val + Convert.ToDecimal(dtGridData.Rows[0][i]);
                val2 = val2 + Convert.ToDecimal(dtGridData.Rows[1][i]);
                c = i + 1;
            }
            dtGridData.Rows[0][c] = val;
            dtGridData.Rows[1][c] = val2;
            dtGridData.Rows[0][c + 1] = Math.Round((val / 720) * 100, 2);
            dtGridData.Rows[1][c + 1] = Math.Round((val2 / 720) * 100, 2);
            GridView1.DataSource = dtGridData;
            GridView1.DataBind();
            //seve image of chart
            if (System.IO.File.Exists("~\\Uploads\\ChartImg" + CommonClass.UserInformation.UserID.ToString() + ".png"))
            {
                System.IO.File.Delete("~\\Uploads\\ChartImg" + CommonClass.UserInformation.UserID.ToString() + ".png");
            }
            Chart1.SaveImage(Server.MapPath("~\\Uploads\\ChartImg" + CommonClass.UserInformation.UserID.ToString() + ".png"));
        }
        protected void LinkButton2_Click(object sender, EventArgs e)
        {
            //**code for export image to excel**//
            //string OutFilename = "Chart" + Guid.NewGuid();
            //string Chartpath = Server.MapPath("Excel") + "/ChartImg1.png" ;           
            //string InputExcelfilepath = Server.MapPath("Excel") + "/myExcelFile.xls";
            //string OutputExcelfilepath = Server.MapPath("Excel") + "/" + OutFilename + ".xls";            
            //XLExport.XLExport.ExportFromTemplate(Chartpath, InputExcelfilepath, OutputExcelfilepath, "B2");

            ExportGridView();
        }
        private void ExportGridView()
        {

            string attachment = "attachment; filename=testReport.xls";

            Response.ClearContent();

            Response.AddHeader("content-disposition", attachment);

            Response.ContentType = "application/ms-excel";

            StringWriter sw = new StringWriter();

            HtmlTextWriter htw = new HtmlTextWriter(sw);



            // Create a form to contain the grid

            HtmlForm frm = new HtmlForm();
            // GridViewNPOlist .BottomPagerRow.Visible = false;
            GridView1.Parent.Controls.Add(frm);

            frm.Attributes["runat"] = "server";

            frm.Controls.Add(GridView1);



            frm.RenderControl(htw);

            //GridView1.RenderControl(htw);

            Response.Write(sw.ToString());
            //Response.Write(grd1);
            Response.End();
            // GridViewNPOlist.BottomPagerRow.Visible = false;

        }
        protected void LinkButton3_Click(object sender, EventArgs e)
        {
            try
            {
                Response.Redirect("~/Administrator/SaveImageAtClientSide.aspx", false);
            }
            catch (Exception ex)
            {
                LogManager._stringObject = "ReportTimeRNChartsPie.ascx ----";
                LogManager.SetExceptionDetails(ex, LogManager._stringObject, null);
                LogManager.LogException(ex, LogManager.LoggingCategory.General, LogManager.LoggingLevel.Error);
                CommonClass.Show(LogManager.ShowErrorDetail(ex));
            }
        }

    }
}