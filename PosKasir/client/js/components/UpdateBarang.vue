<template>
    <div>
        <validation-observer ref="observer" v-slot="{validate,valid}">
            <form method="post" @submit.prevent="UpdateSemen">

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
</template>

<script lang="ts">
    import Vue from 'vue';
    import Component from 'vue-class-component';
    import { ValidationObserver } from 'vee-validate';
    import swal from 'sweetalert2';
    import { AppPosService, SemenModel } from '../Service/NSwagService';

    @Component({
        created: async function (this: UpdateBarang) {
            await this.GetSpecificSemen();
        }
    })
    export default class UpdateBarang extends Vue {
        service: AppPosService = new AppPosService();

        newSemen: SemenModel = {
            barangId: 0,
            barangName: '',
            barangPrice: 0,
            barangStock: 0
        }

        async GetSpecificSemen(): Promise<void> {
            const urlparams = new URLSearchParams(window.location.search);
            const semenid = urlparams.get('semenID')?.valueOf();
            if (semenid != null) {
                this.newSemen = await this.service.getspecificsemen(parseInt(semenid));
            }
        }

        async UpdateSemen(): Promise<void> {
            const valid = await (this.$refs.observer as InstanceType<typeof ValidationObserver>).validate();
            if (valid == false) {
                return;
            }
            swal.fire({
                title: 'Are you sure?',
                text: "Update this data",
                icon: 'warning',
                showCancelButton: true,
                confirmButtonColor: '#3085d6',
                cancelButtonColor: '#d33',
                confirmButtonText: 'Yes, update it!'
            }).then(async (result) => {
                if (result.value) {

                    await this.service.updatesemen(this.newSemen.barangId, this.newSemen.barangName, this.newSemen.barangPrice, this.newSemen.barangStock)
                    swal.fire(
                        'Updated!',
                        'Your file has been updated.',
                        'success'
                    ).then(function () {
                        window.location.href = '/barang/listbarang';
                    });
                }
            })
        }



    }
</script>
