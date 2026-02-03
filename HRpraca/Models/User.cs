namespace HRpraca.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


[Table("User")]
public class User
{
    
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int UserId { get; set; }

    [Required] [MaxLength(100)] public string FirstName { get; set; } = null!;
   
    [Required] [MaxLength(100)] public string LastName { get; set; } = null!;
    
    [Required] [MaxLength(320)] public string Email { get; set; } = null!;

    [Required] [MaxLength(255)] public string PasswordHash { get; set; } = null!;
    
    public bool IsActive { get; set; }

    [Required]
    public int RoleId { get; set; }
    public Role Role { get; set; } = null!;
}