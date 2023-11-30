namespace WebApiProject.ViewModels.UserParents;

public class UserParentsByIdResponse
{
    public int Id { get; set; }
    public string PinMother { get; set; }
    public string PinFather { get; set; }
    public List<GetUserRequest> Users { get; set; }
}