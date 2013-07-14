<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="StandardizeLocationName.ascx.cs"
    Inherits="RMC.Web.UserControls.StandardizeLocationName" %>
<table width="100%">
    <tr>
        <td colspan="4" align="left" style="font-size: 14px; padding-left: 20px; padding-top: 10px;
            color: #06569d; font-weight: bold;">
            <u>Standardize Location Names</u>
        </td>
    </tr>
    <tr>
        <td colspan="4" align="right" style="padding-right: 5px;">
            <asp:ImageButton ID="ImageButtonBack" ToolTip="Back" runat="server" ImageUrl="~/Images/back.gif"
                TabIndex="6" OnClick="ImageButtonBack_Click" />
        </td>
    </tr>
    <tr>
        <td colspan="4">
            <div style="background-image: url('../Images/FileListHeader.gif'); color: White;
                height: 18px; font-size: 13px; font-weight: bold; vertical-align: middle; padding-left: 10px;
                padding-bottom: 4px; padding-top: 5px;">
                <asp:Label ID="LabelFilter" runat="server" Text="" ForeColor="White" Font-Bold="true"
                    Font-Size="12px"></asp:Label><br />
                <asp:Label ID="LabelBegigningDate" runat="server" Text="" ForeColor="White" Font-Bold="true"
                    Font-Size="12px"></asp:Label>
                <asp:Label ID="LabelEndingDate" runat="server" Text="" ForeColor="White" Font-Bold="true"
                    Font-Size="12px"></asp:Label>
            </div>
        </td>
    </tr>
    <tr>
    <td colspan="4" style="height: 6px;"></td>
    </tr>
    <tr>
        <%--<td align="center" colspan="2">
            <asp:GridView ID="GridViewLastLocation" runat="server" Width="400px" CellPadding="3"
                EmptyDataText="No Record to display." GridLines="None" CssClass="GridViewStyle"
                AutoGenerateColumns="False" HorizontalAlign="Center" DataSourceID="ObjectDataSourceLastLocation"
                DataKeyNames="LastLocationID" OnPreRender="GridViewLastLocation_PreRender">
                <HeaderStyle CssClass="GridViewHeaderStyle" />
                <EmptyDataRowStyle CssClass="GridViewEmptyRowStyle" />
                <Columns>
                    <asp:TemplateField HeaderText="Last Location">
                        <ItemTemplate>
                            <%#Eval("LastLocation1") %>
                            <div style="visibility: hidden; display: none;">
                                <asp:Label ID="LabelLastLocation" runat="server" Text='<%#Eval("LastLocation1") %>'></asp:Label>
                            </div>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Rename Last Location">
                        <ItemTemplate>
                            <%#Eval("RenameLastLocation") %>
                            <div style="visibility: hidden; display: none;">
                                <asp:Label ID="LabelRenameLastLocation" runat="server" Text='<%#Eval("RenameLastLocation") %>'></asp:Label>
                            </div>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:DropDownList ID="DropDownListLastLocation" runat="server" AppendDataBoundItems="True"
                                DataSourceID="ObjectDataSourceLastLocation" DataTextField="LastLocation1" DataValueField="LastLocationID"
                                ForeColor="#06569D" SelectedValue='<%# Bind("LastLocationID") %>' CssClass="aspDropDownList"
                                TabIndex="6">
                                <asp:ListItem Value="0">Select Location</asp:ListItem>
                            </asp:DropDownList>
                            <div style="display: none; visibility: hidden;">
                                <asp:Literal ID="LiteralLastLocationID" Text='<%# Eval("LastLocationID") %>' runat="server"></asp:Literal>
                            </div>
                        </EditItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField>
                        <ItemTemplate>
                            <table>
                                <tr>
                                    <td>
                                        <asp:LinkButton ID="LinkButtonEditLast" runat="server" OnClick="LinkButtonEditLast_Click"
                                            TabIndex="5">Edit</asp:LinkButton>
                                    </td>
                                </tr>
                            </table>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <table>
                                <tr>
                                    <td>
                                        <asp:LinkButton ID="LinkButtonEditUpdateLast" runat="server" OnClick="LinkButtonEditUpdateLast_Click"
                                            TabIndex="6">Update</asp:LinkButton>
                                    </td>
                                    <td>
                                        <asp:LinkButton ID="LinkButtonCancelLast" runat="server" OnClick="LinkButtonCancelLast_Click"
                                            TabIndex="6">Cancel</asp:LinkButton>
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
        </td>--%>
        <td align="center" colspan="4">
            <asp:GridView ID="GridViewLocation" runat="server" Width="500px" CellPadding="3"
                EmptyDataText="No Record to display." GridLines="None" CssClass="GridViewStyle"
                AutoGenerateColumns="False" HorizontalAlign="Center" DataSourceID="ObjectDataSourceLocation"
                DataKeyNames="LocationID" OnPreRender="GridViewLocation_PreRender">
                <HeaderStyle CssClass="GridViewHeaderStyle" />
                <EmptyDataRowStyle CssClass="GridViewEmptyRowStyle" />
                <Columns>
                    <asp:TemplateField HeaderText="Location">
                        <ItemTemplate>
                            <%#Eval("Location1") %>
                            <div style="visibility: hidden; display: none;">
                                <asp:Label ID="LabelLocation" runat="server" Text='<%#Eval("Location1") %>'></asp:Label>
                            </div>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Rename Location">
                        <ItemTemplate>
                            <%#Eval("RenameLocation") %>
                            <div style="visibility: hidden; display: none;">
                                <asp:Label ID="LabelRenameLocation" runat="server" Text='<%#Eval("RenameLocation") %>'></asp:Label>
                            </div>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:DropDownList ID="DropDownListLocation" runat="server" AppendDataBoundItems="True"
                                DataSourceID="ObjectDataSourceLocation" DataTextField="Location1" DataValueField="LocationID"
                                ForeColor="#06569D" SelectedValue='<%# Bind("LocationID") %>' CssClass="aspDropDownList"
                                TabIndex="6">
                                <asp:ListItem Value="0">Select Location</asp:ListItem>
                            </asp:DropDownList>
                            <div style="display: none; visibility: hidden;">
                                <asp:Literal ID="LiteralLocationID" Text='<%# Eval("LocationID") %>' runat="server"></asp:Literal>
                                <asp:Literal ID="LiteralLastLocation" Text='<%# Eval("Location1") %>' runat="server"></asp:Literal>
                            </div>
                        </EditItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField>
                        <ItemTemplate>
                            <table>
                                <tr>
                                    <td>
                                        <asp:LinkButton ID="LinkButtonEdit" runat="server" OnClick="LinkButtonEdit_Click"
                                            TabIndex="5">Edit</asp:LinkButton>
                                    </td>
                                </tr>
                            </table>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <table>
                                <tr>
                                    <td>
                                        <asp:LinkButton ID="LinkButtonEditUpdate" runat="server" OnClick="LinkButtonEditUpdate_Click"
                                            TabIndex="6">Update</asp:LinkButton>
                                    </td>
                                    <td>
                                        <asp:LinkButton ID="LinkButtonCancel" runat="server" OnClick="LinkButtonCancel_Click"
                                            TabIndex="6">Cancel</asp:LinkButton>
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
        </td>
    </tr>
</table>
<asp:ObjectDataSource ID="ObjectDataSourceLocation" runat="server" SelectMethod="GetAllLocation"
    TypeName="RMC.BussinessService.BSLocation"></asp:ObjectDataSource>
<asp:ObjectDataSource ID="ObjectDataSourceLastLocation" runat="server" SelectMethod="GetAllLastLocation"
    TypeName="RMC.BussinessService.BSLocation"></asp:ObjectDataSource>
