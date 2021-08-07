using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System.IO;
using System.Text.Json;
using OpenNasa.Apod.Shared;

namespace OpenNasa.Apod.Api
{
    public class ProductsPost
    {
        private readonly IApodPicturesData productData;

        public ProductsPost(IApodPicturesData productData)
        {
            this.productData = productData;
        }

        [FunctionName("ProductsPost")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "products")] HttpRequest req,
            ILogger log)
        {
            var body = await new StreamReader(req.Body).ReadToEndAsync();
            var product = JsonSerializer.Deserialize<ApodPicture>(body, new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase });

            var newProduct = await productData.AddProduct(product);
            return new OkObjectResult(newProduct);
        }
    }
}
