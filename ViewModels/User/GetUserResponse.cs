using WebApiProject.Models;

namespace WebApiProject.ViewModels;

public class GetUserResponse
{
    public int Id { get; set; }
    public string Pin { get; set; }
    public string Fio { get; set; }
    public List<GetUserChildrenRequest> UserChildren { get; set; }
    public List<String> Roles { get; set; }
}