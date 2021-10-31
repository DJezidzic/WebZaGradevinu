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
    public class ImgUploadService
    {

        private readonly WebAppDbContext _dbcontext;
        public ImgUploadService(WebAppDbContext _db)
        {
            _dbcontext = _db;
        }
        public bool UploadImg(ImageLists imglis)
        {
            _dbcontext.ImageListing.Add(imglis);
            _dbcontext.SaveChanges();
            return true;
        }

        public List<ImageLists> displayImage()
        {
            return _dbcontext.ImageListing.ToList();
        }
        public List<ImageLists> DisplaySearchedImages(string search)
        {
            return _dbcontext.ImageListing.Where(x => x.ImageName.ToLower().Contains(search.ToLower())).ToList();
        }

        public async Task OnDelete(int imgid)
        {
            var imgselected = await _dbcontext.ImageListing.FindAsync(imgid);
            try
            {
                _dbcontext.ImageListing.Remove(imgselected);
                await _dbcontext.SaveChangesAsync(); 
            }
            catch (Exception)
            {
                throw;
            }    
        }

    }
}
