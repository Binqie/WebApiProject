using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApiProject.Models;

public class UserChildren
{
    [Key]
    public int Id { get; set; }
    [Column(TypeName = "varchar(14)")]
    public string Pin { get; set; }
    public string Fio { get; set; }
    
    public ICollection<User> Users { get; set; }
}