
using Microsoft.AspNetCore.Mvc;
using monchotradebackend.Interface; 
using monchotradebackend.models.entities; 
using monchotradebackend.models.dtos; 
using Microsoft.EntityFrameworkCore;
using monchotradebackend.service;
using Microsoft.Identity.Client;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.JsonPatch;

/*
    Endpoints implementados
    Get GetAllProducts
    Get{id} GetProductById
    Post CreateProduct
    Patch {id} UpdateProduct
    Delete {id} DeleteProduct
*/


namespace monchotradebackend.controllers
{

    public static class ModelStateExtensions
    {
    public static string GetFullErrorMessage(this ModelStateDictionary modelState)
    {
        var messages = new List<string>();

        foreach (var entry in modelState)
        {
            foreach (var error in entry.Value.Errors)
            {
                messages.Add(error.ErrorMessage);
            }
        }

        return string.Join(" ", messages);
    }
    }

    public class PaginationParameters
    {
        private const int MaxPageSize = 50;
        private int _pageSize = 6;
        private int _pageNumber = 1;

        public int PageNumber
        {
            get => _pageNumber;
            set => _pageNumber = value < 1 ? 1 : value;
        }

        public int PageSize
        {
            get => _pageSize;
            set => _pageSize = value > MaxPageSize ? MaxPageSize : value < 1 ? 1 : value;
        }
    }

    public class PaginatedResponse<T>
    {
        public IEnumerable<T> Items { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public int TotalPages { get; set; }
        public int TotalItems { get; set; }
        public bool HasPrevious => PageNumber > 1;
        public bool HasNext => PageNumber < TotalPages;
    }




    [ApiController]
    [Route("product")]
    public class ProductController : ControllerBase
    {
        private readonly IFileService _fileService;
        private readonly IRepository<Product, int> _dbRepository;
        private readonly ILogger<ProductController> _logger;

        public ProductController(
            IFileService fileService, 
            IRepository<Product, int> dbRepository, 
            ILogger<ProductController> logger)
        {
            _fileService = fileService;
            _dbRepository = dbRepository;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<PaginatedResponse<ProductDto>>> GetAllProducts([FromQuery] PaginationParameters parameters)
        {
            try
            {
                // Get the base query
                var query = _dbRepository.GetQueryable()
                    .Include(u => u.User)
                    .Include(i => i.Images)
                    .Include(p => p.ProductCategory);

                // Get total count
                var totalItems = await query.CountAsync();

                // Calculate total pages
                var totalPages = (int)Math.Ceiling(totalItems / (double)parameters.PageSize);

                // Get paginated data
                var products = await query
                    .Skip((parameters.PageNumber - 1) * parameters.PageSize)
                    .Take(parameters.PageSize)
                    .Select(p => new ProductDto
                    {
                        Id = p.Id,
                        Title = p.Name,
                        ImageUrl = p.Images.FirstOrDefault().Url,
                        OfferedBy = p.User != null ? p.User.Name : "",
                        Description = p.Description,
                        Category = p.ProductCategory.Name,
                        TotalNumber = totalItems
                    })
                    .ToListAsync();

                // Create pagination response
                var response = new PaginatedResponse<ProductDto>
                {
                    Items = products,
                    PageNumber = parameters.PageNumber,
                    PageSize = parameters.PageSize,
                    TotalPages = totalPages,
                    TotalItems = totalItems
                };

                // Add pagination headers
                Response.Headers.Add("X-Pagination", System.Text.Json.JsonSerializer.Serialize(new
                {
                    totalItems,
                    totalPages,
                    currentPage = parameters.PageNumber,
                    pageSize = parameters.PageSize
                }));

                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error fetching products data.");
                return StatusCode(500, "An error occurred while fetching products data.");
            }
        }

        // GET product by id 
    [HttpGet("{id}")]
    public async Task<ActionResult<ProductDto>> GetProductByID(int id)
    {
        try
        {
            var product = await _dbRepository.GetQueryable()
                .Include(p => p.User)
                .Include(p => p.Images)
                .Include(p => p.ProductCategory)
                
                .FirstOrDefaultAsync(p => p.Id == id);

            if (product == null)
            {
                return NotFound("Product not found.");
            }

            // Assuming you're mapping the product to a DTO
            var productDto = new ProductDto
            {
                Id = product.Id,
                Title = product.Name,
                ImageUrl = product.Images.FirstOrDefault()?.Url ?? "",  // Use null-checking for Images collection
                OfferedBy = product.User?.Name ?? "",  // Use null-checking for User
                Description = product.Description,
                Category = product.ProductCategory.Name
            };

            return Ok(productDto);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error fetching product data.");
            return StatusCode(500, "An error occurred while fetching product data.");
        }
    }


        //Create/Post product 
        [HttpPost]
        public async Task<ActionResult> CreateProduct(Product  newProduct)
        {
            try
            {
                bool exists = await _dbRepository.GetQueryable()
                    .AnyAsync(s => s.Id == newProduct.Id);

                if (exists)
                {
                    return BadRequest("Id del salario ya está registrado.");
                }

                await _dbRepository.InsertAsync(newProduct);
                await _dbRepository.SaveChangesAsync();

                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating new product.");
                return StatusCode(500, "An error occurred while creating new product.");
            }
        }

        //Update/Put product 
 [HttpPatch("{id}")]
public async Task<IActionResult> UpdateProduct(int id, [FromBody] JsonPatchDocument<ProductUpdateDto> patchDoc)
{
    try
    {
        if (patchDoc == null)
        {
            return BadRequest("Patch document cannot be null");
        }

        // Get existing product
        var existingProduct = await _dbRepository.GetQueryable()
            .Include(p => p.User)
            .Include(p => p.Images)
            .Include(p => p.ProductCategory)
            .FirstOrDefaultAsync(p => p.Id == id);

        if (existingProduct == null)
        {
            return NotFound($"Product with ID {id} not found");
        }

        // Create DTO from existing product
        var productDto = new ProductUpdateDto
        {
            Title = existingProduct.Name,
            ImageUrl = existingProduct.Images?.FirstOrDefault()?.Url ?? string.Empty,
            OfferedBy = existingProduct.User.Name,
            Description = existingProduct.Description,
            Category = existingProduct.ProductCategory.Name,
            Quantity = existingProduct.Quantity
        };

        // Apply patch operations to the DTO
        patchDoc.ApplyTo(productDto, ModelState);

        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        // Validate the patched DTO
        var validationContext = new ValidationContext(productDto);
        var validationResults = new List<ValidationResult>();
        if (!Validator.TryValidateObject(productDto, validationContext, validationResults, true))
        {
            return BadRequest(validationResults);
        }

        // Update the existing product entity with patched values
        existingProduct.Name = productDto.Title;
        existingProduct.User.Name = productDto.OfferedBy;
        existingProduct.Description = productDto.Description;
        existingProduct.ProductCategory.Name = productDto.Category;
        existingProduct.Quantity = productDto.Quantity;

        // Handle image update if the ImageUrl has changed
        if (!string.IsNullOrEmpty(productDto.ImageUrl))
        {
            var currentImage = existingProduct.Images?.FirstOrDefault();
            if (currentImage == null)
            {
                // Add new image
                existingProduct.Images = new List<ProductImage>
                {
                    new ProductImage
                    {
                        Url = productDto.ImageUrl,
                        ProductId = existingProduct.Id
                    }
                };
            }
            else if (currentImage.Url != productDto.ImageUrl)
            {
                // Update existing image
                currentImage.Url = productDto.ImageUrl;
            }
        }

        // Save changes
        await _dbRepository.UpdateAsync(existingProduct);
        await _dbRepository.SaveChangesAsync();

        // Return updated product
        return Ok(new
        {
            Id = existingProduct.Id,
            Title = existingProduct.Name,
            ImageUrl = existingProduct.Images?.FirstOrDefault()?.Url,
            OfferedBy = existingProduct.User.Name,
            Description = existingProduct.Description,
            Category = existingProduct.ProductCategory.Name,
            Quantity = existingProduct.Quantity
        });
    }
    catch (Exception ex)
    {
        _logger.LogError(ex, "Error updating product with ID {Id}", id);
        return StatusCode(500, "An error occurred while updating the product");
    }
}

        //Delete product
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteProduct(int id)
        {
            try
            {
                var product = await _dbRepository.GetByIdAsync(id);
                if (product == null)
                {
                    return NotFound($"Product with ID {id} not found.");
                }

                await _dbRepository.DeleteAsync(product);
                await _dbRepository.SaveChangesAsync();

                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting product with ID {Id}.", id);
                return StatusCode(500, "An error occurred while deleting product.");
            }
        }


    }

}