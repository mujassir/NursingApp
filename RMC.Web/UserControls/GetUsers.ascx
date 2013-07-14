<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="GetUsers.ascx.cs" Inherits="RMC.Web.UserControls.GetUsers" %>
<script type="text/javascript">
    function clearlist() {
        var listbox = document.getElementById('<%=ListBoxShowUser.ClientID%>');        
        for (var i = 0; i < listbox.options.length; i++)
            listbox.options[i].selected = false;       
    }
</script>
<asp:Panel ID="PanelGetUsers" runat="server" DefaultButton="ButtonSearchUser">
    <table width="99%">
        <tr>
            <th align="left" style="font-size: 14px; color: #06569d; padding-left: 10px;
                padding-top: 10px;">
                <%--<h3 style="font-size: 13px;">--%>
                <u>Users List</u>
                <%--</h3>--%>
            </th>
        </tr>
        <tr>
            <td style="height: 10px;">
            </td>
        </tr>
        <tr>
            <td style="padding-left: 40px;">
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
            <td align="left" valign="middle" style="padding-left: 20px;">
                <asp:Label ID="LabelSearchUser" runat="server" Text="Search Users" ForeColor="#06569d"
                    Font-Size="11px" Font-Bold="true"></asp:Label>
                &nbsp
                <asp:TextBox ID="TextBoxSearchUser" runat="server" MaxLength="200" TabIndex="1"></asp:TextBox>&nbsp
                <asp:Button ID="ButtonSearchUser" runat="server" Text="Search" CssClass="aspButton"
                    Width="80px" TabIndex="2" OnClick="ButtonSearchUser_Click" OnClientClick="clearlist();" />
                <asp:Button ID="ButtonSearchAddANewUser" runat="server" CssClass="aspButton" OnClick="ButtonSearchAddANewUser_Click"
                    PostBackUrl="~/Administrator/UserRegistration.aspx" TabIndex="3" Text="Add New User"
                    Width="120px" />
            </td>
        </tr>
        <tr>
            <td align="center" style="padding-left: 20px;">
                &nbsp&nbsp
                <asp:Button ID="ButtonReset" runat="server" Text="Reset" CssClass="aspButton" Width="70px"
                    OnClick="ButtonReset_Click" Visible="false" />
                &nbsp&nbsp
            </td>
        </tr>
        <tr>
            <td align="left" style="padding-left: 20px;">
                <asp:ListBox ID="ListBoxShowUser" runat="server" ForeColor="#06569D" Width="450px"
                    DataSourceID="ObjectDataSourceShowUser" DataTextField="UserName" DataValueField="UserID"
                    AutoPostBack="True" OnSelectedIndexChanged="ListBoxShowUser_SelectedIndexChanged"
                    Rows="15" BackColor="White" TabIndex="4"></asp:ListBox>
                <asp:ObjectDataSource ID="ObjectDataSourceShowUser" runat="server" SelectMethod="GetUserInformationBySearch"
                    TypeName="RMC.BussinessService.BSUsers">
                    <SelectParameters>
                        <asp:ControlParameter ControlID="TextBoxSearchUser" DefaultValue="0" Name="search"
                            PropertyName="Text" Type="String" />
                    </SelectParameters>
                </asp:ObjectDataSource>
            </td>
        </tr>
        <tr>
            <td align="left">
            </td>
        </tr>
        <tr>
            <td align="left">
                &nbsp;
            </td>
        </tr>
    </table>
</asp:Panel>
