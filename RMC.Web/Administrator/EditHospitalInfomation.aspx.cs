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

namespace RMC.Web.Administrator
{
    public partial class EditHospitalInfomation : System.Web.UI.Page
    {

        #region Variables

        //Bussiness Service Objects.
        BSHospitalInfo _objectBSHospitalInfo = null;
       
        //Data Service Objects
        RMC.DataService.HospitalInfo _objectHospitalInfo = null;

        //Fundamental Data Types.
        bool _flag;
        int _hospitalInfoID;

        #endregion

        #region Events

        /// <summary>
        /// Button Event to reset Control status.
        /// Created By : Davinder Kumar
        /// Creation Date : July 08, 2009        
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ButtonReset_Click(object sender, EventArgs e)
        {
            try
            {
                _objectHospitalInfo = (RMC.DataService.HospitalInfo)Session["HospitalInfo"];
                ResetControls(_objectHospitalInfo);
                LabelErrorMsg.Text = string.Empty;
                LabelErrorMsg.Visible = false;
            }
            catch (Exception ex)
            {
                ex.Data.Add("Events", "ButtonReset_Click");
                ex.Data.Add("Page", "HospitalInfomation.aspx");
                LogManager._stringObject = "HospitalInfomation.aspx ---- ButtonReset_Click";
                LogManager.SetExceptionDetails(ex, LogManager._stringObject, null);
                LogManager.LogException(ex, LogManager.LoggingCategory.General, LogManager.LoggingLevel.Error);
                DisplayMessage(LogManager.ShowErrorDetail(ex), System.Drawing.Color.Red);
            }
        }

        /// <summary>
        /// Save Button Events to Save Hospital Infomation in a Database.
        /// Created By : Davinder Kumar.
        /// Creation Date : July 08, 2009
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
                    _objectBSHospitalInfo = new BSHospitalInfo();
                    _objectHospitalInfo = UpdateHospitalInfo();

                    _flag = _objectBSHospitalInfo.UpdateHospitalInformation(_objectHospitalInfo);
                    if (_flag)
                    {
                        Session.Remove("HospitalInfoID");
                        Session.Remove("HospitalInfo");
                        Response.Redirect("HospitalList.aspx", false);
                    }
                    else
                    {
                        DisplayMessage("Fail to Save Hospital Infomation.", System.Drawing.Color.Red);
                    }
                }
                else
                {
                    DisplayMessage("Invalid Data.", System.Drawing.Color.Red);
                }
            }
            catch (Exception ex)
            {
                ex.Data.Add("Events", "ButtonSave_Click");
                ex.Data.Add("Page", "HospitalInfomation.aspx");
                LogManager._stringObject = "HospitalInfomation.aspx ---- ButtonSave_Click";
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
        /// To Redirect to Registration Page (Hospital Demographic Detail).
        /// Created By : Davinder Kumar.
        /// Creation Date : July 08, 2009.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ButtonBack_Click(object sender, EventArgs e)
        {
            try
            {
                Session.Remove("HospitalInfoID");
                Session.Remove("HospitalInfo");
                Response.Redirect("HospitalList.aspx", false);
            }
            catch (Exception ex)
            {
                ex.Data.Add("Events", "ButtonBack_Click");
                ex.Data.Add("Page", "HospitalInfomation.aspx");
                LogManager._stringObject = "HospitalInfomation.aspx ---- ButtonBack_Click";
                LogManager.SetExceptionDetails(ex, LogManager._stringObject, null);
                LogManager.LogException(ex, LogManager.LoggingCategory.General, LogManager.LoggingLevel.Error);
                DisplayMessage(LogManager.ShowErrorDetail(ex), System.Drawing.Color.Red);
            }
        }

        /// <summary>
        /// Use to fill state dropdownlist.
        /// Created By : Davinder Kumar.
        /// Creation Date : July 8, 2009.
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

                UpdatePanelState.Update();
            }
            catch (Exception ex)
            {
                ex.Data.Add("Events", "DropDownListCountry_SelectedIndexChanged");
                ex.Data.Add("Page", "HospitalInfomation.aspx");
                LogManager._stringObject = "HospitalInfomation.aspx ---- DropDownListCountry_SelectedIndexChanged";
                LogManager.SetExceptionDetails(ex, LogManager._stringObject, null);
                LogManager.LogException(ex, LogManager.LoggingCategory.General, LogManager.LoggingLevel.Error);
                DisplayMessage(LogManager.ShowErrorDetail(ex), System.Drawing.Color.Red);
            }
        }

        /// <summary>
        /// Input the Parameter value.
        /// Created By : Davinder Kumar.
        /// Creation Date : July 08, 2009.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ObjectDataSourceState_Selecting(object sender, ObjectDataSourceSelectingEventArgs e)
        {
            try
            {
                e.InputParameters["CountryID"] = DropDownListCountry.SelectedValue;
            }
            catch (Exception ex)
            {
                ex.Data.Add("Events", "ObjectDataSourceState_Selecting");
                ex.Data.Add("Page", "HospitalInfomation.aspx");
                LogManager._stringObject = "HospitalInfomation.aspx ---- ObjectDataSourceState_Selecting";
                LogManager.SetExceptionDetails(ex, LogManager._stringObject, null);
                LogManager.LogException(ex, LogManager.LoggingCategory.General, LogManager.LoggingLevel.Error);
                DisplayMessage(LogManager.ShowErrorDetail(ex), System.Drawing.Color.Red);
            }
        }

        /// <summary>
        /// Page Event.
        /// Created By : Davinder Kumar 
        /// Creation Date : July 08, 2009
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!Page.IsPostBack)
                {
                    if (Session["HospitalInfoID"] != null)
                    {
                        _hospitalInfoID = Convert.ToInt32(Session["HospitalInfoID"]);
                        //PopulateData(_hospitalInfoID);
                    }
                    else
                    {
                        //Response.Redirect("HospitalList.aspx", false);
                    }
                }
                //Add Javascript Event to restrict the special character in Textboxes.
                TextBoxHospitalName.Attributes.Add("onKeyDown", "return validateNonNumber(event);");
                //TextBoxChiefNursingOfficer.Attributes.Add("onKeyDown", "return validateNonNumber(event);");
                TextBoxZip.Attributes.Add("onKeyDown", "return FilterAlphaNumericDash(event);");
                TextBoxAddress.Attributes.Add("onKeyDown", "return validateNonNumber(event);");
                TextBoxCity.Attributes.Add("onKeyDown", "return validateNonNumber(event);");
            }
            catch (Exception ex)
            {
                ex.Data.Add("Events", "Page_Load");
                ex.Data.Add("Page", "HospitalInfomation.aspx");
                LogManager._stringObject = "HospitalInfomation.aspx ---- Page_Load";
                LogManager.SetExceptionDetails(ex, LogManager._stringObject, null);
                LogManager.LogException(ex, LogManager.LoggingCategory.General, LogManager.LoggingLevel.Error);
                DisplayMessage(LogManager.ShowErrorDetail(ex), System.Drawing.Color.Red);
            }
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Bind the State DropDownList.
        /// Created By : Davinder Kumar.
        /// Creation Date : July 08, 2009.
        /// </summary>
        /// <param name="state">Generic List of State</param>
        private void BindDropDownListState()
        {
            try
            {
                if (DropDownListState.Items.Count > 1)
                {
                    DropDownListState.Items.Clear();
                    DropDownListState.Items.Add("Select State");
                    DropDownListState.Items[0].Value = 0.ToString();
                }
                
                DropDownListState.DataTextField = "StateName";
                DropDownListState.DataValueField = "StateID";
                DropDownListState.DataSourceID = "ObjectDataSourceState";
                DropDownListState.DataBind();
            }
            catch (Exception ex)
            {
                ex.Data.Add("Function", "BindDropDownListState");
                ex.Data.Add("Class", "EditHospitalInfomation");
                throw ex;
            }
        }

        /// <summary>
        /// Bind the Country DropDownList.
        /// Created By : Davinder Kumar.
        /// Creation Date : July 08, 2009.
        /// </summary>
        /// <param name="state">Generic List of Country</param>
        private void BindDropDownListCountry()
        {
            try
            {
                if (DropDownListCountry.Items.Count > 1)
                {
                    DropDownListCountry.Items.Clear();
                    DropDownListCountry.Items.Add("Select Country");
                    DropDownListCountry.Items[0].Value = 0.ToString();
                }

                DropDownListCountry.DataTextField = "CountryName";
                DropDownListCountry.DataValueField = "CountryID";
                DropDownListCountry.DataSourceID = "ObjectDataSourceCountry" ;
                DropDownListCountry.DataBind();
            }
            catch (Exception ex)
            {
                ex.Data.Add("Function", "BindDropDownListCountry");
                ex.Data.Add("Class", "EditHospitalInfomation");
                throw ex;
            }
        }

        //Use to Display message of Login Failure.
        private void DisplayMessage(string msg, System.Drawing.Color color)
        {
            try
            {
                LabelErrorMsg.Text = msg;
                LabelErrorMsg.ForeColor = color;
                LabelErrorMsg.Visible = true;
            }
            catch (Exception ex)
            {
                ex.Data.Add("Function", "DisplayMessage");
                ex.Data.Add("Class", "EditHospitalInfomation");
                throw ex;
            }
        }

        /// <summary>
        /// Fetch Data by HospitalInfoID from HospitalInfo.
        /// Created By : Davinder Kumar.
        /// Creation Date : July 08, 2009.
        /// </summary>
        /// <param name="hospitalInfoID">Hospital Infomation ID</param>
        private void PopulateData(int hospitalInfoID)
        {
            try
            {
                _objectBSHospitalInfo = new BSHospitalInfo();

                _objectHospitalInfo = _objectBSHospitalInfo.GetHospitalInfoByHospitalInfoID(hospitalInfoID);
                ResetControls(_objectHospitalInfo);
                Session["HospitalInfo"] = _objectHospitalInfo;
            }
            catch (Exception ex)
            {
                ex.Data.Add("Function", "PopulateData");
                ex.Data.Add("Class", "EditHospitalInfomation");
                throw ex;
            }
            finally
            {
                _objectBSHospitalInfo = null;
            }
        }

        //Reset all control status.
        private void ResetControls(RMC.DataService.HospitalInfo hospitalInfo)
        {
            try
            {
                int index = 0;
                TextBoxAddress.Text = hospitalInfo.Address;
                TextBoxCity.Text = hospitalInfo.City;
                TextBoxHospitalName.Text = hospitalInfo.HospitalName;
                TextBoxZip.Text = hospitalInfo.Zip;
                //TextBoxChiefNursingOfficer.Text = hospitalInfo.ChiefNursingOfficer;
                TextBoxCNOPhone.Text = hospitalInfo.ChiefNursingOfficerPhone;
                if (hospitalInfo.StateID > 0)
                {
                    if (DropDownListCountry.Items.Count <= 1)
                    {                        
                        BindDropDownListCountry();                       
                    }
                    foreach (ListItem lstItem in DropDownListCountry.Items)
                    {
                        if (lstItem.Value == hospitalInfo.State.CountryID.ToString())
                        {
                            DropDownListCountry.SelectedIndex = index;
                            break;
                        }
                        index++;
                    }

                    if (DropDownListState.Items.Count <= 1)
                    {                       
                        BindDropDownListState();                        
                    }
                    index = 0;
                    foreach (ListItem lstItem in DropDownListState.Items)
                    {
                        if (lstItem.Value == hospitalInfo.StateID.ToString())
                        {
                            DropDownListState.SelectedIndex = index;
                            break;
                        }
                        index++;
                    }
                }
            }
            catch (Exception ex)
            {
                ex.Data.Add("Function", "Reset");
                ex.Data.Add("Class", "EditHospitalInfomation");
                throw ex;
            }           
        }

        //Save Hospital Infomation In a Data service object.
        private RMC.DataService.HospitalInfo UpdateHospitalInfo()
        {
            _objectHospitalInfo = new RMC.DataService.HospitalInfo();
            try
            {
                _objectHospitalInfo.HospitalInfoID = ((RMC.DataService.HospitalInfo)Session["HospitalInfo"]).HospitalInfoID;
                _objectHospitalInfo.Address = TextBoxAddress.Text;
                //_objectHospitalInfo.ChiefNursingOfficer = TextBoxChiefNursingOfficer.Text;
                _objectHospitalInfo.ChiefNursingOfficerPhone = TextBoxCNOPhone.Text;
                _objectHospitalInfo.City = TextBoxCity.Text;
                _objectHospitalInfo.ModifiedBy = CommonClass.UserInformation.FirstName + " " + CommonClass.UserInformation.LastName;
                _objectHospitalInfo.ModifiedDate = DateTime.Now;
                _objectHospitalInfo.HospitalName = TextBoxHospitalName.Text;
                _objectHospitalInfo.StateID = Convert.ToInt32(DropDownListState.SelectedValue);
                _objectHospitalInfo.Zip = TextBoxZip.Text;
                _objectHospitalInfo.IsActive = false;
            }
            catch (Exception ex)
            {
                ex.Data.Add("Function", "SaveHospitalInfo");
                ex.Data.Add("Class", "EditHospitalInfomation");
                throw ex;
            }

            return _objectHospitalInfo;
        }

        #endregion

    }
    //End Of EditHospitalInfomation Class.
}
//End Of Namespace.
