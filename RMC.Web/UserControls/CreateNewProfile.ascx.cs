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
using System.Collections.Generic;

namespace RMC.Web.UserControls
{
    public partial class CreateNewProfile : System.Web.UI.UserControl
    {

        #region "Variables"

        List<RMC.DataService.ProfileType> objectGenericPreofileType = null;
        RMC.BussinessService.BSProfileType objectBSProfileType = null;
        List<RMC.BusinessEntities.BECategoryProfile> objectGenericBECategoryProfile = null;
        RMC.BussinessService.BSCategoryProfiles objectCategoryProfile = null;
        RMC.DataService.ProfileType objectDSProfileType = null;
        RMC.DataService.ProfileUser objectDSProfileUser = null;
        RMC.DataService.CategoryProfile objectDSCategoryProfile = null;
        List<RMC.DataService.CategoryProfile> objectGenericCategoryProfile = null;

        //Total Records view on Page.
        int maxRecords = 652;

        #endregion

        #region Properties

        public int SelectedProfileIndex
        {
            get
            {
                if (Session["SelectedProfileIndex"] != null)
                    return Convert.ToInt32(Session["SelectedProfileIndex"]);
                else
                    return 0;

            }
        }

        public int SelectedProfileValue
        {
            get
            {
                if (Session["SelectedProfileValue"] != null)
                    return Convert.ToInt32(Session["SelectedProfileValue"]);
                else
                    return 0;
            }
        }

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

        public string PType
        {
            get
            {
                if (Convert.ToInt16(Request.QueryString["valuetype"]) == 0)
                {
                    return "value added";
                }
                else if (Convert.ToInt16(Request.QueryString["valuetype"]) == 1)
                {
                    return "others";
                }
                else if (Convert.ToInt16(Request.QueryString["valuetype"]) == 3)
                {
                    return "activities";
                }
                else
                {
                    return "location";
                }
            }
        }

        #endregion

        #region "Methods"

        /// <summary>
        /// Bind DropDownList with ProfileType values.
        /// </summary>
        //private void BindDropDownListProfileType()
        //{
        //    try
        //    {
        //        objectBSProfileType = new RMC.BussinessService.BSProfileType();
        //        objectGenericPreofileType = objectBSProfileType.GetProfileInformation(PType);
        //        if (objectGenericPreofileType.Count > 0)
        //        {
        //            DropDownListProfileType.DataSource = objectGenericPreofileType;
        //            DropDownListProfileType.DataTextField = "ProfileName";
        //            DropDownListProfileType.DataValueField = "ProfileTypeID";
        //            DropDownListProfileType.DataBind();
        //            DropDownListProfileType.Items.Insert(0, new ListItem("--Select Profile--", "0"));
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        ex.Data.Add("Event", "BindDropDownListProfileType");
        //        ex.Data.Add("Page", "ProfileDetail.ascx");
        //        LogManager._stringObject = "ProfileDetail.ascx ---- Page_Load";
        //        LogManager.SetExceptionDetails(ex, LogManager._stringObject, null);
        //        LogManager.LogException(ex, LogManager.LoggingCategory.General, LogManager.LoggingLevel.Error);
        //    }
        //}

        /// <summary>
        /// Bind GridView in case of select the Profile Type item from dropdownlist.
        /// </summary>
        private void BindGridView(int selectedIndex, int[] validationIDs)
        {
            try
            {
                int noOfSkipRecords = 0, totalPages = 0, totalNoOfRecords = 0, noOfRecords = 0, currentPageNo = 0;
                int.TryParse(TextBoxNoOfRecordsPerPage.Text, out noOfRecords);
                int.TryParse(TextBoxPageNo.Text, out currentPageNo);
                noOfSkipRecords = RMC.BussinessService.BSCustomizedPaging.GetNoOfSkipRecords(noOfRecords, currentPageNo);

                if (selectedIndex > 0)
                {
                    if (validationIDs.Count() > 0)
                    {
                        GridViewCreateProfile.DataSource = null;
                        RMC.BussinessService.BSCategoryProfiles objectBSCategoryProfiles = new RMC.BussinessService.BSCategoryProfiles();

                        totalNoOfRecords = objectBSCategoryProfiles.CountCategoryProfileByUserID(CommonClass.UserInformation.UserID, SelectedProfileValue, validationIDs);
                        totalPages = RMC.BussinessService.BSCustomizedPaging.GetNoOfPages(noOfRecords, totalNoOfRecords);
                        if (totalPages < currentPageNo)
                        {
                            TextBoxPageNo.Text = Convert.ToString(totalPages);
                            currentPageNo = totalPages;
                            noOfSkipRecords = RMC.BussinessService.BSCustomizedPaging.GetNoOfSkipRecords(noOfRecords, currentPageNo);
                        }

                        if (totalNoOfRecords < noOfRecords)
                        {
                            TextBoxPageNo.Text = Convert.ToString(totalNoOfRecords);
                            noOfRecords = totalNoOfRecords;
                            noOfSkipRecords = RMC.BussinessService.BSCustomizedPaging.GetNoOfSkipRecords(noOfRecords, currentPageNo);
                        }

                        GridViewCreateProfile.DataSource = objectBSCategoryProfiles.GetCategoryProfileByUserID(CommonClass.UserInformation.UserID, SelectedProfileValue, validationIDs, noOfSkipRecords, noOfRecords);
                    }
                    else
                    {
                        RMC.BussinessService.BSCategoryProfiles objectBSCategoryProfiles = new RMC.BussinessService.BSCategoryProfiles();

                        totalNoOfRecords = objectBSCategoryProfiles.CountCategoryProfileByUserID(CommonClass.UserInformation.UserID, SelectedProfileValue);
                        totalPages = RMC.BussinessService.BSCustomizedPaging.GetNoOfPages(noOfRecords, totalNoOfRecords);
                        if (totalPages < currentPageNo)
                        {
                            TextBoxPageNo.Text = Convert.ToString(totalPages);
                            currentPageNo = totalPages;
                            noOfSkipRecords = RMC.BussinessService.BSCustomizedPaging.GetNoOfSkipRecords(noOfRecords, currentPageNo);
                        }

                        if (totalNoOfRecords < noOfRecords)
                        {
                            TextBoxPageNo.Text = Convert.ToString(totalNoOfRecords);
                            //noOfRecords = totalNoOfRecords;
                            noOfSkipRecords = RMC.BussinessService.BSCustomizedPaging.GetNoOfSkipRecords(noOfRecords, currentPageNo);
                        }

                        //GridViewCreateProfile.DataSource = objectBSCategoryProfiles.GetCategoryProfileByUserID(CommonClass.UserInformation.UserID, SelectedProfileValue, noOfSkipRecords, noOfRecords);
                        GridViewCreateProfile.DataSource = objectBSCategoryProfiles.GetCategoryProfileByUserID(CommonClass.UserInformation.UserID, selectedIndex, 0, noOfRecords);
                    }
                }
                else
                {
                    if (validationIDs.Count() > 0)
                    {
                        RMC.BussinessService.BSValidationData objectBSValidationData = new RMC.BussinessService.BSValidationData();

                        totalNoOfRecords = objectBSValidationData.CountValidationData(validationIDs);
                        totalPages = RMC.BussinessService.BSCustomizedPaging.GetNoOfPages(noOfRecords, totalNoOfRecords);
                        if (totalPages < currentPageNo)
                        {
                            TextBoxPageNo.Text = Convert.ToString(totalPages);
                            currentPageNo = totalPages;
                            noOfSkipRecords = RMC.BussinessService.BSCustomizedPaging.GetNoOfSkipRecords(noOfRecords, currentPageNo);
                        }

                        if (totalNoOfRecords < noOfRecords)
                        {
                            TextBoxPageNo.Text = Convert.ToString(totalNoOfRecords);
                            noOfRecords = totalNoOfRecords;
                            noOfSkipRecords = RMC.BussinessService.BSCustomizedPaging.GetNoOfSkipRecords(noOfRecords, currentPageNo);
                        }

                        GridViewCreateProfile.DataSource = objectBSValidationData.GetValidationData(validationIDs, noOfSkipRecords, noOfRecords);
                    }
                    else
                    {
                        RMC.BussinessService.BSValidationData objectBSValidationData = new RMC.BussinessService.BSValidationData();

                        totalNoOfRecords = objectBSValidationData.CountValidationData();
                        totalPages = RMC.BussinessService.BSCustomizedPaging.GetNoOfPages(noOfRecords, totalNoOfRecords);
                        if (totalPages < currentPageNo)
                        {
                            TextBoxPageNo.Text = Convert.ToString(totalPages);
                            currentPageNo = totalPages;
                            noOfSkipRecords = RMC.BussinessService.BSCustomizedPaging.GetNoOfSkipRecords(noOfRecords, currentPageNo);
                        }

                        if (totalNoOfRecords < noOfRecords)
                        {
                            TextBoxPageNo.Text = Convert.ToString(totalNoOfRecords);
                            noOfRecords = totalNoOfRecords;
                            noOfSkipRecords = RMC.BussinessService.BSCustomizedPaging.GetNoOfSkipRecords(noOfRecords, currentPageNo);
                        }

                        GridViewCreateProfile.DataSource = objectBSValidationData.GetValidationData(noOfSkipRecords, noOfRecords);
                    }
                }

                LabelCurrentPageNo.Text = TextBoxPageNo.Text;
                LabelTotalNoOfRecords.Text = Convert.ToString(totalNoOfRecords);
                LabelTotalPages.Text = Convert.ToString(totalPages);
                GridViewCreateProfile.DataBind();
            }
            catch (Exception ex)
            {
                ex.Data.Add("Event", "ObjectDataSourceRequestList_Selecting");
                ex.Data.Add("Page", "ProfileDetail.ascx");
                LogManager._stringObject = "ProfileDetail.ascx ---- ObjectDataSourceRequestList_Selecting";
                LogManager.SetExceptionDetails(ex, LogManager._stringObject, null);
                LogManager.LogException(ex, LogManager.LoggingCategory.General, LogManager.LoggingLevel.Error);
            }
        }

        private void Reset()
        {
            try
            {
                TextBoxProfileType.Text = string.Empty;
                TextBoxDecription.Text = string.Empty;
                checkBoxShare.Checked = false;
                TextBoxPageNo.Text = "1";
                TextBoxNoOfRecordsPerPage.Text = Convert.ToString(maxRecords);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region "Events"

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                int totalPages = 0, totalNoOfRecords = 0, noOfRecords = 0;
                RMC.BussinessService.BSValidationData objectBSValidationData = new RMC.BussinessService.BSValidationData();

                if (!Page.IsPostBack)
                {
                    //Customize Paging Setting                     
                    TextBoxPageNo.Text = "1";
                    LabelCurrentPageNo.Text = TextBoxPageNo.Text;
                    //commented by MukulP. static page no.
                    //TextBoxNoOfRecordsPerPage.Text = Convert.ToString(maxRecords);

                    //int.TryParse(TextBoxNoOfRecordsPerPage.Text, out noOfRecords);
                    totalNoOfRecords = objectBSValidationData.CountValidationData();
                    TextBoxNoOfRecordsPerPage.Text = Convert.ToString(totalNoOfRecords);

                    int.TryParse(TextBoxNoOfRecordsPerPage.Text, out noOfRecords);
                    totalPages = RMC.BussinessService.BSCustomizedPaging.GetNoOfPages(noOfRecords, totalNoOfRecords);

                    LabelTotalNoOfRecords.Text = Convert.ToString(totalNoOfRecords);
                    LabelTotalPages.Text = Convert.ToString(totalPages);
                    TextBoxPageNo.Attributes.Add("onBlur", "ValidateNoOfPages();");
                    TextBoxNoOfRecordsPerPage.Attributes.Add("onBlur", "ValidateNoOfRecords();");
                    LinkButtonShow.OnClientClick = "return ValidatePaggingOnClick();";
                   // ButtonCopyProfileFromTemplate.OnClientClick = "return myPopup('" + PType.ToString() + "');";

                    int[] zeroValue = { };
                    BindGridView(SelectedProfileIndex, zeroValue);
                    if (Request.UrlReferrer != null)
                    {
                        ImageButtonBack.PostBackUrl = Request.UrlReferrer.AbsoluteUri;
                    }
                    //BindDropDownListProfileType();
                    BindDropDownListProfileType();
                }
                else
                {
                    if (SelectedProfileIndex > 0)
                    {
                        RefreshPageBasedOnProfileTemplate(SelectedProfileIndex);
                    }
                }
            }
            catch (Exception ex)
            {
                ex.Data.Add("Event", "Page_Load");
                ex.Data.Add("Page", "CreateNewProfile.ascx");
                LogManager._stringObject = "CreateNewProfile.ascx ---- Page_Load";
                LogManager.SetExceptionDetails(ex, LogManager._stringObject, null);
                LogManager.LogException(ex, LogManager.LoggingCategory.General, LogManager.LoggingLevel.Error);
                CommonClass.Show(LogManager.ShowErrorDetail(ex));
            }
        }

        protected void ButtonSave_Click(object sender, EventArgs e)
        {
            bool Flag = false;
            try
            {
                objectCategoryProfile = new RMC.BussinessService.BSCategoryProfiles();
                objectDSProfileType = new RMC.DataService.ProfileType();
                objectDSProfileUser = new RMC.DataService.ProfileUser();
                objectDSCategoryProfile = new RMC.DataService.CategoryProfile();

                // Insert records in ProfileType table.
                objectDSProfileType.ProfileName = TextBoxProfileType.Text;
                objectDSProfileType.Description = TextBoxDecription.Text;
                objectDSProfileType.AuthorName = CommonClass.UserInformation.FirstName + " " + CommonClass.UserInformation.LastName;
                if (Request.QueryString["valuetype"] == "0")
                {
                    objectDSProfileType.Type = "Value Added";
                }
                else if (Request.QueryString["valuetype"] == "1")
                {
                    objectDSProfileType.Type = "Others";
                }
                else if (Request.QueryString["valuetype"] == "3")
                {
                    objectDSProfileType.Type = "Activities";
                }
                else
                {
                    objectDSProfileType.Type = "Location";
                }
                objectDSProfileType.IsActive = true;

                // Insert records in ProfileUser table.
                if (SelectedProfileIndex > 0)
                {
                    objectDSProfileUser.ProfileTypeID = Convert.ToInt32(SelectedProfileIndex);
                }
                objectDSProfileUser.UserID = CommonClass.UserInformation.UserID;
                if (checkBoxShare.Checked == true)
                {
                    objectDSProfileUser.IsShared = true;
                }
                else
                {
                    objectDSProfileUser.IsShared = false;
                }
                objectDSProfileUser.CreatedDate = System.DateTime.Now;
                if (SelectedProfileIndex > 0)
                {
                    objectDSCategoryProfile.ProfileTypeID = Convert.ToInt32(SelectedProfileIndex);
                }

                //-------- ****** To Retrieve the records from GridView control.
                objectGenericCategoryProfile = new List<RMC.DataService.CategoryProfile>();

                foreach (GridViewRow grdRow in GridViewCreateProfile.Rows)
                {
                    objectDSCategoryProfile = new RMC.DataService.CategoryProfile();
                    Label LabelLocationID = (Label)grdRow.FindControl("LabelLocationID");
                    Label LabelActivityID = (Label)grdRow.FindControl("LabelActivityID");
                    Label LabelSubActivityID = (Label)grdRow.FindControl("LabelSubActivityID");
                    Label LabelValidationID = (Label)grdRow.FindControl("LabelValidation");
                    DropDownList drpSelectedCategory = (DropDownList)grdRow.FindControl("DropDownListCategoryAssignment");
                    objectDSCategoryProfile.ValidationID = Convert.ToInt32(LabelValidationID.Text);
                    objectDSCategoryProfile.CategoryAssignmentID = Convert.ToInt32(drpSelectedCategory.SelectedValue);
                    objectDSCategoryProfile.CategoryProfileName = objectDSProfileType.ProfileName;
                    objectDSCategoryProfile.CreatedBy = CommonClass.UserInformation.FirstName + " " + CommonClass.UserInformation.LastName;
                    objectDSCategoryProfile.CreatedDate = System.DateTime.Now;
                    objectDSCategoryProfile.IsDeleted = false;
                    objectGenericCategoryProfile.Add(objectDSCategoryProfile);
                }

                //--------------------------------------------------------------
                Flag = objectCategoryProfile.InsertRecordsCreateNewProfile(objectDSProfileType, objectDSProfileUser, objectGenericCategoryProfile);
                if (Flag == true)
                {
                    CommonClass.Show("New Profile Created Successfully!");
                    Reset();
                }
            }

            catch (Exception ex)
            {
                ex.Data.Add("Event", "ButtonSave_Click");
                ex.Data.Add("Page", "CreateNewProfile.ascx");
                LogManager._stringObject = "CreateNewProfile.ascx ---- ButtonSave_Click";
                LogManager.SetExceptionDetails(ex, LogManager._stringObject, null);
                LogManager.LogException(ex, LogManager.LoggingCategory.General, LogManager.LoggingLevel.Error);
                CommonClass.Show(LogManager.ShowErrorDetail(ex));
            }
        }

        protected void DropDownListProfileType_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                ViewState["ValidationIDs"] = null;
                TextBoxPageNo.Text = "1";
                TextBoxNoOfRecordsPerPage.Text = Convert.ToString(maxRecords);
                int[] zeroValue = { };
                BindGridView(DropDownListProfileType.SelectedIndex, zeroValue);
            }
            catch (Exception ex)
            {
                LogManager._stringObject = "CreateNewProfile.ascx ---- DropDownListProfileType_SelectedIndexChanged";
                LogManager.SetExceptionDetails(ex, LogManager._stringObject, null);
                LogManager.LogException(ex, LogManager.LoggingCategory.General, LogManager.LoggingLevel.Error);
                CommonClass.Show(LogManager.ShowErrorDetail(ex));
            }
        }

        protected void RefreshPageBasedOnProfileTemplate(int selectedProfileIndex)
        {
            try
            {
                ViewState["ValidationIDs"] = null;
                TextBoxPageNo.Text = "1";
                TextBoxNoOfRecordsPerPage.Text = Convert.ToString(maxRecords);
                int[] zeroValue = { };
                BindGridView(selectedProfileIndex, zeroValue);
            }
            catch (Exception ex)
            {
                LogManager._stringObject = "CreateNewProfile.ascx ---- DropDownListProfileType_SelectedIndexChanged";
                LogManager.SetExceptionDetails(ex, LogManager._stringObject, null);
                LogManager.LogException(ex, LogManager.LoggingCategory.General, LogManager.LoggingLevel.Error);
                CommonClass.Show(LogManager.ShowErrorDetail(ex));
            }
        }

        protected void LinkButtonDeleteSelected_Click(object sender, EventArgs e)
        {
            try
            {
                List<int> objectGenericValidationID = new List<int>();
                int[] validationIDs = { };

                if (ViewState["ValidationIDs"] != null)
                {
                    objectGenericValidationID.AddRange((int[])ViewState["ValidationIDs"]);
                }
                foreach (GridViewRow grdRow in GridViewCreateProfile.Rows)
                {
                    CheckBox chkBox = (CheckBox)grdRow.FindControl("CheckBoxDelete");

                    if (chkBox.Checked)
                    {
                        objectGenericValidationID.Add(Convert.ToInt32(GridViewCreateProfile.DataKeys[grdRow.RowIndex].Value));
                    }
                }

                if (objectGenericValidationID.Count > 0)
                {
                    validationIDs = objectGenericValidationID.ToArray();
                    ViewState["ValidationIDs"] = validationIDs;

                    LabelCurrentPageNo.Text = TextBoxPageNo.Text;
                    TextBoxNoOfRecordsPerPage.Text = Convert.ToString(maxRecords);

                    //Customize Paging Setting 
                    RMC.BussinessService.BSValidationData objectBSValidationData = new RMC.BussinessService.BSValidationData();
                    int totalPages = 0, totalNoOfRecords = 0, noOfRecords = 0, currentPage = 0;
                    int.TryParse(TextBoxNoOfRecordsPerPage.Text, out noOfRecords);
                    totalNoOfRecords = objectBSValidationData.CountValidationData() - validationIDs.Count();
                    totalPages = RMC.BussinessService.BSCustomizedPaging.GetNoOfPages(noOfRecords, totalNoOfRecords);

                    LabelTotalNoOfRecords.Text = Convert.ToString(totalNoOfRecords);
                    LabelTotalPages.Text = Convert.ToString(totalPages);
                    int.TryParse(TextBoxPageNo.Text, out currentPage);

                    if (currentPage > totalPages)
                    {
                        TextBoxPageNo.Text = Convert.ToString(totalPages);
                        LabelCurrentPageNo.Text = TextBoxPageNo.Text;
                    }
                }
                if (ViewState["ValidationIDs"] != null)
                {
                    BindGridView(SelectedProfileIndex, (int[])ViewState["ValidationIDs"]);
                }
                else
                {
                    int[] zeroValue = { };
                    BindGridView(SelectedProfileIndex, zeroValue);
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

                if (noOfRecordsPerPage <= totalNoOfRecords && noOfRecordsPerPage > 0 && currentPageNo <= totalPages && currentPageNo > 0)
                    BindGridView(SelectedProfileIndex, validationIDs);
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

        protected void ImageButtonFirst_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                int returnValue = 0;
                int[] validationIDs = { };

                if (ViewState["ValidationIDs"] != null)
                {
                    validationIDs = (int[])ViewState["ValidationIDs"];
                }

                returnValue = RMC.BussinessService.BSCustomizedPaging.GetFirstValue();
                TextBoxPageNo.Text = Convert.ToString(returnValue);
                BindGridView(SelectedProfileIndex, validationIDs);
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
                int[] validationIDs = { };

                if (ViewState["ValidationIDs"] != null)
                {
                    validationIDs = (int[])ViewState["ValidationIDs"];
                }
                int.TryParse(TextBoxPageNo.Text, out currentValue);

                returnValue = RMC.BussinessService.BSCustomizedPaging.GetBackwardStep(currentValue, 1);
                TextBoxPageNo.Text = Convert.ToString(returnValue);
                BindGridView(SelectedProfileIndex, validationIDs);
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
                int[] validationIDs = { };

                if (ViewState["ValidationIDs"] != null)
                {
                    validationIDs = (int[])ViewState["ValidationIDs"];
                }
                int.TryParse(TextBoxPageNo.Text, out currentValue);
                int.TryParse(LabelTotalPages.Text, out lastPageNo);

                returnValue = RMC.BussinessService.BSCustomizedPaging.GetForwardStep(currentValue, lastPageNo);
                TextBoxPageNo.Text = Convert.ToString(returnValue);
                BindGridView(SelectedProfileIndex, validationIDs);
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
                int[] validationIDs = { };

                if (ViewState["ValidationIDs"] != null)
                {
                    validationIDs = (int[])ViewState["ValidationIDs"];
                }
                int.TryParse(TextBoxPageNo.Text, out currentValue);
                int.TryParse(LabelTotalPages.Text, out lastPageNo);

                returnValue = RMC.BussinessService.BSCustomizedPaging.GetLastValue(lastPageNo);
                TextBoxPageNo.Text = Convert.ToString(returnValue);
                BindGridView(SelectedProfileIndex, validationIDs);
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
        private void BindDropDownListProfileType()
        {
            try
            {
                objectBSProfileType = new RMC.BussinessService.BSProfileType();
                objectGenericPreofileType = objectBSProfileType.GetProfileInformation(PType);
                if (objectGenericPreofileType.Count > 0)
                {
                    DropDownListProfileType.DataSource = objectGenericPreofileType;
                    DropDownListProfileType.DataTextField = "ProfileName";
                    DropDownListProfileType.DataValueField = "ProfileTypeID";
                    DropDownListProfileType.DataBind();
                    DropDownListProfileType.Items.Insert(0, new ListItem("--Select Profile--", "0"));
                }
            }
            catch (Exception ex)
            {
                ex.Data.Add("Event", "BindDropDownListProfileType");
                ex.Data.Add("Page", "ProfileDetail.ascx");
                LogManager._stringObject = "ProfileDetail.ascx ---- Page_Load";
                LogManager.SetExceptionDetails(ex, LogManager._stringObject, null);
                LogManager.LogException(ex, LogManager.LoggingCategory.General, LogManager.LoggingLevel.Error);
            }
        }

        protected void ButtonCopyProfileFromTemplate_Click(object sender, EventArgs e)
        {
            try
            {
                ViewState["ValidationIDs"] = null;
                TextBoxPageNo.Text = "1";
                TextBoxNoOfRecordsPerPage.Text = Convert.ToString(maxRecords);
                int[] zeroValue = { };
                //BindGridView(DropDownListProfileType.SelectedValue, zeroValue);
                BindGridView(Convert.ToInt32(DropDownListProfileType.SelectedValue) ,zeroValue);
            }
            catch (Exception ex)
            {
                LogManager._stringObject = "CreateNewProfile.ascx ---- DropDownListProfileType_SelectedIndexChanged";
                LogManager.SetExceptionDetails(ex, LogManager._stringObject, null);
                LogManager.LogException(ex, LogManager.LoggingCategory.General, LogManager.LoggingLevel.Error);
                CommonClass.Show(LogManager.ShowErrorDetail(ex));
            }
        }
        //protected void DropDownListProfileType_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    Session["SelectedProfileIndex"] = DropDownListProfileType.SelectedIndex;
        //    Session["SelectedProfileValue"] = DropDownListProfileType.SelectedValue;
        //}
        #endregion

    }
}