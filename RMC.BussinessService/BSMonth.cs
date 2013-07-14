using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RMC.BussinessService
{
    public class BSMonth
    {

        #region Public Methods

        public List<RMC.BusinessEntities.BEMonth> GetMonthByHospitalUnitID(int hospitalUnitID, string year)
        {
            try
            {
                List<RMC.BusinessEntities.BEMonth> objectGenericBEMonth = null;
                using (RMC.DataService.RMCDataContext objectRMCDataContext = new RMC.DataService.RMCDataContext())
                {
                    objectGenericBEMonth = (from y in objectRMCDataContext.Months
                                            where y.Year.Year1.Trim() == year.Trim() && y.Year.HospitalDemographicID == hospitalUnitID
                                            select new RMC.BusinessEntities.BEMonth
                                            {
                                                MonthID = y.MonthName,
                                                MonthName = BSCommon.GetMonthName(y.MonthName)
                                            }).ToList();
                }

                return objectGenericBEMonth.OrderBy(o => Convert.ToInt32(o.MonthID)).ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool InsertMonth(RMC.DataService.Month objectMonth, int hospitalUnitID, string year)
        {
            bool flag = false;
            try
            {
                using (RMC.DataService.RMCDataContext objectRMCDataContext = new RMC.DataService.RMCDataContext())
                {
                    int yearID = objectRMCDataContext.Years.SingleOrDefault(s => s.HospitalDemographicID == hospitalUnitID && s.Year1.Trim() == year.Trim()).YearID;
                    objectMonth.YearID = yearID;
                    objectRMCDataContext.Months.InsertOnSubmit(objectMonth);
                    objectRMCDataContext.SubmitChanges();
                    flag = true;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return flag;
        }
        public string getHospitalname(int Yearid)
        {
            using (RMC.DataService.RMCDataContext objectRMCDataContext = new RMC.DataService.RMCDataContext())
            {

                var obj = (from p in objectRMCDataContext.Years
                           join unit in objectRMCDataContext.HospitalDemographicInfos on p.HospitalDemographicID equals unit.HospitalDemographicID
                           join hospital in objectRMCDataContext.HospitalInfos on unit.HospitalInfoID equals hospital.HospitalInfoID
                           where p.YearID == Yearid
                           select hospital.HospitalName).FirstOrDefault();
                return obj;
            }
        }
        public string getUnitname(int Yearid)
        {
            using (RMC.DataService.RMCDataContext objectRMCDataContext = new RMC.DataService.RMCDataContext())
            {

                var obj = (from p in objectRMCDataContext.Years
                           join unit in objectRMCDataContext.HospitalDemographicInfos on p.HospitalDemographicID equals unit.HospitalDemographicID
                           where p.YearID == Yearid
                           select unit.HospitalUnitName).FirstOrDefault();
                return obj;
            }

        }
        public string getYear(int Yearid)
        {
            using (RMC.DataService.RMCDataContext objectRMCDataContext = new RMC.DataService.RMCDataContext())
            {

                var obj = (from p in objectRMCDataContext.Years

                           where p.YearID == Yearid
                           select p.Year1).FirstOrDefault();
                return obj;
            }

        }


        #endregion

    }
}
