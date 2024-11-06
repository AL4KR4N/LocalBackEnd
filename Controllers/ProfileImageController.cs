using Microsoft.AspNetCore.Mvc;
using monchotradebackend.Interface; 
using monchotradebackend.models.entities; 
using monchotradebackend.models.dtos; 
using Microsoft.EntityFrameworkCore;
using monchotradebackend.service;

namespace monchotradebackend.controllers
{
    [ApiController]
    [Route("profileimage")]
    public class ProfileImageController : ControllerBase
    {
        private readonly IFileService _fileService;
        private readonly IRepository<ProfileImage, int> _dbRepository;
        private readonly ILogger<ProfileImageController> _logger;

        public ProfileImageController(
            IFileService fileService, 
            IRepository<ProfileImage, int> dbRepository, 
            ILogger<ProfileImageController> logger)
        {
            _fileService = fileService;
            _dbRepository = dbRepository;
            _logger = logger;
        }

        [HttpPost]
        public async Task<IActionResult> CreateProfileImage([FromForm] ProfileImageDto imageToAdd)
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
                    UploadType.Profile
                ); 

                var image = new ProfileImage
                {
                    UserId = imageToAdd.UserId, 
                    Url = createdImageName 
                };

                var createdProfile = await _dbRepository.InsertAsync(image); 
                await _dbRepository.SaveChangesAsync();

                return CreatedAtAction(nameof(CreateProfileImage), createdProfile); 
            }
            catch(Exception ex)
            {
                _logger.LogError(ex.Message); 
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message); 
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProfileImage(int id, [FromForm] ProfileImageUpdateDto profileImageToUpdate)
        {
            try
            {
                if(id != profileImageToUpdate.Id)
                {
                    return StatusCode(StatusCodes.Status400BadRequest, "id in url and form body does not match."); 
                }

                var existingProfileImage = await _dbRepository.GetByIdAsync(id); 
                if(existingProfileImage == null)
                {
                    return StatusCode(StatusCodes.Status404NotFound, $"Profile image with id : {id} not found");
                }

                string oldImage = existingProfileImage.Url;
                if(profileImageToUpdate.ImageFile != null)
                {
                    if(profileImageToUpdate.ImageFile.Length > 1 * 1024 * 1024)
                    {
                        return StatusCode(StatusCodes.Status400BadRequest, "File size should not exceed 1 MB");             
                    }

                    string[] allowedFileExtensions = [".jpg", ".jpeg", ".png"]; 

                    string createdImageName = await _fileService.SaveFileAsync(
                        profileImageToUpdate.ImageFile, 
                        allowedFileExtensions,
                        UploadType.Profile
                    ); 
                    profileImageToUpdate.ProductImage = createdImageName;
                }

                if(profileImageToUpdate.ProductImage == null)
                    return StatusCode(StatusCodes.Status400BadRequest, "ProductImage Url is null");

                existingProfileImage.Id = profileImageToUpdate.Id;
                existingProfileImage.UserId = profileImageToUpdate.UserId; 
                existingProfileImage.Url = profileImageToUpdate.ProductImage;

                var updatedProduct = await _dbRepository.UpdateAsync(existingProfileImage);
                await _dbRepository.SaveChangesAsync();

                if(profileImageToUpdate.ImageFile != null)
                {
                    _fileService.DeleteFile(oldImage, UploadType.Profile);
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
        public async Task<IActionResult> DeleteProfileImage(int id)
        {
            try
            {
                var existingProfileImage = await _dbRepository.GetByIdAsync(id);
                if (existingProfileImage == null)
                {
                    return StatusCode(StatusCodes.Status404NotFound, $"Profile image with id: {id} not found");
                }

                await _dbRepository.DeleteAsync(existingProfileImage);
                await _dbRepository.SaveChangesAsync();

                _fileService.DeleteFile(existingProfileImage.Url, UploadType.Profile);
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpGet("user/{id}")]
        public async Task<IActionResult> GetProfileImageBy(int id)
        {
            try 
            {
                var profiles = await _dbRepository.GetQueryable()
                    .Include(i => i.User)
                    .Where(i => i.User.Id == id)
                    .ToListAsync(); 

                if (profiles == null || !profiles.Any())
                {
                    return StatusCode(StatusCodes.Status404NotFound, $"Profile image with userId: {id} not found");
                }

                var path = $"/uploads/profiles/{profiles[0].Url}";
                return Ok(path);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetProfilePics()
        {
            try
            {
                var profiles = await _dbRepository.GetAllAsync();
                return Ok(profiles);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}