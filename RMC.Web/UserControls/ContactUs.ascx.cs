using System;
using System.Collections;
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
    public partial class ContactUs : System.Web.UI.UserControl
    {

        #region Variables
        
        //Data Service Objects.
        RMC.DataService.ContactUs _objectContactUs = null;

        //Bussiness Service Objects.
        RMC.BussinessService.BSContactUs _objectBSContactUs = null; 

        //Fundamental Data Types.
        bool _flag = false;

        #endregion

        #region Events

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
                PanelErrorMsg.Visible = false;
                TextBoxMessage.Focus();
            }
            catch (Exception ex)
            {
                ex.Data.Add("Events", "Page_Load");
                LogManager._stringObject = "ContactUs.ascx.cs ---- ";
                LogManager.SetExceptionDetails(ex, LogManager._stringObject, null);
                LogManager.LogException(ex, LogManager.LoggingCategory.General, LogManager.LoggingLevel.Error);
                //DisplayMessage(LogManager.ShowErrorDetail(ex), System.Drawing.Color.Red);
                CommonClass.Show(LogManager.ShowErrorDetail(ex));
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ButtonSend_Click(object sender, EventArgs e)
        {
            try
            {
                if (Page.IsValid)
                {
                    _objectBSContactUs = new RMC.BussinessService.BSContactUs();

                    _flag = _objectBSContactUs.InsertContactUs(SaveContactUs());
                    if (_flag)
                    {
                        CommonClass.Show("Message Sent Successfully.");
                        //DisplayMessage("Message Send Successfully.", System.Drawing.Color.Green);
                        ResetControls();
                    }
                    else
                    {
                        CommonClass.Show("Failed to Send Message.");
                        //DisplayMessage("Fail to Send Message.", System.Drawing.Color.Red);
                    }
                }
            }
            catch (Exception ex)
            {
                ex.Data.Add("Events", "ButtonSend_Click");
                LogManager._stringObject = "ContactUs.ascx.cs ---- ";
                LogManager.SetExceptionDetails(ex, LogManager._stringObject, null);
                LogManager.LogException(ex, LogManager.LoggingCategory.General, LogManager.LoggingLevel.Error);
                //DisplayMessage(LogManager.ShowErrorDetail(ex), System.Drawing.Color.Red);
                CommonClass.Show(LogManager.ShowErrorDetail(ex));
            }
        } 

        #endregion

        #region Private Methods

        /// <summary>
        /// Use to Display message of Login Failure.       
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="color"></param>
        private void DisplayMessage(string msg, System.Drawing.Color color)
        {
            try
            {
                LabelErrorMsg.Text = msg;
                LabelErrorMsg.ForeColor = color;
                PanelErrorMsg.Visible = true;
            }
            catch (Exception ex)
            {
                ex.Data.Add("Function", "DisplayMessage");
                ex.Data.Add("Class", "GetUsers");
                throw ex;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private RMC.DataService.ContactUs SaveContactUs()
        {
            try
            {
                _objectContactUs = new RMC.DataService.ContactUs();
                               
                _objectContactUs.Message = TextBoxMessage.Text;               
                _objectContactUs.SenderID = CommonClass.UserInformation.UserID;
                _objectContactUs.CreatedBy = CommonClass.UserInformation.FirstName + " " + CommonClass.UserInformation.LastName;
                _objectContactUs.CreationDate = DateTime.Now;

                return _objectContactUs;
            }
            catch (Exception ex)
            {
                ex.Data.Add("Function", "SaveContactUs");
                ex.Data.Add("Class", "ContactUs");
                throw ex;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        private void ResetControls()
        {
            try
            {               
                TextBoxMessage.Text = string.Empty;                
            }
            catch (Exception ex)
            {
                ex.Data.Add("Function", "ResetControls");
                ex.Data.Add("Class", "ContactUs");
                throw ex;
            }
        }

        #endregion

    }
}