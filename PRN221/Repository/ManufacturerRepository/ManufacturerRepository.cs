using DAOs.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

    public class ManufacturerRepository : IManufacturerRepository
    {
     private readonly FucarRentingManagementContext _context;
    public ManufacturerRepository(FucarRentingManagementContext context)
    {
        _context = context;
    }

    public bool Add(Manufacturer entity)
    {
        throw new NotImplementedException();
    }

    public bool Delete(Manufacturer entity)
    {
        throw new NotImplementedException();
    }

    public List<Manufacturer> GetAll() => _context.Manufacturers.ToList();

    public Manufacturer? GetById(int id) => _context.Manufacturers.FirstOrDefault(row => row.ManufacturerId == id);

    public bool Update(Manufacturer entity)
    {
        throw new NotImplementedException();
    }
}

