import VueRouter from 'vue-router'


import HomePage from '../views/HomePage.vue'
import LoginPage from '../views/LoginPage.vue'
import RegisterPage from '../views/RegisterPage.vue'
import RentCarPage from '../views/RentCarPage.vue'
import MyReservationsPage from '../views/MyReservationsPage.vue'

const routes = [
    {
        path: '/',
        name: 'home',
        component: HomePage,
    },
    {
        path: '/login',
        name: 'login',
        component: LoginPage,
    },
    {
        path: '/register',
        name: 'register',
        component: RegisterPage,
    },
    {
        path: '/carRenting',
        name: 'carRenting',
        component: RentCarPage,
    },
    {
        path: '/myReservations',
        name: 'myReservations',
        component: MyReservationsPage,
    },
]

const router = new VueRouter({
    mode: 'history',
    routes: routes
});

export default router
