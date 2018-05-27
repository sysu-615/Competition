using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using Windows.Data.Json;
using Windows.Data.Xml.Dom;
using Windows.UI.Popups;

namespace Competition.Internet
{

    public class API
    {
        private static API api=null;
        private API() { }
        public static API GetAPI()
        {
            if (api == null)
                api = new API();
            return api;
        }
        //获得所有比赛
        public async Task<JObject> queryAllMatchesAsync()
        {
            //Create an HTTP client object
            Windows.Web.Http.HttpClient httpClient = new Windows.Web.Http.HttpClient();

            //Add a user-agent header to the GET request. 
            var headers = httpClient.DefaultRequestHeaders;
            //The safe way to add a header value is to use the TryParseAdd method and verify the return value is true,
            //especially if the header value is coming from user input.
            string header = "ie";
            if (!headers.UserAgent.TryParseAdd(header))
            {
                throw new Exception("Invalid header value: " + header);
            }

            header = "Mozilla/5.0 (compatible; MSIE 10.0; Windows NT 6.2; WOW64; Trident/6.0)";
            if (!headers.UserAgent.TryParseAdd(header))
            {
                throw new Exception("Invalid header value: " + header);
            }

            Uri requestUri = new Uri("http://172.18.35.167:8000/get/all");
            //Send the GET request asynchronously and retrieve the response as a string.
            Windows.Web.Http.HttpResponseMessage httpResponse = new Windows.Web.Http.HttpResponseMessage();
            string httpResponseBody = "";
            JObject info = null;
            try
            {
                //Send the GET request
                httpResponse = await httpClient.GetAsync(requestUri);
                httpResponse.EnsureSuccessStatusCode();
                httpResponseBody = await httpResponse.Content.ReadAsStringAsync();
                Debug.WriteLine(httpResponseBody);
                info = JObject.Parse(httpResponseBody);
            }
            catch (Exception ex)
            {
                await new MessageDialog(ex.Message).ShowAsync();
            }

            return info;

        }
        
        //上传运动员信息，生成对战
        //运动员信息、比赛类型、标题、时间、种子数、比赛时间、比赛场地数
        public async Task<JsonObject> createMatch(DataSet info, string ball = "tennis", string matchType = "SingleElimination", string tilte = "ZERO", string date = "19980104", int seed = -1, int matchLastTime = -1, int place = -1, int placeContain = -1, int sectionPerDay = -1)
        {
            //信息即为运动员信息
            var infoXml = new System.Xml.XmlDocument();
            infoXml.LoadXml(info.GetXml());
            string infoJson = JsonConvert.SerializeXmlNode(infoXml);
            //Debug.WriteLine(infoJson);

            //Create an HTTP client object
            HttpClient httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            httpClient.DefaultRequestHeaders.AcceptEncoding.Add(new StringWithQualityHeaderValue("utf-8"));

            Uri requestUri = new Uri("http://172.18.35.167:8000/save/" + ball + "?format=" + matchType + "&&title=" + tilte + "&&time=" + date + "&&seed=" + seed + "&&matchLastTime=" + matchLastTime + "&&place=" + place + "&&sectionPerDay=" + sectionPerDay + "&&placeContain=" + placeContain );

            string httpResponseBody = "";
            JsonObject result = null;
            try
            {
                //Send the post request
                HttpContent content = new StringContent(infoJson, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await httpClient.PostAsync(requestUri, content);
                response.EnsureSuccessStatusCode();
                httpResponseBody = await response.Content.ReadAsStringAsync();
                //Debug.WriteLine(httpResponseBody);
                result = JsonObject.Parse(httpResponseBody);
                Debug.WriteLine(result);
            }
            catch (Exception ex)
            {
                await new MessageDialog(ex.Message).ShowAsync();
            }
            return result;
        }

        //依据赛事名字获取比赛信息
        public async Task<JsonObject> GetMatchInfo(string title)
        {
            //Create an HTTP client object
            HttpClient httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            httpClient.DefaultRequestHeaders.AcceptEncoding.Add(new StringWithQualityHeaderValue("utf-8"));

            Uri requestUri = new Uri("http://172.18.35.167:8000/get/" + title);

            JsonObject result = null;
            string httpResponseBody = "";
            try
            {
                //Send the post request
                HttpContent content = new StringContent(title, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await httpClient.PostAsync(requestUri, content);
                response.EnsureSuccessStatusCode();
                httpResponseBody = await response.Content.ReadAsStringAsync();
                //Debug.WriteLine(httpResponseBody);
                result = JsonObject.Parse(httpResponseBody);
                Debug.WriteLine(result);
            }
            catch (Exception ex)
            {
                await new MessageDialog(ex.Message).ShowAsync();
            }
            return result;
        }

        //更新数据
        public async void UpdateMatch(JsonObject athleteInfo)
        {
            string infoJson = athleteInfo.ToString();

            //Create an HTTP client object
            HttpClient httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            httpClient.DefaultRequestHeaders.AcceptEncoding.Add(new StringWithQualityHeaderValue("utf-8"));

            Uri requestUri = new Uri("http://172.18.35.167:8000/update/athlete");

            string httpResponseBody = "";
            JsonObject result = null;
            try
            {
                //Send the post request
                HttpContent content = new StringContent(infoJson, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await httpClient.PostAsync(requestUri, content);
                response.EnsureSuccessStatusCode();
                httpResponseBody = await response.Content.ReadAsStringAsync();
                //Debug.WriteLine(httpResponseBody);
                result = JsonObject.Parse(httpResponseBody);
                Debug.WriteLine(result);
            }
            catch (Exception ex)
            {
                await new MessageDialog(ex.Message).ShowAsync();
            }
        }

        //删除比赛
        public async void DeleteMatch(string title)
        {
            //Create an HTTP client object
            HttpClient httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            httpClient.DefaultRequestHeaders.AcceptEncoding.Add(new StringWithQualityHeaderValue("utf-8"));

            Uri requestUri = new Uri("http://172.18.35.167:8000/delete/" + title);

            try
            {
                //Send the post request
                HttpContent content = new StringContent(title, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await httpClient.PostAsync(requestUri, content);
                response.EnsureSuccessStatusCode();
            }
            catch (Exception ex)
            {
                await new MessageDialog(ex.Message).ShowAsync();
            }
        }

        //
        //public async void getMatch
    }
}
