import Vue from "vue";
import VueRouter from "vue-router";
import Home from "../views/Home.vue";
import Categories from "../components/Categories.vue";
import Article from "../components/Article.vue";
import Rol from "../components/Rol.vue";
import User from "../components/User.vue";
import Client from "../components/Client.vue";
import Proveedores from "../components/Proveedores.vue";
import Login from "../components/Login.vue";
import Entry from "../components/Entry.vue";
import Sales from "../components/Sales.vue";

import store from "../store/store";
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
    path: "/categories",
    name: "categories",
    component: Categories,
    meta:{
      administrador:true,
      almacenero:true,
    }
  },
  {
    path: "/article",
    name: "article",
    component: Article,
    meta:{
      administrador:true,
    }
  },
  {
    path: "/roles",
    name: "roles",
    component: Rol,
    meta:{
      administrador:true,
    }
  },
  {
    path: "/users",
    name: "users",
    component: User,
    meta:{
      administrador:true,
    }
  },
   {
    path: "/clients",
    name: "clients",
    component: Client,
    meta:{
      administrador:true,
      almacenero:true,
      vendedor:true
    }
  },
  {
    path: "/proveedores",
    name: "proveedores",
    component: Proveedores,
    meta:{
      administrador:true,
      almacenero:true,
      vendedor:true
    }
  },
  {
    path: "/ingresos",
    name: "ingresos",
    component: Entry,
    meta:{
      administrador:true,
      almacenero:true,
      vendedor:true
    }
  },
   {
    path: "/ventas",
    name: "ventas",
    component: Sales,
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

export default router
