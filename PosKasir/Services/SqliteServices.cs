using Microsoft.EntityFrameworkCore;
using PosKasir.Entities;
using PosKasir.Models.BarangSQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PosKasir.Services
{
    public class SqliteServices
    {
        private readonly AppPosDBContext _db;

        public SqliteServices(AppPosDBContext dBContext)
        {
            this._db = dBContext;
        }

        public async Task<bool> InsertSemen(string name, decimal price, int stock)
        {
            this._db.Add(new Semen
            {
                SemenName = name,
                Price = price,
                stock = stock,
                CreatedAt = DateTime.Now.ToString()
            }); ;
            await this._db.SaveChangesAsync();
            return true;
        }

        public async Task<List<SemenSqliteModel>> GetAsync(string filtername, int pageindex, int itemperpage)
        {
            var semen_query = this._db.Semens
                .AsQueryable();

            if (string.IsNullOrEmpty(filtername) == false)
            {
                semen_query = semen_query
                    .Where(Q => Q.SemenName.StartsWith(filtername));
            }

            var semens = await semen_query
                .Select(Q => new SemenSqliteModel
                {
                    SemenID = Q.SemenID,
                    SemenName = Q.SemenName,
                    SemenStock = Q.stock,
                    SemenPrice = Q.Price
                })
                .Skip((pageindex - 1) * itemperpage)
                .Take(itemperpage)
                 .AsNoTracking()
                 .ToListAsync();
            return semens;
        }

        public async Task<bool> UpdateSemen(int id, string name, decimal price, int stock)
        {
            var semen = await this._db
                .Semens
                .Where(Q => Q.SemenID == id)
                .FirstOrDefaultAsync();
            if (semen == null)
            {
                return false;
            }
            semen.SemenName = name;
            semen.Price = price;
            semen.stock = stock;
            semen.CreatedAt = DateTime.Now.ToString();

            await this._db.SaveChangesAsync();
            return true;
        }

        public async Task<SemenSqliteModel> GetSpecificSemenAsync(int? semenid)
        {
            var semen = await this._db
                .Semens
                .Where(Q => Q.SemenID == semenid)
                .Select(Q => new SemenSqliteModel
                {
                    SemenID = Q.SemenID,
                    SemenPrice = Q.Price,
                    SemenName = Q.SemenName,
                    SemenStock = Q.stock
                }).FirstOrDefaultAsync();
            return semen;
        }

        public async Task<bool> DeleteSemenAsync(int semenid)
        {
            var semen = await this._db
                .Semens
                .Where(Q => Q.SemenID == semenid)
                .FirstOrDefaultAsync();
            if (semen == null)
            {
                return false;
            }
            this._db.Remove(semen);
            await this._db.SaveChangesAsync();
            return true;
        }

        public int TotalSemen()
        {
            var totalsemen = this._db
               .Semens
               .Count();
            return totalsemen;
        }
    }
}
