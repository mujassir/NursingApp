using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using LogExceptions;

namespace RMC.Web.UserControls
{
    public partial class GetUsers : System.Web.UI.UserControl
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
                LogManager._stringObject = "GetUsers.ascx.cs ---- ";
                LogManager.SetExceptionDetails(ex, LogManager._stringObject, null);
                LogManager.LogException(ex, LogManager.LoggingCategory.General, LogManager.LoggingLevel.Error);
                CommonClass.Show(LogManager.ShowErrorDetail(ex));
            }
        }

        protected void ButtonSearchUser_Click(object sender, EventArgs e)
        {
            try
            {
                ListBoxShowUser.DataBind();
            }
            catch (Exception ex)
            {
                ex.Data.Add("Events", "ButtonSearchUser_Click");
                LogManager._stringObject = "GetUsers.ascx.cs ---- ";
                LogManager.SetExceptionDetails(ex, LogManager._stringObject, null);
                LogManager.LogException(ex, LogManager.LoggingCategory.General, LogManager.LoggingLevel.Error);
                CommonClass.Show(LogManager.ShowErrorDetail(ex));
            }
        }
        protected void ListBoxShowUser_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (ListBoxShowUser.SelectedIndex > -1)
                {
                    Response.Redirect("ActivateUser.aspx?UserId=" + ListBoxShowUser.SelectedItem.Value, false);
                }
            }
            catch (Exception ex)
            {
                ex.Data.Add("Events", "ListBoxShowUser_SelectedIndexChanged");
                LogManager._stringObject = "GetUsers.ascx.cs ---- ";
                LogManager.SetExceptionDetails(ex, LogManager._stringObject, null);
                LogManager.LogException(ex, LogManager.LoggingCategory.General, LogManager.LoggingLevel.Error);
                CommonClass.Show(LogManager.ShowErrorDetail(ex));
            }           
        }

       

        protected void ButtonReset_Click(object sender, EventArgs e)
        {
            try
            {
                TextBoxSearchUser.Text = string.Empty;
                ListBoxShowUser.DataBind();
            }
            catch (Exception ex)
            {
                ex.Data.Add("Events", "ButtonReset_Click");
                LogManager._stringObject = "GetUsers.ascx.cs ---- ";
                LogManager.SetExceptionDetails(ex, LogManager._stringObject, null);
                LogManager.LogException(ex, LogManager.LoggingCategory.General, LogManager.LoggingLevel.Error);
                CommonClass.Show(LogManager.ShowErrorDetail(ex));
            }
        }

        protected void ButtonSearchAddANewUser_Click(object sender, EventArgs e)
        {
            try
            {

            }
            catch (Exception ex)
            {
                ex.Data.Add("Events", "ButtonSearchAddANewUser_Click");
                LogManager._stringObject = "GetUsers.ascx.cs ---- ";
                LogManager.SetExceptionDetails(ex, LogManager._stringObject, null);
                LogManager.LogException(ex, LogManager.LoggingCategory.General, LogManager.LoggingLevel.Error);
                CommonClass.Show(LogManager.ShowErrorDetail(ex));
            }
        }

        #endregion

        #region Private Methods
        
        /// <summary>
        /// Use to Display message of Login Failure.       
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="color"></param>
        private void DisplayMessage(string msg, System.Drawing.Color color)
        {
            try
            {
                LabelErrorMsg.Text = msg;
                LabelErrorMsg.Visible = false;
            }
            catch (Exception ex)
            {
                ex.Data.Add("Function", "DisplayMessage");
                ex.Data.Add("Class", "GetUsers");
                throw ex;
            }
        }

        #endregion

    }
}