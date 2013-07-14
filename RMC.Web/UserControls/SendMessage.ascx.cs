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
using RMC.BusinessEntities;

namespace RMC.Web.UserControls
{
    public partial class SendMessage : System.Web.UI.UserControl
    {
        #region Variable
        bool _flag, _emailFlag;
        string _bodyText, _fromAddress, _toAddress, _subjectText;

        RMC.BussinessService.BSEmail _objectBSEmail = null;
        #endregion

        private int FromUserID
        {
            get
            {
                return (Request.QueryString["FromUserID"] != null ? Convert.ToInt32(Request.QueryString["FromUserID"]) : 0);
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void ButtonSend_Click(object sender, EventArgs e)
        {
            try
            {
                if ((TextBoxMessage.Text != "") && (TextBoxSubject.Text != ""))
                {
                    List<BEUserInfomation> objlistemail = new List<BEUserInfomation>();
                    string email = string.Empty;
                    bool flag = false;
                    // added by Raman on 4 Jan 2011 
                    // email functionality for Request Hospital Unit Access
                    RMC.BussinessService.BSCommon objCommon = new BussinessService.BSCommon();
                    objlistemail = objCommon.GetEmailByUserId(FromUserID);
                    //change by cm
                    //string _toAddress = objlistemail[0].Email.ToString();
                   //// string _toAddress = Request.QueryString["id"];
                    _bodyText = TextBoxMessage.Text;
                    _subjectText = TextBoxSubject.Text;
                    string _fromAddress = ConfigurationManager.AppSettings["fromAddress"].ToString();
                    //cm
                    //string Id = Request.QueryString["senderID"];
                    string Id =Request.QueryString["id"];
                    string[] arrayId = Id.Split(',');

                    for (int i = 0; i < arrayId.Length; i++)
                    {
                        Id= arrayId[i];
                        if (Id.Contains("@"))
                        {
                            Id = Id.Trim();
                            if (Id.Contains("("))
                            {
                                int a = Id.IndexOf("(");
                                int b = Id.IndexOf(")");
                                email = Id.Substring(a + 1, b - a - 1);
                            }
                            else if (Id.Contains("@"))
                            {
                                email = Id;
                            }
                        }

                        if (email != string.Empty)
                        {
                            _toAddress = email;
                        }
                        //end cm
                        _objectBSEmail = new RMC.BussinessService.BSEmail(_fromAddress, _toAddress, _subjectText, _bodyText, true);

                        _objectBSEmail.SendMail(true, out _emailFlag);                        
                    }
                    flag = _emailFlag;
                    if (flag)
                    {
                        //DisplayMessage("Message Sent Successfully.", System.Drawing.Color.Green);
                        CommonClass.Show("Message Sent Successfully.");
                    }
                    else
                    {
                        //DisplayMessage("Failed to Send Message", System.Drawing.Color.Red);
                        CommonClass.Show("Failed to Send Message");
                    }
                }

                else
                {
                    CommonClass.Show("Failed to Send Message");
                }
            }
            catch (Exception ex)
            {
                LogManager._stringObject = "SendMessage.ascx.cs ---- ";
                LogManager.SetExceptionDetails(ex, LogManager._stringObject, null);
                LogManager.LogException(ex, LogManager.LoggingCategory.General, LogManager.LoggingLevel.Error);
                CommonClass.Show(LogManager.ShowErrorDetail(ex));
            }
        }

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
    }
}