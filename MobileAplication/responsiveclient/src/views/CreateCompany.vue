<template>
  <v-layout align-start>
    <v-flex>
      <v-row justify="center">
        <v-col cols="12" sm="12" ls="12">
          <h2>Publica tu negocio</h2>
          <br />
          <v-snackbar v-model="snackbar">
            {{ text }}

            <template v-slot:action="{ attrs }">
              <v-btn color="pink" text v-bind="attrs" @click="snackbar = false">
                Cerrar
              </v-btn>
            </template>
          </v-snackbar>
          <v-snackbar v-model="snackbar1">
            {{ text1 }}

            <template v-slot:action="{ attrs }">
              <v-btn
                color="pink"
                text
                v-bind="attrs"
                @click="snackbar1 = false"
              >
                Cerrar
              </v-btn>
            </template>
          </v-snackbar>
          <v-stepper v-model="e1">
            <v-stepper-header>
              <v-stepper-step :complete="e1 > 1" step="1">
                Publica tu negocio
              </v-stepper-step>
            </v-stepper-header>

            <v-stepper-items>
              <v-stepper-content step="1">
                <v-card class="mb-12" height="300px">
                  <v-card-text>
                    <v-text-field
                      v-model="nombre"
                      label="Nombre de su negocio o empresa"
                      :rules="[() => !!nombre || 'El campo es requerido']"
                      required
                    ></v-text-field>
                    <v-text-field
                      v-model="descripcion"
                      label="Descripcion de su negocio o empresa"
                      :rules="[() => !!descripcion || 'El campo es requerido']"
                      required
                    ></v-text-field>
                    <v-select
                      v-model="idcategoria"
                      :items="categorias"
                      label="Categoría"
                      class="input-group--focused"
                    ></v-select>
                  </v-card-text>
                </v-card>
                <v-btn color="warning" text :to="{ name: 'home' }">
                  CANCELAR
                </v-btn>
                <v-btn color="primary" text @click="save">
                  PUBLICAR PRODUCTO
                </v-btn>
              </v-stepper-content>
            </v-stepper-items>
          </v-stepper>
        </v-col>
      </v-row>
    </v-flex>
  </v-layout>
</template>

<script>
import axios from "axios";

export default {
  name: "CreateCompany",
  components: {},
  data() {
    return {
      search: "",

      idcategoria: 0,
      idempresa: 0,
      nombre: "",
      descripcion: "",
      snackbar: false,
      snackbar1: false,
      filler: "",
      ids: 0,
      text: "Registrado con éxito",
      text1: "Error al crear la empresa",
      valid: true,
      rules: {
        required: (value) => !!value || "Requerido.",
        min: (v) => v.length >= 8 || "Minimo 8 caracteres",
      },
      categorias: [],
    };
  },

  mounted() {
    this.buscarPersona();
  },

  created() {
    this.getEmail();
    this.select();
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
          this.ids = response.data[0].idusuario;
          this.people = response.data;
        })
        .catch((err) => {
          console.log(err);
        });
    },
    async select() {
      let me = this;
      var categoriasArray = [];
      await axios
        .get("Categories/Select")
        .then(function (response) {
          categoriasArray = response.data;
          categoriasArray.map(function (x) {
            me.categorias.push({ text: x.nombre, value: x.idcategoria });
          });
        })
        .catch(function (error) {
          console.log(error);
        });
    },

    save() {
      let me = this;
      axios
        .post("Companies/PostCompanies", {
          nombre: me.nombre,
          descripcion: me.descripcion,
          idusuario: parseInt(me.ids),
          idcategoria: parseInt(me.idcategoria),
        })
        .then(function (response) {
          console.log(response);
          me.snackbar = true;
        })
        .catch(function (error) {
          console.log(error);
          me.snackbar1 = true;
        });
    },
  },
};
</script>
