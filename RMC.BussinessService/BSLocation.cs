using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RMC.BussinessService
{
    public class BSLocation
    {

        #region Variables

        //DataContext Object.
        RMC.DataService.RMCDataContext _objectRMCDataContext = null;

        //Generic list of Data Service object.
        List<RMC.DataService.Location> _objectGenericLocation = null;

        #endregion

        #region Public Methods

        /// <summary>
        /// Use in Profile Detail.ascx
        /// </summary>
        /// <returns></returns>
        public List<RMC.DataService.Location> GetAllLocation()
        {
            try
            {
                _objectRMCDataContext = new RMC.DataService.RMCDataContext();
                _objectGenericLocation = (from l in _objectRMCDataContext.Locations
                                          where l.IsActive == true
                                          select l).ToList<RMC.DataService.Location>();

                return _objectGenericLocation;
            }
            catch (Exception ex)
            {
                ex.Data.Add("Function", "GetAllLocation");
                ex.Data.Add("Class", "BSLocation");
                throw ex;
            }
            finally
            {
                _objectRMCDataContext.Dispose();
            }
        }

        /// <summary>
        /// Gets the data of last location
        /// </summary>
        /// <returns></returns>
        public List<RMC.DataService.LastLocation> GetAllLastLocation()
        {
            try
            {
                _objectRMCDataContext = new RMC.DataService.RMCDataContext();
                List<RMC.DataService.LastLocation> objectGenericLastLocation = null;
                objectGenericLastLocation = (from l in _objectRMCDataContext.LastLocations
                                             where l.IsActive == true
                                             select l).ToList<RMC.DataService.LastLocation>();

                return objectGenericLastLocation;
            }
            catch (Exception ex)
            {
                ex.Data.Add("Function", "GetAllLocation");
                ex.Data.Add("Class", "BSLocation");
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
        /// <param name="locationName"></param>
        /// <returns></returns>
        public int InsertLocation(string locationName)
        {
            try
            {
                RMC.DataService.Location objectLocation = new RMC.DataService.Location();
                _objectRMCDataContext = new RMC.DataService.RMCDataContext();

                objectLocation.Location1 = locationName;
                objectLocation.IsActive = true;
                //changes by CM on 27jan2012
                objectLocation.RenameLocation = "";  
                _objectRMCDataContext.Locations.InsertOnSubmit(objectLocation);
                _objectRMCDataContext.SubmitChanges();
                return objectLocation.LocationID;
            }
            catch (Exception ex)
            {
                ex.Data.Add("Function", "InsertLocation");
                ex.Data.Add("Class", "BSLocation");
                throw ex;
            }
            finally
            {
                _objectRMCDataContext.Dispose();
            }
        }

        #endregion

    }
}
