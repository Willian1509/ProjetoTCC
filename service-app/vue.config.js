module.exports = {
  devServer: {
    proxy: {
      '/api': {
        target: 'https://pjoqqavuysmvp4ope7hyvkaicq0iviqh.lambda-url.sa-east-1.on.aws/',
        changeOrigin: true,
        pathRewrite: { '^/api': '' },
      },
    },
  },
};
