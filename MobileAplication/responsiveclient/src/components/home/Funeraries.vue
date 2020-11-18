<template>
  <v-container id="subscribe" tag="section">
    <v-row dense>
      <v-container v-if="loading">
        <div class="text-xs-center">
          <v-progress-circular
            indeterminate
            :size="150"
            :width="8"
            color="green"
          >
          </v-progress-circular>
        </div>
      </v-container>
      <v-col
        cols="12"
        sm="4"
        ls="12"
        v-for="post of mortuary"
        :key="post.idpersona"
      >
        <v-card :loading="loading" class="mx-auto my-12" max-width="500">
          <template slot="progress">
            <v-progress-linear
              color="deep-purple"
              height="10"
              indeterminate
            ></v-progress-linear>
          </template>

          <v-img :src="post.imagen"></v-img>

          <router-link :to="{ name: 'details', params: { id: post } }">
            <v-card-title>{{ post.nombre }}</v-card-title>
          </router-link>
          <v-card-text>
            <v-row align="center" class="mx-0">
              <v-rating
                :value="4.5"
                color="amber"
                dense
                half-increments
                readonly
                size="14"
              ></v-rating>

              <div class="grey--text ml-4">4.5 (413)</div>
            </v-row>

            <div class="my-4 subtitle-1">{{ post.direccion }}</div>

            <div>
              LLamanos en cualquier momento
              <br />
              {{ post.telefono }}
            </div>
          </v-card-text>

          <v-divider class="mx-4"></v-divider>

          <v-card-title>Horario de atención </v-card-title>

          <v-card-text>
            <v-chip-group
              active-class="deep-purple accent-4 white--text"
              column
            >
              <v-chip>5:30PM</v-chip>

              <v-chip>7:30PM</v-chip>

              <v-chip>8:00PM</v-chip>

              <v-chip>9:00PM</v-chip>
            </v-chip-group>
          </v-card-text>

          <v-card-actions>
            <v-btn rounded color="green">View</v-btn>
          </v-card-actions>
        </v-card>
      </v-col>
    </v-row>
  </v-container>
</template>
<script>
import axios from "axios";
export default {
  name: "Funeraries",
  components: {},
  data() {
    return {
      mortuary: [],
      files: [],
      email: this.$route.params.email,
      id: 0,
      loading: true,

      info: null,
      errored: false,
    };
  },
  async mounted() {
    await axios
      .get("https://localhost:44383/api/Mortuaries/GetMortuaries")
      .then((response) => {
        this.mortuary = response.data;
        this.loading = false;
      })
      .catch((error) => {
        console.log(error);
        this.errored = true;
      })
      .finally(() => (this.loading = false));
  },
};
</script>