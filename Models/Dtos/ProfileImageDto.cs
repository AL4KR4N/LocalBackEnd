using System.ComponentModel.DataAnnotations;
namespace monchotradebackend.models.dtos; 
public class ProfileImageDto
{
    [Required]
    public int UserId { get; set; }
    [Required]
    public IFormFile? ImageFile {get;set;}
}

public class ProfileImageUpdateDto{
    [Required]
    public int Id {get;set;}

    [Required]
    public int UserId {get;set;}

    [Required]
    [MaxLength(50)]
    public string? ProfileImageUrl {get;set;}

    public IFormFile? ImageFile {get;set;}

}