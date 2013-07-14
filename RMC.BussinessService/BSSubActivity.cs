using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RMC.BussinessService
{
    public class BSSubActivity
    {

        #region Variables

        //DataContext Object.
        RMC.DataService.RMCDataContext _objectRMCDataContext = null;

        //Generic list of Data Service object.
        List<RMC.DataService.SubActivity> _objectGenericSubActivity = null;

        #endregion

        #region Public Methods

        /// <summary>
        /// Use in Profile Detail.ascx
        /// </summary>
        /// <returns></returns>
        public List<RMC.DataService.SubActivity> GetAllSubActivity()
        {
            try
            {
                _objectRMCDataContext = new RMC.DataService.RMCDataContext();
                _objectGenericSubActivity = (from sa in _objectRMCDataContext.SubActivities
                                             where sa.IsActive == true
                                             select sa).ToList<RMC.DataService.SubActivity>();

                return _objectGenericSubActivity;
            }
            catch (Exception ex)
            {
                ex.Data.Add("Function", "GetAllSubActivity");
                ex.Data.Add("Class", "BSSubActivity");
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
        public int InsertSubActivity(string subActivityName)
        {
            try
            {
                RMC.DataService.SubActivity objectSubActivity = new RMC.DataService.SubActivity();
                _objectRMCDataContext = new RMC.DataService.RMCDataContext();

                objectSubActivity.SubActivity1 = subActivityName;
                objectSubActivity.IsActive = true;

                _objectRMCDataContext.SubActivities.InsertOnSubmit(objectSubActivity);
                _objectRMCDataContext.SubmitChanges();
                return objectSubActivity.SubActivityID;
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
