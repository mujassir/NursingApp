<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DataValidation.ascx.cs"
    Inherits="RMC.Web.UserControls.DataValidation" %>
<table>
    <tr>
        <th align="left">
            <h3 style="font-size: 13px;">
                <asp:ImageButton ID="ImageButtonBack" ToolTip="Back" runat="server" ImageUrl="~/Images/back.gif" />
                <span style="font-weight: bold; color: #06569d; padding-left:200px;">Non-Validated Data</span>
            </h3>
        </th>
    </tr>
    <tr>
        <td align="left">
            <asp:GridView ID="GridViewErrorValidation" runat="server" Width="545px" AutoGenerateColumns="False"
                AllowPaging="True" AllowSorting="True" CellPadding="3" GridLines="None" DataKeyNames="NurseID"
                DataSourceID="ObjectDataSourceNonValidatedData">
                <HeaderStyle BackColor="#A50D0C" Font-Bold="True" ForeColor="White" HorizontalAlign="Left" />                
                <Columns>
                    <asp:BoundField DataField="NurseID" HeaderText="NurseID" 
                        SortExpression="NurseID" />
                    <asp:BoundField DataField="CategoryGroupID" HeaderText="CategoryGroupID" 
                        SortExpression="CategoryGroupID" />
                    <asp:BoundField DataField="TypeID" HeaderText="TypeID" 
                        SortExpression="TypeID" />
                    <asp:BoundField DataField="ResourceRequirementID" 
                        HeaderText="ResourceRequirementID" SortExpression="ResourceRequirementID" />
                    <asp:BoundField DataField="LastLocationID" HeaderText="LastLocationID" 
                        SortExpression="LastLocationID" />
                    <asp:BoundField DataField="LastLocationDate" HeaderText="LastLocationDate" 
                        SortExpression="LastLocationDate" />
                    <asp:BoundField DataField="LastLocationTime" HeaderText="LastLocationTime" 
                        SortExpression="LastLocationTime" />
                    <asp:BoundField DataField="LocationID" HeaderText="LocationID" 
                        SortExpression="LocationID" />
                    <asp:BoundField DataField="LocationDate" HeaderText="LocationDate" 
                        SortExpression="LocationDate" />
                    <asp:BoundField DataField="LocationTime" HeaderText="LocationTime" 
                        SortExpression="LocationTime" />
                    <asp:BoundField DataField="ActivityID" HeaderText="ActivityID" 
                        SortExpression="ActivityID" />
                    <asp:BoundField DataField="ActivityDate" HeaderText="ActivityDate" 
                        SortExpression="ActivityDate" />
                    <asp:BoundField DataField="ActivityTime" HeaderText="ActivityTime" 
                        SortExpression="ActivityTime" />
                    <asp:BoundField DataField="SubActivityID" HeaderText="SubActivityID" 
                        SortExpression="SubActivityID" />
                    <asp:BoundField DataField="SubActivityDate" HeaderText="SubActivityDate" 
                        SortExpression="SubActivityDate" />
                    <asp:BoundField DataField="SubActivityTime" HeaderText="SubActivityTime" 
                        SortExpression="SubActivityTime" />
                    <asp:CheckBoxField DataField="IsErrorExist" HeaderText="IsErrorExist" 
                        SortExpression="IsErrorExist" />
                    <asp:BoundField DataField="CategoryGroupName" HeaderText="CategoryGroupName" 
                        SortExpression="CategoryGroupName" />
                    <asp:BoundField DataField="TypeName" HeaderText="TypeName" 
                        SortExpression="TypeName" />
                    <asp:BoundField DataField="ResourceRequirementName" 
                        HeaderText="ResourceRequirementName" SortExpression="ResourceRequirementName" />
                    <asp:BoundField DataField="LastLocationName" HeaderText="LastLocationName" 
                        SortExpression="LastLocationName" />
                    <asp:BoundField DataField="LocationName" HeaderText="LocationName" 
                        SortExpression="LocationName" />
                    <asp:BoundField DataField="ActivityName" HeaderText="ActivityName" 
                        SortExpression="ActivityName" />
                    <asp:BoundField DataField="SubActivityName" HeaderText="SubActivityName" 
                        SortExpression="SubActivityName" />
                </Columns>
                <FooterStyle BackColor="#A50D0C" ForeColor="White" />
                <PagerStyle ForeColor="White" HorizontalAlign="Center" BackColor="#A50D0C" />
                <SelectedRowStyle BackColor="#738A9C" Font-Bold="True" ForeColor="White" />
                <AlternatingRowStyle BackColor="#FFE2D9" ForeColor="#06569D" />
                <RowStyle BackColor="#FFF7E7" ForeColor="#06569D" />
            </asp:GridView>
            <asp:ObjectDataSource ID="ObjectDataSourceNonValidatedData" runat="server" 
                SelectMethod="GetValidData" TypeName="RMC.BussinessService.BSNursePDADetail">
                <SelectParameters>
                    <asp:QueryStringParameter DefaultValue="0" Name="NurseID" 
                        QueryStringField="NurseID" Type="Int32" />
                </SelectParameters>
            </asp:ObjectDataSource>
        </td>
    </tr>
</table>
