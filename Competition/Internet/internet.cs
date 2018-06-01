using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections;
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
        private string reqUri = "http://111.231.234.96:8000/";
        public static HttpClient httpClient = new HttpClient();
        private static API api = null;
        private API() {
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            httpClient.DefaultRequestHeaders.AcceptEncoding.Add(new StringWithQualityHeaderValue("utf-8"));
        }
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
            //Windows.Web.Http.HttpClient httpClient = new Windows.Web.Http.HttpClient();

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

            Uri requestUri = new Uri(reqUri + "get/all");
            //Send the GET request asynchronously and retrieve the response as a string.
            HttpResponseMessage httpResponse = new HttpResponseMessage();
            string httpResponseBody = "";
            JObject info = null;
            try
            {
                //Send the GET request
                httpResponse = await httpClient.GetAsync(requestUri);
                httpResponse.EnsureSuccessStatusCode();
                httpResponseBody = await httpResponse.Content.ReadAsStringAsync();
                Debug.WriteLine("queryAllMatchesAsync[info]: "+httpResponseBody);
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
        public async Task<JObject> createMatch(DataSet info, string matchEvent = "tennis", string matchType = "SingleElimination", string tilte = "ZERO", string date = "19980104", string seed = null, string matchLastTime = null, string place = null, string placeContain = null, string sectionPerDay = null)
        {
            //信息即为运动员信息
            var infoXml = new System.Xml.XmlDocument();
            infoXml.LoadXml(info.GetXml());
            string infoJson = JsonConvert.SerializeXmlNode(infoXml);
            //Debug.WriteLine(infoJson);

            //Create an HTTP client object
            //HttpClient httpClient = new HttpClient();
            //httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            //httpClient.DefaultRequestHeaders.AcceptEncoding.Add(new StringWithQualityHeaderValue("utf-8"));

            Uri requestUri = new Uri(reqUri + "save/" + matchEvent + "?format=" + matchType + "&&title=" + tilte + "&&time=" + date + "&&seed=" + seed + "&&matchLastTime=" + matchLastTime + "&&place=" + place + "&&sectionPerDay=" + sectionPerDay + "&&placeContain=" + placeContain);

            string httpResponseBody = "";
            JObject result = null;
            try
            {
                //Send the post request
                HttpContent content = new StringContent(infoJson, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await httpClient.PostAsync(requestUri, content);
                response.EnsureSuccessStatusCode();
                httpResponseBody = await response.Content.ReadAsStringAsync();
                Debug.WriteLine(httpResponseBody);
                result = JObject.Parse(httpResponseBody);
                //Debug.WriteLine(result);

                /*
                //数据处理

                string round = result["data"]["round"].ToString();
                JToken groups = result["data"]["groups"];
                foreach (JToken group in groups)
                {

                    string groupId = group["group"].ToString();

                    JToken battles = group["battles"];

                    foreach (JToken battle in battles)
                    {
                        //battle id
                        string _id = battle["_id"].ToString();

                        Models.Athlete A = null, B = null;

                        //Athlete(string _id, string _name, string _sex, string _idNum, string _phoneNum, string _score, string _seedNum)

                        //athleteA
                        JToken athleteA = battle["athleteA"];
                        //轮空选手为空
                        if (athleteA.ToString() != null)
                        {
                            string athleteAId = athleteA["_id"].ToString();
                            //Debug.WriteLine(athleteAId);
                            JToken infoA = athleteA["athlete"];
                            //Debug.WriteLine(infoA);
                            //A = new Models.Athlete(athleteAId, infoA["姓名"].ToString(), infoA["性别"].ToString(), infoA["身份证"].ToString(), infoA["手机号"].ToString(), infoA["积分"].ToString(), "0");
                        }


                        //athleteB
                        JToken athleteB = battle["athleteB"];

                        //轮空选手为空
                        if (athleteB.ToString() != "")
                        {
                            string athleteBId = athleteB["_id"].ToString();
                            JToken infoB = athleteB["athlete"];
                            Debug.WriteLine(infoB);
                            //B = new Models.Athlete(athleteAId, infoB["姓名"].ToString(), infoB["性别"].ToString(), infoB["身份证"].ToString(), infoB["手机号"].ToString(), infoB["积分"].ToString(), "0");
                        }

                        //ViewModels.BattleVM.GetBattleVM().AllBattles.Add(_id, group, A, B);
                    }
                    
                }*/
                
            }
            catch (Exception ex)
            {
                await new MessageDialog(ex.Message).ShowAsync();
            }
            return result;
        }

        //通过比赛名字获得比赛运动员
        //数据处理与createMatch运动员部分相同
        public async Task<JObject> getAthletesByTitle(string title)
        {
            //Create an HTTP client object
            //HttpClient httpClient = new HttpClient();
            Uri requestUri = new Uri(reqUri + "get/athlete?title=" + title);

            //Send the GET request asynchronously and retrieve the response as a string.
            HttpResponseMessage httpResponse = new HttpResponseMessage();
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

        //依据赛事名字获取比赛信息
        //exist info of battles and athletes
        public async Task<JObject> GetMatchInfo(string matchEvent, string title, string round = "1")
        {
            //Create an HTTP client object
            //HttpClient httpClient = new HttpClient();

            Uri requestUri = new Uri(reqUri + "get/" + matchEvent + "?title=" + title + "&&round=" + round);

            JObject result = null;
            string httpResponseBody = "";
            try
            {
                //Send the get request
                HttpResponseMessage response = await httpClient.GetAsync(requestUri);
                response.EnsureSuccessStatusCode();
                httpResponseBody = await response.Content.ReadAsStringAsync();
                //Debug.WriteLine(httpResponseBody);
                result = JObject.Parse(httpResponseBody);
                Debug.WriteLine(result);
            }
            catch (Exception ex)
            {
                await new MessageDialog(ex.Message).ShowAsync();
            }
            return result;
        }

        //更新运动员信息
        public async Task<bool> UpdateAthlete(JObject athleteInfo)
        {
            string infoJson = athleteInfo.ToString();

            //Create an HTTP client object
            //HttpClient httpClient = new HttpClient();
            //httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            //httpClient.DefaultRequestHeaders.AcceptEncoding.Add(new StringWithQualityHeaderValue("utf-8"));

            Uri requestUri = new Uri(reqUri + "update/athlete");

            string httpResponseBody = "";
            JObject result = null;
            try
            {
                //Send the post request
                HttpContent content = new StringContent(infoJson, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await httpClient.PostAsync(requestUri, content);
                response.EnsureSuccessStatusCode();
                httpResponseBody = await response.Content.ReadAsStringAsync();
                //Debug.WriteLine(httpResponseBody);
                result = JObject.Parse(httpResponseBody);
                Debug.WriteLine(result);
                return (bool)result["state"];
            }
            catch (Exception ex)
            {
                await new MessageDialog(ex.Message).ShowAsync();
            }
            return false;
        }

        //胜负更新
        /*
         *  battleId -- 相对应的battle的_id
         *  
         *  winPos -- 1 or 2 分别对应 athleteA or athleteB
         * 
        */
        public async void UpdateWinInfo(string title, string battleId, int winPos = 1, string matchEvent = "tennis")
        {
            //Create an HTTP client object
            //HttpClient httpClient = new HttpClient();

            Uri requestUri = new Uri(reqUri + "update/" + matchEvent + "?title=" + title + "&&_Id=" + battleId + "&&win=" + winPos);
            //Send the GET request asynchronously and retrieve the response as a string.
            HttpResponseMessage httpResponse = new HttpResponseMessage();
            try
            {
                //Send the GET request
                httpResponse = await httpClient.GetAsync(requestUri);
                httpResponse.EnsureSuccessStatusCode();
            }
            catch (Exception ex)
            {
                await new MessageDialog(ex.Message).ShowAsync();
            }
        }

        //删除比赛
        public async void DeleteMatch(string title, string matchEvent = "tennis")
        {
            //Create an HTTP client object
            //HttpClient httpClient = new HttpClient();

            Uri requestUri = new Uri(reqUri + "delete/" + matchEvent + "?title=" + title);

            try
            {
                //Send the post request
                HttpResponseMessage response = await httpClient.GetAsync(requestUri);
                response.EnsureSuccessStatusCode();
            }
            catch (Exception ex)
            {
                await new MessageDialog(ex.Message).ShowAsync();
            }
        }

        //淘汰赛获得下一轮
        /*
         *  前提：确定所有battle都有胜利者并且赛制为淘汰赛
         *  
         *  @param： round - 轮数， idArray - 获胜选手信息id数组集合, title - 比赛名字， ball - 球类
         *  
         */
        public async Task<JObject> getNextMatch(string round, JArray idJson, string title, string matchEvent, string matchType)
        {
            //Create an HTTP client object
            //HttpClient httpClient = new HttpClient();
            //httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            //httpClient.DefaultRequestHeaders.AcceptEncoding.Add(new StringWithQualityHeaderValue("utf-8"));

            Uri requestUri = new Uri(reqUri + "getNextRound/" + matchEvent + "?title=" + title + "&&round=" + round + "&&format=" + matchType);

            JObject result = null;
            string httpResponseBody = "";
            try
            {
                //Send the post request
                //Debug.WriteLine(idJson.ToArray().);
                HttpContent content = new StringContent(idJson.ToString(), Encoding.UTF8, "application/json");
                HttpResponseMessage response = await httpClient.PostAsync(requestUri, content);
                response.EnsureSuccessStatusCode();
                httpResponseBody = await response.Content.ReadAsStringAsync();
                //Debug.WriteLine(httpResponseBody);
                result = JObject.Parse(httpResponseBody);
                //Debug.WriteLine(result);
            }
            catch (Exception ex)
            {
                await new MessageDialog(ex.Message).ShowAsync();
            }
            return result;
        }

        //登陆
        public async Task<JObject> Login(string username, string password)
        {
            //Create an HTTP client object
            //HttpClient httpClient = new HttpClient();
            //httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            //httpClient.DefaultRequestHeaders.AcceptEncoding.Add(new StringWithQualityHeaderValue("utf-8"));

            Uri requestUri = new Uri(reqUri + "signin");
            JObject userInfo = new JObject();
            userInfo.Add("username", username);
            userInfo.Add("password", password);
            JObject result = null;
            string httpResponseBody = "";
            try
            {
                //Send the post request
                //Debug.WriteLine(idJson.ToArray().);
                HttpContent content = new StringContent(userInfo.ToString(), Encoding.UTF8, "application/json");
                HttpResponseMessage response = await httpClient.PostAsync(requestUri, content);
                response.EnsureSuccessStatusCode();
                httpResponseBody = await response.Content.ReadAsStringAsync();
                //Debug.WriteLine(httpResponseBody);
                result = JObject.Parse(httpResponseBody);
                //Debug.WriteLine(result);
            }
            catch (Exception ex)
            {
                await new MessageDialog(ex.Message).ShowAsync();
            }
            return result;
        }

        //退出
        public async void SignOut()
        {
            Uri requestUri = new Uri(reqUri + "signout");
            try
            {
                string httpResponseBody = "";
                HttpResponseMessage response = await httpClient.GetAsync(requestUri);
                response.EnsureSuccessStatusCode();
            }
            catch(Exception ex)
            {
                await new MessageDialog(ex.Message).ShowAsync();
            }
        }

        //用户注册
        public async Task<bool> Register(string username, string password, string email)
        {
            Uri requestUri = new Uri(reqUri + "signup");
            bool result;
            JObject userInfo = new JObject();
            userInfo.Add("username", username);
            userInfo.Add("password", password);
            userInfo.Add("email", email);
            string httpResponseBody = "";
            try
            {
                //Send the post request
                //Debug.WriteLine(idJson.ToArray().);
                HttpContent content = new StringContent(userInfo.ToString(), Encoding.UTF8, "application/json");
                HttpResponseMessage response = await httpClient.PostAsync(requestUri, content);
                response.EnsureSuccessStatusCode();
                httpResponseBody = await response.Content.ReadAsStringAsync();
                //Debug.WriteLine(httpResponseBody);
                JObject rep = JObject.Parse(httpResponseBody);
                Debug.WriteLine(rep);
                result = rep["state"].ToString() == "true" ? true : false;

                string msg = rep["message"].ToString();
                await new MessageDialog(msg).ShowAsync();
                //Debug.WriteLine(result);
            }
            catch (Exception ex)
            {
                result = false;
                await new MessageDialog(ex.Message).ShowAsync();
            }
            return result;
        }
    }
}
