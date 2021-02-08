using AMA.Models.Entities;
using System.Collections.Generic;

namespace AMA.Repositories.Interfaces
{
    public interface IRepositoryCity
    {
        void Insert(City city);
        IEnumerable<City> FindAll();
    }
}
