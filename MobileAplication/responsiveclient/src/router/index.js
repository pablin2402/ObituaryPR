import Vue from "vue";
import VueRouter from "vue-router";
import Flowers from "../views/Flowers.vue";
import Home from "../views/Home.vue";
import store from "../store/store";
import Login from "../components/Login.vue";
import AccountDetails from "../components/AccountDetails.vue";

import Register from "../components/Register.vue";
import Create from "../views/Create.vue";
import Details from "../views/Details.vue";
import CreateItem from "../views/CreateItem.vue";
import CreateCompany from "../views/CreateCompany.vue";

import Deceased from "../views/Deceased.vue";
import CreateObituary from "../components/CreateObituary";
import DeceasedDetails from "../views/DeceasedDetails.vue";
import Categories from "../components/home/Categories.vue";
import Funeraries from "../components/home/Funeraries.vue";
import ProfileCondolences from "../components/profile/ProfileCondolences.vue";

import CreateMortuary from "../views/CreateMortuary.vue";
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
    },
        props: true,

  },
  {
    path: "/categories",  
    name: "categories",
    component: Categories,
    meta:{
      administrador:true,
      almacenero:true,
      vendedor:true
    }
  },
   {
    path: "/profilecondolences",  
    name: "profilecondolences",
    component: ProfileCondolences,
    meta:{
      administrador:true,
      almacenero:true,
      vendedor:true
    }
  },
   {
    path: "/createmortuary",  
    name: "createmortuary",
    component: CreateMortuary,
    meta:{
      administrador:true,
      almacenero:true,
      vendedor:true
    }
  },
  {
    path: "/funeraries",  
    name: "funeraries",
    component: Funeraries,
    meta:{
      administrador:true,
      almacenero:true,
      vendedor:true
    }
  },
  {
    path: "/accountdetails",  
    name: "accountdetails",
    component: AccountDetails,
    meta:{
      administrador:true,
      almacenero:true,
      vendedor:true
    }
  },
   {
    path: "/deceased",  
    name: "deceased",
    component: Deceased,
    meta:{
      administrador:true,
      almacenero:true,
      vendedor:true
    }
  },
  {
    path: "/createobituary",  
    name: "createobituary",
    component: CreateObituary,
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
    path: "/deceaseddetails/:id",  
    name: "deceaseddetails",
    props: true,
    component: DeceasedDetails,
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
    path: "/createcompany",  
    name: "createcompany",
    component: CreateCompany,
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
      libre:true,

    }
  },
  {
    path: "/login",
    name: "login",
    component: Login,
    meta:{
      libre:true,
     },

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
