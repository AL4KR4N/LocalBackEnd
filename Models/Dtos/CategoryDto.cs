namespace monchotradebackend.models.dtos; 

public class CategoryDto{
    public int Id {get; set;}
    public string Name{get;set;} = string.Empty; 
}

public class CategoryCreateDto{
    public string Name{get; set;} = string.Empty;
}