import Vue from 'vue'
import App from './App.vue'
import router from './router'
import Vuex from 'vuex'
import store from './store/index.js'
import 'bootstrap/dist/css/bootstrap.min.css'
import 'bootstrap/dist/css/bootstrap.css'
import 'bootstrap-vue/dist/bootstrap-vue.css'
import 'bootstrap-icons/font/bootstrap-icons.css'
import 'bootstrap-icons/font/bootstrap-icons'
import VueRouter from 'vue-router';
import BootstrapVue from 'bootstrap-vue'

Vue.prototype.$store = store;
Vue.use(BootstrapVue)
Vue.use(VueRouter);
Vue.use(Vuex);



new Vue({
    router,
    store,
    render: (h) => h(App),
}).$mount('#app');