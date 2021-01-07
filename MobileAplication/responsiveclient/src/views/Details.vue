<template>
  <v-layout align-start>
    <v-flex>
      <v-carousel>
        <v-carousel-item
          v-for="(item, i) in items"
          :key="i"
          :src="item.src"
          reverse-transition="fade-transition"
          transition="fade-transition"
        ></v-carousel-item>
      </v-carousel>
      <v-row>
        <div>
          <b-jumbotron>
            <template #header>{{ name }}</template>
            <br />
            <br />

            <template #lead>
              <strong>Ofrecemos los siguientes servicios:</strong>
              {{ description }}
            </template>

            <hr class="my-4" />

            <h3>
              {{ servicios }}
            </h3>
            <v-dialog v-model="dialog" max-width="500px">
              <template v-slot:activator="{ on, attrs }">
                <div class="text-center">
                  <br />
                  <v-card class="mx-auto" max-width="500">
                    <v-card-text>
                      <p class="display-1 text--primary">
                        ¿Cómo podemos ayudarle?
                      </p>
                      <v-btn large color="secondary" v-bind="attrs" v-on="on">
                        Una muerte ha ocurrido o está cerca
                      </v-btn>
                    </v-card-text>
                  </v-card>
                  <br />
                  <br />
                </div>
              </template>
              <v-card max-width="500" class="mx-auto my-12">
                <v-img
                  height="250"
                  src="https://www.ecestaticos.com/imagestatic/clipping/c45/55e/c4555ed291a40f49c731d2a3d5936a1d/encuentran-a-un-hombre-al-que-enterraron-hace-4-meses-vagando-por-el-cementerio.jpg?mtime=1595332382"
                ></v-img>
                <v-card-title>
                  <v-card color="blue-grey lighten-4">
                    <v-card-title>
                      <h5 class="headline" align="center">
                        Estamos aquí para usted en este momento
                      </h5>
                    </v-card-title>
                    <v-container>
                      <h6 align="center">LLÁMENOS Y RECIBA AYUDA AHORA</h6>

                      <h5 align="center">
                        <v-icon color="blue" large>mdi-phone</v-icon>
                        +591 - {{ phone }}
                      </h5>
                    </v-container>
                  </v-card>
                </v-card-title>

                <v-card-text>
                  <v-container>
                    <v-row>
                      <v-col cols="12" sm="6" md="12">
                        <v-text-field
                          v-model="nombre"
                          label="Nombre"
                          name="input-10-1"
                          hint="Por lo menos 8 carácteres"
                          class="input-group--focused"
                          counter="50"
                          maxlength="50"
                        ></v-text-field>
                      </v-col>
                      <v-col cols="12" sm="6" md="12">
                        <v-text-field
                          v-model="apellido"
                          label="Apellido"
                          name="input-10-1"
                          hint="Por lo menos 8 carácteres"
                          class="input-group--focused"
                          counter="50"
                          maxlength="50"
                        ></v-text-field>
                      </v-col>
                      <v-col cols="12" sm="6" md="12">
                        <v-text-field
                          v-model="correo"
                          label="Correo Electrónico"
                          name="input-10-1"
                          hint="Por lo menos 8 carácteres"
                          class="input-group--focused"
                          counter="50"
                          maxlength="50"
                        ></v-text-field>
                      </v-col>
                      <v-col cols="12" sm="6" md="12">
                        <v-text-field
                          v-model="telefono"
                          label="Telefono"
                          type="number"
                          name="input-10-1"
                          class="input-group--focused"
                          counter="9"
                          maxlength="9"
                        ></v-text-field>
                      </v-col>
                      <v-col cols="12" sm="6" md="12">
                        <v-text-field
                          v-model="mensaje"
                          label="Mensaje"
                          name="input-10-1"
                          hint="Por lo menos 8 carácteres"
                          class="input-group--focused"
                          counter="50"
                          maxlength="50"
                        ></v-text-field>
                      </v-col>
                    </v-row>
                  </v-container>
                </v-card-text>

                <v-card-actions>
                  <v-spacer></v-spacer>
                  <v-btn color="blue darken-1" text @click="close">
                    Cancelar
                  </v-btn>
                  <v-btn color="blue darken-1" text @click="save">
                    Guardar
                  </v-btn>
                </v-card-actions>
              </v-card>
            </v-dialog>
            <br />
            <p><strong>Telefono:</strong></p>
            <h4>{{ phone }}</h4>

            <p><strong>Direccion:</strong></p>
            <h4>{{ direccion }}</h4>
          </b-jumbotron>
        </div>
      </v-row>
      <v-snackbar v-model="snackbar">
        {{ text }}

        <template v-slot:action="{ attrs }">
          <v-btn color="pink" text v-bind="attrs" @click="snackbar = false">
            Cerrar
          </v-btn>
        </template>
      </v-snackbar>
      <v-snackbar v-model="snackbar1">
        {{ text1 }}

        <template v-slot:action="{ attrs }">
          <v-btn color="pink" text v-bind="attrs" @click="snackbar1 = false">
            Cerrar
          </v-btn>
        </template>
      </v-snackbar>
      <h3>Nuestra ubicación:</h3>
      <Maps :latitude="latitud" :longitude="longitud" :title="name" />
    </v-flex>
  </v-layout>
</template><script>
import axios from "axios";
import Maps from "../components/home/Maps";

export default {
  name: "Details",
  props: "id",
  components: {
    Maps,
  },
  data() {
    return {
      center: { lat: 100, lng: 100 },
      markers: [
        {
          position: { lat: 100, lng: 100 },
          title: "caca",
        },
      ],
      dialog: false,

      singleMovie: "id",
      mortuary: [],
      name: this.$route.params.id.nombre,
      description: this.$route.params.id.descripcion,
      imagen: this.$route.params.id.imagen,
      imagen2: this.$route.params.id.imagen2,
      latitud: this.$route.params.id.posicionx,
      longitud: this.$route.params.id.posiciony,
      servicios: this.$route.params.id.servicios,
      phone: this.$route.params.id.telefono,
      direccion: this.$route.params.id.direccion,
      idempresa: this.$route.params.id.idempresa,
      nombre: "",
      apellido: "",
      correo: "",
      telefono: 0,
      mensaje: "",
      snackbar: false,
      snackbar1: false,
      items: [
        {
          src: this.$route.params.id.imagen2,
        },
        {
          src: this.$route.params.id.imagen,
        },
      ],
      text: "Registrado con éxito",
      text1: "Error al crear el producto",
    };
  },
  created() {},
  mounted() {
    axios
      .get("Articles/GetArticle/")
      .then((response) => {
        this.mortuary = response.data;
      })
      .catch((error) => {
        console.log(error);
      });
  },
  watch: {
    dialog(val) {
      val || this.close();
    },
  },

  methods: {
    close() {
      this.dialog = false;
    },
    save() {
      let me = this;
      console.log(parseInt(me.idempresa));
      axios
        .post("FuneraryAlerts/PostAlert", {
          nombre: me.nombre,
          apellido: me.apellido,
          correo: me.correo,
          telefono: parseInt(me.telefono),
          mensaje: me.mensaje,
          idempresa: parseInt(me.idempresa),
        })
        .then(function (response) {
          console.log(response);
          me.snackbar = true;
          me.dialog = false;
        })
        .catch(function (error) {
          console.log(error);
          me.snackbar1 = true;
        });
    },
  },
};
</script>