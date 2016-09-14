using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Dynamic;
using Newtonsoft.Json;
using xrwang.weixin.PublicAccount;
using xrwang.weixin.PublicAccount.Semantic;

public partial class Example_Semantic : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            InitControls();
        }
    }

    /// <summary>
    /// 初始化控件，包括：公众号列表、服务类别
    /// </summary>
    private void InitControls()
    {
        foreach (AccountInfo account in AccountInfoCollection.AccountInfos)
            lbPublicAccount.Items.Add(new ListItem(account.Caption, account.UserName));
        foreach (long type in Enum.GetValues(typeof(ServiceTypeEnum)))
            cblType.Items.Add(new ListItem(((ServiceTypeEnum)type).ToString("g"), type.ToString()));
    }

    /// <summary>
    /// 请求语义理解结果
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnQuery_Click(object sender, EventArgs e)
    {
        string userName = lbPublicAccount.SelectedValue;
        if (rbJson.Checked)
        {
            string json = GetQueryJson();
            string url = "https://api.weixin.qq.com/semantic/semproxy/search?access_token={0}";
            ltrMessage.Text = string.Format("请求json字符串：{0}", json);
            txtResult.Text = HttpHelper.RequestResponseContent(url, userName, null, "Post", json);
        }
        else
        {
            string query = txtQuery.Text;
            long type = 0;
            foreach (ListItem item in cblType.Items)
            {
                if (item.Selected)
                    type += long.Parse(item.Value);
            }
            ServiceTypeEnum st = (ServiceTypeEnum)type;
            string city = txtCity.Text;
            string region = txtRegion.Text;
            string appid = txtAppid.Text;
            string uid = txtUid.Text;
            BaseReply reply = Semantic.Query(userName, query, st, city, region, appid, uid);
            if (reply != null)
            {
                ltrMessage.Text = "语义请求成功。";
                txtResult.Text = reply.ToString();
            }
            else
            {
                ltrMessage.Text = "语义请求失败。";
                txtResult.Text = "";
            }
        }
    }

    /// <summary>
    /// 得到请求JSON字符串
    /// </summary>
    /// <returns></returns>
    private string GetQueryJson()
    {
        string json = string.Empty;
        string query = txtQuery.Text;
        long type = 0;
        foreach (ListItem item in cblType.Items)
        {
            if (item.Selected)
                type += long.Parse(item.Value);
        }
        if (type == 0)
            return json;
        ServiceTypeEnum st = (ServiceTypeEnum)type;
        string city = txtCity.Text;
        string region = txtRegion.Text;
        string appid = txtAppid.Text;
        string uid = txtUid.Text;
        dynamic data = new ExpandoObject();
        data.query = query;
        data.city = city;
        if (!string.IsNullOrWhiteSpace(region))
            data.region = region;
        data.category = st.ToString("g");
        data.appid = appid;
        data.uid = uid;
        return JsonConvert.SerializeObject(data);
    }
}