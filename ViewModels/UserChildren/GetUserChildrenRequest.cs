using WebApiProject.Models;

namespace WebApiProject.ViewModels;

public class GetUserChildrenRequest
{
    public string Pin { get; set; }
    public string Fio { get; set; }
    public int UserId { get; set; }
}