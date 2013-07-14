using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI.DataVisualization.Charting;

namespace RMC.BussinessService
{
    public class BSChartControl
    {

        #region Variables

        List<Series> objectGenericSeries = null;

        #endregion

        #region Public Menthods

        /// <summary>
        /// Generate Series Object For Chart Control.
        /// </summary>
        /// <param name="markerStyle"></param>
        /// <param name="markerSize"></param>
        /// <param name="markerColor"></param>
        /// <param name="chartType"></param>
        /// <param name="lineWidth"></param>
        /// <param name="lineColor"></param>
        /// <param name="shadowOffSet"></param>
        /// <param name="objectGenericBEReports">Use Line Chart Method From BSReport</param>
        /// <returns></returns>
        public List<Series> GetLineChartSeries(string markerStyle, int? markerSize, string markerColor, string chartType, int? lineWidth, string lineColor, int? shadowOffSet, string labelStyle, List<RMC.BusinessEntities.BEReports> objectGenericBEReports, string databaseValue)
        {
            try
            {
                if (markerStyle == null)
                {
                    markerStyle = "Diamond";
                }

                if (markerSize == null)
                {
                    markerSize = 6;
                }

                if (markerColor == null)
                {
                    markerColor = "Blue";
                }

                if (chartType == null)
                {
                    chartType = "Line";
                }

                if (lineWidth == null)
                {
                    lineWidth = 2;
                }

                if (lineColor == null)
                {
                    lineColor = "Blue";
                }

                if (shadowOffSet == null)
                {
                    shadowOffSet = 1;
                }

                if (labelStyle == null)
                {
                    labelStyle = "None";
                }

                objectGenericSeries = new List<Series>();
                //if (objectGenericBEReports[1].Name == "Activity" && objectGenericBEReports[1].Name == "Sub-Activity" && objectGenericBEReports[1].Name == "Last Location" && objectGenericBEReports[1].Name == "Current Location" && objectGenericBEReports[1].Name == "Staffing Model")
                if (objectGenericBEReports != null)
                {
                    if (objectGenericBEReports.Count > 0)
                    {
                        //if (objectGenericBEReports[1].Name == "Last Location" || objectGenericBEReports[1].Name == "Current Location" || objectGenericBEReports[1].Name == "Activity" || objectGenericBEReports[1].Name == "Sub-Activity" || objectGenericBEReports[1].Name == "Cognitive" || objectGenericBEReports[1].Name == "Staffing Model")
                        //{
                        //    foreach (var objectVar in objectGenericBEReports.GroupBy(o => o.Name).ToList())
                        //    {

                        //        List<RMC.BusinessEntities.BEReports> objectNewGenericBEReports = objectVar.ToList<RMC.BusinessEntities.BEReports>();
                        //        Series series = new Series(objectVar.Key);

                        //        //set Dynamic
                        //        if (objectVar.Key != "Minimum" && objectVar.Key != "Maximum" && objectVar.Key != "Median" && objectVar.Key != "Average" && objectVar.Key != "Quartile(1)" && objectVar.Key != "Quartile(3)")
                        //        {
                        //            series.BorderDashStyle = ChartDashStyle.Solid;
                        //            series.MarkerStyle = (MarkerStyle)MarkerStyle.Parse(typeof(MarkerStyle), markerStyle);
                        //            series.MarkerSize = markerSize.Value;
                        //            series.MarkerColor = System.Drawing.Color.FromName(markerColor);
                        //            series.Color = System.Drawing.Color.FromName(lineColor);

                        //            if (labelStyle != "None")
                        //            {
                        //                series.IsValueShownAsLabel = true;

                        //                if (labelStyle != "Auto")
                        //                {
                        //                    series["LabelStyle"] = labelStyle;
                        //                }
                        //            }
                        //        }
                        //        else
                        //        {
                        //            series.BorderDashStyle = ChartDashStyle.Dash;
                        //        }
                        //        series.ChartType = (SeriesChartType)SeriesChartType.Parse(typeof(SeriesChartType), chartType);
                        //        series.BorderWidth = lineWidth.Value;
                        //        series.ShadowOffset = shadowOffSet.Value;

                        //        foreach (RMC.BusinessEntities.BEReports objectBEReports in objectNewGenericBEReports)
                        //        {
                        //            //series.Points.AddXY(objectBEReports.MonthName, objectBEReports.ValuesSum);
                        //            series.Points.AddXY(objectBEReports.ColumnName, objectBEReports.ValuesSum);
                        //        }

                        //        objectGenericSeries.Add(series);
                        //    }
                        //}
                        //else
                        //{
                        foreach (var objectVar in objectGenericBEReports.GroupBy(o => o.ColumnName).ToList())
                        {

                            List<RMC.BusinessEntities.BEReports> objectNewGenericBEReports = objectVar.ToList<RMC.BusinessEntities.BEReports>();
                            Series series = new Series(objectVar.Key);

                            //set Dynamic
                            if (objectVar.Key != "Minimum" && objectVar.Key != "Maximum" && objectVar.Key != "Median" && objectVar.Key != "Average" && objectVar.Key != "Quartile(1)" && objectVar.Key != "Quartile(3)")
                            {
                                series.BorderDashStyle = ChartDashStyle.Solid;
                                series.MarkerStyle = (MarkerStyle)MarkerStyle.Parse(typeof(MarkerStyle), markerStyle);
                                series.MarkerSize = markerSize.Value;
                                series.MarkerColor = System.Drawing.Color.FromName(markerColor);
                                series.Color = System.Drawing.Color.FromName(lineColor);

                                if (labelStyle != "None")
                                {
                                    series.IsValueShownAsLabel = true;

                                    if (labelStyle != "Auto")
                                    {
                                        series["LabelStyle"] = labelStyle;
                                    }
                                }
                            }
                            else
                            {
                                series.BorderDashStyle = ChartDashStyle.Dash;
                            }
                            series.ChartType = (SeriesChartType)SeriesChartType.Parse(typeof(SeriesChartType), chartType);
                            series.BorderWidth = lineWidth.Value;
                            series.ShadowOffset = shadowOffSet.Value;

                            foreach (RMC.BusinessEntities.BEReports objectBEReports in objectNewGenericBEReports)
                            {
                                series.Points.AddXY(objectBEReports.MonthName, objectBEReports.ValuesSum);
                            }

                            objectGenericSeries.Add(series);
                        }
                        //}
                    }
                }
                if (objectGenericSeries.Count > 4)
                {
                    if (databaseValue == "Select Value")
                    {
                        objectGenericSeries.RemoveAt(4);
                    }

                    if (databaseValue != "Select Value")
                    {
                        //objectGenericSeries.RemoveAll(x => x.Name != databaseValue);
                        objectGenericSeries.RemoveAt(4);
                    }
                }


                return objectGenericSeries;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Fetch MarkerStyle of ChataControl
        /// </summary>
        /// <returns></returns>
        public List<RMC.BusinessEntities.BEDropDownListData> GetMarkerStyle()
        {
            try
            {
                List<RMC.BusinessEntities.BEDropDownListData> objectGenericMakerStyleNames = new List<RMC.BusinessEntities.BEDropDownListData>();
                string[] objectMarkerStyleName = Enum.GetNames(typeof(MarkerStyle));

                foreach (string name in objectMarkerStyleName)
                {
                    RMC.BusinessEntities.BEDropDownListData objectDDLData = new RMC.BusinessEntities.BEDropDownListData();
                    objectDDLData.Key = name;
                    objectDDLData.Value = name;

                    objectGenericMakerStyleNames.Add(objectDDLData);
                }

                return objectGenericMakerStyleNames;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Fetch Color Names From System.Drawing.
        /// </summary>
        /// <returns></returns>
        public List<RMC.BusinessEntities.BEDropDownListData> GetColor()
        {
            try
            {
                List<RMC.BusinessEntities.BEDropDownListData> objectGenericColorNames = new List<RMC.BusinessEntities.BEDropDownListData>();
                string[] objectColorNames = Enum.GetNames(typeof(System.Drawing.KnownColor));

                foreach (string name in objectColorNames)
                {
                    RMC.BusinessEntities.BEDropDownListData objectDDLData = new RMC.BusinessEntities.BEDropDownListData();
                    objectDDLData.Key = name;
                    objectDDLData.Value = name;

                    objectGenericColorNames.Add(objectDDLData);
                }

                return objectGenericColorNames;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Pie Chart.
        /// </summary>
        /// <param name="chartType"></param>
        /// <param name="drawingStyle"></param>
        /// <param name="labelStyle"></param>
        /// <param name="explodedPoint"></param>
        /// <param name="objectGenericBEReports"></param>
        /// <returns></returns>
        public Series GetPieChartSeries(string chartType, string drawingStyle, string labelStyle, string explodedPoint, List<RMC.BusinessEntities.BEReports> objectGenericBEReports)
        {
            try
            {
                //Pie, Doughnut.
                if (chartType == null)
                {
                    chartType = "Pie";
                }
                //Default, SoftEdge, Concave
                if (drawingStyle == null)
                {
                    drawingStyle = "SoftEdge";
                }
                //Disable, Inside, Outside
                if (labelStyle == null)
                {
                    labelStyle = "Disabled";
                }
                //Exploded (Boolean)
                if (explodedPoint == null)
                {
                    explodedPoint = "None";
                }

                Series series = new Series("Default");
                List<string> XValue = new List<string>();
                List<double> YValue = new List<double>();

               // foreach (var objectVar in objectGenericBEReports.OrderBy(x => x.ValuesSum).GroupBy(o => o.ColumnName).ToList())
                foreach (var objectVar in objectGenericBEReports.GroupBy(o => o.ColumnName).ToList())
                {
                    List<RMC.BusinessEntities.BEReports> objectNewGenericBEReports = objectVar.ToList<RMC.BusinessEntities.BEReports>();

                    XValue.Add(objectVar.Key);
                    YValue.Add(objectNewGenericBEReports.Sum(s => s.ValuesSum) / objectNewGenericBEReports.Count);
                }
                series.Points.DataBindXY(XValue, YValue);
                //series.Label = "#PERCENT{P}" + XValue;
                series.Label = "#PERCENT{P} #LEGENDTEXT";
                int index = 0;
                foreach (DataPoint dt in series.Points)
                {
                    dt.LegendText = XValue[index];
                    index++;
                }

                series["PieDrawingStyle"] = drawingStyle;
                series.BorderColor = System.Drawing.Color.Black;
                series.Color = System.Drawing.Color.Blue;
                series["PieLabelStyle"] = labelStyle;
                series["PieStartAngle"] = "270";
                series.ChartType = (SeriesChartType)SeriesChartType.Parse(typeof(SeriesChartType), chartType);
                //Exploded Point
                foreach (DataPoint point in series.Points)
                {
                    point["Exploded"] = "false";
                    if (point.AxisLabel == explodedPoint)
                    {
                        point["Exploded"] = "true";
                    }
                }

                return series;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public Series GetColumnChartSeries(string chartType, List<RMC.BusinessEntities.BEFunctionNames> objectGenericBEFunctionNames)
        {
            if (chartType == null)
            {
                chartType = "Column";
            }

            Series objectSeries = new Series();

            foreach (RMC.BusinessEntities.BEFunctionNames objectBEFunctionNames in objectGenericBEFunctionNames)
            {
                objectSeries.Points.AddXY(objectBEFunctionNames.ColumnName, objectBEFunctionNames.FunctionNameDouble);

            }

            objectSeries.ChartType = (SeriesChartType)SeriesChartType.Parse(typeof(SeriesChartType), chartType);
            //objectSeries.BorderDashStyle = ChartDashStyle.Dash;
            objectSeries.Color = System.Drawing.Color.MediumSlateBlue;

            objectSeries["DrawingStyle"] = "Emboss";    //Default, Emboss, Cylinder, Wedge, LightToDark
            objectSeries["LabelStyle"] = "Top";
            //objectSeries.IsValueShownAsLabel = true;
            //objectSeries.Label = "#VAL{P2}";
            objectSeries.LabelForeColor = System.Drawing.Color.Black;
            objectSeries["PointWidth"] = "0.3";
            objectSeries.LegendText = "Performance";

            return objectSeries;
        }

        #endregion

    }
}
