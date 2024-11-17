using System.ComponentModel.DataAnnotations;

namespace monchotradebackend.models.entities;

public class User
{
    [Key]
    public int Id { get; set; }

    [Required(ErrorMessage = "The first name is required.")]
    [MaxLength(50, ErrorMessage = "The first name must not exceed 50 characters.")]
    public string Name { get; set; } = string.Empty;

    [Required(ErrorMessage = "The last name is required.")]
    [MaxLength(50, ErrorMessage = "The last name must not exceed 50 characters.")]
    public string LastName { get; set; } = string.Empty;

    [MaxLength(50, ErrorMessage = "The second last name must not exceed 50 characters.")]
    public string? SecondLastName { get; set; }

    [Required(ErrorMessage = "The birth date is required.")]
    public DateOnly BirthDate { get; set; }

    [Required(ErrorMessage = "The email address is required.")]
    [EmailAddress(ErrorMessage = "The email address is not valid.")]
    public string Email { get; set; } = string.Empty;

    [Required(ErrorMessage = "The country is required.")]
    [MaxLength(50, ErrorMessage = "The country name must not exceed 50 characters.")]
    public string Country { get; set; } = string.Empty;

    [Required(ErrorMessage = "The phone number is required.")]
    [Phone(ErrorMessage = "The phone number format is not valid.")]
    public string PhoneNumber { get; set; } = string.Empty;

    [Required(ErrorMessage = "The password hash is required.")]
    public string PasswordHash { get; set; } = string.Empty;

    // Virtual navigation properties for lazy loading
    
    // Intercambios donde el usuario es el iniciador
    public virtual ICollection<Exchange> InitiatedExchanges { get; set; } = new List<Exchange>();

    // Intercambios donde el usuario es el receptor
    public virtual ICollection<Exchange> ReceivedExchanges { get; set; } = new List<Exchange>();
    
    public virtual ProfileImage? ProfileImage { get; set; }
    public virtual ICollection<Product> Products { get; set; } = new List<Product>();
}


