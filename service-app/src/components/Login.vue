<template>
  <v-container fluid fill-height class="login-background">
    <v-row align="center" justify="center" class="fill-height">
      <v-col cols="12" sm="8" md="4">
        <v-card class="pa-5 login-card" elevation="10">
          <v-card-title class="text-h5 font-weight-bold text-center">Login</v-card-title>
          <v-card-subtitle class="text-center mb-3">Entre com seus dados</v-card-subtitle>

          <v-card-text>
            <v-form ref="form" v-model="valid">
              <v-text-field
                v-model="email"
                label="Email"
                outlined
                :rules="[rules.required, rules.email]"
                prepend-icon="mdi-email"
                class="mb-4"
                aria-label="Email"
              ></v-text-field>

              <v-text-field
                v-model="senha"
                label="Senha"
                outlined
                type="password"
                :rules="[rules.required]"
                prepend-icon="mdi-lock"
                class="mb-4"
                aria-label="Senha"
              ></v-text-field>
            </v-form>
          </v-card-text>

          <v-card-actions class="d-flex flex-column justify-center">
            <v-btn color="primary" class="mb-2" @click="login" :disabled="!valid || loading">
              <v-progress-circular
                v-if="loading"
                indeterminate
                color="white"
                size="24"
                class="mr-2"
              ></v-progress-circular>
              Entrar
            </v-btn>
            <v-btn color="secondary" text @click="irParaCadastro">Cadastrar</v-btn>
          </v-card-actions>

          <!-- Alerta de erro -->
          <v-alert v-if="errorMessage" type="error" bordered prominent>
            {{ errorMessage }}
          </v-alert>
        </v-card>
      </v-col>
    </v-row>
  </v-container>
</template>

<script>
import axios from 'axios'; // Importando o Axios

export default {
  name: 'Login',
  data() {
    return {
      valid: false,
      email: '',
      senha: '',
      errorMessage: '', // Para exibir mensagens de erro
      loading: false, // Para controlar o estado de carregamento
      rules: {
        required: (value) => !!value || 'Campo obrigatório.',
        email: (value) => {
          const pattern = /^[a-z0-9._%+-]+@[a-z0-9.-]+\.[a-z]{2,4}$/;
          return pattern.test(value) || 'Email inválido.';
        },
      },
    };
  },
  methods: {
    async login() {
      this.loading = true; // Inicia o carregamento
      const loginData = {
        Email: this.email,
        Senha: this.senha,
      };

      try {
        const response = await axios.post('/api/login', loginData);

        // Verifica se a resposta contém a mensagem de sucesso
        if (response.data.message === 'Login bem-sucedido!') {
          this.$router.push('/pesquisa');
        } else {
          this.errorMessage = response.data.message || 'Erro desconhecido no login.';
        }
      } catch (error) {
        if (error.response) {
          // O servidor retornou um código de erro
          this.errorMessage = `Erro: ${error.response.data.message || 'Falha na autenticação.'}`;
        } else if (error.request) {
          // A requisição foi feita, mas não houve resposta
          this.errorMessage = 'Erro de comunicação com o servidor. Tente novamente.';
        } else {
          // Algo deu errado na configuração da requisição
          this.errorMessage = `Erro ao configurar a requisição: ${error.message}`;
        }
      } finally {
        this.loading = false; // Finaliza o carregamento
      }
    },
    irParaCadastro() {
      this.$router.push('/cadastro');
    },
  },
};
</script>

<style scoped>
.login-background {
  background-image: url('@/assets/login-image.jpg');
  background-size: cover;
  height: 100vh;
}

.login-card {
  background-color: rgba(255, 255, 255, 0.8);
  border-radius: 16px;
  box-shadow: 0 8px 16px rgba(0, 0, 0, 0.1);
}

.v-alert {
  margin-top: 16px;
}
</style>
