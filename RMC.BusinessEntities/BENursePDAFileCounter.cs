using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RMC.BusinessEntities
{
    public class BENursePDAFileCounter
    {

        #region Properties

        public int HospitalUnitID { get; set; }
        public string Year { get; set; }
        public string Month { get; set; }
        public int TotalFiles { get; set; }
        public int TotalRecords { get; set; }

        #endregion

    }
}
