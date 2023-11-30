namespace WebApiProject.ViewModels.UserParents;

public class UserParentsRequest
{
    public int  UserId { get; set; }
    public string PinMother { get; set; }
    public string PinFather { get; set; }
}