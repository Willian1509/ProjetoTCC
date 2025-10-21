const express = require('express');
const cors = require('cors');
const bodyParser = require('body-parser');

const app = express();
app.use(cors());
app.use(bodyParser.json());

// Importar rotas
const authRoutes = require('./routes/authRoutes');
const userRoutes = require('./routes/userRoutes');
const pesquisaRoutes = require('./routes/pesquisaRoutes');

app.use('/api', authRoutes);
app.use('/api', userRoutes);
app.use('/api', pesquisaRoutes);

// Iniciar servidor
const PORT = 3306;
app.listen(PORT, () => {
  console.log(`Servidor rodando na porta ${PORT}`);
});
