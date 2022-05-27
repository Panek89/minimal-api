using minimal_api.Entities;

namespace minimal_api.Services.UserService
{
    public interface IUserService
    {
        Task<List<User>> GetAll();
        Task<User> GetById(Guid id);
        Task<User> GetByLogin(string login);
        Task Delete(User user);
        Task DeleteMany(List<Guid> ids);
    }
}