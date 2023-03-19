<template>
    <div>
        <h2>All reservations</h2>

        <b-table striped hover :items="reservations">
            <template #head(carModelName)="data">
                <span class="text">Model</span>
            </template>
            <template #cell(totalCost)="data">
                <b class="text">{{ data.value }} <i class="bi bi-currency-euro"></i></b>
            </template>
        </b-table>
        <b-button variant="outline-primary" @click="showMore">Show more</b-button>
    </div>
</template>

<script>
    import ReservationsService from '../services/reservations'
    export default {
        name: 'MyReservationsPage',
        data() {
            return {

                reservations: [],
                from: 0,
                to: 10
            }
        },
        async created() {

            await ReservationsService.getAllReservations(this.from, this.to).then((response) => {


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
        methods: {

            async showMore() {

                this.from += 10;
                this.to += 10;

                await ReservationsService.getAllReservations(this.from, this.to).then((response) => {

                    response.data.reservations.forEach(x => {

                        this.reservations.push(x);
                    })


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
            }

        }
    }
</script>

<!-- Add "scoped" attribute to limit CSS to this component only -->
<style scoped>
    blockquote p {
        font-size: 2em !important;
    }

    .b-table {
        font-size: 13px;
    }
</style>
