<template>
  <ion-page>
    <ion-header>
      <ion-toolbar>
        <ion-title color="medium">Categorias</ion-title>
      </ion-toolbar>
    </ion-header>
    <ion-content :fullscreen="true">
      <ion-header collapse="condense">
        <ion-toolbar>
          <ion-title size="large">Categorias</ion-title>
        </ion-toolbar>
      </ion-header>
      <ion-card v-for="post of categories" :key="post.idcategoria">
        <template v-if="post.condicion == true">
          <img :src="post.imagen" />
          <ion-card-header>
            <ion-card-title color="secondary">{{
              post.nombre
            }}</ion-card-title></ion-card-header
          >
        </template>
      </ion-card>
    </ion-content>
  </ion-page>
</template>

<script lang="ts">
import {
  IonPage,
  IonHeader,
  IonToolbar,
  IonTitle,
  IonContent,
} from "@ionic/vue";
import axios from "axios";

export default {
  name: "Tab2",
  components: {
    IonHeader,
    IonToolbar,
    IonTitle,
    IonContent,
    IonPage,
  },
  data: () => ({
    categories: [],
    id: 0,
  }),
  mounted(this: any) {
    axios
      .get("https://localhost:44383/api/Categories/List")
      .then((response) => {
        console.log(response);

        this.categories = response.data;
      });
  },
  methods: {},
};
</script>
<style scoped>
</style>