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
      <h3>Alertas de posiles fallecidos</h3>
      <br />
      <funeraryalert />
      <br />
      <h3>MIS PUBLICACIONES</h3>
      <br />
      <profilecondolences />
      <br />
      <h3>MIS COMPAÑIAS</h3>
      <br />
      <v-row align="stretch" class="ma-2">
        <v-col
          cols="12"
          sm="4"
          ls="12"
          class="pa-3 d-flex flex-column"
          v-for="condolence of companies"
          :key="condolence.titulo"
        >
          <v-card class="elevation-5 flex d-flex flex-column" max-width="344">
            <v-card-text>
              <p class="display-1 text--primary">{{ condolence.nombre }}</p>
              <p>Categoría de la empresa:</p>
              <div class="text--primary">
                <strong>{{ condolence.categoria }}</strong>
              </div>
              <p>Descripción:</p>

              <div class="text--primary">
                <strong> {{ condolence.descripcion }} </strong>
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
  components: {
    profilecondolences: () =>
      import("@/components/profile/ProfileCondolences.vue"),
    funeraryalert: () => import("@/components/profile/FuneraryAlert.vue"),
  },
  data() {
    return {
      condolences: [],
      companies: [],
      people: [],
      filler: "",
      id: 0,
      titulo: "",
      mensaje: "",
      fecha: "",
      info: null,
      loading: true,
      errored: false,
      empresauid: 0,
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
          this.seekCondolences();
          this.seekCompanies();
        })
        .catch((err) => {
          console.log(err);
        });
    },
    seekCondolences() {
      axios
        .get("Condolences/GetbyUser/" + parseInt(this.id))
        .then((response) => {
          this.condolences = response.data;
        })
        .catch((err) => {
          console.log(err);
        });
    },
    seekCompanies() {
      axios
        .get("Companies/GetbyUser/" + parseInt(this.id))
        .then((response) => {
          this.companies = response.data;
          console.log("gi " + response);
        })
        .catch((err) => {
          console.log(err);
        });
    },
  },
};
</script>