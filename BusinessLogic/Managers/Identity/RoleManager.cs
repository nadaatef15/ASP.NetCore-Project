﻿using Contracts.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using static Contracts.Infrastructure.Exceptions.TypesOfExceptions;

namespace BusinessLogic.Managers.Identity
{
    public interface IRoleManager
    {
        Task CreateRole(RoleNameModel roleNameDto);
        Task DeleteRoleById(string roleId);
        Task UpdateRole(string roleId, RoleNameModel role);
        Task<List<string?>> GetAllRoles();
    }

    public class RoleManager : IRoleManager
    {
        private readonly RoleManager<IdentityRole> _roleManagerIdentity;
        public RoleManager(RoleManager<IdentityRole> roleManagerIdentity)
        {
            _roleManagerIdentity = roleManagerIdentity;
        }

        public async Task CreateRole(RoleNameModel roleNameModel)
        {
            var checkRole = await _roleManagerIdentity.FindByNameAsync(roleNameModel.Name);

            if (checkRole is not null)
                throw new ConflictException("Role is not Exist");

            var role = new IdentityRole()
            {
                Name = roleNameModel.Name,
            };
            await _roleManagerIdentity.CreateAsync(role);
        }

        public async Task DeleteRoleById(string roleId)
        {
            var role = await _roleManagerIdentity.FindByIdAsync(roleId);

            if (role is null)
                throw new NotFoundException("Role is not Exist");

            await _roleManagerIdentity.DeleteAsync(role);
        }

        public async Task UpdateRole(string roleId, RoleNameModel role)
        {
            var isRole = await _roleManagerIdentity.FindByIdAsync(roleId);

            if (isRole is null)
                throw new NotFoundException("Role is not Exist");

            isRole.Name = role.Name;
            await _roleManagerIdentity.UpdateAsync(isRole);
        }

        public async Task<List<string?>> GetAllRoles() =>
            (await _roleManagerIdentity.Roles.ToListAsync())
            .Select(a => a.Name).ToList();

    }
}

