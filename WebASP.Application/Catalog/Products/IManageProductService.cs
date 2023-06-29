using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebASP.ViewModels.Catalog.ProductImage;
using WebASP.ViewModels.Catalog.Products;
using WebASP.ViewModels.Common;

namespace WebASP.Application.Catalog.Products
{
    public interface IManageProductService
    {
        Task<int> Create(ProductCreateRequest request);
        Task<PagedResult<ProductViewModel>> GetAllPaging(GetManageProductPagingRequest request);
        Task<int> Update(ProductUpdateRequest request);
        Task<int> Delete(int productId);
        Task AddViewCount(int productId);
        Task<bool> UpdatePrice(int productId, decimal newPrice);
        Task<bool> UpdateStock(int productId, int addedQuantity);
        Task<ProductViewModel> GetById(int productId, string languageId);

        Task<int> AddImage( ProductImageCreateRequest request);
        Task<int> UpdateImage(ProductImageUpdateRequest request);
        Task<int> DeleteImage(int imageId);
        Task<ProductImageViewModel>GetImageById(int imageId);
        Task<List<ProductImageViewModel>>GetListImageById(int productId);
    }
}
