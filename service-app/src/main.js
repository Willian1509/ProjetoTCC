import { createApp } from 'vue'
import App from './App.vue'
import router from './router'
import vuetify from './plugins/vuetify'
import '@mdi/font/css/materialdesignicons.css'
import axios from 'axios';

// Configurar Axios
axios.defaults.baseURL = 'https://q7rrkrgdsi4boqfi5lrqipgkxu0fpich.lambda-url.sa-east-1.on.aws/';

const app = createApp(App);
app.use(router);
app.use(vuetify);
app.mount('#app');
