namespace WebApiProject.ViewModels;

public class GetUserChildrenResponse
{
    public int Id { get; set; }
    public string Pin { get; set; }
    public string Fio { get; set; }
    public List<GetUserRequest> Users { get; set; }
}