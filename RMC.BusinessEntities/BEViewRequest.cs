
namespace RMC.BusinessEntities
{
    public class BEViewRequest
    {

        #region Properties

        public int RequestID { get; set; }
        public int FromUserID { get; set; }
        public int ToUserID { get; set; }
        public int DemographicDetailID { get; set; }
        public int HospitalInfoID { get; set; }
        public string FromUserName { get; set; }
        public string ToUserName { get; set; }
        public string UnitName { get; set; }
        public string HospitalName { get; set; }
        public bool IsApproved { get; set; }

        #endregion

    }
    //End Of BEViewRequest Class
}
//End Of NameSpace
