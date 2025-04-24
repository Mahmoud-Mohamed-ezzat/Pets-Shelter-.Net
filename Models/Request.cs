using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Animal2.Models;
[Table("Request")]
public partial class Request
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    [ForeignKey("AnimalId")]
   public virtual Animal Animal { get; set; }
    public int AnimalId { get; set; }

    [ForeignKey("AdopterId")]
    public virtual Customer Adopter { get; set; }
    public string AdopterId { get; set; }
    public string Status { get; set; } //Approve-reject
    
    
   
}
