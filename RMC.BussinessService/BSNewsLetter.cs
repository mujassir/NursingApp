using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RMC.BussinessService
{
    public class BSNewsLetter
    {

        #region Variables

        //Data Context Object.
        RMC.DataService.RMCDataContext _objectRMCDataContext = null;

        //Generic List Object for DataService Object.
        List<RMC.DataService.Notification> objectGenericNotification = null;

        //Fundamental Data Types.
        bool _flag = false;

        #endregion

        #region Public Methods

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public List<RMC.BusinessEntities.BENotification> GetNotificationByUserID(int UserID)
        {
            try
            {
                _objectRMCDataContext = new RMC.DataService.RMCDataContext();
                List<RMC.BusinessEntities.BENotification> objectGenericBENotification = null;
                objectGenericBENotification = (from n in _objectRMCDataContext.Notifications
                                               where n.UserID == UserID
                                               select new RMC.BusinessEntities.BENotification
                                               {
                                                   Subject=n.Subject,
                                                   Message=n.Message,
                                                   SenderID=n.SenderID,
                                                   NotificationID=n.NotificationID,
                                                   UserName = (from q in _objectRMCDataContext.UserInfos
                                                               where q.UserID == n.SenderID && q.IsActive == true && q.IsDeleted == false
                                                               select q.FirstName + ' ' + q.LastName).FirstOrDefault()                                                                 
                                               }
                                             ).ToList();

                return objectGenericBENotification;
            }
            catch (Exception ex)
            {
                ex.Data.Add("Function", "GetNotificationByUserID");
                ex.Data.Add("Class", "BSNewsLetter");
                throw ex;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="objectGenericNotification"></param>
        /// <returns></returns>
        public bool InsertNewLetter(List<RMC.DataService.Notification> objectGenericNotification)
        {
            try
            {
                _objectRMCDataContext = new RMC.DataService.RMCDataContext();

                _objectRMCDataContext.Notifications.InsertAllOnSubmit(objectGenericNotification);
                _objectRMCDataContext.SubmitChanges();

                _flag = true;
            }
            catch (Exception ex)
            {
                ex.Data.Add("Function", "InsertNewLetter");
                ex.Data.Add("Class", "BSNewsLetter");
                throw ex;
            }

            return _flag;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="objectGenericNotification"></param>
        /// <returns></returns>
        public bool InsertNewLetter(RMC.DataService.Notification objectNotification)
        {
            try
            {
                _objectRMCDataContext = new RMC.DataService.RMCDataContext();

                _objectRMCDataContext.Notifications.InsertOnSubmit(objectNotification);
                _objectRMCDataContext.SubmitChanges();

                _flag = true;
            }
            catch (Exception ex)
            {
                ex.Data.Add("Function", "InsertNewLetter");
                ex.Data.Add("Class", "BSNewsLetter");
                throw ex;
            }

            return _flag;
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="notificationID"></param>
        /// <returns></returns>
        public bool DeleteNewLetter(int notificationID)
        {
            try
            {
                _objectRMCDataContext = new RMC.DataService.RMCDataContext();
                RMC.DataService.Notification objectNotification = (from n in _objectRMCDataContext.Notifications
                                                                   where n.NotificationID == notificationID
                                                                   select n).FirstOrDefault();

                _objectRMCDataContext.Notifications.DeleteOnSubmit(objectNotification);
                _objectRMCDataContext.SubmitChanges();

                _flag = true;
            }
            catch (Exception ex)
            {
                ex.Data.Add("Function", "DeleteNewLetter");
                ex.Data.Add("Class", "BSNewsLetter");
                throw ex;
            }

            return _flag;
        }

        #endregion

    }
}
