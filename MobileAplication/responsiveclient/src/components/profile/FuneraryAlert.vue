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
            <td>{{ props.item.nombre }}</td>
            <td>{{ props.item.apellido }}</td>
            <td>{{ props.item.correo }}</td>
            <td>{{ props.item.telefono }}</td>
            <td>{{ props.item.mensaje }}</td>
            <td>{{ props.item.empresa }}</td>
            <td>
              {{ props.item.fecha | moment("dddd, MMMM Do YYYY, h:mm:ss a") }}
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
  name: "FuneraryAlert",
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
      { text: "Nombre", value: "nombre", sortable: false },
      { text: "Apellido", value: "apellido", sortable: false },
      { text: "Correo", value: "correo", sortable: false },
      { text: "Telefono", value: "telefono", sortable: false },
      { text: "Mensaje", value: "mensaje", sortable: false },
      { text: "Empresa", value: "empresa", sortable: false },
      { text: "Fecha", value: "fecha", sortable: false },
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

  created() {
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
        })
        .catch((err) => {
          console.log(err);
        });
    },
    seekCondolences() {
      axios
        .get("FuneraryAlerts/GetbyUser/" + 1)
        .then((response) => {
          this.condolences = response.data;
        })
        .catch((err) => {
          console.log(err);
        });
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
  },
};
</script>
