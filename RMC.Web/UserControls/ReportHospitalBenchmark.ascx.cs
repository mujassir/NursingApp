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
    public partial class ReportHospitalBenchmark : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                //LabelFilter.Text = "Filter:- " + Request.QueryString["filter"];
                //LabelBegigningDate.Text = "Period:- " + Request.QueryString["beginingDate"];
                //LabelEndingDate.Text = " - " + Request.QueryString["endingDate"];
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

                ////e.InputParameters["hospitalUnitID"] = null;
                ////e.InputParameters["firstYear"] = null;
                ////e.InputParameters["lastYear"] = null;
                ////e.InputParameters["firstMonth"] = null;
                ////e.InputParameters["lastMonth"] = null;
                ////e.InputParameters["bedInUnit"] = 10;
                ////e.InputParameters["optBedInUnit"] = 1;
                ////e.InputParameters["budgetedPatient"] = null;
                ////e.InputParameters["optBudgetedPatient"] = 0;
                ////e.InputParameters["startDate"] = null;
                ////e.InputParameters["endDate"] = null;
                ////e.InputParameters["electronicDocument"] = null;
                ////e.InputParameters["optElectronicDocument"] = 0;
                ////e.InputParameters["docByException"] = 0;
                ////e.InputParameters["unitType"] = null;
                ////e.InputParameters["pharmacyType"] = null;
                ////e.InputParameters["optHospitalSize"] = 0;
                ////e.InputParameters["hospitalSize"] = null;
                ////e.InputParameters["value"] = Request.QueryString["value"];
                ////e.InputParameters["others"] = Request.QueryString["others"];
                ////e.InputParameters["location"] = Request.QueryString["location"];


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
                //if (bedInUnit == string.Empty)
                //{
                //    bedInUnit = null;
                //}
                //e.InputParameters["bedInUnit"] = bedInUnit;

                //e.InputParameters["optBedInUnit"] = Convert.ToInt32(Request.QueryString["optBedInUnit"]);

                //string budgetedPatient = Request.QueryString["budgetedPatient"];
                //if (budgetedPatient == string.Empty)
                //{
                //    budgetedPatient = null;
                //}
                //e.InputParameters["budgetedPatient"] = budgetedPatient;

                //e.InputParameters["optBudgetedPatient"] = Convert.ToInt32(Request.QueryString["optBudgetedPatient"]);

                //string startDate = Request.QueryString["startDate"];
                //if (startDate == string.Empty)
                //{
                //    startDate = null;
                //}
                //e.InputParameters["startDate"] = startDate;

                //string endDate = Request.QueryString["endDate"];
                //if (endDate == string.Empty)
                //{
                //    endDate = null;
                //}
                //e.InputParameters["endDate"] = endDate;

                //string electronicDocument = Request.QueryString["electronicDocument"];
                //if (electronicDocument == string.Empty)
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
                //if (unitType == string.Empty)
                //{
                //    unitType = null;
                //}
                //e.InputParameters["unitType"] = unitType;

                //string pharmacyType = Request.QueryString["pharmacyType"];
                //if (pharmacyType == string.Empty)
                //{
                //    pharmacyType = null;
                //}
                //e.InputParameters["pharmacyType"] = pharmacyType;

                //e.InputParameters["optHospitalSize"] = Convert.ToInt32(Request.QueryString["optHospitalSize"]);

                //string hospitalSize = Request.QueryString["hospitalSize"];
                //if (hospitalSize == string.Empty)
                //{
                //    hospitalSize = null;
                //}
                //e.InputParameters["hospitalSize"] = hospitalSize;

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

                //ImageButtonBack.PostBackUrl = "~/Administrator/HospitalBenchmark.aspx?" + "&filter=" + filter + "&beginingDate=" + beginingDate + "&endingDate=" + endingDate + "&value=" + value + "&others=" + others + "&location=" + location;
                if (HttpContext.Current.User.IsInRole("superadmin"))
                {
                    Response.Redirect("~/Administrator/HospitalBenchmark.aspx?" + "&filter=" + filter + "&beginingDate=" + beginingDate + "&endingDate=" + endingDate + "&value=" + value + "&others=" + others + "&location=" + location);
                }
                else 
                {
                    Response.Redirect("~/Users/HospitalBenchmark.aspx?" + "&filter=" + filter + "&beginingDate=" + beginingDate + "&endingDate=" + endingDate + "&value=" + value + "&others=" + others + "&location=" + location);

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

        //protected void ObjectDataSource2_Selecting(object sender, ObjectDataSourceSelectingEventArgs e)
        //{
        //    e.InputParameters["valueAddedCategoryID"] = 67;
        //    e.InputParameters["OthersCategoryID"] = 68;
        //    e.InputParameters["LocationCategoryID"] = 69;

        //    e.InputParameters["hospitalUnitID"] = null;
        //    e.InputParameters["firstYear"] = null;
        //    e.InputParameters["lastYear"] = null;
        //    e.InputParameters["firstMonth"] = null;
        //    e.InputParameters["lastMonth"] = null;
        //    e.InputParameters["bedInUnit"] = null;
        //    e.InputParameters["optBedInUnit"] = 0;
        //    e.InputParameters["budgetedPatient"] = null;
        //    e.InputParameters["optBudgetedPatient"] = 0;
        //    e.InputParameters["startDate"] = null;
        //    e.InputParameters["endDate"] = null;
        //    e.InputParameters["electronicDocument"] = null;
        //    e.InputParameters["optElectronicDocument"] = 0;
        //    e.InputParameters["docByException"] = 0;
        //    e.InputParameters["unitType"] = null;
        //    e.InputParameters["pharmacyType"] = null;
        //    e.InputParameters["optHospitalSize"] = 0;
        //    e.InputParameters["hospitalSize"] = null;
        //}



    }
}