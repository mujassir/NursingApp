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
using System.Collections.Generic;
using System.IO;

namespace RMC.Web
{
    /// <summary>
    /// Page displaying User Profile
    /// <Author>Raman</Author>
    /// <CreatedOn>July 22, 2009</CreatedOn>
    /// </summary>
    public partial class UserProfile : System.Web.UI.UserControl
    {

        #region Variables
        int _userId = 0;
        private int _hospitalInfoId, _permissionID;
        //Bussiness Service Objects.
        RMC.BussinessService.BSUsers _objectBSUsers = null;
        //Bussiness Service Objects.
        RMC.BussinessService.BSLogin _objectBSLogin = null;//if logged in user has modified his detail  then will use to update userinformation in session
        RMC.BussinessService.BSTreeView objTreeView = null;
        //Data Service Objects
        RMC.DataService.UserInfo _objectUserInfo = null;
        #endregion

        #region Properties

        /// <summary>
        /// Return Role or type of Logged In user
        /// </summary>
        private string UserType
        {
            get
            {
                if (HttpContext.Current.User != null)
                {
                    if (HttpContext.Current.User.Identity.IsAuthenticated)
                    {
                        if (HttpContext.Current.User.Identity is FormsIdentity)
                        {
                            FormsIdentity id = (FormsIdentity)(HttpContext.Current.User.Identity);
                            FormsAuthenticationTicket ticket = id.Ticket;
                            string userData = ticket.UserData;
                            string[] roles = userData.Split(',');
                            try
                            {
                                return Convert.ToString(roles[0]);
                            }
                            catch { }
                        }
                    }
                }
                return "";
            }
        }

        //private List<RMC.DataService.UserInfo> SetUserinformation
        //{
        //    set
        //    {
        //        SelectedUserInformation = ((List<RMC.DataService.UserInfo>)value).Count > 0 ? ((List<RMC.DataService.UserInfo>)value)[0] : null;
        //    }
        //}
        /// <summary>
        /// Return Logged In user information
        /// </summary>
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

        //private RMC.DataService.UserInfo SelectedUserInformation
        //{
        //    get
        //    {
        //        if (Session["SelectedUserInformation"] != null)
        //        {
        //            return ((RMC.DataService.UserInfo)Session["SelectedUserInformation"]);
        //        }
        //        else
        //        {
        //            return null;
        //        }
        //    }
        //    set
        //    {
        //        Session["SelectedUserInformation"] = value;
        //    }
        //}
        /// <summary>
        /// Return UserId from querystring if exist else return userid of logged in user
        /// </summary>
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

        /// <summary>
        /// Return Activeuser i.e. email of logged in user
        /// </summary>
        public string ActiveUser
        {
            get
            {
                return LoggedInUserInfo.Email;
                //return ((List<RMC.DataService.UserInfo>)(BSSerialization.Deserialize<List<RMC.DataService.UserInfo>>(HttpContext.Current.Session["UserInformation"] as MemoryStream))).Count > 0 ? CommonClass.UserInformation.Email : "";
            }
        }

        /// <summary>
        /// Return true if logged in user is SuperAdmin else false
        /// </summary>
        public bool IsSuperAdmin
        {
            get
            {
                return UserType.ToLower() == Convert.ToString(RMC.Web.UserRole.SuperAdmin).ToLower();
                //return ((List<RMC.DataService.UserInfo>)(BSSerialization.Deserialize<List<RMC.DataService.UserInfo>>(HttpContext.Current.Session["UserInformation"] as MemoryStream))).Count > 0 ? CommonClass.UserInformation.UserTypeID==Convert.ToInt32(RMC.Web.UserRole.SuperAdmin)?true :false:false;
            }
        }

        /// <summary>
        /// If Super Admin has come on this page for creating New user then will return true else false
        /// </summary>
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

        /// <summary>
        /// Return passed HospitalInfoId
        /// <Author>Raman</Author>
        /// <createdOn>7 August, 2009</createdOn>
        /// </summary>
        private int HospitalInfoId
        {
            get
            {

                return _hospitalInfoId = Request.QueryString["HospitalInfoId"] != null ? Convert.ToInt32(Request.QueryString["HospitalInfoId"]) : 0;

            }
            set
            {

                _hospitalInfoId = value;
            }
        }

        /// <summary>
        /// Return passed HospitalInfoId
        /// <Author>Raman</Author>
        /// <createdOn>17 August, 2009</createdOn>
        /// </summary>
        private int PermissionID
        {
            get
            {
                return _permissionID = Request.QueryString["PermissionID"] != null ? Convert.ToInt32(Request.QueryString["PermissionID"]) : 0;
            }
            set
            {
                _permissionID = value;
            }
        }

        #endregion
        
        #region Events

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                divDataListRequestHospitalUnitAccess.Visible = false;
                SetPageDefaultSetting();
                ImageButtonBack.Visible = false;
                if (!Page.IsPostBack)
                {
                    TextBoxFirstName.Attributes.Add("onKeyDown", "return validateNonNumber(event);");
                    TextBoxLastName.Attributes.Add("onKeyDown", "return validateNonNumber(event);");
                    ButtonDelete.OnClientClick = "return confirmDelete('Are you sure want to remove this User?', 'This action will permanently delete this user and all related links.  Are you sure you want to do this?');";
                    PopulateData();
                    BindDataListForUnitName();
                }
            }
            catch (Exception ex)
            {
                Response.Write(ex);
                //ex.Data.Add("Function", " protected void Page_Load(object sender, EventArgs e)");
                //LogManager._stringObject = "UserProfile.ascx.cs ---- ";
                //LogManager.SetExceptionDetails(ex, LogManager._stringObject, null);
                //LogManager.LogException(ex, LogManager.LoggingCategory.General, LogManager.LoggingLevel.Error);
                //CommonClass.Show(LogManager.ShowErrorDetail(ex));
            }
        }

        private void BindDataListForUnitName()
        {
            try
            {
                List<RMC.BusinessEntities.BEHospitalMembers> objHospitalMemberList = null;
                objTreeView = new RMC.BussinessService.BSTreeView();
                objHospitalMemberList = objTreeView.GetAllPermission_HospitalUnitName(UserId);
                if (objHospitalMemberList.Count > 0)
                {
                    //divDataListRequestHospitalUnitAccess.Visible = true;
                    DataListRequestHospitalUnitAccess.DataSource = objHospitalMemberList;
                    DataListRequestHospitalUnitAccess.DataBind();
                }
                else
                {
                    //divDataListRequestHospitalUnitAccess.Visible = false;
                }
            }
            catch (Exception ex)
            {
                ex.Data.Add("Function", "ButtonDelete_Click");
                LogManager._stringObject = "UserProfile.ascx.cs ---- ";
                LogManager.SetExceptionDetails(ex, LogManager._stringObject, null);
                LogManager.LogException(ex, LogManager.LoggingCategory.General, LogManager.LoggingLevel.Error);
                CommonClass.Show(LogManager.ShowErrorDetail(ex));
            }
        }

        /// <summary>
        /// To Reset page data
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <CreatedBy>Raman</CreatedBy>
        /// <CreatedOn>July 20, 2009</CreatedOn>
        protected void ButtonReset_Click(object sender, EventArgs e)
        {
            try
            {
                PopulateData();
            }
            catch (Exception ex)
            {
                ex.Data.Add("Function", " protected void ButtonReset_Click(object sender, EventArgs e)");
                LogManager._stringObject = "UserProfile.ascx.cs ---- ";
                LogManager.SetExceptionDetails(ex, LogManager._stringObject, null);
                LogManager.LogException(ex, LogManager.LoggingCategory.General, LogManager.LoggingLevel.Error);
                CommonClass.Show(LogManager.ShowErrorDetail(ex));
            }
        }

        /// <summary>
        /// To save user information
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <CreatedBy>Raman</CreatedBy>
        /// <CreatedOn>July 20, 2009</CreatedOn>
        protected void ButtonSave_Click(object sender, EventArgs e)
        {
            bool isSaved = false;
            try
            {
                if (Page.IsValid)
                {
                    _objectBSUsers = new RMC.BussinessService.BSUsers();
                    //CaptchaControlImage.ValidateCaptcha(TextBoxCaptchaText.Text);
                    //if (!CaptchaControlImage.UserValidated)
                    //{
                    //    DisplayMessage("Please Enter Correct Code", System.Drawing.Color.Red);
                    //    return;
                    //}
                    // else
                    //  {
                    _objectUserInfo = new RMC.DataService.UserInfo();
                    _objectBSUsers = new RMC.BussinessService.BSUsers();

                    if (CreateUser == false)
                    {
                        //_objectUserInfo = GetSelectedUserInformation(); ;// CommonClass.UserInformation;
                        _objectUserInfo.UserID = UserId;
                    }
                    else
                    {
                        if (_objectBSUsers.ExistUserEmailId(TextBoxEmail.Text.Trim()) == true)
                        {
                            CommonClass.Show("This Email-Id Already Exist!");
                            //DisplayMessage("This E-mail Id already exist!", System.Drawing.Color.Green);
                            TextBoxPassword.Attributes.Add("value", TextBoxPassword.Text);
                            TextBoxConfirmPassword.Attributes.Add("value", TextBoxConfirmPassword.Text);
                            return;
                        }
                        _objectUserInfo.CreatedBy = ActiveUser;
                        _objectUserInfo.CreatedDate = DateTime.Now;
                        _objectUserInfo.Email = TextBoxEmail.Text;
                        _objectUserInfo.UserTypeID = Convert.ToInt32(RMC.Web.UserRole.Admin);
                    }
                    if (IsSuperAdmin == true)
                    {
                        _objectUserInfo.IsActive = true;
                    }
                    _objectUserInfo.ModifiedBy = ActiveUser;
                    _objectUserInfo.ModifiedDate = DateTime.Now;
                    _objectUserInfo.CompanyName = TextBoxCompanyName.Text;
                    _objectUserInfo.FirstName = TextBoxFirstName.Text;
                    _objectUserInfo.LastName = TextBoxLastName.Text;
                    _objectUserInfo.Fax = TextBoxFax.Text;
                    _objectUserInfo.Phone = TextBoxPhone.Text;
                    _objectUserInfo.Password = TextBoxPassword.Text;
                    _objectUserInfo.SecurityQuestion = TextBoxSecurityQuestion.Text;
                    _objectUserInfo.SecurityAnswer = TextBoxSecurityAnswer.Text;
                    _objectBSUsers.UpdateUserInformation(_objectUserInfo);
                    if (CreateUser == true)
                    {
                        CommonClass.Show("Record Saved Successfully!");
                        //DisplayMessage("Record has been saved successfully!", System.Drawing.Color.Green);
                    }
                    else
                    {
                        if (UserId == LoggedInUserInfo.UserID)
                        {
                            _objectBSLogin = new RMC.BussinessService.BSLogin();
                            _objectBSLogin.UpdateUserInformationInSession(UserId);
                        }
                        CommonClass.Show("Record Updated Successfully!");
                        //DisplayMessage("Record has been updated successfully!", System.Drawing.Color.Green);
                    }
                    isSaved = true;
                    // }
                }
            }
            catch (Exception ex)
            {
                ex.Data.Add("Function", " protected void ButtonSave_Click(object sender, EventArgs e)");
                ex.Data.Add("Class", "UserProfile.ascx.cs");
                LogManager._stringObject = "UserProfile.ascx.cs ---- ";
                LogManager.SetExceptionDetails(ex, LogManager._stringObject, null);
                LogManager.LogException(ex, LogManager.LoggingCategory.General, LogManager.LoggingLevel.Error);
                CommonClass.Show(LogManager.ShowErrorDetail(ex));
            }
            finally
            {
                TextBoxPassword.Attributes.Add("value", TextBoxPassword.Text);
                TextBoxConfirmPassword.Attributes.Add("value", TextBoxConfirmPassword.Text);
                _objectBSUsers = null;
                _objectUserInfo = null;
            }
            if (isSaved == true && HttpContext.Current.User.IsInRole("superadmin"))
            {
                Response.Redirect("UserList.aspx", false);
            }
        }

        protected void CheckBoxApproval_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                bool flagUpdate = false;
                bool flagDelete = false;
                _objectBSUsers = new RMC.BussinessService.BSUsers();
                CheckBox chkBox = (CheckBox)(sender);
                DataListItem dataLstItem = (DataListItem)(chkBox.NamingContainer);
                int userInfoID = Convert.ToInt32(DataListRequestHospitalUnitAccess.DataKeys[dataLstItem.ItemIndex].ToString());
                if (userInfoID != UserId)
                {
                    flagUpdate = _objectBSUsers.UpdateRequestHospitalUnitByUserId(userInfoID);
                    if (flagUpdate == true)
                    {
                        CommonClass.Show("Unit List Has Gone To Administrator.");
                    }
                }
                else
                {
                    flagDelete = _objectBSUsers.DeleteRequestHospitalUnitByUserId(userInfoID);
                    if (flagDelete == true)
                    {
                        CommonClass.Show("Delete This Record From Your Unit List.");
                    }
                }
                BindDataListForUnitName();
            }
            catch (Exception ex)
            {
                ex.Data.Add("Function", "ButtonDelete_Click");
                LogManager._stringObject = "UserProfile.ascx.cs ---- ";
                LogManager.SetExceptionDetails(ex, LogManager._stringObject, null);
                LogManager.LogException(ex, LogManager.LoggingCategory.General, LogManager.LoggingLevel.Error);
                CommonClass.Show(LogManager.ShowErrorDetail(ex));
            }
        }

        protected void ImageButtonBack_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                if (HttpContext.Current.User.IsInRole("superadmin"))
                {
                    //ImageButtonBack.PostBackUrl = "~/Administrator/HospitalDetail.aspx?HospitalInfoId=" + HospitalInfoId + "&PermissionID=" + PermissionID;
                    Response.Redirect("~/Administrator/HospitalDetail.aspx?HospitalInfoId=" + HospitalInfoId + "&PermissionID=" + PermissionID, false);
                }
                else
                {
                    //ImageButtonBack.PostBackUrl = "~/Users/HospitalDetail.aspx?HospitalInfoId=" + HospitalInfoId + "&PermissionID=" + PermissionID;
                    Response.Redirect("~/Users/HospitalDetail.aspx?HospitalInfoId=" + HospitalInfoId + "&PermissionID=" + PermissionID, false);
                }
            }
            catch (Exception ex)
            {
                LogManager._stringObject = "UserProfile.ascx.cs ---- ";
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
        protected void ButtonDelete_Click(object sender, EventArgs e)
        {
            bool flag = false;
            try
            {
                _objectBSUsers = new BSUsers();
                flag = _objectBSUsers.DeleteLogicallyUserByUserID(UserId, GetSelectedUserInformation().Email);
                if (flag)
                {
                    if (HttpContext.Current.User.IsInRole("superadmin"))
                    {
                        //ImageButtonBack.PostBackUrl = "~/Administrator/HospitalDetail.aspx?HospitalInfoId=" + HospitalInfoId + "&PermissionID=" + PermissionID;
                        Response.Redirect("~/Administrator/DataManagement.aspx", false);
                    }
                    else
                    {
                        //ImageButtonBack.PostBackUrl = "~/Users/HospitalDetail.aspx?HospitalInfoId=" + HospitalInfoId + "&PermissionID=" + PermissionID;
                        Response.Redirect("~/Users/DataManagement.aspx", false);
                    }
                }
                else
                {
                    CommonClass.Show("Failed to Delete Record.");
                    //DisplayMessage("Fail to Delete Record.", System.Drawing.Color.Red);
                }
            }
            catch (Exception ex)
            {
                ex.Data.Add("Function", "ButtonDelete_Click");
                LogManager._stringObject = "UserProfile.ascx.cs ---- ";
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
        protected void ButtonRequestOwnerPriviledges_Click(object sender, EventArgs e)
        {
            bool flag = false;
            string message = string.Empty;
            try
            {
                _objectBSUsers = new BSUsers();

                flag = _objectBSUsers.UpdateRequestActivation("Activation Request", UserId, out message);

                if (flag)
                {
                    CommonClass.Show(message);
                    //DisplayMessage(message, System.Drawing.Color.Green);
                }
                else
                {
                    CommonClass.Show(message);
                    //DisplayMessage(message, System.Drawing.Color.Red);
                }
            }
            catch (Exception ex)
            {
                ex.Data.Add("Function", "ButtonRequestOwnerPriviledges_Click");
                LogManager._stringObject = "UserProfile.ascx.cs ---- ";
                LogManager.SetExceptionDetails(ex, LogManager._stringObject, null);
                LogManager.LogException(ex, LogManager.LoggingCategory.General, LogManager.LoggingLevel.Error);
                CommonClass.Show(LogManager.ShowErrorDetail(ex));
            }
        }

        #endregion

        #region Functions

        /// <summary>
        /// To populate user information on page controls
        /// </summary>
        /// <CreatedBy>Raman</CreatedBy>
        /// <CreatedOn>July 20, 2009</CreatedOn>
        private void PopulateData()
        {
            try
            {
                #region Commented Code
                //if(CreateUser==false)
                //{
                //    if (UserId != 0 && UserType.ToLower() == Convert.ToString(RMC.Web.UserRole.SuperAdmin).ToLower())
                //    {
                //        _objectUserInfo = _objectBSUsers.GetUserInformation(UserId);
                //    }
                //}
                #endregion
                if (CreateUser == true)//Email can't be modified
                {
                    TextBoxEmail.Enabled = true;
                    ButtonReset.Enabled = false;
                }
                else
                {
                    TextBoxEmail.Enabled = false;
                    #region commented code
                    /*if (argUserId != 0 && UserType.ToLower() == Convert.ToString(RMC.Web.UserRole.SuperAdmin).ToLower())
                        {
                            _objectBSUsers = new RMC.BussinessService.BSUsers();
                            SetUserinformation = _objectBSUsers.GetUserInformation(UserId);
                        }
                        else
                        {
                            SetUserinformation = (List<RMC.DataService.UserInfo>)Session["UserInformation"];
                        }
                        if (SelectedUserInformation != null)
                        {
                            PopulateData();
                        }*/
                    #endregion
                    _objectUserInfo = GetSelectedUserInformation();
                    TextBoxCompanyName.Text = _objectUserInfo.CompanyName;
                    TextBoxFirstName.Text = _objectUserInfo.FirstName;
                    TextBoxLastName.Text = _objectUserInfo.LastName;
                    TextBoxPhone.Text = _objectUserInfo.Phone;
                    TextBoxFax.Text = _objectUserInfo.Fax;
                    TextBoxEmail.Text = _objectUserInfo.Email;
                    TextBoxSecurityQuestion.Text = _objectUserInfo.SecurityQuestion;
                    TextBoxSecurityAnswer.Text = _objectUserInfo.SecurityAnswer;
                    TextBoxPassword.Text = _objectUserInfo.Password;
                    TextBoxConfirmPassword.Text = _objectUserInfo.Password;
                    TextBoxPassword.Attributes.Add("value", _objectUserInfo.Password);
                    TextBoxConfirmPassword.Attributes.Add("value", _objectUserInfo.Password);
                    if (_objectUserInfo.IsActive)
                    {
                        trRequestOwnerPriviledges.Visible = false;
                        ButtonDelete.Enabled = true;
                        ButtonDelete.CssClass = "aspButton";
                    }
                    else
                    {
                        trRequestOwnerPriviledges.Visible = true;
                        ButtonDelete.Enabled = false;
                        ButtonDelete.CssClass = "aspButtonDisable";
                    }
                }
            }
            catch (Exception ex)
            {
                ex.Data.Add("Function", "PopulateData");
                ex.Data.Add("Class", "UserProfile.ascx.cs");
                throw ex;
            }
            finally
            {
                _objectUserInfo = null;
            }
        }

        /// <summary>
        /// Use to Display message.
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="color"></param>
        /// <CreatedBy>Raman</CreatedBy>
        /// <CreatedOn>July 20, 2009</CreatedOn>
        private void DisplayMessage(string msg, System.Drawing.Color color)
        {
            try
            {
                LabelErrorMsg.Text = msg;
                LabelErrorMsg.ForeColor = color;
                LabelErrorMsg.Visible = true;
                PanelErrorMsg.Visible = true;
            }
            catch (Exception ex)
            {
                ex.Data.Add("Function", "DisplayMessage");
                ex.Data.Add("Class", "Userprofile.ascx.cs");
                throw ex;
            }
        }

        /// <summary>
        /// Set Page default setting
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="color"></param>
        /// <CreatedBy>Raman</CreatedBy>
        /// <CreatedOn>July 20, 2009</CreatedOn>
        private void SetPageDefaultSetting()
        {
            try
            {
                LabelErrorMsg.Text = "";
                PanelErrorMsg.Visible = false;
                LabelErrorMsg.Visible = false;
            }
            catch (Exception ex)
            {
                ex.Data.Add("Function", " private void SetPageDefaultSetting()");
                ex.Data.Add("Class", " UserProfile.ascx.cs");
                throw ex;
            }
        }

        /// <summary>
        /// Return User information
        /// <Author>Raman</Author>
        /// <CreatedOn>July 22, 2009</CreatedOn>
        /// </summary>
        /// <returns></returns>
        private RMC.DataService.UserInfo GetSelectedUserInformation()
        {
            RMC.DataService.UserInfo objectUserInfo = null;
            try
            {
                if (CreateUser == false)//page has been opened for existing user
                {
                    //if logged in user is super admin and he is opening some other user profile
                    if (UserId != LoggedInUserInfo.UserID && UserType.ToLower() == Convert.ToString(RMC.Web.UserRole.SuperAdmin).ToLower())
                    {
                        _objectBSUsers = new RMC.BussinessService.BSUsers();
                        objectUserInfo = _objectBSUsers.GetUserInformation(UserId);
                    }
                    else//if logged in user is viewing his profile
                    {
                        objectUserInfo = LoggedInUserInfo;
                    }
                }
                return objectUserInfo;
            }
            catch (Exception ex)
            {
                ex.Data.Add("Function", " private void SetPageDefaultSetting()");
                ex.Data.Add("Class", "UserProfile.ascx.cs");
                throw ex;
            }
            finally
            {
                objectUserInfo = null;
                _objectBSUsers = null;
            }
        }

        #endregion        

    }
}