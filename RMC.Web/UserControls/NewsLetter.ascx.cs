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
using RMC.BusinessEntities;

namespace RMC.Web.UserControls
{
    public partial class NewsLetter : System.Web.UI.UserControl
    {
        bool _emailFlag;
        string _bodyText, _fromAddress, _toAddress, _subjectText;

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
            RMC.BussinessService.BSCommon _objectBSCommon = new RMC.BussinessService.BSCommon() ;
           List<BEUserInfomation> objlistemail = new List<BEUserInfomation>();
            try
            {
                if (Page.IsValid)
                {
                  
                    //string users = string.Empty;
                    //for (int i = 0; i < ListBoxUsers.Items.Count; i++)
                    //{                       
                    //    if (ListBoxUsers.Items[i].Selected)
                    //    {
                    //        string selUser=ListBoxUsers.Items[i].Text.Trim();
                    //        users += users == "" ? selUser : "," + selUser;
                           
                    //    }
                    //}

                    string usersEmail = string.Empty;
                    
                    for (int i = 0; i < ListBoxUsers.Items.Count; i++)
                    {
                        if (ListBoxUsers.Items[i].Selected)
                        {
                            Int32 selUser = Convert.ToInt32(ListBoxUsers.Items[i].Value);
                            objlistemail = _objectBSCommon.GetEmailByUserId(selUser);
                            string email = objlistemail[0].Email.ToString();
                            usersEmail += usersEmail == "" ? email : "," + email;
                        }
                    }
                   
                    #region SM. Used to send email notification to selected users

                    try
                    {
                        string email = string.Empty;
                        RMC.BussinessService.BSEmail _objectBSEmail;
                        _toAddress = usersEmail;//ConfigurationManager.AppSettings["superAdminAddress"].ToString();
                        _bodyText = TextBoxMessage.Text;
                        _subjectText = TextBoxSubject.Text;
                        _fromAddress = ConfigurationManager.AppSettings["superAdminAddress"].ToString();
                        _objectBSEmail = new RMC.BussinessService.BSEmail(_fromAddress, _toAddress, _subjectText, _bodyText, true);
                        _objectBSEmail.SendMail(true, out _emailFlag);


                    }
                    catch (Exception ex)
                    {
                        LogManager._stringObject = "SendMessage.ascx.cs ---- ";
                        LogManager.SetExceptionDetails(ex, LogManager._stringObject, null);
                        LogManager.LogException(ex, LogManager.LoggingCategory.General, LogManager.LoggingLevel.Error);
                        CommonClass.Show(LogManager.ShowErrorDetail(ex));
                    }
                    #endregion


                    RMC.BussinessService.BSNewsLetter objectBSNewLetter = new RMC.BussinessService.BSNewsLetter();
                    List<RMC.DataService.Notification> objectGenericNotification = SaveNotification();

                    if (objectGenericNotification.Count > 0)
                    {
                        flag = objectBSNewLetter.InsertNewLetter(objectGenericNotification);
                        if (flag)
                        {
                            CommonClass.Show("Notification Send Successfully.");
                            //DisplayMessage("Notification Send Successfully.", System.Drawing.Color.Green);
                        }
                        else
                        {
                            CommonClass.Show("Fail to Send Notification.");
                            //DisplayMessage("Fail to Send Notification.", System.Drawing.Color.Red);
                        }
                    }
                    else
                    {
                        CommonClass.Show("Please Select a User.");
                        //DisplayMessage("Please Select a User.", System.Drawing.Color.Red);
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
        private List<RMC.DataService.Notification> SaveNotification()
        {
            try
            {
                List<RMC.DataService.Notification> objectGenericNotification = new List<RMC.DataService.Notification>();

                for (int index = 0; index < ListBoxUsers.Items.Count; index++)
                {
                    if (ListBoxUsers.Items[index].Selected)
                    {
                        RMC.DataService.Notification objectNotification = new RMC.DataService.Notification();

                        objectNotification.CreationDate = DateTime.Now;
                        objectNotification.Message = TextBoxMessage.Text;
                        objectNotification.Subject = TextBoxSubject.Text;
                        objectNotification.UserID = Convert.ToInt32(ListBoxUsers.Items[index].Value);
                        objectNotification.SenderID = CommonClass.UserInformation.UserID;

                        objectGenericNotification.Add(objectNotification);
                    }
                }

                return objectGenericNotification;
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