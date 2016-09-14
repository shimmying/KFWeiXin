<%@ WebHandler Language="C#" Class="WeixinInterface" %>

using System;
using System.Web;
using System.Net;
using xrwang.PublicLibrary;
using xrwang.weixin.PublicAccount;
using xrwang.net;

public class WeixinInterface : IHttpHandler
{

    public void ProcessRequest(HttpContext context)
    {
        string result = string.Empty;
        if (Validate(context))
        {
            if (context.Request.HttpMethod == WebRequestMethods.Http.Get)
                result = HandleGet(context);
            else if (context.Request.HttpMethod == WebRequestMethods.Http.Post)
                result = HandlePost(context);
        }
        else
            Message.Insert(new Message(MessageType.Exception, "校验消息失败。\r\n地址：" + context.Request.RawUrl));
        context.Response.Write(result);
    }

    public bool IsReusable
    {
        get
        {
            return false;
        }
    }
    
    /// <summary>
    /// 验证消息的有效性
    /// </summary>
    /// <param name="context"></param>
    /// <returns>如果消息有效，返回true；否则返回false。</returns>
    private bool Validate(HttpContext context)
    {
        string username = RequestEx.TryGetQueryString("username");  //在接口配置的URL中加入了username参数，表示哪个微信公众号
        AccountInfo account = AccountInfoCollection.GetAccountInfo(username);
        if (account == null)
            return false;
        string token = account.Token;
        string signature = RequestEx.TryGetQueryString("signature");
        string timestamp = RequestEx.TryGetQueryString("timestamp");
        string nonce = RequestEx.TryGetQueryString("nonce");
        if (string.IsNullOrWhiteSpace(signature) || string.IsNullOrWhiteSpace(timestamp) || string.IsNullOrWhiteSpace(nonce))
            return false;
        return xrwang.weixin.PublicAccount.Utility.CheckSignature(signature, token, timestamp, nonce);
    }

    /// <summary>
    /// 处理微信的GET请求，校验签名
    /// </summary>
    /// <param name="context"></param>
    /// <returns>返回echostr</returns>
    private string HandleGet(HttpContext context)
    {
        return RequestEx.TryGetQueryString("echostr");
    }

    /// <summary>
    /// 处理微信的POST请求
    /// </summary>
    /// <param name="context"></param>
    /// <returns>返回xml响应</returns>
    private string HandlePost(HttpContext context)
    {
        RequestMessageHelper helper = new RequestMessageHelper(context.Request);
        if (helper.Message != null)
        {
            Message.Insert(new Message(MessageType.Request, helper.Message.ToString()));
            ResponseBaseMessage responseMessage = HandleRequestMessage(helper.Message);
            Message.Insert(new Message(MessageType.Response, responseMessage.ToString()));
            return responseMessage.ToXml(helper.EncryptType);
        }
        else
            return string.Empty;
    }

    /// <summary>
    /// 处理请求消息，返回响应消息
    /// </summary>
    /// <returns>返回响应消息</returns>
    private ResponseBaseMessage HandleRequestMessage(RequestBaseMessage requestMessage)
    {
        ResponseTextMessage response = new ResponseTextMessage(requestMessage.FromUserName, requestMessage.ToUserName, 
            DateTime.Now, string.Format("自动回复，请求内容如下：\r\n{0}", requestMessage));
        response.Content += "\r\n<a href=\"http://xrwang.cnblogs.com\">博客园</a>";
        ErrorMessage errorMessage = CustomerService.SendMessage(new ResponseTextMessage(requestMessage.FromUserName, requestMessage.ToUserName, DateTime.Now, string.Format("自动回复客服消息，请求内容如下：\r\n{0}", requestMessage.ToString())));
        if (!errorMessage.IsSuccess)
            Message.Insert(new Message(MessageType.Exception, errorMessage.ToString()));
        return response;
    }

}