<%@ Application Language="C#" %>
<%@ import namespace="xrwang.net" %>
<%@ import namespace="xrwang.PublicLibrary" %>
<%@ import namespace="xrwang.weixin.PublicAccount" %>

<script runat="server">

    void Application_Start(object sender, EventArgs e) 
    {
        // 在应用程序启动时运行的代码
        //填写默认的SQL SERVER数据库连接字符串
        DataAccess.connectionString = "Data Source=YourDataSource;Initial Catalog=YourDb;Persist Security Info=True;User ID=YourUserId;Password=YourPassword";
        //加入测试公众号
        AccountInfoCollection.SetAccountInfo(new AccountInfo("YourId", "AppId", "AppSecret", "Token", "EncodingAesKey", "测试公众号"));
        //加入xrwang公众号
        AccountInfoCollection.SetAccountInfo(new AccountInfo("YourId2", "AppId", "AppSecret", "Token", "EncodingAesKey", "酷型男"));
    }
    
    void Application_End(object sender, EventArgs e) 
    {
        //  在应用程序关闭时运行的代码

    }

    void Application_Error(object sender, EventArgs e)
    {
        if (Server.GetLastError() == null)
            return;
        Exception exp = Server.GetLastError().GetBaseException();
        HandleException(exp);
        if (exp.InnerException != null)
            HandleException(exp.InnerException);
    }

    /// <summary>
    /// 处理异常
    /// </summary>
    /// <param name="e"></param>
    private void HandleException(Exception e)
    {
        string err = string.Format("捕获未处理的异常。\r\n出错页面：{0}\r\n出错方法：{1}\r\n错误源：{2}\r\n错误提示：{3}\r\n堆栈：{4}",
            Request != null ? Request.RawUrl.ToString() : "", e.TargetSite, e.Source, e.Message, e.StackTrace);
        Message message = new Message(MessageType.Exception, err);
        Message.Insert(message);
    }

    void Session_Start(object sender, EventArgs e) 
    {
        // 在新会话启动时运行的代码

    }

    void Session_End(object sender, EventArgs e) 
    {
        // 在会话结束时运行的代码。 
        // 注意: 只有在 Web.config 文件中的 sessionstate 模式设置为
        // InProc 时，才会引发 Session_End 事件。如果会话模式设置为 StateServer
        // 或 SQLServer，则不引发该事件。

    }
       
</script>
