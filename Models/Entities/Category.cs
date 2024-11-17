namespace monchotradebackend.models.entities; 

public class Category{
    public int Id {get;set;}

    public string Name{get;set;} = string.Empty;
    
    //Navegacion productos
    public virtual ICollection<Product> Products { get; set; } = new List<Product>();

}