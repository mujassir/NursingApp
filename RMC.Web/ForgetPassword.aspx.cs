using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using RMC.BussinessService;
using LogExceptions;

namespace RMC.Web
{
    public partial class ForgetPassword : System.Web.UI.Page
    {
        #region Variables

        //Bussiness Service Objects.
        RMC.BussinessService.BSUsers _objectBSUsers = null;
        BSLogin ObjectBSLogin;

        //Data Service
        List<RMC.DataService.UserInfo> _objectDSUserInfo = null;

        //Fundamental Properties      
        string _redirectUrl;
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// Used for Checking Whether Valid email address or not and get security ans and ques
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ButtonSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                _objectBSUsers = new BSUsers();
                _objectDSUserInfo = new List<RMC.DataService.UserInfo>();
                _objectDSUserInfo = _objectBSUsers.GetUserSecurityInformation(TextBoxUserName.Text);

                if (_objectDSUserInfo != null)
                {
                    if (_objectDSUserInfo.Count > 0)
                    {
                        PanelErrorMsg.Visible = false;
                        LabelErrorMsg.Text = "";
                        PanelSecurityQuestion.Visible = true;
                        RequiredFieldValidatorAnswer.Enabled = true;
                        ButtonSubmit.Visible = false;
                        ButtonSubmitAnswer.Visible = true;
                        TextBoxUserName.ReadOnly = true;
                        foreach (RMC.DataService.UserInfo objectUserInfo in _objectDSUserInfo)
                        {
                            TextBoxSecurityQuestion.Text = objectUserInfo.SecurityQuestion;
                            ViewState["Answer"] = objectUserInfo.SecurityAnswer;
                            ViewState["Password"] = objectUserInfo.Password;
                        }
                    }
                    else
                    {
                        ButtonSubmit.Visible = true;
                        ButtonSubmitAnswer.Visible = false;
                        PanelErrorMsg.Visible = false;
                        TextBoxUserName.ReadOnly = false;
                        RequiredFieldValidatorAnswer.Enabled = false;
                        CommonClass.Show("Email-Id Not Exist. Enter Valid Email Address");
                        //LabelErrorMsg.Text = "Email-Id Not Exist. Enter Valid Email Address";
                    }
                }
            }
            catch (Exception ex)
            {
                ex.Data.Add("Events", "ButtonSubmit");
                ex.Data.Add("Page", "ForgetPassword.aspx");
                LogManager._stringObject = "ForgetPassword.aspx ---- ButtonSubmit_Click ";
                LogManager.SetExceptionDetails(ex, LogManager._stringObject, null);
                LogManager.LogException(ex, LogManager.LoggingCategory.General, LogManager.LoggingLevel.Error);

            }
            finally
            {
                ObjectBSLogin = null;
            }

        }

        /// <summary>
        /// if Securtity and is correct then it redirect to user Home page
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ButtonSubmitAnswer_Click(object sender, EventArgs e)
        {
            try
            {
                bool flag = false ;
                ObjectBSLogin = new BSLogin();
                if (ViewState["Answer"] != null)
                {
                    if (ViewState["Answer"].ToString().ToLower() == TextBoxAnswer.Text.Trim().ToLower())
                    {
                        if (ViewState["Password"]!=null)
                        flag = ObjectBSLogin.CheckCredential(TextBoxUserName.Text, ViewState["Password"].ToString(), out _redirectUrl);
                        
                        if (flag)
                        {
                            Response.Redirect(_redirectUrl, false);
                        }
                    }
                    else
                    {

                        PanelErrorMsg.Visible = true;
                        LabelErrorMsg.Text = "Please  try  again or Call or Contact Rapid Modeling";
                    }
                }
            }
            catch (Exception ex)
            {
                ex.Data.Add("Events", "ButtonSubmitAnswer");
                ex.Data.Add("Page", "ForgetPassword.aspx");
                LogManager._stringObject = "ForgetPassword.aspx ---- ButtonSubmitAnswer_Click ";
                LogManager.SetExceptionDetails(ex, LogManager._stringObject, null);
                LogManager.LogException(ex, LogManager.LoggingCategory.General, LogManager.LoggingLevel.Error);

            }
            finally
            {
                ObjectBSLogin = null;
            }
        }

        /// <summary>
        /// Used for reset controls
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ButtonReset_Click(object sender, EventArgs e)
        {
            try
            {
                PanelErrorMsg.Visible = false;
                LabelErrorMsg.Text = "";
                PanelSecurityQuestion.Visible = false;
                RequiredFieldValidatorAnswer.Enabled = false;
                ButtonSubmit.Visible = true;
                ButtonSubmitAnswer.Visible = false;
                TextBoxSecurityQuestion.Text = "";
                TextBoxUserName.Text = "";
                TextBoxAnswer.Text = "";
                TextBoxUserName.ReadOnly = false;
            }
            catch (Exception ex)
            {
                ex.Data.Add("Events", "ButtonReset");
                ex.Data.Add("Page", "ForgetPassword.aspx");
                LogManager._stringObject = "ForgetPassword.aspx ---- ButtonReset_Click ";
                LogManager.SetExceptionDetails(ex, LogManager._stringObject, null);
                LogManager.LogException(ex, LogManager.LoggingCategory.General, LogManager.LoggingLevel.Error);

            }
        }

    }
}
