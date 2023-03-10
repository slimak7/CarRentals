import { auth } from './auth.js'
import Vuex from 'vuex'
import Vue from 'vue'
import createPersistedState from 'vuex-persistedstate';

Vue.use(Vuex)

export default new Vuex.Store({
    modules: {
        auth,
    },
    plugins: [createPersistedState()]
})
