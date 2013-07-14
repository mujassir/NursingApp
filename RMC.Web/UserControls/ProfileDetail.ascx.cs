using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using LogExceptions;

namespace RMC.Web.UserControls
{
    public partial class ProfileDetail : System.Web.UI.UserControl
    {

        #region Variables

        //Bussines Service object.
        RMC.BussinessService.BSProfileUser objectBSProfileUser = null;
        RMC.BussinessService.BSProfileType _objectBSProfileType = null;

        //Bussiness Entity Objects.
        RMC.BusinessEntities.BEProfileType _objectBEProfileType = null;

        //Total Records view on Page.
        int maxRecords = 652;

        #endregion

        #region Properties

        public Boolean enableCategory = true;

        public int PermissionID
        {
            get
            {
                return Convert.ToInt32(Request.QueryString["PermissionID"]);
            }
        }

        public int ProfileTypeID
        {
            get
            {
                return Convert.ToInt32(Request.QueryString["ProfileTypeID"]);
            }
        }
       
        #endregion

        #region Events

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                int totalPages = 0, totalNoOfRecords = 0, noOfRecords = 0;
                RMC.BussinessService.BSCategoryProfiles objectBSCategoryProfiles = new RMC.BussinessService.BSCategoryProfiles();

                if (!Page.IsPostBack)
                {
                    populateData();
                    if (HttpContext.Current.User.IsInRole("superadmin"))
                    {
                        GridViewCreateProfile.Columns[4].Visible = true;
                    }
                    else
                    {
                        objectBSProfileUser = new RMC.BussinessService.BSProfileUser();
                        bool flag = false;
                        flag = objectBSProfileUser.checkProfileStatus(CommonClass.UserInformation.UserID, ProfileTypeID);
                        if (flag)
                        {
                            //GridViewCreateProfile.Columns[4].Visible = true;
                            enableCategory = true;
                        }
                        else
                        {
                            //GridViewCreateProfile.Columns[4].Visible = false;
                            enableCategory = false;
                        }
                    }
                    //Set Paging Values.
                    TextBoxPageNo.Text = "1";
                    // TextBoxNoOfRecordsPerPage.Text = Convert.ToString(maxRecords);
                    totalNoOfRecords = objectBSCategoryProfiles.CountCategoryProfileByUserID(CommonClass.UserInformation.UserID, ProfileTypeID);
                    TextBoxNoOfRecordsPerPage.Text = Convert.ToString(totalNoOfRecords);
                    TextBoxPageNo.Attributes.Add("onBlur", "ValidateNoOfPages();");
                    TextBoxNoOfRecordsPerPage.Attributes.Add("onBlur", "ValidateNoOfRecords();");
                    LinkButtonShow.OnClientClick = "return ValidatePaggingOnClick();";

                    ImageButtonBack.PostBackUrl = Request.UrlReferrer.AbsoluteUri;
                }
                int.TryParse(TextBoxNoOfRecordsPerPage.Text, out noOfRecords);
                totalNoOfRecords = objectBSCategoryProfiles.CountCategoryProfileByUserID(CommonClass.UserInformation.UserID, ProfileTypeID);
                totalPages = RMC.BussinessService.BSCustomizedPaging.GetNoOfPages(noOfRecords, totalNoOfRecords);
                LabelCurrentPageNo.Text = TextBoxPageNo.Text;
                LabelTotalNoOfRecords.Text = Convert.ToString(totalNoOfRecords);
                LabelTotalPages.Text = Convert.ToString(totalPages);
            }
            catch (Exception ex)
            {
                ex.Data.Add("Event", "Page_Load");
                ex.Data.Add("Page", "ProfileDetail.ascx");
                LogManager._stringObject = "ProfileDetail.ascx ---- Page_Load";
                LogManager.SetExceptionDetails(ex, LogManager._stringObject, null);
                LogManager.LogException(ex, LogManager.LoggingCategory.General, LogManager.LoggingLevel.Error);
                CommonClass.Show(LogManager.ShowErrorDetail(ex));
            }
        }

        protected void ObjectDataSourceRequestList_Selecting(object sender, ObjectDataSourceSelectingEventArgs e)
        {
            try
            {
                string sortExpression=" ";string sortOrder=" ";
                int noOfSkipRecords = 0, noOfRecords = 0, currentPageNo = 0;
                int.TryParse(TextBoxNoOfRecordsPerPage.Text, out noOfRecords);
                int.TryParse(TextBoxPageNo.Text, out currentPageNo);
                if(this.ViewState["SortExp"] != null)
                    sortExpression = this.ViewState["SortExp"].ToString();
                if(this.ViewState["SortOrder"] != null)
                    sortOrder = this.ViewState["SortOrder"].ToString();
                noOfSkipRecords = RMC.BussinessService.BSCustomizedPaging.GetNoOfSkipRecords(noOfRecords, currentPageNo);

                e.InputParameters[0] = CommonClass.UserInformation.UserID;
                e.InputParameters[1] = ProfileTypeID;
                e.InputParameters[2] = noOfSkipRecords;
                e.InputParameters[3] = noOfRecords;
                e.InputParameters[4] = sortExpression;
                e.InputParameters[5] = sortOrder;
            }
            catch (Exception ex)
            {
                ex.Data.Add("Event", "ObjectDataSourceRequestList_Selecting");
                ex.Data.Add("Page", "ProfileDetail.ascx");
                LogManager._stringObject = "ProfileDetail.ascx ---- ObjectDataSourceRequestList_Selecting";
                LogManager.SetExceptionDetails(ex, LogManager._stringObject, null);
                LogManager.LogException(ex, LogManager.LoggingCategory.General, LogManager.LoggingLevel.Error);
                CommonClass.Show(LogManager.ShowErrorDetail(ex));
            }
        }

        protected void ButtonUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                if (Page.IsValid)
                {
                    _objectBSProfileType = new RMC.BussinessService.BSProfileType();
                    _objectBEProfileType = new RMC.BusinessEntities.BEProfileType();

                    _objectBEProfileType.ProfileName = TextBoxProfileType.Text;
                    _objectBEProfileType.Description = TextBoxDecription.Text;
                    _objectBEProfileType.AuthorName = LinkButtonAuthorName.Text;
                    _objectBEProfileType.IsShare = checkBoxShare.Checked;
                    _objectBEProfileType.ProfileTypeID = ProfileTypeID;

                    if (_objectBSProfileType.UpdateProfileType(_objectBEProfileType))
                    {
                        CommonClass.Show("Record Update Successfully.");
                    }
                    else
                    {
                        CommonClass.Show("Failed to Update the Record.");
                    }
                }
            }
            catch (Exception ex)
            {
                ex.Data.Add("Event", "ButtonUpdate_Click");
                ex.Data.Add("Page", "ProfileDetail.ascx");
                LogManager._stringObject = "ProfileDetail.ascx ---- ButtonUpdate_Click";
                LogManager.SetExceptionDetails(ex, LogManager._stringObject, null);
                LogManager.LogException(ex, LogManager.LoggingCategory.General, LogManager.LoggingLevel.Error);
                CommonClass.Show(LogManager.ShowErrorDetail(ex));
            }
        }

        protected void LinkButtonDeleteSelected_Click(object sender, EventArgs e)
        {
            try
            {
                List<int> objectGenericCategoryProfileIDs = new List<int>();
                RMC.BussinessService.BSCategoryProfiles objectBSCategoryProfiles = new RMC.BussinessService.BSCategoryProfiles();

                foreach (GridViewRow grdRow in GridViewCreateProfile.Rows)
                {
                    CheckBox chkBox = (CheckBox)grdRow.FindControl("CheckBoxDelete");

                    if (chkBox.Checked)
                    {
                        objectGenericCategoryProfileIDs.Add(Convert.ToInt32(GridViewCreateProfile.DataKeys[grdRow.RowIndex].Value));
                    }
                }

                if (objectGenericCategoryProfileIDs.Count > 0)
                {
                    int totalPages = 0, totalNoOfRecords = 0, noOfRecords = 0, currentPage = 0;
                    objectBSCategoryProfiles.DeleteCategoryProfileByCategoryProfileID(objectGenericCategoryProfileIDs);

                    int.TryParse(TextBoxNoOfRecordsPerPage.Text, out noOfRecords);
                    totalNoOfRecords = objectBSCategoryProfiles.CountCategoryProfileByUserID(CommonClass.UserInformation.UserID, ProfileTypeID);
                    totalPages = RMC.BussinessService.BSCustomizedPaging.GetNoOfPages(noOfRecords, totalNoOfRecords);
                    int.TryParse(TextBoxPageNo.Text, out currentPage);
                    if (totalPages < currentPage)
                    {
                        TextBoxPageNo.Text = Convert.ToString(totalPages);
                    }

                    LabelCurrentPageNo.Text = TextBoxPageNo.Text;
                    LabelTotalNoOfRecords.Text = Convert.ToString(totalNoOfRecords);
                    LabelTotalPages.Text = Convert.ToString(totalPages);
                    GridViewCreateProfile.DataBind();
                }
            }
            catch (Exception ex)
            {
                LogManager._stringObject = "CreateNewProfile.ascx ---- DropDownListProfileType_SelectedIndexChanged";
                LogManager.SetExceptionDetails(ex, LogManager._stringObject, null);
                LogManager.LogException(ex, LogManager.LoggingCategory.General, LogManager.LoggingLevel.Error);
                CommonClass.Show(LogManager.ShowErrorDetail(ex));
            }
        }

        protected void LinkButtonShow_Click(object sender, EventArgs e)
        {
            try
            {
                int[] validationIDs = { };
                int noOfRecordsPerPage = 0, totalNoOfRecords = 0, currentPageNo = 0, totalPages = 0;

                if (ViewState["ValidationIDs"] != null)
                {
                    validationIDs = (int[])ViewState["ValidationIDs"];
                }
                
                int.TryParse(TextBoxNoOfRecordsPerPage.Text, out noOfRecordsPerPage);
                int.TryParse(LabelTotalNoOfRecords.Text, out totalNoOfRecords);
                int.TryParse(TextBoxPageNo.Text, out currentPageNo);
                int.TryParse(LabelTotalPages.Text, out totalPages);
                if (totalPages < currentPageNo)
                {
                    TextBoxPageNo.Text = Convert.ToString(totalPages);
                    currentPageNo = totalPages;
                }

                if (totalNoOfRecords < noOfRecordsPerPage)
                {
                    TextBoxNoOfRecordsPerPage.Text = Convert.ToString(totalNoOfRecords);
                    noOfRecordsPerPage = totalNoOfRecords;
                }
                if (noOfRecordsPerPage <= totalNoOfRecords && noOfRecordsPerPage > 0 && currentPageNo <= totalPages && currentPageNo > 0)
                    GridViewCreateProfile.DataBind();
                LabelCurrentPageNo.Text = TextBoxPageNo.Text;
            }
            catch (Exception ex)
            {
                LogManager._stringObject = "CreateNewProfile.ascx ---- LinkButtonShow_Click";
                LogManager.SetExceptionDetails(ex, LogManager._stringObject, null);
                LogManager.LogException(ex, LogManager.LoggingCategory.General, LogManager.LoggingLevel.Error);
                CommonClass.Show(LogManager.ShowErrorDetail(ex));
            }
        }

        protected void LinkButtonUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                RMC.BussinessService.BSCategoryProfiles objectBSCategoryProfiles = new RMC.BussinessService.BSCategoryProfiles();
                List<RMC.BusinessEntities.BECategoryProfile> objectGenericBECategoryProfile = new List<RMC.BusinessEntities.BECategoryProfile>();
                List<int> objectGenericCategorProfileIDs = new List<int>();

                foreach (GridViewRow grdRow in GridViewCreateProfile.Rows)
                {
                    RMC.BusinessEntities.BECategoryProfile objectBECategoryProfile = new RMC.BusinessEntities.BECategoryProfile();

                    objectBECategoryProfile.CategoryAssignmentID = Convert.ToInt32(((DropDownList)grdRow.FindControl("DropDownListCategoryAssignment")).SelectedValue);
                    objectBECategoryProfile.CategoryProfileID = Convert.ToInt32(GridViewCreateProfile.DataKeys[grdRow.RowIndex].Value);

                    objectGenericCategorProfileIDs.Add(objectBECategoryProfile.CategoryProfileID);
                    objectGenericBECategoryProfile.Add(objectBECategoryProfile);
                }

                if (objectGenericBECategoryProfile.Count > 0 && objectGenericCategorProfileIDs.Count > 0)
                {
                    objectBSCategoryProfiles.UpdateCategoryProfile(objectGenericCategorProfileIDs, objectGenericBECategoryProfile);
                }
            }
            catch (Exception ex)
            {
                LogManager._stringObject = "CreateNewProfile.ascx ---- DropDownListProfileType_SelectedIndexChanged";
                LogManager.SetExceptionDetails(ex, LogManager._stringObject, null);
                LogManager.LogException(ex, LogManager.LoggingCategory.General, LogManager.LoggingLevel.Error);
                CommonClass.Show(LogManager.ShowErrorDetail(ex));
            }
        }

        protected void ImageButtonFirst_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                int returnValue = 0;
                int noOfRecordsPerPage = 0, totalNoOfRecords = 0, currentPageNo = 0, totalPages = 0, noOfSkipRecords = 0;
                int[] validationIDs = { };

                if (ViewState["ValidationIDs"] != null)
                {
                    validationIDs = (int[])ViewState["ValidationIDs"];
                }

                returnValue = RMC.BussinessService.BSCustomizedPaging.GetFirstValue();
                TextBoxPageNo.Text = Convert.ToString(returnValue);

                int.TryParse(TextBoxNoOfRecordsPerPage.Text, out noOfRecordsPerPage);
                int.TryParse(LabelTotalNoOfRecords.Text, out totalNoOfRecords);
                int.TryParse(TextBoxPageNo.Text, out currentPageNo);
                int.TryParse(LabelTotalPages.Text, out totalPages);

                if (totalPages < currentPageNo)
                {
                    TextBoxPageNo.Text = Convert.ToString(1);
                    currentPageNo = 1;
                }

                if (totalNoOfRecords < noOfRecordsPerPage)
                {
                    TextBoxNoOfRecordsPerPage.Text = Convert.ToString(totalNoOfRecords);
                    noOfRecordsPerPage = totalNoOfRecords;
                }
                                
                GridViewCreateProfile.DataBind();
                LabelCurrentPageNo.Text = TextBoxPageNo.Text;
            }
            catch (Exception ex)
            {
                LogManager._stringObject = "CreateNewProfile.ascx ---- ImageButtonFirst_Click";
                LogManager.SetExceptionDetails(ex, LogManager._stringObject, null);
                LogManager.LogException(ex, LogManager.LoggingCategory.General, LogManager.LoggingLevel.Error);
                CommonClass.Show(LogManager.ShowErrorDetail(ex));
            }
        }

        protected void ImageButtonStepBackward_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                int currentValue = 0, returnValue = 0;
                int noOfRecordsPerPage = 0, totalNoOfRecords = 0, currentPageNo = 0, totalPages = 0, noOfSkipRecords = 0;
                int[] validationIDs = { };

                if (ViewState["ValidationIDs"] != null)
                {
                    validationIDs = (int[])ViewState["ValidationIDs"];
                }
                int.TryParse(TextBoxPageNo.Text, out currentValue);

                returnValue = RMC.BussinessService.BSCustomizedPaging.GetBackwardStep(currentValue, 1);
                TextBoxPageNo.Text = Convert.ToString(returnValue);
                int.TryParse(TextBoxPageNo.Text, out currentPageNo);
                int.TryParse(TextBoxNoOfRecordsPerPage.Text, out noOfRecordsPerPage);
                int.TryParse(LabelTotalNoOfRecords.Text, out totalNoOfRecords);
                int.TryParse(LabelTotalPages.Text, out totalPages);

                if (totalPages < currentPageNo)
                {
                    TextBoxPageNo.Text = Convert.ToString(totalPages);
                    currentPageNo = totalPages;
                }

                if (totalNoOfRecords < noOfRecordsPerPage)
                {
                    TextBoxNoOfRecordsPerPage.Text = Convert.ToString(totalNoOfRecords);
                    noOfRecordsPerPage = totalNoOfRecords;
                }
                
                GridViewCreateProfile.DataBind();
                LabelCurrentPageNo.Text = TextBoxPageNo.Text;
            }
            catch (Exception ex)
            {
                LogManager._stringObject = "CreateNewProfile.ascx ---- ImageButtonFirst_Click";
                LogManager.SetExceptionDetails(ex, LogManager._stringObject, null);
                LogManager.LogException(ex, LogManager.LoggingCategory.General, LogManager.LoggingLevel.Error);
                CommonClass.Show(LogManager.ShowErrorDetail(ex));
            }
        }

        protected void ImageButtonStepForward_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                int currentValue = 0, lastPageNo = 0, returnValue = 0;
                int noOfRecordsPerPage = 0, totalNoOfRecords = 0, currentPageNo = 0, totalPages = 0, noOfSkipRecords = 0;
                int[] validationIDs = { };

                if (ViewState["ValidationIDs"] != null)
                {
                    validationIDs = (int[])ViewState["ValidationIDs"];
                }
                int.TryParse(TextBoxPageNo.Text, out currentValue);
                int.TryParse(LabelTotalPages.Text, out lastPageNo);

                returnValue = RMC.BussinessService.BSCustomizedPaging.GetForwardStep(currentValue, lastPageNo);
                TextBoxPageNo.Text = Convert.ToString(returnValue);
                int.TryParse(TextBoxPageNo.Text, out currentPageNo);
                int.TryParse(TextBoxNoOfRecordsPerPage.Text, out noOfRecordsPerPage);
                int.TryParse(LabelTotalNoOfRecords.Text, out totalNoOfRecords);
                int.TryParse(LabelTotalPages.Text, out totalPages);

                if (totalPages < currentPageNo)
                {
                    TextBoxPageNo.Text = Convert.ToString(totalPages);
                    currentPageNo = totalPages;
                }

                if (totalNoOfRecords < noOfRecordsPerPage)
                {
                    TextBoxNoOfRecordsPerPage.Text = Convert.ToString(totalNoOfRecords);
                    noOfRecordsPerPage = totalNoOfRecords;
                }
                
                GridViewCreateProfile.DataBind();
                LabelCurrentPageNo.Text = TextBoxPageNo.Text;
            }
            catch (Exception ex)
            {
                LogManager._stringObject = "CreateNewProfile.ascx ---- ImageButtonFirst_Click";
                LogManager.SetExceptionDetails(ex, LogManager._stringObject, null);
                LogManager.LogException(ex, LogManager.LoggingCategory.General, LogManager.LoggingLevel.Error);
                CommonClass.Show(LogManager.ShowErrorDetail(ex));
            }
        }

        protected void ImageButtonLast_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                int currentValue = 0, lastPageNo = 0, returnValue = 0;
                int noOfRecordsPerPage = 0, totalNoOfRecords = 0, currentPageNo = 0, totalPages = 0, noOfSkipRecords = 0;
                int[] validationIDs = { };

                if (ViewState["ValidationIDs"] != null)
                {
                    validationIDs = (int[])ViewState["ValidationIDs"];
                }
                int.TryParse(TextBoxPageNo.Text, out currentValue);
                int.TryParse(LabelTotalPages.Text, out lastPageNo);

                returnValue = RMC.BussinessService.BSCustomizedPaging.GetLastValue(lastPageNo);
                TextBoxPageNo.Text = Convert.ToString(returnValue);
                int.TryParse(TextBoxPageNo.Text, out currentPageNo);
                int.TryParse(TextBoxNoOfRecordsPerPage.Text, out noOfRecordsPerPage);
                int.TryParse(LabelTotalNoOfRecords.Text, out totalNoOfRecords);
                int.TryParse(LabelTotalPages.Text, out totalPages);

                if (totalPages < currentPageNo)
                {
                    TextBoxPageNo.Text = Convert.ToString(totalPages);
                    currentPageNo = totalPages;
                }

                if (totalNoOfRecords < noOfRecordsPerPage)
                {
                    TextBoxNoOfRecordsPerPage.Text = Convert.ToString(totalNoOfRecords);
                    noOfRecordsPerPage = totalNoOfRecords;
                }
                
                GridViewCreateProfile.DataBind();
                LabelCurrentPageNo.Text = TextBoxPageNo.Text;
            }
            catch (Exception ex)
            {
                LogManager._stringObject = "CreateNewProfile.ascx ---- ImageButtonFirst_Click";
                LogManager.SetExceptionDetails(ex, LogManager._stringObject, null);
                LogManager.LogException(ex, LogManager.LoggingCategory.General, LogManager.LoggingLevel.Error);
                CommonClass.Show(LogManager.ShowErrorDetail(ex));
            }
        }

        #endregion

        #region Private Methods

        private void populateData()
        {
            try
            {
                _objectBSProfileType = new RMC.BussinessService.BSProfileType();

                _objectBEProfileType = _objectBSProfileType.GetProfileTypeByProfileTypeID(ProfileTypeID);
                //TextBoxAuthorName.Text = _objectProfileType.a;
                TextBoxDecription.Text = _objectBEProfileType.Description;
                TextBoxProfileType.Text = _objectBEProfileType.ProfileName;
                LinkButtonAuthorName.Text = _objectBEProfileType.AuthorName;
                checkBoxShare.Checked = _objectBEProfileType.IsShare;
                if (!HttpContext.Current.User.IsInRole("superadmin"))
                {
                    if (_objectBEProfileType.ProfileName.ToLower().Trim() == "combined profile" ||
                        _objectBEProfileType.ProfileName.ToLower().Trim() == "ascension tcab" ||
                        _objectBEProfileType.ProfileName.ToLower().Trim() == "ihi phase ii" ||
                        _objectBEProfileType.ProfileName.ToLower().Trim() == "ihi phase iii" ||
                        _objectBEProfileType.ProfileName.ToLower().Trim() == "ihi phase iv" ||
                        _objectBEProfileType.ProfileName.ToLower().Trim() == "rmc phase iv" ||
                        _objectBEProfileType.ProfileName.ToLower().Trim() == "rmc phase v" ||
                        _objectBEProfileType.UserID != CommonClass.UserInformation.UserID)
                    {
                        ButtonUpdate.Visible = false;
                        divUpdateDisable.Visible = true;
                        TextBoxDecription.ReadOnly = true;
                        TextBoxProfileType.ReadOnly = true;
                        LinkButtonAuthorName.Enabled = true;
                        LinkButtonAuthorName.OnClientClick = "mypopup('" + _objectBEProfileType.UserID.ToString() + "'); return false;";
                        checkBoxShare.Enabled = false;
                    }
                }
                else
                {
                    if (_objectBEProfileType.UserID != CommonClass.UserInformation.UserID)
                    {
                        LinkButtonAuthorName.Enabled = true;
                        LinkButtonAuthorName.OnClientClick = "mypopupAdministrator('" + _objectBEProfileType.UserID.ToString() + "'); return false;";
                    }
                }
            }
            catch (Exception ex)
            {
                ex.Data.Add("Function", "populateData");
                ex.Data.Add("Class", "ProfileDetail");
                throw ex;
            }
        }

        #endregion

        protected void GridViewCreateProfile_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName.Equals("Sort"))
            {
                if (this.ViewState["SortExp"] == null)
                {
                    this.ViewState["SortExp"] = e.CommandArgument.ToString();
                    this.ViewState["SortOrder"] = "ASC";
                }
                else
                {
                    if (this.ViewState["SortExp"].ToString() == e.CommandArgument.ToString())
                    {
                        if (this.ViewState["SortOrder"].ToString() == "ASC")
                            this.ViewState["SortOrder"] = "DESC";
                        else
                            this.ViewState["SortOrder"] = "ASC";
                    }
                    else
                    {
                        this.ViewState["SortOrder"] = "ASC";
                        this.ViewState["SortExp"] = e.CommandArgument.ToString();
                    }
                }

                GridViewCreateProfile.DataBind();
            }
        }

        protected void ButtonSortAll_Click(object sender, EventArgs e)
        {
            this.ViewState["SortExp"] = "SortAll";
            if (this.ViewState["SortOrder"] != null && this.ViewState["SortOrder"] == "ASC")
            {
                this.ViewState["SortOrder"] = "DESC";
            }
            else
            {
                this.ViewState["SortOrder"] = "ASC";
            }

            GridViewCreateProfile.DataBind();
        }
    }
}