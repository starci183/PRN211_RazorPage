using DAOs.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public interface ICarRepository : IRepository<CarInformation>
{
    public List<CarInformation> GetCarsWithIncludes();
    public CarInformation? GetByIdWithIncludes(int id);
}
