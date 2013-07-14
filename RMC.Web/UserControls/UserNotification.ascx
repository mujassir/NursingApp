    <%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UserNotification.ascx.cs"
    Inherits="RMC.Web.UserControls.UserNotification" %>
<table width="100%">
    <tr>
        <td>
            <asp:GridView ID="GridViewNotification" runat="server" Width="500px" DataKeyNames="NotificationID"
                CssClass="GridViewStyle" CellPadding="3" EmptyDataText="No Record to display."
                GridLines="None" AutoGenerateColumns="False" HorizontalAlign="Left" DataSourceID="ObjectDataSourceNotification">
                <HeaderStyle CssClass="GridViewHeaderStyle" />
                <EmptyDataRowStyle CssClass="GridViewEmptyRowStyle" />
                <Columns>
                    <asp:BoundField DataField="Subject" HeaderText="Subject" SortExpression="Subject" />
                    <asp:BoundField DataField="Message" HeaderText="Message" SortExpression="Message" />
                    <asp:TemplateField HeaderText="Name">
                        <ItemTemplate>
                            <a style="cursor: pointer;" onclick='mypopup(<%#Eval("SenderID") %>,<%#Eval("NotificationID") %>);' title="Send the notification to this user.">
                                <%#DataBinder.Eval(Container.DataItem,"UserName") %></a>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Delete">
                        <ItemTemplate>
                            <asp:ImageButton ID="ImageButtonDelete" ImageUrl="~/Images/delete.png" runat="server"
                                OnClick="ImageButtonDelete_Click" ToolTip="Delete record." OnClientClick='return confirm("Are you sure you wish to delete this entry?");' />
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Center" Wrap="False" />
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:TemplateField>
                </Columns>
                <FooterStyle CssClass="GridViewFooterStyle" />
                <PagerStyle CssClass="GridViewPagerStyle" />
                <SelectedRowStyle CssClass="GridViewSelectedRowStyle" />
                <AlternatingRowStyle CssClass="GridViewAlternatingRowStyle" />
                <RowStyle CssClass="GridViewRowStyle" />
            </asp:GridView>
        </td>
    </tr>
</table>
<asp:ObjectDataSource ID="ObjectDataSourceNotification" runat="server" OnSelecting="ObjectDataSourceNotification_Selecting"
    SelectMethod="GetNotificationByUserID" TypeName="RMC.BussinessService.BSNewsLetter">
    <SelectParameters>
        <asp:Parameter DefaultValue="0" Name="UserID" Type="Int32" />
    </SelectParameters>
</asp:ObjectDataSource>

<script language="javascript" type="text/javascript">
    function mypopup(senderID, NotificationID) {

        mywindow = window.open("../Users/SendUserNotification.aspx?SenderID=" + senderID + "&NotificationID=" + NotificationID, "", "location=0,status=0,scrollbars=1, width=500,height=300,resizable=no ");
       // mywindow = window.open("../Users/SendMessage.aspx?SenderID=" + senderID, "", "location=0,status=0,scrollbars=1, width=500,height=300,resizable=no ");

        return false;
    } 
</script>

