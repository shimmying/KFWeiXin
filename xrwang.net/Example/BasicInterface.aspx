<%@ Page Language="C#" AutoEventWireup="true" CodeFile="BasicInterface.aspx.cs" Inherits="Example_BasicInterface" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>基础接口</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <h3>基础接口</h3>
        <h4>提示：<asp:Literal runat="server" ID="ltrMessage" Text="本示例演示了基础接口的用法，包括：许可令牌、微信服务器地址、上传下载多媒体文件、创建二维码、长链接转短链接。" /></h4>
        <p>
            请选择公众号：
            <asp:ListBox runat="server" ID="lbPublicAccount" Rows="1"></asp:ListBox>
        </p>
        <p>
            许可令牌：
            <asp:TextBox runat="server" ID="txtAccessToken" Text="" />
            <asp:Button runat="server" ID="btnGetAccessToken" Text="获取" OnClick="btnGetAccessToken_Click" />
        </p>
        <p>
            微信服务器地址：
            <asp:TextBox runat="server" ID="txtServerAddress" Text="" />
            <asp:Button runat="server" ID="btnGetServerAddress" Text="获取" OnClick="btnGetServerAddress_Click" />
        </p>
        <p>
            <h5>上传多媒体文件</h5>
            多媒体类型：<asp:ListBox runat="server" ID="lbMultiMediaType" Rows="1" /><br />
            多媒体文件：<asp:FileUpload runat="server" ID="fileUpload" />
            <asp:Button runat="server" ID="btnUpload" Text="上传" OnClick="btnUpload_Click" /><br />
            <asp:HyperLink runat="server" ID="hlShowMultiMedia" Text="查看已上传的文件" Target="_blank"/>
        </p>
        <p>
            <h5>创建二维码</h5>
            <asp:CheckBox runat="server" ID="cbIsTemple" Text="临时二维码" Checked="true" />
            有效时间：<asp:TextBox runat="server" ID="txtExpireSeconds" Text="1200" />秒<br />
            场景值ID：<asp:TextBox runat="server" ID="txtSceneId" Text="" />
            <asp:Button runat="server" ID="btnCreateQrCode" Text="创建" OnClick="btnCreateQrCode_Click" /><br />
            <asp:Image runat="server" ID="imgQrCode" />
        </p>
        <p>
            <h5>长链接转短链接</h5>
            长链接：<asp:TextBox runat="server" ID="txtLongUrl" Text="http://www.cnblogs.com/xrwang/p/PublicAccount-QuickStart.html" /><br />
            短链接：<asp:TextBox runat="server" ID="txtShortUrl" Text="" ReadOnly="true" />
            <asp:Button runat="server" ID="btnGetShortUrl" Text="得到短链接" OnClick="btnGetShortUrl_Click" />
        </p>
    </div>
    </form>
</body>
</html>
