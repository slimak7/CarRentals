import AuthService from '../services/auth'

const user = JSON.parse(localStorage.getItem('user'))

export const auth = {
    namespaced: true,
    state: {
        user: user,
    },
    mutations: {
        login(state, user) {
            state.user = user
        },
        logout(state) {
            state.user = null
        },
    },
    actions: {
        login({ commit }, user) {
            return AuthService.login(user).then(
                (response) => {
                    commit('login', response.data)
                    return Promise.resolve(response.data)
                },
                (error) => {
                    return Promise.reject(error)
                }
            )
        },
        logout({ commit }) {            
            commit('logout')
        },
        register({ commit }, user) {
            return AuthService.register(user).then(
                (response) => {
                    commit('login', response.data)
                    return Promise.resolve(user)
                },
                (error) => {
                    return Promise.reject(error)
                }
            )
        },
    },
}
