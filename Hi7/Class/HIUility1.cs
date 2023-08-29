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

namespace HI7.Class
{
    public static class HIUility1
    {

        public static string HNp="55217";
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


        //para
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
        public static object IsNullString(object s)
        {
            bool isNumber = int.TryParse(( string? ) s, out int numericValue);
            if ( isNumber )
            {
                return s;
            }
            else {
                if ( DBNull.Value.Equals(s) )
                {
                    return "";
                }
                else
                {
                    return s;
                }
            }
          
        }


        public static string c2n_cln(string cln)

        {

            string strCLN = cln;
            string strSql = "select cln.namecln from cln where cln = '"+strCLN+"'";
            DataRow dr;
            DataTable dt = new System.Data.DataTable();
            Dictionary<string, object> dictData = new Dictionary<string, object>();
            dictData.Add("query", strSql);

            try
            {
                dt = Hi7.Class.APIConnect.getDataTable("query/hi7_query", "POST", dictData);
                if ( dt != null )
                {
                    if ( dt.Rows.Count > 0 )
                    {
                        dr = dt.Rows[0];
                        string strnamecln = ( string ) IsNullString(dr["namecln"]);
                        return strnamecln;

                    }
                    else {
                        return "";
                    }
                }
                else
                {
                    return "";
                }

            }
            catch ( Exception ex )
            {
                return "";
            }


        }
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
        //public static Task<BitmapSource> ToBitmapSourceAsync(this Bitmap bitmap)
        //{
         
        //    return Task.Run(() =>
        //    {

          
        //        using (System.IO.MemoryStream memory = new System.IO.MemoryStream())
        //        {


        //            var img = Image.FromStream(m);

        //            bitmap.Save(memory,  System.Drawing.Imaging.ImageFormat.Jpeg);
        //            memory.Position = 0;
        //            BitmapImage bitmapImage = new BitmapImage();
        //            bitmapImage.BeginInit();
        //            bitmapImage.StreamSource = memory;
        //            bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
        //            bitmapImage.EndInit();
        //            bitmapImage.Freeze();
        //            // copy.Save(memory, ImageFormat.Jpeg);
        //            //   MemoryStream ms = new MemoryStream();
        //            FileStream file = new FileStream(@"C:\PT_IMG\1111.jpg", FileMode.Create, FileAccess.Write);
        //            memory.WriteTo(file);
        //            file.Close();
        //            memory.Close();
        //            return bitmapImage as BitmapSource;
        //        }
        //    });

        //}

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

        public static Int64 Hn2Age(string hn)
        {
            string strHN = hn;
            string strSql = "select timestampdiff(year,pt.brthdate,CURDATE()) AS age  from hi.pt where pt.hn = " + strHN;
            DataRow dr;
            DataTable dt = new System.Data.DataTable();
            Dictionary<string, object> dictData = new Dictionary<string, object>();
            dictData.Add("query", strSql);

            try
            {
                dt = Hi7.Class.APIConnect.getDataTable("query/hi7_query", "POST", dictData);
                if ( dt != null )
                {
                    if ( dt.Rows.Count > 0 )
                    {
                        dr = dt.Rows[0];

                        return ( Int64 ) dr.ItemArray[0];

                    }
                    else
                    {
                        return 0;
                    }
                }
                else
                {
                    return 0;
                }
            }
            catch ( Exception ex )
            {
                return 0;
            }
        }
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
                if ( dt != null )
                {
                    if ( dt.Rows.Count > 0 )
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
            catch ( Exception ex )
            {
                return "";
            }
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
                dt = Hi7.Class.APIConnect.getDataTable("query/hi7_query", "POST", dictData);
                if ( dt != null )
                {
                    return dt;
                }
                else
                {
                    return null;
                }
            }
            catch ( Exception ex )
            {
                return null;
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


        //public static void initialAllDefualtControls()
        //{
        //    // Label
        //    HIUility.SET_LABEL_FONT = "tahoma";
        //    HIUility.SET_LABEL_FONT_SIZE = 16;
        //    HIUility.SET_LABEL_COLOR = Color.White;

        //    //Button
        //    HIUility.SET_BUTTON_FONT = "tahoma";
        //    HIUility.SET_BUTTON_FONT_SIZE = 16;
        //    HIUility.SET_BUTTON_COLOR = Color.White;
        //    //TextBox
        //    HIUility.SET_TEXTBOX_FONT = "tahoma";
        //    HIUility.SET_TEXTBOX_FONT_SIZE = 16;
        //    HIUility.SET_TEXTBOX_COLOR = Color.White;


        //}


        //public static void initialHeaderControls()
        //{
        //    // Label
        //    HIUility.SET_LABEL_FONT = "tahoma";
        //    HIUility.SET_LABEL_FONT_SIZE = 20;
        //    HIUility.SET_LABEL_COLOR = Color.White;

        //    //Button
        //    HIUility.SET_BUTTON_FONT = "tahoma";
        //    HIUility.SET_BUTTON_FONT_SIZE = 20;
        //    HIUility.SET_BUTTON_COLOR = Color.White;
        //    //TextBox
        //    HIUility.SET_TEXTBOX_FONT = "tahoma";
        //    HIUility.SET_TEXTBOX_FONT_SIZE = 20;
        //    HIUility.SET_TEXTBOX_COLOR = Color.White;


        //}


    }
}