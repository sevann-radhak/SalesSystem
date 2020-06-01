using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SalesSystem.Areas.Users.Models;
using SalesSystem.Data;
using SalesSystem.Helpers;
using SalesSystem.Library;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SalesSystem.Areas.Users.Pages.Account
{
    public class RegisterModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        private readonly IConverterHelper _converterHelper;
        private readonly IImageHelper _imageHelper;
        private readonly LUsersRoles _usersRoles;
        private readonly IUserHelper _userHelper;
        private readonly IWebHostEnvironment _environment;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        //private UserManager<IdentityUser> _userManager;
        private static InputModel _dataInput;

        public RegisterModel(
            ApplicationDbContext context,
            IConverterHelper converterHelper,
            IImageHelper imageHelper,
            IUserHelper userHelper,
            IWebHostEnvironment environment,
            RoleManager<IdentityRole> roleManager,
            SignInManager<IdentityUser> signInManager)
        //UserManager<IdentityUser> userManager)
        {
            _context = context;
            _converterHelper = converterHelper;
            _imageHelper = imageHelper;
            _userHelper = userHelper;
            _environment = environment;
            _roleManager = roleManager;
            _signInManager = signInManager;
            //_userManager = userManager;
            _usersRoles = new LUsersRoles();
        }

        public void OnGet()
        {
            if (_dataInput != null)
            {
                Input = _dataInput;
                Input.RolesList = _usersRoles.GetRoles(_roleManager);
                Input.AvatarImage = null;
            }
            else
            {
                Input = new InputModel { RolesList = _usersRoles.GetRoles(_roleManager) };
            }
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public class InputModel : InputModelRegister
        {
            public IFormFile AvatarImage { get; set; }

            public string ErrorMessage { get; set; }

            public List<SelectListItem> RolesList { get; set; }
        }

        public async Task<IActionResult> OnPost()
        {

            return await SaveAsync()
                ? Redirect("/Users/Users?area=Users")
                : Redirect("/Users/Register");
            //if (await SaveAsync())
            //{
            //    return Redirect("/Users/Users?area=Users");
            //}
            //return Redirect("/Users/Register");
        }

        private async Task<bool> SaveAsync()
        {
            _dataInput = Input;
            bool value = false;

            var prueba = Input;
            var prueba2 = ModelState;
            var stop = "hola";

            if (ModelState.IsValid)
            {
                IdentityUser userEmailExists = _userHelper.GetUserByEmail(Input.Email);

                if (userEmailExists == null)
                {
                    var strategy = _context.Database.CreateExecutionStrategy();
                    await strategy.ExecuteAsync(async () =>
                    {
                        using (var transaction = _context.Database.BeginTransaction())
                        {
                            var user = new IdentityUser
                            {
                                Email = Input.Email,
                                PhoneNumber = Input.PhoneNumber,
                                UserName = Input.Email
                            };

                            try
                            {
                                var result = await _userHelper.CreateAsync(user, Input.Password);
                                if (result.Succeeded)
                                {
                                    await _userHelper.AddToRoleAsync(user, Input.Role);
                                    var dataUser = _userHelper.GetUserByEmail(Input.Email);
                                    var imageByte = await _imageHelper.ByteAvatarImageAsync(
                                        Input.AvatarImage, 
                                        _environment, 
                                        "images/images/default.png");
                                    var tUser = _converterHelper.ToTUserModel(Input);
                                    tUser.Image = imageByte;
                                    tUser.IdUser = dataUser.Id;

                                    await _context.AddAsync(tUser);
                                    await _context.SaveChangesAsync();

                                    transaction.Commit();
                                    _dataInput = null;
                                    value = true;
                                }
                                else
                                {
                                    _dataInput.ErrorMessage = result.Errors.ToString();
                                    value = false;
                                }
                            }
                            catch (System.Exception ex)
                            {
                                _dataInput.ErrorMessage = ex.Message;
                                transaction.Rollback();
                                value = false;
                            }
                        }
                    });
                }
                else
                {
                    _dataInput.ErrorMessage = $"There email {Input.Email} already exists";
                    value = false;
                }
            }
            else
            {
                _dataInput.ErrorMessage = "Select a Role";
                value = false;
            }

            return value;
        }
    }
}
