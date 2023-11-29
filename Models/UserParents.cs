namespace WebApiProject.Models;

public class UserParents
{
    public int Id { get; set; }
    public string PinMother { get; set; }
    public string PinFather { get; set; }
    
    public List<User> User { get; set; }
}