using API.DTOs;
using API.Entities;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using AutoMapper.QueryableExtensions;

namespace API.Data.Repositories
{
    public class UserRepository(DataContext context, IMapper mapper) : IUserRepository
    {
        public async Task<IEnumerable<AppUser>> GetAllAsync()
        {
            return await context.Users.Include(x => x.Photos).ToListAsync();
        }

        public async Task<MemberDto?> GetMemberByIdAsync(int id)
        {
            return await context.Users.Where(user => user.Id == id)
                .ProjectTo<MemberDto>(mapper.ConfigurationProvider).SingleOrDefaultAsync();
        }

        public async Task<MemberDto?> GetMemberByUsernameAsync(string username)
        {
            return await context.Users.Where(user => user.UserName == username)
                .ProjectTo<MemberDto>(mapper.ConfigurationProvider).SingleOrDefaultAsync();
        }

        public async Task<IEnumerable<MemberDto?>> GetMembersAsync()
        {
            return await context.Users.ProjectTo<MemberDto>(mapper.ConfigurationProvider).ToListAsync();
        }


        public async Task<AppUser?> GetUserByIdAsync(int id)
        {
            return await context.Users.FindAsync(id);
        }

        public async Task<AppUser?> GetUserByUsernameAsync(string username)
        {
            if (username is null) return default;

            return await context.Users.Include(x => x.Photos).SingleOrDefaultAsync(x => x.UserName.Equals(username));
        }

        public async Task<bool> SaveAllAsync()
        {
            return await context.SaveChangesAsync() > 0;
        }

        public void Update(AppUser user)
        {
            context.Entry(user).State = EntityState.Modified;
        }

    }
}