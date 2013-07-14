<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ViewBenchmarkingFilters.ascx.cs"
    Inherits="RMC.Web.UserControls.ViewBenchmarkingFilters" %>
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
    element.style
    {
        height: 1593px;
        left: 0;
        position: fixed;
        top: 0;
        width: 947px;
        z-index: 10000;
    }
    .modalBackground
    {
        filter: alpha(opacity=60);
        background-color: Black;
        opacity: 0.6;
    }
    .style1
    {
        width: 22%;
    }
    .style2
    {
        width: 136px;
    }
    .style3
    {
        height: 28px;
    }
    .style4
    {
        width: 136px;
        height: 28px;
    }
</style>

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
    jQuery(function($) {

        $(document.getElementById('<%=TextBoxBedsInUnitFrom.ClientID%>')).numeric();
        $(document.getElementById('<%=TextBoxBedsInUnitTo.ClientID%>')).numeric();
        $(document.getElementById('<%=TextBoxBudgetedPatientFrom.ClientID%>')).numeric({ allow: "." });
        $(document.getElementById('<%=TextBoxBudgetedPatientTo.ClientID%>')).numeric({ allow: "." });
        $(document.getElementById('<%=TextBoxElectronicDocumentationFrom.ClientID%>')).numeric({ allow: "." });
        $(document.getElementById('<%=TextBoxElectronicDocumentationTo.ClientID%>')).numeric({ allow: "." });
        $(document.getElementById('<%=TextBoxHospitalSizeFrom.ClientID%>')).numeric({ allow: "." });
        $(document.getElementById('<%=TextBoxHospitalSizeTo.ClientID%>')).numeric({ allow: "." });
        $(document.getElementById('<%=TextBoxMinimumDataPointsFrom.ClientID%>')).numeric({ allow: "." });
        $(document.getElementById('<%=TextBoxMinimumDataPointsTo.ClientID%>')).numeric({ allow: "." });

        //        $("#divInner").corner("round 6px").parent().css('padding', '1px').corner("round 6px");
        //        $("#divErrorMsgInner").corner("round 6px").parent().css('padding', '1px').corner("round 6px");
    });

    function windowPopup(page, title) {

        window.open(page, '_blank', 'height=355,width=710,top=150,left=150,status=no,toolbar=no,menubar=no,location=no,scrollbars=no,resizable=no,copyhistory=false');
        return false;
    }

    //    function monthSelect() {
    //       
    //        ValidatorEnable(document.getElementById("RequiredFieldValidatorMonthFrom"), true); 
    //    }

</script>

<cc1:ModalPopupExtender ID="MPE" runat="server" TargetControlID="ButtonSaveAs" PopupControlID="PanelPopup"
    BackgroundCssClass="modalBackground" DropShadow="true" CancelControlID="ButtonCancelPopup" />
<asp:Panel ID="PanelBenchmarking" runat="server">
    <table width="100%">
        <tr>
            <th align="left" style="font-size: 14px; padding-left: 10px; padding-top: 10px; color: #06569d;">
                <%--<h3 style="font-size: 13px;">--%>
                <u>Benchmarking Filters</u>
                <%--</h3>--%>
            </th>
            <th align="right" style="padding-left: 10px; padding-top: 10px;">
                <asp:ImageButton ID="ImageButtonBack" ToolTip="Back" runat="server" ImageUrl="~/Images/back.gif"
                    TabIndex="6" OnClick="ImageButtonBack_Click" />
            </th>
        </tr>
        <tr>
            <td style="padding-left: 20px;" colspan="2">
                <div style="width: 420px; float: left; background-color: Transparent; z-index: 0;">
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
            <td colspan="2">
                <table width="100%" border="0px">
                    <tr>
                        <td align="right" style="color: #06569d; font-weight: bold; font-size: 11px;" class="style3">
                            Filter Name <span style="color: Red;"></span>
                        </td>
                        <td align="left" class="style4">
                            <asp:TextBox CssClass="aspTextBox" ID="TextBoxFilterName" runat="server" TabIndex="1"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidatorFilterName" runat="server"
                                ControlToValidate="TextBoxFilterName" ErrorMessage="Please Fill Filter Name"
                                Display="None" SetFocusOnError="true" ValidationGroup="Demographic">*</asp:RequiredFieldValidator>
                        </td>
                        <td align="right" style="color: #06569d; font-weight: bold; font-size: 11px;" class="style3">
                            Comment <span style="color: Red;"></span>
                        </td>
                        <td align="left" rowspan="3" class="style1">
                            <asp:TextBox CssClass="aspTextBox" ID="TextBoxComment" runat="server" TextMode="MultiLine"
                                Height="80px" TabIndex="4" ForeColor="#06569d"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td align="right" style="color: #06569d; font-weight: bold; font-size: 11px;">
                            Creator Name <span style="color: Red;"></span>
                        </td>
                        <td align="left" class="style2" style="color: #06569d; font-size: 11px;">
                            <%--<asp:TextBox CssClass="aspTextBox" ID="TextBoxCreatorName" runat="server" TabIndex="2"
                                ReadOnly="true"></asp:TextBox>--%>
                            <asp:Label CssClass="aspLabel" ID="LabelCreatorName" runat="server" TabIndex="2"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td align="right" style="color: #06569d; font-weight: bold; font-size: 11px;">
                            Shared <span style="color: Red;"></span>
                        </td>
                        <td align="left" valign="middle" class="style2">
                            <asp:CheckBox ID="CheckBoxShared" Text="" runat="server" Font-Bold="True" Font-Size="11px"
                                ForeColor="#06569D" TabIndex="3" />
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4">
                            <div style="width: 100%; padding-top: 1px; padding-bottom: 1px;">
                                <hr style="height: 1px; color: #d6d6d6;" />
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td align="right" style="color: #06569d; font-weight: bold; font-size: 11px;">
                            Beds In Unit From <span style="color: Red;"></span>
                        </td>
                        <td align="left" class="style2">
                            <asp:TextBox CssClass="aspTextBox" ID="TextBoxBedsInUnitFrom" MaxLength="5" runat="server"
                                Width="62px" TabIndex="5"></asp:TextBox>
                            <asp:DropDownList CssClass="aspDropDownList" ID="DropDownListBedsInUnitOperatorFrom"
                                ForeColor="#06569D" Width="60px" runat="server" AppendDataBoundItems="True" DataTextField="Text"
                                DataValueField="Value" DataSourceID="ObjectDataSourceOperator" TabIndex="6">
                                <asp:ListItem Value="0">Select</asp:ListItem>
                            </asp:DropDownList>
                            <%--<asp:RequiredFieldValidator ID="RequiredFieldValidatorBedsInUnitOperator" runat="server"
                                ControlToValidate="DropDownListBedsInUnitOperator" ErrorMessage="Must select Operator"
                                Display="None" InitialValue="0" SetFocusOnError="True" ValidationGroup="Demographic">*</asp:RequiredFieldValidator>--%>
                        </td>
                        <td align="right" style="color: #06569d; font-weight: bold; font-size: 11px;">
                            Hospital Type <span style="color: Red;"></span>
                        </td>
                        <td align="left" rowspan="4" valign="top" class="style1">
                            <asp:ListBox CssClass="aspListBox" ID="ListBoxHospitalType" runat="server" DataSourceID="LinqDataSourceHospitalType"
                                DataTextField="HospitalTypeName" DataValueField="HospitalTypeID" ForeColor="#06569D"
                                SelectionMode="Multiple" Height="100px" Rows="8" TabIndex="21"></asp:ListBox>
                        </td>
                    </tr>
                    <tr>
                        <td align="right" style="color: #06569d; font-weight: bold; font-size: 11px;">
                            Beds In Unit To <span style="color: Red;"></span>
                        </td>
                        <td align="left" class="style2">
                            <asp:TextBox CssClass="aspTextBox" ID="TextBoxBedsInUnitTo" MaxLength="5" runat="server"
                                Width="62px" TabIndex="7"></asp:TextBox>
                            <asp:DropDownList CssClass="aspDropDownList" ID="DropDownListBedsInUnitOperatorTo"
                                ForeColor="#06569D" Width="60px" runat="server" AppendDataBoundItems="True" DataTextField="Text"
                                DataValueField="Value" DataSourceID="ObjectDataSourceOperator" TabIndex="8">
                                <asp:ListItem Value="0">Select</asp:ListItem>
                            </asp:DropDownList>
                            <%--<asp:RequiredFieldValidator ID="RequiredFieldValidatorBedsInUnitOperator" runat="server"
                                ControlToValidate="DropDownListBedsInUnitOperator" ErrorMessage="Must select Operator"
                                Display="None" InitialValue="0" SetFocusOnError="True" ValidationGroup="Demographic">*</asp:RequiredFieldValidator>--%>
                        </td>
                        <td align="right" style="color: #06569d; font-size: 11px;" rowspan="2">
                            Hold &#39;Ctrl&#39; key down to select/deselect multiple types.
                        </td>
                    </tr>
                    <tr>
                        <td align="right" style="color: #06569d; font-weight: bold; font-size: 11px;">
                            Electronic Doc.(%) From <span style="color: Red;"></span>
                        </td>
                        <td align="left" class="style2">
                            <span class="file" style="font-size: 11px; font-weight: bold; cursor: pointer;"><span>
                                <asp:TextBox CssClass="aspTextBox" ID="TextBoxElectronicDocumentationFrom" runat="server"
                                    MaxLength="3" Width="62px" TabIndex="9"></asp:TextBox>
                                <asp:DropDownList CssClass="aspDropDownList" ID="DropDownListElectronicDocumentationOperatorFrom"
                                    ForeColor="#06569D" Width="60px" runat="server" AppendDataBoundItems="True" DataTextField="Text"
                                    DataValueField="Value" DataSourceID="ObjectDataSourceOperator" TabIndex="10">
                                    <asp:ListItem Value="0">Select</asp:ListItem>
                                </asp:DropDownList>
                                <%if (TextBoxElectronicDocumentationFrom.Text != "")
                                  {%>
                                <%--<asp:RequiredFieldValidator ID="RequiredFieldValidatorElectronicDocumentationOperatorFrom"
                                    runat="server" ControlToValidate="DropDownListElectronicDocumentationOperatorFrom"
                                    ErrorMessage="Must select Operator" Display="None" InitialValue="0" SetFocusOnError="True"
                                    ValidationGroup="Demographic">*</asp:RequiredFieldValidator>--%>
                                <%} %>
                        </td>
                    </tr>
                    <tr>
                        <td align="right" style="color: #06569d; font-weight: bold; font-size: 11px;">
                            Electronic Doc.(%) To <span style="color: Red;"></span>
                        </td>
                        <td align="left" class="style2">
                            <span class="file" style="font-size: 11px; font-weight: bold; cursor: pointer;"><span>
                                <asp:TextBox CssClass="aspTextBox" ID="TextBoxElectronicDocumentationTo" runat="server"
                                    MaxLength="3" Width="62px" TabIndex="11"></asp:TextBox>
                                <asp:DropDownList CssClass="aspDropDownList" ID="DropDownListElectronicDocumentationOperatorTo"
                                    ForeColor="#06569D" Width="60px" runat="server" AppendDataBoundItems="True" DataTextField="Text"
                                    DataValueField="Value" DataSourceID="ObjectDataSourceOperator" TabIndex="12">
                                    <asp:ListItem Value="0">Select</asp:ListItem>
                                </asp:DropDownList>
                                <%if (TextBoxElectronicDocumentationTo.Text != "")
                                  {%>
                                <%--<asp:RequiredFieldValidator ID="RequiredFieldValidatorElectronicDocumentationTo" runat="server" ControlToValidate="DropDownListElectronicDocumentationOperatorTo"
                                    ErrorMessage="Must select Operator" Display="None" InitialValue="0" SetFocusOnError="True"
                                    ValidationGroup="Demographic">*</asp:RequiredFieldValidator>--%>
                                <%} %>
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td align="right" style="color: #06569d; font-weight: bold; font-size: 11px; width: 188px">
                            Budgeted Patient per Nurse From
                        </td>
                        <td align="left" class="style2">
                            <span class="file" style="font-size: 11px; font-weight: bold; cursor: pointer;"><span>
                                <asp:TextBox CssClass="aspTextBox" ID="TextBoxBudgetedPatientFrom" MaxLength="8"
                                    runat="server" Width="61px" TabIndex="13"></asp:TextBox>
                                <asp:DropDownList CssClass="aspDropDownList" ID="DropDownListBudgetedPatientOperatorFrom"
                                    ForeColor="#06569D" Width="60px" runat="server" AppendDataBoundItems="True" DataTextField="Text"
                                    DataValueField="Value" DataSourceID="ObjectDataSourceOperator" TabIndex="14">
                                    <asp:ListItem Value="0">Select</asp:ListItem>
                                </asp:DropDownList>
                                <%if (TextBoxBudgetedPatientFrom.Text != "")
                                  {%>
                                <%--<asp:RequiredFieldValidator ID="RequiredFieldValidatorBudgetedPatientOperatorFrom"
                                    runat="server" ControlToValidate="DropDownListBudgetedPatientOperatorFrom" ErrorMessage="Must select Operator"
                                    Display="None" InitialValue="0" SetFocusOnError="True" ValidationGroup="Demographic">*</asp:RequiredFieldValidator>--%>
                                <%} %>
                        </td>
                        <td align="right" style="color: #06569d; font-weight: bold; font-size: 11px;">
                            Doc By Exception <span style="color: Red;"></span>
                        </td>
                        <td align="left" class="style1">
                            <asp:DropDownList CssClass="aspDropDownList" ID="DropDownListDocByException" ForeColor="#06569D"
                                runat="server" AppendDataBoundItems="True" DataTextField="UserName" DataValueField="UserID"
                                TabIndex="22">
                                <asp:ListItem Value="0">Yes/No</asp:ListItem>
                                <asp:ListItem Value="1">Yes</asp:ListItem>
                                <asp:ListItem Value="2">No</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td align="right" style="color: #06569d; font-weight: bold; font-size: 11px;">
                            Budgeted Patient per Nurse To<span style="color: Red;"></span>
                        </td>
                        <td align="left" class="style2">
                            <span class="file" style="font-size: 11px; font-weight: bold; cursor: pointer;"><span>
                                <asp:TextBox CssClass="aspTextBox" ID="TextBoxBudgetedPatientTo" MaxLength="8" runat="server"
                                    Width="61px" TabIndex="15"></asp:TextBox>
                                <asp:DropDownList CssClass="aspDropDownList" ID="DropDownListBudgetedPatientOperatorTo"
                                    ForeColor="#06569D" Width="60px" runat="server" AppendDataBoundItems="True" DataTextField="Text"
                                    DataValueField="Value" DataSourceID="ObjectDataSourceOperator" TabIndex="16">
                                    <asp:ListItem Value="0">Select</asp:ListItem>
                                </asp:DropDownList>
                                <%if (TextBoxBudgetedPatientTo.Text != "")
                                  {%>
                                <%--<asp:RequiredFieldValidator ID="RequiredFieldValidatorBudgetedPatientOperatorTo"
                                    runat="server" ControlToValidate="DropDownListBudgetedPatientOperatorTo" ErrorMessage="Must select Operator"
                                    Display="None" InitialValue="0" SetFocusOnError="True" ValidationGroup="Demographic">*</asp:RequiredFieldValidator>--%>
                                <%} %>
                        </td>
                        <td align="right" style="color: #06569d; font-weight: bold; font-size: 11px;" valign="middle">
                            Country
                        </td>
                        <td align="left" valign="middle" class="style1">
                            <asp:DropDownList CssClass="aspDropDownList" ID="DropDownListCountry" ForeColor="#06569D"
                                runat="server" AppendDataBoundItems="True" DataTextField="CountryName" DataValueField="CountryID"
                                AutoPostBack="True" DataSourceID="ObjectDataSourceCountry" OnSelectedIndexChanged="DropDownListCountry_SelectedIndexChanged"
                                TabIndex="23">
                                <asp:ListItem Value="0">Select Country</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td align="right" style="color: #06569d; font-weight: bold; font-size: 11px;">
                            Hospital Size From <span style="color: Red;"></span>
                        </td>
                        <td align="left" class="style2">
                            <span class="file" style="font-size: 11px; font-weight: bold; cursor: pointer;">
                            </span>
                            <asp:TextBox CssClass="aspTextBox" ID="TextBoxHospitalSizeFrom" runat="server" Width="61px"
                                MaxLength="5" TabIndex="17"></asp:TextBox>
                            <asp:DropDownList CssClass="aspDropDownList" ID="DropDownListHospitalSizeOperatorFrom"
                                ForeColor="#06569D" Width="60px" runat="server" AppendDataBoundItems="True" DataTextField="Text"
                                DataValueField="Value" DataSourceID="ObjectDataSourceOperator" TabIndex="18">
                                <asp:ListItem Value="0">Select</asp:ListItem>
                            </asp:DropDownList>
                            <%if (TextBoxHospitalSizeFrom.Text != "")
                              {%>
                            <%-- <asp:RequiredFieldValidator ID="RequiredFieldValidatorHospitalSizeOperatorFrom" runat="server"
                                ControlToValidate="DropDownListHospitalSizeOperatorFrom" ErrorMessage="Must select Operator"
                                Display="None" InitialValue="0" SetFocusOnError="True" ValidationGroup="Demographic">*</asp:RequiredFieldValidator>--%>
                            <%} %>
                        </td>
                        <td align="right" style="color: #06569d; font-weight: bold; font-size: 11px;" valign="middle">
                            State
                        </td>
                        <td align="left" valign="middle" class="style1">
                            <asp:UpdatePanel ID="UpdatePanelState" runat="server" UpdateMode="Conditional">
                                <ContentTemplate>
                                    <asp:DropDownList CssClass="aspDropDownList" ID="DropDownListState" ForeColor="#06569D"
                                        runat="server" AppendDataBoundItems="True" DataTextField="StateName" DataValueField="StateID"
                                        DataSourceID="ObjectDataSourceState" TabIndex="24" OnSelectedIndexChanged="DropDownListState_SelectedIndexChanged">
                                        <asp:ListItem Value="0">Select State</asp:ListItem>
                                    </asp:DropDownList>
                                </ContentTemplate>
                                <Triggers>
                                    <asp:AsyncPostBackTrigger ControlID="DropDownListCountry" EventName="SelectedIndexChanged" />
                                </Triggers>
                            </asp:UpdatePanel>
                        </td>
                    </tr>
                    <tr>
                        <td align="right" style="color: #06569d; font-weight: bold; font-size: 11px;">
                            Hospital Size To <span style="color: Red;"></span>
                        </td>
                        <td align="left" class="style2">
                            <span class="file" style="font-size: 11px; font-weight: bold; cursor: pointer;">
                            </span>
                            <asp:TextBox CssClass="aspTextBox" ID="TextBoxHospitalSizeTo" runat="server" Width="61px"
                                MaxLength="5" TabIndex="19"></asp:TextBox>
                            <asp:DropDownList CssClass="aspDropDownList" ID="DropDownListHospitalSizeOperatorTo"
                                ForeColor="#06569D" Width="60px" runat="server" AppendDataBoundItems="True" DataTextField="Text"
                                DataValueField="Value" DataSourceID="ObjectDataSourceOperator" TabIndex="20">
                                <asp:ListItem Value="0">Select</asp:ListItem>
                            </asp:DropDownList>
                            <%if (TextBoxHospitalSizeTo.Text != "")
                              {%>
                            <%--<asp:RequiredFieldValidator ID="RequiredFieldValidatorHospitalSizeOperatorTo" runat="server"
                                ControlToValidate="DropDownListHospitalSizeOperatorTo" ErrorMessage="Must select Operator"
                                Display="None" InitialValue="0" SetFocusOnError="True" ValidationGroup="Demographic">*</asp:RequiredFieldValidator>--%>
                            <%} %>
                        </td>
                        <td align="center" style="color: #06569d; font-size: 11px;" class="style1" colspan="2">
                            If state not mention above, fill in textbox below
                        </td>
                    </tr>
                    <tr>
                        <td align="right" style="color: #06569d; font-weight: bold; font-size: 11px;">
                            Number of Data Points From<span style="color: Red;"></span>
                        </td>
                        <td align="left" class="style2">
                            <asp:TextBox CssClass="aspTextBox" ID="TextBoxMinimumDataPointsFrom" runat="server"
                                MaxLength="5" TabIndex="25" Width="61px"></asp:TextBox>
                            <asp:DropDownList CssClass="aspDropDownList" ID="DropDownListMinimumDataPointsFrom"
                                ForeColor="#06569D" Width="60px" runat="server" AppendDataBoundItems="True" DataTextField="Text"
                                DataValueField="Value" DataSourceID="ObjectDataSourceOperator" TabIndex="20">
                                <asp:ListItem Value="0">Select</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                        <td align="right" style="color: #06569d; font-weight: bold; font-size: 11px;" valign="middle">
                            State
                        </td>
                        <td align="left" valign="middle" class="style1">
                            <asp:TextBox CssClass="aspTextBox" ID="TextBoxState" runat="server" TabIndex="25"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td align="right" style="color: #06569d; font-weight: bold; font-size: 11px;">
                            Number of Data Points To<span style="color: Red;"></span>
                        </td>
                        <td align="left" class="style2">
                            <asp:TextBox CssClass="aspTextBox" ID="TextBoxMinimumDataPointsTo" runat="server"
                                MaxLength="5" TabIndex="25" Width="61px"></asp:TextBox>
                            <asp:DropDownList CssClass="aspDropDownList" ID="DropDownListMinimumDataPointsTo"
                                ForeColor="#06569D" Width="60px" runat="server" AppendDataBoundItems="True" DataTextField="Text"
                                DataValueField="Value" DataSourceID="ObjectDataSourceOperator" TabIndex="20">
                                <asp:ListItem Value="0">Select</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                        <td>
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td align="right" style="color: #06569d; font-weight: bold; font-size: 11px;">
                            Unit Type <span style="color: Red;"></span>
                        </td>
                        <td align="left" rowspan="5" valign="top" class="style2">
                            <asp:ListBox CssClass="aspListBox" ID="ListBoxUnitType" runat="server" DataSourceID="ObjectDataSourceUnitType"
                                DataTextField="UnitTypeName" DataValueField="UnitTypeID" ForeColor="#06569D"
                                SelectionMode="Multiple" Height="100px" Rows="12" TabIndex="26"></asp:ListBox>
                        </td>
                        <td align="right" style="color: #06569d; font-weight: bold; font-size: 11px;">
                            Pharmacy Type <span style="color: Red;"></span>
                        </td>
                        <td align="left" rowspan="5" valign="top" class="style1">
                            <asp:ListBox CssClass="aspListBox" ID="ListBoxPharmacyType" runat="server" ForeColor="#06569D"
                                DataSourceID="ObjectDataSourcePharmacyType" DataTextField="PharmacyName" DataValueField="PharmacyTypeID"
                                SelectionMode="Multiple" Height="100px" Rows="12" TabIndex="27"></asp:ListBox>
                        </td>
                    </tr>
                    <tr>
                        <td align="right" style="color: #06569d; font-size: 11px;">
                            Hold &#39;Ctrl&#39; key down to select/deselect multiple types.
                        </td>
                        <td align="right" style="color: #06569d; font-size: 11px;">
                            Hold &#39;Ctrl&#39; key down to select/deselect multiple types.
                        </td>
                    </tr>
                    <tr>
                        <td align="right" style="color: #06569d; font-weight: bold;">
                        </td>
                        <td align="left" style="color: #06569d; font-weight: bold;">
                        </td>
                    </tr>
                    <tr>
                        <td style="padding-top: 5px;">
                        </td>
                        <td align="left" style="padding-top: 5px;">
                        </td>
                    </tr>
                    <tr>
                        <td>
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td align="right" style="color: #06569d; font-weight: bold; font-size: 11px;">
                            Configuration Name <span style="color: Red;"></span>
                        </td>
                        <td align="left" rowspan="6" valign="top" class="style2">
                            <asp:ListBox CssClass="aspListBox" ID="ListBoxConfiurationName" runat="server" ForeColor="#06569D"
                                SelectionMode="Multiple" Height="100px" Rows="12" TabIndex="26" DataSourceID="ObjectDataSourceConfigurationName"
                                DataTextField="configName" DataValueField="configName"></asp:ListBox>
                        </td>
                        <td align="right" style="color: #06569d; font-weight: bold; font-size: 11px;">
                            <span style="color: Red;"></span>
                        </td>
                        <td align="left" rowspan="6" valign="top" class="style1">
                        </td>
                    </tr>
                    <tr>
                        <td align="right" style="color: #06569d; font-size: 11px;">
                            Hold &#39;Ctrl&#39; key down to select/deselect multiple names.
                        </td>
                        <td align="right" style="color: #06569d; font-size: 11px;">
                        </td>
                    </tr>
                    <tr>
                        <td align="right" style="color: #06569d; font-weight: bold;">
                        </td>
                        <td align="left" style="color: #06569d; font-weight: bold;">
                        </td>
                    </tr>
                    <tr>
                        <td style="padding-top: 5px;">
                        </td>
                        <td align="left" style="padding-top: 5px;">
                        </td>
                    </tr>
                    <tr>
                        <td>
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td>
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td align="center" colspan="4" style="padding-top: 5px; padding-right: 51px;">
                            <asp:Button ID="ButtonSaveAs" runat="server" Text="Save As" ValidationGroup="Demographic"
                                CssClass="aspButton" Width="70px" Visible="true" TabIndex="28" />&nbsp
                            <asp:Button ID="ButtonUpdate" runat="server" Text="Update" ValidationGroup="Demographic"
                                CssClass="aspButton" Width="128px" Visible="true" TabIndex="28" OnClick="ButtonUpdate_Click" />&nbsp
                            <asp:Button ID="ButtonDelete" runat="server" Text="Delete" Visible="True" CssClass="aspButton"
                                Width="70px" TabIndex="29" OnClick="ButtonDelete_Click" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <asp:ObjectDataSource ID="ObjectDataSourcePharmacyType" runat="server" SelectMethod="GetAllPharmacyType"
                    TypeName="RMC.BussinessService.BSPharmacyType"></asp:ObjectDataSource>
                <asp:ObjectDataSource ID="ObjectDataSourceUnitType" runat="server" SelectMethod="GetAllUnitType"
                    TypeName="RMC.BussinessService.BSUnitType"></asp:ObjectDataSource>
                <asp:ObjectDataSource ID="ObjectDataSourceCountry" runat="server" SelectMethod="GetAllCountries"
                    TypeName="RMC.BussinessService.BSCommon"></asp:ObjectDataSource>
                <asp:ObjectDataSource ID="ObjectDataSourceState" runat="server" SelectMethod="GetAllStateNamesByCountryID"
                    TypeName="RMC.BussinessService.BSCommon">
                    <SelectParameters>
                        <asp:ControlParameter ControlID="DropDownListCountry" DefaultValue="0" Name="CountryID"
                            PropertyName="SelectedValue" Type="Int32" />
                    </SelectParameters>
                </asp:ObjectDataSource>
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
                <asp:ObjectDataSource ID="ObjectDataSourceConfigurationName" runat="server" SelectMethod="GetDistinctCongfigName"
                    TypeName="RMC.BussinessService.BSReports"></asp:ObjectDataSource>
                <br />
            </td>
        </tr>
    </table>
</asp:Panel>
<asp:Panel ID="PanelPopup" Width="100%" Height="15%" BorderWidth="3px" BorderColor="#8eb7ff"
    Style="background-color: #e8e8e8;" runat="server">
    <table>
        <tr>
            <td colspan="2" align="left" style="color: #06569d; font-weight: bold; font-size: 14px;">
                <u>Save Filter As</u><span style="color: Red;"></span>
            </td>
        </tr>
        <tr>
            <td style="height: 5px;">
            </td>
        </tr>
        <tr>
            <td align="right" style="color: #06569d; font-weight: bold; font-size: 11px;">
                Filter Name <span style="color: Red;"></span>
            </td>
            <td align="left">
                <asp:TextBox CssClass="aspTextBox" ID="TextBoxNewFilterName" runat="server" TabIndex="1"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidatorNewFilterName" runat="server"
                    ControlToValidate="TextBoxNewFilterName" ErrorMessage="Please Fill Filter Name"
                    Display="None" SetFocusOnError="true" ValidationGroup="Popup">*</asp:RequiredFieldValidator>
                <asp:ValidationSummary ID="ValidationSummaryPopup" ValidationGroup="Popup" runat="server"
                    DisplayMode="List" ShowMessageBox="true" ShowSummary="false" Font-Size="11px"
                    Font-Bold="true" Style="padding-top: 1px;" />
            </td>
        </tr>
        <tr>
            <td style="height: 5px;">
            </td>
        </tr>
        <tr style="padding-top: 5px;">
            <td align="center" colspan="2">
                <asp:Button ID="ButtonSavePopup" runat="server" Text="Save" CssClass="aspButton"
                    Width="70px" Visible="true" OnClick="ButtonSavePopup_Click" ValidationGroup="Popup" />&nbsp
                <asp:Button ID="ButtonCancelPopup" runat="server" Text="Cancel" CssClass="aspButton"
                    Width="70px" Visible="true" />
            </td>
        </tr>
    </table>
</asp:Panel>
<div id="ParentDiv" class="Background" style="visibility: hidden;">
    <div id="IMGDIV" align="center" valign="middle" style="position: absolute; left: 45%;
        top: 50%; visibility: hidden; vertical-align: middle; border-style: none; border-color: black;
        z-index: 40;">
        <asp:Image ID="Image2" runat="server" ImageUrl="~/Images/Loading.gif" BorderWidth="0px"
            AlternateText="Loading"></asp:Image>
    </div>
</div>
