using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;

namespace RMC.BussinessService
{
   public class CategoryProfile
    {
       //RMC.DataService.RMCDataContext objDBML = new RMC.DataService.RMCDataContext(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);
       RMC.DataService.RMCDataContext objDBML = null;
       List<RMC.DataService.ProfileType> _objectGenericProfileType = null;

       //public List<RMC.DataService.ProfileType> GetProfileTypes()
       //{
       //    try
       //    {
       //        objDBML = new RMC.DataService.RMCDataContext();
       //        _objectGenericProfileType = (from gpt in objDBML.ProfileTypes
       //                                     select new { 
       //                                     Location=gpt.
       //                                     }
       //                                     ).ToList<RMC.DataService.ProfileType>();

       //        return _objectGenericProfileType;
       //    }
       //    catch (Exception ex)
       //    {
       //        ex.Data.Add("Function", "GetProfileTypes");
       //        ex.Data.Add("Class", "BSProfileType");
       //        throw ex;
       //    }
       //    finally
       //    {
       //        _objectRMCDataContext.Dispose();
       //    }
       //}
    }
}
