import { auth } from './auth.js'
import Vuex from 'vuex'
import Vue from 'vue'

Vue.use(Vuex)

const store = new Vuex.Store({
    modules: {
        auth,
    },
})

export default store