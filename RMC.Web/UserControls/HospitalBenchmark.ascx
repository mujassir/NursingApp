<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="HospitalBenchmark.ascx.cs"
    Inherits="RMC.Web.UserControls.RMCHospitalBenchmark" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="MSCaptcha" Namespace="MSCaptcha" TagPrefix="cc11" %>
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
<style type="text/css">
.popupClass1 { width:100px;height:300px;background-color:red; }

</style>


 <script type="text/javascript">
     function checkListbox()
     
      {
          //if (document.getElementById('<%=ListBoxSelectedProfiles.ClientID %>').outerText == "")         
         if ($('.listboxselect').children().length == 0) 
         {
             alert("Atleast One Profile Must Be Chosen");
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
     }
     function checkTextBoxNewFilterName() {        
         if (document.getElementById('<%=TextBoxNewFilterName.ClientID %>').value == "") {
             document.getElementById('<%=Labelerror.ClientID %>').innerHTML = "* Filter name should not blank";
             return false;
         }
         else {
             document.getElementById('<%=Labelerror.ClientID %>').innerHTML = "";
             $(".simplediv").hide();
          }
     }

     function PopupClose() {
         $(".simplediv").hide(); return false;
     }

     function getEmailChk() {
         
         var emails="";
         $(".chkGrid:checked").each(function () {
             if (emails != "") {
                 emails = emails + ",";
              }
              emails = emails + $(this).val();
          });
          if (emails != "") {
              javascript: popup_window = window.open('SendMessage.aspx?id=' + emails + '', 'popup_window', 'width=500,height=420,top=100,left=400,scrollbars=1'); popup_window.focus();
          }
          else {              
              alert("select atleast one checkbox from list");
          }
          return false;
      }

      function CancelBtn() {
          document.getElementById('<%=TextBoxNewFilterName.ClientID %>').value = "";
      }
      </script>
      <script type="text/javascript">
          function NewBanchmarkFilter() {
              var emails = "";
              $(".chkGrid:checked").each(function () {
                  if (emails != "") {
                      emails = emails + ",";
                  }
                  emails = emails + $(this).val();
              });
              if (emails == "") {
                  alert("select atleast one checkbox from list");
                  return false;
              }
              else {
                  $(".chkGrid").each(function () {
                      if ($(this).attr('checked'))
                      {  }
                      else {
                          $(this).parent().parent().hide();
                          //$(this).parent().parent().css('background-color', 'red');
                      }
                  });
                  document.getElementById('<%=HiddenFieldUnitId.ClientID %>').value = emails;
                  //popup.show('#simplediv'); return false; 
                  //window.open('#simplediv'); return false; 
                  $(".BackgroundDiv").show();
                  $(".simplediv").show();
                  return false;
                             
              }      
      }
//          debugger;
//                   var GridView = document.getElementById('<%=GridViewReport.ClientID %>');
//                   //var GridView = document.getElementById('');
//                   var DeptId;
//                   var emails;
//                   if (GridView.rows.length > 0) {
//                       for (Row = 1; Row < GridView.rows.length; Row++) {
//                           // DeptId = GridView.rows.cell[0];
//                           //if (GridView.rows[Row].cell[1].type == "checkbox")
//                           // var chkbox = GridView.rows[Row].cell[3].type == "checkbox"
//                           //(GridView.rows[Row].cell[3].type).checked = true;
//                          // alert(GridView.rows[Row].[0].value);                    
//                       }
          //                   }

      
 </script>
<script type="text/javascript">
    function pageLoad() {

        var manager = Sys.WebForms.PageRequestManager.getInstance();

        manager.add_endRequest(endRequest);

        manager.add_beginRequest(onBeginRequest);

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
</script>
<asp:ScriptManager ID="ScriptManagerRMCHospitalBenchmark" runat="server">
</asp:ScriptManager>
<script type="text/javascript" language="javascript">


    function windowPopup(page, title) {

        window.open(page, '_blank', 'height=355,width=710,top=150,left=150,status=no,toolbar=no,menubar=no,location=no,scrollbars=no,resizable=no,copyhistory=false');
        return false;
    }

    //    function monthSelect() {
    //       
    //        ValidatorEnable(document.getElementById("RequiredFieldValidatorMonthFrom"), true); 
    //    }

</script>
<asp:Panel ID="PanelHospitalBenchmark" runat="server">
    <table width="800px" style="text-align: left">
        <tr>
            <th align="left" style="font-size: 14px; padding-left: 10px; padding-top: 10px; color: #06569d;">
                <%--<u>Time Study RN Summary Report</u>--%>
                <asp:Label ID="Label2" runat="server" Text="Collaboration Report" Font-Underline="true"></asp:Label>
            </th>
            <th align="left" style="font-size: 14px; padding-left: 20px; padding-top: 10px; color: #06569d;">
                <%--<h3 style="font-size: 13px;">--%>
            </th>
        </tr>
        <tr>
            <td style="padding-left: 20px;">
                <div style="width: 420px; float: left; background-color: Transparent; z-index: 0;">
                    <div id="divErrorMsgInner" style="width: 418px; float: left; background-color: #E8E9EA;
                        z-index: 10;">
                        <div style="text-align: left; padding-left: 5px;">
                            <asp:ValidationSummary ID="ValidationSummaryUserRegistration" runat="server" DisplayMode="List"
                                Font-Bold="true" Font-Size="11px" ShowMessageBox="true" ShowSummary="false" Style="padding-top: 1px;"
                                ValidationGroup="Demographic" />
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
            <td align="center">
                <table width="95%">
                    <tr>
                        <td>
                            <table style="padding-left: 47px;">
                                <tr>
                                    <td align="right">
                                        <asp:Label ID="LabelBenchmarkingFilters" runat="server" Font-Bold="true" Font-Size="11px"
                                            ForeColor="#06569d" Text="BenchMarking Filter"></asp:Label>
                                        &nbsp;&nbsp;
                                    </td>
                                    <td align="left">
                                        <asp:DropDownList ID="DropDownListBenchmarkingFilter" runat="server" AppendDataBoundItems="True"
                                            CssClass="aspDropDownList" DataSourceID="ObjectDataSourceBenchmarkingFilterNames"
                                            DataTextField="FilterName" DataValueField="FilterId" ForeColor="#06569D" TabIndex="1">
                                            <asp:ListItem Value="0">Select Filter</asp:ListItem>
                                            <asp:ListItem Selected="True" Value="1">No Filter</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidatorFilter" runat="server" ControlToValidate="DropDownListBenchmarkingFilter"
                                            Display="None" ErrorMessage="Must Select Filter" InitialValue="0" SetFocusOnError="true"
                                            ValidationGroup="Demographic">*</asp:RequiredFieldValidator>
                                    </td>
                                    <td>
                                    &nbsp;&nbsp;&nbsp;&nbsp;
                                    </td>
                                    <td>
                                        <table>
                                            <tr>
                                                <td align="left">
                                                <asp:RadioButton ID="RadioButtonSummarizeAllData" Checked="true" 
                                                    Font-Bold="true" Font-Size="11px" ForeColor="#06569d" 
                                                    GroupName="ReportType" runat="server" 
                                                    Text="Summarized By Unit" oncheckedchanged="RadioButtonSummarizeAllData_CheckedChanged"/>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="left">
                                                <asp:RadioButton ID="RadioButtonSummarizeByYear" Font-Bold="true" Font-Size="11px"
                                                    ForeColor="#06569d"
                                                    GroupName="ReportType" runat="server" Text="Summarize By Unit and By Year" 
                                                    oncheckedchanged="RadioButtonSummarizeByYear_CheckedChanged"/>
                                                </td>
                                            </tr>
                                        </table>
                                       
                                    </td>
                                </tr>
                            </table>
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
                        <td align="center">
                            <table>
                                <tr>
                                    <td>
                                        <asp:Label ID="Label1" runat="server" Font-Bold="true" Font-Size="11px" Font-Underline="true"
                                            ForeColor="#06569d" Text="Profile(s)"></asp:Label>
                                        &nbsp;&nbsp;
                                    </td>
                                    <td>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2" style="height: 5px;">
                                    </td>
                                </tr>
                                <tr>
                                    <td align="left" valign="top">
                                        <asp:Label ID="LabelValueAddedProfile" runat="server" Font-Bold="true" Font-Size="11px"
                                            ForeColor="#06569d" Text=""></asp:Label>
                                    </td>
                                    <td>
                                        <asp:UpdatePanel ID="updListbox" runat="server">
                                            <ContentTemplate>
                                                <asp:ListBox ID="ListBoxAvailableProfiles" runat="server" CssClass="aspListBox" ForeColor="#06569D"
                                                    Height="120px" Rows="8" SelectionMode="Multiple" TabIndex="2" ToolTip="Available Profile(s)">
                                                </asp:ListBox>
                                            </ContentTemplate>
                                            <Triggers>
                                                <asp:AsyncPostBackTrigger ControlID="ButtonAdd" EventName="Click" />
                                            </Triggers>
                                        </asp:UpdatePanel>
                                    </td>
                                    <td align="center" style="width: 164px;">
                                        <asp:Button ID="ButtonAdd" runat="server" Font-Bold="true" OnClick="ButtonAdd_Click"
                                            TabIndex="3" Text="&gt;" ToolTip="Add Profile" Visible="true" Width="40px" />
                                        <br />
                                        <br />
                                        <asp:Button ID="ButtonRemove" runat="server" Font-Bold="true" OnClick="ButtonRemove_Click"
                                            TabIndex="5" Text="&lt;" ToolTip="Remove Profile" Visible="true" Width="40px" />
                                    </td>
                                    <td>
                                        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                            <ContentTemplate>
                                                <asp:ListBox ID="ListBoxSelectedProfiles" runat="server" CssClass="aspListBox listboxselect" ForeColor="#06569D"
                                                    Height="120px" Rows="8" SelectionMode="Multiple" TabIndex="4" ToolTip="Selected Profile(s)">
                                                </asp:ListBox>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidatorSelectedProfiles" runat="server"
                                                    ControlToValidate="ListBoxSelectedProfiles" Display="None" ErrorMessage="Must Select Profiles"
                                                    InitialValue="String" ValidationGroup="Demographic">*</asp:RequiredFieldValidator>
                                            </ContentTemplate>
                                            <Triggers>
                                                <asp:AsyncPostBackTrigger ControlID="ButtonRemove" EventName="Click" />
                                            </Triggers>
                                        </asp:UpdatePanel>
                                    </td>
                                    <td align="left" valign="top">
                                        <asp:Label ID="LabelLocationProfile" runat="server" Font-Bold="true" Font-Size="11px"
                                            ForeColor="#06569d" Text=""></asp:Label>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td style="height: 5px;">
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <div style="width: 100%; padding-top: 0px; padding-bottom: 0px;">
                                <hr style="height: 1px; color: #d6d6d6;" />
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td align="center">
                            <asp:UpdatePanel ID="UpdatePanelYearMonth" runat="server" ChildrenAsTriggers="true"
                                UpdateMode="Conditional">
                                <ContentTemplate>
                                    <table style="padding-right: 26px;">
                                        <tr>
                                            <td align="right">
                                                <asp:Label ID="LabelYearFrom" runat="server" Font-Bold="true" Font-Size="11px" ForeColor="#06569d"
                                                    Text="Period From"></asp:Label>
                                                &nbsp;&nbsp;
                                            </td>
                                            <td align="left">
                                                <asp:DropDownList ID="DropDownListYearMonthFrom" runat="server" AppendDataBoundItems="True"
                                                    CssClass="aspDropDownList" ForeColor="#06569D" TabIndex="6">
                                                    <asp:ListItem Value="0">Select Period From</asp:ListItem>
                                                </asp:DropDownList>
                                                <%if (DropDownListYearMonthFrom.SelectedValue == "0" && DropDownListYearMonthTo.SelectedValue != "0")
                                                  { %>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidatorYearMonthFrom" runat="server"
                                                    ControlToValidate="DropDownListYearMonthFrom" Display="None" ErrorMessage="Must Select Period From"
                                                    InitialValue="0" ValidationGroup="Demographic">*</asp:RequiredFieldValidator>
                                                <%} %>
                                            </td>
                                            <td style="width: 98px;">
                                            </td>
                                            <td align="right">
                                                <asp:Label ID="LabelYearTo" runat="server" Font-Bold="true" Font-Size="11px" ForeColor="#06569d"
                                                    Text="Period To"></asp:Label>
                                                &nbsp;&nbsp;
                                            </td>
                                            <td align="left">
                                                <asp:DropDownList ID="DropDownListYearMonthTo" runat="server" AppendDataBoundItems="True"
                                                    CssClass="aspDropDownList" ForeColor="#06569D" TabIndex="7">
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
                                    </table>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </td>
                    </tr>
                    <tr>
                        <td style="height: 35px;">
                        </td>
                    </tr>
                    <tr>
                        <td align="center">
                        
                            <asp:Button ID="ButtonGenerateReport" runat="server" CssClass="aspButton" OnClick="ButtonGenerateReport_Click"
                                TabIndex="8" Text="Generate Report" ValidationGroup="Demographic" Visible="true" OnClientClick="return checkListbox();"
                                Width="130px" />
                            &nbsp;
                            <asp:Button ID="ButtonReset" runat="server" CssClass="aspButton" OnClick="ButtonReset_Click"
                                TabIndex="9" Text="Reset" Visible="True" Width="70px" />
                                &nbsp;
                            <asp:Button ID="ButtonSendEmail" runat="server" CssClass="aspButton" OnClientClick="return getEmailChk();"
                                TabIndex="9" Text="Send Message to Selected Units" Visible="false" 
                                Width="210px" onclick="ButtonSendEmail_Click" />
                                &nbsp;
                            <asp:Button ID="ButtonNewBenchmarkingFilter" runat="server" 
                                CssClass="aspButton" OnClientClick="return NewBanchmarkFilter();"
                                TabIndex="9" Text="Create New Benchmarking Filter" Visible="false" 
                                Width="210px" onclick="ButtonNewBenchmarkingFilter_Click" />
                            <asp:HiddenField ID="HiddenFieldUnitId" runat="server" />
                        </td>
                    </tr>
                </table>
                <%--test--%>
            </td>
        </tr>
        <tr>
            <td>
                <asp:ObjectDataSource ID="ObjectDataSourcePharmacyType" runat="server" SelectMethod="GetAllPharmacyType"
                    TypeName="RMC.BussinessService.BSPharmacyType"></asp:ObjectDataSource>
                <asp:ObjectDataSource ID="ObjectDataSourceUnitType" runat="server" SelectMethod="GetAllUnitType"
                    TypeName="RMC.BussinessService.BSUnitType"></asp:ObjectDataSource>
                <asp:ObjectDataSource ID="ObjectDataSourceProfileTypeValueAdded" runat="server" SelectMethod="GetProfileTypeValueAdded"
                    TypeName="RMC.BussinessService.BSProfileType"></asp:ObjectDataSource>
                <asp:ObjectDataSource ID="ObjectDataSourceProfileTypeOthers" runat="server" SelectMethod="GetProfileTypeOthers"
                    TypeName="RMC.BussinessService.BSProfileType"></asp:ObjectDataSource>
                <asp:ObjectDataSource ID="ObjectDataSourceProfileTypeLocation" runat="server" SelectMethod="GetProfileTypeLocation"
                    TypeName="RMC.BussinessService.BSProfileType"></asp:ObjectDataSource>
                <%--<asp:ObjectDataSource ID="ObjectDataSourceHospitalUnit" runat="server" SelectMethod="GetHospitalDemographicDetailByHospitalID"
                    TypeName="RMC.BussinessService.BSHospitalDemographicDetail">
                    <SelectParameters>
                        <asp:ControlParameter ControlID="DropDownListHospitalName" DefaultValue="0" Name="hospitalID"
                            PropertyName="SelectedValue" Type="Int32" />
                    </SelectParameters>
                </asp:ObjectDataSource>--%>
                <%--<asp:ObjectDataSource ID="ObjectDataSourceCountry" runat="server" SelectMethod="GetAllCountries"
                    TypeName="RMC.BussinessService.BSCommon"></asp:ObjectDataSource>
                <asp:ObjectDataSource ID="ObjectDataSourceState" runat="server" SelectMethod="GetAllStateNamesByCountryID"
                    TypeName="RMC.BussinessService.BSCommon">
                    <SelectParameters>
                        <asp:ControlParameter ControlID="DropDownListCountry" DefaultValue="0" Name="CountryID"
                            PropertyName="SelectedValue" Type="Int32" />
                    </SelectParameters>
                </asp:ObjectDataSource>--%>
                <asp:LinqDataSource ID="LinqDataSourceHospitalType" runat="server" ContextTypeName="RMC.DataService.RMCDataContext"
                    OrderBy="HospitalTypeName" TableName="HospitalTypes">
                </asp:LinqDataSource>
                <asp:ObjectDataSource ID="ObjectDataSourceOperator" runat="server" SelectMethod="Operator"
                    TypeName="RMC.BussinessService.BSCommon"></asp:ObjectDataSource>
                <asp:ObjectDataSource ID="ObjectDataSourceYear" runat="server" SelectMethod="Years"
                    TypeName="RMC.BussinessService.BSYear">
                    <SelectParameters>
                        <asp:Parameter DefaultValue="2002" Name="startingYear" Type="Int32" />
                        <asp:Parameter DefaultValue="20" Name="noOfYears" Type="Int32" />
                    </SelectParameters>
                </asp:ObjectDataSource>
                <br />
            </td>
        </tr>
        </caption>
        <tr>
            <td>
               
            </td>
        </tr>
    </table>
    <table>
        <tr>
            <td>
                <div style="height: 5px;">
                </div>
                <div style="text-align: left; padding-left: 2px;">
                    <asp:LinkButton ID="LinkButtonExportReport" Visible="false" Font-Size="11px" runat="server"
                        TabIndex="2" OnClick="LinkButtonExportReport_Click" Font-Bold="true">ExportToExcel</asp:LinkButton>
                </div>
                <div style="height: 1px;">
                </div>
            </td>
        </tr>
        <tr>
            <td>
                <asp:GridView ID="GridViewFunctionReport" runat="server" CellPadding="1" CssClass="GridViewStyle"
                    EmptyDataText="No Record to display." HorizontalAlign="Center" OnRowDataBound="GridViewFunctionReport_RowDataBound">
                    <HeaderStyle CssClass="GridViewHeaderStyle" />
                    <EmptyDataRowStyle CssClass="GridViewEmptyRowStyle" />
                    <FooterStyle CssClass="GridViewFooterStyle" />
                    <PagerStyle CssClass="GridViewPagerStyle" />
                    <SelectedRowStyle CssClass="GridViewSelectedRowStyle" />
                    <AlternatingRowStyle CssClass="GridViewAlternatingRowStyle" />
                    <RowStyle BackColor="#80ffff" CssClass="GridViewRowStyle" HorizontalAlign="Center"
                        VerticalAlign="Middle" />
                    <Columns>
                        <asp:TemplateField>
                            <ItemTemplate>
                                &nbsp;&nbsp;&nbsp;&nbsp;
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </td>
        </tr>
        <tr>
            <td>
           

                <asp:GridView ID="GridViewReport" runat="server" CellPadding="1" CssClass="GridViewStyle"
                    EmptyDataText="No Record to display." EnableModelValidation="True" HorizontalAlign="Center"
                    OnRowDataBound="GridViewReport_RowDataBound">
                    <HeaderStyle CssClass="GridViewHeaderStyle" />
                    <EmptyDataRowStyle CssClass="GridViewEmptyRowStyle" />
                    <FooterStyle CssClass="GridViewFooterStyle" />
                    <PagerStyle CssClass="GridViewPagerStyle" />
                    <SelectedRowStyle CssClass="GridViewSelectedRowStyle" />
                    <AlternatingRowStyle CssClass="GridViewAlternatingRowStyle" />
                    <RowStyle CssClass="GridViewRowStyle" HorizontalAlign="Center" VerticalAlign="Middle" />
                    <Columns>
                        <asp:TemplateField>
                            <ItemTemplate> 
                            <%--<asp:CheckBox ID="CheckBoxEmail" runat="server" Text="testaa" />  --%>                        
                            <input type="checkbox" class="chkGrid" value="<%#Eval("Email")%>" />
                            <%--<a href="#"  onclick="javascript:popup_window=window.open('SendMessage.aspx?id=<%#Eval("Email")%>', 'popup_window', 'width=500,height=420,top=100,left=400,scrollbars=1');popup_window.focus();"> <img src="../Images/message-04.gif" alt="Send Email" width="11px" height="11px" /></a>--%>
                                <%--<asp:LinkButton ID="LinkButton1" runat="server" ToolTip="Send Message">
                <img src="../Images/message-04.gif" alt="Send Email" width="11px" height="11px" />
                                </asp:LinkButton>--%>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </td>
        </tr>
        
    </table>
</asp:Panel>
<asp:ObjectDataSource ID="ObjectDataSourceBenchmarkingFilterNames" runat="server"
    SelectMethod="GetBenchmarkFilterNames" TypeName="RMC.BussinessService.BSReports">
</asp:ObjectDataSource>
<div class="BackgroundDiv" style="background-color:black;border:1px solid black;display:none;width:100%;height:100%; opacity:0.6; filter: alpha(opacity=60); position:fixed; z-index:800; top:0; left:0;"></div>

<div id="simplediv1" class="simplediv" style="background-color:White;border:1px solid black;display:none;width:400px;height:150px; position:absolute; z-index:1000; top:45%; left:40%;">

<table width="100%">
                   <tr>
                       <td  align="center">
                           &nbsp;
                       </td>
                   </tr>
                   
                   <tr>                
                       <td align="center">  <label style="font-size:14px">Name of New Filter :</label>  &nbsp;
                           <asp:TextBox ID="TextBoxNewFilterName" runat="server"></asp:TextBox>
                       </td>
                   </tr>                   
                   <tr>
                        <td  align="center">
                           &nbsp;
                       </td>
                   </tr>
                   <tr>
                       <td  align="center">
                           <asp:Button ID="Button1" runat="server" CssClass="aspButton" Width="100px" OnClick="ButtonNewBenchmarkingFilter_Click" OnClientClick="return checkTextBoxNewFilterName();" Text="Ok" />
    <asp:Button ID="ButtonCancel" CssClass="aspButton" Width="100px" runat="server" OnClientClick="CancelBtn();" Text="Cancel" />
                       </td>
                   </tr>                  
                   <tr>
                       <td align="center">
                           &nbsp;
                       </td>
                   </tr>
                   <tr>               
                       <td align="center">
                           <h3><asp:Label ID="Labelerror" runat="server" ForeColor="Red"></asp:Label></h3>
                       </td>
                   </tr>
                    <tr>
                       <td  align="center">
                           &nbsp;
                       </td>
                   </tr>
               </table>
</div>


<div id="ParentDiv" class="Background" style="visibility: hidden;">
    <div id="IMGDIV" align="center" valign="middle" style="position: absolute; left: 45%;
        top: 50%; visibility: hidden; vertical-align: middle; border-style: none; border-color: black;
        z-index: 40;">
        <asp:Image ID="Image2" runat="server" ImageUrl="~/Images/Loading.gif" BorderWidth="0px"
            AlternateText="Loading"></asp:Image>
    </div>
</div>
