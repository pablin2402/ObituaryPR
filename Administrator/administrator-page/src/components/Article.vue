<template>
  <v-layout align-start>
    <v-flex>
      <v-data-table
        :headers="headers"
        :items="articulos"
        sort-by="calories"
        class="elevation-1"
      >
        <template v-slot:top>
          <v-toolbar flat>
            <v-toolbar-title>Artículos</v-toolbar-title>
            <v-divider class="mx-4" inset vertical></v-divider>
            <v-spacer></v-spacer>
            <v-dialog v-model="dialog" max-width="500px">
              <template v-slot:activator="{ on, attrs }">
                <v-btn
                  color="primary"
                  dark
                  class="mb-2"
                  v-bind="attrs"
                  v-on="on"
                >
                  Nuevo Articulo
                </v-btn>
              </template>
              <v-card>
                <v-card-title>
                  <span class="headline">{{ formTitle }}</span>
                </v-card-title>

                <v-card-text>
                  <v-container>
                    <v-row>
                      <v-flex xs6 sm6 md6>
                        <v-text-field
                          v-model="codigo"
                          label="Código"
                        ></v-text-field>
                      </v-flex>
                      <v-flex xs6 sm6 md6>
                        <v-select
                          v-model="idcategoria"
                          :items="categorias"
                          label="Categoría"
                        ></v-select>
                      </v-flex>
                      <v-flex xs12 sm12 md12>
                        <v-text-field
                          v-model="nombre"
                          label="Nombre"
                        ></v-text-field>
                      </v-flex>
                      <v-flex xs6 sm6 md6>
                        <v-text-field
                          type="number"
                          v-model="stock"
                          label="Stock"
                        ></v-text-field>
                      </v-flex>
                      <v-flex xs12 sm12 md12>
                        <v-text-field
                          type="number"
                          v-model="precio_venta"
                          label="Precio de Venta"
                        ></v-text-field>
                      </v-flex>
                      <v-flex xs12 sm12 md12>
                        <v-text-field
                          v-model="descripcion"
                          label="Descripcion"
                        ></v-text-field>
                      </v-flex>
                    </v-row>
                  </v-container>
                </v-card-text>

                <v-card-actions>
                  <v-spacer></v-spacer>
                  <v-btn color="blue darken-1" text @click="close">
                    Cancelar
                  </v-btn>
                  <v-btn color="blue darken-1" text @click="save">
                    Guardar
                  </v-btn>
                </v-card-actions>
              </v-card>
            </v-dialog>

            <v-dialog v-model="adModal" max-width="290px">
              <v-card>
                <v-card-title class="headline" v-if="adAccion == 1"
                  >¿Activar Item?</v-card-title
                >
                <v-card-title class="headline" v-if="adAccion == 2"
                  >¿Desactivar Item?</v-card-title
                >
                <v-card-text>
                  Estás a punto de <span v-if="adAccion == 1">Activar</span>
                  <span v-if="adAccion == 2">Desactivar</span> el item
                  {{ AdNombre }}
                </v-card-text>
                <v-card-actions>
                  <v-spacer></v-spacer>
                  <v-btn
                    color="green darken-1"
                    flat="flat"
                    @click="activateclose"
                    >Cancelar</v-btn
                  >
                  <v-btn
                    v-if="adAccion == 1"
                    color="orange darken-4"
                    flat="flat"
                    @click="activar"
                    >Aceptar</v-btn
                  >
                  <v-btn
                    v-if="adAccion == 2"
                    color="orange darken-4"
                    flat="flat"
                    @click="desactivate"
                    >Aceptar</v-btn
                  >
                </v-card-actions>
              </v-card>
            </v-dialog>
          </v-toolbar>
        </template>
        <template v-slot:item="props">
          <tr>
            <td class="justify-center layout px-0">
              <v-icon small class="mr-2" @click="editItem(props.item)"
                >edit</v-icon
              >
              <template v-if="props.item.condicion">
                <v-icon small @click="activate(2, props.item)">block</v-icon>
              </template>
              <template v-else>
                <v-icon small @click="activate(1, props.item)">check</v-icon>
              </template>
            </td>
            <td>{{ props.item.codigo }}</td>
            <td>{{ props.item.nombre }}</td>

            <td>{{ props.item.categoria }}</td>
            <td>{{ props.item.stock }}</td>
            <td>{{ props.item.precio_venta }}</td>
            <td>{{ props.item.descripcion }}</td>

            <td>
              <div v-if="props.item.condicion">
                <span class="blue--text">Active</span>
              </div>
              <div v-else>
                <span class="red--text">Inactive</span>
              </div>
            </td>
          </tr>
        </template>

        <template slot="no-data">
          <v-btn color="primary">Reset</v-btn>
        </template>
      </v-data-table>
    </v-flex>
  </v-layout>
</template>

<script>
import axios from "axios";
export default {
  name: "Categories",
  data: () => ({
    articulos: [],
    dialog: false,
    dialogDelete: false,
    headers: [
      { text: "Opciones", value: "opciones", sortable: false },
      { text: "Código", value: "codigo" },
      { text: "Nombre", value: "nombre", sortable: false },
      { text: "Categoria", value: "categoria", sortable: false },
      { text: "Stock ", value: "stock", sortable: false },
      { text: "Precio de Venta", value: "precio_venta", sortable: false },
      { text: "Descripcion", value: "descripcion", sortable: false },
      { text: "Estado", value: "condicion", sortable: false },
    ],
    desserts: [],
    editedIndex: -1,
    id: 0,
    idcategoria: 0,
    categorias: [],
    nombre: "",
    codigo: 0,
    stock: 0,
    precio_venta: 0,
    descripcion: "",
    adModal: 0,
    adAccion: 0,
    AdNombre: "",
    adId: 0,
  }),

  computed: {
    formTitle() {
      return this.editedIndex === -1 ? "Nuevo Artículo" : "Actualizar Artículo";
    },
  },

  watch: {
    dialog(val) {
      val || this.close();
    },
    dialogDelete(val) {
      val || this.closeDelete();
    },
  },

  created() {
    this.listCategories();
    this.select();
  },

  methods: {
    listCategories() {
      let me = this;
      axios
        .get("Articles/List")
        .then(function(response) {
          console.log(response);
          me.articulos = response.data;
        })
        .catch(function(error) {
          console.log(error);
        });
    },
    select() {
      let me = this;
      var categoriasArray = [];
      axios
        .get("Categories/Select")
        .then(function(response) {
          categoriasArray = response.data;
          categoriasArray.map(function(x) {
            me.categorias.push({ text: x.nombre, value: x.idcategoria });
          });
        })
        .catch(function(error) {
          console.log(error);
        });
    },

    editItem(item) {
      this.id = item.idarticulo;
      this.idcategoria = item.idcategoria;
      this.codigo = item.codigo;
      this.nombre = item.nombre;
      this.stock = item.stock;
      this.precio_venta = item.precio_venta;
      this.descripcion = item.descripcion;
      this.editedIndex = 1;
      this.dialog = true;
    },
    activate(accion, item) {
      this.adModal = 1;
      this.AdNombre = item.nombre;
      this.adId = item.idarticulo;
      if (accion == 1) {
        this.adAccion = 1;
      } else if (accion == 2) {
        this.adAccion = 2;
      } else {
        this.adModal = 0;
      }
    },
    activateclose() {
      this.adModal = 0;
    },

    close() {
      this.dialog = false;
      this.clean();
    },

    closeDelete() {
      this.dialogDelete = false;
      this.$nextTick(() => {
        this.editedItem = Object.assign({}, this.defaultItem);
        this.editedIndex = -1;
      });
    },
    clean() {
      this.id = "";
      this.idcategoria = "";
      this.stock = "";
      this.precio_venta = "";
      this.nombre = "";
      this.descripcion = "";
      this.editedIndex = -1;
    },
    activar() {
      let me = this;
      axios
        .put("Articles/Activar/" + this.adId, {})
        .then(function() {
          me.adModal = 0;
          me.adAccion = 0;
          me.AdNombre = "";
          me.adId = 0;
          me.listCategories();
        })
        .catch(function(error) {
          console.log(error);
        });
    },
    desactivate() {
      let me = this;
      axios
        .put("Articles/Desactivar/" + this.adId, {})
        .then(function() {
          me.adModal = 0;
          me.adAccion = 0;
          me.AdNombre = "";
          me.adId = 0;
          me.listCategories();
        })
        .catch(function(error) {
          console.log(error);
        });
    },
    save() {
      if (this.editedIndex > -1) {
        let me = this;
        axios
          .put("Articles/PutArticle", {
            idarticulo: me.id,
            idcategoria: me.idcategoria,
            codigo: me.codigo,
            nombre: me.nombre,
            stock: me.stock,
            precio_venta: me.precio_venta,
            descripcion: me.descripcion,
          })
          .then(function(response) {
            console.log(response);
            me.close();
            me.listCategories();
            me.clean();
          })
          .catch(function(error) {
            console.log(error);
          });
      } else {
        let me = this;
        axios
          .post("Articles/PostArticle", {
            idcategoria: me.idcategoria,
            codigo: me.codigo,
            nombre: me.nombre,
            stock: me.stock,
            precio_venta: me.precio_venta,
            descripcion: me.descripcion,
          })
          .then(function(response) {
            console.log(response);
            me.close();
            me.listCategories();
            me.clean();
          })
          .catch(function(error) {
            console.log(error);
          });
      }
    },
  },
};
</script>
