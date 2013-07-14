
namespace RMC.BusinessEntities
{

    public class BEValidationData
    {

        #region "Properties"

        public int NurseID { get; set; }
        public int NursePDADetailID { get; set; }
        public int CategoryGroupID { get; set; }
        public int TypeID { get; set; }
        public int ResourceRequirementID { get; set; }
        public int LastLocationID { get; set; }
        public string LastLocationDate { get; set; }
        public string LastLocationTime { get; set; }
        public int LocationID { get; set; }
        public string LocationDate { get; set; }
        public string LocationTime { get; set; }
        public int ActivityID { get; set; }
        public string ActivityDate { get; set; }
        public string ActivityTime { get; set; }
        public int SubActivityID { get; set; }
        public string SubActivityDate { get; set; }
        public string SubActivityTime { get; set; }
        public bool IsErrorExist { get; set; }
        public string CategoryGroupName { get; set; }
        public string TypeName { get; set; }
        public string ResourceRequirementName { get; set; }
        public string LastLocationName { get; set; }
        public string LocationName { get; set; }
        public string ActivityName { get; set; }
        public string SubActivityName { get; set; }
        public string CongnitiveCategories { get; set; }
        //public int CognitiveCategoryID { get; set; }
        #endregion

    }
    //End Of BEValidationData Class.
    public class BEValidation : RMC.DataService.NursePDADetail
    {

        #region Properties

        public string LastLocationName { get; set; }
        public string LocationName { get; set; }
        public string ActivityName { get; set; }
        public string SubActivityName { get; set; }
        public string HospitalUnitName { get; set; }
        public int HospitalUnitID { get; set; }
        public string Month { get; set; }
        public int MonthIndex { get; set; }
        public string Year { get; set; }
        public string FileName { get; set; }
        public int HospitalSize { get; set; }
        public int HospitalID { get; set; }
        public int RecordCounter { get; set; }
        public string HospitalUnitIDCounter { get; set; }
        public int DataPoint { get; set; }
        public string SpecialCategory { get; set; }
        public string SpecialActivity { get; set; }
        public int CognitiveCategoryID { get; set; }
        #endregion

    }
    //End Of BEValidation Class.

    public class BEValidationSpecialType
    {
        #region properties

        public string SpecialCategory { get; set; }
        public string SpecialActivity { get; set; }
        public string Date { get; set; }
        public string Time { get; set; }
        public int SpecialTypeID { get; set; }

        #endregion
    }
}
//End Of Namespace.
