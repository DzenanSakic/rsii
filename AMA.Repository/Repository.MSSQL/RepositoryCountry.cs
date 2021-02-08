using AMA.Models.Entities;
using AMA.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace AMA.Repositories.Repository.MSSQL
{
    public class RepositoryCountry : IRepositoryCountry
    {
        private readonly DbSet<Country> _countryContext;
        private readonly MSSQLDbContext _context;
        public RepositoryCountry(MSSQLDbContext context)
        {
            _countryContext = context.Countries;
            _context = context;
        }

        public IEnumerable<Country> FindAll()
        {
            return _countryContext.ToList();
        }

        public void Insert(Country country)
        {
            _countryContext.Add(country);
            _context.SaveChanges();
        }
    }
}
