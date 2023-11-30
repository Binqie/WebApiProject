using Microsoft.AspNetCore.Mvc;
using WebApiProject.Data;
using WebApiProject.Services;
using WebApiProject.ViewModels;

namespace JwtTest.Controllers;

[Route("api/[controller]/[action]")]
[ApiController]
public class UserChildrenController : ControllerBase
{
    private readonly AppDbContext _dbContext;
    private readonly UserChildrenService _childrenService;
    
    public UserChildrenController(AppDbContext dbContext, UserChildrenService childrenService)
    {
        _dbContext = dbContext;
        _childrenService = childrenService;
    }
    
    [HttpGet("{id}")]
    public async Task<ActionResult> GetByUserId(int id)
    {
        var userChildren = await _childrenService.GetUserChildren(id);
        if (userChildren is null)
        {
            return NotFound();
        }
        return Ok(userChildren);
    }
    
    [HttpGet("{id}")]
    public async Task<ActionResult> Get(int id)
    {
        var userChild = await _childrenService.GetUserChild(id);
        if (userChild is null)
        {
            return NotFound();
        }
        return Ok(userChild);
    }
    
    [HttpPost]
    public async Task<ActionResult> Add(GetUserChildrenRequest data)
    {
        var result = await _childrenService.AddUserChildren(data);
        if (result is null)
        {
            return BadRequest();
        }
        return Ok(result);
    }
    
    [HttpPut]
    public async Task<ActionResult> Update(UpdateUserChildrenRequest data)
    {
        var result = await _childrenService.UpdateUserChildren(data);
        if (result is null)
        {
            return NotFound();
        }
        return Ok(result);
    }
    
    [HttpDelete("{id}")]
    public async Task<ActionResult> Remove(int id)
    {
        var result = await _childrenService.DeleteUserChildren(id);
        if (result is null)
        {
            return NotFound();
        }
        return Ok(result);
    }
}