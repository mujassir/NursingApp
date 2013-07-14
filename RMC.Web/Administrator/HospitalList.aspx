<%@ Page Language="C#" MasterPageFile="~/Administrator/AdminMaster.Master" AutoEventWireup="true"
    CodeBehind="HospitalList.aspx.cs" Inherits="RMC.Web.Administrator.HospitalList"
    Title="RMC:Hospital List" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:Panel ID="PanelHospitalList" runat="server" DefaultButton="ButtonSubmit">
        <center>
            <table style="width: 100%;">
                <tr>
                    <th>
                        <h3 style="font-size: 13px;">
                            Hospital List
                        </h3>
                    </th>
                </tr>
                <tr>
                    <td style="height: 2px;">
                        <asp:Label ID="LabelErrorMsg" runat="server" Font-Size="12px" Visible="False"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        <table width="630px">
                            <tr>
                                <td colspan="2">
                                    <asp:GridView ID="GridViewHospitalInfoList" runat="server" Width="630px" BackColor="#F79220"
                                        DataKeyNames="HospitalInfoID" BorderColor="#F79220" BorderStyle="None" BorderWidth="1px"
                                        CellPadding="3" CellSpacing="2" EmptyDataText="Hospitals records doesn't exists."
                                        AutoGenerateColumns="False" DataSourceID="LinqDataSourceHospitalInfoList" HorizontalAlign="Center">
                                        <RowStyle BackColor="#FFF7E7" ForeColor="#06569D" HorizontalAlign="Left" />
                                        <EmptyDataRowStyle BackColor="#A55129" Font-Bold="True" ForeColor="White" />
                                        <Columns>
                                            <asp:TemplateField HeaderText="IsActive">
                                                <ItemTemplate>
                                                    <center>
                                                        <asp:CheckBox ID="CheckBoxActive" Checked='<%#Eval("IsActive")%>' runat="server" />
                                                    </center>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" Wrap="False" />
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="HospitalName" HeaderText="HospitalName" ReadOnly="True">
                                                <HeaderStyle Wrap="False" />
                                                <ItemStyle Wrap="False" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="ChiefNursingOfficer" HeaderText="C.N.O. Name" ReadOnly="True">
                                                <HeaderStyle Wrap="False" />
                                                <ItemStyle Wrap="False" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="ChiefNursingOfficerPhone" HeaderText="C.N.O. Phone" ReadOnly="True">
                                                <HeaderStyle Wrap="False" />
                                                <ItemStyle Wrap="False" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="Address" HeaderText="Address" ReadOnly="True">
                                                <HeaderStyle Wrap="False" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="City" HeaderText="City" ReadOnly="True">
                                                <HeaderStyle Wrap="False" />
                                                <ItemStyle Wrap="False" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="StateName" HeaderText="State" ReadOnly="True">
                                                <HeaderStyle Wrap="False" />
                                                <ItemStyle Wrap="False" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="Zip" HeaderText="Zip" ReadOnly="True">
                                                <HeaderStyle Wrap="False" />
                                                <ItemStyle Wrap="False" />
                                            </asp:BoundField>
                                            <asp:TemplateField HeaderText="Edit">
                                                <ItemTemplate>
                                                    <asp:ImageButton ID="ImageButtonEdit" runat="server" OnClick="ImageButtonEdit_Click"
                                                        ToolTip="Edit Hospital Information" ImageUrl="~/Images/edit.png" />
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" />
                                                <ItemStyle HorizontalAlign="Center" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Delete">
                                                <ItemTemplate>
                                                    <asp:ImageButton ID="ImageButtonDelete" runat="server" OnClick="ImageButtonDelete_Click"
                                                        OnClientClick="return confirm('Are you sure you want to delete?')" ToolTip="Delete Hospital Information"
                                                        ImageUrl="~/Images/delete.png" />
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" />
                                                <ItemStyle HorizontalAlign="Center" />
                                            </asp:TemplateField>
                                        </Columns>
                                        <FooterStyle BackColor="#F7DFB5" ForeColor="White" />
                                        <PagerStyle ForeColor="White" HorizontalAlign="Center" BackColor="#A50D0C" Font-Bold="True"
                                            Font-Size="13px" />
                                        <SelectedRowStyle BackColor="#738A9C" Font-Bold="True" ForeColor="White" />
                                        <HeaderStyle BackColor="#A50D0C" Font-Bold="True" ForeColor="White" HorizontalAlign="Left" />
                                        <AlternatingRowStyle HorizontalAlign="Left" />
                                    </asp:GridView>
                                    <asp:LinqDataSource ID="LinqDataSourceHospitalInfoList" runat="server" ContextTypeName="RMC.DataService.RMCDataContext"
                                        Select="new (HospitalInfoID, HospitalName, ChiefNursingOfficerFirstName, ChiefNursingOfficerLastName, ChiefNursingOfficerPhone, Address, City, Zip, IsActive, State.StateName As StateName)"
                                        TableName="HospitalInfos" Where="IsDeleted != true">
                                    </asp:LinqDataSource>
                                </td>
                            </tr>
                            <tr>
                                <td align="left">
                                    <asp:Button ID="ButtonSubmit" runat="server" Text="Submit Changes" OnClick="ButtonSubmit_Click"
                                        CssClass="aspButton" Width="130px" />
                                </td>
                                <td align="right">
                                    <asp:Button ID="ButtonReset" runat="server" Text="Reset" OnClick="ButtonReset_Click"
                                        CssClass="aspButton" Visible="false" />
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </center>
    </asp:Panel>
</asp:Content>
