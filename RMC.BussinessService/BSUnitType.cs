using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RMC.BussinessService
{
    public class BSUnitType
    {

        #region Variables

        //Data Context Object.
        RMC.DataService.RMCDataContext _objectRMCDataContext = null;

        #endregion

        #region Functions/Methods

        /// <summary>
        /// Get All Unit Type.
        /// </summary>
        /// <returns></returns>
        public List<RMC.DataService.UnitType> GetAllUnitType()
        {
            try
            {
                _objectRMCDataContext = new RMC.DataService.RMCDataContext();

                List<RMC.DataService.UnitType> objectGenericUnitType = (from ut in _objectRMCDataContext.UnitTypes
                                                                        orderby ut.UnitTypeName 
                                                                        select ut).ToList<RMC.DataService.UnitType>();

                return objectGenericUnitType;
            }
            catch (Exception ex)
            {
                ex.Data.Add("Function", "InsertUnitType");
                ex.Data.Add("Class", "BSUserType");
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
        public bool InsertUnitType(RMC.DataService.UnitType objectUnitType)
        {
            bool flag = false;
            try
            {
                _objectRMCDataContext = new RMC.DataService.RMCDataContext();

                _objectRMCDataContext.UnitTypes.InsertOnSubmit(objectUnitType);
                _objectRMCDataContext.SubmitChanges();
                flag = true;
            }
            catch (Exception ex)
            {
                ex.Data.Add("Function", "InsertUnitType");
                ex.Data.Add("Class", "BSUserType");
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
        public bool DeleteUnitType(int unitTypeID)
        {
            bool flag = false;
            try
            {
                _objectRMCDataContext = new RMC.DataService.RMCDataContext();
                var unitType = (from ut in _objectRMCDataContext.UnitTypes
                                where ut.UnitTypeID == unitTypeID
                                select ut).FirstOrDefault();

                _objectRMCDataContext.UnitTypes.DeleteOnSubmit(unitType);
                _objectRMCDataContext.SubmitChanges();
                flag = true;
            }
            catch (Exception ex)
            {
                ex.Data.Add("Function", "DeleteUnitType");
                ex.Data.Add("Class", "BSUserType");
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
