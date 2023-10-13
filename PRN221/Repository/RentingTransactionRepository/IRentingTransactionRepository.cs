using DAOs.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

 public interface IRentingTransactionRepository : IRepository<RentingTransaction>
    {
        public List<RentingTransaction> GetAllByCustomerId(int customerId);

        public RentingTransaction? GetByIdIncludes(int id);

        public List<RentingTransaction> GetAllIncludes();
}
