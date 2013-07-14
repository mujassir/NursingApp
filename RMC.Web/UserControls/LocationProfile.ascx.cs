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
using Microsoft.Reporting.WebForms;

namespace RMC.Web.UserControls
{
    public partial class WebUserControl1 : System.Web.UI.UserControl
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
                if (!Page.IsPostBack)
                {
                    if (!(HttpContext.Current.User.IsInRole("superadmin")))
                    {
                        LabelBenchmarkingFilters.Visible = false;
                        DropDownListBenchmarkingFilter.Visible = false;

                        if (!CommonClass.UserInformation.IsActive)
                        {
                          // Commented for Location Profile 
                          // 14/12/2010
                          // Response.Redirect("~/Users/InActiveUser.aspx", false);
                        }
                    }
                    else
                    {
                        LabelBenchmarkingFilters.Visible = true;
                        DropDownListBenchmarkingFilter.Visible = true;
                    }
                    int userID = CommonClass.UserInformation.UserID;
                    GetAllHospitalNames(userID);
                    //GetAllAvailableProfiles(userID);
                    GetYearMonth();
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

        protected void DropDownListHospitalName_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (DropDownListHospitalName.SelectedIndex != 0)
                {
                    DropDownListBenchmarkingFilter.Enabled = false;
                }
                else
                {
                    DropDownListBenchmarkingFilter.Enabled = true;
                }
                DropDownListHospitalUnit.Items.Clear();
                DropDownListHospitalUnit.Items.Add(new ListItem("Select Hospital Unit", "0"));
                DropDownListHospitalUnit.DataBind();

            }
            catch (Exception ex)
            {
                LogManager._stringObject = "UserNotification.ascx ---- Page_Load";
                LogManager.SetExceptionDetails(ex, LogManager._stringObject, null);
                LogManager.LogException(ex, LogManager.LoggingCategory.General, LogManager.LoggingLevel.Error);
                CommonClass.Show(LogManager.ShowErrorDetail(ex));
            }
        }

        protected void DropDownListHospitalUnit_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {

            }
            catch (Exception ex)
            {
                LogManager._stringObject = "UserNotification.ascx ---- Page_Load";
                LogManager.SetExceptionDetails(ex, LogManager._stringObject, null);
                LogManager.LogException(ex, LogManager.LoggingCategory.General, LogManager.LoggingLevel.Error);
                CommonClass.Show(LogManager.ShowErrorDetail(ex));
            }
        }

        protected void DropDownListBenchmarkingFilter_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (DropDownListBenchmarkingFilter.SelectedValue != "0")
                {
                    DropDownListHospitalName.Enabled = false;
                    DropDownListHospitalUnit.Enabled = false;
                }
                else
                {
                    DropDownListHospitalName.Enabled = true;
                    DropDownListHospitalUnit.Enabled = true;
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

        protected void DropDownListYearMonthFrom_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {

            }
            catch (Exception ex)
            {
                LogManager._stringObject = "UserNotification.ascx ---- Page_Load";
                LogManager.SetExceptionDetails(ex, LogManager._stringObject, null);
                LogManager.LogException(ex, LogManager.LoggingCategory.General, LogManager.LoggingLevel.Error);
                CommonClass.Show(LogManager.ShowErrorDetail(ex));
            }
        }

        protected void DropDownListYearMonthTo_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {

            }
            catch (Exception ex)
            {
                LogManager._stringObject = "UserNotification.ascx ---- Page_Load";
                LogManager.SetExceptionDetails(ex, LogManager._stringObject, null);
                LogManager.LogException(ex, LogManager.LoggingCategory.General, LogManager.LoggingLevel.Error);
                CommonClass.Show(LogManager.ShowErrorDetail(ex));
            }
        }

        protected void ButtonGenerateReport_Click(object sender, EventArgs e)
        {
            try
            {
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
                RMC.DataService.BenchmarkFilter objectBenchmarkingFilter = new RMC.DataService.BenchmarkFilter();
                RMC.BussinessService.BSReports objectBSReports = new RMC.BussinessService.BSReports();
                string bedsInUnitFrom, optBedsInUnitFrom, bedsInUnitTo, optBedsInUnitTo, budgetedPatientFrom, optBudgetedPatientFrom, budgetedPatientTo, optBudgetedPatientTo, electronicDocumentationFrom, optElectronicDocumentationFrom, electronicDocumentationTo, optElectronicDocumentationTo, docByException, unitType, pharmacyType, hospitalType, hospitalSizeFrom, optHospitalSizeFrom, hospitalSizeTo, optHospitalSizeTo, countryID, stateID, dataPointsFrom, optDataPointsFrom, dataPointsTo, optDataPointsTo, hospitalUnitID, configName;
                if (DropDownListBenchmarkingFilter.SelectedValue == "0" && DropDownListHospitalName.SelectedIndex == 0)
                {
                    CommonClass.Show("Please Select Either Benchmarking Filter Or Hospital");
                }
                else
                {
                    if (DropDownListBenchmarkingFilter.SelectedIndex == 0)
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
                        hospitalUnitID = DropDownListHospitalUnit.SelectedValue;
                        configName = null;
                    }
                    else
                    {
                        if (DropDownListBenchmarkingFilter.SelectedItem.Text == "No Filter")
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
                            hospitalUnitID = null;
                            configName = null;
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

                            //hospitalUnitID = DropDownListHospitalUnit.SelectedValue;
                            hospitalUnitID = null;

                            configName = objectBenchmarkingFilter.ConfigurationName.ToString();
                            if (configName == "0")
                            {
                                configName = null;
                            }
                        }
                    }
                    string filterName = string.Empty, hospitalName = string.Empty, hospitalUnit = string.Empty;
                    if (DropDownListBenchmarkingFilter.Enabled == true)
                    { filterName = DropDownListBenchmarkingFilter.SelectedItem.Text; }
                    if (DropDownListHospitalName.Enabled == true)
                    { hospitalName = DropDownListHospitalName.SelectedItem.Text.Remove(0, 1); }
                    if (DropDownListHospitalUnit.Enabled == true)
                    { hospitalUnit = DropDownListHospitalUnit.SelectedItem.Text;}

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

                    //string url = "ReportLocationProfile.aspx?yearFrom=" + yearFrom + "&yearTo=" + yearTo + "&monthFrom=" + monthFrom + "&monthTo=" + monthTo + "&BedsInUnitFrom=" + bedsInUnitFrom + "&optBedsInUnitFrom=" + optBedsInUnitFrom + "&bedsInUnitTo=" + bedsInUnitTo + "&optBedsInUnitTo=" + optBedsInUnitTo + "&budgetedPatientFrom=" + budgetedPatientFrom + "&optBudgetedPatientFrom=" + optBudgetedPatientFrom + "&budgetedPatientTo=" + budgetedPatientTo + "&optBudgetedPatientTo=" + optBudgetedPatientTo + "&electronicDocumentationFrom=" + electronicDocumentationFrom + "&optElectronicDocumentationFrom=" + optElectronicDocumentationFrom + "&electronicDocumentationTo=" + electronicDocumentationTo + "&optElectronicDocumentationTo=" + optElectronicDocumentationTo + "&docByException=" + docByException + "&unitType=" + unitType + "&pharmacyType=" + pharmacyType + "&hospitalType=" + hospitalType + "&hospitalSizeFrom=" + hospitalSizeFrom + "&optHospitalSizeFrom=" + optHospitalSizeFrom + "&hospitalSizeTo=" + hospitalSizeTo + "&optHospitalSizeTo=" + optHospitalSizeTo + "&countryID=" + countryID + "&stateID=" + stateID + "&hospitalUnitId=" + hospitalUnitID;
                    string url = "ReportLocationProfileGrid.aspx?yearFrom=" + yearFrom + "&yearTo=" + yearTo + "&monthFrom=" + monthFrom + "&monthTo=" + monthTo + "&BedsInUnitFrom=" + bedsInUnitFrom + "&optBedsInUnitFrom=" + optBedsInUnitFrom + "&bedsInUnitTo=" + bedsInUnitTo + "&optBedsInUnitTo=" + optBedsInUnitTo + "&budgetedPatientFrom=" + budgetedPatientFrom + "&optBudgetedPatientFrom=" + optBudgetedPatientFrom + "&budgetedPatientTo=" + budgetedPatientTo + "&optBudgetedPatientTo=" + optBudgetedPatientTo + "&electronicDocumentationFrom=" + electronicDocumentationFrom + "&optElectronicDocumentationFrom=" + optElectronicDocumentationFrom + "&electronicDocumentationTo=" + electronicDocumentationTo + "&optElectronicDocumentationTo=" + optElectronicDocumentationTo + "&docByException=" + docByException + "&unitType=" + unitType + "&pharmacyType=" + pharmacyType + "&hospitalType=" + hospitalType + "&hospitalSizeFrom=" + hospitalSizeFrom + "&optHospitalSizeFrom=" + optHospitalSizeFrom + "&hospitalSizeTo=" + hospitalSizeTo + "&optHospitalSizeTo=" + optHospitalSizeTo + "&countryID=" + countryID + "&stateID=" + stateID + "&hospitalUnitId=" + hospitalUnitID + "&configName=" + configName + "&filterName=" + filterName + "&hospitalName=" + hospitalName + "&hospitalUnit=" + hospitalUnit + "&beginingDate=" + beginingDate + "&endingDate=" + endingDate;
                    Response.Redirect(url,false);
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

        protected void ButtonReset_Click(object sender, EventArgs e)
        {
            try
            {
                DropDownListBenchmarkingFilter.SelectedIndex = 0;
                DropDownListHospitalName.Enabled = true;
                DropDownListHospitalName.SelectedIndex = 0;
                DropDownListHospitalUnit.Enabled = true;
                DropDownListHospitalUnit.SelectedIndex = 0;
                DropDownListYearMonthFrom.SelectedIndex = 0;
                DropDownListYearMonthTo.SelectedIndex = 0;

            }
            catch (Exception ex)
            {
                LogManager._stringObject = "UserNotification.ascx ---- Page_Load";
                LogManager.SetExceptionDetails(ex, LogManager._stringObject, null);
                LogManager.LogException(ex, LogManager.LoggingCategory.General, LogManager.LoggingLevel.Error);
                CommonClass.Show(LogManager.ShowErrorDetail(ex));
            }
        }

        protected void LinkButtonLocationNames_Click1(object sender, EventArgs e)
        {
            Response.Redirect("StandardizeLocationName.aspx", false);
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
                BSHospitalInfo _objectBSHospitalInfo = new BSHospitalInfo();

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
        /// Private method which resets all controls to its default value
        /// </summary>
        private void Reset()
        {

        }

        #endregion

    }

}
