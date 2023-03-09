import AuthService from '../services/auth'

const user = JSON.parse(localStorage.getItem('user'))

export const auth = {
    namespaced: true,
    state: {
        user: user
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
                    const newUser = { email: user.email, id: response.data.userID, token: response.data.token }
                    commit('login', newUser)
                    localStorage.setItem('user', JSON.stringify(newUser))

                    return Promise.resolve(response.data)
                },
                (error) => {
                    return Promise.reject(error)
                }
            )
        },
        logout({ commit }) {
            commit('logout')
            localStorage.removeItem('user')
        },
        register({ commit }, user) {
            return AuthService.register(user).then(
                (response) => {
                    const newUser = { email: user.email, id: response.data.userID, token: response.data.token }
                    commit('login', newUser)
                    localStorage.setItem('user', JSON.stringify(newUser))
                    return Promise.resolve(user)
                },
                (error) => {

                    return Promise.reject(error)
                }
            )
        },
    },

}
