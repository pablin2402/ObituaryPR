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
            <br />
            <br />

            <template #lead>
              <strong>Ofrecemos los siguientes servicios:</strong>
              {{ description }}
            </template>

            <hr class="my-4" />

            <h3>
              {{ servicios }}
            </h3>
            <br />
            <p><strong>Telefono:</strong></p>
            <h4>{{ telefono }}</h4>

            <p><strong>Direccion:</strong></p>
            <h4>{{ direccion }}</h4>
          </b-jumbotron>
        </div>
      </v-row>
      <h3>Nuestra ubicación:</h3>
      <Maps :latitude="latitud" :longitude="longitud" :title="name" />
    </v-flex>
  </v-layout>
</template><script>
import axios from "axios";
import Maps from "../components/home/Maps";

export default {
  name: "Details",
  props: "id",
  components: {
    Maps,
  },
  data() {
    return {
      center: { lat: 100, lng: 100 },
      markers: [
        {
          position: { lat: 100, lng: 100 },
          title: "caca",
        },
      ],
      singleMovie: "id",
      mortuary: [],
      name: this.$route.params.id.nombre,
      description: this.$route.params.id.descripcion,
      imagen: this.$route.params.id.imagen,
      imagen2: this.$route.params.id.imagen2,
      latitud: this.$route.params.id.posicionx,
      longitud: this.$route.params.id.posiciony,
      servicios: this.$route.params.id.servicios,
      telefono: this.$route.params.id.telefono,
      direccion: this.$route.params.id.direccion,

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
  created() {},
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