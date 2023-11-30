namespace WebApiProject.ViewModels;

public class GetUsersDTO
{
    public int Id { get; set; }
    public string Pin { get; set; }
    public string Fio { get; set; }
    public List<GetUserChildrenRequest> UserChildren { get; set; }
}