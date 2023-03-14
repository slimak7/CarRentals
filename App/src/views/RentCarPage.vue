<template>
    <div>
        <h2>Choose location</h2>

        <div class="container">
            <div class="d-flex mt-5 justify-content-center">
                <div v-for="(item, index) in locations" v-bind:key="index">
                    <div class="card text-white bg-primary mb-3">
                        <h5 class="card-header">{{item.locationName}}</h5>
                        <div class="card-body">
                            <h5 class="card-title">{{item.city}}</h5>
                            <p class="card-text">"{{item.street}} {{item.zipcode}}"</p>
                            <a v-if='selectedLocation != item' class="btn btn-primary" @click="selectLocation(item)">Select</a>
                            <a v-else class="btn btn-primary disabled">Selected</a>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div v-if="selectedLocation">
            <label>Choose date range</label>
            <div class="d-flex mt-5 justify-content-center">

                <div>
                    <b-form-datepicker id="fromDatepicker" v-model="fromDate" class="mb-2" :min="min" :max="max" locale="en"></b-form-datepicker>
                </div>
                <div>
                    <b-form-datepicker id="toDatepicker" v-model="toDate" class="mb-2" :min="min" :max="max" locale="en"></b-form-datepicker>
                </div>
            </div>
        </div>
        <div v-if="fromDate && toDate">
            <a class="btn btn-primary" @click="searchForCars">Search</a>           
        </div>
        <div v-if="availableCarsModels">
            <div class="container">
                <div class="d-flex mt-4 justify-content-center">
                    <div v-for="(item, index) in availableCarsModels" v-bind:key="index">
                        <div class="card text-white bg-primary mb-3">
                            <h5 class="card-header">{{item.modelName}}</h5>
                            <div class="card-body">
                                <h5 class="card-title">{{item.modelRange}} km</h5>
                                <p class="card-text">"{{item.acceleration}} s to 100 km/h</p>
                                <p class="card-text">up to {{item.maxNumberOfSeats}} seats</p>
                                <p class="card-text">{{item.pricePerDay}} <i class="bi bi-currency-euro"></i> per day</p>
                                <a v-if='selectedCar != item' class="btn btn-primary" @click="selectCar(item)">Select</a>
                                <a v-else class="btn btn-primary disabled">Selected</a>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div v-if="totalCost">
            <div class="container">
                <div class="d-flex mt-4 justify-content-center">
                    <div class="card text-white bg-primary mb-3">
                        <h5 class="card-header">Total cost</h5>
                        <div class="card-body">
                            <p class="card-text">{{totalCost}} <i class="bi bi-currency-euro"></i></p>
                            <a class="btn btn-primary" @click="makeReservation">Book</a>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        </div>
</template>

<script>
    import LocationService from '../services/locations'
    import ReservationsService from '../services/reservations'
export default {
        name: 'RentCarPage',
        data() {
            const now = new Date()
            const today = new Date(now.getFullYear(), now.getMonth(), now.getDate())
            const minDate = new Date(today)
            minDate.setDate(today.getDate() + 1)
            const maxDate = new Date(today)
            maxDate.setMonth(today.getMonth() + 2)
            const maxDay = new Date(today.getFullYear(), today.getMonth() + 1, 0)
            maxDate.setDate(maxDay.getDate())
            return {
                locations: [],
                selectedLocation: null,
                fromDate: '',
                toDate: '',
                min: minDate,
                max: maxDate,
                availableCarsModels: [],
                selectedCar: null
            }
        },
        computed: {

            totalCost() {
                if  (this.fromDate && this.toDate && this.selectedCar) {

                    const startingDate = new Date(this.fromDate);
                    const endingDate = new Date(this.toDate);

                    return (((endingDate.getTime()-startingDate.getTime())/(24*3600*1000)) + 1) * this.selectedCar.pricePerDay;
                }
                else return null;
            }
        },
        async created() {

            await LocationService.getAllLocations().then((response) => {
                
                this.locations = response.data.locations;
            }).catch((error) => {
                if (error.response.status == 401) {
                    
                    this.$store.dispatch('auth/logout')
                    this.$router.push('/')
                    window.location.reload();
                }
            });
            
            
        },
        methods: {

            selectLocation(location) {
                this.selectedLocation = location;
                this.selectedCar = null;
                this.availableCarsModels = [];
            },
            selectCar(car) {
                this.selectedCar = car;
            },
            makeReservation() {


            },
            async searchForCars() {

                await ReservationsService.getAvailableCars(this.selectedLocation.locationID, this.fromDate, this.toDate).then((response) => {
                    this.availableCarsModels = response.data.cars;
                    
                }).catch((error) => {
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
    .card {
        width: 300px !important;
        margin: 2px
        
    }
    .card a {
        background-color: darkblue;
    }
    
</style>
