using System.ComponentModel.DataAnnotations;

namespace WebApiProject.Models;

public class Token
{
    [Key]
    public int Id { get; set; }
    public string AccessToken { get; set; }
    public string RefreshToken { get; set; }
}