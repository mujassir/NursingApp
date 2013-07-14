using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;

namespace RMC.BussinessService
{
    public class BSContactUs
    {

        #region Variables

        //Generic Bussiness Entity Object.
        List<RMC.BusinessEntities.BEContactUs> _objectGenericBEContactUs = null;

        //Bussiness Object.
        RMC.BussinessService.BSEmail _objectBSEmail = null;
        RMC.BussinessService.BSUsers _objectBSUsers = null;

        //Data Context Object.
        RMC.DataService.RMCDataContext _objectRMCDataContext = null;

        //Fundamental Data Types.
        bool _flag = false, _emailFlag = false;
        string _toAddress;

        #endregion

        #region Public Methods

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public List<RMC.BusinessEntities.BEContactUs> GetContactUs()
        {
            try
            {

                _objectBSUsers = new BSUsers();
                _objectRMCDataContext = new RMC.DataService.RMCDataContext();

                _objectGenericBEContactUs = (from cs in _objectRMCDataContext.ContactUs
                                             select new RMC.BusinessEntities.BEContactUs
                                             {
                                                 ContactUsID = cs.ContactUsID,
                                                 Message = cs.Message,
                                                 SenderID = cs.SenderID,
                                                 MessageDate = cs.CreationDate.ToString(),
                                                 UserName = (from ui in _objectRMCDataContext.UserInfos
                                                             where ui.UserID == cs.SenderID
                                                             select ui.FirstName + ' ' + ui.LastName).FirstOrDefault(),
                                                 // get email id for sending notification message    
                                                 Email = (from ui in _objectRMCDataContext.UserInfos
                                                          where ui.UserID == cs.SenderID
                                                          select ui.Email).FirstOrDefault(),
                                                 MessageType = "ContactUs"
                                             }).Concat(from n in _objectRMCDataContext.Notifications
                                                       where n.SenderID != 1
                                                       select new RMC.BusinessEntities.BEContactUs
                                                       {
                                                           ContactUsID = n.NotificationID,
                                                           Message = n.Message,
                                                           SenderID = n.SenderID.Value,
                                                           MessageDate = n.CreationDate.ToString(),
                                                           UserName = (from ui in _objectRMCDataContext.UserInfos
                                                                       where ui.UserID == n.SenderID
                                                                       select ui.FirstName + ' ' + ui.LastName).FirstOrDefault(),
                                                           // Get email id for sending notification message
                                                           Email = (from ui in _objectRMCDataContext.UserInfos
                                                                    where ui.UserID == n.SenderID
                                                                    select ui.Email).FirstOrDefault(),
                                                           MessageType = "Notification"
                                                       }).ToList();

                return _objectGenericBEContactUs;


            }
            catch (Exception ex)
            {
                //ex.Data.Add("Function", "GetContactUs");
                //ex.Data.Add("Class", "BSContactUs");
                throw ex;
            }
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="objectContactUs"></param>
        /// <returns></returns>
        public bool InsertContactUs(RMC.DataService.ContactUs objectContactUs)
        {
            try
            {
                BSUsers objectBSUsers = new BSUsers();
                _objectRMCDataContext = new RMC.DataService.RMCDataContext();

                _objectRMCDataContext.ContactUs.InsertOnSubmit(objectContactUs);
                _objectRMCDataContext.SubmitChanges();
                _flag = true;
                if (_flag)
                {
                    RMC.DataService.UserInfo objectUserInfo = objectBSUsers.GetUserInformation(objectContactUs.SenderID);
                    _toAddress = ConfigurationManager.AppSettings["superAdminAddress"].ToString();
                    //Send Email.
                    _objectBSEmail = new BSEmail(objectUserInfo.Email, _toAddress, "Contact Us", objectContactUs.Message, true);
                    _objectBSEmail.SendMail(true, out _emailFlag);

                    return _emailFlag;
                }
                else
                {
                    return _flag;
                }
            }
            catch (Exception ex)
            {
                ex.Data.Add("Function", "InsertContactUs");
                ex.Data.Add("Class", "BSContactUs");
                throw ex;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="contactUsID"></param>
        /// <returns></returns>
        public bool DeleteContactUs(int contactUsID, string messageType)
        {
            try
            {
                _objectRMCDataContext = new RMC.DataService.RMCDataContext();
                if (messageType == "ContactUs")
                {
                    RMC.DataService.ContactUs objectContactUs = (from cu in _objectRMCDataContext.ContactUs
                                                                 where cu.ContactUsID == contactUsID
                                                                 select cu).FirstOrDefault();
                    _objectRMCDataContext.ContactUs.DeleteOnSubmit(objectContactUs);
                }
                else
                {
                    RMC.DataService.Notification objectNotification = (from n in _objectRMCDataContext.Notifications
                                                                       where n.NotificationID == contactUsID
                                                                       select n).FirstOrDefault();

                    _objectRMCDataContext.Notifications.DeleteOnSubmit(objectNotification);
                }
                _objectRMCDataContext.SubmitChanges();
                _flag = true;
            }
            catch (Exception ex)
            {
                ex.Data.Add("Function", "DeleteContactUs");
                ex.Data.Add("Class", "BSContactUs");
                throw ex;
            }
            return _flag;
        }

        #endregion

    }
}
