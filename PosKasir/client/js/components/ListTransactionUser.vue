﻿
<template>
    <div>
        <table class="table table-bordered">
            <thead class="thead-dark">
                <tr>
                    <th scope="col">Transaction ID</th>
                    <th scope="col">Product Name</th>
                    <th scope="col">Transaction Date</th>
                    <th scope="col">Quantity</th>
                </tr>
            </thead>
            <tbody>
                <tr v-for="(datas,index) in data" :key="index">
                    <td>{{datas.transactionId}}</td>
                    <td>{{datas.productName}}</td>
                    <td>{{datas.purchaseDate}}</td>
                    <td>{{datas.quantity}}</td>
                </tr>
            </tbody>
        </table>
        <div class="row">
            <div class="col-md-12">
                <ul class="pagination">
                    <li class="page-item design" v-for="page in totalPage">
                        <button @click="ChangePage(page)">{{page}}</button>
                    </li>
                </ul>
            </div>
        </div>
    </div>
</template>

<script lang="ts">
    import Vue from 'vue';
    import Component from 'vue-class-component';
    import { AppPosService, TransactionUserModel } from '../Service/NSwagService';

    @Component({
        created: async function (this: ListTransactionUser) {
            await this.fetch();
        }
    })
    export default class ListTransactionUser extends Vue {
        service: AppPosService = new AppPosService();
        data: TransactionUserModel[] = [];

        //for pagination
        filterByName = '';
        pageIndex = 1;
        itemPerPage = 3;
        totalData = 1;
        totalPage = 1;
        userId = 0;

        async fetch(): Promise<void> {
            this.totalData = await this.service.totalTransactionuser();
            this.totalPage = Math.ceil(this.totalData / this.itemPerPage);
            this.data = await this.service.gettransactionuser(this.pageIndex, this.itemPerPage);
        }

        async ChangePage(page: number): Promise<void> {
            this.pageIndex = page;
            await this.fetch();
        }
    }
</script>
