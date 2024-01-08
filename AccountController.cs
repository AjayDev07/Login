using LoginWithCrud.Data;
using LoginWithCrud.Models;
using LoginWithCrud.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace LoginWithCrud.Controllers
{
    public class AccountController : Controller
    {
        private readonly ApplicationDbContext _context;
        public AccountController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Dashboard()
        {
            return View();
        }

        public IActionResult Index()
        {
            return View();
        }
        [HttpPost] 
        public IActionResult Index(LoginViewModel loginViewModel)
        {
            if (ModelState.IsValid)
            {
                var login = new Login
                {
                    UserName = loginViewModel.UserName,
                    Password = loginViewModel.Password
                };
                _context.Logins.Add(login); 
                _context.SaveChanges(); 
                return RedirectToAction("Login");
            }
            return View(loginViewModel);
        }



        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(LoginViewModel loginViewModel)
        {
            if (ModelState.IsValid)
            {
                
                var user = _context.Logins.FirstOrDefault(u => u.UserName == loginViewModel.UserName);

                
                if (user != null && user.Password == loginViewModel.Password)
                {
                    
                    return RedirectToAction("Index"); 
                }

                
                ModelState.AddModelError(string.Empty, "Invalid username or password");
            }

            
            return View(loginViewModel);
        }

    }
}
