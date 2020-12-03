<template>
    <div>
        <input type="text" v-model="filterByProductName" @change="fetch()" />
        <table class="table table-bordered">
            <thead class="thead-dark">
                <tr>
                    <th scope="col">Transaction ID</th>
                    <th scope="col">Transaction Detail ID</th>
                    <th scope="col">Product Name</th>
                    <th scope="col">Quantity</th>
                </tr>
            </thead>
            <tbody>
                <tr v-for="(data,index) in transactiondetail" :key="index">
                    <td>{{data.transactionId}}</td>
                    <td>{{data.transactionDetailId}}</td>
                    <td>{{data.productName}}</td>
                    <td>{{data.quantity}}</td>
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
    import { AppPosService, TransactionDetailModel } from '../Service/NSwagService';

    @Component({
        created: async function (this: DetailTransaction) {
            await this.fetch();
        }
    })
    export default class DetailTransaction extends Vue {
        service: AppPosService = new AppPosService();
        transactiondetail: TransactionDetailModel[] = [];

        filterByProductName = '';
        pageIndex = 1;
        itemPerPage = 5;
        totalData = 1;
        totalPage = 1;

        async fetch(): Promise<void> {
            const urlParams = new URLSearchParams(window.location.search);
            const transactionID = urlParams.get('transactionID')?.valueOf();
            if (transactionID != null) {
                this.totalData = await this.service.totalDetailtransaction(transactionID);
                this.totalPage = Math.ceil(this.totalData / this.itemPerPage);
                this.transactiondetail = await this.service.filterDetaildata(this.pageIndex, this.itemPerPage, this.filterByProductName, transactionID);
            }

        }

        async ChangePage(page: number): Promise<void> {
            this.pageIndex = page;
            await this.fetch();
        }
    }
</script>
