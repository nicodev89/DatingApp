using Microsoft.AspNetCore.Mvc;
using API.Entities;
using API.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using AutoMapper;
using API.DTOs;

namespace API.Controllers;

[AllowAnonymous]
public class UsersController(IUserRepository userRepository) : BaseApiController
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


}