using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using System.Web;

namespace RMC.BussinessService
{
    public class BSImportXMLExcelFile
    {

        #region Public Methods

        public void GenerateXMLExcelFile(string path)
        {
            XNamespace mainNamespace = XNamespace.Get("urn:schemas-microsoft-com:office:spreadsheet");
            XNamespace o = XNamespace.Get("urn:schemas-microsoft-com:office:office");
            XNamespace x = XNamespace.Get("urn:schemas-microsoft-com:office:excel");
            XNamespace ss = XNamespace.Get("urn:schemas-microsoft-com:office:spreadsheet");
            XNamespace html = XNamespace.Get("http://www.w3.org/TR/REC-html40");

            using (RMC.DataService.RMCDataContext ctx = new RMC.DataService.RMCDataContext())
            {
                var dataToShow = from c in ctx.Validations
                                 select new
                                 {
                                     Location = c.LocationID > 0 ? (from l in ctx.Locations
                                                                    where l.LocationID == c.LocationID
                                                                    select l).FirstOrDefault().Location1 : string.Empty,
                                     Activity = c.ActivityID > 0 ? (from a in ctx.Activities
                                                                    where a.ActivityID == c.ActivityID
                                                                    select a).FirstOrDefault().Activity1 : string.Empty,
                                     SubActivity = c.SubActivityID > 0 ? (from sa in ctx.SubActivities
                                                                          where sa.SubActivityID == c.SubActivityID
                                                                          select sa).FirstOrDefault().SubActivity1 : string.Empty,
                                 };

                var headerRow = from p in dataToShow.ToList().GetType().GetProperties()
                                select new XElement(mainNamespace + "Cell",
                                    new XElement(mainNamespace + "Data",
                                        new XAttribute(ss + "Type", "String"),
                                        p.Name=="Capacity" ? "Location" : (p.Name=="Count")? "Activity" : "SubActivity"
                                      )
                                  );


                XDocument xdoc = new XDocument(new XDeclaration("1.0", "utf-8", "yes"));
                XElement workbook = new XElement(mainNamespace + "Workbook",
                    new XAttribute(XNamespace.Xmlns + "html", html),
                    CreateNamespaceAtt(XName.Get("ss", "http://www.w3.org/2000/xmlns/"), ss),
                    CreateNamespaceAtt(XName.Get("o", "http://www.w3.org/2000/xmlns/"), o),
                    CreateNamespaceAtt(XName.Get("x", "http://www.w3.org/2000/xmlns/"), x),
                    CreateNamespaceAtt(mainNamespace),
                    new XElement(o + "DocumentProperties",
                        CreateNamespaceAtt(o),
                        new XElement(o + "Author", "Davinder Kumar"),
                        new XElement(o + "LastAuthor", "Davinder Kumar"),
                        new XElement(o + "Created", DateTime.Now.ToString())
                    ), //end document properties
                    new XElement(x + "ExcelWorkbook",
                        CreateNamespaceAtt(x),
                        new XElement(x + "WindowHeight", 12750),
                        new XElement(x + "WindowWidth", 24855),
                        new XElement(x + "WindowTopX", 240),
                        new XElement(x + "WindowTopY", 75),
                        new XElement(x + "ProtectStructure", "False"),
                        new XElement(x + "ProtectWindows", "False")
                    ), //end ExcelWorkbook
                    new XElement(mainNamespace + "Styles",
                        new XElement(mainNamespace + "Style",
                            new XAttribute(ss + "ID", "Default"),
                            new XAttribute(ss + "Name", "Normal"),
                            new XElement(mainNamespace + "Alignment",
                                new XAttribute(ss + "Vertical", "Bottom")
                            ),
                            new XElement(mainNamespace + "Borders"),
                            new XElement(mainNamespace + "Font",
                                new XAttribute(ss + "FontName", "Calibri"),
                                new XAttribute(x + "Family", "Swiss"),
                                new XAttribute(ss + "Size", "11"),
                                new XAttribute(ss + "Color", "#000000")
                            ),
                            new XElement(mainNamespace + "Interior"),
                            new XElement(mainNamespace + "NumberFormat"),
                            new XElement(mainNamespace + "Protection")
                        ),
                        new XElement(mainNamespace + "Style",
                            new XAttribute(ss + "ID", "Header"),
                            new XElement(mainNamespace + "Font",
                                new XAttribute(ss + "FontName", "Calibri"),
                                new XAttribute(x + "Family", "Swiss"),
                                new XAttribute(ss + "Size", "13"),
                                new XAttribute(ss + "Color", "#000000"),
                                new XAttribute(ss + "Bold", "1")
                            )
                        )
                    ), // close styles
                    new XElement(mainNamespace + "Worksheet",
                        new XAttribute(ss + "Name", "ValidationTable"),
                        new XElement(mainNamespace + "Table",
                            new XAttribute(ss + "ExpandedColumnCount", headerRow.Count()),
                            new XAttribute(ss + "ExpandedRowCount", dataToShow.Count() + 1),
                            new XAttribute(x + "FullColumns", 1),
                            new XAttribute(x + "FullRows", 1),
                            new XAttribute(ss + "DefaultRowHeight", 15),
                            new XElement(mainNamespace + "Column",
                                new XAttribute(ss + "Width", 81)
                            ),
                            new XElement(mainNamespace + "Row",
                                new XAttribute(ss + "StyleID", "Header"),
                                headerRow
                            ),
                            dataToShow.Select(e =>
                                new XElement(mainNamespace + "Row",
                                    new XAttribute(ss + "StyleID", "Default"),
                                    new XElement(mainNamespace + "Cell",
                                        new XElement(mainNamespace + "Data",
                                            new XAttribute(ss + "Type", "String"),
                                            e.Location
                                        )
                                    ),
                                    new XElement(mainNamespace + "Cell",
                                        new XElement(mainNamespace + "Data",
                                            new XAttribute(ss + "Type", "String"),
                                            e.Activity
                                        )
                                    ),
                                    new XElement(mainNamespace + "Cell",
                                        new XElement(mainNamespace + "Data",
                                            new XAttribute(ss + "Type", "String"),
                                            e.SubActivity
                                        )
                                    )
                                )
                            )
                        ), //close table
                        new XElement(x + "WorksheetOptions",
                            CreateNamespaceAtt(x),
                            new XElement(x + "PageSetup",
                                new XElement(x + "Header",
                                    new XAttribute(x + "Margin", "0.3")
                                ),
                                new XElement(x + "Footer",
                                    new XAttribute(x + "Margin", "0.3")
                                ),
                                new XElement(x + "PageMargins",
                                    new XAttribute(x + "Bottom", "0.75"),
                                    new XAttribute(x + "Left", "0.7"),
                                    new XAttribute(x + "Right", "0.7"),
                                    new XAttribute(x + "Top", "0.75")
                                )
                            ),
                            new XElement(x + "Print",
                                new XElement(x + "ValidPrinterInfo"),
                                new XElement(x + "HorizontalResolution", 600),
                                new XElement(x + "VerticalResolution", 600)
                            ),
                            new XElement(x + "Selected"),
                            new XElement(x + "Panes",
                                new XElement(x + "Pane",
                                    new XElement(x + "Number", 3),
                                    new XElement(x + "ActiveRow", 6),
                                    new XElement(x + "ActiveCol", 1)
                                )
                            ),
                            new XElement(x + "ProtectObjects", "False"),
                            new XElement(x + "ProtectScenarios", "False")
                        ) // close worksheet options
                    ) // close Worksheet
                );
                //HttpContext.Current.Response.Write("Going to save to path:"+path);
                xdoc.Add(workbook);
                //HttpContext.Current.Response.Write("Workbook added to response");
                xdoc.Save(path);
                //HttpContext.Current.Response.Write("Saved"+path);
            }
        } 
        
        #endregion

        #region Private Methods
        
        private static XAttribute CreateNamespaceAtt(XNamespace ns)
        {
            return CreateNamespaceAtt(XName.Get("xmlns", ""), ns);
        }

        private static XAttribute CreateNamespaceAtt(XName name, XNamespace ns)
        {
            XAttribute ssAtt = new XAttribute(name, ns.NamespaceName);
            ssAtt.AddAnnotation(ns);
            return ssAtt;
        } 

        #endregion

    }
}
