<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="RequestApprovalNotification.ascx.cs"
    Inherits="RMC.Web.UserControls.RequestApprovalNotification" %>
<asp:Panel ID="PanelRequestApprovalNotification" runat="server" DefaultButton="ButtonApproved">
    <script language="javascript" type="text/javascript">

        function RadioChecked(param) {

            var frmLength = document.forms.length;
            var varStatus = 0;
            var frm;
            if (frmLength > 1) {
                frm = document.forms[1];
            }
            else {
                frm = document.forms[0];
            }

            for (i = 0; i < frm.length; i++) {
                if (frm.elements[i].type == "radio") {
                    if (param != frm.elements[i].id) {
                        if (frm.elements[i].checked == true) {
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
    </script>
    <table width="100%">
        <tr>
            <td align="left">
                <asp:GridView ID="GridViewRequestApproval" runat="server" Width="500px" DataKeyNames="RequestID"
                    CssClass="GridViewStyle" CellPadding="3" EmptyDataText="No Record to display."
                    GridLines="None" AutoGenerateColumns="False" HorizontalAlign="Left" DataSourceID="ObjectDataSourceRequestApproval"
                    OnRowDataBound="GridViewRequestApproval_RowDataBound">
                    <HeaderStyle CssClass="GridViewHeaderStyle" />
                    <EmptyDataRowStyle CssClass="GridViewEmptyRowStyle" />
                    <Columns>
                        <asp:TemplateField HeaderText="Approved" ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <asp:CheckBox ID="CheckBoxApproval" Visible="false" Checked='<%#Eval("IsApproved") %>'
                                    runat="server" />
                                <asp:RadioButton ID="RadioButtonApproved" runat="server" GroupName="SuppliersGroup" />
                                <div style="visibility: hidden; display: none;">
                                    <asp:Label ID="LabelFromUserID" runat="server" Text='<%#Eval("FromUserID") %>'></asp:Label>
                                    <asp:Label ID="LabelToUserID" runat="server" Text='<%#Eval("ToUserID") %>'></asp:Label>
                                    <asp:Label ID="LabelHospitalID" runat="server" Text='<%#Eval("HospitalInfoID") %>'></asp:Label>
                                    <asp:Label ID="LabelDemograhicID" runat="server" Text='<%#Eval("DemographicDetailID") %>'></asp:Label>
                                </div>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center"></ItemStyle>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Not Approved" ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <asp:CheckBox ID="CheckBoxApproval2" Visible="false" Checked='<%#Eval("IsApproved") %>'
                                    runat="server" />
                                <asp:RadioButton ID="RadioButtonNotApproved" runat="server" GroupName="SuppliersGroup" />
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center"></ItemStyle>
                        </asp:TemplateField>
                        <asp:BoundField DataField="FromUserName" HeaderText="Sender Name" />
                        <asp:BoundField DataField="ToUserName" HeaderText="User Name" />
                        <asp:BoundField DataField="HospitalName" HeaderText="Hospital Name" />
                        <asp:BoundField DataField="UnitName" HeaderText="Unit Name" />
                        <asp:TemplateField HeaderText="Permission">
                            <ItemTemplate>
                                <asp:DropDownList ID="DropDownListPermission" runat="server" DataSourceID="LinqDataSourcePermission"
                                    DataTextField="Permission1" DataValueField="PermissionID">
                                </asp:DropDownList>
                                <asp:LinqDataSource ID="LinqDataSourcePermission" runat="server" ContextTypeName="RMC.DataService.RMCDataContext"
                                    Select="new (PermissionID, Permission1)" TableName="Permissions" Where="PermissionID != @PermissionID">
                                    <WhereParameters>
                                        <asp:Parameter DefaultValue="4" Name="PermissionID" Type="Int32" />
                                    </WhereParameters>
                                </asp:LinqDataSource>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Delete" Visible="false">
                            <ItemTemplate>
                                <asp:ImageButton ID="ImageButtonDelete" OnClientClick="return confirm('Are you sure you wish to delete this entry?');"
                                    ImageUrl="~/Images/delete.png" runat="server" OnClick="ImageButtonDelete_Click" />
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:TemplateField>
                        <%--Added for Customized Notification--%>
                        <asp:TemplateField HeaderText="Notification">
                            <ItemTemplate>
                                <a onclick='mypopupMessage(<%#Eval("FromUserID") %>);' style="cursor: pointer;" title="Send the notification to this user.">
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
                <asp:ObjectDataSource ID="ObjectDataSourceRequestApproval" runat="server" SelectMethod="GetRequestByRole"
                    TypeName="RMC.BussinessService.BSViewRequest" OnSelecting="ObjectDataSourceRequestApproval_Selecting">
                    <SelectParameters>
                        <asp:Parameter Name="userID" Type="Int32" DefaultValue="0" />
                        <asp:Parameter Name="role" Type="String" DefaultValue="" />
                    </SelectParameters>
                </asp:ObjectDataSource>
            </td>
        </tr>
        <tr>
            <td align="left" style="padding-left: 15px;">
                <asp:Button ID="ButtonApproved" runat="server" Text="Accept Settings" CssClass="aspButton"
                    OnClick="ButtonApproved_Click" OnClientClick="return RadioChecked(this);" />
            </td>
        </tr>
    </table>
</asp:Panel>
<%--Added for Customized Notification--%>
<%--Added by Raman 01/Jan/2011--%>
<script language="javascript" type="text/javascript">
    function mypopupMessage(FromUserID) {
        mywindow = window.open("../Administrator/SendMessage.aspx?FromUserID=" + FromUserID, "", "location=1,status=0,scrollbars=1, width=550,height=400,resizable=no ");
        return false;
    } 
</script>
