<template>
  <v-layout align-start>
    <v-flex>
      <v-data-table
        :headers="headers"
        :items="categories"
                        :search="search"
        class="elevation-1"
      >
        <template v-slot:top>
          <v-toolbar text>
            <v-toolbar-title>Categorias</v-toolbar-title>
            <v-divider class="mx-4" inset vertical></v-divider>
            <v-spacer></v-spacer>
             <v-text-field class="text-xs-center" v-model="search" append-icon="search" label="Búsqueda" single-line hide-details></v-text-field>
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
                  Nueva Categoria
                </v-btn>
              </template>
              <v-card>
                <v-card-title>
                  <span class="headline">{{ formTitle }}</span>
                </v-card-title>

                <v-card-text>
                  <v-container>
                    <v-row>
                      <v-col cols="12" sm="6" md="4">
                        <v-text-field
                          v-model="nombre"
                          label="Nombre"
                        ></v-text-field>
                      </v-col>
                      <v-col cols="12" sm="6" md="4">
                        <v-text-field
                          v-model="descripcion"
                          label="Descripcion"
                        ></v-text-field>
                      </v-col>
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
            <v-dialog v-model="dialogDelete" max-width="500px">
              <v-card>
                <v-card-title class="headline"
                  >Are you sure you want to delete this item?</v-card-title
                >
                <v-card-actions>
                  <v-spacer></v-spacer>
                  <v-btn color="blue darken-1" text @click="closeDelete"
                    >Cancel</v-btn
                  >
                  <v-btn color="blue darken-1" text @click="deleteItemConfirm"
                    >OK</v-btn
                  >
                  <v-spacer></v-spacer>
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
                 text
                    @click="activateclose"
                    >Cancelar</v-btn
                  >
                  <v-btn
                    v-if="adAccion == 1"
                    color="orange darken-4"
                text
                    @click="activar"
                    >Aceptar</v-btn
                  >
                  <v-btn
                    v-if="adAccion == 2"
                    color="orange darken-4"
                 text
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
            <td>{{ props.item.nombre }}</td>
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
    categories: [],
                    search: '',

    dialog: false,
    dialogDelete: false,
    headers: [
      { text: "Opciones", value: "opciones", sortable: false },
      { text: "Nombre", value: "nombre" },
      { text: "Descripcion", value: "descripcion", sortable: false },
      { text: "Estado", value: "condicion", sortable: false },
    ],
    desserts: [],
    editedIndex: -1,
    id: 0,
    nombre: "",
    descripcion: "",
    adModal: 0,
    adAccion: 0,
    AdNombre: "",
    adId: 0,
  }),

  computed: {
    formTitle() {
      return this.editedIndex === -1 ? "New Category" : "Edit Category";
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
  },

  methods: {
    listCategories() {
      let me = this;
      axios
        .get("Categories/List")
        .then(function(response) {
          console.log(response);
          me.categories = response.data;
        })
        .catch(function(error) {
          console.log(error);
        });
    },

    editItem(item) {
      this.id = item.idcategoria;
      this.nombre = item.nombre;
      this.descripcion = item.descripcion;
      this.editedIndex = 1;
      this.dialog = true;
    },
    activate(accion, item) {
      this.adModal = 1;
      this.AdNombre = item.nombre;
      this.adId = item.idcategoria;
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
    deleteItem() {
      this.dialogDelete = true;
    },

    deleteItemConfirm() {
      this.desserts.splice(this.editedIndex, 1);
      this.closeDelete();
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
      this.nombre = "";
      this.descripcion = "";
      this.editedIndex = -1;
    },
    activar() {
      let me = this;
      axios
        .put("Categories/Activate/" + this.adId, {})
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
        .put("Categories/Deactivate/" + this.adId, {})
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
          .put("Categories/Put", {
            idcategoria: me.id,
            nombre: me.nombre,
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
          .post("Categories/Post", {
            nombre: me.nombre,
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
