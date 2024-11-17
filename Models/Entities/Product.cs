using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using monchotradebackend.models.entities;

/*
    Id 
    UserId
    Name
    Description
    Quantity
    Category
    CreatedAt
    UpdatedAt
    IsActive
*/


namespace monchotradebackend.models.entities
{
    public class Product
    {
        public int Id { get; set; }  // Unique identifier for each product
        public int? UserId { get; set; }      // ID of the user who owns the product

        [Required]
        [StringLength(100, ErrorMessage = "Name can't be longer than 100 characters.")]
        public string Name { get; set; } = string.Empty; 

        [StringLength(500, ErrorMessage = "Description can't be longer than 500 characters.")]
        public string Description { get; set; } = string.Empty; 

        [Range(0, int.MaxValue, ErrorMessage = "Quantity cannot be negative.")]
        public int Quantity { get; set; }

        [Required]
        public int CategoryId { get; set; } 

        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public DateTime? UpdatedAt { get; set; }

        public bool IsActive { get; set; } = true;

        // Virtual navigation property for lazy loading of images
        public virtual ICollection<ProductImage> Images { get; set; } = new List<ProductImage>();

        // Navigation property to associate with the user
        public virtual User? User { get; set; }
        public virtual Category? ProductCategory {get;set;}
    }
}


