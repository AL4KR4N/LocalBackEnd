using System.ComponentModel.DataAnnotations;


namespace monchotradebackend.models.dtos;


public class UserMyProfileResponseDto{
    public string Name {get;set;} = string.Empty; 
    public string LastName {get;set;} = string.Empty; 
    public string? SecondLastName {get; set;} = string.Empty;
    public string Email {get;set;} = string.Empty;
    public string PhoneNumber {get;set;} = string.Empty;
    public string Country {get;set;} = string.Empty;
    public string ProfileImageUrl {get;set;} = string.Empty; 
}

public class UserUpdateDto{[Required(ErrorMessage = "Name is required")]
    //[StringLength(50, MinimumLength = 2, ErrorMessage = "Name must be between 2 and 50 characters")]
    //[RegularExpression(@"^[a-zA-ZÀ-ÿ\s]*$", ErrorMessage = "Name can only contain letters and spaces")]
    public string? Name { get; set; } = string.Empty;

    //[Required(ErrorMessage = "Last name is required")]
    //[StringLength(50, MinimumLength = 2, ErrorMessage = "Last name must be between 2 and 50 characters")]
    //[RegularExpression(@"^[a-zA-ZÀ-ÿ\s]*$", ErrorMessage = "Last name can only contain letters and spaces")]
    public string? LastName { get; set; } = string.Empty;

    //[StringLength(50, ErrorMessage = "Second last name cannot exceed 50 characters")]
    //[RegularExpression(@"^[a-zA-ZÀ-ÿ\s]*$", ErrorMessage = "Second last name can only contain letters and spaces")]
    public string? SecondLastName { get; set; } = string.Empty;

    //[Required(ErrorMessage = "Email is required")]
    //[EmailAddress(ErrorMessage = "Invalid email format")]
    //[StringLength(100, ErrorMessage = "Email cannot exceed 100 characters")]
    public string? Email { get; set; } = string.Empty;

    //[Required(ErrorMessage = "Phone number is required")]
    //[Phone(ErrorMessage = "Invalid phone number format")]
    //[StringLength(20, MinimumLength = 8, ErrorMessage = "Phone number must be between 8 and 20 characters")]
    //[RegularExpression(@"^\+?[\d\s-()]+$", ErrorMessage = "Phone number can only contain numbers, spaces, hyphens, and parentheses")]
    public string? PhoneNumber { get; set; } = string.Empty;

    //[Required(ErrorMessage = "Country is required")]
    //[StringLength(56, MinimumLength = 2, ErrorMessage = "Country name must be between 2 and 56 characters")]
    //[RegularExpression(@"^[a-zA-ZÀ-ÿ\s-]*$", ErrorMessage = "Country can only contain letters, spaces, and hyphens")]
    public string? Country { get; set; } = string.Empty;
}

public class UserLoginDto{
    public string Email {get; set;} = string.Empty; 
    public string Password {get; set;} = string.Empty;
}


public class UserContactInfoDto{
    public string Email {get; set;} = string.Empty; 
    public string PhoneNumber { get; set; } = string.Empty;
}