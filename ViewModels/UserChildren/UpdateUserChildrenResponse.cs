using WebApiProject.Models;

namespace WebApiProject.ViewModels;

public class UpdateUserChildrenResponse
{
    public int Id { get; set; }
    public string Pin { get; set; }
    public string Fio { get; set; }
    public List<GetUserResponse> Users { get; set; }
}