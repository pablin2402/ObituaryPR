import { createRouter, createWebHistory } from '@ionic/vue-router';
import { RouteRecordRaw } from 'vue-router';
import Tabs from '../views/Tabs.vue';
import ViewIMortuary from "../views/ViewIMortuary.vue";
import Login from "../views/Login.vue";
import Registrar from "../views/Registrar.vue";

const routes: Array<RouteRecordRaw> = [
  {
    path: '/',
    redirect: '/tabs/tab1'
  },
  {
    path: '/view',
    name: 'view',
    component: ViewIMortuary,
    props: true
  },
   {
    path: '/login',
    name: 'login',
    component: Login,
  },
  {
    path: '/caca',
    name: 'caca',
    component: () => import('../views/Mortuary.vue'),
    props: true
  },
  {
    path: '/register',
    name: 'register',
    component: Registrar,
    props: true
  },
  {
    path: '/tabs/',
    component: Tabs,
    children: [
      {
        path: '',
        redirect: 'tab1'
      },
      {
        path: 'tab1',
        component: () => import('@/views/Tab1.vue')
      },
      {
        path: 'tab2',
        component: () => import('@/views/Tab2.vue')
      },
      {
        path: 'tab3',
        component: () => import('@/views/Tab3.vue')
      },
     
      {
        path: 'funeraria',
        component: () => import('@/views/Mortuary.vue')
      },
      
    ]
  }
]

const router = createRouter({
  history: createWebHistory(process.env.BASE_URL),
  routes
})

export default router
