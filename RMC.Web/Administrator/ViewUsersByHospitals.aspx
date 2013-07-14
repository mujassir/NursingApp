<%@ Page Title="RMC:User By Hospital" Language="C#" MasterPageFile="~/Administrator/AdminMaster.Master"
    AutoEventWireup="true" CodeBehind="ViewUsersByHospitals.aspx.cs" Inherits="RMC.Web.Administrator.ViewUsersByHospitals" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <center>
        <table style="width: 100%;">
            <tr>
                <th>
                    <h3 style="font-size: 13px;">
                        Hospital Users
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
                <td align="left">
                    <table>
                        <tr>
                            <td>
                                Hospital
                            </td>
                            <td>
                                <asp:DropDownList ID="DropDownListHospital" CssClass="dropdown" runat="server">
                                </asp:DropDownList>
                            </td>
                            <td>
                                <asp:Button ID="ButtonApplyFilter" CssClass="aspButton" runat="server" Text="Apply Filter"
                                    OnClick="ButtonApplyFilter_Click" />
                            </td>
                        </tr>
                       
                    </table>
                </td>
            </tr>
            <tr>
                <td height="30px">
                </td>
            </tr>
            <tr>
                <td align="left">
                    <table width="630px" align="left">
                    <tr>
                        <td colspan ="2" align ="right" >
                            <table>
                                 <tr>
                                    <td>
                                        Select user to add permission
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="DropDownListUsers" CssClass="dropdown" runat="server">
                                        </asp:DropDownList>
                                    </td>
                                    <td>
                                        <asp:Button ID="ButtonAddViewPermission" CssClass="aspButton" runat="server" Text="Add view Permission"
                                            OnClick="ButtonAddViewPermission_Click" Width="133px" />
                                    </td>
                                </tr>
                                    </table>
                        </td>
                    </tr>
                        <tr>
                            <td colspan="2">
                                <asp:GridView ID="GridHospitalUsers" DataKeyNames="MultiUserHospitalID" runat="server" Width="630px" AutoGenerateColumns="False"
                                    BackColor="#F79220" BorderColor="#F79220" BorderStyle="None" BorderWidth="1px"
                                    AllowPaging="True" AllowSorting="True" PageSize="2" CellPadding="3" CellSpacing="2"
                                    HorizontalAlign="Center" DataSourceID="LinqDataSourceHospitalUnit" OnRowDataBound="GridHospitalUnit_RowDataBound">
                                    <HeaderStyle BackColor="#A50D0C" Font-Bold="True" ForeColor="White" />
                                    <EmptyDataRowStyle BackColor="#A50D0C" Font-Bold="True" ForeColor="White" />
                                    <RowStyle BackColor="#FFF7E7" ForeColor="#06569D" />
                                    <Columns>
                                        <asp:TemplateField HeaderText="Hospital Name" HeaderStyle-Font-Bold="true" SortExpression="HospitalName">
                                            <ItemTemplate>
                                                <asp:HyperLink ID="HyperLinkHospitalName" runat="server" Text='<%# Eval("HospitalName") %>'
                                                    ForeColor="#06569D" NavigateUrl='<%# Eval("HospitalInfoId", "HospitalDetail.aspx?HospitalInfoId={0}")%>'></asp:HyperLink>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Left" Font-Bold="true" VerticalAlign="Middle" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="User Name" HeaderStyle-Font-Bold="true" SortExpression="HospitalName">
                                            <ItemTemplate>
                                                <asp:HyperLink ID="HyperLinkUserName" runat="server" NavigateUrl='<%# Eval("UserID", "UserProfile.aspx?UserID={0}")%>'
                                                    Text='<%# Eval("UserName").ToString() %>' ForeColor="#06569D"></asp:HyperLink>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Left" Font-Bold="true" VerticalAlign="Middle" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="User Name" HeaderStyle-Font-Bold="true" SortExpression="HospitalName">
                                            <ItemTemplate>
                                                <asp:HyperLink ID="HyperLinkEmail" runat="server" NavigateUrl='<%# Eval("Email", "UserProfile.aspx?UserId={0}")%>'
                                                    Text='<%# Eval("Email").ToString() %>' ForeColor="#06569D"></asp:HyperLink>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Left" Font-Bold="true" VerticalAlign="Middle" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderStyle-Font-Bold="true" SortExpression="Permission" HeaderText="Read Only">
                                            <ItemTemplate>
                                                <asp:CheckBox ID="CheckReadOnly" runat="server" Text="" />
                                            </ItemTemplate>
                                            <HeaderStyle Font-Bold="true" HorizontalAlign="Center" VerticalAlign="Middle" />
                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </asp:TemplateField>
                                    </Columns>
                                    <FooterStyle BackColor="#A50D0C" ForeColor="White" />
                                    <PagerStyle ForeColor="White" HorizontalAlign="Center" BackColor="#A50D0C" />
                                    <SelectedRowStyle BackColor="#738A9C" Font-Bold="True" ForeColor="White" />
                                    <EmptyDataTemplate>
                                        <table style="width: 630px;">
                                            <tr style="color: white">
                                                <td>
                                                    Hospital Name
                                                </td>
                                                <td>
                                                    Read Only
                                                </td>
                                            </tr>
                                        </table>
                                    </EmptyDataTemplate>
                                </asp:GridView>
                            </td>
                        </tr>
                        <tr>
                            <td align="left">
                                <asp:Button ID="ButtonSubmit" runat="server" Text="Submit Changes" OnClick="ButtonSubmit_Click"
                                    CssClass="aspButton" Width="130px" />
                            </td>
                            <td align="right">
                                <asp:Button ID="ButtonReset" Visible="false" runat="server" Text="Reset" OnClick="ButtonReset_Click"
                                    CssClass="aspButton" />
                            </td>
                        </tr>
                        <tr>
                            <td align="left" colspan="2">
                                <asp:LinqDataSource ID="LinqDataSourceHospitalUnit" runat="server" ContextTypeName="RMC.DataService.RMCDataContext"
                                    Select="new (HospitalInfoID, MultiUserHospitalID, UserID, PermissionID, UserInfo.Email, (UserInfo.FirstName + ' ' + UserInfo.LastName) as UserName, HospitalInfo.HospitalName)"
                                    TableName="MultiUserHospitals" Where="IsDeleted == @IsDeleted && HospitalInfoID == @HospitalInfoID && PermissionId = 1">
                                    <WhereParameters>
                                        <asp:Parameter DefaultValue="false" Name="IsDeleted" Type="Boolean" />
                                        <asp:ControlParameter ControlID="DropDownListHospital" Name="HospitalInfoID" PropertyName="SelectedValue"
                                            Type="Int32" />
                                    </WhereParameters>
                                </asp:LinqDataSource>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </center>
</asp:Content>
<%--
Select="new ((UserInfo.FirstName + ' ' + UserInfo.LastName) as UserName, HospitalInfo.HospitalName as HospitalName, UserInfo.Email as Email, MultiUserHospitalID, HospitalInfoID, UserID, PermissionID))" 
                                    TableName="MultiUserHospitals" Where="IsDeleted == @IsDeleted &amp;&amp; (HospitalInfoID == @HospitalInfoID || @HospitalInfoID == 0) ">
                                    <WhereParameters>
                                        <asp:Parameter DefaultValue="false" Name="IsDeleted" Type="Boolean" />
                                        <asp:Parameter DefaultValue="false" Name="IsDeleted" Type="Boolean" />
                                        <asp:ControlParameter ControlID="DropDownListHospital" Name="HospitalInfoID" PropertyName="SelectedValue"
                                            Type="Int32" />
                                    </WhereParameters>--%>
<%--Where="IsDeleted == @IsDeleted &amp;&amp; (HospitalInfoID == @HospitalInfoID || @HospitalInfoID == 0) &amp;&amp; UserId != HospitalInfo.UserId">--%>
<%--<WhereParameters>
                                        <asp:Parameter DefaultValue="false" Name="IsDeleted" Type="Boolean" />
                                        <asp:ControlParameter ControlID="DropDownListHospital" Name="HospitalInfoID" PropertyName="SelectedValue"
                                            Type="Int32" />
                                    </WhereParameters>--%>