<template>
  <v-layout align-start>
    <v-flex>
      <v-data-table
        :headers="headers"
        :items="roles"
        sort-by="calories"
        class="elevation-1"
      >
        <template v-slot:top>
          <v-toolbar text>
                 <v-toolbar-title>Roles</v-toolbar-title>
            <v-divider class="mx-4" inset vertical></v-divider>
            <v-spacer></v-spacer>
          </v-toolbar>
        </template>
        <template v-slot:item="props">
          <tr>
            <td>{{ props.item.nombre }}</td>
            <td>{{ props.item.descripcion }}</td>
            <td>
              <div v-if="props.item.condicion">
                <span class="blue--text">Active</span>
              </div>
              <div v-else>
                <span class="red--text">Inactive</span>
              </div>
            </td>
          </tr>
        </template>

        <template slot="no-data">
          <v-btn color="primary">Reset</v-btn>
        </template>
      </v-data-table>
    </v-flex>
  </v-layout>
</template>

<script>
import axios from "axios";
export default {
  name: "Categories",
  data: () => ({
    roles: [],
    dialog: false,
    dialogDelete: false,
    headers: [
      { text: "Nombre", value: "nombre" },
      { text: "Descripcion", value: "descripcion", sortable: false },
      { text: "Estado", value: "condicion", sortable: false },
    ],
    search: "",
  }),

  computed: {},

  watch: {},

  created() {
    this.listCategories();
  },

  methods: {
    listCategories() {
      let me = this;
      axios
        .get("Rols/Listar")
        .then(function(response) {
          me.roles = response.data;
        })
        .catch(function(error) {
          console.log(error);
        });
    },
  },
};
</script>
