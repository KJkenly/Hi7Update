using HandyControl.Controls;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
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
using Window = System.Windows.Window;

namespace Mdr.Forms
{
    /// <summary>
    /// Interaction logic for frmReferins.xaml
    /// </summary>
    public partial class frmReferins : Window
    {
        public frmReferins()
        {
            InitializeComponent();
        }
        private void ClickClose(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        string hn, txvn, txhn, sendtypech,emscodech,emsnamech, vstdttm;

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

        void getpt()
        {
            hn = txtHn.Text;
            DataRow dr;
            DataTable dt = new DataTable();
            Dictionary<string, object> dictData = new Dictionary<string, object>();
            dictData.Add("query", "select ovst.vn,pt.hn,pt.fname,pt.lname,CONCAT(DATE_FORMAT(ovst.vstdttm,'%Y')+543,'-',DATE_FORMAT(ovst.vstdttm,'%m-%d %H:%m:%s')) AS vstdttm from ovst inner join pt on ovst.hn=pt.hn where pt.hn= " + "'"+ hn +"'" + " and date(ovst.vstdttm) = " + "curdate()" );
            dt = Hi7.Class.APIConnect.getDataTable("query/hi7_query", "POST", dictData);

            if (dt != null)
            {
                if (dt.Rows.Count == 1)
                {
                    dr = dt.Rows[0];
                    txvn = dr["vn"].ToString();
                    txhn = dr["hn"].ToString();
                    txtfullname.Text = (string)dr["fname"];
                    //txtlname.Content = (string)dr["lname"];
                    vstdttm = (string)dr["vstdttm"].ToString();
                    txtDatevstdttm.Text = (string)dr["vstdttm"].ToString();
                    cbbrefer.Focus();
                }
                else if (dt.Rows.Count > 1) {
                    frmpopreferin frmpuprefin = new frmpopreferin();
                    frmpuprefin.Show();
                    dr = dt.Rows[0];
                    txvn = dr["vn"].ToString();
                    txhn = dr["hn"].ToString();
                    txtfullname.Text = (string)dr["fname"];
                    //txtlname.Content = (string)dr["lname"];
                    vstdttm = (string)dr["vstdttm"];
                    txtDatevstdttm.Text = (string)dr["vstdttm"];
                    cbbrefer.Focus();
                }
                else
                {
                    Growl.Warning("ไม่เจอข้อมูล HN รายนี้");
                    txtHn.Focus();
                }
            }
            else
            {
                Growl.Warning("ไม่เจอข้อมูล HN รายนี้");
            }
        }
        String strDate3 ,strtime;
        void dtpp() {
        string iDate = DTaccdate.SelectedDateTime.ToString();
        DateTime oDate = DateTime.Parse(iDate);
            //แปลงวันที่ 2566-01-01 เป็น 2023-01-01
        strDate3 = HI7.Class.HIUility.DateChange3(oDate.ToString("yyyy-MM-dd"));
            string changeTime = oDate.TimeOfDay.ToString("hhmm");
        strtime = HI7.Class.HIUility.TimeNumeric(changeTime);
            //MessageBox.Show(strDate3);
            //MessageBox.Show(strtime);
        }
    void getpt2()
        {
            hn = txtHn.Text;
            DataRow dr;
            DataTable dt = new DataTable();
            Dictionary<string, object> dictData = new Dictionary<string, object>();
            dictData.Add("query", "select pt.hn,pt.fname,pt.lname from ovst inner join pt on ovst.hn=pt.hn where pt.hn= " + hn);
            try
            {
                dt = Hi7.Class.APIConnect.getDataTable("query/hi7_query", "POST", dictData);
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        dr = dt.Rows[0];
                        txtfullname.Text = (string)dr["fname"];
                        //txtlname.Content= (string)dr["lname"];
                        cbbrefer.Focus();
                    }
                }
                else
                {
                    Growl.Warning("ไม่สามารถบันทึกข้อมูลได้ กรุณาลองใหม่");
                }
            }
            catch (Exception ex)
            {

            }
        }
        void getl_ems()
        {
            DataTable dt = new DataTable();
            Dictionary<string, object> dictData = new Dictionary<string, object>();
            dictData.Add("query", "select emscode,nameems from l_emscode");
            try
            {
                dt = Hi7.Class.APIConnect.getDataTable("query/hi7_query", "POST", dictData);
                if (dt != null)
                {
                    string[] selectedColumns = new[] { "emscode", "nameems" };//ชื่อคอลัมน์
                    System.Data.DataView view = new System.Data.DataView(dt);
                    System.Data.DataTable selected = view.ToTable("Selected", false, selectedColumns);
                    this.txtEmsname.DisplayMemberPath = "nameems";
                    this.txtEmsname.SelectedValuePath = "emscode";
                    this.txtEmsname.ItemsSource = selected.DefaultView;
                    this.txtEmsname.SelectedIndex = 0;
                }
                else
                {
                    // MessageBox.Show("ไม่พบicd10");
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
                    Growl.Warning("ไม่พบicd10");
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
                        txtdxname.Text = dr["icd10name"].ToString();
                    }
                }
                else
                {
                    Growl.Warning("ไม่พบicd10");
                }
            }
            catch (Exception ex)
            {
            }
        }
        private bool insertData()
        {

            getcheckradio();
            dtpp();
            string dateserver = HI7.Class.HIUility.Getdateserver();
            string ovstdate = HI7.Class.HIUility.CBE2D(dateserver);
            DataTable dt = new DataTable();
            Dictionary<string, object> dictData = new Dictionary<string, object>();
            string strfields, strValues;
            strfields = "vn,rfrno,rfrlct,icd10,rfrcs,accdate, acctime,sendtype,emsnote,emscode,emsname,rfrtype,refer_no ";
            string vn, rfrno, rfrlct, icd10, rfrcs, accdate, acctime, sendtype, emsnote, emscode, emsname, rfrtype, refer_no;
            vn = frmMdr._VNGETDATAGRID;
            rfrno = txtNo.Text;
            rfrlct = txtrefer.Text;
            icd10 = txtdx.Text;
            rfrcs = txtsahad.SelectedValue.ToString();
            accdate = ovstdate;
            acctime = strtime;
            sendtype = sendtypech;
            emsnote = "Hi7 note";
            emscode = emscodech;
            emsname = emsnamech;
            rfrtype = cbbdep.SelectedValue.ToString();
            refer_no = txtNosmartrefer.Text;
            strValues = "'" + vn + "'" +
                        ",'" + rfrno + "'" +
                        ",'" + rfrlct + "'" +
                         ",'" + icd10 + "'" +
                        ",'" + rfrcs + "'" +
                        ",'" + accdate + "'" +
                        ",'" + acctime + "'" +
                        ",'" + sendtype + "'" +
                        ",'" + emsnote + "'" +
                         ",'" + emscode + "'" +
                          ",'" + emsname + "'" +
                     ",'" + rfrtype + "'" +
                        ",'" + refer_no + "'";

            string strSQL = "insert into orfri(" + strfields + ") values(" + strValues + ") ";
            //MessageBox.Show(strSQL);
            //txtdx.Text = strSQL;
            dictData.Add("query", strSQL);
            bool status = Hi7.Class.APIConnect.Excecute("query/hi7_query", "POST", dictData);
            if (status)
            {
                //Growl.Warning("บันทึกข้อมูลสำเร็จ");
                System.Windows.MessageBox.Show("บันทึกตอบรับ Refer in สำเร็จ");
                return true;
                strSQL = "";
                this.Close();
            }
            else
                return false;
        }

        private void btnClickSave(object sender, RoutedEventArgs e)
        {
            insertData();
            this.Close();
        }
        private void frmReferin_Loaded(object sender, RoutedEventArgs e)
        {

            Hi7.Class.APIConnect.getConfgXML();  
            CultureInfo.GetCultureInfo("en-US");
            // getToken();
            DTaccdate.SelectedDateTime = DateTime.Now;
            getl_rfrtype();
            getrfrcs();
            getl_ems();
            rbtn1.IsChecked = true;
            txtHn.Text = frmMdr._HNGETDATAGRID;
            txtfullname.Text = frmMdr._FullnameDATAGRID;
        }

        private void txtHn_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {

                
                getpt();
                txtrefer.Focus();



            }
            else
            {
            }
        }
        private void txtdx_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                getdx();
                cbbdep.Focus();
            }
            else
            {
            }
        }
        string off_id;
        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        private void test_Click(object sender, RoutedEventArgs e)
        {
            dtpp();
        }
        private void txtNo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                DTaccdate.Focus();
            }
            else
            {
            }
        }
        private void txtNosmartrefer_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {

                
            txtdx.Focus();

            }
            else
            {

            } 
        }
        void getcheckradio()
        {
            if (rbtn1.IsChecked == true) {
                sendtypech = "1";
            } else if(rbtn2.IsChecked == true)
                {
                sendtypech = "2";
            }
            else if (rbtn3.IsChecked == true)
            {
                sendtypech = "3";
                emscodech = txtEmsname.SelectedValue.ToString();
                emsnamech = txtEmsname.Text.ToString();
            }

        }
            private void dtp1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                txtNosmartrefer.Focus();
            }
            else
            {
            }
        }
        void gethosp()
        {
            off_id = txtrefer.Text;
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
                        cbbrefer.Text = dr["namehosp"].ToString();
                    }
                }
                else
                {
                    Growl.Warning("ไม่พบข้อมูลสถานบริการ");
                }
            }
            catch (Exception ex)
            {

            }
        }
        private void txtrefer_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                gethosp();
                txtNo.Focus();
            }
            else
            {
            }
        }
    }
}


  
