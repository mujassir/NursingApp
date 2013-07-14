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

namespace RMC.Web.Users
{
    public partial class SendUserNotification : System.Web.UI.Page
    {
        string senderID = string.Empty;
        bool _flag = false;
        bool _emailFlag;
        string _bodyText, _fromAddress, _toAddress, _subjectText;

        RMC.BussinessService.BSEmail _objectBSEmail = null;

        //Data Service Objects.
        RMC.DataService.Notification _objectNotification = null;

        //Bussiness Service Objects.
        RMC.BussinessService.BSNewsLetter _objectBSNewsLetter = null;
        RMC.BussinessService.BSReplyMessage objReply = new RMC.BussinessService.BSReplyMessage();


        protected void Page_Load(object sender, EventArgs e)
        {

            if (Request.QueryString["senderID"] != "")
            {
                senderID = Request.QueryString["senderID"];
            }
        }

        /// <summary>
        /// Function for set the fields values.
        /// </summary>
        /// <returns></returns>
        private RMC.DataService.Notification SaveContactUs()
        {
            try
            {
                _objectNotification = new RMC.DataService.Notification();
                _objectNotification.UserID = Convert.ToInt32(Request.QueryString["senderID"]);
                _objectNotification.SenderID = CommonClass.UserInformation.UserID;
                _objectNotification.Subject = TextBoxSubject.Text;
                _objectNotification.Message = TextBoxMessage.Text;
                _objectNotification.CreationDate = DateTime.Now;
                return _objectNotification;
            }
            catch (Exception ex)
            {
                ex.Data.Add("Function", "SaveContactUs");
                ex.Data.Add("Class", "Notification");
                throw ex;
            }
        }

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
        /// Method for reset the controls.
        /// </summary>
        private void ResetControls()
        {
            try
            {
                TextBoxMessage.Text = string.Empty;
                TextBoxSubject.Text = string.Empty;
            }
            catch (Exception ex)
            {
                ex.Data.Add("Function", "ResetControls");
                ex.Data.Add("Class", "ContactUs");
                throw ex;
            }
        }

        protected void ButtonSend_Click(object sender, EventArgs e)
        {
            try
            {
                if (Page.IsValid)
                {

                    #region Mp. Used to send email notification to superadmin

                    try
                    {
                        string email = string.Empty;

                        _toAddress = ConfigurationManager.AppSettings["superAdminAddress"].ToString();
                        _bodyText = TextBoxMessage.Text;
                        _subjectText = TextBoxSubject.Text;
                        _fromAddress = Session["UserName"].ToString();
                        _objectBSEmail = new RMC.BussinessService.BSEmail(_fromAddress, _toAddress, _subjectText, _bodyText, true);
                        _objectBSEmail.SendMail(true, out _emailFlag);
                        Session["UserName"] = null;

                    }
                    catch (Exception ex)
                    {
                        LogManager._stringObject = "SendMessage.ascx.cs ---- ";
                        LogManager.SetExceptionDetails(ex, LogManager._stringObject, null);
                        LogManager.LogException(ex, LogManager.LoggingCategory.General, LogManager.LoggingLevel.Error);
                        CommonClass.Show(LogManager.ShowErrorDetail(ex));
                    }
                    #endregion
                    _objectBSNewsLetter = new RMC.BussinessService.BSNewsLetter();

                    _flag = _objectBSNewsLetter.InsertNewLetter(SaveContactUs());


                    if (_flag)
                    {
                        CommonClass.Show("Message Send Successfully.");
                        ResetControls();


                    }


                    else
                    {
                        CommonClass.Show("Fail to Send Message.");
                    }
                }
            }
            catch (Exception ex)
            {
                ex.Data.Add("Events", "ButtonSend_Click");
                LogManager._stringObject = "ContactUs.ascx.cs ---- ";
                LogManager.SetExceptionDetails(ex, LogManager._stringObject, null);
                LogManager.LogException(ex, LogManager.LoggingCategory.General, LogManager.LoggingLevel.Error);
                DisplayMessage(LogManager.ShowErrorDetail(ex), System.Drawing.Color.Red);
            }
        }

        protected void TextBoxMessage_PreRender(object sender, EventArgs e)
        {
            TextBoxMessage.Text = objReply.GetMessage(Convert.ToInt32(Request.QueryString["NotificationID"]));
        }
    }
}
