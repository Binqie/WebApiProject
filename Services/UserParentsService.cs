using Microsoft.EntityFrameworkCore;
using WebApiProject.Data;
using WebApiProject.Models;
using WebApiProject.ViewModels;
using WebApiProject.ViewModels.UserParents;

namespace WebApiProject.Services;

public class UserParentsService
{
    private AppDbContext db;

    public UserParentsService(AppDbContext dbContext)
    {
        db = dbContext;
    }

    public async Task<List<UserParentsByIdResponse>> GetById(int parentsId)
    {
        var userParents = await db.UserParents.Include(p => p.User).Where(parents => parents.Id == parentsId).Select(
            parents => new UserParentsByIdResponse()
            {
                Id = parents.Id,
                PinMother = parents.PinMother,
                PinFather = parents.PinFather,
                Users = db.Users.Where(user => user.UserParentsId == parents.Id).Select(user => new GetUserRequest()
                {
                    Id = user.Id,
                    Pin = user.Pin,
                    Fio = user.Fio
                }).ToList()
            }).ToListAsync();

        return userParents;
    }

    public async Task<GetUserParentsRequest> GetUserParentsById(int userId)
    {
        var userParents = await db.UserParents.Include(p => p.User).Where(parents => parents.User.Id == userId).Select(parents => new GetUserParentsRequest()
        {
            Id = parents.Id,
            PinFather = parents.PinFather,
            PinMother = parents.PinMother
        }).FirstOrDefaultAsync();
        
        return userParents;
    }

    public async Task<UserParentsResponse> AddUserParents(UserParentsRequest data)
    {
        var user = await db.Users.FirstOrDefaultAsync(u => u.Id == data.UserId);
        var userParents = await db.UserParents.FirstOrDefaultAsync(u => u.PinMother == data.PinMother || u.PinFather == data.PinFather);

        if (userParents is not null || user is null)
        {
            return null;
        }

        var newUserParents = new UserParents()
        {
            PinMother = data.PinMother,
            PinFather = data.PinFather,
            User = user
        };

        await db.UserParents.AddAsync(newUserParents);
        await db.SaveChangesAsync();
        user.UserParents = newUserParents;
        user.UserParentsId = newUserParents.Id;
        
        var response = new UserParentsResponse()
        {
            Id = newUserParents.Id,
            PinMother = data.PinMother,
            PinFather = data.PinFather,
            User = new GetUserRequest()
            {
                Id = user.Id,
                Fio = user.Fio,
                Pin = user.Pin
            }
        };
        
        return response;
    }

    public async Task<UserParentsByIdResponse> UpdateUserParents(GetUserParentsRequest data)
    {
        var userParents = await db.UserParents.Include(c => c.User).FirstOrDefaultAsync(parents => parents.Id == data.Id);
        if (userParents is null)
        {
            return null;
        }

        userParents.PinFather = data.PinFather;
        userParents.PinMother = data.PinMother;

        var response = new UserParentsByIdResponse()
        {
            Id = userParents.Id,
            PinFather = userParents.PinFather,
            PinMother = userParents.PinMother,
            Users = db.Users.Where(user => user.Id == userParents.User.Id).Select(u => new GetUserRequest()
            {
                Id = u.Id,
                Pin = u.Pin,
                Fio = u.Fio
            }).ToList()
        };

        db.UserParents.Update(userParents);
        await db.SaveChangesAsync();
        return response;
    }

    public async Task<UserParentsByIdResponse> DeleteUserParents(int id)
    {
        var userParents = await db.UserParents.Include(c => c.User).FirstOrDefaultAsync(parents => parents.Id == id);
        if (userParents is null)
        {
            return null;
        }

        var response = new UserParentsByIdResponse()
        {
            Id = userParents.Id,
            PinMother = userParents.PinMother,
            PinFather = userParents.PinFather,
            Users = db.Users.Where(user => user.Id == userParents.User.Id).Select(u => new GetUserRequest()
            {
                Id = u.Id,
                Pin = u.Pin,
                Fio = u.Fio
            }).ToList()
        };

        db.UserParents.Remove(userParents);
        await db.SaveChangesAsync();
        return response;
    }
}