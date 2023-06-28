using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebASP.Application.Catalog.Products;
using WebASP.ViewModels.Catalog.ProductImage;
using WebASP.ViewModels.Catalog.Products;

namespace WebASP.BackendAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
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
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var productId = await _manageProductService.Create(request);
            if (productId == 0) return BadRequest();
            var product = await _manageProductService.GetById(productId,request.LanguageId);
            return CreatedAtAction(nameof(GetById),new { id = productId },product);
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromForm] ProductUpdateRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var affectedResult = await _manageProductService.Update(request);
            if (affectedResult == 0) return BadRequest();
            return Ok();
        }

        [HttpDelete("{productId}")]
        public async Task<IActionResult> Delete(int productId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var affectedResult = await _manageProductService.Delete(productId);
            if (affectedResult == 0) return BadRequest();
            return Ok();
        }

        [HttpPatch("price/{newPrice}")]
        public async Task<IActionResult> UpdatePrice([FromQuery]  int id,decimal newPrice)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var isSuccessful = await _manageProductService.UpdatePrice(id,newPrice);
            if (isSuccessful)
                return Ok();
            return BadRequest();
        }

        [HttpGet("image/{imageId}")]
        public async Task<IActionResult> GetImageById(int imageId)
        {
            var productImage = await _manageProductService.GetImageById(imageId);
            if (productImage == null) return BadRequest("cannot find image");

            return Ok(productImage);
        }

        [HttpPost("image")]
        public async Task<IActionResult> AddImage([FromForm] ProductImageCreateRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var productImage = await _manageProductService.AddImage(request);
            if (productImage == 0) return BadRequest();
            var image = await _manageProductService.GetImageById(productImage);
            return CreatedAtAction(nameof(GetImageById), new { id = productImage }, image);
        }

        [HttpPut("image")]
        public async Task<IActionResult> UpdateImage([FromForm] ProductImageUpdateRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var Result = await _manageProductService.UpdateImage(request);
            if (Result == 0) return BadRequest();
            return Ok();
        }

        [HttpDelete("image/{imageId}")]
        public async Task<IActionResult> DeleteImage(int imageId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var Result = await _manageProductService.DeleteImage(imageId);
            if (Result == 0) return BadRequest();
            return Ok();
        }
        [HttpGet("{productId}/listImage")]
        public async Task<IActionResult> GetListImageById(int productId)
        {
            var productImage = await _manageProductService.GetListImageById(productId);
            if (productImage == null) return BadRequest("cannot find image");

            return Ok(productImage);
        }
    }
}
