using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using RMC.DataService;
using RMC.BusinessEntities;


namespace RMC.BussinessService
{
    public class BSDataValidation
    {

        #region Variables

        //Data Context Object.
        RMC.DataService.RMCDataContext _objectRMCDataContext = null;

        //Data Service Objects.
        BSImportData _objectDSImportData = null;
        RMC.DataService.ValidateFileField _objectValidateFileField = null;
        RMC.DataService.NursePDAInfo _objectNursePDAInfo = null;
        RMC.DataService.NursePDADetail _objectNursePDADetail = null;
        RMC.DataService.LastLocation _objectLastLocation = null;
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
        List<LastLocation> _genericLastLocation = null;
        List<Location> _genericLocation = null;
        List<Activity> _genericActivity = null;
        List<SubActivity> _genericSubActivity = null;
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

                _objectNursePDAInfo.NursePDADetails.AddRange(entityNursePDADetail);
                _objectRMCDataContext.HospitalUploads.InsertOnSubmit(hospitalUpload);
                _objectRMCDataContext.NursePDAInfos.InsertOnSubmit(_objectNursePDAInfo);
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
                _objectRMCDataContext = new RMCDataContext();

                _objectDSImportData = new BSImportData();
                //Get Data From Sda file in Data Service.
                _genricBEImportData = _objectDSImportData.ImportData(fileName, originalFileName, hospitalUpload.Year, hospitalUpload.Month, out flagNonStandardFiles);
                //Extract Nurse and PDA Device Information.
                genericBEImportDataNurseInfo = _genricBEImportData.FindAll(delegate(RMC.BusinessEntities.BEImportData obj) { return obj.KeyDataSequence == 0.ToString() && obj.Header != string.Empty ? true : false; });
                //Validate Nurse Infomation.
                _objectNursePDAInfo = ValidateNursePDAInfo(genericBEImportDataNurseInfo, hospitalDemographicID);
                //Validate and Save Nurse PDA Detail.
                System.Data.Linq.EntitySet<RMC.DataService.NursePDADetail> entitySetNursePDADetail = ValidateNursePDADetail(_genricBEImportData, out errorFlag);
                _objectNursePDAInfo.IsErrorInDetailData = errorFlag;
                flag = SaveFileDataInDatabase(entitySetNursePDADetail, hospitalUpload);
                //Update NursePDA Information.
                
            }
            catch (Exception ex)
            {
                ex.Data.Add("Function", "SaveFileData");
                ex.Data.Add("Class", "BSDataValidation");
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
        /// Get Valid Default Category from Database.
        /// Created By : Davinder Kumar.
        /// Creation Date : July 02, 2009.
        /// </summary>
        /// <returns></returns>
        private List<RMC.DataService.CategoryGroup> ValidateCategoryGroup()
        {
            try
            {
                RMC.DataService.RMCDataContext ObjectRMCDataContext = new RMCDataContext();
                List<RMC.DataService.CategoryGroup> genericCategoryGroup = (from cg in ObjectRMCDataContext.CategoryGroups
                                                                            select cg).ToList<RMC.DataService.CategoryGroup>();

                return genericCategoryGroup;
            }
            catch (Exception ex)
            {
                ex.Data.Add("Function", "ValidateCategoryGroup");
                ex.Data.Add("Class", "BSDataValidation");
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
        private List<RMC.DataService.Activity> ValidateActivity(int validateID)
        {
            try
            {
                RMC.DataService.RMCDataContext ObjectRMCDataContext = new RMCDataContext();
                List<RMC.DataService.Activity> genericActivity = (from a in ObjectRMCDataContext.Activities
                                                                  where a.ValidateID == validateID
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
        /// Creation Date : July 01, 2009.
        /// Modified By : Davinder Kumar.
        /// Modified Date : July 14, 2009.
        /// </summary>
        /// <returns></returns>
        private List<RMC.DataService.LastLocation> ValidateLastLocation(int validateID)
        {
            try
            {
                RMC.DataService.RMCDataContext ObjectRMCDataContext = new RMCDataContext();
                List<RMC.DataService.LastLocation> genericLastLocation = (from ll in ObjectRMCDataContext.LastLocations
                                                                          where ll.ValidateID == validateID
                                                                          select ll).ToList<RMC.DataService.LastLocation>();

                return genericLastLocation;
            }
            catch (Exception ex)
            {
                ex.Data.Add("Function", "ValidateLastLocation");
                ex.Data.Add("Class", "BSDataValidation");
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
        private List<RMC.DataService.Location> ValidateLocation(int validateID)
        {
            try
            {
                RMC.DataService.RMCDataContext ObjectRMCDataContext = new RMCDataContext();
                List<RMC.DataService.Location> genericLocation = (from l in ObjectRMCDataContext.Locations
                                                                  where l.ValidateID == validateID
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
        /// Save Detail Infomation from PDA device To Application Object.
        /// Created By : Davinder Kumar.
        /// Creation Date : July 01, 2009.
        /// Modified By : Davinder Kumar.
        /// Modified Date : July 14, 2009.
        /// </summary>
        /// <param name="genericBEImportDataNurseDetail">Generic List Of Import Data in Business Entity.</param>
        /// <returns>Nurse Infomation and PDA Device Infomation.</returns>
        private System.Data.Linq.EntitySet<RMC.DataService.NursePDADetail> ValidateNursePDADetail(List<BEImportData> genericBEImportDataNurseDetail, out bool errorFlag)
        {
            bool error = false;
            try
            {
                int index = 0;
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

                _objectUserInfo = ((List<UserInfo>)HttpContext.Current.Session["UserInformation"])[0];

                //Get Data from Sda file.
                _objectValidateFileField = GetValidFileFields(genericBEImportDataNurseDetail[0].ConfigName);
                //Get Valid LastLocation from Database.
                _genericLastLocation = ValidateLastLocation(_objectValidateFileField.ValidateID);
                //Get Valid Location from Database.
                _genericLocation = ValidateLocation(_objectValidateFileField.ValidateID);
                //Get Valid Activity from Database.
                _genericActivity = ValidateActivity(_objectValidateFileField.ValidateID);
                //Get Valid Sub-Activity from Database.
                _genericSubActivity = ValidateSubActivity(_objectValidateFileField.ValidateID);
                //Get Valid Category Group from Database.
                //_genericCategoryGroup = ValidateCategoryGroup();
                //Get Valid Resource Requirement from Database.
                _genericResourceRequirement = ValidateResourceRequirement(_objectValidateFileField.ValidateID);
                //Get Valid User Type from Database.
                //_genericValueAddedType = ValidateUserType();

                foreach (BEImportData objectBEImportDataNursePDADetail in genericBEImportDataNurseDetail)
                {
                    //Record Sequence.
                    if (objectBEImportDataNursePDADetail.KeyDataSequence.Trim() == 1.ToString())
                    {
                        //Validate Field according to File Type of Sda.
                        if (Convert.ToBoolean(_objectValidateFileField.IsUseLastLocation))
                        {
                            //Index, 0 use for LastLocation
                            if (index == 0)
                            {
                                _objectLastLocation = new LastLocation();

                                _objectLastLocation = _genericLastLocation.Find(delegate(RMC.DataService.LastLocation objectLastLocation)
                                { return objectLastLocation.LastLocation1.ToLower().Trim() == objectBEImportDataNursePDADetail.KeyData.ToLower().Trim(); });
                                if (_objectLastLocation != null)
                                {
                                    _objectNursePDADetail.LastLocationID = _objectLastLocation.LastLocationID;
                                    _objectNursePDADetail.LastLocationDate = objectBEImportDataNursePDADetail.Date;
                                    _objectNursePDADetail.LastLocationTime = objectBEImportDataNursePDADetail.Time;                                    
                                }
                                else
                                {
                                    _objectNursePDADetail.LastLocationID = 0;
                                    _objectNursePDADetail.IsErrorInLastLocation = true;
                                    if (Convert.ToBoolean(_objectNursePDADetail.IsErrorInLastLocation))
                                    {
                                        _objectNursePDADetail.LastLocationText = objectBEImportDataNursePDADetail.KeyData.ToLower().Trim();
                                        _objectNursePDADetail.LastLocationDate = objectBEImportDataNursePDADetail.Date;
                                        _objectNursePDADetail.LastLocationTime = objectBEImportDataNursePDADetail.Time;
                                    }
                                    error = true;
                                }
                                index++;
                                continue;
                            }
                        }
                        //When Last Location doesn't Exist.
                        if (index == 0)
                            index++;

                        if (Convert.ToBoolean(_objectValidateFileField.IsUseLocation))
                        {
                            //Index, 1 use for Location.
                            if (index == 1)
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
                        }

                        if (Convert.ToBoolean(_objectValidateFileField.IsUseActivity))
                        {
                            //Index, 2 use for Activity.
                            if (index == 2)
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
                    }
                    if (objectBEImportDataNursePDADetail.KeyDataSequence.Trim() == 2.ToString())
                    {
                        if (Convert.ToBoolean(_objectValidateFileField.IsUseActivity))
                        {
                            //Index, 2 use for Activity.
                            if (index == 2)
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
                        }

                        if (Convert.ToBoolean(_objectValidateFileField.IsUseSubActivity))
                        {
                            //Index, 3 use for SubActivity.
                            if (index == 3)
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
                        }

                        //if (Convert.ToBoolean(_objectValidateFileField.IsUseDefaultCategory))
                        //{
                        //    _objectCategoryGroup = new CategoryGroup();

                        //    _objectCategoryGroup = _genericCategoryGroup.Find(delegate(RMC.DataService.CategoryGroup objectCategoryGroup)
                        //    { return objectCategoryGroup.CategoryGroup1.ToLower().Trim() == objectBEImportDataNursePDADetail.DefaultCategory.ToLower().Trim(); });
                        //    if (_objectCategoryGroup != null)
                        //    {
                        //        _objectNursePDADetail.CategoryGroupID = _objectCategoryGroup.CategoryGroupID;
                        //    }
                        //    else
                        //    {
                        //        _objectNursePDADetail.CategoryGroupID = 0;
                        //        error = true;
                        //    }
                        //}
                        if (Convert.ToBoolean(_objectValidateFileField.IsUseResourceRequirement))
                        {
                            _objectResourceRequirement = new ResourceRequirement();

                            _objectResourceRequirement = _genericResourceRequirement.Find(delegate(RMC.DataService.ResourceRequirement objectResourceRequirement)
                            { return objectResourceRequirement.ResourceRequirement1.ToLower().Trim() == objectBEImportDataNursePDADetail.Header.ToLower().Trim(); });
                            if (_objectResourceRequirement != null)
                            {
                                _objectNursePDADetail.ResourceRequirementID = _objectResourceRequirement.ResourceRequirementID;
                            }
                            else if (objectBEImportDataNursePDADetail.Header != string.Empty)
                            {
                                _objectNursePDADetail.ResourceRequirementID = 0;
                                _objectNursePDADetail.IsErrorInResourceRequirement = true;
                                if (Convert.ToBoolean(_objectNursePDADetail.IsErrorInResourceRequirement))
                                {
                                    _objectNursePDADetail.ResourceText = objectBEImportDataNursePDADetail.Header.ToLower().Trim();
                                }
                                error = true;
                            }
                            else
                            {
                                _objectNursePDADetail.ResourceRequirementID = 0;
                            }
                        }
                        //if (Convert.ToBoolean(_objectValidateFileField.IsUseType))
                        //{
                        //    _objectValueAddedType = new ValueAddedType();

                        //    _objectValueAddedType = _genericValueAddedType.Find(delegate(RMC.DataService.ValueAddedType objectValueAddedType)
                        //    { return objectValueAddedType.Abbreviation.ToLower().Trim() == objectBEImportDataNursePDADetail.ValueAddedType.ToLower().Trim(); });
                        //    if (_objectValueAddedType != null)
                        //    {
                        //        _objectNursePDADetail.TypeID = _objectValueAddedType.TypeID;
                        //    }
                        //    else
                        //    {
                        //        _objectNursePDADetail.TypeID = 0;
                        //        error = true;
                        //    }
                        //}
                        if (_objectUserInfo != null)
                        {
                            _objectNursePDADetail.CreatedBy = _objectUserInfo.FirstName + " " + _objectUserInfo.LastName;
                            _objectNursePDADetail.CreatedDate = DateTime.Now;
                            _objectNursePDADetail.IsActive = true;
                            _objectNursePDADetail.IsDeleted = false;
                        }
                        _objectNursePDADetail.IsErrorExist = error;
                        _entityNursePDADetail.Add(_objectNursePDADetail);
                        index = 0;
                        if (error == true)
                        {
                            errorFlag = error;
                        }
                        error = false;
                        _objectNursePDADetail = new NursePDADetail();
                    }
                }

                return _entityNursePDADetail;
            }
            catch (Exception ex)
            {
                ex.Data.Add("Function", "SaveNursePDADetail");
                ex.Data.Add("Class", "BSDataValidation");
                throw ex;
            }
            finally
            {
                //_genericNursePDADetail = null;
                _objectNursePDADetail = null;
                _genericLastLocation = null;
                _genericLocation = null;
                _genericActivity = null;
                _genericSubActivity = null;
                //_genericCategoryGroup = null;
                _genericResourceRequirement = null;
                //_genericValueAddedType = null;
                _objectUserInfo = null;
                _objectValidateFileField = null;
                _objectLastLocation = null;
                _objectLocation = null;
                _objectActivity = null;
                _objectSubActivity = null;
                //_objectCategoryGroup = null;
                _objectResourceRequirement = null;
                //_objectValueAddedType = null;
                genericBEImportDataNurseDetail = null;
            }
        }

        /// <summary>
        /// Save Infomation about Nurse and PDA device.
        /// Created By : Davinder Kumar.
        /// Creation Date : July 01, 2009.
        /// </summary>
        /// <param name="genericBEImportData">Generic List Of Import Data in Business Entity.</param>
        /// <returns>Nurse Infomation and PDA Device Infomation.</returns>
        private RMC.DataService.NursePDAInfo ValidateNursePDAInfo(List<BEImportData> genericBEImportDataNurseInfo, int hospitalDemographicDetailID)
        {
            try
            {
                _objectNursePDAInfo = new NursePDAInfo();
                _objectUserInfo = ((List<UserInfo>)HttpContext.Current.Session["UserInformation"])[0];

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
                            if (objectBEImportDataNurseInfo.Header.Length == 0)
                            {
                                _objectNursePDAInfo.IsErrorInUnitName = true;
                                _objectNursePDAInfo.IsErrorExist = true;
                            }
                            _objectNursePDAInfo.UnitName = objectBEImportDataNurseInfo.Header;
                        }
                        else if (infoSequence == 2)
                        {
                            if (objectBEImportDataNurseInfo.Header.Length == 0)
                            {
                                _objectNursePDAInfo.IsErrorInNurseName = true;
                                _objectNursePDAInfo.IsErrorExist = true;
                            }
                            _objectNursePDAInfo.NurseName = objectBEImportDataNurseInfo.Header;
                        }
                        else if (infoSequence == 3)
                        {
                            int importDataNurseInfo;
                            bool flag = int.TryParse(objectBEImportDataNurseInfo.Header, out importDataNurseInfo);
                            if (flag)
                            {
                                _objectNursePDAInfo.PatientsPerNurse = importDataNurseInfo;
                            }
                            else
                            {
                                _objectNursePDAInfo.IsErrorInPatientsPerNurse = true;
                                _objectNursePDAInfo.IsErrorExist = true;
                                _objectNursePDAInfo.PatientsPerNurse = 0;
                            }
                        }
                    }
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

        /// <summary>
        /// Get Valid Resource Requirement from Database.
        /// Created By : Davinder Kumar.
        /// Creation Date : July 02, 2009.
        /// Modified By : Davinder Kumar.
        /// Modified Date : July 14, 2009.
        /// </summary>
        /// <returns></returns>
        private List<RMC.DataService.ResourceRequirement> ValidateResourceRequirement(int validateID)
        {
            try
            {
                RMC.DataService.RMCDataContext ObjectRMCDataContext = new RMCDataContext();
                List<RMC.DataService.ResourceRequirement> genericResourceRequirement = (from rr in ObjectRMCDataContext.ResourceRequirements
                                                                                        where rr.ValidateID == validateID
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
        private List<RMC.DataService.SubActivity> ValidateSubActivity(int validateID)
        {
            try
            {
                RMC.DataService.RMCDataContext ObjectRMCDataContext = new RMCDataContext();
                List<RMC.DataService.SubActivity> genericSubActivity = (from a in ObjectRMCDataContext.SubActivities
                                                                        where a.ValidateID == validateID
                                                                        select a).ToList<RMC.DataService.SubActivity>();

                return genericSubActivity;
            }
            catch (Exception ex)
            {
                ex.Data.Add("Function", "ValidateSubActivity");
                ex.Data.Add("Class", "BSDataValidation");
                throw ex;
            }
            finally
            {
                _objectRMCDataContext.Dispose();
            }
        }

        /// <summary>
        /// Get Valid User Type from Database.
        /// Created By : Davinder Kumar.
        /// Creation Date : July 02, 2009.
        /// </summary>
        /// <returns></returns>
        private List<RMC.DataService.ValueAddedType> ValidateUserType()
        {
            try
            {
                RMC.DataService.RMCDataContext ObjectRMCDataContext = new RMCDataContext();
                List<RMC.DataService.ValueAddedType> genericValueAddedType = (from ut in ObjectRMCDataContext.ValueAddedTypes
                                                                              where ut.IsActive == true
                                                                              select ut).ToList<RMC.DataService.ValueAddedType>();

                return genericValueAddedType;
            }
            catch (Exception ex)
            {
                ex.Data.Add("Function", "ValidateUserType");
                ex.Data.Add("Class", "BSDataValidation");
                throw ex;
            }
            finally
            {
                _objectRMCDataContext.Dispose();
            }
        }

        #endregion

        #region Predicate

        ///// <summary>
        ///// Predicate to Find Nurse Infomation.
        ///// That Predicate user in ValidateData.
        ///// Created By : Davinder Kumar.
        ///// Creation Date : July 01, 2009.
        ///// </summary>
        ///// <param name="objectBEImportData"></param>
        ///// <returns></returns>
        //public static bool GenericBEImportDataNurseInfoFindAll(BEImportData objectBEImportData)
        //{
        //    try
        //    {
        //        if (objectBEImportData.KeyDataSequence.Trim() == 0.ToString() && objectBEImportData.Header != string.Empty)
        //        {
        //            return true;
        //        }
        //        else
        //        {
        //            return false;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        ex.Data.Add("Predicate", "GenericBEImportDataNurseInfoFindAll");
        //        ex.Data.Add("Class", "BSDataValidation");
        //        throw ex;
        //    }
        //}

        #endregion

    }
    //End Of BSDataValidation Class.
}
//End Of Namespace.
