<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="RequestApprovalForHospital.ascx.cs"
    Inherits="RMC.Web.UserControls.RequestApprovalForHospital" %>

<script language="javascript" type="text/javascript">
        function checkCheckBox() {

            var allControls = document.getElementsByTagName('input');

            for (index = 0; index < allControls.length; index++) {

                if (allControls[index].type == 'checkbox') {

                    if (allControls[index].checked) {
                        
                        return true;
                    }
                }
            }

            alert('Select at Least one User in a list.');
            return false;
        }
</script>

<asp:Panel ID="PanelRequestApproval" runat="server" DefaultButton="ButtonSearch"
    Width="100%">
    <table width="100%" style="text-align: center;">
        <tr>
            <th align="left" style="font-size: 14px; color: #06569d; padding-left: 10px; padding-top: 10px;">
                <%--<h3 style="font-size: 13px;">--%>
                <u>Request Approval</u>
                <asp:Literal ID="LiteralHospitalName" runat="server"></asp:Literal>                
                <%--</h3>--%>
            </th>
            <th align="right" style="padding-left: 10px; padding-top: 10px;">
                <asp:ImageButton ID="ImageButtonBack" ToolTip="Back" runat="server" ImageUrl="~/Images/back.gif"
                    OnClick="ImageButtonBack_Click" TabIndex="5" />
            </th>
        </tr>
        <tr>
            <td style="height: 10px;">
            </td>
        </tr>
        <tr>
            <td align="center" valign="top" colspan="2">
                <asp:Label ID="LabelSearchUser" runat="server" Text="Search Users" ForeColor="#06569d"
                    Font-Size="11px" Font-Bold="true"></asp:Label>
                &nbsp
                <asp:TextBox ID="TextBoxSearchUser" runat="server" TabIndex="1"></asp:TextBox>
                &nbsp &nbsp
                <asp:Button ID="ButtonSearch" runat="server" CssClass="aspButton" Text="Search" OnClick="ButtonSearch_Click"
                    TabIndex="2" />
                <asp:Button ID="ButtonApproved" runat="server" CssClass="aspButton" OnClick="ButtonApproved_Click"
                    OnClientClick="return checkCheckBox();" Text="Approved" TabIndex="3" />
            </td>
        </tr>
        <tr>
            <td style="height: 10px;">
            </td>
        </tr>
          </table>
            
      <div style="height: 500px; overflow: auto; width:585px;border: 1px double #06569d;">    
       <table width="99%" style="text-align: left;">
       <tr>
           <td align="center" colspan="2">
               <asp:GridView ID="GridViewRequestApproval" runat="server" 
                   AutoGenerateColumns="False" CellPadding="3" CssClass="GridViewStyle" 
                   DataKeyNames="UserID" DataSourceID="ObjectDataSourceRequestApproval" 
                   EmptyDataText="No Record to display." GridLines="None" HorizontalAlign="Left" 
                   onrowdatabound="GridViewRequestApproval_RowDataBound" TabIndex="4">
                   <HeaderStyle CssClass="GridViewHeaderStyle" />
                   <EmptyDataRowStyle CssClass="GridViewEmptyRowStyle" />
                   <Columns>
                       <asp:TemplateField HeaderText="Selection">
                           <ItemTemplate>
                               <asp:CheckBox ID="CheckBoxSelection" runat="server" TabIndex="4" />
                           </ItemTemplate>
                       </asp:TemplateField>
                       <asp:BoundField DataField="UserName" HeaderText="UserName" 
                           SortExpression="UserName">
                       <HeaderStyle HorizontalAlign="Left" />
                       <ItemStyle HorizontalAlign="Left" />
                       </asp:BoundField>
                       <asp:BoundField ControlStyle-Width="50px" DataField="Email" HeaderText="Email" 
                           SortExpression="Email">
                       <HeaderStyle HorizontalAlign="Left" />
                       <ItemStyle HorizontalAlign="Left" />
                       </asp:BoundField>
                       <asp:TemplateField HeaderText="Permission">
                           <ItemTemplate>
                               <asp:DropDownList ID="DropDownListPermission" runat="server" 
                                   DataSourceID="LinqDataSourcePermission" DataTextField="Permission1" 
                                   DataValueField="PermissionID" TabIndex="4" Width="65px">
                               </asp:DropDownList>
                               <asp:LinqDataSource ID="LinqDataSourcePermission" runat="server" 
                                   ContextTypeName="RMC.DataService.RMCDataContext" 
                                   Select="new (PermissionID, Permission1)" TableName="Permissions">
                                   <WhereParameters>
                                       <asp:Parameter DefaultValue="1" Name="PermissionID" Type="Int32" />
                                   </WhereParameters>
                               </asp:LinqDataSource>
                           </ItemTemplate>
                           <HeaderStyle HorizontalAlign="Center" />
                           <ItemStyle HorizontalAlign="Center" />
                       </asp:TemplateField>
                       <%--  <asp:TemplateField>
                           <ItemTemplate>
                           
                           </ItemTemplate>   
                        </asp:TemplateField>--%>
                   </Columns>
                   <FooterStyle CssClass="GridViewFooterStyle" />
                   <PagerStyle CssClass="GridViewPagerStyle" />
                   <SelectedRowStyle CssClass="GridViewSelectedRowStyle" />
                   <AlternatingRowStyle CssClass="GridViewAlternatingRowStyle" />
                   <RowStyle CssClass="GridViewRowStyle" />
               </asp:GridView>
           </td>
           </tr>
        <tr>
            <td align="left" style="padding-left: 15px;" colspan="2">
                &nbsp;
            </td>
        </tr>
       </table>
      </div>
  
</asp:Panel>
<asp:ObjectDataSource ID="ObjectDataSourceRequestApproval" runat="server" SelectMethod="GetAllUsersForHospApprovalExceptSuperAdmin"
    TypeName="RMC.BussinessService.BSUsers">
    <SelectParameters>
        <asp:ControlParameter ControlID="TextBoxSearchUser" DefaultValue="0" Name="search"
            PropertyName="Text" Type="String" />
        <asp:QueryStringParameter DefaultValue="0" Name="hospitalInfoID" QueryStringField="HospitalID"
            Type="Int32" />
    </SelectParameters>
</asp:ObjectDataSource>
