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

        public async Task OnNewOfferCreate(Jobs job, string UserName)
        {
            try
            {
                var company = this._dbcontext.Companies.Where(x => x.Email == UserName).Select(y => y.ID).FirstOrDefault();
                job.AktivanOglas = true;
                job.CompanyId = company;
                await _dbcontext.JobsListing.AddAsync(job);
                await _dbcontext.SaveChangesAsync();

            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task OnDelete(int jobid)
        {
            var jobselected = await _dbcontext.JobsListing.FindAsync(jobid);
            jobselected.AktivanOglas = false;
            try
            {
                _dbcontext.JobsListing.Update(jobselected);
                await _dbcontext.SaveChangesAsync();

            }
            catch (Exception)
            {

                throw;
            }

        }

        public async Task OnEditOffer(Jobs job)
        {

            try
            {
                _dbcontext.JobsListing.Update(job);
                await _dbcontext.SaveChangesAsync();
            }
            catch (Exception)
            {

                throw;
            }
        }

        public List<Jobs> DisplayMyJobs(string UserName)
        {
            ///Trebalo bi još vidjet i prikazat poslove koji se trenutno obavljaju, ali mozda ispod aktivnih poslova kao zasebna tablica
            return _dbcontext.JobsListing.Include(c=>c.Company).Where(x => x.Company.Email == UserName && x.AktivanOglas == true).ToList();
        }

        public List<Jobs> displayJobs(string UserName)
        {
            //Treba sredit da se pokazuju samo poslovi koji nisu vezani za firmu koja traži poslove
            return _dbcontext.JobsListing.Include(c=>c.AcceptedJobs).Where(x=> x.Company.Email != UserName && (x.AktivanOglas == true && x.AcceptedJobs.JobId != x.ID)).ToList();
        }

        public List<City> GetCities()
        {
            return _dbcontext.Cities.ToList();
        }

        public Jobs GetJobForEdit(int id)
        {
            return _dbcontext.JobsListing.Where(x => x.ID == id).FirstOrDefault();
        }

        public async Task AcceptJob(Jobs job, string UserName)
        {
            var company = this._dbcontext.Companies.Where(x => x.Email == UserName).Select(y => y.ID).FirstOrDefault();
            AcceptedJobs acjob = new();

            try
            {
                acjob.CompanyId = company;
                acjob.JobId = job.ID;
                acjob.AcceptedTime = DateTime.Now;
                acjob.JobisFinished = false;
                await _dbcontext.AcceptedJobsList.AddAsync(acjob);
                await _dbcontext.SaveChangesAsync();

            }
            catch (Exception)
            {

                throw;
            }

        }

        public List<AcceptedJobs> GetAcceptedJobsList(string Username)
        {
            var company = _dbcontext.Companies.Where(x => x.Email == Username).Select(x => x.ID).FirstOrDefault();
            return _dbcontext.AcceptedJobsList.Where(x => x.CompanyId == company).ToList();
        }

        public async Task FinishJob(AcceptedJobs accjob)
        {
            try
            {
                accjob.JobisFinished = true;
                accjob.FinishedTime = DateTime.Now;
                _dbcontext.AcceptedJobsList.Update(accjob);
                await _dbcontext.SaveChangesAsync();
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task DeleteFinishListJob(AcceptedJobs accjob)
        {

            try
            {
                _dbcontext.AcceptedJobsList.Remove(accjob);
                await _dbcontext.SaveChangesAsync();
            }
            catch (Exception)
            {

                throw;
            }
        }

    }
}
