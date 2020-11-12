<template>
  <ion-page>
    <ion-header>
      <ion-toolbar>
        <ion-title color="medium">Florerias</ion-title>
      </ion-toolbar>
    </ion-header>
    <ion-content :fullscreen="true">
      <ion-header collapse="condense">
        <ion-toolbar>
          <ion-title size="large">Florerias</ion-title>
        </ion-toolbar>
      </ion-header>
      <ion-card v-for="post of flowers" :key="post.idcategoria">
        <template v-if="post.condicion == true && post.idcategoria == 1002">
          <ion-card-header>
            <ion-card-title>{{ post.nombre }}</ion-card-title>
            <ion-card-subtitle>{{ post.descripcion }}</ion-card-subtitle>
          </ion-card-header>
          <img :src="post.imagen" />

          <ion-card-content>
            <ion-row class="ion-align-items-center ion-margin-top">
              <ion-col size="4">
                <h2 class="price">
                  <strong>{{ post.precio_venta }} Bs.</strong>
                </h2>
              </ion-col>
              <ion-col size="8">
                <ion-button
                  expand="block"
                  shape="round"
                  fill="outline"
                  color="warning"
                >
                  Añadir al Carrito
                </ion-button>
              </ion-col>
            </ion-row>
          </ion-card-content>
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
  name: "Tab3",
  components: {
    IonHeader,
    IonToolbar,
    IonTitle,
    IonContent,
    IonPage,
  },
  data: () => ({
    flowers: [],
    id: 0,
  }),
  mounted(this: any) {
    axios.get("https://localhost:44383/api/Articles/List").then((response) => {
      console.log(response);

      this.flowers = response.data;
    });
  },
  methods: {},
};
</script>
<style lang="scss" src="./Tab3.scss" scoped></style>
