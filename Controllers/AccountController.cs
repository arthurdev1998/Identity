using Identity.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Identity.Controllers;

[Route("[controller]")]
public class AccountController : Controller
{

    private readonly UserManager<IdentityUser> _userManager;
    private readonly SignInManager<IdentityUser> _signInManager;

    public AccountController(
        UserManager<IdentityUser> userManager
        , SignInManager<IdentityUser> signInManager)
    {
        _userManager = userManager;
        _signInManager = signInManager;
    }

    [HttpGet]
    [Route("account/register")]
    public IActionResult Register()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Register(RegisterViewModel src)
    {
        // Copia os dados do RegisterViewModel para o IdentityUser
        var user = new IdentityUser
        {
            UserName = src.Email,
            Email = src.Email,
        };

        // Armazena os dados do usuário na tabela AspNetUsers
        var result = await _userManager.CreateAsync(user, src.Password);

        // Se o usuario foi criado com sucesso, faz o login do usuario
        // usando o serviço SignInManager e redireciona para o Método Action Index
        if (result.Succeeded)
        {
            await _signInManager.SignInAsync(user, isPersistent: false);
            return RedirectToAction("Index", "home"); // Método Index do controlador Home
        }

        // Se houver erros então inclui no ModelState
        // que será exibido pela tag helper summary na validação
        foreach (var error in result.Errors)
        {
            ModelState.AddModelError(string.Empty, error.Description);
        }

        return View(src);
    }

    [HttpGet]
    public IActionResult Login()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Login(LoginViewModel model)
    {
        if (ModelState.IsValid)
        {
            var result = await _signInManager.PasswordSignInAsync(
                model.Email, model.Password, model.RememberMe, false);

            if (result.Succeeded)
            {
                return RedirectToAction("index", "home");
            }

            ModelState.AddModelError(string.Empty, "login Inválido");
        }

        return View(model);
    }
}