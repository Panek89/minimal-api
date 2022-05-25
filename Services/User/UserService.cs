using minimal_api.DB;
using minimal_api.Entities;

namespace minimal_api.Services.UserService
{
    public class UserService : IUserService
    {
        private readonly MinApiContext _context;

        public UserService(MinApiContext context)
        {
            _context = context;
        }

        public User GetById(Guid id)
        {
            var userById = _context.Users.First(user => user.Id == id);

            return userById;
        }

        public async Task Register(User user)
        {
            if (user.Role == null) 
            {
                user.Role = _context.Roles.FirstOrDefault();
            }

            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
        }
    }
}