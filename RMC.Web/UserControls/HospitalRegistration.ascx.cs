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
using RMC.BussinessService;
using LogExceptions;

namespace RMC.Web.UserControls
{
    public partial class HospitalRegistration : System.Web.UI.UserControl
    {

        #region Variables

        //Bussiness Service Objects.
        BSCommon _objectBSCommon = null;
        BSHospitalInfo _objectBSHospitalInfo = null;

        //Data Service Objects
        RMC.DataService.HospitalInfo _objectHospitalInfo = null;
        RMC.DataService.MultiUserHospital _objectMultiUserHospital = null;

        //Bussiness Entity Objects.
        RMC.BusinessEntities.BEHospitalInfoDynamicProp _objectBEHospitalInfoDynamicProp;

        //Fundamental Data Types.
        bool _flag;

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
                ex.Data.Add("Page", "HospitalRegistration.ascx");
                LogManager._stringObject = "HospitalRegistration.ascx ---- ButtonReset_Click";
                LogManager.SetExceptionDetails(ex, LogManager._stringObject, null);
                LogManager.LogException(ex, LogManager.LoggingCategory.General, LogManager.LoggingLevel.Error);
                CommonClass.Show(LogManager.ShowErrorDetail(ex));
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
        protected void ButtonSave_Click(object sender, EventArgs e)
        {
            try
            {
                _flag = false;
                if (Page.IsValid)
                {
                    //CaptchaControlImage.ValidateCaptcha(TextBoxCaptchaText.Text);
                    //if (!CaptchaControlImage.UserValidated)
                    //{
                    //    PanelErrorMsg.Visible = false;
                    //    PanelErrorMsg.ForeColor = System.Drawing.Color.Red;
                    //    CommonClass.Show("Please Enter Correct Code");
                    //    //LabelErrorMsg.Text = "Please Enter Correct Code";
                    //    return;
                    //}
                    //else
                    //{
                    #region"mp.Code modified to validate the Index Number"
                    RMC.DataService.RMCDataContext _objectRMCDataContext = new RMC.DataService.RMCDataContext();
                    List<RMC.DataService.HospitalInfo> objlist = new List<RMC.DataService.HospitalInfo>();
                    objlist = (from r in _objectRMCDataContext.HospitalInfos
                               where r.RecordCounter == Convert.ToInt32(TextBoxHospitalIndex.Text)
                               select r).ToList();


                    bool flag = true;
                    foreach (RMC.DataService.HospitalInfo obj in objlist)
                    {
                        if (obj.RecordCounter == int.Parse(TextBoxHospitalIndex.Text))
                        {
                            flag = false;
                        }
                    }
                    if (flag == false)
                    {
                        CommonClass.Show("Hospital Index Already exist");
                    }
                    #endregion
                    else
                    {
                        _objectBSHospitalInfo = new BSHospitalInfo();
                        _objectBEHospitalInfoDynamicProp = new RMC.BusinessEntities.BEHospitalInfoDynamicProp();
                        if (!_objectBSHospitalInfo.ExistHospital(TextBoxHospitalName.Text.Trim(), TextBoxCity.Text.Trim()))
                        {
                            int hospitalInfoID = 0;
                            _objectHospitalInfo = SaveHospitalInfo();
                            _objectMultiUserHospital = SaveInMultiUserHospital();
                            _objectBEHospitalInfoDynamicProp = SaveInDyanmicData();

                            _flag = _objectBSHospitalInfo.InsertHospitalInfomation(_objectHospitalInfo, _objectMultiUserHospital, _objectBEHospitalInfoDynamicProp, out hospitalInfoID);
                            if (_flag)
                            {
                                CommonClass objectCommonClass = new CommonClass();
                                QueryStringHandler.QuerystringParameterEncrpt objectQuerystringParameterEncrpt = new QueryStringHandler.QuerystringParameterEncrpt();
                                string backUrl = objectCommonClass.BackButtonUrl;
                                //add by cm on 10nov2011
                                string strDirectory = Server.MapPath(Request.ApplicationPath + "/Uploads/"+TextBoxHospitalName.Text);


                                string sDirPath = Server.MapPath("~/Folder_Name_you_want");

                                System.IO.DirectoryInfo ObjSearchDir = new System.IO.DirectoryInfo(strDirectory);

                                if (!ObjSearchDir.Exists)
                                {
                                    ObjSearchDir.Create();
                                } 
                                //end
                                if (Convert.ToString(Request.QueryString["Page"]) == "DataManagement")
                                {
                                    Response.Redirect("DataManagementUnit.aspx?" + objectQuerystringParameterEncrpt.EncrptQuerystringParam("FromPage=ADD&HospitalInfoID=" + hospitalInfoID), false);
                                }
                                else
                                {
                                    Response.Redirect(backUrl, false);
                                }
                                //CommonClass.Show("Save Hospital Information Successfully.");
                                //DisplayMessage("Save Hospital Information Successfully.", System.Drawing.Color.Green);
                                //ResetControls();
                            }
                            else
                            {
                                CommonClass.Show("Failed to Save Hospital Infomation.");
                                //DisplayMessage("Fail to Save Hospital Infomation.", System.Drawing.Color.Red);
                            }
                        }
                        //End of Hospital Check
                        else
                        {
                            CommonClass.Show("Hospital Already Exist.");
                            //DisplayMessage("Hospital already Exist.", System.Drawing.Color.Red);
                        }
                    }
                }
                else
                {
                    CommonClass.Show("Please fill all required fields.");
                }
            }


            catch (Exception ex)
            {
                //lblerror.Text = ex.Message + "  " + ex.Source + "  " + ex.StackTrace + "  " + ex.HelpLink ;
                ex.Data.Add("Events", "ButtonSave_Click");
                ex.Data.Add("Page", "HospitalRegistration.ascx");
                LogManager._stringObject = "HospitalRegistration.ascx ---- ButtonSave_Click";
                LogManager.SetExceptionDetails(ex, LogManager._stringObject, null);
                LogManager.LogException(ex, LogManager.LoggingCategory.General, LogManager.LoggingLevel.Error);
                CommonClass.Show(LogManager.ShowErrorDetail(ex));
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
                ScriptManagerHospitalRegistration.FindControl("DropDownListState").Focus();
                UpdatePanelState.Update();
            }
            catch (Exception ex)
            {
                ex.Data.Add("Events", "DropDownListCountry_SelectedIndexChanged");
                ex.Data.Add("Page", "HospitalRegistration.ascx");
                LogManager._stringObject = "HospitalRegistration.ascx ---- DropDownListCountry_SelectedIndexChanged";
                LogManager.SetExceptionDetails(ex, LogManager._stringObject, null);
                LogManager.LogException(ex, LogManager.LoggingCategory.General, LogManager.LoggingLevel.Error);
                CommonClass.Show(LogManager.ShowErrorDetail(ex));
            }
        }

        /// <summary>
        /// Page Event.
        /// Created By : Davinder Kumar
        /// Creation Date : June 24, 2009
        /// Modified By : Davinder Kumar
        /// Modified Date : July 22, 2009
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                //Check for Active and InActive User
                if (!(HttpContext.Current.User.IsInRole("superadmin")))
                {
                    //if (!CommonClass.UserInformation.IsActive)
                    if (!CommonClass.UserInformation.IsActive)
                    {
                        Response.Redirect("~/Users/InActiveUser.aspx", false);
                    }
                }
                //Set Default Country.
                if (!Page.IsPostBack)
                {
                    CommonClass objectCommonClass = new CommonClass();
                    objectCommonClass.BackButtonUrl = Request.UrlReferrer.AbsoluteUri;
                    DropDownListCountry.DataBind();
                    DropDownListCountry.SelectedIndex = DropDownListCountry.Items.IndexOf(DropDownListCountry.Items.FindByText("US"));
                }

                //Check the current user type.
                if (HttpContext.Current.User.IsInRole("superadmin"))
                {
                    HyperLinkOwnerName.Visible = true;
                    DropDownListOwner.Visible = true;
                    RequiredFieldValidatorOwner.Enabled = true;
                    spanOwner.Visible = true;
                    LinkButtonHospitalType.PostBackUrl = "~/Administrator/HospitalType.aspx";
                    LinkButtonOwnershipType.PostBackUrl = "~/Administrator/OwnershipType.aspx";
                    //ImageButtonBack.PostBackUrl = "~/Administrator/HospitalUnitInfomation.aspx";
                    LinkButtonHospitalTypeForUser.Visible = false;
                    LinkButtonOwnershipTypeForUsers.Visible = false;
                    trHospitalIndex.Visible = true;
                }
                else
                {
                    HyperLinkOwnerName.Visible = false;
                    DropDownListOwner.Visible = false;
                    RequiredFieldValidatorOwner.Enabled = false;
                    spanOwner.Visible = false;
                    LinkButtonHospitalType.PostBackUrl = "~/Users/HospitalTypes.aspx";
                    LinkButtonOwnershipType.PostBackUrl = "~/Users/OwnershipType.aspx";
                    LinkButtonHospitalTypeForUser.Visible = true;
                    LinkButtonOwnershipTypeForUsers.Visible = true;
                    LinkButtonHospitalType.Visible = false;
                    LinkButtonOwnershipType.Visible = false;
                    trHospitalIndex.Visible = true;
                    //  ImageButtonBack.PostBackUrl = "~/Users/HospitalUnitInformation.aspx";
                }
                //Add Javascript Event to restrict the special character in Textboxes.
                TextBoxHospitalName.Attributes.Add("onKeyDown", "return FilterAlphaNumeric(event);");
                TextBoxCNOFirstName.Attributes.Add("onKeyDown", "return validateNonNumber(event);");
                TextBoxCNOLastName.Attributes.Add("onKeyDown", "return validateNonNumber(event);");
                TextBoxZip.Attributes.Add("onKeyDown", "return FilterAlphaNumericDash(event);");
                TextBoxCity.Attributes.Add("onKeyDown", "return validateNonNumber(event);");
            }
            catch (Exception ex)
            {
                Response.Write(ex);
                //ex.Data.Add("Events", "Page_Load");
                //ex.Data.Add("Page", "HospitalRegistration.ascx");
                //LogManager._stringObject = "HospitalRegistration.ascx ---- Page_Load";
                //LogManager.SetExceptionDetails(ex, LogManager._stringObject, null);
                //LogManager.LogException(ex, LogManager.LoggingCategory.General, LogManager.LoggingLevel.Error);
                //CommonClass.Show(LogManager.ShowErrorDetail(ex));
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_PreRender(object sender, EventArgs e)
        {
            try
            {
                if (CommonClass.SessionInfomation == null)
                {
                    RMC.BusinessEntities.BESessionInfomation objectBESessionInfomation = new RMC.BusinessEntities.BESessionInfomation();
                    CommonClass.SessionInfomation = objectBESessionInfomation;
                }
                CommonClass.SessionInfomation.HospitalName = TextBoxHospitalName.Text;
                CommonClass.SessionInfomation.HospitalUnitName = string.Empty;
            }
            catch (Exception ex)
            {
                Response.Write(ex);
                //ex.Data.Add("Function", "Page_PreRender");
                //LogManager._stringObject = "HospitalDemographicinfo.ascx.cs ---- ";
                //LogManager.SetExceptionDetails(ex, LogManager._stringObject, null);
                //LogManager.LogException(ex, LogManager.LoggingCategory.General, LogManager.LoggingLevel.Error);
                //CommonClass.Show(LogManager.ShowErrorDetail(ex));
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
                LogManager._stringObject = "HospitalRegistration.ascx ---- ImageButtonBack_Click";
                LogManager.SetExceptionDetails(ex, LogManager._stringObject, null);
                LogManager.LogException(ex, LogManager.LoggingCategory.General, LogManager.LoggingLevel.Error);
                CommonClass.Show(LogManager.ShowErrorDetail(ex));
            }
        }

        #endregion

        #region Private Methods

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
                ex.Data.Add("Class", "HospitalRegistration");
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
                TextBoxAddress.Text = string.Empty;
                TextBoxCity.Text = string.Empty;
                TextBoxHospitalName.Text = string.Empty;
                TextBoxZip.Text = string.Empty;
                TextBoxCNOFirstName.Text = string.Empty;
                TextBoxCNOLastName.Text = string.Empty;
                TextBoxCNOPhone.Text = string.Empty;
                TextBoxCNOEmail.Text = string.Empty;
                TextBoxBedsInHospital.Text = string.Empty;
                //TextBoxCaptchaText.Text = string.Empty;
                DropDownListOwnershipType.SelectedIndex = 0;
                DropDownListOwner.SelectedIndex = 0;
                DropDownListCountry.SelectedIndex = 0;
                if (DropDownListState.Items.Count > 1)
                {
                    DropDownListState.Items.Clear();
                    DropDownListState.Items.Add("Select State");
                    DropDownListState.Items[0].Value = 0.ToString();
                    DropDownListState.SelectedIndex = 0;
                }
                ListBoxHospitalType.SelectedIndex = -1;
            }
            catch (Exception ex)
            {
                ex.Data.Add("Function", "Reset");
                ex.Data.Add("Class", "HospitalRegistration");
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
        private RMC.DataService.HospitalInfo SaveHospitalInfo()
        {
            _objectHospitalInfo = new RMC.DataService.HospitalInfo();
            RMC.DataService.RMCDataContext _objectRMCDataContext = new RMC.DataService.RMCDataContext();
            try
            {
                int userID = 0;

                string userName = string.Empty;
                if (HttpContext.Current.User.IsInRole("admin"))
                {
                    //userID = CommonClass.UserInformation.UserID;
                    //userName = CommonClass.UserInformation.FirstName + " " + CommonClass.UserInformation.LastName;
                    userID = CommonClass.UserInformation.UserID;
                    userName = CommonClass.UserInformation.FirstName + " " + CommonClass.UserInformation.LastName;
                }
                _objectHospitalInfo.Address = TextBoxAddress.Text;
                _objectHospitalInfo.City = TextBoxCity.Text;
                if (HttpContext.Current.User.IsInRole("superadmin"))
                {
                    _objectHospitalInfo.CreatedBy = DropDownListOwner.SelectedItem.Text;
                }
                else
                {
                    _objectHospitalInfo.CreatedBy = userName;
                }
                _objectHospitalInfo.CreatedDate = DateTime.Now;
                _objectHospitalInfo.HospitalName = TextBoxHospitalName.Text;
                _objectHospitalInfo.StateID = Convert.ToInt32(DropDownListState.SelectedValue);              

                if (TextBoxHospitalIndex.Text != string.Empty)
                {

                    _objectHospitalInfo.RecordCounter = Convert.ToInt32(TextBoxHospitalIndex.Text);
                }
                else
                    _objectHospitalInfo.RecordCounter = 0;
                _objectHospitalInfo.Zip = TextBoxZip.Text;
                _objectHospitalInfo.ChiefNursingOfficerFirstName = TextBoxCNOFirstName.Text;
                _objectHospitalInfo.ChiefNursingOfficerLastName = TextBoxCNOLastName.Text;
                _objectHospitalInfo.ChiefNursingOfficerPhone = TextBoxCNOPhone.Text;
                _objectHospitalInfo.ChiefNursingOfficerEmail = TextBoxCNOEmail.Text;
                if (HttpContext.Current.User.IsInRole("superadmin"))
                {
                    _objectHospitalInfo.UserID = Convert.ToInt32(DropDownListOwner.SelectedItem.Value);
                }
                else
                {
                    _objectHospitalInfo.UserID = userID;
                }
                _objectHospitalInfo.IsActive = true;
                _objectHospitalInfo.IsDeleted = false;
                _objectHospitalInfo.IsDynamic = true;
            }

            catch (Exception ex)
            {
                ex.Data.Add("Function", "SaveHospitalInfo");
                ex.Data.Add("Class", "HospitalInfomation");
                throw ex;
            }

            return _objectHospitalInfo;
        }

        /// <summary>
        /// Save Permission to access Hospital.
        /// Created By : Davinder Kumar.
        /// Creation Date : July 22, 2009. 
        /// </summary>
        /// <returns></returns>
        private RMC.DataService.MultiUserHospital SaveInMultiUserHospital()
        {
            _objectMultiUserHospital = new RMC.DataService.MultiUserHospital();
            _objectBSCommon = new BSCommon();
            try
            {
                //int userID = CommonClass.UserInformation.UserID;
                //string userName = CommonClass.UserInformation.FirstName + " " + CommonClass.UserInformation.LastName;
                int userID = CommonClass.UserInformation.UserID;
                string userName = CommonClass.UserInformation.FirstName + " " + CommonClass.UserInformation.LastName;
                if (HttpContext.Current.User.IsInRole("superadmin"))
                {
                    _objectMultiUserHospital.CreatedBy = DropDownListOwner.SelectedItem.Text;
                }
                else
                {
                    _objectMultiUserHospital.CreatedBy = userName;
                }
                _objectMultiUserHospital.CreatedDate = DateTime.Now;
                _objectMultiUserHospital.IsDeleted = false;
                _objectMultiUserHospital.PermissionID = _objectBSCommon.GetPermissionIDByPermissionName("owner");
                if (HttpContext.Current.User.IsInRole("superadmin"))
                {
                    _objectMultiUserHospital.UserID = Convert.ToInt32(DropDownListOwner.SelectedValue);
                }
                else
                {
                    _objectMultiUserHospital.UserID = userID;
                }
            }
            catch (Exception ex)
            {
                ex.Data.Add("Function", "SaveInMultiUserHospital");
                ex.Data.Add("Class", "HospitalInfomation");
                throw ex;
            }

            return _objectMultiUserHospital;
        }

        /// <summary>
        /// Save fields value which adds later.
        /// Created By : Davinder Kumar.
        /// Creation Date : Aug 10, 2009.
        /// </summary>
        /// <returns></returns>
        private RMC.BusinessEntities.BEHospitalInfoDynamicProp SaveInDyanmicData()
        {
            try
            {
                string objectHospitalType = string.Empty;
                _objectBEHospitalInfoDynamicProp = new RMC.BusinessEntities.BEHospitalInfoDynamicProp();

                _objectBEHospitalInfoDynamicProp.BedsInHospital = TextBoxBedsInHospital.Text;
                for (int index = 0; index < ListBoxHospitalType.Items.Count; index++)
                {
                    if (ListBoxHospitalType.Items[index].Selected && index < ListBoxHospitalType.Items.Count - 1)
                    {
                        objectHospitalType += ListBoxHospitalType.Items[index].Text + ",";
                    }
                    else if (index == ListBoxHospitalType.Items.Count - 1)
                    {
                        objectHospitalType += ListBoxHospitalType.Items[index].Text;
                    }
                }

                _objectBEHospitalInfoDynamicProp.HospitalType = objectHospitalType;
                _objectBEHospitalInfoDynamicProp.OwnershipType = DropDownListOwnershipType.SelectedItem.Text;

                return _objectBEHospitalInfoDynamicProp;
            }
            catch (Exception ex)
            {
                ex.Data.Add("Function", "SaveInDyanmicData");
                ex.Data.Add("Class", "HospitalInfomation");
                throw ex;
            }
        }

        #endregion

    }
    //End Of HospitalRegistration Class
}
//End Of NameSpace