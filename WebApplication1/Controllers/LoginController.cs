using Dto;
using Microsoft.AspNetCore.Mvc;
using Servicios;
using WebApplication1.Exceptions;

namespace WebApplication1.Controllers
{
    public class LoginController : Controller
    {
        private IServicioLogin<LoginDto, LoginOutDto> _servicio;
        public LoginController(IServicioLogin<LoginDto, LoginOutDto> servicio)
        {
            _servicio = servicio;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Loginfail()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Logout()
        {
            HttpContext.Session.Remove("email");
            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public IActionResult Login(LoginDto loginDto)
        {

            try
            {
                LoginOutDto loginOutDto = _servicio.Login(loginDto);
                HttpContext.Session.SetString("email", loginOutDto.email);
                HttpContext.Session.SetString("token", loginOutDto.token);
            }
            catch (LoginException loginException)
            {
                return RedirectToAction("Loginfail");
            }
            return RedirectToAction("Index", "Home");
        }
    }
}
