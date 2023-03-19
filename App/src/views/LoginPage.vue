<template>
    <section>
        <div class="d-flex justify-content-center align-items-center">
            <div class="col-lg-6 card bg-dark text-white" style="border-radius: 1rem">
                <div class="p-5">
                    <h2 class="fw-bold text-uppercase mb-5">Log in</h2>

                    <LoginForm />

                    <div>
                        <p class="mb-0">
                            You don't have an account?
                            <a href="/register" class="text-white-50 fw-bold">Register</a>
                        </p>
                    </div>
                </div>
            </div>
        </div>
    </section>
</template>

<script>
    import LoginForm from '../components/LoginForm.vue'


    export default {
        name: 'LoginPage',
        components: {
            LoginForm,
        },
    }
</script>

<style scoped>
    section {
        margin-top: 25px;
    }
</style>
