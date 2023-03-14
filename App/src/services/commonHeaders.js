
class CommonHeaders {

    getCommonHeaderJSONContent() {
   
        return {
            'Content-Type': 'application/json',
            Accept: 'application/json',
        }
    }

    getCommonGetHeaderWithType() {
   
        return {
            'Content-Type': 'application/json',
            Accept: 'application/json',
            Role: this.$store.state.auth.user.userType
        }
    }
}

export default new CommonHeaders();
   
