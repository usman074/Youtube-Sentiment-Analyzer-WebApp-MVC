using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Net.Http;
using System.Net;
using System.IO;
using System.Web.Mvc;  


namespace VP_PROJECT_MVC.Models
{
    public class conn
    {
        commentcs c;
        List<AnalysisResult> result_data;
        List<String> resultValues;
        double sum_score;
        double avg_score;
        double sum_sad;
        double avg_sad;
        double sum_joy;
        double avg_joy;
        double sum_fear;
        double avg_fear;
        double sum_disgust;
        double avg_disgust;
        double sum_anger;
        double avg_anger;
        double sad;
        double joy;
        double fear;
        double dis;
        double ang;
        double positive;
        double negative;
        double neutral;
        public async Task<List<String>> Fetch_Data(String id)
        {
            positive = 0.0;
            negative = 0.0;
            neutral = 0.0;
            sum_score = 0.0;
            avg_score = 0.0;
            sum_sad = 0.0;
            avg_sad = 0.0;
            sum_joy = 0.0;
            avg_joy = 0.0;
            sum_fear = 0.0;
            avg_fear = 0.0;
            sum_disgust = 0.0;
            avg_disgust = 0.0;
            sum_anger = 0.0;
            avg_anger = 0.0;
            result_data = new List<AnalysisResult>();
            resultValues = new List<String>();
            String result = null;

            String key = "AIzaSyBfdKaVnrbrZ281s3GFFKAIXdPtEL2PTgI";
            try
            {
                HttpClient cl = new HttpClient();
                cl.BaseAddress = new Uri("https://www.googleapis.com/youtube/v3/commentThreads");
                cl.DefaultRequestHeaders.Accept.Clear();
                //telling server to send data in json format
                cl.DefaultRequestHeaders.Accept.Add(
                    new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                String url = "?part=snippet&videoId=" + id + "&key=" + key;
                //await suspended the execution of method until get Async is completed
                //Get Async returns an HttpResponseMessage that contains the HTTP response. If the status code in the response is a success code,
                //the response body contains the JSON representation of a product.
                HttpResponseMessage res = await cl.GetAsync(url);
                if (res.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    // result = "done";
                    c = res.Content.ReadAsAsync<commentcs>().Result;
                    //result = c.items[0].snippet.topLevelComment.snippet.textOriginal;
                    int totalResult = Int32.Parse(c.pageInfo.totalResults);
                    for (int i = 0; i < totalResult; i++)
                    {

                        if (c.items[i].snippet.topLevelComment.snippet.textOriginal.Length > 15)
                        {
                            await ibm(c.items[i].snippet.topLevelComment.snippet.textOriginal);

                        }
                        else
                        {
                            continue;
                        }
                    }

                }

                // return View("conn",await x.Fetch_Data());

            }
            catch (Exception e)
            {

            }
                for (int j = 0; j < result_data.Count; j++)
                {

                    sum_score = sum_score + result_data[j].Score;
                    sum_sad = sum_sad + result_data[j].Sadness;
                    sum_joy = sum_joy + result_data[j].Joy;
                    sum_fear = sum_fear + result_data[j].Fear;
                    sum_disgust = sum_disgust + result_data[j].Disgust;
                    sum_anger = sum_anger + result_data[j].Anger;
                }
                for (int j = 0; j < result_data.Count; j++)
                {
                    if (result_data[j].Label.Equals("positive"))
                    {
                        positive++;
                    }
                    else if(result_data[j].Label.Equals("negative"))
                    {
                        negative++;
                    }
                    else if(result_data[j].Label.Equals("neutral"))
                    {
                        neutral++;
                    }
                }

                avg_score = sum_score / result_data.Count;
                avg_sad = sum_sad / result_data.Count;
                avg_joy = sum_joy / result_data.Count;
                avg_fear = sum_fear / result_data.Count;
                avg_disgust = sum_disgust / result_data.Count;
                avg_anger = sum_anger / result_data.Count;
                avg_score = Math.Round(avg_score, 2);
                avg_sad = Math.Round(avg_sad, 2);
                avg_joy = Math.Round(avg_joy, 2);
                avg_fear = Math.Round(avg_fear, 2);
                avg_disgust = Math.Round(avg_disgust, 2);
                avg_anger = Math.Round(avg_anger, 2);
                avg_score = avg_score * 100;
                avg_sad = avg_sad * 100;
                avg_joy = avg_joy * 100;
                avg_fear = avg_fear * 100;
                avg_disgust = avg_disgust * 100;
                avg_anger = avg_anger * 100;
                resultValues.Add(avg_score.ToString());
                resultValues.Add(avg_sad.ToString());
                resultValues.Add(avg_joy.ToString());
                resultValues.Add(avg_fear.ToString());
                resultValues.Add(avg_disgust.ToString());
                resultValues.Add(avg_anger.ToString());
                resultValues.Add(positive.ToString());
                resultValues.Add(negative.ToString());
                resultValues.Add(neutral.ToString());
                return resultValues;
        }
        public async Task ibm(String ab)
        {
            sad = 0.0;
            joy = 0.0;
            fear = 0.0;
            dis = 0.0;
            ang = 0.0;
            String abb = ab;
            string temp = "https://gateway.watsonplatform.net/natural-language-understanding/api/v1/analyze?version=2017-02-27&text=" + abb + "&features=sentiment,keywords,emotion&keywords.sentiment=true";
            HttpWebRequest request;
            try
            {
                request = (HttpWebRequest)WebRequest.Create(temp);

                request.Credentials = new NetworkCredential("082e2d88-9164-4338-b35d-c62580b3a78d", "Eed1kE8nDr8f");
                ServicePointManager.Expect100Continue = true;
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                request.ContentType = "application/json";
                request.MediaType = "application/json";
                request.Accept = "application/json";
                request.Method = "POST";
                string text;
                var response = await request.GetResponseAsync();

                StreamReader sr = new StreamReader(response.GetResponseStream());


                text = sr.ReadToEnd();

                HttpResponseMessage res = await CreateJsonResponse(text);
                Analysis x = res.Content.ReadAsAsync<Analysis>().Result;
                double sc = Convert.ToDouble(x.sentiment.document.score);
                String lb = x.sentiment.document.label;
                try
                {
                    sad = Convert.ToDouble(x.emotion.document.emotion.sadness);
                    joy = Convert.ToDouble(x.emotion.document.emotion.joy);
                    fear = Convert.ToDouble(x.emotion.document.emotion.fear);
                    dis = Convert.ToDouble(x.emotion.document.emotion.disgust);
                    ang = Convert.ToDouble(x.emotion.document.emotion.anger);
                }
                catch (Exception o)
                {
                }

                AnalysisResult a = new AnalysisResult(sc, lb, sad, joy, fear, dis, ang);
                result_data.Add(a);

            }
            catch (WebException e)
            {

            }
        }
        public static async Task<HttpResponseMessage> CreateJsonResponse(string json)
        {

            return new HttpResponseMessage()
            {
                Content = new StringContent(json, System.Text.Encoding.UTF8, "application/json")
            };

        }
        
    }
}