using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using LogExceptions;

namespace RMC.Web.UserControls
{
    public partial class ViewNonValid : System.Web.UI.UserControl
    {

        #region Variables

        //Bussiness Service Object.
        RMC.BussinessService.BSValidationData _objectBSValidationData = null;

        #endregion

        #region Events

        protected void ButtonActiveSave_Click(object sender, EventArgs e)
        {
            try
            {
                string requestQueryString = Request.Url.AbsoluteUri;
                RMC.BussinessService.BSSDAValidation objectBSSDAValidation = new RMC.BussinessService.BSSDAValidation();
                RMC.BusinessEntities.BEValidation objectBEValidation = new RMC.BusinessEntities.BEValidation();

                DropDownList dropDownListLastLocation = (DropDownList)FormViewNurseDetail.FindControl("DropDownListLastLocation");
                DropDownList DropDownListResourceRequirement = (DropDownList)FormViewNurseDetail.FindControl("DropDownListResourceRequirement");

                TextBox Location = (TextBox)FormViewNurseDetail.FindControl("TextBoxLocation");
                TextBox LastLocation = (TextBox)FormViewNurseDetail.FindControl("TextBoxLastLocation");
                TextBox Activity = (TextBox)FormViewNurseDetail.FindControl("TextBoxActivity");
                TextBox SubActivity = (TextBox)FormViewNurseDetail.FindControl("TextBoxSubActivity");
                TextBox ResourceRequirement = (TextBox)FormViewNurseDetail.FindControl("TextBoxResource");
                TextBox ActiveError1 = (TextBox)FormViewNurseDetail.FindControl("TextBoxActiveError1");
                TextBox ActiveError2 = (TextBox)FormViewNurseDetail.FindControl("TextBoxActiveError2");
                TextBox ActiveError3 = (TextBox)FormViewNurseDetail.FindControl("TextBoxActiveError3");
                TextBox ActiveError4 = (TextBox)FormViewNurseDetail.FindControl("TextBoxActiveError4");

                objectBEValidation.NurserDetailID = Convert.ToInt32(FormViewNurseDetail.DataKey["NurserDetailID"]);
                objectBEValidation.LocationName = Location.Text;
                objectBEValidation.ActivityName = Activity.Text;
                objectBEValidation.SubActivityName = SubActivity.Text;
                objectBEValidation.ActiveError1 = ActiveError1.Text;
                objectBEValidation.ActiveError2 = ActiveError2.Text;
                objectBEValidation.ActiveError3 = ActiveError3.Text;
                objectBEValidation.ActiveError4 = ActiveError4.Text;
                objectBEValidation.LastLocationID = Convert.ToInt32(dropDownListLastLocation.SelectedValue);
                objectBEValidation.ResourceRequirementID = Convert.ToInt32(DropDownListResourceRequirement.SelectedValue);
                objectBSSDAValidation.ValidateNonValidNursePDADetail(objectBEValidation, "text");
                Response.Redirect(requestQueryString);
            }
            catch (Exception ex)
            {
                LogManager._stringObject = "ViewNonValid.ascx ---- ButtonTextSave_Click";
                LogManager.SetExceptionDetails(ex, LogManager._stringObject, null);
                LogManager.LogException(ex, LogManager.LoggingCategory.General, LogManager.LoggingLevel.Error);
                CommonClass.Show(LogManager.ShowErrorDetail(ex));
            }
        }

        protected void ButtonDelete_Click(object sender, EventArgs e)
        {
            try
            {
                bool flag = false;
                RMC.BussinessService.BSNursePDADetail objectBSNursePDADetail = new RMC.BussinessService.BSNursePDADetail();

                foreach (GridViewRow grdRow in GridViewViewNonValidData.Rows)
                {
                    CheckBox chkBox = (CheckBox)grdRow.FindControl("CheckBoxRow");
                    if (chkBox.Checked)
                    {
                        flag = objectBSNursePDADetail.DeleteNursePDADetail(Convert.ToInt32(GridViewViewNonValidData.DataKeys[grdRow.RowIndex].Value));
                    }
                }
                GridViewViewNonValidData.DataBind();
                if (GridViewViewNonValidData.Rows.Count == 0)
                {
                    ButtonDelete.Visible = false;
                }
            }
            catch (Exception ex)
            {
                ex.Data.Add("Events", "ButtonDelete_Click");
                ex.Data.Add("Page", "ViewNonValid.ascx");
                LogManager._stringObject = "ViewNonValid.ascx ---- ButtonDelete_Click";
                LogManager.SetExceptionDetails(ex, LogManager._stringObject, null);
                LogManager.LogException(ex, LogManager.LoggingCategory.General, LogManager.LoggingLevel.Error);
                CommonClass.Show(LogManager.ShowErrorDetail(ex));
            }
        }

        protected void ButtonSave_Click(object sender, EventArgs e)
        {
            try
            {
                string requestQueryString = Request.Url.AbsoluteUri;
                RMC.BussinessService.BSSDAValidation objectBSSDAValidation = new RMC.BussinessService.BSSDAValidation();
                RMC.BusinessEntities.BEValidation objectBEValidation = new RMC.BusinessEntities.BEValidation();
                               
                DropDownList dropDownListLastLocation = (DropDownList)FormViewNurseDetail.FindControl("DropDownListLastLocation");
                DropDownList dropDownListLocation = (DropDownList)FormViewNurseDetail.FindControl("DropDownListLocation");
                DropDownList dropDownListActivity = (DropDownList)FormViewNurseDetail.FindControl("DropDownListActivity");
                DropDownList dropDownListSubActivity = (DropDownList)FormViewNurseDetail.FindControl("DropDownListSubActivity");
                DropDownList DropDownListResourceRequirement = (DropDownList)FormViewNurseDetail.FindControl("DropDownListResourceRequirement");
                ListBox listBoxCognitiveCategory = (ListBox)FormViewNurseDetail.FindControl("ListBoxCognitiveCategory");

                objectBEValidation.NurserDetailID = Convert.ToInt32(FormViewNurseDetail.DataKey["NurserDetailID"]);
                objectBEValidation.CognitiveCategory = GenerateString(listBoxCognitiveCategory);
                objectBEValidation.LocationID = Convert.ToInt32(dropDownListLocation.SelectedValue);
                objectBEValidation.ActivityID = Convert.ToInt32(dropDownListActivity.SelectedValue);
                objectBEValidation.SubActivityID = Convert.ToInt32(dropDownListSubActivity.SelectedValue);
                objectBEValidation.LastLocationID = Convert.ToInt32(dropDownListLastLocation.SelectedValue);
                objectBEValidation.ResourceRequirementID = Convert.ToInt32(DropDownListResourceRequirement.SelectedValue);
                objectBSSDAValidation.ValidateNonValidNursePDADetail(objectBEValidation, "ids");

                Response.Redirect(requestQueryString);                
            }
            catch (Exception ex)
            {
                LogManager._stringObject = "ViewNonValid.ascx ---- ButtonSave_Click";
                LogManager.SetExceptionDetails(ex, LogManager._stringObject, null);
                LogManager.LogException(ex, LogManager.LoggingCategory.General, LogManager.LoggingLevel.Error);
                CommonClass.Show(LogManager.ShowErrorDetail(ex));
            }
        }

        protected void ButtonTextSave_Click(object sender, EventArgs e)
        {
            try
            {
                string requestQueryString = Request.Url.AbsoluteUri;
                RMC.BussinessService.BSSDAValidation objectBSSDAValidation = new RMC.BussinessService.BSSDAValidation();
                RMC.BusinessEntities.BEValidation objectBEValidation = new RMC.BusinessEntities.BEValidation();

                TextBox Location = (TextBox)FormViewNurseDetail.FindControl("TextBoxLocation");
                TextBox LastLocation = (TextBox)FormViewNurseDetail.FindControl("TextBoxLastLocation");
                TextBox Activity = (TextBox)FormViewNurseDetail.FindControl("TextBoxActivity");
                TextBox SubActivity = (TextBox)FormViewNurseDetail.FindControl("TextBoxSubActivity");
                TextBox ResourceRequirement = (TextBox)FormViewNurseDetail.FindControl("TextBoxResource");

                Literal literalLastLocationID = (Literal)FormViewNurseDetail.FindControl("LiteralLastLocationID");
                Literal literalResourceRequirementID = (Literal)FormViewNurseDetail.FindControl("LiteralResourceRequirementID");

                objectBEValidation.NurserDetailID = Convert.ToInt32(FormViewNurseDetail.DataKey["NurserDetailID"]);
                objectBEValidation.LocationName = Location.Text;
                objectBEValidation.ActivityName = Activity.Text;
                objectBEValidation.SubActivityName = SubActivity.Text;
                objectBEValidation.ActiveError1 = string.Empty;
                objectBEValidation.ActiveError2 = string.Empty;
                objectBEValidation.ActiveError3 = string.Empty;
                objectBEValidation.ActiveError4 = string.Empty;
                objectBEValidation.LastLocationName = LastLocation.Text;
                objectBEValidation.ResourceText = ResourceRequirement.Text;
                objectBEValidation.LastLocationID = Convert.ToInt32(literalLastLocationID.Text);
                objectBEValidation.ResourceRequirementID = Convert.ToInt32(literalResourceRequirementID.Text);
                objectBSSDAValidation.ValidateNonValidNursePDADetail(objectBEValidation, "edittext");

                Response.Redirect(requestQueryString);  
            }
            catch (Exception ex)
            {
                LogManager._stringObject = "ViewNonValid.ascx ---- ButtonTextSave_Click";
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
        protected void DropDownListLocation_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                _objectBSValidationData = new RMC.BussinessService.BSValidationData();
                //GridViewRow grdRow = (GridViewRow)((DropDownList)sender).NamingContainer;
                DropDownList ddlActivity = (DropDownList)FormViewNurseDetail.FindControl("DropDownListActivity");
                int locationID = Convert.ToInt32(((DropDownList)sender).SelectedValue);

                BindActivityDropDownList(ddlActivity, locationID);
            }
            catch (Exception ex)
            {
                ex.Data.Add("Events", "DropDownListLocation_SelectedIndexChanged");
                ex.Data.Add("Page", "ViewNonValid.ascx");
                LogManager._stringObject = "ViewNonValid.ascx ---- DropDownListLocation_SelectedIndexChanged ";
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
        protected void DropDownListActivity_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                _objectBSValidationData = new RMC.BussinessService.BSValidationData();
                //GridViewRow grdRow = (GridViewRow)((DropDownList)sender).NamingContainer;
                DropDownList ddlSubActivity = (DropDownList)FormViewNurseDetail.FindControl("DropDownListSubActivity");
                int activityID = Convert.ToInt32(((DropDownList)sender).SelectedValue);

                BindSubActivityDropDownList(ddlSubActivity, activityID);
            }
            catch (Exception ex)
            {
                ex.Data.Add("Events", "DropDownListActivity_SelectedIndexChanged");
                ex.Data.Add("Page", "ViewNonValid.ascx");
                LogManager._stringObject = "ViewNonValid.ascx ---- DropDownListActivity_SelectedIndexChanged ";
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
        protected void DropDownListLastLocation_PreRender(object sender, EventArgs e)
        {
            try
            {
                DropDownList dropDownListLastLocation = (DropDownList)sender;

                if (dropDownListLastLocation.Items.Count > 1)
                {
                    for (int index = 1; index < dropDownListLastLocation.Items.Count; index++)
                    {
                        //dropDownListLastLocation.Items[index].Text = dropDownListLastLocation.Items[index].Text.Substring(5);
                        dropDownListLastLocation.Items[index].Text = dropDownListLastLocation.Items[index].Text.Replace("Last ", "");
                    }
                }
            }
            catch (Exception ex)
            {
                LogManager._stringObject = "ViewNonValid.ascx ---- Page_Load ";
                LogManager.SetExceptionDetails(ex, LogManager._stringObject, null);
                LogManager.LogException(ex, LogManager.LoggingCategory.General, LogManager.LoggingLevel.Error);
                CommonClass.Show(LogManager.ShowErrorDetail(ex));
            }
        }

        protected void FormViewNurseDetail_DataBound(object sender, EventArgs e)
        {
            if (ViewState["Active"] != null)
            {
                if (FormViewNurseDetail.DefaultMode == FormViewMode.ReadOnly)
                {
                    int locationID = 0, activityID = 0, subActivityID = 0;
                    bool isErrorInLocation = false, isErrorInActivity = false, isErrorInSubActivity = false, isErrorExist = false, isActiveError = false;
                    string activity = string.Empty, subActivity = string.Empty;

                    activity = ((Literal)FormViewNurseDetail.FindControl("LiteralActivityName")).Text;
                    subActivity = ((Literal)FormViewNurseDetail.FindControl("LiteralSubActivityName")).Text; 
                    locationID = Convert.ToInt32(((Literal)FormViewNurseDetail.FindControl("LiteralLocationID")).Text);
                    activityID = Convert.ToInt32(((Literal)FormViewNurseDetail.FindControl("LiteralActivityID")).Text);
                    subActivityID = Convert.ToInt32(((Literal)FormViewNurseDetail.FindControl("LiteralSubActivityID")).Text);
                    isErrorInLocation = Convert.ToBoolean(((Literal)FormViewNurseDetail.FindControl("LiteralIsErrorInLocation")).Text);
                    isErrorInActivity = Convert.ToBoolean(((Literal)FormViewNurseDetail.FindControl("LiteralIsActiveError")).Text);
                    isErrorInSubActivity = Convert.ToBoolean(((Literal)FormViewNurseDetail.FindControl("LiteralIsErrorInSubActivity")).Text);
                    isErrorExist = Convert.ToBoolean(((Literal)FormViewNurseDetail.FindControl("LiteralIsErrorExist")).Text);
                    isActiveError = Convert.ToBoolean(((Literal)FormViewNurseDetail.FindControl("LiteralIsErrorInActivity")).Text);

                    DropDownList dropDownListLocation = (DropDownList)FormViewNurseDetail.FindControl("DropDownListLocation");
                    DropDownList dropDownListActivity = (DropDownList)FormViewNurseDetail.FindControl("DropDownListActivity");
                    DropDownList dropDownListSubActivity = (DropDownList)FormViewNurseDetail.FindControl("DropDownListSubActivity");

                    BindDropDownList(dropDownListLocation, "LocationName", "LocationID", locationID, activityID, subActivityID, isErrorInLocation, isErrorInActivity, isErrorInSubActivity, isErrorExist, isActiveError);
                    BindDropDownList(dropDownListActivity, "ActivityName", "ActivityID", locationID, activityID, subActivityID, isErrorInLocation, isErrorInActivity, isErrorInSubActivity, isErrorExist, isActiveError);
                    
                    if (activity.Length > 0 && dropDownListActivity.SelectedIndex == 0)
                    {
                        dropDownListActivity.Items[0].Text = activity;
                    }

                    BindDropDownList(dropDownListSubActivity, "SubActivityName", "SubActivityID", locationID, activityID, subActivityID, isErrorInLocation, isErrorInActivity, isErrorInSubActivity, isErrorExist, isActiveError);

                    if (subActivity.Length > 0)
                    {
                        dropDownListSubActivity.Items[0].Text = subActivity;
                    }

                    ViewState["Active"] = null;
                }
            }
        }

        protected void GridViewViewNonValidData_SelectedIndexChanged(object sender, EventArgs e)
        {
            ViewState["Active"] = "Active";
            bool isActiveError = false;

            isActiveError = Convert.ToBoolean(DataBinder.Eval(GridViewViewNonValidData.Rows[GridViewViewNonValidData.SelectedIndex].DataItem, "IsActiveError"));
            if (isActiveError)
            {
                FormViewNurseDetail.DefaultMode = FormViewMode.Edit;
            }
            else
            {
                FormViewNurseDetail.DefaultMode = FormViewMode.ReadOnly;
            }
        }

        protected void LinkButtonEdit_Click(object sender, EventArgs e)
        {
            try
            {
                GridViewRow grdRow = (GridViewRow)((LinkButton)sender).NamingContainer;
                GridViewViewNonValidData.EditIndex = grdRow.RowIndex;
            }
            catch (Exception ex)
            {
                ex.Data.Add("Events", "LinkButtonEdit_Click");
                ex.Data.Add("Page", "ViewNonValid.ascx");
                LogManager._stringObject = "ViewNonValid.ascx ---- LinkButtonEdit_Click";
                LogManager.SetExceptionDetails(ex, LogManager._stringObject, null);
                LogManager.LogException(ex, LogManager.LoggingCategory.General, LogManager.LoggingLevel.Error);
                CommonClass.Show(LogManager.ShowErrorDetail(ex));
            }
        }

        protected void LinkButtonDelete_Click(object sender, EventArgs e)
        {
            try
            {
                bool flag = false;
                RMC.BussinessService.BSNursePDADetail objectBSNursePDADetail = new RMC.BussinessService.BSNursePDADetail();
                GridViewRow grdRow = (GridViewRow)((LinkButton)sender).NamingContainer;

                flag = objectBSNursePDADetail.DeleteNursePDADetail(Convert.ToInt32(GridViewViewNonValidData.DataKeys[grdRow.RowIndex].Value));
                GridViewViewNonValidData.DataBind();
            }
            catch (Exception ex)
            {
                ex.Data.Add("Events", "LinkButtonDelete_Click");
                ex.Data.Add("Page", "ViewNonValid.ascx");
                LogManager._stringObject = "ViewNonValid.ascx ---- LinkButtonDelete_Click";
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
        protected void ListBoxCognitiveCategory_PreRender(object sender, EventArgs e)
        {
            try
            {
                List<string> objectGenericString = null;
                ListBox listBoxCognitiveCategory = (ListBox)sender;
                TextBox TextBoxCognitiveCategory = (TextBox)FormViewNurseDetail.FindControl("TextBoxCognitiveCategory");

                objectGenericString = GenerateStringList(TextBoxCognitiveCategory.Text);
                foreach (ListItem lstItem in listBoxCognitiveCategory.Items)
                {
                    if (objectGenericString.Exists(delegate(string cc)
                    {
                        return cc.ToLower().Trim() == lstItem.Text.ToLower().Trim();
                    }))
                    {
                        lstItem.Selected = true;
                    }
                }
            }
            catch (Exception ex)
            {
                LogManager._stringObject = "ViewNonValid.ascx ---- ListBoxCognitiveCategory_PreRender";
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
                if (!Page.IsPostBack)
                {
                    CommonClass objectCommonClass = new CommonClass();
                    GridViewViewNonValidData.DataBind();
                    if (GridViewViewNonValidData.Rows.Count == 0)
                        ButtonDelete.Visible = false;
                    string previousUrl, currentUrl;
                    int previousIndex = Request.UrlReferrer.AbsoluteUri.IndexOf("?");
                    if (previousIndex > 0)
                        previousUrl = Request.UrlReferrer.AbsoluteUri.Substring(0, previousIndex);
                    else
                        previousUrl = Request.UrlReferrer.AbsoluteUri.Substring(0);
                    int currentIndex = Request.Url.AbsoluteUri.IndexOf("?");
                    if (currentIndex > 0)
                        currentUrl = Request.Url.AbsoluteUri.Substring(0, currentIndex);
                    else
                        currentUrl = Request.Url.AbsoluteUri.Substring(0);
                    if (previousUrl != currentUrl)
                        objectCommonClass.BackButtonUrl = Request.UrlReferrer.AbsoluteUri;
                }
            }
            catch (Exception ex)
            {
                ex.Data.Add("Events", "Page_Load");
                ex.Data.Add("Page", "ViewNonValid.ascx");
                LogManager._stringObject = "ViewNonValid.ascx ---- Page_Load ";
                LogManager.SetExceptionDetails(ex, LogManager._stringObject, null);
                LogManager.LogException(ex, LogManager.LoggingCategory.General, LogManager.LoggingLevel.Error);
                CommonClass.Show(LogManager.ShowErrorDetail(ex));
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
                ex.Data.Add("Events", "ImageButtonBack_Click");
                ex.Data.Add("Page", "ViewNonValid.ascx");
                LogManager._stringObject = "ViewNonValid.ascx ---- ImageButtonBack_Click ";
                LogManager.SetExceptionDetails(ex, LogManager._stringObject, null);
                LogManager.LogException(ex, LogManager.LoggingCategory.General, LogManager.LoggingLevel.Error);
                CommonClass.Show(LogManager.ShowErrorDetail(ex));
            }
        }

        #endregion

        #region Private Methods

        private void BindDropDownList(DropDownList ddl, string textField, string valueField, int locationID, int activityID, int subActivityID, bool isErrorInLocation, bool isErrorInActivity, bool isErrorInSubActivity, bool isErrorExist, bool isActiveError)
        {
            try
            {
                _objectBSValidationData = new RMC.BussinessService.BSValidationData();

                ddl.DataTextField = textField;
                ddl.DataValueField = valueField;
                if (textField.ToLower().Trim() == "locationname")
                {
                    ddl.DataSource = _objectBSValidationData.GetLocationsFromValidationTable(locationID, activityID, subActivityID, isErrorInLocation, isErrorInActivity, isErrorInSubActivity, isErrorExist, isActiveError);
                }
                else if (textField.ToLower().Trim() == "activityname")
                {
                    ddl.DataSource = _objectBSValidationData.GetActivitiesFromValidationTable(locationID, activityID, subActivityID, isErrorInLocation, isErrorInActivity, isErrorInSubActivity, isErrorExist, isActiveError);
                }
                else
                {
                    DropDownList ddlActivity = (DropDownList)FormViewNurseDetail.FindControl("DropDownListActivity");
                    
                    if (ddlActivity.SelectedIndex > 0)
                    {
                        _objectBSValidationData = new RMC.BussinessService.BSValidationData();
                        DropDownList ddlSubActivity = (DropDownList)FormViewNurseDetail.FindControl("DropDownListSubActivity");

                        BindSubActivityDropDownList(ddlSubActivity, activityID);
                    }
                    else
                    {
                        ddl.DataSource = _objectBSValidationData.GetSubActivitiesFromValidationTable(locationID, activityID, subActivityID, isErrorInLocation, isErrorInActivity, isErrorInSubActivity, isErrorExist, isActiveError);
                    }
                }
                ddl.DataBind();

                for (int index = 0; index < ddl.Items.Count; index++)
                {
                    if (textField.ToLower().Trim() == "locationname")
                    {
                        if (ddl.Items[index].Value == locationID.ToString())
                        {
                            ddl.Items[index].Selected = true;
                            break;
                        }
                    }
                    else if (textField.ToLower().Trim() == "activityname")
                    {
                        if (ddl.Items[index].Value == activityID.ToString())
                        {
                            ddl.Items[index].Selected = true;
                            break;
                        }
                    }
                    else
                    {
                        if (ddl.Items[index].Value == subActivityID.ToString())
                        {
                            ddl.Items[index].Selected = true;
                            break;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ex.Data.Add("Function", "BindLocationDropDownList");
                ex.Data.Add("Class", "ViewNonValid");
                throw ex;
            }
        }

        private void BindActivityDropDownList(DropDownList DropDownListActivity, int locationID)
        {
            try
            {
                _objectBSValidationData = new RMC.BussinessService.BSValidationData();

                if (DropDownListActivity.Items.Count > 1)
                {
                    DropDownListActivity.Items.Clear();
                    DropDownListActivity.Items.Add("Select Activity");
                    DropDownListActivity.Items[0].Value = 0.ToString();
                }

                DropDownListActivity.DataTextField = "ActivityName";
                DropDownListActivity.DataValueField = "ActivityID";
                DropDownListActivity.DataSource = _objectBSValidationData.GetActivityFromValidationByLocationID(locationID);
                DropDownListActivity.DataBind();
            }
            catch (Exception ex)
            {
                ex.Data.Add("Function", "BindActivityDropDownList");
                ex.Data.Add("Class", "ViewNonValid");
                throw ex;
            }
        }

        private void BindSubActivityDropDownList(DropDownList DropDownListSubActivity, int activityID)
        {
            try
            {
                _objectBSValidationData = new RMC.BussinessService.BSValidationData();

                if (DropDownListSubActivity.Items.Count > 1)
                {
                    DropDownListSubActivity.Items.Clear();
                    DropDownListSubActivity.Items.Add("Select SubActivity");
                    DropDownListSubActivity.Items[0].Value = 0.ToString();
                }

                DropDownListSubActivity.DataTextField = "SubActivityName";
                DropDownListSubActivity.DataValueField = "SubActivityID";
                DropDownListSubActivity.DataSource = _objectBSValidationData.GetSubActivityFromValidationByActivityID(activityID);
                DropDownListSubActivity.DataBind();
            }
            catch (Exception ex)
            {
                ex.Data.Add("Function", "BindSubActivityDropDownList");
                ex.Data.Add("Class", "ViewNonValid");
                throw ex;
            }
        }

        private List<string> GenerateStringList(string cognitiveCategory)
        {
            List<string> lstCC = new List<string>();
            string[] cc = cognitiveCategory.Split(',');

            foreach (string congnitive in cc)
            {
                if (congnitive != string.Empty)
                {
                    lstCC.Add(congnitive);
                }
            }

            return lstCC;
        }

        private string GenerateString(ListBox lstBox)
        {
            string cc = string.Empty;
            foreach (ListItem lstItem in lstBox.Items)
            {
                if (lstItem.Selected)
                {
                    if (cc != string.Empty)
                    {
                        cc += ',' + lstItem.Text;
                    }
                    else
                    {
                        cc = lstItem.Text;
                    }
                }
            }

            return cc;
        }

        #endregion
        
    }
}