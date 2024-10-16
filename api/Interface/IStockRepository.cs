using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos.Stock;
using api.Models;
using Microsoft.AspNetCore.Mvc;

namespace api.Interface
{
    public interface IStockRepository
    {
        Task<List<Stock>> GetAllAsyn();
        Task<Stock?> GetByIdAsync(int id);
        Task<Stock> CreateAsync(Stock stockmodel);
        Task<Stock?> UpdateAsync(int id, UpdateStockRequestDTO stockDto);
        Task<Stock> DeleteAsync(int id);
        Task<bool> StockExists(int id);
    }
}