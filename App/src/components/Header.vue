<template>

    <div>
        <b-navbar type="dark" variant="dark">
            <b-navbar-brand href="/"><i class="bi bi-house"></i> Home </b-navbar-brand>
            <b-navbar-nav class="ml-auto right">
                <div v-if="!currentUser" class="nav-item">
                    <router-link class="nav-link" to="/login"><i class="bi bi-box-arrow-in-right"></i> Log in </router-link>
                </div>
                <div v-if="!currentUser" class="nav-item">
                    <router-link class="nav-link" to="/register"><i class="bi bi-person-plus"></i> Register </router-link>
                </div>
                <div v-if="currentUser" class="nav-item" @click="logout">
                    <span class="nav-link"><i class="bi bi-box-arrow-left"></i> Log out </span>
                </div>
                <div v-if="currentUser" class="nav-item">
                    <span class="nav-link disabled"> {{ currentUser.email }} </span>
                </div>
            </b-navbar-nav>
        </b-navbar>
    </div>
</template>

<script>


export default {
  name: 'MainHeader',
  computed: {
    currentUser() {
        return this.$store.state.auth.user
    }
  },
  methods: {
      logout() {
        this.$store.dispatch('auth/logout')
          this.$router.push('/')
          window.location.reload();
    }
  },
}
</script>

<style scoped>
    
    
    .nav-link {
        cursor: pointer;
    }

</style>
