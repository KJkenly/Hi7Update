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

public static class APIConnect
{

    public static string TOKEN_KEY;
    public static string SECRET;
    public static string WEB_API;
    //= "http://mis.phoubon.in.th/api/hi7dev";
   
    public static string APP_PATH;
    public static string JSON_DATA;
    public static string USER_LOGIN;
    public static string SELECT_ROLE, INSERT_ROLE, UPDATE_ROLE, DELETE_ROLE;
    public static DataTable getDatafromAPI(string strRoute, string methodType)
    {
        try
        {
            JObject json;
            DataTable dt = new DataTable();


            //     string API_URL = hi7.MySQLConnect.API;
            //    string token = hi7.APIConnect.TOKEN_KEY;
            string API_URL = WEB_API + strRoute;
            //string token = "";

            var request = WebRequest.Create(API_URL) as System.Net.HttpWebRequest;
            request.Method = methodType;

            request.Headers.Add(HttpRequestHeader.Authorization, "Bearer " + TOKEN_KEY + " ");
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
            return null/* TODO Change to default(_) if this is not a reference type */;
        }
    }
    public static string getToken(string API_URL, Dictionary<string, object> dictData)
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
            return (string)json.SelectToken("rows");
        }
        catch (Exception ex)
        {
            return "";

            Console.WriteLine(ex.Message);
        }

    }


    public static DataTable getDatafromAPI2(string strRoute, string methodType, Dictionary<string, object> dictData)
    {
        try
        {
            JObject json;
            DataTable dt = new DataTable();
            string API_URL = WEB_API;
          string token = APIConnect.TOKEN_KEY;
        //    string token = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJpc3MiOiJoaWRldiIsImlhdCI6MTY1MzI5MjU4OCwiZXhwIjoxNjUzMzc4OTg4fQ.ZS_7qamSdhaUJX93OYVsqf2cien6sbCf0n_27OguZHM";
             byte[] reqString;
            string resString;
            byte[] resByte;
            WebClient webClient = new WebClient();

            webClient.Headers.Add("content-type", "application/json");
            webClient.Headers.Add(HttpRequestHeader.Authorization, "Bearer " + token + " ");
            reqString = Encoding.Default.GetBytes(JsonConvert.SerializeObject(dictData, (Newtonsoft.Json.Formatting)System.Xml.Formatting.Indented));

            


            resByte = webClient.UploadData(API_URL + strRoute, methodType, reqString);
            resString = Encoding.UTF8.GetString(resByte);

            json = JObject.Parse(resString);
            Console.WriteLine("resString");
           
            Console.WriteLine(resString);
            JArray jsonArray = (JArray)json.SelectToken("rows");
            //     if (json.SelectToken("statusCode").ToString() == "200")
            if (jsonArray.Count >0)
            {

             
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



    public static DataTable getQueryfromAPI(string API_URL, string strRoute, string methodType, string token, Dictionary<string, object> dictData)
    {
        try
        {
            JObject json;
            DataTable dt = new DataTable();
            byte[] reqString;
            string resString;
            byte[] resByte;
            WebClient webClient = new WebClient();

            webClient.Headers.Add("content-type", "application/json");
            webClient.Headers.Add(HttpRequestHeader.Authorization, "Bearer " + token + " ");
            reqString = Encoding.Default.GetBytes(JsonConvert.SerializeObject(dictData, (Newtonsoft.Json.Formatting)System.Xml.Formatting.Indented));

            resByte = webClient.UploadData(API_URL + strRoute, methodType, reqString);
            resString = Encoding.Default.GetString(resByte);

            json = JObject.Parse(resString);

            if (json.SelectToken("statusCode").ToString() == "200")
            {
                if (json.SelectToken("results").SelectToken("affectedRows").ToString() == null)
                {
                    //    MessageBox("affectedRows" + json.SelectToken("results").SelectToken("affectedRows").ToString(), MsgBoxStyle.Information);
                    return null/* TODO Change to default(_) if this is not a reference type */;
                }
                else
                {
                    JArray jsonArray = (JArray)json.SelectToken("results");
                    dt = JsonConvert.DeserializeObject<DataTable>(jsonArray.ToString());
                    return dt;
                }
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

    //public static void getConfgXML()
    //{
    //    XmlTextReader m_xmlr;
    //    string strConfig_Path = @"C:\HI7\hi7config.xml";

    //    try
    //    {

    //        // m_xmlr = New XmlTextReader(My.Application.Info.DirectoryPath & "\Data\config.xml")
    //        m_xmlr = new XmlTextReader(strConfig_Path);
    //        m_xmlr.WhitespaceHandling = WhitespaceHandling.None;
    //        // read the xml declaration and advance to family tag

    //        // read the Config tagn
    //        m_xmlr.Read();

    //        // Load the Loop
    //        m_xmlr.Read();

    //        while (!m_xmlr.EOF)
    //        {
    //            //// Go to the name tag
    //            m_xmlr.Read();
    //            m_xmlr.GetAttribute("SECRET");
    //            // Read elements firstname and lastname
    //            m_xmlr.Read();

    //            // '   pPassword = m_xmlr.ReadElementString("value")
    //            SECRET = m_xmlr.ReadElementString("value");
    //            // Me.txtoutputfile.Text = pPATH



    //            m_xmlr.Read();

    //            m_xmlr.GetAttribute("API");
    //            // Read elements firstname and lastname
    //            m_xmlr.Read();
    //            WEB_API = m_xmlr.ReadElementString("value");

    //            m_xmlr.Read();

    //            m_xmlr.GetAttribute("APP_PATH");
    //            // Read elements firstname and lastname
    //            m_xmlr.Read();
    //            APP_PATH = m_xmlr.ReadElementString("value");

    //        }
    //        // close the reader

    //        m_xmlr.Close();
    //    }

    //    // Return strIPAddress
    //    catch (Exception ex)
    //    {
    //    }
  //  }


}
