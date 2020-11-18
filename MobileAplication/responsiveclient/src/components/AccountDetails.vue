<template>
  <v-layout align-start>
    <v-flex>
      <v-col
        cols="12"
        sm="4"
        ls="12"
        v-for="filer of files"
        :key="filer.nombre"
      >
        <v-card class="mx-auto" max-width="344">
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
            <div class="text--primary">VER MIS NEGOCIOS</div>
          </v-card-text>
        </v-card>
      </v-col>
    </v-flex>
  </v-layout>
</template>
<script>
import axios from "axios";
export default {
  data() {
    return {
      mortuary: [],
      files: [],

      id: 0,
      info: null,
      loading: true,
      errored: false,
    };
  },
  async mounted() {
    await axios
      .get("People/BuscarPersona/" + this.$route.params.email)
      .then((response) => {
        console.log(response);
        this.files = response.data;
        this.sendMyAccount();
      })
      .catch((err) => {
        console.log(err);
      });
  },
};
</script>