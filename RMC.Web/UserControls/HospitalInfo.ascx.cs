using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using LogExceptions;
using System.Web.Security;
using RMC.BussinessService;
using System.IO;

namespace RMC.Web
{
    public partial class HospitalInfo : System.Web.UI.UserControl
    {
        #region Variables
        RMC.BussinessService.BSUsers _objectBSUsers = null;
        RMC.BussinessService.BSHospitalInfo _objectBSHospitalInfo = null;
        RMC.DataService.HospitalInfo _objectHospitalInfo = null;
        RMC.BusinessEntities.BEHospitalInfo _objectBEHospitalInfo = null;
        #endregion
        #region properties
        /// <summary>
        /// Return Email of Logged in user
        /// <Author>Raman</Author>
        /// <createdOn>July 22, 2009</createdOn>
        /// </summary>
        public string ActiveUser
        {
            get
            {
                return ((List<RMC.DataService.UserInfo>)(BSSerialization.Deserialize<List<RMC.DataService.UserInfo>>(HttpContext.Current.Session["UserInformation"] as MemoryStream))).Count > 0 ? CommonClass.UserInformation.Email : "";
            }
        }
        /// <summary>
        /// Return role of Logged in user
        /// <Author>Raman</Author>
        /// <createdOn>July 22, 2009</createdOn>
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
        /// <summary>
        /// Return true if loggedin user is superadmin
        /// <Author>Raman</Author>
        /// <createdOn>July 22, 2009</createdOn>
        /// </summary>
        public bool IsSuperAdmin
        {
            get
            {
                return UserType.ToLower() == Convert.ToString(RMC.Web.UserRole.SuperAdmin).ToLower();
                //return ((List<RMC.DataService.UserInfo>)(BSSerialization.Deserialize<List<RMC.DataService.UserInfo>>(HttpContext.Current.Session["UserInformation"] as MemoryStream))).Count > 0 ? CommonClass.UserInformation.UserTypeID==Convert.ToInt32(RMC.Web.UserRole.SuperAdmin)?true :false:false;
            }
        }
        /// <summary>
        /// Return true if Page has been opened for Creating Hospital
        /// <Author>Raman</Author>
        /// <createdOn>July 22, 2009</createdOn>
        /// </summary>
        public bool CreateHospital
        {
            get
            {
                if (HospitalInfoId > 0)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
        }
        /// <summary>
        /// Return passed HospitalInfoId
        /// <Author>Raman</Author>
        /// <createdOn>July 22, 2009</createdOn>
        /// </summary>
        private int HospitalInfoId
        {
            get
            {
                return ViewState["HospitalInfoId"] != null ? Convert.ToInt32(ViewState["HospitalInfoId"]) : 0;
            }
            set
            {
                ViewState["HospitalInfoId"] = value;
            }
        }
        #endregion
        #region Events
        /// <summary>
        /// Page load event
        /// <CreatedBy>Raman</CreatedBy>
        /// <CreatedOn>July 20, 2009</CreatedOn>
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            int argHospitalInfoId = 0;
            if (ViewState["PerviousURL"] == null)
            {
                ViewState["PerviousURL"] = Request.UrlReferrer.ToString();
            }
            try
            {
                if (Page.IsPostBack == false)
                {
                    int.TryParse(Request.QueryString["HospitalInfoId"], out argHospitalInfoId);
                    HospitalInfoId = argHospitalInfoId;
                    BindUsers();
                    if (HospitalInfoId > 0)
                    {
                        PopulateData();
                    }
                }
            }
            catch (Exception ex)
            {
                ex.Data.Add("Events", "Page Load");
                ex.Data.Add("Page", "HospitalInfo.ascx.cs");
                LogManager.SetExceptionDetails(ex, LogManager._stringObject, null);
                LogManager.LogException(ex, LogManager.LoggingCategory.General, LogManager.LoggingLevel.Error);
                DisplayMessage(LogManager.ShowErrorDetail(ex), System.Drawing.Color.Red);
            }
        }
        /// <summary>
        /// Use to fill state dropdownlist.
        /// Created By : Davinder Kumar.
        /// Creation Date : July 8, 2009.
        /// Modified By : Davinder Kumar
        /// Modified Date : July 22, 2009
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void DropDownListCountry_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (DropDownListState.Items.Count > 1)
                {
                    DropDownListState.Items.Clear();
                    DropDownListState.Items.Add("Select");
                    DropDownListState.Items[0].Value = 0.ToString();
                }

                ScriptManagerHospitalInfo.FindControl("DropDownListCountry").Focus();
                UpdatePanelState.Update();
            }
            catch (Exception ex)
            {
                ex.Data.Add("Events", "DropDownListCountry_SelectedIndexChanged");
                ex.Data.Add("Page", "HospitalInfo.ascx.cs");
                LogManager._stringObject = "HospitalInfo.ascx.cs ---- DropDownListCountry_SelectedIndexChanged";
                LogManager.SetExceptionDetails(ex, LogManager._stringObject, null);
                LogManager.LogException(ex, LogManager.LoggingCategory.General, LogManager.LoggingLevel.Error);
                DisplayMessage(LogManager.ShowErrorDetail(ex), System.Drawing.Color.Red);
            }
        }
        /// <summary>
        /// To save hospital information
        /// <CreatedBy>Raman</CreatedBy>
        /// <CreatedOn>July 20, 2009</CreatedOn>
        /// </summary>
        protected void ButtonSave_Click(object sender, EventArgs e)
        {
            bool isSaved = false;
            try
            {
                if (Page.IsValid)
                {
                    _objectBEHospitalInfo = new RMC.BusinessEntities.BEHospitalInfo();
                    _objectBSHospitalInfo = new RMC.BussinessService.BSHospitalInfo();
                    if (HospitalInfoId <= 0)
                    {
                        if (_objectBSHospitalInfo.ExistHospital(TextBoxHospitalName.Text, TextBoxCity.Text) == true)
                        {
                            CommonClass.Show("This Hospital Added Already!");
                            //DisplayMessage("This hospital has already been added!", System.Drawing.Color.Red);
                            return;
                        }
                    }
                    _objectBEHospitalInfo.Address = TextBoxAddress.Text;
                    _objectBEHospitalInfo.ChiefNursingOfficerFirstName = TextBoxCNOFirstName.Text;
                    _objectBEHospitalInfo.ChiefNursingOfficerLastName = TextBoxCNOLastName.Text;
                    _objectBEHospitalInfo.ChiefNursingOfficerPhone = TextBoxCNOPhone.Text;
                    _objectBEHospitalInfo.City= TextBoxCity.Text;
                    _objectBEHospitalInfo.CountryID =Convert.ToInt32( DropDownListCountry.SelectedValue);
                    _objectBEHospitalInfo.HospitalInfoID = HospitalInfoId;
                    _objectBEHospitalInfo.HospitalName = TextBoxHospitalName.Text;
                    _objectBEHospitalInfo.StateID = Convert.ToInt32(DropDownListState.SelectedValue);
                    _objectBEHospitalInfo.UserID = Convert.ToInt32(DropDownListAdmin.SelectedValue);
                    _objectBEHospitalInfo.Zip = TextBoxZip.Text;
                    _objectBEHospitalInfo.IsDeleted = false;
                    
                    _objectBSHospitalInfo.UpdateHospitalInfo(_objectBEHospitalInfo,ActiveUser);
                    if (CreateHospital == true)
                    {
                        CommonClass.Show("Record Saved Successfully!");
                        //DisplayMessage("Record has been saved successfully!", System.Drawing.Color.Green);
                    }
                    else
                    {
                        CommonClass.Show("Record Updated Successfully!");
                        //DisplayMessage("Record has been updated successfully!", System.Drawing.Color.Green);
                    }
                    isSaved = true;
                }
            }
            catch (Exception ex)
            {
                ex.Data.Add("Events", "ButtonSave_Click");
                ex.Data.Add("Page", "HospitalInfo.ascx.cs");
                LogManager._stringObject = "HospitalInfo.ascx.cs ---- DropDownListCountry_SelectedIndexChanged";
                LogManager.SetExceptionDetails(ex, LogManager._stringObject, null);
                LogManager.LogException(ex, LogManager.LoggingCategory.General, LogManager.LoggingLevel.Error);
                DisplayMessage(LogManager.ShowErrorDetail(ex), System.Drawing.Color.Red);
            }
            finally
            {
                _objectBEHospitalInfo = null;
                _objectBSHospitalInfo = null;
            }
            if (isSaved == true)
            {
                //Response.Redirect("ManageHospitals.aspx");
                Response.Redirect("DataManagement.aspx", false);
            }
        }
        /// <summary>
        /// Reset Page
        /// <CreatedBy>Raman</CreatedBy>
        /// <CreatedOn>July 20, 2009</CreatedOn>
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ButtonReset_Click(object sender, EventArgs e)
        {
            try
            {
                PopulateData();
            }
            catch (Exception ex)
            {
                ex.Data.Add("Events", "ButtonReset_Click");
                ex.Data.Add("Page", "HospitalInfo.ascx.cs");
                LogManager._stringObject = "HospitalInfo.ascx.cs ---- DropDownListCountry_SelectedIndexChanged";
                LogManager.SetExceptionDetails(ex, LogManager._stringObject, null);
                LogManager.LogException(ex, LogManager.LoggingCategory.General, LogManager.LoggingLevel.Error);
                DisplayMessage(LogManager.ShowErrorDetail(ex), System.Drawing.Color.Red);
            }
        }
        /// <summary>
        /// Redirects to pervious page.
        /// <CreatedBy>Raman</CreatedBy>
        /// <CreatedOn>Aug 06, 2009</CreatedOn>
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ImageButtonBack_Click(object sender, ImageClickEventArgs e)
        {
            if (ViewState["PerviousURL"] != null)
            {
                Response.Redirect(ViewState["PerviousURL"].ToString());
            }
        }

        #endregion
        #region Functions
        /// <summary>
        /// Use to Display message of Login Failure.
        /// Created By : Davinder Kumar.
        /// Creation Date : July 21, 2009.
        /// Modified By : Davinder Kumar
        /// Modified Date : July 22, 2009
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="color"></param>
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
                ex.Data.Add("Class", "UserRegistration");
                throw ex;
            }
        }
        /// <summary>
        /// Used to Bind User dropdownlist.
        /// <CreatedBy>Raman</CreatedBy>
        /// <CreatedOn>July 20, 2009</CreatedOn>
        /// </summary>        
        private void BindUsers()
        {
            List<RMC.BusinessEntities.BEUserInfoTye> listUserInfo = null;
            try
            {
                _objectBSUsers = new RMC.BussinessService.BSUsers();
                listUserInfo = _objectBSUsers.GetUsers();
                DropDownListAdmin.DataSource = listUserInfo;
                DropDownListAdmin.DataTextField = "UserName";
                DropDownListAdmin.DataValueField = "UserId";
                DropDownListAdmin.DataBind();
                DropDownListAdmin.Items.Insert(0, new ListItem("Select", "0"));
            }
            catch (Exception ex)
            {
                ex.Data.Add("Function", "BindUsers");
                ex.Data.Add("Class", "HospitalInfo.ascx.cs");
                throw ex;
            }
            finally { _objectBSUsers = null; }
        }
        /// <summary>
        /// To populate data on the page
        /// <CreatedBy>Raman</CreatedBy>
        /// <CreatedOn>July 20, 2009</CreatedOn>
        /// </summary>
        private void PopulateData()
        {
            try
            {
                _objectBSHospitalInfo = new RMC.BussinessService.BSHospitalInfo();
                _objectBEHospitalInfo = _objectBSHospitalInfo.GetHospitalInformation(HospitalInfoId);
                if (_objectBEHospitalInfo != null)
                {
                    TextBoxAddress.Text = _objectBEHospitalInfo.Address;
                    TextBoxHospitalName.Text = _objectBEHospitalInfo.HospitalName;
                    DropDownListAdmin.SelectedValue = Convert.ToString(_objectBEHospitalInfo.UserID);
                    TextBoxCNOFirstName.Text = _objectBEHospitalInfo.ChiefNursingOfficerFirstName;
                    TextBoxCNOLastName.Text = _objectBEHospitalInfo.ChiefNursingOfficerLastName;
                    TextBoxCNOPhone.Text = _objectBEHospitalInfo.ChiefNursingOfficerPhone;
                    TextBoxCity.Text = _objectBEHospitalInfo.City;
                    TextBoxZip.Text = _objectBEHospitalInfo.Zip;
                    DropDownListCountry.DataBind();
                    DropDownListCountry.SelectedValue = Convert.ToString(_objectBEHospitalInfo.CountryID);
                    DropDownListState.DataBind();
                    DropDownListState.SelectedValue = Convert.ToString(_objectBEHospitalInfo.StateID);
                }
                else
                {
                    ResetData();
                }

            }
            catch (Exception ex)
            {
                ex.Data.Add("Function", "PopulateData");
                ex.Data.Add("Class", "HospitalInfo.ascx.cs");
                throw ex;
            }
            finally { _objectBEHospitalInfo = null; }
        }
        private void ResetData()
        {
            TextBoxAddress.Text = "";
            TextBoxHospitalName.Text = "";
            //DropDownListAdmin.SelectedValue =0; 
            TextBoxCNOFirstName.Text = "";
            TextBoxCNOLastName.Text = "";
            TextBoxCNOPhone.Text = "";
            TextBoxCity.Text = "";
            TextBoxZip.Text = "";
            DropDownListState.ClearSelection();
            DropDownListState.Items[0].Selected = true;
            DropDownListCountry.ClearSelection();
            DropDownListCountry.Items[0].Selected = true;
            DropDownListAdmin.ClearSelection();
            DropDownListAdmin.Items[0].Selected = true;
        }
#endregion

        
    }
}