using Microsoft.AspNetCore.Mvc;
using WebApiProject.Data;
using WebApiProject.Models;
using WebApiProject.Services;

using WebApiProject.ViewModels.UserParents;

namespace JwtTest.Controllers;

[Route("api/[controller]/[action]")]
[ApiController]
public class UserParentsController : ControllerBase
{
    private readonly AppDbContext _dbContext;
    private readonly UserParentsService _parentsService;
    
    public UserParentsController(AppDbContext dbContext, UserParentsService parentsService)
    {
        _dbContext = dbContext;
        _parentsService = parentsService;
    }
    
    [HttpGet("{id}")]
    public async Task<ActionResult> GetUserParentsByUserId(int id)
    {
        var userParents = await _parentsService.GetUserParentsById(id);
        if (userParents is null)
        {
            return NotFound();
        }
        return Ok(userParents);
    }
    
    [HttpGet("{id}")]
    public async Task<ActionResult> GetById(int id)
    {
        var parents = await _parentsService.GetById(id);
        if (parents is null)
        {
            return NotFound();
        }
        return Ok(parents);
    }
    
    [HttpPost]
    public async Task<ActionResult<UserParentsResponse>> Add(UserParentsRequest data)
    {
        var result = await _parentsService.AddUserParents(data);
        if (result is null)
        {
            return BadRequest();
        }
        return Ok(result);
    }
    
    [HttpPut]
    public async Task<ActionResult<UserParentsByIdResponse>> Update(GetUserParentsRequest data)
    {
        var result = await _parentsService.UpdateUserParents(data);
        if (result is null)
        {
            return NotFound();
        }
        return Ok(result);
    }
    
    [HttpDelete("{id}")]
    public async Task<ActionResult<UserParents>> Remove(int id)
    {
        var result = await _parentsService.DeleteUserParents(id);
        if (result is null)
        {
            return NotFound();
        }
        return Ok(result);
    }
}