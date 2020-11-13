<template>
  <v-app id="inspire">
    <v-navigation-drawer v-model="drawer" absolute temporary v-if="logueado">
      <v-sheet rounded="lg">
        <v-list nav dense>
          <v-list-item-group active-class="deep-purple--text text--accent-4">
            <template>
              <v-list-item :to="{ name: 'home' }">
                <v-list-item-action>
                  <v-icon>home</v-icon>
                </v-list-item-action>
                <v-list-item-title>Inicio </v-list-item-title>
              </v-list-item>
            </template>
            <v-divider class="my-2"></v-divider>
            <v-list-item :to="{ name: 'flowers' }">
              <v-list-item-action> </v-list-item-action>
              <v-list-item-title>Flores </v-list-item-title>
            </v-list-item>
            <v-list-item :to="{ name: 'createitem' }">
              <v-list-item-action> </v-list-item-action>
              <v-list-item-title>CREAR ARTÍCULO </v-list-item-title>
            </v-list-item>
            <v-list-item :to="{ name: 'register' }">
              <v-list-item-action> </v-list-item-action>
              <v-list-item-title>REGISTRO </v-list-item-title>
            </v-list-item>
            <v-list-item :to="{ name: 'create' }">
              <v-list-item-action> </v-list-item-action>
              <v-list-item-title>CREA TU ANUNCIO</v-list-item-title>
            </v-list-item>
          </v-list-item-group>
        </v-list>
      </v-sheet>
    </v-navigation-drawer>
    <v-card class="overflow-hidden">
      <v-app-bar
        color="yellow lighten-4
"
      >
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
            <v-tab>OBITUARIO</v-tab>
          </v-tabs>
        </template>
      </v-app-bar>
      <v-main>
        <v-container fluid fill-height>
          <v-slide-y-transition mode="out-in">
            <router-view />
          </v-slide-y-transition>
        </v-container>
      </v-main>
    </v-card>
  </v-app>
</template>

<script>
export default {
  name: "App",

  components: {},

  data: () => ({
    drawer: null,
    tab: null,
    items: ["Appetizers", "Entrees", "Deserts", "Cocktails"],
  }),
  computed: {
    logueado() {
      return this.$store.state.usuario;
    },
  },
  created() {
    this.$store.dispatch("autoLogin");
  },
  methods: {
    salir() {
      this.$store.dispatch("salir");
    },
  },
};
</script>
<style>
/* Helper classes */
.basil {
  background-color: #FFFBE6 !important;
}
.basil--text {
  color: #356859 !important;
}
</style>