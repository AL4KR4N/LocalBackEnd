using System.ComponentModel.DataAnnotations;
namespace monchotradebackend.models.dtos; 
public class ProductImageDto
{
    [Required]
    public int ProducId { get; set; }
    [Required]
    public IFormFile? ImageFile {get;set;}
}


public class ProductImageUpdateDto{
    [Required]
    public int Id {get;set;}

    [Required]
    public int ProductId {get;set;}

    [Required]
    [MaxLength(50)]
    public string? ImageUrl {get;set;}

    public IFormFile? ImageFile {get;set;}

}