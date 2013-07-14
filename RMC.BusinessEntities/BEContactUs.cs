
namespace RMC.BusinessEntities
{
    public class BEContactUs : BEUserInfomation
    {

        #region Properties

        public int ContactUsID { get; set; }

        public string Message { set; get; }

        public int SenderID { set; get; }

        public string MessageType { set; get; }
        public string MessageDate { set; get; }
        public string CreationDate { set; get; }
        public int NotificationId { set; get; }
        #endregion

    }
    //End of Class
}
//End of Namespace
