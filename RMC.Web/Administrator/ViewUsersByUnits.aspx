<%@ Page Title="RMC:View User by Units" Language="C#" MasterPageFile="~/Administrator/AdminMaster.Master" AutoEventWireup="true" CodeBehind="ViewUsersByUnits.aspx.cs" Inherits="RMC.Web.Administrator.ViewUsersByUnits" %>
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
                        Unit Users
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
                                <asp:DropDownList ID="DropDownListHospital" CssClass="dropdown" runat="server" AutoPostBack="true"
                                    onselectedindexchanged="DropDownListHospital_SelectedIndexChanged">
                                     <asp:ListItem Selected="True" Value="0">Select Hospital</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                            <td>
                               
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Units
                            </td>
                            <td>
                                 <%--<asp:UpdatePanel ID="UpdatePanelHospitalUnits" runat="server">
                                    <ContentTemplate>--%>
                                        <asp:DropDownList ID="DropDownListHospitalUnits" CssClass="dropdown" 
                                            runat="server" AutoPostBack ="true" 
                                        DataTextField="HospitalUnitName" DataValueField="HospitalDemographicID" DataSourceID ="ObjectDataSourceHospitalUnits"
                                        ForeColor="#06569d" Height="16px" 
                                            onselectedindexchanged="DropDownListHospitalUnits_SelectedIndexChanged">
                                        </asp:DropDownList>
                                    <%--</ContentTemplate> 
                                     <Triggers>
                                         <asp:AsyncPostBackTrigger ControlID="DropDownListHospital" EventName="SelectedIndexChanged" />
                                      </Triggers>
                                 </asp:UpdatePanel> --%>
                                <asp:ObjectDataSource ID="ObjectDataSourceHospitalUnits" runat="server" SelectMethod="GetAllHospitalUnitsByHospitalInfoID"
                                TypeName="RMC.BussinessService.BSHospitalInfo" 
                                      onselected="ObjectDataSourceHospitalUnits_Selected" 
                                    >
                                <SelectParameters>
                                    <asp:ControlParameter ControlID="DropDownListHospital" DefaultValue="0" Name="HospitalInfoId"
                                        PropertyName="SelectedValue" Type="Int32" />
                                </SelectParameters>
                            </asp:ObjectDataSource>
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
                            <td align ="right" colspan="2">
                                <table>
                                    <tr>
                                        <td>
                                            Select user to add Permission
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
                                <asp:GridView ID="GridHospitalUnitUsers" 
                                    runat="server" Width="630px" AutoGenerateColumns="False"
                                    BackColor="#F79220" BorderColor="#F79220" BorderStyle="None" BorderWidth="1px"
                                    AllowPaging="True" AllowSorting="True" PageSize="2" CellPadding="3" CellSpacing="2"
                                    HorizontalAlign="Center" DataSourceID ="LinqDataSourceHospitalUnit"
                                    onrowdatabound="GridHospitalUnitUsers_RowDataBound" >
                                    <HeaderStyle BackColor="#A50D0C" Font-Bold="True" ForeColor="White" />
                                    <EmptyDataRowStyle BackColor="#A50D0C" Font-Bold="True" ForeColor="White" />
                                    <RowStyle BackColor="#FFF7E7" ForeColor="#06569D" />
                                    <Columns>
                                        <asp:TemplateField HeaderText="Hospital Unit Name" HeaderStyle-Font-Bold="true" SortExpression="HospitalUnitName">
                                            <ItemTemplate>
                                                <asp:HyperLink ID="HyperLinkHospitalName" runat="server" Text='<%# Eval("HospitalUnitName") %>'
                                                    ForeColor="#06569D" NavigateUrl='<%# Eval("HospitalDemographicID", "HospitalDemographicDetail.aspx?HospitalDemographicID={0}")%>'></asp:HyperLink>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Left" Font-Bold="true" VerticalAlign="Middle" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="User Name" HeaderStyle-Font-Bold="true" SortExpression="UserName">
                                            <ItemTemplate>
                                                <asp:HyperLink ID="HyperLinkUserName" runat="server" NavigateUrl='<%# Eval("UserID", "UserProfile.aspx?UserID={0}")%>'
                                                    Text='<%# Eval("UserName").ToString() %>' ForeColor="#06569D"></asp:HyperLink>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Left" Font-Bold="true" VerticalAlign="Middle" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="User Email" HeaderStyle-Font-Bold="true" SortExpression="Email">
                                            <ItemTemplate>
                                                <span style ="color:#06569D"><%# Eval("Email").ToString() %></span>
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
                                                    Unit Name
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
                                    CssClass="aspButton"  />
                            </td>
                        </tr>
                        <tr>
                            <td align="left" colspan="2">
                                <asp:LinqDataSource ID="LinqDataSourceHospitalUnit" runat="server" ContextTypeName="RMC.DataService.RMCDataContext"
                                    Select="new (HospitalDemographicID,MultiUserDemographicID, UserID, PermissionID, UserInfo.Email, (UserInfo.FirstName + ' ' + UserInfo.LastName) as UserName, HospitalDemographicInfo.HospitalUnitName)"
                                    TableName="MultiUserDemographics" Where="IsDeleted == @IsDeleted && HospitalDemographicID == @HospitalDemographicID && PermissionId = 1">
                                    <WhereParameters>
                                        <asp:Parameter DefaultValue="false" Name="IsDeleted" Type="Boolean" />
                                        <asp:ControlParameter ControlID="DropDownListHospitalUnits" Name="HospitalDemographicID" PropertyName="SelectedValue" DefaultValue="0"
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
