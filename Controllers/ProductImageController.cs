using Microsoft.AspNetCore.Mvc;
using monchotradebackend.Interface; 
using monchotradebackend.models.entities; 
using monchotradebackend.models.dtos; 
using Microsoft.EntityFrameworkCore;
using monchotradebackend.service;

/*
    Endpoints implementados
    Get GetAllProductImages
    Get product/{productId} GetProductImagesByProductId
    Post CreateProductImage
    Put{id} UpdateProductImage -> cambiar a patch
    Delete{id} DeleteProductImage
*/


namespace monchotradebackend.controllers
{
    [ApiController]
    [Route("productimage")]
    public class ProductImageController : ControllerBase
    {
        private readonly IFileService _fileService;
        private readonly IRepository<ProductImage, int> _dbRepository;
        private readonly ILogger<ProductImageController> _logger;

        public ProductImageController(
            IFileService fileService, 
            IRepository<ProductImage, int> dbRepository, 
            ILogger<ProductImageController> logger)
        {
            _fileService = fileService;
            _dbRepository = dbRepository;
            _logger = logger;
        }

        [HttpPost]
        public async Task<IActionResult> CreateProductImage([FromForm] ProductImageDto imageToAdd)
        {
            try
            {
                if(imageToAdd.ImageFile?.Length > 1 * 1024 * 1024)
                {
                    return StatusCode(StatusCodes.Status400BadRequest, "File size should not exceed 1 MB");             
                }

                string[] allowedFileExtensions = [".jpg", ".jpeg", ".png"];

                if(imageToAdd.ImageFile == null)
                    return StatusCode(StatusCodes.Status400BadRequest, "ImageFile is null");

                string createdImageName = await _fileService.SaveFileAsync(
                    imageToAdd.ImageFile, 
                    allowedFileExtensions,
                    UploadType.Product
                ); 

                var image = new ProductImage
                {
                    ProductId = imageToAdd.ProducId, 
                    Url = createdImageName 
                };

                var createdProduct = await _dbRepository.InsertAsync(image); 
                await _dbRepository.SaveChangesAsync();

                return CreatedAtAction(nameof(CreateProductImage), createdProduct); 
            }
            catch(Exception ex)
            {
                _logger.LogError(ex.Message); 
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message); 
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProductImage(int id, [FromForm] ProductImageUpdateDto productImageToUpdate)
        {
            try
            {
                if(id != productImageToUpdate.Id)
                {
                    return StatusCode(StatusCodes.Status400BadRequest, "id in url and form body does not match."); 
                }

                var existingProductImage = await _dbRepository.GetQueryable().FirstOrDefaultAsync(p => p.ProductId == productImageToUpdate.ProductId); 
                if(existingProductImage == null)
                {
                    return StatusCode(StatusCodes.Status404NotFound, $"Product image with id : {id} not found");
                }

                string oldImage = existingProductImage.Url;
                if(productImageToUpdate.ImageFile != null)
                {
                    if(productImageToUpdate.ImageFile.Length > 1 * 1024 * 1024)
                    {
                        return StatusCode(StatusCodes.Status400BadRequest, "File size should not exceed 1 MB");             
                    }

                    string[] allowedFileExtensions = [".jpg", ".jpeg", ".png"]; 

                    string createdImageName = await _fileService.SaveFileAsync(
                        productImageToUpdate.ImageFile, 
                        allowedFileExtensions,
                        UploadType.Product
                    ); 
                    productImageToUpdate.ImageUrl = createdImageName;
                }

                if(productImageToUpdate.ImageUrl == null)
                    return StatusCode(StatusCodes.Status400BadRequest, "Product Image Url is null");

               // existingProductImage.Id = productImageToUpdate.Id;
               // existingProductImage.ProductId = productImageToUpdate.ProductId; 
                existingProductImage.Url = productImageToUpdate.ImageUrl;

                var updatedProduct = await _dbRepository.UpdateAsync(existingProductImage);
                await _dbRepository.SaveChangesAsync();

                if(productImageToUpdate.ImageFile != null)
                {
                    _fileService.DeleteFile(oldImage, UploadType.Product);
                }

                return Ok(updatedProduct); 
            }
            catch(Exception ex)
            {
                _logger.LogError(ex.Message); 
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message); 
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProductImage(int id)
        {
            try
            {
                var existingProductImage = await _dbRepository.GetByIdAsync(id);
                if (existingProductImage == null)
                {
                    return StatusCode(StatusCodes.Status404NotFound, $"Product image with id: {id} not found");
                }

                await _dbRepository.DeleteAsync(existingProductImage);
                await _dbRepository.SaveChangesAsync();

                _fileService.DeleteFile(existingProductImage.Url, UploadType.Product);
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpGet("product/{productId}")]
        public async Task<IActionResult> GetProductImagesByProductId(int productId)
        {
            try 
            {
                var productImages = await _dbRepository.GetQueryable()
                    .Include(i => i.Product)
                    .Where(i => i.ProductId == productId)
                    .ToListAsync(); 

                if (productImages == null || !productImages.Any())
                {
                    return StatusCode(StatusCodes.Status404NotFound, $"Product images for product id: {productId} not found");
                }

                var imagePaths = productImages.Select(image => $"/uploads/products/{image.Url}").ToList();
                return Ok(imagePaths);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetAllProductImages()
        {
            try
            {
                var productImages = await _dbRepository.GetAllAsync();
                return Ok(productImages);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}