namespace WebApiProject.Models;

public class User
{
    public int Id { get; set; }
    public string Pin { get; set; }
    public string Fio { get; set; }
    public string Password { get; set; }
    
    public List<Role> Role { get; set; }
    public List<UserChildren>? UserChildren { get; set; }
    public UserParents UserParents { get; set; }
}