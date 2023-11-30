using WebApiProject.Models;
using Microsoft.EntityFrameworkCore;
using WebApiProject.Data;
using WebApiProject.ViewModels;

namespace WebApiProject.Services;

public class UserService
{
    private AppDbContext db;
    
    public UserService(AppDbContext dbContext)
    {
        db = dbContext;
    }
    public async Task<List<GetUsersDTO>> GetAll()
    {
        var users = await db.Users.Include(u => u.UserChildren).Include(u => u.UserParents).Select(user => new GetUsersDTO()
        {
            Id = user.Id,
            Pin = user.Pin,
            Fio = user.Fio,
            UserChildren = user.UserChildren.Select(children => new GetUserChildrenRequest()
            {
                UserId = user.Id,
                Pin = children.Pin,
                Fio = children.Fio
            }).ToList(),
            Roles = user.Roles.Select(r => r.Name).ToList()
        }).ToListAsync();
        
        return users;
    }
    
    public async Task<GetUserResponse> GetUser(int id)
    {
        var user = await db.Users.Include(u => u.Roles).Include(u => u.UserChildren).Include(u => u.UserParents).Select(user => new GetUserResponse()
        {
            Id = user.Id,
            Pin = user.Pin,
            Fio = user.Fio,
            UserChildren = user.UserChildren.Select(children => new GetUserChildrenRequest()
            {
                UserId = user.Id,
                Pin = children.Pin,
                Fio = children.Fio
            }).ToList(),
            Roles = user.Roles.Select(r => r.Name).ToList()
        }).FirstOrDefaultAsync(user => user.Id == id);

        return user;
    }
    
    public async Task<CreateUserResponse> AddUser(CreateUserRequest data)
    {
        var user = await db.Users.FirstOrDefaultAsync(u => u.Pin == data.Pin);
        if (user is not null)
        {
            return null;
        }

        var defaultRole = await db.Role.FirstOrDefaultAsync(r => r.Name == "user");
        
        if (defaultRole is null)
        {
            defaultRole = new Role() { Name = "user" };
            await db.Role.AddAsync(defaultRole);
            await db.SaveChangesAsync();
        }
        
        if (data.Fio == "admin")
        {
            defaultRole = await db.Role.FirstOrDefaultAsync(r => r.Name == "admin");
        }
        
        user = new User()
        {
            Pin = data.Pin,
            Fio = data.Fio,
            Password = BCrypt.Net.BCrypt.HashPassword(data.Password),
            Roles = new List<Role>()
            {
                defaultRole
            }
        };
        
        await db.Users.AddAsync(user);
        await db.SaveChangesAsync();
        defaultRole.Users.Add(user);
        return new CreateUserResponse() { Id = user.Id, Pin = user.Pin, Fio = user.Fio };
    }
    
    public async Task<User> UpdateUser(User data)
    {
        var user = await db.Users.FirstOrDefaultAsync(user => user.Id == data.Id);
        if (user is null)
        {
            return null;
        }
    
        user.Pin = data.Pin;
        user.Fio = data.Fio;
        user.Password = data.Password;
        
        db.Users.Update(user);
        await db.SaveChangesAsync();
    
        return data;
    }
    
    public async Task<User> DeleteUser(int id)
    {
        var user = await db.Users.FirstOrDefaultAsync(user => user.Id == id);
    
        if (user is null)
        {
            return null;
        }
        
        db.Users.Remove(user);
        await db.SaveChangesAsync();
        
        return user;
    }
}