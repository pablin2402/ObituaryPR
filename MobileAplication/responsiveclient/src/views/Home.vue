<template>
  <v-layout align-start>
    <v-flex>
      <v-row dense>
        <v-col cols="12" sm="12" ls="12">
          <funeraries />
        </v-col>
        <v-col cols="12" sm="12" ls="12">
          <h1>Lista de Funerarias de la Zona</h1>
          <funeraries-maps />
        </v-col>
        <categories />
        <subscribe />
      </v-row>
    </v-flex>
  </v-layout>
</template>

<script>
// @ is an alias to /src

export default {
  name: "Home",
  components: {
    Subscribe: () => import("@/components/home/Subscribe"),
    FunerariesMaps: () => import("@/components/home/FunerariesMaps"),

    Categories: () => import("@/components/home/Categories"),
    Funeraries: () => import("@/components/home/Funeraries"),
  },

  data() {
    return {};
  },
};
</script>
