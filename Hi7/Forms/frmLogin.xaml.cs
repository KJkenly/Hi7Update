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
using Newtonsoft.Json.Linq;
using System.Xml;
using Newtonsoft.Json;
using System.Data;
using System.Net;
using System.IO;
using System.Diagnostics;

namespace Hi7.Forms
{
    /// <summary>
    /// Interaction logic for frmLogin.xaml
    /// </summary>
    public partial class frmLogin : Window
    {
        public frmLogin()
        {
            InitializeComponent();
            Loaded += Window_Loaded;
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            // this.textBox.Focusable = true;
            this.textBox.Focus();
            Class.APIConnect.getConfgXML();
            getToken();
            HI7.Class.HIUility.getTokenShare();
            // Keyboard.Focus(this.textBox);
        }
        private void KeyDown_Close(object sender, KeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.Escape:
                    e.Handled = true;
                    this.Close();
                    break;
            }
        }
        private void textBox_GotFocus(object sender, RoutedEventArgs e)
        {
            this.textBox.Focus();
        }
        string app_path = @Class.APIConnect.APP_PATH;
        private void ClickBtnLogin(object sender, RoutedEventArgs e)
        {
            if (getLogin()) {
                //frmMainmenu frmMainmenu = new frmMainmenu();
                //frmMainmenu.Show();

                //frmMainmenu frmMainmenu = new frmMainmenu();
                //frmMainmenu.Show();

                //app_path = Class.APIConnect.API_PATH;
                ////string param = strUserLogin + " " + " " + SELECT_ROLE + " " + INSERT_ROLE + " " + UPDATE_ROLE + " " + DELETE_ROLE;
                Process pro = new Process();
                //pro.StartInfo.FileName = app_path + "Mdr.exe";
                pro.StartInfo.FileName = app_path + "C:\\Hi7 Program\\Hi7Master\\net6.0-windows\\Mdr.exe";
                pro.Start();
                //frmMenuMain frmMenuMain = new frmMenuMain();
                //frmMenuMain.Show();
                this.Close();
            }
        }

        JArray roles_all, modules_all;
        private bool getLogin()
        {
            try
            {
                string username = this.textBox.Text.ToString();
                string password = this.passwordBox.Password;
                DataTable dt = new DataTable();
                DataRow dr;
                //   DataRow dr;
                //    http://tssmart.moph.go.th/api/lookup/changwat/list
                Dictionary<string, object> dictData = new Dictionary<string, object>();
                // dictData.Add("lookup_group_code", "occupation");

                string strSQL = "select * from opstaff where staff='" + username + "' and passwrd_hi7='" + password + "'";
                dictData.Add("query", strSQL);
                dt = Hi7.Class.APIConnect.getDataTable("query/hi7_query", "POST", dictData);

                if (dt != null)
                {
                    dr = dt.Rows[0];
                    string pname, fname, lname, staff;
                    pname = dr["pname"].ToString();
                    fname = dr["fname"].ToString();
                    lname = dr["lname"].ToString();
                    staff = dr["staff"].ToString();
                    Class.APIConnect.USER_LOGIN = pname + fname +" "+ lname;
                    Class.APIConnect.USER_IDLOGIN = staff;
                    modify_configxml();

                    //Class.APIConnect.getConfgXML();
                    return true;

                }
                else
                {
                    MessageBox.Show("รหัสผ่านไม่ถูกต้อง");
                    Keyboard.Focus(this.textBox);
                    return false;
                }
                return false;


            }
            catch (Exception ex)
            {
                MessageBox.Show("รหัสผ่านไม่ถูกต้อง");
                return false;
            }
        }
        
        private bool getToken()
        {
            try
            {
                HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(Hi7.Class.APIConnect.API_SERVER + "v1/sign-token");
                request.Method = "GET";
                String test = String.Empty;
                using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                {

                    Stream dataStream = response.GetResponseStream();
                    StreamReader reader = new StreamReader(dataStream);

                    test = reader.ReadToEnd();
                    Class.APIConnect.TOKEN_KEY = (String)JObject.Parse(test).SelectToken("token");
                    reader.Close();
                    dataStream.Close();
                    return true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("ไม่สามารถติดต่อ API ได้" + ex.Message);
                return false;
            }
        }
        private void modify_configxml()
        {
            string str = "";

            XmlDocument myXml = new XmlDocument();
            XmlNodeList nodes;
            string strPath = "";
            string strConfig_Path = @"C:\HI7\hi7config.xml";


            try
            {

                myXml.Load(strConfig_Path);

                // Dim arr As XmlAttribute
                XmlNode strxmlNode;
                //API_SERVER
                strxmlNode = myXml.SelectSingleNode("/Config/name[@API='API_SERVER']");
                if (strxmlNode != null)
                {
                    nodes = strxmlNode.ChildNodes[0].ChildNodes;
                    if (nodes != null)
                    {
                        //nodes.Item(0).InnerText = this.textBox.Text;
                    }
                }
                //APP_PART
                strxmlNode = myXml.SelectSingleNode("/Config/name[@API='APP_PATH']");
                if (strxmlNode != null)
                {
                    nodes = strxmlNode.ChildNodes[0].ChildNodes;
                    if (nodes != null)
                    {
                        //nodes.Item(0).InnerText = this.textBox.Text;
                    }
                }
                // /// TOKEN
                strxmlNode = myXml.SelectSingleNode("/Config/name[@API='API_USER']");
                if (strxmlNode != null)
                {
                    nodes = strxmlNode.ChildNodes[0].ChildNodes;
                    if (nodes != null)
                    {
                        //nodes.Item(0).InnerText = this.textBox.Text;
                    }
                }

                // /// TOKEN
                strxmlNode = myXml.SelectSingleNode("/Config/name[@API='JSON_DATA_ROLES']");
                if (strxmlNode != null)
                {
                    nodes = strxmlNode.ChildNodes[0].ChildNodes;
                    if (nodes != null)
                    {
                        //nodes.Item(0).InnerText = JsonConvert.SerializeObject(roles_all);

                    }
                }

                // /// TOKEN
                strxmlNode = myXml.SelectSingleNode("/Config/name[@API='JSON_DATA_MODULES']");
                if (strxmlNode != null)
                {
                    nodes = strxmlNode.ChildNodes[0].ChildNodes;
                    if (nodes != null)
                    {
                        //nodes.Item(0).InnerText = JsonConvert.SerializeObject(modules_all);

                    }
                }
                // /// TOKEN
                strxmlNode = myXml.SelectSingleNode("/Config/name[@API='TOKEN_KEY']");
                if (strxmlNode != null)
                {
                    nodes = strxmlNode.ChildNodes[0].ChildNodes;
                    if (nodes != null)
                    {
                        nodes.Item(0).InnerText = Class.APIConnect.TOKEN_KEY;
                    }
                }
                // /// NHSO_TOKEN
                strxmlNode = myXml.SelectSingleNode("/Config/name[@API='NHSO_TOKEN']");
                if (strxmlNode != null)
                {
                    nodes = strxmlNode.ChildNodes[0].ChildNodes;
                    if (nodes != null)
                    {
                        //nodes.Item(0).InnerText = Class.APIConnect.TOKEN_KEY;
                    }
                }
                // /// FONT
                strxmlNode = myXml.SelectSingleNode("/Config/name[@API='FONT']");
                if (strxmlNode != null)
                {
                    nodes = strxmlNode.ChildNodes[0].ChildNodes;
                    if (nodes != null)
                    {
                        //nodes.Item(0).InnerText = Class.APIConnect.TOKEN_KEY;
                    }
                }
                strxmlNode = myXml.SelectSingleNode("/Config/name[@API='FONT_SIZE']");
                if (strxmlNode != null)
                {
                    nodes = strxmlNode.ChildNodes[0].ChildNodes;
                    if (nodes != null)
                    {
                        //nodes.Item(0).InnerText = Class.APIConnect.TOKEN_KEY;
                    }
                }
                //USERLOGIN
                strxmlNode = myXml.SelectSingleNode("/Config/name[@API='USERLOGIN']");
                if (strxmlNode != null)
                {
                    nodes = strxmlNode.ChildNodes[0].ChildNodes;
                    if (nodes != null)
                    {
                        nodes.Item(0).InnerText = Class.APIConnect.USER_LOGIN;
                    }
                }
                //PATHDEVICESMARTCARD
                strxmlNode = myXml.SelectSingleNode("/Config/name[@API='PATHDEVICESMARTCARD']");
                if (strxmlNode != null)
                {
                    nodes = strxmlNode.ChildNodes[0].ChildNodes;
                    if (nodes != null)
                    {
                       // nodes.Item(0).InnerText = Class.APIConnect.USER_LOGIN;
                    }
                }
                //USERIDLOGIN
                strxmlNode = myXml.SelectSingleNode("/Config/name[@API='USERIDLOGIN']");
                if (strxmlNode != null)
                {
                    nodes = strxmlNode.ChildNodes[0].ChildNodes;
                    if (nodes != null)
                    {
                        nodes.Item(0).InnerText = Class.APIConnect.USER_IDLOGIN;
                    }
                }

                myXml.Save(strConfig_Path);
                //   Interaction.MsgBox("success!", MsgBoxStyle.Information);
            }

            catch (Exception ex)
            {
                // MsgBox("error!" & ex.StackTrace, MsgBoxStyle.Exclamation)
                var trace = new System.Diagnostics.StackTrace(ex, true);
                // Interaction.MsgBox(ex.Message + Constants.vbCrLf + "Error in ClaimFlag10 - Line number:" + trace.GetFrame(0).GetFileLineNumber());
            }
        }

        
        
    }


}
