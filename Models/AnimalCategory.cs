using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Animal2.Models;
[Table("AnimalCategory")]

public partial class AnimalCategory
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    [MaxLength(150)]
    public string? CategoryName { get; set; }

    public virtual ICollection<Animal> Animal { get; set; } = new List<Animal>();
    public virtual ICollection<Breed> Breed { get; set; } = new List<Breed>();
}
