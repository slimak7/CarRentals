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
            <a class="btn btn-primary">Search</a>           
        </div>

    </div>
</template>

<script>
import LocationService from '../services/locations'
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
                fromDate: null,
                toDate: null,
                min: minDate,
                max: maxDate
            }
        },
        async created() {

            var response = await LocationService.getAllLocations();

            if (response.status == 401) {
                this.$store.dispatch('auth/logout')
                this.$router.push('/')
                window.location.reload();
            }

            this.locations = response.data.locations;
           
            
        },
        methods: {

            selectLocation(location) {
                this.selectedLocation = location;
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
