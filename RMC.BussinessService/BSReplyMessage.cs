using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RMC.BussinessService
{
 public class BSReplyMessage
    {
        //Data Context Object.
        RMC.DataService.RMCDataContext _objectRMCDataContext = null;
      
        public string GetMessage(int id)
        {
            try
            {
                //_objectRMCDataContext = new RMC.DataService.RMCDataContext();
                //var message = (from ns in _objectRMCDataContext.Notifications
                //               select new RMC.BusinessEntities.BEContactUs
                //               {
                //                   Message = ns.Message,
                //                   UserName = (from ui in _objectRMCDataContext.UserInfos
                //                               where ui.UserID == ns.SenderID
                //                               select ui.FirstName + ' ' + ui.LastName).FirstOrDefault()
                //               }).ToString();

                //return message;
                _objectRMCDataContext = new RMC.DataService.RMCDataContext();
                var obj = (from p in _objectRMCDataContext.Notifications
                           
                           where p.NotificationID == id
                           select p.Message).FirstOrDefault();
                return obj;
               

            }
            catch (Exception ex)
            {
               
                throw ex;
            }
        }
   }
}
