using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RMC.BussinessService
{
    public class BSHospitalType
    {

        #region Variables

        //Data Context Object.
        RMC.DataService.RMCDataContext _objectRMCDataContext = null;

        //Fundamental Data Types
        bool _flag = false;

        #endregion

        #region Public Methods

        /// <summary>
        /// 
        /// </summary>
        /// <param name="objectHospitalType"></param>
        /// <returns></returns>
        public bool InsertHospitalUnit(RMC.DataService.HospitalType objectHospitalType)
        {
            try
            {
                _objectRMCDataContext = new RMC.DataService.RMCDataContext();

                _objectRMCDataContext.HospitalTypes.InsertOnSubmit(objectHospitalType);
                _objectRMCDataContext.SubmitChanges();

                _flag = true;
            }
            catch (Exception ex)
            {
                ex.Data.Add("Function", "InsertHospitalUnit");
                ex.Data.Add("Class", "BSHospitalType");
                throw ex;
            }

            return _flag;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="hospitalTypeID"></param>
        /// <returns></returns>
        public bool DeleteHospitalType(int hospitalTypeID)
        {
            try
            {
                _objectRMCDataContext = new RMC.DataService.RMCDataContext();

                RMC.DataService.HospitalType objectHospitalType = (from ht in _objectRMCDataContext.HospitalTypes
                                                                   where ht.HospitalTypeID == hospitalTypeID
                                                                   select ht).FirstOrDefault();

                _objectRMCDataContext.HospitalTypes.DeleteOnSubmit(objectHospitalType);
                _objectRMCDataContext.SubmitChanges();

                _flag = true;
            }
            catch (Exception ex)
            {
                ex.Data.Add("Function", "InsertHospitalUnit");
                ex.Data.Add("Class", "BSHospitalType");
                throw ex;
            }

            return _flag;
        }
        
        #endregion        

    }
}
