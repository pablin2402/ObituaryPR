<template>
  <v-layout align-center justify-center>
    <v-flex xs12 sm8 md6 lg5 xl4>
      <v-card v-model="valid">
        <v-toolbar dark color="teal darken-1 ">
          <v-toolbar-title color="black">Iniciar Sesión </v-toolbar-title>
        </v-toolbar>
        <v-card-text>
          <v-text-field
            v-model="email"
            autofocus
            :rules="emailRules"
            color="accent"
            label="Email"
            required
            counter="50"
            maxlength="50"
          >
          </v-text-field>
          <v-text-field
            v-model="password"
            type="password"
            color="accent"
            :rules="[rules.required, rules.min]"
            counter="20"
            maxlength="20"
            label="Password"
            required
          >
          </v-text-field>
        </v-card-text>
        <v-card-actions>
          <v-flex>
            <v-btn @click="ingresar" outlined rounded text>Ingresar</v-btn>
            <v-btn outlined rounded text :to="{ name: 'register' }"
              >Registrate</v-btn
            >
          </v-flex>
        </v-card-actions>
      </v-card>
      <v-snackbar v-model="snackbar" color="warning">
        {{ text }}

        <template v-slot:action="{ attrs }">
          <v-btn color="white" text v-bind="attrs" @click="snackbar = false">
            Cerrar
          </v-btn>
        </template>
      </v-snackbar>
    </v-flex>
  </v-layout>
</template>
<script>
import axios from "axios";

export default {
  data() {
    return {
      email: "",
      password: "",
      valid: true,
      text: "Error en la consulta",
      snackbar: false,
      emailRules: [
        (v) => !!v || "El correo electrónico debe ser obligatorio",
        (v) => /.+@.+/.test(v) || "El correo debe ser válido",
      ],
      rules: {
        required: (value) => !!value || "Requerido.",
        min: (v) => v.length >= 8 || "Minimo 8 caracteres",
      },
    };
  },
  name: "caca",

  methods: {
    ingresar() {
      let me = this;

      axios
        .post("People/Login", { email: this.email, password: this.password })
        .then((respuesta) => {
          return respuesta.data;
        })
        .then((data) => {
          this.$store.dispatch("guardarToken", data.token);
          localStorage.setItem("correo", this.email);
          this.$router.push({ name: "home", params: { email: this.email } });
        })
        .catch((err) => {
          me.snackbar = true;

          console.log(err);
        });
    },
  },
};
</script>