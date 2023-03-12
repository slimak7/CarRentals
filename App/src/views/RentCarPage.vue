<template>
    <div>
        <h3>Choose location</h3>

        <div class="container">
            <div class="d-flex mt-5 justify-content-center">
                <div v-for="(item, index) in locations" v-bind:key="index">
                    <div class="card text-white bg-primary mb-3">
                        <h5 class="card-header">{{item.locationName}}</h5>
                        <div class="card-body">
                            <h5 class="card-title">{{item.city}}</h5>
                            <p class="card-text">"{{item.street}} {{item.zipcode}}"</p>
                            <a href="#" class="btn btn-primary">Select</a>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</template>

<script>
import LocationService from '../services/locations'
export default {
        name: 'RentCarPage',
        data() {
            return {
                locations: [],
                token: this.$store.state.auth.user.token
            }
        },
        async created() {

            var response = await LocationService.getAllLocations();
            this.locations = response.data.locations;
            console.log(this.locations);
            
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
        margin: 10px
        
    }
    .card a {
        background-color: darkblue
    }
</style>
