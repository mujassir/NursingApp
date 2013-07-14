using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using RMC.BusinessEntities;
using System.IO;

namespace RMC.BussinessService
{
    public class MaintainSessions
    {
  
        public static List<BEHospitalUnitInfo> SessionHospitalUnitCounterID
        {
            get
            {
                return HttpContext.Current.Session["HospitalUnitCounterIDs"] as List<BEHospitalUnitInfo>;
            }
            set
            {
                HttpContext.Current.Session["HospitalUnitCounterIDs"] = value;
            }
        }

        public static List<RMC.BusinessEntities.BEFunctionNames> SessionFunctionValues
        {
            get
            {
                return HttpContext.Current.Session["FunctionValues"] as List<RMC.BusinessEntities.BEFunctionNames>;
            }
            set
            {
                HttpContext.Current.Session["FunctionValues"] = value;
            }
        }


        public static Int64 NationalDatabaseCount
        {
            get
            {
                return Convert.ToInt64(HttpContext.Current.Session["NationalDatabaseCount"]);
            }
            set
            {
                HttpContext.Current.Session["NationalDatabaseCount"] = value;
            }
        }

        public static List<RMC.BusinessEntities.BEReports> SessionHospitalBenchmarkSummary
        {
            get
            {
                return HttpContext.Current.Session["HospitalBenchmarkSummary"] as List<RMC.BusinessEntities.BEReports>;
            }
            set
            {
                HttpContext.Current.Session["HospitalBenchmarkSummary"] = value;
            }
        }

        public static string SessionProfiles
        {
            get
            {
                return Convert.ToString(HttpContext.Current.Session["ProfileCount"]);
            }
            set
            {
                HttpContext.Current.Session["ProfileCount"] = value;
            }
        }

        public static List<RMC.BusinessEntities.BEValidation> SessionSearchHospitalData
        {
            get
            {
                if (HttpContext.Current.Session["SearchHospitalData"] != null)
                {
                    return HttpContext.Current.Session["SearchHospitalData"] as List<RMC.BusinessEntities.BEValidation>;
                }
                else
                {
                    return null;
                }
            }
            set
            {
                HttpContext.Current.Session["SearchHospitalData"] = value;
            }
        }

        public static bool SessionIsBackNavigation
        {
            get
            {
                bool flag = false;
                if (HttpContext.Current.Session["IsBackNavigation"] != null)
                {
                    flag = Convert.ToBoolean(HttpContext.Current.Session["IsBackNavigation"]);
                    HttpContext.Current.Session["IsBackNavigation"] = false;
                }
                return flag;
            }
            set
            {
                HttpContext.Current.Session["IsBackNavigation"] = value;
            }
        }
    }
}
