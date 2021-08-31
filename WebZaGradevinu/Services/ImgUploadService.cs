using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebZaGradevinu.Data;

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

    }
}
