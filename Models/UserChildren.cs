namespace WebApiProject.Models;

public class UserChildren
{
    public int Id { get; set; }
    public string Pin { get; set; }
    public string Fio { get; set; }
    
    public List<User> Users { get; set; }
}