import Vue from "vue";
import VueRouter from "vue-router";
import Home from "../views/Home.vue";
import Categories from "../components/Categories.vue";
import Article from "../components/Article.vue";
import Rol from "../components/Rol.vue";

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
];

const router = new VueRouter({
  routes,
});

export default router;
