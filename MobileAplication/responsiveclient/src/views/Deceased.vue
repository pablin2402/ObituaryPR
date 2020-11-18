<template>
  <v-layout align-start>
    <v-flex>
      <v-row dense>
        <v-container v-if="loading">
          <div class="text-xs-center">
            <v-progress-circular
              indeterminate
              :size="150"
              :width="8"
              color="green"
            >
            </v-progress-circular>
          </div>
        </v-container>

        <v-col
          cols="12"
          sm="4"
          ls="12"
          v-for="post of mortuary"
          :key="post.idpersona"
        >
          <v-card :loading="loading" class="mx-auto my-12" max-width="500">
            <div class="d-flex flex-no-wrap justify-space-between">
              <div>
                <router-link
                  :to="{
                    name: 'deceaseddetails',
                    params: { id: post },
                  }"
                >
                  <v-card-title class="headline"
                    >{{ post.nombre }} {{ post.apellido }}</v-card-title
                  >
                </router-link>
                <v-card-subtitle>
                  ( {{ post.fecha_nacimiento }} - {{ post.fecha_muerte }} )
                </v-card-subtitle>
                <v-card-subtitle>
                  Este sitio web conmemorativo fue creado en memoria de nuestro
                  ser querido, {{ post.nombre }}
                </v-card-subtitle>

                <v-card-subtitle
                  >Creado por {{ post.usuario }}
                  {{ post.idmuerto }}</v-card-subtitle
                >
              </div>

              <v-avatar class="ma-3" size="125" tile>
                <v-img :src="post.imagen1"></v-img>
              </v-avatar>
            </div>
          </v-card>
        </v-col>
      </v-row>
    </v-flex>
  </v-layout>
</template>

<script>
// @ is an alias to /src
import axios from "axios";

export default {
  name: "Deceased",
  components: {},

  data() {
    return {
      mortuary: [],
      files: [],
      email: this.$route.params.email,
      id: 0,
      info: null,
      loading: true,
      errored: false,
    };
  },
  async mounted() {
    await axios
      .get("DeathPeoples/GetDeathPeople")
      .then((response) => {
        this.accountdetails();
        this.mortuary = response.data;
        this.loading = false;
      })
      .catch((error) => {
        console.log(error);
        this.errored = true;
      })
      .finally(() => (this.loading = false));
  },
  methods: {
    accountdetails() {
      axios
        .get("People/BuscarPersona/" + this.email)
        .then((response) => {
          console.log(response);
          this.files = response.data;
        })
        .catch((err) => {
          console.log(err);
        });
    },
  },
};
</script>
