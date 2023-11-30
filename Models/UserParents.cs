using System.ComponentModel.DataAnnotations;

namespace WebApiProject.Models;

public class UserParents
{
    [Key]
    public int Id { get; set; }
    public string PinMother { get; set; }
    public string PinFather { get; set; }
    
    public User? User { get; set; }
}