<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="NationalDatabase.ascx.cs"
    Inherits="RMC.Web.UserControls.NationalDatabase" %>
<div>
    <table>
        <tr>
            <td align="right">
            </td>
        </tr>
        <tr>
            <td style="padding-left: 10px;">
                <asp:MultiView ID="MultiView1" runat="server">
                    <asp:View ID="ViewInsertNationalDatabase" runat="server">
                        <table>
                            <tr>
                                <td colspan="2">
                                    <asp:Label ID="Label1" runat="server" Text="Manage National Database" ForeColor="#06569d"
                                        Font-Bold="true" Font-Size="14px" Font-Underline="true"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td style="height: 10px;">
                                </td>
                                <td>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2">
                                    <asp:Label ID="LabelInsert" runat="server" Text="Insert Records With Values Into National Database"
                                        ForeColor="#06569d" Font-Bold="true" Font-Size="11px" Font-Underline="true"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2">
                                    <asp:GridView ID="GridViewNationalDataBase" runat="server" DataSourceID="ObjectDataSourceNationalDatabase"
                                        Width="500px" AutoGenerateColumns="False" Height="400px" CellPadding="3" HorizontalAlign="Center"
                                        CssClass="GridViewStyle" EmptyDataText="No Record to display." OnRowCreated="GridViewNationalDataBase_RowCreated">
                                        <HeaderStyle CssClass="GridViewHeaderStyle" />
                                        <EmptyDataRowStyle CssClass="GridViewEmptyRowStyle" />
                                        <Columns>
                                            <asp:BoundField DataField="ProfileType" HeaderText="ProfileType" SortExpression="ProfileType" />
                                            <asp:BoundField DataField="FunctionType" HeaderText="FunctionType" SortExpression="FunctionType" />
                                            <asp:BoundField DataField="FunctionTypeId" HeaderText="FunctionTypeId" SortExpression="FunctionTypeId" />
                                            <asp:BoundField DataField="GroupSequence" HeaderText="GroupSequence" SortExpression="GroupSequence" />
                                            <asp:BoundField DataField="GroupSequenceName" HeaderText="GroupSequenceName" SortExpression="GroupSequenceName" />
                                            <asp:BoundField DataField="ValueText" HeaderText="ValueText" SortExpression="ValueText"
                                                Visible="false" />
                                            <asp:BoundField DataField="Value" HeaderText="Value" SortExpression="Value" Visible="false" />
                                            <asp:TemplateField HeaderText="Value" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="TextBoxValue" runat="server" CssClass="aspTextBox"></asp:TextBox>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                </td>
                            </tr>
                            <tr>
                                <td align="Left">
                                    <asp:Button ID="ButtonToUpdateData" runat="server" Text="To Update Data" CssClass="aspButton"
                                        Width="100px" OnClick="ButtonToUpdateData_Click" />
                                </td>
                                <td align="right">
                                    <asp:Button ID="ButtonSave" runat="server" Text="Save" ValidationGroup="Save" CssClass="aspButton"
                                        Width="55px" OnClick="ButtonSave_Click" />
                                </td>
                            </tr>
                        </table>
                    </asp:View>
                    <asp:View ID="ViewUpdateNationalDatabase" runat="server">
                        <table>
                            <tr>
                                <td colspan="2">
                                    <asp:Label ID="Label2" runat="server" Text="Manage National Database" ForeColor="#06569d"
                                        Font-Bold="true" Font-Size="14px" Font-Underline="true"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td style="height: 10px;">
                                </td>
                                <td>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2">
                                    <asp:Label ID="LabelUpdate" runat="server" Text="Update Values In National Database"
                                        ForeColor="#06569d" Font-Bold="true" Font-Size="11px" Font-Underline="true"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2">
                                    <asp:GridView ID="GridViewUpdateNationalDatabase" runat="server" AutoGenerateColumns="False"
                                        DataSourceID="ObjectDataSourceUpdateNationalDatabase" Height="400px" CellPadding="3"
                                        HorizontalAlign="Center" CssClass="GridViewStyle" EmptyDataText="No Record to display."
                                        OnRowCreated="GridViewUpdateNationalDatabase_RowCreated">
                                        <HeaderStyle CssClass="GridViewHeaderStyle" />
                                        <EmptyDataRowStyle CssClass="GridViewEmptyRowStyle" />
                                        <Columns>
                                            <asp:BoundField DataField="Id" HeaderText="Id" SortExpression="Id" />
                                            <asp:BoundField DataField="ProfileType" HeaderText="ProfileType" SortExpression="ProfileType" />
                                            <asp:BoundField DataField="FunctionType" HeaderText="FunctionType" SortExpression="FunctionType" />
                                            <asp:BoundField DataField="FunctionTypeId" HeaderText="FunctionTypeId" SortExpression="FunctionTypeId" />
                                            <asp:BoundField DataField="GroupSequence" HeaderText="GroupSequence" SortExpression="GroupSequence" />
                                            <asp:BoundField DataField="GroupSequenceName" HeaderText="GroupSequenceName" SortExpression="GroupSequenceName" />
                                            <asp:BoundField DataField="ValueText" HeaderText="ValueText" SortExpression="ValueText"
                                                Visible="false" />
                                            <asp:BoundField DataField="Value" HeaderText="Current Value" SortExpression="Value" />
                                            <asp:TemplateField HeaderText="New Value" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="TextBoxNewValue" runat="server" CssClass="aspTextBox"></asp:TextBox>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                </td>
                                <td>
                                </td>
                            </tr>
                            <tr>
                                <td align="left">
                                    <asp:Button ID="ButtonToInsertData" runat="server" Text="To Insert Data" CssClass="aspButton"
                                        Width="90px" OnClick="ButtonToInsertData_Click" />
                                </td>
                                <td align="right">
                                    <asp:Button ID="ButtonUpdate" runat="server" Text="Update" CssClass="aspButton" Width="55px"
                                        OnClick="ButtonUpdate_Click" />
                                </td>
                            </tr>
                        </table>
                    </asp:View>
                </asp:MultiView>
            </td>
        </tr>
        <tr>
            <td>
            </td>
        </tr>
        <tr>
            <td align="right">
            </td>
        </tr>
    </table>
</div>
<br />
<asp:ObjectDataSource ID="ObjectDataSourceNationalDatabase" runat="server" SelectMethod="GetNationalDatabase"
    TypeName="RMC.BussinessService.BSNationalDatabase"></asp:ObjectDataSource>
<asp:ObjectDataSource ID="ObjectDataSourceUpdateNationalDatabase" runat="server"
    SelectMethod="GetNationalDatabaseForUpdate" TypeName="RMC.BussinessService.BSNationalDatabase">
</asp:ObjectDataSource>
<asp:ValidationSummary ID="ValidationSummaryNationalDatabase" runat="server" DisplayMode="List"
    ShowMessageBox="True" ValidationGroup="Save" ShowSummary="false" Font-Size="11px"
    Font-Bold="true" Style="padding-top: 1px;" />
