using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using RMC.DataService;
using RMC.BusinessEntities;
using System.IO;
using LogExceptions;

namespace RMC.BussinessService
{
    public class BSSDAValidation
    {

        #region Variables

        //Data Context Object.
        RMC.DataService.RMCDataContext _objectRMCDataContext = null;

        //Data Service Objects.
        BSImportData _objectDSImportData = null;
        RMC.DataService.NursePDAInfo _objectNursePDAInfo = null;
        RMC.DataService.NursePDADetail _objectNursePDADetail = null;
        RMC.DataService.NursePDASpecialType _objectNursePDASpecialType = null;
        RMC.DataService.Location _objectLocation = null;
        RMC.DataService.Activity _objectActivity = null;
        RMC.DataService.SubActivity _objectSubActivity = null;
        //RMC.DataService.CategoryGroup _objectCategoryGroup = null;
        RMC.DataService.ResourceRequirement _objectResourceRequirement = null;
        //RMC.DataService.ValueAddedType _objectValueAddedType = null;
        RMC.DataService.UserInfo _objectUserInfo = null;

        //Business Entity Objects.
        List<BEImportData> _genricBEImportData = null;
        System.Data.Linq.EntitySet<NursePDADetail> _entityNursePDADetail = null;
        System.Data.Linq.EntitySet<NursePDASpecialType> _entityNursePDASpecialType = null;
        List<LastLocation> _genericLastLocation = null;
        List<Location> _genericLocation = null;
        List<Activity> _genericActivity = null;
        List<SubActivity> _genericSubActivity = null;
        List<RMC.DataService.Validation> _objectGenericValidation = null;
        //List<CategoryGroup> _genericCategoryGroup = null;
        List<ResourceRequirement> _genericResourceRequirement = null;
        //List<ValueAddedType> _genericValueAddedType = null;

        #endregion

        #region Public Functions/Methods

        /// <summary>
        /// Check the File if any data which is not Validate it's return true to show Error
        /// Created By : Davinder Kumar.
        /// Creation Date : July 14, 2009.
        /// </summary>
        /// <param name="entityNursePDADetail"></param>
        /// <returns></returns>
        public bool CheckFileDataError(System.Data.Linq.EntitySet<NursePDADetail> entityNursePDADetail)
        {
            bool flag = false;
            try
            {
                foreach (NursePDADetail objectNursePDADetail in entityNursePDADetail)
                {
                    if (objectNursePDADetail.ActivityID == 0 || objectNursePDADetail.CategoryGroupID == 0 ||
                        objectNursePDADetail.LastLocationID == 0 || objectNursePDADetail.LocationID == 0 ||
                        objectNursePDADetail.ResourceRequirementID == 0 || objectNursePDADetail.SubActivityID == 0
                        || objectNursePDADetail.TypeID == 0)
                    {
                        flag = true;
                        break;
                    }
                }
            }
            catch (Exception ex)
            {
                ex.Data.Add("Function", "CheckFileDataError");
                ex.Data.Add("Class", "BSDataValidation");
                throw ex;
            }

            return flag;
        }

        /// <summary>
        /// Save Data after Validation.
        /// Created By : Davinder Kumar.
        /// Creation Date : July 14, 2009.
        /// </summary>
        /// <param name="entityNursePDADetail"></param>
        /// <returns></returns>
        public bool SaveFileDataInDatabase(System.Data.Linq.EntitySet<NursePDADetail> entityNursePDADetail, RMC.DataService.HospitalUpload hospitalUpload)
        {
            bool flag = false;
            try
            {
                _objectRMCDataContext = new RMCDataContext();
                int hospitalUploadID = 0;
                _objectRMCDataContext.HospitalUploads.InsertOnSubmit(hospitalUpload);
                _objectRMCDataContext.SubmitChanges();
                hospitalUploadID = hospitalUpload.HospitalUploadID;
                _objectRMCDataContext.Dispose();

                _objectRMCDataContext = new RMCDataContext();
                _objectNursePDAInfo.HospitalUploadID = hospitalUploadID;
                _objectRMCDataContext.NursePDAInfos.InsertOnSubmit(_objectNursePDAInfo);
                if (entityNursePDADetail.Count > 0)
                {
                    _objectNursePDAInfo.NursePDADetails.AddRange(entityNursePDADetail);
                }
                _objectRMCDataContext.SubmitChanges();
                flag = true;
            }
            catch (Exception ex)
            {
                ex.Data.Add("Function", "SaveFileDataInDatabase");
                ex.Data.Add("Class", "BSDataValidation");
                throw ex;
            }
            finally
            {
                _objectRMCDataContext.Dispose();
            }

            return flag;
        }

        /// <summary>
        /// Save Data after Validation. Also Saves Data in NursePDASpecialType Table
        /// </summary>
        public bool SaveFileDataInDatabaseNew(System.Data.Linq.EntitySet<NursePDADetail> entityNursePDADetail, RMC.DataService.HospitalUpload hospitalUpload, System.Data.Linq.EntitySet<NursePDASpecialType> entityNursePDASpecialType)
        {
            bool flag = false;
            try
            {
                _objectRMCDataContext = new RMCDataContext();
                int hospitalUploadID = 0;
                _objectRMCDataContext.HospitalUploads.InsertOnSubmit(hospitalUpload);
                _objectRMCDataContext.SubmitChanges();
                hospitalUploadID = hospitalUpload.HospitalUploadID;
                _objectRMCDataContext.Dispose();

                _objectRMCDataContext = new RMCDataContext();
                _objectNursePDAInfo.HospitalUploadID = hospitalUploadID;
                _objectRMCDataContext.NursePDAInfos.InsertOnSubmit(_objectNursePDAInfo);
                if (entityNursePDADetail.Count > 0)
                {
                    _objectNursePDAInfo.NursePDADetails.AddRange(entityNursePDADetail);
                }
                if (entityNursePDASpecialType.Count > 0)
                {
                    _objectNursePDAInfo.NursePDASpecialTypes.AddRange(entityNursePDASpecialType);
                }
                _objectRMCDataContext.SubmitChanges();
                flag = true;
            }
            catch (Exception ex)
            {
                ex.Data.Add("Function", "SaveFileDataInDatabase");
                ex.Data.Add("Class", "BSDataValidation");
                throw ex;
            }
            finally
            {
                _objectRMCDataContext.Dispose();
            }

            return flag;
        }

        /// <summary>
        /// Validate Data which fetch from sda file.
        /// Created By : Davinder Kumar.
        /// Creation Date : June 30, 2009.
        /// Modified By : Davinder Kumar.
        /// Modified Date : July 14, 2009.
        /// </summary>
        /// <param name="fileName"></param>
        public bool SaveFileData(string fileName, string originalFileName, int hospitalDemographicID, RMC.DataService.HospitalUpload hospitalUpload, out bool errorFlag)
        {
            bool flag = false, flagNonStandardFiles = false;
            errorFlag = false;
            try
            {
                List<RMC.BusinessEntities.BEImportData> genericBEImportDataNurseInfo = null;
                List<RMC.BusinessEntities.BEImportData> genericBEImportDataTimeless = null;
                System.Data.Linq.EntitySet<RMC.DataService.NursePDADetail> entitySetNursePDADetail = null;
                System.Data.Linq.EntitySet<RMC.DataService.NursePDASpecialType> entitySetNursePDASpecialType = null;
                _objectRMCDataContext = new RMCDataContext();

                _objectDSImportData = new BSImportData();
                //Get Data From Sda file in Data Service.
                _genricBEImportData = _objectDSImportData.ImportData(fileName, originalFileName, hospitalUpload.Year, hospitalUpload.Month, out flagNonStandardFiles);
                //Extract Nurse and PDA Device Information.
                //genericBEImportDataNurseInfo = _genricBEImportData.FindAll(delegate(RMC.BusinessEntities.BEImportData obj) { return obj.KeyDataSequence == 0.ToString() && obj.Header != string.Empty ? true : false; });
                genericBEImportDataNurseInfo = _genricBEImportData.FindAll(delegate(RMC.BusinessEntities.BEImportData obj) { return obj.KeyDataSequence == 0.ToString() ? true : false; });

                //Validate Nurse Infomation.
                if (genericBEImportDataNurseInfo.Count > 0)
                {
                    _objectNursePDAInfo = ValidateNursePDAInfo(genericBEImportDataNurseInfo, hospitalDemographicID);
                    //Validate and Save Nurse PDA Detail.
                    //Special function call for RMC Phase VI
                    //if (_genricBEImportData[0].ConfigName == "RMC Phase VI" || _genricBEImportData[1].ConfigName == "RMC Phase VI" || _genricBEImportData[2].ConfigName == "RMC Phase VI")
                    if (_genricBEImportData[0].ConfigName == "RMC Phase VI")
                    {
                        entitySetNursePDADetail = ValidateNursePDADetailForRMCPhaseVI(_genricBEImportData, out errorFlag);
                        //Extract those data which have Timeless = 1
                        genericBEImportDataTimeless = _genricBEImportData.FindAll(delegate(RMC.BusinessEntities.BEImportData obj) { return obj.Timeless == 1.ToString(); });
                        entitySetNursePDASpecialType = ValidateNursePDASpecialType(genericBEImportDataTimeless);
                    }
                    else
                    {
                        entitySetNursePDADetail = ValidateNursePDADetail(_genricBEImportData, out errorFlag);
                    }
                    _objectNursePDAInfo.IsErrorInDetailData = errorFlag;
                    _objectRMCDataContext.Dispose();
                }
                int max = Convert.ToInt16(_genricBEImportData.Max(m => m.KeyDataSequence));
                if (max > 1)
                {
                    //Special case for RMC Phase VI (SaveFileDataInDatabaseNew)
                    //if (_genricBEImportData[0].ConfigName == "RMC Phase VI" || _genricBEImportData[1].ConfigName == "RMC Phase VI" || _genricBEImportData[2].ConfigName == "RMC Phase VI")
                    if (_genricBEImportData[0].ConfigName == "RMC Phase VI")
                    {
                        flag = SaveFileDataInDatabaseNew(entitySetNursePDADetail, hospitalUpload, entitySetNursePDASpecialType);
                    }
                    else
                    {
                        flag = SaveFileDataInDatabase(entitySetNursePDADetail, hospitalUpload);
                    }

                }
                else
                {
                    if (HttpContext.Current.Session["NonValidFileName"] != null)
                    {
                        HttpContext.Current.Session["NonValidFileName"] = Convert.ToString(HttpContext.Current.Session["NonValidFileName"]) + @" \n " + originalFileName;
                        //HttpContext.Current.Session["NonValidFileName"] = Convert.ToString(BSSerialization.Deserialize<string>(HttpContext.Current.Session["NonValidFileName"] as MemoryStream)) + @" \n " + originalFileName;
                    }
                    else
                    {
                        HttpContext.Current.Session["NonValidFileName"] = originalFileName;
                        //HttpContext.Current.Session["NonValidFileName"] = BSSerialization.Serialize(originalFileName);
                    }
                }
                //Update NursePDA Information.
            }
            catch (Exception ex)
            {
                ex.Data.Add("Function", "SaveFileData");
                ex.Data.Add("Class", "BSSDAValidation");
                throw ex;
            }

            return flag;
        }

        public bool SaveFileData(string fileName, string originalFileName, int hospitalDemographicID, RMC.DataService.HospitalUpload hospitalUpload, string configName, out bool errorFlag)
        {
            bool flag = false, flagNonStandardFiles = false;
            errorFlag = false;
            try
            {
                List<RMC.BusinessEntities.BEImportData> genericBEImportDataNurseInfo = null;
                List<RMC.BusinessEntities.BEImportData> genericBEImportDataTimeless = null;
                System.Data.Linq.EntitySet<RMC.DataService.NursePDADetail> entitySetNursePDADetail = null;
                System.Data.Linq.EntitySet<RMC.DataService.NursePDASpecialType> entitySetNursePDASpecialType = null;
                _objectRMCDataContext = new RMCDataContext();

                _objectDSImportData = new BSImportData();
                //Get Data From Sda file in Data Service.
                _genricBEImportData = _objectDSImportData.ImportData(fileName, originalFileName, hospitalUpload.Year, hospitalUpload.Month, configName, out flagNonStandardFiles);
                //Extract Nurse and PDA Device Information.
                genericBEImportDataNurseInfo = _genricBEImportData.FindAll(delegate(RMC.BusinessEntities.BEImportData obj) { return obj.KeyDataSequence == 0.ToString() && obj.Header != string.Empty ? true : false; });
                //Validate Nurse Infomation.
                if (genericBEImportDataNurseInfo.Count > 0)
                {
                    _objectNursePDAInfo = ValidateNursePDAInfo(genericBEImportDataNurseInfo, hospitalDemographicID);
                    //Validate and Save Nurse PDA Detail.
                    //Special function call for RMC Phase VI
                    if (_genricBEImportData[0].ConfigName == "RMC Phase VI" || _genricBEImportData[1].ConfigName == "RMC Phase VI" || _genricBEImportData[2].ConfigName == "RMC Phase VI")
                    {
                        entitySetNursePDADetail = ValidateNursePDADetailForRMCPhaseVI(_genricBEImportData, out errorFlag);
                        //Extract those data which have Timeless = 1
                        genericBEImportDataTimeless = _genricBEImportData.FindAll(delegate(RMC.BusinessEntities.BEImportData obj) { return obj.Timeless == 1.ToString(); });
                        entitySetNursePDASpecialType = ValidateNursePDASpecialType(genericBEImportDataTimeless);
                    }
                    else
                    {
                        entitySetNursePDADetail = ValidateNursePDADetail(_genricBEImportData, out errorFlag);
                    }
                    _objectNursePDAInfo.IsErrorInDetailData = errorFlag;
                    _objectRMCDataContext.Dispose();
                }
                int max = Convert.ToInt16(_genricBEImportData.Max(m => m.KeyDataSequence));
                if (max > 1)
                {
                    //Special case for RMC Phase VI (SaveFileDataInDatabaseNew)
                    if (_genricBEImportData[0].ConfigName == "RMC Phase VI" || _genricBEImportData[1].ConfigName == "RMC Phase VI" || _genricBEImportData[2].ConfigName == "RMC Phase VI")
                    {
                        flag = SaveFileDataInDatabaseNew(entitySetNursePDADetail, hospitalUpload, entitySetNursePDASpecialType);
                    }
                    else
                    {
                        flag = SaveFileDataInDatabase(entitySetNursePDADetail, hospitalUpload);
                    }
                }
                else
                {
                    if (HttpContext.Current.Session["NonValidFileName"] != null)
                    {
                        //HttpContext.Current.Session["NonValidFileName"] = Convert.ToString(HttpContext.Current.Session["NonValidFileName"]) + @" \n " + originalFileName;
                        HttpContext.Current.Session["NonValidFileName"] = Convert.ToString(BSSerialization.Deserialize<string>(HttpContext.Current.Session["NonValidFileName"] as MemoryStream)) + @" \n " + originalFileName;
                    }
                    else
                    {
                        //HttpContext.Current.Session["NonValidFileName"] = originalFileName;
                        HttpContext.Current.Session["NonValidFileName"] = BSSerialization.Serialize(originalFileName);
                    }
                }
                //Update NursePDA Information.
            }
            catch (Exception ex)
            {
                ex.Data.Add("Function", "SaveFileData");
                ex.Data.Add("Class", "BSSDAValidation");
                throw ex;
            }

            return flag;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="hospitalDemographicID"></param>
        /// <returns></returns>
        public List<RMC.DataService.NursePDADetail> GetNursePDADetail(int hospitalDemographicID)
        {
            try
            {
                List<RMC.DataService.NursePDADetail> objectGenericNursePDADetail = null;
                _objectRMCDataContext = new RMCDataContext();

                objectGenericNursePDADetail = (from nd in _objectRMCDataContext.NursePDADetails
                                               where nd.NursePDAInfo.HospitalDemographicID == hospitalDemographicID && nd.IsDeleted == false && nd.NursePDAInfo.IsDeleted == false
                                               select nd).ToList<RMC.DataService.NursePDADetail>();

                return objectGenericNursePDADetail;
            }
            catch (Exception ex)
            {
                ex.Data.Add("Function", "GetNursePDADetail");
                ex.Data.Add("Class", "BSDataValidation");
                throw ex;
            }
        }

        /// <summary>
        /// Validate Non-Valid Data.   
        /// </summary>
        /// <param name="genericBEImportDataNurseDetail">Generic List Of Import Data in Business Entity.</param>
        /// <returns>Nurse Infomation and PDA Device Infomation.</returns>
        public void ValidateNonValidNursePDADetail(RMC.BusinessEntities.BEValidation objectBEValidation, string mode)
        {
            bool error = false;
            try
            {
                _entityNursePDADetail = new System.Data.Linq.EntitySet<NursePDADetail>();
                _objectRMCDataContext = new RMCDataContext();
                _objectNursePDADetail = new NursePDADetail();
                _genericLocation = new List<Location>();
                _genericActivity = new List<Activity>();
                _genericSubActivity = new List<SubActivity>();

                _objectNursePDADetail.LastLocationID = objectBEValidation.LastLocationID;
                _objectNursePDADetail.CognitiveCategory = objectBEValidation.CognitiveCategory;
                _objectNursePDADetail.ResourceRequirementID = objectBEValidation.ResourceRequirementID;
                if (mode.ToLower().Trim() == "ids")
                {
                    if (objectBEValidation != null)
                    {
                        _objectNursePDADetail.LocationID = objectBEValidation.LocationID;
                        if (objectBEValidation.LocationID > 0)
                        {
                            _objectNursePDADetail.IsErrorInLocation = false;
                        }
                        else
                        {
                            _objectNursePDADetail.IsErrorInLocation = true;
                        }

                        _objectNursePDADetail.ActivityID = objectBEValidation.ActivityID;
                        if (objectBEValidation.ActivityID > 0)
                        {
                            _objectNursePDADetail.IsErrorInActivity = false;
                        }
                        else
                        {
                            _objectNursePDADetail.IsErrorInActivity = true;
                        }

                        _objectNursePDADetail.SubActivityID = objectBEValidation.SubActivityID;
                        if (objectBEValidation.SubActivityID > 0)
                        {
                            _objectNursePDADetail.IsErrorInSubActivity = false;
                        }
                        else
                        {
                            _objectNursePDADetail.IsErrorInSubActivity = true;
                        }
                    }
                }
                else
                {
                    //Get Valid Location from Database.
                    _genericLocation = ValidateLocation();
                    //Get Valid Activity from Database.
                    _genericActivity = ValidateActivity();
                    //Get Valid Sub-Activity from Database.
                    _genericSubActivity = ValidateSubActivity();

                    _objectLocation = new Location();
                    _objectActivity = new Activity();
                    _objectSubActivity = new SubActivity();

                    if (objectBEValidation != null)
                    {
                        _objectNursePDADetail.LastLocationID = EditedLastLocation(objectBEValidation.LastLocationID, objectBEValidation.LastLocationName);
                        _objectNursePDADetail.ResourceRequirementID = EditedResourceRequirement(Convert.ToInt32(objectBEValidation.ResourceRequirementID), objectBEValidation.ResourceText);

                        _objectLocation = _genericLocation.Find(delegate(RMC.DataService.Location objectLocation)
                        { return objectLocation.Location1.ToLower().Trim() == objectBEValidation.LocationName.ToLower().Trim(); });
                        if (_objectLocation != null)
                        {
                            _objectNursePDADetail.LocationID = _objectLocation.LocationID;
                        }
                        else
                        {
                            if (mode.ToLower().Trim() == "edittext")
                            {
                                RMC.BussinessService.BSLocation objectBSLocation = new RMC.BussinessService.BSLocation();

                                _objectNursePDADetail.LocationID = objectBSLocation.InsertLocation(objectBEValidation.LocationName);
                            }
                            else
                            {
                                _objectNursePDADetail.LocationID = 0;
                                _objectNursePDADetail.IsErrorInLocation = true;
                                if (Convert.ToBoolean(_objectNursePDADetail.IsErrorInLocation))
                                {
                                    _objectNursePDADetail.LocationText = objectBEValidation.LocationName;
                                }
                                error = true;
                            }
                        }

                        _objectActivity = _genericActivity.Find(delegate(RMC.DataService.Activity objectActivity)
                        { return objectActivity.Activity1.ToLower().Trim() == objectBEValidation.ActivityName.ToLower().Trim(); });
                        if (_objectActivity != null)
                        {
                            _objectNursePDADetail.ActivityID = _objectActivity.ActivityID;
                        }
                        else
                        {
                            if (mode.ToLower().Trim() == "edittext")
                            {
                                RMC.BussinessService.BSActivity objectBSActivity = new RMC.BussinessService.BSActivity();

                                _objectNursePDADetail.ActivityID = objectBSActivity.InsertActivity(objectBEValidation.ActivityName);
                            }
                            else
                            {
                                _objectNursePDADetail.ActivityID = 0;
                                _objectNursePDADetail.IsErrorInActivity = true;
                                if (Convert.ToBoolean(_objectNursePDADetail.IsErrorInActivity))
                                {
                                    _objectNursePDADetail.ActivityText = objectBEValidation.ActivityName;
                                }
                                error = true;
                            }
                        }

                        _objectSubActivity = _genericSubActivity.Find(delegate(RMC.DataService.SubActivity objectSubActivity)
                        { return objectSubActivity.SubActivity1.ToLower().Trim() == objectBEValidation.SubActivityName.ToLower().Trim(); });
                        if (_objectSubActivity != null)
                        {
                            _objectNursePDADetail.SubActivityID = _objectSubActivity.SubActivityID;
                        }
                        else
                        {
                            if (mode.ToLower().Trim() == "edittext")
                            {
                                RMC.BussinessService.BSSubActivity objectBSSubActivity = new RMC.BussinessService.BSSubActivity();

                                _objectNursePDADetail.SubActivityID = objectBSSubActivity.InsertSubActivity(objectBEValidation.SubActivityName);
                            }
                            else
                            {
                                _objectNursePDADetail.SubActivityID = 0;
                                _objectNursePDADetail.IsErrorInSubActivity = true;
                                if (Convert.ToBoolean(_objectNursePDADetail.IsErrorInSubActivity))
                                {
                                    _objectNursePDADetail.SubActivityText = objectBEValidation.SubActivityName;
                                }
                                error = true;
                            }
                        }
                    }
                }

                _objectGenericValidation = GetValidateData();

                if (!error)
                {
                    error = _objectGenericValidation.TrueForAll(delegate(RMC.DataService.Validation objectValidation)
                    {
                        if (objectValidation.LocationID == _objectNursePDADetail.LocationID)
                        {
                            if (objectValidation.ActivityID == _objectNursePDADetail.ActivityID)
                            {
                                if (objectValidation.SubActivityID.Value > 0)
                                {
                                    if (objectValidation.SubActivityID == _objectNursePDADetail.SubActivityID)
                                    {
                                        return false;
                                    }
                                    else
                                    {
                                        return true;
                                    }
                                }
                                else
                                {
                                    return false;
                                }
                            }
                            else
                            {
                                return true;
                            }
                        }
                        else
                        {
                            return true;
                        }
                    });
                }

                if ((objectBEValidation.LocationID > 0 && objectBEValidation.ActivityID > 0) || (objectBEValidation.ActiveError1 == string.Empty && objectBEValidation.ActiveError2 == string.Empty &&
                        objectBEValidation.ActiveError3 == string.Empty && objectBEValidation.ActiveError4 == string.Empty))
                {
                    _objectNursePDADetail.IsActiveError = false;
                    _objectNursePDADetail.ActiveError1 = string.Empty;
                    _objectNursePDADetail.ActiveError2 = string.Empty;
                    _objectNursePDADetail.ActiveError3 = string.Empty;
                    _objectNursePDADetail.ActiveError4 = string.Empty;
                }
                else
                {
                    _objectNursePDADetail.IsActiveError = true;
                    _objectNursePDADetail.ActiveError1 = objectBEValidation.ActiveError1;
                    _objectNursePDADetail.ActiveError2 = objectBEValidation.ActiveError2;
                    _objectNursePDADetail.ActiveError3 = objectBEValidation.ActiveError3;
                    _objectNursePDADetail.ActiveError4 = objectBEValidation.ActiveError4;
                }
                _objectNursePDADetail.IsErrorExist = error;
                _objectRMCDataContext = new RMCDataContext();
                RMC.DataService.NursePDADetail objectNursePDADetail = (from npd in _objectRMCDataContext.NursePDADetails
                                                                       where npd.NurserDetailID == objectBEValidation.NurserDetailID
                                                                       select npd).FirstOrDefault();

                objectNursePDADetail.LastLocationID = _objectNursePDADetail.LastLocationID;
                objectNursePDADetail.LocationID = _objectNursePDADetail.LocationID;
                objectNursePDADetail.ActivityID = _objectNursePDADetail.ActivityID;
                objectNursePDADetail.SubActivityID = _objectNursePDADetail.SubActivityID;
                objectNursePDADetail.CognitiveCategory = _objectNursePDADetail.CognitiveCategory;
                objectNursePDADetail.ResourceRequirementID = _objectNursePDADetail.ResourceRequirementID;
                objectNursePDADetail.IsErrorExist = _objectNursePDADetail.IsErrorExist;
                objectNursePDADetail.IsErrorInActivity = _objectNursePDADetail.IsErrorInActivity;
                objectNursePDADetail.IsErrorInLocation = _objectNursePDADetail.IsErrorInLocation;
                objectNursePDADetail.IsErrorInSubActivity = _objectNursePDADetail.IsErrorInSubActivity;
                objectNursePDADetail.IsActiveError = _objectNursePDADetail.IsActiveError;
                objectNursePDADetail.ActiveError1 = _objectNursePDADetail.ActiveError1;
                objectNursePDADetail.ActiveError2 = _objectNursePDADetail.ActiveError2;
                objectNursePDADetail.ActiveError3 = _objectNursePDADetail.ActiveError3;
                objectNursePDADetail.ActiveError4 = _objectNursePDADetail.ActiveError4;

                _objectRMCDataContext.SubmitChanges();
            }
            catch (Exception ex)
            {
                ex.Data.Add("Function", "ValidateNonValidNursePDADetail");
                ex.Data.Add("Class", "BSSDAValidation");
                throw ex;
            }
            finally
            {
                _objectNursePDADetail = null;
                _genericLastLocation = null;
                _genericLocation = null;
                _genericActivity = null;
                _genericSubActivity = null;
                _genericResourceRequirement = null;
                _objectUserInfo = null;
                _objectLocation = null;
                _objectActivity = null;
                _objectSubActivity = null;
                _objectResourceRequirement = null;
                _objectGenericValidation = null;
                _objectRMCDataContext.Dispose();
            }
        }

        public bool ValidateStandardFileFormat(string filePath)
        {
            try
            {
                BSImportData objectDSImportData = new BSImportData();

                return objectDSImportData.CheckImportDataFileStandard(filePath);
            }
            catch (Exception ex)
            {
                //2011-0222 [amelinc] instead of throwing the exception
                //according to how this function is being called, it should log the exception and return true for the errorFlag
                //throw ex;
                LogManager.LogException(ex, LogManager.LoggingCategory.General, LogManager.LoggingLevel.Error);
                return true;
            }
        }

        #endregion

        #region Private Functions/Methods

        /// <summary>
        /// Get Validating Fields according to File Type(Config Name).
        /// Created By : Davinder Kumar
        /// Creation Date : June 30, 2009.
        /// </summary>
        /// <param name="configName">File Type Name</param>
        private RMC.DataService.ValidateFileField GetValidFileFields(string configName)
        {
            RMC.DataService.ValidateFileField validateFileFields;
            try
            {
                _objectRMCDataContext = new RMCDataContext();
                validateFileFields = (from vff in _objectRMCDataContext.ValidateFileFields
                                      where vff.ConfigurationName.ToLower().Trim() == configName.ToLower().Trim()
                                      select vff).FirstOrDefault();
            }
            catch (Exception ex)
            {
                ex.Data.Add("Function", "GetValidFileFields");
                ex.Data.Add("Class", "BSDataValidation");
                throw ex;
            }
            finally
            {
                _objectRMCDataContext.Dispose();
            }

            return validateFileFields;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private List<RMC.DataService.Validation> GetValidateData()
        {
            try
            {
                RMC.DataService.RMCDataContext ObjectRMCDataContext = new RMCDataContext();
                List<RMC.DataService.Validation> objectGenericValidation = (from v in ObjectRMCDataContext.Validations
                                                                            select v).ToList<RMC.DataService.Validation>();

                return objectGenericValidation;
            }
            catch (Exception ex)
            {
                ex.Data.Add("Function", "ValidateData");
                ex.Data.Add("Class", "BSSDAValidation");
                throw ex;
            }
            finally
            {
                _objectRMCDataContext.Dispose();
            }
        }

        /// <summary>
        /// Get Valid Activity from Database.
        /// Created By : Davinder Kumar.
        /// Creation Date : July 02, 2009.
        /// Modified By : Davinder Kumar.
        /// Modified Date : July 14, 2009.
        /// </summary>
        /// <returns></returns>
        private List<RMC.DataService.Activity> ValidateActivity()
        {
            try
            {
                RMC.DataService.RMCDataContext ObjectRMCDataContext = new RMCDataContext();
                List<RMC.DataService.Activity> genericActivity = (from a in ObjectRMCDataContext.Activities
                                                                  select a).ToList<RMC.DataService.Activity>();

                return genericActivity;
            }
            catch (Exception ex)
            {
                ex.Data.Add("Function", "ValidateActivity");
                ex.Data.Add("Class", "BSDataValidation");
                throw ex;
            }
            finally
            {
                _objectRMCDataContext.Dispose();
            }
        }

        /// <summary>
        /// Get Valid LastLocation from Database.
        /// Created By : Davinder Kumar.        
        /// </summary>
        /// <returns></returns>
        private List<RMC.DataService.LastLocation> GetLastLocation()
        {
            try
            {
                RMC.DataService.RMCDataContext ObjectRMCDataContext = new RMCDataContext();
                List<RMC.DataService.LastLocation> genericLastLocation = (from ll in ObjectRMCDataContext.LastLocations
                                                                          select ll).ToList<RMC.DataService.LastLocation>();

                return genericLastLocation;
            }
            catch (Exception ex)
            {
                ex.Data.Add("Function", "ValidateLastLocation");
                ex.Data.Add("Class", "BSSDAValidation");
                throw ex;
            }
            finally
            {
                _objectRMCDataContext.Dispose();
            }
        }

        /// <summary>
        /// Get Valid Location from Database.
        /// Created By : Davinder Kumar.
        /// Creation Date : July 01, 2009.
        /// Modified By : Davinder Kumar.
        /// Modified Date : July 14, 2009.
        /// </summary>
        /// <returns></returns>
        private List<RMC.DataService.Location> ValidateLocation()
        {
            try
            {
                RMC.DataService.RMCDataContext ObjectRMCDataContext = new RMCDataContext();
                List<RMC.DataService.Location> genericLocation = (from l in ObjectRMCDataContext.Locations
                                                                  select l).ToList<RMC.DataService.Location>();

                return genericLocation;
            }
            catch (Exception ex)
            {
                ex.Data.Add("Function", "ValidateLocation");
                ex.Data.Add("Class", "BSDataValidation");
                throw ex;
            }
            finally
            {
                _objectRMCDataContext.Dispose();
            }
        }

        /// <summary>
        /// Get Valid Resource Requirement from Database.
        /// Created By : Davinder Kumar.
        /// </summary>
        /// <returns></returns>
        private List<RMC.DataService.ResourceRequirement> GetResourceRequirement()
        {
            try
            {
                RMC.DataService.RMCDataContext ObjectRMCDataContext = new RMCDataContext();
                List<RMC.DataService.ResourceRequirement> genericResourceRequirement = (from rr in ObjectRMCDataContext.ResourceRequirements
                                                                                        select rr).ToList<RMC.DataService.ResourceRequirement>();

                return genericResourceRequirement;
            }
            catch (Exception ex)
            {
                ex.Data.Add("Function", "ValidateResourceRequirement");
                ex.Data.Add("Class", "BSSDAValidation");
                throw ex;
            }
            finally
            {
                _objectRMCDataContext.Dispose();
            }
        }

        /// <summary>
        /// Save Special type data. Specially used for RMC Phase VI having Timeless = 1 records only 
        /// Created By : Bharat Gupta.        
        /// </summary>
        /// <param name="genericBEImportDataNurseDetail">Generic List Of Import Data in Business Entity.</param>
        /// <returns>Special Type information</returns>
        private System.Data.Linq.EntitySet<RMC.DataService.NursePDASpecialType> ValidateNursePDASpecialType(List<BEImportData> genericBEImportDataNurseDetail)
        {
            try
            {
                _entityNursePDASpecialType = new System.Data.Linq.EntitySet<NursePDASpecialType>();
                _objectNursePDASpecialType = new NursePDASpecialType();
                foreach (BEImportData objectBEImportDataNursePDASpecialType in genericBEImportDataNurseDetail)
                {
                    if (Convert.ToString(objectBEImportDataNursePDASpecialType.KeyDataSequence).Trim() == 1.ToString())
                    {
                        _objectNursePDASpecialType.Date = objectBEImportDataNursePDASpecialType.Date;
                        _objectNursePDASpecialType.Time = objectBEImportDataNursePDASpecialType.Time;
                        _objectNursePDASpecialType.SpecialCategory = objectBEImportDataNursePDASpecialType.KeyData;
                        //_objectNursePDASpecialType.SpecialActivity = string.Empty;
                        continue;
                    }
                    if (Convert.ToString(objectBEImportDataNursePDASpecialType.KeyDataSequence).Trim() == 2.ToString())
                    {
                        //_objectNursePDASpecialType.SpecialCategory = string.Empty;
                        _objectNursePDASpecialType.SpecialActivity = objectBEImportDataNursePDASpecialType.KeyData;
                    }
                    _entityNursePDASpecialType.Add(_objectNursePDASpecialType);
                    _objectNursePDASpecialType = new NursePDASpecialType();
                }
                return _entityNursePDASpecialType;
            }
            catch (Exception ex)
            {
                ex.Data.Add("Function", "ValidateNursePDASpecialType");
                ex.Data.Add("Class", "BSSDAValidation");
                throw ex;
            }
        }


        /// <summary>
        /// Save Detail Infomation from PDA device To Application Object.
        /// Created By : Davinder Kumar.        
        /// </summary>
        /// <param name="genericBEImportDataNurseDetail">Generic List Of Import Data in Business Entity.</param>
        /// <returns>Nurse Infomation and PDA Device Infomation.</returns>
        private System.Data.Linq.EntitySet<RMC.DataService.NursePDADetail> ValidateNursePDADetail(List<BEImportData> genericBEImportDataNurseDetail, out bool errorFlag)
        {
            bool error = false;
            bool flagLastLocationPass = false;
            try
            {
                int index = 0, rowIndex = -1, countKeyDataSeq = 0, totalKeyDataSeq = 3, totalKeyDataSeq2 = 4;
                errorFlag = false;
                _entityNursePDADetail = new System.Data.Linq.EntitySet<NursePDADetail>();
                _objectNursePDADetail = new NursePDADetail();
                _genericLastLocation = new List<LastLocation>();
                _genericLocation = new List<Location>();
                _genericActivity = new List<Activity>();
                _genericSubActivity = new List<SubActivity>();
                //_genericCategoryGroup = new List<CategoryGroup>();
                _genericResourceRequirement = new List<ResourceRequirement>();
                //_genericValueAddedType = new List<ValueAddedType>();
                
                //_objectUserInfo = ((List<UserInfo>)HttpContext.Current.Session["UserInformation"])[0];
                _objectUserInfo = ((List<RMC.DataService.UserInfo>)(BSSerialization.Deserialize<List<RMC.DataService.UserInfo>>(HttpContext.Current.Session["UserInformation"] as MemoryStream)))[0];

                _objectGenericValidation = GetValidateData();

                _genericLastLocation = GetLastLocation();
                //Get Valid Location from Database.
                _genericLocation = ValidateLocation();
                //Get Valid Activity from Database.
                _genericActivity = ValidateActivity();
                //Get Valid Sub-Activity from Database.
                _genericSubActivity = ValidateSubActivity();
                //Get Valid Resource Requirement from Database.
                _genericResourceRequirement = ValidateResourceRequirement();

                foreach (BEImportData objectBEImportDataNursePDADetail in genericBEImportDataNurseDetail)
                {
                    rowIndex++;
                    objectBEImportDataNursePDADetail.KeyData = AutoCorrection(objectBEImportDataNursePDADetail.KeyData);
                    //Record Sequence.
                    if (Convert.ToString(objectBEImportDataNursePDADetail.KeyDataSequence).Trim() == 1.ToString())
                    {

                        countKeyDataSeq++;
                        if (countKeyDataSeq > totalKeyDataSeq)
                        {
                            switch (countKeyDataSeq)
                            {
                                case 4:
                                    _objectNursePDADetail.ActiveError1 = objectBEImportDataNursePDADetail.KeyData;
                                    break;
                                case 5:
                                    _objectNursePDADetail.ActiveError2 = objectBEImportDataNursePDADetail.KeyData;
                                    break;
                                case 6:
                                    _objectNursePDADetail.ActiveError3 = objectBEImportDataNursePDADetail.KeyData;
                                    break;
                                //case 7:
                                //    _objectNursePDADetail.ActiveError4 = objectBEImportDataNursePDADetail.KeyData;
                                //    break;
                                default:
                                    break;
                            }

                            continue;
                        }
                        //Conditions check the length of string to avoid exception in a next statement.
                        if (objectBEImportDataNursePDADetail.KeyData.Length > 3)
                        {
                            if (objectBEImportDataNursePDADetail.KeyData.Substring(0, 4).ToLower().Trim() == "last")
                            {
                                RMC.DataService.LastLocation objectLastLocation = null;
                                objectLastLocation = _genericLastLocation.Find(delegate(RMC.DataService.LastLocation objectLastLoc)
                                {
                                    return objectLastLoc.LastLocation1.ToLower().Trim() == objectBEImportDataNursePDADetail.KeyData.ToLower().Trim();
                                });
                                if (objectLastLocation != null)
                                {
                                    _objectNursePDADetail.LastLocationID = objectLastLocation.LastLocationID;
                                    _objectNursePDADetail.LastLocationDate = objectBEImportDataNursePDADetail.Date;
                                    _objectNursePDADetail.LastLocationTime = objectBEImportDataNursePDADetail.Time;
                                }
                                else
                                {
                                    _objectNursePDADetail.LastLocationID = InsertLastLocation(objectBEImportDataNursePDADetail.KeyData);
                                    _objectNursePDADetail.LastLocationDate = objectBEImportDataNursePDADetail.Date;
                                    _objectNursePDADetail.LastLocationTime = objectBEImportDataNursePDADetail.Time;
                                }

                                flagLastLocationPass = true;
                                continue;
                            }
                            // Special Case for IHI Phase IV and RMC Phase IV files types.
                            else if (!flagLastLocationPass)
                            {
                                if (objectBEImportDataNursePDADetail.ConfigName.ToLower().Trim() == "ihi phase iv" || objectBEImportDataNursePDADetail.ConfigName.ToLower().Trim() == "rmc phase iv")
                                {
                                    if (checkLastLocationForSpecificCase(objectBEImportDataNursePDADetail.ConfigName, objectBEImportDataNursePDADetail.KeyData))
                                    {
                                        RMC.DataService.LastLocation objectLastLocation = null;
                                        objectLastLocation = _genericLastLocation.Find(delegate(RMC.DataService.LastLocation objectLastLoc)
                                        {
                                            return objectLastLoc.LastLocation1.ToLower().Trim() == "last " + objectBEImportDataNursePDADetail.KeyData.ToLower().Trim();
                                        });
                                        if (objectLastLocation != null)
                                        {
                                            _objectNursePDADetail.LastLocationID = objectLastLocation.LastLocationID;
                                            _objectNursePDADetail.LastLocationDate = objectBEImportDataNursePDADetail.Date;
                                            _objectNursePDADetail.LastLocationTime = objectBEImportDataNursePDADetail.Time;
                                        }
                                        else
                                        {
                                            _objectNursePDADetail.LastLocationID = InsertLastLocation(objectBEImportDataNursePDADetail.KeyData);
                                            _objectNursePDADetail.LastLocationDate = objectBEImportDataNursePDADetail.Date;
                                            _objectNursePDADetail.LastLocationTime = objectBEImportDataNursePDADetail.Time;
                                        }

                                        flagLastLocationPass = true;
                                        continue;
                                    }
                                }
                            }
                        }

                        //Index, 0 use for Location.
                        if (index == 0)
                        {
                            _objectLocation = new Location();

                            _objectLocation = _genericLocation.Find(delegate(RMC.DataService.Location objectLocation)
                            { return objectLocation.Location1.ToLower().Trim() == objectBEImportDataNursePDADetail.KeyData.ToLower().Trim(); });
                            if (_objectLocation != null)
                            {
                                _objectNursePDADetail.LocationID = _objectLocation.LocationID;
                                _objectNursePDADetail.LocationDate = objectBEImportDataNursePDADetail.Date;
                                _objectNursePDADetail.LocationTime = objectBEImportDataNursePDADetail.Time;
                            }
                            else
                            {
                                _objectNursePDADetail.LocationID = 0;
                                _objectNursePDADetail.IsErrorInLocation = true;
                                if (Convert.ToBoolean(_objectNursePDADetail.IsErrorInLocation))
                                {
                                    _objectNursePDADetail.LocationText = objectBEImportDataNursePDADetail.KeyData.ToLower().Trim();
                                    _objectNursePDADetail.LocationDate = objectBEImportDataNursePDADetail.Date;
                                    _objectNursePDADetail.LocationTime = objectBEImportDataNursePDADetail.Time;
                                }
                                error = true;
                            }
                            index++;
                            continue;
                        }

                        //Index, 1 use for Activity.
                        if (index == 1)
                        {
                            _objectActivity = new Activity();

                            _objectActivity = _genericActivity.Find(delegate(RMC.DataService.Activity objectActivity)
                            { return objectActivity.Activity1.ToLower().Trim() == objectBEImportDataNursePDADetail.KeyData.ToLower().Trim(); });
                            if (_objectActivity != null)
                            {
                                _objectNursePDADetail.ActivityID = _objectActivity.ActivityID;
                                _objectNursePDADetail.ActivityDate = objectBEImportDataNursePDADetail.Date;
                                _objectNursePDADetail.ActivityTime = objectBEImportDataNursePDADetail.Time;
                            }
                            else
                            {
                                _objectNursePDADetail.ActivityID = 0;
                                _objectNursePDADetail.IsErrorInActivity = true;
                                if (Convert.ToBoolean(_objectNursePDADetail.IsErrorInActivity))
                                {
                                    _objectNursePDADetail.ActivityText = objectBEImportDataNursePDADetail.KeyData.ToLower().Trim();
                                    _objectNursePDADetail.ActivityDate = objectBEImportDataNursePDADetail.Date;
                                    _objectNursePDADetail.ActivityTime = objectBEImportDataNursePDADetail.Time;
                                }
                                error = true;
                            }
                            index++;
                            continue;
                        }
                    }
                    if (Convert.ToString(objectBEImportDataNursePDADetail.KeyDataSequence).Trim() == 2.ToString())
                    {
                        if (countKeyDataSeq > totalKeyDataSeq)
                        {
                            countKeyDataSeq++;
                            switch (countKeyDataSeq)
                            {
                                case 4:
                                    _objectNursePDADetail.ActiveError1 = objectBEImportDataNursePDADetail.KeyData;
                                    break;
                                case 5:
                                    _objectNursePDADetail.ActiveError2 = objectBEImportDataNursePDADetail.KeyData;
                                    break;
                                case 6:
                                    _objectNursePDADetail.ActiveError3 = objectBEImportDataNursePDADetail.KeyData;
                                    break;
                                case 7:
                                    _objectNursePDADetail.ActiveError4 = objectBEImportDataNursePDADetail.KeyData;
                                    break;
                                default:
                                    break;
                            }

                            _objectNursePDADetail.IsActiveError = true;
                            countKeyDataSeq = 0;
                            index = 0;
                            error = true;
                        }
                        else
                        {
                            if (_objectNursePDADetail.IsActiveError != true)
                            {
                                _objectNursePDADetail.IsActiveError = false;

                                if (_objectNursePDADetail.ActiveError1 == null)
                                {
                                    _objectNursePDADetail.ActiveError1 = string.Empty;
                                }

                                if (_objectNursePDADetail.ActiveError2 == null)
                                {
                                    _objectNursePDADetail.ActiveError2 = string.Empty;
                                }

                                if (_objectNursePDADetail.ActiveError3 == null)
                                {
                                    _objectNursePDADetail.ActiveError3 = string.Empty;
                                }

                                if (_objectNursePDADetail.ActiveError4 == null)
                                {
                                    _objectNursePDADetail.ActiveError4 = string.Empty;
                                }

                            }
                            else
                            {
                                if (_objectNursePDADetail.ActiveError2 == null)
                                {
                                    _objectNursePDADetail.ActiveError2 = string.Empty;
                                }

                                if (_objectNursePDADetail.ActiveError3 == null)
                                {
                                    _objectNursePDADetail.ActiveError3 = string.Empty;
                                }

                                if (_objectNursePDADetail.ActiveError4 == null)
                                {
                                    _objectNursePDADetail.ActiveError4 = string.Empty;
                                }
                            }

                            countKeyDataSeq = 0;
                        }
                        //Index, 1 use for Activity.
                        if (index == 1)
                        {
                            _objectActivity = new Activity();

                            _objectActivity = _genericActivity.Find(delegate(RMC.DataService.Activity objectActivity)
                            { return objectActivity.Activity1.ToLower().Trim() == objectBEImportDataNursePDADetail.KeyData.ToLower().Trim(); });
                            if (_objectActivity != null)
                            {
                                _objectNursePDADetail.ActivityID = _objectActivity.ActivityID;
                                _objectNursePDADetail.ActivityDate = objectBEImportDataNursePDADetail.Date;
                                _objectNursePDADetail.ActivityTime = objectBEImportDataNursePDADetail.Time;
                            }
                            else
                            {
                                _objectNursePDADetail.ActivityID = 0;
                                _objectNursePDADetail.IsErrorInActivity = true;
                                if (Convert.ToBoolean(_objectNursePDADetail.IsErrorInActivity))
                                {
                                    _objectNursePDADetail.ActivityText = objectBEImportDataNursePDADetail.KeyData.ToLower().Trim();
                                    _objectNursePDADetail.ActivityDate = objectBEImportDataNursePDADetail.Date;
                                    _objectNursePDADetail.ActivityTime = objectBEImportDataNursePDADetail.Time;
                                }
                                error = true;
                            }
                        }

                        //Index, 2 use for SubActivity.
                        if (index == 2)
                        {
                            _objectSubActivity = new SubActivity();

                            _objectSubActivity = _genericSubActivity.Find(delegate(RMC.DataService.SubActivity objectSubActivity)
                            { return objectSubActivity.SubActivity1.ToLower().Trim() == objectBEImportDataNursePDADetail.KeyData.ToLower().Trim(); });
                            if (_objectSubActivity != null)
                            {
                                _objectNursePDADetail.SubActivityID = _objectSubActivity.SubActivityID;
                                _objectNursePDADetail.SubActivityDate = objectBEImportDataNursePDADetail.Date;
                                _objectNursePDADetail.SubActivityTime = objectBEImportDataNursePDADetail.Time;
                            }
                            else
                            {
                                _objectNursePDADetail.SubActivityID = 0;
                                _objectNursePDADetail.IsErrorInSubActivity = true;
                                if (Convert.ToBoolean(_objectNursePDADetail.IsErrorInSubActivity))
                                {
                                    _objectNursePDADetail.SubActivityText = objectBEImportDataNursePDADetail.KeyData.ToLower().Trim();
                                    _objectNursePDADetail.SubActivityDate = objectBEImportDataNursePDADetail.Date;
                                    _objectNursePDADetail.SubActivityTime = objectBEImportDataNursePDADetail.Time;
                                }
                                error = true;
                            }
                        }
                        else if (index < 2)
                        {
                            _objectNursePDADetail.SubActivityID = 0;
                        }

                        if (!error)
                        {
                            error = _objectGenericValidation.TrueForAll(delegate(RMC.DataService.Validation objectValidation)
                                    {
                                        if (objectValidation.LocationID == _objectNursePDADetail.LocationID)
                                        {
                                            if (objectValidation.ActivityID == _objectNursePDADetail.ActivityID)
                                            {
                                                if (objectValidation.SubActivityID.Value > 0 && _objectNursePDADetail.SubActivityID > 0)
                                                {
                                                    if (objectValidation.SubActivityID == _objectNursePDADetail.SubActivityID)
                                                    {
                                                        return false;
                                                    }
                                                    else
                                                    {
                                                        return true;
                                                    }
                                                }
                                                else
                                                {
                                                    return false;
                                                }
                                            }
                                            else
                                            {
                                                return true;
                                            }
                                        }
                                        else
                                        {
                                            return true;
                                        }
                                    });
                        }
                        if (_objectUserInfo != null)
                        {
                            _objectNursePDADetail.CreatedBy = _objectUserInfo.FirstName + " " + _objectUserInfo.LastName;
                            _objectNursePDADetail.CreatedDate = DateTime.Now;
                            _objectNursePDADetail.IsActive = true;
                            _objectNursePDADetail.IsDeleted = false;
                        }

                        if (_objectNursePDADetail.IsActiveError.Value)
                        {
                            _objectNursePDADetail.IsErrorExist = _objectNursePDADetail.IsActiveError.Value;
                        }
                        else
                        {
                            _objectNursePDADetail.IsErrorExist = error;
                        }

                        if (rowIndex < genericBEImportDataNurseDetail.Count() - 1 && genericBEImportDataNurseDetail[rowIndex + 1].KeyDataSequence.Trim() == 0.ToString())
                        {
                            if (genericBEImportDataNurseDetail[rowIndex + 1].KeyData.ToLower().Trim() == "notes")
                            {
                                string returnText = string.Empty;
                                returnText = GetCognitiveCategoryResourcRequirement(genericBEImportDataNurseDetail, rowIndex + 2);

                                if (genericBEImportDataNurseDetail[rowIndex].ConfigName.ToLower().Trim() == "rmc phase v")
                                {
                                    int resourceRequirementID = 0;
                                    bool flag = false;
                                    flag = int.TryParse(returnText, out resourceRequirementID);
                                    if (flag)
                                    {
                                        _objectNursePDADetail.ResourceRequirementID = resourceRequirementID;
                                    }
                                }
                                else if (genericBEImportDataNurseDetail[rowIndex].ConfigName.ToLower().Trim() == "ascension tcab")
                                {
                                    _objectNursePDADetail.CognitiveCategory = returnText;
                                }
                            }
                        }

                        if (_objectNursePDADetail.ResourceRequirementID == null)
                        {
                            _objectNursePDADetail.ResourceRequirementID = 0;
                        }
                        _entityNursePDADetail.Add(_objectNursePDADetail);
                        index = 0;
                        if (error == true)
                        {
                            errorFlag = error;
                        }
                        error = false;
                        flagLastLocationPass = false;
                        _objectNursePDADetail = new NursePDADetail();
                    }
                }

                return _entityNursePDADetail;
            }
            catch (Exception ex)
            {
                ex.Data.Add("Function", "SaveNursePDADetail");
                ex.Data.Add("Class", "BSSDAValidation");
                throw ex;
            }
            finally
            {
                _objectNursePDADetail = null;
                _genericLastLocation = null;
                _genericLocation = null;
                _genericActivity = null;
                _genericSubActivity = null;
                _genericResourceRequirement = null;
                _objectUserInfo = null;
                _objectLocation = null;
                _objectActivity = null;
                _objectSubActivity = null;
                _objectResourceRequirement = null;
                _objectGenericValidation = null;
                genericBEImportDataNurseDetail = null;
            }
        }



        /// <summary>
        /// Special Case for RMC Phase VI config. Save Detail Infomation from PDA device To Application Object.
        /// Created By : Davinder Kumar.        
        /// </summary>
        /// <param name="genericBEImportDataNurseDetail">Generic List Of Import Data in Business Entity.</param>
        /// <returns>Nurse Infomation and PDA Device Infomation.</returns>
        private System.Data.Linq.EntitySet<RMC.DataService.NursePDADetail> ValidateNursePDADetailForRMCPhaseVI(List<BEImportData> genericBEImportDataNurseDetail, out bool errorFlag)
        {
            bool error = false;
            bool flagLastLocationPass = false;
            try
            {
                int index = 0, rowIndex = -1, countKeyDataSeq = 0, totalKeyDataSeq = 3, totalKeyDataSeq2 = 4;
                errorFlag = false;
                _entityNursePDADetail = new System.Data.Linq.EntitySet<NursePDADetail>();
                _objectNursePDADetail = new NursePDADetail();
                _genericLastLocation = new List<LastLocation>();
                _genericLocation = new List<Location>();
                _genericActivity = new List<Activity>();
                _genericSubActivity = new List<SubActivity>();
                //_genericCategoryGroup = new List<CategoryGroup>();
                _genericResourceRequirement = new List<ResourceRequirement>();
                //_genericValueAddedType = new List<ValueAddedType>();

                //_objectUserInfo = ((List<UserInfo>)HttpContext.Current.Session["UserInformation"])[0];
                _objectUserInfo = ((List<RMC.DataService.UserInfo>)(BSSerialization.Deserialize<List<RMC.DataService.UserInfo>>(HttpContext.Current.Session["UserInformation"] as MemoryStream)))[0];

                _objectGenericValidation = GetValidateData();

                _genericLastLocation = GetLastLocation();
                //Get Valid Location from Database.
                _genericLocation = ValidateLocation();
                //Get Valid Activity from Database.
                _genericActivity = ValidateActivity();
                //Get Valid Sub-Activity from Database.
                _genericSubActivity = ValidateSubActivity();
                //Get Valid Resource Requirement from Database.
                _genericResourceRequirement = ValidateResourceRequirement();

                foreach (BEImportData objectBEImportDataNursePDADetail in genericBEImportDataNurseDetail)
                {
                    rowIndex++;
                    objectBEImportDataNursePDADetail.KeyData = AutoCorrection(objectBEImportDataNursePDADetail.KeyData);
                    //Record Sequence.
                    if (Convert.ToString(objectBEImportDataNursePDADetail.KeyDataSequence).Trim() == 1.ToString() && Convert.ToString(objectBEImportDataNursePDADetail.Timeless).Trim() == 0.ToString())
                    {

                        countKeyDataSeq++;
                        if (countKeyDataSeq > totalKeyDataSeq)
                        {
                            switch (countKeyDataSeq)
                            {
                                case 4:
                                    _objectNursePDADetail.ActiveError1 = objectBEImportDataNursePDADetail.KeyData;
                                    break;
                                case 5:
                                    _objectNursePDADetail.ActiveError2 = objectBEImportDataNursePDADetail.KeyData;
                                    break;
                                case 6:
                                    _objectNursePDADetail.ActiveError3 = objectBEImportDataNursePDADetail.KeyData;
                                    break;
                                //case 7:
                                //    _objectNursePDADetail.ActiveError4 = objectBEImportDataNursePDADetail.KeyData;
                                //    break;
                                default:
                                    break;
                            }

                            continue;
                        }
                        //Conditions check the length of string to avoid exception in a next statement.
                        if (objectBEImportDataNursePDADetail.KeyData.Length > 3)
                        {
                            if (objectBEImportDataNursePDADetail.KeyData.Substring(0, 4).ToLower().Trim() == "last")
                            {
                                RMC.DataService.LastLocation objectLastLocation = null;
                                objectLastLocation = _genericLastLocation.Find(delegate(RMC.DataService.LastLocation objectLastLoc)
                                {
                                    return objectLastLoc.LastLocation1.ToLower().Trim() == objectBEImportDataNursePDADetail.KeyData.ToLower().Trim();
                                });
                                if (objectLastLocation != null)
                                {
                                    _objectNursePDADetail.LastLocationID = objectLastLocation.LastLocationID;
                                    _objectNursePDADetail.LastLocationDate = objectBEImportDataNursePDADetail.Date;
                                    _objectNursePDADetail.LastLocationTime = objectBEImportDataNursePDADetail.Time;
                                }
                                else
                                {
                                    _objectNursePDADetail.LastLocationID = InsertLastLocation(objectBEImportDataNursePDADetail.KeyData);
                                    _objectNursePDADetail.LastLocationDate = objectBEImportDataNursePDADetail.Date;
                                    _objectNursePDADetail.LastLocationTime = objectBEImportDataNursePDADetail.Time;
                                }

                                flagLastLocationPass = true;
                                continue;
                            }
                            // Special Case for IHI Phase IV and RMC Phase IV files types.
                            else if (!flagLastLocationPass)
                            {
                                if (objectBEImportDataNursePDADetail.ConfigName.ToLower().Trim() == "ihi phase iv" || objectBEImportDataNursePDADetail.ConfigName.ToLower().Trim() == "rmc phase iv")
                                {
                                    if (checkLastLocationForSpecificCase(objectBEImportDataNursePDADetail.ConfigName, objectBEImportDataNursePDADetail.KeyData))
                                    {
                                        RMC.DataService.LastLocation objectLastLocation = null;
                                        objectLastLocation = _genericLastLocation.Find(delegate(RMC.DataService.LastLocation objectLastLoc)
                                        {
                                            return objectLastLoc.LastLocation1.ToLower().Trim() == "last " + objectBEImportDataNursePDADetail.KeyData.ToLower().Trim();
                                        });
                                        if (objectLastLocation != null)
                                        {
                                            _objectNursePDADetail.LastLocationID = objectLastLocation.LastLocationID;
                                            _objectNursePDADetail.LastLocationDate = objectBEImportDataNursePDADetail.Date;
                                            _objectNursePDADetail.LastLocationTime = objectBEImportDataNursePDADetail.Time;
                                        }
                                        else
                                        {
                                            _objectNursePDADetail.LastLocationID = InsertLastLocation(objectBEImportDataNursePDADetail.KeyData);
                                            _objectNursePDADetail.LastLocationDate = objectBEImportDataNursePDADetail.Date;
                                            _objectNursePDADetail.LastLocationTime = objectBEImportDataNursePDADetail.Time;
                                        }

                                        flagLastLocationPass = true;
                                        continue;
                                    }
                                }
                            }
                        }

                        //Index, 0 use for Location.
                        if (index == 0)
                        {
                            _objectLocation = new Location();

                            _objectLocation = _genericLocation.Find(delegate(RMC.DataService.Location objectLocation)
                            { return objectLocation.Location1.ToLower().Trim() == objectBEImportDataNursePDADetail.KeyData.ToLower().Trim(); });
                            if (_objectLocation != null)
                            {
                                _objectNursePDADetail.LocationID = _objectLocation.LocationID;
                                _objectNursePDADetail.LocationDate = objectBEImportDataNursePDADetail.Date;
                                _objectNursePDADetail.LocationTime = objectBEImportDataNursePDADetail.Time;
                            }
                            else
                            {
                                _objectNursePDADetail.LocationID = 0;
                                _objectNursePDADetail.IsErrorInLocation = true;
                                if (Convert.ToBoolean(_objectNursePDADetail.IsErrorInLocation))
                                {
                                    _objectNursePDADetail.LocationText = objectBEImportDataNursePDADetail.KeyData.ToLower().Trim();
                                    _objectNursePDADetail.LocationDate = objectBEImportDataNursePDADetail.Date;
                                    _objectNursePDADetail.LocationTime = objectBEImportDataNursePDADetail.Time;
                                }
                                error = true;
                            }
                            index++;
                            continue;
                        }

                        //Index, 1 use for Activity.
                        if (index == 1)
                        {
                            _objectActivity = new Activity();

                            _objectActivity = _genericActivity.Find(delegate(RMC.DataService.Activity objectActivity)
                            { return objectActivity.Activity1.ToLower().Trim() == objectBEImportDataNursePDADetail.KeyData.ToLower().Trim(); });
                            if (_objectActivity != null)
                            {
                                _objectNursePDADetail.ActivityID = _objectActivity.ActivityID;
                                _objectNursePDADetail.ActivityDate = objectBEImportDataNursePDADetail.Date;
                                _objectNursePDADetail.ActivityTime = objectBEImportDataNursePDADetail.Time;
                            }
                            else
                            {
                                _objectNursePDADetail.ActivityID = 0;
                                _objectNursePDADetail.IsErrorInActivity = true;
                                if (Convert.ToBoolean(_objectNursePDADetail.IsErrorInActivity))
                                {
                                    _objectNursePDADetail.ActivityText = objectBEImportDataNursePDADetail.KeyData.ToLower().Trim();
                                    _objectNursePDADetail.ActivityDate = objectBEImportDataNursePDADetail.Date;
                                    _objectNursePDADetail.ActivityTime = objectBEImportDataNursePDADetail.Time;
                                }
                                error = true;
                            }
                            index++;
                            continue;
                        }
                    }
                    if (Convert.ToString(objectBEImportDataNursePDADetail.KeyDataSequence).Trim() == 2.ToString() && Convert.ToString(objectBEImportDataNursePDADetail.Timeless).Trim() == 0.ToString())
                    {
                        if (countKeyDataSeq > totalKeyDataSeq)
                        {
                            countKeyDataSeq++;
                            switch (countKeyDataSeq)
                            {
                                case 4:
                                    _objectNursePDADetail.ActiveError1 = objectBEImportDataNursePDADetail.KeyData;
                                    break;
                                case 5:
                                    _objectNursePDADetail.ActiveError2 = objectBEImportDataNursePDADetail.KeyData;
                                    break;
                                case 6:
                                    _objectNursePDADetail.ActiveError3 = objectBEImportDataNursePDADetail.KeyData;
                                    break;
                                case 7:
                                    _objectNursePDADetail.ActiveError4 = objectBEImportDataNursePDADetail.KeyData;
                                    break;
                                default:
                                    break;
                            }

                            _objectNursePDADetail.IsActiveError = true;
                            countKeyDataSeq = 0;
                            index = 0;
                            error = true;
                        }
                        else
                        {
                            if (_objectNursePDADetail.IsActiveError != true)
                            {
                                _objectNursePDADetail.IsActiveError = false;

                                if (_objectNursePDADetail.ActiveError1 == null)
                                {
                                    _objectNursePDADetail.ActiveError1 = string.Empty;
                                }

                                if (_objectNursePDADetail.ActiveError2 == null)
                                {
                                    _objectNursePDADetail.ActiveError2 = string.Empty;
                                }

                                if (_objectNursePDADetail.ActiveError3 == null)
                                {
                                    _objectNursePDADetail.ActiveError3 = string.Empty;
                                }

                                if (_objectNursePDADetail.ActiveError4 == null)
                                {
                                    _objectNursePDADetail.ActiveError4 = string.Empty;
                                }

                            }
                            else
                            {
                                if (_objectNursePDADetail.ActiveError2 == null)
                                {
                                    _objectNursePDADetail.ActiveError2 = string.Empty;
                                }

                                if (_objectNursePDADetail.ActiveError3 == null)
                                {
                                    _objectNursePDADetail.ActiveError3 = string.Empty;
                                }

                                if (_objectNursePDADetail.ActiveError4 == null)
                                {
                                    _objectNursePDADetail.ActiveError4 = string.Empty;
                                }
                            }

                            countKeyDataSeq = 0;
                        }
                        //Index, 1 use for Activity.
                        if (index == 1)
                        {
                            _objectActivity = new Activity();

                            _objectActivity = _genericActivity.Find(delegate(RMC.DataService.Activity objectActivity)
                            { return objectActivity.Activity1.ToLower().Trim() == objectBEImportDataNursePDADetail.KeyData.ToLower().Trim(); });
                            if (_objectActivity != null)
                            {
                                _objectNursePDADetail.ActivityID = _objectActivity.ActivityID;
                                _objectNursePDADetail.ActivityDate = objectBEImportDataNursePDADetail.Date;
                                _objectNursePDADetail.ActivityTime = objectBEImportDataNursePDADetail.Time;
                            }
                            else
                            {
                                _objectNursePDADetail.ActivityID = 0;
                                _objectNursePDADetail.IsErrorInActivity = true;
                                if (Convert.ToBoolean(_objectNursePDADetail.IsErrorInActivity))
                                {
                                    _objectNursePDADetail.ActivityText = objectBEImportDataNursePDADetail.KeyData.ToLower().Trim();
                                    _objectNursePDADetail.ActivityDate = objectBEImportDataNursePDADetail.Date;
                                    _objectNursePDADetail.ActivityTime = objectBEImportDataNursePDADetail.Time;
                                }
                                error = true;
                            }
                        }

                        //Index, 2 use for SubActivity.
                        if (index == 2)
                        {
                            _objectSubActivity = new SubActivity();

                            _objectSubActivity = _genericSubActivity.Find(delegate(RMC.DataService.SubActivity objectSubActivity)
                            { return objectSubActivity.SubActivity1.ToLower().Trim() == objectBEImportDataNursePDADetail.KeyData.ToLower().Trim(); });
                            if (_objectSubActivity != null)
                            {
                                _objectNursePDADetail.SubActivityID = _objectSubActivity.SubActivityID;
                                _objectNursePDADetail.SubActivityDate = objectBEImportDataNursePDADetail.Date;
                                _objectNursePDADetail.SubActivityTime = objectBEImportDataNursePDADetail.Time;
                            }
                            else
                            {
                                _objectNursePDADetail.SubActivityID = 0;
                                _objectNursePDADetail.IsErrorInSubActivity = true;
                                if (Convert.ToBoolean(_objectNursePDADetail.IsErrorInSubActivity))
                                {
                                    _objectNursePDADetail.SubActivityText = objectBEImportDataNursePDADetail.KeyData.ToLower().Trim();
                                    _objectNursePDADetail.SubActivityDate = objectBEImportDataNursePDADetail.Date;
                                    _objectNursePDADetail.SubActivityTime = objectBEImportDataNursePDADetail.Time;
                                }
                                error = true;
                            }
                        }
                        else if (index < 2)
                        {
                            _objectNursePDADetail.SubActivityID = 0;
                        }

                        if (!error)
                        {
                            error = _objectGenericValidation.TrueForAll(delegate(RMC.DataService.Validation objectValidation)
                            {
                                if (objectValidation.LocationID == _objectNursePDADetail.LocationID)
                                {
                                    if (objectValidation.ActivityID == _objectNursePDADetail.ActivityID)
                                    {
                                        if (objectValidation.SubActivityID.Value > 0 && _objectNursePDADetail.SubActivityID > 0)
                                        {
                                            if (objectValidation.SubActivityID == _objectNursePDADetail.SubActivityID)
                                            {
                                                return false;
                                            }
                                            else
                                            {
                                                return true;
                                            }
                                        }
                                        else
                                        {
                                            return false;
                                        }
                                    }
                                    else
                                    {
                                        return true;
                                    }
                                }
                                else
                                {
                                    return true;
                                }
                            });
                        }
                        if (_objectUserInfo != null)
                        {
                            _objectNursePDADetail.CreatedBy = _objectUserInfo.FirstName + " " + _objectUserInfo.LastName;
                            _objectNursePDADetail.CreatedDate = DateTime.Now;
                            _objectNursePDADetail.IsActive = true;
                            _objectNursePDADetail.IsDeleted = false;
                        }

                        if (_objectNursePDADetail.IsActiveError.Value)
                        {
                            _objectNursePDADetail.IsErrorExist = _objectNursePDADetail.IsActiveError.Value;
                        }
                        else
                        {
                            _objectNursePDADetail.IsErrorExist = error;
                        }

                        if (rowIndex < genericBEImportDataNurseDetail.Count() - 1 && genericBEImportDataNurseDetail[rowIndex + 1].KeyDataSequence.Trim() == 0.ToString())
                        {
                            if (genericBEImportDataNurseDetail[rowIndex + 1].KeyData.ToLower().Trim() == "notes")
                            {
                                string returnText = string.Empty;
                                returnText = GetCognitiveCategoryResourcRequirement(genericBEImportDataNurseDetail, rowIndex + 2);

                                if (genericBEImportDataNurseDetail[rowIndex].ConfigName.ToLower().Trim() == "rmc phase v")
                                {
                                    int resourceRequirementID = 0;
                                    bool flag = false;
                                    flag = int.TryParse(returnText, out resourceRequirementID);
                                    if (flag)
                                    {
                                        _objectNursePDADetail.ResourceRequirementID = resourceRequirementID;
                                    }
                                }
                                else if (genericBEImportDataNurseDetail[rowIndex].ConfigName.ToLower().Trim() == "ascension tcab")
                                {
                                    _objectNursePDADetail.CognitiveCategory = returnText;
                                }
                            }
                        }

                        if (_objectNursePDADetail.ResourceRequirementID == null)
                        {
                            _objectNursePDADetail.ResourceRequirementID = 0;
                        }
                        _entityNursePDADetail.Add(_objectNursePDADetail);
                        index = 0;
                        if (error == true)
                        {
                            errorFlag = error;
                        }
                        error = false;
                        flagLastLocationPass = false;
                        _objectNursePDADetail = new NursePDADetail();
                    }
                }

                return _entityNursePDADetail;
            }
            catch (Exception ex)
            {
                ex.Data.Add("Function", "SaveNursePDADetail");
                ex.Data.Add("Class", "BSSDAValidation");
                throw ex;
            }
            finally
            {
                _objectNursePDADetail = null;
                _genericLastLocation = null;
                _genericLocation = null;
                _genericActivity = null;
                _genericSubActivity = null;
                _genericResourceRequirement = null;
                _objectUserInfo = null;
                _objectLocation = null;
                _objectActivity = null;
                _objectSubActivity = null;
                _objectResourceRequirement = null;
                _objectGenericValidation = null;
                genericBEImportDataNurseDetail = null;
            }
        }


        /// <summary>
        /// Save Infomation about Nurse and PDA device.
        /// Created By : Davinder Kumar.       
        /// </summary>
        /// <param name="genericBEImportData">Generic List Of Import Data in Business Entity.</param>
        /// <returns>Nurse Infomation and PDA Device Infomation.</returns>
        private RMC.DataService.NursePDAInfo ValidateNursePDAInfo(List<BEImportData> genericBEImportDataNurseInfo, int hospitalDemographicDetailID)
        {
            try
            {
                _objectNursePDAInfo = new NursePDAInfo();
                //_objectUserInfo = ((List<UserInfo>)HttpContext.Current.Session["UserInformation"])[0];
                _objectUserInfo = ((List<RMC.DataService.UserInfo>)(BSSerialization.Deserialize<List<RMC.DataService.UserInfo>>(HttpContext.Current.Session["UserInformation"] as MemoryStream)))[0];

                if (genericBEImportDataNurseInfo[0].ConfigName.Length == 0)
                {
                    _objectNursePDAInfo.IsErrorInConfigName = true;
                    _objectNursePDAInfo.IsErrorExist = true;
                }
                _objectNursePDAInfo.ConfigName = genericBEImportDataNurseInfo[0].ConfigName;

                _objectNursePDAInfo.FileRefference = genericBEImportDataNurseInfo[0].FileRef;
                if (genericBEImportDataNurseInfo[0].PDAName.Length == 0)
                {
                    _objectNursePDAInfo.IsErrorInPDAUserName = true;
                    _objectNursePDAInfo.IsErrorExist = true;
                }
                _objectNursePDAInfo.PDAUserName = genericBEImportDataNurseInfo[0].PDAName;
                _objectNursePDAInfo.SoftwareVersion = genericBEImportDataNurseInfo[0].SoftwareVersion;
                _objectNursePDAInfo.HospitalDemographicID = hospitalDemographicDetailID;
                _objectNursePDAInfo.Year = genericBEImportDataNurseInfo[0].Year;
                _objectNursePDAInfo.Month = genericBEImportDataNurseInfo[0].Month;
                _objectNursePDAInfo.CreatedBy = _objectUserInfo.FirstName + " " + _objectUserInfo.LastName;
                _objectNursePDAInfo.CreatedDate = DateTime.Now;

                foreach (BEImportData objectBEImportDataNurseInfo in genericBEImportDataNurseInfo)
                {
                    int infoSequence;
                    bool flagInfoSeq = int.TryParse(objectBEImportDataNurseInfo.InfoSequence, out infoSequence);

                    if (flagInfoSeq)
                    {
                        if (infoSequence == 1)
                        {
                            //if (objectBEImportDataNurseInfo.Header.Length == 0)
                            //{
                            //    //_objectNursePDAInfo.IsErrorInUnitName = true;
                            //    //_objectNursePDAInfo.IsErrorExist = true;
                            //}
                            if (objectBEImportDataNurseInfo.KeyData.ToLower().Trim() == "unit name")
                            {
                                _objectNursePDAInfo.UnitName = objectBEImportDataNurseInfo.Header;
                            }
                            else if (objectBEImportDataNurseInfo.KeyData.ToLower().Trim() == "enter hospital unit")
                            {
                                _objectNursePDAInfo.UnitName = objectBEImportDataNurseInfo.Header;
                            }
                            else if (objectBEImportDataNurseInfo.KeyData.ToLower().Trim() == "nurse name")
                            {
                                if (objectBEImportDataNurseInfo.Header != null)
                                {
                                    _objectNursePDAInfo.NurseName = objectBEImportDataNurseInfo.Header;
                                }
                                else
                                {
                                    _objectNursePDAInfo.NurseName = string.Empty;
                                }
                            }
                            else if (objectBEImportDataNurseInfo.KeyData.ToLower().Trim() == "enter nurse name")
                            {
                                if (objectBEImportDataNurseInfo.Header != null)
                                {
                                    _objectNursePDAInfo.NurseName = objectBEImportDataNurseInfo.Header;
                                }
                                else
                                {
                                    _objectNursePDAInfo.NurseName = string.Empty;
                                }
                            }
                            else if (objectBEImportDataNurseInfo.KeyData.ToLower().Trim() == "avg pat. census for nurse")
                            {
                                int importDataNurseInfo;
                                bool flag = int.TryParse(objectBEImportDataNurseInfo.Header, out importDataNurseInfo);
                                if (flag)
                                {
                                    _objectNursePDAInfo.PatientsPerNurse = importDataNurseInfo;
                                }
                                else
                                {
                                    //_objectNursePDAInfo.IsErrorInPatientsPerNurse = true;
                                    //_objectNursePDAInfo.IsErrorExist = true;
                                    _objectNursePDAInfo.PatientsPerNurse = 0;
                                }
                            }
                        }
                        else if (infoSequence == 2)
                        {
                            //if (objectBEImportDataNurseInfo.Header.Length == 0)
                            //{
                            //    _objectNursePDAInfo.IsErrorInNurseName = true;
                            //    _objectNursePDAInfo.IsErrorExist = true;
                            //}
                            if (objectBEImportDataNurseInfo.KeyData.ToLower().Trim() == "unit name")
                            {
                                _objectNursePDAInfo.UnitName = objectBEImportDataNurseInfo.Header;
                            }
                            else if (objectBEImportDataNurseInfo.KeyData.ToLower().Trim() == "enter hospital unit")
                            {
                                _objectNursePDAInfo.UnitName = objectBEImportDataNurseInfo.Header;
                            }
                            else if (objectBEImportDataNurseInfo.KeyData.ToLower().Trim() == "enter nurse name")
                            {
                                if (objectBEImportDataNurseInfo.Header != null)
                                {
                                    _objectNursePDAInfo.NurseName = objectBEImportDataNurseInfo.Header;
                                }
                                else
                                {
                                    _objectNursePDAInfo.NurseName = string.Empty;
                                }
                            }
                            else if (objectBEImportDataNurseInfo.KeyData.ToLower().Trim() == "nurse name")
                            {
                                if (objectBEImportDataNurseInfo.Header != null)
                                {
                                    _objectNursePDAInfo.NurseName = objectBEImportDataNurseInfo.Header;
                                }
                                else
                                {
                                    _objectNursePDAInfo.NurseName = string.Empty;
                                }
                            }
                            else if (objectBEImportDataNurseInfo.KeyData.ToLower().Trim() == "avg pat. census for nurse")
                            {
                                int importDataNurseInfo;
                                bool flag = int.TryParse(objectBEImportDataNurseInfo.Header, out importDataNurseInfo);
                                if (flag)
                                {
                                    _objectNursePDAInfo.PatientsPerNurse = importDataNurseInfo;
                                }
                                else
                                {
                                    //_objectNursePDAInfo.IsErrorInPatientsPerNurse = true;
                                    //_objectNursePDAInfo.IsErrorExist = true;
                                    _objectNursePDAInfo.PatientsPerNurse = 0;
                                }
                            }
                        }
                        else if (infoSequence == 3)
                        {
                            if (objectBEImportDataNurseInfo.KeyData.ToLower().Trim() == "unit name")
                            {
                                _objectNursePDAInfo.UnitName = objectBEImportDataNurseInfo.Header;
                            }
                            else if (objectBEImportDataNurseInfo.KeyData.ToLower().Trim() == "nurse name")
                            {
                                if (objectBEImportDataNurseInfo.Header != null)
                                {
                                    _objectNursePDAInfo.NurseName = objectBEImportDataNurseInfo.Header;
                                }
                                else
                                {
                                    _objectNursePDAInfo.NurseName = string.Empty;
                                }
                            }
                            else if (objectBEImportDataNurseInfo.KeyData.ToLower().Trim() == "avg pat. census for nurse")
                            {
                                int importDataNurseInfo;
                                bool flag = int.TryParse(objectBEImportDataNurseInfo.Header, out importDataNurseInfo);
                                if (flag)
                                {
                                    _objectNursePDAInfo.PatientsPerNurse = importDataNurseInfo;
                                }
                                else
                                {
                                    //_objectNursePDAInfo.IsErrorInPatientsPerNurse = true;
                                    //_objectNursePDAInfo.IsErrorExist = true;
                                    _objectNursePDAInfo.PatientsPerNurse = 0;
                                }
                            }
                            else if (objectBEImportDataNurseInfo.KeyData.ToLower().Trim() == "number patients assigned")
                            {
                                int importDataNurseInfo;
                                bool flag = int.TryParse(objectBEImportDataNurseInfo.Header, out importDataNurseInfo);
                                if (flag)
                                {
                                    _objectNursePDAInfo.PatientsPerNurse = importDataNurseInfo;
                                }
                                else
                                {
                                    //_objectNursePDAInfo.IsErrorInPatientsPerNurse = true;
                                    //_objectNursePDAInfo.IsErrorExist = true;
                                    _objectNursePDAInfo.PatientsPerNurse = 0;
                                }
                            }
                        }
                    }
                }

                if (_objectNursePDAInfo.ConfigName.ToLower().Trim() == "Ascension TCAB".ToLower().Trim())
                {
                    _objectNursePDAInfo.NurseName = string.Empty;
                }
                else if (_objectNursePDAInfo.ConfigName.ToLower().Trim() == "IHI Phase II".ToLower().Trim())
                {
                    if (_objectNursePDAInfo.NurseName == null || _objectNursePDAInfo.NurseName == string.Empty)
                    {
                        _objectNursePDAInfo.NurseName = string.Empty;
                    }
                    _objectNursePDAInfo.UnitName = string.Empty;
                }
                else if (_objectNursePDAInfo.ConfigName.ToLower().Trim() == "IHI Phase IV".ToLower().Trim() ||
                    _objectNursePDAInfo.ConfigName.ToLower().Trim() == "RMC Phase IV".ToLower().Trim())
                {
                    _objectNursePDAInfo.UnitName = string.Empty;
                }

                if (_objectNursePDAInfo.NurseName == null)
                {
                    _objectNursePDAInfo.NurseName = string.Empty;
                }
                else if (_objectNursePDAInfo.UnitName == null)
                {
                    _objectNursePDAInfo.UnitName = string.Empty;
                }
                else if (_objectNursePDAInfo.PatientsPerNurse == null)
                {
                    _objectNursePDAInfo.PatientsPerNurse = 0;
                }

                //2011-0228 [amelinc] Worked on the phone with Nelson
                //Figured out some RMC Phase VI files not including the UnitName in the filename
                //were failing to upload with DB Error, UnitName can't be null.
                //Decided to set to blank if UnitName is not present for all cases.
                if (_objectNursePDAInfo.UnitName == null)
                {
                    _objectNursePDAInfo.UnitName = string.Empty;
                }

                return _objectNursePDAInfo;
            }
            catch (Exception ex)
            {
                ex.Data.Add("Function", "SaveNursePDAInfo");
                ex.Data.Add("Class", "BSDataValidation");
                throw ex;
            }
        }

        private string GetCognitiveCategoryResourcRequirement(List<BEImportData> objectGenericBEImportData, int rowIndex)
        {
            try
            {
                RMC.DataService.ResourceRequirement objectResourceRequirement = null;
                int ID = 0;
                string returnValue = string.Empty;
                _genericResourceRequirement = GetResourceRequirement();

                for (int index = rowIndex; index < objectGenericBEImportData.Count(); index++)
                {
                    if (objectGenericBEImportData[index].KeyDataSequence.Trim() == 0.ToString())
                    {
                        if (objectGenericBEImportData[index].KeyData.ToLower().Trim() == "cognitive categories" && objectGenericBEImportData[index].Header != string.Empty)
                        {
                            returnValue = objectGenericBEImportData[index].Header;
                        }
                        else
                        {
                            if (objectGenericBEImportData[index].KeyData.Length > 3 && objectGenericBEImportData[index].KeyData.Substring(0, 5).ToLower().Trim() != "notes")
                            {
                                if (objectGenericBEImportData[index].Header != string.Empty)
                                {
                                    objectResourceRequirement = _genericResourceRequirement.Find(delegate(RMC.DataService.ResourceRequirement objectRR)
                                                                {
                                                                    return objectRR.ResourceRequirement1.ToLower().Trim() == objectGenericBEImportData[index].Header.ToLower().Trim();
                                                                });

                                    if (objectResourceRequirement == null)
                                    {
                                        try
                                        {
                                            ID = InsertResourceRequirement(objectGenericBEImportData[index].KeyData.Trim());
                                        }
                                        catch
                                        {
                                            //2011-0406 A set of file has column AV hand edited with non-sensical values (such as "brekk")
                                            //This is causing the above logic to fail 
                                            //Particularly irrelevant as these files are Phase VI and the calling method doesn't use the result regardless of success
                                            //for Phase VI
                                            //SWALLOW THE EXCPETION AND BREAK OUT
                                            break;
                                        }
                                    }
                                    else
                                    {
                                        ID = objectResourceRequirement.ResourceRequirementID;
                                    }

                                    returnValue = ID.ToString();
                                    //[amelinc] 2011-0406 Why continue scanning the rest of the file? - adding this break
                                    break;
                                }
                            }
                        }
                    }
                }

                return returnValue;
            }
            catch (Exception ex)
            {
                ex.Data.Add("Function", "GetCognitiveCategoryResourcRequirement");
                ex.Data.Add("Class", "BSSDAValidation");
                throw ex;
            }
        }


        /// <summary>
        /// Get Valid Resource Requirement from Database.
        /// Created By : Davinder Kumar.
        /// Creation Date : July 02, 2009.
        /// Modified By : Davinder Kumar.
        /// Modified Date : July 14, 2009.
        /// </summary>
        /// <returns></returns>
        private List<RMC.DataService.ResourceRequirement> ValidateResourceRequirement()
        {
            try
            {
                RMC.DataService.RMCDataContext ObjectRMCDataContext = new RMCDataContext();
                List<RMC.DataService.ResourceRequirement> genericResourceRequirement = (from rr in ObjectRMCDataContext.ResourceRequirements
                                                                                        select rr).ToList<RMC.DataService.ResourceRequirement>();

                return genericResourceRequirement;
            }
            catch (Exception ex)
            {
                ex.Data.Add("Function", "ValidateResourceRequirement");
                ex.Data.Add("Class", "BSDataValidation");
                throw ex;
            }
            finally
            {
                _objectRMCDataContext.Dispose();
            }
        }

        /// <summary>
        /// Get Valid Sub-Activity from Database.
        /// Created By : Davinder Kumar.
        /// Creation Date : July 02, 2009.
        /// Modified By : Davinder Kumar.
        /// Modified Date : July 14, 2009.
        /// </summary>
        /// <returns></returns>
        private List<RMC.DataService.SubActivity> ValidateSubActivity()
        {
            try
            {
                RMC.DataService.RMCDataContext ObjectRMCDataContext = new RMCDataContext();
                List<RMC.DataService.SubActivity> genericSubActivity = (from a in ObjectRMCDataContext.SubActivities
                                                                        select a).ToList<RMC.DataService.SubActivity>();

                return genericSubActivity;
            }
            catch (Exception ex)
            {
                ex.Data.Add("Function", "ValidateSubActivity");
                ex.Data.Add("Class", "BSSDAValidation");
                throw ex;
            }
            finally
            {
                _objectRMCDataContext.Dispose();
            }
        }

        private int InsertLastLocation(string lastLocation)
        {
            int lastLocationID = 0;
            try
            {
                RMC.DataService.LastLocation objectLastLocation = new LastLocation();
                _objectRMCDataContext = new RMCDataContext();

                objectLastLocation.IsActive = true;
                objectLastLocation.LastLocation1 = lastLocation;
                objectLastLocation.RenameLastLocation = string.Empty;

                _objectRMCDataContext.LastLocations.InsertOnSubmit(objectLastLocation);
                _objectRMCDataContext.SubmitChanges();
                lastLocationID = objectLastLocation.LastLocationID;
            }
            catch (Exception ex)
            {
                ex.Data.Add("Function", "InsertLastLocation");
                ex.Data.Add("Class", "BSSDAValidation");
                throw ex;
            }
            finally
            {
                _objectRMCDataContext.Dispose();
            }

            return lastLocationID;
        }

        private int InsertResourceRequirement(string resourceRequirement)
        {
            int resourceRequirementID = 0;
            try
            {
                RMC.DataService.ResourceRequirement objectResourceRequirement = new ResourceRequirement();
                _objectRMCDataContext = new RMCDataContext();

                objectResourceRequirement.IsActive = true;
                objectResourceRequirement.ResourceRequirement1 = resourceRequirement;

                _objectRMCDataContext.ResourceRequirements.InsertOnSubmit(objectResourceRequirement);
                _objectRMCDataContext.SubmitChanges();
                resourceRequirementID = objectResourceRequirement.ResourceRequirementID;
            }
            catch (Exception ex)
            {
                ex.Data.Add("Function", "InsertResourceRequirement");
                ex.Data.Add("Class", "BSSDAValidation");
                throw ex;
            }
            finally
            {
                _objectRMCDataContext.Dispose();
            }

            return resourceRequirementID;
        }

        private string AutoCorrection(string checkWord)
        {
            Dictionary<string, string> objectDictionary = new Dictionary<string, string>();
            try
            {
                if (checkWord != null)
                {
                    string result = string.Empty;
                    bool flag = false;

                    objectDictionary.Add("assess- ment", "Assessment");
                    objectDictionary.Add("meds activities", "Meds Activity");

                    if (objectDictionary.ContainsKey(checkWord.ToLower().Trim()))
                    {
                        flag = objectDictionary.TryGetValue(checkWord.ToLower().Trim(), out result);
                    }
                    if (flag)
                    {
                        return result;
                    }
                    else
                    {
                        return checkWord;
                    }
                }
                else
                {
                    return string.Empty;
                }
            }
            catch (Exception ex)
            {
                ex.Data.Add("Function", "AutoCorrection");
                ex.Data.Add("Class", "BSSDAValidation");
                throw ex;
            }
            finally
            {
                objectDictionary = null;
            }
        }

        private int EditedLastLocation(int lastLocationID, string lastLocation)
        {
            try
            {
                RMC.DataService.LastLocation objectLastLocation = null;
                List<RMC.DataService.LastLocation> objectGenericLastLocation = null;
                RMC.DataService.RMCDataContext objectRMCDataContext = new RMCDataContext();

                if (lastLocation != null && lastLocation != string.Empty)
                {
                    lastLocation = CheckLastKeyWordInLastLocationData(lastLocation);
                    if (lastLocationID > 0)
                    {
                        objectLastLocation = objectRMCDataContext.LastLocations.Single(ll => ll.LastLocationID == lastLocationID && ll.IsActive == true);

                        objectLastLocation.LastLocation1 = lastLocation;
                    }
                    else
                    {
                        objectGenericLastLocation = (from ll in objectRMCDataContext.LastLocations
                                                     where ll.IsActive == true
                                                     select ll).ToList();

                        objectLastLocation = objectGenericLastLocation.Find(delegate(RMC.DataService.LastLocation objectNewLastLocation)
                                            {
                                                return objectNewLastLocation.LastLocation1.ToLower().Trim() == lastLocation.ToLower().Trim();
                                            });
                        if (objectLastLocation == null)
                        {
                            lastLocationID = InsertLastLocation(lastLocation);
                        }
                        else
                        {
                            lastLocationID = objectLastLocation.LastLocationID;
                        }
                    }
                }

                objectRMCDataContext.SubmitChanges();
                return lastLocationID;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private int EditedResourceRequirement(int resourceRequirementID, string resourceRequirement)
        {
            try
            {
                RMC.DataService.ResourceRequirement objectResourceRequirement = null;
                List<RMC.DataService.ResourceRequirement> objectGenericResourceRequirement = null;
                RMC.DataService.RMCDataContext objectRMCDataContext = new RMCDataContext();

                if (resourceRequirement != null && resourceRequirement != string.Empty)
                {
                    if (resourceRequirementID > 0)
                    {
                        objectResourceRequirement = objectRMCDataContext.ResourceRequirements.Single(rr => rr.ResourceRequirementID == resourceRequirementID && rr.IsActive == true);

                        objectResourceRequirement.ResourceRequirement1 = resourceRequirement;
                    }
                    else
                    {
                        objectGenericResourceRequirement = (from rr in objectRMCDataContext.ResourceRequirements
                                                            where rr.IsActive == true
                                                            select rr).ToList();

                        objectResourceRequirement = objectGenericResourceRequirement.Find(delegate(RMC.DataService.ResourceRequirement objectNewResourceRequirement)
                        {
                            return objectNewResourceRequirement.ResourceRequirement1.ToLower().Trim() == resourceRequirement.ToLower().Trim();
                        });
                        if (objectResourceRequirement == null)
                        {
                            resourceRequirementID = InsertResourceRequirement(resourceRequirement);
                        }
                        else
                        {
                            resourceRequirementID = objectResourceRequirement.ResourceRequirementID;
                        }
                    }
                }
                objectRMCDataContext.SubmitChanges();
                return resourceRequirementID;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private string CheckLastKeyWordInLastLocationData(string keyWord)
        {
            try
            {
                string subString;
                subString = keyWord.Substring(0, 5);
                if (subString.Trim().ToLower() != "last")
                {
                    keyWord = "Last " + keyWord;
                }

                return keyWord;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private bool checkLastLocationForSpecificCase(string configName, string Value)
        {
            try
            {
                List<RMC.BusinessEntities.BEDropDownListData> objectKeyValuePairs = SpecificLastLocationValues();

                return objectKeyValuePairs.Exists(delegate(RMC.BusinessEntities.BEDropDownListData objectBEKeyValue)
                {
                    return objectBEKeyValue.Key.ToLower().Trim() == configName.ToLower().Trim() && objectBEKeyValue.Value.ToLower().Trim() == Value.ToLower().Trim();
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private List<RMC.BusinessEntities.BEDropDownListData> SpecificLastLocationValues()
        {
            try
            {
                List<RMC.BusinessEntities.BEDropDownListData> objectKeyValuePairs = new List<BEDropDownListData>();
                RMC.BusinessEntities.BEDropDownListData objectBEConfigNameIHIPhaseIV = new BEDropDownListData();
                objectBEConfigNameIHIPhaseIV.Key = "IHI Phase IV";
                objectBEConfigNameIHIPhaseIV.Value = "Conf Room";
                objectKeyValuePairs.Add(objectBEConfigNameIHIPhaseIV);

                RMC.BusinessEntities.BEDropDownListData objectBEConfigNameRMCPhaseIV = new BEDropDownListData();
                objectBEConfigNameRMCPhaseIV.Key = "RMC Phase IV";
                objectBEConfigNameRMCPhaseIV.Value = "Conf Room";
                objectKeyValuePairs.Add(objectBEConfigNameRMCPhaseIV);

                return objectKeyValuePairs;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

    }
}
