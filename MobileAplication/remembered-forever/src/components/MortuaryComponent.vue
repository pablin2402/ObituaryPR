<template>
  <ion-content :fullscreen="true">
    <ion-card v-for="post of mortuary" :key="post.idpersona">
      <template v-if="post.condicion == true && post.idcategoria == 2">
        <img :src="post.imagen" />
        <ion-card-header>
          <ion-card-title color="secondary" @click="viewMore(post)"
            >{{ post.nombre }}
          </ion-card-title>
          <ion-card-title color="secondary"
            >{{ post.direccion }}
          </ion-card-title>
          <ion-card-title color="secondary">
            LLamanos en cualquier momento
            <br />
            {{ post.telefono }}
          </ion-card-title>
        </ion-card-header>
        <ion-card-content>
          <ion-label color="secondary">{{ post.descripcion }}</ion-label>
        </ion-card-content>
        <ion-row responsive-sm>
          <ion-col>
            <ion-buttom color="light" @click="viewMore(post)"
              >Ver mas</ion-buttom
            >
          </ion-col>
        </ion-row>
      </template>
    </ion-card>
  </ion-content>
</template>

<script lang="ts">
import { IonContent } from "@ionic/vue";
import axios from "axios";

export default {
  name: "MortuaryComponent",
  components: {
    IonContent,
  },

  data() {
    return {
      mortuary: [],
      id: 0,
    };
  },
  mounted(this: any) {
    axios
      .get("https://localhost:44383/api/Mortuaries/GetMortuaries")
      .then((response) => {
        console.log(response);

        this.mortuary = response.data;
      });
  },
  methods: {
    viewMore(post) {
      this.$router.push({ name: "view", params: { post } });
    },
  },
};
</script>
 <style>
:root {
  --ion-safe-area-top: 20px;
  --ion-safe-area-bottom: 22px;
}
</style>