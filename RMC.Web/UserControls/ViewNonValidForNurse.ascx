<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ViewNonValidForNurse.ascx.cs"
    Inherits="RMC.Web.UserControls.ViewNonValidForNurse" %>
<asp:GridView ID="GridViewNonValidDataOfNurse" runat="server" Width="450px" CssClass="GridViewStyle"
    DataKeyNames="NurseID" CellPadding="5" EmptyDataText="No Record to display."
    GridLines="None" AutoGenerateColumns="False" HorizontalAlign="Left" DataSourceID="LinqDataSourceNonValidDataOfNurse"
    OnRowDataBound="GridViewNonValidDataOfNurse_RowDataBound">
    <HeaderStyle CssClass="GridViewHeaderStyle" />
    <EmptyDataRowStyle CssClass="GridViewEmptyRowStyle" />
    <Columns>
        <asp:TemplateField HeaderText="Configuration Name">
            <ItemTemplate>
                <%#Eval("ConfigName")%>
            </ItemTemplate>
            <EditItemTemplate>
                <asp:TextBox ID="TextBoxConfigurationName" Text='<%#Eval("ConfigName")%>' runat="server"></asp:TextBox>
                <asp:Literal ID="LiteralConfigName" Text='<%#Eval("IsErrorInConfigName")%>' Visible='false'
                    runat="server"></asp:Literal>
            </EditItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="Nurse Name">
            <ItemTemplate>
                <%#Eval("NurseName")%>
            </ItemTemplate>
            <EditItemTemplate>
                <asp:TextBox ID="TextBoxNurseName" Text='<%#Eval("NurseName")%>' runat="server"></asp:TextBox>
                <asp:Literal ID="LiteralNurseName" Text='<%#Eval("IsErrorInNurseName")%>' Visible='false'
                    runat="server"></asp:Literal>
            </EditItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="Patients Per Nurse">
            <ItemTemplate>
                <%#Eval("PatientsPerNurse")%>
            </ItemTemplate>
            <EditItemTemplate>
                <asp:TextBox ID="TextBoxPatientsPerNurse" Text='<%#Eval("PatientsPerNurse")%>' runat="server"></asp:TextBox>
                <asp:Literal ID="LiteralPatientsPerNurseName" Text='<%#Eval("IsErrorInPatientsPerNurse")%>'
                    Visible='false' runat="server"></asp:Literal>
            </EditItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="Action">
            <ItemTemplate>
            <table>
                <tr>
                    <td>
                    <asp:LinkButton ID="LinkButtonEdit" runat="server" OnClick="LinkButtonEdit_Click">Edit</asp:LinkButton>
                    </td>
                    <td>
                        <asp:LinkButton ID="LinkButtonDelete" runat="server" 
                            onclick="LinkButtonDelete_Click">Delete</asp:LinkButton>
                    </td>
                </tr>
            </table>
                
            </ItemTemplate>
            <EditItemTemplate>
                <table width="100%">
                    <tr>
                        <td>
                            <asp:LinkButton ID="LinkButtonUpdate" runat="server" OnClick="LinkButtonUpdate_Click">Update</asp:LinkButton>
                        </td>
                        <td>
                            <asp:LinkButton ID="LinkButtonCancel" runat="server" OnClick="LinkButtonCancel_Click">Cancel</asp:LinkButton>
                        </td>
                    </tr>
                </table>
            </EditItemTemplate>
        </asp:TemplateField>
    </Columns>
    <FooterStyle CssClass="GridViewFooterStyle" />
    <PagerStyle CssClass="GridViewPagerStyle" />
    <SelectedRowStyle CssClass="GridViewSelectedRowStyle" />
    <AlternatingRowStyle CssClass="GridViewAlternatingRowStyle" />
    <RowStyle CssClass="GridViewRowStyle" />
</asp:GridView>
<asp:LinqDataSource ID="LinqDataSourceNonValidDataOfNurse" runat="server" ContextTypeName="RMC.DataService.RMCDataContext"
    Select="new (NurseName, ConfigName, PatientsPerNurse, IsErrorInNurseName, IsErrorInPatientsPerNurse, IsErrorInConfigName, NurseID)"
    TableName="NursePDAInfos" Where="IsErrorExist == @IsErrorExist">
    <WhereParameters>
        <asp:Parameter DefaultValue="true" Name="IsErrorExist" Type="Boolean" />
    </WhereParameters>
</asp:LinqDataSource>
