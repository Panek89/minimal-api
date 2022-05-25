using minimal_api.Entities;

namespace minimal_api.Services.UserService
{
    public interface IUserService
    {
        User GetById(Guid id);

        Task Register(User user);
    }
}