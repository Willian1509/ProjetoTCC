<template>
  <v-container>
    <v-card class="mx-auto" max-width="600">
      <v-card-title>Histórico de Serviços</v-card-title>
      <v-card-text>
        <!-- Histórico de serviços solicitados -->
        <v-list>
          <v-list-item v-for="(item, index) in historicos" :key="index">
            <v-list-item-content>
              <v-list-item-title>{{ item.servico }}</v-list-item-title>
              <v-list-item-subtitle>{{ item.data }}</v-list-item-subtitle>
            </v-list-item-content>
          </v-list-item>
        </v-list>
      </v-card-text>
    </v-card>
  </v-container>
</template>

<script>
export default {
  name: 'Historico',
  data() {
    return {
      historicos: [
        { servico: 'Cortar Grama', data: '2024-09-20' },
        { servico: 'Maquinar Escavação', data: '2024-09-18' },
      ],
    };
  },
}
</script>

<style scoped>
.v-card {
  border: 1px solid black;
}
</style>
