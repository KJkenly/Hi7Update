using Microsoft.Web.WebView2.Core;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing.Printing;
using System.IO;
using System.Linq;
using System.Security.Policy;
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

namespace Mdr.Forms
{
    /// <summary>
    /// Interaction logic for frmprtcardq.xaml
    /// </summary>
    public partial class frmprtcardq : Window
    {
        bool ensure = false;
        public string printcount = "1";
        public string url = Hi7.Class.APIConnect.IP_PRINTQUEUE;
        public string strhn, strvn, strhospname, strnamepttype, strclinic, strdateServ, strtitle, strfirstName, strlastName, strage, strQueueNumber, strQueueID, strSQueueNumber, strClimeCode;
 
        public frmprtcardq()
        {
            InitializeComponent();
            InitializeBrowser();

             webview_showprint.EnsureCoreWebView2Async();
        }
        private async Task InitializeBrowser()
        {
            var userDataFolder = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\Hi7Setup";

            CoreWebView2EnvironmentOptions opts = new CoreWebView2EnvironmentOptions() { AdditionalBrowserArguments = "--kiosk-printing" };
            var env = await CoreWebView2Environment.CreateAsync(null, userDataFolder, opts);
            await webview_showprint.EnsureCoreWebView2Async(env);
            this.Close();
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
            printQueue();
            //this.Close();
            //openURL();
        }
        //print q4u
        void printQueue()
        {
         
                 strhn = HI7.Class.HIUility._HN;
                 strvn = HI7.Class.HIUility._VN;
                 strhospname = HI7.Class.HIUility._hi_hsp_nm;
                 strnamepttype = '[' + HI7.Class.HIUility._PTTYPE + ']' + HI7.Class.HIUility.c2n_pttype(HI7.Class.HIUility._PTTYPE);
                 strclinic = HI7.Class.HIUility.c2n_cln(HI7.Class.HIUility._CLN);
                 strdateServ = HI7.Class.HIUility._DateServQ4U;
                 strtitle = HI7.Class.HIUility._PnameQ4U;
                 strfirstName = HI7.Class.HIUility._FnameQ4U;
                 strlastName = HI7.Class.HIUility._LnameQ4U;
                 strage = HI7.Class.HIUility.Hn2AgeYY(HI7.Class.HIUility._HN);
                 strQueueNumber = HI7.Class.HIUility._QueueNumber;
                 strQueueID = HI7.Class.HIUility._QueueID;
                 strSQueueNumber = HI7.Class.HIUility._StrQueueNumber;
                 strClimeCode = HI7.Class.HIUility._claimCode;
           
            try
            {
                //string PostDataString = "nametype=queue&hn=" + strhn + "&vn=" + strvn + "&hospcode= " + strhospname + "&nameptype=" +
                //   strnamepttype + "&clinic=" + strclinic + "&title=" + strtitle + "&fname=" + strfirstName + "&lname=" + strlastName + "&age=" + strage +
                //   "&Queuenumber=" + strQueueNumber + "&Queueid=" + strQueueID + "&strQueuenumber=" + strSQueueNumber + "&dateServ=" + strdateServ + "&cliamcode=" + strClimeCode + "&qtyprtq=" + printcount;

                string PostDataString = "nametype=queue&hn=" + strhn + "&vn=" + strvn + "&hospcode= " + strhospname + "&nameptype=" +
                    strnamepttype + "&clinic=" + strclinic + "&title=" + strtitle + "&fname=" + strfirstName + "&lname=" + strlastName + "&age=" + strage +
                    "&Queuenumber=" + strQueueNumber + "&Queueid=" + strQueueID + "&strQueuenumber=" + strSQueueNumber + "&dateServ=" + strdateServ + "&cliamcode=" + strClimeCode;
                  
                    UTF8Encoding utfEncoding = new UTF8Encoding();
                    byte[] postData = utfEncoding.GetBytes(PostDataString);

                    MemoryStream postDataStream = new MemoryStream(PostDataString.Length);
                    postDataStream.Write(postData, 0, postData.Length);
                    postDataStream.Seek(0, SeekOrigin.Begin);
                    var request = webview_showprint.CoreWebView2.Environment.CreateWebResourceRequest(url,
                    "POST", postDataStream, "Content-Type: application/x-www-form-urlencoded");
                    webview_showprint.CoreWebView2.NavigateWithWebResourceRequest(request);
               
                

                //string postDataString = "nametype=queue&hn=1234";
               
               
                //PrinterSettings settings = new PrinterSettings();
                //string printerName = settings.PrinterName;
                //settings.Copies = 2;

                //settings.PrinterName = printerName;
                
               
                



                //CoreWebView2PrintSettings printSettings = GetSelectedPrinterPrintSettings(printerName);

            }
            catch (Exception ex)
            {
                MessageBox.Show("printQueue: " +url + " | "+ex.Message);

            }
        }
       
        //CoreWebView2PrintSettings GetSelectedPrinterPrintSettings(string printerName)
        //{
        //    CoreWebView2PrintSettings printSettings =this.webview_showprint.CoreWebView2.Environment.CreatePrintSettings();
           
        //    printSettings.ShouldPrintBackgrounds = false;
        //    printSettings.ShouldPrintHeaderAndFooter = false;
            
        //    return printSettings;

        //    // or
        //    //
        //    // Get PrintQueue for the selected printer and use GetPrintCapabilities() of PrintQueue from System.Printing
        //    // to get the capabilities of the selected printer.
        //    // Display the printer capabilities to the user along with the page settings.
        //    // Return the user selected settings.
        //}
        void getSETUP()
        {
           

            //Console.WriteLine(settings.PrinterName);
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
                        printcount = dr["qtyprtq"].ToString();

                    }
                    else
                    {
                        HI7.Class.HIUility._HCODE = "";
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


        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Hi7.Class.APIConnect.getConfgXML();
            //Hi7.Class.APIConnect.API_SERVER = "http://203.114.123.210:30000/";
            //ขอรหัส Token
            //getToken();
            //ดึงข้อมูลในแฟ้ม SETUP และเลือกใช้ค่า _DGR
            getSETUP();
        }

        private void webview_showprint_NavigationCompleted(object sender, CoreWebView2NavigationCompletedEventArgs e)
        {

        }
    }
}
