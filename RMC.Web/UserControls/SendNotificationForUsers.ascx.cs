using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using LogExceptions;


namespace RMC.Web.UserControls
{

    public partial class SendNotificationForUsers : System.Web.UI.UserControl
    {
        public RMC.BussinessService.BSReplyMessage objReply = new RMC.BussinessService.BSReplyMessage();
        //string sMessage;
        #region Properties

        private int UserID
        {
            get
            {
                return (Request.QueryString["UserID"] != null ? Convert.ToInt32(Request.QueryString["UserID"]) : 0);
            }
        }
       
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
                if (!Page.IsPostBack)
                {
                    ImageButtonBack.PostBackUrl = Request.UrlReferrer.AbsoluteUri;
                   
                }
                //sMessage = objReply.GetMessage();
                TextBoxMessage.Text = "";
                LabelErrorMsg.Text = string.Empty;
                PanelErrorMsg.Visible = false;
               
            }
            catch (Exception ex)
            {
                ex.Data.Add("Function", "Page_Load");
                LogManager._stringObject = "NewsLetter.ascx.cs ---- ";
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
        protected void ButtonSend_Click(object sender, EventArgs e)
        {
            bool flag = false;
            try
            {
                if (Page.IsValid)
                {
                    RMC.BussinessService.BSNewsLetter objectBSNewLetter = new RMC.BussinessService.BSNewsLetter();
                    //List<RMC.DataService.Notification> objectGenericNotification = SaveNotification();

                    flag = objectBSNewLetter.InsertNewLetter(SaveNotification());
                    if (flag)
                    {
                        CommonClass.Show("Notification Send Successfully.");
                        // DisplayMessage("Notification Send Successfully.", System.Drawing.Color.Green);
                    }
                    else
                    {
                        CommonClass.Show("Fail to Send Notification.");
                        //DisplayMessage("Fail to Send Notification.", System.Drawing.Color.Red);
                    }
                }
            }
            catch (Exception ex)
            {
                ex.Data.Add("Function", " protected void Page_Load(object sender, EventArgs e)");
                LogManager._stringObject = "UserProfile.ascx.cs ---- ";
                LogManager.SetExceptionDetails(ex, LogManager._stringObject, null);
                LogManager.LogException(ex, LogManager.LoggingCategory.General, LogManager.LoggingLevel.Error);
                CommonClass.Show(LogManager.ShowErrorDetail(ex));
            }
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Use to Display message.
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
                ex.Data.Add("Class", "NewsLetter.ascx.cs");
                throw ex;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private RMC.DataService.Notification SaveNotification()
        {
            try
            {
                RMC.DataService.Notification objectNotification = new RMC.DataService.Notification();

                objectNotification.CreationDate = DateTime.Now;
                objectNotification.Message = TextBoxMessage.Text;
                objectNotification.Subject = TextBoxSubject.Text;
                objectNotification.UserID = UserID;//Convert.ToInt32(ListBoxUsers.Items[index].Value);
                objectNotification.SenderID = CommonClass.UserInformation.UserID;

                return objectNotification;
            }
            catch (Exception ex)
            {
                ex.Data.Add("Function", "SaveNotification");
                ex.Data.Add("Class", "NewsLetter.ascx.cs");
                throw ex;
            }
        }

        #endregion

    }
}