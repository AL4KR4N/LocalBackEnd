using monchotradebackend.models.entities;
using Microsoft.EntityFrameworkCore;
using monchotradebackend.Interface;

namespace monchotradebackend.data;

public class MonchoDbContext: DbContext{


    private readonly IPasswordHashingService _passwordHashingService;
    private readonly ILogger<MonchoDbContext> _logger;

    public MonchoDbContext(DbContextOptions<MonchoDbContext> options,IPasswordHashingService passwordHashingService, ILogger<MonchoDbContext> logger) : base(options){
        _passwordHashingService = passwordHashingService ?? throw new ArgumentNullException(nameof(passwordHashingService));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
  
    }

    public DbSet<User> Users {get;set;}
    public DbSet<ProfileImage> ProfileImages {get;set;}
    public DbSet<Product> Products{get; set;}
    public DbSet<ProductImage> ProductImages {get; set;}
    public DbSet<Category> Categories{get;set;}
    public DbSet<Exchange> Exchanges { get; set; }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {


        // Configuración para Exchange
        modelBuilder.Entity<Exchange>(entity =>
        {
            // Relaciones
            entity.HasOne(e => e.InitiatorUser)
                .WithMany()
                .HasForeignKey(e => e.InitiatorUserId)
                .OnDelete(DeleteBehavior.Restrict);

            entity.HasOne(e => e.ReceiverUser)
                .WithMany()
                .HasForeignKey(e => e.ReceiverUserId)
                .OnDelete(DeleteBehavior.Restrict);

            entity.HasOne(e => e.InitiatorProduct)
                .WithMany()
                .HasForeignKey(e => e.InitiatorProductId)
                .OnDelete(DeleteBehavior.Restrict);

            entity.HasOne(e => e.ReceiverProduct)
                .WithMany()
                .HasForeignKey(e => e.ReceiverProductId)
                .OnDelete(DeleteBehavior.Restrict);

            // Validaciones
            entity.Property(e => e.Status)
                .IsRequired()
                .HasConversion<string>();

            entity.Property(e => e.RejectionReason)
                .HasMaxLength(500);

            entity.Property(e => e.Notes)
                .HasMaxLength(1000);

            // Índices
            entity.HasIndex(e => e.Status);
            entity.HasIndex(e => e.CreatedAt);
            entity.HasIndex(e => new { e.InitiatorUserId, e.Status });
            entity.HasIndex(e => new { e.ReceiverUserId, e.Status });
        });

        
        
        modelBuilder.Entity<User>()
        .HasMany(u => u.InitiatedExchanges)
        .WithOne(e => e.InitiatorUser)
        .HasForeignKey(e => e.InitiatorUserId)
        .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<User>()
        .HasMany(u => u.ReceivedExchanges)
        .WithOne(e => e.ReceiverUser)
        .HasForeignKey(e => e.ReceiverUserId)
        .OnDelete(DeleteBehavior.Restrict);


        // Relación User - Product (Un usuario puede tener muchos productos)
        modelBuilder.Entity<User>()
            .HasMany(u => u.Products)
            .WithOne(p => p.User)
            .HasForeignKey(p => p.UserId)
            .OnDelete(DeleteBehavior.Restrict);

        // Relación User - ProfileImage (Un usuario tiene una imagen de perfil)
        modelBuilder.Entity<User>()
            .HasOne(u => u.ProfileImage)
            .WithOne(pi => pi.User)
            .HasForeignKey<ProfileImage>(pi => pi.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        // Relación Product - ProductImage (Un producto puede tener muchas imágenes)
        modelBuilder.Entity<Product>()
            .HasMany(p => p.Images)
            .WithOne(pi => pi.Product)
            .HasForeignKey(pi => pi.ProductId)
            .OnDelete(DeleteBehavior.Cascade);

        // Relación Category - Product 
        modelBuilder.Entity<Category>()
            .HasMany(c => c.Products)
            .WithOne(p => p.ProductCategory)
            .HasForeignKey(p => p.CategoryId)
            .OnDelete(DeleteBehavior.Restrict);

        // Configuraciones adicionales 
        modelBuilder.Entity<Category>()
            .Property(c => c.Name)
            .IsRequired()
            .HasMaxLength(100);

        modelBuilder.Entity<Category>()
            .HasIndex(c => c.Name);

        //validaciones para otras propiedades
        modelBuilder.Entity<User>()
            .Property(u => u.Email)
            .IsRequired()
            .HasMaxLength(100);

        modelBuilder.Entity<User>()
            .HasIndex(u => u.Email)
            .IsUnique();

        modelBuilder.Entity<Product>()
            .Property(p => p.Name)
            .IsRequired()
            .HasMaxLength(100);

        modelBuilder.Entity<Product>()
            .Property(p => p.Description)
            .HasMaxLength(500);
        // Para búsquedas de productos por nombre
        modelBuilder.Entity<Product>()
            .HasIndex(p => p.Name);

        // Para filtrar productos activos
        modelBuilder.Entity<Product>()
            .HasIndex(p => p.IsActive);

        modelBuilder.Entity<Product>()
            .Property(p => p.Quantity)
            .HasDefaultValue(0);

        modelBuilder.Entity<User>()
            .Property(u => u.PhoneNumber)
            .HasMaxLength(20);

        base.OnModelCreating(modelBuilder);

        
    
        //Hago seed de 20 usuario, todos mismo password
        modelBuilder.Entity<User>().HasData(
        new User { Id = 1, Name = "Ana Luisa", LastName = "Martinez", SecondLastName = "Gomez", BirthDate = new DateOnly(1990, 5, 15), Email = "ana.martinezgomez@example.com", Country = "Mexico", PhoneNumber = "+52 55 1234 5678", PasswordHash = _passwordHashingService.HashPassword("Mexicali#11") },
        new User { Id = 2, Name = "Carlos", LastName = "Gomez", SecondLastName = "Lopez", BirthDate = new DateOnly(1985, 8, 22), Email = "carlos.gomezlopez@example.com", Country = "Mexico", PhoneNumber = "+52 33 9876 5432", PasswordHash = _passwordHashingService.HashPassword("Mexicali#11") },
        new User { Id = 3, Name = "Miguel", LastName = "Torres", SecondLastName = "Ramirez", BirthDate = new DateOnly(1992, 3, 10), Email = "miguel.torresramirez@example.com", Country = "Mexico", PhoneNumber = "+52 81 5555 4444", PasswordHash = _passwordHashingService.HashPassword("Mexicali#11") },
        new User { Id = 4, Name = "Laura", LastName = "Rodriguez", SecondLastName = "Fernandez", BirthDate = new DateOnly(1988, 11, 5), Email = "laura.rodriguezfernandez@example.com", Country = "Mexico", PhoneNumber = "+52 55 2222 3333", PasswordHash = _passwordHashingService.HashPassword("Mexicali#11") },
        new User { Id = 5, Name = "Juan Carlos", LastName = "Sanchez", SecondLastName = "Martinez", BirthDate = new DateOnly(1995, 7, 18), Email = "juan.sanchezmartinez@example.com", Country = "Mexico", PhoneNumber = "+52 33 7777 8888", PasswordHash = _passwordHashingService.HashPassword("Mexicali#11") },
        new User { Id = 6, Name = "Sofia", LastName = "Ramirez", SecondLastName = "Castillo", BirthDate = new DateOnly(1987, 9, 30), Email = "sofia.ramirezcastillo@example.com", Country = "Mexico", PhoneNumber = "+52 81 4444 5555", PasswordHash = _passwordHashingService.HashPassword("Mexicali#11") },
        new User { Id = 7, Name = "Andres", LastName = "Lopez", SecondLastName = "Castro", BirthDate = new DateOnly(1993, 2, 14), Email = "andres.lopezcastro@example.com", Country = "Mexico", PhoneNumber = "+52 55 6666 7777", PasswordHash = _passwordHashingService.HashPassword("Mexicali#11") },
        new User { Id = 8, Name = "Valentina", LastName = "Castro", SecondLastName = "Perez", BirthDate = new DateOnly(1991, 6, 25), Email = "valentina.castroperez@example.com", Country = "Mexico", PhoneNumber = "+52 33 1111 2222", PasswordHash = _passwordHashingService.HashPassword("Mexicali#11") },
        new User { Id = 9, Name = "Pedro", LastName = "Jimenez", SecondLastName = "Morales", BirthDate = new DateOnly(1986, 12, 7), Email = "pedro.jimenezmorales@example.com", Country = "Mexico", PhoneNumber = "+52 81 9999 0000", PasswordHash = _passwordHashingService.HashPassword("Mexicali#11") },
        new User { Id = 10, Name = "Mariana", LastName = "Diaz", SecondLastName = "Herrera", BirthDate = new DateOnly(1994, 4, 12), Email = "mariana.diazherrera@example.com", Country = "Mexico", PhoneNumber = "+52 55 3333 4444", PasswordHash = _passwordHashingService.HashPassword("Mexicali#11") },
        new User { Id = 11, Name = "Javier", LastName = "Morales", SecondLastName = "Jimenez", BirthDate = new DateOnly(1989, 10, 20), Email = "javier.moralesjimenez@example.com", Country = "Mexico", PhoneNumber = "+52 33 5555 6666", PasswordHash = _passwordHashingService.HashPassword("Mexicali#11") },
        new User { Id = 12, Name = "Gabriela", LastName = "Fernandez", SecondLastName = "Torres", BirthDate = new DateOnly(1996, 1, 8), Email = "gabriela.fernandeztorres@example.com", Country = "Mexico", PhoneNumber = "+52 81 2222 3333", PasswordHash = _passwordHashingService.HashPassword("Mexicali#11") },
        new User { Id = 13, Name = "Tomas", LastName = "Herrera", SecondLastName = "Mendoza", BirthDate = new DateOnly(1984, 8, 16), Email = "tomas.herreramendoza@example.com", Country = "Mexico", PhoneNumber = "+52 55 7777 8888", PasswordHash = _passwordHashingService.HashPassword("Mexicali#11") },
        new User { Id = 14, Name = "Natalia", LastName = "Mendoza", SecondLastName = "Rodriguez", BirthDate = new DateOnly(1997, 5, 3), Email = "natalia.mendozarodriguez@example.com", Country = "Mexico", PhoneNumber = "+52 33 4444 5555", PasswordHash = _passwordHashingService.HashPassword("Mexicali#11") },
        new User { Id = 15, Name = "Luis Fernando", LastName = "Ruiz", SecondLastName = "Sanchez", BirthDate = new DateOnly(1983, 11, 22), Email = "luis.ruizsanchez@example.com", Country = "Mexico", PhoneNumber = "+52 81 6666 7777", PasswordHash = _passwordHashingService.HashPassword("Mexicali#11") },
        new User { Id = 16, Name = "Elena", LastName = "Gonzalez", SecondLastName = "Reyes", BirthDate = new DateOnly(1990, 7, 7), Email = "elena.gonzalezreyes@example.com", Country = "Mexico", PhoneNumber = "+52 55 8888 9999", PasswordHash = _passwordHashingService.HashPassword("Mexicali#11") },
        new User { Id = 17, Name = "Ricardo", LastName = "Reyes", SecondLastName = "Garcia", BirthDate = new DateOnly(1986, 3, 19), Email = "ricardo.reyesgarcia@example.com", Country = "Mexico", PhoneNumber = "+52 33 0000 1111", PasswordHash = _passwordHashingService.HashPassword("Mexicali#11") },
        new User { Id = 18, Name = "Isabel", LastName = "Garcia", SecondLastName = "Moreno", BirthDate = new DateOnly(1993, 9, 14), Email = "isabel.garciamoreno@example.com", Country = "Mexico", PhoneNumber = "+52 81 3333 4444", PasswordHash = _passwordHashingService.HashPassword("Mexicali#11") },
        new User { Id = 19, Name = "Daniel", LastName = "Moreno", SecondLastName = "Silva", BirthDate = new DateOnly(1988, 6, 30), Email = "daniel.morenosilva@example.com", Country = "Mexico", PhoneNumber = "+52 55 5555 6666", PasswordHash = _passwordHashingService.HashPassword("Mexicali#11") },
        new User { Id = 20, Name = "Carmen", LastName = "Silva", SecondLastName = "Navarro", BirthDate = new DateOnly(1995, 12, 5), Email = "carmen.silvanavarro@example.com", Country = "Mexico", PhoneNumber = "+52 33 2222 3333", PasswordHash = _passwordHashingService.HashPassword("Mexicali#11") }
        );

       modelBuilder.Entity<Product>().HasData(
            // Clothing (CategoryId = 1)
            new Product { Id = 1, UserId = 1, Name = "Men's Jeans", Description = "Classic blue jeans for everyday wear", Quantity = 10, CategoryId = 1, CreatedAt = DateTime.Now, IsActive = true },
            new Product { Id = 2, UserId = 1, Name = "Casual T-Shirt", Description = "Comfortable cotton t-shirt, perfect for casual outings", Quantity = 5, CategoryId = 1, CreatedAt = DateTime.Now, IsActive = true },
            new Product { Id = 9, UserId = 5, Name = "Formal Shirt", Description = "Stylish white formal shirt, perfect for office", Quantity = 11, CategoryId = 1, CreatedAt = DateTime.Now, IsActive = true },
            new Product { Id = 10, UserId = 5, Name = "Dress Pants", Description = "Comfortable black dress pants for formal events", Quantity = 6, CategoryId = 1, CreatedAt = DateTime.Now, IsActive = true },
            new Product { Id = 15, UserId = 8, Name = "Men's Jacket", Description = "Stylish and warm winter jacket", Quantity = 4, CategoryId = 1, CreatedAt = DateTime.Now, IsActive = true },
            new Product { Id = 16, UserId = 8, Name = "Wool Scarf", Description = "Soft wool scarf for cold weather", Quantity = 7, CategoryId = 1, CreatedAt = DateTime.Now, IsActive = true },
            new Product { Id = 33, UserId = 17, Name = "Cotton Hoodie", Description = "Comfortable cotton hoodie", Quantity = 8, CategoryId = 1, CreatedAt = DateTime.Now, IsActive = true },
            new Product { Id = 34, UserId = 17, Name = "Sports Cap", Description = "Adjustable sports cap", Quantity = 15, CategoryId = 1, CreatedAt = DateTime.Now, IsActive = true },
            new Product { Id = 39, UserId = 20, Name = "Pants", Description = "Comfortable everyday pants", Quantity = 6, CategoryId = 1, CreatedAt = DateTime.Now, IsActive = true },
            new Product { Id = 40, UserId = 20, Name = "Sweater", Description = "Warm sweater for winter", Quantity = 15, CategoryId = 1, CreatedAt = DateTime.Now, IsActive = true },

            // Accessories (CategoryId = 2)
            new Product { Id = 7, UserId = 4, Name = "Smartphone Charger", Description = "Fast-charging USB-C charger", Quantity = 15, CategoryId = 2, CreatedAt = DateTime.Now, IsActive = true },
            new Product { Id = 8, UserId = 4, Name = "Power Bank", Description = "10,000mAh power bank for mobile devices", Quantity = 9, CategoryId = 2, CreatedAt = DateTime.Now, IsActive = true },
            new Product { Id = 13, UserId = 7, Name = "Digital Watch", Description = "Sporty digital watch with water resistance", Quantity = 14, CategoryId = 2, CreatedAt = DateTime.Now, IsActive = true },
            new Product { Id = 14, UserId = 7, Name = "Fitness Tracker", Description = "Tracks steps, calories, and more", Quantity = 5, CategoryId = 2, CreatedAt = DateTime.Now, IsActive = true },
            new Product { Id = 31, UserId = 16, Name = "Sunglasses", Description = "Polarized sunglasses with UV protection", Quantity = 10, CategoryId = 2, CreatedAt = DateTime.Now, IsActive = true },
            new Product { Id = 32, UserId = 16, Name = "Leather Wallet", Description = "Classic leather wallet with multiple compartments", Quantity = 9, CategoryId = 2, CreatedAt = DateTime.Now, IsActive = true },

            // Electronics (CategoryId = 3)
            new Product { Id = 3, UserId = 2, Name = "Wireless Headphones", Description = "Noise-cancelling headphones with high-quality sound", Quantity = 8, CategoryId = 3, CreatedAt = DateTime.Now, IsActive = true },
            new Product { Id = 4, UserId = 2, Name = "Bluetooth Speaker", Description = "Portable speaker with long battery life", Quantity = 12, CategoryId = 3, CreatedAt = DateTime.Now, IsActive = true },
            new Product { Id = 17, UserId = 9, Name = "Tablet", Description = "10-inch tablet with high-resolution display", Quantity = 6, CategoryId = 3, CreatedAt = DateTime.Now, IsActive = true },
            new Product { Id = 18, UserId = 9, Name = "Tablet Case", Description = "Protective case for 10-inch tablet", Quantity = 10, CategoryId = 3, CreatedAt = DateTime.Now, IsActive = true },
            new Product { Id = 25, UserId = 13, Name = "Gaming Mouse", Description = "High-precision gaming mouse", Quantity = 6, CategoryId = 3, CreatedAt = DateTime.Now, IsActive = true },
            new Product { Id = 26, UserId = 13, Name = "Mechanical Keyboard", Description = "Tactile mechanical keyboard", Quantity = 5, CategoryId = 3, CreatedAt = DateTime.Now, IsActive = true },
            new Product { Id = 29, UserId = 15, Name = "Smart Thermostat", Description = "Programmable smart thermostat", Quantity = 5, CategoryId = 3, CreatedAt = DateTime.Now, IsActive = true },
            new Product { Id = 30, UserId = 15, Name = "Air Purifier", Description = "Compact air purifier for small rooms", Quantity = 3, CategoryId = 3, CreatedAt = DateTime.Now, IsActive = true },
            new Product { Id = 35, UserId = 18, Name = "Portable Charger", Description = "Compact charger for mobile devices", Quantity = 14, CategoryId = 3, CreatedAt = DateTime.Now, IsActive = true },
            new Product { Id = 36, UserId = 18, Name = "USB Cable Set", Description = "Set of 3 USB cables", Quantity = 12, CategoryId = 3, CreatedAt = DateTime.Now, IsActive = true },

            // Kitchen (CategoryId = 4)
            new Product { Id = 37, UserId = 19, Name = "Cookware Set", Description = "Non-stick cookware set", Quantity = 6, CategoryId = 4, CreatedAt = DateTime.Now, IsActive = true },
            new Product { Id = 38, UserId = 19, Name = "Knife Set", Description = "Stainless steel knives for every need", Quantity = 7, CategoryId = 4, CreatedAt = DateTime.Now, IsActive = true },

            // Home Decor (CategoryId = 5)
            new Product { Id = 23, UserId = 12, Name = "Ceramic Vase", Description = "Elegant ceramic vase for home decor", Quantity = 7, CategoryId = 5, CreatedAt = DateTime.Now, IsActive = true },
            new Product { Id = 24, UserId = 12, Name = "Decorative Pillow", Description = "Soft pillow with a decorative cover", Quantity = 10, CategoryId = 5, CreatedAt = DateTime.Now, IsActive = true },
            new Product { Id = 27, UserId = 14, Name = "Wall Art", Description = "Modern abstract wall art", Quantity = 4, CategoryId = 5, CreatedAt = DateTime.Now, IsActive = true },
            new Product { Id = 28, UserId = 14, Name = "Table Lamp", Description = "Stylish table lamp with adjustable brightness", Quantity = 8, CategoryId = 5, CreatedAt = DateTime.Now, IsActive = true },

            // Footwear (CategoryId = 6)
            new Product { Id = 5, UserId = 3, Name = "Sports Sneakers", Description = "Comfortable and durable sports shoes", Quantity = 7, CategoryId = 6, CreatedAt = DateTime.Now, IsActive = true },
            new Product { Id = 6, UserId = 3, Name = "Running Shoes", Description = "Lightweight running shoes for everyday exercise", Quantity = 10, CategoryId = 6, CreatedAt = DateTime.Now, IsActive = true },

            // Office Supplies (CategoryId = 7)
            new Product { Id = 11, UserId = 6, Name = "Laptop Stand", Description = "Ergonomic stand for laptops", Quantity = 8, CategoryId = 7, CreatedAt = DateTime.Now, IsActive = true },
            new Product { Id = 12, UserId = 6, Name = "Wireless Mouse", Description = "Compact wireless mouse for convenience", Quantity = 13, CategoryId = 7, CreatedAt = DateTime.Now, IsActive = true },
            new Product { Id = 21, UserId = 11, Name = "Desk Organizer", Description = "Keeps your desk neat and organized", Quantity = 8, CategoryId = 7, CreatedAt = DateTime.Now, IsActive = true },
            new Product { Id = 22, UserId = 11, Name = "Notepad Set", Description = "Set of three notepads", Quantity = 15, CategoryId = 7, CreatedAt = DateTime.Now, IsActive = true },

            // Fitness (CategoryId = 8)
            new Product { Id = 19, UserId = 10, Name = "Yoga Mat", Description = "Non-slip yoga mat for workouts", Quantity = 12, CategoryId = 8, CreatedAt = DateTime.Now, IsActive = true },
            new Product { Id = 20, UserId = 10, Name = "Dumbbells Set", Description = "Adjustable dumbbells for home workouts", Quantity = 3, CategoryId = 8, CreatedAt = DateTime.Now, IsActive = true }
        );

        modelBuilder.Entity<Category>().HasData(
            new Category{Id = 1, Name ="Clothing"},
            new Category{Id = 2, Name ="Accessories"},
            new Category{Id = 3, Name ="Electronics"},
            new Category{Id = 4, Name ="Kitchen"},
            new Category{Id = 5, Name ="Home Decor"},
            new Category{Id = 6, Name ="Footwear"},
            new Category{Id = 7, Name ="Office Supplies"},
            new Category{Id = 8, Name ="Fitness"},
            new Category{Id = 9, Name ="Other"}

        );

        modelBuilder.Entity<ProductImage>().HasData(
            new ProductImage { Id = 3, Url = "2c2abcc8-d9de-4657-b2ea-9cb6317e35c1.jpg", ProductId = 1 },
            new ProductImage { Id = 4, Url = "d8440f61-bf9a-45f8-b18c-e9b7e2d35ba8.jpg", ProductId = 2 },
            new ProductImage { Id = 5, Url = "13268719-b95e-4ffa-af40-83b1c7a7a079.jpg", ProductId = 3 },
            new ProductImage { Id = 6, Url = "6c97ef84-b64c-4f70-8995-71f8889dd311.png", ProductId = 4 }
        );

        modelBuilder.Entity<ProfileImage>().HasData(
            new ProfileImage{Id = 1, Url = "64d3f2dd-200a-49d4-b1ac-cb3541ba22f0.jpg", UserId = 1},
            new ProfileImage{Id = 2, Url = "bae63af5-6084-465d-b4dc-b6549dee41ee.jpg", UserId = 2}
        );

        
            // Datos de ejemplo para Exchange
        modelBuilder.Entity<Exchange>().HasData(
            // User 1 exchanges (ID: 1-2)
            new Exchange { Id = 1, InitiatorUserId = 1, ReceiverUserId = 2, InitiatorProductId = 1, ReceiverProductId = 3, CreatedAt = DateTime.Now, Status = ExchangeStatus.Pending, Notes = "Jeans for headphones" },
            new Exchange { Id = 2, InitiatorUserId = 1, ReceiverUserId = 3, InitiatorProductId = 2, ReceiverProductId = 5, CreatedAt = DateTime.Now, Status = ExchangeStatus.Accepted, Notes = "T-Shirt for sneakers" },

            // User 2 exchanges (ID: 3-4)
            new Exchange { Id = 3, InitiatorUserId = 2, ReceiverUserId = 4, InitiatorProductId = 3, ReceiverProductId = 7, CreatedAt = DateTime.Now, Status = ExchangeStatus.Pending, Notes = "Headphones for charger" },
            new Exchange { Id = 4, InitiatorUserId = 2, ReceiverUserId = 5, InitiatorProductId = 4, ReceiverProductId = 9, CreatedAt = DateTime.Now, Status = ExchangeStatus.Rejected, RejectionReason = "Item condition not as expected", Notes = "Speaker for shirt" },

            // User 3 exchanges (ID: 5-6)
            new Exchange { Id = 5, InitiatorUserId = 3, ReceiverUserId = 6, InitiatorProductId = 5, ReceiverProductId = 11, CreatedAt = DateTime.Now, Status = ExchangeStatus.Accepted, Notes = "Sneakers for laptop stand" },
            new Exchange { Id = 6, InitiatorUserId = 3, ReceiverUserId = 7, InitiatorProductId = 6, ReceiverProductId = 13, CreatedAt = DateTime.Now, Status = ExchangeStatus.Pending, Notes = "Running shoes for watch" },

            // User 4 exchanges (ID: 7-8)
            new Exchange { Id = 7, InitiatorUserId = 4, ReceiverUserId = 8, InitiatorProductId = 7, ReceiverProductId = 15, CreatedAt = DateTime.Now, Status = ExchangeStatus.Rejected, RejectionReason = "Found another exchange", Notes = "Charger for jacket" },
            new Exchange { Id = 8, InitiatorUserId = 4, ReceiverUserId = 9, InitiatorProductId = 8, ReceiverProductId = 17, CreatedAt = DateTime.Now, Status = ExchangeStatus.Pending, Notes = "Power bank for tablet" },

            // User 5 exchanges (ID: 9-10)
            new Exchange { Id = 9, InitiatorUserId = 5, ReceiverUserId = 10, InitiatorProductId = 9, ReceiverProductId = 19, CreatedAt = DateTime.Now, Status = ExchangeStatus.Accepted, Notes = "Formal shirt for yoga mat" },
            new Exchange { Id = 10, InitiatorUserId = 5, ReceiverUserId = 11, InitiatorProductId = 10, ReceiverProductId = 21, CreatedAt = DateTime.Now, Status = ExchangeStatus.Pending, Notes = "Dress pants for desk organizer" },

            // User 6 exchanges (ID: 11-12)
            new Exchange { Id = 11, InitiatorUserId = 6, ReceiverUserId = 12, InitiatorProductId = 11, ReceiverProductId = 23, CreatedAt = DateTime.Now, Status = ExchangeStatus.Rejected, RejectionReason = "Size doesn't match description", Notes = "Laptop stand for vase" },
            new Exchange { Id = 12, InitiatorUserId = 6, ReceiverUserId = 13, InitiatorProductId = 12, ReceiverProductId = 25, CreatedAt = DateTime.Now, Status = ExchangeStatus.Pending, Notes = "Mouse for gaming mouse" },

            // User 7 exchanges (ID: 13-14)
            new Exchange { Id = 13, InitiatorUserId = 7, ReceiverUserId = 14, InitiatorProductId = 13, ReceiverProductId = 27, CreatedAt = DateTime.Now, Status = ExchangeStatus.Accepted, Notes = "Digital watch for wall art" },
            new Exchange { Id = 14, InitiatorUserId = 7, ReceiverUserId = 15, InitiatorProductId = 14, ReceiverProductId = 29, CreatedAt = DateTime.Now, Status = ExchangeStatus.Pending, Notes = "Fitness tracker for thermostat" },

            // User 8 exchanges (ID: 15-16)
            new Exchange { Id = 15, InitiatorUserId = 8, ReceiverUserId = 16, InitiatorProductId = 15, ReceiverProductId = 31, CreatedAt = DateTime.Now, Status = ExchangeStatus.Rejected, RejectionReason = "Item no longer available", Notes = "Jacket for sunglasses" },
            new Exchange { Id = 16, InitiatorUserId = 8, ReceiverUserId = 17, InitiatorProductId = 16, ReceiverProductId = 33, CreatedAt = DateTime.Now, Status = ExchangeStatus.Pending, Notes = "Scarf for hoodie" },

            // User 9 exchanges (ID: 17-18)
            new Exchange { Id = 17, InitiatorUserId = 9, ReceiverUserId = 18, InitiatorProductId = 17, ReceiverProductId = 35, CreatedAt = DateTime.Now, Status = ExchangeStatus.Accepted, Notes = "Tablet for portable charger" },
            new Exchange { Id = 18, InitiatorUserId = 9, ReceiverUserId = 19, InitiatorProductId = 18, ReceiverProductId = 37, CreatedAt = DateTime.Now, Status = ExchangeStatus.Pending, Notes = "Tablet case for cookware" },

            // User 10 exchanges (ID: 19-20)
            new Exchange { Id = 19, InitiatorUserId = 10, ReceiverUserId = 20, InitiatorProductId = 19, ReceiverProductId = 39, CreatedAt = DateTime.Now, Status = ExchangeStatus.Rejected, RejectionReason = "Changed my mind", Notes = "Yoga mat for pants" },
            new Exchange { Id = 20, InitiatorUserId = 10, ReceiverUserId = 1, InitiatorProductId = 20, ReceiverProductId = 1, CreatedAt = DateTime.Now, Status = ExchangeStatus.Pending, Notes = "Dumbbells for jeans" },

            // User 11 exchanges (ID: 21-22)
            new Exchange { Id = 21, InitiatorUserId = 11, ReceiverUserId = 2, InitiatorProductId = 21, ReceiverProductId = 3, CreatedAt = DateTime.Now, Status = ExchangeStatus.Pending, Notes = "Desk organizer for headphones" },
            new Exchange { Id = 22, InitiatorUserId = 11, ReceiverUserId = 3, InitiatorProductId = 22, ReceiverProductId = 5, CreatedAt = DateTime.Now, Status = ExchangeStatus.Accepted, Notes = "Notepad for sneakers" },

            // User 12 exchanges (ID: 23-24)
            new Exchange { Id = 23, InitiatorUserId = 12, ReceiverUserId = 4, InitiatorProductId = 23, ReceiverProductId = 7, CreatedAt = DateTime.Now, Status = ExchangeStatus.Rejected, RejectionReason = "Distance too far for exchange", Notes = "Vase for charger" },
            new Exchange { Id = 24, InitiatorUserId = 12, ReceiverUserId = 5, InitiatorProductId = 24, ReceiverProductId = 9, CreatedAt = DateTime.Now, Status = ExchangeStatus.Pending, Notes = "Pillow for shirt" },

            // User 13 exchanges (ID: 25-26)
            new Exchange { Id = 25, InitiatorUserId = 13, ReceiverUserId = 6, InitiatorProductId = 25, ReceiverProductId = 11, CreatedAt = DateTime.Now, Status = ExchangeStatus.Accepted, Notes = "Gaming mouse for laptop stand" },
            new Exchange { Id = 26, InitiatorUserId = 13, ReceiverUserId = 7, InitiatorProductId = 26, ReceiverProductId = 13, CreatedAt = DateTime.Now, Status = ExchangeStatus.Pending, Notes = "Keyboard for watch" },

            // User 14 exchanges (ID: 27-28)
            new Exchange { Id = 27, InitiatorUserId = 14, ReceiverUserId = 8, InitiatorProductId = 27, ReceiverProductId = 15, CreatedAt = DateTime.Now, Status = ExchangeStatus.Rejected, RejectionReason = "Item value mismatch", Notes = "Wall art for jacket" },
            new Exchange { Id = 28, InitiatorUserId = 14, ReceiverUserId = 9, InitiatorProductId = 28, ReceiverProductId = 17, CreatedAt = DateTime.Now, Status = ExchangeStatus.Pending, Notes = "Lamp for tablet" },

            // User 15 exchanges (ID: 29-30)
            new Exchange { Id = 29, InitiatorUserId = 15, ReceiverUserId = 10, InitiatorProductId = 29, ReceiverProductId = 19, CreatedAt = DateTime.Now, Status = ExchangeStatus.Accepted, Notes = "Thermostat for yoga mat" },
            new Exchange { Id = 30, InitiatorUserId = 15, ReceiverUserId = 11, InitiatorProductId = 30, ReceiverProductId = 21, CreatedAt = DateTime.Now, Status = ExchangeStatus.Pending, Notes = "Air purifier for organizer" },

            // User 16 exchanges (ID: 31-32)
            new Exchange { Id = 31, InitiatorUserId = 16, ReceiverUserId = 12, InitiatorProductId = 31, ReceiverProductId = 23, CreatedAt = DateTime.Now, Status = ExchangeStatus.Rejected, RejectionReason = "Communication issues", Notes = "Sunglasses for vase" },
            new Exchange { Id = 32, InitiatorUserId = 16, ReceiverUserId = 13, InitiatorProductId = 32, ReceiverProductId = 25, CreatedAt = DateTime.Now, Status = ExchangeStatus.Pending, Notes = "Wallet for gaming mouse" },

            // User 17 exchanges (ID: 33-34)
            new Exchange { Id = 33, InitiatorUserId = 17, ReceiverUserId = 14, InitiatorProductId = 33, ReceiverProductId = 27, CreatedAt = DateTime.Now, Status = ExchangeStatus.Accepted, Notes = "Hoodie for wall art" },
            new Exchange { Id = 34, InitiatorUserId = 17, ReceiverUserId = 15, InitiatorProductId = 34, ReceiverProductId = 29, CreatedAt = DateTime.Now, Status = ExchangeStatus.Pending, Notes = "Cap for thermostat" },

            // User 18 exchanges (ID: 35-36)
            new Exchange { Id = 35, InitiatorUserId = 18, ReceiverUserId = 16, InitiatorProductId = 35, ReceiverProductId = 31, CreatedAt = DateTime.Now, Status = ExchangeStatus.Rejected, RejectionReason = "Schedule conflicts for meetup", Notes = "Portable charger for sunglasses" },
            new Exchange { Id = 36, InitiatorUserId = 18, ReceiverUserId = 17, InitiatorProductId = 36, ReceiverProductId = 33, CreatedAt = DateTime.Now, Status = ExchangeStatus.Pending, Notes = "USB cables for hoodie" },

            // User 19 exchanges (ID: 37-38)
            new Exchange { Id = 37, InitiatorUserId = 19, ReceiverUserId = 18, InitiatorProductId = 37, ReceiverProductId = 35, CreatedAt = DateTime.Now, Status = ExchangeStatus.Accepted, Notes = "Cookware for charger" },
            new Exchange { Id = 38, InitiatorUserId = 19, ReceiverUserId = 1, InitiatorProductId = 38, ReceiverProductId = 1, CreatedAt = DateTime.Now, Status = ExchangeStatus.Pending, Notes = "Knife set for jeans" },

            // User 20 exchanges (ID: 39-40)
            new Exchange { Id = 39, InitiatorUserId = 20, ReceiverUserId = 2, InitiatorProductId = 39, ReceiverProductId = 3, CreatedAt = DateTime.Now, Status = ExchangeStatus.Rejected, RejectionReason = "Product quality concerns", Notes = "Pants for headphones" },
            new Exchange { Id = 40, InitiatorUserId = 20, ReceiverUserId = 3, InitiatorProductId = 40, ReceiverProductId = 5, CreatedAt = DateTime.Now, Status = ExchangeStatus.Pending, Notes = "Sweater for sneakers" }
        );

        //Soluciona un problema con sqlite
        foreach (var entity in modelBuilder.Model.GetEntityTypes())
        {
            entity.SetSchema(null);
        }
    }


}