<%@ Page Language="C#" MasterPageFile="~/Administrator/AdminMaster.Master" AutoEventWireup="true"
    CodeBehind="UserList.aspx.cs" Inherits="RMC.Web.Administrator.UserList" Title="RMC:Manage Users" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <center>
        <table style="width: 100%;">
            <tr>
                <th>
                    <h3 style="font-size: 13px;">
                        Users List
                    </h3>
                </th>
            </tr>
            <tr>
                <td style="height: 2px;">
                    <asp:Label ID="LabelErrorMsg" runat="server" Font-Size="12px" Visible="False"></asp:Label>
                    <asp:ValidationSummary ID="ValidationSummaryLogin" runat="server" DisplayMode="List"
                        Font-Size="12px" ValidationGroup="Login" />
                </td>
            </tr>
            <tr>
                <td>
                    <table width="630px">
                        <tr>
                            <td colspan="2" align="right">
                                <a style="font-weight:bold;" href="UserProfile.aspx?CreateUser=Y">Add User</a> 
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                <asp:GridView ID="GridUsers" runat="server" Width="630px" DataKeyNames="UserID" 
                                    AutoGenerateColumns="False" BackColor="#F79220" BorderColor="#F79220" 
                                    BorderStyle="None" BorderWidth="1px" AllowPaging="True" AllowSorting="True" 
                                    PageSize="2" CellPadding="3" CellSpacing="2" HorizontalAlign="Center" 
                                    DataSourceID="LinqDataSourceUserList" onrowcommand="GridUsers_RowCommand" 
                                    onrowdatabound="GridUsers_RowDataBound">
                                    <HeaderStyle BackColor="#A50D0C" Font-Bold="True" ForeColor="White" />
                                    <RowStyle BackColor="#FFF7E7" ForeColor="#06569D" />
                                    
                                    <Columns>
                                        <asp:TemplateField HeaderStyle-Font-Bold="true" SortExpression="IsActive" HeaderText="Active">
                                            <ItemTemplate>
                                                <asp:CheckBox ID="CheckIsActive" runat="server" Text="" Checked='<%#Eval("IsActive") %>' />
                                            </ItemTemplate>                                            
                                            <HeaderStyle CssClass="GridHeader" Font-Bold="true" HorizontalAlign="Center" VerticalAlign="Middle" />
                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Name" HeaderStyle-Font-Bold="true" SortExpression="FirstName, LastName">
                                            <ItemTemplate>
                                               <asp:HyperLink ID="HyperLinkName" runat="server" 
                                                    NavigateUrl='<%# Eval("UserId", "UserProfile.aspx?UserId={0}")%>' 
                                                    Text='<%# Eval("FirstName").ToString() +" " + Eval("LastName").ToString() %>' ForeColor="#06569D"></asp:HyperLink>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Left" Font-Bold="true" VerticalAlign="Middle" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Phone" SortExpression="Phone">
                                            <ItemTemplate>
                                                <asp:Label ID="LabelPhone" runat="server" Text='<%#Eval("Phone") %>'>
                                                </asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Left" Font-Bold="true" VerticalAlign="Middle" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Fax" SortExpression="Fax">
                                            <ItemTemplate>
                                                <asp:Label ID="LabelFax" runat="server" Text='<%#Eval("Fax") %>'>
                                                </asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Left" Font-Bold="true" VerticalAlign="Middle" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="E - mail" SortExpression="Email">
                                            <ItemTemplate>
                                                <asp:Label ID="LabelEmail" runat="server" Text='<%#Eval("Email") %>'>
                                                </asp:Label>
                                            </ItemTemplate>                                            
                                            <HeaderStyle HorizontalAlign="Left" Font-Bold="true" VerticalAlign="Middle" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderStyle-Font-Bold="true" HeaderText="Delete">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="LinkButtonDelete" runat="server" Font-Bold="true" CommandName="logicallydelete" Text="Delete"
                                                    Width="50px" />
                                            </ItemTemplate>                                            
                                            <HeaderStyle Font-Bold="true" HorizontalAlign="Center" VerticalAlign="Middle" />
                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </asp:TemplateField>
                                        <%--<asp:TemplateField>
                                            <ItemTemplate>
                                                <asp:Label ID="LabelHospital" runat="server" Text='<%#Eval("HospitalName") %>'>
                                                </asp:Label>
                                            </ItemTemplate>
                                            <HeaderTemplate>
                                                <asp:Label ID="LabelHospitalCaption" runat="server" Text="Hospital Name">
                                                </asp:Label>
                                            </HeaderTemplate>
                                            <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                        </asp:TemplateField>--%>
                                    </Columns>
                                    <FooterStyle BackColor="#A50D0C" ForeColor="White" />
                                    <PagerStyle ForeColor="White" HorizontalAlign="Center" BackColor="#A50D0C" />
                                    <SelectedRowStyle BackColor="#738A9C" Font-Bold="True" ForeColor="White" />
                                    
                                </asp:GridView>
                                <asp:LinqDataSource ID="LinqDataSourceUserList" 
                                    AutoGenerateOrderByClause="True" runat="server" ContextTypeName="RMC.DataService.RMCDataContext"
                                    TableName="UserInfos" 
                                    Where="IsDeleted == false && Email != @LoggedInUser" 
                                    onselecting="LinqDataSourceUserList_Selecting">  
                                    <WhereParameters>
                                        <asp:Parameter Name="LoggedInUser" DefaultValue="" />
                                    </WhereParameters>                                  
                                </asp:LinqDataSource>
                            </td>
                        </tr>
                        <tr>
                            <td align="left">
                                <asp:Button ID="ButtonSubmit" runat="server" Text="Submit Changes" OnClick="ButtonSubmit_Click"
                                    CssClass="aspButton" Width="130px" />
                            </td>
                            <td align="right">
                                <asp:Button ID="ButtonReset" runat="server" Visible="false" Text="Reset" OnClick="ButtonReset_Click"
                                    CssClass="aspButton" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </center>
</asp:Content>
