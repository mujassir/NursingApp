<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ActivationOfUsers.ascx.cs"
    Inherits="RMC.Web.UserControls.ActivationOfUsers" %>
<script language="javascript" type="text/javascript">

    function RadioChecked(param) {debugger;

        var frm = document.forms[0];
        var varStatus = 0;
        for (i = 0; i < frm.length; i++) {
            if (frm.elements[i].type == "radio") {
                if (param != frm.elements[i].id) {

                    if (frm.elements[i].checked == 'True') {
                        varStatus = 1;
                        break;
                    }
                }
            }
        }
        if (varStatus == 0) {
            alert('Please select atleast one list from the table.');
            return false;
        }

    }

    function confirmMsg() {

        if (confirm('Are you sure you want perporm the operation for this user?')) {
            return true;
        }
        return false;
    }



</script>
<table>
    <tr>
        <td>
            <asp:GridView ID="GridViewActivationOfUsers" runat="server" Width="500px" AutoGenerateColumns="False"
                DataKeyNames="UserID" AllowSorting="True" CellPadding="3" HorizontalAlign="Center"
                GridLines="None" CssClass="GridViewStyle" DataSourceID="LinqDataSourceActivationOfUsers"
                EmptyDataText="No Record to display." EnableModelValidation="True">
                <HeaderStyle CssClass="GridViewHeaderStyle" />
                <EmptyDataRowStyle CssClass="GridViewEmptyRowStyle" />
                <Columns>
                    <asp:TemplateField HeaderText="Active" ItemStyle-HorizontalAlign="Center">
                        <ItemTemplate>
                            <asp:RadioButton ID="RadioButtonActive" runat="server" GroupName="SuppliersGroup" />
                            <div style="visibility: hidden; display: none;">
                                <asp:Label ID="LabelUserID" runat="server" Text='<%#Eval("UserID") %>'></asp:Label>
                            </div>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Not Active" ItemStyle-HorizontalAlign="Center">
                        <ItemTemplate>
                            <asp:RadioButton ID="RadioButtonNotActive" runat="server" GroupName="SuppliersGroup" />
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                    </asp:TemplateField>
                    <asp:BoundField DataField="FirstName" HeaderText="FirstName" ReadOnly="True" SortExpression="FirstName" />
                    <asp:BoundField DataField="LastName" HeaderText="LastName" ReadOnly="True" SortExpression="LastName" />
                    <asp:BoundField DataField="Email" HeaderText="Email" ReadOnly="True" SortExpression="Email" />
                    <asp:TemplateField HeaderText="View" HeaderStyle-HorizontalAlign="Center">
                        <ItemTemplate>
                            <asp:ImageButton ID="ImageButtonView" runat="server" ImageUrl="~/Images/View.gif"
                                ToolTip="View" OnClick="ImageButtonView_Click" />
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Delete" Visible="false">
                        <ItemTemplate>
                            <asp:ImageButton ID="ImageButtonDelete" OnClientClick="return confirm('Are you sure you wish to delete this entry?');"
                                ImageUrl="~/Images/delete.png" runat="server" OnClick="ImageButtonDelete_Click" />
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Save">
                        <ItemTemplate>
                            <asp:LinkButton ID="LinkButtonSave" ForeColor="Red" ToolTip="To Activate/Not Activate user's request."
                                runat="server" Text="Save" OnClick="LinkButtonSave_Click" OnClientClick="return RadioChecked(this);"></asp:LinkButton>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Access Request">
                        <ItemTemplate>
                            <asp:Label ID="LabelAccessRequest" runat="server" Text='<%#Eval("AccessRequest") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                     <%--Added for Customized Notification--%>
                        <asp:TemplateField HeaderText="Notification">
                            <ItemTemplate>
                                <a onclick='mypopupMessage(<%#Eval("UserID") %>);' style="cursor: pointer;" title="Send the notification to this user.">
                                    Notify User</a>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <%--Added for Customized Notification--%>
                </Columns>
                <FooterStyle CssClass="GridViewFooterStyle" />
                <PagerStyle CssClass="GridViewPagerStyle" />
                <SelectedRowStyle CssClass="GridViewSelectedRowStyle" />
                <AlternatingRowStyle CssClass="GridViewAlternatingRowStyle" />
                <RowStyle CssClass="GridViewRowStyle" />
            </asp:GridView>
            <asp:LinqDataSource ID="LinqDataSourceActivationOfUsers" runat="server" ContextTypeName="RMC.DataService.RMCDataContext"
                Select="new (UserID, CompanyName, FirstName, LastName, IsActive, Email, AccessRequest)"
                TableName="UserInfos" Where="IsDeleted == @IsDeleted &amp;&amp; UserActivationRequest == @UserActivationRequest &amp;&amp; IsActive == @IsActive">
                <WhereParameters>
                    <asp:Parameter DefaultValue="false" Name="IsDeleted" Type="Boolean" />
                    <asp:Parameter DefaultValue="Activation Request" Name="UserActivationRequest" Type="String" />
                    <asp:Parameter DefaultValue="false" Name="IsActive" Type="Boolean" />
                </WhereParameters>
            </asp:LinqDataSource>
        </td>
    </tr>
    <tr>
        <td align="left" style="padding-left: 15px;">
            <asp:Button ID="ButtonActivate" runat="server" Text="Accept Settings" CssClass="aspButton"
                OnClick="ButtonActivate_Click" OnClientClick="return RadioChecked(this);" />
        </td>
    </tr>
</table>
<%--Added for Customized Notification--%>
<%--Added by Raman 01/Jan/2011--%>
<script language="javascript" type="text/javascript">
    function mypopupMessage(UserID) {
        mywindow = window.open("../Administrator/SendMessage.aspx?FromUserID=" + UserID, "", "location=1,status=0,scrollbars=1, width=550,height=400,resizable=no ");
        return false;
    } 
</script>