using CloudinaryDotNet.Core;
using Contracts.Models;
using DataAccess.Entity;

namespace BusinessLogic.Services.User
{
    public interface IUserService
    {
        void SetValues(UserEntity entity, UserModel model);
    }
    public class UserService : IUserService
    {
        public void SetValues(UserEntity entity, UserModel model)
        {
            entity.Address = model.Address;
            entity.UserName = model.UserName;
            entity.Email = model.Email;
        }
    }
}
