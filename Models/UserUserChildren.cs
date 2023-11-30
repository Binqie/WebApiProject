using System.ComponentModel.DataAnnotations;

namespace WebApiProject.Models;

public class UserUserChildren
{
    public int UserId { get; set; }
    public int UserChildrenId { get; set; }
    public User User { get; set; } = null!;
    public UserChildren UserChildren { get; set; } = null!;
}