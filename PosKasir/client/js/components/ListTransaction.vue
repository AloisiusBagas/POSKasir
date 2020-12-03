<template>
    <div>
        <input type="text" v-model="filterByName" @change="fetch()" />
        <table class="table table-bordered">
            <thead class="thead-dark">
                <tr>
                    <th scope="col">Transaction ID</th>
                    <th scope="col">Customer Name</th>
                    <th scope="col">Transaction Date</th>
                    <th scope="col">Actions</th>
                </tr>
            </thead>
            <tbody>
                <tr v-for="(data,index) in transaction" :key="index">
                    <td>{{data.transactionId}}</td>
                    <td>{{data.userName}}</td>
                    <td>{{data.purchaseDate}}</td>
                    <td>
                        <a :href="'transaction/transactiondetail?transactionID='+data.transactionId" class="btn btn-primary">
                            <fa-icon icon="list" class="mr-3"></fa-icon>
                             Detail
                        </a>
                    </td>
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
import {AppPosService,TransactionModel} from '../Service/NSwagService';

    @Component({
        created: async function (this: ListTransaction) {
            await this.fetch();
        }
})
    export default class ListTransaction extends Vue {
        service: AppPosService = new AppPosService();
        transaction: TransactionModel[] = [];

        filterByName = '';
        pageIndex = 1;
        itemPerPage = 5;
        totalData = 1;
        totalPage = 1;

        async fetch(): Promise<void> {
            this.totalData = await this.service.totalTransaction();
            this.totalPage = Math.ceil(this.totalData / this.itemPerPage);
            this.transaction = await this.service.filterData(this.pageIndex, this.itemPerPage, this.filterByName)
        }

        async ChangePage(page: number): Promise<void> {
            this.pageIndex = page;
            await this.fetch();
        }
}
</script>
