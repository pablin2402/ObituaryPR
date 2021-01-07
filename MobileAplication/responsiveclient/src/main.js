import Vue from "vue";
import App from "./App.vue";
import router from "./router";
import store from "./store/store";
import vuetify from "./plugins/vuetify";
import axios from "axios";
import { BootstrapVue, IconsPlugin } from 'bootstrap-vue';
import 'bootstrap/dist/css/bootstrap.css'
import 'bootstrap-vue/dist/bootstrap-vue.css'
import './custom.scss';
import * as VueGoogleMaps from "vue2-google-maps";

Vue.config.productionTip = false;
axios.defaults.baseURL = "https://localhost:44383/api/";
Vue.use(BootstrapVue);
Vue.use(IconsPlugin);
Vue.use(require('vue-moment'));

Vue.use(VueGoogleMaps, {
  load: {
    key: "AIzaSyCNmukHTcFfWJcYE0mWKGAWC5PKCq9uC2w",
  },
  //AIzaSyCNmukHTcFfWJcYE0mWKGAWC5PKCq9uC2w
});
new Vue({
  router,
  store,
  vuetify,
  render: h => h(App)
}).$mount("#app");
