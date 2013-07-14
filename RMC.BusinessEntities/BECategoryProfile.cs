
using System.Collections.Generic;
namespace RMC.BusinessEntities
{
    public class BECategoryProfile : RMC.DataService.CategoryProfile 
    {

        #region Properties
        
        public int UserID { get; set; }
        public int PermissionID { get; set; }
        public string Activity { get; set; }
        public string SubActivity { get; set; }
        public string Location { get; set; }
        public string PermissionText { get; set; }
        public string CategoryAssignmentName { get; set; }
        public int CategoryAssignmentID { get; set; }
       // public int ValidationID { get; set; }
        public int LocationID { get; set; }
        public int SubActivityID { get; set; }
        public int ActivityID { get; set; }    

        #endregion

    }
    //End of Class

    public class CompareCategoryProfile : IEqualityComparer<BECategoryProfile>
    {

        public bool Equals(BECategoryProfile x, BECategoryProfile y)
        {
            return x.ActivityID == y.ActivityID && x.LocationID == y.LocationID;
        }

        public int GetHashCode(BECategoryProfile obj)
        {
            string code = obj.LocationID.ToString() + obj.ActivityID.ToString();
            return code.GetHashCode();
        }
    }
}

//End of Namespace
