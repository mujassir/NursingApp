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
    public partial class ProfileType : System.Web.UI.UserControl
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!Page.IsPostBack)
                {
                    ImageButtonBack.PostBackUrl = Request.UrlReferrer.AbsoluteUri;
                    ButtonDecremental.Enabled = false;
                    ButtonDecremental.CssClass = "aspSquareButtonDisable";
                    ButtonIncremental.Enabled = false;
                    ButtonIncremental.CssClass = "aspSquareButtonDisable";
                }
            }
            catch (Exception ex)
            {
                ex.Data.Add("Events", "Page_Load");
                ex.Data.Add("Page", "ProfileType.aspx");
                LogManager._stringObject = "ProfileType.aspx ---- ButtonSave_Click";
                LogManager.SetExceptionDetails(ex, LogManager._stringObject, null);
                LogManager.LogException(ex, LogManager.LoggingCategory.General, LogManager.LoggingLevel.Error);
                CommonClass.Show(LogManager.ShowErrorDetail(ex));
            }

        }

        protected void ButtonSave_Click(object sender, EventArgs e)
        {
            try
            {
                bool flag = false;
                RMC.BussinessService.BSProfileType objectBSProfileType = new RMC.BussinessService.BSProfileType();
                RMC.DataService.ValueAddedType objectValueAddedType = new RMC.DataService.ValueAddedType();
                RMC.DataService.CategoryGroup objectCategoryGroup = new RMC.DataService.CategoryGroup();
                RMC.DataService.LocationCategory objectLocationCategory = new RMC.DataService.LocationCategory();
                RMC.DataService.ActivitiesCategory objectActivitiesCategory = new RMC.DataService.ActivitiesCategory();
                //RMC.BusinessEntities.BECategoryType objectBECategoryType = new RMC.BusinessEntities.BECategoryType();
                //objectBECategoryType.ProfileCategory = TextBoxProfileType.Text;
                string txtProfileTypeName = TextBoxProfileType.Text;
                if (ButtonIncremental.Enabled)
                {
                    txtProfileTypeName += " (-)";
                }
                else
                {
                    txtProfileTypeName += " (+)";
                }

                if (Request.QueryString["valuetype"] == "0")
                {
                    objectValueAddedType.TypeName = txtProfileTypeName;
                    objectValueAddedType.Abbreviation = TextBoxProfileType.Text.Substring(0, 1);
                    objectValueAddedType.IsActive = true;
                    flag = objectBSProfileType.InsertValueAddedType(objectValueAddedType);
                }
                else if (Request.QueryString["valuetype"] == "1")
                {
                    objectCategoryGroup.CategoryGroup1 = txtProfileTypeName;
                    objectCategoryGroup.IsActive = true;
                    flag = objectBSProfileType.InsertCategoryGroup(objectCategoryGroup);
                }
                else if (Request.QueryString["valuetype"] == "3")
                {
                    objectActivitiesCategory.ActivitiesCategory1 = txtProfileTypeName;
                    flag = objectBSProfileType.InsertActivitiesCategoryGroup(objectActivitiesCategory);
                }
                else
                {
                    objectLocationCategory.LocationCategory1 = txtProfileTypeName;
                    flag = objectBSProfileType.InsertLocationCategory(objectLocationCategory);
                }
                if (flag)
                {
                    CommonClass.Show("Profile Type Save Successfully");
                }
                else
                {
                    CommonClass.Show("Fail to Save Profile Type");
                }
                ButtonDecremental.Enabled = false;
                ButtonDecremental.CssClass = "aspSquareButtonDisable";
                ButtonIncremental.Enabled = false;
                ButtonIncremental.CssClass = "aspSquareButtonDisable";
                TextBoxProfileType.Text = string.Empty;
                ListBoxProfileTypes.DataBind();
            }
            catch (Exception ex)
            {
                ex.Data.Add("Events", "ButtonSave_Click");
                ex.Data.Add("Page", "ProfileType.aspx");
                LogManager._stringObject = "ProfileType.aspx ---- ButtonSave_Click";
                LogManager.SetExceptionDetails(ex, LogManager._stringObject, null);
                LogManager.LogException(ex, LogManager.LoggingCategory.General, LogManager.LoggingLevel.Error);
                CommonClass.Show(LogManager.ShowErrorDetail(ex));
            }
        }

        protected void ButtonUpdate_Click(object sender, EventArgs e)
        {
            bool flag = false;
            try
            {
                RMC.BussinessService.BSProfileType objectBSProfileType = new RMC.BussinessService.BSProfileType();
                RMC.DataService.ValueAddedType objectValueAddedType = new RMC.DataService.ValueAddedType();
                RMC.DataService.CategoryGroup objectCategoryGroup = new RMC.DataService.CategoryGroup();
                RMC.DataService.LocationCategory objectLocationCategory = new RMC.DataService.LocationCategory();
                RMC.DataService.ActivitiesCategory objectActivitiesCategory = new RMC.DataService.ActivitiesCategory();

                string txtProfileTypeName = TextBoxProfileType.Text;
                if (ButtonIncremental.Enabled)
                {
                    txtProfileTypeName += " (-)";
                }
                else
                {
                    txtProfileTypeName += " (+)";
                }

                if (Request.QueryString["valuetype"] == "0")
                {
                    objectValueAddedType.TypeID = Convert.ToInt32(ListBoxProfileTypes.SelectedValue);
                    objectValueAddedType.TypeName = txtProfileTypeName;
                    objectValueAddedType.Abbreviation = TextBoxProfileType.Text.Substring(0, 1);
                    objectValueAddedType.IsActive = true;
                    flag = objectBSProfileType.UpdateValueAddedType(objectValueAddedType);
                }
                else if (Request.QueryString["valuetype"] == "1")
                {
                    objectCategoryGroup.CategoryGroupID = Convert.ToInt32(ListBoxProfileTypes.SelectedValue);
                    objectCategoryGroup.CategoryGroup1 = txtProfileTypeName;
                    objectCategoryGroup.IsActive = true;
                    flag = objectBSProfileType.UpdateCategoryGroup(objectCategoryGroup);
                }
                else if (Request.QueryString["valuetype"] == "3")
                {
                    objectActivitiesCategory.ActivitiesID = Convert.ToInt32(ListBoxProfileTypes.SelectedValue);
                    objectActivitiesCategory.ActivitiesCategory1 = txtProfileTypeName;
                    flag = objectBSProfileType.UpdateActivitiesCategoryGroup(objectActivitiesCategory);
                }
                else
                {
                    objectLocationCategory.LocationID = Convert.ToInt32(ListBoxProfileTypes.SelectedValue);
                    objectLocationCategory.LocationCategory1 = txtProfileTypeName;
                    flag = objectBSProfileType.UpdateLocationCategory(objectLocationCategory);
                }

                if (flag)
                {
                    CommonClass.Show("Profile Type Updated Successfully");
                }
                else
                {
                    CommonClass.Show("Fail to Update Profile Type");
                }
                ButtonDecremental.Enabled = false;
                ButtonDecremental.CssClass = "aspSquareButtonDisable";
                ButtonIncremental.Enabled = false;
                ButtonIncremental.CssClass = "aspSquareButtonDisable";
                TextBoxProfileType.Text = string.Empty;
                ListBoxProfileTypes.DataBind();
            }
            catch (Exception ex)
            {
                ex.Data.Add("Events", "ButtonUpdate_Click");
                ex.Data.Add("Page", "ProfileType.aspx");
                LogManager._stringObject = "ProfileType.aspx ---- ButtonUpdate_Click";
                LogManager.SetExceptionDetails(ex, LogManager._stringObject, null);
                LogManager.LogException(ex, LogManager.LoggingCategory.General, LogManager.LoggingLevel.Error);
                CommonClass.Show(LogManager.ShowErrorDetail(ex));
            }
        }

        protected void ButtonDelete_Click(object sender, EventArgs e)
        {
            bool flag = false;
            try
            {
                RMC.BussinessService.BSProfileType objectBSProfileType = new RMC.BussinessService.BSProfileType();

                if (Request.QueryString["valuetype"] == "0")
                {
                    flag = objectBSProfileType.DeleteValueAddedType(Convert.ToInt32(ListBoxProfileTypes.SelectedValue));
                }
                else if (Request.QueryString["valuetype"] == "1")
                {
                    flag = objectBSProfileType.DeleteCategoryGroup(Convert.ToInt32(ListBoxProfileTypes.SelectedValue));
                }
                else if (Request.QueryString["valuetype"] == "3")
                {
                    flag = objectBSProfileType.DeleteActivitiesCategory(Convert.ToInt32(ListBoxProfileTypes.SelectedValue));
                }
                else
                {
                    flag = objectBSProfileType.DeleteLocationCategory(Convert.ToInt32(ListBoxProfileTypes.SelectedValue));
                }

                if (flag)
                {
                    CommonClass.Show("Delete Record Successfully");
                }
                else
                {
                    CommonClass.Show("Fail to Delete Record");
                }
                ButtonDecremental.Enabled = false;
                ButtonDecremental.CssClass = "aspSquareButtonDisable";
                ButtonIncremental.Enabled = false;
                ButtonIncremental.CssClass = "aspSquareButtonDisable";
                TextBoxProfileType.Text = string.Empty;
                ListBoxProfileTypes.DataBind();
            }
            catch (Exception ex)
            {
                ex.Data.Add("Events", "ButtonDelete_Click");
                ex.Data.Add("Page", "ProfileType.aspx");
                LogManager._stringObject = "ProfileType.aspx ---- ButtonDelete_Click";
                LogManager.SetExceptionDetails(ex, LogManager._stringObject, null);
                LogManager.LogException(ex, LogManager.LoggingCategory.General, LogManager.LoggingLevel.Error);
                CommonClass.Show(LogManager.ShowErrorDetail(ex));
            }
        }

        protected void ListBoxProfileTypes_SelectedIndexChanged(object sender, EventArgs e)
        {
            string lstProfileTypeName = string.Empty;
            string lstProfileType = ListBoxProfileTypes.SelectedItem.ToString();
            int indexOfNonAlphaNumeric = lstProfileType.IndexOf('(') + 1;
            string extractNonAlphaNumeric = string.Empty;
            
            if(indexOfNonAlphaNumeric > 1)
            {
                extractNonAlphaNumeric = lstProfileType.Substring(indexOfNonAlphaNumeric, 1);
                lstProfileTypeName = lstProfileType.Substring(0, indexOfNonAlphaNumeric - 2);
            }
            else
            {
                lstProfileTypeName = lstProfileType; 
            }

            if (extractNonAlphaNumeric == "-")
            {                
                ButtonDecremental.Enabled = false;
                ButtonDecremental.CssClass = "aspSquareButtonDisable";
                ButtonIncremental.Enabled = true;
                ButtonIncremental.CssClass = "aspSquareButton";
            }
            else
            {
                ButtonIncremental.Enabled = false;
                ButtonIncremental.CssClass = "aspSquareButtonDisable";
                ButtonDecremental.Enabled = true;
                ButtonDecremental.CssClass = "aspSquareButton";
            }
           
            TextBoxProfileType.Text = lstProfileTypeName;
            ButtonSave.Visible = false;
            ButtonDelete.Visible = true;
            ButtonUpdate.Visible = true;
            ButtonCancel.Visible = true;
        }

        protected void ButtonCancel_Click(object sender, EventArgs e)
        {
            ButtonSave.Visible = true;
            ButtonDelete.Visible = true;
            ButtonUpdate.Visible = false;
            ButtonCancel.Visible = false;
            ButtonDecremental.Enabled = false;
            ButtonDecremental.CssClass = "aspSquareButtonDisable";
            ButtonIncremental.Enabled = false;
            ButtonIncremental.CssClass = "aspSquareButtonDisable";
        }

        protected void ButtonIncremental_Click(object sender, EventArgs e)
        {
            ButtonDecremental.Enabled = true;
            ButtonDecremental.CssClass = "aspSquareButton";
            ButtonIncremental.Enabled = false;
            ButtonIncremental.CssClass = "aspSquareButtonDisable";
        }

        protected void ButtonDecremental_Click(object sender, EventArgs e)
        {
            ButtonDecremental.Enabled = false;
            ButtonDecremental.CssClass = "aspSquareButtonDisable";
            ButtonIncremental.Enabled = true;
            ButtonIncremental.CssClass = "aspSquareButton";
        }

        protected void TextBoxProfileType_TextChanged(object sender, EventArgs e)
        {
            ButtonDecremental.Enabled = true;
            ButtonIncremental.CssClass = "aspSquareButton";
            ButtonDecremental.Enabled = true;
            ButtonIncremental.CssClass = "aspSquareButton";
        }
    }
}