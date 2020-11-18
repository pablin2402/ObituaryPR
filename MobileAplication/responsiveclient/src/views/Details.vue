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
        <div>
          <b-jumbotron>
            <template #header>{{ name }}</template>

            <template #lead>
              {{ description }}
            </template>

            <hr class="my-4" />

            <p>
              It uses utility classes for typography and spacing to space
              content out within the larger container.
            </p>

            <b-button variant="primary" href="#">Do Something</b-button>
            <b-button variant="success" href="#">Do Something Else</b-button>
          </b-jumbotron>
        </div>
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