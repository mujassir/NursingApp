using System;
using System.Collections.Generic;

namespace RMC.BusinessEntities
{
    
    /// <summary>
    /// For user info
    /// <CreatedBy>Raman</CreatedBy>
    /// <CreatedOn>July 20, 2009</CreatedOn>
    /// </summary>
    public class BEUserInfoTye
    {

        #region Properties
        
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string UserNameEmail { get; set; } 

        #endregion

    }
    //End of BEUserInfoTye Class

    /// <summary>
    /// For Hospital
    /// <CreatedBy>Raman</CreatedBy>
    /// <CreatedOn>July 20, 2009</CreatedOn>
    /// </summary>
    public class BEHospitalIdName
    {

        #region Properties
        
        public int HospitalInfoID { get; set; }
        public string HospitalName { get; set; } 

        #endregion

    }
    //End of BEHospitalIdName Class

    /// <summary>
    /// For Hospital information
    /// <CreatedBy>Raman</CreatedBy>
    /// <CreatedOn>July 20, 2009</CreatedOn>
    /// </summary>
    public class BEHospitalInfo
    {

        #region Properties
       
        public int HospitalInfoID { get; set; }
        public int UserID { get; set; }
        public string HospitalName { get; set; }
        public string ChiefNursingOfficerFirstName { get; set; }
        public string ChiefNursingOfficerLastName { get; set; }
        public string ChiefNursingOfficerPhone { get; set; }
        public string ChiefNursingOfficerEmail { get; set; }
        public int ColumnID { get; set; }
        public string Value { get; set; }
        public int DynamicDataID { get; set; }

        public string Address { get; set; }
        public string City { get; set; }
        public int StateID { get; set; }
        public string Zip { get; set; }
        public bool IsActive { get; set; }
        public int CountryID { get; set; }
        public bool IsDeleted { get; set; }
        public int HospitalRecordCount { get; set; }

        #endregion

    }
    //End of BEHospitalInfo Class

    public class BEHospitalDemographicInfo
    {

        #region Properties
        
        public int HospitalDemographicID { get; set; }
        public string HospitalUnitName { get; set; } 

        #endregion

    }
    //End of BEHospitalDemographicInfo Class

    public class BETreeHospitalInfo
    {

        #region Properties
        
        public int HospitalID { get; set; }
        public string HospitalName { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public DateTime CreatedDate { get; set; }
        public Nullable<DateTime> ModifiedDate { get; set; }
        public List<BETreeHospitalUnits> HospitalUnitsList { get; set; }
        public bool IsCollapseHospital { get; set; }
        public int PermissionID { get; set; }
        public int HospitalRecordCount { get; set; } 

        #endregion

    }
    //End of BETreeHospitalInfo Class
        
    public class BETreeHospitalUnits
    {

        #region Properties
        
        public int HospitalInfoID { get; set; }
        public int HospitalDemographicID { get; set; }
        public string HospitalUnitName { get; set; }
        public DateTime CreatedDate { get; set; }
        public Nullable<DateTime> ModifiedDate { get; set; }
        public List<BETreeYears> HospitalUnitsYears { get; set; }
        public int PermissionID { get; set; }
        public bool IsCollapseHospitalUnit { get; set; } 

        #endregion

    }
    //End of BETreeHospitalUnits Class

    public class BETreeYears
    {

        #region Properties
        
        public int HospitalDemographicID { get; set; }
        public string Year { get; set; }
        public List<BETreeMonths> HospitalUnitsYearsMonths { get; set; } 

        #endregion

    }
    //End of BETreeYears Class

    public class BETreeMonths
    {

        #region Properties
        
        public int HospitalDemographicID { get; set; }
        public bool IsError { get; set; }
        public string Month { get; set; }
        public string Year { get; set; }
        public List<BENursePDAInfo> NursePDAInfoList { get; set; } 

        #endregion

    }
    //End of BETreeMonths Class

    public class BENursePDAInfo
    {

        #region Properties
        
        public int NurseID { get; set; }
        public string FileReference { get; set; }
        public bool IsCollapseMonth { get; set; }
        public bool IsCollapseYear { get; set; }
        public bool IsAdminCollapseMonth { get; set; }
        public bool IsAdminCollapseYear { get; set; } 

        #endregion

    }
    public class BEHospitalUpdate
    {
        #region Properties

        public int HospitalUploadId { get; set; }
        public string UploadedFileName { get; set; }
        public int HospitalDemographicId { get; set; }
        public string FilePath { get; set; }
        public string Year { get; set; }
        public string Month { get; set; }
        public string OriginalFileName { get; set; }
            
        #endregion
    }
    public class BEnursePDADetail : IEquatable<BEnursePDADetail>
    {
        #region properties

        public string Location { get; set; }
        public string Activity { get; set; }
        public string Subactivity { get; set; }

        public bool Equals(BEnursePDADetail other)
        {

            //Check whether the compared object is null.
            if (Object.ReferenceEquals(other, null)) return false;

            //Check whether the compared object references the same data.
            if (Object.ReferenceEquals(this, other)) return true;

            //Check whether the products' properties are equal.
            return Location.Equals(other.Location) && Activity.Equals(other.Activity) && Subactivity.Equals(other.Subactivity);
        }

        // If Equals() returns true for a pair of objects 
        // then GetHashCode() must return the same value for these objects.

        public override int GetHashCode()
        {

            //Get hash code for the Name field if it is not null.
            int hashProductName = Location == null ? 0 : Location.GetHashCode();

            //Get hash code for the Code field.
            int hashProductCode = Activity.GetHashCode();

            //Get hash code for the Code field.
            int hasSubactivity = Subactivity.GetHashCode();

            //Calculate the hash code for the product.
            return hashProductName ^ hashProductCode ^ hasSubactivity;
        }

        #endregion
    }
    //End of BENursePDAInfo Class

    public class BEHospitalMembers
    {

        #region Properties
        
        public bool Owner { get; set; }
        public Nullable<int> UserID { get; set; }
        public string UserName { get; set; }
        public List<BETreeHospitalUnits> UnitList { get; set; }
        public bool IsApproved { get; set; }
        public string Permission { get; set; }
        public string UnitName { get; set; }
        public int MultiUserDemographicID { get; set; }
        public string HospitalName { get; set; }
        public int PermissionID { get; set; }
        public string HospitalCity { get; set; }
        public string HospitalState { get; set; }
        public Nullable<int> uID { get; set; }
        public Nullable<int> hospitalInfoID { get; set; }

        #endregion

    }
    //End of BEHospitalMembers Class

    public class BEIntegerCollection
    {

        #region Properties
        
        public int ID { get; set; } 

        #endregion

    }
    //End of BEIntegerCollection Class

    public class BEHospitalCumUnitInformation : RMC.DataService.HospitalInfo
    {

        #region Properties
        
        public int UnitID { get; set; }
        public string UnitName { get; set; } 

        #endregion

    }
    //End of BEHospitalCumUnitInformation Class

    public class BEDropDownListData
    {

        #region Properties
        
        public string Key { get; set; }
        public string Value { get; set; } 

        #endregion

    }
    //End of BEDropDownListData Class
}
//End of Namespace.