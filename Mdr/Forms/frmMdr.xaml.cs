using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
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
using System.Xml;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using ThaiNationalIDCard;
using Image = System.Drawing.Image;
using HandyControl.Controls;
using MessageBox = System.Windows.MessageBox;
using System.Threading;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows.Threading;
using Formatting = Newtonsoft.Json.Formatting;
using RestSharp;
using ComboBox = HandyControl.Controls.ComboBox;
using System.Text.RegularExpressions;
using System.ComponentModel;
using System.Windows.Markup;
using System.Windows.Navigation;
using System.Net.Http;
using System.Reflection.Metadata;
using Mdr.UserControl;
using HandyControl.Tools.Extension;
using FontFamily = System.Windows.Media.FontFamily;
using MaterialDesignThemes.Wpf;
using MaterialDesignColors.ColorManipulation;
using System.Reflection;
using System.Xml.Linq;
using Path = System.IO.Path;
using Hi7.UsercontrolHi7;
using Microsoft.VisualBasic;
using Guna.UI2.WinForms.Enums;
using FontAwesome.WPF;
using System.Collections.Immutable;
using System.Security.Cryptography;
using System.Runtime.InteropServices;
using Excel = Microsoft.Office.Interop.Excel;
using OfficeOpenXml;
using OfficeOpenXml.Table;

namespace Mdr.Forms
{
    /// <summary>
    /// Interaction logic for frmMdr.xaml
    /// </summary>
    public partial class frmMdr : System.Windows.Window
    {
        
        // "username":"Phuwanartanugool@gmail.com","password":"Aofpor10

        string SMARTHEALTH_API_TOKEN = "https://smarthealth.service.moph.go.th/phps/public/api/v3/gettoken";
        string SMARTHEALTH_API_NSHODATA = "https://smarthealth.service.moph.go.th/phps/api/nhsodata/v1/search_by_pid";
        string SMARTHEALTH_USER = "Phuwanartanugool@gmail.com";
        string SMARTHEALTH_PASSWORD = "Aofpor1030";
        string SMARTHEALTH_TOKEN = "";
        //SMC ตัวแปรเช็คสิทธิ์กรณีไม่มีบัตร
        public static string SMCBIRTHDAY = "", wsStatus = "", SMCCardID="", SMCEHmaincode="", SMCEHsubcode = "", SMCStartdate="", SMCExpdate="";
        //userid ล็อคอิน
        public static string strstaff = "";
        //HN จากลงทะเบียนใหม่
        public static string HN = "",VN = "";
        //Search 
        public static string txtClinic = "", dataClinic = "", idClinic = "", txtPttype = "", dataPttype = "", idPttype = "", searchhn = "", typeCheckpttype = "";
        //อ่านบัตรครั้งเดียวถ้ามีเก็บไว้ไม่ต้องไปอ่านอีก
        public static Personal personalCard;
        public static string _ClickAppointsearchhn, _HNdouble, _CIDREADER, _IDSCREEN, _CLN, _PRYORITY, _BIRTHDATEPT, _DTHDATE, _VNGETDATAGRID, _HNGETDATAGRID, _PTTYPEDATAGRID, _FullnameDATAGRID;
        //ประกาศตัวแปรให้มองเห็นทุกฟังก์ชั่นนำไปใช้งานได้
        public static JObject jsonnhso;
        public static string jsonsmartAgent, josnSendInsure;
        //Alert เช็คลงทะเบียนซ้ำ
        //public static string alertHN, alertCln;
        private readonly BackgroundWorker worker = new BackgroundWorker();
        //set ตัวแปรพิมพ์ยัตรคิวซ้ำ
        public static string reprintHn, reprintCln, reprintFullname,reprintBirthdate,reprintSex,reprintAge,reprintPttype,reprintQueuenumber,reprintQueuepriority,reprintAuthencode, reprintIdpttype, reprintDate, reprintTime;
        // set ตัวแปรจากการอ่านข้อมูลจากบัตรประชาชน
        public static string Strtxtnshdata_maininsclName, Strtxtnshdata_subinsclName, Strtxtnshdata_hmainName, Strtxthosub, Strtxtnshdata_startdate, Strtxtexpdate;
        //set ตัวแปร CID จาก HI PT
        public static string PTCID,PTFULLNAME,PTHN,PTTYPE, PTTYPECODE;
        //setตัวแปร ดูประวัต Authen Code
        public static string CIDNHS;
        //ตัวแปรหลังจากเปิด Froms ค้นหาด้วยชื่อ-สกุล และบัตรประชาชน
        public static string statusOpenfroms = "N";
        //ตัวแปรวันที่จาก datepicker
        public static string datepicker,datebackvisit, datepickerAuthen;
        public static string clickbk = "";
        public static string idinsure = "";

        /// <smartcardagent>
        string API_LOCALHOST = "http://localhost:8189/";
        string API_PREFIX_READ = "api/smartcard/read";
        string API_PREFIX_READ_ONLY = "api/smartcard/read-card-only";
        string API_PREFIX_PROBE = "api/smartcard/probe/";
        string API_PREFIX_CONFIRM_SAVE = "api/nhso-service/confirm-save";
        string API_PREFIX_SAVE_DRAFT = "api/nhso-service/save-as-draft";
        //จังหวัด อำเภอ ตำบล หมู่บ้าน
        string strProvince_code = "", strAmphur_code = "", strTumbon_code = "", strVillage_code = "";
        string stroccptn, ctzshp, ntnlty, rlgn, mrtlst, typearea, housetype, strengpname, strpnameth;

        public string WindowName { get { return ( string ) GetValue(WindowNameProperty); } set { SetValue(WindowNameProperty, value); } }
        public static readonly DependencyProperty WindowNameProperty = DependencyProperty.Register("frmReferIn", typeof(string), typeof(frmMdr), null);
        public XmlDocument xmlSentInsurl = new XmlDocument();
        public static string contentInsure = "";
        public static string Statuschangeinsure = "0";
        //DataTable dtvisit = new System.Data.DataTable();
        DataTable dtvisitAuthen = new System.Data.DataTable();


        private void Application_Startup(object sender, StartupEventArgs e)
        {

            //    this.txtsearch.Focus();
            //   Keyboard.Focus(this.txtsearch);
            //if (e.Args.Length > 0)
            //{
            //    MessageBox.Show("e.Args.Length=" + e.Args.Length);
            //    APIConnect.USER_LOGIN = e.Args[0];
            //    APIConnect.SELECT_ROLE = e.Args[1];
            //    APIConnect.INSERT_ROLE = e.Args[2];
            //    APIConnect.UPDATE_ROLE = e.Args[3];
            //    APIConnect.DELETE_ROLE = e.Args[4];
            //}
        }
        public static void WriteVersionToFile()
        {
            string directoryPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "bin", "Debug", "net6.0-windows");
            string filePath = Path.Combine(directoryPath, "mdrversion.txt");

            Version version = Assembly.GetExecutingAssembly().GetName().Version;

            try
            {
                string versionString = $"{version.Major}.{version.Minor}.{version.Build}.{version.Revision}";
                Directory.CreateDirectory(directoryPath);
                File.WriteAllText(filePath, versionString);
                Console.WriteLine("เขียนเลขเวอร์ชันลงในไฟล์สำเร็จ");
            }
            catch (Exception ex)
            {
                Console.WriteLine("เกิดข้อผิดพลาดในการเขียนเลขเวอร์ชันลงในไฟล์: " + ex.Message);
            }
        }

        public frmMdr()
        {
            InitializeComponent();
            //webview_showprint.EnsureCoreWebView2Async();
            worker.DoWork += worker_DoWork;
            worker.RunWorkerCompleted += worker_RunWorkerCompleted;
        }

        private void ClickClose(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void ClickMin(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;

        }
        private void moveallposition(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                this.DragMove();
            }
        }

        private void ClickRestore(object sender, RoutedEventArgs e)
        {
            if (WindowState == WindowState.Normal)
            {
                this.WindowState = WindowState.Maximized;
            }
            else
                this.WindowState = WindowState.Normal;
        }

        private void ClickReferIn(object sender, RoutedEventArgs e)
        {
            GridMdr.Effect = new BlurEffect();
            GridMdr.Visibility = Visibility.Visible;
            frmReferins frmReferins = new frmReferins();
            frmReferins.ShowDialog();
            GridMdr.Effect = null;
            
        }

        private void ChickReferOut(object sender, RoutedEventArgs e)
        {
            GridMdr.Effect = new BlurEffect();
            GridMdr.Visibility = Visibility.Visible;
            frmReferOut frmReferOut = new frmReferOut();
            frmReferOut.ShowDialog();
            GridMdr.Effect = null;
            
        }
        private void ClearDataresetHis()
        {

            sppttypeovst.Visibility = Visibility.Visible;
            spappoint.Visibility = Visibility.Visible;
            spauthencode.Visibility = Visibility.Visible;
            spdct.Visibility = Visibility.Visible;
            sppttypept.Visibility = Visibility.Visible;
            spqueue.Visibility = Visibility.Visible;
            cb_servicetypefu.Visibility = Visibility.Visible;
            cb_changepttype.Visibility = Visibility.Visible;
            cb_Register.Visibility = Visibility.Visible;
            cb_print.Visibility = Visibility.Visible;
            cb_insure.Visibility = Visibility.Visible;
            this.cbbPname.Text = "";
            this.txtpt_fname.Text = "";
            this.txtpt_lname.Text = "";
            this.cbbSexth.Text = "";
            this.txtpt_hn.Text = "";
            this.cbbPname_eng.Text = "";
            this.txtpt_fname_eng.Text = "";
            this.txtpt_lname_eng.Text = "";
            this.cbbSexen.Text = "";
            this.txtpt_cid.Text = "";
            this.txtpassport.Text = "";
            this.txtpt_HouseNo.Text = "";
            this.cbbChanwat.Text = "";
            this.cbbAmphur.Text = "";
            this.cbbTumbon.Text = "";
            this.cbbVillage.Text = "";
            this.txtpt_dob.Text = "00/00/0000";
            //this.txtpt_dob.DisplayDate = DateTime.Now;
            this.date_dthdate.Text = "00/00/0000"; 
            //this.date_dthdate.DisplayDate = DateTime.Now;
            this.txtpt_phone_number.Text = "";
            this.cbbpt_citizenship.Text = "";
            this.cbbpt_nationality.Text = "";
            this.cbbpt_religion.Text = "";
            this.cbbpt_mrtlst.Text = "";
            this.cbbpt_occupation.Text = "";
            this.txtpt_father_name.Text = "";
            this.txtpt_mother_name.Text = "";
            this.txtpt_contact_name.Text = "";
            this.txtpt_contact_relation.Text = "";
            this.txtpt_house_id.Text = "";
            this.cbbhousetype.Text = "";
            this.txtpt_contact_address.Text = "";
            this.txtpt_contact_phone_number.Text = "";
            this.cbbbloodgroup.Text = "";
            this.txtpt_allergy.Text = "";
            this.cbbareatype.Text = "";
            this.txtpt_cidlabor.Text = "";
            //this.txtAgenow.Text = "";
            this.txtCheckstatus.Text = "";
            this.txtPtAgenow.Text = "";
            this.cbbpt_claimtype.Text = "";
            this.txt_cbbpttype.Text = "";
            this.cbb_pttype.Text = "";
            //reset colorbox
            this.txtpt_hn.BorderBrush = (SolidColorBrush)new BrushConverter().ConvertFromString("#FF599D7E");
            this.cbbClinic.BorderBrush = (SolidColorBrush)new BrushConverter().ConvertFromString("#FF599D7E");
            this.txtcbbClinic.BorderBrush = (SolidColorBrush)new BrushConverter().ConvertFromString("#FF599D7E");
            this.cbbpttype.BorderBrush = (SolidColorBrush)new BrushConverter().ConvertFromString("#FF599D7E");
            this.txtcbbpttype.BorderBrush = (SolidColorBrush)new BrushConverter().ConvertFromString("#FF599D7E");
            this.cbbpt_queuetype.BorderBrush = (SolidColorBrush)new BrushConverter().ConvertFromString("#FF599D7E");
            this.txtpt_phone_number.BorderBrush = (SolidColorBrush)new BrushConverter().ConvertFromString("#FF599D7E");
            //เคลียร์ปุ่ม
            //this.pBar.Visibility = Visibility.Collapsed;
            this.txtnhsdata_cid.Visibility = Visibility.Visible;
            this.lb_maininsclName.Visibility = Visibility.Visible;
            this.txtnshdata_maininsclName.Visibility = Visibility.Visible;
            this.lb_subinsclName.Visibility = Visibility.Visible;
            this.txtnshdata_subinsclName.Visibility = Visibility.Visible;
            this.lb_startdate.Visibility = Visibility.Visible;
            this.txtnshdata_startdate.Visibility = Visibility.Visible;
            this.lb_hmainName.Visibility = Visibility.Visible;
            this.txtnshdata_hmainName.Visibility = Visibility.Visible;
            this.lb_txthosub.Visibility = Visibility.Visible;
            this.txthosub.Visibility = Visibility.Visible;
            this.lb_expdate.Visibility = Visibility.Visible;
            this.txtexpdate.Visibility = Visibility.Visible;
            //เคลียร์เลข Authen 
            reprintAuthencode = "";
            HI7.Class.HIUility._claimCode = "";
            this.cb_servicetypefu.IsChecked = false;
            Strtxtnshdata_maininsclName = "";
            Strtxtnshdata_subinsclName = "";
            Strtxtnshdata_hmainName = "";
            Strtxthosub = "";
            Strtxtnshdata_startdate = "";
            Strtxtexpdate = "";
        }
        private void ClearDatareset() {
            this.txtPttypetoday.Text = "";
            this.txtnote_insure.Text = "";
            txtsearchfname.Text = "";
            txtfindregister.Text = "";
            txtnote_insure.Text = "";
            sppttypeovst.Visibility = Visibility.Visible;
            spappoint.Visibility = Visibility.Visible;
            spauthencode.Visibility = Visibility.Visible;
            spdct.Visibility = Visibility.Visible;
            sppttypept.Visibility = Visibility.Visible;
            spqueue.Visibility = Visibility.Visible;
            cb_servicetypefu.Visibility = Visibility.Visible;
            cb_changepttype.Visibility = Visibility.Visible;
            cb_Register.Visibility = Visibility.Visible;
            cb_print.Visibility = Visibility.Visible;
            cb_insure.Visibility = Visibility.Visible;

            this.txtsearch.Text = "";
            this.txtIDCard.Text = "";
            this.txtBirthDate.Text = "";
            //txtBirthDate.ResetDate();
            this.txtPrefixThai.Text = "";
            this.txtFirstNameThai.Text = "";
            this.txtLastnameThai.Text = "";
            this.txtPrefixEng.Text = "";
            this.txtFirstNameEng.Text = "";
            this.txtLastnameEng.Text = "";
            this.txtSex.Text = "";
            this.txtsexeng.Text = "";
            this.txtHouseNo.Text = "";
            this.txtVillageNo.Text = "";
            this.txtSub_district.Text = "";
            this.txtDistrict.Text = "";
            this.txtProvince.Text = "";
            this.txtIssueDate.Text = "";
            this.txtExpireDate.Text = "";
            this.txtissuecard.Text = "";
            /// get his
            this.cbbPname.Text = "";
            this.txtpt_fname.Text = "";
            this.txtpt_lname.Text = "";
            this.cbbSexth.Text = "";
            this.txtpt_hn.Text = "";
            this.cbbPname_eng.Text = "";
            this.txtpt_fname_eng.Text = "";
            this.txtpt_lname_eng.Text = "";
            this.cbbSexen.Text = "";
            this.txtpt_cid.Text = "";
            this.txtpassport.Text = "";
            this.txtpt_HouseNo.Text = "";
            this.cbbChanwat.Text = "";
            this.cbbAmphur.Text = "";
            this.cbbTumbon.Text = "";
            this.cbbVillage.Text = "";
            this.txtpt_dob.Text = "00/00/0000";
            //this.txtpt_dob.DisplayDate = DateTime.Now;
            this.date_dthdate.Text = "00/00/0000";
            //this.date_dthdate.DisplayDate = DateTime.Now;
            this.txtpt_phone_number.Text = "";
            this.cbbpt_citizenship.Text = "";
            this.cbbpt_nationality.Text = "";
            this.cbbpt_religion.Text = "";
            this.cbbpt_mrtlst.Text = "";
            this.cbbpt_occupation.Text = "";
            this.txtpt_father_name.Text = "";
            this.txtpt_mother_name.Text = "";
            this.txtpt_contact_name.Text = "";
            this.txtpt_contact_relation.Text = "";
            this.txtpt_house_id.Text = "";
            this.cbbhousetype.Text = "";
            this.txtpt_contact_address.Text = "";           
            this.txtpt_contact_phone_number.Text = "";
            this.cbbbloodgroup.Text = "";
            this.txtpt_allergy.Text = "";
            this.cbbareatype.Text = "";
            this.txtpt_cidlabor.Text = "";
            this.txtAgenow.Text = "";
            this.txtCheckstatus.Text = "";
            this.txtPtAgenow.Text = "";
            this.txt_cbbpttype.Text = "";
            this.cbb_pttype.Text = "";
            //nhso
            this.txtnhsdata_cid.Text = "";
            //this.txtnshdata_province.Text = "";
            this.txtnshdata_maininsclName.Text = "";
            this.txtnshdata_subinsclName.Text = "";
            this.txtnshdata_startdate.Text = "";
            this.txtnshdata_hmainName.Text = "";
            this.txthosub.Text = "";
            this.txtexpdate.Text = "";
            //this.txtstartdatesss.Text = "";
            // register
            //this.txtpttype_pt.Text = "";
            //this.txtpttype_last.Text = "";
            this.cbbpttype.Text = "";
            this.cbbClinic.Text = "";
            this.cbbpt_queuetype.Text = "";
            this.txtsend_appoint.Text = "";
            this.txtsend_docappoint.Text = "";
            //Claimtype/ClaimCode
            this.txtclaimcode.Text = "";
            this.txtclaimtype.Text = "";
            this.cbbpt_claimtype.Text = "";
            this.txtcbbClinic.Text = "";
            this.txtcbbpttype.Text = "";

            //Clear public
            HI7.Class.HIUility._SENDCLN = "";
            HI7.Class.HIUility._FULLNAMEDCT = "";
            _BIRTHDATEPT = "";
            _DTHDATE = "";
            strProvince_code = "";
            strAmphur_code = "";
            strTumbon_code = "";
            strVillage_code = "";
            _VNGETDATAGRID = null;
            _HNGETDATAGRID = null;
            //ลบตัวแปรสร้างคิว
            HI7.Class.HIUility._VN = ""; 
            HI7.Class.HIUility._HN = "";
            HI7.Class.HIUility._CLN = ""; 
            HI7.Class.HIUility._PriorityIDQ4U = ""; 
            HI7.Class.HIUility._PnameQ4U = ""; 
            HI7.Class.HIUility._PTTYPE =""; 
            HI7.Class.HIUility._SexQ4U = "";
            HI7.Class.HIUility._FnameQ4U = "";
            HI7.Class.HIUility._LnameQ4U = ""; 
            HI7.Class.HIUility._BrthdateQ4U = "";
            HI7.Class.HIUility._BrthdateQ4UPrint = "";
            //ยกเลิก Collapsed ทั้งหมดที่ซ่อนไว้
            btnRegister.Visibility = Visibility.Visible;
            //btnReferIn.Visibility = Visibility.Visible;
            //btnReferOut.Visibility = Visibility.Visible;
            btnRegisterappoint.Visibility = Visibility.Collapsed;
            btnEdittype.Visibility = Visibility.Collapsed;
            btnEditcln.Visibility = Visibility.Collapsed;
            //ล้างรูปภาพหลังจากเสียบบัตร
            this.BitmapToBitmapImageNotpicture();
            //reset colorbox
            this.txtpt_hn.BorderBrush = (SolidColorBrush)new BrushConverter().ConvertFromString("#FF599D7E");
            this.cbbClinic.BorderBrush = (SolidColorBrush)new BrushConverter().ConvertFromString("#FF599D7E");
            this.txtcbbClinic.BorderBrush = (SolidColorBrush)new BrushConverter().ConvertFromString("#FF599D7E");
            this.cbbpttype.BorderBrush = (SolidColorBrush)new BrushConverter().ConvertFromString("#FF599D7E");
            this.txtcbbpttype.BorderBrush = (SolidColorBrush)new BrushConverter().ConvertFromString("#FF599D7E");
            this.cbbpt_queuetype.BorderBrush = (SolidColorBrush)new BrushConverter().ConvertFromString("#FF599D7E");
            this.txtpt_phone_number.BorderBrush = (SolidColorBrush)new BrushConverter().ConvertFromString("#FF599D7E");
            this.cbbSexth.BorderBrush = (SolidColorBrush)new BrushConverter().ConvertFromString("#FF599D7E");
            this.lb_txtpt_phone_number.Foreground = (SolidColorBrush)new BrushConverter().ConvertFromString("#FF0E482E");
            this.txtpt_phone_number.BorderBrush = (SolidColorBrush)new BrushConverter().ConvertFromString("#FFE48524");
            this.lb_txtpt_cid.Foreground = (SolidColorBrush)new BrushConverter().ConvertFromString("#FF0E482E");
            this.txtpt_cid.BorderBrush = (SolidColorBrush)new BrushConverter().ConvertFromString("#FFE48524");

            //เคลียร์ปุ่ม
            //pBar.Visibility = Visibility.Collapsed;
            this.txtnhsdata_cid.Visibility = Visibility.Visible;
            this.lb_maininsclName.Visibility = Visibility.Visible;
            this.txtnshdata_maininsclName.Visibility = Visibility.Visible;
            this.lb_subinsclName.Visibility = Visibility.Visible;
            this.txtnshdata_subinsclName.Visibility = Visibility.Visible;
            this.lb_startdate.Visibility = Visibility.Visible;
            this.txtnshdata_startdate.Visibility = Visibility.Visible;
            this.lb_hmainName.Visibility = Visibility.Visible;
            this.txtnshdata_hmainName.Visibility = Visibility.Visible;
            this.lb_txthosub.Visibility = Visibility.Visible;
            this.txthosub.Visibility = Visibility.Visible;
            this.lb_expdate.Visibility = Visibility.Visible;
            this.txtexpdate.Visibility = Visibility.Visible;
            txtClinic = "";
            dataClinic = "";
            idClinic = "";
            txtPttype = "";
            dataPttype = "";
            idPttype = "";
            searchhn = "";
        //อ่านบัตรครั้งเดียวถ้ามีเก็บไว้ไม่ต้องไปอ่านอีก
            personalCard = null;
            _HNdouble = "";
            _CIDREADER = "";
            _IDSCREEN = "";
            _CLN = "";
            _PRYORITY = "";
            _BIRTHDATEPT = "";
            _DTHDATE = "";
            _VNGETDATAGRID = "";
            _HNGETDATAGRID = "";
            _PTTYPEDATAGRID = "";
            //ประกาศตัวแปรให้มองเห็นทุกฟังก์ชั่นนำไปใช้งานได้
            jsonnhso = null;
            jsonsmartAgent = "";
            _ClickAppointsearchhn = "";
            //ตัวแปร ptcid
            PTCID = "";
            //เคลียร์เลข Authen 
            reprintAuthencode = "";
            HI7.Class.HIUility._claimCode = "";
            this.cb_servicetypefu.IsChecked = false;
            Strtxtnshdata_maininsclName = "";
            Strtxtnshdata_subinsclName = "";
            Strtxtnshdata_hmainName = "";
            Strtxthosub = "";
            Strtxtnshdata_startdate = "";
            Strtxtexpdate = "";

        }
        private void ClearData()
        {
            this.txtPttypetoday.Text = "";
            this.txtnote_insure.Text = "";
            VN = "";
            //txtsearchfname.Text = "";
            txtfindregister.Text = "";
            sppttypeovst.Visibility = Visibility.Visible;
            spappoint.Visibility = Visibility.Visible;
            spauthencode.Visibility = Visibility.Visible;
            spdct.Visibility = Visibility.Visible;
            sppttypept.Visibility = Visibility.Visible;
            spqueue.Visibility = Visibility.Visible;
            cb_servicetypefu.Visibility = Visibility.Visible;
            cb_changepttype.Visibility = Visibility.Visible;
            cb_Register.Visibility = Visibility.Visible;
            cb_print.Visibility = Visibility.Visible;
            cb_insure.Visibility = Visibility.Visible;
            this.txtIDCard.Text = "";
            txtBirthDate.Text = string.Empty;
            //txtBirthDate.ResetDate();
            this.txtPrefixThai.Text = "";
            this.txtFirstNameThai.Text = "";
            this.txtLastnameThai.Text = "";
            this.txtPrefixEng.Text = "";
            this.txtFirstNameEng.Text = "";
            this.txtLastnameEng.Text = "";
            this.txtSex.Text = "";
            this.txtsexeng.Text = "";
            this.txtHouseNo.Text = "";
            this.txtVillageNo.Text = "";
            this.txtSub_district.Text = "";
            this.txtDistrict.Text = "";
            this.txtProvince.Text = "";
            this.txtIssueDate.Text = "";
            this.txtExpireDate.Text = "";
            this.txtissuecard.Text = "";
            this.txtAgenow.Text = "";
            this.txtCheckstatus.Text = "";
            /// get his
            this.cbbPname.Text = "";
            this.txtpt_fname.Text = "";
            this.txtpt_lname.Text = "";
            this.cbbSexth.Text = "";
            this.txtpt_hn.Text = "";
            this.cbbPname_eng.Text = "";
            this.txtpt_fname_eng.Text = "";
            this.txtpt_lname_eng.Text = "";
            this.cbbSexen.Text = "";
            this.txtpt_cid.Text = "";
            this.txtpassport.Text = "";
            this.txtpt_HouseNo.Text = "";
            this.cbbChanwat.Text = "";
            this.cbbAmphur.Text = "";
            this.cbbTumbon.Text = "";
            this.cbbVillage.Text = "";
            this.txtpt_dob.Text = "00/00/0000";
            //this.txtpt_dob.DisplayDate = DateTime.Now;
            this.date_dthdate.Text = "00/00/0000";
            //this.date_dthdate.DisplayDate = DateTime.Now;
            this.txtpt_phone_number.Text = "";
            this.cbbpt_citizenship.Text = "";
            this.cbbpt_nationality.Text = "";
            this.cbbpt_religion.Text = "";
            this.cbbpt_mrtlst.Text = "";
            this.cbbpt_occupation.Text = "";
            this.txtpt_father_name.Text = "";
            this.txtpt_mother_name.Text = "";
            this.txtpt_contact_name.Text = "";
            this.txtpt_contact_relation.Text = "";
            this.txtpt_house_id.Text = "";
            this.cbbhousetype.Text = "";
            this.txtpt_contact_address.Text = "";
            this.txtpt_contact_phone_number.Text = "";
            this.cbbbloodgroup.Text = "";
            this.txtpt_allergy.Text = "";
            this.cbbareatype.Text = "";
            this.txtpt_cidlabor.Text = "";
            this.txtPtAgenow.Text = "";
            this.txt_cbbpttype.Text = "";
            this.cbb_pttype.Text = "";
            //nhso
            this.txtnhsdata_cid.Text = "";
            //this.txtnshdata_province.Text = "";
            this.txtnshdata_maininsclName.Text = "";
            this.txtnshdata_subinsclName.Text = "";
            this.txtnshdata_startdate.Text = "";
            this.txtnshdata_hmainName.Text = "";
            this.txthosub.Text = "";
            this.txtexpdate.Text = "";
            //this.txtstartdatesss.Text = "";
            // register
            //this.txtpttype_pt.Text = "";
            //this.txtpttype_last.Text = "";
            this.cbbpttype.Text = "";
            this.txtcbbpttype.Text = "";
            this.cbbClinic.Text = "";
            this.txtcbbClinic.Text = "";
            this.cbbpt_queuetype.Text = "";
            this.txtsend_appoint.Text = "";
            this.txtsend_docappoint.Text = "";
            //Claimtype/ClaimCode
            this.txtclaimcode.Text = "";
            this.txtclaimtype.Text = "";
            this.cbbpt_claimtype.Text = "";

            //reset btn
            btnRegister.Visibility = Visibility.Visible;
            btnRegisterappoint.Visibility = Visibility.Collapsed;
            cb_Register.IsChecked = false;
            this.BitmapToBitmapImageNotpicture();
            //reset colorbox
            this.txtpt_hn.BorderBrush = (SolidColorBrush)new BrushConverter().ConvertFromString("#FF599D7E");
            this.cbbClinic.BorderBrush = (SolidColorBrush)new BrushConverter().ConvertFromString("#FF599D7E");
            this.txtcbbClinic.BorderBrush = (SolidColorBrush)new BrushConverter().ConvertFromString("#FF599D7E");
            this.cbbpttype.BorderBrush = (SolidColorBrush)new BrushConverter().ConvertFromString("#FF599D7E");
            this.txtcbbpttype.BorderBrush = (SolidColorBrush)new BrushConverter().ConvertFromString("#FF599D7E");
            this.cbbpt_queuetype.BorderBrush = (SolidColorBrush)new BrushConverter().ConvertFromString("#FF599D7E");
            this.txtpt_phone_number.BorderBrush = (SolidColorBrush)new BrushConverter().ConvertFromString("#FF599D7E");
            this.cbbSexth.BorderBrush = (SolidColorBrush)new BrushConverter().ConvertFromString("#FF599D7E");
            this.lb_txtpt_phone_number.Foreground = (SolidColorBrush)new BrushConverter().ConvertFromString("#FF0E482E");
            this.txtpt_phone_number.BorderBrush = (SolidColorBrush)new BrushConverter().ConvertFromString("#FFE48524");
            this.lb_txtpt_cid.Foreground = (SolidColorBrush)new BrushConverter().ConvertFromString("#FF0E482E");
            this.txtpt_cid.BorderBrush = (SolidColorBrush)new BrushConverter().ConvertFromString("#FFE48524");
            //Clear public
            HI7.Class.HIUility._SENDCLN = "";
            HI7.Class.HIUility._FULLNAMEDCT = "";
            _BIRTHDATEPT = "";
            _DTHDATE = "";
            strProvince_code = "";
            strAmphur_code = "";
            strTumbon_code = "";
            strVillage_code = "";
            _VNGETDATAGRID = null;
            _HNGETDATAGRID = null;
            //เคลียร์ปุ่ม
            //pBar.Visibility = Visibility.Collapsed;
            this.txtnhsdata_cid.Visibility = Visibility.Visible;
            this.lb_maininsclName.Visibility = Visibility.Visible;
            this.txtnshdata_maininsclName.Visibility = Visibility.Visible;
            this.lb_subinsclName.Visibility = Visibility.Visible;
            this.txtnshdata_subinsclName.Visibility = Visibility.Visible;
            this.lb_startdate.Visibility = Visibility.Visible;
            this.txtnshdata_startdate.Visibility = Visibility.Visible;
            this.lb_hmainName.Visibility = Visibility.Visible;
            this.txtnshdata_hmainName.Visibility = Visibility.Visible;
            this.lb_txthosub.Visibility = Visibility.Visible;
            this.txthosub.Visibility = Visibility.Visible;
            this.lb_expdate.Visibility = Visibility.Visible;
            this.txtexpdate.Visibility = Visibility.Visible;
            //ตัวแปรบน MDR
            txtClinic = "";
            dataClinic = "";
            idClinic = "";
            txtPttype = "";
            dataPttype = "";
            idPttype = "";
            searchhn = "";
            //อ่านบัตรครั้งเดียวถ้ามีเก็บไว้ไม่ต้องไปอ่านอีก
            personalCard = null;
            _HNdouble = "";
            _CIDREADER = "";
            _IDSCREEN = "";
            _CLN = "";
            _PRYORITY = "";
            _VNGETDATAGRID = "";
            _HNGETDATAGRID = "";
            _PTTYPEDATAGRID = "";
            //ประกาศตัวแปรให้มองเห็นทุกฟังก์ชั่นนำไปใช้งานได้
            jsonnhso = null;
            jsonsmartAgent = "";
            _ClickAppointsearchhn = "";
            //ตัวแปร ptcid
            PTCID = "";
            //เคลียร์เลข Authen 
            reprintAuthencode = "";
            HI7.Class.HIUility._claimCode = "";
            this.cb_servicetypefu.IsChecked = false;
            Strtxtnshdata_maininsclName = "";
            Strtxtnshdata_subinsclName = "";
            Strtxtnshdata_hmainName = "";
            Strtxthosub = "";
            Strtxtnshdata_startdate = "";
            Strtxtexpdate = "";
            btnUpdateCard.Visibility = Visibility.Collapsed;

        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                //ดึงเวอร์ชั่น จากAssembly 
                //string assemblyVersion = Assembly.GetExecutingAssembly().GetName().Version.ToString();
                //Hi7.Class.APIConnect.GetMDRVersionFromXML();
                
                lblVersion.Content = "1.4.8";
                //this.SetPublishVersion();
                
                DispatcherTimer dt = new DispatcherTimer();
                dt.Interval = TimeSpan.FromSeconds(1);
                dt.Tick += dtTicker;
                dt.Dispatcher.ShutdownStarted += (s, e) => dt.Stop();
                dt.Start();
                int nWidth = (int)System.Windows.SystemParameters.PrimaryScreenWidth;
                int nHieght = (int)System.Windows.SystemParameters.PrimaryScreenHeight;
                this.LayoutTransform = new ScaleTransform(nWidth, nHieght);
                lbWindowsscreen.Content = nWidth + "*" + nHieght;
                //ServicePointManager.ServerCertificateValidationCallback = delegate { return true; };
                Keyboard.Focus(this.txtsearch);
                Hi7.Class.APIConnect.getConfgXML();
                HI7.Class.HIUility.getTokenShare();
               
                //HI7.Class.HIUility.getTokenShare();
                HI7.Class.HIUility.getHcode();
                if (HI7.Class.HIUility._HCODE == "") {
                    MessageBox.Show("กรุณาLoginใหม่");
                    this.Close();
                    
                }
                HI7.Class.HIUility.getHostname();
                //initailData();
                getVisit();
                getPrintergetway();
                btnUpdateCard.Visibility = Visibility.Collapsed;//ปิดปุ่มแก้ไขข้อมูลด้วยบัตรประชาชน
                lbLogin.Content = Hi7.Class.APIConnect.USER_LOGIN;
                strstaff = Hi7.Class.APIConnect.USER_IDLOGIN;
                lbhospitalname.Content = HI7.Class.HIUility._HOSPITALNAME;
                initailData();
                this.cbbpt_claimtype.SelectedIndex = 0;
                //MyMethodToCallExpansiveOperation();


            }
            catch (Exception ex)
            {
                Growl.Error("Window_Loaded say:\r\n" + ex.Message);
            }

        }
        private void SetPublishVersion()
        {
            string publishVersion = GetPublishVersion();

            if (!string.IsNullOrEmpty(publishVersion))
            {
                string versionText = string.Format("เวอร์ชั่น 1 Build: {0}", publishVersion);
                lblVersion.Content = versionText;
            }
            else
            {
                lblVersion.Content = " N/A";
            }
        }
        private string GetPublishVersion()
        {
            try
            {
                AppDomain currentDomain = AppDomain.CurrentDomain;
                string appBasePath = currentDomain.BaseDirectory;

                string manifestPath = System.IO.Path.Combine(appBasePath, "Mdr.exe.manifest");
                if (File.Exists(manifestPath))
                {
                    XDocument manifestDoc = XDocument.Load(manifestPath);
                    XNamespace ns = "urn:schemas-microsoft-com:asm.v1";

                    XElement assemblyIdentity = manifestDoc.Descendants(ns + "assemblyIdentity").FirstOrDefault();
                    if (assemblyIdentity != null)
                    {
                        XAttribute versionAttribute = assemblyIdentity.Attribute("version");
                        if (versionAttribute != null)
                        {
                            return versionAttribute.Value;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // จัดการข้อผิดพลาดที่เกิดขึ้น
            }

            return string.Empty;
        }
        private void SetDate()
        {
            //datetoday.Text = DateTime.Now.Date.ToString("dd/MM/yyyy");
            datetoday.Text = HI7.Class.HIUility.Getdateserver();
            Getvaluedatetoday(datetoday.Text);
            //labeldatenow.Content = DateTime.Now.ToString("ddddที่ dd MMMM พ.ศ.yyyy เวลา HH:mm น.");
        }
        private void dtTicker(object sender,EventArgs e)
        {

            labeldatenow.Dispatcher.Invoke(() =>
            {
                labeldatenow.Content = new StackPanel
                {
                    Orientation = Orientation.Horizontal,
                    Children =
                {
            new ImageAwesome
            {
                Icon = FontAwesomeIcon.Calendar,
                VerticalAlignment = VerticalAlignment.Center,
                HorizontalAlignment = HorizontalAlignment.Center,
                Width = 30,
                Height = 22,
                Foreground = new System.Windows.Media.SolidColorBrush((System.Windows.Media.Color)System.Windows.Media.ColorConverter.ConvertFromString("#FF599D7E"))

            },
            new TextBlock
            {
                Text = HI7.Class.HIUility.Getdateserverdatethai(),
                VerticalAlignment = VerticalAlignment.Center
            },
            new ImageAwesome
            {
                Icon = FontAwesomeIcon.ClockOutline,
                VerticalAlignment = VerticalAlignment.Center,
                HorizontalAlignment = HorizontalAlignment.Center,
                Width = 30,
                Height = 22,
                Foreground = new System.Windows.Media.SolidColorBrush((System.Windows.Media.Color)System.Windows.Media.ColorConverter.ConvertFromString("#FF599D7E"))
            },
            new TextBlock
            {
                Text = DateTime.Now.ToString("HH:mm:ss น."),
                VerticalAlignment = VerticalAlignment.Center
            }
        }
                };
            });

        }
        void MyMethodToCallExpansiveOperation()
        {
            //Call method to show wait screen
            BackgroundWorker workertranaction = new BackgroundWorker();
            workertranaction.DoWork += new DoWorkEventHandler(workertranaction_DoWork);
            workertranaction.RunWorkerCompleted += new RunWorkerCompletedEventHandler(
            workertranaction_RunWorkerCompleted);
            workertranaction.RunWorkerAsync();
        }
        void workertranaction_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {

            //this.getAmphur();
            //this.getTumbon();
            //this.getVillage();
            //this.getClinic();
            //this.getPttype();
            //this.getPnameEN();
            //this.getPnameTH();
            //this.getOccupation();
            //this.getCitizen();
            //this.getNation();
            //this.getReligion();
            //this.getQueueType();
            //this.getMrtlst();
            //this.getAreaType();
            //this.getHouseType();
            //this.getPttypeName();
            //this.getPttypePtHIS();
            //this.getodata_Screen();
            //this.getClaimtype();
            //this.getProvince();
            this.getProvince();
            //Growl.Warning("getProvince");
            this.getAmphur();
            //Growl.Warning("getAmphur");
            this.getTumbon();
            //Growl.Warning("getTumbon");
            this.getVillage();
            //Growl.Warning("getVillage");
            this.getClinic();
            //Growl.Warning("getClinic");
            this.getPttype();
            //Growl.Warning("getPttype");
            this.getPnameEN();
            //Growl.Warning("getPnameEN");
            this.getPnameTH();
            //Growl.Warning("getPnameTH");
            this.getOccupation();
            //Growl.Warning("getOccupation");
            this.getCitizen();
            //Growl.Warning("getCitizen");
            this.getNation();
            //Growl.Warning("getNation");
            this.getReligion();
            //Growl.Warning("getReligion");
            this.getQueueType();
            //Growl.Warning("getQueueType");
            this.getMrtlst();
            //Growl.Warning("getMrtlst");
            this.getAreaType();
            //Growl.Warning("getAreaType");
            this.getHouseType();
            //Growl.Warning("getHouseType");
            //this.getPttypeName();
            //Growl.Warning("getPttypeName");
            this.getPttypePtHIS();
            //Growl.Warning("getPttypePtHIS");
            this.getodata_Screen();
            //Growl.Warning("getodata_Screen");
            this.getClaimtype();
            //Growl.Warning("getClaimtype");
            
        }
        void workertranaction_DoWork(object sender, DoWorkEventArgs e)
        {
            ////this.getProvince();
            //Growl.Warning("getProvince");
            ////this.getAmphur();
            //Growl.Warning("getAmphur");
            ////this.getTumbon();
            //Growl.Warning("getTumbon");
            ////this.getVillage();
            //Growl.Warning("getVillage");
            ////this.getClinic();
            //Growl.Warning("getClinic");
            ////this.getPttype();
            //Growl.Warning("getPttype");
            ////this.getPnameEN();
            //Growl.Warning("getPnameEN");
            ////this.getPnameTH();
            //Growl.Warning("getPnameTH");
            ////this.getOccupation();
            //Growl.Warning("getOccupation");
            ////this.getCitizen();
            //Growl.Warning("getCitizen");
            ////this.getNation();
            //Growl.Warning("getNation");
            ////this.getReligion();
            //Growl.Warning("getReligion");
            ////this.getQueueType();
            //Growl.Warning("getQueueType");
            ////this.getMrtlst();
            //Growl.Warning("getMrtlst");
            ////this.getAreaType();
            //Growl.Warning("getAreaType");
            ////this.getHouseType();
            //Growl.Warning("getHouseType");
            ////this.getPttypeName();
            //Growl.Warning("getPttypeName");
            ////this.getPttypePtHIS();
            //Growl.Warning("getPttypePtHIS");
            ////this.getodata_Screen();
            //Growl.Warning("getodata_Screen");
            ////this.getClaimtype();
            //Growl.Warning("getClaimtype");
        }
        private void txtsearch_KeyDown(object sender, KeyEventArgs e)
        {
            
            if (e.Key == Key.Enter)//ตรวจสอบคีย์ Enter
            {
                ClearData();//เคลียร์ข้อมูลล่าสุดที่แสดงในฟอร์ม
                if (string.IsNullOrEmpty(this.txtsearch.Text))
                {
                    Growl.Warning("กรุณาระบุ HN ในการค้นหา");
                }
                else
                {
                    if (GetDadehn(txtsearch.Text) == true)
                    {                        
                        Growl.Warning("ผู้มารับบริการได้เสียชีวิตแล้ว\r\nไม่สามารถลงทะเบียนได้");
                    }
                    else
                    {

                    }
                    if (GetAdmithn(txtsearch.Text) == true) 
                    {
                        Growl.Warning("ผู้มารับบริการสถานะยังไม่ได้ Discharge\r\nไม่สามารถลงทะเบียนได้");
                    }
                    else
                    {

                    }
                    if (GetReferin(txtsearch.Text) == true)
                    {
                        Growl.Warning("ผู้มารับบริการสถานะ Referin เข้ามา\r\nการุณาตรวจสอบ");
                    }
                    else
                    {

                    }
                    if (GetRegistertodayhn(txtsearch.Text) == true)
                    {
                        if (MessageBox.Show("วันนี้มีการเข้ารับริการแล้ว\r\nตรวจสอบใน Tab การตารางผู้ลงทะเบียน", "เตือน HN:" + txtsearch.Text + " มีการลงทะเบียนแล้ว", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.No)
                        {
                            this.getVisit();
                            return;
                        }
                        else
                        {
                            if (Getcheck24hr(txtsearch.Text) == true)
                            {
                                string messageBoxText = "ไม่สามารถลงทะเบียนได้ เนื่องจากยังไม่ครบ 4 ชั่วโมงในกลุ่มสิทธิ์(อปท.)\r\n หากยืนยันการลงทะเบียนกด Yes หากไม่กดดำเนินการกด No";
                                string caption = "ดำเนินการต่อไป";
                                MessageBoxButton button = MessageBoxButton.YesNo;
                                MessageBoxImage image = MessageBoxImage.Warning;
                                MessageBoxResult result = MessageBox.Show(messageBoxText, caption, button, image);
                                if (result == MessageBoxResult.Yes)
                                {
                                    Growl.Warning("จะมีผลต่อการเครมตามเกณฑ์ต้องเข้ารับบริการหลัง 4 ชั่วโมงของสิทธิ์\r\nอปท.และจ่ายตรงกรมบัญชีกลาง ในการเครมด้วยสิทธิ์ LGO/OFC");
                                }
                                else {
                                    return;
                                }
                            }
                        }
                    }
                    else
                    {

                    }
                   
                        try
                    {
                        DataTable dtappoint;
                        dtappoint = HI7.Class.HIUility.getAppointment(txtsearch.Text);//ตรวจสอบการนัดผู้ป่วย
                        if (dtappoint != null && dtappoint.Rows.Count > 0)
                        {
                            //เช็คนัด
                            checkAppoint(txtsearch.Text);//แสดงฟอร์มนัด
                            if(_ClickAppointsearchhn == "Click")
                            {
                                getPatientInfoappoint("hn", this.txtsearch.Text);
                                BitmapToBitmapImageGetpt();
                                this.cbbpt_claimtype.SelectedIndex = 0;//กดหนด Defalut
                                this.cbbpt_queuetype.Text = "ปกติ";
                                this.txtsend_appoint.Text = "มาตามนัด";        
                                Keyboard.Focus(this.tabmdr);

                            }
                            else
                            {
                                //ClearDatareset();
                                btnRegister.Visibility = Visibility.Visible;//ปิดปุ่มลงทะเบียนทั่วไป
                                btnRegisterappoint.Visibility = Visibility.Collapsed;//แสดงปุ่มลงทะเบียนมีนัด                                
                                getPatientInfo("hn", txtsearch.Text);
                                this.cbbpt_claimtype.SelectedIndex = 0;//กดหนด Defalut
                                this.cbbpt_queuetype.Text = "ปกติ";
                            }
                            
                        }
                        else
                        {
                            getPatientInfo("hn", this.txtsearch.Text);
                            //ตรวจสอบสิทธิ์โดยไม่มีบัตรประชาชน
                            if (this.txtsearch.Text != null || this.txtsearch.Text != "")
                            {
                                this.UcwsnhsoCID(txtpt_cid.Text);
                            }                            
                            BitmapToBitmapImageGetpt();
                            this.cbbpt_claimtype.SelectedIndex = 0;//กดหนด Defalut
                            this.cbbpt_queuetype.Text = "ปกติ";
                            
                            Keyboard.Focus(this.tabmdr);
                            


                        }
                        Keyboard.Focus(this.txtcbbpttype);
                    }
                    catch (Exception ex)
                    {
                        Growl.Error("txtsearch_KeyDown say\r\n: เกิดข้อผิดพลาด");
                    }                                      
                }
            }
            else
               {
                    ClearData();
               }
            
        }
        
        //เรียกฟอร์มนัดผู้ป่วย
        void checkAppoint(string hn)//หาผู้ป่วยที่มีนัด
        {
            Mdr.Forms.frmCheckappoint._HNSCREEN = hn;
            frmCheckappoint frmCheckappoint = new frmCheckappoint();//เรียกฟอร์มแสดงข้อมูลการนัด
            frmCheckappoint.ShowDialog();
            btnRegister.Visibility = Visibility.Collapsed;//ปิดปุ่มลงทะเบียนทั่วไป
            btnRegisterappoint.Visibility = Visibility.Visible;//แสดงปุ่มลงทะเบียนมีนัด
            //btnReferIn.Visibility = Visibility.Collapsed;
            //btnReferOut.Visibility = Visibility.Collapsed;
            btnEditcln.Visibility = Visibility.Collapsed;
        }
        public bool GetReader()
        {
            try
            {
                System.Text.EncodingProvider provider = System.Text.CodePagesEncodingProvider.Instance;// แปลง encode tis-620
                Encoding.RegisterProvider(provider);
                var th = new ThaiIDCard();
                Personal Personal = th.readAllPhoto();
                if(Personal != null)
                {
                    if (Personal.Citizenid.ToString() != null || Personal.Citizenid.ToString() != "")
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    return false;
                }

            }
            catch (Exception ex)
            {
                return false;
            }
        }

        Int32 intReadCard_Sex;
        private void getData()//หลับดึงข้อมูลจากบัตรประชาชน
        {
            try
            {
                if (!(personalCard == null))
                {
                    GetbirthInfodataSmartCard();
                     // รูปภาพ
                    this.BitmapToMemory(personalCard.PhotoBitmap);
                    if(this.txtpt_hn.Text != null && this.txtpt_hn.Text != "")
                    {
                        this.UpdateimagePT(this.txtIDCard.Text);
                    }                    
                    this.BitmapToBitmapImage();
                    string cid = personalCard.Citizenid.ToString();
                    try
                    {
                        if (!string.IsNullOrEmpty(txtBirthDate.Text))
                        {
                            this.txtAgenow.Text = Birthdatenow(txtBirthDate.Text);
                            //DateTime dob = Convert.ToDateTime(txtBirthDate.Text);
                            //DateTime PresentYear = Convert.ToDateTime(20/07/2566);
                            //TimeSpan ts = PresentYear - dob;
                            //DateTime Age = DateTime.MinValue.AddDays(ts.Days);
                            //string fullAge = (string.Format(" {0} ปี {1} เดือน {2} วัน", Age.Year-1, Age.Month-1, Age.Day-1));
                            //this.txtAgenow.Text = fullAge;//บันทึก
                        }
                        else {
                            this.txtAgenow.Text = "เกิดข้อผิดพลาด";
                        }
                    }
                    catch (Exception ex)
                    {
                        this.txtAgenow.Text = "เกิดข้อผิดพลาด";
                    }
                    this.getPatientInfo("cid", cid);
                    
                }
                else
                {
                   // MessageBox.Show("Catch All", "รายงานความผิดพลาด", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                Growl.Error("getData say:"+ ex.Message);
            }
    }
        private string Birthdatenow(string bd)
        {
            try
            {
                string dateserver = HI7.Class.HIUility.Getdateserver();
                int bdDay = int.Parse(bd.Substring(0, 2));
                int bdMonth = int.Parse(bd.Substring(3, 2));
                int bdYear = int.Parse(bd.Substring(6, 4));

                int todayDay = int.Parse(dateserver.Substring(0, 2));
                int todayMonth = int.Parse(dateserver.Substring(3, 2));
                int todayYear = int.Parse(dateserver.Substring(6, 4));
                int age = todayYear - bdYear;

                int ageYears = todayYear - bdYear;
                int ageMonths = todayMonth - bdMonth;
                int ageDays = todayDay - bdDay;
                if (ageMonths < 0 || (ageMonths == 0 && ageDays < 0))
                {
                    ageYears--;
                    ageMonths += 12;
                }
                string ddmmyy = ageYears + " ปี " + ageMonths + " เดือน " + ageDays+" วัน";
                return ddmmyy;
            }
            catch
            {
                return "";
            }            
        }
        private void GetbirthInfodataSmartCard()
        {
            try
            {
                this.txtIDCard.Text = personalCard.Citizenid.ToString();
                this.txtnhsdata_cid.Text = personalCard.Citizenid.ToString();
                this.txtPrefixThai.Text = personalCard.Th_Prefix.ToString();
                this.txtFirstNameThai.Text = personalCard.Th_Firstname.ToString();
                this.txtPrefixEng.Text = personalCard.En_Prefix.ToString();
                this.txtLastnameThai.Text = personalCard.Th_Lastname.ToString();
                this.txtFirstNameEng.Text = personalCard.En_Firstname.ToString();
                this.txtLastnameEng.Text = personalCard.En_Lastname.ToString();
                //แปลงข้อมูลจาก 10/12/1991 เป็น 10/12/2534
                try
                {
                    this.txtBirthDate.Text = HI7.Class.HIUility.DateConvert(CheckDMY(personalCard.Birthday.Day.ToString(), personalCard.Birthday.Month.ToString(), personalCard.Birthday.Year.ToString()));
                }
                catch
                {
                    this.txtBirthDate.Text = "";
                }
                this.intReadCard_Sex = int.Parse(personalCard.Sex);
                this.txtSex.Text = this.CheckSex(int.Parse(personalCard.Sex), this.txtSex, "th").ToString();
                this.txtsexeng.Text = this.CheckSex(int.Parse(personalCard.Sex), this.txtSex, "en").ToString();
                try
                {
                    this.txtIssueDate.Text = HI7.Class.HIUility.DateConvert(CheckDMY(personalCard.Issue.Day.ToString(), personalCard.Issue.Month.ToString(), personalCard.Issue.Year.ToString()));
                }
                catch
                {
                    this.txtIssueDate.Text = "";
                }
                try
                {
                    this.txtExpireDate.Text = HI7.Class.HIUility.DateConvert(CheckDMY(personalCard.Expire.Day.ToString(), personalCard.Expire.Month.ToString(), personalCard.Expire.Year.ToString()));
                }
                catch
                {
                    this.txtExpireDate.Text = "";
                }
                this.txtHouseNo.Text = personalCard.addrHouseNo.ToString();
                this.txtVillageNo.Text = personalCard.addrVillageNo.ToString();
                this.txtSub_district.Text = personalCard.addrTambol.ToString();
                this.txtDistrict.Text = personalCard.addrAmphur.ToString();
                this.txtProvince.Text = personalCard.addrProvince.ToString();
                this.txtissuecard.Text = personalCard.Issuer.ToString();
            }
            catch
            {

            }
        }
        private string CheckDMY(string d,string m,string y) 
        {
            if (!string.IsNullOrEmpty(d))
            {
                if (d.Length == 1)
                {
                    d = '0' + d;
                }
                else
                {
                    d = d;
                }                
            }
            else
            {
                d = "00";
            }
            if (!string.IsNullOrEmpty(m))
            {
                if (m.Length == 1)
                {
                    m = '0' + m;
                }
                else
                {
                    m = m;
                }
                
            }
            else
            {
                m = "00";
            }
            if (!string.IsNullOrEmpty(y))
            {
                y = y;
            }
            else
            {
                y = "00";
            }
            return d+'/'+m+'/'+y;
        }
        bool UpdateimagePT(string cid)
        {
            try
            {
                string strSQL, strValues;
                strValues = "pathimage=" + "'" + cid + "'" + ".jpg";
                strSQL = "UPDATE pt set " + strValues + " WHERE pop_id=" + cid;
                Dictionary<string, object> dictData = new Dictionary<string, object>();
                dictData.Add("query", strSQL);
                bool status = Hi7.Class.APIConnect.Excecute("query/hi7_query", "POST", dictData);
                if (status)
                {
                    return true;
                }
                else
                {
                    return false;
                }

            }
            catch (Exception ex)
            {
                Growl.Error("UpdateimagePT UPDATE say:\r\n" + ex.Message);
                return false;
            }
        }
        private void getDataAppoint()//หลับดึงข้อมูลจากบัตรประชาชน
        {
            try
            {
                // // -------------------------------------------
                // // โค้ดส่วนของการอ่านข้อมูลบัตรประชาชน
                //System.Text.EncodingProvider provider = System.Text.CodePagesEncodingProvider.Instance;// แปลง encode tis-620
                //Encoding.RegisterProvider(provider);
                //var th = new ThaiIDCard();
                //Personal Personal = th.readAllPhoto();
                if (!(personalCard == null))
                {
                    this.txtIDCard.Text = personalCard.Citizenid.ToString();
                    this.txtnhsdata_cid.Text = personalCard.Citizenid.ToString();
                    this.txtPrefixThai.Text = personalCard.Th_Prefix.ToString();
                    this.txtFirstNameThai.Text = personalCard.Th_Firstname.ToString();
                    this.txtPrefixEng.Text = personalCard.En_Prefix.ToString();
                    this.txtLastnameThai.Text = personalCard.Th_Lastname.ToString();
                    this.txtFirstNameEng.Text = personalCard.En_Firstname.ToString();
                    this.txtLastnameEng.Text = personalCard.En_Lastname.ToString();
                    try
                    {
                        this.txtBirthDate.Text = personalCard.Birthday.ToString("dd/MM/yyyy");
                    }
                    catch (Exception ex)
                    {
                        this.txtBirthDate.Text = null;
                        Growl.Warning("ข้อมูลวันเดือนปีเกิดไม่ถูกต้อง:\r\n" + "ตัวอย่าง 10:12:2534");
                    }
                    this.intReadCard_Sex = int.Parse(personalCard.Sex);
                    this.txtSex.Text = this.CheckSex(int.Parse(personalCard.Sex), this.txtSex, "th").ToString();
                    this.txtsexeng.Text = this.CheckSex(int.Parse(personalCard.Sex), this.txtSex, "en").ToString();
                    this.txtIssueDate.Text = personalCard.Issue.ToString("dd/MM/yyyy");
                    this.txtExpireDate.Text = personalCard.Expire.ToString("dd/MM/yyyy");
                    this.txtHouseNo.Text = personalCard.addrHouseNo.ToString();
                    this.txtVillageNo.Text = personalCard.addrVillageNo.ToString();
                    this.txtSub_district.Text = personalCard.addrTambol.ToString();
                    this.txtDistrict.Text = personalCard.addrAmphur.ToString();
                    this.txtProvince.Text = personalCard.addrProvince.ToString();
                    this.txtissuecard.Text = personalCard.Issuer.ToString();
                    try
                    {
                        this.txtpt_dob.Text = personalCard.Birthday.ToString("dd/MM/yyyy");
                    }
                    catch (Exception ex)
                    {
                    }
                    // // รูปภาพ
                    this.BitmapToMemory(personalCard.PhotoBitmap);
                    if (this.txtpt_hn.Text != null && this.txtpt_hn.Text != "")
                    {
                        this.UpdateimagePT(this.txtIDCard.Text);
                    }
                    this.BitmapToBitmapImage();
                    string cid = personalCard.Citizenid.ToString();
                    try
                    {
                        if (txtBirthDate.Text != null)
                        {
                            DateTime dob = Convert.ToDateTime(txtBirthDate.Text);
                            DateTime PresentYear = DateTime.Now;
                            TimeSpan ts = PresentYear - dob;
                            DateTime Age = DateTime.MinValue.AddDays(ts.Days);
                            string fullAge = (string.Format(" {0} ปี {1} เดือน {2} วัน", Age.Year - 1, Age.Month - 1, Age.Day - 2));
                            this.txtAgenow.Text = fullAge;//บันทึก
                        }
                        else
                        {

                        }
                    }
                    catch (Exception ex)
                    {
                    }

                    //this.getPatientInfo("cid", cid);
                    getPatientInfo("hn", _HNdouble);
                }
                //else if (th.ErrorCode() > 0)
                //{
                //    Growl.Error("getData เกิดข้อผิดพลาด");
                //    //MessageBox.Show(th.Error());
                //}
                else
                {
                    // MessageBox.Show("Catch All", "รายงานความผิดพลาด", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                Growl.Error("getData say:" + ex.Message);
            }
        }
        private void getDataappoint()//หลับดึงข้อมูลจากบัตรประชาชน
        {
            try
            {
                if (!(personalCard == null))//ถ้าเสียบบัตรประชาชนให้ดึงข้อมูลจากบัตรมาแสดงในฟอร์มส่วนข้อมูลจากบัตร
                {
                    
                    this.txtIDCard.Text = personalCard.Citizenid.ToString();
                    //   this.txtIDCard.Text = Personal.Citizenid;
                    this.txtnhsdata_cid.Text = personalCard.Citizenid.ToString();
                    this.txtPrefixThai.Text = personalCard.Th_Prefix.ToString();
                    this.txtFirstNameThai.Text = personalCard.Th_Firstname.ToString();
                    this.txtPrefixEng.Text = personalCard.En_Prefix.ToString();
                    this.txtLastnameThai.Text = personalCard.Th_Lastname.ToString();
                    this.txtFirstNameEng.Text = personalCard.En_Firstname.ToString();
                    this.txtLastnameEng.Text = personalCard.En_Lastname.ToString();
                    //  txtBirthDate.Text = Strings.Format(Conversions.ToDate(Personal.Birthday.ToString), "dd/MM/yyyy");
                    //ตรวจสอบข้อมูลจากบัตรประชาชน วันเดือนปีเกิด หากไม่สมบูรณ์ให้แสดงค่าว่างออกไป
                    try
                    {
                        //this.txtBirthDate.Text = HI7.Class.HIUility.DateChange4(personalCard.Birthday.ToString("dd/MM/yyyy"));
                        this.txtBirthDate.Text = personalCard.Birthday.ToString("dd/MM/yyyy");
                    }
                    catch (Exception ex)
                    {
                        this.txtBirthDate.Text = "";
                        Growl.Warning("วันและเดือนไม่พบในจ้อมูลบัตรประชาชน\r\nระบบจะทำการดึงข้อมูลจาก สปสช.ในการบันทึก\r\n(กรุณาสอบถามผู้มารับบริการอีกครั้งเพื่อความถูกต้อง)");
                    }
                    this.intReadCard_Sex = int.Parse(personalCard.Sex);
                    this.txtSex.Text = this.CheckSex(int.Parse(personalCard.Sex), this.txtSex, "th").ToString();
                    this.txtsexeng.Text = this.CheckSex(int.Parse(personalCard.Sex), this.txtSex, "en").ToString();
                    this.txtIssueDate.Text = personalCard.Issue.ToString("dd/MM/yyyy");
                    this.txtExpireDate.Text = personalCard.Expire.ToString("dd/MM/yyyy");
                    this.txtHouseNo.Text = personalCard.addrHouseNo.ToString();
                    this.txtVillageNo.Text = personalCard.addrVillageNo.ToString();
                    this.txtSub_district.Text = personalCard.addrTambol.ToString();
                    this.txtDistrict.Text = personalCard.addrAmphur.ToString();
                    this.txtProvince.Text = personalCard.addrProvince.ToString();
                    this.txtissuecard.Text = personalCard.Issuer.ToString();
                    //try
                    //{
                    //    this.txtpt_dob.Text = personalCard.Birthday.ToString("dd/MM/yyyy");
                    //}
                    //catch (Exception ex)
                    //{
                    //}
                    // // รูปภาพ
                    this.BitmapToMemory(personalCard.PhotoBitmap);
                    if (this.txtpt_hn.Text != null && this.txtpt_hn.Text != "")
                    {
                        this.UpdateimagePT(this.txtIDCard.Text);
                    }
                    this.BitmapToBitmapImage();
                    string cid = personalCard.Citizenid.ToString();
                    try
                    {
                        if (personalCard.Birthday != null)
                        {
                            DateTime dob = Convert.ToDateTime(personalCard.Birthday);
                            DateTime PresentYear = DateTime.Now;
                            TimeSpan ts = PresentYear - dob;
                            DateTime Age = DateTime.MinValue.AddDays(ts.Days);
                            string fullAge = (string.Format(" {0} ปี {1} เดือน {2} วัน", Age.Year - 1, Age.Month - 1, Age.Day - 2));
                            this.txtAgenow.Text = fullAge;//บันทึก
                        }
                        else
                        {

                        }
                    }
                    catch (Exception ex)
                    {

                    }
                    
                }
                //else if (th.ErrorCode() > 0)
                //{
                //    Growl.Error("getDataappoint เกิดข้อผิดพลาด");
                //}
                else
                {
                    // MessageBox.Show("Catch All", "รายงานความผิดพลาด", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                Growl.Error("getDataappoint say:" + ex.Message);
            }
        }


        void BitmapToMemory(System.Drawing.Image imageToConvert)
        {
            using (var ms = new MemoryStream())
            {
                Bitmap bmp = new Bitmap(imageToConvert);
                bmp.Save(ms, ImageFormat.Jpeg);
                using (Image img = Image.FromStream(ms, true))
                {
                    //  img.Save(satelliteFolderImagesDownload + "\\image" + satCounter + ".gif", System.Drawing.Imaging.ImageFormat.Gif);
                    img.Save(Hi7.Class.APIConnect.PATHPICPT + this.txtIDCard.Text+".jpg");

                      base64String = Convert.ToBase64String(ms.ToArray());
                 //   addimage(base64String);

                }
             //   return ms.ToArray();
            }
        }
        string base64String;
        void addimage( ) {
             Dictionary<string, object> dictData = new Dictionary<string, object>();
            string strSQL ="insert into hi7ptimage(hn,cid_image) values("+this.txtpt_hn.Text+",'"+base64String+"') ";

            dictData.Add("query", strSQL);


             Hi7.Class.APIConnect.Excecute("query/hi7_query", "POST", dictData);
 


        }
        void BitmapToBitmapImageNotpicture()
        {
            try
            {

                BitmapImage bitmapImage = new BitmapImage();
                bitmapImage.BeginInit();
                bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
                bitmapImage.UriSource = new Uri(Hi7.Class.APIConnect.PATHPICPT + "0000000000000" + ".jpg", UriKind.RelativeOrAbsolute);
                bitmapImage.EndInit();
                bitmapImage.Freeze();
                this.imgPerson.Source = bitmapImage; // <== //display image
            }
            catch { }
        }
        void BitmapToBitmapImageWait()
        {
            BitmapImage bitmapImage = new BitmapImage();
            bitmapImage.BeginInit();
            bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
            bitmapImage.UriSource = new Uri(Hi7.Class.APIConnect.PATHPICPT + "spin" + ".gif", UriKind.RelativeOrAbsolute);
            bitmapImage.EndInit();
            bitmapImage.Freeze();
            this.imgPerson.Source = bitmapImage; // <== //display image

        }
        void  BitmapToBitmapImage()
        {
            BitmapImage bitmapImage = new BitmapImage();
            bitmapImage.BeginInit();
            bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
            bitmapImage.UriSource = new Uri(Hi7.Class.APIConnect.PATHPICPT + txtIDCard.Text + ".jpg", UriKind.RelativeOrAbsolute);
            bitmapImage.EndInit();
            bitmapImage.Freeze();
            this.imgPerson.Source = bitmapImage; // <== //display image
        }
        void BitmapToBitmapImageGetpt()
        {
            try
            {
                BitmapImage bitmapImage = new BitmapImage();
                bitmapImage.BeginInit();
                bitmapImage.CacheOption = BitmapCacheOption.OnLoad;

                if (txtpt_cid.Text == null || txtpt_cid.Text == "")
                {
                    bitmapImage.UriSource = new Uri(Hi7.Class.APIConnect.PATHPICPT + "0000000000000" + ".jpg", UriKind.RelativeOrAbsolute);
                }
                else
                {
                    bitmapImage.UriSource = new Uri(Hi7.Class.APIConnect.PATHPICPT + txtpt_cid.Text + ".jpg", UriKind.RelativeOrAbsolute);
                }

                bitmapImage.EndInit();
                bitmapImage.Freeze();
                this.imgPerson.Source = bitmapImage; // <== //display image
            }
            catch (Exception ex) {
                BitmapImage bitmapImage = new BitmapImage();
                bitmapImage.BeginInit();
                bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
                bitmapImage.UriSource = new Uri(Hi7.Class.APIConnect.PATHPICPT + "0000000000000" + ".jpg", UriKind.RelativeOrAbsolute);               
                bitmapImage.EndInit();
                bitmapImage.Freeze();
                this.imgPerson.Source = bitmapImage;
            }
            
        }
        void LoadImage()
        {
            DataTable dt = new DataTable();
            DataRow dr;
            Dictionary<string, object> dictData = new Dictionary<string, object>();
            string strSQL = "select * from hi7ptimage where hn='"+this.txtpt_hn.Text+"'";
            dictData.Add("query", strSQL);
            try
            {
                dt = Hi7.Class.APIConnect.getDataTable("query/hi7_query", "POST", dictData);

                if ( dt != null )
                {
                    dr = dt.Rows[0];
                    byte[] bytes = Convert.FromBase64String(dr["cid_image"].ToString());

                    Image image;
                    using ( MemoryStream ms = new MemoryStream(bytes) )
                    {
                      //  image = Image.FromStream(ms);
                        // ms.Save("C:\\PT_IMG\\" + this.txtIDCard.Text + ".jpg");
                        using ( Image img = Image.FromStream(ms, true) )
                        {
                            //  img.Save(satelliteFolderImagesDownload + "\\image" + satCounter + ".gif", System.Drawing.Imaging.ImageFormat.Gif);
                            img.Save(Hi7.Class.APIConnect.PATHPICPT + this.txtIDCard.Text + ".jpg");

                          //  base64String = Convert.ToBase64String(ms.ToArray());
                            //   addimage(base64String);

                        }
                    }
                }

            }catch(Exception ex )
            {


            }

          //  return image;
        }
        public string CheckSex(int sex, System.Windows.Controls.TextBox txt,string sex_type)
        {
            string CheckSexRet;
            // // เก็บค่าตัวเลขไว้ใน Tag เผื่อไว้ตอนไปเก็บลงในฐานข้อมูล [กำหนดให้ ชาย=1 หญิง=2]
            txt.Tag = sex;
            if (sex == 1)
            {
                if (sex_type == "th") {
                    CheckSexRet = "ชาย";
                } else {
                   CheckSexRet = "Male";
                } 
            }
            else
            {
                if (sex_type == "th")
                {
                    CheckSexRet = "หญิง";
                }
                else
                {
                    CheckSexRet = "Female";
                   
                }
            
            }

            return CheckSexRet;
        }
        int GetIndexes(DataTable dt)
        {
            // Get the DataSet of a DataGrid.
            DataSet thisDataSet = new DataSet();
            thisDataSet.Tables.Add(dt);
            // Get the DataTableCollection through the Tables property.
            DataTableCollection tables = thisDataSet.Tables;
            // Get the index of the table named "Authors", if it exists.
            if (tables.Contains("สระบุรี"))
            {
                 return tables.IndexOf("สระบุรี");
            }
            return 0;
                //  System.Diagnostics.Debug.WriteLine(tables.IndexOf("สระบุรี"));
        }
        private void btnReaderCard_Click(object sender, RoutedEventArgs e)
        {
            this.ClearData();
            this.txtsearch.Text = "";
            this.gifsmc.Visibility = Visibility.Visible;
            if (!worker.IsBusy)
                worker.RunWorkerAsync();
            else
                MessageBox.Show("Can't run the worker twice!");
        }
        private void worker_DoWork(object sender, DoWorkEventArgs e)
        {
            System.Text.EncodingProvider provider = System.Text.CodePagesEncodingProvider.Instance;// แปลง encode tis-620
            Encoding.RegisterProvider(provider);
            var th = new ThaiIDCard();
            Personal Personal = th.readAllPhoto();//อ่านภาพ
            personalCard = Personal;
            if (personalCard != null)
            {
                _CIDREADER = personalCard.Citizenid.ToString();
                
                Dispatcher.Invoke(() =>
                {
                    GetA();
                });
            }
            else {
                Growl.Warning("กรุณาตรวจสอบบัตรประชาชน!!");
            }
            
        }
        private void GetA() {
            this.cbbpt_queuetype.Text = "ปกติ";
            string cid = "";
            DataTable dt;
            DataRow dr;
            try
            {
                if (personalCard != null)
                {
                    cid = personalCard.Citizenid.ToString();//ดึงหมายเลขบัตรประชาชนจากบัครประชาชน
                    dt = HI7.Class.HIUility.getHn(cid);//หาเลข Hn จากเลขบัตรประชาชน
                    if (dt != null)//ถ้ามีข้อมูล
                    {
                        this.txtCheckstatus.Text = "รายเก่า";
                        if (dt.Rows.Count == 1)//ถ้ามีข้อมูล 1 แถว
                        {
                            dr = dt.Rows[0];
                            string hn = dr["hn"].ToString();
                            if (!String.IsNullOrEmpty(hn))
                            {
                                if (GetDadehn(hn) == true)
                                {
                                    Growl.Warning("ผู้มารับบริการได้เสียชีวิตแล้ว\r\nไม่สามารถลงทะเบียนได้");
                                }
                                else { }
                                if (GetAdmithn(hn) == true)
                                {
                                    Growl.Warning("ผู้มารับบริการสถานะยังไม่ได้ Discharge\r\nไม่สามารถลงทะเบียนได้");   
                                }
                                else { }
                                if (GetReferin(hn) == true)
                                {
                                    Growl.Warning("ผู้มารับบริการสถานะ Referin เข้ามา\r\nการุณาตรวจสอบ"); 
                                }
                                else
                                {

                                }
                                if (GetRegistertodayhn(hn) == true)
                                {
                                    if (MessageBox.Show("วันนี้มีการเข้ารับริการแล้ว\r\nตรวจสอบใน Tab การตารางผู้ลงทะเบียน", "เตือน HN:" + hn + " มีการลงทะเบียนแล้ว", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.No)
                                    {
                                        this.getVisit();
                                        return;
                                    }
                                    else
                                    {
                                        if (Getcheck24hr(hn) == true)
                                        {
                                            string messageBoxText = "ไม่สามารถลงทะเบียนได้ เนื่องจากยังไม่ครบ 4 ชั่วโมงในกลุ่มสิทธิ์(อปท.)\r\n หากยืนยันการลงทะเบียนกด Yes หากไม่กดดำเนินการกด No";
                                            string caption = "ดำเนินการต่อไป";
                                            MessageBoxButton button = MessageBoxButton.YesNo;
                                            MessageBoxImage image = MessageBoxImage.Warning;
                                            MessageBoxResult result = MessageBox.Show(messageBoxText, caption, button, image);
                                            if (result == MessageBoxResult.Yes)
                                            {
                                                Growl.Warning("จะมีผลต่อการเครมตามเกณฑ์ต้องเข้ารับบริการหลัง 4 ชั่วโมงของสิทธิ์\r\nอปท.และจ่ายตรงกรมบัญชีกลาง ในการเครมด้วยสิทธิ์ LGO/OFC");
                                            }
                                            else
                                            {
                                                return;
                                            }
                                        }
                                    }
                                }
                                else { }                                                          
                            }
                            DataTable dtappoint;
                            dtappoint = HI7.Class.HIUility.getAppointment(hn);//หาการนัดจาก hn
                            if (dtappoint != null && dtappoint.Rows.Count > 0)//ถ้า มีข้อมูลในตารางนัด
                            {
                                //เช็คนัด
                                checkAppoint(hn);//เรียกฟอร์มนัด
                                getDataappoint();//แสดงข้อมูลจากบัตรประชาชนในฟอร์มส่วนบัตร                                
                                this.nhso_smartcard_readpttype();//เช็คสิทธิ์แสดงในส่วน GroupBoxแสดงสิทธ์
                                getPatientInfoappoint("hn", hn);//แสดงข้อมูลส่วนบุคคลผู้มารับบริการและขอ Authencode
                                this.cbbpt_claimtype.SelectedIndex = 0;//กดหนด Defalut
                                Keyboard.Focus(this.tabmdr);
                                this.txtsearchfname.Text = "";
                                btnUpdateCard.Visibility = Visibility.Visible;
                                this.cbbpt_queuetype.Text = "ปกติ";
                            }
                            else//ไม่มีข้อมูลนัด
                            {
                                getData();//ดึงข้อมูลส่วนบุคคล
                                this.nhso_smartcard_readpttype();//เช็คสิทธิ์แสดงในส่วน GroupBoxแสดงสิทธ์
                                this.cbbpt_claimtype.SelectedIndex = 0;//กดหนด Defalut
                                Keyboard.Focus(this.tabmdr);
                                btnUpdateCard.Visibility = Visibility.Visible;
                                this.cbbpt_queuetype.Text = "ปกติ";
                            }
                        }
                        else if (dt.Rows.Count > 1)
                        {
                            Growl.Warning("หมายเลข 13 หลักนี้มี HN มากว่า 1 HN\r\nกรุณาเลือก HN ที่มารับบริการครั้งนี้");
                            frmHndouble frmHndouble = new frmHndouble();
                            frmHndouble.ShowDialog();
                            if (!String.IsNullOrEmpty(_HNdouble))
                            {
                                if (GetDadehn(_HNdouble) == true)
                                {

                                    Growl.Warning("ผู้มารับบริการได้เสียชีวิตแล้ว\r\nไม่สามารถลงทะเบียนได้");
                                    

                                }
                                else
                                {

                                }
                                if (GetAdmithn(_HNdouble) == true)
                                {
                                    Growl.Warning("ผู้มารับบริการสถานะยังไม่ได้ Discharge\r\nไม่สามารถลงทะเบียนได้");
                                    //return;
                                }
                                else
                                {

                                }
                                if (GetRegistertodayhn(_HNdouble) == true)
                                {
                                    if (MessageBox.Show("วันนี้มีการเข้ารับริการแล้ว\r\nตรวจสอบใน Tab การตารางผู้ลงทะเบียน", "เตือน HN:" + _HNdouble + " มีการลงทะเบียนแล้ว", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.No)
                                    {
                                        this.getVisit();
                                        return;
                                    }
                                    else
                                    {
                                        if (Getcheck24hr(_HNdouble) == true)
                                        {
                                            string messageBoxText = "ไม่สามารถลงทะเบียนได้ เนื่องจากยังไม่ครบ 4 ชั่วโมงในกลุ่มสิทธิ์(อปท.)\r\n หากยืนยันการลงทะเบียนกด Yes หากไม่กดดำเนินการกด No";
                                            string caption = "ดำเนินการต่อไป";
                                            MessageBoxButton button = MessageBoxButton.YesNo;
                                            MessageBoxImage image = MessageBoxImage.Warning;
                                            MessageBoxResult result = MessageBox.Show(messageBoxText, caption, button, image);
                                            if (result == MessageBoxResult.Yes)
                                            {
                                                Growl.Warning("จะมีผลต่อการเครมตามเกณฑ์ต้องเข้ารับบริการหลัง 4 ชั่วโมงของสิทธิ์\r\nอปท.และจ่ายตรงกรมบัญชีกลาง ในการเครมด้วยสิทธิ์ LGO/OFC");
                                            }
                                            else
                                            {
                                                return;
                                            }
                                        }
                                    }
                                }
                                else
                                {

                                }
                            }
                            if (!String.IsNullOrEmpty(_HNdouble)) {
                                DataTable dtappoint;
                                dtappoint = HI7.Class.HIUility.getAppointment(_HNdouble);//หาการนัดจาก hn
                                if (dtappoint != null && dtappoint.Rows.Count > 0)//ถ้า มีข้อมูลในตารางนัด
                                {
                                    //เช็คนัด
                                    checkAppoint(_HNdouble);//เรียกฟอร์มนัด
                                    getDataappoint();//แสดงข้อมูลจากบัตรประชาชนในฟอร์มส่วนบัตร
                                    
                                    this.nhso_smartcard_readpttype();//เช็คสิทธิ์แสดงในส่วน GroupBoxแสดงสิทธ์
                                    getPatientInfoappoint("hn", _HNdouble);//แสดงข้อมูลส่วนบุคคลผู้มารับบริการและขอ Authencode
                                    this.cbbpt_claimtype.SelectedIndex = 0;//กดหนด Defalut
                                    Keyboard.Focus(this.tabmdr);
                                    this.txtsearchfname.Text = "";
                                    btnUpdateCard.Visibility = Visibility.Visible;
                                    this.cbbpt_queuetype.Text = "ปกติ";
                                }
                                else//ไม่มีข้อมูลนัด
                                {
                                    if (!String.IsNullOrEmpty(_HNdouble))
                                    {
                                        //this.getDataAppoint();//ถ้าเลือกมาจากหน้า 1 คนมี HN มากกว่า 1
                                        this.getData();
                                        this.nhso_smartcard_readpttype();//เช็คสิทธิ์แสดงในส่วน GroupBoxแสดงสิทธ์
                                        this.cbbpt_claimtype.SelectedIndex = 0;//กดหนด Defalut
                                        Keyboard.Focus(this.tabmdr);
                                        btnUpdateCard.Visibility = Visibility.Visible;
                                        this.cbbpt_queuetype.Text = "ปกติ";
                                    }
                                    else
                                    {
                                        this.getData();//ดึงข้อมูลส่วนบุคคล
                                        this.nhso_smartcard_readpttype();//เช็คสิทธิ์แสดงในส่วน GroupBoxแสดงสิทธ์
                                        this.cbbpt_claimtype.SelectedIndex = 0;//กดหนด Defalut
                                        Keyboard.Focus(this.tabmdr);
                                        btnUpdateCard.Visibility = Visibility.Visible;
                                        this.cbbpt_queuetype.Text = "ปกติ";
                                    }
                                    
                                }
                            }
                            else { 
                            }
                            
                        }
                        else//ไม่เจอ 13 หลักใน PT
                        {
                            try
                            {
                                if (!(personalCard == null))
                                {

                                    getData();
                                    if (!string.IsNullOrEmpty(txtBirthDate.Text))
                                    {
                                        txtpt_dob.Text = txtBirthDate.Text;
                                        //txtPtAgenow.Text = txtAgenow.Text;
                                    }
                                    else
                                    {
                                        this.UcwsnhsoCIDBirthDay(this.txtIDCard.Text);
                                    }
                                    //this.intReadCard_Sex = int.Parse(personalCard.Sex);
                                    //this.txtSex.Text = this.CheckSex(int.Parse(personalCard.Sex), this.txtSex, "th").ToString();
                                    //this.txtsexeng.Text = this.CheckSex(int.Parse(personalCard.Sex), this.txtSex, "en").ToString();
                                    //this.txtIssueDate.Text = HI7.Class.HIUility.DateChange4(personalCard.Issue.ToString("dd/MM/yyyy"));
                                    //this.txtExpireDate.Text = HI7.Class.HIUility.DateChange4(personalCard.Expire.ToString("dd/MM/yyyy"));
                                    //this.txtHouseNo.Text = personalCard.addrHouseNo.ToString();
                                    //this.txtVillageNo.Text = personalCard.addrVillageNo.ToString();
                                    //this.txtSub_district.Text = personalCard.addrTambol.ToString();
                                    //this.txtDistrict.Text = personalCard.addrAmphur.ToString();
                                    //this.txtProvince.Text = personalCard.addrProvince.ToString();
                                    //this.txtissuecard.Text = personalCard.Issuer.ToString();                                    
                                    // // รูปภาพ
                                    this.BitmapToMemory(personalCard.PhotoBitmap);
                                    if (this.txtpt_hn.Text != null && this.txtpt_hn.Text != "")
                                    {
                                        this.UpdateimagePT(this.txtIDCard.Text);
                                    }
                                    this.BitmapToBitmapImage();

                                    //string cid = personalCard.Citizenid.ToString();                                    
                                    //this.getPatientInfo("cid", cid);

                                    //this.cbbpt_claimtype.SelectedIndex = 0;//กดหนด Defalut
                                    //this.getdata_newPatient();
                                    //this.txtCheckstatus.Text = "รายใหม่";
                                    //this.cbbpt_claimtype.SelectedIndex = 0;
                                    //this.cbbareatype.SelectedIndex = 4;
                                    //this.cbbpt_queuetype.Text = "ปกติ";
                                    //Growl.Warning("คนไข้รายใหม่");
                                    this.nhso_smartcard_readpttype();
                                    Keyboard.Focus(this.tabmdr);
                                }
                                else
                                {
                                }
                            }
                            catch { 
                            }
                        }
                    }
                    else
                    {
                        Growl.Warning("error getHn");
                    }
                }
                else
                {
                    Growl.Warning("กรุณาตรวจสอบบัตรประชาชน!!");
                }
            }
            catch (Exception ex)
            {
                Growl.Warning("worker_RunWorkerCompleted" + ex.Message);
            }
        }
        bool GetAdmithn(string hn)
        {
            string strSql = "select ipt.*,idpm.nameidpm from hi.ipt LEFT JOIN hi.idpm on ipt.ward = idpm.idpm where ipt.dchdate = '0000-00-00' and YEAR(ipt.rgtdate) = YEAR(NOW()) and ipt.hn = '"+ hn + "' order by vn desc limit 1";
            DataRow dr;
            DataTable dt = new System.Data.DataTable();
            Dictionary<string, object> dictData = new Dictionary<string, object>();
            dictData.Add("query", strSql);
            try
            {
                dt = Hi7.Class.APIConnect.getDataTable("query/hi7_query", "POST", dictData);
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                return false;
            }
            return true;
        }
        bool GetReferin(string hn)
        {
            string strSql = "SELECT o.vn,o.hn,DATE_FORMAT(o.vstdttm,'%d-%m-%Y') as vstdttm,r.rfrlct,h.namehosp,r.rfrcs,rf.namerfrcs,r.rfrno as crfrno,r.accdate,concat('(',r.icd10,')',i.icd10name) as icd10name, lr.nametype" +  
                            " from hi.ovst as o"+
                            " INNER JOIN hi.orfri as r on o.vn = r.vn"+
                            " INNER JOIN hi.hospcode as h on r.rfrlct = h.off_id"+
                            " LEFT JOIN hi.icd101 as i on r.icd10 = i.icd10"+
                            " LEFT JOIN hi.rfrcs as rf on rf.rfrcs = r.rfrcs" +
                            " LEFT JOIN hi.l_rfrtype as lr on lr.rfrtype = r.rfrtype" +
                            " where o.hn = '" +hn+ "' ORDER BY o.vstdttm DESC LIMIT 5 ";
            DataRow dr;
            DataTable dt = new System.Data.DataTable();
            Dictionary<string, object> dictData = new Dictionary<string, object>();
            dictData.Add("query", strSql);
            try
            {
                dt = Hi7.Class.APIConnect.getDataTable("query/hi7_query", "POST", dictData);
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        Forms.frmPopupCheckReferIn.dt = dt;
                        /*frmPopupCheckReferIn.dt = dt*/;
                        frmPopupCheckReferIn frmPopupCheckReferIn = new frmPopupCheckReferIn();
                        frmPopupCheckReferIn.ShowDialog();
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                return false;
            }
            return true;
        }
        //ตรวจสอบ referin ของวันนี้
        bool GetReferintoday(string hn)
        {
            string dateserver = HI7.Class.HIUility.Getdateserver();
            string ovstdate = HI7.Class.HIUility.CBE2D(dateserver);            

            string strSql = "SELECT o.vn,o.hn,DATE_FORMAT(o.vstdttm,'%d-%m-%Y') as vstdttm,r.rfrlct,h.namehosp,r.rfrcs,rf.namerfrcs,r.rfrno as crfrno,r.accdate,concat('(',r.icd10,')',i.icd10name) as icd10name, lr.nametype" +
                            " from hi.ovst as o" +
                            " INNER JOIN hi.orfri as r on o.vn = r.vn" +
                            " INNER JOIN hi.hospcode as h on r.rfrlct = h.off_id" +
                            " LEFT JOIN hi.icd101 as i on r.icd10 = i.icd10" +
                            " LEFT JOIN hi.rfrcs as rf on rf.rfrcs = r.rfrcs" +
                            " LEFT JOIN hi.l_rfrtype as lr on lr.rfrtype = r.rfrtype" +
                            " where o.hn = '" + hn + "' AND date(o.vstdttm) ='"+ ovstdate + "' ORDER BY o.vstdttm DESC";
            DataRow dr;
            DataTable dt = new System.Data.DataTable();
            Dictionary<string, object> dictData = new Dictionary<string, object>();
            dictData.Add("query", strSql);
            try
            {
                dt = Hi7.Class.APIConnect.getDataTable("query/hi7_query", "POST", dictData);
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        Forms.frmPopupCheckReferIn.dt = dt;                 
                        
                       return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                return false;
            }
            return true;
        }
        bool GetDadehn(string hn)
        {
            string strSql = "select DATE_FORMAT(pt.dthdate,'%Y-%m-%d') as dthdate from pt where hn = '" + hn + "'";
            DataRow dr;
            DataTable dt = new System.Data.DataTable();
            Dictionary<string, object> dictData = new Dictionary<string, object>();
            dictData.Add("query", strSql);

            try
            {
                dt = Hi7.Class.APIConnect.getDataTable("query/hi7_query", "POST", dictData);
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        dr = dt.Rows[0];
                        string dthdate = (string)IsNullString(dr["dthdate"]).ToString();
                        if (dthdate != "0000-00-00")
                        {
                            return true;
                        }
                        else
                        {
                            return false;
                        }

                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                return false;
            }
            return true;
        }
        bool GetRegistertodayhn(string hn)
        {
            if (!string.IsNullOrEmpty(hn))
            {
                string strSql = "SELECT * FROM ovst WHERE ovst.hn = '" + hn + "' and date(ovst.vstdttm) = CURRENT_DATE() limit 1";
                DataRow dr;
                DataTable dt = new System.Data.DataTable();
                Dictionary<string, object> dictData = new Dictionary<string, object>();
                dictData.Add("query", strSql);

                try
                {
                    dt = Hi7.Class.APIConnect.getDataTable("query/hi7_query", "POST", dictData);
                    if (dt != null)
                    {
                        if (dt.Rows.Count > 0)
                        {

                            return true;
                        }
                        else
                        {
                            return false;
                        }
                    }
                    else
                    {
                        return false;
                    }
                }
                catch (Exception ex)
                {
                    return false;
                }
                return true;
            }
            else
            {
                return false;
            }
            
        }
        bool Getcheck24hr(string hn)
        {
            string strSql = "SELECT o.hn,o.vn,o.register,f.fname,o.cln,c.namecln,o.vstdttm,o.pttype,s.namepttype,(o.vstdttm + INTERVAL 4 HOUR) as d_update,"+
                            "if (now() >= (o.vstdttm + INTERVAL 4 HOUR),0,1) as chk4hr"+
                            " from hi.ovst as o"+
                            " INNER JOIN hi.pttype as s on o.pttype = s.pttype  and s.inscl in ('LGO','OFC')"+
                            " INNER JOIN hi.cln as c on o.cln = c.cln"+
                            " INNER JOIN hi.opstaff as f on o.register = f.staff"+
                            " where o.hn = '"+hn+"' ORDER BY o.vn desc limit 1";
            DataRow dr;
            DataTable dt = new System.Data.DataTable();
            Dictionary<string, object> dictData = new Dictionary<string, object>();
            dictData.Add("query", strSql);

            try
            {
                dt = Hi7.Class.APIConnect.getDataTable("query/hi7_query", "POST", dictData);
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {

                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                return false;
            }
            return true;
        }
        private void worker_RunWorkerCompleted(object sender,RunWorkerCompletedEventArgs e)
        {
            //update ui once worker complete his work
            //Growl.Success("success");
            this.gifsmc.Visibility = Visibility.Collapsed;
            Keyboard.Focus(this.txtcbbpttype);
        }
        void getVillage()
        {
            DataTable dt = new DataTable();         
            Dictionary<string, object> dictData = new Dictionary<string, object>();
            //dictData.Add("limit", "100");
            //dictData.Add("orderby", "chwpart");
            string strSQL = "select  namemoob,moopart  from mooban where chwpart='" + strProvince_code + "' and amppart='"+strAmphur_code+ "' and tmbpart='"+strTumbon_code+"'   order by namemoob";
            dictData.Add("query", strSQL);
            try
            {
                dt = Hi7.Class.APIConnect.getDataTable("query/hi7_query", "POST", dictData);
                //  dt = Hi7.Class.APIConnect.getDataTable("/lookup/village/list", "POST", dictData);
                if (dt != null)
                {
                    string[] selectedColumns = new[] { "namemoob", "moopart" };
                    System.Data.DataView view = new System.Data.DataView(dt);
                    // Create a new DataTable from the DataView with just the columns desired - and in the order desired
                    System.Data.DataTable selected = view.ToTable("Selected", false, selectedColumns);

                    this.cbbVillage.DisplayMemberPath = "namemoob";
                    this.cbbVillage.SelectedValuePath = "moopart";
                    this.cbbVillage.ItemsSource = selected.DefaultView;
                    string c = strVillage_code;
                    DataRow[] result = selected.Select("moopart ='" + c + "'");

                    if (result.Length > 0)
                    {
                        int SelectedIndex = selected.Rows.IndexOf(result[0]);
                        this.cbbVillage.SelectedIndex = SelectedIndex;
                        
                    }

                }

            }
            catch (Exception ex)
            {

            }

        }

        void getTumbon()
        {
            DataTable dt = new DataTable();

       //     strAmphur_code = this.cbbAmphur.SelectedValue.ToString();
            Dictionary<string, object> dictData = new Dictionary<string, object>();
             string strSQL = "select nametumb,tmbpart from tumbon where chwpart='" + strProvince_code + "' and amppart='"+strAmphur_code+ "'    order by nametumb";
            dictData.Add("query", strSQL);

            try
            {


                dt = Hi7.Class.APIConnect.getDataTable("query/hi7_query", "POST", dictData);
             //   dt = Hi7.Class.APIConnect.getDataTable("/lookup/tambon/list", "POST", dictData);
                if (dt != null)
                {

                    string[] selectedColumns = new[] { "nametumb", "tmbpart" };
                    System.Data.DataView view = new System.Data.DataView(dt);
                    // Create a new DataTable from the DataView with just the columns desired - and in the order desired
                    System.Data.DataTable selected = view.ToTable("Selected", false, selectedColumns);

                    this.cbbTumbon.DisplayMemberPath = "nametumb";
                    this.cbbTumbon.SelectedValuePath = "tmbpart";
                    this.cbbTumbon.ItemsSource = selected.DefaultView;
                    string c =  strTumbon_code;
                    DataRow[] result = selected.Select("tmbpart ='" + c + "'");

                    if (result.Length > 0)
                    {
                        int SelectedIndex = selected.Rows.IndexOf(result[0]);
                        this.cbbTumbon.SelectedIndex = SelectedIndex;
                     
                    }

                }

            }
            catch (Exception ex)
            {

            }

        }

        void getAmphur()
        {
            DataTable dt = new DataTable();
            Dictionary<string, object> dictData = new Dictionary<string, object>();
            if (string.IsNullOrEmpty(strProvince_code) == false)
            {
                //   dictData.Add("limit", "100");
                //   dictData.Add("query", "select CONCAT(chwpart,amppart) AS amppart_code,nameampur from ampur where chwpart='" + strProvince_code+"' order by nameampur");
                dictData.Add("query", "select amppart AS amppart_code,nameampur from ampur where chwpart='" + strProvince_code + "' order by nameampur");
                try
                {
                    dt = Hi7.Class.APIConnect.getDataTable("query/hi7_query", "POST", dictData);
                    //    dt = Hi7.Class.APIConnect.getDataTable("/lookup/ampur/list", "POST", dictData);
                    if (dt != null)
                    {
                        string[] selectedColumns = new[] { "nameampur", "amppart_code" };
                        System.Data.DataView view = new System.Data.DataView(dt);
                        // Create a new DataTable from the DataView with just the columns desired - and in the order desired
                        System.Data.DataTable selected = view.ToTable("Selected", false, selectedColumns);
                        this.cbbAmphur.DisplayMemberPath = "nameampur";
                        this.cbbAmphur.SelectedValuePath = "amppart_code";
                        this.cbbAmphur.ItemsSource = selected.DefaultView;
                        //   string c = strProvince_code +strAmphur_code;
                        string c = strAmphur_code;
                        DataRow[] result = selected.Select("amppart_code =" + c);
                        if (result.Length > 0)
                        {
                            int SelectedIndex = selected.Rows.IndexOf(result[0]);
                            this.cbbAmphur.SelectedIndex = SelectedIndex;
                        }
                    }
                }
                catch (Exception ex)
                {

                }
            }
            else {
                dictData.Add("query", "select amppart AS amppart_code,nameampur from ampur order by nameampur");
                if (strAmphur_code != "")
                {
                }
                try
                {
                    dt = Hi7.Class.APIConnect.getDataTable("query/hi7_query", "POST", dictData);
                    if (dt != null)
                    {
                        string[] selectedColumns = new[] { "nameampur", "amppart_code" };
                        System.Data.DataView view = new System.Data.DataView(dt);
                        System.Data.DataTable selected = view.ToTable("Selected", false, selectedColumns);
                        this.cbbAmphur.DisplayMemberPath = "nameampur";
                        this.cbbAmphur.SelectedValuePath = "amppart_code";
                        this.cbbAmphur.ItemsSource = selected.DefaultView;
                        DataRow[] result = selected.Select("amppart_code =" + strAmphur_code);
                        if (result.Length > 0)
                        {
                            int SelectedIndex = selected.Rows.IndexOf(result[0]);
                            this.cbbAmphur.SelectedIndex = SelectedIndex;
                        }
                    }
                }
                catch (Exception ex)
                {

                }

            }

        }

        void getProvince() {
            DataTable dt = new DataTable();
            Dictionary<string, object> dictData = new Dictionary<string, object>();
            //dictData.Add("limit", "100");
            //dictData.Add("orderby", "chwpart");
            dictData.Add("query", "select * from changwat ORDER BY namechw");
            if (strProvince_code != "") {
            }
            try
            {
                dt = Hi7.Class.APIConnect.getDataTable("query/hi7_query", "POST", dictData);
                if (dt != null)
                {
                     string[] selectedColumns = new[] { "namechw", "chwpart" };
                     System.Data.DataView view = new System.Data.DataView(dt);
                     System.Data.DataTable selected = view.ToTable("Selected", false, selectedColumns);     
                     this.cbbChanwat.DisplayMemberPath = "namechw";
                     this.cbbChanwat.SelectedValuePath = "chwpart";
                     this.cbbChanwat.ItemsSource = selected.DefaultView;                 
                    DataRow[] result = selected.Select("chwpart ="+ strProvince_code);                 
                    if (result.Length > 0)
                    {
                        int SelectedIndex = selected.Rows.IndexOf(result[0]);
                        this.cbbChanwat.SelectedIndex = SelectedIndex;                 
                    }
                }
            }
            catch (Exception ex)
            {
                
            }

            }


        void getClinic()
        {
            DataTable dt = new DataTable();

            Dictionary<string, object> dictData = new Dictionary<string, object>();
            string strSQL = "select cln,concat('(',cln,')',namecln) AS 'namecln' from cln order by cln ASC";
            dictData.Add("query", strSQL);
            try
            {
                dt = Hi7.Class.APIConnect.getDataTable("query/hi7_query", "POST", dictData);
                if (dt != null)
                {
                    string[] selectedColumns = new[] { "namecln", "cln" };
                    System.Data.DataView view = new System.Data.DataView(dt);
                    // Create a new DataTable from the DataView with just the columns desired - and in the order desired
                    System.Data.DataTable selected = view.ToTable("Selected", false, selectedColumns);

                    this.cbbClinic.DisplayMemberPath = "namecln";
                    this.cbbClinic.SelectedValuePath = "cln";
                    this.cbbClinic.ItemsSource = selected.DefaultView;
                   /* string c = strTumbon_code;
                    DataRow[] result = selected.Select("village_code_full ='" + c + "'");

                    if (result.Length > 0)
                    {
                        int SelectedIndex = selected.Rows.IndexOf(result[0]);
                        this.cbbClinic.SelectedIndex = SelectedIndex;
                    }*/

                }

            }
            catch (Exception ex)
            {

            }

        }

        void getPttype()
        {
            DataTable dt = new DataTable();

        
            Dictionary<string, object> dictData = new Dictionary<string, object>();
            string strSQL = "select pttype,concat('(',pttype,')',' ',namepttype) AS 'namepttype' from pttype where pttype.chkshow = '1' order by pttype ASC";
            dictData.Add("query", strSQL);

            try
            {


                dt = Hi7.Class.APIConnect.getDataTable("query/hi7_query", "POST", dictData);

                if (dt != null)
                {
                    string[] selectedColumns = new[] { "namepttype", "pttype" };
                    System.Data.DataView view = new System.Data.DataView(dt);
                    System.Data.DataTable selected = view.ToTable("Selected", false, selectedColumns);
                    this.cbbpttype.DisplayMemberPath = "namepttype";
                    this.cbbpttype.SelectedValuePath = "pttype";
                    this.cbbpttype.ItemsSource = selected.DefaultView;
                    this.cbb_pttype.DisplayMemberPath = "namepttype";
                    this.cbb_pttype.SelectedValuePath = "pttype";
                    this.cbb_pttype.ItemsSource = selected.DefaultView;
                }

            }
            catch (Exception ex)
            {

            }

        }
        //พิมพ์ getway
        void getPrintergetway()
        {
            DataTable dt = HI7.Class.HIUility.getSetup();
            DataRow dr;
            
            if (dt != null)
            {
                if (dt.Rows.Count > 0)
                {
                    dr = dt.Rows[0];
                    //qtyprtq, printerid, smallqueue;
                    HI7.Class.HIUility.qtyprtq = dr["qtyprtq"].ToString();
                    HI7.Class.HIUility.printerid = dr["printerid"].ToString();
                    HI7.Class.HIUility.smallqueue = dr["smallqueue"].ToString();
                }
                else
                {
                    
                }
            }

        }
        //หาสิทธิ์
        private string GetPttype_id(string namepttype)
        {
            DataTable dt = new DataTable();
            Dictionary<string, object> dictData = new Dictionary<string, object>();
            string strSQL = "select pttype from pttype where pttype.namepttype = '"+ namepttype+"' LIMIT 1";
            dictData.Add("query", strSQL);
            try
            {

                DataRow dr;
                dt = Hi7.Class.APIConnect.getDataTable("query/hi7_query", "POST", dictData);

                if (dt != null)
                {                   
                    if (dt.Rows.Count != 0)
                    {
                        dr = dt.Rows[0];
                        return dr["pttype"].ToString();

                    }                    
                    else
                    {                        
                        Growl.Warning("ไม่พบสิทธิ์");
                        return "";
                    }
                }
                else
                {
                    Growl.Warning("ไม่พบสิทธิ์");
                    return "";
                }

            }
            catch (Exception ex)
            {
                Growl.Error("GetPttype_id say:\r\n" + ex.Message);
                return "";
            }
        }
        void getPttypeId(string pttype)
        {
            DataTable dt = new DataTable();
            Dictionary<string, object> dictData = new Dictionary<string, object>();
            string strSQL = "select pttype,concat('(',pttype,')',' ',namepttype) AS 'namepttype' from pttype"+ " where pttype.chkshow = '1' and pttype.pttype = '" + pttype+"' order by pttype ASC";
            dictData.Add("query", strSQL);
            try
            {

                DataRow dr;
                dt = Hi7.Class.APIConnect.getDataTable("query/hi7_query", "POST", dictData);

                if (dt != null)
                {
                    if (dt.Rows.Count == 1)
                    {
                        dr = dt.Rows[0];
                        this.cbbpttype.Text = (string)dr["namepttype"];
                    }
                    else if (dt.Rows.Count > 1)
                    {
                        typeCheckpttype = "0";
                        Growl.Warning("กรุณาเลือกสิทธิ์");
                        HI7.Class.HIUility._TXTSEARCHPTTYPE = pttype;                      
                        frmPopuppttype frmPopuppttype = new frmPopuppttype();
                        Mdr.Forms.frmMdr.txtPttype = pttype;
                        frmPopuppttype.ShowDialog();
                        if (typeCheckpttype == "0")
                        {
                            cbbpttype.Text = dataPttype;
                            txtcbbpttype.Text = idPttype;
                        }
                        else
                        {
                            cbbpttype.Text = dataPttype;
                            txtcbbpttype.Text = idPttype;
                        }
                        
                    }
                    else if (dt.Rows.Count == 0)
                    {
                        Growl.Warning("ไม่พบรายการสิทธิ์ที่ค้นหา");
                        HI7.Class.HIUility._TXTSEARCHPTTYPE = pttype;
                        frmPopuppttype frmPopuppttype = new frmPopuppttype();
                        Mdr.Forms.frmMdr.txtPttype = pttype;
                        frmPopuppttype.ShowDialog();
                        if (typeCheckpttype == "0")
                        {
                            cbbpttype.Text = dataPttype;
                            txtcbbpttype.Text = idPttype;
                        }
                        else
                        {
                            cbbpttype.Text = dataPttype;
                            txtcbbpttype.Text = idPttype;
                        }
                    }
                    else
                    {
                        Growl.Warning("ไม่พบรายการสิทธิ์");
                    }
                }
                else
                {
                    Growl.Warning("ไม่พบรายการสิทธิ์");
                }

            }
            catch (Exception ex)
            {
                Growl.Error("getPttypeId say:\r\n" + ex.Message);
            }
        }
        void GetPttypeIdPT(string pttype)
        {
            DataTable dt = new DataTable();


            Dictionary<string, object> dictData = new Dictionary<string, object>();
            string strSQL = "select pttype,concat('(',pttype,')',' ',namepttype) AS 'namepttype' from pttype" + " where pttype.pttype = '" + pttype + "' order by pttype ASC";
            dictData.Add("query", strSQL);
            try
            {

                DataRow dr;
                dt = Hi7.Class.APIConnect.getDataTable("query/hi7_query", "POST", dictData);

                if (dt != null)
                {
                    if (dt.Rows.Count == 1)
                    {
                        dr = dt.Rows[0];
                        this.cbb_pttype.Text = (string)dr["namepttype"];
                    }
                    else if (dt.Rows.Count > 1)
                    {
                        typeCheckpttype = "1";
                        Growl.Warning("กรุณาเลือกสิทธิ์");
                        HI7.Class.HIUility._TXTSEARCHPTTYPE = pttype;
                        frmPopuppttype frmPopuppttype = new frmPopuppttype();
                        Mdr.Forms.frmMdr.txtPttype = pttype;
                        frmPopuppttype.ShowDialog();
                        if(typeCheckpttype == "1")
                        {
                            cbb_pttype.Text = dataPttype;
                            txt_cbbpttype.Text = idPttype;
                        }
                        else
                        {
                            cbb_pttype.Text = dataPttype;
                            txt_cbbpttype.Text = idPttype;
                        }
                        
                    }
                    else if (dt.Rows.Count == 0)
                    {
                        Growl.Warning("ไม่พบรายการสิทธิ์ที่ค้นหา");
                        HI7.Class.HIUility._TXTSEARCHPTTYPE = pttype;
                        frmPopuppttype frmPopuppttype = new frmPopuppttype();
                        Mdr.Forms.frmMdr.txtPttype = pttype;
                        frmPopuppttype.ShowDialog();
                        if(typeCheckpttype == "1")
                        {
                            cbb_pttype.Text = dataPttype;
                            txt_cbbpttype.Text = idPttype;
                        }
                        else
                        {
                            cbb_pttype.Text = dataPttype;
                            txt_cbbpttype.Text = idPttype;
                        }
                    }
                    else
                    {
                        Growl.Warning("ไม่พบรายการสิทธิ์");
                    }
                }
                else
                {
                    Growl.Warning("ไม่พบรายการสิทธิ์");
                }

            }
            catch (Exception ex)
            {
                Growl.Error("getPttypeId say:\r\n" + ex.Message);
            }
        }
        void getPtclinicId(string cln)
        {
            DataTable dt = new DataTable();
            Dictionary<string, object> dictData = new Dictionary<string, object>();
            string strSQL = "select cln,concat('(',cln,')',namecln) AS 'namecln' from cln where cln.cln LIKE('" + cln + "%') order by cln ASC";
            dictData.Add("query", strSQL);
            try
            {

                DataRow dr;
                dt = Hi7.Class.APIConnect.getDataTable("query/hi7_query", "POST", dictData);

                if (dt != null)
                {
                    if (dt.Rows.Count == 1)
                    {
                        dr = dt.Rows[0];
                        this.cbbClinic.Text = (string)dr["namecln"];
                    }
                    else if (dt.Rows.Count > 1) {
                        Growl.Warning("มีคลินิกมากว่าหนึ่ง");
                        HI7.Class.HIUility._TXTSEARCHCLINIC = cln;
                        GridMdr.Effect = new BlurEffect();
                        GridMdr.Visibility = Visibility.Visible;
                        frmPopupclinic frmPopupclinic = new frmPopupclinic();
                        //HI7.Class.HIUility._TXTSEARCH = txtSearchCC.Text;
                        Mdr.Forms.frmMdr.txtClinic = cln;
                        frmPopupclinic.ShowDialog();
                        //this.txtCC.Text = HI7.Class.HIUility.getodata_Cc(HI7.Class.HIUility._VN);
                        //getoCcTab(HI7.Class.HIUility._VN);
                        GridMdr.Effect = null;
                        cbbClinic.Text = dataClinic;
                        txtcbbClinic.Text = idClinic;
                    }
                    else if (dt.Rows.Count == 0) {
                        Growl.Warning("ไม่พบจุดให้บริการ");
                        HI7.Class.HIUility._TXTSEARCHCLINIC = cln;
                        GridMdr.Effect = new BlurEffect();
                        GridMdr.Visibility = Visibility.Visible;
                        frmPopupclinic frmPopupclinic = new frmPopupclinic();
                        //HI7.Class.HIUility._TXTSEARCH = txtSearchCC.Text;
                        Mdr.Forms.frmMdr.txtClinic = cln;
                        frmPopupclinic.ShowDialog();
                        //this.txtCC.Text = HI7.Class.HIUility.getodata_Cc(HI7.Class.HIUility._VN);
                        //getoCcTab(HI7.Class.HIUility._VN);
                        GridMdr.Effect = null;
                        cbbClinic.Text = dataClinic;
                        txtcbbClinic.Text = idClinic;
                    }
                    else
                    {
                        Growl.Warning("ไม่พบจุดให้บริการ");
                    }
                }
                else
                {
                    Growl.Warning("ไม่พบจุดให้บริการ");
                }

            }
            catch (Exception ex)
            {
                Growl.Error("getPtclinicId say:\r\n" + ex.Message);
            }

        }
        object IsNullString(object s) {
            if(DBNull.Value.Equals(s))
            {
                return "";
            }
            else {
                return s;
            }
        }
        private void cbbTumbon_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                if (cbbAmphur.SelectedItem != null)
                {
                    Object[] data = ((DataRowView)e.AddedItems[0]).Row.ItemArray;
                    strTumbon_code = data[1].ToString();
                    getVillage();
                    Keyboard.Focus(this.cbbVillage);
                }

            }
            catch (Exception ex)
            {

            }
        }

        private void btnRegister_Click(object sender, RoutedEventArgs e)
        {
            //ตรวจสอบเบอร์โทรศัพท์ 10 ตัวอักษร
             if (txtpt_phone_number.Text.Length == 9)
            {
                this.lb_txtpt_phone_number.Foreground = System.Windows.Media.Brushes.Red;
                this.txtpt_phone_number.BorderBrush = System.Windows.Media.Brushes.Red;
                Growl.Warning("กรุณาใส่เบอร์โทรศัพท์ให้ครบ 10 ตัว\r\n**หมายเหตุ เป็นเบอร์มือถือเท่านั้น");
                return;
            }
            else if(txtpt_phone_number.Text.Length < 10)
            {
                this.lb_txtpt_phone_number.Foreground = System.Windows.Media.Brushes.Red;
                this.txtpt_phone_number.BorderBrush = System.Windows.Media.Brushes.Red;
                Growl.Warning("กรุณาใส่เบอร์โทรศัพท์ให้ครบ 10 ตัว");
                return;
            }
            if (txtpt_cid.Text.Length != 13)
            {
                this.lb_txtpt_cid.Foreground = System.Windows.Media.Brushes.Red;
                this.txtpt_cid.BorderBrush = System.Windows.Media.Brushes.Red;
                Growl.Warning("กรุณาใส่หมายเลขบัตรประชาชนให้ครบ 13 หลัก");
                return;
            }            
            //เช็คสถานะการเสียชีวิต ลงทะเบียนไม่ได้
            if (GetDadehn(txtsearch.Text) == true)
            {
                Growl.Warning("ผู้มารับบริการได้เสียชีวิตแล้ว\r\nไม่สามารถลงทะเบียนได้");
                return;
            }
            else
            {

            }
            //เช็คการ Admin ลงทะเบียนไม่ได้
            if (GetAdmithn(txtsearch.Text) == true)
            {
                Growl.Warning("ผู้มารับบริการสถานะยังไม่ได้ Discharge\r\nไม่สามารถลงทะเบียนได้");
                return;
            }
            else
            {

            }
            //เช็คการ Refer ลงทะเบียนได้
            if (GetReferin(txtsearch.Text) == true)
            {
                Growl.Warning("ผู้มารับบริการสถานะ Referin เข้ามา\r\nการุณาตรวจสอบ");
            }
            else
            {

            }
            //เช็คการลงทะเบียน 48 ชั่วโมง
            if (GetRegistertodayhn(txtsearch.Text) == true)
            {
                if (MessageBox.Show("วันนี้มีการเข้ารับริการแล้ว\r\nตรวจสอบใน Tab การตารางผู้ลงทะเบียน", "เตือน HN:" + txtsearch.Text + " มีการลงทะเบียนแล้ว", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.No)
                {
                    this.getVisit();
                    return;
                }
                else
                {
                    if (Getcheck24hr(txtsearch.Text) == true)
                    {
                        string messageBoxText = "ไม่สามารถลงทะเบียนได้ เนื่องจากยังไม่ครบ 4 ชั่วโมงในกลุ่มสิทธิ์(อปท.)\r\n หากยืนยันการลงทะเบียนกด Yes หากไม่กดดำเนินการกด No";
                        string caption = "ดำเนินการต่อไป";
                        MessageBoxButton button = MessageBoxButton.YesNo;
                        MessageBoxImage image = MessageBoxImage.Warning;
                        MessageBoxResult result = MessageBox.Show(messageBoxText, caption, button, image);
                        if (result == MessageBoxResult.Yes)
                        {
                            Growl.Warning("จะมีผลต่อการเครมตามเกณฑ์ต้องเข้ารับบริการหลัง 4 ชั่วโมงของสิทธิ์\r\nอปท.และจ่ายตรงกรมบัญชีกลาง ในการเครมด้วยสิทธิ์ LGO/OFC");
                        }
                        else
                        {
                            return;
                        }
                    }
                }
            }
            else
            {
            }
            CheckDatapersonRegister();
            if (String.IsNullOrEmpty(this.txtpt_hn.Text))
            {//รายใหม่
                //เช็คซ้ำเลข 13 หลัก
                try {

                    if(txtpt_cid.Text != "9999999999994" && txtpt_cid.Text != "1111111111119")
                    {
                        DataTable databelcheckhn;
                        databelcheckhn = HI7.Class.HIUility.getHn(txtpt_cid.Text);
                        if (databelcheckhn != null)//ถ้ามีข้อมูล
                        {
                            if (databelcheckhn.Rows.Count != 0)
                            {
                                _CIDREADER = "";
                                _CIDREADER = txtpt_cid.Text;
                                frmHndouble frmHndouble = new frmHndouble();
                                frmHndouble.ShowDialog();                               
                                txtsearch.Text = _HNdouble;
                                Keyboard.Focus(txtsearch);
                                _HNdouble = "";
                                return;
                            }

                        }
                        else
                        {

                        }
                    }
                   
                }
                catch {
                }
                

                if (cb_Register.IsChecked == true)//รายใหม่ออกหมายเลข HN รับบริการด้วยหากคลิกปุ่ม
                {
                    if ((!String.IsNullOrEmpty(txtpt_cid.Text)) && (!String.IsNullOrEmpty(txtcbbpttype.Text))
                        && (!String.IsNullOrEmpty(cbbClinic.Text)) && (!String.IsNullOrEmpty(cbbpt_queuetype.Text))
                        && (!string.IsNullOrEmpty(txtpt_phone_number.Text)) && (!string.IsNullOrEmpty(txtpt_dob.Text))
                        && (!string.IsNullOrEmpty(cbbSexth.Text))
                        )
                    {
                        this.addPT();//อากหมายเลข HN เพิ่มข้อมูลในแฟ้ม PT รายใหม่                      
                        if (!string.IsNullOrEmpty(txtpt_hn.Text))
                        {
                            this.txtpt_hn.BorderBrush = (SolidColorBrush)new BrushConverter().ConvertFromString("#FF599D7E");
                            DataTable dtCheckotoday;
                            // oDate.ToString("yyyy-MM-dd", CultureInfo.GetCultureInfo("en-US"));
                            DateTime dateTime = DateTime.Now;
                            string strTable = "o" + dateTime.ToString("ddMMyy", CultureInfo.GetCultureInfo("th-TH"));
                            dtCheckotoday = HI7.Class.HIUility.checkODDMMYY(strTable);

                            if (Register() != null) //ลงทะเบียนพร้อมดึง VN ล่าสุด พร้อมส่งค่า Default บัตรคิว
                            {
                                if (dtCheckotoday != null)
                                {
                                    //มีตาราง o รายวันของวันนี้แล้ว
                                    try { this.OTODAY(strTable); } catch { Growl.Warning("สร้างแฟ้มประจำวันไม่สำเร็จ!!"); }
                                    try { this.hi7visittoday(); } catch { Growl.Warning("สร้างแฟ้มประจำวันHi7ไม่สำเร็จ!!"); }
                                }
                                else
                                {
                                    try { CREATE_TABLE_OTODAY(); } catch { };//สร้างตาราง OTODAY รายวัน
                                    try { this.OTODAY(strTable); } catch { Growl.Warning("สร้างแฟ้มประจำวันไม่สำเร็จ!!"); }
                                    try { this.hi7visittoday(); } catch { Growl.Warning("สร้างแฟ้มประจำวันHi7ไม่สำเร็จ!!"); }
                                }
                            }
                            updatePtldate();//อัพเดตวันที่มาบริการล่าสุด
                            getDataInscl();//บันทึกUnscl
                            if (cb_servicetypefu.IsChecked == true)//ถ้ามีการกดปุ่มล้างแผลให้บันทึกแฟ้ม RegisterFU
                            {
                                Rtservicetypefu();                                
                            }
                            //การขอ Authencode
                            AuthencodeandInsure();
                            if (cb_print.IsChecked == true)//พิมพ์บัตรคิว
                            {
                                if (String.IsNullOrEmpty(HI7.Class.HIUility.printerid))//เช็คเครื่องพิมพ์ เก็ทเวย์
                                {
                                    HI7.Class.HIUility.getLoginQ4U();
                                    if (!string.IsNullOrEmpty(HI7.Class.HIUility._StrQueueNumber))
                                    {
                                        Mdr.Forms.frmPrintQHi7.strCheckSetting = "0";
                                        frmPrintQHi7 f = new frmPrintQHi7();
                                        f.ShowDialog();
                                        f.Close();
                                        
                                    }
                                    else
                                    {
                                        Growl.Warning("ไม่ได้หมายเลขคิว");                                      
                                    }
                                }
                                else//ไม่มีเกทเวย์
                                {
                                    HI7.Class.HIUility.getLoginQ4U();
                                    if (HI7.Class.HIUility._StrQueueNumber != null)
                                    {
                                        HI7.Class.HIUility.getPrintQ4U();//print Getway
                                        
                                    }
                                    else
                                    {
                                        Growl.Warning("ไม่ได้หมายเลขคิว");
                                        
                                    }
                                }
                            }
                            else
                            {
                                try {
                                    HI7.Class.HIUility.getLoginQ4U();//ออกคิวแต่ไม่พิมพ์         
                                } catch { }
                                                       
                            }
                            this.txtsearch.Text = null;
                            ClearData();
                            getVisit();//ดึงข้อมูลในการลงทะเบียน
                            Growl.Success("ลงทะเบียนเสร็จแล้ว");
                           
                        }
                        else
                        {
                            this.txtpt_hn.BorderBrush = System.Windows.Media.Brushes.Red;
                            Growl.Warning("ไม่สามารถส่งทะเบียนได้กรุณาตรวจสอบการลงข้อมูล(กรอกHN แล้วหรือไม่ ค้นหาผู้มารับบริการแล้วหรือไม่ ใส่บัตรประชาชนแล้วหรือไม่)");
                        }
                       
                    }
                    else {
                        Growl.Warning("ไม่สามารถลงทะเบียนได้กรุณาตรวจสอบข้อมูล");
                    }      
                }
                else {//รายใหม่ขอ HN
                    try {

                        if ((!String.IsNullOrEmpty(txtpt_cid.Text)) && (!String.IsNullOrEmpty(txt_cbbpttype.Text))
                         && (!String.IsNullOrEmpty(cbbClinic.Text)) && (!String.IsNullOrEmpty(cbbpt_queuetype.Text))
                         && (!string.IsNullOrEmpty(txtpt_phone_number.Text)) && (!string.IsNullOrEmpty(txtpt_dob.Text))
                         && (!string.IsNullOrEmpty(cbbSexth.Text))
                         )
                        {
                            if (!String.IsNullOrEmpty(txtpt_cid.Text))
                            {
                                this.txtpt_cid.BorderBrush = (SolidColorBrush)new BrushConverter().ConvertFromString("#FF599D7E");
                            }
                            else
                            {
                                this.txtpt_cid.BorderBrush = System.Windows.Media.Brushes.Red;
                                Growl.Warning("กรุณาระบุหมายเลขบัตรประชาชน");
                            }
                            if (!String.IsNullOrEmpty(txtpt_fname.Text))
                            {
                                this.txtpt_fname.BorderBrush = (SolidColorBrush)new BrushConverter().ConvertFromString("#FF599D7E");
                            }
                            else
                            {
                                this.txtpt_fname.BorderBrush = System.Windows.Media.Brushes.Red;
                                Growl.Warning("กรุณาระบุชื่อ");
                            }
                            if (!String.IsNullOrEmpty(txtpt_lname.Text))
                            {
                                this.txtpt_lname.BorderBrush = (SolidColorBrush)new BrushConverter().ConvertFromString("#FF599D7E");
                            }
                            else
                            {
                                this.txtpt_lname.BorderBrush = System.Windows.Media.Brushes.Red;
                                Growl.Warning("กรุณาระบุชื่อ");
                            }
                            if (!string.IsNullOrEmpty(txtpt_dob.Text))
                            {
                                this.txtpt_dob.BorderBrush = (SolidColorBrush)new BrushConverter().ConvertFromString("#FF599D7E");
                            }
                            else
                            {
                                this.txtpt_dob.BorderBrush = System.Windows.Media.Brushes.Red;
                                Growl.Warning("กรุณระบุ วันเดือนปีเกิด");
                            }

                            if (!String.IsNullOrEmpty(this.txtpt_cid.Text) && !String.IsNullOrEmpty(this.txtpt_fname.Text) && !String.IsNullOrEmpty(this.txtpt_lname.Text))
                            {
                                this.addPT();
                               

                                Growl.Success("ออก HN รายใหม่เสร็จแล้ว");
                            }
                            else
                            {
                                Growl.Warning("ตรวจสอบเลขบัตรประชาชน/ชื่อ/สกุล");
                            }
                        }
                        else
                        {
                            Growl.Warning("ไม่สามารถลงทะเบียนได้กรุณาตรวจสอบข้อมูล");
                        }
                        
                        }
                    catch (Exception ex) {
                        Growl.Warning("ไม่สามารถบันทึกได้กรุณาตรวจสอบ!!" + ex.Message);
                    }
                }
            }
            else {//รายเก่ามี HN
                if ((!String.IsNullOrEmpty(txtpt_cid.Text)) && (!String.IsNullOrEmpty(txtcbbpttype.Text))
                        && (!String.IsNullOrEmpty(cbbClinic.Text)) && (!String.IsNullOrEmpty(cbbpt_queuetype.Text))
                        && (!string.IsNullOrEmpty(txtpt_phone_number.Text)) && (!string.IsNullOrEmpty(txtpt_dob.Text))
                        && (!string.IsNullOrEmpty(cbbSexth.Text))
                        )
                {
                    DataTable dtCheckotoday;
                    // oDate.ToString("yyyy-MM-dd", CultureInfo.GetCultureInfo("en-US"));
                    DateTime dateTime = DateTime.Now;
                    string strTable = "o" + dateTime.ToString("ddMMyy", CultureInfo.GetCultureInfo("th-TH"));
                    dtCheckotoday = HI7.Class.HIUility.checkODDMMYY(strTable);
                    if (Register() != null) //ลงทะเบียนพร้อมดึง VN ล่าสุด พร้อมส่งค่า Default บัตรคิว
                    {
                        if (dtCheckotoday != null)
                        {
                            //มีตาราง o รายวันของวันนี้แล้ว
                            try { this.OTODAY(strTable); } catch { Growl.Warning("สร้างแฟ้มประจำวันไม่สำเร็จ!!"); }
                            try { this.hi7visittoday(); } catch { Growl.Warning("สร้างแฟ้มประจำวันHi7ไม่สำเร็จ!!"); }
                        }
                        else
                        {
                            try { CREATE_TABLE_OTODAY(); } catch { };//สร้างตาราง OTODAY รายวัน
                            try { this.OTODAY(strTable); } catch { Growl.Warning("สร้างแฟ้มประจำวันไม่สำเร็จ!!"); }
                            try { this.hi7visittoday(); } catch { Growl.Warning("สร้างแฟ้มประจำวันHi7ไม่สำเร็จ!!"); }
                        }
                        getDataInscl();
                        this.txtsearch.Text = null;
                        updatePtldate();
                        //การขอ Authencode
                        AuthencodeandInsure();
                        if (cb_servicetypefu.IsChecked == true)//ตรวจสอบปุ่ม ล้างแผล
                        {
                            Rtservicetypefu();
                        }
                        //getodata_Screen();
                        if (cb_print.IsChecked == true)
                        {

                            if (String.IsNullOrEmpty(HI7.Class.HIUility.printerid))//เช็คเครื่องพิมพ์ เก็ทเวย์
                            {
                                HI7.Class.HIUility.getLoginQ4U();
                                if (HI7.Class.HIUility._StrQueueNumber != null)
                                {
                                    Mdr.Forms.frmPrintQHi7.strCheckSetting = "0";
                                    frmPrintQHi7 f = new frmPrintQHi7();
                                    f.ShowDialog();

                                    f.Close();
                                   
                                }
                                else
                                {
                                    Growl.Warning("ไม่ได้หมายเลขคิว");                                    
                                }
                            }
                            else
                            {
                                HI7.Class.HIUility.getLoginQ4U();
                                if (HI7.Class.HIUility._StrQueueNumber != null)
                                {
                                    HI7.Class.HIUility.getPrintQ4U();//print Getway                                    
                                }
                                else
                                {
                                    Growl.Warning("ไม่ได้หมายเลขคิว");                                    
                                }
                            }
                        }
                        else
                        {
                            try {
                                HI7.Class.HIUility.getLoginQ4U();
                            } catch
                            {

                            }
                                                
                        }
                        ClearData();
                    }
                    else {
                        Growl.Warning("ไม่สามารถลงทะเบียนได้กรุณาตรวจสอบข้อมูล");
                    }
                    
                }
                else {
                    Growl.Warning("ไม่สามารถลงทะเบียนได้กรุณาตรวจสอบข้อมูล");
                }
                getVisit();//ดึงข้อมูลในการลงทะเบียน
                Growl.Success("ดึงข้อมูล Visit success!!");
            }
        }
        void AuthencodeandInsure()
        {
            if (!string.IsNullOrEmpty(jsonsmartAgent))//ถ้าเสียบบัตร
            {
                try { nhso_smartcard_read(); } catch { Growl.Error("ไม่สามารถขอ AuthenCode"); }
            }
            else//ไมาเสียบตรวจสอบประวัติเก่า
            {
                try { check_nhsoAuthencode(); } catch { Growl.Error("ไม่สามารถขอ AuthenCode"); }
            }
            //บันทึก insure
            if (cb_insure.IsChecked == true)
            {//บันทึกสิทธิ์อัติโนมัติ
                if (string.IsNullOrEmpty(jsonsmartAgent))//สิทธิจากบัตรประชาชน ถ้าไม่มีดึงจาก UCW
                {
                    if (wsStatus == "NHSO-000001")
                    {
                        InsureDataUCW();
                    }
                    else if (wsStatus == "NHSO-00003")//บันทึกใน insure frmInsure
                    {
                        frmInsure frmInsure = new frmInsure();
                        frmInsure.ShowDialog();
                    }
                }
                else
                {
                    try { INSURE(); } catch { }
                }
            }
            else if (cb_insure.IsChecked == false)
            {
                frmInsure frmInsure = new frmInsure();
                frmInsure.ShowDialog();

            }
        }
        void CheckDatapersonRegister()
        {
            if (!String.IsNullOrEmpty(cbbClinic.Text))
            {
                this.cbbClinic.BorderBrush = (SolidColorBrush)new BrushConverter().ConvertFromString("#FF599D7E");
                this.txtcbbClinic.BorderBrush = (SolidColorBrush)new BrushConverter().ConvertFromString("#FF599D7E");
            }
            else
            {
                this.cbbClinic.BorderBrush = System.Windows.Media.Brushes.Red;
                this.txtcbbClinic.BorderBrush = System.Windows.Media.Brushes.Red;
                Growl.Warning("กรุณเลือกจุดรับบริการ");
            }
            if (!String.IsNullOrEmpty(txtcbbpttype.Text))
            {
                this.txtcbbpttype.BorderBrush = (SolidColorBrush)new BrushConverter().ConvertFromString("#FF599D7E");
                this.cbbpttype.BorderBrush = (SolidColorBrush)new BrushConverter().ConvertFromString("#FF599D7E");
            }
            else
            {
                this.txtcbbpttype.BorderBrush = System.Windows.Media.Brushes.Red;
                this.cbbpttype.BorderBrush = System.Windows.Media.Brushes.Red;
                Growl.Warning("กรุณาเลือกสิทธิ์การรักษา");
            }
            if (!String.IsNullOrEmpty(cbbpt_queuetype.Text))
            {
                this.cbbpt_queuetype.BorderBrush = (SolidColorBrush)new BrushConverter().ConvertFromString("#FF599D7E");
            }
            else
            {
                this.cbbpt_queuetype.BorderBrush = System.Windows.Media.Brushes.Red;
                Growl.Warning("กรุณาเลือกประเภทคิว");
            }
            if (!string.IsNullOrEmpty(txtpt_phone_number.Text))
            {
                this.txtpt_phone_number.BorderBrush = (SolidColorBrush)new BrushConverter().ConvertFromString("#FF599D7E");
            }
            else
            {
                this.txtpt_phone_number.BorderBrush = System.Windows.Media.Brushes.Red;
                Growl.Warning("กรุณาระบุหมายเลขเบอร์โทรศัพท์");
            }
            if (!string.IsNullOrEmpty(txtpt_cid.Text))
            {
                this.txtpt_cid.BorderBrush = (SolidColorBrush)new BrushConverter().ConvertFromString("#FF599D7E");
            }
            else
            {
                this.txtpt_cid.BorderBrush = System.Windows.Media.Brushes.Red;
                Growl.Warning("กรุณระบุ เลขบัตรประชาชน");
            }

            if (!string.IsNullOrEmpty(txtpt_dob.Text))
            {
                this.txtpt_dob.BorderBrush = (SolidColorBrush)new BrushConverter().ConvertFromString("#FF599D7E");
            }
            else
            {
                this.txtpt_dob.BorderBrush = System.Windows.Media.Brushes.Red;
                Growl.Warning("กรุณระบุ วันเดือนปีเกิด");
            }
            if (!String.IsNullOrEmpty(cbbSexth.Text))
            {
                this.cbbSexth.BorderBrush = (SolidColorBrush)new BrushConverter().ConvertFromString("#FF599D7E");
            }
            else
            {
                this.cbbSexth.BorderBrush = System.Windows.Media.Brushes.Red;
                Growl.Warning("กรุณาเลือกเพศ");
            }
        }
        private void getDataInscl() {
            try
            {
                string pttype = txtcbbpttype.Text;
                string datainscl = HI7.Class.HIUility.getDatainscl(pttype);
                if (datainscl == "OFC" || datainscl == "LGO")
                {
                    Inscl();
                    Insgt();
                }
                else
                {
    
                }
            }
            catch(Exception ex) {
                Growl.Error("getDataInscl say:\r\n" + ex.Message);
            }
            
        }
        bool Inscl()//บันทึกสิทธิ์ OFC/LGO 
        {
            string vn = getVN();
            string prtq = cbbClinic.SelectedValue.ToString();
            string strField = "vn, hn";
            string strValue = "'" + vn + "','" + txtpt_hn.Text + "'";
            string strSQL = "INSERT INTO inscl(" + strField + ") values(" + strValue + ")";
            //INSERT INTO hi.inscl(vn, hn, an, subinscl, relinscl, useright, righttype)
            Dictionary<string, object> dictData = new Dictionary<string, object>();

            try
            {
                dictData.Add("query", strSQL);
                bool status = Hi7.Class.APIConnect.Excecute("query/hi7_query", "POST", dictData);
                if (status)
                {
                    Growl.Success("Inscl Succress!!!");
                    // insert data
                    return true;

                }
                else
                {
                    return false;
                }


            }
            catch (Exception ex)
            {
                Growl.Error("Inscl say:\r\n"+ex.Message);
                return false;
            }

        }
        bool Insgt()//บันทึกสิทธิ์ OFC/LGO Insgt
        {
            string vn = getVN();
            string prtq = cbbClinic.SelectedValue.ToString();
            string strField = "vn, hn";
            string strValue = "'" + vn + "','" + txtpt_hn.Text + "'";
            string strSQL = "INSERT INTO inspt(" + strField + ") values(" + strValue + ")";
            //INSERT INTO hi.inscl(vn, hn, an, subinscl, relinscl, useright, righttype)
            Dictionary<string, object> dictData = new Dictionary<string, object>();

            try
            {
                dictData.Add("query", strSQL);
                bool status = Hi7.Class.APIConnect.Excecute("query/hi7_query", "POST", dictData);
                if (status)
                {
                    Growl.Success("Insgt Succress!!!");
                    // insert data
                    return true;

                }
                else
                {
                    return false;
                }


            }
            catch (Exception ex)
            {
                Growl.Error("Insgt say:\r\n" + ex.Message);
                return false;
            }
        }
        bool Rtservicetypefu()//ลงนัด ทำแผล/เย็บแผล/มาไม่ตรงนัด เก็บเงิน 50 บาท
        {
            string vn  = getVN();
            string prtq = cbbClinic.SelectedValue.ToString();
            string strField = "vn, hn, fname, lname, cln, prtq";
            string strValue = "'"+vn +"','"+ txtpt_hn.Text + "','" + txtpt_fname.Text + "','" + txtpt_lname.Text + "','" + prtq + "','" + "0"+ "'";
            string strSQL = "INSERT INTO registerfu(" + strField + ") values(" + strValue + ")";

            Dictionary<string, object> dictData = new Dictionary<string, object>();

            try
            {
                dictData.Add("query", strSQL);
                bool status = Hi7.Class.APIConnect.Excecute("query/hi7_query", "POST", dictData);
                if (status)
                {
                    Growl.Success("Rtservicetypefu Succress!!!");
                    // insert data
                    return true;

                }
                else
                {
                    return false;
                }


            }
            catch (Exception ex)
            {
                Growl.Error("Rtservicetypefu say:\r\n" + ex.Message);
                return false;
            }

        }
        void GetVisitback() {
            try
            {
                string datenow = HI7.Class.HIUility.Getdateserver();
                if (datenow == datetoday.Text)
                {
                    datepicker = HI7.Class.HIUility.DateChange6(datenow);
                    this.ct_edittypecln.Visibility = Visibility.Visible; 
                    this.ct_editcln.Visibility = Visibility.Visible;
                    this.ct_reprint.Visibility = Visibility.Visible;
                    this.ct_delect.Visibility = Visibility.Visible;
                    this.ct_referin.Visibility = Visibility.Visible;
                    this.ct_changedate.Visibility = Visibility.Collapsed;
                }
                else
                {
                    datepicker = HI7.Class.HIUility.DateChange6(datetoday.Text);
                    this.ct_edittypecln.Visibility = Visibility.Collapsed; 
                    this.ct_editcln.Visibility = Visibility.Collapsed;
                    this.ct_reprint.Visibility = Visibility.Collapsed; 
                    this.ct_delect.Visibility = Visibility.Collapsed;
                    this.ct_referin.Visibility = Visibility.Collapsed;
                    this.ct_changedate.Visibility = Visibility.Visible;
                }
                

                Dictionary<string, object> dictData = new Dictionary<string, object>();
                //string strSQL1 = "SELECT o.vn, a.hn,concat(a.pname,a.fname,' ',a.lname) as fullname,p.namepttype, if ( a.male = 1,'ชาย','หญิง') AS sex,c.namecln,o.register,concat(trim(s.fname),' ',trim(s.lname)) as nameregis,  DATE_FORMAT(o.vstdttm, '%Y-%m-%d')  AS vstdttm, DATE_FORMAT(o.vstdttm, '%H:%i')  AS vsttime,a.fname,a.lname,kios_pttype.claimCode,visitqueueid.queue_number AS queue_number,visitqueueid.queue_priority AS priority_name,timestampdiff(year, a.brthdate, CURDATE()) AS age,o.pttype AS idpttype " +
                //            " FROM pt AS a left JOIN ovst AS o ON a.hn = o.hn and a.ldate = '" + datepicker +
                //            "' LEFT JOIN opstaff as s ON o.register = s.staff" +
                //            " LEFT JOIN pttype AS p ON p.pttype = o.pttype" +
                //            " LEFT JOIN cln AS c ON c.cln = o.cln" +
                //            " LEFT JOIN kios_pttype on kios_pttype.vn = o.vn" +
                //            " LEFT JOIN visitqueueid ON visitqueueid.vn = o.vn" +
                //            " WHERE DATE(o.vstdttm) = '" + datepicker + "' order by o.vn desc ";
                string strSQL = "SELECT " +
                    "o.vn," +
                    "a.hn," +
                    "DATE_FORMAT(o.vstdttm, '%Y-%m-%d')  AS vstdttm," +
                    "DATE_FORMAT(o.vstdttm, '%H:%i')  AS vsttime," +
                    "if ( a.male = 1,'ชาย','หญิง') AS sex," +
                    "concat(a.pname,a.fname,' ',a.lname) as fullname," +
                    "p.namepttype," +
                    "c.namecln," +
                    "concat(trim( s.fname ),' ',trim( s.lname )) AS nameregis," +
                    "kios_pttype.claimCode," +
                    "visitqueueid.queue_number AS queue_number," +
                    "visitqueueid.queue_priority AS priority_name" +
                            " FROM pt AS a left JOIN ovst AS o ON a.hn = o.hn and a.ldate = '" + datepicker +"'"+
                            " LEFT JOIN opstaff as s ON o.register = s.staff" +
                            " LEFT JOIN pttype AS p ON p.pttype = o.pttype" +
                            " LEFT JOIN cln AS c ON c.cln = o.cln" +
                            " LEFT JOIN kios_pttype on kios_pttype.vn = o.vn" +
                            " LEFT JOIN visitqueueid ON visitqueueid.vn = o.vn" +
                            " WHERE DATE(o.vstdttm) = '"+ datepicker + "' order by o.vn desc ";
                dictData.Add("query", strSQL);
                dtvisit = Hi7.Class.APIConnect.getDataTable("query/hi7_query", "POST", dictData);
                if (dtvisit != null && dtvisit.Rows.Count > 0)
                {
                    System.Data.DataView view = new System.Data.DataView(dtvisit);
                    System.Data.DataTable selected = view.ToTable("Selected", false);
                    this.drgVisit.ItemsSource = selected.DefaultView;
                }
            }
            catch (Exception ex)
            {
            }
        }
        DataTable dtvisit = new System.Data.DataTable();
        void getVisit()//เรียก รายการแสดง Visit
        {
            try
            {
                //string datenow = DateTime.Now.ToString("dd/m/yyyy", new CultureInfo("th-TH"));

                SetDate();
                this.ct_edittypecln.Visibility = Visibility.Visible;
                this.ct_editcln.Visibility = Visibility.Visible;
                this.ct_reprint.Visibility = Visibility.Visible;
                this.ct_delect.Visibility = Visibility.Visible;
                this.ct_referin.Visibility = Visibility.Visible;
                this.ct_changedate.Visibility = Visibility.Collapsed;
                //DataTable dt = new DataTable();
                Dictionary<string, object> dictData = new Dictionary<string, object>();

                string strSQL = "SELECT "+
                    "o.vn,"+
                    "a.hn,"+
                    "DATE_FORMAT(o.vstdttm, '%Y-%m-%d')  AS vstdttm,"+
                    "DATE_FORMAT(o.vstdttm, '%H:%i')  AS vsttime," +
                    "if ( a.male = 1,'ชาย','หญิง') AS sex,"+
                    "concat(a.pname,a.fname,' ',a.lname) as fullname," +
                    "p.namepttype,"+
                    "c.namecln,"+
                    "concat(trim( s.fname ),' ',trim( s.lname )) AS nameregis,"+
                    "kios_pttype.claimCode,"+
                    "visitqueueid.queue_number AS queue_number,"+
                    "visitqueueid.queue_priority AS priority_name"+         
                            " FROM pt AS a left JOIN ovst AS o ON a.hn = o.hn and a.ldate = CURRENT_DATE()" +
                            " LEFT JOIN opstaff as s ON o.register = s.staff" +
                            " LEFT JOIN pttype AS p ON p.pttype = o.pttype" +
                            " LEFT JOIN cln AS c ON c.cln = o.cln" +
                            " LEFT JOIN kios_pttype on kios_pttype.vn = o.vn" +
                            " LEFT JOIN visitqueueid ON visitqueueid.vn = o.vn" +
                            " WHERE DATE(o.vstdttm) = CURDATE() order by o.vn desc ";
                dictData.Add("query", strSQL);
                dtvisit = Hi7.Class.APIConnect.getDataTable("query/hi7_query", "POST", dictData);
                if (dtvisit != null && dtvisit.Rows.Count > 0)
                {
                    System.Data.DataView view = new System.Data.DataView(dtvisit);
                    System.Data.DataTable selected = view.ToTable("Selected", false);
                    this.drgVisit.ItemsSource = selected.DefaultView;
                }
            }
            catch (Exception ex)
            {
            }
        }
        void GetNoAuthencode()//เรียก รายการแสดง Visit ยังไม่ได้ขอ AuthenCode
        {
            try
            {
                string datenow = HI7.Class.HIUility.Getdateserver();
                if (datenow == datetodayAuthen.Text)
                {
                    datepickerAuthen = HI7.Class.HIUility.DateChange6(datenow);
                }
                else
                {
                    datepickerAuthen = HI7.Class.HIUility.DateChange6(datetodayAuthen.Text);

                }


                Dictionary<string, object> dictData = new Dictionary<string, object>();
                string strSQL = "SELECT o.vn, a.hn,concat(a.pname,a.fname,' ',a.lname) as fullname,p.namepttype, if ( a.male = 1,'ชาย','หญิง') AS sex,c.namecln,o.register,concat(trim(s.fname),' ',trim(s.lname)) as nameregis,  DATE_FORMAT(o.vstdttm, '%Y-%m-%d')  AS vstdttm, DATE_FORMAT(o.vstdttm, '%H:%i')  AS vsttime,a.fname,a.lname,kios_pttype.claimCode,visitqueueid.queue_number AS queue_number,visitqueueid.queue_priority AS priority_name,timestampdiff(year, a.brthdate, CURDATE()) AS age,o.pttype AS idpttype,a.pop_id " +
                            " FROM pt AS a left JOIN ovst AS o ON a.hn = o.hn and a.ldate = '" + datepickerAuthen +
                            "' LEFT JOIN opstaff as s ON o.register = s.staff" +
                            " LEFT JOIN pttype AS p ON p.pttype = o.pttype" +
                            " LEFT JOIN cln AS c ON c.cln = o.cln" +
                            " LEFT JOIN kios_pttype on kios_pttype.vn = o.vn" +
                            " LEFT JOIN visitqueueid ON visitqueueid.vn = o.vn" +
                            " WHERE DATE(o.vstdttm) = '" + datepickerAuthen + "' and kios_pttype.claimCode is NULL order by o.vn desc ";
                dictData.Add("query", strSQL);
                dtvisitAuthen = Hi7.Class.APIConnect.getDataTable("query/hi7_query", "POST", dictData);
                if (dtvisitAuthen != null && dtvisitAuthen.Rows.Count > 0)
                {
                    System.Data.DataView view = new System.Data.DataView(dtvisitAuthen);
                    System.Data.DataTable selected = view.ToTable("Selected", false);
                    this.drgVisitNoAuthencode.ItemsSource = selected.DefaultView;
                }
            }
            catch (Exception ex)
            {
            }
        }
        void getReprintQueu()//เรียก รายการแสดง Visit
        {
            DataTable dtvisit = new System.Data.DataTable();
            try
            {
                Dictionary<string, object> dictData = new Dictionary<string, object>();
                string strSQL = " SELECT o.vn, a.hn,concat(a.fname,' ',a.lname) as fullname,p.namepttype, if ( a.male = 1,'ชาย','หญิง') AS sex,c.namecln,o.register,concat(trim(s.fname),' ',trim(s.lname)) as nameregis,  DATE_FORMAT(o.vstdttm, '%Y-%m-%d')  AS vstdttm, DATE_FORMAT(o.vstdttm, '%H:%i')  AS vsttime,a.fname,a.lname,kios_pttype.claimCode  " +
                            " FROM pt AS a left JOIN ovst AS o ON a.hn = o.hn and a.ldate = CURRENT_DATE()" +
                            " LEFT JOIN opstaff as s ON o.register = s.staff" +
                            " LEFT JOIN pttype AS p ON p.pttype = o.pttype" +
                            " LEFT JOIN cln AS c ON c.cln = o.cln" +
                            " LEFT JOIN kios_pttype on kios_pttype.vn = o.vn" +
                            " WHERE DATE(o.vstdttm) = CURDATE() order by o.vn desc ";
                dictData.Add("query", strSQL);
                dtvisit = Hi7.Class.APIConnect.getDataTable("query/hi7_query", "POST", dictData);
                if (dtvisit != null && dtvisit.Rows.Count > 0)
                {
                    System.Data.DataView view = new System.Data.DataView(dtvisit);
                    System.Data.DataTable selected = view.ToTable("Selected", false);
                    this.drgVisit.ItemsSource = selected.DefaultView;
                }
            }
            catch (Exception ex)
            {
            }
        }

        private void btnnshoService_Click(object sender, RoutedEventArgs e)
        {
            this.ClearData();
            this.nhso_smartcard_readpttype();
            //this.getnhsodata();
        }

        private void menuRefer_Click(object sender, RoutedEventArgs e)
        {
            GridMdr.Effect = new BlurEffect();
            GridMdr.Visibility = Visibility.Visible;
            frmRefer frmRefer = new frmRefer();
            frmRefer.ShowDialog();
            GridMdr.Effect = null;
        }

        private void cbbAmphur_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
             try
            {
                if ( cbbAmphur.SelectedItem != null )
                {
                    Object[] data = ((DataRowView)e.AddedItems[0]).Row.ItemArray;
                    strAmphur_code = data[1].ToString();
                    getTumbon();
                    Keyboard.Focus(this.cbbTumbon);
                }

            }
            catch (Exception ex)
            {

            }
           }
        private void cbbChanwat_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (this.cbbChanwat.Text != "")
            {
                try
                {
                    if (cbbAmphur.SelectedItem != null)
                    {
                        Object[] data = ((DataRowView)e.AddedItems[0]).Row.ItemArray;
                        this.strProvince_code = data[1].ToString();
                        getAmphur();
                        Keyboard.Focus(this.cbbAmphur);
                    }
                    else
                    {
                        Object[] data = ((DataRowView)e.AddedItems[0]).Row.ItemArray;
                        this.strProvince_code = data[1].ToString();
                        getAmphur();
                        Keyboard.Focus(this.cbbAmphur);
                    }

                }
                catch (Exception ex)
                {

                }

            }
            else {
                try
                {
                    if (cbbAmphur.SelectedItem != null)
                    {
                        Object[] data = ((DataRowView)e.AddedItems[0]).Row.ItemArray;
                        this.strProvince_code = data[1].ToString();
                        getAmphur();
                        Keyboard.Focus(this.cbbAmphur);
                    }
                    else
                    {
                        Object[] data = ((DataRowView)e.AddedItems[0]).Row.ItemArray;
                        this.strProvince_code = data[1].ToString();
                        getAmphur();
                        Keyboard.Focus(this.cbbAmphur);
                    }

                }
                catch (Exception ex)
                {

                }
            }
        }

        void getPatientInfo(string stype,string strSearch) {
            DataTable dt = new DataTable();
            DataRow dr;
            Dictionary<string, object> dictData = new Dictionary<string, object>();
            string strCondition="";
            mrtlst = "";
            stroccptn = "";
            ctzshp = "";
            ntnlty = "";
            rlgn = "";
            typearea = "";
            housetype = "";
            if ( stype == "hn" ) {
                strCondition = " p.hn =" + "'"+strSearch+"'";
            }
            else {
                strCondition = " p.pop_id =" + strSearch;
            }
            string strSQL = "SELECT p.*,REPLACE(p.hometel,'-','') as hometelnew,DATE_FORMAT(DATE_ADD(p.brthdate, INTERVAL 543 YEAR), '%d/%m/%Y') AS dob,DATE_FORMAT(DATE_ADD(p.dthdate, INTERVAL 543 YEAR), '%d/%m/%Y') AS dthdate,c.namechw,a.nameampur,t.nametumb,m.namemoob from hi.pt as p " +
                                " LEFT JOIN hi.changwat as c on p.chwpart = c.chwpart" +
                                " LEFT JOIN hi.ampur as a on p.chwpart = a.chwpart and p.amppart = a.amppart" +
                                " LEFT JOIN hi.tumbon as t on p.chwpart = t.chwpart and p.amppart = t.amppart and p.tmbpart = t.tmbpart" +
                                " LEFT JOIN hi.mooban as m on p.chwpart = m.chwpart and p.amppart = m.amppart and p.tmbpart = m.tmbpart and p.moopart = m.moopart" +
                                " where  " + strCondition + " GROUP BY p.hn";
            dictData.Add("query", strSQL);
            try {
                dt = Hi7.Class.APIConnect.getDataTable("query/hi7_query", "POST", dictData);
                if (dt != null)
                {
                    if ( dt.Rows.Count > 0 )
                    {
                        dr = dt.Rows[0];
                        this.cbbPname.Text = ( string ) dr["pname"];
                        this.cbbPname_eng.Text = (string)dr["engpname"];
                        this.txtpt_fname.Text = ( string ) HI7.Class.HIUility.IsNullString(dr["fname"]);
                        this.txtpt_lname.Text = ( string ) HI7.Class.HIUility.IsNullString(dr["lname"]);
                        this.txtpt_fname_eng.Text = ( string ) HI7.Class.HIUility.IsNullString(dr["engfname"]);
                        this.txtpt_lname_eng.Text = ( string ) HI7.Class.HIUility.IsNullString(dr["englname"]);
                        this.cbbSexth.Text = this.CheckSex(int.Parse(( string ) dr["male"]), this.txtSex, "th").ToString();
                        this.cbbSexen.Text = this.CheckSex(int.Parse(( string ) dr["male"]), this.txtSex, "en").ToString();
                        this.txtpt_cid.Text = dr["pop_id"].ToString();
                        this.txtpt_hn.Text = dr["hn"].ToString();
                        this.txtnhsdata_cid.Text = dr["pop_id"].ToString();
                        this.txtCheckstatus.Text = "รายเก่า";
                        strProvince_code = dr["chwpart"].ToString();
                        strAmphur_code = dr["amppart"].ToString();
                        strTumbon_code = dr["tmbpart"].ToString();
                        strVillage_code = dr["moopart"].ToString();
                        this.txtpt_HouseNo.Text = ( string ) HI7.Class.HIUility.IsNullString(dr["addrpart"]);
                        strpnameth = ( string ) dr["pname"];
                        strengpname = ( string ) HI7.Class.HIUility.IsNullString(dr["engpname"]);
                        try
                        {
                            this.txtpt_phone_number.Text = (string)HI7.Class.HIUility.IsNullString(dr["hometelnew"]).ToString().Substring(0, 10);
                        }
                        catch (Exception ex) {
                            this.txtpt_phone_number.Text = dr["hometel"].ToString();
                        }
                        this.txtpt_contact_name.Text = dr["infmname"].ToString();
                        this.txtpt_contact_phone_number.Text = ( string ) HI7.Class.HIUility.IsNullString(dr["infmtel"]);
                        this.txtpt_contact_address.Text = dr["infmaddr"].ToString();
                        this.txtpt_mother_name.Text = ( string ) HI7.Class.HIUility.IsNullString(dr["mthname"]);
                        this.txtpt_father_name.Text = ( string ) HI7.Class.HIUility.IsNullString(dr["fthname"]);
                        mrtlst = dr["mrtlst"].ToString();
                        stroccptn = dr["occptn"].ToString();
                        ctzshp = dr["ctzshp"].ToString();
                        ntnlty = dr["ntnlty"].ToString();
                        rlgn = dr["rlgn"].ToString();
                        typearea = dr["typearea"].ToString();
                        housetype = dr["housetype"].ToString();
                        this.cbbbloodgroup.Text = dr["bloodgrp"].ToString();
                        this.txtpt_allergy.Text = dr["allergy"].ToString();
                        this.txtpt_cidlabor.Text = dr["cidlabor"].ToString();
                        this.txtpassport.Text = dr["passport"].ToString();
                        this.cbbareatype.Text = dr["typearea"].ToString();                      
                        initailData();
                        this.cbbClinic.Text = _CLN;
                        this.cbbpt_queuetype.Text = _PRYORITY;

                        try {
                            txtpt_dob.Text = dr["dob"].ToString();
                            Getvaluedatehi7dob(txtpt_dob.Text);
                            this.txtPtAgenow.Text = HI7.Class.HIUility.Hn2Agem(txtpt_hn.Text);
                        } catch {
                            }


                        date_dthdate.Text = dr["dthdate"].ToString();
                        if (!string.IsNullOrEmpty(date_dthdate.Text))
                        {
                            if (date_dthdate.Text == "00/00/0000" || string.IsNullOrEmpty(date_dthdate.Text))
                            {
                                date_dthdate.Text = "00/00/0000";
                            }
                            else
                            {
    
                                Getvaluedatehi7dth(date_dthdate.Text);
                            }
                        }
                        else
                        {
                            date_dthdate.Text = "00/00/0000";
                        }    
                        this.txtpt_contact_relation.Text = dr["statusinfo"].ToString();
                        this.txtpt_house_id.Text = dr["house_id"].ToString();
                        this.txtpt_cidlabor.Text = dr["cidmophic"].ToString();
                        Growl.Success("โหลดข้อมูลสำเร็จแล้ว");
                    }
                    else {//ไม่พบ HN เป็นรายใหม่
                        initailData();
                        ClearDataresetHis();
                        txtPtAgenow.Text = txtAgenow.Text;
                        string checkcardid = this.txtIDCard.Text;//ดึงข้อมูลมาจากบัตร
                        if (checkcardid != "")
                        {
                            getdata_newPatient();
                            this.txtCheckstatus.Text = "รายใหม่";
                            this.cbbpt_claimtype.SelectedIndex = 0;
                            this.cbbareatype.SelectedIndex = 4;
                            Growl.Warning("คนไข้รายใหม่");
                        }
                        else {
                            Growl.Success("ไม่พบประวัติมารักษาที่โรงพยาบาล");
                        }
                    }
                }
                else
                {
                    initailData();
                    ClearData();
                    getdata_newPatient();
                    this.txtCheckstatus.Text = "รายใหม่";
                    this.cbbpt_claimtype.SelectedIndex = 0;
                    this.cbbareatype.SelectedIndex = 4;
                    Growl.Warning("คนไข้รายใหม่");
                }
            }
            catch (Exception ex) {
                // Get stack trace for the exception with source file information
                var st = new StackTrace(ex, true);
                // Get the top stack frame
                //var frame = st.GetFrame(0);
                //// Get the line number from the stack frame
                //var line = frame.GetFileLineNumber();
                this.ClearData();
                Growl.Error("getPatientInfo say:\r\n" + ex.Message+ st);
                //this.ClearData();
                //Growl.Error("getPatientInfo say:\r\n" + ex.Message);
            }            
         }
        private void Getvaluedatetoday(string day)
        {
            string dd = "";
            string mm = "";
            string yy = "";
            string[] arr = day.Split('/');
            bool d = arr[0].Length == 1;
            dd = d ? "0" + arr[0] : arr[0];
            bool m = arr[1].Length == 1;
            mm = m ? "0" + arr[1] : arr[1];
               bool y = arr[2].Length == 1;
            yy = y ? "0" + arr[2] : arr[2];
            datetoday.searchTextBoxDay.Text = dd;
            datetoday.searchTextBoxMonth.Text = GetMonthName(mm);
            datetoday.searchTextBoxYear.Text = yy;
            datetodayAuthen.searchTextBoxDay.Text = dd;
            datetodayAuthen.searchTextBoxMonth.Text = GetMonthName(mm);
            datetodayAuthen.searchTextBoxYear.Text = yy;
        }
        //หาวันที่
        private void Getvaluedatehi7dob(string day)
        {
            string dd = "";
            string mm = "";
            string yy = "";
            string[] arr = day.Split('/');
            bool d = arr[0].Length == 1;
            dd = d ? "0" + arr[0] : arr[0];
            bool m = arr[1].Length == 1;
            mm = m ? "0" + arr[1] : arr[1];
            bool y = arr[2].Length == 1;
            yy = y ? "0" + arr[2] : arr[2];
            txtpt_dob.searchTextBoxDay.Text = dd;
            txtpt_dob.searchTextBoxMonth.Text = GetMonthName(mm);
            txtpt_dob.searchTextBoxYear.Text = yy;
        }
        private void Getvaluedatehi7dth(string day)
        {
            string dd = "";
            string mm = "";
            string yy = "";
            string[] arr = day.Split('/');
            bool d = arr[0].Length == 1;
            dd = d ? "0" + arr[0] : arr[0];
            bool m = arr[1].Length == 1;
            mm = m ? "0" + arr[1] : arr[1];
            bool y = arr[2].Length == 1;
            yy = y ? "0" + arr[2] : arr[2];
            date_dthdate.searchTextBoxDay.Text = dd;
            date_dthdate.searchTextBoxMonth.Text = GetMonthName(mm);
            date_dthdate.searchTextBoxYear.Text = yy;
        }
        private string GetMonthName(string monthCode)
        {
            // สร้างคู่ key-value สำหรับรหัสเดือนและชื่อเดือน
            Dictionary<string, string> monthNames = new Dictionary<string, string>
        {
            { "00",""},
            { "01", "ม.ค." },
            { "02", "ก.พ." },
            { "03", "มี.ค." },
            { "04", "เม.ย." },
            { "05", "พ.ค." },
            { "06", "มิ.ย." },
            { "07", "ก.ค." },
            { "08", "ส.ค." },
            { "09", "ก.ย." },
            { "10", "ต.ค." },
            { "11", "พ.ย." },
            { "12", "ธ.ค." }
        };

            // ค้นหาชื่อเดือนจากรหัสเดือนที่กำหนด
            if (monthNames.ContainsKey(monthCode))
            {
                return monthNames[monthCode];
            }
            else
            {
                return string.Empty;
            }

            
        }
        void getPatientInfoappoint(string stype, string strSearch)//ค้นกรณีมีนัด
        {
            //txtBirthDate.Text;
            DataTable dt = new DataTable();
            DataRow dr;
            Dictionary<string, object> dictData = new Dictionary<string, object>();
            string strCondition = "";
            if (stype == "hn")
            {
                strCondition = " p.hn =" + strSearch;
            }
            else
            {
                strCondition = " p.pop_id =" + strSearch;
            }
            string strSQL = "SELECT p.*,REPLACE(p.hometel,'-','') as hometelnew,DATE_FORMAT(DATE_ADD(p.brthdate, INTERVAL 543 YEAR), '%d/%m/%Y') AS dob,DATE_FORMAT(DATE_ADD(p.dthdate, INTERVAL 543 YEAR), '%d/%m/%Y') AS dthdate,c.namechw,a.nameampur,t.nametumb,m.namemoob from hi.pt as p " +
                                " LEFT JOIN hi.changwat as c on p.chwpart = c.chwpart" +
                                " LEFT JOIN hi.ampur as a on p.chwpart = a.chwpart and p.amppart = a.amppart" +
                                " LEFT JOIN hi.tumbon as t on p.chwpart = t.chwpart and p.amppart = t.amppart and p.tmbpart = t.tmbpart" +
                                " LEFT JOIN hi.mooban as m on p.chwpart = m.chwpart and p.amppart = m.amppart and p.tmbpart = m.tmbpart and p.moopart = m.moopart" +
                                " where  " + strCondition + " GROUP BY p.hn";
            dictData.Add("query", strSQL);

            try
            {
                dt = Hi7.Class.APIConnect.getDataTable("query/hi7_query", "POST", dictData);
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        dr = dt.Rows[0];

                        try
                        {
                            if (!string.IsNullOrEmpty(txtBirthDate.Text))
                            {
                                DateTime date = DateTime.ParseExact(txtBirthDate.Text, "dd/MM/yyyy", null);
                                int year = date.Year;
                                int newYear = year + 543;
                                DateTime newDate = new DateTime(newYear, date.Month, date.Day);
                                string result = newDate.ToString("dd/MM/yyyy");

                                DateTime dob = Convert.ToDateTime(result);
                                DateTime PresentYear = DateTime.Now;
                                TimeSpan ts = PresentYear - dob;
                                DateTime Age = DateTime.MinValue.AddDays(ts.Days);
                                string fullAge = (string.Format(" {0} ปี {1} เดือน {2} วัน", Age.Year - 1, Age.Month - 1, Age.Day - 2));
                                this.txtPtAgenow.Text = fullAge; ;//บันทึก
                            }
                            else
                            {
                                this.txtPtAgenow.Text = HI7.Class.HIUility.Hn2Agem(strSearch);
                            }                                                
                        }
                        catch(Exception ex)
                        {
                            this.txtPtAgenow.Text = HI7.Class.HIUility.Hn2Agem(strSearch);
                        }
                        this.txtpt_hn.Text = dr["hn"].ToString();
                        try
                        {
                            txtpt_dob.Text = dr["dob"].ToString();
                            Getvaluedatehi7dob(txtpt_dob.Text);
                            this.txtPtAgenow.Text = HI7.Class.HIUility.Hn2Agem(txtpt_hn.Text);
                        }
                        catch
                        {
                        }
                       
                        date_dthdate.Text = dr["dthdate"].ToString();
                        if (!string.IsNullOrEmpty(date_dthdate.Text))
                        {
                            if (date_dthdate.Text == "00/00/0000" || string.IsNullOrEmpty(date_dthdate.Text))
                            {
                                date_dthdate.Text = "00/00/0000";
                            }
                            else
                            {

                                Getvaluedatehi7dth(date_dthdate.Text);
                            }
                        }
                        else
                        {
                            date_dthdate.Text = "00/00/0000";
                        }

                        this.cbbPname.Text = (string)dr["pname"];
                        this.txtpt_fname.Text = (string)HI7.Class.HIUility.IsNullString(dr["fname"]);
                        this.txtpt_lname.Text = (string)HI7.Class.HIUility.IsNullString(dr["lname"]);
                        this.txtpt_fname_eng.Text = (string)HI7.Class.HIUility.IsNullString(dr["engfname"]);
                        this.txtpt_lname_eng.Text = (string)HI7.Class.HIUility.IsNullString(dr["englname"]);
                        this.cbbSexth.Text = this.CheckSex(int.Parse((string)dr["male"]), this.txtSex, "th").ToString();
                        this.cbbSexen.Text = this.CheckSex(int.Parse((string)dr["male"]), this.txtSex, "en").ToString();
                        this.txtpt_cid.Text = dr["pop_id"].ToString();
                        this.txtnhsdata_cid.Text = dr["pop_id"].ToString();                        
                        strProvince_code = dr["chwpart"].ToString();
                        strAmphur_code = dr["amppart"].ToString();
                        strTumbon_code = dr["tmbpart"].ToString();
                        strVillage_code = dr["moopart"].ToString();
                        this.txtpt_HouseNo.Text = (string)HI7.Class.HIUility.IsNullString(dr["addrpart"]);
                        strpnameth = (string)dr["pname"];
                        strengpname = (string)HI7.Class.HIUility.IsNullString(dr["engpname"]);
                        try
                        {
                            this.txtpt_phone_number.Text = (string)HI7.Class.HIUility.IsNullString(dr["hometelnew"]).ToString().Substring(0, 10);
                        }
                        catch (Exception ex)
                        {
                            this.txtpt_phone_number.Text = dr["hometel"].ToString();
                        }
                        this.txtpt_contact_phone_number.Text = (string)HI7.Class.HIUility.IsNullString(dr["infmtel"]);
                        this.txtpt_mother_name.Text = (string)HI7.Class.HIUility.IsNullString(dr["mthname"]);
                        this.txtpt_father_name.Text = (string)HI7.Class.HIUility.IsNullString(dr["fthname"]);
                        mrtlst = dr["mrtlst"].ToString();
                        stroccptn = dr["occptn"].ToString();
                        ctzshp = dr["ctzshp"].ToString();
                        ntnlty = dr["ntnlty"].ToString();
                        rlgn = dr["rlgn"].ToString();
                        typearea = dr["typearea"].ToString();
                        housetype = dr["housetype"].ToString();
                        this.cbbbloodgroup.Text = dr["bloodgrp"].ToString();
                        this.txtpt_allergy.Text = dr["allergy"].ToString();
                        this.txtpt_cidlabor.Text = dr["cidlabor"].ToString();
                        this.txtpassport.Text = dr["passport"].ToString();
                        this.cbbareatype.Text = dr["typearea"].ToString();
                        //มาตามนัดเช็คสิทธิ์ กรณีที่ไม่ได้เสียบบัตร
                        if (string.IsNullOrEmpty(jsonsmartAgent))
                        {
                            this.UcwsnhsoCID(dr["pop_id"].ToString());
                        }
                        else { 
                        }                        
                        initailData();
                        this.txtsend_appoint.Text = "มาตามนัด";
                        try
                        {
                            this.getPtclinicId(HI7.Class.HIUility._SENDCLN);
                        }
                        catch (Exception ex)
                        {
                            Growl.Error("เกิดข้อผิดพลาด");
                        }
                        this.txtsend_docappoint.Text = HI7.Class.HIUility._FULLNAMEDCT;
                        
                      
                    }
                    else
                    {

                        initailData();
                        ClearData();
                        /// add field
                        //getdata_newPatient();

                    }
                }
                else
                {
                    initailData();
                    ClearData();
                    /// add field
                    //getdata_newPatient();
                }


            }
            catch (Exception ex)
            {
                Growl.Error("getPatientInfoappoint say:\r\n" + ex.Message);
            }


        }
        string pttype;
        string getPttypeName()//หาสิทธิการรักษาล่าสุด
        {           
            try
            {
                DataTable dt = new DataTable();
                DataRow dr;
                Dictionary<string, object> dictData = new Dictionary<string, object>();

                //string strSQL = "select a.pttype,concat('(',p.pttype,')',p.namepttype) as 'namepttype',a.vstdttm from ovst as a inner join pttype as p on a.pttype=p.pttype where  a.hn ="+txtpt_hn.Text+" order by a.vstdttm desc  limit 1";
                string strSQL = "select a.pttype,concat('(',p.pttype,')',p.namepttype) as 'namepttype',a.vstdttm,(SELECT insure.note from insure where insure.hn = a.hn order by insure.id desc limit 1) note,concat(date(a.vstdttm),' ',concat('(',p.pttype,')',p.namepttype)) as 'pttypetoday' from ovst as a inner join pttype as p on a.pttype=p.pttype where a.hn =" + txtpt_hn.Text + " order by a.vstdttm desc limit 1";
                dictData.Add("query", strSQL);
                dt = Hi7.Class.APIConnect.getDataTable("query/hi7_query", "POST", dictData);
                //   dt = Hi7.Class.APIConnect.getDataTable("/register/patient/getPatientInfo", "POST", dictData);
                if ( dt != null )
                {
                    if ( dt.Rows.Count > 0 )
                    {
                        dr = dt.Rows[0];
                        this.txtPttypetoday.Text = dr["pttypetoday"].ToString();
                        GetnoteInsure();
                        //this.txtnote_insure.Text = dr["note"].ToString();
                        //this.cbbpttype.Text = dr["namepttype"].ToString();

                        //this.txtpttype_last.Text ="("+ dr["pttype"].ToString()+")"+ dr["namepttype"].ToString();
                    }

                }
            }
            catch ( Exception ex )
            {
                return "";
            }
            return "";
        }
        string getPttypePtHIS()//หาสิทธิการรักษาใน HIS
        {           
            try
            {
                DataTable dt = new DataTable();
                DataRow dr;
                Dictionary<string, object> dictData = new Dictionary<string, object>();

                string strSQL = "select pt.pttype,pttype.namepttype from pt join pttype on pttype.pttype = pt.pttype" +
                                " where  pt.hn =" + this.txtpt_hn.Text + "";
                dictData.Add("query", strSQL);
                dt = Hi7.Class.APIConnect.getDataTable("query/hi7_query", "POST", dictData);
                //   dt = Hi7.Class.APIConnect.getDataTable("/register/patient/getPatientInfo", "POST", dictData);
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        dr = dt.Rows[0];

                        //this.txtpttype_pt.Text = "(" + dr["pttype"].ToString() + ") " + dr["namepttype"].ToString();
                        this.cbb_pttype.Text = "(" + dr["pttype"].ToString() + ") " + dr["namepttype"].ToString();
                        this.cbbpttype.Text += this.cbb_pttype.Text;
                    }

                }
            }
            catch (Exception ex)
            {
                return "";
            }
            return "";
        }
        string getProvince_code(string province_name)
        {
            DataTable dt = new DataTable();
            DataRow dr;
            Dictionary<string, object> dictData = new Dictionary<string, object>();
            dictData.Add("query", "select * from changwat  where namechw='"+ province_name + "' ");

            try
            {
                dt = Hi7.Class.APIConnect.getDataTable("query/hi7_query", "POST", dictData);
                if ( dt != null )
                {
                    if ( dt.Rows.Count > 0 )
                    {
                        dr = dt.Rows[0];

                        strProvince_code = dr["chwpart"].ToString();
                        //strAmphur_code = dr["amppart"].ToString();
                        //strTumbon_code = dr["tmbpart"].ToString();
                        //strVillage_code = dr["moopart"].ToString();

                        return "";

                    }
                    return "";
                }
                return "";

            }
            catch (Exception ex) {
                return "";
            }
        }

        
              string getAmphur_code(string Amphurname)
        {
            DataTable dt = new DataTable();
            DataRow dr;
            Dictionary<string, object> dictData = new Dictionary<string, object>();
            dictData.Add("query", "select * from ampur  where chwpart='" + strProvince_code + "' and nameampur='" + Amphurname + "' ");

            try
            {
                dt = Hi7.Class.APIConnect.getDataTable("query/hi7_query", "POST", dictData);
                if ( dt != null )
                {
                    if ( dt.Rows.Count > 0 )
                    {
                        dr = dt.Rows[0];

                        strAmphur_code = dr["amppart"].ToString();
                        //strAmphur_code = dr["amppart"].ToString();
                        //strTumbon_code = dr["tmbpart"].ToString();
                        //strVillage_code = dr["moopart"].ToString();

                        return "";

                    }
                    return "";
                }
                return "";

            }
            catch ( Exception ex )
            {
                return "";
            }
        }
        string getTumbon_code(string Tumbonname)
        {
            DataTable dt = new DataTable();
            DataRow dr;
            Dictionary<string, object> dictData = new Dictionary<string, object>();
            dictData.Add("query", "select * from tumbon  where chwpart='" + strProvince_code + "' and amppart='" + strAmphur_code + "' and nametumb='" + Tumbonname+"'");

            try
            {
                dt = Hi7.Class.APIConnect.getDataTable("query/hi7_query", "POST", dictData);
                if ( dt != null )
                {
                    if ( dt.Rows.Count > 0 )
                    {
                        dr = dt.Rows[0];

                        strTumbon_code = dr["tmbpart"].ToString();
                        //strAmphur_code = dr["amppart"].ToString();
                        //strTumbon_code = dr["tmbpart"].ToString();
                        //strVillage_code = dr["moopart"].ToString();

                        return "";

                    }
                    return "";
                }
                return "";

            }
            catch ( Exception ex )
            {
                return "";
            }
        }


        void getdata_newPatient()
        {
            
           
                this.txtpt_cid.Text = this.txtIDCard.Text;
                this.txtpt_fname.Text = this.txtFirstNameThai.Text;
                this.txtpt_lname.Text = this.txtLastnameThai.Text;
                this.txtpt_fname_eng.Text = this.txtFirstNameEng.Text;
                this.txtpt_lname_eng.Text = this.txtLastnameEng.Text;
                

                this.txtpt_HouseNo.Text = this.txtHouseNo.Text;

                // // เก็บค่าตัวเลขไว้ใน Tag เผื่อไว้ตอนไปเก็บลงในฐานข้อมูล [กำหนดให้ ชาย=1 หญิง=2]

                this.cbbSexth.Text = this.CheckSex(intReadCard_Sex, this.txtSex, "th").ToString();
                this.cbbSexen.Text = this.CheckSex(intReadCard_Sex, this.txtSex, "en").ToString();

                this.cbbPname.Text = this.txtPrefixThai.Text;

                strpnameth = this.cbbPname.Text;
                getPnameTH();

                this.cbbPname_eng.Text = this.txtPrefixEng.Text;
                strengpname = this.cbbPname_eng.Text;
                getPnameEN();

                try { 
                getProvince_code(this.txtProvince.Text.Substring(7, this.txtProvince.Text.Length - 7));
                this.getProvince();
                } catch {
                this.getProvince();
                }
                try
                {
                getAmphur_code(this.txtDistrict.Text.Substring(5, this.txtDistrict.Text.Length - 5));
                this.getAmphur();
                }
                catch
                {
                this.getAmphur();
                }
                try
                {
                getTumbon_code(this.txtSub_district.Text.Substring(4, this.txtSub_district.Text.Length - 4));
                this.getTumbon();
                }
                catch
                {
                this.getTumbon();
                }
            try
            {
                //หาหมู่บ้านจากเลขบัตรประชนในฐานข้อมูล
                string strVillageLength = this.txtVillageNo.Text;//ตัวแปร String หมู่บ้าน
                int VillageLength = strVillageLength.Length; //ตัวแปร int ในการนับขนาดตัวอักษร
                if (VillageLength == 9)
                {
                    string Village = strVillageLength.Substring(VillageLength - 1, 1);
                    if (Village.Length == 1)
                    {
                        strVillage_code = "0" + Village;
                    }
                    else
                    {
                        strVillage_code = Village;
                    }
                }
                else if (VillageLength == 10)
                {
                    string Village = strVillageLength.Substring(VillageLength - 2, 2);
                    strVillage_code = Village;
                }
                else
                {
                    string Village = "";
                    strVillage_code = Village;
                }
                this.getVillage();
            }
            catch {
                this.getVillage();
            }
                this.cbbpt_citizenship.Text = "ไทย";
                this.cbbpt_nationality.Text = "ไทย";
                this.cbbpt_religion.Text = "พุทธ";
            Growl.Success("โหลดข้อมูลจากบัตรสำเร็จแล้ว");
        }
        void initailData()
        {
            this.getProvince();
            this.getAmphur();
            this.getTumbon();
            this.getVillage();
            this.getClinic();
            this.getPttype();
            this.getPnameEN();
            this.getPnameTH();
            this.getOccupation();
            this.getCitizen();
            this.getNation();
            this.getReligion();
            this.getQueueType();
            this.getMrtlst();
            this.getAreaType();
            this.getHouseType();
            this.getPttypeName();
            this.getPttypePtHIS();
            this.getodata_Screen();
            this.getClaimtype();
        }

        void getPnameTH()
        {
            DataTable dt = new DataTable();
            //   DataRow dr;
            //    http://tssmart.moph.go.th/api/lookup/changwat/list
            Dictionary<string, object> dictData = new Dictionary<string, object>();
            string strSQL = "select id,prefix_name from psn_prefix order by prefix_name";
            dictData.Add("query", strSQL);



            try
            {

                dt = Hi7.Class.APIConnect.getDataTable("query/hi7_query", "POST", dictData);
                if ( dt != null )
                {

                    string[] selectedColumns = new[] { "prefix_name", "id" };
                    System.Data.DataView view = new System.Data.DataView(dt);
                    // Create a new DataTable from the DataView with just the columns desired - and in the order desired
                    System.Data.DataTable selected = view.ToTable("Selected", false, selectedColumns);

                    this.cbbPname.DisplayMemberPath = "prefix_name";
                    this.cbbPname.SelectedValuePath = "id";
                    this.cbbPname.ItemsSource = selected.DefaultView;

                      DataRow[] result = selected.Select("prefix_name ='" + strpnameth+"'");

                       if (result.Length > 0)
                       {
                           int SelectedIndex = selected.Rows.IndexOf(result[0]);
                           this.cbbPname.SelectedIndex = SelectedIndex;
                         
                       }
                    
                }

            }
            catch ( Exception ex )
            {

            }

        }
        void getClaimtype() {
            DataTable dt = new DataTable();
            //   DataRow dr;

            Dictionary<string, object> dictData = new Dictionary<string, object>();

            //string strSQL = "select priority_id,priority_name from hi7priority    order by priority_name";
            string strSQL = "select * from hi7_claimtype";
            dictData.Add("query", strSQL);

            try
            {


                dt = Hi7.Class.APIConnect.getDataTable("query/hi7_query", "POST", dictData);
                if (dt != null)
                {

                    string[] selectedColumns = new[] { "nametype", "claimtype" };
                    System.Data.DataView view = new System.Data.DataView(dt);
                    // Create a new DataTable from the DataView with just the columns desired - and in the order desired
                    System.Data.DataTable selected = view.ToTable("Selected", false, selectedColumns);

                    this.cbbpt_claimtype.DisplayMemberPath = "nametype";
                    this.cbbpt_claimtype.SelectedValuePath = "claimtype";
                    this.cbbpt_claimtype.ItemsSource = selected.DefaultView;
                }

            }
            catch (Exception ex)
            {

            }
        }
        void getQueueType()
        {
            DataTable dt = new DataTable();
            //   DataRow dr;
      
            Dictionary<string, object> dictData = new Dictionary<string, object>();

            //string strSQL = "select priority_id,priority_name from hi7priority    order by priority_name";
            string strSQL = "select priority_id, priority_name from q4u_priorities order by priority_id";
            dictData.Add("query", strSQL);

            try
            {


                dt = Hi7.Class.APIConnect.getDataTable("query/hi7_query", "POST", dictData);
                if ( dt != null )
                {

                    string[] selectedColumns = new[] { "priority_name", "priority_id" };
                    System.Data.DataView view = new System.Data.DataView(dt);
                    // Create a new DataTable from the DataView with just the columns desired - and in the order desired
                    System.Data.DataTable selected = view.ToTable("Selected", false, selectedColumns);

                    this.cbbpt_queuetype.DisplayMemberPath = "priority_name";
                    this.cbbpt_queuetype.SelectedValuePath = "priority_id";
                    this.cbbpt_queuetype.ItemsSource = selected.DefaultView;
                }

            }
            catch ( Exception ex )
            {

            }

        }
        void getReligion()
        {
            DataTable dt = new DataTable();
            //   DataRow dr;
            //    http://tssmart.moph.go.th/api/lookup/changwat/list
            Dictionary<string, object> dictData = new Dictionary<string, object>();
            // dictData.Add("lookup_group_code", "occupation");
            string strSQL = "select rlgn,namerlgn from rlgn order by namerlgn";
            dictData.Add("query", strSQL);



            try
            {

                dt = Hi7.Class.APIConnect.getDataTable("query/hi7_query", "POST", dictData);

                if ( dt != null )
                {

                    string[] selectedColumns = new[] { "rlgn", "namerlgn" };
                    System.Data.DataView view = new System.Data.DataView(dt);
                    // Create a new DataTable from the DataView with just the columns desired - and in the order desired
                    System.Data.DataTable selected = view.ToTable("Selected", false, selectedColumns);

                    this.cbbpt_religion.DisplayMemberPath = "namerlgn";
                    this.cbbpt_religion.SelectedValuePath = "rlgn";
                    this.cbbpt_religion.ItemsSource = selected.DefaultView;

                     DataRow[] result = selected.Select("rlgn =" + rlgn);

                       if (result.Length > 0)
                       {
                           int SelectedIndex = selected.Rows.IndexOf(result[0]);
                           this.cbbpt_religion.SelectedIndex = SelectedIndex;
                          
                       }
                    
                }

            }
            catch ( Exception ex )
            {

            }

        }

        //สถานะสมรส
        void getMrtlst()
        {
            DataTable dt = new DataTable();
            //   DataRow dr;
            //    http://tssmart.moph.go.th/api/lookup/changwat/list
            Dictionary<string, object> dictData = new Dictionary<string, object>();
            // dictData.Add("lookup_group_code", "occupation");
            string strSQL = "select * from mrtlst order by mrtlst ";
            dictData.Add("query", strSQL);



            try
            {

                dt = Hi7.Class.APIConnect.getDataTable("query/hi7_query", "POST", dictData);

                if ( dt != null )
                {

                    string[] selectedColumns = new[] { "mrtlst", "namemrt" };
                    System.Data.DataView view = new System.Data.DataView(dt);
                    // Create a new DataTable from the DataView with just the columns desired - and in the order desired
                    System.Data.DataTable selected = view.ToTable("Selected", false, selectedColumns);

                    this.cbbpt_mrtlst.DisplayMemberPath = "namemrt";
                    this.cbbpt_mrtlst.SelectedValuePath = "mrtlst";
                    this.cbbpt_mrtlst.ItemsSource = selected.DefaultView;

                    DataRow[] result = selected.Select("mrtlst =" + mrtlst);

                    if ( result.Length > 0 )
                    {
                        int SelectedIndex = selected.Rows.IndexOf(result[0]);
                        this.cbbpt_mrtlst.SelectedIndex = SelectedIndex;

                    }

                }

            }
            catch ( Exception ex )
            {

            }

        }


        void getAreaType()
        {
            DataTable dt = new DataTable();
            //   DataRow dr;
            //    http://tssmart.moph.go.th/api/lookup/changwat/list
            Dictionary<string, object> dictData = new Dictionary<string, object>();
            // dictData.Add("lookup_group_code", "occupation");
            string strSQL = "select * from areatype order by areatype ";
            dictData.Add("query", strSQL);



            try
            {

                dt = Hi7.Class.APIConnect.getDataTable("query/hi7_query", "POST", dictData);

                if ( dt != null )
                {

                    string[] selectedColumns = new[] { "areatype", "namearea" };
                    System.Data.DataView view = new System.Data.DataView(dt);
                    // Create a new DataTable from the DataView with just the columns desired - and in the order desired
                    System.Data.DataTable selected = view.ToTable("Selected", false, selectedColumns);

                    this.cbbareatype.DisplayMemberPath = "namearea";
                    this.cbbareatype.SelectedValuePath = "areatype";
                    this.cbbareatype.ItemsSource = selected.DefaultView;

                    DataRow[] result = selected.Select("areatype =" + typearea);

                    if ( result.Length > 0 )
                    {
                        int SelectedIndex = selected.Rows.IndexOf(result[0]);
                        this.cbbareatype.SelectedIndex = SelectedIndex;

                    }

                }

            }
            catch ( Exception ex )
            {

            }

        }

        
                 void getHouseType()
        {
            DataTable dt = new DataTable();
            //   DataRow dr;
            //    http://tssmart.moph.go.th/api/lookup/changwat/list
            Dictionary<string, object> dictData = new Dictionary<string, object>();
            // dictData.Add("lookup_group_code", "occupation");
            string strSQL = "select * from l_housetype order by housetype ";
            dictData.Add("query", strSQL);



            try
            {

                dt = Hi7.Class.APIConnect.getDataTable("query/hi7_query", "POST", dictData);

                if ( dt != null )
                {

                    string[] selectedColumns = new[] { "housetype", "namehouse" };
                    System.Data.DataView view = new System.Data.DataView(dt);
                    // Create a new DataTable from the DataView with just the columns desired - and in the order desired
                    System.Data.DataTable selected = view.ToTable("Selected", false, selectedColumns);

                    this.cbbhousetype.DisplayMemberPath = "namehouse";
                    this.cbbhousetype.SelectedValuePath = "housetype";
                    this.cbbhousetype.ItemsSource = selected.DefaultView;

                    DataRow[] result = selected.Select("housetype =" + housetype);

                    if ( result.Length > 0 )
                    {
                        int SelectedIndex = selected.Rows.IndexOf(result[0]);
                        this.cbbhousetype.SelectedIndex = SelectedIndex;

                    }

                }

            }
            catch ( Exception ex )
            {

            }

        }

       
        //public static DataTable TopRows(this DataTable dTable, int rowCount)
        //{
        //    DataTable dtNew = dTable.Clone();
        //    dtNew.BeginLoadData();
        //    if ( rowCount > dTable.Rows.Count ) { rowCount = dTable.Rows.Count; }
        //    for ( int i = 0; i < rowCount; i++ )
        //    {
        //        DataRow drNew = dtNew.NewRow();
        //        drNew.ItemArray = dTable.Rows[i].ItemArray;
        //        dtNew.Rows.Add(drNew);
        //    }
        //    dtNew.EndLoadData();
        //    return dtNew;
        //}
        void getNation()
        {
            DataTable dt = new DataTable();
            //   DataRow dr;
            //    http://tssmart.moph.go.th/api/lookup/changwat/list
            Dictionary<string, object> dictData = new Dictionary<string, object>();
            string strSQL = "select ntnlty,namentnlty from ntnlty order by namentnlty";
            dictData.Add("query", strSQL);


            try
            {
                dt = Hi7.Class.APIConnect.getDataTable("query/hi7_query", "POST", dictData);

                if ( dt != null )
                {

                    string[] selectedColumns = new[] { "ntnlty", "namentnlty" };
                    System.Data.DataView view = new System.Data.DataView(dt);
                    // Create a new DataTable from the DataView with just the columns desired - and in the order desired
                    System.Data.DataTable selected = view.ToTable("Selected", false, selectedColumns);

                    this.cbbpt_nationality.DisplayMemberPath = "namentnlty";
                    this.cbbpt_nationality.SelectedValuePath = "ntnlty";
                    this.cbbpt_nationality.ItemsSource = selected.DefaultView;

                      DataRow[] result = selected.Select("ntnlty =" + ntnlty);

                       if (result.Length > 0)
                       {
                           int SelectedIndex = selected.Rows.IndexOf(result[0]);
                           this.cbbpt_nationality.SelectedIndex = SelectedIndex;
                          
                       }
                    
                }

            }
            catch ( Exception ex )
            {

            }

        }
        void getOccupation()
        {
            DataTable dt = new DataTable();
            //   DataRow dr;
            //    http://tssmart.moph.go.th/api/lookup/changwat/list
            Dictionary<string, object> dictData = new Dictionary<string, object>();
 
        // dictData.Add("lookup_group_code", "occupation");
            string strSQL = "select occptn,nameoccptn from occptn order by nameoccptn";
            dictData.Add("query", strSQL);



            try
            {

                dt = Hi7.Class.APIConnect.getDataTable("query/hi7_query", "POST", dictData);

                //     dt = Hi7.Class.APIConnect.getDataTable("/lookup/lookup/list", "POST", dictData);
                if ( dt != null )
                {

                    string[] selectedColumns = new[] { "nameoccptn", "occptn" };
                    System.Data.DataView view = new System.Data.DataView(dt);
                    // Create a new DataTable from the DataView with just the columns desired - and in the order desired
                    System.Data.DataTable selected = view.ToTable("Selected", false, selectedColumns);

                    this.cbbpt_occupation.DisplayMemberPath = "nameoccptn";
                    this.cbbpt_occupation.SelectedValuePath = "occptn";
                    this.cbbpt_occupation.ItemsSource = selected.DefaultView;

                      DataRow[] result = selected.Select("occptn =" + stroccptn);

                       if (result.Length > 0)
                       {
                           int SelectedIndex = selected.Rows.IndexOf(result[0]);
                           this.cbbpt_occupation.SelectedIndex = SelectedIndex;
                        //   this.getAmphur();
                       }
                     
                }

            }
            catch ( Exception ex )
            {

            }

        }

        void getCitizen()
        {
            DataTable dt = new DataTable();
            //   DataRow dr;
            //    http://tssmart.moph.go.th/api/lookup/changwat/list
            Dictionary<string, object> dictData = new Dictionary<string, object>();
            string strSQL = "select ntnlty,namentnlty from ntnlty order by namentnlty";
            dictData.Add("query", strSQL);


            try
            {
                dt = Hi7.Class.APIConnect.getDataTable("query/hi7_query", "POST", dictData);

                if ( dt != null )
                {

                    string[] selectedColumns = new[] { "ntnlty", "namentnlty" };
                    System.Data.DataView view = new System.Data.DataView(dt);
                    // Create a new DataTable from the DataView with just the columns desired - and in the order desired
                    System.Data.DataTable selected = view.ToTable("Selected", false, selectedColumns);

                    this.cbbpt_citizenship.DisplayMemberPath = "namentnlty";
                    this.cbbpt_citizenship.SelectedValuePath = "ntnlty";
                    this.cbbpt_citizenship.ItemsSource = selected.DefaultView;
                    //ctzshp = dr["ctzshp"].ToString();
                    DataRow[] result = selected.Select("ntnlty =" + ctzshp);

                    if ( result.Length > 0 )
                    {
                        int SelectedIndex = selected.Rows.IndexOf(result[0]);
                        this.cbbpt_citizenship.SelectedIndex = SelectedIndex;

                    }

                }

            }
            catch ( Exception ex )
            {

            }  

        }

      
        private void txthn_KeyUp(object sender, KeyEventArgs e)
        {
            if(e.Key == Key.Enter )
            {
                gindex = 0;
                Keyboard.Focus(this.drgVisit);
            
                this.drgVisit.SelectedIndex = gindex;
                gindex++;

            }

        }

        private void drgVisit_KeyUp(object sender, KeyEventArgs e)
        {
            //Keyboard.Focus(this.tabmdr);
        }

        Int16 gindex=0;
        private void drgVisit_KeyDown(object sender, KeyEventArgs e)
        {
            if ( e.Key.Equals(Key.Up) )
            {
                Keyboard.Focus(this.drgVisit);
                moveUp();
            }
            if ( e.Key.Equals(Key.Down) )
            {
                Keyboard.Focus(this.drgVisit);
                moveDown();
            }
            e.Handled = true;
        }

        private void moveUp()
        {
            if ( drgVisit.Items.Count > 0 ) {
                    if ( gindex <= drgVisit.Items.Count && gindex >=0 ){
                       this.drgVisit.SelectedIndex = gindex;
                        gindex--;
                    }
                    else
                    {
                        gindex = 0;
                    }
            }
        }

        private void moveDown()
        {
            if ( drgVisit.Items.Count > 0 )
            {


                if ( gindex <= drgVisit.Items.Count && gindex >= 0 )
                {

                    this.drgVisit.SelectedIndex = gindex;
                    gindex++;
                }
                else {
                    gindex = 0;
                }
            }
        }

        private void drgVisit_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            drgVisit.ScrollIntoView(CollectionView.NewItemPlaceholder);
        }

       

        private void drgVisit_RequestBringIntoView(object sender, RequestBringIntoViewEventArgs e)
        {
            e.Handled= true;
            this.drgVisit.RequestBringIntoView += drgVisit_RequestBringIntoView;
        }

        void getPnameEN()
        {
            DataTable dt = new DataTable();
            Dictionary<string, object> dictData = new Dictionary<string, object>();
            // dictData.Add("lookup_group_code", "occupation");
            string strSQL = "select id,prefix_name from psn_prefix order by prefix_name";
            dictData.Add("query", strSQL);



            try
            {

                dt = Hi7.Class.APIConnect.getDataTable("query/hi7_query", "POST", dictData);



                if ( dt != null )
                {

                    string[] selectedColumns = new[] { "prefix_name", "id" };
                    System.Data.DataView view = new System.Data.DataView(dt);
                    // Create a new DataTable from the DataView with just the columns desired - and in the order desired
                    System.Data.DataTable selected = view.ToTable("Selected", false, selectedColumns);

                    this.cbbPname_eng.DisplayMemberPath = "prefix_name";
                    this.cbbPname_eng.SelectedValuePath = "id";
                    this.cbbPname_eng.ItemsSource = selected.DefaultView;

                     DataRow[] result = selected.Select("prefix_name ='" + strengpname+"'");

                       if (result.Length > 0)
                        {
                            int SelectedIndex = selected.Rows.IndexOf(result[0]);
                            this.cbbPname_eng.SelectedIndex = SelectedIndex;
                            
                        } 

                }

            }
            catch ( Exception ex )
            {

            }

        }
        


        
        private void btnUpdate1_Click(object sender, RoutedEventArgs e)//แก้ไขทั่วไป PT
        {
            try {
                string hntest = "";
                updatePT();
                hntest = this.txtpt_hn.Text;
                ClearDataresetHis();
                
                _PRYORITY = this.cbbpt_queuetype.Text;
                _CLN = this.cbbClinic.Text;
                this.cbbChanwat.SelectedItem = null;
                this.cbbAmphur.SelectedItem = null;
                this.cbbTumbon.SelectedItem = null;
                this.cbbVillage.SelectedItem = null;
                strProvince_code = "";
                strAmphur_code = "";
                strTumbon_code = "";
                strVillage_code = "";

                getPatientInfo("hn", hntest);
                if (jsonsmartAgent != null && jsonsmartAgent != "")
                {
                    nhso_smartcard_readpttype();
                }
                else
                {
                    this.UcwsnhsoCID(this.txtpt_cid.Text);
                }
                this.cbbpt_claimtype.SelectedIndex = 0;
                Keyboard.Focus(btnRegister);
            }
            catch { 
            }
        }
        string getVN() {//Last VN
            string strSQL ="SELECT vn from ovst where hn='"+this.txtpt_hn.Text+"' and date(vstdttm)=curdate() ORDER BY ovst.VN DESC";
            Dictionary<string, object> dictData = new Dictionary<string, object>();
            dictData.Add("query", strSQL);
            DataRow dr;
            try
            {
                DataTable dt= Hi7.Class.APIConnect.getDataTable("query/hi7_query", "POST", dictData);
                if ( dt.Rows.Count > 0 ) {
                    dr = dt.Rows[0];
                   
                    return dr.ItemArray[0].ToString();
                }
            }
            catch (Exception ex) { 
            
            }
                return "";
        }

        private void menuReport_Click(object sender, RoutedEventArgs e)
        {
            GridMdr.Effect = new BlurEffect();
            frmPrintall frmPrintall = new frmPrintall();
            frmPrintall.ShowDialog();
            GridMdr.Effect = null;
            //Opd.Forms.frmPrintall 
            //Opd.Forms.frm f1 = new Opd.Form1();
            //f1.Show();
            //    frmPrintall = new Opd.Forms.frmPrintall();
            //Opd.Forms.frmPrintall f = new Opd.Forms.frmPrintall();
            //f.ShowDialog();

        }
        string getHNbyvn()
        {

            string strSQL = "SELECT hn from ovst where vn='" + VN + "' limit 1  ";
            Dictionary<string, object> dictData = new Dictionary<string, object>();
            dictData.Add("query", strSQL);
            DataRow dr;
            try
            {
                DataTable dt = Hi7.Class.APIConnect.getDataTable("query/hi7_query", "POST", dictData);
                if (dt.Rows.Count > 0)
                {
                    dr = dt.Rows[0];

                    return dr.ItemArray[0].ToString();
                }
            }
            catch (Exception ex)
            {

            }
            return "";
        }
            string getHN()
        {
            
            string strSQL ="SELECT hn from pt where pop_id='"+this.txtpt_cid.Text+"' limit 1  ";
            Dictionary<string, object> dictData = new Dictionary<string, object>();
            dictData.Add("query", strSQL);
            DataRow dr;
            try
            {
                DataTable dt= Hi7.Class.APIConnect.getDataTable("query/hi7_query", "POST", dictData);
                if ( dt.Rows.Count > 0 )
                {
                    dr = dt.Rows[0];

                    return dr.ItemArray[0].ToString();
                }
            }
            catch ( Exception ex )
            {

            }
            return "";
        }

        

        private void btnSmartCardAgent_Click(object sender, RoutedEventArgs e)
        {
            try {
                if (cbbpt_claimtype.SelectedValue.ToString() != "" || cbbpt_claimtype.SelectedValue.ToString != null)
                {
                    nhso_smartcard_readbtn();//อ่านเพื่อขอข้อมูล
                }
                else
                {
                    Growl.Warning("กรุณาตรวจสอบดังนี้!!\r\n1.เสียบบัตรประชาชนและกดอ่านบัตร\r\n2.เลือกประเภทของการเครม Authencode\r\n3.ตรวจสอบเบอร์โทรศัพท์\r\n" +
                        "กรณีที่ไม่ได้นำบัตรมาให้ใส่หมายเลขบัตรประชาชน");
                }
                //nhso_confirm_save();//บันทึกในตาราง
            }
            catch {
                Growl.Warning("กรุณาตรวจสอบดังนี้!!\r\n1.เสียบบัตรประชาชนและกดอ่านบัตร\r\n2.เลือกประเภทของการเครม Authencode\r\n3.ตรวจสอบเบอร์โทรศัพท์\r\n" +
                        "กรณีที่ไม่ได้นำบัตรมาให้ใส่หมายเลขบัตรประชาชน");
            }
        }
        //เลือกคนไข้ลทะเบียนจากการคัดกรอง
        private void dataGridScreen_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            ClearData();                      
            DataRowView dataRow = (DataRowView)dataGridScreen.SelectedItem;
            int index = dataGridScreen.CurrentCell.Column.DisplayIndex;
            string idscreen = dataRow.Row.ItemArray[0].ToString();
            string hn = dataRow.Row.ItemArray[1].ToString();
            string clinic = dataRow.Row.ItemArray[5].ToString();
            string priority = dataRow.Row.ItemArray[6].ToString();
            txtsearch.Text = hn;
            _CLN = clinic;
            _PRYORITY = priority;
            _IDSCREEN = idscreen;
            if (this.txtsearch.Text == "")
            {
                //  this.txtfname.Text = "";
            }
            else
            {                          
                try
                {
                    //DataRow drappoint;
                    //Hi7.Class.APIConnect.API_SERVER = "http://203.114.123.210:30000/";
                    DataTable dtappoint;
                    dtappoint = HI7.Class.HIUility.getAppointment(txtsearch.Text);
                    if (dtappoint.Rows.Count == 0)
                    {
                        getPatientInfo("hn", this.txtsearch.Text);
                        this.cbbpt_claimtype.SelectedIndex = 0;
                        Keyboard.Focus(this.tabmdr);
                        this.nhso_smartcard_readpttype();
                    }
                    else
                    {
                        //เช็คนัด
                        checkAppoint(txtsearch.Text);
                        getPatientInfoappoint("hn", this.txtsearch.Text);
                        this.cbbpt_queuetype.Text = _PRYORITY;
                        Keyboard.Focus(this.tabmdr);
                        this.nhso_smartcard_readpttype();
                    }
                }
                catch (Exception ex)
                {
                    Growl.Error("dataGridScreen_MouseDoubleClick say:\r\n"+ex.Message);
                    //MessageBox.Show("เกิดข้อผิดพลาด");
                }               
            }
            this.cbbpt_claimtype.SelectedIndex = 0;
            Keyboard.Focus(this.tabmdr);            
        }
        private void drgVisit_MouseDoubleClick(object sender, MouseButtonEventArgs e)//ดึงข้อมูลมาเปลี่ยนจุดให้บริการ
        {
            _VNGETDATAGRID = null;
            DataRowView dataRow;
            
            try
            {
                dataRow = drgVisit.SelectedItem as DataRowView;
                //int index = drgVisit.CurrentCell.Column.DisplayIndex;
                //string vn = dataRow.Row.ItemArray[0].ToString();
                _VNGETDATAGRID = dataRow.Row.ItemArray[0].ToString();
                string hn = dataRow.Row.ItemArray[1].ToString();
                string clinic = dataRow.Row.ItemArray[5].ToString();
                string namepttype = dataRow.Row.ItemArray[3].ToString();
                getPatientInfo("hn", hn);
                this.cbbpttype.Text = HI7.Class.HIUility.getIdpttype(namepttype);
                this.cbbClinic.Text = HI7.Class.HIUility.getIdclinic(clinic);
                btnRegister.Visibility = Visibility.Collapsed;
                //btnReferIn.Visibility = Visibility.Collapsed;
                //btnReferOut.Visibility = Visibility.Collapsed;
                this.cbbpt_claimtype.SelectedIndex = 0;
                BitmapToBitmapImageGetpt();
                Keyboard.Focus(this.tabmdr);
            }
            catch (Exception ex) {
                Growl.Warning("การดึงข้อมููลจากตารางลงทะเบียนไม่สำเร็จ\r\n" + ex.Message);
            }
        }
        void addPT()
        {
            string pathimage = "";
            pathimage = this.txtpt_cid.Text + ".jpg";
            string sex = getSex();
            string user = Hi7.Class.APIConnect.USER_IDLOGIN;            
            string strField ="pname,fname,lname,pop_id,addrpart,moopart,tmbpart,"+   
                "amppart,chwpart,brthdate,male,bloodgrp,rlgn,ntnlty,ctzshp,mrtlst,occptn,hometel,"+
                "mthname,fthname,infmname,infmaddr,infmtel,pttype,fdate,"+
                "truebrth,allergy,register,typearea,statusinfo,house_id,housetype,cidlabor,passport,engpname,engfname,englname,pathimage";

            string bdate = HI7.Class.HIUility.CBE2D(this.txtpt_dob.Text);

            string strValues ="'"+this.cbbPname.Text+"','"+this.txtpt_fname.Text+"','"+this.txtpt_lname.Text+"',"+
                "'"+this.txtpt_cid.Text+"','"+this.txtpt_HouseNo.Text+"','"+this.cbbVillage.SelectedValue+"','"+cbbTumbon.SelectedValue + "','"+cbbAmphur.SelectedValue+"',"+
                "'"+cbbChanwat.SelectedValue+"','"+bdate+"','"+ sex + "','"+this.cbbbloodgroup.Text+"','"+this.cbbpt_religion.SelectedValue+"',"+
                "'"+ this.cbbpt_nationality.SelectedValue +"','"+this.cbbpt_citizenship.SelectedValue+"','"+this.cbbpt_mrtlst.SelectedValue+"',"+
                "'"+this.cbbpt_occupation .SelectedValue+ "','"+this.txtpt_phone_number.Text+"','"+this.txtpt_mother_name.Text+"',"+
                "'"+this.txtpt_father_name.Text+"','"+this.txtpt_contact_name.Text+"','"+this.txtpt_contact_address.Text+"','"+this.txtpt_contact_phone_number.Text+"',"+
                "'"+this.txtcbbpttype.Text+"',CURDATE(),0,'"+this.txtpt_allergy.Text+"','"+user+"','"+this.cbbareatype.SelectedValue+"',"+
                "'"+this.txtpt_contact_relation.Text+"','"+this.txtpt_house_id.Text+"','"+this.cbbhousetype.SelectedValue+"',"+
                "'"+this.txtpt_cidlabor.Text+"','"+this.txtpassport.Text+"','"+this.cbbPname_eng.Text+"','"+this.txtpt_fname_eng.Text+"',"+
                "'"+this.txtpt_lname_eng.Text+"',"+ "'" + pathimage + "'";
            string strSQL ="insert into pt ("+strField+") values("+strValues+")";
            Dictionary<string, object> dictData = new Dictionary<string, object>();
            dictData.Add("query", strSQL);
            bool status = Hi7.Class.APIConnect.Excecute("query/hi7_query", "POST", dictData);


            if ( status )
            {
               
                if (txtpt_cid.Text == "1111111111119" || txtpt_cid.Text == "9999999999994")
                {
                    txtpt_hn.Text = Hi7.Class.APIConnect.HNrespon;
                    Gencid(txtpt_cid.Text);
                    Growl.Success("ออกหมายเลข HN" + "(" + Hi7.Class.APIConnect.HNrespon + ")" + " สำเร็จ");
                }
                else
                {
                    txtpt_hn.Text = Hi7.Class.APIConnect.HNrespon;
                    Growl.Success("ออกหมายเลข HN" + "(" + Hi7.Class.APIConnect.HNrespon + ")" + " สำเร็จ");
                }
                
            }

        }
        string getSex() {
            string checksex = cbbSexth.Text;
            if (cbbSexth.Text == null || cbbSexth.Text == "")
            {
                Growl.Warning("มีค่าว่าง");
                //MessageBox.Show("มีค่าว่าง");
                return "";
            }
            else
            {

                if (checksex == "ชาย")
                {
                    return getsex = "1";

                }
                else if (checksex == "หญิง")
                {
                    return getsex = "2";

                }
                else if (checksex == "Male")
                {
                    return getsex = "1";

                }
                else if (checksex == "Female")
                {
                    return getsex = "2";

                }
                else {
                    return "";
                }
            }
        }
        public string  getsex;
        bool updatePT()//update แก้ไขข้อมูลแฟ้ม pt ใส่รหัสผู้แก้ไขด้วย
        {
            try
            {
                //string switchcriteria = "10";
                //var labelName = string.Format("lbllabel{0}", 1);     
                string bdpt = "", bdsmartcard = "", checksex = "", txtpt_fname = "", txtpt_lname = "", cbbPname_eng = "", txtpt_fname_eng = "", txtpt_lname_eng = "",
                    txtpt_cid = "", txtpassport = "", txtpt_HouseNo = "", cbbChanwat = "", cbbAmphur = "", cbbTumbon = "", cbbVillage = "", bdate = "", txtpt_phone_number = "", cbbpt_citizenship = "", cbbpt_nationality = "",
                    cbbpt_religion = "", cbbpt_mrtlst = "", cbbpt_occupation = "", txtpt_father_name = "", txtpt_mother_name = "", txtpt_contact_name = "", txtpt_contact_relation = "", txtpt_house_id = "",
                    cbbhousetype = "", txtpt_contact_address = "", txtpt_contact_phone_number = "", cbbbloodgroup = "", txtpt_allergy = "", cbbareatype = "", txtpt_cidlabor = "",pttype = "", dthdate = "",register="";
                    ;
                //คำนวนวันเกิด
                register = strstaff;
                bdpt = txtpt_dob.Text;                
                checksex = cbbSexth.Text;
                try {
                    bdsmartcard = txtBirthDate.Text;
                    bdate = HI7.Class.HIUility.CBE2D(bdpt);
                }
                catch
                {

                }
               
                if (cbbSexth.Text == null || cbbSexth.Text == "")
                {
                    //MessageBox.Show("มีค่าว่าง");
                    Growl.Warning("มีค่าว่าง");
                }
                else
                {                    
                    if (checksex == "ชาย")
                    {
                        getsex = "1";
                    }
                    else if (checksex == "หญิง")
                    {
                        getsex = "2";
                    }
                    else if (checksex == "Male")
                    {
                        getsex = "1";
                    }
                    else if (checksex == "Female")
                    {
                        getsex = "2";
                    }
                }
                try
                {
                    if (this.cbbChanwat.SelectedValue.ToString() != null || this.cbbChanwat.SelectedValue.ToString() != "")
                    {
                        cbbChanwat = this.cbbChanwat.SelectedValue.ToString();
                    }
                    else
                    {
                        cbbChanwat = "";
                    }

                }
                catch (Exception ex)
                {
                    cbbChanwat = "";
                }
                try
                {
                    if (this.cbbAmphur.SelectedValue.ToString() != null || this.cbbAmphur.SelectedValue.ToString() != "")
                    {
                        cbbAmphur = this.cbbAmphur.SelectedValue.ToString();
                    }
                    else
                    {
                        cbbAmphur = "";
                    }

                }
                catch (Exception ex)
                {
                    cbbAmphur = "";
                }
                try
                {
                    if (this.cbbTumbon.SelectedValue.ToString() != null || this.cbbTumbon.SelectedValue.ToString() != "")
                    {
                        cbbTumbon = this.cbbTumbon.SelectedValue.ToString();
                    }
                    else
                    {
                        cbbTumbon = "";
                    }

                }
                catch (Exception ex)
                {
                    cbbTumbon = "";
                }
                try {
                    if (this.cbbVillage.SelectedValue.ToString() != null || this.cbbVillage.SelectedValue.ToString() != "")
                    {
                        cbbVillage = this.cbbVillage.SelectedValue.ToString();
                    }
                    else
                    {
                        cbbVillage = "";
                    }
                }
                catch (Exception ex) {
                    cbbVillage = "";
                }
                if (!String.IsNullOrEmpty(this.txtpt_fname.Text))
                    txtpt_fname = this.txtpt_fname.Text;
                if (!String.IsNullOrEmpty(this.txtpt_lname.Text))
                    txtpt_lname = this.txtpt_lname.Text;
                if (!String.IsNullOrEmpty(this.cbbPname_eng.Text))
                    cbbPname_eng = this.cbbPname_eng.Text;
                if (!String.IsNullOrEmpty(this.txtpt_fname_eng.Text))
                    txtpt_fname_eng = this.txtpt_fname_eng.Text;
                if (!String.IsNullOrEmpty(this.txtpt_lname_eng.Text))
                    txtpt_lname_eng = this.txtpt_lname_eng.Text;
                if (!String.IsNullOrEmpty(this.txtpt_cid.Text))
                    txtpt_cid = this.txtpt_cid.Text;
                if (!String.IsNullOrEmpty(this.txtpassport.Text))
                    txtpassport = this.txtpassport.Text;
                if (!String.IsNullOrEmpty(this.txtpt_HouseNo.Text))
                    txtpt_HouseNo = this.txtpt_HouseNo.Text;
                try {
                    if (!String.IsNullOrEmpty(this.cbbChanwat.SelectedValue.ToString()))
                        cbbChanwat = this.cbbChanwat.SelectedValue.ToString();
                } catch { cbbChanwat = ""; }
                try
                {
                    if (!String.IsNullOrEmpty(this.cbbAmphur.SelectedValue.ToString()))
                        cbbAmphur = this.cbbAmphur.SelectedValue.ToString();
                }
                catch { cbbAmphur = ""; }
                try
                {
                    if (!String.IsNullOrEmpty(this.cbbTumbon.SelectedValue.ToString()))
                        cbbTumbon = this.cbbTumbon.SelectedValue.ToString();
                }
                catch { cbbTumbon = ""; }
                
                if (!String.IsNullOrEmpty(this.txtpt_phone_number.Text))
                    txtpt_phone_number = this.txtpt_phone_number.Text;
                try
                {
                    if (!String.IsNullOrEmpty(this.cbbpt_citizenship.SelectedValue.ToString()))
                        cbbpt_citizenship = this.cbbpt_citizenship.SelectedValue.ToString();
                }
                catch { cbbpt_citizenship = ""; }
                try
                {
                    if (!String.IsNullOrEmpty(this.cbbpt_nationality.SelectedValue.ToString()))
                        cbbpt_nationality = this.cbbpt_nationality.SelectedValue.ToString();
                }
                catch { cbbpt_nationality = ""; }
                try
                {
                    if (!String.IsNullOrEmpty(this.cbbpt_religion.SelectedValue.ToString()))
                        cbbpt_religion = this.cbbpt_religion.SelectedValue.ToString();
                }
                catch { cbbpt_religion = ""; }
                try
                {
                    if (!String.IsNullOrEmpty(this.cbbpt_mrtlst.SelectedValue.ToString()))
                        cbbpt_mrtlst = this.cbbpt_mrtlst.SelectedValue.ToString();
                }
                catch { cbbpt_mrtlst = ""; }
                try
                {
                    if (!String.IsNullOrEmpty(this.cbbpt_occupation.SelectedValue.ToString()))
                        cbbpt_occupation = this.cbbpt_occupation.SelectedValue.ToString();
                }
                catch { cbbpt_occupation = ""; }
                
                if (!String.IsNullOrEmpty(this.txtpt_father_name.Text))
                    txtpt_father_name = this.txtpt_father_name.Text;
                if (!String.IsNullOrEmpty(this.txtpt_mother_name.Text))
                    txtpt_mother_name = this.txtpt_mother_name.Text;
                if (!String.IsNullOrEmpty(this.txtpt_contact_name.Text))
                    txtpt_contact_name = this.txtpt_contact_name.Text;
                if (!String.IsNullOrEmpty(this.txtpt_contact_relation.Text))
                    txtpt_contact_relation = this.txtpt_contact_relation.Text;
                if (!String.IsNullOrEmpty(this.txtpt_house_id.Text))
                    txtpt_house_id = this.txtpt_house_id.Text;
                try
                {
                    if (!String.IsNullOrEmpty(this.cbbhousetype.SelectedValue.ToString()))
                        cbbhousetype = this.cbbhousetype.SelectedValue.ToString();
                }
                catch { cbbhousetype = ""; }
                
                if (!String.IsNullOrEmpty(this.txtpt_contact_address.Text))
                    txtpt_contact_address = this.txtpt_contact_address.Text;
                if (!String.IsNullOrEmpty(this.txtpt_contact_phone_number.Text))
                    txtpt_contact_phone_number = this.txtpt_contact_phone_number.Text;
                if (!String.IsNullOrEmpty(this.cbbbloodgroup.Text))
                    cbbbloodgroup = this.cbbbloodgroup.Text;
                if (!String.IsNullOrEmpty(this.txtpt_allergy.Text))
                    txtpt_allergy = this.txtpt_allergy.Text;
                try
                {
                    if (!String.IsNullOrEmpty(this.cbbareatype.SelectedValue.ToString()))
                        cbbareatype = this.cbbareatype.SelectedValue.ToString();
                }
                catch { cbbareatype = ""; }
                
                if (!String.IsNullOrEmpty(this.txtpt_cidlabor.Text))
                    txtpt_cidlabor = this.txtpt_cidlabor.Text;
                try
                {
                    if (!String.IsNullOrEmpty(this.cbb_pttype.SelectedValue.ToString()))
                        pttype = this.cbb_pttype.SelectedValue.ToString();
                }
                catch
                {
                    Growl.Warning("ไม่พบสิทธิ์ใน HIS");
                    return false;
                    //pttype = "";
                }                
                    try
                    {
                        string getdthdate = "";
                        getdthdate = date_dthdate.Text;
                        if(getdthdate == "00/00/0000")
                        {
                        dthdate = "0000-00-00";
                    }
                        else {
                        dthdate = HI7.Class.HIUility.CBE2D(getdthdate);
                        }                        
                    }
                    catch
                    {

                    }                  
                string strSQL,strValues; 
                strValues = "pname = '"+this.cbbPname.Text+"',"+ "fname = '" + txtpt_fname + "'," + "lname = '" + txtpt_lname + "'," + "male = '" + getsex + "',"
                    + "engpname = '" + cbbPname_eng + "'," + "engfname = '" + txtpt_fname_eng + "'," + "englname = '" + txtpt_lname_eng + "'," + "pop_id = '" + txtpt_cid + "',"
                    + "passport = '" + txtpassport + "'," + "addrpart = '" + txtpt_HouseNo + "'," + "chwpart = '" + cbbChanwat + "'," + "amppart = '" + cbbAmphur + "',"
                    + "tmbpart = '" + cbbTumbon + "'," + "moopart = '" + cbbVillage + "'," + "brthdate = '" + bdate + "',"
                    + "hometel = '" + txtpt_phone_number + "'," + "ctzshp = '" + cbbpt_citizenship + "'," + "ntnlty = '" + cbbpt_nationality + "',"
                    + "rlgn = '" + cbbpt_religion + "'," + "mrtlst = '" + cbbpt_mrtlst + "'," + "occptn = '" + cbbpt_occupation + "',"
                    + "fthname = '" + txtpt_father_name + "'," + "mthname = '" + txtpt_mother_name + "'," + "infmname = '" + txtpt_contact_name + "'," + "statusinfo = '" + txtpt_contact_relation + "',"
                    + "house_id = '" + txtpt_house_id + "'," + "housetype = '" + cbbhousetype + "'," + "infmaddr = '" + txtpt_contact_address + "',"
                    + "infmtel = '" + txtpt_contact_phone_number + "'," + "bloodgrp = '" + cbbbloodgroup + "'," + "pttype = '" + pttype + "'," + "dthdate = '" + dthdate + "',"
                    + "allergy = '" + txtpt_allergy + "'," + "typearea = '" + cbbareatype + "'," + "cidmophic = '" + txtpt_cidlabor + "',"+ "register ='"+ register+"'"
                    ;                 
                strSQL = "UPDATE pt set " + strValues + " WHERE hn=" + this.txtpt_hn.Text;
                Dictionary<string, object> dictData = new Dictionary<string, object>();
                dictData.Add("query", strSQL);
                bool status = Hi7.Class.APIConnect.Excecute("query/hi7_query", "POST", dictData);
                if ( status )
                {
                    Growl.Success("แก้ไขข้อมูลสำเร็จ");
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch ( Exception ex )
            {
                Growl.Error("updatePT say:\r\n"+ex.Message);
                return false;
            }
        }
        string getSexThEn()
        {
            string sendSex, checksex;
            checksex = this.txtSex.Text;
            if (checksex == "ชาย")
            {
                return sendSex = "1";

            }
            else if (checksex == "หญิง")
            {
                return sendSex = "2";

            }
            else if (checksex == "Male")
            {
                return sendSex = "1";

            }
            else if (checksex == "Female")
            {
                return sendSex = "2";

            }
            else {
                return sendSex = "ระบุไม่ได้";
            }
        }
        public string mooban;
        bool updatePtfromcard()//update แก้ไขข้อมูลแฟ้ม pt จากบัตรประชาชน
        {
            try
            {
                
                string IDcard = this.txtIDCard.Text;
                string Province = this.txtProvince.Text.Substring(7);
                //string subProvince.Substring(5,0);
                string District = this.txtDistrict.Text.Substring(5);
                string Sub_district= this.txtSub_district.Text.Substring(4);
                string strVillageLength = this.txtVillageNo.Text;//ตัวแปร String หมู่บ้าน
                int VillageLength = strVillageLength.Length; //ตัวแปร int ในการนับขนาดตัวอักษร
                if (VillageLength == 9)
                {
                    string Village = strVillageLength.Substring(VillageLength - 1, 1);
                    if (Village.Length == 1) {
                        mooban = "0"+Village;
                    }
                    else
                    {
                        mooban = Village;
                    }
                    
                }
                else if (VillageLength == 10)
                {
                    string Village = strVillageLength.Substring(VillageLength - 2, 2);
                    mooban = Village;
                }
                else {
                    string Village = "";
                    mooban = Village;
                }
                //string House = this.txtHouseNo.Text;
                 HI7.Class.HIUility._CHANWAT = Province;
                 HI7.Class.HIUility._AMPHUR = District;
                 HI7.Class.HIUility._TUMBON = Sub_district;
                string codeChanwat = HI7.Class.HIUility.getCHANWAT();
                string codeAmpur = HI7.Class.HIUility.getAMPHUR();
                string codeTumbon = HI7.Class.HIUility.getTUMBON();
                string sex = getSexThEn();
                string addrpart = this.txtHouseNo.Text + " " + this.txtVillageNo.Text;
                //+ "brthdate = '" + bdate + "',"
                
                string bdate = txtBirthDate.Text;
                string brthdate = HI7.Class.HIUility.DateChange6(bdate);
                string strSQL, strValues;
                strValues = "pname = '" + this.txtPrefixThai.Text + "'," + "fname = '" + this.txtFirstNameThai.Text + "'," + "lname = '" + this.txtLastnameThai.Text + "'," + "male = '" + sex + "',"
                    + "engpname = '" + this.txtPrefixEng.Text + "'," + "engfname = '" + this.txtFirstNameEng.Text + "'," + "englname = '" + this.txtLastnameEng.Text + "'," + "pop_id = '" + this.txtIDCard.Text + "',"
                    + "addrpart = '" + addrpart + "'," + "chwpart = '" + codeChanwat + "'," + "amppart = '" + codeAmpur + "',"
                    + "tmbpart = '" + codeTumbon + "'," + "moopart = '" + mooban+"'," + "brthdate = '"+ brthdate + "'";
                    ;
                
                strSQL = "UPDATE pt set " + strValues + " WHERE hn= '" + this.txtpt_hn.Text+"'";
                Dictionary<string, object> dictData = new Dictionary<string, object>();

                dictData.Add("query", strSQL);
                bool status = Hi7.Class.APIConnect.Excecute("query/hi7_query", "POST", dictData);
                if (status)
                {
                    Growl.Success("แก้ไขข้อมูลสำเร็จ");
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                Growl.Error("updatePtfromcard say:\r\n"+ex.Message);
                return false;
            }
        }
        private void btnRegisterappoint_Click(object sender, RoutedEventArgs e)
        {
            string hn = txtpt_hn.Text;
            //ตัวแปรส่งเข้า INSURE
            PTCID = txtpt_cid.Text.ToString();
            PTFULLNAME = cbbPname.Text + txtpt_fname.Text + " " + txtpt_lname.Text;
            PTHN = txtpt_hn.Text.ToString();
            PTTYPE = cbb_pttype.Text;
            PTTYPECODE = txt_cbbpttype.Text;
            if (!string.IsNullOrEmpty(hn)) {
                //เช็คสถานะการเสียชีวิต ลงทะเบียนไม่ได้
                if (GetDadehn(txtsearch.Text) == true)
                {
                    Growl.Warning("ผู้มารับบริการได้เสียชีวิตแล้ว\r\nไม่สามารถลงทะเบียนได้");
                    return;
                }
                else
                {

                }
                //เช็คการ Admin ลงทะเบียนไม่ได้
                if (GetAdmithn(txtsearch.Text) == true)
                {
                    Growl.Warning("ผู้มารับบริการสถานะยังไม่ได้ Discharge\r\nไม่สามารถลงทะเบียนได้");
                    return;
                }
                else
                {

                }
                //เช็คการ Refer ลงทะเบียนได้
                if (GetReferin(txtsearch.Text) == true)
                {
                    Growl.Warning("ผู้มารับบริการสถานะ Referin เข้ามา\r\nการุณาตรวจสอบ");
                }
                else
                {

                }
                //เช็คการลงทะเบียน 48 ชั่วโมง
                if (GetRegistertodayhn(txtsearch.Text) == true)
                {
                    if (MessageBox.Show("วันนี้มีการเข้ารับริการแล้ว\r\nตรวจสอบใน Tab การตารางผู้ลงทะเบียน", "เตือน HN:" + txtsearch.Text + " มีการลงทะเบียนแล้ว", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.No)
                    {
                        this.getVisit();
                        return;
                    }
                    else
                    {
                        if (Getcheck24hr(txtsearch.Text) == true)
                        {
                            string messageBoxText = "ไม่สามารถลงทะเบียนได้ เนื่องจากยังไม่ครบ 4 ชั่วโมงในกลุ่มสิทธิ์(อปท.)\r\n หากยืนยันการลงทะเบียนกด Yes หากไม่กดดำเนินการกด No";
                            string caption = "ดำเนินการต่อไป";
                            MessageBoxButton button = MessageBoxButton.YesNo;
                            MessageBoxImage image = MessageBoxImage.Warning;
                            MessageBoxResult result = MessageBox.Show(messageBoxText, caption, button, image);
                            if (result == MessageBoxResult.Yes)
                            {
                                Growl.Warning("จะมีผลต่อการเครมตามเกณฑ์ต้องเข้ารับบริการหลัง 4 ชั่วโมงของสิทธิ์\r\nอปท.และจ่ายตรงกรมบัญชีกลาง ในการเครมด้วยสิทธิ์ LGO/OFC");
                            }
                            else
                            {
                                return;
                            }
                        }
                    }
                }
                else
                {

                }
            }
            else
            {
                Growl.Warning("ไม่สามารถลงทะเบียนได้ ไม่พบHN ของผู้มารับบริการ");
                return;
            }
            CheckDatapersonRegister();
            if ((!String.IsNullOrEmpty(txtpt_cid.Text)) && (!String.IsNullOrEmpty(txtcbbpttype.Text))
                        && (!String.IsNullOrEmpty(cbbClinic.Text)) && (!String.IsNullOrEmpty(cbbpt_queuetype.Text))
                        && (!string.IsNullOrEmpty(txtpt_phone_number.Text)) && (!string.IsNullOrEmpty(txtpt_dob.Text))
                        && (!string.IsNullOrEmpty(cbbSexth.Text))
                        )
            {
                DataTable dtCheckotoday;
                // oDate.ToString("yyyy-MM-dd", CultureInfo.GetCultureInfo("en-US"));
                DateTime dateTime = DateTime.Now;
                string strTable = "o" + dateTime.ToString("ddMMyy", CultureInfo.GetCultureInfo("th-TH"));
                dtCheckotoday = HI7.Class.HIUility.checkODDMMYY(strTable);
                Registerappoint();//ลงทะเบียน Ovst
                VN = this.getVN();//ดึง VN ที่ลงทะเบียนล่าสุด
                getLX();//สั่งรายการ ตามนัด
                if (dtCheckotoday != null)
                { //มีตาราง o รายวันของวันนี้แล้ว
                    try { this.OTODAY(strTable); } catch { Growl.Warning("สร้างแฟ้มประจำวันไม่สำเร็จ!!"); }
                    try { this.hi7visittoday(); } catch { Growl.Warning("สร้างแฟ้มประจำวันHi7ไม่สำเร็จ!!"); }
                }
                else
                {
                    try { CREATE_TABLE_OTODAY(); } catch { };//สร้างตาราง OTODAY รายวัน
                    try { this.OTODAY(strTable); } catch { Growl.Warning("สร้างแฟ้มประจำวันไม่สำเร็จ!!"); }
                    try { this.hi7visittoday(); } catch { Growl.Warning("สร้างแฟ้มประจำวันHi7ไม่สำเร็จ!!"); }
                }
                updateOapp();//หลังลงนัดเสร็จแก้ไขสถานะ fuok = 1
                updatePtldate();//อัพเดต Ptldate                                 
                AuthencodeandInsure();//การขอ Authencode
                Rtservicetypefu();//ลงตาราง Registerfu               
                //updateStatusscreen();//อัพเดตสถานนะแฟ้มคัดกรอง Y
                //getodata_Screen();//เรียกตารางคิวใหม่
                HI7.Class.HIUility._VN = VN; //get VN เพื่อเอาไว้เรียกใช้งาน Q4U,อื่น ๆ 
                HI7.Class.HIUility._HN = this.txtpt_hn.Text; //get VN เพื่อเอาไว้เรียกใช้งาน Q4U,อื่น ๆ 
                HI7.Class.HIUility._CLN = this.cbbClinic.SelectedValue.ToString(); //get CLN
                HI7.Class.HIUility._PriorityIDQ4U = this.cbbpt_queuetype.SelectedValue.ToString(); //get ประเภทคิว
                HI7.Class.HIUility._PnameQ4U = this.cbbPname.Text; //get value 
                HI7.Class.HIUility._PTTYPE = this.cbb_pttype.SelectedValue.ToString(); //get pttype q4u
                HI7.Class.HIUility._PrintPriority = this.cbbpt_queuetype.Text.ToString();
                if (this.cbbSexth.Text == "ชาย")
                {
                    HI7.Class.HIUility._SexQ4U = "1";// get code ชาย
                }
                else
                {
                    HI7.Class.HIUility._SexQ4U = "2";// get code หญิง
                }
                HI7.Class.HIUility._FnameQ4U = this.txtpt_fname.Text;//get value fname
                HI7.Class.HIUility._LnameQ4U = this.txtpt_lname.Text; //get value lname
                HI7.Class.HIUility._BrthdateQ4U = HI7.Class.HIUility.DateTranform1(this.txtpt_dob.Text); //get date 12/03/2524 to 20220101 
                HI7.Class.HIUility._BrthdateQ4UPrint = this.txtpt_dob.Text;
                if (cb_print.IsChecked == true)//ถ้าเคลิ๊กพิมพ์บัตรคิว
                {
                    if (String.IsNullOrEmpty(HI7.Class.HIUility.printerid))//เช็คเครื่องพิมพ์ เก็ทเวย์
                    {
                        HI7.Class.HIUility.getLoginQ4U();
                        if (HI7.Class.HIUility._StrQueueNumber != null)
                        {
                            Mdr.Forms.frmPrintQHi7.strCheckSetting = "0";
                            frmPrintQHi7 f = new frmPrintQHi7();
                            f.ShowDialog();

                            f.Close();
                            ClearDatareset();
                        }
                        else
                        {
                            Growl.Warning("ไม่ได้หมายเลขคิว");
                            ClearDatareset();
                        }
                    }
                    else
                    {
                        HI7.Class.HIUility.getLoginQ4U();
                        if (HI7.Class.HIUility._StrQueueNumber != null)
                        {
                            HI7.Class.HIUility.getPrintQ4U();//print Getway
                            ClearDatareset();
                        }
                        else
                        {
                            Growl.Warning("ไม่ได้หมายเลขคิว");
                            ClearDatareset();
                        }

                    }

                }
                else//ไม่พิมพ์บัตรคิว
                {
                    HI7.Class.HIUility.getLoginQ4U();//ลงทะเบียนคิว
                    //ClearDatareset();//ล้างฟอร์ม
                }
                ClearData();
                Growl.Success("ลงทะเบียนตามนีดสำเสร็จ");
                getVisit();//ดึงข้อมูลในการลงทะเบียน
            }
            else {
                Growl.Warning("มีช่องว่างกรุณาตรวจสอบ!!!");
            }
            
        }
        bool Registerappoint()//ฟังก์ชั่นลงทะเบียน ovst
        {
            string strValues = this.txtpt_hn.Text + ", NOW(),'" + this.cbb_pttype.SelectedValue + "','" + this.cbbClinic.SelectedValue + "','" + strstaff + "'";
            string strField = "hn,vstdttm,pttype,cln,register";
            string strSQL = "insert into ovst (" + strField + ") values(" + strValues + ")";
                Dictionary<string, object> dictData = new Dictionary<string, object>();
                dictData.Add("query", strSQL);
                bool status = Hi7.Class.APIConnect.Excecute("query/hi7_query", "POST", dictData);
                if (status == true)
                {return true;}
                else { return false; } 
        }
        void getLX() {
            DataTable dt;
            string hn = this.txtpt_hn.Text;
            dt = HI7.Class.HIUility.getAppointment(hn);
            DataRow drDt;
            drDt = dt.Rows[0];
            string chkcln = drDt["chkcln"].ToString();
            string chkcln1 = drDt["cln"].ToString();
            if (chkcln == "20100" || chkcln == "30100")
            {
                try{
                    DataTable dtEr = dt.Select("grpcln = '1' AND vn = " + HI7.Class.HIUility._VNAPPOINT + "").CopyToDataTable();
                    DataRow drEr;
                    foreach (DataRow drEr1 in dtEr.Rows)
                    {
                        string namecode = drEr1[10].ToString();
                        int countchkcln = drEr1[5].ToString().Length - 1;
                        string cutstringEr = chkcln1.Substring(1, countchkcln);
                        string lfudate = drEr1[11].ToString();
                        string dct = drEr1[12].ToString();
                        string oappid = drEr1[0].ToString();
                        string vn = drEr1[4].ToString();
                        string dscrptn = drEr1[10].ToString();
                        string code = HI7.Class.HIUility.getCodeicd9id(cutstringEr);
                        CREATE_TABLE_OPRT(cutstringEr, lfudate, code, dct, oappid, dscrptn);//บันทึกแฟ้ม oprt หัตถการ
                    }
                }
                catch {}
                try {
                    
                        DataTable dt2 = dt.Select("grpcln = '2' AND vn = " + HI7.Class.HIUility._VNAPPOINT + "").CopyToDataTable();
                        DataRow dr3;
                        foreach (DataRow dr2 in dt2.Rows)
                        {
                            string namecode = dr2[10].ToString();
                            string code = dr2[8].ToString();
                            string checktype = dr2[5].ToString();
                            string cutstring = checktype.Substring(0, 1);
                            string lfudate = dr2[11].ToString();
                            string dct = dr2[12].ToString();
                            string oappid = dr2[0].ToString();
                            string vn = dr2[4].ToString();
                            if (cutstring == "7")
                            {
                                string idcln = "";
                                DataTable dt3 = dt.Select("grpcln = '1' AND vn = " + HI7.Class.HIUility._VNAPPOINT + "").CopyToDataTable();
                                dr3 = dt3.Rows[0];
                                if (dr3 != null)
                                {
                                    idcln = dr3["idcln"].ToString();
                                }
                                try
                                {
                                    CREATE_TABLE_LBBK(code, lfudate, dct, idcln, oappid);
                                    Growl.Success("สั่งรายการแลบ " + namecode + " แล้วครับ");
                                }
                                catch
                                {
                                    Growl.Warning("สั่งรายการแลบ " + namecode + " ไม่สำเร็จ");
                                }


                                //callfunctionCreateXryrqt(xraycode);
                            }
                            else if (cutstring == "8")
                            {
                                try
                                {
                                    CREATE_TABLE_XRYRQT(code, oappid);
                                    Growl.Success("สั่งรายการเอ็กซเรย์ " + namecode + " แล้วครับ");
                                }
                                catch
                                {
                                    Growl.Warning("สั่งรายการเอ็กซเรย์ " + namecode + " ไม่สำเร็จ");
                                }

                            }
                            else if (cutstring == "2")
                            {
                                try
                                {
                                    //CREATE_TABLE_XRYRQT(code, oappid);
                                    Growl.Success("สั่งรายการเอ็กซเรย์ " + namecode + " แล้วครับ");
                                }
                                catch
                                {
                                    Growl.Warning("สั่งรายการเอ็กซเรย์ " + namecode + " ไม่สำเร็จ");
                                }

                            }

                        }
                    }
                catch {}
                

            }
            else {
                try {
                    DataTable dt2 = dt.Select("grpcln = '2' AND vn = " + HI7.Class.HIUility._VNAPPOINT + "").CopyToDataTable();
                    DataRow dr3;
                    foreach (DataRow dr2 in dt2.Rows)
                    {
                        string namecode = dr2[10].ToString();
                        string code = dr2[8].ToString();
                        string checktype = dr2[5].ToString();
                        string cutstring = checktype.Substring(0, 1);
                        string lfudate = dr2[11].ToString();
                        string dct = dr2[12].ToString();
                        string oappid = dr2[0].ToString();
                        string vn = dr2[4].ToString();
                        if (cutstring == "7")
                        {
                            string idcln = "";
                            DataTable dt3 = dt.Select("grpcln = '1' AND vn = " + HI7.Class.HIUility._VNAPPOINT + "").CopyToDataTable();
                            dr3 = dt3.Rows[0];
                            if (dr3 != null)
                            {
                                idcln = dr3["idcln"].ToString();
                            }
                            try
                            {
                                CREATE_TABLE_LBBK(code, lfudate, dct, idcln, oappid);
                                Growl.Success("สั่งรายการแลบ " + namecode + " แล้วครับ");
                            }
                            catch
                            {
                                Growl.Warning("สั่งรายการแลบ " + namecode + " ไม่สำเร็จ");
                            }


                            //callfunctionCreateXryrqt(xraycode);
                        }
                        else if (cutstring == "8")
                        {
                            try
                            {
                                CREATE_TABLE_XRYRQT(code, oappid);
                                Growl.Success("สั่งรายการเอ็กซเรย์ " + namecode + " แล้วครับ");
                            }
                            catch
                            {
                                Growl.Warning("สั่งรายการเอ็กซเรย์ " + namecode + " ไม่สำเร็จ");
                            }

                        }
                        else if (cutstring == "2")
                        {
                            try
                            {
                                //CREATE_TABLE_XRYRQT(code, oappid);
                                Growl.Success("สั่งรายการเอ็กซเรย์ " + namecode + " แล้วครับ");
                            }
                            catch
                            {
                                Growl.Warning("สั่งรายการเอ็กซเรย์ " + namecode + " ไม่สำเร็จ");
                            }

                        }

                    }
                }
                catch { 
                }
                
            }
        }
        bool updateOapp()//แก้ไขข้อมูลหลังจากลงทะเบียนนัดเสร็จ
        {
            try
            {
                //string bdate = getBirthDate(this.txtBirthDate.Text);
                string strSQL, strValues;
                strValues = "fuok='1'";
                strSQL = "UPDATE oapp set " + strValues + " WHERE id=" + HI7.Class.HIUility._IDOAPP;
                Dictionary<string, object> dictData = new Dictionary<string, object>();
                dictData.Add("query", strSQL);
                bool status = Hi7.Class.APIConnect.Excecute("query/hi7_query", "POST", dictData);
                if (status)
                {
                    return true;
                     
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                Growl.Error("updateOapp say:\r\n"+ex.Message);
                return false;
            }
        }

        bool CREATE_TABLE_OPRT(string cutstringEr, string lfudate, string code, string dct, string oappid,string dscrptn)//ฟังก์ชั่นบันทึกข้อมูลแลบที่นัด
        {
            string strSQLVSTIME = "SELECT DATE_FORMAT(date(ovst.vstdttm), '%Y%m%d') AS vstdate,TIME_FORMAT(time(ovst.vstdttm), '%H%i') AS vsttime, concat(DATE_FORMAT(date(ovst.vstdttm), '%Y-%m-%d'),' ',TIME_FORMAT(time(ovst.vstdttm), '%H:%i:%s'))as vstdttm,ovst.pttype,ovst.hn FROM ovst WHERE ovst.vn = '" + VN + "'";
            Dictionary<string, object> dictDataVSTIME = new Dictionary<string, object>();
            dictDataVSTIME.Add("query", strSQLVSTIME);
            DataRow drVS;
            string pttype = "";
            string vstdttm = "";
            string vstdate = "";
            string vsttime = "";
            string hn = "";

            try
            {
                DataTable dt = Hi7.Class.APIConnect.getDataTable("query/hi7_query", "POST", dictDataVSTIME);
                if (dt.Rows.Count > 0)
                {
                    drVS = dt.Rows[0];
                    vstdate = drVS[0].ToString();
                    vsttime = drVS[1].ToString();
                    vstdttm = drVS[2].ToString();
                    pttype = drVS[3].ToString();
                    hn = drVS[4].ToString();
                }
            }
            catch (Exception ex)
            {
                Growl.Error("CREATE_TABLE_OPRT say:\r\n" + ex.Message);
            }
            //strstaff = Hi7.Class.APIConnect.USER_IDLOGIN;
            string strField = "vn,opdttm,an,icd9cm,icd9name,codeicd9id";
            //string strValues = "'" + lfudate + "','" + code + "','" + VN + "','" + requestby + "','0','"+hn+"',"+"'0'"+",'"+ vstdttm + "','"+ vstdate + "','"+ vsttime + "','" + pttype + "','0','1',''";
            string strValues = "'" + VN + "','"+ vstdttm + "','0','" + cutstringEr + "','" + dscrptn + "','" + code + "'";
            string strSQL = "insert oprt (" + strField + ") values(" + strValues + ")";
            try
            {
                Dictionary<string, object> dictData = new Dictionary<string, object>();
                dictData.Add("query", strSQL);
                bool status = Hi7.Class.APIConnect.Excecute("query/hi7_query", "POST", dictData);
                if (status)
                {
                    updateOappLX(oappid);
                    return true;

                }
                else { return false; }
            }
            catch (Exception ex)
            {
                Growl.Error("CREATE_TABLE_LBBK INSERT say:\r\n" + ex.Message);
                return false;
            }

        }
        bool CREATE_TABLE_LBBK(string code, string lfudate, string dct, string idcln, string oappid)//ฟังก์ชั่นบันทึกข้อมูลแลบที่นัด
        {
            string strSQLVSTIME = "SELECT DATE_FORMAT(date(ovst.vstdttm), '%Y%m%d') AS vstdate,TIME_FORMAT(time(ovst.vstdttm), '%H%i') AS vsttime, concat(DATE_FORMAT(date(ovst.vstdttm), '%Y-%m-%d'),' ',TIME_FORMAT(time(ovst.vstdttm), '%H:%i:%s'))as vstdttm,ovst.pttype,ovst.hn FROM ovst WHERE ovst.vn = '" + VN + "'";
            Dictionary<string, object> dictDataVSTIME = new Dictionary<string, object>();
            dictDataVSTIME.Add("query", strSQLVSTIME);
            DataRow drVS;
            string pttype = "";
            string vstdttm = "";
            string vstdate = "";
            string vsttime = "";
            string hn = "";
            
            try
            {
                DataTable dt = Hi7.Class.APIConnect.getDataTable("query/hi7_query", "POST", dictDataVSTIME);
                if (dt.Rows.Count > 0)
                {
                    drVS = dt.Rows[0];
                    vstdate = drVS[0].ToString();
                    vsttime = drVS[1].ToString();
                    vstdttm = drVS[2].ToString();
                    pttype = drVS[3].ToString();
                    hn = drVS[4].ToString();
                    //try
                    //{
                    //    string ss = "1/7/2556 0:00:00";
                    //    DateTime dt1;

                    //    IFormatProvider c = CultureInfo.CreateSpecificCulture("th-TH");
                    //    DateTime.TryParse(ss, c, DateTimeStyles.None, out dt1);
                    //    IFormatProvider culture = CultureInfo.CreateSpecificCulture("en-US");
                    //    dateparse1.ToString("dd-MM-yyyy HH:MM:SS");
                    //}
                    //catch { 
                    //}
                }
            }
            catch (Exception ex)
            {
                Growl.Error("CREATE_TABLE_LBBK SELECT say:\r\n" + ex.Message);
            }
            //strstaff = Hi7.Class.APIConnect.USER_IDLOGIN;
            string requestby = "FU" + dct + "/" + idcln;
            string strField = "vn,an,hn,vstdttm,labcode,senddate,sendtime,pttype,requestby,labgroup,labcomment,hcode";
            //string strValues = "'" + lfudate + "','" + code + "','" + VN + "','" + requestby + "','0','"+hn+"',"+"'0'"+",'"+ vstdttm + "','"+ vstdate + "','"+ vsttime + "','" + pttype + "','0','1',''";
            string strValues = "'" + VN + "','0','" + hn + "','" + vstdttm + "','" + code + "','"+ vstdate + "','" + vsttime + "','" + pttype + "','" + requestby + "','"+"1"+"',"+"'',"+"''";
            string strSQL = "insert into lbbk (" + strField + ") values(" + strValues + ")";
            try
            {
                Dictionary<string, object> dictData = new Dictionary<string, object>();
                dictData.Add("query", strSQL);
                bool status = Hi7.Class.APIConnect.Excecute("query/hi7_query", "POST", dictData);
                if (status)
                {
                    updateOappLX(oappid);
                    return true;

                }
                else { return false; }
            }
            catch (Exception ex)
            {
                Growl.Error("CREATE_TABLE_LBBK INSERT say:\r\n" + ex.Message);
                return false;
            }

        }
        bool updateOappLX(string oappid)//แก้ไขข้อมูลหลังจากลงทะเบียนนัดเสร็จLX
        {
            try
            {
                //string bdate = getBirthDate(this.txtBirthDate.Text);
                string strSQL, strValues;
                strValues = "fuok='1'";
                strSQL = "UPDATE oapp set " + strValues + " WHERE id=" + oappid;
                Dictionary<string, object> dictData = new Dictionary<string, object>();
                dictData.Add("query", strSQL);
                bool status = Hi7.Class.APIConnect.Excecute("query/hi7_query", "POST", dictData);
                if (status)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                Growl.Error("updateOappLX UPDATE say:\r\n" + ex.Message);
                return false;
            }
        }
        bool CREATE_TABLE_XRYRQT(string code, string oappid)//ฟังก์ชั่นบันทึกข้อมูลเอ็กซเราย์ที่นัด
        {
            //หาวันที่
            string strSQLVSTIME = "SELECT DATE_FORMAT(date(ovst.vstdttm), '%Y%m%d') AS vstdate,TIME_FORMAT(time(ovst.vstdttm), '%H%i') AS vsttime FROM ovst WHERE ovst.vn = '" + VN + "'";
            Dictionary<string, object> dictDataVSTIME = new Dictionary<string, object>();
            dictDataVSTIME.Add("query", strSQLVSTIME);
            DataRow drVSTIME;
            string vstdate = "";
            string vsttime = "";
            string rqttime = "";
            try
            {
                DataTable dt = Hi7.Class.APIConnect.getDataTable("query/hi7_query", "POST", dictDataVSTIME);
                if (dt.Rows.Count > 0)
                {
                    drVSTIME = dt.Rows[0];
                    vstdate = drVSTIME[0].ToString();
                    vsttime = drVSTIME[1].ToString();
                    rqttime = timeNow();
                }
            }
            catch (Exception ex)
            {
                Growl.Error("CREATE_TABLE_XRYRQT SELECT say:\r\n" + ex.Message);
            }
            //strstaff = "00";
            string strField = "xryrqt.vn,xryrqt.an,xryrqt.hn,xryrqt.vstdate,xryrqt.xrycode,xryrqt.vsttime,xryrqt.rqttime";
            string strValues = VN + "," + "'0'" + "," + txtpt_hn.Text + ",'" + vstdate + "','" + code + "','" + vsttime + "','" + vsttime + "'";
            string strSQL = "insert into xryrqt (" + strField + ") values(" + strValues + ")";
            try
            {
                Dictionary<string, object> dictData = new Dictionary<string, object>();
                dictData.Add("query", strSQL);
                bool status = Hi7.Class.APIConnect.Excecute("query/hi7_query", "POST", dictData);
                if (status)
                {
                    updateOappLX(oappid);
                    return true;

                }
                else { return false; }
            }
            catch (Exception ex)
            {
                Growl.Error("CREATE_TABLE_XRYRQT INSERT say:\r\n" + ex.Message);
                return false;
            }

        }
        string timeNow()//แปลงเวลาปัจุบันให้เก็บเป็นนัมเมอร์ริก
        {
            DateTime dateTime = DateTime.Now;
            string strTable = dateTime.ToString("HHmm");
            int check = strTable.Length;
            try
            {
                if (check == 4)
                {
                    string xrytime = "";
                    String checkzeroone = strTable.Substring(0, 1);
                    String checkzerotwo = strTable.Substring(0, 2);
                    String checkzerotree = strTable.Substring(0, 3);
                    String checkzerofour = strTable.Substring(0, 4);
                    if (checkzerofour == "0000")
                    {
                        xrytime = "0";
                        return xrytime;
                    }
                    else if (checkzerotree == "000")
                    {
                        xrytime = strTable.Substring(3, 1);
                        return xrytime;
                    }
                    else if (checkzerotwo == "00")
                    {
                        xrytime = strTable.Substring(2, 2);
                        return xrytime;
                    }
                    else if (checkzeroone == "0")
                    {
                        xrytime = strTable.Substring(1, 3);
                        return xrytime;
                    }
                    else
                    {
                        xrytime = strTable;
                        return xrytime;
                    }
                }
            }
            catch (Exception ex)
            {
                return "";
            }
            return "";
        }
        bool updatePtldate()
        {  
            try
            {
                if (cb_changepttype.IsChecked == true)
                {
                    string pttype = "", hometel="";
                    string datescreening = DateTime.Now.ToString("yyyy-MM-dd", new CultureInfo("en-US"));
                    pttype = txtcbbpttype.Text;
                    hometel = txtpt_phone_number.Text;
                    string strSQL, strValues;
                    strValues = "ldate=" + "'" + datescreening + "',"+"pttype="+"'"+ pttype + "',"+"hometel='"+ hometel+"',"+"register = '"+ strstaff+"'";
                    strSQL = "UPDATE pt set " + strValues + " WHERE hn=" + this.txtpt_hn.Text;
                    Dictionary<string, object> dictData = new Dictionary<string, object>();
                    dictData.Add("query", strSQL);
                    bool status = Hi7.Class.APIConnect.Excecute("query/hi7_query", "POST", dictData);
                    if (status)
                    {
                        Growl.Success("ลงทะเบียนแก้ไข้สิทธิ์หลักให้เรียบร้อยแล้วครับ!!!");
                        return true;
                    }
                    else
                    {
                        Growl.Error("Update pt.ldate & pt.pttype Error!!!");
                        return false;
                    }
                }
                else {
                    string datescreening = DateTime.Now.ToString("yyyy-MM-dd", new CultureInfo("en-US"));
                    string hometel = "";
                    hometel = txtpt_phone_number.Text;
                    string strSQL, strValues;
                    strValues = "ldate=" + "'" + datescreening + "',"+"hometel='" + hometel + "'," + "register = '" + strstaff + "'";
                    strSQL = "UPDATE pt set " + strValues + " WHERE hn=" + this.txtpt_hn.Text;
                    Dictionary<string, object> dictData = new Dictionary<string, object>();
                    dictData.Add("query", strSQL);
                    bool status = Hi7.Class.APIConnect.Excecute("query/hi7_query", "POST", dictData);
                    if (status)
                    {
                        Growl.Success("Update pt.ldate Success!!!");
                        return true;
                    }
                    else
                    {
                        Growl.Error("Update pt.ldate Error!!!");
                        return false;
                    }
                }
                
            }
            catch (Exception ex)
            {
                Growl.Error("updatePtldate UPDATE say:\r\n" + ex.Message);
                return false;
            }
        }

        private void btnClear_Click(object sender, RoutedEventArgs e)
        {
            this.pBar.Visibility = Visibility.Collapsed;
            this.txtnhsdata_cid.Visibility = Visibility.Visible;
            this.lb_maininsclName.Visibility = Visibility.Visible;
            this.txtnshdata_maininsclName.Visibility = Visibility.Visible;
            this.lb_subinsclName.Visibility = Visibility.Visible;
            this.txtnshdata_subinsclName.Visibility = Visibility.Visible;
            this.lb_startdate.Visibility = Visibility.Visible;
            this.txtnshdata_startdate.Visibility = Visibility.Visible;
            this.lb_hmainName.Visibility = Visibility.Visible;
            this.txtnshdata_hmainName.Visibility = Visibility.Visible;
            this.lb_txthosub.Visibility = Visibility.Visible;
            this.txthosub.Visibility = Visibility.Visible;
            this.lb_expdate.Visibility = Visibility.Visible;
            this.txtexpdate.Visibility = Visibility.Visible;            
            ClearDatareset();
            this.getVisit();

        }
        private void AddMenuItem_Click(object sender, RoutedEventArgs e)
        {
            // โค้ดที่ต้องการให้ทำเมื่อคลิกที่เมนู "เพิ่มข้อมูล"
        }

        private void EditMenuItem_Click(object sender, RoutedEventArgs e)
        {
            // โค้ดที่ต้องการให้ทำเมื่อคลิกที่เมนู "แก้ไขข้อมูล"
        }

        private void DeleteMenuItem_Click(object sender, RoutedEventArgs e)
        {
            // โค้ดที่ต้องการให้ทำเมื่อคลิกที่เมนู "ลบข้อมูล"
        }
        private void btnEdittype_Click(object sender, RoutedEventArgs e)//แก้ไขสิทธิ์บริการย้อนหลัง
        {            
            if (!String.IsNullOrEmpty(cbbpttype.Text))
            {
                this.cbbpttype.BorderBrush = (SolidColorBrush)new BrushConverter().ConvertFromString("#FF599D7E");
                this.txtcbbpttype.BorderBrush = (SolidColorBrush)new BrushConverter().ConvertFromString("#FF599D7E");
            }
            else
            {
                this.cbbpttype.BorderBrush = System.Windows.Media.Brushes.Red;
                this.txtcbbpttype.BorderBrush = System.Windows.Media.Brushes.Red;
                Growl.Warning("กรุณาเลือกสิทธิ์การรักษา");
            }
            if ((!String.IsNullOrEmpty(cbbpttype.Text)) && (!String.IsNullOrEmpty(cbbClinic.Text)))
            {
                clickbk = "1";
                datepicker = "";
                string iDate = datetoday.Text;
                DateTime oDate = DateTime.Parse(iDate);
                datepicker = DateConvert(oDate.ToString("yyyy-MM-dd"));
                datebackvisit = "o" + oDate.ToString("ddMMyy");
                //แฟ้ม Ovst คลินิก และ สิทธิ์
                try { updateOvstCln(); } catch { }
                //แฟ้ม Insure สิทธิ์
                try { updateInsure(); } catch { }                
                this.GetVisitback();
                btnRegister.Visibility = Visibility.Visible;//คืนค่า
                //btnReferIn.Visibility = Visibility.Visible;//คืนค่า
                //btnReferOut.Visibility = Visibility.Visible;//คืนค่า
                                                            //cb_changepttype.Visibility = Visibility.Visible;
                ClearDatareset();
                Keyboard.Focus(this.tabregismdr);
            }
            else {
                Growl.Warning("ไม่เปลี่ยนสิทธิ์ได้กรุณาตรวจสอบ!!");
            }           

        }
        private void cbbpt_queuetype_KeyDown(object sender, KeyEventArgs e)
        {
            Keyboard.Focus(this.btnRegister);
        }

        private void txtpt_cid_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            //Regex regex = new Regex("[^0-9]");
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void ct_referin_Click(object sender, RoutedEventArgs e)
        {
            
            DataRowView dataRow;
            _VNGETDATAGRID = "";
            _HNGETDATAGRID = "";
            _FullnameDATAGRID = "";
            datepicker = "";
            try
            {
                dataRow = drgVisit.SelectedItem as DataRowView;
                _VNGETDATAGRID = dataRow.Row.ItemArray[0].ToString();
                _HNGETDATAGRID = dataRow.Row.ItemArray[1].ToString();
                _FullnameDATAGRID = dataRow.Row.ItemArray[5].ToString();

                if(GetReferintoday(_HNGETDATAGRID) == true)
                {
                    MessageBox.Show("(HN:" + _HNGETDATAGRID + ") " + _FullnameDATAGRID + "\r\n" + "วันนี้มีการลงทะเบียนรับ Refer in แล้ว");
                    return;
                }
                else
                {
                    GridMdr.Effect = new BlurEffect();
                    GridMdr.Visibility = Visibility.Visible;
                    frmReferins frmReferins = new frmReferins();
                    frmReferins.ShowDialog();
                    GridMdr.Effect = null;
                }


                
                
            }
            catch (Exception ex)
            {
                Growl.Warning("การดึงข้อมููลจากตารางลงทะเบียนไม่สำเร็จ\r\n" + ex.Message);
            }
        }

        private void btnExportexcel_Click(object sender, RoutedEventArgs e)
        {
            ExportToExcel(drgVisit);
        }
        private void ExportToExcel(DataGrid dataGrid)
        {            

            try
            {
                Microsoft.Office.Interop.Excel.Application excelApp = new Microsoft.Office.Interop.Excel.Application();
                excelApp.Visible = false; // Prevent Excel from being visible during export

                Microsoft.Office.Interop.Excel.Workbook workbook = excelApp.Workbooks.Add();
                Microsoft.Office.Interop.Excel.Worksheet worksheet = (Microsoft.Office.Interop.Excel.Worksheet)workbook.Worksheets[1];
                // Write headers
                for (int col = 0; col < dataGrid.Columns.Count; col++)
                {
                    worksheet.Cells[1, col + 1] = dataGrid.Columns[col].Header;
                }

                // Write data rows
                for (int row = 0; row < dataGrid.Items.Count; row++)
                {
                    for (int col = 0; col < dataGrid.Columns.Count; col++)
                    {
                        var cellValue = ((System.Data.DataRowView)dataGrid.Items[row]).Row.ItemArray[col].ToString();
                        worksheet.Cells[row + 2, col + 1] = cellValue;
                    }
                }
                string namefile = "ข้อมูลการลงทะเบียนประจำวัน" + HI7.Class.HIUility.DateTranform1(datetoday.Text);
                // Save the Excel file
                string exportPath = @"C:\HI7\export file\"+ namefile + ".xlsx";
                workbook.SaveAs(exportPath);

                // Clean up resources
                workbook.Close(false);
                excelApp.Quit();

                Marshal.ReleaseComObject(worksheet);
                Marshal.ReleaseComObject(workbook);
                Marshal.ReleaseComObject(excelApp);

                MessageBox.Show("Export successful!");
                OpenExcelFile(exportPath);
            }
            catch (Exception ex)
            {
                MessageBox.Show("ส่งออกผิดพลาด: " + ex.Message);
            }
            
        }        
        private void OpenExcelFile(string filePath)
        {
            try
            {
                Process.Start("explorer.exe", $@"/select,""{filePath}""");
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred while trying to open the Excel file: " + ex.Message);
            }
        }

        private void btnChangesaveAuthen_Click(object sender, RoutedEventArgs e)
        {
            this.GetNoAuthencode();
        }

        private void btnEditcln_Click(object sender, RoutedEventArgs e)//กดแก้ไขข้อมูลคลินิก
        {
            if (!String.IsNullOrEmpty(cbbClinic.Text))
            {
                this.cbbClinic.BorderBrush = (SolidColorBrush)new BrushConverter().ConvertFromString("#FF599D7E");
                this.txtcbbClinic.BorderBrush = (SolidColorBrush)new BrushConverter().ConvertFromString("#FF599D7E");
            }
            else
            {
                this.cbbClinic.BorderBrush = System.Windows.Media.Brushes.Red;
                this.txtcbbClinic.BorderBrush = System.Windows.Media.Brushes.Red;
                Growl.Warning("กรุณเลือกจุดรับบริการ");
            }            
            if (!String.IsNullOrEmpty(cbbClinic.Text))
            {      
                //แฟ้ม Ovst คลินิก และ สิทธิ์
                try { updateOvstCln(); } catch { }                              
                getVisit();
                btnRegister.Visibility = Visibility.Visible;//คืนค่า
                //btnReferIn.Visibility = Visibility.Visible;//คืนค่า
                //btnReferOut.Visibility = Visibility.Visible;//คืนค่า
                sppttypeovst.Visibility = Visibility.Visible;
                spappoint.Visibility = Visibility.Visible;
                spauthencode.Visibility = Visibility.Visible;
                spdct.Visibility = Visibility.Visible;
                sppttypept.Visibility = Visibility.Visible;
                spqueue.Visibility = Visibility.Visible;
                cb_servicetypefu.Visibility = Visibility.Visible;
                cb_changepttype.Visibility = Visibility.Visible;
                cb_Register.Visibility = Visibility.Visible;
                cb_print.Visibility = Visibility.Visible;
                cb_insure.Visibility = Visibility.Visible;
                ClearDatareset();
                Statuschangeinsure = "0";
                Keyboard.Focus(this.tabregismdr);
                
            }
            else
            {
                Growl.Warning("ไม่เปลี่ยนสิทธิ์และคลินิกได้กรุณาตรวจสอบ!!");
            }
            
        }
        bool UpdatePttypebk()//แก้ไขสิทธิ์ย้อนหลัง
        {
            try
            {
                string strSQL, strValues;
                //strValues = "cln = '" + cbbClinic.SelectedValue.ToString() + "'," + "pttype = '" + cbbpttype.SelectedValue.ToString() + "'";
                strValues = "pttype = '" + cbbpttype.SelectedValue.ToString() + "'";
                strSQL = "UPDATE ovst set " + strValues + " WHERE vn = '" + _VNGETDATAGRID + "'";
                Dictionary<string, object> dictData = new Dictionary<string, object>();
                dictData.Add("query", strSQL);
                bool status = Hi7.Class.APIConnect.Excecute("query/hi7_query", "POST", dictData);
                if (status)
                {

                    Growl.Success("แก้สิทธิ์บริการย้อนหลังเป็น " + cbbClinic.Text.ToString() + "แล้วครับ");
                    return true;
                }
                else
                {
                    Growl.Error("UpdatePttypebk การแก้ไขข้อมูลสิทธิ์มีปัญหา 5612");
                    return false;
                }
            }
            catch (Exception ex)
            {
                Growl.Error("UpdatePttypebk UPDATE say:\r\n" + ex.Message);
                return false;
            }
        }
        bool updateOvstCln()//update อัพเดตคลินิกและสิทธิ์
        {
            try
               {
                string strSQL, strValues="";
                strValues = "cln = '" + txtcbbClinic.Text + "'";                
                strSQL = "UPDATE ovst set " + strValues + " WHERE vn = '" + _VNGETDATAGRID+"'";
                Dictionary<string, object> dictData = new Dictionary<string, object>();
                dictData.Add("query", strSQL);
                bool status = Hi7.Class.APIConnect.Excecute("query/hi7_query", "POST", dictData);
                if (status)
                {
                   
                    Growl.Success("แก้ไขเป็นจุดบริการ " + cbbClinic.Text.ToString() + "แล้วครับ");
                    return true;
                }
                else
                {
                    Growl.Error("Error update change in ovst");
                    return false;
                }
            }
            catch (Exception ex)
            {
                Growl.Error("updateOvstCln UPDATE say:\r\n"+ex.Message);
                return false;
            }
        }

        private void btnChangesave_Click(object sender, RoutedEventArgs e)
        {
            GetVisitback();
        }

        void updateInsure()//update อัพเดตแฟ้มเก็บสิทธิรักษา
        {
            try
            {     
                string strSQL, strValues = "";
                DataRow dr;
                DataTable dt = HI7.Class.HIUility.CheckInsure(_HNGETDATAGRID,_VNGETDATAGRID);
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        dr = dt.Rows[0];
                        //หา id กรณีไม่มี VN                     
                        idinsure = (string)IsNullString(dr["id"]).ToString();
                        frmInsure frmInsure = new frmInsure();
                        frmInsure.ShowDialog();
                    }
                    else
                    {                       
                    }
                }
                else
                {
                    
                }                
            }
            catch (Exception ex)
            {                
            }
        }          
        //private void tabmdr_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        //{
        //    txtsearch.Focus();
        //}
        bool updateStatusscreen()
        {
            try
            {
                //string datescreening = DateTime.Now.ToString("yyyy-MM-dd", new CultureInfo("en-US"));
                string strSQL, strValues;
                strValues = "hi7screen.status_flg=" + "'Y'";
                strSQL = "UPDATE hi7screen set " + strValues + " WHERE hi7screen.idscreen=" + _IDSCREEN;
                Dictionary<string, object> dictData = new Dictionary<string, object>();
                dictData.Add("query", strSQL);
                bool status = Hi7.Class.APIConnect.Excecute("query/hi7_query", "POST", dictData);
                if (status)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                Growl.Error("updateStatusscreen UPDATE say:\r\n" + ex.Message);
                return false;
            }
        }
        private bool IsValidCheckPersonID(string pid)
        {

            char[] numberChars = pid.ToCharArray();

            int total = 0;
            int mul = 13;
            int mod = 0, mod2 = 0;
            int nsub = 0;
            int numberChars12 = 0;

            for ( int i = 0; i < numberChars.Length - 1; i++ )
            {
                int num = 0;
                int.TryParse(numberChars[i].ToString(), out num);

                total = total + num * mul;
                mul = mul - 1;

                //Debug.Log(total + " - " + num + " - "+mul);
            }

            mod = total % 11;
            nsub = 11 - mod;
            mod2 = nsub % 10;

            //Debug.Log(mod);
            //Debug.Log(nsub);
            //Debug.Log(mod2);


            int.TryParse(numberChars[12].ToString(), out numberChars12);

            //Debug.Log(numberChars12);

            if ( mod2 != numberChars12 )
                return false;
            else
                return true;
        }


        string getBirthDate(string bdate) {
            //  16 / 1 / 1982
            string[] arr = bdate.Split('/');
            bool m = arr[1].Length == 1;
            string mm = m ? "0" + arr[1] : arr[1];

            bool d = arr[0].Length == 1;
            string dd = d ? "0" + arr[0] : arr[0];

            string ddate = arr[2] + "-" + mm + "-" + dd;
            return ddate;

            
        }
        string getconvertBirthDate(string bdate)
        {
            //  16 / 1 / 1982
            string[] arr = bdate.Split('/');
            bool m = arr[1].Length == 1;
            string mm = m ? "0" + arr[1] : arr[1];

            bool d = arr[0].Length == 1;
            string dd = d ? "0" + arr[0] : arr[0];

            bool y = arr[2].Length == 1;
            string yyyy = d ? "0" + arr[2] : arr[2];



            //int year = int.Parse(ddate);
            int yearConvert = int.Parse(yyyy) - 543;
            string ddate = yearConvert + "-" + mm + "-" + dd;
            return ddate;
           
        }

        bool Register() {

            if (string.IsNullOrEmpty(this.txtpt_hn.Text))
            { 
                return false;
            }
            string strValues =this.txtpt_hn.Text+", NOW(),'"+this.txtcbbpttype.Text+ "','"+this.cbbClinic.SelectedValue+"','"+strstaff+"'";
            string strField ="hn,vstdttm,pttype,cln,register";
            string strSQL ="insert into ovst ("+strField+") values("+strValues+")";
            try
            {
            Dictionary<string, object> dictData = new Dictionary<string, object>();
            dictData.Add("query", strSQL); 
            bool status = Hi7.Class.APIConnect.Excecute("query/hi7_query", "POST", dictData);
                if ( status == true )
                {
                    Growl.Success("Insert Ovst success!!");
                    //ตัวแปรส่งเข้า INSURE
                    PTCID = txtpt_cid.Text.ToString();
                    PTFULLNAME = cbbPname.Text + txtpt_fname.Text + " "+txtpt_lname.Text;
                    PTHN = txtpt_hn.Text.ToString();
                    PTTYPE = cbbpttype.Text;
                    PTTYPECODE = txtcbbpttype.Text;
                    //ตัวแปรส่งเข้า Q4U
                    VN = getVN();// get vn 
                    HI7.Class.HIUility.GETVN = getVN();
                    HI7.Class.HIUility._VN = VN; //get VN เพื่อเอาไว้เรียกใช้งาน Q4U,อื่น ๆ 
                    HI7.Class.HIUility._HN = this.txtpt_hn.Text; //get VN เพื่อเอาไว้เรียกใช้งาน Q4U,อื่น ๆ 
                    HI7.Class.HIUility._CLN = this.cbbClinic.SelectedValue.ToString(); //get CLN
                    HI7.Class.HIUility._PriorityIDQ4U = this.cbbpt_queuetype.SelectedValue.ToString(); //get ประเภทคิว 
                    HI7.Class.HIUility._PnameQ4U = this.cbbPname.Text; //get value 
                    HI7.Class.HIUility._PTTYPE = txtcbbpttype.Text; //get pttype q4u
                    HI7.Class.HIUility._PrintPriority = this.cbbpt_queuetype.Text.ToString();
                    if (this.cbbSexth.Text == "ชาย")
                    {
                        HI7.Class.HIUility._SexQ4U = "1";// get code ชาย
                    }
                    else
                    {
                        HI7.Class.HIUility._SexQ4U = "2";// get code หญิง
                    }
                    HI7.Class.HIUility._FnameQ4U = this.txtpt_fname.Text;//get value fname
                    HI7.Class.HIUility._LnameQ4U = this.txtpt_lname.Text; //get value lname
                    HI7.Class.HIUility._BrthdateQ4U = HI7.Class.HIUility.DateTranform1(this.txtpt_dob.Text); //get date 20220101
                    HI7.Class.HIUility._BrthdateQ4UPrint = this.txtpt_dob.Text;
                    //ตรวจสอบคลินิกทันตกรรม
                    if (this.cbbClinic.SelectedValue.ToString() == "40100")
                    {
                        this.InsertDental();
                    }
                    else
                    {

                    }
                    return true;
                }
                else { 
                    return false; 
                }
            } catch (Exception ex) {
                Growl.Error("Register say:\r\n"+ex.Message);
                return false;
            }
          
        }
        //ลงตารางทันตกรรม
        bool InsertDental()
        {
            try
            {
                DataTable dtCheckotoday;
                // oDate.ToString("yyyy-MM-dd", CultureInfo.GetCultureInfo("en-US"));
                DateTime dateTime = DateTime.Now;
                string strField = "vn,hn,vstdttm,pttype,an";
                string strValues = "'" + HI7.Class.HIUility._VN + "'," + "'" + HI7.Class.HIUility._HN + "'," + "NOW()" + ",'" + HI7.Class.HIUility._PTTYPE + "'," + "'"+"'";
                string strSQL = "insert into dt (" + strField + ") values(" + strValues + ")";
                Dictionary<string, object> dictData = new Dictionary<string, object>();
                dictData.Add("query", strSQL);
                bool status = Hi7.Class.APIConnect.Excecute("query/hi7_query", "POST", dictData);
                if (status == true)
                {
                    return true;
                }
                else
                {
                    Growl.Error("สร้างตารางรายวันไม่สำเร็จ!!!");
                    return false;
                }
            }
            catch (Exception ex) {
                return false;
            }
            
        }
        //บันทึกแฟ้ม insure
        bool INSURE()
        {
            string hn, pop_id, card_id, pttype, datein, dateexp, hospmain, hospsub, note, notedate;
            hn = this.txtpt_hn.Text;
            pop_id = this.txtpt_cid.Text;            
            card_id = "";
            pttype = this.txtcbbpttype.Text;
            DateTime dateTime = DateTime.Now;
            if (JObject.Parse(jsonsmartAgent).SelectToken("startDateTime") != null)
            {
                datein = ((DateTime)JObject.Parse(jsonsmartAgent).SelectToken("startDateTime")).ToString("yyyy-MM-dd", CultureInfo.GetCultureInfo("en-US"));
            }
            else {
                datein = "";
            }
            if (JObject.Parse(jsonsmartAgent).SelectToken("expireDateTime") != null)//เช็ควันหมดอายุบัตร
            {
                dateexp = ((DateTime)JObject.Parse(jsonsmartAgent).SelectToken("expireDateTime")).ToString("yyyy-MM-dd", CultureInfo.GetCultureInfo("en-US"));
            }
            else
            {
                dateexp = "";
            }
            if (JObject.Parse(jsonsmartAgent).SelectToken("hospMain") != null)//เช็ควันหมดอายุบัตร
            {
                hospmain = JObject.Parse(jsonsmartAgent).SelectToken("hospMain").SelectToken("hcode").ToString();
            }
            else
            {
                hospmain = "";
            }
            if (JObject.Parse(jsonsmartAgent).SelectToken("hospSub") != null)//เช็ควันหมดอายุบัตร
            {
                hospsub = JObject.Parse(jsonsmartAgent).SelectToken("hospSub").SelectToken("hcode").ToString();
            }
            else
            {
                hospsub = "";
            }
            if (string.IsNullOrEmpty(txtnote_insure.Text))
            {
                note ="บันทึกข้อมูลโดย SmartServiceICT";
            }
            else
            {
                note = txtnote_insure.Text;
            }
            
            notedate = dateTime.ToString("yyyy-MM-dd", CultureInfo.GetCultureInfo("en-US")); 
            string strField = "hn,vn,pop_id,card_id,pttype,datein,dateexp,hospmain,hospsub,note,notedate";
            string strValues = "'"+hn+"',"+ "'"+VN+"'," + "'" + pop_id + "'," + "'" + card_id + "'," + "'" + pttype + "'," + "'" + datein + "'," + "'" + dateexp + "',"
                + "'" + hospmain + "'," + "'" + hospsub + "'," + "'" + note + "'," + "'" + notedate + "'";
            if (VN != null || VN != "")
            {
                string strSQL = "insert into insure (" + strField + ") values(" + strValues + ")";
                Dictionary<string, object> dictData = new Dictionary<string, object>();
                try
                {
                    dictData.Add("query", strSQL);
                    bool status = Hi7.Class.APIConnect.Excecute("query/hi7_query", "POST", dictData);
                    if (status)
                    {
                        Growl.Success("INSURE Success!!!");
                        return true;
                    }
                    else
                    {
                        Growl.Error("Error Insure!!!");
                        return false;
                    }
                }
                catch (Exception ex)
                {
                    Growl.Error("เกิดปํญหากับ API ลองใหม่อีกครั้ง\n", ex.Message);
                    return false;
                }
            }
            else {
                Growl.Error("มี VN เป็นค่าว่างไม่สามารถบันทึก INSURE ได้");
                return false;
            }           
        }
        bool InsureDataUCW()
        {
            string hn, pop_id, card_id, pttype, datein, dateexp, hospmain, hospsub, note, notedate;
            hn = this.txtpt_hn.Text;
            pop_id = this.txtpt_cid.Text;
            card_id = SMCCardID;
            pttype = this.txtcbbpttype.Text;
            DateTime dateTime = DateTime.Now;
            if (!string.IsNullOrEmpty(SMCStartdate))
            {
                datein = HI7.Class.HIUility.DateChange6(SMCStartdate);
            }
            else
            {
                datein = "0000-00-00";
            }            
            if (!string.IsNullOrEmpty(SMCExpdate))
            {
                dateexp = HI7.Class.HIUility.DateChange6(SMCExpdate);
            }
            else
            {
                dateexp = "0000-00-00";
            }
            
            hospmain = SMCEHmaincode;
            hospsub = SMCEHsubcode;
            if (!string.IsNullOrEmpty(txtnote_insure.Text))
            {
                note = txtnote_insure.Text;
            }
            else
            {
                note = "บันทึกข้อมูลโดย UCAuthentication";
            }            
            notedate = dateTime.ToString("yyyy-MM-dd", CultureInfo.GetCultureInfo("en-US"));
            string strField = "hn,vn,pop_id,card_id,pttype,datein,dateexp,hospmain,hospsub,note,notedate";
            string strValues = "'" + hn + "'," + "'" + VN + "'," + "'" + pop_id + "'," + "'" + card_id + "'," + "'" + pttype + "'," + "'" + datein + "'," + "'" + dateexp + "',"
                + "'" + hospmain + "'," + "'" + hospsub + "'," + "'" + note + "'," + "'" + notedate + "'";
            if (VN != null || VN != "")
            {
                string strSQL = "insert into insure (" + strField + ") values(" + strValues + ")";
                Dictionary<string, object> dictData = new Dictionary<string, object>();
                try
                {
                    dictData.Add("query", strSQL);
                    bool status = Hi7.Class.APIConnect.Excecute("query/hi7_query", "POST", dictData);
                    if (status)
                    {
                        Growl.Success("INSURE Success!!!");
                        return true;
                    }
                    else
                    {
                        Growl.Error("Error Insure!!!");
                        return false;
                    }
                }
                catch (Exception ex)
                {
                    Growl.Error("เกิดปํญหากับ API ลองใหม่อีกครั้ง\n", ex.Message);
                    return false;
                }
            }
            else
            {
                Growl.Error("มี VN เป็นค่าว่างไม่สามารถบันทึก INSURE ได้");
                return false;
            }
        }

        ////////////////////////////////////////////////////////
        //สร้างแฟ้มประจำวัน
        bool CREATE_TABLE_OTODAY()
        {
            DataTable dtCheckotoday;
            // oDate.ToString("yyyy-MM-dd", CultureInfo.GetCultureInfo("en-US"));
            DateTime dateTime = DateTime.Now;
            string strTable = "o" + dateTime.ToString("ddMMyy", CultureInfo.GetCultureInfo("th-TH"));    
                    string strSQL = "CREATE TABLE IF NOT EXISTS  " + strTable + "(vn int(11) NOT NULL, PRIMARY KEY(vn), hn int(8), fname char(25), lname char(25), male char(1)," +
                            "age char(4), allergy char(60), pttype char(2), vsttime int(4) NOT NULL, dct char(5)," +
                            "bw DECIMAL(5, 1) NOT NULL, tt DECIMAL(4, 1) NOT NULL, pr int(3) NOT NULL, rr int(3) NOT NULL, sbp int(3) NOT NULL, dbp int(3) NOT NULL, nrs int(1) NOT NULL," +
                            "dtr int(1) NOT NULL, dtt int(1) NOT NULL, lab int(1) NOT NULL, xry int(1) NOT NULL, er int(1) NOT NULL, ors int(1) NOT NULL, rec int(1) NOT NULL, phm int(1) NOT NULL, hpt int(1) NOT NULL, phy int(1) NOT NULL," +
                            "drxtime int(4) NOT NULL, fudate date default '0000-00-00'," +
                            "fus1 char(5), fus2 char(5), fus3 char(5), fus4 char(5), fus5 char(5)," +
                            "ldrug TINYINT(1) NOT NULL, INDEX vn(vn), INDEX hn(hn))";
                    Dictionary<string, object> dictData = new Dictionary<string, object>();
                    dictData.Add("query", strSQL);
                    bool status = Hi7.Class.APIConnect.Excecute("query/hi7_query", "POST", dictData);
            if (status == true) { 
                return true;
            }
            else
            {
                Growl.Error("สร้างตารางรายวันไม่สำเร็จ!!!");
                return false;
            }            
        }
        private void txthn_screen_SelectionChanged(object sender, RoutedEventArgs e)//ค้นหาข้อมูลใน tab 
        {
            string x = txthn_screen.Text;
            string cCkhLength = txthn_screen.Text;
            int n;
            bool isNumeric = int.TryParse(x, out n);
            if (cCkhLength.Length == 1)
            {
                try
                {
                    System.Data.DataView view = new System.Data.DataView(dtscreen);
                    DataTable selected = view.ToTable("Selected", false).Select("Convert(hn, 'System.String') LIKE '" + this.txthn_screen.Text + "%'").CopyToDataTable();
                    this.dataGridScreen.ItemsSource = selected.DefaultView;
                }
                catch (Exception ex)
                {
                    //Info.
                    HI7.Class.HIUility._TXTSEARCH = txthn_screen.Text;
                    getodata_Screen();
                }
            }
            else
            {
                if (isNumeric)
                {
                    try
                    {
                        System.Data.DataView view = new System.Data.DataView(dtscreen);
                        DataTable selected = view.ToTable("Selected", false).Select("Convert(hn, 'System.String') LIKE '" + this.txthn_screen.Text + "%'").CopyToDataTable();
                        this.drgVisit.ItemsSource = selected.DefaultView;
                    }
                    catch (Exception ex)
                    {
                        //Info.
                        HI7.Class.HIUility._TXTSEARCH = txthn_screen.Text;
                        getodata_Screen();
                    }

                }
                else
                {
                    try
                    {
                        System.Data.DataView view = new System.Data.DataView(dtscreen);
                        DataTable selected = view.ToTable("Selected", false).Select("fname like '" + this.txthn_screen.Text + "%'").CopyToDataTable();
                        this.drgVisit.ItemsSource = selected.DefaultView;
                    }
                    catch (Exception ex)
                    {
                        //Info.
                        HI7.Class.HIUility._TXTSEARCH = txthn_screen.Text;
                        getodata_Screen();
                    }
                }

            }
        }
        //บันทึก pt.gencid 
        bool Gencid(string cid)
        {
            //10949167717
            string strField = "gencid";
            string gencid = HI7.Class.HIUility._HCODE;
            string hn = txtpt_hn.Text;
            Int32 lengthInput = (gencid+hn).Length;
            Int32 popid = 13;
            Int32 ans = popid - lengthInput;
            string zero = "0";
            string countZero = "";
            string andZero = "";
            string cidgennew = "";
            for (Int32 i = 1;i<=ans; i++)
            {
                countZero = zero;
                andZero = andZero + countZero;
            }
            cidgennew = gencid + andZero + hn;
            
            string strValues = "'"+ cidgennew+"'";
            string strSQL = "UPDATE pt SET pt.gencid = "+ strValues+" WHERE pt.hn = "+"'"+ hn +"'";
            Dictionary<string, object> dictData = new Dictionary<string, object>();
            try
            {
                dictData.Add("query", strSQL);
                bool status = Hi7.Class.APIConnect.Excecute("query/hi7_query", "POST", dictData);
                if (status)
                {
                    Growl.Success("Insert" + gencid + "success!!");
                    // insert data
                    return true;

                }
                else
                {
                    return false;
                }


            }
            catch (Exception ex)
            {
                Growl.Error("Insert say:\r\n" + ex.Message);
                return false;
            }

        }
        //SaveData Today
        bool OTODAY(string table) 
        {
            //strstaff = "00";
            string age = HI7.Class.HIUility.Hn2AgeYY(this.txtpt_hn.Text);
            string sex = getAgeChange(this.cbbSexth.Text);
            string vsttime = HI7.Class.HIUility.TimeOtoday();
            //Int64 age = HI7.Class.HIUility.Hn2Age(this.txtpt_hn.Text);
            DateTime dateTime = DateTime.Now;
            string strTable = "o"+dateTime.ToString("ddMMyy", CultureInfo.GetCultureInfo("th-TH"));
            string strValues, strField, strSQL;
            if (this.cbbClinic.SelectedValue.ToString() == "40100")//เช็คเงื่อนไขถ้าเป็น ทันตกรรม
            {
                strField = "vn,hn,male,fname,lname,age,pttype,vsttime,dtt";
                strValues = VN + "," + this.txtpt_hn.Text + ",'" + sex + "','" + this.txtpt_fname.Text + "','" + this.txtpt_lname.Text + "'," + age + ",'" + this.txtcbbpttype.Text + "'," + "'" + vsttime + "',"+"'"+"1"+"'";
                strSQL = "INSERT INTO " + table + "(" + strField + ") values(" + strValues + ")";
            }
            else
            {
                strValues = VN + "," + this.txtpt_hn.Text + ",'" + sex + "','" + this.txtpt_fname.Text + "','" + this.txtpt_lname.Text + "'," + age + ",'" + this.txtcbbpttype.Text + "'," + "'" + vsttime + "'";
                strField = "vn,hn,male,fname,lname,age,pttype,vsttime";
                strSQL = "INSERT INTO " + table + "(" + strField + ") values(" + strValues + ")";
            }
       
            Dictionary<string, object> dictData = new Dictionary<string, object>();

            try
            {
                dictData.Add("query", strSQL);
                bool status = Hi7.Class.APIConnect.Excecute("query/hi7_query", "POST", dictData);
                if ( status )
                {
                    Growl.Success("Insert"+ table + "success!!");
                    // insert data
                    return true;

                }
                else {
                    return false;
                }

          
            }
            catch ( Exception ex )
            {
                Growl.Error("OTODAY say:\r\n" + ex.Message);
                return false;
            }

        }

        private void btnUpdateCard_Click(object sender, RoutedEventArgs e)//แก้ไขข้อมูลตามบัตรประชาชน
        {
            if (IsValidCheckPersonID(this.txtIDCard.Text))
            {
                try {
                    string hntest = "";
                    updatePtfromcard();
                    hntest = this.txtpt_hn.Text;
                    this.txtsearch.Text = this.txtpt_hn.Text;
                    ClearDataresetHis();
                    _PRYORITY = this.cbbpt_queuetype.Text;
                    _CLN = this.cbbClinic.Text;
                    this.cbbChanwat.SelectedItem = null;
                    this.cbbAmphur.SelectedItem = null;
                    this.cbbTumbon.SelectedItem = null;
                    this.cbbVillage.SelectedItem = null;
                    strProvince_code = "";
                    strAmphur_code = "";
                    strTumbon_code = "";
                    strVillage_code = "";
                    getPatientInfo("hn", hntest);
                    this.cbbpt_claimtype.SelectedIndex = 0;
                    if (jsonsmartAgent != null || jsonsmartAgent != "")
                    {
                        nhso_smartcard_readpttype();
                    }
                    else
                    {
                        this.UcwsnhsoCID(this.txtpt_cid.Text);
                    }
                    Keyboard.Focus(btnRegister);
                }
                catch
                {
                }               
            }
            //btnUpdateCard.Visibility = Visibility.Collapsed;

        }

        private void cb_bdk_Checked(object sender, RoutedEventArgs e)
        {
            txtpt_contact_address.Text = txtpt_HouseNo.Text+" "+cbbVillage.Text + " ต." + cbbTumbon.Text + " อ." + cbbAmphur.Text + " จ." + cbbChanwat.Text;
        }

        private void txtfindregister_SelectionChanged(object sender, RoutedEventArgs e)
        {
            string x = txtfindregister.Text;
            string cCkhLength = txtfindregister.Text;
            int n;
            bool isNumeric = int.TryParse(x, out n);
            if (cCkhLength.Length == 1)
            {
                try
                {
                    System.Data.DataView view = new System.Data.DataView(dtvisit);
                    DataTable selected = view.ToTable("Selected", false).Select("Convert(hn, 'System.String') LIKE '" + this.txtfindregister.Text + "%'").CopyToDataTable();
                    this.drgVisit.ItemsSource = selected.DefaultView;
                }
                catch (Exception ex)
                {
                    //Info.
                    HI7.Class.HIUility._TXTSEARCH = txtfindregister.Text;
                    getVisit();
                }
            }
            else
            {
                if (isNumeric)
                {
                    try
                    {
                        System.Data.DataView view = new System.Data.DataView(dtvisit);
                        DataTable selected = view.ToTable("Selected", false).Select("Convert(hn, 'System.String') LIKE '" + this.txtfindregister.Text + "%'").CopyToDataTable();
                        this.drgVisit.ItemsSource = selected.DefaultView;
                    }
                    catch (Exception ex)
                    {
                        //Info.
                        HI7.Class.HIUility._TXTSEARCH = txtfindregister.Text;
                        getVisit();
                    }

                }
                else
                {
                    try
                    {
                        System.Data.DataView view = new System.Data.DataView(dtvisit);
                        if (string.IsNullOrEmpty(this.txtfindregister.Text))
                        {
                            this.drgVisit.ItemsSource = view;
                        }
                        else
                        {
                            DataTable selected = view.ToTable("Selected", false).Select("fname like '" + this.txtfindregister.Text + "%'").CopyToDataTable();
                            this.drgVisit.ItemsSource = selected.DefaultView;
                        }
                       
                    }
                    catch (Exception ex)
                    {
                        //Info.
                        HI7.Class.HIUility._TXTSEARCH = txtfindregister.Text;
                        getVisit();
                    }
                }

            }
        }

        private void ct_changedate_Click(object sender, RoutedEventArgs e)
        {
            _VNGETDATAGRID = null;
            DataRowView dataRow;
            try
            {
                dataRow = drgVisit.SelectedItem as DataRowView;
                _VNGETDATAGRID = dataRow.Row.ItemArray[0].ToString();
                _HNGETDATAGRID = dataRow.Row.ItemArray[1].ToString();
                _FullnameDATAGRID = dataRow.Row.ItemArray[2].ToString();
                Statuschangeinsure = "1";
                clickbk = "1";
                datepicker = "";
                string iDate = datetoday.Text;
                DateTime oDate = DateTime.Parse(iDate);
                datepicker = DateConvert(oDate.ToString("yyyy-MM-dd"));
                datebackvisit = "o" + oDate.ToString("ddMMyy");
                updateInsure();
            }
            catch (Exception ex)
            {
                Growl.Warning("การดึงข้อมููลจากตารางลงทะเบียนไม่สำเร็จ\r\n" + ex.Message);
            }
            GetVisitback();
        }

        private void datetoday_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            datepicker = "";
            string iDate = datetoday.ToString();
            DateTime oDate = DateTime.Parse(iDate);
            string aDate = oDate.ToString("yyyy-MM-dd");
            datepicker = DateConvert(aDate);
            datetoday.Text = oDate.ToString("dd/MM/yyyy");

            

            this.GetVisitback();
            btnRegister.Visibility = Visibility.Visible;//คืนค่า
            //btnReferIn.Visibility = Visibility.Visible;//คืนค่า
            //btnReferOut.Visibility = Visibility.Visible;//คืนค่า
            //cb_changepttype.Visibility = Visibility.Visible;
            ClearDatareset();
            Keyboard.Focus(this.tabregismdr);
        }

        private void txt_cbbpttype_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                try
                {
                    if (string.IsNullOrEmpty(this.txt_cbbpttype.Text))
                    {
                        Growl.Warning("กรุณาระบุการค้นหาด้วยรหัสสิทธิ์!!");
                        this.GetPttypeIdPT(txt_cbbpttype.Text);                  
                    }
                    else
                    {
                        try
                        {
                            this.GetPttypeIdPT(txt_cbbpttype.Text);
                           
                        }
                        catch (Exception ex)
                        {
                            Growl.Error("หาสิทธิ์ไม่เจอตรวจสอบตาราง pttype");
                        }
                    }
                }
                catch (Exception ex)
                {
                    Growl.Error(ex.Message);
                }
            }
        }

        private void date_dthdate_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {

            //string iDate_date_dthdate = datetoday.ToString();
            //DateTime oDate_date_dthdate = DateTime.Parse(iDate_date_dthdate);
            //DateTime newDate = oDate_date_dthdate.AddYears(543);
            //date_dthdate.Text = newDate.ToString("dd/MM/yyyy");

            // แสดงผลลัพธ์
        }

        private void cbb_pttype_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                if (cbb_pttype.SelectedValue != null)
                {
                    string valuepttype = cbb_pttype.SelectedValue.ToString();
                    this.txt_cbbpttype.Text = valuepttype;
                }
                else
                {
                    this.txt_cbbpttype.Text = "";
                }

            }
            catch
            {

            }
        }

        //D2CBE พ.ศ. '2565-18-04' ---> ค.ศ.'2023-18-04'
        public static string DateConvert(string ldDATE)
        {
            try
            {
                string[] arr = ldDATE.Split('-');
                bool m = arr[1].Length == 1;
                string mm = m ? "0" + arr[1] : arr[1];

                bool d = arr[2].Length == 1;
                string dd = d ? "0" + arr[2] : arr[2];

                int YY = int.Parse(arr[0]) - 543;

                string ddate = YY+ "-"+mm+"-"+dd;
                return ddate;
            }
            catch (Exception ex)
            {
                return "";
            }

        }
        private void txtcbbClinic_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void txtcbbpttype_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                try
                {
                    if (this.txtcbbpttype.Text == "")
                    {
                        Growl.Warning("กรุณาระบุการค้นหาด้วยรหัสสิทธิ์!!");
                        this.getPttypeId(txtcbbpttype.Text);
                        GetnoteInsure();
                    }
                    else {
                        try
                        {
                            this.getPttypeId(txtcbbpttype.Text);
                            GetnoteInsure();
                            Keyboard.Focus(this.txtnote_insure);
                        }
                        catch (Exception ex)
                        {
                            Growl.Error("เกิดข้อผิดพลาด");
                        }
                    }
                }
                catch (Exception ex)
                {
                    Growl.Error(ex.Message);
                }
            }
            
        }
        private void GetnoteInsure()
        {
            string strSql = "SELECT insure.pttype,CONCAT('(',insure.pttype,')',pttype.namepttype)as namepttype,insure.note" +
                            " FROM insure" +
                            " LEFT JOIN pttype on pttype.pttype = insure.pttype" +
                            " WHERE insure.hn =" + txtpt_hn.Text +
                            " ORDER BY insure.id DESC LIMIT 1";
            DataRow dr;
            DataTable dt = new System.Data.DataTable();
            Dictionary<string, object> dictData = new Dictionary<string, object>();
            dictData.Add("query", strSql);

            try
            {
                dt = Hi7.Class.APIConnect.getDataTable("query/hi7_query", "POST", dictData);
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        dr = dt.Rows[0];
                        txtnote_insure.Text = dr.ItemArray[2].ToString();
                    }
                    else
                    {
                        txtnote_insure.Text = "";
                    }
                }
                else
                {
                    txtnote_insure.Text = "";
                }
            }
            catch (Exception ex)
            {
                txtnote_insure.Text = "";
            }
        }

        private void txtcbbClinic_KeyDown(object sender, KeyEventArgs e)
        {
            string txtCln = txtcbbClinic.Text;
            if (e.Key == Key.Enter)
            {
                try
                {
                    if (this.txtcbbClinic.Text == "")
                    {
                        Growl.Warning("กรุณาระบุการค้นหาด้วยรหัสคลินิก!!");
                        this.getPtclinicId(txtCln);
                        //this.cbbpttype.Text = "";
                    }
                    else
                    {
                        
                        try
                        {
                            this.getPtclinicId(txtCln);
                            if (!string.IsNullOrEmpty(cbbpt_queuetype.Text))
                            {
                                Keyboard.Focus(btnRegister);
                            }
                            else
                            {
                              
                                Keyboard.Focus(cbbpt_queuetype);
                                //cbbpt_queuetype.IsDropDownOpen = true;
                            }
                            
                            
                        }
                        catch (Exception ex)
                        {
                            Growl.Error("เกิดข้อผิดพลาด");
                        }
                    }
                }
                catch (Exception ex)
                {
                    Growl.Error(ex.Message);
                }
            }

        }

        private void txtsearchfname_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                ClearData();
                string checkFname = txtsearchfname.Text;
                this.cbbpt_queuetype.Text = "ปกติ";
                //Int32 lengthInput = this.txtHnPopidSearch.Text.Length;
                if (checkFname != "")//เงื่อนไข Fname ไม่ว่าง
                {
                    try
                    {
                        statusOpenfroms = "N";
                        HI7.Class.HIUility._TXTSEARCH = checkFname;
                        frmSearchInfro frmSearchInfro = new frmSearchInfro();
                        frmSearchInfro.ShowDialog();
                        if (statusOpenfroms == "Y") {
                            return;
                        }
                        
                        if (GetDadehn(searchhn) == true)
                        {

                            Growl.Warning("ผู้มารับบริการได้เสียชีวิตแล้ว\r\nไม่สามารถลงทะเบียนได้");
                            //return;

                        }
                        else
                        {
                            //return;
                        }
                        if (GetAdmithn(searchhn) == true)
                        {
                            Growl.Warning("ผู้มารับบริการสถานะยังไม่ได้ Discharge\r\nไม่สามารถลงทะเบียนได้");
                            //return;
                        }
                        else
                        {

                        }
                        if (GetRegistertodayhn(searchhn) == true)
                        {
                            if (MessageBox.Show("วันนี้มีการเข้ารับริการแล้ว\r\nตรวจสอบใน Tab การตารางผู้ลงทะเบียน", "เตือน HN:" + searchhn + " มีการลงทะเบียนแล้ว", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.No)
                            {
                                this.getVisit();
                                return;
                            }
                            else
                            {
                                if (Getcheck24hr(searchhn) == true)
                                {
                                    string messageBoxText = "ไม่สามารถลงทะเบียนได้ เนื่องจากยังไม่ครบ 4 ชั่วโมงในกลุ่มสิทธิ์(อปท.)\r\n หากยืนยันการลงทะเบียนกด Yes หากไม่กดดำเนินการกด No";
                                    string caption = "ดำเนินการต่อไป";
                                    MessageBoxButton button = MessageBoxButton.YesNo;
                                    MessageBoxImage image = MessageBoxImage.Warning;
                                    MessageBoxResult result = MessageBox.Show(messageBoxText, caption, button, image);
                                    if (result == MessageBoxResult.Yes)
                                    {
                                        Growl.Warning("จะมีผลต่อการเครมตามเกณฑ์ต้องเข้ารับบริการหลัง 4 ชั่วโมงของสิทธิ์\r\nอปท.และจ่ายตรงกรมบัญชีกลาง ในการเครมด้วยสิทธิ์ LGO/OFC");
                                    }
                                    else
                                    {
                                        return;
                                    }
                                }
                            }

                        }
                        else
                        {

                        }
                        try
                            {
                                DataTable dtappoint;
                                dtappoint = HI7.Class.HIUility.getAppointment(searchhn);
                                if (dtappoint != null && dtappoint.Rows.Count > 0)
                                {
                                    //เช็คนัด
                                    checkAppoint(searchhn);
                                    getPatientInfoappoint("hn", searchhn);
                                if (this.txtsearch.Text != null || this.txtsearch.Text != "")
                                {
                                    this.UcwsnhsoCID(txtpt_cid.Text);
                                }
                                BitmapToBitmapImageGetpt();
                                    this.cbbpt_claimtype.SelectedIndex = 0;//กดหนด Defalut
                                    this.txtsend_appoint.Text = "มาตามนัด";
                                    Keyboard.Focus(this.tabmdr);
                                    this.txtsearchfname.Text = "";
                                    this.cbbpt_queuetype.Text = "ปกติ";
                                }
                                else
                                {
                                    getPatientInfo("hn", searchhn);
                                if (this.txtsearch.Text != null || this.txtsearch.Text != "")
                                {
                                    this.UcwsnhsoCID(txtpt_cid.Text);
                                }
                                BitmapToBitmapImageGetpt();
                                    this.cbbpt_claimtype.SelectedIndex = 0;//กดหนด Defalut
                                    Keyboard.Focus(this.tabmdr);
                                    this.txtsearchfname.Text = "";
                                    this.cbbpt_queuetype.Text = "ปกติ";
                            }
                            }
                            catch (Exception ex)
                            {
                            this.txtsearchfname.Text = "";
                            Growl.Error("เกิดข้อผิดพลาด");
                                //MessageBox.Show("เกิดข้อผิดพลาด");
                            }
                    }
                    catch (Exception ex) {
                        this.txtsearchfname.Text = "";
                        Growl.Error("txtsearchfname_KeyDown say:" + ex.Message); }
                    }
                }
            }

        private void cbbpttype_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try {
                if (cbbpttype.SelectedValue != null) {
                    string valuepttype = cbbpttype.SelectedValue.ToString();
                    this.txtcbbpttype.Text = valuepttype;
                }
                else
                {
                    this.txtcbbpttype.Text = "";
                }
                
            }
            catch
            {

            }
            
        }

        private void cbbClinic_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                if (cbbClinic.SelectedValue != null) {
                    string valueclinic = cbbClinic.SelectedValue.ToString();
                    string txtClinic = cbbClinic.Text;
                    this.txtcbbClinic.Text = valueclinic;
                    cbbClinic.Text = txtClinic;
                }
                else
                {
                    cbbClinic.Text = "";
                    txtcbbClinic.Text = "";
                }
                
            }
            catch
            {

            }
           
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)//กดคีย์บอร์ดปุ่ม F2/F3
        {
            switch (e.Key)
            {
                case Key.F2:
                    e.Handled = true;
                    GridMdr.Effect = new BlurEffect();
                    GridMdr.Visibility = Visibility.Visible;
                    frmReferins frmReferins = new frmReferins();
                    frmReferins.ShowDialog();
                    GridMdr.Effect = null;
                    break;

                case Key.F3:
                    e.Handled = true;
                    GridMdr.Effect = new BlurEffect();
                    GridMdr.Visibility = Visibility.Visible;
                    frmReferOut frmReferOut = new frmReferOut();
                    frmReferOut.ShowDialog();
                    GridMdr.Effect = null;
                    break;
            }
        }

        private void txtpt_phone_number_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }
        //แก้ไขข้อมูล
        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            _VNGETDATAGRID = null;
            DataRowView dataRow;
            try
            {
                dataRow = drgVisit.SelectedItem as DataRowView;
                _VNGETDATAGRID = dataRow.Row.ItemArray[0].ToString();
                _HNGETDATAGRID = dataRow.Row.ItemArray[1].ToString();
                _FullnameDATAGRID = dataRow.Row.ItemArray[2].ToString();
                Statuschangeinsure = "1";
                updateInsure();
            }
            catch (Exception ex)
            {
                Growl.Warning("การดึงข้อมููลจากตารางลงทะเบียนไม่สำเร็จ\r\n" + ex.Message);
            }
            GetVisitback();
        }
        private void ct_editcln_Click(object sender, RoutedEventArgs e)
        {
            _VNGETDATAGRID = null;
            DataRowView dataRow;
            try
            {
                //this.cbbpttype.Text = "ว่าง";
                //this.cbbClinic.Text = "ว่าง";
                dataRow = drgVisit.SelectedItem as DataRowView;
                _VNGETDATAGRID = dataRow.Row.ItemArray[0].ToString();
                _HNGETDATAGRID = dataRow.Row.ItemArray[1].ToString();
               
                getPatientInfo("hn", _HNGETDATAGRID);
                this.UcwsnhsoCID(this.txtpt_cid.Text);
                string clinic = dataRow.Row.ItemArray[7].ToString();
                string namepttype = dataRow.Row.ItemArray[6].ToString();
                this.cbbpttype.Text = HI7.Class.HIUility.getIdpttype(namepttype);
                this.cbbClinic.Text = HI7.Class.HIUility.getIdclinic(clinic);

                sppttypeovst.Visibility = Visibility.Collapsed;
                spappoint.Visibility = Visibility.Collapsed;
                spauthencode.Visibility = Visibility.Collapsed;
                spdct.Visibility = Visibility.Collapsed;
                sppttypept.Visibility = Visibility.Collapsed;
                spqueue.Visibility = Visibility.Collapsed;
                cb_servicetypefu.Visibility = Visibility.Collapsed;
                cb_changepttype.Visibility = Visibility.Collapsed;
                cb_Register.Visibility = Visibility.Collapsed;
                cb_print.Visibility = Visibility.Collapsed;
                cb_insure.Visibility = Visibility.Collapsed;

                btnRegister.Visibility = Visibility.Collapsed;
                //btnReferIn.Visibility = Visibility.Collapsed;
                //btnReferOut.Visibility = Visibility.Collapsed;
                btnEditcln.Visibility = Visibility.Visible;
                this.cbbpt_claimtype.SelectedIndex = 0;
                BitmapToBitmapImageGetpt();
                Keyboard.Focus(this.tabmdr);
                

            }
            catch (Exception ex)
            {
                Growl.Warning("การดึงข้อมููลจากตารางลงทะเบียนไม่สำเร็จ\r\n" + ex.Message);
            }
        }
        //ลบข้อมูลจาก DatagridVisit
        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            DataTable dtIncoth, dtLbbk, dtXryrqt, dtOprt,dtDt,dtOvst,dtInsure;
            bool delOvst, delOtoday, delHi7visittoday,delQ4uqueue,delInsure;
            DataRowView dataRow = (DataRowView)drgVisit.SelectedItem;
            _VNGETDATAGRID = dataRow.Row.ItemArray[0].ToString();
            string hn = dataRow.Row.ItemArray[1].ToString();
            try
            {
                dtOvst = HI7.Class.HIUility.checkOvst(_VNGETDATAGRID);
                dtDt = HI7.Class.HIUility.checkDt(_VNGETDATAGRID);
                dtIncoth = HI7.Class.HIUility.checkIncoth(_VNGETDATAGRID);
                dtLbbk = HI7.Class.HIUility.checkLbbk(_VNGETDATAGRID);
                dtOprt = HI7.Class.HIUility.checkOprt(_VNGETDATAGRID);
                dtXryrqt = HI7.Class.HIUility.checkXryrqt(_VNGETDATAGRID);
                //dtInsure = HI7.Class.HIUility.checkInsure(_VNGETDATAGRID);

                if (dtOvst.Rows.Count == 0 || dtDt.Rows.Count == 0) {
                    if (dtXryrqt.Rows.Count == 0 && dtLbbk.Rows.Count == 0 && dtIncoth.Rows.Count == 0 && dtOprt.Rows.Count == 0)
                    {
                        if (MessageBox.Show("ต้องการยกเลิกการลงละเบียนของ HN:" + hn + " นี้ให้กด Yes\r\n", "ยกเลิกการลงทะเบียน", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.No)
                        {
                        }
                        else
                        {
                            try
                            {
                                DateTime dateTime = DateTime.Now;
                                string otoday = "o" + dateTime.ToString("ddMMyy", CultureInfo.GetCultureInfo("th-TH"));
                                delOvst = HI7.Class.HIUility.removeOvst(_VNGETDATAGRID);
                                delOtoday = HI7.Class.HIUility.removeOtoday(otoday, _VNGETDATAGRID);
                                delHi7visittoday = HI7.Class.HIUility.removeHi7visittoday(_VNGETDATAGRID);
                                delInsure = HI7.Class.HIUility.removeInsure(_VNGETDATAGRID);
                                try { delQ4uqueue = HI7.Class.HIUility.removeQ4uqueue(_VNGETDATAGRID); } catch { }
                                if (delOvst == true && delOtoday == true && delHi7visittoday == true)
                                {
                                    Growl.Success("ยกเลิกการลงทะเบียนสำเร็จ");
                                }
                                else
                                {
                                    Growl.Warning("ยกเลิกการลงทะเบียนไม่สมบูรณ์\r\n" + "Ovst(" + delOvst + ")\r\n" + "Otoday(" + delOtoday + ")\r\n" + "Hi7visittoday(" + delHi7visittoday + ")");
                                }
                            }
                            catch
                            {
                            }
                        }
                    }
                    else
                    {
                        Growl.Success("ไม่สามารถยกเลิกการลงทะเบียนได้มีการลงข้อมูลทางการแพทย์แล้ว\r\n(Ovst,Dt,Xryrqt,Lbbk,Oprt,Incoth)");
                    }

                }
                else {
                    Growl.Success("ไม่สามารถยกเลิกการลงทะเบียนได้มีการลงข้อมูลทางการแพทย์แล้ว\r\n(Ovst,Dt,Xryrqt,Lbbk,Oprt,Incoth)");
                }
                
            }
            catch { 

            }

            this.getVisit();
        }
        private void menuSettingprintqueue_Click(object sender, RoutedEventArgs e)
        {
            GridMdr.Effect = new BlurEffect();
            frmPrintQHi7 frmPrintQHi7 = new frmPrintQHi7();
            frmPrintQHi7.stpSeting.Visibility = Visibility.Visible;//สั่งเปิดปุ่มการตั้งค่า
            Mdr.Forms.frmPrintQHi7.strCheckSetting = "1";
            frmPrintQHi7.ShowDialog();
            GridMdr.Effect = null;            
        }

        private void cb_Newborn_Checked(object sender, RoutedEventArgs e)
        {
            if(cb_Newborn.IsChecked == true) 
            {
                txtpt_cid.Text = "1111111111119";
                cb_Nonthailand.IsChecked = false;
            }
        }

        private void cb_Nonthailand_Checked(object sender, RoutedEventArgs e)
        {
            if (cb_Nonthailand.IsChecked == true)
            {
                txtpt_cid.Text = "9999999999994";
                cb_Newborn.IsChecked = false;
            }
        }

        private void btnLastAgent_Click(object sender, RoutedEventArgs e)
        {
            CIDNHS = this.txtnhsdata_cid.Text;
            DataTable dt;
            dt = HI7.Class.HIUility.getHn(Mdr.Forms.frmMdr.CIDNHS);
            if (dt != null)//ถ้ามีข้อมูล
            {
                if (dt.Rows.Count == 1)//ถ้ามีข้อมูล 1 แถว
                {
                    GridMdr.Effect = new BlurEffect();
                    frmAuthenservice frmAuthenservice = new frmAuthenservice();
                    frmAuthenservice.ShowDialog();
                    GridMdr.Effect = null;
                    CIDNHS = "";
                }
                else
                {
                    Growl.Warning("ไม่พบประวัติการรักษาที่โรงพยาบาล");
                }
            }
            else
            {
                Growl.Warning("ไม่พบประวัติการรักษาที่โรงพยาบาล");
            }
            
        }

        private void drgVisit_LoadingRow(object sender, DataGridRowEventArgs e)
        {
            e.Row.Header = (e.Row.GetIndex()+1).ToString();
        }

        private void btnPrint_Click(object sender, RoutedEventArgs e)
        {
            DataRowView dataRow = (DataRowView)drgVisit.SelectedItem;

            //int index = dataGridScreen.CurrentCell.Column.DisplayIndex;
            //string = reprintHn, reprintCln, reprintFullname,reprintSex,reprintAge,reprintPttype,reprintQueuenumber,reprintQueuepriority,reprintAuthencode;
            reprintQueuenumber = dataRow.Row.ItemArray[11].ToString();
            reprintHn = dataRow.Row.ItemArray[1].ToString();
            reprintFullname = dataRow.Row.ItemArray[5].ToString();
            reprintBirthdate = HI7.Class.HIUility.GetBirthday(reprintHn);
            reprintPttype = dataRow.Row.ItemArray[6].ToString();
            reprintSex = dataRow.Row.ItemArray[4].ToString();
            reprintCln = dataRow.Row.ItemArray[7].ToString();
            reprintAuthencode = dataRow.Row.ItemArray[9].ToString();
            reprintQueuepriority = dataRow.Row.ItemArray[11].ToString();
            reprintAge = HI7.Class.HIUility.Hn2AgeYY(reprintHn);
            reprintIdpttype = GetPttype_id(reprintPttype);
            reprintDate = dataRow.Row.ItemArray[2].ToString().Replace("-", "");
            reprintTime = dataRow.Row.ItemArray[3].ToString().Replace(":","");

            //MessageBox.Show(dataRow.Row.ItemArray[13].ToString());

            if (!string.IsNullOrEmpty(reprintQueuenumber))
            {
                Mdr.Forms.frmPrintQHi7.strCheckSetting = "2";
                frmPrintQHi7 f = new frmPrintQHi7();
                f.ShowDialog();
                f.Close();
                ClearData();
            }
            else
            {
                Growl.Warning("ไม่สามารถพิมพ์บัตรคิวได้ เนื่องจากไม่ได้สร้างคิว");
                ClearData();
                
            }

        }

        string getAgeChange(string male)
        {
            string sendmale;
            if (male == "ชาย")
            {
               sendmale = "1";
                return sendmale;
            }
            else {
                sendmale = "2";
                return sendmale;
            }
 
        }
        //check อายุจาก "ชาย == 1"
        //string getAgeChange() {
        //    string nameAge = this.cbbSexth.Text;
        //    if (nameAge == "ชาย") {
        //        return "1";
        //    }
        //}
        //SaveData VisitToday
        bool hi7visittoday()
        {
            //strstaff = "00";

            string strValues =VN;

            string strField ="vn";
            string strSQL ="INSERT INTO hi7visittoday("+strField+") values("+strValues+")";

            Dictionary<string, object> dictData = new Dictionary<string, object>();

            try
            {
                dictData.Add("query", strSQL);
                bool status = Hi7.Class.APIConnect.Excecute("query/hi7_query", "POST", dictData);
                if ( status )
                {
                    Growl.Success("Insert hi7visittoday Succress!!!");
                    // insert data
                    return true;

                }
                else
                {
                    return false;
                }


            }
            catch ( Exception ex )
            {
                Growl.Error("hi7visittoday say:\r\n");
                return false;
            }

        }

        DataTable dtscreen = new System.Data.DataTable();
        private void getodata_Screen()
        {
            try
            {
                //DataTable dt = new DataTable();
                Dictionary<string, object> dictData = new Dictionary<string, object>();

                string strSQL = "select idscreen,hn,pop_id,concat(pname,fname,' ',lname) AS 'fullname'" +
            ",age,cln.namecln as 'clnscreen',hi7priority.priority_name as 'priorityscreen', appointmentscreen,oldnew,appointment,DATE_FORMAT(DATE_ADD(datescreening, INTERVAL 543 YEAR),'%Y-%m-%d')AS 'datescreeningth',fname,lname " +
            "from  hi7screen " +
            "left join cln on cln.cln = hi7screen.clnscreen " +
            "left join hi7priority on hi7priority.priority_id = hi7screen.priorityscreen " +
            "where date(datescreening) = CURDATE() AND status_flg = 'N'";

                // string strSQL ="    select * from ovst limit 10 desc";
                dictData.Add("query", strSQL);


                dtscreen = Hi7.Class.APIConnect.getDataTable("query/hi7_query", "POST", dictData);

                //    dt = Hi7.Class.APIConnect.getDataTable("/lookup/ampur/list", "POST", dictData);
                if (dtscreen != null && dtscreen.Rows.Count > 0)
                {
                    // string[] selectedColumns = new[] { };
                    System.Data.DataView view = new System.Data.DataView(dtscreen);
                    // Create a new DataTable from the DataView with just the columns desired - and in the order desired
                    System.Data.DataTable selected = view.ToTable("Selected", false);


                    this.dataGridScreen.ItemsSource = selected.DefaultView;
                    //   string c = strProvince_code +strAmphur_code;
                }
            }
            catch (Exception ex)
            {

            }

        }
        //เช็คสิทธิ์โดยไม่เสียบบัตรประชาชน เอาวดป เกิดจาก สปสช กรณี Smart Agent ไม่มีบัตร
        private async Task UcwsnhsoCIDBirthDay(string cid)
        {
            using (HttpClient client = new HttpClient())
            {
                string user_person_id = HI7.Class.HIUility.SMARTHEALTH_USER_PERSON_UD;
                string smctoken = HI7.Class.HIUility.SMARTHEALTH_SMCTOKEN;
                string person_id = cid;
                wsStatus = "";
                var requestContent = new StringContent(string.Format(@"<?xml version=""1.0"" encoding=""UTF-8""?>
                    <S:Envelope xmlns:S=""http://schemas.xmlsoap.org/soap/envelope/"" xmlns:SOAP-ENV=""http://schemas.xmlsoap.org/soap/envelope/"">
                        <SOAP-ENV:Header/>
                        <S:Body>
                            <ns2:searchCurrentByPID xmlns:ns2=""http://tokenws.ucws.nhso.go.th/"">
                                <user_person_id>{0}</user_person_id>
                                <smctoken>{1}</smctoken>
                                <person_id>{2}</person_id>
                                <wsStatus>{3}</wsStatus>
                            </ns2:searchCurrentByPID>
                        </S:Body>
                    </S:Envelope>", user_person_id, smctoken, person_id, wsStatus), System.Text.Encoding.UTF8, "text/xml");
                using (HttpResponseMessage response = await client.PostAsync("http://ucws.nhso.go.th/ucwstokenp1/UCWSTokenP1?wsdl", requestContent))
                {
                    response.EnsureSuccessStatusCode(); // ตรวจสอบว่าการส่งคำขอไม่มีข้อผิดพลาด
                    string content = await response.Content.ReadAsStringAsync();
                    XmlDocument xml = new XmlDocument();
                    xml.LoadXml(content);
                    //ตรวจสอบ smc-token
                    try
                    {
                        XmlNode wsStatusDesc = xml.SelectSingleNode("//ws_status");
                        wsStatus = wsStatusDesc.InnerText;
                        if (wsStatus == "NHSO-00003")
                        {
                            ucHi7dialog ucHi7Dialog = new ucHi7dialog();
                            ucHi7Dialog.Message = "ผู้มารับบริการไม่มีวันเดือนเกิดระบบไม่สามารถดึงข้อมูลจากบัตรประชาชนได้\r\nต้องการให้ระบบดึงจาก UCAuthentication แทน\r\n ให้ทำการ Login ในโปรแกรม UCAuthentication เพื่อทำการขอ ucw_token\r\n1.ทำการเสียบบัตร จนท.ห้องบัตร(ที่มีสิทธิ์ในการเช็คสิทธิ์)\r\n2.เข้าโปรแกรม UCAuthentication ทำการลงชื่อเข้าใช้\r\n3.ใส่ PIN ของเจ้าหน้าที่\r\n4.เมื่อทำเสร็จแล้วให้กด Yes";
                            System.Windows.Window window = new System.Windows.Window
                            {
                                Title = "แจ้งเตือน!!!",
                                Content = ucHi7Dialog,
                                SizeToContent = SizeToContent.WidthAndHeight,
                                WindowStartupLocation = WindowStartupLocation.CenterScreen,
                                ResizeMode = ResizeMode.NoResize,
                                FontFamily = new FontFamily("Kanit"), // กำหนด FontFamily
                                FontSize = 18,
                                Background = System.Windows.Media.Brushes.Transparent,
                                AllowsTransparency = true,
                                WindowStyle = WindowStyle.None
                            };

                            ucHi7Dialog.YesButtonClicked += (sender, args) =>
                            {
                                HI7.Class.HIUility.getTokenShare();
                                UcwsnhsoCID(this.txtpt_cid.Text);
                                window.Close(); // ปิดหน้าต่าง dialog
                            };
                            ucHi7Dialog.NoButtonClicked += (sender, args) =>
                            {
                                window.Close(); // ปิดหน้าต่าง dialog
                            };
                            window.ShowDialog();
                        }
                        else if (wsStatus == "NHSO-000001")
                        {                           
                            //วันเดือนปีเกิด
                            try
                            {
                                SMCBIRTHDAY = "";
                                XmlNode birthdate = xml.SelectSingleNode("//birthdate");
                                SMCBIRTHDAY = HI7.Class.HIUility.Datetranform2(birthdate.InnerText);
                                //this.txtnhsdata_cid.Text = birthdate.InnerText;
                            }
                            catch
                            {
                                SMCBIRTHDAY = "";
                            }
                        }

                        else
                        {

                        }
                    }
                    catch { }

                }
            }
        }
        //เช็คสิทธิ์โดยไม่เสียบบัตรประชาชน
        private async Task UcwsnhsoCID(string cid)
        {
            using (HttpClient client = new HttpClient())
            {
                //3340700608801#1y7k6n22gt4uz462
                
                string user_person_id = HI7.Class.HIUility.SMARTHEALTH_USER_PERSON_UD;
                string smctoken = HI7.Class.HIUility.SMARTHEALTH_SMCTOKEN;
                string person_id = cid;
                wsStatus = "";

                var requestContent = new StringContent(string.Format(@"<?xml version=""1.0"" encoding=""UTF-8""?>
                    <S:Envelope xmlns:S=""http://schemas.xmlsoap.org/soap/envelope/"" xmlns:SOAP-ENV=""http://schemas.xmlsoap.org/soap/envelope/"">
                        <SOAP-ENV:Header/>
                        <S:Body>
                            <ns2:searchCurrentByPID xmlns:ns2=""http://tokenws.ucws.nhso.go.th/"">
                                <user_person_id>{0}</user_person_id>
                                <smctoken>{1}</smctoken>
                                <person_id>{2}</person_id>
                                <wsStatus>{3}</wsStatus>
                            </ns2:searchCurrentByPID>
                        </S:Body>
                    </S:Envelope>", user_person_id, smctoken, person_id, wsStatus), System.Text.Encoding.UTF8, "text/xml");

                using (HttpResponseMessage response = await client.PostAsync("http://ucws.nhso.go.th/ucwstokenp1/UCWSTokenP1?wsdl", requestContent))
                {
                    response.EnsureSuccessStatusCode(); // ตรวจสอบว่าการส่งคำขอไม่มีข้อผิดพลาด
                    string content = await response.Content.ReadAsStringAsync();
                    contentInsure = content;
                    XmlDocument xml = new XmlDocument();
                    xml.LoadXml(content);
                    xmlSentInsurl.LoadXml(content);
                    // ใช้ XPath เพื่อเลือกข้อมูลที่ต้องการจาก XML
                    //ตรวจสอบ smc-token
                    try {
                        XmlNode wsStatusDesc = xml.SelectSingleNode("//ws_status");
                        wsStatus = wsStatusDesc.InnerText;
                        if (wsStatus == "NHSO-00003")
                        {
                            Growl.Warning("ไม่สามารถตรวจสอบสิทธิ์ได้\r\nกรุณา Loginด้วยบัตรเจ้าหน้าที่\r\nในโปรแกรม UCAuthentication และใส่ PIN");

                            ucHi7dialog ucHi7Dialog = new ucHi7dialog();
                            ucHi7Dialog.Message = "ให้ทำการ Login ในโปรแกรม UCAuthentication เพื่อทำการขอ ucw_token\r\n1.ทำการเสียบบัตร จนท.ห้องบัตร(ที่มีสิทธิ์ในการเช็คสิทธิ์)\r\n2.เข้าโปรแกรม UCAuthentication ทำการลงชื่อเข้าใช้\r\n3.ใส่ PIN ของเจ้าหน้าที่\r\n4.เมื่อทำเสร็จแล้วให้กด Yes";

                            System.Windows.Window window = new System.Windows.Window
                            {
                                Title = "แจ้งเตือน!!!",
                                Content = ucHi7Dialog,
                                SizeToContent = SizeToContent.WidthAndHeight,
                                WindowStartupLocation = WindowStartupLocation.CenterScreen,
                                ResizeMode = ResizeMode.NoResize,
                                FontFamily = new FontFamily("Kanit"), // กำหนด FontFamily
                                FontSize = 18,
                                Background = System.Windows.Media.Brushes.Transparent,
                                AllowsTransparency = true,
                                WindowStyle = WindowStyle.None
                                //AllowsTransparency = true
                                // กำหนด FontSize
                                //WindowStyle = WindowStyle.None,
                                //ShowInTaskbar = false,
                                //Owner = Application.Current.MainWindow,
                                //WindowState = WindowState.Normal
                            };     
                            
                            ucHi7Dialog.YesButtonClicked += (sender, args) =>
                            {
                                HI7.Class.HIUility.getTokenShare();
                                UcwsnhsoCID(this.txtpt_cid.Text);
                                window.Close(); // ปิดหน้าต่าง dialog
                            };
                            ucHi7Dialog.NoButtonClicked += (sender, args) =>
                            {
                                window.Close(); // ปิดหน้าต่าง dialog
                            };
                            window.ShowDialog();
                        }else if(wsStatus == "NHSO-000001")
                        {
                            //เลขบัตรประชาชน
                            try
                            {
                                XmlNode personId = xml.SelectSingleNode("//person_id");
                                this.txtnhsdata_cid.Text = personId.InnerText;
                            }
                            catch { }
                            //สิทธิ์หลัก
                            try
                            {
                                XmlNode maininscl = xml.SelectSingleNode("//maininscl");
                                XmlNode maininsclNname = xml.SelectSingleNode("//maininscl_name");
                                if (maininscl == null && maininsclNname == null)
                                {
                                    this.lb_maininsclName.Visibility = Visibility.Collapsed;
                                    this.txtnshdata_maininsclName.Visibility = Visibility.Collapsed;
                                }
                                else
                                {
                                    this.txtnshdata_maininsclName.Text = "(" + maininscl.InnerText + ")" + maininsclNname.InnerText;
                                }
                                
                            }
                            catch
                            {
                                this.lb_maininsclName.Visibility = Visibility.Collapsed;
                                this.txtnshdata_maininsclName.Visibility = Visibility.Collapsed;
                            }

                            //สิทธิ์รอง
                            try
                            {
                                XmlNode subinscl = xml.SelectSingleNode("//subinscl");
                                XmlNode subinsclName = xml.SelectSingleNode("//subinscl_name");
                                if (subinscl == null && subinsclName == null)
                                {
                                    this.lb_subinsclName.Visibility = Visibility.Collapsed;
                                    this.txtnshdata_subinsclName.Visibility = Visibility.Collapsed;
                                }
                                else
                                {
                                    this.txtnshdata_subinsclName.Text = "(" + subinscl.InnerText + ")" + subinsclName.InnerText;
                                }
                                
                            }
                            catch
                            {
                                this.lb_subinsclName.Visibility = Visibility.Collapsed;
                                this.txtnshdata_subinsclName.Visibility = Visibility.Collapsed;
                            }

                            //สถานบริการหลัก
                            try
                            {
                                SMCEHmaincode = "";
                                XmlNode hmain = xml.SelectSingleNode("//hmain");
                                XmlNode hmainName = xml.SelectSingleNode("//hmain_name");
                                if (hmain == null && hmainName == null)
                                {
                                    SMCEHmaincode = "";
                                    this.lb_hmainName.Visibility = Visibility.Collapsed;
                                    this.txtnshdata_hmainName.Visibility = Visibility.Collapsed;
                                }
                                else
                                {
                                    this.txtnshdata_hmainName.Text = "(" + hmain.InnerText + ")" + hmainName.InnerText;
                                    SMCEHmaincode = hmain.InnerText;
                                }
                                
                            }
                            catch
                            {
                                SMCEHmaincode = "";
                                this.lb_hmainName.Visibility = Visibility.Collapsed;
                                this.txtnshdata_hmainName.Visibility = Visibility.Collapsed;
                            }

                            //สถานบริการรอง
                            try
                            {
                                SMCEHsubcode = "";
                                XmlNode hsub = xml.SelectSingleNode("//hsub");
                                XmlNode hsubName = xml.SelectSingleNode("//hsub_name");
                                if (hsub == null && hsubName == null)
                                {
                                    SMCEHsubcode = "";
                                    this.lb_txthosub.Visibility = Visibility.Collapsed;
                                    this.txthosub.Visibility = Visibility.Collapsed;
                                }
                                else
                                {
                                    this.txthosub.Text = "(" + hsub.InnerText + ")" + hsubName.InnerText;
                                    SMCEHsubcode = hsub.InnerText;
                                }
                                
                            }
                            catch
                            {
                                SMCEHsubcode = "";
                                this.lb_txthosub.Visibility = Visibility.Collapsed;
                                this.txthosub.Visibility = Visibility.Collapsed;
                            }

                            //วันที่เริ่มใช้สิทธิ์
                            try
                            {
                                SMCStartdate = "";
                                XmlNode startdate = xml.SelectSingleNode("//startdate");
                                if(startdate == null)
                                {
                                    SMCStartdate = "";
                                    this.lb_startdate.Visibility = Visibility.Collapsed;
                                    this.txtnshdata_startdate.Visibility = Visibility.Collapsed;
                                }
                                else
                                {
                                    this.txtnshdata_startdate.Text = HI7.Class.HIUility.Datetranform3(startdate.InnerText);
                                    SMCStartdate = this.txtnshdata_startdate.Text;
                                }
                                
                            }
                            catch
                            {
                                SMCStartdate = "";
                                this.lb_startdate.Visibility = Visibility.Collapsed;
                                this.txtnshdata_startdate.Visibility = Visibility.Collapsed;
                            }

                            //วันที่หมดอายุ
                            try
                            {
                                SMCExpdate = "";
                                XmlNode expdate = xml.SelectSingleNode("//expdate");
                                if(expdate == null)
                                {
                                    SMCExpdate = "";
                                    this.lb_expdate.Visibility = Visibility.Collapsed;
                                    this.txtexpdate.Visibility = Visibility.Collapsed;
                                }
                                else
                                {
                                    this.txtexpdate.Text = HI7.Class.HIUility.Datetranform3(expdate.InnerText);
                                    SMCExpdate = this.txtexpdate.Text;
                                    if (SMCExpdate == "NoExp")
                                    {
                                        this.lb_expdate.Visibility = Visibility.Collapsed;
                                        this.txtexpdate.Visibility = Visibility.Collapsed;
                                    }
                                    else
                                    {
                                        SMCExpdate = this.txtexpdate.Text;
                                    }
                                }
                               
                            }
                            catch
                            {
                                SMCExpdate = "";
                                this.lb_expdate.Visibility = Visibility.Collapsed;
                                this.txtexpdate.Visibility = Visibility.Collapsed;
                            }

                            //การ์ด ID สิทธิ์UC
                            try
                            {
                                SMCCardID = "";
                                XmlNode cardid = xml.SelectSingleNode("//cardid");
                                if (cardid == null)
                                {
                                    SMCCardID = "";
                                }
                                else
                                {
                                    SMCCardID = cardid.InnerText;
                                }
                                
                                //this.txtnhsdata_cid.Text = cardid.InnerText;
                            }
                            catch {
                                SMCCardID = "";
                            }

                            //วันเดือนปีเกิด
                            try
                            {
                                SMCBIRTHDAY = "";
                                XmlNode birthdate = xml.SelectSingleNode("//birthdate");
                                if (birthdate == null)
                                {
                                    SMCBIRTHDAY = "";
                                }
                                else
                                {
                                    SMCBIRTHDAY = HI7.Class.HIUility.Datetranform3(birthdate.InnerText);
                                }
                                
                                //this.txtnhsdata_cid.Text = birthdate.InnerText;
                            }
                            catch {
                                SMCBIRTHDAY = "";
                            }
                        }

                        else
                        {
                            
                        }
                    }
                    catch { }
                    
                 }
            }
        }
        //อ่านบัตร SmartขอAgent เช็คสืทธิ์

        private void nhso_smartcard_readpttype()
        {

            string strSQL;
            DataTable dt = new DataTable();
            DataRow dr;
            try
            {
                string readImageFlag = "?readImageFlag=true";
                WebClient webClient = new WebClient();
                string url = API_LOCALHOST + API_PREFIX_READ + readImageFlag;
                webClient.Encoding = Encoding.UTF8;
                jsonsmartAgent = webClient.DownloadString(url);
                josnSendInsure = jsonsmartAgent;
                string v = jsonsmartAgent;
                //วันที่เริ่มสิทธิ์
                try
                {                 
                    DateTime dateTime = DateTime.Parse(JObject.Parse(jsonsmartAgent).SelectToken("startDateTime").ToString());                    
                    string formattedDate = HI7.Class.HIUility.DateConvert(CheckDMY(dateTime.Day.ToString(), dateTime.Month.ToString(), dateTime.Year.ToString()));
                    Strtxtnshdata_startdate = formattedDate;
                }
                catch { }
                //วันที่หมดสิทธิ์
                try
                {
                    DateTime dateTime = DateTime.Parse(JObject.Parse(jsonsmartAgent).SelectToken("expireDateTime").ToString());
                    string formattedDate = HI7.Class.HIUility.DateConvert(CheckDMY(dateTime.Day.ToString(), dateTime.Month.ToString(), dateTime.Year.ToString()));
                    Strtxtexpdate = formattedDate;
                }
                catch { }
                //เช็ควันเดือนปีเกิดที่ไม่มี ว/ด/ป 00/00/2560
                try {
                    if(string.IsNullOrEmpty(txtBirthDate.Text))
                    {
                        string Strnumber = JObject.Parse(jsonsmartAgent).SelectToken("birthDate").ToString();
                        int number = JObject.Parse(jsonsmartAgent).SelectToken("birthDate").ToString().Length;
                        if (number == 8)
                        {
                            txtBirthDate.Text = HI7.Class.HIUility.Datetranform3(Strnumber);
                            txtAgenow.Text = JObject.Parse(jsonsmartAgent).SelectToken("age").ToString();
                        }
                        else
                        {

                        }
                        //ตรวจสอบ 8 หลัก

                    }
                    
                }
                catch { }
                //Set ตัวแปรจากการอ่านบัตรตรวจสอบสิทธิ์
                try { Strtxtnshdata_maininsclName = JObject.Parse(jsonsmartAgent).SelectToken("mainInscl").ToString(); } catch { }
                try { Strtxtnshdata_subinsclName = JObject.Parse(jsonsmartAgent).SelectToken("subInscl").ToString(); } catch { }
                try { Strtxtnshdata_hmainName = "(" + JObject.Parse(jsonsmartAgent).SelectToken("hospMain").SelectToken("hcode").ToString() + ")" + JObject.Parse(jsonsmartAgent).SelectToken("hospMain").SelectToken("hname").ToString(); } catch { }
                try { Strtxthosub = "(" + JObject.Parse(jsonsmartAgent).SelectToken("hospSub").SelectToken("hcode").ToString() + ")" + JObject.Parse(jsonsmartAgent).SelectToken("hospSub").SelectToken("hname").ToString(); } catch { }
                //try {
                //    string date = JObject.Parse(jsonsmartAgent).SelectToken("startDateTime").ToString();
                //    DateTime dateTime = DateTime.Parse(JObject.Parse(jsonsmartAgent).SelectToken("startDateTime").ToString());
                //    string formattedDate = HI7.Class.HIUility.DateConvert(dateTime.ToString("dd/MM/yyyy"));
                //    Strtxtnshdata_startdate = formattedDate; 
                //} catch { }
                //try {
                //    DateTime dateTime = DateTime.Parse(JObject.Parse(jsonsmartAgent).SelectToken("expireDateTime").ToString());
                //    string formattedDate = HI7.Class.HIUility.DateConvert(dateTime.ToString("dd/MM/yyyy"));
                //    Strtxtexpdate = formattedDate; } catch { }
                
                if (Strtxtnshdata_maininsclName != null && Strtxtnshdata_maininsclName != "")//เช็ควันหมดอายุบัตร
                {

                    this.txtnshdata_maininsclName.Text = Strtxtnshdata_maininsclName;
                }
                else
                {
                    this.lb_maininsclName.Visibility = Visibility.Collapsed;
                    this.txtnshdata_maininsclName.Visibility = Visibility.Collapsed;
                }
                if (Strtxtnshdata_subinsclName != null && Strtxtnshdata_subinsclName != "")//เช็ควันหมดอายุบัตร
                {
                    this.txtnshdata_subinsclName.Text = Strtxtnshdata_subinsclName;
                }
                else 
                {
                    this.lb_subinsclName.Visibility = Visibility.Collapsed;
                    this.txtnshdata_subinsclName.Visibility = Visibility.Collapsed;
                }
                if (Strtxtnshdata_hmainName != null && Strtxtnshdata_hmainName != "")//เช็ควันหมดอายุบัตร
                {
                    this.txtnshdata_hmainName.Text = Strtxtnshdata_hmainName;
                }
                else 
                {
                    this.lb_hmainName.Visibility = Visibility.Collapsed;
                    this.txtnshdata_hmainName.Visibility = Visibility.Collapsed;
                }
                if (Strtxthosub != null && Strtxthosub != "")//เช็ควันหมดอายุบัตร
                {
                    this.txthosub.Text = Strtxthosub;
                }
                else 
                {
                    this.lb_txthosub.Visibility = Visibility.Collapsed;
                    this.txthosub.Visibility = Visibility.Collapsed;
                }
                if (!string.IsNullOrEmpty(Strtxtnshdata_startdate))//เช็ควันหมดอายุบัตร
                {
                    this.txtnshdata_startdate.Text = Strtxtnshdata_startdate;
                }
                else 
                {
                    this.lb_startdate.Visibility = Visibility.Collapsed;
                    this.txtnshdata_startdate.Visibility = Visibility.Collapsed;
                }
                if (Strtxtexpdate != null && Strtxtexpdate != "")//เช็ควันหมดอายุบัตร
                {
                    this.txtexpdate.Text = Strtxtexpdate;
                }
                else 
                {
                    this.lb_expdate.Visibility = Visibility.Collapsed;
                    this.txtexpdate.Visibility = Visibility.Collapsed;
                }
            }

            catch (Exception ex)
            {
                Growl.Error("กรุณาตรวจสอบ Service secureagent-1.1.1\r\nเพื่อให้ระบบตรวจสอบสิทธิ์โปรแกรมจะเรียกใช้\r\n Service UCAuthentication แทน\r\n**หมายเหตุ ระบบจะไม่ขอ AuthenCode ให้");
                this.UcwsnhsoCID(this.txtIDCard.Text);
            }
        }
        //ตรวจสอบการขอ Authencode ว่าเคยขอไปหรือไม่
        private void check_nhsoAuthencode()
        {
            string cbbpt_claimtype = "";
            HI7.Class.HIUility._claimCode = "";
            pid = "";
            string strSQL;
            DataTable dt = new DataTable();
            DataRow dr;
            try
            {
                pid = txtpt_cid.Text;
                cbbpt_claimtype = this.cbbpt_claimtype.SelectedValue.ToString();
                strSQL = "Select * FROM kios_pttype WHERE  cid='" + pid + "' and regist_date=curdate() and claimType='" + cbbpt_claimtype + "' LIMIT 1";

                Dictionary<string, object> dictData2 = new Dictionary<string, object>();
                dictData2.Add("query", strSQL);
                dt = Hi7.Class.APIConnect.getDataTable("query/hi7_query", "POST", dictData2);
                //    dt = Hi7.Class.APIConnect.getDataTable("/lookup/ampur/list", "POST", dictData);
                if (dt != null && dt.Rows.Count > 0)  // มี Claimtype ในวันนี้แล้วไม่ต้องบันทึก
                {
                    dr = dt.Rows[0];
                    string json_data = "";
                    this.txtclaimcode.Text = dr["claimCode"].ToString();
                    this.txtclaimtype.Text = dr["claimType"].ToString();
                    json_data = dr["json_data"].ToString();
                    HI7.Class.HIUility._claimCode = this.txtclaimcode.Text;
                    if (!string.IsNullOrEmpty(this.txtclaimcode.Text))
                    {
                        strSQL = "INSERT INTO kios_pttype (cid,json_data,regist_date,regist_time,claimType,claimCode,vn,cln) VALUES('" + pid + "','" + json_data + "',curdate(),curtime()" + "," + "'" + txtclaimtype.Text + "','" + txtclaimcode.Text + "','" + VN + "','" + "ห้องบัตร(ขอซ้ำ)" + "')";
                        Dictionary<string, object> dictData = new Dictionary<string, object>();
                        dictData.Add("query", strSQL);

                        bool status = Hi7.Class.APIConnect.Excecute("query/hi7_query", "POST", dictData);
                    }
                    //this.nhso_confirm_save();//ลบออก
                }
            }

            catch (Exception ex)
            {
                
            }
        }
        //อ่านบัตร SmartขอAuthencode
        string claimType_PG0060001, pid, correlationId;
        private void nhso_smartcard_read()
        {
            claimType_PG0060001 = "";
            HI7.Class.HIUility._claimCode = "";
            pid = "";
            correlationId = "";
            string strSQL;
            DataTable dt = new DataTable();
            DataRow dr;
            try
            {           
                pid = JObject.Parse(jsonsmartAgent).SelectToken("pid").ToString();
                string cbbpt_claimtype = this.cbbpt_claimtype.SelectedValue.ToString();

                if (cbbpt_claimtype == "PG0060001")
                {
                    claimType_PG0060001 = JObject.Parse(jsonsmartAgent).SelectToken("claimTypes")[0].SelectToken("claimType").ToString();
                }
                else if (cbbpt_claimtype == "PG0110001")
                {
                    claimType_PG0060001 = JObject.Parse(jsonsmartAgent).SelectToken("claimTypes")[1].SelectToken("claimType").ToString();
                }
                else if (cbbpt_claimtype == "PG0120001")
                {
                    claimType_PG0060001 = JObject.Parse(jsonsmartAgent).SelectToken("claimTypes")[2].SelectToken("claimType").ToString();
                }
                else if (cbbpt_claimtype == "PG0130001")
                {
                    claimType_PG0060001 = JObject.Parse(jsonsmartAgent).SelectToken("claimTypes")[3].SelectToken("claimType").ToString();
                }
                else {
                    claimType_PG0060001 = "";
                }              

                correlationId = JObject.Parse(jsonsmartAgent).SelectToken("correlationId").ToString();


                strSQL = "Select * FROM kios_pttype WHERE  cid='" + pid + "' and regist_date=curdate() and claimType='"+ claimType_PG0060001 + "' ";

                Dictionary<string, object> dictData2 = new Dictionary<string, object>();
                dictData2.Add("query", strSQL);

                dt = Hi7.Class.APIConnect.getDataTable("query/hi7_query", "POST", dictData2);

                //    dt = Hi7.Class.APIConnect.getDataTable("/lookup/ampur/list", "POST", dictData);
                if (dt != null && dt.Rows.Count > 0)  // มี Claimtype ในวันนี้แล้วไม่ต้องบันทึก
                {
                    dr = dt.Rows[0];
                    string json_data = "";
                    this.txtclaimcode.Text = dr["claimCode"].ToString();
                    this.txtclaimtype.Text = dr["claimType"].ToString();
                    json_data = dr["json_data"].ToString();
                    HI7.Class.HIUility._claimCode = this.txtclaimcode.Text;
                    if(!string.IsNullOrEmpty(this.txtclaimcode.Text))
                    {
                        strSQL = "INSERT INTO kios_pttype (cid,json_data,regist_date,regist_time,claimType,claimCode,vn,cln) VALUES('" + pid + "','" + json_data + "',curdate(),curtime()" + "," + "'" + txtclaimtype.Text + "','" + txtclaimcode.Text + "','" + VN +"','"+"ห้องบัตร(ขอซ้ำ)"+"')";
                        Dictionary<string, object> dictData = new Dictionary<string, object>();
                        dictData.Add("query", strSQL);

                        bool status = Hi7.Class.APIConnect.Excecute("query/hi7_query", "POST", dictData);
                    }
                    //this.nhso_confirm_save();//ลบออก
                }
                else { // ขอ ClaimType ใหม่ หรือ Claimtype ไม่ตรงกัน บันทึก
                    strSQL = "INSERT INTO kios_pttype (cid,json_data,regist_date,regist_time,claimType,vn) VALUES('" + pid + "','" + jsonsmartAgent + "',curdate(),curtime()" + "," +"'"+ this.claimType_PG0060001+"'"+","+VN+")";

                    Dictionary<string, object> dictData = new Dictionary<string, object>();
                    dictData.Add("query", strSQL);

                    bool status = Hi7.Class.APIConnect.Excecute("query/hi7_query", "POST", dictData);
                    this.nhso_confirm_save();//บันทึกในตาราง
                }
            }

            catch (Exception ex)
            {
                Growl.Error("nhso_smartcard_read say\r\nเตรียมข้อมูลการขอ Authencode ไม่สำเร็จ\r\n" + ex.Message);
            }
        }
        private void nhso_confirm_save()
        {
            string url = API_LOCALHOST + API_PREFIX_CONFIRM_SAVE;
            Dictionary<string, object> dictData = new Dictionary<string, object>();
            dictData.Add("pid", this.pid);
            dictData.Add("claimType", this.claimType_PG0060001);
            dictData.Add("mobile", this.txtpt_phone_number.Text);
            dictData.Add("correlationId", this.correlationId);
            dictData.Add("hn", this.txtpt_hn.Text.Trim());
            dictData.Add("hcode", HI7.Class.HIUility._HCODE);
            try
            {
                WebClient web = new WebClient();               
                string v = "";
                var u = new Uri(url);
                if (this.txtpt_hn.Text != "")
                {
                    var data = Encoding.Default.GetBytes(JsonConvert.SerializeObject(dictData, (Newtonsoft.Json.Formatting)Formatting.Indented));
                    var result_post = SendRequest(u, data, "application/json", "POST");
                    v = JObject.Parse(result_post).SelectToken("claimCode").ToString();
                    //เก็บรหัส ClaimCode ส่งไปพิมพ์ใบคิว
                    HI7.Class.HIUility._claimCode = v;
                    UpdateTable("UPDATE kios_pttype SET claimCode = '" + v + "', cln = 'ห้องบัตร', claimType = '" + this.claimType_PG0060001 + "' where vn = '" + VN + "' and regist_date = curdate()  and claimType = '" + this.claimType_PG0060001 + "'");
                    UpdateTable("UPDATE pt SET hometel='" + this.txtpt_phone_number.Text + "' where pop_id='" + this.pid + "'");
                    Growl.Success("ขอAuthenCode สำเร็จ");
                    this.txtclaimtype.Text = this.claimType_PG0060001;
                    this.txtclaimcode.Text = v;
                }
                else {
                    var client = new RestClient(url);
                    //new RestClient("http://localhost:8189/api/nhso-service/confirm-save");
                    client.Timeout = -1;
                    var request = new RestRequest(Method.POST);
                    request.AddHeader("Content-Type", "application/json");
                    string aa = JsonConvert.SerializeObject(dictData, (Newtonsoft.Json.Formatting)Formatting.Indented);

                    request.AddParameter("application/json", aa, ParameterType.RequestBody);
                    IRestResponse response = client.Execute(request);
                    string resultApi = response.Content.ToString();
                    string messageError = JObject.Parse(resultApi).SelectToken("errors")[0].SelectToken("defaultMessage").ToString();
                    this.txtclaimtype.Text = "";
                    this.txtclaimcode.Text = "";
                    Growl.Error("AuthenCode "+ "ไม่พบ HN กรุณากดค้นหา HN\r\n" + messageError);
                }
            }
            catch (Exception ex)
            {
                var client = new RestClient(url);
                //new RestClient("http://localhost:8189/api/nhso-service/confirm-save");
                client.Timeout = -1;
                var request = new RestRequest(Method.POST);
                request.AddHeader("Content-Type", "application/json");
                string aa = JsonConvert.SerializeObject(dictData, (Newtonsoft.Json.Formatting)Formatting.Indented);

                request.AddParameter("application/json", aa, ParameterType.RequestBody);
                IRestResponse response = client.Execute(request);
                string resultApi = response.Content.ToString();
                string messageError = JObject.Parse(resultApi).SelectToken("errors")[0].SelectToken("defaultMessage").ToString();
                Growl.Error("AuthenCode say:"+messageError);
                //Interaction.MsgBox("ไม่สามารถขอ Authen ได้ กรุณาลองใหม่อีกครั้ง", MsgBoxStyle.Exclamation);
                //this.SetupScreen();
            }
        }
        private void nhso_smartcard_readbtn()
        {
            claimType_PG0060001 = "";
            HI7.Class.HIUility._claimCode = "";
            pid = "";
            correlationId = "";
            string strSQL;
            DataTable dt = new DataTable();
            DataRow dr;
            try
            {
                string readImageFlag = "?readImageFlag=true";
                WebClient webClient = new WebClient();
                string url = API_LOCALHOST + API_PREFIX_READ + readImageFlag;
                webClient.Encoding = Encoding.UTF8;
                string result = webClient.DownloadString(url);
                string v = result;

                pid = JObject.Parse(result).SelectToken("pid").ToString();
                string cbbpt_claimtype = this.cbbpt_claimtype.SelectedValue.ToString();

                if (cbbpt_claimtype == "PG0060001")
                {
                    claimType_PG0060001 = JObject.Parse(result).SelectToken("claimTypes")[0].SelectToken("claimType").ToString();
                }
                else if (cbbpt_claimtype == "PG0110001")
                {
                    claimType_PG0060001 = JObject.Parse(result).SelectToken("claimTypes")[1].SelectToken("claimType").ToString();
                }
                else if (cbbpt_claimtype == "PG0120001")
                {
                    claimType_PG0060001 = JObject.Parse(result).SelectToken("claimTypes")[2].SelectToken("claimType").ToString();
                }
                else if (cbbpt_claimtype == "PG0130001")
                {
                    claimType_PG0060001 = JObject.Parse(result).SelectToken("claimTypes")[3].SelectToken("claimType").ToString();
                }
                else
                {
                    claimType_PG0060001 = "";
                }
                correlationId = JObject.Parse(result).SelectToken("correlationId").ToString();
                strSQL = "Select * FROM kios_pttype WHERE  cid='" + pid + "' and regist_date=curdate() and claimType='" + claimType_PG0060001 + "' ";
                Dictionary<string, object> dictData2 = new Dictionary<string, object>();
                dictData2.Add("query", strSQL);
                dt = Hi7.Class.APIConnect.getDataTable("query/hi7_query", "POST", dictData2);
                if (dt != null && dt.Rows.Count > 0)  // มี Claimtype ในวันนี้แล้วไม่ต้องบันทึก
                {
                    dr = dt.Rows[0];
                    this.txtclaimcode.Text = dr["claimCode"].ToString();
                    this.txtclaimtype.Text = claimType_PG0060001;
                    HI7.Class.HIUility._claimCode = this.txtclaimcode.Text;
                    Growl.Warning("วันนี้มีการขอ Authencode แล้ว");
                    //this.nhso_confirm_save();//ลบออก
                }
                else
                { // ขอ ClaimType ใหม่ หรือ Claimtype ไม่ตรงกัน บันทึก
                    strSQL = "INSERT INTO kios_pttype (cid,json_data,regist_date,regist_time,claimType,vn) VALUES('" + pid + "','" + result + "',curdate(),curtime()" + "," + "'" + this.claimType_PG0060001 + "'" + ",'" + VN + "')";

                    Dictionary<string, object> dictData = new Dictionary<string, object>();
                    dictData.Add("query", strSQL);

                    bool status = Hi7.Class.APIConnect.Excecute("query/hi7_query", "POST", dictData);
                    if (status == true)
                    {
                        this.nhso_confirm_savebtn();//บันทึกในตาราง
                    }
                    else if (status == false)
                    {
                        Growl.Warning("Error kios_pttype");
                    }
                    else { }
                }
            }

            catch (Exception ex)
            {
                Growl.Error("nhso_smartcard_read say\r\nไม่สามารถเชื่อมต่อเครื่องอ่านบัตรได้/กรุณาตรวจสอบ เสียบบัตรประชาชนอีกครั้ง\r\n" + ex.Message);
            }
        }
        private void nhso_confirm_savebtn()
        {
            string url = API_LOCALHOST + API_PREFIX_CONFIRM_SAVE;
            Dictionary<string, object> dictData = new Dictionary<string, object>();
            dictData.Add("pid", this.pid);
            dictData.Add("claimType", this.claimType_PG0060001);
            dictData.Add("mobile", this.txtpt_phone_number.Text);
            dictData.Add("correlationId", this.correlationId);
            dictData.Add("hn", this.txtpt_hn.Text.Trim());
            dictData.Add("hcode", HI7.Class.HIUility._HCODE);
            try
            {
                WebClient web = new WebClient();
                string v = "";
                var u = new Uri(url);
                if (this.txtpt_hn.Text != "")
                {
                    var data = Encoding.Default.GetBytes(JsonConvert.SerializeObject(dictData, (Newtonsoft.Json.Formatting)Formatting.Indented));
                    var result_post = SendRequest(u, data, "application/json", "POST");
                    v = JObject.Parse(result_post).SelectToken("claimCode").ToString();
                    //เก็บรหัส ClaimCode ส่งไปพิมพ์ใบคิว
                    HI7.Class.HIUility._claimCode = v;
                    UpdateTable("UPDATE kios_pttype SET claimCode = '" + v + "', cln = 'ห้องบัตร', claimType = '" + this.claimType_PG0060001 + "' where cid = '" + this.pid + "' and regist_date = curdate()  and claimType = '" + this.claimType_PG0060001 + "'");
                    UpdateTable("UPDATE pt SET hometel='" + this.txtpt_phone_number.Text + "' where pop_id='" + this.pid + "'");
                    Growl.Success("ขอAuthenCode สำเร็จ");
                    this.txtclaimtype.Text = this.claimType_PG0060001;
                    this.txtclaimcode.Text = v;
                }
                else
                {
                    var client = new RestClient(url);
                    //new RestClient("http://localhost:8189/api/nhso-service/confirm-save");
                    client.Timeout = -1;
                    var request = new RestRequest(Method.POST);
                    request.AddHeader("Content-Type", "application/json");
                    string aa = JsonConvert.SerializeObject(dictData, (Newtonsoft.Json.Formatting)Formatting.Indented);

                    request.AddParameter("application/json", aa, ParameterType.RequestBody);
                    IRestResponse response = client.Execute(request);
                    string resultApi = response.Content.ToString();
                    string messageError = JObject.Parse(resultApi).SelectToken("errors")[0].SelectToken("defaultMessage").ToString();
                    this.txtclaimtype.Text = "";
                    this.txtclaimcode.Text = "";
                    Growl.Warning("AuthenCode " + "ไม่พบ HN กรุณากดค้นหา HN\r\n" + messageError);
                }
            }
            catch (Exception ex)
            {
                var client = new RestClient(url);
                //new RestClient("http://localhost:8189/api/nhso-service/confirm-save");
                client.Timeout = -1;
                var request = new RestRequest(Method.POST);
                request.AddHeader("Content-Type", "application/json");
                string aa = JsonConvert.SerializeObject(dictData, (Newtonsoft.Json.Formatting)Formatting.Indented);

                request.AddParameter("application/json", aa, ParameterType.RequestBody);
                IRestResponse response = client.Execute(request);
                string resultApi = response.Content.ToString();
                string messageError = JObject.Parse(resultApi).SelectToken("errors")[0].SelectToken("defaultMessage").ToString();
                Growl.Warning("AuthenCode say:" + messageError);
                //Interaction.MsgBox("ไม่สามารถขอ Authen ได้ กรุณาลองใหม่อีกครั้ง", MsgBoxStyle.Exclamation);
                //this.SetupScreen();
            }
        }
        private void UpdateTable(string strSQL)
        {

            Dictionary<string, object> dictData = new Dictionary<string, object>();
            dictData.Add("query", strSQL);

            Hi7.Class.APIConnect.Excecute("query/hi7_query", "POST", dictData);

        }

        private string SendRequest(Uri uri, byte[] jsonDataBytes, string contentType, string method)
        {
            string strResponse;
            WebRequest request;

            request = WebRequest.Create(uri);
            request.ContentLength = jsonDataBytes.Length;
            request.ContentType = contentType;
            request.Method = method;

            using (var requestStream = request.GetRequestStream())
            {
                requestStream.Write(jsonDataBytes, 0, jsonDataBytes.Length);
                requestStream.Close();
                using (var response = (HttpWebResponse)request.GetResponse())
                {
                    var encoding = Encoding.GetEncoding(response.CharacterSet);

                    using (var responseStream = response.GetResponseStream())
                    using (var reader = new StreamReader(responseStream, encoding))
                        strResponse = reader.ReadToEnd();
                }
            }
            return strResponse;
        }


        //end
        }
}

  

    


