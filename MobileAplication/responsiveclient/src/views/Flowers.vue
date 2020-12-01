<template>
  <v-layout align-start>
    <v-flex>
      <v-row align="stretch" class="ma-2">
        <v-col
          cols="12"
          sm="4"
          ls="12"
          class="pa-3 d-flex flex-column"
          v-for="post of flowers"
          :key="post.idcategoria"
        >
          <v-card
            :loading="loading"
            class="elevation-5 flex d-flex flex-column"
            width="400"
          >
            <template slot="progress">
              <v-progress-linear
                color="deep-purple"
                height="10"
                indeterminate
              ></v-progress-linear>
            </template>
            <template v-if="post.condicion == true">
              <v-img :src="post.imagen"></v-img>

              <v-card-title>{{ post.nombre }}</v-card-title>

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
                  <strong>{{ post.precio_venta }} Bs.</strong>
                </div>

                <div>
                  {{ post.descripcion }}
                </div>
              </v-card-text>

              <v-divider class="mx-4"></v-divider>

              <v-card-title>Horario de atención </v-card-title>

              <v-card-text>
                <v-chip-group
                  active-class="deep-purple accent-4 white--text"
                  column
                >
                  <v-chip>5:30PM</v-chip>
                </v-chip-group>
              </v-card-text>

              <v-card-actions>
                <v-btn color="deep-purple lighten-2" text>
                  AÑADIR AL CARRITO
                </v-btn>
              </v-card-actions>
            </template>
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
  name: "Flowers",
  components: {},
  data() {
    return {
      flowers: [],
      id: 0,
      loading: false,
    };
  },
  mounted() {
    axios.get("Articles/List").then((response) => {
      console.log(response);

      this.flowers = response.data;
    });
  },
};
</script>
