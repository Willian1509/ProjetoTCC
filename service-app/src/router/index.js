import { createRouter, createWebHistory } from 'vue-router'
import Cadastro from '../components/Cadastro.vue'
import Pesquisa from '../components/Pesquisa.vue'
import Historico from '../components/Historico.vue'
import Login from '../components/Login.vue'
import Provedor from '../components/Provedor.vue'

const routes = [
  { path: '/', redirect: '/login' }, // Redireciona para a tela de login
  { path: '/login', component: Login },
  { path: '/cadastro', component: Cadastro },
  { path: '/pesquisa', component: Pesquisa },
  { path: '/historico', component: Historico },
  { path: '/provedor', component: Provedor },
]

const router = createRouter({
  history: createWebHistory(),
  routes,
})

export default router
