<template>
    <div>
        <blockquote class="blockquote">
            <p class="mb-3">Mallorca Car Renting</p>
            <footer class="blockquote-footer">Rent Your dreamed Tesla today!</footer>
        </blockquote>

        <b-carousel ref="TeslaImagesCarousel"
                    id="carousel-1"
                    :interval="1500"
                    fade
                    indicators>

            <b-carousel-slide>
                <template #img>
                    <img class="d-block img-fluid w-100"
                         src="../assets/tesla1.png"
                         alt="image slot">
                </template>
            </b-carousel-slide>
            <b-carousel-slide>
                <template #img>
                    <img class="d-block img-fluid w-100"
                         src="../assets/tesla2.jpg"
                         alt="image slot">
                </template>
            </b-carousel-slide>
            <b-carousel-slide>
                <template #img>
                    <img class="d-block img-fluid w-100"
                         src="../assets/tesla3.jpg"
                         alt="image slot">
                </template>
            </b-carousel-slide>
        </b-carousel>
    </div>
</template>

<script>
export default {
        name: 'HomePage',
        created() {


            if (this.$store.state.auth.user && this.$store.state.auth.user.userType == 'Client') {
                this.$router.push('/carRenting')
            }
        }
}
</script>

<style scoped>
    blockquote p {
        font-size: 2em !important;
    }
    #carousel-1.carousel.slide {
        width: 50% !important;
        margin: auto;
        border-radius: 50px;
        overflow: hidden;        
    }
    
</style>
