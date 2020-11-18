<template>
  <v-layout align-center justify-center>
    <v-flex xs12 sm8 md6 lg5 xl4>
      <v-card class="mx-auto" style="max-width: 2500px">
        <v-toolbar cards dark flat>
          <v-card-title class="title font-weight-regular">
            Crear un Memorial</v-card-title
          >
          <v-spacer></v-spacer>
        </v-toolbar>
        <v-form ref="form" v-model="form" class="pa-4 pt-6">
          <v-row align="center">
            <v-col class="d-flex" cols="12" sm="6">
              <v-text-field
                v-model="idusuario"
                filled
                color="deep-purple"
                label="Código Usuario"
              ></v-text-field
            ></v-col>
            <v-col class="d-flex" cols="12" sm="6">
              <v-text-field
                v-model="nombre"
                filled
                :rules="[rules.required]"
                color="deep-purple"
                label="Nombres:"
              ></v-text-field>
            </v-col>
            <v-col class="d-flex" cols="12" sm="6">
              <v-text-field
                filled
                v-model="apellido"
                :rules="[rules.required]"
                color="deep-purple"
                label="Apellidos:"
              ></v-text-field
            ></v-col>
            <v-col class="d-flex" cols="12" sm="6">
              <v-text-field
                v-model="lugar_nacimiento"
                filled
                :rules="[rules.required]"
                color="deep-purple"
                label="Lugar de nacimiento:"
              ></v-text-field>
            </v-col>
            <v-col class="d-flex" cols="12" sm="12">
              <v-menu
                v-model="menu2"
                :close-on-content-click="false"
                :nudge-right="40"
                transition="scale-transition"
                offset-y
                min-width="290px"
              >
                <template v-slot:activator="{ on, attrs }">
                  <v-text-field
                    v-model="fecha_nacimiento"
                    label="Fecha de Nacimiento"
                    prepend-icon="mdi-calendar"
                    readonly
                    v-bind="attrs"
                    v-on="on"
                  ></v-text-field>
                </template>
                <v-date-picker
                  v-model="fecha_nacimiento"
                  @input="menu2 = false"
                ></v-date-picker>
              </v-menu>
            </v-col>
            <v-col class="d-flex" cols="12" sm="12">
              <v-menu
                v-model="menu1"
                :close-on-content-click="false"
                :nudge-right="40"
                transition="scale-transition"
                offset-y
                min-width="290px"
              >
                <template v-slot:activator="{ on, attrs }">
                  <v-text-field
                    v-model="fecha_muerte"
                    label="Fecha del deceso"
                    prepend-icon="mdi-calendar"
                    readonly
                    v-bind="attrs"
                    v-on="on"
                  ></v-text-field>
                </template>
                <v-date-picker
                  v-model="fecha_nacimiento"
                  @input="menu1 = false"
                ></v-date-picker>
              </v-menu>
            </v-col>

            <v-divider></v-divider>
            <v-col class="d-flex" cols="12" sm="6">
              <v-text-field
                v-model="imagen1"
                filled
                color="deep-purple"
                label="Imagen 1:"
              ></v-text-field>
            </v-col>
            <v-col class="d-flex" cols="12" sm="6">
              <v-text-field
                v-model="imagen2"
                filled
                color="deep-purple"
                label="Imagen 2:"
              ></v-text-field>
            </v-col>
            <v-col class="d-flex" cols="12" sm="6">
              <v-text-field
                v-model="imagen3"
                filled
                color="deep-purple"
                label="Imagen 3:"
              ></v-text-field>
            </v-col>
            <v-col class="d-flex" cols="12" sm="6">
              <v-text-field
                v-model="imagen4"
                filled
                color="deep-purple"
                label="Imagen 4:"
              ></v-text-field>
            </v-col>
            <v-col class="d-flex" cols="12" sm="6">
              <v-text-field
                v-model="imagen5"
                filled
                color="deep-purple"
                label="Imagen 5:"
              ></v-text-field>
            </v-col>
            <v-divider></v-divider>
            <v-col class="d-flex" cols="12" sm="12">
              <v-text-field
                v-model="descripcion"
                filled
                color="deep-purple"
                label="Descripcion: "
              ></v-text-field>
            </v-col>

            <v-col class="d-flex" cols="12" sm="6">
              <v-text-field
                v-model="musica"
                filled
                color="deep-purple"
                label="Musica :"
              ></v-text-field>
            </v-col>
            <v-col class="d-flex" cols="12" sm="6">
              <v-select
                v-model="genero"
                :items="items"
                filled
                label="Género"
              ></v-select>
            </v-col>
            <v-col class="d-flex" cols="12" sm="12">
              <v-select
                v-model="designacion"
                :items="items2"
                filled
                label="Designación"
              ></v-select>
            </v-col>
            <v-checkbox
              v-model="agreement"
              :rules="[rules.required]"
              color="deep-purple"
            >
              <template v-slot:label>
                Estoy de acuedo con los &nbsp;
                <a href="#" @click.stop.prevent="dialog = true">
                  Términos de servicio
                </a>
                &nbsp; y &nbsp;
                <a href="#" @click.stop.prevent="dialog = true">
                  Políticas de Privacidad. </a
                >*
              </template>
            </v-checkbox>
          </v-row>
        </v-form>
        <v-divider></v-divider>
        <v-card-actions>
          <v-btn text @click="$refs.form.reset()"> Limpiar </v-btn>
          <v-spacer></v-spacer>
          <v-btn
            :disabled="!form"
            :loading="isLoading"
            class="white--text"
            color="deep-purple accent-4"
            depressed
            @click="save()"
          >
            CREAR
          </v-btn>
        </v-card-actions>
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
            <v-btn color="pink" text v-bind="attrs" @click="snackbar1 = false">
              Cerrar
            </v-btn>
          </template>
        </v-snackbar>
        <v-dialog v-model="dialog" absolute max-width="400" persistent>
          <v-card>
            <v-card-title class="headline grey lighten-3"> Legal </v-card-title>
            <v-card-text> </v-card-text>
            <v-divider></v-divider>
            <v-card-actions>
              <v-btn text @click="(agreement = false), (dialog = false)">
                No
              </v-btn>
              <v-spacer></v-spacer>
              <v-btn
                class="white--text"
                color="deep-purple accent-4"
                @click="(agreement = true), (dialog = false)"
              >
                Si
              </v-btn>
            </v-card-actions>
          </v-card>
        </v-dialog>
      </v-card>
    </v-flex>
  </v-layout>
</template>
<script>
import axios from "axios";

export default {
  data: () => ({
    agreement: false,
    snackbar: false,
    snackbar1: false,

    idusuario: 1,
    nombre: "",
    apellido: "",
    lugar_nacimiento: "",
    fecha_nacimiento: new Date().toISOString().substr(0, 10),
    fecha_muerte: new Date().toISOString().substr(0, 10),
    imagen1: "",
    text: "Registrado con éxito",
    text1:
      "Error al crear obituario, por favor revise los datos y vuelva a intentar",

    menu2: false,
    modal: false,
    menu1: false,

    imagen2: "",
    imagen3: "",
    imagen4: "",
    imagen5: "",
    descripcion: "",
    musica: "",
    genero: "",
    designacion: "",

    dialog: false,
    form: false,
    isLoading: false,
    items: ["Masculino", "Femenino"],
    items2: [
      "Víctima del COVID-19",
      "VETERANO DE LA GUERRA",
      "POLÍTICO",
      "PERSONALIDAD SOCIAL",
      "ENFERMO TERMINAL",
    ],

    rules: {
      length: (len) => (v) =>
        (v || "").length >= len || `Invalid character length, required ${len}`,

      required: (v) => !!v || "Este campo es obligatorio",
    },
  }),
  methods: {
    save() {
      let me = this;
      axios
        .post("DeathPeoples/PostDeath", {
          idusuario: 1,
          nombre: me.nombre,
          apellido: me.apellido,
          lugar_nacimiento: me.lugar_nacimiento,
          fecha_nacimiento: me.fecha_nacimiento,
          fecha_muerte: me.fecha_muerte,
          imagen1: me.imagen1,
          imagen2: me.imagen2,
          imagen3: me.imagen3,
          imagen4: me.imagen4,
          imagen5: me.imagen5,
          descripcion: me.descripcion,
          musica: me.musica,
          genero: me.genero,
          designacion: me.descripcion,
        })
        .then(function () {
          me.snackbar = true;
        })
        .catch(function () {
          me.snackbar1 = true;
        });
    },
  },
};
</script>