import AuthService from '../services/auth'

export const auth = {
    namespaced: true,
    state: {
        user: null
    },
    mutations: {
        login(state, user) {
            const newUser = { email: user.email, id: user.userID, token: user.token, userType: user.userType }
            state.user = newUser
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

                    if (error.status == 401) {
                        commit('logout');
                        window.location.reload();
                        this.$router.push('/')
                    }

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

                    if (error.status == 401) {
                        commit('logout');
                        window.location.reload();
                        this.$router.push('/')
                    }

                    return Promise.reject(error)
                }
            )
        },
    },

}
