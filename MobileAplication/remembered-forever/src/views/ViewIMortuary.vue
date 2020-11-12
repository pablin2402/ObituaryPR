<template>
  <ion-page>
    <ion-header>
      <ion-toolbar>
        <ion-title color="medium">Florerias</ion-title>
      </ion-toolbar>
    </ion-header>
    <ion-content>
      <h1>caca {{ post }}</h1>
      <ion-img :src="post"></ion-img>
    </ion-content>
  </ion-page>
</template>

<script lang="ts">
import { IonPage, IonContent, IonImg } from "@ionic/vue";

export default {
  name: "ViewIMortuary",
  components: { IonPage, IonContent, IonImg },
  props: ["post"],
  data() {
    return {
      nombre: "",
    };
  },
  mounted() {
    console.log(this.post);
  },
  methods: {},
};
</script>
 <style>
:root {
  --ion-safe-area-top: 20px;
  --ion-safe-area-bottom: 22px;
}
</style>