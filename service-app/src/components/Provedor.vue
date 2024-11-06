<template>
  <v-container fluid fill-height class="provedor-background">
    <v-row align="center" justify="center" class="fill-height">
      <v-col cols="12" sm="8" md="6">
        <v-card class="pa-5 cadastro-card" elevation="10">
          <v-card-title class="text-h5 font-weight-bold text-center">Tipo de Usuário</v-card-title>
          <v-card-subtitle class="text-center mb-3">Selecione se você é um cliente ou provedor</v-card-subtitle>

          <v-card-text>
            <v-form ref="form" v-model="valid">
              <v-select
                v-model="tipoUsuario"
                :items="['Cliente', 'Provedor']"
                label="Selecione o tipo de usuário"
                outlined
                :rules="[rules.required]"
                class="mb-4"
              ></v-select>

              <!-- Mostrar serviços apenas se o usuário for Provedor -->
              <v-checkbox-group
                v-if="tipoUsuario === 'Provedor'"
                v-model="servicosSelecionados"
                :rules="[rules.requiredIfProvedor]"
                class="mb-4"
              >
                <v-checkbox label="Cortar Grama" value="Cortar Grama"></v-checkbox>
                <v-checkbox label="Maquinar Escavação" value="Maquinar Escavação"></v-checkbox>
                <v-checkbox label="Pintura" value="Pintura"></v-checkbox>
                <!-- Adicione mais opções conforme necessário -->
              </v-checkbox-group>
            </v-form>
          </v-card-text>

          <v-card-actions class="d-flex flex-column justify-center">
            <v-btn color="primary" class="mb-2" @click="cadastrar" :disabled="!valid">Cadastrar</v-btn>
          </v-card-actions>
        </v-card>
      </v-col>
    </v-row>
  </v-container>
</template>

<script>
export default {
  name: 'Provedor',
  data() {
    return {
      valid: false,
      tipoUsuario: '',
      servicosSelecionados: [],
      rules: {
        required: (value) => !!value || 'Campo obrigatório.',
        requiredIfProvedor: (value) => {
          if (this.tipoUsuario === 'Provedor') {
            return value.length > 0 || 'Selecione pelo menos um serviço.';
          }
          return true; // Não precisa validar se o tipo de usuário for Cliente
        },
      },
    };
  },
  methods: {
    async cadastrar() {
      if (this.$refs.form.validate()) {
        try {
          const dadosCadastro = {
            nome: this.$route.query.nome,
            email: this.$route.query.email,
            telefone: this.$route.query.telefone,
            senha: this.$route.query.senha,
            cidade: this.$route.query.cidade,
            estado: this.$route.query.estado,
            tipoUsuario: this.tipoUsuario,
            servico: this.tipoUsuario === 'Provedor' ? JSON.stringify(this.servicosSelecionados) : null,
          };

          const response = await fetch('https://a7raw27fk2joikwvbwrlzm7uxa0epmnt.lambda-url.sa-east-1.on.aws/', {
            method: 'POST',
            body: JSON.stringify(dadosCadastro),
            headers: { 'Content-Type': 'application/json' },
          });

          if (response.ok) {
            this.$router.push('/pesquisa');
          } else {
            console.error('Erro ao cadastrar:', response);
          }
        } catch (error) {
          console.error('Erro ao cadastrar:', error);
        }
      }
    },
  },
};
</script>

<style scoped>
.provedor-background {
  background-image: url('@/assets/login-image.jpg');
  background-size: cover;
  height: 100vh;
}

.fill-height {
  height: 100%;
}

.cadastro-card {
  background-color: rgba(255, 255, 255, 0.85); /* Leve transparência no card */
  border-radius: 16px;
  box-shadow: 0 8px 16px rgba(0, 0, 0, 0.2); /* Sombra para o card */
}

.v-card-title {
  color: #1976d2; /* Azul escuro */
}

.mb-4 {
  margin-bottom: 16px;
}

.v-btn {
  width: 100%;
}
</style>
