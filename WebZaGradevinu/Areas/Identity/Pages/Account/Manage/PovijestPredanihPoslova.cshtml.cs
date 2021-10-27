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
        public List<Jobs> Job { get; set; }   //s time šaljemo u model ono što hocemo prikazat

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
        public async Task<IActionResult> OnGetAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }
            Job = _dbContext.JobsListing.Include(y => y.Company).Include(y => y.City).Include(y=>y.AcceptedJobs).Where(y => y.Company.Email == user.Email).ToList();
            return Page();
        }
    }
}
