using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RMC.BussinessService
{
    public partial class BSNationalDatabase
    {
        #region Valriable
        RMC.DataService.RMCDataContext _objectRMCDataContext = null;
        #endregion

        #region public Methods

        /// <summary>
        /// Get Data Whose Values Not In National Database.
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        public List<RMC.BusinessEntities.BENationalDatabase> GetNationalDatabase()
        {
            try
            {
                _objectRMCDataContext = new RMC.DataService.RMCDataContext();
                List<RMC.BusinessEntities.BENationalDatabase> objectGenericBENationalDatabase = new List<RMC.BusinessEntities.BENationalDatabase>();
                RMC.BusinessEntities.BENationalDatabase objectBENationalDatabase = null;

                List<RMC.DataService.ValueAddedType> objectGenericValueAddedType = null;
                objectGenericValueAddedType = _objectRMCDataContext.ValueAddedTypes.ToList();

                List<RMC.DataService.CategoryGroup> objectGenericCategoryGroup = null;
                objectGenericCategoryGroup = _objectRMCDataContext.CategoryGroups.ToList();

                List<RMC.DataService.LocationCategory> objectGenericLocationCategory = null;
                objectGenericLocationCategory = _objectRMCDataContext.LocationCategories.ToList();

                List<RMC.DataService.NationalDatabase> objectGenericNationalDatabase = null;
                objectGenericNationalDatabase = _objectRMCDataContext.NationalDatabases.ToList();

                List<RMC.DataService.NationalDatabaseCategory> objectGenericNationalDatabaseCategory = null;
                objectGenericNationalDatabaseCategory = _objectRMCDataContext.NationalDatabaseCategories.ToList();

                objectGenericNationalDatabaseCategory.ForEach(delegate(RMC.DataService.NationalDatabaseCategory objectNationalDatabaseCategory)
                {
                    objectGenericValueAddedType.ForEach(delegate(RMC.DataService.ValueAddedType objectValueAddedType)
                    {
                        if (!objectGenericNationalDatabase.Exists(delegate(RMC.DataService.NationalDatabase objectNationalDatabase)
                        {
                            return objectNationalDatabase.NationalDatabaseCategoryID == objectNationalDatabaseCategory.NationalDatabaseCategoryID && objectNationalDatabase.Type.ToLower().Trim() == "value added" && objectNationalDatabase.TypeValueID == objectValueAddedType.TypeID;
                        }))
                        {
                            objectBENationalDatabase = new RMC.BusinessEntities.BENationalDatabase();

                            objectBENationalDatabase.GroupSequenceName = objectValueAddedType.TypeName;
                            objectBENationalDatabase.FunctionTypeId = objectNationalDatabaseCategory.NationalDatabaseCategoryID;
                            objectBENationalDatabase.FunctionType = objectNationalDatabaseCategory.NationalDatabaseCategoryName;
                            objectBENationalDatabase.ProfileType = "Value Added";
                            objectBENationalDatabase.GroupSequence = objectValueAddedType.TypeID;
                            objectBENationalDatabase.ValueText = "%";
                            objectBENationalDatabase.Value = 0;

                            objectGenericBENationalDatabase.Add(objectBENationalDatabase);
                        }
                    });

                    objectGenericCategoryGroup.ForEach(delegate(RMC.DataService.CategoryGroup objectCategoryGroup)
                    {
                        if (!objectGenericNationalDatabase.Exists(delegate(RMC.DataService.NationalDatabase objectNationalDatabase)
                        {
                            return objectNationalDatabase.NationalDatabaseCategoryID == objectNationalDatabaseCategory.NationalDatabaseCategoryID && objectNationalDatabase.Type.ToLower().Trim() == "others" && objectNationalDatabase.TypeValueID == objectCategoryGroup.CategoryGroupID;
                        }))
                        {
                            objectBENationalDatabase = new RMC.BusinessEntities.BENationalDatabase();

                            objectBENationalDatabase.GroupSequenceName = objectCategoryGroup.CategoryGroup1;
                            objectBENationalDatabase.FunctionTypeId = objectNationalDatabaseCategory.NationalDatabaseCategoryID;
                            objectBENationalDatabase.FunctionType = objectNationalDatabaseCategory.NationalDatabaseCategoryName;
                            objectBENationalDatabase.ProfileType = "Others";
                            objectBENationalDatabase.GroupSequence = objectCategoryGroup.CategoryGroupID;
                            objectBENationalDatabase.ValueText = "%";
                            objectBENationalDatabase.Value = 0;

                            objectGenericBENationalDatabase.Add(objectBENationalDatabase);
                        }
                    });

                    objectGenericLocationCategory.ForEach(delegate(RMC.DataService.LocationCategory objectLocationCategory)
                    {
                        if (!objectGenericNationalDatabase.Exists(delegate(RMC.DataService.NationalDatabase objectNationalDatabase)
                        {
                            return objectNationalDatabase.NationalDatabaseCategoryID == objectNationalDatabaseCategory.NationalDatabaseCategoryID && objectNationalDatabase.Type.ToLower().Trim() == "location" && objectNationalDatabase.TypeValueID == objectLocationCategory.LocationID;
                        }))
                        {
                            objectBENationalDatabase = new RMC.BusinessEntities.BENationalDatabase();

                            objectBENationalDatabase.GroupSequenceName = objectLocationCategory.LocationCategory1;
                            objectBENationalDatabase.FunctionTypeId = objectNationalDatabaseCategory.NationalDatabaseCategoryID;
                            objectBENationalDatabase.FunctionType = objectNationalDatabaseCategory.NationalDatabaseCategoryName;
                            objectBENationalDatabase.ProfileType = "Location";
                            objectBENationalDatabase.GroupSequence = objectLocationCategory.LocationID;
                            objectBENationalDatabase.ValueText = "%";
                            objectBENationalDatabase.Value = 0;

                            objectGenericBENationalDatabase.Add(objectBENationalDatabase);
                        }
                    }
                    );
                });

                return objectGenericBENationalDatabase;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Get National Database Records For Updating Values.
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        public List<RMC.BusinessEntities.BENationalDatabase> GetNationalDatabaseForUpdate()
        {
            try
            {
                _objectRMCDataContext = new RMC.DataService.RMCDataContext();
                List<RMC.BusinessEntities.BENationalDatabase> objectGenericBENationalDatabase = new List<RMC.BusinessEntities.BENationalDatabase>();
                List<RMC.DataService.ValueAddedType> objectGenericValueAddedType = new List<RMC.DataService.ValueAddedType>();
                objectGenericValueAddedType = _objectRMCDataContext.ValueAddedTypes.ToList();


                objectGenericBENationalDatabase = (from nd in _objectRMCDataContext.NationalDatabases
                                                   select new RMC.BusinessEntities.BENationalDatabase
                                                   {
                                                       Id = nd.NationalDatabaseID,
                                                       FunctionTypeId = nd.NationalDatabaseCategoryID,
                                                       FunctionType = nd.NationalDatabaseCategory.NationalDatabaseCategoryName,
                                                       ProfileType = nd.Type,
                                                       GroupSequenceName = (nd.Type.ToLower() == "value added") ? (from pt in _objectRMCDataContext.ValueAddedTypes
                                                                                                                   where pt.TypeID == nd.TypeValueID
                                                                                                                   select pt).FirstOrDefault().TypeName :
                                                                              (nd.Type.ToLower() == "others") ? (from pt in _objectRMCDataContext.CategoryGroups
                                                                                                                 where pt.CategoryGroupID == nd.TypeValueID
                                                                                                                 select pt).FirstOrDefault().CategoryGroup1 : (from pt in _objectRMCDataContext.LocationCategories
                                                                                                                                                               where pt.LocationID == nd.TypeValueID
                                                                                                                                                               select pt).FirstOrDefault().LocationCategory1,
                                                       ValueText = nd.ValueText,
                                                       Value = Convert.ToDouble(nd.Value)
                                                   }).ToList();
                
                return objectGenericBENationalDatabase;



            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Updating Data to National Database.
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        public bool UpdateBulkNationalDatabase(List<RMC.DataService.NationalDatabase> objectGenericNationalDatabase)
        {
            bool flag = false;
            try
            {
                foreach (RMC.DataService.NationalDatabase objectNationalDatabase in objectGenericNationalDatabase)
                {
                    _objectRMCDataContext = new RMC.DataService.RMCDataContext();
                    RMC.DataService.NationalDatabase objectNewNationalDatabase = new RMC.DataService.NationalDatabase();
                    objectNewNationalDatabase = _objectRMCDataContext.NationalDatabases.Single(n => n.NationalDatabaseID == objectNationalDatabase.NationalDatabaseID);
                    objectNewNationalDatabase.Value = objectNationalDatabase.Value;
                    _objectRMCDataContext.SubmitChanges();
                }

                flag = true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return flag;
        }

        /// <summary>
        /// Save Data to National Database.
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        public bool InsertBulkNationalDatabase(List<RMC.DataService.NationalDatabase> objectGenericNationalDatabase)
        {
            bool flag = false;
            try
            {
                _objectRMCDataContext = new RMC.DataService.RMCDataContext();

                _objectRMCDataContext.NationalDatabases.InsertAllOnSubmit(objectGenericNationalDatabase);
                _objectRMCDataContext.SubmitChanges();

                flag = true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return flag;
        }

        #endregion
    }
}
