﻿using System;
using System.Net;
using KFWeiXin.PublicAccount;
using KFWeiXin.PublicAccount.CustomerService;
using KFWeiXin.PublicAccount.RequestMessage;
using KFWeiXin.PublicAccount.ResponseMessage;
using KFWeiXin.PublicLibrary;
using Utility = KFWeiXin.PublicAccount.Miscellaneous.Utility;

namespace KFWeiXinWeb
{
    public partial class Index : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string result = string.Empty;
            if (Validate())
            {
                if (Request.HttpMethod == WebRequestMethods.Http.Get)
                    result = HandleGet();
                else if (Request.HttpMethod == WebRequestMethods.Http.Post)
                    result = HandlePost();
                Message.Insert(new Message(MessageType.Response, "校验消息成功。\r\n返回值：" + result));
            }
            else
            {
                result = "校验消息失败。\r\n地址：" + Request.RawUrl;
                Message.Insert(new Message(MessageType.Exception, "校验消息失败。\r\n地址：" + Request.RawUrl));
            }
            Response.Write(result);
        }

        /// <summary>
        /// 验证消息的有效性
        /// </summary>
        /// <param name="context"></param>
        /// <returns>如果消息有效，返回true；否则返回false。</returns>
        private bool Validate()
        {
            string username = "gh_ee1453182f2c";  //在接口配置的URL中加入了username参数，表示哪个微信公众号
            AccountInfo account = AccountInfoCollection.GetAccountInfo(username);
            if (account == null)
                return false;
            string token = account.Token;
            string signature = RequestEx.TryGetQueryString("signature");
            string timestamp = RequestEx.TryGetQueryString("timestamp");
            string nonce = RequestEx.TryGetQueryString("nonce");
            if (string.IsNullOrWhiteSpace(signature) || string.IsNullOrWhiteSpace(timestamp) || string.IsNullOrWhiteSpace(nonce))
                return false;
            return Utility.CheckSignature(signature, token, timestamp, nonce);
        }

        /// <summary>
        /// 处理微信的GET请求，校验签名
        /// </summary>
        /// <returns>返回echostr</returns>
        private string HandleGet()
        {
            return RequestEx.TryGetQueryString("echostr");
        }

        /// <summary>
        /// 处理微信的POST请求
        /// </summary>
        /// <param name="context"></param>
        /// <returns>返回xml响应</returns>
        private string HandlePost()
        {
            RequestMessageHelper helper = new RequestMessageHelper(this.Request);
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
            response.Content += "\r\n科峰微信测试";
            ErrorMessage errorMessage = CustomerService.SendMessage(new ResponseTextMessage(requestMessage.FromUserName, requestMessage.ToUserName, DateTime.Now, string.Format("自动回复客服消息，请求内容如下：\r\n{0}", requestMessage.ToString())));
            if (!errorMessage.IsSuccess)
                Message.Insert(new Message(MessageType.Exception, errorMessage.ToString()));
            return response;
        }
    }
}