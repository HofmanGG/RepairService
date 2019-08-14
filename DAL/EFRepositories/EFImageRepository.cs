using System;
using System.Collections.Generic;
using System.Text;
using DAL.Сontexts;
using Microsoft.EntityFrameworkCore;

namespace DAL.EFRepositories
{
    public class EfImageRepository
    {
        private readonly SocNetwContext _context;

        public EfImageRepository(DbContext dbContext) => _context = dbContext as SocNetwContext;



    }
}
