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
using RMC.BussinessService;
using LogExceptions;

namespace RMC.Web.Administrator
{
    public partial class Login : System.Web.UI.Page
    {
        #region Variable

        //Bussiness Service Objects.
        BSLogin ObjectBSLogin;

        #endregion
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!Page.IsPostBack)
                {
                    LabelErrorMsg.Visible = false;
                }
            }
            catch (Exception ex)
            {
                ex.Data.Add("Events", "Page Load");
                ex.Data.Add("Page", "Administrator/Login.aspx");
                LogManager._stringObject = "Login.aspx ---- Page_Load";
                LogManager.SetExceptionDetails(ex, LogManager._stringObject, null);
                LogManager.LogException(ex, LogManager.LoggingCategory.General, LogManager.LoggingLevel.Error);
                DisplayMessage(LogManager.ShowErrorDetail(ex), System.Drawing.Color.Red);
            }
        }

        #region Events

        /// <summary>
        /// Button Click Event to validate Username/Password.
        /// Created By : Amit Chawla
        /// Created Date : June 26, 2009
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ButtonSubmit_Click(object sender, EventArgs e)
        {
            bool flag;
            try
            {
                ObjectBSLogin = new BSLogin();
                if (Page.IsValid)
                {
                    flag = ObjectBSLogin.CheckCredential(TextBoxUserName.Text, TextBoxPassword.Text);
                    if (!flag)
                    {
                        DisplayMessage("Invalid Username/Password.", System.Drawing.Color.Red);
                    }
                    else
                    {
                        LabelErrorMsg.Visible = false;
                    }
                }
            }
            catch (Exception ex)
            {
                ex.Data.Add("Events", "ButtonSubmit");
                ex.Data.Add("Page", "Login.aspx");
                LogManager._stringObject = "Login.aspx ---- ButtonSubmit_Click ";
                LogManager.SetExceptionDetails(ex, LogManager._stringObject, null);
                LogManager.LogException(ex, LogManager.LoggingCategory.General, LogManager.LoggingLevel.Error);
                DisplayMessage(LogManager.ShowErrorDetail(ex), System.Drawing.Color.Red);
            }
            finally
            {
                ObjectBSLogin = null;
            }
        }
        //End Of ButtonSubmit_Click Event.


      

        #endregion


        #region Private Methods

        //Use to Display message of Login Failure.
        private void DisplayMessage(string msg, System.Drawing.Color color)
        {
            try
            {
                LabelErrorMsg.Text = msg;
                LabelErrorMsg.ForeColor = color;
                LabelErrorMsg.Visible = true;
            }
            catch (Exception ex)
            {
                ex.Data.Add("Function", "DisplayMessage");
                ex.Data.Add("Class", "Login");
                throw ex;
            }
        }
        //End Of DisplayMessage Methods.

        #endregion
    }
}
