<template>
  <v-container id="subscribe" tag="section">
    <h1>CATEGORIAS</h1>
    <v-sheet class="mx-auto" elevation="8" max-width="1200">
      <v-slide-group v-model="model" active-class="success" show-arrows>
        <v-slide-item
          v-for="post of categories"
          :key="post.idcategoria"
          v-slot="{ active, toggle }"
        >
          <v-card
            :color="active ? undefined : 'grey lighten-1'"
            class="ma-4"
            max-width="400"
            @click="toggle"
          >
            <v-row class="fill-height" align="center" justify="center">
              <router-link :to="{ name: post.link }">
                <v-card-title>{{ post.nombre }}</v-card-title>
              </router-link>
              <v-scale-transition>
                <v-img :src="post.imagen" rounded></v-img>
              </v-scale-transition>
            </v-row>
          </v-card>
        </v-slide-item>
      </v-slide-group>
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
      categories: [],
      id: 0,
      link: "funeraries",
      loading: false,
      model: null,
    };
  },
  mounted() {
    axios.get("Categories/SelectImage").then((response) => {
      this.categories = response.data;
    });
  },
};
</script>