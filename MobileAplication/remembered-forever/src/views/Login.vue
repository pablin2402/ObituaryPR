 <template>
  <ion-page>
    <link
      href="https://fonts.googleapis.com/css2?family=Josefin+Sans:wght@400;700&display=swap"
      rel="stylesheet"
    />
    <ion-header class="ion-no-border">
      <ion-toolbar>
        <div class="back-circle"></div>
        <ion-title class="ion-text-center custom-font">Ingresar</ion-title>
      </ion-toolbar>
    </ion-header>

    <ion-content fullscreen="true">
      <svg
        class="back-blob"
        viewBox="0 0 200 200"
        xmlns="http://www.w3.org/2000/svg"
      >
        <path
          fill="#8295A3"
          d="M68.6,-33.5C81.6,-17.2,79.6,13.8,65.7,37.6C51.8,61.3,25.9,77.8,2.1,76.6C-21.7,75.4,-43.5,56.5,-54.2,34.6C-65,12.7,-64.8,-12.3,-53.9,-27.4C-43,-42.4,-21.5,-47.6,3.2,-49.4C27.9,-51.2,55.7,-49.7,68.6,-33.5Z"
          transform="translate(100 100)"
        />
      </svg>

      <div class="ion-padding">
        <form class="ion-padding">
          <div class="wrap-input">
            <input
              class="input"
              type="text"
              name="user-email"
              v-model="email"
              placeholder="Correo Electrónico"
            />
          </div>

          <div class="wrap-input">
            <input
              v-model="password"
              class="input"
              type="password"
              name="pass"
              placeholder="Contraseña"
            />
          </div>
          <div class="container-form-btn">
            <button class="form-btn custom-font" @click="ingresar">
              Entrar
            </button>
          </div>
        </form>
      </div>
    </ion-content>

    <ion-footer class="ion-no-border">
      <ion-toolbar>
        <p class="ion-text-center custom-font">
          ¿No tienes una cuenta? <a href="/register">Registrate, es gratis </a>
        </p>
      </ion-toolbar>
    </ion-footer>
  </ion-page>
</template>


<script lang="ts">
import {
  IonPage,
  IonHeader,
  IonToolbar,
  IonTitle,
  IonContent,
} from "@ionic/vue";
import axios from "axios";

export default {
  name: "Login",
  components: {
    IonPage,
    IonHeader,
    IonToolbar,
    IonTitle,
    IonContent,
  },

  data: () => ({
    email: "",
    password: "",
  }),
  methods: {
    async ingresar(this: any) {
      // this.$router.push({ path: "/register" });

      const res = await axios.post("https://localhost:44383/api/People/Login", {
        email: this.email,
        password: this.password,
      });
      console.log(`Status code: ${res.status}`);
      this.$router.push({ name: "view" });
    },
  },
};
</script>
<style lang="scss" src="./Login.scss" scoped></style>

