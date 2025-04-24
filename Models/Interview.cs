using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Animal2.Models;
[Table("Interview")]
public partial class Interview
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    [MaxLength(150)]
    public string? InterviewDate { get; set; }
    [ForeignKey("AdopterId")]
     public virtual Customer Adopter { get; set; }
    public string AdopterId { get; set; }

    [ForeignKey("ShelterStaffId")]
   public virtual Customer ShelterStaff { get; set; }
   public string ShelterStaffId { get; set; }
    
   
}
