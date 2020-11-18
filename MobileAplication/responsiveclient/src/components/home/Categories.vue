<template>
  <v-container id="subscribe" tag="section">
    <h1>CATEGORIAS</h1>
    <v-sheet class="mx-auto" elevation="8" max-width="1200">
      <v-slide-group v-model="model" class="pa-4" show-arrows>
        <v-slide-item
          v-for="post of flowers"
          :key="post.idcategoria"
          v-slot="{ active, toggle }"
        >
          <v-card
            :color="active ? 'primary' : 'grey lighten-1'"
            class="ma-4"
            max-width="400"
            @click="toggle"
          >
            <v-row class="fill-height" align="center" justify="center">
              <v-scale-transition>
                <v-img :src="post.imagen" rounded></v-img>
                <v-card-title>{{ post.nombre }}</v-card-title>
              </v-scale-transition>
            </v-row>
          </v-card>
        </v-slide-item>
      </v-slide-group>

      <v-expand-transition>
        <v-sheet v-if="model != null" height="200" tile>
          <v-row class="fill-height" align="center" justify="center">
            <h3 class="title">Selected {{ model }}</h3>
          </v-row>
        </v-sheet>
      </v-expand-transition>
    </v-sheet>
  </v-container>
</template>
<script>
import axios from "axios";
export default {
  name: "Categories",
  components: {},
  data() {
    return {
      flowers: [],
      id: 0,
      loading: false,
      model: null,
    };
  },
  mounted() {
    axios.get("Categories/SelectImage").then((response) => {
      console.log(response);

      this.flowers = response.data;
    });
  },
};
</script>