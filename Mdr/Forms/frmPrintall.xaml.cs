using HandyControl.Controls;
using Newtonsoft.Json.Linq;
using System;
//using System.Windows;
using System.Diagnostics;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Microsoft.Web.WebView2.Core;
namespace Mdr.Forms
{
    /// <summary>
    /// Interaction logic for frmPrintall.xaml
    /// </summary>
    public partial class frmPrintall : System.Windows.Window
    {
        public bool ensure = false;
        public static string strstaff;
        public frmPrintall()
        {
            InitializeComponent();
            webview_showprint.EnsureCoreWebView2Async();
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Hi7.Class.APIConnect.getConfgXML();
            strstaff = Hi7.Class.APIConnect.USER_IDLOGIN;
            getSETUP();
            Keyboard.Focus(this.txthn);
            lblhn.Content = "";
            lblfullname.Content = "";

        }
        private void ClickClose(object sender, RoutedEventArgs e)
        {
          
            this.Close();
        }

        private void ClickMin(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        private void webview_showprint_CoreWebView2InitializationCompleted(object sender, Microsoft.Web.WebView2.Core.CoreWebView2InitializationCompletedEventArgs e)
        {
            ensure = true;
        }

        void openURL() {

            try {
                string user_login = Hi7.Class.APIConnect.USER_IDLOGIN;
                string strvn = HI7.Class.HIUility._VN;
                string strHcode = HI7.Class.HIUility._HCODE;
                string serverprint = Hi7.Class.APIConnect.IPSERVERPTINT;
                if (ensure == true)
                {
                    if (webview_showprint != null && webview_showprint.CoreWebView2 != null)
                    {
                        if (rd_Admission.IsChecked == true)//Admission
                        {
                            webview_showprint.CoreWebView2.Navigate(serverprint + "hidoc/forms/admission.php?hcode=" + strHcode + "&vn=" + strvn + "&uid=" + user_login + "&print=1");//print web
                        }
                        else if (rd_Allipd.IsChecked == true)//admit_all
                        {
                            webview_showprint.CoreWebView2.Navigate(serverprint + "hidoc/forms/admit_all.php?hcode=" + strHcode + "&vn=" + strvn + "&uid=" + user_login + "&print=1");//print web
                        }
                        else if (rd_OPDCARD.IsChecked == true)//OPDCARD
                        {
                            webview_showprint.CoreWebView2.Navigate(serverprint + "hidoc/forms/opdcard.php?hcode=" + strHcode + "&vn=" + strvn + "&uid=" + user_login + "&print=1");//print web
                        }
                        else if (rd_Summary.IsChecked == true)//Summary
                        {
                            webview_showprint.CoreWebView2.Navigate(serverprint + "hidoc/forms/summary.php?hcode=" + strHcode + "&vn=" + strvn + "&uid=" + user_login + "&print=1");//print web
                        }
                        else if (rd_Historyopd.IsChecked == true)//Historyopd
                        {
                            webview_showprint.CoreWebView2.Navigate(serverprint + "hidoc/forms/prtpopd.php?hcode=" + strHcode + "&vn=" + strvn + "&uid=" + user_login + "&print=1");//print web
                        }
                        else
                        {
                            Growl.Warning("กรุณนาเลือกหัวข้อที่ต้องการปริ้น");
                        }

                    }
                    //if (rd_Admission.IsChecked == true)//Admission
                    //{
                    //    webview_showprint.CoreWebView2.Navigate(serverprint + "hidoc/forms/admission.php?hcode=" + strHcode + "&vn=" + strvn + "&uid=" + user_login + "&print=1");//print web
                    //}
                    //else if (rd_Allipd.IsChecked == true)//admit_all
                    //{
                    //    webview_showprint.CoreWebView2.Navigate(serverprint + "hidoc/forms/admit_all.php?hcode=" + strHcode + "&vn=" + strvn + "&uid=" + user_login + "&print=1");//print web
                    //}
                    //else if (rd_OPDCARD.IsChecked == true)//OPDCARD
                    //{
                    //    webview_showprint.CoreWebView2.Navigate(serverprint + "hidoc/forms/opdcard.php?hcode=" + strHcode + "&vn=" + strvn + "&uid=" + user_login + "&print=1");//print web
                    //}
                    //else if (rd_Summary.IsChecked == true)//Summary
                    //{
                    //    webview_showprint.CoreWebView2.Navigate(serverprint + "hidoc/forms/summary.php?hcode=" + strHcode + "&vn=" + strvn + "&uid=" + user_login + "&print=1");//print web
                    //}
                    //else if (rd_Historyopd.IsChecked == true)//Historyopd
                    //{
                    //    webview_showprint.CoreWebView2.Navigate(serverprint + "hidoc/forms/prtpopd.php?hcode=" + strHcode + "&vn=" + strvn + "&uid=" + user_login + "&print=1");//print web
                    //}
                    //else
                    //{
                    //    Growl.Warning("กรุณนาเลือกหัวข้อที่ต้องการปริ้น");
                    //}
                }

            }
            catch(Exception ex) {
                Growl.Warning("openURL" + ex);
            } 
            
        }
        void getSETUP()
        {
            DataRow dr;
            DataTable dt = new System.Data.DataTable();
            try
            {
                dt = HI7.Class.HIUility.getSetup();
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        dr = dt.Rows[0];
                        //string strname = dr["sign"].ToString();
                        string strHcode = (string)HI7.Class.HIUility.IsNullString(dr["_drg"]);
                        HI7.Class.HIUility._HCODE = strHcode;

                    }
                    else
                    {
                        HI7.Class.HIUility._HCODE =  "";
                    }
                }
                else
                {
                    HI7.Class.HIUility._HCODE = "";
                }

            }
            catch (Exception ex)
            {

            }
        }
        void getodata(string hn)
        {
            string strHN = hn;
            string strSql = "";
            if (selectAN.IsChecked == true)
            {
                 strSql = "select o.an,o.vn,o.hn,o.pttype,s.namepttype,o.cln,c.namecln,date_format(o.vstdttm,'%Y-%m-%d') as vstdate,date_format(o.vstdttm,'%H:%i') as vsttime " +
                " from ovst as o inner join cln as c on o.cln = c.cln  inner join pttype as s on o.pttype = s.pttype where o.hn = " + strHN + " AND o.an != '0'"+" order by o.vn desc ";
            }else if(selectAN.IsChecked == false)
            {
                 strSql = "select o.an,o.vn,o.hn,o.pttype,s.namepttype,o.cln,c.namecln,date_format(o.vstdttm,'%Y-%m-%d') as vstdate,date_format(o.vstdttm,'%H:%i') as vsttime " +
                " from ovst as o inner join cln as c on o.cln = c.cln  inner join pttype as s on o.pttype = s.pttype where o.hn = " + strHN + " order by o.vn desc ";
            }           

            DataTable dt = new System.Data.DataTable();
            Dictionary<string, object> dictData = new Dictionary<string, object>();
            dictData.Add("query", strSql);

            try
            {
                dt = Hi7.Class.APIConnect.getDataTable("query/hi7_query", "POST", dictData);
                if (dt != null)
                {
                    this.gridprintall.ItemsSource = dt.DefaultView;
                }
            }
            catch (Exception ex)
            {

            }
        }
        private void btnfind_Click(object sender, RoutedEventArgs e)
        {
            HI7.Class.HIUility._HN = this.txthn.Text;
            this.lblhn.Content = this.txthn.Text;

            this.lblfullname.Content = HI7.Class.HIUility.gethn2name(HI7.Class.HIUility._HN);
            
            getodata(HI7.Class.HIUility._HN);
        }

        private void gridprintall_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            try
            {
                DataRowView dataRow = (DataRowView)gridprintall.SelectedItem;
                string cellValue = dataRow.Row.ItemArray[1].ToString();
                if (cellValue != "")
                {
                    string user_login = Hi7.Class.APIConnect.USER_IDLOGIN;
                    string strvn = HI7.Class.HIUility._VN;
                    string strHcode = HI7.Class.HIUility._HCODE;
                    string serverprint = Hi7.Class.APIConnect.IPSERVERPTINT;

                    //webview_showprint.CoreWebView2.Navigate(serverprint + "hidoc/forms/admission.php?hcode=" + strHcode + "&vn=" + strvn + "&uid=" + user_login + "&print=1");//print web

                    HI7.Class.HIUility._VN = cellValue;
                    //string url = serverprint + "hidoc/forms/opdcard.php?hcode=" + strHcode + "&vn=" + strvn + "&uid=" + user_login + "&print=1";
                    //Process.Start(new ProcessStartInfo("cmd", $"/c start {url}") { CreateNoWindow = true });
                    //System.Diagnostics.Process.Start(serverprint+ "hidoc/forms/admission.php?hcode=" + strHcode + "&vn=" + strvn + "&uid=" + user_login + "&print=1");
                    ensure = true;
                    openURL();
                }
            }
            catch(Exception ex)
            {
                Growl.Warning("Error gridprintall" + ex.Message);
            }
           

        }

        private void txthn_KeyUp(object sender, KeyEventArgs e)
        {
            if(e.Key == Key.Enter)
            {
                HI7.Class.HIUility._HN = this.txthn.Text;
                this.lblhn.Content = this.txthn.Text;
                this.lblfullname.Content = HI7.Class.HIUility.gethn2name(HI7.Class.HIUility._HN);               
                getodata(HI7.Class.HIUility._HN);
            }

        }

        private void gridprintall_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                DataRowView dataRow = (DataRowView)gridprintall.SelectedItem;
                string cellValue = dataRow.Row.ItemArray[1].ToString();
                if (cellValue != "")
                {
                    HI7.Class.HIUility._VN = cellValue;
                    ensure = true;
                    openURL();
                }
            }
        }
    }

}
