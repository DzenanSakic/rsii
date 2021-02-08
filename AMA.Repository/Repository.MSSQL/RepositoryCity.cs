using AMA.Models.Entities;
using AMA.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace AMA.Repositories.Repository.MSSQL
{
    public class RepositoryCity : IRepositoryCity
    {
        private readonly DbSet<City> _cityContext;
        private readonly MSSQLDbContext _context;
        public RepositoryCity(MSSQLDbContext context)
        {
            _cityContext = context.Cities;
            _context = context;
        }

        public IEnumerable<City> FindAll()
        {
            return _cityContext.Include(x => x.Country).ToList();
        }

        public void Insert(City city)
        {
            _cityContext.Add(city);
            _context.SaveChanges();
        }
    }
}
