using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RMC.BussinessService
{
    public class BSRequestForTypes
    {

        #region Variables

        //Data Context Objects.
        RMC.DataService.RMCDataContext _objectRMCDataContext = null; 

        //Fundamental Data Types.
        bool _flag = false;

        #endregion

        #region Public Methods

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public List<RMC.BusinessEntities.BERequestForTypes> GetAllRequest()
        {
            try
            {
                _objectRMCDataContext = new RMC.DataService.RMCDataContext();
                List<RMC.BusinessEntities.BERequestForTypes> objectGenericBERequestForTypes = new List<RMC.BusinessEntities.BERequestForTypes>();

                objectGenericBERequestForTypes = (from rt in _objectRMCDataContext.RequestForTypes
                                                  select new RMC.BusinessEntities.BERequestForTypes
                                                  {
                                                      HospitalName = rt.HospitalName,
                                                      HospitalUnitName = rt.HospitalUnitName,
                                                      RequestID = rt.RequestID,
                                                      Type = rt.Type,
                                                      UserID = rt.UserID,
                                                      Value = rt.Value,
                                                      MessageDescription = rt.MessageDescription,
                                                      UserName = (from ui in _objectRMCDataContext.UserInfos
                                                                  where ui.UserID == rt.UserID 
                                                                  select ui.FirstName + " " + ui.LastName).FirstOrDefault()
                                                  }).ToList();

                return objectGenericBERequestForTypes;
            }
            catch (Exception ex)
            {
                ex.Data.Add("Function", "GetAllRequest");
                ex.Data.Add("Class", "BSRequestForTypes");
                throw ex;
            }
            finally
            {
                _objectRMCDataContext.Dispose();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public RMC.BusinessEntities.BERequestForTypes GetRequestByRequestID(int requestID)
        {
            try
            {
                _objectRMCDataContext = new RMC.DataService.RMCDataContext();
                RMC.BusinessEntities.BERequestForTypes objectBERequestForTypes = new RMC.BusinessEntities.BERequestForTypes();

                objectBERequestForTypes = (from rt in _objectRMCDataContext.RequestForTypes
                                                  where rt.RequestID == requestID
                                                  select new RMC.BusinessEntities.BERequestForTypes
                                                  {
                                                      HospitalName = rt.HospitalName,
                                                      HospitalUnitName = rt.HospitalUnitName,
                                                      RequestID = rt.RequestID,
                                                      Type = rt.Type,
                                                      UserID = rt.UserID,
                                                      Value = rt.Value,
                                                      UserName = (from ui in _objectRMCDataContext.UserInfos
                                                                  where ui.UserID == rt.UserID
                                                                  select ui.FirstName + " " + ui.LastName).FirstOrDefault()
                                                  }).FirstOrDefault();

                return objectBERequestForTypes;
            }
            catch (Exception ex)
            {
                ex.Data.Add("Function", "GetRequestByRequestID");
                ex.Data.Add("Class", "BSRequestForTypes");
                throw ex;
            }
            finally
            {
                _objectRMCDataContext.Dispose();
            }
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="objectRequestForType"></param>
        /// <returns></returns>
        public bool InsertRequestForTypes(RMC.DataService.RequestForType objectRequestForType)
        {
            try
            {
                _objectRMCDataContext = new RMC.DataService.RMCDataContext();

                _objectRMCDataContext.RequestForTypes.InsertOnSubmit(objectRequestForType);
                _objectRMCDataContext.SubmitChanges();
                _flag = true;
            }
            catch (Exception ex)
            {
                ex.Data.Add("Function", "InsertRequestForTypes");
                ex.Data.Add("Class", "BSRequestForTypes");
                throw ex;
            }
            finally
            {
                _objectRMCDataContext.Dispose();
            }

            return _flag;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="requestID"></param>
        /// <returns></returns>
        public bool DeleteRequestForTypes(int requestID)
        {
            try
            {
                _objectRMCDataContext = new RMC.DataService.RMCDataContext();
                RMC.DataService.RequestForType objectRequestForType = (from rt in _objectRMCDataContext.RequestForTypes
                                                                       where rt.RequestID == requestID
                                                                       select rt).FirstOrDefault();

                _objectRMCDataContext.RequestForTypes.DeleteOnSubmit(objectRequestForType);
                _objectRMCDataContext.SubmitChanges();
                _flag = true;
            }
            catch (Exception ex)
            {
                ex.Data.Add("Function", "DeleteRequestForTypes");
                ex.Data.Add("Class", "BSRequestForTypes");
                throw ex;
            }
            finally
            {
                _objectRMCDataContext.Dispose();
            }

            return _flag;
        }

        #endregion

    }
}
