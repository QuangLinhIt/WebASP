using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebASP.Application.Catalog.Products;
using WebASP.ViewModels.Catalog.Products;

namespace WebASP.BackendAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        public readonly IPublicProductService _publicProductService;
        public readonly IManageProductService _manageProductService;
        public ProductController(IPublicProductService publicProductService,IManageProductService manageProductService)
        {
            _publicProductService = publicProductService;
            _manageProductService = manageProductService;
        }

        //http://localhost:port/product
        [HttpGet("{languageId}")]
        public async Task<IActionResult> GetAll(string languageId)
        {
            var products = await _publicProductService.GetAll(languageId);
            if (products == null) return BadRequest("cannot find product");
            return Ok(products);
        }

        //http://localhost:port/product/public-paging
        [HttpGet("public-paging")]
        public async Task<IActionResult> GetAllByCategoryId([FromForm] GetPublicProductPagingRequest request)
        {
            var products = await _publicProductService.GetAllByCategoryId(request);
            return Ok(products);
        }

        //http://localhost:port/product/{productId}
        [HttpGet("{productId}/{languageId}")]
        public async Task<IActionResult> GetById(int productId,string languageId)
        {
            var product = await _manageProductService.GetById(productId,languageId);
            if (product == null) return BadRequest("cannot find product");

            return Ok(product);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromForm] ProductCreateRequest request)
        {
            var productId = await _manageProductService.Create(request);
            if (productId == 0) return BadRequest();
            var product = await _manageProductService.GetById(productId,request.LanguageId);
            return CreatedAtAction(nameof(GetById),new { id = productId },product);
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromForm] ProductUpdateRequest request)
        {
            var affectedResult = await _manageProductService.Update(request);
            if (affectedResult == 0) return BadRequest();
            return Ok();
        }

        [HttpDelete("{productId}")]
        public async Task<IActionResult> Delete(int productId)
        {
            var affectedResult = await _manageProductService.Delete(productId);
            if (affectedResult == 0) return BadRequest();
            return Ok();
        }

        [HttpPatch("price/{newPrice}")]
        public async Task<IActionResult> UpdatePrice([FromQuery]  int id,decimal newPrice)
        {
            var isSuccessful = await _manageProductService.UpdatePrice(id,newPrice);
            if (isSuccessful)
                return Ok();
            return BadRequest();
        }
    }
}
