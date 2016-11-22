var abigen = require('../');
var path = require('path');

abigen.generateCode(path.join(__dirname, 'Multiplication.json'), 'cs-service');
abigen.generateCode(path.join(__dirname, 'functionDTO.json'), 'cs-service');