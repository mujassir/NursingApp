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
using RMC.BussinessService;

namespace RMC.Web.UserControls
{
    public partial class UserRegistration : System.Web.UI.UserControl
    {

        #region Variables

        //Bussiness Service Objects.  
        BSUsers _objectBSUsers;
        BSUserType _objectBSUserType;

        //Data Service Objects.
        RMC.DataService.UserInfo _objectUserInfo;
        int _hospitalDemographicId;
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
                DisplayMessage(LogManager.ShowErrorDetail(ex), System.Drawing.Color.Red);
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
            try
            {
                if (Page.IsValid)
                {
                    if (CheckBoxActivateUser.Checked)
                    {
                        bool flagDelete = false;
                        int userID = 0;
                        _objectBSUsers = new BSUsers();
                        if (!_objectBSUsers.ExistUserEmailId(TextBoxPrimaryEmail.Text.Trim(), out flagDelete, out userID))
                        {
                            if (flagDelete)
                            {
                                _objectBSUsers.DeleteUserByUserID(userID);
                            }
                            bool flag = false;
                            _objectBSUsers = new BSUsers();

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
                                flag = _objectBSUsers.InsertUsersInfomation(SavePrimaryUserInfo(), SaveMultiUserDemographic());
                                if (flag)
                                {
                                    //DisplayMessage("Save User Information Successfully.", System.Drawing.Color.Green);
                                    CommonClass.Show("Save User Information Successfully.");
                                    ResetControls();
                                }
                                else
                                {
                                    CommonClass.Show("Fail to Save User Information.");
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
                    else
                    {
                        CommonClass.Show("Must Select the Activate a User Checkbox.");
                        //DisplayMessage("Must Select the Activate a User Checkbox.", System.Drawing.Color.Red);
                    }
                }
            }
            catch (Exception ex)
            {
                ex.Data.Add("Events", "ButtonSave_Click");
                ex.Data.Add("Page", "UserRegistration.aspx");
                LogManager._stringObject = "UserRegistration.aspx ---- ButtonSave_Click";
                LogManager.SetExceptionDetails(ex, LogManager._stringObject, null);
                LogManager.LogException(ex, LogManager.LoggingCategory.General, LogManager.LoggingLevel.Error);
                DisplayMessage(LogManager.ShowErrorDetail(ex), System.Drawing.Color.Red);
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
                //if (HttpContext.Current.User.IsInRole("superadmin"))
                //{
                //    //if (Request.QueryString["HospitalDemographicId"] != null)
                //    //{
                //    //    _hospitalDemographicId = Convert.ToInt32(Request.QueryString["HospitalDemographicId"]);
                //    //    ImageButtonBack.PostBackUrl = "~/Administrator/HospitalDemographicDetail.aspx?HospitalDemographicId=" + _hospitalDemographicId;
                //    //}
                //    //else
                //    //{
                //    //    ImageButtonBack.PostBackUrl = "~/Administrator/GetUsers.aspx";
                //    //}

                //}
                if (!Page.IsPostBack)
                {
                    CommonClass objectCommonClass = new CommonClass();
                    objectCommonClass.BackButtonUrl = Request.UrlReferrer.AbsoluteUri;
                }
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
                DisplayMessage(LogManager.ShowErrorDetail(ex), System.Drawing.Color.Red);
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
                LogManager._stringObject = "UserRegistration.ascx.cs ---- ";
                LogManager.SetExceptionDetails(ex, LogManager._stringObject, null);
                LogManager.LogException(ex, LogManager.LoggingCategory.General, LogManager.LoggingLevel.Error);
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
                CheckBoxActivateUser.Checked = true;
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
            _objectBSUserType = new BSUserType();
            _objectUserInfo = new RMC.DataService.UserInfo();
            try
            {
                //Name Of UserType are SuperAdmin, Admin, PowerUser, User.
                userTypeID = _objectBSUserType.GetUserTypeByName("Admin");
                if (userTypeID > 0)
                {
                    _objectUserInfo.CreatedBy = TextBoxPrimaryFirstName.Text.Trim() + " " + TextBoxPrimaryLastName.Text.Trim();
                    _objectUserInfo.CreatedDate = DateTime.Now;
                    _objectUserInfo.Email = TextBoxPrimaryEmail.Text.Trim();
                    _objectUserInfo.Fax = TextBoxPrimaryFax.Text.Trim();
                    _objectUserInfo.FirstName = TextBoxPrimaryFirstName.Text.Trim();
                    _objectUserInfo.CompanyName = TextBoxCompanyName.Text.Trim();
                    _objectUserInfo.IsActive = CheckBoxActivateUser.Checked;
                    _objectUserInfo.LastName = TextBoxPrimaryLastName.Text.Trim();
                    _objectUserInfo.Password = TextBoxPrimaryPassword.Text.Trim();
                    _objectUserInfo.Phone = TextBoxPrimaryPhone.Text.Trim();
                    _objectUserInfo.SecurityAnswer = TextBoxPrimarySecurityAnswer.Text.Trim();
                    _objectUserInfo.SecurityQuestion = TextBoxPrimarySecurityQuestion.Text.Trim();
                    _objectUserInfo.UserTypeID = userTypeID;
                    _objectUserInfo.IsDeleted = false;
                    _objectUserInfo.UserActivationRequest = string.Empty;
                }
                else
                {
                    _objectUserInfo = null;
                }
            }
            catch (Exception ex)
            {
                ex.Data.Add("Function", "SaveUserInfo");
                ex.Data.Add("Class", "UserRegistration");
                throw ex;
            }
            finally
            {
                _objectBSUserType = null;
            }

            return _objectUserInfo;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private RMC.DataService.MultiUserDemographic SaveMultiUserDemographic()
        {
            try
            {
                RMC.DataService.MultiUserDemographic objectMultiUserDemographic = new RMC.DataService.MultiUserDemographic();
                RMC.BussinessService.BSPermission objectPermission = new BSPermission();

                objectMultiUserDemographic.HospitalDemographicID = Convert.ToInt32(Request.QueryString["HospitalDemographicId"]);
                //Set Permission for ownership.
                objectMultiUserDemographic.PermissionID = Convert.ToInt32(DropDownListPermission.SelectedValue);
                objectMultiUserDemographic.IsDeleted = false;
                objectMultiUserDemographic.CreatedBy = "SuperAdmin";
                objectMultiUserDemographic.CreatedDate = DateTime.Now;

                return objectMultiUserDemographic;
            }
            catch (Exception ex)
            {
                ex.Data.Add("Function", "SaveHospitalDemographicInfo");
                ex.Data.Add("Class", "UserRegistration");
                throw ex;
            }
        }

        #endregion

    }
}