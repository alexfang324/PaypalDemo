using PayPalDemo.Data;
using PayPalDemo.Repositories;
using PayPalDemo.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace PayPalDemo.Controllers
{
    //[Authorize(Roles = "Admin")]
    public class UserRoleController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public UserRoleController(ApplicationDbContext context,
                                 UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public ActionResult Index()
        {
            UserRepo userRepo = new UserRepo(_context);
            IEnumerable<UserVM> users = userRepo.GetAllUsers();

            return View(users);
        }

        public async Task<IActionResult> Detail(string userName,
                                                string message = "",
                                                bool success = false)
        {
            UserRoleRepo userRoleRepo = new UserRoleRepo(_userManager);
            MyRegisteredUserRepo registeredUserRepo = new MyRegisteredUserRepo(_context);


            var roles = await userRoleRepo.GetUserRolesAsync(userName);
            string userFullName = registeredUserRepo.GetUsernameFromEmail(userName);

            ViewBag.Message = message;
            ViewBag.UserName = userFullName;
            ViewBag.Email = userName;
            ViewBag.Success = success;


            return View(roles);
        }

        public ActionResult Create()
        {
            RoleRepo roleRepo = new RoleRepo(_context);
            ViewBag.RoleSelectList = roleRepo.GetRoleSelectList();


            UserRepo userRepo = new UserRepo(_context);
            ViewBag.UserSelectList = userRepo.GetUserSelectList();

            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(UserRoleVM userRoleVM)
        {
            UserRoleRepo userRoleRepo = new UserRoleRepo(_userManager);

            if (ModelState.IsValid)
            {
                try
                {
                    var addUR =
                    await userRoleRepo.AddUserRoleAsync(userRoleVM.Email,
                                                        userRoleVM.RoleName);

                    string message = $"{userRoleVM.RoleName} permissions" +
                                     $" successfully added to " +
                                     $"{userRoleVM.Email}.";

                    return RedirectToAction("Detail", "UserRole",
                                      new
                                      {
                                          userName = userRoleVM.Email,
                                          message = message,
                                          success = true
                                      });
                }
                catch
                {
                    ModelState.AddModelError("", "UserRole creation failed.");
                    ModelState.AddModelError("", "The Role may exist " +
                                                 "for this user.");
                }
            }

            RoleRepo roleRepo = new RoleRepo(_context);
            ViewBag.RoleSelectList = roleRepo.GetRoleSelectList();

            UserRepo userRepo = new UserRepo(_context);
            ViewBag.UserSelectList = userRepo.GetUserSelectList();

            return View();
        }

        public IActionResult Delete(string email, string roleName)
        {
            UserRoleVM viewModel = new UserRoleVM() { Email = email, RoleName = roleName };
            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(UserRoleVM viewModel)
        {
            string message;
            UserRoleRepo userRoleRepo = new UserRoleRepo(_userManager);
            bool success = await userRoleRepo.RemoveUserRoleAsync(viewModel.Email, viewModel.RoleName);

            if (success)
            {
                message = "Role deleted sucessfully";
            }
            else { message = "An error occured while deleting role"; }

            return RedirectToAction("Detail", new { username = viewModel.Email, message = message, success = success });
        }

    }
}
