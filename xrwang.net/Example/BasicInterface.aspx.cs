using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using xrwang.weixin.PublicAccount;

public partial class Example_BasicInterface : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            InitControls();
        }
    }

    /// <summary>
    /// 初始化控件，包括：公众号列表、多媒体类型列表
    /// </summary>
    private void InitControls()
    {
        foreach (AccountInfo account in AccountInfoCollection.AccountInfos)
            lbPublicAccount.Items.Add(new ListItem(account.Caption, account.UserName));
        foreach (string name in Enum.GetNames(typeof(MultiMediaTypeEnum)))
            lbMultiMediaType.Items.Add(name);
    }

    /// <summary>
    /// 获取许可令牌
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnGetAccessToken_Click(object sender, EventArgs e)
    {
        string userName = lbPublicAccount.SelectedValue;
        AccessToken token = AccessToken.Get(userName);
        if (token == null)
        {
            txtAccessToken.Text = "";
            ltrMessage.Text = "获取许可令牌失败。";
        }
        else
        {
            txtAccessToken.Text = token.access_token;
            ltrMessage.Text = "获取许可令牌成功。";
        }
    }

    /// <summary>
    /// 获取微信服务器地址
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnGetServerAddress_Click(object sender, EventArgs e)
    {
        ErrorMessage errorMessage;
        ServerAddresses addresses = ServerAddresses.Get(out errorMessage);
        if (errorMessage.IsSuccess)
        {
            StringBuilder sb = new StringBuilder();
            if (addresses.ip_list != null && addresses.ip_list.Length > 0)
            {
                foreach (string ip in addresses.ip_list)
                    sb.AppendFormat("{0},", ip);
            }
            txtServerAddress.Text = sb.ToString();
            ltrMessage.Text = "获取微信服务器地址成功。";
        }
        else
        {
            txtServerAddress.Text = "";
            ltrMessage.Text = string.Format("获取微信服务器地址失败。{0}", errorMessage);
        }
    }

    /// <summary>
    /// 上传多媒体文件
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnUpload_Click(object sender, EventArgs e)
    {
        string userName = lbPublicAccount.SelectedValue;
        MultiMediaTypeEnum type = (MultiMediaTypeEnum)Enum.Parse(typeof(MultiMediaTypeEnum), lbMultiMediaType.SelectedValue);
        string filename = fileUpload.FileName;
        byte[] bytes = fileUpload.FileBytes;
        ErrorMessage errorMessage;
        MultiMediaUploadResult result = MultiMediaHelper.Upload(userName, type, filename, bytes, out errorMessage);
        if (errorMessage.IsSuccess && result != null)
        {
            hlShowMultiMedia.NavigateUrl = MultiMediaHelper.GetDownloadUrl(AccessToken.Get(userName).access_token, result.MediaId);
            ltrMessage.Text = "上传多媒体文件成功。";
        }
        else
        {
            hlShowMultiMedia.NavigateUrl = string.Format("javascript:alert('上传多媒体文件失败。\r\n{0}');", errorMessage);
            ltrMessage.Text = string.Format("上传多媒体文件失败。\r\n{0}", errorMessage);
        }
    }

    /// <summary>
    /// 创建二维码
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnCreateQrCode_Click(object sender, EventArgs e)
    {
        string userName = lbPublicAccount.SelectedValue;
        string strSceneId = txtSceneId.Text;
        QrCode qrcode = null;
        ErrorMessage errorMessage;
        if (cbIsTemple.Checked)
        {
            int expireSeconds = int.Parse(txtExpireSeconds.Text);
            int sceneId;
            if (int.TryParse(strSceneId, out sceneId))
                qrcode = QrCode.Create(userName, expireSeconds, sceneId, out errorMessage);
            else
                errorMessage = new ErrorMessage(ErrorMessage.ExceptionCode, "场景值id必须为整数。");
        }
        else
        {
            int sceneId;
            if (int.TryParse(strSceneId, out sceneId))
                qrcode = QrCode.Create(userName, sceneId, out errorMessage);
            else
                qrcode = QrCode.Create(userName, strSceneId, out errorMessage);
        }
        if (errorMessage.IsSuccess && qrcode != null)
        {
            imgQrCode.ImageUrl = QrCode.GetUrl(qrcode.ticket);
            ltrMessage.Text = "创建二维码成功。";
        }
        else
        {
            imgQrCode.ImageUrl = "";
            ltrMessage.Text = string.Format("创建二维码失败。\r\n{0}", errorMessage);
        }
    }

    /// <summary>
    /// 获取短链接
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnGetShortUrl_Click(object sender, EventArgs e)
    {
        string userName = lbPublicAccount.SelectedValue;
        ErrorMessage errorMessage;
        string shortUrl = ShortUrl.Get(userName, txtLongUrl.Text, out errorMessage);
        if (errorMessage.IsSuccess && string.IsNullOrWhiteSpace(shortUrl))
        {
            txtShortUrl.Text = shortUrl;
            ltrMessage.Text = "获取短链接成功。";
        }
        else
        {
            txtShortUrl.Text = "";
            ltrMessage.Text = string.Format("获取短链接失败。{0}", errorMessage);
        }
    }
}