

namespace RMC.BusinessEntities
{
 public class BELocation
    {
        #region Properties
        public int LocationID { get; set; }
        public string Location { get; set; }
        public int ValidateID { get; set; }
        public bool IsActive { get; set; }
        public string RenameLocation { get; set; }
        #endregion

    }
    //End of BELocation Class
}
