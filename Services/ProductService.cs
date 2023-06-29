using ProductApi.Models;
using ProductApi.Context;

namespace ProductApi.Services
{
    public class ProductService : GeneralService<Product>, IProductService
    {
        public ProductService(ServiceContext serviceContext) : base(serviceContext)
        {

        }
    }
}
