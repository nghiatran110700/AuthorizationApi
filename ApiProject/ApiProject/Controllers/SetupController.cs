using ApiProject.Configuration;
using ApiProject.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SetupController : ControllerBase
    {
        private readonly UserManager<IdentityUser> userManager;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly NccData nccData;
        private readonly ILogger<SetupController> ilogger;
        public SetupController(
            NccData nccData,
            UserManager<IdentityUser> userManager,
            RoleManager<IdentityRole> roleManager,
            ILogger<SetupController> ilogger
            )
        {
            this.ilogger = ilogger;
            this.nccData = nccData;
            this.userManager = userManager;
            this.roleManager = roleManager;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllRole()
        {
            var role = await roleManager.Roles.ToListAsync();
            return Ok(role);
        }

        [HttpPost]
        [Route("CreateRole")]
        public async Task<IActionResult> CreateRole( string name)
        {
            // checl if role exits
            var roleExits = await roleManager.RoleExistsAsync(name);
            if (!roleExits)
            {
                var roleResult = await roleManager.CreateAsync(new IdentityRole(name));
                // check reole is add success ??
                if (roleResult.Succeeded)
                {
                    ilogger.LogInformation($"The role {name} has been add successfully");
                    return Ok( new { 
                        result = $"the role {name} has been add successfully"
                    });
                }
                else
                {
                    ilogger.LogInformation($"The role {name} has been add error");
                    return BadRequest( new { 
                        error  = $"The role {name} has been add error"
                    });
                }
            }
            return BadRequest(new { error = "Role is ready exits"});
        }

        [HttpGet]
        [Route("getAllUser")]
        public async Task<IActionResult> GetAllUser()
        {
            var user = await userManager.Users.ToListAsync();
            return Ok(user);
        }

        [HttpPost]
        [Route("addUserToRole")]
        public async Task<IActionResult> AddUserToRole(string email, string roleName)
        {
            //check user is exist
            var user = await userManager.FindByEmailAsync(email);
            if (user == null) // User does not exist
            {
                ilogger.LogInformation($"The user with the {email} does not exist");
                return BadRequest(new
                {
                    error = "User does not exist"
                });
            }

            var roleExits = await roleManager.FindByNameAsync(roleName);
            if(roleExits == null)
            {
                ilogger.LogInformation($"The role with the {roleName} does not exist");
                return BadRequest(new
                {
                    error = "Role does not exist"
                });
            }

            var result = await userManager.AddToRoleAsync(user, roleName) ;
            if (result.Succeeded)
            {
                ilogger.LogInformation($"The add user with {email} to {roleName} is successully ");
                return BadRequest(new
                {
                    success = $"The add user to { roleName} is successully"
                });
            }
            else
            {
                ilogger.LogInformation($"The add user with {email} to {roleName} is error ");
                return BadRequest(new
                {
                    error = $"The add user to { roleName} is error"
                });
            }
        }
        [HttpGet]
        [Route("GetUserRole/{email}")]
        public async Task<IActionResult> GetUserRole(string email)
        {
            //check user is exist
            var user = await userManager.FindByEmailAsync(email);
            if (user == null) // User does not exist
            {
                ilogger.LogInformation($"The user with the {email} does not exist");
                return BadRequest(new
                {
                    error = "User does not exist"
                });
            }
            var role = await userManager.GetRolesAsync(user);
            return Ok(role);
        }
    }
}
