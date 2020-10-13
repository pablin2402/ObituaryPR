<template>
  <v-app id="inspire">
    <v-navigation-drawer v-model="drawer" absolute temporary>
      <v-sheet rounded="lg">
        <v-list nav dense>
          <v-list-item-group active-class="deep-purple--text text--accent-4">
            <v-list-item :to="{ name: 'home' }">
              <v-list-item-action>
                <v-icon>home</v-icon>
              </v-list-item-action>
              <v-list-item-title>Inicio </v-list-item-title>
            </v-list-item>

            <v-divider class="my-2"></v-divider>
            <v-list-group :value="true" prepend-icon="mdi-account-circle">
              <template v-slot:activator>
                <v-list-item-title>WareHouse</v-list-item-title>
              </template>
              <v-list-item :to="{ name: 'categories' }">
                <v-list-item-action>
                  <v-icon>home</v-icon>
                </v-list-item-action>
                <v-list-item-title>Category </v-list-item-title>
              </v-list-item>
              <v-list-item :to="{ name: 'article' }">
                <v-list-item-action>
                  <v-icon>table_chart</v-icon>
                </v-list-item-action>
                <v-list-item-title>Article </v-list-item-title>
              </v-list-item>
            </v-list-group>
            <v-list-group :value="true" prepend-icon="mdi-account-circle">
              <template v-slot:activator>
                <v-list-item-title>Accesos</v-list-item-title>
              </template>
              <v-list-item :to="{ name: 'roles' }">
                <v-list-item-action>
                  <v-icon>table_chart</v-icon>
                </v-list-item-action>
                <v-list-item-title>Roles </v-list-item-title>
              </v-list-item>
            </v-list-group>
          </v-list-item-group>
        </v-list>
      </v-sheet>
    </v-navigation-drawer>
    <v-app-bar color="blue" dark>
      <v-app-bar-nav-icon @click="drawer = true"></v-app-bar-nav-icon>

      <v-toolbar-title>Obituary</v-toolbar-title>
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
        {{ new Date().getFullYear() }} — <strong>Obituary</strong>
      </v-col>
    </v-footer>
  </v-app>
</template>

<script>
export default {
  name: "App",

  components: {},

  data: () => ({ drawer: null }),
};
</script>
