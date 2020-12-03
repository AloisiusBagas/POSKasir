<template>
    <div>
        <div v-if="isAddSemen === false">
            <!--table-->
            <button class="btn btn-primary" type="button" @click="addnewsemen">Add New Semen</button>
            <input type="text" v-model="filterByName" @change="fetch()" />
            <table class="table table-bordered">
                <thead class="thead-dark">
                    <tr>
                        <th scope="col">Semen ID</th>
                        <th scope="col">Semen Name</th>
                        <th scope="col">Price</th>
                        <th scope="col">Stock</th>
                        <th scope="col">Action</th>
                    </tr>
                </thead>
                <tbody>
                    <tr v-for="(data,index) in Semen" :key="index">
                        <td>{{data.semenID}}</td>
                        <td>{{data.semenName}}</td>
                        <td>{{data.semenPrice}}</td>
                        <td>{{data.semenStock}}</td>
                        <td>
                            <a :href="'barang/UpdateBarangSqlite?semenID='+data.semenID" class="btn btn-warning">Edit</a>
                            <button class="btn btn-danger" @click="deleteSemen(data.semenID)">
                                <fa-icon icon="trash-alt"></fa-icon>
                                Delete
                            </button>
                        </td>
                    </tr>
                </tbody>
            </table>
            <div class="row">
                <div class="col-md-12">
                    <ul class="pagination" v-for="page in totalPage">
                        <li class="page-item">
                            <button @click="changePage(page)">{{page}}</button>
                        </li>
                    </ul>
                </div>
            </div>
        </div>

        <div v-if="isAddSemen === true">
            <!--form-->
            <validation-observer ref="observer" v-slot="{validate,valid}">
                <form method="post" @submit.prevent="insertsemen">

                    <label class="col-form-label">Semen Name</label>
                    <validation-provider name="Name" rules="required|min:3|max:20" v-slot="{errors}">
                        <input class="form-control" type="text" v-model="newsemen.semenName" />
                        <span class="text-danger font-italic">{{errors[0]}}</span>
                    </validation-provider>
                    <br />

                    <label class="col-form-label">Semen Price</label>
                    <validation-provider name="Price" rules="required" v-slot="{errors}">
                        <input class="form-control" type="text" v-model="newsemen.semenPrice" />
                        <span class="text-danger font-italic">{{errors[0]}}</span>
                    </validation-provider>
                    <br />

                    <label class="col-form-label">Semen Stock</label>
                    <validation-provider name="Price" rules="required|numeric" v-slot="{errors}">
                        <input class="form-control" type="text" v-model="newsemen.semenStock" />
                        <span class="text-danger font-italic">{{errors[0]}}</span>
                    </validation-provider>
                    <br />

                    <button class="btn btn-primary" type="submit">
                        <fa-icon icon="check"></fa-icon>
                        Submit
                    </button>
                </form>
            </validation-observer>

        </div>
    </div>
</template>

<script lang="ts">
    import Vue from 'vue';
    import Component from 'vue-class-component';
    import { ValidationObserver } from 'vee-validate';
    import swal from 'sweetalert2';
    import { AppPosService, SemenSqliteModel } from '../Service/NSwagService';

    @Component({
        created: async function (this: ListBarangSqlite) {
        await this.fetch();
    }
    })
    export default class ListBarangSqlite extends Vue {
        service: AppPosService = new AppPosService();
        Semen: SemenSqliteModel[] = [];

        isAddSemen = false;

        newsemen: SemenSqliteModel = {
            semenID: 0,
            semenName: '',
            semenPrice: 0,
            semenStock: 0
        }

        filterByName = '';
        pageIndex = 1;
        itemPerPage = 3;
        totalData = 1;
        totalPage = 1;

        async fetch(): Promise<void> {
            this.totalData = await this.service.totalDataSqlite();
            this.totalPage = Math.ceil(this.totalData / this.itemPerPage);

            this.Semen = await this.service.filterSemenSqlite(this.pageIndex, this.itemPerPage, this.filterByName);
        }

        async changePage(page: number): Promise<void> {
            this.pageIndex = page;
            await this.fetch();
        }

        async insertsemen(): Promise<void> {
            const valid = await (this.$refs.observer as InstanceType<typeof ValidationObserver>).validate();
            if (valid == false) {
                return;
            }
            await this.service.insertsemensqlite(this.newsemen.semenName, this.newsemen.semenPrice, this.newsemen.semenStock);
            await this.fetch();
            swal.fire({
                title: 'Yeayy!',
                icon: 'success',
                text: 'Success insert new Data.'
            });
            this.isAddSemen = false;
        }

        addnewsemen(): void {
            this.isAddSemen = true;
            return;
        }

                async deleteSemen(data): Promise<void> {
            swal.fire({
                title: 'Are you sure?',
                text: "Delete this data",
                icon: 'warning',
                showCancelButton: true,
                confirmButtonColor: '#3085d6',
                cancelButtonColor: '#d33',
                confirmButtonText: 'Yes, Delete it!'
            }).then(async (result) => {
                if (result.value) {
                    await this.service.deleteSemenSqlite(data);
                    await this.fetch();
                    swal.fire(
                        'Deleted!',
                        'Your file has been Deleted.',
                        'success'
                    )

                }
            })
        }
    }
</script>
