using System;
using Newtonsoft.Json;

namespace OpenNasa.Apod.Api
{
    public partial class GetPictures
    {
        public class ApodPictureDto
        {
            [JsonProperty("date")]
            public DateTime Date { get; set; }

            [JsonProperty("explanation")]
            public string Explanation { get; set; }

            [JsonProperty("hdurl")]
            public string HdUrl { get; set; }

            [JsonProperty("media_type")]
            public string MediaType { get; set; }

            [JsonProperty("service_version")]
            public string ServiceVersion { get; set; }

            [JsonProperty("title")]
            public string Title { get; set; }

            [JsonProperty("url")]
            public string Url { get; set; }

            [JsonProperty("copyright")]
            public string Copyright { get; set; }
        }
    }
}
