<template>
  <ion-content>
    <div class="ion-padding-horizontal wrapper">
      <ion-grid class="ion-no-padding">
        <ion-row class="ion-align-items-baseline">
          <ion-col size="6">
            <h4 class="title">Ver Categorías</h4>
          </ion-col>
          <ion-col size="6">
            <h4 class="more ion-text-end">
              <a href="/tabs/tab2">Mostrar más</a>
            </h4>
          </ion-col>
        </ion-row>
      </ion-grid>
    </div>
    <ion-grid class="ion-no-padding">
      <ion-row>
        <ion-col size="12">
          <ion-slides>
            <ion-slide v-for="post of categories" :key="post.idcategoria">
              <template v-if="post.condicion == true">
                <ion-card class="category">
                  <ion-card-header class="ion-text-center">
                    <img :src="post.imagen" />
                    <ion-card-title>{{ post.nombre }}</ion-card-title>
                  </ion-card-header>
                </ion-card>
              </template>
            </ion-slide>
          </ion-slides>
        </ion-col>
      </ion-row>
    </ion-grid>
  </ion-content>
</template>

<script lang="ts">
import {
  IonContent,
  IonCard,
  IonCardHeader,
  IonCardTitle,
  IonCol,
  IonRow,
  IonSlide,
  IonSlides,
} from "@ionic/vue";
import axios from "axios";

export default {
  name: "CategoriesComponent",
  components: {
    IonContent,
    IonCard,
    IonCardHeader,
    IonCardTitle,
    IonCol,
    IonRow,
    IonSlide,
    IonSlides,
  },

  data: () => ({
    categories: [],
    id: 0,
  }),
  async mounted(this: any) {
    const response = await axios.get(
      "https://localhost:44383/api/Categories/List"
    );
    this.categories = response.data;
  },
};
</script>
<style lang="scss" src="./Categoriees.scss" scoped></style>
