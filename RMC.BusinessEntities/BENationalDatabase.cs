
namespace RMC.BusinessEntities
{
    public class BENationalDatabase
    {

        #region Properties

        public int Id { get; set; }
        public string ProfileType { get; set; }
        public string FunctionType { get; set; }
        public int FunctionTypeId { get; set; }
        public int GroupSequence { get; set; }
        public string GroupSequenceName { get; set; }
        public string ValueText { get; set; }
        public double Value { get; set; }

        #endregion

    }
    //End of Class
}
//End of Namespace
