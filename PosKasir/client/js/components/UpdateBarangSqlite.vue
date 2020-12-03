<template>
    <div>
        <validation-observer ref="observer" v-slot="{validate,valid}">
            <form method="post" @submit.prevent="updateHandphone">

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
</template>

<script lang="ts">
    import Vue from 'vue';
    import Component from 'vue-class-component';
    import { ValidationObserver } from 'vee-validate';
    import swal from 'sweetalert2';
    import { AppPosService, SemenSqliteModel } from '../Service/NSwagService';

    @Component({
        created: async function (this: UpdateBarangSqlite) {
            await this.getSpecificSemen();
    }
    })
    export default class UpdateBarangSqlite extends Vue {
        service: AppPosService = new AppPosService();

        newsemen: SemenSqliteModel = {
            semenID: 0,
            semenName: '',
            semenPrice: 0,
            semenStock: 0
        }

        async getSpecificSemen(): Promise<void> {
            const urlParams = new URLSearchParams(window.location.search);
            const handphoneID = urlParams.get('semenID')?.valueOf();
            if (handphoneID != null) {
                this.newsemen = await this.service.getsemenspecificssqlite(parseInt(handphoneID));
            }

        }

        async updateHandphone(): Promise<void> {
            const valid = await (this.$refs.observer as InstanceType<typeof ValidationObserver>).validate();
            if (valid == false) {
                return;
            }
            swal.fire({
                title: 'Are you sure?',
                text: "Update this Semen data",
                icon: 'warning',
                showCancelButton: true,
                confirmButtonColor: '#3085d6',
                cancelButtonColor: '#d33',
                confirmButtonText: 'Yes, update it!'
            }).then(async (result) => {
                if (result.value) {

                    await this.service.updatesemensqlite(this.newsemen.semenID, this.newsemen.semenName, this.newsemen.semenPrice, this.newsemen.semenStock);
                    swal.fire(
                        'Updated!',
                        'Your file has been updated.',
                        'success'
                    ).then(function () {
                        window.location.href = '/barang/listbarangsqlite';
                    });
                }
            })
        }


    }
</script>
