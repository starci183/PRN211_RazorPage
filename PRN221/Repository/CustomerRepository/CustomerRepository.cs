using DAOs.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class CustomerRepository : ICustomerRepository
{   
    private readonly FucarRentingManagementContext _context;
    public CustomerRepository() {
        _context = new FucarRentingManagementContext();
    }
    public bool Add(Customer entity)
    {
        try
        {
           _context.Customers.Add(entity);
           _context.SaveChanges();
            return true;
        } catch (DbException ex)
        {
            Console.Write(ex.Message);
            return false;
        }
    }

    public bool Delete(Customer entity)
    {
        try
        {
            _context.Customers.Remove(entity);
            _context.SaveChangesAsync();

            return true;
        } catch (DbException ex)
        {
            Console.WriteLine(ex.Message);
            return false;
        }
    }

    public List<Customer> GetAll() => _context.Customers.ToList();

    public Customer? GetByEmailAndPassword(string email, string password)
    => _context.Customers.FirstOrDefault(
            row => row.Email == email && row.Password == password
            );

    public Customer? GetById(int id) => _context.Customers.FirstOrDefault(
        row => row.CustomerId == id
        );

    public bool Update(Customer entity)
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