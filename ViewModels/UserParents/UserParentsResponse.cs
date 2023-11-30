namespace WebApiProject.ViewModels.UserParents;

public class UserParentsResponse
{
    public int Id { get; set; }
    public string PinMother { get; set; }
    public string PinFather { get; set; }
    public GetUserRequest User { get; set; }
}