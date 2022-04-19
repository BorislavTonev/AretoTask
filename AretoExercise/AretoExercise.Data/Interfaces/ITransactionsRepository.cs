using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AretoExercise.Data.Interfaces
{
    public interface ITransactionsRepository
    {
       Task<int> AddTransactionToDb(string data);
    }
}
