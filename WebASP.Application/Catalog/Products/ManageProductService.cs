using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using WebASP.Application.Common;
using WebASP.Data.EF;
using WebASP.Data.Entities;
using WebASP.Utilities.Exceptions;
using WebASP.ViewModels.Catalog.ProductImage;
using WebASP.ViewModels.Catalog.Products;
using WebASP.ViewModels.Common;

namespace WebASP.Application.Catalog.Products
{
    public class ManageProductService : IManageProductService
    {
        private readonly WebASPDbContext _context;
        private readonly IStorageService _storageService;
        public ManageProductService(WebASPDbContext context, IStorageService storageService)
        {
            _context = context;
            _storageService = storageService;
        }
        //API PRODUCT=======================================================================================================================
        public async Task<int> Create(ProductCreateRequest request)
        {
            var product = new Product()
            {
                Price = request.Price,
                OriginalPrice = request.OriginalPrice,
                Stock = request.Stock,
                ViewCount = 0,
                DateCreated = DateTime.Now,
                ProductTranslations = new List<ProductTranslation>()
                {
                    new ProductTranslation()
                    {
                        Name=request.Name,
                        Description=request.Description,
                        Details=request.Details,
                        SeoAlias=request.SeoAlias,
                        SeoTitle=request.SeoTitle,
                        SeoDescription=request.SeoDescription,
                        LanguageId=request.LanguageId
                    }
                }
            };
            //save image
            if (request.ThumbnailImage != null)
            {
                product.ProductImages = new List<ProductImage>()
                {
                    new ProductImage()
                    {
                        Caption="Thumbnail image",
                        DateCreated=DateTime.Now,
                        FileSize=request.ThumbnailImage.Length,
                        ImagePath=await this.SaveFile(request.ThumbnailImage),
                        IsDefault=true,
                        SortOrder=1
                    }
                };
            }
            _context.Products.Add(product);
            await _context.SaveChangesAsync();
            return product.Id;
        }

        public async Task<PagedResult<ProductViewModel>> GetAllPaging(GetManageProductPagingRequest request)
        {
            //1. Select join
            var query = from p in _context.Products
                        join pt in _context.ProductTranslations on p.Id equals pt.ProductId
                        join pic in _context.ProductInCategories on p.Id equals pic.ProductId
                        join c in _context.Categories on pic.CategoryId equals c.Id
                        select new { p, pt, pic };
            //2. filter
            if (!string.IsNullOrEmpty(request.Keyword))
                query = query.Where(x => x.pt.Name.Contains(request.Keyword));

            if (request.CategoryId.Count > 0)
            {
                query = query.Where(p => request.CategoryId.Contains(p.pic.CategoryId));
            }
            //3. Paging
            int totalRow = await query.CountAsync();

            var data = await query.Skip((request.PageIndex - 1) * request.PageSize)
                .Take(request.PageSize)
                .Select(x => new ProductViewModel()
                {
                    Id = x.p.Id,
                    Name = x.pt.Name,
                    DateCreated = x.p.DateCreated,
                    Description = x.pt.Description,
                    Details = x.pt.Details,
                    LanguageId = x.pt.LanguageId,
                    OriginalPrice = x.p.OriginalPrice,
                    Price = x.p.Price,
                    SeoAlias = x.pt.SeoAlias,
                    SeoDescription = x.pt.SeoDescription,
                    SeoTitle = x.pt.SeoTitle,
                    Stock = x.p.Stock,
                    ViewCount = x.p.ViewCount
                }).ToListAsync();

            //4. Select and projection
            var pagedResult = new PagedResult<ProductViewModel>()
            {
                TotalRecord = totalRow,
                Items = data
            };
            return pagedResult;
        }

        public async Task<int> Update(ProductUpdateRequest request)
        {
            var product = await _context.Products.FindAsync(request.Id);
            var productTranSlations = await _context.ProductTranslations.FirstOrDefaultAsync(x => x.ProductId == request.Id && x.LanguageId == request.LanguageId);
            if (product == null || productTranSlations==null) throw new WebASPException($"cannot find a product with id:{request.Id}");

            productTranSlations.Name = request.Name;
            productTranSlations.SeoAlias = request.SeoAlias;
            productTranSlations.SeoDescription = request.SeoDescription;
            productTranSlations.SeoTitle = request.SeoTitle;
            productTranSlations.Description = request.Description;
            productTranSlations.Details = request.Details;
            //save image
            if (request.ThumbnailImage != null)
            {
                var thumbnailImage = await _context.ProductImages.FirstOrDefaultAsync(i => i.IsDefault == true && i.ProductId == request.Id);
                if (thumbnailImage != null)
                {
                    thumbnailImage.FileSize = request.ThumbnailImage.Length;
                    thumbnailImage.ImagePath = await this.SaveFile(request.ThumbnailImage);
                    _context.ProductImages.Update(thumbnailImage);
                }
            }
            return await _context.SaveChangesAsync();

        }

        public async Task<int> Delete(int productId)
        {
            var product = await _context.Products.FindAsync(productId);
            if (product == null)
                throw new WebASPException($"cannot find a product:{productId}");
            var images = _context.ProductImages.Where(i => i.ProductId == productId);
            foreach (var image in images)
            {
                await _storageService.DeleteFileAsync(image.ImagePath);
            }

            _context.Products.Remove(product);

            return await _context.SaveChangesAsync();
        }

        public async Task AddViewCount(int productId)
        {
            var product = await _context.Products.FindAsync(productId);
            product.ViewCount += 1;
            await _context.SaveChangesAsync();

        }

        public async Task<bool> UpdatePrice(int productId, decimal newPrice)
        {
            var product = await _context.Products.FindAsync(productId);
            if (product == null) throw new WebASPException($"Cannot find a product with id: {productId}");
            product.Price = newPrice;
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> UpdateStock(int productId, int addedQuantity)
        {
            var product = await _context.Products.FindAsync(productId);
            if (product == null) throw new WebASPException($"Cannot find a product with id: {productId}");
            product.Stock += addedQuantity;
            return await _context.SaveChangesAsync() > 0;
        }

        private async Task<string> SaveFile(IFormFile file)
        {
            var originalFileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
            var fileName = $"{Guid.NewGuid()}{Path.GetExtension(originalFileName)}";
            await _storageService.SaveFileAsync(file.OpenReadStream(), fileName);
            return fileName;
        }

        public async Task<ProductViewModel> GetById(int productId, string languageId)
        {
            var product = await _context.Products.FindAsync(productId);
            var productTranslation = await _context.ProductTranslations.FirstOrDefaultAsync(x => x.ProductId == productId && x.LanguageId == languageId);

            var productViewModel = new ProductViewModel()
            {
                Id = product.Id,
                DateCreated = product.DateCreated,
                Description = productTranslation != null ? productTranslation.Description : null,
                LanguageId = productTranslation.LanguageId,
                Details = productTranslation != null ? productTranslation.Details : null,
                Name = productTranslation != null ? productTranslation.Name : null,
                OriginalPrice = product.OriginalPrice,
                Price = product.Price,
                SeoAlias = productTranslation != null ? productTranslation.SeoAlias : null,
                SeoDescription = productTranslation != null ? productTranslation.SeoDescription : null,
                SeoTitle = productTranslation != null ? productTranslation.SeoTitle : null,
                Stock = product.Stock,
                ViewCount = product.ViewCount
            };
            return productViewModel;
        }

        public async Task<int> AddImage( ProductImageCreateRequest request)
        {
            var productImage = new ProductImage()
            {
                Caption = request.Caption,
                IsDefault = request.IsDefault,
                DateCreated = DateTime.Now,
                SortOrder = request.SortOrder,
                ProductId = request.ProductId
            };
            if (request.ImageFile != null)
            {
                productImage.ImagePath = await this.SaveFile(request.ImageFile);
                productImage.FileSize = request.ImageFile.Length;
            }
            _context.ProductImages.Add(productImage);
            await _context.SaveChangesAsync();
            return productImage.Id;
        }

        public async Task<int> UpdateImage(ProductImageUpdateRequest request)
        {
            var productImage = await _context.ProductImages.FindAsync(request.Id);
            if (productImage == null) throw new WebASPException($"cannot find an image With id :{request.Id}");
            if (request.ImageFile != null)
            {
                productImage.ImagePath = await this.SaveFile(request.ImageFile);
                productImage.FileSize = request.ImageFile.Length;
                productImage.Caption = request.Caption;
                productImage.DateCreated = DateTime.Now;
                productImage.IsDefault = request.IsDefault;
                productImage.SortOrder = request.SortOrder;
            }
            _context.ProductImages.Update(productImage);
            return await _context.SaveChangesAsync();
        }

        public async Task<int> DeleteImage(int imageId)
        {
            var productImage = await _context.ProductImages.FindAsync(imageId);
            if (productImage == null) throw new WebASPException($"cannot find an image with id :{imageId}");
            _context.ProductImages.Remove(productImage);
            return await _context.SaveChangesAsync();
        }

        public async Task<ProductImageViewModel> GetImageById(int imageId)
        {
            var imageView = await _context.ProductImages.FindAsync(imageId);
            if (imageView == null) throw new WebASPException($"cannot image with id:{imageId}");
            var viewlImage = new ProductImageViewModel()
            {
                Id = imageId,
                Caption = imageView.Caption,
                ImagePath = imageView.ImagePath,
                FileSize = imageView.FileSize,
                IsDefault = imageView.IsDefault,
                ProductId = imageView.ProductId,
                SortOrder = imageView.SortOrder,
                DateCreated = imageView.DateCreated
            };
            return viewlImage;
        }

        public async Task<List<ProductImageViewModel>> GetListImageById(int productId)
        {
            var listImage = await _context.ProductImages.Where(x => x.ProductId == productId)
                 .Select(x => new ProductImageViewModel()
                 {
                     Caption = x.Caption,
                     SortOrder = x.SortOrder,
                     IsDefault = x.IsDefault,
                     Id = x.Id,
                     ProductId = x.ProductId,
                     DateCreated = x.DateCreated
                 }).ToListAsync();
            return listImage;

        }

        //API IMAGE=========================================================================================================================

    }
}
