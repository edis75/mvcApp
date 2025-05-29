using mvcApp.Models;

namespace mvcApp.Repository.Interfaces
{
    public interface IAccountRepository
    {
        void Register(User user);
        User? Login(string email, string password);

    }
}
