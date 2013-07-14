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
using RMC.BusinessEntities;
using RMC.BussinessService;

namespace RMC.Web.UserControls
{
    public partial class RequestApprovalNotification : System.Web.UI.UserControl
    {

        #region Variables

        bool _emailFlag;
        string _bodyText, _fromAddress, _toAddress, _subjectText;

        //Bussiness Object
        RMC.BussinessService.BSViewRequest _objectBSViewRequest = null;
        RMC.BussinessService.BSMultiUserHospital _objectBSMultiUserHospital = null;
        RMC.BussinessService.BSMultiUserDemographic _objectBSMultiUserDemographic = null;

        //Data Service Objects.
        RMC.DataService.ViewRequest _objectViewRequest = null;

        //Generic List Of Data Service objects.
        List<RMC.DataService.ViewRequest> _objectGenericViewRequest = null;

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

        private RMC.DataService.UserInfo UserDetail
        {
            get
            {
                //return CommonClass.UserInformation;
                return CommonClass.UserInformation;
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
            bool flag = false;

            try
            {
                _objectGenericViewRequest = new List<RMC.DataService.ViewRequest>();
                _objectBSViewRequest = new RMC.BussinessService.BSViewRequest();
                _objectBSMultiUserHospital = new RMC.BussinessService.BSMultiUserHospital();
                _objectBSMultiUserDemographic = new RMC.BussinessService.BSMultiUserDemographic();

                foreach (GridViewRow grdRow in GridViewRequestApproval.Rows)
                {
                    _objectViewRequest = new RMC.DataService.ViewRequest();
                    CheckBox chkBox = (CheckBox)grdRow.FindControl("CheckBoxApproval");
                    RadioButton RadioButtonApproved = (RadioButton)grdRow.FindControl("RadioButtonApproved");
                    RadioButton RadioButtonNotApproved = (RadioButton)grdRow.FindControl("RadioButtonNotApproved");
                    Label fromUserID = (Label)grdRow.FindControl("LabelFromUserID");
                    // Added for EmailFunctionlity 13/12/2010
                    ViewState["UserID"] = Convert.ToString(fromUserID.Text.Trim());

                    // Added for Email Functionality 13/12/2010
                    Label toUserID = (Label)grdRow.FindControl("LabelToUserID");
                    Label hospitalInfoID = (Label)grdRow.FindControl("LabelHospitalID");
                    Label demographicDetailID = (Label)grdRow.FindControl("LabelDemograhicID");
                    DropDownList DropDownListPermission = (DropDownList)grdRow.FindControl("DropDownListPermission");
                    /////------ ***** Change by Raman ----- [24-Sept.]
                    ////-------****** Change by Mahesh Sachdeva-----[24 june 2010]

                    int requestID = Convert.ToInt32(GridViewRequestApproval.DataKeys[grdRow.RowIndex].Value);
                    _objectViewRequest.PermissionId = Convert.ToInt16(DropDownListPermission.SelectedItem.Value);
                    _objectViewRequest.RequestID = requestID;
                    _objectViewRequest.FromUserID = Convert.ToInt32(fromUserID.Text);
                    _objectViewRequest.ToUserID = Convert.ToInt32(toUserID.Text);
                    _objectViewRequest.HospitalID = Convert.ToInt32(hospitalInfoID.Text);
                    _objectViewRequest.HospitalDemographicDetailID = Convert.ToInt32(demographicDetailID.Text);
                    _objectViewRequest.IsApproved = RadioButtonApproved.Checked;
                    if (RadioButtonApproved.Checked)
                    {
                        _objectGenericViewRequest.Add(_objectViewRequest);

                        // Added for Email Functionality, 13/12/2010 
                        // If the radio button is checked (Approved), then email goes out to the logged in user 

                        

                        RMC.BussinessService.BSCommon _objectBSCommon = new RMC.BussinessService.BSCommon();
                        RMC.BussinessService.BSUsers _objectBSUser = new RMC.BussinessService.BSUsers();

                        try
                        {
                            string email = string.Empty;
                            RMC.BussinessService.BSEmail _objectBSEmail;
                            //int UserId = Convert.ToInt32(ViewState["UserID"]);
                            _toAddress = _objectBSUser.GetUserEmailByUserID(Convert.ToInt32(ViewState["UserID"]));
                            if (DropDownListPermission.SelectedItem.Text == "Owner")
                            {
                                _bodyText = "Your request for hospital unit Access record and reporting access in the Time Study RN National Benchmarking Database has been approved. You can now login to the database using your email address and password. <br><br>You can now edit the Hospital Units Details.<br><br>If you have any questions, then please do not hesitate to contact the system administrator at (513) 624-6629 or by email at nlee@rapidmodeling.com.<br><br>Thanks for participating in the Time Study RN National Benchmarking Database.<br><br>Sincerely,<br>System Administrator<br>Time Study RN National Benchmarking Database Project";
                            }
                            if (DropDownListPermission.SelectedItem.Text == "ReadOnly")
                            {
                                _bodyText = "Your request for hospital unit Access record and reporting access in the Time Study RN National Benchmarking Database has been approved. You can now login to the database using your email address and password.<br><br> If you have any questions, then please do not hesitate to contact the system administrator at (513) 624-6629 or by email at nlee@rapidmodeling.com.<br><br>Thanks for participating in the Time Study RN National Benchmarking Database.<br><br>Sincerely,<br>System Administrator<br>Time Study RN National Benchmarking Database Project";
                            }
                            else if (DropDownListPermission.SelectedItem.Text == "Upload Data")
                            {
                                _bodyText = "Your request for hospital unit Access record and reporting access in the Time Study RN National Benchmarking Database has been approved. You can now login to the database using your email address and password. <br><br>You can now Upload Data for respective Hospital Units.<br><br>If you have any questions, then please do not hesitate to contact the system administrator at (513) 624-6629 or by email at nlee@rapidmodeling.com.<br><br>Thanks for participating in the Time Study RN National Benchmarking Database.<br><br>Sincerely,<br>System Administrator<br>Time Study RN National Benchmarking Database Project";
                            }

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
        #endregion

                        // Added for Email Functionality, 13/12/2010
                    }
                    else if (RadioButtonNotApproved.Checked)
                    {
                        flag = _objectBSViewRequest.DeleteViewRequest(Convert.ToInt32(GridViewRequestApproval.DataKeys[grdRow.RowIndex].Value));
                    }
                    //------------------------------------------------
                }

                flag = _objectBSViewRequest.UpdateViewRequest(_objectGenericViewRequest);

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
        /// Use to Pass Parameter.
        /// Created By : Davinder Kumar.
        /// Creation Date : July 29, 2009.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ObjectDataSourceRequestApproval_Selecting(object sender, ObjectDataSourceSelectingEventArgs e)
        {
            try
            {
                e.InputParameters[0] = UserDetail.UserID;
                //e.InputParameters[1] = UserDetail.UserType.UserType1.ToLower().Trim();
                int userTypeID = UserDetail.UserTypeID;
                if (userTypeID == 1)
                {
                    e.InputParameters[1] = "SuperAdmin".ToLower().Trim();
                }
                if (userTypeID == 2)
                {
                    e.InputParameters[1] = "Admin".ToLower().Trim();
                }
                if (userTypeID == 3)
                {
                    e.InputParameters[1] = "PowerUser".ToLower().Trim();
                }
                if (userTypeID == 4)
                {
                    e.InputParameters[1] = "User".ToLower().Trim();
                }

            }
            catch (Exception ex)
            {
                //Response.Write(ex);
                ex.Data.Add("Events", "ObjectDataSourceRequestApproval_Selecting");
                ex.Data.Add("Page", "RequestApproval.ascx");
                LogManager._stringObject = "RequestApproval.ascx ---- ObjectDataSourceRequestApproval_Selecting";
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
                    GridViewRequestApproval.DataBind();
            }
            catch (Exception ex)
            {
                //Response.Write(ex);
                ex.Data.Add("Events", "Page_Load");
                ex.Data.Add("Page", "RequestApproval.ascx");
                LogManager._stringObject = "RequestApproval.ascx ---- Page_Load";
                LogManager.SetExceptionDetails(ex, LogManager._stringObject, null);
                LogManager.LogException(ex, LogManager.LoggingCategory.General, LogManager.LoggingLevel.Error);
                CommonClass.Show(LogManager.ShowErrorDetail(ex));
            }
        }

        /// <summary>
        /// Page PreRender.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_PreRender(object sender, EventArgs e)
        {
            try
            {
                if (GridViewRequestApproval.Rows.Count == 0)
                {
                    ButtonApproved.Visible = false;
                }
                else
                {
                    ButtonApproved.Visible = true;
                }
            }
            catch (Exception ex)
            {
                ex.Data.Add("Events", "Page_Load");
                ex.Data.Add("Page", "RequestApproval.ascx");
                LogManager._stringObject = "RequestApproval.ascx ---- Page_Load";
                LogManager.SetExceptionDetails(ex, LogManager._stringObject, null);
                LogManager.LogException(ex, LogManager.LoggingCategory.General, LogManager.LoggingLevel.Error);
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
            int _hospitalDemographicId = 0;
            try
            {
                if (Request.QueryString["HospitalDemographicId"] != null)
                {
                    _hospitalDemographicId = Convert.ToInt32(Request.QueryString["HospitalDemographicId"]);
                }

                if (HttpContext.Current.User.IsInRole("superadmin"))
                {
                    Response.Redirect("~/Administrator/HospitalDemographicDetail.aspx?HospitalDemographicId=" + _hospitalDemographicId, false);
                }
                else
                {
                    Response.Redirect("~/Users/HospitalDemographicDetail.aspx?HospitalDemographicId=" + _hospitalDemographicId, false);
                }
            }
            catch (Exception ex)
            {
                ex.Data.Add("Events", "ImageButtonBack_Click");
                ex.Data.Add("Page", "RequestApproval.ascx");
                LogManager._stringObject = "RequestApproval.ascx ---- ImageButtonBack_Click";
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
        protected void ImageButtonDelete_Click(object sender, ImageClickEventArgs e)
        {
            bool flag = false;
            try
            {
                _objectBSViewRequest = new RMC.BussinessService.BSViewRequest();

                GridViewRow grdRow = (GridViewRow)((ImageButton)sender).NamingContainer;
                flag = _objectBSViewRequest.DeleteViewRequest(Convert.ToInt32(GridViewRequestApproval.DataKeys[grdRow.RowIndex].Value));
                if (flag)
                {
                    GridViewRequestApproval.DataBind();
                }
            }
            catch (Exception ex)
            {
                ex.Data.Add("Events", "ImageButtonDelete_Click");
                ex.Data.Add("Page", "RequestApproval.ascx");
                LogManager._stringObject = "RequestApproval.ascx ---- ImageButtonDelete_Click";
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
                string sas = DropDownListPermission.SelectedValue;
            }
            catch (Exception ex)
            {
                ex.Data.Add("Events", "ImageButtonDelete_Click");
                ex.Data.Add("Page", "RequestApproval.ascx");
                LogManager._stringObject = "RequestApproval.ascx ---- ImageButtonDelete_Click";
                LogManager.SetExceptionDetails(ex, LogManager._stringObject, null);
                LogManager.LogException(ex, LogManager.LoggingCategory.General, LogManager.LoggingLevel.Error);
                CommonClass.Show(LogManager.ShowErrorDetail(ex));
            }
        }

        protected void GridViewRequestApproval_RowDataBound(object sender, GridViewRowEventArgs e)
        {

            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    DropDownList permissionDropDown = (DropDownList)(e.Row.FindControl("DropDownListPermission"));
                    permissionDropDown.SelectedValue = "2";
                }
            }
            catch (Exception ex)
            {
                ex.Data.Add("Events", "GridViewRequestApproval_Row DataBound");
                ex.Data.Add("Page", "RequestApproval.ascx");
                LogManager._stringObject = "RequestApproval.ascx ---- ObjectDataSourceRequestApproval_Selecting";
                LogManager.SetExceptionDetails(ex, LogManager._stringObject, null);
                LogManager.LogException(ex, LogManager.LoggingCategory.General, LogManager.LoggingLevel.Error);
                CommonClass.Show(LogManager.ShowErrorDetail(ex));
            }
        }

    }
}