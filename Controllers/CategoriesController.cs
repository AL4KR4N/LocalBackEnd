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
using Microsoft.AspNetCore.Mvc;
using monchotradebackend.models.dtos;

/*
    endpoints
    get all 
    get by id 
    get products by category 
    put create 
    patch update
    delete by id
    
*/


namespace monchotradebackend.controllers{
    [ApiController]
    [Route("categories")]
    public class CategoriesController : ControllerBase{

   private readonly IFileService _fileService;
        private readonly IRepository<Category, int> _dbRepository;
        private readonly ILogger<ProductController> _logger;

        public CategoriesController(
            IFileService fileService, 
            IRepository<Category, int> dbRepository, 
            ILogger<ProductController> logger)
        {
            _fileService = fileService;
            _dbRepository = dbRepository;
            _logger = logger;
        }

        
        [HttpGet]
        public async Task<ActionResult<CategoryDto>> GetAllCategories(){
            try{
                var categories = await _dbRepository.GetAllAsync();
                
                var categoryDtos = categories.Select(category => new CategoryDto
                {
                    Id = category.Id,
                    Name = category.Name
                    // Map other properties if necessary
                }).ToList();
                
                return Ok(categoryDtos); 
            }catch (Exception ex)
            {
                _logger.LogError(ex, "Error fetching categories data.");
                return StatusCode(500, "An error occurred while fetching categories data.");
            }
        }

        [HttpGet("products/{categoryId}")]
        public async Task<ActionResult<List<ProductByCategoryDto>>> GetAllProductsByCategory(int categoryId)
        {
            try
            {
                var products = await _dbRepository.GetQueryable()
                    .Where(c => c.Id == categoryId)
                    .SelectMany(c => c.Products)
                    .Select(p => new ProductByCategoryDto
                    {
                        Title = p.Name ?? string.Empty,
                        ImageUrl = p.Images != null && p.Images.Any() 
                            ? p.Images.First().Url ?? string.Empty 
                            : string.Empty,
                        OfferedBy = p.User != null ? p.User.Name ?? string.Empty : string.Empty,
                        Description = p.Description ?? string.Empty,
                        Category = p.ProductCategory != null ? p.ProductCategory.Name ?? string.Empty : string.Empty,
                        Quantity = p.Quantity
                    })
                    .ToListAsync();

                return Ok(products);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error fetching products for category {CategoryId}", categoryId);
                return StatusCode(500, "An error occurred while fetching products by categories data.");
            }
        }

        
        [HttpGet("{id}")]
        public async Task<ActionResult<PaginatedResponse<CategoryDto>>> GetCategoryId(int id){
            try{  
                var category = await _dbRepository.GetByIdAsync(id);

                var response = new CategoryDto{
                    Id = category.Id, 
                    Name = category.Name};
            
                return Ok(response);  

            } catch (Exception ex)
            {
                _logger.LogError(ex, "Error fetching products for category {CategoryId}", id);
                return StatusCode(500, "An error occurred while fetching products by categories data.");
            }
        }

        [HttpPut]
        public async Task<ActionResult> CreateCategory(CategoryCreateDto  newCategory)
        {
            try
            {
                
                if (newCategory == null)
                {
                    return BadRequest("categoria nulo.");
                }

                var category = new Category{
                    Name = newCategory.Name
                };

                await _dbRepository.InsertAsync(category);
                await _dbRepository.SaveChangesAsync();

                return Ok(category);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating new category.");
                return StatusCode(500, "An error occurred while creating new category.");
            }
        }  

        
      [HttpPatch("{id}")]
        public async Task<IActionResult> UpdateCategory(int id, [FromBody] JsonPatchDocument<CategoryDto> patchDoc)
        {
            try
            {
                if (patchDoc == null)
                {
                    return BadRequest("Patch document cannot be null");
                }

                var getExistingCategory = await _dbRepository.GetByIdAsync(id);
                
                if(getExistingCategory == null)
                    return NotFound($"Category with ID {id} not found");
                
                var categoryDto = new CategoryDto
                {
                    Id = getExistingCategory.Id,
                    Name = getExistingCategory.Name
                };

                patchDoc.ApplyTo(categoryDto, ModelState);

                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                // Map the updated DTO back to the entity
                getExistingCategory.Name = categoryDto.Name;
                // Add any other properties that need to be updated

                // Update the entity in the database
                await _dbRepository.UpdateAsync(getExistingCategory);
                await _dbRepository.SaveChangesAsync();

                // Return the updated category
                return Ok(categoryDto);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating category.");
                return StatusCode(500, "An error occurred while updating category.");
            }
        }
    
        


        //Delete category 
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteCategory(int id)
        {
            try
            {
                var category = await _dbRepository.GetByIdAsync(id);
                if (category == null)
                {
                    return NotFound($"Category with ID {id} not found.");
                }

                await _dbRepository.DeleteAsync(category);
                await _dbRepository.SaveChangesAsync();

                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting category with ID {Id}.", id);
                return StatusCode(500, "An error occurred while deleting category.");
            }
        }
    }
}
