using Microsoft.AspNetCore.Mvc;
using Assignment_Friday_.Models;
using Microsoft.AspNetCore.Identity;
using Assignment_Friday_.Areas.Identity.Pages.Account;
namespace Assignment_Friday_.Controllers;

public class EmailController : Controller
{
    private UserManager<IdentityUser> userManager;
    public EmailController(UserManager<IdentityUser> usrMgr)
    {
        userManager = usrMgr;
    }

    public async Task<IActionResult> ConfirmEmail(string token, string email)
    {
        var user = await userManager.FindByEmailAsync(email);
        if (user == null)
            return View("Error");

        var result = await userManager.ConfirmEmailAsync(user, token);
        return View(result.Succeeded ? "ConfirmEmail" : "Error");
    }
}
