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
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.Windows.Xps;
using System.Windows.Xps.Packaging;
using System.IO;
using System.Globalization;
using HandyControl.Controls;
using MessageBox = HandyControl.Controls.MessageBox;

namespace Mdr.Forms
{
    /// <summary>
    /// Interaction logic for frmReferIn.xaml
    /// </summary>
    public partial class frmReferOut : System.Windows.Window
    {
        public string Norefer = "";
        public frmReferOut()
        {
            InitializeComponent();

        }
        private void moveallposition(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                this.DragMove();
            }
        }


        private void ClickClose(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void ClickMin(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;

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
        string hn, txvn, txhn;


        private bool insertData()
        {
            //try
            //{
           
            getRefno();  // เจนเลข refer
            DataTable dt = new DataTable();
            Dictionary<string, object> dictData = new Dictionary<string, object>();
            string strfields, strValues;
            strfields = "vn, rfrno, vstdate, vsttime, an, ward, dct, rfrtype, km, rfrlct, icd10, pricerefer, rfrcs, cln,loads ";
            string vn, rfrno, vstdate, vsttime, an, ward, dct, rfrtype, km, rfrlct, icd10, pricerefer, rfrcs, cln, l_load;
            DateTime dateTime = DateTime.Now;
            
            //rqttime = timeNow();
            vn = txvn;
            rfrno = Norefer; // "64000962";
            vstdate = dateTime.ToString("yyyy-MM-dd", CultureInfo.GetCultureInfo("en-US"));
            vsttime = timeNow();
            an = "0";
            ward = "";
            dct = "";
            rfrtype = cbbdep.SelectedValue.ToString();
            km = txtkm.Text;
            rfrlct = txtRefercode.Text;
            icd10 = txtdx.Text;
            pricerefer = txtprice.Text;
            rfrcs = txtsahad.SelectedValue.ToString();
            cln = cbbdep.SelectedValue.ToString();
            l_load = cbbl_loads.SelectedValue.ToString();
            strValues = "'" + vn + "'" +
                        ",'" + rfrno + "'" +
                        ",'" + vstdate + "'" +
                         ",'" + vsttime + "'" +
                        ",'" + an + "'" +
                        ",'" + ward + "'" +
                        ",'" + dct + "'" +
                        ",'" + rfrtype + "'" +
                        ",'" + km + "'" +
                         ",'" + rfrlct + "'" +
                          ",'" + icd10 + "'" +
                     ",'" + pricerefer + "'" +
                           ",'" + rfrcs + "'" +
                             ",'" + cln + "'" +
                        ",'" + l_load + "'";
            string strSQL = "insert into orfro(" + strfields + ") values(" + strValues + ") ";
            // MessageBox.Show(strSQL);
            //txtdx.Text = strSQL;
            dictData.Add("query", strSQL);
            bool status = Hi7.Class.APIConnect.Excecute("query/hi7_query", "POST", dictData);
            if (status)
            {
                MessageBox.Show("บันทึกข้อมูลสำเร็จ");
                return true;
                strSQL = "";
                this.Close();
            }
            else
                return false;
            strSQL = "";
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
        //}
        //catch (Exception ex)
        //{
        //    return false;
        //}
        void getpt()
        {
            hn = txtSearchHn.Text;
            DataRow dr;
            DataTable dt = new DataTable();
            Dictionary<string, object> dictData = new Dictionary<string, object>();
            dictData.Add("query", "select ovst.vn,pt.hn,pt.fname,pt.lname from ovst inner join pt on ovst.hn=pt.hn where pt.hn= " + "'" + hn + "'" + " and date(ovst.vstdttm) = " + "curdate()");
            dt = Hi7.Class.APIConnect.getDataTable("query/hi7_query", "POST", dictData);

            if (dt != null)
            {

                if (dt.Rows.Count == 1)
                {
                    dr = dt.Rows[0];
                    txvn = dr["vn"].ToString();
                    txhn = dr["hn"].ToString();
                    txtSearchFname.Text = (string)dr["fname"];
                    txtSearchLname.Text = (string)dr["lname"];
                    txtRefercode.Focus();
                }
                else if (dt.Rows.Count > 1)
                {
                    frmpopreferin frmpuprefin = new frmpopreferin();
                    frmpuprefin.Show();
                    dr = dt.Rows[0];
                    txvn = dr["vn"].ToString();
                    txhn = dr["hn"].ToString();
                    txtSearchFname.Text = (string)dr["fname"];
                    txtSearchLname.Text = (string)dr["lname"];
                    txtRefercode.Focus();
                }
                else
                {
                    Growl.Warning("ไม่เจอข้อมูล HN รายนี้");
                    txtSearchHn.Focus();
                }
            }
            else
            {
                Growl.Warning("ไม่เจอข้อมูล HN รายนี้");
            }
        }
        //void getpt()
        //{
        //    hn = txtSearchHn.Text;
        //    DataRow dr;
        //    DataTable dt = new DataTable();
        //    Dictionary<string, object> dictData = new Dictionary<string, object>();
        //    dictData.Add("query", "select Max(ovst.vn) as vn,pt.hn,pt.fname,pt.lname from ovst inner join pt on ovst.hn=pt.hn where pt.hn= " + hn);
        //    dt = Hi7.Class.APIConnect.getDataTable("query/hi7_query", "POST", dictData);
        //    if (dt != null)
        //    {
        //        if (dt.Rows.Count > 0)
        //        {
        //            dr = dt.Rows[0];
        //            txvn = dr["vn"].ToString();
        //            txhn = dr["hn"].ToString();
        //            txtSearchFname.Text = dr["fname"].ToString();
        //            txtSearchLname.Text = dr["lname"].ToString();
        //        }
        //    }
        //    else
        //    {
        //        //   MessageBox.Show("ไม่เจอข้อมูล HN รายนี้");
        //    }

        //    //}
        //    //catch (Exception ex)
        //    //{
        //    //}
        //}
        string off_id;
        void gethosp()
        {
            off_id = txtRefercode.Text;
            DataRow dr;
            DataTable dt = new DataTable();
            Dictionary<string, object> dictData = new Dictionary<string, object>();
            dictData.Add("query", "select off_id,namehosp from hospcode where off_id= " + off_id);
            try
            {
                dt = Hi7.Class.APIConnect.getDataTable("query/hi7_query", "POST", dictData);
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        dr = dt.Rows[0];
                        //MessageBox.Show("cccccccccccc");
                        txtrname.Text = dr["namehosp"].ToString();
                        // txtdx.Focus();
                    }
                }
                else
                {
                    MessageBox.Show("ไม่พบข้อมูลสถานบริการ");
                }
            }
            catch (Exception ex)
            {

            }

        }
        void getRefno()
        {
            dx = txtdx.Text;
            DataRow dr;
            DataTable dt = new DataTable();
            Dictionary<string, object> dictData = new Dictionary<string, object>();
            dictData.Add("query", "select Max(rfrno) + 1 as rfrno from orfro");
            try
            {
                dt = Hi7.Class.APIConnect.getDataTable("query/hi7_query", "POST", dictData);
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        dr = dt.Rows[0];
                        Norefer = dr["rfrno"].ToString();
                        //MessageBox.Show(dr["rfrno"].ToString());
                    }
                }
                else
                {
                    MessageBox.Show("ไม่พบicd10");
                }
            }
            catch (Exception ex)
            {

            }
        }
        string dx;
        void getdx()
        {
            dx = txtdx.Text;
            DataRow dr;
            DataTable dt = new DataTable();
            Dictionary<string, object> dictData = new Dictionary<string, object>();
            dictData.Add("query", "select icd10,icd10name from icd101 where icd10 = " + "'" + dx + "'");
            try
            {
                dt = Hi7.Class.APIConnect.getDataTable("query/hi7_query", "POST", dictData);
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        dr = dt.Rows[0];
                        //MessageBox.Show("cccccccccccc");
                        txtdxname.Text = dr["icd10name"].ToString();

                        cbbdep.Focus();
                    }
                }
                else
                {
                    MessageBox.Show("ไม่พบicd10");
                }
            }
            catch (Exception ex)
            {

            }
        }
        void getl_rfrtype()
        {
            DataTable dt = new DataTable();
            Dictionary<string, object> dictData = new Dictionary<string, object>();
            dictData.Add("query", "select rfrtype,nametype from l_rfrtype");
            try
            {
                dt = Hi7.Class.APIConnect.getDataTable("query/hi7_query", "POST", dictData);
                if (dt != null)
                {
                    string[] selectedColumns = new[] { "rfrtype", "nametype" };//ชื่อคอลัมน์
                    System.Data.DataView view = new System.Data.DataView(dt);
                    System.Data.DataTable selected = view.ToTable("Selected", false, selectedColumns);
                    this.cbbdep.DisplayMemberPath = "nametype";
                    this.cbbdep.SelectedValuePath = "rfrtype";
                    this.cbbdep.ItemsSource = selected.DefaultView;
                    this.cbbdep.SelectedIndex = 0;
                }
                else
                {
                    MessageBox.Show("ไม่พบicd10");
                }
            }
            catch (Exception ex)
            {

            }
        }
        void getcln()
        {
            DataTable dt = new DataTable();
            Dictionary<string, object> dictData = new Dictionary<string, object>();
            dictData.Add("query", "select cln,namecln from cln");
            try
            {
                dt = Hi7.Class.APIConnect.getDataTable("query/hi7_query", "POST", dictData);
                if (dt != null)
                {
                    string[] selectedColumns = new[] { "cln", "namecln" };//ชื่อคอลัมน์
                    System.Data.DataView view = new System.Data.DataView(dt);
                    System.Data.DataTable selected = view.ToTable("Selected", false, selectedColumns);
                    this.cbbsendrefer.DisplayMemberPath = "namecln";
                    this.cbbsendrefer.SelectedValuePath = "cln";
                    this.cbbsendrefer.ItemsSource = selected.DefaultView;
                    this.cbbsendrefer.SelectedIndex = 0;
                }
                else
                {
                    MessageBox.Show("ไม่พบicd10");
                }
            }
            catch (Exception ex)
            {

            }
        }
        void getrfrcs()
        {
            DataTable dt = new DataTable();
            Dictionary<string, object> dictData = new Dictionary<string, object>();
            dictData.Add("query", "select rfrcs,namerfrcs from rfrcs");
            try
            {
                dt = Hi7.Class.APIConnect.getDataTable("query/hi7_query", "POST", dictData);
                if (dt != null)
                {
                    string[] selectedColumns = new[] { "rfrcs", "namerfrcs" };//ชื่อคอลัมน์
                    System.Data.DataView view = new System.Data.DataView(dt);
                    System.Data.DataTable selected = view.ToTable("Selected", false, selectedColumns);
                    this.txtsahad.DisplayMemberPath = "namerfrcs";
                    this.txtsahad.SelectedValuePath = "rfrcs";
                    this.txtsahad.ItemsSource = selected.DefaultView;
                    this.txtsahad.SelectedIndex = 0;
                }
                else
                {
                    MessageBox.Show("ไม่พบicd10");
                }
            }
            catch (Exception ex)
            {

            }
        }
        void getl_load()
        {
            DataTable dt = new DataTable();
            Dictionary<string, object> dictData = new Dictionary<string, object>();
            dictData.Add("query", "select codeloads,nameloads from l_loads");
            try
            {
                dt = Hi7.Class.APIConnect.getDataTable("query/hi7_query", "POST", dictData);
                if (dt != null)
                {
                    string[] selectedColumns = new[] { "codeloads", "nameloads" };//ชื่อคอลัมน์

                    System.Data.DataView view = new System.Data.DataView(dt);
                    System.Data.DataTable selected = view.ToTable("Selected", false, selectedColumns);
                    this.cbbl_loads.DisplayMemberPath = "nameloads";
                    this.cbbl_loads.SelectedValuePath = "codeloads";
                    this.cbbl_loads.ItemsSource = selected.DefaultView;
                    this.cbbl_loads.SelectedIndex = 0;
                }
                else
                {
                    MessageBox.Show("ไม่พบicd10");
                }
            }
            catch (Exception ex)
            {

            }
        }
        private void txtSearchHn_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                getpt();
            }
            else
            {
            }
        }
        private void txtRefercode_KeyUp(object sender, KeyEventArgs e)
        {

        }
        private void txtdx_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                getdx();
            }
            else
            {

            }
        }
        private void btnsaves_Click(object sender, RoutedEventArgs e)
        {
            insertData();
            Close();
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        private void txtRefercode_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                gethosp();
                txtdx.Focus();
            }
            else
            {
            }
        }
        private void PrintDocument1_PrintPage(System.Object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            int NumRow, i, Lines;
            i = 0;

            Lines = 140;
            AnyString2(e.Graphics, "ใบสั่งยา", 65, 20);
            AnyString2(e.Graphics, "โรงพยาลบาลตาลสุม", 60, 40);
            AnyString2(e.Graphics, "--------------------------------------------------------------", 7, 110);
            AnyString2(e.Graphics, "รายการ                                ราคา    จำนวน        หน่วยนับ", 10, 120);
            AnyString2(e.Graphics, "--------------------------------------------------------------", 7, 130);            
            AnyString2(e.Graphics, "--------------------------------------------------------------", 7, Lines);
            AnyString2(e.Graphics, "--------------------------------------------------------------", 7, Lines + 40);       
            AnyString2(e.Graphics, "โปรดเก็บไวเป็นหลักฐาน  ขอบคุณ(Thank You)", 20, Lines + 120);
        }
        Font UseFont2 = new Font("MS Sans Serif", 11);
        private void AnyString2(Graphics g, string printString, int xPos, int yPos)
        {
            PointF anyPoint = new PointF(xPos, yPos);
            g.DrawString(printString, UseFont2, System.Drawing.Brushes.Black, anyPoint);
        }
        private void btnprint_Click(object sender, RoutedEventArgs e)
        {         
            PrintDocument pd = new PrintDocument();
            pd.PrintPage += new PrintPageEventHandler
               (this.PrintDocument1_PrintPage);
            pd.Print();
        }
        private void frmreferout_Loaded(object sender, RoutedEventArgs e)
        {
            Hi7.Class.APIConnect.getConfgXML();          
            CultureInfo.GetCultureInfo("en-US");
            //String hns = HI7.Class.HIUility.HNR;
            //String refs = HI7.Class.HIUility.RefNo;
            //String vns = HI7.Class.HIUility.Vnp;
            //txtSearchHn.Text = hns;
            //txtorfrno.Text = refs;
            getl_rfrtype();
            getcln();
            getrfrcs();
            getl_load();
            this.txtrname.Text = string.Empty;
            this.cbbdep.Text = string.Empty;
            this.cbbsendrefer.Text = string.Empty;
            this.txtsahad.Text = string.Empty;
            this.cbbl_loads.Text = string.Empty;
            this.txtorfrno.Text = string.Empty;
            this.txtSearchHn.Focus();
        }
    }
}
