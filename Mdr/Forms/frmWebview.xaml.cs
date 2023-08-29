using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Microsoft.Web.WebView2.Core;

namespace Mdr.Forms
{
    /// <summary>
    /// Interaction logic for frmWebview.xaml
    /// </summary>
    /// 
 
    public partial class frmWebview : Window
    {
        bool ensure =false;
        public frmWebview()
        {
            InitializeComponent();
            webview.EnsureCoreWebView2Async();
        }
        private void Application_Startup(object sender, StartupEventArgs e)
        {


        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
          
        }

        private void webview_CoreWebView2InitializationCompleted(object sender, CoreWebView2InitializationCompletedEventArgs e)
        {
            ensure = true;
            openURL();
        }
        void openURL() {
            string user_login ="";
            string strhn="";
            if ( ensure )
            {
                webview.CoreWebView2.Navigate("http://203.114.123.212/opdcard2020/main_dev.php?user=" + user_login + "&hn=" + strhn);
            }
        }
    }
}
