const express = require('express');

const app = express();

app.use(express.static('./dist/KayCafet'));

app.get('/*', (req, res) =>
    res.sendFile('index.html', {root: 'dist/KayCafet/'}),
);

app.listen(process.env.PORT || 8080);