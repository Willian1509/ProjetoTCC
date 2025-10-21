const express = require('express');
const router = express.Router();
const db = require('../config/db');

// Pesquisa de Provedores
router.post('/pesquisa', (req, res) => {
  const { servico } = req.body;

  db.query(
    "SELECT id, nome, telefone, cidade, estado, tipoUsuario, servico FROM usuarios WHERE tipoUsuario = 'Provedor' AND servico LIKE ?",
    [`%${servico}%`],
    (err, results) => {
      if (err) return res.status(500).json({ message: 'Erro na busca' });

      res.json({ provedores: results });
    }
  );
});

module.exports = router;
