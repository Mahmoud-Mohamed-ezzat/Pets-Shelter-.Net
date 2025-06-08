using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Animal2.Models;

public partial class AnimalsContext : IdentityDbContext<Customer>
{
    public AnimalsContext(DbContextOptions<AnimalsContext> options)
        : base(options)
    {
    }
    public virtual DbSet<Animal> Animals { get; set; }
    public virtual DbSet<AnimalCategory> AnimalCategories { get; set; }
    public virtual DbSet<Request> Requests { get; set; }
    public virtual DbSet<Message> Messages { get; set; }
    public virtual DbSet<UserCategory> UserCategories { get; set; }
    public virtual DbSet<Breed> Breeds { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        // seed roles of Roles in my App

        List<IdentityRole> roles = new List<IdentityRole>
            {
                new IdentityRole
                {
                    Id = "1",
                    Name = "Admin",
                    NormalizedName = "ADMIN"
                },
                new IdentityRole
                {
                    Id="2",
                    Name = "Adopter",
                    NormalizedName = "ADOPTER"
                },
                new IdentityRole
                {
                    Id="3",
                    Name="Shelterstaff",
                    NormalizedName="SHELTERSTAFF"
                },
            };
        modelBuilder.Entity<IdentityRole>().HasData(roles);


        modelBuilder.Entity<Customer>().ToTable("Customers");

        // AnimalCategory relationships
        modelBuilder.Entity<AnimalCategory>(entity =>
        {
            entity.HasMany(ac => ac.Animal)
                  .WithOne(a => a.AnimalCategory)
                  .HasForeignKey(a => a.AnimalCategoryId)
                  .OnDelete(DeleteBehavior.Cascade);

            entity.HasMany(ac => ac.Breed)
                  .WithOne(b => b.AnimalCategory)
                  .HasForeignKey(b => b.AnimalCategoryId)
                  .OnDelete(DeleteBehavior.ClientCascade);
        });

        // Animal relationships
        modelBuilder.Entity<Animal>(entity =>
        {
            entity.HasOne(a => a.AnimalCategory)
                  .WithMany(ac => ac.Animal)
                  .HasForeignKey(a => a.AnimalCategoryId)
                  .OnDelete(DeleteBehavior.Cascade);

            entity.HasOne(a => a.ShelterStaff)
                  .WithMany(c => c.AnimalsAsShelterStaff)
                  .HasForeignKey(a => a.ShelterStaffId)
                  .OnDelete(DeleteBehavior.ClientCascade)
                  .IsRequired();

            entity.HasOne(a => a.Adopter)
                  .WithMany(c => c.AnimalsAsAdopter)
                  .HasForeignKey(a => a.AdopterId)
                  .OnDelete(DeleteBehavior.SetNull);

            entity.HasOne(a => a.Breed)
                  .WithMany(b => b.Animals)
                  .HasForeignKey(a => a.BreedId)
                  .OnDelete(DeleteBehavior.SetNull);
            entity.HasIndex(a => a.AnimalCategoryId);
            entity.HasIndex(a => a.ShelterStaffId);
            entity.HasIndex(a => a.AdopterId);
            entity.HasIndex(a => a.BreedId);
        });

        // Breed relationships
        modelBuilder.Entity<Breed>(entity =>
        {


            entity.HasMany(b => b.Animals)
                  .WithOne(a => a.Breed)
                  .HasForeignKey(a => a.BreedId)
                  .OnDelete(DeleteBehavior.SetNull);
        });

        // Customer relationships
        modelBuilder.Entity<Customer>(entity =>
        {
            entity.HasOne(c => c.UserCategory)
                  .WithMany(uc => uc.Customer)
                  .HasForeignKey(c => c.UserCategoryId)
                  .OnDelete(DeleteBehavior.Restrict);

            entity.HasMany(c => c.AnimalsAsShelterStaff)
                  .WithOne(a => a.ShelterStaff)
                  .HasForeignKey(a => a.ShelterStaffId)
                  .OnDelete(DeleteBehavior.ClientCascade);

            entity.HasMany(c => c.AnimalsAsAdopter)
                  .WithOne(a => a.Adopter)
                  .HasForeignKey(a => a.AdopterId)
                  .OnDelete(DeleteBehavior.SetNull);

            entity.HasMany(c => c.Requests)
                  .WithOne(r => r.Adopter)
                  .HasForeignKey(r => r.AdopterId)
                  .OnDelete(DeleteBehavior.Cascade);
        });

        // Request relationships
        modelBuilder.Entity<Request>(entity =>
        {

            entity.HasOne(r => r.Animal)
                  .WithMany(a => a.Requests)
                  .HasForeignKey(r => r.AnimalId)
                  .OnDelete(DeleteBehavior.Cascade);

            entity.HasOne(r => r.Adopter)
                  .WithMany(c => c.Requests)
                  .HasForeignKey(r => r.AdopterId)
                  .OnDelete(DeleteBehavior.Cascade);
            entity.HasIndex(r => r.AnimalId);
            entity.HasIndex(r => r.AdopterId);
        });
        modelBuilder.Entity<Message>(entity =>
        {
            entity.HasOne(a => a.Sender)
              .WithMany(c => c.SenderMessages)
              .HasForeignKey(a => a.SenderId)
              .OnDelete(DeleteBehavior.ClientSetNull);

            entity.HasOne(a => a.Receiver)
                 .WithMany(c => c.ReceiverMessages)
                 .HasForeignKey(a => a.ReceiverId)
                 .OnDelete(DeleteBehavior.ClientSetNull);
        });
    }


}
