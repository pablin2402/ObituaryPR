<template>
  <ion-page>
    <ion-header>
      <ion-toolbar>
        <ion-buttons slot="start">
          <ion-button>
            <ion-icon name="apps"></ion-icon>
          </ion-button>
        </ion-buttons>
        <ion-buttons slot="end">
          <ion-button class="bordered" fill="solid" color="light">
            <ion-icon color="custom" name="notifications"></ion-icon>
          </ion-button>
        </ion-buttons>

        <ion-title color="medium">Ingresa</ion-title>
      </ion-toolbar>

      <ion-item lines="none">
        <ion-avatar slot="start">
          <img
            src="https://scontent.fsrz1-1.fna.fbcdn.net/v/t1.0-9/18275010_1322276281189108_4991178332583010145_n.jpg?_nc_cat=106&ccb=2&_nc_sid=09cbfe&_nc_ohc=_va_FCcYIeMAX_mOEKr&_nc_ht=scontent.fsrz1-1.fna&oh=9cac89416e7eb7c59af991334330cde0&oe=5FCEA69D"
          />
        </ion-avatar>
        <ion-label>
          <p>Hello,</p>
          <h3>Pablo Rivas</h3>
        </ion-label>
      </ion-item>
    </ion-header>
    <ion-content>
      <div class="slides ion-margin-bottom">
        <ion-slides>
          <ion-slide v-for="post of mortuary" :key="post.idpersona">
            <ion-card>
              <ion-card-header>
                <img class="files" :src="post.imagen" />
              </ion-card-header>
              <ion-card-content class="ion-text-left">
                <ion-card-title class="title">{{ post.nombre }}</ion-card-title>
                <ion-card-subtitle class="ion-text-right">
                  LLamanos en cualquier momento
                  <br />
                  {{ post.telefono }}</ion-card-subtitle
                >
                <ion-progress-bar value="0.5" color="custom"></ion-progress-bar>
              </ion-card-content>
            </ion-card>
          </ion-slide>
        </ion-slides>
      </div>
    </ion-content>
    <CategoriesComponent />
  </ion-page>
</template>

<script lang="ts">
import {
  IonPage,
  IonHeader,
  IonToolbar,
  IonTitle,
  IonContent,
  IonButton,
  IonSlide,
} from "@ionic/vue";
import CategoriesComponent from "@/components/CategoriesComponent.vue";
import { defineComponent, reactive } from "vue";

import axios from "axios";

export default {
  name: "Tab1",
  components: {
    IonHeader,
    IonSlide,
    IonButton,
    IonToolbar,
    IonTitle,
    IonContent,
    IonPage,
    CategoriesComponent,
  },
  data() {
    return {
      mortuary: [],
      id: 0,
    };
  },
  async mounted(this: any) {
    const response = await axios.get(
      "https://localhost:44383/api/Mortuaries/GetMortuaries"
    );
    this.mortuary = response.data;
  },
  methods: {
    viewMore(post) {
      this.$router.push({ name: "view", params: { post } });
    },
  },
};
</script>
<style lang="scss" src="./Tab1.scss" scoped></style>
