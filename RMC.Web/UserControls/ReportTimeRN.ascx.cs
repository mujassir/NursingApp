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
    public partial class ReportTimeRN : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                LabelHospitalName.Text = "#" + Request.QueryString["hospitalName"];
                LabelHospitalUnitName.Text = "Unit: " + Request.QueryString["hospitalUnitName"];
                LabelFilter.Text = "Filter: " + Request.QueryString["filter"];

            }
            catch (Exception ex)
            {
                LogManager._stringObject = "ReportTimeRN.ascx ----";
                LogManager.SetExceptionDetails(ex, LogManager._stringObject, null);
                LogManager.LogException(ex, LogManager.LoggingCategory.General, LogManager.LoggingLevel.Error);
                CommonClass.Show(LogManager.ShowErrorDetail(ex));
            }
        }

        //protected void ObjectDataSource5_Selecting(object sender, ObjectDataSourceSelectingEventArgs e)
        //{
        //    try
        //    {
        //        e.InputParameters["profileCategory"] = DropDownListProfileCategory.SelectedValue.ToString();
        //        if (DropDownListChart.SelectedValue == "Pie")
        //        {
        //            e.InputParameters["value"] = DropDownListProfileCategory.SelectedValue.ToString();
        //        }
        //        else
        //        {
        //            e.InputParameters["value"] = DropDownListProfileSubCategory.SelectedValue.ToString();
        //        }
        //        e.InputParameters["valueAddedCategoryID"] = Convert.ToInt32(Request.QueryString["valueAddedCategoryID"]);
        //        e.InputParameters["OthersCategoryID"] = Convert.ToInt32(Request.QueryString["OthersCategoryID"]);
        //        e.InputParameters["LocationCategoryID"] = Convert.ToInt32(Request.QueryString["LocationCategoryID"]);

        //        string hospitalUnitID = Request.QueryString["hospitalUnitID"];
        //        if (hospitalUnitID == "0")
        //        {
        //            hospitalUnitID = null;
        //        }
        //        e.InputParameters["hospitalUnitID"] = hospitalUnitID;

        //        string firstYear = Request.QueryString["firstYear"];
        //        if (firstYear == "0")
        //        {
        //            firstYear = null;
        //        }
        //        e.InputParameters["firstYear"] = firstYear;

        //        string lastYear = Request.QueryString["lastYear"];
        //        if (lastYear == "0")
        //        {
        //            lastYear = null;
        //        }
        //        e.InputParameters["lastYear"] = lastYear;

        //        string firstMonth = Request.QueryString["firstMonth"];
        //        if (firstMonth == "0")
        //        {
        //            firstMonth = null;
        //        }
        //        e.InputParameters["firstMonth"] = firstMonth;

        //        string lastMonth = Request.QueryString["lastMonth"];
        //        if (lastMonth == "0")
        //        {
        //            lastMonth = null;
        //        }
        //        e.InputParameters["lastMonth"] = lastMonth;

        //        string bedInUnit = Request.QueryString["bedInUnit"];
        //        if (bedInUnit == "0")
        //        {
        //            bedInUnit = null;
        //        }
        //        e.InputParameters["bedInUnit"] = bedInUnit;

        //        e.InputParameters["optBedInUnit"] = Convert.ToInt32(Request.QueryString["optBedInUnit"]);

        //        string budgetedPatient = Request.QueryString["budgetedPatient"];
        //        if (budgetedPatient == "0")
        //        {
        //            budgetedPatient = null;
        //        }
        //        e.InputParameters["budgetedPatient"] = budgetedPatient;

        //        e.InputParameters["optBudgetedPatient"] = Convert.ToInt32(Request.QueryString["optBudgetedPatient"]);

        //        string startDate = Request.QueryString["startDate"];
        //        if (startDate == "")
        //        {
        //            startDate = null;
        //        }
        //        e.InputParameters["startDate"] = startDate;

        //        string endDate = Request.QueryString["endDate"];
        //        if (endDate == "")
        //        {
        //            endDate = null;
        //        }
        //        e.InputParameters["endDate"] = endDate;

        //        string electronicDocument = Request.QueryString["electronicDocument"];
        //        if (electronicDocument == "0")
        //        {
        //            electronicDocument = null;
        //        }
        //        e.InputParameters["electronicDocument"] = electronicDocument;

        //        e.InputParameters["optElectronicDocument"] = Convert.ToInt32(Request.QueryString["optElectronicDocument"]);

        //        //string docByException = Request.QueryString["docByException"];
        //        //if (docByException == "0")
        //        //{
        //        //    docByException = null;
        //        //}
        //        //e.InputParameters["docByException"] = docByException;

        //        e.InputParameters["docByException"] = Convert.ToInt32(Request.QueryString["docByException"]);

        //        string unitType = Request.QueryString["unitType"];
        //        if (unitType == "")
        //        {
        //            unitType = null;
        //        }
        //        e.InputParameters["unitType"] = unitType;

        //        string pharmacyType = Request.QueryString["pharmacyType"];
        //        if (pharmacyType == "")
        //        {
        //            pharmacyType = null;
        //        }
        //        e.InputParameters["pharmacyType"] = pharmacyType;

        //        e.InputParameters["optHospitalSize"] = Convert.ToInt32(Request.QueryString["optHospitalSize"]);

        //        string hospitalSize = Request.QueryString["hospitalSize"];
        //        if (hospitalSize == "0")
        //        {
        //            hospitalSize = null;
        //        }
        //        e.InputParameters["hospitalSize"] = hospitalSize;

        //    }
        //    catch (Exception ex)
        //    {
        //        LogManager._stringObject = "ReportTimeRN.ascx ---- Page_Load";
        //        LogManager.SetExceptionDetails(ex, LogManager._stringObject, null);
        //        LogManager.LogException(ex, LogManager.LoggingCategory.General, LogManager.LoggingLevel.Error);
        //        CommonClass.Show(LogManager.ShowErrorDetail(ex));
        //    }
        //}

        protected void ButtonGenerateChart_Click(object sender, EventArgs e)
        {
            try
            {
                //if (DropDownListChart.SelectedValue == "Line")
                //{
                //    ReportViewer1.Visible = false;
                //    ReportViewer2.LocalReport.Refresh();
                //    ReportViewer2.Visible = true;
                //    ReportViewer3.Visible = false;
                //}
                //if (DropDownListChart.SelectedValue == "Pie")
                //{
                //    ReportViewer1.Visible = false;
                //    ReportViewer2.Visible = false;
                //    ReportViewer3.LocalReport.Refresh();
                //    ReportViewer3.Visible = true;
                //}
            }
            catch (Exception ex)
            {
                LogManager._stringObject = "ReportTimeRN.ascx ----";
                LogManager.SetExceptionDetails(ex, LogManager._stringObject, null);
                LogManager.LogException(ex, LogManager.LoggingCategory.General, LogManager.LoggingLevel.Error);
                CommonClass.Show(LogManager.ShowErrorDetail(ex));
            }
        }

        protected void DropDownListProfileCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                //DropDownListProfileSubCategory.Items.Clear();
                //DropDownListProfileSubCategory.Items.Add("Select Sub Profile");
                //DropDownListProfileSubCategory.Items[0].Value = 0.ToString();
                //DropDownListProfileSubCategory.Focus();
            }
            catch (Exception ex)
            {
                LogManager._stringObject = "ReportTimeRN.ascx ----";
                LogManager.SetExceptionDetails(ex, LogManager._stringObject, null);
                LogManager.LogException(ex, LogManager.LoggingCategory.General, LogManager.LoggingLevel.Error);
                CommonClass.Show(LogManager.ShowErrorDetail(ex));
            }
        }

        protected void DropDownListChart_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                //DropDownListProfileCategory.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                LogManager._stringObject = "ReportTimeRN.ascx ----";
                LogManager.SetExceptionDetails(ex, LogManager._stringObject, null);
                LogManager.LogException(ex, LogManager.LoggingCategory.General, LogManager.LoggingLevel.Error);
                CommonClass.Show(LogManager.ShowErrorDetail(ex));
            }
        }

        protected void ButtonShowReport_Click(object sender, EventArgs e)
        {
            try
            {
                //ReportViewer1.Visible = true;
                //ReportViewer2.Visible = false;
                //ReportViewer3.Visible = false;
                //DropDownListProfileCategory.Enabled = false;
                //DropDownListProfileSubCategory.Enabled = false;
                //DropDownListChart.SelectedIndex = 0;
                //DropDownListProfileCategory.SelectedIndex = 0;
                //DropDownListProfileSubCategory.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                LogManager._stringObject = "ReportTimeRN.ascx ----";
                LogManager.SetExceptionDetails(ex, LogManager._stringObject, null);
                LogManager.LogException(ex, LogManager.LoggingCategory.General, LogManager.LoggingLevel.Error);
                CommonClass.Show(LogManager.ShowErrorDetail(ex));
            }
        }

        protected void ObjectDataSource1_Selecting(object sender, ObjectDataSourceSelectingEventArgs e)
        {
            try
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

                e.InputParameters["hospitalUnitID"] = Request.QueryString["hospitalUnitID"];

                string firstYear = Request.QueryString["yearFrom"];
                if (firstYear == "")
                {
                    firstYear = null;
                }
                e.InputParameters["firstYear"] = firstYear;

                string lastYear = Request.QueryString["yearTo"];
                if (lastYear == "")
                {
                    lastYear = null;
                }
                e.InputParameters["lastYear"] = lastYear;

                string firstMonth = Request.QueryString["monthFrom"];
                if (firstMonth == "")
                {
                    firstMonth = null;
                }
                e.InputParameters["firstMonth"] = firstMonth;

                string lastMonth = Request.QueryString["monthTo"];
                if (lastMonth == "")
                {
                    lastMonth = null;
                }
                e.InputParameters["lastMonth"] = lastMonth;

                string bedInUnitFrom = Request.QueryString["BedsInUnitFrom"];
                if (bedInUnitFrom == "")
                {
                    bedInUnitFrom = null;
                }
                e.InputParameters["bedInUnitFrom"] = bedInUnitFrom;
                e.InputParameters["optBedInUnitFrom"] = Convert.ToInt32(Request.QueryString["optBedsInUnitFrom"]);
                string bedInUnitTo = Request.QueryString["bedsInUnitTo"];
                if (bedInUnitTo == "")
                {
                    bedInUnitTo = null;
                }
                e.InputParameters["bedInUnitTo"] = bedInUnitTo;
                e.InputParameters["optBedInUnitTo"] = Convert.ToInt32(Request.QueryString["optBedsInUnitTo"]);

                string budgetedPatientFrom = Request.QueryString["budgetedPatientFrom"];
                if (budgetedPatientFrom == "")
                {
                    budgetedPatientFrom = null;
                }
                e.InputParameters["budgetedPatientFrom"] = budgetedPatientFrom;
                e.InputParameters["optBudgetedPatientFrom"] = Convert.ToInt32(Request.QueryString["optBudgetedPatientFrom"]);
                string budgetedPatientTo = Request.QueryString["budgetedPatientTo"];
                if (budgetedPatientTo == "")
                {
                    budgetedPatientTo = null;
                }
                e.InputParameters["budgetedPatientTo"] = budgetedPatientTo;
                e.InputParameters["optBudgetedPatientTo"] = Convert.ToInt32(Request.QueryString["optBudgetedPatientTo"]);

                e.InputParameters["startDate"] = null;
                e.InputParameters["endDate"] = null;

                string electronicDocumentFrom = Request.QueryString["electronicDocumentationFrom"];
                if (electronicDocumentFrom == "")
                {
                    electronicDocumentFrom = null;
                }
                e.InputParameters["electronicDocumentFrom"] = electronicDocumentFrom;
                e.InputParameters["optElectronicDocumentFrom"] = Convert.ToInt32(Request.QueryString["optElectronicDocumentationFrom"]);
                string electronicDocumentTo = Request.QueryString["electronicDocumentationTo"];
                if (electronicDocumentTo == "")
                {
                    electronicDocumentTo = null;
                }
                e.InputParameters["electronicDocumentTo"] = electronicDocumentTo;
                e.InputParameters["optElectronicDocumentTo"] = Convert.ToInt32(Request.QueryString["optElectronicDocumentationTo"]);

                string docByException = Request.QueryString["docByException"];
                if (docByException == "")
                {
                    docByException = null;
                }
                e.InputParameters["docByException"] = docByException;

                string unitType = Request.QueryString["unitType"];
                if (unitType == "")
                {
                    unitType = null;
                }
                e.InputParameters["unitType"] = unitType;
                string pharmacyType = Request.QueryString["pharmacyType"];
                if (pharmacyType == "")
                {
                    pharmacyType = null;
                }
                e.InputParameters["pharmacyType"] = pharmacyType;
                string hospitalType = Request.QueryString["hospitalType"];
                if (hospitalType == "")
                {
                    hospitalType = null;
                }
                e.InputParameters["hospitalType"] = hospitalType;


                e.InputParameters["optHospitalSizeFrom"] = Convert.ToInt32(Request.QueryString["optHospitalSizeFrom"]);
                string hospitalSizeFrom = Request.QueryString["hospitalSizeFrom"];
                if (hospitalSizeFrom == "")
                {
                    hospitalSizeFrom = null;
                }
                e.InputParameters["hospitalSizeFrom"] = hospitalSizeFrom;
                e.InputParameters["optHospitalSizeTo"] = Convert.ToInt32(Request.QueryString["optHospitalSizeTo"]);
                string hospitalSizeTo = Request.QueryString["hospitalSizeTo"];
                if (hospitalSizeTo == "")
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
                e.InputParameters["value"] = Request.QueryString["value"];
                e.InputParameters["others"] = Request.QueryString["others"];
                e.InputParameters["location"] = Request.QueryString["location"];




                //e.InputParameters["valueAddedCategoryID"] = Convert.ToInt32(Request.QueryString["valueAddedCategoryID"]);
                //e.InputParameters["OthersCategoryID"] = Convert.ToInt32(Request.QueryString["OthersCategoryID"]);
                //e.InputParameters["LocationCategoryID"] = Convert.ToInt32(Request.QueryString["LocationCategoryID"]);

                //string hospitalUnitID = Request.QueryString["hospitalUnitID"];
                //if (hospitalUnitID == "0")
                //{
                //    hospitalUnitID = null;
                //}
                //e.InputParameters["hospitalUnitID"] = hospitalUnitID;

                //string firstYear = Request.QueryString["firstYear"];
                //if (firstYear == "0")
                //{
                //    firstYear = null;
                //}
                //e.InputParameters["firstYear"] = firstYear;

                //string lastYear = Request.QueryString["lastYear"];
                //if (lastYear == "0")
                //{
                //    lastYear = null;
                //}
                //e.InputParameters["lastYear"] = lastYear;

                //string firstMonth = Request.QueryString["firstMonth"];
                //if (firstMonth == "0")
                //{
                //    firstMonth = null;
                //}
                //e.InputParameters["firstMonth"] = firstMonth;

                //string lastMonth = Request.QueryString["lastMonth"];
                //if (lastMonth == "0")
                //{
                //    lastMonth = null;
                //}
                //e.InputParameters["lastMonth"] = lastMonth;

                //string bedInUnit = Request.QueryString["bedInUnit"];
                //if (bedInUnit == "0")
                //{
                //    bedInUnit = null;
                //}
                //e.InputParameters["bedInUnit"] = bedInUnit;

                //e.InputParameters["optBedInUnit"] = Convert.ToInt32(Request.QueryString["optBedInUnit"]);

                //string budgetedPatient = Request.QueryString["budgetedPatient"];
                //if (budgetedPatient == "0")
                //{
                //    budgetedPatient = null;
                //}
                //e.InputParameters["budgetedPatient"] = budgetedPatient;

                //e.InputParameters["optBudgetedPatient"] = Convert.ToInt32(Request.QueryString["optBudgetedPatient"]);

                //string startDate = Request.QueryString["startDate"];
                //if (startDate == "")
                //{
                //    startDate = null;
                //}
                //e.InputParameters["startDate"] = startDate;

                //string endDate = Request.QueryString["endDate"];
                //if (endDate == "")
                //{
                //    endDate = null;
                //}
                //e.InputParameters["endDate"] = endDate;

                //string electronicDocument = Request.QueryString["electronicDocument"];
                //if (electronicDocument == "0")
                //{
                //    electronicDocument = null;
                //}
                //e.InputParameters["electronicDocument"] = electronicDocument;

                //e.InputParameters["optElectronicDocument"] = Convert.ToInt32(Request.QueryString["optElectronicDocument"]);

                ////string docByException = Request.QueryString["docByException"];
                ////if (docByException == "0")
                ////{
                ////    docByException = null;
                ////}
                ////e.InputParameters["docByException"] = docByException;

                //e.InputParameters["docByException"] = Convert.ToInt32(Request.QueryString["docByException"]);

                //string unitType = Request.QueryString["unitType"];
                //if (unitType == "")
                //{
                //    unitType = null;
                //}
                //e.InputParameters["unitType"] = unitType;

                //string pharmacyType = Request.QueryString["pharmacyType"];
                //if (pharmacyType == "")
                //{
                //    pharmacyType = null;
                //}
                //e.InputParameters["pharmacyType"] = pharmacyType;

                //e.InputParameters["optHospitalSize"] = Convert.ToInt32(Request.QueryString["optHospitalSize"]);

                //string hospitalSize = Request.QueryString["hospitalSize"];
                //if (hospitalSize == "0")
                //{
                //    hospitalSize = null;
                //}
                //e.InputParameters["hospitalSize"] = hospitalSize;

            }
            catch (Exception ex)
            {
                LogManager._stringObject = "ReportTimeRN.ascx ----";
                LogManager.SetExceptionDetails(ex, LogManager._stringObject, null);
                LogManager.LogException(ex, LogManager.LoggingCategory.General, LogManager.LoggingLevel.Error);
                CommonClass.Show(LogManager.ShowErrorDetail(ex));
            }
        }

        protected void ObjectDataSource2_Selecting(object sender, ObjectDataSourceSelectingEventArgs e)
        {
            try
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

                e.InputParameters["hospitalUnitID"] = null;

                string firstYear = Request.QueryString["yearFrom"];
                if (firstYear == "")
                {
                    firstYear = null;
                }
                e.InputParameters["firstYear"] = firstYear;

                string lastYear = Request.QueryString["yearTo"];
                if (lastYear == "")
                {
                    lastYear = null;
                }
                e.InputParameters["lastYear"] = lastYear;

                string firstMonth = Request.QueryString["monthFrom"];
                if (firstMonth == "")
                {
                    firstMonth = null;
                }
                e.InputParameters["firstMonth"] = firstMonth;

                string lastMonth = Request.QueryString["monthTo"];
                if (lastMonth == "")
                {
                    lastMonth = null;
                }
                e.InputParameters["lastMonth"] = lastMonth;

                string bedInUnitFrom = Request.QueryString["BedsInUnitFrom"];
                if (bedInUnitFrom == "")
                {
                    bedInUnitFrom = null;
                }
                e.InputParameters["bedInUnitFrom"] = bedInUnitFrom;
                e.InputParameters["optBedInUnitFrom"] = Convert.ToInt32(Request.QueryString["optBedsInUnitFrom"]);
                string bedInUnitTo = Request.QueryString["bedsInUnitTo"];
                if (bedInUnitTo == "")
                {
                    bedInUnitTo = null;
                }
                e.InputParameters["bedInUnitTo"] = bedInUnitTo;
                e.InputParameters["optBedInUnitTo"] = Convert.ToInt32(Request.QueryString["optBedsInUnitTo"]);

                string budgetedPatientFrom = Request.QueryString["budgetedPatientFrom"];
                if (budgetedPatientFrom == "")
                {
                    budgetedPatientFrom = null;
                }
                e.InputParameters["budgetedPatientFrom"] = budgetedPatientFrom;
                e.InputParameters["optBudgetedPatientFrom"] = Convert.ToInt32(Request.QueryString["optBudgetedPatientFrom"]);
                string budgetedPatientTo = Request.QueryString["budgetedPatientTo"];
                if (budgetedPatientTo == "")
                {
                    budgetedPatientTo = null;
                }
                e.InputParameters["budgetedPatientTo"] = budgetedPatientTo;
                e.InputParameters["optBudgetedPatientTo"] = Convert.ToInt32(Request.QueryString["optBudgetedPatientTo"]);

                e.InputParameters["startDate"] = null;
                e.InputParameters["endDate"] = null;

                string electronicDocumentFrom = Request.QueryString["electronicDocumentationFrom"];
                if (electronicDocumentFrom == "")
                {
                    electronicDocumentFrom = null;
                }
                e.InputParameters["electronicDocumentFrom"] = electronicDocumentFrom;
                e.InputParameters["optElectronicDocumentFrom"] = Convert.ToInt32(Request.QueryString["optElectronicDocumentationFrom"]);
                string electronicDocumentTo = Request.QueryString["electronicDocumentationTo"];
                if (electronicDocumentTo == "")
                {
                    electronicDocumentTo = null;
                }
                e.InputParameters["electronicDocumentTo"] = electronicDocumentTo;
                e.InputParameters["optElectronicDocumentTo"] = Convert.ToInt32(Request.QueryString["optElectronicDocumentationTo"]);

                string docByException = Request.QueryString["docByException"];
                if (docByException == "")
                {
                    docByException = null;
                }
                e.InputParameters["docByException"] = docByException;

                string unitType = Request.QueryString["unitType"];
                if (unitType == "")
                {
                    unitType = null;
                }
                e.InputParameters["unitType"] = unitType;
                string pharmacyType = Request.QueryString["pharmacyType"];
                if (pharmacyType == "")
                {
                    pharmacyType = null;
                }
                e.InputParameters["pharmacyType"] = pharmacyType;
                string hospitalType = Request.QueryString["hospitalType"];
                if (hospitalType == "")
                {
                    hospitalType = null;
                }
                e.InputParameters["hospitalType"] = hospitalType;


                e.InputParameters["optHospitalSizeFrom"] = Convert.ToInt32(Request.QueryString["optHospitalSizeFrom"]);
                string hospitalSizeFrom = Request.QueryString["hospitalSizeFrom"];
                if (hospitalSizeFrom == "")
                {
                    hospitalSizeFrom = null;
                }
                e.InputParameters["hospitalSizeFrom"] = hospitalSizeFrom;
                e.InputParameters["optHospitalSizeTo"] = Convert.ToInt32(Request.QueryString["optHospitalSizeTo"]);
                string hospitalSizeTo = Request.QueryString["hospitalSizeTo"];
                if (hospitalSizeTo == "")
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
                e.InputParameters["value"] = Request.QueryString["value"];
                e.InputParameters["others"] = Request.QueryString["others"];
                e.InputParameters["location"] = Request.QueryString["location"];




                //e.InputParameters["valueAddedCategoryID"] = Convert.ToInt32(Request.QueryString["valueAddedCategoryID"]);
                //e.InputParameters["OthersCategoryID"] = Convert.ToInt32(Request.QueryString["OthersCategoryID"]);
                //e.InputParameters["LocationCategoryID"] = Convert.ToInt32(Request.QueryString["LocationCategoryID"]);

                //string hospitalUnitID = Request.QueryString["hospitalUnitID"];
                //if (hospitalUnitID == "0")
                //{
                //    hospitalUnitID = null;
                //}
                //e.InputParameters["hospitalUnitID"] = hospitalUnitID;

                //string firstYear = Request.QueryString["firstYear"];
                //if (firstYear == "0")
                //{
                //    firstYear = null;
                //}
                //e.InputParameters["firstYear"] = firstYear;

                //string lastYear = Request.QueryString["lastYear"];
                //if (lastYear == "0")
                //{
                //    lastYear = null;
                //}
                //e.InputParameters["lastYear"] = lastYear;

                //string firstMonth = Request.QueryString["firstMonth"];
                //if (firstMonth == "0")
                //{
                //    firstMonth = null;
                //}
                //e.InputParameters["firstMonth"] = firstMonth;

                //string lastMonth = Request.QueryString["lastMonth"];
                //if (lastMonth == "0")
                //{
                //    lastMonth = null;
                //}
                //e.InputParameters["lastMonth"] = lastMonth;

                //string bedInUnit = Request.QueryString["bedInUnit"];
                //if (bedInUnit == "0")
                //{
                //    bedInUnit = null;
                //}
                //e.InputParameters["bedInUnit"] = bedInUnit;

                //e.InputParameters["optBedInUnit"] = Convert.ToInt32(Request.QueryString["optBedInUnit"]);

                //string budgetedPatient = Request.QueryString["budgetedPatient"];
                //if (budgetedPatient == "0")
                //{
                //    budgetedPatient = null;
                //}
                //e.InputParameters["budgetedPatient"] = budgetedPatient;

                //e.InputParameters["optBudgetedPatient"] = Convert.ToInt32(Request.QueryString["optBudgetedPatient"]);

                //string startDate = Request.QueryString["startDate"];
                //if (startDate == "")
                //{
                //    startDate = null;
                //}
                //e.InputParameters["startDate"] = startDate;

                //string endDate = Request.QueryString["endDate"];
                //if (endDate == "")
                //{
                //    endDate = null;
                //}
                //e.InputParameters["endDate"] = endDate;

                //string electronicDocument = Request.QueryString["electronicDocument"];
                //if (electronicDocument == "0")
                //{
                //    electronicDocument = null;
                //}
                //e.InputParameters["electronicDocument"] = electronicDocument;

                //e.InputParameters["optElectronicDocument"] = Convert.ToInt32(Request.QueryString["optElectronicDocument"]);

                ////string docByException = Request.QueryString["docByException"];
                ////if (docByException == "0")
                ////{
                ////    docByException = null;
                ////}
                ////e.InputParameters["docByException"] = docByException;

                //e.InputParameters["docByException"] = Convert.ToInt32(Request.QueryString["docByException"]);

                //string unitType = Request.QueryString["unitType"];
                //if (unitType == "")
                //{
                //    unitType = null;
                //}
                //e.InputParameters["unitType"] = unitType;

                //string pharmacyType = Request.QueryString["pharmacyType"];
                //if (pharmacyType == "")
                //{
                //    pharmacyType = null;
                //}
                //e.InputParameters["pharmacyType"] = pharmacyType;

                //e.InputParameters["optHospitalSize"] = Convert.ToInt32(Request.QueryString["optHospitalSize"]);

                //string hospitalSize = Request.QueryString["hospitalSize"];
                //if (hospitalSize == "0")
                //{
                //    hospitalSize = null;
                //}
                //e.InputParameters["hospitalSize"] = hospitalSize;

            }
            catch (Exception ex)
            {
                LogManager._stringObject = "ReportTimeRN.ascx ----";
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
                string hospitalName = Request.QueryString["hospitalName"];
                string hospitalUnitName = Request.QueryString["hospitalUnitName"];
                //ImageButtonBack.PostBackUrl = "~/Administrator/HospitalBenchmark.aspx?" + "&filter=" + filter + "&beginingDate=" + beginingDate + "&endingDate=" + endingDate + "&value=" + value + "&others=" + others + "&location=" + location;
                if (HttpContext.Current.User.IsInRole("superadmin"))
                {
                    Response.Redirect("~/Administrator/TimeRN.aspx?Report=Dashboard" + "&filter=" + filter + "&beginingDate=" + beginingDate + "&endingDate=" + endingDate + "&value=" + value + "&others=" + others + "&location=" + location + "&hospitalName=" + hospitalName + "&hospitalUnitName=" + hospitalUnitName);
                }
                else
                {
                    Response.Redirect("~/Users/TimeRN.aspx?Report=Dashboard" + "&filter=" + filter + "&beginingDate=" + beginingDate + "&endingDate=" + endingDate + "&value=" + value + "&others=" + others + "&location=" + location + "&hospitalName=" + hospitalName + "&hospitalUnitName=" + hospitalUnitName);
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




    }
}