using System;

namespace RMC.BusinessEntities
{
    [Serializable]
    public class BEReports
    {

        #region Properties

        public string ColumnName { get; set; }
        public string RowName { get; set; }
        public int ColumnNumber { get; set; }
        public string Values { get; set; }
        public Decimal ValuesDecimal { get; set; }
        public string MonthName { get; set; }
        public double ValuesSum { get; set; }
        public string Name { get; set; }
        public double DataPoint { get; set; }
        public int RecordCounter { get; set;}
        public string Email { get; set; }

        #endregion

    }
    //End of BEReports Class

    public class BEHospitalBenchmarkSummary : RMC.DataService.HospitalDemographicInfo
    {

        #region Properties

        public string Demographic { get; set; }
        public string State { get; set; }
        public string HospitalBedSize { get; set; }

        #endregion

    }
    //End of BEHospitalBenchmarkSummary Class
    [System.Serializable]
    public class BEFunctionNames
    {

        #region Properties

        public string ColumnName { get; set; }
        public string FunctionName { get; set; }
        public string FunctionValueText { get; set; }
        public double FunctionNameDouble { get; set; }

        #endregion

    }
    //End of BEFunctionNames Class

    public class BEProfileCategory
    {

        #region Properties
        public string ProfileCategoryName { get; set; }
        public string ProfileType { get; set; }
        #endregion

    }
    //End of BEProfileCategory Class

    public class BEReportsFilter
    {

        #region Properties
        
        public int filterId { get; set; }
        public string filterName { get; set; } 

        #endregion

    }
    //End of BEReportsFilter Class

    public class BEReportsYearMonth
    {

        #region Properties

        public string year { get; set; }
        public string month { get; set; }
        public int monthIndex { get; set; }

        #endregion

    }
    //End of BEReportsYearMonth Class

    public class BEReportsDatabaseValues
    {

        #region Properties

        public int valueId { get; set; }
        public string value { get; set; }
    
        #endregion

    }
    //End of BEReportsDatabaseValues Class

    public class BEReportsLocationProfile : RMC.DataService.NursePDADetail
    {
        #region Properties

        public string LastLocationName { get; set; }
        public string LocationName { get; set; }
        public string HospitalUnitName { get; set; }
        public int HospitalUnitID { get; set; }
        public int HospitalSize { get; set; }
        public int HospitalID { get; set; }
        public int RecordCounter { get; set; }
        public string HospitalUnitIDCounter { get; set; }
        public double CountTrip { get; set; }
        public string comboName { get; set; }
        public string CountTripDisplay { get; set; }

        #endregion
    }
    //End of BEReportsLocationProfile Class

    public class BEReportsConfigName
    {
        #region Properties

        public string  configName { get; set; }

        #endregion
    }
    //End of BEReportsConfigName Class

    //End of BEReportsDatabaseValues Class

    public class BEReportsHospitalUpload : RMC.DataService.HospitalUpload
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
    //End of BEReportsLocationProfile Class
}
//End of Namespace
