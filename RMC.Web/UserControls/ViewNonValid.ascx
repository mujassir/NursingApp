<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ViewNonValid.ascx.cs"
    Inherits="RMC.Web.UserControls.ViewNonValid" %>
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

    function toggleDiv(value) {

        var divSelected = document.getElementById('divSelect');
        var divEdited = document.getElementById('divEdit');

        if (value == 1) {

            divSelected.style.visibility = 'hidden';
            divSelected.style.display = 'none';
            divEdited.style.visibility = 'visible';
            divEdited.style.display = 'block';
        }
        else if (value == 0) {

            divSelected.style.visibility = 'visible';
            divSelected.style.display = 'block';
            divEdited.style.visibility = 'hidden';
            divEdited.style.display = 'none';
        }

        return false;
    }

    //    function checkAllCheckboxes(id) {

    //        //var checkBoxHeader = document.getElementById(id);
    //        for (var i = 0; i < document.forms[0].elements.length; i++) {

    //            if (document.forms[0].elements[i].type == "checkbox") {

    //                document.forms[0].elements[i].checked = id.checked;
    //            }
    //        }
    //    }


    //    function toggleHeaderCheckBox(id) {

    //        var checkBoxHeader = document.getElementById("CheckboxHeader");

    //        if (id.checked) {

    //            var flag = 0;
    //            for (var i = 0; i < document.forms[0].elements.length; i++) {

    //                if (document.forms[0].elements[i].type == "checkbox") {

    //                    if (document.forms[0].elements[i].checked == false) {

    //                        flag = 1;
    //                    }
    //                }
    //            }

    //            if (flag == 0) {

    //                checkBoxHeader.checked = true;
    //            }
    //        }
    //        else {

    //            checkBoxHeader.checked = false;
    //        }
    //    }

</script>

<asp:ScriptManager ID="ScriptManagerForPage" runat="server">
</asp:ScriptManager>
<table width="99%">
    <tr>
        <th align="left" style="font-size: 14px; padding-left: 10px; padding-top: 10px; color: #06569d;">
            <%--<h3 style="font-size: 13px;">--%>
            <%--<span style="font-weight: bold; color: #06569d; padding-left: 200px;">--%><u>Non-Valid
                Data</u><%--</span>--%>
            <%--</h3>--%>
        </th>
        <th align="right" style="padding-top: 10px;">
            <asp:ImageButton ID="ImageButtonBack" ToolTip="Back" runat="server" 
                ImageUrl="~/Images/back.gif" TabIndex="4" onclick="ImageButtonBack_Click" />
        </th>
    </tr>
    <tr>
        <td align="right" colspan="2" style="padding-right: 5px;">
            <asp:Button ID="ButtonDelete" CssClass="aspButton" runat="server" Text="Delete" OnClientClick="return confirm('Are you sure you wish to delete this entry?')"
                OnClick="ButtonDelete_Click" TabIndex="3" />
        </td>
    </tr>
    <tr>
        <td colspan="2" style="padding-top: 10px;">
            <div style="overflow: auto; width: 590px;">
                <asp:UpdatePanel ID="UpdatePanelForPage" runat="server" UpdateMode="Conditional"
                    ChildrenAsTriggers="true">
                    <ContentTemplate>
                        <asp:FormView ID="FormViewNurseDetail" runat="server" DataSourceID="ObjectDataSourceNurseDetail"
                            DataKeyNames="NurserDetailID" OnDataBound="FormViewNurseDetail_DataBound" TabIndex="2">
                            <EditItemTemplate>
                                <table>
                                    <tr>
                                        <td align="right" style="color: #06569d; font-weight: bold; font-size: 11px;">
                                            Last Location :
                                        </td>
                                        <td align="left">
                                            <asp:DropDownList ID="DropDownListLastLocation" runat="server" AppendDataBoundItems="True"
                                                CssClass="aspDropDownList" ForeColor="#06569D" DataSourceID="LinqDataSourceLastLocation"
                                                DataTextField="LastLocation1" DataValueField="LastLocationID" SelectedValue='<%# Bind("LastLocationID") %>'
                                                OnPreRender="DropDownListLastLocation_PreRender">
                                                <asp:ListItem Value="0">Select LastLocation</asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:LinqDataSource ID="LinqDataSourceLastLocation" runat="server" ContextTypeName="RMC.DataService.RMCDataContext"
                                                OrderBy="LastLocation1" TableName="LastLocations">
                                            </asp:LinqDataSource>
                                        </td>
                                        <td style="height: 5px">
                                        </td>
                                        <td align="right" style="color: #06569d; font-weight: bold; font-size: 11px;">
                                            Last Location :
                                        </td>
                                        <td align="left">
                                            <asp:TextBox ID="TextBoxLocation" CssClass="aspTextBox" runat="server" Text='<%#Eval("LocationName")%>'></asp:TextBox>
                                            <div style="visibility: hidden; display: none;">
                                                <asp:Literal ID="LiteralLocationID" runat="server" Text='<%#Eval("LocationID")%>'></asp:Literal>
                                            </div>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right" style="color: #06569d; font-weight: bold; font-size: 11px;">
                                            Activity :
                                        </td>
                                        <td align="left">
                                            <asp:TextBox ID="TextBoxActivity" CssClass="aspTextBox" runat="server" Text='<%#Eval("ActivityName")%>'></asp:TextBox>
                                            <div style="visibility: hidden; display: none;">
                                                <asp:Literal ID="LiteralActivityID" runat="server" Text='<%#Eval("ActivityID")%>'></asp:Literal>
                                            </div>
                                        </td>
                                        <td style="height: 5px">
                                        </td>
                                        <td align="right" style="color: #06569d; font-weight: bold; font-size: 11px;">
                                            Sub-Activity :
                                        </td>
                                        <td align="left">
                                            <asp:TextBox ID="TextBoxSubActivity" CssClass="aspTextBox" runat="server" Text='<%#Eval("SubActivityName")%>'></asp:TextBox>
                                            <div style="visibility: hidden; display: none;">
                                                <asp:Literal ID="LiteralSubActivityID" runat="server" Text='<%#Eval("SubActivityID")%>'></asp:Literal>
                                            </div>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right" style="color: #06569d; font-weight: bold; font-size: 11px;">
                                            Error 1 :
                                        </td>
                                        <td align="left">
                                            <asp:TextBox ID="TextBoxActiveError1" CssClass="aspTextBox" runat="server" Text='<%#Eval("ActiveError1")%>'></asp:TextBox>
                                        </td>
                                        <td style="height: 5px">
                                        </td>
                                        <td align="right" style="color: #06569d; font-weight: bold; font-size: 11px;">
                                            Error 2 :
                                        </td>
                                        <td align="left">
                                            <asp:TextBox ID="TextBoxActiveError2" CssClass="aspTextBox" runat="server" Text='<%#Eval("ActiveError2")%>'></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right" style="color: #06569d; font-weight: bold; font-size: 11px;">
                                            Error 3 :
                                        </td>
                                        <td align="left">
                                            <asp:TextBox ID="TextBoxActiveError3" CssClass="aspTextBox" runat="server" Text='<%#Eval("ActiveError3")%>'></asp:TextBox>
                                        </td>
                                        <td style="height: 5px">
                                        </td>
                                        <td align="right" style="color: #06569d; font-weight: bold; font-size: 11px;">
                                            Error 4 :
                                        </td>
                                        <td align="left">
                                            <asp:TextBox ID="TextBoxActiveError4" CssClass="aspTextBox" runat="server" Text='<%#Eval("ActiveError4")%>'></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right" style="color: #06569d; font-weight: bold; font-size: 11px;">
                                            Resource Requirement :
                                        </td>
                                        <td align="left">
                                            <asp:DropDownList ID="DropDownListResourceRequirement" runat="server" AppendDataBoundItems="True"
                                                CssClass="aspDropDownList" DataSourceID="LinqDataSourceResourceRequirement" DataTextField="ResourceRequirement1"
                                                DataValueField="ResourceRequirementID" ForeColor="#06569D" SelectedValue='<%# Eval("ResourceRequirementID") %>'>
                                                <asp:ListItem Value="0">Select ResourceRequirement</asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:LinqDataSource ID="LinqDataSourceResourceRequirement" runat="server" ContextTypeName="RMC.DataService.RMCDataContext"
                                                OrderBy="ResourceRequirement1" TableName="ResourceRequirements">
                                            </asp:LinqDataSource>
                                        </td>
                                        <td style="height: 5px">
                                        </td>
                                        <td align="right" style="color: #06569d; font-weight: bold; font-size: 11px;">
                                        </td>
                                        <td align="left">
                                            <asp:Button ID="ButtonActiveSave" runat="server" CssClass="aspButton" Text="Save"
                                                Width="70px" OnClick="ButtonActiveSave_Click" />
                                        </td>
                                    </tr>
                                </table>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <div id="divSelect">
                                    <table>
                                        <tr>
                                            <td align="left" style="color: #06569d; font-size: 11px; text-decoration: underline;
                                                font-weight: bold;" colspan="5">
                                                <asp:Literal ID="LiteralFileRef" Text='<%#Eval("FileName")%>' runat="server"></asp:Literal>
                                                (<asp:Literal ID="LiteralYear" Text='<%#Eval("Year")%>' runat="server"></asp:Literal>)
                                                (<asp:Literal ID="LiteralMonth" Text='<%#Eval("Month")%>' runat="server"></asp:Literal>)
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="height: 15px;">
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right" style="color: #06569d; font-weight: bold; font-size: 11px;">
                                                Last Location :
                                            </td>
                                            <td align="left">
                                                <asp:DropDownList ID="DropDownListLastLocation" runat="server" AppendDataBoundItems="True"
                                                    CssClass="aspDropDownList" ForeColor="#06569D" DataSourceID="LinqDataSourceLastLocation"
                                                    DataTextField="LastLocation1" DataValueField="LastLocationID" SelectedValue='<%# Bind("LastLocationID") %>'
                                                    OnPreRender="DropDownListLastLocation_PreRender" TabIndex="2">
                                                    <asp:ListItem Value="0">Select LastLocation</asp:ListItem>
                                                </asp:DropDownList>
                                                <asp:LinqDataSource ID="LinqDataSourceLastLocation" runat="server" ContextTypeName="RMC.DataService.RMCDataContext"
                                                    OrderBy="LastLocation1" TableName="LastLocations">
                                                </asp:LinqDataSource>
                                            </td>
                                            <td style="height: 5px">
                                            </td>
                                            <td align="right" style="color: #06569d; font-weight: bold; font-size: 11px;">
                                                Location :
                                            </td>
                                            <td align="left">
                                                <asp:DropDownList ID="DropDownListLocation" runat="server" AppendDataBoundItems="True"
                                                    CssClass="aspDropDownList" ForeColor="#06569D" AutoPostBack="True" OnSelectedIndexChanged="DropDownListLocation_SelectedIndexChanged" TabIndex="2">
                                                    <asp:ListItem Value="0">Select Location</asp:ListItem>
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right" style="color: #06569d; font-weight: bold; font-size: 11px;">
                                                Activity :
                                            </td>
                                            <td align="left">
                                                <asp:DropDownList ID="DropDownListActivity" runat="server" AppendDataBoundItems="True"
                                                    CssClass="aspDropDownList" ForeColor="#06569D" AutoPostBack="True" OnSelectedIndexChanged="DropDownListActivity_SelectedIndexChanged" TabIndex="2">
                                                    <asp:ListItem Value="0" style="color: Red;">Select Activity</asp:ListItem>
                                                </asp:DropDownList>
                                            </td>
                                            <td style="height: 5px">
                                            </td>
                                            <td align="right" style="color: #06569d; font-weight: bold; font-size: 11px;">
                                                Sub-Activity :
                                            </td>
                                            <td align="left">
                                                <asp:DropDownList ID="DropDownListSubActivity" runat="server" AppendDataBoundItems="True"
                                                    CssClass="aspDropDownList" ForeColor="#06569D" TabIndex="2">
                                                    <asp:ListItem Value="0" style="color: Red;">Select SubActivity</asp:ListItem>
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right" style="color: #06569d; font-weight: bold; font-size: 11px;" rowspan="2"
                                                valign="top" rowspan="2">
                                                Cognitive Category :
                                            </td>
                                            <td align="left" rowspan="2">
                                                <asp:ListBox ID="ListBoxCognitiveCategory" runat="server" DataSourceID="LinqDataSourceCognitiveCategory"
                                                    DataTextField="CognitiveCategoryText" DataValueField="CognitiveCategoryID" ForeColor="#06569D"
                                                    SelectionMode="Multiple" Font-Size="10px" OnPreRender="ListBoxCognitiveCategory_PreRender" TabIndex="2">
                                                    <%--OnPreRender="ListBoxCognitiveCategory_PreRender"--%>
                                                </asp:ListBox>
                                                <asp:LinqDataSource ID="LinqDataSourceCognitiveCategory" runat="server" ContextTypeName="RMC.DataService.RMCDataContext"
                                                    OrderBy="CognitiveCategoryText" TableName="CognitiveCategories">
                                                </asp:LinqDataSource>
                                                <div style="visibility: hidden; display: none;">
                                                    <asp:TextBox ID="TextBoxCognitiveCategory" CssClass="aspTextBox" runat="server" Text='<%#Eval("CognitiveCategory") %>'></asp:TextBox></div>
                                            </td>
                                            <td style="height: 5px">
                                            </td>
                                            <td align="right" style="color: #06569d; font-weight: bold; font-size: 11px;" valign="top">
                                                Resource Requirement:
                                            </td>
                                            <td align="left" valign="top">
                                                <asp:DropDownList ID="DropDownListResourceRequirement" runat="server" AppendDataBoundItems="True"
                                                    CssClass="aspDropDownList" DataSourceID="LinqDataSourceResourceRequirement" DataTextField="ResourceRequirement1"
                                                    DataValueField="ResourceRequirementID" ForeColor="#06569D" SelectedValue='<%# Eval("ResourceRequirementID") %>' TabIndex="2">
                                                    <asp:ListItem Value="0">Select ResourceRequirement</asp:ListItem>
                                                </asp:DropDownList>
                                                <asp:LinqDataSource ID="LinqDataSourceResourceRequirement" runat="server" ContextTypeName="RMC.DataService.RMCDataContext"
                                                    OrderBy="ResourceRequirement1" TableName="ResourceRequirements">
                                                </asp:LinqDataSource>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right" colspan="3">
                                                <asp:Button ID="ButtonSave" runat="server" CssClass="aspButton" OnClick="ButtonSave_Click"
                                                    Text="Save" Width="70px" TabIndex="2" />
                                                <asp:Button ID="ButtonEdit" runat="server" Text="Edit" OnClientClick="return toggleDiv(1);"
                                                    Width="70px" CssClass="aspButton" TabIndex="2"/>
                                            </td>
                                            <%--<td style="height: 5px">
                                            </td>
                                            <td align="right" style="color: #06569d; font-weight: bold; font-size: 11px;">
                                            
                                            </td>--%>
                                        </tr>
                                        <tr>
                                            <td colspan="5">
                                                &nbsp;
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                                <div id="divEdit" style="visibility: hidden; display: none;">
                                    <table>
                                        <tr>
                                            <td align="right" style="color: #06569d; font-weight: bold; font-size: 11px;">
                                                Last Location :
                                            </td>
                                            <td align="left">
                                                <asp:TextBox ID="TextBoxLastLocation" CssClass="aspTextBox" runat="server" Text='<%#Eval("LastLocationName")%>'></asp:TextBox>
                                                <div style="visibility: hidden; display: none;">
                                                    <asp:Literal ID="LiteralLastLocationID" runat="server" Text='<%#Eval("LastLocationID")%>'></asp:Literal>
                                                </div>
                                            </td>
                                            <td style="height: 5px">
                                            </td>
                                            <td align="right" style="color: #06569d; font-weight: bold; font-size: 11px;">
                                                Last Location :
                                            </td>
                                            <td align="left">
                                                <asp:TextBox ID="TextBoxLocation" CssClass="aspTextBox" runat="server" Text='<%#Eval("LocationName")%>'></asp:TextBox>
                                                <div style="visibility: hidden; display: none;">
                                                    <asp:Literal ID="Literal1" runat="server" Text='<%#Eval("LocationID")%>'></asp:Literal>
                                                </div>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right" style="color: #06569d; font-weight: bold; font-size: 11px;">
                                                Activity :
                                            </td>
                                            <td align="left">
                                                <asp:TextBox ID="TextBoxActivity" CssClass="aspTextBox" runat="server" Text='<%#Eval("ActivityName")%>'></asp:TextBox>
                                                <div style="visibility: hidden; display: none;">
                                                    <asp:Literal ID="Literal2" runat="server" Text='<%#Eval("ActivityID")%>'></asp:Literal>
                                                </div>
                                            </td>
                                            <td style="height: 5px">
                                            </td>
                                            <td align="right" style="color: #06569d; font-weight: bold; font-size: 11px;">
                                                Sub-Activity :
                                            </td>
                                            <td align="left">
                                                <asp:TextBox ID="TextBoxSubActivity" CssClass="aspTextBox" runat="server" Text='<%#Eval("SubActivityName")%>'></asp:TextBox>
                                                <div style="visibility: hidden; display: none;">
                                                    <asp:Literal ID="Literal3" runat="server" Text='<%#Eval("SubActivityID")%>'></asp:Literal>
                                                </div>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right" style="color: #06569d; font-weight: bold; font-size: 11px;">
                                                Resource Requirement :
                                            </td>
                                            <td align="left">
                                                <asp:TextBox ID="TextBoxResource" CssClass="aspTextBox" runat="server" Text='<%#Eval("ResourceText")%>'></asp:TextBox>
                                                <div style="visibility: hidden; display: none;">
                                                    <asp:Literal ID="LiteralResourceRequirementID" runat="server" Text='<%#Eval("ResourceRequirementID")%>'></asp:Literal>
                                                </div>
                                            </td>
                                            <%--<td style="height: 5px">
                                            </td>
                                            <td align="right" style="color: #06569d; font-weight: bold; font-size: 11px;">
                                            </td>--%>
                                            <td align="right" colspan="3">
                                                <asp:Button ID="ButtonTextSave" runat="server" CssClass="aspButton" OnClick="ButtonTextSave_Click"
                                                    Text="Save" Width="70px" />
                                                <asp:Button ID="ButtonCancel" runat="server" Text="Cancel" OnClientClick="return toggleDiv(0);"
                                                    Width="70px" CssClass="aspButton" />
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                                <div style="visibility: hidden; display: none;">
                                    <asp:Literal ID="LiteralLocationID" Text='<%#Eval("LocationID")%>' runat="server"></asp:Literal>
                                    <asp:Literal ID="LiteralActivityID" Text='<%#Eval("ActivityID")%>' runat="server"></asp:Literal>
                                    <asp:Literal ID="LiteralActivityName" Text='<%#Eval("ActivityName")%>' runat="server"></asp:Literal>
                                    <asp:Literal ID="LiteralSubActivityID" Text='<%#Eval("SubActivityID")%>' runat="server"></asp:Literal>
                                    <asp:Literal ID="LiteralSubActivityName" Text='<%#Eval("SubActivityName")%>' runat="server"></asp:Literal>
                                    <asp:Literal ID="LiteralIsErrorExist" Text='<%#Eval("IsErrorExist")%>' runat="server"></asp:Literal>
                                    <asp:Literal ID="LiteralIsActiveError" Text='<%#Eval("IsActiveError")%>' runat="server"></asp:Literal>
                                    <asp:Literal ID="LiteralIsErrorInLocation" Text='<%#Eval("IsErrorInLocation")%>'
                                        runat="server"></asp:Literal>
                                    <asp:Literal ID="LiteralIsErrorInActivity" Text='<%#Eval("IsErrorInActivity")%>'
                                        runat="server"></asp:Literal>
                                    <asp:Literal ID="LiteralIsErrorInSubActivity" Text='<%#Eval("IsErrorInSubActivity")%>'
                                        runat="server"></asp:Literal>
                                </div>
                            </ItemTemplate>
                        </asp:FormView>
                        <asp:ObjectDataSource ID="ObjectDataSourceNurseDetail" runat="server" SelectMethod="GetNonValidDataByNurseDetailID"
                            TypeName="RMC.BussinessService.BSNursePDADetail">
                            <SelectParameters>
                                <asp:ControlParameter ControlID="GridViewViewNonValidData" DefaultValue="0" Name="nurseDetailID"
                                    PropertyName="SelectedValue" Type="Int32" />
                            </SelectParameters>
                        </asp:ObjectDataSource>
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="GridViewViewNonValidData" EventName="SelectedIndexChanged" />
                    </Triggers>
                </asp:UpdatePanel>
            </div>
        </td>
    </tr>
    <tr>
        <td colspan="2" align="left" style="padding-top: 10px;">
            <div id="divGridView" style="overflow: auto; width: 632px; height: 350px;">
                <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="true">
                    <ContentTemplate>
                        <asp:GridView ID="GridViewViewNonValidData" runat="server" CssClass="GridViewStyle"
                            CellPadding="5" EmptyDataText="No Record to display." GridLines="None" AutoGenerateColumns="False"
                            DataKeyNames="NurserDetailID" HorizontalAlign="Left" DataSourceID="ObjectDataSourceViewNonValidData"
                            OnSelectedIndexChanged="GridViewViewNonValidData_SelectedIndexChanged" TabIndex="1">
                            <HeaderStyle CssClass="GridViewHeaderStyle" />
                            <EmptyDataRowStyle CssClass="GridViewEmptyRowStyle" />
                            <Columns>
                                <asp:CommandField ShowSelectButton="true" ButtonType="Image" SelectImageUrl="~/Images/iconEdit_e.gif" />
                                <asp:TemplateField>
                                    <%--<HeaderTemplate>
                                <input id="CheckboxHeader" type="checkbox" onclick="checkAllCheckboxes(this);" />
                            </HeaderTemplate>--%>
                                    <ItemTemplate>
                                        <%--<asp:CheckBox ID="CheckBoxRow" runat="server" onclick="toggleHeaderCheckBox(this);" />--%>
                                        <asp:CheckBox ID="CheckBoxRow" runat="server" TabIndex="1" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Last Location">
                                    <ItemTemplate>
                                        <%#Eval("LastLocationName")%>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Location">
                                    <ItemTemplate>
                                        <%#Eval("LocationName")%>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Activity">
                                    <ItemTemplate>
                                        <%#Eval("ActivityName")%>
                                        <div style="visibility: hidden; display: none">
                                            <%#Eval("IsActiveError")%></div>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Sub-Activity">
                                    <ItemTemplate>
                                        <%#Eval("SubActivityName")%>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Error 1">
                                    <ItemTemplate>
                                        <%#Eval("ActiveError1")%>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Error 2">
                                    <ItemTemplate>
                                        <%#Eval("ActiveError2")%>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Error 3">
                                    <ItemTemplate>
                                        <%#Eval("ActiveError3")%>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Error 4">
                                    <ItemTemplate>
                                        <%#Eval("ActiveError4")%>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Resource Requirement">
                                    <ItemTemplate>
                                        <%#Eval("ResourceText")%>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Cognitive Category">
                                    <ItemTemplate>
                                        <%#Eval("CognitiveCategory")%>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                            <FooterStyle CssClass="GridViewFooterStyle" />
                            <PagerStyle CssClass="GridViewPagerStyle" />
                            <SelectedRowStyle CssClass="GridViewSelectedRowStyle" />
                            <AlternatingRowStyle CssClass="GridViewAlternatingRowStyle" />
                            <RowStyle CssClass="GridViewRowStyle" />
                        </asp:GridView>
                        <asp:ObjectDataSource ID="ObjectDataSourceViewNonValidData" runat="server" SelectMethod="GetNonValidDataByHospitalUnitID"
                            TypeName="RMC.BussinessService.BSNursePDADetail">
                            <SelectParameters>
                                <asp:QueryStringParameter DefaultValue="0" Name="hospitalUntiID" QueryStringField="HospitalUnitID"
                                    Type="Int32" />
                            </SelectParameters>
                        </asp:ObjectDataSource>
                    </ContentTemplate>
                    <%--<Triggers>
                        <asp:AsyncPostBackTrigger ControlID="GridViewViewNonValidData" EventName="SelectedIndexChanged" />
                    </Triggers>--%>
                </asp:UpdatePanel>
            </div>
        </td>
    </tr>
    <%--<tr>
        <td align="left" style="padding-top: 5px;">
            
        </td>
    </tr>--%>
</table>
<div id="ParentDiv" class="Background" style="visibility: hidden;">
    <div id="IMGDIV" align="center" valign="middle" style="position: absolute; left: 45%;
        top: 50%; visibility: hidden; vertical-align: middle; border-style: none; border-color: black;
        z-index: 40;">
        <asp:Image ID="Image2" runat="server" ImageUrl="~/Images/Loading.gif" BorderWidth="0px"
            AlternateText="Loading"></asp:Image>
    </div>
</div>
