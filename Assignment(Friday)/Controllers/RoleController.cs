﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Rendering;
using Assignment_Friday_.Models;

namespace Assignment_Friday_.Controllers
{
    public class RoleController : Controller
    {
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly UserManager<IdentityUser> userManager;

        public RoleController(RoleManager<IdentityRole> roleManager, UserManager<IdentityUser> userManager)
        {
            this.roleManager = roleManager;
            this.userManager = userManager;
        }

        public IActionResult Index()
        {
            var roles = roleManager.Roles.ToList();
            return View(roles);
        }

        public IActionResult Create() 
        {
            var role = new IdentityRole();
            return View(role);
        }

        [HttpPost]

        public async Task<IActionResult> Create(IdentityRole role) 
        {
            var isRoleExist = await roleManager.RoleExistsAsync(role.Name);
            if (isRoleExist) 
            {
                ViewBag.Message = $"Role {role.Name} is already exist";
                return View(role);
            }

            var result = await roleManager.CreateAsync(role);
            if (result.Succeeded) 
            {
                return RedirectToAction("Index");
            }
            ViewBag.Message = $"Role {role.Name} Creation failed";
            return View(role);
        }

        public IActionResult AssignRoleToUser() 
        {
            var roles = roleManager.Roles;
            List<SelectListItem> roleItems = new List<SelectListItem>();
            foreach (var role in roles) 
            {
                roleItems.Add(new SelectListItem(role.Name, role.Id));
            }
            ViewBag.Roles = roleItems;

            var users = userManager.Users;
            List<SelectListItem> userItems = new List<SelectListItem>();
            foreach (var user in users) 
            {
                userItems.Add(new SelectListItem(user.UserName, user.Id));
            }
            ViewBag.Users = userItems;
            return View(new UserInRole());
        }

        [HttpPost]
        public async Task<IActionResult> AssignRoleToUser(UserInRole userInRole) 
        {
            if(userInRole == null || userInRole.UserId == null || userInRole.RoleId == null) 
            {
                var roles = roleManager.Roles;

                List<SelectListItem> roleItems = new List<SelectListItem>();
                foreach (var role in roles) 
                {
                    roleItems.Add(new SelectListItem(role.Name, role.Id));
                }
                ViewBag.Roles = roleItems;

                var users = userManager.Users;
                List<SelectListItem> usersItems = new List<SelectListItem>();
                foreach (var user in users) 
                {
                    usersItems.Add(new SelectListItem(user.UserName, user.Id));
                }
                ViewBag.Users = usersItems;

                return View(new UserInRole());
            }

            var UserExist = await userManager.FindByIdAsync(userInRole.UserId);
            var RoleExist = await roleManager.FindByIdAsync(userInRole.RoleId);

            if (UserExist == null || RoleExist == null) 
            {
                ViewBag.UserStatus = $"The User {userInRole.UserId} does not exist";
                ViewBag.RoleStatus = $"The User {userInRole.RoleId} does not exist";
                return View(new UserInRole());
            }

            var result = await userManager.AddToRoleAsync(UserExist, RoleExist.Name);

            if (result.Succeeded)
                return RedirectToAction("Index");
            return View(new UserInRole());
        }
    }
}
