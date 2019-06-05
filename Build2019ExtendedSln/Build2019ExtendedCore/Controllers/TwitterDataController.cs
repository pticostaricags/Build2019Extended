using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Build2019ExtendedCore.Models;
using Microsoft.AspNetCore.Mvc;

namespace Build2019ExtendedCore.Controllers
{
    public class TwitterDataController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public class RetrieveTwitterUserFollowersDataParameter
        {
            public string Username { get; set; }
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
                    Property1 = convertResult
                };
            }
            return View("GetTwitterUserFollowersData", result);
        }
    }
}