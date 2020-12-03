using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PosKasir.Models;
using PosKasir.Models.Barang;
using PosKasir.Models.BarangSQLite;
using PosKasir.Models.Register;
using PosKasir.Models.Transaction;
using PosKasir.Models.UserTransaction;
using PosKasir.Services;

namespace PosKasir.API
{
    [AllowAnonymous]
    [Route("api/v1/create")]
    [ApiController]
    public class AppPosController : ControllerBase
    {
        private readonly AppPosServices _appposman;
        private readonly SqliteServices _sqliteservice;
        public AppPosController(AppPosServices appPosServices, SqliteServices sqlite)
        {
            this._appposman = appPosServices;
            this._sqliteservice = sqlite;
        }

        //---------transactionheader---------

        [HttpGet("filter-data")]
        public async Task<ActionResult<List<TransactionModel>>> GetFilterDataAsync([FromQuery] int pageindex, int itemperpage, string filterbyusername)
        {
            var data = await this._appposman.GetCreateAsync(pageindex, itemperpage, filterbyusername);
            return Ok(data);
        }

        [HttpGet("total-transaction")]
        public ActionResult<int> GetTotalData()
        {
            var totaldata = _appposman.GetTotalData();
            return Ok(totaldata);
        }

        //---------transactionDetail---------

        [HttpGet("filter-detaildata")]
        public async Task<ActionResult<List<TransactionDetailModel>>> GetFilterDetailDataAsync([FromQuery] int pageindex, int itemperpage, string filterbyusername, string transactionid)
        {
            var data = await this._appposman.GetCreateDetailAsync(pageindex, itemperpage, filterbyusername, transactionid);
            return Ok(data);
        }

        [HttpGet("total-detailtransaction")]
        public ActionResult<int> GetTotalDetailData([FromQuery] string id)
        {
            var totaldata = _appposman.GetTotalDetailData(id);
            return Ok(totaldata);
        }

        //-----InsertUser-----

        [HttpPost("insert", Name = "insertuser")]
        public async Task<ActionResult<ResponseModel>> InsertNewUserAsync([FromBody] RegisterFormModel usermodel)
        {
            //usermodel.Password = _appposman.Hash(usermodel.Password);

            var issuccess = await _appposman.InsertUser(usermodel);
            return Ok(new ResponseModel
            {
                ResponseMessage = "Registration success!"
            });
        }

        //------- Barang -------

        [HttpPost("insertsemen", Name = "insertsemen")]
        public async Task<ActionResult<ResponseModel>> InsertNewSemenAsync([FromQuery]string name, decimal price, int stock)
        {
            var isSuccess = await _appposman.InsertBarang(name, price, stock);
            return Ok(new ResponseModel
            {
                ResponseMessage = "Success Insert New Data"
            });
        }

        [HttpPut("update", Name = "updatesemen")]
        public async Task<ActionResult<ResponseModel>> UpdateSemenAsync([FromQuery]int id, string name, decimal price, int stock)
        {
            var isSuccess = await _appposman.UpdateBarang(id, name, price, stock);
            if (isSuccess == false)
            {
                return BadRequest(new ResponseModel
                {
                    ResponseMessage = "ID Not Found!!"
                });
            }

            return Ok(new ResponseModel
            {
                ResponseMessage = "Success Update Data!!"
            });
        }

        [HttpDelete("delete", Name = "deletesemen")]
        public async Task<ActionResult<ResponseModel>> DeleteSemenAsync([FromBody] SemenModel model)
        {
            var issucces = await _appposman.DeleteBarang(model.BarangId);
            if (issucces == false)
            {
                return BadRequest(new ResponseModel
                {
                    ResponseMessage = "ID Not Found!!"
                });
            }
            return Ok(new ResponseModel
            {
                ResponseMessage = "Success Delete data!!"
            });
        }

        [HttpGet("filter-barang")]
        public async Task<ActionResult<List<SemenModel>>> GetFilterBarangAsync([FromQuery] int pageIndex, int itemPerPage, string filterByName)
        {
            var data = await _appposman.GetBarangAsync(pageIndex, itemPerPage, filterByName);
            return Ok(data);
        }

        [HttpGet("total-barang")]
        public ActionResult<int> GetTotalBarang()
        {
            var totaldata = _appposman.GetTotalbarang();
            return Ok(totaldata);
        }

        [HttpGet("semen-specific", Name = "getspecificsemen")]
        public async Task<ActionResult<SemenModel>> GetSemenAsync(int? semenid)
        {
            if (semenid.HasValue == false)
            {
                return BadRequest(null);
            }
            var data = await _appposman.GetSpecificSemen(semenid.Value);
            if (data == null)
            {
                return BadRequest(null);
            }
            return Ok(data);
        }

        //------- list transaction user -----------
        [HttpGet("filter-Transactionusers", Name = "gettransactionuser")]
        public async Task<ActionResult<List<TransactionUserModel>>> GetuserTransaction([FromQuery]int pageIndex, int itemPerPage)
        {
            var data = await _appposman.GetTransactionUserAsync(pageIndex, itemPerPage);
            return Ok(data);
        }

        [HttpGet("total-transactionuser")]
        public ActionResult<int> GetTotalTransactionUser()
        {
            var totaldata = _appposman.GetTotalTransactionUser();
            return Ok(totaldata);
        }


        //-----  SQLITE ----------
        [HttpGet("semen-specific-sqlite", Name = "getsemenspecificssqlite")]
        public async Task<ActionResult<SemenSqliteModel>> GetSpecificSemenSqliteAsync(int? semenid)
        {
            if (semenid.HasValue == false)
            {
                return BadRequest(null);
            }
            var semen = await _sqliteservice.GetSpecificSemenAsync(semenid.Value);
            if (semen == null)
            {
                return BadRequest(null);
            }
            return Ok(semen);
        }

        [HttpGet("filter-semen-sqlite")]
        public async Task<ActionResult<List<SemenSqliteModel>>> GetFilterSemenSqliteAsync([FromQuery] int pageIndex, int itemPerPage, string filterByName)
        {
            var data = await _sqliteservice.GetAsync(filterByName, pageIndex, itemPerPage);
            return Ok(data);
        }

        [HttpPost("insertsemen-sqlite", Name = "insertsemensqlite")]
        public async Task<ActionResult<ResponseModel>> InsertnewSemenSqlite([FromQuery]string name, decimal price, int stock)
        {
            var isuccess = await _sqliteservice.InsertSemen(name, price, stock);
            return Ok(new ResponseModel
            {
                ResponseMessage = "Success insert new data"
            });
        }

        [HttpPut("updatesemen-sqlite", Name = "updatesemensqlite")]
        public async Task<ActionResult<ResponseModel>> UpdateSemenSqliteAsync([FromQuery]int id, string name, decimal price, int stock)
        {
            var issuccess = await _sqliteservice.UpdateSemen(id, name, price, stock);
            if (issuccess == false)
            {
                return BadRequest(new ResponseModel
                {
                    ResponseMessage = "ID Not Found!"
                });
            }

            return Ok(new ResponseModel
            {
                ResponseMessage = "Success update data"
            });
        }

        [HttpDelete("delete-semen-sqlite")]
        public async Task<IActionResult> DeleteSemenSqliteAsync([FromQuery] int semenid)
        {
            var issuccess = await _sqliteservice.DeleteSemenAsync(semenid);
            if (issuccess == false)
            {
                return BadRequest("ID Not Found");
            }
            return Ok(issuccess);
        }

        [HttpGet("total-data-sqlite")]
        public ActionResult<int> GetTotalDataSqlite()
        {
            var totaldata = _sqliteservice.TotalSemen();
            return Ok(totaldata);
        }

    }
}