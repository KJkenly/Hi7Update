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

namespace HI7.Class
{
    public static class HIUility2
    {

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
        public static string _VN;
        public static string _AN;
        public static string _HCODE;
        public static string _VSTDTTM;
        public static string _VSTDATE;
        public static string _VSTTIME;
        public static string _PTTYPE;
        public static string _CLN;
        public static string _DCT;
        public static string _TXTSEARCH;
        public static string _CONSULTID;
        public static String _POPID;
        public static String Vnp ;
        public static String HNR ;
        public static String RefNo ;
        //Para get Prcd - codeprcd,nameprcd,price
        //get PRCD.CODEPRCD
        public static string pCODEPRCD;
        //get PRCD.NAMEPRCD
        public static String pNAMEPRCD;
        //get PRCD.CGD
        public static string pCGD;
        //get PRCD.INCOME
        public static string pINCOME;
        //get PRCD.ID
        public static string pICD9ID;
        //get Charge Ref.  PTTYPE - PRCD.PRICEPRCD,PRCD.PRICECGD
        public static string pCHARGE;

        //Para get Lab - 




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
            HIUility2.SET_LABEL_FONT = "tahoma";
            HIUility2.SET_LABEL_FONT_SIZE = 16;
            HIUility2.SET_LABEL_COLOR = Color.White;

            //Button
            HIUility2.SET_BUTTON_FONT = "tahoma";
            HIUility2.SET_BUTTON_FONT_SIZE = 16;
            HIUility2.SET_BUTTON_COLOR = Color.White;
            //TextBox
            HIUility2.SET_TEXTBOX_FONT = "tahoma";
            HIUility2.SET_TEXTBOX_FONT_SIZE = 16;
            HIUility2.SET_TEXTBOX_COLOR = Color.White;


        }


        public static void initialHeaderControls()
        {
            // Label
            HIUility2.SET_LABEL_FONT = "tahoma";
            HIUility2.SET_LABEL_FONT_SIZE = 20;
            HIUility2.SET_LABEL_COLOR = Color.White;

            //Button
            HIUility2.SET_BUTTON_FONT = "tahoma";
            HIUility2.SET_BUTTON_FONT_SIZE = 20;
            HIUility2.SET_BUTTON_COLOR = Color.White;
            //TextBox
            HIUility2.SET_TEXTBOX_FONT = "tahoma";
            HIUility2.SET_TEXTBOX_FONT_SIZE = 20;
            HIUility2.SET_TEXTBOX_COLOR = Color.White;


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

        public static bool InsertData(string nTable,string nFields,string nValues)
        {
            Dictionary<string, object> dictData = new Dictionary<string, object>();
            string strSQL = "insert into "+nTable+"(" + nFields + ") values(" + nValues+ ") ";
            dictData.Add("query", strSQL);
            bool status = Hi7.Class.APIConnect.Excecute("query/hi7_query", "POST", dictData);
            return status;            
        }
        //OP5 lookup Setup
        public static DataTable getSetup()
        {
            string strSQL = "select * from setup";

            DataTable dt = new System.Data.DataTable();
            Dictionary<string ,object> dictData = new Dictionary<string ,object>();
            dictData.Add("query", strSQL);
            try
            {
                dt = Hi7.Class.APIConnect.getDataTable("query/hi7_query","POST",dictData);
                if(dt != null)
                {
                    return dt;
                }
                else
                {
                    return null;
                }
            } catch (Exception ex)
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
            }else
            {
                string x = strICD10.Substring(1, 1);
                int n;
                bool isNumeric = int.TryParse(x, out n);
                if (isNumeric)
                {
                    string strSql = "SELECT i.icd10,x.namedx,i.icd10name,i.name_t from icd101 as i  inner join dx as x on i.icd10 = x.codedx where icd_use ='1' and i.icd10 like  UPPER('"+strICD10 +"%') limit 1000";
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

            string strSql = "SELECT p.id,p.codeprcd,p.nameprcd,p.fullname,p.cgd,p.income,p.ptright,p.priceprcd,p.pricecgd,i.namecost from prcd as p "+
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

            string strSql = "SELECT r.rtnno,r.labcode,l.labname from rtnldt as r "+
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
            " INNER JOIN lab as l on r.labcode = l.labcode where r.rtnno = " + strlcCode ;

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
        //แปลงรหัสคลินิกเป็นชื่อ
        public static string c2n_cln(string lcCode)
        {
            string strlcCode = lcCode;
            string strSql = "select cln.namecln from cln where cln = '" + strlcCode +"'";
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
            string strSql = "select male.namemale from male where male = '" + strlcCode +"'";
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
            " INNER JOIN typemed_claim as t on m.type = t.code_tme where o.vn = " + strVN  ;
      
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
            " INNER JOIN typemed_claim as t on m.type = t.code_tme where o.hn = "  + strHN + "  ORDER BY o.vn desc " ;

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
            " where o.vn = " + strVN ;

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
            string strSql = "select q.vn,q.an,o.hn,CONCAT(DATE_FORMAT( q.vstdate , '%d-%m' ),'-',DATE_FORMAT( q.vstdate , '%Y' ) +543 )  as vstdate,q.vsttime,q.xrycode,x.xryname,"+
            "x.cgd,q.dct,if (q.rgtok = 0,'ยังไม่เสร็จ','เสร็จแล้ว') as cstatus ,TIME_FORMAT(time(q.vsttime * 100), '%H:%i') as vsttime," +
            "(SELECT(CASE WHEN LENGTH(q.dct) = 5 THEN(select concat(dct.fname, ' ', dct.lname) as fndct from dct where dct.lcno = q.dct limit 1)" +
            " WHEN LENGTH(q.dct) = 4 THEN concat((select concat(dct.fname, ' ', dct.lname) as fndct from dct where dct.dct = substr(q.dct, 1, 2) limit 1), '/', (select concat(dct.fname, ' ', dct.lname) as fndct from dct where dct.dct = substr(q.dct, 3, 2) limit 1)) " +
            " WHEN LENGTH(q.dct) = 3 THEN(select concat(dentist.fname, ' ', dentist.lname) as fndct from dentist where dentist.codedtt = q.dct limit 1) " +
            " WHEN LENGTH(q.dct) = 2 THEN(select concat(dct.fname, ' ', dct.lname) as fndct from dct where dct.dct = q.dct limit 1) else '' END ) as namedct from dct GROUP BY namedct ) as namedct " +
            " from ovst as o" +
            " INNER JOIN xryrqt as q on o.vn = q.vn" +
            " INNER JOIN xray as x on q.xrycode = x.xrycode" +
            " where q.an = 0 and o.vn = " + strVN + " order by vstdate asc" ;

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
            string strSql = "SELECT p.*,(CASE p.male WHEN 1 THEN IF(p.mrtlst< 6, IF(TIMESTAMPDIFF(year, p.brthdate, NOW() ) < 15, 'ด.ช.', 'นาย' ),"+
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
            string strSQL = "select * from hi7ptimage where hn='" + HI7.Class.HIUility2._HN + "'";
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
                            img.Save("C:\\PT_IMG\\" + HI7.Class.HIUility2._POPID + ".jpg");

                            //  base64String = Convert.ToBase64String(ms.ToArray());
                            //   addimage(base64String);

                        }
                    }
                }
                //ส่วนแสดงภาพ
                BitmapImage bitmapImage = new BitmapImage();
                bitmapImage.BeginInit();
                bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
                bitmapImage.UriSource = new Uri(@"C://PT_IMG//" + HI7.Class.HIUility2._POPID + ".jpg", UriKind.RelativeOrAbsolute);
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
        public static string getGradBmi(string bw,string  heigth)
        {
            double lnBw, lnHe, bmi;
            string lcRtn;
            lnBw = double.Parse(bw);
            lnHe = double.Parse(heigth);           
            bmi = lnBw / ((lnHe / 100) * (lnHe / 100));
            if (bmi > 29.9)
            {
                lcRtn =  "[" + bmi.ToString("0.##") + "]" + " อ้วนมาก";
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


        //ปิดท้าย
    }
}