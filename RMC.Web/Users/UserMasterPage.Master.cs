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
using System.IO;
using RMC.BussinessService;

namespace RMC.Web.Users
{
    public partial class UserMasterPage : System.Web.UI.MasterPage
    {

        #region "Variables"
        int _userId = 0;
        //Bussiness Service Objects.
        RMC.BussinessService.BSUsers _objectBSUsers = null;
        //Data Service Objects
        RMC.DataService.UserInfo _objectUserInfo = null;
        #endregion

        #region Events

        /// <summary>
        /// Logout Administrator.
        /// Created By : Davinder Kumar
        /// Creation Date : July 06, 2009.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void LinkButtonLogout_Click(object sender, EventArgs e)
        {
            try
            {
                FormsAuthentication.SignOut();
                Session.Abandon();
                Response.Redirect("~/Login.aspx", false);
            }
            catch (Exception ex)
            {
                ex.Data.Add("Events", "LinkButtonLogout_Click");
                ex.Data.Add("Page", "Administrator/AdminMaster.Master");
                LogManager._stringObject = "AdminMaster.Master ---- LinkButtonLogout_Click";
                LogManager.SetExceptionDetails(ex, LogManager._stringObject, null);
                LogManager.LogException(ex, LogManager.LoggingCategory.General, LogManager.LoggingLevel.Error);
            }
        }

        /// <summary>
        /// Page Load Method use to delete Cache.
        /// Created By : Davinder Kumar.
        /// Creation Date : July 06, 2009.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!Page.IsPostBack)
                {
                    RMC.BussinessService.BSHospitalInfo objectBSHospitalInfo = new RMC.BussinessService.BSHospitalInfo();
                    //to delete cache
                    Response.AddHeader("Pragma", "no-cache");
                    Response.AddHeader("Cache-Control", "no-cache");
                    Response.CacheControl = "no-cache";
                    Response.Expires = -1;
                    Response.ExpiresAbsolute = new DateTime(1900, 1, 1);
                    Response.Cache.SetCacheability(HttpCacheability.NoCache);

                    if ((UserInformation.IsActive && UserInformation.AccessRequest == "ReadOnly") || (UserInformation.IsActive && UserInformation.AccessRequest == null))
                    {
                        LinkButtonDataManagement.Enabled = false;
                        //LinkButtonAddDemographicDetail.Enabled = false;
                        LinkButtonViewCategoryProfiles.Enabled = false;
                        //LinkButtonExcelUploader.Enabled = false;\
                        HyperLinkHospitalBenchmark.Enabled = false;
                        HyperLinkMonthlySummaryDashBoard.Enabled = false;
                        HyperLinkMonthlyDataPieCharts.Enabled = false;
                        HyperLinkControlCharts.Enabled = false;
                        // added by raman
                        // this is added for location profile functionality
                        HyperLink1.Enabled = false;
                        LinkButtonViewCategoryProfiles.Enabled = false;
                        LinkButtonBenchmarkingFilter.Enabled = false;
                    }

                    if ((objectBSHospitalInfo.CheckForHospitalExistence(UserInformation.UserID) && (UserInformation.AccessRequest == "Owner")) || (objectBSHospitalInfo.CheckForHospitalExistence(UserInformation.UserID) && (UserInformation.AccessRequest == "ReadOnly")) || (objectBSHospitalInfo.CheckForHospitalExistence(UserInformation.UserID) && (UserInformation.AccessRequest == null)))
                    {
                        LinkButtonDataManagement.Enabled = true;
                        LinkButtonViewCategoryProfiles.Enabled = true;
                        //LinkButtonExcelUploader.Enabled = true;
                        HyperLinkHospitalBenchmark.Enabled = true;
                        HyperLinkMonthlySummaryDashBoard.Enabled = true;
                        HyperLinkMonthlyDataPieCharts.Enabled = true;
                        HyperLinkControlCharts.Enabled = true;
                        HyperLink1.Enabled = true;
                        LinkButtonViewCategoryProfiles.Enabled = true;
                        LinkButtonBenchmarkingFilter.Enabled = true;
                    }

                    _objectUserInfo = GetSelectedUserInformation();
                    if (_objectUserInfo.IsActive)
                    {
                        //ButtonRequestOwnerPriviledges.Visible = false;
                        // liRequestOwnerPriviledges.Visible = false;
                    }
                    else
                    {
                        // ButtonRequestOwnerPriviledges.Visible = true;
                        // liRequestOwnerPriviledges.Visible = true;
                    }
                }
            }
            catch (Exception ex)
            {
                ex.Data.Add("Events", "Page_Load");
                ex.Data.Add("Page", "Administrator/AdminMaster.Master");
                LogManager._stringObject = "AdminMaster.Master ---- Page_Load";
                LogManager.SetExceptionDetails(ex, LogManager._stringObject, null);
                LogManager.LogException(ex, LogManager.LoggingCategory.General, LogManager.LoggingLevel.Error);
            }
        }
        // Commented by Raman
        // This code is commmented as we are required to set ownership priviledges during user set up page
        // dated - 27/Dec/2010

        //protected void ButtonRequestOwnerPriviledges_Click(object sender, EventArgs e)
        //{
        //    bool flag = false;
        //    string message = string.Empty;
        //    try
        //    {
        //        _objectBSUsers = new RMC.BussinessService.BSUsers();

        //        flag = _objectBSUsers.UpdateRequestActivation("Activation Request", UserId, out message);

        //        if (flag)
        //        {
        //            CommonClass.Show(message);
        //            //DisplayMessage(message, System.Drawing.Color.Green);
        //        }
        //        else
        //        {
        //            CommonClass.Show(message);
        //            //DisplayMessage(message, System.Drawing.Color.Red);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        ex.Data.Add("Function", "ButtonRequestOwnerPriviledges_Click");
        //        LogManager._stringObject = "UserProfile.ascx.cs ---- ";
        //        LogManager.SetExceptionDetails(ex, LogManager._stringObject, null);
        //        LogManager.LogException(ex, LogManager.LoggingCategory.General, LogManager.LoggingLevel.Error);
        //        //DisplayMessage(LogManager.ShowErrorDetail(ex), System.Drawing.Color.Red);
        //    }
        //}

        #endregion

        #region Properties

        public RMC.DataService.UserInfo UserInformation
        {
            get
            {
                //return CommonClass.UserInformation;
                return CommonClass.UserInformation;
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
        #endregion

    }
}
