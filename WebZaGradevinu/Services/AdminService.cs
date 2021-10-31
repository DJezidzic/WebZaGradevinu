using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebZaGradevinu.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.IO;

namespace WebZaGradevinu.Services
{
    public class AdminService
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<AppUser> _userManager;
        public AdminService(RoleManager<IdentityRole> roleManager, UserManager<AppUser> userManager)
        {
            _roleManager = roleManager;
            _userManager = userManager;
        }

        public class UserWithRolesModel
        {
            public string UserId { get; set; }
            public string NazivTvrtke { get; set; }
            public string UserName { get; set; }
            public string Email { get; set; }
            public IEnumerable<string> Roles { get; set; }
        }

        public class ManageUserRolesModel
        {
            public string RoleId { get; set; }
            public string RoleName { get; set; }
            public bool SelectedAdmin { get; set; }
        }

        public List<IdentityRole> GetRoles()
        {
            return _roleManager.Roles.ToList();
        }
        public async Task AddRole(string roleName)
        {
            if(roleName != null)
            {
                await _roleManager.CreateAsync(new IdentityRole(roleName.Trim()));
            }
        }

        public List<AppUser> GetUsers()
        {
            return _userManager.Users.ToList();
        }
        public async Task<List<string>> GetRole(AppUser user)
        {
            var role = await _userManager.GetRolesAsync(user);
            return new List<string> (role);
        }
        /*private async Task<List<string>> GetRolesForUser(AppUser user)
        {
            return new List<string> (await _userManager.GetRolesAsync(user));
        }*/

        /*public async Task<List<UserWithRolesModel>> GetUsersWithRoles()
        {
            var users = await _userManager.Users.ToListAsync();
            var usersWithRoles = new List<UserWithRolesModel>();
            foreach (AppUser user in users)
            {
                var thisModel = new UserWithRolesModel();
                thisModel.UserId = user.Id;
                thisModel.Email = user.Email;
                thisModel.NazivTvrtke = user.NazivTvrtke;
                thisModel.Roles = await GetRolesForUser(user);
                usersWithRoles.Add(thisModel);
            }
            return usersWithRoles;
        }*/

        /*public async Task<List<ManageUserRolesModel>> GetManageUserRole(AppUser user)
        {
            var userRoleModel = new List<ManageUserRolesModel>();
                try
                {
                    foreach (var role in GetRoles())
                    {
                        var userRolesModel = new ManageUserRolesModel
                        {
                            RoleId = role.Id,
                            RoleName = role.Name
                        };
                        var result = await _userManager.IsInRoleAsync(user, role.Name);
                        if (result) //tu nastaje greška
                        {
                            userRolesModel.Selected = true;
                        }
                        else
                        {
                            userRolesModel.Selected = false;
                        }
                        userRoleModel.Add(userRolesModel);
                    }
                }
                catch (Exception ex)
                {
                    throw;
                }
            return userRoleModel;
        }*/
        public async Task<ManageUserRolesModel> GetManageUserRole(AppUser user)
        {

            var userRole = new ManageUserRolesModel();
            var result = await _userManager.IsInRoleAsync(user, "Admin");
            if (result)
            {
                var role = await _roleManager.FindByNameAsync("Admin");
                userRole.RoleId = role.Id;
                userRole.RoleName = role.Name;
                userRole.SelectedAdmin = true;
            }
            else
            {
                var role = await _roleManager.FindByNameAsync("User");
                userRole.RoleId = role.Id;
                userRole.RoleName = role.Name;
                userRole.SelectedAdmin = false;
            }
            return userRole;
            
        }

        public async Task EditRoles(ManageUserRolesModel model,AppUser user)
        {
            var role = await _userManager.IsInRoleAsync(user, "Admin");
            if(role == false && model.SelectedAdmin == true)
            {
                await _userManager.RemoveFromRoleAsync(user, "User");
                await _userManager.AddToRoleAsync(user, "Admin");
            }
            if(role == true && model.SelectedAdmin == false)
            {
                await _userManager.RemoveFromRoleAsync(user, "Admin");
                await _userManager.AddToRoleAsync(user, "User");
            }
            /*if (user != null)
            {
                var roles = await _userManager.GetRolesAsync(user);
                var result = await _userManager.RemoveFromRolesAsync(user, roles);
                result = await _userManager.AddToRolesAsync(user, model.Where(x => x.SelectedAdmin).Select(y => y.RoleName)); 
            }*/
        }


    }
}
