 <template>
  <ion-page>
    <link
      href="https://fonts.googleapis.com/css2?family=Josefin+Sans:wght@400;700&display=swap"
      rel="stylesheet"
    />
    <ion-header class="ion-no-border">
      <ion-toolbar>
        <div class="back-circle"></div>
        <ion-title class="ion-text-center custom-font">Registro</ion-title>
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
              v-model="nombre"
              class="input"
              type="text"
              name="nombre"
              placeholder="Nombre Completo"
            />
          </div>
          <div class="wrap-input">
            <input
              v-model="tipo_documento"
              class="input"
              type="text"
              name="tipo_documento"
              placeholder="Tipo de Documento"
            />
          </div>
          <div class="wrap-input">
            <input
              v-model="num_documento"
              class="input"
              type="text"
              name="num_documento"
              placeholder="Número de Documento"
            />
          </div>
          <div class="wrap-input">
            <input
              v-model="direccion"
              class="input"
              type="text"
              name="direccion"
              placeholder="Dirección"
            />
          </div>
          <div class="wrap-input">
            <input
              v-model="email"
              class="input"
              type="text"
              name="email"
              placeholder="Correo Electrónico"
            />
          </div>
          <div class="wrap-input">
            <input
              v-model="telefono"
              class="input"
              type="number"
              name="telefono"
              placeholder="Teléfono"
            />
          </div>

          <div class="wrap-input">
            <input
              v-model="password"
              class="input"
              type="password"
              name="password"
              placeholder="Contraseña"
            />
          </div>

          <div class="container-form-btn">
            <button class="form-btn custom-font" @click="save">
              Registrarse
            </button>
          </div>
        </form>
      </div>
    </ion-content>

    <ion-footer class="ion-no-border">
      <ion-toolbar>
        <p class="ion-text-center custom-font">
          ¿Tienes una cuenta? <a href="/login">Ingresa </a>
        </p>
      </ion-toolbar>
    </ion-footer>
  </ion-page>
</template>

<script lang="ts">
import axios from "axios";

import {
  IonPage,
  IonHeader,
  IonToolbar,
  IonTitle,
  IonContent,
  IonFooter,
} from "@ionic/vue";

export default {
  name: "Registrar",
  components: {
    IonHeader,

    IonToolbar,
    IonTitle,
    IonContent,
    IonPage,
    IonFooter,
  },

  data: () => ({
    username: "",
    submitted: false,
    /* eslint-disable @typescript-eslint/camelcase */
    num_documento: "",
    id: 0,
    /* eslint-disable @typescript-eslint/camelcase */
    tipo_documento: "",
    documentos: ["DNI", "PASAPORTE", "CEDULA"],

    direccion: "",
    telefono: "",
    email: "",
    password: "",
    nombre: "",
    descripcion: "",
  }),
  methods: {
    save(this: any) {
      axios
        .post("https://localhost:44383/api/Users/Crear", {
          idrol: "Vendedor",
          nombre: this.nombre,
          tipo_documento: this.tipo_documento,
          num_documento: this.num_documento,
          direccion: this.direccion,
          telefono: this.telefono,
          email: this.email,
          password: this.password,
        })
        .then(function (response) {
          console.log(response);
          console.log("Exito");
          // me.close();
          // me.listCategories();
          // me.clean();
          //this.snackbar = true;
        })
        .catch(function (error) {
          //this.snackbar = true;
          console.log("caca");
          console.log(error);
        });
    },
  },
};
</script>

<style lang="scss" src="./Login.scss" scoped></style>
