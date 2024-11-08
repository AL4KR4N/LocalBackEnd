using Microsoft.AspNetCore.Mvc;
using monchotradebackend.Interface;
using monchotradebackend.models.entities;
using monchotradebackend.models.dtos;
using Microsoft.EntityFrameworkCore;
using monchotradebackend.service;
using Newtonsoft.Json;
using Azure;
using Microsoft.AspNetCore.JsonPatch;
using System.ComponentModel.DataAnnotations;

/*
    Endpoints implementados
    
    Get{id} GetUserById
    Patch{id} UpdateUser
    Get products/{useid} GetProductsByUserId
*/



namespace monchotradebackend.controllers 
{
    [ApiController]
    [Route("user")]
    public class UserController : ControllerBase
    {
        private readonly IRepository<User, int> _dbRepository;
        private readonly IPasswordHashingService _passwordHashingService;
        private readonly ILogger<UserController> _logger;

        public UserController(
            IRepository<User, int> dbRepository,
            IPasswordHashingService passwordHashingService,
            ILogger<UserController> logger)
        {
            _dbRepository = dbRepository;
            _passwordHashingService = passwordHashingService;
            _logger = logger;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<UserMyProfileResponseDto>> GetUserById(int id)
        {
            try
            {
                var user = await _dbRepository.GetQueryable()
                    .Include(u => u.ProfileImage)
                    .Include(u => u.Products)
                    .FirstOrDefaultAsync(u => u.Id == id);

                if (user == null)
                {
                    return NotFound("User not found.");
                }

                var userDto = new UserMyProfileResponseDto
                {
                    Name = user.Name,
                    LastName = user.LastName,
                    SecondLastName = user.SecondLastName,
                    Email = user.Email,
                    PhoneNumber = user.PhoneNumber,
                    Country = user.Country,
                    ProfileImageUrl = user.ProfileImage.Url
                };

                return Ok(userDto);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error fetching user data for ID: {Id}", id);
                return StatusCode(500, "An error occurred while fetching user data.");
            }
        }

        //Proper patch update implementation 
        [HttpPatch("{id}")]
        public async Task<IActionResult> UpdateUser(int id, [FromBody] JsonPatchDocument<UserUpdateDto> patchDoc)
        {
            try
            {
                if (patchDoc == null)
                {
                    return BadRequest("Patch document cannot be null");
                }

                // Get existing user
                var existingUser = await _dbRepository.GetQueryable()
                    .Include(u => u.ProfileImage)
                    .FirstOrDefaultAsync(u => u.Id == id);

                if (existingUser == null)
                {
                    return NotFound($"User with ID {id} not found");
                }

                // Create DTO from existing user
                var userDto = new UserUpdateDto
                {
                    Name = existingUser.Name,
                    LastName = existingUser.LastName,
                    SecondLastName = existingUser.SecondLastName,
                    Email = existingUser.Email,
                    PhoneNumber = existingUser.PhoneNumber,
                    Country = existingUser.Country
                };

                // Apply patch operations to the DTO
                patchDoc.ApplyTo(userDto, ModelState);

                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                // Validate the patched DTO
                var validationContext = new ValidationContext(userDto);
                var validationResults = new List<ValidationResult>();
                if (!Validator.TryValidateObject(userDto, validationContext, validationResults, true))
                {
                    return BadRequest(validationResults);
                }

                // Update the existing user entity with patched values
                existingUser.Name = userDto.Name;
                existingUser.LastName = userDto.LastName;
                existingUser.SecondLastName = userDto.SecondLastName;
                existingUser.Email = userDto.Email;
                existingUser.PhoneNumber = userDto.PhoneNumber;
                existingUser.Country = userDto.Country;

                // Save changes
                await _dbRepository.UpdateAsync(existingUser);
                await _dbRepository.SaveChangesAsync();

                // Return updated user
                return Ok(new UserMyProfileResponseDto
                {
                    Name = existingUser.Name,
                    LastName = existingUser.LastName,
                    SecondLastName = existingUser.SecondLastName,
                    Email = existingUser.Email,
                    PhoneNumber = existingUser.PhoneNumber,
                    Country = existingUser.Country,
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating user with ID {Id}", id);
                return StatusCode(500, "An error occurred while updating the user");
            }
        }

    [HttpGet("products/{userId}")]
    public async Task<ActionResult<List<ProductDto>>> GetProductsByUserId(int userId)
    {
        try
        {      
            var user = await _dbRepository.GetQueryable()
                .Include(p => p.Products)
                    .ThenInclude(p => p.Images)
                .FirstOrDefaultAsync(p => p.Id == userId);

        if (user == null)
        {
            return NotFound("User not found.");
        }

        if (user.Products == null || !user.Products.Any())
        {
            return NotFound("No products found for this user.");
        }

        // Changed from CountAsync() to Count()
        var totalItems = user.Products.Count();


            var productsDto = user.Products.Select(product => new ProductGetbyUserIdDto
            {
                Id = product.Id,
                Title = product.Name,
                ImageUrl = $"https://localhost:7001/uploads/products/{product.Images?.FirstOrDefault()?.Url ?? "default-image.jpg"}",
                OfferedBy = user.Name, // o cualquier propiedad del usuario que identifique al vendedor
                Description = product.Description,
                Category = product.Category,
                TotalNumber = totalItems,
                Quantity = product.Quantity
                // Agrega otras propiedades según tu modelo
            }).ToList();

            return Ok(productsDto);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error fetching products for user ID: {Id}", userId);
            return StatusCode(500, "An error occurred while fetching products.");
        }
    }

    }
}