using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebASP.ViewModels.Common;

namespace WebASP.ViewModels.Catalog.Products
{
   public class GetManageProductPagingRequest:PagingRequestBase
    {
        public string Keyword { get; set; }
        public List<int> CategoryId { get; set; }
    }
}
