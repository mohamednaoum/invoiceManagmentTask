using InvoiceManagement.Web.Models;
using InvoiceManagement.Web.Services;
using Microsoft.AspNetCore.Mvc;

namespace InvoiceManagement.Web.Controllers;

public class AccountController : Controller
{
    private readonly ApiService _apiService;

    public AccountController(ApiService apiService)
    {
        _apiService = apiService;
    }

    public IActionResult Login()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Login(string username, string password)
    {
        var token = await _apiService.AuthenticateAsync(username, password);
        if (!string.IsNullOrEmpty(token))
        {
            HttpContext.Response.Cookies.Append("AuthToken", token, new CookieOptions { HttpOnly = true });
            return RedirectToAction("Create", "Invoice");
        }

        ModelState.AddModelError(string.Empty, "Invalid login attempt");
        return View();
    }

    public IActionResult Register()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Register(string username, string password, string email)
    {
        var result = await _apiService.RegisterAsync(new RegisterViewModel()
        {
            UserName = username,
            Password = password,
            Email = email
            
        });
        if (result)
        {
            return RedirectToAction("Login");
        }

        ModelState.AddModelError(string.Empty, "Registration failed");
        return View();
    }
}