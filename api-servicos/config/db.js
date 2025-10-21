const mysql = require('mysql2');

const connection = mysql.createConnection({
  host: '127.0.0.1',     // seu host
  user: 'root',          // usuário do MySQL
  password: 'Bemvindo@21', // senha
  database: 'Servises'   // nome do banco
});

connection.connect((err) => {
  if (err) {
    console.error('Erro ao conectar ao MySQL:', err);
    return;
  }
  console.log('Conectado ao MySQL!');
});

module.exports = connection;