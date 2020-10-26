import Vue from "vue";
import App from "./App.vue";
import router from "./router";
import vuetify from "./plugins/vuetify";
import axios from "axios";
import store from './store/store';

Vue.config.productionTip = false;
axios.defaults.baseURL = "https://localhost:44383/api/";
new Vue({
  router,
  store,
  vuetify,
  render: (h) => h(App),
}).$mount("#app");
