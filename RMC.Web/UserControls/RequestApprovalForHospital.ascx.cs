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

namespace RMC.Web.UserControls
{
    public partial class RequestApprovalForHospital : System.Web.UI.UserControl
    {


        #region Variables

        bool _emailFlag;
        public string _bodyText, _fromAddress, _toAddress, _subjectText;

        //Bussiness Object
        RMC.BussinessService.BSHospitalInfo _objectBSHospitalInfo = null;
        RMC.BussinessService.BSHospitalDemographicDetail _objectBSHospitalDemographicDetail = null;
        RMC.BussinessService.BSMultiUserHospital _objectBSMultiUserHospital = null;
        RMC.BussinessService.BSMultiUserDemographic _objectBSMultiUserDemographic = null;

        //Data Service Objects.
        RMC.DataService.MultiUserHospital _objectMultiUserHospital = null;
        RMC.DataService.MultiUserDemographic _objectMultiUserDemographic = null;

        //Generic List Of Data Service objects.
        List<RMC.DataService.MultiUserDemographic> _objectGenericMultiUserDemographic = null;

        // Bussiness Entities objects
        List<RMC.BusinessEntities.BEUserInfomation> _objectGenericBEUserInfomation = null;
        #endregion

        #region Properties
        /// <summary>
        /// Return passed HospitalInfoId
        /// <Author>Raman</Author>
        /// <createdOn>Aug 5, 2009</createdOn>
        /// </summary>
        private int HospitalDemographicId
        {
            get
            {
                return Request.QueryString["HospitalDemographicId"] != null ? Convert.ToInt32(Request.QueryString["HospitalDemographicId"].ToString()) : 0;
            }
        }
        /// <summary>
        /// Return passed HospitalInfoId
        /// <Author>Raman</Author>
        /// <createdOn>Aug 5, 2009</createdOn>
        /// </summary>
        private int HospitalInfoID
        {
            get
            {
                return Request.QueryString["HospitalID"] != null ? Convert.ToInt32(Request.QueryString["HospitalID"].ToString()) : 0;
            }
        }
        /// <summary>
        /// Return passed HospitalInfoId
        /// <Author>Raman</Author>
        /// <createdOn>Aug 5, 2009</createdOn>
        /// </summary>
        public ucDemographicMembersTreeView objectDemographicMembersTreeView = null;
        public ucDemographicMembersTreeView DemographicMemberTreeView
        {

            get
            {
                return objectDemographicMembersTreeView;
            }
            set
            {
                objectDemographicMembersTreeView = value;
            }
        }
        #endregion

        #region Events

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ButtonApproved_Click(object sender, EventArgs e)
        {
            
            try
            {
                List<RMC.DataService.MultiUserHospital> objectGenericMultiUserHospital = new List<RMC.DataService.MultiUserHospital>();
                _objectBSMultiUserHospital = new RMC.BussinessService.BSMultiUserHospital();

                foreach (GridViewRow grdRow in GridViewRequestApproval.Rows)
                {
                    CheckBox chkBox = (CheckBox)grdRow.FindControl("CheckBoxSelection");

                    if (chkBox.Checked)
                    {
                        DropDownList DropDownListPermission = (DropDownList)grdRow.FindControl("DropDownListPermission");
                        _objectMultiUserDemographic = new RMC.DataService.MultiUserDemographic();
                        int userID = Convert.ToInt32(GridViewRequestApproval.DataKeys[grdRow.RowIndex].Value);
                        ViewState["vwuserID"] = Convert.ToString(userID);
                        _objectMultiUserHospital = new RMC.DataService.MultiUserHospital();
                        _objectMultiUserHospital.CreatedBy = CommonClass.UserInformation.FirstName + " " + CommonClass.UserInformation.LastName;
                        _objectMultiUserHospital.CreatedDate = DateTime.Now;
                        _objectMultiUserHospital.HospitalInfoID = HospitalInfoID;
                        _objectMultiUserHospital.IsDeleted = false;
                        _objectMultiUserHospital.PermissionID = Convert.ToInt32(DropDownListPermission.SelectedValue);
                        _objectMultiUserHospital.UserID = userID;
                        objectGenericMultiUserHospital.Add(_objectMultiUserHospital);

                        if (DropDownListPermission.SelectedItem.Text == "Owner")
                        {
                            _bodyText = "Your request for hospital unit Access record and reporting access in the Time Study RN National Benchmarking Database has been approved. You can now login to the database using your email address and password. <br><br>You can now edit the Hospital Units Details.<br><br>If you have any questions, then please do not hesitate to contact the system administrator at (513) 624-6629 or by email at nlee@rapidmodeling.com.<br><br>Thanks for participating in the Time Study RN National Benchmarking Database.<br><br>Sincerely,<br>System Administrator<br>Time Study RN National Benchmarking Database Project";
                        }
                        if (DropDownListPermission.SelectedItem.Text == "ReadOnly")
                        {
                            _bodyText = "Your request for hospital unit Access record and reporting access in the Time Study RN National Benchmarking Database has been approved. You can now login to the database using your email address and password.<br><br> If you have any questions, then please do not hesitate to contact the system administrator at (513) 624-6629 or by email at nlee@rapidmodeling.com.<br><br>Thanks for participating in the Time Study RN National Benchmarking Database.<br><br>Sincerely,<br>System Administrator<br>Time Study RN National Benchmarking Database Project";
                        }
                        else
                        {
                            _bodyText = "Your request for hospital unit Access record and reporting access in the Time Study RN National Benchmarking Database has been approved. You can now login to the database using your email address and password. <br><br>You can now Upload Data for respective Hospital Units.<br><br>If you have any questions, then please do not hesitate to contact the system administrator at (513) 624-6629 or by email at nlee@rapidmodeling.com.<br><br>Thanks for participating in the Time Study RN National Benchmarking Database.<br><br>Sincerely,<br>System Administrator<br>Time Study RN National Benchmarking Database Project";
                        }


                    }
                }

                if (objectGenericMultiUserHospital.Count > 0)
                {
                    _objectBSMultiUserHospital.InsertMultiUserHospitalForViewOnly(objectGenericMultiUserHospital);
                    // Summary
                    // Added By Raman
                    // Added for Email Functionality, 03/Jan/2011



                    RMC.BussinessService.BSCommon _objectBSCommon = new RMC.BussinessService.BSCommon();
                    RMC.BussinessService.BSUsers _objectBSUser = new RMC.BussinessService.BSUsers();

                    try
                    {
                        string email = string.Empty;
                        RMC.BussinessService.BSEmail _objectBSEmail;
                        _toAddress = _objectBSUser.GetUserEmailByUserID(Convert.ToInt32(ViewState["vwuserID"]));

                        _subjectText = "Time Study RN National Benchmarking Database Approval Status";
                        _fromAddress = ConfigurationManager.AppSettings["superAdminAddress"].ToString();
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
                else
                {
                    CommonClass.Show("Must select at least one user.");
                }
                GetApprovedUserList();
                GridViewRequestApproval.DataBind();
                if (DemographicMemberTreeView != null)
                {
                    DemographicMemberTreeView.BuildDemographicMembersTree();
                }
            }
            catch (Exception ex)
            {
                ex.Data.Add("Events", "ButtonApproved_Click");
                ex.Data.Add("Page", "RequestApproval.ascx");
                LogManager._stringObject = "RequestApproval.ascx ---- ButtonApproved_Click";
                LogManager.SetExceptionDetails(ex, LogManager._stringObject, null);
                LogManager.LogException(ex, LogManager.LoggingCategory.General, LogManager.LoggingLevel.Error);
                CommonClass.Show(LogManager.ShowErrorDetail(ex));
            }
        }

        /// <summary>
        /// Page Load.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!Page.IsPostBack)
                {
                    CommonClass objectCommonClass = new CommonClass();
                    objectCommonClass.BackButtonUrl = Request.UrlReferrer.AbsoluteUri;
                    _objectBSHospitalInfo = new RMC.BussinessService.BSHospitalInfo();
                    LiteralHospitalName.Text = " (" + _objectBSHospitalInfo.GetHospitalNameByHospitalID(HospitalInfoID) + ") ";
                }
                GetApprovedUserList();
            }
            catch (Exception ex)
            {
                ex.Data.Add("Events", "Page_Load");
                ex.Data.Add("Page", "RequestApproval.ascx");
                LogManager._stringObject = "RequestApproval.ascx ---- Page_Load";
                LogManager.SetExceptionDetails(ex, LogManager._stringObject, null);
                LogManager.LogException(ex, LogManager.LoggingCategory.General, LogManager.LoggingLevel.Error);
                CommonClass.Show(LogManager.ShowErrorDetail(ex));
            }
        }

        /// <summary>
        /// Redirects to previous page
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <CreatedBy>Raman</CreatedBy>
        /// <CreatedOn>Aug 06, 2009</CreatedOn>
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
                LogManager._stringObject = "RequestApproval.ascx.cs ---- ";
                LogManager.SetExceptionDetails(ex, LogManager._stringObject, null);
                LogManager.LogException(ex, LogManager.LoggingCategory.General, LogManager.LoggingLevel.Error);
                CommonClass.Show(LogManager.ShowErrorDetail(ex));
            }
        }

        protected void ButtonSearch_Click(object sender, EventArgs e)
        {
            try
            {
                GridViewRequestApproval.DataBind();
            }
            catch (Exception ex)
            {
                ex.Data.Add("Events", "ButtonSearch_Click");
                ex.Data.Add("Page", "RequestApproval.ascx");
                LogManager._stringObject = "RequestApproval.ascx ---- Page_Load";
                LogManager.SetExceptionDetails(ex, LogManager._stringObject, null);
                LogManager.LogException(ex, LogManager.LoggingCategory.General, LogManager.LoggingLevel.Error);
                CommonClass.Show(LogManager.ShowErrorDetail(ex));
            }
        }

        protected void DropDownListPermission_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                DropDownList DropDownListPermission = default(DropDownList);
                DropDownListPermission = (DropDownList)sender;
                GridViewRow row = default(GridViewRow);
                row = (GridViewRow)DropDownListPermission.NamingContainer;

                //DropDownList LabelStateID = (DropDownList)row.FindControl("DropDownListPermission");               
            }
            catch (Exception ex)
            {
                ex.Data.Add("Events", "DropDownListPermission_SelectedIndexChanged");
                ex.Data.Add("Page", "RequestApproval.ascx");
                LogManager._stringObject = "RequestApproval.ascx ---- DropDownListPermission_SelectedIndexChanged";
                LogManager.SetExceptionDetails(ex, LogManager._stringObject, null);
                LogManager.LogException(ex, LogManager.LoggingCategory.General, LogManager.LoggingLevel.Error);
                CommonClass.Show(LogManager.ShowErrorDetail(ex));
            }
        }

        #endregion

        #region Properties

        private RMC.DataService.UserInfo UserDetail
        {
            get
            {
                return CommonClass.UserInformation;
            }
        }

        #endregion


        /// <summary>
        /// Sets Permissions in the Grid
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <CreatedBy>Mahesh Sachdeva</CreatedBy>
        /// <CreatedOn>june 17, 2010</CreatedOn>
        protected void GridViewRequestApproval_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                bool flag = false;
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    int userID = Convert.ToInt32(GridViewRequestApproval.DataKeys[e.Row.RowIndex].Value);
                    DropDownList dropDownList = (DropDownList)e.Row.FindControl("DropDownListPermission");

                    flag = false;
                    foreach (RMC.BusinessEntities.BEUserInfomation objectUserInformation in _objectGenericBEUserInfomation)
                    {

                        if (objectUserInformation.UserID == userID)
                        {
                            //CheckBox chkBox = (CheckBox)e.Row.FindControl("CheckBoxSelection");
                            //chkBox.Checked = true;
                            dropDownList.SelectedValue = objectUserInformation.PermissionId.ToString();
                            flag = true;
                        }
                    }
                    if (flag == false)
                    {
                        dropDownList.SelectedValue = "4";
                    }
                }
            }
            catch (Exception ex)
            {
                ex.Data.Add("Function", "GridViewRequestApproval_RowDataBound");
                ex.Data.Add("Page", "RequestApprovalForHospital");
                throw ex;
            }
        }

        /// <summary>
        /// Get the list of approved users of the hospital 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <CreatedBy>Mahesh Sachdeva</CreatedBy>
        /// <CreatedOn>june 17, 2010</CreatedOn>
        protected void GetApprovedUserList()
        {
            try
            {
                _objectGenericBEUserInfomation = new List<RMC.BusinessEntities.BEUserInfomation>();
                _objectBSMultiUserHospital = new RMC.BussinessService.BSMultiUserHospital();
                _objectGenericBEUserInfomation = _objectBSMultiUserHospital.GetUserInfomationByHospitalInfoID(HospitalInfoID);
            }
            catch (Exception ex)
            {
                ex.Data.Add("Function", "GetApprovedUserList()");
                ex.Data.Add("Page", "RequestApprovalForHospital");
                throw ex;
            }
        }
    }
}