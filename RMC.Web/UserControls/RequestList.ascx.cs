using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using LogExceptions;

namespace RMC.Web.UserControls
{
    public partial class RequestList : System.Web.UI.UserControl
    {

        #region Events
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {

            }
            catch (Exception ex)
            {
                ex.Data.Add("Events", "Page_Load");
                ex.Data.Add("Page", "RequestList.ascx");
                LogManager._stringObject = "RequestList.ascx ---- Page_Load";
                LogManager.SetExceptionDetails(ex, LogManager._stringObject, null);
                LogManager.LogException(ex, LogManager.LoggingCategory.General, LogManager.LoggingLevel.Error);
                CommonClass.Show(LogManager.ShowErrorDetail(ex));
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ImageButtonView_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                GridViewRow grdRow = (GridViewRow)((ImageButton)sender).NamingContainer;

                Response.Redirect("~/Administrator/Type.aspx?RequestID=" + Convert.ToString(GridViewRequestList.DataKeys[grdRow.RowIndex].Value), false);
            }
            catch (Exception ex)
            {
                ex.Data.Add("Events", "ImageButtonView_Click");
                ex.Data.Add("Page", "RequestList.ascx");
                LogManager._stringObject = "RequestList.ascx ---- ImageButtonView_Click";
                LogManager.SetExceptionDetails(ex, LogManager._stringObject, null);
                LogManager.LogException(ex, LogManager.LoggingCategory.General, LogManager.LoggingLevel.Error);
                CommonClass.Show(LogManager.ShowErrorDetail(ex));
            }
        }

        protected void ImageButtonDelete_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                RMC.BussinessService.BSRequestForTypes objectBSRequestForTypes = new RMC.BussinessService.BSRequestForTypes();
                GridViewRow grdRow = (GridViewRow)((ImageButton)sender).NamingContainer;

                objectBSRequestForTypes.DeleteRequestForTypes(Convert.ToInt32(GridViewRequestList.DataKeys[grdRow.RowIndex].Value));
                GridViewRequestList.DataBind();
            }
            catch (Exception ex)
            {
                ex.Data.Add("Events", "ImageButtonDelete_Click");
                ex.Data.Add("Page", "RequestList.ascx");
                LogManager._stringObject = "RequestList.ascx ---- ImageButtonView_Click";
                LogManager.SetExceptionDetails(ex, LogManager._stringObject, null);
                LogManager.LogException(ex, LogManager.LoggingCategory.General, LogManager.LoggingLevel.Error);
                CommonClass.Show(LogManager.ShowErrorDetail(ex));
            }
        }

        #endregion

    }
}