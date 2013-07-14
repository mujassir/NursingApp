using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RMC.BussinessService
{
    public class BSUtility
    {
        public string getHospitalname(int unitid)
        {
            using (RMC.DataService.RMCDataContext objectRMCDataContext = new RMC.DataService.RMCDataContext())
            {

                var obj = (from p in objectRMCDataContext.HospitalDemographicInfos
                           join q in objectRMCDataContext.HospitalInfos on p.HospitalInfoID equals q.HospitalInfoID
                           where p.HospitalDemographicID == unitid
                           select q.HospitalName).FirstOrDefault();
                return obj;
            }

        }
        public string UtilitysaveFile()
        {

            return "asdf";
        }
    }
}
