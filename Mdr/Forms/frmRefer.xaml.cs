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

namespace Mdr.Forms
{
    /// <summary>
    /// Interaction logic for frmRefer.xaml
    /// </summary>
    public partial class frmRefer : Window
    {
        public frmRefer()
        {
            InitializeComponent();
        }


        string strDate3 = "";

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
        private void moveallposition(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                this.DragMove();
            }
        }
        void getreferout()
        {
            string strSQL = "SELECT ovst.vn,DATE_FORMAT(ovst.vstdttm,'%Y-%m-%d') as datein,DATE_FORMAT((ovst.vstdttm), '%H:%i') as time," +
            "orfro.rfrno as refer_no,ovst.hn as hn,pt.fname as fname,pt.lname as lname, " +
            "CASE WHEN pt.male = '1' THEN 'ชาย' WHEN pt.male = '2' THEN 'หญิง' END AS 'sex', " +
            "DATE_FORMAT(NOW(), '%Y') - DATE_FORMAT(pt.brthdate, '%Y') - (DATE_FORMAT(NOW(), '00-%m-%d') < DATE_FORMAT(pt.brthdate, '00-%m-%d')) AS age," +
            "pt.pop_id as cid,ovst.pttype,pttype.namepttype as namepttype," +
            "ovstdx.icd10 as icd10,icd101.icd10name as icd10name,rfrcs.namerfrcs,hospcode.namehosp as refer_from " +
            " FROM orfro " +
            "INNER JOIN ovst ON orfro.vn = ovst.vn " +
            "INNER JOIN pt ON pt.hn = ovst.hn " +
            "INNER JOIN ovstdx ON ovstdx.vn = ovst.vn " +
            "INNER JOIN icd101 ON icd101.icd10 = ovstdx.icd10 " +
            "INNER JOIN hospcode ON hospcode.off_id = orfro.rfrlct " +
            "INNER JOIN rfrcs ON rfrcs.rfrcs = orfro.rfrcs " +
            "INNER JOIN pttype ON pttype.pttype = ovst.pttype " +
            " WHERE date(ovst.vstdttm) = '" + strDate3 + "' GROUP BY orfro.rfrno ORDER BY ovst.vstdttm desc ";

            DataTable dt = new DataTable();
            Dictionary<string, object> dictData = new Dictionary<string, object>();
            dictData.Add("query", strSQL);

            try
            {
                dt = Hi7.Class.APIConnect.getDataTable("query/hi7_query", "POST", dictData);
                if (dt != null)
                {
                    System.Data.DataView view = new System.Data.DataView(dt);
                    System.Data.DataTable selected = view.ToTable("Selected", false, "vn", "datein", "time", "refer_no", "hn", "fname", "lname", "sex", "age", "cid", "pttype", "namepttype", "icd10", "icd10name", "namerfrcs", "refer_from");
                    this.gridReferout.ItemsSource = selected.DefaultView;

                }
                else
                {
                    MessageBox.Show("ไม่สามารถบันทึกข้อมูลได้ กรุณาลองใหม่");
                }

            }
            catch (Exception ex)
            {

            }

        }

        void getreferin()
        {

            string strSQL = "SELECT ovst.vn,DATE_FORMAT(ovst.vstdttm,'%Y-%m-%d') as datein,DATE_FORMAT((ovst.vstdttm), '%H:%i') as time," +
            "orfri.rfrno AS refer_in_no,ovst.hn AS hn,pt.fname AS fname,pt.lname AS lname," +
            "CASE WHEN pt.male = '1' THEN 'ชาย' WHEN pt.male = '2' THEN 'หญิง' END AS 'sex', " +
            "DATE_FORMAT(NOW(), '%Y') - DATE_FORMAT(pt.brthdate, '%Y') - (DATE_FORMAT(NOW(), '00-%m-%d') < DATE_FORMAT(pt.brthdate, '00-%m-%d')) AS age," +
            "pt.pop_id AS cid,ovst.pttype,pttype.namepttype," +
            "orfri.icd10, " +
            "icd101.icd10name,rfrcs.namerfrcs,hospcode.off_name1 AS refer_from" +
            " FROM ovst " +
            "INNER JOIN orfri ON orfri.vn = ovst.vn " +
            "INNER JOIN icd101 ON icd101.icd10 = orfri.icd10 " +
            "INNER JOIN pt ON pt.hn = ovst.hn " +
            "INNER JOIN hospcode ON hospcode.off_id = orfri.rfrlct " +
            "INNER JOIN pttype ON pttype.pttype = ovst.pttype " +
            "INNER JOIN rfrcs ON rfrcs.rfrcs = orfri.rfrcs " +
            "WHERE date(ovst.vstdttm) = '" + strDate3 + "' ORDER BY ovst.vstdttm desc";

            DataTable dt = new DataTable();
            Dictionary<string, object> dictData = new Dictionary<string, object>();
            dictData.Add("query", strSQL);

            try
            {

                dt = Hi7.Class.APIConnect.getDataTable("query/hi7_query", "POST", dictData);
                if (dt != null)
                {

                    System.Data.DataView view = new System.Data.DataView(dt);
                    System.Data.DataTable selected = view.ToTable("Selected", false, "vn", "datein", "time", "refer_in_no", "hn", "fname", "lname", "sex", "age", "cid", "pttype", "namepttype", "icd10", "icd10name", "namerfrcs", "refer_from");
                    this.gridReferin.ItemsSource = selected.DefaultView;
                }
                else
                {
                    MessageBox.Show("ไม่สามารถบันทึกข้อมูลได้ กรุณาลองใหม่");
                }

            }
            catch (Exception ex)
            {

            }
            // gridReferin.Rows[0].Selected = true;

        }

        private void getToken()
        {

            JObject json;
            string url = "http://10.0.0.40:30040/v1/sign-token";
            //  string url = "http://localhost:3000/v1/sign-token";
           
            string resString;
            byte[] resByte;
            try
            {
                System.Net.WebClient client = new System.Net.WebClient();
                resByte = client.DownloadData(url);
                resString = Encoding.UTF8.GetString(resByte);
                json = JObject.Parse(resString);
                Hi7.Class.APIConnect.TOKEN_KEY = json.SelectToken("token").ToString();

            }
            catch (Exception ex)
            {
                MessageBox.Show("ไม่สามารถเชื่อมต่อ API ได้" + ex.Message);
            }
        }
        private void frmRefermain_Loaded(object sender, RoutedEventArgs e)
        {
            Hi7.Class.APIConnect.API_SERVER = "http://10.0.0.40:30040/";
            //  Hi7.Class.APIConnect.API_SERVER = "http://localhost:3000/";
            this.txtTotal_RferBack.Text = "0";
            this.txtTotal_RferIN.Text = "0";
            this.txtTotal_RferOUT.Text = "0";
            this.txtTotal_RferRecive.Text = "0";
            getToken();
            Hi7.Class.APIConnect.getConfgXML();
            DateTime dateTime = DateTime.Now;
            this.dtp.Text = dateTime.ToString("dd-MM-yyyy");
            string ldDate = HI7.Class.HIUility.CBE2D(this.dtp.Text);
            string lcDate = HI7.Class.HIUility.DTOS(ldDate);
            //MessageBox.Show(strDate3);
            strDate3 = lcDate;
         

            getreferin();
            getreferout();
            getSumTotoalReferO();
            getSumTotoalReferI();
        }

        private void dtp_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            string ldDate = HI7.Class.HIUility.CBE2D(this.dtp.Text);
            string lcDate = HI7.Class.HIUility.DTOS(ldDate);
            //MessageBox.Show(strDate3);
            strDate3 = lcDate;

            getreferin();
            getreferout();
            getSumTotoalReferO();
            getSumTotoalReferI();

        }

        private void btntest_Click(object sender, RoutedEventArgs e)
        {
            string iDate = dtp.ToString();
            DateTime oDate = DateTime.Parse(iDate);
            MessageBox.Show(strDate3);
        }

        private void gridReferout_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Up || e.Key == Key.Down)
            {


                DataRowView dataRow = (DataRowView)gridReferout.SelectedItem;
                int index = gridReferout.CurrentCell.Column.DisplayIndex;
                string cellValue = dataRow.Row.ItemArray[1].ToString();
                MessageBox.Show(cellValue);
            }
        }

        private void gridReferout_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            DataRowView dataRow = (DataRowView)gridReferout.SelectedItem;
            int index = gridReferout.CurrentCell.Column.DisplayIndex;
            string RefNo = dataRow.Row.ItemArray[0].ToString();
            string RefHn = dataRow.Row.ItemArray[3].ToString();
            string RefVn = dataRow.Row.ItemArray[4].ToString();
            HI7.Class.HIUility.Vnp = RefVn;
            HI7.Class.HIUility.RefNo = RefNo;
            HI7.Class.HIUility.HNR = RefHn;

            frmReferOut frmReferOut = new frmReferOut();
            frmReferOut.Show();



        }

        private void gridReferout_MouseRightButtonUp(object sender, MouseButtonEventArgs e)
        {
            DataRowView dataRow = (DataRowView)gridReferout.SelectedItem;
            int index = gridReferout.CurrentCell.Column.DisplayIndex;
            string cellValue = dataRow.Row.ItemArray[1].ToString();
            MessageBox.Show(cellValue);
        }

        private void gridReferout_KeyDown(object sender, KeyEventArgs e)
        {

        }

        private void dtp_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {

        }
        private void btnReferout_Click(object sender, RoutedEventArgs e)
        {
            frmReferOut frmReferOut = new frmReferOut();
            frmReferOut.Show();
        }

        private void btnreply_Click(object sender, RoutedEventArgs e)
        {
            frmReferins f = new frmReferins();
            f.Show();
        }

        private void btntest_Click_1(object sender, RoutedEventArgs e)
        {
            string iDate = dtp.ToString();
            DateTime oDate = DateTime.Parse(iDate);

        }

        void getSumTotoalReferO()
        {
            DataTable dt = new DataTable();
            DataRow dr;
            Dictionary<string, object> dictData = new Dictionary<string, object>();
            string strRferOUT = "SELECT COUNT(orfro.rfrno) as SumRferO FROM orfro INNER JOIN ovst ON orfro.vn = ovst.vn " +
            " WHERE date(ovst.vstdttm) = '" + strDate3 + "'";
            dictData.Add("query", strRferOUT);

            try
            {
                dt = Hi7.Class.APIConnect.getDataTable("query/hi7_query", "POST", dictData);
                //   dt = Hi7.Class.APIConnect.getDataTable("/register/patient/getPatientInfo", "POST", dictData);
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        dr = dt.Rows[0];
                        this.txtTotal_RferOUT.Text = dr["SumRferO"].ToString();
                    }
                    else
                    {
                        this.txtTotal_RferOUT.Text = "0";
                    }

                }
                else
                {
                    this.txtTotal_RferOUT.Text = "0";
                }
            }
            catch (Exception ex)
            {

            }


        }
        void getSumTotoalReferI()
        {
            DataTable dt = new DataTable();
            DataRow dr;
            Dictionary<string, object> dictData = new Dictionary<string, object>();
            string strRferOUT = "SELECT COUNT(orfri.rfrno) as SumRferI FROM orfri INNER JOIN ovst ON orfri.vn = ovst.vn " +
            " WHERE date(ovst.vstdttm) = '" + strDate3 + "'";
            dictData.Add("query", strRferOUT);

            try
            {
                dt = Hi7.Class.APIConnect.getDataTable("query/hi7_query", "POST", dictData);
                //   dt = Hi7.Class.APIConnect.getDataTable("/register/patient/getPatientInfo", "POST", dictData);
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        dr = dt.Rows[0];
                        this.txtTotal_RferIN.Text = dr["SumRferI"].ToString();
                    }
                    else
                    {
                        this.txtTotal_RferIN.Text = "0";
                    }

                }
                else
                {
                    this.txtTotal_RferIN.Text = "0";
                }
            }
            catch (Exception ex)
            {

            }


        }

        private void dtp_KeyDown(object sender, KeyEventArgs e)
        {

        }
    }
}
