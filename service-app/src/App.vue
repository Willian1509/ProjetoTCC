<template>
  <v-app>
    <!-- Aplicação do background -->
    <div class="app-background">
      <v-container fill-height fluid class="d-flex align-center justify-center">
        <!-- Router-view para trocar entre as páginas -->
        <router-view />
      </v-container>
    </div>
  </v-app>
</template>

<script>
export default {
  name: 'App',
};
</script>

<style>
/* Define o background da aplicação */
.app-background {
  background-image: url('@/assets/login-image.jpg'); /* Certifique-se de que o caminho está correto */
  background-size: cover;
  background-position: center;
  height: 100vh; /* Preenche a tela toda */
  width: 100vw;
  position: relative;
  overflow: auto;
}

/* Adicionalmente, se precisar de ajuste no card de pesquisa */
.v-card {
  background-color: rgba(255, 255, 255, 0.8); /* Deixe os cards levemente transparentes */
  border-radius: 10px;
  padding: 20px;
  box-shadow: 0px 2px 10px rgba(0, 0, 0, 0.2);
}

/* Estilo responsivo */
@media (max-width: 600px) {
  .v-card {
    max-width: 90%; /* Ajusta o tamanho dos cards em telas menores */
  }
}
</style>
