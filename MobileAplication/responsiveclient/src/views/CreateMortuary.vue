<template>
  <v-layout align-start>
    <v-flex>
      <v-row justify="center">
        <v-col cols="12" sm="12" ls="12">
          <h2>Publica tu Funeraria</h2>
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
                Datos de tu funeraria
              </v-stepper-step>

              <v-divider></v-divider>

              <v-stepper-step :complete="e1 > 2" step="2">
                Dirección de la funeraria
              </v-stepper-step>
              <v-stepper-step step="3">
                Imagenes de la funeraria
              </v-stepper-step>
            </v-stepper-header>

            <v-stepper-items>
              <v-stepper-content step="1">
                <v-card class="mb-12" height="600px">
                  <v-card-text>
                    <v-text-field
                      ref="codigo"
                      v-model="codigo"
                      label="Código"
                      :rules="[() => !!codigo || 'El campo es requerido']"
                      required
                    ></v-text-field>

                    <v-text-field
                      v-model="nombre"
                      label="Nombre"
                      required
                    ></v-text-field>

                    <v-text-field
                      ref="descripcion"
                      v-model="descripcion"
                      label="Descripcion"
                      :rules="[() => !!descripcion || 'El campo es requerido']"
                      required
                    ></v-text-field>
                    <v-text-field
                      ref="servicios"
                      v-model="servicios"
                      label="Servicios"
                      :rules="[() => !!servicios || 'El campo es requerido']"
                      required
                    ></v-text-field>
                  </v-card-text>
                </v-card>
                <v-btn color="primary" @click="e1 = 2"> CONTINUAR </v-btn>

                <v-btn text :to="{ name: 'home' }"> CANCELAR </v-btn>
              </v-stepper-content>

              <v-stepper-content step="2">
                <v-card class="mb-12" height="600px">
                  <v-card class="mb-12" height="1000px">
                    <v-text-field
                      ref="posicionx"
                      v-model="posicionx"
                      label="Posicion X"
                      :rules="[() => !!posicionx || 'El campo es requerido']"
                      required
                    ></v-text-field>
                    <v-text-field
                      ref="posiciony"
                      v-model="posiciony"
                      label="Posicion Y"
                      :rules="[() => !!posiciony || 'El campo es requerido']"
                      required
                    ></v-text-field>
                    <v-text-field
                      ref="direccion"
                      v-model="direccion"
                      label="Direccion"
                      :rules="[() => !!direccion || 'El campo es requerido']"
                      required
                    ></v-text-field>
                  </v-card>
                </v-card>
                <v-btn color="primary" @click="e1 = 1"> ATRÁS </v-btn>

                <v-btn color="primary" @click="e1 = 3"> CONTINUAR </v-btn>
              </v-stepper-content>
              <v-stepper-content step="3">
                <v-card class="mb-12" height="500px">
                  <v-text-field
                    ref="imagen"
                    v-model="imagen"
                    label="Imagen"
                    :rules="[() => !!imagen || 'El campo es requerido']"
                    required
                  ></v-text-field>
                  <v-img :src="imagen" height="200px"></v-img>
                  <v-text-field
                    ref="imagen2"
                    v-model="imagen2"
                    label="Imagen 2"
                    :rules="[() => !!imagen2 || 'El campo es requerido']"
                    required
                  ></v-text-field>
                  <v-img :src="imagen2" height="200px"></v-img>
                </v-card>
                <v-btn color="primary" @click="e1 = 2"> ATRÁS </v-btn>

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
// @ is an alias to /src
import axios from "axios";

export default {
  name: "CreateMortuary",
  components: {},
  data() {
    return {
      search: "",

      editedIndex: -1,
      id: 0,

      idcategoria: 0,
      nombre: "",
      codigo: "",
      telefono: 0,
      descripcion: "",
      servicios: "",
      imagen: "",
      imagen2: "",
      posicionx: 0,
      posiciony: 0,
      direccion: "",

      adModal: 0,
      adAccion: 0,
      AdNombre: "",
      adId: 0,
      e1: 1,
      snackbar: false,
      snackbar1: false,

      text: "Registrado con éxito",
      text1: "Error al crear el producto",
      valid: true,
      emailRules: [
        (v) => !!v || "El correo electrónico debe ser obligatorio",
        (v) => /.+@.+/.test(v) || "El correo debe ser válido",
      ],
      rules: {
        required: (value) => !!value || "Requerido.",
        min: (v) => v.length >= 8 || "Minimo 8 caracteres",
      },
      categorias: [],
    };
  },
  created() {
    this.select();
  },
  methods: {
    select() {
      let me = this;
      var categoriasArray = [];
      axios
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
        .post("Mortuaries/PostMortuary", {
          idcategoria: parseInt(2),
          codigo: me.codigo,
          nombre: me.nombre,
          descripcion: me.descripcion,
          servicios: me.servicios,
          imagen: me.imagen,
          imagen2: me.imagen2,
          posicionx: parseInt(me.posicionx),
          posiciony: parseInt(me.posiciony),
          direccion: me.direccion,
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
