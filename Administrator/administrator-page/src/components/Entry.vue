<template>
  <v-layout align-start>
    <v-flex>
      <v-data-table
        :headers="headers"
        :items="ingresos"
        :search="search"
        v-if="verNuevo == 0"
      >
        <template v-slot:top>
          <v-toolbar>
            <v-toolbar-title>Ingresos</v-toolbar-title>
            <v-divider class="mx-2" inset vertical></v-divider>

            <v-divider class="mx-4" inset vertical></v-divider>
            <v-spacer></v-spacer>
            <v-text-field
              class="text-xs-center"
              v-model="search"
              append-icon="search"
              label="Búsqueda"
              single-line
              hide-details
            ></v-text-field>
            <v-spacer></v-spacer>
            <v-dialog v-model="adModal" max-width="290">
              <v-card>
                <v-card-title class="headline" v-if="adAccion == 1"
                  >¿Activar Item?</v-card-title
                >
                <v-card-title class="headline" v-if="adAccion == 2"
                  >¿Anular Ingreso?</v-card-title
                >
                <v-card-text>
                  Estás a punto de
                  <span v-if="adAccion == 1">Activar </span>
                  <span v-if="adAccion == 2">Anular </span>
                  el ítem {{ adNombre }}
                </v-card-text>
                <v-card-actions>
                  <v-spacer></v-spacer>
                  <v-btn
                    color="green darken-1"
                    @click="activarDesactivarCerrar"
                  >
                    Cancelar
                  </v-btn>
                  <v-btn
                    v-if="adAccion == 1"
                    color="orange darken-4"
                    @click="activar"
                  >
                    Activar
                  </v-btn>
                  <v-btn
                    v-if="adAccion == 2"
                    color="orange darken-4"
                    @click="desactivar"
                  >
                    Anular
                  </v-btn>
                </v-card-actions>
              </v-card>
            </v-dialog>
            <v-btn
              v-if="verNuevo == 0"
              @click="mostrarNuevo()"
              color="primary"
              dark
              class="mb-2"
              >Nuevo</v-btn
            >
          </v-toolbar>
        </template>
        <template v-slot:item="props">
          <tr>
            <td class="justify-center layout px-0">
              <v-icon small class="mr-2" @click="verDetalles(props.item)"
                >tab</v-icon
              >
              <template v-if="props.item.estado == 'Aceptado'">
                <v-icon small @click="activarDesactivarMostrar(2, props.item)"
                  >block</v-icon
                >
              </template>
            </td>
            <td>{{ props.item.usuario }}</td>
            <td>{{ props.item.proveedor }}</td>
            <td>{{ props.item.tipo_comprobante }}</td>
            <td>{{ props.item.serie_comprobante }}</td>
            <td>{{ props.item.num_comprobante }}</td>
            <td>{{ props.item.fecha_hora }}</td>
            <td>{{ props.item.impuesto }}</td>
            <td>{{ props.item.total }}</td>

            <td>
              <div v-if="props.item.estado == 'Aceptado'">
                <span class="blue--text">Aceptado</span>
              </div>
              <div v-else>
                <span class="red--text">{{ props.item.estado }}</span>
              </div>
            </td>
          </tr>
        </template>

        <template slot="no-data">
          <v-btn color="primary">Reset</v-btn>
        </template>
      </v-data-table>
      <br />
      <br />
      <v-dialog v-model="verArticulos" max-width="1000px">
        <v-card>
          <v-card-title>
            <span class="headline">Seleccione un artículo</span>
          </v-card-title>
          <v-card-text>
            <v-container grid-list-md>
              <v-layout wrap>
                <v-flex xs12 sm12 md12 lg12 xl12>
                  <v-text-field
                    append-icon="search"
                    class="text-xs-center"
                    v-model="texto"
                    label="Ingrese artículo a buscar"
                    @keyup.enter="listarArticulo()"
                  >
                  </v-text-field>
                  <template>
                    <v-data-table
                      :headers="cabeceraArticulos"
                      :items="articulos"
                      class="elevation-1"
                    >
                      <template v-slot:item="props">
                        <td class="justify-center layout px-0">
                          <v-icon
                            small
                            class="mr-2"
                            @click="agregarDetalle(props.item)"
                          >
                            add
                          </v-icon>
                        </td>
                        <td>{{ props.item.nombre }}</td>
                        <td>{{ props.item.categoria }}</td>
                        <td>{{ props.item.descripcion }}</td>
                        <td>{{ props.item.stock }}</td>
                        <td>{{ props.item.precio_venta }}</td>
                      </template>
                      <template slot="no-data">
                        <h3>No hay artículos para mostrar.</h3>
                      </template>
                    </v-data-table>
                  </template>
                </v-flex>
              </v-layout>
            </v-container>
          </v-card-text>
          <v-card-actions>
            <v-spacer></v-spacer>
            <v-btn @click="ocultarArticulos()" color="blue darken" text>
              Cancelar
            </v-btn>
          </v-card-actions>
        </v-card>
      </v-dialog>
      <v-container grid-list-sm class="pa-4 white" v-if="verNuevo">
        <v-layout row wrap>
          <v-flex xs12 sm4 md4 lg4 xl4>
            <v-select
              v-model="tipo_comprobante"
              :items="comprobantes"
              label="Tipo Comprobante"
            >
            </v-select>
          </v-flex>

          <v-flex xs12 sm8 md8 lg8 xl8>
            <v-text-field v-model="direccion" label="Direccion : ">
            </v-text-field>
          </v-flex>
          <v-flex xs12 sm4 md4 lg4 xl4>
            <v-text-field type="number" v-model="impuesto" label="Impuesto">
            </v-text-field>
          </v-flex>
          <v-flex xs12 sm8 md8 lg8 xl8>
            <v-text-field
              @keyup.enter="listByCode()"
              v-model="codigo"
              label="Código"
            >
            </v-text-field>
          </v-flex>
          <v-flex xs12 sm2 md2 lg2 xl2>
            <v-btn @click="mostrarArticulos()" small fab dark color="teal">
              <v-icon dark>list</v-icon>
            </v-btn>
          </v-flex>

          <v-flex xs12 sm2 md2 lg2 xl2 v-if="errorArticulo">
            <div class="red--text" v-text="errorArticulo"></div>
          </v-flex>
          <v-flex xs12 sm12 md12 lg12 xl12>
            <v-data-table
              :headers="cabeceraDetalles"
              :items="detalles"
              hide-default-footer
              class="elevation-1"
            >
              <template v-slot:item="props">
                <td class="justify-center layout px-0">
                  <v-icon
                    medium
                    class="mr-2"
                    @click="eliminarDetalle(detalles, props.item)"
                  >
                    delete
                  </v-icon>
                </td>
                <td>{{ props.item.articulo }}</td>
                <td>
                  <v-text-field
                    type="number"
                    v-model.number="props.item.cantidad"
                  ></v-text-field>
                </td>
                <td>
                  <v-text-field
                    type="number"
                    v-model.number="props.item.precio"
                  ></v-text-field>
                </td>
                <td>${{ props.item.cantidad * props.item.precio }}</td>
              </template>
              <template slot="no-data">
                <h3>No hay artículos agregados al detalle.</h3>
              </template>
            </v-data-table>
            <br />
            <v-flex class="text-xs-right">
              <strong>Total Parcial: </strong>${{
                (totalParcial = total - totalImpuesto).toFixed(2)
              }}
            </v-flex>
            <v-flex class="text-xs-right">
              <strong>Total Impuesto: </strong>${{
                (totalImpuesto = (total * impuesto) / (100 + impuesto)).toFixed(
                  2
                )
              }}
            </v-flex>
            <v-flex class="text-xs-right">
              <strong>Total Neto: </strong>${{
                (total = calcularTotal.toFixed(2))
              }}
            </v-flex>
          </v-flex>

          <v-flex xs12 sm12 md12 lg12 xl12>
            <v-btn @click="ocultarNuevo" color="blue darken-1" text
              >Cancelar</v-btn
            >
            <v-btn @click="guardar()" color="success" v-if="verDet == 0"
              >Guardar</v-btn
            >
          </v-flex>
        </v-layout>
      </v-container>
    </v-flex>
  </v-layout>
</template>

<script>
import axios from "axios";
export default {
  name: "Categories",
  data: () => ({
    ingresos: [],
    dialog: false,
    dialogDelete: false,
    headers: [
      { text: "Opciones", value: "opciones", sortable: false },
      { text: "Usuario", value: "usuario" },
      { text: "Proveedor", value: "proveedor" },
      { text: "Tipo Comprobante", value: "tipo_comprobante" },
      {
        text: "Serie Comprobante",
        value: "serie_comprobante",
        sortable: false,
      },
      { text: "Número Comprobante", value: "num_comprobante", sortable: false },
      { text: "Fecha", value: "fecha_hora", sortable: false },
      { text: "Impuesto", value: "impuesto", sortable: false },
      { text: "Total", value: "total", sortable: false },
      { text: "Estado", value: "estado", sortable: false },
    ],
    cabeceraDetalles: [
      { text: "Borrar", value: "borrar", sortable: false },
      { text: "Artículo", value: "articulo", sortable: false },
      { text: "Cantidad", value: "cantidad", sortable: false },
      { text: "Precio", value: "precio", sortable: false },
      { text: "Subtotal", value: "subtotal", sortable: false },
    ],
    cabeceraArticulos: [
      { text: "Seleccionar", value: "seleccionar", sortable: false },
      { text: "Artículo", value: "articulo" },
      { text: "Categoría", value: "categoria" },
      { text: "Descripción", value: "descripcion", sortable: false },
      { text: "Stock", value: "stock", sortable: false },
      { text: "Precio Venta", value: "precio_venta", sortable: false },
    ],
    id: "",
    proveedores: [],
    comprobantes: ["FACTURA", "TICKET", "BOLETA", "CEDULA"],
    verArticulos: false,
    search: "",
    telefono: "",
    codigo: "",
    direccion: "",
    idproveedor: 0,
    idusuario: 0,
    tipo_comprobante: "001",
    serie_comprobante: "0100",
    num_comprobante: "0010",
    impuesto: 18,
    total: 0,
    detalles: [],

    totalParcial: 0,
    totalImpuesto: 0,
    articulos: [],
    texto: "",
    verDet: 0,
    //1 o 0
    verNuevo: 0,
    errorArticulo: null,

    email: "",

    adModal: 0,
    adAccion: 0,
    adNombre: "",
    adId: 0,
  }),

  computed: {
    calcularTotal: function () {
      var resultado = 0.0;
      for (var i = 0; i < this.detalles.length; i++) {
        resultado =
          resultado + this.detalles[i].precio * this.detalles[i].cantidad;
      }
      return resultado;
    },
  },

  watch: {
    dialog(val) {
      val || this.close();
    },
    dialogDelete(val) {
      val || this.closeDelete();
    },
  },

  created() {
    this.listCategories();
    this.select();
  },

  methods: {
    guardar() {
      let me = this;
      console.log(me.total);

      axios
        .post("Entries/Crear", {
          idproveedor: parseInt(me.idproveedor),
          idusuario: 2,
          tipo_comprobante: me.tipo_comprobante,
          serie_comprobante: me.serie_comprobante,
          num_comprobante: me.num_comprobante,
          impuesto: parseInt(me.impuesto),
          total: parseInt(me.total),
          detalles: this.detalles,
        })
        .then(function () {
          me.ocultarNuevo();
          me.listCategories();
          me.clean();
        })
        .catch(function (error) {
          console.log(error);
        });
    },
    agregarDetalle(caca) {
      //console.log(data["idarticulo"]);
      console.log("caca" + caca.nombre);

      this.detalles.push({
        idarticulo: parseInt(caca.idarticulo),
        cantidad: 1,
        precio: 1,
        num_comprobante: "1",
      });
      //}
    },
    activarDesactivarMostrar(accion, item) {
      this.adModal = 1;
      this.adNombre = item.num_comprobante;
      this.adId = item.idingreso;
      if (accion == 1) {
        this.adAccion = 1;
      } else if (accion == 2) {
        this.adAccion = 2;
      } else {
        this.adModal = 0;
      }
    },
    listCategories() {
      let me = this;
      axios
        .get("Entries/Listar")
        .then(function (response) {
          me.ingresos = response.data;
        })
        .catch(function (error) {
          console.log("" + error);
        });
    },
    listDetails(id) {
      let me = this;
      axios
        .get("Entries/ListarDetalles/" + id)
        .then(function (response) {
          me.detalles = response.data;
        })
        .catch(function (error) {
          console.log("" + error);
        });
    },
    verDetalles(item) {
      this.clean();
      this.tipo_comprobante = item.tipo_comprobante;
      this.serie_comprobante = item.serie_comprobante;
      this.num_comprobante = item.num_comprobante;
      this.idproveedor = item.idproveedor;
      this.impuesto = item.impuesto;
      this.listDetails(item.num_comprobante);
      this.verNuevo = 1;
      this.verDet = 1;
    },
    listByCode() {
      let me = this;

      me.errorArticulo = null;
      axios
        .get("Articles/BuscarCodigoIngreso/" + this.codigo)
        .then(function (response) {
          me.agregarDetalle(response.data);
        })
        .catch(function (error) {
          console.log(error);
          me.errorArticulo = "No existe el Artículo";
        });
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
    eliminarDetalle(arr, item) {
      var i = arr.indexOf(item);
      if (i !== -1) {
        arr.splice(i, 1);
      }
    },
    mostrarNuevo() {
      this.verNuevo = 1;
    },
    ocultarNuevo() {
      this.verNuevo = 0;
      this.clean();
    },
    mostrarArticulos() {
      this.verArticulos = true;
    },
    ocultarArticulos() {
      this.verArticulos = false;
    },
    select() {
      let me = this;
      var proveedores = [];
      axios
        .get("People/ListarProveedores")
        .then(function (response) {
          proveedores = response.data;
          proveedores.map(function (x) {
            me.proveedores.push({ text: x.nombre, value: x.idpersona });
          });
        })
        .catch(function (error) {
          console.log(error);
        });
    },
    listarArticulo() {
      let me = this;

      axios
        .get("Articles/ListarIngreso/" + me.texto)
        .then(function (response) {
          //console.log(response);
          me.articulos = response.data;
        })
        .catch(function (error) {
          console.log(error);
        });
    },

    close() {
      this.dialog = false;
      this.clean();
    },

    closeDelete() {
      this.dialogDelete = false;
      this.$nextTick(() => {
        this.editedItem = Object.assign({}, this.defaultItem);
        this.editedIndex = -1;
      });
    },
    activarDesactivarCerrar() {
      this.adModal = 0;
    },
    clean() {
      this.id = "";
      this.idproveedor = "";
      this.tipo_comprobante = "";
      this.serie_comprobante = "";
      this.num_comprobante = "";
      this.impuesto = 18;
      this.codigo = "";
      this.detalles = [];
      this.total = 0;
      this.totalImpuesto = 0;
      this.act_password = false;
      this.totalParcial = 0;
      this.verDet = 0;
    },
    desactivar() {
      let me = this;

      axios
        .put("Entries/Anular/" + this.adId)
        .then(function () {
          me.adModal = 0;
          me.adAccion = 0;
          me.adNombre = "";
          me.adId = "";
          me.listarArticulo();
        })
        .catch(function (error) {
          console.log(error);
        });
    },
  },
};
</script>
