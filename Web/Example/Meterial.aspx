<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Meterial.aspx.cs" Inherits="KFWeiXinWeb.Example.Meterial" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>永久素材管理</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <h3>永久素材管理</h3>
        <h4>提示：<asp:Literal runat="server" ID="ltrMessage" Text="本示例演示永久素材管理。临时素材管理请看基础接口。" /></h4>
        <p>
            请选择公众号：
            <asp:ListBox runat="server" ID="lbPublicAccount" Rows="1" />
        </p>
        <p>
            <h5>新增素材</h5>
            素材类型：<asp:ListBox runat="server" ID="lbMultiMediaType" Rows="1" /><br />
            多媒体文件：<asp:FileUpload runat="server" ID="fileUpload" />
            <asp:Button runat="server" ID="btnAdd" Text="新增" OnClick="btnAdd_Click" />
        </p>
        <p>
            <h5>获取素材</h5>
            素材列表：<br />
            <asp:ListBox runat="server" ID="lbMeterial" Width="600px" Height="400px" /><br />
            <asp:Button runat="server" ID="btnGetCount" Text="获取素材总数" OnClick="btnGetCount_Click" />
            <asp:Button runat="server" ID="btnBatchGet" Text="批量获取素材列表" OnClick="btnBatchGet_Click" />
            <asp:Button runat="server" ID="btnDelete" Text="删除选中的素材" OnClick="btnDelete_Click" />
            <asp:Button runat="server" ID="btnGet" Text="获取选中的素材" OnClick="btnGet_Click" />
        </p>
    </div>
    </form>
</body>
</html>
