using Microsoft.EntityFrameworkCore;
using Midterm_EquipmentRental.Models;

namespace Midterm_EquipmentRental.Data
{
    public class AppDbContext : DbContext
    {

        public DbSet<Customer> Customers { get; set; }
        public DbSet<Equipment> Equipments { get; set; }
        public DbSet<Rental> Rentals { get; set; }

        public AppDbContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Equipment>().HasData(
                new Equipment
                {
                    Id = 1,
                    Name = "Excavator",
                    Description = "Large hydraulic excavator suitable for heavy construction work.",
                    Category = EquipmentCategory.HeavyMachinery,
                    Condition = EquipmentCondition.Good,
                    RentalPrice = 500.0,
                    IsAvailable = true,
                    CreatedAt = new DateTime(2025, 10, 12)
                },
                new Equipment
                {
                    Id = 2,
                    Name = "Bulldozer",
                    Description = "Powerful bulldozer for earthmoving and site preparation.",
                    Category = EquipmentCategory.HeavyMachinery,
                    Condition = EquipmentCondition.Excellent,
                    RentalPrice = 650.0,
                    IsAvailable = true,
                    CreatedAt = new DateTime(2025, 10, 12)
                },
                new Equipment
                {
                    Id = 3,
                    Name = "Electric Drill",
                    Description = "Cordless electric drill with variable speed control.",
                    Category = EquipmentCategory.PowerTools,
                    Condition = EquipmentCondition.New,
                    RentalPrice = 30.0,
                    IsAvailable = true,
                    CreatedAt = new DateTime(2025, 10, 12)
                },
                new Equipment
                {
                    Id = 4,
                    Name = "Circular Saw",
                    Description = "High-speed saw ideal for cutting wood and metal.",
                    Category = EquipmentCategory.PowerTools,
                    Condition = EquipmentCondition.Good,
                    RentalPrice = 25.0,
                    IsAvailable = true,
                    CreatedAt = new DateTime(2025, 10, 12)
                },
                new Equipment
                {
                    Id = 5,
                    Name = "Pickup Truck",
                    Description = "Reliable pickup truck for transporting materials and tools.",
                    Category = EquipmentCategory.Vehicles,
                    Condition = EquipmentCondition.Fair,
                    RentalPrice = 120.0,
                    IsAvailable = false,
                    CreatedAt = new DateTime(2025, 10, 12)
                },
                new Equipment
                {
                    Id = 6,
                    Name = "Forklift",
                    Description = "Compact forklift with 2-ton lifting capacity.",
                    Category = EquipmentCategory.HeavyMachinery,
                    Condition = EquipmentCondition.Good,
                    RentalPrice = 200.0,
                    IsAvailable = true,
                    CreatedAt = new DateTime(2025, 10, 12)
                },
                new Equipment
                {
                    Id = 7,
                    Name = "Safety Helmet",
                    Description = "Durable safety helmet for construction site protection.",
                    Category = EquipmentCategory.Safety,
                    Condition = EquipmentCondition.New,
                    RentalPrice = 5.0,
                    IsAvailable = true,
                    CreatedAt = new DateTime(2025, 10, 12)
                },
                new Equipment
                {
                    Id = 8,
                    Name = "Reflective Vest",
                    Description = "High-visibility safety vest for on-site workers.",
                    Category = EquipmentCategory.Safety,
                    Condition = EquipmentCondition.Excellent,
                    RentalPrice = 3.0,
                    IsAvailable = true,
                    CreatedAt = new DateTime(2025, 10, 12)
                },
                new Equipment
                {
                    Id = 9,
                    Name = "Total Station",
                    Description = "Surveying instrument for precise distance and angle measurement.",
                    Category = EquipmentCategory.Surveying,
                    Condition = EquipmentCondition.Good,
                    RentalPrice = 400.0,
                    IsAvailable = false,
                    CreatedAt = new DateTime(2025, 10, 12)
                },
                new Equipment
                {
                    Id = 10,
                    Name = "Laser Level",
                    Description = "High-accuracy laser level for construction alignment tasks.",
                    Category = EquipmentCategory.Surveying,
                    Condition = EquipmentCondition.Excellent,
                    RentalPrice = 50.0,
                    IsAvailable = true,
                    CreatedAt = new DateTime(2025, 10, 12)
                }
    );
            modelBuilder.Entity<Customer>().HasData(
                    new Customer
                    {
                        Id = 1,
                        Name = "Dany Ko",
                        Email = "dko8957@conestogac.on.ca",
                        UserName = "Dany",
                        Password = "Danypassword",
                        Role = UserRole.Admin
                    },
                    new Customer
                    {
                        Id = 2,
                        Name = "Gabriel Siewert",
                        Email = "Gsiewert2384@conestogac.on.ca",
                        UserName = "Gabe",
                        Password = "Gabepassword",
                        Role = UserRole.User
                    }
                );
            modelBuilder.Entity<Rental>().HasData(
                new Rental
                {
                    Id = 1,
                    EquipmentId = 1,   // Excavator
                    CustomerId = 1,
                    IssuedAt = new DateTime(2025, 10, 1),
                    ReturnedAt = new DateTime(2025, 10, 5),
                    Status = RentalStatus.Returned
                },
                new Rental
                {
                    Id = 2,
                    EquipmentId = 3,   // Electric Drill
                    CustomerId = 2,
                    IssuedAt = new DateTime(2025, 9, 28),
                    ReturnedAt = new DateTime(2025, 10, 2),
                    Status = RentalStatus.Active
                },
                new Rental
                {
                    Id = 3,
                    EquipmentId = 5,   // Pickup Truck
                    CustomerId = 3,
                    IssuedAt = new DateTime(2025, 9, 30),
                    ReturnedAt = new DateTime(2025, 10, 7),
                    Status = RentalStatus.Returned
                },
                new Rental
                {
                    Id = 4,
                    EquipmentId = 7,   // Safety Helmet
                    CustomerId = 4,
                    IssuedAt = new DateTime(2025, 10, 3),
                    ReturnedAt = new DateTime(2025, 10, 3),
                    Status = RentalStatus.Returned
                },
                new Rental
                {
                    Id = 5,
                    EquipmentId = 9,   // Total Station
                    CustomerId = 5,
                    IssuedAt = new DateTime(2025, 9, 25),
                    ReturnedAt = new DateTime(2025, 9, 30),
                    Status = RentalStatus.Overdue
                }
            );
        }

    }
}
