<template>
  <v-layout align-start>
    <v-flex>
      <v-dialog v-model="dialog" persistent max-width="600px">
        <template v-slot:activator="{ on, attrs }">
          <b-form-input
            type="search"
            v-model="selectedCategory"
            placeholder="Buscar"
          ></b-form-input>
          <br />

          <v-btn
            color="green"
            content="counter1"
            large
            fab
            dark
            v-bind="attrs"
            v-on="on"
          >
            <v-icon dark>mdi-cart</v-icon>
          </v-btn>
          <br />
        </template>
        <v-card>
          <v-card-title>
            <span class="headline">Datos de Compra</span>
          </v-card-title>
          <v-card-text>
            <v-container>
              <v-row>
                <v-col cols="12">
                  <v-text-field
                    v-model="telefono"
                    label="Telefono :"
                    required
                  ></v-text-field>
                </v-col>
                <v-col cols="12">
                  <v-text-field
                    v-model="direccion"
                    label="Direccion: "
                    required
                  ></v-text-field>
                </v-col>

                <v-col cols="12">
                  <v-data-table
                    :headers="cabeceraDetalles"
                    :items="detalles"
                    hide-default-footer
                    class="elevation-1"
                  >
                    <template slot="items" slot-scope="props">
                      <td>
                        <v-btn
                          medium
                          class="mr-2"
                          @click="eliminarDetalle(detalles, props.item)"
                        >
                          ELIMINAR
                        </v-btn>
                      </td>
                      <td>{{ props.item.articulo }}</td>
                      <td>
                        {{ props.item.cantidad }}
                      </td>
                      <td>
                        {{ props.item.precio }}
                      </td>

                      <td>
                        {{ props.item.descuento }}
                      </td>
                      <td>
                        ${{
                          props.item.cantidad * props.item.precio -
                          props.item.descuento
                        }}
                      </td>
                    </template>
                    <template slot="no-data">
                      <h3>No hay artículos agregados al detalle.</h3>
                    </template>
                  </v-data-table>
                  <br />
                  <v-flex class="text-xs-right">
                    <strong>Total Parcial: </strong>${{
                      (totalParcial = total - totalImpuesto)
                    }}
                  </v-flex>
                  <v-flex class="text-xs-right">
                    <strong>Total Impuesto: </strong>${{
                      (totalImpuesto = (total * impuesto) / (100 + impuesto))
                    }}
                  </v-flex>
                  <v-flex class="text-xs-right">
                    <strong>Total Neto: </strong>${{
                      (total = calcularTotal.toFixed(2))
                    }}
                  </v-flex>
                </v-col>
              </v-row>
            </v-container>
            <small>*indicates required field</small>
          </v-card-text>
          <v-card-actions>
            <v-spacer></v-spacer>
            <v-btn color="blue darken-1" text @click="dialog = false">
              CERRAR
            </v-btn>
            <v-btn color="blue darken-1" text @click="guardar()">
              GUARDAR
            </v-btn>
          </v-card-actions>
        </v-card>
      </v-dialog>
      <v-snackbar v-model="snackbar">
        {{ text }}

        <template v-slot:action="{ attrs }">
          <v-btn color="red" text v-bind="attrs" @click="snackbar = false">
            Close
          </v-btn>
        </template>
      </v-snackbar>
      <v-snackbar v-model="snackbar2">
        {{ text2 }}

        <template v-slot:action="{ attrs }">
          <v-btn color="red" text v-bind="attrs" @click="snackbar2 = false">
            Close
          </v-btn>
        </template>
      </v-snackbar>
      <v-flex xs12 sm2 md2 lg2 xl2 v-if="errorArticulo">
        <div class="red--text" v-text="errorArticulo"></div>
      </v-flex>
      <v-row align="stretch" class="ma-2">
        <v-col
          cols="12"
          md="4"
          class="pa-3 d-flex flex-column"
          v-for="post of filterArticles"
          :key="post.idcategoria"
        >
          <v-card
            :loading="loading"
            class="elevation-5 flex d-flex flex-column"
            width="400"
          >
            <template slot="progress">
              <v-progress-linear
                color="deep-purple"
                height="10"
                indeterminate
              ></v-progress-linear>
            </template>
            <template v-if="post.condicion == true">
              <v-img :src="post.imagen"></v-img>

              <v-card-title>{{ post.nombre }}</v-card-title>

              <v-card-text>
                <v-row align="center" class="mx-0">
                  <v-rating
                    :value="4.5"
                    color="amber"
                    dense
                    half-increments
                    readonly
                    size="14"
                  ></v-rating>

                  <div class="grey--text ml-4">4.5 (413)</div>
                </v-row>

                <div class="my-4 subtitle-1">
                  <strong>{{ post.precio_venta }} Bs.</strong>
                </div>
                <div>
                  <strong>{{ post.empresa }}</strong>
                  <br />
                  <br />
                </div>
                <div>
                  {{ post.descripcion }}
                </div>
              </v-card-text>

              <v-divider class="mx-4"></v-divider>

              <v-card-title>Horario de atención </v-card-title>

              <v-card-text>
                <v-chip-group
                  active-class="deep-purple accent-4 white--text"
                  column
                >
                  <v-chip>9:00 AM a 18:00 PM</v-chip>
                  <v-chip>MARTES A DOMINGO</v-chip>
                </v-chip-group>
              </v-card-text>

              <v-card-actions>
                <v-btn
                  color="deep-purple lighten-2"
                  text
                  @click="agregarDetalle(post)"
                >
                  AÑADIR AL CARRITO
                </v-btn>
              </v-card-actions>
            </template>
          </v-card>
        </v-col>
      </v-row>
    </v-flex>
  </v-layout>
</template>

<script>
// @ is an alias to /src
import axios from "axios";

export default {
  name: "Flowers",
  components: {},
  data() {
    return {
      flowers: [],
      idproveedor: 2003,
      idusuario: 1,
      tipo_comprobante: "001",
      serie_comprobante: "0100",
      num_comprobante: "0010",
      nombre: "",
      empresa: "",

      impuesto: 0,
      total: 0,
      detalles: [],
      errorArticulo: null,
      direccion: "",
      telefono: "",
      verArticulos: false,
      dialog: false,
      multiLine: true,
      snackbar: false,
      text: `Añadido al carrito`,
      snackbar2: false,
      text2: `¡Formulario enviado con exito !`,
      id: 0,
      loading: false,
      cabeceraDetalles: [
        { text: "Borrar", value: "borrar", sortable: false },
        { text: "Artículo", value: "articulo", sortable: false },
        { text: "Cantidad", value: "cantidad", sortable: false },
        { text: "Precio", value: "precio", sortable: false },
        { text: "Descuento", value: "descuento", sortable: false },
        { text: "Subtotal", value: "subtotal", sortable: false },
      ],
      selectedCategory: "",
    };
  },
  computed: {
    calcularTotal: function () {
      var resultado = 0.0;
      for (var i = 0; i < this.detalles.length; i++) {
        resultado =
          resultado + this.detalles[i].precio * this.detalles[i].cantidad;
      }
      return resultado;
    },
    filterArticles: function () {
      var self = this;
      var category = self.selectedCategory;
      if (category === "All") {
        return self.flowers;
      } else {
        return this.flowers.filter(function (lenguaje) {
          return (
            lenguaje.nombre
              .toLowerCase()
              .indexOf(self.selectedCategory.toLowerCase()) >= 0 ||
            lenguaje.empresa

              .toLowerCase()
              .indexOf(self.selectedCategory.toLowerCase()) >= 0 ||
            lenguaje.descripcion
              .toLowerCase()
              .indexOf(self.selectedCategory.toLowerCase()) >= 0
          );
        });
      }
    },
  },
  mounted() {
    axios.get("Articles/List").then((response) => {
      console.log(response);

      this.flowers = response.data;
    });
  },
  methods: {
    agregarDetalle(caca) {
      this.detalles.push({
        idarticulo: parseInt(caca.idarticulo),
        cantidad: 1,
        precio: parseInt(caca.precio_venta),
        num_comprobante: "1",
        nombre: caca.nombre,
        direccion: this.direccion,
        telefono: this.telefono,
      });
      this.snackbar = true;
    },
    encuentra(id) {
      var sw = 0;
      for (var i = 0; i < this.detalles.length; i++) {
        if (this.detalles[i].idarticulo == id) {
          sw = 1;
        }
      }
      return sw;
    },
    mostrarArticulos() {
      this.verArticulos = true;
    },
    guardar() {
      let me = this;
      console.log(me.total);

      axios
        .post("Entries/Crear", {
          idproveedor: parseInt(2),
          idusuario: parseInt(2),
          tipo_comprobante: "FACTURA",
          serie_comprobante: "0100",
          num_comprobante: "0010",
          impuesto: parseInt(0),
          total: parseInt(me.total),
          detalles: this.detalles,
        })
        .then(function () {
          me.dialog = false;
          me.snackbar2 = true;
        })
        .catch(function (error) {
          console.log(error);
        });
    },
  },
};
</script>
