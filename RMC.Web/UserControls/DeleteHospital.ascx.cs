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
    public partial class DeleteHospital : System.Web.UI.UserControl
    {

        #region Variables

        RMC.BussinessService.BSHospitalInfo objectBSHospitalInfo = null;
        bool IsDelete = false;

        #endregion

        #region Events

        protected void Page_Load(object sender, EventArgs e)
        {
            string returnParam = string.Empty;
            QueryStringHandler.QuerystringParameterEncrpt objectQuerystringEncrpt = new QueryStringHandler.QuerystringParameterEncrpt();
            if (Request.RawUrl != null)
                returnParam = Request.Params["HospitalInfoID"];

            try
            {
                objectBSHospitalInfo = new RMC.BussinessService.BSHospitalInfo();                
                
                if (Request.QueryString["HospitalID"] != null)
                {
                    IsDelete = objectBSHospitalInfo.DeletePhysicallyHospitalInfoRelatedRecord(Convert.ToInt32(Request.QueryString["HospitalID"]));
                }
                if (Request.QueryString["HospitalDemographicId"] != null)
                {
                    IsDelete = objectBSHospitalInfo.DeletePhysicallyHospitalUnitRelatedRecord(Convert.ToInt32(Request.QueryString["HospitalDemographicId"]));
                }
                if (IsDelete == true)
                {
                    if (Request.QueryString["HospitalID"] != null)
                    {
                        if (HttpContext.Current.User.IsInRole("superadmin"))
                        {
                            Response.Redirect("~/Administrator/DataManagement.aspx", false);
                        }
                        else
                        {
                            Response.Redirect("~/Users/DataManagement.aspx", false);
                        }
                    }

                    if (Request.QueryString["HospitalDemographicId"] != null)
                    {
                        if (HttpContext.Current.User.IsInRole("superadmin"))
                        {
                            Response.Redirect("~/Administrator/DataManagementUnit.aspx?" + objectQuerystringEncrpt.EncrptQuerystringParam("HospitalInfoID=" + returnParam), false);
                        }
                        else
                        {
                            Response.Redirect("~/Users/DataManagementUnit.aspx?" + objectQuerystringEncrpt.EncrptQuerystringParam("HospitalInfoID=" + returnParam), false);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ex.Data.Add("Methods", "populateTreeviewParentNodes");
                ex.Data.Add("Page", "DataManagement.ascx");
                LogManager._stringObject = "DataManagement.ascx ---- populateTreeviewParentNodes";
                LogManager.SetExceptionDetails(ex, LogManager._stringObject, null);
                LogManager.LogException(ex, LogManager.LoggingCategory.General, LogManager.LoggingLevel.Error);
                if (Request.QueryString["HospitalID"] != null)
                {
                    if (HttpContext.Current.User.IsInRole("superadmin"))
                    {
                        Response.Redirect("~/Administrator/DataManagement.aspx", false);
                    }
                    else
                    {
                        Response.Redirect("~/Users/DataManagement.aspx", false);
                    }
                }

                if (Request.QueryString["HospitalDemographicId"] != null)
                {
                    if (HttpContext.Current.User.IsInRole("superadmin"))
                    {
                        Response.Redirect("~/Administrator/DataManagementUnit.aspx?" + objectQuerystringEncrpt.EncrptQuerystringParam("HospitalInfoID=" + returnParam), false);
                    }
                    else
                    {
                        Response.Redirect("~/Users/DataManagementUnit.aspx?" + objectQuerystringEncrpt.EncrptQuerystringParam("HospitalInfoID=" + returnParam), false);
                    }
                }
            }
        }

        #endregion

    }
}