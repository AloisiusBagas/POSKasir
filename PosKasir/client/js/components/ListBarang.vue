<template>
    <div>
        <div v-if="isAddSemen === false">
            <!--table-->
            <button class="btn btn-primary" type="button" @click="addnewsemen">
                <fa-icon icon="plus"></fa-icon>
                Add New Semen
            </button>
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
                        <td>{{data.barangId}}</td>
                        <td>{{data.barangName}}</td>
                        <td>{{data.barangPrice + ".000"}}</td>
                        <td>{{data.barangStock}}</td>
                        <td>
                            <a :href="'barang/updatebarang?semenID='+data.barangId" class="btn btn-warning">
                            <fa-icon icon="edit"></fa-icon>
                            Edit</a>
                            <button class="btn btn-danger" @click="deleteSemen(data)">
                                <fa-icon icon="trash-alt"></fa-icon>
                                Delete
                            </button>
                        </td>
                    </tr>
                </tbody>
            </table>
            <div class="row">
                <div class="col-md-12">
                    <ul class="pagination">
                        <li class="page-item design" v-for="page in totalPage">
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
                        <input class="form-control" type="text" v-model="newSemen.barangName" />
                        <span class="text-danger font-italic">{{errors[0]}}</span>
                    </validation-provider>
                    <br />

                    <label class="col-form-label">Price</label>
                    <validation-provider name="Price" rules="required" v-slot="{errors}">
                        <input class="form-control" type="text" v-model="newSemen.barangPrice" />
                        <span class="text-danger font-italic">{{errors[0]}}</span>
                    </validation-provider>
                    <br />

                    <label class="col-form-label">Stock</label>
                    <validation-provider name="Stock" rules="required|numeric" v-slot="{errors}">
                        <input class="form-control" type="text" v-model="newSemen.barangStock" />
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
    import { AppPosService, SemenModel } from '../Service/NSwagService';

    @Component({
        created: async function (this: ListBarang) {
            await this.fetch();
        }
    })
    export default class ListBarang extends Vue {
        service: AppPosService = new AppPosService();
        Semen: SemenModel[] = [];

        isAddSemen = false;

        newSemen: SemenModel = {
            barangId: 0,
            barangName: '',
            barangPrice: 0,
            barangStock: 0
        }

        filterByName = '';
        pageIndex = 1;
        itemPerPage = 3;
        totalData = 1;
        totalPage = 1;

        async fetch(): Promise<void> {
            this.totalData = await this.service.totalBarang();
            this.totalPage = Math.ceil(this.totalData / this.itemPerPage);

            this.Semen = await this.service.filterBarang(this.pageIndex, this.itemPerPage, this.filterByName);
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
            await this.service.insertsemen(this.newSemen.barangName, this.newSemen.barangPrice, this.newSemen.barangStock);
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
                    await this.service.deletesemen(data);
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
