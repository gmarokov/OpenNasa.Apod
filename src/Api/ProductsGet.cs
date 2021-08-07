using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;

namespace OpenNasa.Apod.Api
{
  public class ProductsGet
  {
    private readonly IApodPicturesData productData;

    public ProductsGet(IApodPicturesData productData)
    {
      this.productData = productData;
    }

    [FunctionName("ProductsGet")]
    public async Task<IActionResult> Run(
        [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "products")] HttpRequest req)
    {
      var products = await productData.GetProducts();
      return new OkObjectResult(products);
    }
  }
}
