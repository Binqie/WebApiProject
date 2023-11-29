using WebApiProject.Models;
using Microsoft.EntityFrameworkCore;
using WebApiProject.Data;

namespace WebApiProject.Services;

public class UserService
{
    private AppDbContext db;
    
    public UserService(AppDbContext dbContext)
    {
        db = dbContext;
    }
    public async Task<List<User>> GetUsers()
    {
        var users = await db.Users.ToListAsync();
        return users;
    }

    public async Task<User> GetUser(int id)
    {
        var user = await db.Users.FirstOrDefaultAsync(user => user.Id == id);
        return user;
    }

    public async Task<User> AddUser(User data)
    {
        var user = await db.Users.FirstOrDefaultAsync(u => u.Pin == data.Pin);
        if (user is not null)
        {
            return null;
        }

        var id = Guid.NewGuid().GetHashCode();
        data.Id = id;
        await db.Users.AddAsync(data);
        await db.SaveChangesAsync();
        
        return data;
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