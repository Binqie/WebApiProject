using System.ComponentModel.DataAnnotations;

namespace WebApiProject.Models;


public class Role
{
    [Key]
    public int Id { get; set; }
    public string Name { get; set; }
    
    public ICollection<User> Users { get; }
}