<template>
  <v-layout align-start>
    <v-flex>
      <v-carousel>
        <v-carousel-item
          v-for="(item, i) in items"
          :key="i"
          :src="item.src"
          reverse-transition="fade-transition"
          transition="fade-transition"
        ></v-carousel-item>
      </v-carousel>
      <v-row>
        <v-col cols="12" sm="2">
          <v-sheet rounded="lg" min-height="268">
            <!--  -->
          </v-sheet>
        </v-col>

        <v-col cols="12" sm="8">
          <v-sheet min-height="70vh" rounded="lg">
            <v-list-item-title class="headline mb-1"
              >{{ name }}
            </v-list-item-title>
          </v-sheet>
        </v-col>

        <v-col cols="12" sm="2">
          <v-sheet rounded="lg" min-height="268">
            <!--  -->
          </v-sheet>
        </v-col>
      </v-row>
    </v-flex>
  </v-layout>
</template><script>
import axios from "axios";

export default {
  name: "Details",

  props: "id",

  data() {
    return {
      singleMovie: "id",
      mortuary: [],
      name: this.$route.params.id.nombre,
      description: this.$route.params.id.descripcion,
      imagen: this.$route.params.id.imagen,
      imagen2: this.$route.params.id.imagen2,

      items: [
        {
          src: this.$route.params.id.imagen2,
        },
        {
          src: this.$route.params.id.imagen,
        },
      ],
    };
  },
  created() {
    this.select();
  },
  mounted() {
    axios
      .get("Articles/GetArticle/")
      .then((response) => {
        this.mortuary = response.data;
      })
      .catch((error) => {
        console.log(error);
      });
  },
  methods: {},
};
</script>