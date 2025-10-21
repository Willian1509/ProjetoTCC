const express = require('express');
const router = express.Router();
const db = require('../config/db');

// Login
router.post('/login', (req, res) => {
  const { Email, Senha } = req.body;

  db.query(
    'SELECT * FROM usuarios WHERE email = ? AND senha = ?',
    [Email, Senha],
    (err, results) => {
      if (err) {
        return res.status(500).json({ message: 'Erro no servidor' });
      }

      if (results.length > 0) {
        res.json({ message: 'Login bem-sucedido!' });
      } else {
        res.status(401).json({ message: 'Email ou senha inválidos.' });
      }
    }
  );
});

module.exports = router;
