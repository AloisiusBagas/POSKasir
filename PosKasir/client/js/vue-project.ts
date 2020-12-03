import Vue, { VNode } from 'vue';
import { FontAwesomeIcon } from '@fortawesome/vue-fontawesome';

// register child components usable inside Top-Level Components:
// https://vuejs.org/v2/guide/components-registration.html#Global-Registration
// https://vuejs.org/v2/guide/components-registration.html#Local-Registration
Vue.component('fa-icon', FontAwesomeIcon);
// https://vuejs.org/v2/guide/components-dynamic-async.html#Async-Components
Vue.component('validation-provider', () => import('vee-validate').then(esm => esm.ValidationProvider));
Vue.component('validation-observer', () => import('vee-validate').then(esm => esm.ValidationObserver));

/**
 * A factory function returning a Promise of Vue Single-File Component.
 */
type VueAsyncComponent = () => Promise<typeof import('*.vue')>;

/**
 * For each matching HTML Elements, render and mount a Vue Async Component without props.
 * @param selector HTML Element selector query
 * @param lazyComponent Vue Async Component
 */
function renderAsyncComponent(selector: string, lazyComponent: VueAsyncComponent): void {
    for (const el of document.querySelectorAll(selector)) {
        new Vue({
            el: el,
            render: function (h): VNode {
                return h(lazyComponent);
            }
        });
    }
}

renderAsyncComponent('list-transactionuser', () => import('./components/ListTransactionUser.vue'));
renderAsyncComponent('update-barangsqlite', () => import('./components/UpdateBarangSqlite.vue'));
renderAsyncComponent('list-barangsqlite', () => import('./components/ListBarangSqlite.vue'));
renderAsyncComponent('update-barang', () => import('./components/UpdateBarang.vue'));
renderAsyncComponent('list-barang', () => import('./components/ListBarang.vue'));
renderAsyncComponent('user-register', () => import('./components/UserRegister.vue'));
renderAsyncComponent('detail-transaction', () => import('./components/DetailTransaction.vue'));
renderAsyncComponent('list-transaction', () => import('./components/ListTransaction.vue'));
renderAsyncComponent('hello-world', () => import('./components/HelloWorld.vue'));
// now <hello-world></hello-world> can be invoked in DOM!
// add more components to be rendered in DOM here ^
