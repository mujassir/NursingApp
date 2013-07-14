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
    public partial class UserNotification : System.Web.UI.UserControl
    {
        #region Events
        
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {

            }
            catch (Exception ex)
            {
                ex.Data.Add("Events", "Page_Load");
                ex.Data.Add("Page", "UserNotification.ascx");
                LogManager._stringObject = "UserNotification.ascx ---- Page_Load";
                LogManager.SetExceptionDetails(ex, LogManager._stringObject, null);
                LogManager.LogException(ex, LogManager.LoggingCategory.General, LogManager.LoggingLevel.Error);
                CommonClass.Show(LogManager.ShowErrorDetail(ex));            
            }
        }

        protected void ObjectDataSourceNotification_Selecting(object sender, ObjectDataSourceSelectingEventArgs e)
        {
            try
            {
                e.InputParameters[0] = CommonClass.UserInformation.UserID;
            }
            catch (Exception ex)
            {
                ex.Data.Add("Events", "ObjectDataSourceNotification_Selecting");
                ex.Data.Add("Page", "UserNotification.ascx");
                LogManager._stringObject = "UserNotification.ascx ---- ObjectDataSourceNotification_Selecting";
                LogManager.SetExceptionDetails(ex, LogManager._stringObject, null);
                LogManager.LogException(ex, LogManager.LoggingCategory.General, LogManager.LoggingLevel.Error);
                CommonClass.Show(LogManager.ShowErrorDetail(ex));
            }
        }

        protected void ImageButtonDelete_Click(object sender, ImageClickEventArgs e)
        {
            bool flag = false;
            try
            {
                RMC.BussinessService.BSNewsLetter objectBSNewsLetter = new RMC.BussinessService.BSNewsLetter();
                GridViewRow grdRow = (GridViewRow)((ImageButton)sender).NamingContainer;

                flag = objectBSNewsLetter.DeleteNewLetter(Convert.ToInt32(GridViewNotification.DataKeys[grdRow.RowIndex].Value));
                if (flag)
                {
                    GridViewNotification.DataBind();
                }
            }
            catch (Exception ex)
            {
                ex.Data.Add("Events", "ImageButtonDelete_Click");
                ex.Data.Add("Page", "UserNotification.ascx");
                LogManager._stringObject = "UserNotification.ascx ---- ImageButtonDelete_Click";
                LogManager.SetExceptionDetails(ex, LogManager._stringObject, null);
                LogManager.LogException(ex, LogManager.LoggingCategory.General, LogManager.LoggingLevel.Error);
                CommonClass.Show(LogManager.ShowErrorDetail(ex));
            }
        } 

        #endregion

    }
}