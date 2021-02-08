using AMA.Models.Entities;
using System.Collections.Generic;

namespace AMA.Repositories.Interfaces
{
    public interface IRepositoryCountry
    {
        void Insert(Country country);
        IEnumerable<Country> FindAll();
    }
}
