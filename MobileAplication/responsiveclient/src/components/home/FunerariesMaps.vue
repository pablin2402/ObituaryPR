<template>
  <gmap-map :center="center" :zoom="13" style="width: 100%; height: 500px">
    <gmap-marker
      :key="index"
      v-for="(m, index) in markers"
      :position="m.position"
      :title="m.title"
      :clickable="true"
      :draggable="true"
      @click="center = m.position"
    ></gmap-marker>
  </gmap-map>
</template>
<script>
import axios from "axios";

export default {
  name: "FunerariesMap",

  data() {
    return {
      markers: [],
      center: { lat: -17.388688, lng: -66.1645 },
    };
  },
  mounted() {
    axios
      .get("Mortuaries/GetMortuariesLocation")
      .then((response) => {
        for (var i = 0; i < response.data.length; i++) {
          const marker = {
            lat: response.data[i].posicionx,
            lng: response.data[i].posiciony,
          };
          this.markers.push({
            title: response.data[i].nombre,
            position: marker,
          });
        }
      })
      .catch(() => {});
  },
};
</script>