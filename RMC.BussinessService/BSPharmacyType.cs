using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RMC.BussinessService
{
   public  class BSPharmacyType
    {
        #region Variables

        //Data Context Object.
        RMC.DataService.RMCDataContext _objectRMCDataContext = null;

        #endregion

        #region Functions/Methods

        /// <summary>
        /// Get All Pharmacy Type.
        /// </summary>
        /// <returns></returns>
        public List<RMC.DataService.PharmacyType> GetAllPharmacyType()
        {
            try
            {
                _objectRMCDataContext = new RMC.DataService.RMCDataContext();

                List<RMC.DataService.PharmacyType> objectGenericPharmacyType = (from pt in _objectRMCDataContext.PharmacyTypes
                                                                                orderby pt.PharmacyName
                                                                                select pt).ToList<RMC.DataService.PharmacyType>();

                return objectGenericPharmacyType;
            }
            catch (Exception ex)
            {
                ex.Data.Add("Function", "InsertPharmacyType");
                ex.Data.Add("Class", "BSPharmacyType");
                throw ex;
            }
            finally
            {
                _objectRMCDataContext.Dispose();
            }
        }

        /// <summary>
        /// Save Unit Type.
        /// </summary>
        /// <param name="objectUnitType"></param>
        /// <returns></returns>
        public bool InsertPharmacyType(RMC.DataService.PharmacyType objectPharmacyType)
        {
            bool flag = false;
            try
            {
                _objectRMCDataContext = new RMC.DataService.RMCDataContext();
                _objectRMCDataContext.PharmacyTypes.InsertOnSubmit(objectPharmacyType);
                _objectRMCDataContext.SubmitChanges();
                flag = true;
            }
            catch (Exception ex)
           {
                ex.Data.Add("Function", "InsertPharmacyType");
                ex.Data.Add("Class", "BSPharmacyType");
                throw ex;
            }
            finally
            {
                _objectRMCDataContext.Dispose();
            }

            return flag;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pharmacyID"></param>
        /// <returns></returns>
        public bool DeletePharmacyType(int pharmacyTypeID)
        {
            bool flag = false;
            try
            {
                _objectRMCDataContext = new RMC.DataService.RMCDataContext();
                var pharmacyType = (from pt in _objectRMCDataContext.PharmacyTypes
                                    where pt.PharmacyTypeID == pharmacyTypeID
                                    select pt).FirstOrDefault();

                _objectRMCDataContext.PharmacyTypes.DeleteOnSubmit(pharmacyType);
                _objectRMCDataContext.SubmitChanges();
                flag = true;
            }
            catch (Exception ex)
            {
                ex.Data.Add("Function", "InsertPharmacyType");
                ex.Data.Add("Class", "BSPharmacyType");
                throw ex;
            }
            finally
            {
                _objectRMCDataContext.Dispose();
            }

            return flag;
        }

        #endregion


    }
}
