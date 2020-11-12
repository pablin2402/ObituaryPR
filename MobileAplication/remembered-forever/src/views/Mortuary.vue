<template>
  <ion-page>
    <ion-header>
      <ion-toolbar>
        <ion-title color="medium">Florerias</ion-title>
      </ion-toolbar>
    </ion-header>
    <MortuaryComponent />
  </ion-page>
</template>

<script lang="ts">
import { IonPage, IonHeader, IonToolbar, IonTitle } from "@ionic/vue";
import MortuaryComponent from "@/components/MortuaryComponent.vue";

export default {
  name: "Mortuary",
  components: {
    IonHeader,
    IonToolbar,
    IonTitle,
    IonPage,
    MortuaryComponent,
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