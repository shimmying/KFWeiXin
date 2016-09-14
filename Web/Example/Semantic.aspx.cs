using System;
using System.Dynamic;
using System.Web.UI;
using System.Web.UI.WebControls;
using KFWeiXin.PublicAccount;
using KFWeiXin.PublicAccount.Miscellaneous;
using KFWeiXin.PublicAccount.Semantic;
using Newtonsoft.Json;

namespace KFWeiXinWeb.Example
{
    public partial class Semantic : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                InitControls();
            }
        }

        /// <summary>
        ///     初始化控件，包括：公众号列表、服务类别
        /// </summary>
        private void InitControls()
        {
            foreach (var account in AccountInfoCollection.AccountInfos)
                lbPublicAccount.Items.Add(new ListItem(account.Caption, account.UserName));
            foreach (long type in Enum.GetValues(typeof (ServiceTypeEnum)))
                cblType.Items.Add(new ListItem(((ServiceTypeEnum) type).ToString("g"), type.ToString()));
        }

        /// <summary>
        ///     请求语义理解结果
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnQuery_Click(object sender, EventArgs e)
        {
            var userName = lbPublicAccount.SelectedValue;
            if (rbJson.Checked)
            {
                var json = GetQueryJson();
                var url = "https://api.weixin.qq.com/semantic/semproxy/search?access_token={0}";
                ltrMessage.Text = string.Format("请求json字符串：{0}", json);
                txtResult.Text = HttpHelper.RequestResponseContent(url, userName, null, "Post", json);
            }
            else
            {
                var query = txtQuery.Text;
                long type = 0;
                foreach (ListItem item in cblType.Items)
                {
                    if (item.Selected)
                        type += long.Parse(item.Value);
                }
                var st = (ServiceTypeEnum) type;
                var city = txtCity.Text;
                var region = txtRegion.Text;
                var appid = txtAppid.Text;
                var uid = txtUid.Text;
                var reply = KFWeiXin.PublicAccount.Semantic.Semantic.Query(userName, query, st, city, region, appid,
                    uid);
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
        ///     得到请求JSON字符串
        /// </summary>
        /// <returns></returns>
        private string GetQueryJson()
        {
            var json = string.Empty;
            var query = txtQuery.Text;
            long type = 0;
            foreach (ListItem item in cblType.Items)
            {
                if (item.Selected)
                    type += long.Parse(item.Value);
            }
            if (type == 0)
                return json;
            var st = (ServiceTypeEnum) type;
            var city = txtCity.Text;
            var region = txtRegion.Text;
            var appid = txtAppid.Text;
            var uid = txtUid.Text;
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
}