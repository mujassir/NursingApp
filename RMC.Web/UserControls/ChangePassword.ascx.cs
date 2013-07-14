using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using RMC.BussinessService;
using LogExceptions;
using System.IO;

namespace RMC.Web.UserControls
{
    public partial class ChangePassword : System.Web.UI.UserControl
    {

        #region Variables

        //Bussiness Service Objects.
        RMC.BussinessService.BSUsers _objectBSUsers = null;
        RMC.BussinessService.BSLogin _objectBSLogin = null;
        
        #endregion
        
        #region Properties
        
        /// <summary>
        /// Return Logged In user Password
        /// <CreatedBy>Raman</CreatedBy>
        /// <CreatedOn>August 18, 2009</CreatedOn>
        /// </summary>
        public string Password
        {
            get
            {
                if (Session["UserInformation"] != null)
                {
                    //return ((List<RMC.DataService.UserInfo>)(BSSerialization.Deserialize<List<RMC.DataService.UserInfo>>(HttpContext.Current.Session["UserInformation"] as MemoryStream))).Count > 0 ? CommonClass.UserInformation.Password : "";
                    return ((List<RMC.DataService.UserInfo>)(BSSerialization.Deserialize<List<RMC.DataService.UserInfo>>(HttpContext.Current.Session["UserInformation"] as MemoryStream))).Count > 0 ? CommonClass.UserInformation.Password : "";
                }
                else
                {
                    return "";
                }
            }
        }
        
        public int UserID
        {
            get
            {
                if (Session["UserInformation"] != null)
                {
                    //return ((List<RMC.DataService.UserInfo>)(BSSerialization.Deserialize<List<RMC.DataService.UserInfo>>(HttpContext.Current.Session["UserInformation"] as MemoryStream))).Count > 0 ? CommonClass.UserInformation.UserID : 0;
                    return ((List<RMC.DataService.UserInfo>)(BSSerialization.Deserialize<List<RMC.DataService.UserInfo>>(HttpContext.Current.Session["UserInformation"] as MemoryStream))).Count > 0 ? CommonClass.UserInformation.UserID : 0;
                }
                else
                {
                    return 0;
                }
            }
        } 

        #endregion

        #region Events
        
        protected void Page_Load(object sender, EventArgs e)
        {
            TextBoxOldPassword.Focus();
        }

        protected void ButtonSave_Click(object sender, EventArgs e)
        {

            try
            {
                bool flag = false;
                LabelErrorMsg.ForeColor = System.Drawing.Color.Red;
                if (Page.IsValid)
                {
                    if (Password == TextBoxOldPassword.Text)
                    {
                        PanelErrorMsg.Visible = false;
                        LabelErrorMsg.Text = "";
                        _objectBSUsers = new BSUsers();

                        flag = _objectBSUsers.UpdateUserPassword(TextBoxNewPassword.Text, UserID);
                        if (flag)
                        {
                            PanelErrorMsg.Visible = false;
                            CommonClass.Show("Password Updated Successfully");
                            //LabelErrorMsg.Text = "Password Update Successfully";                           
                            LabelErrorMsg.ForeColor = System.Drawing.Color.Green;
                            if (UserID > 0)
                            {
                                _objectBSLogin = new RMC.BussinessService.BSLogin();
                                _objectBSLogin.UpdateUserInformationInSession(UserID);
                            }
                        }
                        else
                        {
                            PanelErrorMsg.Visible = false;
                            CommonClass.Show("Password Not Updated Successfully");
                            //LabelErrorMsg.Text = "Password  Not Updated Successfully";
                        }
                    }
                    else
                    {
                        PanelErrorMsg.Visible = false;
                        CommonClass.Show("Old Password Not Matched.");
                        //LabelErrorMsg.Text = "Old Password Not Matched.";
                    }
                }
            }
            catch (Exception ex)
            {
                ex.Data.Add("Events", "ButtonSave_Click");
                ex.Data.Add("Page", "ChangePassword.ascx");
                LogManager._stringObject = "ChangePassword.ascx ---- ButtonSave_Click ";
                LogManager.SetExceptionDetails(ex, LogManager._stringObject, null);
                LogManager.LogException(ex, LogManager.LoggingCategory.General, LogManager.LoggingLevel.Error);
                CommonClass.Show(LogManager.ShowErrorDetail(ex));
            }

        }

        protected void ButtonReset_Click(object sender, EventArgs e)
        {
            try
            {
                PanelErrorMsg.Visible = false;
                LabelErrorMsg.Text = "";
                TextBoxOldPassword.Text = "";
                TextBoxNewPassword.Text = "";
                TextBoxConfirmPassword.Text = "";
            }
            catch (Exception ex)
            {
                ex.Data.Add("Events", "ButtonReset");
                ex.Data.Add("Page", "ChangePassword.ascx");
                LogManager._stringObject = "ChangePassword.ascx ---- ButtonReset_Click ";
                LogManager.SetExceptionDetails(ex, LogManager._stringObject, null);
                LogManager.LogException(ex, LogManager.LoggingCategory.General, LogManager.LoggingLevel.Error);
                CommonClass.Show(LogManager.ShowErrorDetail(ex));
            }
        }

        #endregion

    }
}