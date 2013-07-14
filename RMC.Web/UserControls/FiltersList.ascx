<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="FiltersList.ascx.cs"
    Inherits="RMC.Web.UserControls.FiltersList" %>
    
<asp:Panel ID="PanelFiltersList" runat="server">
<table width="99%">
        <tr>
            <th align="left" colspan="4" style="font-size: 14px; color: #06569d; padding-left: 10px;
                padding-top: 10px;">
                <%--<h3 style="font-size: 13px;">--%>
                <u>Filters List</u>
                <%--</h3>--%>
            </th>
        </tr>
        <tr>
            <td style="height: 10px;">
            </td>
        </tr>
        <tr>
            <td style="padding-left: 40px;" colspan="4">
                <div style="width: 420px; float: left; background-color: Transparent; z-index: 0;">
                    <center>
                        <div id="divErrorMsgInner" style="width: 418px; float: left; background-color: #E8E9EA;
                            z-index: 10;">
                            <div style="text-align: left; margin-left: 100px">
                                <asp:Label ID="LabelErrorMsg" runat="server" Font-Bold="true" Font-Size="11px">
                                </asp:Label>
                            </div>
                        </div>
                    </center>
                </div>
            </td>
        </tr>
        <tr>
            <td align="left" colspan="4" valign="middle" style="padding-left: 20px;">
                <asp:Label ID="LabelSearchFilter" runat="server" Text="Search Filter" ForeColor="#06569d"
                    Font-Size="11px" Font-Bold="true"></asp:Label>
                &nbsp
                <asp:TextBox ID="TextBoxSearchFilter" runat="server" MaxLength="200" TabIndex="1"></asp:TextBox>&nbsp
                <asp:Button ID="ButtonSearchFilter" runat="server" Text="Search" CssClass="aspButton"
                    Width="80px" TabIndex="2" onclick="ButtonSearchFilter_Click" />
                <asp:Button ID="ButtonSearchAddANewUser" runat="server" CssClass="aspButton"
                    TabIndex="3" Text="Create New Filter"
                    Width="120px" onclick="ButtonSearchAddANewUser_Click" />
            </td>
        </tr>
        <tr>
            <td align="center" colspan="4" style="padding-left: 20px;">
                
            </td>
        </tr>
        <tr>
            <td align="left" colspan="4" style="padding-left: 20px;">
                <asp:ListBox ID="ListBoxShowFilter" runat="server" ForeColor="#06569D" Width="450px"
                    
                    AutoPostBack="True"
                    Rows="15" BackColor="White" TabIndex="4" 
                    onselectedindexchanged="ListBoxShowFilter_SelectedIndexChanged" 
                    DataSourceID="ObjectDataSourceShowFilter" DataTextField="filterName" 
                    DataValueField="filterId"></asp:ListBox>
                <asp:ObjectDataSource ID="ObjectDataSourceShowFilter" runat="server" SelectMethod="GetFilterInformationBySearch"
                    TypeName="RMC.BussinessService.BSReports">
                    <SelectParameters>
                        <asp:ControlParameter ControlID="TextBoxSearchFilter" DefaultValue="0" Name="search"
                            PropertyName="Text" Type="String" />
                    </SelectParameters>
                </asp:ObjectDataSource>
            </td>
        </tr>
        <tr>
            <td align="left" colspan="4">
            </td>
        </tr>
        <tr>
            <td align="left" colspan="4">
                &nbsp;
            </td>
        </tr>
    </table>
</asp:Panel>    
