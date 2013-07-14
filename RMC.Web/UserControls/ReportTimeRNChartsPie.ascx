<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ReportTimeRNChartsPie.ascx.cs"
 Inherits="RMC.Web.UserControls.WebUserControl2" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="MSCaptcha" Namespace="MSCaptcha" TagPrefix="cc11" %>

<%@ Register assembly="System.Web.DataVisualization, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" namespace="System.Web.UI.DataVisualization.Charting" tagprefix="asp" %>

<style type="text/css">
    .Background
    {
        position: fixed;
        left: 0;
        top: 0;
        z-index: 10;
        width: 100%;
        height: 100%;
        filter: alpha(opacity=60);
        opacity: 0.6;
        background-color: Black;
        visibility: hidden;
    }
</style>

    <script src="http://code.jquery.com/jquery-latest.min.js"
        type="text/javascript"></script>
    <script src="http://code.highcharts.com/highcharts.js"></script>
    <script src="http://code.highcharts.com/modules/exporting.js"></script>
    <style type="text/css">
        .head
        {
            color:Blue;
        }
        #ContentPlaceHolder1_ReportTimeRNChartsPie1_PanelHospitalUnitInformation
        {
            min-width:1000px !important;
            width:100% !important;
            display:inline-block;
            
        }
    </style>



<script type="text/javascript">
        var sessionName ="";
        var sessionValue ="";
        var sessionTitle="";

        var chart, chart1;
        function DrawChart(name, value, title) { 
            sessionName = name;
            sessionValue = value;
            sessionTitle = title;

            drawFirstLevel(name, value, title);
        }

        
        function drawFirstLevel(name, value, title){
        
            name = name.slice(1,-2);
            value = value.slice(1,-2);

            var array = name.split(",");  
            var valArray = value.split(",");

          
            // Build the chart
            chart = new Highcharts.Chart({
            chart: {
                renderTo: 'container',
                plotBackgroundColor: null,
                plotBorderWidth: null,
                plotShadow: false,
            },
            title: {
                text: '<div class="head">' + title + '</div>',
                margin: 75
            },
            tooltip: {
                pointFormat: '{series.name}: <b>{point.y}</b>',
                percentageDecimals: 1
            },
            plotOptions: {
                pie: {
                    allowPointSelect: true,
                    cursor: 'pointer',
                    dataLabels: {
                        enabled: true,
                        color: '#000000',
                        connectorColor: '#000000',
                        formatter: function() {
                            return '<b>'+ this.point.name +'</b>: '+ this.percentage.toFixed(2) +' %';
                        }
                    },
                    point: {
                        events: {
                            click: function() {
                                drawSecondLevel(name, value, title)
                            }
                        }
                    }
                }
            },
            scrollbar:{
                enabled:true
            },
         
            series: [{         
                type: 'pie',
                name: 'Claim Details',
                data: (function() {
                    // generate an array of random data
                    var data = [],
                        time = (new Date()).getTime(),
                        i;
    
                    for (i = 0; i < array.length; i++) {
                        data.push({
                            name: array[i],
                            y: parseFloat(valArray[i])
                           // color: 'red'
                        });
                    }
                    return data;
                })()
            }]
        });
        evt.preventDefault();
    }
 
 
 
 function drawSecondLevel(){ 
    var name, value, title;

    name = sessionName;
    value = sessionValue;
    title = sessionTitle;


    name = name.slice(1,-2);
    value = value.slice(1,-2);

    var array = name.split(",");  
    var valArray = value.split(",");

    var chartData = new Array();
    var chartData1 = new Array();
    for(var j = 0; j < valArray.length; j++)
    {           
        var data = parseFloat(parseFloat(valArray[j].toString()).toFixed(2));
        var data1 = array[j].toString();
        chartData[j] = data;
        chartData1[j] = data1;
    }


     chart.destroy();
     
     chart1=new Highcharts.Chart({
         chart: {
            renderTo: 'container',
                type:'column',
           },
           title: {
               text: title,
               margin: 75
           },
           
           plotOptions: {
                 column: {
                     dataLabels: {
                         enabled: true,
                         color: '#000000',
                         connectorColor: '#000000',
                         formatter: function() {
                             return '<b>'+ this.point.y +'</b> '+'%';
                         }
                     },
                     
                 }
             },
         xAxis: {
                  categories: chartData1,
                  min: 0,
                  labels: {
                    rotation: -45,
                    align: 'right'
                }

              },
        yAxis:{
                title:{
                text:'Claim Value'
                    }
              },
              scrollbar: {
                  enabled: true,
                height: 20
              },
          series: [{ 
            data : chartData
            }],
        legend:{
                  enabled:false
              }
     });

     return false;
 }
    




    function pageLoad() {
        
        var manager = Sys.WebForms.PageRequestManager.getInstance();

        manager.add_endRequest(endRequest);

        manager.add_beginRequest(onBeginRequest);

        $('#back').click(function () {
            chart1.destroy();
            drawFirstLevel(sessionName, sessionValue, sessionTitle);
        });
            

    }

    function onBeginRequest(sender, args) {
      
        var divParent = $get('ParentDiv');
        var divImage = $get('IMGDIV');

        divParent.className = 'Background';
        divParent.style.visibility = 'visible';
        divImage.style.visibility = 'visible';

    }

    function endRequest(sender, args) {

        var divParent = $get('ParentDiv');
        var divImage = $get('IMGDIV');

        divParent.className = '';
        divParent.style.visibility = 'hidden';
        divImage.style.visibility = 'hidden';
    }

    function checkListbox() {
        if (document.getElementById('<%=DropDownListHospitalName.ClientID %>').value == "0") {
            alert("Please Select Hospital From List");
            return false;
        }
        if (document.getElementById('<%=DropDownListHospitalUnit.ClientID %>').value == "0") {
            alert("Please Select Hospital Unit From List");
            return false;
        }
        var datefrom = document.getElementById('<%=DropDownListYearMonthFrom.ClientID %>').value;
        var dateto = document.getElementById('<%=DropDownListYearMonthTo.ClientID %>').value;
        var arDatefrom = new Array();
        var arDateto = new Array();
        arDatefrom = datefrom.split(',');
        arDateto = dateto.split(',');
        if (datefrom != "0" && dateto == "0") {
            alert("Please Select PeriodTo");
            return false;
        }
        if (dateto != "0" && datefrom == "0") {
            alert("Please Select PeriodFrom");
            return false;
        }
        if (datefrom != "0" && dateto != "0") {
            if (parseInt(arDatefrom[1]) == parseInt(arDateto[1])) {
                if (parseInt(arDatefrom[0]) > parseInt(arDateto[0])) {
                    alert("PeriodTo Cannot Be Less Then PeriodFrom");
                    return false;
                }
            }
            if (parseInt(arDatefrom[1]) > parseInt(arDateto[1])) {
                alert("PeriodTo Cannot Be Less Then PeriodFrom");
                return false;
            } 
        }
        if (document.getElementById('<%=DropDownListProfileCategory.ClientID %>').value == "0") {
            alert("Please Select Profile Category From List");
            return false;
        }
        if (document.getElementById('<%=DropDownListProfiles.ClientID %>').value == "0") {
            alert("Please Select Profiles From List");
            return false;
        }
    }
</script>

<script language="javascript" type="text/javascript">
    function saveImageAs(imgOrURL) {
        if (typeof imgOrURL == 'object')
            imgOrURL = imgOrURL.src;
        window.win = open(imgOrURL);
        setTimeout('win.document.execCommand("SaveAs")', 500);
    }

    function msg() {

        var imgOrURL = document.getElementById('embedImage');
        saveImageAs(imgOrURL);
    }

    function copyToClipboard() {

        var div = document.getElementById('divChart');
        div.contentEditable = 'true';
        var controlRange;
        if (document.body.createControlRange) {
            controlRange = document.body.createControlRange();
            controlRange.addElement(div);
            controlRange.execCommand('Copy');
        }
        div.contentEditable = 'false';
    }
</script>

<asp:ScriptManager ID="ScriptManagerReportTimeRNChartsPie" runat="server">
</asp:ScriptManager>

<script type="text/javascript" language="javascript">

    function windowPopup(page, title) {

        window.open(page, '_blank', 'height=355,width=710,top=150,left=150,status=no,toolbar=no,menubar=no,location=no,scrollbars=no,resizable=no,copyhistory=false');
        return false;
    }

    function refresh() {
        window.location.reload();
    }

</script>


<asp:Panel ID="PanelHospitalUnitInformation" runat="server">

    <div style="display:inline-block; width:100%; text-align:center; border:1px green solid; margin:0 auto;">
    <div style="display:inline-block; width:1003px; border:1px red solid;">
    
    



    <table width="100%">
        <tr>
            <th align="left" 
                style="font-size: 14px; padding-left: 10px; padding-top: 10px; color: #06569d;">
                <%--<u>Time RN Summary</u>--%>
                <asp:Label ID="LabelHeading" runat="server" Text="" Font-Underline="true"></asp:Label>
            </th>
            <th>
            </th>
            <th>
            </th>
        </tr>
        <tr>
        <td>
        <table>
        <tr>
                        <td style="padding-left: 20px;">
                            <div style="width: 20px; float: left; background-color: Transparent; z-index: 0;">
                                <div id="divErrorMsgInner" style="width: 418px; float: left; background-color: #E8E9EA;
                                    z-index: 10;">
                                    <div style="text-align: left; padding-left: 5px;">
                                        <asp:ValidationSummary ID="ValidationSummaryUserRegistration" ValidationGroup="Demographic"
                                            runat="server" DisplayMode="List" ShowMessageBox="true" ShowSummary="false" Font-Size="11px"
                                            Font-Bold="true" Style="padding-top: 1px;" />
                                        <asp:Panel ID="PanelErrorMsg" runat="server" Style="padding-top: 1px;" Visible="false">
                                            <ul>
                                                <li>
                                                    <asp:Label ID="LabelErrorMsg" runat="server" Font-Bold="true" Font-Size="11px"></asp:Label>
                                                </li>
                                            </ul>
                                        </asp:Panel>
                                    </div>
                                </div>
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td style="height: 10px;">
                        </td>
                    </tr>
                    <tr>
                        <td align="left">
                            <asp:UpdatePanel ID="UpdatePanelHospital" runat="server" ChildrenAsTriggers="true"
                                UpdateMode="Conditional">
                                <ContentTemplate>
                                    <table style="text-align:left;">
                                        <tr>
                                            <td align="right">
                                                <asp:Label ID="LabelHospitalName" runat="server" Text="Hospital Name" ForeColor="#06569d"
                                                    Font-Bold="true" Font-Size="11px"></asp:Label>
                                                <span id="spanHospitalName" style="color: Red;" runat="server">*</span>
                                            </td>
                                            <td align="left">
                                                <asp:DropDownList CssClass="aspDropDownList" ID="DropDownListHospitalName" ForeColor="#06569D"
                                                    runat="server" AppendDataBoundItems="True" AutoPostBack="true" OnSelectedIndexChanged="DropDownListHospitalName_SelectedIndexChanged"
                                                    TabIndex="1">
                                                    <asp:ListItem Value="0">Select Hospital Name</asp:ListItem>
                                                </asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidatorHospitalName" runat="server"
                                                    ControlToValidate="DropDownListHospitalName" SetFocusOnError="true" Display="None"
                                                    ErrorMessage="Must Select Hospital Name" InitialValue="0" ValidationGroup="Demographic">*</asp:RequiredFieldValidator>
                                            </td>
                                            <td style="width: 70px;">
                                            </td>
                                            <td align="right">
                                                <asp:Label ID="Label5" runat="server" Text="Hospital Unit" ForeColor="#06569d" Font-Bold="true"
                                                    Font-Size="11px"></asp:Label>
                                                <span id="spanHospitalUnit" style="color: Red;" runat="server">*</span>
                                            </td>
                                            <td align="left">
                                                <asp:DropDownList CssClass="aspDropDownList" ID="DropDownListHospitalUnit" ForeColor="#06569D"
                                                    runat="server" AppendDataBoundItems="True" DataSourceID="ObjectDataSourceHospitalUnit"
                                                    DataTextField="HospitalUnitName" DataValueField="HospitalDemographicID" TabIndex="2"
                                                    AutoPostBack="True" OnSelectedIndexChanged="DropDownListHospitalUnit_SelectedIndexChanged">
                                                    <asp:ListItem Value="0">Select Hospital Unit</asp:ListItem>
                                                </asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidatorHospitalUnit" runat="server"
                                                    ControlToValidate="DropDownListHospitalUnit" SetFocusOnError="true" Display="None"
                                                    ErrorMessage="Must Select Hospital Unit" InitialValue="0" ValidationGroup="Demographic">*</asp:RequiredFieldValidator>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="4" style="height: 5px;">
                                            </td>
                                        </tr>
                                        <%--<tr>
                                            <td align="right">
                                                <asp:Label ID="LabelYear" runat="server" Text="Year" ForeColor="#06569d" Font-Bold="true"
                                                    Font-Size="11px"></asp:Label>
                                                <span id="span1" style="color: Red;" runat="server">*</span>
                                            </td>
                                            <td align="left">
                                                <asp:DropDownList CssClass="aspDropDownList" ID="DropDownListYear" ForeColor="#06569D"
                                                    runat="server" AppendDataBoundItems="True" AutoPostBack="True" TabIndex="3" DataSourceID="ObjectDataSourceYear"
                                                    DataTextField="year" DataValueField="year" OnSelectedIndexChanged="DropDownListYear_SelectedIndexChanged">
                                                    <asp:ListItem Value="0">Select Year</asp:ListItem>
                                                </asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidatorYear" runat="server" ControlToValidate="DropDownListYear"
                                                    SetFocusOnError="true" Display="None" ErrorMessage="Must Select Year" InitialValue="0"
                                                    ValidationGroup="Demographic">*</asp:RequiredFieldValidator>
                                                <asp:ObjectDataSource ID="ObjectDataSourceYear" runat="server" SelectMethod="GetYear"
                                                    TypeName="RMC.BussinessService.BSReports">
                                                    <SelectParameters>
                                                        <asp:ControlParameter ControlID="DropDownListHospitalUnit" DefaultValue="0" Name="hospitalUnitId"
                                                            PropertyName="SelectedValue" Type="Int32" />
                                                    </SelectParameters>
                                                </asp:ObjectDataSource>
                                            </td>
                                            <td style="width: 70px;">
                                            </td>
                                            <td align="right">
                                                <asp:Label ID="Label4" runat="server" Text="Month" ForeColor="#06569d" Font-Bold="true"
                                                    Font-Size="11px"></asp:Label>
                                                <span id="span2" style="color: Red;" runat="server">*</span>
                                            </td>
                                            <td align="left">
                                                <asp:DropDownList CssClass="aspDropDownList" ID="DropDownListMonth" ForeColor="#06569D"
                                                    runat="server" AppendDataBoundItems="True" TabIndex="4" DataSourceID="ObjectDataSourceMonth"
                                                    DataTextField="month" DataValueField="monthIndex">
                                                    <asp:ListItem Value="0">Select Month</asp:ListItem>
                                                </asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidatorMonth" runat="server" ControlToValidate="DropDownListMonth"
                                                    SetFocusOnError="true" Display="None" ErrorMessage="Must Select Month" InitialValue="0"
                                                    ValidationGroup="Demographic">*</asp:RequiredFieldValidator>
                                                <asp:ObjectDataSource ID="ObjectDataSourceMonth" runat="server" SelectMethod="GetYearMonth"
                                                    TypeName="RMC.BussinessService.BSReports">
                                                    <SelectParameters>
                                                        <asp:ControlParameter ControlID="DropDownListHospitalUnit" DefaultValue="0" Name="hospitalUnitId"
                                                            PropertyName="SelectedValue" Type="Int32" />
                                                        <asp:ControlParameter ControlID="DropDownListYear" DefaultValue="0" Name="year" PropertyName="SelectedValue"
                                                            Type="String" />
                                                    </SelectParameters>
                                                </asp:ObjectDataSource>
                                            </td>
                                        </tr>--%>
                                        <tr>
                                            <td align="right">
                                                <asp:Label ID="LabelYearFrom" runat="server" Text="Period From" ForeColor="#06569d"
                                                    Font-Bold="true" Font-Size="11px"></asp:Label>
                                                &nbsp&nbsp
                                            </td>
                                            <td align="left">
                                                <asp:DropDownList CssClass="aspDropDownList" ID="DropDownListYearMonthFrom" ForeColor="#06569D"
                                                    runat="server" AppendDataBoundItems="True" AutoPostBack="true" TabIndex="8">
                                                    <asp:ListItem Value="0">Select Period From</asp:ListItem>
                                                </asp:DropDownList>
                                                <%if (DropDownListYearMonthFrom.SelectedValue == "0" && DropDownListYearMonthTo.SelectedValue != "0")
                                                  { %>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidatorYearMonthFrom" runat="server"
                                                    ControlToValidate="DropDownListYearMonthFrom" Display="None" ErrorMessage="Must Select Period From"
                                                    InitialValue="0" ValidationGroup="Demographic">*</asp:RequiredFieldValidator>
                                                <%} %>
                                            </td>
                                            <td style="width: 115px;">
                                            </td>
                                            <td align="right">
                                                <asp:Label ID="LabelYearTo" runat="server" Text="Period To" ForeColor="#06569d" Font-Bold="true"
                                                    Font-Size="11px"></asp:Label>
                                                &nbsp&nbsp
                                            </td>
                                            <td align="left">
                                                <asp:DropDownList CssClass="aspDropDownList" ID="DropDownListYearMonthTo" ForeColor="#06569D"
                                                    runat="server" AppendDataBoundItems="True" AutoPostBack="true" TabIndex="9">
                                                    <asp:ListItem Value="0">Select Period To</asp:ListItem>
                                                </asp:DropDownList>
                                                <%if (DropDownListYearMonthTo.SelectedValue == "0" && DropDownListYearMonthFrom.SelectedValue != "0")
                                                  { %>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidatorYearMonthTo" runat="server"
                                                    ControlToValidate="DropDownListYearMonthTo" Display="None" ErrorMessage="Must Select Period To"
                                                    InitialValue="0" ValidationGroup="Demographic">*</asp:RequiredFieldValidator>
                                                <%} %>
                                                <%--<asp:CompareValidator ID="CompareValidatorYearToYearFrom" runat="server" ControlToCompare="DropDownListYear"
                                                    ControlToValidate="DropDownListYearTo" ErrorMessage="Year To must be greater than or equal to Year From"
                                                    Operator="GreaterThanEqual" Type="Integer" Display="None" ValidationGroup="Demographic">*</asp:CompareValidator>--%>
                                            </td>
                                        </tr>
                                        <%--cm end--%>
                                    </table>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <div style="width: 100%; padding-top: 1px; padding-bottom: 1px;">
                                <hr style="height: 1px; color: #d6d6d6;" />
                            </div>
                        </td>
                    </tr>
                    <tr>

                        <td align="left">
                            <asp:UpdatePanel ID="UpdatePanelProfile" runat="server" ChildrenAsTriggers="true"
                                UpdateMode="Conditional">
                                <ContentTemplate>
                                    <table style="padding-right: 15px;">
                                        <tr>
                                            <td align="right">
                                                &nbsp;&nbsp;&nbsp;<asp:Label ID="Label1" runat="server" Text="Profile Category" ForeColor="#06569d"
                                                    Font-Bold="true" Font-Size="11px"></asp:Label><span id="span3" style="color: Red;"
                                                        runat="server">*</span>
                                            </td>
                                            <td align="left">
                                                <asp:DropDownList CssClass="aspDropDownList" ID="DropDownListProfileCategory" ForeColor="#06569D"
                                                    runat="server" AppendDataBoundItems="True" TabIndex="5" OnSelectedIndexChanged="DropDownListProfileCategory_SelectedIndexChanged"
                                                    AutoPostBack="True">
                                                    <asp:ListItem Value="0">Select Category</asp:ListItem>
                                                    <asp:ListItem Value="Value Added">Value Added</asp:ListItem>
                                                    <asp:ListItem Value="Others">Others</asp:ListItem>
                                                    <asp:ListItem Value="Location">Location</asp:ListItem>
                                                    <asp:ListItem Value="Database Values">Database Values</asp:ListItem>
                                                    <asp:ListItem Value="Activities">Activities</asp:ListItem>
                                                    <%--<asp:ListItem Value="Special Category">Special Category</asp:ListItem>--%>
                                                </asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidatorProfileCategory" runat="server"
                                                    ControlToValidate="DropDownListProfileCategory" SetFocusOnError="true" Display="None"
                                                    ErrorMessage="Must Select Profile Category" InitialValue="0" ValidationGroup="Demographic">*</asp:RequiredFieldValidator>
                                            </td>
                                            <td style="width: 100px;">
                                            </td>
                                            <td align="right">
                                                <asp:Label ID="Label2" runat="server" Text="Profiles" ForeColor="#06569d" Font-Bold="true"
                                                    Font-Size="11px"></asp:Label>
                                                <span id="span4" style="color: Red;" runat="server">*</span>
                                            </td>
                                            <td align="left">
                                                <asp:DropDownList CssClass="aspDropDownList" ID="DropDownListProfiles" ForeColor="#06569D"
                                                    runat="server" AppendDataBoundItems="True" TabIndex="6" DataSourceID="ObjectDataSourceProfiles"
                                                    DataTextField="ProfileName" DataValueField="ProfileTypeID">
                                                    <asp:ListItem Value="0">Select Profile</asp:ListItem>
                                                </asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidatorProfiles" runat="server" ControlToValidate="DropDownListProfiles"
                                                    SetFocusOnError="true" Display="None" ErrorMessage="Must Select Profile" InitialValue="0"
                                                    ValidationGroup="Demographic">*</asp:RequiredFieldValidator>
                                            </td>
                                        </tr>
                                    </table>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </td>
                    </tr>
                    <tr>
                        <td style="height: 25px;">
                        </td>
                    </tr>
                    <tr>
                        <td align="left" style="padding-left: 394px;">
                            <asp:Button ID="ButtonShowChart" runat="server" Text="Show Chart" ValidationGroup="Demographic"
                                CssClass="aspButton" Width="127px" Visible="true" OnClick="ButtonShowChart_Click" TabIndex="7" OnClientClick="return checkListbox();"/>
                            <%-- <asp:LinkButton ID="LinkButtonShowChart" runat="server" Text="Show Chart" ValidationGroup="Chart"
                    CssClass="aspLinkButton" Font-Size="11px" Width="90px" TabIndex="3" OnClick="LinkButtonShowChart_Click"></asp:LinkButton>--%>
                        </td>
                    </tr>
                    <tr>
                        <td style="height: 5px;">
                        </td>
                    </tr>
        </table>
        </td>
        </tr>
        <tr>
            <td align="center">
                
                <div id="container" style="min-width: auto; height: auto; margin: 0 auto"></div>
                <button id="back" type="button" onclick="drawFirstLevel()">Pie Chart</button>
                <button id="Button1" type="button" onclick="return drawSecondLevel()">Bar Chart</button>


                <table width="85%">
                    
                    <%--//chander--%>
                    <tr>
                        <td style="height: 5px;" align="center">
                            
                            <table>
                                <tr>
                                    <td style="width: 25px;">
                                    </td>
                                    <td>
                                        <asp:Panel ID="PanelProperties" runat="server">
                                            <fieldset>
                                                <legend style="font-size: 14px; padding-left: 10px; padding-top: 10px; color: #06569d;">
                                                    Chart</legend>
                                                <table>
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="LabelChartType" runat="server" Text="Chart Type" ForeColor="#06569d"
                                                                Font-Bold="true" Font-Size="11px"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:DropDownList ID="DropDownListChartType" runat="server" ForeColor="#06569D" CssClass="aspDropDownList"
                                                                AppendDataBoundItems="True" AutoPostBack="True" OnSelectedIndexChanged="DropDownListChartType_SelectedIndexChanged">
                                                                <asp:ListItem Value="Doughnut">Doughnut</asp:ListItem>
                                                                <asp:ListItem Value="Pie">Pie</asp:ListItem>
                                                            </asp:DropDownList>
                                                        </td>
                                                        <td style="width: 140px;" align="right">
                                                            <asp:Label ID="LabelDrawingStyle" runat="server" Text="Drawing Style" ForeColor="#06569d"
                                                                Font-Bold="true" Font-Size="11px"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:DropDownList ID="DropDownListDrawingStyle" runat="server" ForeColor="#06569D"
                                                                CssClass="aspDropDownList" AppendDataBoundItems="True" AutoPostBack="True" OnSelectedIndexChanged="DropDownListDrawingStyle_SelectedIndexChanged">
                                                                <asp:ListItem Value="Default">Default</asp:ListItem>
                                                                <asp:ListItem Value="SoftEdge">SoftEdge</asp:ListItem>
                                                                <asp:ListItem Value="Concave">Concave</asp:ListItem>
                                                            </asp:DropDownList>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="LabelLabelStyle" runat="server" Text="Label Style" ForeColor="#06569d"
                                                                Font-Bold="true" Font-Size="11px"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:DropDownList ID="DropDownListLabelStyle" runat="server" ForeColor="#06569D"
                                                                CssClass="aspDropDownList" AppendDataBoundItems="True" AutoPostBack="True" OnSelectedIndexChanged="DropDownListLabelStyle_SelectedIndexChanged">
                                                                <asp:ListItem Value="Disabled">Disabled</asp:ListItem>
                                                                <asp:ListItem Value="Inside">Inside</asp:ListItem>
                                                                <asp:ListItem Value="Outside">Outside</asp:ListItem>
                                                            </asp:DropDownList>
                                                        </td>
                                                        <td style="width: 140px;" align="right">
                                                            <asp:CheckBox ID="CheckBoxExplodedPoint" runat="server" CssClass="aspCheckBox" Text="Exploded Point"
                                                                ForeColor="#06569D" Font-Size="11px" Font-Bold="true" AutoPostBack="true" OnCheckedChanged="CheckBoxExplodedPoint_CheckedChanged" />
                                                        </td>
                                                        <td>
                                                            <asp:DropDownList ID="DropDownListProfileSubCategory" runat="server" ForeColor="#06569D"
                                                                CssClass="aspDropDownList" AppendDataBoundItems="True" AutoPostBack="True" DataSourceID="ObjectDataSourceProfileSubCategory"
                                                                DataTextField="ProfileCategoryName" DataValueField="ProfileCategoryName" OnSelectedIndexChanged="DropDownListProfileSubCategory_SelectedIndexChanged">
                                                            </asp:DropDownList>
                                                            <asp:ObjectDataSource ID="ObjectDataSourceProfileSubCategory" runat="server" SelectMethod="GetSubCategoryProfile"
                                                                TypeName="RMC.BussinessService.BSReports">
                                                                <SelectParameters>
                                                                    <asp:ControlParameter ControlID="DropDownListProfileCategory" DefaultValue="0" Name="profileCategory"
                                                                        PropertyName="SelectedValue" Type="String" />
                                                                </SelectParameters>
                                                            </asp:ObjectDataSource>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </fieldset>
                                        </asp:Panel>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    </div>
    </div>
</asp:Panel>
<asp:ObjectDataSource ID="ObjectDataSourceProfiles" runat="server" SelectMethod="GetProfiles"
    TypeName="RMC.BussinessService.BSReports">
    <SelectParameters>
        <asp:ControlParameter ControlID="DropDownListProfileCategory" DefaultValue="0" Name="profileCategory"
            PropertyName="SelectedValue" Type="String" />
    </SelectParameters>
</asp:ObjectDataSource>
<asp:ObjectDataSource ID="ObjectDataSourceHospitalUnit" runat="server" SelectMethod="GetHospitalDemographicDetailByHospitalID"
    TypeName="RMC.BussinessService.BSHospitalDemographicDetail">
    <SelectParameters>
        <asp:ControlParameter ControlID="DropDownListHospitalName" DefaultValue="0" Name="hospitalID"
            PropertyName="SelectedValue" Type="Int32" />
    </SelectParameters>
</asp:ObjectDataSource>
<div id="ParentDiv" class="Background" style="visibility: hidden;">
    <div id="IMGDIV" align="center" valign="middle" style="position: absolute; left: 45%;
        top: 50%; visibility: hidden; vertical-align: middle; border-style: none; border-color: black;
        z-index: 40;">
        <asp:Image ID="Image2" runat="server" ImageUrl="~/Images/Loading.gif" BorderWidth="0px"
            AlternateText="Loading"></asp:Image>
    </div>
</div>
<div style="visibility: hidden; display: none;">
    <asp:TextBox ID="TextBoxPostback" runat="server" AutoPostBack="true"></asp:TextBox>
</div>  

        
        </div>