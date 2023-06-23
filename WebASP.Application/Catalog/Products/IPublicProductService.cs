using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebASP.ViewModels.Catalog.Products;
using WebASP.ViewModels.Common;

namespace WebASP.Application.Catalog.Products
{
   public interface IPublicProductService
    {
       Task<PagedResult<ProductViewModel>> GetAllByCategoryId(GetPublicProductPagingRequest request);
        Task<List<ProductViewModel>> GetAll();   
    }
}
