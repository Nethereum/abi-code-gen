var ejs = require('ejs');
var fs = require('fs-sync');
var path = require('path');

var abicodegen = module.exports = {};

abicodegen.generateCode = function(outputAbiJsonFilePath, generatorName) {

    var generatorPath = path.join(__dirname, 'templates/' + generatorName + '.ejs');
    
    var outputPathInfo = path.parse(outputAbiJsonFilePath);
    var contractName = outputPathInfo.name;

    var compilationOutput = JSON.parse(fs.read(outputAbiJsonFilePath, 'utf8'));
    var settingsFileName = path.join(outputPathInfo.dir,contractName + '-' + generatorName + '.json'); 
    var settings = {};

    if(fs.exists(settingsFileName)){
        settings = JSON.parse(fs.read(settingsFileName,'utf8'));
    }

    var combinedInput={};
    for(var key in compilationOutput) combinedInput[key]=compilationOutput[key];
    for(var key in settings) combinedInput[key]=settings[key];

    combinedInput.abi = JSON.parse(combinedInput.abi.split('\\').join());

    if (typeof combinedInput.contractName === "undefined") {
    combinedInput.contractName = contractName;
    }

    var fileNameOutput = path.join(outputPathInfo.dir, combinedInput.contractName + '.cs');

    if(fs.exists(fileNameOutput)){
        fs.remove(fileNameOutput);
    }

    var template = ejs.compile(fs.read(generatorPath, 'utf8'));
    fs.write(fileNameOutput, template(combinedInput));

}


