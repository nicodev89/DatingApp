using System.Security.Cryptography;
using System.Text;
using API.Data;
using API.DTOs;
using API.Entities;
using API.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers;
public class AccountController(DataContext context) : BaseApiController
{
    [HttpPost("register")]
    public async Task<ActionResult<UserDto>> Register(RegisterDto registerDto, ITokenService tokenService)
    {
        if (await UserAlreadyExists(registerDto.Username)) return BadRequest("User name already exists.");

        using var hmac = new HMACSHA512();

        var user = new AppUser
        {
            UserName = registerDto.Username,
            PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(registerDto.Password)),
            PasswordSalt = hmac.Key
        };

        context.Users.Add(user);
        await context.SaveChangesAsync();

        return new UserDto
        {
            Username = user.UserName,
            Token = tokenService.CreateToken(user)
        };
    }

    [HttpPost("login")]
    public async Task<ActionResult<UserDto>> LoginRequest(LoginDto loginDto, ITokenService tokenService)
    {
        var user = await context.Users.FirstOrDefaultAsync(x =>
        x.UserName == loginDto.Username.ToLower());

        if (user is null) return Unauthorized("Invalid Username.");

        using var hmac = new HMACSHA512(user.PasswordSalt);

        var computerHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(loginDto.Password));

        if (computerHash.Length != user.PasswordHash.Length)
        {
            return Unauthorized("Wrong password.");
        }

        for (int i = 0; i < computerHash.Length; i++)
        {
            if (computerHash[i] != user.PasswordHash[i])
            {
                return Unauthorized("Wrong password.");
            }
        }

        return new UserDto
        {
            Username = user.UserName,
            Token = tokenService.CreateToken(user)
        };
    }



    private async Task<bool> UserAlreadyExists(string Username)
    {
        return await context.Users.AnyAsync(x => x.UserName.ToLower() == Username.ToLower());
    }
}
