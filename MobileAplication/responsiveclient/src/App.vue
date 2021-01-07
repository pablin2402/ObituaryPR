<template>
  <v-app id="inspire">
    <v-navigation-drawer v-model="drawer" absolute temporary v-if="logueado">
      <v-sheet rounded="lg">
        <v-list nav dense>
          <v-list-item-group active-class="deep-purple--text text--accent-4">
            <template>
              <v-list-item>
                <v-list-item-action>
                  <v-icon></v-icon>
                </v-list-item-action>
                <v-list-item-title>{{ files }} - {{ rol }}</v-list-item-title>
              </v-list-item>
              <v-list-item :to="{ name: 'home' }">
                <v-list-item-action>
                  <v-icon>mdi-home</v-icon>
                </v-list-item-action>
                <v-list-item-title>Inicio </v-list-item-title>
              </v-list-item>
              <v-list-item :to="{ name: 'accountdetails' }">
                <v-list-item-action>
                  <v-icon>mdi-account</v-icon>
                </v-list-item-action>
                <v-list-item-title>Mis Datos </v-list-item-title>
              </v-list-item>
            </template>
            <v-divider class="my-2"></v-divider>
            <v-list-item :to="{ name: 'deceased' }">
              <v-list-item-action> </v-list-item-action>
              <v-list-item-title>Encontrar un obituario </v-list-item-title>
            </v-list-item>
            <v-list-item :to="{ name: 'flowers' }">
              <v-list-item-action> </v-list-item-action>
              <v-list-item-title>Flores </v-list-item-title>
            </v-list-item>
            <v-list-item :to="{ name: 'createobituary' }">
              <v-list-item-action> </v-list-item-action>
              <v-list-item-title>CREAR OBITUARIO </v-list-item-title>
            </v-list-item>

            <v-list-item :to="{ name: 'create' }">
              <v-list-item-action> </v-list-item-action>
              <v-list-item-title>PUBLICA TU NEGOCIO</v-list-item-title>
            </v-list-item>
          </v-list-item-group>
        </v-list>
      </v-sheet>
    </v-navigation-drawer>
    <v-card class="overflow-hidden">
      <v-app-bar color="yellow lighten-4">
        <v-app-bar-nav-icon @click="drawer = true"></v-app-bar-nav-icon>

        <v-toolbar-title class="text-center justify-center py-6"
          ><h3 class="font-weight-bold display-1 basil--text">
            REMEMBERED FOREVER
          </h3></v-toolbar-title
        >
        <v-spacer></v-spacer>

        <v-btn @click="salir" v-if="logueado"> Salir </v-btn>
        <v-btn :to="{ name: 'login' }" v-else> Ingresar </v-btn>
        <template v-slot:extension>
          <v-tabs
            align-with-title
            v-model="tab"
            background-color="transparent"
            color="basil"
            grow
          >
            <v-tab :to="{ name: 'home' }">INICIO</v-tab>
            <v-tab :to="{ name: 'flowers' }">ENVÍE FLORES</v-tab>
            <v-tab :to="{ name: 'deceased' }">OBITUARIO</v-tab>
          </v-tabs>
        </template>
      </v-app-bar>
      <v-main>
        <v-container fluid fill-height>
          <router-view />
        </v-container>
      </v-main>
    </v-card>
  </v-app>
</template>

<script>
import axios from "axios";

export default {
  name: "App",

  components: {},

  data: () => ({
    drawer: null,
    tab: null,
    files: "",
    filler: "",
    rol: "",
  }),
  computed: {
    logueado() {
      return this.$store.state.usuario;
    },
  },
  created() {
    this.$store.dispatch("autoLogin");
    this.getcorreo();
  },
  async mounted() {
    await axios
      .get("People/BuscarPersona/" + this.filler)
      .then((response) => {
        this.files = response.data[0].nombre;
        this.rol = response.data[0].rol;
      })
      .catch((err) => {
        console.log(err);
      });
  },
  methods: {
    salir() {
      localStorage.removeItem("correo");
      this.$store.dispatch("salir");
    },
    getcorreo() {
      let datos = localStorage.getItem("correo");
      if (datos == null) {
        this.filler = "";
      } else {
        this.filler = datos;
      }
    },
  },
};
</script>
<style>
/* Helper classes */
.basil {
  background-color: #fffbe6 !important;
}
.basil--text {
  color: #356859 !important;
}
</style>