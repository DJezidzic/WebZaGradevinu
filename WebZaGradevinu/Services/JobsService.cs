using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebZaGradevinu.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.IO;

namespace WebZaGradevinu.Services
{
    public class JobsService
    {
        private readonly WebAppDbContext _dbcontext;
        public JobsService(WebAppDbContext _db)
        {
            _dbcontext = _db;
        }

        public async Task OnNewOfferCreate(Jobs job)
        {

        }

        public List<Jobs> displayJobs()
        {
            //Treba sredit da se pokazuju samo poslovi koji nisu vezani za firmu koja traži poslove
            return _dbcontext.JobsListing.ToList();
        }

    }
}
