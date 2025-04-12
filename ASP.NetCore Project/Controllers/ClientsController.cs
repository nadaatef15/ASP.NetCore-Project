using BusinessLogic.Managers.Clients;
using BusinessLogic.Resources;
using Contracts.Models;
using Microsoft.AspNetCore.Mvc;

namespace ASP.NetCore_Project.Controllers
{
    public class ClientsController : BaseController
    {
        private readonly IClientsManager _clientsManager;
        public ClientsController(IClientsManager clientsManager)=>
        _clientsManager = clientsManager;

        [HttpPost(Name = "RegisterClient")]
        [ProducesResponseType(typeof(UserResource), StatusCodes.Status201Created)]
        public async Task<IActionResult> RegisterDoctor([FromForm] ClientModel user)
        {
            var client = await _clientsManager.RegisterClient(user);
            return Created();
        }

        [HttpGet("{Id}", Name = "GetClientById")]
        [ProducesResponseType(typeof(UserResource), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetClientById(string Id)
        {
            var client = await _clientsManager.GetClientById(Id);
            return Ok(client);
        }

        [HttpPut("{Id}", Name = "UpdateClient")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> UpdateClient(string Id, [FromForm] UserModel user)
        {
            await _clientsManager.UpdateClient(Id, user);
            return NoContent();
        }


        [HttpGet(Name = "GetAllClients")]
        [ProducesResponseType(typeof(List<UserResource>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAllClients()
        {
            var client = await _clientsManager.GetAllClients();
            return Ok(client);
        }

        [HttpDelete("{Id}", Name = "DeleteClient")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> DeleteClient(string Id)
        {
            await _clientsManager.DeleteDoctor(Id);
            return NoContent();
        }
    }
}
