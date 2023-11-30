using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApiProject.Models;

public class User
{
    [Key]
    public int Id { get; set; }
    [Column(TypeName = "varchar(14)")]
    public string Pin { get; set; }
    [Column(TypeName = "varchar(50)")]
    public string Fio { get; set; }
    [Column(TypeName = "varchar(20)")]
    public string Password { get; set; }
    
    public ICollection<Role> Roles { get; set; }
    public ICollection<UserChildren> UserChildren { get; set; }
    public UserParents? UserParents { get; set; }
    public int? UserParentsId { get; set; }
}