using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Animal2.Models;
[Table("UserCategory")]
public partial class UserCategory
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    public string? CategoryName { get; set; }
    public virtual ICollection<Customer> Customer { get; set; }=new List<Customer>();
}
