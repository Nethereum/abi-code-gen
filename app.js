var ejs = require('ejs');
var fs = require('fs-sync');
var fileName = 'test.cs';
var join = require('path').join;
var path = join(__dirname, '/cs-service.ejs');

data = { namespace: "MyNamespace",
         contractName: "myContract",
         abi: eval(fs.read('test.abi'))
       };

if(fs.exists(fileName)){
    fs.remove(fileName);
}

var template = ejs.compile(fs.read(path, 'utf8'));
fs.write(fileName, template(data));

