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
    public partial class ContactUsNotification : System.Web.UI.UserControl
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
                ex.Data.Add("Events", "ImageButtonDelete_Click");
                ex.Data.Add("Page", "ContactUsNotification.aspx");
                LogManager._stringObject = "ContactUsNotification.aspx ---- ImageButtonDelete_Click";
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
        protected void ImageButtonDelete_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                bool flag;
                int contactUsID;
                GridViewRow grdRow = (GridViewRow)((ImageButton)sender).NamingContainer;

                Label LabelContactUs = (Label)grdRow.FindControl("LabelContactUsID");
                Label LabelMessageType = (Label)grdRow.FindControl("LabelMessageType");
                flag = int.TryParse(LabelContactUs.Text, out contactUsID);

                if (flag)
                {
                    RMC.BussinessService.BSContactUs objectBSContactUs = new RMC.BussinessService.BSContactUs();

                    flag = objectBSContactUs.DeleteContactUs(contactUsID, LabelMessageType.Text);
                    if (flag)
                    {
                        GridViewContactUsNotification.DataBind();
                    }
                }
            }
            catch (Exception ex)
            {
                ex.Data.Add("Events", "ImageButtonDelete_Click");
                ex.Data.Add("Page", "ContactUsNotification.aspx");
                LogManager._stringObject = "ContactUsNotification.aspx ---- ImageButtonDelete_Click";
                LogManager.SetExceptionDetails(ex, LogManager._stringObject, null);
                LogManager.LogException(ex, LogManager.LoggingCategory.General, LogManager.LoggingLevel.Error);
                CommonClass.Show(LogManager.ShowErrorDetail(ex));
            }
        }
        #endregion

    }
}