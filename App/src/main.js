import Vue from 'vue/dist/vue.js'
import App from './App.vue'
import router from './router'
import axios from 'axios'
import Vuex from 'vuex'
import store from './store/index.js'
import 'bootstrap/dist/css/bootstrap.min.css'
import 'bootstrap'
import 'bootstrap-icons/font/bootstrap-icons.css'
import 'bootstrap-icons/font/bootstrap-icons'
import 'bootstrap-vue'
import VueRouter from 'vue-router';
import { BootstrapVue } from 'bootstrap-vue'


Vue.use(BootstrapVue)
Vue.use(VueRouter);
Vue.use(Vuex);
Vue.use(axios);


new Vue({
    router,
    store,
    render: (h) => h(App),
}).$mount('#app');