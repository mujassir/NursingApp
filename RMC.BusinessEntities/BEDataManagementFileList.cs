using System.Collections.Generic;

namespace RMC.BusinessEntities
{
    public class BEDataManagementFileList
    {

        #region Properties

        public string ConfigName { get; set; }
        public List<BEDataManagementFileReffList> FileList { get; set; }

        #endregion

    }
    //End of BEDataManagementFileList Class

    public class BEDataManagementFileReffList
    {

        #region Properties

        public string FileReff { get; set; }
        public int NurseID { get; set; }
        public int HospitalUploadID { get; set; }
        
        #endregion

    }
    //End of BEDataManagementFileReffList Class
}
//End of Namespace
