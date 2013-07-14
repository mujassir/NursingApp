<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ContactUsNotification.ascx.cs"
    Inherits="RMC.Web.UserControls.ContactUsNotification" %>
<table width="100%">
    <tr>
        <td>
            <asp:GridView ID="GridViewContactUsNotification" runat="server" Width="500px" AutoGenerateColumns="False"
                AllowSorting="True" CellPadding="3" GridLines="None" EmptyDataText="No Record to display."
                CssClass="GridViewStyle" DataSourceID="ObjectDataSourceContactUsNotification">
                <HeaderStyle CssClass="GridViewHeaderStyle" />
                <EmptyDataRowStyle CssClass="GridViewEmptyRowStyle" />
                <Columns>
                    <asp:BoundField DataField="Email" HeaderText="EmailID" Visible="false" ReadOnly="True" />
                    <asp:BoundField DataField="Message" HeaderText="Message" ReadOnly="True" />
                    <%--<asp:BoundField DataField="UserName" HeaderText="Name" ReadOnly="True" OnClientClick='return mypopup("<%#Eval("SenderID") %>");'>
                        <ItemStyle Wrap="False" />
                    </asp:BoundField>--%>
                    <asp:TemplateField HeaderText="Name">
                        <ItemTemplate>
                            <a style="cursor: pointer;">
                                <%#Eval("UserName") %></a>
                            <%--<asp:LinkButton ID="LinkButtonUserName" OnClientClick='return mypopup();' runat="server"
                                Text='<%#Eval("UserName") %>'></asp:LinkButton>--%>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Delete">
                        <ItemTemplate>
                            <asp:ImageButton ID="ImageButtonDelete" ImageUrl="~/Images/delete.png" runat="server"
                                OnClick="ImageButtonDelete_Click" ToolTip="Delete record." OnClientClick='return confirm("Are you sure you wish to delete this entry?");' />
                            <div style="display: none; visibility: hidden;">
                                <asp:Label ID="LabelContactUsID" runat="server" Text='<%#Eval("ContactUsID") %>'></asp:Label>
                                <asp:Label ID="LabelMessageType" runat="server" Text='<%#Eval("MessageType") %>'></asp:Label>
                            </div>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Center" />
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:TemplateField>
                    <asp:BoundField DataField="MessageDate" HeaderText="DateTime" ReadOnly="True" />
                   
                    <asp:TemplateField HeaderText="Reply">
                        <ItemTemplate>
                            <a onclick='mypopup(<%#Eval("SenderID") %>,<%#Eval("ContactUsID") %>,"<%#Eval("MessageType") %>","<%#Eval("Email") %>");'
                                style="cursor: pointer;" title="Send the notification to this user.">Reply To</a>
                        </ItemTemplate>
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
<%--<asp:LinqDataSource ID="LinqDataSourceContactUsNotification" runat="server" 
    ContextTypeName="RMC.DataService.RMCDataContext" OrderBy="Message" 
    Select="new (ContactUsID, Message, SenderID, UserInfo.FirstName + ' ' + UserInfo.LastName as Name)" TableName="ContactUs">
</asp:LinqDataSource>--%>
<asp:ObjectDataSource ID="ObjectDataSourceContactUsNotification" runat="server" SelectMethod="GetContactUs"
    TypeName="RMC.BussinessService.BSContactUs"></asp:ObjectDataSource>

<script language="javascript" type="text/javascript">
    function mypopup(senderID, ContactUsID, MessageType, Email) {

        mywindow = window.open("../Administrator/SendUserNotification.aspx?SenderID=" + senderID + "&ID=" + ContactUsID + "&Type=" + MessageType + "&Email=" + Email,"", "location=1,status=0,scrollbars=1, width=500,height=300,resizable=no ");
        //mywindow = window.open("~/Administrator/SendUserNotification.aspx", "", "location=0,status=0,scrollbars=1, width=500,height=300,resizable=no ");

        return false;
    } 
</script>

