﻿using KFWeiXin.PublicAccount.Miscellaneous;
using Newtonsoft.Json.Linq;

namespace KFWeiXin.PublicAccount.Semantic.Reply
{
    /// <summary>
    /// 火车语义应答
    /// </summary>
    public class TrainReply:BaseReply
    {
        /// <summary>
        /// 火车语义
        /// </summary>
        public TrainSemantic semantic { get; private set; }

        /// <summary>
        /// 从JObject对象解析
        /// </summary>
        /// <param name="jo"></param>
        public override void Parse(JObject jo)
        {
            base.Parse(jo);
            semantic = Utility.Parse<TrainSemantic>((JObject)jo["semantic"]);
        }

        /// <summary>
        /// 返回字符串
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return string.Format("{0}\r\n语义应答：{1}",
                base.ToString(), semantic);
        }
    }
}
