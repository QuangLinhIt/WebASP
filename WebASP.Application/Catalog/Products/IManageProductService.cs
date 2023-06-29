using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebASP.ViewModels.Catalog.Products;
using WebASP.ViewModels.Common;

namespace WebASP.Application.Catalog.Products
{
    public interface IManageProductService
    {
        Task<int> Create(ProductCreateRequest request);
        Task<int> Update(ProductUpdateRequest request);
        Task<bool> UpdatePrice(int productId, decimal newPrice);
        Task<bool> UpdateStock(int productId, int addedQuantity);
        Task AddViewCount(int productId);
        Task<PagedResult<ProductViewModel>> GetAllPaging(GetManageProductPagingRequest request);
        Task<int> AddImages(int productId, List<IFormFile> files);
        Task<int> RemoveImages(int imageId);
        Task<int> UpdateImages(int imageId, string caption, bool isDefault);
        Task<List<ProductImageViewModel>> GetListImage(int productId);
    }
}
