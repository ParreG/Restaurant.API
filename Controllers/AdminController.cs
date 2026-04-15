using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RestrurantPG.DTOs.AdminDTOs;
using RestrurantPG.Models;
using RestrurantPG.Services.Interfaces;

namespace RestrurantPG.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly IAdminService adminService;

        public AdminController(IAdminService _adminService)
        {
            adminService = _adminService;
        }

        [HttpGet("GetAllInvites")]
        public async Task<IActionResult> GetAllInvitesAsync()
        {
            var invites = await adminService.GetAllInvitesAsync();
            if (!invites.Success)
            {
                return NotFound();
            }

            return Ok(invites.invites);
        }

        [HttpGet("GetAllAdmins")]
        public async Task<IActionResult> GetAllAdminsAsync()
        {
            var admins = await adminService.GetAllAdminsAsync();

            if (!admins.Success)
            {
                return NotFound(admins.Massange);
            }

            return Ok(admins.admins);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateAdminAsync(int id, AdminInfoDTO adminInfoDTO)
        {
            var admin = new Admin
            {
                Admin_Id = id,
                Email = adminInfoDTO.Email,
                UserName = adminInfoDTO.UserName,
            };

            var result = await adminService.UpdateAdminAsync(admin);

            if (!result.Success)
            {
                return NotFound(result.Massange);
            }

            return Ok(result.admin);
        }
    }
}
