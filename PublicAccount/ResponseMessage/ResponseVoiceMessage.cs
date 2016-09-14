﻿using System;
using System.Xml;
using Newtonsoft.Json;

namespace KFWeiXin.PublicAccount.ResponseMessage
{
    /// <summary>
    /// 响应语音消息
    /// </summary>
    public class ResponseVoiceMessage : ResponseBaseMessage
    {
        private string mediaId;
        /// <summary>
        /// 获取或设置媒体ID
        /// </summary>
        public string MediaId
        {
            get
            {
                return mediaId;
            }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentNullException("MediaId", "Media为空。");
                mediaId = value;
            }
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="toUserName">接收方账号</param>
        /// <param name="fromUserName">开发者微信号</param>
        /// <param name="createTime">消息创建时间</param>
        /// <param name="mediaId">媒体ID</param>
        public ResponseVoiceMessage(string toUserName, string fromUserName, DateTime createTime,
            string mediaId)
            : base(toUserName, fromUserName, createTime, ResponseMessageTypeEnum.voice)
        {
            MediaId = mediaId;
        }

        /// <summary>
        /// 返回消息字符串
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return string.Format("{0}\r\n媒体ID：{1}",
                base.ToString(), MediaId);
        }

        /// <summary>
        /// 返回XML格式的响应消息
        /// </summary>
        /// <returns>返回响应消息</returns>
        protected override string ToXml()
        {
            XmlDocument doc = CreateXmlDocument();
            XmlElement root = doc.DocumentElement;
            XmlElement voice = CreateXmlElement(doc, "Voice");
            voice.AppendChild(CreateXmlElement(doc, "MediaId", MediaId));
            root.AppendChild(voice);
            return doc.InnerXml;
        }

        /// <summary>
        /// 返回JSON格式的客服消息
        /// </summary>
        /// <returns></returns>
        public override string ToJson()
        {
            var customerService = new
            {
                touser = ToUserName,
                msgtype = MsgType.ToString("g"),
                voice = new
                {
                    media_id = MediaId
                }
            };
            return JsonConvert.SerializeObject(customerService);
        }
    }
}