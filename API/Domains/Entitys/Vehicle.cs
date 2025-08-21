
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.NET.Domains.Entitys;

public class Vehicle
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }  = default!;

    [Required]
    [StringLength(100)]
    public string Name { get; set; } = default!;

    [Required]
    [StringLength(100)]
    public string Model { get; set; } = default!;
    
    [Required]
    [StringLength(10)]
    public int Year { get; set; } = default!;
}