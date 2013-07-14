using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RMC.BussinessService
{
    public class BSReply
    {
        public IEnumerable<String> GetMessage(int id, string msgtype)
        {
            List<RMC.BusinessEntities.BEContactUs> objectGetMessage = null;
            List<RMC.BusinessEntities.BENotification> objectGetNotificationMessage = null;
            RMC.DataService.RMCDataContext _objectRMCDataContext = new RMC.DataService.RMCDataContext();
            RMC.DataService.Notification _objnotification = new RMC.DataService.Notification();
            RMC.DataService.ContactUs _objcontactus = new RMC.DataService.ContactUs();

            try
            {
                if (msgtype == "Notification")
                {
                    objectGetNotificationMessage = (from a in _objectRMCDataContext.Notifications

                                                    where a.NotificationID == id
                                                    select new RMC.BusinessEntities.BENotification
                                              {
                                                  Message = a.Message
                                              }).ToList();
                    return objectGetNotificationMessage.Select(m => m.Message);
                }

                else if (msgtype == "ContactUs")

                    objectGetMessage = (from a in _objectRMCDataContext.ContactUs

                                        where a.ContactUsID == id
                                        select new RMC.BusinessEntities.BEContactUs
                                        {
                                            Message = a.Message
                                        }).ToList();
                return objectGetMessage.Select(m => m.Message);

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                objectGetMessage = null;
                objectGetNotificationMessage = null;
            }
        }

    }
}
