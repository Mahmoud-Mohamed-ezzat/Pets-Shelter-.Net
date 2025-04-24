using Animal2.Mapper;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Animal2.Models;

[Table("Animal")]
public partial class Animal
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    [MaxLength(150)]
    public string AnimalName { get; set; }
    [MaxLength(150)]
    public string AnimalGender { get; set; }
    [MaxLength(150)]
    public string AnimalStatus { get; set; }
    [MaxLength(150)]
    public string AnimalHistory { get; set; }
    public string Image { get; set; }

    public int Age { get; set; }

     [ForeignKey("AnimalCategoryId")]
    public virtual AnimalCategory? AnimalCategory { get; set; }
        [ForeignKey("ShelterStaffId")]
    public  virtual Customer ShelterStaff { get; set; }
        [ForeignKey("AdopterId")]
    public virtual Customer? Adopter { get; set; }

    [ForeignKey("BreedId")]
    public virtual Breed Breed {get; set; }
    public int AnimalCategoryId { get; set; }

    public string ShelterStaffId { get; set; }

    public string? AdopterId { get; set; }

    public int BreedId { get; set; }

    public virtual ICollection<Request> Requests { get; set; } = new List<Request>();


}
