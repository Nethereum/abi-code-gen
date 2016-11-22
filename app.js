var ejs = require('ejs');
var fs = require('fs-sync');
var fileName = 'test.cs';
var join = require('path').join;
var path = join(__dirname, '/cs-service.ejs');

data = { namespace: "MyNamespace",
         contractName: "myContract",
         byteCode: fs.read('test.bin'),
         abi: eval(fs.read('functionDTOTest.abi'))
       };

if(fs.exists(fileName)){
    fs.remove(fileName);
}

var template = ejs.compile(fs.read(path, 'utf8'));
fs.write(fileName, template(data));

