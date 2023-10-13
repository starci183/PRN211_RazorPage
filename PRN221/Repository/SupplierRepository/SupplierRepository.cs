using DAOs.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class SupplierRepository : ISupplierRepository
{
    private readonly FucarRentingManagementContext _context;
    public SupplierRepository(FucarRentingManagementContext context)
    {
        _context = context;
    }
    public bool Add(Supplier entity)
    {
        throw new NotImplementedException();
    }

    public bool Delete(Supplier entity)
    {
        throw new NotImplementedException();
    }

    public List<Supplier> GetAll() => _context.Suppliers.ToList();

    public Supplier? GetById(int id) => _context.Suppliers.FirstOrDefault(row => row.SupplierId == id);

    public bool Update(Supplier entity)
    {
        throw new NotImplementedException();
    }
}
