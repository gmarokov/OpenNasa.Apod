using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using System.Net.Http;
using System;
using OpenNasa.Apod.Shared;
using System.Collections.Generic;
using Microsoft.Extensions.Primitives;
using System.Linq;

namespace OpenNasa.Apod.Api
{
    public partial class GetPictures
    {
        private static readonly string API_URL = "https://api.nasa.gov/planetary/apod";
        private static readonly int DEFAULT_ITEMS_PER_PAGE = 10;

        [FunctionName("GetPictures")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "pictures")] HttpRequest req)
        {
            var apiKey = Environment.GetEnvironmentVariable("Nasa_Api_Key", EnvironmentVariableTarget.Process);
            var client = new HttpClient
            {
                BaseAddress = new Uri(API_URL)
            };
            var baseQuery = $"?api_key={apiKey}";

            if (req.Query.TryGetValue("startDate", out StringValues startDateStr) && req.Query.TryGetValue("endDate", out StringValues endDateStr))
            {
                var startDate = DateTime.Parse(startDateStr);
                var endDate = DateTime.Parse(endDateStr);
                if (startDate <= DateTime.Parse(ApodConstants.FirstDateApodPost))
                {
                    return new BadRequestObjectResult("No images are available before that given range");
                }

                baseQuery += $"&start_date={startDate.Year}-{startDate.Month}-{startDate.Day}&end_date={endDate.Year}-{endDate.Month}-{endDate.Day}";
            }
            else if (req.Query.TryGetValue("count", out StringValues count))
            {
                baseQuery += $"&count={count}";
            }
            else
            {
                baseQuery += $"&count={DEFAULT_ITEMS_PER_PAGE}";
            }

            var response = await client.GetAsync(baseQuery);
            var pics = await response.Content.ReadAsAsync<List<ApodPictureDto>>();
            var pictureForResponse = pics
                .OrderByDescending(x => x.Date)
                .Select(x => new ApodPicture()
                {
                    Date = x.Date,
                    Explanation = x.Explanation,
                    HdUrl = x.HdUrl,
                    MediaType = x.MediaType,
                    Title = x.Title,
                    Url = x.Url,
                    Copyright = x.Copyright
                })
                .ToList();

            return new OkObjectResult(pictureForResponse);
        }
    }
}
