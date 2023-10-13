using DAOs.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class RentingTransactionRepository : IRentingTransactionRepository
{
    private readonly FucarRentingManagementContext _context;
    public RentingTransactionRepository()
    {
        _context = new FucarRentingManagementContext();
    }
    public bool Add(RentingTransaction entity)
    {
        try
        {
            _context.RentingTransactions.Add(entity);
            _context.SaveChanges();
            return true;
        }
        catch (DbException ex)
        {
            Console.WriteLine(ex.Message);
            return false;
        }
    }

    public bool Delete(RentingTransaction entity)
    {
        try
        {
            _context.RentingTransactions.Remove(entity);
            _context.SaveChanges();
            return true;
        }
        catch (DbException ex)
        {
            Console.WriteLine(ex.Message);
            return false;
        }
    }

    public List<RentingTransaction> GetAll() => _context.RentingTransactions.ToList();

    public List<RentingTransaction> GetAllByCustomerId(int customerId) => _context.RentingTransactions
        .Include(row => row.Customer).Where(row => row.CustomerId == customerId).ToList();

    public List<RentingTransaction> GetAllIncludes() =>
        _context.RentingTransactions
        .Include(row => row.Customer)
        .ToList();

    public RentingTransaction? GetById(int id) => _context.RentingTransactions.FirstOrDefault(row => row.RentingTransationId == id);

    public RentingTransaction? GetByIdIncludes(int id) => _context.RentingTransactions.Include(row => row.Customer).Include(row => row.RentingDetails).FirstOrDefault(row => row.RentingTransationId == id);

    public bool Update(RentingTransaction entity)
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
}
