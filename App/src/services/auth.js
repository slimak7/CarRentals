import axios from 'axios'

const API_URL = process.env.VUE_APP_BACKEND_URL

class AuthService {
    async login(user) {
        const url = API_URL;
        const response = await axios.post(url, {
            email: user.email,
            password: user.password,
        })
        return response;
    }

    async register(user) {
        
        const url = API_URL
        const response = await axios
            .post(url, {
                email: user.email,
                password: user.password,
                firstName: user.firstName,
                lastName: user.latName,
                phoneNumber: user.phoneNumber
            })

        return response
    }
}

export default new AuthService()
