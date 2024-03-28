using ApiDevBP.Models;
using ApiDevBP.Response;

namespace ApiDevBP.Contracts
{
    public interface IUserService
    {
        Task<Response<bool>> Insert(UserModel userModel);
        Task<Response<bool>> Update(UserModel userModel, int id);
        Task<Response<bool>> Delete(int userId);

        Task<Response<UserModel>> Get(int userId);
        Task<Response<IEnumerable<UserModel>>> GetAll();
    }
}
