using System;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.Linq;
using System.Web;

namespace xrwang.net
{
    /// <summary>
    /// IpAddress：用于报告、获取客户端地址的类
    /// </summary>
    public static class IpAddress
    {
        /// <summary>
        /// 保存客户端地址的字典，键为客户端名称，值为地址及报告时间
        /// </summary>
        private static ConcurrentDictionary<string, Tuple<string, DateTime>> addresses;

        /// <summary>
        /// 报告客户端地址
        /// </summary>
        /// <param name="request">HttpRequest</param>
        /// <returns>返回报告是否成功</returns>
        public static bool Report(HttpRequest request)
        {
            if (request != null)
            {
                string address = request.UserHostAddress;
                string name = request.UserHostName;
                if (string.IsNullOrWhiteSpace(name))
                    name = address;
                if (addresses == null)
                    addresses = new ConcurrentDictionary<string, Tuple<string, DateTime>>();
                if (addresses.ContainsKey(name))
                {
                    Tuple<string, DateTime> value;
                    addresses.TryRemove(name, out value);
                }
                return addresses.TryAdd(name, new Tuple<string, DateTime>(address, DateTime.Now));
            }
            else
                return false;
        }

        /// <summary>
        /// 报告客户端地址
        /// </summary>
        /// <returns>返回报告是否成功</returns>
        public static bool Report()
        {
            HttpRequest request = null;
            if (HttpContext.Current != null && HttpContext.Current.Request != null)
                request = HttpContext.Current.Request;
            return Report(request);
        }

        /// <summary>
        /// 获取已保存的地址
        /// </summary>
        /// <returns></returns>
        public static ConcurrentDictionary<string, Tuple<string, DateTime>> Get()
        {
            return addresses;
        }

        /// <summary>
        /// 移除超过指定天数的地址
        /// </summary>
        /// <param name="days">天数</param>
        public static void RemoveOverdueAddress(double days=1)
        {
            if (addresses != null)
            {
                DateTime now = DateTime.Now;
                foreach (string key in addresses.Keys)
                {
                    Tuple<string, DateTime> value;
                    if (addresses.TryGetValue(key, out value) && (now - value.Item2).TotalDays > days)
                        addresses.TryRemove(key, out value);
                }
            }
        }
    }
}