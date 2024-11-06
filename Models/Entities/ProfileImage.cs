using System.ComponentModel.DataAnnotations;
namespace monchotradebackend.models.entities;
public class ProfileImage
{
     public int Id { get; set; }
        
     // Store the URL or path to the image
    public string Url { get; set; } = string.Empty;
        
    // Foreign key to the user who owns the image
    public int UserId { get; set; }
        
    // Navigation property
    public virtual User? User { get; set; }

}
