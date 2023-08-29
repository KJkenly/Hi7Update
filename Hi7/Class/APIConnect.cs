using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Net;
using System.Net.Http;
using System.Xml;


namespace Hi7.Class
{
    public static class APIConnect
    {

        public static string TOKEN_KEY;
        public static string SECRET;
        public static string API_SERVER; 
        public static string IP_PRINTQUEUE;
        public static string IPSERVERPTINT;
        public static string API_PATH;
        public static string API_USER;
        public static string API_USER_ID;
        public static JObject JSON_DATA;
        public static JArray JSON_DATA_MODULES;
        public static JArray JSON_DATA_ROLES;
        public static string FONT;
        public static string FONT_SIZE;
        public static string PATHPICPT;

        public static string NHSO_TOKEN;
        public static string PART_DEVICESMARTCARD;
        //= "http://mis.phoubon.in.th/api/hi7dev";

        public static string APP_PATH;

        public static string USER_LOGIN, USER_IDLOGIN;
        public static string SELECT_ROLE, INSERT_ROLE, UPDATE_ROLE, DELETE_ROLE;
        public static string HNrespon;
        /* public static DataTable getDatafromAPI(string strRoute, string methodType)
               {
                   try
                   {
                       JObject json;
                       DataTable dt = new DataTable();


                       //      string API_URL = hi7.MySQLConnect.API;
                       //     string token = hi7.APIConnect.TOKEN_KEY;
                       string API_URL = "";
                       string token = "";

                       var request = WebRequest.Create(API_URL + strRoute) as System.Net.HttpWebRequest;
                       request.Method = methodType;

                       request.Headers.Add(HttpRequestHeader.Authorization, "Bearer " + token + " ");
                       request.ContentLength = 0;

                       using (var response = request.GetResponse() as System.Net.HttpWebResponse)
                       {
                           using (var reader = new System.IO.StreamReader(response.GetResponseStream()))
                           {
                               json = JObject.Parse(reader.ReadToEnd());
                               JArray jsonArray = (JArray)json.SelectToken("results");
                               dt = JsonConvert.DeserializeObject<DataTable>(jsonArray.ToString());
                           }
                       }

                       return dt;
                   }
                   catch (Exception ex)
                   {
                       return null;

                   }
               }
        */

        public static int Timeout { get; set; }

        public static JObject getToken(string API_URL, Dictionary<string, object> dictData)
        {
            WebClient webClient = new WebClient();
            byte[] resByte;
            string resString;
            byte[] reqString;
            string api = API_URL;

            try
            {

                webClient.Headers.Add("content-type", "application/json");
                reqString = Encoding.Default.GetBytes(JsonConvert.SerializeObject(dictData, (Newtonsoft.Json.Formatting)System.Xml.Formatting.Indented));
                resByte = webClient.UploadData(api, "post", reqString);
                resString = Encoding.Default.GetString(resByte);
                JObject json = JObject.Parse(resString);
                webClient.Dispose();
                return json;




            }
            catch (Exception ex)
            {
                return null;

                Console.WriteLine(ex.Message);
            }

        }


        public static DataTable getDataTable(string strRoute, string methodType, Dictionary<string, object> dictData)
        {
            try
            {
                JObject json;
                DataTable dt = new DataTable();
                string API_URL = API_SERVER;
                //  string token = TOKEN_KEY;

                byte[] reqString;
                string resString;
                byte[] resByte;
                WebClient webClient = new WebClient();

                webClient.Headers.Add("content-type", "application/json");
                webClient.Headers.Add(HttpRequestHeader.Authorization, "Bearer " + Hi7.Class.APIConnect.TOKEN_KEY + " ");
                reqString = Encoding.Default.GetBytes(JsonConvert.SerializeObject(dictData, (Newtonsoft.Json.Formatting)System.Xml.Formatting.Indented));




                resByte = webClient.UploadData(API_URL + strRoute, methodType, reqString);
                resString = Encoding.UTF8.GetString(resByte);

                json = JObject.Parse(resString);
                Console.WriteLine("resString");

                Console.WriteLine(resString);

                //  if ((Boolean)json.SelectToken("ok"))
                if ((Int16)json.SelectToken("statusCode") == 200)
                {

                    JArray jsonArray = (JArray)json.SelectToken("results");

                    dt = JsonConvert.DeserializeObject<DataTable>(jsonArray.ToString());
                    return dt;

                }
                else
                {
                    // Interaction.MsgBox("Error", MsgBoxStyle.Critical);
                    return null/* TODO Change to default(_) if this is not a reference type */;
                }
            }
            catch (Exception ex)
            {
                //  Interaction.MsgBox("Error getQueryfromAPI:" + ex.Message, MsgBoxStyle.Critical, "Error");
                return null/* TODO Change to default(_) if this is not a reference type */;
            }



        }


        public static bool Excecute(string strRoute, string methodType, Dictionary<string, object> dictData)
        {
            try
            {
                JObject json;
                DataTable dt = new DataTable();
                string API_URL = API_SERVER;
                //  string token = TOKEN_KEY;

                byte[] reqString;
                string resString;
                byte[] resByte;
                WebClient webClient = new WebClient();

                webClient.Headers.Add("content-type", "application/json");
                webClient.Headers.Add(HttpRequestHeader.Authorization, "Bearer " + Hi7.Class.APIConnect.TOKEN_KEY + " ");
                reqString = Encoding.Default.GetBytes(JsonConvert.SerializeObject(dictData, (Newtonsoft.Json.Formatting)System.Xml.Formatting.Indented));




                resByte = webClient.UploadData(API_URL + strRoute, methodType, reqString);
                resString = Encoding.UTF8.GetString(resByte);

                json = JObject.Parse(resString);
                HNrespon = json.SelectToken("results['insertId']").ToString();
                Console.WriteLine("resString");

                Console.WriteLine(resString);

                //  if ((Boolean)json.SelectToken("ok"))
                if ((Int16)json.SelectToken("statusCode") == 200)
                {
                    return true;

                }
                else
                {
                    // Interaction.MsgBox("Error", MsgBoxStyle.Critical);
                    return false;

                }
            }
            catch (Exception ex)
            {
                return false;
            }



        }


        public static bool getQueryData(string strRoute, string methodType, Dictionary<string, object> dictData)
        {
            byte[] resByte;
            string resString;
            byte[] reqString;
            JObject json;
            string API_URL = API_SERVER;
            try
            {
                WebClient webClient = new WebClient();
                // Dim request = TryCast(System.Net.WebRequest.Create(API_URL & "users/info"), System.Net.HttpWebRequest)


                webClient.Headers.Add("content-type", "application/json");
                webClient.Headers.Add(HttpRequestHeader.Authorization, "Bearer " + Hi7.Class.APIConnect.TOKEN_KEY + " ");
                reqString = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(dictData, (Newtonsoft.Json.Formatting)System.Xml.Formatting.Indented));


                resByte = webClient.UploadData(API_URL + strRoute, methodType, reqString);
                resString = Encoding.Default.GetString(resByte);
                json = JObject.Parse(resString);

                if ((Boolean)json.SelectToken("ok"))
                {
                    //   Interaction.MsgBox("success", MsgBoxStyle.Information);
                    return true;
                }
                else
                {
                    //  Interaction.MsgBox("Error", MsgBoxStyle.Critical);
                    return false;
                }
            }
            catch (Exception ex)
            {
                // Interaction.MsgBox("Error", ex.Message);
                return false;
            }
        }

        public static bool updateDataToAPI(string API_URL, string strToken, string strRoute, string methodType, Dictionary<string, object> dictData, string strCondition)
        {
            byte[] resByte;
            string resString;
            byte[] reqString;
            JObject json;

            try
            {
                WebClient webClient = new WebClient();
                // Dim request = TryCast(System.Net.WebRequest.Create(API_URL & "users/info"), System.Net.HttpWebRequest)
                string t = strToken;

                webClient.Headers.Add("content-type", "application/json");
                webClient.Headers.Add(HttpRequestHeader.Authorization, "Bearer " + t + " ");
                reqString = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(dictData, (Newtonsoft.Json.Formatting)System.Xml.Formatting.Indented));


                resByte = webClient.UploadData(API_URL + strRoute + strCondition, methodType, reqString);
                resString = Encoding.Default.GetString(resByte);
                json = JObject.Parse(resString);

                if (json.SelectToken("statusCode").ToString() == "200")
                {
                    //   Interaction.MsgBox("success", MsgBoxStyle.Information);
                    return true;
                }
                else
                {
                    //  Interaction.MsgBox("Error", MsgBoxStyle.Critical);
                    return false;
                }
            }
            catch (Exception ex)
            {
                // Interaction.MsgBox("Error", ex.Message);
                return false;
            }
        }
        public static bool deleteDataToAPI(string API_URL, string strToken, string strRoute, string strCondition)
        {
            HttpClient client = new HttpClient();
            try
            {
                string t = strToken;
                client.BaseAddress = new Uri(API_URL);
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", t);
                var respone = client.DeleteAsync(strRoute + strCondition).Result;

                if (respone.StatusCode.ToString() == "200")
                {
                    //     Interaction.MsgBox("success", MsgBoxStyle.Information);
                    return true;
                }
                else
                {
                    //Interaction.MsgBox("Error", MsgBoxStyle.Critical);
                    return false;
                }
            }
            catch (Exception ex)
            {
                //   Interaction.MsgBox("Error", ex.Message);
                return false;
            }
        }

        public static void getConfgXML()
        {
            XmlTextReader m_xmlr;
            string strConfig_Path = @"C:\HI7\hi7config.xml";

            try
            {

                // m_xmlr = New XmlTextReader(My.Application.Info.DirectoryPath & "\Data\config.xml")
                m_xmlr = new XmlTextReader(strConfig_Path);
                m_xmlr.WhitespaceHandling = WhitespaceHandling.None;
                // read the xml declaration and advance to family tag

                // read the Config tagn
                m_xmlr.Read();

                // Load the Loop
                m_xmlr.Read();

                while (!m_xmlr.EOF)
                {
                    //// Go to the name tag
                    m_xmlr.Read();
                    m_xmlr.GetAttribute("API_SERVER");
                    // Read elements firstname and lastname
                    m_xmlr.Read();

                    // '   pPassword = m_xmlr.ReadElementString("value")
                    APIConnect.API_SERVER = m_xmlr.ReadElementString("value");
                    // Me.txtoutputfile.Text = pPATH

                    m_xmlr.Read();
                    m_xmlr.GetAttribute("APP_PATH");
                    // Read elements firstname and lastname
                    m_xmlr.Read();
                    APIConnect.API_PATH = m_xmlr.ReadElementString("value");


                    m_xmlr.Read();
                    m_xmlr.GetAttribute("API_USER");
                    // Read elements firstname and lastname
                    m_xmlr.Read();
                    APIConnect.API_USER = m_xmlr.ReadElementString("value");



                    m_xmlr.Read();
                    m_xmlr.GetAttribute("API_USER_ID");
                    // Read elements firstname and lastname
                    m_xmlr.Read();
                    APIConnect.API_USER_ID = m_xmlr.ReadElementString("value");


                    m_xmlr.Read();
                    m_xmlr.GetAttribute("JSON_DATA_ROLES");
                    // Read elements firstname and lastname
                    m_xmlr.Read();
                    APIConnect.JSON_DATA_ROLES = JArray.Parse(m_xmlr.ReadElementString("value"));
                    // JObject.Parse( m_xmlr.ReadElementString("value"));


                    m_xmlr.Read();
                    m_xmlr.GetAttribute("JSON_DATA_MODULES");
                    // Read elements firstname and lastname
                    m_xmlr.Read();
                    APIConnect.JSON_DATA_MODULES = JArray.Parse(m_xmlr.ReadElementString("value")); //.Parse(m_xmlr.ReadElementString("value"));


                    m_xmlr.Read();
                    m_xmlr.GetAttribute("TOKEN_KEY");
                    // Read elements firstname and lastname
                    m_xmlr.Read();
                    APIConnect.TOKEN_KEY = m_xmlr.ReadElementString("value");

                    m_xmlr.Read();
                    m_xmlr.GetAttribute("NHSO_TOKEN");
                    // Read elements firstname and lastname
                    m_xmlr.Read();
                    APIConnect.NHSO_TOKEN = m_xmlr.ReadElementString("value");
                    //Read Font and Size 
                    m_xmlr.Read();
                    m_xmlr.GetAttribute("FONT");
                    // Read elements firstname and lastname
                    m_xmlr.Read();
                    APIConnect.FONT = m_xmlr.ReadElementString("value");
              

                    m_xmlr.Read();
                    m_xmlr.GetAttribute("FONT_SIZE");
                    // Read elements firstname and lastname
                    m_xmlr.Read();
                    APIConnect.FONT_SIZE = m_xmlr.ReadElementString("value");

                    m_xmlr.Read();
                    m_xmlr.GetAttribute("USERLOGIN");
                    // Read elements firstname and lastname
                    m_xmlr.Read();
                    APIConnect.USER_LOGIN = m_xmlr.ReadElementString("value");
                    //HI7.Class.HIUility.USERLOGIN = m_xmlr.ReadElementString("value");

                    m_xmlr.Read();
                    m_xmlr.GetAttribute("PATHDEVICESMARTCARD");
                    m_xmlr.Read();
                    APIConnect.PART_DEVICESMARTCARD = m_xmlr.ReadElementString("value");

                    m_xmlr.Read();
                    m_xmlr.GetAttribute("USERIDLOGIN");
                    m_xmlr.Read();
                    APIConnect.USER_IDLOGIN = m_xmlr.ReadElementString("value");
                    
                    m_xmlr.Read();
                    m_xmlr.GetAttribute("IP_PTINTQ");
                    m_xmlr.Read();
                    APIConnect.IP_PRINTQUEUE = m_xmlr.ReadElementString("value");

                    m_xmlr.Read();
                    m_xmlr.GetAttribute("SERVERPRINT");
                    m_xmlr.Read();
                    APIConnect.IPSERVERPTINT = m_xmlr.ReadElementString("value");

                    m_xmlr.Read();
                    m_xmlr.GetAttribute("PATHIMAGE");
                    m_xmlr.Read();
                    APIConnect.PATHPICPT = m_xmlr.ReadElementString("value");
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


    }
}