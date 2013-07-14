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

namespace RMC.Web.Users
{
    public partial class EditOrViewHospitalRegistration : System.Web.UI.Page
    {

        #region Variables

        //Bussiness Service Object.
        RMC.BussinessService.BSMultiUserHospital _objectBSMultiUserHospital = null;
        RMC.BussinessService.BSPermission _objectBSPermission = null;
        RMC.BussinessService.BSHospitalInfo _objectBSHospitalInfo = null;
        RMC.BussinessService.BSCommon _objectBSCommon = null;
        

        //Data Service Object.
        RMC.DataService.HospitalInfo _objectHospitalInfo = null;
        RMC.DataService.MultiUserHospital _objectMultiUserHospital = null;
         
        //Fundamental Data Types
        bool _flag;
        string _permissionName;
        
        #endregion

        #region Events

        /// <summary>
        /// Button Event to reset Control status.
        /// Created By : Davinder Kumar
        /// Creation Date : June 24, 2009
        /// Modified By : Davinder Kumar
        /// Modified Date : July 8, 2009
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
                ex.Data.Add("Page", "EditOrViewHospitalRegistration.aspx");
                LogManager._stringObject = "EditOrViewHospitalRegistration.aspx ---- ButtonReset_Click";
                LogManager.SetExceptionDetails(ex, LogManager._stringObject, null);
                LogManager.LogException(ex, LogManager.LoggingCategory.General, LogManager.LoggingLevel.Error);
                DisplayMessage(LogManager.ShowErrorDetail(ex), System.Drawing.Color.Red);
            }
        }

        /// <summary>
        /// Save Button Events to Save Hospital Infomation in a Database.
        /// Created By : Davinder Kumar.
        /// Creation Date : June 24, 2009
        /// Modified By : Davinder Kumar
        /// Modified Date : July 22, 2009
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ButtonUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                _flag = false;
                if (Page.IsValid)
                {
                    _objectBSHospitalInfo = new RMC.BussinessService.BSHospitalInfo();
                    if (!_objectBSHospitalInfo.ExistHospital(TextBoxHospitalName.Text.Trim(), TextBoxCity.Text.Trim()))
                    {
                        _objectHospitalInfo = UpdateHospitalInfo();

                        _flag = _objectBSHospitalInfo.UpdateHospitalInformation(_objectHospitalInfo);
                        if (_flag)
                        {
                            DisplayMessage("Update Hospital Information Successfully.", System.Drawing.Color.Green);
                            ResetControls();
                        }
                        else
                        {
                            DisplayMessage("Fail to Update Hospital Infomation.", System.Drawing.Color.Red);
                        }
                    }
                    //End of Hospital Check.
                    else
                    {
                        DisplayMessage("Hospital already Exist.", System.Drawing.Color.Red);
                    }
                }
            }
            catch (Exception ex)
            {
                ex.Data.Add("Events", "ButtonUpdate_Click");
                ex.Data.Add("Page", "EditOrViewHospitalRegistration.aspx");
                LogManager._stringObject = "EditOrViewHospitalRegistration.aspx ---- ButtonUpdate_Click";
                LogManager.SetExceptionDetails(ex, LogManager._stringObject, null);
                LogManager.LogException(ex, LogManager.LoggingCategory.General, LogManager.LoggingLevel.Error);
                DisplayMessage(LogManager.ShowErrorDetail(ex), System.Drawing.Color.Red);
            }
            finally
            {
                _objectBSHospitalInfo = null;
                _objectHospitalInfo = null;
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
                    DropDownListState.Items.Add("Select State");
                    DropDownListState.Items[0].Value = 0.ToString();
                }

                ScriptManagerEditOrViewHospitalRegistration.FindControl("DropDownListCountry").Focus();
                UpdatePanelState.Update();
            }
            catch (Exception ex)
            {
                ex.Data.Add("Events", "DropDownListCountry_SelectedIndexChanged");
                ex.Data.Add("Page", "EditOrViewHospitalRegistration.aspx");
                LogManager._stringObject = "EditOrViewHospitalRegistration.aspx ---- DropDownListCountry_SelectedIndexChanged";
                LogManager.SetExceptionDetails(ex, LogManager._stringObject, null);
                LogManager.LogException(ex, LogManager.LoggingCategory.General, LogManager.LoggingLevel.Error);
                DisplayMessage(LogManager.ShowErrorDetail(ex), System.Drawing.Color.Red);
            }
        }

        /// <summary>
        /// Check the User's Permission on Hospital.
        /// Created By : Davinder Kumar.
        /// Creation Date : July 23, 2009.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            RMC.BusinessEntities.BESessionInfomation objectBESessionInfomation = new RMC.BusinessEntities.BESessionInfomation();
            try
            {
                _flag = false;
                if (Session["UserInformation"] != null)
                {
                    if (Request.QueryString.Count > 0)
                    {
                        _objectBSMultiUserHospital = new RMC.BussinessService.BSMultiUserHospital();
                        int hospitalID = Convert.ToInt32(Request.QueryString["HospitalInfoID"]);
                        int permissionID = Convert.ToInt32(Request.QueryString["PermissionID"]);
                        int userID = CommonClass.UserInformation.UserID;
                        _flag = _objectBSMultiUserHospital.CheckPermissionOnHospitalByUserID(userID, hospitalID, permissionID);
                    }
                    else
                    {
                        objectBESessionInfomation.ErrorMessage = "Invalid Hospital Information and User Permission.";
                        CommonClass.SessionInfomation = objectBESessionInfomation;
                        Response.Redirect("~/Users/ErrorMessage.aspx", false);
                    }
                }
                else
                {
                    objectBESessionInfomation.ErrorMessage = "Invalid User Information.";
                    CommonClass.SessionInfomation = objectBESessionInfomation;
                    Response.Redirect("~/Users/ErrorMessage.aspx", false);
                }

                if (_flag)
                {
                    _objectBSPermission = new RMC.BussinessService.BSPermission();
                    int permissionID = Convert.ToInt32(Request.QueryString["PermissionID"]);
                    _permissionName = _objectBSPermission.GetPermissionByPermissionID(permissionID);
                    if (_permissionName.ToLower().Trim() == "readonly")
                    {
                        MultiViewForEditOrViewHospitalRegistration.ActiveViewIndex = 0;
                    }
                    else
                    {
                        MultiViewForEditOrViewHospitalRegistration.ActiveViewIndex = 1;
                    }
                }
                else
                {
                    objectBESessionInfomation.ErrorMessage = "Invalid User Permission.";
                    CommonClass.SessionInfomation = objectBESessionInfomation;
                    Response.Redirect("~/Users/ErrorMessage.aspx", false);
                }
            }
            catch (Exception ex)
            {
                ex.Data.Add("Events", "Page_Load");
                ex.Data.Add("Page", "EditOrViewHospitalRegistration.aspx");
                LogManager._stringObject = "EditOrViewHospitalRegistration.aspx ---- Page_Load";
                LogManager.SetExceptionDetails(ex, LogManager._stringObject, null);
                LogManager.LogException(ex, LogManager.LoggingCategory.General, LogManager.LoggingLevel.Error);
                CommonClass.SessionInfomation.ErrorMessage = LogManager.ShowErrorDetail(ex);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ViewEditHospitalRegistration_Load(object sender, EventArgs e)
        {
            try
            {
                if (!Page.IsPostBack)
                {
                    int hospitalID = Convert.ToInt32(Request.QueryString["HospitalInfoID"]);
                    int userID = CommonClass.UserInformation.UserID;

                    populateHospitalInformationForEdit(userID, hospitalID);
                }
                //Add Javascript Event to restrict the special character in Textboxes.
                TextBoxHospitalName.Attributes.Add("onKeyDown", "return validateNonNumber(event);");
                TextBoxCNOFirstName.Attributes.Add("onKeyDown", "return validateNonNumber(event);");
                TextBoxCNOLastName.Attributes.Add("onKeyDown", "return validateNonNumber(event);");
                TextBoxZip.Attributes.Add("onKeyDown", "return FilterAlphaNumericDash(event);");                
                TextBoxCity.Attributes.Add("onKeyDown", "return validateNonNumber(event);");
            }
            catch (Exception ex)
            {
                ex.Data.Add("Events", "ViewEditHospitalRegistration_Load");
                ex.Data.Add("Page", "EditOrViewHospitalRegistration.aspx");
                LogManager._stringObject = "EditOrViewHospitalRegistration.aspx ---- ViewEditHospitalRegistration_Load";
                LogManager.SetExceptionDetails(ex, LogManager._stringObject, null);
                LogManager.LogException(ex, LogManager.LoggingCategory.General, LogManager.LoggingLevel.Error);
                DisplayMessage(LogManager.ShowErrorDetail(ex), System.Drawing.Color.Red);
            }
        }

        /// <summary>
        /// Populate Hospital Infomation for view.
        /// Created By : Davinder Kumar.
        /// Creation Date : July 24, 2009.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ViewHospitalRegistration_Load(object sender, EventArgs e)
        {
            try
            {
                int hospitalID = Convert.ToInt32(Request.QueryString["HospitalInfoID"]);
                int userID = CommonClass.UserInformation.UserID;

                populateHospitalInformation(userID, hospitalID);                
            }
            catch (Exception ex)
            {
                ex.Data.Add("Events", "ViewHospitalRegistration_Load");
                ex.Data.Add("Page", "EditOrViewHospitalRegistration.aspx");
                LogManager._stringObject = "EditOrViewHospitalRegistration.aspx ---- ViewHospitalRegistration_Load";
                LogManager.SetExceptionDetails(ex, LogManager._stringObject, null);
                LogManager.LogException(ex, LogManager.LoggingCategory.General, LogManager.LoggingLevel.Error);
                DisplayMessageForView(LogManager.ShowErrorDetail(ex), System.Drawing.Color.Red);
            }
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Use to Display Error Message for View.
        /// Created By : Davinder Kumar.
        /// Creation Date : July 24, 2009.
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="color"></param>
        private void DisplayMessageForView(string msg, System.Drawing.Color color)
        {
            try
            {
                LabelViewHospitalRegistrationError.Text = msg;
                PanelViewHospitalRegistrationError.ForeColor = color;
                PanelViewHospitalRegistrationError.Visible = true;
            }
            catch (Exception ex)
            {
                ex.Data.Add("Function", "DisplayMessage");
                ex.Data.Add("Class", "UserRegistration");
                throw ex;
            }
        }
        
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
        /// Populate Hospital Infomation By HospitalID and UserID.
        /// Created By : Davinder Kumar.
        /// Creation Date : July 24, 2009.
        /// </summary>
        /// <param name="userID"></param>
        private void populateHospitalInformation(int userID, int hospitalID)
        {
            try
            {
                _objectBSHospitalInfo = new RMC.BussinessService.BSHospitalInfo();

                _objectHospitalInfo = _objectBSHospitalInfo.GetHospitalInfoByUserID(userID, hospitalID);
                LabelAddress.Text = _objectHospitalInfo.Address;
                LabelCity.Text = _objectHospitalInfo.City;
                LabelCNOFirstName.Text = _objectHospitalInfo.ChiefNursingOfficerFirstName;
                LabelCNOLastName.Text = _objectHospitalInfo.ChiefNursingOfficerLastName;
                LabelCNOPhone.Text = _objectHospitalInfo.ChiefNursingOfficerPhone;
                LabelCountry.Text = _objectHospitalInfo.State.Country.CountryName;
                LabelHospitalName.Text = _objectHospitalInfo.HospitalName;
                LabelState.Text = _objectHospitalInfo.State.StateName;
                LabelZip.Text = _objectHospitalInfo.Zip;
            }
            catch (Exception ex)
            {
                ex.Data.Add("Function", "populateHospitalInformation");
                ex.Data.Add("Class", "EditOrViewHospitalRegistration");
                throw ex;
            }
        }

        /// <summary>
        /// Populate Hospital Infomation For Edit.
        /// Created By : Davinder Kumar.
        /// Creation Date : July 24, 2009.
        /// </summary>
        /// <param name="userID"></param>
        private void populateHospitalInformationForEdit(int userID, int hospitalID)
        {
            try
            {
                _objectBSHospitalInfo = new RMC.BussinessService.BSHospitalInfo();

                _objectHospitalInfo = _objectBSHospitalInfo.GetHospitalInfoByUserID(userID, hospitalID);
                TextBoxAddress.Text = _objectHospitalInfo.Address;
                TextBoxCity.Text = _objectHospitalInfo.City;
                TextBoxCNOFirstName.Text = _objectHospitalInfo.ChiefNursingOfficerFirstName;
                TextBoxCNOLastName.Text = _objectHospitalInfo.ChiefNursingOfficerLastName;
                TextBoxCNOPhone.Text = _objectHospitalInfo.ChiefNursingOfficerPhone;
                DropDownListCountry.DataBind();
                DropDownListCountry.SelectedIndex = DropDownListCountry.Items.IndexOf(DropDownListCountry.Items.FindByValue(_objectHospitalInfo.State.CountryID.ToString()));
                TextBoxHospitalName.Text = _objectHospitalInfo.HospitalName;
                DropDownListState.DataBind();
                DropDownListState.SelectedIndex = DropDownListState.Items.IndexOf(DropDownListState.Items.FindByValue(_objectHospitalInfo.StateID.ToString()));
                TextBoxZip.Text = _objectHospitalInfo.Zip;
            }
            catch (Exception ex)
            {
                ex.Data.Add("Function", "populateHospitalInformation");
                ex.Data.Add("Class", "EditOrViewHospitalRegistration");
                throw ex;
            }
        }

        /// <summary>
        /// Reset all control status.
        /// Created By : Davinder Kumar.
        /// Creation Date : July 21, 2009.
        /// Modified By : Davinder Kumar
        /// Modified Date : July 22, 2009
        /// </summary>
        private void ResetControls()
        {
            try
            {
                int hospitalID = Convert.ToInt32(Request.QueryString["HospitalInfoID"]);
                int userID = CommonClass.UserInformation.UserID;

                populateHospitalInformationForEdit(userID, hospitalID);
            }
            catch (Exception ex)
            {
                ex.Data.Add("Function", "Reset");
                ex.Data.Add("Class", "EditOrViewHospitalRegistration");
                throw ex;
            }
        }

        /// <summary>
        /// Save Hospital Infomation In a Data service object.
        /// Created By : Davinder Kumar.
        /// Creation Date : July 21, 2009.
        /// Modified By : Davinder Kumar
        /// Modified Date : July 22, 2009
        /// </summary>
        /// <returns></returns>
        private RMC.DataService.HospitalInfo UpdateHospitalInfo()
        {
            _objectHospitalInfo = new RMC.DataService.HospitalInfo();
            try
            {
                int userID = CommonClass.UserInformation.UserID;
                string userName = CommonClass.UserInformation.FirstName + " " + CommonClass.UserInformation.LastName;
                int hospitalID = Convert.ToInt32(Request.QueryString["HospitalInfoID"]);

                _objectHospitalInfo.HospitalInfoID = hospitalID;
                _objectHospitalInfo.Address = TextBoxAddress.Text;
                _objectHospitalInfo.City = TextBoxCity.Text;
                _objectHospitalInfo.CreatedBy = userName;
                _objectHospitalInfo.CreatedDate = DateTime.Now;
                _objectHospitalInfo.HospitalName = TextBoxHospitalName.Text;
                _objectHospitalInfo.StateID = Convert.ToInt32(DropDownListState.SelectedValue);
                _objectHospitalInfo.Zip = TextBoxZip.Text;
                _objectHospitalInfo.ChiefNursingOfficerFirstName = TextBoxCNOFirstName.Text;
                _objectHospitalInfo.ChiefNursingOfficerLastName = TextBoxCNOLastName.Text;
                _objectHospitalInfo.ChiefNursingOfficerPhone = TextBoxCNOPhone.Text;
                _objectHospitalInfo.UserID = userID;
                _objectHospitalInfo.IsActive = true;
                _objectHospitalInfo.IsDeleted = false;
            }
            catch (Exception ex)
            {
                ex.Data.Add("Function", "SaveHospitalInfo");
                ex.Data.Add("Class", "EditOrViewHospitalRegistration");
                throw ex;
            }

            return _objectHospitalInfo;
        }

        #endregion
               
    }
    //End Of EditOrViewHospitalRegistration Class
}
//End Of NameSpace