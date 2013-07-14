using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using LogExceptions;

namespace RMC.Web.UserControls
{
    public partial class DeleteAll : System.Web.UI.UserControl
    {

        #region Properties

        protected string Type
        {
            get
            {
                return Convert.ToString(Request.QueryString["Type"]).ToLower().Trim();
            }
        }

        protected int Year
        {
            get
            {
                int year = 0;
                if (int.TryParse(Convert.ToString(Request.QueryString["Year"]), out year))
                {
                    return year;
                }
                else
                {
                    return 0;
                }
            }
        }

        protected int Month
        {
            get
            {
                int month = 0;
                if (int.TryParse(Convert.ToString(Request.QueryString["Month"]), out month))
                {
                    return month;
                }
                else
                {
                    return 0;
                }
            }
        }

        protected int HospitalUnitID
        {
            get
            {
                int hospitalUnitID = 0;
                if (int.TryParse(Convert.ToString(Request.QueryString["HospitalUnitID"]), out hospitalUnitID))
                {
                    return hospitalUnitID;
                }
                else
                {
                    return 0;
                }
            }
        }
        #endregion

        #region Events

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                QueryStringHandler.QuerystringParameterEncrpt objectQueryStringEnc = new QueryStringHandler.QuerystringParameterEncrpt();
                bool flag;
                if (Type == "dataimport")
                {
                    RMC.BussinessService.BSNursePDADetail objectBSNursePDADetail = new RMC.BussinessService.BSNursePDADetail();

                    flag = objectBSNursePDADetail.DeleteNursePDAInfo(HospitalUnitID, Year.ToString(), Month.ToString());

                    if (HttpContext.Current.User.IsInRole("superadmin"))
                    {
                        Response.Redirect("~/Administrator/DataManagementMonth.aspx?" + objectQueryStringEnc.EncrptQuerystringParam("Year=" + Year + "&HospitalInfoID=" + Request.QueryString["HospitalInfoID"] + "&HospitalDemographicId=" + Request.QueryString["HospitalDemographicId"] + "&UnitCounter=" + Request.QueryString["UnitCounter"] + "&PermissionID=" + Request.QueryString["PermissionID"]), false);
                    }
                    else
                    {
                        Response.Redirect("~/Users/DataManagementMonth.aspx?" + objectQueryStringEnc.EncrptQuerystringParam("Year=" + Year + "&HospitalInfoID=" + Request.QueryString["HospitalInfoID"] + "&HospitalDemographicId=" + Request.QueryString["HospitalDemographicId"] + "&UnitCounter=" + Request.QueryString["UnitCounter"] + "&PermissionID=" + Request.QueryString["PermissionID"]), false);
                    }
                }

                if (Type == "dataimportwithoutmonth")
                {
                    RMC.BussinessService.BSNursePDADetail objectBSNursePDADetail = new RMC.BussinessService.BSNursePDADetail();

                    flag = objectBSNursePDADetail.DeleteNursePDAInfo(HospitalUnitID, Year.ToString());

                    if (HttpContext.Current.User.IsInRole("superadmin"))
                    {
                        Response.Redirect("~/Administrator/DataManagementYear.aspx?" + objectQueryStringEnc.EncrptQuerystringParam("HospitalInfoId=" + Convert.ToString(Request.QueryString["HospitalInfoId"]) + "&HospitalDemographicId=" + Convert.ToString(Request.QueryString["HospitalDemographicId"]) + "&UnitCounter=" + Convert.ToString(Request.QueryString["UnitCounter"]) + "&PermissionID=" + Request.QueryString["PermissionID"]), false);
                    }
                    else
                    {
                        Response.Redirect("~/Users/DataManagementYear.aspx?" + objectQueryStringEnc.EncrptQuerystringParam("HospitalInfoId=" + Convert.ToString(Request.QueryString["HospitalInfoId"]) + "&HospitalDemographicId=" + Convert.ToString(Request.QueryString["HospitalDemographicId"]) + "&UnitCounter=" + Convert.ToString(Request.QueryString["UnitCounter"]) + "&PermissionID=" + Request.QueryString["PermissionID"]), false);
                    }
                }
            }
            catch (Exception ex)
            {
                LogManager._stringObject = "DeleteAll.ascx ---- Page_Load";
                LogManager.SetExceptionDetails(ex, LogManager._stringObject, null);
                LogManager.LogException(ex, LogManager.LoggingCategory.General, LogManager.LoggingLevel.Error);
                CommonClass.Show(LogManager.ShowErrorDetail(ex));
            }
        }

        #endregion

    }
}