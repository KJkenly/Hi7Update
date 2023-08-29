using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
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
    /// Interaction logic for frmCheckappoint.xaml
    /// </summary>
    public partial class frmCheckappoint : Window
    {
        public static string _HNSCREEN;
        public DataTable dt;
        public frmCheckappoint()
        {
            InitializeComponent();
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Hi7.Class.APIConnect.getConfgXML();
            getDataappoint();
        }
        void getDataappoint()
        {

            dt = HI7.Class.HIUility.getAppointment(_HNSCREEN);
            if (dt != null)
            {
                if (dt.Rows.Count > 0)
                {
                    System.Data.DataView view = new System.Data.DataView(dt);
                    System.Data.DataTable selected = view.ToTable("Selected", false, "vn", "hn", "vstdttm", "namecln", "dscrptn", "docname", "fudate");
                    this.dataGridappoint.ItemsSource = selected.DefaultView;
                    this.dataGridappoint.Columns[0].Header = "vn";
                    this.dataGridappoint.Columns[1].Header = "HN";
                    this.dataGridappoint.Columns[2].Header = "วันรับบริการ";
                    this.dataGridappoint.Columns[3].Header = "จุดรับบริการ";
                    this.dataGridappoint.Columns[4].Header = "รายละเอียด";
                    this.dataGridappoint.Columns[5].Header = "แพทย์นัด";
                    this.dataGridappoint.Columns[6].Header = "วันที่นัดรับบริการ";
                }
                else
                {
                    this.Close();
                }
            }
        }

        private void dataGridappoint_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {

            DataRowView dataRow = (DataRowView)dataGridappoint.SelectedItem;
            int index = dataGridappoint.CurrentCell.Column.DisplayIndex;
            string vn = dataRow.Row.ItemArray[0].ToString();

            string strSql = "SELECT oapp.id,CONCAT(DATE_FORMAT(oapp.vstdate , '%d-%m' ),'-',DATE_FORMAT(oapp.vstdate , '%Y' ) +543 )  as vstdttm,CONCAT(DATE_FORMAT(oapp.fudate , '%d-%m' ),'-',DATE_FORMAT(oapp.fudate , '%Y' ) +543 )  as fudate,oapp.hn,oapp.vn,oapp.cln,concat(a.fname, ' ', a.lname)AS 'docname',if(substr(oapp.cln,1,1) = '7' OR substr(oapp.cln,1,1) = 8,'2','1') as grpcln," +
                            " (" +
                            " CASE" +
                            " WHEN SUBSTR(oapp.cln, 1, 1) = '0' THEN '10100'" +
                            " WHEN SUBSTR(oapp.cln, 1, 1) = '1' THEN '10100'" +
                            " WHEN SUBSTR(oapp.cln, 1, 1) = '2' THEN '20100'" +
                            " WHEN SUBSTR(oapp.cln, 1, 1) = '3' THEN '30100'" +
                            " WHEN SUBSTR(oapp.cln, 1, 1) = '4' THEN '40100'" +
                            " WHEN SUBSTR(oapp.cln, 1, 1) = '5' THEN '50100'" +
                            " WHEN SUBSTR(oapp.cln, 1, 1) = '6' THEN '60100'" +
                            " WHEN SUBSTR(oapp.cln, 1, 1) = '7' THEN  SUBSTR(oapp.cln, 2)" +
                            " WHEN SUBSTR(oapp.cln, 1, 1) = '8' THEN SUBSTR(oapp.cln, 2)" +
                            " WHEN SUBSTR(oapp.cln, 1, 1) = '9' THEN '90100'" +
                            " else oapp.cln" +
                            " END" +
                            " ) as chkcln,cln.namecln,oapp.dscrptn" +
                            " FROM oapp" +
                            " LEFT JOIN cln on cln.cln = (" +
                            " CASE" +
                            " WHEN SUBSTR(oapp.cln,1,1) = '0' THEN '10100'" +
                            " WHEN SUBSTR(oapp.cln,1,1) = '1' THEN '10100'" +
                            " WHEN SUBSTR(oapp.cln,1,1) = '2' THEN '20100'" +
                            " WHEN SUBSTR(oapp.cln,1,1) = '3' THEN '30100'" +
                            " WHEN SUBSTR(oapp.cln,1,1) = '4' THEN '40100'" +
                            " WHEN SUBSTR(oapp.cln,1,1) = '5' THEN '50100'" +
                            " WHEN SUBSTR(oapp.cln,1,1) = '6' THEN '60100'" +
                            " WHEN SUBSTR(oapp.cln,1,1) = '7' THEN SUBSTR(oapp.cln,2)" +
                            " WHEN SUBSTR(oapp.cln,1,1) = '8' THEN SUBSTR(oapp.cln,2)" +
                            " WHEN SUBSTR(oapp.cln,1,1) = '9' THEN '90100'" +
                            " else oapp.cln" +
                            " END" +
                            " )" +
                            " LEFT JOIN dct a ON" +
                            " (" +
                            " CASE" +
                                " WHEN LENGTH(oapp.dct) = 2 THEN oapp.dct = a.dct" +
                                " WHEN LENGTH(oapp.dct) = 4 THEN right(oapp.dct, 2) = a.dct" +
                                " WHEN LENGTH(oapp.dct) = 5 THEN oapp.dct = a.lcno" +
                                " END )" +
                            " where fudate = curdate() AND oapp.vn = " + vn + "";
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
                        string checknamecln = dr["chkcln"].ToString();
                        string checkid = dr["id"].ToString();
                        string checkvn = dr["vn"].ToString();
                        string checknamedct = dr["docname"].ToString();
                        frmMdr frmMdr = new frmMdr();
                        HI7.Class.HIUility._SENDCLN = checknamecln;
                        HI7.Class.HIUility._IDOAPP = checkid;
                        HI7.Class.HIUility._VNAPPOINT = checkvn;
                        HI7.Class.HIUility._FULLNAMEDCT = checknamedct;
                        Mdr.Forms.frmMdr._ClickAppointsearchhn = "Click";
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("ไม่มีค่าส่งกลับ");
                    }
                }
                else
                {
                    MessageBox.Show("null");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("dataGridappoint_MouseDoubleClick say:\r\n"+ex.Message);
            }

        }
    }
}
