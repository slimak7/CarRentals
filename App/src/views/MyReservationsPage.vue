<template>
    <div>
        <h2>My reservations</h2>

        <b-table striped hover :items="reservations">
            <template #head(carModelName)="data">
                <span class="text">Model</span>
            </template>
            <template #cell(totalCost)="data">
                <b class="text">{{ data.value }} <i class="bi bi-currency-euro"></i></b>
            </template>
        </b-table>
    </div>
</template>

<script>
    import ReservationsService from '../services/reservations'
    export default {
        name: 'MyReservationsPage',
        data() {
            return {

                reservations: []
            }
        },
        async created() {

            await ReservationsService.getMyReservations().then((response) => {

                this.reservations = response.data.reservations;

            }).catch(error => {
                if (error.response.status == 401) {

                    this.$store.dispatch('auth/logout')
                    this.$router.push('/')
                    window.location.reload();
                }
                else {

                    console.log(error);
                }
            })

        },
    }
</script>

<!-- Add "scoped" attribute to limit CSS to this component only -->
<style scoped>
    blockquote p {
        font-size: 2em !important;
    }
</style>
