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
    public partial class RequestApproval : System.Web.UI.UserControl
    {

        #region Variables

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
        List<RMC.DataService.MultiUserHospital> _objectGenericMultiUserHospital = null;

        //Bussiness Entity object 
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
     
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        //protected void ButtonApproved_Click(object sender, EventArgs e)
        //{
        //    bool flag = false;
        //    bool flagHospital = false;
        //    try
        //    {
        //        _objectBSMultiUserDemographic = new RMC.BussinessService.BSMultiUserDemographic();
        //        _objectBSMultiUserHospital = new RMC.BussinessService.BSMultiUserHospital();
        //        _objectGenericMultiUserDemographic = new List<RMC.DataService.MultiUserDemographic>();

        //        foreach (GridViewRow grdRow in GridViewRequestApproval.Rows)
        //        {

        //            CheckBox chkBox = (CheckBox)grdRow.FindControl("CheckBoxSelection");

        //            if (chkBox.Checked)
        //            {
        //                DropDownList DropDownListPermission = (DropDownList)grdRow.FindControl("DropDownListPermission");
        //                _objectMultiUserDemographic = new RMC.DataService.MultiUserDemographic();
        //                int userID = Convert.ToInt32(GridViewRequestApproval.DataKeys[grdRow.RowIndex].Value);
        //                if (!flagHospital)
        //                {
        //                    _objectMultiUserHospital = new RMC.DataService.MultiUserHospital();
        //                    _objectMultiUserHospital.CreatedBy = CommonClass.UserInformation.FirstName + " " + CommonClass.UserInformation.LastName;
        //                    _objectMultiUserHospital.CreatedDate = DateTime.Now;
        //                    _objectMultiUserHospital.HospitalInfoID = HospitalInfoID;
        //                    _objectMultiUserHospital.IsDeleted = false;
        //                    _objectMultiUserHospital.PermissionID = Convert.ToInt32(DropDownListPermission.SelectedValue);
        //                    _objectMultiUserHospital.UserID = userID;
        //                    flagHospital = true;
        //                }
        //                _objectMultiUserDemographic.CreatedBy = CommonClass.UserInformation.FirstName + " " + CommonClass.UserInformation.LastName;
        //                _objectMultiUserDemographic.CreatedDate = DateTime.Now;
        //                _objectMultiUserDemographic.HospitalDemographicID = HospitalDemographicId;
        //                _objectMultiUserDemographic.IsDeleted = false;
        //                _objectMultiUserDemographic.PermissionID = Convert.ToInt32(DropDownListPermission.SelectedValue);
        //                _objectMultiUserDemographic.UserID = userID;
        //                _objectGenericMultiUserDemographic.Add(_objectMultiUserDemographic);
        //            }
        //        }

        //        if (_objectMultiUserHospital != null)
        //        {
        //            if (_objectBSMultiUserHospital.InsertMultiUserHospitalForViewOnly(_objectMultiUserHospital))
        //            {
        //                if (_objectGenericMultiUserDemographic.Count > 0)
        //                {
        //                    flag = _objectBSMultiUserDemographic.InsertBulkMultiUserDemographic(_objectGenericMultiUserDemographic);
        //                }
        //            }
        //        }

        //        GridViewRequestApproval.DataBind();
        //        if (DemographicMemberTreeView != null)
        //        {
        //            DemographicMemberTreeView.BuildDemographicMembersTree();
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        ex.Data.Add("Events", "ButtonApproved_Click");
        //        ex.Data.Add("Page", "RequestApproval.ascx");
        //        LogManager._stringObject = "RequestApproval.ascx ---- ButtonApproved_Click";
        //        LogManager.SetExceptionDetails(ex, LogManager._stringObject, null);
        //        LogManager.LogException(ex, LogManager.LoggingCategory.General, LogManager.LoggingLevel.Error);
        //        CommonClass.Show(LogManager.ShowErrorDetail(ex));
        //    }
        //}

        protected void ButtonApproved_Click(object sender, EventArgs e)
        {
            bool flag = false;
            bool flagHospital = false;
            try
            {
                _objectBSMultiUserDemographic = new RMC.BussinessService.BSMultiUserDemographic();
                _objectBSMultiUserHospital = new RMC.BussinessService.BSMultiUserHospital();
                _objectGenericMultiUserDemographic = new List<RMC.DataService.MultiUserDemographic>();
                _objectGenericMultiUserHospital = new List<RMC.DataService.MultiUserHospital>();
                foreach (GridViewRow grdRow in GridViewRequestApproval.Rows)
                {

                    CheckBox chkBox = (CheckBox)grdRow.FindControl("CheckBoxSelection");

                    if (chkBox.Checked)
                    {
                        DropDownList DropDownListPermission = (DropDownList)grdRow.FindControl("DropDownListPermission");
                        _objectMultiUserDemographic = new RMC.DataService.MultiUserDemographic();
                        int userID = Convert.ToInt32(GridViewRequestApproval.DataKeys[grdRow.RowIndex].Value);
                        flagHospital = _objectBSMultiUserHospital.CheckUserExistsOrNot(userID, HospitalInfoID);
                        if (!flagHospital)
                        {
                            _objectMultiUserHospital = new RMC.DataService.MultiUserHospital();
                            _objectMultiUserHospital.CreatedBy = CommonClass.UserInformation.FirstName + " " + CommonClass.UserInformation.LastName;
                            _objectMultiUserHospital.CreatedDate = DateTime.Now;
                            _objectMultiUserHospital.HospitalInfoID = HospitalInfoID;
                            _objectMultiUserHospital.IsDeleted = false;
                            _objectMultiUserHospital.PermissionID = 2;// Convert.ToInt32(DropDownListPermission.SelectedValue);
                            _objectMultiUserHospital.UserID = userID;
                            _objectGenericMultiUserHospital.Add(_objectMultiUserHospital);
                            _objectBSMultiUserHospital.InsertMultiUserHospitalForViewOnly(_objectMultiUserHospital);
                            flagHospital = true;
                        }
                        _objectMultiUserDemographic.CreatedBy = CommonClass.UserInformation.FirstName + " " + CommonClass.UserInformation.LastName;
                        _objectMultiUserDemographic.CreatedDate = DateTime.Now;
                        _objectMultiUserDemographic.HospitalDemographicID = HospitalDemographicId;
                        _objectMultiUserDemographic.IsDeleted = false;
                        _objectMultiUserDemographic.PermissionID = Convert.ToInt32(DropDownListPermission.SelectedValue);
                        _objectMultiUserDemographic.UserID = userID;
                        _objectGenericMultiUserDemographic.Add(_objectMultiUserDemographic);
                    }
                }

               // if (_objectMultiUserHospital != null)
                //{
                 //   if (_objectBSMultiUserHospital.InsertMultiUserHospitalForViewOnly(_objectMultiUserHospital))
                   // {
                        if (_objectGenericMultiUserDemographic.Count > 0)
                        {
                            flag = _objectBSMultiUserDemographic.InsertBulkMultiUserDemographic(_objectGenericMultiUserDemographic);
                        }
                    //}
                //}
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
                    _objectBSHospitalDemographicDetail = new RMC.BussinessService.BSHospitalDemographicDetail();

                    LiteralHospitalName.Text = " (" + _objectBSHospitalInfo.GetHospitalNameByHospitalID(HospitalInfoID) + ") ";
                    LiteralUnitName.Text = "(" + _objectBSHospitalDemographicDetail.GetHospitalUnitNameByHospitalDemographicID(HospitalDemographicId) + ")";
                    GetApprovedUserList();
                }
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
                string sas = DropDownListPermission.SelectedValue;
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
                          //  CheckBox chkBox = (CheckBox)e.Row.FindControl("CheckBoxSelection");
                           // chkBox.Checked = true;
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
        /// Get the list of approved users of unit
        /// </summary>
        /// <CreatedBy>Mahesh Sachdeva </CreatedBy>
        /// <CreatedOn>June 18 , 2010</CreatedOn>
        protected void GetApprovedUserList()
        {
            try
            {   
                _objectGenericBEUserInfomation = new List<RMC.BusinessEntities.BEUserInfomation>();
                _objectBSMultiUserDemographic = new RMC.BussinessService.BSMultiUserDemographic();
                _objectGenericBEUserInfomation = _objectBSMultiUserDemographic.GetMultiUserDemographicByHospitalDemogaphicId(HospitalDemographicId);
            }
            catch (Exception ex)
            {
                ex.Data.Add("Function", "GetApprovedUserList()");
                ex.Data.Add("Page", "RequestApprovalForHospital");
                throw ex;
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

    }
    //End Of RequestApproval Class
}
//End Of NameSpace