using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.NET.Entitys;

public class Administrator
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    [Required]
    [StringLength(255)]
    public string Email { get; set; } = default!;

    [StringLength(50)]
    public string Password { get; set; } = default!;
    
    [StringLength(20)]
    public string Profile { get; set; } = default!;
}