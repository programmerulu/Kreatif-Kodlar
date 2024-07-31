using BlogProject.Data;
using BlogProject.Entites;
using BlogProject.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace BlogProject.Controllers
{
    public class AccountController : Controller
    {

        private readonly UserRepository _UserRepository;
        public AccountController(UserRepository UserRepository)
        {
            _UserRepository = UserRepository;
        }
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(User user)
        {
            if (user == null)
            {
                throw new Exception("Kullanıcı gönderilemedi");
            }
            var allUsers = await _UserRepository.GetAllAsync();
            var findedUser = allUsers.FirstOrDefault(u => u.Mail == user.Mail);
            if (findedUser == null)
            {
                throw new Exception("Kullanıcı bulunamadı");

            }

            if (user.Password != findedUser.Password || user.Mail != findedUser.Mail)
            {
                throw new Exception("Şifre veya mail hatalı");
            }

            var claims = new List<Claim>
    {
        new Claim(ClaimTypes.NameIdentifier, findedUser.Id.ToString()),

        new Claim(ClaimTypes.Name, findedUser.Mail)
    };

            var claimsIdentity = new ClaimsIdentity(claims, "MyCookieAuthenticationScheme");

            await HttpContext.SignInAsync("MyCookieAuthenticationScheme", new ClaimsPrincipal(claimsIdentity));

            return RedirectToAction(actionName: "Profile", controllerName: "Account", new {userId=findedUser.Id });




        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync("MyCookieAuthenticationScheme");
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public IActionResult Register()
        {
            User user = new User();
            return View(user);
        }

        [HttpPost]
        public async Task<IActionResult> Register(User user,IFormFile profilePicture)
        {
            await _UserRepository.CreateUserAsync(user, profilePicture);
            return RedirectToAction(actionName: "Index", controllerName: "Home");
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> Profile(int userId)
        {
            var user = await _UserRepository.GetById(userId);
            if (user == null)
            {
                return NotFound();
            }
            return View(user);
        }


    }
}
