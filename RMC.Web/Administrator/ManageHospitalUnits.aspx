<%@ Page Title="RMC:Manage Hospital Units" Language="C#" MasterPageFile="~/Administrator/AdminMaster.Master" AutoEventWireup="true" CodeBehind="ManageHospitalUnits.aspx.cs" Inherits="RMC.Web.Administrator.ManageUnits" %>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
 <center>
        <table style="width: 100%;">
            <tr>
                <th>
                    <h3 style="font-size: 13px;">
                        Hospital Units
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
                                <asp:Button ID="ButtonApplyFilter" CssClass="aspButton" runat="server" 
                                    Text="Apply Filter" onclick="ButtonApplyFilter_Click"
                                    />
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
                                <a style="font-weight:bold;" href="HospitalDemographicDetail.aspx?CreateHospitalUnit=Y">Add Hospital Units</a>                                                                
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                <asp:GridView ID="GridHospitalUnit" runat="server" Width="630px" DataKeyNames="HospitalDemographicID"
                                    AutoGenerateColumns="False" BackColor="#F79220" BorderColor="#F79220" BorderStyle="None"
                                    BorderWidth="1px" AllowPaging="true" AllowSorting="true" PageSize="2" CellPadding="3"
                                    CellSpacing="2" HorizontalAlign="Center" DataSourceID="LinqDataSourceHospitalUnit"
                                    OnRowDataBound="GridHospitalUnit_RowDataBound" OnRowCommand="GridHospitalUnit_RowCommand">
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
                                        <asp:TemplateField HeaderText="Unit Name" HeaderStyle-Font-Bold="true" SortExpression="HospitalUnitName">
                                            <ItemTemplate>
                                                <asp:HyperLink ID="HyperLinkHospitalUnitName" runat="server" Text='<%# Eval("HospitalUnitName") %>'
                                                    ForeColor="#06569D" NavigateUrl='<%# Eval("HospitalDemographicID", "HospitalDemographicDetail.aspx?HospitalDemographicID={0}")%>'></asp:HyperLink>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Left" Font-Bold="true" VerticalAlign="Middle" />
                                        </asp:TemplateField> 
                                        <asp:TemplateField HeaderText="View Users" HeaderStyle-Font-Bold="true" SortExpression="HospitalUnitName">
                                            <ItemTemplate>
                                                <asp:HyperLink ID="HyperLinkHospitalViewUsers" runat="server" Text='View Users'
                                                    ForeColor="#06569D" NavigateUrl='<%# "ViewUsersByUnits.aspx?HospitalInfoId="+Eval("HospitalInfoId")+"&HospitalDemographicID="+Eval("HospitalDemographicID")%>'></asp:HyperLink>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Left" Font-Bold="true" VerticalAlign="Middle" />
                                        </asp:TemplateField>                                       
                                        <asp:TemplateField HeaderStyle-Font-Bold="true" HeaderText="Delete">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="LinkButtonDelete" runat="server" CommandName="logicallydelete"
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
                                                    Hospital Name
                                                </td>
                                                <td>
                                                    Unit Name
                                                </td>                                                
                                                <td>
                                                    Delete
                                                </td>
                                            </tr>
                                        </table>
                                    </EmptyDataTemplate>
                                </asp:GridView>
                            </td>
                        </tr>
                        <tr>
                            <td align="left" colspan="2">
                                <asp:LinqDataSource ID="LinqDataSourceHospitalUnit" runat="server" ContextTypeName="RMC.DataService.RMCDataContext"
                                    Select="new (HospitalInfo.HospitalName,HospitalDemographicID, HospitalInfoID, HospitalUnitName, HospitalInfo)"
                                    TableName="HospitalDemographicInfos" 
                                    Where="IsDeleted == @IsDeleted &amp;&amp; HospitalInfo.IsDeleted ==@IsDeleted &amp;&amp; (HospitalInfoID == @HospitalInfoID || @HospitalInfoID == 0)">
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
