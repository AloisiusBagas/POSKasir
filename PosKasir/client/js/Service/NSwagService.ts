﻿/* tslint:disable */
/* eslint-disable */
//----------------------
// <auto-generated>
//     Generated using the NSwag toolchain v13.2.5.0 (NJsonSchema v10.1.7.0 (Newtonsoft.Json v12.0.0.0)) (http://NSwag.org)
// </auto-generated>
//----------------------
// ReSharper disable InconsistentNaming

export class AppPosService {
    private http: { fetch(url: RequestInfo, init?: RequestInit): Promise<Response> };
    private baseUrl: string;
    protected jsonParseReviver: ((key: string, value: any) => any) | undefined = undefined;

    constructor(baseUrl?: string, http?: { fetch(url: RequestInfo, init?: RequestInit): Promise<Response> }) {
        this.http = http ? http : <any>window;
        this.baseUrl = baseUrl ? baseUrl : "";
    }

    /**
     * @param pageindex (optional) 
     * @param itemperpage (optional) 
     * @param filterbyusername (optional) 
     * @return Success
     */
    filterData(pageindex: number | undefined, itemperpage: number | undefined, filterbyusername: string | undefined): Promise<TransactionModel[]> {
        let url_ = this.baseUrl + "/api/v1/create/filter-data?";
        if (pageindex === null)
            throw new Error("The parameter 'pageindex' cannot be null.");
        else if (pageindex !== undefined)
            url_ += "pageindex=" + encodeURIComponent("" + pageindex) + "&"; 
        if (itemperpage === null)
            throw new Error("The parameter 'itemperpage' cannot be null.");
        else if (itemperpage !== undefined)
            url_ += "itemperpage=" + encodeURIComponent("" + itemperpage) + "&"; 
        if (filterbyusername === null)
            throw new Error("The parameter 'filterbyusername' cannot be null.");
        else if (filterbyusername !== undefined)
            url_ += "filterbyusername=" + encodeURIComponent("" + filterbyusername) + "&"; 
        url_ = url_.replace(/[?&]$/, "");

        let options_ = <RequestInit>{
            method: "GET",
            headers: {
                "Accept": "text/plain"
            }
        };

        return this.http.fetch(url_, options_).then((_response: Response) => {
            return this.processFilterData(_response);
        });
    }

    protected processFilterData(response: Response): Promise<TransactionModel[]> {
        const status = response.status;
        let _headers: any = {}; if (response.headers && response.headers.forEach) { response.headers.forEach((v: any, k: any) => _headers[k] = v); };
        if (status === 200) {
            return response.text().then((_responseText) => {
            let result200: any = null;
            result200 = _responseText === "" ? null : <TransactionModel[]>JSON.parse(_responseText, this.jsonParseReviver);
            return result200;
            });
        } else if (status !== 200 && status !== 204) {
            return response.text().then((_responseText) => {
            return throwException("An unexpected server error occurred.", status, _responseText, _headers);
            });
        }
        return Promise.resolve<TransactionModel[]>(<any>null);
    }

    /**
     * @return Success
     */
    totalTransaction(): Promise<number> {
        let url_ = this.baseUrl + "/api/v1/create/total-transaction";
        url_ = url_.replace(/[?&]$/, "");

        let options_ = <RequestInit>{
            method: "GET",
            headers: {
                "Accept": "text/plain"
            }
        };

        return this.http.fetch(url_, options_).then((_response: Response) => {
            return this.processTotalTransaction(_response);
        });
    }

    protected processTotalTransaction(response: Response): Promise<number> {
        const status = response.status;
        let _headers: any = {}; if (response.headers && response.headers.forEach) { response.headers.forEach((v: any, k: any) => _headers[k] = v); };
        if (status === 200) {
            return response.text().then((_responseText) => {
            let result200: any = null;
            result200 = _responseText === "" ? null : <number>JSON.parse(_responseText, this.jsonParseReviver);
            return result200;
            });
        } else if (status !== 200 && status !== 204) {
            return response.text().then((_responseText) => {
            return throwException("An unexpected server error occurred.", status, _responseText, _headers);
            });
        }
        return Promise.resolve<number>(<any>null);
    }

    /**
     * @param pageindex (optional) 
     * @param itemperpage (optional) 
     * @param filterbyusername (optional) 
     * @param transactionid (optional) 
     * @return Success
     */
    filterDetaildata(pageindex: number | undefined, itemperpage: number | undefined, filterbyusername: string | undefined, transactionid: string | undefined): Promise<TransactionDetailModel[]> {
        let url_ = this.baseUrl + "/api/v1/create/filter-detaildata?";
        if (pageindex === null)
            throw new Error("The parameter 'pageindex' cannot be null.");
        else if (pageindex !== undefined)
            url_ += "pageindex=" + encodeURIComponent("" + pageindex) + "&"; 
        if (itemperpage === null)
            throw new Error("The parameter 'itemperpage' cannot be null.");
        else if (itemperpage !== undefined)
            url_ += "itemperpage=" + encodeURIComponent("" + itemperpage) + "&"; 
        if (filterbyusername === null)
            throw new Error("The parameter 'filterbyusername' cannot be null.");
        else if (filterbyusername !== undefined)
            url_ += "filterbyusername=" + encodeURIComponent("" + filterbyusername) + "&"; 
        if (transactionid === null)
            throw new Error("The parameter 'transactionid' cannot be null.");
        else if (transactionid !== undefined)
            url_ += "transactionid=" + encodeURIComponent("" + transactionid) + "&"; 
        url_ = url_.replace(/[?&]$/, "");

        let options_ = <RequestInit>{
            method: "GET",
            headers: {
                "Accept": "text/plain"
            }
        };

        return this.http.fetch(url_, options_).then((_response: Response) => {
            return this.processFilterDetaildata(_response);
        });
    }

    protected processFilterDetaildata(response: Response): Promise<TransactionDetailModel[]> {
        const status = response.status;
        let _headers: any = {}; if (response.headers && response.headers.forEach) { response.headers.forEach((v: any, k: any) => _headers[k] = v); };
        if (status === 200) {
            return response.text().then((_responseText) => {
            let result200: any = null;
            result200 = _responseText === "" ? null : <TransactionDetailModel[]>JSON.parse(_responseText, this.jsonParseReviver);
            return result200;
            });
        } else if (status !== 200 && status !== 204) {
            return response.text().then((_responseText) => {
            return throwException("An unexpected server error occurred.", status, _responseText, _headers);
            });
        }
        return Promise.resolve<TransactionDetailModel[]>(<any>null);
    }

    /**
     * @param id (optional) 
     * @return Success
     */
    totalDetailtransaction(id: string | undefined): Promise<number> {
        let url_ = this.baseUrl + "/api/v1/create/total-detailtransaction?";
        if (id === null)
            throw new Error("The parameter 'id' cannot be null.");
        else if (id !== undefined)
            url_ += "id=" + encodeURIComponent("" + id) + "&"; 
        url_ = url_.replace(/[?&]$/, "");

        let options_ = <RequestInit>{
            method: "GET",
            headers: {
                "Accept": "text/plain"
            }
        };

        return this.http.fetch(url_, options_).then((_response: Response) => {
            return this.processTotalDetailtransaction(_response);
        });
    }

    protected processTotalDetailtransaction(response: Response): Promise<number> {
        const status = response.status;
        let _headers: any = {}; if (response.headers && response.headers.forEach) { response.headers.forEach((v: any, k: any) => _headers[k] = v); };
        if (status === 200) {
            return response.text().then((_responseText) => {
            let result200: any = null;
            result200 = _responseText === "" ? null : <number>JSON.parse(_responseText, this.jsonParseReviver);
            return result200;
            });
        } else if (status !== 200 && status !== 204) {
            return response.text().then((_responseText) => {
            return throwException("An unexpected server error occurred.", status, _responseText, _headers);
            });
        }
        return Promise.resolve<number>(<any>null);
    }

    /**
     * @param body (optional) 
     * @return Success
     */
    insertuser(body: RegisterFormModel | undefined): Promise<ResponseModel> {
        let url_ = this.baseUrl + "/api/v1/create/insert";
        url_ = url_.replace(/[?&]$/, "");

        const content_ = JSON.stringify(body);

        let options_ = <RequestInit>{
            body: content_,
            method: "POST",
            headers: {
                "Content-Type": "application/json",
                "Accept": "text/plain"
            }
        };

        return this.http.fetch(url_, options_).then((_response: Response) => {
            return this.processInsertuser(_response);
        });
    }

    protected processInsertuser(response: Response): Promise<ResponseModel> {
        const status = response.status;
        let _headers: any = {}; if (response.headers && response.headers.forEach) { response.headers.forEach((v: any, k: any) => _headers[k] = v); };
        if (status === 200) {
            return response.text().then((_responseText) => {
            let result200: any = null;
            result200 = _responseText === "" ? null : <ResponseModel>JSON.parse(_responseText, this.jsonParseReviver);
            return result200;
            });
        } else if (status !== 200 && status !== 204) {
            return response.text().then((_responseText) => {
            return throwException("An unexpected server error occurred.", status, _responseText, _headers);
            });
        }
        return Promise.resolve<ResponseModel>(<any>null);
    }

    /**
     * @param name (optional) 
     * @param price (optional) 
     * @param stock (optional) 
     * @return Success
     */
    insertsemen(name: string | undefined, price: number | undefined, stock: number | undefined): Promise<ResponseModel> {
        let url_ = this.baseUrl + "/api/v1/create/insertsemen?";
        if (name === null)
            throw new Error("The parameter 'name' cannot be null.");
        else if (name !== undefined)
            url_ += "name=" + encodeURIComponent("" + name) + "&"; 
        if (price === null)
            throw new Error("The parameter 'price' cannot be null.");
        else if (price !== undefined)
            url_ += "price=" + encodeURIComponent("" + price) + "&"; 
        if (stock === null)
            throw new Error("The parameter 'stock' cannot be null.");
        else if (stock !== undefined)
            url_ += "stock=" + encodeURIComponent("" + stock) + "&"; 
        url_ = url_.replace(/[?&]$/, "");

        let options_ = <RequestInit>{
            method: "POST",
            headers: {
                "Accept": "text/plain"
            }
        };

        return this.http.fetch(url_, options_).then((_response: Response) => {
            return this.processInsertsemen(_response);
        });
    }

    protected processInsertsemen(response: Response): Promise<ResponseModel> {
        const status = response.status;
        let _headers: any = {}; if (response.headers && response.headers.forEach) { response.headers.forEach((v: any, k: any) => _headers[k] = v); };
        if (status === 200) {
            return response.text().then((_responseText) => {
            let result200: any = null;
            result200 = _responseText === "" ? null : <ResponseModel>JSON.parse(_responseText, this.jsonParseReviver);
            return result200;
            });
        } else if (status !== 200 && status !== 204) {
            return response.text().then((_responseText) => {
            return throwException("An unexpected server error occurred.", status, _responseText, _headers);
            });
        }
        return Promise.resolve<ResponseModel>(<any>null);
    }

    /**
     * @param id (optional) 
     * @param name (optional) 
     * @param price (optional) 
     * @param stock (optional) 
     * @return Success
     */
    updatesemen(id: number | undefined, name: string | undefined, price: number | undefined, stock: number | undefined): Promise<ResponseModel> {
        let url_ = this.baseUrl + "/api/v1/create/update?";
        if (id === null)
            throw new Error("The parameter 'id' cannot be null.");
        else if (id !== undefined)
            url_ += "id=" + encodeURIComponent("" + id) + "&"; 
        if (name === null)
            throw new Error("The parameter 'name' cannot be null.");
        else if (name !== undefined)
            url_ += "name=" + encodeURIComponent("" + name) + "&"; 
        if (price === null)
            throw new Error("The parameter 'price' cannot be null.");
        else if (price !== undefined)
            url_ += "price=" + encodeURIComponent("" + price) + "&"; 
        if (stock === null)
            throw new Error("The parameter 'stock' cannot be null.");
        else if (stock !== undefined)
            url_ += "stock=" + encodeURIComponent("" + stock) + "&"; 
        url_ = url_.replace(/[?&]$/, "");

        let options_ = <RequestInit>{
            method: "PUT",
            headers: {
                "Accept": "text/plain"
            }
        };

        return this.http.fetch(url_, options_).then((_response: Response) => {
            return this.processUpdatesemen(_response);
        });
    }

    protected processUpdatesemen(response: Response): Promise<ResponseModel> {
        const status = response.status;
        let _headers: any = {}; if (response.headers && response.headers.forEach) { response.headers.forEach((v: any, k: any) => _headers[k] = v); };
        if (status === 200) {
            return response.text().then((_responseText) => {
            let result200: any = null;
            result200 = _responseText === "" ? null : <ResponseModel>JSON.parse(_responseText, this.jsonParseReviver);
            return result200;
            });
        } else if (status !== 200 && status !== 204) {
            return response.text().then((_responseText) => {
            return throwException("An unexpected server error occurred.", status, _responseText, _headers);
            });
        }
        return Promise.resolve<ResponseModel>(<any>null);
    }

    /**
     * @param body (optional) 
     * @return Success
     */
    deletesemen(body: SemenModel | undefined): Promise<ResponseModel> {
        let url_ = this.baseUrl + "/api/v1/create/delete";
        url_ = url_.replace(/[?&]$/, "");

        const content_ = JSON.stringify(body);

        let options_ = <RequestInit>{
            body: content_,
            method: "DELETE",
            headers: {
                "Content-Type": "application/json",
                "Accept": "text/plain"
            }
        };

        return this.http.fetch(url_, options_).then((_response: Response) => {
            return this.processDeletesemen(_response);
        });
    }

    protected processDeletesemen(response: Response): Promise<ResponseModel> {
        const status = response.status;
        let _headers: any = {}; if (response.headers && response.headers.forEach) { response.headers.forEach((v: any, k: any) => _headers[k] = v); };
        if (status === 200) {
            return response.text().then((_responseText) => {
            let result200: any = null;
            result200 = _responseText === "" ? null : <ResponseModel>JSON.parse(_responseText, this.jsonParseReviver);
            return result200;
            });
        } else if (status !== 200 && status !== 204) {
            return response.text().then((_responseText) => {
            return throwException("An unexpected server error occurred.", status, _responseText, _headers);
            });
        }
        return Promise.resolve<ResponseModel>(<any>null);
    }

    /**
     * @param pageIndex (optional) 
     * @param itemPerPage (optional) 
     * @param filterByName (optional) 
     * @return Success
     */
    filterBarang(pageIndex: number | undefined, itemPerPage: number | undefined, filterByName: string | undefined): Promise<SemenModel[]> {
        let url_ = this.baseUrl + "/api/v1/create/filter-barang?";
        if (pageIndex === null)
            throw new Error("The parameter 'pageIndex' cannot be null.");
        else if (pageIndex !== undefined)
            url_ += "pageIndex=" + encodeURIComponent("" + pageIndex) + "&"; 
        if (itemPerPage === null)
            throw new Error("The parameter 'itemPerPage' cannot be null.");
        else if (itemPerPage !== undefined)
            url_ += "itemPerPage=" + encodeURIComponent("" + itemPerPage) + "&"; 
        if (filterByName === null)
            throw new Error("The parameter 'filterByName' cannot be null.");
        else if (filterByName !== undefined)
            url_ += "filterByName=" + encodeURIComponent("" + filterByName) + "&"; 
        url_ = url_.replace(/[?&]$/, "");

        let options_ = <RequestInit>{
            method: "GET",
            headers: {
                "Accept": "text/plain"
            }
        };

        return this.http.fetch(url_, options_).then((_response: Response) => {
            return this.processFilterBarang(_response);
        });
    }

    protected processFilterBarang(response: Response): Promise<SemenModel[]> {
        const status = response.status;
        let _headers: any = {}; if (response.headers && response.headers.forEach) { response.headers.forEach((v: any, k: any) => _headers[k] = v); };
        if (status === 200) {
            return response.text().then((_responseText) => {
            let result200: any = null;
            result200 = _responseText === "" ? null : <SemenModel[]>JSON.parse(_responseText, this.jsonParseReviver);
            return result200;
            });
        } else if (status !== 200 && status !== 204) {
            return response.text().then((_responseText) => {
            return throwException("An unexpected server error occurred.", status, _responseText, _headers);
            });
        }
        return Promise.resolve<SemenModel[]>(<any>null);
    }

    /**
     * @return Success
     */
    totalBarang(): Promise<number> {
        let url_ = this.baseUrl + "/api/v1/create/total-barang";
        url_ = url_.replace(/[?&]$/, "");

        let options_ = <RequestInit>{
            method: "GET",
            headers: {
                "Accept": "text/plain"
            }
        };

        return this.http.fetch(url_, options_).then((_response: Response) => {
            return this.processTotalBarang(_response);
        });
    }

    protected processTotalBarang(response: Response): Promise<number> {
        const status = response.status;
        let _headers: any = {}; if (response.headers && response.headers.forEach) { response.headers.forEach((v: any, k: any) => _headers[k] = v); };
        if (status === 200) {
            return response.text().then((_responseText) => {
            let result200: any = null;
            result200 = _responseText === "" ? null : <number>JSON.parse(_responseText, this.jsonParseReviver);
            return result200;
            });
        } else if (status !== 200 && status !== 204) {
            return response.text().then((_responseText) => {
            return throwException("An unexpected server error occurred.", status, _responseText, _headers);
            });
        }
        return Promise.resolve<number>(<any>null);
    }

    /**
     * @param semenid (optional) 
     * @return Success
     */
    getspecificsemen(semenid: number | undefined): Promise<SemenModel> {
        let url_ = this.baseUrl + "/api/v1/create/semen-specific?";
        if (semenid === null)
            throw new Error("The parameter 'semenid' cannot be null.");
        else if (semenid !== undefined)
            url_ += "semenid=" + encodeURIComponent("" + semenid) + "&"; 
        url_ = url_.replace(/[?&]$/, "");

        let options_ = <RequestInit>{
            method: "GET",
            headers: {
                "Accept": "text/plain"
            }
        };

        return this.http.fetch(url_, options_).then((_response: Response) => {
            return this.processGetspecificsemen(_response);
        });
    }

    protected processGetspecificsemen(response: Response): Promise<SemenModel> {
        const status = response.status;
        let _headers: any = {}; if (response.headers && response.headers.forEach) { response.headers.forEach((v: any, k: any) => _headers[k] = v); };
        if (status === 200) {
            return response.text().then((_responseText) => {
            let result200: any = null;
            result200 = _responseText === "" ? null : <SemenModel>JSON.parse(_responseText, this.jsonParseReviver);
            return result200;
            });
        } else if (status !== 200 && status !== 204) {
            return response.text().then((_responseText) => {
            return throwException("An unexpected server error occurred.", status, _responseText, _headers);
            });
        }
        return Promise.resolve<SemenModel>(<any>null);
    }

    /**
     * @param pageIndex (optional) 
     * @param itemPerPage (optional) 
     * @return Success
     */
    gettransactionuser(pageIndex: number | undefined, itemPerPage: number | undefined): Promise<TransactionUserModel[]> {
        let url_ = this.baseUrl + "/api/v1/create/filter-Transactionusers?";
        if (pageIndex === null)
            throw new Error("The parameter 'pageIndex' cannot be null.");
        else if (pageIndex !== undefined)
            url_ += "pageIndex=" + encodeURIComponent("" + pageIndex) + "&"; 
        if (itemPerPage === null)
            throw new Error("The parameter 'itemPerPage' cannot be null.");
        else if (itemPerPage !== undefined)
            url_ += "itemPerPage=" + encodeURIComponent("" + itemPerPage) + "&"; 
        url_ = url_.replace(/[?&]$/, "");

        let options_ = <RequestInit>{
            method: "GET",
            headers: {
                "Accept": "text/plain"
            }
        };

        return this.http.fetch(url_, options_).then((_response: Response) => {
            return this.processGettransactionuser(_response);
        });
    }

    protected processGettransactionuser(response: Response): Promise<TransactionUserModel[]> {
        const status = response.status;
        let _headers: any = {}; if (response.headers && response.headers.forEach) { response.headers.forEach((v: any, k: any) => _headers[k] = v); };
        if (status === 200) {
            return response.text().then((_responseText) => {
            let result200: any = null;
            result200 = _responseText === "" ? null : <TransactionUserModel[]>JSON.parse(_responseText, this.jsonParseReviver);
            return result200;
            });
        } else if (status !== 200 && status !== 204) {
            return response.text().then((_responseText) => {
            return throwException("An unexpected server error occurred.", status, _responseText, _headers);
            });
        }
        return Promise.resolve<TransactionUserModel[]>(<any>null);
    }

    /**
     * @return Success
     */
    totalTransactionuser(): Promise<number> {
        let url_ = this.baseUrl + "/api/v1/create/total-transactionuser";
        url_ = url_.replace(/[?&]$/, "");

        let options_ = <RequestInit>{
            method: "GET",
            headers: {
                "Accept": "text/plain"
            }
        };

        return this.http.fetch(url_, options_).then((_response: Response) => {
            return this.processTotalTransactionuser(_response);
        });
    }

    protected processTotalTransactionuser(response: Response): Promise<number> {
        const status = response.status;
        let _headers: any = {}; if (response.headers && response.headers.forEach) { response.headers.forEach((v: any, k: any) => _headers[k] = v); };
        if (status === 200) {
            return response.text().then((_responseText) => {
            let result200: any = null;
            result200 = _responseText === "" ? null : <number>JSON.parse(_responseText, this.jsonParseReviver);
            return result200;
            });
        } else if (status !== 200 && status !== 204) {
            return response.text().then((_responseText) => {
            return throwException("An unexpected server error occurred.", status, _responseText, _headers);
            });
        }
        return Promise.resolve<number>(<any>null);
    }

    /**
     * @param semenid (optional) 
     * @return Success
     */
    getsemenspecificssqlite(semenid: number | undefined): Promise<SemenSqliteModel> {
        let url_ = this.baseUrl + "/api/v1/create/semen-specific-sqlite?";
        if (semenid === null)
            throw new Error("The parameter 'semenid' cannot be null.");
        else if (semenid !== undefined)
            url_ += "semenid=" + encodeURIComponent("" + semenid) + "&"; 
        url_ = url_.replace(/[?&]$/, "");

        let options_ = <RequestInit>{
            method: "GET",
            headers: {
                "Accept": "text/plain"
            }
        };

        return this.http.fetch(url_, options_).then((_response: Response) => {
            return this.processGetsemenspecificssqlite(_response);
        });
    }

    protected processGetsemenspecificssqlite(response: Response): Promise<SemenSqliteModel> {
        const status = response.status;
        let _headers: any = {}; if (response.headers && response.headers.forEach) { response.headers.forEach((v: any, k: any) => _headers[k] = v); };
        if (status === 200) {
            return response.text().then((_responseText) => {
            let result200: any = null;
            result200 = _responseText === "" ? null : <SemenSqliteModel>JSON.parse(_responseText, this.jsonParseReviver);
            return result200;
            });
        } else if (status !== 200 && status !== 204) {
            return response.text().then((_responseText) => {
            return throwException("An unexpected server error occurred.", status, _responseText, _headers);
            });
        }
        return Promise.resolve<SemenSqliteModel>(<any>null);
    }

    /**
     * @param pageIndex (optional) 
     * @param itemPerPage (optional) 
     * @param filterByName (optional) 
     * @return Success
     */
    filterSemenSqlite(pageIndex: number | undefined, itemPerPage: number | undefined, filterByName: string | undefined): Promise<SemenSqliteModel[]> {
        let url_ = this.baseUrl + "/api/v1/create/filter-semen-sqlite?";
        if (pageIndex === null)
            throw new Error("The parameter 'pageIndex' cannot be null.");
        else if (pageIndex !== undefined)
            url_ += "pageIndex=" + encodeURIComponent("" + pageIndex) + "&"; 
        if (itemPerPage === null)
            throw new Error("The parameter 'itemPerPage' cannot be null.");
        else if (itemPerPage !== undefined)
            url_ += "itemPerPage=" + encodeURIComponent("" + itemPerPage) + "&"; 
        if (filterByName === null)
            throw new Error("The parameter 'filterByName' cannot be null.");
        else if (filterByName !== undefined)
            url_ += "filterByName=" + encodeURIComponent("" + filterByName) + "&"; 
        url_ = url_.replace(/[?&]$/, "");

        let options_ = <RequestInit>{
            method: "GET",
            headers: {
                "Accept": "text/plain"
            }
        };

        return this.http.fetch(url_, options_).then((_response: Response) => {
            return this.processFilterSemenSqlite(_response);
        });
    }

    protected processFilterSemenSqlite(response: Response): Promise<SemenSqliteModel[]> {
        const status = response.status;
        let _headers: any = {}; if (response.headers && response.headers.forEach) { response.headers.forEach((v: any, k: any) => _headers[k] = v); };
        if (status === 200) {
            return response.text().then((_responseText) => {
            let result200: any = null;
            result200 = _responseText === "" ? null : <SemenSqliteModel[]>JSON.parse(_responseText, this.jsonParseReviver);
            return result200;
            });
        } else if (status !== 200 && status !== 204) {
            return response.text().then((_responseText) => {
            return throwException("An unexpected server error occurred.", status, _responseText, _headers);
            });
        }
        return Promise.resolve<SemenSqliteModel[]>(<any>null);
    }

    /**
     * @param name (optional) 
     * @param price (optional) 
     * @param stock (optional) 
     * @return Success
     */
    insertsemensqlite(name: string | undefined, price: number | undefined, stock: number | undefined): Promise<ResponseModel> {
        let url_ = this.baseUrl + "/api/v1/create/insertsemen-sqlite?";
        if (name === null)
            throw new Error("The parameter 'name' cannot be null.");
        else if (name !== undefined)
            url_ += "name=" + encodeURIComponent("" + name) + "&"; 
        if (price === null)
            throw new Error("The parameter 'price' cannot be null.");
        else if (price !== undefined)
            url_ += "price=" + encodeURIComponent("" + price) + "&"; 
        if (stock === null)
            throw new Error("The parameter 'stock' cannot be null.");
        else if (stock !== undefined)
            url_ += "stock=" + encodeURIComponent("" + stock) + "&"; 
        url_ = url_.replace(/[?&]$/, "");

        let options_ = <RequestInit>{
            method: "POST",
            headers: {
                "Accept": "text/plain"
            }
        };

        return this.http.fetch(url_, options_).then((_response: Response) => {
            return this.processInsertsemensqlite(_response);
        });
    }

    protected processInsertsemensqlite(response: Response): Promise<ResponseModel> {
        const status = response.status;
        let _headers: any = {}; if (response.headers && response.headers.forEach) { response.headers.forEach((v: any, k: any) => _headers[k] = v); };
        if (status === 200) {
            return response.text().then((_responseText) => {
            let result200: any = null;
            result200 = _responseText === "" ? null : <ResponseModel>JSON.parse(_responseText, this.jsonParseReviver);
            return result200;
            });
        } else if (status !== 200 && status !== 204) {
            return response.text().then((_responseText) => {
            return throwException("An unexpected server error occurred.", status, _responseText, _headers);
            });
        }
        return Promise.resolve<ResponseModel>(<any>null);
    }

    /**
     * @param id (optional) 
     * @param name (optional) 
     * @param price (optional) 
     * @param stock (optional) 
     * @return Success
     */
    updatesemensqlite(id: number | undefined, name: string | undefined, price: number | undefined, stock: number | undefined): Promise<ResponseModel> {
        let url_ = this.baseUrl + "/api/v1/create/updatesemen-sqlite?";
        if (id === null)
            throw new Error("The parameter 'id' cannot be null.");
        else if (id !== undefined)
            url_ += "id=" + encodeURIComponent("" + id) + "&"; 
        if (name === null)
            throw new Error("The parameter 'name' cannot be null.");
        else if (name !== undefined)
            url_ += "name=" + encodeURIComponent("" + name) + "&"; 
        if (price === null)
            throw new Error("The parameter 'price' cannot be null.");
        else if (price !== undefined)
            url_ += "price=" + encodeURIComponent("" + price) + "&"; 
        if (stock === null)
            throw new Error("The parameter 'stock' cannot be null.");
        else if (stock !== undefined)
            url_ += "stock=" + encodeURIComponent("" + stock) + "&"; 
        url_ = url_.replace(/[?&]$/, "");

        let options_ = <RequestInit>{
            method: "PUT",
            headers: {
                "Accept": "text/plain"
            }
        };

        return this.http.fetch(url_, options_).then((_response: Response) => {
            return this.processUpdatesemensqlite(_response);
        });
    }

    protected processUpdatesemensqlite(response: Response): Promise<ResponseModel> {
        const status = response.status;
        let _headers: any = {}; if (response.headers && response.headers.forEach) { response.headers.forEach((v: any, k: any) => _headers[k] = v); };
        if (status === 200) {
            return response.text().then((_responseText) => {
            let result200: any = null;
            result200 = _responseText === "" ? null : <ResponseModel>JSON.parse(_responseText, this.jsonParseReviver);
            return result200;
            });
        } else if (status !== 200 && status !== 204) {
            return response.text().then((_responseText) => {
            return throwException("An unexpected server error occurred.", status, _responseText, _headers);
            });
        }
        return Promise.resolve<ResponseModel>(<any>null);
    }

    /**
     * @param semenid (optional) 
     * @return Success
     */
    deleteSemenSqlite(semenid: number | undefined): Promise<void> {
        let url_ = this.baseUrl + "/api/v1/create/delete-semen-sqlite?";
        if (semenid === null)
            throw new Error("The parameter 'semenid' cannot be null.");
        else if (semenid !== undefined)
            url_ += "semenid=" + encodeURIComponent("" + semenid) + "&"; 
        url_ = url_.replace(/[?&]$/, "");

        let options_ = <RequestInit>{
            method: "DELETE",
            headers: {
            }
        };

        return this.http.fetch(url_, options_).then((_response: Response) => {
            return this.processDeleteSemenSqlite(_response);
        });
    }

    protected processDeleteSemenSqlite(response: Response): Promise<void> {
        const status = response.status;
        let _headers: any = {}; if (response.headers && response.headers.forEach) { response.headers.forEach((v: any, k: any) => _headers[k] = v); };
        if (status === 200) {
            return response.text().then((_responseText) => {
            return;
            });
        } else if (status !== 200 && status !== 204) {
            return response.text().then((_responseText) => {
            return throwException("An unexpected server error occurred.", status, _responseText, _headers);
            });
        }
        return Promise.resolve<void>(<any>null);
    }

    /**
     * @return Success
     */
    totalDataSqlite(): Promise<number> {
        let url_ = this.baseUrl + "/api/v1/create/total-data-sqlite";
        url_ = url_.replace(/[?&]$/, "");

        let options_ = <RequestInit>{
            method: "GET",
            headers: {
                "Accept": "text/plain"
            }
        };

        return this.http.fetch(url_, options_).then((_response: Response) => {
            return this.processTotalDataSqlite(_response);
        });
    }

    protected processTotalDataSqlite(response: Response): Promise<number> {
        const status = response.status;
        let _headers: any = {}; if (response.headers && response.headers.forEach) { response.headers.forEach((v: any, k: any) => _headers[k] = v); };
        if (status === 200) {
            return response.text().then((_responseText) => {
            let result200: any = null;
            result200 = _responseText === "" ? null : <number>JSON.parse(_responseText, this.jsonParseReviver);
            return result200;
            });
        } else if (status !== 200 && status !== 204) {
            return response.text().then((_responseText) => {
            return throwException("An unexpected server error occurred.", status, _responseText, _headers);
            });
        }
        return Promise.resolve<number>(<any>null);
    }
}

export interface TransactionModel {
    transactionId?: string | undefined;
    userName?: string | undefined;
    purchaseDate?: string;
}

export interface TransactionDetailModel {
    userName?: string | undefined;
    productName?: string | undefined;
    quantity?: number;
    transactionId?: string | undefined;
    transactionDetailId?: number;
}

export interface RegisterFormModel {
    userName: string;
    password: string;
    confirmPassword: string;
}

export interface ResponseModel {
    responseMessage?: string | undefined;
    status?: string | undefined;
}

export interface SemenModel {
    barangId?: number;
    barangName?: string | undefined;
    barangPrice?: number;
    barangStock?: number;
}

export interface TransactionUserModel {
    transactionId?: string | undefined;
    productName?: string | undefined;
    purchaseDate?: string;
    quantity?: number;
}

export interface SemenSqliteModel {
    semenID?: number;
    semenName: string;
    semenPrice: number;
    semenStock: number;
}

export class ApiException extends Error {
    message: string;
    status: number; 
    response: string; 
    headers: { [key: string]: any; };
    result: any; 

    constructor(message: string, status: number, response: string, headers: { [key: string]: any; }, result: any) {
        super();

        this.message = message;
        this.status = status;
        this.response = response;
        this.headers = headers;
        this.result = result;
    }

    protected isApiException = true;

    static isApiException(obj: any): obj is ApiException {
        return obj.isApiException === true;
    }
}

function throwException(message: string, status: number, response: string, headers: { [key: string]: any; }, result?: any): any {
    if (result !== null && result !== undefined)
        throw result;
    else
        throw new ApiException(message, status, response, headers, null);
}