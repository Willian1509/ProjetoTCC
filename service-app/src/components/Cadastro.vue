<template>
  <v-container fluid fill-height class="cadastro-background">
    <v-row align="center" justify="center" class="fill-height">
      <v-col cols="12" sm="8" md="6">
        <v-card class="pa-5 cadastro-card" elevation="10">
          <v-card-title class="text-h5 font-weight-bold text-center">Cadastro</v-card-title>
          <v-card-subtitle class="text-center mb-3">Preencha os campos abaixo</v-card-subtitle>

          <v-card-text>
            <v-form ref="form" v-model="valid">
              <v-text-field
                v-model="nome"
                label="Nome"
                outlined
                :rules="[rules.required]"
                prepend-icon="mdi-account"
                class="mb-4"
              ></v-text-field>

              <v-text-field
                v-model="email"
                label="Email"
                outlined
                :rules="[rules.required, rules.email]"
                prepend-icon="mdi-email"
                class="mb-4"
              ></v-text-field>

              <v-text-field
                v-model="telefone"
                label="Telefone"
                outlined
                :rules="[rules.required]"
                prepend-icon="mdi-phone"
                class="mb-4"
              ></v-text-field>

              <v-text-field
                v-model="senha"
                label="Senha"
                outlined
                type="password"
                :rules="[rules.required]"
                prepend-icon="mdi-lock"
                class="mb-4"
              ></v-text-field>

              <!-- Campos Cidade e Estado lado a lado -->
              <v-row>
                <v-col cols="12" sm="6">
                  <v-text-field
                    v-model="cidade"
                    label="Cidade"
                    outlined
                    :rules="[rules.required]"
                    prepend-icon="mdi-city"
                  ></v-text-field>
                </v-col>

                <v-col cols="12" sm="6">
                  <v-text-field
                    v-model="estado"
                    label="Estado"
                    outlined
                    :rules="[rules.required]"
                    prepend-icon="mdi-map-marker"
                  ></v-text-field>
                </v-col>
              </v-row>
            </v-form>
          </v-card-text>

          <v-card-actions class="d-flex flex-column justify-center">
            <v-btn color="primary" class="mb-2" @click="irParaProximaTela" :disabled="!valid">Próximo</v-btn>
          </v-card-actions>
        </v-card>
      </v-col>
    </v-row>
  </v-container>
</template>

<script>
export default {
  name: 'Cadastro',
  data() {
    return {
      valid: false,
      nome: '',
      email: '',
      telefone: '',
      senha: '',
      cidade: '',
      estado: '',
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
    irParaProximaTela() {
      if (this.$refs.form.validate()) {
        // Redirecionar para a tela de Provedor, passando os dados
        this.$router.push({
          path: '/provedor',
          query: {
            nome: this.nome,
            email: this.email,
            telefone: this.telefone,
            senha: this.senha,
            cidade: this.cidade,
            estado: this.estado,
          },
        });
      }
    },
  },
};
</script>

<style scoped>
.cadastro-background {
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
