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
using System.Xml;

namespace Hi7.Forms
{
    /// <summary>
    /// Interaction logic for frmSeting.xaml
    /// </summary>
    public partial class frmSeting : Window
    {
        string CallFont;
        string CallFont_SIZE;
        public frmSeting()
        {
            InitializeComponent();
           
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Class.APIConnect.getConfgXML();
            CallFont = Class.APIConnect.FONT;
            CallFont_SIZE = Class.APIConnect.FONT_SIZE;
            double doubleVal = Convert.ToDouble(CallFont_SIZE);
            FontFamily = new FontFamily(CallFont);
            FontSize = doubleVal;
        }
        private void modify_configxmlFont()
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


                // /// FONT
                strxmlNode = myXml.SelectSingleNode("/Config/name[@API='FONT']");
                if (strxmlNode != null && this.theComboBox.Text !="")
                {
                    nodes = strxmlNode.ChildNodes[0].ChildNodes;
                    if (nodes != null)
                    {
                        //theComboBox.Text = nodes[0].InnerText;
                        nodes.Item(0).InnerText = theComboBox.Text;
                    }
                    else {
                        nodes.Item(0).InnerText = theComboBox.Text;
                    }
                }

                // /// FONT_SIZE
                strxmlNode = myXml.SelectSingleNode("/Config/name[@API='FONT_SIZE']");
                if (strxmlNode != null && this.cbbFontsize.Text != "")
                {
                    nodes = strxmlNode.ChildNodes[0].ChildNodes;
                    if (nodes != null)
                    {
                        //theComboBox.Text = nodes[0].InnerText;
                        nodes.Item(0).InnerText = cbbFontsize.Text;
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
        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            //double doubleVal = 18;
            this.modify_configxmlFont();
            Class.APIConnect.getConfgXML();
            CallFont = Class.APIConnect.FONT;
            CallFont_SIZE = Class.APIConnect.FONT_SIZE;
            double doubleVal = Convert.ToDouble(CallFont_SIZE);
            
            //lbPreviewFont.Text = CallFont;
            FontFamily = new FontFamily(CallFont);
            FontSize = doubleVal;
                      
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (theComboBox.SelectedItem != null)
            {
                
                lbPreviewFont.Text = "Fontที่คุณได้เลือกคือ "+theComboBox.SelectedItem.ToString();
                lbPreviewFont.FontFamily = (FontFamily)theComboBox.SelectedItem;
                //FontFamily = (FontFamily)theComboBox.SelectedItem;
            }
            else
            {
                lbPreviewFont.Text = "ว่าง";
            }
        }
        private void cbbFontsize_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cbbFontsize.SelectedItem != null)
            {
                //ComboBoxItem cbi1 = (ComboBoxItem)(sender as ComboBox).SelectedItem;
                ComboBoxItem cbi = (ComboBoxItem)cbbFontsize.SelectedItem;
                FontFamily cbif = (FontFamily)theComboBox.SelectedItem;
                string selectedText = cbi.Content.ToString();
                double doubleVal = Convert.ToDouble(selectedText);
                if (cbif == null)
                {
                    lbPreviewFont.Text = "ขนาด Font" + selectedText + "px";
                    lbPreviewFont.FontSize = doubleVal;

                }
                else {
                    lbPreviewFont.Text = "Fontที่คุณได้เลือกคือ " + theComboBox.SelectedItem.ToString() + " \r\nขนาด Font " + selectedText + "px";
                    lbPreviewFont.FontSize = doubleVal;
                }
                
            }
        }

        
    }
}
