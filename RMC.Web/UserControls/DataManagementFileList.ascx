<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DataManagementFileList.ascx.cs"
    Inherits="RMC.Web.UserControls.DataManagementFileList" %>
<style type="text/css">
    .Background
    {
        position: fixed;
        left: 0;
        top: 0;
        z-index: 10;
        width: 100%;
        height: 100%;
        filter: alpha(opacity=60);
        opacity: 0.6;
        background-color: Black;
        visibility: hidden;
    }
</style>
<script type="text/javascript">

    function pageLoad() {

        var manager = Sys.WebForms.PageRequestManager.getInstance();

        manager.add_initializeRequest(CheckStatus);
        manager.add_endRequest(endRequest);
        manager.add_beginRequest(onBeginRequest);
    }

    function CheckStatus(sender, args) {

        if (getLinkButtonName(args.get_postBackElement().id) != 'LinkButtonFileName') {

            if (!confirm('Are you sure you wish to delete these Files and it\'s related records?')) {
                args.set_cancel(true);
            }
        }
    }

    function onBeginRequest(sender, args) {

        var divParent = $get('ParentDiv');
        var divImage = $get('IMGDIV');

        divParent.className = 'Background';
        divParent.style.visibility = 'visible';
        divImage.style.visibility = 'visible';
    }

    function endRequest(sender, args) {

        var divParent = $get('ParentDiv');
        var divImage = $get('IMGDIV');

        divParent.className = '';
        divParent.style.visibility = 'hidden';
        divImage.style.visibility = 'hidden';
    }

    function getLinkButtonName(fullName) {

        var arr = Array();
        arr = fullName.split('_');

        return arr[arr.length - 1];
    }

    function getEndRequest() {

        var divParent = top.document.getElementById('ParentDiv');
        var divImage = top.document.getElementById('IMGDIV');

        top.document.getElementById('iframeFileList').src = '';
        divParent.className = '';
        divParent.style.visibility = 'hidden';
        divImage.style.visibility = 'hidden';
    }

</script>
<asp:ScriptManager ID="ScriptManager1" runat="server">
</asp:ScriptManager>
<table width="99%">
    <tr>
        <th align="left" style="font-size: 14px; color: #06569D; padding-left: 10px; padding-top: 10px;">
            <u>Data Management</u>
        </th>
        <th align="right" style="padding-top: 10px;">
            <asp:ImageButton ID="ImageButtonBack" ToolTip="Back" runat="server" ImageUrl="~/Images/back.gif"
                TabIndex="6" OnClick="ImageButtonBack_Click" />
        </th>
    </tr>
    <tr>
        <td style="height: 10px;" colspan="2">
        </td>
    </tr>
    <tr>
        <td align="left" colspan="2">
            <div style="padding-left: 15px;">
                <asp:LinkButton ID="LinkButtonHospitalIndex" Font-Bold="true" Font-Size="11px" runat="server"
                    OnClick="LinkButtonHospitalIndex_Click">Hospital Index</asp:LinkButton>
            </div>
            <div style="padding-left: 30px; padding-top: 3px; width: 100%; height: 14px;">
                <div style="float: left; width: 87%;">
                    <asp:LinkButton ID="LinkButtonHospitalInformation" Font-Bold="true" Font-Size="11px"
                        runat="server" OnClick="LinkButtonHospitalInformation_Click">LinkButton</asp:LinkButton>
                </div>
                <div style="float: left; width: 12%;" id="divEditHospital" runat="server">
                </div>
            </div>
            <div style="padding-left: 45px; padding-top: 3px; width: 100%; height: 14px;">
                <div style="float: left; width: 84%;">
                    <asp:LinkButton ID="LinkButtonHospitalUnitInformation" Font-Bold="true" Font-Size="11px"
                        runat="server" OnClick="LinkButtonHospitalUnitInformation_Click">LinkButton</asp:LinkButton>
                </div>
                <div style="float: left; width: 15%;" id="divEditHospitalUnit" runat="server">
                </div>
            </div>
            <div style="padding-left: 60px; padding-top: 2px;">
                <asp:LinkButton ID="LinkButtonYear" Font-Bold="true" Font-Size="11px" runat="server"
                    OnClick="LinkButtonYear_Click">LinkButton</asp:LinkButton>
            </div>
            <div style="padding-left: 75px; padding-top: 2px;">
                <asp:LinkButton ID="LinkButtonMonth" Font-Bold="true" Font-Size="11px" runat="server"
                    OnClick="LinkButtonMonth_Click">LinkButton</asp:LinkButton>
            </div>
        </td>
    </tr>
    <tr>
        <td align="right" colspan="2">
            <span style="color: #06569d; font-size: 13px; font-weight: bold; padding-right: 5px;">
                (
                <asp:LinkButton ID="LinkButtonAddData" Font-Bold="true" Font-Size="13px" ForeColor="#06569d"
                    runat="server">Add Data</asp:LinkButton>
                )</span>
        </td>
    </tr>
    <tr>
        <td colspan="2">
            <hr width="100%" />
        </td>
    </tr>
    <tr>
        <td style="height: 2px;" colspan="2">
        </td>
    </tr>
    <tr>
        <td colspan="2" align="right">
            <asp:LinkButton ID="LinkButtonDeleteAll" Font-Bold="true" Font-Size="11px" runat="server"
                OnClick="LinkButtonDeleteAll_Click">Delete Selected</asp:LinkButton>
        </td>
    </tr>
    <tr>
        <td align="left" style="font-size: 10px;" colspan="2">
            <asp:MultiView ID="MultiViewShowFiles" runat="server">
                <asp:View ID="ViewSuperAdmin" runat="server">
                    <asp:UpdatePanel ID="UpdatePanelShowFiles" runat="server">
                        <ContentTemplate>
                            <div style="border-bottom: groove 2px #cccccc; border-right: groove 2px #cccccc;">
                                <div id="divShowEmptyMessage" style="background-color: #06569d; font-size: 13px;
                                    font-weight: bold; color: White; padding-top: 5px; height: 20px; padding-left: 10px;"
                                    runat="server" visible="false">
                                    No Records
                                </div>
                                <asp:Repeater ID="RepeaterShowFileType" runat="server" DataSourceID="ObjectDataSource1">
                                    <ItemTemplate>
                                        <div style="font-size: 14px; font-weight: bold; background-image: url('../Images/FileListHeader.gif');
                                            color: White; height: 28px; padding-left: 5px; padding-top: 10px;">
                                            <%# DataBinder.Eval(Container.DataItem,"ConfigName") %>
                                        </div>
                                        <div style="padding-top: 1px;">
                                            <asp:Repeater ID="RepeaterShowFileList" runat="server" DataSource='<%# DataBinder.Eval(Container.DataItem,"FileList") %>'>
                                                <HeaderTemplate>
                                                    <div style="background-image: url('../Images/FileListHeader.gif'); color: White;
                                                        height: 20px; font-size: 13px; font-weight: bold; vertical-align: middle; padding-left: 10px;
                                                        padding-bottom: 4px; padding-top: 5px;">
                                                        File Name
                                                    </div>
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <div style="font-size: 11px; font-weight: bold; padding-left: 10px; background-color: #f7f6f3;">
                                                        <table>
                                                            <tr>
                                                                <td align="right">
                                                                    <asp:CheckBox ID="CheckBoxFileName" runat="server" />
                                                                    <asp:HiddenField ID="HiddenFieldNurseID" Value='<%# DataBinder.Eval(Container.DataItem,"NurseID") %>'
                                                                        runat="server" />
                                                                    <asp:HiddenField ID="HiddenFieldHospitalUploadID" Value='<%# DataBinder.Eval(Container.DataItem,"HospitalUploadID") %>'
                                                                        runat="server" />
                                                                </td>
                                                                <td align="left">
                                                                    <asp:LinkButton ID="LinkButtonFileName" runat="server" OnClick="LinkButtonFileName_Click"
                                                                        Font-Bold="true" Font-Size="10px"><%# DataBinder.Eval(Container.DataItem, "FileReff")%></asp:LinkButton>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </div>
                                                </ItemTemplate>
                                                <AlternatingItemTemplate>
                                                    <div style="font-size: 11px; font-weight: bold; padding-left: 10px;">
                                                        <table>
                                                            <tr>
                                                                <td align="right">
                                                                    <asp:CheckBox ID="CheckBoxFileName" runat="server" />
                                                                    <asp:HiddenField ID="HiddenFieldNurseID" Value='<%# DataBinder.Eval(Container.DataItem,"NurseID") %>'
                                                                        runat="server" />
                                                                    <asp:HiddenField ID="HiddenFieldHospitalUploadID" Value='<%# DataBinder.Eval(Container.DataItem,"HospitalUploadID") %>'
                                                                        runat="server" />
                                                                </td>
                                                                <td align="left">
                                                                    <asp:LinkButton ID="LinkButtonFileName" runat="server" OnClick="LinkButtonFileName_Click"
                                                                        Font-Bold="true" Font-Size="10px"><%# DataBinder.Eval(Container.DataItem, "FileReff")%></asp:LinkButton>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </div>
                                                </AlternatingItemTemplate>
                                            </asp:Repeater>
                                        </div>
                                    </ItemTemplate>
                                </asp:Repeater>
                            </div>
                            <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" SelectMethod="GetFileListByHospitalUnitID"
                                TypeName="RMC.BussinessService.BSDataManagement">
                                <SelectParameters>
                                    <asp:QueryStringParameter DefaultValue="0" Name="hospitalUnitID" QueryStringField="HospitalDemographicID"
                                        Type="Int32" />
                                    <%--<asp:QueryStringParameter DefaultValue="0" Name="hospitalUnitID" QueryStringField="HospitalUnitID"
                                        Type="Int32" />--%>
                                    <asp:QueryStringParameter DefaultValue="0" Name="year" QueryStringField="Year" Type="String" />
                                    <asp:QueryStringParameter DefaultValue="0" Name="month" QueryStringField="Month"
                                        Type="String" />
                                </SelectParameters>
                            </asp:ObjectDataSource>
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="LinkButtonDeleteAll" EventName="Click" />
                        </Triggers>
                    </asp:UpdatePanel>
                </asp:View>
                <asp:View ID="ViewUsers" runat="server">
                    <asp:UpdatePanel ID="UpdatePanelUsers" runat="server">
                        <ContentTemplate>
                            <div style="border-bottom: groove 2px #cccccc; border-right: groove 2px #cccccc;">
                                <div id="divShowEmptyRecords" style="background-color: #06569d; font-size: 13px;
                                    font-weight: bold; color: White; padding-top: 5px; height: 20px; padding-left: 10px;"
                                    runat="server" visible="false">
                                    No records to display.
                                </div>
                                <asp:Repeater ID="RepeaterShowUsersFiles" runat="server" DataSourceID="ObjectDataSource2">
                                    <ItemTemplate>
                                        <div style="font-size: 14px; font-weight: bold; background-image: url('../Images/FileListHeader.gif');
                                            color: White; height: 28px; padding-left: 5px; padding-top: 10px;">
                                            <%# DataBinder.Eval(Container.DataItem,"ConfigName") %>
                                        </div>
                                        <div style="padding-top: 1px;">
                                            <asp:Repeater ID="RepeaterShowFileList" runat="server" DataSource='<%# DataBinder.Eval(Container.DataItem,"FileList") %>'>
                                                <HeaderTemplate>
                                                    <div style="background-image: url('../Images/FileListHeader.gif'); color: White;
                                                        height: 20px; font-size: 13px; font-weight: bold; vertical-align: middle; padding-left: 10px;
                                                        padding-bottom: 4px; padding-top: 5px;">
                                                        File Name
                                                    </div>
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <div style="font-size: 11px; font-weight: bold; padding-left: 10px; background-color: #f7f6f3;">
                                                        <table>
                                                            <tr>
                                                                <td align="right">
                                                                    <asp:HiddenField ID="HiddenFieldNurseID" Value='<%# DataBinder.Eval(Container.DataItem,"NurseID") %>'
                                                                        runat="server" />
                                                                    <asp:HiddenField ID="HiddenFieldHospitalUploadID" Value='<%# DataBinder.Eval(Container.DataItem,"HospitalUploadID") %>'
                                                                        runat="server" />
                                                                </td>
                                                                <td align="left">
                                                                    <div style="line-height: 20px">
                                                                        <asp:LinkButton ID="LinkButtonFileName" runat="server" OnClick="LinkButtonFileName_Click"
                                                                            Font-Bold="true" Font-Size="10px"><%# DataBinder.Eval(Container.DataItem, "FileReff")%></asp:LinkButton>
                                                                    </div>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </div>
                                                </ItemTemplate>
                                                <AlternatingItemTemplate>
                                                    <div style="font-size: 11px; font-weight: bold; padding-left: 10px;">
                                                        <table>
                                                            <tr>
                                                                <td align="right">
                                                                    <asp:HiddenField ID="HiddenFieldNurseID" Value='<%# DataBinder.Eval(Container.DataItem,"NurseID") %>'
                                                                        runat="server" />
                                                                    <asp:HiddenField ID="HiddenFieldHospitalUploadID" Value='<%# DataBinder.Eval(Container.DataItem,"HospitalUploadID") %>'
                                                                        runat="server" />
                                                                </td>
                                                                <td align="left">
                                                                    <asp:LinkButton ID="LinkButtonFileName" runat="server" OnClick="LinkButtonFileName_Click"
                                                                        Font-Bold="true" Font-Size="10px"><%# DataBinder.Eval(Container.DataItem, "FileReff")%></asp:LinkButton>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </div>
                                                </AlternatingItemTemplate>
                                            </asp:Repeater>
                                        </div>
                                    </ItemTemplate>
                                </asp:Repeater>
                            </div>
                            <asp:ObjectDataSource ID="ObjectDataSource2" runat="server" SelectMethod="GetFileListByHospitalUnitID"
                                TypeName="RMC.BussinessService.BSDataManagement">
                                <SelectParameters>
                                    <asp:QueryStringParameter DefaultValue="0" Name="hospitalUnitID" QueryStringField="HospitalUnitID"
                                        Type="Int32" />
                                    <asp:QueryStringParameter DefaultValue="0" Name="year" QueryStringField="Year" Type="String" />
                                    <asp:QueryStringParameter DefaultValue="0" Name="month" QueryStringField="Month"
                                        Type="String" />
                                </SelectParameters>
                            </asp:ObjectDataSource>
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="LinkButtonDeleteAll" EventName="Click" />
                        </Triggers>
                    </asp:UpdatePanel>
                </asp:View>
            </asp:MultiView>
        </td>
    </tr>
</table>
<div id="ParentDiv" class="Background" style="visibility: hidden;">
    <div id="IMGDIV" align="center" valign="middle" style="position: absolute; left: 45%;
        top: 50%; visibility: hidden; vertical-align: middle; border-style: none; border-color: black;
        z-index: 40;">
        <asp:Image ID="Image2" runat="server" ImageUrl="~/Images/Loading.gif" BorderWidth="0px"
            AlternateText="Loading"></asp:Image>
    </div>
</div>
