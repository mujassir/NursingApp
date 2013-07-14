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
using RMC.BussinessService;
using System.IO;
using RMC.DataService;

namespace RMC.Web
{
    public partial class UserRegistration : System.Web.UI.Page
    {

        #region Variables

        //Bussiness Service Objects  
        BSUsers _objectBSUsers;
        BSUserType _objectBSUserType;
        BSLogin ObjectBSLogin;

        //Fundamental Data Types.
        string _redirectUrl;
        //Data Service Objects.
        RMC.DataService.UserInfo _objectUserInfo;

        #endregion

        #region Events

        /// <summary>
        /// Reset all controls.
        /// Created By : Davinder Kumar
        /// Creation Date : July 10, 2009
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ButtonReset_Click(object sender, EventArgs e)
        {
            try
            {
                ResetControls();
                LabelErrorMsg.Text = string.Empty;
                PanelErrorMsg.Visible = false;
            }
            catch (Exception ex)
            {
                ex.Data.Add("Events", "ButtonReset_Click");
                ex.Data.Add("Page", "UserRegistration.aspx");
                LogManager._stringObject = "UserRegistration.aspx ---- ButtonReset_Click";
                LogManager.SetExceptionDetails(ex, LogManager._stringObject, null);
                LogManager.LogException(ex, LogManager.LoggingCategory.General, LogManager.LoggingLevel.Error);
                //DisplayMessage(LogManager.ShowErrorDetail(ex), System.Drawing.Color.Red);
                CommonClass.Show(LogManager.ShowErrorDetail(ex));
            }
        }

        /// <summary>
        /// Save Data of Hospital Demographic Detail.
        /// Created By : Davinder Kumar.
        /// Creation Date : July 10, 2009.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ButtonSave_Click(object sender, EventArgs e)
        {
            BSUsers _objectBSUsers = new BSUsers();
            BSLogin ObjectBSLogin = new BSLogin();
            try
            {
                //cm start
                if (Page.IsValid)
                {
                    bool flagDelete = false;
                    int userID = 0;

                    if (!_objectBSUsers.ExistUserEmailId(TextBoxPrimaryEmail.Text.Trim(), out flagDelete, out userID))
                    {
                        bool flag = false;
                        //BSUsers _objectBSUsers = new BSUsers();
                        if (flagDelete)
                        {
                            _objectBSUsers.DeleteUserByUserID(userID);
                        }

                        CaptchaControlImage.ValidateCaptcha(TextBoxCaptchaText.Text);
                        if (!CaptchaControlImage.UserValidated)
                        {
                            CommonClass.Show("Please Enter Correct Code");
                            //LabelErrorMsg.Text = "Please Enter Correct Code";
                            PanelErrorMsg.Visible = false;
                            PanelErrorMsg.ForeColor = System.Drawing.Color.Red;
                            return;
                        }
                        else
                        {
                            int currentUserID = 0;
                            string message = string.Empty;
                            flag = _objectBSUsers.InsertUsersInfomationByUserID(SavePrimaryUserInfo(), out currentUserID);
                            if (flag)
                            {
                                // flag = ObjectBSLogin.CheckCredential(TextBoxPrimaryEmail.Text, TextBoxPrimaryPassword.Text, out _redirectUrl);
                                //if (!flag)
                                //{
                                //    CommonClass.Show("User Information Not Save Successfully.");
                                //    //DisplayMessage("User Information Not Save Successfully.", System.Drawing.Color.Green);  
                                //}
                                //else
                                // {
                                bool flagActivation = false;
                                flagActivation = _objectBSUsers.UpdateRequestActivation("Activation Request", currentUserID, out message);
                                if (!flagActivation)
                                {
                                    CommonClass.Show(message);
                                    //DisplayMessage(message, System.Drawing.Color.Red);
                                }
                                //Response.Redirect(_redirectUrl, false);

                                string script = string.Empty;
                                //string path = ResolveUrl("~login.aspx");
                                script = "var ConfirmationMessage = confirm('Your application to the national Benchmarking Database has been received by the system. However, your application must be approved by the system administrator. The system administrator has been notified by email of your request and should approve you in the next 24 hours. If you don’t receive an approval message in the next 24 hours, then please contact Nelson Lee at nlee@rapidmodeling.com for assistance. Thank you for participating in the National Benchmarking Database.');";
                                script = script + "if(ConfirmationMessage){ window.open('login.aspx', '_self','location=1,status=0,scrollbars=1, width=500,height=300,resizable=no')}";
                                Page.ClientScript.RegisterStartupScript(GetType(), "msgbox", script, true);
                                //}
                                //DisplayMessage("Save User Information Successfully.", System.Drawing.Color.Green);
                                ResetControls();
                            }
                            else
                            {
                                CommonClass.Show("Failed to Save User Information.");
                                //DisplayMessage("Fail to Save User Information.", System.Drawing.Color.Red);
                            }
                        }
                    }
                    //End of Email Check.
                    else
                    {
                        CommonClass.Show("Email already Registered.");
                        //DisplayMessage("Email already Registered.", System.Drawing.Color.Red);
                    }
                }
                //cm end
                
                //string script = string.Empty;
                ////string path = ResolveUrl("~login.aspx");
                //script = "var ConfirmationMessage = confirm('Your application to the national Benchmarking Database has been received by the system."+"</br>"+" However, your application must be approved by the system administrator. The system administrator has been notified by email of your request and should approve you in the next 24 hours. If you don’t receive an approval message in the next 24 hours, then please contact Nelson Lee at nlee@rapidmodeling.com for assistance. Thank you for participating in the National Benchmarking Database.');";
                //script = script + "if(ConfirmationMessage){ window.open('login.aspx', '_self')}";
                //Page.ClientScript.RegisterStartupScript(GetType(), "msgbox", script, true);
                ////}
                ////DisplayMessage("Save User Information Successfully.", System.Drawing.Color.Green);
                //ResetControls();
            }
            catch (Exception ex)
            {
                ex.Data.Add("Events", "ButtonSave_Click");
                ex.Data.Add("Page", "UserRegistration.aspx");
                LogManager._stringObject = "UserRegistration.aspx ---- ButtonSave_Click";
                LogManager.SetExceptionDetails(ex, LogManager._stringObject, null);
                LogManager.LogException(ex, LogManager.LoggingCategory.General, LogManager.LoggingLevel.Error);
                //DisplayMessage(LogManager.ShowErrorDetail(ex), System.Drawing.Color.Red);
                CommonClass.Show(LogManager.ShowErrorDetail(ex));

            }
        }


        /// <summary>
        /// Page Load Events.
        /// Created By : Davinder Kumar
        /// Creation Date : July 10, 2009
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                //Add Javascript Event to restrict the special character in Textboxes.                
                TextBoxPrimaryFirstName.Attributes.Add("onKeyDown", "return validateNonNumber(event);");
                TextBoxPrimaryLastName.Attributes.Add("onKeyDown", "return validateNonNumber(event);");
            }
            catch (Exception ex)
            {
                ex.Data.Add("Events", "Page_Load");
                ex.Data.Add("Page", "UserRegistration.aspx");
                LogManager._stringObject = "UserRegistration.aspx ---- Page_Load";
                LogManager.SetExceptionDetails(ex, LogManager._stringObject, null);
                LogManager.LogException(ex, LogManager.LoggingCategory.General, LogManager.LoggingLevel.Error);
                //DisplayMessage(LogManager.ShowErrorDetail(ex), System.Drawing.Color.Red);
                CommonClass.Show(LogManager.ShowErrorDetail(ex));
            }
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Use to Display message of Login Failure.
        /// Created By : Davinder Kumar.
        /// Creation Date : July 10, 2009.
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="color"></param>
        private void DisplayMessage(string msg, System.Drawing.Color color)
        {
            try
            {
                LabelErrorMsg.Text = msg;
                PanelErrorMsg.ForeColor = color;
                PanelErrorMsg.Visible = true;
            }
            catch (Exception ex)
            {
                ex.Data.Add("Function", "DisplayMessage");
                ex.Data.Add("Class", "UserRegistration");
                throw ex;
            }
        }

        /// <summary>
        /// Reset all control status.
        /// Created By : Davinder Kumar.
        /// Creation Date : July 10, 2009.
        /// </summary>
        private void ResetControls()
        {
            try
            {
                TextBoxCaptchaText.Text = string.Empty;
                TextBoxCompanyName.Text = string.Empty;
                TextBoxPrimaryEmail.Text = string.Empty;
                TextBoxPrimaryFax.Text = string.Empty;
                TextBoxPrimaryFirstName.Text = string.Empty;
                TextBoxPrimaryLastName.Text = string.Empty;
                TextBoxPrimaryPhone.Text = string.Empty;
                TextBoxPrimarySecurityAnswer.Text = string.Empty;
                TextBoxPrimarySecurityQuestion.Text = string.Empty;
            }
            catch (Exception ex)
            {
                ex.Data.Add("Function", "ResetControls");
                ex.Data.Add("Class", "UserRegistration");
                throw ex;
            }
        }

        /// <summary>
        /// Save Primary user information In a Data service object.
        /// Created By : Davinder Kumar.
        /// Creation Date : July 10, 2009.
        /// </summary>
        /// <returns></returns>
        private RMC.DataService.UserInfo SavePrimaryUserInfo()
        {

            int userTypeID;
            BSUserType _objectBSUserType = new BSUserType();
            UserInfo _objectUserInfo = new RMC.DataService.UserInfo();
            try
            {
            //    //Name Of UserType are SuperAdmin, Admin, PowerUser, User.
                userTypeID = _objectBSUserType.GetUserTypeByName("Admin");
                if (userTypeID > 0)
                {
                    _objectUserInfo.CreatedBy = TextBoxPrimaryFirstName.Text.Trim() + " " + TextBoxPrimaryLastName.Text.Trim();
                    _objectUserInfo.CreatedDate = DateTime.Now;
                    _objectUserInfo.Email = TextBoxPrimaryEmail.Text.Trim();
                    _objectUserInfo.Fax = TextBoxPrimaryFax.Text.Trim();
                    _objectUserInfo.FirstName = TextBoxPrimaryFirstName.Text.Trim();
                    _objectUserInfo.CompanyName = TextBoxCompanyName.Text.Trim();
                    _objectUserInfo.IsActive = false;
                    _objectUserInfo.LastName = TextBoxPrimaryLastName.Text.Trim();
                    _objectUserInfo.Password = TextBoxPrimaryPassword.Text.Trim();
                    _objectUserInfo.Phone = TextBoxPrimaryPhone.Text.Trim();
                    _objectUserInfo.SecurityAnswer = TextBoxPrimarySecurityAnswer.Text.Trim();
                    _objectUserInfo.SecurityQuestion = TextBoxPrimarySecurityQuestion.Text.Trim();
                    _objectUserInfo.UserTypeID = userTypeID;
                    _objectUserInfo.IsDeleted = false;
                    _objectUserInfo.UserActivationRequest = string.Empty;

                    // added by Raman 
                    // This code is added for Adding a Flag as Owner or Readonly in the database
                    // This is stored as nvarchar, as in future it may increase like roles Owner, Readonly, or something else
                    // 

                    if (rbtOwner.Checked)
                    {
                        _objectUserInfo.AccessRequest = "Owner";
                    }
                    else
                    {
                        _objectUserInfo.AccessRequest = "ReadOnly";

                    }

                }
                else
                {
                   // _objectUserInfo = null;
                }
            }
            catch (Exception ex)
            {
                ex.Data.Add("Function", "SaveUserInfo");
                ex.Data.Add("Class", "UserRegistration");
                // throw ex;

            }
            finally
            {
                //_objectBSUserType = null;
            }

            return _objectUserInfo;
        }

        #endregion

        public bool CreateUser
        {
            get
            {
                if (Request.QueryString["CreateUser"] != null)
                {
                    return Convert.ToString(Request.QueryString["CreateUser"]).ToUpper() == "Y";
                }
                else
                {
                    return false;
                }
            }
        }
        int _userId = 0;

        private int UserId
        {
            get
            {
                if (CreateUser == false)
                {
                    if (Request.QueryString["UserId"] == null)
                    {
                        _userId = LoggedInUserInfo.UserID;
                    }
                    else
                    {
                        int.TryParse(Request.QueryString["UserId"], out _userId);
                    }
                    return _userId;
                }
                else
                {
                    return 0;
                }
            }
            set
            {
                _userId = value;
            }
        }

        private RMC.DataService.UserInfo LoggedInUserInfo
        {
            get
            {
                if (Session["UserInformation"] == null)
                {
                    return null;
                }
                else
                {
                    //return ((List<RMC.DataService.UserInfo>)(BSSerialization.Deserialize<List<RMC.DataService.UserInfo>>(HttpContext.Current.Session["UserInformation"] as MemoryStream))).Count > 0 ? CommonClass.UserInformation : null;
                    return ((List<RMC.DataService.UserInfo>)(BSSerialization.Deserialize<List<RMC.DataService.UserInfo>>(HttpContext.Current.Session["UserInformation"] as MemoryStream))).Count > 0 ? CommonClass.UserInformation : null;
                }
            }
        }
    }
    //End Of UserRegistration Class.
}
//End Of Namespace.
