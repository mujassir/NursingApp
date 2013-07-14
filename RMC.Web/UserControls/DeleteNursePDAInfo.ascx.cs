using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using LogExceptions;

namespace RMC.Web.UserControls
{
    public partial class DeleteNursePDAInfo : System.Web.UI.UserControl
    {

        #region Properties

        private int NurseID
        {
            get
            {
                int nurseID = 0;
                if (Request.QueryString["NurseID"] != null)
                {
                    if (!int.TryParse(Convert.ToString(Request.QueryString["NurseID"]), out nurseID))
                    {
                        nurseID = 0;
                    }
                }

                return nurseID;
            }
        }

        #endregion

        #region Events

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                int hospitalUnitID = 0;
                RMC.BussinessService.BSNursePDADetail objectBSNursePDADetail = new RMC.BussinessService.BSNursePDADetail();

                if (NurseID > 0)
                {
                    hospitalUnitID = objectBSNursePDADetail.DeleteNursePDAInfo(NurseID);
                }
                if (HttpContext.Current.User.IsInRole("superadmin"))
                {
                    Response.Redirect("~/Administrator/DataManagement.aspx", false);
                }
                else
                {
                    Response.Redirect("~/Users/DataManagement.aspx", false);
                }
            }
            catch (Exception ex)
            {
                ex.Data.Add("Events", "Page_Load");
                ex.Data.Add("Page", "DemographicDetail.aspx");
                LogManager._stringObject = "DemographicDetail.aspx ---- ButtonReset_Click";
                LogManager.SetExceptionDetails(ex, LogManager._stringObject, null);
                LogManager.LogException(ex, LogManager.LoggingCategory.General, LogManager.LoggingLevel.Error);
                CommonClass.Show(LogManager.ShowErrorDetail(ex));
            }
        }

        #endregion

    }
}