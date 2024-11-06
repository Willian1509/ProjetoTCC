<template>
  <v-container class="pesquisa-background fill-height">
    <v-row align="center" justify="center" class="fill-height">
      <v-col cols="12" sm="8" md="6">
        <v-card class="pa-5 pesquisa-card" elevation="10">
          <v-card-title class="text-h5 font-weight-bold text-center">Pesquisar Provedores</v-card-title>
          <v-card-subtitle class="text-center mb-3">Digite o serviço para procurar provedores</v-card-subtitle>

          <v-card-text>
            <v-autocomplete
              v-model="servico"
              :items="servicosSugeridos"
              label="Serviço"
              outlined
              clearable
              @input="filtrarProvedores"
            ></v-autocomplete>
          </v-card-text>
        </v-card>
      </v-col>
    </v-row>

    <v-row v-if="provedores.length" class="mt-5" justify="center">
      <v-col cols="12" sm="8" md="6">
        <v-card class="pa-3 resultado-card" elevation="8">
          <v-card-title class="text-h6">Provedores Encontrados</v-card-title>
          <v-card-text>
            <div v-for="provedor in provedores" :key="provedor.id">
              <v-card class="my-3" outlined>
                <v-card-title>{{ provedor.nome }}</v-card-title>
                <v-card-subtitle>{{ provedor.telefone }}</v-card-subtitle>
                <v-card-text>
                  <p><strong>Cidade:</strong> {{ provedor.cidade }}</p>
                  <p><strong>Estado:</strong> {{ provedor.estado }}</p>
                  <p><strong>Serviços:</strong> {{ formatarServicos(provedor.servicos) }}</p>
                  <p><strong>Tipo de Usuário:</strong> {{ provedor.tipoUsuario }}</p>
                </v-card-text>
              </v-card>
            </div>
          </v-card-text>
        </v-card>
      </v-col>
    </v-row>
  </v-container>
</template>

<script>
import axios from 'axios';

export default {
  name: 'Pesquisa',
  data() {
    return {
      servico: '',
      servicosSugeridos: [],
      provedores: [],
    };
  },
methods: {
    async filtrarProvedores() {
        if (this.servico.length > 1) {
            try {
                const response = await axios.post('https://<sua-url-lambda>', {
                    servico: this.servico,
                });
                this.provedores = response.data.provedores; // Atualizar os provedores conforme digita
            } catch (error) {
                console.error('Erro ao buscar provedores:', error);
            }
        } else {
            this.provedores = []; // Limpar os resultados se o input for muito curto
        }
    },
    formatarServicos(servicos) {
        return servicos.join(', '); // Formatar o array de serviços como uma string separada por vírgulas
    }
},
};
</script>

<style scoped>
.pesquisa-background {
  background-image: url('@/assets/login-image.jpg');
  background-size: cover;
  background-position: center;
  background-repeat: no-repeat;
  min-height: 100vh;
  width: 100vw;
  display: flex;
  align-items: center;
  justify-content: center;
}

.fill-height {
  height: 100%;
}

.pesquisa-card {
  background-color: rgba(255, 255, 255, 0.85);
  border-radius: 16px;
  box-shadow: 0 8px 16px rgba(0, 0, 0, 0.2);
}

.resultado-card {
  background-color: rgba(255, 255, 255, 0.85);
  border-radius: 8px;
  box-shadow: 0 6px 12px rgba(0, 0, 0, 0.15);
}

.v-card-title {
  color: #1976d2;
}
</style>
