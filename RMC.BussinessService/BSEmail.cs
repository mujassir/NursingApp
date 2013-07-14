using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Mail;
using System.Configuration;
//using System.Web.Mail;

namespace RMC.BussinessService
{
    public class BSEmail
    {

        #region Variablefs
        string _fromAddress;
        string _toAddress;
        string _subject;
        string _body;
        string _message;
        bool _isHtml;

        #endregion

        #region Constructor

        public BSEmail(string fromAddress, string toAddress, string subject, string body, bool isHtml)
        {
            _fromAddress = fromAddress;
            _toAddress = toAddress;
            _subject = subject;
            _body = body;
            _isHtml = isHtml;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Use to send email.
        /// Created By : Davinder Kumar.
        /// Creation Date : July 22, 2009.
        /// </summary>
        /// <param name="flag"></param>
        /// <returns></returns>
        public string SendMail(bool isHtmlText, out bool flag)
        {
            flag = false;
            try
            {
                MailMessage mailMsg = new MailMessage(_fromAddress, _toAddress, _subject, _body);
                SmtpClient smtpClient = new SmtpClient();
                mailMsg.IsBodyHtml = isHtmlText;
                //smtpClient.EnableSsl = true;
                smtpClient.Port = 25;
                mailMsg.Priority = MailPriority.Normal;
                smtpClient.Send(mailMsg);
                _message = "4";
                flag = true;
            }
            
            catch (Exception ex)
            {
                _message = ex.InnerException.ToString();
                _message = "Error in application. " + ex.InnerException;
            }
            
            _message = "4";

            return _message;
        }
        //public string EmailSendMail()
        //{
        //    const string SERVER = "relay-hosting.secureserver.net";
        //    MailMessage oMail = new System.Web.Mail.MailMessage();
        //    oMail.From = "nida.nidamalik@gmail.com";
        //    oMail.To = "safdaraltaf@yahoo.co.uk";
        //    oMail.Subject = "Test email subject";
        //    oMail.BodyFormat = MailFormat.Html; // enumeration
        //    oMail.Priority = MailPriority.High; // enumeration
        //    oMail.Body = "Sent at: " + DateTime.Now;
        //    SmtpMail.SmtpServer = SERVER;
        //    SmtpMail.Send(oMail);
        //    oMail = null; // free up resources

        //    return "";                
        //}
        #endregion

    }
    //End Of BSEmail Class
}
//End Of NameSpace