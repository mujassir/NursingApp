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

namespace RMC.Web.UserControls
{
    public partial class ShowSpecialTypeData : System.Web.UI.UserControl
    {
        #region Properties

        public string Permission
        {
            get
            {
                return Convert.ToString(Request.QueryString["Permission"]);
            }
        }

        #endregion

        #region Events

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                Page.Header.Title = "RMC :: Special Type Data";
                if (!Page.IsPostBack)
                {
                    CommonClass objectCommonClass = new CommonClass();
                    objectCommonClass.BackButtonUrl = Request.UrlReferrer.AbsoluteUri;
                    if (Permission == "readonly" || Permission == "upload data")
                    {
                        MultiViewFileRecords.ActiveViewIndex = 1;
                        GridViewValidDataForUsers.DataBind();
                    }
                    else
                    {
                        MultiViewFileRecords.ActiveViewIndex = 0;
                        GridViewErrorValidation.DataBind();
                    }
                }
            }
            catch (Exception ex)
            {
                ex.Data.Add("Events", "Page_Load");
                ex.Data.Add("Page", "ShowValidData.ascx");
                LogManager._stringObject = "ShowValidData.ascx ---- Page_Load ";
                LogManager.SetExceptionDetails(ex, LogManager._stringObject, null);
                LogManager.LogException(ex, LogManager.LoggingCategory.General, LogManager.LoggingLevel.Error);
                CommonClass.Show(LogManager.ShowErrorDetail(ex));
            }
        }

        protected void LinkButtonDeleteSelected_Click(object sender, EventArgs e)
        {
            List<int> objectGenericNursePDASpecialTypeIDs = new List<int>();
            try
            {
                foreach (GridViewRow grdRow in GridViewErrorValidation.Rows)
                {
                    CheckBox chkBoxDelete = (CheckBox)grdRow.FindControl("CheckBoxDelete");

                    if (chkBoxDelete.Checked)
                    {
                        int nurseSpecialTypeID = 0;
                        if (int.TryParse(Convert.ToString(GridViewErrorValidation.DataKeys[grdRow.RowIndex].Value), out nurseSpecialTypeID))
                            objectGenericNursePDASpecialTypeIDs.Add(nurseSpecialTypeID);
                    }
                }

                if (objectGenericNursePDASpecialTypeIDs.Count > 0)
                {
                    RMC.BussinessService.BSNursePDADetail objectBSNursePDADetail = new RMC.BussinessService.BSNursePDADetail();
                    objectBSNursePDADetail.DeleteNursePDASpecialType(objectGenericNursePDASpecialTypeIDs);
                    GridViewErrorValidation.DataBind();
                }
            }
            catch (Exception ex)
            {
                LogManager._stringObject = "DataManagementHospitalUnit.ascx ---- LinkButtonDeleteSelected_Click";
                LogManager.SetExceptionDetails(ex, LogManager._stringObject, null);
                LogManager.LogException(ex, LogManager.LoggingCategory.General, LogManager.LoggingLevel.Error);
                CommonClass.Show(LogManager.ShowErrorDetail(ex));
            }
            finally
            {
                objectGenericNursePDASpecialTypeIDs = null;
            }
        }

        protected void ImageButtonBack_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                CommonClass objectCommonClass = new CommonClass();
                string backUrl = objectCommonClass.BackButtonUrl;
                Response.Redirect(backUrl, false);
            }
            catch (Exception ex)
            {
                LogManager._stringObject = "DataManagementHospitalUnit.ascx ---- ImageButtonBack_Click";
                LogManager.SetExceptionDetails(ex, LogManager._stringObject, null);
                LogManager.LogException(ex, LogManager.LoggingCategory.General, LogManager.LoggingLevel.Error);
                CommonClass.Show(LogManager.ShowErrorDetail(ex));
            }
        }

        #endregion
    }
}