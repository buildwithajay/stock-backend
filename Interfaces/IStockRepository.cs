using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Models;

namespace api.Interfaces
{
    public interface IStockRepository
    {
        Task<List<Stock>> GetAllAsync();

        Task<Stock?> GetById(int id);
        Task<Stock> Create();
        Task<Stock?> Update(int id);
        Task<Stock?> Delete(int id);
        

    }
}