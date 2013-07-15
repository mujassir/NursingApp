using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Net.Mail;
using System.Net;

namespace RMC.Web.Administrator
{
    public partial class SendEmail : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        protected void btnsend_Click(object sender, EventArgs e)
        {
            if (SendMail())
            {
                lblmessage.Text = "Email Sent Successfully.";
                lblmessage.Style.Add("color", "green");
                ClearControl(divcontent.Controls);

            }
            else
            {
                lblmessage.Text = "Error in email sending.Please try again.";
                lblmessage.Style.Add("color", "#cc0000");
            }
        }
        bool SendMail()
        {

            string email = txtemailto.Value;
            MailMessage mail = new MailMessage();
            mail.From = new MailAddress("noreply@mrc.com.pk");
            //mail.To.Add(email);

            mail.To.Add(email);

            mail.Subject = "RMC Customer Support";


            mail.Body = @"<div style=background-color:#F4F4F4;padding:15px !important;> <h1 style=font-size:22px;margin-left: -13px;margin-bottom:0;padding:10px;border:0 !important>Email Support<br></h1>";
            mail.Body += "<p>Below is detail of Email query.</p>";
            mail.Body += "<h3>Sender Info</h3>";
            mail.Body += " <table>";
            mail.Body += " <tr><td style='font-weight:bold;'>Name:</td><td>" + txtname.Value + "</td></tr>";
            mail.Body += " <tr><td style='font-weight:bold;'>Email:</td><td>" + txtemail.Value + "</td></tr>";
            mail.Body += " <tr><td style='font-weight:bold';>Phone:</td><td>" + txtphone.Value + "</td></tr>";

            mail.Body += " </table>";
            mail.Body += "<h3>Message</h3>";
            mail.Body += "<p style=line-height:22px;>" + txtcoment.Value + "<p/>";


            mail.Body += "</div>";


            mail.IsBodyHtml = true;
            SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587);
            smtp.Credentials = new NetworkCredential("test@intelliscence.com", "intelliscence");
            smtp.EnableSsl = true;


            try
            {
                smtp.Send(mail);
                ClearControl(this.Controls);
                txtname.Value = "";
                txtemail.Value = "";
                txtemailto.Value = "";
                txtphone.Value = "";
                txtcoment.Value = "";
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }

        }
        public void ClearControl(ControlCollection Controls)
        {
            try
            {
                foreach (Control cntrl in Controls)
                {
                    if (cntrl is TextBox)
                    {
                        ((TextBox)cntrl).Text = "";
                    }
                    ClearControl(cntrl.Controls);
                }
            }
            catch (Exception)
            {

               
            }
        }

        protected string GetVale(string key)
        {
            return System.Configuration.ConfigurationManager.AppSettings[key];
        }
    }
}