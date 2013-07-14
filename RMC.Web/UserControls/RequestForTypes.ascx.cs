using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using LogExceptions;

namespace RMC.Web.UserControls
{
    public partial class RequestForTypes : System.Web.UI.UserControl
    {

        #region Variables

        //Bussiness Service Objects.
        RMC.BussinessService.BSRequestForTypes _objectBSRequestForTypes = null;

        #endregion

        #region Properties

        /// <summary>
        /// 
        /// </summary>
        public string Type
        {
            get
            {
                return Convert.ToString(ViewState["Type"]);
            }
            set
            {
                ViewState["Type"] = value;
            }
        }
          
        #endregion
        
        #region Events

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ButtonSendRequest_Click(object sender, EventArgs e)
        {
            bool flag = false;
            try
            {
                _objectBSRequestForTypes = new RMC.BussinessService.BSRequestForTypes();
                if (Page.IsValid)
                {
                    flag = _objectBSRequestForTypes.InsertRequestForTypes(SaveRequestForType());
                    if (flag)
                    {
                        CommonClass.Show("Request Send Successfully.");
                        //DisplayMessage("Request Send Successfully.", System.Drawing.Color.Green);
                    }
                    else
                    {
                        CommonClass.Show("Fail to Send Request.");
                        //DisplayMessage("Fail to Send Request.", System.Drawing.Color.Red);
                    }
                }
            }
            catch (Exception ex)
            {
                ex.Data.Add("Function", "ButtonSendRequest_Click");
                LogManager._stringObject = "RequestForTypes.ascx.cs ---- ";
                LogManager.SetExceptionDetails(ex, LogManager._stringObject, null);
                LogManager.LogException(ex, LogManager.LoggingCategory.General, LogManager.LoggingLevel.Error);
                DisplayMessage(LogManager.ShowErrorDetail(ex), System.Drawing.Color.Red);
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
                LabelErrorMsg.Text = string.Empty;
                LabelErrorMsg.Visible = false;
                PanelErrorMsg.Visible = false;
                LiteralType.Text = Convert.ToString(ViewState["Type"]);
                LiteralHead.Text = Convert.ToString(ViewState["Type"]);
                PopulateData();
            }
            catch (Exception ex)
            {
                ex.Data.Add("Function", "Page_Load");
                LogManager._stringObject = "RequestForTypes.ascx.cs ---- ";
                LogManager.SetExceptionDetails(ex, LogManager._stringObject, null);
                LogManager.LogException(ex, LogManager.LoggingCategory.General, LogManager.LoggingLevel.Error);
                DisplayMessage(LogManager.ShowErrorDetail(ex), System.Drawing.Color.Red);
            }
        }
        
        #endregion

        #region Private Methods
        
        /// <summary>
        /// Use to Display message of Login Failure.
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="color"></param>
        /// <CreatedBy>Raman</CreatedBy>
        /// <CreatedOn>July 20, 2009</CreatedOn>
        private void DisplayMessage(string msg, System.Drawing.Color color)
        {
            try
            {
                LabelErrorMsg.Text = msg;
                LabelErrorMsg.ForeColor = color;
                LabelErrorMsg.Visible = true;
                PanelErrorMsg.Visible = true;
            }
            catch (Exception ex)
            {
                ex.Data.Add("Function", "DisplayMessage");
                LogManager._stringObject = "RequestForTypes.ascx.cs ---- ";
                LogManager.SetExceptionDetails(ex, LogManager._stringObject, null);
                throw ex;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        private void PopulateData()
        {
            try
            {
                if (CommonClass.SessionInfomation != null && HttpContext.Current.User.IsInRole("superadmin"))
                {
                    if (CommonClass.SessionInfomation.HospitalName != string.Empty)
                    {
                        LiteralHospitalName.Text = " (" + CommonClass.SessionInfomation.HospitalName + ")";
                    }
                    else
                    {
                        //LabelHospitalName.Visible = false;
                        LiteralHospitalName.Visible = false;
                    }
                    if (CommonClass.SessionInfomation.HospitalUnitName != string.Empty)
                    {
                        LiteralUnitName.Text = " (" + CommonClass.SessionInfomation.HospitalUnitName + ")";
                    }
                    else
                    {
                        LiteralUnitName.Visible = false;
                        //LabelUnitNameValue.Visible = false;
                    }
                }
                else
                {
                    LiteralHospitalName.Visible = false;
                    //LabelHospitalNameValue.Visible = false;
                    LiteralUnitName.Visible = false;
                    //LabelUnitNameValue.Visible = false;
                }
            }
            catch (Exception ex)
            {
                ex.Data.Add("Function", "PopulateData");
                LogManager._stringObject = "RequestForTypes.ascx.cs ---- ";
                LogManager.SetExceptionDetails(ex, LogManager._stringObject, null);
                throw ex;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private RMC.DataService.RequestForType SaveRequestForType()
        {
            try
            {
                RMC.DataService.RequestForType objectRequestForType = new RMC.DataService.RequestForType();

                if (LiteralHospitalName.Visible == true)
                {
                    objectRequestForType.HospitalName = CommonClass.SessionInfomation.HospitalName;
                }
                if (LiteralUnitName.Visible == true)
                {
                    objectRequestForType.HospitalUnitName = CommonClass.SessionInfomation.HospitalUnitName;
                }
                objectRequestForType.Type = Convert.ToString(ViewState["Type"]);
                objectRequestForType.UserID = CommonClass.UserInformation.UserID;
                objectRequestForType.Value = TextBoxType.Text;
                objectRequestForType.CreatedBy = CommonClass.UserInformation.FirstName + " " + CommonClass.UserInformation.LastName;
                objectRequestForType.CreationDate = DateTime.Now;
                objectRequestForType.MessageDescription = TextBoxMessage.Text;

                return objectRequestForType;
            }
            catch (Exception ex)
            {
                ex.Data.Add("Function", "SaveRequestForType");
                LogManager._stringObject = "RequestForTypes.ascx.cs ---- ";
                LogManager.SetExceptionDetails(ex, LogManager._stringObject, null);
                throw ex;
            }
        }

        #endregion
                
    }
}