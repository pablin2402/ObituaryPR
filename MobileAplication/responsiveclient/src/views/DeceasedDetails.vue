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
          v-for="caca of files"
          :key="caca.idcondolencia"
        >
          <v-card class="mx-auto my-12" max-width="500">
            <div class="d-flex flex-no-wrap justify-space-between">
              <div>
                <v-card-title class="headline">{{ caca.titulo }}</v-card-title>
                <v-card-subtitle>
                  {{ caca.fecha }}
                </v-card-subtitle>
                <v-card-subtitle>
                  {{ caca.mensaje }}
                </v-card-subtitle>

                <v-card-subtitle>Creado por {{ caca.usuario }}</v-card-subtitle>
              </div>
            </div>
          </v-card>
        </v-col>
      </v-row>
      <v-container id="social" tag="section">
        <v-row>
          <v-col cols="12" md="4">
            <p class="display-1 text--primary">Galería</p>

            <v-row>
              <v-col v-for="(post, i) in posts" :key="i" cols="4">
                <v-card
                  :href="post.href"
                  color="grey lighten-2"
                  height="88"
                  tag="a"
                >
                  <v-img :src="post.src" height="100%" />
                </v-card>
              </v-col>
            </v-row>
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
                      v-model="idusuario"
                      filled
                      color="deep-purple"
                      label="Código Usuario"
                    ></v-text-field
                  ></v-col>
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
              <v-snackbar v-model="snackbar">
                {{ text }}

                <template v-slot:action="{ attrs }">
                  <v-btn
                    color="pink"
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
</template><script>
import axios from "axios";

export default {
  name: "DeceasedDetails",

  data() {
    return {
      posts: [
        { src: this.$route.params.id.imagen1 },
        { src: this.$route.params.id.imagen2 },
        { src: this.$route.params.id.imagen3 },
        { src: this.$route.params.id.imagen4 },
        { src: this.$route.params.id.imagen5 },
      ],

      idusuario: 0,
      idmuerto: "",
      titulo: "",
      mensaje: "",

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
  },

  async mounted() {},
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
</style>