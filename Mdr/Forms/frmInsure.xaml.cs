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
    /// Interaction logic for frmInsure.xaml
    /// </summary>
    public partial class frmInsure : Window
    {
        //public static string callnote, calldatein, calldateexp,callhospmain,callsubmain;
        public static string hninsure = "";
        public frmInsure()
        {
            InitializeComponent();
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Hi7.Class.APIConnect.getConfgXML();
            if (Forms.frmMdr.Statuschangeinsure == "0")//ลงสิทธิ์แบบปกติ
            {
                this.getData();
                this.getDatainsure(Forms.frmMdr.PTHN);
                getPttype();
                txtNote.Focus();
                btnsave.Visibility = Visibility.Visible;
                btnEdit.Visibility = Visibility.Collapsed;
            }
            else if(Forms.frmMdr.Statuschangeinsure == "1")//แก้ไขสิทธิ์ขณะให้บริการ
            {
                if (!string.IsNullOrEmpty(Forms.frmMdr.idinsure))
                {
                    lbtxtidpttype.Content = "หมายเลขสิทธิ์";
                    lbtxtnamepttype.Content = "สิทธิ์ขณะรับบริการ";
                    spsubpttype.Visibility = Visibility.Collapsed;
                    spsubpttypename.Visibility = Visibility.Collapsed;

                    //clickbk = "1";
                    //datepicker = "";
                    //string iDate = datetoday.Text;
                    //DateTime oDate = DateTime.Parse(iDate);
                    //datepicker = DateConvert(oDate.ToString("yyyy-MM-dd"));
                    //datebackvisit = "o" + oDate.ToString("ddMMyy");
                    if (Forms.frmMdr.clickbk == "1")//เพิ่มเงื่อนไขแก้สิทธิ์ย้อนหลังได้
                    {
                        lbtxtnamepttype.Content = "เปลี่ยนสิทธิ์(ย้อนหลัง)";
                        getDatachanginsure(Forms.frmMdr.idinsure);
                        btnsave.Visibility = Visibility.Collapsed;
                        btnEdit.Content = "แก้ไขสิทธิ์ย้อนหลัง";
                        btnEdit.Visibility = Visibility.Visible;
                    }
                    else//เปลี่ยนสิทธิขณะให้บริการ
                    {
                        getDatachanginsure(Forms.frmMdr.idinsure);
                        //this.txtIDCard.Text = Mdr.Forms.frmMdr.PTCID;
                        //this.txtFullname.Text = Forms.frmMdr.PTFULLNAME;
                        //this.txtPttypenumber.Text = Forms.frmMdr.PTTYPECODE;
                        //this.cbbPttypename.Text = Forms.frmMdr.PTTYPE;
                        btnsave.Visibility = Visibility.Collapsed;
                        btnEdit.Visibility = Visibility.Visible;
                    }

                    

                    //Forms.frmMdr.Statuschangeinsure = "0";
                }

            }
                        
        }
        //เปลี่ยน data insure
        void getDatachanginsure(string idinsure)
        {
            hninsure = "";
            string strSql = "SELECT insure.id,insure.hn,insure.pop_id,insure.card_id,insure.pttype,CONCAT('(',insure.pttype,')',pttype.namepttype) as namepttype,DATE_FORMAT(insure.datein, '%Y-%m-%d') as datein,DATE_FORMAT(insure.dateexp, '%Y-%m-%d') as dateexp,insure.hospmain,insure.hospsub,insure.note,insure.notedate,insure.vn FROM insure LEFT JOIN pttype ON pttype.pttype = insure.pttype WHERE insure.id = " + idinsure;
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
                        hninsure = dr["hn"].ToString();
                        this.txtIDCard.Text = dr["pop_id"].ToString();
                        this.txtFullname.Text = Forms.frmMdr._FullnameDATAGRID;
                        GetnoteInsure();
                        this.txtNumbercardid.Text = dr["card_id"].ToString();
                        this.txtPttypenumber.Text = dr["pttype"].ToString();
                        this.cbbPttypename.Text = dr["namepttype"].ToString();
                        string dateinc = dr["datein"].ToString();
                        string dateexpc = dr["dateexp"].ToString();
                        if (!string.IsNullOrEmpty(dateinc))
                        {
                            if (dateinc == "0000-00-00")
                            {
                                this.dStartdate.Text = "00-00-0000";
                            }
                            else
                            {
                                try
                                {
                                    string calldatein = HI7.Class.HIUility.D2CBE(dr["datein"].ToString());
                                    this.dStartdate.Text = calldatein;
                                }
                                catch { }                               
                            }
                        }
                        else
                        {
                            this.dStartdate.Text = "00/00/0000";

                        }
                        if (!string.IsNullOrEmpty(dateexpc))
                        {
                            if (dateexpc == "0000-00-00")
                            {
                                this.dEnddate.Text = "00-00-0000";
                            }
                            else
                            {
                                try
                                {
                                    string calldatein = HI7.Class.HIUility.D2CBE(dr["dateexp"].ToString());
                                    this.dEnddate.Text = calldatein;
                                }
                                catch { }
                            }
                        }
                        else
                        {
                            this.dEnddate.Text = "00/00/0000";

                        }                      
                        string hospmain = dr["hospmain"].ToString();
                        string hospsub = dr["hospsub"].ToString();
                        this.txtMainhostfind.Text = hospmain;
                        this.txtSubhostfind.Text = hospsub;
                        this.cbbMainhost.Text = Hospname(hospmain);
                        this.cbbSubhost.Text = Hospname(hospsub);
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
        void getDatainsure(string namepttype)
        {
            string strNamepttype = namepttype;
            string strSql = "SELECT insure.id,insure.hn,insure.pop_id,insure.card_id,insure.pttype,DATE_FORMAT(insure.datein, '%Y-%m-%d') as datein,DATE_FORMAT(insure.dateexp, '%Y-%m-%d') as dateexp,insure.hospmain,insure.hospsub,insure.note,insure.notedate,insure.vn FROM insure WHERE insure.hn =" + "'" + Forms.frmMdr.PTHN + "'" + " AND insure.pttype = " + "'" + Forms.frmMdr.PTTYPECODE + "'" + "ORDER BY insure.id desc LIMIT 1";
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
                        GetnoteInsure();
                        //this.txtNote.Text = dr["note"].ToString();
                        this.txtNumbercardid.Text = dr["card_id"].ToString();
                        string dateinc = dr["datein"].ToString();
                        string dateexpc = dr["dateexp"].ToString();
                        try {
                            if (dateinc == "0000-00-00") {
                                this.dStartdate.Text = "00/00/0000";
                            }
                            else {
                                string calldatein = HI7.Class.HIUility.D2CBE(dr["datein"].ToString());
                                this.dStartdate.Text = calldatein;
                            }                                                      
                        } catch {
                            this.dStartdate.Text = "00/00/0000";
                        }
                        try {                            
                            if(dateexpc == "0000-00-00")
                            {
                                this.dEnddate.Text = "00/00/0000";
                            }
                            else
                            {
                                string calldateout = HI7.Class.HIUility.D2CBE(dr["dateexp"].ToString());
                                this.dEnddate.Text = calldateout;
                            }
                        } catch {
                            this.dEnddate.Text = "00/00/0000";
                        }
                        string hospmain = dr["hospmain"].ToString();
                        string hospsub = dr["hospsub"].ToString();
                        this.txtMainhostfind.Text = hospmain;
                        this.txtSubhostfind.Text = hospsub;
                        this.cbbMainhost.Text = Hospname(hospmain);
                        this.cbbSubhost.Text = Hospname(hospsub);
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
        //หา Note ล่าสุด
        private void GetnoteInsure()
        {
            txtNote.Text = string.Empty;
            string strSql = "SELECT insure.pttype,CONCAT('(',insure.pttype,')',pttype.namepttype)as namepttype,insure.note" +
                            " FROM insure" +
                            " LEFT JOIN pttype on pttype.pttype = insure.pttype" +
                            " WHERE insure.hn =" + hninsure+
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
                        this.txtNote.Text = dr.ItemArray[2].ToString();
                    }
                    else
                    {
                        this.txtNote.Text = "";
                    }
                }
                else
                {
                    this.txtNote.Text = "";
                }
            }
            catch (Exception ex)
            {
                this.txtNote.Text = "";
            }
        }
        //แปลงวันที่จากฐาน

        public static string Hospname(string hospname)
        {
            string strSql = "SELECT hospcode.namehosp FROM hospcode WHERE hospcode.off_id = '" + hospname + "'";
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
                        string strname = dr["namehosp"].ToString();
                        return strname;

                    }
                    else
                    {
                        return "";
                    }
                }
                else
                {
                    return "";
                }

            }
            catch (Exception ex)
            {
                return "";
            }

        }

        //private void ClickClose(object sender, RoutedEventArgs e)
        //{
        //    this.Close();
        //}

        //private void ClickMin(object sender, RoutedEventArgs e)
        //{
        //    this.WindowState = WindowState.Minimized;

        //}
        void getData()
        {
            this.txtIDCard.Text = Mdr.Forms.frmMdr.PTCID;
            this.txtFullname.Text = Forms.frmMdr.PTFULLNAME;
            this.txtPttypenumber.Text = Forms.frmMdr.PTTYPECODE;
            this.cbbPttypename.Text = Forms.frmMdr.PTTYPE;
            
        }
        void gethospmain()
        {
            DataRow dr;
            Dictionary<string, object> dictData = new Dictionary<string, object>();
            string strSql = "select off_id,namehosp from hospcode where off_id= " + txtMainhostfind.Text;
            DataTable dt = new System.Data.DataTable();
            dictData.Add("query", strSql);
            try
            {
                dt = Hi7.Class.APIConnect.getDataTable("query/hi7_query", "POST", dictData);
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        dr = dt.Rows[0];
                        cbbMainhost.Text = dr["namehosp"].ToString();
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
        void gethospsub()
        {
            DataRow dr;
            DataTable dt = new DataTable();
            Dictionary<string, object> dictData = new Dictionary<string, object>();
            dictData.Add("query", "select off_id,namehosp from hospcode where off_id= " + txtSubhostfind.Text);
            try
            {
                dt = Hi7.Class.APIConnect.getDataTable("query/hi7_query", "POST", dictData);
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        dr = dt.Rows[0];
                        cbbSubhost.Text = dr["namehosp"].ToString();
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

        private void txtMainhostfind_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.Key == Key.Enter)
            {
                gethospmain();
            }
            else
            {

            }
        }

        private void txtSubhostfind_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                gethospsub();
            }
            else
            {

            }
        }

        private void btnsave_Click(object sender, RoutedEventArgs e)
        {
            InsertInsure();
            Forms.frmMdr.PTHN = "";
            Forms.frmMdr.PTCID = "";
            Forms.frmMdr.PTTYPE = "";
            Forms.frmMdr.PTFULLNAME = "";
            Mdr.Forms.frmMdr.josnSendInsure = "";
            Mdr.Forms.frmMdr.contentInsure = "";
            this.Close();
        }
        bool InsertInsure()
        {
            //string checkdatein = JObject.Parse(jsonsmartAgent).SelectToken("mainInscl").ToString();
            string vn,hn, pop_id, card_id, pttype, datein, dateexp, hospmain, hospsub, note, notedate;
            //DateTime dtstartcon,dtendcon; 
            vn = HI7.Class.HIUility.GETVN;
            hn = Forms.frmMdr.PTHN;
            pop_id = Forms.frmMdr.PTCID;
            card_id = txtNumbercardid.Text;
            pttype = Forms.frmMdr.PTTYPECODE;
            //pttype = txtPttypenumber.Text;
            DateTime dateTime = DateTime.Now;
            datein = HI7.Class.HIUility.DateChange6(this.dStartdate.Text);
            dateexp = HI7.Class.HIUility.DateChange6(this.dEnddate.Text);
            hospmain = txtMainhostfind.Text;
            hospsub = txtSubhostfind.Text;
            if (string.IsNullOrEmpty(txtNote.Text))
            {
                note = "บันทึกข้อมูลโดย SmartServiceICT";
            }
            else
            {
                note = txtNote.Text;
            }
            notedate = dateTime.ToString("yyyy-MM-dd", CultureInfo.GetCultureInfo("en-US"));
            //string strTable = "o" + dateTime.ToString("ddMMyy", CultureInfo.GetCultureInfo("th-TH"));
            string strField = "hn,vn,pop_id,card_id,pttype,datein,dateexp,hospmain,hospsub,note,notedate";
            string strValues = "'" + hn + "'," + "'" + vn + "'," + "'" + pop_id + "'," + "'" + card_id + "'," + "'" + pttype + "'," + "'" + datein + "'," + "'" + dateexp + "',"
                + "'" + hospmain + "'," + "'" + hospsub + "'," + "'" + note + "'," + "'" + notedate + "'";
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
        void getPttype()
        {
            DataTable dt = new DataTable();


            Dictionary<string, object> dictData = new Dictionary<string, object>();
            string strSQL = "select pttype,concat('(',pttype,')',' ',namepttype) AS 'namepttype' from pttype order by pttype ASC";
            dictData.Add("query", strSQL);

            try
            {
                dt = Hi7.Class.APIConnect.getDataTable("query/hi7_query", "POST", dictData);
                if (dt != null)
                {
                    string[] selectedColumns = new[] { "namepttype", "pttype" };
                    System.Data.DataView view = new System.Data.DataView(dt);
                    System.Data.DataTable selected = view.ToTable("Selected", false, selectedColumns);
                    this.cbbPttypename.DisplayMemberPath = "namepttype";
                    this.cbbPttypename.SelectedValuePath = "pttype";
                    this.cbbPttypename.ItemsSource = selected.DefaultView;
                }
            }
            catch (Exception ex)
            {

            }

        }

        private void txtPttypenumber_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                try
                {
                    if (this.txtPttypenumber.Text == "")
                    {
                        Growl.Warning("กรุณาระบุการค้นหาด้วยรหัสสิทธิ์!!");
                        this.getPttypeId(txtPttypenumber.Text);
                        //this.cbbPttypename.Text = "";
                    }
                    else
                    {
                        try
                        {
                            this.getPttypeId(txtPttypenumber.Text);
                            
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
        void getPttypeId(string pttype)
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
                        this.cbbPttypename.Text = (string)dr["namepttype"];
                    }
                    else if (dt.Rows.Count > 1)
                    {
                        Growl.Warning("กรุณาเลือกสิทธิ์");
                        HI7.Class.HIUility._TXTSEARCHPTTYPE = pttype;
                        frmPopuppttype frmPopuppttype = new frmPopuppttype();
                        Mdr.Forms.frmMdr.txtPttype = pttype;
                        frmPopuppttype.ShowDialog();
                        cbbPttypename.Text = Forms.frmMdr.dataPttype;
                        txtPttypenumber.Text = Forms.frmMdr.idPttype;
                    }
                    else if (dt.Rows.Count == 0)
                    {
                        Growl.Warning("ไม่พบรายการสิทธิ์ที่ค้นหา");
                        HI7.Class.HIUility._TXTSEARCHPTTYPE = pttype;
                        frmPopuppttype frmPopuppttype = new frmPopuppttype();
                        Mdr.Forms.frmMdr.txtPttype = pttype;
                        frmPopuppttype.ShowDialog();
                        cbbPttypename.Text = Forms.frmMdr.dataPttype;
                        txtPttypenumber.Text = Forms.frmMdr.idPttype;
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

        private void cbbPttypename_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                if (cbbPttypename.SelectedValue != null)
                {
                    string valuepttype = cbbPttypename.SelectedValue.ToString();
                    this.txtPttypenumber.Text = valuepttype;
                }
                else
                {
                    this.txtPttypenumber.Text = "";
                }

            }
            catch
            {

            }
        }

        private void Border_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                this.DragMove();
            }
        }

        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string strvn = " vn = '" + Forms.frmMdr._VNGETDATAGRID + "'";
                string strSQL;
                strSQL = "UPDATE insure set vn = " + "'" + Forms.frmMdr._VNGETDATAGRID + "',card_id = '" + txtNumbercardid.Text + "',pttype = '" + txtPttypenumber.Text +
                    "',datein = '" + HI7.Class.HIUility.DateChange6(dStartdate.Text) + "',dateexp = '" + HI7.Class.HIUility.DateChange6(dEnddate.Text) + "',hospmain = '" + txtMainhostfind.Text + "',hospsub = '" + txtSubhostfind.Text + "',note = '" + txtNote.Text +
                    "' WHERE id = '" + Forms.frmMdr.idinsure + "'";

                Dictionary<string, object> dictData = new Dictionary<string, object>();
                dictData.Add("query", strSQL);
                bool status = Hi7.Class.APIConnect.Excecute("query/hi7_query", "POST", dictData);
                if (status == true)
                {
                    try { updateOvst(); } catch { }
                    //แฟ้ม รายวัน สิทธิ์
                    try { updateOtoday(); } catch { }
                    //แฟ้มผู้ป่วนใน สิทธิ์
                    try { updateIpd(); } catch { }
                    //แฟ้มเเก็บเงิน สิทธิ์
                    try { updateRcpt(); } catch { }
                    //แฟ้มหมวดเก็บเงิน สิทธิ์
                    try { updateIncoth(); } catch { }
                    //แฟ้มสั่งแล็บ สิทธิ์
                    try { updateLbbk(); } catch { }
                    //แฟ้มทันตกรรม สิทธิ์
                    try { updateDt(); } catch { }
                    //แฟ้มfp สิทธิ์
                    try { updateFp(); } catch { }
                    //แฟ้มEpi สิทธิ์
                    try { updateEpi(); } catch { }
                    //แฟ้มAnc สิทธิ์
                    try { updateAnc(); } catch { }
                    //แฟ้มPrsc สิทธิ์
                    try { updatePrsc(); } catch { }
                    //แฟ้ม PT
                    try { updatePt(); }catch { }
                }
            }
            catch
            {
                Growl.Error("เกิดข้อผิดพลาดไม่สามารถบันทึกได้ insure");
                Forms.frmMdr._VNGETDATAGRID = "";
                Forms.frmMdr.idinsure = "";
                Forms.frmMdr.Statuschangeinsure = "0";
                this.Close();
            }
            Forms.frmMdr._VNGETDATAGRID = "";
            Forms.frmMdr.idinsure = "";
            Forms.frmMdr.Statuschangeinsure = "0";            
            this.Close();
        }
        bool updateOvst()//update อัพเดตคลินิกและสิทธิ์
        {
            try
            {
                string strSQL, strValues = "";
                strValues = "pttype = '" + txtPttypenumber.Text + "'";

                strSQL = "UPDATE ovst set " + strValues + " WHERE vn = '" + Forms.frmMdr._VNGETDATAGRID + "'";
                Dictionary<string, object> dictData = new Dictionary<string, object>();
                dictData.Add("query", strSQL);
                bool status = Hi7.Class.APIConnect.Excecute("query/hi7_query", "POST", dictData);
                if (status)
                {
                    Growl.Success("แก้ไขสิทธิ์ OVST " + txtPttypenumber.Text + "แล้วครับ");
                    return true;
                }
                else
                {
                    Growl.Error("Error update OVST");
                    return false;
                }
            }
            catch (Exception ex)
            {
                Growl.Error("updateOvstCln UPDATE say:\r\n" + ex.Message);
                return false;
            }
        }
        bool updateOtoday()//update เปลี่ยนสิทธิ์ oXXXXXXXX
        {
            try
            {
                string strSQL, strValues = "", strTable = "";
                DateTime dateTime = DateTime.Now;
                if(Forms.frmMdr.clickbk == "1")
                {
                    strTable = Forms.frmMdr.datebackvisit;
                }
                else
                {
                    strTable = "o" + dateTime.ToString("ddMMyy", CultureInfo.GetCultureInfo("th-TH"));
                }
                
                strValues = "pttype = '" + txtPttypenumber.Text + "'";
                strSQL = "UPDATE " + strTable + " set " + strValues + " WHERE vn = '" + Forms.frmMdr._VNGETDATAGRID + "'";
                Dictionary<string, object> dictData = new Dictionary<string, object>();

                dictData.Add("query", strSQL);
                bool status = Hi7.Class.APIConnect.Excecute("query/hi7_query", "POST", dictData);
                if (status)
                {
                    Growl.Success("เปลี่ยนสิทธิ์ขณะให้บริการสำเร็จ Otoday " + txtPttypenumber.Text + "แล้วครับ");
                    return true;
                }
                else
                {

                    Growl.Error("Error update change pttype in " + strTable);
                    return false;
                }
            }
            catch (Exception ex)
            {
                Growl.Error("updateOvstCln UPDATE say:\r\n" + ex.Message);
                return false;
            }
        }
        bool updateIpd()
        {
            try
            {
                string strSQL, strValues;
                strValues = "pttype = '" + txtPttypenumber.Text + "'";
                strSQL = "UPDATE ipt set " + strValues + " WHERE vn = '" + Forms.frmMdr._VNGETDATAGRID + "'";
                Dictionary<string, object> dictData = new Dictionary<string, object>();
                dictData.Add("query", strSQL);
                bool status = Hi7.Class.APIConnect.Excecute("query/hi7_query", "POST", dictData);
                if (status)
                {
                    return true;

                }
                else
                {
                    Growl.Error("Error update ipt");
                    return false;
                }
            }
            catch (Exception ex)
            {
                Growl.Error("UpdateIpd UPDATE say:\r\n" + ex.Message);
                return false;
            }
        }
        bool updateRcpt()
        {
            try
            {
                string strSQL, strValues;
                strValues = "pttype = '" + txtPttypenumber.Text + "'";
                strSQL = "UPDATE rcpt set " + strValues + " WHERE vn = '" + Forms.frmMdr._VNGETDATAGRID + "'";
                Dictionary<string, object> dictData = new Dictionary<string, object>();
                dictData.Add("query", strSQL);
                bool status = Hi7.Class.APIConnect.Excecute("query/hi7_query", "POST", dictData);
                if (status)
                {
                    return true;

                }
                else
                {
                    Growl.Error("Error update rcpt");
                    return false;
                }
            }
            catch (Exception ex)
            {
                Growl.Error("updateRcpt UPDATE say:\r\n" + ex.Message);
                return false;
            }
        }
        bool updateIncoth()
        {
            try
            {
                string strSQL, strValues;
                strValues = "pttype = '" + txtPttypenumber.Text + "'";
                strSQL = "UPDATE incoth set " + strValues + " WHERE vn = '" + Forms.frmMdr._VNGETDATAGRID + "'";
                Dictionary<string, object> dictData = new Dictionary<string, object>();
                dictData.Add("query", strSQL);
                bool status = Hi7.Class.APIConnect.Excecute("query/hi7_query", "POST", dictData);
                if (status)
                {
                    return true;

                }
                else
                {
                    Growl.Error("Error update incoth");
                    return false;
                }
            }
            catch (Exception ex)
            {
                Growl.Error("updateIncoth UPDATE say:\r\n" + ex.Message);
                return false;
            }
        }
        bool updateLbbk()
        {
            try
            {
                string strSQL, strValues;
                strValues = "pttype = '" + txtPttypenumber.Text + "'";
                strSQL = "UPDATE lbbk set " + strValues + " WHERE vn = '" + Forms.frmMdr._VNGETDATAGRID + "'";
                Dictionary<string, object> dictData = new Dictionary<string, object>();
                dictData.Add("query", strSQL);
                bool status = Hi7.Class.APIConnect.Excecute("query/hi7_query", "POST", dictData);
                if (status)
                {
                    return true;

                }
                else
                {
                    Growl.Error("Error update lbbk");
                    return false;
                }
            }
            catch (Exception ex)
            {
                Growl.Error("updateLbbk UPDATE say:\r\n" + ex.Message);
                return false;
            }
        }
        bool updateDt()
        {
            try
            {
                string strSQL, strValues;
                strValues = "pttype = '" + txtPttypenumber.Text + "'";
                strSQL = "UPDATE dt set " + strValues + " WHERE vn = '" + Forms.frmMdr._VNGETDATAGRID + "'";
                Dictionary<string, object> dictData = new Dictionary<string, object>();
                dictData.Add("query", strSQL);
                bool status = Hi7.Class.APIConnect.Excecute("query/hi7_query", "POST", dictData);
                if (status)
                {
                    return true;

                }
                else
                {
                    Growl.Error("Error update dt");
                    return false;
                }
            }
            catch (Exception ex)
            {
                Growl.Error("updateDt UPDATE say:\r\n" + ex.Message);
                return false;
            }
        }
        bool updateFp()
        {
            try
            {
                string strSQL, strValues;
                strValues = "pttype = '" + txtPttypenumber.Text + "'";
                strSQL = "UPDATE fp set " + strValues + " WHERE vn = '" + Forms.frmMdr._VNGETDATAGRID + "'";
                Dictionary<string, object> dictData = new Dictionary<string, object>();
                dictData.Add("query", strSQL);
                bool status = Hi7.Class.APIConnect.Excecute("query/hi7_query", "POST", dictData);
                if (status)
                {
                    return true;

                }
                else
                {
                    Growl.Error("Error update fp");
                    return false;
                }
            }
            catch (Exception ex)
            {
                Growl.Error("updateFp UPDATE say:\r\n" + ex.Message);
                return false;
            }
        }
        bool updateEpi()
        {
            try
            {
                string strSQL, strValues;
                strValues = "pttype = '" + txtPttypenumber.Text + "'";
                strSQL = "UPDATE epi set " + strValues + " WHERE vn = '" + Forms.frmMdr._VNGETDATAGRID + "'";
                Dictionary<string, object> dictData = new Dictionary<string, object>();
                dictData.Add("query", strSQL);
                bool status = Hi7.Class.APIConnect.Excecute("query/hi7_query", "POST", dictData);
                if (status)
                {
                    return true;

                }
                else
                {
                    Growl.Error("Error update epi");
                    return false;
                }
            }
            catch (Exception ex)
            {
                Growl.Error("updateEpi UPDATE say:\r\n" + ex.Message);
                return false;
            }
        }
        bool updateAnc()
        {
            try
            {
                string strSQL, strValues;
                strValues = "pttype = '" + txtPttypenumber.Text + "'";
                strSQL = "UPDATE anc set " + strValues + " WHERE vn = '" + Forms.frmMdr._VNGETDATAGRID + "'";
                Dictionary<string, object> dictData = new Dictionary<string, object>();
                dictData.Add("query", strSQL);
                bool status = Hi7.Class.APIConnect.Excecute("query/hi7_query", "POST", dictData);
                if (status)
                {
                    return true;

                }
                else
                {
                    Growl.Error("Error update anc");
                    return false;
                }
            }
            catch (Exception ex)
            {
                Growl.Error("updateAnc UPDATE say:\r\n" + ex.Message);
                return false;
            }
        }
        bool updatePrsc()
        {
            try
            {
                string strSQL, strValues;
                strValues = "pttype = '" + txtPttypenumber.Text + "'";
                strSQL = "UPDATE prsc set " + strValues + " WHERE vn = '" + Forms.frmMdr._VNGETDATAGRID + "'";
                Dictionary<string, object> dictData = new Dictionary<string, object>();
                dictData.Add("query", strSQL);
                bool status = Hi7.Class.APIConnect.Excecute("query/hi7_query", "POST", dictData);
                if (status)
                {
                    return true;
                }
                else
                {
                    Growl.Error("Error update prsc");
                    return false;
                }
            }
            catch (Exception ex)
            {
                Growl.Error("updatePrsc UPDATE say:\r\n" + ex.Message);
                return false;
            }
        }
        bool updatePt()
        {
            try
            {

                string strSQL, strValues;
                strValues = "register = '" + frmMdr.strstaff + "'";
                strSQL = "UPDATE pt set " + strValues + " WHERE hn = '" + hninsure + "'";
                Dictionary<string, object> dictData = new Dictionary<string, object>();
                dictData.Add("query", strSQL);
                bool status = Hi7.Class.APIConnect.Excecute("query/hi7_query", "POST", dictData);
                if (status)
                {
                    return true;
                }
                else
                {
                    Growl.Error("Error update prsc");
                    return false;
                }
            }
            catch (Exception ex)
            {
                Growl.Error("updatePrsc UPDATE say:\r\n" + ex.Message);
                return false;
            }
        }
        //end
    }
}
