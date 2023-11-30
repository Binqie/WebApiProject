using Microsoft.EntityFrameworkCore;
using WebApiProject.Data;
using WebApiProject.Models;
using WebApiProject.ViewModels;

namespace WebApiProject.Services;

public class UserChildrenService
{
    private AppDbContext db;

    public UserChildrenService(AppDbContext dbContext)
    {
        db = dbContext;
    }

    public async Task<List<GetChildrenByUserIdResponse>> GetUserChildren(int userId)
    {
        var userChildren = await db.UserChildren.Include(c => c.Users).Where(children => children.Users.Select(u => u.Id).Contains(userId)).Select(children => new GetChildrenByUserIdResponse()
        {
            Id = children.Id,
            Pin = children.Pin,
            Fio = children.Fio
        }).ToListAsync();
        
        return userChildren;
    }

    public async Task<GetUserChildrenResponse> GetUserChild(int childId)
    {
        var userChild = await db.UserChildren.Include(c => c.Users).FirstOrDefaultAsync(uc => uc.Id == childId);

        var response = new GetUserChildrenResponse()
        {
            Pin = userChild.Pin,
            Fio = userChild.Fio,
            Users = userChild.Users.Select(u => new GetUserRequest()
            {
                Id = u.Id,
                Pin = u.Pin,
                Fio = u.Fio
            }).ToList()
        };
        
        return response;
    }

    public async Task<GetUserChildrenResponse> AddUserChildren(GetUserChildrenRequest data)
    {
        var userChildren = await db.UserChildren.FirstOrDefaultAsync(c => c.Pin == data.Pin);
        var parent = await db.Users.FirstOrDefaultAsync(u => u.Id == data.UserId);

        if (userChildren is not null || parent is null)
        {
            return null;
        }

        var newUserChildren = new UserChildren()
        {
            Pin = data.Pin,
            Fio = data.Fio,
            Users = new List<User>() { parent }
        };

        await db.UserChildren.AddAsync(newUserChildren);
        await db.SaveChangesAsync();
        parent.UserChildren = new List<UserChildren>() { newUserChildren };
        
        var response = new GetUserChildrenResponse()
        {
            Id = newUserChildren.Id,
            Pin = data.Pin,
            Fio = data.Fio,
            Users = new List<GetUserRequest>() { new GetUserRequest()
            {
                Id = parent.Id,
                Pin = parent.Pin,
                Fio = parent.Fio
            }}
        };
        return response;
    }

    public async Task<UpdateUserChildrenResponse> UpdateUserChildren(UpdateUserChildrenRequest data)
    {
        var userChildren = await db.UserChildren.Include(c => c.Users).FirstOrDefaultAsync(c => c.Id == data.Id);
        if (userChildren is null)
        {
            return null;
        }

        userChildren.Fio = data.Fio;
        userChildren.Pin = data.Pin;

        var response = new UpdateUserChildrenResponse()
        {
            Id = userChildren.Id,
            Pin = userChildren.Pin,
            Fio = userChildren.Fio,
            Users = userChildren.Users.Select(u => new GetUserResponse()
            {
                Id = u.Id,
                Pin = u.Pin,
                Fio = u.Fio
            }).ToList()
        };

        db.UserChildren.Update(userChildren);
        await db.SaveChangesAsync();
        return response;
    }

    public async Task<UserChildren> DeleteUserChildren(int id)
    {
        var userChildren = await db.UserChildren.Include(c => c.Users).FirstOrDefaultAsync(c => c.Id == id);
        if (userChildren is null)
        {
            return null;
        }

        db.UserChildren.Remove(userChildren);
        await db.SaveChangesAsync();
        return userChildren;
    }
}