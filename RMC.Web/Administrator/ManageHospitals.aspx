<%@ Page Title="RMC:Manage Hospital" Language="C#" MasterPageFile="~/Administrator/AdminMaster.Master"
    AutoEventWireup="true" CodeBehind="ManageHospitals.aspx.cs" Inherits="RMC.Web.Administrator.ManageHospitals" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <center>
        <table style="width: 100%;">
            <tr>
                <th>
                    <h3 style="font-size: 13px;">
                        Hospitals
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
                    <table align="left">
                        <tr>
                            <td>
                                Hospital
                            </td>
                            <td>
                                <asp:DropDownList ID="DropDownListHospital" CssClass="dropdown" runat="server">
                                </asp:DropDownList>
                            </td>
                            <td>
                                Admin
                            </td>
                            <td>
                                <asp:DropDownList ID="DropDownListAdmin" CssClass="dropdown" runat="server">
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
                <td align="left">
                    <table width="630px" align="left">
                        <tr>
                            <td colspan="2" align="right">
                                <a style="font-weight:bold;" href="HospitalDetail.aspx?CreateHospital=Y">Add Hospital</a>                                
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                <asp:GridView ID="GridHospital" runat="server" Width="630px" DataKeyNames="HospitalInfoID"
                                    AutoGenerateColumns="False" BackColor="#F79220" BorderColor="#F79220" BorderStyle="None"
                                    BorderWidth="1px" AllowPaging="true" AllowSorting="true" PageSize="2" CellPadding="3"
                                    CellSpacing="2" HorizontalAlign="Center" DataSourceID="LinqDataSourceHospital"
                                    OnRowDataBound="GridHospital_RowDataBound" OnRowCommand="GridHospital_RowCommand">
                                    <HeaderStyle BackColor="#A50D0C" Font-Bold="True" ForeColor="White" />
                                    <EmptyDataRowStyle BackColor="#A50D0C" Font-Bold="True" ForeColor="White" />
                                    <RowStyle BackColor="#FFF7E7" ForeColor="#06569D" />
                                    <Columns>
                                        <asp:TemplateField HeaderStyle-Font-Bold="true" SortExpression="IsActive" HeaderText="Active">
                                            <ItemTemplate>
                                                <asp:CheckBox ID="CheckIsActive" runat="server" Text="" Checked='<%#Eval("IsActive") %>' />
                                            </ItemTemplate>
                                            <HeaderStyle Font-Bold="true" HorizontalAlign="Center" VerticalAlign="Middle" />
                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="HospitalName" HeaderStyle-Font-Bold="true" SortExpression="HospitalName">
                                            <ItemTemplate>
                                                <asp:HyperLink ID="HyperLinkName" runat="server" Text='<%# Eval("HospitalName") %>'
                                                    ForeColor="#06569D" NavigateUrl='<%# Eval("HospitalInfoId", "HospitalDetail.aspx?HospitalInfoId={0}")%>'></asp:HyperLink>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Left" Font-Bold="true" VerticalAlign="Middle" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Chief Nursing Officer" SortExpression="ChiefNursingOfficer">
                                            <ItemTemplate>
                                                <asp:Label ID="LabelChiefNursingOfficer" runat="server" Text='<%#Eval("ChiefNursingOfficer") %>'>
                                                </asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Left" Font-Bold="true" VerticalAlign="Middle" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Admin" SortExpression="Admin">
                                            <ItemTemplate>
                                                <asp:Label ID="LabelAdmin" runat="server" Text='<%#Eval("Admin") %>'>
                                                </asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Left" Font-Bold="true" VerticalAlign="Middle" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="E-mail" SortExpression="Email">
                                            <ItemTemplate>
                                                <asp:Label ID="LabelEmail" runat="server" Text='<%#Eval("Email") %>'>
                                                </asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Left" Font-Bold="true" VerticalAlign="Middle" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderStyle-Font-Bold="true" HeaderText="View Users">
                                            <ItemTemplate>                                                
                                                <asp:HyperLink ID="HyperLinkViewUsers" runat="server" Text="View Users"
                                                    Font-Bold="true" NavigateUrl='<%# Eval("HospitalInfoId", "ViewUsersByHospitals.aspx?HospitalInfoId={0}")%>'></asp:HyperLink>
                                            </ItemTemplate>
                                            <HeaderStyle Font-Bold="true" HorizontalAlign="Center" VerticalAlign="Middle" />
                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderStyle-Font-Bold="true" HeaderText="View Units">
                                            <ItemTemplate>
                                                <asp:HyperLink ID="HyperLinkViewUnits" runat="server" Text="View Units"
                                                    Font-Bold="true" NavigateUrl='<%# Eval("HospitalInfoId", "ManageHospitalUnits.aspx?HospitalInfoId={0}")%>'></asp:HyperLink>                                                
                                            </ItemTemplate>
                                            <HeaderStyle Font-Bold="true" HorizontalAlign="Center" VerticalAlign="Middle" />
                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderStyle-Font-Bold="true" HeaderText="Delete">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="LinkButtonDelete" Font-Bold="true" runat="server" CommandName="logicallydelete"
                                                    Text="Delete" Width="50px" />
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
                                                    Active
                                                </td>
                                                <td>
                                                    Hospital
                                                </td>
                                                <td>
                                                    Chief Nursing Officer
                                                </td>
                                                <td>
                                                    Admin
                                                </td>
                                                <td>
                                                    E-mail
                                                </td>
                                                <td>
                                                    View Users
                                                </td>
                                                <td>
                                                    View Units
                                                </td>
                                            </tr>
                                        </table>
                                    </EmptyDataTemplate>
                                </asp:GridView>
                            </td>
                        </tr>
                        <tr>
                            <td align="left">
                                <asp:Button ID="ButtonSubmit" runat="server" Text="Submit Changes" CssClass="aspButton"
                                    Width="130px" OnClick="ButtonSubmit_Click" />
                                <asp:LinqDataSource ID="LinqDataSourceHospital" runat="server" ContextTypeName="RMC.DataService.RMCDataContext"
                                    Select="new (HospitalInfoID, UserID, HospitalName, (ChiefNursingOfficerFirstName + ' ' + ChiefNursingOfficerLastName) as ChiefNursingOfficer, HospitalDemographicInfos, IsActive, (UserInfo.Firstname + ' ' + UserInfo.Lastname) as Admin, UserInfo.Email)"
                                    TableName="HospitalInfos" Where="IsDeleted == @IsDeleted &amp;&amp; (HospitalInfoID == @HospitalInfoID || @HospitalInfoID == 0) &amp;&amp; (UserID == @UserID || @UserID == 0)"
                                    EnableDelete="True">
                                    <WhereParameters>
                                        <asp:Parameter DefaultValue="false" Name="IsDeleted" Type="Boolean" />
                                        <asp:ControlParameter ControlID="DropDownListHospital" Name="HospitalInfoID" PropertyName="SelectedValue"
                                            Type="Int32" />
                                        <asp:ControlParameter ControlID="DropDownListAdmin" Name="UserID" PropertyName="SelectedValue"
                                            Type="Int32" />
                                    </WhereParameters>
                                </asp:LinqDataSource>
                            </td>
                            <td align="right">
                               <asp:Button ID="ButtonReset" runat="server" Visible="false" Text="Reset" CssClass="aspButton" OnClick="ButtonReset_Click" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </center>
</asp:Content>
