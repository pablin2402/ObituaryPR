<template>
  <v-container id="subscribe" tag="section">
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
      <b-form-input
        type="search"
        v-model="selectedCategory"
        placeholder="Busca tu funeraria favorita"
      ></b-form-input>

      <v-col
        cols="12"
        sm="4"
        ls="12"
        v-for="post of filterfuneraries"
        :key="post.idpersona"
      >
        <v-card :loading="loading" class="mx-auto my-12" max-width="500">
          <template slot="progress">
            <v-progress-linear
              color="deep-purple"
              height="10"
              indeterminate
            ></v-progress-linear>
          </template>

          <v-img :src="post.imagen"></v-img>

          <router-link :to="{ name: 'details', params: { id: post } }">
            <v-card-title>{{ post.nombre }}</v-card-title>
          </router-link>
          <v-card-text>
            <v-row align="center" class="mx-0">
              <v-rating
                :value="4.5"
                color="amber"
                dense
                half-increments
                readonly
                size="14"
              ></v-rating>

              <div class="grey--text ml-4">4.5 (413)</div>
            </v-row>

            <div class="my-4 subtitle-1">
              <strong>Nuestra dirección: </strong>
            </div>

            <div class="my-4 subtitle-1">{{ post.direccion }}</div>
          </v-card-text>

          <v-divider class="mx-4"></v-divider>

          <v-card-title>Llamanos en cualquier momento </v-card-title>
          <h5 align="center">
            <v-icon color="blue" large>mdi-cellphone</v-icon>
            +591 - {{ post.telefono }}
          </h5>
          <br />
          <div class="text-center">
            <v-btn
              rounded
              large
              align="center"
              color="success"
              :to="{ name: 'details', params: { id: post } }"
              >VER DETALLES</v-btn
            >
          </div>
          <br />
        </v-card>
      </v-col>
    </v-row>
  </v-container>
</template>
<script>
import axios from "axios";
export default {
  name: "Funeraries",
  components: {},
  data() {
    return {
      mortuary: [],
      email: this.$route.params.email,
      id: 0,
      loading: true,

      info: null,
      errored: false,
      selectedCategory: "",
    };
  },
  async mounted() {
    await axios
      .get("Mortuaries/GetMortuaries")
      .then((response) => {
        this.mortuary = response.data;
        this.loading = false;
      })
      .catch((error) => {
        console.log(error);
        this.errored = true;
      })
      .finally(() => (this.loading = false));
  },
  computed: {
    filterfuneraries: function () {
      var self = this;
      var category = self.selectedCategory;
      if (category === "All") {
        return self.mortuary;
      } else {
        return this.mortuary.filter(function (lenguaje) {
          return (
            lenguaje.nombre
              .toLowerCase()
              .indexOf(self.selectedCategory.toLowerCase()) >= 0 ||
            lenguaje.direccion
              .toLowerCase()
              .indexOf(self.selectedCategory.toLowerCase()) >= 0
          );
        });
      }
    },
  },
};
</script>