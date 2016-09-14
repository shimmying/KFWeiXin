<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Menu.aspx.cs" Inherits="Example_Menu" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>自定义菜单</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <h3>自定义菜单</h3>
        <h4>提示：<asp:Literal runat="server" ID="ltrMessage" Text="本示例演示自定义菜单的使用。如果您删除了菜单，请在离开之前，点击“创建”按钮，多谢。" /></h4>
        <p>
            请选择公众号：
            <asp:ListBox runat="server" ID="lbPublicAccount" Rows="1"></asp:ListBox>
        </p>
        <asp:TextBox runat="server" ID="txtMenu" TextMode="MultiLine" Width="600px" Height="400px" /><br />
        <asp:Button runat="server" ID="btnGet" Text="查询" OnClick="btnGet_Click" />
        <asp:Button runat="server" ID="btnCreate" Text="创建" OnClick="btnCreate_Click" />
        <asp:Button runat="server" ID="btnDelete" Text="删除" OnClick="btnDelete_Click" />
        <asp:Button runat="server" ID="btnGetSelfMenuInfo" Text="获取自定义菜单配置" OnClick="btnGetSelfMenuInfo_Click" />
    </div>
    </form>
</body>
</html>
