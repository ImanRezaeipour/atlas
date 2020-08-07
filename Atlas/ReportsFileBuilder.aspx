<%@ Page Language="C#" AutoEventWireup="true" Inherits="ReportsFileBuilder" CodeBehind="ReportsFileBuilder.aspx.cs" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <fieldset>
                <legend>Update report file</legend>
                <table style="width: 100%;">
                    <tr>
                        <td style="width: 10%">
                            <asp:Button ID="btnCreateReportFiles" runat="server" Text="Create All Report Files" OnClick="btnCreateReportFiles_Click" />
                        </td>
                        <td>&nbsp;</td>
                    </tr>
                    <tr>
                        <td style="width: 10%">
                            <asp:Button ID="btnUpdateReportFile" runat="server" Text="Update Report File" Width="197px" OnClick="btnUpdateReportFile_Click" />
                        </td>
                        <td>
                            <span>Report File ID:</span><span><asp:TextBox runat="server" ID="txtReportFileID"></asp:TextBox></span>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <asp:Label ID="lblMessage" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <hr />
                        </td>
                    </tr>

                </table>
            </fieldset>
            <fieldset>
                <legend>Upload report file to database</legend>
                <table>
                    <tr>
                        <td >Root:</td>
                        <td >
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="DrpRoot" ErrorMessage="RequiredFieldValidator" ValidationGroup="Upload">*</asp:RequiredFieldValidator>
                        </td>
                        <td >
                            <asp:DropDownList ID="DrpRoot" runat="server" Width="400px">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td >
                            Report File:</td>
                        <td >
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="FileUpload1" ErrorMessage="RequiredFieldValidator" ValidationGroup="Upload">*</asp:RequiredFieldValidator>
                        </td>
                        <td >
                            <asp:FileUpload ID="FileUpload1" runat="server" Width="400px" />
                        </td>
                    </tr>
                    <tr>
                        <td >
                            &nbsp;</td>
                        <td >
                            &nbsp;</td>
                        <td >
                            <asp:Button ID="Button1" runat="server" Text="Save" OnClick="Button1_Click" Width="106px" ValidationGroup="Upload" />
                        </td>
                    </tr>
                    <tr>
                        <td >&nbsp;</td>
                        <td >
                            &nbsp;</td>
                        <td >
                            <asp:Label ID="lblMessage2" runat="server"></asp:Label>
                        </td>
                    </tr>
                </table>
            </fieldset>
        </div>
    </form>
</body>
</html>
