using System;
using System.Drawing;
using System.Windows.Controls;
using JWT;
using JWT.Algorithms;
using JWT.Serializers;
using System.Windows.Media.Imaging;
using System.Drawing.Imaging;
using System.Threading.Tasks;
using System.IO;
using System.Data;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System.Net;
using System.Linq;
using RestSharp;
using HandyControl.Controls;
using static System.Net.Mime.MediaTypeNames;
using Hi7.Class;
using System.Windows.Media.Converters;

namespace HI7.Class
{
    public static class HIUility
    {
        //User Login
        public static string USERLOGIN;
        //mak
        public static string Vnp = "";
        public static string HNR = "";
        public static string RefNo = "";
        // init LABEL
        public static Int16 SET_LABEL_FONT_SIZE;
        public static string SET_LABEL_FONT;
        public static Color SET_LABEL_COLOR;

        // init BUTTON
        public static Int16 SET_BUTTON_FONT_SIZE;
        public static string SET_BUTTON_FONT;
        public static Color SET_BUTTON_COLOR;


        // init TextBox
        public static Int16 SET_TEXTBOX_FONT_SIZE;
        public static string SET_TEXTBOX_FONT;
        public static Color SET_TEXTBOX_COLOR;

        //Para get Other
        public static string _HN;
        public static string _VN,GETVN;
        public static string _AN;
        public static string _HCODE;
        public static string _HOSPITALNAME;
        public static string _VSTDTTM;
        public static string _VSTDATE;
        public static string _VSTTIME;
        public static string _PTTYPE;
        public static string _CLN;
        public static string _DCT;
        public static string _TXTSEARCH;
        public static string _CONSULTID;
        public static string _POPID;
        public static string _SENDCLN;
        public static string _IDOAPP;
        public static string _VNAPPOINT;
        public static string _FULLNAMEDCT;
        //para Mdr
        public static string _TXTSEARCHPTTYPE;
        public static string _TXTSEARCHCLINIC;
        public static string _FNAME; 
        public static string _LNAME;
        // // token จากโปรแกรม UC AUTHEN
        public static string SMARTHEALTH_SMCTOKEN;//get value Tokey
        public static string SMARTHEALTH_USER_PERSON_UD; // get value CID "3341501073887";
        public static string _claimCode;//get Value authe claimcode
        //Q4U
        public static string _drg; //get hospcode
        public static string _hi_hsp_nm; //get Hospname
        public static string _serverapiq4u; //get Server Q4U
        public static string _prt_q4u; //get Check Print
        public static string _qtyprtq; //get qty print
        public static string _printerid; //get printer Server id
        public static string _smallqueue;//get smallqueue
        public static string _user_q4u; //get User q4u
        public static string _TokenQ4U; //get Token Q4U
        public static string _DateServQ4U; //get Curdate()
        public static string _TimeServQ4U; //get Curtime()
        public static string _PriorityIDQ4U; //get PriorityID
        public static string _SexQ4U; //get Code Male
        public static string _PnameQ4U; // get Value pname
        public static string _FnameQ4U; //get Value fname
        public static string _LnameQ4U; //get Value lname
        public static string _BrthdateQ4U; //get Value Brthdate
        public static string _QueueNumber; //get Return QueueNumber from Q4U
        public static string _QueueID;//get Return QueueID from Q4U
        public static string _StrQueueNumber;//get Return strQueueNumber '0-123' from Q4U
        public static string _BrthdateQ4UPrint;//เอาไว้พิมพ์
        //printQ
        public static string _PrintPriority;
        
        //Para get Prcd - codeprcd,nameprcd,price
        public static string pCODEPRCD; //get PRCD.CODEPRCD;       
        public static String pNAMEPRCD; //get PRCD.NAMEPRCD       
        public static string pCGD; //get PRCD.CGD     
        public static string pINCOME; //get PRCD.INCOME
        public static string pICD9ID; //get PRCD.ID    
        public static string pCHARGE; //get Charge Ref.  PTTYPE - PRCD.PRICEPRCD,PRCD.PRICECGD

        //Para get Rx - 
        public static string mDCD; //Drug Code
        public static string mDNM; //Drug Name
        public static string mSRT; //Drug Sale Rate
        public static string mP_C; //Drug Pack_charge
        public static string mDED; //Drug Code Ed
        public static string mlDconfa; //Drug Confirm of allergy
        public static string mlDconfi; //Drug Confirm of Interaction
        public static string mlDconfp; //Drug Confirm of Pregnany
        public static string mlRemed; //ใช้ยา Remed หรือ ไม่
        public static string mlXright; //ใช้ยานอกเหนือสิทธิ์ หรือ ไม่   
        public static string ml_n_dsn; //Free style dosename or NOT
        public static string mlType; //Drug Type
        public static string mPrice; // Drug Price
        public static string mMeduse; //Drug Medusage
        
        //update from Smartcard 
        public static string _CHANWAT;
        public static string _AMPHUR;
        public static string _TUMBON;
        public static string _MOOBAN;

        //CONFIG_API
        public static string API_CONFIG;
        //set พิมพ์ Getway
        public static string qtyprtq, printerid, smallqueue;
        //ตัวแปร HN Pttype
        public static string HNinsure = "";
        public static string Pttypeinsure = "";

        public static string JWT_DECODE(string token, string secret)
        {
            try
            {
                IJsonSerializer serializer = new JsonNetSerializer();
                UtcDateTimeProvider provider = new UtcDateTimeProvider();
                IJwtValidator validator = new JwtValidator(serializer, provider);
                IBase64UrlEncoder urlEncoder = new JwtBase64UrlEncoder();
                IJwtAlgorithm algorithm = new HMACSHA256Algorithm();
                IJwtDecoder decoder = new JwtDecoder(serializer, validator, urlEncoder, algorithm);

                var json = decoder.Decode(token, secret, verify: true);
                return json;
            }
            catch (Exception ex)
            {
                return "";
            }
        }
        public static BitmapImage ToBitmapImage(Bitmap bitmap)
        {
            //  System.Drawing.Image img = (System.Drawing.Image)bitmap;
            // //   string fileSavePath = System.IO.Path.Combine(saveDirectory, fileName);
            //   System.Drawing.Image copy = img;

            Bitmap oBitmap;
            oBitmap = new Bitmap(@"C:\PT_IMG\xx.jpg");
            Graphics oGraphic;
            // Here create a new bitmap object of the same height and width of the image.
            //  Bitmap bmpNew = new Bitmap(oBitmap.Width, oBitmap.Height);
            oGraphic = Graphics.FromImage(bitmap);
            //  oGraphic.DrawImage(oBitmap, new Rectangle(0, 0, oBitmap.Width, oBitmap.Height), 0, 0, oBitmap.Width, oBitmap.Height, GraphicsUnit.Pixel);
            oBitmap.Dispose();
            //    oBitmap = bmpNew;

            SolidBrush oBrush = new SolidBrush(Color.Black);
            Font ofont = new Font("Arial", 8);
            oGraphic.DrawString("Some text to write", ofont, oBrush, 10, 10);
            oGraphic.Dispose();
            ofont.Dispose();
            oBrush.Dispose();
            oBitmap.Save(@"C:\PT_IMG\1111.jpg", ImageFormat.Jpeg);
            oBitmap.Dispose();


            using (var memory = new System.IO.MemoryStream())
            {
                bitmap.Save(memory, ImageFormat.Jpeg);
                memory.Position = 0;

                var bitmapImage = new BitmapImage();
                bitmapImage.BeginInit();
                bitmapImage.StreamSource = memory;
                bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
                bitmapImage.EndInit();
                bitmapImage.Freeze();

                return bitmapImage;
            }
        }

        public static void saveBitmapImage(this BitmapImage image, string filePath)
        {
            BitmapEncoder encoder = new PngBitmapEncoder();
            encoder.Frames.Add(BitmapFrame.Create(image));

            using (var fileStream = new System.IO.FileStream(filePath, System.IO.FileMode.Create))
            {
                encoder.Save(fileStream);
            }
        }


        public static void initialAllDefualtControls()
        {
            // Label
            HIUility.SET_LABEL_FONT = "tahoma";
            HIUility.SET_LABEL_FONT_SIZE = 16;
            HIUility.SET_LABEL_COLOR = Color.White;

            //Button
            HIUility.SET_BUTTON_FONT = "tahoma";
            HIUility.SET_BUTTON_FONT_SIZE = 16;
            HIUility.SET_BUTTON_COLOR = Color.White;
            //TextBox
            HIUility.SET_TEXTBOX_FONT = "tahoma";
            HIUility.SET_TEXTBOX_FONT_SIZE = 16;
            HIUility.SET_TEXTBOX_COLOR = Color.White;


        }


        public static void initialHeaderControls()
        {
            // Label
            HIUility.SET_LABEL_FONT = "tahoma";
            HIUility.SET_LABEL_FONT_SIZE = 20;
            HIUility.SET_LABEL_COLOR = Color.White;

            //Button
            HIUility.SET_BUTTON_FONT = "tahoma";
            HIUility.SET_BUTTON_FONT_SIZE = 20;
            HIUility.SET_BUTTON_COLOR = Color.White;
            //TextBox
            HIUility.SET_TEXTBOX_FONT = "tahoma";
            HIUility.SET_TEXTBOX_FONT_SIZE = 20;
            HIUility.SET_TEXTBOX_COLOR = Color.White;


        }
        public static object IsNullString(object s)
        {
            if (DBNull.Value.Equals(s))
            {
                return "";
            }
            else
            {
                return s;
            }
        }
        //Insert Data
        public static bool InsertData(string nTable, string nFields, string nValues)
        {
            Dictionary<string, object> dictData = new Dictionary<string, object>();
            string strSQL = "insert into " + nTable + "(" + nFields + ") values(" + nValues + ") ";
            dictData.Add("query", strSQL);
            bool status = Hi7.Class.APIConnect.Excecute("query/hi7_query", "POST", dictData);
            return status;
        }
        //Update Data
        public static bool UdateData(string nTable, string nValues, string cCondi)
        {
            Dictionary<string, object> dictData = new Dictionary<string, object>();
            string strSQL = "update " + nTable + " set " + nValues + " where " + cCondi + ") ";
            dictData.Add("query", strSQL);
            bool status = Hi7.Class.APIConnect.Excecute("query/hi7_query", "POST", dictData);
            return status;
        }
        //Delete Data
        public static bool DeleteData(string nTable, string cCondi)
        {
            Dictionary<string, object> dictData = new Dictionary<string, object>();
            string strSQL = "delete from " + nTable + " where " + cCondi + ") ";
            dictData.Add("query", strSQL);
            bool status = Hi7.Class.APIConnect.Excecute("query/hi7_query", "POST", dictData);
            return status;
        }
        //OP5 lookup Setup
        public static DataTable getSetup()
        {
            string strSQL = "select * from setup";

            DataTable dt = new System.Data.DataTable();
            Dictionary<string, object> dictData = new Dictionary<string, object>();
            dictData.Add("query", strSQL);
            try
            {
                dt = Hi7.Class.APIConnect.getDataTable("query/hi7_query", "POST", dictData);
                if (dt != null)
                {
                    return dt;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        //OP5 lookup LAB
        public static DataTable getlookupLab()
        {

            string strSql = "select * from lab";

            DataTable dt = new System.Data.DataTable();
            Dictionary<string, object> dictData = new Dictionary<string, object>();
            dictData.Add("query", strSql);

            try
            {
                dt = Hi7.Class.APIConnect.getDataTable("query/hi7_query", "POST", dictData);
                if (dt != null)
                {

                    return dt;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        //OP5 lookup XRAY
        public static DataTable getlookupXR()
        {

            string strSql = "select * from xray";

            DataTable dt = new System.Data.DataTable();
            Dictionary<string, object> dictData = new Dictionary<string, object>();
            dictData.Add("query", strSql);

            try
            {
                dt = Hi7.Class.APIConnect.getDataTable("query/hi7_query", "POST", dictData);
                if (dt != null)
                {

                    return dt;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        //OP5 lookup DX
        public static DataTable getlookupDX()
        {

            string strSql = "SELECT x.codedx as icd10,namedx as icd10name,i.name_t from dx as x " +
            "INNER JOIN icd101 as i on x.codedx = i.icd10 where i.icd_use = '1' limit 1000";

            DataTable dt = new System.Data.DataTable();
            Dictionary<string, object> dictData = new Dictionary<string, object>();
            dictData.Add("query", strSql);

            try
            {
                dt = Hi7.Class.APIConnect.getDataTable("query/hi7_query", "POST", dictData);
                if (dt != null)
                {

                    return dt;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        //OP5 lookup ICD10
        public static DataTable getlookupIcd10(string icd10)
        {
            string strICD10 = icd10;
            int strICD10Length = strICD10.Length;

            if (strICD10Length == 1)
            {
                string strSql = "SELECT i.icd10,x.namedx,i.icd10name,i.name_t from icd101 as i  inner join dx as x on i.icd10 = x.codedx where icd_use ='1' and x.namedx like  UPPER('" + strICD10 + "%') limit 1000";
                DataTable dt = new System.Data.DataTable();
                Dictionary<string, object> dictData = new Dictionary<string, object>();
                dictData.Add("query", strSql);
                try
                {
                    dt = Hi7.Class.APIConnect.getDataTable("query/hi7_query", "POST", dictData);
                    if (dt != null)
                    {

                        return dt;
                    }
                    else
                    {
                        return null;
                    }
                }
                catch (Exception ex)
                {
                    return null;
                }
            }
            else
            {
                string x = strICD10.Substring(1, 1);
                int n;
                bool isNumeric = int.TryParse(x, out n);
                if (isNumeric)
                {
                    string strSql = "SELECT i.icd10,x.namedx,i.icd10name,i.name_t from icd101 as i  inner join dx as x on i.icd10 = x.codedx where icd_use ='1' and i.icd10 like  UPPER('" + strICD10 + "%') limit 1000";
                    DataTable dt = new System.Data.DataTable();
                    Dictionary<string, object> dictData = new Dictionary<string, object>();
                    dictData.Add("query", strSql);
                    try
                    {
                        dt = Hi7.Class.APIConnect.getDataTable("query/hi7_query", "POST", dictData);
                        if (dt != null)
                        {

                            return dt;
                        }
                        else
                        {
                            return null;
                        }
                    }
                    catch (Exception ex)
                    {
                        return null;
                    }
                }
                else
                {
                    string strSql = "SELECT i.icd10,x.namedx,i.icd10name,i.name_t from icd101 as i  inner join dx as x on i.icd10 = x.codedx where icd_use ='1' and x.namedx like  UPPER('" + strICD10 + "%') limit 1000";
                    DataTable dt = new System.Data.DataTable();
                    Dictionary<string, object> dictData = new Dictionary<string, object>();
                    dictData.Add("query", strSql);
                    try
                    {
                        dt = Hi7.Class.APIConnect.getDataTable("query/hi7_query", "POST", dictData);
                        if (dt != null)
                        {

                            return dt;
                        }
                        else
                        {
                            return null;
                        }
                    }
                    catch (Exception ex)
                    {
                        return null;
                    }
                }

            }



        }
        //Mdr lookup clinic
        public static DataTable getlookupClinic(string cln)
        {
            string strSymp = cln;
            string strSql = "select cln,concat('(',cln,')',namecln) AS 'namecln' from cln where cln.cln LIKE('" + cln + "%') order by cln ASC";

            DataTable dt = new System.Data.DataTable();
            Dictionary<string, object> dictData = new Dictionary<string, object>();
            dictData.Add("query", strSql);

            try
            {
                dt = Hi7.Class.APIConnect.getDataTable("query/hi7_query", "POST", dictData);
                if (dt != null)
                {

                    return dt;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        //Mdr lookup pttype
        public static DataTable getlookupPttype(string pttype) 
            //cln.cln LIKE('" + pttype + "%')
        {
            
            string strSql = "select pttype, concat('(', pttype, ')', ' ', namepttype) AS 'namepttype' from pttype" + " where pttype.chkshow = '1' and pttype.pttype LIKE('" + pttype + "%') order by pttype ASC";

            DataTable dt = new System.Data.DataTable();
            Dictionary<string, object> dictData = new Dictionary<string, object>();
            dictData.Add("query", strSql);

            try
            {
                dt = Hi7.Class.APIConnect.getDataTable("query/hi7_query", "POST", dictData);
                if (dt != null)
                {

                    return dt;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        //OP5 lookup SYMPTOM
        public static DataTable getlookupSymptom(string symp)
        {
            string strSymp = symp;
            string strSql = "SELECT s.symptom as namefind from symptom as  s where symptom like '" + strSymp + "%' ";

            DataTable dt = new System.Data.DataTable();
            Dictionary<string, object> dictData = new Dictionary<string, object>();
            dictData.Add("query", strSql);

            try
            {
                dt = Hi7.Class.APIConnect.getDataTable("query/hi7_query", "POST", dictData);
                if (dt != null)
                {

                    return dt;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        //OP5 lookup SYMPTOM
        public static DataTable getlookupSign()
        {

            string strSql = "SELECT s.sign as namefind from sign0 as s";

            DataTable dt = new System.Data.DataTable();
            Dictionary<string, object> dictData = new Dictionary<string, object>();
            dictData.Add("query", strSql);

            try
            {
                dt = Hi7.Class.APIConnect.getDataTable("query/hi7_query", "POST", dictData);
                if (dt != null)
                {

                    return dt;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        //OP5 lookup PRCD
        public static DataTable getlookupPrcd(string strPrcd)
        {

            string strSql = "SELECT p.id,p.codeprcd,p.nameprcd,p.fullname,p.cgd,p.income,p.ptright,p.priceprcd,p.pricecgd,i.namecost from prcd as p " +
            "INNER JOIN income as i on p.income = i.costcenter where p.nameprcd like UPPER('" + strPrcd + "%') limit 100";

            DataTable dt = new System.Data.DataTable();
            Dictionary<string, object> dictData = new Dictionary<string, object>();
            dictData.Add("query", strSql);

            try
            {
                dt = Hi7.Class.APIConnect.getDataTable("query/hi7_query", "POST", dictData);
                if (dt != null)
                {

                    return dt;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        //OP5 lookup MEDITEM
        public static DataTable getlookupMeditem(string strMed)
        {
            string strName = strMed;
            string strSql = "SELECT m.*, " +
            "if (m.ed = 1,'ในบัญชี','นอกบัญชี') as nameed, " +
            "if (m.chkstock = 1,'ปกติ','หมดคลัง') namechkstock " +
            " from meditem as m where pres_nm like UPPER('" + strName + "%') limit 1000";

            DataTable dt = new System.Data.DataTable();
            Dictionary<string, object> dictData = new Dictionary<string, object>();
            dictData.Add("query", strSql);

            try
            {
                dt = Hi7.Class.APIConnect.getDataTable("query/hi7_query", "POST", dictData);
                if (dt != null)
                {

                    return dt;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        //OP5 RTN
        public static DataTable getRtn()
        {

            string strSql = "select * from rtn";

            DataTable dt = new System.Data.DataTable();
            Dictionary<string, object> dictData = new Dictionary<string, object>();
            dictData.Add("query", strSql);

            try
            {
                dt = Hi7.Class.APIConnect.getDataTable("query/hi7_query", "POST", dictData);
                if (dt != null)
                {

                    return dt;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        //OP5 RTNDT
        public static DataTable getRtnDt()
        {

            string strSql = "SELECT r.rtnno,r.labcode,l.labname from rtnldt as r " +
            "INNER JOIN lab as l on r.labcode = l.labcode";

            DataTable dt = new System.Data.DataTable();
            Dictionary<string, object> dictData = new Dictionary<string, object>();
            dictData.Add("query", strSql);

            try
            {
                dt = Hi7.Class.APIConnect.getDataTable("query/hi7_query", "POST", dictData);
                if (dt != null)
                {

                    return dt;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        //OP5 RTNL
        public static DataTable getRtnl()
        {

            string strSql = "select * from rtnl";

            DataTable dt = new System.Data.DataTable();
            Dictionary<string, object> dictData = new Dictionary<string, object>();
            dictData.Add("query", strSql);

            try
            {
                dt = Hi7.Class.APIConnect.getDataTable("query/hi7_query", "POST", dictData);
                if (dt != null)
                {

                    return dt;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        //OP5 RTNLDT
        public static DataTable getRtnlDt(string lcCode)
        {
            string strlcCode = lcCode;
            string strSql = "SELECT r.rtnno,r.labcode,l.labname from rtnldt as r " +
            " INNER JOIN lab as l on r.labcode = l.labcode where r.rtnno = " + strlcCode;

            DataTable dt = new System.Data.DataTable();
            Dictionary<string, object> dictData = new Dictionary<string, object>();
            dictData.Add("query", strSql);

            try
            {
                dt = Hi7.Class.APIConnect.getDataTable("query/hi7_query", "POST", dictData);
                if (dt != null)
                {

                    return dt;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public static DataTable getHn(string cid)
        {
            string strCid = cid;
            string strSql = "SELECT pt.hn,concat(pt.pname,pt.fname,' ',pt.lname) as 'fullname',DATE_FORMAT(ldate,'%d-%m-%Y') as 'ldate',pt.hometel from pt" +
            " where pt.pop_id = " + cid;

            DataTable dt = new System.Data.DataTable();
            Dictionary<string, object> dictData = new Dictionary<string, object>();
            dictData.Add("query", strSql);

            try
            {
                dt = Hi7.Class.APIConnect.getDataTable("query/hi7_query", "POST", dictData);
                if (dt != null)
                {

                    return dt;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        //MDR เอาชื่อสิทธิ์มาหา id
        public static string getIdpttype(string namepttype)
        {
            string strNamepttype = namepttype;
            string strSql = "select pttype, concat('(', pttype, ')', ' ', namepttype) AS 'namepttype' from pttype" + " where pttype.namepttype = '" + strNamepttype + "'";
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
                        string strname = dr["namepttype"].ToString();
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
        //ค้นหาจากชื่อเป็นรหัส เวชปฏิบัติทั่วไป เป็น (10100)เวชปฏิบัติทั่วไป
        public static string getIdclinic(string namecln)
        {
            string strNamecln = namecln;
            string strSql = "select cln,concat('(',cln,')',namecln) AS 'namecln' from cln" + " where cln.namecln = '" + strNamecln + "'";
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
                        string strname = dr["namecln"].ToString();
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
        //ค้นหาจากชื่อเป็นรหัส เวชปฏิบัติทั่วไป เป็น 10100
        public static string getIdcln(string namecln)
        {
            string strNamecln = namecln;
            string strSql = "select cln from cln" + " where cln.namecln = '" + strNamecln + "'";
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
                        string strname = dr["cln"].ToString();
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
        //แปลงรหัสคลินิกเป็นชื่อ
        public static string c2n_cln(string lcCode)
        {
            string strlcCode = lcCode;
            string strSql = "select cln.namecln from cln where cln = '" + strlcCode + "'";
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
                        string strname = dr["namecln"].ToString();
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
        //แปลงรหัสเพศเป็นชื่อ
        public static string c2n_male(string lcCode)
        {
            string strlcCode = lcCode;
            string strSql = "select male.namemale from male where male = '" + strlcCode + "'";
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
                        string strname = dr["namemale"].ToString();
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
        //แปลงรหัสเป็นชื่อโรค
        public static string c2n_icd(string lcCode)
        {
            string strlcCode = lcCode;
            string strSql = "select icd10name from icd101 where icd10 = '" + strlcCode + "'";
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
                        string strname = dr["icd10name"].ToString();
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
        //แปลงรหัสเป็นชื่อ
        public static string c2n_dtr(string lcCode)
        {
            string strlcCode = lcCode;
            string strSql = "SELECT (CASE WHEN LENGTH('" + lcCode + "') = 5 THEN (select concat(dct.fname,' ',dct.lname) as fndct from dct where dct.lcno = @dct limit 1) " +
            "WHEN LENGTH('" + lcCode + "') = 4 THEN concat((select concat(dct.fname,' ',dct.lname) as fndct from dct where dct.dct = substr(@dct, 1, 2) limit 1),'/',(select concat(dct.fname, ' ', dct.lname) as fndct from dct where dct.dct = substr(@dct, 3, 2) limit 1)) " +
            "WHEN LENGTH('" + lcCode + "') = 3 THEN(select concat(dentist.fname, ' ', dentist.lname) as fndct from dentist where dentist.codedtt = @dct limit 1)" +
            "WHEN LENGTH('" + lcCode + "') = 2 THEN(select concat(dct.fname, ' ', dct.lname) as fndct from hi.dct where dct.dct = @dct limit 1) else '' END ) as namedct from dct GROUP BY namedct";
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
                        string strname = dr["namedct"].ToString();
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
        //แปลงรหัสสิทธิ์เป็นชื่อ
        public static string c2n_pttype(string lcCode)
        {
            string strlcCode = lcCode;
            string strSql = "select pttype.namepttype from pttype where pttype = '" + strlcCode + "'";
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
                        string strname = dr["namepttype"].ToString();
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
        //HN2AGE
        public static string Hn2Age(string hn)
        {
            string strHN = hn;
            string strSql = "select concat(timestampdiff(year,pt.brthdate,CURDATE()),' ปี  '  ,mod(timestampdiff(month,pt.brthdate,CURDATE()),12),' เดือน' ," +
            "timestampdiff(day, date_add(pt.brthdate, interval(timestampdiff(month, pt.brthdate, curdate())) month), curdate()),'  วัน' )as cage from hi.pt where pt.hn = " + strHN;
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
                        string strname = (string)IsNullString(dr["cage"]);
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
        
        public static string getDatainscl(string pttype)
        {
            string strVN = pttype;
            string strSql = "select inscl from pttype where pttype = " + strVN;
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
                        //string strname = dr["sign"].ToString();
                        string strname = (string)IsNullString(dr["inscl"]);
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
        //แสดงข้อมูล Sign Rtn dataTable
        public static DataTable getodata_PeTab(string vn)
        {

            string strVN = vn;
            string strSql = "select *  from sign where vn = " + strVN;

            DataTable dt = new System.Data.DataTable();
            Dictionary<string, object> dictData = new Dictionary<string, object>();
            dictData.Add("query", strSql);

            try
            {
                dt = Hi7.Class.APIConnect.getDataTable("query/hi7_query", "POST", dictData);
                if (dt != null)
                {

                    return dt;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        //แสดงข้อมูล SIGN to Values
        public static string getodata_Pe(string vn)
        {
            string strVN = vn;
            string strSql = "select group_concat(sign.sign) as sign from sign where vn = " + strVN;
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
                        //string strname = dr["sign"].ToString();
                        string strname = (string)IsNullString(dr["sign"]);
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
        //แสดงข้อมูล Pillness Rtn dataTable
        public static DataTable getodata_PiTab(string vn)
        {

            string strVN = vn;
            string strSql = "select *  from pillness where vn = " + strVN;

            DataTable dt = new System.Data.DataTable();
            Dictionary<string, object> dictData = new Dictionary<string, object>();
            dictData.Add("query", strSql);

            try
            {
                dt = Hi7.Class.APIConnect.getDataTable("query/hi7_query", "POST", dictData);
                if (dt != null)
                {

                    return dt;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        //แสดงข้อมูล Symptm Rtn dataTable
        public static DataTable getodata_CcTab(string vn)
        {

            string strVN = vn;
            string strSql = "select *  from symptm where vn = " + strVN;

            DataTable dt = new System.Data.DataTable();
            Dictionary<string, object> dictData = new Dictionary<string, object>();
            dictData.Add("query", strSql);

            try
            {
                dt = Hi7.Class.APIConnect.getDataTable("query/hi7_query", "POST", dictData);
                if (dt != null)
                {

                    return dt;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        //แสดงข้อมูล Symptm Rtn Values
        public static string getodata_Cc(string vn)
        {
            string strVN = vn;
            string strSql = "select group_concat(symptom) as symptom from symptm where vn = " + strVN;
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
                        string strname = (string)IsNullString(dr["symptom"]);
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
        //แสดงข้อมูล Pillness
        public static string getodata_Pi(string vn)
        {
            string strVN = vn;
            string strSql = "select group_concat(pillness) as pillness from pillness where vn = " + strVN;
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
                        string strname = (string)IsNullString(dr["pillness"]);
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
        //แสดงข้อมูล Rx OPD
        public static DataTable getodata_RxOpd(string vn)
        {

            string strVN = vn;
            string strSql = "SELECT o.vn,o.vstdttm,r.prscno,CONCAT(DATE_FORMAT(r.prscdate , '%d-%m' ),'-',DATE_FORMAT(r.prscdate , '%Y' ) +543 )  as vstdate,TIME_FORMAT(time(r.prsctime * 100), '%H:%i') as vsttime," +
            "r.prscdate,r.prsctime,rd.meditem,rd.nameprscdt,if (rd.xdoseno = 0,rd.medusage,rd.xdoseno) as meduse," +
            "if (rd.xdoseno = 0,(SELECT m.dosename from medusage as m where m.dosecode = rd.medusage GROUP BY m.dosecode ),(SELECT x.doseprn from xdose as x WHERE x.xdoseno = rd.xdoseno GROUP BY x.xdoseno) )as doseprn," +
            "t.name_tme,if (rd.ed = 0,'ในบัญชี','นอกบัญชี') as namemedtype,rd.qty,rd.charge " +
            " from ovst as o " +
            " INNER JOIN prsc as r on o.vn = r.vn and r.an = 0" +
            " INNER JOIN prscdt as rd on r.prscno = rd.prscno" +
            " INNER JOIN meditem as m on rd.meditem = m.meditem" +
            " INNER JOIN typemed_claim as t on m.type = t.code_tme where o.vn = " + strVN;

            DataTable dt = new System.Data.DataTable();
            Dictionary<string, object> dictData = new Dictionary<string, object>();
            dictData.Add("query", strSql);

            try
            {
                dt = Hi7.Class.APIConnect.getDataTable("query/hi7_query", "POST", dictData);
                if (dt != null)
                {

                    return dt;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        //แสดงข้อมูล ReMed
        public static DataTable getRemedRx(string hn)
        {
            string strHN = hn;
            string strSql = "SELECT o.vn,o.vstdttm,r.prscno,CONCAT(DATE_FORMAT(r.prscdate , '%d-%m' ),'-',DATE_FORMAT(r.prscdate , '%Y' ) +543 )  as vstdate,TIME_FORMAT(time(r.prsctime * 100), '%H:%i') as vsttime," +
            "r.prscdate,r.prsctime,rd.meditem,rd.nameprscdt,if (rd.xdoseno = 0,rd.medusage,rd.xdoseno) as meduse," +
            "if (rd.xdoseno = 0,(SELECT m.dosename from medusage as m where m.dosecode = rd.medusage GROUP BY m.dosecode ),(SELECT x.doseprn from xdose as x WHERE x.xdoseno = rd.xdoseno GROUP BY x.xdoseno) ) as doseprn," +
            "t.name_tme,if (rd.ed = 0,'ในบัญชี','นอกบัญชี') as namemedtype,rd.qty,rd.charge " +
            " from ovst as o " +
            " INNER JOIN prsc as r on o.vn = r.vn and r.an = 0 " +
            " INNER JOIN prscdt as rd on r.prscno = rd.prscno " +
            " INNER JOIN meditem as m on rd.meditem = m.meditem " +
            " INNER JOIN typemed_claim as t on m.type = t.code_tme where o.hn = " + strHN + "  ORDER BY o.vn desc ";

            DataTable dt = new System.Data.DataTable();
            Dictionary<string, object> dictData = new Dictionary<string, object>();
            dictData.Add("query", strSql);

            try
            {
                dt = Hi7.Class.APIConnect.getDataTable("query/hi7_query", "POST", dictData);
                if (dt != null)
                {

                    return dt;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        //แสดงข้อมูล DX OPD
        public static DataTable getodata_DxOpd(string vn)
        {

            string strVN = vn;
            string strSql = "SELECT x.icd10,i.icd10name,i.name_t as nameth,x.cnt,o.dct from ovst as o " +
            "INNER JOIN ovstdx as x on o.vn = x.vn " +
            "INNER JOIN icd101 as i on x.icd10 = i.icd10 " +
            " where o.vn = " + strVN;

            DataTable dt = new System.Data.DataTable();
            Dictionary<string, object> dictData = new Dictionary<string, object>();
            dictData.Add("query", strSql);

            try
            {
                dt = Hi7.Class.APIConnect.getDataTable("query/hi7_query", "POST", dictData);
                if (dt != null)
                {

                    return dt;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        //แสดงข้อมูล OPRT OPD
        public static DataTable getodata_PrOpd(string vn)
        {
            string strVN = vn;
            string strSql = "SELECT o.vn,CONCAT(DATE_FORMAT(r.opdttm , '%d-%m' ),'-',DATE_FORMAT(r.opdttm , '%Y' ) +543 )  as vstdate,DATE_FORMAT(r.opdttm , '%H:%i') as vsttime" +
            ",r.icd9cm,p.nameprcd,p.fullname,p.income,c.namecost,p.cgd,r.qty,r.charge," +
            "(SELECT(CASE WHEN LENGTH(r.dct) = 5 THEN(select concat(dct.fname, ' ', dct.lname) as fndct from dct where dct.lcno = r.dct limit 1)" +
            " WHEN LENGTH(r.dct) = 4 THEN concat((select concat(dct.fname, ' ', dct.lname) as fndct from dct where dct.dct = substr(r.dct, 1, 2) limit 1), '/', (select concat(dct.fname, ' ', dct.lname) as fndct from dct where dct.dct = substr(r.dct, 3, 2) limit 1))" +
            "WHEN LENGTH(r.dct) = 3 THEN(select concat(dentist.fname, ' ', dentist.lname) as fndct from dentist where dentist.codedtt = r.dct limit 1)" +
            "WHEN LENGTH(r.dct) = 2 THEN(select concat(dct.fname, ' ', dct.lname) as fndct from dct where dct.dct = r.dct limit 1) else '' END ) as namedct from dct GROUP BY namedct ) as namedct" +
            " from ovst as o" +
            " INNER JOIN oprt as r on o.vn = r.vn" +
            " INNER JOIN prcd as p on r.icd9cm = p.codeprcd and r.icd9name = p.nameprcd" +
            " INNER JOIN income as c on p.income = c.costcenter " +
            " where o.vn = " + strVN;

            DataTable dt = new System.Data.DataTable();
            Dictionary<string, object> dictData = new Dictionary<string, object>();
            dictData.Add("query", strSql);

            try
            {
                dt = Hi7.Class.APIConnect.getDataTable("query/hi7_query", "POST", dictData);
                if (dt != null)
                {

                    return dt;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        //แสดงข้อมูล Xray OPD
        public static DataTable getodata_XrOpd(string vn)
        {
            string strVN = vn;
            string strSql = "select q.vn,q.an,o.hn,CONCAT(DATE_FORMAT( q.vstdate , '%d-%m' ),'-',DATE_FORMAT( q.vstdate , '%Y' ) +543 )  as vstdate,q.vsttime,q.xrycode,x.xryname," +
            "x.cgd,q.dct,if (q.rgtok = 0,'ยังไม่เสร็จ','เสร็จแล้ว') as cstatus ,TIME_FORMAT(time(q.vsttime * 100), '%H:%i') as vsttime," +
            "(SELECT(CASE WHEN LENGTH(q.dct) = 5 THEN(select concat(dct.fname, ' ', dct.lname) as fndct from dct where dct.lcno = q.dct limit 1)" +
            " WHEN LENGTH(q.dct) = 4 THEN concat((select concat(dct.fname, ' ', dct.lname) as fndct from dct where dct.dct = substr(q.dct, 1, 2) limit 1), '/', (select concat(dct.fname, ' ', dct.lname) as fndct from dct where dct.dct = substr(q.dct, 3, 2) limit 1)) " +
            " WHEN LENGTH(q.dct) = 3 THEN(select concat(dentist.fname, ' ', dentist.lname) as fndct from dentist where dentist.codedtt = q.dct limit 1) " +
            " WHEN LENGTH(q.dct) = 2 THEN(select concat(dct.fname, ' ', dct.lname) as fndct from dct where dct.dct = q.dct limit 1) else '' END ) as namedct from dct GROUP BY namedct ) as namedct " +
            " from ovst as o" +
            " INNER JOIN xryrqt as q on o.vn = q.vn" +
            " INNER JOIN xray as x on q.xrycode = x.xrycode" +
            " where q.an = 0 and o.vn = " + strVN + " order by vstdate asc";

            DataTable dt = new System.Data.DataTable();
            Dictionary<string, object> dictData = new Dictionary<string, object>();
            dictData.Add("query", strSql);

            try
            {
                dt = Hi7.Class.APIConnect.getDataTable("query/hi7_query", "POST", dictData);
                if (dt != null)
                {

                    return dt;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public static DataTable getPtinfo(string hn)
        {
            string strHN = hn;
            string strSql = "SELECT p.*,(CASE p.male WHEN 1 THEN IF(p.mrtlst< 6, IF(TIMESTAMPDIFF(year, p.brthdate, NOW() ) < 15, 'ด.ช.', 'นาย' )," +
            "IF(TIMESTAMPDIFF(year, p.brthdate, NOW()) < 20, 'พระครู', 'พระ')) WHEN 2 THEN " +
            "IF(p.mrtlst = 1, IF(TIMESTAMPDIFF(year, p.brthdate, NOW()) < 15, 'ด.ญ.', 'น.ส.'), IF(p.mrtlst < 6, 'นาง', 'แม่ชี')) END) AS prename," +
                "concat(p.fname,' ',p.lname) as fullname,c.namechw,a.nameampur,t.nametumb,m.namemoob " +
           "  from pt as p " +
           "LEFT JOIN changwat as c on p.chwpart = c.chwpart " +
           "LEFT JOIN ampur as a on p.chwpart = a.chwpart and p.amppart = a.amppart " +
           "LEFT JOIN tumbon as t on p.chwpart = t.chwpart and p.amppart = t.amppart and p.tmbpart = t.tmbpart " +
           "LEFT JOIN mooban as m on p.chwpart = m.chwpart and p.amppart = m.amppart and p.tmbpart = m.tmbpart and p.moopart = m.moopart " +
           " where p.hn = " + strHN + " GROUP BY p.hn ";

            DataTable dt = new System.Data.DataTable();
            Dictionary<string, object> dictData = new Dictionary<string, object>();
            dictData.Add("query", strSql);

            try
            {
                dt = Hi7.Class.APIConnect.getDataTable("query/hi7_query", "POST", dictData);
                if (dt != null)
                {

                    return dt;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        //ค้นหาด้วย Fname
        public static DataTable getPtinfoFname(string fanme)
        {

            string strSql = "SELECT p.*,concat(p.pname,' ',p.fname,' ',p.lname) as fullname,c.namechw,a.nameampur,t.nametumb,m.namemoob " +
           "  from pt as p " +
           "LEFT JOIN changwat as c on p.chwpart = c.chwpart " +
           "LEFT JOIN ampur as a on p.chwpart = a.chwpart and p.amppart = a.amppart " +
           "LEFT JOIN tumbon as t on p.chwpart = t.chwpart and p.amppart = t.amppart and p.tmbpart = t.tmbpart " +
           "LEFT JOIN mooban as m on p.chwpart = m.chwpart and p.amppart = m.amppart and p.tmbpart = m.tmbpart and p.moopart = m.moopart " +
           " where p.fname LIKE('" + fanme + "%') LIMIT 20";

            DataTable dt = new System.Data.DataTable();
            Dictionary<string, object> dictData = new Dictionary<string, object>();
            dictData.Add("query", strSql);

            try
            {
                dt = Hi7.Class.APIConnect.getDataTable("query/hi7_query", "POST", dictData);
                if (dt != null)
                {

                    return dt;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        //ค้นหาด้วย Lname
        public static DataTable getPtinfoLname(string lname)
        {
            string strSql = "SELECT p.*,concat(p.pname,' ',p.fname,' ',p.lname) as fullname,c.namechw,a.nameampur,t.nametumb,m.namemoob " +
           "  from pt as p " +
           "LEFT JOIN changwat as c on p.chwpart = c.chwpart " +
           "LEFT JOIN ampur as a on p.chwpart = a.chwpart and p.amppart = a.amppart " +
           "LEFT JOIN tumbon as t on p.chwpart = t.chwpart and p.amppart = t.amppart and p.tmbpart = t.tmbpart " +
           "LEFT JOIN mooban as m on p.chwpart = m.chwpart and p.amppart = m.amppart and p.tmbpart = m.tmbpart and p.moopart = m.moopart " +
           " where p.lname LIKE('" + lname + "%')";

            DataTable dt = new System.Data.DataTable();
            Dictionary<string, object> dictData = new Dictionary<string, object>();
            dictData.Add("query", strSql);

            try
            {
                dt = Hi7.Class.APIConnect.getDataTable("query/hi7_query", "POST", dictData);
                if (dt != null)
                {

                    return dt;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        //ค้นหาด้วย Fname and Lname
        public static DataTable getPtinfoFullname(string fname, string lname)
        {
            string strSql = "SELECT p.*,concat(p.pname,' ',p.fname,' ',p.lname) as fullname,c.namechw,a.nameampur,t.nametumb,m.namemoob " +
           "  from pt as p " +
           "LEFT JOIN changwat as c on p.chwpart = c.chwpart " +
           "LEFT JOIN ampur as a on p.chwpart = a.chwpart and p.amppart = a.amppart " +
           "LEFT JOIN tumbon as t on p.chwpart = t.chwpart and p.amppart = t.amppart and p.tmbpart = t.tmbpart " +
           "LEFT JOIN mooban as m on p.chwpart = m.chwpart and p.amppart = m.amppart and p.tmbpart = m.tmbpart and p.moopart = m.moopart " +
          " where p.fname LIKE('" + fname + "%') AND p.lname LIKE('" + lname + "%') limit 20";
            DataTable dt = new System.Data.DataTable();
            Dictionary<string, object> dictData = new Dictionary<string, object>();
            dictData.Add("query", strSql);
            try
            {
                dt = Hi7.Class.APIConnect.getDataTable("query/hi7_query", "POST", dictData);
                if (dt != null)
                {

                    return dt;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        //get Computer name
        public static string getComputername()
        {
            string strComputerName = System.Net.Dns.GetHostName();
            return strComputerName;
        }
        // get IpAddress 
        public static string getIpAddress()
        {
            string Hostname = System.Net.Dns.GetHostName();
            IPAddress[] ipAddress = System.Net.Dns.GetHostAddresses(Hostname);
            foreach (IPAddress ip in ipAddress.Where(ip => ip.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork))
            {
                return ip.ToString();
            }
            return "";
        }
        // HN2NAME Retrue Fullname
        public static string gethn2name(string hn)
        {
            string strHN = hn;
            string strSql = "SELECT p.*,(CASE p.male WHEN 1 THEN IF(p.mrtlst< 6, IF(TIMESTAMPDIFF(year, p.brthdate, NOW() ) < 15, 'ด.ช.', 'นาย' )," +
            "IF(TIMESTAMPDIFF(year, p.brthdate, NOW()) < 20, 'พระครู', 'พระ')) WHEN 2 THEN " +
            "IF(p.mrtlst = 1, IF(TIMESTAMPDIFF(year, p.brthdate, NOW()) < 15, 'ด.ญ.', 'น.ส.'), IF(p.mrtlst < 6, 'นาง', 'แม่ชี')) END) AS prename," +
                "concat(p.fname,' ',p.lname) as fullname,c.namechw,a.nameampur,t.nametumb,m.namemoob " +
           "  from pt as p " +
           "LEFT JOIN changwat as c on p.chwpart = c.chwpart " +
           "LEFT JOIN ampur as a on p.chwpart = a.chwpart and p.amppart = a.amppart " +
           "LEFT JOIN tumbon as t on p.chwpart = t.chwpart and p.amppart = t.amppart and p.tmbpart = t.tmbpart " +
           "LEFT JOIN mooban as m on p.chwpart = m.chwpart and p.amppart = m.amppart and p.tmbpart = m.tmbpart and p.moopart = m.moopart " +
           " where p.hn = " + strHN + " GROUP BY p.hn ";
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
                        string strname = (string)IsNullString(dr["fullname"]);
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
        // แสดง Lab
        public static DataTable getodata_LabOpd(string vn)
        {
            string strVN = vn;
            string strSql = "SELECT l.*,concat(DATE_FORMAT(l.senddate , '%d-%m' ),'-',DATE_FORMAT(l.senddate , '%Y' ) +543 ) as senddate,lab.labname,lab.cgd,if(l.finish = 0,'ยังไม่เสร็จ','เสร็จแล้ว') as cstatus, " +
            "TIME_FORMAT(time(l.sendtime * 100),'%H:%i') as sendtime FROM lbbk as l " +
            "inner join lab on l.labcode = lab.labcode " +
            " where l.vn = " + strVN;

            DataTable dt = new System.Data.DataTable();
            Dictionary<string, object> dictData = new Dictionary<string, object>();
            dictData.Add("query", strSql);

            try
            {
                dt = Hi7.Class.APIConnect.getDataTable("query/hi7_query", "POST", dictData);
                if (dt != null)
                {

                    return dt;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        // แสดงภาพคนไข้
        public static void LoadImage(System.Windows.Controls.Image objimg)
        {
            DataTable dt = new DataTable();
            DataRow dr;
            Dictionary<string, object> dictData = new Dictionary<string, object>();
            string strSQL = "select * from hi7ptimage where hn='" + HI7.Class.HIUility._HN + "'";
            dictData.Add("query", strSQL);


            try
            {
                dt = Hi7.Class.APIConnect.getDataTable("query/hi7_query", "POST", dictData);

                if (dt != null)
                {
                    dr = dt.Rows[0];
                    byte[] bytes = Convert.FromBase64String(dr["cid_image"].ToString());

                    //    Image image;
                    using (MemoryStream ms = new MemoryStream(bytes))
                    {
                        //  image = Image.FromStream(ms);
                        // ms.Save("C:\\PT_IMG\\" + this.txtIDCard.Text + ".jpg");
                        using (System.Drawing.Image img = System.Drawing.Image.FromStream(ms, true))
                        {
                            //  img.Save(satelliteFolderImagesDownload + "\\image" + satCounter + ".gif", System.Drawing.Imaging.ImageFormat.Gif);
                            img.Save("C:\\PT_IMG\\" + HI7.Class.HIUility._POPID + ".jpg");

                            //  base64String = Convert.ToBase64String(ms.ToArray());
                            //   addimage(base64String);

                        }
                    }
                }
                //ส่วนแสดงภาพ
                BitmapImage bitmapImage = new BitmapImage();
                bitmapImage.BeginInit();
                bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
                bitmapImage.UriSource = new Uri(@"C://PT_IMG//" + HI7.Class.HIUility._POPID + ".jpg", UriKind.RelativeOrAbsolute);
                bitmapImage.EndInit();
                bitmapImage.Freeze();
                objimg.Source = bitmapImage; // <== //display image


            }
            catch (Exception ex)
            {
            }
            //  return image;
        }

        // Grad Bmi
        public static string getGradBmi(string bw, string heigth)
        {
            double lnBw, lnHe, bmi;
            string lcRtn;
            lnBw = double.Parse(bw);
            lnHe = double.Parse(heigth);
            bmi = lnBw / ((lnHe / 100) * (lnHe / 100));
            if (bmi > 29.9)
            {
                lcRtn = "[" + bmi.ToString("0.##") + "]" + " อ้วนมาก";
                return lcRtn;
            }
            if (bmi >= 25.0 & bmi <= 29.9)
            {
                lcRtn = "[" + bmi.ToString("0.##") + "]" + " อ้วน";
                return lcRtn;
            }
            if (bmi >= 23.0 & bmi <= 25.0)
            {
                lcRtn = "[" + bmi.ToString("0.##") + "]" + " น้ำหนักเกิน";
                return lcRtn;
            }
            if (bmi >= 18.5 & bmi <= 23.0)
            {
                lcRtn = "[" + bmi.ToString("0.##") + "]" + " ปกติ";
                return lcRtn;
            }
            if (bmi < 18.5)
            {
                lcRtn = "[" + bmi.ToString("0.##") + "]" + " ผอม";
                return lcRtn;
            }
            return "";
        }

        //แจ้งเตือนแพ้ยา
        public static DataTable getAllergy(string hn)
        {
            string lnHN = hn;
            string strSql = "SELECT a.meditem,a.namedrug,a.allgtype,t.nameallgtype,a.alevel,v.namealevel,a.detail,DATE_FORMAT(a.entrydate,'%Y-%m-%d')as entrydate,CONCAT(p.fname,' ',p.lname) as namephm from allergy as a " +
            " INNER JOIN phrmcst as p on a.pharmacist = p.pmc " +
            " INNER JOIN allgtype as t on a.allgtype = t.allgtype " +
            " INNER JOIN alevel as v on a.alevel = v.codealevel " +
            " where a.hn = " + lnHN + " ORDER BY a.id desc  ";

            DataTable dt = new System.Data.DataTable();
            Dictionary<string, object> dictData = new Dictionary<string, object>();
            dictData.Add("query", strSql);

            try
            {
                dt = Hi7.Class.APIConnect.getDataTable("query/hi7_query", "POST", dictData);
                if (dt != null)
                {

                    return dt;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                return null;
            }

        }
        //OP5 Search Infro Pt
        public static DataTable getSearchInfor(string lcSearch)
        {
            string strSearch = lcSearch;
            int strSearchLength = strSearch.Length;

            if (strSearchLength == 1)
            {
                string strSql = "SELECT p.hn,p.fname,p.lname,p.pop_id,IF(p.male = '1','ชาย','หญิง') as sex,YEAR(curdate()) - year(p.brthdate) as age," +
                "p.addrpart,p.moopart,m.namemoob,p.tmbpart,t.nametumb,p.amppart,a.nameampur,p.chwpart,c.namechw,concat(fname,' ',lname) as fullname" +
                " from pt as p " +
                "LEFT JOIN changwat as c on p.chwpart = c.chwpart " +
                "LEFT JOIN ampur as a on p.chwpart = a.chwpart and p.amppart = a.amppart " +
                "LEFT JOIN tumbon as t on p.chwpart = t.chwpart and p.amppart = t.amppart and p.tmbpart = t.tmbpart " +
                "LEFT JOIN mooban as m on p.chwpart = m.chwpart and p.amppart = m.amppart and p.tmbpart = m.tmbpart and p.moopart = m.moopart " +
                " where p.fname like '" + strSearch + "%'  limit 100";
                DataTable dt = new System.Data.DataTable();
                Dictionary<string, object> dictData = new Dictionary<string, object>();
                dictData.Add("query", strSql);
                try
                {
                    dt = Hi7.Class.APIConnect.getDataTable("query/hi7_query", "POST", dictData);
                    if (dt != null)
                    {

                        return dt;
                    }
                    else
                    {
                        return null;
                    }
                }
                catch (Exception ex)
                {
                    return null;
                }
            }
            else
            {
                string x = strSearch.Substring(1, 1);
                int n;
                bool isNumeric = int.TryParse(x, out n);
                if (isNumeric)
                {
                    string strSql = "SELECT p.hn,p.fname,p.lname,p.pop_id,IF(p.male = '1','ชาย','หญิง') as sex,YEAR(curdate()) - year(p.brthdate) as age," +
                    "p.addrpart,p.moopart,m.namemoob,p.tmbpart,t.nametumb,p.amppart,a.nameampur,p.chwpart,c.namechw,concat(fname,' ',lname) as fullname" +
                    " from pt as p " +
                    "LEFT JOIN changwat as c on p.chwpart = c.chwpart " +
                    "LEFT JOIN ampur as a on p.chwpart = a.chwpart and p.amppart = a.amppart " +
                    "LEFT JOIN tumbon as t on p.chwpart = t.chwpart and p.amppart = t.amppart and p.tmbpart = t.tmbpart " +
                    "LEFT JOIN mooban as m on p.chwpart = m.chwpart and p.amppart = m.amppart and p.tmbpart = m.tmbpart and p.moopart = m.moopart " +
                    " where p.pop_id like '" + strSearch + "%'  limit 100";
                    DataTable dt = new System.Data.DataTable();
                    Dictionary<string, object> dictData = new Dictionary<string, object>();
                    dictData.Add("query", strSql);
                    try
                    {
                        dt = Hi7.Class.APIConnect.getDataTable("query/hi7_query", "POST", dictData);
                        if (dt != null)
                        {

                            return dt;
                        }
                        else
                        {
                            return null;
                        }
                    }
                    catch (Exception ex)
                    {
                        return null;
                    }
                }
                else
                {
                    string strSql = "SELECT p.hn,p.fname,p.lname,p.pop_id,IF(p.male = '1','ชาย','หญิง') as sex,YEAR(curdate()) - year(p.brthdate) as age," +
                    "p.addrpart,p.moopart,m.namemoob,p.tmbpart,t.nametumb,p.amppart,a.nameampur,p.chwpart,c.namechw,concat(fname,' ',lname) as fullname" +
                    " from pt as p " +
                    "LEFT JOIN changwat as c on p.chwpart = c.chwpart " +
                    "LEFT JOIN ampur as a on p.chwpart = a.chwpart and p.amppart = a.amppart " +
                    "LEFT JOIN tumbon as t on p.chwpart = t.chwpart and p.amppart = t.amppart and p.tmbpart = t.tmbpart " +
                    "LEFT JOIN mooban as m on p.chwpart = m.chwpart and p.amppart = m.amppart and p.tmbpart = m.tmbpart and p.moopart = m.moopart " +
                    " where p.fname like '" + strSearch + "%'  limit 100";
                    DataTable dt = new System.Data.DataTable();
                    Dictionary<string, object> dictData = new Dictionary<string, object>();
                    dictData.Add("query", strSql);
                    try
                    {
                        dt = Hi7.Class.APIConnect.getDataTable("query/hi7_query", "POST", dictData);
                        if (dt != null)
                        {

                            return dt;
                        }
                        else
                        {
                            return null;
                        }
                    }
                    catch (Exception ex)
                    {
                        return null;
                    }
                }

            }
        }

        //แปลงเลขบัตรเป็นรูปแบบมาตรฐาน x-xxxx-xxxxx-xx-x
        public static string Pid13217(string cid)
        {
            string lcCid = cid;
            return lcCid.Substring(0, 1) + '-' + lcCid.Substring(1, 4) + '-' + lcCid.Substring(5, 5) + '-' + lcCid.Substring(10, 2) + '-' + lcCid.Substring(12);
        }

        //แปลงเลขบัตร  x-xxxx-xxxxx-xx-x ให้เป็นรูปแบบมาตรฐาน xxxxxxxxxxxxx
        public static string Pid17213(string cid)
        {
            string lcCid = cid;
            return lcCid.Substring(0, 1) + '-' + lcCid.Substring(3, 4) + '-' + lcCid.Substring(8, 5) + '-' + lcCid.Substring(14, 2) + '-' + lcCid.Substring(17);
        }

        //แปลงเวลาปัจุบันให้เก็บเป็นนัมเมอร์ริก
        public static string timeNow()
        {
            DateTime dateTime = DateTime.Now;
            //string strTable = "o" + dateTime.ToString("ddMMyy", CultureInfo.GetCultureInfo("th-TH"));
            //string strValues = this.txtHn.Text + ", NOW(),'" + this.ccbMainhis.SelectedValue + "','" + strstaff + "'";
            //DateTime dateTime = DateTime.Now;
            string strTable = dateTime.ToString("HHmm");
            int check = strTable.Length;
            try
            {
                if (check == 4)
                {
                    string time = "";
                    String checkzeroone = strTable.Substring(0, 1);
                    String checkzerotwo = strTable.Substring(0, 2);
                    String checkzerotree = strTable.Substring(0, 3);
                    String checkzerofour = strTable.Substring(0, 4);
                    if (checkzerofour == "0000")
                    {
                        time = "0";
                        return time;
                    }
                    else if (checkzerotree == "000")
                    {
                        time = strTable.Substring(3, 1);
                        return time;
                    }
                    else if (checkzerotwo == "00")
                    {
                        time = strTable.Substring(2, 2);
                        return time;
                    }
                    else if (checkzeroone == "0")
                    {
                        time = strTable.Substring(1, 3);
                        return time;
                    }
                    else
                    {
                        time = strTable;
                        return time;
                    }
                }
            }
            catch (Exception ex)
            {
                return "";
            }
            return "";
        }
        //หาค่า Last ID เพื่อ reture ค่าที่ต้องการ
        public static string getLastID(string sql)
        {
            string strSQL = sql;
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

        //หาคนไข้นัด
        public static DataTable getAppointment(string hn)//ดึงข้อมูล ken นัดหน้าหลัก
        {
            //string strlcCode = lcCode;
            //string strSql = "SELECT CONCAT(DATE_FORMAT(oapp.vstdate , '%d-%m' ),'-',DATE_FORMAT(oapp.vstdate , '%Y' ) +543 )  as vstdttm,oapp.hn,oapp.an,oapp.dct,oapp.cln,cln.namecln,oapp.dscrptn,oapp.fudate,concat(a.fname, ' ', a.lname)AS 'docname' FROM oapp  LEFT JOIN dct a ON(CASE WHEN LENGTH(oapp.dct) = 2 THEN oapp.dct = a.dct WHEN LENGTH(oapp.dct) = 4 THEN right(oapp.dct, 2) = a.dct WHEN LENGTH(oapp.dct) = 5 THEN oapp.dct = a.lcno END) LEFT JOIN cln ON cln.cln = oapp.cln where fudate = '2022-01-13' AND oapp.hn = " + hn + "";
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
            " ) as chkcln,cln.namecln,oapp.dscrptn,DATE_FORMAT(oapp.vstdate,'%Y-%d-%m') as vstdate,oapp.dct as dct," +
            " ( CASE" +
            " WHEN LENGTH(cln.id)  IS NULL THEN CONCAT('00')" +
            " WHEN LENGTH(cln.id) = '1' THEN CONCAT('0',cln.id)" +
            " WHEN LENGTH(cln.id) > '1' THEN CONCAT(cln.id)" +
            "END" +
            ") AS idcln,oapp.vn" +
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
            " where fudate = curdate() AND oapp.hn = " + hn + " AND oapp.fuok = '0'";
            DataTable dt = new System.Data.DataTable();
            Dictionary<string, object> dictData = new Dictionary<string, object>();
            dictData.Add("query", strSql);
            try
            {
                dt = Hi7.Class.APIConnect.getDataTable("query/hi7_query", "POST", dictData);

                //DataRow[] rows = dt.Select("grpcln = 2");
                //if (rows.Length > 0)
                //{
                //    var filldt = rows.CopyToDataTable();
                //}
                if (dt != null)
                {

                    return dt;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        //ค้นหาโน๊ตสิทธิ์การรักษา
        public static string Getnodeinsure()
        {  
            string strSql = "SELECT * FROM insure WHERE insure.hn = "+ HNinsure + " and "+ "insure.pttype = "+ Pttypeinsure +" ORDER BY insure.id DESC LIMIT 1" ;
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
                        string strname = dr.ItemArray[10].ToString();
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
        //HN2AGE หาอายุ เป็น ปี
        public static string Hn2AgeYY(string hn)
        {
            string strHN = hn;
            string strSql = "select IFNULL(timestampdiff(year,pt.brthdate,CURDATE()),'0') as cage from pt where pt.hn = " + strHN;
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
                        string strname = dr.ItemArray[0].ToString();
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
        //หาวันเกิด
        public static string GetBirthday(string hn)
        {
            string strHN = hn;
            string strSql = "select DATE_FORMAT(pt.brthdate, '%d/%m/%Y') AS brthdate from hi.pt where pt.hn = " + strHN;
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
                        string strname = HI7.Class.HIUility.DateConvert((string)IsNullString(dr["brthdate"]));
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
        //HN2AGE
        public static string Hn2Agem(string hn)
        {
            string strHN = hn;
            string strSql = "select concat(timestampdiff(year,pt.brthdate,CURDATE()),' ปี '  ,mod(timestampdiff(month,pt.brthdate,CURDATE()),12),' เดือน ' ," +
            "timestampdiff(day, date_add(pt.brthdate, interval(timestampdiff(month, pt.brthdate, curdate())) month), curdate()),' วัน' )as cage from hi.pt where pt.hn = " + strHN;
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
                        string strname = (string)IsNullString(dr["cage"]);
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
        //เก็บค่าส่่งให้ Q4U
        public static string getLoginQ4U()
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
                        _drg = dr.ItemArray[0].ToString();
                        _hi_hsp_nm = dr.ItemArray[1].ToString();
                        _serverapiq4u = dr.ItemArray[2].ToString();
                        _prt_q4u = dr.ItemArray[3].ToString();
                        _printerid = dr.ItemArray[4].ToString();
                        _smallqueue = dr.ItemArray[5].ToString();
                        _qtyprtq = dr.ItemArray[6].ToString();
                        _TimeServQ4U = dr.ItemArray[7].ToString();
                        _DateServQ4U = dr.ItemArray[8].ToString();
                        _user_q4u = "username=U" + dr.ItemArray[0].ToString() + "&password=123456";
                        _TokenQ4U = getTokenQ4U(_user_q4u); //get Rout Login Q4U
                        if (_TokenQ4U != "")
                        {
                            getRegisterQ4U();
                            return "1";
                        }
                        else
                        {
                            return "0";
                        }

                    }
                    else
                    {
                        return "0";
                    }
                }
                else
                {
                    return "0";
                }
            }
            catch (Exception ex)
            {
                return "0";
            }
        }

        //get Token Q4U
        public static string getTokenQ4U(string login)
        {

            try {
                var client = new RestClient(_serverapiq4u + "/login");
                client.Timeout = -1;
                var request = new RestRequest(Method.POST);
                request.AddHeader("Content-Type", "application/x-www-form-urlencoded");
                request.AddParameter("username", "U" + _drg);
                request.AddParameter("password", "123456");
                IRestResponse response = client.Execute(request);
                HttpStatusCode statusCode = response.StatusCode;
                if (statusCode == HttpStatusCode.OK)
                {
                    int numericStatusCode = (int)statusCode;
                    dynamic jsonResponse = JsonConvert.DeserializeObject<object>(response.Content.ToString());
                    //Growl.Success("ติดต่อ API สำเร็จ");
                    return jsonResponse.token;
                }
                else
                {
                    //messagebox.Show("ไม่สามารถเชื่อมต่อ API ได้ " + response.message);
                    Growl.Error("ติดต่อ API ระบบคิวไม่ได้.....!!!!!");
                    return "";
                }
            } 
            catch { Growl.Error("ติดต่อ SERVER Q4U ไม่ได้.....!!!!!"); return ""; }
            
        }

        //get Register Q4U
        public static string getRegisterQ4U()
        {       
            var client = new RestClient(_serverapiq4u + "/queue/register");
            client.Timeout = -1;
            var request = new RestRequest(Method.POST);
            request.AddHeader("Authorization", "Bearer " + _TokenQ4U);
            request.AddHeader("Content-Type", "application/x-www-form-urlencoded");
            request.AddParameter("hn", _HN);
            request.AddParameter("vn", _VN);
            request.AddParameter("clinicCode", _CLN);
            request.AddParameter("dateServ", _DateServQ4U);
            request.AddParameter("firstName", _FnameQ4U);
            request.AddParameter("lastName", _LnameQ4U);
            request.AddParameter("priorityId", _PriorityIDQ4U);
            request.AddParameter("timeServ", _TimeServQ4U);
            request.AddParameter("birthDate", _BrthdateQ4U);
            request.AddParameter("title", _PnameQ4U);
            request.AddParameter("sex", _SexQ4U);
            IRestResponse response = client.Execute(request);
            HttpStatusCode statusCode = response.StatusCode;
            if (statusCode == HttpStatusCode.OK)
            {
                int numericStatusCode = (int)statusCode;
                dynamic jsonResponse = JsonConvert.DeserializeObject<object>(response.Content.ToString());
                _QueueNumber = jsonResponse.queueNumber;
                _QueueID = jsonResponse.queueId;
                _StrQueueNumber = jsonResponse.strQueueNumber;
                
                Growl.Success("สร้างคิวสำเร็จ! " + _StrQueueNumber);
                try {
                    HI7.Class.HIUility.Visitqueueid();
                }
                catch
                { 
                }
            }
            return "";
        }
        //Insert Data
        public static bool Visitqueueid()
        {
            try {
                string Namepriority = getNamepriority();
                Dictionary<string, object> dictData = new Dictionary<string, object>();
                string strSQL = "insert into visitqueueid(vn,queue_id,queue_number,queue_priority)"+" values"+"("+"'"+ _VN+"',"+"'"+ _QueueID+"',"+"'"+ _StrQueueNumber+"',"+"'"+ Namepriority + "')";
                //string strSQL = "insert into " + nTable + "(" + nFields + ") values(" + nValues + ") ";
                dictData.Add("query", strSQL);
                bool status = Hi7.Class.APIConnect.Excecute("query/hi7_query", "POST", dictData);
                return true;
            }
            catch {
                return false;
            }
        }
        public static string getNamepriority()
        {
            //string priority = "";
            string strSql = "SELECT q4u_priorities.priority_name FROM q4u_priorities WHERE q4u_priorities.priority_id ="+ "'"+ _PriorityIDQ4U + "'";
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
                        string priority = (string)IsNullString(dr["priority_name"]).ToString();
                        return priority;

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
            //return priority;
        }
        public static string getPrintQ4U()
        {
            var client = new RestClient(_serverapiq4u + "/print/queue/prepare/print");
            client.Timeout = -1;
            var request = new RestRequest(Method.POST);
            request.AddHeader("Authorization", "Bearer " + _TokenQ4U);
            request.AddHeader("Content-Type", "application/json");
            request.AddJsonBody(
                            new
                            {
                                queueId = _QueueID,
                                topic = "/printer/"+printerid,
                                printSmallQueue = smallqueue
                            }); 
            // AddJsonBody serializes the object automatically
            IRestResponse response = client.Execute(request);
            HttpStatusCode statusCode = response.StatusCode;
            if (statusCode == HttpStatusCode.OK)
            {
                int numericStatusCode = (int)statusCode;
                dynamic jsonResponse = JsonConvert.DeserializeObject<object>(response.Content.ToString());
                Growl.Success("Print gateway success:" + statusCode + response.Content);
            }
            return statusCode.ToString();
        }
        public static string getNumberQueue(string q4u_numberqueue)
        {
            //string strlcCode = lcCode;
            string strSql = "SELECT q4u_queue.queue_number FROM q4u_queue WHERE q4u_queue.vn = '"+ q4u_numberqueue + "'";
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
                        string numberqueu = dr["queue_number"].ToString();
                        return numberqueu;
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
        //CBE2D   พ.ศ'01/02/2525' ---> ค.ศ '1982-02-01'
        public static string CBE2D(string bdate)
        {
            try
            {
                //  16 / 1 / 1982
                string[] arr = bdate.Split('/');
                bool m = arr[1].Length == 1;
                string mm = m ? "0" + arr[1] : arr[1];

                bool d = arr[0].Length == 1;
                string dd = d ? "0" + arr[0] : arr[0];

                int YY = int.Parse(arr[2]) - 543;

                string ddate = YY + "-" + mm + "-" + dd;
                return ddate;
            }
            catch(Exception ex) {
                return "";
            }
            
            
        }
        //date 3/2/2023 to 03/02/2565
        public static string DateConvert(string bdate)
        {
            try
            {
                //  16 / 1 / 1982
                string[] arr = bdate.Split('/');
                bool m = arr[1].Length == 1;
                string mm = m ? "0" + arr[1] : arr[1];

                bool d = arr[0].Length == 1;
                string dd = d ? "0" + arr[0] : arr[0];

                int YY = int.Parse(arr[2]) + 543;

                string ddate = dd + "/" + mm + "/" + YY;
                return ddate;
            }
            catch (Exception ex)
            {
                return "";
            }
        }
        //date 3/2/2023 to 2023-02-03
        public static string datealog(string bdate)
        {
            try
            {
                //  16 / 1 / 1982
                string[] arr = bdate.Split('/');
                bool m = arr[1].Length == 1;
                string mm = m ? "0" + arr[1] : arr[1];

                bool d = arr[0].Length == 1;
                string dd = d ? "0" + arr[0] : arr[0];

                string YY = arr[2];

                string ddate = YY + "-" + mm + "-" + dd;
                return ddate;
            }
            catch (Exception ex)
            {
                return "";
            }
        }

        //DTOS ค.ศ.'2022-01-31' ---> String '20220131'
        public static string DTOS(string ldDATE)
        {
            try
            {
                string[] arr = ldDATE.Split('-');
                bool m = arr[1].Length == 1;
                string mm = m ? "0" + arr[1] : arr[1];

                bool d = arr[2].Length == 1;
                string dd = d ? "0" + arr[2] : arr[2];
                string ddate = arr[0] + mm + dd;
                return ddate;
            }
            catch (Exception ex) {
                return "";
            }
            
        }
        //DTOS ค.ศ.'10/12/2534' ---> String '20220131'
        public static string DateTranform1(string ldDATE)
        {
            try
            {
                string[] arr = ldDATE.Split('/');

                bool d = arr[0].Length == 1;
                string dd = d ? "0" + arr[0] : arr[0];
                bool m = arr[1].Length == 1;
                string mm = m ? "0" + arr[1] : arr[1];
                int yy = int.Parse(arr[2]) - 543;

                //bool d = arr[2].Length == 1;
                //string dd = d ? "0" + arr[2] : arr[2];
                string ddate =yy+mm+dd;

                return ddate;
            }
            catch (Exception ex)
            {
                return "";
            }

        }

        //D2CBE ค.ศ. '2022-01-31' ---> พ.ศ.'31/01/2565'
        public static string D2CBE(string ldDATE)
        {
            try
            {
                string[] arr = ldDATE.Split('-');
                bool m = arr[1].Length == 1;
                string mm = m ? "0" + arr[1] : arr[1];

                bool d = arr[2].Length == 1;
                string dd = d ? "0" + arr[2] : arr[2];

                int YY = int.Parse(arr[0]) + 543;

                string ddate = dd + "/" + mm + "/" + YY;
                return ddate;
            }
            catch (Exception ex) {
                return "";
            }
            
        }
        //แปลง 1991-12-10 to 1/12/1991
        public static string DateChange1(string ldDATE)
        {
            try
            {
                string[] arr = ldDATE.Split('-');
                bool m = arr[1].Length == 1;
                string mm = m ? "0" + arr[1] : arr[1];

                bool d = arr[2].Length == 1;
                string dd = d ? "0" + arr[2] : arr[2];

                int YY = int.Parse(arr[0]);

                string ddate = YY + "/" + mm + "/" + dd;
                return ddate;
            }
            catch (Exception ex)
            {
                return "";
            }
        }
        //แปลง 1/12/1991 to 1991-12-10
        public static string DateChange2(string ldDATE)
        {
            try
            {
                string[] arr = ldDATE.Split('/');
                bool m = arr[1].Length == 1;
                string mm = m ? "0" + arr[1] : arr[1];

                bool d = arr[0].Length == 1;
                string dd = d ? "0" + arr[0] : arr[0];

                int YY = int.Parse(arr[2]);

                string ddate = YY + "-" + mm + "-" + dd;
                return ddate;
            }
            catch (Exception ex)
            {
                return "";
            }
        }
        //แปลง 2566-12-01 to 1991-12-01
        public static string DateChange3(string ldDATE)
        {
            try
            {
                string[] arr = ldDATE.Split('-');
                bool m = arr[1].Length == 1;
                string mm = m ? "0" + arr[1] : arr[1];

                bool d = arr[0].Length == 1;
                string dd = d ? "0" + arr[2] : arr[2];

                int YY = int.Parse(arr[0])-543;

                string ddate = YY + "-" + mm + "-" + dd;
                return ddate;
            }
            catch (Exception ex)
            {
                return "";
            }
        }
        //แปลง 26/08/2528 to 26/8/1985
        public static string DateChange4(string ldDATE)
        {
            try
            {
                string[] arr = ldDATE.Split('/');
                bool m = arr[1].Length == 1;
                string mm = m ? "0" + arr[1] : arr[1];
                bool d = arr[0].Length == 1;
                string dd = d ? "0" + arr[0] : arr[0];
                int YY = int.Parse(arr[2])-543;

                string ddate = dd + "/" + mm + "/" + YY;
                return ddate;
            }
            catch (Exception ex)
            {
                return "";
            }
        }
        //แปลง 1/2/2528 to 01/02/1985
        public static string DateChange5(string ldDATE)
        {
            try
            {
                string[] arr = ldDATE.Split('/');
                bool m = arr[1].Length == 1;
                string mm = m ? "0" + arr[1] : arr[1];
                bool d = arr[0].Length == 1;
                string dd = d ? "0" + arr[0] : arr[0];
                int YY = int.Parse(arr[2]);

                string ddate = dd + "/" + mm + "/" + YY;
                return ddate;
            }
            catch (Exception ex)
            {
                return "";
            }
        }
        //10/12/2534 to 1991-12-10
        public static string DateChange6(string bdate)
        {
            try
            {
                //  16 / 1 / 1982
                string[] arr = bdate.Split('/');
                bool m = arr[1].Length == 1;
                string mm = m ? "0" + arr[1] : arr[1];

                bool d = arr[0].Length == 1;
                string dd = d ? "0" + arr[0] : arr[0];

                int YY = int.Parse(arr[2]) - 543;

                string ddate = YY + "-" + mm + "-" + dd;
                return ddate;
            }
            catch (Exception ex)
            {
                return "";
            }
        }
        //Datetranform1 ค.ศ. '2022-01-31' ---> พ.ศ.'31/01/2022'
        public static string Datetranform1(string ldDATE)
        {
            try
            {
                string[] arr = ldDATE.Split('-');
                bool m = arr[1].Length == 1;
                string mm = m ? arr[1] : arr[1];

                bool d = arr[2].Length == 1;
                string dd = d ? arr[2] : arr[2];

                int YY = int.Parse(arr[0]);

                string ddate = dd + "/" + mm + "/" + YY;
                return ddate;
            }
            catch (Exception ex)
            {
                return "";
            }

        }

        //แปลง 25660101 เป็น 01/01/2023 เช็ค+หลักเท่านั้น
        public static string Datetranform2(string ldDATE)
        {
            try
            {
                int count = ldDATE.Length;
                if (count == 8)
                {
                    string Y = ldDATE.Substring(0, 4);
                    int numberY = int.Parse(Y)-543;
                    string YY = numberY.ToString();
                    string M = ldDATE.Substring(4, 2);
                    if(M == "00")
                    {
                        M = "01";
                    }
                    string D = ldDATE.Substring(6, 2);
                    if (D == "00")
                    {
                        D = "01";
                    }
                    string date = D+"/"+M+"/"+YY;
                    return date;
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
        //แปลง 25660101 เป็น 01/01/2566 เช็ค+หลักเท่านั้น
        public static string Datetranform3(string ldDATE)
        {
            try
            {
                int count = ldDATE.Length;
                if (count == 8)
                {
                    string Y = ldDATE.Substring(0, 4);
                    int numberY = int.Parse(Y);
                    string YY = numberY.ToString();
                    string M = ldDATE.Substring(4, 2);
                    if (M == "00")
                    {
                        M = "01";
                    }
                    string D = ldDATE.Substring(6, 2);
                    if (D == "00")
                    {
                        D = "01";
                    }
                    string date = D + "/" + M + "/" + YY;
                    return date;
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
        //แปลงเวลาปัจุบันให้เก็บเป็นนัมเมอร์ริก
        public static string TimeOtoday()
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
        //แปลงเวลา จากTime 4 หลักเป็น นัมเมอร์ริก
        public static string TimeNumeric(string strTable)
        {
            //DateTime dateTime = DateTime.Now;
            //string strTable = dateTime.ToString("HHmm");
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

        public static string getTokenShare()
        {
            try
            {
                string SmcDeviceSMC = APIConnect.PART_DEVICESMARTCARD;
                StreamReader str = new StreamReader(SmcDeviceSMC);
                string line;
                while ((line = str.ReadLine()) != null)
                {
                    string[] arr = line.Split('#');
                    // // เลขบัตรเจ้าหน้าที่ห้องบัตร
                    SMARTHEALTH_USER_PERSON_UD = arr[0];
                    // // token จากโปรแกรม UC AUTHEN
                    SMARTHEALTH_SMCTOKEN = arr[1];
                    
                }
            }catch(Exception e)
            {
                MessageBox.Warning("ไม่พบไฟล์ nhso Token"+ e.Message);
                //Console.WriteLine("ไม่พบไฟล์ nhso Token");
                //Console.WriteLine(e.Message);
            }
            return "";
        }




        public static void getHcode()
        {
            DataRow dr;
            DataTable dt = new System.Data.DataTable();
            try
            {
                dt = getSetup();
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        dr = dt.Rows[0];
                        //string strname = dr["sign"].ToString();
                        string strHcode = (string)HI7.Class.HIUility.IsNullString(dr["_drg"]);
                        HI7.Class.HIUility._HCODE = strHcode;
                    }
                    else
                    {
                        
                    }
                }
                else
                {
                    HI7.Class.HIUility._HCODE = "";
                   
                }

            }
            catch (Exception ex)
            {
                HI7.Class.HIUility._HCODE = "";
                Growl.Warning("getHcode Warning!!!");
            }
        }
        public static void getHostname()
        {
            DataRow dr;
            DataTable dt = new System.Data.DataTable();
            try
            {
                dt = getSetup();
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        dr = dt.Rows[0];
                        //string strname = dr["sign"].ToString();
                        string strHname = (string)HI7.Class.HIUility.IsNullString(dr["hi_hsp_nm"]);
                        HI7.Class.HIUility._HOSPITALNAME = "โรงพยาบาล"+strHname;
                        
                    }
                    else
                    {
                        HI7.Class.HIUility._HCODE = "";
                    }
                }
                else
                {
                    HI7.Class.HIUility._HCODE = "";
                }

            }
            catch (Exception ex)
            {
                HI7.Class.HIUility._HCODE = "";
                Growl.Warning("getHostname Warning!!!");
            }
        }

        public static string getCHANWAT()
        {
            //string strlcCode = lcCode;
            string strSql = "SELECT * FROM hi.changwat WHERE namechw = '" + _CHANWAT + "'";
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
                        string codechanwat = dr["chwpart"].ToString();
                        return codechanwat;
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
        public static string getAMPHUR()
        {
            //string strlcCode = lcCode;
            string chanwat = getCHANWAT();
            string strSql = "SELECT * FROM hi.ampur WHERE chwpart = '" + chanwat + "' AND nameampur ='" + _AMPHUR + "'";
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
                        string codeamppart = dr["amppart"].ToString();
                        return codeamppart;

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
        public static string getTUMBON()
        {
            string chanwat = getCHANWAT();
            string amppur = getAMPHUR();
            //string strlcCode = lcCode;
            string strSql = "SELECT * FROM hi.tumbon WHERE"
                + " chwpart = '" + chanwat + "'"
                + " AND amppart ='" + amppur + "'"
                + " AND nametumb ='" + _TUMBON + "'";
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
                        string codeTumbon = dr["tmbpart"].ToString();
                        return codeTumbon;

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
        //ลบข้อมูล ตรวจสอบแฟ้ม incoth
        public static DataTable checkIncoth(string vn)
        {
            string strVN = vn;
            string strSql = "SELECT * FROM incoth WHERE incoth.vn = '" + vn+"'";

            DataTable dt = new System.Data.DataTable();
            Dictionary<string, object> dictData = new Dictionary<string, object>();
            dictData.Add("query", strSql);

            try
            {
                dt = Hi7.Class.APIConnect.getDataTable("query/hi7_query", "POST", dictData);
                if (dt != null)
                {

                    return dt;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        //ลบข้อมูล ตรวจสอบแฟ้ม lbbk
        public static DataTable checkLbbk(string vn)
        {
            string strVN = vn;
            string strSql = "SELECT * FROM lbbk WHERE lbbk.vn = '" + vn + "'";

            DataTable dt = new System.Data.DataTable();
            Dictionary<string, object> dictData = new Dictionary<string, object>();
            dictData.Add("query", strSql);

            try
            {
                dt = Hi7.Class.APIConnect.getDataTable("query/hi7_query", "POST", dictData);
                if (dt != null)
                {

                    return dt;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        //ลบข้อมูล ตรวจสอบแฟ้ม xryrqt
        public static DataTable checkXryrqt(string vn)
        {
            string strVN = vn;
            string strSql = "SELECT * FROM xryrqt WHERE xryrqt.vn = '" + vn + "'";

            DataTable dt = new System.Data.DataTable();
            Dictionary<string, object> dictData = new Dictionary<string, object>();
            dictData.Add("query", strSql);

            try
            {
                dt = Hi7.Class.APIConnect.getDataTable("query/hi7_query", "POST", dictData);
                if (dt != null)
                {

                    return dt;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        //checkInsure
        public static DataTable CheckInsure(string hn,string vn)
        {
            string strSql = "SELECT * FROM insure WHERE insure.vn = '" + vn + "'";
            DataTable dt = new System.Data.DataTable();
            Dictionary<string, object> dictData = new Dictionary<string, object>();
            dictData.Add("query", strSql);
            string strSql2 = "SELECT * FROM insure WHERE insure.hn = '" + hn + "' ORDER BY insure.id DESC LIMIT 1";
            DataTable dt2 = new System.Data.DataTable();
            Dictionary<string, object> dictData2 = new Dictionary<string, object>();
            dictData2.Add("query", strSql2);
            

            try
            {
                dt = Hi7.Class.APIConnect.getDataTable("query/hi7_query", "POST", dictData);
                if (dt.Rows.Count != 0)
                {

                    return dt;
                }
                else
                {                   

                    dt2 = Hi7.Class.APIConnect.getDataTable("query/hi7_query", "POST", dictData2);
                    if (dt2.Rows.Count != 0)
                    {

                        return dt2;
                    }
                    else {
                        return null;
                    }
                    
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        //ลบข้อมูล ตรวจสอบแฟ้ม oprt
        public static DataTable checkOprt(string vn)
        {
            string strVN = vn;
            string strSql = "SELECT * FROM oprt WHERE oprt.vn = '" + vn + "'";

            DataTable dt = new System.Data.DataTable();
            Dictionary<string, object> dictData = new Dictionary<string, object>();
            dictData.Add("query", strSql);

            try
            {
                dt = Hi7.Class.APIConnect.getDataTable("query/hi7_query", "POST", dictData);
                if (dt != null)
                {

                    return dt;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        //ลบข้อมูล ตรวจสอบแฟ้ม dt
        public static DataTable checkDt(string vn)
        {
            string strVN = vn;
            string strSql = "SELECT * FROM dt WHERE dt.vn = '" + vn + "' and (dt.dnt IS NOT NULL or dt.dnt != '')";

            DataTable dt = new System.Data.DataTable();
            Dictionary<string, object> dictData = new Dictionary<string, object>();
            dictData.Add("query", strSql);

            try
            {
                dt = Hi7.Class.APIConnect.getDataTable("query/hi7_query", "POST", dictData);
                if (dt != null)
                {

                    return dt;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        //ลบข้อมูล ตรวจสอบแฟ้ม Ovst
        public static DataTable checkOvst(string vn)
        {
            string strVN = vn;
            string strSql = "SELECT * FROM ovst WHERE ovst.vn = '" + vn + "' and (ovst.dct IS NOT NULL or ovst.dct != '')";

            DataTable dt = new System.Data.DataTable();
            Dictionary<string, object> dictData = new Dictionary<string, object>();
            dictData.Add("query", strSql);

            try
            {
                dt = Hi7.Class.APIConnect.getDataTable("query/hi7_query", "POST", dictData);
                if (dt != null)
                {

                    return dt;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        //คำลั่งลบข้อมูลออกจากการลงทะเบียน Ovst
        public static bool removeOvst(string vn)
        {
            string strSQL = "DELETE FROM ovst WHERE ovst.vn = '"+vn+"'";

            Dictionary<string, object> dictData = new Dictionary<string, object>();

            try
            {
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
                Growl.Error("removeOvst say:\r\n" + ex.Message);
                return false;
            }

        }
        //คำลั่งลบข้อมูลออกจากการลงทะเบียน Ovst
        public static bool removeOtoday(string otoday,string vn)
        {
            string strSQL = "DELETE FROM "+ otoday + " WHERE "+ otoday + ".vn = '" + vn + "'";

            Dictionary<string, object> dictData = new Dictionary<string, object>();

            try
            {
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
                Growl.Error("removeOvst say:\r\n" + ex.Message);
                return false;
            }

        }
        //คำลั่งลบข้อมูลออกจากการลงทะเบียน removeInsure
        public static bool removeInsure(string vn)
        {
            string strSQL = "DELETE FROM insure WHERE insure.vn = '" + vn + "'";

            Dictionary<string, object> dictData = new Dictionary<string, object>();

            try
            {
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
                Growl.Error("removeInsure say:\r\n" + ex.Message);
                return false;
            }

        }
        //คำลั่งลบข้อมูลออกจากการลงทะเบียน Hi7visittoday
        public static bool removeHi7visittoday(string vn)
        {
            string strSQL = "DELETE FROM hi7visittoday WHERE hi7visittoday.vn = '" + vn + "'";

            Dictionary<string, object> dictData = new Dictionary<string, object>();

            try
            {
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
                Growl.Error("removeOvst say:\r\n" + ex.Message);
                return false;
            }

        }
        //คำลั่งลบข้อมูลออกจาก Q4U แฟ้ม Q4U_queue
        public static bool removeQ4uqueue(string vn)
        {
            string strSQL = "DELETE FROM q4u_queue WHERE q4u_queue.vn = '" + vn + "'";

            Dictionary<string, object> dictData = new Dictionary<string, object>();

            try
            {
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
                Growl.Error("Q4uqueue say:\r\n" + ex.Message);
                return false;
            }

        }
        //ตรวจสอบ Otoday oDDMMYY ตัวอย่า o061265
        public static DataTable checkODDMMYY(string otoday)
        {
            string strVN = otoday;
            string strSql = "SELECT * FROM "+ strVN + "";

            DataTable dt = new System.Data.DataTable();
            Dictionary<string, object> dictData = new Dictionary<string, object>();
            dictData.Add("query", strSql);

            try
            {
                dt = Hi7.Class.APIConnect.getDataTable("query/hi7_query", "POST", dictData);
                if (dt != null)
                {

                    return dt;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public static string getCodeicd9id(string icd9cm)
        {
            string strHN = icd9cm;
            string strSql = "SELECT prcd.id FROM prcd WHERE prcd.codeprcd = '"+ strHN+"'";
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
                        string strname = (string)IsNullString(dr["id"]).ToString();
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
        public static string Convertnamemonthtonumber(string monthName)
        {
            // สร้างคู่ key-value สำหรับรหัสเดือนและชื่อเดือน
            Dictionary<string, string> monthCode = new Dictionary<string, string>
        {
            { "ม.ค.","01" },
            { "ก.พ.","02" },
            { "มี.ค.","03" },
            { "เม.ย.","04" },
            { "พ.ค.","05" },
            { "มิ.ย.","06" },
            { "ก.ค.","07" },
            { "ส.ค.","08" },
            { "ก.ย.","09" },
            { "ต.ค.","10" },
            { "พ.ย.","11" },
            { "ธ.ค.","12" }
        };

            // ค้นหาชื่อเดือนจากรหัสเดือนที่กำหนด
            if (monthCode.ContainsKey(monthName))
            {
                return monthCode[monthName];
            }

            return string.Empty;
        }
        public static string[] Getvaluedatehi7(string day,string month,string year)
        {
            string[] dateArray = new string[3];
            dateArray[0] = day;
            dateArray[1] = Convertnamemonthtonumber(month);
            dateArray[2] = year;
            return dateArray;

        }
        //วันปัจุบันจาก SERVER
        public static string Getdateserver()
        {
            
            string strSql = "SELECT concat((case dayofweek(CURDATE()) when 1 then 'วันอาทิตย์' when 2 then 'วันจันทร์' when 3 then 'วันอังคาร' when 4 then 'วันพุธ' when 5 then 'วันพฤหัสบดี' when 6 then 'วันศุกร์' when 7 then 'วันเสาร์' END),'ที่',day(CURDATE()),' ',(case month(CURDATE()) when 1 then 'มกราคม'when 2 then 'กุมภาพันธ์'when 3 then 'มีนาคม' when 4 then 'เมษายน' when 5 then 'พฤษภาคม' when 6 then 'มิถุนายน' when 7 then 'กรกฏาคม' when 8 then 'สิงหาคม' when 9 then 'กันยายน' when 10 then 'ตุลาคม' when 11 then 'พฤศจิกายน' when 12 then 'ธันวาคม' end),' พ.ศ. ', year(CURDATE()) +543) as datethai,DATE_FORMAT(DATE_ADD(CURDATE(), INTERVAL 543 YEAR), '%d/%m/%Y') AS formatted_date";
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
                        string strname = (string)IsNullString(dr["formatted_date"]).ToString();
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
        public static string Getdateserverdatethai()
        {

            string strSql = "SELECT concat((case dayofweek(CURDATE()) when 1 then 'วันอาทิตย์' when 2 then 'วันจันทร์' when 3 then 'วันอังคาร' when 4 then 'วันพุธ' when 5 then 'วันพฤหัสบดี' when 6 then 'วันศุกร์' when 7 then 'วันเสาร์' END),'ที่',day(CURDATE()),' ',(case month(CURDATE()) when 1 then 'มกราคม'when 2 then 'กุมภาพันธ์'when 3 then 'มีนาคม' when 4 then 'เมษายน' when 5 then 'พฤษภาคม' when 6 then 'มิถุนายน' when 7 then 'กรกฏาคม' when 8 then 'สิงหาคม' when 9 then 'กันยายน' when 10 then 'ตุลาคม' when 11 then 'พฤศจิกายน' when 12 then 'ธันวาคม' end),' พ.ศ. ', year(CURDATE()) +543) as datethai,DATE_FORMAT(DATE_ADD(CURDATE(), INTERVAL 543 YEAR), '%d/%m/%Y') AS formatted_date";
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
                        string strname = (string)IsNullString(dr["datethai"]).ToString();
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
        public static string Getdateserverdatethaishort()
        {

            string strSql = "SELECT concat(day(CURDATE()),' ',(case month(CURDATE()) when 1 then 'ม.ค.'when 2 then 'ก.พ.'when 3 then 'มี.ค.' when 4 then 'เม.ย.' when 5 then 'พ.ค.' when 6 then 'มิ.ย.' when 7 then 'ก.ค.' when 8 then 'ส.ค.' when 9 then 'ก.ย.' when 10 then 'ต.ค.' when 11 then 'พ.ย.' when 12 then 'ธ.ค.' end),' พ.ศ. ', year(CURDATE()) +543) as datethai,DATE_FORMAT(DATE_ADD(CURDATE(), INTERVAL 543 YEAR), '%d/%m/%Y') AS formatted_date";
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
                        string strname = (string)IsNullString(dr["datethai"]).ToString();
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
        //public static string Getvaluedatehi7(string day)
        //{

        //    string[] arr = day.Split('/');
        //    bool d = arr[0].Length == 1;
        //    string dd = d ? "0" + arr[0] : arr[0];
        //    bool m = arr[1].Length == 1;
        //    string mm = m ? "0" + arr[1] : arr[1];
        //    bool y = arr[2].Length == 1;
        //    string yy = y ? "0" + arr[1] : arr[1];


        //    return day;

        //}
        //ปิดท้าย
    }
}