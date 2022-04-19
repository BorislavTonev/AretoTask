using AretoExercise.Data.Interfaces;
using AretoExercise.Domain;
using System;
using System.Threading.Tasks;

namespace AretoExercise.Data.Repository
{
    public class TransactionsRepository : ITransactionsRepository
    {
        private readonly AretoDBContext _context;

        public TransactionsRepository(AretoDBContext context)
        {
            this._context = context;
        }
        public  async Task<int> AddTransactionToDb(string data)
        {
            var newRec = new TransactionDbEntity()
            {
                JsonData = data
            };

            await _context.Transactions.AddAsync(newRec);
            return  await _context.SaveChangesAsync();
        }
    }
}
