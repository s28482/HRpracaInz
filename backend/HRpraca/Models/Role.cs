namespace HRpraca.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


[Table("Role")]
public class Role
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int RoleId { get; set; }

    [Required] [MaxLength(100)] public string Name { get; set; } = null!;
    
    public ICollection<User> Users { get; set; } = new List<User>();
    
    
}