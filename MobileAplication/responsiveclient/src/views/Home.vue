<template>
  <v-layout align-start>
    <v-flex>
      <v-row dense>
        <v-col
          cols="12"
          sm="4"
          ls="12"
          v-for="filer of files"
          :key="filer.nombre"
        >
          <v-card class="mx-auto" max-width="344">
            <v-card-text>
              <div>BIENVENIDO</div>
              <p class="display-1 text--primary">{{ filer.nombre }}</p>
            </v-card-text>
            <v-card-actions>
              <router-link
                :to="{
                  name: 'accountdetails',
                  params: { email: filer.email },
                }"
              >
                <v-btn text color="teal accent-4" @click="reveal = true">
                  VER MIS DATOS
                </v-btn>
              </router-link>
            </v-card-actions>
          </v-card>
        </v-col>

        <funeraries />
        <categories />
        <subscribe />
      </v-row>
    </v-flex>
  </v-layout>
</template>

<script>
// @ is an alias to /src
import axios from "axios";

export default {
  name: "Home",
  components: {
    Subscribe: () => import("@/components/home/Subscribe"),
    Categories: () => import("@/components/home/Categories"),
    Funeraries: () => import("@/components/home/Funeraries"),
  },

  data() {
    return {
      files: [],
      email: this.$route.params.email,
      id: 0,
      info: null,
      errored: false,
    };
  },
  async mounted() {
    axios
      .get("People/BuscarPersona/" + this.email)
      .then((response) => {
        console.log(response);
        this.files = response.data;
        this.$store.dispatch("guardarCorreo", response.email);
      })
      .catch((err) => {
        console.log(err);
      });
  },
  methods: {},
};
</script>
