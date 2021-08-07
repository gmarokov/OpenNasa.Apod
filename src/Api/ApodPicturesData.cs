using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OpenNasa.Apod.Shared;

namespace OpenNasa.Apod.Api
{
    public interface IApodPicturesData
    {
        Task<ApodPicture> AddProduct(ApodPicture product);
        Task<bool> DeleteProduct(int id);
        Task<IEnumerable<ApodPicture>> GetProducts();
        Task<ApodPicture> UpdateProduct(ApodPicture product);
    }

    public class ApodPicturesData : IApodPicturesData
    {
        private readonly List<ApodPicture> products = new List<ApodPicture>
        {
            new ApodPicture(),
            new ApodPicture(),
            new ApodPicture()
        };

        public Task<ApodPicture> AddProduct(ApodPicture product)
        {
            products.Add(product);
            return Task.FromResult(product);
        }

        public Task<ApodPicture> UpdateProduct(ApodPicture product)
        {
            return Task.FromResult(product);
        }

        public Task<bool> DeleteProduct(int id)
        {
            return Task.FromResult(true);
        }

        public Task<IEnumerable<ApodPicture>> GetProducts()
        {
            return Task.FromResult(products.AsEnumerable());
        }
    }
}
