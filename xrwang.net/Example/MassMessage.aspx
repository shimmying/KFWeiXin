<%@ Page Language="C#" AutoEventWireup="true" CodeFile="MassMessage.aspx.cs" Inherits="Example_MassMessage" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>群发消息</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <h3>群发消息</h3>
        <h4>提示：<asp:Literal runat="server" ID="ltrMessage" Text="本示例演示群发消息的使用。" /></h4>
        <p>
            请选择公众号：
            <asp:ListBox runat="server" ID="lbPublicAccount" Rows="1"></asp:ListBox>
        </p>
        <p>
            请输入待群发的消息内容：
            <asp:TextBox runat="server" ID="txtContent" Text="" />
        </p>
        <table border="1">
            <tr>
                <td>用户组：</td>
                <td>用户：</td>
                <td>已群发消息：</td>
            </tr>
            <tr style="vertical-align:top; min-height:200px; height:200px;">
                <td><asp:RadioButtonList runat="server" ID="rblGroup" RepeatDirection="Vertical" /></td>
                <td><asp:CheckBoxList runat="server" ID="cblUser" RepeatDirection="Vertical" RepeatColumns="5" /></td>
                <td><asp:RadioButtonList runat="server" ID="rblMassMessage" RepeatDirection="Vertical" /></td>
            </tr>
            <tr style="vertical-align:top;">
                <td>
                    <asp:Button runat="server" ID="btnGetGroup" Text="获取用户组" OnClick="btnGetGroup_Click" /><br />
                    <asp:Button runat="server" ID="btnSendToGroup" Text="按用户组群发" OnClick="btnSendToGroup_Click" />
                </td>
                <td>
                    <asp:Button runat="server" ID="btnGetUser" Text="获取用户" OnClick="btnGetUser_Click" /><br />
                    <asp:Button runat="server" ID="btnSendToUsers" Text="按用户列表群发" OnClick="btnSendToUsers_Click" /><br />
                    <asp:Button runat="server" ID="btnPreview" Text="预览群发消息" OnClick="btnPreview_Click" />
                </td>
                <td>
                    <asp:Button runat="server" ID="btnDelete" Text="删除群发消息" OnClick="btnDelete_Click" /><br />
                    <asp:Button runat="server" ID="btnGetStatus" Text="查询群发状态" OnClick="btnGetStatus_Click" />
                </td>
            </tr>
        </table>
        <script type="text/javascript" src="../js/jquery-1.11.2.min.js"></script>
    </div>
    </form>
</body>
</html>
