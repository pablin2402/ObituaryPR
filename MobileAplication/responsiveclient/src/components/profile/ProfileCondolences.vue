<template>
  <v-layout align-start>
    <v-flex>
      <v-data-table
        :headers="headers"
        :items="condolences"
        :search="search"
        class="elevation-1"
      >
        <template v-slot:top>
          <v-toolbar text>
            <v-toolbar-title>Mis publicaciones</v-toolbar-title>
            <v-divider class="mx-4" inset vertical></v-divider>
            <v-spacer></v-spacer>
            <v-text-field
              class="text-xs-center"
              v-model="search"
              label="Búsqueda"
              single-line
              hide-details
            ></v-text-field>
            <v-spacer></v-spacer>
            <v-dialog v-model="dialog" max-width="800px">
              <!--template v-slot:activator="{ on, attrs }">
                <v-btn
                  color="primary"
                  dark
                  class="mb-2"
                  v-bind="attrs"
                  v-on="on"
                >
                  Nueva Categoria
                </v-btn>
              </template-->
              <v-card>
                <v-card-title>
                  <span class="headline">{{ formTitle }}</span>
                </v-card-title>

                <v-card-text>
                  <v-container>
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
                  <v-btn color="green darken-1" text @click="activateclose"
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
              <!--v-btn
                color="primary"
                small
                class="mr-2"
                @click="editItem(props.item)"
                >EDITAR
              </!--v-btn-->
              <template v-if="props.item.condicion">
                <v-btn color="warning" small @click="activate(2, props.item)"
                  >Deshabilitar</v-btn
                >
              </template>
              <template v-else>
                <v-btn color="primary" small @click="activate(1, props.item)"
                  >Habilitar</v-btn
                >
              </template>
            </td>
            <td>{{ props.item.fallecido }}</td>
            <td>{{ props.item.titulo }}</td>
            <td>{{ props.item.mensaje }}</td>

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
  name: "ProfileCondolences",
  data: () => ({
    categories: [],
    search: "",
    rules: {
      required: (value) => !!value || "Requerido.",
      min: (v) => v.length >= 8 || "Minimo 8 caracteres",
    },
    dialog: false,
    dialogDelete: false,
    headers: [
      { text: "Opciones", value: "opciones", sortable: false },
      { text: "Fallecido", value: "fallecido" },
      { text: "Titulo", value: "titulo", sortable: false },
      { text: "Mensaje", value: "mensaje", sortable: false },
      { text: "Estado", value: "condicion", sortable: false },
    ],
    editedIndex: -1,
    id: 0,
    idmuerto: 0,
    fallecido: "",
    titulo: "",
    mensaje: "",
    fecha: "",

    adModal: 0,
    adAccion: 0,
    AdNombre: "",
    adId: 0,

    //condolences
    condolences: [],
    filler: "",
    people: [],
  }),

  computed: {
    formTitle() {
      return this.editedIndex === -1 ? "Nueva Categoría" : "Editar Categoría";
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
    // this.listCategories();
    this.getEmail();
  },
  mounted() {
    this.buscarPersona();
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
    editItem(item) {
      this.id = item.idcondolencia;
      this.nombre = item.nombre;
      this.descripcion = item.descripcion;
      this.imagen = item.imagen;
      this.editedIndex = 1;
      this.dialog = true;
    },
    activate(accion, item) {
      this.adModal = 1;
      this.AdNombre = item.titulo;
      this.adId = item.idcondolencia;
      if (accion == 1) {
        this.adAccion = 1;
      } else if (accion == 2) {
        this.adAccion = 2;
      } else {
        this.adModal = 0;
      }
    },
    activar() {
      let me = this;
      axios
        .put("Condolences/Activate/" + this.adId, {})
        .then(function () {
          me.adModal = 0;
          me.adAccion = 0;
          me.AdNombre = "";
          me.adId = 0;
          me.seekCondolences();
        })
        .catch(function (error) {
          console.log(error);
        });
    },
    desactivate() {
      let me = this;
      axios
        .put("Condolences/Deactivate/" + this.adId, {})
        .then(function () {
          me.adModal = 0;
          me.adAccion = 0;
          me.AdNombre = "";
          me.adId = 0;
          me.seekCondolences();
        })
        .catch(function (error) {
          console.log(error);
        });
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
      this.imagen = "";
      this.editedIndex = -1;
    },

    save() {
      if (this.editedIndex > -1) {
        let me = this;
        axios
          .put("Categories/Put", {
            idcondolencia: me.id,
            nombre: me.nombre,
            descripcion: me.descripcion,
            imagen: me.imagen,
          })
          .then(function (response) {
            console.log(response);
            me.close();
            me.listCategories();
            me.clean();
          })
          .catch(function (error) {
            console.log(error);
          });
      } else {
        let me = this;
        axios
          .post("Categories/Post", {
            nombre: me.nombre,
            descripcion: me.descripcion,
            imagen: me.imagen,
          })
          .then(function (response) {
            console.log(response);
            me.close();
            me.listCategories();
            me.clean();
          })
          .catch(function (error) {
            console.log(error);
          });
      }
    },
  },
};
</script>
