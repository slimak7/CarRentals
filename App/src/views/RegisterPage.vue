<template>
    <section>
        <div class="d-flex justify-content-center align-items-center">
            <div class="col-lg-6 card bg-dark text-white" style="border-radius: 1rem">
                <div class="p-5">
                    <h2 class="fw-bold text-uppercase mb-5">Register</h2>

                    <RegisterForm />

                    <div>
                        <p class="mb-0">
                            Already have an account?
                            <a href="/login" class="text-white-50 fw-bold">Log in</a>
                        </p>
                    </div>
                </div>
            </div>
        </div>
    </section>
</template>

<script>
    import RegisterForm from '../components/RegisterForm.vue'

    export default {
        name: 'RegisterPage',
        components: {
            RegisterForm,
        },
    }
</script>

<style scoped>
    section {
        margin-top: 25px;
    }
</style>
