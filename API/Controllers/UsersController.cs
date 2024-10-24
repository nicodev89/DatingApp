using Microsoft.AspNetCore.Mvc;
using API.Entities;
using API.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using AutoMapper;
using API.DTOs;
using System.Security.Claims;

namespace API.Controllers;

[AllowAnonymous]
public class UsersController(IUserRepository userRepository, IMapper mapper) : BaseApiController
{

    // OLD WAY TO DO STUFF HERE
    // private readonly ILogger<UsersController> _logger;
    // private readonly DataContext _dataContext;

    // // Data context will be scoped only for the HTTP request that will be done
    // // when http request arrives the framework will check out controller and use the datacontext
    // // to create db connection and retrieve the data and once it serves it it will be disposed of
    // public UsersController(ILogger<UsersController> logger, DataContext dataContext)
    // {
    //     _logger = logger;
    //     _dataContext = dataContext;
    // }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<MemberDto>>> GetUsers()
    {

        var users = await userRepository.GetMembersAsync();
        // this will actually return a 200 http respose (OK) 
        // since we specified the return type the framework create the correct
        // type of http response
        return Ok(users);
    }

    [HttpGet("{username}")]
    public async Task<ActionResult<MemberDto>> GetUser(string username)
    {
        var user = await userRepository.GetMemberByUsernameAsync(username);

        if (user is null) return NotFound();

        return user;
    }

    [HttpPut]
    public async Task<ActionResult> UpdateUser(MemberUpdateDto memberUpdateDto) 
    {
        // we are checking inside the Claim which is inside the token we are using for
        // authorization of these requests
        var username = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        if(username is null) return BadRequest("No username found in token");

        // using our repository to get the user
        var user = await userRepository.GetUserByUsernameAsync(username);

        if(user is null) return BadRequest("Cannot find user");

        // here we are mapping the DTO object to the user. The dto member update object
        // contains some of the properties of user so THOSE properties will be changed in the user object
        // now since Entity Framework is tracking changes it will see the changes we are doing on user object
        mapper.Map(memberUpdateDto, user);

        // here we are saving those changes and since this is a Http Put request we return NoContent
        // which means change was done but we have nothing to return
        if(await userRepository.SaveAllAsync()) return NoContent();

        return BadRequest("Failed to Update the user");
    }
}