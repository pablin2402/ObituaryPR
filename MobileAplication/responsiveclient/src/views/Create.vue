<template>
  <v-layout align-start>
    <v-flex>
      <v-row dense>
        <v-col cols="12" sm="4" ls="12" v-for="card in cards" :key="card.title">
          <v-card>
            <v-img
              :src="card.src"
              class="white--text align-end"
              gradient="to bottom, rgba(0,0,0,.1), rgba(0,0,0,.5)"
              height="500px"
            >
              <v-card-title v-text="card.title"></v-card-title>
            </v-img>

            <v-card-text class="text--primary" v-text="card.description">
            </v-card-text>

            <v-card-actions>
              <v-btn color="orange" text @click="opendialogItem(card.title)">
                CREAR
              </v-btn>
            </v-card-actions>
          </v-card>
        </v-col>
      </v-row>
    </v-flex>
  </v-layout>
</template>

<script>
// @ is an alias to /src

export default {
  name: "Create",
  components: {},
  data: () => ({
    cards: [
      {
        title: "Crea tu funeraria",
        src:
          "https://image.freepik.com/foto-gratis/mujer-luto-funeral-ataud_79405-12218.jpg",
        description:
          "Aqui puedes publicar todos los datos acerca de tu funeraria para compartirla por el mundo.",
        flex: 6,
      },
      {
        title: "Publica tus productos",
        src:
          "https://image.freepik.com/foto-gratis/urna-funeraria-velas-flores_79405-11682.jpg",
        description:
          "Sientete libre de publicar los productos de tu floreria, o servicios adicionales a una funeraria",
        flex: 6,
      },
    ],
  }),
  methods: {
    opendialogItem(item) {
      if (item == "Publica tus productos") {
        this.$router.push({ name: "createitem" });
      } else {
        this.$router.push({ name: "createmortuary" });
      }
    },
  },
};
</script>
