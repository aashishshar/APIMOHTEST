using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml.Schema;


namespace MOHFW_CLIENT_WEBAPP
{
    public partial class _Default : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string baseAddress = "http://localhost:8484/";
            using (var client = new HttpClient())
            {
                var form = new Dictionary<string, string>    
               {    
                   {"grant_type", "password"},    
                   {"username", "user"},    
                   {"password", "user"},    
               };
                var tokenResponse = client.PostAsync(baseAddress + "/token", new FormUrlEncodedContent(form)).Result;
                var token = tokenResponse.Content.ReadAsStringAsync().Result;
                var jObject = JObject.Parse(token);
                var p = jObject.GetValue("access_token").ToString();

                //var tokenResponse = client.PostAsync(baseAddress + "accesstoken", new FormUrlEncodedContent(form)).Result;
                //var token1 = tokenResponse.Content.ReadAsAsync<System.Xml.Schema..Token>(new[] { new JsonMediaTypeFormatter() }).Result;  

                using (HttpClient httpClient1 = new HttpClient())
                {
                    httpClient1.BaseAddress = new Uri(baseAddress);
                    httpClient1.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", p);
                    HttpResponseMessage response = httpClient1.GetAsync("api/data/authenticate").Result;
                    //var response = httpClient1.GetStringAsync("api/data/authenticate");
                    if (response.IsSuccessStatusCode)
                    {
                        HttpResponseMessage responseStatedata = httpClient1.GetAsync("api/DataItems").Result;
                        Label1.Text = responseStatedata.Content.ReadAsStringAsync().Result;
                    }
                    else
                    {
                        //string message = response.Content.ReadAsStringAsync().Result;
                        Label1.Text ="Unautherized" ;//"URL responese : " + message;
                    }
                }

                //var token = tokenResponse.Content.ReadAsAsync<System.Xml.Schema..Token>(new[] { new JsonMediaTypeFormatter() }).Result;  
                //if (string.IsNullOrEmpty(token.Error))  
                //{  
                //    Console.WriteLine("Token issued is: {0}", token.AccessToken);  
                //}  
                //else  
                //{  
                //    Console.WriteLine("Error : {0}", token.Error);  
                //}  
                //Console.Read();  
            }  
        }
    }
}