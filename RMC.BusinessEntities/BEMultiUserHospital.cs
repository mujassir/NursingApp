using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RMC.BusinessEntities
{
    class BEMultiUserHospital
    {

        public int HospitalInfoID { get; set; }
        public int UserId { get; set; }
        public  int PermissionID { get; set; }
        public bool IsDeleted { get; set; }
        public string CreatedBy { get; set; }
        public System.DateTime CreatedDate { get; set; }
    }
}
