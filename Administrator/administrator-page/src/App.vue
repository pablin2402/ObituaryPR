<template>
  <v-app id="inspire">
    <v-navigation-drawer v-model="drawer" absolute temporary v-if="logueado">
      <v-sheet rounded="lg">
        <v-list nav dense>
          <v-list-item-group active-class="deep-purple--text text--accent-4">
            <template v-if="esAdministrador || esAlmacenero || esVendedor">
              <v-list-item :to="{ name: 'home' }">
                <v-list-item-action>
                  <v-icon>home</v-icon>
                </v-list-item-action>
                <v-list-item-title>INICIO </v-list-item-title>
              </v-list-item>
            </template>
            <v-divider class="my-2"></v-divider>
            <template v-if="esAdministrador || esAlmacenero">
              <v-list-group :value="true" prepend-icon="mdi-account-circle">
                <template v-slot:activator>
                  <v-list-item-title>SERVICIOS</v-list-item-title>
                </template>
                <v-list-item :to="{ name: 'categories' }">
                  <v-list-item-action>
                    <v-icon>shop_two</v-icon>
                  </v-list-item-action>
                  <v-list-item-title>CATEGORIAS </v-list-item-title>
                </v-list-item>
                <v-list-item :to="{ name: 'article' }">
                  <v-list-item-action>
                    <v-icon>spa</v-icon>
                  </v-list-item-action>
                  <v-list-item-title>ARTÍCULOS </v-list-item-title>
                </v-list-item>
                <v-list-item :to="{ name: 'mortuary' }">
                  <v-list-item-action>
                    <v-icon>local_hospital</v-icon>
                  </v-list-item-action>
                  <v-list-item-title>FUNERARIAS </v-list-item-title>
                </v-list-item>
              </v-list-group>
            </template>
            <template v-if="esAdministrador">
              <v-list-group :value="true" prepend-icon="mdi-account-circle">
                <template v-slot:activator>
                  <v-list-item-title>ACCESOS</v-list-item-title>
                </template>
                <v-list-item :to="{ name: 'roles' }">
                  <v-list-item-action>
                    <v-icon>people</v-icon>
                  </v-list-item-action>
                  <v-list-item-title>ROLES </v-list-item-title>
                </v-list-item>
                <v-list-item :to="{ name: 'users' }">
                  <v-list-item-action>
                    <v-icon>people</v-icon>
                  </v-list-item-action>
                  <v-list-item-title>USUARIOS </v-list-item-title>
                </v-list-item>

                <v-list-item :to="{ name: 'proveedores' }">
                  <v-list-item-action>
                    <v-icon>people</v-icon>
                  </v-list-item-action>
                  <v-list-item-title>EMPRESAS</v-list-item-title>
                </v-list-item>
              </v-list-group>
            </template>

            <template v-if="esAdministrador || esAlmacenero || esVendedor">
              <v-list-group :value="true" prepend-icon="mdi-account-circle">
                <template v-slot:activator>
                  <v-list-item-title>VENTAS</v-list-item-title>
                </template>

                <v-list-item :to="{ name: 'ingresos' }">
                  <v-list-item-action>
                    <v-icon>monetization_on</v-icon>
                  </v-list-item-action>
                  <v-list-item-title>INGRESOS</v-list-item-title>
                </v-list-item>
                <!--v-list-item :to="{ name: 'ventas' }">
                  <v-list-item-action>
                    <v-icon>shopping_cart</v-icon>
                  </v-list-item-action>
                  <v-list-item-title>VENTAS</v-list-item-title>
                </!--v-list-item-->
              </v-list-group>
            </template>
          </v-list-item-group>
        </v-list>
      </v-sheet>
    </v-navigation-drawer>
    <v-app-bar color="amber accent-2" dark>
      <v-app-bar-nav-icon @click="drawer = true"></v-app-bar-nav-icon>

      <v-toolbar-title
        color="grey"
        class="font-weight-bold display-1 basil--text"
        >REMEMBERED FOREVER</v-toolbar-title
      >
      <v-spacer></v-spacer>
      <v-btn @click="salir" v-if="logueado">
        <v-icon>logout</v-icon>Salir
      </v-btn>
      <v-btn :to="{ name: 'login' }" v-else>
        <v-icon>apps</v-icon>Ingresar
      </v-btn>
    </v-app-bar>

    <v-main>
      <v-container fluid fill-height>
        <v-slide-y-transition mode="out-in">
          <router-view />
        </v-slide-y-transition>
      </v-container>
    </v-main>
    <v-footer padless>
      <v-col class="text-center" cols="12">
        {{ new Date().getFullYear() }} — <strong>Remembered Forever</strong>
      </v-col>
    </v-footer>
  </v-app>
</template>

<script>
export default {
  name: "App",

  components: {},

  data: () => ({ drawer: null }),
  computed: {
    logueado() {
      return this.$store.state.usuario;
    },
    esAdministrador() {
      return (
        this.$store.state.usuario &&
        this.$store.state.usuario.rol == "Administrador"
      );
    },
    esAlmacenero() {
      return (
        this.$store.state.usuario &&
        this.$store.state.usuario.rol == "Almacenero"
      );
    },
    esVendedor() {
      return (
        this.$store.state.usuario && this.$store.state.usuario.rol == "Vendedor"
      );
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
