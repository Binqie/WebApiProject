namespace WebApiProject.ViewModels.UserParents;

public class GetUserParentsRequest
{
    public int Id { get; set; }
    public string PinMother { get; set; }
    public string PinFather { get; set; }
}