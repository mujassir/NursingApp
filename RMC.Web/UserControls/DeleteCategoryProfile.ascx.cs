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
    public partial class DeleteCategoryProfile : System.Web.UI.UserControl
    {

        #region Variables
        
        RMC.BussinessService.BSCategoryProfiles objectBSCategoryProfiles = null;
        bool IsDelete = false; 

        #endregion
        
        #region Events
        
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                objectBSCategoryProfiles = new RMC.BussinessService.BSCategoryProfiles();
                IsDelete = objectBSCategoryProfiles.DeleteCategoryProfile(Convert.ToInt32(Request.QueryString["ProfileTypeID"]));
                if (IsDelete == true)
                {
                    if (HttpContext.Current.User.IsInRole("superadmin"))
                    {
                        Response.Redirect("~/Administrator/CategoryProfiles.aspx", false);
                    }
                    else
                    {
                        Response.Redirect("~/Users/CategoryProfiles.aspx", false);
                    }
                }
            }
            catch (Exception ex)
            {
                ex.Data.Add("Methods", "populateTreeviewParentNodes");
                ex.Data.Add("Page", "ProfileTreeView.ascx");
                LogManager._stringObject = "ProfileTreeView.ascx ---- populateTreeviewParentNodes";
                LogManager.SetExceptionDetails(ex, LogManager._stringObject, null);
                LogManager.LogException(ex, LogManager.LoggingCategory.General, LogManager.LoggingLevel.Error);
                if (HttpContext.Current.User.IsInRole("superadmin"))
                {
                    Response.Redirect("~/Administrator/CategoryProfiles.aspx", false);
                }
                else
                {
                    Response.Redirect("~/Users/CategoryProfiles.aspx", false);
                }
            }
        } 

        #endregion

    }
}