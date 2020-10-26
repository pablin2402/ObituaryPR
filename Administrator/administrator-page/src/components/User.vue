<template>
  <v-layout align-start>
    <v-flex>
      <v-data-table
        :headers="headers"
        :items="usuarios"
        :search="search"
        class="elevation-1"
      >
        <template v-slot:top>
          <v-toolbar text>
            <v-toolbar-title>Usuarios</v-toolbar-title>
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
                  Nuevo Usuario
                </v-btn>
              </template>
              <v-card>
                <v-card-title>
                  <span class="headline">{{ formTitle }}</span>
                </v-card-title>

                <v-card-text>
                  <v-container>
                    <v-row>
                      <v-flex xs12 sm6 md6>
                        <v-text-field
                          v-model="nombre"
                          label="Nombre"
                        ></v-text-field>
                      </v-flex>
                      <v-flex xs12 sm6 md6>
                        <v-select
                          v-model="idrol"
                          :items="roles"
                          label="Roles"
                        ></v-select>
                      </v-flex>
                        <v-flex xs12 sm6 md6>
                        <v-select
                          v-model="tipo_documento"
                          :items="documentos"
                          label="Tipo de Documentos"
                        ></v-select>
                      </v-flex>
                      <v-flex xs12 sm12 md12>
                        <v-text-field
                          v-model="num_documento"
                          label="Número de Documento"
                        ></v-text-field>
                      </v-flex>
                      <v-flex xs12 sm6 md6>
                        <v-text-field
                          
                          v-model="direccion"
                          label="Direccion"
                        ></v-text-field>
                      </v-flex>
                      <v-flex xs12 sm12 md12>
                        <v-text-field
                          
                          v-model="email"
                          label="Correo Electrónico"
                        ></v-text-field>
                      </v-flex>
                      <v-flex xs12 sm12 md12>
                        <v-text-field
                          v-model="telefono"
                          label="Telefono"
                        ></v-text-field>
                      </v-flex>
                       <v-flex xs12 sm12 md12>
                        <v-text-field
                        type="password"
                          v-model="password"
                          label="Contraseña"
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
            <td>{{ props.item.rol }}</td>

            <td>{{ props.item.tipo_documento }}</td>
            <td>{{ props.item.num_documento }}</td>
            <td>{{ props.item.direccion }}</td>
            <td>{{ props.item.telefono }}</td>
                        <td>{{ props.item.email }}</td>

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
    usuarios: [],
    dialog: false,
    dialogDelete: false,
    headers: [
      { text: "Opciones", value: "opciones", sortable: false },
      { text: "Nombre", value: "nombre" },
      { text: "Rol", value: "rol", sortable: false },
      { text: "Tipo Documento", value: "tipo_documento", sortable: false },
      { text: "Número Documento", value: "num_documento", sortable: false },
      { text: "Direccion", value: "direccion", sortable: false },
      { text: "Telefono", value: "telefono", sortable: false },
      { text: "Email", value: "email", sortable: false },

      { text: "Estado", value: "condicion", sortable: false },
    ],
    desserts: [],
    editedIndex: -1,
    id: 0,
    idrol:'',
    roles:[],
    tipo_documento:'',
    
    documentos: ['DNI','PASAPORTE','CEDULA'],
    num_documento:'',
    direccion: "",
    telefono: '',
    email: '',
    password: '',
    nombre:'',
    descripcion: "",
    actPassword:false,
    adModal: 0,
    adAccion: 0,
    AdNombre: "",
    adId: 0,
    search: '',

    passwordAnt:''
  }),

  computed: {
    formTitle() {
      return this.editedIndex === -1 ? "Nuevo Usuario" : "Actualizar Usuario";
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
        .get("Users/GetUsers")
        .then(function(response) {
          me.usuarios = response.data;
        })
        .catch(function(error) {
          console.log(error);
        });
    },
    select() {
      let me = this;
      var rolesArray = [];
      axios
        .get("Rols/Select")
        .then(function(response) {
          rolesArray = response.data;
          rolesArray.map(function(x) {
            me.roles.push({ text: x.nombre, value: x.idrol });
          });
        })
        .catch(function(error) {
          console.log(error);
        });
    },

    editItem(item) {
      this.id = item.idusuario;
      this.idrol = item.idrol;
      this.nombre = item.nombre;
      this.tipo_documento = item.tipo_documento;
      this.num_documento = item.num_documento;
      this.direccion = item.direccion;
      this.telefono = item.telefono;
      this.email = item.email;
      this.password = item.password_hash;
      this.passwordAnt=item.password_hash;
      this.editedIndex = 1;
      this.dialog = true;
    },
    activate(accion, item) {
      this.adModal = 1;
      this.AdNombre = item.nombre;
      this.adId = item.idusuario;
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
      this.idrol = "";
      this.nombre = "";
      this.tipo_documento = "";
      this.num_documento = "";
      this.direccion = "";
      this.telefono="";
      this.email="";
      this.password="";
      this.passwordAnt="";
      this.act_password=false;
      this.editedIndex = -1;
    },
    activar() {
      let me = this;
      axios
        .put("Users/Activar/" + this.adId, {})
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
        .put("Users/Desactivar/" + this.adId, {})
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
        if(me.password!= me.passwordAnt){
          me.actPassword=true;
        }
        axios
          .put("Users/Actualizar", {
            idusuario: me.id,
            idrol: me.idrol,
            nombre: me.nombre,
            tipo_documento: me.tipo_documento,
            num_documento: me.num_documento,
            direccion: me.direccion,
            telefono: me.telefono,
            email: me.email,
            password: me.password,
            act_password:me.actPassword
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
          .post("Users/Crear", {
            idrol: me.idrol,
            nombre: me.nombre,
            tipo_documento: me.tipo_documento,
            num_documento: me.num_documento,
            direccion: me.direccion,
            telefono: me.telefono,
            email: me.email,
            password: me.password,


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
