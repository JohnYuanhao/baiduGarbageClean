using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace baiduGarbageClean
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        private static void WebBrowserVersionEmulation()
        {
            const string BROWSER_EMULATION_KEY =
            @"Software\Microsoft\Internet Explorer\Main\FeatureControl\FEATURE_BROWSER_EMULATION";
            //
            // app.exe and app.vshost.exe
            String appname = System.Diagnostics.Process.GetCurrentProcess().ProcessName + ".exe";
            //
            // Webpages are displayed in IE9 Standards mode, regardless of the !DOCTYPE directive.
            const int browserEmulationMode = 9999;

            Microsoft.Win32.RegistryKey browserEmulationKey =
                Microsoft.Win32.Registry.CurrentUser.OpenSubKey(BROWSER_EMULATION_KEY, Microsoft.Win32.RegistryKeyPermissionCheck.ReadWriteSubTree) ??
                Microsoft.Win32.Registry.CurrentUser.CreateSubKey(BROWSER_EMULATION_KEY);

            if (browserEmulationKey != null)
            {
                browserEmulationKey.SetValue(appname, browserEmulationMode, Microsoft.Win32.RegistryValueKind.DWord);
                browserEmulationKey.Close();
            }
        }
        private void webBrowser1_NewWindow(object sender, CancelEventArgs e)
        {
            WebBrowserVersionEmulation();
        }

        private void webBrowser1_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
                HtmlElement script = webBrowser1.Document.CreateElement("script");
                script.SetAttribute("type", "text/javascript");
                string javascript = "var taskid,progress;function aa(){alert('as');return 's'}$(function(){try{addCss();$('.user-name').attr('id','userName');if(location.href.indexOf('duplicate')!=-1){}else{var login=setInterval(function(){var l=$('#TANGRAM__PSP_4__footerULoginBtn');if(l.length>0){l[0].click();clearInterval(login)}},300)}}catch(e){}$.ajaxSetup({complete:function(a,b){try{if(this.url.indexOf('garbagetaskquery')!=-1){progress=a.responseJSON.progress;taskid=getParameters(this.url)['taskid']}}catch(e){}}})});function returnIM(){return progress+','+taskid}function getParameters(url){var url=url==undefined?location.href:url.href;var arr=url.match(/([^?=&]+)(=([^&]*))/g);if(arr!=null){return arr.reduce(function(a,v){return a[v.slice(0,v.indexOf('='))]=decodeURI(v.slice(v.indexOf('=')+1)),a},{})}return{}}function addCss(){var acss='.login-main{width:auto !important;top:0 !important;}#login-container{min-width:auto !important;}#login-header{display:none !important;}.header-login{border-radius:0 !important;position:absolute !important;top:0 !important;left:0 !important;right:0 !important;bottom:0 !important;margin:0 auto !important;width:352px !important;height:398px !important}#login-middle{height:auto !important}';if(document.all){window.style=acss;document.createStyleSheet('javascript:style')}else{var style=document.createElement('style');style.type='text/css';style.innerHTML=acss;document.getElementsByTagName('HEAD').item(0).appendChild(style)}}";
                script.SetAttribute("text", javascript);
                webBrowser1.Document.Body.AppendChild(script);
                webBrowser1.Visible = true;
            try
            {
                userName.Text = webBrowser1.Document.GetElementById("userName").InnerText;
            }
            catch (Exception)
            {
            }
            //userName.Text = webBrowser1.Document.InvokeScript("aa",null).ToString();
            //foreach (object value in (IEnumerable)values)
            //    MessageBox.Show(value.ToString());

        }

        private void webBrowser1_Navigating(object sender, WebBrowserNavigatingEventArgs e)
        {
            //webBrowser1.Visible = false;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            //https://pan.baidu.com/api/garbagetaskquery?type=dup&option=scan&taskid=1116918574628315
            //https://pan.baidu.com/api/garbagelist?type=dup&start=0&limit=999999999&taskid=717563683100489 
            //string str = webBrowser1.Document.InvokeScript("returnIM").ToString();
        }
    }
}
