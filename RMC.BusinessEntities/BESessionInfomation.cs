using System.Collections.Generic;

namespace RMC.BusinessEntities
{
    public class BESessionInfomation
    {

        #region Properties

        /// <summary>
        /// Use to Display Error Message.
        /// </summary>
        public string ErrorMessage { set; get; }

        /// <summary>
        /// Save HospitalUploadIDs.
        /// </summary>
        public List<int> HospitalUploadIDs { set; get; }

        /// <summary>
        /// 
        /// </summary>
        public string HospitalName { set; get; }

        /// <summary>
        /// 
        /// </summary>
        public string HospitalUnitName { set; get; }

        /// <summary>
        /// Use in File Uploader to maintain the State.
        /// </summary>
        public string Year { set; get; }

        /// <summary>
        /// Use in File Uploader to maintain the State.
        /// </summary>
        public string Month { set; get; }

        #endregion

    }
    //End of Class
}
//End of Namespace
