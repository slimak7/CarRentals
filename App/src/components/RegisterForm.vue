<template>
    <form @submit.prevent="register">
        <div class="form-outline form-white mb-4">
            <label class="form-label text-white-50" for="typeEmailX">{{ this.emailLabel }}</label>
            <input id="email" v-model="email" type="text" class="form-control form-control-lg" />
        </div>

        <div class="form-outline form-white mb-4">
            <label class="form-label text-white-50" for="typePasswordX">{{ this.passwordLabel }}</label>
            <input id="password"
                   v-model="password"
                   type="password"
                   class="form-control form-control-lg" />
        </div>

        <div class="form-outline form-white mb-4">
            <label class="form-label text-white-50" for="typePasswordX">{{ this.firstNameLabel }}</label>
            <input id="firstName"
                   v-model="firstName"
                   type="text"
                   class="form-control form-control-lg" />
        </div>

        <div class="form-outline form-white mb-4">
            <label class="form-label text-white-50" for="typePasswordX">{{ this.lastNameLabel }}</label>
            <input id="lastName"
                   v-model="lastName"
                   type="text"
                   class="form-control form-control-lg" />
        </div>

        <div class="form-outline form-white mb-4">
            <label class="form-label text-white-50" for="typePasswordX">{{ this.phoneNumberLabel }}</label>
            <input id="phoneNumber"
                   v-model="phoneNumber"
                   type="text"
                   class="form-control form-control-lg" />
        </div>

        <div v-if="errorMessage" class="error">
            <p>{{ errorMessage }}</p>
        </div>

        <p v-if="infoVisibility" class="mb-3">
            {{ $t('login.account-created') }}
            <a href="/login" class="text-white-50 fw-bold">{{ this.loginLabel }}</a>
        </p>

        <button class="btn btn-outline-light btn-lg px-5 mb-4" type="submit">
            {{ this.registerLabel }}
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
      firstName: '',
      firstNameLabel: '',
      lastName: '',
      lastNameLabel: '',
      phoneNumber: '',
      phoneNumberLabel: '',
      loginLabel: '',
      registerLabel: '',
      errorMessage: '',
      infoVisibility: false,
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
    this.loginLabel = 'Login'
    this.firstNameLabel = 'First name'
    this.lastNameLabel = 'Last name'
    this.phoneNumberLabel = 'Phone number'
    this.registerLabel = 'Submit'
  },
  methods: {
    async register() {
      this.errorMessage = ''

      let user = {
        email: this.email,
        password: this.password,
        firstName: this.firstName,
        lastName: this.lastName,
        phoneNumber: this.phoneNumber
      }

    this.$store.dispatch('auth/register', user).then(
        () => {
            window.location.reload();
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