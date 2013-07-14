using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Data.OleDb;
using RMC.BussinessService;
using RMC.BusinessEntities;
using System.Web.UI.DataVisualization.Charting;
using LogExceptions;

namespace RMC.Web.UserControls
{
    public partial class ReportTimeRNGrid : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                LabelHospitalName.Text = "#" + Request.QueryString["hospitalName"] + "    ";
                LabelHospitalUnitName.Text = "Unit: " + Request.QueryString["hospitalUnitName"] + "    ";
                LabelFilter.Text = "Filter: " + Request.QueryString["filter"];

                if (IsPostBack == false)
                {
                    RMC.BussinessService.BSReports objectBEReports = new BSReports();

                    string activitiesID = Request.QueryString["activitiesID"];
                    if (activitiesID == string.Empty)
                    {
                        activitiesID = null;
                    }
                    string valueAddedCategoryID = Request.QueryString["valueAddedCategoryID"];
                    if (valueAddedCategoryID == string.Empty)
                    {
                        valueAddedCategoryID = null;
                    }

                    string OthersCategoryID = Request.QueryString["OthersCategoryID"];
                    if (OthersCategoryID == string.Empty)
                    {
                        OthersCategoryID = null;
                    }

                    string LocationCategoryID = Request.QueryString["LocationCategoryID"];
                    if (LocationCategoryID == string.Empty)
                    {
                        LocationCategoryID = null;
                    }

                    int? hospitalUnitId = null;
                    if (Request.QueryString["hospitalUnitID"] != string.Empty)
                    {
                        hospitalUnitId = Convert.ToInt32(Request.QueryString["hospitalUnitID"]);
                    }

                    int? firstYear = null;
                    if (Request.QueryString["yearFrom"] != string.Empty)
                    {
                        firstYear = Convert.ToInt32(Request.QueryString["yearFrom"]);
                    }

                    int? lastYear = null;
                    if (Request.QueryString["yearTo"] != string.Empty)
                    {
                        lastYear = Convert.ToInt32(Request.QueryString["yearTo"]);
                    }

                    int? firstMonth = null;
                    if (Request.QueryString["monthFrom"] != string.Empty)
                    {
                        firstMonth = Convert.ToInt32(Request.QueryString["monthFrom"]);
                    }

                    int? lastMonth = null;
                    if (Request.QueryString["monthTo"] != string.Empty)
                    {
                        lastMonth = Convert.ToInt32(Request.QueryString["monthTo"]);
                    }

                    int? bedInUnitFrom = null;
                    if (Request.QueryString["BedsInUnitFrom"] != string.Empty)
                    {
                        bedInUnitFrom = Convert.ToInt32(Request.QueryString["BedsInUnitFrom"]);
                    }

                    int optBedInUnitFrom = 0;
                    if (Request.QueryString["optBedsInUnitFrom"] != "0")
                    {
                        optBedInUnitFrom = Convert.ToInt32(Request.QueryString["optBedsInUnitFrom"]);
                    }

                    int? bedInUnitTo = null;
                    if (Request.QueryString["bedsInUnitTo"] != string.Empty)
                    {
                        bedInUnitTo = Convert.ToInt32(Request.QueryString["bedsInUnitTo"]);
                    }

                    int optBedInUnitTo = 0;
                    if (Request.QueryString["optBedsInUnitTo"] != "0")
                    {
                        optBedInUnitTo = Convert.ToInt32(Request.QueryString["optBedsInUnitTo"]);
                    }

                    float? budgetedPatientFrom = null;
                    if (Request.QueryString["budgetedPatientFrom"] != string.Empty)
                    {
                        budgetedPatientFrom = Convert.ToInt32(Request.QueryString["budgetedPatientFrom"]);
                    }

                    int optBudgetedPatientFrom = 0;
                    if (Request.QueryString["optBudgetedPatientFrom"] != "0")
                    {
                        optBudgetedPatientFrom = Convert.ToInt32(Request.QueryString["optBudgetedPatientFrom"]);
                    }

                    float? budgetedPatientTo = null;
                    if (Request.QueryString["budgetedPatientTo"] != string.Empty)
                    {
                        budgetedPatientTo = Convert.ToInt32(Request.QueryString["budgetedPatientTo"]);
                    }

                    int optBudgetedPatientTo = 0;
                    if (Request.QueryString["optBudgetedPatientTo"] != "0")
                    {
                        optBudgetedPatientTo = Convert.ToInt32(Request.QueryString["optBudgetedPatientTo"]);
                    }

                    string startDate = null;
                    string endDate = null;

                    int? electronicDocumentFrom = null;
                    if (Request.QueryString["electronicDocumentationFrom"] != string.Empty)
                    {
                        electronicDocumentFrom = Convert.ToInt32(Request.QueryString["electronicDocumentationFrom"]);
                    }

                    int optElectronicDocumentFrom = 0;
                    if (Request.QueryString["optElectronicDocumentationFrom"] != "0")
                    {
                        optElectronicDocumentFrom = Convert.ToInt32(Request.QueryString["optElectronicDocumentationFrom"]);
                    }

                    int? electronicDocumentTo = null;
                    if (Request.QueryString["electronicDocumentationTo"] != string.Empty)
                    {
                        electronicDocumentTo = Convert.ToInt32(Request.QueryString["electronicDocumentationTo"]);
                    }

                    int optElectronicDocumentTo = 0;
                    if (Request.QueryString["optElectronicDocumentationTo"] != "0")
                    {
                        optElectronicDocumentTo = Convert.ToInt32(Request.QueryString["optElectronicDocumentationTo"]);
                    }

                    int docByException = 0;
                    if (Request.QueryString["docByException"] != "0")
                    {
                        docByException = Convert.ToInt32(Request.QueryString["docByException"]);
                    }

                    string unitType = null;
                    if (Request.QueryString["unitType"] != string.Empty)
                    {
                        unitType = Request.QueryString["unitType"];
                    }

                    string pharmacyType = null;
                    if (Request.QueryString["pharmacyType"] != string.Empty)
                    {
                        pharmacyType = Request.QueryString["pharmacyType"];
                    }

                    string hospitalType = null;
                    if (Request.QueryString["hospitalType"] != string.Empty)
                    {
                        hospitalType = Request.QueryString["hospitalType"];
                    }

                    int? hospitalSizeFrom = null;
                    if (Request.QueryString["hospitalSizeFrom"] != string.Empty)
                    {
                        hospitalSizeFrom = Convert.ToInt32(Request.QueryString["hospitalSizeFrom"]);
                    }

                    int optHospitalSizeFrom = 0;
                    if (Request.QueryString["optHospitalSizeFrom"] != "0")
                    {
                        optHospitalSizeFrom = Convert.ToInt32(Request.QueryString["optHospitalSizeFrom"]);
                    }

                    int? hospitalSizeTo = null;
                    if (Request.QueryString["hospitalSizeTo"] != string.Empty)
                    {
                        hospitalSizeTo = Convert.ToInt32(Request.QueryString["hospitalSizeTo"]);
                    }

                    int optHospitalSizeTo = 0;
                    if (Request.QueryString["optHospitalSizeTo"] != "0")
                    {
                        optHospitalSizeTo = Convert.ToInt32(Request.QueryString["optHospitalSizeTo"]);
                    }

                    int? countryId = null;
                    if (Request.QueryString["countryId"] != "0")
                    {
                        countryId = Convert.ToInt32(Request.QueryString["countryId"]);
                    }

                    int? stateId = null;
                    if (Request.QueryString["stateId"] != "0")
                    {
                        stateId = Convert.ToInt32(Request.QueryString["stateId"]);
                    }

                    string activities = string.Empty;
                    if (Request.QueryString["activities"] != string.Empty)
                    {
                        activities = Request.QueryString["activities"];
                    }


                    string value = string.Empty;
                    if (Request.QueryString["value"] != string.Empty)
                    {
                        value = Request.QueryString["value"];
                    }

                    string others = string.Empty;
                    if (Request.QueryString["others"] != string.Empty)
                    {
                        others = Request.QueryString["others"];
                    }

                    string location = string.Empty;
                    if (Request.QueryString["location"] != string.Empty)
                    {
                        location = Request.QueryString["location"];
                    }

                    int? dataPointsFrom = null;
                    if (Request.QueryString["dataPointsFrom"] != string.Empty)
                    {
                        dataPointsFrom = Convert.ToInt32(Request.QueryString["dataPointsFrom"]);
                    }
                    int optDataPointsFrom = 0;
                    if (Request.QueryString["optDataPointsFrom"] != "0")
                    {
                        optDataPointsFrom = Convert.ToInt32(Request.QueryString["optDataPointsFrom"]);
                    }
                    int? dataPointsTo = null;
                    if (Request.QueryString["dataPointsTo"] != string.Empty)
                    {
                        dataPointsTo = Convert.ToInt32(Request.QueryString["dataPointsTo"]);
                    }
                    int optDataPointsTo = 0;
                    if (Request.QueryString["optDataPointsTo"] != "0")
                    {
                        optDataPointsTo = Convert.ToInt32(Request.QueryString["optDataPointsTo"]);
                    }

                    string configName = null;
                    if (Request.QueryString["configName"] != string.Empty)
                    {
                        configName = Request.QueryString["configName"];
                    }

                    //List<RMC.BusinessEntities.BEFunctionNames> objectBEFunctionNames = objectBEReports.GetPerformanceTest(valueAddedCategoryID, OthersCategoryID, LocationCategoryID, hospitalUnitId, firstYear, lastYear, firstMonth, lastMonth, bedInUnitFrom, optBedInUnitFrom, bedInUnitTo, optBedInUnitTo, budgetedPatientFrom, optBudgetedPatientFrom, budgetedPatientTo, optBudgetedPatientTo, startDate, endDate, electronicDocumentFrom, optElectronicDocumentFrom, electronicDocumentTo, optElectronicDocumentTo, docByException, unitType, pharmacyType, hospitalType, optHospitalSizeFrom, hospitalSizeFrom, optHospitalSizeTo, hospitalSizeTo, countryId, stateId, value, others, location, null, 0, null, 0, configName, "MonthlySummaryDashboard");
                    List<RMC.BusinessEntities.BEFunctionNames> objectBEFunctionNames = objectBEReports.GetPerformanceTest(activitiesID, valueAddedCategoryID, OthersCategoryID, LocationCategoryID, hospitalUnitId, firstYear, lastYear, firstMonth, lastMonth, bedInUnitFrom, optBedInUnitFrom, bedInUnitTo, optBedInUnitTo, budgetedPatientFrom, optBudgetedPatientFrom, budgetedPatientTo, optBudgetedPatientTo, startDate, endDate, electronicDocumentFrom, optElectronicDocumentFrom, electronicDocumentTo, optElectronicDocumentTo, docByException, unitType, pharmacyType, hospitalType, optHospitalSizeFrom, hospitalSizeFrom, optHospitalSizeTo, hospitalSizeTo, countryId, stateId, activities, value, others, location, dataPointsFrom, optDataPointsFrom, dataPointsTo, optDataPointsTo, configName, "MonthlySummaryDashboard");
                    RMC.BussinessService.BSChartControl objectBSChartControl = new RMC.BussinessService.BSChartControl();
                    Series objectSeries = objectBSChartControl.GetColumnChartSeries(null, objectBEFunctionNames);

                    ChartTimeRN.Series.Add(objectSeries);
                    //ChartTimeRN.Series["Series1"]["PointWidth"] = "0.3";
                    int count = objectBEFunctionNames.Count();
                    ChartTimeRN.Width = 450 + (count * 60);
                    ChartTimeRN.ChartAreas["ChartArea1"].AxisY.Title = "Positive is Better than National Average";
                }
            }
            catch (Exception ex)
            {
                LogManager._stringObject = "ReportTimeRN.ascx ----";
                LogManager.SetExceptionDetails(ex, LogManager._stringObject, null);
                LogManager.LogException(ex, LogManager.LoggingCategory.General, LogManager.LoggingLevel.Error);
                CommonClass.Show(LogManager.ShowErrorDetail(ex));
            }
        }

        protected void ObjectDataSource1_Selecting(object sender, ObjectDataSourceSelectingEventArgs e)
        {
            try
            {
                //e.InputParameters["valueAddedCategoryID"] = Request.QueryString["valueAddedCategoryID"];
                //e.InputParameters["OthersCategoryID"] = Request.QueryString["OthersCategoryID"];
                //e.InputParameters["LocationCategoryID"] = Request.QueryString["LocationCategoryID"];
                string activitiesID = Request.QueryString["activitiesID"];
                if (activitiesID == string.Empty)
                {
                    activitiesID = null;
                }
                e.InputParameters["activitiesID"] = activitiesID;

                string valueAddedCategoryID = Request.QueryString["valueAddedCategoryID"];
                if (valueAddedCategoryID == string.Empty)
                {
                    valueAddedCategoryID = null;
                }
                e.InputParameters["valueAddedCategoryID"] = valueAddedCategoryID;

                string OthersCategoryID = Request.QueryString["OthersCategoryID"];
                if (OthersCategoryID == string.Empty)
                {
                    OthersCategoryID = null;
                }
                e.InputParameters["OthersCategoryID"] = OthersCategoryID;

                string LocationCategoryID = Request.QueryString["LocationCategoryID"];
                if (LocationCategoryID == string.Empty)
                {
                    LocationCategoryID = null;
                }
                e.InputParameters["LocationCategoryID"] = LocationCategoryID;

                e.InputParameters["hospitalUnitID"] = Request.QueryString["hospitalUnitID"];

                string firstYear = Request.QueryString["yearFrom"];
                if (firstYear == string.Empty)
                {
                    firstYear = null;
                }
                e.InputParameters["firstYear"] = firstYear;

                string lastYear = Request.QueryString["yearTo"];
                if (lastYear == string.Empty)
                {
                    lastYear = null;
                }
                e.InputParameters["lastYear"] = lastYear;

                string firstMonth = Request.QueryString["monthFrom"];
                if (firstMonth == string.Empty)
                {
                    firstMonth = null;
                }
                e.InputParameters["firstMonth"] = firstMonth;

                string lastMonth = Request.QueryString["monthTo"];
                if (lastMonth == string.Empty)
                {
                    lastMonth = null;
                }
                e.InputParameters["lastMonth"] = lastMonth;

                string bedInUnitFrom = Request.QueryString["BedsInUnitFrom"];
                if (bedInUnitFrom == string.Empty)
                {
                    bedInUnitFrom = null;
                }
                e.InputParameters["bedInUnitFrom"] = bedInUnitFrom;
                e.InputParameters["optBedInUnitFrom"] = Convert.ToInt32(Request.QueryString["optBedsInUnitFrom"]);
                string bedInUnitTo = Request.QueryString["bedsInUnitTo"];
                if (bedInUnitTo == string.Empty)
                {
                    bedInUnitTo = null;
                }
                e.InputParameters["bedInUnitTo"] = bedInUnitTo;
                e.InputParameters["optBedInUnitTo"] = Convert.ToInt32(Request.QueryString["optBedsInUnitTo"]);

                string budgetedPatientFrom = Request.QueryString["budgetedPatientFrom"];
                if (budgetedPatientFrom == string.Empty)
                {
                    budgetedPatientFrom = null;
                }
                e.InputParameters["budgetedPatientFrom"] = budgetedPatientFrom;
                e.InputParameters["optBudgetedPatientFrom"] = Convert.ToInt32(Request.QueryString["optBudgetedPatientFrom"]);
                string budgetedPatientTo = Request.QueryString["budgetedPatientTo"];
                if (budgetedPatientTo == string.Empty)
                {
                    budgetedPatientTo = null;
                }
                e.InputParameters["budgetedPatientTo"] = budgetedPatientTo;
                e.InputParameters["optBudgetedPatientTo"] = Convert.ToInt32(Request.QueryString["optBudgetedPatientTo"]);

                e.InputParameters["startDate"] = null;
                e.InputParameters["endDate"] = null;

                string electronicDocumentFrom = Request.QueryString["electronicDocumentationFrom"];
                if (electronicDocumentFrom == string.Empty)
                {
                    electronicDocumentFrom = null;
                }
                e.InputParameters["electronicDocumentFrom"] = electronicDocumentFrom;
                e.InputParameters["optElectronicDocumentFrom"] = Convert.ToInt32(Request.QueryString["optElectronicDocumentationFrom"]);
                string electronicDocumentTo = Request.QueryString["electronicDocumentationTo"];
                if (electronicDocumentTo == string.Empty)
                {
                    electronicDocumentTo = null;
                }
                e.InputParameters["electronicDocumentTo"] = electronicDocumentTo;
                e.InputParameters["optElectronicDocumentTo"] = Convert.ToInt32(Request.QueryString["optElectronicDocumentationTo"]);

                string docByException = Request.QueryString["docByException"];
                if (docByException == string.Empty)
                {
                    docByException = null;
                }
                e.InputParameters["docByException"] = docByException;

                string unitType = Request.QueryString["unitType"];
                if (unitType == string.Empty)
                {
                    unitType = null;
                }
                e.InputParameters["unitType"] = unitType;
                string pharmacyType = Request.QueryString["pharmacyType"];
                if (pharmacyType == string.Empty)
                {
                    pharmacyType = null;
                }
                e.InputParameters["pharmacyType"] = pharmacyType;
                string hospitalType = Request.QueryString["hospitalType"];
                if (hospitalType == string.Empty)
                {
                    hospitalType = null;
                }
                e.InputParameters["hospitalType"] = hospitalType;

                e.InputParameters["optHospitalSizeFrom"] = Convert.ToInt32(Request.QueryString["optHospitalSizeFrom"]);
                string hospitalSizeFrom = Request.QueryString["hospitalSizeFrom"];
                if (hospitalSizeFrom == string.Empty)
                {
                    hospitalSizeFrom = null;
                }
                e.InputParameters["hospitalSizeFrom"] = hospitalSizeFrom;
                e.InputParameters["optHospitalSizeTo"] = Convert.ToInt32(Request.QueryString["optHospitalSizeTo"]);
                string hospitalSizeTo = Request.QueryString["hospitalSizeTo"];
                if (hospitalSizeTo == string.Empty)
                {
                    hospitalSizeTo = null;
                }
                e.InputParameters["hospitalSizeTo"] = hospitalSizeTo;

                string countryId = Request.QueryString["countryId"];
                if (countryId == "0")
                {
                    countryId = null;
                }
                e.InputParameters["countryId"] = countryId;
                string stateId = Request.QueryString["stateId"];
                if (stateId == "0")
                {
                    stateId = null;
                }
                e.InputParameters["stateId"] = stateId;
                //e.InputParameters["countryId"] = 0;
                //e.InputParameters["stateId"] = 0;
                e.InputParameters["activities"] = Request.QueryString["activities"];
                e.InputParameters["value"] = Request.QueryString["value"];
                e.InputParameters["others"] = Request.QueryString["others"];
                e.InputParameters["location"] = Request.QueryString["location"];


                string dataPointsFrom = Request.QueryString["dataPointsFrom"];
                if (dataPointsFrom == string.Empty)
                {
                    dataPointsFrom = null;
                }
                e.InputParameters["dataPointsFrom"] = dataPointsFrom;

                e.InputParameters["optDataPointsFrom"] = Convert.ToInt32(Request.QueryString["optDataPointsFrom"]);

                string dataPointsTo = Request.QueryString["dataPointsTo"];
                if (dataPointsTo == string.Empty)
                {
                    dataPointsTo = null;
                }
                e.InputParameters["dataPointsTo"] = dataPointsTo;
                e.InputParameters["optdataPointsTo"] = Convert.ToInt32(Request.QueryString["optDataPointsTo"]);



                string configName = Request.QueryString["configName"];
                if (configName == string.Empty)
                {
                    configName = null;
                }
                e.InputParameters["configName"] = configName;

            }
            catch (Exception ex)
            {
                LogManager._stringObject = "ReportTimeRN.ascx ----";
                LogManager.SetExceptionDetails(ex, LogManager._stringObject, null);
                LogManager.LogException(ex, LogManager.LoggingCategory.General, LogManager.LoggingLevel.Error);
                CommonClass.Show(LogManager.ShowErrorDetail(ex));
            }
        }

        protected void ObjectDataSource2_Selecting(object sender, ObjectDataSourceSelectingEventArgs e)
        {
            try
            {
                //e.InputParameters["valueAddedCategoryID"] = Request.QueryString["valueAddedCategoryID"];
                //e.InputParameters["OthersCategoryID"] = Request.QueryString["OthersCategoryID"];
                //e.InputParameters["LocationCategoryID"] = Request.QueryString["LocationCategoryID"];
                string activitiesID = Request.QueryString["activitiesID"];
                if (activitiesID == string.Empty)
                {
                    activitiesID = null;
                }
                e.InputParameters["activitiesID"] = activitiesID;

                string valueAddedCategoryID = Request.QueryString["valueAddedCategoryID"];
                if (valueAddedCategoryID == string.Empty)
                {
                    valueAddedCategoryID = null;
                }
                e.InputParameters["valueAddedCategoryID"] = valueAddedCategoryID;

                string OthersCategoryID = Request.QueryString["OthersCategoryID"];
                if (OthersCategoryID == string.Empty)
                {
                    OthersCategoryID = null;
                }
                e.InputParameters["OthersCategoryID"] = OthersCategoryID;

                string LocationCategoryID = Request.QueryString["LocationCategoryID"];
                if (LocationCategoryID == string.Empty)
                {
                    LocationCategoryID = null;
                }
                e.InputParameters["LocationCategoryID"] = LocationCategoryID;

                //int? hospitalUnitId = null;
                //if (Request.QueryString["hospitalUnitID"] != string.Empty)
                //{
                //    hospitalUnitId = Convert.ToInt32(Request.QueryString["hospitalUnitID"]);
                //}
                //if (hospitalUnitId != null)
                //{
                //    e.InputParameters["hospitalUnitID"] = hospitalUnitId;
                //}
                //else
                //{
                    e.InputParameters["hospitalUnitID"] = null;
                //}
                string firstYear = Request.QueryString["yearFrom"];
                if (firstYear == string.Empty)
                {
                    firstYear = null;
                }
                e.InputParameters["firstYear"] = firstYear;

                string lastYear = Request.QueryString["yearTo"];
                if (lastYear == string.Empty)
                {
                    lastYear = null;
                }
                e.InputParameters["lastYear"] = lastYear;

                string firstMonth = Request.QueryString["monthFrom"];
                if (firstMonth == string.Empty)
                {
                    firstMonth = null;
                }
                e.InputParameters["firstMonth"] = firstMonth;

                string lastMonth = Request.QueryString["monthTo"];
                if (lastMonth == string.Empty)
                {
                    lastMonth = null;
                }
                e.InputParameters["lastMonth"] = lastMonth;

                string bedInUnitFrom = Request.QueryString["BedsInUnitFrom"];
                if (bedInUnitFrom == string.Empty)
                {
                    bedInUnitFrom = null;
                }
                e.InputParameters["bedInUnitFrom"] = bedInUnitFrom;
                e.InputParameters["optBedInUnitFrom"] = Convert.ToInt32(Request.QueryString["optBedsInUnitFrom"]);
                string bedInUnitTo = Request.QueryString["bedsInUnitTo"];
                if (bedInUnitTo == string.Empty)
                {
                    bedInUnitTo = null;
                }
                e.InputParameters["bedInUnitTo"] = bedInUnitTo;
                e.InputParameters["optBedInUnitTo"] = Convert.ToInt32(Request.QueryString["optBedsInUnitTo"]);

                string budgetedPatientFrom = Request.QueryString["budgetedPatientFrom"];
                if (budgetedPatientFrom == string.Empty)
                {
                    budgetedPatientFrom = null;
                }
                e.InputParameters["budgetedPatientFrom"] = budgetedPatientFrom;
                e.InputParameters["optBudgetedPatientFrom"] = Convert.ToInt32(Request.QueryString["optBudgetedPatientFrom"]);
                string budgetedPatientTo = Request.QueryString["budgetedPatientTo"];
                if (budgetedPatientTo == string.Empty)
                {
                    budgetedPatientTo = null;
                }
                e.InputParameters["budgetedPatientTo"] = budgetedPatientTo;
                e.InputParameters["optBudgetedPatientTo"] = Convert.ToInt32(Request.QueryString["optBudgetedPatientTo"]);

                e.InputParameters["startDate"] = null;
                e.InputParameters["endDate"] = null;

                string electronicDocumentFrom = Request.QueryString["electronicDocumentationFrom"];
                if (electronicDocumentFrom == string.Empty)
                {
                    electronicDocumentFrom = null;
                }
                e.InputParameters["electronicDocumentFrom"] = electronicDocumentFrom;
                e.InputParameters["optElectronicDocumentFrom"] = Convert.ToInt32(Request.QueryString["optElectronicDocumentationFrom"]);
                string electronicDocumentTo = Request.QueryString["electronicDocumentationTo"];
                if (electronicDocumentTo == string.Empty)
                {
                    electronicDocumentTo = null;
                }
                e.InputParameters["electronicDocumentTo"] = electronicDocumentTo;
                e.InputParameters["optElectronicDocumentTo"] = Convert.ToInt32(Request.QueryString["optElectronicDocumentationTo"]);

                string docByException = Request.QueryString["docByException"];
                if (docByException == string.Empty)
                {
                    docByException = null;
                }
                e.InputParameters["docByException"] = docByException;

                string unitType = Request.QueryString["unitType"];
                if (unitType == string.Empty)
                {
                    unitType = null;
                }
                e.InputParameters["unitType"] = unitType;
                string pharmacyType = Request.QueryString["pharmacyType"];
                if (pharmacyType == string.Empty)
                {
                    pharmacyType = null;
                }
                e.InputParameters["pharmacyType"] = pharmacyType;
                string hospitalType = Request.QueryString["hospitalType"];
                if (hospitalType == string.Empty)
                {
                    hospitalType = null;
                }
                e.InputParameters["hospitalType"] = hospitalType;


                e.InputParameters["optHospitalSizeFrom"] = Convert.ToInt32(Request.QueryString["optHospitalSizeFrom"]);
                string hospitalSizeFrom = Request.QueryString["hospitalSizeFrom"];
                if (hospitalSizeFrom == string.Empty)
                {
                    hospitalSizeFrom = null;
                }
                e.InputParameters["hospitalSizeFrom"] = hospitalSizeFrom;
                e.InputParameters["optHospitalSizeTo"] = Convert.ToInt32(Request.QueryString["optHospitalSizeTo"]);
                string hospitalSizeTo = Request.QueryString["hospitalSizeTo"];
                if (hospitalSizeTo == string.Empty)
                {
                    hospitalSizeTo = null;
                }
                e.InputParameters["hospitalSizeTo"] = hospitalSizeTo;

                string countryId = Request.QueryString["countryId"];
                if (countryId == "0")
                {
                    countryId = null;
                }
                e.InputParameters["countryId"] = countryId;
                string stateId = Request.QueryString["stateId"];
                if (stateId == "0")
                {
                    stateId = null;
                }
                e.InputParameters["stateId"] = stateId;
                //e.InputParameters["countryId"] = 0;
                //e.InputParameters["stateId"] = 0;
                e.InputParameters["activities"] = Request.QueryString["activities"];
                e.InputParameters["value"] = Request.QueryString["value"];
                e.InputParameters["others"] = Request.QueryString["others"];
                e.InputParameters["location"] = Request.QueryString["location"];


                string dataPointsFrom = Request.QueryString["dataPointsFrom"];
                if (dataPointsFrom == string.Empty)
                {
                    dataPointsFrom = null;
                }
                e.InputParameters["dataPointsFrom"] = dataPointsFrom;

                e.InputParameters["optDataPointsFrom"] = Convert.ToInt32(Request.QueryString["optDataPointsFrom"]);

                string dataPointsTo = Request.QueryString["dataPointsTo"];
                if (dataPointsTo == string.Empty)
                {
                    dataPointsTo = null;
                }
                e.InputParameters["dataPointsTo"] = dataPointsTo;
                e.InputParameters["optdataPointsTo"] = Convert.ToInt32(Request.QueryString["optDataPointsTo"]);


                string configName = Request.QueryString["configName"];
                if (configName == string.Empty)
                {
                    configName = null;
                }
                e.InputParameters["configName"] = configName;

            }
            catch (Exception ex)
            {
                LogManager._stringObject = "ReportTimeRN.ascx ----";
                LogManager.SetExceptionDetails(ex, LogManager._stringObject, null);
                LogManager.LogException(ex, LogManager.LoggingCategory.General, LogManager.LoggingLevel.Error);
                CommonClass.Show(LogManager.ShowErrorDetail(ex));
            }
        }

        protected void ImageButtonBack_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                string filter = Request.QueryString["filter"];
                string beginingDate = Request.QueryString["beginingDate"];
                string endingDate = Request.QueryString["endingDate"];
                string value = Request.QueryString["value"];
                string others = Request.QueryString["others"];
                string location = Request.QueryString["location"];
                string activities = Request.QueryString["activities"];
                string hospitalName = Request.QueryString["hospitalName"];
                string hospitalUnitName = Request.QueryString["hospitalUnitName"];
                //ImageButtonBack.PostBackUrl = "~/Administrator/HospitalBenchmark.aspx?" + "&filter=" + filter + "&beginingDate=" + beginingDate + "&endingDate=" + endingDate + "&value=" + value + "&others=" + others + "&location=" + location;
                if (HttpContext.Current.User.IsInRole("superadmin"))
                {
                    Response.Redirect("~/Administrator/TimeRN.aspx?Report=Dashboard" + "&filter=" + filter + "&beginingDate=" + beginingDate + "&endingDate=" + endingDate + "&value=" + value + "&others=" + others + "&activities=" + activities + "&location=" + location + "&hospitalName=" + hospitalName + "&hospitalUnitName=" + hospitalUnitName, false);
                }
                else
                {
                    Response.Redirect("~/Users/TimeRN.aspx?Report=Dashboard" + "&filter=" + filter + "&beginingDate=" + beginingDate + "&endingDate=" + endingDate + "&value=" + value + "&others=" + others + "&activities=" + activities + "&location=" + location + "&hospitalName=" + hospitalName + "&hospitalUnitName=" + hospitalUnitName, false);
                }
            }
            catch (Exception ex)
            {
                LogManager._stringObject = "ReportTimeRN.ascx ----";
                LogManager.SetExceptionDetails(ex, LogManager._stringObject, null);
                LogManager.LogException(ex, LogManager.LoggingCategory.General, LogManager.LoggingLevel.Error);
                CommonClass.Show(LogManager.ShowErrorDetail(ex));
            }
        }

        protected void LinkButtonExportReport_Click(object sender, EventArgs e)
        {
            try
            {
                string filterName = Request.QueryString["filter"].Replace(" ", "");
                string fileName = "Attachment; FileName = MontlySummaryReport(";
                Response.Clear();
                if (filterName.Contains(">"))
                {
                    filterName = filterName.Replace(">", "gt");
                }
                if (filterName.Contains("<"))
                {
                    filterName = filterName.Replace("<", "lt");
                }
                fileName = fileName + filterName + "," + Request.QueryString["beginingDate"].Replace(" ", "") + "-" + Request.QueryString["endingDate"].Replace(" ", "") + ")" + ".xls";

                //fileName = fileName + ".xls";

                Response.AddHeader("Content-Disposition", fileName);
                Response.Charset = "";
                Response.Cache.SetCacheability(HttpCacheability.NoCache);
                Response.ContentType = "Application/ms-excel";
                System.IO.StringWriter stringWrite = new System.IO.StringWriter();
                System.Web.UI.HtmlTextWriter htmlWrite = new HtmlTextWriter(stringWrite);

                //LabelHospitalName.RenderControl(htmlWrite);
                //LabelHospitalUnitName.RenderControl(htmlWrite);
                //if (!LabelFilter.Text.Contains("No Filter"))
                //{
                //    LabelFilter.RenderControl(htmlWrite);
                //}

                GridViewReport.RenderControl(htmlWrite);
                GridViewFunctionReport.RenderControl(htmlWrite);
                //ChartJan.RenderControl(htmlWrite);
                Response.Write(stringWrite.ToString());
                Response.End();
            }
            catch (Exception ex)
            {
                LogManager._stringObject = "ReportTimeRNGrid.ascx ----";
                LogManager.SetExceptionDetails(ex, LogManager._stringObject, null);
                LogManager.LogException(ex, LogManager.LoggingCategory.General, LogManager.LoggingLevel.Error);
                CommonClass.Show(LogManager.ShowErrorDetail(ex));
            }
        }

        protected void GridViewReport_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    //DataRowView drv = (DataRowView)e.Row.DataItem;

                    //for (int i = 1; i < drv.DataView.Table.Columns.Count; i++)
                    //{
                    //    e.Row.Cells[i].BackColor = System.Drawing.Color.Green;
                    //}
                    //e.Row.Cells[0].BackColor = System.Drawing.ColorTranslator.FromHtml("#80ffff");
                    e.Row.Cells[0].BackColor = System.Drawing.ColorTranslator.FromHtml("#06569d");
                    e.Row.Cells[0].ForeColor = System.Drawing.Color.White;
                    e.Row.Cells[0].Font.Bold = true;
                }
                if (e.Row.RowType == DataControlRowType.Header)
                {
                    //e.Row.Cells[0].Width = 100;
                    //e.Row.Cells[1].Width = 40;
                }
            }
            catch (Exception ex)
            {
                LogManager._stringObject = "ReportTimeRNGrid.ascx ---- Page_Load";
                LogManager.SetExceptionDetails(ex, LogManager._stringObject, null);
                LogManager.LogException(ex, LogManager.LoggingCategory.General, LogManager.LoggingLevel.Error);
                CommonClass.Show(LogManager.ShowErrorDetail(ex));
            }

        }

        protected void GridViewFunctionReport_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    //DataRowView drv = (DataRowView)e.Row.DataItem;

                    //for (int i = 1; i < drv.DataView.Table.Columns.Count; i++)
                    //{
                    //    e.Row.Cells[i].BackColor = System.Drawing.Color.Green;
                    //}
                    //e.Row.Cells[0].BackColor = System.Drawing.ColorTranslator.FromHtml("#80ffff");
                    e.Row.Cells[0].BackColor = System.Drawing.ColorTranslator.FromHtml("#06569d");
                    e.Row.Cells[0].ForeColor = System.Drawing.Color.White;
                    e.Row.Cells[0].Font.Bold = true;
                    e.Row.Cells[0].ColumnSpan = 2;
                    //if (e.Row.Cells.Count >= 21)
                    //{
                    //    e.Row.Cells[0].Width = 444;
                    //}
                    //else if (e.Row.Cells.Count >= 13 && e.Row.Cells.Count < 21)
                    if (e.Row.Cells.Count >= 13)
                    {
                        e.Row.Cells[0].Width = 344;
                    }
                    else if (e.Row.Cells.Count > 5 && e.Row.Cells.Count < 13)
                    {
                        e.Row.Cells[0].Width = 264;
                    }
                    else
                    { e.Row.Cells[0].Width = 160; }
                }
                if (e.Row.RowType == DataControlRowType.Header)
                {
                    ////e.Row.Cells[1].Text = "";
                    e.Row.Cells[0].ColumnSpan = 2;

                    //if (e.Row.Cells.Count >= 21)
                    //{
                    //    e.Row.Cells[0].Width = 444;
                    //}
                    //else if (e.Row.Cells.Count >= 13 && e.Row.Cells.Count < 21)
                    if (e.Row.Cells.Count >= 13)
                    {
                        e.Row.Cells[0].Width = 344;
                    }
                    else if (e.Row.Cells.Count > 5 && e.Row.Cells.Count < 13)
                    {
                        e.Row.Cells[0].Width = 264;
                    }
                    else
                    { e.Row.Cells[0].Width = 160; }
                }
            }
            catch (Exception ex)
            {
                LogManager._stringObject = "ReportTimeRNGrid.ascx ---- Page_Load";
                LogManager.SetExceptionDetails(ex, LogManager._stringObject, null);
                LogManager.LogException(ex, LogManager.LoggingCategory.General, LogManager.LoggingLevel.Error);
                CommonClass.Show(LogManager.ShowErrorDetail(ex));
            }
        }

        protected void LinkButtonSaveImage_Click(object sender, EventArgs e)
        {
            try
            {
                Response.Redirect("SaveImageAtClientSide.aspx", false);
            }
            catch (Exception ex)
            {
                LogManager._stringObject = "ReportTimeRNGrid.ascx ----";
                LogManager.SetExceptionDetails(ex, LogManager._stringObject, null);
                LogManager.LogException(ex, LogManager.LoggingCategory.General, LogManager.LoggingLevel.Error);
                CommonClass.Show(LogManager.ShowErrorDetail(ex));
            }
        }

    }
}