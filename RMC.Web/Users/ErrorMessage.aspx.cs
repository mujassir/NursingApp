using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Xml.Linq;
using LogExceptions;

namespace RMC.Web.Users
{
    public partial class ErrorMessage : System.Web.UI.Page
    {

        #region Events

        /// <summary>
        /// Show Error Message.
        /// Created By : Davinder Kumar.
        /// Creation Date : July 24, 2009.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (CommonClass.SessionInfomation.ErrorMessage != string.Empty)
                {
                    DisplayMessage(CommonClass.SessionInfomation.ErrorMessage);
                }
                else
                {
                    DisplayMessage("Error Occured! Contact your site Administrator.");
                }

                CommonClass.SessionInfomation.ErrorMessage = string.Empty;
            }
            catch (Exception ex)
            {
                ex.Data.Add("Events", "Page_Load");
                ex.Data.Add("Page", "ErrorMessage.aspx");
                LogManager._stringObject = "ErrorMessage.aspx ---- Page_Load";
                LogManager.SetExceptionDetails(ex, LogManager._stringObject, null);
                LogManager.LogException(ex, LogManager.LoggingCategory.General, LogManager.LoggingLevel.Error);
                DisplayMessage(LogManager.ShowErrorDetail(ex));
            }
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Display Error Message.
        /// Created By : Davinder Kumar.
        /// Creation Date : July 23, 2009.
        /// </summary>
        /// <param name="message"></param>
        /// <param name="color"></param>
        private void DisplayMessage(string message)
        {
            try
            {
                LiteralErrorMessage.Text = message;
            }
            catch (Exception ex)
            {
                ex.Data.Add("Function", "DisplayMessage");
                ex.Data.Add("Class", "UserRegistration");
                throw ex;
            }
        }

        #endregion

    }
}
