using HandyControl.Controls;
using Microsoft.Web.WebView2.Core;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Threading;
using Window = System.Windows.Window;

namespace Mdr.Forms
{
    /// <summary>
    /// Interaction logic for frmAuthenservice.xaml
    /// </summary>
    public partial class frmAuthenservice : Window
    {
        string API_LOCALHOST = "http://localhost:8189/";
        public frmAuthenservice()
        {
            InitializeComponent();
                   
        }

        //private void ClickClose(object sender, RoutedEventArgs e)
        //{
        //    this.Close();
        //}
        void BitmapToBitmapImageGetpt()
        {
            try
            {
                BitmapImage bitmapImage = new BitmapImage();
                bitmapImage.BeginInit();
                bitmapImage.CacheOption = BitmapCacheOption.OnLoad;

                if (txtpt_cid.Text == null || txtpt_cid.Text == "")
                {
                    bitmapImage.UriSource = new Uri(@"C://PT_IMG//" + "0000000000000" + ".jpg", UriKind.RelativeOrAbsolute);
                }
                else
                {
                    bitmapImage.UriSource = new Uri(@"C://PT_IMG//" + txtpt_cid.Text + ".jpg", UriKind.RelativeOrAbsolute);
                }

                bitmapImage.EndInit();
                bitmapImage.Freeze();
                this.imgPerson.Source = bitmapImage; // <== //display image
            }
            catch (Exception ex)
            {
                BitmapImage bitmapImage = new BitmapImage();
                bitmapImage.BeginInit();
                bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
                bitmapImage.UriSource = new Uri(@"C://PT_IMG//" + "0000000000000" + ".jpg", UriKind.RelativeOrAbsolute);
                bitmapImage.EndInit();
                bitmapImage.Freeze();
                this.imgPerson.Source = bitmapImage;
            }

        }
        void GetdatePerson() {
            txtpt_cid.Text = Mdr.Forms.frmMdr.CIDNHS;            
            getPhone();
        }
        void getPhone() {
            DataTable dt;
            DataRow dr;
            dt = HI7.Class.HIUility.getHn(Mdr.Forms.frmMdr.CIDNHS);
            if (dt != null)//ถ้ามีข้อมูล
            {
                if (dt.Rows.Count == 1)//ถ้ามีข้อมูล 1 แถว
                {
                    dr = dt.Rows[0];
                    txtpt_fullname.Text = dr["fullname"].ToString();
                    txtpt_phone.Text  = dr["hometel"].ToString();
                }
                else {
                    Growl.WarningGlobal("ไม่พบประวัติการรักษาที่โรงพยาบาล");
                    this.Close();
                }
            }
            else
            {

            }
        }
        void GetValue()
        {
            try
            {
                DataTable GridAuthen = new System.Data.DataTable();
                var client = new RestClient("http://localhost:8189/api/nhso-service/latest-5-authen-code-all-hospital/" + Mdr.Forms.frmMdr.CIDNHS);
                client.Timeout = -1;
                var request = new RestRequest(Method.GET);     
                IRestResponse response = client.Execute(request);
                string data = response.Content;
                GridAuthen = JsonConvert.DeserializeObject<DataTable>(data.ToString());
                var posts = JsonConvert.DeserializeObject<List<Post>>(data);
                DataView view = new DataView(GridAuthen);
                System.Data.DataTable selected = view.ToTable("Selected", false);
                this.dataGridAuthen.ItemsSource = selected.DefaultView;
            }
            catch
            {
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Hi7.Class.APIConnect.getConfgXML();
            try { GetdatePerson(); } catch { }
            try { GetValue(); } catch { }
            try { BitmapToBitmapImageGetpt(); } catch { }
        }
        DispatcherTimer dispatcherTimer;
        TimeSpan time;
        private void Ellipse_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                Storyboard s = (Storyboard)TryFindResource("StoryboardClose");
                s.Begin();

                time = TimeSpan.FromSeconds(1);
                dispatcherTimer = new DispatcherTimer();
                dispatcherTimer.Interval = TimeSpan.FromSeconds(1);
                dispatcherTimer.Tick += DispatcherTimer_Tick;
                dispatcherTimer.Start();
                //this.Close();
            }
            catch { this.Close(); }
        }
        
        private void Label_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            try {
                Storyboard s = (Storyboard)TryFindResource("StoryboardClose");
                s.Begin();
                
                time = TimeSpan.FromSeconds(1);
                dispatcherTimer = new DispatcherTimer();
                dispatcherTimer.Interval = TimeSpan.FromSeconds(1);
                dispatcherTimer.Tick += DispatcherTimer_Tick;
                dispatcherTimer.Start();
                //this.Close();
            }
            catch { this.Close(); }
            
        }
        private void DispatcherTimer_Tick(object sender, EventArgs e)
        {
            if (time == TimeSpan.Zero) {
                dispatcherTimer.Stop();
                this.Close();
            } 
            else
            {
                time = time.Add(TimeSpan.FromSeconds(-1));
                //MyTime.Text = time.ToString("c");
            }
        }

        private void dataGridAuthen_LoadingRow(object sender, DataGridRowEventArgs e)
        {
            e.Row.Header = (e.Row.GetIndex() + 1).ToString();
        }
    }
    public class Post
    {
        public string claimType { get; set; }
        public string claimCode { get; set; }
        public string hcode { get; set; }
        public DateTime claimDateTime { get; set; }
    }

    }
