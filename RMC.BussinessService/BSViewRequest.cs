using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RMC.BussinessService
{
    public class BSViewRequest
    {

        #region Variables

        //Data Context Object.
        RMC.DataService.RMCDataContext _objectRMCDataContext = null;

        //Bussiness Service Objects.
        RMC.BussinessService.BSUsers _objectBSUsers = null;
        RMC.BussinessService.BSHospitalDemographicDetail _objectBSHospitalDemographicDetail = null;
        RMC.BussinessService.BSHospitalInfo _objectBSHospitalInfo = null;

        //Generic Objects Of Data Service Classes.
        List<RMC.DataService.ViewRequest> _objectGenericViewRequest = null;

        //Entity Set Objects.
        System.Collections.Generic.List<RMC.BusinessEntities.BEViewRequest> _objectBEViewRequest = null;

        #endregion

        #region Methods

        /// <summary>
        /// Get Request by role.
        /// </summary>
        /// <param name="role">Role of User</param>
        /// <returns>Returns a Generic type list of RMC.BusinessEntities.BEViewRequest type</returns>
        public System.Collections.Generic.List<RMC.BusinessEntities.BEViewRequest> GetRequestByRole(int userID, string role)
        {
            try
            {
                _objectRMCDataContext = new RMC.DataService.RMCDataContext();
                _objectBSUsers = new BSUsers();
                _objectBSHospitalInfo = new BSHospitalInfo();
                _objectBSHospitalDemographicDetail = new BSHospitalDemographicDetail();

                _objectBEViewRequest = new List<RMC.BusinessEntities.BEViewRequest>();

                if (role.ToLower().Trim() == "superadmin")
                {
                    var viewReq = from vr in _objectRMCDataContext.ViewRequests
                                  where vr.IsApproved == false
                                  select vr;

                    foreach (RMC.DataService.ViewRequest objectViewRequest in viewReq)
                    {
                        RMC.BusinessEntities.BEViewRequest objectBEViewRequest = new RMC.BusinessEntities.BEViewRequest();

                        objectBEViewRequest.DemographicDetailID = Convert.ToInt32(objectViewRequest.HospitalDemographicDetailID);
                        objectBEViewRequest.FromUserID = Convert.ToInt32(objectViewRequest.FromUserID);
                        string fromUserName = _objectBSUsers.GetUserNameByUserID(objectViewRequest.FromUserID);
                        if (fromUserName == string.Empty)
                        {
                            continue;
                        }
                        else
                        {
                            objectBEViewRequest.FromUserName = fromUserName;
                        }
                        objectBEViewRequest.HospitalInfoID = Convert.ToInt32(objectViewRequest.HospitalID);
                        string hospitalName = _objectBSHospitalInfo.GetHospitalNameByHospitalID(Convert.ToInt32(objectViewRequest.HospitalID));
                        if (hospitalName == string.Empty)
                        {
                            continue;
                        }
                        else
                        {
                            objectBEViewRequest.HospitalName = hospitalName;
                        }
                        objectBEViewRequest.ToUserID = Convert.ToInt32(objectViewRequest.ToUserID);
                        string toUserName = _objectBSUsers.GetUserNameByUserID(objectViewRequest.ToUserID);
                        if (toUserName == string.Empty)
                        {
                            continue;
                        }
                        else
                        {
                            objectBEViewRequest.ToUserName = toUserName;
                        }
                        if (Convert.ToInt32(objectViewRequest.HospitalDemographicDetailID) > 0)
                        {
                            string unitName = _objectBSHospitalDemographicDetail.GetHospitalUnitNameByHospitalDemographicID(Convert.ToInt32(objectViewRequest.HospitalDemographicDetailID));
                            if (unitName == string.Empty)
                            {
                                continue;
                            }
                            else
                            {
                                objectBEViewRequest.UnitName = unitName;
                            }
                        }
                        else
                        {
                            objectBEViewRequest.UnitName = string.Empty;
                        }
                        objectBEViewRequest.RequestID = objectViewRequest.RequestID;

                        _objectBEViewRequest.Add(objectBEViewRequest);
                    }
                }
                else
                {
                    var viewReq = from vr in _objectRMCDataContext.ViewRequests
                                  where vr.ToUserID == userID && vr.IsApproved == false
                                  select vr;

                    foreach (RMC.DataService.ViewRequest objectViewRequest in viewReq)
                    {
                        RMC.BusinessEntities.BEViewRequest objectBEViewRequest = new RMC.BusinessEntities.BEViewRequest();

                        objectBEViewRequest.DemographicDetailID = Convert.ToInt32(objectViewRequest.HospitalDemographicDetailID);
                        objectBEViewRequest.FromUserID = Convert.ToInt32(objectViewRequest.FromUserID);
                        objectBEViewRequest.FromUserName = _objectBSUsers.GetUserNameByUserID(objectViewRequest.FromUserID);
                        objectBEViewRequest.HospitalInfoID = Convert.ToInt32(objectViewRequest.HospitalID);
                        objectBEViewRequest.HospitalName = _objectBSHospitalInfo.GetHospitalNameByHospitalID(Convert.ToInt32(objectViewRequest.HospitalID));
                        objectBEViewRequest.ToUserID = Convert.ToInt32(objectViewRequest.ToUserID);
                        objectBEViewRequest.ToUserName = _objectBSUsers.GetUserNameByUserID(objectViewRequest.ToUserID);
                        if (Convert.ToInt32(objectViewRequest.HospitalDemographicDetailID) > 0)
                        {
                            objectBEViewRequest.UnitName = _objectBSHospitalDemographicDetail.GetHospitalUnitNameByHospitalDemographicID(Convert.ToInt32(objectViewRequest.HospitalDemographicDetailID));
                        }
                        else
                        {
                            objectBEViewRequest.UnitName = string.Empty;
                        }
                        objectBEViewRequest.RequestID = objectViewRequest.RequestID;

                        _objectBEViewRequest.Add(objectBEViewRequest);
                    }
                }

                return _objectBEViewRequest;
            }
            catch (Exception ex)
            {
                ex.Data.Add("Function", "GetRequestByRole");
                ex.Data.Add("Class", "BSViewRequest");
                throw ex;
            }

            //return _entitySetViewRequest;
        }

        /// <summary>
        /// Get Request by role.
        /// </summary>
        /// <param name="role">Role of User</param>
        /// <returns>Returns a Generic type list of RMC.BusinessEntities.BEViewRequest type</returns>
        public System.Collections.Generic.List<RMC.BusinessEntities.BEViewRequest> GetRequestByRole(string search, int userID, string role, int hospitalDemographicID)
        {
            try
            {
                _objectRMCDataContext = new RMC.DataService.RMCDataContext();
                _objectBSUsers = new BSUsers();
                _objectBSHospitalInfo = new BSHospitalInfo();
                _objectBSHospitalDemographicDetail = new BSHospitalDemographicDetail();

                _objectBEViewRequest = new List<RMC.BusinessEntities.BEViewRequest>();

                if (role.ToLower().Trim() == "superadmin")
                {
                    var viewReq = from vr in _objectRMCDataContext.ViewRequests
                                  where vr.HospitalDemographicDetailID == hospitalDemographicID
                                  && vr.IsApproved == false
                                  select vr;

                    foreach (RMC.DataService.ViewRequest objectViewRequest in viewReq)
                    {
                        RMC.BusinessEntities.BEViewRequest objectBEViewRequest = new RMC.BusinessEntities.BEViewRequest();

                        objectBEViewRequest.DemographicDetailID = Convert.ToInt32(objectViewRequest.HospitalDemographicDetailID);
                        objectBEViewRequest.FromUserID = Convert.ToInt32(objectViewRequest.FromUserID);
                        objectBEViewRequest.FromUserName = _objectBSUsers.GetUserNameByUserID(objectViewRequest.FromUserID);
                        objectBEViewRequest.HospitalInfoID = Convert.ToInt32(objectViewRequest.HospitalID);
                        objectBEViewRequest.HospitalName = _objectBSHospitalInfo.GetHospitalNameByHospitalID(Convert.ToInt32(objectViewRequest.HospitalID));
                        objectBEViewRequest.ToUserID = Convert.ToInt32(objectViewRequest.ToUserID);
                        objectBEViewRequest.ToUserName = _objectBSUsers.GetUserNameByUserID(objectViewRequest.ToUserID);
                        if (Convert.ToInt32(objectViewRequest.HospitalDemographicDetailID) > 0)
                        {
                            objectBEViewRequest.UnitName = _objectBSHospitalDemographicDetail.GetHospitalUnitNameByHospitalDemographicID(Convert.ToInt32(objectViewRequest.HospitalDemographicDetailID));
                        }
                        else
                        {
                            objectBEViewRequest.UnitName = string.Empty;
                        }
                        objectBEViewRequest.RequestID = objectViewRequest.RequestID;

                        _objectBEViewRequest.Add(objectBEViewRequest);
                    }
                }
                else
                {
                    var viewReq = from vr in _objectRMCDataContext.ViewRequests
                                  where vr.ToUserID == userID && vr.IsApproved == false
                                  select vr;

                    foreach (RMC.DataService.ViewRequest objectViewRequest in viewReq)
                    {
                        RMC.BusinessEntities.BEViewRequest objectBEViewRequest = new RMC.BusinessEntities.BEViewRequest();

                        objectBEViewRequest.DemographicDetailID = Convert.ToInt32(objectViewRequest.HospitalDemographicDetailID);
                        objectBEViewRequest.FromUserID = Convert.ToInt32(objectViewRequest.FromUserID);
                        objectBEViewRequest.FromUserName = _objectBSUsers.GetUserNameByUserID(objectViewRequest.FromUserID);
                        objectBEViewRequest.HospitalInfoID = Convert.ToInt32(objectViewRequest.HospitalID);
                        objectBEViewRequest.HospitalName = _objectBSHospitalInfo.GetHospitalNameByHospitalID(Convert.ToInt32(objectViewRequest.HospitalID));
                        objectBEViewRequest.ToUserID = Convert.ToInt32(objectViewRequest.ToUserID);
                        objectBEViewRequest.ToUserName = _objectBSUsers.GetUserNameByUserID(objectViewRequest.ToUserID);
                        if (Convert.ToInt32(objectViewRequest.HospitalDemographicDetailID) > 0)
                        {
                            objectBEViewRequest.UnitName = _objectBSHospitalDemographicDetail.GetHospitalUnitNameByHospitalDemographicID(Convert.ToInt32(objectViewRequest.HospitalDemographicDetailID));
                        }
                        else
                        {
                            objectBEViewRequest.UnitName = string.Empty;
                        }
                        objectBEViewRequest.RequestID = objectViewRequest.RequestID;

                        _objectBEViewRequest.Add(objectBEViewRequest);
                    }
                }

                return _objectBEViewRequest.FindAll(delegate(RMC.BusinessEntities.BEViewRequest objectViewRequest)
                {
                    if ((objectViewRequest.FromUserName.ToLower() + " " + objectViewRequest.HospitalName.ToLower() + " " + objectViewRequest.UnitName.ToLower() + " " + objectViewRequest.ToUserName.ToLower()).Contains(search.ToLower()))
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                });
                //return _objectBEViewRequest;
            }
            catch (Exception ex)
            {
                ex.Data.Add("Function", "GetRequestByRole");
                ex.Data.Add("Class", "BSViewRequest");
                throw ex;
            }

            //return _entitySetViewRequest;
        }

        /// <summary>
        /// Save View Request.
        /// Created By : Davinder Kumar.
        /// Creation Date : July 29, 2009.
        /// </summary>
        /// <param name="objectViewRequest">All requests of type RMC.DataService.ViewRequest</param>
        /// <returns>boolean value</returns>
        public bool InsertViewRequest(List<RMC.DataService.ViewRequest> objectViewRequest)
        {
            bool flag = false;
            try
            {
                _objectRMCDataContext = new RMC.DataService.RMCDataContext();
                if (objectViewRequest.Count() > 0)
                {
                    _objectRMCDataContext.ViewRequests.InsertAllOnSubmit(objectViewRequest);
                    _objectRMCDataContext.SubmitChanges();
                    flag = true;
                }
              
            }
            catch (Exception ex)
            {
                ex.Data.Add("Function", "SaveViewRequest");
                ex.Data.Add("Class", "BSViewRequest");
                throw ex;
            }
            finally
            {
                _objectRMCDataContext.Dispose();
            }

            return flag; 
        }

        /// <summary>
        /// Delete View Request.
        /// </summary>
        /// <param name="requestID">Id of particular request</param>
        /// <returns>boolean value</returns>
        public bool DeleteViewRequest(int requestID)
        {
            bool flag = false;
            try
            {
                _objectRMCDataContext = new RMC.DataService.RMCDataContext();

                RMC.DataService.ViewRequest objectViewRequest = (from vr in _objectRMCDataContext.ViewRequests
                                                                 where vr.RequestID == requestID
                                                                 select vr).FirstOrDefault();
                // Added by Mahesh Sachdeva 
                List<RMC.DataService.ViewRequest> objectGeneric = (from vr in _objectRMCDataContext.ViewRequests where vr.FromUserID == objectViewRequest.FromUserID && vr.HospitalDemographicDetailID == objectViewRequest.HospitalDemographicDetailID select vr).ToList();
                _objectRMCDataContext.ViewRequests.DeleteAllOnSubmit(objectGeneric);
                _objectRMCDataContext.SubmitChanges();
                flag = true;
            }
            catch (Exception ex)
            {
                ex.Data.Add("Function", "SaveViewRequest");
                ex.Data.Add("Class", "BSViewRequest");
                throw ex;
            }
            finally
            {
                _objectRMCDataContext.Dispose();
            }

            return flag;
        }

        /// <summary>
        /// Update View Request List.
        /// Created By : Davinder Kumar.
        /// Creation Date : July 29, 2009.
        /// </summary>
        /// <param name="objectGenericViewRequest">All Requests which is of type RMC.BusinessEntities.ViewRequest</param>
        /// <returns>Returns a Generic type list of RMC.BusinessEntities.ViewRequest type</returns>
        public bool UpdateViewRequest(List<RMC.DataService.ViewRequest> objectGenericViewRequest)
        {
            bool flag = false;
            bool flagHospial = false;
           
            RMC.DataService.MultiUserHospital objectMultiUserHospital = null;
            RMC.DataService.MultiUserDemographic objectMultiUserDemographic = null;
            try
            {
                _objectRMCDataContext = new RMC.DataService.RMCDataContext();
                _objectGenericViewRequest = (from vr in _objectRMCDataContext.ViewRequests
                                             where vr.IsApproved == false
                                             select vr).ToList<RMC.DataService.ViewRequest>();
               List<RMC.DataService.ViewRequest> objectGenericViewRequest1=_objectGenericViewRequest;
                for (int index = 0; index < _objectGenericViewRequest.Count; index++)
                {
                    foreach (RMC.DataService.ViewRequest objectViewRequest in objectGenericViewRequest)
                    {  
                         if(_objectGenericViewRequest[index].FromUserID==objectViewRequest.FromUserID && _objectGenericViewRequest[index].HospitalID==objectViewRequest.HospitalID && _objectGenericViewRequest[index].HospitalDemographicDetailID==objectViewRequest.HospitalDemographicDetailID)
                         {
                         //if (_objectGenericViewRequest[index].RequestID == objectViewRequest.RequestID)
                         //{
                            if (objectViewRequest.HospitalDemographicDetailID == 0)
                            {
                                objectMultiUserHospital = new RMC.DataService.MultiUserHospital();
                                objectMultiUserHospital.UserID = objectViewRequest.FromUserID;
                                objectMultiUserHospital.HospitalInfoID = objectViewRequest.HospitalID;
                                objectMultiUserHospital.PermissionID = objectViewRequest.PermissionId;
                                objectMultiUserHospital.CreatedBy = "";
                                objectMultiUserHospital.CreatedDate = DateTime.Now;
                                objectMultiUserHospital.IsDeleted = false;
                                _objectRMCDataContext.MultiUserHospitals.InsertOnSubmit(objectMultiUserHospital);
                            }
                            else
                            {
                                BSMultiUserDemographic objectBSMultiUserDemographic = new BSMultiUserDemographic();
                                BSHospitalInfo objectBSHospitalInfo = new BSHospitalInfo();
                                objectMultiUserHospital = new RMC.DataService.MultiUserHospital();
                                objectMultiUserDemographic = new RMC.DataService.MultiUserDemographic();
                                bool flagCheckHospital = objectBSHospitalInfo.CheckForHospitalExistenceByUserID(objectViewRequest.FromUserID, Convert.ToInt32(objectViewRequest.HospitalID));
                                if (flagCheckHospital == false)
                                {
                                    objectMultiUserHospital.UserID = objectViewRequest.FromUserID;
                                    objectMultiUserHospital.HospitalInfoID = objectViewRequest.HospitalID;
                                    objectMultiUserHospital.PermissionID = objectViewRequest.PermissionId;
                                    objectMultiUserHospital.CreatedBy = "";
                                    objectMultiUserHospital.CreatedDate = DateTime.Now;
                                    objectMultiUserHospital.IsDeleted = false;
                                    _objectRMCDataContext.MultiUserHospitals.InsertOnSubmit(objectMultiUserHospital);
                                    flagHospial = true;
                                }
                                bool flagCheckMultiUserDemographic=objectBSMultiUserDemographic.CheckUserExistdOrNot(Convert.ToInt32(objectViewRequest.FromUserID),Convert.ToInt32(objectViewRequest.HospitalDemographicDetailID));
                                if (!flagCheckMultiUserDemographic)
                                {
                                    objectMultiUserDemographic.UserID = objectViewRequest.FromUserID;
                                    objectMultiUserDemographic.HospitalDemographicID = objectViewRequest.HospitalDemographicDetailID;
                                    objectMultiUserDemographic.PermissionID = objectViewRequest.PermissionId;
                                    objectMultiUserDemographic.CreatedBy = "";
                                    objectMultiUserDemographic.CreatedDate = DateTime.Now;
                                    objectMultiUserDemographic.IsDeleted = false;
                                    _objectRMCDataContext.MultiUserDemographics.InsertOnSubmit(objectMultiUserDemographic);
                                }
                             }
                           
                                    _objectGenericViewRequest[index].IsApproved = objectViewRequest.IsApproved;
                                   _objectGenericViewRequest[index].PermissionId = objectViewRequest.PermissionId;
                                   _objectRMCDataContext.SubmitChanges();
    
                         
                        }
                    }
                }

              
                flag = true;
            }
            catch (Exception ex)
            {
                ex.Data.Add("Function", "UpdateViewRequest");
                ex.Data.Add("Class", "BSViewRequest");
                throw ex;
            }

            return flag;
        }

        public bool IsUserOwnerOfHospitalUnit(int userID, int hospitalDemographicID)
        {
            bool flag = false;
            _objectRMCDataContext = new RMC.DataService.RMCDataContext();
            RMC.DataService.MultiUserDemographic multUserDemographic = (from mud in _objectRMCDataContext.MultiUserDemographics
                                                                        where mud.HospitalDemographicID == hospitalDemographicID && mud.UserID == userID && mud.PermissionID == 1
                                                                        select mud).FirstOrDefault();
            if (multUserDemographic != null)
            {
                flag = true;
            }

            return flag;
        }

        
        /// <summary>
        /// Update View Request if any Disapproval
        /// </summary>
        /// <param name="userID">Id of User</param>
        /// <param name="toUserID">To whom user</param>
        /// <param name="hospitalDemographicID">ID of Hospital Unit</param>
        /// <returns>boolean value - True/False</returns>
        public bool UpdateViewRequestForDisapproval(int userID, int toUserID, int hospitalDemographicID)
        {
            bool flag = false;
            try
            {
                _objectRMCDataContext = new RMC.DataService.RMCDataContext();

                RMC.DataService.MultiUserDemographic multUserDemographic = (from mud in _objectRMCDataContext.MultiUserDemographics
                                                                            where mud.HospitalDemographicID == hospitalDemographicID && mud.UserID == userID && mud.IsDeleted == false
                                                                            select mud).FirstOrDefault();

                RMC.DataService.HospitalInfo hospitalInfo = (from mud in _objectRMCDataContext.MultiUserDemographics
                                                             where mud.HospitalDemographicID == hospitalDemographicID && mud.UserID == userID && mud.IsDeleted == false
                                                             select mud.HospitalDemographicInfo.HospitalInfo).FirstOrDefault();

                if (multUserDemographic != null)
                {
                    if (multUserDemographic.PermissionID != 1)
                    {
                        _objectRMCDataContext.MultiUserDemographics.DeleteOnSubmit(multUserDemographic);
                    }
                    else
                    {
                        //multUserDemographic.UserID = 1;   // Commented by Mahesh
                        //Added  by mahesh 
                        _objectRMCDataContext.MultiUserDemographics.DeleteOnSubmit(multUserDemographic);
                    }


                    // Commented by Mahesh Sachdeva
                    //RMC.DataService.HospitalDemographicInfo hospitalDemograhicInfo = (from mud in _objectRMCDataContext.MultiUserDemographics
                    //                                                                  where mud.HospitalDemographicID == hospitalDemographicID && mud.UserID == userID && mud.IsDeleted == false
                    //                                                                  select mud.HospitalDemographicInfo).FirstOrDefault();
                    //if (hospitalDemograhicInfo != null)
                    //{
                    //    RMC.DataService.MultiUserHospital multiUserHospital = (from hi in _objectRMCDataContext.MultiUserHospitals
                    //                                                           where hi.HospitalInfoID == hospitalDemograhicInfo.HospitalInfoID && hi.UserID == userID && hi.IsDeleted == false
                    //                                                           select hi).FirstOrDefault();
                    //    if (multiUserHospital != null)
                    //    {
                    //        if (multiUserHospital.PermissionID != 1)
                    //        {

                    //            _objectRMCDataContext.MultiUserHospitals.DeleteOnSubmit(multiUserHospital);
                    //        }
                    //        else
                    //        {
                    //            multiUserHospital.UserID = 1;
                                
                    //            // Commented Added by 
                              
                    //            //RMC.DataService.HospitalInfo objectHospitalInfo = (from hi in _objectRMCDataContext.HospitalDemographicInfos
                    //            //                                                   where hi.HospitalDemographicID == hospitalDemographicID && hi.HospitalInfo.UserID == userID && hi.IsDeleted == false
                    //            //                                                   select hi.HospitalInfo).FirstOrDefault();

                    //            // Added  by Mahesh 
                    //            RMC.DataService.HospitalInfo objectHospitalInfo = (from mud in _objectRMCDataContext.MultiUserDemographics
                    //                                                         where mud.HospitalDemographicID == hospitalDemographicID && mud.UserID == userID && mud.IsDeleted == false
                    //                                                         select mud.HospitalDemographicInfo.HospitalInfo).FirstOrDefault();

                    //            objectHospitalInfo.UserID = 1;
                    //        }

                    //        var viewReq = (from vr in _objectRMCDataContext.ViewRequests
                    //                       where vr.IsApproved == true && vr.FromUserID == userID && vr.HospitalDemographicDetailID == hospitalDemographicID && vr.HospitalID == multiUserHospital.HospitalInfoID
                    //                       select vr).ToList();

                    //        if (viewReq.Count > 0)
                    //        {
                    //            if (multiUserHospital.PermissionID != 1)
                    //            {
                    //                _objectRMCDataContext.ViewRequests.DeleteAllOnSubmit(viewReq);
                    //            }
                    //            else
                    //            {
                    //                viewReq.FirstOrDefault().IsApproved = false;
                    //            }
                    //        }
                    //    }
                   //}

                    _objectRMCDataContext.SubmitChanges();


                    ////Added by Bharat===============
                    //List<RMC.DataService.MultiUserDemographic> GenericmultUserDemographic = (from mud in _objectRMCDataContext.MultiUserDemographics
                    //                                                                         where mud.HospitalDemographicID == hospitalDemographicID && mud.PermissionID == 1 && mud.UserID == 1 && mud.IsDeleted == false
                    //                                                                         select mud).ToList();

                    //if (GenericmultUserDemographic.Count() > 1)
                    //{
                    //    multUserDemographic = (from mud in _objectRMCDataContext.MultiUserDemographics
                    //                           where mud.HospitalDemographicID == hospitalDemographicID && mud.PermissionID == 1 && mud.UserID == 1 && mud.IsDeleted == false
                    //                           select mud).FirstOrDefault();

                    //    _objectRMCDataContext.MultiUserDemographics.DeleteOnSubmit(multUserDemographic);
                    //    _objectRMCDataContext.SubmitChanges();
                    //}
                    ////==============================
                    flag = true;
                }
                else
                {
                    flag = false;
                }
            }
            catch (Exception ex)
            {
                ex.Data.Add("Function", "UpdateViewRequest");
                ex.Data.Add("Class", "BSViewRequest");
                throw ex;
            }
            return flag;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="objectViewRequest"></param>
        /// <returns></returns>
        public bool RequestApprovalExist(RMC.DataService.ViewRequest objectViewRequest)
        {
            try
            {  
                 // Modified by Mahesh to get only unapproved request
                _objectRMCDataContext = new RMC.DataService.RMCDataContext();
                RMC.DataService.ViewRequest objectViewReq = (from vr in _objectRMCDataContext.ViewRequests
                                                             where vr.FromUserID == objectViewRequest.FromUserID &&
                                                                    vr.HospitalDemographicDetailID == objectViewRequest.HospitalDemographicDetailID &&
                                                                    vr.HospitalID == objectViewRequest.HospitalID && vr.ToUserID == objectViewRequest.ToUserID && vr.IsApproved==false 
                                                             select vr).FirstOrDefault();

                if (objectViewReq == null)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
            catch (Exception ex)
            {
                ex.Data.Add("Function", "RequestApprovalExist");
                ex.Data.Add("Class", "BSViewRequest");
                throw ex;
            }
            finally
            {
                _objectRMCDataContext.Dispose();
            }
        }

        #endregion

    }
    //End Of BSViewRequest Classsss
}
//End Of NameSpace
