<template>
  <v-row justify="center">
    <v-col cols="12" sm="10" md="8" lg="6">
      <v-card ref="form">
        <v-card-title>
          <span class="headline">REGISTRATE</span>
        </v-card-title>
        <v-card-text>
          <v-container>
            <v-row>
              <v-flex xs12 sm6 md6>
                <v-text-field
                  v-model="nombre"
                  label="Nombre"
                  :rules="[rules.required, rules.min]"
                  hint="Por lo menos 8 carácteres"
                  class="input-group--focused"
                  counter="100"
                  maxlength="100"
                ></v-text-field>
              </v-flex>
              <v-flex xs12 sm6 md6>
                <v-select
                  v-model="idrol"
                  :items="roles"
                  label="Roles"
                  :rules="[rules.required]"
                  class="input-group--focused"
                ></v-select>
              </v-flex>
              <v-flex xs12 sm6 md6>
                <v-select
                  v-model="tipo_documento"
                  :items="documentos"
                  label="Tipo de Documentos"
                  :rules="[rules.required]"
                  class="input-group--focused"
                  counter="20"
                  maxlength="20"
                ></v-select>
              </v-flex>
              <v-flex xs12 sm12 md12>
                <v-text-field
                  v-model="num_documento"
                  label="Número de Documento"
                  :rules="[rules.required]"
                  class="input-group--focused"
                  counter="20"
                  maxlength="20"
                ></v-text-field>
              </v-flex>
              <v-flex xs12 sm6 md6>
                <v-text-field
                  v-model="direccion"
                  label="Direccion"
                  :rules="[rules.required, rules.min]"
                  hint="Por lo menos 8 carácteres"
                  class="input-group--focused"
                  counter="70"
                  maxlength="70"
                ></v-text-field>
              </v-flex>
              <v-flex xs12 sm12 md12>
                <v-text-field
                  v-model="email"
                  label="Correo Electrónico"
                  :rules="emailRules"
                  class="input-group--focused"
                  counter="50"
                  maxlength="50"
                ></v-text-field>
              </v-flex>
              <v-flex xs12 sm12 md12>
                <v-text-field
                  v-model="telefono"
                  label="Telefono"
                  :rules="[rules.required, rules.min]"
                  hint="Por lo menos 8 carácteres"
                  class="input-group--focused"
                  counter="20"
                  maxlength="20"
                ></v-text-field>
              </v-flex>
              <v-flex xs12 sm12 md12>
                <v-text-field
                  type="password"
                  v-model="password"
                  label="Contraseña"
                  :rules="[rules.required, rules.min]"
                  hint="Por lo menos 8 carácteres"
                  class="input-group--focused"
                  counter="256"
                  maxlength="256"
                ></v-text-field>
              </v-flex>

              <v-snackbar v-model="snackbar">
                {{ text }}

                <template v-slot:action="{ attrs }">
                  <v-btn
                    color="pink"
                    text
                    v-bind="attrs"
                    @click="snackbar = false"
                  >
                    Close
                  </v-btn>
                </template>
              </v-snackbar>
            </v-row>
          </v-container>
        </v-card-text>
        <v-snackbar v-model="alert1" color="warning">
          {{ text }}

          <template v-slot:action="{ attrs }">
            <v-btn color="pink" text v-bind="attrs" @click="alert1 = false">
              Close
            </v-btn>
          </template>
        </v-snackbar>
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
          <v-btn color="blue darken-1" text @click="save"> Guardar </v-btn>
        </v-card-actions>
      </v-card>
    </v-col>
  </v-row>
</template>
<script>
import axios from "axios";

export default {
  data() {
    return {
      email: "",
      password: "",
      alert1: false,
      text: "El correo electrónico ya existe",
      valid: true,
      emailRules: [
        (v) => !!v || "El correo electrónico debe ser obligatorio",
        (v) => /.+@.+/.test(v) || "El correo debe ser válido",
      ],
      rules: {
        required: (value) => !!value || "Requerido.",
        min: (v) => v.length >= 8 || "Minimo 8 caracteres",
      },
      editedIndex: -1,
      id: 0,
      idrol: "",
      roles: [],
      tipo_documento: "",

      documentos: ["DNI", "PASAPORTE", "CEDULA"],
      num_documento: "",
      direccion: "",
      telefono: "",
      nombre: "",
      descripcion: "",
      actPassword: false,
      adModal: 0,
      adAccion: 0,
      AdNombre: "",
      adId: 0,
      search: "",

      passwordAnt: "",
    };
  },
  created() {
    this.select();
  },
  methods: {
    save() {
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
        .then(function (response) {
          console.log(response);
          me.$router.push({ name: "home" });
        })
        .catch(function (error) {
          me.alert1 = true;

          console.log(error);
        });
    },

    select() {
      let me = this;
      var rolesArray = [];
      axios
        .get("Rols/Select")
        .then(function (response) {
          rolesArray = response.data;
          rolesArray.map(function (x) {
            me.roles.push({ text: x.nombre, value: x.idrol });
          });
        })
        .catch(function (error) {
          console.log(error);
          me.snackbar = true;
        });
    },
  },
};
</script>