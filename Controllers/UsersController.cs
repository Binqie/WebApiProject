using WebApiProject.Models;
using WebApiProject.Services;
using WebApiProject.Services;
using WebApiProject.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApiProject.Data;

[Authorize]
[Route("api/[controller]/[action]")]
[ApiController]  
public class UsersController : ControllerBase
{
    private readonly IJWTService _jWTService;
    private readonly UserService _userService;
    private readonly AppDbContext _dbContext;

    public UsersController(IJWTService jWTService, UserService userService, AppDbContext dbContext)
    {
        _jWTService = jWTService;
        _userService = userService;
        _dbContext = dbContext;
    }
    
    [HttpGet]
    public async Task<List<User>> GetAll()
    {
        return await _userService.GetUsers();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult> Get(int id)
    {
        var user = await _userService.GetUser(id);

        if (user == null)
        {
            return NotFound();
        }

        return Ok(user);
    }
    
    [AllowAnonymous]
    [HttpPost]
    public async Task<IActionResult> Create(User user)
    {
        if (user is null)
        {
            return BadRequest();
        }
    
        var newUser = await _userService.AddUser(user);
        return Ok(newUser);
    }
    
    [HttpPut]
    public async Task<IActionResult> Update(User user)
    {
        if (user is null)
        {
            return BadRequest();
        }

        var updatedUser = await _userService.UpdateUser(user);
        if (updatedUser is null)
        {
            return NotFound();
        }

        return Ok(updatedUser);
    }
    
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var deletedUser = await _userService.DeleteUser(id);
        if (deletedUser is null)
        {
            return NotFound();
        }

        return Ok(deletedUser);
    }

    [AllowAnonymous]
    [HttpPost]
    public async Task<IActionResult> SignIn([FromBody] SignInRequest data)
    {
        if (!_dbContext.Users.Any(user => user.Pin == data.Pin))
        {
            return BadRequest();
        }

        var user = await _dbContext.Users.FirstOrDefaultAsync(user => user.Pin == data.Pin && user.Password == data.Password);
        
        if (user is null)
        {
            return Unauthorized();
        }

        var accessToken = _jWTService.Authenticate(user);
        // var refreshToken = tokenService.GenerateWebToken(user, 1000);
        
        return Ok(accessToken);
    }
}