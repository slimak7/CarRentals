import axios from 'axios'
import CommonHeaders from './commonHeaders.js'

const API_URL = process.env.VUE_APP_BACKEND_URL

class AuthService {
    async login(user) {
        const url = API_URL + 'Auth/Login';
        const response = await axios.post(url, {
            email : user.email,
            password: user.password,
        }, { headers: CommonHeaders.getCommonHeadersJSONContent })
        return response;
    }

    async register(user) {

        const url = API_URL + 'Auth/Register';
        const response = await axios
            .post(url, {
                email: user.email,
                password: user.password,
                firstName: user.firstName,
                lastName: user.lastName,
                phoneNumber: user.phoneNumber
            }, { headers: CommonHeaders.getCommonHeadersJSONContent })

        return response;
    }
}

export default new AuthService()
