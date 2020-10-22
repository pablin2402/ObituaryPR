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

Vue.use(VueRouter);

const routes = [
  {
    path: "/",
    name: "home",
    component: Home,
  },
  {
    path: "/categories",
    name: "categories",
    component: Categories,
  },
  {
    path: "/article",
    name: "article",
    component: Article,
  },
  {
    path: "/roles",
    name: "roles",
    component: Rol,
  },
  {
    path: "/users",
    name: "users",
    component: User,
  },
   {
    path: "/clients",
    name: "clients",
    component: Client,
  },
  {
    path: "/proveedores",
    name: "proveddores",
    component: Proveedores,
  },
  {
    path: "/login",
    name: "login",
    component: Login,
  },
];

const router = new VueRouter({
  routes,
});

export default router;
