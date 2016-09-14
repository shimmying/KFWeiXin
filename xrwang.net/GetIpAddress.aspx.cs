using System;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using xrwang.net;

public partial class GetIpAddress : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        IpAddress.RemoveOverdueAddress();
        ConcurrentDictionary<string, Tuple<string, DateTime>> addresses = IpAddress.Get();
        StringBuilder sbAddress = new StringBuilder();
        if (addresses != null && addresses.Count > 0)
        {
            sbAddress.AppendFormat("已保存{0}台客户端的地址：", addresses.Count);
            int i = 0;
            foreach (string key in addresses.Keys)
            {
                Tuple<string, DateTime> value;
                if (addresses.TryGetValue(key, out value))
                {
                    i++;
                    sbAddress.AppendFormat("<br />({0}){1},{2},{3}", i, key, value.Item1, value.Item2);
                }
            }
        }
        else
            sbAddress.Append("没有已报告的客户端地址。");
        ltrAddress.Text = sbAddress.ToString();
    }
}