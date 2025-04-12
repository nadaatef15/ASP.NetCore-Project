using BusinessLogic.Resources;
using Contracts.Models;
using DataAccess.Entity;
using static DataAccess.Constants.SysEnums;

namespace BusinessLogic.Mapping
{
    public static class ClientsMapping
    {
        public static ClientEntity ToEntity(this ClientModel user) => new()
        {
            UserName = user.UserName,
            Email = user.Email,
            Address = user.Address,
            Gender = user.Gender == 'M' ? Gender.M : Gender.F
        };

        public static UserResource ToResource(this ClientEntity user) => new()
        {
            Id=user.Id,
            UserName = user.UserName,
            Email = user.Email,
            Address = user.Address,
            Gender = user.Gender.ToString()
        };
    }
}
