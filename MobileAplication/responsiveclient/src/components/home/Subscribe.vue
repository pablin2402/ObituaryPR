<template>
  <v-container id="subscribe" tag="section">
    <v-card color="rgba(203, 170, 92, 0.51)" class="pa-3">
      <v-container>
        <v-container>
          <v-row>
            <v-col cols="12" md="5">
              <h1 class="mb-3">Subscribe</h1>

              <p>
                Si tienes alguna duda o consulta puedes poner tu dirección de
                correo electrónico y nos comunicaremos contigo
              </p>

              <v-row class="pa-2">
                <v-responsive class="overflow-visible mr-2" max-width="400">
                  <v-text-field hide-details label="Your Email Address" solo />
                </v-responsive>

                <v-btn
                  :block="$vuetify.breakpoint.xsOnly"
                  class="ma-0"
                  color="secondary"
                  style="height: 48px"
                >
                  Contacto
                </v-btn>
              </v-row>
            </v-col>

            <v-col cols="12" md="6" offset-md="1">
              <h2>Contacto</h2>

              <v-list class="transparent">
                <v-list-item>
                  <v-list-item-avatar color="primary">
                    <v-icon class="elevation-4" dark>
                      mdi-email-outline
                    </v-icon>
                  </v-list-item-avatar>

                  <v-list-item-title>CORREO ELECTRÓNICO</v-list-item-title>

                  <v-list-item-subtitle>
                    pablorivas04@icloud.com
                  </v-list-item-subtitle>
                </v-list-item>

                <v-list-item>
                  <v-list-item-avatar color="primary">
                    <v-icon class="elevation-4" dark> mdi-map-marker </v-icon>
                  </v-list-item-avatar>

                  <v-list-item-title>DIRECCIÓN</v-list-item-title>

                  <v-list-item-subtitle>
                    COCHABAMBA-BOLIVIA
                  </v-list-item-subtitle>
                </v-list-item>

                <v-list-item>
                  <v-list-item-avatar color="primary">
                    <v-icon class="elevation-4" dark> mdi-phone </v-icon>
                  </v-list-item-avatar>

                  <v-list-item-title>TELÉFONO</v-list-item-title>

                  <v-list-item-subtitle> 69501045 </v-list-item-subtitle>
                </v-list-item>
              </v-list>
            </v-col>
          </v-row>
        </v-container>
      </v-container>
    </v-card>
  </v-container>
</template>

<script>
export default {
  name: "HomeSubscribe",
};
</script>
