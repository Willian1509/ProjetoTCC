const express = require('express');
const router = express.Router();
const db = require('../config/db');

// Cadastro
router.post('/cadastro', (req, res) => {
  const { nome, email, telefone, senha, cidade, estado, tipoUsuario, servico } = req.body;

  db.query(
    'SELECT * FROM usuarios WHERE email = ?',
    [email],
    (err, results) => {
      if (err) return res.status(500).json({ message: 'Erro no servidor' });

      if (results.length > 0) {
        return res.status(400).json({ message: 'Email já cadastrado!' });
      }

      db.query(
        'INSERT INTO usuarios (nome, email, telefone, senha, cidade, estado, tipoUsuario, servico) VALUES (?, ?, ?, ?, ?, ?, ?, ?)',
        [nome, email, telefone, senha, cidade, estado, tipoUsuario, servico],
        (err, result) => {
          if (err) return res.status(500).json({ message: 'Erro ao cadastrar' });
          res.json({ message: 'Cadastro realizado com sucesso!' });
        }
      );
    }
  );
});

module.exports = router;
