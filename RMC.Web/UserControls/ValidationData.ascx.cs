using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Text;
using System.IO;
using LogExceptions;


namespace RMC.Web.UserControls
{
    public partial class ValidationData : System.Web.UI.UserControl
    {

        #region Variables

        //Bussiness Service Object.
        RMC.BussinessService.BSValidationData _objectBSValidationData = null;

        //Bussiness Entity Object.
        RMC.BusinessEntities.BECategoryProfile _objectBECategoryProfile = null;

        #endregion

        #region Events

        protected void GridViewValidation_PreRender(object sender, EventArgs e)
        {
            try
            {
                if (ViewState["RowIndex"] != null && ViewState["Mode"] != null)
                {
                    GridViewRow grdRow = GridViewValidation.Rows[Convert.ToInt32(ViewState["RowIndex"])];

                    DropDownList ddlLocation = (DropDownList)grdRow.FindControl("DropDownListLocation");
                    DropDownList ddlActivity = (DropDownList)grdRow.FindControl("DropDownListActivity");
                    DropDownList ddlSubActivity = (DropDownList)grdRow.FindControl("DropDownListSubActivity");
                    System.Web.UI.WebControls.TextBox txtLocation = (System.Web.UI.WebControls.TextBox)grdRow.FindControl("TextBoxLocation");
                    System.Web.UI.WebControls.TextBox txtActivity = (System.Web.UI.WebControls.TextBox)grdRow.FindControl("TextBoxActivity");
                    System.Web.UI.WebControls.TextBox txtSubActivity = (System.Web.UI.WebControls.TextBox)grdRow.FindControl("TextBoxSubActivity");
                    LinkButton lnkUpdateEdit = (LinkButton)grdRow.FindControl("LinkButtonEditUpdate");
                    LinkButton lnkUpdate = (LinkButton)grdRow.FindControl("LinkButtonUpdate");

                    if (Convert.ToString(ViewState["Mode"]) == "Edit")
                    {
                        ddlActivity.Visible = false;
                        ddlLocation.Visible = false;
                        ddlSubActivity.Visible = false;
                        txtActivity.Visible = true;
                        txtLocation.Visible = true;
                        txtSubActivity.Visible = true;
                        lnkUpdateEdit.Visible = true;
                        lnkUpdate.Visible = false;
                    }
                    else if (Convert.ToString(ViewState["Mode"]) == "Change")
                    {
                        ddlActivity.Visible = true;
                        ddlLocation.Visible = true;
                        ddlSubActivity.Visible = true;
                        txtActivity.Visible = false;
                        txtLocation.Visible = false;
                        txtSubActivity.Visible = false;
                        lnkUpdateEdit.Visible = false;
                        lnkUpdate.Visible = true;
                    }
                    ViewState.Remove("RowIndex");
                    ViewState.Remove("Mode");
                }
            }
            catch (Exception ex)
            {
                ex.Data.Add("Events", "GridViewValidation_PreRender");
                ex.Data.Add("Page", "ValidationData.ascx");
                LogManager._stringObject = "ValidationData.ascx ---- GridViewValidation_PreRender";
                LogManager.SetExceptionDetails(ex, LogManager._stringObject, null);
                LogManager.LogException(ex, LogManager.LoggingCategory.General, LogManager.LoggingLevel.Error);
                CommonClass.Show(LogManager.ShowErrorDetail(ex));
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            TextBoxLocation.Text = String.Empty;
            TextBoxActivity.Text = String.Empty;
            TextBoxSubActivity.Text = String.Empty;
        }

        protected void LinkButtonCancel_Click(object sender, EventArgs e)
        {
            try
            {
                GridViewValidation.EditIndex = -1;
            }
            catch (Exception ex)
            {
                ex.Data.Add("Events", "LinkButtonCancel_Click");
                ex.Data.Add("Page", "ValidationData.ascx");
                LogManager._stringObject = "ValidationData.ascx ---- LinkButtonCancel_Click";
                LogManager.SetExceptionDetails(ex, LogManager._stringObject, null);
                LogManager.LogException(ex, LogManager.LoggingCategory.General, LogManager.LoggingLevel.Error);
                CommonClass.Show(LogManager.ShowErrorDetail(ex));
            }
        }

        protected void LinkButtonDelete_Click(object sender, EventArgs e)
        {
            try
            {
                _objectBSValidationData = new RMC.BussinessService.BSValidationData();

                GridViewRow grdRow = (GridViewRow)((LinkButton)sender).NamingContainer;
                int validationID = Convert.ToInt32(GridViewValidation.DataKeys[grdRow.RowIndex].Value);

                _objectBSValidationData.DeleteValidationData(validationID);
                GridViewValidation.DataBind();
            }
            catch (Exception ex)
            {
                ex.Data.Add("Events", "LinkButtonDelete_Click");
                ex.Data.Add("Page", "ValidationData.ascx");
                LogManager._stringObject = "ValidationData.ascx ---- LinkButtonDelete_Click";
                LogManager.SetExceptionDetails(ex, LogManager._stringObject, null);
                LogManager.LogException(ex, LogManager.LoggingCategory.General, LogManager.LoggingLevel.Error);
                CommonClass.Show(LogManager.ShowErrorDetail(ex));
            }
        }

        protected void LinkButtonEdit_Click(object sender, EventArgs e)
        {
            try
            {
                GridViewRow grdRow = (GridViewRow)((LinkButton)sender).NamingContainer;
                GridViewValidation.EditIndex = grdRow.RowIndex;
                ViewState["RowIndex"] = grdRow.RowIndex;
                ViewState["Mode"] = "Edit";
            }
            catch (Exception ex)
            {
                ex.Data.Add("Events", "LinkButtonEdit_Click");
                ex.Data.Add("Page", "ValidationData.ascx");
                LogManager._stringObject = "ValidationData.ascx ---- LinkButtonEdit_Click";
                LogManager.SetExceptionDetails(ex, LogManager._stringObject, null);
                LogManager.LogException(ex, LogManager.LoggingCategory.General, LogManager.LoggingLevel.Error);
                CommonClass.Show(LogManager.ShowErrorDetail(ex));
            }
        }

        protected void LinkButtonInsert_Click(object sender, EventArgs e)
        {
            try
            {
                SaveValidationData();
                GridViewValidation.DataBind();
            }
            catch (Exception ex)
            {
                ex.Data.Add("Events", "Page_Load");
                ex.Data.Add("Page", "ValidationData.ascx");
                LogManager._stringObject = "ValidationData.ascx ---- Page_Load";
                LogManager.SetExceptionDetails(ex, LogManager._stringObject, null);
                LogManager.LogException(ex, LogManager.LoggingCategory.General, LogManager.LoggingLevel.Error);
                CommonClass.Show(LogManager.ShowErrorDetail(ex));
            }
        }

        protected void LinkButtonUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                int validationID, activityID, locationID, subActivityID;
                RMC.DataService.Validation objectValidation = new RMC.DataService.Validation();
                _objectBSValidationData = new RMC.BussinessService.BSValidationData();

                GridViewRow grdRow = (GridViewRow)((LinkButton)sender).NamingContainer;
                validationID = Convert.ToInt32(GridViewValidation.DataKeys[grdRow.RowIndex].Value);
                activityID = Convert.ToInt32(((DropDownList)GridViewValidation.Rows[grdRow.RowIndex].FindControl("DropDownListActivity")).SelectedValue);
                locationID = Convert.ToInt32(((DropDownList)GridViewValidation.Rows[grdRow.RowIndex].FindControl("DropDownListLocation")).SelectedValue);
                subActivityID = Convert.ToInt32(((DropDownList)GridViewValidation.Rows[grdRow.RowIndex].FindControl("DropDownListSubActivity")).SelectedValue);

                objectValidation.ActivityID = activityID;
                objectValidation.CreatedBy = CommonClass.UserInformation.FirstName + " " + CommonClass.UserInformation.LastName;
                objectValidation.CreatedDate = DateTime.Now;
                objectValidation.LocationID = locationID;
                objectValidation.SubActivityID = subActivityID;
                objectValidation.ValidationID = validationID;

                _objectBSValidationData.UpdateValidationData(objectValidation);
                GridViewValidation.EditIndex = -1;
                GridViewValidation.DataBind();
            }
            catch (Exception ex)
            {
                ex.Data.Add("Events", "LinkButtonUpdate_Click");
                ex.Data.Add("Page", "ValidationData.ascx");
                LogManager._stringObject = "ValidationData.ascx ---- LinkButtonUpdate_Click";
                LogManager.SetExceptionDetails(ex, LogManager._stringObject, null);
                LogManager.LogException(ex, LogManager.LoggingCategory.General, LogManager.LoggingLevel.Error);
                CommonClass.Show(LogManager.ShowErrorDetail(ex));
            }
        }

        protected void LinkButtonChange_Click(object sender, EventArgs e)
        {
            try
            {
                GridViewRow grdRow = (GridViewRow)((LinkButton)sender).NamingContainer;
                GridViewValidation.EditIndex = grdRow.RowIndex;
                ViewState["RowIndex"] = grdRow.RowIndex;
                ViewState["Mode"] = "Change";
            }
            catch (Exception ex)
            {
                ex.Data.Add("Events", "LinkButtonChange_Click");
                ex.Data.Add("Page", "ValidationData.ascx");
                LogManager._stringObject = "ValidationData.ascx ---- LinkButtonChange_Click";
                LogManager.SetExceptionDetails(ex, LogManager._stringObject, null);
                LogManager.LogException(ex, LogManager.LoggingCategory.General, LogManager.LoggingLevel.Error);
                CommonClass.Show(LogManager.ShowErrorDetail(ex));
            }
        }

        protected void LinkButtonEditUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                int validationID, activityID, locationID, subActivityID;
                string location, activity, subactivity;
                RMC.DataService.Location objectLocation = new RMC.DataService.Location();
                RMC.DataService.Activity objectAcitivity = new RMC.DataService.Activity();
                RMC.DataService.SubActivity objectSubActivity = new RMC.DataService.SubActivity();
                _objectBSValidationData = new RMC.BussinessService.BSValidationData();

                GridViewRow grdRow = (GridViewRow)((LinkButton)sender).NamingContainer;
                validationID = Convert.ToInt32(GridViewValidation.DataKeys[grdRow.RowIndex].Value);
                locationID = Convert.ToInt32(((Literal)GridViewValidation.Rows[grdRow.RowIndex].FindControl("LiteralLocationID")).Text);
                activityID = Convert.ToInt32(((Literal)GridViewValidation.Rows[grdRow.RowIndex].FindControl("LiteralActivityID")).Text);
                subActivityID = Convert.ToInt32(((Literal)GridViewValidation.Rows[grdRow.RowIndex].FindControl("LiteralSubActivityID")).Text);
                location = ((System.Web.UI.WebControls.TextBox)GridViewValidation.Rows[grdRow.RowIndex].FindControl("TextBoxLocation")).Text;
                activity = ((System.Web.UI.WebControls.TextBox)GridViewValidation.Rows[grdRow.RowIndex].FindControl("TextBoxActivity")).Text;
                subactivity = ((System.Web.UI.WebControls.TextBox)GridViewValidation.Rows[grdRow.RowIndex].FindControl("TextBoxSubActivity")).Text;

                objectAcitivity.ActivityID = activityID;
                objectAcitivity.Activity1 = activity;
                objectLocation.LocationID = locationID;
                objectLocation.Location1 = location;
                objectSubActivity.SubActivityID = subActivityID;
                objectSubActivity.SubActivity1 = subactivity;


                _objectBSValidationData.UpdateEditValidationData(objectLocation, objectAcitivity, objectSubActivity);
                GridViewValidation.EditIndex = -1;
                GridViewValidation.DataBind();
            }
            catch (Exception ex)
            {
                ex.Data.Add("Events", "LinkButtonEditUpdate_Click");
                ex.Data.Add("Page", "ValidationData.ascx");
                LogManager._stringObject = "ValidationData.ascx ---- LinkButtonEditUpdate_Click";
                LogManager.SetExceptionDetails(ex, LogManager._stringObject, null);
                LogManager.LogException(ex, LogManager.LoggingCategory.General, LogManager.LoggingLevel.Error);
                CommonClass.Show(LogManager.ShowErrorDetail(ex));
            }
        }

        protected void ButtonExportExcelSheet_Click(object sender, EventArgs e)
        {
            try
            {
                Response.Redirect("~/Administrator/ExportExcelFile.aspx", false);                
            }
            catch (Exception ex)
            {
                ex.Data.Add("Events", "LinkButtonExportExcelSheet_Click");
                ex.Data.Add("Page", "ValidationData.ascx");
                LogManager._stringObject = "ValidationData.ascx ---- LinkButtonExportExcelSheet_Click";
                LogManager.SetExceptionDetails(ex, LogManager._stringObject, null);
                LogManager.LogException(ex, LogManager.LoggingCategory.General, LogManager.LoggingLevel.Error);
                CommonClass.Show(LogManager.ShowErrorDetail(ex));
            }
        }

        protected void ButtonDeleteAll_Click(object sender, EventArgs e)
        {
            try
            {
                _objectBSValidationData = new RMC.BussinessService.BSValidationData();
                _objectBSValidationData.DeleteValidationData();
                GridViewValidation.DataBind();
            }
            catch (Exception ex)
            {
                ex.Data.Add("Events", "LinkButtonDeleteAll_Click");
                ex.Data.Add("Page", "ValidationData.ascx");
                LogManager._stringObject = "ValidationData.ascx ---- LinkButtonDeleteAll_Click";
                LogManager.SetExceptionDetails(ex, LogManager._stringObject, null);
                LogManager.LogException(ex, LogManager.LoggingCategory.General, LogManager.LoggingLevel.Error);
                CommonClass.Show(LogManager.ShowErrorDetail(ex));
            }
        }

        protected void ButtonDeleteSelected_Click(object sender, EventArgs e)
        {
            try
            {
                List<int> objectGenericValidationIDs = new List<int>();
                RMC.BussinessService.BSValidationData objectBSValidationData = new RMC.BussinessService.BSValidationData();

                foreach (GridViewRow grdRow in GridViewValidation.Rows)
                {
                    CheckBox chkBox = (CheckBox)grdRow.FindControl("CheckBoxDelete");

                    if (chkBox.Checked)
                    {
                        objectGenericValidationIDs.Add(Convert.ToInt32(GridViewValidation.DataKeys[grdRow.RowIndex].Value));
                    }
                }

                if (objectGenericValidationIDs.Count > 0)
                {
                    //int totalPages = 0, totalNoOfRecords = 0, noOfRecords = 0, currentPage = 0;
                    foreach (int validationID in objectGenericValidationIDs)
                    {
                        objectBSValidationData.DeleteValidationData(validationID);
                    }

                    //int.TryParse(TextBoxNoOfRecordsPerPage.Text, out noOfRecords);
                    //totalNoOfRecords = objectBSCategoryProfiles.CountCategoryProfileByUserID(CommonClass.UserInformation.UserID, ProfileTypeID);
                    //totalPages = RMC.BussinessService.BSCustomizedPaging.GetNoOfPages(noOfRecords, totalNoOfRecords);
                    //int.TryParse(TextBoxPageNo.Text, out currentPage);
                    //if (totalPages < currentPage)
                    //{
                    //    TextBoxPageNo.Text = Convert.ToString(totalPages);
                    //}

                    //LabelCurrentPageNo.Text = TextBoxPageNo.Text;
                    //LabelTotalNoOfRecords.Text = Convert.ToString(totalNoOfRecords);
                    //LabelTotalPages.Text = Convert.ToString(totalPages);

                    GridViewValidation.DataBind();
                }
            }
            catch (Exception ex)
            {
                LogManager._stringObject = "ValidationData.ascx ---- LinkButtonDeleteSelected_Click";
                LogManager.SetExceptionDetails(ex, LogManager._stringObject, null);
                LogManager.LogException(ex, LogManager.LoggingCategory.General, LogManager.LoggingLevel.Error);
                CommonClass.Show(LogManager.ShowErrorDetail(ex));
            }
        }
        #endregion

        #region Private Method

        private void SaveValidationData()
        {
            try
            {
                _objectBSValidationData = new RMC.BussinessService.BSValidationData();
                _objectBECategoryProfile = new RMC.BusinessEntities.BECategoryProfile();

                //System.Web.UI.WebControls.TextBox TextBoxLoc = (System.Web.UI.WebControls.TextBox)GridViewValidation.HeaderRow.FindControl("TextBoxLocation");
                //System.Web.UI.WebControls.TextBox TextBoxAct = (System.Web.UI.WebControls.TextBox)GridViewValidation.HeaderRow.FindControl("TextBoxActivity");
                //System.Web.UI.WebControls.TextBox TextBoxSubAct = (System.Web.UI.WebControls.TextBox)GridViewValidation.HeaderRow.FindControl("TextBoxSubActivity");

                System.Web.UI.WebControls.TextBox TextBoxLoc = TextBoxLocation;
                System.Web.UI.WebControls.TextBox TextBoxAct = TextBoxActivity;
                System.Web.UI.WebControls.TextBox TextBoxSubAct = TextBoxSubActivity;

                if (TextBoxLoc.Text != string.Empty)
                {
                    _objectBECategoryProfile.Activity = TextBoxAct.Text;
                    _objectBECategoryProfile.Location = TextBoxLoc.Text;
                    _objectBECategoryProfile.SubActivity = TextBoxSubAct.Text;
                    _objectBECategoryProfile.CreatedBy = CommonClass.UserInformation.FirstName + " " + CommonClass.UserInformation.LastName;
                    _objectBECategoryProfile.CreatedDate = DateTime.Now;

                    _objectBSValidationData.InsertValidationData(_objectBECategoryProfile);
                }
                else
                {
                    CommonClass.Show("Must enter the Location.");
                }
            }
            catch (Exception ex)
            {
                ex.Data.Add("Function", "SaveValidationData");
                ex.Data.Add("Class", "ValidationData");
                throw ex;
            }
        }
                
        #endregion
        protected void GridViewValidation_RowCommand(object sender, GridViewCommandEventArgs e)
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

                GridViewValidation.DataBind();
            }
        }
        protected void ObjectDataSourceValidationData_Selecting(object sender, ObjectDataSourceSelectingEventArgs e)
        {
            try
            {
                string sortExpression = " "; string sortOrder = " ";
                
                if (this.ViewState["SortExp"] != null)
                    sortExpression = this.ViewState["SortExp"].ToString();
                if (this.ViewState["SortOrder"] != null)
                    sortOrder = this.ViewState["SortOrder"].ToString();
                
                e.InputParameters[0] = sortExpression;
                e.InputParameters[1] = sortOrder;
            }
            catch (Exception ex)
            {
                ex.Data.Add("Event", "ObjectDataSourceValidationData_Selecting");
                ex.Data.Add("Page", "ValidationData.ascx");
                LogManager._stringObject = "ValidationData.ascx ---- ObjectDataSourceValidationData_Selecting";
                LogManager.SetExceptionDetails(ex, LogManager._stringObject, null);
                LogManager.LogException(ex, LogManager.LoggingCategory.General, LogManager.LoggingLevel.Error);
                CommonClass.Show(LogManager.ShowErrorDetail(ex));
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

            GridViewValidation.DataBind();
        }

    }
}