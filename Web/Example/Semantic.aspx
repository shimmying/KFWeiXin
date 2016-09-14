<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Semantic.aspx.cs" Inherits="KFWeiXinWeb.Example.Semantic" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>语义理解接口测试</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <h3>语义理解接口</h3>
        <h4>提示：<asp:Literal runat="server" ID="ltrMessage" Text="" /></h4>
        请选择公众号：<asp:ListBox runat="server" ID="lbPublicAccount" Rows="1" Width="400"></asp:ListBox><br />
        输入文本：<asp:TextBox runat="server" ID="txtQuery" Text="" Width="400" /><br />
        服务类别：<asp:CheckBoxList runat="server" ID="cblType" RepeatDirection="Horizontal" RepeatColumns="5" /><br />
        城市：<asp:TextBox runat="server" ID="txtCity" Text="" />
        区域：<asp:TextBox runat="server" ID="txtRegion" Text="" /><br />
        应用id：<asp:TextBox runat="server" ID="txtAppid" Text="" />
        用户id：<asp:TextBox runat="server" ID="txtUid" Text="" /><br />
        结果类型：
        <asp:RadioButton runat="server" ID="rbJson" Text="Json" GroupName="ResultType" Checked="true" />
        <asp:RadioButton runat="server" ID="rbEntity" Text="实体类" GroupName="ResultType" Checked="false"/><br />
        结果：<br />
        <asp:TextBox runat="server" ID="txtResult" Text="" TextMode="MultiLine" Width="400" Height="300" /><br />
        <asp:Button runat="server" ID="btnQuery" Text="请求" Width="150" OnClick="btnQuery_Click" />
    </div>
    </form>
</body>
</html>
