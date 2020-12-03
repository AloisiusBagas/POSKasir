<template>
    <div>
        <div>
            <h1>Registration Form</h1>
            <validation-observer ref="observer" v-slot="{validate,valid}">
                <form method="post" @submit.prevent="insertuser">

                    <label class="col-form-label">Username</label>
                    <validation-provider name="Username" rules="required" v-slot="{errors}">
                        <input class="form-control" type="text" v-model="newuser.userName" />
                        <span class="text-danger font-italic">{{errors[0]}}</span>
                    </validation-provider>
                    <br />

                    <label class="col-form-label">Password</label>
                    <validation-provider name="Password" rules="required|min:5|" v-slot="{errors}">
                        <input class="form-control" type="password" v-model="newuser.password" />
                        <span class="text-danger font-italic">{{errors[0]}}</span>
                    </validation-provider>
                    <br />

                    <label class="col-form-label">Confirm Password</label>
                    <validation-provider name="Confirm" rules="required|min:5" v-slot="{errors}">
                        <input class="form-control" type="password" v-model="newuser.confirmPassword" />
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
    import Swal from 'sweetalert2';
    import { ValidationObserver } from 'vee-validate';
    import { AppPosService, RegisterFormModel } from '../Service/NSwagService';

    @Component({

    })

    export default class UserRegister extends Vue {
        service: AppPosService = new AppPosService();

        newuser: RegisterFormModel = {
            userName: '',
            password: '',
            confirmPassword: '',
        }

        async insertuser(): Promise<void> {
            const valid = await (this.$refs.observer as InstanceType<typeof ValidationObserver>).validate();
            if (valid == false) {
                return
            }

            if (this.newuser.password != this.newuser.confirmPassword) {
                Swal.fire({
                    title: 'Failed!',
                    icon: 'error',
                    text: 'Confirmation Password does not match'
                });
            }
            else {
                await this.service.insertuser(this.newuser);
                Swal.fire({
                    title: 'Success!',
                    icon: 'success',
                    text: 'Your registration is successful!.'
                }).then(function () {
                    window.location.href = '/auth/login';
                });

            }
        }
    }
</script>
