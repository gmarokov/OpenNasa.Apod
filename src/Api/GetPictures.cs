using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using System.Net.Http;
using System;
using OpenNasa.Apod.Shared;
using System.Collections.Generic;

namespace OpenNasa.Apod.Api
{
    public partial class GetPictures
    {
        private readonly IApodPicturesData productData;
        private static readonly string API_URL = "https://api.nasa.gov/planetary/apod";
        private static readonly int DEFAULT_ITEMS_PER_PAGE = 10;

        public GetPictures(IApodPicturesData productData)
        {
            this.productData = productData;
        }

        [FunctionName("GetPictures")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "pictures")] HttpRequest req)
        {
            var apiKey = Environment.GetEnvironmentVariable("Nasa_Api_Key", EnvironmentVariableTarget.Process);
            var client = new HttpClient
            {
                BaseAddress = new Uri(API_URL)
            };

            //TODO: Filters?
            var response = await client.GetAsync($"?api_key={apiKey}&count={DEFAULT_ITEMS_PER_PAGE}");
            //TODO: List of images?
            var pics = await response.Content.ReadAsAsync<List<ApodPictureDto>>();
            var pictureForResponse = new List<ApodPicture>();
            foreach (var pic in pics)
            {
                pictureForResponse.Add(new ApodPicture()
                {
                    Date = pic.Date,
                    Explanation = pic.Explanation,
                    HdUrl = pic.HdUrl,
                    MediaType = pic.MediaType,
                    Title = pic.Title,
                    Url = pic.Url
                });
            }

            return new OkObjectResult(pictureForResponse);
        }
    }
}
