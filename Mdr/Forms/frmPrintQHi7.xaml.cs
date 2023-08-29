using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Printing;
using System.Net;
using System.Net.Http;
using System.Xml;
using System.Drawing.Printing;
using QRCoder;
using System.Drawing.Imaging;
using System.IO;
using System.Drawing;

namespace Mdr.Forms
{
    
    /// <summary>
    /// Interaction logic for frmPrintQHi7.xaml
    /// </summary>
    public partial class frmPrintQHi7 : Window
    {
        public static string strhn, strfullname,strbirthday, strnamepttype, strclinic, strdateServ, strtitle, strsex, strlastName,
            strage, strQueueNumber, strQueuetype, strhospname, strClimeCode, strFreetext, strPrinterName, strPrinterCopy, strWidthfroms;
        public static string strFreetextvalue1, strFreetextvalue2, strFreetextvalue3, strFreetextvalue4, strFreetextvalue5, strFreetextvalue6,strFreetextvalue7, strFreetext1, strFreetext2;
        public static string strCheckSetting = "0";
        public static string TimeServQ4U = "", DateServQ4U="";
        public frmPrintQHi7()
        {
            InitializeComponent();
            
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {        
            this.stpSeting.Visibility = Visibility.Visible;
            foreach (string printer in PrinterSettings.InstalledPrinters)
                cbbSpritername.Items.Add(printer);
            if (strCheckSetting == "1")
            {
                Hi7.Class.APIConnect.getConfgXML();
                this.getConfgXMLsetPrintqueue();
                Width = Int32.Parse(strWidthfroms);
                this.setFontsize();
                this.setValuesetting();
                this.lbFreetext.Content = strFreetext1;
                this.SizeToContent = SizeToContent.Height;
            }
            else if(strCheckSetting == "0") {
                this.stpSeting.Visibility = Visibility.Collapsed;
                Hi7.Class.APIConnect.getConfgXML();
                this.getConfgXMLsetPrintqueue();
                Width = Int32.Parse(strWidthfroms);
                this.setFontsize();
                this.getDataprint();
                this.SizeToContent = SizeToContent.Height;
                int number = int.Parse(strPrinterCopy);
                try { 
                    for(Int16 i = 1; i <= number; i++)
                    {
                        this.getPrint();
                    }
                    HI7.Class.HIUility._VN = "";
                    HI7.Class.HIUility._HN = "";
                    HI7.Class.HIUility._CLN = "";
                    HI7.Class.HIUility._PriorityIDQ4U = "";
                    HI7.Class.HIUility._PnameQ4U = "";
                    HI7.Class.HIUility._PTTYPE = "";
                    HI7.Class.HIUility._SexQ4U = "";
                    HI7.Class.HIUility._FnameQ4U = "";
                    HI7.Class.HIUility._LnameQ4U = "";
                    HI7.Class.HIUility._BrthdateQ4U = "";
                    HI7.Class.HIUility._BrthdateQ4UPrint = "";
                }
                catch (Exception) { 
                    
                }
                       
            }
            else if (strCheckSetting == "2")
            {
                this.stpSeting.Visibility = Visibility.Collapsed;
                Hi7.Class.APIConnect.getConfgXML();
                this.getConfgXMLsetPrintqueue();
                Width = Int32.Parse(strWidthfroms);
                this.setFontsize();
                this.getDatareprint();
                this.SizeToContent = SizeToContent.Height;
                int number = int.Parse(strPrinterCopy);
                try
                {
                    for (Int16 i = 1; i <= number; i++)
                    {
                        this.getPrint();
                    }
                }
                catch (Exception)
                {

                }

            }

        }
        //private void btnSettingClose_Click(object sender, RoutedEventArgs e)
        //{
        //    this.Close();
        //}
        private void btnSettingSave_Click(object sender, RoutedEventArgs e)
        {
            this.modify_configxmlFont();
            this.getConfgXMLsetPrintqueue();
            Width = Int32.Parse(strWidthfroms);
            this.setFontsize();
            this.setValuesetting();
            MessageBox.Show("บันทึกการตั้งค่าสำเร็จ!!");
            this.Close();
        }
        private void modify_configxmlFont()
        {    
            XmlDocument myXml = new XmlDocument();
            XmlNodeList nodes;
            string strConfig_Path = @"C:\HI7\hi7setprintqueue.xml";


            try
            {
                myXml.Load(strConfig_Path);
                XmlNode strxmlNode;
                //printerName
                strxmlNode = myXml.SelectSingleNode("/Config/name[@API='printerName']");
                if (strxmlNode != null && this.cbbSpritername.Text != "")
                {
                    nodes = strxmlNode.ChildNodes[0].ChildNodes;
                    if (nodes != null)
                    {
                        nodes.Item(0).InnerText = cbbSpritername.Text;
                    }
                    else
                    {
                        nodes.Item(0).InnerText = cbbSpritername.Text;
                    }
                }
                //widthforms
                strxmlNode = myXml.SelectSingleNode("/Config/name[@API='widthforms']");
                if (strxmlNode != null && this.tbSwidth.Text != "")
                {
                    nodes = strxmlNode.ChildNodes[0].ChildNodes;
                    if (nodes != null)
                    {
                        nodes.Item(0).InnerText = tbSwidth.Text;
                    }
                    else
                    {
                        nodes.Item(0).InnerText = tbSwidth.Text;
                    }
                }
                //tbShostname
                strxmlNode = myXml.SelectSingleNode("/Config/name[@API='lbhostName']");
                if (strxmlNode != null && this.tbShostname.Text != "")
                {
                    nodes = strxmlNode.ChildNodes[0].ChildNodes;
                    if (nodes != null)
                    {
                        nodes.Item(0).InnerText = tbShostname.Text;
                    }
                    else
                    {
                        nodes.Item(0).InnerText = tbShostname.Text;
                    }
                }
                //tbSclinic
                strxmlNode = myXml.SelectSingleNode("/Config/name[@API='lbClinic']");
                if (strxmlNode != null && this.tbSclinic.Text != "")
                {
                    nodes = strxmlNode.ChildNodes[0].ChildNodes;
                    if (nodes != null)
                    {
                        nodes.Item(0).InnerText = tbSclinic.Text;
                    }
                    else
                    {
                        nodes.Item(0).InnerText = tbSclinic.Text;
                    }
                }
                //lbHn
                strxmlNode = myXml.SelectSingleNode("/Config/name[@API='lbHn']");
                if (strxmlNode != null && this.tbShn.Text != "")
                {
                    nodes = strxmlNode.ChildNodes[0].ChildNodes;
                    if (nodes != null)
                    {
                        nodes.Item(0).InnerText = tbShn.Text;
                    }
                    else
                    {
                        nodes.Item(0).InnerText = tbShn.Text;
                    }
                }
                //lbFullname
                strxmlNode = myXml.SelectSingleNode("/Config/name[@API='lbFullname']");
                if (strxmlNode != null && this.tbSfullname.Text != "")
                {
                    nodes = strxmlNode.ChildNodes[0].ChildNodes;
                    if (nodes != null)
                    {
                        nodes.Item(0).InnerText = tbSfullname.Text;
                    }
                    else
                    {
                        nodes.Item(0).InnerText = tbSfullname.Text;
                    }
                }
                //lbBirthday
                strxmlNode = myXml.SelectSingleNode("/Config/name[@API='lbBirthday']");
                if (strxmlNode != null && this.tbSbirthday.Text != "")
                {
                    nodes = strxmlNode.ChildNodes[0].ChildNodes;
                    if (nodes != null)
                    {
                        nodes.Item(0).InnerText = tbSbirthday.Text;
                    }
                    else
                    {
                        nodes.Item(0).InnerText = tbSbirthday.Text;
                    }
                }
                //lbSex
                strxmlNode = myXml.SelectSingleNode("/Config/name[@API='lbSex']");
                if (strxmlNode != null && this.tbSsecage.Text != "")
                {
                    nodes = strxmlNode.ChildNodes[0].ChildNodes;
                    if (nodes != null)
                    {
                        nodes.Item(0).InnerText = tbSsecage.Text;
                    }
                    else
                    {
                        nodes.Item(0).InnerText = tbSsecage.Text;
                    }
                }
                //lbAge
                strxmlNode = myXml.SelectSingleNode("/Config/name[@API='lbAge']");
                if (strxmlNode != null && this.tbSsecage.Text != "")
                {
                    nodes = strxmlNode.ChildNodes[0].ChildNodes;
                    if (nodes != null)
                    {
                        nodes.Item(0).InnerText = tbSsecage.Text;
                    }
                    else
                    {
                        nodes.Item(0).InnerText = tbSsecage.Text;
                    }
                }
                //lbPttype
                strxmlNode = myXml.SelectSingleNode("/Config/name[@API='lbPttype']");
                if (strxmlNode != null && this.tbSpttype.Text != "")
                {
                    nodes = strxmlNode.ChildNodes[0].ChildNodes;
                    if (nodes != null)
                    {
                        nodes.Item(0).InnerText = tbSpttype.Text;
                    }
                    else
                    {
                        nodes.Item(0).InnerText = tbSpttype.Text;
                    }
                }
                //lbQueue
                strxmlNode = myXml.SelectSingleNode("/Config/name[@API='lbQueue']");
                if (strxmlNode != null && this.tbSqueue.Text != "")
                {
                    nodes = strxmlNode.ChildNodes[0].ChildNodes;
                    if (nodes != null)
                    {
                        nodes.Item(0).InnerText = tbSqueue.Text;
                    }
                    else
                    {
                        nodes.Item(0).InnerText = tbSqueue.Text;
                    }
                }
                //lbQueuetype
                strxmlNode = myXml.SelectSingleNode("/Config/name[@API='lbQueuetype']");
                if (strxmlNode != null && this.tbStypequeue.Text != "")
                {
                    nodes = strxmlNode.ChildNodes[0].ChildNodes;
                    if (nodes != null)
                    {
                        nodes.Item(0).InnerText = tbStypequeue.Text;
                    }
                    else
                    {
                        nodes.Item(0).InnerText = tbStypequeue.Text;
                    }
                }
                //lbAuthenCode
                strxmlNode = myXml.SelectSingleNode("/Config/name[@API='lbAuthenCode']");
                if (strxmlNode != null && this.tbSauthencode.Text != "")
                {
                    nodes = strxmlNode.ChildNodes[0].ChildNodes;
                    if (nodes != null)
                    {
                        nodes.Item(0).InnerText = tbSauthencode.Text;
                    }
                    else
                    {
                        nodes.Item(0).InnerText = tbSauthencode.Text;
                    }
                }
                //lbFreetext
                strxmlNode = myXml.SelectSingleNode("/Config/name[@API='lbFreetext']");
                if (strxmlNode != null && this.tbFreetext.Text != "")
                {
                    nodes = strxmlNode.ChildNodes[0].ChildNodes;
                    if (nodes != null)
                    {
                        nodes.Item(0).InnerText = tbFreetext.Text;
                    }
                    else
                    {
                        nodes.Item(0).InnerText = tbFreetext.Text;
                    }
                }
                //lbDatenow
                strxmlNode = myXml.SelectSingleNode("/Config/name[@API='lbDatenow']");
                if (strxmlNode != null && this.tbSdatenow.Text != "")
                {
                    nodes = strxmlNode.ChildNodes[0].ChildNodes;
                    if (nodes != null)
                    {
                        nodes.Item(0).InnerText = tbSdatenow.Text;
                    }
                    else
                    {
                        nodes.Item(0).InnerText = tbSdatenow.Text;
                    }
                }
                //printerCopy
                strxmlNode = myXml.SelectSingleNode("/Config/name[@API='printerCopy']");
                if (strxmlNode != null && this.tbScoppy.Text != "")
                {
                    nodes = strxmlNode.ChildNodes[0].ChildNodes;
                    if (nodes != null)
                    {
                        nodes.Item(0).InnerText = tbScoppy.Text;
                    }
                    else
                    {
                        nodes.Item(0).InnerText = tbScoppy.Text;
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
        private void getConfgXMLsetPrintqueue()
        {
        
        XmlTextReader m_xmlrprintprint;
            string strConfigsetPrint_Path = @"C:\HI7\hi7setprintqueue.xml";
            //string strConfig_Path = @"C:\HI7\hi7config.xml";

            try
            {

                // m_xmlrprint = New XmlTextReader(My.Application.Info.DirectoryPath & "\Data\config.xml")
                m_xmlrprintprint = new XmlTextReader(strConfigsetPrint_Path);
                m_xmlrprintprint.WhitespaceHandling = WhitespaceHandling.None;
                // read the xml declaration and advance to family tag

                // read the Config tagn
                m_xmlrprintprint.Read();

                // Load the Loop
                m_xmlrprintprint.Read();

                while (!m_xmlrprintprint.EOF)
                {

                    m_xmlrprintprint.Read();
                    m_xmlrprintprint.GetAttribute("lbhostName");
                    m_xmlrprintprint.Read();
                    strhospname = m_xmlrprintprint.ReadElementString("value");
                    //ขนาด Font lbClinic
                    m_xmlrprintprint.Read();
                    m_xmlrprintprint.GetAttribute("lbClinic");
                    m_xmlrprintprint.Read();
                    strclinic = m_xmlrprintprint.ReadElementString("value").ToString();
                    //ขนาด Font lbHn
                    m_xmlrprintprint.Read();
                    m_xmlrprintprint.GetAttribute("lbHn");
                    m_xmlrprintprint.Read();
                    strhn = m_xmlrprintprint.ReadElementString("value");
                    //ขนาด Font lbFullname
                    m_xmlrprintprint.Read();
                    m_xmlrprintprint.GetAttribute("lbFullname");
                    m_xmlrprintprint.Read();
                    strfullname = m_xmlrprintprint.ReadElementString("value");
                    //ขนาด Font lbBirthday
                    m_xmlrprintprint.Read();
                    m_xmlrprintprint.GetAttribute("lbBirthday");
                    m_xmlrprintprint.Read();
                    strbirthday = m_xmlrprintprint.ReadElementString("value");
                    //ขนาด Font lbSex
                    m_xmlrprintprint.Read();
                    m_xmlrprintprint.GetAttribute("lbSex");
                    m_xmlrprintprint.Read();
                    strsex = m_xmlrprintprint.ReadElementString("value");
                    //ขนาด Font lbAge
                    m_xmlrprintprint.Read();
                    m_xmlrprintprint.GetAttribute("lbAge");
                    m_xmlrprintprint.Read();
                    strage = m_xmlrprintprint.ReadElementString("value");
                    //ขนาด Font lbPttype
                    m_xmlrprintprint.Read();
                    m_xmlrprintprint.GetAttribute("lbPttype");
                    m_xmlrprintprint.Read();
                    strnamepttype = m_xmlrprintprint.ReadElementString("value");
                    //ขนาด Font lbQueue
                    m_xmlrprintprint.Read();
                    m_xmlrprintprint.GetAttribute("lbQueue");
                    m_xmlrprintprint.Read();
                    strQueueNumber = m_xmlrprintprint.ReadElementString("value");
                    //ขนาด Font lbQueuetype
                    m_xmlrprintprint.Read();
                    m_xmlrprintprint.GetAttribute("lbQueuetype");
                    m_xmlrprintprint.Read();
                    strQueuetype = m_xmlrprintprint.ReadElementString("value");
                    //ขนาด Font lbAuthenCode
                    m_xmlrprintprint.Read();
                    m_xmlrprintprint.GetAttribute("lbAuthenCode");
                    m_xmlrprintprint.Read();
                    strClimeCode = m_xmlrprintprint.ReadElementString("value");
                    //ขนาด Font lbFreetext
                    m_xmlrprintprint.Read();
                    m_xmlrprintprint.GetAttribute("lbFreetext");
                    m_xmlrprintprint.Read();
                    strFreetext = m_xmlrprintprint.ReadElementString("value");
                    strFreetext1 = m_xmlrprintprint.ReadElementString("text1");
                    //strFreetext2 = m_xmlrprintprint.ReadElementString("text2");
                    //strFreetextvalue1 = m_xmlrprintprint.ReadElementString("value1");
                    //strFreetextvalue2 = m_xmlrprintprint.ReadElementString("value2");
                    //strFreetextvalue3 = m_xmlrprintprint.ReadElementString("value3");
                    //strFreetextvalue4 = m_xmlrprintprint.ReadElementString("value4");
                    //strFreetextvalue5 = m_xmlrprintprint.ReadElementString("value5");
                    //strFreetextvalue6 = m_xmlrprintprint.ReadElementString("value6");
                    //strFreetextvalue7 = m_xmlrprintprint.ReadElementString("value7");
                    //ขนาด Font lbDatenow
                    m_xmlrprintprint.Read();
                    m_xmlrprintprint.GetAttribute("lbDatenow");
                    m_xmlrprintprint.Read();
                    strdateServ = m_xmlrprintprint.ReadElementString("value");                    
                    //ขนาด Font printerName
                    m_xmlrprintprint.Read();
                    m_xmlrprintprint.GetAttribute("printerName");
                    m_xmlrprintprint.Read();
                    strPrinterName = m_xmlrprintprint.ReadElementString("value"); 
                    //ขนาด Font printerCopy
                    m_xmlrprintprint.Read();
                    m_xmlrprintprint.GetAttribute("printerCopy");
                    m_xmlrprintprint.Read();
                    strPrinterCopy = m_xmlrprintprint.ReadElementString("value");
                    //ขนาด Font widthforms
                    m_xmlrprintprint.Read();
                    m_xmlrprintprint.GetAttribute("widthforms");
                    m_xmlrprintprint.Read();
                    strWidthfroms = m_xmlrprintprint.ReadElementString("value");
                    break;
                }
                // close the reader


            }

            // Return strIPAddress
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        //กำหนดขนาดตัวอักษร ถ้าเป็น 0 จะหมายถึงไม่แสดงข้อความนั้นๆ
        void setFontsize() {
            if (strhospname == "0"){ lbhostName.Visibility = Visibility.Collapsed; } else { lbhostName.FontSize = Int32.Parse(strhospname); lbhostName.Visibility = Visibility.Visible; }
            if (strclinic == "0") { lbClinic.Visibility = Visibility.Collapsed; } else { lbClinic.FontSize = Int32.Parse(strclinic); lbClinic.Visibility = Visibility.Visible; }
            if (strhn == "0") { lbHn.Visibility = Visibility.Collapsed; } else { lbHn.FontSize = Int32.Parse(strhn); lbHn.Visibility = Visibility.Visible; }
            if (strfullname == "0") { lbFullname.Visibility = Visibility.Collapsed; } else { lbFullname.FontSize = Int32.Parse(strfullname); lbFullname.Visibility = Visibility.Visible; }
            if (strbirthday == "0") { lbBirthday.Visibility = Visibility.Collapsed; } else { lbBirthday.FontSize = Int32.Parse(strbirthday); lbBirthday.Visibility = Visibility.Visible; }
            if (strsex == "0") { lbSex.Visibility = Visibility.Collapsed; } else { lbSex.FontSize = Int32.Parse(strsex); lbSex.Visibility = Visibility.Visible; }
            if (strage == "0") { lbAge.Visibility = Visibility.Collapsed; } else { lbAge.FontSize = Int32.Parse(strage); lbAge.Visibility = Visibility.Visible; }
            if (strnamepttype == "0") { lbPttype.Visibility = Visibility.Collapsed; } else { lbPttype.FontSize = Int32.Parse(strnamepttype); lbPttype.Visibility = Visibility.Visible; }
            if (strQueueNumber == "0") { lbQueue.Visibility = Visibility.Collapsed; } else { lbQueue.FontSize = Int32.Parse(strQueueNumber); lbQueue.Visibility = Visibility.Visible; }
            if (strQueuetype == "0") { lbQueuetype.Visibility = Visibility.Collapsed; } else { lbQueuetype.FontSize = Int32.Parse(strQueuetype); lbQueuetype.Visibility = Visibility.Visible; }
            if (strClimeCode == "0") { lbAuthenCode.Visibility = Visibility.Collapsed; } else { lbAuthenCode.FontSize = Int32.Parse(strClimeCode); lbAuthenCode.Visibility = Visibility.Visible; }
            if (strFreetext == "0") { lbFreetext.Visibility = Visibility.Collapsed; } else { lbFreetext.FontSize = Int32.Parse(strFreetext); lbFreetext.Visibility = Visibility.Visible; }
            if (strdateServ == "0") { lbDatenow.Visibility = Visibility.Collapsed; } else { lbDatenow.FontSize = Int32.Parse(strdateServ); lbDatenow.Visibility = Visibility.Visible; }
        }       

        void setValuesetting()
        {
            this.cbbSpritername.Text = strPrinterName.ToString();
            this.tbSwidth.Text = strWidthfroms.ToString();
            this.tbShostname.Text = strhospname.ToString();
            this.tbSclinic.Text = strclinic.ToString();
            this.tbShn.Text = strhn.ToString();
            this.tbSfullname.Text = strfullname.ToString();
            this.tbSbirthday.Text = strbirthday.ToString();
            this.tbSsecage.Text = strsex.ToString();
            this.tbSqueue.Text = strQueueNumber.ToString();
            this.tbStypequeue.Text = strQueuetype.ToString(); 
            this.tbSauthencode.Text = strClimeCode.ToString();
            this.tbFreetext.Text = strFreetext.ToString();
            this.tbSdatenow.Text = strdateServ.ToString();
            this.tbSpttype.Text = strnamepttype.ToString();
            this.tbScoppy.Text = strPrinterCopy.ToString();
        }
        void getDataprint() {
            lbhostName.Content = HI7.Class.HIUility._HOSPITALNAME;
            lbClinic.Content = HI7.Class.HIUility.c2n_cln(HI7.Class.HIUility._CLN);
            lbHn.Content = "HN: " + HI7.Class.HIUility._HN;
            lbHn.FontWeight = FontWeights.Bold;
            lbFullname.Content = HI7.Class.HIUility._PnameQ4U + HI7.Class.HIUility._FnameQ4U + " " + HI7.Class.HIUility._LnameQ4U;
            if (HI7.Class.HIUility._BrthdateQ4U != "" && HI7.Class.HIUility._BrthdateQ4U != null)
            {
                lbBirthday.Content = " วันเกิด: "+HI7.Class.HIUility._BrthdateQ4UPrint;
            }
            else
            {
                lbBirthday.Content = "ไม่ระบุ";
            }
            
            if (HI7.Class.HIUility._SexQ4U == "1")
            {
                lbSex.Content = "เพศ: ชาย";
            }
            else if (HI7.Class.HIUility._SexQ4U == "2")
            {
                lbSex.Content = "เพศ: หญิง";
            }
            else {
                lbSex.Content = "เพศ: ไม่รุเพศ";
            }
            getdatetimeServerprintQ();
            lbAge.Content = "อายุ: " + HI7.Class.HIUility.Hn2AgeYY(HI7.Class.HIUility._HN) + " ปี";
            lbPttype.Content = "สิทธิ์: "+'[' + HI7.Class.HIUility._PTTYPE + ']' + HI7.Class.HIUility.c2n_pttype(HI7.Class.HIUility._PTTYPE);
            lbQueue.Content = "คิวที่: " + HI7.Class.HIUility._StrQueueNumber;
            lbQueue.FontWeight = FontWeights.Bold;
            lbQueuetype.Content = "ประเภทคิว: "+HI7.Class.HIUility._PrintPriority;           
            lbAuthenCode.Content = "Authen Code: " + HI7.Class.HIUility._claimCode;
            lbFreetext.Content = strFreetext1;            
            string dataGen = HI7.Class.HIUility._HCODE + "#xxxxxxxxxxxxxx#" + HI7.Class.HIUility._HN + "#" + HI7.Class.HIUility._CLN + "#" +
                HI7.Class.HIUility._StrQueueNumber + "#" + HI7.Class.HIUility._StrQueueNumber.Split('-')[1] + "#" +
                DateServQ4U + "#" + TimeServQ4U + "#" +
                HI7.Class.HIUility.c2n_cln(HI7.Class.HIUility._CLN) + "#" + HI7.Class.HIUility._PrintPriority;
            GenQRCODE(dataGen);
            lbDatenow.Content = "วันที่ " + HI7.Class.HIUility.Getdateserverdatethaishort() + " " + DateTime.Now.ToString("HH:mm:ss");
        }
        void getdatetimeServerprintQ()
        {        
                string strSql = "select _drg,hi_hsp_nm,serverapi,prt_q4u,printerid,smallqueue,qtyprtq,CURTIME() AS T,DATE_FORMAT(CURDATE(),'%Y%m%d') AS D,now() as N from setup ";
                DataRow dr;
                DataTable dt = new System.Data.DataTable();
                Dictionary<string, object> dictData = new Dictionary<string, object>();
                dictData.Add("query", strSql);

                try
                {
                    dt = Hi7.Class.APIConnect.getDataTable("query/hi7_query", "POST", dictData);
                    if (dt != null)
                    {
                        //Growl.Success("Connection database Queue Success!!!");
                        if (dt.Rows.Count > 0)
                        {
                            dr = dt.Rows[0];
                            DateServQ4U = dr.ItemArray[8].ToString();
                            TimeServQ4U = dr.ItemArray[7].ToString().Replace(":","");
                                                     
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
        void getDatareprint()
        {
            //HI7.Class.HIUility.getLoginQ4U();
            //reprintHn, reprintCln, reprintFullname,reprintSex,reprintAge,reprintPttype,reprintQueuenumber,reprintQueuepriority,reprintAuthencode;
            lbhostName.Content = HI7.Class.HIUility._HOSPITALNAME;
            lbClinic.Content = Mdr.Forms.frmMdr.reprintCln;
            string idCln = HI7.Class.HIUility.getIdcln(Mdr.Forms.frmMdr.reprintCln);
            lbHn.Content = "HN: " + Mdr.Forms.frmMdr.reprintHn;
            lbHn.FontWeight = FontWeights.Bold;
            lbFullname.Content = Mdr.Forms.frmMdr.reprintFullname;
            if (Mdr.Forms.frmMdr.reprintBirthdate != "" && Mdr.Forms.frmMdr.reprintBirthdate != null)
            {
                lbBirthday.Content = "วันเกิด: " + Mdr.Forms.frmMdr.reprintBirthdate;
            }
            else
            {
                lbBirthday.Content = "ไม่ระบุ";
            }
            lbSex.Content = "เพศ: "+Mdr.Forms.frmMdr.reprintSex;
            lbAge.Content = "อายุ: " + Mdr.Forms.frmMdr.reprintAge + " ปี";
            lbPttype.Content = "สิทธิ์: " + '[' + Mdr.Forms.frmMdr.reprintIdpttype + ']' + Mdr.Forms.frmMdr.reprintPttype;
            lbQueue.Content = "คิวที่: " + Mdr.Forms.frmMdr.reprintQueuenumber;
            lbQueue.FontWeight = FontWeights.Bold;
            lbQueuetype.Content = "ประเภทคิว: " + Mdr.Forms.frmMdr.reprintQueuepriority;
            lbAuthenCode.Content = "Authen Code: " + Mdr.Forms.frmMdr.reprintAuthencode;
            lbFreetext.Content = strFreetext1;           
            string dataGen = HI7.Class.HIUility._HCODE + "#xxxxxxxxxxxxxx#" + Mdr.Forms.frmMdr.reprintHn + "#" + idCln + "#" +
                Mdr.Forms.frmMdr.reprintQueuenumber + "#" + Mdr.Forms.frmMdr.reprintQueuenumber.Split('-')[1] + "#" +
                Mdr.Forms.frmMdr.reprintDate + "#" + Mdr.Forms.frmMdr.reprintTime + "#" +
                Mdr.Forms.frmMdr.reprintCln + "#" + Mdr.Forms.frmMdr.reprintQueuepriority;
            GenQRCODE(dataGen);
            lbDatenow.Content = "วันที่ " + HI7.Class.HIUility.Getdateserverdatethaishort() + " " + DateTime.Now.ToString("HH:mm:ss");
        }
        void getPrint() {
            PrintDialog printDlg = new PrintDialog();
            printDlg.PrintTicket.PageBorderless = System.Printing.PageBorderless.Borderless;    
            //printDlg.PrintTicket.CopyCount = Int32.Parse(strPrinterCopy);
            printDlg.PrintQueue = new PrintQueue(new PrintServer(), strPrinterName);
            printDlg.PrintVisual(this, "PrintQ");
            strCheckSetting = "0";
            this.Close();
        }
        private void tbSqueue_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                realTimesetvalue();
            }
        }

        private void tbStypequeue_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                realTimesetvalue();
            }
        }

        private void tbSauthencode_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                realTimesetvalue();
            }
        }

        private void tbSdatenow_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                realTimesetvalue();
            }
        }

        private void tbSpttype_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                realTimesetvalue();
            }
        }

        private void tbSsecage_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                realTimesetvalue();
            }
        }

        private void tbSfullname_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                realTimesetvalue();
            }
        }

        private void tbShn_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                realTimesetvalue();
            }
        }

        private void tbSclinic_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                realTimesetvalue();
            }
        }

        private void tbShostname_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                realTimesetvalue();
            }
        }
        private void tbFreetext_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                realTimesetvalue();
            }
        }

        private void tbSwidth_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                realTimesetvalue();
            }
        }
        private void tbSbirthday_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                realTimesetvalue();
            }
        }
        void realTimesetvalue()
        {
            this.modify_configxmlFont();
            this.getConfgXMLsetPrintqueue();
            Width = Int32.Parse(strWidthfroms);
            this.setFontsize();
            this.setValuesetting();
        }
        void GenQRCODE(string dataGen)
        {
            
            QRCodeGenerator qrGenerator = new QRCodeGenerator();
            QRCodeData qrCodeData = qrGenerator.CreateQrCode(dataGen, QRCodeGenerator.ECCLevel.Q);
            QRCode qrCode = new QRCode(qrCodeData);
            System.Drawing.Bitmap qrCodeBitmap = qrCode.GetGraphic(20); // Adjust the size as needed

            using (MemoryStream memoryStream = new MemoryStream())
            {
                qrCodeBitmap.Save(memoryStream, ImageFormat.Png);
                memoryStream.Position = 0;

                BitmapImage bitmapImage = new BitmapImage();
                bitmapImage.BeginInit();
                bitmapImage.StreamSource = memoryStream;
                bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
                bitmapImage.EndInit();

                qrCodeBitmap.Dispose();

                qrCodeImage.Source = bitmapImage;
            }
        }


        //end
    }
}
