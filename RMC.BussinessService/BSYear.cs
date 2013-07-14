using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RMC.BussinessService
{
    public class BSYear
    {

        #region Static Methods

        /// <summary>
        /// Use to get the no. of years by giving startingYear and noOfYears as parameters
        /// </summary>
        /// <param name="startingYear">From when the year start</param>
        /// <param name="noOfYears">Total no. of years to display</param>
        /// <returns>Returns a Generic type list of RMC.BusinessEntities.BEYear type</returns>
        public static List<RMC.BusinessEntities.BEYear> Years(int startingYear, int noOfYears)
        {
            try
            {
                List<RMC.BusinessEntities.BEYear> objectGenericBEYear = new List<RMC.BusinessEntities.BEYear>();

                for (int index = 0; index < noOfYears; index++)
                {
                    RMC.BusinessEntities.BEYear objectBEYear = new RMC.BusinessEntities.BEYear();

                    objectBEYear.Year = Convert.ToString(startingYear + index);
                    objectBEYear.YearIndex = Convert.ToString(startingYear + index);

                    objectGenericBEYear.Add(objectBEYear);
                }

                return objectGenericBEYear;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Insert Into Year table.
        /// </summary>
        /// <param name="objectYear">Year object of dataservice</param>
        /// <returns></returns>
        public bool InsertYear(RMC.DataService.Year objectYear)
        {
            bool flag = false;
            try
            {
                using (RMC.DataService.RMCDataContext objectRMCDataContext = new RMC.DataService.RMCDataContext())
                {
                    objectRMCDataContext.Years.InsertOnSubmit(objectYear);
                    objectRMCDataContext.SubmitChanges();
                    flag = true;                                                
                }

                return flag;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string getHospitalname(int unitid)
        {
            using (RMC.DataService.RMCDataContext objectRMCDataContext = new RMC.DataService.RMCDataContext())
            {
                
                var obj= (from p  in objectRMCDataContext.HospitalDemographicInfos
                          join q in objectRMCDataContext.HospitalInfos on p.HospitalInfoID equals q.HospitalInfoID 
                          where p.HospitalDemographicID== unitid
                             select q.HospitalName).FirstOrDefault();
                return obj;
            }
             
        }
        public string getUnitname(int unitid)
        {
            using (RMC.DataService.RMCDataContext objectRMCDataContext = new RMC.DataService.RMCDataContext())
            {

                var obj = (from p in objectRMCDataContext.HospitalDemographicInfos
                           
                           where p.HospitalDemographicID == unitid
                           select p.HospitalUnitName).FirstOrDefault();
                return obj;
            }

        }
        public Int32 getHospitalId(int unitid)
        {
            using (RMC.DataService.RMCDataContext objectRMCDataContext = new RMC.DataService.RMCDataContext())
            {

                var obj = (from p in objectRMCDataContext.HospitalDemographicInfos
                           join q in objectRMCDataContext.HospitalInfos on p.HospitalInfoID equals q.HospitalInfoID
                           where p.HospitalDemographicID == unitid
                           select q.HospitalInfoID).FirstOrDefault();
                return obj;
            }

        }
        #endregion

    }
    //End of Class
}
//End of Namespace