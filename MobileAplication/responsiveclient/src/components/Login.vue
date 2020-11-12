<template>
  <v-layout align-center justify-center>
    <v-flex xs12 sm8 md6 lg5 xl4>
      <v-card v-model="valid">
        <v-toolbar dark color="blue darken-3">
          <v-toolbar-title>
            Acceso al Sistema
          </v-toolbar-title>
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
        <v-card-actions class="px-3 pb-3">
          <v-flex text-xs-right>
            <v-btn @click="ingresar" color="primary" :disabled="!valid"
              >Ingresar</v-btn
            >
          </v-flex>
        </v-card-actions>
      </v-card>
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
      emailRules: [
        v => !!v || "El correo electrónico debe ser obligatorio",
        v => /.+@.+/.test(v) || "El correo debe ser válido"
      ],
      rules: {
        required: value => !!value || "Requerido.",
        min: v => v.length >= 8 || "Minimo 8 caracteres"
      }
    };
  },
  methods: {
    ingresar() {
      axios
        .post("People/Login", { email: this.email, password: this.password })
        .then(respuesta => {
          return respuesta.data;
        })
        .then(data => {
          this.$store.dispatch("guardarToken", data.token);
          this.$router.push({ name: "home" });
        })
        .catch(err => {
          console.log(err);
        });
    }
  }
};
</script>