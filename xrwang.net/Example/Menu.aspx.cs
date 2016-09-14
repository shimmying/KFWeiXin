using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using xrwang.weixin.PublicAccount;

public partial class Example_Menu : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            InitControls();
        }
    }

    /// <summary>
    /// 初始化控件，包括：公众号列表
    /// </summary>
    private void InitControls()
    {
        foreach (AccountInfo account in AccountInfoCollection.AccountInfos)
            lbPublicAccount.Items.Add(new ListItem(account.Caption, account.UserName));
    }

    /// <summary>
    /// 查询菜单
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnGet_Click(object sender, EventArgs e)
    {
        string userName = lbPublicAccount.SelectedValue;
        ErrorMessage errorMessage;
        BaseMenu[] menus = MenuHelper.Get(userName, out errorMessage);
        if (errorMessage.IsSuccess)
        {
            ltrMessage.Text = string.Format("查询菜单成功。根菜单数：{0}", menus != null ? menus.Length : 0);
            FillMenuInfo(menus);
        }
        else
        {
            txtMenu.Text = "";
            ltrMessage.Text = string.Format("查询菜单失败。{0}", errorMessage);
        }
    }

    /// <summary>
    /// 填充菜单信息
    /// </summary>
    /// <param name="menus">菜单数组</param>
    private void FillMenuInfo(BaseMenu[] menus)
    {
        StringBuilder sb = new StringBuilder();
        if (menus != null && menus.Length > 0)
        {
            foreach (BaseMenu menu in menus)
            {
                sb.AppendLine(menu.ToString());
                sb.AppendLine("---------------------------------------");
            }
        }
        txtMenu.Text = sb.ToString();
    }

    /// <summary>
    /// 创建菜单
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnCreate_Click(object sender, EventArgs e)
    {
        string userName = lbPublicAccount.SelectedValue;
        MenuContainer mc1 = MenuHelper.CreateContainer("酷型男");
        mc1.Add(MenuHelper.CreateItem(MenuTypeEnum.click, "关于", "about"));
        mc1.Add(MenuHelper.CreateItem(MenuTypeEnum.view, "博客", "http://xrwang.cnblogs.com"));
        mc1.Add(MenuHelper.CreateItem(MenuTypeEnum.location_select, "方位", "location"));
        mc1.Add(MenuHelper.CreateItem(MenuTypeEnum.view, "网页授权", OAuthAccessToken.GetOAuthUrl(userName, "http://www.xrwang.net/OAuth.aspx", OAuthScopeEnum.snsapi_userinfo, "oauth")));
        MenuContainer mc2 = MenuHelper.CreateContainer("扫描");
        mc2.Add(MenuHelper.CreateItem(MenuTypeEnum.scancode_push, "扫码推", "push"));
        mc2.Add(MenuHelper.CreateItem(MenuTypeEnum.scancode_waitmsg, "接收扫码", "waitmsg"));
        MenuContainer mc3 = MenuHelper.CreateContainer("发图");
        mc3.Add(MenuHelper.CreateItem(MenuTypeEnum.pic_sysphoto, "系统发图", "sysphoto"));
        mc3.Add(MenuHelper.CreateItem(MenuTypeEnum.pic_photo_or_album, "拍照或相册发图", "photo_or_album"));
        mc3.Add(MenuHelper.CreateItem(MenuTypeEnum.pic_weixin, "微信发图", "weixin"));
        ErrorMessage errorMessage = MenuHelper.Create(userName, new BaseMenu[] { mc1, mc2, mc3 });
        ltrMessage.Text = string.Format("创建菜单{0}。{1}",
            errorMessage.IsSuccess ? "成功" : "失败",
            errorMessage.IsSuccess ? "" : errorMessage.ToString());
    }

    /// <summary>
    /// 删除菜单
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnDelete_Click(object sender, EventArgs e)
    {
        string userName = lbPublicAccount.SelectedValue;
        ErrorMessage errorMessage = MenuHelper.Delete(userName);
        ltrMessage.Text = string.Format("删除菜单{0}。{1}",
            errorMessage.IsSuccess ? "成功" : "失败",
            errorMessage.IsSuccess ? "" : errorMessage.ToString());
    }

    /// <summary>
    /// 获取自定义菜单配置
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnGetSelfMenuInfo_Click(object sender, EventArgs e)
    {
        string userName = lbPublicAccount.SelectedValue;
        ErrorMessage errorMessage;
        bool isOpened;
        BaseMenu[] menus = MenuHelper.GetSelfMenuInfo(userName, out isOpened, out errorMessage);
        if (errorMessage.IsSuccess)
        {
            ltrMessage.Text = string.Format("查询菜单成功。菜单{0}开启。根菜单数：{1}", isOpened ? "已" : "未", menus != null ? menus.Length : 0);
            FillMenuInfo(menus);
        }
        else
        {
            txtMenu.Text = "";
            ltrMessage.Text = string.Format("查询菜单失败。{0}", errorMessage);
        }
    }
}