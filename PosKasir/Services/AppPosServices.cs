using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using PosKasir.Entities.Entities.postGre;
using PosKasir.Enums;
using PosKasir.Models.Barang;
using PosKasir.Models.Cart;
using PosKasir.Models.Login;
using PosKasir.Models.Product;
using PosKasir.Models.Register;
using PosKasir.Models.Transaction;
using PosKasir.Models.UserTransaction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace PosKasir.Services
{
    public class AppPosServices
    {
        private readonly PostGreDbContext _postGreDbContext;
        private readonly IDistributedCache _CacheMan;
        private readonly string _CacheKey = "purchase";
        public List<PurchaseCartModel> Purchases = new List<PurchaseCartModel>();
        public bool IsLoaded = false;
        public string UserNames { get; set; } = UserEnum.name;

        /// <summary>
        /// Dependency Injection
        /// </summary>
        /// <param name="dbContext"></param>
        /// <param name="distributedCache"></param>
        public AppPosServices(PostGreDbContext dbContext, IDistributedCache distributedCache)
        {
            this._postGreDbContext = dbContext;
            this._CacheMan = distributedCache;
        }

        /// <summary>
        /// hashing password user
        /// digunakan saat register
        /// </summary>
        /// <param name="password"></param>
        /// <returns></returns>
        public string Hash(string password)
        {
            return BCrypt.Net.BCrypt.HashPassword(password, 12);
        }

        //---------------------- register-------
        /// <summary>
        /// check apakah username sudah ada
        /// dipakai saat register
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        public async Task<bool> CheckExistUsername(string username)
        {
            var CheckExist = await _postGreDbContext.TbUser
                .Where(Q => Q.UserName == username)
                .CountAsync();
            if (CheckExist > 0)
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// insert new user
        /// dipakai di register
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<bool> InsertUser(RegisterFormModel model)
        {
            this._postGreDbContext.TbUser
                .Add(new TbUser
                {
                    UserName = model.UserName,
                    UserPassword = Hash(model.Password),
                    UserRole = UserRole.User
                });
            await _postGreDbContext.SaveChangesAsync();
            return true;
        }

        //----------------------Login-------

        /// <summary>
        /// ambil data user berdasarkan username
        /// dipake di login,indexpurchase,insert,update,delete
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        public async Task<LoginDbModel> GetLogin(string username)
        {
            var getdata = await this._postGreDbContext.TbUser
                .Where(Q => Q.UserName == username)
                .Select(Q => new LoginDbModel
                {
                    UserId = Q.UserId,
                    UserName = Q.UserName,
                    UserPassword = Q.UserPassword,
                    UserRole = Q.UserRole
                })
                .FirstOrDefaultAsync();
            return getdata;
        }

        /// <summary>
        /// memeriksa password yg dihash sama belum di hash sama apa tidak
        /// dipake di login
        /// </summary>
        /// <param name="nothash"></param>
        /// <param name="hash"></param>
        /// <returns></returns>
        public bool Verify(string nothash, string hash)
        {
            //bcrypt install nuget -> BCrypt.Net-Next
            return BCrypt.Net.BCrypt.Verify(nothash, hash);
        }

        //----------------------Purchase & insert ke tb listalltransaction(data yg dibeli sementara)-------
        /// <summary>
        /// ambil data barang yg dibeli dari id user
        /// </summary>
        /// <param name="userid"></param>
        /// <returns></returns>
        public async Task FetcAllById(int userid)
        {
            if (this.IsLoaded)
            {
                return;
            }
            var all = await _CacheMan.GetStringAsync(this._CacheKey);
            this.IsLoaded = true;
            if (all == null)
            {
                return;
            }
            //ambil valuenya
            this.Purchases = JsonSerializer.Deserialize<List<PurchaseCartModel>>(all);
            this.Purchases = this.Purchases
            .Where(Q => Q.UserId == userid)
            .ToList();
        }


        //--------------insert ke DB transaction--------
        /// <summary>
        /// insert ke tb transaction
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<bool> InsertTransactionHeader(PurchaseModel model)
        {
            this._postGreDbContext.TbTransaction.Add(new TbTransaction
            {
                UserId = model.UserId,
                TransacionDate = model.PurchaseDate,
                TransactionId = model.TransactionID
            });
            return true;
        }


        public async Task<bool> InsertransactionDetail(PurchaseDetailModel model)
        {
            _postGreDbContext.TbTransactionDetail.Add(new TbTransactionDetail
            {
                SemenId = model.ProductId,
                Quantity = model.Quantity,
                TransactionId = model.TransactionId,
                UserId = model.UserId
            });
            Purchases = new List<PurchaseCartModel>();
            return true;
        }

        public async Task<bool> SaveToDatabase()
        {
            await this._postGreDbContext.SaveChangesAsync();
            return true;
        }
        //--------------insert ke cart--------

        /// <summary>
        /// menampilkan semua data product
        /// </summary>
        /// <returns></returns>
        public async Task<List<ProductViewModel>> GetAllProduct()
        {
            var getproduct = await this._postGreDbContext.TbSemen
                .Select(Q => new ProductViewModel
                {
                    ProductId = Q.SemenId,
                    ProductName = Q.SemenName,
                    Price = Q.Price,
                    Stock = Q.Stock
                }).ToListAsync();
            return getproduct;
        }

        /// <summary>
        /// mengecek apakah produk yg ingin dibeli sudah ada di keranjang belanja
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<bool> CheckProductExists(PurchaseFormModel model)
        {
            await FetcAllById(model.UserId);
            var exist = Purchases.Where(Q => Q.ProductId == model.ProductId).FirstOrDefault();
            if (exist != null)
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// ambil data semen dari id
        /// </summary>
        /// <param name="productId"></param>
        /// <returns></returns>
        public async Task<ProductViewModel> GetProductDataById(int productId)
        {
            var getProduct = await _postGreDbContext.TbSemen
    .Where(Q => Q.SemenId == productId)
    .Select(Q => new ProductViewModel
    {
        ProductName = Q.SemenName,
        Price = Q.Price,
        Stock = Q.Stock
    })
    .FirstOrDefaultAsync();

            return getProduct;
        }

        public async Task<bool> CheckStock(PurchaseFormModel model)
        {
            var getProduct = await GetProductDataById(model.ProductId);

            if (model.Quantity > getProduct.Stock)
            {
                return false;
            }

            return true;
        }

        private async Task SaveToCache(int userId)
        {
            await FetcAllById(userId);
            var serialized = JsonSerializer.Serialize(this.Purchases); //{ProductId:1}
            await _CacheMan.SetStringAsync(this._CacheKey, serialized);
        }

        /// <summary>
        /// insert ke cart
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task InsertCart(PurchaseFormModel model)
        {
            var getProduct = await GetProductDataById(model.ProductId);

            await FetcAllById(model.UserId);

            this.Purchases.Add(new PurchaseCartModel
            {
                UserId = model.UserId,
                ProductId = model.ProductId,
                ProductName = getProduct.ProductName,
                PurchaseCartDate = DateTimeOffset.Now,
                Quantity = model.Quantity
            });

            await SaveToCache(model.ProductId);
        }
        //----------- update -----------

        /// <summary>
        /// Ambil data cart(distributed cache), lalu disimpan dalam PurchaseFormModel
        /// </summary>
        /// <param name="productId"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public async Task<PurchaseFormModel> GetPurchaseByProductId(int productId, int userId)
        {
            await FetcAllById(userId);
            var findproduct = Purchases
                .Where(Q => Q.ProductId == productId)
                .FirstOrDefault();
            if (findproduct == null)
            {
                return new PurchaseFormModel();
            }

            var result = new PurchaseFormModel()
            {
                UserId = findproduct.UserId,
                ProductId = findproduct.ProductId,
                ProductName = findproduct.ProductName,
                Quantity = findproduct.Quantity
            };
            return result;
        }

        /// <summary>
        /// update cart data
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task UpdateCart(PurchaseFormModel model)
        {
            await FetcAllById(model.UserId);
            var findproduct = Purchases
                .Where(Q => Q.ProductId == model.ProductId)
                .FirstOrDefault();
            findproduct.Quantity = model.Quantity;
            findproduct.PurchaseCartDate = DateTime.Now;
            await SaveToCache(model.UserId);
        }

        //------- Delete Cart--------------
        public async Task DeleteCart(int productid, int userid)
        {
            await FetcAllById(userid);

            var deletedata = Purchases
                .Where(Q => Q.ProductId == productid)
                .FirstOrDefault();

            this.Purchases.Remove(deletedata);
            await SaveToCache(userid);
        }

        //---- listtransaction ----

        /// <summary>
        /// ambil data transaction menggunakan filter
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="itemPerPage"></param>
        /// <param name="filterbyUsername"></param>
        /// <returns></returns>
        public async Task<List<TransactionModel>> GetCreateAsync(int pageIndex, int itemPerPage, string filterbyUsername)
        {
            var query = this._postGreDbContext.TbTransaction
                .AsQueryable();
            if (string.IsNullOrEmpty(filterbyUsername) == false)
            {
                query = query
                    .Where(Q => Q.User.UserName.StartsWith(filterbyUsername));
            }
            var transaction = await query
                .Select(Q => new TransactionModel
                {
                    TransactionId = Q.TransactionId,
                    UserName = Q.User.UserName,
                    PurchaseDate = Q.TransacionDate
                })
                .AsNoTracking()
                .ToListAsync();

            transaction = transaction
                //.OrderBy(Q => Q.)
                .Skip((pageIndex - 1) * itemPerPage)
                .Take(itemPerPage)
                .ToList();
            return transaction;
        }

        /// <summary>
        /// hitung jumlah transaction
        /// </summary>
        /// <returns></returns>
        public int GetTotalData()
        {
            var totaldata = this._postGreDbContext
                .TbTransaction
                .Count();
            return totaldata;
        }

        //---- listtDetailransaction ----
        /// <summary>
        /// mendapatkan data detail transaction berdasar filter nama produk
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="itemPerPage"></param>
        /// <param name="filterbyproductname"></param>
        /// <param name="transactionid"></param>
        /// <returns></returns>
        public async Task<List<TransactionDetailModel>> GetCreateDetailAsync(int pageIndex, int itemPerPage, string filterbyproductname, string transactionid)
        {
            var query = this._postGreDbContext.TbTransactionDetail
                .Where(Q => Q.TransactionId == transactionid)
                .AsQueryable();
            if (string.IsNullOrEmpty(filterbyproductname) == false)
            {
                query = query
                    .Where(Q => Q.Semen.SemenName.StartsWith(filterbyproductname));
            }
            var detail = await query
                .Select(Q => new TransactionDetailModel
                {
                    UserName = Q.User.UserName,
                    ProductName = Q.Semen.SemenName,
                    Quantity = Q.Quantity,
                    TransactionId = Q.TransactionId,
                    TransactionDetailId = Q.TransactionDetailId
                })
                .AsNoTracking()
                .ToListAsync();

            detail = detail
                .OrderBy(Q => Q.TransactionDetailId)
                .Skip((pageIndex - 1) * itemPerPage)
                .Take(itemPerPage)
                .ToList();
            return detail;
        }

        public int GetTotalDetailData(string id)
        {
            var totaldata = this._postGreDbContext
                .TbTransactionDetail
                .Where(Q => Q.TransactionId == id)
                .Count();
            return totaldata;
        }

        //----------- InsertBarang ------

        /// <summary>
        /// melakukan insert barang (semen)
        /// </summary>
        /// <param name="id"></param>
        /// <param name="name"></param>
        /// <param name="price"></param>
        /// <param name="stock"></param>
        /// <returns></returns>
        public async Task<bool> InsertBarang(string name, decimal price, int stock)
        {
            this._postGreDbContext.TbSemen.Add(new TbSemen
            {
                SemenName = name,
                Price = price,
                Stock = stock
            });
            await this._postGreDbContext.SaveChangesAsync();
            return true;
        }

        /// <summary>
        /// Update barang (semen)
        /// </summary>
        /// <param name="id"></param>
        /// <param name="name"></param>
        /// <param name="price"></param>
        /// <param name="stock"></param>
        /// <returns></returns>
        public async Task<bool> UpdateBarang(int id, string name, decimal price, int stock)
        {
            var semen = await this._postGreDbContext
                .TbSemen
                .Where(Q => Q.SemenId == id)
                .FirstOrDefaultAsync();
            if (semen == null)
            {
                return false;
            }
            semen.SemenName = name;
            semen.Price = price;
            semen.Stock = stock;
            await this._postGreDbContext.SaveChangesAsync();
            return true;
        }

        /// <summary>
        /// Delete barang (semen)
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<bool> DeleteBarang(int id)
        {
            var semen = await this._postGreDbContext
                .TbSemen
                .Where(Q => Q.SemenId == id)
                .FirstOrDefaultAsync();
            if (semen == null)
            {
                return false;
            }
            this._postGreDbContext.Remove(semen);
            await this._postGreDbContext.SaveChangesAsync();
            return true;
        }

        public async Task<List<SemenModel>> GetBarangAsync(int pageIndex, int itemPerPage, string filterbyproductname)
        {
            var query = this._postGreDbContext.TbSemen
                .AsQueryable();
            if (string.IsNullOrEmpty(filterbyproductname) == false)
            {
                query = query
                    .Where(Q => Q.SemenName.StartsWith(filterbyproductname));
            }
            var barang = await query
                .Select(Q => new SemenModel
                {
                    BarangId = Q.SemenId,
                    BarangName = Q.SemenName,
                    BarangPrice = Q.Price,
                    BarangStock = Q.Stock
                })
                .AsNoTracking()
                .ToListAsync();

            barang = barang
                //.OrderBy(Q => Q.)
                .Skip((pageIndex - 1) * itemPerPage)
                .Take(itemPerPage)
                .ToList();
            return barang;
        }

        public int GetTotalbarang()
        {
            var totalsemen = this._postGreDbContext
                .TbSemen
                .Count();
            return totalsemen;
        }

        public async Task<SemenModel> GetSpecificSemen(int? semenid)
        {
            var semen = await this._postGreDbContext
                .TbSemen
                .Where(Q => Q.SemenId == semenid)
                .Select(Q => new SemenModel
                {
                    BarangId = Q.SemenId,
                    BarangName = Q.SemenName,
                    BarangPrice = Q.Price,
                    BarangStock = Q.Stock
                })
                .FirstOrDefaultAsync();
            return semen;
        }

        // ---- list transaction user -----
        public async Task<List<TransactionUserModel>> GetTransactionUserAsync(int pageIndex, int itemPerPage)
        {
            var data = await this._postGreDbContext
                .TbTransactionDetail
                .Where(Q => Q.User.UserName == UserNames)
                .Select(Q => new TransactionUserModel
                {
                    ProductName = Q.Semen.SemenName,
                    quantity = Q.Quantity,
                    PurchaseDate = Q.CreatedAt,
                    TransactionId = Q.Transaction.TransactionId,
                })
                .AsNoTracking()
                .ToListAsync();
            data = data
            //.OrderBy(Q => Q.)
            .Skip((pageIndex - 1) * itemPerPage)
            .Take(itemPerPage)
            .ToList();
            return data;
        }

        public int GetTotalTransactionUser()
        {
            var totaltransaction = this._postGreDbContext
                .TbTransactionDetail
                .Where(Q => Q.User.UserName == UserNames)
                .Count();
            return totaltransaction;
        }


    }
}
