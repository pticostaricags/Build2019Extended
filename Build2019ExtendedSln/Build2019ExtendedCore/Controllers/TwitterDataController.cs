using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Build2019ExtendedCore.DataAccess.DbEntities;
using Build2019ExtendedCore.Models;
using F23.StringSimilarity;
using Microsoft.AspNetCore.Mvc;

namespace Build2019ExtendedCore.Controllers
{
    public class TwitterDataController : Controller
    {
        private MainContext _mainContext;

        public TwitterDataController (MainContext mainContext)
        {
            this._mainContext = mainContext;
        }
        public IActionResult Index()
        {
            return View();
        }

        public class RetrieveTwitterUserFollowersDataParameter
        {
            public string Username { get; set; }
        }

        public IActionResult GetTwitterUserFollowersData()
        {
            return View(null);
            //RetrieveTwitterUserFollowersData model = new RetrieveTwitterUserFollowersData();
            //return View(model);
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> GetTwitterUserFollowersData(string txtTwitterUserHandle)
        {
            RetrieveTwitterUserFollowersDataParameter parameter = new RetrieveTwitterUserFollowersDataParameter()
            {
                Username = txtTwitterUserHandle
            };
            var jsonParameter = Newtonsoft.Json.JsonConvert.SerializeObject(parameter);
            RetrieveTwitterUserFollowersData result = null;
            System.Net.Http.HttpClient httpClient = new System.Net.Http.HttpClient();
            StringContent stringContent = new StringContent(jsonParameter);
            stringContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue( "application/json");
            var postResult = await httpClient.PostAsync("https://prod-00.westus.logic.azure.com:443/workflows/6ad354d7c7ab43edbb9aeecaa7526883/" +
                "triggers/manual/paths/invoke?api-version=2016-10-01&sp=%2Ftriggers%2Fmanual%2Frun&sv=1.0&sig=kSK-iegBR0WguOblQqlfjrAXAO4DaDMDN3GLsZklU0k",
                stringContent);
            if (postResult.IsSuccessStatusCode)
            {
                var resultString = await postResult.Content.ReadAsStringAsync();
                var convertResult = Newtonsoft.Json.JsonConvert.DeserializeObject<Class1[]>(resultString);
                result = new RetrieveTwitterUserFollowersData()
                {
                    Property1 = convertResult.OrderByDescending(p=>p.CreatedAtIso).ToArray()
                };
                try
                {
                    Levenshtein lev = new Levenshtein();
                    //var allTweets = _mainContext.Tweet.ToList();
                    foreach (var singleTweet in result.Property1)
                    {
                        //allTweets.ForEach((singledbTweet) => 
                        //{
                        //    var distance = lev.Distance(singledbTweet.TweetText, singleTweet.TweetText);
                        //});
                        var firstPartOfTweet = singleTweet.TweetText.Substring(0, singleTweet.TweetText.Length / 2);
                        var tweetEntity = _mainContext.Tweet.Where(p =>
                        p.TweetId == singleTweet.TweetId || (p.TweetText.StartsWith(firstPartOfTweet))).SingleOrDefault();
                        if (tweetEntity == null)
                        {
                            tweetEntity = new Tweet()
                            {
                                TweetId = singleTweet.TweetId,
                                TweetText = singleTweet.TweetText,
                                TweetedBy = singleTweet.TweetedBy
                            };
                            _mainContext.Tweet.Add(tweetEntity);
                            _mainContext.SaveChanges();
                        }
                    }
                }
                catch (Exception ex)
                {

                }
            }
            return View("GetTwitterUserFollowersData", result);
        }
    }
}