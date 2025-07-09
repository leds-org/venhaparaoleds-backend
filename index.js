// const express = require('express');

const port = 3000;

const app = require('./app');

app.listen(port, () => {
    console.log('Backend rodando. Porta: ', port);
});