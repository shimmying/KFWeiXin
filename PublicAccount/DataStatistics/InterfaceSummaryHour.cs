﻿using Newtonsoft.Json.Linq;

namespace KFWeiXin.PublicAccount.DataStatistics
{
    /// <summary>
    /// 接口分析分时数据
    /// </summary>
    public class InterfaceSummaryHour : InterfaceSummary
    {
        /// <summary>
        /// 数据的小时
        /// </summary>
        public int ref_hour { get; private set; }

        /// <summary>
        /// 获取数据的小时（24小时制）
        /// </summary>
        public int Hour
        {
            get
            {
                return ref_hour / 100;
            }
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        public InterfaceSummaryHour()
        { }

        /// <summary>
        /// 从JObject对象解析统计数据
        /// </summary>
        /// <param name="jo"></param>
        public override void Parse(JObject jo)
        {
            base.Parse(jo);
            ref_hour = (int)jo["ref_hour"];
        }

        /// <summary>
        /// 返回字符串
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return string.Format("数据小时：{0}\r\n{1}", Hour, base.ToString());
        }
    }
}