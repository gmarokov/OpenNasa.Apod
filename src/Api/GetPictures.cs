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
            var response = await client.GetAsync($"?api_key={apiKey}");
            //TODO: List of images?
            var pic = await response.Content.ReadAsAsync<ApodPictureDto>();
            
            var list = new List<ApodPicture>()
            {
                new ApodPicture()
                {
                    Date = pic.Date,
                    Explanation = pic.Explanation,
                    Hdurl = pic.Hdurl,
                    MediaType = pic.MediaType,
                    Title = pic.Title,
                    Url = pic.Url
                }
            };

            return new OkObjectResult(list);
        }
    }
}
