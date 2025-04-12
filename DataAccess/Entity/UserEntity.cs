using DataAccess.Interfaces;
using Microsoft.AspNetCore.Identity;
using static DataAccess.Constants.SysEnums;

namespace DataAccess.Entity
{
    public class UserEntity : IdentityUser, ISoftDelete
    {
        public string Address { get; set; }

        public string? ImagePath { get; set; }

        public int Age { get; set; }

        public Gender Gender { get; set; }

        public bool IsDeleted { get; set; }
        public DateTime? DeletedOn { get; set; }
        public string? DeletedBy { get; set; }
    }
}