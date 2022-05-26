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

        public User GetById(Guid id)
        {
            var userById = _context.Users.Include(user => user.Role)
                                         .First(user => user.Id == id);

            return userById;
        }

        public async Task Register(User user)
        {
            if (user.Role == null) 
            {
                user.Role = _context.Roles.FirstOrDefault();
            }

            var hashedPassword = _passwordHasher.HashPassword(user, user.Password);
            user.Password = hashedPassword;

            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
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