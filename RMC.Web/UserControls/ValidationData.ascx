<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ValidationData.ascx.cs"
    Inherits="RMC.Web.UserControls.ValidationData" %>
    <script type="text/javascript">
        function DeleteAllConfirm() {
        
            if (confirm('Are you sure want to delete all records?')) {                
                return true;
            }
            else {
                return false;
            }
        }

        function DeleteSelectedConfirm() {

            if (confirm('Are you sure want to delete selected records?')) {
                return true;
            }
            else {
                return false;
            }
        }
        
    </script>
<table>
    <tr>
        <td align="left" style="width:125px">
            <asp:Button Font-Bold="true" Font-Size="11px" runat="server" 
                Width="110px" CssClass="aspButton" ID="ButtonExportExcelSheet" 
                OnClick="ButtonExportExcelSheet_Click" TabIndex="1" Text="Export Excel Sheet" />
            &nbsp;&nbsp;
            <asp:Button ID="ButtonImportExcelSheet" Font-Bold="true" Font-Size="11px" CssClass="aspButton" PostBackUrl="~/Administrator/ImportExcelSheet.aspx"
                Width="110px" runat="server" TabIndex="2" Text="Import Excel Sheet" />
            &nbsp;&nbsp;
            <asp:Button ID="ButtonDeleteAll" runat="server" Font-Size="11px" CssClass="aspButton" OnClientClick="return DeleteAllConfirm();"
                Width="80px" onclick="ButtonDeleteAll_Click" TabIndex="3" Text="Delete All" />
                &nbsp;&nbsp;
            <asp:Button ID="ButtonDeleteSelected" runat="server" Font-Size="11px" CssClass="aspButton" OnClientClick="return DeleteSelectedConfirm();"
                Width="100px" onclick="ButtonDeleteSelected_Click" TabIndex="4" Text="Delete Selected" />
                &nbsp;&nbsp;
            <asp:Button ID="ButtonSortAll" runat="server" Font-Size="11px" CssClass="aspButton" 
               Width="50px" TabIndex="4" onclick="ButtonSortAll_Click" Text="Sort All" />
        </td>
    </tr>
    <tr>
        <td>
            <table>
                 <tr>
                    <td>
                        <asp:Label Font-Size="11px" Font-Underline="False" ForeColor="#06569D"  ID="LabelLocation" Text="Location" style="Font-Size:11px" runat="server"></asp:Label>  
                    </td>
                    <td>
                        <asp:Label Font-Size="11px" Font-Underline="False" ForeColor="#06569D"  ID="LabelActivity" Text="Activity" style="Font-Size:11px" runat="server"></asp:Label>
                    </td>
                    <td>
                        <asp:Label Font-Size="11px" Font-Underline="False" ForeColor="#06569D"  ID="LabelSubActivity" Text="SubActivity" style="Font-Size:11px" runat="server"></asp:Label>
                    </td>
                 </tr>
                 <tr>
                    <td>
                        <asp:TextBox ID="TextBoxLocation" CssClass="aspTextBox" runat="server" TabIndex="4"></asp:TextBox>
                    </td>
                    <td>
                        
                        <asp:TextBox ID="TextBoxActivity" CssClass="aspTextBox" runat="server" TabIndex="5"></asp:TextBox>
                    </td>
                    <td>
                        
                        <asp:TextBox ID="TextBoxSubActivity" CssClass="aspTextBox" runat="server" TabIndex="6"></asp:TextBox>
                    </td>
                 
                    <td>
                        <asp:LinkButton ID="LinkButtonInsert" runat="server" style="Font-Size:11px" OnClick="LinkButtonInsert_Click" TabIndex="7">Insert</asp:LinkButton>
                    </td>
                </tr>
            </table>
        </td>
    </tr>
    <tr>
        <td colspan="3">
            <div style="height: 500px; overflow: auto; border: 1px double #06569d; width: 516px;">
            <asp:GridView ID="GridViewValidation" runat="server" Width="500px" CellPadding="3"
                EnableViewState="true" EmptyDataText="No Record to display." GridLines="None"
                CssClass="GridViewStyle" AutoGenerateColumns="False" DataKeyNames="ValidationID"
                HorizontalAlign="Left" DataSourceID="ObjectDataSourceValidationData" OnRowCommand="GridViewValidation_RowCommand" OnPreRender="GridViewValidation_PreRender">
                <HeaderStyle CssClass="GridViewHeaderStyle" />
                <EmptyDataRowStyle CssClass="GridViewEmptyRowStyle" />
                <Columns>
                    <asp:TemplateField>
                        <ItemTemplate>
                            <asp:CheckBox ID="CheckBoxDelete" runat="server" EnableViewState="true" />
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Location">
                        <ItemTemplate>
                            <%#Eval("Location") %>
                            <div style="visibility: hidden; display: none;">
                                <asp:Label ID="LabelLocation" runat="server" Text='<%#Eval("Location") %>'></asp:Label>
                            </div>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:DropDownList ID="DropDownListLocation" runat="server" AppendDataBoundItems="True"
                                DataSourceID="ObjectDataSourceLocation" DataTextField="Location1" DataValueField="LocationID"
                                ForeColor="#06569D" SelectedValue='<%# Bind("LocationID") %>' CssClass="aspDropDownList" Width="100px" TabIndex="6">
                                <asp:ListItem Value="0">Select Location</asp:ListItem>
                            </asp:DropDownList>
                            <asp:ObjectDataSource ID="ObjectDataSourceLocation" runat="server" SelectMethod="GetAllLocation"
                                TypeName="RMC.BussinessService.BSLocation"></asp:ObjectDataSource>
                            <asp:TextBox ID="TextBoxLocation" CssClass="aspTextBox" Width="100px" runat="server" Visible="false"
                                Text='<%#Bind("Location")%>' TabIndex="6"></asp:TextBox>
                            <div style="display: none; visibility: hidden;">
                                <asp:Literal ID="LiteralLocationID" Text='<%# Eval("LocationID") %>' runat="server"></asp:Literal>
                            </div>
                        </EditItemTemplate>
                        <HeaderTemplate>
                            <%--<asp:Literal ID="LiteralLocation" Text="Location" runat="server"></asp:Literal>--%>
                            <asp:LinkButton id="lnkbtnLocation" runat="server" CommandName="Sort"
                                                        CommandArgument="Location">Location</asp:LinkButton> 
                           <%-- <asp:TextBox ID="TextBoxLocation" CssClass="aspTextBox" runat="server" TabIndex="4"></asp:TextBox>--%>
                        </HeaderTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Activity">
                        <ItemTemplate>
                            <%#Eval("Activity") %>
                            <div style="visibility: hidden; display: none;">
                                <asp:Label ID="LabelActivity" runat="server" Text='<%#Eval("Activity") %>'></asp:Label>
                            </div>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:DropDownList ID="DropDownListActivity" runat="server" AppendDataBoundItems="True"
                                DataSourceID="ObjectDataSourceActivity" DataTextField="Activity1" DataValueField="ActivityID"
                                ForeColor="#06569D" SelectedValue='<%# Bind("ActivityID") %>' CssClass="aspDropDownList" Width="100px" TabIndex="6">
                                <asp:ListItem Value="0">Select Activity</asp:ListItem>
                            </asp:DropDownList>
                            <asp:ObjectDataSource ID="ObjectDataSourceActivity" runat="server" SelectMethod="GetAllActivity"
                                TypeName="RMC.BussinessService.BSActivity"></asp:ObjectDataSource>
                            <asp:TextBox ID="TextBoxActivity" CssClass="aspTextBox" Width="100px" runat="server" Visible="false"
                                Text='<%#Bind("Activity")%>' TabIndex="6"></asp:TextBox>
                            <div style="display: none; visibility: hidden;">
                                <asp:Literal ID="LiteralActivityID" Text='<%# Eval("ActivityID") %>' runat="server"></asp:Literal>
                            </div>
                        </EditItemTemplate>
                        <HeaderTemplate>
                            <%--<asp:Literal ID="LiteralActivity" Text="Activity" runat="server"></asp:Literal>--%>
                             <asp:LinkButton id="lnkbtnActivity" runat="server" CommandName="Sort"
                                                        CommandArgument="Activity">Activity</asp:LinkButton> 
                            <%--<asp:TextBox ID="TextBoxActivity" CssClass="aspTextBox" runat="server" TabIndex="4"></asp:TextBox>--%>
                        </HeaderTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="SubActivity">
                        <ItemTemplate>
                            <%#Eval("SubActivity") %>
                            <div style="visibility: hidden; display: none;">
                                <asp:Label ID="LabelSubActivity" runat="server" Text='<%#Eval("SubActivity") %>'></asp:Label>
                            </div>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:DropDownList ID="DropDownListSubActivity" runat="server" AppendDataBoundItems="True"
                                DataSourceID="ObjectDataSourceSubActivity" DataTextField="SubActivity1" DataValueField="SubActivityID"
                                ForeColor="#06569D" SelectedValue='<%# Bind("SubActivityID") %>' CssClass="aspDropDownList" Width="100px" TabIndex="6">
                                <asp:ListItem Value="0">Select SubActivity</asp:ListItem>
                            </asp:DropDownList>
                            <asp:ObjectDataSource ID="ObjectDataSourceSubActivity" runat="server" SelectMethod="GetAllSubActivity"
                                TypeName="RMC.BussinessService.BSSubActivity"></asp:ObjectDataSource>
                            <asp:TextBox ID="TextBoxSubActivity" CssClass="aspTextBox" Width="100px" runat="server" Visible="false"
                                Text='<%#Bind("SubActivity")%>' TabIndex="6"></asp:TextBox>
                            <div style="display: none; visibility: hidden;">
                                <asp:Literal ID="LiteralSubActivityID" Text='<%# Eval("SubActivityID") %>' runat="server"></asp:Literal>
                            </div>
                        </EditItemTemplate>
                        <HeaderTemplate>
                            <%--<asp:Literal ID="LiteralSubActivity" Text="SubActivity" runat="server"></asp:Literal>--%>
                            <asp:LinkButton id="lnkbtnSubActivity" runat="server" CommandName="Sort"
                                                        CommandArgument="SubActivity">SubActivity</asp:LinkButton>
                            <%--<asp:TextBox ID="TextBoxSubActivity" CssClass="aspTextBox" runat="server" TabIndex="4"></asp:TextBox>--%>
                        </HeaderTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField>
                        <HeaderTemplate>
                            &nbsp
                           <%-- <asp:LinkButton ID="LinkButtonInsert" runat="server" OnClick="LinkButtonInsert_Click" TabIndex="4">Insert</asp:LinkButton>--%>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <table>
                                <tr>
                                    <td>
                                        <asp:LinkButton ID="LinkButtonChange" runat="server" OnClick="LinkButtonChange_Click" TabIndex="5">Change</asp:LinkButton>
                                    </td>
                                    <td>
                                        <asp:LinkButton ID="LinkButtonEdit" runat="server" OnClick="LinkButtonEdit_Click" TabIndex="5">Edit</asp:LinkButton>
                                    </td>
                                    <td>
                                        <asp:LinkButton ID="LinkButtonDelete" runat="server" OnClick="LinkButtonDelete_Click"
                                            OnClientClick="return confirm('Are you sure you wish to delete this entry?')" TabIndex="5">Delete</asp:LinkButton>
                                    </td>
                                </tr>
                            </table>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <table>
                                <tr>
                                    <td>
                                        <asp:LinkButton ID="LinkButtonEditUpdate" runat="server" Visible="false" OnClick="LinkButtonEditUpdate_Click" TabIndex="6">Update</asp:LinkButton>
                                        <asp:LinkButton ID="LinkButtonUpdate" runat="server" OnClick="LinkButtonUpdate_Click"
                                            Visible="false" TabIndex="6">Update</asp:LinkButton>
                                    </td>
                                    <td>
                                        <asp:LinkButton ID="LinkButtonCancel" runat="server" OnClick="LinkButtonCancel_Click" TabIndex="6">Cancel</asp:LinkButton>
                                    </td>
                                </tr>
                            </table>
                        </EditItemTemplate>
                    </asp:TemplateField>

                </Columns>
                <FooterStyle CssClass="GridViewFooterStyle" />
                <PagerStyle CssClass="GridViewPagerStyle" />
                <SelectedRowStyle CssClass="GridViewSelectedRowStyle" />
                <AlternatingRowStyle CssClass="GridViewAlternatingRowStyle" />
                <RowStyle CssClass="GridViewRowStyle" />
            </asp:GridView>
            </div>
        </td>        
    </tr>
</table>
<asp:ObjectDataSource ID="ObjectDataSourceValidationData" OnSelecting="ObjectDataSourceValidationData_Selecting" runat="server" SelectMethod="GetValidationData"
    TypeName="RMC.BussinessService.BSValidationData">
    <SelectParameters>
        <asp:Parameter DefaultValue=" " Name="sortExpression" Type="String" />
        <asp:Parameter DefaultValue=" " Name="sortOrder" Type="String" />
    </SelectParameters>    
</asp:ObjectDataSource>
