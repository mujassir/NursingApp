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

namespace RMC.Web.Users
{
    public partial class DemographicDetail : System.Web.UI.Page
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
                DisplayMessage(LogManager.ShowErrorDetail(ex), System.Drawing.Color.Red);
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

                    CaptchaControlImage.ValidateCaptcha(TextBoxCaptchaText.Text);
                    if (!CaptchaControlImage.UserValidated)
                    {
                        PanelErrorMsg.Visible = true;
                        PanelErrorMsg.ForeColor = System.Drawing.Color.Red;
                        LabelErrorMsg.Text = "Please Enter Correct Code";
                        return;
                    }
                    else
                    {
                        flag = _objectBSHospitalDemgraphicDetail.InsertHospitalDemographicDetail(SaveHospitalDemographicDetail(), SaveInHospitalDemographicDetail());
                        if (flag)
                        {
                            DisplayMessage("Save Hospital Demographic Detail Successfully.", System.Drawing.Color.Green);
                            ResetControls();
                        }
                        else
                        {
                            DisplayMessage("Fail to Save Hospital Demographic Detail.", System.Drawing.Color.Red);
                        }
                    }
                }                
            }
            catch (Exception ex)
            {
                ex.Data.Add("Events", "ButtonSave_Click");
                ex.Data.Add("Page", "DemographicDetail.aspx");
                LogManager._stringObject = "DemographicDetail.aspx ---- ButtonSave_Click";
                LogManager.SetExceptionDetails(ex, LogManager._stringObject, null);
                LogManager.LogException(ex, LogManager.LoggingCategory.General, LogManager.LoggingLevel.Error);
                DisplayMessage(LogManager.ShowErrorDetail(ex), System.Drawing.Color.Red);
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
                if (!Page.IsPostBack)
                {
                    int userID = CommonClass.UserInformation.UserID;
                    GetAllHospitalNames(userID);
                    TextBoxStartDate.Text = DateTime.Now.ToShortDateString();
                }

                //Add Javascript Event to restrict the special character in Textboxes.
                TextBoxStartDate.Attributes.Add("readonly", "readonly");
                TextBoxEndDate.Attributes.Add("readonly", "readonly");
                TextBoxUnitName.Attributes.Add("onKeyDown", "return FilterAlphaNumeric(event);");
                //TextBoxUnitType.Attributes.Add("onKeyDown", "return validateNonNumber(event);");
                TextBoxPharmacyType.Attributes.Add("onKeyDown", "return validateNonNumber(event);");                
            }
            catch (Exception ex)
            {
                ex.Data.Add("Events", "Page_Load");
                ex.Data.Add("Page", "DemographicDetail.aspx");
                LogManager._stringObject = "DemographicDetail.aspx ---- Page_Load";
                LogManager.SetExceptionDetails(ex, LogManager._stringObject, null);
                LogManager.LogException(ex, LogManager.LoggingCategory.General, LogManager.LoggingLevel.Error);
                DisplayMessage(LogManager.ShowErrorDetail(ex), System.Drawing.Color.Red);
            }
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Bind DropDown list with HospitalInfo.
        /// Created By : Davinder Kumar.
        /// Creation Date : June 25, 2009
        /// Modified By : Davinder Kumar.
        /// Modified Date : July 21, 2009.
        /// </summary>
        /// <param name="objectGenericHospitalInfo"></param>
        private void BindDropDownListHospitalNames(List<RMC.BusinessEntities.BEHospitalList> objectGenericHospitalInfo)
        {
            try
            {
                if (DropDownListHospitalName.Items.Count > 1)
                {
                    DropDownListHospitalName.Items.Clear();
                    DropDownListHospitalName.Items.Add("Select Hospital");
                    DropDownListHospitalName.Items[0].Value = 0.ToString();
                    DropDownListHospitalName.Items[0].Selected = true;
                }

                DropDownListHospitalName.DataTextField = "HospitalExtendedName";
                DropDownListHospitalName.DataValueField = "HospitalInfoID";
                DropDownListHospitalName.DataSource = objectGenericHospitalInfo;
                DropDownListHospitalName.DataBind();
            }
            catch (Exception ex)
            {
                ex.Data.Add("Function", "BindDropDownListHospitalNames");
                ex.Data.Add("Class", "DemographicDetail");
                throw ex;
            }
            finally
            {
                objectGenericHospitalInfo = null;
            }
        }

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
        /// Use to fetch all hospital names.
        /// Created By : Davinder Kumar.
        /// Creation Date : June 25, 2009.
        /// Modified By : Davinder Kumar.
        /// Modified Date : July 21, 2009.
        /// </summary>
        private void GetAllHospitalNames(int userID)
        {
            try
            {
                _objectBSHospitalInfo = new BSHospitalInfo();

                _objectGenericBEHospitalList = _objectBSHospitalInfo.GetHospitalNamesByUserID(userID);
                if (_objectGenericBEHospitalList.Count > 0)
                {
                    BindDropDownListHospitalNames(_objectGenericBEHospitalList);
                }
                else
                {
                    DisplayMessage("Hospital Names does not exist.", System.Drawing.Color.Red);
                }
            }
            catch (Exception ex)
            {
                ex.Data.Add("Function", "GetAllHospitalNames");
                ex.Data.Add("Class", "DemographicDetail");
                throw ex;
            }
            finally
            {
                _objectBSHospitalInfo = null;
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
                DropDownListHospitalName.SelectedIndex = 0;
                CheckBoxDocByException.Checked = false;
                CheckBoxTCABUnit.Checked = false;
                //TextBoxBedsInHospital.Text = string.Empty;
                TextBoxBedsInUnit.Text = string.Empty;
                //TextBoxDemographic.Text = string.Empty;
                TextBoxElectronicDocumentation.Text = string.Empty;
                TextBoxPatientsPerNurse.Text = string.Empty;
                TextBoxPharmacyType.Text = string.Empty;
                TextBoxUnitName.Text = string.Empty;
                //TextBoxUnitType.Text = string.Empty;
                TextBoxStartDate.Text = DateTime.Now.ToShortDateString();
                TextBoxEndDate.Text = string.Empty;
                TextBoxCaptchaText.Text = string.Empty;
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
                int intReturn;
                short shortReturn;
                double doubleReturn;
                DateTime datTimeReturn;
                bool flag = false;

                int userID = CommonClass.UserInformation.UserID;
                string userName = CommonClass.UserInformation.FirstName + " " + CommonClass.UserInformation.LastName;
                //flag = int.TryParse(TextBoxBedsInHospital.Text, out intReturn);
                //if (flag)
                //{
                //    _objectHospitalDemographicInfo.BedsInHospital = intReturn;
                //}
                //else
                //{
                //    _objectHospitalDemographicInfo.BedsInHospital = 0;
                //}

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

                _objectHospitalDemographicInfo.CreatedBy = userName;                
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

                flag = DateTime.TryParse(TextBoxStartDate.Text, out datTimeReturn);
                if (flag)
                {
                    _objectHospitalDemographicInfo.StartDate = datTimeReturn;
                }
                else
                {
                    _objectHospitalDemographicInfo.StartDate = DateTime.Now;
                }

                flag = DateTime.TryParse(TextBoxEndDate.Text, out datTimeReturn);
                if (flag)
                {
                    _objectHospitalDemographicInfo.IsEndDate = true;
                    _objectHospitalDemographicInfo.EndedDate = datTimeReturn;
                }
                else
                {
                    _objectHospitalDemographicInfo.IsEndDate = false;
                }
                                
                _objectHospitalDemographicInfo.HospitalInfoID = Convert.ToInt32(DropDownListHospitalName.SelectedValue);
                _objectHospitalDemographicInfo.HospitalUnitName = TextBoxUnitName.Text;
                _objectHospitalDemographicInfo.IsDeleted = false;
                _objectHospitalDemographicInfo.PharmacyType = TextBoxPharmacyType.Text;
                _objectHospitalDemographicInfo.TCABUnit = CheckBoxTCABUnit.Checked;
                //_objectHospitalDemographicInfo.UnitType = TextBoxUnitType.Text;
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
                int userID = CommonClass.UserInformation.UserID;
                string userName = CommonClass.UserInformation.FirstName + " " + CommonClass.UserInformation.LastName;

                _objectMultiUserDemographic.CreatedBy = userName;
                _objectMultiUserDemographic.CreatedDate = DateTime.Now;
                _objectMultiUserDemographic.IsDeleted = false;
                _objectMultiUserDemographic.PermissionID = _objectBSCommon.GetPermissionIDByPermissionName("All");
                _objectMultiUserDemographic.UserID = userID;
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
    //End Of DemographicDetail Class.
}
//End Of Namespace.