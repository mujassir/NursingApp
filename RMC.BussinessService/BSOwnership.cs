using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RMC.BussinessService
{
    public class BSOwnership
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
        public bool InsertOwnershipType(RMC.DataService.OwnerShipType objectOwnershipType)
        {
            try
            {
                _objectRMCDataContext = new RMC.DataService.RMCDataContext();

                _objectRMCDataContext.OwnerShipTypes.InsertOnSubmit(objectOwnershipType);
                _objectRMCDataContext.SubmitChanges();

                _flag = true;
            }
            catch (Exception ex)
            {
                ex.Data.Add("Function", "InsertOwnershipType");
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
        public bool DeleteOwnershipType(int ownershipTypeID)
        {
            try
            {
                _objectRMCDataContext = new RMC.DataService.RMCDataContext();

                RMC.DataService.OwnerShipType objectOwnershipType = (from ht in _objectRMCDataContext.OwnerShipTypes
                                                                     where ht.OwnerShipTypeID == ownershipTypeID
                                                                     select ht).FirstOrDefault();

                _objectRMCDataContext.OwnerShipTypes.DeleteOnSubmit(objectOwnershipType);
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
