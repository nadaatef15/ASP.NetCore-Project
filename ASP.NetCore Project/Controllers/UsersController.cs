using BusinessLogic.Managers.Identity;
using BusinessLogic.Resources;
using Microsoft.AspNetCore.Mvc;

namespace ASP.NetCore_Project.Controllers
{
    public class UsersController : BaseController
    {
        IUserManager userManager;
        public UsersController(IUserManager _userManager) =>
            userManager = _userManager;


        [HttpPost("AssignRolesToUser/{Id}", Name = "AssignRolesToUser")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<IActionResult> AssignRolesToUser(string Id, List<string> roleName)
        {
            await userManager.AssignRolesToUser(Id, roleName);
            return Created();
        }

        [HttpGet("{Id}", Name = "GetUserById")]
        [ProducesResponseType(typeof(UserResource), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetUserById(string Id)
        {
            var user = await userManager.GetUserById(Id);
            return Ok(user);
        }

        [HttpDelete("{Id}", Name = "DeleteUserById")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> DeleteUserById(string Id)
        {
            await userManager.DeleteUser(Id);
            return NoContent();
        }

        [HttpGet(Name = "GetAllUser")]
        [ProducesResponseType(typeof(UserResource), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAllUser()
        {
            var users = await userManager.GetAllUsers();
            return Ok(users);
        }
    }
}
