<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ParseRequestMessage.aspx.cs" Inherits="KFWeiXinWeb.Example.ParseRequestMessage" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>解析消息</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <h3>解析消息</h3>
        <h4>提示：本示例演示解析消息。</h4>
        <p>
            消息文本：
            <asp:TextBox runat="server" ID="txtMessage" Text="" TextMode="MultiLine" Height="200" Width="500" />
        </p>
        <p>
            解析结果：
            <asp:TextBox runat="server" ID="txtResult" Text="" TextMode="MultiLine" Height="200" Width="500" />
        </p>
        <p>
            <asp:Button runat="server" ID="btnParse" Text="开始解析" OnClick="btnParse_Click" />
        </p>
    </div>
    </form>
</body>
</html>
