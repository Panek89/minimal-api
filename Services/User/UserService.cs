using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using minimal_api.DB;
using minimal_api.Entities;

namespace minimal_api.Services.UserService
{
    public class UserService : IUserService
    {
        private readonly MinApiContext _context;
        private readonly IPasswordHasher<User> _passwordHasher;

        public UserService(MinApiContext context, IPasswordHasher<User> passwordHasher)
        {
            _context = context;
            _passwordHasher = passwordHasher;
        }

        public async Task<List<User>> GetAll()
        {
            var users = await _context.Users.Include(user => user.Role)
                                            .ToListAsync();

            return users;
        }

        public async Task<User> GetById(Guid id)
        {
            var userById = await _context.Users.Include(user => user.Role)
                .FirstOrDefaultAsync(user => user.Id == id);

            return userById;
        }

        public async Task<User> GetByLogin(string login)
        {
            var userByLogin = await _context.Users.Include(user => user.Role)
                .FirstOrDefaultAsync(user => user.Login == login);

            return userByLogin;
        }

        public async Task Delete(User user)
        {
            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteMany(List<Guid> ids)
        {
            var usersToRemove = _context.Users.Where(x => ids.Contains(x.Id));
            _context.Users.RemoveRange(usersToRemove);
            await _context.SaveChangesAsync();
        }
    }
}