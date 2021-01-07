<template>
  <v-layout align-start>
    <v-flex>
      <v-row>
        <v-col cols="12">
          <v-card color="rgba(203, 170, 92, 0.51)" class="pa-3">
            <v-container>
              <v-container>
                <v-row>
                  <v-col cols="12" md="4" lg="12">
                    <p class="display-1 text--primary">
                      {{ name }} {{ apellido }}
                    </p>
                  </v-col>
                  <v-col cols="12" md="4">
                    {{ description }}
                  </v-col>

                  <v-col cols="12" md="4" offset-md="1">
                    <v-img
                      :src="this.$route.params.id.imagen1"
                      max-height="300"
                      max-width="2397"
                      class="imgRedonda"
                    ></v-img>
                  </v-col>
                </v-row>
              </v-container>
            </v-container>
          </v-card>
        </v-col>
        <v-col
          cols="12"
          sm="4"
          ls="12"
          md="4"
          class="pa-3 d-flex flex-column"
          v-for="post of files"
          :key="post.idcondolencia"
        >
          <template v-if="post.condicion == true">
            <v-card class="elevation-5 flex d-flex flex-column" max-width="500">
              <div class="d-flex flex-no-wrap justify-space-between">
                <div>
                  <v-card-title class="headline">{{
                    post.titulo
                  }}</v-card-title>
                  <v-card-subtitle>
                    {{ post.fecha | moment("dddd, MMMM Do YYYY, h:mm:ss a") }}
                  </v-card-subtitle>
                  <v-card-subtitle>
                    {{ post.mensaje }}
                  </v-card-subtitle>

                  <v-card-subtitle
                    >Creado por :
                    <strong> {{ post.usuario }}</strong></v-card-subtitle
                  >
                </div>
              </div>
            </v-card>
          </template>
        </v-col>
      </v-row>
      <v-container id="social" tag="section">
        <v-row>
          <v-col cols="12" md="4">
            <h3>Galeria de fotos</h3>
            <gallery
              :images="images"
              :index="index"
              @close="index = null"
            ></gallery>
            <div
              class="image"
              v-for="(image, imageIndex) in images"
              :key="imageIndex"
              @click="index = imageIndex"
              :style="{
                backgroundImage: 'url(' + image + ')',
                width: '200px',
                height: '200px',
              }"
            ></div>
          </v-col>

          <v-col cols="12" md="4"> </v-col>
          <v-col cols="12" md="4">
            <v-card class="mx-auto" style="max-width: 2500px">
              <v-toolbar cards dark flat>
                <v-card-title class="title font-weight-regular">
                  Crear un Memorial</v-card-title
                >
                <v-spacer></v-spacer>
              </v-toolbar>
              <v-form class="pa-4 pt-6">
                <v-row align="center">
                  <v-col class="d-flex" cols="12" sm="12">
                    <v-text-field
                      v-model="titulo"
                      filled
                      shaped
                      :rules="[rules.required]"
                      color="deep-purple"
                      label="Titulo:"
                    ></v-text-field>
                  </v-col>
                  <v-col class="d-flex" cols="12" sm="12">
                    <v-textarea
                      label=""
                      auto-grow
                      outlined
                      rows="3"
                      row-height="25"
                      shaped
                      placeholder="Deja tu tributo aqui"
                      v-model="mensaje"
                      :rules="[rules.required]"
                    ></v-textarea>
                  </v-col>
                </v-row>
              </v-form>
              <v-divider></v-divider>
              <v-card-actions>
                <v-btn
                  class="white--text"
                  color="deep-purple accent-4"
                  depressed
                  @click="save()"
                >
                  CREAR
                </v-btn>
              </v-card-actions>
              <v-snackbar v-model="snackbar" color="success">
                {{ text }}

                <template v-slot:action="{ attrs }">
                  <v-btn
                    color="success"
                    text
                    v-bind="attrs"
                    @click="snackbar = false"
                  >
                    Cerrar
                  </v-btn>
                </template>
              </v-snackbar>
            </v-card>
          </v-col>
        </v-row>
      </v-container>
    </v-flex>
  </v-layout>
</template>

<script>
import axios from "axios";
import VueGallery from "vue-gallery";

export default {
  name: "DeceasedDetails",
  components: {
    gallery: VueGallery,
  },
  data() {
    return {
      images: [
        this.$route.params.id.imagen1,
        this.$route.params.id.imagen2,
        this.$route.params.id.imagen3,
        this.$route.params.id.imagen4,
        this.$route.params.id.imagen5,
      ],
      visible: false,

      idusuario: 0,
      idmuerto: "",
      titulo: "",
      mensaje: "",
      filler: "",
      index: null,

      text: "Tu tributo fue publicado",
      snackbar: false,
      rules: {
        length: (len) => (v) =>
          (v || "").length >= len ||
          `Invalid character length, required ${len}`,

        required: (v) => !!v || "Este campo es obligatorio",
      },
      files: [],
      name: this.$route.params.id.nombre,
      apellido: this.$route.params.id.apellido,

      description: this.$route.params.id.descripcion,
      imagen1: this.$route.params.id.imagen1,
      kl: this.$route.params.id.idmuerto,
      items: [
        {
          src: this.$route.params.id.imagen1,
        },
        {
          src: this.$route.params.id.imagen2,
        },
      ],
      loading: true,
    };
  },
  created() {
    this.list();
    this.getEmail();
  },

  async mounted() {
    await axios
      .get("People/BuscarPersona/" + this.filler)
      .then((response) => {
        //console.log("id:" + response.data[0].idusuario);
        this.idusuario = response.data[0].idusuario;
      })
      .catch((err) => {
        console.log(err);
      });
  },

  methods: {
    list() {
      axios
        .get("Condolences/GetbyId/" + this.kl)
        .then((response) => {
          console.log(response.data);
          this.files = response.data;
        })
        .catch((error) => {
          console.log(error);
        });
    },
    save() {
      let me = this;
      axios
        .post("Condolences/PostCondolence", {
          idusuario: Number(me.idusuario),
          idmuerto: this.$route.params.id.idmuerto,
          titulo: me.titulo,
          mensaje: me.mensaje,
        })
        .then(function () {
          me.snackbar = true;
          this.list();
          console.log("Exito");
        })
        .catch(function () {});
    },
    getEmail() {
      let datos = localStorage.getItem("correo");
      if (datos == null) {
        this.filler = "";
      } else {
        this.filler = datos;
      }
    },
    showImg(index) {
      this.index = index;
      this.visible = true;
    },
    handleHide() {
      this.visible = false;
    },
  },
};
</script>
<style scoped>
.imgRedonda {
  width: 300px;
  height: 300px;
  border-radius: 160px;
  border: 10px solid #666;
}
body {
  font-family: sans-serif;
}

.image {
  width: 100px;
  height: 100px;
  background-size: cover;
  cursor: pointer;
  margin: 5px;
  border-radius: 3px;
  border: 1px solid lightgray;
  object-fit: contain;
}
</style>