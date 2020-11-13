import Vue from "vue";
import VueRouter from "vue-router";
import Flowers from "../views/Flowers.vue";
import Home from "../views/Home.vue";
import store from "../store/store";
import Login from "../components/Login.vue";
import Register from "../components/Register.vue";
import Create from "../views/Create.vue";
import Details from "../views/Details.vue";
import CreateItem from "../views/CreateItem.vue";
Vue.use(VueRouter);

var router = new VueRouter({
  mode: 'history',
  base: process.env.BASE_URL,
  routes: [
  {
    path: "/",  
    name: "home",
    component: Home,
    meta:{
      administrador:true,
      almacenero:true,
      vendedor:true
    }
  },
  {
    path: "/details/:id",  
    name: "details",
    props: true,
    component: Details,
    meta:{
      administrador:true,
      almacenero:true,
      vendedor:true
    }
  },
  {
    path: "/createitem",  
    name: "createitem",
    component: CreateItem,
    meta:{
      administrador:true,
      almacenero:true,
      vendedor:true
    }
  },
   {
    path: "/create",  
    name: "create",
    component: Create,
    meta:{
      administrador:true,
      almacenero:true,
      vendedor:true
    }
  },
  {
    path: "/flowers",
    name: "flowers",
    component: Flowers,
    meta:{
      administrador:true,
      almacenero:true,
      vendedor:true
    }
  },
    {
    path: "/register",
    name: "register",
    component: Register,
    meta:{
      administrador:true,
      almacenero:true,
      vendedor:true
    }
  },
  {
    path: "/login",
    name: "login",
    component: Login,
    meta:{
      libre:true,
     }
  }]
})

router.beforeEach((to, from, next) => {
  if (to.matched.some(record => record.meta.libre)){
    next()
  } else if (store.state.usuario && store.state.usuario.rol== 'Administrador'){
    if (to.matched.some(record => record.meta.administrador)){
      next()
    }
  }else if (store.state.usuario && store.state.usuario.rol== 'Almacenero'){
    if (to.matched.some(record => record.meta.almacenero)){
      next()
    }
  }else if (store.state.usuario && store.state.usuario.rol== 'Vendedor'){
    if (to.matched.some(record => record.meta.vendedor)){
      next()
    }
  } else{
    next({
      name: 'login'
    })
  }
})

export default router;
