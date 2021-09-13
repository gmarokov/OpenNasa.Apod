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
        private const string API_URL = "https://api.nasa.gov/planetary/apod";
        private const int DEFAULT_ITEMS_PER_PAGE = 10;

        /// <summary>
        /// Get images function for retrieving the list of images based by filters
        /// </summary>
        /// <param name="req"><see cref="HttpRequest"/></param>
        /// <returns>List of images</returns>
        [FunctionName("GetPictures")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "pictures")] HttpRequest req)
        {
            var client = new HttpClient
            {
                BaseAddress = new Uri(API_URL)
            };

            var query = ExtractQueryString(req);
            var response = await client.GetAsync(query);
            var pictures = await ParseImages(response);

            return new OkObjectResult(pictures);
        }

        private static async Task<List<ApodPicture>> ParseImages(HttpResponseMessage response)
        {
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

            return pictureForResponse;
        }

        private static string ExtractQueryString(HttpRequest req)
        {
            var apiKey = Environment.GetEnvironmentVariable("Nasa_Api_Key", EnvironmentVariableTarget.Process);
            var baseQuery = $"?api_key={apiKey}";

            if (req.Query.TryGetValue("startDate", out StringValues startDateStr) && req.Query.TryGetValue("endDate", out StringValues endDateStr))
            {
                var startDate = DateTime.Parse(startDateStr);
                var endDate = DateTime.Parse(endDateStr);
                if (startDate <= DateTime.Parse(ApodConstants.FirstDateApodPost))
                    throw new ArgumentException("No images are available before that given range");

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

            return baseQuery;
        }
    }
}
