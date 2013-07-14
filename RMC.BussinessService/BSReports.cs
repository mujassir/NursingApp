using System;
using System.Data;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Data.Linq.SqlClient;

using System.Web;

namespace RMC.BussinessService
{
    public partial class BSReports
    {
        #region Variables

        //Data Context Object.
        RMC.DataService.RMCDataContext _objectRMCDataContext = new RMC.DataService.RMCDataContext();

        //Generic Object of Bussiness Entity.
        List<RMC.BusinessEntities.BEReports> _objectGenericBEReports = null;

        //Generic object of Data Service.
        List<RMC.BusinessEntities.BEHospitalBenchmarkSummary> _objectGenericBEHospitalBenchmarkSummary = null;

        //Fundamental Data Type
        bool _flag;

        #endregion

        #region Private Methods

        /// <summary>
        /// Adds fileds to HospitalBenchmark
        /// </summary>
        /// <returns></returns>
        private List<string> HospitalBenchmarkFields()
        {
            try
            {
                List<string> objectGenericHospitalBenchmarkFields = new List<string>();

                objectGenericHospitalBenchmarkFields.Add("Unit Type");
                objectGenericHospitalBenchmarkFields.Add("Unit Size");
                objectGenericHospitalBenchmarkFields.Add("Budgeted Patient Per Nurse");
                objectGenericHospitalBenchmarkFields.Add("Doc by Exception");
                objectGenericHospitalBenchmarkFields.Add("% Electronic Documentation");
                objectGenericHospitalBenchmarkFields.Add("Pharmacy Type");
                objectGenericHospitalBenchmarkFields.Add("Hospital Beds - Size");
                objectGenericHospitalBenchmarkFields.Add("Data Collected Begin-End Dates");
                objectGenericHospitalBenchmarkFields.Add("State");
                objectGenericHospitalBenchmarkFields.Add("Demographic (Urban/Community/Regional)");

                return objectGenericHospitalBenchmarkFields;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Gets data of Category Profile according to the profileTypeId given in parameter
        /// </summary>
        private List<RMC.BusinessEntities.BECategoryProfile> GetCategoryProfileDataByProfileID(int profileTypeID)
        {
            try
            {
                List<RMC.BusinessEntities.BECategoryProfile> objectGenericBECategoryProfile = (from cp in _objectRMCDataContext.CategoryProfiles
                                                                                               where cp.ProfileTypeID == profileTypeID
                                                                                               select new RMC.BusinessEntities.BECategoryProfile
                                                                                               {
                                                                                                   ActivityID = cp.Validation.ActivityID.Value,
                                                                                                   CategoryAssignmentID = cp.CategoryAssignmentID.Value,
                                                                                                   LocationID = cp.Validation.LocationID.Value,
                                                                                                   SubActivityID = cp.Validation.SubActivityID.Value
                                                                                               }).ToList();

                return objectGenericBECategoryProfile;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Gets data of hospitals for benchmarkSummary report according to the values given in parameter
        /// </summary>
        private List<RMC.BusinessEntities.BEValidation> SearchHospitalsDataForBenchmark(int? firstYear, int? lastYear, int? firstMonth, int? lastMonth, int? hospitalUnitID, int? bedInUnit, int optBedsInUnit, float? budgetedPatient, int optBudgetedPatient, string startDate, string endDate, int? electronicDocument, int optElectronicDocument, int docByException, string unitType, string pharmacyType, int optHospitalSize, int? hospitalSize)
        {
            List<RMC.BusinessEntities.BEValidation> objectGenericBEValidation;
            IQueryable<RMC.DataService.NursePDADetail> queryableNursePDADetail = null;

            try
            {
                if (endDate == null)
                {
                    DateTime datTimeFirst, datTimeLast;
                    if (firstYear != null && lastYear != null && firstYear.Value > 0 && lastYear.Value > 0)
                    {
                        datTimeFirst = Convert.ToDateTime(firstMonth.Value.ToString() + "/01/" + firstYear.Value.ToString());
                        if (lastMonth.Value != 12)
                        {
                            datTimeLast = Convert.ToDateTime((lastMonth + 1) + "/01/" + lastYear.Value.ToString());
                        }
                        else
                        {
                            datTimeLast = Convert.ToDateTime("01/01/" + (lastYear.Value + 1).ToString());
                        }

                        queryableNursePDADetail = (from v in _objectRMCDataContext.NursePDADetails
                                                   where (Convert.ToDateTime(v.NursePDAInfo.Month + "/01/" + v.NursePDAInfo.Year) >= datTimeFirst && Convert.ToDateTime((v.NursePDAInfo.Month != "12" ? (Convert.ToInt32(v.NursePDAInfo.Month) + 1).ToString() : "01") + "/01/" + v.NursePDAInfo.Year) < datTimeLast)
                                                   && v.NursePDAInfo.HospitalDemographicInfo.StartDate >= Convert.ToDateTime(startDate == null ? Convert.ToString(v.NursePDAInfo.HospitalDemographicInfo.StartDate) : startDate) &&
                                                   v.NursePDAInfo.HospitalDemographicInfo.HospitalDemographicID == (hospitalUnitID ?? v.NursePDAInfo.HospitalDemographicInfo.HospitalDemographicID)
                                                       //&& v.NursePDAInfo.HospitalDemographicInfo.BedsInUnit > 1000 //v.NursePDAInfo.HospitalDemographicInfo.BedsInUnit == (bedInUnit ?? v.NursePDAInfo.HospitalDemographicInfo.BedsInUnit) && v.NursePDAInfo.HospitalDemographicInfo.BudgetedPatientsPerNurse == (budgetedPatient ?? v.NursePDAInfo.HospitalDemographicInfo.BudgetedPatientsPerNurse)
                                                       //&& v.NursePDAInfo.HospitalDemographicInfo.ElectronicDocumentation == (electronicDocument ?? v.NursePDAInfo.HospitalDemographicInfo.ElectronicDocumentation) &&
                                                   && v.NursePDAInfo.HospitalDemographicInfo.UnitType == (unitType ?? v.NursePDAInfo.HospitalDemographicInfo.UnitType) && v.NursePDAInfo.HospitalDemographicInfo.PharmacyType == (pharmacyType ?? v.NursePDAInfo.HospitalDemographicInfo.PharmacyType)
                                                   && v.NursePDAInfo.HospitalDemographicInfo.IsDeleted != true && v.IsErrorExist == false && v.NursePDAInfo.IsErrorExist == false
                                                   orderby v.NursePDAInfo.HospitalDemographicInfo.HospitalUnitName
                                                   select v);

                        queryableNursePDADetail = queryableNursePDADetail.Where(SetFilterForBedsInUnit(optBedsInUnit, bedInUnit));
                        queryableNursePDADetail = queryableNursePDADetail.Where(SetFilterForBudgetedPatientsPerNurse(optBudgetedPatient, budgetedPatient));
                        queryableNursePDADetail = queryableNursePDADetail.Where(SetFilterForElectronicDocumentation(optElectronicDocument, electronicDocument));
                        queryableNursePDADetail = queryableNursePDADetail.Where(SetFilterForDocByException(docByException));
                    }
                    else
                    {
                        queryableNursePDADetail = (from v in _objectRMCDataContext.NursePDADetails
                                                   where v.NursePDAInfo.HospitalDemographicInfo.StartDate >= Convert.ToDateTime(startDate == null ? Convert.ToString(v.NursePDAInfo.HospitalDemographicInfo.StartDate) : startDate) &&
                                                   v.NursePDAInfo.HospitalDemographicInfo.HospitalDemographicID == (hospitalUnitID ?? v.NursePDAInfo.HospitalDemographicInfo.HospitalDemographicID)
                                                       //&& v.NursePDAInfo.HospitalDemographicInfo.BedsInUnit == (bedInUnit ?? v.NursePDAInfo.HospitalDemographicInfo.BedsInUnit) && v.NursePDAInfo.HospitalDemographicInfo.BudgetedPatientsPerNurse == (budgetedPatient ?? v.NursePDAInfo.HospitalDemographicInfo.BudgetedPatientsPerNurse)
                                                       //&& v.NursePDAInfo.HospitalDemographicInfo.ElectronicDocumentation == (electronicDocument ?? v.NursePDAInfo.HospitalDemographicInfo.ElectronicDocumentation) &&
                                                   && v.NursePDAInfo.HospitalDemographicInfo.UnitType == (unitType ?? v.NursePDAInfo.HospitalDemographicInfo.UnitType) && v.NursePDAInfo.HospitalDemographicInfo.PharmacyType == (pharmacyType ?? v.NursePDAInfo.HospitalDemographicInfo.PharmacyType)
                                                   && v.NursePDAInfo.HospitalDemographicInfo.IsDeleted != true && v.IsErrorExist == false && v.NursePDAInfo.IsErrorExist == false
                                                   orderby v.NursePDAInfo.HospitalDemographicInfo.HospitalUnitName
                                                   select v);

                        queryableNursePDADetail = queryableNursePDADetail.Where(SetFilterForBedsInUnit(optBedsInUnit, bedInUnit));
                        queryableNursePDADetail = queryableNursePDADetail.Where(SetFilterForBudgetedPatientsPerNurse(optBudgetedPatient, budgetedPatient));
                        queryableNursePDADetail = queryableNursePDADetail.Where(SetFilterForElectronicDocumentation(optElectronicDocument, electronicDocument));
                        queryableNursePDADetail = queryableNursePDADetail.Where(SetFilterForDocByException(docByException));
                    }
                }
                else
                {
                    queryableNursePDADetail = (from v in _objectRMCDataContext.NursePDADetails
                                               where v.NursePDAInfo.HospitalDemographicInfo.StartDate >= Convert.ToDateTime(startDate == null ? Convert.ToString(v.NursePDAInfo.HospitalDemographicInfo.StartDate) : startDate) &&
                                               v.NursePDAInfo.HospitalDemographicInfo.EndedDate <= Convert.ToDateTime(endDate == null ? Convert.ToString(v.NursePDAInfo.HospitalDemographicInfo.EndedDate) : endDate) &&
                                               v.NursePDAInfo.HospitalDemographicInfo.HospitalDemographicID == (hospitalUnitID ?? v.NursePDAInfo.HospitalDemographicInfo.HospitalDemographicID)
                                                   //&& v.NursePDAInfo.HospitalDemographicInfo.BedsInUnit == (bedInUnit ?? v.NursePDAInfo.HospitalDemographicInfo.BedsInUnit) && v.NursePDAInfo.HospitalDemographicInfo.BudgetedPatientsPerNurse == (budgetedPatient ?? v.NursePDAInfo.HospitalDemographicInfo.BudgetedPatientsPerNurse)
                                                   //&& v.NursePDAInfo.HospitalDemographicInfo.ElectronicDocumentation == (electronicDocument ?? v.NursePDAInfo.HospitalDemographicInfo.ElectronicDocumentation) &&
                                               && v.NursePDAInfo.HospitalDemographicInfo.UnitType == (unitType ?? v.NursePDAInfo.HospitalDemographicInfo.UnitType) && v.NursePDAInfo.HospitalDemographicInfo.PharmacyType == (pharmacyType ?? v.NursePDAInfo.HospitalDemographicInfo.PharmacyType)
                                               && v.NursePDAInfo.HospitalDemographicInfo.IsDeleted != true
                                               orderby v.NursePDAInfo.HospitalDemographicInfo.HospitalUnitName
                                               select v);

                    queryableNursePDADetail = queryableNursePDADetail.Where(SetFilterForBedsInUnit(optBedsInUnit, bedInUnit));
                    queryableNursePDADetail = queryableNursePDADetail.Where(SetFilterForBudgetedPatientsPerNurse(optBudgetedPatient, budgetedPatient));
                    queryableNursePDADetail = queryableNursePDADetail.Where(SetFilterForElectronicDocumentation(optElectronicDocument, electronicDocument));
                    queryableNursePDADetail = queryableNursePDADetail.Where(SetFilterForDocByException(docByException));
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            objectGenericBEValidation = queryableNursePDADetail.Select(v => new RMC.BusinessEntities.BEValidation
                {
                    ActivityID = v.ActivityID,
                    LocationID = v.LocationID,
                    SubActivityID = v.SubActivityID,
                    HospitalUnitID = v.NursePDAInfo.HospitalDemographicInfo.HospitalDemographicID,
                    HospitalUnitName = v.NursePDAInfo.HospitalDemographicInfo.HospitalUnitName,
                    CategoryGroupID = 0,
                    Month = v.NursePDAInfo.Month,
                    MonthIndex = Convert.ToInt32(v.NursePDAInfo.Month),
                    HospitalSize = (_objectRMCDataContext.DynamicDatas
                                  .Where(w => w.ColumnName.TableName.ToLower().Trim() == "HospitalInfo".ToLower() && w.ColumnName.ColumnName1.ToLower().Trim() == "BedsInHospital".ToLower() && w.ID == v.NursePDAInfo.HospitalDemographicInfo.HospitalInfo.HospitalInfoID)
                                  .Select(s => Convert.ToInt32(s.Value)).FirstOrDefault())
                }).ToList<RMC.BusinessEntities.BEValidation>();

            if (hospitalSize > 0)
            {
                objectGenericBEValidation = objectGenericBEValidation.Where(SetFilterForHospitalSize(optHospitalSize, hospitalSize)).ToList<RMC.BusinessEntities.BEValidation>();
            }

            return objectGenericBEValidation;
        }

        /// <summary>
        /// Calculates data for each profile given in parameter, and used in reporting
        /// </summary>
        private List<RMC.BusinessEntities.BEReports> CalculationOnDataByProfile(string profileCategory, List<RMC.BusinessEntities.BECategoryProfile> objectGenericBECategoryProfile, List<RMC.BusinessEntities.BEValidation> objectGenericBEValidation)
        {
            try
            {
                int totalCount = 0;
                List<RMC.DataService.ValueAddedType> objectGenericValueAddedType = _objectRMCDataContext.ValueAddedTypes.OrderBy(o => o.TypeID).ToList();
                List<RMC.DataService.CategoryGroup> objectGenericCategoryGroup = _objectRMCDataContext.CategoryGroups.OrderBy(o => o.CategoryGroupID).ToList();
                List<RMC.DataService.LocationCategory> objectGenericLocationCategory = _objectRMCDataContext.LocationCategories.OrderBy(o => o.LocationID).ToList();
                List<RMC.BusinessEntities.BEReports> objectGenericBEReports = null;
                objectGenericBEValidation.ForEach(delegate(RMC.BusinessEntities.BEValidation objectBEValidation)
                {
                    RMC.BusinessEntities.BECategoryProfile objectNewBECategoryProfile = objectGenericBECategoryProfile.Find(delegate(RMC.BusinessEntities.BECategoryProfile objectBECategoryProfile)
                    {
                        return objectBECategoryProfile.LocationID == objectBEValidation.LocationID && objectBECategoryProfile.ActivityID == objectBEValidation.ActivityID && objectBECategoryProfile.SubActivityID == objectBEValidation.SubActivityID;
                    });
                    if (objectNewBECategoryProfile != null)
                    {
                        objectBEValidation.CategoryGroupID = objectNewBECategoryProfile.CategoryAssignmentID;
                    }
                    else
                    {
                        totalCount++;
                        objectBEValidation.CategoryGroupID = 0;
                    }
                });

                objectGenericBEValidation = objectGenericBEValidation.Where(w => w.CategoryGroupID != 0).ToList();

                totalCount = objectGenericBEValidation.Count;

                if (profileCategory == "value added")
                {
                    objectGenericBEReports = (from v in objectGenericBEValidation.GroupBy(o => o.HospitalUnitName).ToList()
                                              from t in v.GroupBy(x => x.CategoryGroupID).ToList()
                                              select new RMC.BusinessEntities.BEReports
                                              {
                                                  ColumnName = objectGenericValueAddedType.Find(delegate(RMC.DataService.ValueAddedType objectValueAddedType)
                                                  {
                                                      return objectValueAddedType.TypeID == t.Key;
                                                  }).TypeName,
                                                  ColumnNumber = objectGenericValueAddedType.Find(delegate(RMC.DataService.ValueAddedType objectValueAddedType)
                                                  {
                                                      return objectValueAddedType.TypeID == t.Key;
                                                  }).TypeID,
                                                  RowName = t.FirstOrDefault(r => r.HospitalUnitName != "").HospitalUnitName,
                                                  Values = string.Format("{0:#.##}%", ((double)t.Count(r => r.CategoryGroupID == t.Key) / (double)v.Count(c => c.CategoryGroupID != 0)) * 100),
                                                  ValuesSum = ((double)t.Count(r => r.CategoryGroupID == t.Key) / (double)v.Count(c => c.CategoryGroupID != 0)) * 100
                                              }).ToList();

                    //List<string> objectGenericHospitalUnitName = objectGenericBEReports.Select(s => s.RowName).Distinct().ToList();
                    //objectGenericValueAddedType.ForEach(delegate(RMC.DataService.ValueAddedType objectValueAddedType)
                    //{
                    //    foreach (string objectHospitalUnitName in objectGenericHospitalUnitName)
                    //    {
                    //        if (!objectGenericBEReports.Exists(delegate(RMC.BusinessEntities.BEReports objectNewBEReport)
                    //        {
                    //            return objectHospitalUnitName == objectNewBEReport.RowName && objectValueAddedType.TypeName == objectNewBEReport.ColumnName;
                    //        }))
                    //        {
                    //            RMC.BusinessEntities.BEReports objectNewBEReports = new RMC.BusinessEntities.BEReports();

                    //            objectNewBEReports.RowName = objectHospitalUnitName;
                    //            objectNewBEReports.ColumnName = objectValueAddedType.TypeName;
                    //            objectNewBEReports.ColumnNumber = objectValueAddedType.TypeID;
                    //            objectNewBEReports.Values = "0.00%";

                    //            objectGenericBEReports.Add(objectNewBEReports);
                    //        }
                    //    }
                    //});
                }
                else if (profileCategory == "others")
                {
                    objectGenericBEReports = (from v in objectGenericBEValidation.GroupBy(o => o.HospitalUnitName).ToList()
                                              from t in v.GroupBy(x => x.CategoryGroupID).ToList()
                                              select new RMC.BusinessEntities.BEReports
                                              {
                                                  ColumnName = objectGenericCategoryGroup.Find(delegate(RMC.DataService.CategoryGroup objectCategoryGroup)
                                                  {
                                                      return objectCategoryGroup.CategoryGroupID == t.Key;
                                                  }).CategoryGroup1,
                                                  ColumnNumber = objectGenericCategoryGroup.Find(delegate(RMC.DataService.CategoryGroup objectCategoryGroup)
                                                  {
                                                      return objectCategoryGroup.CategoryGroupID == t.Key;
                                                  }).CategoryGroupID,
                                                  RowName = t.FirstOrDefault(r => r.HospitalUnitName != "").HospitalUnitName,
                                                  Values = string.Format("{0:#.##}%", ((double)t.Count(r => r.CategoryGroupID == t.Key) / (double)v.Count(c => c.CategoryGroupID != 0)) * 100),
                                                  ValuesSum = ((double)t.Count(r => r.CategoryGroupID == t.Key) / (double)v.Count(c => c.CategoryGroupID != 0)) * 100
                                              }).ToList();

                    //List<string> objectGenericHospitalUnitName = objectGenericBEReports.Select(s => s.RowName).Distinct().ToList();
                    //objectGenericCategoryGroup.ForEach(delegate(RMC.DataService.CategoryGroup objectCategoryGroup)
                    //{
                    //    foreach (string objectHospitalUnitName in objectGenericHospitalUnitName)
                    //    {
                    //        if (!objectGenericBEReports.Exists(delegate(RMC.BusinessEntities.BEReports objectNewBEReport)
                    //        {
                    //            return objectHospitalUnitName == objectNewBEReport.RowName && objectCategoryGroup.CategoryGroup1 == objectNewBEReport.ColumnName;
                    //        }))
                    //        {
                    //            RMC.BusinessEntities.BEReports objectNewBEReports = new RMC.BusinessEntities.BEReports();

                    //            objectNewBEReports.RowName = objectHospitalUnitName;
                    //            objectNewBEReports.ColumnName = objectCategoryGroup.CategoryGroup1;
                    //            objectNewBEReports.ColumnNumber = objectCategoryGroup.CategoryGroupID;
                    //            objectNewBEReports.Values = "0.00%";

                    //            objectGenericBEReports.Add(objectNewBEReports);
                    //        }
                    //    }
                    //});
                }
                else
                {
                    objectGenericBEReports = (from v in objectGenericBEValidation.GroupBy(o => o.HospitalUnitName).ToList()
                                              from t in v.GroupBy(x => x.CategoryGroupID).ToList()
                                              select new RMC.BusinessEntities.BEReports
                                              {
                                                  ColumnName = objectGenericLocationCategory.Find(delegate(RMC.DataService.LocationCategory objectLocationCategory)
                                                  {
                                                      return objectLocationCategory.LocationID == t.Key;
                                                  }).LocationCategory1,
                                                  ColumnNumber = objectGenericLocationCategory.Find(delegate(RMC.DataService.LocationCategory objectLocationCategory)
                                                  {
                                                      return objectLocationCategory.LocationID == t.Key;
                                                  }).LocationID,
                                                  RowName = t.FirstOrDefault(r => r.HospitalUnitName != "").HospitalUnitName,
                                                  Values = string.Format("{0:#.##}%", ((double)t.Count(r => r.CategoryGroupID == t.Key) / (double)v.Count(c => c.CategoryGroupID != 0)) * 100),
                                                  ValuesSum = ((double)t.Count(r => r.CategoryGroupID == t.Key) / (double)v.Count(c => c.CategoryGroupID != 0)) * 100
                                              }).ToList();

                    //List<string> objectGenericHospitalUnitName = objectGenericBEReports.Select(s => s.RowName).Distinct().ToList();
                    //objectGenericLocationCategory.ForEach(delegate(RMC.DataService.LocationCategory objectLocationCategory)
                    //{
                    //    foreach (string objectHospitalUnitName in objectGenericHospitalUnitName)
                    //    {
                    //        if (!objectGenericBEReports.Exists(delegate(RMC.BusinessEntities.BEReports objectNewBEReport)
                    //        {
                    //            return objectHospitalUnitName == objectNewBEReport.RowName && objectLocationCategory.LocationCategory1 == objectNewBEReport.ColumnName;
                    //        }))
                    //        {
                    //            RMC.BusinessEntities.BEReports objectNewBEReports = new RMC.BusinessEntities.BEReports();

                    //            objectNewBEReports.RowName = objectHospitalUnitName;
                    //            objectNewBEReports.ColumnName = objectLocationCategory.LocationCategory1;
                    //            objectNewBEReports.ColumnNumber = objectLocationCategory.LocationID;
                    //            objectNewBEReports.Values = "0.00%";

                    //            objectGenericBEReports.Add(objectNewBEReports);
                    //        }
                    //    }
                    //});
                }

                return objectGenericBEReports;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Gets all data for hospitalUnit according to the values given in parameters
        /// </summary>
        private List<RMC.BusinessEntities.BEReports> GetDataForHospitalUnit(int? firstYear, int? lastYear, int? firstMonth, int? lastMonth, int? hospitalUnitID, int? bedInUnit, int optBedsInUnit, float? budgetedPatient, int optBudgetedPatient, string startDate, string endDate, int? electronicDocument, int optElectronicDocument, int docByException, string unitType, string pharmacyType, int optHospitalSize, int? hospitalSize)
        {
            try
            {
                IQueryable<RMC.DataService.NursePDADetail> queryableNursePDADetail = null;
                _objectGenericBEReports = new List<RMC.BusinessEntities.BEReports>();

                if (endDate == null)
                {
                    DateTime datTimeFirst, datTimeLast;
                    if (firstYear != null && lastYear != null && firstYear.Value > 0 && lastYear.Value > 0)
                    {
                        datTimeFirst = Convert.ToDateTime(firstMonth.Value.ToString() + "/01/" + firstYear.Value.ToString());
                        if ((lastMonth.Value + 1) != 12)
                        {
                            datTimeLast = Convert.ToDateTime((lastMonth + 1) + "/01/" + lastYear.Value.ToString());
                        }
                        else
                        {
                            datTimeLast = Convert.ToDateTime("01/01/" + (lastYear.Value + 1).ToString());
                        }
                        queryableNursePDADetail = (from v in _objectRMCDataContext.NursePDADetails
                                                   where (Convert.ToDateTime(v.NursePDAInfo.Month + "/01/" + v.NursePDAInfo.Year) >= datTimeFirst && Convert.ToDateTime((v.NursePDAInfo.Month != "12" ? (Convert.ToInt32(v.NursePDAInfo.Month) + 1).ToString() : "01") + "/01/" + v.NursePDAInfo.Year) < datTimeLast)
                                                   && v.NursePDAInfo.HospitalDemographicInfo.StartDate >= Convert.ToDateTime(startDate == null ? Convert.ToString(v.NursePDAInfo.HospitalDemographicInfo.StartDate) : startDate) &&
                                                   v.NursePDAInfo.HospitalDemographicInfo.HospitalDemographicID == (hospitalUnitID ?? v.NursePDAInfo.HospitalDemographicInfo.HospitalDemographicID)
                                                       //&& v.NursePDAInfo.HospitalDemographicInfo.BedsInUnit == (bedInUnit ?? v.NursePDAInfo.HospitalDemographicInfo.BedsInUnit) && v.NursePDAInfo.HospitalDemographicInfo.BudgetedPatientsPerNurse == (budgetedPatient ?? v.NursePDAInfo.HospitalDemographicInfo.BudgetedPatientsPerNurse)
                                                       //&& v.NursePDAInfo.HospitalDemographicInfo.ElectronicDocumentation == (electronicDocument ?? v.NursePDAInfo.HospitalDemographicInfo.ElectronicDocumentation) &&
                                                   && v.NursePDAInfo.HospitalDemographicInfo.UnitType == (unitType ?? v.NursePDAInfo.HospitalDemographicInfo.UnitType) && v.NursePDAInfo.HospitalDemographicInfo.PharmacyType == (pharmacyType ?? v.NursePDAInfo.HospitalDemographicInfo.PharmacyType)
                                                   && v.NursePDAInfo.HospitalDemographicInfo.IsDeleted != true
                                                   orderby v.NursePDAInfo.HospitalDemographicInfo.HospitalUnitName
                                                   select v);

                        queryableNursePDADetail = queryableNursePDADetail.Where(SetFilterForBedsInUnit(optBedsInUnit, bedInUnit));
                        queryableNursePDADetail = queryableNursePDADetail.Where(SetFilterForBudgetedPatientsPerNurse(optBudgetedPatient, budgetedPatient));
                        queryableNursePDADetail = queryableNursePDADetail.Where(SetFilterForElectronicDocumentation(optElectronicDocument, electronicDocument));
                        queryableNursePDADetail = queryableNursePDADetail.Where(SetFilterForDocByException(docByException));
                    }
                    else
                    {
                        queryableNursePDADetail = (from v in _objectRMCDataContext.NursePDADetails
                                                   where v.NursePDAInfo.HospitalDemographicInfo.StartDate >= Convert.ToDateTime(startDate == null ? Convert.ToString(v.NursePDAInfo.HospitalDemographicInfo.StartDate) : startDate) &&
                                                   v.NursePDAInfo.HospitalDemographicInfo.HospitalDemographicID == (hospitalUnitID ?? v.NursePDAInfo.HospitalDemographicInfo.HospitalDemographicID)
                                                       //&& v.NursePDAInfo.HospitalDemographicInfo.BedsInUnit == (bedInUnit ?? v.NursePDAInfo.HospitalDemographicInfo.BedsInUnit) && v.NursePDAInfo.HospitalDemographicInfo.BudgetedPatientsPerNurse == (budgetedPatient ?? v.NursePDAInfo.HospitalDemographicInfo.BudgetedPatientsPerNurse)
                                                       //&& v.NursePDAInfo.HospitalDemographicInfo.ElectronicDocumentation == (electronicDocument ?? v.NursePDAInfo.HospitalDemographicInfo.ElectronicDocumentation) &&
                                                   && v.NursePDAInfo.HospitalDemographicInfo.UnitType == (unitType ?? v.NursePDAInfo.HospitalDemographicInfo.UnitType) && v.NursePDAInfo.HospitalDemographicInfo.PharmacyType == (pharmacyType ?? v.NursePDAInfo.HospitalDemographicInfo.PharmacyType)
                                                   && v.NursePDAInfo.HospitalDemographicInfo.IsDeleted != true
                                                   orderby v.NursePDAInfo.HospitalDemographicInfo.HospitalUnitName
                                                   select v);

                        queryableNursePDADetail = queryableNursePDADetail.Where(SetFilterForBedsInUnit(optBedsInUnit, bedInUnit));
                        queryableNursePDADetail = queryableNursePDADetail.Where(SetFilterForBudgetedPatientsPerNurse(optBudgetedPatient, budgetedPatient));
                        queryableNursePDADetail = queryableNursePDADetail.Where(SetFilterForElectronicDocumentation(optElectronicDocument, electronicDocument));
                        queryableNursePDADetail = queryableNursePDADetail.Where(SetFilterForDocByException(docByException));
                    }
                }
                else
                {
                    queryableNursePDADetail = (from v in _objectRMCDataContext.NursePDADetails
                                               where v.NursePDAInfo.HospitalDemographicInfo.StartDate >= Convert.ToDateTime(startDate == null ? Convert.ToString(v.NursePDAInfo.HospitalDemographicInfo.StartDate) : startDate) &&
                                               v.NursePDAInfo.HospitalDemographicInfo.EndedDate <= Convert.ToDateTime(endDate == null ? Convert.ToString(v.NursePDAInfo.HospitalDemographicInfo.EndedDate) : endDate) &&
                                               v.NursePDAInfo.HospitalDemographicInfo.HospitalDemographicID == (hospitalUnitID ?? v.NursePDAInfo.HospitalDemographicInfo.HospitalDemographicID)
                                                   //&& v.NursePDAInfo.HospitalDemographicInfo.BedsInUnit == (bedInUnit ?? v.NursePDAInfo.HospitalDemographicInfo.BedsInUnit) && v.NursePDAInfo.HospitalDemographicInfo.BudgetedPatientsPerNurse == (budgetedPatient ?? v.NursePDAInfo.HospitalDemographicInfo.BudgetedPatientsPerNurse)
                                                   //&& v.NursePDAInfo.HospitalDemographicInfo.ElectronicDocumentation == (electronicDocument ?? v.NursePDAInfo.HospitalDemographicInfo.ElectronicDocumentation) &&
                                               && v.NursePDAInfo.HospitalDemographicInfo.UnitType == (unitType ?? v.NursePDAInfo.HospitalDemographicInfo.UnitType) && v.NursePDAInfo.HospitalDemographicInfo.PharmacyType == (pharmacyType ?? v.NursePDAInfo.HospitalDemographicInfo.PharmacyType)
                                               && v.NursePDAInfo.HospitalDemographicInfo.IsDeleted != true
                                               orderby v.NursePDAInfo.HospitalDemographicInfo.HospitalUnitName
                                               select v);

                    queryableNursePDADetail = queryableNursePDADetail.Where(SetFilterForBedsInUnit(optBedsInUnit, bedInUnit));
                    queryableNursePDADetail = queryableNursePDADetail.Where(SetFilterForBudgetedPatientsPerNurse(optBudgetedPatient, budgetedPatient));
                    queryableNursePDADetail = queryableNursePDADetail.Where(SetFilterForElectronicDocumentation(optElectronicDocument, electronicDocument));
                    queryableNursePDADetail = queryableNursePDADetail.Where(SetFilterForDocByException(docByException));
                }

                _objectGenericBEHospitalBenchmarkSummary = queryableNursePDADetail.Select(v => new RMC.BusinessEntities.BEHospitalBenchmarkSummary
                                                                {
                                                                    BedsInUnit = v.NursePDAInfo.HospitalDemographicInfo.BedsInUnit,
                                                                    BudgetedPatientsPerNurse = v.NursePDAInfo.HospitalDemographicInfo.BudgetedPatientsPerNurse,
                                                                    Demographic = (from dd in _objectRMCDataContext.DynamicDatas
                                                                                   where dd.ColumnName.ColumnName1.ToLower().Trim() == "hospitaltype" && dd.ColumnName.TableName.ToLower().Trim() == "hospitalinfo" && dd.ID == v.NursePDAInfo.HospitalDemographicInfo.HospitalInfo.HospitalInfoID
                                                                                   select dd.Value).FirstOrDefault(),
                                                                    DocByException = v.NursePDAInfo.HospitalDemographicInfo.DocByException,
                                                                    ElectronicDocumentation = v.NursePDAInfo.HospitalDemographicInfo.ElectronicDocumentation,
                                                                    HospitalDemographicID = v.NursePDAInfo.HospitalDemographicInfo.HospitalDemographicID,
                                                                    HospitalInfoID = v.NursePDAInfo.HospitalDemographicInfo.HospitalInfoID,
                                                                    HospitalUnitName = v.NursePDAInfo.HospitalDemographicInfo.HospitalUnitName,
                                                                    PharmacyType = v.NursePDAInfo.HospitalDemographicInfo.PharmacyType,
                                                                    StartDate = v.NursePDAInfo.HospitalDemographicInfo.StartDate,
                                                                    State = v.NursePDAInfo.HospitalDemographicInfo.HospitalInfo.State.StateAbbreviation,
                                                                    TCABUnit = v.NursePDAInfo.HospitalDemographicInfo.TCABUnit,
                                                                    UnitType = v.NursePDAInfo.HospitalDemographicInfo.UnitType,
                                                                    HospitalBedSize = (from dd in _objectRMCDataContext.DynamicDatas
                                                                                       where dd.ColumnName.ColumnName1.ToLower().Trim() == "bedsinhospital" && dd.ColumnName.TableName.ToLower().Trim() == "hospitalinfo" && dd.ID == v.NursePDAInfo.HospitalDemographicInfo.HospitalInfo.HospitalInfoID
                                                                                       select dd.Value).FirstOrDefault()
                                                                }).Distinct().ToList();
                if (hospitalSize > 0)
                {
                    _objectGenericBEHospitalBenchmarkSummary = _objectGenericBEHospitalBenchmarkSummary.Where(SetFilterForHospitalSizeUsebyHospital(optHospitalSize, hospitalSize)).ToList<RMC.BusinessEntities.BEHospitalBenchmarkSummary>();
                }
                _objectGenericBEHospitalBenchmarkSummary.ForEach(delegate(RMC.BusinessEntities.BEHospitalBenchmarkSummary objectBEHospitalBenchmarkSummary)
                {
                    HospitalBenchmarkFields().ForEach(delegate(string objectValues)
                    {
                        RMC.BusinessEntities.BEReports objectBEReports = new RMC.BusinessEntities.BEReports();

                        objectBEReports.RowName = objectBEHospitalBenchmarkSummary.HospitalUnitName;
                        objectBEReports.ColumnName = objectValues;
                        switch (objectValues)
                        {
                            case "Unit Type":
                                objectBEReports.Values = objectBEHospitalBenchmarkSummary.UnitType;
                                break;
                            case "Unit Size":
                                objectBEReports.Values = Convert.ToString(objectBEHospitalBenchmarkSummary.BedsInUnit);
                                break;
                            case "Budgeted Patient Per Nurse":
                                objectBEReports.Values = Convert.ToString(objectBEHospitalBenchmarkSummary.BudgetedPatientsPerNurse);
                                break;
                            case "Doc by Exception":
                                objectBEReports.Values = Convert.ToString(objectBEHospitalBenchmarkSummary.DocByException);
                                break;
                            case "% Electronic Documentation":
                                objectBEReports.Values = Convert.ToString(objectBEHospitalBenchmarkSummary.ElectronicDocumentation);
                                break;
                            case "Pharmacy Type":
                                objectBEReports.Values = objectBEHospitalBenchmarkSummary.PharmacyType;
                                break;
                            case "Hospital Beds - Size":
                                objectBEReports.Values = objectBEHospitalBenchmarkSummary.HospitalBedSize;
                                break;
                            case "Data Collected Begin-End Dates":
                                objectBEReports.Values = Convert.ToString(objectBEHospitalBenchmarkSummary.StartDate) + "-" + Convert.ToString(objectBEHospitalBenchmarkSummary.EndedDate);
                                break;
                            case "State":
                                objectBEReports.Values = objectBEHospitalBenchmarkSummary.State;
                                break;
                            case "Demographic (Urban/Community/Regional)":
                                objectBEReports.Values = objectBEHospitalBenchmarkSummary.Demographic;
                                break;
                        }

                        _objectGenericBEReports.Add(objectBEReports);
                    });
                });

                return _objectGenericBEReports;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Call by SearchHospitalsDataForBenchmark method to filter data according to the 
        /// BedsInUnit data
        /// </summary>
        private Expression<Func<RMC.DataService.NursePDADetail, bool>> SetFilterForBedsInUnit(int opt, int? value)
        {
            try
            {
                Expression<Func<RMC.DataService.NursePDADetail, bool>> filter;
                switch (opt)
                {
                    case 1:
                        filter = f => f.NursePDAInfo.HospitalDemographicInfo.BedsInUnit < value;
                        break;
                    case 2:
                        filter = f => f.NursePDAInfo.HospitalDemographicInfo.BedsInUnit > value;
                        break;
                    case 3:
                        filter = f => f.NursePDAInfo.HospitalDemographicInfo.BedsInUnit == value;
                        break;
                    case 4:
                        filter = f => f.NursePDAInfo.HospitalDemographicInfo.BedsInUnit >= value;
                        break;
                    case 5:
                        filter = f => f.NursePDAInfo.HospitalDemographicInfo.BedsInUnit <= value;
                        break;
                    case 6:
                        filter = f => f.NursePDAInfo.HospitalDemographicInfo.BedsInUnit != value;
                        break;
                    default:
                        filter = f => f.NursePDAInfo.HospitalDemographicInfo.BedsInUnit == (value ?? f.NursePDAInfo.HospitalDemographicInfo.BedsInUnit);
                        break;
                }

                return filter;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Call by SearchHospitalsDataForBenchmark method to filter data according to the 
        /// BudgetedPatientsPerNurse data
        /// </summary>
        private Expression<Func<RMC.DataService.NursePDADetail, bool>> SetFilterForBudgetedPatientsPerNurse(int opt, double? value)
        {
            try
            {
                Expression<Func<RMC.DataService.NursePDADetail, bool>> filter;
                switch (opt)
                {
                    case 1:
                        filter = f => f.NursePDAInfo.HospitalDemographicInfo.BudgetedPatientsPerNurse < value;
                        break;
                    case 2:
                        filter = f => f.NursePDAInfo.HospitalDemographicInfo.BudgetedPatientsPerNurse > value;
                        break;
                    case 3:
                        filter = f => f.NursePDAInfo.HospitalDemographicInfo.BudgetedPatientsPerNurse == value;
                        break;
                    case 4:
                        filter = f => f.NursePDAInfo.HospitalDemographicInfo.BudgetedPatientsPerNurse >= value;
                        break;
                    case 5:
                        filter = f => f.NursePDAInfo.HospitalDemographicInfo.BudgetedPatientsPerNurse <= value;
                        break;
                    case 6:
                        filter = f => f.NursePDAInfo.HospitalDemographicInfo.BudgetedPatientsPerNurse != value;
                        break;
                    default:
                        filter = f => f.NursePDAInfo.HospitalDemographicInfo.BudgetedPatientsPerNurse == (value ?? f.NursePDAInfo.HospitalDemographicInfo.BudgetedPatientsPerNurse);
                        break;
                }

                return filter;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Call by SearchHospitalsDataForBenchmark method to filter data according to the 
        /// ElectronicDocumentation data
        /// </summary>
        private Expression<Func<RMC.DataService.NursePDADetail, bool>> SetFilterForElectronicDocumentation(int opt, int? value)
        {
            try
            {
                Expression<Func<RMC.DataService.NursePDADetail, bool>> filter;
                switch (opt)
                {
                    case 1:
                        filter = f => f.NursePDAInfo.HospitalDemographicInfo.ElectronicDocumentation < value;
                        break;
                    case 2:
                        filter = f => f.NursePDAInfo.HospitalDemographicInfo.ElectronicDocumentation > value;
                        break;
                    case 3:
                        filter = f => f.NursePDAInfo.HospitalDemographicInfo.ElectronicDocumentation == value;
                        break;
                    case 4:
                        filter = f => f.NursePDAInfo.HospitalDemographicInfo.ElectronicDocumentation >= value;
                        break;
                    case 5:
                        filter = f => f.NursePDAInfo.HospitalDemographicInfo.ElectronicDocumentation <= value;
                        break;
                    case 6:
                        filter = f => f.NursePDAInfo.HospitalDemographicInfo.ElectronicDocumentation != value;
                        break;
                    default:
                        filter = f => f.NursePDAInfo.HospitalDemographicInfo.ElectronicDocumentation == (value ?? f.NursePDAInfo.HospitalDemographicInfo.ElectronicDocumentation);
                        break;
                }

                return filter;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Call by SearchHospitalsDataForBenchmark method to filter data according to the 
        /// DocByException data
        /// </summary>
        private Expression<Func<RMC.DataService.NursePDADetail, bool>> SetFilterForDocByException(int Value)
        {
            try
            {
                Expression<Func<RMC.DataService.NursePDADetail, bool>> filter;
                switch (Value)
                {
                    case 1:
                        filter = f => f.NursePDAInfo.HospitalDemographicInfo.DocByException == true;
                        break;
                    case 2:
                        filter = f => f.NursePDAInfo.HospitalDemographicInfo.DocByException == false;
                        break;
                    default:
                        filter = f => f.NursePDAInfo.HospitalDemographicInfo.DocByException == f.NursePDAInfo.HospitalDemographicInfo.DocByException;
                        break;
                }

                return filter;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Call by SearchHospitalsDataForBenchmark method to filter data according to the 
        /// HospitalSize data
        /// </summary>
        private Func<RMC.BusinessEntities.BEValidation, bool> SetFilterForHospitalSize(int opt, int? value)
        {
            try
            {
                Func<RMC.BusinessEntities.BEValidation, bool> filter;

                switch (opt)
                {
                    case 1:
                        filter = f => f.HospitalSize < value;
                        break;
                    case 2:
                        filter = f => f.HospitalSize > value;
                        break;
                    case 3:
                        filter = f => f.HospitalSize == value;
                        break;
                    case 4:
                        filter = f => f.HospitalSize >= value;
                        break;
                    case 5:
                        filter = f => f.HospitalSize <= value;
                        break;
                    case 6:
                        filter = f => f.HospitalSize != value;
                        break;
                    default:
                        filter = f => f.HospitalSize == (value ?? f.HospitalSize);
                        break;
                }

                return filter;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// To filter data according to the HospitalSize use by hospital data
        /// </summary>
        private Func<RMC.BusinessEntities.BEHospitalBenchmarkSummary, bool> SetFilterForHospitalSizeUsebyHospital(int opt, int? value)
        {
            try
            {
                Func<RMC.BusinessEntities.BEHospitalBenchmarkSummary, bool> filter;

                switch (opt)
                {
                    case 1:
                        filter = f => Convert.ToInt32(f.HospitalBedSize) < value;
                        break;
                    case 2:
                        filter = f => Convert.ToInt32(f.HospitalBedSize) > value;
                        break;
                    case 3:
                        filter = f => Convert.ToInt32(f.HospitalBedSize) == value;
                        break;
                    case 4:
                        filter = f => Convert.ToInt32(f.HospitalBedSize) >= value;
                        break;
                    case 5:
                        filter = f => Convert.ToInt32(f.HospitalBedSize) <= value;
                        break;
                    case 6:
                        filter = f => Convert.ToInt32(f.HospitalBedSize) != value;
                        break;
                    default:
                        filter = f => Convert.ToInt32(f.HospitalBedSize) == (value ?? Convert.ToInt32(f.HospitalBedSize));
                        break;
                }

                return filter;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Call by GetDataForTimeRNSummary, it calculates data for timeRNSummary report
        /// </summary>
        /// <param name="profileCategory"></param>
        /// <param name="objectGenericBECategoryProfile"></param>
        /// <param name="objectGenericBEValidation"></param>
        /// <returns>objectGenericBEReports</returns>
        private List<RMC.BusinessEntities.BEReports> CalculationForTimeRNSummary(string profileCategory, List<RMC.BusinessEntities.BECategoryProfile> objectGenericBECategoryProfile, List<RMC.BusinessEntities.BEValidation> objectGenericBEValidation)
        {
            try
            {
                int totalCount = 0;
                List<RMC.DataService.ValueAddedType> objectGenericValueAddedType = _objectRMCDataContext.ValueAddedTypes.OrderBy(o => o.TypeID).ToList();
                List<RMC.DataService.CategoryGroup> objectGenericCategoryGroup = _objectRMCDataContext.CategoryGroups.OrderBy(o => o.CategoryGroupID).ToList();
                List<RMC.DataService.LocationCategory> objectGenericLocationCategory = _objectRMCDataContext.LocationCategories.OrderBy(o => o.LocationID).ToList();
                List<RMC.BusinessEntities.BEReports> objectGenericBEReports = null;
                objectGenericBEValidation.ForEach(delegate(RMC.BusinessEntities.BEValidation objectBEValidation)
                {
                    objectBEValidation.CategoryGroupID = objectGenericBECategoryProfile.Find(delegate(RMC.BusinessEntities.BECategoryProfile objectBECategoryProfile)
                    {
                        return objectBECategoryProfile.LocationID == objectBEValidation.LocationID && objectBECategoryProfile.ActivityID == objectBEValidation.ActivityID && objectBECategoryProfile.SubActivityID == objectBEValidation.SubActivityID;
                    }).CategoryAssignmentID;
                });

                totalCount = objectGenericBEValidation.Count;


                if (profileCategory == "value added")
                {
                    objectGenericBEValidation = objectGenericBEValidation.FindAll(delegate(RMC.BusinessEntities.BEValidation objectNewBEValidation)
                    {
                        return objectGenericValueAddedType.Exists(delegate(RMC.DataService.ValueAddedType objectValueAddedType)
                        {
                            return objectValueAddedType.TypeID == objectNewBEValidation.CategoryGroupID;
                        });
                    });
                    objectGenericBEReports = (from v in objectGenericBEValidation.OrderBy(x => x.MonthIndex).GroupBy(o => o.Month).ToList()
                                              from t in v.GroupBy(x => x.CategoryGroupID).ToList()
                                              select new RMC.BusinessEntities.BEReports
                                              {
                                                  ColumnName = objectGenericValueAddedType.Find(delegate(RMC.DataService.ValueAddedType objectValueAddedType)
                                                  {
                                                      return objectValueAddedType.TypeID == t.Key;
                                                  }).TypeName,
                                                  ColumnNumber = objectGenericValueAddedType.Find(delegate(RMC.DataService.ValueAddedType objectValueAddedType)
                                                  {
                                                      return objectValueAddedType.TypeID == t.Key;
                                                  }).TypeID,
                                                  MonthName = BSCommon.GetMonthName(t.FirstOrDefault(r => r.Month != "").Month),
                                                  RowName = t.FirstOrDefault(r => r.HospitalUnitName != "").HospitalUnitName,
                                                  Values = string.Format("{0:#.##}%", ((double)t.Count(r => r.CategoryGroupID == t.Key) / (double)v.Count(c => c.CategoryGroupID != 0)) * 100),
                                                  ValuesSum = Convert.ToDouble(string.Format("{0:#.##}", ((double)t.Count(r => r.CategoryGroupID == t.Key) / (double)v.Count(c => c.CategoryGroupID != 0)) * 100))
                                              }).ToList();

                    List<string> objectGenericMonthName = objectGenericBEReports.Select(s => s.MonthName).Distinct().ToList();
                    objectGenericValueAddedType.ForEach(delegate(RMC.DataService.ValueAddedType objectValueAddedType)
                    {
                        foreach (string objectMonthName in objectGenericMonthName)
                        {
                            if (!objectGenericBEReports.Exists(delegate(RMC.BusinessEntities.BEReports objectNewBEReport)
                            {
                                return objectMonthName == objectNewBEReport.MonthName && objectValueAddedType.TypeName == objectNewBEReport.ColumnName;
                            }))
                            {
                                RMC.BusinessEntities.BEReports objectNewBEReports = new RMC.BusinessEntities.BEReports();

                                objectNewBEReports.MonthName = objectMonthName;
                                objectNewBEReports.ColumnName = objectValueAddedType.TypeName;
                                objectNewBEReports.ColumnNumber = objectValueAddedType.TypeID;
                                objectNewBEReports.Values = "0.00%";
                                objectNewBEReports.ValuesSum = 0.00;

                                objectGenericBEReports.Add(objectNewBEReports);
                            }
                        }
                    });
                }
                else if (profileCategory == "others")
                {
                    objectGenericBEValidation = objectGenericBEValidation.FindAll(delegate(RMC.BusinessEntities.BEValidation objectNewBEValidation)
                    {
                        return objectGenericCategoryGroup.Exists(delegate(RMC.DataService.CategoryGroup objectCategoryGroup)
                        {
                            return objectCategoryGroup.CategoryGroupID == objectNewBEValidation.CategoryGroupID;
                        });
                    });
                    objectGenericBEReports = (from v in objectGenericBEValidation.OrderBy(x => x.MonthIndex).GroupBy(o => o.Month).ToList()
                                              from t in v.GroupBy(x => x.CategoryGroupID).ToList()
                                              select new RMC.BusinessEntities.BEReports
                                              {
                                                  ColumnName = objectGenericCategoryGroup.Find(delegate(RMC.DataService.CategoryGroup objectCategoryGroup)
                                                  {
                                                      return objectCategoryGroup.CategoryGroupID == t.Key;
                                                  }).CategoryGroup1,
                                                  ColumnNumber = objectGenericCategoryGroup.Find(delegate(RMC.DataService.CategoryGroup objectCategoryGroup)
                                                  {
                                                      return objectCategoryGroup.CategoryGroupID == t.Key;
                                                  }).CategoryGroupID,
                                                  MonthName = BSCommon.GetMonthName(t.FirstOrDefault(r => r.Month != "").Month),
                                                  RowName = t.FirstOrDefault(r => r.HospitalUnitName != "").HospitalUnitName,
                                                  Values = string.Format("{0:#.##}%", ((double)t.Count(r => r.CategoryGroupID == t.Key) / (double)v.Count(c => c.CategoryGroupID != 0)) * 100),
                                                  ValuesSum = Convert.ToDouble(string.Format("{0:#.##}", ((double)t.Count(r => r.CategoryGroupID == t.Key) / (double)v.Count(c => c.CategoryGroupID != 0)) * 100))
                                              }).ToList();

                    List<string> objectGenericMonthName = objectGenericBEReports.Select(s => s.MonthName).Distinct().ToList();
                    objectGenericCategoryGroup.ForEach(delegate(RMC.DataService.CategoryGroup objectCategoryGroup)
                    {
                        foreach (string objectMonthName in objectGenericMonthName)
                        {
                            if (!objectGenericBEReports.Exists(delegate(RMC.BusinessEntities.BEReports objectNewBEReport)
                            {
                                return objectMonthName == objectNewBEReport.MonthName && objectCategoryGroup.CategoryGroup1 == objectNewBEReport.ColumnName;
                            }))
                            {
                                RMC.BusinessEntities.BEReports objectNewBEReports = new RMC.BusinessEntities.BEReports();

                                objectNewBEReports.MonthName = objectMonthName;
                                objectNewBEReports.ColumnName = objectCategoryGroup.CategoryGroup1;
                                objectNewBEReports.ColumnNumber = objectCategoryGroup.CategoryGroupID;
                                objectNewBEReports.Values = "0.00%";
                                objectNewBEReports.ValuesSum = 0.00;

                                objectGenericBEReports.Add(objectNewBEReports);
                            }
                        }
                    });
                }
                else
                {
                    objectGenericBEValidation = objectGenericBEValidation.FindAll(delegate(RMC.BusinessEntities.BEValidation objectNewBEValidation)
                    {
                        return objectGenericLocationCategory.Exists(delegate(RMC.DataService.LocationCategory objectLocationCategory)
                        {
                            return objectLocationCategory.LocationID == objectNewBEValidation.CategoryGroupID;
                        });
                    });
                    objectGenericBEReports = (from v in objectGenericBEValidation.OrderBy(x => x.MonthIndex).GroupBy(o => o.Month).ToList()
                                              from t in v.GroupBy(x => x.CategoryGroupID).ToList()
                                              select new RMC.BusinessEntities.BEReports
                                              {
                                                  ColumnName = objectGenericLocationCategory.Find(delegate(RMC.DataService.LocationCategory objectLocationCategory)
                                                  {
                                                      return objectLocationCategory.LocationID == t.Key;
                                                  }).LocationCategory1,
                                                  ColumnNumber = objectGenericLocationCategory.Find(delegate(RMC.DataService.LocationCategory objectLocationCategory)
                                                  {
                                                      return objectLocationCategory.LocationID == t.Key;
                                                  }).LocationID,
                                                  MonthName = BSCommon.GetMonthName(t.FirstOrDefault(r => r.Month != "").Month),
                                                  RowName = t.FirstOrDefault(r => r.HospitalUnitName != "").HospitalUnitName,
                                                  Values = string.Format("{0:#.##}%", ((double)t.Count(r => r.CategoryGroupID == t.Key) / (double)v.Count(c => c.CategoryGroupID != 0)) * 100),
                                                  ValuesSum = Convert.ToDouble(string.Format("{0:#.##}", ((double)t.Count(r => r.CategoryGroupID == t.Key) / (double)v.Count(c => c.CategoryGroupID != 0)) * 100))
                                              }).ToList();

                    List<string> objectGenericMonthName = objectGenericBEReports.Select(s => s.MonthName).Distinct().ToList();
                    objectGenericLocationCategory.ForEach(delegate(RMC.DataService.LocationCategory objectLocationCategory)
                    {
                        foreach (string objectMonthName in objectGenericMonthName)
                        {
                            if (!objectGenericBEReports.Exists(delegate(RMC.BusinessEntities.BEReports objectNewBEReport)
                            {
                                return objectMonthName == objectNewBEReport.MonthName && objectLocationCategory.LocationCategory1 == objectNewBEReport.ColumnName;
                            }))
                            {
                                RMC.BusinessEntities.BEReports objectNewBEReports = new RMC.BusinessEntities.BEReports();

                                objectNewBEReports.MonthName = objectMonthName;
                                objectNewBEReports.ColumnName = objectLocationCategory.LocationCategory1;
                                objectNewBEReports.ColumnNumber = objectLocationCategory.LocationID;
                                objectNewBEReports.Values = "0.00%";
                                objectNewBEReports.ValuesSum = 0.00;

                                objectGenericBEReports.Add(objectNewBEReports);
                            }
                        }
                    });
                }

                return objectGenericBEReports.OrderBy(o => o.ColumnNumber).ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Use to generate graph values for monthlySummary-Dashboard Report
        /// </summary>
        /// <param name="objectNewGenericBENationalDatabase"></param>
        /// <param name="objectNewGenericBEReports"></param>
        /// <returns>objectPerformaceBENationalDatabase</returns>
        private List<RMC.BusinessEntities.BENationalDatabase> CalculatePerformance(List<RMC.BusinessEntities.BENationalDatabase> objectNewGenericBENationalDatabase, List<RMC.BusinessEntities.BEReports> objectNewGenericBEReports)
        {
            try
            {
                List<RMC.BusinessEntities.BENationalDatabase> objectGenericBENationalDatabase = null;
                List<RMC.BusinessEntities.BEReports> objectGenericSingleBEReports = null;
                List<RMC.BusinessEntities.BENationalDatabase> objectPerformaceBENationalDatabase = new List<RMC.BusinessEntities.BENationalDatabase>();

                objectGenericBENationalDatabase = objectNewGenericBENationalDatabase.FindAll(delegate(RMC.BusinessEntities.BENationalDatabase objectNewBENationalDatabase)
                {
                    return objectNewBENationalDatabase.FunctionType.ToLower() == "median";
                });

                objectGenericSingleBEReports = objectNewGenericBEReports.FindAll(delegate(RMC.BusinessEntities.BEReports objectNewBEReports)
                {
                    return objectNewBEReports.MonthName.ToLower() == "hosp avg";
                });

                objectGenericBENationalDatabase.ForEach(delegate(RMC.BusinessEntities.BENationalDatabase objectNewBENationalDatabase)
                {
                    RMC.BusinessEntities.BEReports objectSingleBEReports = null;
                    objectSingleBEReports = objectGenericSingleBEReports.Find(delegate(RMC.BusinessEntities.BEReports objectNewBEReports)
                    {
                        return objectNewBEReports.ColumnName == objectNewBENationalDatabase.ProfileType;
                    });

                    if (objectSingleBEReports != null)
                    {
                        RMC.BusinessEntities.BENationalDatabase objectPerformance = new RMC.BusinessEntities.BENationalDatabase();

                        objectPerformance.FunctionType = "Performance";
                        objectPerformance.ProfileType = objectSingleBEReports.ColumnName;
                        objectPerformance.Value = objectSingleBEReports.ValuesSum - objectNewBENationalDatabase.Value;
                        objectPerformance.ValueText = string.Format("{0:#.##}%", objectPerformance.Value);

                        objectPerformaceBENationalDatabase.Add(objectPerformance);
                    }
                });

                return objectPerformaceBENationalDatabase;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Calculates Quartile function for given values used in report
        /// </summary>
        /// <param name="NoOfElements"></param>
        /// <param name="quartile"></param>
        /// <param name="objectGenericValues"></param>
        /// <returns>result</returns>
        private double CalculateQuartile(int NoOfElements, int quartile, List<double> objectGenericValues)
        {
            try
            {
                double result, firstPart, secondPart;
                objectGenericValues.Sort(CompareDoubleType);
                double subCal = (NoOfElements - 1) * ((double)quartile / 4.0);
                int beforeDecimal = (int)subCal;
                double afterDecimal = subCal - beforeDecimal;

                firstPart = objectGenericValues.ElementAt(beforeDecimal);

                if (objectGenericValues.Count == 1)
                {
                    secondPart = firstPart;
                }
                else
                {
                    secondPart = objectGenericValues.ElementAt(beforeDecimal + 1);
                }

                result = (1 - afterDecimal) * firstPart + afterDecimal * secondPart;

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Compares double type given in parameter
        /// </summary>
        private static int CompareDoubleType(double x, double y)
        {
            if (x == null)
            {
                if (y == null)
                {
                    return 0;
                }
                else
                {
                    return -1;
                }
            }
            else
            {
                if (y == null)
                {
                    return 1;
                }
                else
                {
                    int retval = x.CompareTo(y);

                    return retval;
                }
            }
        }

        /// <summary>
        /// Compares integer type given in parameter
        /// </summary>
        private static int CompareIntergerType(RMC.BusinessEntities.BEReports x, RMC.BusinessEntities.BEReports y)
        {
            if (x == null)
            {
                if (y == null)
                {
                    return 0;
                }
                else
                {
                    return -1;
                }
            }
            else
            {
                if (y == null)
                {
                    return 1;
                }
                else
                {
                    int retval = x.ColumnName.CompareTo(y.ColumnName);

                    return retval;
                }
            }
        }

        /// <summary>
        /// Compares object string given in parameter
        /// </summary>
        private int CompareObjectString(RMC.BusinessEntities.BEReports x, RMC.BusinessEntities.BEReports y)
        {
            if (x == null)
            {
                if (y == null)
                {
                    // If x is null and y is null, they're
                    // equal. 
                    return 0;
                }
                else
                {
                    // If x is null and y is not null, y
                    // is greater. 
                    return -1;
                }
            }
            else
            {
                // If x is not null...
                //
                if (y == null)
                // ...and y is null, x is greater.
                {
                    return 1;
                }
                else
                {
                    // ...and y is not null, compare the 
                    // lengths of the two strings.
                    //
                    //int retval = x.ColumnName.Length.CompareTo(y.ColumnName.Length);

                    //if (retval != 0)
                    //{
                    //    // If the strings are not of equal length,
                    //    // the longer string is greater.
                    //    //
                    //    return retval;
                    //}
                    //else
                    //{
                    // If the strings are of equal length,
                    // sort them with ordinary string comparison.
                    //
                    return x.ColumnName.CompareTo(y.ColumnName);
                    //}
                }
            }
        }

        /// <summary>
        /// Dispose all Global Objects
        /// </summary>
        private void DisposeGlobalObjects()
        {
            _objectRMCDataContext.Dispose();
            _objectGenericBEReports = null;
            _objectGenericBEHospitalBenchmarkSummary = null;
        }

        #endregion

        #region public Methods

        /// <summary>
        /// Get Data for Hospital Benchmark Report for all three profile categories
        /// </summary>
        public List<RMC.BusinessEntities.BEReports> GetDataForHospitalBenchmarkSummary(int? valueAddedCategoryID, int? OthersCategoryID, int? LocationCategoryID, int? hospitalUnitID, int? firstYear, int? lastYear, int? firstMonth, int? lastMonth, int? bedInUnit, int optBedInUnit, float? budgetedPatient, int optBudgetedPatient, string startDate, string endDate, int? electronicDocument, int optElectronicDocument, int docByException, string unitType, string pharmacyType, int optHospitalSize, int? hospitalSize)
        {
            try
            {
                List<RMC.BusinessEntities.BEReports> objectGenericListBEReportValueAdded = null;
                List<RMC.BusinessEntities.BEReports> objectGenericListBEReportOthers = null;
                List<RMC.BusinessEntities.BEReports> objectGenericListBEReportLocation = null;

                objectGenericListBEReportValueAdded = CalculationOnDataByProfile("value added", GetCategoryProfileDataByProfileID(valueAddedCategoryID.Value), SearchHospitalsDataForBenchmark(firstYear, lastYear, firstMonth, lastMonth, hospitalUnitID, bedInUnit, optBedInUnit, budgetedPatient, optBudgetedPatient, startDate, endDate, electronicDocument, optElectronicDocument, docByException, unitType, pharmacyType, optHospitalSize, hospitalSize));
                objectGenericListBEReportOthers = CalculationOnDataByProfile("others", GetCategoryProfileDataByProfileID(OthersCategoryID.Value), SearchHospitalsDataForBenchmark(firstYear, lastYear, firstMonth, lastMonth, hospitalUnitID, bedInUnit, optBedInUnit, budgetedPatient, optBudgetedPatient, startDate, endDate, electronicDocument, optElectronicDocument, docByException, unitType, pharmacyType, optHospitalSize, hospitalSize));
                objectGenericListBEReportLocation = CalculationOnDataByProfile("location", GetCategoryProfileDataByProfileID(LocationCategoryID.Value), SearchHospitalsDataForBenchmark(firstYear, lastYear, firstMonth, lastMonth, hospitalUnitID, bedInUnit, optBedInUnit, budgetedPatient, optBudgetedPatient, startDate, endDate, electronicDocument, optElectronicDocument, docByException, unitType, pharmacyType, optHospitalSize, hospitalSize));
                objectGenericListBEReportValueAdded.AddRange(objectGenericListBEReportOthers);
                objectGenericListBEReportValueAdded.AddRange(objectGenericListBEReportLocation);
                objectGenericListBEReportValueAdded.AddRange(GetDataForHospitalUnit(firstYear, lastYear, firstMonth, lastMonth, hospitalUnitID, bedInUnit, optBedInUnit, budgetedPatient, optBudgetedPatient, startDate, endDate, electronicDocument, optElectronicDocument, docByException, unitType, pharmacyType, optHospitalSize, hospitalSize));
                return objectGenericListBEReportValueAdded;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                //DisposeGlobalObjects();
            }
        }

        /// <summary>
        /// Calculates Function Values for HospitalBenchmarkSummary Report for each Columns
        /// </summary>
        public List<RMC.BusinessEntities.BEFunctionNames> CalculateFunctionValues(int? valueAddedCategoryID, int? OthersCategoryID, int? LocationCategoryID, int? hospitalUnitID, int? firstYear, int? lastYear, int? firstMonth, int? lastMonth, int? bedInUnit, int optBedInUnit, float? budgetedPatient, int optBudgetedPatient, string startDate, string endDate, int? electronicDocument, int optElectronicDocument, int docByException, string unitType, string pharmacyType, int optHospitalSize, int? hospitalSize)
        {
            try
            {
                List<RMC.BusinessEntities.BEReports> objectNewGenericBEReports = null;
                List<RMC.BusinessEntities.BEFunctionNames> objectGenericBEFunctionNames = new List<RMC.BusinessEntities.BEFunctionNames>();

                List<RMC.BusinessEntities.BEReports> objectGenericListBEReportValueAdded = null;
                List<RMC.BusinessEntities.BEReports> objectGenericListBEReportOthers = null;
                List<RMC.BusinessEntities.BEReports> objectGenericListBEReportLocation = null;

                objectGenericListBEReportValueAdded = CalculationOnDataByProfile("value added", GetCategoryProfileDataByProfileID(valueAddedCategoryID.Value), SearchHospitalsDataForBenchmark(firstYear, lastYear, firstMonth, lastMonth, hospitalUnitID, bedInUnit, optBedInUnit, budgetedPatient, optBudgetedPatient, startDate, endDate, electronicDocument, optElectronicDocument, docByException, unitType, pharmacyType, optHospitalSize, hospitalSize));
                objectGenericListBEReportOthers = CalculationOnDataByProfile("others", GetCategoryProfileDataByProfileID(OthersCategoryID.Value), SearchHospitalsDataForBenchmark(firstYear, lastYear, firstMonth, lastMonth, hospitalUnitID, bedInUnit, optBedInUnit, budgetedPatient, optBudgetedPatient, startDate, endDate, electronicDocument, optElectronicDocument, docByException, unitType, pharmacyType, optHospitalSize, hospitalSize));
                objectGenericListBEReportLocation = CalculationOnDataByProfile("location", GetCategoryProfileDataByProfileID(LocationCategoryID.Value), SearchHospitalsDataForBenchmark(firstYear, lastYear, firstMonth, lastMonth, hospitalUnitID, bedInUnit, optBedInUnit, budgetedPatient, optBudgetedPatient, startDate, endDate, electronicDocument, optElectronicDocument, docByException, unitType, pharmacyType, optHospitalSize, hospitalSize));
                objectGenericListBEReportValueAdded.AddRange(objectGenericListBEReportOthers);
                objectGenericListBEReportValueAdded.AddRange(objectGenericListBEReportLocation);
                objectNewGenericBEReports = objectGenericListBEReportValueAdded;

                foreach (var r in objectNewGenericBEReports.GroupBy(o => o.ColumnName))
                {
                    //Calculate Minimum value.
                    RMC.BusinessEntities.BEFunctionNames objectNewBEFunctionNamesMin = new RMC.BusinessEntities.BEFunctionNames();
                    double valueMin = 0;
                    valueMin = r.Select(s => Convert.ToDouble((s.Values.Length > 0) ? s.Values.Substring(0, s.Values.Length - 1) : "0")).Min();

                    objectNewBEFunctionNamesMin.ColumnName = r.FirstOrDefault().ColumnName;
                    objectNewBEFunctionNamesMin.FunctionName = "Minimum";
                    objectNewBEFunctionNamesMin.FunctionNameDouble = valueMin;
                    objectNewBEFunctionNamesMin.FunctionValueText = string.Format("{0:#.##}%", (valueMin == 0) ? "0.00" : valueMin.ToString());

                    objectGenericBEFunctionNames.Add(objectNewBEFunctionNamesMin);

                    //Calculate Maximum value
                    RMC.BusinessEntities.BEFunctionNames objectNewBEFunctionNamesMax = new RMC.BusinessEntities.BEFunctionNames();
                    double valueMax = 0;
                    valueMax = r.Select(s => Convert.ToDouble((s.Values.Length > 0) ? s.Values.Substring(0, s.Values.Length - 1) : "0")).Max();

                    objectNewBEFunctionNamesMax.ColumnName = r.FirstOrDefault().ColumnName;
                    objectNewBEFunctionNamesMax.FunctionName = "Maximum";
                    objectNewBEFunctionNamesMax.FunctionNameDouble = valueMax;
                    objectNewBEFunctionNamesMax.FunctionValueText = string.Format("{0:#.##}%", (valueMax == 0) ? "0.00" : valueMax.ToString());

                    //Calculate Average value
                    RMC.BusinessEntities.BEFunctionNames objectNewBEFunctionNamesAvg = new RMC.BusinessEntities.BEFunctionNames();
                    List<double> valueSum = null;
                    valueSum = r.Select(s => Convert.ToDouble((s.Values.Length > 0) ? s.Values.Substring(0, s.Values.Length - 1) : "0")).ToList();

                    objectNewBEFunctionNamesAvg.ColumnName = r.FirstOrDefault().ColumnName;
                    objectNewBEFunctionNamesAvg.FunctionName = "Average";
                    objectNewBEFunctionNamesAvg.FunctionNameDouble = (valueSum.Sum() / r.Select(s => s.RowName).Distinct().Count());
                    objectNewBEFunctionNamesAvg.FunctionValueText = string.Format("{0:#.##}%", (objectNewBEFunctionNamesAvg.FunctionNameDouble == 0) ? "0.00" : string.Format("{0:#.##}", objectNewBEFunctionNamesAvg.FunctionNameDouble));

                    //Median Value.
                    int median = 0;
                    int count = r.Select(s => s.RowName).Distinct().Count();
                    if (count % 2 == 0)
                    {
                        median = count / 2;
                    }
                    else
                    {
                        median = (count + 1) / 2;
                    }
                    median--;

                    RMC.BusinessEntities.BEFunctionNames objectNewBEFunctionNamesMed = new RMC.BusinessEntities.BEFunctionNames();

                    objectNewBEFunctionNamesMed.ColumnName = r.FirstOrDefault().ColumnName;
                    objectNewBEFunctionNamesMed.FunctionName = "Median";
                    objectNewBEFunctionNamesMed.FunctionNameDouble = r.Select(s => s.ValuesSum).ToList().ElementAt(median);
                    objectNewBEFunctionNamesMed.FunctionValueText = string.Format("{0:#.##}%", string.Format("{0:#.##}", (objectNewBEFunctionNamesMed.FunctionNameDouble == 0.00) ? "0.00" : string.Format("{0:#.##}", objectNewBEFunctionNamesMed.FunctionNameDouble)));

                    //Quartile
                    RMC.BusinessEntities.BEFunctionNames objectNewBEFunctionNamesQuartile1 = new RMC.BusinessEntities.BEFunctionNames();

                    objectNewBEFunctionNamesQuartile1.ColumnName = r.FirstOrDefault().ColumnName;
                    objectNewBEFunctionNamesQuartile1.FunctionName = "Quartile(1)";
                    objectNewBEFunctionNamesQuartile1.FunctionNameDouble = CalculateQuartile(count, 1, r.Select(s => s.ValuesSum).ToList());
                    objectNewBEFunctionNamesQuartile1.FunctionValueText = string.Format("{0:#.##}%", (objectNewBEFunctionNamesQuartile1.FunctionNameDouble == 0.00) ? "0.00" : string.Format("{0:#.##}", objectNewBEFunctionNamesQuartile1.FunctionNameDouble));

                    RMC.BusinessEntities.BEFunctionNames objectNewBEFunctionNamesQuartile3 = new RMC.BusinessEntities.BEFunctionNames();

                    objectNewBEFunctionNamesQuartile3.ColumnName = r.FirstOrDefault().ColumnName;
                    objectNewBEFunctionNamesQuartile3.FunctionName = "Quartile(3)";
                    objectNewBEFunctionNamesQuartile3.FunctionNameDouble = CalculateQuartile(count, 3, r.Select(s => s.ValuesSum).ToList());
                    objectNewBEFunctionNamesQuartile3.FunctionValueText = string.Format("{0:#.##}%", (objectNewBEFunctionNamesQuartile3.FunctionNameDouble == 0.00) ? "0.00" : string.Format("{0:#.##}", objectNewBEFunctionNamesQuartile3.FunctionNameDouble));

                    objectGenericBEFunctionNames.Add(objectNewBEFunctionNamesQuartile1);
                    objectGenericBEFunctionNames.Add(objectNewBEFunctionNamesMed);
                    objectGenericBEFunctionNames.Add(objectNewBEFunctionNamesAvg);
                    objectGenericBEFunctionNames.Add(objectNewBEFunctionNamesQuartile3);
                    objectGenericBEFunctionNames.Add(objectNewBEFunctionNamesMax);
                }

                return objectGenericBEFunctionNames;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                DisposeGlobalObjects();
            }
        }

        /// <summary>
        /// Gets data for timeRNSummary Report for all three profiles category
        /// </summary>
        public List<RMC.BusinessEntities.BEReports> GetDataForTimeRNSummary(int? valueAddedCategoryID, int? OthersCategoryID, int? LocationCategoryID, int? hospitalUnitID, int? firstYear, int? lastYear, int? firstMonth, int? lastMonth, int? bedInUnit, int optBedInUnit, float? budgetedPatient, int optBudgetedPatient, string startDate, string endDate, int? electronicDocument, int optElectronicDocument, int docByException, string unitType, string pharmacyType, int optHospitalSize, int? hospitalSize)
        {
            try
            {
                List<RMC.BusinessEntities.BEReports> objectGenericListBEReportValueAdded = null;
                List<RMC.BusinessEntities.BEReports> objectGenericListBEReportOthers = null;
                List<RMC.BusinessEntities.BEReports> objectGenericListBEReportLocation = null;

                objectGenericListBEReportValueAdded = CalculationForTimeRNSummary("value added", GetCategoryProfileDataByProfileID(valueAddedCategoryID.Value), SearchHospitalsDataForBenchmark(firstYear, lastYear, firstMonth, lastMonth, hospitalUnitID, bedInUnit, optBedInUnit, budgetedPatient, optBudgetedPatient, startDate, endDate, electronicDocument, optElectronicDocument, docByException, unitType, pharmacyType, optHospitalSize, hospitalSize));
                objectGenericListBEReportOthers = CalculationForTimeRNSummary("others", GetCategoryProfileDataByProfileID(OthersCategoryID.Value), SearchHospitalsDataForBenchmark(firstYear, lastYear, firstMonth, lastMonth, hospitalUnitID, bedInUnit, optBedInUnit, budgetedPatient, optBudgetedPatient, startDate, endDate, electronicDocument, optElectronicDocument, docByException, unitType, pharmacyType, optHospitalSize, hospitalSize));
                objectGenericListBEReportLocation = CalculationForTimeRNSummary("location", GetCategoryProfileDataByProfileID(LocationCategoryID.Value), SearchHospitalsDataForBenchmark(firstYear, lastYear, firstMonth, lastMonth, hospitalUnitID, bedInUnit, optBedInUnit, budgetedPatient, optBudgetedPatient, startDate, endDate, electronicDocument, optElectronicDocument, docByException, unitType, pharmacyType, optHospitalSize, hospitalSize));
                objectGenericListBEReportValueAdded.AddRange(objectGenericListBEReportOthers);
                objectGenericListBEReportValueAdded.AddRange(objectGenericListBEReportLocation);

                List<RMC.BusinessEntities.BEReports> objectBEReportsResultant = (from r in objectGenericListBEReportValueAdded.GroupBy(o => o.ColumnName)
                                                                                 select new RMC.BusinessEntities.BEReports
                                                                                 {
                                                                                     ColumnName = r.FirstOrDefault().ColumnName,
                                                                                     MonthName = "Hosp Avg",
                                                                                     Values = string.Format("{0:#.##}%", (r.Sum(x => x.ValuesSum) == 0) ? "0.00" : string.Format("{0:#.##}", r.Sum(x => x.ValuesSum) / 12)),
                                                                                     ValuesSum = (r.Sum(x => x.ValuesSum) == 0) ? 0.00 : r.Sum(x => x.ValuesSum) / 12
                                                                                 }).ToList();

                objectGenericListBEReportValueAdded.AddRange(objectBEReportsResultant);
                return objectGenericListBEReportValueAdded;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Gets data from NationalDatabase table from database
        /// </summary>
        public List<RMC.BusinessEntities.BENationalDatabase> GetNationalDatabase()
        {
            try
            {
                List<RMC.BusinessEntities.BENationalDatabase> objectGenericBENationalDatabase = null;

                IQueryable<RMC.BusinessEntities.BENationalDatabase> queryable = (from nd in _objectRMCDataContext.NationalDatabases
                                                                                 orderby nd.TypeValueID
                                                                                 select new RMC.BusinessEntities.BENationalDatabase
                                                                                 {
                                                                                     FunctionType = (from nc in _objectRMCDataContext.NationalDatabaseCategories
                                                                                                     where nc.NationalDatabaseCategoryID == nd.NationalDatabaseCategoryID
                                                                                                     select nc.NationalDatabaseCategoryName).FirstOrDefault(),
                                                                                     ProfileType = (nd.Type.ToLower() == "value added") ? (from pt in _objectRMCDataContext.ValueAddedTypes
                                                                                                                                           where pt.TypeID == nd.TypeValueID
                                                                                                                                           select pt).FirstOrDefault().TypeName :
                                                                                                    (nd.Type.ToLower() == "others") ? (from pt in _objectRMCDataContext.CategoryGroups
                                                                                                                                       where pt.CategoryGroupID == nd.TypeValueID
                                                                                                                                       select pt).FirstOrDefault().CategoryGroup1 : (from pt in _objectRMCDataContext.LocationCategories
                                                                                                                                                                                     where pt.LocationID == nd.TypeValueID
                                                                                                                                                                                     select pt).FirstOrDefault().LocationCategory1,
                                                                                     GroupSequence = (nd.Type.ToLower() == "value added") ? 1 : (nd.Type.ToLower() == "others") ? 2 : 3,
                                                                                     ValueText = string.Format("{0:#.##}%", nd.Value.Value),
                                                                                     Value = nd.Value.Value
                                                                                 }).AsQueryable();

                objectGenericBENationalDatabase = queryable.ToList();
                return objectGenericBENationalDatabase.OrderBy(o => o.GroupSequence).ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Calculates performance values for monthlySummaryDashboard report
        /// </summary>
        public List<RMC.BusinessEntities.BENationalDatabase> GetPerformance(int? valueAddedCategoryID, int? OthersCategoryID, int? LocationCategoryID, int? hospitalUnitID, int? firstYear, int? lastYear, int? firstMonth, int? lastMonth, int? bedInUnit, int optBedInUnit, float? budgetedPatient, int optBudgetedPatient, string startDate, string endDate, int? electronicDocument, int optElectronicDocument, int docByException, string unitType, string pharmacyType, int optHospitalSize, int? hospitalSize)
        {
            try
            {
                List<RMC.BusinessEntities.BENationalDatabase> objectGenericBENationalDatabase = GetNationalDatabase();
                List<RMC.BusinessEntities.BEReports> objectGenericBEReports = GetDataForTimeRNSummary(valueAddedCategoryID, OthersCategoryID, LocationCategoryID, hospitalUnitID, firstYear, lastYear, firstMonth, lastMonth, bedInUnit, optBedInUnit, budgetedPatient, optBudgetedPatient, startDate, endDate, electronicDocument, optElectronicDocument, docByException, unitType, pharmacyType, optHospitalSize, hospitalSize);

                return CalculatePerformance(objectGenericBENationalDatabase, objectGenericBEReports);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Gets subCategoryProfiles from database according to the profileCategory given
        /// </summary>
        public List<RMC.BusinessEntities.BEProfileCategory> GetSubCategoryProfile(string profileCategory)
        {
            RMC.DataService.RMCDataContext objectRMCDataContext = new RMC.DataService.RMCDataContext();
            List<RMC.BusinessEntities.BEProfileCategory> objectSubCategoryProfile = new List<RMC.BusinessEntities.BEProfileCategory>();
            if (profileCategory.ToLower().Trim() == "value added")
            {
                objectSubCategoryProfile = (from vat in objectRMCDataContext.ValueAddedTypes
                                            select new RMC.BusinessEntities.BEProfileCategory
                                            {
                                                ProfileCategoryName = vat.TypeName
                                            }).ToList();
            }
            if (profileCategory.ToLower().Trim() == "others")
            {
                objectSubCategoryProfile = (from cg in objectRMCDataContext.CategoryGroups
                                            select new RMC.BusinessEntities.BEProfileCategory
                                            {
                                                ProfileCategoryName = cg.CategoryGroup1
                                            }).ToList();
            }
            if (profileCategory.ToLower().Trim() == "location")
            {
                objectSubCategoryProfile = (from lc in objectRMCDataContext.LocationCategories
                                            select new RMC.BusinessEntities.BEProfileCategory
                                            {
                                                ProfileCategoryName = lc.LocationCategory1
                                            }).ToList();
            }
            return objectSubCategoryProfile;
        }

        /// <summary>
        /// Groups the special category column data from NursePDASpecialCategory table
        /// </summary>
        public List<RMC.BusinessEntities.BEValidationSpecialType> GetSpecialCategory()
        {
            try
            {
                _objectRMCDataContext = new RMC.DataService.RMCDataContext();
                List<RMC.BusinessEntities.BEValidationSpecialType> objectGenericBEValidationSpecialType = null;
                objectGenericBEValidationSpecialType = (from st in _objectRMCDataContext.NursePDASpecialTypes
                                                        group st by new { st.SpecialCategory } into g
                                                        select new RMC.BusinessEntities.BEValidationSpecialType
                                                        {
                                                            SpecialCategory = g.Key.SpecialCategory.ToString(),
                                                        }).OrderBy(a => a.SpecialCategory).ToList();

                return objectGenericBEValidationSpecialType;
            }
            catch (Exception ex)
            {
                ex.Data.Add("Function", "GetSpecialCategory");
                ex.Data.Add("Class", "BSProfileType");
                throw ex;
            }
            finally
            {
                _objectRMCDataContext.Dispose();
            }
        }



        /// <summary>
        /// Gets users Profiles for each profileCategory passed to it
        /// </summary>
        public List<RMC.DataService.ProfileType> GetProfiles(string profileCategory)
        {
            try
            {
                _objectRMCDataContext = new RMC.DataService.RMCDataContext();
                List<RMC.DataService.ProfileType> objectGenericProfileType = null;
                if (profileCategory != "0")
                {
                    if (profileCategory.ToLower().Trim() == "value added")
                    {
                        objectGenericProfileType = (from gpt in _objectRMCDataContext.ProfileUsers
                                                    where gpt.IsShared == true && gpt.ProfileType.Type.ToLower().Trim() == "value added"
                                                    orderby gpt.ProfileType.ProfileName
                                                    select gpt.ProfileType).ToList<RMC.DataService.ProfileType>();
                    }
                    if (profileCategory.ToLower().Trim() == "others")
                    {
                        objectGenericProfileType = (from gpt in _objectRMCDataContext.ProfileUsers
                                                    where gpt.IsShared == true && gpt.ProfileType.Type.ToLower().Trim() == "others"
                                                    orderby gpt.ProfileType.ProfileName
                                                    select gpt.ProfileType).ToList<RMC.DataService.ProfileType>();
                    }
                    if (profileCategory.ToLower().Trim() == "location")
                    {
                        objectGenericProfileType = (from gpt in _objectRMCDataContext.ProfileUsers
                                                    where gpt.IsShared == true && gpt.ProfileType.Type.ToLower().Trim() == "location"
                                                    orderby gpt.ProfileType.ProfileName
                                                    select gpt.ProfileType).ToList<RMC.DataService.ProfileType>();
                    }
                    if (profileCategory.ToLower().Trim() == "activities")
                    {
                        objectGenericProfileType = (from gpt in _objectRMCDataContext.ProfileUsers
                                                    where gpt.IsShared == true && gpt.ProfileType.Type.ToLower().Trim() == "activities"
                                                    orderby gpt.ProfileType.ProfileName
                                                    select gpt.ProfileType).ToList<RMC.DataService.ProfileType>();
                    }
                }
                return objectGenericProfileType;
            }
            catch (Exception ex)
            {
                ex.Data.Add("Function", "GetProfileTypes");
                ex.Data.Add("Class", "BSProfileType");
                throw ex;
            }
            finally
            {
                _objectRMCDataContext.Dispose();
            }
        }



        #endregion

        #region Modified Methods

        /// <summary>
        /// Call by SearchHospitalsDataForBenchmarkTest method to filter data according to the BedsInUnit data
        /// </summary>
        private Expression<Func<RMC.DataService.NursePDADetail, bool>> SetFilterForBedsInUnitTest(int optFrom, int? valueFrom, int optTo, int? valueTo)
        {
            try
            {
                Expression<Func<RMC.DataService.NursePDADetail, bool>> filter;
                switch (optFrom)
                {
                    case 1:
                        filter = f => f.NursePDAInfo.HospitalDemographicInfo.BedsInUnit < valueFrom;
                        break;
                    case 2:
                        filter = f => f.NursePDAInfo.HospitalDemographicInfo.BedsInUnit > valueFrom;
                        break;
                    case 3:
                        filter = f => f.NursePDAInfo.HospitalDemographicInfo.BedsInUnit == valueFrom;
                        break;
                    case 4:
                        filter = f => f.NursePDAInfo.HospitalDemographicInfo.BedsInUnit >= valueFrom;
                        break;
                    case 5:
                        filter = f => f.NursePDAInfo.HospitalDemographicInfo.BedsInUnit <= valueFrom;
                        break;
                    case 6:
                        filter = f => f.NursePDAInfo.HospitalDemographicInfo.BedsInUnit != valueFrom;
                        break;
                    default:
                        filter = f => f.NursePDAInfo.HospitalDemographicInfo.BedsInUnit == (valueFrom ?? f.NursePDAInfo.HospitalDemographicInfo.BedsInUnit);
                        break;
                }

                if ((optFrom == 0 && optTo == 0) && (valueFrom != null && valueTo != null))
                {
                    filter = f => f.NursePDAInfo.HospitalDemographicInfo.BedsInUnit >= valueFrom && f.NursePDAInfo.HospitalDemographicInfo.BedsInUnit <= valueTo;
                }

                return filter;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Call by SearchHospitalsDataForBenchmarkTest method to filter data according to the 
        /// BudgetedPatientsPerNurse data
        /// </summary>
        private Expression<Func<RMC.DataService.NursePDADetail, bool>> SetFilterForBudgetedPatientsPerNurseTest(int optFrom, double? valueFrom, int optTo, double? valueTo)
        {
            try
            {
                Expression<Func<RMC.DataService.NursePDADetail, bool>> filter;
                switch (optFrom)
                {
                    case 1:
                        filter = f => f.NursePDAInfo.HospitalDemographicInfo.BudgetedPatientsPerNurse < valueFrom;
                        break;
                    case 2:
                        filter = f => f.NursePDAInfo.HospitalDemographicInfo.BudgetedPatientsPerNurse > valueFrom;
                        break;
                    case 3:
                        filter = f => f.NursePDAInfo.HospitalDemographicInfo.BudgetedPatientsPerNurse == valueFrom;
                        break;
                    case 4:
                        filter = f => f.NursePDAInfo.HospitalDemographicInfo.BudgetedPatientsPerNurse >= valueFrom;
                        break;
                    case 5:
                        filter = f => f.NursePDAInfo.HospitalDemographicInfo.BudgetedPatientsPerNurse <= valueFrom;
                        break;
                    case 6:
                        filter = f => f.NursePDAInfo.HospitalDemographicInfo.BudgetedPatientsPerNurse != valueFrom;
                        break;
                    default:
                        filter = f => f.NursePDAInfo.HospitalDemographicInfo.BudgetedPatientsPerNurse == (valueFrom ?? f.NursePDAInfo.HospitalDemographicInfo.BudgetedPatientsPerNurse);
                        break;
                }

                if ((optFrom == 0 && optTo == 0) && (valueFrom != null && valueTo != null))
                {
                    filter = f => f.NursePDAInfo.HospitalDemographicInfo.BudgetedPatientsPerNurse >= valueFrom && f.NursePDAInfo.HospitalDemographicInfo.BudgetedPatientsPerNurse <= valueTo;
                }

                return filter;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Call by SearchHospitalsDataForBenchmarkTest method to filter data according to the 
        /// ElectronicDocumentation data
        /// </summary>
        private Expression<Func<RMC.DataService.NursePDADetail, bool>> SetFilterForElectronicDocumentationTest(int optFrom, int? valueFrom, int optTo, int? valueTo)
        {
            try
            {
                Expression<Func<RMC.DataService.NursePDADetail, bool>> filter;
                switch (optFrom)
                {
                    case 1:
                        filter = f => f.NursePDAInfo.HospitalDemographicInfo.ElectronicDocumentation < valueFrom;
                        break;
                    case 2:
                        filter = f => f.NursePDAInfo.HospitalDemographicInfo.ElectronicDocumentation > valueFrom;
                        break;
                    case 3:
                        filter = f => f.NursePDAInfo.HospitalDemographicInfo.ElectronicDocumentation == valueFrom;
                        break;
                    case 4:
                        filter = f => f.NursePDAInfo.HospitalDemographicInfo.ElectronicDocumentation >= valueFrom;
                        break;
                    case 5:
                        filter = f => f.NursePDAInfo.HospitalDemographicInfo.ElectronicDocumentation <= valueFrom;
                        break;
                    case 6:
                        filter = f => f.NursePDAInfo.HospitalDemographicInfo.ElectronicDocumentation != valueFrom;
                        break;
                    default:
                        filter = f => f.NursePDAInfo.HospitalDemographicInfo.ElectronicDocumentation == (valueFrom ?? f.NursePDAInfo.HospitalDemographicInfo.ElectronicDocumentation);
                        break;
                }

                if ((optFrom == 0 && optTo == 0) && (valueFrom != null && valueTo != null))
                {
                    filter = f => f.NursePDAInfo.HospitalDemographicInfo.ElectronicDocumentation >= valueFrom && f.NursePDAInfo.HospitalDemographicInfo.ElectronicDocumentation <= valueTo;
                }

                return filter;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Call by SearchHospitalsDataForBenchmarkTest method to filter data according to the 
        /// DocByException data
        /// </summary>
        private Expression<Func<RMC.DataService.NursePDADetail, bool>> SetFilterForDocByExceptionTest(int Value)
        {
            try
            {
                Expression<Func<RMC.DataService.NursePDADetail, bool>> filter;
                switch (Value)
                {
                    case 1:
                        filter = f => f.NursePDAInfo.HospitalDemographicInfo.DocByException == true;
                        break;
                    case 2:
                        filter = f => f.NursePDAInfo.HospitalDemographicInfo.DocByException == false;
                        break;
                    default:
                        filter = f => f.NursePDAInfo.HospitalDemographicInfo.DocByException == f.NursePDAInfo.HospitalDemographicInfo.DocByException;
                        break;
                }

                return filter;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Call by SearchHospitalsDataForBenchmarkTest method to filter data according to the 
        /// HospitalSize data
        /// </summary>
        private Func<RMC.BusinessEntities.BEValidation, bool> SetFilterForHospitalSizeTest(int optFrom, int? valueFrom, int optTo, int? valueTo)
        {
            try
            {
                Func<RMC.BusinessEntities.BEValidation, bool> filter;

                switch (optFrom)
                {
                    case 1:
                        filter = f => f.HospitalSize < valueFrom;
                        break;
                    case 2:
                        filter = f => f.HospitalSize > valueFrom;
                        break;
                    case 3:
                        filter = f => f.HospitalSize == valueFrom;
                        break;
                    case 4:
                        filter = f => f.HospitalSize >= valueFrom;
                        break;
                    case 5:
                        filter = f => f.HospitalSize <= valueFrom;
                        break;
                    case 6:
                        filter = f => f.HospitalSize != valueFrom;
                        break;
                    default:
                        filter = f => f.HospitalSize == (valueFrom ?? f.HospitalSize);
                        break;
                }

                if ((optFrom == 0 && optTo == 0) && (valueFrom != null && valueTo != null))
                {
                    filter = f => f.HospitalSize >= valueFrom && f.HospitalSize <= valueTo;
                }

                return filter;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Call by SearchHospitalsDataForBenchmarkGrid method to filter data according to the 
        /// UnitType data, this method makes a part of query which uses like, or operator
        /// </summary>
        private Expression<Func<RMC.DataService.NursePDADetail, bool>> SetFilterForUnitType(string unitType)
        {
            try
            {
                //Expression<Func<RMC.DataService.NursePDADetail, bool>> filter;

                var predicate = PredicateBuilder.False<RMC.DataService.NursePDADetail>();
                string[] arrUnitType = unitType.Split(new char[] { ',' });

                foreach (string type in arrUnitType)
                {
                    string unit = type;
                    predicate = predicate.Or(f => SqlMethods.Like(f.NursePDAInfo.HospitalDemographicInfo.UnitType, "%" + unit + "%"));
                }
                return predicate;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Call by SearchHospitalsDataForBenchmarkGrid method to filter data according to the 
        /// UnitType data, this method makes a part of query which uses like, or operator
        /// </summary>
        private Expression<Func<RMC.DataService.NursePDADetail, bool>> SetFilterForUnitIds(string unitIds)
        {
            try
            {
                //Expression<Func<RMC.DataService.NursePDADetail, bool>> filter;

                var predicate = PredicateBuilder.False<RMC.DataService.NursePDADetail>();
                string[] arrUnitIds = unitIds.Split(new char[] { ',' });

                foreach (string type in arrUnitIds)
                {
                    string unit = type;
                    predicate = predicate.Or(f => SqlMethods.Equals(f.NursePDAInfo.HospitalDemographicInfo.HospitalDemographicID,unit));
                }
                return predicate;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// Call by SearchHospitalsDataForBenchmarkGrid method to filter data according to the 
        /// PharmacyType data, this method makes a part of query which uses like, or operator
        /// </summary>
        private Expression<Func<RMC.DataService.NursePDADetail, bool>> SetFilterForPharmacyType(string pharmacyType)
        {
            try
            {
                //Expression<Func<RMC.DataService.NursePDADetail, bool>> filter;

                var predicate = PredicateBuilder.False<RMC.DataService.NursePDADetail>();
                string[] arrPharmacyType = pharmacyType.Split(new char[] { ',' });

                foreach (string type in arrPharmacyType)
                {
                    string pharmacy = type;
                    predicate = predicate.Or(f => SqlMethods.Like(f.NursePDAInfo.HospitalDemographicInfo.PharmacyType, "%" + pharmacy + "%"));
                }
                return predicate;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Call by SearchHospitalsDataForBenchmarkGrid method to filter data according to the 
        /// ConfigurationName data, this method makes a part of query which uses like, or operator
        /// </summary>
        private Expression<Func<RMC.DataService.NursePDADetail, bool>> SetFilterForConfigName(string configName)
        {
            try
            {
                //Expression<Func<RMC.DataService.NursePDADetail, bool>> filter;

                var predicate = PredicateBuilder.False<RMC.DataService.NursePDADetail>();
                string[] arrConfigName = configName.Split(new char[] { ',' });

                foreach (string type in arrConfigName)
                {
                    string config = type;
                    predicate = predicate.Or(f => SqlMethods.Like(f.NursePDAInfo.ConfigName, "%" + config + "%"));
                }
                return predicate;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Call by SearchHospitalsDataForBenchmarkGrid method to filter data according to the 
        /// HospitalType data, this method makes a part of query which uses like, or operator
        /// </summary>
        //private Expression<Func<RMC.DataService.NursePDADetail, bool>> SetFilterForHospitalType(string hospitalType)
        //{
        //    try
        //    {
        //        //Expression<Func<RMC.DataService.NursePDADetail, bool>> filter;

        //        var predicate = PredicateBuilder.False<RMC.DataService.DynamicData>();
        //        string[] arrHospitalType = hospitalType.Split(new char[] { ',' });

        //        foreach (string type in arrHospitalType)
        //        {
        //            predicate = predicate.Or(f => SqlMethods.Like(f.Value, "%" + type + "%"));
        //        }

        //        return predicate;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}


        //temp

        /// <summary>
        /// Filters Data for HospitalBenchmark Report
        /// </summary>
        public List<RMC.BusinessEntities.BEValidation> SearchHospitalsDataForBenchmarkTest(int? hospitalUnitID, int? firstYear, int? lastYear, int? firstMonth, int? lastMonth, int? bedInUnitFrom, int optBedInUnitFrom, int? bedInUnitTo, int optBedInUnitTo, float? budgetedPatientFrom, int optBudgetedPatientFrom, float? budgetedPatientTo, int optBudgetedPatientTo, string startDate, string endDate, int? electronicDocumentFrom, int optElectronicDocumentFrom, int? electronicDocumentTo, int optElectronicDocumentTo, int docByException, string unitType, string pharmacyType, string hospitalType, int optHospitalSizeFrom, int? hospitalSizeFrom, int optHospitalSizeTo, int? hospitalSizeTo, int? countryId, int? stateId, string configName, string unitIds)
        {
            List<RMC.BusinessEntities.BEValidation> objectGenericBEValidation;
            IQueryable<RMC.DataService.NursePDADetail> queryableNursePDADetail = null;

            //_objectRMCDataContext.ObjectTrackingEnabled = false;

            try
            {
                //firstYear = null; firstMonth = null; lastMonth = null; lastYear = null;
                DateTime datTimeFirst, datTimeLast;
                if (firstYear != null && lastYear != null && firstYear.Value > 0 && lastYear.Value > 0)
                {
                    datTimeFirst = Convert.ToDateTime(firstMonth.Value.ToString() + "/01/" + firstYear.Value.ToString());
                    datTimeLast = Convert.ToDateTime(lastMonth.Value.ToString() + "/01/" + lastYear.Value.ToString());

                    queryableNursePDADetail = (from v in _objectRMCDataContext.NursePDADetails
                                               where (Convert.ToDateTime(v.NursePDAInfo.Month + "/01/" + v.NursePDAInfo.Year) >= datTimeFirst && Convert.ToDateTime(v.NursePDAInfo.Month + "/01/" + v.NursePDAInfo.Year) <= datTimeLast)
                                                   //&& v.NursePDAInfo.HospitalDemographicInfo.StartDate >= Convert.ToDateTime(startDate == null ? Convert.ToString(v.NursePDAInfo.HospitalDemographicInfo.StartDate) : startDate)
                                               && v.NursePDAInfo.HospitalDemographicInfo.HospitalDemographicID == (hospitalUnitID ?? v.NursePDAInfo.HospitalDemographicInfo.HospitalDemographicID)
                                                   //&& v.NursePDAInfo.HospitalDemographicInfo.BedsInUnit > 1000 //v.NursePDAInfo.HospitalDemographicInfo.BedsInUnit == (bedInUnit ?? v.NursePDAInfo.HospitalDemographicInfo.BedsInUnit) && v.NursePDAInfo.HospitalDemographicInfo.BudgetedPatientsPerNurse == (budgetedPatient ?? v.NursePDAInfo.HospitalDemographicInfo.BudgetedPatientsPerNurse)
                                                   //&& v.NursePDAInfo.HospitalDemographicInfo.ElectronicDocumentation == (electronicDocument ?? v.NursePDAInfo.HospitalDemographicInfo.ElectronicDocumentation) &&
                                                   //&& v.NursePDAInfo.HospitalDemographicInfo.UnitType == (unitType ?? v.NursePDAInfo.HospitalDemographicInfo.UnitType) && v.NursePDAInfo.HospitalDemographicInfo.PharmacyType == (pharmacyType ?? v.NursePDAInfo.HospitalDemographicInfo.PharmacyType)
                                               && v.NursePDAInfo.HospitalDemographicInfo.HospitalInfo.State.CountryID == (countryId ?? v.NursePDAInfo.HospitalDemographicInfo.HospitalInfo.State.CountryID) && v.NursePDAInfo.HospitalDemographicInfo.HospitalInfo.StateID == (stateId ?? v.NursePDAInfo.HospitalDemographicInfo.HospitalInfo.StateID)
                                               && v.NursePDAInfo.HospitalDemographicInfo.IsDeleted != true && v.IsErrorExist == false && v.NursePDAInfo.IsErrorExist == false
                                               orderby v.NursePDAInfo.HospitalDemographicInfo.HospitalUnitName
                                               select v);


                    if (unitIds != null)
                    {
                        queryableNursePDADetail = queryableNursePDADetail.Where(SetFilterForUnitIds(unitIds));
                    }
                    if (unitType != null)
                    {
                        queryableNursePDADetail = queryableNursePDADetail.Where(SetFilterForUnitType(unitType));
                    }
                    if (pharmacyType != null)
                    {
                        queryableNursePDADetail = queryableNursePDADetail.Where(SetFilterForPharmacyType(pharmacyType));
                    }
                    if (configName != null)
                    {
                        queryableNursePDADetail = queryableNursePDADetail.Where(SetFilterForConfigName(configName));
                    }
                    queryableNursePDADetail = queryableNursePDADetail.Where(SetFilterForBedsInUnitTest(optBedInUnitFrom, bedInUnitFrom, optBedInUnitTo, bedInUnitTo));
                    queryableNursePDADetail = queryableNursePDADetail.Where(SetFilterForBudgetedPatientsPerNurseTest(optBudgetedPatientFrom, budgetedPatientFrom, optBudgetedPatientTo, budgetedPatientTo));
                    queryableNursePDADetail = queryableNursePDADetail.Where(SetFilterForElectronicDocumentationTest(optElectronicDocumentFrom, electronicDocumentFrom, optElectronicDocumentTo, electronicDocumentTo));
                    queryableNursePDADetail = queryableNursePDADetail.Where(SetFilterForDocByException(docByException));
                }
                else
                {
                    //queryableNursePDADetail = (from v in _objectRMCDataContext.NursePDADetails
                    //                           join dd in _objectRMCDataContext.DynamicDatas on v.NursePDAInfo.HospitalDemographicInfo.HospitalInfoID equals dd.ID
                    //                           where dd.ColumnID == 3
                    //                           where v.NursePDAInfo.HospitalDemographicInfo.StartDate >= Convert.ToDateTime(startDate == null ? Convert.ToString(v.NursePDAInfo.HospitalDemographicInfo.StartDate) : startDate)
                    //                           && v.NursePDAInfo.HospitalDemographicInfo.HospitalDemographicID == (hospitalUnitID ?? v.NursePDAInfo.HospitalDemographicInfo.HospitalDemographicID)
                    //                           && v.NursePDAInfo.HospitalDemographicInfo.HospitalInfo.State.CountryID == (countryId ?? v.NursePDAInfo.HospitalDemographicInfo.HospitalInfo.State.CountryID) && v.NursePDAInfo.HospitalDemographicInfo.HospitalInfo.StateID == (stateId ?? v.NursePDAInfo.HospitalDemographicInfo.HospitalInfo.StateID)
                    //                           && v.NursePDAInfo.HospitalDemographicInfo.IsDeleted != true && v.IsErrorExist == false && v.NursePDAInfo.IsErrorExist == false
                    //                           //&& (SqlMethods.Like(dd.Value, "%" + hospitalType + "%") || SqlMethods.Like(dd.Value, "%" + hospitalType + "%"))
                    //                           orderby v.NursePDAInfo.HospitalDemographicInfo.HospitalUnitName
                    //                           select v);

                    //if (unitType != null)
                    //{
                    //    queryableNursePDADetail = queryableNursePDADetail.Where(SetFilterForUnitType(unitType));
                    //}
                    //if (pharmacyType != null)
                    //{
                    //    queryableNursePDADetail = queryableNursePDADetail.Where(SetFilterForPharmacyType(pharmacyType));
                    //}
                    ////if (hospitalType != null)
                    ////{
                    ////    queryableNursePDADetail = queryableNursePDADetail.Where(SetFilterForHospitalType(hospitalType));
                    ////}
                    //queryableNursePDADetail = queryableNursePDADetail.Where(SetFilterForBedsInUnitTest(optBedInUnitFrom, bedInUnitFrom, optBedInUnitTo, bedInUnitTo));
                    //queryableNursePDADetail = queryableNursePDADetail.Where(SetFilterForBudgetedPatientsPerNurseTest(optBudgetedPatientFrom, budgetedPatientFrom, optBudgetedPatientTo, budgetedPatientTo));
                    //queryableNursePDADetail = queryableNursePDADetail.Where(SetFilterForElectronicDocumentationTest(optElectronicDocumentFrom, electronicDocumentFrom, optElectronicDocumentTo, electronicDocumentTo));
                    //queryableNursePDADetail = queryableNursePDADetail.Where(SetFilterForDocByException(docByException));

                    if (hospitalType == null)
                    {
                        queryableNursePDADetail = (from v in _objectRMCDataContext.NursePDADetails
                                                   where //v.NursePDAInfo.HospitalDemographicInfo.StartDate >= Convert.ToDateTime(startDate == null ? Convert.ToString(v.NursePDAInfo.HospitalDemographicInfo.StartDate) : startDate)
                                                   v.NursePDAInfo.HospitalDemographicInfo.HospitalDemographicID == (hospitalUnitID ?? v.NursePDAInfo.HospitalDemographicInfo.HospitalDemographicID)
                                                   && v.NursePDAInfo.HospitalDemographicInfo.HospitalInfo.State.CountryID == (countryId ?? v.NursePDAInfo.HospitalDemographicInfo.HospitalInfo.State.CountryID) && v.NursePDAInfo.HospitalDemographicInfo.HospitalInfo.StateID == (stateId ?? v.NursePDAInfo.HospitalDemographicInfo.HospitalInfo.StateID)
                                                   && v.NursePDAInfo.HospitalDemographicInfo.IsDeleted != true && v.IsErrorExist == false && v.NursePDAInfo.IsErrorExist == false
                                                   //&& (SqlMethods.Like(v.NursePDAInfo.HospitalDemographicInfo.UnitType, "%" + "Medical" + "%") || SqlMethods.Like(v.NursePDAInfo.HospitalDemographicInfo.UnitType, "%" + "Surgical" + "%"))
                                                   orderby v.NursePDAInfo.HospitalDemographicInfo.HospitalUnitName
                                                   select v);

                        if (unitIds != null)
                        {
                            queryableNursePDADetail = queryableNursePDADetail.Where(SetFilterForUnitIds(unitIds));
                        }
                        if (unitType != null)
                        {
                            queryableNursePDADetail = queryableNursePDADetail.Where(SetFilterForUnitType(unitType));
                        }
                        if (pharmacyType != null)
                        {
                            queryableNursePDADetail = queryableNursePDADetail.Where(SetFilterForPharmacyType(pharmacyType));
                        }
                        if (configName != null)
                        {
                            queryableNursePDADetail = queryableNursePDADetail.Where(SetFilterForConfigName(configName));
                        }
                        //if (hospitalType != null)
                        //{
                        //    queryableNursePDADetail = queryableNursePDADetail.Where(SetFilterForHospitalType(hospitalType));
                        //}
                        queryableNursePDADetail = queryableNursePDADetail.Where(SetFilterForBedsInUnitTest(optBedInUnitFrom, bedInUnitFrom, optBedInUnitTo, bedInUnitTo));
                        queryableNursePDADetail = queryableNursePDADetail.Where(SetFilterForBudgetedPatientsPerNurseTest(optBudgetedPatientFrom, budgetedPatientFrom, optBudgetedPatientTo, budgetedPatientTo));
                        queryableNursePDADetail = queryableNursePDADetail.Where(SetFilterForElectronicDocumentationTest(optElectronicDocumentFrom, electronicDocumentFrom, optElectronicDocumentTo, electronicDocumentTo));
                        queryableNursePDADetail = queryableNursePDADetail.Where(SetFilterForDocByException(docByException));


                    }
                    else
                    {
                        string[] strHospitalType = hospitalType.Split(new char[] { ',' }, System.StringSplitOptions.RemoveEmptyEntries);
                        int count = strHospitalType.Count();
                        if (count == 1)
                        {
                            queryableNursePDADetail = (from v in _objectRMCDataContext.NursePDADetails
                                                       join dd in _objectRMCDataContext.DynamicDatas on v.NursePDAInfo.HospitalDemographicInfo.HospitalInfoID equals dd.ID
                                                       where dd.ColumnID == 3
                                                       where //v.NursePDAInfo.HospitalDemographicInfo.StartDate >= Convert.ToDateTime(startDate == null ? Convert.ToString(v.NursePDAInfo.HospitalDemographicInfo.StartDate) : startDate)
                                                       v.NursePDAInfo.HospitalDemographicInfo.HospitalDemographicID == (hospitalUnitID ?? v.NursePDAInfo.HospitalDemographicInfo.HospitalDemographicID)
                                                       && v.NursePDAInfo.HospitalDemographicInfo.HospitalInfo.State.CountryID == (countryId ?? v.NursePDAInfo.HospitalDemographicInfo.HospitalInfo.State.CountryID) && v.NursePDAInfo.HospitalDemographicInfo.HospitalInfo.StateID == (stateId ?? v.NursePDAInfo.HospitalDemographicInfo.HospitalInfo.StateID)
                                                       && v.NursePDAInfo.HospitalDemographicInfo.IsDeleted != true && v.IsErrorExist == false && v.NursePDAInfo.IsErrorExist == false
                                                       && SqlMethods.Like(dd.Value, "%" + strHospitalType[0] + "%")
                                                       orderby v.NursePDAInfo.HospitalDemographicInfo.HospitalUnitName
                                                       select v);

                            if (unitIds != null)
                            {
                                queryableNursePDADetail = queryableNursePDADetail.Where(SetFilterForUnitIds(unitIds));
                            }
                            if (unitType != null)
                            {
                                queryableNursePDADetail = queryableNursePDADetail.Where(SetFilterForUnitType(unitType));
                            }
                            if (pharmacyType != null)
                            {
                                queryableNursePDADetail = queryableNursePDADetail.Where(SetFilterForPharmacyType(pharmacyType));
                            }
                            if (configName != null)
                            {
                                queryableNursePDADetail = queryableNursePDADetail.Where(SetFilterForConfigName(configName));
                            }
                            //if (hospitalType != null)
                            //{
                            //    queryableNursePDADetail = queryableNursePDADetail.Where(SetFilterForHospitalType(hospitalType));
                            //}
                            queryableNursePDADetail = queryableNursePDADetail.Where(SetFilterForBedsInUnitTest(optBedInUnitFrom, bedInUnitFrom, optBedInUnitTo, bedInUnitTo));
                            queryableNursePDADetail = queryableNursePDADetail.Where(SetFilterForBudgetedPatientsPerNurseTest(optBudgetedPatientFrom, budgetedPatientFrom, optBudgetedPatientTo, budgetedPatientTo));
                            queryableNursePDADetail = queryableNursePDADetail.Where(SetFilterForElectronicDocumentationTest(optElectronicDocumentFrom, electronicDocumentFrom, optElectronicDocumentTo, electronicDocumentTo));
                            queryableNursePDADetail = queryableNursePDADetail.Where(SetFilterForDocByException(docByException));
                        }
                        if (count == 2)
                        {
                            queryableNursePDADetail = (from v in _objectRMCDataContext.NursePDADetails
                                                       join dd in _objectRMCDataContext.DynamicDatas on v.NursePDAInfo.HospitalDemographicInfo.HospitalInfoID equals dd.ID
                                                       where dd.ColumnID == 3
                                                       where //v.NursePDAInfo.HospitalDemographicInfo.StartDate >= Convert.ToDateTime(startDate == null ? Convert.ToString(v.NursePDAInfo.HospitalDemographicInfo.StartDate) : startDate)
                                                       v.NursePDAInfo.HospitalDemographicInfo.HospitalDemographicID == (hospitalUnitID ?? v.NursePDAInfo.HospitalDemographicInfo.HospitalDemographicID)
                                                       && v.NursePDAInfo.HospitalDemographicInfo.HospitalInfo.State.CountryID == (countryId ?? v.NursePDAInfo.HospitalDemographicInfo.HospitalInfo.State.CountryID) && v.NursePDAInfo.HospitalDemographicInfo.HospitalInfo.StateID == (stateId ?? v.NursePDAInfo.HospitalDemographicInfo.HospitalInfo.StateID)
                                                       && v.NursePDAInfo.HospitalDemographicInfo.IsDeleted != true && v.IsErrorExist == false && v.NursePDAInfo.IsErrorExist == false
                                                       && (SqlMethods.Like(dd.Value, "%" + strHospitalType[0].ToString() + "%") || SqlMethods.Like(dd.Value, "%" + strHospitalType[1].ToString() + "%"))
                                                       orderby v.NursePDAInfo.HospitalDemographicInfo.HospitalUnitName
                                                       select v);

                            if (unitIds != null)
                            {
                                queryableNursePDADetail = queryableNursePDADetail.Where(SetFilterForUnitIds(unitIds));
                            }
                            if (unitType != null)
                            {
                                queryableNursePDADetail = queryableNursePDADetail.Where(SetFilterForUnitType(unitType));
                            }
                            if (pharmacyType != null)
                            {
                                queryableNursePDADetail = queryableNursePDADetail.Where(SetFilterForPharmacyType(pharmacyType));
                            }
                            if (configName != null)
                            {
                                queryableNursePDADetail = queryableNursePDADetail.Where(SetFilterForConfigName(configName));
                            }
                            //if (hospitalType != null)
                            //{
                            //    queryableNursePDADetail = queryableNursePDADetail.Where(SetFilterForHospitalType(hospitalType));
                            //}
                            queryableNursePDADetail = queryableNursePDADetail.Where(SetFilterForBedsInUnitTest(optBedInUnitFrom, bedInUnitFrom, optBedInUnitTo, bedInUnitTo));
                            queryableNursePDADetail = queryableNursePDADetail.Where(SetFilterForBudgetedPatientsPerNurseTest(optBudgetedPatientFrom, budgetedPatientFrom, optBudgetedPatientTo, budgetedPatientTo));
                            queryableNursePDADetail = queryableNursePDADetail.Where(SetFilterForElectronicDocumentationTest(optElectronicDocumentFrom, electronicDocumentFrom, optElectronicDocumentTo, electronicDocumentTo));
                            queryableNursePDADetail = queryableNursePDADetail.Where(SetFilterForDocByException(docByException));
                        }
                        if (count == 3)
                        {
                            queryableNursePDADetail = (from v in _objectRMCDataContext.NursePDADetails
                                                       join dd in _objectRMCDataContext.DynamicDatas on v.NursePDAInfo.HospitalDemographicInfo.HospitalInfoID equals dd.ID
                                                       where dd.ColumnID == 3
                                                       where //v.NursePDAInfo.HospitalDemographicInfo.StartDate >= Convert.ToDateTime(startDate == null ? Convert.ToString(v.NursePDAInfo.HospitalDemographicInfo.StartDate) : startDate)
                                                       v.NursePDAInfo.HospitalDemographicInfo.HospitalDemographicID == (hospitalUnitID ?? v.NursePDAInfo.HospitalDemographicInfo.HospitalDemographicID)
                                                       && v.NursePDAInfo.HospitalDemographicInfo.HospitalInfo.State.CountryID == (countryId ?? v.NursePDAInfo.HospitalDemographicInfo.HospitalInfo.State.CountryID) && v.NursePDAInfo.HospitalDemographicInfo.HospitalInfo.StateID == (stateId ?? v.NursePDAInfo.HospitalDemographicInfo.HospitalInfo.StateID)
                                                       && v.NursePDAInfo.HospitalDemographicInfo.IsDeleted != true && v.IsErrorExist == false && v.NursePDAInfo.IsErrorExist == false
                                                       && (SqlMethods.Like(dd.Value, "%" + strHospitalType[0].ToString() + "%") || SqlMethods.Like(dd.Value, "%" + strHospitalType[1].ToString() + "%") || SqlMethods.Like(dd.Value, "%" + strHospitalType[2].ToString() + "%"))
                                                       orderby v.NursePDAInfo.HospitalDemographicInfo.HospitalUnitName
                                                       select v);

                            if (unitIds != null)
                            {
                                queryableNursePDADetail = queryableNursePDADetail.Where(SetFilterForUnitIds(unitIds));
                            }
                            if (unitType != null)
                            {
                                queryableNursePDADetail = queryableNursePDADetail.Where(SetFilterForUnitType(unitType));
                            }
                            if (pharmacyType != null)
                            {
                                queryableNursePDADetail = queryableNursePDADetail.Where(SetFilterForPharmacyType(pharmacyType));
                            }
                            if (configName != null)
                            {
                                queryableNursePDADetail = queryableNursePDADetail.Where(SetFilterForConfigName(configName));
                            }
                            //if (hospitalType != null)
                            //{
                            //    queryableNursePDADetail = queryableNursePDADetail.Where(SetFilterForHospitalType(hospitalType));
                            //}
                            queryableNursePDADetail = queryableNursePDADetail.Where(SetFilterForBedsInUnitTest(optBedInUnitFrom, bedInUnitFrom, optBedInUnitTo, bedInUnitTo));
                            queryableNursePDADetail = queryableNursePDADetail.Where(SetFilterForBudgetedPatientsPerNurseTest(optBudgetedPatientFrom, budgetedPatientFrom, optBudgetedPatientTo, budgetedPatientTo));
                            queryableNursePDADetail = queryableNursePDADetail.Where(SetFilterForElectronicDocumentationTest(optElectronicDocumentFrom, electronicDocumentFrom, optElectronicDocumentTo, electronicDocumentTo));
                            queryableNursePDADetail = queryableNursePDADetail.Where(SetFilterForDocByException(docByException));
                        }
                        if (count == 4)
                        {
                            queryableNursePDADetail = (from v in _objectRMCDataContext.NursePDADetails
                                                       join dd in _objectRMCDataContext.DynamicDatas on v.NursePDAInfo.HospitalDemographicInfo.HospitalInfoID equals dd.ID
                                                       where dd.ColumnID == 3
                                                       where //v.NursePDAInfo.HospitalDemographicInfo.StartDate >= Convert.ToDateTime(startDate == null ? Convert.ToString(v.NursePDAInfo.HospitalDemographicInfo.StartDate) : startDate)
                                                       v.NursePDAInfo.HospitalDemographicInfo.HospitalDemographicID == (hospitalUnitID ?? v.NursePDAInfo.HospitalDemographicInfo.HospitalDemographicID)
                                                       && v.NursePDAInfo.HospitalDemographicInfo.HospitalInfo.State.CountryID == (countryId ?? v.NursePDAInfo.HospitalDemographicInfo.HospitalInfo.State.CountryID) && v.NursePDAInfo.HospitalDemographicInfo.HospitalInfo.StateID == (stateId ?? v.NursePDAInfo.HospitalDemographicInfo.HospitalInfo.StateID)
                                                       && v.NursePDAInfo.HospitalDemographicInfo.IsDeleted != true && v.IsErrorExist == false && v.NursePDAInfo.IsErrorExist == false
                                                       && (SqlMethods.Like(dd.Value, "%" + strHospitalType[0].ToString() + "%") || SqlMethods.Like(dd.Value, "%" + strHospitalType[1].ToString() + "%") || SqlMethods.Like(dd.Value, "%" + strHospitalType[2].ToString() + "%") || SqlMethods.Like(dd.Value, "%" + strHospitalType[3].ToString() + "%"))
                                                       orderby v.NursePDAInfo.HospitalDemographicInfo.HospitalUnitName
                                                       select v);

                            if (unitIds != null)
                            {
                                queryableNursePDADetail = queryableNursePDADetail.Where(SetFilterForUnitIds(unitIds));
                            }
                            if (unitType != null)
                            {
                                queryableNursePDADetail = queryableNursePDADetail.Where(SetFilterForUnitType(unitType));
                            }
                            if (pharmacyType != null)
                            {
                                queryableNursePDADetail = queryableNursePDADetail.Where(SetFilterForPharmacyType(pharmacyType));
                            }
                            if (configName != null)
                            {
                                queryableNursePDADetail = queryableNursePDADetail.Where(SetFilterForConfigName(configName));
                            }
                            //if (hospitalType != null)
                            //{
                            //    queryableNursePDADetail = queryableNursePDADetail.Where(SetFilterForHospitalType(hospitalType));
                            //}
                            queryableNursePDADetail = queryableNursePDADetail.Where(SetFilterForBedsInUnitTest(optBedInUnitFrom, bedInUnitFrom, optBedInUnitTo, bedInUnitTo));
                            queryableNursePDADetail = queryableNursePDADetail.Where(SetFilterForBudgetedPatientsPerNurseTest(optBudgetedPatientFrom, budgetedPatientFrom, optBudgetedPatientTo, budgetedPatientTo));
                            queryableNursePDADetail = queryableNursePDADetail.Where(SetFilterForElectronicDocumentationTest(optElectronicDocumentFrom, electronicDocumentFrom, optElectronicDocumentTo, electronicDocumentTo));
                            queryableNursePDADetail = queryableNursePDADetail.Where(SetFilterForDocByException(docByException));
                        }
                        if (count == 5)
                        {
                            queryableNursePDADetail = (from v in _objectRMCDataContext.NursePDADetails
                                                       join dd in _objectRMCDataContext.DynamicDatas on v.NursePDAInfo.HospitalDemographicInfo.HospitalInfoID equals dd.ID
                                                       where dd.ColumnID == 3
                                                       where //v.NursePDAInfo.HospitalDemographicInfo.StartDate >= Convert.ToDateTime(startDate == null ? Convert.ToString(v.NursePDAInfo.HospitalDemographicInfo.StartDate) : startDate)
                                                       v.NursePDAInfo.HospitalDemographicInfo.HospitalDemographicID == (hospitalUnitID ?? v.NursePDAInfo.HospitalDemographicInfo.HospitalDemographicID)
                                                       && v.NursePDAInfo.HospitalDemographicInfo.HospitalInfo.State.CountryID == (countryId ?? v.NursePDAInfo.HospitalDemographicInfo.HospitalInfo.State.CountryID) && v.NursePDAInfo.HospitalDemographicInfo.HospitalInfo.StateID == (stateId ?? v.NursePDAInfo.HospitalDemographicInfo.HospitalInfo.StateID)
                                                       && v.NursePDAInfo.HospitalDemographicInfo.IsDeleted != true && v.IsErrorExist == false && v.NursePDAInfo.IsErrorExist == false
                                                       && (SqlMethods.Like(dd.Value, "%" + strHospitalType[0].ToString() + "%") || SqlMethods.Like(dd.Value, "%" + strHospitalType[1].ToString() + "%") || SqlMethods.Like(dd.Value, "%" + strHospitalType[2].ToString() + "%") || SqlMethods.Like(dd.Value, "%" + strHospitalType[3].ToString() + "%") || SqlMethods.Like(dd.Value, "%" + strHospitalType[4].ToString() + "%"))
                                                       orderby v.NursePDAInfo.HospitalDemographicInfo.HospitalUnitName
                                                       select v);

                            if (unitIds != null)
                            {
                                queryableNursePDADetail = queryableNursePDADetail.Where(SetFilterForUnitIds(unitIds));
                            }
                            if (unitType != null)
                            {
                                queryableNursePDADetail = queryableNursePDADetail.Where(SetFilterForUnitType(unitType));
                            }
                            if (pharmacyType != null)
                            {
                                queryableNursePDADetail = queryableNursePDADetail.Where(SetFilterForPharmacyType(pharmacyType));
                            }
                            if (configName != null)
                            {
                                queryableNursePDADetail = queryableNursePDADetail.Where(SetFilterForConfigName(configName));
                            }
                            //if (hospitalType != null)
                            //{
                            //    queryableNursePDADetail = queryableNursePDADetail.Where(SetFilterForHospitalType(hospitalType));
                            //}
                            queryableNursePDADetail = queryableNursePDADetail.Where(SetFilterForBedsInUnitTest(optBedInUnitFrom, bedInUnitFrom, optBedInUnitTo, bedInUnitTo));
                            queryableNursePDADetail = queryableNursePDADetail.Where(SetFilterForBudgetedPatientsPerNurseTest(optBudgetedPatientFrom, budgetedPatientFrom, optBudgetedPatientTo, budgetedPatientTo));
                            queryableNursePDADetail = queryableNursePDADetail.Where(SetFilterForElectronicDocumentationTest(optElectronicDocumentFrom, electronicDocumentFrom, optElectronicDocumentTo, electronicDocumentTo));
                            queryableNursePDADetail = queryableNursePDADetail.Where(SetFilterForDocByException(docByException));
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            RMC.BussinessService.BSDataManagement objectBSDataManagement = new RMC.BussinessService.BSDataManagement();

            //Modified the method for year to include with month also
            //objectGenericBEValidation = (from a in _objectRMCDataContext.NursePDADetails

            //                             select new RMC.BusinessEntities.BEValidation 
            //                             { 

            //                             }).ToList<RMC.BusinessEntities.BEValidation>();
                    
                                                                                              
            objectGenericBEValidation = queryableNursePDADetail.Select(v => new RMC.BusinessEntities.BEValidation
            {
                CreatedBy = v.NursePDAInfo.HospitalDemographicInfo.CreatedBy +"{"+ v.NursePDAInfo.HospitalDemographicInfo.HospitalDemographicID+"}",
                HospitalID = v.NursePDAInfo.HospitalDemographicInfo.HospitalInfo.HospitalInfoID,
                HospitalUnitID = v.NursePDAInfo.HospitalDemographicInfo.HospitalDemographicID,
                RecordCounter = v.NursePDAInfo.HospitalDemographicInfo.HospitalInfo.RecordCounter,
                LastLocationID = v.LastLocationID,
                CognitiveCategory = v.CognitiveCategory,
                ResourceRequirementID = v.ResourceRequirementID,
                ActivityID = v.ActivityID,
                LocationID = v.LocationID,
                SubActivityID = v.SubActivityID,
                HospitalUnitName = v.NursePDAInfo.HospitalDemographicInfo.HospitalUnitName,
                CategoryGroupID = 0,
                Year = v.NursePDAInfo.Year,
                Month = v.NursePDAInfo.Month,
                MonthIndex = Convert.ToInt32(v.NursePDAInfo.Month),
                HospitalUnitIDCounter = "#" + v.NursePDAInfo.HospitalDemographicInfo.HospitalInfo.RecordCounter + "_",
                HospitalSize = (_objectRMCDataContext.DynamicDatas
                              .Where(w => w.ColumnName.TableName.ToLower().Trim() == "HospitalInfo".ToLower() && w.ColumnName.ColumnName1.ToLower().Trim() == "BedsInHospital".ToLower() && w.ID == v.NursePDAInfo.HospitalDemographicInfo.HospitalInfo.HospitalInfoID)
                              .Select(s => Convert.ToInt32(s.Value)).FirstOrDefault())
            }).OrderBy(x => x.RecordCounter).ThenBy(x => x.HospitalUnitID).ToList<RMC.BusinessEntities.BEValidation>();

            List<int> objectGenericHospitalIDs = (from v in objectGenericBEValidation
                                                  orderby v.HospitalID
                                                  select v.HospitalID).Distinct().ToList();

            List<RMC.BusinessEntities.BEHospitalUnitInfo> objectGenericListHospitalUnitInfo = (from hu in _objectRMCDataContext.HospitalDemographicInfos
                                                                                               where objectGenericHospitalIDs.Contains(Convert.ToInt32(hu.HospitalInfoID))
                                                                                               orderby hu.HospitalInfoID, hu.HospitalDemographicID
                                                                                               select new RMC.BusinessEntities.BEHospitalUnitInfo
                                                                                               {
                                                                                                   HospitalID = hu.HospitalInfoID.Value,
                                                                                                   HospitalUnitID = hu.HospitalDemographicID,
                                                                                                   HospitalUnitSequence = 0
                                                                                               }).ToList();
            int index = 1;

            objectGenericHospitalIDs.ForEach(delegate(int objectBEHospitalInfo)
            {
                List<RMC.BusinessEntities.BEHospitalUnitInfo> objectGenericList = objectGenericListHospitalUnitInfo.FindAll(delegate(RMC.BusinessEntities.BEHospitalUnitInfo objectBEHospitalUnitInfo)
                {
                    if (objectBEHospitalInfo == objectBEHospitalUnitInfo.HospitalID)
                    {
                        objectBEHospitalUnitInfo.HospitalUnitSequence = index++;
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                });
                index = 1;
            });

            
            //---To make units like #4_1, #4_2 format
            /*List<int> objectGenericHospitalIDs = (from v in objectGenericBEValidation
                                                  //orderby v.RecordCounter, v.HospitalUnitID
                                                  orderby v.HospitalID
                                                  select v.HospitalID).Distinct().ToList();*/

            /*List<RMC.BusinessEntities.BEValidation> objectHospitalIdUnitId = null;
            //objectHospitalIdUnitId = (from a in objectGenericBEValidation
            //                          group a by new { a.HospitalUnitID, a.HospitalID } into g
            //                          select new RMC.BusinessEntities.BEValidation
            //                          {
            //                              HospitalID = g.Key.HospitalID,
            //                              HospitalUnitID = g.Key.HospitalUnitID
            //                          }).OrderBy(x => x.HospitalID).ToList();

            objectHospitalIdUnitId = (from a in _objectRMCDataContext.HospitalDemographicInfos 
                                      where objectGenericHospitalIDs.Contains(Convert.ToInt32(a.HospitalInfoID))
                                      select new RMC.BusinessEntities.BEValidation
                                      {
                                          HospitalID = Convert.ToInt32(a.HospitalInfoID),
                                          HospitalUnitID = a.HospitalDemographicID 
                                      }).OrderBy(x => x.HospitalID).ToList();*/

            /*List<RMC.BusinessEntities.BEValidation> objectGenericBEValidationCollection = null;
            List<RMC.BusinessEntities.BEValidation> objectGenericNewBEValidation = new List<RMC.BusinessEntities.BEValidation>();
            objectGenericHospitalIDs.ForEach(delegate(int hospitalID)
            {
                objectGenericBEValidationCollection = objectGenericBEValidation.FindAll(delegate(RMC.BusinessEntities.BEValidation objectBEVal)
                {
                    return objectBEVal.HospitalID == hospitalID;
                });

                //objectTempHospUnitId = (from a in _objectRMCDataContext.HospitalDemographicInfos
                //                        where a.HospitalInfoID == hospitalID
                //                        select new RMC.BusinessEntities.BEValidation
                //                        {
                //                            HospitalUnitID = a.HospitalDemographicID
                //                        }).OrderBy(x => x.HospitalUnitID).ToList();

                int indexCounter = 0;
                //int hospUnitID = 0;
                objectGenericBEValidationCollection.ForEach(delegate(RMC.BusinessEntities.BEValidation objectBEVal)
                {
                    List<RMC.BusinessEntities.BEValidation> objectHospitalIdUnitIdTemp = null;
                    objectHospitalIdUnitIdTemp = objectHospitalIdUnitId.Where(x => x.HospitalID == hospitalID).ToList();
                    for (int i = 0; i < objectHospitalIdUnitIdTemp.Count(); i++)
                    {
                        if (objectHospitalIdUnitIdTemp[i].HospitalUnitID == objectBEVal.HospitalUnitID)
                        {
                            indexCounter = i + 1;
                        }
                    }
                    objectBEVal.HospitalUnitIDCounter = "#" + Convert.ToString(objectBEVal.RecordCounter) + "_" + Convert.ToString(indexCounter);
                    objectGenericNewBEValidation.Add(objectBEVal);
                });
                               
                //objectHospitalIdUnitId.FindAll(delegate(RMC.BusinessEntities.BEValidation objectTemp)
                //{
                //    return objectTemp.HospitalID == hospitalID;  
                //}).ForEach(delegate(RMC.BusinessEntities.BEValidation objectBEValidationForUnit)
                //{
                //    indexCounter++;
                //    objectGenericBEValidationCollection.FindAll(delegate(RMC.BusinessEntities.BEValidation objectBEVal)
                //    {
                //        return objectBEValidationForUnit.HospitalUnitID == objectBEVal.HospitalUnitID;
                //    }).TrueForAll(delegate(RMC.BusinessEntities.BEValidation objectAssignVal)
                //    {
                //        objectAssignVal.HospitalUnitIDCounter = "#" + Convert.ToString(objectAssignVal.RecordCounter) + "_" + Convert.ToString(indexCounter);
                //        objectGenericNewBEValidation.Add(objectAssignVal);
                //        return true;
                //    });
                //});
            });*/
            //----------------------------------------

            ////---To make units like #4_1, #4_2 format
            //List<int> objectGenericHospitalIDs = (from v in objectGenericBEValidation
            //                                      orderby v.RecordCounter, v.HospitalUnitID
            //                                      select v.HospitalID).Distinct().ToList();

            //List<RMC.BusinessEntities.BEValidation> objectTempHospUnitId = null;
            //List<RMC.BusinessEntities.BEValidation> objectGenericBEValidationCollection = null;
            //List<RMC.BusinessEntities.BEValidation> objectGenericNewBEValidation = new List<RMC.BusinessEntities.BEValidation>();
            //objectGenericHospitalIDs.ForEach(delegate(int hospitalID)
            //{
            //    objectGenericBEValidationCollection = objectGenericBEValidation.FindAll(delegate(RMC.BusinessEntities.BEValidation objectBEVal)
            //    {
            //        return objectBEVal.HospitalID == hospitalID;
            //    });

            //    objectTempHospUnitId = (from a in _objectRMCDataContext.HospitalDemographicInfos
            //                            where a.HospitalInfoID == hospitalID
            //                            select new RMC.BusinessEntities.BEValidation
            //                            {
            //                                HospitalUnitID = a.HospitalDemographicID
            //                            }).OrderBy(x => x.HospitalUnitID).ToList();

            //    int indexCounter = 0;
            //    //int hospUnitID = 0;
            //    objectGenericBEValidationCollection.ForEach(delegate(RMC.BusinessEntities.BEValidation objectBEVal)
            //    {
            //        //if (hospUnitID == 0 || hospUnitID != objectBEVal.HospitalUnitID)
            //        //{

            //        //    hospUnitID = objectBEVal.HospitalUnitID;
            //        //    indexCounter++;
            //        //}
            //        for (int i = 0; i < objectTempHospUnitId.Count(); i++)
            //        {
            //            if (objectTempHospUnitId[i].HospitalUnitID == objectBEVal.HospitalUnitID)
            //            {
            //                indexCounter = i + 1;
            //            }
            //        }
            //        objectBEVal.HospitalUnitIDCounter = "#" + Convert.ToString(objectBEVal.RecordCounter) + "_" + Convert.ToString(indexCounter);
            //        objectGenericNewBEValidation.Add(objectBEVal);
            //    });
            //});
            ////----------------------------------------

            if (hospitalSizeFrom > 0)
            {
                objectGenericBEValidation = objectGenericBEValidation.Where(SetFilterForHospitalSizeTest(optHospitalSizeFrom, hospitalSizeFrom, optHospitalSizeTo, hospitalSizeTo)).ToList<RMC.BusinessEntities.BEValidation>();
            }

            //_objectRMCDataContext.ObjectTrackingEnabled = true;

            //if (hospitalUnitID == null)
            //{
            //    HttpContext.Current.Session["SearchHospitalsDataForBenchmark"] = objectGenericNewBEValidation.OrderBy(x => x.RecordCounter).ToList();
            //}
            if (hospitalUnitID == null)
            {
                MaintainSessions.SessionHospitalUnitCounterID = objectGenericListHospitalUnitInfo;
                MaintainSessions.SessionSearchHospitalData = objectGenericBEValidation;
            }
            //MaintainSessions.SessionHospitalUnitCounterID = objectGenericListHospitalUnitInfo;
            //MaintainSessions.SessionSearchHospitalData = objectGenericBEValidation;

            return objectGenericBEValidation.OrderBy(x => x.RecordCounter).ToList();
        }

        /// <summary>
        /// Calculates Function Values for HospitalBenchmarkSummary Report for each Columns
        /// </summary>
        public List<RMC.BusinessEntities.BEFunctionNames> CalculateFunctionValuesTest(string activitiesID,string valueAddedCategoryID, string OthersCategoryID, string LocationCategoryID, int? hospitalUnitID, int? firstYear, int? lastYear, int? firstMonth, int? lastMonth, int? bedInUnitFrom, int optBedInUnitFrom, int? bedInUnitTo, int optBedInUnitTo, float? budgetedPatientFrom, int optBudgetedPatientFrom, float? budgetedPatientTo, int optBudgetedPatientTo, string startDate, string endDate, int? electronicDocumentFrom, int optElectronicDocumentFrom, int? electronicDocumentTo, int optElectronicDocumentTo, int docByException, string unitType, string pharmacyType, string hospitalType, int optHospitalSizeFrom, int? hospitalSizeFrom, int optHospitalSizeTo, int? hospitalSizeTo, int? countryId, int? stateId, string activities, string value, string others, string location, int? dataPointsFrom, int optDataPointsFrom, int? dataPointsTo, int optdataPointsTo, string configName, string unitIds)
        {
            try
            {
                List<RMC.BusinessEntities.BEReports> objectNewGenericBEReports = null;
                List<RMC.BusinessEntities.BEFunctionNames> objectGenericBEFunctionNames = new List<RMC.BusinessEntities.BEFunctionNames>();

                List<RMC.BusinessEntities.BEReports> objectGenericListBEReportValueAddedProfile = null;
                List<RMC.BusinessEntities.BEReports> objectGenericListBEReportValueAdded = null;
                List<RMC.BusinessEntities.BEReports> objectGenericListBEReportOthers = null;
                List<RMC.BusinessEntities.BEReports> objectGenericListBEReportLocation = null;

                //List<RMC.BusinessEntities.BEValidation> ObjectSearchHospitalsDataForBenchmark = SearchHospitalsDataForBenchmarkTest(hospitalUnitID, firstYear, lastYear, firstMonth, lastMonth, bedInUnitFrom, optBedInUnitFrom, bedInUnitTo, optBedInUnitTo, budgetedPatientFrom, optBudgetedPatientFrom, budgetedPatientTo, optBudgetedPatientTo, startDate, endDate, electronicDocumentFrom, optElectronicDocumentFrom, electronicDocumentTo, optElectronicDocumentTo, docByException, unitType, pharmacyType, hospitalType, optHospitalSizeFrom, hospitalSizeFrom, optHospitalSizeTo, hospitalSizeTo, countryId, stateId, configName);

                List<RMC.BusinessEntities.BEValidation> ObjectSearchHospitalsDataForBenchmark = null;

                //if (HttpContext.Current.Session["SearchHospitalsDataForBenchmark"] != null)
                //{
                //    ObjectSearchHospitalsDataForBenchmark = (List<RMC.BusinessEntities.BEValidation>)HttpContext.Current.Session["SearchHospitalsDataForBenchmark"];
                //}
                //else
                //{
                long counter = 0;
                bool checkSessionAccess = false;
                counter = NationalDatabaseRecordCount(hospitalUnitID, firstYear, lastYear, firstMonth, lastMonth, bedInUnitFrom, optBedInUnitFrom, bedInUnitTo, optBedInUnitTo, budgetedPatientFrom, optBudgetedPatientFrom, budgetedPatientTo, optBudgetedPatientTo, startDate, endDate, electronicDocumentFrom, optElectronicDocumentFrom, electronicDocumentTo, optElectronicDocumentTo, docByException, unitType, pharmacyType, hospitalType, optHospitalSizeFrom, hospitalSizeFrom, optHospitalSizeTo, hospitalSizeTo, countryId, stateId, configName,unitIds);

                if (valueAddedCategoryID != null)
                {
                    checkSessionAccess = CheckProfileCount("VA", valueAddedCategoryID);
                }
                else
                {
                    checkSessionAccess = removeProfileCount("VA");
                }
                if (OthersCategoryID != null && checkSessionAccess == false)
                {
                    checkSessionAccess = CheckProfileCount("OC", OthersCategoryID);
                }
                else if (checkSessionAccess == false)
                {
                    checkSessionAccess = removeProfileCount("OC");
                }
                if (LocationCategoryID != null && checkSessionAccess == false)
                {
                    checkSessionAccess = CheckProfileCount("LC", LocationCategoryID);
                }
                else if (checkSessionAccess == false)
                {
                    checkSessionAccess = removeProfileCount("LC");
                }
                if (MaintainSessions.SessionFunctionValues != null && checkSessionAccess == false && counter == MaintainSessions.NationalDatabaseCount)
                {
                    objectGenericBEFunctionNames = MaintainSessions.SessionFunctionValues;
                    return objectGenericBEFunctionNames;
                }
                else
                {
                    MaintainSessions.SessionProfiles = null;
                    if (valueAddedCategoryID != null)
                    {
                        MaintainSessions.SessionProfiles = "VA" + ":" + valueAddedCategoryID;
                    }
                    if (OthersCategoryID != null)
                    {
                        if (MaintainSessions.SessionProfiles != null && MaintainSessions.SessionProfiles != string.Empty)
                        {
                            MaintainSessions.SessionProfiles += "^OC" + ":" + OthersCategoryID;
                        }
                        else
                        {
                            MaintainSessions.SessionProfiles = "OC" + ":" + OthersCategoryID;
                        }
                    }
                    if (LocationCategoryID != null)
                    {
                        if (MaintainSessions.SessionProfiles != null && MaintainSessions.SessionProfiles != string.Empty)
                        {
                            MaintainSessions.SessionProfiles += "^LC" + ":" + LocationCategoryID;
                        }
                        else
                        {
                            MaintainSessions.SessionProfiles = "LC" + ":" + LocationCategoryID;
                        }
                    }
                }

                if (MaintainSessions.NationalDatabaseCount != counter)
                {
                    ObjectSearchHospitalsDataForBenchmark = SearchHospitalsDataForBenchmarkTest(hospitalUnitID, firstYear, lastYear, firstMonth, lastMonth, bedInUnitFrom, optBedInUnitFrom, bedInUnitTo, optBedInUnitTo, budgetedPatientFrom, optBudgetedPatientFrom, budgetedPatientTo, optBudgetedPatientTo, startDate, endDate, electronicDocumentFrom, optElectronicDocumentFrom, electronicDocumentTo, optElectronicDocumentTo, docByException, unitType, pharmacyType, hospitalType, optHospitalSizeFrom, hospitalSizeFrom, optHospitalSizeTo, hospitalSizeTo, countryId, stateId, configName, unitIds);
                    MaintainSessions.NationalDatabaseCount = counter;
                }
                else
                {
                    ObjectSearchHospitalsDataForBenchmark = MaintainSessions.SessionSearchHospitalData;
                }
                //}
                //activities
                if (activitiesID != null)
                {
                    if (activitiesID.Contains(","))
                    {
                        string[] strArrvalue = null;
                        strArrvalue = activities.Split(new char[] { ',' });
                        string[] strArrvalueAddedCategoryID = null;
                        strArrvalueAddedCategoryID = activitiesID.Split(new char[] { ',' });
                        int[] intArrvalueAddedCategoryID = new int[strArrvalueAddedCategoryID.Length];
                        for (int i = 0; i < strArrvalueAddedCategoryID.Length; i++)
                        {
                            intArrvalueAddedCategoryID[i] = int.Parse(strArrvalueAddedCategoryID[i]);
                            objectGenericListBEReportValueAddedProfile = CalculationOnDataByProfileTest(strArrvalue[i], "activities", GetCategoryProfileDataByProfileID(intArrvalueAddedCategoryID[i]), ObjectSearchHospitalsDataForBenchmark, dataPointsFrom, optDataPointsFrom, dataPointsTo, optdataPointsTo);
                            if (i == 0)
                            {
                                objectGenericListBEReportValueAdded = objectGenericListBEReportValueAddedProfile;
                            }
                            if (i != 0)
                            {
                                objectGenericListBEReportValueAdded.AddRange(objectGenericListBEReportValueAddedProfile);
                            }
                        }
                    }
                    else
                    {
                        objectGenericListBEReportValueAdded = CalculationOnDataByProfileTest(activities, "activities", GetCategoryProfileDataByProfileID(Convert.ToInt32(activitiesID)), ObjectSearchHospitalsDataForBenchmark, dataPointsFrom, optDataPointsFrom, dataPointsTo, optdataPointsTo);
                    }

                }

                //valueAddedCategoryID
                if (valueAddedCategoryID != null)
                {
                    if (valueAddedCategoryID.Contains(","))
                    {
                        string[] strArrvalue = null;
                        strArrvalue = value.Split(new char[] { ',' });
                        string[] strArrvalueAddedCategoryID = null;
                        strArrvalueAddedCategoryID = valueAddedCategoryID.Split(new char[] { ',' });
                        int[] intArrvalueAddedCategoryID = new int[strArrvalueAddedCategoryID.Length];
                        for (int i = 0; i < strArrvalueAddedCategoryID.Length; i++)
                        {
                            intArrvalueAddedCategoryID[i] = int.Parse(strArrvalueAddedCategoryID[i]);
                            objectGenericListBEReportValueAddedProfile = CalculationOnDataByProfileTest(strArrvalue[i], "value added", GetCategoryProfileDataByProfileID(intArrvalueAddedCategoryID[i]), ObjectSearchHospitalsDataForBenchmark, dataPointsFrom, optDataPointsFrom, dataPointsTo, optdataPointsTo);
                            if (i == 0)
                            {
                                objectGenericListBEReportValueAdded = objectGenericListBEReportValueAddedProfile;
                            }
                            if (i != 0)
                            {
                                objectGenericListBEReportValueAdded.AddRange(objectGenericListBEReportValueAddedProfile);
                            }
                        }
                    }
                    else
                    {
                        objectGenericListBEReportValueAdded = CalculationOnDataByProfileTest(value, "value added", GetCategoryProfileDataByProfileID(Convert.ToInt32(valueAddedCategoryID)), ObjectSearchHospitalsDataForBenchmark, dataPointsFrom, optDataPointsFrom, dataPointsTo, optdataPointsTo);
                    }

                }

                //OthersCategoryID
                if (OthersCategoryID != null)
                {
                    if (OthersCategoryID.Contains(","))
                    {
                        string[] strArrothers = null;
                        strArrothers = others.Split(new char[] { ',' });
                        string[] strArrOthersCategoryID = null;
                        strArrOthersCategoryID = OthersCategoryID.Split(new char[] { ',' });
                        int[] intArrOthersCategoryID = new int[strArrOthersCategoryID.Length];
                        for (int i = 0; i < strArrOthersCategoryID.Length; i++)
                        {
                            intArrOthersCategoryID[i] = int.Parse(strArrOthersCategoryID[i]);
                            objectGenericListBEReportOthers = CalculationOnDataByProfileTest(strArrothers[i], "others", GetCategoryProfileDataByProfileID(intArrOthersCategoryID[i]), ObjectSearchHospitalsDataForBenchmark, dataPointsFrom, optDataPointsFrom, dataPointsTo, optdataPointsTo);
                            if (objectGenericListBEReportValueAdded == null)
                            {
                                objectGenericListBEReportValueAdded = objectGenericListBEReportOthers;
                            }
                            else
                            {
                                objectGenericListBEReportValueAdded.AddRange(objectGenericListBEReportOthers);
                            }
                        }
                    }
                    else
                    {
                        objectGenericListBEReportOthers = CalculationOnDataByProfileTest(others, "others", GetCategoryProfileDataByProfileID(Convert.ToInt32(OthersCategoryID)), ObjectSearchHospitalsDataForBenchmark, dataPointsFrom, optDataPointsFrom, dataPointsTo, optdataPointsTo);
                        if (objectGenericListBEReportValueAdded == null)
                        {
                            objectGenericListBEReportValueAdded = objectGenericListBEReportOthers;
                        }
                        else
                        {
                            objectGenericListBEReportValueAdded.AddRange(objectGenericListBEReportOthers);
                        }
                    }

                }
                //LocationCategoryID;
                if (LocationCategoryID != null)
                {
                    if (LocationCategoryID.Contains(","))
                    {
                        string[] strArrLocation = null;
                        strArrLocation = location.Split(new char[] { ',' });
                        string[] strArrLocationCategoryID = null;
                        strArrLocationCategoryID = LocationCategoryID.Split(new char[] { ',' });
                        int[] intArrLocationCategoryID = new int[strArrLocationCategoryID.Length];
                        for (int i = 0; i < strArrLocationCategoryID.Length; i++)
                        {
                            intArrLocationCategoryID[i] = int.Parse(strArrLocationCategoryID[i]);
                            objectGenericListBEReportLocation = CalculationOnDataByProfileTest(strArrLocation[i], "location", GetCategoryProfileDataByProfileID(intArrLocationCategoryID[i]), ObjectSearchHospitalsDataForBenchmark, dataPointsFrom, optDataPointsFrom, dataPointsTo, optdataPointsTo);
                            if (objectGenericListBEReportValueAdded == null)
                            {
                                objectGenericListBEReportValueAdded = objectGenericListBEReportLocation;
                            }
                            else
                            {
                                objectGenericListBEReportValueAdded.AddRange(objectGenericListBEReportLocation);
                            }
                        }
                    }
                    else
                    {
                        objectGenericListBEReportLocation = CalculationOnDataByProfileTest(location, "location", GetCategoryProfileDataByProfileID(Convert.ToInt32(LocationCategoryID)), ObjectSearchHospitalsDataForBenchmark, dataPointsFrom, optDataPointsFrom, dataPointsTo, optdataPointsTo);
                        if (objectGenericListBEReportValueAdded == null)
                        {
                            objectGenericListBEReportValueAdded = objectGenericListBEReportLocation;
                        }
                        else
                        {
                            objectGenericListBEReportValueAdded.AddRange(objectGenericListBEReportLocation);
                        }
                    }

                }
                
                
                objectNewGenericBEReports = objectGenericListBEReportValueAdded;
                //Added By Davinder Kumar for purpose of code optimization.
                MaintainSessions.SessionHospitalBenchmarkSummary = objectGenericListBEReportValueAdded;
                foreach (var r in objectNewGenericBEReports.GroupBy(o => o.ColumnName))
                {
                    //Calculate Minimum value.
                    RMC.BusinessEntities.BEFunctionNames objectNewBEFunctionNamesMin = new RMC.BusinessEntities.BEFunctionNames();
                    double valueMin = 0;
                    valueMin = r.Select(s => Convert.ToDouble((s.Values.Length > 0) ? s.Values.Substring(0, s.Values.Length - 1) : "0")).Min();

                    objectNewBEFunctionNamesMin.ColumnName = r.FirstOrDefault().ColumnName;
                    objectNewBEFunctionNamesMin.FunctionName = "Minimum";
                    objectNewBEFunctionNamesMin.FunctionNameDouble = valueMin;
                    objectNewBEFunctionNamesMin.FunctionValueText = string.Format("{0:#.##}%", (valueMin == 0) ? "0.00" : valueMin.ToString());

                    objectGenericBEFunctionNames.Add(objectNewBEFunctionNamesMin);

                    //Calculate Maximum value
                    RMC.BusinessEntities.BEFunctionNames objectNewBEFunctionNamesMax = new RMC.BusinessEntities.BEFunctionNames();
                    double valueMax = 0;
                    valueMax = r.Select(s => Convert.ToDouble((s.Values.Length > 0) ? s.Values.Substring(0, s.Values.Length - 1) : "0")).Max();

                    objectNewBEFunctionNamesMax.ColumnName = r.FirstOrDefault().ColumnName;
                    objectNewBEFunctionNamesMax.FunctionName = "Maximum";
                    objectNewBEFunctionNamesMax.FunctionNameDouble = valueMax;
                    objectNewBEFunctionNamesMax.FunctionValueText = string.Format("{0:#.##}%", (valueMax == 0) ? "0.00" : valueMax.ToString());

                    //Calculate Average value
                    RMC.BusinessEntities.BEFunctionNames objectNewBEFunctionNamesAvg = new RMC.BusinessEntities.BEFunctionNames();
                    List<double> valueSum = null;
                    valueSum = r.Select(s => Convert.ToDouble((s.Values.Length > 0) ? s.Values.Substring(0, s.Values.Length - 1) : "0")).ToList();

                    objectNewBEFunctionNamesAvg.ColumnName = r.FirstOrDefault().ColumnName;
                    objectNewBEFunctionNamesAvg.FunctionName = "Average";
                    objectNewBEFunctionNamesAvg.FunctionNameDouble = (valueSum.Sum() / r.Select(s => s.RowName).Distinct().Count());
                    objectNewBEFunctionNamesAvg.FunctionValueText = string.Format("{0:#.##}%", (objectNewBEFunctionNamesAvg.FunctionNameDouble == 0) ? "0.00" : string.Format("{0:#.##}", objectNewBEFunctionNamesAvg.FunctionNameDouble));

                    //Median Value.
                    RMC.BusinessEntities.BEFunctionNames objectNewBEFunctionNamesMed = new RMC.BusinessEntities.BEFunctionNames();
                    int median = 0;
                    int medianEven = 0;
                    int count = r.Select(s => s.RowName).Distinct().Count();
                    if (count % 2 == 0)
                    {
                        median = count / 2;
                        medianEven = median + 1;

                        objectNewBEFunctionNamesMed.ColumnName = r.FirstOrDefault().ColumnName;
                        objectNewBEFunctionNamesMed.FunctionName = "Median";
                        objectNewBEFunctionNamesMed.FunctionNameDouble = (r.OrderBy(x => x.ValuesSum).Select(s => s.ValuesSum).ToList().ElementAt(median - 1) + r.OrderBy(x => x.ValuesSum).Select(s => s.ValuesSum).ToList().ElementAt(medianEven - 1)) / 2;
                        objectNewBEFunctionNamesMed.FunctionValueText = string.Format("{0:#.##}%", string.Format("{0:#.##}", (objectNewBEFunctionNamesMed.FunctionNameDouble == 0.00) ? "0.00" : string.Format("{0:#.##}", objectNewBEFunctionNamesMed.FunctionNameDouble)));
                    }
                    else
                    {
                        median = (count + 1) / 2;

                        objectNewBEFunctionNamesMed.ColumnName = r.FirstOrDefault().ColumnName;
                        objectNewBEFunctionNamesMed.FunctionName = "Median";
                        objectNewBEFunctionNamesMed.FunctionNameDouble = r.OrderBy(x => x.ValuesSum).Select(s => s.ValuesSum).ToList().ElementAt(median - 1);
                        objectNewBEFunctionNamesMed.FunctionValueText = string.Format("{0:#.##}%", string.Format("{0:#.##}", (objectNewBEFunctionNamesMed.FunctionNameDouble == 0.00) ? "0.00" : string.Format("{0:#.##}", objectNewBEFunctionNamesMed.FunctionNameDouble)));
                    }
                    //median--;


                    //Quartile
                    RMC.BusinessEntities.BEFunctionNames objectNewBEFunctionNamesQuartile1 = new RMC.BusinessEntities.BEFunctionNames();

                    objectNewBEFunctionNamesQuartile1.ColumnName = r.FirstOrDefault().ColumnName;
                    objectNewBEFunctionNamesQuartile1.FunctionName = "Quartile(1)";
                    objectNewBEFunctionNamesQuartile1.FunctionNameDouble = CalculateQuartile(count, 1, r.Select(s => s.ValuesSum).ToList());
                    objectNewBEFunctionNamesQuartile1.FunctionValueText = string.Format("{0:#.##}%", (objectNewBEFunctionNamesQuartile1.FunctionNameDouble == 0.00) ? "0.00" : string.Format("{0:#.##}", objectNewBEFunctionNamesQuartile1.FunctionNameDouble));

                    RMC.BusinessEntities.BEFunctionNames objectNewBEFunctionNamesQuartile3 = new RMC.BusinessEntities.BEFunctionNames();

                    objectNewBEFunctionNamesQuartile3.ColumnName = r.FirstOrDefault().ColumnName;
                    objectNewBEFunctionNamesQuartile3.FunctionName = "Quartile(3)";
                    objectNewBEFunctionNamesQuartile3.FunctionNameDouble = CalculateQuartile(count, 3, r.Select(s => s.ValuesSum).ToList());
                    objectNewBEFunctionNamesQuartile3.FunctionValueText = string.Format("{0:#.##}%", (objectNewBEFunctionNamesQuartile3.FunctionNameDouble == 0.00) ? "0.00" : string.Format("{0:#.##}", objectNewBEFunctionNamesQuartile3.FunctionNameDouble));

                    objectGenericBEFunctionNames.Add(objectNewBEFunctionNamesQuartile1);
                    objectGenericBEFunctionNames.Add(objectNewBEFunctionNamesMed);
                    objectGenericBEFunctionNames.Add(objectNewBEFunctionNamesAvg);
                    objectGenericBEFunctionNames.Add(objectNewBEFunctionNamesQuartile3);
                    objectGenericBEFunctionNames.Add(objectNewBEFunctionNamesMax);
                }
                //objectGenericBEFunctionNames.OrderBy(o => o.ColumnName).ToList();
                //if (HttpContext.Current.Session["SearchHospitalsDataForBenchmark"] != null)
                //{
                //    HttpContext.Current.Session.Remove("SearchHospitalsDataForBenchmark");
                //}
                MaintainSessions.SessionFunctionValues = objectGenericBEFunctionNames;
                return objectGenericBEFunctionNames;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                //DisposeGlobalObjects();
            }
        }
        //created by cm 19 oct
        public List<RMC.BusinessEntities.BEFunctionNames> CalculateFunctionValuesTestForUnitAcc(string dbValues, string ProfileCategoryValue, string ProfileSubCategoryValue, string valueAddedCategoryID, string OthersCategoryID, string LocationCategoryID, int? hospitalUnitID, int? firstYear, int? lastYear, int? firstMonth, int? lastMonth, int? bedInUnitFrom, int optBedInUnitFrom, int? bedInUnitTo, int optBedInUnitTo, float? budgetedPatientFrom, int optBudgetedPatientFrom, float? budgetedPatientTo, int optBudgetedPatientTo, string startDate, string endDate, int? electronicDocumentFrom, int optElectronicDocumentFrom, int? electronicDocumentTo, int optElectronicDocumentTo, int docByException, string unitType, string pharmacyType, string hospitalType, int optHospitalSizeFrom, int? hospitalSizeFrom, int optHospitalSizeTo, int? hospitalSizeTo, int? countryId, int? stateId, string activities, string value, string others, string location, int? dataPointsFrom, int optDataPointsFrom, int? dataPointsTo, int optdataPointsTo, string configName, string unitIds)
        {
            try
            {
                    List<RMC.BusinessEntities.BEReports> objectNewGenericBEReports = null;
                    List<RMC.BusinessEntities.BEFunctionNames> objectGenericBEFunctionNames = new List<RMC.BusinessEntities.BEFunctionNames>();

                    List<RMC.BusinessEntities.BEReports> objectGenericListBEReportValueAddedProfile = null;
                    List<RMC.BusinessEntities.BEReports> objectGenericListBEReportValueAdded = null;
                    List<RMC.BusinessEntities.BEReports> objectGenericListBEReportOthers = null;
                    List<RMC.BusinessEntities.BEReports> objectGenericListBEReportLocation = null;
                    List<RMC.BusinessEntities.BEReports> objectGenericListBEReportActivity = null;

                    //List<RMC.BusinessEntities.BEValidation> ObjectSearchHospitalsDataForBenchmark = SearchHospitalsDataForBenchmarkTest(hospitalUnitID, firstYear, lastYear, firstMonth, lastMonth, bedInUnitFrom, optBedInUnitFrom, bedInUnitTo, optBedInUnitTo, budgetedPatientFrom, optBudgetedPatientFrom, budgetedPatientTo, optBudgetedPatientTo, startDate, endDate, electronicDocumentFrom, optElectronicDocumentFrom, electronicDocumentTo, optElectronicDocumentTo, docByException, unitType, pharmacyType, hospitalType, optHospitalSizeFrom, hospitalSizeFrom, optHospitalSizeTo, hospitalSizeTo, countryId, stateId, configName);

                    List<RMC.BusinessEntities.BEValidation> ObjectSearchHospitalsDataForBenchmark = null;

                    //if (HttpContext.Current.Session["SearchHospitalsDataForBenchmark"] != null)
                    //{
                    //    ObjectSearchHospitalsDataForBenchmark = (List<RMC.BusinessEntities.BEValidation>)HttpContext.Current.Session["SearchHospitalsDataForBenchmark"];
                    //}
                    //else
                    //{
                    long counter = 0;
                    bool checkSessionAccess = false;
                    counter = NationalDatabaseRecordCount(hospitalUnitID, firstYear, lastYear, firstMonth, lastMonth, bedInUnitFrom, optBedInUnitFrom, bedInUnitTo, optBedInUnitTo, budgetedPatientFrom, optBudgetedPatientFrom, budgetedPatientTo, optBudgetedPatientTo, startDate, endDate, electronicDocumentFrom, optElectronicDocumentFrom, electronicDocumentTo, optElectronicDocumentTo, docByException, unitType, pharmacyType, hospitalType, optHospitalSizeFrom, hospitalSizeFrom, optHospitalSizeTo, hospitalSizeTo, countryId, stateId, configName, unitIds);

                    if (valueAddedCategoryID != null)
                    {
                        checkSessionAccess = CheckProfileCount("VA", valueAddedCategoryID);
                    }
                    else
                    {
                        checkSessionAccess = removeProfileCount("VA");
                    }
                    if (OthersCategoryID != null && checkSessionAccess == false)
                    {
                        checkSessionAccess = CheckProfileCount("OC", OthersCategoryID);
                    }
                    else if (checkSessionAccess == false)
                    {
                        checkSessionAccess = removeProfileCount("OC");
                    }
                    if (LocationCategoryID != null && checkSessionAccess == false)
                    {
                        checkSessionAccess = CheckProfileCount("LC", LocationCategoryID);
                    }
                    else if (checkSessionAccess == false)
                    {
                        checkSessionAccess = removeProfileCount("LC");
                    }
                    if (MaintainSessions.SessionFunctionValues != null && checkSessionAccess == false && counter == MaintainSessions.NationalDatabaseCount)
                    {
                        objectGenericBEFunctionNames = MaintainSessions.SessionFunctionValues;
                        return objectGenericBEFunctionNames;
                    }
                    else
                    {
                        MaintainSessions.SessionProfiles = null;
                        if (valueAddedCategoryID != null)
                        {
                            MaintainSessions.SessionProfiles = "VA" + ":" + valueAddedCategoryID;
                        }
                        if (OthersCategoryID != null)
                        {
                            if (MaintainSessions.SessionProfiles != null && MaintainSessions.SessionProfiles != string.Empty)
                            {
                                MaintainSessions.SessionProfiles += "^OC" + ":" + OthersCategoryID;
                            }
                            else
                            {
                                MaintainSessions.SessionProfiles = "OC" + ":" + OthersCategoryID;
                            }
                        }
                        if (LocationCategoryID != null)
                        {
                            if (MaintainSessions.SessionProfiles != null && MaintainSessions.SessionProfiles != string.Empty)
                            {
                                MaintainSessions.SessionProfiles += "^LC" + ":" + LocationCategoryID;
                            }
                            else
                            {
                                MaintainSessions.SessionProfiles = "LC" + ":" + LocationCategoryID;
                            }
                        }
                    }

                    if (MaintainSessions.NationalDatabaseCount != counter)
                    {
                        ObjectSearchHospitalsDataForBenchmark = SearchHospitalsDataForBenchmarkTest(hospitalUnitID, firstYear, lastYear, firstMonth, lastMonth, bedInUnitFrom, optBedInUnitFrom, bedInUnitTo, optBedInUnitTo, budgetedPatientFrom, optBudgetedPatientFrom, budgetedPatientTo, optBudgetedPatientTo, startDate, endDate, electronicDocumentFrom, optElectronicDocumentFrom, electronicDocumentTo, optElectronicDocumentTo, docByException, unitType, pharmacyType, hospitalType, optHospitalSizeFrom, hospitalSizeFrom, optHospitalSizeTo, hospitalSizeTo, countryId, stateId, configName, unitIds );
                        MaintainSessions.NationalDatabaseCount = counter;
                    }
                    else
                    {
                        ObjectSearchHospitalsDataForBenchmark = MaintainSessions.SessionSearchHospitalData;
                    }

                    if (ProfileCategoryValue != "Database Values" && ProfileCategoryValue != "Special Category")
                    {
                    //}

                    //valueAddedCategoryID
                    if (valueAddedCategoryID != null)
                    {
                        if (valueAddedCategoryID.Contains(","))
                        {
                            string[] strArrvalue = null;
                            strArrvalue = value.Split(new char[] { ',' });
                            string[] strArrvalueAddedCategoryID = null;
                            strArrvalueAddedCategoryID = valueAddedCategoryID.Split(new char[] { ',' });
                            int[] intArrvalueAddedCategoryID = new int[strArrvalueAddedCategoryID.Length];
                            for (int i = 0; i < strArrvalueAddedCategoryID.Length; i++)
                            {
                                intArrvalueAddedCategoryID[i] = int.Parse(strArrvalueAddedCategoryID[i]);
                                objectGenericListBEReportValueAddedProfile = CalculationOnDataByProfileTest(strArrvalue[i], "value added", GetCategoryProfileDataByProfileID(intArrvalueAddedCategoryID[i]), ObjectSearchHospitalsDataForBenchmark, dataPointsFrom, optDataPointsFrom, dataPointsTo, optdataPointsTo);
                                if (i == 0)
                                {
                                    objectGenericListBEReportValueAdded = objectGenericListBEReportValueAddedProfile;
                                }
                                if (i != 0)
                                {
                                    objectGenericListBEReportValueAdded.AddRange(objectGenericListBEReportValueAddedProfile);
                                }
                            }
                        }
                        else
                        {
                            objectGenericListBEReportValueAdded = CalculationOnDataByProfileTest(value, "value added", GetCategoryProfileDataByProfileID(Convert.ToInt32(valueAddedCategoryID)), ObjectSearchHospitalsDataForBenchmark, dataPointsFrom, optDataPointsFrom, dataPointsTo, optdataPointsTo);
                        }

                    }

                    //OthersCategoryID
                    if (OthersCategoryID != null)
                    {
                        if (OthersCategoryID.Contains(","))
                        {
                            string[] strArrothers = null;
                            strArrothers = others.Split(new char[] { ',' });
                            string[] strArrOthersCategoryID = null;
                            strArrOthersCategoryID = OthersCategoryID.Split(new char[] { ',' });
                            int[] intArrOthersCategoryID = new int[strArrOthersCategoryID.Length];
                            for (int i = 0; i < strArrOthersCategoryID.Length; i++)
                            {
                                intArrOthersCategoryID[i] = int.Parse(strArrOthersCategoryID[i]);
                                objectGenericListBEReportOthers = CalculationOnDataByProfileTest(strArrothers[i], "others", GetCategoryProfileDataByProfileID(intArrOthersCategoryID[i]), ObjectSearchHospitalsDataForBenchmark, dataPointsFrom, optDataPointsFrom, dataPointsTo, optdataPointsTo);
                                if (objectGenericListBEReportValueAdded == null)
                                {
                                    objectGenericListBEReportValueAdded = objectGenericListBEReportOthers;
                                }
                                else
                                {
                                    objectGenericListBEReportValueAdded.AddRange(objectGenericListBEReportOthers);
                                }
                            }
                        }
                        else
                        {
                            objectGenericListBEReportOthers = CalculationOnDataByProfileTest(others, "others", GetCategoryProfileDataByProfileID(Convert.ToInt32(OthersCategoryID)), ObjectSearchHospitalsDataForBenchmark, dataPointsFrom, optDataPointsFrom, dataPointsTo, optdataPointsTo);
                            if (objectGenericListBEReportValueAdded == null)
                            {
                                objectGenericListBEReportValueAdded = objectGenericListBEReportOthers;
                            }
                            else
                            {
                                objectGenericListBEReportValueAdded.AddRange(objectGenericListBEReportOthers);
                            }
                        }

                    }
                    //LocationCategoryID;
                    //if (LocationCategoryID != null)
                    if (ProfileCategoryValue == "Location")
                    {
                        if (LocationCategoryID.Contains(","))
                        {
                            string[] strArrLocation = null;
                            strArrLocation = location.Split(new char[] { ',' });
                            string[] strArrLocationCategoryID = null;
                            strArrLocationCategoryID = LocationCategoryID.Split(new char[] { ',' });
                            int[] intArrLocationCategoryID = new int[strArrLocationCategoryID.Length];
                            for (int i = 0; i < strArrLocationCategoryID.Length; i++)
                            {
                                intArrLocationCategoryID[i] = int.Parse(strArrLocationCategoryID[i]);
                                objectGenericListBEReportLocation = CalculationOnDataByProfileTest(strArrLocation[i], "location", GetCategoryProfileDataByProfileID(intArrLocationCategoryID[i]), ObjectSearchHospitalsDataForBenchmark, dataPointsFrom, optDataPointsFrom, dataPointsTo, optdataPointsTo);
                                if (objectGenericListBEReportValueAdded == null)
                                {
                                    objectGenericListBEReportValueAdded = objectGenericListBEReportLocation;
                                }
                                else
                                {
                                    objectGenericListBEReportValueAdded.AddRange(objectGenericListBEReportLocation);
                                }
                            }
                        }
                        else
                        {
                            objectGenericListBEReportLocation = CalculationOnDataByProfileTest(location, "location", GetCategoryProfileDataByProfileID(Convert.ToInt32(LocationCategoryID)), ObjectSearchHospitalsDataForBenchmark, dataPointsFrom, optDataPointsFrom, dataPointsTo, optdataPointsTo);
                            if (objectGenericListBEReportValueAdded == null)
                            {
                                objectGenericListBEReportValueAdded = objectGenericListBEReportLocation;
                            }
                            else
                            {
                                objectGenericListBEReportValueAdded.AddRange(objectGenericListBEReportLocation);
                            }
                        }

                    }

                    //Activity category by cm
                    if (ProfileCategoryValue == "Activities")
                    {
                        if (LocationCategoryID.Contains(","))
                        {
                            string[] strArrLocation = null;
                            strArrLocation = activities.Split(new char[] { ',' });
                            string[] strArrLocationCategoryID = null;
                            strArrLocationCategoryID = LocationCategoryID.Split(new char[] { ',' });
                            int[] intArrLocationCategoryID = new int[strArrLocationCategoryID.Length];
                            for (int i = 0; i < strArrLocationCategoryID.Length; i++)
                            {
                                intArrLocationCategoryID[i] = int.Parse(strArrLocationCategoryID[i]);
                                objectGenericListBEReportActivity = CalculationOnDataByProfileTest(strArrLocation[i], "activities", GetCategoryProfileDataByProfileID(intArrLocationCategoryID[i]), ObjectSearchHospitalsDataForBenchmark, dataPointsFrom, optDataPointsFrom, dataPointsTo, optdataPointsTo);
                                if (objectGenericListBEReportValueAdded == null)
                                {
                                    objectGenericListBEReportValueAdded = objectGenericListBEReportActivity;
                                }
                                else
                                {
                                    objectGenericListBEReportValueAdded.AddRange(objectGenericListBEReportActivity);
                                }
                            }
                        }
                        else
                        {
                            objectGenericListBEReportActivity = CalculationOnDataByProfileTest(activities, "activities", GetCategoryProfileDataByProfileID(Convert.ToInt32(LocationCategoryID)), ObjectSearchHospitalsDataForBenchmark, dataPointsFrom, optDataPointsFrom, dataPointsTo, optdataPointsTo);
                            if (objectGenericListBEReportValueAdded == null)
                            {
                                objectGenericListBEReportValueAdded = objectGenericListBEReportActivity;
                            }
                            else
                            {
                                objectGenericListBEReportValueAdded.AddRange(objectGenericListBEReportActivity);
                            }
                        }

                    }
                    //end



                    objectNewGenericBEReports = objectGenericListBEReportValueAdded;
                    //Added By Davinder Kumar for purpose of code optimization.
                    MaintainSessions.SessionHospitalBenchmarkSummary = objectGenericListBEReportValueAdded;
                    foreach (var r in objectNewGenericBEReports.GroupBy(o => o.ColumnName))
                    {
                        //Calculate Minimum value.
                        RMC.BusinessEntities.BEFunctionNames objectNewBEFunctionNamesMin = new RMC.BusinessEntities.BEFunctionNames();
                        double valueMin = 0;
                        valueMin = r.Select(s => Convert.ToDouble((s.Values.Length > 0) ? s.Values.Substring(0, s.Values.Length - 1) : "0")).Min();

                        objectNewBEFunctionNamesMin.ColumnName = r.FirstOrDefault().ColumnName;
                        objectNewBEFunctionNamesMin.FunctionName = "Minimum";
                        objectNewBEFunctionNamesMin.FunctionNameDouble = valueMin;
                        objectNewBEFunctionNamesMin.FunctionValueText = string.Format("{0:#.##}%", (valueMin == 0) ? "0.00" : valueMin.ToString());

                        objectGenericBEFunctionNames.Add(objectNewBEFunctionNamesMin);

                        //Calculate Maximum value
                        RMC.BusinessEntities.BEFunctionNames objectNewBEFunctionNamesMax = new RMC.BusinessEntities.BEFunctionNames();
                        double valueMax = 0;
                        valueMax = r.Select(s => Convert.ToDouble((s.Values.Length > 0) ? s.Values.Substring(0, s.Values.Length - 1) : "0")).Max();

                        objectNewBEFunctionNamesMax.ColumnName = r.FirstOrDefault().ColumnName;
                        objectNewBEFunctionNamesMax.FunctionName = "Maximum";
                        objectNewBEFunctionNamesMax.FunctionNameDouble = valueMax;
                        objectNewBEFunctionNamesMax.FunctionValueText = string.Format("{0:#.##}%", (valueMax == 0) ? "0.00" : valueMax.ToString());

                        //Calculate Average value
                        RMC.BusinessEntities.BEFunctionNames objectNewBEFunctionNamesAvg = new RMC.BusinessEntities.BEFunctionNames();
                        List<double> valueSum = null;
                        valueSum = r.Select(s => Convert.ToDouble((s.Values.Length > 0) ? s.Values.Substring(0, s.Values.Length - 1) : "0")).ToList();

                        objectNewBEFunctionNamesAvg.ColumnName = r.FirstOrDefault().ColumnName;
                        objectNewBEFunctionNamesAvg.FunctionName = "Average";
                        objectNewBEFunctionNamesAvg.FunctionNameDouble = (valueSum.Sum() / r.Select(s => s.RowName).Distinct().Count());
                        objectNewBEFunctionNamesAvg.FunctionValueText = string.Format("{0:#.##}%", (objectNewBEFunctionNamesAvg.FunctionNameDouble == 0) ? "0.00" : string.Format("{0:#.##}", objectNewBEFunctionNamesAvg.FunctionNameDouble));

                        //Median Value.
                        RMC.BusinessEntities.BEFunctionNames objectNewBEFunctionNamesMed = new RMC.BusinessEntities.BEFunctionNames();
                        int median = 0;
                        int medianEven = 0;
                        int count = r.Select(s => s.RowName).Distinct().Count();
                        if (count % 2 == 0)
                        {
                            median = count / 2;
                            medianEven = median + 1;

                            objectNewBEFunctionNamesMed.ColumnName = r.FirstOrDefault().ColumnName;
                            objectNewBEFunctionNamesMed.FunctionName = "Median";
                            objectNewBEFunctionNamesMed.FunctionNameDouble = (r.OrderBy(x => x.ValuesSum).Select(s => s.ValuesSum).ToList().ElementAt(median - 1) + r.OrderBy(x => x.ValuesSum).Select(s => s.ValuesSum).ToList().ElementAt(medianEven - 1)) / 2;
                            objectNewBEFunctionNamesMed.FunctionValueText = string.Format("{0:#.##}%", string.Format("{0:#.##}", (objectNewBEFunctionNamesMed.FunctionNameDouble == 0.00) ? "0.00" : string.Format("{0:#.##}", objectNewBEFunctionNamesMed.FunctionNameDouble)));
                        }
                        else
                        {
                            median = (count + 1) / 2;

                            objectNewBEFunctionNamesMed.ColumnName = r.FirstOrDefault().ColumnName;
                            objectNewBEFunctionNamesMed.FunctionName = "Median";
                            objectNewBEFunctionNamesMed.FunctionNameDouble = r.OrderBy(x => x.ValuesSum).Select(s => s.ValuesSum).ToList().ElementAt(median - 1);
                            objectNewBEFunctionNamesMed.FunctionValueText = string.Format("{0:#.##}%", string.Format("{0:#.##}", (objectNewBEFunctionNamesMed.FunctionNameDouble == 0.00) ? "0.00" : string.Format("{0:#.##}", objectNewBEFunctionNamesMed.FunctionNameDouble)));
                        }
                        //median--;


                        //Quartile
                        RMC.BusinessEntities.BEFunctionNames objectNewBEFunctionNamesQuartile1 = new RMC.BusinessEntities.BEFunctionNames();

                        objectNewBEFunctionNamesQuartile1.ColumnName = r.FirstOrDefault().ColumnName;
                        objectNewBEFunctionNamesQuartile1.FunctionName = "Quartile(1)";
                        objectNewBEFunctionNamesQuartile1.FunctionNameDouble = CalculateQuartile(count, 1, r.Select(s => s.ValuesSum).ToList());
                        objectNewBEFunctionNamesQuartile1.FunctionValueText = string.Format("{0:#.##}%", (objectNewBEFunctionNamesQuartile1.FunctionNameDouble == 0.00) ? "0.00" : string.Format("{0:#.##}", objectNewBEFunctionNamesQuartile1.FunctionNameDouble));

                        RMC.BusinessEntities.BEFunctionNames objectNewBEFunctionNamesQuartile3 = new RMC.BusinessEntities.BEFunctionNames();

                        objectNewBEFunctionNamesQuartile3.ColumnName = r.FirstOrDefault().ColumnName;
                        objectNewBEFunctionNamesQuartile3.FunctionName = "Quartile(3)";
                        objectNewBEFunctionNamesQuartile3.FunctionNameDouble = CalculateQuartile(count, 3, r.Select(s => s.ValuesSum).ToList());
                        objectNewBEFunctionNamesQuartile3.FunctionValueText = string.Format("{0:#.##}%", (objectNewBEFunctionNamesQuartile3.FunctionNameDouble == 0.00) ? "0.00" : string.Format("{0:#.##}", objectNewBEFunctionNamesQuartile3.FunctionNameDouble));

                        objectGenericBEFunctionNames.Add(objectNewBEFunctionNamesQuartile1);
                        objectGenericBEFunctionNames.Add(objectNewBEFunctionNamesMed);
                        objectGenericBEFunctionNames.Add(objectNewBEFunctionNamesAvg);
                        objectGenericBEFunctionNames.Add(objectNewBEFunctionNamesQuartile3);
                        objectGenericBEFunctionNames.Add(objectNewBEFunctionNamesMax);
                    }
                    //objectGenericBEFunctionNames.OrderBy(o => o.ColumnName).ToList();
                    //if (HttpContext.Current.Session["SearchHospitalsDataForBenchmark"] != null)
                    //{
                    //    HttpContext.Current.Session.Remove("SearchHospitalsDataForBenchmark");
                    //}
                    MaintainSessions.SessionFunctionValues = objectGenericBEFunctionNames;
                    return objectGenericBEFunctionNames;
                }
                //======================For Special Category Type
                else if (ProfileCategoryValue == "Special Category")
                {
                    List<RMC.BusinessEntities.BEReports> objectGenericListResult = null;
                    List<RMC.BusinessEntities.BEValidation> objectGenericBEValidation = SearchHospitalsDataForSpecialCategory(hospitalUnitID, firstYear, lastYear, firstMonth, lastMonth, null, 0, null, 0, null, 0, null, 0, startDate, endDate, null, 0, null, 0, docByException, unitType, pharmacyType, null, 0, null, 0, null, null, null, null);
                    List<RMC.BusinessEntities.BEValidation> objectGenericBEValidationAll = SearchHospitalsDataForSpecialCategory(null, firstYear, lastYear, firstMonth, lastMonth, null, 0, null, 0, null, 0, null, 0, startDate, endDate, null, 0, null, 0, docByException, unitType, pharmacyType, null, 0, null, 0, null, null, null, null);
                    List<RMC.DataService.NursePDASpecialType> objectGenericNursePDASpecialType = _objectRMCDataContext.NursePDASpecialTypes.ToList();
                    //List<RMC.BusinessEntities.BEReports> objectNewGenericBEReports = null;
                    //List<RMC.BusinessEntities.BEFunctionNames> objectGenericBEFunctionNames = new List<RMC.BusinessEntities.BEFunctionNames>();

                    objectGenericListResult = (from v in objectGenericBEValidation.OrderBy(o => o.Year).ThenBy(p => p.MonthIndex)
                                               where v.SpecialCategory == ProfileSubCategoryValue
                                               group v by new { v.Year, v.MonthIndex } into g
                                               from t in g.GroupBy(x => x.SpecialActivity).ToList()
                                               select new RMC.BusinessEntities.BEReports
                                               {
                                                   ColumnName = objectGenericNursePDASpecialType.Find(delegate(RMC.DataService.NursePDASpecialType objectNursePDASpecialType)
                                                   {
                                                       return objectNursePDASpecialType.SpecialActivity == t.Key;
                                                   }).SpecialActivity,
                                                   ColumnNumber = 1,
                                                   MonthName = BSCommon.GetMonthName(t.FirstOrDefault(r => r.Month != "").Month) + " " + t.FirstOrDefault().Year,
                                                   RowName = t.FirstOrDefault(r => r.HospitalUnitName != "").HospitalUnitName,
                                                   Values = string.Format("{0:#.##}%", ((double)t.Count(r => r.SpecialActivity == t.Key) / (double)g.Count(c => c.SpecialActivity != "")) * 100),
                                                   ValuesSum = Convert.ToDouble(string.Format("{0:#.##}", ((double)t.Count(r => r.SpecialActivity == t.Key) / (double)g.Count(c => c.SpecialActivity != "")) * 100)),
                                                   //DataPoint = (double)g.Count(c => c.SpecialActivity != "")
                                               }).ToList();

                    objectGenericListResult = objectGenericListResult.FindAll(delegate(RMC.BusinessEntities.BEReports objectBEReport)
                    {
                        //return objectBEReport.ColumnName.ToLower().Trim() == dbValues.ToLower().Trim();
                        return true;
                    });

                    objectGenericBEValidationAll = objectGenericBEValidationAll.FindAll(delegate(RMC.BusinessEntities.BEValidation objectNewBEValidation)
                    {
                        if (objectNewBEValidation != null)
                        {
                            if (objectNewBEValidation.SpecialCategory != null)
                            {
                                return objectNewBEValidation.SpecialCategory.ToLower().Trim() == ProfileSubCategoryValue.ToLower().Trim();
                            }
                            else { return false; }
                        }
                        else { return false; }
                    });

                    objectNewGenericBEReports = (from v in objectGenericBEValidationAll.GroupBy(o => o.HospitalUnitID).ToList()
                                                 from t in v.GroupBy(x => x.SpecialActivity).ToList()
                                                 select new RMC.BusinessEntities.BEReports
                                                 {
                                                     Name = "Special Category",
                                                     ColumnName = objectGenericNursePDASpecialType.Find(delegate(RMC.DataService.NursePDASpecialType objectNursePDASpecialType)
                                                     {
                                                         return objectNursePDASpecialType.SpecialActivity == t.Key;
                                                     }).SpecialActivity,
                                                     //ColumnName = "Deliver FoodTray",
                                                     ColumnNumber = 1,
                                                     MonthName = BSCommon.GetMonthName(t.FirstOrDefault(r => r.Month != "").Month) + " " + t.FirstOrDefault().Year,
                                                     RowName = t.FirstOrDefault(r => r.HospitalUnitName != "").HospitalUnitName,
                                                     Values = string.Format("{0:#.##}%", ((double)t.Count(r => r.SpecialActivity == t.Key) / (double)v.Count(c => c.SpecialActivity != "")) * 100),
                                                     ValuesSum = Convert.ToDouble(string.Format("{0:#.##}", ((double)t.Count(r => r.SpecialActivity == t.Key) / (double)v.Count(c => c.SpecialActivity != "")) * 100)),
                                                     DataPoint = (double)v.Count(c => c.SpecialActivity != ""),
                                                 }).ToList();


                    objectNewGenericBEReports = objectNewGenericBEReports.FindAll(delegate(RMC.BusinessEntities.BEReports objectBEReport)
                    {
                        //return objectBEReport.ColumnName.ToLower().Trim() == dbValues.ToLower().Trim();
                        return true;
                    });

                    if (objectNewGenericBEReports != null)
                    {
                        //filteration according to DataPoints
                        if (dataPointsFrom != null && dataPointsTo == null)
                        {
                            if (optDataPointsFrom == 1)
                            {
                                objectNewGenericBEReports = objectNewGenericBEReports.Where(x => Convert.ToInt32(x.DataPoint) < dataPointsFrom).ToList();
                            }
                            if (optDataPointsFrom == 2)
                            {
                                objectNewGenericBEReports = objectNewGenericBEReports.Where(x => x.DataPoint > dataPointsFrom).ToList();
                            }
                            if (optDataPointsFrom == 3)
                            {
                                objectNewGenericBEReports = objectNewGenericBEReports.Where(x => x.DataPoint == dataPointsFrom).ToList();
                            }
                            if (optDataPointsFrom == 4)
                            {
                                objectNewGenericBEReports = objectNewGenericBEReports.Where(x => x.DataPoint >= dataPointsFrom).ToList();
                            }
                            if (optDataPointsFrom == 5)
                            {
                                objectNewGenericBEReports = objectNewGenericBEReports.Where(x => x.DataPoint <= dataPointsFrom).ToList();
                            }
                            if (optDataPointsFrom == 6)
                            {
                                objectNewGenericBEReports = objectNewGenericBEReports.Where(x => x.DataPoint != dataPointsFrom).ToList();
                            }
                        }

                        if ((optDataPointsFrom == 0 && optdataPointsTo == 0) && (dataPointsFrom != null && dataPointsTo != null))
                        {
                            objectNewGenericBEReports = objectNewGenericBEReports.Where(x => x.DataPoint >= dataPointsFrom && x.DataPoint <= dataPointsTo).ToList();
                        }

                        foreach (var r in objectNewGenericBEReports.GroupBy(o => o.ColumnName))
                        {
                            //Calculate Minimum value.
                            RMC.BusinessEntities.BEFunctionNames objectNewBEFunctionNamesMin = new RMC.BusinessEntities.BEFunctionNames();
                            double valueMin = 0;
                            valueMin = r.Select(s => Convert.ToDouble((s.Values.Length > 0) ? s.Values.Substring(0, s.Values.Length - 1) : "0")).Min();

                            objectNewBEFunctionNamesMin.ColumnName = r.FirstOrDefault().ColumnName;
                            objectNewBEFunctionNamesMin.FunctionName = "Minimum";
                            objectNewBEFunctionNamesMin.FunctionNameDouble = valueMin;
                            objectNewBEFunctionNamesMin.FunctionValueText = string.Format("{0:#.##}%", (valueMin == 0) ? "0.00" : valueMin.ToString());

                            objectGenericBEFunctionNames.Add(objectNewBEFunctionNamesMin);

                            //Calculate Maximum value
                            RMC.BusinessEntities.BEFunctionNames objectNewBEFunctionNamesMax = new RMC.BusinessEntities.BEFunctionNames();
                            double valueMax = 0;
                            valueMax = r.Select(s => Convert.ToDouble((s.Values.Length > 0) ? s.Values.Substring(0, s.Values.Length - 1) : "0")).Max();

                            objectNewBEFunctionNamesMax.ColumnName = r.FirstOrDefault().ColumnName;
                            objectNewBEFunctionNamesMax.FunctionName = "Maximum";
                            objectNewBEFunctionNamesMax.FunctionNameDouble = valueMax;
                            objectNewBEFunctionNamesMax.FunctionValueText = string.Format("{0:#.##}%", (valueMax == 0) ? "0.00" : valueMax.ToString());

                            //Calculate Average value
                            RMC.BusinessEntities.BEFunctionNames objectNewBEFunctionNamesAvg = new RMC.BusinessEntities.BEFunctionNames();
                            List<double> valueSum = null;
                            valueSum = r.Select(s => Convert.ToDouble((s.Values.Length > 0) ? s.Values.Substring(0, s.Values.Length - 1) : "0")).ToList();

                            objectNewBEFunctionNamesAvg.ColumnName = r.FirstOrDefault().ColumnName;
                            objectNewBEFunctionNamesAvg.FunctionName = "Average";
                            objectNewBEFunctionNamesAvg.FunctionNameDouble = (valueSum.Sum() / r.Select(s => s.RowName).Distinct().Count());
                            objectNewBEFunctionNamesAvg.FunctionValueText = string.Format("{0:#.##}%", (objectNewBEFunctionNamesAvg.FunctionNameDouble == 0) ? "0.00" : string.Format("{0:#.##}", objectNewBEFunctionNamesAvg.FunctionNameDouble));

                            //Median Value.
                            RMC.BusinessEntities.BEFunctionNames objectNewBEFunctionNamesMed = new RMC.BusinessEntities.BEFunctionNames();
                            int median = 0;
                            int medianEven = 0;
                            int count = r.Select(s => s.RowName).Distinct().Count();
                            if (count % 2 == 0)
                            {
                                median = count / 2;
                                medianEven = median + 1;

                                objectNewBEFunctionNamesMed.ColumnName = r.FirstOrDefault().ColumnName;
                                objectNewBEFunctionNamesMed.FunctionName = "Median";
                                objectNewBEFunctionNamesMed.FunctionNameDouble = (r.OrderBy(x => x.ValuesSum).Select(s => s.ValuesSum).ToList().ElementAt(median - 1) + r.OrderBy(x => x.ValuesSum).Select(s => s.ValuesSum).ToList().ElementAt(medianEven - 1)) / 2;
                                objectNewBEFunctionNamesMed.FunctionValueText = string.Format("{0:#.##}%", string.Format("{0:#.##}", (objectNewBEFunctionNamesMed.FunctionNameDouble == 0.00) ? "0.00" : string.Format("{0:#.##}", objectNewBEFunctionNamesMed.FunctionNameDouble)));
                            }
                            else
                            {
                                median = (count + 1) / 2;

                                objectNewBEFunctionNamesMed.ColumnName = r.FirstOrDefault().ColumnName;
                                objectNewBEFunctionNamesMed.FunctionName = "Median";
                                objectNewBEFunctionNamesMed.FunctionNameDouble = r.OrderBy(x => x.ValuesSum).Select(s => s.ValuesSum).ToList().ElementAt(median - 1);
                                objectNewBEFunctionNamesMed.FunctionValueText = string.Format("{0:#.##}%", string.Format("{0:#.##}", (objectNewBEFunctionNamesMed.FunctionNameDouble == 0.00) ? "0.00" : string.Format("{0:#.##}", objectNewBEFunctionNamesMed.FunctionNameDouble)));
                            }
                            //median--;

                            //Quartile
                            RMC.BusinessEntities.BEFunctionNames objectNewBEFunctionNamesQuartile1 = new RMC.BusinessEntities.BEFunctionNames();

                            objectNewBEFunctionNamesQuartile1.ColumnName = r.FirstOrDefault().ColumnName;
                            objectNewBEFunctionNamesQuartile1.FunctionName = "Quartile(1)";
                            objectNewBEFunctionNamesQuartile1.FunctionNameDouble = CalculateQuartile(count, 1, r.Select(s => s.ValuesSum).ToList());
                            objectNewBEFunctionNamesQuartile1.FunctionValueText = string.Format("{0:#.##}%", (objectNewBEFunctionNamesQuartile1.FunctionNameDouble == 0.00) ? "0.00" : string.Format("{0:#.##}", objectNewBEFunctionNamesQuartile1.FunctionNameDouble));

                            RMC.BusinessEntities.BEFunctionNames objectNewBEFunctionNamesQuartile3 = new RMC.BusinessEntities.BEFunctionNames();

                            objectNewBEFunctionNamesQuartile3.ColumnName = r.FirstOrDefault().ColumnName;
                            objectNewBEFunctionNamesQuartile3.FunctionName = "Quartile(3)";
                            objectNewBEFunctionNamesQuartile3.FunctionNameDouble = CalculateQuartile(count, 3, r.Select(s => s.ValuesSum).ToList());
                            objectNewBEFunctionNamesQuartile3.FunctionValueText = string.Format("{0:#.##}%", (objectNewBEFunctionNamesQuartile3.FunctionNameDouble == 0.00) ? "0.00" : string.Format("{0:#.##}", objectNewBEFunctionNamesQuartile3.FunctionNameDouble));

                            objectGenericBEFunctionNames.Add(objectNewBEFunctionNamesQuartile1);
                            objectGenericBEFunctionNames.Add(objectNewBEFunctionNamesMed);
                            objectGenericBEFunctionNames.Add(objectNewBEFunctionNamesAvg);
                            objectGenericBEFunctionNames.Add(objectNewBEFunctionNamesQuartile3);
                            objectGenericBEFunctionNames.Add(objectNewBEFunctionNamesMax);
                        }

                        int countt = objectGenericListResult.Count;
                        //List<RMC.BusinessEntities.BEReports> objectGenericListResultTemp = new List<RMC.BusinessEntities.BEReports>();
                        //objectGenericListResultTemp = objectGenericListResult;
                        objectGenericBEFunctionNames.ForEach(delegate(RMC.BusinessEntities.BEFunctionNames objectBEFunctionNames)
                        {
                            for (int index = 0; index < countt; index++)
                            {
                                RMC.BusinessEntities.BEReports objectBEReports = new RMC.BusinessEntities.BEReports();
                                //RMC.BusinessEntities.BEFunctionNames  objectBEReports = new RMC.BusinessEntities.BEFunctionNames();
                                objectBEReports.ColumnName = objectBEFunctionNames.FunctionName;
                                //objectBEReports.MonthName = BSCommon.GetMonthName(Convert.ToString(index + 1));
                                objectBEReports.MonthName = objectGenericListResult[index].MonthName;
                                objectBEReports.Values = objectBEFunctionNames.FunctionValueText;
                                objectBEReports.ValuesSum = objectBEFunctionNames.FunctionNameDouble;

                                objectGenericListResult.Add(objectBEReports);
                                
                                //objectGenericBEFunctionNames.Add(objectBEFunctionNames);
                            }
                        });
                        
                    } 
                } 

                //cm
                    //======================

                    else if (ProfileCategoryValue == "Database Values")
                    {
                        List<RMC.BusinessEntities.BEReports> objectGenericListResult = null;
                        List<RMC.BusinessEntities.BEValidation> objectGenericBEValidation = SearchHospitalsDataForBenchmarkTest(hospitalUnitID, firstYear, lastYear, firstMonth, lastMonth, null, 0, null, 0, null, 0, null, 0, startDate, endDate, null, 0, null, 0, docByException, unitType, pharmacyType, null, 0, null, 0, null, null, null, configName, unitIds);
                        //Here hospitalId is null to compare with single hospital data
                        List<RMC.BusinessEntities.BEValidation> objectGenericBEValidationAll = SearchHospitalsDataForBenchmarkTest(null, firstYear, lastYear, firstMonth, lastMonth, null, 0, null, 0, null, 0, null, 0, startDate, endDate, null, 0, null, 0, docByException, unitType, pharmacyType, null, 0, null, 0, null, null, null, configName, unitIds);
                        //List<RMC.BusinessEntities.BEReports> objectNewGenericBEReports = null;
                        //List<RMC.BusinessEntities.BEFunctionNames> objectGenericBEFunctionNames = new List<RMC.BusinessEntities.BEFunctionNames>();

                        if (ProfileSubCategoryValue == "Activity")
                        {
                            List<RMC.DataService.Activity> objectGenericActivity = _objectRMCDataContext.Activities.ToList();

                            objectGenericBEValidation = objectGenericBEValidation.FindAll(delegate(RMC.BusinessEntities.BEValidation objectNewBEValidation)
                            {
                                return objectGenericActivity.Exists(delegate(RMC.DataService.Activity objectActivity)
                                {
                                    return objectActivity.ActivityID == objectNewBEValidation.ActivityID;
                                });
                            });

                            objectGenericListResult = (from v in objectGenericBEValidation.OrderBy(o => o.Year).ThenBy(p => p.MonthIndex)
                                                       group v by new { v.Year, v.MonthIndex } into g
                                                       from t in g.GroupBy(x => x.ActivityID).ToList()
                                                       select new RMC.BusinessEntities.BEReports
                                                       {
                                                           Name = "Activity",
                                                           ColumnName = objectGenericActivity.Find(delegate(RMC.DataService.Activity objectActivity)
                                                           {
                                                               return objectActivity.ActivityID == t.Key;
                                                           }).Activity1,
                                                           //ColumnName = "Deliver FoodTray",
                                                           ColumnNumber = objectGenericActivity.Find(delegate(RMC.DataService.Activity objectActivity)
                                                           {
                                                               return objectActivity.ActivityID == t.Key;
                                                           }).ActivityID,
                                                           MonthName = BSCommon.GetMonthName(t.FirstOrDefault(r => r.Month != "").Month) + " " + t.FirstOrDefault().Year,
                                                           RowName = t.FirstOrDefault(r => r.HospitalUnitName != "").HospitalUnitName,
                                                           Values = string.Format("{0:#.##}%", ((double)t.Count(r => r.ActivityID == t.Key) / (double)g.Count(c => c.ActivityID != 0)) * 100),
                                                           ValuesSum = Convert.ToDouble(string.Format("{0:#.##}", ((double)t.Count(r => r.ActivityID == t.Key) / (double)g.Count(c => c.ActivityID != 0)) * 100))
                                                       }).ToList();

                            objectGenericListResult = objectGenericListResult.FindAll(delegate(RMC.BusinessEntities.BEReports objectBEReport)
                            {
                                //return objectBEReport.ColumnName.ToLower().Trim() == dbValues.ToLower().Trim();
                                return true;
                            });

                            //for comparing with National Database means calculating function values (min, max, quartile etc)

                            objectGenericBEValidationAll = objectGenericBEValidationAll.FindAll(delegate(RMC.BusinessEntities.BEValidation objectNewBEValidation)
                            {
                                return objectGenericActivity.Exists(delegate(RMC.DataService.Activity objectActivity)
                                {
                                    return objectActivity.ActivityID == objectNewBEValidation.ActivityID;
                                });
                            });

                            objectNewGenericBEReports = (from v in objectGenericBEValidationAll.GroupBy(o => o.HospitalUnitIDCounter).ToList()
                                                         from t in v.GroupBy(x => x.ActivityID).ToList()
                                                         select new RMC.BusinessEntities.BEReports
                                                         {
                                                             Name = "Activity",
                                                             ColumnName = objectGenericActivity.Find(delegate(RMC.DataService.Activity objectActivity)
                                                             {
                                                                 return objectActivity.ActivityID == t.Key;
                                                             }).Activity1,
                                                             //ColumnName = "Deliver FoodTray",
                                                             ColumnNumber = objectGenericActivity.Find(delegate(RMC.DataService.Activity objectActivity)
                                                             {
                                                                 return objectActivity.ActivityID == t.Key;
                                                             }).ActivityID,
                                                             MonthName = BSCommon.GetMonthName(t.FirstOrDefault(r => r.Month != "").Month) + " " + t.FirstOrDefault().Year,
                                                             RowName = t.FirstOrDefault(r => r.HospitalUnitName != "").HospitalUnitName,
                                                             Values = string.Format("{0:#.##}%", ((double)t.Count(r => r.ActivityID == t.Key) / (double)v.Count(c => c.ActivityID != 0)) * 100),
                                                             ValuesSum = Convert.ToDouble(string.Format("{0:#.##}", ((double)t.Count(r => r.ActivityID == t.Key) / (double)v.Count(c => c.ActivityID != 0)) * 100)),
                                                             DataPoint = (double)v.Count(c => c.ActivityID != 0),
                                                         }).ToList();


                            objectNewGenericBEReports = objectNewGenericBEReports.FindAll(delegate(RMC.BusinessEntities.BEReports objectBEReport)
                            {
                                //return objectBEReport.ColumnName.ToLower().Trim() == dbValues.ToLower().Trim();
                                return true;
                            });

                        }
                        if (ProfileSubCategoryValue == "Sub-Activity")
                        {
                            List<RMC.DataService.SubActivity> objectGenericSubActivity = _objectRMCDataContext.SubActivities.ToList();

                            objectGenericBEValidation = objectGenericBEValidation.FindAll(delegate(RMC.BusinessEntities.BEValidation objectNewBEValidation)
                            {
                                return objectGenericSubActivity.Exists(delegate(RMC.DataService.SubActivity objectSubActivity)
                                {
                                    return objectSubActivity.SubActivityID == objectNewBEValidation.SubActivityID;
                                });
                            });

                            objectGenericListResult = (from v in objectGenericBEValidation.OrderBy(o => o.Year).ThenBy(p => p.MonthIndex)
                                                       group v by new { v.Year, v.MonthIndex } into g
                                                       from t in g.GroupBy(x => x.SubActivityID).ToList()
                                                       select new RMC.BusinessEntities.BEReports
                                                       {
                                                           Name = "Sub-Activity",
                                                           ColumnName = objectGenericSubActivity.Find(delegate(RMC.DataService.SubActivity objectSubActivity)
                                                           {
                                                               return objectSubActivity.SubActivityID == t.Key;
                                                           }).SubActivity1,
                                                           ColumnNumber = objectGenericSubActivity.Find(delegate(RMC.DataService.SubActivity objectSubActivity)
                                                           {
                                                               return objectSubActivity.SubActivityID == t.Key;
                                                           }).SubActivityID,
                                                           MonthName = BSCommon.GetMonthName(t.FirstOrDefault(r => r.Month != "").Month) + " " + t.FirstOrDefault().Year,
                                                           RowName = t.FirstOrDefault(r => r.HospitalUnitName != "").HospitalUnitName,
                                                           Values = string.Format("{0:#.##}%", ((double)t.Count(r => r.SubActivityID == t.Key) / (double)g.Count(c => c.SubActivityID != 0)) * 100),
                                                           ValuesSum = Convert.ToDouble(string.Format("{0:#.##}", ((double)t.Count(r => r.SubActivityID == t.Key) / (double)g.Count(c => c.SubActivityID != 0)) * 100))
                                                       }).ToList();

                            objectGenericListResult = objectGenericListResult.FindAll(delegate(RMC.BusinessEntities.BEReports objectBEReport)
                            {
                                //return objectBEReport.ColumnName.ToLower().Trim() == dbValues.ToLower().Trim();
                                return true;
                            });

                            //for comparing with National Database means calculating function values (min, max, quartile etc)

                            objectGenericBEValidationAll = objectGenericBEValidationAll.FindAll(delegate(RMC.BusinessEntities.BEValidation objectNewBEValidation)
                            {
                                return objectGenericSubActivity.Exists(delegate(RMC.DataService.SubActivity objectSubActivity)
                                {
                                    return objectSubActivity.SubActivityID == objectNewBEValidation.SubActivityID;
                                });
                            });

                            objectNewGenericBEReports = (from v in objectGenericBEValidationAll.GroupBy(o => o.HospitalUnitIDCounter).ToList()
                                                         from t in v.GroupBy(x => x.SubActivityID).ToList()
                                                         select new RMC.BusinessEntities.BEReports
                                                         {
                                                             Name = "Sub-Activity",
                                                             ColumnName = objectGenericSubActivity.Find(delegate(RMC.DataService.SubActivity objectSubActivity)
                                                             {
                                                                 return objectSubActivity.SubActivityID == t.Key;
                                                             }).SubActivity1,
                                                             ColumnNumber = objectGenericSubActivity.Find(delegate(RMC.DataService.SubActivity objectSubActivity)
                                                             {
                                                                 return objectSubActivity.SubActivityID == t.Key;
                                                             }).SubActivityID,
                                                             MonthName = BSCommon.GetMonthName(t.FirstOrDefault(r => r.Month != "").Month) + " " + t.FirstOrDefault().Year,
                                                             RowName = t.FirstOrDefault(r => r.HospitalUnitName != "").HospitalUnitName,
                                                             Values = string.Format("{0:#.##}%", ((double)t.Count(r => r.SubActivityID == t.Key) / (double)v.Count(c => c.SubActivityID != 0)) * 100),
                                                             ValuesSum = Convert.ToDouble(string.Format("{0:#.##}", ((double)t.Count(r => r.SubActivityID == t.Key) / (double)v.Count(c => c.SubActivityID != 0)) * 100))
                                                         }).ToList();


                            objectNewGenericBEReports = objectNewGenericBEReports.FindAll(delegate(RMC.BusinessEntities.BEReports objectBEReport)
                            {
                                //return objectBEReport.ColumnName.ToLower().Trim() == dbValues.ToLower().Trim();
                                return true;
                            });


                        }
                        if (ProfileSubCategoryValue == "Last Location")
                        {
                            List<RMC.DataService.LastLocation> objectGenericLastLocation = _objectRMCDataContext.LastLocations.ToList();

                            objectGenericBEValidation = objectGenericBEValidation.FindAll(delegate(RMC.BusinessEntities.BEValidation objectNewBEValidation)
                            {
                                return objectGenericLastLocation.Exists(delegate(RMC.DataService.LastLocation objectLastLocation)
                                {
                                    return objectLastLocation.LastLocationID == objectNewBEValidation.LastLocationID;
                                });
                            });

                            objectGenericListResult = (from v in objectGenericBEValidation.OrderBy(o => o.Year).ThenBy(p => p.MonthIndex)
                                                       group v by new { v.Year, v.MonthIndex } into g
                                                       from t in g.GroupBy(x => x.LastLocationID).ToList()
                                                       select new RMC.BusinessEntities.BEReports
                                                       {
                                                           Name = "Last Location",
                                                           ColumnName = objectGenericLastLocation.Find(delegate(RMC.DataService.LastLocation objectLastLocation)
                                                           {
                                                               return objectLastLocation.LastLocationID == t.Key;
                                                           }).LastLocation1,
                                                           ColumnNumber = objectGenericLastLocation.Find(delegate(RMC.DataService.LastLocation objectLastLocation)
                                                           {
                                                               return objectLastLocation.LastLocationID == t.Key;
                                                           }).LastLocationID,
                                                           MonthName = BSCommon.GetMonthName(t.FirstOrDefault(r => r.Month != "").Month) + " " + t.FirstOrDefault().Year,
                                                           RowName = t.FirstOrDefault(r => r.HospitalUnitName != "").HospitalUnitName,
                                                           Values = string.Format("{0:#.##}%", ((double)t.Count(r => r.LastLocationID == t.Key) / (double)g.Count(c => c.LastLocationID != 0)) * 100),
                                                           ValuesSum = Convert.ToDouble(string.Format("{0:#.##}", ((double)t.Count(r => r.LastLocationID == t.Key) / (double)g.Count(c => c.LastLocationID != 0)) * 100))
                                                       }).ToList();

                            objectGenericListResult = objectGenericListResult.FindAll(delegate(RMC.BusinessEntities.BEReports objectBEReport)
                            {
                                //return objectBEReport.ColumnName.ToLower().Trim() == dbValues.ToLower().Trim();
                                return true;
                            });

                            //for comparing with National Database means calculating function values (min, max, quartile etc)

                            objectGenericBEValidationAll = objectGenericBEValidationAll.FindAll(delegate(RMC.BusinessEntities.BEValidation objectNewBEValidation)
                            {
                                return objectGenericLastLocation.Exists(delegate(RMC.DataService.LastLocation objectLastLocation)
                                {
                                    return objectLastLocation.LastLocationID == objectNewBEValidation.LastLocationID;
                                });
                            });

                            objectNewGenericBEReports = (from v in objectGenericBEValidationAll.GroupBy(o => o.HospitalUnitIDCounter).ToList()
                                                         from t in v.GroupBy(x => x.LastLocationID).ToList()
                                                         select new RMC.BusinessEntities.BEReports
                                                         {
                                                             Name = "Last Location",
                                                             ColumnName = objectGenericLastLocation.Find(delegate(RMC.DataService.LastLocation objectLastLocation)
                                                             {
                                                                 return objectLastLocation.LastLocationID == t.Key;
                                                             }).LastLocation1,
                                                             ColumnNumber = objectGenericLastLocation.Find(delegate(RMC.DataService.LastLocation objectLastLocation)
                                                             {
                                                                 return objectLastLocation.LastLocationID == t.Key;
                                                             }).LastLocationID,
                                                             MonthName = BSCommon.GetMonthName(t.FirstOrDefault(r => r.Month != "").Month) + " " + t.FirstOrDefault().Year,
                                                             RowName = t.FirstOrDefault(r => r.HospitalUnitName != "").HospitalUnitName,
                                                             Values = string.Format("{0:#.##}%", ((double)t.Count(r => r.LastLocationID == t.Key) / (double)v.Count(c => c.LastLocationID != 0)) * 100),
                                                             ValuesSum = Convert.ToDouble(string.Format("{0:#.##}", ((double)t.Count(r => r.LastLocationID == t.Key) / (double)v.Count(c => c.LastLocationID != 0)) * 100))
                                                         }).ToList();

                            objectNewGenericBEReports = objectNewGenericBEReports.FindAll(delegate(RMC.BusinessEntities.BEReports objectBEReport)
                            {
                                //return objectBEReport.ColumnName.ToLower().Trim() == dbValues.ToLower().Trim();
                                return true;
                            });

                        }
                        if (ProfileSubCategoryValue == "Current Location")
                        {
                            List<RMC.DataService.Location> objectGenericLocation = _objectRMCDataContext.Locations.ToList();

                            objectGenericBEValidation = objectGenericBEValidation.FindAll(delegate(RMC.BusinessEntities.BEValidation objectNewBEValidation)
                            {
                                return objectGenericLocation.Exists(delegate(RMC.DataService.Location objectLocation)
                                {
                                    return objectLocation.LocationID == objectNewBEValidation.LocationID;
                                });
                            });


                            objectGenericListResult = (from v in objectGenericBEValidation.OrderBy(o => o.Year).ThenBy(p => p.MonthIndex)
                                                       group v by new { v.Year, v.MonthIndex } into g
                                                       from t in g.GroupBy(x => x.LocationID).ToList()
                                                       select new RMC.BusinessEntities.BEReports
                                                       {
                                                           Name = "Current Location",
                                                           ColumnName = objectGenericLocation.Find(delegate(RMC.DataService.Location objectLocation)
                                                           {
                                                               return objectLocation.LocationID == t.Key;
                                                           }).Location1,
                                                           ColumnNumber = objectGenericLocation.Find(delegate(RMC.DataService.Location objectLocation)
                                                           {
                                                               return objectLocation.LocationID == t.Key;
                                                           }).LocationID,
                                                           MonthName = BSCommon.GetMonthName(t.FirstOrDefault(r => r.Month != "").Month) + " " + t.FirstOrDefault().Year,
                                                           RowName = t.FirstOrDefault(r => r.HospitalUnitName != "").HospitalUnitName,
                                                           Values = string.Format("{0:#.##}%", ((double)t.Count(r => r.LocationID == t.Key) / (double)g.Count(c => c.LocationID != 0)) * 100),
                                                           ValuesSum = Convert.ToDouble(string.Format("{0:#.##}", ((double)t.Count(r => r.LocationID == t.Key) / (double)g.Count(c => c.LocationID != 0)) * 100))
                                                       }).ToList();


                            objectGenericListResult = objectGenericListResult.FindAll(delegate(RMC.BusinessEntities.BEReports objectBEReport)
                            {
                                //return objectBEReport.ColumnName.ToLower().Trim() == dbValues.ToLower().Trim();
                                return true;
                            });

                            //for comparing with National Database means calculating function values (min, max, quartile etc)

                            objectGenericBEValidationAll = objectGenericBEValidationAll.FindAll(delegate(RMC.BusinessEntities.BEValidation objectNewBEValidation)
                            {
                                return objectGenericLocation.Exists(delegate(RMC.DataService.Location objectLocation)
                                {
                                    return objectLocation.LocationID == objectNewBEValidation.LocationID;
                                });
                            });

                            objectNewGenericBEReports = (from v in objectGenericBEValidationAll.GroupBy(o => o.HospitalUnitIDCounter).ToList()
                                                         from t in v.GroupBy(x => x.LocationID).ToList()
                                                         select new RMC.BusinessEntities.BEReports
                                                         {
                                                             Name = "Current Location",
                                                             ColumnName = objectGenericLocation.Find(delegate(RMC.DataService.Location objectLocation)
                                                             {
                                                                 return objectLocation.LocationID == t.Key;
                                                             }).Location1,
                                                             ColumnNumber = objectGenericLocation.Find(delegate(RMC.DataService.Location objectLocation)
                                                             {
                                                                 return objectLocation.LocationID == t.Key;
                                                             }).LocationID,
                                                             MonthName = BSCommon.GetMonthName(t.FirstOrDefault(r => r.Month != "").Month) + " " + t.FirstOrDefault().Year,
                                                             RowName = t.FirstOrDefault(r => r.HospitalUnitName != "").HospitalUnitName,
                                                             Values = string.Format("{0:#.##}%", ((double)t.Count(r => r.LocationID == t.Key) / (double)v.Count(c => c.LocationID != 0)) * 100),
                                                             ValuesSum = Convert.ToDouble(string.Format("{0:#.##}", ((double)t.Count(r => r.LocationID == t.Key) / (double)v.Count(c => c.LocationID != 0)) * 100))
                                                         }).ToList();

                            objectNewGenericBEReports = objectNewGenericBEReports.FindAll(delegate(RMC.BusinessEntities.BEReports objectBEReport)
                            {
                                //return objectBEReport.ColumnName.ToLower().Trim() == dbValues.ToLower().Trim();
                                return true;
                            });

                        }
                        if (ProfileSubCategoryValue == "Staffing Model")
                        {
                            List<RMC.DataService.ResourceRequirement> objectGenericResourceRequirement = _objectRMCDataContext.ResourceRequirements.ToList();

                            objectGenericBEValidation = objectGenericBEValidation.FindAll(delegate(RMC.BusinessEntities.BEValidation objectNewBEValidation)
                            {
                                return objectGenericResourceRequirement.Exists(delegate(RMC.DataService.ResourceRequirement objectResourceRequirement)
                                {
                                    return objectResourceRequirement.ResourceRequirementID == objectNewBEValidation.ResourceRequirementID;
                                });
                            });


                            objectGenericListResult = (from v in objectGenericBEValidation.OrderBy(o => o.Year).ThenBy(p => p.MonthIndex)
                                                       group v by new { v.Year, v.MonthIndex } into g
                                                       from t in g.GroupBy(x => x.ResourceRequirementID).ToList()
                                                       select new RMC.BusinessEntities.BEReports
                                                       {
                                                           Name = "Staffing Model",
                                                           ColumnName = objectGenericResourceRequirement.Find(delegate(RMC.DataService.ResourceRequirement objectResourceRequirement)
                                                           {
                                                               return objectResourceRequirement.ResourceRequirementID == t.Key;
                                                           }).ResourceRequirement1,
                                                           ColumnNumber = objectGenericResourceRequirement.Find(delegate(RMC.DataService.ResourceRequirement objectResourceRequirement)
                                                           {
                                                               return objectResourceRequirement.ResourceRequirementID == t.Key;
                                                           }).ResourceRequirementID,
                                                           MonthName = BSCommon.GetMonthName(t.FirstOrDefault(r => r.Month != "").Month) + " " + t.FirstOrDefault().Year,
                                                           RowName = t.FirstOrDefault(r => r.HospitalUnitName != "").HospitalUnitName,
                                                           Values = string.Format("{0:#.##}%", ((double)t.Count(r => r.ResourceRequirementID == t.Key) / (double)g.Count(c => c.ResourceRequirementID != 0)) * 100),
                                                           ValuesSum = Convert.ToDouble(string.Format("{0:#.##}", ((double)t.Count(r => r.ResourceRequirementID == t.Key) / (double)g.Count(c => c.ResourceRequirementID != 0)) * 100))
                                                       }).ToList();

                            objectGenericListResult = objectGenericListResult.FindAll(delegate(RMC.BusinessEntities.BEReports objectBEReport)
                            {
                                //return objectBEReport.ColumnName.ToLower().Trim() == dbValues.ToLower().Trim();
                                return true;
                            });

                            //for comparing with National Database means calculating function values (min, max, quartile etc)

                            objectGenericBEValidationAll = objectGenericBEValidationAll.FindAll(delegate(RMC.BusinessEntities.BEValidation objectNewBEValidation)
                            {
                                return objectGenericResourceRequirement.Exists(delegate(RMC.DataService.ResourceRequirement objectResourceRequirement)
                                {
                                    return objectResourceRequirement.ResourceRequirementID == objectNewBEValidation.ResourceRequirementID;
                                });
                            });

                            objectNewGenericBEReports = (from v in objectGenericBEValidationAll.GroupBy(o => o.HospitalUnitIDCounter).ToList()
                                                         from t in v.GroupBy(x => x.ResourceRequirementID).ToList()
                                                         select new RMC.BusinessEntities.BEReports
                                                         {
                                                             Name = "Staffing Model",
                                                             ColumnName = objectGenericResourceRequirement.Find(delegate(RMC.DataService.ResourceRequirement objectResourceRequirement)
                                                             {
                                                                 return objectResourceRequirement.ResourceRequirementID == t.Key;
                                                             }).ResourceRequirement1,
                                                             ColumnNumber = objectGenericResourceRequirement.Find(delegate(RMC.DataService.ResourceRequirement objectResourceRequirement)
                                                             {
                                                                 return objectResourceRequirement.ResourceRequirementID == t.Key;
                                                             }).ResourceRequirementID,
                                                             MonthName = BSCommon.GetMonthName(t.FirstOrDefault(r => r.Month != "").Month) + " " + t.FirstOrDefault().Year,
                                                             RowName = t.FirstOrDefault(r => r.HospitalUnitName != "").HospitalUnitName,
                                                             Values = string.Format("{0:#.##}%", ((double)t.Count(r => r.ResourceRequirementID == t.Key) / (double)v.Count(c => c.ResourceRequirementID != 0)) * 100),
                                                             ValuesSum = Convert.ToDouble(string.Format("{0:#.##}", ((double)t.Count(r => r.ResourceRequirementID == t.Key) / (double)v.Count(c => c.ResourceRequirementID != 0)) * 100))
                                                         }).ToList();

                            objectNewGenericBEReports = objectNewGenericBEReports.FindAll(delegate(RMC.BusinessEntities.BEReports objectBEReport)
                            {
                                //return objectBEReport.ColumnName.ToLower().Trim() == dbValues.ToLower().Trim();
                                return true;
                            });
                        }
                        //
                        if (ProfileSubCategoryValue == "Cognitive")
                        {
                            List<RMC.DataService.CognitiveCategory> objectGenericCognitiveCategories = _objectRMCDataContext.CognitiveCategories.ToList();

                            objectGenericBEValidation = objectGenericBEValidation.FindAll(delegate(RMC.BusinessEntities.BEValidation objectNewBEValidation)
                            {
                                return objectGenericCognitiveCategories.Exists(delegate(RMC.DataService.CognitiveCategory objectCognitiveCategory)
                                {
                                    return objectCognitiveCategory.CognitiveCategoryID == objectNewBEValidation.CognitiveCategoryID;
                                });
                            });


                            objectGenericListResult = (from v in objectGenericBEValidation.OrderBy(o => o.Year).ThenBy(p => p.MonthIndex)
                                                                   group v by new { v.Year, v.MonthIndex } into g
                                                                   from t in g.GroupBy(x => x.CognitiveCategoryID).ToList()
                                                                   select new RMC.BusinessEntities.BEReports
                                                                   {
                                                                       Name = "Cognitive",
                                                                       ColumnName = objectGenericCognitiveCategories.Find(delegate(RMC.DataService.CognitiveCategory objectLocation)
                                                                       {
                                                                           return objectLocation.CognitiveCategoryID == t.Key;
                                                                       }).CognitiveCategoryText,
                                                                       ColumnNumber = objectGenericCognitiveCategories.Find(delegate(RMC.DataService.CognitiveCategory objectLocation)
                                                                       {
                                                                           return objectLocation.CognitiveCategoryID == t.Key;
                                                                       }).CognitiveCategoryID,
                                                                       MonthName = BSCommon.GetMonthName(t.FirstOrDefault(r => r.Month != "").Month) + " " + t.FirstOrDefault().Year,
                                                                       RowName = t.FirstOrDefault(r => r.HospitalUnitName != "").HospitalUnitName,
                                                                       Values = string.Format("{0:#.##}%", ((double)t.Count(r => r.CognitiveCategoryID == t.Key) / (double)g.Count(c => c.CognitiveCategoryID != 0)) * 100),
                                                                       ValuesSum = Convert.ToDouble(string.Format("{0:#.##}", ((double)t.Count(r => r.CognitiveCategoryID == t.Key) / (double)g.Count(c => c.CognitiveCategoryID != 0)) * 100))
                                                                   }).ToList();


                            objectGenericListResult = objectGenericListResult.FindAll(delegate(RMC.BusinessEntities.BEReports objectBEReport)
                            {
                                //return objectBEReport.ColumnName.ToLower().Trim() == dbValues.ToLower().Trim();
                                return true;
                            });

                            //for comparing with National Database means calculating function values (min, max, quartile etc)

                            objectGenericBEValidationAll = objectGenericBEValidationAll.FindAll(delegate(RMC.BusinessEntities.BEValidation objectNewBEValidation)
                            {
                                return objectGenericCognitiveCategories.Exists(delegate(RMC.DataService.CognitiveCategory objectLocation)
                                {
                                    return objectLocation.CognitiveCategoryID == objectNewBEValidation.CognitiveCategoryID;
                                });
                            });

                            objectNewGenericBEReports = (from v in objectGenericBEValidationAll.GroupBy(o => o.HospitalUnitIDCounter).ToList()
                                                         from t in v.GroupBy(x => x.CognitiveCategoryID).ToList()
                                                         select new RMC.BusinessEntities.BEReports
                                                         {
                                                             Name = "Cognitive",
                                                             ColumnName = objectGenericCognitiveCategories.Find(delegate(RMC.DataService.CognitiveCategory objectCognitiveCategory)
                                                             {
                                                                 return objectCognitiveCategory.CognitiveCategoryID == t.Key;
                                                             }).CognitiveCategoryText,
                                                             ColumnNumber = objectGenericCognitiveCategories.Find(delegate(RMC.DataService.CognitiveCategory objectLocation)
                                                             {
                                                                 return objectLocation.CognitiveCategoryID == t.Key;
                                                             }).CognitiveCategoryID,
                                                             MonthName = BSCommon.GetMonthName(t.FirstOrDefault(r => r.Month != "").Month) + " " + t.FirstOrDefault().Year,
                                                             RowName = t.FirstOrDefault(r => r.HospitalUnitName != "").HospitalUnitName,
                                                             Values = string.Format("{0:#.##}%", ((double)t.Count(r => r.CognitiveCategoryID == t.Key) / (double)v.Count(c => c.CognitiveCategoryID != 0)) * 100),
                                                             ValuesSum = Convert.ToDouble(string.Format("{0:#.##}", ((double)t.Count(r => r.CognitiveCategoryID == t.Key) / (double)v.Count(c => c.CognitiveCategoryID != 0)) * 100))
                                                         }).ToList();

                            objectNewGenericBEReports = objectNewGenericBEReports.FindAll(delegate(RMC.BusinessEntities.BEReports objectBEReport)
                            {
                                //return objectBEReport.ColumnName.ToLower().Trim() == dbValues.ToLower().Trim();
                                return true;
                            });

                        }
                        //
                        if (objectNewGenericBEReports != null)
                        {
                            //filteration according to DataPoints
                            if (dataPointsFrom != null && dataPointsTo == null)
                            {
                                if (optDataPointsFrom == 1)
                                {
                                    objectNewGenericBEReports = objectNewGenericBEReports.Where(x => Convert.ToInt32(x.DataPoint) < dataPointsFrom).ToList();
                                }
                                if (optDataPointsFrom == 2)
                                {
                                    objectNewGenericBEReports = objectNewGenericBEReports.Where(x => x.DataPoint > dataPointsFrom).ToList();
                                }
                                if (optDataPointsFrom == 3)
                                {
                                    objectNewGenericBEReports = objectNewGenericBEReports.Where(x => x.DataPoint == dataPointsFrom).ToList();
                                }
                                if (optDataPointsFrom == 4)
                                {
                                    objectNewGenericBEReports = objectNewGenericBEReports.Where(x => x.DataPoint >= dataPointsFrom).ToList();
                                }
                                if (optDataPointsFrom == 5)
                                {
                                    objectNewGenericBEReports = objectNewGenericBEReports.Where(x => x.DataPoint <= dataPointsFrom).ToList();
                                }
                                if (optDataPointsFrom == 6)
                                {
                                    objectNewGenericBEReports = objectNewGenericBEReports.Where(x => x.DataPoint != dataPointsFrom).ToList();
                                }
                            }

                            if ((optDataPointsFrom == 0 && optdataPointsTo == 0) && (dataPointsFrom != null && dataPointsTo != null))
                            {
                                objectNewGenericBEReports = objectNewGenericBEReports.Where(x => x.DataPoint >= dataPointsFrom && x.DataPoint <= dataPointsTo).ToList();
                            }

                            foreach (var r in objectNewGenericBEReports.GroupBy(o => o.ColumnName))
                            {
                                //Calculate Minimum value.
                                RMC.BusinessEntities.BEFunctionNames objectNewBEFunctionNamesMin = new RMC.BusinessEntities.BEFunctionNames();
                                double valueMin = 0;
                                valueMin = r.Select(s => Convert.ToDouble((s.Values.Length > 0) ? s.Values.Substring(0, s.Values.Length - 1) : "0")).Min();

                                objectNewBEFunctionNamesMin.ColumnName = r.FirstOrDefault().ColumnName;
                                objectNewBEFunctionNamesMin.FunctionName = "Minimum";
                                objectNewBEFunctionNamesMin.FunctionNameDouble = valueMin;
                                objectNewBEFunctionNamesMin.FunctionValueText = string.Format("{0:#.##}%", (valueMin == 0) ? "0.00" : valueMin.ToString());

                                objectGenericBEFunctionNames.Add(objectNewBEFunctionNamesMin);

                                //Calculate Maximum value
                                RMC.BusinessEntities.BEFunctionNames objectNewBEFunctionNamesMax = new RMC.BusinessEntities.BEFunctionNames();
                                double valueMax = 0;
                                valueMax = r.Select(s => Convert.ToDouble((s.Values.Length > 0) ? s.Values.Substring(0, s.Values.Length - 1) : "0")).Max();

                                objectNewBEFunctionNamesMax.ColumnName = r.FirstOrDefault().ColumnName;
                                objectNewBEFunctionNamesMax.FunctionName = "Maximum";
                                objectNewBEFunctionNamesMax.FunctionNameDouble = valueMax;
                                objectNewBEFunctionNamesMax.FunctionValueText = string.Format("{0:#.##}%", (valueMax == 0) ? "0.00" : valueMax.ToString());

                                //Calculate Average value
                                RMC.BusinessEntities.BEFunctionNames objectNewBEFunctionNamesAvg = new RMC.BusinessEntities.BEFunctionNames();
                                List<double> valueSum = null;
                                valueSum = r.Select(s => Convert.ToDouble((s.Values.Length > 0) ? s.Values.Substring(0, s.Values.Length - 1) : "0")).ToList();

                                objectNewBEFunctionNamesAvg.ColumnName = r.FirstOrDefault().ColumnName;
                                objectNewBEFunctionNamesAvg.FunctionName = "Average";
                                objectNewBEFunctionNamesAvg.FunctionNameDouble = (valueSum.Sum() / r.Select(s => s.RowName).Distinct().Count());
                                objectNewBEFunctionNamesAvg.FunctionValueText = string.Format("{0:#.##}%", (objectNewBEFunctionNamesAvg.FunctionNameDouble == 0) ? "0.00" : string.Format("{0:#.##}", objectNewBEFunctionNamesAvg.FunctionNameDouble));

                                //Median Value.
                                RMC.BusinessEntities.BEFunctionNames objectNewBEFunctionNamesMed = new RMC.BusinessEntities.BEFunctionNames();
                                int median = 0;
                                int medianEven = 0;
                                int count = r.Select(s => s.RowName).Distinct().Count();
                                if (count % 2 == 0)
                                {
                                    median = count / 2;
                                    medianEven = median + 1;

                                    objectNewBEFunctionNamesMed.ColumnName = r.FirstOrDefault().ColumnName;
                                    objectNewBEFunctionNamesMed.FunctionName = "Median";
                                    objectNewBEFunctionNamesMed.FunctionNameDouble = (r.OrderBy(x => x.ValuesSum).Select(s => s.ValuesSum).ToList().ElementAt(median - 1) + r.OrderBy(x => x.ValuesSum).Select(s => s.ValuesSum).ToList().ElementAt(medianEven - 1)) / 2;
                                    objectNewBEFunctionNamesMed.FunctionValueText = string.Format("{0:#.##}%", string.Format("{0:#.##}", (objectNewBEFunctionNamesMed.FunctionNameDouble == 0.00) ? "0.00" : string.Format("{0:#.##}", objectNewBEFunctionNamesMed.FunctionNameDouble)));
                                }
                                else
                                {
                                    median = (count + 1) / 2;

                                    objectNewBEFunctionNamesMed.ColumnName = r.FirstOrDefault().ColumnName;
                                    objectNewBEFunctionNamesMed.FunctionName = "Median";
                                    objectNewBEFunctionNamesMed.FunctionNameDouble = r.OrderBy(x => x.ValuesSum).Select(s => s.ValuesSum).ToList().ElementAt(median - 1);
                                    objectNewBEFunctionNamesMed.FunctionValueText = string.Format("{0:#.##}%", string.Format("{0:#.##}", (objectNewBEFunctionNamesMed.FunctionNameDouble == 0.00) ? "0.00" : string.Format("{0:#.##}", objectNewBEFunctionNamesMed.FunctionNameDouble)));
                                }
                                //median--;

                                //Quartile
                                RMC.BusinessEntities.BEFunctionNames objectNewBEFunctionNamesQuartile1 = new RMC.BusinessEntities.BEFunctionNames();

                                objectNewBEFunctionNamesQuartile1.ColumnName = r.FirstOrDefault().ColumnName;
                                objectNewBEFunctionNamesQuartile1.FunctionName = "Quartile(1)";
                                objectNewBEFunctionNamesQuartile1.FunctionNameDouble = CalculateQuartile(count, 1, r.Select(s => s.ValuesSum).ToList());
                                objectNewBEFunctionNamesQuartile1.FunctionValueText = string.Format("{0:#.##}%", (objectNewBEFunctionNamesQuartile1.FunctionNameDouble == 0.00) ? "0.00" : string.Format("{0:#.##}", objectNewBEFunctionNamesQuartile1.FunctionNameDouble));

                                RMC.BusinessEntities.BEFunctionNames objectNewBEFunctionNamesQuartile3 = new RMC.BusinessEntities.BEFunctionNames();

                                objectNewBEFunctionNamesQuartile3.ColumnName = r.FirstOrDefault().ColumnName;
                                objectNewBEFunctionNamesQuartile3.FunctionName = "Quartile(3)";
                                objectNewBEFunctionNamesQuartile3.FunctionNameDouble = CalculateQuartile(count, 3, r.Select(s => s.ValuesSum).ToList());
                                objectNewBEFunctionNamesQuartile3.FunctionValueText = string.Format("{0:#.##}%", (objectNewBEFunctionNamesQuartile3.FunctionNameDouble == 0.00) ? "0.00" : string.Format("{0:#.##}", objectNewBEFunctionNamesQuartile3.FunctionNameDouble));

                                objectGenericBEFunctionNames.Add(objectNewBEFunctionNamesQuartile1);
                                objectGenericBEFunctionNames.Add(objectNewBEFunctionNamesMed);
                                objectGenericBEFunctionNames.Add(objectNewBEFunctionNamesAvg);
                                objectGenericBEFunctionNames.Add(objectNewBEFunctionNamesQuartile3);
                                objectGenericBEFunctionNames.Add(objectNewBEFunctionNamesMax);
                            }

                            int countt = objectGenericListResult.Count;
                            //List<RMC.BusinessEntities.BEReports> objectGenericListResultTemp = new List<RMC.BusinessEntities.BEReports>();
                            //objectGenericListResultTemp = objectGenericListResult;
                            objectGenericBEFunctionNames.ForEach(delegate(RMC.BusinessEntities.BEFunctionNames objectBEFunctionNames)
                            {
                                for (int index = 0; index < countt; index++)
                                {
                                    RMC.BusinessEntities.BEReports objectBEReports = new RMC.BusinessEntities.BEReports();

                                    objectBEReports.ColumnName = objectBEFunctionNames.FunctionName;
                                    //objectBEReports.MonthName = BSCommon.GetMonthName(Convert.ToString(index + 1));
                                    objectBEReports.MonthName = objectGenericListResult[index].MonthName;
                                    objectBEReports.Values = objectBEFunctionNames.FunctionValueText;
                                    objectBEReports.ValuesSum = objectBEFunctionNames.FunctionNameDouble;

                                    objectGenericListResult.Add(objectBEReports);
                                }
                            });
                        }

                    }
                //cm

                return objectGenericBEFunctionNames;
                //======================
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                //DisposeGlobalObjects();
            }
        }
        //cm
        /// <summary>
        /// Calculates Function Values for HospitalBenchmarkSummary and MonthlySummaryDashboard Report for each Columns and display data in Grid
        /// </summary>
        public System.Data.DataTable CalculateFunctionValuesGrid(string activitiesID, string valueAddedCategoryID, string OthersCategoryID, string LocationCategoryID, int? hospitalUnitID, int? firstYear, int? lastYear, int? firstMonth, int? lastMonth, int? bedInUnitFrom, int optBedInUnitFrom, int? bedInUnitTo, int optBedInUnitTo, float? budgetedPatientFrom, int optBudgetedPatientFrom, float? budgetedPatientTo, int optBudgetedPatientTo, string startDate, string endDate, int? electronicDocumentFrom, int optElectronicDocumentFrom, int? electronicDocumentTo, int optElectronicDocumentTo, int docByException, string unitType, string pharmacyType, string hospitalType, int optHospitalSizeFrom, int? hospitalSizeFrom, int optHospitalSizeTo, int? hospitalSizeTo, int? countryId, int? stateId, string activities, string value, string others, string location, int? dataPointsFrom, int optDataPointsFrom, int? dataPointsTo, int optdataPointsTo, string configName, string unitIds)
        {
            try
            {
                System.Data.DataTable dt = null;
                if (valueAddedCategoryID != null || OthersCategoryID != null || LocationCategoryID != null || activitiesID != null)
                {
                    MaintainSessions.NationalDatabaseCount = 0;
                    MaintainSessions.SessionFunctionValues = null;
                    MaintainSessions.SessionHospitalBenchmarkSummary = null;
                    MaintainSessions.SessionSearchHospitalData = null;
                    List<RMC.BusinessEntities.BEFunctionNames> objectGenericBEFunctionNames = new List<RMC.BusinessEntities.BEFunctionNames>();
                    //If condition and Session variable is added by Davinder Kumar for a purpose of Code optimization.
                    objectGenericBEFunctionNames = CalculateFunctionValuesTest(activitiesID, valueAddedCategoryID, OthersCategoryID, LocationCategoryID, hospitalUnitID, firstYear, lastYear, firstMonth, lastMonth, bedInUnitFrom, optBedInUnitFrom, bedInUnitTo, optBedInUnitTo, budgetedPatientFrom, optBudgetedPatientFrom, budgetedPatientTo, optBudgetedPatientTo, startDate, endDate, electronicDocumentFrom, optElectronicDocumentFrom, electronicDocumentTo, optElectronicDocumentTo, docByException, unitType, pharmacyType, hospitalType, optHospitalSizeFrom, hospitalSizeFrom, optHospitalSizeTo, hospitalSizeTo, countryId, stateId, activities, value, others, location, dataPointsFrom, optDataPointsFrom, dataPointsTo, optdataPointsTo, configName, unitIds);
                    //}
                    //return objectGenericBEFunctionNames;
                    removeIncDecSymbol<RMC.BusinessEntities.BEFunctionNames>("ColumnName", ref objectGenericBEFunctionNames);
                    
                    dt = AddRowsCalculateFunctionValues(CreateTableCalculateFunctionValues(objectGenericBEFunctionNames), objectGenericBEFunctionNames);

                } 
                return dt;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {               
                //DisposeGlobalObjects();
            }
        }

        public System.Data.DataTable CalculateFunctionValuesGridForUnitAssessment(string dbValues, string ProfileCategoryValue, string ProfileSubCategoryValue, string valueAddedCategoryID, string OthersCategoryID, string LocationCategoryID, int? hospitalUnitID, int? firstYear, int? lastYear, int? firstMonth, int? lastMonth, int? bedInUnitFrom, int optBedInUnitFrom, int? bedInUnitTo, int optBedInUnitTo, float? budgetedPatientFrom, int optBudgetedPatientFrom, float? budgetedPatientTo, int optBudgetedPatientTo, string startDate, string endDate, int? electronicDocumentFrom, int optElectronicDocumentFrom, int? electronicDocumentTo, int optElectronicDocumentTo, int docByException, string unitType, string pharmacyType, string hospitalType, int optHospitalSizeFrom, int? hospitalSizeFrom, int optHospitalSizeTo, int? hospitalSizeTo, int? countryId, int? stateId, string activities, string value, string others, string location, int? dataPointsFrom, int optDataPointsFrom, int? dataPointsTo, int optdataPointsTo, string configName, string unitIds)
        {
            try
            {
                List<RMC.BusinessEntities.BEFunctionNames> objectGenericBEFunctionNames = new List<RMC.BusinessEntities.BEFunctionNames>();
                //If condition and Session variable is added by Davinder Kumar for a purpose of Code optimization.
                List<RMC.BusinessEntities.BEReports >objbereport= new List<RMC.BusinessEntities.BEReports>();
                //objbereport = GetDataForLineChartModified(ProfileCategoryValue, ProfileSubCategoryValue, valueAddedCategoryID, OthersCategoryID, LocationCategoryID, hospitalUnitID, firstYear, lastYear, firstMonth, lastMonth, bedInUnitFrom, optBedInUnitFrom, bedInUnitTo, optBedInUnitTo, budgetedPatientFrom, optBudgetedPatientFrom, budgetedPatientTo, optBudgetedPatientTo, startDate, endDate, electronicDocumentFrom, optElectronicDocumentFrom, electronicDocumentTo, optElectronicDocumentTo, docByException, unitType, pharmacyType, hospitalType, optHospitalSizeFrom, hospitalSizeFrom, optHospitalSizeTo, hospitalSizeTo, countryId, stateId, value, others, location, dataPointsFrom, optDataPointsFrom, dataPointsTo, optdataPointsTo, configName, dbValues);
                objectGenericBEFunctionNames = CalculateFunctionValuesTestForUnitAcc(dbValues, ProfileCategoryValue, ProfileSubCategoryValue, valueAddedCategoryID, OthersCategoryID, LocationCategoryID, hospitalUnitID, firstYear, lastYear, firstMonth, lastMonth, bedInUnitFrom, optBedInUnitFrom, bedInUnitTo, optBedInUnitTo, budgetedPatientFrom, optBudgetedPatientFrom, budgetedPatientTo, optBudgetedPatientTo, startDate, endDate, electronicDocumentFrom, optElectronicDocumentFrom, electronicDocumentTo, optElectronicDocumentTo, docByException, unitType, pharmacyType, hospitalType, optHospitalSizeFrom, hospitalSizeFrom, optHospitalSizeTo, hospitalSizeTo, countryId, stateId, activities, value, others, location, dataPointsFrom, optDataPointsFrom, dataPointsTo, optdataPointsTo, configName,unitIds);
                //}
                //return objectGenericBEFunctionNames;
                removeIncDecSymbol<RMC.BusinessEntities.BEFunctionNames>("ColumnName", ref objectGenericBEFunctionNames);
                System.Data.DataTable dt = null;
                dt = AddRowsCalculateFunctionValues(CreateTableCalculateFunctionValues(objectGenericBEFunctionNames), objectGenericBEFunctionNames);
                if (dt.Rows.Count >= 5)
                {
                    dt.Rows[5].Delete();
                    dt.Rows[4].Delete();
                    dt.Rows[3].Delete();
                    dt.Rows[0].Delete();

                    int col = dt.Columns.Count;
                    dt.Rows[0][0] = "Top Quartile in Minutes";
                    dt.Rows[1][0] = "Median in Minutes";
                    for (int i = 1; i < col; i++)
                    {
                        string val = dt.Rows[0][i].ToString();
                        string[] val1 = val.Split('%');
                        //string a = val1[0];
                        dt.Rows[0][i] = (12 * 60 * Convert.ToDecimal(val1[0])) / 100;
                    }
                    for (int i = 1; i < col; i++)
                    {
                        string val = dt.Rows[1][i].ToString();
                        string[] val1 = val.Split('%');

                        dt.Rows[1][i] = (12 * 60 * Convert.ToDecimal(val1[0])) / 100;
                    }
                }
                HttpContext.Current.Session["dtTopQuritile"] = dt;
                return dt;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                //DisposeGlobalObjects();
            }
        }

        /// <summary>
        /// Calculation on Data According to the Profile Category
        /// </summary>
        private List<RMC.BusinessEntities.BEReports> CalculationOnDataByProfileTest(string profileName, string profileCategory, List<RMC.BusinessEntities.BECategoryProfile> objectGenericBECategoryProfile, List<RMC.BusinessEntities.BEValidation> objectGenericBEValidation, int? dataPointsFrom, int optDataPointsFrom, int? dataPointsTo, int optdataPointsTo)
        {
            try
            {
                int totalCount = 0;
                List<RMC.DataService.ValueAddedType> objectGenericValueAddedType = _objectRMCDataContext.ValueAddedTypes.OrderBy(o => o.TypeID).ToList();
                List<RMC.DataService.CategoryGroup> objectGenericCategoryGroup = _objectRMCDataContext.CategoryGroups.OrderBy(o => o.CategoryGroupID).ToList();
                List<RMC.DataService.ActivitiesCategory> objectGenericActivitiesCategory = _objectRMCDataContext.ActivitiesCategories.OrderBy(o => o.ActivitiesID).ToList();
                List<RMC.DataService.LocationCategory> objectGenericLocationCategory = _objectRMCDataContext.LocationCategories.OrderBy(o => o.LocationID).ToList();
                List<RMC.BusinessEntities.BEReports> objectGenericBEReports = null;

                //objectGenericBEValidation.ForEach(delegate(RMC.BusinessEntities.BEValidation objectBEValidation)
                //{
                //    RMC.BusinessEntities.BECategoryProfile objectNewBECategoryProfile = objectGenericBECategoryProfile.Find(delegate(RMC.BusinessEntities.BECategoryProfile objectBECategoryProfile)
                //    {
                //        if (objectBECategoryProfile.SubActivityID > 0 && objectBEValidation.SubActivityID > 0)
                //            return objectBECategoryProfile.LocationID == objectBEValidation.LocationID && objectBECategoryProfile.ActivityID == objectBEValidation.ActivityID && objectBECategoryProfile.SubActivityID == objectBEValidation.SubActivityID;
                //        else
                //            return objectBECategoryProfile.LocationID == objectBEValidation.LocationID && objectBECategoryProfile.ActivityID == objectBEValidation.ActivityID;
                //    });
                //    if (objectNewBECategoryProfile != null)
                //    {
                //        objectBEValidation.CategoryGroupID = objectNewBECategoryProfile.CategoryAssignmentID.Value;
                //    }
                //    else
                //    {
                //        totalCount++;
                //        objectBEValidation.CategoryGroupID = 0;
                //    }
                //});
                
                List<RMC.BusinessEntities.BEValidation> objectGenericBEValidationGZero = (from be in objectGenericBEValidation.Where(w=>w.SubActivityID > 0)
                                                                                        join cp in objectGenericBECategoryProfile.Where(w => w.SubActivityID > 0)
                                                                                        on new { be.LocationID, be.ActivityID, SubActivityID = be.SubActivityID.Value }
                                                                                        equals
                                                                                        new { cp.LocationID, cp.ActivityID, SubActivityID = cp.SubActivityID } 
                                                                                        select new RMC.BusinessEntities.BEValidation 
                                                                                        {
                                                                                            ActiveError1 = be.ActiveError1,
                                                                                            ActiveError2 = be.ActiveError2,
                                                                                            ActiveError3 = be.ActiveError3,
                                                                                            ActiveError4 = be.ActiveError4,
                                                                                            ActivityDate = be.ActivityDate,
                                                                                            ActivityID = be.ActivityID,
                                                                                            ActivityName = be.ActivityName,
                                                                                            ActivityText = be.ActivityText,
                                                                                            ActivityTime = be.ActivityTime,
                                                                                            CategoryGroupID = cp.CategoryAssignmentID,
                                                                                            CognitiveCategory = be.CognitiveCategory,
                                                                                            CreatedBy = be.CreatedBy,
                                                                                            CreatedDate = be.CreatedDate,
                                                                                            DataPoint = be.DataPoint,
                                                                                            DeletedBy = be.DeletedBy,
                                                                                            DeletedDate = be.DeletedDate,
                                                                                            FileName = be.FileName,
                                                                                            HospitalID = be.HospitalID,
                                                                                            HospitalSize = be.HospitalSize,
                                                                                            HospitalUnitID = be.HospitalUnitID,
                                                                                            HospitalUnitIDCounter = be.HospitalUnitIDCounter,
                                                                                            HospitalUnitName = be.HospitalUnitName,
                                                                                            IsActive = be.IsActive,
                                                                                            IsActiveError = be.IsActiveError,
                                                                                            IsDeleted = be.IsDeleted,
                                                                                            IsErrorExist = be.IsErrorExist,
                                                                                            IsErrorInActivity = be.IsErrorInActivity,
                                                                                            IsErrorInCognitiveCategory = be.IsErrorInCognitiveCategory,
                                                                                            IsErrorInLastLocation = be.IsErrorInLastLocation,
                                                                                            IsErrorInLocation = be.IsErrorInLocation,
                                                                                            IsErrorInResourceRequirement = be.IsErrorInResourceRequirement,
                                                                                            IsErrorInSubActivity  = be.IsErrorInSubActivity,
                                                                                            LastLocationDate = be.LastLocationDate,
                                                                                            LastLocationID = be.LastLocationID,
                                                                                            LastLocationName = be.LastLocationName,
                                                                                            LastLocationText = be.LastLocationText,
                                                                                            LastLocationTime = be.LastLocationTime,
                                                                                            LocationDate  = be.LocationDate,
                                                                                            LocationID = be.LocationID,
                                                                                            LocationName = be.LocationName,
                                                                                            LocationText = be.LocationText,
                                                                                            LocationTime = be.LocationTime,
                                                                                            ModifiedBy = be.ModifiedBy,
                                                                                            ModifiedDate = be.ModifiedDate,
                                                                                            Month = be.Month,
                                                                                            MonthIndex = be.MonthIndex,
                                                                                            NurseID = be.NurseID,
                                                                                            NursePDAInfo = be.NursePDAInfo,
                                                                                            NurserDetailID = be.NurserDetailID,
                                                                                            RecordCounter = be.RecordCounter,
                                                                                            ResourceRequirementID = be.ResourceRequirementID,
                                                                                            ResourceText = be.ResourceText,
                                                                                            SpecialActivity = be.SpecialActivity,
                                                                                            SpecialCategory = be.SpecialCategory,
                                                                                            SubActivityDate = be.SubActivityDate,
                                                                                            SubActivityID = be.SubActivityID,
                                                                                            SubActivityName = be.SubActivityName,
                                                                                            SubActivityText = be.SubActivityText,
                                                                                            SubActivityTime = be.SubActivityTime,
                                                                                            TypeID = be.TypeID,
                                                                                            Year = be.Year
                                                                                        }).ToList();

                List<RMC.BusinessEntities.BEValidation> objectGenericBEValidationZero = (from be in objectGenericBEValidation.Where(w => w.SubActivityID == 0)
                                                                                         join cp in objectGenericBECategoryProfile.Distinct(new RMC.BusinessEntities.CompareCategoryProfile())
                                                                                         on new { be.LocationID, be.ActivityID }
                                                                                         equals
                                                                                          new { cp.LocationID, cp.ActivityID }
                                                                                         select new RMC.BusinessEntities.BEValidation
                                                                                         {
                                                                                             ActiveError1 = be.ActiveError1,
                                                                                             ActiveError2 = be.ActiveError2,
                                                                                             ActiveError3 = be.ActiveError3,
                                                                                             ActiveError4 = be.ActiveError4,
                                                                                             ActivityDate = be.ActivityDate,
                                                                                             ActivityID = be.ActivityID,
                                                                                             ActivityName = be.ActivityName,
                                                                                             ActivityText = be.ActivityText,
                                                                                             ActivityTime = be.ActivityTime,
                                                                                             CategoryGroupID = cp.CategoryAssignmentID,
                                                                                             CognitiveCategory = be.CognitiveCategory,
                                                                                             CreatedBy = be.CreatedBy,
                                                                                             CreatedDate = be.CreatedDate,
                                                                                             DataPoint = be.DataPoint,
                                                                                             DeletedBy = be.DeletedBy,
                                                                                             DeletedDate = be.DeletedDate,
                                                                                             FileName = be.FileName,
                                                                                             HospitalID = be.HospitalID,
                                                                                             HospitalSize = be.HospitalSize,
                                                                                             HospitalUnitID = be.HospitalUnitID,
                                                                                             HospitalUnitIDCounter = be.HospitalUnitIDCounter,
                                                                                             HospitalUnitName = be.HospitalUnitName,
                                                                                             IsActive = be.IsActive,
                                                                                             IsActiveError = be.IsActiveError,
                                                                                             IsDeleted = be.IsDeleted,
                                                                                             IsErrorExist = be.IsErrorExist,
                                                                                             IsErrorInActivity = be.IsErrorInActivity,
                                                                                             IsErrorInCognitiveCategory = be.IsErrorInCognitiveCategory,
                                                                                             IsErrorInLastLocation = be.IsErrorInLastLocation,
                                                                                             IsErrorInLocation = be.IsErrorInLocation,
                                                                                             IsErrorInResourceRequirement = be.IsErrorInResourceRequirement,
                                                                                             IsErrorInSubActivity = be.IsErrorInSubActivity,
                                                                                             LastLocationDate = be.LastLocationDate,
                                                                                             LastLocationID = be.LastLocationID,
                                                                                             LastLocationName = be.LastLocationName,
                                                                                             LastLocationText = be.LastLocationText,
                                                                                             LastLocationTime = be.LastLocationTime,
                                                                                             LocationDate = be.LocationDate,
                                                                                             LocationID = be.LocationID,
                                                                                             LocationName = be.LocationName,
                                                                                             LocationText = be.LocationText,
                                                                                             LocationTime = be.LocationTime,
                                                                                             ModifiedBy = be.ModifiedBy,
                                                                                             ModifiedDate = be.ModifiedDate,
                                                                                             Month = be.Month,
                                                                                             MonthIndex = be.MonthIndex,
                                                                                             NurseID = be.NurseID,
                                                                                             NursePDAInfo = be.NursePDAInfo,
                                                                                             NurserDetailID = be.NurserDetailID,
                                                                                             RecordCounter = be.RecordCounter,
                                                                                             ResourceRequirementID = be.ResourceRequirementID,
                                                                                             ResourceText = be.ResourceText,
                                                                                             SpecialActivity = be.SpecialActivity,
                                                                                             SpecialCategory = be.SpecialCategory,
                                                                                             SubActivityDate = be.SubActivityDate,
                                                                                             SubActivityID = be.SubActivityID,
                                                                                             SubActivityName = be.SubActivityName,
                                                                                             SubActivityText = be.SubActivityText,
                                                                                             SubActivityTime = be.SubActivityTime,
                                                                                             TypeID = be.TypeID,
                                                                                             Year = be.Year
                                                                                         }).Distinct().ToList();
                objectGenericBEValidation.Clear();
                objectGenericBEValidation.AddRange(objectGenericBEValidationGZero);
                objectGenericBEValidation.AddRange(objectGenericBEValidationZero);
                //objectGenericBECategoryProfile.ForEach(delegate(RMC.BusinessEntities.BECategoryProfile objectBECategoryProfile)
                //{
                //    objectGenericBEValidation.FindAll(delegate(RMC.BusinessEntities.BEValidation objectBEValidation)
                //    {
                //        if (objectBEValidation.SubActivityID > 0 && objectBECategoryProfile.SubActivityID > 0)
                //        {
                //            return objectBECategoryProfile.LocationID == objectBEValidation.LocationID && objectBECategoryProfile.ActivityID == objectBEValidation.ActivityID && objectBECategoryProfile.SubActivityID == objectBEValidation.SubActivityID;
                //        }
                //        else
                //        {
                //            return objectBECategoryProfile.LocationID == objectBEValidation.LocationID && objectBECategoryProfile.ActivityID == objectBEValidation.ActivityID;                            
                //        }
                //    }).TrueForAll(delegate(RMC.BusinessEntities.BEValidation objectBEValidationAnother)
                //    {
                //        if (objectBEValidationAnother != null)
                //        {
                //            objectBEValidationAnother.CategoryGroupID = objectBECategoryProfile.CategoryAssignmentID.Value;
                //        }
                //        else
                //        {
                //            totalCount++;
                //            objectBEValidationAnother.CategoryGroupID = 0;
                //        }
                //        return true;
                //    });
                //});

                objectGenericBEValidation = objectGenericBEValidation.Where(w => w.CategoryGroupID != 0).ToList();

                totalCount = objectGenericBEValidation.Count;
                if (HttpContext.Current.Session["CollaborationReportView"] == null || Convert.ToInt32(HttpContext.Current.Session["CollaborationReportView"]) == 1)
                {
                    #region OtherThenCollaborationReport or SummarizeCollaborationReportAllData
                    
                        if (profileCategory == "value added")
                        {
                            objectGenericBEReports = (from v in objectGenericBEValidation.GroupBy(o => o.HospitalUnitID).ToList()
                                                      from t in v.GroupBy(x => x.CategoryGroupID).ToList()
                                                      select new RMC.BusinessEntities.BEReports
                                                      {
                                                          Email = t.FirstOrDefault(r => r.CreatedBy != "").CreatedBy,
                                                          ColumnName = objectGenericValueAddedType.Find(delegate(RMC.DataService.ValueAddedType objectValueAddedType)
                                                          {
                                                              return objectValueAddedType.TypeID == t.Key;
                                                          }).TypeName + " (" + profileName + ")",
                                                          ColumnNumber = objectGenericValueAddedType.Find(delegate(RMC.DataService.ValueAddedType objectValueAddedType)
                                                          {
                                                              return objectValueAddedType.TypeID == t.Key;
                                                          }).TypeID,
                                                          //RowName = t.FirstOrDefault(r => r.HospitalUnitName != "").HospitalUnitIDCounter,
                                                          //RowName = "#" + t.FirstOrDefault(r => r.HospitalUnitName != "").RecordCounter.ToString(),
                                                          //RowName = t.Select(r => r.HospitalUnitIDCounter).FirstOrDefault(),
                                                          RowName = v.Select(x => x.HospitalUnitIDCounter).First() + MaintainSessions.SessionHospitalUnitCounterID.Find(x => x.HospitalUnitID == Convert.ToInt32(v.Key)).HospitalUnitSequence.ToString(),
                                                          Values = string.Format("{0:#.##}%", ((double)t.Count(r => r.CategoryGroupID == t.Key) / (double)v.Count(c => c.CategoryGroupID != 0)) * 100),
                                                          ValuesSum = ((double)t.Count(r => r.CategoryGroupID == t.Key) / (double)v.Count(c => c.CategoryGroupID != 0)) * 100,
                                                          DataPoint = (double)v.Count(c => c.CategoryGroupID != 0),

                                                          RecordCounter = t.FirstOrDefault(r => r.HospitalUnitName != "").RecordCounter
                                                      }).ToList();

                        }
                        else if (profileCategory == "activities")
                        {
                            objectGenericBEReports =
                                (from v in objectGenericBEValidation.GroupBy(o => o.HospitalUnitID).ToList()
                                 from t in v.GroupBy(x => x.CategoryGroupID).ToList()
                                 select new RMC.BusinessEntities.BEReports
                                 {
                                     Email = t.FirstOrDefault(r => r.CreatedBy != "").CreatedBy,
                                     ColumnName = objectGenericActivitiesCategory.Find(delegate(RMC.DataService.ActivitiesCategory objectActivities)
                                     {
                                         return objectActivities.ActivitiesID == t.Key;
                                     }).ActivitiesCategory1 + " (" + profileName + ")",
                                     ColumnNumber = objectGenericActivitiesCategory.Find(delegate(RMC.DataService.ActivitiesCategory objectActivities)
                                     {
                                         return objectActivities.ActivitiesID == t.Key;
                                     }).ActivitiesID,
                                     //RowName = t.FirstOrDefault(r => r.HospitalUnitName != "").HospitalUnitIDCounter,
                                     //RowName = "#" + t.FirstOrDefault(r => r.HospitalUnitName != "").RecordCounter.ToString(),
                                     //RowName = t.Select(r => r.HospitalUnitIDCounter).FirstOrDefault(),
                                     RowName = v.Select(x => x.HospitalUnitIDCounter).First() + MaintainSessions.SessionHospitalUnitCounterID.Find(x => x.HospitalUnitID == Convert.ToInt32(v.Key)).HospitalUnitSequence.ToString(),
                                     Values = string.Format("{0:#.##}%", ((double)t.Count(r => r.CategoryGroupID == t.Key) / (double)v.Count(c => c.CategoryGroupID != 0)) * 100),
                                     ValuesSum = ((double)t.Count(r => r.CategoryGroupID == t.Key) / (double)v.Count(c => c.CategoryGroupID != 0)) * 100,
                                     DataPoint = (double)v.Count(c => c.CategoryGroupID != 0),

                                     RecordCounter = t.FirstOrDefault(r => r.HospitalUnitName != "").RecordCounter
                                 }).ToList();
                        }
                        else if (profileCategory == "others")
                        {
                            objectGenericBEReports = (from v in objectGenericBEValidation.GroupBy(o => o.HospitalUnitID).ToList()
                                                      from t in v.GroupBy(x => x.CategoryGroupID).ToList()
                                                      select new RMC.BusinessEntities.BEReports
                                                      {
                                                          Email = t.FirstOrDefault(r => r.CreatedBy != "").CreatedBy,
                                                          ColumnName = objectGenericCategoryGroup.Find(delegate(RMC.DataService.CategoryGroup objectCategoryGroup)
                                                          {
                                                              return objectCategoryGroup.CategoryGroupID == t.Key;
                                                          }).CategoryGroup1 + " (" + profileName + ")",
                                                          ColumnNumber = objectGenericCategoryGroup.Find(delegate(RMC.DataService.CategoryGroup objectCategoryGroup)
                                                          {
                                                              return objectCategoryGroup.CategoryGroupID == t.Key;
                                                          }).CategoryGroupID,
                                                          //RowName = t.FirstOrDefault(r => r.HospitalUnitName != "").HospitalUnitName,
                                                          //RowName = t.FirstOrDefault(r => r.HospitalUnitName != "").HospitalUnitIDCounter,
                                                          RowName = v.Select(x => x.HospitalUnitIDCounter).First() + MaintainSessions.SessionHospitalUnitCounterID.Find(x => x.HospitalUnitID == Convert.ToInt32(v.Key)).HospitalUnitSequence.ToString(),
                                                          Values = string.Format("{0:#.##}%", ((double)t.Count(r => r.CategoryGroupID == t.Key) / (double)v.Count(c => c.CategoryGroupID != 0)) * 100),
                                                          ValuesSum = ((double)t.Count(r => r.CategoryGroupID == t.Key) / (double)v.Count(c => c.CategoryGroupID != 0)) * 100,
                                                          DataPoint = (double)v.Count(c => c.CategoryGroupID != 0),
                                                          RecordCounter = t.FirstOrDefault(r => r.HospitalUnitName != "").RecordCounter
                                                      }).ToList();


                        }
                        else if (profileCategory == "location")
                        {
                            objectGenericBEReports = (from v in objectGenericBEValidation.GroupBy(o => o.HospitalUnitID).ToList()
                                                      from t in v.GroupBy(x => x.CategoryGroupID).ToList()
                                                      select new RMC.BusinessEntities.BEReports
                                                      {
                                                          Email = t.FirstOrDefault(r => r.CreatedBy != "").CreatedBy,
                                                          ColumnName = objectGenericLocationCategory.Find(delegate(RMC.DataService.LocationCategory objectLocationCategory)
                                                          {
                                                              return objectLocationCategory.LocationID == t.Key;
                                                          }).LocationCategory1 + " (" + profileName + ")",
                                                          ColumnNumber = objectGenericLocationCategory.Find(delegate(RMC.DataService.LocationCategory objectLocationCategory)
                                                          {
                                                              return objectLocationCategory.LocationID == t.Key;
                                                          }).LocationID,
                                                          //RowName = t.FirstOrDefault(r => r.HospitalUnitName != "").HospitalUnitName,
                                                          //RowName = t.FirstOrDefault(r => r.HospitalUnitName != "").HospitalUnitIDCounter,
                                                          RowName = v.Select(x => x.HospitalUnitIDCounter).First() + MaintainSessions.SessionHospitalUnitCounterID.Find(x => x.HospitalUnitID == Convert.ToInt32(v.Key)).HospitalUnitSequence.ToString(),
                                                          Values = string.Format("{0:#.##}%", ((double)t.Count(r => r.CategoryGroupID == t.Key) / (double)v.Count(c => c.CategoryGroupID != 0)) * 100),
                                                          ValuesSum = ((double)t.Count(r => r.CategoryGroupID == t.Key) / (double)v.Count(c => c.CategoryGroupID != 0)) * 100,
                                                          DataPoint = (double)v.Count(c => c.CategoryGroupID != 0),
                                                          RecordCounter = t.FirstOrDefault(r => r.HospitalUnitName != "").RecordCounter
                                                      }).ToList();




                            //List<string> objectGenericHospitalUnitName = objectGenericBEReports.Select(s => s.RowName).Distinct().ToList();
                            //objectGenericLocationCategory.ForEach(delegate(RMC.DataService.LocationCategory objectLocationCategory)
                            //{
                            //    foreach (string objectHospitalUnitName in objectGenericHospitalUnitName)
                            //    {
                            //        if (!objectGenericBEReports.Exists(delegate(RMC.BusinessEntities.BEReports objectNewBEReport)
                            //        {
                            //            return objectHospitalUnitName == objectNewBEReport.RowName && objectLocationCategory.LocationCategory1 == objectNewBEReport.ColumnName;
                            //        }))
                            //        {
                            //            RMC.BusinessEntities.BEReports objectNewBEReports = new RMC.BusinessEntities.BEReports();

                            //            objectNewBEReports.RowName = objectHospitalUnitName;
                            //            objectNewBEReports.ColumnName = objectLocationCategory.LocationCategory1;
                            //            objectNewBEReports.ColumnNumber = objectLocationCategory.LocationID;
                            //            objectNewBEReports.Values = "0.00%";

                            //            objectGenericBEReports.Add(objectNewBEReports);
                            //        }
                            //    }
                            //});
                        }

                        //added by cm
                        else if (profileCategory == "activities")
                        {
                            objectGenericBEReports = (from v in objectGenericBEValidation.GroupBy(o => o.HospitalUnitID).ToList()
                                                      from t in v.GroupBy(x => x.CategoryGroupID).ToList()
                                                      select new RMC.BusinessEntities.BEReports
                                                      {
                                                          Email = t.FirstOrDefault(r => r.CreatedBy != "").CreatedBy,
                                                          ColumnName = objectGenericActivitiesCategory.Find(delegate(RMC.DataService.ActivitiesCategory objectActivitiesCategory)
                                                          {
                                                              return objectActivitiesCategory.ActivitiesID == t.Key;
                                                          }).ActivitiesCategory1 + " (" + profileName + ")",
                                                          ColumnNumber = objectGenericActivitiesCategory.Find(delegate(RMC.DataService.ActivitiesCategory objectActivitiesCategory)
                                                          {
                                                              return objectActivitiesCategory.ActivitiesID == t.Key;
                                                          }).ActivitiesID,
                                                          //RowName = t.FirstOrDefault(r => r.HospitalUnitName != "").HospitalUnitName,
                                                          //RowName = t.FirstOrDefault(r => r.HospitalUnitName != "").HospitalUnitIDCounter,
                                                          RowName = v.Select(x => x.HospitalUnitIDCounter).First() + MaintainSessions.SessionHospitalUnitCounterID.Find(x => x.HospitalUnitID == Convert.ToInt32(v.Key)).HospitalUnitSequence.ToString(),
                                                          Values = string.Format("{0:#.##}%", ((double)t.Count(r => r.CategoryGroupID == t.Key) / (double)v.Count(c => c.CategoryGroupID != 0)) * 100),
                                                          ValuesSum = ((double)t.Count(r => r.CategoryGroupID == t.Key) / (double)v.Count(c => c.CategoryGroupID != 0)) * 100,
                                                          DataPoint = (double)v.Count(c => c.CategoryGroupID != 0),
                                                          RecordCounter = t.FirstOrDefault(r => r.HospitalUnitName != "").RecordCounter
                                                      }).ToList();


                        }
                    #endregion
                }
                else
                {
                    #region SummarizeCollaborationReportViewByYear
                    if (Convert.ToInt32(HttpContext.Current.Session["CollaborationReportView"]) == 2) //SummarizeByYear
                    {
                        if (profileCategory == "value added")
                        {
                            objectGenericBEReports = (from v in objectGenericBEValidation.GroupBy(o => o.HospitalUnitID).ToList()
                                                      from w in v.GroupBy(r => r.Year).ToList()
                                                      from t in w.GroupBy(x => x.CategoryGroupID).ToList()
                                                      select new RMC.BusinessEntities.BEReports
                                                      {
                                                          Email = t.FirstOrDefault(r => r.CreatedBy != "").CreatedBy,
                                                          ColumnName = objectGenericValueAddedType.Find(delegate(RMC.DataService.ValueAddedType objectValueAddedType)
                                                          {
                                                              return objectValueAddedType.TypeID == t.Key;
                                                          }).TypeName + " (" + profileName + ")",
                                                          ColumnNumber = objectGenericValueAddedType.Find(delegate(RMC.DataService.ValueAddedType objectValueAddedType)
                                                          {
                                                              return objectValueAddedType.TypeID == t.Key;
                                                          }).TypeID,
                                                          //RowName = t.FirstOrDefault(r => r.HospitalUnitName != "").HospitalUnitIDCounter,
                                                          //RowName = "#" + t.FirstOrDefault(r => r.HospitalUnitName != "").RecordCounter.ToString(),
                                                          //RowName = t.Select(r => r.HospitalUnitIDCounter).FirstOrDefault(),
                                                          RowName = w.Select(x => x.HospitalUnitIDCounter).First() + MaintainSessions.SessionHospitalUnitCounterID.Find(x => x.HospitalUnitID == Convert.ToInt32(v.Key)).HospitalUnitSequence.ToString() + "_" + w.Key.ToString(),
                                                          Values = string.Format("{0:#.##}%", ((double)t.Count(r => r.CategoryGroupID == t.Key) / (double)w.Count(c => c.CategoryGroupID != 0)) * 100),
                                                          ValuesSum = ((double)t.Count(r => r.CategoryGroupID == t.Key) / (double)w.Count(c => c.CategoryGroupID != 0)) * 100,
                                                          DataPoint = (double)w.Count(c => c.CategoryGroupID != 0),

                                                          RecordCounter = t.FirstOrDefault(r => r.HospitalUnitName != "").RecordCounter
                                                      }).ToList();

                        }
                        else if (profileCategory == "activities")
                        {
                            objectGenericBEReports =
                                (from v in objectGenericBEValidation.GroupBy(o => o.HospitalUnitID).ToList()
                                 from w in v.GroupBy(r => r.Year).ToList()
                                 from t in w.GroupBy(x => x.CategoryGroupID).ToList()
                                 select new RMC.BusinessEntities.BEReports
                                 {
                                     Email = t.FirstOrDefault(r => r.CreatedBy != "").CreatedBy,
                                     ColumnName = objectGenericActivitiesCategory.Find(delegate(RMC.DataService.ActivitiesCategory objectActivities)
                                     {
                                         return objectActivities.ActivitiesID == t.Key;
                                     }).ActivitiesCategory1 + " (" + profileName + ")",
                                     ColumnNumber = objectGenericActivitiesCategory.Find(delegate(RMC.DataService.ActivitiesCategory objectActivities)
                                     {
                                         return objectActivities.ActivitiesID == t.Key;
                                     }).ActivitiesID,
                                     //RowName = t.FirstOrDefault(r => r.HospitalUnitName != "").HospitalUnitIDCounter,
                                     //RowName = "#" + t.FirstOrDefault(r => r.HospitalUnitName != "").RecordCounter.ToString(),
                                     //RowName = t.Select(r => r.HospitalUnitIDCounter).FirstOrDefault(),
                                     RowName = w.Select(x => x.HospitalUnitIDCounter).First() + MaintainSessions.SessionHospitalUnitCounterID.Find(x => x.HospitalUnitID == Convert.ToInt32(v.Key)).HospitalUnitSequence.ToString() + "_" + w.Key.ToString(),
                                     Values = string.Format("{0:#.##}%", ((double)t.Count(r => r.CategoryGroupID == t.Key) / (double)w.Count(c => c.CategoryGroupID != 0)) * 100),
                                     ValuesSum = ((double)t.Count(r => r.CategoryGroupID == t.Key) / (double)w.Count(c => c.CategoryGroupID != 0)) * 100,
                                     DataPoint = (double)w.Count(c => c.CategoryGroupID != 0),

                                     RecordCounter = t.FirstOrDefault(r => r.HospitalUnitName != "").RecordCounter
                                 }).ToList();
                        }
                        else if (profileCategory == "others")
                        {
                            objectGenericBEReports = (from v in objectGenericBEValidation.GroupBy(o => o.HospitalUnitID).ToList()
                                                      from w in v.GroupBy(r => r.Year).ToList()
                                                      from t in w.GroupBy(x => x.CategoryGroupID).ToList()
                                                      select new RMC.BusinessEntities.BEReports
                                                      {
                                                          Email = t.FirstOrDefault(r => r.CreatedBy != "").CreatedBy,
                                                          ColumnName = objectGenericCategoryGroup.Find(delegate(RMC.DataService.CategoryGroup objectCategoryGroup)
                                                          {
                                                              return objectCategoryGroup.CategoryGroupID == t.Key;
                                                          }).CategoryGroup1 + " (" + profileName + ")",
                                                          ColumnNumber = objectGenericCategoryGroup.Find(delegate(RMC.DataService.CategoryGroup objectCategoryGroup)
                                                          {
                                                              return objectCategoryGroup.CategoryGroupID == t.Key;
                                                          }).CategoryGroupID,
                                                          //RowName = t.FirstOrDefault(r => r.HospitalUnitName != "").HospitalUnitName,
                                                          //RowName = t.FirstOrDefault(r => r.HospitalUnitName != "").HospitalUnitIDCounter,
                                                          RowName = w.Select(x => x.HospitalUnitIDCounter).First() + MaintainSessions.SessionHospitalUnitCounterID.Find(x => x.HospitalUnitID == Convert.ToInt32(v.Key)).HospitalUnitSequence.ToString() + "_" + w.Key.ToString(),
                                                          Values = string.Format("{0:#.##}%", ((double)t.Count(r => r.CategoryGroupID == t.Key) / (double)w.Count(c => c.CategoryGroupID != 0)) * 100),
                                                          ValuesSum = ((double)t.Count(r => r.CategoryGroupID == t.Key) / (double)w.Count(c => c.CategoryGroupID != 0)) * 100,
                                                          DataPoint = (double)w.Count(c => c.CategoryGroupID != 0),
                                                          RecordCounter = t.FirstOrDefault(r => r.HospitalUnitName != "").RecordCounter
                                                      }).ToList();


                        }
                        else if (profileCategory == "location")
                        {
                            objectGenericBEReports = (from v in objectGenericBEValidation.GroupBy(o => o.HospitalUnitID).ToList()
                                                      from w in v.GroupBy(r => r.Year).ToList()
                                                      from t in w.GroupBy(x => x.CategoryGroupID).ToList()
                                                      select new RMC.BusinessEntities.BEReports
                                                      {
                                                          Email = t.FirstOrDefault(r => r.CreatedBy != "").CreatedBy,
                                                          ColumnName = objectGenericLocationCategory.Find(delegate(RMC.DataService.LocationCategory objectLocationCategory)
                                                          {
                                                              return objectLocationCategory.LocationID == t.Key;
                                                          }).LocationCategory1 + " (" + profileName + ")",
                                                          ColumnNumber = objectGenericLocationCategory.Find(delegate(RMC.DataService.LocationCategory objectLocationCategory)
                                                          {
                                                              return objectLocationCategory.LocationID == t.Key;
                                                          }).LocationID,
                                                          //RowName = t.FirstOrDefault(r => r.HospitalUnitName != "").HospitalUnitName,
                                                          //RowName = t.FirstOrDefault(r => r.HospitalUnitName != "").HospitalUnitIDCounter,
                                                          RowName = w.Select(x => x.HospitalUnitIDCounter).First() + MaintainSessions.SessionHospitalUnitCounterID.Find(x => x.HospitalUnitID == Convert.ToInt32(v.Key)).HospitalUnitSequence.ToString() + "_" + w.Key.ToString(),
                                                          Values = string.Format("{0:#.##}%", ((double)t.Count(r => r.CategoryGroupID == t.Key) / (double)w.Count(c => c.CategoryGroupID != 0)) * 100),
                                                          ValuesSum = ((double)t.Count(r => r.CategoryGroupID == t.Key) / (double)w.Count(c => c.CategoryGroupID != 0)) * 100,
                                                          DataPoint = (double)w.Count(c => c.CategoryGroupID != 0),
                                                          RecordCounter = t.FirstOrDefault(r => r.HospitalUnitName != "").RecordCounter
                                                      }).ToList();




                            //List<string> objectGenericHospitalUnitName = objectGenericBEReports.Select(s => s.RowName).Distinct().ToList();
                            //objectGenericLocationCategory.ForEach(delegate(RMC.DataService.LocationCategory objectLocationCategory)
                            //{
                            //    foreach (string objectHospitalUnitName in objectGenericHospitalUnitName)
                            //    {
                            //        if (!objectGenericBEReports.Exists(delegate(RMC.BusinessEntities.BEReports objectNewBEReport)
                            //        {
                            //            return objectHospitalUnitName == objectNewBEReport.RowName && objectLocationCategory.LocationCategory1 == objectNewBEReport.ColumnName;
                            //        }))
                            //        {
                            //            RMC.BusinessEntities.BEReports objectNewBEReports = new RMC.BusinessEntities.BEReports();

                            //            objectNewBEReports.RowName = objectHospitalUnitName;
                            //            objectNewBEReports.ColumnName = objectLocationCategory.LocationCategory1;
                            //            objectNewBEReports.ColumnNumber = objectLocationCategory.LocationID;
                            //            objectNewBEReports.Values = "0.00%";

                            //            objectGenericBEReports.Add(objectNewBEReports);
                            //        }
                            //    }
                            //});
                        }

                        //added by cm
                        else if (profileCategory == "activities")
                        {
                            objectGenericBEReports = (from v in objectGenericBEValidation.GroupBy(o => o.HospitalUnitID).ToList()
                                                      from w in v.GroupBy(r => r.Year).ToList()
                                                      from t in w.GroupBy(x => x.CategoryGroupID).ToList()
                                                      select new RMC.BusinessEntities.BEReports
                                                      {
                                                          Email = t.FirstOrDefault(r => r.CreatedBy != "").CreatedBy,
                                                          ColumnName = objectGenericActivitiesCategory.Find(delegate(RMC.DataService.ActivitiesCategory objectActivitiesCategory)
                                                          {
                                                              return objectActivitiesCategory.ActivitiesID == t.Key;
                                                          }).ActivitiesCategory1 + " (" + profileName + ")",
                                                          ColumnNumber = objectGenericActivitiesCategory.Find(delegate(RMC.DataService.ActivitiesCategory objectActivitiesCategory)
                                                          {
                                                              return objectActivitiesCategory.ActivitiesID == t.Key;
                                                          }).ActivitiesID,
                                                          //RowName = t.FirstOrDefault(r => r.HospitalUnitName != "").HospitalUnitName,
                                                          //RowName = t.FirstOrDefault(r => r.HospitalUnitName != "").HospitalUnitIDCounter,
                                                          RowName = w.Select(x => x.HospitalUnitIDCounter).First() + MaintainSessions.SessionHospitalUnitCounterID.Find(x => x.HospitalUnitID == Convert.ToInt32(v.Key)).HospitalUnitSequence.ToString() + "_" + w.Key.ToString(),
                                                          Values = string.Format("{0:#.##}%", ((double)t.Count(r => r.CategoryGroupID == t.Key) / (double)w.Count(c => c.CategoryGroupID != 0)) * 100),
                                                          ValuesSum = ((double)t.Count(r => r.CategoryGroupID == t.Key) / (double)w.Count(c => c.CategoryGroupID != 0)) * 100,
                                                          DataPoint = (double)w.Count(c => c.CategoryGroupID != 0),
                                                          RecordCounter = t.FirstOrDefault(r => r.HospitalUnitName != "").RecordCounter
                                                      }).ToList();


                        }
                    }
                    #endregion
                }
                        

                if (dataPointsFrom != null && dataPointsTo == null)
                {
                    if (optDataPointsFrom == 1)
                    {
                        objectGenericBEReports = objectGenericBEReports.Where(x => Convert.ToInt32(x.DataPoint) < dataPointsFrom).ToList();
                    }
                    if (optDataPointsFrom == 2)
                    {
                        objectGenericBEReports = objectGenericBEReports.Where(x => x.DataPoint > dataPointsFrom).ToList();
                    }
                    if (optDataPointsFrom == 3)
                    {
                        objectGenericBEReports = objectGenericBEReports.Where(x => x.DataPoint == dataPointsFrom).ToList();
                    }
                    if (optDataPointsFrom == 4)
                    {
                        objectGenericBEReports = objectGenericBEReports.Where(x => x.DataPoint >= dataPointsFrom).ToList();
                    }
                    if (optDataPointsFrom == 5)
                    {
                        objectGenericBEReports = objectGenericBEReports.Where(x => x.DataPoint <= dataPointsFrom).ToList();
                    }
                    if (optDataPointsFrom == 6)
                    {
                        objectGenericBEReports = objectGenericBEReports.Where(x => x.DataPoint != dataPointsFrom).ToList();
                    }
                }

                if ((optDataPointsFrom == 0 && optdataPointsTo == 0) && (dataPointsFrom != null && dataPointsTo != null))
                {
                    objectGenericBEReports = objectGenericBEReports.Where(x => x.DataPoint >= dataPointsFrom && x.DataPoint <= dataPointsTo).ToList();
                }

                //return objectGenericBEReports.OrderBy(x => x.RecordCounter).ToList();
                return objectGenericBEReports.OrderBy(x => x.ColumnName).ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Get Data for Hospital Benchmark Report
        /// </summary>
        public List<RMC.BusinessEntities.BEReports> GetDataForHospitalBenchmarkSummaryTest(string activitiesID, string valueAddedCategoryID, string OthersCategoryID, string LocationCategoryID, int? hospitalUnitID, int? firstYear, int? lastYear, int? firstMonth, int? lastMonth, int? bedInUnitFrom, int optBedInUnitFrom, int? bedInUnitTo, int optBedInUnitTo, float? budgetedPatientFrom, int optBudgetedPatientFrom, float? budgetedPatientTo, int optBudgetedPatientTo, string startDate, string endDate, int? electronicDocumentFrom, int optElectronicDocumentFrom, int? electronicDocumentTo, int optElectronicDocumentTo, int docByException, string unitType, string pharmacyType, string hospitalType, int optHospitalSizeFrom, int? hospitalSizeFrom, int optHospitalSizeTo, int? hospitalSizeTo, int? countryId, int? stateId, string activities, string value, string others, string location, int? dataPointsFrom, int optDataPointsFrom, int? dataPointsTo, int optdataPointsTo, string configName, string unitIds)
        {
            try
            {
                List<RMC.BusinessEntities.BEReports> objectGenericListBEReportValueAddedProfile = null;
                List<RMC.BusinessEntities.BEReports> objectGenericListBEReportValueAdded = null;
                List<RMC.BusinessEntities.BEReports> objectGenericListBEReportOthers = null;
                List<RMC.BusinessEntities.BEReports> objectGenericListBEReportLocation = null;

                List<RMC.BusinessEntities.BEValidation> ObjectSearchHospitalsDataForBenchmark = SearchHospitalsDataForBenchmarkTest(hospitalUnitID, firstYear, lastYear, firstMonth, lastMonth, bedInUnitFrom, optBedInUnitFrom, bedInUnitTo, optBedInUnitTo, budgetedPatientFrom, optBudgetedPatientFrom, budgetedPatientTo, optBudgetedPatientTo, startDate, endDate, electronicDocumentFrom, optElectronicDocumentFrom, electronicDocumentTo, optElectronicDocumentTo, docByException, unitType, pharmacyType, hospitalType, optHospitalSizeFrom, hospitalSizeFrom, optHospitalSizeTo, hospitalSizeTo, countryId, stateId, configName, unitIds);

                //activities
                if (activitiesID != null)
                {
                    if (activitiesID.Contains(","))
                    {
                        string[] strArrvalue = null;
                        strArrvalue = activities.Split(new char[] { ',' });
                        string[] strArrvalueAddedCategoryID = null;
                        strArrvalueAddedCategoryID = activitiesID.Split(new char[] { ',' });
                        int[] intArrvalueAddedCategoryID = new int[strArrvalueAddedCategoryID.Length];
                        for (int i = 0; i < strArrvalueAddedCategoryID.Length; i++)
                        {
                            intArrvalueAddedCategoryID[i] = int.Parse(strArrvalueAddedCategoryID[i]);
                            objectGenericListBEReportValueAddedProfile = CalculationOnDataByProfileTest(strArrvalue[i], "activities", GetCategoryProfileDataByProfileID(intArrvalueAddedCategoryID[i]), ObjectSearchHospitalsDataForBenchmark, dataPointsFrom, optDataPointsFrom, dataPointsTo, optdataPointsTo);
                            if (i == 0)
                            {
                                objectGenericListBEReportValueAdded = objectGenericListBEReportValueAddedProfile;
                            }
                            if (i != 0)
                            {
                                objectGenericListBEReportValueAdded.AddRange(objectGenericListBEReportValueAddedProfile);
                            }
                        }
                    }
                    else
                    {
                        objectGenericListBEReportValueAdded = CalculationOnDataByProfileTest(activities, "activities", GetCategoryProfileDataByProfileID(Convert.ToInt32(activitiesID)), ObjectSearchHospitalsDataForBenchmark, dataPointsFrom, optDataPointsFrom, dataPointsTo, optdataPointsTo);
                    }
                }
                //valueAddedCategoryID
                if (valueAddedCategoryID != null)
                {
                    if (valueAddedCategoryID.Contains(","))
                    {
                        string[] strArrvalue = null;
                        strArrvalue = value.Split(new char[] { ',' });
                        string[] strArrvalueAddedCategoryID = null;
                        strArrvalueAddedCategoryID = valueAddedCategoryID.Split(new char[] { ',' });
                        int[] intArrvalueAddedCategoryID = new int[strArrvalueAddedCategoryID.Length];
                        for (int i = 0; i < strArrvalueAddedCategoryID.Length; i++)
                        {
                            intArrvalueAddedCategoryID[i] = int.Parse(strArrvalueAddedCategoryID[i]);
                            objectGenericListBEReportValueAddedProfile = CalculationOnDataByProfileTest(strArrvalue[i], "value added", GetCategoryProfileDataByProfileID(intArrvalueAddedCategoryID[i]), ObjectSearchHospitalsDataForBenchmark, dataPointsFrom, optDataPointsFrom, dataPointsTo, optdataPointsTo);
                            if (i == 0)
                            {
                                objectGenericListBEReportValueAdded = objectGenericListBEReportValueAddedProfile;
                            }
                            if (i != 0)
                            {
                                objectGenericListBEReportValueAdded.AddRange(objectGenericListBEReportValueAddedProfile);
                            }
                        }
                    }
                    else
                    {
                        objectGenericListBEReportValueAdded = CalculationOnDataByProfileTest(value, "value added", GetCategoryProfileDataByProfileID(Convert.ToInt32(valueAddedCategoryID)), ObjectSearchHospitalsDataForBenchmark, dataPointsFrom, optDataPointsFrom, dataPointsTo, optdataPointsTo);
                    }
                }
                //OthersCategoryID
                if (OthersCategoryID != null)
                {
                    if (OthersCategoryID.Contains(","))
                    {
                        string[] strArrothers = null;
                        strArrothers = others.Split(new char[] { ',' });
                        string[] strArrOthersCategoryID = null;
                        strArrOthersCategoryID = OthersCategoryID.Split(new char[] { ',' });
                        int[] intArrOthersCategoryID = new int[strArrOthersCategoryID.Length];
                        for (int i = 0; i < strArrOthersCategoryID.Length; i++)
                        {
                            intArrOthersCategoryID[i] = int.Parse(strArrOthersCategoryID[i]);
                            objectGenericListBEReportOthers = CalculationOnDataByProfileTest(strArrothers[i], "others", GetCategoryProfileDataByProfileID(intArrOthersCategoryID[i]), ObjectSearchHospitalsDataForBenchmark, dataPointsFrom, optDataPointsFrom, dataPointsTo, optdataPointsTo);
                            //objectGenericListBEReportValueAdded.AddRange(objectGenericListBEReportOthers);
                            if (objectGenericListBEReportValueAdded == null)
                            {
                                objectGenericListBEReportValueAdded = objectGenericListBEReportOthers;
                            }
                            else
                            {
                                objectGenericListBEReportValueAdded.AddRange(objectGenericListBEReportOthers);
                            }
                        }
                    }
                    else
                    {
                        objectGenericListBEReportOthers = CalculationOnDataByProfileTest(others, "others", GetCategoryProfileDataByProfileID(Convert.ToInt32(OthersCategoryID)), ObjectSearchHospitalsDataForBenchmark, dataPointsFrom, optDataPointsFrom, dataPointsTo, optdataPointsTo);
                        //objectGenericListBEReportValueAdded.AddRange(objectGenericListBEReportOthers);
                        if (objectGenericListBEReportValueAdded == null)
                        {
                            objectGenericListBEReportValueAdded = objectGenericListBEReportOthers;
                        }
                        else
                        {
                            objectGenericListBEReportValueAdded.AddRange(objectGenericListBEReportOthers);
                        }
                    }
                }
                //LocationCategoryID;
                if (LocationCategoryID != null)
                {
                    if (LocationCategoryID.Contains(","))
                    {
                        string[] strArrLocation = null;
                        strArrLocation = location.Split(new char[] { ',' });
                        string[] strArrLocationCategoryID = null;
                        strArrLocationCategoryID = LocationCategoryID.Split(new char[] { ',' });
                        int[] intArrLocationCategoryID = new int[strArrLocationCategoryID.Length];
                        for (int i = 0; i < strArrLocationCategoryID.Length; i++)
                        {
                            intArrLocationCategoryID[i] = int.Parse(strArrLocationCategoryID[i]);
                            objectGenericListBEReportLocation = CalculationOnDataByProfileTest(strArrLocation[i], "location", GetCategoryProfileDataByProfileID(intArrLocationCategoryID[i]), ObjectSearchHospitalsDataForBenchmark, dataPointsFrom, optDataPointsFrom, dataPointsTo, optdataPointsTo);
                            //objectGenericListBEReportValueAdded.AddRange(objectGenericListBEReportLocation);
                            if (objectGenericListBEReportValueAdded == null)
                            {
                                objectGenericListBEReportValueAdded = objectGenericListBEReportLocation;
                            }
                            else
                            {
                                objectGenericListBEReportValueAdded.AddRange(objectGenericListBEReportLocation);
                            }
                        }
                    }
                    else
                    {
                        objectGenericListBEReportLocation = CalculationOnDataByProfileTest(location, "location", GetCategoryProfileDataByProfileID(Convert.ToInt32(LocationCategoryID)), ObjectSearchHospitalsDataForBenchmark, dataPointsFrom, optDataPointsFrom, dataPointsTo, optdataPointsTo);
                        //objectGenericListBEReportValueAdded.AddRange(objectGenericListBEReportLocation);
                        if (objectGenericListBEReportValueAdded == null)
                        {
                            objectGenericListBEReportValueAdded = objectGenericListBEReportLocation;
                        }
                        else
                        {
                            objectGenericListBEReportValueAdded.AddRange(objectGenericListBEReportLocation);
                        }
                    }
                }

                return objectGenericListBEReportValueAdded;

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                //DisposeGlobalObjects();
            }
        }
        //changes by cm
        public List<RMC.BusinessEntities.BEReports> GetDataForHospitalBenchmarkSummaryTestNew(string dbValues, string ProfileCategoryValue, string ProfileSubCategoryValue, string valueAddedCategoryID, string OthersCategoryID, string LocationCategoryID, int? hospitalUnitID, int? firstYear, int? lastYear, int? firstMonth, int? lastMonth, int? bedInUnitFrom, int optBedInUnitFrom, int? bedInUnitTo, int optBedInUnitTo, float? budgetedPatientFrom, int optBudgetedPatientFrom, float? budgetedPatientTo, int optBudgetedPatientTo, string startDate, string endDate, int? electronicDocumentFrom, int optElectronicDocumentFrom, int? electronicDocumentTo, int optElectronicDocumentTo, int docByException, string unitType, string pharmacyType, string hospitalType, int optHospitalSizeFrom, int? hospitalSizeFrom, int optHospitalSizeTo, int? hospitalSizeTo, int? countryId, int? stateId, string activities, string value, string others, string location, int? dataPointsFrom, int optDataPointsFrom, int? dataPointsTo, int optdataPointsTo, string configName, string unitIds)
        {
            try
            {
                List<RMC.BusinessEntities.BEReports> objectGenericListBEReportValueAddedProfile = null;                
                List<RMC.BusinessEntities.BEReports> objectGenericListBEReportValueAdded = null;
                List<RMC.BusinessEntities.BEReports> objectGenericListBEReportOthers = null;
                List<RMC.BusinessEntities.BEReports> objectGenericListBEReportLocation = null;

                List<RMC.BusinessEntities.BEValidation> ObjectSearchHospitalsDataForBenchmark = SearchHospitalsDataForBenchmarkTest(hospitalUnitID, firstYear, lastYear, firstMonth, lastMonth, bedInUnitFrom, optBedInUnitFrom, bedInUnitTo, optBedInUnitTo, budgetedPatientFrom, optBudgetedPatientFrom, budgetedPatientTo, optBudgetedPatientTo, startDate, endDate, electronicDocumentFrom, optElectronicDocumentFrom, electronicDocumentTo, optElectronicDocumentTo, docByException, unitType, pharmacyType, hospitalType, optHospitalSizeFrom, hospitalSizeFrom, optHospitalSizeTo, hospitalSizeTo, countryId, stateId, configName,unitIds);

                if (ProfileCategoryValue != "Database Values" && ProfileCategoryValue != "Special Category")
                {
                    //valueAddedCategoryID
                    if (valueAddedCategoryID != null)
                    {
                        if (valueAddedCategoryID.Contains(","))
                        {
                            string[] strArrvalue = null;
                            strArrvalue = value.Split(new char[] { ',' });
                            string[] strArrvalueAddedCategoryID = null;
                            strArrvalueAddedCategoryID = valueAddedCategoryID.Split(new char[] { ',' });
                            int[] intArrvalueAddedCategoryID = new int[strArrvalueAddedCategoryID.Length];
                            for (int i = 0; i < strArrvalueAddedCategoryID.Length; i++)
                            {
                                intArrvalueAddedCategoryID[i] = int.Parse(strArrvalueAddedCategoryID[i]);
                                objectGenericListBEReportValueAddedProfile = CalculationOnDataByProfileTest(strArrvalue[i], "value added", GetCategoryProfileDataByProfileID(intArrvalueAddedCategoryID[i]), ObjectSearchHospitalsDataForBenchmark, dataPointsFrom, optDataPointsFrom, dataPointsTo, optdataPointsTo);
                                if (i == 0)
                                {
                                    objectGenericListBEReportValueAdded = objectGenericListBEReportValueAddedProfile;
                                }
                                if (i != 0)
                                {
                                    objectGenericListBEReportValueAdded.AddRange(objectGenericListBEReportValueAddedProfile);
                                }
                            }
                        }
                        else
                        {
                            objectGenericListBEReportValueAdded = CalculationOnDataByProfileTest(value, "value added", GetCategoryProfileDataByProfileID(Convert.ToInt32(valueAddedCategoryID)), ObjectSearchHospitalsDataForBenchmark, dataPointsFrom, optDataPointsFrom, dataPointsTo, optdataPointsTo);
                        }
                    }
                    //OthersCategoryID
                    if (OthersCategoryID != null)
                    {
                        if (OthersCategoryID.Contains(","))
                        {
                            string[] strArrothers = null;
                            strArrothers = others.Split(new char[] { ',' });
                            string[] strArrOthersCategoryID = null;
                            strArrOthersCategoryID = OthersCategoryID.Split(new char[] { ',' });
                            int[] intArrOthersCategoryID = new int[strArrOthersCategoryID.Length];
                            for (int i = 0; i < strArrOthersCategoryID.Length; i++)
                            {
                                intArrOthersCategoryID[i] = int.Parse(strArrOthersCategoryID[i]);
                                objectGenericListBEReportOthers = CalculationOnDataByProfileTest(strArrothers[i], "others", GetCategoryProfileDataByProfileID(intArrOthersCategoryID[i]), ObjectSearchHospitalsDataForBenchmark, dataPointsFrom, optDataPointsFrom, dataPointsTo, optdataPointsTo);
                                //objectGenericListBEReportValueAdded.AddRange(objectGenericListBEReportOthers);
                                if (objectGenericListBEReportValueAdded == null)
                                {
                                    objectGenericListBEReportValueAdded = objectGenericListBEReportOthers;
                                }
                                else
                                {
                                    objectGenericListBEReportValueAdded.AddRange(objectGenericListBEReportOthers);
                                }
                            }
                        }
                        else
                        {
                            objectGenericListBEReportOthers = CalculationOnDataByProfileTest(others, "others", GetCategoryProfileDataByProfileID(Convert.ToInt32(OthersCategoryID)), ObjectSearchHospitalsDataForBenchmark, dataPointsFrom, optDataPointsFrom, dataPointsTo, optdataPointsTo);
                            //objectGenericListBEReportValueAdded.AddRange(objectGenericListBEReportOthers);
                            if (objectGenericListBEReportValueAdded == null)
                            {
                                objectGenericListBEReportValueAdded = objectGenericListBEReportOthers;
                            }
                            else
                            {
                                objectGenericListBEReportValueAdded.AddRange(objectGenericListBEReportOthers);
                            }
                        }
                    }
                    //LocationCategoryID;
                    //if (LocationCategoryID != null)
                    if (ProfileCategoryValue == "Location")
                    {
                        if (LocationCategoryID.Contains(","))
                        {
                            string[] strArrLocation = null;
                            strArrLocation = location.Split(new char[] { ',' });
                            string[] strArrLocationCategoryID = null;
                            strArrLocationCategoryID = LocationCategoryID.Split(new char[] { ',' });
                            int[] intArrLocationCategoryID = new int[strArrLocationCategoryID.Length];
                            for (int i = 0; i < strArrLocationCategoryID.Length; i++)
                            {
                                intArrLocationCategoryID[i] = int.Parse(strArrLocationCategoryID[i]);
                                objectGenericListBEReportLocation = CalculationOnDataByProfileTest(strArrLocation[i], "location", GetCategoryProfileDataByProfileID(intArrLocationCategoryID[i]), ObjectSearchHospitalsDataForBenchmark, dataPointsFrom, optDataPointsFrom, dataPointsTo, optdataPointsTo);
                                //objectGenericListBEReportValueAdded.AddRange(objectGenericListBEReportLocation);
                                if (objectGenericListBEReportValueAdded == null)
                                {
                                    objectGenericListBEReportValueAdded = objectGenericListBEReportLocation;
                                }
                                else
                                {
                                    objectGenericListBEReportValueAdded.AddRange(objectGenericListBEReportLocation);
                                }
                            }
                        }
                        else
                        {
                            objectGenericListBEReportLocation = CalculationOnDataByProfileTest(location, "location", GetCategoryProfileDataByProfileID(Convert.ToInt32(LocationCategoryID)), ObjectSearchHospitalsDataForBenchmark, dataPointsFrom, optDataPointsFrom, dataPointsTo, optdataPointsTo);
                            //objectGenericListBEReportValueAdded.AddRange(objectGenericListBEReportLocation);
                            if (objectGenericListBEReportValueAdded == null)
                            {
                                objectGenericListBEReportValueAdded = objectGenericListBEReportLocation;
                            }
                            else
                            {
                                objectGenericListBEReportValueAdded.AddRange(objectGenericListBEReportLocation);
                            }
                        }
                    }
                    //for Activity
                    if (ProfileCategoryValue == "Activities")
                    {
                        if (LocationCategoryID.Contains(","))
                        {
                            string[] strArrLocation = null;
                            strArrLocation = activities.Split(new char[] { ',' });
                            string[] strArrLocationCategoryID = null;
                            strArrLocationCategoryID = LocationCategoryID.Split(new char[] { ',' });
                            int[] intArrLocationCategoryID = new int[strArrLocationCategoryID.Length];
                            for (int i = 0; i < strArrLocationCategoryID.Length; i++)
                            {
                                intArrLocationCategoryID[i] = int.Parse(strArrLocationCategoryID[i]);
                                objectGenericListBEReportLocation = CalculationOnDataByProfileTest(strArrLocation[i], "activities", GetCategoryProfileDataByProfileID(intArrLocationCategoryID[i]), ObjectSearchHospitalsDataForBenchmark, dataPointsFrom, optDataPointsFrom, dataPointsTo, optdataPointsTo);
                                //objectGenericListBEReportValueAdded.AddRange(objectGenericListBEReportLocation);
                                if (objectGenericListBEReportValueAdded == null)
                                {
                                    objectGenericListBEReportValueAdded = objectGenericListBEReportLocation;
                                }
                                else
                                {
                                    objectGenericListBEReportValueAdded.AddRange(objectGenericListBEReportLocation);
                                }
                            }
                        }
                        else
                        {
                            objectGenericListBEReportLocation = CalculationOnDataByProfileTest(activities, "activities", GetCategoryProfileDataByProfileID(Convert.ToInt32(LocationCategoryID)), ObjectSearchHospitalsDataForBenchmark, dataPointsFrom, optDataPointsFrom, dataPointsTo, optdataPointsTo);
                            //objectGenericListBEReportValueAdded.AddRange(objectGenericListBEReportLocation);
                            if (objectGenericListBEReportValueAdded == null)
                            {
                                objectGenericListBEReportValueAdded = objectGenericListBEReportLocation;
                            }
                            else
                            {
                                objectGenericListBEReportValueAdded.AddRange(objectGenericListBEReportLocation);
                            }
                        }
                    }
                    //end
                }
                ////
                //======================For Special Category Type
                else if (ProfileCategoryValue == "Special Category")
                {
                    List<RMC.BusinessEntities.BEValidation> objectGenericBEValidation = SearchHospitalsDataForSpecialCategory(hospitalUnitID, firstYear, lastYear, firstMonth, lastMonth, null, 0, null, 0, null, 0, null, 0, startDate, endDate, null, 0, null, 0, docByException, unitType, pharmacyType, null, 0, null, 0, null, null, null, null);
                    List<RMC.BusinessEntities.BEValidation> objectGenericBEValidationAll = SearchHospitalsDataForSpecialCategory(null, firstYear, lastYear, firstMonth, lastMonth, null, 0, null, 0, null, 0, null, 0, startDate, endDate, null, 0, null, 0, docByException, unitType, pharmacyType, null, 0, null, 0, null, null, null, null);
                    List<RMC.DataService.NursePDASpecialType> objectGenericNursePDASpecialType = _objectRMCDataContext.NursePDASpecialTypes.ToList();
                    List<RMC.BusinessEntities.BEReports> objectNewGenericBEReports = null;
                    List<RMC.BusinessEntities.BEFunctionNames> objectGenericBEFunctionNames = new List<RMC.BusinessEntities.BEFunctionNames>();

                    objectGenericListBEReportValueAdded = (from v in objectGenericBEValidation.OrderBy(o => o.Year).ThenBy(p => p.MonthIndex)
                                                           where v.SpecialCategory == ProfileSubCategoryValue
                                               group v by new { v.Year, v.MonthIndex } into g
                                               from t in g.GroupBy(x => x.SpecialActivity).ToList()
                                               select new RMC.BusinessEntities.BEReports
                                               {
                                                   ColumnName = objectGenericNursePDASpecialType.Find(delegate(RMC.DataService.NursePDASpecialType objectNursePDASpecialType)
                                                   {
                                                       return objectNursePDASpecialType.SpecialActivity == t.Key;
                                                   }).SpecialActivity,
                                                   ColumnNumber = 1,
                                                   MonthName = BSCommon.GetMonthName(t.FirstOrDefault(r => r.Month != "").Month) + " " + t.FirstOrDefault().Year,
                                                   RowName = t.FirstOrDefault(r => r.HospitalUnitName != "").HospitalUnitName,
                                                   Values = string.Format("{0:#.##}%", ((double)t.Count(r => r.SpecialActivity == t.Key) / (double)g.Count(c => c.SpecialActivity != "")) * 100),
                                                   ValuesSum = Convert.ToDouble(string.Format("{0:#.##}", ((double)t.Count(r => r.SpecialActivity == t.Key) / (double)g.Count(c => c.SpecialActivity != "")) * 100)),
                                                   //DataPoint = (double)g.Count(c => c.SpecialActivity != "")
                                               }).ToList();

                    objectGenericListBEReportValueAdded = objectGenericListBEReportValueAdded.FindAll(delegate(RMC.BusinessEntities.BEReports objectBEReport)
                    {
                        //return objectBEReport.ColumnName.ToLower().Trim() == dbValues.ToLower().Trim();
                        return true;
                    });

                    objectGenericBEValidationAll = objectGenericBEValidationAll.FindAll(delegate(RMC.BusinessEntities.BEValidation objectNewBEValidation)
                    {
                        if (objectNewBEValidation.SpecialCategory != null)
                        {
                            return objectNewBEValidation.SpecialCategory.ToLower().Trim() == ProfileSubCategoryValue.ToLower().Trim();
                        }
                        else { return false; }

                    });

                    objectNewGenericBEReports = (from v in objectGenericBEValidationAll.GroupBy(o => o.HospitalUnitID).ToList()
                                                 from t in v.GroupBy(x => x.SpecialActivity).ToList()
                                                 select new RMC.BusinessEntities.BEReports
                                                 {
                                                     Name = "Special Category",
                                                     ColumnName = objectGenericNursePDASpecialType.Find(delegate(RMC.DataService.NursePDASpecialType objectNursePDASpecialType)
                                                     {
                                                         return objectNursePDASpecialType.SpecialActivity == t.Key;
                                                     }).SpecialActivity,
                                                     //ColumnName = "Deliver FoodTray",
                                                     ColumnNumber = 1,
                                                     MonthName = BSCommon.GetMonthName(t.FirstOrDefault(r => r.Month != "").Month) + " " + t.FirstOrDefault().Year,
                                                     RowName = t.FirstOrDefault(r => r.HospitalUnitName != "").HospitalUnitName,
                                                     Values = string.Format("{0:#.##}%", ((double)t.Count(r => r.SpecialActivity == t.Key) / (double)v.Count(c => c.SpecialActivity != "")) * 100),
                                                     ValuesSum = Convert.ToDouble(string.Format("{0:#.##}", ((double)t.Count(r => r.SpecialActivity == t.Key) / (double)v.Count(c => c.SpecialActivity != "")) * 100)),
                                                     DataPoint = (double)v.Count(c => c.SpecialActivity != ""),
                                                 }).ToList();


                    objectNewGenericBEReports = objectNewGenericBEReports.FindAll(delegate(RMC.BusinessEntities.BEReports objectBEReport)
                    {
                        //return objectBEReport.ColumnName.ToLower().Trim() == dbValues.ToLower().Trim();
                        return true;
                    });

                    if (objectNewGenericBEReports != null)
                    {
                        //filteration according to DataPoints
                        if (dataPointsFrom != null && dataPointsTo == null)
                        {
                            if (optDataPointsFrom == 1)
                            {
                                objectNewGenericBEReports = objectNewGenericBEReports.Where(x => Convert.ToInt32(x.DataPoint) < dataPointsFrom).ToList();
                            }
                            if (optDataPointsFrom == 2)
                            {
                                objectNewGenericBEReports = objectNewGenericBEReports.Where(x => x.DataPoint > dataPointsFrom).ToList();
                            }
                            if (optDataPointsFrom == 3)
                            {
                                objectNewGenericBEReports = objectNewGenericBEReports.Where(x => x.DataPoint == dataPointsFrom).ToList();
                            }
                            if (optDataPointsFrom == 4)
                            {
                                objectNewGenericBEReports = objectNewGenericBEReports.Where(x => x.DataPoint >= dataPointsFrom).ToList();
                            }
                            if (optDataPointsFrom == 5)
                            {
                                objectNewGenericBEReports = objectNewGenericBEReports.Where(x => x.DataPoint <= dataPointsFrom).ToList();
                            }
                            if (optDataPointsFrom == 6)
                            {
                                objectNewGenericBEReports = objectNewGenericBEReports.Where(x => x.DataPoint != dataPointsFrom).ToList();
                            }
                        }

                        if ((optDataPointsFrom == 0 && optdataPointsTo == 0) && (dataPointsFrom != null && dataPointsTo != null))
                        {
                            objectNewGenericBEReports = objectNewGenericBEReports.Where(x => x.DataPoint >= dataPointsFrom && x.DataPoint <= dataPointsTo).ToList();
                        }

                        foreach (var r in objectNewGenericBEReports.GroupBy(o => o.ColumnName))
                        {
                            //Calculate Minimum value.
                            RMC.BusinessEntities.BEFunctionNames objectNewBEFunctionNamesMin = new RMC.BusinessEntities.BEFunctionNames();
                            double valueMin = 0;
                            valueMin = r.Select(s => Convert.ToDouble((s.Values.Length > 0) ? s.Values.Substring(0, s.Values.Length - 1) : "0")).Min();

                            objectNewBEFunctionNamesMin.ColumnName = r.FirstOrDefault().ColumnName;
                            objectNewBEFunctionNamesMin.FunctionName = "Minimum";
                            objectNewBEFunctionNamesMin.FunctionNameDouble = valueMin;
                            objectNewBEFunctionNamesMin.FunctionValueText = string.Format("{0:#.##}%", (valueMin == 0) ? "0.00" : valueMin.ToString());

                            objectGenericBEFunctionNames.Add(objectNewBEFunctionNamesMin);

                            //Calculate Maximum value
                            RMC.BusinessEntities.BEFunctionNames objectNewBEFunctionNamesMax = new RMC.BusinessEntities.BEFunctionNames();
                            double valueMax = 0;
                            valueMax = r.Select(s => Convert.ToDouble((s.Values.Length > 0) ? s.Values.Substring(0, s.Values.Length - 1) : "0")).Max();

                            objectNewBEFunctionNamesMax.ColumnName = r.FirstOrDefault().ColumnName;
                            objectNewBEFunctionNamesMax.FunctionName = "Maximum";
                            objectNewBEFunctionNamesMax.FunctionNameDouble = valueMax;
                            objectNewBEFunctionNamesMax.FunctionValueText = string.Format("{0:#.##}%", (valueMax == 0) ? "0.00" : valueMax.ToString());

                            //Calculate Average value
                            RMC.BusinessEntities.BEFunctionNames objectNewBEFunctionNamesAvg = new RMC.BusinessEntities.BEFunctionNames();
                            List<double> valueSum = null;
                            valueSum = r.Select(s => Convert.ToDouble((s.Values.Length > 0) ? s.Values.Substring(0, s.Values.Length - 1) : "0")).ToList();

                            objectNewBEFunctionNamesAvg.ColumnName = r.FirstOrDefault().ColumnName;
                            objectNewBEFunctionNamesAvg.FunctionName = "Average";
                            objectNewBEFunctionNamesAvg.FunctionNameDouble = (valueSum.Sum() / r.Select(s => s.RowName).Distinct().Count());
                            objectNewBEFunctionNamesAvg.FunctionValueText = string.Format("{0:#.##}%", (objectNewBEFunctionNamesAvg.FunctionNameDouble == 0) ? "0.00" : string.Format("{0:#.##}", objectNewBEFunctionNamesAvg.FunctionNameDouble));

                            //Median Value.
                            RMC.BusinessEntities.BEFunctionNames objectNewBEFunctionNamesMed = new RMC.BusinessEntities.BEFunctionNames();
                            int median = 0;
                            int medianEven = 0;
                            int count = r.Select(s => s.RowName).Distinct().Count();
                            if (count % 2 == 0)
                            {
                                median = count / 2;
                                medianEven = median + 1;

                                objectNewBEFunctionNamesMed.ColumnName = r.FirstOrDefault().ColumnName;
                                objectNewBEFunctionNamesMed.FunctionName = "Median";
                                objectNewBEFunctionNamesMed.FunctionNameDouble = (r.OrderBy(x => x.ValuesSum).Select(s => s.ValuesSum).ToList().ElementAt(median - 1) + r.OrderBy(x => x.ValuesSum).Select(s => s.ValuesSum).ToList().ElementAt(medianEven - 1)) / 2;
                                objectNewBEFunctionNamesMed.FunctionValueText = string.Format("{0:#.##}%", string.Format("{0:#.##}", (objectNewBEFunctionNamesMed.FunctionNameDouble == 0.00) ? "0.00" : string.Format("{0:#.##}", objectNewBEFunctionNamesMed.FunctionNameDouble)));
                            }
                            else
                            {
                                median = (count + 1) / 2;

                                objectNewBEFunctionNamesMed.ColumnName = r.FirstOrDefault().ColumnName;
                                objectNewBEFunctionNamesMed.FunctionName = "Median";
                                objectNewBEFunctionNamesMed.FunctionNameDouble = r.OrderBy(x => x.ValuesSum).Select(s => s.ValuesSum).ToList().ElementAt(median - 1);
                                objectNewBEFunctionNamesMed.FunctionValueText = string.Format("{0:#.##}%", string.Format("{0:#.##}", (objectNewBEFunctionNamesMed.FunctionNameDouble == 0.00) ? "0.00" : string.Format("{0:#.##}", objectNewBEFunctionNamesMed.FunctionNameDouble)));
                            }
                            //median--;

                            //Quartile
                            RMC.BusinessEntities.BEFunctionNames objectNewBEFunctionNamesQuartile1 = new RMC.BusinessEntities.BEFunctionNames();

                            objectNewBEFunctionNamesQuartile1.ColumnName = r.FirstOrDefault().ColumnName;
                            objectNewBEFunctionNamesQuartile1.FunctionName = "Quartile(1)";
                            objectNewBEFunctionNamesQuartile1.FunctionNameDouble = CalculateQuartile(count, 1, r.Select(s => s.ValuesSum).ToList());
                            objectNewBEFunctionNamesQuartile1.FunctionValueText = string.Format("{0:#.##}%", (objectNewBEFunctionNamesQuartile1.FunctionNameDouble == 0.00) ? "0.00" : string.Format("{0:#.##}", objectNewBEFunctionNamesQuartile1.FunctionNameDouble));

                            RMC.BusinessEntities.BEFunctionNames objectNewBEFunctionNamesQuartile3 = new RMC.BusinessEntities.BEFunctionNames();

                            objectNewBEFunctionNamesQuartile3.ColumnName = r.FirstOrDefault().ColumnName;
                            objectNewBEFunctionNamesQuartile3.FunctionName = "Quartile(3)";
                            objectNewBEFunctionNamesQuartile3.FunctionNameDouble = CalculateQuartile(count, 3, r.Select(s => s.ValuesSum).ToList());
                            objectNewBEFunctionNamesQuartile3.FunctionValueText = string.Format("{0:#.##}%", (objectNewBEFunctionNamesQuartile3.FunctionNameDouble == 0.00) ? "0.00" : string.Format("{0:#.##}", objectNewBEFunctionNamesQuartile3.FunctionNameDouble));

                            objectGenericBEFunctionNames.Add(objectNewBEFunctionNamesQuartile1);
                            objectGenericBEFunctionNames.Add(objectNewBEFunctionNamesMed);
                            objectGenericBEFunctionNames.Add(objectNewBEFunctionNamesAvg);
                            objectGenericBEFunctionNames.Add(objectNewBEFunctionNamesQuartile3);
                            objectGenericBEFunctionNames.Add(objectNewBEFunctionNamesMax);
                        }

                        int countt = objectGenericListBEReportValueAdded.Count;
                        //List<RMC.BusinessEntities.BEReports> objectGenericListResultTemp = new List<RMC.BusinessEntities.BEReports>();
                        //objectGenericListResultTemp = objectGenericListResult;
                        objectGenericBEFunctionNames.ForEach(delegate(RMC.BusinessEntities.BEFunctionNames objectBEFunctionNames)
                        {
                            for (int index = 0; index < countt; index++)
                            {
                                RMC.BusinessEntities.BEReports objectBEReports = new RMC.BusinessEntities.BEReports();

                                objectBEReports.ColumnName = objectBEFunctionNames.FunctionName;
                                //objectBEReports.MonthName = BSCommon.GetMonthName(Convert.ToString(index + 1));
                                objectBEReports.MonthName = objectGenericListBEReportValueAdded[index].MonthName;
                                objectBEReports.Values = objectBEFunctionNames.FunctionValueText;
                                objectBEReports.ValuesSum = objectBEFunctionNames.FunctionNameDouble;

                                objectGenericListBEReportValueAdded.Add(objectBEReports);
                            }
                        });
                    }
                }
                //======================

                else if (ProfileCategoryValue == "Database Values")
                {
                    List<RMC.BusinessEntities.BEValidation> objectGenericBEValidation = SearchHospitalsDataForBenchmarkTest(hospitalUnitID, firstYear, lastYear, firstMonth, lastMonth, null, 0, null, 0, null, 0, null, 0, startDate, endDate, null, 0, null, 0, docByException, unitType, pharmacyType, null, 0, null, 0, null, null, null, configName,null);
                    //List<RMC.BusinessEntities.BEValidation> objectGenericBEValidation = SearchHospitalsDataForBenchmarkTest(hospitalUnitID, firstYear, lastYear, firstMonth, lastMonth, bedInUnitFrom, optBedInUnitFrom, bedInUnitTo, optBedInUnitTo, budgetedPatientFrom, optBudgetedPatientFrom, budgetedPatientTo, optBudgetedPatientTo, startDate, endDate, electronicDocumentFrom, optElectronicDocumentFrom, electronicDocumentTo, optElectronicDocumentTo, docByException, unitType, pharmacyType, hospitalType, optHospitalSizeFrom, hospitalSizeFrom, optHospitalSizeTo, hospitalSizeTo, countryId, stateId, configName);
                    //Here hospitalId is null to compare with single hospital data
                    List<RMC.BusinessEntities.BEValidation> objectGenericBEValidationAll = SearchHospitalsDataForBenchmarkTest(null, firstYear, lastYear, firstMonth, lastMonth, null, 0, null, 0, null, 0, null, 0, startDate, endDate, null, 0, null, 0, docByException, unitType, pharmacyType, null, 0, null, 0, null, null, null, configName, null);
                    //List<RMC.BusinessEntities.BEValidation> objectGenericBEValidationAll = SearchHospitalsDataForBenchmarkTest(null, firstYear, lastYear, firstMonth, lastMonth, bedInUnitFrom, optBedInUnitFrom, bedInUnitTo, optBedInUnitTo, budgetedPatientFrom, optBudgetedPatientFrom, budgetedPatientTo, optBudgetedPatientTo, startDate, endDate, electronicDocumentFrom, optElectronicDocumentFrom, electronicDocumentTo, optElectronicDocumentTo, docByException, unitType, pharmacyType, hospitalType, optHospitalSizeFrom, hospitalSizeFrom, optHospitalSizeTo, hospitalSizeTo, countryId, stateId, configName);
                    List<RMC.BusinessEntities.BEReports> objectNewGenericBEReports = null;
                    List<RMC.BusinessEntities.BEFunctionNames> objectGenericBEFunctionNames = new List<RMC.BusinessEntities.BEFunctionNames>();

                    if (ProfileSubCategoryValue == "Activity")
                    {
                        List<RMC.DataService.Activity> objectGenericActivity = _objectRMCDataContext.Activities.ToList();

                        objectGenericBEValidation = objectGenericBEValidation.FindAll(delegate(RMC.BusinessEntities.BEValidation objectNewBEValidation)
                        {
                            return objectGenericActivity.Exists(delegate(RMC.DataService.Activity objectActivity)
                            {
                                return objectActivity.ActivityID == objectNewBEValidation.ActivityID;
                            });
                        });

                        objectGenericListBEReportValueAdded = (from v in objectGenericBEValidation.OrderBy(o => o.Year).ThenBy(p => p.MonthIndex)
                                                   group v by new { v.Year, v.MonthIndex } into g
                                                   from t in g.GroupBy(x => x.ActivityID).ToList()
                                                   select new RMC.BusinessEntities.BEReports
                                                   {
                                                       Name = "Activity",
                                                       ColumnName = objectGenericActivity.Find(delegate(RMC.DataService.Activity objectActivity)
                                                       {
                                                           return objectActivity.ActivityID == t.Key;
                                                       }).Activity1,
                                                       //ColumnName = "Deliver FoodTray",
                                                       ColumnNumber = objectGenericActivity.Find(delegate(RMC.DataService.Activity objectActivity)
                                                       {
                                                           return objectActivity.ActivityID == t.Key;
                                                       }).ActivityID,
                                                       MonthName = BSCommon.GetMonthName(t.FirstOrDefault(r => r.Month != "").Month) + " " + t.FirstOrDefault().Year,
                                                       RowName = t.FirstOrDefault(r => r.HospitalUnitName != "").HospitalUnitName,
                                                       Values = string.Format("{0:#.##}%", ((double)t.Count(r => r.ActivityID == t.Key) / (double)g.Count(c => c.ActivityID != 0)) * 100),
                                                       ValuesSum = Convert.ToDouble(string.Format("{0:#.##}", ((double)t.Count(r => r.ActivityID == t.Key) / (double)g.Count(c => c.ActivityID != 0)) * 100))
                                                   }).ToList();

                        objectGenericListBEReportValueAdded = objectGenericListBEReportValueAdded.FindAll(delegate(RMC.BusinessEntities.BEReports objectBEReport)
                        {
                            //return objectBEReport.ColumnName.ToLower().Trim() == dbValues.ToLower().Trim();
                            return true;
                        });

                        //for comparing with National Database means calculating function values (min, max, quartile etc)

                        objectGenericBEValidationAll = objectGenericBEValidationAll.FindAll(delegate(RMC.BusinessEntities.BEValidation objectNewBEValidation)
                        {
                            return objectGenericActivity.Exists(delegate(RMC.DataService.Activity objectActivity)
                            {
                                return objectActivity.ActivityID == objectNewBEValidation.ActivityID;
                            });
                        });

                        objectNewGenericBEReports = (from v in objectGenericBEValidationAll.GroupBy(o => o.HospitalUnitIDCounter).ToList()
                                                     from t in v.GroupBy(x => x.ActivityID).ToList()
                                                     select new RMC.BusinessEntities.BEReports
                                                     {
                                                         Name = "Activity",
                                                         ColumnName = objectGenericActivity.Find(delegate(RMC.DataService.Activity objectActivity)
                                                         {
                                                             return objectActivity.ActivityID == t.Key;
                                                         }).Activity1,
                                                         //ColumnName = "Deliver FoodTray",
                                                         ColumnNumber = objectGenericActivity.Find(delegate(RMC.DataService.Activity objectActivity)
                                                         {
                                                             return objectActivity.ActivityID == t.Key;
                                                         }).ActivityID,
                                                         MonthName = BSCommon.GetMonthName(t.FirstOrDefault(r => r.Month != "").Month) + " " + t.FirstOrDefault().Year,
                                                         RowName = t.FirstOrDefault(r => r.HospitalUnitName != "").HospitalUnitName,
                                                         Values = string.Format("{0:#.##}%", ((double)t.Count(r => r.ActivityID == t.Key) / (double)v.Count(c => c.ActivityID != 0)) * 100),
                                                         //ValuesSum = Convert.ToDouble(string.Format("{0:#.##}", ((double)t.Count(r => r.ActivityID == t.Key) / (double)v.Count(c => c.ActivityID != 0)) * 100)),
                                                         ValuesSum = ((double)t.Count(r => r.ActivityID == t.Key) / (double)v.Count(c => c.ActivityID != 0)) * 100,
                                                         DataPoint = (double)v.Count(c => c.ActivityID != 0),
                                                     }).ToList();


                        objectNewGenericBEReports = objectNewGenericBEReports.FindAll(delegate(RMC.BusinessEntities.BEReports objectBEReport)
                        {
                            //return objectBEReport.ColumnName.ToLower().Trim() == dbValues.ToLower().Trim();
                            return true;
                        });

                    }
                    if (ProfileSubCategoryValue == "Sub-Activity")
                    {
                        List<RMC.DataService.SubActivity> objectGenericSubActivity = _objectRMCDataContext.SubActivities.ToList();

                        objectGenericBEValidation = objectGenericBEValidation.FindAll(delegate(RMC.BusinessEntities.BEValidation objectNewBEValidation)
                        {
                            return objectGenericSubActivity.Exists(delegate(RMC.DataService.SubActivity objectSubActivity)
                            {
                                return objectSubActivity.SubActivityID == objectNewBEValidation.SubActivityID;
                            });
                        });

                        objectGenericListBEReportValueAdded = (from v in objectGenericBEValidation.OrderBy(o => o.Year).ThenBy(p => p.MonthIndex)
                                                   group v by new { v.Year, v.MonthIndex } into g
                                                   from t in g.GroupBy(x => x.SubActivityID).ToList()
                                                   select new RMC.BusinessEntities.BEReports
                                                   {
                                                       Name = "Sub-Activity",
                                                       ColumnName = objectGenericSubActivity.Find(delegate(RMC.DataService.SubActivity objectSubActivity)
                                                       {
                                                           return objectSubActivity.SubActivityID == t.Key;
                                                       }).SubActivity1,
                                                       ColumnNumber = objectGenericSubActivity.Find(delegate(RMC.DataService.SubActivity objectSubActivity)
                                                       {
                                                           return objectSubActivity.SubActivityID == t.Key;
                                                       }).SubActivityID,
                                                       MonthName = BSCommon.GetMonthName(t.FirstOrDefault(r => r.Month != "").Month) + " " + t.FirstOrDefault().Year,
                                                       RowName = t.FirstOrDefault(r => r.HospitalUnitName != "").HospitalUnitName,
                                                       Values = string.Format("{0:#.##}%", ((double)t.Count(r => r.SubActivityID == t.Key) / (double)g.Count(c => c.SubActivityID != 0)) * 100),
                                                       ValuesSum = Convert.ToDouble(string.Format("{0:#.##}", ((double)t.Count(r => r.SubActivityID == t.Key) / (double)g.Count(c => c.SubActivityID != 0)) * 100))
                                                   }).ToList();

                        objectGenericListBEReportValueAdded = objectGenericListBEReportValueAdded.FindAll(delegate(RMC.BusinessEntities.BEReports objectBEReport)
                        {
                            //return objectBEReport.ColumnName.ToLower().Trim() == dbValues.ToLower().Trim();
                            return true;
                        });

                        //for comparing with National Database means calculating function values (min, max, quartile etc)

                        objectGenericBEValidationAll = objectGenericBEValidationAll.FindAll(delegate(RMC.BusinessEntities.BEValidation objectNewBEValidation)
                        {
                            return objectGenericSubActivity.Exists(delegate(RMC.DataService.SubActivity objectSubActivity)
                            {
                                return objectSubActivity.SubActivityID == objectNewBEValidation.SubActivityID;
                            });
                        });

                        objectNewGenericBEReports = (from v in objectGenericBEValidationAll.GroupBy(o => o.HospitalUnitIDCounter).ToList()
                                                     from t in v.GroupBy(x => x.SubActivityID).ToList()
                                                     select new RMC.BusinessEntities.BEReports
                                                     {
                                                         Name = "Sub-Activity",
                                                         ColumnName = objectGenericSubActivity.Find(delegate(RMC.DataService.SubActivity objectSubActivity)
                                                         {
                                                             return objectSubActivity.SubActivityID == t.Key;
                                                         }).SubActivity1,
                                                         ColumnNumber = objectGenericSubActivity.Find(delegate(RMC.DataService.SubActivity objectSubActivity)
                                                         {
                                                             return objectSubActivity.SubActivityID == t.Key;
                                                         }).SubActivityID,
                                                         MonthName = BSCommon.GetMonthName(t.FirstOrDefault(r => r.Month != "").Month) + " " + t.FirstOrDefault().Year,
                                                         RowName = t.FirstOrDefault(r => r.HospitalUnitName != "").HospitalUnitName,
                                                         Values = string.Format("{0:#.##}%", ((double)t.Count(r => r.SubActivityID == t.Key) / (double)v.Count(c => c.SubActivityID != 0)) * 100),
                                                         ValuesSum = Convert.ToDouble(string.Format("{0:#.##}", ((double)t.Count(r => r.SubActivityID == t.Key) / (double)v.Count(c => c.SubActivityID != 0)) * 100))
                                                     }).ToList();


                        objectNewGenericBEReports = objectNewGenericBEReports.FindAll(delegate(RMC.BusinessEntities.BEReports objectBEReport)
                        {
                            //return objectBEReport.ColumnName.ToLower().Trim() == dbValues.ToLower().Trim();
                            return true;
                        });


                    }
                    if (ProfileSubCategoryValue == "Last Location")
                    {
                        List<RMC.DataService.LastLocation> objectGenericLastLocation = _objectRMCDataContext.LastLocations.ToList();

                        objectGenericBEValidation = objectGenericBEValidation.FindAll(delegate(RMC.BusinessEntities.BEValidation objectNewBEValidation)
                        {
                            return objectGenericLastLocation.Exists(delegate(RMC.DataService.LastLocation objectLastLocation)
                            {
                                return objectLastLocation.LastLocationID == objectNewBEValidation.LastLocationID;
                            });
                        });

                        objectGenericListBEReportValueAdded = (from v in objectGenericBEValidation.OrderBy(o => o.Year).ThenBy(p => p.MonthIndex)
                                                   group v by new { v.Year, v.MonthIndex } into g
                                                   from t in g.GroupBy(x => x.LastLocationID).ToList()
                                                   select new RMC.BusinessEntities.BEReports
                                                   {
                                                       Name = "Last Location",
                                                       ColumnName = objectGenericLastLocation.Find(delegate(RMC.DataService.LastLocation objectLastLocation)
                                                       {
                                                           return objectLastLocation.LastLocationID == t.Key;
                                                       }).LastLocation1,
                                                       ColumnNumber = objectGenericLastLocation.Find(delegate(RMC.DataService.LastLocation objectLastLocation)
                                                       {
                                                           return objectLastLocation.LastLocationID == t.Key;
                                                       }).LastLocationID,
                                                       MonthName = BSCommon.GetMonthName(t.FirstOrDefault(r => r.Month != "").Month) + " " + t.FirstOrDefault().Year,
                                                       RowName = t.FirstOrDefault(r => r.HospitalUnitName != "").HospitalUnitName,
                                                       Values = string.Format("{0:#.##}%", ((double)t.Count(r => r.LastLocationID == t.Key) / (double)g.Count(c => c.LastLocationID != 0)) * 100),
                                                       ValuesSum = Convert.ToDouble(string.Format("{0:#.##}", ((double)t.Count(r => r.LastLocationID == t.Key) / (double)g.Count(c => c.LastLocationID != 0)) * 100))
                                                   }).ToList();

                        objectGenericListBEReportValueAdded = objectGenericListBEReportValueAdded.FindAll(delegate(RMC.BusinessEntities.BEReports objectBEReport)
                        {
                            //return objectBEReport.ColumnName.ToLower().Trim() == dbValues.ToLower().Trim();
                            return true;
                        });

                        //for comparing with National Database means calculating function values (min, max, quartile etc)

                        objectGenericBEValidationAll = objectGenericBEValidationAll.FindAll(delegate(RMC.BusinessEntities.BEValidation objectNewBEValidation)
                        {
                            return objectGenericLastLocation.Exists(delegate(RMC.DataService.LastLocation objectLastLocation)
                            {
                                return objectLastLocation.LastLocationID == objectNewBEValidation.LastLocationID;
                            });
                        });

                        objectNewGenericBEReports = (from v in objectGenericBEValidationAll.GroupBy(o => o.HospitalUnitIDCounter).ToList()
                                                     from t in v.GroupBy(x => x.LastLocationID).ToList()
                                                     select new RMC.BusinessEntities.BEReports
                                                     {
                                                         Name = "Last Location",
                                                         ColumnName = objectGenericLastLocation.Find(delegate(RMC.DataService.LastLocation objectLastLocation)
                                                         {
                                                             return objectLastLocation.LastLocationID == t.Key;
                                                         }).LastLocation1,
                                                         ColumnNumber = objectGenericLastLocation.Find(delegate(RMC.DataService.LastLocation objectLastLocation)
                                                         {
                                                             return objectLastLocation.LastLocationID == t.Key;
                                                         }).LastLocationID,
                                                         MonthName = BSCommon.GetMonthName(t.FirstOrDefault(r => r.Month != "").Month) + " " + t.FirstOrDefault().Year,
                                                         RowName = t.FirstOrDefault(r => r.HospitalUnitName != "").HospitalUnitName,
                                                         Values = string.Format("{0:#.##}%", ((double)t.Count(r => r.LastLocationID == t.Key) / (double)v.Count(c => c.LastLocationID != 0)) * 100),
                                                         ValuesSum = Convert.ToDouble(string.Format("{0:#.##}", ((double)t.Count(r => r.LastLocationID == t.Key) / (double)v.Count(c => c.LastLocationID != 0)) * 100))
                                                     }).ToList();

                        objectNewGenericBEReports = objectNewGenericBEReports.FindAll(delegate(RMC.BusinessEntities.BEReports objectBEReport)
                        {
                            //return objectBEReport.ColumnName.ToLower().Trim() == dbValues.ToLower().Trim();
                            return true;
                        });

                    }
                    if (ProfileSubCategoryValue == "Current Location")
                    {
                        List<RMC.DataService.Location> objectGenericLocation = _objectRMCDataContext.Locations.ToList();

                        objectGenericBEValidation = objectGenericBEValidation.FindAll(delegate(RMC.BusinessEntities.BEValidation objectNewBEValidation)
                        {
                            return objectGenericLocation.Exists(delegate(RMC.DataService.Location objectLocation)
                            {
                                return objectLocation.LocationID == objectNewBEValidation.LocationID;
                            });
                        });


                        objectGenericListBEReportValueAdded = (from v in objectGenericBEValidation.OrderBy(o => o.Year).ThenBy(p => p.MonthIndex)
                                                   group v by new { v.Year, v.MonthIndex } into g
                                                   from t in g.GroupBy(x => x.LocationID).ToList()
                                                   select new RMC.BusinessEntities.BEReports
                                                   {
                                                       Name = "Current Location",
                                                       ColumnName = objectGenericLocation.Find(delegate(RMC.DataService.Location objectLocation)
                                                       {
                                                           return objectLocation.LocationID == t.Key;
                                                       }).Location1,
                                                       ColumnNumber = objectGenericLocation.Find(delegate(RMC.DataService.Location objectLocation)
                                                       {
                                                           return objectLocation.LocationID == t.Key;
                                                       }).LocationID,
                                                       MonthName = BSCommon.GetMonthName(t.FirstOrDefault(r => r.Month != "").Month) + " " + t.FirstOrDefault().Year,
                                                       RowName = t.FirstOrDefault(r => r.HospitalUnitName != "").HospitalUnitName,
                                                       Values = string.Format("{0:#.##}%", ((double)t.Count(r => r.LocationID == t.Key) / (double)g.Count(c => c.LocationID != 0)) * 100),
                                                       ValuesSum = Convert.ToDouble(string.Format("{0:#.##}", ((double)t.Count(r => r.LocationID == t.Key) / (double)g.Count(c => c.LocationID != 0)) * 100))
                                                   }).ToList();


                        objectGenericListBEReportValueAdded = objectGenericListBEReportValueAdded.FindAll(delegate(RMC.BusinessEntities.BEReports objectBEReport)
                        {
                            //return objectBEReport.ColumnName.ToLower().Trim() == dbValues.ToLower().Trim();
                            return true;
                        });

                        //for comparing with National Database means calculating function values (min, max, quartile etc)

                        objectGenericBEValidationAll = objectGenericBEValidationAll.FindAll(delegate(RMC.BusinessEntities.BEValidation objectNewBEValidation)
                        {
                            return objectGenericLocation.Exists(delegate(RMC.DataService.Location objectLocation)
                            {
                                return objectLocation.LocationID == objectNewBEValidation.LocationID;
                            });
                        });

                        objectNewGenericBEReports = (from v in objectGenericBEValidationAll.GroupBy(o => o.HospitalUnitIDCounter).ToList()
                                                     from t in v.GroupBy(x => x.LocationID).ToList()
                                                     select new RMC.BusinessEntities.BEReports
                                                     {
                                                         Name = "Current Location",
                                                         ColumnName = objectGenericLocation.Find(delegate(RMC.DataService.Location objectLocation)
                                                         {
                                                             return objectLocation.LocationID == t.Key;
                                                         }).Location1,
                                                         ColumnNumber = objectGenericLocation.Find(delegate(RMC.DataService.Location objectLocation)
                                                         {
                                                             return objectLocation.LocationID == t.Key;
                                                         }).LocationID,
                                                         MonthName = BSCommon.GetMonthName(t.FirstOrDefault(r => r.Month != "").Month) + " " + t.FirstOrDefault().Year,
                                                         RowName = t.FirstOrDefault(r => r.HospitalUnitName != "").HospitalUnitName,
                                                         Values = string.Format("{0:#.##}%", ((double)t.Count(r => r.LocationID == t.Key) / (double)v.Count(c => c.LocationID != 0)) * 100),
                                                         ValuesSum = Convert.ToDouble(string.Format("{0:#.##}", ((double)t.Count(r => r.LocationID == t.Key) / (double)v.Count(c => c.LocationID != 0)) * 100))
                                                     }).ToList();

                        objectNewGenericBEReports = objectNewGenericBEReports.FindAll(delegate(RMC.BusinessEntities.BEReports objectBEReport)
                        {
                            //return objectBEReport.ColumnName.ToLower().Trim() == dbValues.ToLower().Trim();
                            return true;
                        });

                    }
                    if (ProfileSubCategoryValue == "Staffing Model")
                    {
                        List<RMC.DataService.ResourceRequirement> objectGenericResourceRequirement = _objectRMCDataContext.ResourceRequirements.ToList();

                        objectGenericBEValidation = objectGenericBEValidation.FindAll(delegate(RMC.BusinessEntities.BEValidation objectNewBEValidation)
                        {
                            return objectGenericResourceRequirement.Exists(delegate(RMC.DataService.ResourceRequirement objectResourceRequirement)
                            {
                                return objectResourceRequirement.ResourceRequirementID == objectNewBEValidation.ResourceRequirementID;
                            });
                        });


                        objectGenericListBEReportValueAdded = (from v in objectGenericBEValidation.OrderBy(o => o.Year).ThenBy(p => p.MonthIndex)
                                                   group v by new { v.Year, v.MonthIndex } into g
                                                   from t in g.GroupBy(x => x.ResourceRequirementID).ToList()
                                                   select new RMC.BusinessEntities.BEReports
                                                   {
                                                       Name = "Staffing Model",
                                                       ColumnName = objectGenericResourceRequirement.Find(delegate(RMC.DataService.ResourceRequirement objectResourceRequirement)
                                                       {
                                                           return objectResourceRequirement.ResourceRequirementID == t.Key;
                                                       }).ResourceRequirement1,
                                                       ColumnNumber = objectGenericResourceRequirement.Find(delegate(RMC.DataService.ResourceRequirement objectResourceRequirement)
                                                       {
                                                           return objectResourceRequirement.ResourceRequirementID == t.Key;
                                                       }).ResourceRequirementID,
                                                       MonthName = BSCommon.GetMonthName(t.FirstOrDefault(r => r.Month != "").Month) + " " + t.FirstOrDefault().Year,
                                                       RowName = t.FirstOrDefault(r => r.HospitalUnitName != "").HospitalUnitName,
                                                       Values = string.Format("{0:#.##}%", ((double)t.Count(r => r.ResourceRequirementID == t.Key) / (double)g.Count(c => c.ResourceRequirementID != 0)) * 100),
                                                       ValuesSum = Convert.ToDouble(string.Format("{0:#.##}", ((double)t.Count(r => r.ResourceRequirementID == t.Key) / (double)g.Count(c => c.ResourceRequirementID != 0)) * 100))
                                                   }).ToList();

                        objectGenericListBEReportValueAdded = objectGenericListBEReportValueAdded.FindAll(delegate(RMC.BusinessEntities.BEReports objectBEReport)
                        {
                            //return objectBEReport.ColumnName.ToLower().Trim() == dbValues.ToLower().Trim();
                            return true;
                        });

                        //for comparing with National Database means calculating function values (min, max, quartile etc)

                        objectGenericBEValidationAll = objectGenericBEValidationAll.FindAll(delegate(RMC.BusinessEntities.BEValidation objectNewBEValidation)
                        {
                            return objectGenericResourceRequirement.Exists(delegate(RMC.DataService.ResourceRequirement objectResourceRequirement)
                            {
                                return objectResourceRequirement.ResourceRequirementID == objectNewBEValidation.ResourceRequirementID;
                            });
                        });

                        objectNewGenericBEReports = (from v in objectGenericBEValidationAll.GroupBy(o => o.HospitalUnitIDCounter).ToList()
                                                     from t in v.GroupBy(x => x.ResourceRequirementID).ToList()
                                                     select new RMC.BusinessEntities.BEReports
                                                     {
                                                         Name = "Staffing Model",
                                                         ColumnName = objectGenericResourceRequirement.Find(delegate(RMC.DataService.ResourceRequirement objectResourceRequirement)
                                                         {
                                                             return objectResourceRequirement.ResourceRequirementID == t.Key;
                                                         }).ResourceRequirement1,
                                                         ColumnNumber = objectGenericResourceRequirement.Find(delegate(RMC.DataService.ResourceRequirement objectResourceRequirement)
                                                         {
                                                             return objectResourceRequirement.ResourceRequirementID == t.Key;
                                                         }).ResourceRequirementID,
                                                         MonthName = BSCommon.GetMonthName(t.FirstOrDefault(r => r.Month != "").Month) + " " + t.FirstOrDefault().Year,
                                                         RowName = t.FirstOrDefault(r => r.HospitalUnitName != "").HospitalUnitName,
                                                         Values = string.Format("{0:#.##}%", ((double)t.Count(r => r.ResourceRequirementID == t.Key) / (double)v.Count(c => c.ResourceRequirementID != 0)) * 100),
                                                         ValuesSum = Convert.ToDouble(string.Format("{0:#.##}", ((double)t.Count(r => r.ResourceRequirementID == t.Key) / (double)v.Count(c => c.ResourceRequirementID != 0)) * 100))
                                                     }).ToList();

                        objectNewGenericBEReports = objectNewGenericBEReports.FindAll(delegate(RMC.BusinessEntities.BEReports objectBEReport)
                        {
                            //return objectBEReport.ColumnName.ToLower().Trim() == dbValues.ToLower().Trim();
                            return true;
                        });
                    }
                    //
                    if (ProfileSubCategoryValue == "Cognitive")
                    {
                        List<RMC.DataService.CognitiveCategory> objectGenericCognitiveCategories = _objectRMCDataContext.CognitiveCategories.ToList();

                        objectGenericBEValidation = objectGenericBEValidation.FindAll(delegate(RMC.BusinessEntities.BEValidation objectNewBEValidation)
                        {
                            return objectGenericCognitiveCategories.Exists(delegate(RMC.DataService.CognitiveCategory objectCognitiveCategory)
                            {
                                return objectCognitiveCategory.CognitiveCategoryID == objectNewBEValidation.CognitiveCategoryID;
                            });
                        });


                        objectGenericListBEReportValueAdded = (from v in objectGenericBEValidation.OrderBy(o => o.Year).ThenBy(p => p.MonthIndex)
                                                               group v by new { v.Year, v.MonthIndex } into g
                                                               from t in g.GroupBy(x => x.CognitiveCategoryID).ToList()
                                                               select new RMC.BusinessEntities.BEReports
                                                               {
                                                                   Name = "Cognitive",
                                                                   ColumnName = objectGenericCognitiveCategories.Find(delegate(RMC.DataService.CognitiveCategory objectLocation)
                                                                   {
                                                                       return objectLocation.CognitiveCategoryID == t.Key;
                                                                   }).CognitiveCategoryText,
                                                                   ColumnNumber = objectGenericCognitiveCategories.Find(delegate(RMC.DataService.CognitiveCategory objectLocation)
                                                                   {
                                                                       return objectLocation.CognitiveCategoryID == t.Key;
                                                                   }).CognitiveCategoryID,
                                                                   MonthName = BSCommon.GetMonthName(t.FirstOrDefault(r => r.Month != "").Month) + " " + t.FirstOrDefault().Year,
                                                                   RowName = t.FirstOrDefault(r => r.HospitalUnitName != "").HospitalUnitName,
                                                                   Values = string.Format("{0:#.##}%", ((double)t.Count(r => r.CognitiveCategoryID == t.Key) / (double)g.Count(c => c.CognitiveCategoryID != 0)) * 100),
                                                                   ValuesSum = Convert.ToDouble(string.Format("{0:#.##}", ((double)t.Count(r => r.CognitiveCategoryID == t.Key) / (double)g.Count(c => c.CognitiveCategoryID != 0)) * 100))
                                                               }).ToList();


                        objectGenericListBEReportValueAdded = objectGenericListBEReportValueAdded.FindAll(delegate(RMC.BusinessEntities.BEReports objectBEReport)
                        {
                            //return objectBEReport.ColumnName.ToLower().Trim() == dbValues.ToLower().Trim();
                            return true;
                        });

                        //for comparing with National Database means calculating function values (min, max, quartile etc)

                        objectGenericBEValidationAll = objectGenericBEValidationAll.FindAll(delegate(RMC.BusinessEntities.BEValidation objectNewBEValidation)
                        {
                            return objectGenericCognitiveCategories.Exists(delegate(RMC.DataService.CognitiveCategory objectLocation)
                            {
                                return objectLocation.CognitiveCategoryID == objectNewBEValidation.CognitiveCategoryID;
                            });
                        });

                        objectNewGenericBEReports = (from v in objectGenericBEValidationAll.GroupBy(o => o.HospitalUnitIDCounter).ToList()
                                                     from t in v.GroupBy(x => x.CognitiveCategoryID).ToList()
                                                     select new RMC.BusinessEntities.BEReports
                                                     {
                                                         Name = "Cognitive",
                                                         ColumnName = objectGenericCognitiveCategories.Find(delegate(RMC.DataService.CognitiveCategory objectCognitiveCategory)
                                                         {
                                                             return objectCognitiveCategory.CognitiveCategoryID == t.Key;
                                                         }).CognitiveCategoryText,
                                                         ColumnNumber = objectGenericCognitiveCategories.Find(delegate(RMC.DataService.CognitiveCategory objectLocation)
                                                         {
                                                             return objectLocation.CognitiveCategoryID == t.Key;
                                                         }).CognitiveCategoryID,
                                                         MonthName = BSCommon.GetMonthName(t.FirstOrDefault(r => r.Month != "").Month) + " " + t.FirstOrDefault().Year,
                                                         RowName = t.FirstOrDefault(r => r.HospitalUnitName != "").HospitalUnitName,
                                                         Values = string.Format("{0:#.##}%", ((double)t.Count(r => r.CognitiveCategoryID == t.Key) / (double)v.Count(c => c.CognitiveCategoryID != 0)) * 100),
                                                         ValuesSum = Convert.ToDouble(string.Format("{0:#.##}", ((double)t.Count(r => r.CognitiveCategoryID == t.Key) / (double)v.Count(c => c.CognitiveCategoryID != 0)) * 100))
                                                     }).ToList();

                        objectNewGenericBEReports = objectNewGenericBEReports.FindAll(delegate(RMC.BusinessEntities.BEReports objectBEReport)
                        {
                            //return objectBEReport.ColumnName.ToLower().Trim() == dbValues.ToLower().Trim();
                            return true;
                        });

                    }
                    //////
                    if (objectGenericListBEReportValueAdded != null)
                    {
                        if (dataPointsFrom != null && dataPointsTo == null)
                        {
                            if (optDataPointsFrom == 1)
                            {
                                objectGenericListBEReportValueAdded = objectGenericListBEReportValueAdded.Where(x => Convert.ToInt32(x.DataPoint) < dataPointsFrom).ToList();
                            }
                            if (optDataPointsFrom == 2)
                            {
                                objectGenericListBEReportValueAdded = objectGenericListBEReportValueAdded.Where(x => x.DataPoint > dataPointsFrom).ToList();
                            }
                            if (optDataPointsFrom == 3)
                            {
                                objectGenericListBEReportValueAdded = objectGenericListBEReportValueAdded.Where(x => x.DataPoint == dataPointsFrom).ToList();
                            }
                            if (optDataPointsFrom == 4)
                            {
                                objectGenericListBEReportValueAdded = objectGenericListBEReportValueAdded.Where(x => x.DataPoint >= dataPointsFrom).ToList();
                            }
                            if (optDataPointsFrom == 5)
                            {
                                objectGenericListBEReportValueAdded = objectGenericListBEReportValueAdded.Where(x => x.DataPoint <= dataPointsFrom).ToList();
                            }
                            if (optDataPointsFrom == 6)
                            {
                                objectGenericListBEReportValueAdded = objectGenericListBEReportValueAdded.Where(x => x.DataPoint != dataPointsFrom).ToList();
                            }
                        }
                        if ((optDataPointsFrom == 0 && optdataPointsTo == 0) && (dataPointsFrom != null && dataPointsTo != null))
                        {
                            objectGenericListBEReportValueAdded = objectGenericListBEReportValueAdded.Where(x => x.DataPoint >= dataPointsFrom && x.DataPoint <= dataPointsTo).ToList();
                        }
                    }
                    //////
                    //
                    if (objectNewGenericBEReports != null)
                    {
                        //filteration according to DataPoints
                        if (dataPointsFrom != null && dataPointsTo == null)
                        {
                            if (optDataPointsFrom == 1)
                            {
                                objectNewGenericBEReports = objectNewGenericBEReports.Where(x => Convert.ToInt32(x.DataPoint) < dataPointsFrom).ToList();
                            }
                            if (optDataPointsFrom == 2)
                            {
                                objectNewGenericBEReports = objectNewGenericBEReports.Where(x => x.DataPoint > dataPointsFrom).ToList();
                            }
                            if (optDataPointsFrom == 3)
                            {
                                objectNewGenericBEReports = objectNewGenericBEReports.Where(x => x.DataPoint == dataPointsFrom).ToList();
                            }
                            if (optDataPointsFrom == 4)
                            {
                                objectNewGenericBEReports = objectNewGenericBEReports.Where(x => x.DataPoint >= dataPointsFrom).ToList();
                            }
                            if (optDataPointsFrom == 5)
                            {
                                objectNewGenericBEReports = objectNewGenericBEReports.Where(x => x.DataPoint <= dataPointsFrom).ToList();
                            }
                            if (optDataPointsFrom == 6)
                            {
                                objectNewGenericBEReports = objectNewGenericBEReports.Where(x => x.DataPoint != dataPointsFrom).ToList();
                            }
                        }

                        if ((optDataPointsFrom == 0 && optdataPointsTo == 0) && (dataPointsFrom != null && dataPointsTo != null))
                        {
                            objectNewGenericBEReports = objectNewGenericBEReports.Where(x => x.DataPoint >= dataPointsFrom && x.DataPoint <= dataPointsTo).ToList();
                        }

                        foreach (var r in objectNewGenericBEReports.GroupBy(o => o.ColumnName))
                        {
                            //Calculate Minimum value.
                            RMC.BusinessEntities.BEFunctionNames objectNewBEFunctionNamesMin = new RMC.BusinessEntities.BEFunctionNames();
                            double valueMin = 0;
                            valueMin = r.Select(s => Convert.ToDouble((s.Values.Length > 0) ? s.Values.Substring(0, s.Values.Length - 1) : "0")).Min();

                            objectNewBEFunctionNamesMin.ColumnName = r.FirstOrDefault().ColumnName;
                            objectNewBEFunctionNamesMin.FunctionName = "Minimum";
                            objectNewBEFunctionNamesMin.FunctionNameDouble = valueMin;
                            objectNewBEFunctionNamesMin.FunctionValueText = string.Format("{0:#.##}%", (valueMin == 0) ? "0.00" : valueMin.ToString());

                            objectGenericBEFunctionNames.Add(objectNewBEFunctionNamesMin);

                            //Calculate Maximum value
                            RMC.BusinessEntities.BEFunctionNames objectNewBEFunctionNamesMax = new RMC.BusinessEntities.BEFunctionNames();
                            double valueMax = 0;
                            valueMax = r.Select(s => Convert.ToDouble((s.Values.Length > 0) ? s.Values.Substring(0, s.Values.Length - 1) : "0")).Max();

                            objectNewBEFunctionNamesMax.ColumnName = r.FirstOrDefault().ColumnName;
                            objectNewBEFunctionNamesMax.FunctionName = "Maximum";
                            objectNewBEFunctionNamesMax.FunctionNameDouble = valueMax;
                            objectNewBEFunctionNamesMax.FunctionValueText = string.Format("{0:#.##}%", (valueMax == 0) ? "0.00" : valueMax.ToString());

                            //Calculate Average value
                            RMC.BusinessEntities.BEFunctionNames objectNewBEFunctionNamesAvg = new RMC.BusinessEntities.BEFunctionNames();
                            List<double> valueSum = null;
                            valueSum = r.Select(s => Convert.ToDouble((s.Values.Length > 0) ? s.Values.Substring(0, s.Values.Length - 1) : "0")).ToList();

                            objectNewBEFunctionNamesAvg.ColumnName = r.FirstOrDefault().ColumnName;
                            objectNewBEFunctionNamesAvg.FunctionName = "Average";
                            objectNewBEFunctionNamesAvg.FunctionNameDouble = (valueSum.Sum() / r.Select(s => s.RowName).Distinct().Count());
                            objectNewBEFunctionNamesAvg.FunctionValueText = string.Format("{0:#.##}%", (objectNewBEFunctionNamesAvg.FunctionNameDouble == 0) ? "0.00" : string.Format("{0:#.##}", objectNewBEFunctionNamesAvg.FunctionNameDouble));

                            //Median Value.
                            RMC.BusinessEntities.BEFunctionNames objectNewBEFunctionNamesMed = new RMC.BusinessEntities.BEFunctionNames();
                            int median = 0;
                            int medianEven = 0;
                            int count = r.Select(s => s.RowName).Distinct().Count();
                            if (count % 2 == 0)
                            {
                                median = count / 2;
                                medianEven = median + 1;

                                objectNewBEFunctionNamesMed.ColumnName = r.FirstOrDefault().ColumnName;
                                objectNewBEFunctionNamesMed.FunctionName = "Median";
                                objectNewBEFunctionNamesMed.FunctionNameDouble = (r.OrderBy(x => x.ValuesSum).Select(s => s.ValuesSum).ToList().ElementAt(median - 1) + r.OrderBy(x => x.ValuesSum).Select(s => s.ValuesSum).ToList().ElementAt(medianEven - 1)) / 2;
                                objectNewBEFunctionNamesMed.FunctionValueText = string.Format("{0:#.##}%", string.Format("{0:#.##}", (objectNewBEFunctionNamesMed.FunctionNameDouble == 0.00) ? "0.00" : string.Format("{0:#.##}", objectNewBEFunctionNamesMed.FunctionNameDouble)));
                            }
                            else
                            {
                                median = (count + 1) / 2;

                                objectNewBEFunctionNamesMed.ColumnName = r.FirstOrDefault().ColumnName;
                                objectNewBEFunctionNamesMed.FunctionName = "Median";
                                objectNewBEFunctionNamesMed.FunctionNameDouble = r.OrderBy(x => x.ValuesSum).Select(s => s.ValuesSum).ToList().ElementAt(median - 1);
                                objectNewBEFunctionNamesMed.FunctionValueText = string.Format("{0:#.##}%", string.Format("{0:#.##}", (objectNewBEFunctionNamesMed.FunctionNameDouble == 0.00) ? "0.00" : string.Format("{0:#.##}", objectNewBEFunctionNamesMed.FunctionNameDouble)));
                            }
                            //median--;

                            //Quartile
                            RMC.BusinessEntities.BEFunctionNames objectNewBEFunctionNamesQuartile1 = new RMC.BusinessEntities.BEFunctionNames();

                            objectNewBEFunctionNamesQuartile1.ColumnName = r.FirstOrDefault().ColumnName;
                            objectNewBEFunctionNamesQuartile1.FunctionName = "Quartile(1)";
                            objectNewBEFunctionNamesQuartile1.FunctionNameDouble = CalculateQuartile(count, 1, r.Select(s => s.ValuesSum).ToList());
                            objectNewBEFunctionNamesQuartile1.FunctionValueText = string.Format("{0:#.##}%", (objectNewBEFunctionNamesQuartile1.FunctionNameDouble == 0.00) ? "0.00" : string.Format("{0:#.##}", objectNewBEFunctionNamesQuartile1.FunctionNameDouble));

                            RMC.BusinessEntities.BEFunctionNames objectNewBEFunctionNamesQuartile3 = new RMC.BusinessEntities.BEFunctionNames();

                            objectNewBEFunctionNamesQuartile3.ColumnName = r.FirstOrDefault().ColumnName;
                            objectNewBEFunctionNamesQuartile3.FunctionName = "Quartile(3)";
                            objectNewBEFunctionNamesQuartile3.FunctionNameDouble = CalculateQuartile(count, 3, r.Select(s => s.ValuesSum).ToList());
                            objectNewBEFunctionNamesQuartile3.FunctionValueText = string.Format("{0:#.##}%", (objectNewBEFunctionNamesQuartile3.FunctionNameDouble == 0.00) ? "0.00" : string.Format("{0:#.##}", objectNewBEFunctionNamesQuartile3.FunctionNameDouble));

                            objectGenericBEFunctionNames.Add(objectNewBEFunctionNamesQuartile1);
                            objectGenericBEFunctionNames.Add(objectNewBEFunctionNamesMed);
                            objectGenericBEFunctionNames.Add(objectNewBEFunctionNamesAvg);
                            objectGenericBEFunctionNames.Add(objectNewBEFunctionNamesQuartile3);
                            objectGenericBEFunctionNames.Add(objectNewBEFunctionNamesMax);
                        }

                        int countt = objectGenericListBEReportValueAdded.Count;
                        //List<RMC.BusinessEntities.BEReports> objectGenericListResultTemp = new List<RMC.BusinessEntities.BEReports>();
                        //objectGenericListResultTemp = objectGenericListResult;
                        objectGenericBEFunctionNames.ForEach(delegate(RMC.BusinessEntities.BEFunctionNames objectBEFunctionNames)
                        {
                            for (int index = 0; index < countt; index++)
                            {
                                RMC.BusinessEntities.BEReports objectBEReports = new RMC.BusinessEntities.BEReports();

                                objectBEReports.ColumnName = objectBEFunctionNames.FunctionName;
                                //objectBEReports.MonthName = BSCommon.GetMonthName(Convert.ToString(index + 1));
                                objectBEReports.MonthName = objectGenericListBEReportValueAdded[index].MonthName;
                                objectBEReports.Values = objectBEFunctionNames.FunctionValueText;
                                objectBEReports.ValuesSum = objectBEFunctionNames.FunctionNameDouble;

                                objectGenericListBEReportValueAdded.Add(objectBEReports);
                            }
                        });
                    }
                }
                return objectGenericListBEReportValueAdded;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                //DisposeGlobalObjects();
            }
        }
        //cm
        /// <summary>
        /// This method is used for generating Hospital Benchmark Report in a grid form
        /// </summary>
        public System.Data.DataTable GetDataForHospitalBenchmarkSummaryGrid(string activitiesID, string valueAddedCategoryID, string OthersCategoryID, string LocationCategoryID, int? hospitalUnitID, int? firstYear, int? lastYear, int? firstMonth, int? lastMonth, int? bedInUnitFrom, int optBedInUnitFrom, int? bedInUnitTo, int optBedInUnitTo, float? budgetedPatientFrom, int optBudgetedPatientFrom, float? budgetedPatientTo, int optBudgetedPatientTo, string startDate, string endDate, int? electronicDocumentFrom, int optElectronicDocumentFrom, int? electronicDocumentTo, int optElectronicDocumentTo, int docByException, string unitType, string pharmacyType, string hospitalType, int optHospitalSizeFrom, int? hospitalSizeFrom, int optHospitalSizeTo, int? hospitalSizeTo, int? countryId, int? stateId, string activities, string value, string others, string location, int? dataPointsFrom, int optDataPointsFrom, int? dataPointsTo, int optdataPointsTo, string configName, string unitIds)
        {
            try
            {
                System.Data.DataTable dt = null;
                if (valueAddedCategoryID != null || OthersCategoryID != null || LocationCategoryID != null || activitiesID != null)
                {
                    List<RMC.BusinessEntities.BEReports> objectGenericListBEReportValueAdded = null;

                    if (MaintainSessions.SessionHospitalBenchmarkSummary != null)
                    {
                        objectGenericListBEReportValueAdded = MaintainSessions.SessionHospitalBenchmarkSummary as List<BusinessEntities.BEReports>;
                    }
                    else
                    {
                        objectGenericListBEReportValueAdded = GetDataForHospitalBenchmarkSummaryTest(activitiesID, valueAddedCategoryID, OthersCategoryID, LocationCategoryID, hospitalUnitID, firstYear, lastYear, firstMonth, lastMonth, bedInUnitFrom, optBedInUnitFrom, bedInUnitTo, optBedInUnitTo, budgetedPatientFrom, optBudgetedPatientFrom, budgetedPatientTo, optBudgetedPatientTo, startDate, endDate, electronicDocumentFrom, optElectronicDocumentFrom, electronicDocumentTo, optElectronicDocumentTo, docByException, unitType, pharmacyType, hospitalType, optHospitalSizeFrom, hospitalSizeFrom, optHospitalSizeTo, hospitalSizeTo, countryId, stateId, activities, value, others, location, dataPointsFrom, optDataPointsFrom, dataPointsTo, optdataPointsTo, configName, unitIds);
                    }

                    removeIncDecSymbol<RMC.BusinessEntities.BEReports>("ColumnName", ref objectGenericListBEReportValueAdded);
                    //System.Data.DataTable dt = null;
                    dt = AddRows(CreateTable(objectGenericListBEReportValueAdded), objectGenericListBEReportValueAdded);
                }
                return dt;

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                //DisposeGlobalObjects();
            }
        }

        public System.Data.DataTable GetDataForHospitalBenchmarkSummaryGridUnitID(string dbValues, string ProfileCategoryValue, string ProfileSubCategoryValue, string valueAddedCategoryID, string OthersCategoryID, string LocationCategoryID, int? hospitalUnitID, int? firstYear, int? lastYear, int? firstMonth, int? lastMonth, int? bedInUnitFrom, int optBedInUnitFrom, int? bedInUnitTo, int optBedInUnitTo, float? budgetedPatientFrom, int optBudgetedPatientFrom, float? budgetedPatientTo, int optBudgetedPatientTo, string startDate, string endDate, int? electronicDocumentFrom, int optElectronicDocumentFrom, int? electronicDocumentTo, int optElectronicDocumentTo, int docByException, string unitType, string pharmacyType, string hospitalType, int optHospitalSizeFrom, int? hospitalSizeFrom, int optHospitalSizeTo, int? hospitalSizeTo, int? countryId, int? stateId, string activities, string value, string others, string location, int? dataPointsFrom, int optDataPointsFrom, int? dataPointsTo, int optdataPointsTo, string configName, string unitIds)
        {
            try
            {
                List<RMC.BusinessEntities.BEReports> objectGenericListBEReportValueAdded = null;
                objectGenericListBEReportValueAdded = GetDataForHospitalBenchmarkSummaryTestNew(dbValues, ProfileCategoryValue, ProfileSubCategoryValue, valueAddedCategoryID, OthersCategoryID, LocationCategoryID, hospitalUnitID, firstYear, lastYear, firstMonth, lastMonth, bedInUnitFrom, optBedInUnitFrom, bedInUnitTo, optBedInUnitTo, budgetedPatientFrom, optBudgetedPatientFrom, budgetedPatientTo, optBudgetedPatientTo, startDate, endDate, electronicDocumentFrom, optElectronicDocumentFrom, electronicDocumentTo, optElectronicDocumentTo, docByException, unitType, pharmacyType, hospitalType, optHospitalSizeFrom, hospitalSizeFrom, optHospitalSizeTo, hospitalSizeTo, countryId, stateId, activities, value, others, location, dataPointsFrom, optDataPointsFrom, dataPointsTo, optdataPointsTo, configName, unitIds);
                

                removeIncDecSymbol<RMC.BusinessEntities.BEReports>("ColumnName", ref objectGenericListBEReportValueAdded);
                System.Data.DataTable dt = null;
                dt = AddRows(CreateTable(objectGenericListBEReportValueAdded), objectGenericListBEReportValueAdded);

                HttpContext.Current.Session["dtUnitId"] = dt;
                return dt;

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                //DisposeGlobalObjects();
            }
        }

       // public System.Data.DataTable CalculateData(string dbValues, string ProfileCategoryValue, string ProfileSubCategoryValue, string valueAddedCategoryID, string OthersCategoryID, string LocationCategoryID, int? hospitalUnitID, int? firstYear, int? lastYear, int? firstMonth, int? lastMonth, int? bedInUnitFrom, int optBedInUnitFrom, int? bedInUnitTo, int optBedInUnitTo, float? budgetedPatientFrom, int optBudgetedPatientFrom, float? budgetedPatientTo, int optBudgetedPatientTo, string startDate, string endDate, int? electronicDocumentFrom, int optElectronicDocumentFrom, int? electronicDocumentTo, int optElectronicDocumentTo, int docByException, string unitType, string pharmacyType, string hospitalType, int optHospitalSizeFrom, int? hospitalSizeFrom, int optHospitalSizeTo, int? hospitalSizeTo, int? countryId, int? stateId, string activities, string value, string others, string location, int? dataPointsFrom, int optDataPointsFrom, int? dataPointsTo, int optdataPointsTo, string configName)
         public System.Data.DataTable CalculateData()    
         {
            try
            {
                DataTable dtTopQuritile = (DataTable) HttpContext.Current.Session["dtTopQuritile"];
                DataTable dtUnitId = (DataTable)HttpContext.Current.Session["dtUnitId"];
                //DataTable dtresult = (DataTable)HttpContext.Current.Session["dtTopQuritile"];
                DataTable dtresult = new DataTable();
                DataRow dr = dtresult.NewRow();
                //for database value 08-11-2011
                //int rowcount = dtUnitId.Rows.Count;
                //int colcount = dtUnitId.Columns.Count;
                for (int i = 0; i < dtUnitId.Rows.Count; i++)
                {
                    for (int j = 0; j < dtUnitId.Columns.Count; j++)
                    {
                        if (dtUnitId.Columns[j].ToString().Trim().ToLower() == "email" && dtUnitId.Rows[i][j] == System.DBNull.Value)
                        {
                            dtUnitId.Rows[i][j] = "0";
                        }
                        if (dtUnitId.Columns[j].ToString().Trim().ToLower() == "hospital_unit" && dtUnitId.Rows[i][j]==System.DBNull.Value)
                        {
                            dtUnitId.Rows[i].Delete();
                        }                        
                    }
                }
                for (int i = 0; i < dtUnitId.Rows.Count; i++)
                {
                    for (int j = 0; j < dtUnitId.Columns.Count; j++)
                    {
                        if (dtUnitId.Rows[i][j] == System.DBNull.Value)
                        {
                            dtUnitId.Columns.Remove(dtUnitId.Columns[j]);
                            j = j - 1;
                        }
                    }
                }
                //end
                int col = dtUnitId.Columns.Count;
                int col1=dtTopQuritile.Columns.Count;
                for (int i = 3; i < col; i++)
                {
                    string val = dtUnitId.Rows[0][i].ToString();
                    string[] val1 = val.Split('%');
                    //string a = val1[0];
                    
                    dtUnitId.Rows[0][i] = (12 * 60 * Convert.ToDecimal(val1[0])) / 100;
                }

                for (int i = 1; i < col; i++)
                {
                   
                    for (int j = 1; j < col1; j++)
                    {
                        if (dtUnitId.Columns[i].ToString() == dtTopQuritile.Columns[j].ToString())
                        {
                            dtresult.Columns.Add(dtTopQuritile.Columns[j].ColumnName.ToString());
                          //  dtresult.Rows.Add(Convert.ToDecimal(dtUnitId.Rows[0][i]) - Convert.ToDecimal(dtTopQuritile.Rows[0][j]));
                           // dtresult.Rows.Add(Convert.ToDecimal(dtUnitId.Rows[0][i]) - Convert.ToDecimal(dtTopQuritile.Rows[1][j]));
                            //dtresult.Rows[0][j] = Convert.ToDecimal(dtUnitId.Rows[0][i]) - Convert.ToDecimal(dtTopQuritile.Rows[0][j]);
                            //dtresult.Rows[1][j] = Convert.ToDecimal(dtUnitId.Rows[0][i]) - Convert.ToDecimal(dtTopQuritile.Rows[1][j]);
                        }
                    }
                }
                int k = 0;
                for (int i = 1; i < col; i++)
                {

                    for (int j = 1; j < col1; j++)
                    {
                        if (dtUnitId.Columns[i].ToString() == dtTopQuritile.Columns[j].ToString())
                        {

                           
                            //dr[k] = (Convert.ToDecimal(dtUnitId.Rows[0][i]) - Convert.ToDecimal(dtTopQuritile.Rows[0][j]));
                            dr[k] = Convert.ToDecimal(dtUnitId.Rows[0][i]);
                            k += 1;                           
                        }
                    }
                }
                dtresult.Rows.Add(dr);
                HttpContext.Current.Session["dtresult"] = dtresult;
                return dtresult;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                //DisposeGlobalObjects();
            }
        }
        /// <summary>
        /// Gets a Row from BenchmarkFilter According to the Parameter Value
        /// </summary>
        public RMC.DataService.BenchmarkFilter GetBenchmarkFilterRow(int filterID, string filterName)
        {
            _objectRMCDataContext = new RMC.DataService.RMCDataContext();
            RMC.DataService.BenchmarkFilter objectBenchmarkFilter = new RMC.DataService.BenchmarkFilter();

            objectBenchmarkFilter = (from bf in _objectRMCDataContext.BenchmarkFilters
                                     where bf.FilterId == filterID && bf.FilterName == filterName
                                     select bf).FirstOrDefault();

            return objectBenchmarkFilter;
        }

        /// <summary>
        /// To get names of Benchmarking filter from BenchmarkingFilter table
        /// </summary>
        /// <returns></returns>
        public List<RMC.DataService.BenchmarkFilter> GetBenchmarkFilterNames()
        {
            try
            {
                _objectRMCDataContext = new RMC.DataService.RMCDataContext();

                List<RMC.DataService.BenchmarkFilter> objectGenericBenchmarkFilter = _objectRMCDataContext.BenchmarkFilters.ToList();

                return objectGenericBenchmarkFilter;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {

            }
        }
        public bool FilterNameIsExist(string filtername)
        {
            bool flack = false;
            //List<RMC.DataService.BenchmarkFilter> objectGenericBenchmarkFilter = _objectRMCDataContext.BenchmarkFilters.ToList();
            //var obj = (from p in objectRMCDataContext.HospitalDemographicInfos

            //           where p.HospitalDemographicID == unitid
            //           select p.HospitalUnitName).FirstOrDefault();
            //return obj;
            var obj = (from p in _objectRMCDataContext.BenchmarkFilters
                       where p.FilterName == filtername
                       select p.FilterId).FirstOrDefault();
            if (obj != 0)
            {
                flack = true;
            }
            return flack;
        }
        /// <summary>
        /// Gets data for timeRNSummary Report for all three profiles category
        /// </summary>
        public List<RMC.BusinessEntities.BEReports> GetDataForTimeRNSummaryTest(string activitiesID, string valueAddedCategoryID, string OthersCategoryID, string LocationCategoryID, int? hospitalUnitID, int? firstYear, int? lastYear, int? firstMonth, int? lastMonth, int? bedInUnitFrom, int optBedInUnitFrom, int? bedInUnitTo, int optBedInUnitTo, float? budgetedPatientFrom, int optBudgetedPatientFrom, float? budgetedPatientTo, int optBudgetedPatientTo, string startDate, string endDate, int? electronicDocumentFrom, int optElectronicDocumentFrom, int? electronicDocumentTo, int optElectronicDocumentTo, int docByException, string unitType, string pharmacyType, string hospitalType, int optHospitalSizeFrom, int? hospitalSizeFrom, int optHospitalSizeTo, int? hospitalSizeTo, int? countryId, int? stateId, string activities, string value, string others, string location, int? dataPointsFrom, int optDataPointsFrom, int? dataPointsTo, int optdataPointsTo, string configName)
        {
            try
            {
                List<RMC.BusinessEntities.BEReports> objectGenericListBEReportValueAddedProfile = null;
                List<RMC.BusinessEntities.BEReports> objectGenericListBEReportValueAdded = null;
                List<RMC.BusinessEntities.BEReports> objectGenericListBEReportOthers = null;
                List<RMC.BusinessEntities.BEReports> objectGenericListBEReportLocation = null;
                //List<RMC.BusinessEntities.BEValidation> ObjectSearchHospitalsDataForBenchmark = SearchHospitalsDataForBenchmarkTest(hospitalUnitID, firstYear, lastYear, firstMonth, lastMonth, bedInUnitFrom, optBedInUnitFrom, bedInUnitTo, optBedInUnitTo, budgetedPatientFrom, optBudgetedPatientFrom, budgetedPatientTo, optBudgetedPatientTo, startDate, endDate, electronicDocumentFrom, optElectronicDocumentFrom, electronicDocumentTo, optElectronicDocumentTo, docByException, unitType, pharmacyType, hospitalType, optHospitalSizeFrom, hospitalSizeFrom, optHospitalSizeTo, hospitalSizeTo, countryId, stateId, configName);
                List<RMC.BusinessEntities.BEValidation> ObjectSearchHospitalsDataForBenchmark = SearchHospitalsDataForBenchmarkTest(hospitalUnitID, firstYear, lastYear, firstMonth, lastMonth, null, 0, null, 0, null, 0, null, 0, null, null, null, 0, null, 0, 0, null, null, null, 0, null, 0, null, null, null, null,null);

                //for Activity change CalculationOnDataByProfileTest
                if (activitiesID != null)
                {
                    if (activitiesID.Contains(","))
                    {
                        string[] strArrLocation = null;
                        strArrLocation = activities.Split(new char[] { ',' });
                        string[] strArrLocationCategoryID = null;
                        strArrLocationCategoryID = activitiesID.Split(new char[] { ',' });
                        int[] intArrLocationCategoryID = new int[strArrLocationCategoryID.Length];
                        for (int i = 0; i < strArrLocationCategoryID.Length; i++)
                        {
                            intArrLocationCategoryID[i] = int.Parse(strArrLocationCategoryID[i]);
                            objectGenericListBEReportLocation = CalculationForTimeRNSummaryTest(strArrLocation[i], "activities", GetCategoryProfileDataByProfileID(intArrLocationCategoryID[i]), ObjectSearchHospitalsDataForBenchmark, dataPointsFrom, optDataPointsFrom, dataPointsTo, optdataPointsTo);
                            //objectGenericListBEReportValueAdded.AddRange(objectGenericListBEReportLocation);
                            if (objectGenericListBEReportValueAdded == null)
                            {
                                objectGenericListBEReportValueAdded = objectGenericListBEReportLocation;
                            }
                            else
                            {
                                objectGenericListBEReportValueAdded.AddRange(objectGenericListBEReportLocation);
                            }
                        }
                    }
                    else
                    {
                        objectGenericListBEReportLocation =CalculationForTimeRNSummaryTest (activities, "activities", GetCategoryProfileDataByProfileID(Convert.ToInt32(activitiesID)), ObjectSearchHospitalsDataForBenchmark, dataPointsFrom, optDataPointsFrom, dataPointsTo, optdataPointsTo);
                        //objectGenericListBEReportValueAdded.AddRange(objectGenericListBEReportLocation);
                        if (objectGenericListBEReportValueAdded == null)
                        {
                            objectGenericListBEReportValueAdded = objectGenericListBEReportLocation;
                        }
                        else
                        {
                            objectGenericListBEReportValueAdded.AddRange(objectGenericListBEReportLocation);
                        }
                    }
                }
                //valueAddedCategoryID
                if (valueAddedCategoryID != null)
                {
                    if (valueAddedCategoryID.Contains(","))
                    {
                        string[] strArrvalue = null;
                        strArrvalue = value.Split(new char[] { ',' });
                        string[] strArrvalueAddedCategoryID = null;
                        strArrvalueAddedCategoryID = valueAddedCategoryID.Split(new char[] { ',' });
                        int[] intArrvalueAddedCategoryID = new int[strArrvalueAddedCategoryID.Length];
                        for (int i = 0; i < strArrvalueAddedCategoryID.Length; i++)
                        {
                            intArrvalueAddedCategoryID[i] = int.Parse(strArrvalueAddedCategoryID[i]);
                            //objectGenericListBEReportValueAddedProfile = CalculationForTimeRNSummaryTest(strArrvalue[i], "value added", GetCategoryProfileDataByProfileID(intArrvalueAddedCategoryID[i]), ObjectSearchHospitalsDataForBenchmark, dataPointsFrom, optDataPointsFrom, dataPointsTo, optdataPointsTo);
                            objectGenericListBEReportValueAddedProfile = CalculationForTimeRNSummaryTest(strArrvalue[i], "value added", GetCategoryProfileDataByProfileID(intArrvalueAddedCategoryID[i]), ObjectSearchHospitalsDataForBenchmark, null, 0, null, 0);
                            if (i == 0)
                            {
                                objectGenericListBEReportValueAdded = objectGenericListBEReportValueAddedProfile;
                            }
                            if (i != 0)
                            {
                                objectGenericListBEReportValueAdded.AddRange(objectGenericListBEReportValueAddedProfile);
                            }
                        }
                    }
                    else
                    {
                        //objectGenericListBEReportValueAdded = CalculationForTimeRNSummaryTest(value, "value added", GetCategoryProfileDataByProfileID(Convert.ToInt32(valueAddedCategoryID)), ObjectSearchHospitalsDataForBenchmark, dataPointsFrom, optDataPointsFrom, dataPointsTo, optdataPointsTo);
                        objectGenericListBEReportValueAdded = CalculationForTimeRNSummaryTest(value, "value added", GetCategoryProfileDataByProfileID(Convert.ToInt32(valueAddedCategoryID)), ObjectSearchHospitalsDataForBenchmark, null, 0, null, 0);
                    }
                    //objectGenericListBEReportValueAdded = objectGenericListBEReportValueAdded.OrderBy(s => s.ColumnName).ToList();
                }

                //OthersCategoryID
                if (OthersCategoryID != null)
                {
                    if (OthersCategoryID.Contains(","))
                    {
                        string[] strArrothers = null;
                        strArrothers = others.Split(new char[] { ',' });
                        string[] strArrOthersCategoryID = null;
                        strArrOthersCategoryID = OthersCategoryID.Split(new char[] { ',' });
                        int[] intArrOthersCategoryID = new int[strArrOthersCategoryID.Length];
                        for (int i = 0; i < strArrOthersCategoryID.Length; i++)
                        {
                            intArrOthersCategoryID[i] = int.Parse(strArrOthersCategoryID[i]);
                            //objectGenericListBEReportOthers = CalculationForTimeRNSummaryTest(strArrothers[i], "others", GetCategoryProfileDataByProfileID(intArrOthersCategoryID[i]), ObjectSearchHospitalsDataForBenchmark, dataPointsFrom, optDataPointsFrom, dataPointsTo, optdataPointsTo);
                            objectGenericListBEReportOthers = CalculationForTimeRNSummaryTest(strArrothers[i], "others", GetCategoryProfileDataByProfileID(intArrOthersCategoryID[i]), ObjectSearchHospitalsDataForBenchmark, null, 0, null, 0);
                            //objectGenericListBEReportValueAdded.AddRange(objectGenericListBEReportOthers);
                            if (objectGenericListBEReportValueAdded == null)
                            {
                                objectGenericListBEReportValueAdded = objectGenericListBEReportOthers;
                            }
                            else
                            {
                                objectGenericListBEReportValueAdded.AddRange(objectGenericListBEReportOthers);
                            }
                        }
                    }
                    else
                    {
                        //objectGenericListBEReportOthers = CalculationForTimeRNSummaryTest(others, "others", GetCategoryProfileDataByProfileID(Convert.ToInt32(OthersCategoryID)), ObjectSearchHospitalsDataForBenchmark, dataPointsFrom, optDataPointsFrom, dataPointsTo, optdataPointsTo);
                        objectGenericListBEReportOthers = CalculationForTimeRNSummaryTest(others, "others", GetCategoryProfileDataByProfileID(Convert.ToInt32(OthersCategoryID)), ObjectSearchHospitalsDataForBenchmark, null, 0, null, 0);
                        if (objectGenericListBEReportValueAdded == null)
                        {
                            objectGenericListBEReportValueAdded = objectGenericListBEReportOthers;
                        }
                        else
                        {
                            objectGenericListBEReportValueAdded.AddRange(objectGenericListBEReportOthers);
                        }
                    }
                    //objectGenericListBEReportValueAdded = objectGenericListBEReportValueAdded.OrderBy(s => s.ColumnName).ToList();
                }

                //LocationCategoryID;
                if (LocationCategoryID != null)
                {
                    if (LocationCategoryID.Contains(","))
                    {
                        string[] strArrLocation = null;
                        strArrLocation = location.Split(new char[] { ',' });
                        string[] strArrLocationCategoryID = null;
                        strArrLocationCategoryID = LocationCategoryID.Split(new char[] { ',' });
                        int[] intArrLocationCategoryID = new int[strArrLocationCategoryID.Length];
                        for (int i = 0; i < strArrLocationCategoryID.Length; i++)
                        {
                            intArrLocationCategoryID[i] = int.Parse(strArrLocationCategoryID[i]);
                            //objectGenericListBEReportLocation = CalculationForTimeRNSummaryTest(strArrLocation[i], "location", GetCategoryProfileDataByProfileID(intArrLocationCategoryID[i]), ObjectSearchHospitalsDataForBenchmark, dataPointsFrom, optDataPointsFrom, dataPointsTo, optdataPointsTo);
                            objectGenericListBEReportLocation = CalculationForTimeRNSummaryTest(strArrLocation[i], "location", GetCategoryProfileDataByProfileID(intArrLocationCategoryID[i]), ObjectSearchHospitalsDataForBenchmark, null, 0, null, 0);
                            //objectGenericListBEReportValueAdded.AddRange(objectGenericListBEReportLocation);
                            if (objectGenericListBEReportValueAdded == null)
                            {
                                objectGenericListBEReportValueAdded = objectGenericListBEReportLocation;
                            }
                            else
                            {
                                objectGenericListBEReportValueAdded.AddRange(objectGenericListBEReportLocation);
                            }
                        }
                    }
                    else
                    {
                        //objectGenericListBEReportLocation = CalculationForTimeRNSummaryTest(location, "location", GetCategoryProfileDataByProfileID(Convert.ToInt32(LocationCategoryID)), ObjectSearchHospitalsDataForBenchmark, dataPointsFrom, optDataPointsFrom, dataPointsTo, optdataPointsTo);
                        objectGenericListBEReportLocation = CalculationForTimeRNSummaryTest(location, "location", GetCategoryProfileDataByProfileID(Convert.ToInt32(LocationCategoryID)), ObjectSearchHospitalsDataForBenchmark, null, 0, null, 0);
                        if (objectGenericListBEReportValueAdded == null)
                        {
                            objectGenericListBEReportValueAdded = objectGenericListBEReportLocation;
                        }
                        else
                        {
                            objectGenericListBEReportValueAdded.AddRange(objectGenericListBEReportLocation);
                        }
                    }
                    //objectGenericListBEReportValueAdded = objectGenericListBEReportValueAdded.OrderBy(s => s.ColumnName).ToList();
                }


                //objectGenericListBEReportValueAdded = CalculationForTimeRNSummary("value added", GetCategoryProfileDataByProfileID(valueAddedCategoryID.Value), SearchHospitalsDataForBenchmark(firstYear, lastYear, firstMonth, lastMonth, hospitalUnitID, bedInUnit, optBedInUnit, budgetedPatient, optBudgetedPatient, startDate, endDate, electronicDocument, optElectronicDocument, docByException, unitType, pharmacyType, optHospitalSize, hospitalSize));
                //objectGenericListBEReportOthers = CalculationForTimeRNSummary("others", GetCategoryProfileDataByProfileID(OthersCategoryID.Value), SearchHospitalsDataForBenchmark(firstYear, lastYear, firstMonth, lastMonth, hospitalUnitID, bedInUnit, optBedInUnit, budgetedPatient, optBudgetedPatient, startDate, endDate, electronicDocument, optElectronicDocument, docByException, unitType, pharmacyType, optHospitalSize, hospitalSize));
                //objectGenericListBEReportLocation = CalculationForTimeRNSummary("location", GetCategoryProfileDataByProfileID(LocationCategoryID.Value), SearchHospitalsDataForBenchmark(firstYear, lastYear, firstMonth, lastMonth, hospitalUnitID, bedInUnit, optBedInUnit, budgetedPatient, optBudgetedPatient, startDate, endDate, electronicDocument, optElectronicDocument, docByException, unitType, pharmacyType, optHospitalSize, hospitalSize));
                //objectGenericListBEReportValueAdded.AddRange(objectGenericListBEReportOthers);
                //objectGenericListBEReportValueAdded.AddRange(objectGenericListBEReportLocation);

                //double dataPoint = 0;
                List<RMC.BusinessEntities.BEReports> objectBEReportsResultant = (from r in objectGenericListBEReportValueAdded.GroupBy(o => o.ColumnName)

                                                                                 select new RMC.BusinessEntities.BEReports
                                                                                 {
                                                                                     ColumnName = r.FirstOrDefault().ColumnName,
                                                                                     MonthName = "Hosp Avg",
                                                                                     Values = string.Format("{0:#.##}%", (r.Sum(x => x.ValuesSum) == 0) ? "0.00" : string.Format("{0:#.##}", r.Sum(x => x.ValuesSum) / r.Count())),
                                                                                     ValuesSum = (r.Sum(x => x.ValuesSum) == 0) ? 0.00 : r.Sum(x => x.ValuesSum) / r.Count(),
                                                                                     //DataPoint =  (dataPoint += r.FirstOrDefault().DataPoint)
                                                                                 }).ToList();

                List<double> objectDataPoint = (from r in objectGenericListBEReportValueAdded.GroupBy(o => o.MonthName)

                                                select r.FirstOrDefault().DataPoint
                                    ).ToList();
                //RMC.BusinessEntities.BEReports objectNewBEReports = objectBEReportsResultant.Find(delegate(RMC.BusinessEntities.BEReports objectBERep)
                //{
                //    return objectBERep.MonthName == "Hosp Avg";
                //});

                objectBEReportsResultant.ForEach(delegate(RMC.BusinessEntities.BEReports objectBERep)
                {
                    objectBERep.DataPoint = Convert.ToDouble(string.Format("{0:#.##}", objectDataPoint.Sum() /* / objectDataPoint.Count*/));
                });

                //if (objectNewBEReports != null)
                //{
                //    objectNewBEReports.DataPoint = objectDataPoint.Sum()/objectDataPoint.Count;
                //}

                objectGenericListBEReportValueAdded.AddRange(objectBEReportsResultant);
                return objectGenericListBEReportValueAdded;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Gets data for timeRNSummary Report for all three profiles category
        /// </summary>
        public System.Data.DataTable GetDataForTimeRNSummaryGrid(string activitiesID, string valueAddedCategoryID, string OthersCategoryID, string LocationCategoryID, int? hospitalUnitID, int? firstYear, int? lastYear, int? firstMonth, int? lastMonth, int? bedInUnitFrom, int optBedInUnitFrom, int? bedInUnitTo, int optBedInUnitTo, float? budgetedPatientFrom, int optBudgetedPatientFrom, float? budgetedPatientTo, int optBudgetedPatientTo, string startDate, string endDate, int? electronicDocumentFrom, int optElectronicDocumentFrom, int? electronicDocumentTo, int optElectronicDocumentTo, int docByException, string unitType, string pharmacyType, string hospitalType, int optHospitalSizeFrom, int? hospitalSizeFrom, int optHospitalSizeTo, int? hospitalSizeTo, int? countryId, int? stateId, string activities, string value, string others, string location, int? dataPointsFrom, int optDataPointsFrom, int? dataPointsTo, int optdataPointsTo, string configName)
        {
            try
            {
                List<RMC.BusinessEntities.BEReports> objectGenericListBEReportValueAdded = null;
                objectGenericListBEReportValueAdded = GetDataForTimeRNSummaryTest(activitiesID, valueAddedCategoryID, OthersCategoryID, LocationCategoryID, hospitalUnitID, firstYear, lastYear, firstMonth, lastMonth, bedInUnitFrom, optBedInUnitFrom, bedInUnitTo, optBedInUnitTo, budgetedPatientFrom, optBudgetedPatientFrom, budgetedPatientTo, optBudgetedPatientTo, startDate, endDate, electronicDocumentFrom, optElectronicDocumentFrom, electronicDocumentTo, optElectronicDocumentTo, docByException, unitType, pharmacyType, hospitalType, optHospitalSizeFrom, hospitalSizeFrom, optHospitalSizeTo, hospitalSizeTo, countryId, stateId, activities, value, others, location, dataPointsFrom, optDataPointsFrom, dataPointsTo, optdataPointsTo, configName);
                
                System.Data.DataTable dt = null;
                dt = AddRowsTimeRN(CreateTableTimeRN(objectGenericListBEReportValueAdded), objectGenericListBEReportValueAdded);
                return dt;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Call by GetDataForTimeRNSummaryGrid, it calculates data for timeRNSummary report
        /// </summary>
        private List<RMC.BusinessEntities.BEReports> CalculationForTimeRNSummaryTest(string profileName, string profileCategory, List<RMC.BusinessEntities.BECategoryProfile> objectGenericBECategoryProfile, List<RMC.BusinessEntities.BEValidation> objectGenericBEValidation, int? dataPointsFrom, int optDataPointsFrom, int? dataPointsTo, int optdataPointsTo)
        {
            try
            {
                int totalCount = 0;
                List<RMC.DataService.ValueAddedType> objectGenericValueAddedType = _objectRMCDataContext.ValueAddedTypes.OrderBy(o => o.TypeID).ToList();
                List<RMC.DataService.CategoryGroup> objectGenericCategoryGroup = _objectRMCDataContext.CategoryGroups.OrderBy(o => o.CategoryGroupID).ToList();
                List<RMC.DataService.ActivitiesCategory> objectGenericActivities = _objectRMCDataContext.ActivitiesCategories.OrderBy(o => o.ActivitiesID).ToList();
                List<RMC.DataService.LocationCategory> objectGenericLocationCategory = _objectRMCDataContext.LocationCategories.OrderBy(o => o.LocationID).ToList();
                List<RMC.BusinessEntities.BEReports> objectGenericBEReports = null;
                //objectGenericBEValidation.ForEach(delegate(RMC.BusinessEntities.BEValidation objectBEValidation)
                //{
                //    objectBEValidation.CategoryGroupID = objectGenericBECategoryProfile.Find(delegate(RMC.BusinessEntities.BECategoryProfile objectBECategoryProfile)
                //    {
                //        return objectBECategoryProfile.LocationID == objectBEValidation.LocationID && objectBECategoryProfile.ActivityID == objectBEValidation.ActivityID && objectBECategoryProfile.SubActivityID == objectBEValidation.SubActivityID;
                //    }).CategoryAssignmentID.Value;
                //});

                objectGenericBECategoryProfile.ForEach(delegate(RMC.BusinessEntities.BECategoryProfile objectBECategoryProfile)
                {
                    objectGenericBEValidation.FindAll(delegate(RMC.BusinessEntities.BEValidation objectBEValidation)
                    {
                        if (objectBEValidation.SubActivityID > 0 && objectBECategoryProfile.SubActivityID > 0)
                        {
                            return objectBECategoryProfile.LocationID == objectBEValidation.LocationID && objectBECategoryProfile.ActivityID == objectBEValidation.ActivityID && objectBECategoryProfile.SubActivityID == objectBEValidation.SubActivityID;
                        }
                        else
                        {
                            return objectBECategoryProfile.LocationID == objectBEValidation.LocationID && objectBECategoryProfile.ActivityID == objectBEValidation.ActivityID;                            
                        }
                    }).TrueForAll(delegate(RMC.BusinessEntities.BEValidation objectBEValidationAnother)
                    {
                        objectBEValidationAnother.CategoryGroupID = objectBECategoryProfile.CategoryAssignmentID;
                        return true;
                    });
                });

                totalCount = objectGenericBEValidation.Count;


                if (profileCategory == "value added")
                {
                    objectGenericBEValidation = objectGenericBEValidation.FindAll(delegate(RMC.BusinessEntities.BEValidation objectNewBEValidation)
                    {
                        return objectGenericValueAddedType.Exists(delegate(RMC.DataService.ValueAddedType objectValueAddedType)
                        {
                            return objectValueAddedType.TypeID == objectNewBEValidation.CategoryGroupID;
                        });
                    });
                    //chanderm
                    //=================Again Modified=============================
                    //changing method to include year with month also in report
                    objectGenericBEReports = (from v in objectGenericBEValidation.OrderBy(o => o.Year).ThenBy(p => p.MonthIndex)
                                              group v by new { v.Year, v.MonthIndex } into g
                                              from t in g.GroupBy(x => x.CategoryGroupID).ToList()
                                              select new RMC.BusinessEntities.BEReports
                                              {
                                                  ColumnName = objectGenericValueAddedType.Find(delegate(RMC.DataService.ValueAddedType objectValueAddedType)
                                                  {
                                                      return objectValueAddedType.TypeID == t.Key;
                                                  }).TypeName + " (" + profileName + ")",
                                                  ColumnNumber = objectGenericValueAddedType.Find(delegate(RMC.DataService.ValueAddedType objectValueAddedType)
                                                  {
                                                      return objectValueAddedType.TypeID == t.Key;
                                                  }).TypeID,
                                                  MonthName = BSCommon.GetMonthName(t.FirstOrDefault(r => r.Month != "").Month) + "_" + t.FirstOrDefault().Year,
                                                  RowName = t.FirstOrDefault(x => x.HospitalUnitName != "").HospitalUnitName,
                                                  Values = string.Format("{0:#.##}%", ((double)t.Count(r => r.CategoryGroupID == t.Key) / (double)g.Count(c => c.CategoryGroupID != 0)) * 100),
                                                  ValuesSum = Convert.ToDouble(string.Format("{0:#.##}", ((double)t.Count(r => r.CategoryGroupID == t.Key) / (double)g.Count(c => c.CategoryGroupID != 0)) * 100)),
                                                  DataPoint = (double)g.Count(c => c.CategoryGroupID != 0)
                                              }).ToList();


                }
                else if (profileCategory == "others")
                {
                    objectGenericBEValidation = objectGenericBEValidation.FindAll(delegate(RMC.BusinessEntities.BEValidation objectNewBEValidation)
                    {
                        return objectGenericCategoryGroup.Exists(delegate(RMC.DataService.CategoryGroup objectCategoryGroup)
                        {
                            return objectCategoryGroup.CategoryGroupID == objectNewBEValidation.CategoryGroupID;
                        });
                    });
                    //=================Again Modified=============================
                    //changing method to include year with month also in report
                    objectGenericBEReports = (from v in objectGenericBEValidation.OrderBy(o => o.Year).ThenBy(p => p.MonthIndex)
                                              group v by new { v.Year, v.MonthIndex } into g
                                              from t in g.GroupBy(x => x.CategoryGroupID).ToList()
                                              select new RMC.BusinessEntities.BEReports
                                              {
                                                  ColumnName = objectGenericCategoryGroup.Find(delegate(RMC.DataService.CategoryGroup objectCategoryGroup)
                                                  {
                                                      return objectCategoryGroup.CategoryGroupID == t.Key;
                                                  }).CategoryGroup1 + " (" + profileName + ")",
                                                  ColumnNumber = objectGenericCategoryGroup.Find(delegate(RMC.DataService.CategoryGroup objectCategoryGroup)
                                                  {
                                                      return objectCategoryGroup.CategoryGroupID == t.Key;
                                                  }).CategoryGroupID,
                                                  MonthName = BSCommon.GetMonthName(t.FirstOrDefault(r => r.Month != "").Month) + "_" + t.FirstOrDefault().Year,
                                                  RowName = t.FirstOrDefault(r => r.HospitalUnitName != "").HospitalUnitName,
                                                  Values = string.Format("{0:#.##}%", ((double)t.Count(r => r.CategoryGroupID == t.Key) / (double)g.Count(c => c.CategoryGroupID != 0)) * 100),
                                                  ValuesSum = Convert.ToDouble(string.Format("{0:#.##}", ((double)t.Count(r => r.CategoryGroupID == t.Key) / (double)g.Count(c => c.CategoryGroupID != 0)) * 100)),
                                                  DataPoint = (double)g.Count(c => c.CategoryGroupID != 0)
                                              }).ToList();


                   
                }
                    //
                else if (profileCategory == "activities")
                {
                    objectGenericBEValidation = objectGenericBEValidation.FindAll(delegate(RMC.BusinessEntities.BEValidation objectNewBEValidation)
                    {
                        return objectGenericActivities.Exists(delegate(RMC.DataService.ActivitiesCategory objectActivities)
                        {
                            return objectActivities.ActivitiesID == objectNewBEValidation.CategoryGroupID;
                        });
                    });
                    //=================Again Modified=============================
                    //changing method to include year with month also in report
                    objectGenericBEReports = (from v in objectGenericBEValidation.OrderBy(o => o.Year).ThenBy(p => p.MonthIndex)
                                              group v by new { v.Year, v.MonthIndex } into g
                                              from t in g.GroupBy(x => x.CategoryGroupID).ToList()
                                              select new RMC.BusinessEntities.BEReports
                                              {
                                                  ColumnName = objectGenericActivities.Find(delegate(RMC.DataService.ActivitiesCategory objectActivities)
                                                  {
                                                      return objectActivities.ActivitiesID == t.Key;
                                                  }).ActivitiesCategory1 + " (" + profileName + ")",
                                                  ColumnNumber = objectGenericActivities.Find(delegate(RMC.DataService.ActivitiesCategory objectActivities)
                                                  {
                                                      return objectActivities.ActivitiesID == t.Key;
                                                  }).ActivitiesID,
                                                  MonthName = BSCommon.GetMonthName(t.FirstOrDefault(r => r.Month != "").Month) + "_" + t.FirstOrDefault().Year,
                                                  RowName = t.FirstOrDefault(r => r.HospitalUnitName != "").HospitalUnitName,
                                                  Values = string.Format("{0:#.##}%", ((double)t.Count(r => r.CategoryGroupID == t.Key) / (double)g.Count(c => c.CategoryGroupID != 0)) * 100),
                                                  ValuesSum = Convert.ToDouble(string.Format("{0:#.##}", ((double)t.Count(r => r.CategoryGroupID == t.Key) / (double)g.Count(c => c.CategoryGroupID != 0)) * 100)),
                                                  DataPoint = (double)g.Count(c => c.CategoryGroupID != 0)
                                              }).ToList();



                }
                    //

                else
                {
                    objectGenericBEValidation = objectGenericBEValidation.FindAll(delegate(RMC.BusinessEntities.BEValidation objectNewBEValidation)
                    {
                        return objectGenericLocationCategory.Exists(delegate(RMC.DataService.LocationCategory objectLocationCategory)
                        {
                            return objectLocationCategory.LocationID == objectNewBEValidation.CategoryGroupID;
                        });
                    });

                    //=================Again Modified=============================
                    //changing method to include year with month also in report
                    objectGenericBEReports = (from v in objectGenericBEValidation.OrderBy(o => o.Year).ThenBy(p => p.MonthIndex)
                                              group v by new { v.Year, v.MonthIndex } into g
                                              from t in g.GroupBy(x => x.CategoryGroupID).ToList()
                                              select new RMC.BusinessEntities.BEReports
                                              {
                                                  ColumnName = objectGenericLocationCategory.Find(delegate(RMC.DataService.LocationCategory objectLocationCategory)
                                                  {
                                                      return objectLocationCategory.LocationID == t.Key;
                                                  }).LocationCategory1 + " (" + profileName + ")",
                                                  ColumnNumber = objectGenericLocationCategory.Find(delegate(RMC.DataService.LocationCategory objectLocationCategory)
                                                  {
                                                      return objectLocationCategory.LocationID == t.Key;
                                                  }).LocationID,
                                                  MonthName = BSCommon.GetMonthName(t.FirstOrDefault(r => r.Month != "").Month) + "_" + t.FirstOrDefault().Year,
                                                  RowName = t.FirstOrDefault(r => r.HospitalUnitName != "").HospitalUnitName,
                                                  Values = string.Format("{0:#.##}%", ((double)t.Count(r => r.CategoryGroupID == t.Key) / (double)g.Count(c => c.CategoryGroupID != 0)) * 100),
                                                  ValuesSum = Convert.ToDouble(string.Format("{0:#.##}", ((double)t.Count(r => r.CategoryGroupID == t.Key) / (double)g.Count(c => c.CategoryGroupID != 0)) * 100)),
                                                  DataPoint = (double)g.Count(c => c.CategoryGroupID != 0)
                                              }).ToList();
                }

                if (dataPointsFrom != null && dataPointsTo == null)
                {
                    if (optDataPointsFrom == 1)
                    {
                        objectGenericBEReports = objectGenericBEReports.Where(x => Convert.ToInt32(x.DataPoint) < dataPointsFrom).ToList();
                    }
                    if (optDataPointsFrom == 2)
                    {
                        objectGenericBEReports = objectGenericBEReports.Where(x => x.DataPoint > dataPointsFrom).ToList();
                    }
                    if (optDataPointsFrom == 3)
                    {
                        objectGenericBEReports = objectGenericBEReports.Where(x => x.DataPoint == dataPointsFrom).ToList();
                    }
                    if (optDataPointsFrom == 4)
                    {
                        objectGenericBEReports = objectGenericBEReports.Where(x => x.DataPoint >= dataPointsFrom).ToList();
                    }
                    if (optDataPointsFrom == 5)
                    {
                        objectGenericBEReports = objectGenericBEReports.Where(x => x.DataPoint <= dataPointsFrom).ToList();
                    }
                    if (optDataPointsFrom == 6)
                    {
                        objectGenericBEReports = objectGenericBEReports.Where(x => x.DataPoint != dataPointsFrom).ToList();
                    }
                }

                if ((optDataPointsFrom == 0 && optdataPointsTo == 0) && (dataPointsFrom != null && dataPointsTo != null))
                {
                    objectGenericBEReports = objectGenericBEReports.Where(x => x.DataPoint >= dataPointsFrom && x.DataPoint <= dataPointsTo).ToList();
                }

                objectGenericBEReports = objectGenericBEReports.OrderBy(o => o.ColumnName).ToList();
                return objectGenericBEReports;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Used By MonthlySummary-Dashboard Report, Calculates performance values for bar chart
        /// </summary>
        public List<RMC.BusinessEntities.BEFunctionNames> GetPerformanceTest(string activitiesID, string valueAddedCategoryID, string OthersCategoryID, string LocationCategoryID, int? hospitalUnitID, int? firstYear, int? lastYear, int? firstMonth, int? lastMonth, int? bedInUnitFrom, int optBedInUnitFrom, int? bedInUnitTo, int optBedInUnitTo, float? budgetedPatientFrom, int optBudgetedPatientFrom, float? budgetedPatientTo, int optBudgetedPatientTo, string startDate, string endDate, int? electronicDocumentFrom, int optElectronicDocumentFrom, int? electronicDocumentTo, int optElectronicDocumentTo, int docByException, string unitType, string pharmacyType, string hospitalType, int optHospitalSizeFrom, int? hospitalSizeFrom, int optHospitalSizeTo, int? hospitalSizeTo, int? countryId, int? stateId, string activities, string value, string others, string location, int? dataPointsFrom, int optDataPointsFrom, int? dataPointsTo, int optdataPointsTo, string configName, string unitIds)
        {
            try
            {
                //List<RMC.BusinessEntities.BENationalDatabase> objectGenericBENationalDatabase = GetNationalDatabaseForMultipleProfile(valueAddedCategoryID, OthersCategoryID, LocationCategoryID, hospitalUnitID, firstYear, lastYear, firstMonth, lastMonth, bedInUnitFrom, optBedInUnitFrom, bedInUnitTo, optBedInUnitTo, budgetedPatientFrom, optBudgetedPatientFrom, budgetedPatientTo, optBudgetedPatientTo, startDate, endDate, electronicDocumentFrom, optElectronicDocumentFrom, electronicDocumentTo, optElectronicDocumentTo, docByException, unitType, pharmacyType, hospitalType, optHospitalSizeFrom, hospitalSizeFrom, optHospitalSizeTo, hospitalSizeTo, countryId, stateId, value, others, location);
                //List<RMC.BusinessEntities.BEFunctionNames> objectGenericBEFunctionNames = CalculateFunctionValuesTest(valueAddedCategoryID, OthersCategoryID, LocationCategoryID, hospitalUnitID, firstYear, lastYear, firstMonth, lastMonth, bedInUnitFrom, optBedInUnitFrom, bedInUnitTo, optBedInUnitTo, budgetedPatientFrom, optBudgetedPatientFrom, budgetedPatientTo, optBudgetedPatientTo, startDate, endDate, electronicDocumentFrom, optElectronicDocumentFrom, electronicDocumentTo, optElectronicDocumentTo, docByException, unitType, pharmacyType, hospitalType, optHospitalSizeFrom, hospitalSizeFrom, optHospitalSizeTo, hospitalSizeTo, countryId, stateId, value, others, location);

                //Here hospitalUnitId is null because the chosen hospital unit is compared with global data or all hospital unit data (or National Database)
                List<RMC.BusinessEntities.BEFunctionNames> objectGenericBEFunctionNames = null;
                //if (hospitalUnitID == null)
                //{
                objectGenericBEFunctionNames = CalculateFunctionValuesTest(activitiesID, valueAddedCategoryID, OthersCategoryID, LocationCategoryID, null, firstYear, lastYear, firstMonth, lastMonth, bedInUnitFrom, optBedInUnitFrom, bedInUnitTo, optBedInUnitTo, budgetedPatientFrom, optBudgetedPatientFrom, budgetedPatientTo, optBudgetedPatientTo, startDate, endDate, electronicDocumentFrom, optElectronicDocumentFrom, electronicDocumentTo, optElectronicDocumentTo, docByException, unitType, pharmacyType, hospitalType, optHospitalSizeFrom, hospitalSizeFrom, optHospitalSizeTo, hospitalSizeTo, countryId, stateId, activities, value, others, location, dataPointsFrom, optDataPointsFrom, dataPointsTo, optdataPointsTo, configName, unitIds);
                //}
                //else
                //{
                //    objectGenericBEFunctionNames = CalculateFunctionValuesTest(valueAddedCategoryID, OthersCategoryID, LocationCategoryID, hospitalUnitID, firstYear, lastYear, firstMonth, lastMonth, bedInUnitFrom, optBedInUnitFrom, bedInUnitTo, optBedInUnitTo, budgetedPatientFrom, optBudgetedPatientFrom, budgetedPatientTo, optBudgetedPatientTo, startDate, endDate, electronicDocumentFrom, optElectronicDocumentFrom, electronicDocumentTo, optElectronicDocumentTo, docByException, unitType, pharmacyType, hospitalType, optHospitalSizeFrom, hospitalSizeFrom, optHospitalSizeTo, hospitalSizeTo, countryId, stateId, value, others, location, dataPointsFrom, optDataPointsFrom, dataPointsTo, optdataPointsTo, configName);
                //}

                //List<RMC.BusinessEntities.BEReports> objectGenericBEReports = GetDataForTimeRNSummaryTest(valueAddedCategoryID, OthersCategoryID, LocationCategoryID, hospitalUnitID, firstYear, lastYear, firstMonth, lastMonth, bedInUnitFrom, optBedInUnitFrom, bedInUnitTo, optBedInUnitTo, budgetedPatientFrom, optBudgetedPatientFrom, budgetedPatientTo, optBudgetedPatientTo, startDate, endDate, electronicDocumentFrom, optElectronicDocumentFrom, electronicDocumentTo, optElectronicDocumentTo, docByException, unitType, pharmacyType, hospitalType, optHospitalSizeFrom, hospitalSizeFrom, optHospitalSizeTo, hospitalSizeTo, countryId, stateId, value, others, location, dataPointsFrom, optDataPointsFrom, dataPointsTo, optdataPointsTo, configName);
                List<RMC.BusinessEntities.BEReports> objectGenericBEReports = GetDataForTimeRNSummaryTest(activitiesID, valueAddedCategoryID, OthersCategoryID, LocationCategoryID, hospitalUnitID, firstYear, lastYear, firstMonth, lastMonth, null, 0, null, 0, null, 0, null, 0, null, null, null, 0, null, 0, 0, null, null, null, 0, null, 0, null, null, null, activities, value, others, location, null, 0, null, 0, null);

                return CalculatePerformanceTest(objectGenericBEFunctionNames, objectGenericBEReports);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        //Overload Method.
        public List<RMC.BusinessEntities.BEFunctionNames> GetPerformanceTest(string activitiesID, string valueAddedCategoryID, string OthersCategoryID, string LocationCategoryID, int? hospitalUnitID, int? firstYear, int? lastYear, int? firstMonth, int? lastMonth, int? bedInUnitFrom, int optBedInUnitFrom, int? bedInUnitTo, int optBedInUnitTo, float? budgetedPatientFrom, int optBudgetedPatientFrom, float? budgetedPatientTo, int optBudgetedPatientTo, string startDate, string endDate, int? electronicDocumentFrom, int optElectronicDocumentFrom, int? electronicDocumentTo, int optElectronicDocumentTo, int docByException, string unitType, string pharmacyType, string hospitalType, int optHospitalSizeFrom, int? hospitalSizeFrom, int optHospitalSizeTo, int? hospitalSizeTo, int? countryId, int? stateId, string activities, string value, string others, string location, int? dataPointsFrom, int optDataPointsFrom, int? dataPointsTo, int optdataPointsTo, string configName, string unitIds, string reportName)
        {
            try
            {
                //List<RMC.BusinessEntities.BENationalDatabase> objectGenericBENationalDatabase = GetNationalDatabaseForMultipleProfile(valueAddedCategoryID, OthersCategoryID, LocationCategoryID, hospitalUnitID, firstYear, lastYear, firstMonth, lastMonth, bedInUnitFrom, optBedInUnitFrom, bedInUnitTo, optBedInUnitTo, budgetedPatientFrom, optBudgetedPatientFrom, budgetedPatientTo, optBudgetedPatientTo, startDate, endDate, electronicDocumentFrom, optElectronicDocumentFrom, electronicDocumentTo, optElectronicDocumentTo, docByException, unitType, pharmacyType, hospitalType, optHospitalSizeFrom, hospitalSizeFrom, optHospitalSizeTo, hospitalSizeTo, countryId, stateId, value, others, location);
                //List<RMC.BusinessEntities.BEFunctionNames> objectGenericBEFunctionNames = CalculateFunctionValuesTest(valueAddedCategoryID, OthersCategoryID, LocationCategoryID, hospitalUnitID, firstYear, lastYear, firstMonth, lastMonth, bedInUnitFrom, optBedInUnitFrom, bedInUnitTo, optBedInUnitTo, budgetedPatientFrom, optBudgetedPatientFrom, budgetedPatientTo, optBudgetedPatientTo, startDate, endDate, electronicDocumentFrom, optElectronicDocumentFrom, electronicDocumentTo, optElectronicDocumentTo, docByException, unitType, pharmacyType, hospitalType, optHospitalSizeFrom, hospitalSizeFrom, optHospitalSizeTo, hospitalSizeTo, countryId, stateId, value, others, location);

                //Here hospitalUnitId is null because the chosen hospital unit is compared with global data or all hospital unit data (or National Database)
                List<RMC.BusinessEntities.BEFunctionNames> objectGenericBEFunctionNames = null;
                //if (hospitalUnitID == null)
                //{
                objectGenericBEFunctionNames = CalculateFunctionValuesTest(activitiesID, valueAddedCategoryID, OthersCategoryID, LocationCategoryID, null, firstYear, lastYear, firstMonth, lastMonth, bedInUnitFrom, optBedInUnitFrom, bedInUnitTo, optBedInUnitTo, budgetedPatientFrom, optBudgetedPatientFrom, budgetedPatientTo, optBudgetedPatientTo, startDate, endDate, electronicDocumentFrom, optElectronicDocumentFrom, electronicDocumentTo, optElectronicDocumentTo, docByException, unitType, pharmacyType, hospitalType, optHospitalSizeFrom, hospitalSizeFrom, optHospitalSizeTo, hospitalSizeTo, countryId, stateId, activities, value, others, location, dataPointsFrom, optDataPointsFrom, dataPointsTo, optdataPointsTo, configName, unitIds);

                if (reportName == "MonthlySummaryDashboard")
                {
                    MaintainSessions.SessionFunctionValues = objectGenericBEFunctionNames;
                }
                //}
                //else
                //{
                //    objectGenericBEFunctionNames = CalculateFunctionValuesTest(valueAddedCategoryID, OthersCategoryID, LocationCategoryID, hospitalUnitID, firstYear, lastYear, firstMonth, lastMonth, bedInUnitFrom, optBedInUnitFrom, bedInUnitTo, optBedInUnitTo, budgetedPatientFrom, optBudgetedPatientFrom, budgetedPatientTo, optBudgetedPatientTo, startDate, endDate, electronicDocumentFrom, optElectronicDocumentFrom, electronicDocumentTo, optElectronicDocumentTo, docByException, unitType, pharmacyType, hospitalType, optHospitalSizeFrom, hospitalSizeFrom, optHospitalSizeTo, hospitalSizeTo, countryId, stateId, value, others, location, dataPointsFrom, optDataPointsFrom, dataPointsTo, optdataPointsTo, configName);
                //}

                //List<RMC.BusinessEntities.BEReports> objectGenericBEReports = GetDataForTimeRNSummaryTest(valueAddedCategoryID, OthersCategoryID, LocationCategoryID, hospitalUnitID, firstYear, lastYear, firstMonth, lastMonth, bedInUnitFrom, optBedInUnitFrom, bedInUnitTo, optBedInUnitTo, budgetedPatientFrom, optBudgetedPatientFrom, budgetedPatientTo, optBudgetedPatientTo, startDate, endDate, electronicDocumentFrom, optElectronicDocumentFrom, electronicDocumentTo, optElectronicDocumentTo, docByException, unitType, pharmacyType, hospitalType, optHospitalSizeFrom, hospitalSizeFrom, optHospitalSizeTo, hospitalSizeTo, countryId, stateId, value, others, location, dataPointsFrom, optDataPointsFrom, dataPointsTo, optdataPointsTo, configName);
                List<RMC.BusinessEntities.BEReports> objectGenericBEReports = GetDataForTimeRNSummaryTest(activitiesID, valueAddedCategoryID, OthersCategoryID, LocationCategoryID, hospitalUnitID, firstYear, lastYear, firstMonth, lastMonth, null, 0, null, 0, null, 0, null, 0, null, null, null, 0, null, 0, 0, null, null, null, 0, null, 0, null, null, null, activities, value, others, location, null, 0, null, 0, null);

                return CalculatePerformanceTest(objectGenericBEFunctionNames, objectGenericBEReports);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Use to generate graph values for monthlySummary-Dashboard Report
        /// </summary>
        /// <param name="objectNewGenericBENationalDatabase"></param>
        /// <param name="objectNewGenericBEReports"></param>
        /// <returns>objectPerformaceBENationalDatabase</returns>
        private List<RMC.BusinessEntities.BEFunctionNames> CalculatePerformanceTest(List<RMC.BusinessEntities.BEFunctionNames> objectNewGenericBEFunctionNames, List<RMC.BusinessEntities.BEReports> objectNewGenericBEReports)
        {
            try
            {
                List<RMC.BusinessEntities.BEFunctionNames> objectGenericBEFunctionNames = null;
                List<RMC.BusinessEntities.BEReports> objectGenericSingleBEReports = null;
                List<RMC.BusinessEntities.BEFunctionNames> objectPerformaceBEFunctionNames = new List<RMC.BusinessEntities.BEFunctionNames>();

                objectGenericBEFunctionNames = objectNewGenericBEFunctionNames.FindAll(delegate(RMC.BusinessEntities.BEFunctionNames objectNewBEFunctionNames)
                {
                    return objectNewBEFunctionNames.FunctionName.ToLower() == "median";
                });

                objectGenericSingleBEReports = objectNewGenericBEReports.FindAll(delegate(RMC.BusinessEntities.BEReports objectNewBEReports)
                {
                    if (objectNewBEReports.MonthName == null)
                    {
                        return false;
                    }
                    else 
                    {
                        return objectNewBEReports.MonthName.ToLower() == "hosp avg";
                    }
                    
                });

                objectGenericBEFunctionNames.ForEach(delegate(RMC.BusinessEntities.BEFunctionNames objectNewBEFunctionNames)
                {
                    RMC.BusinessEntities.BEReports objectSingleBEReports = null;
                    objectSingleBEReports = objectGenericSingleBEReports.Find(delegate(RMC.BusinessEntities.BEReports objectNewBEReports)
                    {
                        string newReportColumnName = removeIncDecSymbol<BusinessEntities.BEReports>("ColumnName", objectNewBEReports);
                        
                        return newReportColumnName == objectNewBEFunctionNames.ColumnName;
                    });

                    if (objectSingleBEReports != null)
                    {
                        RMC.BusinessEntities.BEFunctionNames objectPerformance = new RMC.BusinessEntities.BEFunctionNames();

                        objectPerformance.FunctionName = "Performance";
                        objectPerformance.ColumnName = objectSingleBEReports.ColumnName;
                        int indexSymbolNegation = 0;
                        int indexBrace = objectSingleBEReports.ColumnName.IndexOf('(');
                        if (indexBrace > 0)
                        {
                            indexSymbolNegation = objectSingleBEReports.ColumnName.Substring(indexBrace, 3).IndexOf('-');
                        }
                        if (indexSymbolNegation > 0)
                        {
                            objectPerformance.FunctionNameDouble = objectNewBEFunctionNames.FunctionNameDouble - objectSingleBEReports.ValuesSum;
                        }
                        else
                        {
                            objectPerformance.FunctionNameDouble = objectSingleBEReports.ValuesSum - objectNewBEFunctionNames.FunctionNameDouble;
                        }
                        objectPerformance.FunctionValueText = string.Format("{0:#.##}%", objectPerformance.FunctionNameDouble);

                        objectPerformaceBEFunctionNames.Add(objectPerformance);
                    }
                });

                return objectPerformaceBEFunctionNames;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Gets data from NationalDatabase table from database for multiple profiles
        /// </summary>
        public List<RMC.BusinessEntities.BENationalDatabase> GetNationalDatabaseForMultipleProfile(string valueAddedCategoryID, string OthersCategoryID, string LocationCategoryID, int? hospitalUnitID, int? firstYear, int? lastYear, int? firstMonth, int? lastMonth, int? bedInUnitFrom, int optBedInUnitFrom, int? bedInUnitTo, int optBedInUnitTo, float? budgetedPatientFrom, int optBudgetedPatientFrom, float? budgetedPatientTo, int optBudgetedPatientTo, string startDate, string endDate, int? electronicDocumentFrom, int optElectronicDocumentFrom, int? electronicDocumentTo, int optElectronicDocumentTo, int docByException, string unitType, string pharmacyType, string hospitalType, int optHospitalSizeFrom, int? hospitalSizeFrom, int optHospitalSizeTo, int? hospitalSizeTo, int? countryId, int? stateId, string value, string others, string location)
        {
            try
            {
                List<RMC.BusinessEntities.BENationalDatabase> objectGenericBENationalDatabaseTemp = null;
                List<RMC.BusinessEntities.BENationalDatabase> objectGenericBENationalDatabase = null;

                //valueAddedCategoryID
                if (valueAddedCategoryID != null)
                {
                    if (valueAddedCategoryID.Contains(","))
                    {
                        string[] strArrvalue = null;
                        strArrvalue = value.Split(new char[] { ',' });
                        string[] strArrvalueAddedCategoryID = null;
                        strArrvalueAddedCategoryID = valueAddedCategoryID.Split(new char[] { ',' });
                        int[] intArrvalueAddedCategoryID = new int[strArrvalueAddedCategoryID.Length];
                        for (int i = 0; i < strArrvalueAddedCategoryID.Length; i++)
                        {
                            intArrvalueAddedCategoryID[i] = int.Parse(strArrvalueAddedCategoryID[i]);
                            //objectGenericListBEReportValueAddedProfile = CalculationForTimeRNSummaryTest(strArrvalue[i], "value added", GetCategoryProfileDataByProfileID(intArrvalueAddedCategoryID[i]), SearchHospitalsDataForBenchmarkTest(hospitalUnitID, firstYear, lastYear, firstMonth, lastMonth, bedInUnitFrom, optBedInUnitFrom, bedInUnitTo, optBedInUnitTo, budgetedPatientFrom, optBudgetedPatientFrom, budgetedPatientTo, optBudgetedPatientTo, startDate, endDate, electronicDocumentFrom, optElectronicDocumentFrom, electronicDocumentTo, optElectronicDocumentTo, docByException, unitType, pharmacyType, hospitalType, optHospitalSizeFrom, hospitalSizeFrom, optHospitalSizeTo, hospitalSizeTo, countryId, stateId));
                            IQueryable<RMC.BusinessEntities.BENationalDatabase> queryable = (from nd in _objectRMCDataContext.NationalDatabases
                                                                                             orderby nd.TypeValueID
                                                                                             where nd.Type.ToLower().Trim() == "value added"
                                                                                             select new RMC.BusinessEntities.BENationalDatabase
                                                                                             {
                                                                                                 FunctionType = (from nc in _objectRMCDataContext.NationalDatabaseCategories
                                                                                                                 where nc.NationalDatabaseCategoryID == nd.NationalDatabaseCategoryID
                                                                                                                 select nc.NationalDatabaseCategoryName).FirstOrDefault(),
                                                                                                 ProfileType = (from pt in _objectRMCDataContext.ValueAddedTypes
                                                                                                                where pt.TypeID == nd.TypeValueID
                                                                                                                select pt).FirstOrDefault().TypeName + " (" + strArrvalue[i] + ")",
                                                                                                 //ProfileType = (nd.Type.ToLower() == "value added") ? (from pt in _objectRMCDataContext.ValueAddedTypes
                                                                                                 //                                                      where pt.TypeID == nd.TypeValueID
                                                                                                 //                                                      select pt).FirstOrDefault().TypeName :
                                                                                                 //               (nd.Type.ToLower() == "others") ? (from pt in _objectRMCDataContext.CategoryGroups
                                                                                                 //                                                  where pt.CategoryGroupID == nd.TypeValueID
                                                                                                 //                                                  select pt).FirstOrDefault().CategoryGroup1 : (from pt in _objectRMCDataContext.LocationCategories
                                                                                                 //                                                                                                where pt.LocationID == nd.TypeValueID
                                                                                                 //                                                                                                select pt).FirstOrDefault().LocationCategory1,
                                                                                                 GroupSequence = (nd.Type.ToLower() == "value added") ? 1 : (nd.Type.ToLower() == "others") ? 2 : 3,
                                                                                                 ValueText = string.Format("{0:#.##}%", nd.Value.Value),
                                                                                                 Value = nd.Value.Value
                                                                                             }).AsQueryable();


                            objectGenericBENationalDatabaseTemp = queryable.ToList();
                            if (i == 0)
                            {
                                objectGenericBENationalDatabase = objectGenericBENationalDatabaseTemp;
                                //objectGenericListBEReportValueAdded = objectGenericListBEReportValueAddedProfile;
                            }
                            if (i != 0)
                            {
                                objectGenericBENationalDatabase.AddRange(objectGenericBENationalDatabaseTemp);
                            }
                        }
                    }
                    else
                    {
                        IQueryable<RMC.BusinessEntities.BENationalDatabase> queryable = (from nd in _objectRMCDataContext.NationalDatabases
                                                                                         orderby nd.TypeValueID
                                                                                         where nd.Type.ToLower().Trim() == "value added"
                                                                                         select new RMC.BusinessEntities.BENationalDatabase
                                                                                         {
                                                                                             FunctionType = (from nc in _objectRMCDataContext.NationalDatabaseCategories
                                                                                                             where nc.NationalDatabaseCategoryID == nd.NationalDatabaseCategoryID
                                                                                                             select nc.NationalDatabaseCategoryName).FirstOrDefault(),
                                                                                             ProfileType = (from pt in _objectRMCDataContext.ValueAddedTypes
                                                                                                            where pt.TypeID == nd.TypeValueID
                                                                                                            select pt).FirstOrDefault().TypeName + " (" + value + ")",
                                                                                             //ProfileType = (nd.Type.ToLower() == "value added") ? (from pt in _objectRMCDataContext.ValueAddedTypes
                                                                                             //                                                      where pt.TypeID == nd.TypeValueID
                                                                                             //                                                      select pt).FirstOrDefault().TypeName :
                                                                                             //               (nd.Type.ToLower() == "others") ? (from pt in _objectRMCDataContext.CategoryGroups
                                                                                             //                                                  where pt.CategoryGroupID == nd.TypeValueID
                                                                                             //                                                  select pt).FirstOrDefault().CategoryGroup1 : (from pt in _objectRMCDataContext.LocationCategories
                                                                                             //                                                                                                where pt.LocationID == nd.TypeValueID
                                                                                             //                                                                                                select pt).FirstOrDefault().LocationCategory1,
                                                                                             GroupSequence = (nd.Type.ToLower() == "value added") ? 1 : (nd.Type.ToLower() == "others") ? 2 : 3,
                                                                                             ValueText = string.Format("{0:#.##}%", nd.Value.Value),
                                                                                             Value = nd.Value.Value
                                                                                         }).AsQueryable();


                        objectGenericBENationalDatabase = queryable.ToList();
                    }
                }
                //OthersCategoryID
                if (OthersCategoryID != null)
                {
                    if (OthersCategoryID.Contains(","))
                    {
                        string[] strArrothers = null;
                        strArrothers = others.Split(new char[] { ',' });
                        string[] strArrOthersCategoryID = null;
                        strArrOthersCategoryID = OthersCategoryID.Split(new char[] { ',' });
                        int[] intArrOthersCategoryID = new int[strArrOthersCategoryID.Length];
                        for (int i = 0; i < strArrOthersCategoryID.Length; i++)
                        {
                            intArrOthersCategoryID[i] = int.Parse(strArrOthersCategoryID[i]);
                            //objectGenericListBEReportOthers = CalculationForTimeRNSummaryTest(strArrothers[i], "others", GetCategoryProfileDataByProfileID(intArrOthersCategoryID[i]), SearchHospitalsDataForBenchmarkTest(hospitalUnitID, firstYear, lastYear, firstMonth, lastMonth, bedInUnitFrom, optBedInUnitFrom, bedInUnitTo, optBedInUnitTo, budgetedPatientFrom, optBudgetedPatientFrom, budgetedPatientTo, optBudgetedPatientTo, startDate, endDate, electronicDocumentFrom, optElectronicDocumentFrom, electronicDocumentTo, optElectronicDocumentTo, docByException, unitType, pharmacyType, hospitalType, optHospitalSizeFrom, hospitalSizeFrom, optHospitalSizeTo, hospitalSizeTo, countryId, stateId));
                            IQueryable<RMC.BusinessEntities.BENationalDatabase> queryable = (from nd in _objectRMCDataContext.NationalDatabases
                                                                                             orderby nd.TypeValueID
                                                                                             where nd.Type.ToLower().Trim() == "others"
                                                                                             select new RMC.BusinessEntities.BENationalDatabase
                                                                                             {
                                                                                                 FunctionType = (from nc in _objectRMCDataContext.NationalDatabaseCategories
                                                                                                                 where nc.NationalDatabaseCategoryID == nd.NationalDatabaseCategoryID
                                                                                                                 select nc.NationalDatabaseCategoryName).FirstOrDefault(),
                                                                                                 ProfileType = (from pt in _objectRMCDataContext.CategoryGroups
                                                                                                                where pt.CategoryGroupID == nd.TypeValueID
                                                                                                                select pt).FirstOrDefault().CategoryGroup1 + " (" + strArrothers[i] + ")",
                                                                                                 //ProfileType = (nd.Type.ToLower() == "value added") ? (from pt in _objectRMCDataContext.ValueAddedTypes
                                                                                                 //                                                      where pt.TypeID == nd.TypeValueID
                                                                                                 //                                                      select pt).FirstOrDefault().TypeName :
                                                                                                 //               (nd.Type.ToLower() == "others") ? (from pt in _objectRMCDataContext.CategoryGroups
                                                                                                 //                                                  where pt.CategoryGroupID == nd.TypeValueID
                                                                                                 //                                                  select pt).FirstOrDefault().CategoryGroup1 : (from pt in _objectRMCDataContext.LocationCategories
                                                                                                 //                                                                                                where pt.LocationID == nd.TypeValueID
                                                                                                 //                                                                                                select pt).FirstOrDefault().LocationCategory1,
                                                                                                 GroupSequence = (nd.Type.ToLower() == "value added") ? 1 : (nd.Type.ToLower() == "others") ? 2 : 3,
                                                                                                 ValueText = string.Format("{0:#.##}%", nd.Value.Value),
                                                                                                 Value = nd.Value.Value
                                                                                             }).AsQueryable();


                            objectGenericBENationalDatabaseTemp = queryable.ToList();

                            objectGenericBENationalDatabase.AddRange(objectGenericBENationalDatabaseTemp);
                        }
                    }
                    else
                    {
                        IQueryable<RMC.BusinessEntities.BENationalDatabase> queryable = (from nd in _objectRMCDataContext.NationalDatabases
                                                                                         orderby nd.TypeValueID
                                                                                         where nd.Type.ToLower().Trim() == "others"
                                                                                         select new RMC.BusinessEntities.BENationalDatabase
                                                                                         {
                                                                                             FunctionType = (from nc in _objectRMCDataContext.NationalDatabaseCategories
                                                                                                             where nc.NationalDatabaseCategoryID == nd.NationalDatabaseCategoryID
                                                                                                             select nc.NationalDatabaseCategoryName).FirstOrDefault(),
                                                                                             ProfileType = (from pt in _objectRMCDataContext.CategoryGroups
                                                                                                            where pt.CategoryGroupID == nd.TypeValueID
                                                                                                            select pt).FirstOrDefault().CategoryGroup1 + " (" + others + ")",
                                                                                             //ProfileType = (nd.Type.ToLower() == "value added") ? (from pt in _objectRMCDataContext.ValueAddedTypes
                                                                                             //                                                      where pt.TypeID == nd.TypeValueID
                                                                                             //                                                      select pt).FirstOrDefault().TypeName :
                                                                                             //               (nd.Type.ToLower() == "others") ? (from pt in _objectRMCDataContext.CategoryGroups
                                                                                             //                                                  where pt.CategoryGroupID == nd.TypeValueID
                                                                                             //                                                  select pt).FirstOrDefault().CategoryGroup1 : (from pt in _objectRMCDataContext.LocationCategories
                                                                                             //                                                                                                where pt.LocationID == nd.TypeValueID
                                                                                             //                                                                                                select pt).FirstOrDefault().LocationCategory1,
                                                                                             GroupSequence = (nd.Type.ToLower() == "value added") ? 1 : (nd.Type.ToLower() == "others") ? 2 : 3,
                                                                                             ValueText = string.Format("{0:#.##}%", nd.Value.Value),
                                                                                             Value = nd.Value.Value
                                                                                         }).AsQueryable();


                        objectGenericBENationalDatabaseTemp = queryable.ToList();

                        objectGenericBENationalDatabase.AddRange(objectGenericBENationalDatabaseTemp);
                    }
                }
                //LocationCategoryID;
                if (LocationCategoryID != null)
                {
                    if (LocationCategoryID.Contains(","))
                    {
                        string[] strArrLocation = null;
                        strArrLocation = location.Split(new char[] { ',' });
                        string[] strArrLocationCategoryID = null;
                        strArrLocationCategoryID = LocationCategoryID.Split(new char[] { ',' });
                        int[] intArrLocationCategoryID = new int[strArrLocationCategoryID.Length];
                        for (int i = 0; i < strArrLocationCategoryID.Length; i++)
                        {
                            intArrLocationCategoryID[i] = int.Parse(strArrLocationCategoryID[i]);
                            //objectGenericListBEReportLocation = CalculationForTimeRNSummaryTest(strArrLocation[i], "location", GetCategoryProfileDataByProfileID(intArrLocationCategoryID[i]), SearchHospitalsDataForBenchmarkTest(hospitalUnitID, firstYear, lastYear, firstMonth, lastMonth, bedInUnitFrom, optBedInUnitFrom, bedInUnitTo, optBedInUnitTo, budgetedPatientFrom, optBudgetedPatientFrom, budgetedPatientTo, optBudgetedPatientTo, startDate, endDate, electronicDocumentFrom, optElectronicDocumentFrom, electronicDocumentTo, optElectronicDocumentTo, docByException, unitType, pharmacyType, hospitalType, optHospitalSizeFrom, hospitalSizeFrom, optHospitalSizeTo, hospitalSizeTo, countryId, stateId));
                            IQueryable<RMC.BusinessEntities.BENationalDatabase> queryable = (from nd in _objectRMCDataContext.NationalDatabases
                                                                                             orderby nd.TypeValueID
                                                                                             where nd.Type.ToLower().Trim() == "location"
                                                                                             select new RMC.BusinessEntities.BENationalDatabase
                                                                                             {
                                                                                                 FunctionType = (from nc in _objectRMCDataContext.NationalDatabaseCategories
                                                                                                                 where nc.NationalDatabaseCategoryID == nd.NationalDatabaseCategoryID
                                                                                                                 select nc.NationalDatabaseCategoryName).FirstOrDefault(),
                                                                                                 ProfileType = (from pt in _objectRMCDataContext.LocationCategories
                                                                                                                where pt.LocationID == nd.TypeValueID
                                                                                                                select pt).FirstOrDefault().LocationCategory1 + " (" + strArrLocation[i] + ")",
                                                                                                 //ProfileType = (nd.Type.ToLower() == "value added") ? (from pt in _objectRMCDataContext.ValueAddedTypes
                                                                                                 //                                                      where pt.TypeID == nd.TypeValueID
                                                                                                 //                                                      select pt).FirstOrDefault().TypeName :
                                                                                                 //               (nd.Type.ToLower() == "others") ? (from pt in _objectRMCDataContext.CategoryGroups
                                                                                                 //                                                  where pt.CategoryGroupID == nd.TypeValueID
                                                                                                 //                                                  select pt).FirstOrDefault().CategoryGroup1 : (from pt in _objectRMCDataContext.LocationCategories
                                                                                                 //                                                                                                where pt.LocationID == nd.TypeValueID
                                                                                                 //                                                                                                select pt).FirstOrDefault().LocationCategory1,
                                                                                                 GroupSequence = (nd.Type.ToLower() == "value added") ? 1 : (nd.Type.ToLower() == "others") ? 2 : 3,
                                                                                                 ValueText = string.Format("{0:#.##}%", nd.Value.Value),
                                                                                                 Value = nd.Value.Value
                                                                                             }).AsQueryable();


                            objectGenericBENationalDatabaseTemp = queryable.ToList();

                            objectGenericBENationalDatabase.AddRange(objectGenericBENationalDatabaseTemp);

                        }
                    }
                    else
                    {
                        IQueryable<RMC.BusinessEntities.BENationalDatabase> queryable = (from nd in _objectRMCDataContext.NationalDatabases
                                                                                         orderby nd.TypeValueID
                                                                                         where nd.Type.ToLower().Trim() == "location"
                                                                                         select new RMC.BusinessEntities.BENationalDatabase
                                                                                         {
                                                                                             FunctionType = (from nc in _objectRMCDataContext.NationalDatabaseCategories
                                                                                                             where nc.NationalDatabaseCategoryID == nd.NationalDatabaseCategoryID
                                                                                                             select nc.NationalDatabaseCategoryName).FirstOrDefault(),
                                                                                             ProfileType = (from pt in _objectRMCDataContext.LocationCategories
                                                                                                            where pt.LocationID == nd.TypeValueID
                                                                                                            select pt).FirstOrDefault().LocationCategory1 + " (" + location + ")",
                                                                                             //ProfileType = (nd.Type.ToLower() == "value added") ? (from pt in _objectRMCDataContext.ValueAddedTypes
                                                                                             //                                                      where pt.TypeID == nd.TypeValueID
                                                                                             //                                                      select pt).FirstOrDefault().TypeName :
                                                                                             //               (nd.Type.ToLower() == "others") ? (from pt in _objectRMCDataContext.CategoryGroups
                                                                                             //                                                  where pt.CategoryGroupID == nd.TypeValueID
                                                                                             //                                                  select pt).FirstOrDefault().CategoryGroup1 : (from pt in _objectRMCDataContext.LocationCategories
                                                                                             //                                                                                                where pt.LocationID == nd.TypeValueID
                                                                                             //                                                                                                select pt).FirstOrDefault().LocationCategory1,
                                                                                             GroupSequence = (nd.Type.ToLower() == "value added") ? 1 : (nd.Type.ToLower() == "others") ? 2 : 3,
                                                                                             ValueText = string.Format("{0:#.##}%", nd.Value.Value),
                                                                                             Value = nd.Value.Value
                                                                                         }).AsQueryable();


                        objectGenericBENationalDatabaseTemp = queryable.ToList();

                        objectGenericBENationalDatabase.AddRange(objectGenericBENationalDatabaseTemp);
                    }
                }

                return objectGenericBENationalDatabase.OrderBy(o => o.GroupSequence).ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Gets subCategoryProfiles from database according to the profileCategory given
        /// </summary>
        public List<RMC.BusinessEntities.BEProfileCategory> GetSubCategoryProfileModified(string profileCategory, string valueAddedCategoryID, string OthersCategoryID, string LocationCategoryID, string value, string others, string location)
        {
            RMC.DataService.RMCDataContext objectRMCDataContext = new RMC.DataService.RMCDataContext();
            List<RMC.BusinessEntities.BEProfileCategory> objectSubCategoryProfile = new List<RMC.BusinessEntities.BEProfileCategory>();
            List<RMC.BusinessEntities.BEProfileCategory> objectSubCategoryProfileTemp = new List<RMC.BusinessEntities.BEProfileCategory>();

            //valueAddedCategoryID
            if (profileCategory.ToLower().Trim() == "value added")
            {
                if (valueAddedCategoryID != null)
                {
                    if (valueAddedCategoryID.Contains(","))
                    {
                        string[] strArrvalue = null;
                        strArrvalue = value.Split(new char[] { ',' });
                        string[] strArrvalueAddedCategoryID = null;
                        strArrvalueAddedCategoryID = valueAddedCategoryID.Split(new char[] { ',' });
                        int[] intArrvalueAddedCategoryID = new int[strArrvalueAddedCategoryID.Length];
                        for (int i = 0; i < strArrvalueAddedCategoryID.Length; i++)
                        {
                            intArrvalueAddedCategoryID[i] = int.Parse(strArrvalueAddedCategoryID[i]);
                            //objectGenericListBEReportValueAddedProfile = CalculationForTimeRNSummaryTest(strArrvalue[i], "value added", GetCategoryProfileDataByProfileID(intArrvalueAddedCategoryID[i]), SearchHospitalsDataForBenchmarkTest(hospitalUnitID, firstYear, lastYear, firstMonth, lastMonth, bedInUnitFrom, optBedInUnitFrom, bedInUnitTo, optBedInUnitTo, budgetedPatientFrom, optBudgetedPatientFrom, budgetedPatientTo, optBudgetedPatientTo, startDate, endDate, electronicDocumentFrom, optElectronicDocumentFrom, electronicDocumentTo, optElectronicDocumentTo, docByException, unitType, pharmacyType, hospitalType, optHospitalSizeFrom, hospitalSizeFrom, optHospitalSizeTo, hospitalSizeTo, countryId, stateId));

                            objectSubCategoryProfileTemp = (from vat in objectRMCDataContext.ValueAddedTypes
                                                            select new RMC.BusinessEntities.BEProfileCategory
                                                            {
                                                                ProfileCategoryName = vat.TypeName + " (" + strArrvalue[i] + ")"
                                                            }).ToList();


                            if (i == 0)
                            {
                                objectSubCategoryProfile = objectSubCategoryProfileTemp;
                            }
                            if (i != 0)
                            {
                                objectSubCategoryProfile.AddRange(objectSubCategoryProfileTemp);
                            }
                        }
                    }
                    else
                    {
                        objectSubCategoryProfile = (from vat in objectRMCDataContext.ValueAddedTypes
                                                    select new RMC.BusinessEntities.BEProfileCategory
                                                    {
                                                        ProfileCategoryName = vat.TypeName + " (" + value + ")"
                                                    }).ToList();
                    }
                }
            }

            //OthersCategoryID
            if (profileCategory.ToLower().Trim() == "others")
            {
                if (OthersCategoryID != null)
                {
                    if (OthersCategoryID.Contains(","))
                    {
                        string[] strArrothers = null;
                        strArrothers = others.Split(new char[] { ',' });
                        string[] strArrOthersCategoryID = null;
                        strArrOthersCategoryID = OthersCategoryID.Split(new char[] { ',' });
                        int[] intArrOthersCategoryID = new int[strArrOthersCategoryID.Length];
                        for (int i = 0; i < strArrOthersCategoryID.Length; i++)
                        {
                            intArrOthersCategoryID[i] = int.Parse(strArrOthersCategoryID[i]);
                            objectSubCategoryProfile = (from cg in objectRMCDataContext.CategoryGroups
                                                        select new RMC.BusinessEntities.BEProfileCategory
                                                        {
                                                            ProfileCategoryName = cg.CategoryGroup1 + " (" + strArrothers[i] + ")"
                                                        }).ToList();
                            objectSubCategoryProfile.AddRange(objectSubCategoryProfile);
                        }
                    }
                    else
                    {
                        objectSubCategoryProfile = (from cg in objectRMCDataContext.CategoryGroups
                                                    select new RMC.BusinessEntities.BEProfileCategory
                                                    {
                                                        ProfileCategoryName = cg.CategoryGroup1 + " (" + others + ")"
                                                    }).ToList();
                        objectSubCategoryProfile.AddRange(objectSubCategoryProfile);
                    }
                }
            }
            //LocationCategoryID;
            if (profileCategory.ToLower().Trim() == "location")
            {
                if (LocationCategoryID != null)
                {
                    if (LocationCategoryID.Contains(","))
                    {
                        string[] strArrLocation = null;
                        strArrLocation = location.Split(new char[] { ',' });
                        string[] strArrLocationCategoryID = null;
                        strArrLocationCategoryID = LocationCategoryID.Split(new char[] { ',' });
                        int[] intArrLocationCategoryID = new int[strArrLocationCategoryID.Length];
                        for (int i = 0; i < strArrLocationCategoryID.Length; i++)
                        {
                            intArrLocationCategoryID[i] = int.Parse(strArrLocationCategoryID[i]);
                            objectSubCategoryProfile = (from lc in objectRMCDataContext.LocationCategories
                                                        select new RMC.BusinessEntities.BEProfileCategory
                                                        {
                                                            ProfileCategoryName = lc.LocationCategory1 + " (" + strArrLocation[i] + ")"
                                                        }).ToList();

                            objectSubCategoryProfile.AddRange(objectSubCategoryProfile);
                        }
                    }
                    else
                    {
                        objectSubCategoryProfile = (from lc in objectRMCDataContext.LocationCategories
                                                    select new RMC.BusinessEntities.BEProfileCategory
                                                    {
                                                        ProfileCategoryName = lc.LocationCategory1 + " (" + location + ")"
                                                    }).ToList();

                        objectSubCategoryProfile.AddRange(objectSubCategoryProfile);
                    }
                }
            }
            //if (profileCategory.ToLower().Trim() == "value added")
            //{
            //    objectSubCategoryProfile = (from vat in objectRMCDataContext.ValueAddedTypes
            //                                select new RMC.BusinessEntities.BEProfileCategory
            //                                {
            //                                    ProfileCategoryName = vat.TypeName
            //                                }).ToList();
            //}
            //if (profileCategory.ToLower().Trim() == "others")
            //{
            //    objectSubCategoryProfile = (from cg in objectRMCDataContext.CategoryGroups
            //                                select new RMC.BusinessEntities.BEProfileCategory
            //                                {
            //                                    ProfileCategoryName = cg.CategoryGroup1
            //                                }).ToList();
            //}
            //if (profileCategory.ToLower().Trim() == "location")
            //{
            //    objectSubCategoryProfile = (from lc in objectRMCDataContext.LocationCategories
            //                                select new RMC.BusinessEntities.BEProfileCategory
            //                                {
            //                                    ProfileCategoryName = lc.LocationCategory1
            //                                }).ToList();
            //}

            return objectSubCategoryProfile;
        }

        /// <summary>
        /// Gets subCategoryProfiles from database according to the profileCategory given
        /// </summary>
        public List<RMC.BusinessEntities.BEProfileCategory> GetSubCategoryProfileByProfileId(string profileId)
        {
            RMC.DataService.RMCDataContext objectRMCDataContext = new RMC.DataService.RMCDataContext();
            List<RMC.BusinessEntities.BEProfileCategory> objectSubCategoryProfile = new List<RMC.BusinessEntities.BEProfileCategory>();
            List<RMC.BusinessEntities.BEProfileCategory> objectSubCategoryProfileTemp = new List<RMC.BusinessEntities.BEProfileCategory>();

            if (profileId != "Database Values" && profileId != "Special Category")
            {
                if (profileId.Contains(","))
                {
                    string[] profileIdArr = profileId.Split(new char[] { ',' });
                    profileId = profileIdArr[0].ToString();
                }

                objectSubCategoryProfile = null;
                if (profileId != "0")
                {
                    List<RMC.BusinessEntities.BEProfileCategory> objectProfileType = (from pt in _objectRMCDataContext.ProfileTypes
                                                                                      where pt.ProfileTypeID == Convert.ToInt32(profileId)
                                                                                      select new RMC.BusinessEntities.BEProfileCategory
                                                                                      {
                                                                                          ProfileType = pt.Type
                                                                                      }).ToList();


                    //valueAddedCategoryID
                    if (objectProfileType[0].ProfileType.ToLower().Trim() == "value added")
                    {

                        objectSubCategoryProfile = (from cp in objectRMCDataContext.CategoryProfiles
                                                    join vat in objectRMCDataContext.ValueAddedTypes on cp.CategoryAssignmentID equals vat.TypeID
                                                    where cp.ProfileTypeID == Convert.ToInt32(profileId)
                                                    select new RMC.BusinessEntities.BEProfileCategory
                                                    {
                                                        ProfileCategoryName = vat.TypeName
                                                    }).Distinct().ToList();
                    }

                    //OthersCategoryID
                    if (objectProfileType[0].ProfileType.ToLower().Trim() == "others")
                    {


                        objectSubCategoryProfile = (from cp in objectRMCDataContext.CategoryProfiles
                                                    join cg in objectRMCDataContext.CategoryGroups on cp.CategoryAssignmentID equals cg.CategoryGroupID
                                                    where cp.ProfileTypeID == Convert.ToInt32(profileId)
                                                    select new RMC.BusinessEntities.BEProfileCategory
                                                    {
                                                        ProfileCategoryName = cg.CategoryGroup1
                                                    }).Distinct().ToList();

                    }
                    //LocationCategoryID;
                    if (objectProfileType[0].ProfileType.ToLower().Trim() == "location")
                    {

                        objectSubCategoryProfile = (from cp in objectRMCDataContext.CategoryProfiles
                                                    join lc in objectRMCDataContext.LocationCategories on cp.CategoryAssignmentID equals lc.LocationID
                                                    where cp.ProfileTypeID == Convert.ToInt32(profileId)
                                                    select new RMC.BusinessEntities.BEProfileCategory
                                                    {
                                                        ProfileCategoryName = lc.LocationCategory1
                                                    }).Distinct().ToList();
                    }
                    //ActivitiesID;
                    if (objectProfileType[0].ProfileType.ToLower().Trim() == "activities")
                    {

                        objectSubCategoryProfile = (from cp in objectRMCDataContext.CategoryProfiles
                                                    join ac in objectRMCDataContext.ActivitiesCategories on cp.CategoryAssignmentID equals ac.ActivitiesID
                                                    where cp.ProfileTypeID == Convert.ToInt32(profileId)
                                                    select new RMC.BusinessEntities.BEProfileCategory
                                                    {
                                                        ProfileCategoryName = ac.ActivitiesCategory1
                                                    }).Distinct().ToList();
                    }
                }
            }

            if(objectSubCategoryProfile != null)
                removeIncDecSymbol<BusinessEntities.BEProfileCategory>("ProfileCategoryName", ref objectSubCategoryProfile);
            return objectSubCategoryProfile;
        }

        /// <summary>
        /// Get Data from Benchmark Filter According to the Filter Name given in Parameter
        /// </summary>
        public List<RMC.BusinessEntities.BEReportsFilter> GetFilterInformationBySearch(string search)
        {
            _objectRMCDataContext = new RMC.DataService.RMCDataContext();

            try
            {
                //List<RMC.BusinessEntities.BEUserInfomation> userInformation = null;
                List<RMC.BusinessEntities.BEReportsFilter> filterInformation = null;

                if (search == 0.ToString())
                {
                    filterInformation = (from bf in _objectRMCDataContext.BenchmarkFilters
                                         //where ui.IsDeleted == false
                                         orderby bf.FilterName
                                         select new RMC.BusinessEntities.BEReportsFilter
                                         {
                                             filterId = bf.FilterId,
                                             filterName = bf.FilterName
                                         }).ToList<RMC.BusinessEntities.BEReportsFilter>();
                }
                else
                {
                    filterInformation = (from bf in _objectRMCDataContext.BenchmarkFilters
                                         //where ui.IsDeleted == false
                                         where bf.FilterName.ToLower().Trim().Contains(search.ToLower().Trim())
                                         orderby bf.FilterName
                                         select new RMC.BusinessEntities.BEReportsFilter
                                         {
                                             filterId = bf.FilterId,
                                             filterName = bf.FilterName
                                         }).ToList<RMC.BusinessEntities.BEReportsFilter>();
                }
                return filterInformation;
            }
            catch (Exception ex)
            {
                ex.Data.Add("Function", "GetFilterInformationBySearch");
                ex.Data.Add("Class", "BEReportsFilter");
                throw ex;
            }
            finally
            {
                _objectRMCDataContext.Dispose();
            }
        }

        /// <summary>
        /// Gets the filtered data according to filterid from the benchmarkFilter table
        /// </summary>
        public RMC.DataService.BenchmarkFilter GetBenchmarkFilterData(int filterId)
        {
            _objectRMCDataContext = new RMC.DataService.RMCDataContext();
            RMC.DataService.BenchmarkFilter objectBenchmarkFilter = null;
            try
            {
                objectBenchmarkFilter = (from bf in _objectRMCDataContext.BenchmarkFilters
                                         where bf.FilterId == filterId
                                         select bf).FirstOrDefault();

                return objectBenchmarkFilter;
            }
            catch (Exception ex)
            {
                ex.Data.Add("Function", "GetBenchmarkFilterData");
                ex.Data.Add("Class", "BSReport");
                throw ex;
            }
            finally
            {
                objectBenchmarkFilter = null;
                _objectRMCDataContext.Dispose();
            }
        }

        /// <summary>
        /// Inserts Data into BenchmarkFilter table
        /// </summary>
        /// <param name="benchmarkFilter"></param>
        /// <returns></returns>
        public bool InsertBenchmarkFilter(RMC.DataService.BenchmarkFilter benchmarkFilter)
        {
            _flag = false;
            try
            {
                _objectRMCDataContext = new RMC.DataService.RMCDataContext();
                _objectRMCDataContext.BenchmarkFilters.InsertOnSubmit(benchmarkFilter);
                _objectRMCDataContext.SubmitChanges();
                _flag = true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                _objectRMCDataContext.Dispose();
            }

            return _flag;
        }

        /// <summary>
        /// Update BenchmarkFilter Data According to the Given FilterID.  
        /// </summary>
        public bool UpdateBenchmarkFilter(RMC.DataService.BenchmarkFilter benchmarkFilter)
        {
            _flag = false;
            try
            {
                _objectRMCDataContext = new RMC.DataService.RMCDataContext();
                RMC.DataService.BenchmarkFilter objectBenchmarkFilter = null;

                if (benchmarkFilter.FilterId > 0)
                {
                    objectBenchmarkFilter = (from bf in _objectRMCDataContext.BenchmarkFilters
                                             where bf.FilterId == benchmarkFilter.FilterId
                                             select bf).FirstOrDefault();

                    objectBenchmarkFilter.BedsInUnitFrom = benchmarkFilter.BedsInUnitFrom;
                    objectBenchmarkFilter.optBedsInUnitFrom = benchmarkFilter.optBedsInUnitFrom;
                    objectBenchmarkFilter.BedsInUnitTo = benchmarkFilter.BedsInUnitTo;
                    objectBenchmarkFilter.optBedsInUnitTo = benchmarkFilter.optBedsInUnitTo;
                    objectBenchmarkFilter.BudgetedPatientFrom = benchmarkFilter.BudgetedPatientFrom;
                    objectBenchmarkFilter.BudgetedPatientTo = benchmarkFilter.BudgetedPatientTo;
                    objectBenchmarkFilter.optBudgetedPatientFrom = benchmarkFilter.optBudgetedPatientFrom;
                    objectBenchmarkFilter.optBudgetedPatientTo = benchmarkFilter.optBudgetedPatientTo;
                    objectBenchmarkFilter.ElectronicDocumentationFrom = benchmarkFilter.ElectronicDocumentationFrom;
                    objectBenchmarkFilter.ElectronicDocumentationTo = benchmarkFilter.ElectronicDocumentationTo;
                    objectBenchmarkFilter.optElectronicDocumentationFrom = benchmarkFilter.optElectronicDocumentationFrom;
                    objectBenchmarkFilter.optElectronicDocumentationTo = benchmarkFilter.optElectronicDocumentationTo;
                    objectBenchmarkFilter.DocByException = benchmarkFilter.DocByException;
                    objectBenchmarkFilter.UnitType = benchmarkFilter.UnitType;
                    objectBenchmarkFilter.PharmacyType = benchmarkFilter.PharmacyType;
                    objectBenchmarkFilter.HospitalType = benchmarkFilter.HospitalType;

                    objectBenchmarkFilter.HospitalSizeFrom = benchmarkFilter.HospitalSizeFrom;
                    objectBenchmarkFilter.HospitalSizeTo = benchmarkFilter.HospitalSizeTo;
                    objectBenchmarkFilter.optHospitalSizeFrom = benchmarkFilter.optHospitalSizeFrom;
                    objectBenchmarkFilter.optHospitalSizeTo = benchmarkFilter.optHospitalSizeTo;
                    objectBenchmarkFilter.DataPointsFrom = benchmarkFilter.DataPointsFrom;
                    objectBenchmarkFilter.DataPointsTo = benchmarkFilter.DataPointsTo;
                    objectBenchmarkFilter.optDataPointsFrom = benchmarkFilter.optDataPointsFrom;
                    objectBenchmarkFilter.optDataPointsTo = benchmarkFilter.optDataPointsTo;
                    objectBenchmarkFilter.CountryId = benchmarkFilter.CountryId;
                    objectBenchmarkFilter.StateId = benchmarkFilter.StateId;
                    objectBenchmarkFilter.FilterName = benchmarkFilter.FilterName;
                    objectBenchmarkFilter.Share = benchmarkFilter.Share;
                    objectBenchmarkFilter.Comment = benchmarkFilter.Comment;
                    //objectBenchmarkFilter.CreatedBy
                    //objectBenchmarkFilter.CreatedDate
                    //objectBenchmarkFilter.ModifiedBy
                    objectBenchmarkFilter.ModifiedDate = benchmarkFilter.ModifiedDate;
                    objectBenchmarkFilter.ConfigurationName = benchmarkFilter.ConfigurationName;
                    _objectRMCDataContext.SubmitChanges();
                    _flag = true;

                }
                return _flag;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                _objectRMCDataContext.Dispose();
            }

            return _flag;
        }

        /// <summary>
        /// Delete Filter of Particular FilterId from BenchmarkFilter Table 
        /// </summary>
        public bool DeleteBenchmarkFilter(int filterID)
        {
            _flag = false;
            try
            {
                _objectRMCDataContext = new RMC.DataService.RMCDataContext();
                RMC.DataService.BenchmarkFilter objectBenchmarkFilter = null;
                if (filterID > 0)
                {
                    objectBenchmarkFilter = (from bf in _objectRMCDataContext.BenchmarkFilters
                                             where bf.FilterId == filterID
                                             select bf).FirstOrDefault();
                    _objectRMCDataContext.BenchmarkFilters.DeleteOnSubmit(objectBenchmarkFilter);
                    _objectRMCDataContext.SubmitChanges();
                    _flag = true;
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                _objectRMCDataContext.Dispose();
            }

            return _flag;
        }

        /// <summary>
        /// Gets both month and year combined
        /// </summary>
        /// <returns>returns generic list of type RMC.BusinessEntities.BEReportsYearMonth</returns>
        public List<RMC.BusinessEntities.BEReportsYearMonth> GetYearMonthCombo()
        {
            try
            {
                List<RMC.BusinessEntities.BEReportsYearMonth> objectBEReportsYearMonth = new List<RMC.BusinessEntities.BEReportsYearMonth>();

                IQueryable<RMC.BusinessEntities.BEReportsYearMonth> Queryable = (from ni in _objectRMCDataContext.NursePDAInfos
                                                                                 orderby ni.Year, ni.Month
                                                                                 select new RMC.BusinessEntities.BEReportsYearMonth
                                                                                 {
                                                                                     year = ni.Year,
                                                                                     month = ni.Month,
                                                                                     monthIndex = Convert.ToInt32(ni.Month)
                                                                                 }).Distinct();

                objectBEReportsYearMonth = Queryable.ToList();
                objectBEReportsYearMonth = objectBEReportsYearMonth.OrderBy(o => o.year).ThenBy(o => o.monthIndex).ToList();
                return objectBEReportsYearMonth;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        // add by cm. get data by hospitalunitid
        public List<RMC.BusinessEntities.BEReportsYearMonth> GetYearMonthComboByUnitId(int hospitalUnitId)
        {
            try
            {
                List<RMC.BusinessEntities.BEReportsYearMonth> objectBEReportsYearMonth = new List<RMC.BusinessEntities.BEReportsYearMonth>();

                IQueryable<RMC.BusinessEntities.BEReportsYearMonth> Queryable = (from ni in _objectRMCDataContext.NursePDAInfos
                                                                                 where ni.HospitalDemographicID == hospitalUnitId
                                                                                 orderby ni.Year, ni.Month
                                                                                 select new RMC.BusinessEntities.BEReportsYearMonth
                                                                                 {
                                                                                     year = ni.Year,
                                                                                     month = ni.Month,
                                                                                     monthIndex = Convert.ToInt32(ni.Month)
                                                                                 }).Distinct();

                objectBEReportsYearMonth = Queryable.ToList();
                objectBEReportsYearMonth = objectBEReportsYearMonth.OrderBy(o => o.year).ThenBy(o => o.monthIndex).ToList();
                return objectBEReportsYearMonth;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Gets the related year of particular hospitalUnitId
        /// </summary>
        /// <param name="hospitalUnitId"></param>
        /// <returns></returns>
        public List<RMC.BusinessEntities.BEReportsYearMonth> GetYear(int hospitalUnitId)
        {
            try
            {
                List<RMC.BusinessEntities.BEReportsYearMonth> objectBEReportsYearMonth = new List<RMC.BusinessEntities.BEReportsYearMonth>();
                objectBEReportsYearMonth = null;
                if (hospitalUnitId > 0)
                {
                    IQueryable<RMC.BusinessEntities.BEReportsYearMonth> Queryable = (from ni in _objectRMCDataContext.NursePDAInfos
                                                                                     where ni.HospitalDemographicID == hospitalUnitId
                                                                                     orderby ni.Year, ni.Month
                                                                                     select new RMC.BusinessEntities.BEReportsYearMonth
                                                                                     {
                                                                                         year = ni.Year,
                                                                                     }).Distinct();

                    objectBEReportsYearMonth = Queryable.ToList();
                    objectBEReportsYearMonth = objectBEReportsYearMonth.OrderBy(o => o.year).ToList();
                }
                return objectBEReportsYearMonth;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Gets the related year and month of particular hospitalUnitId and year
        /// </summary>
        /// <param name="hospitalUnitId"></param>
        /// <returns></returns>
        public List<RMC.BusinessEntities.BEReportsYearMonth> GetYearMonth(int hospitalUnitId, string year)
        {
            try
            {
                List<RMC.BusinessEntities.BEReportsYearMonth> objectBEReportsYearMonth = new List<RMC.BusinessEntities.BEReportsYearMonth>();
                objectBEReportsYearMonth = null;

                if (year != "0")
                {
                    IQueryable<RMC.BusinessEntities.BEReportsYearMonth> Queryable = (from ni in _objectRMCDataContext.NursePDAInfos
                                                                                     where ni.HospitalDemographicID == hospitalUnitId && ni.Year == year
                                                                                     orderby ni.Year, ni.Month
                                                                                     select new RMC.BusinessEntities.BEReportsYearMonth
                                                                                     {
                                                                                         year = ni.Year,
                                                                                         month = BSCommon.GetMonthName(ni.Month),
                                                                                         monthIndex = Convert.ToInt32(ni.Month)
                                                                                     }).Distinct();
                    objectBEReportsYearMonth = Queryable.ToList();
                    objectBEReportsYearMonth = objectBEReportsYearMonth.OrderBy(o => o.year).ThenBy(o => o.monthIndex).ToList();
                }
                return objectBEReportsYearMonth;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// To retrieve the recordCounter field value of particular hospitalId from hospitalInfo table
        /// </summary>
        /// <param name="hospitalId"></param>
        /// <returns></returns>
        public int GetRecordCounterOfHospitalID(int hospitalId)
        {
            var RecordCounter = 0;
            if (hospitalId != 0)
            {
                RecordCounter = (from hi in _objectRMCDataContext.HospitalInfos
                                 where hi.HospitalInfoID == hospitalId
                                 select hi.RecordCounter).First();
            }
            return RecordCounter;
        }

        //----------For Pie Chart
        /// <summary>
        /// It calculates data for MonthlyData-PieCharts Report
        /// </summary>
        private List<RMC.BusinessEntities.BEReports> CalculationForTimeRNSummaryPieChart(string profileName, string profileCategory, List<RMC.BusinessEntities.BECategoryProfile> objectGenericBECategoryProfile, List<RMC.BusinessEntities.BEValidation> objectGenericBEValidation)
        {
            try
            {
                int totalCount = 0;
                List<RMC.DataService.ValueAddedType> objectGenericValueAddedType = _objectRMCDataContext.ValueAddedTypes.OrderBy(o => o.TypeID).ToList();
                List<RMC.DataService.CategoryGroup> objectGenericCategoryGroup = _objectRMCDataContext.CategoryGroups.OrderBy(o => o.CategoryGroupID).ToList();
                List<RMC.DataService.LocationCategory> objectGenericLocationCategory = _objectRMCDataContext.LocationCategories.OrderBy(o => o.LocationID).ToList();
                List<RMC.DataService.ActivitiesCategory> objectGenericActivities = _objectRMCDataContext.ActivitiesCategories.OrderBy(o => o.ActivitiesID).ToList();
                List<RMC.BusinessEntities.BEReports> objectGenericBEReports = null;
                objectGenericBEValidation.ForEach(delegate(RMC.BusinessEntities.BEValidation objectBEValidation)
                {
                    RMC.BusinessEntities.BECategoryProfile objectBECategoryProfileTemp = null;

                    objectBECategoryProfileTemp = objectGenericBECategoryProfile.Find(delegate(RMC.BusinessEntities.BECategoryProfile objectBECategoryProfile)
                    {                        
                        if (objectBEValidation.SubActivityID > 0 && objectBECategoryProfile.SubActivityID > 0)
                        {
                            return objectBECategoryProfile.LocationID == objectBEValidation.LocationID && objectBECategoryProfile.ActivityID == objectBEValidation.ActivityID && objectBECategoryProfile.SubActivityID == objectBEValidation.SubActivityID;
                        }
                        else
                        {
                            return objectBECategoryProfile.LocationID == objectBEValidation.LocationID && objectBECategoryProfile.ActivityID == objectBEValidation.ActivityID;
                        }
                    });
                    if(objectBECategoryProfileTemp != null)
                        objectBEValidation.CategoryGroupID = objectBECategoryProfileTemp.CategoryAssignmentID;
                });

                totalCount = objectGenericBEValidation.Count;

                if (profileCategory == "value added")
                {
                    objectGenericBEValidation = objectGenericBEValidation.FindAll(delegate(RMC.BusinessEntities.BEValidation objectNewBEValidation)
                    {
                        return objectGenericValueAddedType.Exists(delegate(RMC.DataService.ValueAddedType objectValueAddedType)
                        {
                            return objectValueAddedType.TypeID == objectNewBEValidation.CategoryGroupID;
                        });
                    });

                    //=================Again Modified=============================
                    //changing method to include year with month also in report
                    objectGenericBEReports = (from v in objectGenericBEValidation.OrderBy(o => o.Year).ThenBy(p => p.MonthIndex)
                                              group v by new { v.Year, v.MonthIndex } into g
                                              from t in g.GroupBy(x => x.CategoryGroupID).ToList()
                                              select new RMC.BusinessEntities.BEReports
                                              {
                                                  ColumnName = objectGenericValueAddedType.Find(delegate(RMC.DataService.ValueAddedType objectValueAddedType)
                                                  {
                                                      return objectValueAddedType.TypeID == t.Key;
                                                  }).TypeName,
                                                  ColumnNumber = objectGenericValueAddedType.Find(delegate(RMC.DataService.ValueAddedType objectValueAddedType)
                                                  {
                                                      return objectValueAddedType.TypeID == t.Key;
                                                  }).TypeID,
                                                  MonthName = BSCommon.GetMonthName(t.FirstOrDefault(r => r.Month != "").Month) + " " + t.FirstOrDefault().Year,
                                                  RowName = t.FirstOrDefault(r => r.HospitalUnitName != "").HospitalUnitName,
                                                  Values = string.Format("{0:#.##}%", ((double)t.Count(r => r.CategoryGroupID == t.Key) / (double)g.Count(c => c.CategoryGroupID != 0)) * 100),
                                                  ValuesSum = Convert.ToDouble(string.Format("{0:#.##}", ((double)t.Count(r => r.CategoryGroupID == t.Key) / (double)g.Count(c => c.CategoryGroupID != 0)) * 100)),
                                                  DataPoint = (double)g.Count(c => c.CategoryGroupID != 0)
                                              }).ToList();
                }
                else if (profileCategory == "others")
                {
                    objectGenericBEValidation = objectGenericBEValidation.FindAll(delegate(RMC.BusinessEntities.BEValidation objectNewBEValidation)
                    {
                        return objectGenericCategoryGroup.Exists(delegate(RMC.DataService.CategoryGroup objectCategoryGroup)
                        {
                            return objectCategoryGroup.CategoryGroupID == objectNewBEValidation.CategoryGroupID;
                        });
                    });
                    //=================Again Modified=============================
                    //changing method to include year with month also in report
                    objectGenericBEReports = (from v in objectGenericBEValidation.OrderBy(o => o.Year).ThenBy(p => p.MonthIndex)
                                              group v by new { v.Year, v.MonthIndex } into g
                                              from t in g.GroupBy(x => x.CategoryGroupID).ToList()
                                              select new RMC.BusinessEntities.BEReports
                                              {
                                                  ColumnName = objectGenericCategoryGroup.Find(delegate(RMC.DataService.CategoryGroup objectCategoryGroup)
                                                  {
                                                      return objectCategoryGroup.CategoryGroupID == t.Key;
                                                  }).CategoryGroup1,
                                                  ColumnNumber = objectGenericCategoryGroup.Find(delegate(RMC.DataService.CategoryGroup objectCategoryGroup)
                                                  {
                                                      return objectCategoryGroup.CategoryGroupID == t.Key;
                                                  }).CategoryGroupID,
                                                  MonthName = BSCommon.GetMonthName(t.FirstOrDefault(r => r.Month != "").Month) + " " + t.FirstOrDefault().Year,
                                                  RowName = t.FirstOrDefault(r => r.HospitalUnitName != "").HospitalUnitName,
                                                  Values = string.Format("{0:#.##}%", ((double)t.Count(r => r.CategoryGroupID == t.Key) / (double)g.Count(c => c.CategoryGroupID != 0)) * 100),
                                                  ValuesSum = Convert.ToDouble(string.Format("{0:#.##}", ((double)t.Count(r => r.CategoryGroupID == t.Key) / (double)g.Count(c => c.CategoryGroupID != 0)) * 100)),
                                                  DataPoint = (double)g.Count(c => c.CategoryGroupID != 0)
                                              }).ToList();
                }
                else if (profileCategory == "activities")
                {
                    objectGenericBEValidation = objectGenericBEValidation.FindAll(delegate(RMC.BusinessEntities.BEValidation objectNewBEValidation)
                    {
                        return objectGenericActivities.Exists(delegate(RMC.DataService.ActivitiesCategory objectActivities)
                        {
                            return objectActivities.ActivitiesID == objectNewBEValidation.CategoryGroupID;
                        });
                    });
                    //=================Again Modified=============================
                    //changing method to include year with month also in report
                    objectGenericBEReports = (from v in objectGenericBEValidation.OrderBy(o => o.Year).ThenBy(p => p.MonthIndex)
                                              group v by new { v.Year, v.MonthIndex } into g
                                              from t in g.GroupBy(x => x.CategoryGroupID).ToList()
                                              select new RMC.BusinessEntities.BEReports
                                              {
                                                  ColumnName = objectGenericActivities.Find(delegate(RMC.DataService.ActivitiesCategory objectActivities)
                                                  {
                                                      return objectActivities.ActivitiesID == t.Key;
                                                  }).ActivitiesCategory1,
                                                  ColumnNumber = objectGenericActivities.Find(delegate(RMC.DataService.ActivitiesCategory objectActivities)
                                                  {
                                                      return objectActivities.ActivitiesID == t.Key;
                                                  }).ActivitiesID,
                                                  MonthName = BSCommon.GetMonthName(t.FirstOrDefault(r => r.Month != "").Month) + " " + t.FirstOrDefault().Year,
                                                  RowName = t.FirstOrDefault(r => r.HospitalUnitName != "").HospitalUnitName,
                                                  Values = string.Format("{0:#.##}%", ((double)t.Count(r => r.CategoryGroupID == t.Key) / (double)g.Count(c => c.CategoryGroupID != 0)) * 100),
                                                  ValuesSum = Convert.ToDouble(string.Format("{0:#.##}", ((double)t.Count(r => r.CategoryGroupID == t.Key) / (double)g.Count(c => c.CategoryGroupID != 0)) * 100)),
                                                  DataPoint = (double)g.Count(c => c.CategoryGroupID != 0)
                                              }).ToList();
                }
                else
                {
                    objectGenericBEValidation = objectGenericBEValidation.FindAll(delegate(RMC.BusinessEntities.BEValidation objectNewBEValidation)
                    {
                        return objectGenericLocationCategory.Exists(delegate(RMC.DataService.LocationCategory objectLocationCategory)
                        {
                            return objectLocationCategory.LocationID == objectNewBEValidation.CategoryGroupID;
                        });
                    });

                    //--method changed to include year with month also, in report
                    objectGenericBEReports = (from v in objectGenericBEValidation.OrderBy(o => o.Year).ThenBy(p => p.MonthIndex)
                                              group v by new { v.Year, v.MonthIndex } into g
                                              from t in g.GroupBy(x => x.CategoryGroupID).ToList()
                                              select new RMC.BusinessEntities.BEReports
                                              {
                                                  ColumnName = objectGenericLocationCategory.Find(delegate(RMC.DataService.LocationCategory objectLocationCategory)
                                                  {
                                                      return objectLocationCategory.LocationID == t.Key;
                                                  }).LocationCategory1,
                                                  ColumnNumber = objectGenericLocationCategory.Find(delegate(RMC.DataService.LocationCategory objectLocationCategory)
                                                  {
                                                      return objectLocationCategory.LocationID == t.Key;
                                                  }).LocationID,
                                                  MonthName = BSCommon.GetMonthName(t.FirstOrDefault(r => r.Month != "").Month) + " " + t.FirstOrDefault().Year,
                                                  RowName = t.FirstOrDefault(r => r.HospitalUnitName != "").HospitalUnitName,
                                                  Values = string.Format("{0:#.##}%", ((double)t.Count(r => r.CategoryGroupID == t.Key) / (double)g.Count(c => c.CategoryGroupID != 0)) * 100),
                                                  ValuesSum = Convert.ToDouble(string.Format("{0:#.##}", ((double)t.Count(r => r.CategoryGroupID == t.Key) / (double)g.Count(c => c.CategoryGroupID != 0)) * 100)),
                                                  DataPoint = (double)g.Count(c => c.CategoryGroupID != 0)
                                              }).ToList();
                }

                return objectGenericBEReports.OrderBy(o => o.ColumnName).ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public List<RMC.BusinessEntities.BEValidation> SearchHospitalsDataForSpecialCategory(int? hospitalUnitID, int? firstYear, int? lastYear, int? firstMonth, int? lastMonth, int? bedInUnitFrom, int optBedInUnitFrom, int? bedInUnitTo, int optBedInUnitTo, float? budgetedPatientFrom, int optBudgetedPatientFrom, float? budgetedPatientTo, int optBudgetedPatientTo, string startDate, string endDate, int? electronicDocumentFrom, int optElectronicDocumentFrom, int? electronicDocumentTo, int optElectronicDocumentTo, int docByException, string unitType, string pharmacyType, string hospitalType, int optHospitalSizeFrom, int? hospitalSizeFrom, int optHospitalSizeTo, int? hospitalSizeTo, int? countryId, int? stateId, string configName)
        {
            List<RMC.BusinessEntities.BEValidation> objectGenericBEValidation = null;
            IQueryable<RMC.DataService.NursePDASpecialType> queryableNursePDADetail = null;
            try
            {
                queryableNursePDADetail = (from v in _objectRMCDataContext.NursePDASpecialTypes
                                           where v.NursePDAInfo.HospitalDemographicInfo.HospitalDemographicID == (hospitalUnitID ?? v.NursePDAInfo.HospitalDemographicInfo.HospitalDemographicID)
                                           && v.NursePDAInfo.HospitalDemographicInfo.IsDeleted != true && v.NursePDAInfo.IsErrorExist == false
                                           orderby v.NursePDAInfo.HospitalDemographicInfo.HospitalUnitName
                                           select v);

                objectGenericBEValidation = queryableNursePDADetail.Select(v => new RMC.BusinessEntities.BEValidation
                {
                    HospitalID = v.NursePDAInfo.HospitalDemographicInfo.HospitalInfo.HospitalInfoID,
                    HospitalUnitID = v.NursePDAInfo.HospitalDemographicInfo.HospitalDemographicID,
                    RecordCounter = v.NursePDAInfo.HospitalDemographicInfo.HospitalInfo.RecordCounter,
                    SpecialCategory = v.SpecialCategory,
                    SpecialActivity = v.SpecialActivity,
                    HospitalUnitName = v.NursePDAInfo.HospitalDemographicInfo.HospitalUnitName,
                    Year = v.NursePDAInfo.Year,
                    Month = v.NursePDAInfo.Month,
                    MonthIndex = Convert.ToInt32(v.NursePDAInfo.Month)
                }).OrderBy(x => x.RecordCounter).ThenBy(x => x.HospitalUnitID).ToList<RMC.BusinessEntities.BEValidation>();

                return objectGenericBEValidation.OrderBy(x => x.RecordCounter).ToList();

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        /// <summary>
        /// Gets data for PieChart used by MonthlyData-PieCharts report
        /// </summary>
        public List<RMC.BusinessEntities.BEReports> GetDataForPieChartModified(string profileCategory, string value, int? valueAddedCategoryID, int? OthersCategoryID, int? LocationCategoryID, int? ActivitiesID, int? hospitalUnitID, int? firstYear, int? lastYear, int? firstMonth, int? lastMonth, int? bedInUnit, int optBedInUnit, float? budgetedPatient, int optBudgetedPatient, string startDate, string endDate, int? electronicDocument, int optElectronicDocument, int docByException, string unitType, string pharmacyType, int optHospitalSize, int? hospitalSize)
        {
            try
            {
                List<RMC.BusinessEntities.BEReports> objectGenericListBEReport = null;
                if (valueAddedCategoryID != null)
                {
                    objectGenericListBEReport = CalculationForTimeRNSummaryPieChart("", profileCategory.ToLower().Trim(), GetCategoryProfileDataByProfileID(valueAddedCategoryID.Value), SearchHospitalsDataForBenchmarkTest(hospitalUnitID, firstYear, lastYear, firstMonth, lastMonth, null, 0, null, 0, null, 0, null, 0, startDate, endDate, null, 0, null, 0, docByException, unitType, pharmacyType, null, 0, null, 0, null, null, null, null, null));
                }
                if (OthersCategoryID != null)
                {
                    objectGenericListBEReport = CalculationForTimeRNSummaryPieChart("", profileCategory.ToLower().Trim(), GetCategoryProfileDataByProfileID(OthersCategoryID.Value), SearchHospitalsDataForBenchmarkTest(hospitalUnitID, firstYear, lastYear, firstMonth, lastMonth, null, 0, null, 0, null, 0, null, 0, startDate, endDate, null, 0, null, 0, docByException, unitType, pharmacyType, null, 0, null, 0, null, null, null, null, null));
                }
                if (LocationCategoryID != null)
                {
                    objectGenericListBEReport = CalculationForTimeRNSummaryPieChart("", profileCategory.ToLower().Trim(), GetCategoryProfileDataByProfileID(LocationCategoryID.Value), SearchHospitalsDataForBenchmarkTest(hospitalUnitID, firstYear, lastYear, firstMonth, lastMonth, null, 0, null, 0, null, 0, null, 0, startDate, endDate, null, 0, null, 0, docByException, unitType, pharmacyType, null, 0, null, 0, null, null, null, null, null));
                }
                if (ActivitiesID != null)
                {
                    objectGenericListBEReport = CalculationForTimeRNSummaryPieChart("", profileCategory.ToLower().Trim(), GetCategoryProfileDataByProfileID(ActivitiesID.Value), SearchHospitalsDataForBenchmarkTest(hospitalUnitID, firstYear, lastYear, firstMonth, lastMonth, null, 0, null, 0, null, 0, null, 0, startDate, endDate, null, 0, null, 0, docByException, unitType, pharmacyType, null, 0, null, 0, null, null, null, null, null));
                }
                //======================For Special Category
                if (profileCategory == "Special Category")
                {
                    List<RMC.BusinessEntities.BEValidation> objectGenericBEValidation = SearchHospitalsDataForSpecialCategory(hospitalUnitID, firstYear, lastYear, firstMonth, lastMonth, null, 0, null, 0, null, 0, null, 0, startDate, endDate, null, 0, null, 0, docByException, unitType, pharmacyType, null, 0, null, 0, null, null, null, null);
                    List<RMC.DataService.NursePDASpecialType> objectGenericNursePDASpecialType = _objectRMCDataContext.NursePDASpecialTypes.ToList();

                    objectGenericListBEReport = (from v in objectGenericBEValidation.OrderBy(o => o.Year).ThenBy(p => p.MonthIndex)
                                                 where v.SpecialCategory == value
                                                 group v by new { v.Year, v.MonthIndex } into g
                                                 from t in g.GroupBy(x => x.SpecialActivity).ToList()
                                                 select new RMC.BusinessEntities.BEReports
                                                 {
                                                     ColumnName = objectGenericNursePDASpecialType.Find(delegate(RMC.DataService.NursePDASpecialType objectNursePDASpecialType)
                                                     {
                                                         return objectNursePDASpecialType.SpecialActivity == t.Key;
                                                     }).SpecialActivity,
                                                     ColumnNumber = 1,
                                                     MonthName = BSCommon.GetMonthName(t.FirstOrDefault(r => r.Month != "").Month) + " " + t.FirstOrDefault().Year,
                                                     RowName = t.FirstOrDefault(r => r.HospitalUnitName != "").HospitalUnitName,
                                                     Values = string.Format("{0:#.##}%", ((double)t.Count(r => r.SpecialActivity == t.Key) / (double)g.Count(c => c.SpecialActivity != "")) * 100),
                                                     ValuesSum = Convert.ToDouble(string.Format("{0:#.##}", ((double)t.Count(r => r.SpecialActivity == t.Key) / (double)g.Count(c => c.SpecialActivity != "")) * 100)),
                                                     DataPoint = (double)g.Count(c => c.SpecialActivity != "")
                                                 }).ToList();

                }
                //======================



                //---------------------------for Database Values
                if (profileCategory == "Database Values")
                {
                    List<RMC.BusinessEntities.BEValidation> objectGenericBEValidation = SearchHospitalsDataForBenchmarkTest(hospitalUnitID, firstYear, lastYear, firstMonth, lastMonth, null, 0, null, 0, null, 0, null, 0, startDate, endDate, null, 0, null, 0, docByException, unitType, pharmacyType, null, 0, null, 0, null, null, null, null, null);

                    if (value == "Activity")
                    {
                        List<RMC.DataService.Activity> objectGenericActivity = _objectRMCDataContext.Activities.ToList();

                        objectGenericBEValidation = objectGenericBEValidation.FindAll(delegate(RMC.BusinessEntities.BEValidation objectNewBEValidation)
                        {
                            return objectGenericActivity.Exists(delegate(RMC.DataService.Activity objectActivity)
                            {
                                return objectActivity.ActivityID == objectNewBEValidation.ActivityID;
                            });
                        });

                        objectGenericListBEReport = (from v in objectGenericBEValidation.OrderBy(o => o.Year).ThenBy(p => p.MonthIndex)
                                                     group v by new { v.Year, v.MonthIndex } into g
                                                     from t in g.GroupBy(x => x.ActivityID).ToList()
                                                     select new RMC.BusinessEntities.BEReports
                                                     {
                                                         ColumnName = objectGenericActivity.Find(delegate(RMC.DataService.Activity objectActivity)
                                                         {
                                                             return objectActivity.ActivityID == t.Key;
                                                         }).Activity1,
                                                         ColumnNumber = objectGenericActivity.Find(delegate(RMC.DataService.Activity objectActivity)
                                                         {
                                                             return objectActivity.ActivityID == t.Key;
                                                         }).ActivityID,
                                                         MonthName = BSCommon.GetMonthName(t.FirstOrDefault(r => r.Month != "").Month) + " " + t.FirstOrDefault().Year,
                                                         RowName = t.FirstOrDefault(r => r.HospitalUnitName != "").HospitalUnitName,
                                                         Values = string.Format("{0:#.##}%", ((double)t.Count(r => r.ActivityID == t.Key) / (double)g.Count(c => c.ActivityID != 0)) * 100),
                                                         ValuesSum = Convert.ToDouble(string.Format("{0:#.##}", ((double)t.Count(r => r.ActivityID == t.Key) / (double)g.Count(c => c.ActivityID != 0)) * 100)),
                                                         DataPoint = (double)g.Count(c => c.ActivityID != 0)
                                                     }).ToList();
                    }
                    if (value == "Sub-Activity")
                    {

                        List<RMC.DataService.SubActivity> objectGenericSubActivity = _objectRMCDataContext.SubActivities.ToList();

                        objectGenericBEValidation = objectGenericBEValidation.FindAll(delegate(RMC.BusinessEntities.BEValidation objectNewBEValidation)
                        {
                            return objectGenericSubActivity.Exists(delegate(RMC.DataService.SubActivity objectSubActivity)
                            {
                                return objectSubActivity.SubActivityID == objectNewBEValidation.SubActivityID;
                            });
                        });


                        objectGenericListBEReport = (from v in objectGenericBEValidation.OrderBy(o => o.Year).ThenBy(p => p.MonthIndex)
                                                     group v by new { v.Year, v.MonthIndex } into g
                                                     from t in g.GroupBy(x => x.SubActivityID).ToList()
                                                     select new RMC.BusinessEntities.BEReports
                                                     {
                                                         ColumnName = objectGenericSubActivity.Find(delegate(RMC.DataService.SubActivity objectSubActivity)
                                                         {
                                                             return objectSubActivity.SubActivityID == t.Key;
                                                         }).SubActivity1,
                                                         ColumnNumber = objectGenericSubActivity.Find(delegate(RMC.DataService.SubActivity objectSubActivity)
                                                         {
                                                             return objectSubActivity.SubActivityID == t.Key;
                                                         }).SubActivityID,
                                                         MonthName = BSCommon.GetMonthName(t.FirstOrDefault(r => r.Month != "").Month) + " " + t.FirstOrDefault().Year,
                                                         RowName = t.FirstOrDefault(r => r.HospitalUnitName != "").HospitalUnitName,
                                                         Values = string.Format("{0:#.##}%", ((double)t.Count(r => r.SubActivityID == t.Key) / (double)g.Count(c => c.SubActivityID != 0)) * 100),
                                                         ValuesSum = Convert.ToDouble(string.Format("{0:#.##}", ((double)t.Count(r => r.SubActivityID == t.Key) / (double)g.Count(c => c.SubActivityID != 0)) * 100)),
                                                         DataPoint = (double)g.Count(c => c.SubActivityID != 0)
                                                     }).ToList();
                    }
                    if (value == "Last Location")
                    {
                        List<RMC.DataService.LastLocation> objectGenericLastLocation = _objectRMCDataContext.LastLocations.ToList();

                        objectGenericBEValidation = objectGenericBEValidation.FindAll(delegate(RMC.BusinessEntities.BEValidation objectNewBEValidation)
                        {
                            return objectGenericLastLocation.Exists(delegate(RMC.DataService.LastLocation objectLastLocation)
                            {
                                return objectLastLocation.LastLocationID == objectNewBEValidation.LastLocationID;
                            });
                        });


                        objectGenericListBEReport = (from v in objectGenericBEValidation.OrderBy(o => o.Year).ThenBy(p => p.MonthIndex)
                                                     group v by new { v.Year, v.MonthIndex } into g
                                                     from t in g.GroupBy(x => x.LastLocationID).ToList()
                                                     select new RMC.BusinessEntities.BEReports
                                                     {
                                                         ColumnName = objectGenericLastLocation.Find(delegate(RMC.DataService.LastLocation objectLastLocation)
                                                         {
                                                             return objectLastLocation.LastLocationID == t.Key;
                                                         }).LastLocation1,
                                                         ColumnNumber = objectGenericLastLocation.Find(delegate(RMC.DataService.LastLocation objectLastLocation)
                                                         {
                                                             return objectLastLocation.LastLocationID == t.Key;
                                                         }).LastLocationID,
                                                         MonthName = BSCommon.GetMonthName(t.FirstOrDefault(r => r.Month != "").Month) + " " + t.FirstOrDefault().Year,
                                                         RowName = t.FirstOrDefault(r => r.HospitalUnitName != "").HospitalUnitName,
                                                         Values = string.Format("{0:#.##}%", ((double)t.Count(r => r.LastLocationID == t.Key) / (double)g.Count(c => c.LastLocationID != 0)) * 100),
                                                         ValuesSum = Convert.ToDouble(string.Format("{0:#.##}", ((double)t.Count(r => r.LastLocationID == t.Key) / (double)g.Count(c => c.LastLocationID != 0)) * 100)),
                                                         DataPoint = (double)g.Count(c => c.LastLocationID != 0)
                                                     }).ToList();
                    }
                    if (value == "Current Location")
                    {
                        List<RMC.DataService.Location> objectGenericLocation = _objectRMCDataContext.Locations.ToList();

                        objectGenericBEValidation = objectGenericBEValidation.FindAll(delegate(RMC.BusinessEntities.BEValidation objectNewBEValidation)
                        {
                            return objectGenericLocation.Exists(delegate(RMC.DataService.Location objectLocation)
                            {
                                return objectLocation.LocationID == objectNewBEValidation.LocationID;
                            });
                        });


                        objectGenericListBEReport = (from v in objectGenericBEValidation.OrderBy(o => o.Year).ThenBy(p => p.MonthIndex)
                                                     group v by new { v.Year, v.MonthIndex } into g
                                                     from t in g.GroupBy(x => x.LocationID).ToList()
                                                     select new RMC.BusinessEntities.BEReports
                                                     {
                                                         ColumnName = objectGenericLocation.Find(delegate(RMC.DataService.Location objectLocation)
                                                         {
                                                             return objectLocation.LocationID == t.Key;
                                                         }).Location1,
                                                         ColumnNumber = objectGenericLocation.Find(delegate(RMC.DataService.Location objectLocation)
                                                         {
                                                             return objectLocation.LocationID == t.Key;
                                                         }).LocationID,
                                                         MonthName = BSCommon.GetMonthName(t.FirstOrDefault(r => r.Month != "").Month) + " " + t.FirstOrDefault().Year,
                                                         RowName = t.FirstOrDefault(r => r.HospitalUnitName != "").HospitalUnitName,
                                                         Values = string.Format("{0:#.##}%", ((double)t.Count(r => r.LocationID == t.Key) / (double)g.Count(c => c.LocationID != 0)) * 100),
                                                         ValuesSum = Convert.ToDouble(string.Format("{0:#.##}", ((double)t.Count(r => r.LocationID == t.Key) / (double)g.Count(c => c.LocationID != 0)) * 100)),
                                                         DataPoint = (double)g.Count(c => c.LocationID != 0)
                                                     }).ToList();
                    }
                    if (value == "Staffing Model")
                    {
                        List<RMC.DataService.ResourceRequirement> objectGenericResourceRequirement = _objectRMCDataContext.ResourceRequirements.ToList();

                        objectGenericBEValidation = objectGenericBEValidation.FindAll(delegate(RMC.BusinessEntities.BEValidation objectNewBEValidation)
                        {
                            return objectGenericResourceRequirement.Exists(delegate(RMC.DataService.ResourceRequirement objectResourceRequirement)
                            {
                                return objectResourceRequirement.ResourceRequirementID == objectNewBEValidation.ResourceRequirementID;
                            });
                        });


                        objectGenericListBEReport = (from v in objectGenericBEValidation.OrderBy(o => o.Year).ThenBy(p => p.MonthIndex)
                                                     group v by new { v.Year, v.MonthIndex } into g
                                                     from t in g.GroupBy(x => x.ResourceRequirementID).ToList()
                                                     select new RMC.BusinessEntities.BEReports
                                                     {
                                                         ColumnName = objectGenericResourceRequirement.Find(delegate(RMC.DataService.ResourceRequirement objectResourceRequirement)
                                                         {
                                                             return objectResourceRequirement.ResourceRequirementID == t.Key;
                                                         }).ResourceRequirement1,
                                                         ColumnNumber = objectGenericResourceRequirement.Find(delegate(RMC.DataService.ResourceRequirement objectResourceRequirement)
                                                         {
                                                             return objectResourceRequirement.ResourceRequirementID == t.Key;
                                                         }).ResourceRequirementID,
                                                         MonthName = BSCommon.GetMonthName(t.FirstOrDefault(r => r.Month != "").Month) + " " + t.FirstOrDefault().Year,
                                                         RowName = t.FirstOrDefault(r => r.HospitalUnitName != "").HospitalUnitName,
                                                         Values = string.Format("{0:#.##}%", ((double)t.Count(r => r.ResourceRequirementID == t.Key) / (double)g.Count(c => c.ResourceRequirementID != 0)) * 100),
                                                         ValuesSum = Convert.ToDouble(string.Format("{0:#.##}", ((double)t.Count(r => r.ResourceRequirementID == t.Key) / (double)g.Count(c => c.ResourceRequirementID != 0)) * 100)),
                                                         DataPoint = (double)g.Count(c => c.ResourceRequirementID != 0)
                                                     }).ToList();
                    }
                    if (value == "Cognitive")
                    {
                        List<RMC.DataService.CognitiveCategory> objectGenericCognitive = _objectRMCDataContext.CognitiveCategories.ToList();

                        objectGenericBEValidation = objectGenericBEValidation.FindAll(delegate(RMC.BusinessEntities.BEValidation objectNewBEValidation)
                        {
                            return objectGenericCognitive.Exists(delegate(RMC.DataService.CognitiveCategory objectCognitive)
                            {
                                return objectCognitive.CognitiveCategoryID == objectNewBEValidation.CognitiveCategoryID;
                            });
                        });


                        objectGenericListBEReport = (from v in objectGenericBEValidation.OrderBy(o => o.Year).ThenBy(p => p.MonthIndex)
                                                     group v by new { v.Year, v.MonthIndex } into g
                                                     from t in g.GroupBy(x => x.CognitiveCategoryID).ToList()
                                                     select new RMC.BusinessEntities.BEReports
                                                     {
                                                         ColumnName = objectGenericCognitive.Find(delegate(RMC.DataService.CognitiveCategory objectCognitive)
                                                         {
                                                             return objectCognitive.CognitiveCategoryID== t.Key;
                                                         }).CognitiveCategoryText,
                                                         ColumnNumber = objectGenericCognitive.Find(delegate(RMC.DataService.CognitiveCategory objectCognitive)
                                                         {
                                                             return objectCognitive.CognitiveCategoryID == t.Key;
                                                         }).CognitiveCategoryID,
                                                         MonthName = BSCommon.GetMonthName(t.FirstOrDefault(r => r.Month != "").Month) + " " + t.FirstOrDefault().Year,
                                                         RowName = t.FirstOrDefault(r => r.HospitalUnitName != "").HospitalUnitName,
                                                         Values = string.Format("{0:#.##}%", ((double)t.Count(r => r.CognitiveCategoryID == t.Key) / (double)g.Count(c => c.CognitiveCategoryID != 0)) * 100),
                                                         ValuesSum = Convert.ToDouble(string.Format("{0:#.##}", ((double)t.Count(r => r.CognitiveCategoryID == t.Key) / (double)g.Count(c => c.CognitiveCategoryID != 0)) * 100)),
                                                         DataPoint = (double)g.Count(c => c.CognitiveCategoryID != 0)
                                                     }).ToList();
                    }
                }

                removeIncDecSymbol<BusinessEntities.BEReports>("ColumnName", ref objectGenericListBEReport);
                //
                List<RMC.BusinessEntities.BEReports> objectBEReportsResultant = (from r in objectGenericListBEReport.GroupBy(o => o.ColumnName)

                                                                                 select new RMC.BusinessEntities.BEReports
                                                                                 {
                                                                                     ColumnName = r.FirstOrDefault().ColumnName,
                                                                                     MonthName = "Hosp Avg",
                                                                                     Values = string.Format("{0:#.##}%", (r.Sum(x => x.ValuesSum) == 0) ? "0.00" : string.Format("{0:#.##}", r.Sum(x => x.ValuesSum) / r.Count())),
                                                                                     ValuesSum = (r.Sum(x => x.ValuesSum) == 0) ? 0.00 : r.Sum(x => x.ValuesSum) / r.Count(),
                                                                                     //DataPoint =  (dataPoint += r.FirstOrDefault().DataPoint)
                                                                                 }).ToList();

                List<double> objectDataPoint = (from r in objectGenericListBEReport.GroupBy(o => o.MonthName)

                                                select r.FirstOrDefault().DataPoint
                                    ).ToList();
                //RMC.BusinessEntities.BEReports objectNewBEReports = objectBEReportsResultant.Find(delegate(RMC.BusinessEntities.BEReports objectBERep)
                //{
                //    return objectBERep.MonthName == "Hosp Avg";
                //});

                objectBEReportsResultant.ForEach(delegate(RMC.BusinessEntities.BEReports objectBERep)
                {
                    objectBERep.DataPoint = Convert.ToDouble(string.Format("{0:#.##}", objectDataPoint.Sum() /* / objectDataPoint.Count*/));
                });
                //
               // return objectGenericListBEReport;
                objectBEReportsResultant=objectBEReportsResultant.OrderByDescending(x => x.ValuesSum).ToList();

                List<RMC.BusinessEntities.BEReports> newlist1=new List<BusinessEntities.BEReports>();

                if (objectBEReportsResultant.Count > 15)
                {
                    newlist1 = objectBEReportsResultant.Take(15).ToList();
                    newlist1 = newlist1.OrderBy(x => x.ValuesSum).ToList();
                    List<RMC.BusinessEntities.BEReports> remainingItemList = objectBEReportsResultant.Skip(15).ToList();
                    double _sum = remainingItemList.Sum(s => s.ValuesSum);
                    RMC.BusinessEntities.BEReports obj = new BusinessEntities.BEReports();
                    obj.ValuesSum = _sum;
                    obj.Name = null;
                    obj.ColumnName = "Remaining Categories";
                    obj.MonthName = "Hosp Avg";
                    obj.RowName = null;
                    obj.ValuesDecimal = 0;
                    obj.Values = _sum + "%";
                    obj.RecordCounter = 0;
                    obj.DataPoint = objectDataPoint.Sum();
                    newlist1.Insert(0,obj);

                }
                else 
                { 
                    newlist1 = objectBEReportsResultant.ToList();
                    newlist1 = newlist1.OrderBy(x => x.ValuesSum).ToList();
                }
                return newlist1;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //----------For Control Chart
        /// <summary>
        /// Gets the values of DatabaseValues(like Activity, Sub-Activity, Location etc) from their tables in database
        /// </summary>
        /// <param name="valueId">Id of the DatabaseValues(like Activity, Sub-Activity, Location etc)</param>
        /// <returns></returns>
        public List<RMC.BusinessEntities.BEReportsDatabaseValues> GetDatabaseValuesValue(string value, string profileCategory)
        {
            RMC.DataService.RMCDataContext objectRMCDataContext = new RMC.DataService.RMCDataContext();
            List<RMC.BusinessEntities.BEReportsDatabaseValues> objectBEReportsDatabaseValues = null;
            try
            {
                if (profileCategory == "Database Values")
                {
                    if (value == "Activity")
                    {
                        objectBEReportsDatabaseValues = (from a in objectRMCDataContext.Activities
                                                         select new RMC.BusinessEntities.BEReportsDatabaseValues
                                                         {
                                                             valueId = a.ActivityID,
                                                             value = a.Activity1
                                                         }).ToList();
                    }
                    if (value == "Sub-Activity")
                    {
                        objectBEReportsDatabaseValues = (from sa in objectRMCDataContext.SubActivities
                                                         select new RMC.BusinessEntities.BEReportsDatabaseValues
                                                         {
                                                             valueId = sa.SubActivityID,
                                                             value = sa.SubActivity1
                                                         }).ToList();
                    }

                    if (value == "Last Location")
                    {
                        objectBEReportsDatabaseValues = (from ll in objectRMCDataContext.LastLocations
                                                         select new RMC.BusinessEntities.BEReportsDatabaseValues
                                                         {
                                                             valueId = ll.LastLocationID,
                                                             value = ll.LastLocation1
                                                         }).ToList();
                    }

                    if (value == "Current Location")
                    {
                        objectBEReportsDatabaseValues = (from l in objectRMCDataContext.Locations
                                                         select new RMC.BusinessEntities.BEReportsDatabaseValues
                                                         {
                                                             valueId = l.LocationID,
                                                             value = l.Location1
                                                         }).ToList();
                    }
                    if (value == "Cognitive")
                    {
                        objectBEReportsDatabaseValues = (from cc in objectRMCDataContext.CognitiveCategories
                                                         select new RMC.BusinessEntities.BEReportsDatabaseValues
                                                         {
                                                             valueId = cc.CognitiveCategoryID,
                                                             value = cc.CognitiveCategoryText
                                                         }).ToList();
                    }
                    if (value == "Staffing Model")
                    {
                        objectBEReportsDatabaseValues = (from rr in objectRMCDataContext.ResourceRequirements
                                                         select new RMC.BusinessEntities.BEReportsDatabaseValues
                                                         {
                                                             valueId = rr.ResourceRequirementID,
                                                             value = rr.ResourceRequirement1
                                                         }).ToList();
                    }
                }
                else if (profileCategory == "Special Category")
                {
                    objectBEReportsDatabaseValues = (from st in objectRMCDataContext.NursePDASpecialTypes
                                                     where st.SpecialCategory == value
                                                     group st by new { st.SpecialActivity } into g
                                                     select new RMC.BusinessEntities.BEReportsDatabaseValues
                                                     {
                                                         value = g.Key.SpecialActivity.ToString(),
                                                         valueId = 1
                                                     }).OrderBy(a => a.value).ToList();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return objectBEReportsDatabaseValues;
        }

        /// <summary>
        /// Gets data for LineChart used by controlCharts Report
        /// </summary>
        public List<RMC.BusinessEntities.BEReports> GetDataForLineChartModified(string profileCategory, string profileValue, string activitiesID, string valueAddedCategoryID, string OthersCategoryID, string LocationCategoryID, int? hospitalUnitID, int? firstYear, int? lastYear, int? firstMonth, int? lastMonth, int? bedInUnitFrom, int optBedInUnitFrom, int? bedInUnitTo, int optBedInUnitTo, float? budgetedPatientFrom, int optBudgetedPatientFrom, float? budgetedPatientTo, int optBudgetedPatientTo, string startDate, string endDate, int? electronicDocumentFrom, int optElectronicDocumentFrom, int? electronicDocumentTo, int optElectronicDocumentTo, int docByException, string unitType, string pharmacyType, string hospitalType, int optHospitalSizeFrom, int? hospitalSizeFrom, int optHospitalSizeTo, int? hospitalSizeTo, int? countryId, int? stateId, string activities, string value, string others, string location, int? dataPointsFrom, int optDataPointsFrom, int? dataPointsTo, int optdataPointsTo, string configName, string unitIds, string dbValues)
        {
            try
            {
                List<RMC.BusinessEntities.BEReports> objectGenericListResult = null;
                List<RMC.BusinessEntities.BEReports> objectGenericListBEReportValueAddedProfile = null;
                List<RMC.BusinessEntities.BEReports> objectGenericListBEReportActivities = null;
                List<RMC.BusinessEntities.BEReports> objectGenericListBEReportValueAdded = null;
                List<RMC.BusinessEntities.BEReports> objectGenericListBEReportOthers = null;
                List<RMC.BusinessEntities.BEReports> objectGenericListBEReportLocation = null;
                List<RMC.BusinessEntities.BEReports> objectGenericListBEReport = null;
               
                if (profileCategory != "Database Values" && profileCategory != "Special Category")
                {
                    List<RMC.BusinessEntities.BEValidation> ObjectSearchHospitalsDataForBenchmark = SearchHospitalsDataForBenchmarkTest(hospitalUnitID, firstYear, lastYear, firstMonth, lastMonth, bedInUnitFrom, optBedInUnitFrom, bedInUnitTo, optBedInUnitTo, budgetedPatientFrom, optBudgetedPatientFrom, budgetedPatientTo, optBudgetedPatientTo, startDate, endDate, electronicDocumentFrom, optElectronicDocumentFrom, electronicDocumentTo, optElectronicDocumentTo, docByException, unitType, pharmacyType, hospitalType, optHospitalSizeFrom, hospitalSizeFrom, optHospitalSizeTo, hospitalSizeTo, countryId, stateId, configName, unitIds);
                    //Here hospitalUnitId is null because the chosen hospital unit is compared with global data or all hospital unit data (or National Database)
                    List<RMC.BusinessEntities.BEFunctionNames> objectGenericBEFunctionNames = CalculateFunctionValuesTest(activitiesID, valueAddedCategoryID, OthersCategoryID, LocationCategoryID, null, firstYear, lastYear, firstMonth, lastMonth, bedInUnitFrom, optBedInUnitFrom, bedInUnitTo, optBedInUnitTo, budgetedPatientFrom, optBudgetedPatientFrom, budgetedPatientTo, optBudgetedPatientTo, startDate, endDate, electronicDocumentFrom, optElectronicDocumentFrom, electronicDocumentTo, optElectronicDocumentTo, docByException, unitType, pharmacyType, hospitalType, optHospitalSizeFrom, hospitalSizeFrom, optHospitalSizeTo, hospitalSizeTo, countryId, stateId, activities, value, others, location, dataPointsFrom, optDataPointsFrom, dataPointsTo, optdataPointsTo, configName, unitIds);
                    removeIncDecSymbol<RMC.BusinessEntities.BEFunctionNames>("ColumnName", ref objectGenericBEFunctionNames);
                    List<RMC.BusinessEntities.BEFunctionNames> objectGenericListFilter = null;

                    //activitiesID
                    if (activitiesID != null)
                    {
                        if (activitiesID.Contains(","))
                        {
                            string[] strArrvalue = null;
                            strArrvalue = activities.Split(new char[] { ',' });
                            string[] strArrvalueActivitiesID = null;
                            strArrvalueActivitiesID = activitiesID.Split(new char[] { ',' });
                            int[] intArrvalueActivitiesID = new int[strArrvalueActivitiesID.Length];
                            for (int i = 0; i < strArrvalueActivitiesID.Length; i++)
                            {
                                intArrvalueActivitiesID[i] = int.Parse(strArrvalueActivitiesID[i]);
                                objectGenericListBEReportActivities = CalculationForTimeRNSummaryTest(strArrvalue[i], "activities", GetCategoryProfileDataByProfileID(intArrvalueActivitiesID[i]), ObjectSearchHospitalsDataForBenchmark, null, 0, null, 0);
                                if (i == 0)
                                {
                                    objectGenericListBEReportValueAdded = objectGenericListBEReportActivities;
                                }
                                if (i != 0)
                                {
                                    objectGenericListBEReportValueAdded.AddRange(objectGenericListBEReportActivities);
                                }
                            }
                        }
                        else
                        {
                            //objectGenericListBEReportValueAdded = CalculationForTimeRNSummaryTest(value, "value added", GetCategoryProfileDataByProfileID(Convert.ToInt32(valueAddedCategoryID)), SearchHospitalsDataForBenchmarkTest(hospitalUnitID, firstYear, lastYear, firstMonth, lastMonth, bedInUnitFrom, optBedInUnitFrom, bedInUnitTo, optBedInUnitTo, budgetedPatientFrom, optBudgetedPatientFrom, budgetedPatientTo, optBudgetedPatientTo, startDate, endDate, electronicDocumentFrom, optElectronicDocumentFrom, electronicDocumentTo, optElectronicDocumentTo, docByException, unitType, pharmacyType, hospitalType, optHospitalSizeFrom, hospitalSizeFrom, optHospitalSizeTo, hospitalSizeTo, countryId, stateId), dataPointsFrom, optDataPointsFrom, dataPointsTo, optdataPointsTo);
                            objectGenericListBEReportValueAdded = CalculationForTimeRNSummaryTest(activities, "activities", GetCategoryProfileDataByProfileID(Convert.ToInt32(activitiesID)), ObjectSearchHospitalsDataForBenchmark, null, 0, null, 0);
                        }
                    }   
                    //valueAddedCategoryID
                    if (valueAddedCategoryID != null)
                    {
                        if (valueAddedCategoryID.Contains(","))
                        {
                            string[] strArrvalue = null;
                            strArrvalue = value.Split(new char[] { ',' });
                            string[] strArrvalueAddedCategoryID = null;
                            strArrvalueAddedCategoryID = valueAddedCategoryID.Split(new char[] { ',' });
                            int[] intArrvalueAddedCategoryID = new int[strArrvalueAddedCategoryID.Length];
                            for (int i = 0; i < strArrvalueAddedCategoryID.Length; i++)
                            {
                                intArrvalueAddedCategoryID[i] = int.Parse(strArrvalueAddedCategoryID[i]);
                                objectGenericListBEReportValueAddedProfile = CalculationForTimeRNSummaryTest(strArrvalue[i], "value added", GetCategoryProfileDataByProfileID(intArrvalueAddedCategoryID[i]), ObjectSearchHospitalsDataForBenchmark, null, 0, null, 0);
                                if (i == 0)
                                {
                                    objectGenericListBEReportValueAdded = objectGenericListBEReportValueAddedProfile;
                                }
                                if (i != 0)
                                {
                                    objectGenericListBEReportValueAdded.AddRange(objectGenericListBEReportValueAddedProfile);
                                }
                            }
                        }
                        else
                        {
                            //objectGenericListBEReportValueAdded = CalculationForTimeRNSummaryTest(value, "value added", GetCategoryProfileDataByProfileID(Convert.ToInt32(valueAddedCategoryID)), SearchHospitalsDataForBenchmarkTest(hospitalUnitID, firstYear, lastYear, firstMonth, lastMonth, bedInUnitFrom, optBedInUnitFrom, bedInUnitTo, optBedInUnitTo, budgetedPatientFrom, optBudgetedPatientFrom, budgetedPatientTo, optBudgetedPatientTo, startDate, endDate, electronicDocumentFrom, optElectronicDocumentFrom, electronicDocumentTo, optElectronicDocumentTo, docByException, unitType, pharmacyType, hospitalType, optHospitalSizeFrom, hospitalSizeFrom, optHospitalSizeTo, hospitalSizeTo, countryId, stateId), dataPointsFrom, optDataPointsFrom, dataPointsTo, optdataPointsTo);
                            objectGenericListBEReportValueAdded = CalculationForTimeRNSummaryTest(value, "value added", GetCategoryProfileDataByProfileID(Convert.ToInt32(valueAddedCategoryID)), ObjectSearchHospitalsDataForBenchmark, null, 0, null, 0);
                        }
                    }                    

                    //OthersCategoryID
                    if (OthersCategoryID != null)
                    {
                        if (OthersCategoryID.Contains(","))
                        {
                            string[] strArrothers = null;
                            strArrothers = others.Split(new char[] { ',' });
                            string[] strArrOthersCategoryID = null;
                            strArrOthersCategoryID = OthersCategoryID.Split(new char[] { ',' });
                            int[] intArrOthersCategoryID = new int[strArrOthersCategoryID.Length];
                            for (int i = 0; i < strArrOthersCategoryID.Length; i++)
                            {
                                intArrOthersCategoryID[i] = int.Parse(strArrOthersCategoryID[i]);
                                objectGenericListBEReportOthers = CalculationForTimeRNSummaryTest(strArrothers[i], "others", GetCategoryProfileDataByProfileID(intArrOthersCategoryID[i]), ObjectSearchHospitalsDataForBenchmark, null, 0, null, 0);
                                //objectGenericListBEReportValueAdded.AddRange(objectGenericListBEReportOthers);
                                if (objectGenericListBEReportValueAdded == null)
                                {
                                    objectGenericListBEReportValueAdded = objectGenericListBEReportOthers;
                                }
                                else
                                {
                                    objectGenericListBEReportValueAdded.AddRange(objectGenericListBEReportOthers);
                                }
                            }
                        }
                        else
                        {
                            objectGenericListBEReportOthers = CalculationForTimeRNSummaryTest(others, "others", GetCategoryProfileDataByProfileID(Convert.ToInt32(OthersCategoryID)), ObjectSearchHospitalsDataForBenchmark, null, 0, null, 0);
                            if (objectGenericListBEReportValueAdded == null)
                            {
                                objectGenericListBEReportValueAdded = objectGenericListBEReportOthers;
                            }
                            else
                            {
                                objectGenericListBEReportValueAdded.AddRange(objectGenericListBEReportOthers);
                            }
                        }
                    }

                    //LocationCategoryID;
                    if (LocationCategoryID != null)
                    {
                        if (LocationCategoryID.Contains(","))
                        {
                            string[] strArrLocation = null;
                            strArrLocation = location.Split(new char[] { ',' });
                            string[] strArrLocationCategoryID = null;
                            strArrLocationCategoryID = LocationCategoryID.Split(new char[] { ',' });
                            int[] intArrLocationCategoryID = new int[strArrLocationCategoryID.Length];
                            for (int i = 0; i < strArrLocationCategoryID.Length; i++)
                            {
                                intArrLocationCategoryID[i] = int.Parse(strArrLocationCategoryID[i]);
                                objectGenericListBEReportLocation = CalculationForTimeRNSummaryTest(strArrLocation[i], "location", GetCategoryProfileDataByProfileID(intArrLocationCategoryID[i]), ObjectSearchHospitalsDataForBenchmark, null, 0, null, 0);
                                //objectGenericListBEReportValueAdded.AddRange(objectGenericListBEReportLocation);
                                if (objectGenericListBEReportValueAdded == null)
                                {
                                    objectGenericListBEReportValueAdded = objectGenericListBEReportLocation;
                                }
                                else
                                {
                                    objectGenericListBEReportValueAdded.AddRange(objectGenericListBEReportLocation);
                                }
                            }
                        }
                        else
                        {
                            objectGenericListBEReportLocation = CalculationForTimeRNSummaryTest(location, "location", GetCategoryProfileDataByProfileID(Convert.ToInt32(LocationCategoryID)), ObjectSearchHospitalsDataForBenchmark, null, 0, null, 0);
                            if (objectGenericListBEReportValueAdded == null)
                            {
                                objectGenericListBEReportValueAdded = objectGenericListBEReportLocation;
                            }
                            else
                            {
                                objectGenericListBEReportValueAdded.AddRange(objectGenericListBEReportLocation);
                            }
                        }
                    }
                    removeIncDecSymbol<BusinessEntities.BEReports>("ColumnName", ref objectGenericListBEReportValueAdded);
                    objectGenericListBEReport = objectGenericListBEReportValueAdded;

                    objectGenericListResult = objectGenericListBEReport.FindAll(delegate(RMC.BusinessEntities.BEReports objectBEReport)
                    {
                        return objectBEReport.ColumnName.ToLower().Trim() == (profileValue.Trim() + " (" + profileCategory.Trim() + ")").ToLower().Trim();
                    });

                    objectGenericListFilter = objectGenericBEFunctionNames.FindAll(delegate(RMC.BusinessEntities.BEFunctionNames objectBEFunctionNames)
                    {
                        if (objectBEFunctionNames.ColumnName != null)
                        {
                            return objectBEFunctionNames.ColumnName.ToLower().Trim() == (profileValue.Trim() + " (" + profileCategory.Trim() + ")").ToLower().Trim();
                        }
                        else
                        {
                            return false;
                        }
                    });

                    int count = objectGenericListResult.Count;
                    //List<RMC.BusinessEntities.BEReports> objectGenericListResultTemp = new List<RMC.BusinessEntities.BEReports>();
                    //objectGenericListResultTemp = objectGenericListResult;
                    objectGenericListFilter.ForEach(delegate(RMC.BusinessEntities.BEFunctionNames objectBEFunctionNames)
                    {
                        for (int index = 0; index < count; index++)
                        {
                            RMC.BusinessEntities.BEReports objectBEReports = new RMC.BusinessEntities.BEReports();

                            objectBEReports.ColumnName = objectBEFunctionNames.FunctionName;
                            //objectBEReports.MonthName = BSCommon.GetMonthName(Convert.ToString(index + 1));
                            objectBEReports.MonthName = objectGenericListResult[index].MonthName;
                            objectBEReports.Values = objectBEFunctionNames.FunctionValueText;
                            objectBEReports.ValuesSum = objectBEFunctionNames.FunctionNameDouble;

                            objectGenericListResult.Add(objectBEReports);
                        }
                    });
                }

                //======================For Special Category Type
                else if (profileCategory == "Special Category")
                {
                    List<RMC.BusinessEntities.BEValidation> objectGenericBEValidation = SearchHospitalsDataForSpecialCategory(hospitalUnitID, firstYear, lastYear, firstMonth, lastMonth, null, 0, null, 0, null, 0, null, 0, startDate, endDate, null, 0, null, 0, docByException, unitType, pharmacyType, null, 0, null, 0, null, null, null, null);
                    List<RMC.BusinessEntities.BEValidation> objectGenericBEValidationAll = SearchHospitalsDataForSpecialCategory(null, firstYear, lastYear, firstMonth, lastMonth, null, 0, null, 0, null, 0, null, 0, startDate, endDate, null, 0, null, 0, docByException, unitType, pharmacyType, null, 0, null, 0, null, null, null, null);
                    List<RMC.DataService.NursePDASpecialType> objectGenericNursePDASpecialType = _objectRMCDataContext.NursePDASpecialTypes.ToList();
                    List<RMC.BusinessEntities.BEReports> objectNewGenericBEReports = null;
                    List<RMC.BusinessEntities.BEFunctionNames> objectGenericBEFunctionNames = new List<RMC.BusinessEntities.BEFunctionNames>();

                    objectGenericListResult = (from v in objectGenericBEValidation.OrderBy(o => o.Year).ThenBy(p => p.MonthIndex)
                                               where v.SpecialCategory == profileValue
                                               group v by new { v.Year, v.MonthIndex } into g
                                               from t in g.GroupBy(x => x.SpecialActivity).ToList()
                                               select new RMC.BusinessEntities.BEReports
                                               {
                                                   ColumnName = objectGenericNursePDASpecialType.Find(delegate(RMC.DataService.NursePDASpecialType objectNursePDASpecialType)
                                                   {
                                                       return objectNursePDASpecialType.SpecialActivity == t.Key;
                                                   }).SpecialActivity,
                                                   ColumnNumber = 1,
                                                   MonthName = BSCommon.GetMonthName(t.FirstOrDefault(r => r.Month != "").Month) + " " + t.FirstOrDefault().Year,
                                                   RowName = t.FirstOrDefault(r => r.HospitalUnitName != "").HospitalUnitName,
                                                   Values = string.Format("{0:#.##}%", ((double)t.Count(r => r.SpecialActivity == t.Key) / (double)g.Count(c => c.SpecialActivity != "")) * 100),
                                                   ValuesSum = Convert.ToDouble(string.Format("{0:#.##}", ((double)t.Count(r => r.SpecialActivity == t.Key) / (double)g.Count(c => c.SpecialActivity != "")) * 100)),
                                                   //DataPoint = (double)g.Count(c => c.SpecialActivity != "")
                                               }).ToList();

                    objectGenericListResult = objectGenericListResult.FindAll(delegate(RMC.BusinessEntities.BEReports objectBEReport)
                    {
                        return objectBEReport.ColumnName.ToLower().Trim() == dbValues.ToLower().Trim();
                    });

                    objectGenericBEValidationAll = objectGenericBEValidationAll.FindAll(delegate(RMC.BusinessEntities.BEValidation objectNewBEValidation)
                    {
                        if (objectNewBEValidation.SpecialCategory != null)
                        {
                            return objectNewBEValidation.SpecialCategory.ToLower().Trim() == profileValue.ToLower().Trim();
                        }
                        else { return false; }
                        
                    });

                    objectNewGenericBEReports = (from v in objectGenericBEValidationAll.GroupBy(o => o.HospitalUnitID).ToList()
                                                 from t in v.GroupBy(x => x.SpecialActivity).ToList()
                                                 select new RMC.BusinessEntities.BEReports
                                                 {
                                                     Name = "Special Category",
                                                     ColumnName = objectGenericNursePDASpecialType.Find(delegate(RMC.DataService.NursePDASpecialType objectNursePDASpecialType)
                                                     {
                                                         return objectNursePDASpecialType.SpecialActivity == t.Key;
                                                     }).SpecialActivity,
                                                     //ColumnName = "Deliver FoodTray",
                                                     ColumnNumber = 1,
                                                     MonthName = BSCommon.GetMonthName(t.FirstOrDefault(r => r.Month != "").Month) + " " + t.FirstOrDefault().Year,
                                                     RowName = t.FirstOrDefault(r => r.HospitalUnitName != "").HospitalUnitName,
                                                     Values = string.Format("{0:#.##}%", ((double)t.Count(r => r.SpecialActivity == t.Key) / (double)v.Count(c => c.SpecialActivity != "")) * 100),
                                                     ValuesSum = Convert.ToDouble(string.Format("{0:#.##}", ((double)t.Count(r => r.SpecialActivity == t.Key) / (double)v.Count(c => c.SpecialActivity != "")) * 100)),
                                                     DataPoint = (double)v.Count(c => c.SpecialActivity != ""),
                                                 }).ToList();


                    objectNewGenericBEReports = objectNewGenericBEReports.FindAll(delegate(RMC.BusinessEntities.BEReports objectBEReport)
                    {
                        return objectBEReport.ColumnName.ToLower().Trim() == dbValues.ToLower().Trim();
                    });

                    if (objectNewGenericBEReports != null)
                    {
                        //filteration according to DataPoints
                        if (dataPointsFrom != null && dataPointsTo == null)
                        {
                            if (optDataPointsFrom == 1)
                            {
                                objectNewGenericBEReports = objectNewGenericBEReports.Where(x => Convert.ToInt32(x.DataPoint) < dataPointsFrom).ToList();
                            }
                            if (optDataPointsFrom == 2)
                            {
                                objectNewGenericBEReports = objectNewGenericBEReports.Where(x => x.DataPoint > dataPointsFrom).ToList();
                            }
                            if (optDataPointsFrom == 3)
                            {
                                objectNewGenericBEReports = objectNewGenericBEReports.Where(x => x.DataPoint == dataPointsFrom).ToList();
                            }
                            if (optDataPointsFrom == 4)
                            {
                                objectNewGenericBEReports = objectNewGenericBEReports.Where(x => x.DataPoint >= dataPointsFrom).ToList();
                            }
                            if (optDataPointsFrom == 5)
                            {
                                objectNewGenericBEReports = objectNewGenericBEReports.Where(x => x.DataPoint <= dataPointsFrom).ToList();
                            }
                            if (optDataPointsFrom == 6)
                            {
                                objectNewGenericBEReports = objectNewGenericBEReports.Where(x => x.DataPoint != dataPointsFrom).ToList();
                            }
                        }

                        if ((optDataPointsFrom == 0 && optdataPointsTo == 0) && (dataPointsFrom != null && dataPointsTo != null))
                        {
                            objectNewGenericBEReports = objectNewGenericBEReports.Where(x => x.DataPoint >= dataPointsFrom && x.DataPoint <= dataPointsTo).ToList();
                        }

                        foreach (var r in objectNewGenericBEReports.GroupBy(o => o.ColumnName))
                        {
                            //Calculate Minimum value.
                            RMC.BusinessEntities.BEFunctionNames objectNewBEFunctionNamesMin = new RMC.BusinessEntities.BEFunctionNames();
                            double valueMin = 0;
                            valueMin = r.Select(s => Convert.ToDouble((s.Values.Length > 0) ? s.Values.Substring(0, s.Values.Length - 1) : "0")).Min();

                            objectNewBEFunctionNamesMin.ColumnName = r.FirstOrDefault().ColumnName;
                            objectNewBEFunctionNamesMin.FunctionName = "Minimum";
                            objectNewBEFunctionNamesMin.FunctionNameDouble = valueMin;
                            objectNewBEFunctionNamesMin.FunctionValueText = string.Format("{0:#.##}%", (valueMin == 0) ? "0.00" : valueMin.ToString());

                            objectGenericBEFunctionNames.Add(objectNewBEFunctionNamesMin);

                            //Calculate Maximum value
                            RMC.BusinessEntities.BEFunctionNames objectNewBEFunctionNamesMax = new RMC.BusinessEntities.BEFunctionNames();
                            double valueMax = 0;
                            valueMax = r.Select(s => Convert.ToDouble((s.Values.Length > 0) ? s.Values.Substring(0, s.Values.Length - 1) : "0")).Max();

                            objectNewBEFunctionNamesMax.ColumnName = r.FirstOrDefault().ColumnName;
                            objectNewBEFunctionNamesMax.FunctionName = "Maximum";
                            objectNewBEFunctionNamesMax.FunctionNameDouble = valueMax;
                            objectNewBEFunctionNamesMax.FunctionValueText = string.Format("{0:#.##}%", (valueMax == 0) ? "0.00" : valueMax.ToString());

                            //Calculate Average value
                            RMC.BusinessEntities.BEFunctionNames objectNewBEFunctionNamesAvg = new RMC.BusinessEntities.BEFunctionNames();
                            List<double> valueSum = null;
                            valueSum = r.Select(s => Convert.ToDouble((s.Values.Length > 0) ? s.Values.Substring(0, s.Values.Length - 1) : "0")).ToList();

                            objectNewBEFunctionNamesAvg.ColumnName = r.FirstOrDefault().ColumnName;
                            objectNewBEFunctionNamesAvg.FunctionName = "Average";
                            objectNewBEFunctionNamesAvg.FunctionNameDouble = (valueSum.Sum() / r.Select(s => s.RowName).Distinct().Count());
                            objectNewBEFunctionNamesAvg.FunctionValueText = string.Format("{0:#.##}%", (objectNewBEFunctionNamesAvg.FunctionNameDouble == 0) ? "0.00" : string.Format("{0:#.##}", objectNewBEFunctionNamesAvg.FunctionNameDouble));

                            //Median Value.
                            RMC.BusinessEntities.BEFunctionNames objectNewBEFunctionNamesMed = new RMC.BusinessEntities.BEFunctionNames();
                            int median = 0;
                            int medianEven = 0;
                            int count = r.Select(s => s.RowName).Distinct().Count();
                            if (count % 2 == 0)
                            {
                                median = count / 2;
                                medianEven = median + 1;

                                objectNewBEFunctionNamesMed.ColumnName = r.FirstOrDefault().ColumnName;
                                objectNewBEFunctionNamesMed.FunctionName = "Median";
                                objectNewBEFunctionNamesMed.FunctionNameDouble = (r.OrderBy(x => x.ValuesSum).Select(s => s.ValuesSum).ToList().ElementAt(median - 1) + r.OrderBy(x => x.ValuesSum).Select(s => s.ValuesSum).ToList().ElementAt(medianEven - 1)) / 2;
                                objectNewBEFunctionNamesMed.FunctionValueText = string.Format("{0:#.##}%", string.Format("{0:#.##}", (objectNewBEFunctionNamesMed.FunctionNameDouble == 0.00) ? "0.00" : string.Format("{0:#.##}", objectNewBEFunctionNamesMed.FunctionNameDouble)));
                            }
                            else
                            {
                                median = (count + 1) / 2;

                                objectNewBEFunctionNamesMed.ColumnName = r.FirstOrDefault().ColumnName;
                                objectNewBEFunctionNamesMed.FunctionName = "Median";
                                objectNewBEFunctionNamesMed.FunctionNameDouble = r.OrderBy(x => x.ValuesSum).Select(s => s.ValuesSum).ToList().ElementAt(median - 1);
                                objectNewBEFunctionNamesMed.FunctionValueText = string.Format("{0:#.##}%", string.Format("{0:#.##}", (objectNewBEFunctionNamesMed.FunctionNameDouble == 0.00) ? "0.00" : string.Format("{0:#.##}", objectNewBEFunctionNamesMed.FunctionNameDouble)));
                            }
                            //median--;

                            //Quartile
                            RMC.BusinessEntities.BEFunctionNames objectNewBEFunctionNamesQuartile1 = new RMC.BusinessEntities.BEFunctionNames();

                            objectNewBEFunctionNamesQuartile1.ColumnName = r.FirstOrDefault().ColumnName;
                            objectNewBEFunctionNamesQuartile1.FunctionName = "Quartile(1)";
                            objectNewBEFunctionNamesQuartile1.FunctionNameDouble = CalculateQuartile(count, 1, r.Select(s => s.ValuesSum).ToList());
                            objectNewBEFunctionNamesQuartile1.FunctionValueText = string.Format("{0:#.##}%", (objectNewBEFunctionNamesQuartile1.FunctionNameDouble == 0.00) ? "0.00" : string.Format("{0:#.##}", objectNewBEFunctionNamesQuartile1.FunctionNameDouble));

                            RMC.BusinessEntities.BEFunctionNames objectNewBEFunctionNamesQuartile3 = new RMC.BusinessEntities.BEFunctionNames();

                            objectNewBEFunctionNamesQuartile3.ColumnName = r.FirstOrDefault().ColumnName;
                            objectNewBEFunctionNamesQuartile3.FunctionName = "Quartile(3)";
                            objectNewBEFunctionNamesQuartile3.FunctionNameDouble = CalculateQuartile(count, 3, r.Select(s => s.ValuesSum).ToList());
                            objectNewBEFunctionNamesQuartile3.FunctionValueText = string.Format("{0:#.##}%", (objectNewBEFunctionNamesQuartile3.FunctionNameDouble == 0.00) ? "0.00" : string.Format("{0:#.##}", objectNewBEFunctionNamesQuartile3.FunctionNameDouble));

                            objectGenericBEFunctionNames.Add(objectNewBEFunctionNamesQuartile1);
                            objectGenericBEFunctionNames.Add(objectNewBEFunctionNamesMed);
                            objectGenericBEFunctionNames.Add(objectNewBEFunctionNamesAvg);
                            objectGenericBEFunctionNames.Add(objectNewBEFunctionNamesQuartile3);
                            objectGenericBEFunctionNames.Add(objectNewBEFunctionNamesMax);
                        }

                        int countt = objectGenericListResult.Count;
                        //List<RMC.BusinessEntities.BEReports> objectGenericListResultTemp = new List<RMC.BusinessEntities.BEReports>();
                        //objectGenericListResultTemp = objectGenericListResult;
                        objectGenericBEFunctionNames.ForEach(delegate(RMC.BusinessEntities.BEFunctionNames objectBEFunctionNames)
                        {
                            for (int index = 0; index < countt; index++)
                            {
                                RMC.BusinessEntities.BEReports objectBEReports = new RMC.BusinessEntities.BEReports();

                                objectBEReports.ColumnName = objectBEFunctionNames.FunctionName;
                                //objectBEReports.MonthName = BSCommon.GetMonthName(Convert.ToString(index + 1));
                                objectBEReports.MonthName = objectGenericListResult[index].MonthName;
                                objectBEReports.Values = objectBEFunctionNames.FunctionValueText;
                                objectBEReports.ValuesSum = objectBEFunctionNames.FunctionNameDouble;

                                objectGenericListResult.Add(objectBEReports);
                            }
                        });
                    }
                }
                //======================

                else if (profileCategory == "Database Values")
                {
                    List<RMC.BusinessEntities.BEValidation> objectGenericBEValidation = SearchHospitalsDataForBenchmarkTest(hospitalUnitID, firstYear, lastYear, firstMonth, lastMonth, null, 0, null, 0, null, 0, null, 0, startDate, endDate, null, 0, null, 0, docByException, unitType, pharmacyType, null, 0, null, 0, null, null, null, configName, null);
                    //Here hospitalId is null to compare with single hospital data
                    List<RMC.BusinessEntities.BEValidation> objectGenericBEValidationAll = SearchHospitalsDataForBenchmarkTest(null, firstYear, lastYear, firstMonth, lastMonth, null, 0, null, 0, null, 0, null, 0, startDate, endDate, null, 0, null, 0, docByException, unitType, pharmacyType, null, 0, null, 0, null, null, null, configName, null);
                    List<RMC.BusinessEntities.BEReports> objectNewGenericBEReports = null;
                    List<RMC.BusinessEntities.BEFunctionNames> objectGenericBEFunctionNames = new List<RMC.BusinessEntities.BEFunctionNames>();

                    if (profileValue == "Activity")
                    {
                        List<RMC.DataService.Activity> objectGenericActivity = _objectRMCDataContext.Activities.ToList();

                        objectGenericBEValidation = objectGenericBEValidation.FindAll(delegate(RMC.BusinessEntities.BEValidation objectNewBEValidation)
                        {
                            return objectGenericActivity.Exists(delegate(RMC.DataService.Activity objectActivity)
                            {
                                return objectActivity.ActivityID == objectNewBEValidation.ActivityID;
                            });
                        });

                        objectGenericListResult = (from v in objectGenericBEValidation.OrderBy(o => o.Year).ThenBy(p => p.MonthIndex)
                                                   group v by new { v.Year, v.MonthIndex } into g
                                                   from t in g.GroupBy(x => x.ActivityID).ToList()
                                                   select new RMC.BusinessEntities.BEReports
                                                   {
                                                       Name = "Activity",
                                                       ColumnName = objectGenericActivity.Find(delegate(RMC.DataService.Activity objectActivity)
                                                       {
                                                           return objectActivity.ActivityID == t.Key;
                                                       }).Activity1,
                                                       //ColumnName = "Deliver FoodTray",
                                                       ColumnNumber = objectGenericActivity.Find(delegate(RMC.DataService.Activity objectActivity)
                                                       {
                                                           return objectActivity.ActivityID == t.Key;
                                                       }).ActivityID,
                                                       MonthName = BSCommon.GetMonthName(t.FirstOrDefault(r => r.Month != "").Month) + " " + t.FirstOrDefault().Year,
                                                       RowName = t.FirstOrDefault(r => r.HospitalUnitName != "").HospitalUnitName,
                                                       Values = string.Format("{0:#.##}%", ((double)t.Count(r => r.ActivityID == t.Key) / (double)g.Count(c => c.ActivityID != 0)) * 100),
                                                       ValuesSum = Convert.ToDouble(string.Format("{0:#.##}", ((double)t.Count(r => r.ActivityID == t.Key) / (double)g.Count(c => c.ActivityID != 0)) * 100))
                                                   }).ToList();

                        objectGenericListResult = objectGenericListResult.FindAll(delegate(RMC.BusinessEntities.BEReports objectBEReport)
                        {
                            return objectBEReport.ColumnName.ToLower().Trim() == dbValues.ToLower().Trim();
                        });

                        //for comparing with National Database means calculating function values (min, max, quartile etc)

                        objectGenericBEValidationAll = objectGenericBEValidationAll.FindAll(delegate(RMC.BusinessEntities.BEValidation objectNewBEValidation)
                        {
                            return objectGenericActivity.Exists(delegate(RMC.DataService.Activity objectActivity)
                            {
                                return objectActivity.ActivityID == objectNewBEValidation.ActivityID;
                            });
                        });

                        objectNewGenericBEReports = (from v in objectGenericBEValidationAll.GroupBy(o => o.HospitalUnitIDCounter).ToList()
                                                     from t in v.GroupBy(x => x.ActivityID).ToList()
                                                     select new RMC.BusinessEntities.BEReports
                                                     {
                                                         Name = "Activity",
                                                         ColumnName = objectGenericActivity.Find(delegate(RMC.DataService.Activity objectActivity)
                                                         {
                                                             return objectActivity.ActivityID == t.Key;
                                                         }).Activity1,
                                                         //ColumnName = "Deliver FoodTray",
                                                         ColumnNumber = objectGenericActivity.Find(delegate(RMC.DataService.Activity objectActivity)
                                                         {
                                                             return objectActivity.ActivityID == t.Key;
                                                         }).ActivityID,
                                                         MonthName = BSCommon.GetMonthName(t.FirstOrDefault(r => r.Month != "").Month) + " " + t.FirstOrDefault().Year,
                                                         RowName = t.FirstOrDefault(r => r.HospitalUnitName != "").HospitalUnitName,
                                                         Values = string.Format("{0:#.##}%", ((double)t.Count(r => r.ActivityID == t.Key) / (double)v.Count(c => c.ActivityID != 0)) * 100),
                                                         //ValuesSum = Convert.ToDouble(string.Format("{0:#.##}", ((double)t.Count(r => r.ActivityID == t.Key) / (double)v.Count(c => c.ActivityID != 0)) * 100)),
                                                         ValuesSum = ((double)t.Count(r => r.ActivityID == t.Key) / (double)v.Count(c => c.ActivityID != 0)) * 100,
                                                         DataPoint = (double)v.Count(c => c.ActivityID != 0),
                                                     }).ToList();


                        objectNewGenericBEReports = objectNewGenericBEReports.FindAll(delegate(RMC.BusinessEntities.BEReports objectBEReport)
                        {
                            return objectBEReport.ColumnName.ToLower().Trim() == dbValues.ToLower().Trim();
                        });

                    }
                    if (profileValue == "Sub-Activity")
                    {
                        List<RMC.DataService.SubActivity> objectGenericSubActivity = _objectRMCDataContext.SubActivities.ToList();

                        objectGenericBEValidation = objectGenericBEValidation.FindAll(delegate(RMC.BusinessEntities.BEValidation objectNewBEValidation)
                        {
                            return objectGenericSubActivity.Exists(delegate(RMC.DataService.SubActivity objectSubActivity)
                            {
                                return objectSubActivity.SubActivityID == objectNewBEValidation.SubActivityID;
                            });
                        });

                        objectGenericListResult = (from v in objectGenericBEValidation.OrderBy(o => o.Year).ThenBy(p => p.MonthIndex)
                                                   group v by new { v.Year, v.MonthIndex } into g
                                                   from t in g.GroupBy(x => x.SubActivityID).ToList()
                                                   select new RMC.BusinessEntities.BEReports
                                                   {
                                                       Name = "Sub-Activity",
                                                       ColumnName = objectGenericSubActivity.Find(delegate(RMC.DataService.SubActivity objectSubActivity)
                                                       {
                                                           return objectSubActivity.SubActivityID == t.Key;
                                                       }).SubActivity1,
                                                       ColumnNumber = objectGenericSubActivity.Find(delegate(RMC.DataService.SubActivity objectSubActivity)
                                                       {
                                                           return objectSubActivity.SubActivityID == t.Key;
                                                       }).SubActivityID,
                                                       MonthName = BSCommon.GetMonthName(t.FirstOrDefault(r => r.Month != "").Month) + " " + t.FirstOrDefault().Year,
                                                       RowName = t.FirstOrDefault(r => r.HospitalUnitName != "").HospitalUnitName,
                                                       Values = string.Format("{0:#.##}%", ((double)t.Count(r => r.SubActivityID == t.Key) / (double)g.Count(c => c.SubActivityID != 0)) * 100),
                                                       ValuesSum = Convert.ToDouble(string.Format("{0:#.##}", ((double)t.Count(r => r.SubActivityID == t.Key) / (double)g.Count(c => c.SubActivityID != 0)) * 100))
                                                   }).ToList();

                        objectGenericListResult = objectGenericListResult.FindAll(delegate(RMC.BusinessEntities.BEReports objectBEReport)
                        {
                            return objectBEReport.ColumnName.ToLower().Trim() == dbValues.ToLower().Trim();
                        });

                        //for comparing with National Database means calculating function values (min, max, quartile etc)

                        objectGenericBEValidationAll = objectGenericBEValidationAll.FindAll(delegate(RMC.BusinessEntities.BEValidation objectNewBEValidation)
                        {
                            return objectGenericSubActivity.Exists(delegate(RMC.DataService.SubActivity objectSubActivity)
                            {
                                return objectSubActivity.SubActivityID == objectNewBEValidation.SubActivityID;
                            });
                        });

                        objectNewGenericBEReports = (from v in objectGenericBEValidationAll.GroupBy(o => o.HospitalUnitIDCounter).ToList()
                                                     from t in v.GroupBy(x => x.SubActivityID).ToList()
                                                     select new RMC.BusinessEntities.BEReports
                                                     {
                                                         Name = "Sub-Activity",
                                                         ColumnName = objectGenericSubActivity.Find(delegate(RMC.DataService.SubActivity objectSubActivity)
                                                         {
                                                             return objectSubActivity.SubActivityID == t.Key;
                                                         }).SubActivity1,
                                                         ColumnNumber = objectGenericSubActivity.Find(delegate(RMC.DataService.SubActivity objectSubActivity)
                                                         {
                                                             return objectSubActivity.SubActivityID == t.Key;
                                                         }).SubActivityID,
                                                         MonthName = BSCommon.GetMonthName(t.FirstOrDefault(r => r.Month != "").Month) + " " + t.FirstOrDefault().Year,
                                                         RowName = t.FirstOrDefault(r => r.HospitalUnitName != "").HospitalUnitName,
                                                         Values = string.Format("{0:#.##}%", ((double)t.Count(r => r.SubActivityID == t.Key) / (double)v.Count(c => c.SubActivityID != 0)) * 100),
                                                         ValuesSum = Convert.ToDouble(string.Format("{0:#.##}", ((double)t.Count(r => r.SubActivityID == t.Key) / (double)v.Count(c => c.SubActivityID != 0)) * 100))
                                                     }).ToList();


                        objectNewGenericBEReports = objectNewGenericBEReports.FindAll(delegate(RMC.BusinessEntities.BEReports objectBEReport)
                        {
                            return objectBEReport.ColumnName.ToLower().Trim() == dbValues.ToLower().Trim();
                        });


                    }
                    if (profileValue == "Last Location")
                    {
                        List<RMC.DataService.LastLocation> objectGenericLastLocation = _objectRMCDataContext.LastLocations.ToList();

                        objectGenericBEValidation = objectGenericBEValidation.FindAll(delegate(RMC.BusinessEntities.BEValidation objectNewBEValidation)
                        {
                            return objectGenericLastLocation.Exists(delegate(RMC.DataService.LastLocation objectLastLocation)
                            {
                                return objectLastLocation.LastLocationID == objectNewBEValidation.LastLocationID;
                            });
                        });

                        objectGenericListResult = (from v in objectGenericBEValidation.OrderBy(o => o.Year).ThenBy(p => p.MonthIndex)
                                                   group v by new { v.Year, v.MonthIndex } into g
                                                   from t in g.GroupBy(x => x.LastLocationID).ToList()
                                                   select new RMC.BusinessEntities.BEReports
                                                   {
                                                       Name = "Last Location",
                                                       ColumnName = objectGenericLastLocation.Find(delegate(RMC.DataService.LastLocation objectLastLocation)
                                                       {
                                                           return objectLastLocation.LastLocationID == t.Key;
                                                       }).LastLocation1,
                                                       ColumnNumber = objectGenericLastLocation.Find(delegate(RMC.DataService.LastLocation objectLastLocation)
                                                       {
                                                           return objectLastLocation.LastLocationID == t.Key;
                                                       }).LastLocationID,
                                                       MonthName = BSCommon.GetMonthName(t.FirstOrDefault(r => r.Month != "").Month) + " " + t.FirstOrDefault().Year,
                                                       RowName = t.FirstOrDefault(r => r.HospitalUnitName != "").HospitalUnitName,
                                                       Values = string.Format("{0:#.##}%", ((double)t.Count(r => r.LastLocationID == t.Key) / (double)g.Count(c => c.LastLocationID != 0)) * 100),
                                                       ValuesSum = Convert.ToDouble(string.Format("{0:#.##}", ((double)t.Count(r => r.LastLocationID == t.Key) / (double)g.Count(c => c.LastLocationID != 0)) * 100))
                                                   }).ToList();

                        objectGenericListResult = objectGenericListResult.FindAll(delegate(RMC.BusinessEntities.BEReports objectBEReport)
                        {
                            return objectBEReport.ColumnName.ToLower().Trim() == dbValues.ToLower().Trim();
                        });

                        //for comparing with National Database means calculating function values (min, max, quartile etc)

                        objectGenericBEValidationAll = objectGenericBEValidationAll.FindAll(delegate(RMC.BusinessEntities.BEValidation objectNewBEValidation)
                        {
                            return objectGenericLastLocation.Exists(delegate(RMC.DataService.LastLocation objectLastLocation)
                            {
                                return objectLastLocation.LastLocationID == objectNewBEValidation.LastLocationID;
                            });
                        });

                        objectNewGenericBEReports = (from v in objectGenericBEValidationAll.GroupBy(o => o.HospitalUnitIDCounter).ToList()
                                                     from t in v.GroupBy(x => x.LastLocationID).ToList()
                                                     select new RMC.BusinessEntities.BEReports
                                                     {
                                                         Name = "Last Location",
                                                         ColumnName = objectGenericLastLocation.Find(delegate(RMC.DataService.LastLocation objectLastLocation)
                                                         {
                                                             return objectLastLocation.LastLocationID == t.Key;
                                                         }).LastLocation1,
                                                         ColumnNumber = objectGenericLastLocation.Find(delegate(RMC.DataService.LastLocation objectLastLocation)
                                                         {
                                                             return objectLastLocation.LastLocationID == t.Key;
                                                         }).LastLocationID,
                                                         MonthName = BSCommon.GetMonthName(t.FirstOrDefault(r => r.Month != "").Month) + " " + t.FirstOrDefault().Year,
                                                         RowName = t.FirstOrDefault(r => r.HospitalUnitName != "").HospitalUnitName,
                                                         Values = string.Format("{0:#.##}%", ((double)t.Count(r => r.LastLocationID == t.Key) / (double)v.Count(c => c.LastLocationID != 0)) * 100),
                                                         ValuesSum = Convert.ToDouble(string.Format("{0:#.##}", ((double)t.Count(r => r.LastLocationID == t.Key) / (double)v.Count(c => c.LastLocationID != 0)) * 100))
                                                     }).ToList();

                        objectNewGenericBEReports = objectNewGenericBEReports.FindAll(delegate(RMC.BusinessEntities.BEReports objectBEReport)
                        {
                            return objectBEReport.ColumnName.ToLower().Trim() == dbValues.ToLower().Trim();
                        });

                    }
                    if (profileValue == "Current Location")
                    {
                        List<RMC.DataService.Location> objectGenericLocation = _objectRMCDataContext.Locations.ToList();

                        objectGenericBEValidation = objectGenericBEValidation.FindAll(delegate(RMC.BusinessEntities.BEValidation objectNewBEValidation)
                        {
                            return objectGenericLocation.Exists(delegate(RMC.DataService.Location objectLocation)
                            {
                                return objectLocation.LocationID == objectNewBEValidation.LocationID;
                            });
                        });


                        objectGenericListResult = (from v in objectGenericBEValidation.OrderBy(o => o.Year).ThenBy(p => p.MonthIndex)
                                                   group v by new { v.Year, v.MonthIndex } into g
                                                   from t in g.GroupBy(x => x.LocationID).ToList()
                                                   select new RMC.BusinessEntities.BEReports
                                                   {
                                                       Name = "Current Location",
                                                       ColumnName = objectGenericLocation.Find(delegate(RMC.DataService.Location objectLocation)
                                                       {
                                                           return objectLocation.LocationID == t.Key;
                                                       }).Location1,
                                                       ColumnNumber = objectGenericLocation.Find(delegate(RMC.DataService.Location objectLocation)
                                                       {
                                                           return objectLocation.LocationID == t.Key;
                                                       }).LocationID,
                                                       MonthName = BSCommon.GetMonthName(t.FirstOrDefault(r => r.Month != "").Month) + " " + t.FirstOrDefault().Year,
                                                       RowName = t.FirstOrDefault(r => r.HospitalUnitName != "").HospitalUnitName,
                                                       Values = string.Format("{0:#.##}%", ((double)t.Count(r => r.LocationID == t.Key) / (double)g.Count(c => c.LocationID != 0)) * 100),
                                                       ValuesSum = Convert.ToDouble(string.Format("{0:#.##}", ((double)t.Count(r => r.LocationID == t.Key) / (double)g.Count(c => c.LocationID != 0)) * 100))
                                                   }).ToList();


                        objectGenericListResult = objectGenericListResult.FindAll(delegate(RMC.BusinessEntities.BEReports objectBEReport)
                        {
                            return objectBEReport.ColumnName.ToLower().Trim() == dbValues.ToLower().Trim();
                        });

                        //for comparing with National Database means calculating function values (min, max, quartile etc)

                        objectGenericBEValidationAll = objectGenericBEValidationAll.FindAll(delegate(RMC.BusinessEntities.BEValidation objectNewBEValidation)
                        {
                            return objectGenericLocation.Exists(delegate(RMC.DataService.Location objectLocation)
                            {
                                return objectLocation.LocationID == objectNewBEValidation.LocationID;
                            });
                        });

                        objectNewGenericBEReports = (from v in objectGenericBEValidationAll.GroupBy(o => o.HospitalUnitIDCounter).ToList()
                                                     from t in v.GroupBy(x => x.LocationID).ToList()
                                                     select new RMC.BusinessEntities.BEReports
                                                     {
                                                         Name = "Current Location",
                                                         ColumnName = objectGenericLocation.Find(delegate(RMC.DataService.Location objectLocation)
                                                         {
                                                             return objectLocation.LocationID == t.Key;
                                                         }).Location1,
                                                         ColumnNumber = objectGenericLocation.Find(delegate(RMC.DataService.Location objectLocation)
                                                         {
                                                             return objectLocation.LocationID == t.Key;
                                                         }).LocationID,
                                                         MonthName = BSCommon.GetMonthName(t.FirstOrDefault(r => r.Month != "").Month) + " " + t.FirstOrDefault().Year,
                                                         RowName = t.FirstOrDefault(r => r.HospitalUnitName != "").HospitalUnitName,
                                                         Values = string.Format("{0:#.##}%", ((double)t.Count(r => r.LocationID == t.Key) / (double)v.Count(c => c.LocationID != 0)) * 100),
                                                         ValuesSum = Convert.ToDouble(string.Format("{0:#.##}", ((double)t.Count(r => r.LocationID == t.Key) / (double)v.Count(c => c.LocationID != 0)) * 100))
                                                     }).ToList();

                        objectNewGenericBEReports = objectNewGenericBEReports.FindAll(delegate(RMC.BusinessEntities.BEReports objectBEReport)
                        {
                            return objectBEReport.ColumnName.ToLower().Trim() == dbValues.ToLower().Trim();
                        });

                    }
                    if (profileValue == "Staffing Model")
                    {
                        List<RMC.DataService.ResourceRequirement> objectGenericResourceRequirement = _objectRMCDataContext.ResourceRequirements.ToList();

                        objectGenericBEValidation = objectGenericBEValidation.FindAll(delegate(RMC.BusinessEntities.BEValidation objectNewBEValidation)
                        {
                            return objectGenericResourceRequirement.Exists(delegate(RMC.DataService.ResourceRequirement objectResourceRequirement)
                            {
                                return objectResourceRequirement.ResourceRequirementID == objectNewBEValidation.ResourceRequirementID;
                            });
                        });


                        objectGenericListResult = (from v in objectGenericBEValidation.OrderBy(o => o.Year).ThenBy(p => p.MonthIndex)
                                                   group v by new { v.Year, v.MonthIndex } into g
                                                   from t in g.GroupBy(x => x.ResourceRequirementID).ToList()
                                                   select new RMC.BusinessEntities.BEReports
                                                   {
                                                       Name = "Staffing Model",
                                                       ColumnName = objectGenericResourceRequirement.Find(delegate(RMC.DataService.ResourceRequirement objectResourceRequirement)
                                                       {
                                                           return objectResourceRequirement.ResourceRequirementID == t.Key;
                                                       }).ResourceRequirement1,
                                                       ColumnNumber = objectGenericResourceRequirement.Find(delegate(RMC.DataService.ResourceRequirement objectResourceRequirement)
                                                       {
                                                           return objectResourceRequirement.ResourceRequirementID == t.Key;
                                                       }).ResourceRequirementID,
                                                       MonthName = BSCommon.GetMonthName(t.FirstOrDefault(r => r.Month != "").Month) + " " + t.FirstOrDefault().Year,
                                                       RowName = t.FirstOrDefault(r => r.HospitalUnitName != "").HospitalUnitName,
                                                       Values = string.Format("{0:#.##}%", ((double)t.Count(r => r.ResourceRequirementID == t.Key) / (double)g.Count(c => c.ResourceRequirementID != 0)) * 100),
                                                       ValuesSum = Convert.ToDouble(string.Format("{0:#.##}", ((double)t.Count(r => r.ResourceRequirementID == t.Key) / (double)g.Count(c => c.ResourceRequirementID != 0)) * 100))
                                                   }).ToList();

                        objectGenericListResult = objectGenericListResult.FindAll(delegate(RMC.BusinessEntities.BEReports objectBEReport)
                        {
                            return objectBEReport.ColumnName.ToLower().Trim() == dbValues.ToLower().Trim();
                        });

                        //for comparing with National Database means calculating function values (min, max, quartile etc)

                        objectGenericBEValidationAll = objectGenericBEValidationAll.FindAll(delegate(RMC.BusinessEntities.BEValidation objectNewBEValidation)
                        {
                            return objectGenericResourceRequirement.Exists(delegate(RMC.DataService.ResourceRequirement objectResourceRequirement)
                            {
                                return objectResourceRequirement.ResourceRequirementID == objectNewBEValidation.ResourceRequirementID;
                            });
                        });

                        objectNewGenericBEReports = (from v in objectGenericBEValidationAll.GroupBy(o => o.HospitalUnitIDCounter).ToList()
                                                     from t in v.GroupBy(x => x.ResourceRequirementID).ToList()
                                                     select new RMC.BusinessEntities.BEReports
                                                     {
                                                         Name = "Staffing Model",
                                                         ColumnName = objectGenericResourceRequirement.Find(delegate(RMC.DataService.ResourceRequirement objectResourceRequirement)
                                                         {
                                                             return objectResourceRequirement.ResourceRequirementID == t.Key;
                                                         }).ResourceRequirement1,
                                                         ColumnNumber = objectGenericResourceRequirement.Find(delegate(RMC.DataService.ResourceRequirement objectResourceRequirement)
                                                         {
                                                             return objectResourceRequirement.ResourceRequirementID == t.Key;
                                                         }).ResourceRequirementID,
                                                         MonthName = BSCommon.GetMonthName(t.FirstOrDefault(r => r.Month != "").Month) + " " + t.FirstOrDefault().Year,
                                                         RowName = t.FirstOrDefault(r => r.HospitalUnitName != "").HospitalUnitName,
                                                         Values = string.Format("{0:#.##}%", ((double)t.Count(r => r.ResourceRequirementID == t.Key) / (double)v.Count(c => c.ResourceRequirementID != 0)) * 100),
                                                         ValuesSum = Convert.ToDouble(string.Format("{0:#.##}", ((double)t.Count(r => r.ResourceRequirementID == t.Key) / (double)v.Count(c => c.ResourceRequirementID != 0)) * 100))
                                                     }).ToList();

                        objectNewGenericBEReports = objectNewGenericBEReports.FindAll(delegate(RMC.BusinessEntities.BEReports objectBEReport)
                        {
                            return objectBEReport.ColumnName.ToLower().Trim() == dbValues.ToLower().Trim();
                        });
                    }

                    if (objectNewGenericBEReports != null)
                    {
                        //filteration according to DataPoints
                        if (dataPointsFrom != null && dataPointsTo == null)
                        {
                            if (optDataPointsFrom == 1)
                            {
                                objectNewGenericBEReports = objectNewGenericBEReports.Where(x => Convert.ToInt32(x.DataPoint) < dataPointsFrom).ToList();
                            }
                            if (optDataPointsFrom == 2)
                            {
                                objectNewGenericBEReports = objectNewGenericBEReports.Where(x => x.DataPoint > dataPointsFrom).ToList();
                            }
                            if (optDataPointsFrom == 3)
                            {
                                objectNewGenericBEReports = objectNewGenericBEReports.Where(x => x.DataPoint == dataPointsFrom).ToList();
                            }
                            if (optDataPointsFrom == 4)
                            {
                                objectNewGenericBEReports = objectNewGenericBEReports.Where(x => x.DataPoint >= dataPointsFrom).ToList();
                            }
                            if (optDataPointsFrom == 5)
                            {
                                objectNewGenericBEReports = objectNewGenericBEReports.Where(x => x.DataPoint <= dataPointsFrom).ToList();
                            }
                            if (optDataPointsFrom == 6)
                            {
                                objectNewGenericBEReports = objectNewGenericBEReports.Where(x => x.DataPoint != dataPointsFrom).ToList();
                            }
                        }

                        if ((optDataPointsFrom == 0 && optdataPointsTo == 0) && (dataPointsFrom != null && dataPointsTo != null))
                        {
                            objectNewGenericBEReports = objectNewGenericBEReports.Where(x => x.DataPoint >= dataPointsFrom && x.DataPoint <= dataPointsTo).ToList();
                        }

                        foreach (var r in objectNewGenericBEReports.GroupBy(o => o.ColumnName))
                        {
                            //Calculate Minimum value.
                            RMC.BusinessEntities.BEFunctionNames objectNewBEFunctionNamesMin = new RMC.BusinessEntities.BEFunctionNames();
                            double valueMin = 0;
                            valueMin = r.Select(s => Convert.ToDouble((s.Values.Length > 0) ? s.Values.Substring(0, s.Values.Length - 1) : "0")).Min();

                            objectNewBEFunctionNamesMin.ColumnName = r.FirstOrDefault().ColumnName;
                            objectNewBEFunctionNamesMin.FunctionName = "Minimum";
                            objectNewBEFunctionNamesMin.FunctionNameDouble = valueMin;
                            objectNewBEFunctionNamesMin.FunctionValueText = string.Format("{0:#.##}%", (valueMin == 0) ? "0.00" : valueMin.ToString());

                            objectGenericBEFunctionNames.Add(objectNewBEFunctionNamesMin);

                            //Calculate Maximum value
                            RMC.BusinessEntities.BEFunctionNames objectNewBEFunctionNamesMax = new RMC.BusinessEntities.BEFunctionNames();
                            double valueMax = 0;
                            valueMax = r.Select(s => Convert.ToDouble((s.Values.Length > 0) ? s.Values.Substring(0, s.Values.Length - 1) : "0")).Max();

                            objectNewBEFunctionNamesMax.ColumnName = r.FirstOrDefault().ColumnName;
                            objectNewBEFunctionNamesMax.FunctionName = "Maximum";
                            objectNewBEFunctionNamesMax.FunctionNameDouble = valueMax;
                            objectNewBEFunctionNamesMax.FunctionValueText = string.Format("{0:#.##}%", (valueMax == 0) ? "0.00" : valueMax.ToString());

                            //Calculate Average value
                            RMC.BusinessEntities.BEFunctionNames objectNewBEFunctionNamesAvg = new RMC.BusinessEntities.BEFunctionNames();
                            List<double> valueSum = null;
                            valueSum = r.Select(s => Convert.ToDouble((s.Values.Length > 0) ? s.Values.Substring(0, s.Values.Length - 1) : "0")).ToList();

                            objectNewBEFunctionNamesAvg.ColumnName = r.FirstOrDefault().ColumnName;
                            objectNewBEFunctionNamesAvg.FunctionName = "Average";
                            objectNewBEFunctionNamesAvg.FunctionNameDouble = (valueSum.Sum() / r.Select(s => s.RowName).Distinct().Count());
                            objectNewBEFunctionNamesAvg.FunctionValueText = string.Format("{0:#.##}%", (objectNewBEFunctionNamesAvg.FunctionNameDouble == 0) ? "0.00" : string.Format("{0:#.##}", objectNewBEFunctionNamesAvg.FunctionNameDouble));

                            //Median Value.
                            RMC.BusinessEntities.BEFunctionNames objectNewBEFunctionNamesMed = new RMC.BusinessEntities.BEFunctionNames();
                            int median = 0;
                            int medianEven = 0;
                            int count = r.Select(s => s.RowName).Distinct().Count();
                            if (count % 2 == 0)
                            {
                                median = count / 2;
                                medianEven = median + 1;

                                objectNewBEFunctionNamesMed.ColumnName = r.FirstOrDefault().ColumnName;
                                objectNewBEFunctionNamesMed.FunctionName = "Median";
                                objectNewBEFunctionNamesMed.FunctionNameDouble = (r.OrderBy(x => x.ValuesSum).Select(s => s.ValuesSum).ToList().ElementAt(median - 1) + r.OrderBy(x => x.ValuesSum).Select(s => s.ValuesSum).ToList().ElementAt(medianEven - 1)) / 2;
                                objectNewBEFunctionNamesMed.FunctionValueText = string.Format("{0:#.##}%", string.Format("{0:#.##}", (objectNewBEFunctionNamesMed.FunctionNameDouble == 0.00) ? "0.00" : string.Format("{0:#.##}", objectNewBEFunctionNamesMed.FunctionNameDouble)));
                            }
                            else
                            {
                                median = (count + 1) / 2;

                                objectNewBEFunctionNamesMed.ColumnName = r.FirstOrDefault().ColumnName;
                                objectNewBEFunctionNamesMed.FunctionName = "Median";
                                objectNewBEFunctionNamesMed.FunctionNameDouble = r.OrderBy(x => x.ValuesSum).Select(s => s.ValuesSum).ToList().ElementAt(median - 1);
                                objectNewBEFunctionNamesMed.FunctionValueText = string.Format("{0:#.##}%", string.Format("{0:#.##}", (objectNewBEFunctionNamesMed.FunctionNameDouble == 0.00) ? "0.00" : string.Format("{0:#.##}", objectNewBEFunctionNamesMed.FunctionNameDouble)));
                            }
                            //median--;

                            //Quartile
                            RMC.BusinessEntities.BEFunctionNames objectNewBEFunctionNamesQuartile1 = new RMC.BusinessEntities.BEFunctionNames();

                            objectNewBEFunctionNamesQuartile1.ColumnName = r.FirstOrDefault().ColumnName;
                            objectNewBEFunctionNamesQuartile1.FunctionName = "Quartile(1)";
                            objectNewBEFunctionNamesQuartile1.FunctionNameDouble = CalculateQuartile(count, 1, r.Select(s => s.ValuesSum).ToList());
                            objectNewBEFunctionNamesQuartile1.FunctionValueText = string.Format("{0:#.##}%", (objectNewBEFunctionNamesQuartile1.FunctionNameDouble == 0.00) ? "0.00" : string.Format("{0:#.##}", objectNewBEFunctionNamesQuartile1.FunctionNameDouble));

                            RMC.BusinessEntities.BEFunctionNames objectNewBEFunctionNamesQuartile3 = new RMC.BusinessEntities.BEFunctionNames();

                            objectNewBEFunctionNamesQuartile3.ColumnName = r.FirstOrDefault().ColumnName;
                            objectNewBEFunctionNamesQuartile3.FunctionName = "Quartile(3)";
                            objectNewBEFunctionNamesQuartile3.FunctionNameDouble = CalculateQuartile(count, 3, r.Select(s => s.ValuesSum).ToList());
                            objectNewBEFunctionNamesQuartile3.FunctionValueText = string.Format("{0:#.##}%", (objectNewBEFunctionNamesQuartile3.FunctionNameDouble == 0.00) ? "0.00" : string.Format("{0:#.##}", objectNewBEFunctionNamesQuartile3.FunctionNameDouble));

                            objectGenericBEFunctionNames.Add(objectNewBEFunctionNamesQuartile1);
                            objectGenericBEFunctionNames.Add(objectNewBEFunctionNamesMed);
                            objectGenericBEFunctionNames.Add(objectNewBEFunctionNamesAvg);
                            objectGenericBEFunctionNames.Add(objectNewBEFunctionNamesQuartile3);
                            objectGenericBEFunctionNames.Add(objectNewBEFunctionNamesMax);
                        }

                        int countt = objectGenericListResult.Count;
                        //List<RMC.BusinessEntities.BEReports> objectGenericListResultTemp = new List<RMC.BusinessEntities.BEReports>();
                        //objectGenericListResultTemp = objectGenericListResult;
                        objectGenericBEFunctionNames.ForEach(delegate(RMC.BusinessEntities.BEFunctionNames objectBEFunctionNames)
                        {
                            for (int index = 0; index < countt; index++)
                            {
                                RMC.BusinessEntities.BEReports objectBEReports = new RMC.BusinessEntities.BEReports();

                                objectBEReports.ColumnName = objectBEFunctionNames.FunctionName;
                                //objectBEReports.MonthName = BSCommon.GetMonthName(Convert.ToString(index + 1));
                                objectBEReports.MonthName = objectGenericListResult[index].MonthName;
                                objectBEReports.Values = objectBEFunctionNames.FunctionValueText;
                                objectBEReports.ValuesSum = objectBEFunctionNames.FunctionNameDouble;

                                objectGenericListResult.Add(objectBEReports);
                            }
                        });
                    }

                }               
                    return objectGenericListResult;               
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //----------For Location Profile
        /// <summary>
        /// Gets the Location Profile Data for Generating Report which is used by Location Profile Report
        /// </summary>
        public List<RMC.BusinessEntities.BEReportsLocationProfile> GetDataForLocationProfileReport(int? hospitalUnitID, int? firstYear, int? lastYear, int? firstMonth, int? lastMonth, int? bedInUnitFrom, int optBedInUnitFrom, int? bedInUnitTo, int optBedInUnitTo, float? budgetedPatientFrom, int optBudgetedPatientFrom, float? budgetedPatientTo, int optBudgetedPatientTo, string startDate, string endDate, int? electronicDocumentFrom, int optElectronicDocumentFrom, int? electronicDocumentTo, int optElectronicDocumentTo, int docByException, string unitType, string pharmacyType, string hospitalType, int optHospitalSizeFrom, int? hospitalSizeFrom, int optHospitalSizeTo, int? hospitalSizeTo, int? countryId, int? stateId, string configName)
        {




            IQueryable<RMC.BusinessEntities.BEReportsLocationProfile> objectGeneric = null;
            List<RMC.BusinessEntities.BEReportsLocationProfile> objectGenericBEValidation;
            IQueryable<RMC.DataService.NursePDADetail> queryableNursePDADetail = null;
            try
            {
                DateTime datTimeFirst, datTimeLast;
                if (firstYear != null && lastYear != null && firstYear.Value > 0 && lastYear.Value > 0)
                {
                    datTimeFirst = Convert.ToDateTime(firstMonth.Value.ToString() + "/01/" + firstYear.Value.ToString());
                    datTimeLast = Convert.ToDateTime(lastMonth.Value.ToString() + "/01/" + lastYear.Value.ToString());

                    queryableNursePDADetail = (from v in _objectRMCDataContext.NursePDADetails
                                               where (Convert.ToDateTime(v.NursePDAInfo.Month + "/01/" + v.NursePDAInfo.Year) >= datTimeFirst && Convert.ToDateTime(v.NursePDAInfo.Month + "/01/" + v.NursePDAInfo.Year) <= datTimeLast)
                                               && v.NursePDAInfo.HospitalDemographicInfo.StartDate >= Convert.ToDateTime(startDate == null ? Convert.ToString(v.NursePDAInfo.HospitalDemographicInfo.StartDate) : startDate)
                                               && v.NursePDAInfo.HospitalDemographicInfo.HospitalDemographicID == (hospitalUnitID ?? v.NursePDAInfo.HospitalDemographicInfo.HospitalDemographicID)
                                                   //&& v.NursePDAInfo.HospitalDemographicInfo.BedsInUnit > 1000 //v.NursePDAInfo.HospitalDemographicInfo.BedsInUnit == (bedInUnit ?? v.NursePDAInfo.HospitalDemographicInfo.BedsInUnit) && v.NursePDAInfo.HospitalDemographicInfo.BudgetedPatientsPerNurse == (budgetedPatient ?? v.NursePDAInfo.HospitalDemographicInfo.BudgetedPatientsPerNurse)
                                                   //&& v.NursePDAInfo.HospitalDemographicInfo.ElectronicDocumentation == (electronicDocument ?? v.NursePDAInfo.HospitalDemographicInfo.ElectronicDocumentation) &&
                                                   //&& v.NursePDAInfo.HospitalDemographicInfo.UnitType == (unitType ?? v.NursePDAInfo.HospitalDemographicInfo.UnitType) && v.NursePDAInfo.HospitalDemographicInfo.PharmacyType == (pharmacyType ?? v.NursePDAInfo.HospitalDemographicInfo.PharmacyType)
                                               && v.NursePDAInfo.HospitalDemographicInfo.HospitalInfo.State.CountryID == (countryId ?? v.NursePDAInfo.HospitalDemographicInfo.HospitalInfo.State.CountryID) && v.NursePDAInfo.HospitalDemographicInfo.HospitalInfo.StateID == (stateId ?? v.NursePDAInfo.HospitalDemographicInfo.HospitalInfo.StateID)
                                               && v.NursePDAInfo.HospitalDemographicInfo.IsDeleted != true && v.IsErrorExist == false && v.NursePDAInfo.IsErrorExist == false
                                               orderby v.NursePDAInfo.HospitalDemographicInfo.HospitalUnitName
                                               select v);

                    if (unitType != null)
                    {
                        queryableNursePDADetail = queryableNursePDADetail.Where(SetFilterForUnitType(unitType));
                    }
                    if (pharmacyType != null)
                    {
                        queryableNursePDADetail = queryableNursePDADetail.Where(SetFilterForPharmacyType(pharmacyType));
                    }
                    if (configName != null)
                    {
                        queryableNursePDADetail = queryableNursePDADetail.Where(SetFilterForConfigName(configName));
                    }
                    queryableNursePDADetail = queryableNursePDADetail.Where(SetFilterForBedsInUnitTest(optBedInUnitFrom, bedInUnitFrom, optBedInUnitTo, bedInUnitTo));
                    queryableNursePDADetail = queryableNursePDADetail.Where(SetFilterForBudgetedPatientsPerNurseTest(optBudgetedPatientFrom, budgetedPatientFrom, optBudgetedPatientTo, budgetedPatientTo));
                    queryableNursePDADetail = queryableNursePDADetail.Where(SetFilterForElectronicDocumentationTest(optElectronicDocumentFrom, electronicDocumentFrom, optElectronicDocumentTo, electronicDocumentTo));
                    queryableNursePDADetail = queryableNursePDADetail.Where(SetFilterForDocByException(docByException));
                }
                else
                {
                    if (hospitalType == null)
                    {
                        queryableNursePDADetail = (from v in _objectRMCDataContext.NursePDADetails
                                                   where v.NursePDAInfo.HospitalDemographicInfo.StartDate >= Convert.ToDateTime(startDate == null ? Convert.ToString(v.NursePDAInfo.HospitalDemographicInfo.StartDate) : startDate)
                                                   && v.NursePDAInfo.HospitalDemographicInfo.HospitalDemographicID == (hospitalUnitID ?? v.NursePDAInfo.HospitalDemographicInfo.HospitalDemographicID)
                                                   && v.NursePDAInfo.HospitalDemographicInfo.HospitalInfo.State.CountryID == (countryId ?? v.NursePDAInfo.HospitalDemographicInfo.HospitalInfo.State.CountryID) && v.NursePDAInfo.HospitalDemographicInfo.HospitalInfo.StateID == (stateId ?? v.NursePDAInfo.HospitalDemographicInfo.HospitalInfo.StateID)
                                                   && v.NursePDAInfo.HospitalDemographicInfo.IsDeleted != true && v.IsErrorExist == false && v.NursePDAInfo.IsErrorExist == false
                                                   //&& (SqlMethods.Like(dd.Value, "%" + hospitalType + "%") || SqlMethods.Like(dd.Value, "%" + hospitalType + "%"))
                                                   orderby v.NursePDAInfo.HospitalDemographicInfo.HospitalUnitName
                                                   select v);

                        if (unitType != null)
                        {
                            queryableNursePDADetail = queryableNursePDADetail.Where(SetFilterForUnitType(unitType));
                        }
                        if (pharmacyType != null)
                        {
                            queryableNursePDADetail = queryableNursePDADetail.Where(SetFilterForPharmacyType(pharmacyType));
                        }
                        if (configName != null)
                        {
                            queryableNursePDADetail = queryableNursePDADetail.Where(SetFilterForConfigName(configName));
                        }
                        //if (hospitalType != null)
                        //{
                        //    queryableNursePDADetail = queryableNursePDADetail.Where(SetFilterForHospitalType(hospitalType));
                        //}
                        queryableNursePDADetail = queryableNursePDADetail.Where(SetFilterForBedsInUnitTest(optBedInUnitFrom, bedInUnitFrom, optBedInUnitTo, bedInUnitTo));
                        queryableNursePDADetail = queryableNursePDADetail.Where(SetFilterForBudgetedPatientsPerNurseTest(optBudgetedPatientFrom, budgetedPatientFrom, optBudgetedPatientTo, budgetedPatientTo));
                        queryableNursePDADetail = queryableNursePDADetail.Where(SetFilterForElectronicDocumentationTest(optElectronicDocumentFrom, electronicDocumentFrom, optElectronicDocumentTo, electronicDocumentTo));
                        queryableNursePDADetail = queryableNursePDADetail.Where(SetFilterForDocByException(docByException));
                    }
                    else
                    {
                        string[] strHospitalType = hospitalType.Split(new char[] { ',' }, System.StringSplitOptions.RemoveEmptyEntries);
                        int count = strHospitalType.Count();
                        if (count == 1)
                        {
                            queryableNursePDADetail = (from v in _objectRMCDataContext.NursePDADetails
                                                       join dd in _objectRMCDataContext.DynamicDatas on v.NursePDAInfo.HospitalDemographicInfo.HospitalInfoID equals dd.ID
                                                       where dd.ColumnID == 3
                                                       where v.NursePDAInfo.HospitalDemographicInfo.StartDate >= Convert.ToDateTime(startDate == null ? Convert.ToString(v.NursePDAInfo.HospitalDemographicInfo.StartDate) : startDate)
                                                       && v.NursePDAInfo.HospitalDemographicInfo.HospitalDemographicID == (hospitalUnitID ?? v.NursePDAInfo.HospitalDemographicInfo.HospitalDemographicID)
                                                       && v.NursePDAInfo.HospitalDemographicInfo.HospitalInfo.State.CountryID == (countryId ?? v.NursePDAInfo.HospitalDemographicInfo.HospitalInfo.State.CountryID) && v.NursePDAInfo.HospitalDemographicInfo.HospitalInfo.StateID == (stateId ?? v.NursePDAInfo.HospitalDemographicInfo.HospitalInfo.StateID)
                                                       && v.NursePDAInfo.HospitalDemographicInfo.IsDeleted != true && v.IsErrorExist == false && v.NursePDAInfo.IsErrorExist == false
                                                       && SqlMethods.Like(dd.Value, "%" + strHospitalType[0] + "%")
                                                       orderby v.NursePDAInfo.HospitalDemographicInfo.HospitalUnitName
                                                       select v);

                            if (unitType != null)
                            {
                                queryableNursePDADetail = queryableNursePDADetail.Where(SetFilterForUnitType(unitType));
                            }
                            if (pharmacyType != null)
                            {
                                queryableNursePDADetail = queryableNursePDADetail.Where(SetFilterForPharmacyType(pharmacyType));
                            }
                            if (configName != null)
                            {
                                queryableNursePDADetail = queryableNursePDADetail.Where(SetFilterForConfigName(configName));
                            }
                            //if (hospitalType != null)
                            //{
                            //    queryableNursePDADetail = queryableNursePDADetail.Where(SetFilterForHospitalType(hospitalType));
                            //}
                            queryableNursePDADetail = queryableNursePDADetail.Where(SetFilterForBedsInUnitTest(optBedInUnitFrom, bedInUnitFrom, optBedInUnitTo, bedInUnitTo));
                            queryableNursePDADetail = queryableNursePDADetail.Where(SetFilterForBudgetedPatientsPerNurseTest(optBudgetedPatientFrom, budgetedPatientFrom, optBudgetedPatientTo, budgetedPatientTo));
                            queryableNursePDADetail = queryableNursePDADetail.Where(SetFilterForElectronicDocumentationTest(optElectronicDocumentFrom, electronicDocumentFrom, optElectronicDocumentTo, electronicDocumentTo));
                            queryableNursePDADetail = queryableNursePDADetail.Where(SetFilterForDocByException(docByException));
                        }
                        if (count == 2)
                        {
                            queryableNursePDADetail = (from v in _objectRMCDataContext.NursePDADetails
                                                       join dd in _objectRMCDataContext.DynamicDatas on v.NursePDAInfo.HospitalDemographicInfo.HospitalInfoID equals dd.ID
                                                       where dd.ColumnID == 3
                                                       where v.NursePDAInfo.HospitalDemographicInfo.StartDate >= Convert.ToDateTime(startDate == null ? Convert.ToString(v.NursePDAInfo.HospitalDemographicInfo.StartDate) : startDate)
                                                       && v.NursePDAInfo.HospitalDemographicInfo.HospitalDemographicID == (hospitalUnitID ?? v.NursePDAInfo.HospitalDemographicInfo.HospitalDemographicID)
                                                       && v.NursePDAInfo.HospitalDemographicInfo.HospitalInfo.State.CountryID == (countryId ?? v.NursePDAInfo.HospitalDemographicInfo.HospitalInfo.State.CountryID) && v.NursePDAInfo.HospitalDemographicInfo.HospitalInfo.StateID == (stateId ?? v.NursePDAInfo.HospitalDemographicInfo.HospitalInfo.StateID)
                                                       && v.NursePDAInfo.HospitalDemographicInfo.IsDeleted != true && v.IsErrorExist == false && v.NursePDAInfo.IsErrorExist == false
                                                       && (SqlMethods.Like(dd.Value, "%" + strHospitalType[0].ToString() + "%") || SqlMethods.Like(dd.Value, "%" + strHospitalType[1].ToString() + "%"))
                                                       orderby v.NursePDAInfo.HospitalDemographicInfo.HospitalUnitName
                                                       select v);

                            if (unitType != null)
                            {
                                queryableNursePDADetail = queryableNursePDADetail.Where(SetFilterForUnitType(unitType));
                            }
                            if (pharmacyType != null)
                            {
                                queryableNursePDADetail = queryableNursePDADetail.Where(SetFilterForPharmacyType(pharmacyType));
                            }
                            if (configName != null)
                            {
                                queryableNursePDADetail = queryableNursePDADetail.Where(SetFilterForConfigName(configName));
                            }
                            //if (hospitalType != null)
                            //{
                            //    queryableNursePDADetail = queryableNursePDADetail.Where(SetFilterForHospitalType(hospitalType));
                            //}
                            queryableNursePDADetail = queryableNursePDADetail.Where(SetFilterForBedsInUnitTest(optBedInUnitFrom, bedInUnitFrom, optBedInUnitTo, bedInUnitTo));
                            queryableNursePDADetail = queryableNursePDADetail.Where(SetFilterForBudgetedPatientsPerNurseTest(optBudgetedPatientFrom, budgetedPatientFrom, optBudgetedPatientTo, budgetedPatientTo));
                            queryableNursePDADetail = queryableNursePDADetail.Where(SetFilterForElectronicDocumentationTest(optElectronicDocumentFrom, electronicDocumentFrom, optElectronicDocumentTo, electronicDocumentTo));
                            queryableNursePDADetail = queryableNursePDADetail.Where(SetFilterForDocByException(docByException));
                        }
                        if (count == 3)
                        {
                            queryableNursePDADetail = (from v in _objectRMCDataContext.NursePDADetails
                                                       join dd in _objectRMCDataContext.DynamicDatas on v.NursePDAInfo.HospitalDemographicInfo.HospitalInfoID equals dd.ID
                                                       where dd.ColumnID == 3
                                                       where v.NursePDAInfo.HospitalDemographicInfo.StartDate >= Convert.ToDateTime(startDate == null ? Convert.ToString(v.NursePDAInfo.HospitalDemographicInfo.StartDate) : startDate)
                                                       && v.NursePDAInfo.HospitalDemographicInfo.HospitalDemographicID == (hospitalUnitID ?? v.NursePDAInfo.HospitalDemographicInfo.HospitalDemographicID)
                                                       && v.NursePDAInfo.HospitalDemographicInfo.HospitalInfo.State.CountryID == (countryId ?? v.NursePDAInfo.HospitalDemographicInfo.HospitalInfo.State.CountryID) && v.NursePDAInfo.HospitalDemographicInfo.HospitalInfo.StateID == (stateId ?? v.NursePDAInfo.HospitalDemographicInfo.HospitalInfo.StateID)
                                                       && v.NursePDAInfo.HospitalDemographicInfo.IsDeleted != true && v.IsErrorExist == false && v.NursePDAInfo.IsErrorExist == false
                                                       && (SqlMethods.Like(dd.Value, "%" + strHospitalType[0].ToString() + "%") || SqlMethods.Like(dd.Value, "%" + strHospitalType[1].ToString() + "%") || SqlMethods.Like(dd.Value, "%" + strHospitalType[2].ToString() + "%"))
                                                       orderby v.NursePDAInfo.HospitalDemographicInfo.HospitalUnitName
                                                       select v);

                            if (unitType != null)
                            {
                                queryableNursePDADetail = queryableNursePDADetail.Where(SetFilterForUnitType(unitType));
                            }
                            if (pharmacyType != null)
                            {
                                queryableNursePDADetail = queryableNursePDADetail.Where(SetFilterForPharmacyType(pharmacyType));
                            }
                            if (configName != null)
                            {
                                queryableNursePDADetail = queryableNursePDADetail.Where(SetFilterForConfigName(configName));
                            }
                            //if (hospitalType != null)
                            //{
                            //    queryableNursePDADetail = queryableNursePDADetail.Where(SetFilterForHospitalType(hospitalType));
                            //}
                            queryableNursePDADetail = queryableNursePDADetail.Where(SetFilterForBedsInUnitTest(optBedInUnitFrom, bedInUnitFrom, optBedInUnitTo, bedInUnitTo));
                            queryableNursePDADetail = queryableNursePDADetail.Where(SetFilterForBudgetedPatientsPerNurseTest(optBudgetedPatientFrom, budgetedPatientFrom, optBudgetedPatientTo, budgetedPatientTo));
                            queryableNursePDADetail = queryableNursePDADetail.Where(SetFilterForElectronicDocumentationTest(optElectronicDocumentFrom, electronicDocumentFrom, optElectronicDocumentTo, electronicDocumentTo));
                            queryableNursePDADetail = queryableNursePDADetail.Where(SetFilterForDocByException(docByException));
                        }
                        if (count == 4)
                        {
                            queryableNursePDADetail = (from v in _objectRMCDataContext.NursePDADetails
                                                       join dd in _objectRMCDataContext.DynamicDatas on v.NursePDAInfo.HospitalDemographicInfo.HospitalInfoID equals dd.ID
                                                       where dd.ColumnID == 3
                                                       where v.NursePDAInfo.HospitalDemographicInfo.StartDate >= Convert.ToDateTime(startDate == null ? Convert.ToString(v.NursePDAInfo.HospitalDemographicInfo.StartDate) : startDate)
                                                       && v.NursePDAInfo.HospitalDemographicInfo.HospitalDemographicID == (hospitalUnitID ?? v.NursePDAInfo.HospitalDemographicInfo.HospitalDemographicID)
                                                       && v.NursePDAInfo.HospitalDemographicInfo.HospitalInfo.State.CountryID == (countryId ?? v.NursePDAInfo.HospitalDemographicInfo.HospitalInfo.State.CountryID) && v.NursePDAInfo.HospitalDemographicInfo.HospitalInfo.StateID == (stateId ?? v.NursePDAInfo.HospitalDemographicInfo.HospitalInfo.StateID)
                                                       && v.NursePDAInfo.HospitalDemographicInfo.IsDeleted != true && v.IsErrorExist == false && v.NursePDAInfo.IsErrorExist == false
                                                       && (SqlMethods.Like(dd.Value, "%" + strHospitalType[0].ToString() + "%") || SqlMethods.Like(dd.Value, "%" + strHospitalType[1].ToString() + "%") || SqlMethods.Like(dd.Value, "%" + strHospitalType[2].ToString() + "%") || SqlMethods.Like(dd.Value, "%" + strHospitalType[3].ToString() + "%"))
                                                       orderby v.NursePDAInfo.HospitalDemographicInfo.HospitalUnitName
                                                       select v);

                            if (unitType != null)
                            {
                                queryableNursePDADetail = queryableNursePDADetail.Where(SetFilterForUnitType(unitType));
                            }
                            if (pharmacyType != null)
                            {
                                queryableNursePDADetail = queryableNursePDADetail.Where(SetFilterForPharmacyType(pharmacyType));
                            }
                            if (configName != null)
                            {
                                queryableNursePDADetail = queryableNursePDADetail.Where(SetFilterForConfigName(configName));
                            }
                            //if (hospitalType != null)
                            //{
                            //    queryableNursePDADetail = queryableNursePDADetail.Where(SetFilterForHospitalType(hospitalType));
                            //}
                            queryableNursePDADetail = queryableNursePDADetail.Where(SetFilterForBedsInUnitTest(optBedInUnitFrom, bedInUnitFrom, optBedInUnitTo, bedInUnitTo));
                            queryableNursePDADetail = queryableNursePDADetail.Where(SetFilterForBudgetedPatientsPerNurseTest(optBudgetedPatientFrom, budgetedPatientFrom, optBudgetedPatientTo, budgetedPatientTo));
                            queryableNursePDADetail = queryableNursePDADetail.Where(SetFilterForElectronicDocumentationTest(optElectronicDocumentFrom, electronicDocumentFrom, optElectronicDocumentTo, electronicDocumentTo));
                            queryableNursePDADetail = queryableNursePDADetail.Where(SetFilterForDocByException(docByException));
                        }
                        if (count == 5)
                        {
                            queryableNursePDADetail = (from v in _objectRMCDataContext.NursePDADetails
                                                       join dd in _objectRMCDataContext.DynamicDatas on v.NursePDAInfo.HospitalDemographicInfo.HospitalInfoID equals dd.ID
                                                       where dd.ColumnID == 3
                                                       where v.NursePDAInfo.HospitalDemographicInfo.StartDate >= Convert.ToDateTime(startDate == null ? Convert.ToString(v.NursePDAInfo.HospitalDemographicInfo.StartDate) : startDate)
                                                       && v.NursePDAInfo.HospitalDemographicInfo.HospitalDemographicID == (hospitalUnitID ?? v.NursePDAInfo.HospitalDemographicInfo.HospitalDemographicID)
                                                       && v.NursePDAInfo.HospitalDemographicInfo.HospitalInfo.State.CountryID == (countryId ?? v.NursePDAInfo.HospitalDemographicInfo.HospitalInfo.State.CountryID) && v.NursePDAInfo.HospitalDemographicInfo.HospitalInfo.StateID == (stateId ?? v.NursePDAInfo.HospitalDemographicInfo.HospitalInfo.StateID)
                                                       && v.NursePDAInfo.HospitalDemographicInfo.IsDeleted != true && v.IsErrorExist == false && v.NursePDAInfo.IsErrorExist == false
                                                       && (SqlMethods.Like(dd.Value, "%" + strHospitalType[0].ToString() + "%") || SqlMethods.Like(dd.Value, "%" + strHospitalType[1].ToString() + "%") || SqlMethods.Like(dd.Value, "%" + strHospitalType[2].ToString() + "%") || SqlMethods.Like(dd.Value, "%" + strHospitalType[3].ToString() + "%") || SqlMethods.Like(dd.Value, "%" + strHospitalType[4].ToString() + "%"))
                                                       orderby v.NursePDAInfo.HospitalDemographicInfo.HospitalUnitName
                                                       select v);

                            if (unitType != null)
                            {
                                queryableNursePDADetail = queryableNursePDADetail.Where(SetFilterForUnitType(unitType));
                            }
                            if (pharmacyType != null)
                            {
                                queryableNursePDADetail = queryableNursePDADetail.Where(SetFilterForPharmacyType(pharmacyType));
                            }
                            if (configName != null)
                            {
                                queryableNursePDADetail = queryableNursePDADetail.Where(SetFilterForConfigName(configName));
                            }
                            //if (hospitalType != null)
                            //{
                            //    queryableNursePDADetail = queryableNursePDADetail.Where(SetFilterForHospitalType(hospitalType));
                            //}
                            queryableNursePDADetail = queryableNursePDADetail.Where(SetFilterForBedsInUnitTest(optBedInUnitFrom, bedInUnitFrom, optBedInUnitTo, bedInUnitTo));
                            queryableNursePDADetail = queryableNursePDADetail.Where(SetFilterForBudgetedPatientsPerNurseTest(optBudgetedPatientFrom, budgetedPatientFrom, optBudgetedPatientTo, budgetedPatientTo));
                            queryableNursePDADetail = queryableNursePDADetail.Where(SetFilterForElectronicDocumentationTest(optElectronicDocumentFrom, electronicDocumentFrom, optElectronicDocumentTo, electronicDocumentTo));
                            queryableNursePDADetail = queryableNursePDADetail.Where(SetFilterForDocByException(docByException));
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
           

            RMC.BussinessService.BSDataManagement objectBSDataManagement = new RMC.BussinessService.BSDataManagement();

            //Modified the method for year to include with month also
            //_objectRMCDataContext = new RMC.DataService.RMCDataContext();
            
            //------------mukul------------//
            #region "Create List to get all records from Location and LastLocation Table"
            //List<RMC.BusinessEntities.BELocation> GenBELocation = new List<RMC.BusinessEntities.BELocation>();
            //_objectRMCDataContext = new RMC.DataService.RMCDataContext();
            //var Location = (from l in _objectRMCDataContext.Locations
            //                select l);

            //foreach (var l in Location)
            //{
            //    RMC.BusinessEntities.BELocation objectBELocation = new RMC.BusinessEntities.BELocation();
            //    objectBELocation.LocationID = l.LocationID;
            //    objectBELocation.Location = l.Location1;
            //    objectBELocation.ValidateID = l.ValidateID;
            //    objectBELocation.IsActive = l.IsActive;
            //    objectBELocation.RenameLocation = l.RenameLocation;
            //    GenBELocation.Add(objectBELocation);

            //}
            //_objectRMCDataContext.Dispose();

            //List<RMC.BusinessEntities.BELastLocation> GenBELastLocation = new List<RMC.BusinessEntities.BELastLocation>();
            //_objectRMCDataContext = new RMC.DataService.RMCDataContext();
            //var LastLocation = (from ll in _objectRMCDataContext.LastLocations
            //                    select ll);

            //foreach (var ll in LastLocation)
            //{
            //    RMC.BusinessEntities.BELastLocation objectBELastLocation = new RMC.BusinessEntities.BELastLocation();

            //    objectBELastLocation.LastLocationID = ll.LastLocationID;
            //    objectBELastLocation.LastLocation = ll.LastLocation1;
            //    objectBELastLocation.ValidateID = ll.ValidateID;
            //    objectBELastLocation.IsActive = ll.IsActive;
            //    objectBELastLocation.RenameLastLocation = ll.RenameLastLocation;
            //    GenBELastLocation.Add(objectBELastLocation);

            //}
            //_objectRMCDataContext.Dispose();
            #endregion
            //------------------------------//

            objectGeneric = queryableNursePDADetail.Select(v => new RMC.BusinessEntities.BEReportsLocationProfile
            {
                HospitalID = v.NursePDAInfo.HospitalDemographicInfo.HospitalInfo.HospitalInfoID,
                RecordCounter = v.NursePDAInfo.HospitalDemographicInfo.HospitalInfo.RecordCounter,
                LastLocationID = v.LastLocationID,
                LocationID = v.LocationID,
               
                #region "Fatch records(LocationName LastLocationName LocationText)fron the List"


                //LastLocationName=GenBELastLocation.FirstOrDefault(q =>q.Equals(v.LocationID)).ToString(),


                //LocationName = (from l in GenBELocation
                //                where l.LocationID == v.LocationID
                //                select (l.RenameLocation != string.Empty) ? l.RenameLocation : l.Location1).FirstOrDefault(),



                //LastLocationName = (from ll in GenBELastLocation
                //                    where ll.LastLocationID == v.LastLocationID
                //                    select (ll.RenameLastLocation != string.Empty) ? ll.RenameLastLocation : ll.LastLocation).FirstOrDefault(),

                //LocationText = (from ll in GenBELastLocation
                //                where ll.LastLocationID == v.LastLocationID
                //                select (ll.RenameLastLocation != string.Empty) ? ll.RenameLastLocation : ll.LastLocation).FirstOrDefault()


                #endregion

                LocationName = (from l in _objectRMCDataContext.Locations
                                where l.LocationID == v.LocationID
                                select (l.RenameLocation != string.Empty) ? l.RenameLocation : l.Location1).FirstOrDefault(),

                //LocationName = (from l in _objectRMCDataContext.GetLocationName(v.LocationID)
                //                select l).ToList().ToString(),


                LastLocationName = (from ll in _objectRMCDataContext.LastLocations
                                    where ll.LastLocationID == v.LastLocationID
                                    select (ll.RenameLastLocation != string.Empty) ? ll.RenameLastLocation : ll.LastLocation1).FirstOrDefault(),


                HospitalUnitID = v.NursePDAInfo.HospitalDemographicInfo.HospitalDemographicID,
                HospitalUnitName = v.NursePDAInfo.HospitalDemographicInfo.HospitalUnitName,
                CategoryGroupID = 0,

                LocationText = (from ll in _objectRMCDataContext.LastLocations
                                where ll.LastLocationID == v.LastLocationID
                                select (ll.RenameLastLocation != string.Empty) ? ll.RenameLastLocation : ll.LastLocation1).FirstOrDefault()


            }).OrderBy(x => x.RecordCounter).ThenBy(x => x.HospitalUnitID).Where(x => x.LastLocationName != null);
            //.ToList<RMC.BusinessEntities.BEReportsLocationProfile>();
            //List<RMC.BusinessEntities.BEReportsLocationProfile> objectGenericBEValidation;
             objectGenericBEValidation = objectGeneric.ToList<RMC.BusinessEntities.BEReportsLocationProfile>();
            //---for hospital units naming e.g #1-1,#1-2
            List<int> objectGenericHospitalIDs = (from v in objectGenericBEValidation
                                                  orderby v.RecordCounter, v.HospitalUnitID
                                                  select v.HospitalID).Distinct().ToList();

            List<RMC.BusinessEntities.BEValidation> objectTempHospUnitId = null;
            List<RMC.BusinessEntities.BEReportsLocationProfile> objectGenericBEValidationCollection = null;
            List<RMC.BusinessEntities.BEReportsLocationProfile> objectGenericNewBEValidation = new List<RMC.BusinessEntities.BEReportsLocationProfile>();
            objectGenericHospitalIDs.ForEach(delegate(int hospitalID)
            {
                objectGenericBEValidationCollection = objectGenericBEValidation.FindAll(delegate(RMC.BusinessEntities.BEReportsLocationProfile objectBEVal)
                {
                   return objectBEVal.HospitalID == hospitalID;
                });
               /* objectGenericBEValidationCollection = objectGenericBEValidation.ToList().FindAll(delegate(RMC.BusinessEntities.BEReportsLocationProfile objectBEVal)
                {
                    return objectBEVal.HospitalID == hospitalID;
                });*/

                objectTempHospUnitId = (from a in _objectRMCDataContext.HospitalDemographicInfos
                                        where a.HospitalInfoID == hospitalID
                                        select new RMC.BusinessEntities.BEValidation
                                        {
                                            HospitalUnitID = a.HospitalDemographicID
                                        }).OrderBy(x => x.HospitalUnitID).ToList();

                int indexCounter = 0;
                //int hospUnitID = 0;
                objectGenericBEValidationCollection.ForEach(delegate(RMC.BusinessEntities.BEReportsLocationProfile objectBEVal)
                {
                    //if (hospUnitID == 0 || hospUnitID != objectBEVal.HospitalUnitID)
                    //{
                    //    hospUnitID = objectBEVal.HospitalUnitID;
                    //    indexCounter++;
                    //}
                    for (int i = 0; i < objectTempHospUnitId.Count(); i++)
                    {
                        if (objectTempHospUnitId[i].HospitalUnitID == objectBEVal.HospitalUnitID)
                        {
                            indexCounter = i + 1;
                        }
                    }
                    objectBEVal.HospitalUnitIDCounter = "#" + Convert.ToString(objectBEVal.RecordCounter) + "_" + Convert.ToString(indexCounter);
                    objectGenericNewBEValidation.Add(objectBEVal);
                });
            });
            //----------------------------------------

            //var obj1 = objectGenericNewBEValidation.Where(x => x.HospitalUnitIDCounter == "#4_1").Count();

            objectGenericNewBEValidation = objectGenericNewBEValidation.Where(x => x.LastLocationName.Remove(0, 5) != x.LocationName).ToList();
            //objectGenericNewBEValidation = objectGenericNewBEValidation.Where(x => x.LastLocationID != x.LocationID).ToList();

            List<RMC.BusinessEntities.BEReportsLocationProfile> objectGenericBERepLocProfile = (from n in objectGenericNewBEValidation
                                                                                                group n by new { n.LocationName, n.LastLocationName, n.HospitalUnitIDCounter } into g
                                                                                                from t in g.GroupBy(x => x.HospitalUnitIDCounter)
                                                                                                select new RMC.BusinessEntities.BEReportsLocationProfile
                                                                                                {
                                                                                                    LocationName = g.Key.LocationName,
                                                                                                    LastLocationName = g.Key.LastLocationName.Remove(0, 5),
                                                                                                    HospitalUnitIDCounter = g.Key.HospitalUnitIDCounter,
                                                                                                    CountTrip = Convert.ToDouble(string.Format("{0:0.000000}", (double)g.Count() / (double)objectGenericNewBEValidation.Count(x => x.HospitalUnitIDCounter == t.Key))),
                                                                                                    CountTripDisplay = string.Format("{0:0.000000}", (double)g.Count() / (double)objectGenericNewBEValidation.Count(x => x.HospitalUnitIDCounter == t.Key)),
                                                                                                    comboName = g.Key.LastLocationName.Remove(0, 5) + g.Key.LocationName
                                                                                                }).ToList();

          /*IEnumerable<RMC.BusinessEntities.BEReportsLocationProfile> objectGenericBERepLocProfile = (from n in objectGenericNewBEValidation
                                                                                                group n by new { n.LocationName, n.LastLocationName, n.HospitalUnitIDCounter } into g
                                                                                                from t in g.GroupBy(x => x.HospitalUnitIDCounter)
                                                                                                select new RMC.BusinessEntities.BEReportsLocationProfile
                                                                                                {
                                                                                                    LocationName = g.Key.LocationName,
                                                                                                    LastLocationName = g.Key.LastLocationName.Remove(0, 5),
                                                                                                    HospitalUnitIDCounter = g.Key.HospitalUnitIDCounter,
                                                                                                    CountTrip = Convert.ToDouble(string.Format("{0:0.000000}", (double)g.Count() / (double)objectGenericNewBEValidation.Count(x => x.HospitalUnitIDCounter == t.Key))),
                                                                                                    CountTripDisplay = string.Format("{0:0.000000}", (double)g.Count() / (double)objectGenericNewBEValidation.Count(x => x.HospitalUnitIDCounter == t.Key)),
                                                                                                    comboName = g.Key.LastLocationName.Remove(0, 5) + g.Key.LocationName
                                                                                                });*/


            //foreach (string hospitalName in objectGenericBERepLocProfile.Select(s => s.HospitalUnitIDCounter).Distinct().ToList())
            //{
            //    RMC.BusinessEntities.BEReportsLocationProfile objectBERepLocProfile = new RMC.BusinessEntities.BEReportsLocationProfile();
            //    objectBERepLocProfile.LocationName = "Total";
            //    objectBERepLocProfile.LastLocationName = "";
            //    objectBERepLocProfile.LocationText = "zzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzz";
            //    objectBERepLocProfile.HospitalUnitIDCounter = hospitalName;
            //    objectBERepLocProfile.CountTrip = Convert.ToDouble(string.Format("{0:0.0}", (double)objectGenericBERepLocProfile.Where(s => s.LocationName != "Total").Select(s => s.CountTrip).Sum()));
            //    objectBERepLocProfile.CountTripDisplay = string.Format("{0:0.0}", (double)objectGenericBERepLocProfile.Where(s => s.LocationName != "Total").Select(s => s.CountTrip).Sum());
            //    objectGenericBERepLocProfile.Add(objectBERepLocProfile);
            //}

            return objectGenericBERepLocProfile;//.OrderBy(x => x.RecordCounter).ToList();

        }

        /// <summary>
        /// Gets the Location Profile Data for Generating Report which is used by Location Profile Report
        /// </summary>
        public System.Data.DataTable GetDataForLocationProfileReportGrid(int? hospitalUnitID, int? firstYear, int? lastYear, int? firstMonth, int? lastMonth, int? bedInUnitFrom, int optBedInUnitFrom, int? bedInUnitTo, int optBedInUnitTo, float? budgetedPatientFrom, int optBudgetedPatientFrom, float? budgetedPatientTo, int optBudgetedPatientTo, string startDate, string endDate, int? electronicDocumentFrom, int optElectronicDocumentFrom, int? electronicDocumentTo, int optElectronicDocumentTo, int docByException, string unitType, string pharmacyType, string hospitalType, int optHospitalSizeFrom, int? hospitalSizeFrom, int optHospitalSizeTo, int? hospitalSizeTo, int? countryId, int? stateId, string configName)
        {
            List<RMC.BusinessEntities.BEReportsLocationProfile> objectGenericBERepLocProfile = null;
            objectGenericBERepLocProfile = GetDataForLocationProfileReport(hospitalUnitID, firstYear, lastYear, firstMonth, lastMonth, bedInUnitFrom, optBedInUnitFrom, bedInUnitTo, optBedInUnitTo, budgetedPatientFrom, optBudgetedPatientFrom, budgetedPatientTo, optBudgetedPatientTo, startDate, endDate, electronicDocumentFrom, optElectronicDocumentFrom, electronicDocumentTo, optElectronicDocumentTo, docByException, unitType, pharmacyType, hospitalType, optHospitalSizeFrom, hospitalSizeFrom, optHospitalSizeTo, hospitalSizeTo, countryId, stateId, configName);

            System.Data.DataTable dt = null;
            dt = AddRowsLocationProfile(CreateTableLocationProfile(objectGenericBERepLocProfile), objectGenericBERepLocProfile);

            return dt;

        }

        /// <summary>
        /// Use for location names standardization. It updates the location profile in location table
        /// </summary>
        /// <param name="objectLoc">RMC.DataService.Location type parameter</param>
        /// <returns>bool flag</returns>
        public bool UpdateEditLocationRename(RMC.DataService.Location objectLoc, string lastLocation)
        {
            bool flag = false;
            try
            {
                RMC.DataService.Location objectLocation = (from l in _objectRMCDataContext.Locations
                                                           where l.LocationID == objectLoc.LocationID && l.IsActive == true
                                                           select l).FirstOrDefault();

                RMC.DataService.LastLocation objectLastLocation = (from ll in _objectRMCDataContext.LastLocations
                                                                   where ll.LastLocation1 == "Last " + lastLocation && ll.IsActive == true
                                                                   select ll).FirstOrDefault();

                if (objectLocation != null)
                {
                    if (objectLoc.RenameLocation != "Select Location")
                    {
                        objectLocation.LocationID = Convert.ToInt32(objectLoc.LocationID);
                        objectLocation.RenameLocation = objectLoc.RenameLocation;

                        objectLastLocation.RenameLastLocation = "Last " + objectLoc.RenameLocation;
                    }
                    else
                    {
                        objectLocation.LocationID = Convert.ToInt32(objectLoc.LocationID);
                        objectLocation.RenameLocation = string.Empty;

                        objectLastLocation.RenameLastLocation = string.Empty;
                    }

                    _objectRMCDataContext.SubmitChanges();

                    flag = true;
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }

            return flag;
        }

        /// <summary>
        /// Use for last location names standardization. It updates the last location profile in lastlocation table
        /// </summary>
        /// <param name="objectLoc">RMC.DataService.LastLocation type parameter</param>
        /// <returns>bool flag</returns>
        public bool UpdateEditLastLocationRename(RMC.DataService.LastLocation objectLoc)
        {
            bool flag = false;
            try
            {
                RMC.DataService.LastLocation objectLastLocation = (from l in _objectRMCDataContext.LastLocations
                                                                   where l.LastLocationID == objectLoc.LastLocationID && l.IsActive == true
                                                                   select l).FirstOrDefault();

                if (objectLastLocation != null)
                {
                    objectLastLocation.LastLocationID = Convert.ToInt32(objectLoc.LastLocationID);
                    objectLastLocation.RenameLastLocation = objectLoc.RenameLastLocation;
                    _objectRMCDataContext.SubmitChanges();
                    flag = true;
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }

            return flag;
        }


        #endregion

        #region Chart Public Methods

        /// <summary>
        /// Gets data for LineChart used by controlCharts report
        /// </summary>
        public List<RMC.BusinessEntities.BEReports> GetDataForLineChart(string profileCategory, string value, int? valueAddedCategoryID, int? OthersCategoryID, int? LocationCategoryID, int? hospitalUnitID, int? firstYear, int? lastYear, int? firstMonth, int? lastMonth, int? bedInUnit, int optBedInUnit, float? budgetedPatient, int optBudgetedPatient, string startDate, string endDate, int? electronicDocument, int optElectronicDocument, int docByException, string unitType, string pharmacyType, int optHospitalSize, int? hospitalSize)
        {
            try
            {
                List<RMC.BusinessEntities.BEReports> objectGenericListResult = null;
                List<RMC.BusinessEntities.BENationalDatabase> objectGenericListFilter = null;
                List<RMC.BusinessEntities.BENationalDatabase> objectGenericListBENationalDatabase = GetNationalDatabase();
                List<RMC.BusinessEntities.BEReports> objectGenericListBEReport = CalculationForTimeRNSummary(profileCategory.ToLower().Trim(), GetCategoryProfileDataByProfileID(valueAddedCategoryID.Value), SearchHospitalsDataForBenchmark(firstYear, lastYear, firstMonth, lastMonth, hospitalUnitID, bedInUnit, optBedInUnit, budgetedPatient, optBudgetedPatient, startDate, endDate, electronicDocument, optElectronicDocument, docByException, unitType, pharmacyType, optHospitalSize, hospitalSize));

                objectGenericListResult = objectGenericListBEReport.FindAll(delegate(RMC.BusinessEntities.BEReports objectBEReport)
                {
                    return objectBEReport.ColumnName.ToLower().Trim() == value.ToLower().Trim();
                });

                objectGenericListFilter = objectGenericListBENationalDatabase.FindAll(delegate(RMC.BusinessEntities.BENationalDatabase objectBENationalDatabase)
                {
                    if (objectBENationalDatabase.ProfileType != null)
                    {
                        return objectBENationalDatabase.ProfileType.ToLower().Trim() == value.ToLower().Trim();
                    }
                    else
                    {
                        return false;
                    }
                });

                objectGenericListFilter.ForEach(delegate(RMC.BusinessEntities.BENationalDatabase objectBENationalDatabase)
                {
                    for (int index = 0; index < 12; index++)
                    {
                        RMC.BusinessEntities.BEReports objectBEReports = new RMC.BusinessEntities.BEReports();

                        objectBEReports.ColumnName = objectBENationalDatabase.FunctionType;
                        objectBEReports.MonthName = BSCommon.GetMonthName(Convert.ToString(index + 1));
                        objectBEReports.Values = objectBENationalDatabase.ValueText;
                        objectBEReports.ValuesSum = objectBENationalDatabase.Value;

                        objectGenericListResult.Add(objectBEReports);
                    }
                });

                return objectGenericListResult;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Gets data for PieChart used by MonthlyData-PieCharts report
        /// </summary>
        public List<RMC.BusinessEntities.BEReports> GetDataForPieChart(string profileCategory, string value, int? valueAddedCategoryID, int? OthersCategoryID, int? LocationCategoryID, int? hospitalUnitID, int? firstYear, int? lastYear, int? firstMonth, int? lastMonth, int? bedInUnit, int optBedInUnit, float? budgetedPatient, int optBudgetedPatient, string startDate, string endDate, int? electronicDocument, int optElectronicDocument, int docByException, string unitType, string pharmacyType, int optHospitalSize, int? hospitalSize)
        {
            try
            {
                List<RMC.BusinessEntities.BEReports> objectGenericListBEReport = null;
                if (valueAddedCategoryID != null)
                {
                    objectGenericListBEReport = CalculationForTimeRNSummary(profileCategory.ToLower().Trim(), GetCategoryProfileDataByProfileID(valueAddedCategoryID.Value), SearchHospitalsDataForBenchmark(firstYear, lastYear, firstMonth, lastMonth, hospitalUnitID, bedInUnit, optBedInUnit, budgetedPatient, optBudgetedPatient, startDate, endDate, electronicDocument, optElectronicDocument, docByException, unitType, pharmacyType, optHospitalSize, hospitalSize));
                }
                if (OthersCategoryID != null)
                {
                    objectGenericListBEReport = CalculationForTimeRNSummary(profileCategory.ToLower().Trim(), GetCategoryProfileDataByProfileID(OthersCategoryID.Value), SearchHospitalsDataForBenchmark(firstYear, lastYear, firstMonth, lastMonth, hospitalUnitID, bedInUnit, optBedInUnit, budgetedPatient, optBudgetedPatient, startDate, endDate, electronicDocument, optElectronicDocument, docByException, unitType, pharmacyType, optHospitalSize, hospitalSize));
                }
                if (LocationCategoryID != null)
                {
                    objectGenericListBEReport = CalculationForTimeRNSummary(profileCategory.ToLower().Trim(), GetCategoryProfileDataByProfileID(LocationCategoryID.Value), SearchHospitalsDataForBenchmark(firstYear, lastYear, firstMonth, lastMonth, hospitalUnitID, bedInUnit, optBedInUnit, budgetedPatient, optBudgetedPatient, startDate, endDate, electronicDocument, optElectronicDocument, docByException, unitType, pharmacyType, optHospitalSize, hospitalSize));
                }
                return objectGenericListBEReport;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region TableFunctions

        /// <summary>
        /// Creates table for hospital benchmark summary report
        /// </summary>
        public System.Data.DataTable CreateTable(List<RMC.BusinessEntities.BEReports> objectGenericBEReports)
        {
            try
            {
                System.Data.DataTable tbl = new System.Data.DataTable();
                DataColumn dcEmail = new DataColumn("Email", typeof(System.String));
                tbl.Columns.Add(dcEmail);
                DataColumn dcHospitalUnit = new DataColumn("Hospital_Unit", typeof(System.String));
                tbl.Columns.Add(dcHospitalUnit);
                DataColumn dcDataPoint = new DataColumn("Data Point", typeof(System.String));
                tbl.Columns.Add(dcDataPoint);
                foreach (string objectColumn in objectGenericBEReports.Select(s => s.ColumnName).Distinct().ToList())
                {
                    RMC.BusinessEntities.BEReports objectBEReport = objectGenericBEReports.Find(delegate(RMC.BusinessEntities.BEReports objectBERep)
                    {
                        return objectBERep.ColumnName == objectColumn;
                    });

                    DataColumn dc = new DataColumn(objectBEReport.ColumnName, typeof(System.String));
                    tbl.Columns.Add(dc);
                }

                return tbl;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Creates rows of table for hospital benchmark summary report
        /// </summary>
        public System.Data.DataTable AddRows(System.Data.DataTable tbl, List<RMC.BusinessEntities.BEReports> objectGenericBEReports)
        {
            try
            {

                foreach (string hospitalUnitIndex in objectGenericBEReports.OrderBy(x => x.RecordCounter).Select(s => s.RowName).Distinct())
                {
                    List<RMC.BusinessEntities.BEReports> objectListBEReports = objectGenericBEReports.FindAll(delegate(RMC.BusinessEntities.BEReports objectBERep)
                    {
                        return objectBERep.RowName == hospitalUnitIndex;
                    });

                    DataRow dr = tbl.NewRow();
                    dr["Email"] = objectListBEReports[0].Email;
                    dr["Hospital_Unit"] = objectListBEReports[0].RowName;
                    dr["Data Point"] = objectListBEReports[0].DataPoint;
                    foreach (RMC.BusinessEntities.BEReports objectBEReports in objectListBEReports)
                    {
                        dr[objectBEReports.ColumnName] = objectBEReports.Values;
                    }
                    tbl.Rows.Add(dr);
                }

                return tbl;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Creates table for national database used in hospital benchmark summary report and also for monthly summary dashboard
        /// </summary>
        public System.Data.DataTable CreateTableCalculateFunctionValues(List<RMC.BusinessEntities.BEFunctionNames> objectGenericBEReports)
        {
            try
            {
                System.Data.DataTable tbl = new System.Data.DataTable();
                DataColumn dcHospitalUnit = new DataColumn("National_Database", typeof(System.String));
                tbl.Columns.Add(dcHospitalUnit);
                foreach (string objectColumn in objectGenericBEReports.Select(s => s.ColumnName).Distinct().ToList())
                {
                    RMC.BusinessEntities.BEFunctionNames objectBEReport = objectGenericBEReports.Find(delegate(RMC.BusinessEntities.BEFunctionNames objectBERep)
                    {
                        return objectBERep.ColumnName == objectColumn;
                    });

                    DataColumn dc = new DataColumn(objectBEReport.ColumnName, typeof(System.String));
                    tbl.Columns.Add(dc);
                }

                return tbl;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Creates rows of table for National database used in hospital benchmark summary report and also used in monthly summary dashboard
        /// </summary>
        public System.Data.DataTable AddRowsCalculateFunctionValues(System.Data.DataTable tbl, List<RMC.BusinessEntities.BEFunctionNames> objectGenericBEReports)
        {
            try
            {
                foreach (string hospitalUnitIndex in objectGenericBEReports.Select(s => s.FunctionName).Distinct().ToList())
                {
                    List<RMC.BusinessEntities.BEFunctionNames> objectListBEReports = objectGenericBEReports.FindAll(delegate(RMC.BusinessEntities.BEFunctionNames objectBERep)
                    {
                        return objectBERep.FunctionName == hospitalUnitIndex;
                    });

                    DataRow dr = tbl.NewRow();
                    dr["National_Database"] = objectListBEReports[0].FunctionName;
                    foreach (RMC.BusinessEntities.BEFunctionNames objectBEReports in objectListBEReports)
                    {
                        dr[objectBEReports.ColumnName] = objectBEReports.FunctionValueText;
                    }
                    tbl.Rows.Add(dr);
                }

                return tbl;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        //created by cm 19 oct
        //public System.Data.DataTable AddRowsCalculateFunctionValuesNEW(System.Data.DataTable tbl, List<RMC.BusinessEntities.BEFunctionNames> objectGenericBEReports)
        //{
        //    try
        //    {
        //        foreach (string hospitalUnitIndex in objectGenericBEReports.Select(s => s.FunctionName).Distinct().ToList())
        //        {
        //            List<RMC.BusinessEntities.BEReports> objectListBEReports = objectGenericBEReports.FindAll(delegate(RMC.BusinessEntities.BEReports objectBERep)
        //            {
        //                return objectBERep.FunctionName == hospitalUnitIndex;
        //            });

        //            DataRow dr = tbl.NewRow();
        //            dr["National_Database"] = objectListBEReports[0].FunctionName;
        //            foreach (RMC.BusinessEntities.BEReports objectBEReports in objectListBEReports)
        //            {
        //                dr[objectBEReports.ColumnName] = objectBEReports.FunctionValueText;
        //            }
        //            tbl.Rows.Add(dr);
        //        }

        //        return tbl;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}
        //cm
        /// <summary>
        /// Creates table for Monthly Summary Dashboard report
        /// </summary>
        public System.Data.DataTable CreateTableTimeRN(List<RMC.BusinessEntities.BEReports> objectGenericBEReports)
        {
            try
            {
                System.Data.DataTable tbl = new System.Data.DataTable();
                DataColumn dcHospitalUnit = new DataColumn("Month_Year", typeof(System.String));
                tbl.Columns.Add(dcHospitalUnit);
                DataColumn dcDataPoint = new DataColumn("Data Point", typeof(System.String));
                tbl.Columns.Add(dcDataPoint);
                foreach (string objectColumn in objectGenericBEReports.Select(s => s.ColumnName).Distinct().ToList())
                {
                    RMC.BusinessEntities.BEReports objectBEReport = objectGenericBEReports.Find(delegate(RMC.BusinessEntities.BEReports objectBERep)
                    {
                        return objectBERep.ColumnName == objectColumn;
                    });

                    DataColumn dc = new DataColumn(objectBEReport.ColumnName, typeof(System.String));
                    tbl.Columns.Add(dc);
                }

                return tbl;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Creates rows of table for Monthly Summary Dashboard report
        /// </summary>
        public System.Data.DataTable AddRowsTimeRN(System.Data.DataTable tbl, List<RMC.BusinessEntities.BEReports> objectGenericBEReports)
        {
            try
            {
                foreach (string MonthIndex in objectGenericBEReports.Select(s => s.MonthName).Distinct().ToList())
                {
                    List<RMC.BusinessEntities.BEReports> objectListBEReports = objectGenericBEReports.FindAll(delegate(RMC.BusinessEntities.BEReports objectBERep)
                    {
                        return objectBERep.MonthName == MonthIndex;
                    });

                    DataRow dr = tbl.NewRow();
                    dr["Month_Year"] = objectListBEReports[0].MonthName;
                    dr["Data Point"] = objectListBEReports[0].DataPoint;
                    foreach (RMC.BusinessEntities.BEReports objectBEReports in objectListBEReports)
                    {
                        dr[objectBEReports.ColumnName] = objectBEReports.Values;
                    }
                    tbl.Rows.Add(dr);
                }

                return tbl;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Creates table for location profile report
        /// </summary>
        public System.Data.DataTable CreateTableLocationProfile(List<RMC.BusinessEntities.BEReportsLocationProfile> objectGenericBEReportsLocationProfile)
        {
            try
            {
                System.Data.DataTable tbl = new System.Data.DataTable();
                DataColumn dcHospitalUnit = new DataColumn("Previous Location", typeof(System.String));
                tbl.Columns.Add(dcHospitalUnit);
                DataColumn dcDataPoint = new DataColumn("Current Location", typeof(System.String));
                tbl.Columns.Add(dcDataPoint);
                foreach (string objectColumn in objectGenericBEReportsLocationProfile.Select(s => s.HospitalUnitIDCounter).Distinct().ToList())
                {
                    RMC.BusinessEntities.BEReportsLocationProfile objectBEReport = objectGenericBEReportsLocationProfile.Find(delegate(RMC.BusinessEntities.BEReportsLocationProfile objectBERep)
                    {
                        return objectBERep.HospitalUnitIDCounter == objectColumn;
                    });

                    DataColumn dc = new DataColumn(objectBEReport.HospitalUnitIDCounter, typeof(System.String));
                    tbl.Columns.Add(dc);
                }

                return tbl;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Creates rows of table for location profile report
        /// </summary>
        public System.Data.DataTable AddRowsLocationProfile(System.Data.DataTable tbl, List<RMC.BusinessEntities.BEReportsLocationProfile> objectGenericBEReportsLocationProfile)
        {
            try
            {
                //foreach (string location in objectGenericBEReportsLocationProfile.Select(s => new { s.LastLocationName, s.LocationName }).Distinct().ToList())
                foreach (string comboName in objectGenericBEReportsLocationProfile.Select(s => s.comboName).Distinct().ToList())
                {
                    List<RMC.BusinessEntities.BEReportsLocationProfile> objectListBEReports = objectGenericBEReportsLocationProfile.FindAll(delegate(RMC.BusinessEntities.BEReportsLocationProfile objectBERep)
                    {
                        return objectBERep.comboName == comboName;
                    });

                    DataRow dr = tbl.NewRow();
                    dr["Previous Location"] = objectListBEReports[0].LastLocationName;
                    dr["Current Location"] = objectListBEReports[0].LocationName;
                    foreach (RMC.BusinessEntities.BEReportsLocationProfile objectBEReports in objectListBEReports)
                    {
                        dr[objectBEReports.HospitalUnitIDCounter] = objectBEReports.CountTripDisplay;
                    }
                    tbl.Rows.Add(dr);
                }

                return tbl;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Gets distinct ConfigName from NursePDAInfo
        /// </summary>
        /// <returns></returns>
        public List<RMC.BusinessEntities.BEReportsConfigName> GetDistinctCongfigName()
        {
            try
            {
                List<RMC.BusinessEntities.BEReportsConfigName> objectGenericNursePDAInfo = null;

                objectGenericNursePDAInfo = (from n in _objectRMCDataContext.NursePDAInfos
                                             select new RMC.BusinessEntities.BEReportsConfigName
                                             {
                                                 configName = n.ConfigName
                                             }).Distinct().ToList();

                objectGenericNursePDAInfo = objectGenericNursePDAInfo.OrderBy(x => x.configName).ToList();
                return objectGenericNursePDAInfo;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        public List<RMC.BusinessEntities.BEReports> test()
        {
            List<RMC.BusinessEntities.BEReports> objectBE = new List<RMC.BusinessEntities.BEReports>();

            for (int i = 0; i <= 10; i++)
            {
                RMC.BusinessEntities.BEReports objectbe = new RMC.BusinessEntities.BEReports();

                objectbe.ColumnName = i.ToString();
                objectbe.RowName = i.ToString();
                objectbe.MonthName = i.ToString();

                objectBE.Add(objectbe);
            }

            RMC.BusinessEntities.BEReports objectbe1 = new RMC.BusinessEntities.BEReports();

            objectbe1.ColumnName = 1.ToString();
            objectbe1.RowName = 1.ToString();
            objectbe1.MonthName = 1.ToString();

            objectBE.Add(objectbe1);
            return objectBE;
        }

        #region Set/Get NationalDatabase record count

        public Int64 NationalDatabaseRecordCount(int? hospitalUnitID, int? firstYear, int? lastYear, int? firstMonth, int? lastMonth, int? bedInUnitFrom, int optBedInUnitFrom, int? bedInUnitTo, int optBedInUnitTo, float? budgetedPatientFrom, int optBudgetedPatientFrom, float? budgetedPatientTo, int optBudgetedPatientTo, string startDate, string endDate, int? electronicDocumentFrom, int optElectronicDocumentFrom, int? electronicDocumentTo, int optElectronicDocumentTo, int docByException, string unitType, string pharmacyType, string hospitalType, int optHospitalSizeFrom, int? hospitalSizeFrom, int optHospitalSizeTo, int? hospitalSizeTo, int? countryId, int? stateId, string configName,string unitIds)
        {
            IQueryable<RMC.DataService.NursePDADetail> queryableNursePDADetail = null;

            try
            {
                DateTime datTimeFirst, datTimeLast;
                if (firstYear != null && lastYear != null && firstYear.Value > 0 && lastYear.Value > 0)
                {
                    datTimeFirst = Convert.ToDateTime(firstMonth.Value.ToString() + "/01/" + firstYear.Value.ToString());
                    datTimeLast = Convert.ToDateTime(lastMonth.Value.ToString() + "/01/" + lastYear.Value.ToString());

                    queryableNursePDADetail = (from v in _objectRMCDataContext.NursePDADetails
                                               where (Convert.ToDateTime(v.NursePDAInfo.Month + "/01/" + v.NursePDAInfo.Year) >= datTimeFirst && Convert.ToDateTime(v.NursePDAInfo.Month + "/01/" + v.NursePDAInfo.Year) <= datTimeLast)                                                   
                                               && v.NursePDAInfo.HospitalDemographicInfo.HospitalDemographicID == (hospitalUnitID ?? v.NursePDAInfo.HospitalDemographicInfo.HospitalDemographicID)                                                   
                                               && v.NursePDAInfo.HospitalDemographicInfo.HospitalInfo.State.CountryID == (countryId ?? v.NursePDAInfo.HospitalDemographicInfo.HospitalInfo.State.CountryID) && v.NursePDAInfo.HospitalDemographicInfo.HospitalInfo.StateID == (stateId ?? v.NursePDAInfo.HospitalDemographicInfo.HospitalInfo.StateID)
                                               && v.NursePDAInfo.HospitalDemographicInfo.IsDeleted != true && v.IsErrorExist == false && v.NursePDAInfo.IsErrorExist == false
                                               orderby v.NursePDAInfo.HospitalDemographicInfo.HospitalUnitName
                                               select v);

                    if (unitIds != null)
                    {
                        queryableNursePDADetail = queryableNursePDADetail.Where(SetFilterForUnitIds(unitIds));
                    }
                    if (unitType != null)
                    {
                        queryableNursePDADetail = queryableNursePDADetail.Where(SetFilterForUnitType(unitType));
                    }
                    if (pharmacyType != null)
                    {
                        queryableNursePDADetail = queryableNursePDADetail.Where(SetFilterForPharmacyType(pharmacyType));
                    }
                    if (configName != null)
                    {
                        queryableNursePDADetail = queryableNursePDADetail.Where(SetFilterForConfigName(configName));
                    }
                    queryableNursePDADetail = queryableNursePDADetail.Where(SetFilterForBedsInUnitTest(optBedInUnitFrom, bedInUnitFrom, optBedInUnitTo, bedInUnitTo));
                    queryableNursePDADetail = queryableNursePDADetail.Where(SetFilterForBudgetedPatientsPerNurseTest(optBudgetedPatientFrom, budgetedPatientFrom, optBudgetedPatientTo, budgetedPatientTo));
                    queryableNursePDADetail = queryableNursePDADetail.Where(SetFilterForElectronicDocumentationTest(optElectronicDocumentFrom, electronicDocumentFrom, optElectronicDocumentTo, electronicDocumentTo));
                    queryableNursePDADetail = queryableNursePDADetail.Where(SetFilterForDocByException(docByException));
                }
                else
                {     
                    if (hospitalType == null)
                    {
                        queryableNursePDADetail = (from v in _objectRMCDataContext.NursePDADetails
                                                   where v.NursePDAInfo.HospitalDemographicInfo.HospitalDemographicID == (hospitalUnitID ?? v.NursePDAInfo.HospitalDemographicInfo.HospitalDemographicID)
                                                   && v.NursePDAInfo.HospitalDemographicInfo.HospitalInfo.State.CountryID == (countryId ?? v.NursePDAInfo.HospitalDemographicInfo.HospitalInfo.State.CountryID) && v.NursePDAInfo.HospitalDemographicInfo.HospitalInfo.StateID == (stateId ?? v.NursePDAInfo.HospitalDemographicInfo.HospitalInfo.StateID)
                                                   && v.NursePDAInfo.HospitalDemographicInfo.IsDeleted != true && v.IsErrorExist == false && v.NursePDAInfo.IsErrorExist == false
                                                   orderby v.NursePDAInfo.HospitalDemographicInfo.HospitalUnitName
                                                   select v);

                        if (unitIds != null)
                        {
                            queryableNursePDADetail = queryableNursePDADetail.Where(SetFilterForUnitIds(unitIds));
                        }
                        if (unitType != null)
                        {
                            queryableNursePDADetail = queryableNursePDADetail.Where(SetFilterForUnitType(unitType));
                        }
                        if (pharmacyType != null)
                        {
                            queryableNursePDADetail = queryableNursePDADetail.Where(SetFilterForPharmacyType(pharmacyType));
                        }
                        if (configName != null)
                        {
                            queryableNursePDADetail = queryableNursePDADetail.Where(SetFilterForConfigName(configName));
                        }
                        queryableNursePDADetail = queryableNursePDADetail.Where(SetFilterForBedsInUnitTest(optBedInUnitFrom, bedInUnitFrom, optBedInUnitTo, bedInUnitTo));
                        queryableNursePDADetail = queryableNursePDADetail.Where(SetFilterForBudgetedPatientsPerNurseTest(optBudgetedPatientFrom, budgetedPatientFrom, optBudgetedPatientTo, budgetedPatientTo));
                        queryableNursePDADetail = queryableNursePDADetail.Where(SetFilterForElectronicDocumentationTest(optElectronicDocumentFrom, electronicDocumentFrom, optElectronicDocumentTo, electronicDocumentTo));
                        queryableNursePDADetail = queryableNursePDADetail.Where(SetFilterForDocByException(docByException));


                    }
                    else
                    {
                        string[] strHospitalType = hospitalType.Split(new char[] { ',' }, System.StringSplitOptions.RemoveEmptyEntries);
                        int count = strHospitalType.Count();
                        if (count == 1)
                        {
                            queryableNursePDADetail = (from v in _objectRMCDataContext.NursePDADetails
                                                       join dd in _objectRMCDataContext.DynamicDatas on v.NursePDAInfo.HospitalDemographicInfo.HospitalInfoID equals dd.ID
                                                       where dd.ColumnID == 3
                                                       where v.NursePDAInfo.HospitalDemographicInfo.HospitalDemographicID == (hospitalUnitID ?? v.NursePDAInfo.HospitalDemographicInfo.HospitalDemographicID)
                                                       && v.NursePDAInfo.HospitalDemographicInfo.HospitalInfo.State.CountryID == (countryId ?? v.NursePDAInfo.HospitalDemographicInfo.HospitalInfo.State.CountryID) && v.NursePDAInfo.HospitalDemographicInfo.HospitalInfo.StateID == (stateId ?? v.NursePDAInfo.HospitalDemographicInfo.HospitalInfo.StateID)
                                                       && v.NursePDAInfo.HospitalDemographicInfo.IsDeleted != true && v.IsErrorExist == false && v.NursePDAInfo.IsErrorExist == false
                                                       && SqlMethods.Like(dd.Value, "%" + strHospitalType[0] + "%")
                                                       orderby v.NursePDAInfo.HospitalDemographicInfo.HospitalUnitName
                                                       select v);

                            if (unitIds != null)
                            {
                                queryableNursePDADetail = queryableNursePDADetail.Where(SetFilterForUnitIds(unitIds));
                            }
                            if (unitType != null)
                            {
                                queryableNursePDADetail = queryableNursePDADetail.Where(SetFilterForUnitType(unitType));
                            }
                            if (pharmacyType != null)
                            {
                                queryableNursePDADetail = queryableNursePDADetail.Where(SetFilterForPharmacyType(pharmacyType));
                            }
                            if (configName != null)
                            {
                                queryableNursePDADetail = queryableNursePDADetail.Where(SetFilterForConfigName(configName));
                            }
                            queryableNursePDADetail = queryableNursePDADetail.Where(SetFilterForBedsInUnitTest(optBedInUnitFrom, bedInUnitFrom, optBedInUnitTo, bedInUnitTo));
                            queryableNursePDADetail = queryableNursePDADetail.Where(SetFilterForBudgetedPatientsPerNurseTest(optBudgetedPatientFrom, budgetedPatientFrom, optBudgetedPatientTo, budgetedPatientTo));
                            queryableNursePDADetail = queryableNursePDADetail.Where(SetFilterForElectronicDocumentationTest(optElectronicDocumentFrom, electronicDocumentFrom, optElectronicDocumentTo, electronicDocumentTo));
                            queryableNursePDADetail = queryableNursePDADetail.Where(SetFilterForDocByException(docByException));
                        }
                        if (count == 2)
                        {
                            queryableNursePDADetail = (from v in _objectRMCDataContext.NursePDADetails
                                                       join dd in _objectRMCDataContext.DynamicDatas on v.NursePDAInfo.HospitalDemographicInfo.HospitalInfoID equals dd.ID
                                                       where dd.ColumnID == 3
                                                       where v.NursePDAInfo.HospitalDemographicInfo.HospitalDemographicID == (hospitalUnitID ?? v.NursePDAInfo.HospitalDemographicInfo.HospitalDemographicID)
                                                       && v.NursePDAInfo.HospitalDemographicInfo.HospitalInfo.State.CountryID == (countryId ?? v.NursePDAInfo.HospitalDemographicInfo.HospitalInfo.State.CountryID) && v.NursePDAInfo.HospitalDemographicInfo.HospitalInfo.StateID == (stateId ?? v.NursePDAInfo.HospitalDemographicInfo.HospitalInfo.StateID)
                                                       && v.NursePDAInfo.HospitalDemographicInfo.IsDeleted != true && v.IsErrorExist == false && v.NursePDAInfo.IsErrorExist == false
                                                       && (SqlMethods.Like(dd.Value, "%" + strHospitalType[0].ToString() + "%") || SqlMethods.Like(dd.Value, "%" + strHospitalType[1].ToString() + "%"))
                                                       orderby v.NursePDAInfo.HospitalDemographicInfo.HospitalUnitName
                                                       select v);

                            if (unitIds != null)
                            {
                                queryableNursePDADetail = queryableNursePDADetail.Where(SetFilterForUnitIds(unitIds));
                            }
                            if (unitType != null)
                            {
                                queryableNursePDADetail = queryableNursePDADetail.Where(SetFilterForUnitType(unitType));
                            }
                            if (pharmacyType != null)
                            {
                                queryableNursePDADetail = queryableNursePDADetail.Where(SetFilterForPharmacyType(pharmacyType));
                            }
                            if (configName != null)
                            {
                                queryableNursePDADetail = queryableNursePDADetail.Where(SetFilterForConfigName(configName));
                            }
                            queryableNursePDADetail = queryableNursePDADetail.Where(SetFilterForBedsInUnitTest(optBedInUnitFrom, bedInUnitFrom, optBedInUnitTo, bedInUnitTo));
                            queryableNursePDADetail = queryableNursePDADetail.Where(SetFilterForBudgetedPatientsPerNurseTest(optBudgetedPatientFrom, budgetedPatientFrom, optBudgetedPatientTo, budgetedPatientTo));
                            queryableNursePDADetail = queryableNursePDADetail.Where(SetFilterForElectronicDocumentationTest(optElectronicDocumentFrom, electronicDocumentFrom, optElectronicDocumentTo, electronicDocumentTo));
                            queryableNursePDADetail = queryableNursePDADetail.Where(SetFilterForDocByException(docByException));
                        }
                        if (count == 3)
                        {
                            queryableNursePDADetail = (from v in _objectRMCDataContext.NursePDADetails
                                                       join dd in _objectRMCDataContext.DynamicDatas on v.NursePDAInfo.HospitalDemographicInfo.HospitalInfoID equals dd.ID
                                                       where dd.ColumnID == 3
                                                       where v.NursePDAInfo.HospitalDemographicInfo.HospitalDemographicID == (hospitalUnitID ?? v.NursePDAInfo.HospitalDemographicInfo.HospitalDemographicID)
                                                       && v.NursePDAInfo.HospitalDemographicInfo.HospitalInfo.State.CountryID == (countryId ?? v.NursePDAInfo.HospitalDemographicInfo.HospitalInfo.State.CountryID) && v.NursePDAInfo.HospitalDemographicInfo.HospitalInfo.StateID == (stateId ?? v.NursePDAInfo.HospitalDemographicInfo.HospitalInfo.StateID)
                                                       && v.NursePDAInfo.HospitalDemographicInfo.IsDeleted != true && v.IsErrorExist == false && v.NursePDAInfo.IsErrorExist == false
                                                       && (SqlMethods.Like(dd.Value, "%" + strHospitalType[0].ToString() + "%") || SqlMethods.Like(dd.Value, "%" + strHospitalType[1].ToString() + "%") || SqlMethods.Like(dd.Value, "%" + strHospitalType[2].ToString() + "%"))
                                                       orderby v.NursePDAInfo.HospitalDemographicInfo.HospitalUnitName
                                                       select v);

                            if (unitIds != null)
                            {
                                queryableNursePDADetail = queryableNursePDADetail.Where(SetFilterForUnitIds(unitIds));
                            }
                            if (unitType != null)
                            {
                                queryableNursePDADetail = queryableNursePDADetail.Where(SetFilterForUnitType(unitType));
                            }
                            if (pharmacyType != null)
                            {
                                queryableNursePDADetail = queryableNursePDADetail.Where(SetFilterForPharmacyType(pharmacyType));
                            }
                            if (configName != null)
                            {
                                queryableNursePDADetail = queryableNursePDADetail.Where(SetFilterForConfigName(configName));
                            }
                            queryableNursePDADetail = queryableNursePDADetail.Where(SetFilterForBedsInUnitTest(optBedInUnitFrom, bedInUnitFrom, optBedInUnitTo, bedInUnitTo));
                            queryableNursePDADetail = queryableNursePDADetail.Where(SetFilterForBudgetedPatientsPerNurseTest(optBudgetedPatientFrom, budgetedPatientFrom, optBudgetedPatientTo, budgetedPatientTo));
                            queryableNursePDADetail = queryableNursePDADetail.Where(SetFilterForElectronicDocumentationTest(optElectronicDocumentFrom, electronicDocumentFrom, optElectronicDocumentTo, electronicDocumentTo));
                            queryableNursePDADetail = queryableNursePDADetail.Where(SetFilterForDocByException(docByException));
                        }
                        if (count == 4)
                        {
                            queryableNursePDADetail = (from v in _objectRMCDataContext.NursePDADetails
                                                       join dd in _objectRMCDataContext.DynamicDatas on v.NursePDAInfo.HospitalDemographicInfo.HospitalInfoID equals dd.ID
                                                       where dd.ColumnID == 3
                                                       where v.NursePDAInfo.HospitalDemographicInfo.HospitalDemographicID == (hospitalUnitID ?? v.NursePDAInfo.HospitalDemographicInfo.HospitalDemographicID)
                                                       && v.NursePDAInfo.HospitalDemographicInfo.HospitalInfo.State.CountryID == (countryId ?? v.NursePDAInfo.HospitalDemographicInfo.HospitalInfo.State.CountryID) && v.NursePDAInfo.HospitalDemographicInfo.HospitalInfo.StateID == (stateId ?? v.NursePDAInfo.HospitalDemographicInfo.HospitalInfo.StateID)
                                                       && v.NursePDAInfo.HospitalDemographicInfo.IsDeleted != true && v.IsErrorExist == false && v.NursePDAInfo.IsErrorExist == false
                                                       && (SqlMethods.Like(dd.Value, "%" + strHospitalType[0].ToString() + "%") || SqlMethods.Like(dd.Value, "%" + strHospitalType[1].ToString() + "%") || SqlMethods.Like(dd.Value, "%" + strHospitalType[2].ToString() + "%") || SqlMethods.Like(dd.Value, "%" + strHospitalType[3].ToString() + "%"))
                                                       orderby v.NursePDAInfo.HospitalDemographicInfo.HospitalUnitName
                                                       select v);

                            if (unitIds != null)
                            {
                                queryableNursePDADetail = queryableNursePDADetail.Where(SetFilterForUnitIds(unitIds));
                            }
                            if (unitType != null)
                            {
                                queryableNursePDADetail = queryableNursePDADetail.Where(SetFilterForUnitType(unitType));
                            }
                            if (pharmacyType != null)
                            {
                                queryableNursePDADetail = queryableNursePDADetail.Where(SetFilterForPharmacyType(pharmacyType));
                            }
                            if (configName != null)
                            {
                                queryableNursePDADetail = queryableNursePDADetail.Where(SetFilterForConfigName(configName));
                            }
                            queryableNursePDADetail = queryableNursePDADetail.Where(SetFilterForBedsInUnitTest(optBedInUnitFrom, bedInUnitFrom, optBedInUnitTo, bedInUnitTo));
                            queryableNursePDADetail = queryableNursePDADetail.Where(SetFilterForBudgetedPatientsPerNurseTest(optBudgetedPatientFrom, budgetedPatientFrom, optBudgetedPatientTo, budgetedPatientTo));
                            queryableNursePDADetail = queryableNursePDADetail.Where(SetFilterForElectronicDocumentationTest(optElectronicDocumentFrom, electronicDocumentFrom, optElectronicDocumentTo, electronicDocumentTo));
                            queryableNursePDADetail = queryableNursePDADetail.Where(SetFilterForDocByException(docByException));
                        }
                        if (count == 5)
                        {
                            queryableNursePDADetail = (from v in _objectRMCDataContext.NursePDADetails
                                                       join dd in _objectRMCDataContext.DynamicDatas on v.NursePDAInfo.HospitalDemographicInfo.HospitalInfoID equals dd.ID
                                                       where dd.ColumnID == 3
                                                       where v.NursePDAInfo.HospitalDemographicInfo.HospitalDemographicID == (hospitalUnitID ?? v.NursePDAInfo.HospitalDemographicInfo.HospitalDemographicID)
                                                       && v.NursePDAInfo.HospitalDemographicInfo.HospitalInfo.State.CountryID == (countryId ?? v.NursePDAInfo.HospitalDemographicInfo.HospitalInfo.State.CountryID) && v.NursePDAInfo.HospitalDemographicInfo.HospitalInfo.StateID == (stateId ?? v.NursePDAInfo.HospitalDemographicInfo.HospitalInfo.StateID)
                                                       && v.NursePDAInfo.HospitalDemographicInfo.IsDeleted != true && v.IsErrorExist == false && v.NursePDAInfo.IsErrorExist == false
                                                       && (SqlMethods.Like(dd.Value, "%" + strHospitalType[0].ToString() + "%") || SqlMethods.Like(dd.Value, "%" + strHospitalType[1].ToString() + "%") || SqlMethods.Like(dd.Value, "%" + strHospitalType[2].ToString() + "%") || SqlMethods.Like(dd.Value, "%" + strHospitalType[3].ToString() + "%") || SqlMethods.Like(dd.Value, "%" + strHospitalType[4].ToString() + "%"))
                                                       orderby v.NursePDAInfo.HospitalDemographicInfo.HospitalUnitName
                                                       select v);

                            if (unitIds != null)
                            {
                                queryableNursePDADetail = queryableNursePDADetail.Where(SetFilterForUnitIds(unitIds));
                            }
                            if (unitType != null)
                            {
                                queryableNursePDADetail = queryableNursePDADetail.Where(SetFilterForUnitType(unitType));
                            }
                            if (pharmacyType != null)
                            {
                                queryableNursePDADetail = queryableNursePDADetail.Where(SetFilterForPharmacyType(pharmacyType));
                            }
                            if (configName != null)
                            {
                                queryableNursePDADetail = queryableNursePDADetail.Where(SetFilterForConfigName(configName));
                            }
                            queryableNursePDADetail = queryableNursePDADetail.Where(SetFilterForBedsInUnitTest(optBedInUnitFrom, bedInUnitFrom, optBedInUnitTo, bedInUnitTo));
                            queryableNursePDADetail = queryableNursePDADetail.Where(SetFilterForBudgetedPatientsPerNurseTest(optBudgetedPatientFrom, budgetedPatientFrom, optBudgetedPatientTo, budgetedPatientTo));
                            queryableNursePDADetail = queryableNursePDADetail.Where(SetFilterForElectronicDocumentationTest(optElectronicDocumentFrom, electronicDocumentFrom, optElectronicDocumentTo, electronicDocumentTo));
                            queryableNursePDADetail = queryableNursePDADetail.Where(SetFilterForDocByException(docByException));
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return queryableNursePDADetail.Count();
            //RMC.BussinessService.BSDataManagement objectBSDataManagement = new RMC.BussinessService.BSDataManagement();
            
            //objectGenericBEValidation = queryableNursePDADetail.Select(v => new RMC.BusinessEntities.BEValidation
            //{
            //    CreatedBy = v.NursePDAInfo.HospitalDemographicInfo.CreatedBy,
            //    HospitalID = v.NursePDAInfo.HospitalDemographicInfo.HospitalInfo.HospitalInfoID,
            //    HospitalUnitID = v.NursePDAInfo.HospitalDemographicInfo.HospitalDemographicID,
            //    RecordCounter = v.NursePDAInfo.HospitalDemographicInfo.HospitalInfo.RecordCounter,
            //    LastLocationID = v.LastLocationID,
            //    CognitiveCategory = v.CognitiveCategory,
            //    ResourceRequirementID = v.ResourceRequirementID,
            //    ActivityID = v.ActivityID,
            //    LocationID = v.LocationID,
            //    SubActivityID = v.SubActivityID,
            //    HospitalUnitName = v.NursePDAInfo.HospitalDemographicInfo.HospitalUnitName,
            //    CategoryGroupID = 0,
            //    Year = v.NursePDAInfo.Year,
            //    Month = v.NursePDAInfo.Month,
            //    MonthIndex = Convert.ToInt32(v.NursePDAInfo.Month),
            //    HospitalUnitIDCounter = "#" + v.NursePDAInfo.HospitalDemographicInfo.HospitalInfo.RecordCounter + "_",
            //    HospitalSize = (_objectRMCDataContext.DynamicDatas
            //                  .Where(w => w.ColumnName.TableName.ToLower().Trim() == "HospitalInfo".ToLower() && w.ColumnName.ColumnName1.ToLower().Trim() == "BedsInHospital".ToLower() && w.ID == v.NursePDAInfo.HospitalDemographicInfo.HospitalInfo.HospitalInfoID)
            //                  .Select(s => Convert.ToInt32(s.Value)).FirstOrDefault())
            //}).OrderBy(x => x.RecordCounter).ThenBy(x => x.HospitalUnitID).ToList<RMC.BusinessEntities.BEValidation>();

            //List<int> objectGenericHospitalIDs = (from v in objectGenericBEValidation
            //                                      orderby v.HospitalID
            //                                      select v.HospitalID).Distinct().ToList();

            //List<RMC.BusinessEntities.BEHospitalUnitInfo> objectGenericListHospitalUnitInfo = (from hu in _objectRMCDataContext.HospitalDemographicInfos
            //                                                                                   where objectGenericHospitalIDs.Contains(Convert.ToInt32(hu.HospitalInfoID))
            //                                                                                   orderby hu.HospitalInfoID, hu.HospitalDemographicID
            //                                                                                   select new RMC.BusinessEntities.BEHospitalUnitInfo
            //                                                                                   {
            //                                                                                       HospitalID = hu.HospitalInfoID.Value,
            //                                                                                       HospitalUnitID = hu.HospitalDemographicID,
            //                                                                                       HospitalUnitSequence = 0
            //                                                                                   }).ToList();
        }
        
        #endregion

        public bool CheckProfileCount(string profileType, string profile)
        {
            bool result = false;
            bool isProfileTypeNotExist = true;
            if (MaintainSessions.SessionProfiles != null && MaintainSessions.SessionProfiles != string.Empty)
            {
                string[] profiles = MaintainSessions.SessionProfiles.Split('^');

                foreach (string strProfile in profiles)
                {
                    string[] profileId = strProfile.Split(':');

                    if (profileId[0] == profileType)
                    {
                        isProfileTypeNotExist = false;
                        if (Convert.ToInt32(profileId[1]) != Convert.ToInt32(profile))
                        {
                            result = true;
                            MaintainSessions.SessionProfiles += "^" + profileType + ":" + profile;
                        }
                    }
                }
            }
            else
            {
                MaintainSessions.SessionProfiles = profileType + ":" + profile;
            }

            if (isProfileTypeNotExist == true)
            {
                if (MaintainSessions.SessionProfiles.Length > 0)
                {
                    MaintainSessions.SessionProfiles += "^" + profileType + ":" + profile;
                }
                else
                {
                    MaintainSessions.SessionProfiles = profileType + ":" + profile;
                }
            }
            return result || isProfileTypeNotExist;
        }

        public bool removeProfileCount(string profileType)
        {
            bool result = false;

            if (MaintainSessions.SessionProfiles != null && MaintainSessions.SessionProfiles != string.Empty)
            {
                string profileString = string.Empty;
                string[] profiles = MaintainSessions.SessionProfiles.Split('^');
                foreach (string strProfile in profiles)
                {
                    string[] profileId = strProfile.Split(':');

                    if (profileId[0] == profileType)
                    {
                        result = true;
                    }
                    else
                    {
                        if (profileString == string.Empty)
                        {
                            profileString = profileId[0] + ":" + profileId[1];
                        }
                        else
                        {
                            profileString += "^" + profileId[0] + ":" + profileId[1];
                        }
                    }
                }

                if (result == true)
                {
                    MaintainSessions.SessionProfiles = profileString;
                }
            }

            return result;
        }


        public string removeIncDecSymbol<T>(string columnName, T obj)
        {
            int indexOfSymbol = 0;
            Type t = typeof(T);
            System.Reflection.PropertyInfo propInfo = t.GetProperty(columnName);
            string columnValue = Convert.ToString(propInfo.GetValue(obj, null));
            indexOfSymbol = columnValue.IndexOf("(+)");
            if (indexOfSymbol == -1)
            {
                indexOfSymbol = columnValue.IndexOf("(-)");
            }
            //commented by cm
            //if (indexOfSymbol > -1)
            //{
            //    int columnValueLength = columnValue.Length;
            //    int count = columnValueLength - indexOfSymbol;
            //    int removingCharacters = 3;
            //    if (count > 3)
            //    {
            //        removingCharacters = 4;
            //    }
            //    columnValue = columnValue.Remove(indexOfSymbol, removingCharacters);
            //}

            return columnValue;
        }

        public void removeIncDecSymbol<T>(string columnName, ref List<T> genericList)
        {
            Type t = typeof(T);
            System.Reflection.PropertyInfo propInfo = t.GetProperty(columnName);
            genericList.ForEach(delegate(T obj)
            {
                int indexOfSymbol = 0;
                string columnValue = Convert.ToString(propInfo.GetValue(obj, null));
                indexOfSymbol = columnValue.IndexOf("(+)");
                if (indexOfSymbol == -1)
                {
                    indexOfSymbol = columnValue.IndexOf("(-)");
                }
                //commented by cm
                //if (indexOfSymbol > -1)
                //{
                //    int columnValueLength = columnValue.Length;
                //    int count = columnValueLength - indexOfSymbol;
                //    int removingCharacters = 3;
                //    if (count > 3)
                //    {
                //        removingCharacters = 4;
                //    }
                //    columnValue = columnValue.Remove(indexOfSymbol, removingCharacters);
                //    propInfo.SetValue(obj, columnValue, null);
                //}
            });
        }
    }

    /// <summary>
    /// This class is used for applying or, and, like operators in Linq query
    /// </summary>
    public static class PredicateBuilder
    {
        public static Expression<Func<T, bool>> True<T>() { return f => true; }
        public static Expression<Func<T, bool>> False<T>() { return f => false; }

        public static Expression<Func<T, bool>> Or<T>(this Expression<Func<T, bool>> expr1,
                                                            Expression<Func<T, bool>> expr2)
        {
            var invokedExpr = Expression.Invoke(expr2, expr1.Parameters.Cast<Expression>());
            return Expression.Lambda<Func<T, bool>>
                  (Expression.OrElse(expr1.Body, invokedExpr), expr1.Parameters);
        }

        public static Expression<Func<T, bool>> And<T>(this Expression<Func<T, bool>> expr1,
                                                             Expression<Func<T, bool>> expr2)
        {
            var invokedExpr = Expression.Invoke(expr2, expr1.Parameters.Cast<Expression>());
            return Expression.Lambda<Func<T, bool>>
                  (Expression.AndAlso(expr1.Body, invokedExpr), expr1.Parameters);
        }        
    }
}
