<template>
  <v-layout align-start>
    <v-flex>
      <v-row justify="center">
        <v-col cols="12" sm="4" ls="12">
          <v-card ref="form">
            <v-card-title>
              <span class="headline">CREAR ARTÍCULO </span>
            </v-card-title>
            <v-card-text>
              <v-text-field
                ref="codigo"
                v-model="codigo"
                label="Código"
                :rules="[() => !!codigo || 'El campo es requerido']"
                :error-messages="errorMessages"
                required
              ></v-text-field>
              <v-select
                v-model="idcategoria"
                :items="categorias"
                label="Categoría"
                :rules="[
                  () => !!idcategoria || 'El campo es requerido',
                  idcategoria,
                ]"
                class="input-group--focused"
              ></v-select>
              <v-text-field
                v-model="nombre"
                label="Nombre"
                :rules="[() => !!nombre || 'El campo es requerido', nombre]"
                required
              ></v-text-field>
              <v-text-field
                ref="stock"
                type="number"
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
              <v-text-field
                ref="imagen"
                v-model="imagen"
                label="Imagen"
                :rules="[() => !!imagen || 'El campo es requerido']"
                required
              ></v-text-field>
            </v-card-text>
            <v-divider class="mt-12"></v-divider>
            <v-card-actions>
              <v-btn text> Cancel </v-btn>
              <v-spacer></v-spacer>
              <v-slide-x-reverse-transition>
                <v-tooltip v-if="formHasErrors" left>
                  <template v-slot:activator="{ on, attrs }">
                    <v-btn
                      icon
                      class="my-0"
                      v-bind="attrs"
                      @click="resetForm"
                      v-on="on"
                    >
                      <v-icon>mdi-refresh</v-icon>
                    </v-btn>
                  </template>
                  <span>Refresh form</span>
                </v-tooltip>
              </v-slide-x-reverse-transition>
              <v-btn color="primary" text @click="save"> Submit </v-btn>
            </v-card-actions>
          </v-card>
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
      nombre: "",
      codigo: 0,
      stock: 0,
      imagen: "",
      precio_venta: 0,
      descripcion: "",
      adModal: 0,
      adAccion: 0,
      AdNombre: "",
      adId: 0,
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
        .post("Articles/PostArticle", {
          idcategoria: me.idcategoria,
          codigo: me.codigo,
          nombre: me.nombre,
          stock: me.stock,
          precio_venta: me.precio_venta,
          descripcion: me.descripcion,
          imagen: me.imagen,
        })
        .then(function (response) {
          console.log(response);
          me.close();
          //    me.listCategories();
          //  me.clean();
        })
        .catch(function (error) {
          console.log(error);
        });
    },
  },
};
</script>
