using System;
using System.Collections;
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
using LogExceptions;
using System.Web.UI.DataVisualization.Charting;
using System.Drawing;
using System.IO;
using System.Web.UI.HtmlControls;
//using Microsoft.Office.Interop.Excel;
using Microsoft.Office;
using System.Text.RegularExpressions;
using System.Runtime.InteropServices;
using System.Reflection.Emit;
using XLExport;

namespace RMC.Web.UserControls
{
    public partial class UserAssessmentReport : System.Web.UI.UserControl
    {
       // int count = 0;
       
        protected void Page_Load(object sender, EventArgs e)
        {
            
            try
            {
                RMC.BussinessService.BSYear objectBSYear = new RMC.BussinessService.BSYear();
                string unitname = objectBSYear.getUnitname(Convert.ToInt32(Request.QueryString["hospitalUnitId"]));
                string hispitalname = objectBSYear.getHospitalname(Convert.ToInt32(Request.QueryString["hospitalUnitId"]));
                LabelFilter.Text = " Filter:- " + Request.QueryString["filter"] + ",";
                LabelMonthname.Text = " Period From:- " + Request.QueryString["yearMonthFrom"];
                LabelYearName.Text = " To " + Request.QueryString["yearMonthTo"];
                LabelHospitalname.Text = " Hospital:- " + hispitalname;
                LabelHospitalUnitname.Text = " Unit:- " + unitname;
            }
            catch (Exception ex)
            {
                ex.Data.Add("Page", "CreateNewProfile.ascx");
                LogManager._stringObject = "CreateNewProfile.ascx ---- Page_Load";
                LogManager.SetExceptionDetails(ex, LogManager._stringObject, null);
                LogManager.LogException(ex, LogManager.LoggingCategory.General, LogManager.LoggingLevel.Error);
                CommonClass.Show(LogManager.ShowErrorDetail(ex));
            }

            //showchart();

        }
        private void showchart()
        {
            
            DataTable dt = new DataTable();
            DataTable dtTopQuritile = (DataTable)Session["dtTopQuritile"];
            DataTable dtresult = (DataTable)Session["dtresult"];
            ///
            DataRow dr0 = dtresult.NewRow();
            DataRow dr1 = dtresult.NewRow();
            int col = dtresult.Columns.Count;
            int col1 = dtTopQuritile.Columns.Count;
         
            int k = 0;
            //merge table dtTopQuritile and dtresult, rows are actual , topQ, median
            for (int i = 0; i < col; i++)
            {

                for (int j = 1; j < col1; j++)
                {
                    if (dtresult.Columns[i].ToString() == dtTopQuritile.Columns[j].ToString())
                    {

                        // dtresult.Columns.Add(dtTopQuritile.Columns[j].ColumnName.ToString());
                        dr0[k] = (Convert.ToDecimal(dtTopQuritile.Rows[0][j]));
                        dr1[k] = (Convert.ToDecimal(dtTopQuritile.Rows[1][j]));
                        k += 1;
                        
                       
                    }
                }
            }
            dtresult.Rows.Add(dr0);
            dtresult.Rows.Add(dr1);
           // remove nagitive value from Qtr(1)
            for (int row = 1; row < dtresult.Rows.Count; row++)
            {
                for (int colm = 0; colm < dtresult.Columns.Count; colm++)
                {
                    //if (Math.Round(Convert.ToDouble(dtresult.Rows[row][colm]), 2) <= 0.00)
                    if (Math.Round(Convert.ToDouble(dtresult.Rows[0][colm]) - Convert.ToDouble(dtresult.Rows[row][colm]), 2) < 0.00)
                    {
                        dtresult.Columns.Remove(dtresult.Columns[colm]);
                        colm = colm - 1;
                        //dtresult.Rows[row][colm] = 0;
                    }
                }
            }
            //sorting function by cm
            ArrayList arrlist = new ArrayList();
            ArrayList arrlist1 = new ArrayList();
            Hashtable hs = new Hashtable();

            for (int i = 0; i < dtresult.Columns.Count; i++)
            {
                // hs.Add(dtresult.Columns[i].ColumnName.ToString(), dtresult.Rows[1][i].ToString());
                arrlist.Insert(i, (Convert.ToDouble(dtresult.Rows[0][i]) - Convert.ToDouble(dtresult.Rows[1][i])));
                arrlist1.Insert(i, (Convert.ToDouble(dtresult.Rows[0][i]) - Convert.ToDouble(dtresult.Rows[1][i])));
            }
            // arrlist = new ArrayList(hs.Values);
            // arrlist.Add();
            (arrlist).Sort();
            DataTable dtSorted = new DataTable();
            DataRow drs0 = dtSorted.NewRow();
            DataRow drs11 = dtSorted.NewRow();
            DataRow drs2 = dtSorted.NewRow();
            for (int i = 0; i < arrlist.Count; i++)
            {
                for (int i1 = 0; i1 < arrlist1.Count; i1++)
                {
                    if (arrlist1[i1].ToString() == arrlist[i].ToString())
                    {
                        string col22 = dtresult.Columns[i1].ColumnName.ToString();
                        dtSorted.Columns.Add(col22);
                        //arrlist1.Insert(i1, 0);
                        arrlist1[i1] = "-1";
                        break;
                    }
                }
            }

            //for (int y = 0; y < 3; y++)
            //{
            for (int i = 0; i < dtSorted.Columns.Count; i++)
            {
                for (int x = 0; x < dtresult.Columns.Count; x++)
                {
                    if (dtSorted.Columns[i].ColumnName.ToUpper() == dtresult.Columns[x].ColumnName.ToUpper())
                    {
                        drs0[i] = dtresult.Rows[0][x].ToString();
                        drs11[i] = dtresult.Rows[1][x].ToString();
                        drs2[i] = dtresult.Rows[2][x].ToString();
                    }
                }
            }
            dtSorted.Rows.Add(drs0);
            dtSorted.Rows.Add(drs11);
            dtSorted.Rows.Add(drs2);
            //end by cm



            for (int colm = 0; colm < dtSorted.Columns.Count; colm++)
            {
                dtSorted.Rows[0][colm] = Convert.ToDouble(dtSorted.Rows[0][colm]) - Convert.ToDouble(dtSorted.Rows[2][colm]);
                dtSorted.Rows[2][colm] = Convert.ToDouble(dtSorted.Rows[2][colm]) - Convert.ToDouble(dtSorted.Rows[1][colm]);
            }
            ////
           // dt = (DataTable)HttpContext.Current.Session["dtTopQuritile"];
            for (int colm = 0; colm < dtSorted.Columns.Count; colm++)
            {
                string colname = null;
                colname = dtSorted.Columns[colm].ColumnName.ToString();
                string[] strarr = colname.Split(new Char[] { '(' });
                if (colname.Contains("("))
                {
                    dtSorted.Columns[colm].ColumnName = strarr[0] + "(" + strarr[1];
                }
            }

            
            //}
             
            var a = dt; Random random = new Random();
            for (int i = 0; i < dtSorted.Columns.Count; i++)
            {
                Chart1.Series["Actual"].Points.Add(Convert.ToDouble(dtSorted.Rows[0][i].ToString()));
                Chart1.Series["Median"].Points.Add(Convert.ToDouble(dtSorted.Rows[2][i].ToString()));
                Chart1.Series["Top Quartile"].Points.Add(Convert.ToDouble(dtSorted.Rows[1][i].ToString()));

                Chart1.Series["Actual"].Points[i].AxisLabel = dtSorted.Columns[i].ToString();
                Chart1.Series["Median"].Points[i].AxisLabel = dtSorted.Columns[i].ToString();
                Chart1.Series["Top Quartile"].Points[i].AxisLabel = dtSorted.Columns[i].ToString();
                
            }
            //Chart1.Series["Actual"].IsValueShownAsLabel = true;
            //Chart1.Series["Median"].IsValueShownAsLabel = true;
            //Chart1.Series["Top Quartial"].IsValueShownAsLabel = true;
            //Chart1.Series["Actual"]["BarLabelStyle"] = "Outside";

            /////
            // Set Legend's visual attributes
           // Chart1.Legends["Default"].BackColor = Color.MediumSeaGreen;
           // Chart1.Legends["Default"].BackSecondaryColor = Color.Green;
           // Chart1.Legends["Default"].BackGradientStyle = GradientStyle.DiagonalLeft;

           // Chart1.Legends["Default"].BorderColor = Color.Black;
            //Chart1.Legends["Default"].BorderWidth = 2;
           // Chart1.Legends["Default"].BorderDashStyle = ChartDashStyle.Solid;

           // Chart1.Legends["Default"].ShadowOffset = 2;
            ////
            for (int colm = 0; colm < dtSorted.Columns.Count; colm++)
            {
                dtSorted.Rows[2][colm] = Convert.ToDouble(dtSorted.Rows[2][colm]) + Convert.ToDouble(dtSorted.Rows[1][colm]);
                dtSorted.Rows[0][colm] = Convert.ToDouble(dtSorted.Rows[0][colm]) + Convert.ToDouble(dtSorted.Rows[2][colm]);                
            }

            DataTable dtGridData = new DataTable();
            dtGridData.Columns.Add(".");
            for (int colm = 0; colm < dtSorted.Columns.Count; colm++)
            {
                dtGridData.Columns.Add(dtSorted.Columns[colm].ColumnName.ToString());
            }
            dtGridData.Columns.Add("Total Number of Minutes");
            dtGridData.Columns.Add("% Productivity Improvement");
            int k1=1;
            DataRow drs = dtGridData.NewRow();
            DataRow drs1 = dtGridData.NewRow();            
            drs[0] = "Top Quartile ";
            drs1[0] = "Median";
            for (int i = 1; i < dtGridData.Columns.Count; i++)
            {
                for (int j = 0; j < dtSorted.Columns.Count; j++)
                {
                    if (dtSorted.Columns[j].ToString() == dtGridData.Columns[i].ToString())
                    {                           
                       // drs[k1] = (Convert.ToDecimal(dtresult.Rows[1][j]));
                       // drs1[k1] = (Convert.ToDecimal(dtresult.Rows[2][j]));
                        drs[k1] = Math.Round(Convert.ToDecimal(dtSorted.Rows[0][j]) - Convert.ToDecimal(dtSorted.Rows[1][j]), 2);
                        drs1[k1] = Math.Round(Convert.ToDecimal(dtSorted.Rows[0][j]) - Convert.ToDecimal(dtSorted.Rows[2][j]), 2);
                        k1 += 1;


                    }
                }
            }
            dtGridData.Rows.Add(drs);
            dtGridData.Rows.Add(drs1);
            decimal val = 0;
            decimal val2 = 0;
            int c = 0;
            for (int i = 1; i < dtGridData.Columns.Count - 2; i++)
            {
                val = val + Convert.ToDecimal(dtGridData.Rows[0][i]);
                val2 = val2 + Convert.ToDecimal(dtGridData.Rows[1][i]);
                c = i+1;
            }
            dtGridData.Rows[0][c] = val;
            dtGridData.Rows[1][c] = val2;
            dtGridData.Rows[0][c + 1] = Math.Round((val / 720) * 100,2);
            dtGridData.Rows[1][c + 1] = Math.Round((val2 / 720) * 100,2);
            GridView1.DataSource = dtGridData;
            GridView1.DataBind();
        }
        protected void ObjectDataSource1_Selecting(object sender, ObjectDataSourceSelectingEventArgs e)
        {
            try
            {

                string dbValues = Request.QueryString["dbValues"];
                if (dbValues == string.Empty)
                {
                    dbValues = null;
                }
                e.InputParameters["dbValues"] = dbValues;

                string ProfileSubCategoryValue = Request.QueryString["ProfileSubCategoryValue"];
                if (ProfileSubCategoryValue == string.Empty)
                {
                    ProfileSubCategoryValue = null;
                }
                e.InputParameters["ProfileSubCategoryValue"] = ProfileSubCategoryValue;

                string ProfileCategoryValue = Request.QueryString["ProfileCategoryValue"];
                if (ProfileCategoryValue == string.Empty)
                {
                    ProfileCategoryValue = null;
                }
                e.InputParameters["ProfileCategoryValue"] = ProfileCategoryValue;

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
                e.InputParameters["activities"] = Request.QueryString["ProfileSubCategoryValue"];
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
                e.InputParameters["optdataPointsTo"] = Convert.ToInt32(Request.QueryString["optdataPointsTo"]);

                string configName = Request.QueryString["configName"];
                if (configName == string.Empty)
                {
                    configName = null;
                }
                e.InputParameters["configName"] = configName;
                //string filter = Request.QueryString["filter"];
                //if (filter == string.Empty)
                //{
                //    filter = null;
                //}
                //e.InputParameters["filter"] = filter;

            }
            catch (Exception ex)
            {
                ex.Data.Add("Page", "CreateNewProfile.ascx");
                LogManager._stringObject = "CreateNewProfile.ascx ---- Page_Load";
                LogManager.SetExceptionDetails(ex, LogManager._stringObject, null);
                LogManager.LogException(ex, LogManager.LoggingCategory.General, LogManager.LoggingLevel.Error);
                CommonClass.Show(LogManager.ShowErrorDetail(ex));
            }
        }

        protected void ObjectDataSource2_Selecting(object sender, ObjectDataSourceSelectingEventArgs e)
        {
            try
            {
                string dbValues = Request.QueryString["dbValues"];
                if (dbValues == string.Empty)
                {
                    dbValues = null;
                }
                e.InputParameters["dbValues"] = dbValues;

                string ProfileSubCategoryValue = Request.QueryString["ProfileSubCategoryValue"];
                if (ProfileSubCategoryValue == string.Empty)
                {
                    ProfileSubCategoryValue = null;
                }
                e.InputParameters["ProfileSubCategoryValue"] = ProfileSubCategoryValue;

                string ProfileCategoryValue = Request.QueryString["ProfileCategoryValue"];
                if (ProfileCategoryValue == string.Empty)
                {
                    ProfileCategoryValue = null;
                }
                e.InputParameters["ProfileCategoryValue"] = ProfileCategoryValue;
                //


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

                e.InputParameters["hospitalUnitID"] = null;
               // e.InputParameters["hospitalUnitID"] = Request.QueryString["hospitalUnitID"];
               

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
                e.InputParameters["activities"] = Request.QueryString["ProfileSubCategoryValue"];
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
                e.InputParameters["optdataPointsTo"] = Convert.ToInt32(Request.QueryString["optdataPointsTo"]);

                string configName = Request.QueryString["configName"];
                if (configName == string.Empty)
                {
                    configName = null;
                }
                e.InputParameters["configName"] = configName;

            }
            catch (Exception ex)
            {
                ex.Data.Add("Page", "CreateNewProfile.ascx");
                LogManager._stringObject = "CreateNewProfile.ascx ---- Page_Load";
                LogManager.SetExceptionDetails(ex, LogManager._stringObject, null);
                LogManager.LogException(ex, LogManager.LoggingCategory.General, LogManager.LoggingLevel.Error);
                CommonClass.Show(LogManager.ShowErrorDetail(ex));
            }
        }

        protected void ImageButtonBack_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                //string filter = Request.QueryString["filter"];
                //string beginingDate = Request.QueryString["beginingDate"];
                //string endingDate = Request.QueryString["endingDate"];
                //string value = Request.QueryString["value"];
                //string others = Request.QueryString["others"];
                //string location = Request.QueryString["location"];
                string filter = Request.QueryString["filter"];
                string year = Request.QueryString["yearFrom"];
                string month = Request.QueryString["monthFrom"];
                string hospitalid = Request.QueryString["hospitalType"];
                string hospitalunitid = Request.QueryString["hospitalUnitID"];
                string ProfileCategoryValue = Request.QueryString["ProfileCategoryValue"];
                string ProfileSubCategoryValue = Request.QueryString["ProfileSubCategoryValue"];

                //ImageButtonBack.PostBackUrl = "~/Administrator/HospitalBenchmark.aspx?" + "&filter=" + filter + "&beginingDate=" + beginingDate + "&endingDate=" + endingDate + "&value=" + value + "&others=" + others + "&location=" + location;
                if (HttpContext.Current.User.IsInRole("superadmin"))
                {
                   // Response.Redirect("~/Common/ReportUnitAssessment.aspx");
                    Response.Redirect("~/Common/ReportUnitAssessment.aspx?" + "&filter=" + filter + "&year=" + year + "&month=" + month + "&hospitalid=" + hospitalid + "&hospitalunitid=" + hospitalunitid + "&ProfileCategoryValue=" + ProfileCategoryValue + "&ProfileSubCategoryValue=" + ProfileSubCategoryValue, false);
                   // Response.Redirect("~/Administrator/HospitalBenchmark.aspx?" + "&filter=" + filter + "&beginingDate=" + beginingDate + "&endingDate=" + endingDate + "&value=" + value + "&others=" + others + "&location=" + location, false);
                }
                else
                {
                    Response.Redirect("~/Common/ReportUnitAssessment.aspx");
                  //  Response.Redirect("~/Users/HospitalBenchmark.aspx?" + "&filter=" + filter + "&beginingDate=" + beginingDate + "&endingDate=" + endingDate + "&value=" + value + "&others=" + others + "&location=" + location, false);

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
                string fileName = "Attachment; FileName = HospitalBenchmarkingSummary(";
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
                //Response.ContentType = "Application/vnd.xls";
                Response.ContentType = "Application/ms-excel";
                System.IO.StringWriter stringWrite = new System.IO.StringWriter();
                System.Web.UI.HtmlTextWriter htmlWrite = new HtmlTextWriter(stringWrite);

                //if (!LabelFilter.Text.Contains("No Filter"))
                //{
                //    LabelFilter.RenderControl(htmlWrite);
                //}
                //LabelBegigningDate.RenderControl(htmlWrite);
                //LabelEndingDate.RenderControl(htmlWrite);


                GridViewFunctionReport.RenderControl(htmlWrite);
                GridViewReport.RenderControl(htmlWrite);
                Response.Write(stringWrite.ToString());
                Response.End();
            }
            catch (Exception ex)
            {
                LogManager._stringObject = "ReportTimeRN.ascx ----";
                LogManager.SetExceptionDetails(ex, LogManager._stringObject, null);
                LogManager.LogException(ex, LogManager.LoggingCategory.General, LogManager.LoggingLevel.Error);
                CommonClass.Show(LogManager.ShowErrorDetail(ex));
            }
        }

        protected void LinkButtonExportFunctionReport_Click(object sender, EventArgs e)
        {
            //Response.Clear();
            //Response.AddHeader("Content-Disposition", "Attachment; FileName = FunctionReport.xls");
            //Response.Charset = "";
            //Response.Cache.SetCacheability(HttpCacheability.NoCache);
            //Response.ContentType = "Application/vnd.xls";
            //System.IO.StringWriter stringWrite = new System.IO.StringWriter();
            //System.Web.UI.HtmlTextWriter htmlWrite = new HtmlTextWriter(stringWrite);
            //GridViewFunctionReport.RenderControl(htmlWrite);
            //Response.Write(stringWrite.ToString());
            //Response.End();
        }

        protected void GridViewReport_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                //if (e.Row.RowType == DataControlRowType.DataRow)
                //{
                //    e.Row.Cells[0].BackColor = System.Drawing.ColorTranslator.FromHtml("#06569d");
                //    e.Row.Cells[0].ForeColor = System.Drawing.Color.White;
                //    e.Row.Cells[0].Font.Bold = true;

                //    //e.Row.Cells[0].Style.Add("padding", "2");
                //    //e.Row.Cells[1].Style.Add("padding", "2");
                //}
                //if (e.Row.RowType == DataControlRowType.Header)
                //{
                //    //e.Row.Cells[0].Visible = false;
                //}

                //for popup
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    e.Row.Cells[1].Visible = false;
                    e.Row.Cells[0].BackColor = System.Drawing.ColorTranslator.FromHtml("#06569d");
                    e.Row.Cells[2].BackColor = System.Drawing.ColorTranslator.FromHtml("#06569d");
                    e.Row.Cells[2].ForeColor = System.Drawing.Color.White;
                    e.Row.Cells[2].Font.Bold = true;

                    //HyperLink popupLink = new HyperLink();
                    //popupLink.NavigateUrl = "#";
                    //popupLink.Attributes.Add("onclick", String.Format("Javascript:window.open('dv.aspx?applicationid={0}', 'windowName', 'options');", e.Row.Cells[0].Text));
                    //e.Row.Cells[0].Controls.Add(popupLink);

                    LinkButton emailLink = new LinkButton();
                    emailLink = (LinkButton)(e.Row.Cells[1].FindControl("LinkButton1"));
                    emailLink.OnClientClick = "javascript:popup_window=window.open('SendMessage.aspx?id=" + e.Row.Cells[1].Text + "', 'popup_window', 'width=500,height=420,top=100,left=400,scrollbars=1');popup_window.focus()";
                    //emailLink.OnClientClick = "javascript:popup_window=window.open('SendMessage.aspx?id=" + "bharatg@smartdatainc.net" + "', 'popup_window', 'width=500,height=420,top=100,left=400,scrollbars=1');popup_window.focus()";


                }
                if (e.Row.RowType == DataControlRowType.Header)
                {
                    e.Row.Cells[1].Visible = false;
                    //e.Row.Cells[0].Visible = false;
                }
            }
            catch (Exception ex)
            {
                LogManager._stringObject = "ReportHospitalBenchmarkGrid.ascx ---- Page_Load";
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
                    e.Row.Cells[0].BackColor = System.Drawing.ColorTranslator.FromHtml("#06569d");
                    e.Row.Cells[1].BackColor = System.Drawing.ColorTranslator.FromHtml("#06569d");
                    e.Row.Cells[1].ForeColor = System.Drawing.Color.White;
                    e.Row.Cells[1].Font.Bold = true;
                    //e.Row.Cells[0].ColumnSpan = 4;
                    //e.Row.Cells[0].Width = 140;
                }
                if (e.Row.RowType == DataControlRowType.Header)
                {
                    //e.Row.Cells[1].Text = "";
                    //e.Row.Cells[0].ColumnSpan = 4;
                    //e.Row.Cells[0].Width = 140;
                }
            }
            catch (Exception ex)
            {
                LogManager._stringObject = "ReportHospitalBenchmarkGrid.ascx ---- Page_Load";
                LogManager.SetExceptionDetails(ex, LogManager._stringObject, null);
                LogManager.LogException(ex, LogManager.LoggingCategory.General, LogManager.LoggingLevel.Error);
                CommonClass.Show(LogManager.ShowErrorDetail(ex));
            }
        }

        public void chart()
        {
            // Populate series data
            Random random = new Random();
            for (int pointIndex = 0; pointIndex < 10; pointIndex++)
            {
                Chart1.Series["Series1"].Points.AddY(random.Next(45, 95));
            }

            // Set chart type
            Chart1.Series["Series1"].ChartType = SeriesChartType.StackedArea100;

            // Show point labels
            Chart1.Series["Series1"].IsValueShownAsLabel = true;

            // Disable X axis margin
            Chart1.ChartAreas["ChartArea1"].AxisX.IsMarginVisible = false;

            // Enable 3D
            Chart1.ChartAreas["ChartArea1"].Area3DStyle.Enable3D = true;

            // Set the first two series to be grouped into Group1
            Chart1.Series["LightBlue"]["StackedGroupName"] = "Group1";
            Chart1.Series["Gold"]["StackedGroupName"] = "Group1";

            // Set the last two series to be grouped into Group2
            Chart1.Series["Red"]["StackedGroupName"] = "Group2";
            Chart1.Series["DarkBlue"]["StackedGroupName"] = "Group2";

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            showchart();
        }

        protected void ObjectDataSource3_Selected(object sender, ObjectDataSourceStatusEventArgs e)
        {
            showchart();
            if (System.IO.File.Exists("~\\Uploads\\ChartImg" + CommonClass.UserInformation.UserID.ToString() + ".png"))
            {
                System.IO.File.Delete("~\\Uploads\\ChartImg" + CommonClass.UserInformation.UserID.ToString() + ".png");
            }
            Chart1.SaveImage(Server.MapPath("~\\Uploads\\ChartImg" + CommonClass.UserInformation.UserID.ToString() + ".png"));
        }

        private void ExportGridView()
        {

            string attachment = "attachment; filename=testReport.xls";

            Response.ClearContent();

            Response.AddHeader("content-disposition", attachment);

            Response.ContentType = "application/ms-excel";

            StringWriter sw = new StringWriter();

            HtmlTextWriter htw = new HtmlTextWriter(sw);



            // Create a form to contain the grid

            HtmlForm frm = new HtmlForm();
            // GridViewNPOlist .BottomPagerRow.Visible = false;
            GridView1.Parent.Controls.Add(frm);

            frm.Attributes["runat"] = "server";

            frm.Controls.Add(GridView1);



            frm.RenderControl(htw);

            //GridView1.RenderControl(htw);

            Response.Write(sw.ToString());
            //Response.Write(grd1);
            Response.End();
            // GridViewNPOlist.BottomPagerRow.Visible = false;

        }

        protected void LinkButton2_Click(object sender, EventArgs e)
        {
            //**code for export image to excel**//
            //string OutFilename = "Chart" + Guid.NewGuid();
            //string Chartpath = Server.MapPath("Excel") + "/ChartImg1.png" ;           
            //string InputExcelfilepath = Server.MapPath("Excel") + "/myExcelFile.xls";
            //string OutputExcelfilepath = Server.MapPath("Excel") + "/" + OutFilename + ".xls";            
            //XLExport.XLExport.ExportFromTemplate(Chartpath, InputExcelfilepath, OutputExcelfilepath, "B2");

           ExportGridView();
        }      

        protected void LinkButton3_Click(object sender, EventArgs e)
        {
            try
            {
                 Response.Redirect("~/Administrator/SaveImageAtClientSide.aspx", false);
            }
            catch (Exception ex)
            {
                LogManager._stringObject = "ReportTimeRNChartsPie.ascx ----";
                LogManager.SetExceptionDetails(ex, LogManager._stringObject, null);
                LogManager.LogException(ex, LogManager.LoggingCategory.General, LogManager.LoggingLevel.Error);
                CommonClass.Show(LogManager.ShowErrorDetail(ex));
            }
        }

    }
}