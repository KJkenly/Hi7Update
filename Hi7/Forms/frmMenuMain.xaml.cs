using HandyControl.Media.Effects;
using Hi7.UsercontrolHi7;
using LibGit2Sharp;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Security.AccessControl;
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
using Path = System.IO.Path;
using System.Xml.Linq;
using System.Runtime.InteropServices;
using MySqlConnector;
using System.Data;
using Newtonsoft.Json.Linq;
using System.Management;
using System.Reflection;

namespace Hi7.Forms
{
    /// <summary>
    /// Interaction logic for frmMenuMain.xaml
    /// </summary>
    public partial class frmMenuMain : Window
    {
        public static string app_path = @Class.APIConnect.APP_PATH;
        //Hi7 Control pannel KEN
        public static string PathMainVerDevice = "C:\\Program Files (x86)\\Hi7 Ubon\\Hi7Setup\\mdrversion.txt";
        public static string PathMainVerNew = "C:\\Program Files (x86)\\Hi7 Ubon\\Hi7CheckVersion\\mdrversion.txt";
        public static string PathMainVerRevert = "C:\\Program Files (x86)\\Hi7 Ubon\\Hi7Setup\\mdrversion.txt";
        public static string repositoryUrlMain = "https://github.com/KJkenly/MainHi7.git";
        public static string branchNameMain = "main";
        public static string destinationFolderPathMain = "C:\\Program Files (x86)\\Hi7 Ubon\\Hi7Setup";
        public static string destinationFolderPathcheckversionMain = "C:\\Program Files (x86)\\Hi7 Ubon\\Hi7Setup\\Hi7CheckVersion";
        public static string oldprogrameMain = "C:\\Program Files (x86)\\Hi7 Ubon\\Hi7Setup\\Hi7Oldversion";
        //MDR KEN
        public static string PathMdrVerDevice = "C:\\Hi7 Program\\Hi7Master\\net6.0-windows\\mdrversion.txt";
        public static string PathMdrVerNew = "C:\\Hi7 Program\\Hi7CheckVersion\\mdrversion.txt";
        public static string PathMdrVerRevert = "C:\\Hi7 Program\\Hi7Oldversion\\net6.0-windows\\mdrversion.txt";
        public static string repositoryUrl = "https://github.com/KJkenly/Hi7Update.git";
        public static string branchName = "main";
        public static string destinationFolderPath = "C:\\Hi7 Program\\Hi7Master";
        public static string destinationFolderPathcheckversion = "C:\\Hi7 Program\\Hi7CheckVersion";
        public static string oldprograme = "C:\\Hi7 Program\\Hi7Oldversion";
        //HICM UPLOAD REP/STATEMENT MAX
        public static string PathMdrVerDevicehicm = "C:\\Hicm Program\\HicmMaster\\Debug\\mdrversion.txt";
        public static string PathMdrVerNewhicm = "C:\\Hicm Program\\HicmCheckVersion\\mdrversion.txt";
        public static string PathMdrVerReverthicm = "C:\\Hicm Program\\HicmOldversion\\bin\\mdrversion.txt";
        public static string repositoryUrlhicm = "https://github.com/chaiyonk/hicm.git";
        public static string branchNamehicm = "main";
        public static string destinationFolderPathhicm = "C:\\Hicm Program\\HicmMaster";
        public static string destinationFolderPathcheckversionhicm = "C:\\Hicm Program\\HicmCheckVersion";
        public static string oldprogramehicm = "C:\\Hicm Program\\HicmOldversion";
        //HICM API CLAIM KHO 
        public static string PathMdrVerDeviceEclaim = "C:\\Apieclaim Program\\ApieclaimMaster\\Debug\\hicmversion.txt";
        public static string PathMdrVerNewEclaim = "C:\\Apieclaim Program\\ApieclaimCheckVersion\\hicmversion.txt";
        public static string PathMdrVerRevertEclaim = "C:\\Apieclaim Program\\ApieclaimOldversion\\Debug\\hicmversion.txt";
        public static string repositoryUrlEclaim = "https://github.com/jkasri/hicm.git";
        public static string branchNameEclaim = "main";
        public static string destinationFolderPathEclaim = "C:\\Apieclaim Program\\ApieclaimMaster";
        public static string destinationFolderPathcheckversionEclaim = "C:\\Apieclaim Program\\ApieclaimCheckVersion";
        public static string oldprogrameEclaim = "C:\\Apieclaim Program\\ApieclaimOldversion"; 
        //HICM Report KHO 
        public static string PathMdrVerDeviceHicmreport = "C:\\Hicmreport Program\\HicmreportMaster\\Debug\\hicmreportversion.txt";
        public static string PathMdrVerNewHicmreport = "C:\\Hicmreport Program\\HicmreportCheckVersion\\hicmreportversion.txt";
        public static string PathMdrVerRevertHicmreport = "C:\\Hicmreport Program\\HicmreportOldversion\\Debug\\hicmreportversion.txt";
        public static string repositoryUrlHicmreport = "https://github.com/jkasri/hicmreport.git";
        public static string branchNameHicmreport = "main";
        public static string destinationFolderPathHicmreport = "C:\\Hicmreport Program\\HicmreportMaster";
        public static string destinationFolderPathcheckversionHicmreport = "C:\\Hicmreport Program\\HicmreportCheckVersion";
        public static string oldprogrameHicmreport = "C:\\Hicmreport Program\\HicmreportOldversion";

        public static string versionnew = string.Empty;
        public static string versiondevice = string.Empty;
        public static string versionrevert = string.Empty;
        public static string readversionnew = string.Empty; 
        public static string readversiondevice = string.Empty;
        //ตัวแปรเกี่ยว Donwload
        //MDR
        private BackgroundWorker downloadWorker;       
        public Storyboard storyboard;
        public Storyboard storyboarding;
        //HICM UPLOAD REP/STATEMENT

        //ตัวแปรเก็บค่าจากเครื่องคอมในการบันทึก
        public static string BuildNumber = "", Caption = "", Computername = "", SystemType = "", SerialNumber = "", Brand = "";
        //เชื่อมต่อ DB
        public static string connectionString = "";
        //กำหนดโปรแกรมติดตั้งว่า USER กดเลือกโปรแกรมอะไร
        public static bool useMDR, useRepStatement, useIncomereport, useEclaimAPI;
        public frmMenuMain()
        {
            InitializeComponent();
        }
        private void borderExit_MouseEnter(object sender, MouseEventArgs e)
        {
            borderExit.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF940000"));
        }

        private void borderExit_MouseLeave(object sender, MouseEventArgs e)
        {
            borderExit.Background = Brushes.Red;
        }

        private void borderExit_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.Close();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            HI7.Class.HIUility.getHcode();
            Brand = Environment.UserName;
            try
            {
                ManagementObjectSearcher searcher = new ManagementObjectSearcher("SELECT * FROM Win32_OperatingSystem");
                ManagementObjectCollection collection = searcher.Get();
                foreach (ManagementObject obj in collection)
                {
                    if (!string.IsNullOrEmpty(obj["BuildNumber"].ToString()))
                    {
                         BuildNumber = obj["BuildNumber"].ToString();
                    }
                    else
                    {
                         BuildNumber = "N/A";
                    }
                    if (!string.IsNullOrEmpty(obj["Caption"].ToString()))
                    {
                         Caption = obj["Caption"].ToString();
                    }
                    else
                    {
                         Caption = "N/A";
                    }
                    if (!string.IsNullOrEmpty(obj["CSName"].ToString()))
                    {
                         Computername = obj["CSName"].ToString();
                    }
                    else
                    {
                         Computername = "N/A";
                    }
                    if (!string.IsNullOrEmpty(obj["OSArchitecture"].ToString()))
                    {
                         SystemType = obj["OSArchitecture"].ToString();
                    }
                    else
                    {
                         SystemType = "N/A";
                    }
                    if (!string.IsNullOrEmpty(obj["SerialNumber"].ToString()))
                    {
                         SerialNumber = obj["SerialNumber"].ToString();
                    }
                    else
                    {
                         SerialNumber = "N/A";
                    }         
                }
            }
            catch
            {
                
            }
            //ConnectDB();
            //UpdateData();

            //SelectData();
            Reset();
            GetHospitalname();
            //getCheckversion();
            //getCheckversionhicm();
            //getCheckversionhicmeclaim();
            //getCheckversionhicmincome();
            GetVersion();
            GetVersionHi7main();
            GetVersionhicm();
            GetVersioneclaimapi();
            GetVersioneCheckHicm();

            //getCheckversion();
        }
        void ConnectDB()
        {
            string server = "203.114.123.212";
            string user = "hi7";
            string password = "hi712345678##";
            string database = "hi7"; // แทนด้วยชื่อของฐานข้อมูลที่ต้องการเชื่อมต่อ
            int port = 3306;

            // สร้าง Connection String
            connectionString = $"Server={server};Port={port};Database={database};Uid={user};Pwd={password};";
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    
                }
                catch (Exception ex)
                {
                    
                }
            }
        }
        public void UpdateData(string data)
        {
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    string sql = data;
                    using (MySqlCommand command = new MySqlCommand(sql, connection))
                    {
                        //command.Parameters.AddWithValue("@newData", newData);
                        //command.Parameters.AddWithValue("@id", id);
                        int rowsAffected = command.ExecuteNonQuery();
                        //Console.WriteLine("Rows affected: " + rowsAffected);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error: " + ex.Message);
                }
            }
        }
        public bool InsertData(string data)
        {
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                try
                {
                    connection.Open();

                    string sql = "INSERT INTO historysetup (hcode, brand, computername, sn, os, systemtype, buildnumber, programinstall, versionprogram, status, dateaction) VALUES ("+data+")";
                    using (MySqlCommand command = new MySqlCommand(sql, connection))
                    {
                        int rowsAffected = command.ExecuteNonQuery();
                        // If the INSERT is successful, return true
                        return rowsAffected > 0;
                    }
                }
                catch (Exception ex)
                {
                    // If there's an error during INSERT, return false
                    return false;
                }
            }
        }       
        private DataTable SelectData(string hcode,string serialNumber)
        {
            DataTable dataTable = new DataTable();
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                try
                {
                    // Add AllowZeroDateTime=True to the connection string
                    connectionString += ";ConvertZeroDateTime=True";

                    connection.Open();
                    string sql = "SELECT * FROM historysetup WHERE historysetup.sn = "+"'"+ serialNumber+"' AND "+ "historysetup.hcode = '"+hcode+"'";

                    // Assuming you have a valid SerialNumber value, otherwise handle it accordingly
                    //string serialNumber = "00327-36345-49826-AAOEM";

                    using (MySqlCommand command = new MySqlCommand(sql, connection))
                    {
                        // Assign value to the command parameter
                        //command.Parameters.AddWithValue("@serialNumber", serialNumber);

                        using (MySqlDataAdapter adapter = new MySqlDataAdapter(command))
                        {
                            
                            adapter.Fill(dataTable);
                            return dataTable;
                        }
                    }
                }
                catch (Exception ex)
                {
                    return dataTable;
                }
            }
        }

        void Reset()
        {
            versionnew = string.Empty;
            versiondevice = string.Empty;
            versionrevert = string.Empty;
            readversionnew = string.Empty;
            readversiondevice = string.Empty;
        }
        void GetHospitalname()
        {
            HI7.Class.HIUility.getHostname();
            lbhospitalname.Text = HI7.Class.HIUility._HOSPITALNAME;
        }
        void GetVersion()
        {
            Cardleftver.Text = "";
            if (File.Exists(PathMdrVerDevice))
            {
                // อ่านข้อมูลจากไฟล์
                string[] lines = File.ReadAllLines(PathMdrVerDevice);
                // วนลูปตรวจสอบทุกบรรทัด
                foreach (string line in lines)
                {
                    // ตรวจสอบว่าบรรทัดนั้นเป็นบรรทัดที่มีเวอร์ชัน
                    if (line.StartsWith("Version:"))
                    {
                        // ดึงเฉพาะเวอร์ชันออกมา
                        versiondevice = line.Replace("Version: ", "").Trim();
                        Mdrversion.Text = "V." + versiondevice;
                        Cardleftver.Text = "V."+versiondevice;
                        borderIconMdr.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFD34A4F"));                        
                        break;
                    }
                }
            }
            if (File.Exists(PathMdrVerNew))
            {
                // อ่านข้อมูลจากไฟล์
                string[] lines = File.ReadAllLines(PathMdrVerNew);
                // วนลูปตรวจสอบทุกบรรทัด
                foreach (string line in lines)
                {
                    // ตรวจสอบว่าบรรทัดนั้นเป็นบรรทัดที่มีเวอร์ชัน
                    if (line.StartsWith("Version:"))
                    {
                        // ดึงเฉพาะเวอร์ชันออกมา
                        versionnew = line.Replace("Version:", "").Trim();
                        break;
                    }
                }
            }
            if (File.Exists(PathMdrVerNew))
            {
                // อ่านข้อมูลจากไฟล์
                string[] lines = File.ReadAllLines(PathMdrVerNew);
                if (lines.Length > 0)
                {
                    try {
                        string line = lines[1];
                        readversionnew = line.Replace("Read:", "").Trim();
                    } catch { }
                    
                }
            }
            if (File.Exists(PathMdrVerDevice))
            {
                // อ่านข้อมูลจากไฟล์
                string[] lines = File.ReadAllLines(PathMdrVerDevice);
                if (lines.Length > 0)
                {
                    try {
                        string line = lines[1];
                        readversiondevice = line.Replace("Read:", "").Trim();
                    }
                    catch { }
                    
                }
            }
            if (File.Exists(PathMdrVerRevert))
            {
                // อ่านข้อมูลจากไฟล์
                string[] lines = File.ReadAllLines(PathMdrVerRevert);
                // วนลูปตรวจสอบทุกบรรทัด
                foreach (string line in lines)
                {
                    // ตรวจสอบว่าบรรทัดนั้นเป็นบรรทัดที่มีเวอร์ชัน
                    if (line.StartsWith("Version:"))
                    {
                        // ดึงเฉพาะเวอร์ชันออกมา
                        versionrevert = line.Replace("Version:", "").Trim();
                        break;
                    }
                }
            }
        }
        //HICM UPLOAD REP/STATEMENT
        void GetVersionhicm()
        {
            //useRepStatement = true;
            CardleftURSver.Text = "";
            if (File.Exists(PathMdrVerDevicehicm))
            {
                // อ่านข้อมูลจากไฟล์
                string[] lines = File.ReadAllLines(PathMdrVerDevicehicm);
                // วนลูปตรวจสอบทุกบรรทัด
                foreach (string line in lines)
                {
                    // ตรวจสอบว่าบรรทัดนั้นเป็นบรรทัดที่มีเวอร์ชัน
                    if (line.StartsWith("Version:"))
                    {
                        // ดึงเฉพาะเวอร์ชันออกมา
                        versiondevice = line.Replace("Version: ", "").Trim();
                        Mdrversion.Text = "V." + versiondevice;
                        CardleftURSver.Text = "V."+versiondevice;
                        borderIconRepStatement.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFD34A4F"));
                        break;
                    }
                }
            }
            if (File.Exists(PathMdrVerNewhicm))
            {
                // อ่านข้อมูลจากไฟล์
                string[] lines = File.ReadAllLines(PathMdrVerNewhicm);
                // วนลูปตรวจสอบทุกบรรทัด
                foreach (string line in lines)
                {
                    // ตรวจสอบว่าบรรทัดนั้นเป็นบรรทัดที่มีเวอร์ชัน
                    if (line.StartsWith("Version:"))
                    {
                        // ดึงเฉพาะเวอร์ชันออกมา
                        versionnew = line.Replace("Version:", "").Trim();
                        break;
                    }
                }
            }           
            if (File.Exists(PathMdrVerDevicehicm))
            {
                // อ่านข้อมูลจากไฟล์
                string[] lines = File.ReadAllLines(PathMdrVerDevicehicm);
                if (lines.Length > 0)
                {
                    try
                    {
                        string line = lines[1];
                        readversiondevice = line.Replace("Read:", "").Trim();
                    }
                    catch { }

                }
            }
            if (File.Exists(PathMdrVerReverthicm))
            {
                // อ่านข้อมูลจากไฟล์
                string[] lines = File.ReadAllLines(PathMdrVerReverthicm);
                // วนลูปตรวจสอบทุกบรรทัด
                foreach (string line in lines)
                {
                    // ตรวจสอบว่าบรรทัดนั้นเป็นบรรทัดที่มีเวอร์ชัน
                    if (line.StartsWith("Version:"))
                    {
                        // ดึงเฉพาะเวอร์ชันออกมา
                        versionrevert = line.Replace("Version:", "").Trim();
                        break;
                    }
                }
            }
        }
        //HICM KHO
        void GetVersioneclaimapi()
        {
            Mdrversion.Text = "0.0.0";
            CardleftEclaimapiver.Text = "";
            if (File.Exists(PathMdrVerDeviceEclaim))
            {
                // อ่านข้อมูลจากไฟล์
                string[] lines = File.ReadAllLines(PathMdrVerDeviceEclaim);
                // วนลูปตรวจสอบทุกบรรทัด
                foreach (string line in lines)
                {
                    // ตรวจสอบว่าบรรทัดนั้นเป็นบรรทัดที่มีเวอร์ชัน
                    if (line.StartsWith("Version:"))
                    {
                        // ดึงเฉพาะเวอร์ชันออกมา
                        versiondevice = line.Replace("Version:", "").Trim();
                        Mdrversion.Text = "V." + versiondevice;
                        CardleftEclaimapiver.Text = "V." + versiondevice;
                        borderIconApi.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFD34A4F"));
                        break;
                    }
                }
            }
            if (File.Exists(PathMdrVerNewEclaim))
            {
                // อ่านข้อมูลจากไฟล์
                string[] lines = File.ReadAllLines(PathMdrVerNewEclaim);
                // วนลูปตรวจสอบทุกบรรทัด
                foreach (string line in lines)
                {
                    // ตรวจสอบว่าบรรทัดนั้นเป็นบรรทัดที่มีเวอร์ชัน
                    if (line.StartsWith("Version:"))
                    {
                        // ดึงเฉพาะเวอร์ชันออกมา
                        versionnew = line.Replace("Version:", "").Trim();
                        break;
                    }
                }
            }
            if (File.Exists(PathMdrVerDeviceEclaim))
            {
                // อ่านข้อมูลจากไฟล์
                string[] lines = File.ReadAllLines(PathMdrVerDeviceEclaim);
                if (lines.Length > 0)
                {
                    try
                    {
                        string line = lines[1];
                        readversiondevice = line.Replace("Read:", "").Trim();
                    }
                    catch { }

                }
            }
            if (File.Exists(PathMdrVerRevertEclaim))
            {
                // อ่านข้อมูลจากไฟล์
                string[] lines = File.ReadAllLines(PathMdrVerRevertEclaim);
                // วนลูปตรวจสอบทุกบรรทัด
                foreach (string line in lines)
                {
                    // ตรวจสอบว่าบรรทัดนั้นเป็นบรรทัดที่มีเวอร์ชัน
                    if (line.StartsWith("Version:"))
                    {
                        // ดึงเฉพาะเวอร์ชันออกมา
                        versionrevert = line.Replace("Version:", "").Trim();
                        break;
                    }
                }
            }
        }
        //HI7 MAIN GetVersionHi7main
        void GetVersionHi7main()
        {
            Versionmain.Text = "Ver.0.0.0";
            versiondevice = "";
            versionnew = "";
            try {
                if (File.Exists(PathMainVerDevice))
                {
                    // อ่านข้อมูลจากไฟล์
                    string[] lines = File.ReadAllLines(PathMainVerDevice);
                    // วนลูปตรวจสอบทุกบรรทัด
                    foreach (string line in lines)
                    {
                        // ตรวจสอบว่าบรรทัดนั้นเป็นบรรทัดที่มีเวอร์ชัน
                        if (line.StartsWith("Version:"))
                        {
                            // ดึงเฉพาะเวอร์ชันออกมา
                            versiondevice = line.Replace("Version:", "").Trim();
                            Versionmain.Text = "Ver." + versiondevice;
                            break;
                        }
                    }
                }
            }
            catch
            {

            }
            try
            {
                if (File.Exists(PathMainVerNew))
                {
                    // อ่านข้อมูลจากไฟล์
                    string[] lines = File.ReadAllLines(PathMainVerNew);
                    // วนลูปตรวจสอบทุกบรรทัด
                    foreach (string line in lines)
                    {
                        // ตรวจสอบว่าบรรทัดนั้นเป็นบรรทัดที่มีเวอร์ชัน
                        if (line.StartsWith("Version:"))
                        {
                            // ดึงเฉพาะเวอร์ชันออกมา
                            versionnew = line.Replace("Version:", "").Trim();
                            break;
                        }
                    }
                }
            }
            catch { }
            if (versiondevice == versionnew)
            {
                Versionmain.Text = "Ver." + versiondevice+ " (ล่าสุด)";
            }
            else if (versiondevice != versionnew)
            {
                btnUpdateMain.Visibility = Visibility.Visible;
                Versionmain.Text = "Ver." + versiondevice + " (มีอัพเดต)";
                txtblockStatus.Text = "*New Ver." + versionnew;
                txtblockStatus.Visibility = Visibility.Visible;
            }
            
                        
        }

        //HICM CKECK HICM KHO 
        void GetVersioneCheckHicm()
        {
            Mdrversion.Text = "0.0.0";
            CardleftCheckHICM.Text = "";
            if (File.Exists(PathMdrVerDeviceHicmreport))
            {
                // อ่านข้อมูลจากไฟล์
                string[] lines = File.ReadAllLines(PathMdrVerDeviceHicmreport);
                // วนลูปตรวจสอบทุกบรรทัด
                foreach (string line in lines)
                {
                    // ตรวจสอบว่าบรรทัดนั้นเป็นบรรทัดที่มีเวอร์ชัน
                    if (line.StartsWith("Version:"))
                    {
                        // ดึงเฉพาะเวอร์ชันออกมา
                        versiondevice = line.Replace("Version:", "").Trim();
                        Mdrversion.Text = "V." + versiondevice;
                        CardleftCheckHICM.Text = "V." + versiondevice;
                        borderIconIncome.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFD34A4F"));
                        break;
                    }
                }
            }
            if (File.Exists(PathMdrVerNewHicmreport))
            {
                // อ่านข้อมูลจากไฟล์
                string[] lines = File.ReadAllLines(PathMdrVerNewHicmreport);
                // วนลูปตรวจสอบทุกบรรทัด
                foreach (string line in lines)
                {
                    // ตรวจสอบว่าบรรทัดนั้นเป็นบรรทัดที่มีเวอร์ชัน
                    if (line.StartsWith("Version:"))
                    {
                        // ดึงเฉพาะเวอร์ชันออกมา
                        versionnew = line.Replace("Version:", "").Trim();
                        break;
                    }
                }
            }
            if (File.Exists(PathMdrVerDeviceHicmreport))
            {
                // อ่านข้อมูลจากไฟล์
                string[] lines = File.ReadAllLines(PathMdrVerDeviceHicmreport);
                if (lines.Length > 0)
                {
                    try
                    {
                        string line = lines[1];
                        readversiondevice = line.Replace("Read:", "").Trim();
                    }
                    catch { }

                }
            }
            if (File.Exists(PathMdrVerRevertHicmreport))
            {
                // อ่านข้อมูลจากไฟล์
                string[] lines = File.ReadAllLines(PathMdrVerRevertHicmreport);
                // วนลูปตรวจสอบทุกบรรทัด
                foreach (string line in lines)
                {
                    // ตรวจสอบว่าบรรทัดนั้นเป็นบรรทัดที่มีเวอร์ชัน
                    if (line.StartsWith("Version:"))
                    {
                        // ดึงเฉพาะเวอร์ชันออกมา
                        versionrevert = line.Replace("Version:", "").Trim();
                        break;
                    }
                }
            }
        }
        void getCheckversion()
        {
            GetVersion();
            Cardleftver.Text = "";
            nameMdr.Text = "";
            Mdrversion.Text = "0.0.0";
            if (!string.IsNullOrEmpty(versionnew))
            {
                if (versiondevice != versionnew)//ต่างเวอร์ชั่นกัน
                {
                    if (!string.IsNullOrEmpty(versiondevice))
                    {
                        nameMdr.Text = "โปรแกรมเวชระเบียน(มีอัพเดต): " + versionnew;
                        icon.Visibility = Visibility.Visible;
                        detail.Visibility = Visibility.Visible;
                        //picslide.Visibility = Visibility.Visible;
                        btncardprogram.IsEnabled = true;
                        btnMdrinstall.Visibility = Visibility.Collapsed;
                        btnMdrrevert.Visibility = Visibility.Collapsed;
                        btnMdrupdate.Visibility = Visibility.Visible;

                    }
                    else
                    {
                        nameMdr.Text = "โปรแกรมเวชระเบียน(ยังไม่ได้ติดตั้ง)";
                        btnMdrinstall.Visibility = Visibility.Visible;
                        btnMdrrevert.Visibility = Visibility.Collapsed;
                        btnMdrupdate.Visibility = Visibility.Collapsed;
                        icon.Visibility = Visibility.Visible;
                        detail.Visibility = Visibility.Visible;
                        //picslide.Visibility = Visibility.Visible;
                        btncardprogram.IsEnabled = false;
                    }
                    
                    
                }
                else if (versiondevice == versionnew)
                {
                    nameMdr.Text = "โปรแกรมเวชระเบียน(ล่าสุด)";
                    icon.Visibility = Visibility.Visible;
                    detail.Visibility = Visibility.Visible;
                    //picslide.Visibility = Visibility.Visible;
                    btncardprogram.IsEnabled = true;
                    btnMdrinstall.Visibility = Visibility.Collapsed;
                    btnMdrrevert.Visibility = Visibility.Collapsed;
                    btnMdrupdate.Visibility = Visibility.Collapsed;
                    //nameMdr.Text = "MDR(ล่าสุด)";
                    //btnMdrrevert.Visibility = !string.IsNullOrEmpty(versionrevert) ? Visibility.Visible : Visibility.Collapsed;
                    //btnMdrrevert.Content = !string.IsNullOrEmpty(versionrevert) ? "ย้อน V." + versionrevert : null;
                    //viewDevice.Visibility = Visibility.Visible;
                }
            }
            else
            {//ยังไม่ติดตั้ง
                nameMdr.Text = "โปรแกรมเวชระเบียน(ยังไม่ได้ติดตั้ง)";
                btnMdrinstall.Visibility = Visibility.Visible;
                btnMdrrevert.Visibility = Visibility.Collapsed;
                btnMdrupdate.Visibility = Visibility.Collapsed;
                icon.Visibility = Visibility.Visible;
                detail.Visibility = Visibility.Visible;
                //picslide.Visibility = Visibility.Visible;
                btncardprogram.IsEnabled = false;

            }
        }
        //HICM UPLOAD/STATEMENT
        void getCheckversionhicm()
        {
            GetVersionhicm();
            nameMdr.Text = "";
            if (!string.IsNullOrEmpty(versionnew))
            {
                if (versiondevice != versionnew)//ต่างเวอร์ชั่นกัน
                {
                    if (!string.IsNullOrEmpty(versiondevice))
                    {
                        nameMdr.Text = "โปรแกรมเวชระเบียน(มีอัพเดต)V. " + versionnew;
                        btnMdrinstall.Visibility = Visibility.Collapsed;
                        btnMdrrevert.Visibility = Visibility.Collapsed;
                        btnMdrupdate.Visibility = Visibility.Visible;
                    }
                    else
                    {
                        nameMdr.Text = "โปรแกรมเวชระเบียน(ยังไม่ได้ติดตั้ง)";
                        btnMdrinstall.Visibility = Visibility.Visible;
                        btnMdrrevert.Visibility = Visibility.Collapsed;
                        btnMdrupdate.Visibility = Visibility.Collapsed;
                        icon.Visibility = Visibility.Visible;
                        detail.Visibility = Visibility.Visible;
                        btncardprogram.IsEnabled = false;

                    }


                }
                else if (versiondevice == versionnew)
                {
                    nameMdr.Text = "โปรแกรมเวชระเบียน(ล่าสุด)";
                    icon.Visibility = Visibility.Visible;
                    detail.Visibility = Visibility.Visible;
                    btncardprogram.IsEnabled = true;
                    btnMdrinstall.Visibility = Visibility.Collapsed;
                    btnMdrrevert.Visibility = Visibility.Collapsed;
                    btnMdrupdate.Visibility = Visibility.Collapsed;
                }
            }
            else
            {//ยังไม่ติดตั้ง
                nameMdr.Text = "โปรแกรมเวชระเบียน(ยังไม่ได้ติดตั้ง)";
                btnMdrinstall.Visibility = Visibility.Visible;
                btnMdrrevert.Visibility = Visibility.Collapsed;
                btnMdrupdate.Visibility = Visibility.Collapsed;
                icon.Visibility = Visibility.Visible;
                detail.Visibility = Visibility.Visible;
                btncardprogram.IsEnabled = false;

            }
        }
        //HICM Eclaim api 
        void getCheckversionhicmeclaim()
        {
            GetVersioneclaimapi();
            nameMdr.Text = "";
            if (!string.IsNullOrEmpty(versionnew))
            {
                if (versiondevice != versionnew)//ต่างเวอร์ชั่นกัน
                {
                    if (!string.IsNullOrEmpty(versiondevice))
                    {
                        nameMdr.Text = "Eclaim API ส่ง16แฟ้ม(มีอัพเดต)V. "+ versionnew;                      
                        btnMdrinstall.Visibility = Visibility.Collapsed;
                        btnMdrrevert.Visibility = Visibility.Collapsed;
                        btnMdrupdate.Visibility = Visibility.Visible;
                    }
                    else
                    {
                        nameMdr.Text = "Eclaim API ส่ง16แฟ้ม(ยังไม่ได้ติดตั้ง)";
                        btnMdrinstall.Visibility = Visibility.Visible;
                        btnMdrrevert.Visibility = Visibility.Collapsed;
                        btnMdrupdate.Visibility = Visibility.Collapsed;
                        icon.Visibility = Visibility.Visible;
                        detail.Visibility = Visibility.Visible;
                        btncardprogram.IsEnabled = false;
                        
                    }


                }
                else if (versiondevice == versionnew)
                {
                    nameMdr.Text = "Eclaim API ส่ง16แฟ้ม(ล่าสุด)";
                    icon.Visibility = Visibility.Visible;
                    detail.Visibility = Visibility.Visible;
                    btncardprogram.IsEnabled = true;
                    btnMdrinstall.Visibility = Visibility.Collapsed;
                    btnMdrrevert.Visibility = Visibility.Collapsed;
                    btnMdrupdate.Visibility = Visibility.Collapsed;
                }
            }
            else
            {//ยังไม่ติดตั้ง
                nameMdr.Text = "Eclaim API ส่ง16แฟ้ม(ยังไม่ได้ติดตั้ง)";
                btnMdrinstall.Visibility = Visibility.Visible;
                btnMdrrevert.Visibility = Visibility.Collapsed;
                btnMdrupdate.Visibility = Visibility.Collapsed;
                icon.Visibility = Visibility.Visible;
                detail.Visibility = Visibility.Visible;
                btncardprogram.IsEnabled = false;

            }
        }
        //HICM HICM INCOME 
        void getCheckversionhicmincome()
        {
            GetVersioneCheckHicm();
            nameMdr.Text = "";
            if (!string.IsNullOrEmpty(versionnew))
            {
                if (versiondevice != versionnew)//ต่างเวอร์ชั่นกัน
                {
                    if (!string.IsNullOrEmpty(versiondevice))
                    {
                        nameMdr.Text = "Income Report(มีอัพเดต)V. " + versionnew;
                        btnMdrinstall.Visibility = Visibility.Collapsed;
                        btnMdrrevert.Visibility = Visibility.Collapsed;
                        btnMdrupdate.Visibility = Visibility.Visible;
                    }
                    else
                    {
                        nameMdr.Text = "Income Report(ยังไม่ได้ติดตั้ง)";
                        btnMdrinstall.Visibility = Visibility.Visible;
                        btnMdrrevert.Visibility = Visibility.Collapsed;
                        btnMdrupdate.Visibility = Visibility.Collapsed;
                        icon.Visibility = Visibility.Visible;
                        detail.Visibility = Visibility.Visible;
                        btncardprogram.IsEnabled = false;

                    }


                }
                else if (versiondevice == versionnew)
                {
                    nameMdr.Text = "Income Report(ล่าสุด)";
                    icon.Visibility = Visibility.Visible;
                    detail.Visibility = Visibility.Visible;
                    btncardprogram.IsEnabled = true;
                    btnMdrinstall.Visibility = Visibility.Collapsed;
                    btnMdrrevert.Visibility = Visibility.Collapsed;
                    btnMdrupdate.Visibility = Visibility.Collapsed;
                }
            }
            else
            {//ยังไม่ติดตั้ง
                nameMdr.Text = "Income Report(ยังไม่ได้ติดตั้ง)";
                btnMdrinstall.Visibility = Visibility.Visible;
                btnMdrrevert.Visibility = Visibility.Collapsed;
                btnMdrupdate.Visibility = Visibility.Collapsed;
                icon.Visibility = Visibility.Visible;
                detail.Visibility = Visibility.Visible;
                btncardprogram.IsEnabled = false;

            }
        }
        private void viewUpdate_MouseDown(object sender, MouseButtonEventArgs e)
        {
            UserControlReadtext userControl = new UserControlReadtext();
            string text = userControl.Message = readversionnew;
            System.Windows.Window window = new System.Windows.Window
            {
                Title = "อ่านรายละเอียดในการอัพเดตเวอร์ชั่น: "+versionnew+"!!!",
                WindowStartupLocation = WindowStartupLocation.CenterScreen,
                ResizeMode = ResizeMode.NoResize,
                FontFamily = new FontFamily("Kanit"), // กำหนด FontFamily
                FontSize = 18,
            };
            // สร้าง StackPanel สำหรับเก็บเนื้อหา
            StackPanel stackPanel = new StackPanel
            {
                Margin = new Thickness(10) // กำหนดระยะห่างรอบด้านให้ StackPanel
            };

            string[] lines = text.Split("\\n");

            foreach (string line in lines)
            {
                string trimmedLine = line.Trim(); // ตัดช่องว่างหน้าและหลังข้อความ
                if (!string.IsNullOrEmpty(trimmedLine))
                {
                    TextBlock textBlock = new TextBlock
                    {
                        Text = trimmedLine,
                        TextWrapping = TextWrapping.Wrap
                    };
                    stackPanel.Children.Add(textBlock);
                }
            }

            // กำหนด StackPanel เป็นเนื้อหาของหน้าต่าง Dialog
            window.Content = stackPanel;

            // แสดงหน้าต่าง Dialog
            window.SizeToContent = SizeToContent.Manual;
            window.MaxWidth = 800;
            window.MaxHeight = 450;
            window.ShowDialog();
        }
        private void viewDevice_MouseDown(object sender, MouseButtonEventArgs e)
        {
            UserControlReadtext userControl = new UserControlReadtext();
            string text = userControl.Message = readversiondevice;
            System.Windows.Window window = new System.Windows.Window
            {
                Title = "อ่านรายละเอียดเวอร์ชั่นปัจจุบัน: "+versiondevice+"!!!",
                WindowStartupLocation = WindowStartupLocation.CenterScreen,
                ResizeMode = ResizeMode.NoResize,
                FontFamily = new FontFamily("Kanit"), // กำหนด FontFamily
                FontSize = 18,
            };
            // สร้าง StackPanel สำหรับเก็บเนื้อหา
            StackPanel stackPanel = new StackPanel
            {
                Margin = new Thickness(10) // กำหนดระยะห่างรอบด้านให้ StackPanel
            };

            string[] lines = text.Split("\\n");

            foreach (string line in lines)
            {
                string trimmedLine = line.Trim(); // ตัดช่องว่างหน้าและหลังข้อความ
                if (!string.IsNullOrEmpty(trimmedLine))
                {
                    TextBlock textBlock = new TextBlock
                    {
                        Text = trimmedLine,
                        TextWrapping = TextWrapping.Wrap
                    };
                    stackPanel.Children.Add(textBlock);
                }
            }

            // กำหนด StackPanel เป็นเนื้อหาของหน้าต่าง Dialog
            window.Content = stackPanel;

            // แสดงหน้าต่าง Dialog
            window.SizeToContent = SizeToContent.Manual;
            window.MaxWidth = 800;
            window.MaxHeight = 450;
            window.ShowDialog();
        }
        
        private void packIconCard_MouseEnter(object sender, MouseEventArgs e)
        {
            packIconCard.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF63609C"));
        }
        private void packIconCard_MouseLeave(object sender, MouseEventArgs e)
        {
            packIconCard.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF9895D0"));
        }
        private bool isStoryboardPlaying = false;
        private bool isStoryboardPlayingloading = false;
        private void btnMdrupdate_Click(object sender, RoutedEventArgs e)
        {
            GetDisableprogram();
            nameMdr.Text = "กำลังอัพเดต";
            btncardprogram.IsEnabled = false;
            if (!isStoryboardPlaying)
            {
                //useMDR, useRepStatement, useIncomereport, useEclaimAPI;
                if (useMDR == true)
                {
                    RunBackgroundTask();
                }
                else if(useRepStatement == true)
                {
                    RunBackgroundTaskUpdateHICM();
                }
                else if (useIncomereport == true)
                {
                    RunBackgroundTaskUpdateHICMEIncomereport();
                }
                else if (useEclaimAPI == true)
                {
                    RunBackgroundTaskUpdateHICMEclaimapi();
                }
                
                isStoryboardPlaying = true;
                isStoryboardPlayingloading = true;
            }
        }
        //MDR
        private void RunBackgroundTaskinstall()
        {
            downloadWorker = new BackgroundWorker();
            downloadWorker.DoWork += DownloadWorker_DoWorkinstall;
            downloadWorker.RunWorkerCompleted += DownloadWorker_RunWorkerCompletedinstall;
            downloadWorker.RunWorkerAsync();
        } 
        //HICM REP STATEMENT INSTALL
        private void RunBackgroundTaskinstallRepStatement()
        {
            downloadWorker = new BackgroundWorker();
            downloadWorker.DoWork += DownloadWorker_DoWorkinstallRepStatement;
            downloadWorker.RunWorkerCompleted += DownloadWorker_RunWorkerCompletedinstallRepStatement;
            downloadWorker.RunWorkerAsync();
        }
        //HICM ECLAIM API INSTALL 
        private void RunBackgroundTaskinstallEclaimapi()
        {
            downloadWorker = new BackgroundWorker();
            downloadWorker.DoWork += DownloadWorker_DoWorkinstallEclaimapi;
            downloadWorker.RunWorkerCompleted += DownloadWorker_RunWorkerCompletedinstallEclaimapi;
            downloadWorker.RunWorkerAsync();
        }
        //HICM INCOME REOIRT 
        private void RunBackgroundTaskinstallIncomereport()
        {
            downloadWorker = new BackgroundWorker();
            downloadWorker.DoWork += DownloadWorker_DoWorkinstallIncomereport;
            downloadWorker.RunWorkerCompleted += DownloadWorker_RunWorkerCompletedinstallIncomereport;
            downloadWorker.RunWorkerAsync();
        }
        //MDR UPDATE
        private void RunBackgroundTask()
        {
            downloadWorker = new BackgroundWorker();
            downloadWorker.DoWork += DownloadWorker_DoWork;
            downloadWorker.RunWorkerCompleted += DownloadWorker_RunWorkerCompleted;
            downloadWorker.RunWorkerAsync();
        }
        //HICM REP STATEMENT 
        private void RunBackgroundTaskUpdateHICM()
        {
            downloadWorker = new BackgroundWorker();
            downloadWorker.DoWork += DownloadWorker_DoWorkHICM;
            downloadWorker.RunWorkerCompleted += DownloadWorker_RunWorkerCompletedHICM;
            downloadWorker.RunWorkerAsync();
        }
        //HICM ECLAIM API 
        private void RunBackgroundTaskUpdateHICMEclaimapi()
        {
            downloadWorker = new BackgroundWorker();
            downloadWorker.DoWork += DownloadWorker_DoWorkHICMclaimapi;
            downloadWorker.RunWorkerCompleted += DownloadWorker_RunWorkerCompletedHICMclaimapi;
            downloadWorker.RunWorkerAsync();
        }
        //HI7 MAIN 
        private void RunBackgroundTaskUpdateHi7main()
        {
            downloadWorker = new BackgroundWorker();
            downloadWorker.DoWork += DownloadWorker_DoWorkHi7main;
            downloadWorker.RunWorkerCompleted += DownloadWorker_RunWorkerCompletedHi7main;
            downloadWorker.RunWorkerAsync();
        }
        //HICM INCOME REPORT 
        private void RunBackgroundTaskUpdateHICMEIncomereport()
        {
            downloadWorker = new BackgroundWorker();
            downloadWorker.DoWork += DownloadWorker_DoWorkHICMIncomereport;
            downloadWorker.RunWorkerCompleted += DownloadWorker_RunWorkerCompletedHICMIncomereport;
            downloadWorker.RunWorkerAsync();
        }
        //ตรวจสอบ MDR
        private void RunBackgroundTaskCheck()
        {
            downloadWorker = new BackgroundWorker();
            downloadWorker.DoWork += DownloadWorker_DoWorkCheck;
            downloadWorker.RunWorkerCompleted += DownloadWorker_RunWorkerCompletedCheck;
            downloadWorker.RunWorkerAsync();
        }
        //ตรวจสอบ HICM พี่แม็ก
        private void RunBackgroundTaskCheckHICM()
        {
            downloadWorker = new BackgroundWorker();
            downloadWorker.DoWork += DownloadWorker_DoWorkCheckHICM;
            downloadWorker.RunWorkerCompleted += DownloadWorker_RunWorkerCompletedCheckhicm;
            downloadWorker.RunWorkerAsync();
        }
        // HICM ECLAIM API
        private void RunBackgroundTaskCheckHICMeclaim()
        {
            downloadWorker = new BackgroundWorker();
            downloadWorker.DoWork += DownloadWorker_DoWorkCheckHICMclaim;
            downloadWorker.RunWorkerCompleted += DownloadWorker_RunWorkerCompletedCheckhicmclaim;
            downloadWorker.RunWorkerAsync();
        }
        // HICM CHECK INCOME 
        private void RunBackgroundTaskCheckHICMIncome()
        {
            downloadWorker = new BackgroundWorker();
            downloadWorker.DoWork += DownloadWorker_DoWorkCheckHICMIncome;
            downloadWorker.RunWorkerCompleted += DownloadWorker_RunWorkerCompletedCheckhicmIncome;
            downloadWorker.RunWorkerAsync();
        }
        //HI7 MAIN 
        private void RunBackgroundTaskCheckHi7main()
        {
            downloadWorker = new BackgroundWorker();
            downloadWorker.DoWork += DownloadWorker_DoWorkCheckHi7main;
            downloadWorker.RunWorkerCompleted += DownloadWorker_RunWorkerCompletedCheckHi7main;
            downloadWorker.RunWorkerAsync();
        }
        //MDR 
        private void DownloadWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            Application.Current.Dispatcher.Invoke(() =>
            {
                packIconCard.Kind = MaterialDesignThemes.Wpf.PackIconKind.CloudDownload;

                storyboard = (Storyboard)FindResource("StoryboardUpdate");
                if (storyboard != null)
                {
                    storyboard.Completed += Storyboard_Completed;
                    storyboard.Begin(packIconCard);
                }
            });
            Application.Current.Dispatcher.Invoke(() =>
            {
                gridLoading.Visibility = Visibility.Visible;
                // Find the storyboard resource using its key
                storyboarding = (Storyboard)FindResource("Storyboardloading");
                // If the storyboard is found, start it
                if (storyboarding != null)
                {
                    storyboarding.Completed += Storyboardlooding_Completed;
                    storyboarding.Begin();
                }
                else
                {
                }

            });
            DownloadFilesFromGitRepositoryOnlyFile();
        }
        //HICM MAX 
        private void DownloadWorker_DoWorkHICM(object sender, DoWorkEventArgs e)
        {
            Application.Current.Dispatcher.Invoke(() =>
            {
                packIconCard.Kind = MaterialDesignThemes.Wpf.PackIconKind.CloudDownload;

                storyboard = (Storyboard)FindResource("StoryboardUpdate");
                if (storyboard != null)
                {
                    storyboard.Completed += Storyboard_Completed;
                    storyboard.Begin(packIconCard);
                }
            });
            Application.Current.Dispatcher.Invoke(() =>
            {
                gridLoading.Visibility = Visibility.Visible;
                // Find the storyboard resource using its key
                storyboarding = (Storyboard)FindResource("Storyboardloading");
                // If the storyboard is found, start it
                if (storyboarding != null)
                {
                    storyboarding.Completed += Storyboardlooding_Completed;
                    storyboarding.Begin();
                }
                else
                {
                }

            });
            DownloadFilesFromGitRepositoryOnlyFileHICM();
        }
        //HICM ECLAIM API KHO 
        private void DownloadWorker_DoWorkHICMclaimapi(object sender, DoWorkEventArgs e)
        {
            Application.Current.Dispatcher.Invoke(() =>
            {
                packIconCard.Kind = MaterialDesignThemes.Wpf.PackIconKind.CloudDownload;

                storyboard = (Storyboard)FindResource("StoryboardUpdate");
                if (storyboard != null)
                {
                    storyboard.Completed += Storyboard_Completed;
                    storyboard.Begin(packIconCard);
                }
            });
            Application.Current.Dispatcher.Invoke(() =>
            {
                gridLoading.Visibility = Visibility.Visible;
                // Find the storyboard resource using its key
                storyboarding = (Storyboard)FindResource("Storyboardloading");
                // If the storyboard is found, start it
                if (storyboarding != null)
                {
                    storyboarding.Completed += Storyboardlooding_Completed;
                    storyboarding.Begin();
                }
                else
                {
                }

            });
            DownloadFilesFromGitRepositoryOnlyFileHICMclaimapi();
        }
        //HI& MAIN 
        private void DownloadWorker_DoWorkHi7main(object sender, DoWorkEventArgs e)
        {
            Application.Current.Dispatcher.Invoke(() =>
            {
                
            });
            DownloadFilesFromGitRepositoryOnlyFileHi7main();
        }
        //HICM INCOME REPORT 
        private void DownloadWorker_DoWorkHICMIncomereport(object sender, DoWorkEventArgs e)
        {
            Application.Current.Dispatcher.Invoke(() =>
            {
                packIconCard.Kind = MaterialDesignThemes.Wpf.PackIconKind.CloudDownload;

                storyboard = (Storyboard)FindResource("StoryboardUpdate");
                if (storyboard != null)
                {
                    storyboard.Completed += Storyboard_Completed;
                    storyboard.Begin(packIconCard);
                }
            });
            Application.Current.Dispatcher.Invoke(() =>
            {
                gridLoading.Visibility = Visibility.Visible;
                // Find the storyboard resource using its key
                storyboarding = (Storyboard)FindResource("Storyboardloading");
                // If the storyboard is found, start it
                if (storyboarding != null)
                {
                    storyboarding.Completed += Storyboardlooding_Completed;
                    storyboarding.Begin();
                }
                else
                {
                }

            });
            DownloadFilesFromGitRepositoryOnlyFileHICMIncomereport();
        }
        //MDR 
        private void DownloadWorker_DoWorkinstall(object sender, DoWorkEventArgs e)
        {
            Application.Current.Dispatcher.Invoke(() =>
            {
                packIconCard.Kind = MaterialDesignThemes.Wpf.PackIconKind.CloudDownload;

                storyboard = (Storyboard)FindResource("StoryboardUpdate");
                if (storyboard != null)
                {
                    storyboard.Completed += Storyboard_Completed;
                    storyboard.Begin(packIconCard);
                }
            });
            Application.Current.Dispatcher.Invoke(() =>
            {
                gridLoading.Visibility = Visibility.Visible;
                // Find the storyboard resource using its key
                storyboarding = (Storyboard)FindResource("Storyboardloading");
                // If the storyboard is found, start it
                if (storyboarding != null)
                {
                    storyboarding.Completed += Storyboardlooding_Completed;
                    storyboarding.Begin();
                }
                else
                {
                }

            });

            DownloadFilesFromGitRepositoryOnlyFileinstall();
        }
        //RepStatement
        private void DownloadWorker_DoWorkinstallRepStatement(object sender, DoWorkEventArgs e)
        {
            Application.Current.Dispatcher.Invoke(() =>
            {
                packIconCard.Kind = MaterialDesignThemes.Wpf.PackIconKind.CloudDownload;

                storyboard = (Storyboard)FindResource("StoryboardUpdate");
                if (storyboard != null)
                {
                    storyboard.Completed += Storyboard_Completed;
                    storyboard.Begin(packIconCard);
                }
            });
            Application.Current.Dispatcher.Invoke(() =>
            {
                gridLoading.Visibility = Visibility.Visible;
                // Find the storyboard resource using its key
                storyboarding = (Storyboard)FindResource("Storyboardloading");
                // If the storyboard is found, start it
                if (storyboarding != null)
                {
                    storyboarding.Completed += Storyboardlooding_Completed;
                    storyboarding.Begin();
                }
                else
                {
                }

            });

            DownloadFilesFromGitRepositoryOnlyFileinstallRepStatement();
        }
        //Eclain api 
        private void DownloadWorker_DoWorkinstallEclaimapi(object sender, DoWorkEventArgs e)
        {
            Application.Current.Dispatcher.Invoke(() =>
            {
                packIconCard.Kind = MaterialDesignThemes.Wpf.PackIconKind.CloudDownload;

                storyboard = (Storyboard)FindResource("StoryboardUpdate");
                if (storyboard != null)
                {
                    storyboard.Completed += Storyboard_Completed;
                    storyboard.Begin(packIconCard);
                }
            });
            Application.Current.Dispatcher.Invoke(() =>
            {
                gridLoading.Visibility = Visibility.Visible;
                // Find the storyboard resource using its key
                storyboarding = (Storyboard)FindResource("Storyboardloading");
                // If the storyboard is found, start it
                if (storyboarding != null)
                {
                    storyboarding.Completed += Storyboardlooding_Completed;
                    storyboarding.Begin();
                }
                else
                {
                }

            });

            DownloadFilesFromGitRepositoryOnlyFileinstallEclaimapi();
        }
        //Income report 
        private void DownloadWorker_DoWorkinstallIncomereport(object sender, DoWorkEventArgs e)
        {
            Application.Current.Dispatcher.Invoke(() =>
            {
                packIconCard.Kind = MaterialDesignThemes.Wpf.PackIconKind.CloudDownload;

                storyboard = (Storyboard)FindResource("StoryboardUpdate");
                if (storyboard != null)
                {
                    storyboard.Completed += Storyboard_Completed;
                    storyboard.Begin(packIconCard);
                }
            });
            Application.Current.Dispatcher.Invoke(() =>
            {
                gridLoading.Visibility = Visibility.Visible;
                // Find the storyboard resource using its key
                storyboarding = (Storyboard)FindResource("Storyboardloading");
                // If the storyboard is found, start it
                if (storyboarding != null)
                {
                    storyboarding.Completed += Storyboardlooding_Completed;
                    storyboarding.Begin();
                }
                else
                {
                }

            });

            DownloadFilesFromGitRepositoryOnlyFileinstallIncomereport();
        }
        //ตรวจสอบของ MDR
        private void DownloadWorker_DoWorkCheck(object sender, DoWorkEventArgs e)
        {
            Application.Current.Dispatcher.Invoke(() =>
            {
                packIconCard.Kind = MaterialDesignThemes.Wpf.PackIconKind.Search;

                storyboard = (Storyboard)FindResource("StoryboardUpdate");
                if (storyboard != null)
                {
                    storyboard.Completed += Storyboard_Completed;
                    storyboard.Begin(packIconCard);
                }
            });
            Application.Current.Dispatcher.Invoke(() =>
            {
                gridLoading.Visibility = Visibility.Visible;
                // Find the storyboard resource using its key
                storyboarding = (Storyboard)FindResource("Storyboardloading");
                // If the storyboard is found, start it
                if (storyboarding != null)
                {
                    storyboarding.Completed += Storyboardlooding_Completed;
                    storyboarding.Begin();
                }
                else
                {
                }

            });
            DownloadFilesFromGitRepositoryOnlyFileCheck();
        }
        //ตรวจสอบของ HICM statement/upload DownloadWorker_DoWorkCheckHICMclaim
        private void DownloadWorker_DoWorkCheckHICM(object sender, DoWorkEventArgs e)
        {
            Application.Current.Dispatcher.Invoke(() =>
            {
                packIconCard.Kind = MaterialDesignThemes.Wpf.PackIconKind.Search;

                storyboard = (Storyboard)FindResource("StoryboardUpdate");
                if (storyboard != null)
                {
                    storyboard.Completed += Storyboard_Completed;
                    storyboard.Begin(packIconCard);
                }
            });
            Application.Current.Dispatcher.Invoke(() =>
            {
                gridLoading.Visibility = Visibility.Visible;
                // Find the storyboard resource using its key
                storyboarding = (Storyboard)FindResource("Storyboardloading");
                // If the storyboard is found, start it
                if (storyboarding != null)
                {
                    storyboarding.Completed += Storyboardlooding_Completed;
                    storyboarding.Begin();
                }
                else
                {
                }

            });

            DownloadFilesFromGitRepositoryOnlyFileCheckHICM();
        }
        //ตรวจสอบของ HICM statement/upload 
        private void DownloadWorker_DoWorkCheckHICMclaim(object sender, DoWorkEventArgs e)
        {
            Application.Current.Dispatcher.Invoke(() =>
            {
                packIconCard.Kind = MaterialDesignThemes.Wpf.PackIconKind.Search;
                storyboard = (Storyboard)FindResource("StoryboardUpdate");
                if (storyboard != null)
                {
                    storyboard.Completed += Storyboard_Completed;
                    storyboard.Begin(packIconCard);
                }
            });
            Application.Current.Dispatcher.Invoke(() =>
            {
                gridLoading.Visibility = Visibility.Visible;
                // Find the storyboard resource using its key
                storyboarding = (Storyboard)FindResource("Storyboardloading");
                // If the storyboard is found, start it
                if (storyboarding != null)
                {
                    storyboarding.Completed += Storyboardlooding_Completed;
                    storyboarding.Begin();
                }               

            });

            DownloadFilesFromGitRepositoryOnlyFileCheckHICMclaim();
        }
        //ตรวจสอบก่อนส่งเครม HICM CHECK INCOME 
        private void DownloadWorker_DoWorkCheckHICMIncome(object sender, DoWorkEventArgs e)
        {
            
            Application.Current.Dispatcher.Invoke(() =>
            {
                packIconCard.Kind = MaterialDesignThemes.Wpf.PackIconKind.Search;
                storyboard = (Storyboard)FindResource("StoryboardUpdate");
                if (storyboard != null)
                {
                    storyboard.Completed += Storyboard_Completed;
                    storyboard.Begin(packIconCard);
                }
            });
            Application.Current.Dispatcher.Invoke(() =>
            {
                gridLoading.Visibility = Visibility.Visible;
                // Find the storyboard resource using its key
                storyboarding = (Storyboard)FindResource("Storyboardloading");               

                // If the storyboard is found, start it
                if (storyboarding != null)
                {
                    storyboarding.Completed += Storyboardlooding_Completed;
                    storyboarding.Begin();
                }
                else
                {
                    //MessageBox.Show("Storyboard not found.");
                }
                
            });            
            DownloadFilesFromGitRepositoryOnlyFileCheckHICMIncome();
        }
        //HI7 MDR 
        private void DownloadWorker_DoWorkCheckHi7main(object sender, DoWorkEventArgs e)
        {
            Application.Current.Dispatcher.Invoke(() =>
            {
                txtblockStatus.Visibility = Visibility.Visible;
            });

            DownloadFilesFromGitRepositoryOnlyFileCheckHi7main();
        }
        //MDR KEN
        private void DownloadWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            Application.Current.Dispatcher.Invoke(() =>
            {
                packIconCard.Kind = MaterialDesignThemes.Wpf.PackIconKind.CardAccountDetails;
            });
            
            isStoryboardPlaying = false;
            isStoryboardPlayingloading = false;
            gridLoading.Visibility = Visibility.Collapsed;
            GetEnableprograme();
            getCheckversion();
            GetVersion();
            //string data = "UPDATE historysetup SET versionprogram = '" + versiondevice + "',status = 'อัพเดต',dateupdate = "+ "NOW()" + " WHERE sn = '" + SerialNumber + "' AND programinstall = 'MDR' AND hcode = '" + HI7.Class.HIUility._HCODE + "'";
            //UpdateData(data);            
            MessageBox.Show("อัพเดตสำเร็จ!!!");
        }
        //HICM MAX 
        private void DownloadWorker_RunWorkerCompletedHICM(object sender, RunWorkerCompletedEventArgs e)
        {
            Application.Current.Dispatcher.Invoke(() =>
            {
                packIconCard.Kind = MaterialDesignThemes.Wpf.PackIconKind.AttachMoney;
            });
            isStoryboardPlaying = false;
            isStoryboardPlayingloading = false;
            gridLoading.Visibility = Visibility.Collapsed;
            GetEnableprograme();
            getCheckversionhicm();
            GetVersionhicm();
            
            //string data = "UPDATE historysetup SET versionprogram = '" + versiondevice + "',status = 'Update',dateupdate = " + "NOW()" + " WHERE sn = '" + SerialNumber + "' AND programinstall = 'URS' AND hcode = '" + HI7.Class.HIUility._HCODE + "'";
            //UpdateData(data);
            MessageBox.Show("อัพเดตสำเร็จ!!!");
        }
        //HICM ECLAIM API KHO 
        private void DownloadWorker_RunWorkerCompletedHICMclaimapi(object sender, RunWorkerCompletedEventArgs e)
        {
            Application.Current.Dispatcher.Invoke(() =>
            {
                packIconCard.Kind = MaterialDesignThemes.Wpf.PackIconKind.Api;
            });
            isStoryboardPlaying = false;
            isStoryboardPlayingloading = false;
            gridLoading.Visibility = Visibility.Collapsed;
            GetEnableprograme();
            getCheckversionhicmeclaim();
            GetVersioneclaimapi();
            //string data = "UPDATE historysetup SET versionprogram = '" + versiondevice + "',status = 'Update',dateupdate = " + "NOW()" + " WHERE sn = '" + SerialNumber + "' AND programinstall = 'URS' AND hcode = '" + HI7.Class.HIUility._HCODE + "'";
            //UpdateData(data);
            MessageBox.Show("อัพเดตสำเร็จ!!!");
        }
        //HI7 MAIN 
        private void DownloadWorker_RunWorkerCompletedHi7main(object sender, RunWorkerCompletedEventArgs e)
        {
            Application.Current.Dispatcher.Invoke(() =>
            {
                txtblockStatus.Text = " กระบวนการอัพเดตสำเร็จ!!";
                txtblockStatus.Visibility = Visibility.Visible;
            });
            isStoryboardPlaying = false;     
            isStoryboardPlayingloading = false;
            gridLoading.Visibility = Visibility.Collapsed;
        
            //string data = "UPDATE historysetup SET versionprogram = '" + versiondevice + "',status = 'Update',dateupdate = " + "NOW()" + " WHERE sn = '" + SerialNumber + "' AND programinstall = 'URS' AND hcode = '" + HI7.Class.HIUility._HCODE + "'";
            //UpdateData(data);
            MessageBox.Show("อัพเดตสำเร็จ กรุณาเปิดโปรแกรมใหม่อีกครั้ง!!!");

            Application.Current.Shutdown(); // ปิดแอปพลิเคชันปัจจุบัน
        }
        
        //HICM INCOME REPORT 
        private void DownloadWorker_RunWorkerCompletedHICMIncomereport(object sender, RunWorkerCompletedEventArgs e)
        {
            Application.Current.Dispatcher.Invoke(() =>
            {
                packIconCard.Kind = MaterialDesignThemes.Wpf.PackIconKind.CheckboxMarked;
            });
            isStoryboardPlaying = false;
            isStoryboardPlayingloading = false;
            gridLoading.Visibility = Visibility.Collapsed;
            GetEnableprograme();
            getCheckversionhicmincome();
            GetVersioneCheckHicm();
            //string data = "UPDATE historysetup SET versionprogram = '" + versiondevice + "',status = 'Update',dateupdate = " + "NOW()" + " WHERE sn = '" + SerialNumber + "' AND programinstall = 'URS' AND hcode = '" + HI7.Class.HIUility._HCODE + "'";
            //UpdateData(data);
            MessageBox.Show("อัพเดตสำเร็จ!!!");
        }
        //MDR
        private void DownloadWorker_RunWorkerCompletedinstall(object sender, RunWorkerCompletedEventArgs e)
        {
            Application.Current.Dispatcher.Invoke(() =>
            {
                packIconCard.Kind = MaterialDesignThemes.Wpf.PackIconKind.CardAccountDetails;
            });
            isStoryboardPlaying = false;
            isStoryboardPlayingloading = false;
            gridLoading.Visibility = Visibility.Collapsed;
            if (Directory.Exists(destinationFolderPath))
            {
                btnMdrinstall.Visibility = Visibility.Collapsed;
                nameMdr.Text = "โปรแกรมเวชระเบียน(พร้อมใช้งาน)";
                //GetHospitalname();
                getCheckversion();
                GetEnableprograme();
                //DataTable dt = SelectData(HI7.Class.HIUility._HCODE, SerialNumber);
                //if (dt.Rows.Count == 0)
                //{
                //    string data = "'" + HI7.Class.HIUility._HCODE + "','" + Brand + "','" + Computername + "','" + SerialNumber + "','" + Caption + "','" + SystemType + "','" + BuildNumber + "'," + "'MDR','" + versiondevice + "'," + "'install'," + "NOW()";
                //    InsertData(data);
                //}
                //else
                //{

                //}                
                MessageBox.Show("ติดตั้งสำเร็จ!!!");
            }
            else
            {               
            }    
        }
        //REP STATEMENT
        
            private void DownloadWorker_RunWorkerCompletedinstallRepStatement(object sender, RunWorkerCompletedEventArgs e)
        {
            Application.Current.Dispatcher.Invoke(() =>
            {
                packIconCard.Kind = MaterialDesignThemes.Wpf.PackIconKind.CardAccountDetails;
            });
            isStoryboardPlaying = false;
            isStoryboardPlayingloading = false;
            gridLoading.Visibility = Visibility.Collapsed;
            if (Directory.Exists(destinationFolderPathhicm))
            {
                btnMdrinstall.Visibility = Visibility.Collapsed;
                GetEnableprograme();
                //nameMdr.Text = "Rep/Statement(พร้อมใช้งาน)";
                MessageBox.Show("ตรวจสอบเวอร์ชั่นสำเร็จ!!!");
                GetVersionhicm();
                //try { GetHospitalname(); } catch { }
                //try { getCheckversion(); } catch { }
                //try {
                //    DataTable dt = SelectData(HI7.Class.HIUility._HCODE, SerialNumber);
                //    if (dt.Rows.Count == 0)
                //    {
                //        string data = "'" + HI7.Class.HIUility._HCODE + "','" + Brand + "','" + Computername + "','" + SerialNumber + "','" + Caption + "','" + SystemType + "','" + BuildNumber + "'," + "'Rep/Statement','" + versiondevice + "'," + "'install'," + "NOW()";
                //        InsertData(data);
                //    }
                //    else
                //    {

                //    }
                //    MessageBox.Show("ติดตั้งสำเร็จ!!!");
                //    //btncardprogram.IsEnabled = true;
                //}
                //catch
                //{

                //}

            }
            else
            {
            }
        }
        //ECHAIM API 
        private void DownloadWorker_RunWorkerCompletedinstallEclaimapi(object sender, RunWorkerCompletedEventArgs e)
        {
            Application.Current.Dispatcher.Invoke(() =>
            {
                packIconCard.Kind = MaterialDesignThemes.Wpf.PackIconKind.Api;
            });
            isStoryboardPlaying = false;
            isStoryboardPlayingloading = false;
            gridLoading.Visibility = Visibility.Collapsed;
            if (Directory.Exists(destinationFolderPathEclaim))
            {
                btnMdrinstall.Visibility = Visibility.Collapsed;
                nameMdr.Text = "Rep/Statement(พร้อมใช้งาน)";
                try { GetHospitalname(); } catch { }
                try { getCheckversionhicmeclaim(); } catch { }
                GetEnableprograme();
                //nameMdr.Text = "Rep/Statement(พร้อมใช้งาน)";
                MessageBox.Show("ตรวจสอบเวอร์ชั่นสำเร็จ!!!");
                GetVersionhicm();
                //try
                //{
                //    DataTable dt = SelectData(HI7.Class.HIUility._HCODE, SerialNumber);
                //    if (dt.Rows.Count == 0)
                //    {
                //        string data = "'" + HI7.Class.HIUility._HCODE + "','" + Brand + "','" + Computername + "','" + SerialNumber + "','" + Caption + "','" + SystemType + "','" + BuildNumber + "'," + "'Eclaim Api','" + versiondevice + "'," + "'install'," + "NOW()";
                //        InsertData(data);
                //    }
                //    else
                //    {

                //    }
                //    MessageBox.Show("ติดตั้งสำเร็จ!!!");
                //    //btncardprogram.IsEnabled = true;
                //}
                //catch
                //{

                //}

            }
            else
            {
            }
        }
        //Income report  
        private void DownloadWorker_RunWorkerCompletedinstallIncomereport(object sender, RunWorkerCompletedEventArgs e)
        {
            Application.Current.Dispatcher.Invoke(() =>
            {
                packIconCard.Kind = MaterialDesignThemes.Wpf.PackIconKind.CheckboxMarked;
            });
            isStoryboardPlaying = false;
            isStoryboardPlayingloading = false;
            gridLoading.Visibility = Visibility.Collapsed;
            if (Directory.Exists(destinationFolderPathHicmreport))
            {
                btnMdrinstall.Visibility = Visibility.Collapsed;
                nameMdr.Text = "Income Report(พร้อมใช้งาน)";
                try { GetHospitalname(); } catch { }
                try { getCheckversionhicmincome(); } catch { }
                GetEnableprograme();
                //nameMdr.Text = "Rep/Statement(พร้อมใช้งาน)";
                MessageBox.Show("ตรวจสอบเวอร์ชั่นสำเร็จ!!!");
                GetVersionhicm();
                //try
                //{
                //    DataTable dt = SelectData(HI7.Class.HIUility._HCODE, SerialNumber);
                //    if (dt.Rows.Count == 0)
                //    {
                //        string data = "'" + HI7.Class.HIUility._HCODE + "','" + Brand + "','" + Computername + "','" + SerialNumber + "','" + Caption + "','" + SystemType + "','" + BuildNumber + "'," + "'Eclaim Api','" + versiondevice + "'," + "'install'," + "NOW()";
                //        InsertData(data);
                //    }
                //    else
                //    {

                //    }
                //    MessageBox.Show("ติดตั้งสำเร็จ!!!");
                //    //btncardprogram.IsEnabled = true;
                //}
                //catch
                //{

                //}

            }
            else
            {
            }
        }
        //MDR CARD
        private void DownloadWorker_RunWorkerCompletedCheck(object sender, RunWorkerCompletedEventArgs e)
        {
            Application.Current.Dispatcher.Invoke(() =>
            {
                packIconCard.Kind = MaterialDesignThemes.Wpf.PackIconKind.CardAccountDetails;
            });
            isStoryboardPlaying = false;
            isStoryboardPlayingloading = false;
            gridLoading.Visibility = Visibility.Collapsed;
            btnMdrinstall.Visibility = Visibility.Visible;
            btnMdrrevert.Visibility = Visibility.Collapsed;
            btnMdrupdate.Visibility = Visibility.Collapsed;
            GetEnableprograme();
            getCheckversionhicmincome();
            //Application.Current.Dispatcher.Invoke(() =>
            //{
            //    packIconCard.Kind = MaterialDesignThemes.Wpf.PackIconKind.CardAccountDetails;
            //});
            //isStoryboardPlaying = false;
            //isStoryboardPlayingloading = false;
            //gridLoading.Visibility = Visibility.Collapsed;
            //if (Directory.Exists(destinationFolderPath))
            //{
            //    btnMdrinstall.Visibility = Visibility.Collapsed;
            //    nameMdr.Text = "โปรแกรมเวชระเบียน(พร้อมใช้งาน)";
            //    try { GetHospitalname(); } catch { }
            //    try { getCheckversion(); } catch { }
            //    GetEnableprograme();
            //    //nameMdr.Text = "Rep/Statement(พร้อมใช้งาน)";
            //    MessageBox.Show("ตรวจสอบเวอร์ชั่นสำเร็จ!!!");
            //    //GetVersion();
            //    getCheckversion();
            //    //try
            //    //{
            //    //    DataTable dt = SelectData(HI7.Class.HIUility._HCODE, SerialNumber);
            //    //    if (dt.Rows.Count == 0)
            //    //    {
            //    //        string data = "'" + HI7.Class.HIUility._HCODE + "','" + Brand + "','" + Computername + "','" + SerialNumber + "','" + Caption + "','" + SystemType + "','" + BuildNumber + "'," + "'Eclaim Api','" + versiondevice + "'," + "'install'," + "NOW()";
            //    //        InsertData(data);
            //    //    }
            //    //    else
            //    //    {

            //    //    }
            //    //    MessageBox.Show("ติดตั้งสำเร็จ!!!");
            //    //    //btncardprogram.IsEnabled = true;
            //    //}
            //    //catch
            //    //{

            //    //}

            //}
            //else
            //{
            //}

        }
        // HICM UPLOAD/STATEMENT DownloadWorker_RunWorkerCompletedCheckhicmclaim
        private void DownloadWorker_RunWorkerCompletedCheckhicm(object sender, RunWorkerCompletedEventArgs e)
        {
            Application.Current.Dispatcher.Invoke(() =>
            {
                packIconCard.Kind = MaterialDesignThemes.Wpf.PackIconKind.AttachMoney;
            });
            isStoryboardPlaying = false;
            isStoryboardPlayingloading = false;
            gridLoading.Visibility = Visibility.Collapsed;
            btnMdrinstall.Visibility = Visibility.Visible;
            btnMdrrevert.Visibility = Visibility.Collapsed;
            btnMdrupdate.Visibility = Visibility.Collapsed;
            btnMdrSearchupdate.Visibility = Visibility.Visible;
            GetEnableprograme();
            getCheckversionhicm();

        }
        // HICM Eclaim api 
        private void DownloadWorker_RunWorkerCompletedCheckhicmclaim(object sender, RunWorkerCompletedEventArgs e)
        {
            Application.Current.Dispatcher.Invoke(() =>
            {
                packIconCard.Kind = MaterialDesignThemes.Wpf.PackIconKind.Api;
            });
            isStoryboardPlaying = false;
            isStoryboardPlayingloading = false;
            gridLoading.Visibility = Visibility.Collapsed;
            btnMdrinstall.Visibility = Visibility.Visible;
            btnMdrrevert.Visibility = Visibility.Collapsed;
            btnMdrupdate.Visibility = Visibility.Collapsed;
            GetEnableprograme();
            getCheckversionhicmeclaim();

        }
        //HICM HICM INCOME 
        private void DownloadWorker_RunWorkerCompletedCheckhicmIncome(object sender, RunWorkerCompletedEventArgs e)
        {
            Application.Current.Dispatcher.Invoke(() =>
            {
                packIconCard.Kind = MaterialDesignThemes.Wpf.PackIconKind.CheckboxMarked;
            });
            isStoryboardPlaying = false;
            isStoryboardPlayingloading = false;
            gridLoading.Visibility = Visibility.Collapsed;
            btnMdrinstall.Visibility = Visibility.Visible;
            btnMdrrevert.Visibility = Visibility.Collapsed;
            btnMdrupdate.Visibility = Visibility.Collapsed;
            GetEnableprograme();
            getCheckversionhicmincome();

        }
        //Hi7 Main 
        private void DownloadWorker_RunWorkerCompletedCheckHi7main(object sender, RunWorkerCompletedEventArgs e)
        {
            Application.Current.Dispatcher.Invoke(() =>
            {
                txtblockStatus.Visibility = Visibility.Collapsed;
                btncard.IsEnabled = true;
                btnEclaimapi.IsEnabled = true;
                btnUploadstatement.IsEnabled = true;
                btnCheckHicm.IsEnabled = true;
                btnSearchMain.IsEnabled = true;
            });            
            GetVersionHi7main();
            //getCheckversionhicmincome();

        }
        private void Storyboard_Completed(object sender, EventArgs e)
        {
            if (isStoryboardPlaying)
            {
                if (storyboard != null)
                {
                    storyboard.Stop();
                    storyboard.Begin(packIconCard);
                }
            }
        }
        //storyboarding
        private void Storyboardlooding_Completed(object sender, EventArgs e)
        {
            if (isStoryboardPlayingloading)
            {
                if (storyboarding != null)
                {
                    storyboarding.Stop();
                    storyboarding.Begin();
                }
            }
        }

        //MDR 
        public void DownloadFilesFromGitRepositoryOnlyFileinstall()
        {
            string tempFolderPath = "C:\\Hi7 Program\\Hi7Temp";
            if (Directory.Exists(tempFolderPath))
            {
                var filestemp = Directory.GetFiles(tempFolderPath, "*", SearchOption.AllDirectories);
                foreach (var file in filestemp)
                {
                    var fileInfo = new FileInfo(file);
                    if (fileInfo.Directory.Name != "net6.0-windows")
                    {
                        fileInfo.Attributes = FileAttributes.Normal;
                        fileInfo.Delete();
                    }
                }
                Directory.Delete(tempFolderPath, true);
                if (Directory.Exists(destinationFolderPathcheckversion))
                {
                    Directory.Delete(destinationFolderPathcheckversion, true);
                }
                try { Repository.Clone(repositoryUrl, tempFolderPath); } catch { return; }
            }
            else
            {
                try { Repository.Clone(repositoryUrl, tempFolderPath); } catch { return; }
            }
            string net60WindowsFolderPath = "C:\\Hi7 Program\\Hi7Temp\\Mdr\\bin\\Debug\\net6.0-windows";
            //สร้างโฟร์เดอร์หลัก Master
            Directory.CreateDirectory(destinationFolderPath);
            // Move the 'net6.0-windows' folder to the desired destination   
            Directory.Move(net60WindowsFolderPath, Path.Combine(destinationFolderPath, "net6.0-windows"));
            // Delete the temporary folder
            var files = Directory.GetFiles(tempFolderPath, "*", SearchOption.AllDirectories);
            foreach (var file in files)
            {
                var fileInfo = new FileInfo(file);
                if (fileInfo.Directory.Name != "net6.0-windows")
                {
                    fileInfo.Attributes = FileAttributes.Normal;
                    fileInfo.Delete();
                }
            }
            Directory.Delete(tempFolderPath, true);
        }
        //RepStatement
        public void DownloadFilesFromGitRepositoryOnlyFileinstallRepStatement()
        {
            string tempFolderPath = "C:\\Hicm Program\\HicmTemp";
            if (Directory.Exists(tempFolderPath))
            {
                var filestemp = Directory.GetFiles(tempFolderPath, "*", SearchOption.AllDirectories);
                foreach (var file in filestemp)
                {
                    var fileInfo = new FileInfo(file);
                    if (fileInfo.Directory.Name != "Debug")
                    {
                        fileInfo.Attributes = FileAttributes.Normal;
                        fileInfo.Delete();
                    }
                }
                Directory.Delete(tempFolderPath, true);

                if (Directory.Exists(destinationFolderPathcheckversionhicm))
                {
                    Directory.Delete(destinationFolderPathcheckversionhicm, true);
                }
                try { Repository.Clone(repositoryUrlhicm, tempFolderPath); } catch { return; }
            }
            else
            {
                try { Repository.Clone(repositoryUrlhicm, tempFolderPath); } catch { return; }
            }
            string net60WindowsFolderPath = "C:\\Hicm Program\\HicmTemp\\Debug";
            //สร้างโฟร์เดอร์หลัก Master
            if (Directory.Exists(destinationFolderPathhicm))
            {
                Directory.Delete(destinationFolderPathhicm, true);
                Directory.CreateDirectory(destinationFolderPathhicm);
            }
            else
            {
                Directory.CreateDirectory(destinationFolderPathhicm);
            }                
            // Move the 'net6.0-windows' folder to the desired destination   
            Directory.Move(net60WindowsFolderPath, Path.Combine(destinationFolderPathhicm, "Debug"));
            // Delete the temporary folder
            var files = Directory.GetFiles(tempFolderPath, "*", SearchOption.AllDirectories);
            foreach (var file in files)
            {
                var fileInfo = new FileInfo(file);
                if (fileInfo.Directory.Name != "Debug")
                {
                    fileInfo.Attributes = FileAttributes.Normal;
                    fileInfo.Delete();
                }
            }
            Directory.Delete(tempFolderPath, true);
        }
        //HICM Chaim api 
        public void DownloadFilesFromGitRepositoryOnlyFileinstallEclaimapi()
        {            
            string tempFolderPath = "C:\\Apieclaim Program\\ApieclaimTemp";
            if (Directory.Exists(tempFolderPath))
            {
                Directory.Delete(tempFolderPath, true);
                if (Directory.Exists(destinationFolderPathcheckversionEclaim))
                {
                    Directory.Delete(destinationFolderPathcheckversionEclaim, true);
                }
                try { Repository.Clone(repositoryUrlEclaim, tempFolderPath); } catch { return; }
            }
            else
            {
                try { Repository.Clone(repositoryUrlEclaim, tempFolderPath); } catch { return; }
            }
            string net60WindowsFolderPath = "C:\\Apieclaim Program\\ApieclaimTemp\\Debug";
            //สร้างโฟร์เดอร์หลัก Master
            Directory.CreateDirectory(destinationFolderPathEclaim);
            // Move the 'net6.0-windows' folder to the desired destination   
            Directory.Move(net60WindowsFolderPath, Path.Combine(destinationFolderPathEclaim, "Debug"));
            // Delete the temporary folder
            var files = Directory.GetFiles(tempFolderPath, "*", SearchOption.AllDirectories);
            foreach (var file in files)
            {
                var fileInfo = new FileInfo(file);
                if (fileInfo.Directory.Name != "Debug")
                {
                    fileInfo.Attributes = FileAttributes.Normal;
                    fileInfo.Delete();
                }
            }
            Directory.Delete(tempFolderPath, true);
        }
        //HICM Income report DownloadFilesFromGitRepositoryOnlyFileinstallIncomereport
        public void DownloadFilesFromGitRepositoryOnlyFileinstallIncomereport()
        {
            string tempFolderPath = "C:\\Hicmreport Program\\HicmreportTemp";
            if (Directory.Exists(tempFolderPath))
            {
                Directory.Delete(tempFolderPath, true);
                if (Directory.Exists(destinationFolderPathcheckversionHicmreport))
                {
                    Directory.Delete(destinationFolderPathcheckversionHicmreport, true);
                }
                try { Repository.Clone(repositoryUrlHicmreport, tempFolderPath); } catch { return; }
            }
            else
            {
                try { Repository.Clone(repositoryUrlHicmreport, tempFolderPath); } catch { return; }
            }
            string net60WindowsFolderPath = "C:\\Hicmreport Program\\HicmreportTemp\\Debug";
            //สร้างโฟร์เดอร์หลัก Master
            Directory.CreateDirectory(destinationFolderPathHicmreport);
            // Move the 'net6.0-windows' folder to the desired destination   
            Directory.Move(net60WindowsFolderPath, Path.Combine(destinationFolderPathHicmreport, "Debug"));
            // Delete the temporary folder
            var files = Directory.GetFiles(tempFolderPath, "*", SearchOption.AllDirectories);
            foreach (var file in files)
            {
                var fileInfo = new FileInfo(file);
                if (fileInfo.Directory.Name != "Debug")
                {
                    fileInfo.Attributes = FileAttributes.Normal;
                    fileInfo.Delete();
                }
            }
            Directory.Delete(tempFolderPath, true);
        }
        //MDR
        public void DownloadFilesFromGitRepositoryOnlyFile()
        {
            string tempFolderPath = "C:\\Hi7 Program\\Hi7Temp";
            if (Directory.Exists(tempFolderPath))
            {
                var filestemp = Directory.GetFiles(tempFolderPath, "*", SearchOption.AllDirectories);
                foreach (var file in filestemp)
                {
                    var fileInfo = new FileInfo(file);
                    if (fileInfo.Directory.Name != "Debug")
                    {
                        fileInfo.Attributes = FileAttributes.Normal;
                        fileInfo.Delete();
                    }
                }
                Directory.Delete(tempFolderPath, true);
                if (Directory.Exists(destinationFolderPathcheckversion))
                {
                    Directory.Delete(destinationFolderPathcheckversion, true);
                }
                try { Repository.Clone(repositoryUrl, tempFolderPath); } catch { return; }
            }
            else
            {
                try { Repository.Clone(repositoryUrl, tempFolderPath); } catch { return; }
            }
            string net60WindowsFolderPath = "C:\\Hi7 Program\\Hi7Temp\\Mdr\\bin\\Debug\\net6.0-windows";
            string sourceFilePath = Path.Combine(net60WindowsFolderPath, "mdrversion.txt");
            string destinationFilePath = Path.Combine(destinationFolderPathcheckversion, "mdrversion.txt");

            if (Directory.Exists(destinationFolderPathcheckversion))
            {
                Directory.Delete(destinationFolderPathcheckversion, true);
                Directory.CreateDirectory(destinationFolderPathcheckversion);
            }
            else
            {
                Directory.CreateDirectory(destinationFolderPathcheckversion);
            }
                File.Copy(sourceFilePath, destinationFilePath);
            string masterpathtranfer = "C:\\Hi7 Program\\Hi7Master\\net6.0-windows";
            if (Directory.Exists(destinationFolderPath))
            {
                if (Directory.Exists(oldprograme))
                {
                    Directory.Delete(oldprograme, true);
                    Directory.CreateDirectory(oldprograme);
                }
                else
                {
                    Directory.CreateDirectory(oldprograme);
                }
                Directory.Move(masterpathtranfer, Path.Combine(oldprograme, "net6.0-windows"));
            }
            else
            {
            }  
            Directory.Move(net60WindowsFolderPath, Path.Combine(destinationFolderPath, "net6.0-windows"));
            var files = Directory.GetFiles(tempFolderPath, "*", SearchOption.AllDirectories);
            foreach (var file in files)
            {
                var fileInfo = new FileInfo(file);
                if (fileInfo.Directory.Name != "net6.0-windows")
                {
                    fileInfo.Attributes = FileAttributes.Normal;
                    fileInfo.Delete();
                }
            }
            Directory.Delete(tempFolderPath, true);
            
        }
        //HICM MAX 
        public void DownloadFilesFromGitRepositoryOnlyFileHICM()
        {
            string tempFolderPath = "C:\\Hicm Program\\HicmTemp";
            if (Directory.Exists(tempFolderPath))
            {
                Directory.Delete(tempFolderPath, true);
                if (Directory.Exists(destinationFolderPathcheckversionhicm))
                {
                    Directory.Delete(destinationFolderPathcheckversionhicm, true);
                }
                try { Repository.Clone(repositoryUrlhicm, tempFolderPath); } catch { return; }
            }
            else
            {
                try { Repository.Clone(repositoryUrlhicm, tempFolderPath); } catch { return; }
            }
            string net60WindowsFolderPath = "C:\\Hicm Program\\HicmTemp\\Debug";
            string sourceFilePath = Path.Combine(net60WindowsFolderPath, "mdrversion.txt");
            string destinationFilePath = Path.Combine(destinationFolderPathcheckversionhicm, "mdrversion.txt");

            if (Directory.Exists(destinationFolderPathcheckversionhicm))
            {
                Directory.Delete(destinationFolderPathcheckversionhicm, true);
                Directory.CreateDirectory(destinationFolderPathcheckversionhicm);
            }
            else
            {
                Directory.CreateDirectory(destinationFolderPathcheckversionhicm);
            }
            File.Copy(sourceFilePath, destinationFilePath);
            string masterpathtranfer = "C:\\Hicm Program\\HicmMaster\\Debug";
            if (Directory.Exists(destinationFolderPathhicm))
            {
                if (Directory.Exists(oldprogramehicm))
                {
                    Directory.Delete(oldprogramehicm, true);
                    Directory.CreateDirectory(oldprogramehicm);
                }
                else
                {
                    Directory.CreateDirectory(oldprogramehicm);
                }
                Directory.Move(masterpathtranfer, Path.Combine(oldprogramehicm, "Debug"));
            }
            else
            {
            }
            Directory.Move(net60WindowsFolderPath, Path.Combine(destinationFolderPathhicm, "Debug"));
            var files = Directory.GetFiles(tempFolderPath, "*", SearchOption.AllDirectories);
            foreach (var file in files)
            {
                var fileInfo = new FileInfo(file);
                if (fileInfo.Directory.Name != "Debug")
                {
                    fileInfo.Attributes = FileAttributes.Normal;
                    fileInfo.Delete();
                }
            }
            Directory.Delete(tempFolderPath, true);

        }
        //HICM ECLAIM API KHO 
        public void DownloadFilesFromGitRepositoryOnlyFileHICMclaimapi()
        {
            string tempFolderPath = "C:\\Apieclaim Program\\ApieclaimTemp";
            if (Directory.Exists(tempFolderPath))
            {
                var filetemp = Directory.GetFiles(tempFolderPath, "*", SearchOption.AllDirectories);
                foreach (var file in filetemp)
                {
                    var fileInfo = new FileInfo(file);
                    if (fileInfo.Directory.Name != "Debug")
                    {
                        fileInfo.Attributes = FileAttributes.Normal;
                        fileInfo.Delete();
                    }
                }
                Directory.Delete(tempFolderPath, true);

                if (Directory.Exists(destinationFolderPathcheckversionEclaim))
                {
                    Directory.Delete(destinationFolderPathcheckversionEclaim, true);
                }
                try { Repository.Clone(repositoryUrlEclaim, tempFolderPath); } catch { return; }
            }
            else
            {
                try { Repository.Clone(repositoryUrlEclaim, tempFolderPath); } catch { return; }
            }
            string net60WindowsFolderPath = "C:\\Apieclaim Program\\ApieclaimTemp\\Debug";
            string sourceFilePath = Path.Combine(net60WindowsFolderPath, "hicmversion.txt");
            string destinationFilePath = Path.Combine(destinationFolderPathcheckversionEclaim, "hicmversion.txt");

            if (Directory.Exists(destinationFolderPathcheckversionEclaim))
            {
                Directory.Delete(destinationFolderPathcheckversionEclaim, true);
                Directory.CreateDirectory(destinationFolderPathcheckversionEclaim);
            }
            else
            {
                Directory.CreateDirectory(destinationFolderPathcheckversionEclaim);
            }
            File.Copy(sourceFilePath, destinationFilePath);
            string masterpathtranfer = "C:\\Apieclaim Program\\ApieclaimMaster\\Debug";
            if (Directory.Exists(destinationFolderPathEclaim))
            {
                if (Directory.Exists(oldprogrameEclaim))
                {
                    Directory.Delete(oldprogrameEclaim, true);
                    Directory.CreateDirectory(oldprogrameEclaim);
                }
                else
                {
                    Directory.CreateDirectory(oldprogrameEclaim);
                }
                Directory.Move(masterpathtranfer, Path.Combine(oldprogrameEclaim, "Debug"));
            }
            else
            {
            }
            Directory.Move(net60WindowsFolderPath, Path.Combine(destinationFolderPathEclaim, "Debug"));
            var files = Directory.GetFiles(tempFolderPath, "*", SearchOption.AllDirectories);
            foreach (var file in files)
            {
                var fileInfo = new FileInfo(file);
                if (fileInfo.Directory.Name != "Debug")
                {
                    fileInfo.Attributes = FileAttributes.Normal;
                    fileInfo.Delete();
                }
            }
            Directory.Delete(tempFolderPath, true);

        }
        //HI7 MAIN 
        public void DownloadFilesFromGitRepositoryOnlyFileHi7main()
        {
            string tempFolderPath = "C:\\Program Files (x86)\\Hi7 Ubon\\Hi7Temp";
            if (Directory.Exists(tempFolderPath))
            {
                Directory.Delete(tempFolderPath, true);
                if (Directory.Exists("C:\\Program Files (x86)\\Hi7 Ubon\\Hi7CheckVersion"))
                {
                    Directory.Delete("C:\\Program Files (x86)\\Hi7 Ubon\\Hi7CheckVersion", true);
                }
                try { Repository.Clone(repositoryUrlMain, tempFolderPath); } catch { return; }
            }
            else
            {
                try { Repository.Clone(repositoryUrlMain, tempFolderPath); } catch { return; }
            }
            string net60WindowsFolderPath = "C:\\Program Files (x86)\\Hi7 Ubon\\Hi7Temp\\net6.0-windows";
            string sourceFilePath = Path.Combine(net60WindowsFolderPath, "mdrversion.txt");
            string destinationFilePath = Path.Combine("C:\\Program Files (x86)\\Hi7 Ubon\\Hi7CheckVersion", "mdrversion.txt");

            if (Directory.Exists("C:\\Program Files (x86)\\Hi7 Ubon\\Hi7CheckVersion"))
            {
                Directory.Delete("C:\\Program Files (x86)\\Hi7 Ubon\\Hi7CheckVersion", true);
                Directory.CreateDirectory("C:\\Program Files (x86)\\Hi7 Ubon\\Hi7CheckVersion");
            }
            else
            {
                Directory.CreateDirectory("C:\\Program Files (x86)\\Hi7 Ubon\\Hi7CheckVersion");
            }
            File.Copy(sourceFilePath, destinationFilePath);
            string masterpathtranfer = "C:\\Program Files (x86)\\Hi7 Ubon\\Hi7Setup";
            if (Directory.Exists("C:\\Program Files (x86)\\Hi7 Ubon\\Hi7Setup"))
            {
                if (Directory.Exists("C:\\Program Files (x86)\\Hi7 Ubon\\Hi7Oldversion"))
                {
                    Directory.Delete("C:\\Program Files (x86)\\Hi7 Ubon\\Hi7Oldversion", true);
                    Directory.CreateDirectory("C:\\Program Files (x86)\\Hi7 Ubon\\Hi7Oldversion");
                }
                else
                {
                    Directory.CreateDirectory("C:\\Program Files (x86)\\Hi7 Ubon\\Hi7Oldversion");
                }
                //Directory.Move(masterpathtranfer, Path.Combine("C:\\Program Files (x86)\\Hi7 Ubon\\Hi7Oldversion"));
            }
            else
            {
            }
            try
            {
                string sourcePath = @"C:\Program Files (x86)\Hi7 Ubon\Hi7Setup";
                string targetPath = @"C:\Program Files (x86)\Hi7 Ubon\Hi7Oldversion";
                if (Directory.Exists(sourcePath))
                {
                    // Copy the directory and its contents to the target location
                    CopyDirectory(sourcePath, targetPath);
                }
                else if (File.Exists(sourcePath))
                {
                    // Copy the file to the target location
                    File.Copy(sourcePath, Path.Combine(targetPath, Path.GetFileName(sourcePath)), true);
                }
                else
                {
                    Console.WriteLine("Source file or folder not found.");
                }

                Console.WriteLine("Copy operation completed.");
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred: " + ex.Message);
            }
            //เอาไฟล์ จากtemp ไปไว้ใน ตัวหลัก
            try
            {
                string sourcePath = @"C:\Program Files (x86)\Hi7 Ubon\Hi7Temp\net6.0-windows";
                string targetPath = @"C:\Program Files (x86)\Hi7 Ubon\Hi7Setup";
                if (Directory.Exists(sourcePath))
                {
                    // Copy the directory and its contents to the target location
                    CopyDirectory(sourcePath, targetPath);
                }
                else if (File.Exists(sourcePath))
                {
                    // Copy the file to the target location
                    File.Copy(sourcePath, Path.Combine(targetPath, Path.GetFileName(sourcePath)), true);
                }
                else
                {
                    Console.WriteLine("Source file or folder not found.");
                }

                Console.WriteLine("Copy operation completed.");
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred: " + ex.Message);
            }
            //ลบไฟล์ออก temp
            var files = Directory.GetFiles(tempFolderPath, "*", SearchOption.AllDirectories);
            foreach (var file in files)
            {
                var fileInfo = new FileInfo(file);
                if (fileInfo.Directory.Name != "net6.0-windows")
                {
                    fileInfo.Attributes = FileAttributes.Normal;
                    fileInfo.Delete();
                }
            }
            Directory.Delete(tempFolderPath, true);

        }
        static void CopyDirectory(string sourceDir, string targetDir)
        {
            if (!Directory.Exists(targetDir))
            {
                Directory.CreateDirectory(targetDir);
            }

            foreach (string sourceSubDir in Directory.GetDirectories(sourceDir))
            {
                string targetSubDir = Path.Combine(targetDir, Path.GetFileName(sourceSubDir));
                CopyDirectory(sourceSubDir, targetSubDir);
            }

            foreach (string sourceFile in Directory.GetFiles(sourceDir))
            {
                File.Copy(sourceFile, Path.Combine(targetDir, Path.GetFileName(sourceFile)), true);
            }
        }

        //HICM INCOME REPORT 
        public void DownloadFilesFromGitRepositoryOnlyFileHICMIncomereport()
        {
            string tempFolderPath = "C:\\Hicmreport Program\\HicmreportTemp";
            if (Directory.Exists(tempFolderPath))
            {
                var filestemp = Directory.GetFiles(tempFolderPath, "*", SearchOption.AllDirectories);
                foreach (var file in filestemp)
                {
                    var fileInfo = new FileInfo(file);
                    if (fileInfo.Directory.Name != "Debug")
                    {
                        fileInfo.Attributes = FileAttributes.Normal;
                        fileInfo.Delete();
                    }
                }
                Directory.Delete(tempFolderPath, true);
                if (Directory.Exists(destinationFolderPathcheckversionHicmreport))
                {
                    Directory.Delete(destinationFolderPathcheckversionHicmreport, true);
                }
                try { Repository.Clone(repositoryUrlHicmreport, tempFolderPath); } catch { return; }
            }
            else
            {
                try { Repository.Clone(repositoryUrlHicmreport, tempFolderPath); } catch { return; }
            }
            string net60WindowsFolderPath = "C:\\Hicmreport Program\\HicmreportTemp\\Debug";
            string sourceFilePath = Path.Combine(net60WindowsFolderPath, "hicmreportversion.txt");
            string destinationFilePath = Path.Combine(destinationFolderPathcheckversionHicmreport, "hicmreportversion.txt");

            if (Directory.Exists(destinationFolderPathcheckversionHicmreport))
            {
                Directory.Delete(destinationFolderPathcheckversionHicmreport, true);
                Directory.CreateDirectory(destinationFolderPathcheckversionHicmreport);
            }
            else
            {
                Directory.CreateDirectory(destinationFolderPathcheckversionHicmreport);
            }
            File.Copy(sourceFilePath, destinationFilePath);
            string masterpathtranfer = "C:\\Hicmreport Program\\HicmreportMaster\\Debug";
            if (Directory.Exists(destinationFolderPathHicmreport))
            {
                if (Directory.Exists(oldprogrameHicmreport))
                {
                    Directory.Delete(oldprogrameHicmreport, true);
                    Directory.CreateDirectory(oldprogrameHicmreport);
                }
                else
                {
                    Directory.CreateDirectory(oldprogrameHicmreport);
                }
                Directory.Move(masterpathtranfer, Path.Combine(oldprogrameHicmreport, "Debug"));
            }
            else
            {
            }
            Directory.Move(net60WindowsFolderPath, Path.Combine(destinationFolderPathHicmreport, "Debug"));
            var files = Directory.GetFiles(tempFolderPath, "*", SearchOption.AllDirectories);
            foreach (var file in files)
            {
                var fileInfo = new FileInfo(file);
                if (fileInfo.Directory.Name != "Debug")
                {
                    fileInfo.Attributes = FileAttributes.Normal;
                    fileInfo.Delete();
                }
            }
            Directory.Delete(tempFolderPath, true);

        }
        //MDR        
        public void DownloadFilesFromGitRepositoryOnlyFileCheck()
        {
            string tempFolderPath = "C:\\Hi7 Program\\Hi7Temp";
            if (Directory.Exists(tempFolderPath))
            {
                var filestemp = Directory.GetFiles(tempFolderPath, "*", SearchOption.AllDirectories);
                foreach (var file in filestemp)
                {
                    var fileInfo = new FileInfo(file);
                    if (fileInfo.Name != "mdrversion.txt")
                    {
                        fileInfo.Attributes = FileAttributes.Normal;
                        fileInfo.Delete();
                    }
                }
                Directory.Delete(tempFolderPath, true);
                if (Directory.Exists(destinationFolderPathcheckversion))
                {
                    Directory.Delete(destinationFolderPathcheckversion, true);
                }
                try { Repository.Clone(repositoryUrl, tempFolderPath); } catch { return; }
            }
            else
            {
                try { Repository.Clone(repositoryUrl, tempFolderPath); } catch { return; }
            }
            var files = Directory.GetFiles(tempFolderPath, "*", SearchOption.AllDirectories);
            foreach (var file in files)
            {
                var fileInfo = new FileInfo(file);
                if (fileInfo.Name != "mdrversion.txt")
                {
                    fileInfo.Attributes = FileAttributes.Normal;
                    fileInfo.Delete();
                }
            }
            if (Directory.Exists(destinationFolderPathcheckversion))
            {
                Directory.Delete(destinationFolderPathcheckversion, true);
            }
            else
            {
                Directory.CreateDirectory(destinationFolderPathcheckversion);
            }
            Directory.CreateDirectory(destinationFolderPathcheckversion);
            string net60WindowsFolderPath = "C:\\Hi7 Program\\Hi7Temp\\Mdr\\bin\\Debug\\net6.0-windows";
            string sourceFilePath = Path.Combine(net60WindowsFolderPath, "mdrversion.txt");
            string destinationFilePath = Path.Combine(destinationFolderPathcheckversion, "mdrversion.txt");
            File.Move(sourceFilePath, destinationFilePath);
            Directory.Delete(tempFolderPath, true);
        }

        private void btnMdrSearchupdate_Click(object sender, RoutedEventArgs e)
        {
            btncardprogram.IsEnabled = false;
            nameMdr.Text = "กำลังตรวจสอบอัพเดต";
            GetDisableprogram();
            if (!isStoryboardPlaying)
            {
                if (useMDR == true)
                {
                    btnMdrSearchupdate.IsEnabled = false;
                    RunBackgroundTaskCheck();
                }
                else if (useRepStatement == true)
                {
                    btnMdrSearchupdate.IsEnabled = false;
                    RunBackgroundTaskCheckHICM();
                    
                }
                else if (useIncomereport == true)
                {
                    RunBackgroundTaskCheckHICMIncome();
                }
                else if (useEclaimAPI == true)
                {
                    RunBackgroundTaskCheckHICMeclaim();
                    
                }
                
                isStoryboardPlaying = true;
                isStoryboardPlayingloading = true;
            }
        }
        void GetEnableprograme()
        {
            btnMdrSearchupdate.IsEnabled = true;
            btnSearchMain.IsEnabled = true;
            btncard.IsEnabled = true;
            btnEclaimapi.IsEnabled = true;
            btnUploadstatement.IsEnabled = true;
            btnCheckHicm.IsEnabled = true;
            btnMdrupdate.IsEnabled = true;
        }

        private void btnEclaimapi_Click(object sender, RoutedEventArgs e)
        {
            btncard.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFFFFFFF"));
            btnUploadstatement.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFFFFFFF"));
            btnEclaimapi.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFC1BFFD"));
            btnCheckHicm.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFFFFFFF"));
            useMDR = false;
            useRepStatement = false;
            useEclaimAPI = true;
            useIncomereport = false;
            versionnew = string.Empty;
            versiondevice = string.Empty;
            versionrevert = string.Empty;
            readversionnew = string.Empty;
            readversiondevice = string.Empty;
            GetVersioneclaimapi();
            if (!string.IsNullOrEmpty(versionnew))
            {
                if (versiondevice != versionnew)//ต่างเวอร์ชั่นกัน
                {
                    if (!string.IsNullOrEmpty(versiondevice))
                    {
                        nameMdr.Text = "Eclaim API ส่ง16แฟ้ม(มีอัพเดต): " + versionnew;
                        ButtonUpdate();

                    }
                    else
                    {
                        nameMdr.Text = "Eclaim API ส่ง16แฟ้ม(ยังไม่ได้ติดตั้ง)";
                        ButtonNotinstall();
                    }


                }
                else if (versiondevice == versionnew)
                {
                    nameMdr.Text = "Eclaim API ส่ง16แฟ้ม(ล่าสุด)";
                    ButtonVersionsame();
                }
            }
            else
            {//ยังไม่ติดตั้ง
                nameMdr.Text = "Eclaim API ส่ง16แฟ้ม(ยังไม่ได้ติดตั้ง)";
                ButtonNotinstall();
            }
            nameProgram.Text = "โปรแกรม Eclaim API ส่ง16แฟ้ม ";
            detailProgram.Text = "โปรแกรม Eclaim API ส่ง16แฟ้ม เป็นโปรแกรมนำเข้าไฟล์ Statement และ Rep เพื่อตรวจสอบการเคลม";
            packIconCard.Kind = MaterialDesignThemes.Wpf.PackIconKind.Api;
            icon.Visibility = Visibility.Visible;
            detail.Visibility = Visibility.Visible;
        }

        //HICM statement/rep
        //public void DownloadFilesFromGitRepositoryOnlyFileCheckHICM()
        //{
        //    string tempFolderPath = "C:\\Hicm Program\\HicmTemp";
        //    if (Directory.Exists(tempFolderPath))
        //    {
        //        Directory.Delete(tempFolderPath, true);
        //        if (Directory.Exists(destinationFolderPathcheckversionhicm))
        //        {
        //            Directory.Delete(destinationFolderPathcheckversionhicm, true);
        //        }
        //        try { Repository.Clone(repositoryUrlhicm, tempFolderPath); } catch { return; }
        //    }
        //    else
        //    {
        //        try { Repository.Clone(repositoryUrlhicm, tempFolderPath); } catch { return; }
        //    }
        //    var files = Directory.GetFiles(tempFolderPath, "*", SearchOption.AllDirectories);
        //    foreach (var file in files)
        //    {
        //        var fileInfo = new FileInfo(file);
        //        if (fileInfo.Name != "mdrversion.txt")//หาไฟล์เวอร์ชั่น
        //        {
        //            fileInfo.Attributes = FileAttributes.Normal;
        //            fileInfo.Delete();
        //        }
        //    }
        //    if (Directory.Exists(destinationFolderPathcheckversionhicm))
        //    {
        //        Directory.Delete(destinationFolderPathcheckversionhicm, true);
        //    }
        //    else
        //    {
        //        Directory.CreateDirectory(destinationFolderPathcheckversionhicm);
        //    }
        //    Directory.CreateDirectory(destinationFolderPathcheckversionhicm); 
        ////C:\Hicm Program\HicmTemp\Debug
        //    string net60WindowsFolderPath = "C:\\Hicm Program\\HicmTemp\\Debug";
        //    string sourceFilePath = Path.Combine(net60WindowsFolderPath, "mdrversion.txt");
        //    string destinationFilePath = Path.Combine(destinationFolderPathcheckversionhicm, "mdrversion.txt");
        //    File.Move(sourceFilePath, destinationFilePath);
        //    Directory.Delete(tempFolderPath, true);
        //}
        public void DownloadFilesFromGitRepositoryOnlyFileCheckHICM()
        {
            string tempFolderPath = "C:\\Hicm Program\\HicmTemp";
            if (Directory.Exists(tempFolderPath))
            {
                Directory.Delete(tempFolderPath, true);
                if (Directory.Exists(destinationFolderPathcheckversionhicm))
                {
                    Directory.Delete(destinationFolderPathcheckversionhicm, true);
                }
                try { Repository.Clone(repositoryUrlhicm, tempFolderPath); } catch { return; }
            }
            else
            {
                try { Repository.Clone(repositoryUrlhicm, tempFolderPath); } catch { return; }
            }
            var files = Directory.GetFiles(tempFolderPath, "*", SearchOption.AllDirectories);
            foreach (var file in files)
            {
                var fileInfo = new FileInfo(file);
                if (fileInfo.Name != "mdrversion.txt")//หาไฟล์เวอร์ชั่น
                {
                    fileInfo.Attributes = FileAttributes.Normal;
                    fileInfo.Delete();
                }
            }
            if (Directory.Exists(destinationFolderPathcheckversionhicm))
            {
                Directory.Delete(destinationFolderPathcheckversionhicm, true);
            }
            else
            {
                Directory.CreateDirectory(destinationFolderPathcheckversionhicm);
            }
            Directory.CreateDirectory(destinationFolderPathcheckversionhicm);
            //C:\Hicm Program\HicmTemp\Debug
            string net60WindowsFolderPath = "C:\\Hicm Program\\HicmTemp\\Debug";
            string sourceFilePath = Path.Combine(net60WindowsFolderPath, "mdrversion.txt");
            string destinationFilePath = Path.Combine(destinationFolderPathcheckversionhicm, "mdrversion.txt");
            File.Move(sourceFilePath, destinationFilePath);
            Directory.Delete(tempFolderPath, true);
        }
        //HICM Eclaim API 
        public void DownloadFilesFromGitRepositoryOnlyFileCheckHICMclaim()
        {
            string tempFolderPath = "C:\\Apieclaim Program\\ApieclaimTemp"; 
            if (Directory.Exists(tempFolderPath))
            {
                Directory.Delete(tempFolderPath, true);
                if (Directory.Exists(destinationFolderPathcheckversionEclaim))
                {
                    Directory.Delete(destinationFolderPathcheckversionEclaim, true);
                }
                try { Repository.Clone(repositoryUrlEclaim, tempFolderPath); } catch { return; }
            }
            else
            {
                try { Repository.Clone(repositoryUrlEclaim, tempFolderPath); } catch { return; }
            }
            var files = Directory.GetFiles(tempFolderPath, "*", SearchOption.AllDirectories);
            foreach (var file in files)
            {
                var fileInfo = new FileInfo(file);
                if (fileInfo.Name != "hicmversion.txt")//หาไฟล์เวอร์ชั่น
                {
                    fileInfo.Attributes = FileAttributes.Normal;
                    fileInfo.Delete();
                }
            }
            if (Directory.Exists(destinationFolderPathcheckversionEclaim))
            {
                Directory.Delete(destinationFolderPathcheckversionEclaim, true);
            }
            else
            {
                Directory.CreateDirectory(destinationFolderPathcheckversionEclaim);
            }
            Directory.CreateDirectory(destinationFolderPathcheckversionEclaim);
            //C:\Hicm Program\HicmTemp\Debug
            string net60WindowsFolderPath = "C:\\Apieclaim Program\\ApieclaimTemp\\Debug";
            string sourceFilePath = Path.Combine(net60WindowsFolderPath, "hicmversion.txt");
            string destinationFilePath = Path.Combine(destinationFolderPathcheckversionEclaim, "hicmversion.txt");
            File.Move(sourceFilePath, destinationFilePath);
            Directory.Delete(tempFolderPath, true);
        }
        //HICM Income 
        public void DownloadFilesFromGitRepositoryOnlyFileCheckHICMIncome()
        {
            string tempFolderPath = "C:\\Hicmreport Program\\HicmreportTemp";
            if (Directory.Exists(tempFolderPath))
            {
                Directory.Delete(tempFolderPath, true);
                if (Directory.Exists(destinationFolderPathcheckversionHicmreport))
                {
                    Directory.Delete(destinationFolderPathcheckversionHicmreport, true);
                }
                try { Repository.Clone(repositoryUrlHicmreport, tempFolderPath); } catch { return; }
            }
            else
            {
                try { Repository.Clone(repositoryUrlHicmreport, tempFolderPath); } catch { return; }
            }
            var files = Directory.GetFiles(tempFolderPath, "*", SearchOption.AllDirectories);
            foreach (var file in files)
            {
                var fileInfo = new FileInfo(file);
                if (fileInfo.Name != "hicmreportversion.txt")//หาไฟล์เวอร์ชั่น
                {
                    fileInfo.Attributes = FileAttributes.Normal;
                    fileInfo.Delete();
                }
            }
            if (Directory.Exists(destinationFolderPathcheckversionHicmreport))
            {
                Directory.Delete(destinationFolderPathcheckversionHicmreport, true);
            }
            else
            {
                Directory.CreateDirectory(destinationFolderPathcheckversionHicmreport);
            }
            Directory.CreateDirectory(destinationFolderPathcheckversionHicmreport);
            //C:\Hicm Program\HicmTemp\Debug
            string net60WindowsFolderPath = "C:\\Hicmreport Program\\HicmreportTemp\\Debug";
            string sourceFilePath = Path.Combine(net60WindowsFolderPath, "hicmreportversion.txt");
            string destinationFilePath = Path.Combine(destinationFolderPathcheckversionHicmreport, "hicmreportversion.txt");
            File.Move(sourceFilePath, destinationFilePath);
            Directory.Delete(tempFolderPath, true);
        }
        //HI7 Main 
        public void DownloadFilesFromGitRepositoryOnlyFileCheckHi7main()
        {
            string tempFolderPath = "C:\\Program Files (x86)\\Hi7 Ubon\\Hi7Temp";
            if (Directory.Exists(tempFolderPath))
            {
                var filetemp = Directory.GetFiles(tempFolderPath, "*", SearchOption.AllDirectories);
                foreach (var file in filetemp)
                {
                    var fileInfo = new FileInfo(file);
                    if (fileInfo.Directory.Name != "Debug")
                    {
                        fileInfo.Attributes = FileAttributes.Normal;
                        fileInfo.Delete();
                    }
                }
                Directory.Delete(tempFolderPath, true);

                if (Directory.Exists(destinationFolderPathcheckversionMain))
                {
                    Directory.Delete(destinationFolderPathcheckversionMain, true);
                }
                try { Repository.Clone(repositoryUrlMain, tempFolderPath); } catch { return; }
            }
            else
            {
                try { Repository.Clone(repositoryUrlMain, tempFolderPath); } catch { return; }
            }
            var files = Directory.GetFiles(tempFolderPath, "*", SearchOption.AllDirectories);
            foreach (var file in files)
            {
                var fileInfo = new FileInfo(file);
                if (fileInfo.Name != "mdrversion.txt")//หาไฟล์เวอร์ชั่น
                {
                    fileInfo.Attributes = FileAttributes.Normal;
                    fileInfo.Delete();
                }
            }
            if (Directory.Exists(destinationFolderPathcheckversionMain))
            {
                Directory.Delete(destinationFolderPathcheckversionMain, true);
            }
            else
            {
                Directory.CreateDirectory("C:\\Program Files (x86)\\Hi7 Ubon\\Hi7CheckVersion");
            }
            Directory.CreateDirectory("C:\\Program Files (x86)\\Hi7 Ubon\\Hi7CheckVersion");
            //C:\Hicm Program\HicmTemp\Debug
            string net60WindowsFolderPath = "C:\\Program Files (x86)\\Hi7 Ubon\\Hi7Temp\\net6.0-windows";
            string sourceFilePath = Path.Combine(net60WindowsFolderPath, "mdrversion.txt");
            string destinationFilePath = Path.Combine("C:\\Program Files (x86)\\Hi7 Ubon\\Hi7CheckVersion", "mdrversion.txt");
            //File.Move(sourceFilePath, destinationFilePath);
            try
            {
                if (File.Exists(destinationFilePath))
                {
                    File.Delete(destinationFilePath);
                }

                File.Copy(sourceFilePath, destinationFilePath);
                File.Delete(sourceFilePath); // Optionally delete the source file if needed
            }
            catch (IOException ex)
            {
            }
            Directory.Delete(tempFolderPath, true);
        }
        public void Checkversion()
        {
            // ตัวแปรสำหรับเก็บเวอร์ชันหลัก
            string versionnew = string.Empty;
            string versiondevice = string.Empty;
            // ตำแหน่งของไฟล์ mdrversion.txt
            // ที่อยู่เวอร์ชั่นใหม่
            string filePathversionnew = "C:\\Hi7 Program\\Hi7CheckVersion\\mdrversion.txt";
            // ที่อยู่เวอร์ชั่นในเครื่อง
            string filePathversiondevice = "C:\\Hi7 Program\\Hi7Master\\net6.0-windows\\mdrversion.txt";
            // ตรวจสอบว่าไฟล์มีอยู่หรือไม่
            if (File.Exists(filePathversionnew))
            {
                // อ่านข้อมูลจากไฟล์
                string[] lines = File.ReadAllLines(filePathversionnew);
                // วนลูปตรวจสอบทุกบรรทัด
                foreach (string line in lines)
                {
                    // ตรวจสอบว่าบรรทัดนั้นเป็นบรรทัดที่มีเวอร์ชัน
                    if (line.StartsWith("Version:"))
                    {
                        // ดึงเฉพาะเวอร์ชันออกมา
                        versionnew = line.Replace("Version:", "").Trim();
                        break;
                    }
                }
            }
            if (File.Exists(filePathversiondevice))
            {
                // อ่านข้อมูลจากไฟล์
                string[] lines = File.ReadAllLines(filePathversiondevice);
                // วนลูปตรวจสอบทุกบรรทัด
                foreach (string line in lines)
                {
                    // ตรวจสอบว่าบรรทัดนั้นเป็นบรรทัดที่มีเวอร์ชัน
                    if (line.StartsWith("Version:"))
                    {
                        // ดึงเฉพาะเวอร์ชันออกมา
                        versiondevice = line.Replace("Version:", "").Trim();
                        break;
                    }
                }
            }
            // ตรวจสอบว่าเวอร์ชันหลักไม่ว่างเปล่า
            if (!string.IsNullOrEmpty(versionnew) && !string.IsNullOrEmpty(versiondevice))
            {
                //DownloadFilesFromGitRepository();
            }
            else
            {
                // ถ้าไม่พบเวอร์ชันหลักในไฟล์
                //MessageBoxResult result = MessageBox.Show("คุณยังไม่ได้ติดตั้งโปรแกรม?\r\n", "ตรวจสอบโปรแกรม", MessageBoxButton.YesNo, MessageBoxImage.Question);
                // ตรวจสอบผลลัพธ์ที่ได้
                //if (result == MessageBoxResult.Yes)
                //{
                    string tempFolderPath = "C:\\Hi7 Program\\Hi7Temp";
                    //Directory.CreateDirectory(destinationFolderPath);
                    //DirectoryInfo directoryInfo = new DirectoryInfo(destinationFolderPath);
                    ////ทำการ Everyone โฟร์เดอร์ทั้งหมด
                    //DirectorySecurity security = directoryInfo.GetAccessControl();
                    //security.AddAccessRule(new FileSystemAccessRule("Everyone", FileSystemRights.FullControl, InheritanceFlags.ContainerInherit | InheritanceFlags.ObjectInherit, PropagationFlags.None, AccessControlType.Allow));
                    //directoryInfo.SetAccessControl(security);
                    // Delete the temporary folder if it already exists
                    if (Directory.Exists(tempFolderPath))
                    {
                        Directory.Delete(tempFolderPath, true);
                        //Directory.Delete(destinationFolderPath, true);
                    }
                    // Clone the repository to the temporary folder
                    Repository.Clone(repositoryUrl, tempFolderPath);
                    // Delete all files except the 'net6.0-windows' folder
                    //var files = Directory.GetFiles(tempFolderPath, "*", SearchOption.AllDirectories);
                    //foreach (var file in files)
                    //{
                    //    var fileInfo = new FileInfo(file);
                    //    if (fileInfo.Directory.Name != "net6.0-windows")
                    //    {
                    //        fileInfo.Attributes = FileAttributes.Normal;
                    //        fileInfo.Delete();
                    //    }
                    //}
                //สร้างโฟร์เดอร์หลัก Master
                Directory.CreateDirectory(destinationFolderPath);
                // Move the 'net6.0-windows' folder to the desired destination   
                string net60WindowsFolderPath = "C:\\Hi7 Program\\Hi7Temp\\Mdr\\bin\\Debug\\net6.0-windows";
                    Directory.Move(net60WindowsFolderPath, Path.Combine(destinationFolderPath, "net6.0-windows"));
                // Delete the temporary folder
                //var files = Directory.GetFiles(tempFolderPath, "*", SearchOption.AllDirectories);
                //foreach (var file in files)
                //{
                //    var fileInfo = new FileInfo(file);
                //    if (fileInfo.Directory.Name != "net6.0-windows")
                //    {
                //        fileInfo.Attributes = FileAttributes.Normal;
                //        fileInfo.Delete();
                //    }
                //}
                Directory.Delete(tempFolderPath, true);
                    //MessageBox.Show("ติดตั้งโปรแกรมใหม่สำเร็จ");
                //}

            }
        }

        //private void btnUploadstatement_Click(object sender, RoutedEventArgs e)
        //{
        //    icon.Visibility = Visibility.Visible;
        //    detail.Visibility = Visibility.Visible;
        //    picslide.Visibility = Visibility.Visible;
        //    btncardprogram.IsEnabled = false;
        //    nameMdr.Text = "กำลังตรวจสอบอัพเดต";
        //    if (!isStoryboardPlaying)
        //    {
        //        RunBackgroundTaskCheckHICM();
        //        isStoryboardPlaying = true;
        //    }
        //}
        private void btnUploadstatement_Click(object sender, RoutedEventArgs e)
        {
            btncard.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFFFFFFF"));
            btnUploadstatement.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFC1BFFD"));
            btnEclaimapi.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFFFFFFF"));
            btnCheckHicm.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFFFFFFF"));
            useMDR = false;
            useRepStatement = true;
            useEclaimAPI = false;           
            useIncomereport = false;
            versionnew = string.Empty;
            versiondevice = string.Empty;
            versionrevert = string.Empty;
            readversionnew = string.Empty;
            readversiondevice = string.Empty;
            GetVersionhicm();
            if (!string.IsNullOrEmpty(versionnew))
            {
                if (versiondevice != versionnew)//ต่างเวอร์ชั่นกัน
                {
                    if (!string.IsNullOrEmpty(versiondevice))
                    {
                        nameMdr.Text = "Upload Rep/Statement(มีอัพเดต): " + versionnew;
                        ButtonUpdate();

                    }
                    else
                    {
                        nameMdr.Text = "Upload Rep/Statement(ยังไม่ได้ติดตั้ง)";
                        ButtonNotinstall();
                    }


                }
                else if (versiondevice == versionnew)
                {
                    nameMdr.Text = "Upload Rep/Statement(ล่าสุด)";
                    ButtonVersionsame();
                }
            }
            else
            {//ยังไม่ติดตั้ง
               
                nameMdr.Text = "Upload Rep/Statement(ยังไม่ได้ติดตั้ง)";
                ButtonNotinstall();

            }
            nameProgram.Text = "โปรแกรม Upload Rep/Statement ";
            detailProgram.Text = "โปรแกรม Upload Rep/Statement เป็นโปรแกรมนำเข้าไฟล์ Statement และ Rep เพื่อตรวจสอบการเคลม";
            packIconCard.Kind = MaterialDesignThemes.Wpf.PackIconKind.AttachMoney;
            icon.Visibility = Visibility.Visible;            
            detail.Visibility = Visibility.Visible;
        }

        public void DownloadFilesFromGitRepository()
        {
            string tempFolderPath = "C:\\Hi7 Program\\Hi7Temp";
            if (Directory.Exists(tempFolderPath))
            {
                Directory.Delete(tempFolderPath, true);
                Directory.Delete(destinationFolderPath, true);
            }
            Repository.Clone(repositoryUrl, tempFolderPath);
            var files = Directory.GetFiles(tempFolderPath, "*", SearchOption.AllDirectories);
            foreach (var file in files)
            {
                var fileInfo = new FileInfo(file);
                if (fileInfo.Directory.Name != "net6.0-windows")
                {
                    fileInfo.Attributes = FileAttributes.Normal;
                    fileInfo.Delete();
                }
            }
            //ลบ
            Directory.Delete(destinationFolderPath, true);
            Directory.CreateDirectory(destinationFolderPath);
            DirectoryInfo directoryInfo = new DirectoryInfo(destinationFolderPath);
            DirectorySecurity security = directoryInfo.GetAccessControl();
            security.AddAccessRule(new FileSystemAccessRule("Everyone", FileSystemRights.FullControl, InheritanceFlags.ContainerInherit | InheritanceFlags.ObjectInherit, PropagationFlags.None, AccessControlType.Allow));
            directoryInfo.SetAccessControl(security);
            string net60WindowsFolderPath = "C:\\Hi7 Program\\Hi7Temp\\Mdr\\bin\\Debug\\net6.0-windows";
            Directory.Move(net60WindowsFolderPath, Path.Combine(destinationFolderPath, "net6.0-windows"));
            Directory.Delete(tempFolderPath, true);
            //MessageBox.Show("อัพเดตสำเร็จ");
        }

        private void btnCheckHicm_Click(object sender, RoutedEventArgs e)
        {
            btncard.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFFFFFFF"));
            btnUploadstatement.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFFFFFFF"));
            btnEclaimapi.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFFFFFFF"));
            btnCheckHicm.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFC1BFFD"));
            useMDR = false;
            useRepStatement = false;
            useEclaimAPI = false;
            useIncomereport = true;
            versionnew = string.Empty;
            versiondevice = string.Empty;
            versionrevert = string.Empty;
            readversionnew = string.Empty;
            readversiondevice = string.Empty;
            GetVersioneCheckHicm();
            if (!string.IsNullOrEmpty(versionnew))
            {
                if (versiondevice != versionnew)//ต่างเวอร์ชั่นกัน
                {
                    if (!string.IsNullOrEmpty(versiondevice))
                    {
                        nameMdr.Text = "Income Report(มีอัพเดต): " + versionnew;
                        ButtonUpdate();

                    }
                    else
                    {
                        nameMdr.Text = "Income Report(ยังไม่ได้ติดตั้ง)";
                        ButtonNotinstall();
                    }


                }
                else if (versiondevice == versionnew)
                {
                    nameMdr.Text = "Income Report(ล่าสุด)";
                    ButtonVersionsame();
                }
            }
            else
            {//ยังไม่ติดตั้ง
                nameMdr.Text = "Income Report(ยังไม่ได้ติดตั้ง)";
                ButtonNotinstall();
            }
            nameProgram.Text = "โปรแกรม Income Report ";
            detailProgram.Text = "โปรแกรม Income Report เป็นโปรแกรมใช้ในการตรวจสอบข้อมูลก่อนเบิกค่ารักษาพยาบาล";
            packIconCard.Kind = MaterialDesignThemes.Wpf.PackIconKind.CheckboxMarked;
            icon.Visibility = Visibility.Visible;
            detail.Visibility = Visibility.Visible;
        }

        private void btnSearchMain_Click(object sender, RoutedEventArgs e)
        {
            btncard.IsEnabled = false;
            btnEclaimapi.IsEnabled = false;
            btnUploadstatement.IsEnabled = false;
            btnCheckHicm.IsEnabled = false;
            btnSearchMain.IsEnabled = false;

            RunBackgroundTaskCheckHi7main();
        }
        void GetDisableprogram()
        {
            btnSearchMain.IsEnabled = false;
            btnMdrSearchupdate.IsEnabled = false;
            btnMdrupdate.IsEnabled = false;
            btncard.IsEnabled = false;
            btnEclaimapi.IsEnabled = false;
            btnUploadstatement.IsEnabled = false;
            btnCheckHicm.IsEnabled = false;
        }

        private void btnUpdateMain_Click(object sender, RoutedEventArgs e)
        {
            txtblockStatus.Text = " กำลังอัพเดตกรุณารอ..";
            txtblockStatus.Visibility = Visibility.Visible;
            btncard.IsEnabled = false;
            btnEclaimapi.IsEnabled = false;
            btnUploadstatement.IsEnabled = false;
            btnCheckHicm.IsEnabled = false;
            btnSearchMain.IsEnabled = false;
            btnUpdateMain.IsEnabled = false;
            RunBackgroundTaskUpdateHi7main();
        }

        private void btncard_Click(object sender, RoutedEventArgs e)
        {
            //icon.Visibility = Visibility.Visible;
            //detail.Visibility = Visibility.Visible;
            ////picslide.Visibility = Visibility.Visible;
            //btncardprogram.IsEnabled = false;
            //nameMdr.Text = "กำลังตรวจสอบอัพเดต";
            //if (!isStoryboardPlaying)
            //{
            //    RunBackgroundTaskCheck();
            //    isStoryboardPlaying = true;
            //}            
            btncard.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFC1BFFD"));
            btnUploadstatement.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFFFFFFF"));
            btnEclaimapi.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFFFFFFF"));
            btnCheckHicm.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFFFFFFF"));
            useMDR = true;
            useRepStatement = false;
            useEclaimAPI = false;
            useIncomereport = false;
            versionnew = string.Empty;
            versiondevice = string.Empty;
            versionrevert = string.Empty;
            readversionnew = string.Empty;
            readversiondevice = string.Empty;
            GetVersion();
            if (!string.IsNullOrEmpty(versionnew))
            {
                if (versiondevice != versionnew)//ต่างเวอร์ชั่นกัน
                {
                    if (!string.IsNullOrEmpty(versiondevice))
                    {
                        nameMdr.Text = "โปรแกรมเวชระเบียน(มีอัพเดต): " + versionnew;
                        ButtonUpdate();

                    }
                    else
                    {
                        nameMdr.Text = "โปรแกรมเวชระเบียน(ยังไม่ได้ติดตั้ง)";
                        ButtonNotinstall();
                    }


                }
                else if (versiondevice == versionnew)
                {
                    nameMdr.Text = "โปรแกรมเวชระเบียน(ล่าสุด)";
                    ButtonVersionsame();
                }
            }
            else
            {//ยังไม่ติดตั้ง
                nameMdr.Text = "โปรแกรมเวชระเบียน(ยังไม่ได้ติดตั้ง)";
                ButtonNotinstall();
            }
            nameProgram.Text = "โปรแกรมเวชระเบียน ";
            detailProgram.Text = "โปรแกรมเวชระเบียน เป็นโปรแกรมในการบันทึกข้อมูลผู้มารับบริการ";
            packIconCard.Kind = MaterialDesignThemes.Wpf.PackIconKind.CardAccountDetails;
            icon.Visibility = Visibility.Visible;
            detail.Visibility = Visibility.Visible;
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
                DragMove();
        }

        private void btnMdrinstall_Click(object sender, RoutedEventArgs e)
        {
            GetDisableprogram();
            if (useMDR == true)
            {
                if (!isStoryboardPlaying)
                {
                    RunBackgroundTaskinstall();
                    isStoryboardPlaying = true;
                    isStoryboardPlayingloading = true;
                }
            }
            else if (useRepStatement == true)
            {
                if (!isStoryboardPlaying)
                {
                    RunBackgroundTaskinstallRepStatement();
                    isStoryboardPlaying = true;
                    isStoryboardPlayingloading = true;
                }
            }
            else if (useIncomereport == true)
            {
                if (!isStoryboardPlaying)
                {
                    RunBackgroundTaskinstallIncomereport();
                    isStoryboardPlaying = true;
                    isStoryboardPlayingloading = true;
                }
            }
            else if (useEclaimAPI == true)
            {
                if (!isStoryboardPlaying)
                {
                    RunBackgroundTaskinstallEclaimapi();
                    isStoryboardPlaying = true;
                    isStoryboardPlayingloading = true;
                }
            }
            //if(useRepStatement == true)         
        }

        private void btncardprogram_Click(object sender, RoutedEventArgs e)
        {
            //app_path = Class.APIConnect.API_PATH;
            //string param = strUserLogin + " " + " " + SELECT_ROLE + " " + INSERT_ROLE + " " + UPDATE_ROLE + " " + DELETE_ROLE;
            Process pro = new Process();
            if (useMDR == true)
            {
                frmLogin frmLogin = new frmLogin();
                frmLogin.ShowDialog();
            }
            else if (useRepStatement == true)
            {
                pro.StartInfo.FileName = app_path + "C:\\Hicm Program\\HicmMaster\\Debug\\Mdr.exe";
                pro.Start();
            }
            else if (useIncomereport == true)
            {
                pro.StartInfo.FileName = app_path + "C:\\Hicmreport Program\\HicmreportMaster\\Debug\\income_report.exe";
                pro.Start();
            }
            else if (useEclaimAPI == true)
            {
                pro.StartInfo.FileName = app_path + "C:\\Apieclaim Program\\ApieclaimMaster\\Debug\\eclaim_api.exe";
                pro.Start();
            }
            
            
        }
        //get เปิดปิดปุ่ม ที่เป็นเหมือนกัน
        void ButtonNotinstall()
        {
            btnMdrinstall.Visibility = Visibility.Collapsed;
            btnMdrrevert.Visibility = Visibility.Collapsed;
            btnMdrupdate.Visibility = Visibility.Collapsed;
            btnMdrSearchupdate.Visibility = Visibility.Visible;
            btncardprogram.IsEnabled = false;
        }
        void ButtonInstall()
        {
            btnMdrinstall.Visibility = Visibility.Collapsed;
            btnMdrrevert.Visibility = Visibility.Collapsed;
            btnMdrupdate.Visibility = Visibility.Collapsed;
            btnMdrSearchupdate.Visibility = Visibility.Visible;
            btncardprogram.IsEnabled = false;
        }
        void ButtonVersionsame()
        {
            btnMdrinstall.Visibility = Visibility.Collapsed;
            btnMdrrevert.Visibility = Visibility.Collapsed;
            btnMdrupdate.Visibility = Visibility.Collapsed;
            btnMdrSearchupdate.Visibility = Visibility.Visible;
            btncardprogram.IsEnabled = true;
        }
        void ButtonUpdate()
        {
            btnMdrinstall.Visibility = Visibility.Collapsed;
            btnMdrrevert.Visibility = Visibility.Collapsed;
            btnMdrupdate.Visibility = Visibility.Visible;
            btnMdrSearchupdate.Visibility = Visibility.Visible;
            btncardprogram.IsEnabled = true;
        }

    }
}
