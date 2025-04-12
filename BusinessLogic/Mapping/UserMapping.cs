using BusinessLogic.Resources;
using Contracts.Models;
using DataAccess.Entity;
using static DataAccess.Constants.SysEnums;

namespace BusinessLogic.Mapping
{
    public static class UserMapping
    {
        public static UserEntity ToEntity(this UserModel user) => new()
        {
            UserName = user.UserName,
            Email = user.Email,
            Address = user.Address,
            Gender = user.Gender == 'M' ? Gender.M : Gender.F
        };

        public static UserResource ToResource(this UserEntity user) => new()
        {
            UserName = user.UserName,
            Email = user.Email,
            Address = user.Address,
            Gender = user.Gender.ToString()
        };
    }
}
