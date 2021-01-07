<template>
  <v-layout align-start>
    <v-flex>
      <v-row justify="center">
        <v-col cols="12" sm="12" ls="12">
          <h2>Publica tu producto</h2>
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
                Publica tu producto
              </v-stepper-step>

              <v-divider></v-divider>

              <v-stepper-step :complete="e1 > 2" step="2">
                Imagen del producto
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
                    <v-select
                      v-model="idcategoria"
                      :items="categorias"
                      label="Categoría"
                      class="input-group--focused"
                    ></v-select>
                    <h7>
                      <strong
                        >Debes crear tu negocio, antes de publicar un
                        producto</strong
                      >
                    </h7>
                    <v-select
                      v-model="idempresa"
                      :items="companies"
                      label="Negocio al que pertenece el producto"
                      class="input-group--focused"
                    ></v-select>
                    <v-text-field
                      v-model="nombre"
                      label="Nombre"
                      required
                    ></v-text-field>
                    <v-text-field
                      ref="stock"
                      type="number"
                      label="Stock"
                      v-model="stock"
                      :rules="[() => !!stock || 'El campo es requerido']"
                      required
                      placeholder="Stock"
                    ></v-text-field>
                    <v-text-field
                      ref="precio_venta"
                      type="number"
                      label="Precio de Venta"
                      v-model="precio_venta"
                      :rules="[() => !!precio_venta || 'El campo es requerido']"
                      required
                    ></v-text-field>
                    <v-text-field
                      ref="descripcion"
                      v-model="descripcion"
                      label="Descripcion"
                      :rules="[() => !!descripcion || 'El campo es requerido']"
                      required
                    ></v-text-field>
                  </v-card-text>
                </v-card>
                <v-btn color="primary" @click="e1 = 2"> CONTINUAR </v-btn>

                <v-btn text :to="{ name: 'home' }"> CANCELAR </v-btn>
              </v-stepper-content>

              <v-stepper-content step="2">
                <v-card class="mb-12" height="600px">
                  <v-text-field
                    ref="imagen"
                    v-model="imagen"
                    label="Imagen"
                    :rules="[() => !!imagen || 'El campo es requerido']"
                    required
                  ></v-text-field>
                  <v-img :src="imagen" height="400px"></v-img>
                </v-card>
                <v-btn color="primary" @click="e1 = 1"> ATRÁS </v-btn>

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
  name: "CreateItem",
  components: {},
  data() {
    return {
      search: "",

      editedIndex: -1,
      id: 0,
      idcategoria: 0,
      idempresa: 0,
      nombre: "",
      codigo: "",
      stock: 0,
      imagen: "",
      precio_venta: 0,
      descripcion: "",
      adModal: 0,
      adAccion: 0,
      AdNombre: "",
      adId: 0,
      e1: 1,
      snackbar: false,
      snackbar1: false,
      filler: "",
      ids: 0,
      companies: [],
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

  mounted() {
    this.buscarPersona();
  },

  created() {
    this.getEmail();
    this.selectMyCompany();

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
          console.log(this.ids);
          this.people = response.data;
          this.selectMyCompany();
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
    async selectMyCompany() {
      let me = this;
      var companiesArray = [];
      await axios
        .get("Companies/GetbyUser/" + parseInt(this.ids))
        .then(function (response) {
          companiesArray = response.data;
          companiesArray.map(function (x) {
            console.log(x.empresa);
            me.companies.push({ text: x.nombre, value: x.idempresa });
          });
          console.log("cool");
        })
        .catch(function (error) {
          console.log(error);
        });
    },
    save() {
      let me = this;
      axios
        .post("Articles/PostArticle", {
          idcategoria: parseInt(me.idcategoria),
          idempresa: parseInt(me.idempresa),
          codigo: me.codigo,
          nombre: me.nombre,
          stock: parseInt(me.stock),
          precio_venta: parseInt(me.precio_venta),
          descripcion: me.descripcion,
          imagen: me.imagen,
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
