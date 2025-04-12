using BusinessLogic.Managers.Identity;
using BusinessLogic.Mapping;
using BusinessLogic.Resources;
using BusinessLogic.Services.GeneralServices;
using Contracts.Models;
using DataAccess.Entity;
using DataAccess.Repo;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using static Contracts.Infrastructure.Exceptions.TypesOfExceptions;

namespace BusinessLogic.Managers.Clients
{
    public interface IClientsManager
    {
        Task<UserResource> RegisterClient(ClientModel model);
        Task UpdateClient(string dctorId, UserModel doctorModel);
        Task<UserResource> GetClientById(string id);
        Task<List<UserResource>> GetAllClients();
        Task DeleteDoctor(string docId);
    }
    public class ClientsManager : IClientsManager
    {
        private readonly UserManager<UserEntity> _userManagerIdentity;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IClientRepo _clientRepo;
        private readonly IFileService _fileService;
        private readonly IUserManager _userManager;

        public ClientsManager(UserManager<UserEntity> userManagerIdentity , RoleManager<IdentityRole> roleManager ,
             IClientRepo clientRepo , IFileService fileService , IUserManager userManager)

        {
            _clientRepo = clientRepo;
            _fileService= fileService;
            _userManagerIdentity= userManagerIdentity;
            _roleManager= roleManager;
            _userManager= userManager;
        }
        public async Task DeleteDoctor(string clientId)
        {
            var client = await _clientRepo.GetClientById(clientId) ??
                 throw new NotFoundException("client is not Exist");

            var result = await _userManagerIdentity.IsInRoleAsync(client, "Client");

            if (!result)
                throw new ConflictException("It does not hav role Client");

            await _userManager.DeleteUser(clientId);
        }

        public async Task<List<UserResource>> GetAllClients()=>
            (await _clientRepo.GetAllClients()).Select(a=>a.ToResource()).ToList();
        

        public async Task<UserResource> GetClientById(string id)
        {
           var client= await _clientRepo.GetClientById(id) ??
                throw new NotFoundException("Client is not Exist");

            return client.ToResource();
        }

        public async Task<UserResource> RegisterClient(ClientModel model)
        {
            if (!await _roleManager.RoleExistsAsync("Client"))
                throw new NotFoundException("Role Client is Not Exist");

            var client = model.ToEntity();

            if (model.Image is not null)
                client.ImagePath = await _fileService.UploadImage(model.Image);

            var result = await _userManagerIdentity.CreateAsync(client, model.Password);

            if (!result.Succeeded)
            {
                var errors = string.Join(Environment.NewLine, result.Errors);
                throw new ValidationException(errors);
            }

            await _userManagerIdentity.AddToRoleAsync(client,"Client");

            return (await _clientRepo.GetClientById(client.Id))!.ToResource();

        }

        public async Task UpdateClient(string clientId, UserModel model)
        {
            if (model.Id != clientId)
                throw new ConflictException("Not the same Id");

            var client = await _clientRepo.GetClientById(clientId) ??
                throw new NotFoundException("Client is not exist");

            if (model.Image is not null)
                client.ImagePath = await _fileService.UploadImage(model.Image);

            await _userManager.UpdateUser(client, model);
        }
    }
}
