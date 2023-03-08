<template>
    <form @submit.prevent="login">
        <div class="form-outline form-white mb-4">
            <label class="form-label text-white-50" for="typeEmailX">{{ this.emailLabel }}</label>
            <input id="email" v-model="email" type="text" class="form-control form-control-lg" />
        </div>

        <div class="form-outline form-white mb-5">
            <label class="form-label text-white-50" for="typePasswordX">{{ this.passwordLabel }}</label>
            <input id="password"
                   v-model="password"
                   type="password"
                   class="form-control form-control-lg" />
        </div>

        <div v-if="errorMessage" class="error">
            <p>{{ errorMessage }}</p>
        </div>

        <button class="btn btn-outline-light btn-lg px-5 mb-4" type="submit">
            {{ this.loginLabel }}
        </button>
    </form>
</template>

<script>
export default {
  data() {
    return {
      email: '',
      emailLabel: '',
      password: '',
      passwordLabel: '',
      loginLabel: '',
      errorMessage: '',
    }
  },
  computed: {
    getUser() {
      return this.$store?.state?.auth?.user
    },
  },
  created() {
    if (this.getUser) {
      this.$router.push('/')
    }

    this.emailLabel = 'Email'
    this.passwordLabel = 'Password'
    this.loginLabel = 'Submit'
  },
  methods: {
    async login() {
      this.errorMessage = ''

      let user = {
        email: this.email,
        password: this.password,
      }

      this.$store.dispatch('auth/login', user).then(
        () => {
          this.$router.push('/')
        },
        (err) => {
          this.errorMessage = err?.response?.data.errors[0];
          console.error(err)
        }
      )
    },
  },
}
</script>

<style scoped>
</style>