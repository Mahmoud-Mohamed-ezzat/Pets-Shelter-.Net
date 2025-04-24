using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Animal2.Models;

public partial class AnimalsContext : IdentityDbContext<Customer>
{
    public AnimalsContext()
    {
    }

    public AnimalsContext(DbContextOptions<AnimalsContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Animal> Animals { get; set; }

    public virtual DbSet<AnimalCategory> AnimalCategories { get; set; }

    public virtual DbSet<Interview> Interviews { get; set; }

    public virtual DbSet<Request> Requests { get; set; }


    public virtual DbSet<UserCategory> UserCategories { get; set; }
    public virtual DbSet<Breed> Breeds { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
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
        //modelBuilder.Entity<IdentityRole>().HasData(roles);
        //var Customer= new Customer { 
        //Id = "1",
        //UserName="admin"
        
        //}

        modelBuilder.Entity<Customer>().ToTable("Customers");

        modelBuilder.Entity<AnimalCategory>(entity =>
        {
            entity.HasMany(ac => ac.Animal)
                  .WithOne(a => a.AnimalCategory)
                  .HasForeignKey(a => a.AnimalCategoryId)
                  .OnDelete(DeleteBehavior.Restrict);

            entity.HasMany(ac => ac.Breed)
                  .WithOne(b => b.AnimalCategory)
                  .HasForeignKey(b => b.AnimalCategoryId)
                  .OnDelete(DeleteBehavior.Restrict);
        });

        modelBuilder.Entity<Animal>(entity =>
        {
            entity.HasOne(a => a.AnimalCategory)
                  .WithMany(ac => ac.Animal)
                  .HasForeignKey(a => a.AnimalCategoryId)
                  .OnDelete(DeleteBehavior.Restrict);

            entity.HasOne(a => a.ShelterStaff)
                  .WithMany(c => c.AnimalsAsShelterStaff)
                  .HasForeignKey(a => a.ShelterStaffId)
                  .OnDelete(DeleteBehavior.Restrict)
                  .IsRequired();

            entity.HasOne(a => a.Adopter)
                  .WithMany(c => c.AnimalsAsAdopter)
                  .HasForeignKey(a => a.AdopterId)
                  .OnDelete(DeleteBehavior.SetNull);

            entity.HasOne(a => a.Breed)
                  .WithMany(b => b.Animals)
                  .HasForeignKey(a => a.BreedId)
                  .OnDelete(DeleteBehavior.Restrict);

            entity.HasIndex(a => a.AnimalCategoryId);
            entity.HasIndex(a => a.ShelterStaffId);
            entity.HasIndex(a => a.AdopterId);
            entity.HasIndex(a => a.BreedId);
        });

        modelBuilder.Entity<Breed>(entity =>
        {
            entity.HasOne(b => b.AnimalCategory)
                  .WithMany(ac => ac.Breed)
                  .HasForeignKey(b => b.AnimalCategoryId)
                  .OnDelete(DeleteBehavior.Restrict);

            entity.HasMany(b => b.Animals)
                  .WithOne(a => a.Breed)
                  .HasForeignKey(a => a.BreedId)
                  .OnDelete(DeleteBehavior.Restrict);
        });

        modelBuilder.Entity<Customer>(entity =>
        {
            entity.HasOne(c => c.UserCategory)
                  .WithMany(uc => uc.Customer)
                  .HasForeignKey(c => c.UserCategoryId)
                  .OnDelete(DeleteBehavior.Restrict);

            entity.HasMany(c => c.AnimalsAsShelterStaff)
                  .WithOne(a => a.ShelterStaff)
                  .HasForeignKey(a => a.ShelterStaffId)
                  .OnDelete(DeleteBehavior.Restrict);

            entity.HasMany(c => c.AnimalsAsAdopter)
                  .WithOne(a => a.Adopter)
                  .HasForeignKey(a => a.AdopterId)
                  .OnDelete(DeleteBehavior.SetNull);

            entity.HasMany(c => c.Requests)
                  .WithOne(r => r.Adopter)
                  .HasForeignKey(r => r.AdopterId)
                  .OnDelete(DeleteBehavior.Restrict);

            entity.HasMany(c => c.InterviewsAsAdopter)
                  .WithOne(i => i.Adopter)
                  .HasForeignKey(i => i.AdopterId)
                  .OnDelete(DeleteBehavior.Restrict);

            entity.HasMany(c => c.InterviewsAsStaff)
                  .WithOne(i => i.ShelterStaff)
                  .HasForeignKey(i => i.ShelterStaffId)
                  .OnDelete(DeleteBehavior.Restrict);
        });

        modelBuilder.Entity<Request>(entity =>
        {
            entity.HasOne(r => r.Animal)
                  .WithMany(a => a.Requests)
                  .HasForeignKey(r => r.AnimalId)
                  .OnDelete(DeleteBehavior.Restrict);

            entity.HasOne(r => r.Adopter)
                  .WithMany(c => c.Requests)
                  .HasForeignKey(r => r.AdopterId)
                  .OnDelete(DeleteBehavior.Restrict);

            entity.HasIndex(r => r.AnimalId);
            entity.HasIndex(r => r.AdopterId);
        });

        modelBuilder.Entity<Interview>(entity =>
        {
            entity.HasOne(i => i.Adopter)
                  .WithMany(c => c.InterviewsAsAdopter)
                  .HasForeignKey(i => i.AdopterId)
                  .OnDelete(DeleteBehavior.Restrict);

            entity.HasOne(i => i.ShelterStaff)
                  .WithMany(c => c.InterviewsAsStaff)
                  .HasForeignKey(i => i.ShelterStaffId)
                  .OnDelete(DeleteBehavior.Restrict);

            entity.HasIndex(i => i.AdopterId);
            entity.HasIndex(i => i.ShelterStaffId);
        });
        modelBuilder.Entity<UserCategory>(entity =>
        {
            entity.HasMany(uc => uc.Customer)
                  .WithOne(c => c.UserCategory)
                  .HasForeignKey(c => c.UserCategoryId)
                  .OnDelete(DeleteBehavior.Restrict);
        });
    }


}
