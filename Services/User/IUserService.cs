using minimal_api.Entities;

namespace minimal_api.Services.UserService
{
    public interface IUserService
    {
        Task<List<User>> GetAll();
        User GetById(Guid id);
        Task Register(User user);
        Task Delete(User user);
        Task DeleteMany(List<Guid> ids);
    }
}