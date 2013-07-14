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

namespace RMC.Web.Administrator
{
    public partial class SendUserNotification : System.Web.UI.Page
    {
        bool _emailFlag;
        string _bodyText, _fromAddress, _toAddress, _subjectText;
        RMC.BussinessService.BSEmail _objectBSEmail = null;

        string senderID = string.Empty;
        bool _flag = false;
        //Data Service Objects.
        RMC.DataService.Notification _objectNotification = new RMC.DataService.Notification();
        RMC.DataService.ContactUs _objectContactUs = new RMC.DataService.ContactUs();
        RMC.BussinessService.BSReply _objectBSReply = new RMC.BussinessService.BSReply();
        //Bussiness Service Objects.
        RMC.BussinessService.BSNewsLetter _objectBSNewsLetter = new RMC.BussinessService.BSNewsLetter();
        private string MessageType
        {
            get
            {
                return (Request.QueryString["Type"] != null ? Request.QueryString["Type"] : "0");
            }
        }
        private int ContactUsID
        {
            get
            {
                return (Request.QueryString["ID"] != null ? Convert.ToInt32(Request.QueryString["ID"]) : 0);
            }
        }
        private int SenderID
        {
            get
            {
                return (Request.QueryString["ID"] != null ? Convert.ToInt32(Request.QueryString["ID"]) : 0);
            }
        }
        private string Email
        {
            get
            {
                return (Request.QueryString["Email"] != null ? Convert.ToString(Request.QueryString["Email"]) : "0");

            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {

            //if (Request.QueryString["senderID"] != "")
            //{
            //    senderID = Request.QueryString["senderID"];
            //    TextBoxMessage.Text = this.MessageType + " - " + this.ContactUsID;
            //}
            if (!Page.IsPostBack)
            {
                var objmessage = _objectBSReply.GetMessage(SenderID, MessageType);
                foreach (var val in objmessage)
                {
                    TextBoxMessage.Text = val;
                    break;
                }
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
                _objectNotification.SenderID = CommonClass.UserInformation.UserID;
                _objectNotification.Subject = TextBoxSubject.Text;
                _objectNotification.Message = TextBoxMessage.Text;
                _objectNotification.UserID = Convert.ToInt32(Request.QueryString["senderID"]);
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
                    #region Mp. Used to send email notification to user
                    string email = string.Empty;
                    _toAddress = Email;
                    _bodyText = TextBoxMessage.Text;
                    _subjectText = TextBoxSubject.Text;
                    //_fromAddress = Session["UserName"].ToString();
                    _fromAddress = ConfigurationManager.AppSettings["superAdminAddress"].ToString();
                    _objectBSEmail = new RMC.BussinessService.BSEmail(_fromAddress, _toAddress, _subjectText, _bodyText, true);
                    _objectBSEmail.SendMail(true, out _emailFlag);
                    #endregion

                    RMC.BussinessService.BSNewsLetter objBSNewsLetter = new RMC.BussinessService.BSNewsLetter();

                    _flag = objBSNewsLetter.InsertNewLetter(SaveContactUs());
                    if (_flag)
                    {
                        CommonClass.Show("Message Sent Successfully.");
                        ResetControls();
                    }
                    else
                    {
                        CommonClass.Show("Failed To Send Message.");
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
    }
}
