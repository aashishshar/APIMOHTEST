using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml.Schema;
using System.Security.Cryptography.X509Certificates;

namespace MOHFW_CLIENT_WEBAPP
{
    public partial class _Default : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // The path to the certificate.
            string Certificate = "E:\\Aashish-Work-Details\\PramodGK\\GSTN_PublicKey.cer";
            // Load the certificate into an X509Certificate object.
            X509Certificate cert = X509Certificate.CreateFromCertFile(Certificate);
            // Get the value.
            byte[] results = cert.GetPublicKey();
            string encryptedSignature = EncryptString(GenerateSalt(32), results); 

            //string encryptedSignature1 = Encrypt("383dc1f4835f43ad80f80f6cf284cd7b", System.Text.Encoding.UTF8.GetString(results));
            
           //string encryptedSignature1= AESEncryptionDecryption.Decrypt(encryptedSignature, results);
            string url = "http://devapi.gstsystem.co.in/taxpayerapi/v0.1/authenticate";
            string qstring = "?action=" + Server.UrlEncode("OTPREQUEST") + "&app_key=" + Server.UrlEncode(encryptedSignature) + "&username=" + Server.UrlEncode("BALAJI.TN.1");
            byte[] postArray = Encoding.UTF8.GetBytes(qstring);
            url = url + qstring;

            //Start--STEP1---Calling Method for Request/Response
            Uri uri=new Uri("http://devapi.gstsystem.co.in/taxpayerapi/v0.1/authenticate");
            string data0 = @"
        POST /taxpayerapi/v0.1/authenticate HTTP/1.1
        Host: devapi.gstsystem.co.in
        Content-Type: application/json
        username: BALAJI.TN.1
        clientid: l7xx5edefdd923ad438eb5b332a73269f812
        client-secret: 383dc1f4835f43ad80f80f6cf284cd7b
        Cache-Control: no-cache      
        { 
	        ""action"": ""OTPREQUEST"", 
	        ""username"": ""BALAJI.TN.1"", 
	        ""app_key"": " + encryptedSignature+"}";
            System.Net.Http.HttpClient client = new System.Net.Http.HttpClient();

            client.BaseAddress = new System.Uri(uri.ToString());
            byte[] cred = UTF8Encoding.UTF8.GetBytes("BALAJI.TN.1:383dc1f4835f43ad80f80f6cf284cd7b");
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Basic", Convert.ToBase64String(cred));
            client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

            System.Net.Http.HttpContent content = new StringContent(data0, UTF8Encoding.UTF8, "application/json");
            HttpResponseMessage messge = client.PostAsync(url, content).Result;
            string description = string.Empty;
            if (messge.IsSuccessStatusCode)
            {
                string result = messge.Content.ReadAsStringAsync().Result;
                description = result;
            }

            //END--STEP1---Another Calling Method for Request/Response


            //Start--STEP2---Another Calling Method for Request/Response
            HttpWebRequest webRequest = HttpWebRequest.Create(url) as HttpWebRequest;
            webRequest.Method = WebRequestMethods.Http.Post;
           // string postdata=string.Format()
           webRequest.ContentType = "application/json;charset=UTF-8";
            webRequest.ContentLength = postArray.Length;
           Stream dataStream;
           try
           {
               dataStream = webRequest.GetRequestStream();
               // Write the data to the request stream.
               dataStream.Write(postArray, 0, postArray.Length);
               // Close the Stream object.
               dataStream.Close();
           }
           catch (WebException exWrite)
           {
               throw exWrite;
           }
           catch (Exception genException)
           {
               throw genException;
           }
           WebResponse response = null;
           try
           {
               response = webRequest.GetResponse();
               // Display the status.
               Console.WriteLine(((HttpWebResponse)response).StatusDescription);
           }
           catch (WebException exResponse)
           {
               throw exResponse;
           }
           StreamReader reader;
           try
           {
               // Get the stream containing content returned by the server.
               dataStream = response.GetResponseStream();

               // Open the stream using a StreamReader for easy access.
               reader = new StreamReader(dataStream);
               // Read the content.
           }
           catch (WebException exWrite)
           {
               throw exWrite;
           }
           catch (Exception genException)
           {
               throw genException;
           }
           string responseFromServer = reader.ReadToEnd();
           // Display the content.
           Response.Write(responseFromServer);
           // Clean up the streams.
           reader.Close();
           dataStream.Close();
           response.Close();
            //End--STEP2
      
        }
        public static string GenerateSalt(int byteCount)
        {
            // cryptographic strong random numbers 
            RandomNumberGenerator rng = new RNGCryptoServiceProvider();
            byte[] salt = new byte[byteCount];
            rng.GetBytes(salt);
            return Convert.ToBase64String(salt);
        }

        public static string RandomString()
        {
            Guid g;
            g = Guid.NewGuid();
            return Guid.NewGuid().ToString().Substring(1, 32);
        }
        private string Encrypt(string clearText,string publickey)
        {
            string EncryptionKey = "MAKV2SPBNI99212";
            byte[] clearBytes = Encoding.Unicode.GetBytes(clearText);
            using (Aes encryptor = Aes.Create())
            {
                Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
                encryptor.Key = pdb.GetBytes(32);
                encryptor.IV = pdb.GetBytes(16);
                //encryptor.Mode = CipherMode.ECB;
                //encryptor.Padding = PaddingMode.PKCS7;
                //encryptor.KeySize = 256;
                //encryptor.BlockSize = 128;

                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateEncryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(clearBytes, 0, clearBytes.Length);
                        cs.Close();
                    }
                    clearText = Convert.ToBase64String(ms.ToArray());
                }
            }
            return clearText;
        }

   
        public static RijndaelManaged GetRijndaelManaged(String secretKey)
        {
            try
            {
                var keyBytes = new byte[32];
               // var ivBytes = new byte[16];
                var secretKeyBytes = Encoding.UTF8.GetBytes(secretKey);
                
                Array.Copy(secretKeyBytes, keyBytes, Math.Min(keyBytes.Length, secretKeyBytes.Length));
                return new RijndaelManaged
                {
                    Mode = CipherMode.ECB,
                    Padding = PaddingMode.PKCS7,
                    KeySize = 256,
                    BlockSize = 128,
                    Key = keyBytes//,
                    //IV = ivBytes
                };
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }

        public static byte[] Encrypt(byte[] plainBytes, RijndaelManaged rijndaelManaged)
        {
            try
            {
                return rijndaelManaged.CreateEncryptor().TransformFinalBlock(plainBytes, 0, plainBytes.Length);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }


        public static String EncryptString(String key,byte[] certificate)
        {
            try
            {
                //string dateSignData = DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss.fff");
                //var plainBytes = Encoding.UTF8.GetBytes(certificate);
                return Convert.ToBase64String(Encrypt(certificate, GetRijndaelManaged(key)));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }
    }
}