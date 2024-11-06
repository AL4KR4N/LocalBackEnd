using System.ComponentModel.DataAnnotations;

namespace monchotradebackend.models.entities;

public class ProductImage
{
    [Key]
    public int Id { get; set; }

    [Required(ErrorMessage = "The image URL is required.")]
    [Url(ErrorMessage = "The URL provided is not valid.")]
    public string Url { get; set; } = string.Empty;

    [Required(ErrorMessage = "The Product ID is required.")]
    public int ProductId { get; set; }

    // Navigation property to the related Product
    public virtual Product? Product { get; set; }
}
