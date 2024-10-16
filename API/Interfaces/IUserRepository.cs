using API.DTOs;
using API.Entities;

namespace API;

public interface IUserRepository 
{
    void Update(AppUser user);
    Task<bool> SaveAllAsync();

    Task<IEnumerable<AppUser>> GetAllAsync(); 
    Task<AppUser?> GetUserByIdAsync(int id);

    Task<AppUser?> GetUserByUsernameAsync(string username);

 Task<MemberDto?> GetMemberByIdAsync(int id);

 Task<MemberDto?> GetMemberByUsernameAsync(string username);

 Task<IEnumerable<MemberDto?>> GetMembersAsync();
}