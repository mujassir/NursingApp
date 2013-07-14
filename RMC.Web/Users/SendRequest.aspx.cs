using System;
using System.Collections;
using System.Collections.Generic;
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
using RMC.BussinessService;

namespace RMC.Web.Users
{
    public partial class SendRequest : System.Web.UI.Page
    {

        #region Variables

        //Bussiness Service Objeect.
        RMC.BussinessService.BSUsers _objectBSUsers = null;
        RMC.BussinessService.BSViewRequest _objectBSViewRequest = null;

        //Fundamental Data Types.
        bool _flag;

        #endregion

        #region Events

        /// <summary>
        /// Reset Controls.
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
                ex.Data.Add("Page", "SendRequest.aspx");
                LogManager._stringObject = "SendRequest.aspx ---- ButtonReset_Click";
                LogManager.SetExceptionDetails(ex, LogManager._stringObject, null);
                LogManager.LogException(ex, LogManager.LoggingCategory.General, LogManager.LoggingLevel.Error);
                CommonClass.Show(LogManager.ShowErrorDetail(ex));
            }
        }

        /// <summary>
        /// Save Request.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ButtonSendRequest_Click(object sender, EventArgs e)
        {
            // Edited by Raman
            // This code is edited 
            try
            {
                if (Page.IsValid)
                {
                    _objectBSViewRequest = new RMC.BussinessService.BSViewRequest();

                    _flag = _objectBSViewRequest.InsertViewRequest(SaveViewRequest());
                    if (_flag)
                    {

                        CommonClass.Show("Your request for Unit Access has been received by the system administrator for review.  You should receive approval for access to this unit in the next 24 hours.  If you don’t receive notification by email within the next 24 hours, then please contact the system administrator at (513) 624-6629 or by email at nlee@rapidmodeling.com");
                        //DisplayMessage("Request Send Successfully.", System.Drawing.Color.Green);
                        bool _emailFlag;
                        string _bodyText, _fromAddress, _toAddress, _subjectText;
                        int userID = CommonClass.UserInformation.UserID;
                        //int hospitalID= Convert.ToInt32(DropDownListHospital.SelectedValue);
                        for (int counter = 0; counter <= ListBoxUnit.Items.Count - 1; counter++)
                        {
                            if (ListBoxUnit.Items[counter].Selected == true)
                            {
                                try
                                {
                                    DataService.UserInfo _objectUserInfo = new DataService.UserInfo();
                                    DataService.HospitalDemographicInfo _objectHospitaDemoInfo = new DataService.HospitalDemographicInfo();
                                    RMC.BussinessService.BSCommon _objectBSCommon = new RMC.BussinessService.BSCommon();
                                    RMC.BussinessService.BSUsers _objectBSUser = new RMC.BussinessService.BSUsers();
                                    BSEmailNotificationBody _objectBSEmailNotificationBody = new BSEmailNotificationBody();
                                    BSHospitalDemographicDetail objHosDemo = new BSHospitalDemographicDetail();
                                    RMC.DataService.RMCDataContext _objectRMCDataContext = new RMC.DataService.RMCDataContext();

                                    _objectUserInfo = (from ui in _objectRMCDataContext.UserInfos
                                                       where ui.UserID == userID && (ui.IsDeleted ?? false) == false
                                                       select ui).FirstOrDefault();

                                    _objectHospitaDemoInfo = objHosDemo.GetHospitalDemographicDetail(Convert.ToInt32(ListBoxUnit.Items[counter].Value));

                                    string email = string.Empty;
                                    RMC.BussinessService.BSEmail _objectBSEmail;
                                    _toAddress = ConfigurationManager.AppSettings["superAdminAddress"].ToString();
                                    _bodyText = Convert.ToString(_objectBSEmailNotificationBody.GetEmailBodyOfRequestHospitalUnitAccess(_objectUserInfo, _objectHospitaDemoInfo));
                                    _subjectText = "Time Study RN National Benchmarking Database Hospital Unit Access Approval Status.";
                                    _fromAddress = ConfigurationManager.AppSettings["fromAddress"].ToString();
                                    _objectBSEmail = new RMC.BussinessService.BSEmail(_fromAddress, _toAddress, _subjectText, _bodyText, true);
                                    _objectBSEmail.SendMail(true, out _emailFlag);
                                }
                                catch (Exception ex)
                                {
                                    LogManager._stringObject = "SendMessage.ascx.cs ---- ";
                                    LogManager.SetExceptionDetails(ex, LogManager._stringObject, null);
                                    LogManager.LogException(ex, LogManager.LoggingCategory.General, LogManager.LoggingLevel.Error);
                                    CommonClass.Show(LogManager.ShowErrorDetail(ex));
                                }
                            }
                        }
                        ResetControls();

                    }
                    else
                    {
                        CommonClass.Show("Failed to Send Request or Request Already Exists.");
                        //DisplayMessage("Fail to Send Request.", System.Drawing.Color.Red);
                    }
                }
            }
            catch (Exception ex)
            {
                ex.Data.Add("Events", "ButtonSendRequest_Click");
                ex.Data.Add("Page", "SendRequest.aspx");
                LogManager._stringObject = "SendRequest.aspx ---- ButtonSendRequest_Click";
                LogManager.SetExceptionDetails(ex, LogManager._stringObject, null);
                LogManager.LogException(ex, LogManager.LoggingCategory.General, LogManager.LoggingLevel.Error);
                CommonClass.Show(LogManager.ShowErrorDetail(ex));
            }
        }

        /// <summary>
        /// Clear ListBox.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void DropDownListHospital_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (ListBoxUnit.Items.Count > 0)
                {
                    ListBoxUnit.Items.Clear();
                }
            }
            catch (Exception ex)
            {
                ex.Data.Add("Events", "DropDownListHospital_SelectedIndexChanged");
                ex.Data.Add("Page", "SendRequest.aspx");
                LogManager._stringObject = "SendRequest.aspx ---- DropDownListHospital_SelectedIndexChanged";
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
        protected void ObjectDataSourceHospital_Selecting(object sender, ObjectDataSourceSelectingEventArgs e)
        {
            try
            {
                //int userID = CommonClass.UserInformation.UserID;
                int userID = CommonClass.UserInformation.UserID;
                e.InputParameters[0] = userID;
            }
            catch (Exception ex)
            {
                ex.Data.Add("Events", "ObjectDataSourceHospital_Selecting");
                ex.Data.Add("Page", "SendRequest.aspx");
                LogManager._stringObject = "SendRequest.aspx ---- ObjectDataSourceHospital_Selecting";
                LogManager.SetExceptionDetails(ex, LogManager._stringObject, null);
                LogManager.LogException(ex, LogManager.LoggingCategory.General, LogManager.LoggingLevel.Error);
                CommonClass.Show(LogManager.ShowErrorDetail(ex));
            }
        }

        protected void ObjectDataSourceUnit_Selecting(object sender, ObjectDataSourceSelectingEventArgs e)
        {
            try
            {
                //int userID = CommonClass.UserInformation.UserID;
                int userID = CommonClass.UserInformation.UserID;
                e.InputParameters[0] = Convert.ToInt32(DropDownListHospital.SelectedValue);
                e.InputParameters[1] = userID;
            }
            catch (Exception ex)
            {
                ex.Data.Add("Events", "ObjectDataSourceHospital_Selecting");
                ex.Data.Add("Page", "SendRequest.aspx");
                LogManager._stringObject = "SendRequest.aspx ---- ObjectDataSourceHospital_Selecting";
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
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                DropDownListHospital.Focus();
                if (!Page.IsPostBack)
                {
                    if (!HttpContext.Current.User.IsInRole("superadmin"))
                    {
                        ImageButtonBack.PostBackUrl = "~/Users/UserProfile.aspx";
                    }
                }
            }
            catch (Exception ex)
            {
                Response.Write(ex);
                //ex.Data.Add("Events", "Page_Load");
                //ex.Data.Add("Page", "SendRequest.aspx");
                //LogManager._stringObject = "SendRequest.aspx ---- SendRequest";
                //LogManager.SetExceptionDetails(ex, LogManager._stringObject, null);
                //LogManager.LogException(ex, LogManager.LoggingCategory.General, LogManager.LoggingLevel.Error);
                //CommonClass.Show(LogManager.ShowErrorDetail(ex));
            }
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Use to Display message of Login Failure.
        /// Created By : Davinder Kumar.
        /// Creation Date : June 25, 2009
        /// Modified By : Davinder Kumar.
        /// Modified Date : July 21, 2009.
        /// </summary>
        /// <param name="msg">Message</param>
        /// <param name="color">Color</param>
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
                ex.Data.Add("Class", "SendRequest");
                throw ex;
            }
        }

        /// <summary>
        /// Save View Request.
        /// Created By : Davinder Kumar.
        /// Creation Date : July 29, 2009.
        /// Modified By :Mahesh Sachdeava 
        /// Modified Date:June 24,2010.
        /// </summary>
        /// <returns></returns>
        private List<RMC.DataService.ViewRequest> SaveViewRequest()
        {
            List<RMC.DataService.ViewRequest> _objectSendRequestList = new List<RMC.DataService.ViewRequest>();
            RMC.DataService.ViewRequest objectViewRequest = null;
            _objectBSUsers = new RMC.BussinessService.BSUsers();
            int userID;
            try
            {
                if (ListBoxUnit.SelectedIndex > -1)
                {
                    userID = CommonClass.UserInformation.UserID;
                    for (int index = 0; index < ListBoxUnit.Items.Count; index++)
                    {
                        if (ListBoxUnit.Items[index].Selected)
                        {
                            //objectViewRequest = new RMC.DataService.ViewRequest();
                            //objectViewRequest.FromUserID = userID;

                            //objectViewRequest.HospitalDemographicDetailID = Convert.ToInt32(ListBoxUnit.Items[index].Value);

                            //objectViewRequest.HospitalID = Convert.ToInt32(DropDownListHospital.SelectedValue);
                            //objectViewRequest.IsApproved = false;
                            // Added by Mahesh Sachdeva to send request to all owners of unit 

                            List<int?> toUser = _objectBSUsers.GetUserIDByHospitalUnitID(Convert.ToInt32(ListBoxUnit.Items[index].Value));
                            for (int i = 0; i < toUser.Count; i++)
                            {
                                objectViewRequest = new RMC.DataService.ViewRequest();
                                objectViewRequest.FromUserID = userID;

                                objectViewRequest.HospitalDemographicDetailID = Convert.ToInt32(ListBoxUnit.Items[index].Value);

                                objectViewRequest.HospitalID = Convert.ToInt32(DropDownListHospital.SelectedValue);
                                objectViewRequest.IsApproved = false;
                                objectViewRequest.ToUserID = Convert.ToInt32(toUser[i]);
                                if (_objectBSViewRequest.RequestApprovalExist(objectViewRequest))
                                {
                                    continue;
                                }
                                else
                                {
                                    _objectSendRequestList.Add(objectViewRequest);
                                }
                                objectViewRequest = null;
                            }

                            // Commented by Mahesh Sachdeva
                            // objectViewRequest.ToUserID = _objectBSUsers.GetUserIDByHospitalUnitID(Convert.ToInt32(ListBoxUnit.Items[index].Value));
                            //if (_objectBSViewRequest.RequestApprovalExist(objectViewRequest))
                            //{
                            //    continue;
                            //}
                            //else
                            //{
                            //    _objectSendRequestList.Add(objectViewRequest);
                            //}
                        }
                    }
                }
                return _objectSendRequestList;
            }
            catch (Exception ex)
            {
                ex.Data.Add("Function", "DisplayMessage");
                ex.Data.Add("Class", "SendRequest");
                throw ex;
            }
        }

        /// <summary>
        /// Reset Control.
        /// </summary>
        private void ResetControls()
        {
            try
            {
                DropDownListHospital.SelectedIndex = 0;
                ListBoxUnit.Items.Clear();
            }
            catch (Exception ex)
            {
                ex.Data.Add("Function", "ResetControls");
                ex.Data.Add("Class", "SendRequest");
                throw ex;
            }
        }

        #endregion

    }
    //End Of SendRequest Class
}
//End Of NameSpace
