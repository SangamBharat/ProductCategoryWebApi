using ProductApi.Models;
using ProductApi.Context;

namespace ProductApi.Services
{
    public class CategoryService:GeneralService<Category>,ICategoryService
    {
        public CategoryService(ServiceContext serviceContext):base(serviceContext)
        {
            
        }
    }
}
