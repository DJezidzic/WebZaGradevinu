using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebZaGradevinu.Data;

namespace WebZaGradevinu.Areas.Identity.Pages.Account.Manage
{
    public partial class IndexModel : PageModel
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly WebAppDbContext _dbContext;

        public IndexModel(
            UserManager<AppUser> userManager,
            WebAppDbContext dbContext,
            SignInManager<AppUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _dbContext = dbContext;
        }

        public string Username { get; set; }

        [TempData]
        public string StatusMessage { get; set; }

        [BindProperty]
        public InputModel Input { get; set; }

        public class InputModel
        {
            [Phone]
            [Display(Name = "Phone number")]
            public string PhoneNumber { get; set; }
            [Display(Name ="Adresa")]
            public string Adress { get; set; }
            [Display(Name ="Grad")]
            public string Grad { get; set; }
        }

        private async Task LoadAsync(AppUser user)
        {
            var userName = await _userManager.GetUserNameAsync(user);
            var phoneNumber = await _userManager.GetPhoneNumberAsync(user);
            var Adress = user.Adress;
            var Grad = user.Grad;
            Username = userName;

            Input = new InputModel
            {
                PhoneNumber = phoneNumber,
                Adress = Adress,
                Grad = Grad
            };
        }

        public async Task<IActionResult> OnGetAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            await LoadAsync(user);
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            Company company = new Company();

            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            if (!ModelState.IsValid)
            {
                await LoadAsync(user);
                return Page();
            }

            

            var grad = user.Grad;
            var adresa = user.Adress;
            int cityid;

            if(Input.Grad != grad)
            {
                user.Grad = Input.Grad;
                try
                {
                    cityid = this._dbContext.Cities.Where(x => x.Name == user.Grad).Select(y => y.ID).FirstOrDefault();

                }
                catch (Exception)
                {

                    return NotFound("Uneseni grad ne postoji");
                }
                company.CityId = cityid;
                this._dbContext.Companies.Update(company);
                await _userManager.UpdateAsync(user);
                await _dbContext.SaveChangesAsync();
            }

            if (Input.Adress != adresa)
            {
                user.Adress = Input.Adress;
                company.Adresa = user.Adress;
                this._dbContext.Companies.Update(company);
                await _userManager.UpdateAsync(user);
                await _dbContext.SaveChangesAsync();
            }

            var phoneNumber = await _userManager.GetPhoneNumberAsync(user);
            if (Input.PhoneNumber != phoneNumber)
            {
                var setPhoneResult = await _userManager.SetPhoneNumberAsync(user, Input.PhoneNumber);
                company.PhoneNumber = Input.PhoneNumber;
                if (!setPhoneResult.Succeeded)
                {
                    StatusMessage = "Unexpected error when trying to set phone number.";
                    return RedirectToPage();
                }
                this._dbContext.Companies.Update(company);
                await _dbContext.SaveChangesAsync();
            }

            await _signInManager.RefreshSignInAsync(user);
            StatusMessage = "Your profile has been updated";
            return RedirectToPage();
        }
    }
}
