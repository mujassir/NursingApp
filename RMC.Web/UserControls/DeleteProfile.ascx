<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DeleteProfile.ascx.cs"
    Inherits="RMC.Web.UserControls.DeleteProfile" %>
<table width="99%">
    <tr>
        <th align="center">
            <h3 style="font-size: 13px;">
                <table width="100%">
                    <tr>
                        <td align="left" style="width: 20px;">
                            <asp:ImageButton ID="ImageButtonBack" ToolTip="Back" runat="server" ImageUrl="~/Images/back.gif" />
                        </td>
                        <td align="center">
                            <span style="font-weight: bold; color: #06569d;">Category Profile Detail</span>
                        </td>
                    </tr>
                </table>
            </h3>
        </th>
    </tr>
    <tr>
        <td align="right" colspan="2">
            <asp:LinkButton ID="LinkButtonDelete" runat="server" Text="Delete Profile" Font-Size="12px"
                Font-Underline="true" Font-Bold="true"></asp:LinkButton>
        </td>
    </tr>
    <tr>
        <td>
            <div style="overflow-y: scroll; height: 400px; border: 1px solid Black;">
                <asp:GridView ID="GridViewDeleteProfile" runat="server" Width="540px" CssClass="GridViewStyle"
                    CellPadding="5" EmptyDataText="No Record to display." DataKeyNames="CategoryProfileID"
                    GridLines="None" AutoGenerateColumns="False" HorizontalAlign="Left" AllowPaging="false"
                    AutoGenerateEditButton="True">
                    <HeaderStyle CssClass="GridViewHeaderStyle" />
                    <EmptyDataRowStyle CssClass="GridViewEmptyRowStyle" />
                    <Columns>
                        <asp:TemplateField HeaderText="Location">
                            <ItemTemplate>
                                <%#Eval("Location")%>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Activity">
                            <ItemTemplate>
                                <%#Eval("Activity")%>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="SubActivity">
                            <ItemTemplate>
                                <%#Eval("SubActivity")%>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Category">
                            <ItemTemplate>
                                <%#Eval("CategoryAssignmentName")%>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                    <FooterStyle CssClass="GridViewFooterStyle" />
                    <PagerStyle CssClass="GridViewPagerStyle" />
                    <SelectedRowStyle CssClass="GridViewSelectedRowStyle" />
                    <AlternatingRowStyle CssClass="GridViewAlternatingRowStyle" />
                    <RowStyle CssClass="GridViewRowStyle" />
                </asp:GridView>
            </div>
            <%-- <asp:ObjectDataSource ID="ObjectDataSourceRequestList" runat="server" OnSelecting="ObjectDataSourceRequestList_Selecting"
                SelectMethod="GetCategoryProfileByUserID" TypeName="RMC.BussinessService.BSCategoryProfiles"
                DataObjectTypeName="RMC.BusinessEntities.BECategoryProfile" UpdateMethod="UpdateCategoryProfile">
                <SelectParameters>
                    <asp:Parameter DefaultValue="0" Name="userID" Type="Int32" />
                    <asp:Parameter DefaultValue="0" Name="profileTypeID" Type="Int32" />
                </SelectParameters>
            </asp:ObjectDataSource>--%>
        </td>
    </tr>
</table>
