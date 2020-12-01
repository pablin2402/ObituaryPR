<template>
  <v-layout align-start>
    <v-flex>
      <br />
      <h3>MIS DATOS</h3>
      <br />
      <v-col
        cols="12"
        sm="12"
        ls="12"
        v-for="filer of people"
        :key="filer.nombre"
      >
        <v-card class="elevation-5 flex d-flex flex-column" max-width="344">
          <v-card-text>
            <p class="display-1 text--primary">{{ filer.nombre }}</p>
            <p>Dirección :</p>
            <div class="text--primary">
              {{ filer.direccion }}
            </div>
            <p>Teléfono :</p>
            <div class="text--primary">
              {{ filer.telefono }}
            </div>
            <p>Correo Electrónico :</p>
            <div class="text--primary">
              {{ filer.email }}
            </div>
            <p>Número de documento:</p>
            <div class="text--primary">
              {{ filer.num_documento }}
            </div>
            <p>Tipo de Documento:</p>
            <div class="text--primary">
              {{ filer.tipo_documento }}
            </div>
          </v-card-text>
        </v-card>
      </v-col>
      <br />
      <h3>MIS PUBLICACIONES</h3>
      <br />

      <v-row align="stretch" class="ma-2">
        <v-col
          cols="12"
          sm="4"
          ls="12"
          class="pa-3 d-flex flex-column"
          v-for="condolence of condolences"
          :key="condolence.titulo"
        >
          <v-card class="elevation-5 flex d-flex flex-column" max-width="344">
            <v-card-text>
              <p class="display-1 text--primary">{{ condolence.titulo }}</p>
              <p>Nombre del fallecido :</p>
              <div class="text--primary">
                {{ condolence.fallecido }}
              </div>
              <p>Mensaje :</p>
              <div class="text--primary">
                {{ condolence.mensaje }}
              </div>
              <p>Fecha de publicación :</p>
              <div class="text--primary">
                {{ condolence.fecha | moment("calendar") }}
              </div>
            </v-card-text>
          </v-card>
        </v-col>
      </v-row>
    </v-flex>
  </v-layout>
</template>
<script>
import axios from "axios";
export default {
  data() {
    return {
      condolences: [],
      people: [],
      filler: "",
      id: 0,
      info: null,
      loading: true,
      errored: false,
    };
  },
  mounted() {
    this.buscarPersona();
  },

  created() {
    this.getEmail();
  },

  methods: {
    getEmail() {
      let datos = localStorage.getItem("correo");
      if (datos == null) {
        this.filler = "";
      } else {
        this.filler = datos;
      }
    },
    async buscarPersona() {
      await axios
        .get("People/BuscarPersona/" + this.filler)
        .then((response) => {
          this.id = response.data[0].idusuario;
          this.people = response.data;
          this.buscarCondolencias();
        })
        .catch((err) => {
          console.log(err);
        });
    },
    buscarCondolencias() {
      axios
        .get("Condolences/GetbyUser/" + parseInt(this.id))
        .then((response) => {
          this.condolences = response.data;
          console.log("gi " + response);
        })
        .catch((err) => {
          console.log(err);
        });
    },
  },
};
</script>