using DespensaAPI.Models;
using System.Threading.Tasks;

namespace DespensaAPI.Services
{
    public interface IUserService
    {
        Task<UserModel> AuthenticateAsync(string username, string password);
        // Otros métodos relacionados con la gestión de usuarios
    }

   
}