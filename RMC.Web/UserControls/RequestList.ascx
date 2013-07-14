<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="RequestList.ascx.cs"
    Inherits="RMC.Web.UserControls.RequestList" %>
<table width="100%">
    <tr>
        <td>
            <asp:GridView ID="GridViewRequestList" runat="server" Width="500px" DataKeyNames="RequestID"
                CssClass="GridViewStyle" CellPadding="5" EmptyDataText="No Record to display."
                GridLines="None" AutoGenerateColumns="False" HorizontalAlign="Left" DataSourceID="ObjectDataSourceRequestList">
                <HeaderStyle CssClass="GridViewHeaderStyle" />
                <EmptyDataRowStyle CssClass="GridViewEmptyRowStyle" />
                <Columns>
                    <asp:BoundField DataField="UserName" HeaderText="User Name" />                    
                    <asp:BoundField DataField="Value" HeaderText="Value" />
                    <asp:BoundField DataField="MessageDescription" HeaderText="Description" />                    
                    <asp:TemplateField HeaderText="View">
                        <ItemTemplate>
                            <asp:ImageButton ID="ImageButtonView" ImageUrl="~/Images/View.gif" runat="server"
                                OnClick="ImageButtonView_Click" Style="width: 15px" />
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Center" />
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Delete">
                        <ItemTemplate>
                            <asp:ImageButton ID="ImageButtonDelete" ImageUrl="~/Images/delete.png" runat="server"
                                OnClientClick="return confirm('Are you sure you wish to delete this entry?');"
                                OnClick="ImageButtonDelete_Click" />
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Center" />
                        <ItemStyle HorizontalAlign="Center" />
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
<asp:ObjectDataSource ID="ObjectDataSourceRequestList" runat="server" SelectMethod="GetAllRequest"
    TypeName="RMC.BussinessService.BSRequestForTypes"></asp:ObjectDataSource>
