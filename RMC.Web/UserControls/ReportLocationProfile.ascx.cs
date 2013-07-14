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
    public partial class ReportLocationProfile : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void ObjectDataSource1_Selecting(object sender, ObjectDataSourceSelectingEventArgs e)
        {
            try
            {
                string hospitalUnitID = Request.QueryString["hospitalUnitId"];
                if (hospitalUnitID == string.Empty)
                {
                    hospitalUnitID = null;
                }
                e.InputParameters["hospitalUnitID"] = hospitalUnitID;

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
                if (HttpContext.Current.User.IsInRole("superadmin"))
                {
                    Response.Redirect("~/Administrator/LocationProfile.aspx");
                }
                else
                {
                    Response.Redirect("~/Users/LocationProfile.aspx");

                }
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
    }
}