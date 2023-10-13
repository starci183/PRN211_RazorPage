using DAOs.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class CarRepository : ICarRepository
{
    private readonly FucarRentingManagementContext _context;

    public CarRepository(FucarRentingManagementContext context)
    {
        _context = context;
    }

    public bool Add(CarInformation entity)
    {
        try
        {
            _context.CarInformations.Add(entity);
            _context.SaveChanges();
            return true;
        }
        catch (DbException ex)
        {
            Console.WriteLine(ex.Message);
            return false;
        }
    }

    public bool Delete(CarInformation entity)
    {
        try
        {
            _context.CarInformations.Remove(entity);
            _context.SaveChanges();
            return true;
        }
        catch (DbException ex)
        {
            Console.WriteLine(ex.Message);
            return false;
        }
    }

    public List<CarInformation> GetAll() => _context.CarInformations.ToList();

    public CarInformation? GetById(int id) => _context.CarInformations.FirstOrDefault(row => row.CarId == id);

    public bool Update(CarInformation entity)
    {
        try
        {
            _context.Update(entity);
            _context.SaveChanges();
            return true;
        }
        catch (DbException ex)
        {
            Console.WriteLine(ex.Message);
            return false;
        }
    }

    public List<CarInformation> GetCarsWithIncludes() =>
        _context.CarInformations.Include(c => c.Manufacturer)
                .Include(c => c.Supplier).ToList();

    public CarInformation? GetByIdWithIncludes(int id) => _context.CarInformations.Include(c => c.Manufacturer)
                .Include(c => c.Supplier).FirstOrDefault(row => row.CarId == id);
}