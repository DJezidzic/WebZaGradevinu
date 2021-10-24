using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using WebZaGradevinu.Data;

namespace WebZaGradevinu.Areas.Identity.Pages.Account.Manage
{
    public class PovijestPredanihPoslovaModel : PageModel
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly WebAppDbContext _dbContext;

        public PovijestPredanihPoslovaModel(
            UserManager<AppUser> userManager,
            WebAppDbContext dbContext,
            SignInManager<AppUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _dbContext = dbContext;
        }

        [BindProperty]
        public OutputModel Output { get; set; }

        public class OutputModel
        {
            [Display(Name ="NazivPosla")]
            public string NazivPosla { get; set; }
            [Display(Name = "Adresa")]
            public string Adresa { get; set; }
            [Display(Name ="Grad")]
            public string Grad { get; set; }
            [Display(Name ="Opis")]
            public string Opis { get; set; }
        }

        public async Task LoadAsync(AppUser user)
        {
            var naziv = await _dbContext.JobsListing.Include(c => c.Company).Where(x => x.Company.Email == user.Email).Select(x=>x.Name).FirstOrDefaultAsync();
            var adresa = await _dbContext.JobsListing.Include(c => c.Company).Where(x => x.Company.Email == user.Email).Select(x => x.Adresa).FirstOrDefaultAsync();
            var gradid = await _dbContext.JobsListing.Include(c => c.Company).Where(x => x.Company.Email == user.Email).Select(x => x.CityId).FirstOrDefaultAsync();
            var grad = await _dbContext.Cities.Where(x => x.ID == gradid).Select(x => x.Name).FirstOrDefaultAsync();
            var opis = await _dbContext.JobsListing.Include(c => c.Company).Where(x => x.Company.Email == user.Email).Select(x => x.Description).FirstOrDefaultAsync();

            Output = new OutputModel
            {
                NazivPosla = naziv,
                Adresa = adresa,
                Grad = grad,
                Opis = opis
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
    }
}
