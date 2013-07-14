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
using RMC.BussinessService;

namespace RMC.Web.UserControls
{
    public partial class HospitalUnitInformation : System.Web.UI.UserControl
    {

        #region Variables

        //Bussiness Service Objects. 
        BSCommon _objectBSCommon = null;
        RMC.BussinessService.BSHospitalInfo _objectBSHospitalInfo = null;
        BSHospitalDemographicDetail _objectBSHospitalDemgraphicDetail = null;

        //Data Service Objects.
        RMC.DataService.HospitalDemographicInfo _objectHospitalDemographicInfo = null;
        RMC.DataService.MultiUserDemographic _objectMultiUserDemographic = null;

        //Generic Data Service Objects.
        List<RMC.DataService.HospitalInfo> _objectGenericHospitalInfo = null;
        List<RMC.BusinessEntities.BEHospitalList> _objectGenericBEHospitalList = null;

        #endregion

        #region Properties

        public RMC.DataService.HospitalInfo HospitalInformation
        {
            get
            {
                RMC.DataService.HospitalInfo objectHospitalInfo = null;
                if (Request.QueryString["HospitalInfoID"] != null)
                {
                    int hospitalInfoID = 0;
                    RMC.BussinessService.BSHospitalInfo objectBSHospitalInfo = new BSHospitalInfo();

                    int.TryParse(Convert.ToString(Request.QueryString["HospitalInfoID"]), out hospitalInfoID);
                    if (hospitalInfoID > 0)
                        objectHospitalInfo = objectBSHospitalInfo.GetHospitalInfoByHospitalInfoID(hospitalInfoID);
                }

                return objectHospitalInfo;
            }
        }

        public int HospitalUnitCounter
        {
            get
            {
                int lastValue = 0;
                if (ViewState["HospitalUnitCounter"] == null)
                {
                    RMC.BussinessService.BSHospitalDemographicDetail objectBSHospitalDemographicDetail = new BSHospitalDemographicDetail();

                    lastValue = objectBSHospitalDemographicDetail.GetTotalHospitalUnit(HospitalInformation.HospitalInfoID);
                }
                else
                {
                    lastValue = Convert.ToInt32(ViewState["HospitalUnitCounter"]);
                }

                return lastValue;
            }
        }

        #endregion

        #region Events

        /// <summary>
        /// Reset all controls.
        /// Created By : Davinder Kumar
        /// Creation Date : June 25, 2009
        /// Modified By : Davinder Kumar.
        /// Modified Date : July 21, 2009.
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
                ex.Data.Add("Page", "DemographicDetail.aspx");
                LogManager._stringObject = "DemographicDetail.aspx ---- ButtonReset_Click";
                LogManager.SetExceptionDetails(ex, LogManager._stringObject, null);
                LogManager.LogException(ex, LogManager.LoggingCategory.General, LogManager.LoggingLevel.Error);
                CommonClass.Show(LogManager.ShowErrorDetail(ex));
            }
        }

        /// <summary>
        /// Save Data of Hospital Demographic Detail.
        /// Created By : Davinder Kumar.
        /// Creation Date : June 25, 2009.
        /// Modified By : Davinder Kumar.
        /// Modified Date : July 21, 2009.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ButtonSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (Page.IsValid)
                {
                    bool flag = false;
                    _objectBSHospitalDemgraphicDetail = new BSHospitalDemographicDetail();

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
                        int hospitalUnitID = 0;
                        flag = _objectBSHospitalDemgraphicDetail.InsertHospitalDemographicDetail(SaveHospitalDemographicDetail(), SaveInHospitalDemographicDetail(), out hospitalUnitID);
                        if (flag)
                        {
                            //add by cm on 10nov2011
                            string strHospitalDir = Server.MapPath(Request.ApplicationPath + "/Uploads/" + HospitalInformation.HospitalName); 
                            string strDirectory = Server.MapPath(Request.ApplicationPath + "/Uploads/" + HospitalInformation.HospitalName + "/" + TextBoxUnitName.Text);
                            System.IO.DirectoryInfo ObjSearchDir = new System.IO.DirectoryInfo(strDirectory);
                            System.IO.DirectoryInfo ObjSearchHospitalDir = new System.IO.DirectoryInfo(strHospitalDir);
                            if (!ObjSearchHospitalDir.Exists)
                            {
                                ObjSearchHospitalDir.Create();
                            }
                            if (!ObjSearchDir.Exists)
                            {
                                ObjSearchDir.Create();
                            }
                            //end

                            if (Request.QueryString["Mode"] == "New")
                            {
                                CommonClass.Show("Save Hospital Demographic Detail Successfully.");
                                //DisplayMessage("Save Hospital Demographic Detail Successfully.", System.Drawing.Color.Green);
                                ResetControls();
                            }
                            else
                            {
                                CommonClass objectCommonClass = new CommonClass();
                                QueryStringHandler.QuerystringParameterEncrpt objectQuerystringParameterEncrpt = new QueryStringHandler.QuerystringParameterEncrpt();
                                string backUrl = objectCommonClass.BackButtonUrl;

                                if (Convert.ToString(Request.QueryString["Page"]) == "DataManagementUnit")
                                {
                                    Response.Redirect("DataManagementYear.aspx?" + objectQuerystringParameterEncrpt.EncrptQuerystringParam("FromPage=ADD&HospitalDemographicId=" + hospitalUnitID + "&HospitalInfoID=" + HospitalInformation.HospitalInfoID + "&UnitCounter=" + HospitalUnitCounter + "&PermissionID=1&FromPage=ADD"), false);
                                }
                                else
                                {
                                    Response.Redirect(backUrl, false);
                                }
                            }
                        }
                        else
                        {
                            CommonClass.Show("Fail to Save Hospital Demographic Detail.");
                            //DisplayMessage("Fail to Save Hospital Demographic Detail.", System.Drawing.Color.Red);
                        }
                    //}
                }
            }
            catch (Exception ex)
            {
                ex.Data.Add("Events", "ButtonSave_Click");
                ex.Data.Add("Page", "DemographicDetail.aspx");
                LogManager._stringObject = "DemographicDetail.aspx ---- ButtonSave_Click";
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
        protected void DropDownListHospitalName_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (DropDownListOwner.Items.Count > 1)
                {
                    DropDownListOwner.Items.Clear();
                    DropDownListOwner.Items.Add(new ListItem("Select Owner Name", "0"));
                }
                DropDownListOwner.DataBind();
            }
            catch (Exception ex)
            {
                ex.Data.Add("Events", "DropDownListHospitalName_SelectedIndexChanged");
                ex.Data.Add("Page", "DemographicDetail.aspx");
                LogManager._stringObject = "DemographicDetail.aspx ---- DropDownListHospitalName_SelectedIndexChanged";
                LogManager.SetExceptionDetails(ex, LogManager._stringObject, null);
                LogManager.LogException(ex, LogManager.LoggingCategory.General, LogManager.LoggingLevel.Error);
                CommonClass.Show(LogManager.ShowErrorDetail(ex));
            }
        }

        protected void ImageButtonBack_Click(object sender, EventArgs e)
        {
            try
            {
                CommonClass objectCommonClass = new CommonClass();
                string backUrl = objectCommonClass.BackButtonUrl;
                Response.Redirect(backUrl, false);
            }
            catch (Exception ex)
            {
                LogManager._stringObject = "DemographicDetail.aspx ---- Page_Load";
                LogManager.SetExceptionDetails(ex, LogManager._stringObject, null);
                LogManager.LogException(ex, LogManager.LoggingCategory.General, LogManager.LoggingLevel.Error);
                CommonClass.Show(LogManager.ShowErrorDetail(ex));
            }
        }

        /// <summary>
        /// Page Load Events.
        /// Created By : Davinder Kumar
        /// Creation Date : June 25, 2009
        /// Modified By : Davinder Kumar.
        /// Modified Date : July 21, 2009.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                QueryStringHandler.QuerystringParameterEncrpt objectQuerystringEncrpt = new QueryStringHandler.QuerystringParameterEncrpt();
                LiteralHospitalName.Text = HospitalInformation.HospitalName;
                if (!Page.IsPostBack)
                {
                    if (!(HttpContext.Current.User.IsInRole("superadmin")))
                    {
                        if (!CommonClass.UserInformation.IsActive)
                        {
                            Response.Redirect("~/Users/InActiveUser.aspx", false);
                        }
                    }
                    string startDate = DateTime.Now.ToString("MM/dd/yyyy", new System.Globalization.CultureInfo("en-US"));
                    TextBoxStartDate.Text = startDate;
                    ListBoxPharmacyType.SelectedIndex = -1;
                    ListBoxUnitType.SelectedIndex = -1;
                }
                if (Request.QueryString["HospitalInfoID"] != null)
                {
                    ImageButtonBack.Visible = true;
                    if (!Page.IsPostBack)
                    {
                        CommonClass objectCommonClass = new CommonClass();
                        objectCommonClass.BackButtonUrl = Request.UrlReferrer.AbsoluteUri;
                    }
                }
                //Check the current user type.
                if (HttpContext.Current.User.IsInRole("superadmin"))
                {
                    LabelOwner.Visible = true;
                    DropDownListOwner.Visible = true;
                    RequiredFieldValidatorOwner.Enabled = true;
                    spanOwner.Visible = true;
                    //if (Request.QueryString["HospitalInfoID"] != null)
                    //{
                    //    LinkButtonAddHospital.PostBackUrl = "~/Administrator/HospitalRegistration.aspx?" + objectQuerystringEncrpt.EncrptQuerystringParam("HospitalInfoID=" + Convert.ToString(Request.QueryString["HospitalInfoID"]));
                    //}
                    //else
                    //{
                    //    LinkButtonAddHospital.PostBackUrl = "~/Administrator/HospitalRegistration.aspx";
                    //}
                    LinkButtonAddUnitType.PostBackUrl = "~/Administrator/AdminUnitType.aspx";
                    LinkButtonPharmacyType.PostBackUrl = "~/Administrator/PharmacyType.aspx";
                    LinkButtonUnitTypeForUser.Visible = false;
                    LinkButtonAddPharmacyForUser.Visible = false;
                    LinkButtonAddUnitType.Visible = true;
                    LinkButtonPharmacyType.Visible = true;
                }
                else
                {
                    LabelOwner.Visible = false;
                    DropDownListOwner.Visible = false;
                    RequiredFieldValidatorOwner.Enabled = false;
                    spanOwner.Visible = false;
                    //if (Request.QueryString["HospitalInfoID"] != null)
                    //{
                    //    LinkButtonAddHospital.PostBackUrl = "~/Users/HospitalRegistration.aspx?" + objectQuerystringEncrpt.EncrptQuerystringParam("HospitalInfoID=" + Convert.ToString(Request.QueryString["HospitalInfoID"]));
                    //}
                    //else
                    //{
                    //    LinkButtonAddHospital.PostBackUrl = "~/Users/HospitalRegistration.aspx";
                    //}
                    LinkButtonAddUnitType.PostBackUrl = "~/Users/UnitType.aspx";
                    LinkButtonPharmacyType.PostBackUrl = "~/Users/PharmacyType.aspx";
                    LinkButtonUnitTypeForUser.Visible = true;
                    LinkButtonAddPharmacyForUser.Visible = true;
                    LinkButtonAddUnitType.Visible = false;
                    LinkButtonPharmacyType.Visible = false;
                }
                //Add Javascript Event to restrict the special character in Textboxes.
                TextBoxStartDate.Attributes.Add("readonly", "readonly");
                TextBoxEndDate.Attributes.Add("readonly", "readonly");
                TextBoxUnitName.Attributes.Add("onKeyDown", "return FilterAlphaNumeric(event);");
                //TextBoxUnitType.Attributes.Add("onKeyDown", "return validateNonNumber(event);");
                //TextBoxPharmacyType.Attributes.Add("onKeyDown", "return validateNonNumber(event);");
                if (HospitalInformation != null)
                {
                    bool activeUserFlag = false;
                    _objectBSHospitalInfo = new BSHospitalInfo();
                    int userID = CommonClass.UserInformation.UserID;
                    _objectGenericBEHospitalList = _objectBSHospitalInfo.GetHospitalNamesByUserID(userID);
                    if (_objectGenericBEHospitalList.Count > 0)
                    {
                        for (int index = 0; index < _objectGenericBEHospitalList.Count; index++)
                        {
                            if (HospitalInformation.HospitalInfoID == Convert.ToInt32(_objectGenericBEHospitalList[index].HospitalInfoID))
                            {
                                activeUserFlag = true;
                                break;
                            }
                        }

                        if (!activeUserFlag)
                        {
                            ButtonSave.Enabled = false;
                            CommonClass.Show("Owner of this hospital is not active. Please activate the User then add units.");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ex.Data.Add("Events", "Page_Load");
                ex.Data.Add("Page", "DemographicDetail.aspx");
                LogManager._stringObject = "DemographicDetail.aspx ---- Page_Load";
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
        protected void Page_PreRender(object sender, EventArgs e)
        {
            try
            {
                if (CommonClass.SessionInfomation == null)
                {
                    RMC.BusinessEntities.BESessionInfomation objectBESessionInfomation = new RMC.BusinessEntities.BESessionInfomation();
                    CommonClass.SessionInfomation = objectBESessionInfomation;
                }

                CommonClass.SessionInfomation.HospitalName = HospitalInformation.HospitalName;
                CommonClass.SessionInfomation.HospitalUnitName = TextBoxUnitName.Text;
            }
            catch (Exception ex)
            {
                ex.Data.Add("Function", "Page_PreRender");
                LogManager._stringObject = "HospitalDemographicinfo.ascx.cs ---- ";
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
        /// Creation Date : June 25, 2009
        /// Modified By : Davinder Kumar.
        /// Modified Date : July 21, 2009.
        /// </summary>
        /// <param name="msg">Message</param>
        /// <param name="color">Color</param>
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
                ex.Data.Add("Class", "DemographicDetail");
                throw ex;
            }
        }


        /// <summary>
        /// Reset all control status.
        /// Created By : Davinder Kumar.
        /// Creation Date : June 25, 2009
        /// Modified By : Davinder Kumar.
        /// Modified Date : July 10, 2009.
        /// </summary>
        private void ResetControls()
        {
            try
            {
                CheckBoxDocByException.Checked = false;
                CheckBoxTCABUnit.Checked = false;
                //TextBoxBedsInHospital.Text = string.Empty;
                TextBoxBedsInUnit.Text = string.Empty;
                //TextBoxDemographic.Text = string.Empty;
                TextBoxElectronicDocumentation.Text = string.Empty;
                TextBoxPatientsPerNurse.Text = string.Empty;
                //TextBoxPharmacyType.Text = string.Empty;
                TextBoxUnitName.Text = string.Empty;
                //TextBoxUnitType.Text = string.Empty;
                TextBoxStartDate.Text = DateTime.Now.ToString("MM/dd/yyyy", new System.Globalization.CultureInfo("en-US"));
                TextBoxEndDate.Text = string.Empty;
                //TextBoxCaptchaText.Text = string.Empty;
                ListBoxUnitType.SelectedIndex = 0;
                ListBoxPharmacyType.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                ex.Data.Add("Function", "ResetControls");
                ex.Data.Add("Class", "Registration");
                throw ex;
            }
        }

        /// <summary>
        /// Save Hospital Demographic Detail In a Data service object.
        /// Created By : Davinder Kumar.
        /// Creation Date : June 25, 2009
        /// Modified By : Davinder Kumar.
        /// Modified Date : July 10, 2009.
        /// </summary>
        /// <returns></returns>
        private RMC.DataService.HospitalDemographicInfo SaveHospitalDemographicDetail()
        {
            _objectHospitalDemographicInfo = new RMC.DataService.HospitalDemographicInfo();
            try
            {
                int userID = 0;
                short shortReturn;
                double doubleReturn;
                DateTime datTimeReturn;
                bool flag = false;
                string userName = string.Empty;
                string unitType = string.Empty, pharmacyType = string.Empty;
                //Check the current user type.
                if (HttpContext.Current.User.IsInRole("admin"))
                {
                    userID = CommonClass.UserInformation.UserID;
                    userName = CommonClass.UserInformation.FirstName + " " + CommonClass.UserInformation.LastName;
                }
                flag = short.TryParse(TextBoxBedsInUnit.Text, out shortReturn);
                if (flag)
                {
                    _objectHospitalDemographicInfo.BedsInUnit = shortReturn;
                }
                else
                {
                    _objectHospitalDemographicInfo.BedsInUnit = 0;
                }

                flag = double.TryParse(TextBoxPatientsPerNurse.Text, out doubleReturn);
                if (flag)
                {
                    _objectHospitalDemographicInfo.BudgetedPatientsPerNurse = doubleReturn;
                }
                else
                {
                    _objectHospitalDemographicInfo.BudgetedPatientsPerNurse = 0.00;
                }
                if (HttpContext.Current.User.IsInRole("superadmin"))
                {
                    _objectHospitalDemographicInfo.CreatedBy = DropDownListOwner.SelectedItem.Text;
                }
                else
                {
                    _objectHospitalDemographicInfo.CreatedBy = userName;
                }
                //_objectHospitalDemographicInfo.Demographic = TextBoxDemographic.Text;
                _objectHospitalDemographicInfo.DocByException = CheckBoxDocByException.Checked;

                flag = short.TryParse(TextBoxElectronicDocumentation.Text, out shortReturn);
                if (flag)
                {
                    _objectHospitalDemographicInfo.ElectronicDocumentation = shortReturn;
                }
                else
                {
                    _objectHospitalDemographicInfo.ElectronicDocumentation = 0;
                }

                flag = DateTime.TryParseExact(TextBoxStartDate.Text, "MM/dd/yyyy", new System.Globalization.CultureInfo("en-US"), System.Globalization.DateTimeStyles.None, out datTimeReturn);
                if (flag)
                {
                    _objectHospitalDemographicInfo.StartDate = datTimeReturn;
                }
                else
                {
                    _objectHospitalDemographicInfo.StartDate = DateTime.Now;
                }

                flag = DateTime.TryParseExact(TextBoxEndDate.Text, "MM-dd-yyyy", new System.Globalization.CultureInfo("en-US"), System.Globalization.DateTimeStyles.None, out datTimeReturn);
                if (flag)
                {
                    _objectHospitalDemographicInfo.IsEndDate = true;
                    _objectHospitalDemographicInfo.EndedDate = datTimeReturn;
                }
                else
                {
                    _objectHospitalDemographicInfo.IsEndDate = false;
                }

                _objectHospitalDemographicInfo.HospitalInfoID = Convert.ToInt32(HospitalInformation.HospitalInfoID);
                _objectHospitalDemographicInfo.HospitalUnitName = TextBoxUnitName.Text;
                _objectHospitalDemographicInfo.IsDeleted = false;
                //_objectHospitalDemographicInfo.PharmacyType = TextBoxPharmacyType.Text;
                _objectHospitalDemographicInfo.TCABUnit = CheckBoxTCABUnit.Checked;
                for (int Index = 0; Index < ListBoxUnitType.Items.Count; Index++)
                {
                    if (ListBoxUnitType.Items[Index].Selected)
                    {
                        unitType += ListBoxUnitType.Items[Index].Text + ",";
                    }
                    else if (Index == (ListBoxUnitType.Items.Count - 1))
                    {
                        unitType += ListBoxUnitType.Items[Index].Text;
                    }
                }
                for (int Index = 0; Index < ListBoxPharmacyType.Items.Count; Index++)
                {
                    if (ListBoxPharmacyType.Items[Index].Selected)
                    {
                        pharmacyType += ListBoxPharmacyType.Items[Index].Text + ",";
                    }
                    else if (Index == (ListBoxPharmacyType.Items.Count - 1))
                    {
                        pharmacyType += ListBoxPharmacyType.Items[Index].Text;
                    }
                }
                _objectHospitalDemographicInfo.PharmacyType = pharmacyType;
                _objectHospitalDemographicInfo.UnitType = unitType;
                _objectHospitalDemographicInfo.IsDynamic = false;
                _objectHospitalDemographicInfo.CreatedDate = DateTime.Now;
            }
            catch (Exception ex)
            {
                ex.Data.Add("Function", "SaveHospitalDemographicDetail");
                ex.Data.Add("Class", "DemographicDetail");
                throw ex;
            }

            return _objectHospitalDemographicInfo;
        }

        /// <summary>
        /// Save Permission in MultiUserDemographicDetail.
        /// Created By : Davinder Kumar.
        /// Creation Date : July 22, 2009.
        /// </summary>
        /// <returns></returns>
        private RMC.DataService.MultiUserDemographic SaveInHospitalDemographicDetail()
        {
            _objectMultiUserDemographic = new RMC.DataService.MultiUserDemographic();
            _objectBSCommon = new BSCommon();
            try
            {
                int userID = 0;
                string userName = string.Empty;
                if (HttpContext.Current.User.IsInRole("admin"))
                {
                    userID = CommonClass.UserInformation.UserID;
                    userName = CommonClass.UserInformation.FirstName + " " + CommonClass.UserInformation.LastName;
                }
                if (HttpContext.Current.User.IsInRole("superadmin"))
                {
                    _objectMultiUserDemographic.CreatedBy = DropDownListOwner.SelectedItem.Text;
                }
                else
                {
                    _objectMultiUserDemographic.CreatedBy = userName;
                }
                _objectMultiUserDemographic.CreatedDate = DateTime.Now;
                _objectMultiUserDemographic.IsDeleted = false;
                _objectMultiUserDemographic.PermissionID = _objectBSCommon.GetPermissionIDByPermissionName("owner");
                if (HttpContext.Current.User.IsInRole("superadmin"))
                {
                    _objectMultiUserDemographic.UserID = Convert.ToInt32(DropDownListOwner.SelectedValue);
                }
                else
                {
                    _objectMultiUserDemographic.UserID = userID;
                }
            }
            catch (Exception ex)
            {
                ex.Data.Add("Function", "SaveHospitalDemographicDetail");
                ex.Data.Add("Class", "DemographicDetail");
                throw ex;
            }

            return _objectMultiUserDemographic;
        }

        #endregion
    }
}